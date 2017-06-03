using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.OPManage;
using HIS_OPManage.Dao;

namespace HIS_OPManage.ObjectModel
{
    /// <summary>
    /// 缴款处理
    /// </summary>
    public class AccountProcess : AbstractObjectModel
    {
        /// <summary>
        /// 获取未缴款记录
        /// </summary>
        /// <param name="empid">操作员ID</param>
        /// <returns>未缴款记录</returns>
        public List<OP_Account> GetNotAccountByEmp(int empid)
        {
            List<OP_Account> notAccountList = NewDao<IOPManageDao>().GetAccountByEmp(empid,0,DateTime.Now,DateTime.Now);
            return notAccountList;
        }

        /// <summary>
        /// 获取已经缴款记录
        /// </summary>
        /// <param name="empid">人员ID</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <returns>已经缴款记录</returns>
        public List<OP_Account> GetHistoryAccountByEmp(int empid,DateTime bdate,DateTime edate)
        {
            List<OP_Account> accountList = NewDao<IOPManageDao>().GetAccountByEmp(empid, 1, bdate, edate);
            return accountList;
        }

        /// <summary>
        /// 获取票据信息
        /// </summary>
        /// <param name="accountid">缴款ID</param>
        /// <returns>DataTable票据信息</returns>
        public DataTable GetAccountInvoiceData(int accountid)
        {
            DataTable dtRefundInvoice = NewDao<IOPManageDao>().GetAccountInvoiceData(accountid);
            return dtRefundInvoice;
        }

        /// <summary>
        /// 获取支付方式明细
        /// </summary>
        /// <param name="accountid">缴款ID</param>
        /// <param name="accountFlag">缴款状态 0未缴款1已经缴款</param>
        /// <returns>DataTable支付方式明细</returns>
        public DataTable GetAccountPatmentInfo(int accountid, int accountFlag)
        {
            if (accountFlag == 0)
            {
                //从明细统
                DataTable dtPayMent = NewDao<IOPManageDao>().GetAccountPayMent(accountid);
                DataTable dtPay = dtPayMent.Clone();
                dtPay.Clear();
                for (int i = 0; i < dtPayMent.Rows.Count; i++)
                {
                    if (Convert.ToDecimal(dtPayMent.Rows[i]["paymentcount"]) == 0)
                    {
                        continue;
                    }

                    dtPay.Rows.Add(dtPayMent.Rows[i].ItemArray);
                }

                return dtPay;
            }
            else
            {
                List<OP_AccountPatMentInfo> accountPayList = NewObject<OP_AccountPatMentInfo>().getlist<OP_AccountPatMentInfo>(" accountid="+accountid);
                return EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable(accountPayList);
            }
        }

        /// <summary>
        /// 获取项目信息
        /// </summary>
        /// <param name="accountid">交款ID</param>
        /// <returns>DataTable项目信息</returns>
        public DataTable GetAccountItemData(int accountid)
        {
            DataTable dtItemDate = NewDao<IOPManageDao>().GetAccountItemData(accountid);
            return dtItemDate;
        }

        /// <summary>
        /// 获取收费费用
        /// </summary>
        /// <param name="accountid">缴款ID</param>
        /// <returns>DataTable收费费用</returns>
        public DataTable GetRegBalanceFee(int accountid)
        {
            DataTable dtRegBalanceFee = NewDao<IOPManageDao>().GetRegBalanceFee(accountid);
            return dtRegBalanceFee;
        }

        /// <summary>
        /// 获取票据明细
        /// </summary>
        /// <param name="invoiceID">发票卷序号</param>
        /// <param name="invoiceType">票据类型 0正常 1退费</param>
        /// <param name="accountid">缴款ID</param>
        /// <returns>票据明细</returns>
        public DataTable GetInvoiceDetail(int invoiceID,int invoiceType,int accountid)
        {
            DataTable dtInvoiceDetail = NewDao<IOPManageDao>().GetInvoiceDetail(invoiceID, invoiceType, accountid);
            if (dtInvoiceDetail.Rows.Count > 0)
            {                
                List<OP_CostPayMentInfo> payInfoList = NewObject<OP_CostPayMentInfo>().getlist<OP_CostPayMentInfo>(" accountid="+accountid);
                for (int i = 0; i < dtInvoiceDetail.Rows.Count; i++)
                {
                    int costHeadid = Convert.ToInt32(dtInvoiceDetail.Rows[i]["costheadid"]);
                    List<OP_CostPayMentInfo> payInfos = payInfoList.Where(p => p.CostHeadID == costHeadid).ToList();
                    foreach (OP_CostPayMentInfo payInfo in payInfos)
                    {
                        if (dtInvoiceDetail.Columns.Contains(payInfo.PayMentName))
                        {
                            dtInvoiceDetail.Rows[i][payInfo.PayMentName] = payInfo.PayMentMoney;
                        }
                        else
                        {
                            DataColumn col = new DataColumn();
                            col.ColumnName = payInfo.PayMentName;
                            col.DataType = typeof(decimal);
                            dtInvoiceDetail.Columns.Add(col);
                            dtInvoiceDetail.Rows[i][payInfo.PayMentName] = payInfo.PayMentMoney;
                        }
                    }
                }
            }

            return dtInvoiceDetail;
        }

        /// <summary>
        /// 提交结账
        /// </summary>
        /// <param name="account">缴款对象</param>
        /// <returns>返回缴款对象</returns>
        public OP_Account SubmitAccount(OP_Account account)
        {
            OP_Account curAccount = NewObject<OP_Account>().getmodel(account.AccountID) as OP_Account;
            if (curAccount.AccountFlag != 0)
            {
                throw new Exception("已经结账不允许再交结账,请刷新界面数据");
            }

            //挂号收费交款
            if (account.AccountType == 0)
            {
                if (curAccount.TotalFee != account.TotalFee || curAccount.CashFee != account.CashFee)
                {
                    throw new Exception("结账数据发生变化,请刷新界面数据");
                }

                DataTable dtPayMent = NewDao<IOPManageDao>().GetAccountPayMent(curAccount.AccountID);
                for (int i = 0; i < dtPayMent.Rows.Count; i++)
                {
                    if (Convert.ToDecimal(dtPayMent.Rows[i]["paymentcount"]) == 0)
                    {
                        continue;
                    }

                    OP_AccountPatMentInfo accountPayment = new OP_AccountPatMentInfo();
                    accountPayment.AccountID = curAccount.AccountID;
                    accountPayment.PayMentCode = dtPayMent.Rows[i]["paymentcode"].ToString();
                    accountPayment.PayMentCount = Convert.ToInt32(dtPayMent.Rows[i]["paymentcount"]);
                    accountPayment.PayMentID = Convert.ToInt32(dtPayMent.Rows[i]["paymentid"]);
                    accountPayment.PayMentMoney = Convert.ToDecimal(dtPayMent.Rows[i]["paymentmoney"]);
                    accountPayment.PayMentName = dtPayMent.Rows[i]["paymentname"].ToString();
                    this.BindDb(accountPayment);
                    accountPayment.save();
                }
            }           
            else
            {
                //会员充值交款
                DataTable dt = GetAccountDataME(account.AccountID);  
                if (dt == null || dt.Rows.Count <= 0)
                {
                    throw new Exception("结账数据发生变化,请刷新界面数据");
                }

                decimal totalFee = Convert.ToDecimal(dt.Compute("sum(Money)", string.Empty));
                if (account.TotalFee != totalFee)
                {
                    throw new Exception("结账数据发生变化,请刷新界面数据");
                }

                bool bResult = NewDao<IOPManageDao>().UpdateAccountME(account.AccountID);
                if (!bResult)
                {
                    throw new Exception("更新明细表失败！请重新操作！");
                }

                int cashCount = dt.Select(" PayType = '现金'").Length;                
                int posCount = dt.Select(" PayType = 'POS'").Length; 

                if (cashCount > 0)
                {
                    decimal cashFee = Convert.ToDecimal(dt.Compute("sum(Money)", " PayType = '现金'"));
                    OP_AccountPatMentInfo accountPayment = new OP_AccountPatMentInfo();
                    accountPayment.AccountID = curAccount.AccountID;
                    accountPayment.PayMentCode = "01";
                    accountPayment.PayMentCount = cashCount;
                    accountPayment.PayMentID = 1002;
                    accountPayment.PayMentMoney = cashFee;
                    accountPayment.PayMentName = "现金支付";
                    this.BindDb(accountPayment);
                    accountPayment.save();
                }

                if (posCount > 0)
                {
                    decimal posFee = Convert.ToDecimal(dt.Compute("sum(Money)", " PayType = 'POS'"));
                    OP_AccountPatMentInfo accountPayment = new OP_AccountPatMentInfo();
                    accountPayment.AccountID = curAccount.AccountID;
                    accountPayment.PayMentCode = "02";
                    accountPayment.PayMentCount = posCount;
                    accountPayment.PayMentID = 1004;
                    accountPayment.PayMentMoney = posFee;
                    accountPayment.PayMentName = "POS支付";
                    this.BindDb(accountPayment);
                    accountPayment.save();
                }

                curAccount.TotalFee = account.TotalFee;
                curAccount.CashFee = account.CashFee;
                curAccount.PosFee = account.PosFee;
            }

            int jkdh = NewDao<IOPManageDao>().GetMaxJkdh();
            curAccount.AccountFlag = 1;
            curAccount.AccountDate = DateTime.Now;
            curAccount.ReceivBillNO = jkdh + 1;
            this.BindDb(curAccount);
            curAccount.save();
            return curAccount;
        }

        /// <summary>
        /// 查询所有已经缴款记录
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="empid">人员ID</param>
        /// <param name="status">状态0未缴款1已缴款</param>
        /// <returns>DataTable所有已经缴款记录</returns>
        public DataTable QueryAllAccount(DateTime bdate, DateTime edate, int empid,int status)
        {
            DataTable dtAllAccount = NewDao<IOPManageDao>().GetAllAccountData(bdate, edate, empid,status);
            if (dtAllAccount != null && dtAllAccount.Rows.Count > 0)
            {
                DataTable dtPay = NewDao<IOPManageDao>().GetAllAccountPayment(bdate, edate, empid);
                List<OP_AccountPatMentInfo> payInfoList = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<OP_AccountPatMentInfo>(dtPay);
                for (int i = 0; i < dtAllAccount.Rows.Count; i++)
                {
                    int accountid = Convert.ToInt32(dtAllAccount.Rows[i]["accountid"]);
                    List<OP_AccountPatMentInfo> payInfos = payInfoList.Where(p => p.AccountID == accountid).ToList().OrderBy(p => p.AccountPayInfoID).ToList();
                    foreach (OP_AccountPatMentInfo payInfo in payInfos)
                    {
                        if (dtAllAccount.Columns.Contains(payInfo.PayMentName))
                        {
                            dtAllAccount.Rows[i][payInfo.PayMentName] = payInfo.PayMentMoney;
                        }
                        else
                        {
                            DataColumn col = new DataColumn();
                            col.ColumnName = payInfo.PayMentName;
                            col.DataType = typeof(decimal);
                            dtAllAccount.Columns.Add(col);
                            dtAllAccount.Rows[i][payInfo.PayMentName] = payInfo.PayMentMoney;
                        }
                    }
                }
            }

            return dtAllAccount;
        }

        /// <summary>
        /// 获取所有未缴款记录
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="empid">人员ID</param>
        /// <returns>DataTable未缴款记录</returns>
        public DataTable QueryAllNotAccount(DateTime bdate, DateTime edate, int empid)
        {
            DataTable dtAllNotAccount = NewDao<IOPManageDao>().GetAllNotAccountData(bdate, edate, empid);

            if (dtAllNotAccount != null && dtAllNotAccount.Rows.Count > 0)
            {
                DataTable dtPay = NewDao<IOPManageDao>().GetAllNotAccountPayment(bdate, edate, empid);
                DataTable dtPayME = NewDao<IOPManageDao>().GetAccountDataMETotal(empid);
                List<OP_CostPayMentInfo> payInfoList = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<OP_CostPayMentInfo>(dtPay);
                for (int i = 0; i < dtAllNotAccount.Rows.Count; i++)
                {
                    int accountid = Convert.ToInt32(dtAllNotAccount.Rows[i]["accountid"]);
                    
                    List<OP_CostPayMentInfo> payInfos = payInfoList.Where(p => p.AccountID == accountid).ToList().OrderBy(p => p.ID).ToList();
                    foreach (OP_CostPayMentInfo payInfo in payInfos)
                    {
                        if (dtAllNotAccount.Columns.Contains(payInfo.PayMentName))
                        {
                            dtAllNotAccount.Rows[i][payInfo.PayMentName] = payInfo.PayMentMoney;
                        }
                        else
                        {
                            DataColumn col = new DataColumn();
                            col.ColumnName = payInfo.PayMentName;
                            col.DataType = typeof(decimal);
                            dtAllNotAccount.Columns.Add(col);
                            dtAllNotAccount.Rows[i][payInfo.PayMentName] = payInfo.PayMentMoney;
                        }
                    }
                }

                #region 会员交款信息补充
                foreach (DataRow dr in dtAllNotAccount.Rows)
                {
                    if (dr["FeeType"].ToString().Contains("会员"))
                    {
                        int accountid = Convert.ToInt32(dr["accountid"]);
                        DataRow[] drs = dtPayME.Select(" Account=" + accountid);
                        if (drs.Length > 0)
                        {
                            decimal dCash = Convert.ToDecimal(drs[0]["现金"]);
                            decimal dPos = Convert.ToDecimal(drs[0]["POS"]);
                            dr["TotalFee"] = dCash + dPos;                         
                            bool bCash = false;
                            bool bPos = false;
                            foreach (DataColumn dc in dtAllNotAccount.Columns)
                            {
                                if (dc.ColumnName.Contains("现金"))
                                {
                                    dr[dc.ColumnName] = dCash;
                                    bCash = true;
                                    continue;
                                }
                                else if (dc.ColumnName.Contains("POS"))
                                {
                                    dr[dc.ColumnName] = dPos;
                                    bPos = true;
                                    continue;
                                }
                            }

                            if (!bCash)
                            {
                                DataColumn col = new DataColumn();
                                col.ColumnName = "现金";
                                col.DataType = typeof(decimal);
                                dtAllNotAccount.Columns.Add(col);
                                dr["现金"] = dCash;
                            }

                            if (!bPos)
                            {
                                DataColumn col = new DataColumn();
                                col.ColumnName = "POS";
                                col.DataType = typeof(decimal);
                                dtAllNotAccount.Columns.Add(col);
                                dr["POS"] = dPos;
                            }
                        }
                    }
                }               
                #endregion
            }
         
            return dtAllNotAccount;
        }

        /// <summary>
        /// 获取充值明细
        /// </summary>
        /// <param name="iAcountId">缴款ID</param>
        /// <returns>DataTable充值明细</returns>
        public DataTable GetAccountDataME(int iAcountId)
        {
            DataTable dtPay = NewDao<IOPManageDao>().GetAccountDataME(iAcountId);
            return dtPay;
        }
    }
}
