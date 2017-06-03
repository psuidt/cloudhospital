using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.OPManage;
using HIS_OPManage.ObjectModel;
using HIS_PublicManage.ObjectModel;

namespace HIS_OPManage.WcfController
{
    /// <summary>
    /// 门诊缴款控制器类
    /// </summary>
    [WCFController]
    public class AccountController : WcfServerController
    {
        #region 个人缴款
        /// <summary>
        /// 数据获取
        /// </summary>
        /// <returns>返回缴款数据</returns>
        [WCFMethod]
        public ServiceResponseData AccountInit()
        {
            int empID = requestData.GetData<int>(0);//人员ID
            DateTime bdate = requestData.GetData<DateTime>(1);//开始日期
            DateTime edate = requestData.GetData<DateTime>(2);//结束日期

            //获取收费员列表
            DataTable dt = new DataTable();
            BasicDataManagement basicmanagement = NewObject<BasicDataManagement>();
            dt = basicmanagement.GetBasicData(HIS_PublicManage.ObjectModel.EmpDataSourceType.收费员, false);
            responseData.AddData(dt);           

            //获取未缴款记录
            List<OP_Account> notAccountList = NewObject<AccountProcess>().GetNotAccountByEmp(empID);
            responseData.AddData(notAccountList);

            //获取已经缴款记录
            List<OP_Account> historyAccountList = NewObject<AccountProcess>().GetHistoryAccountByEmp(empID, bdate, edate);
            responseData.AddData(historyAccountList);
            return responseData;
        }

        /// <summary>
        /// 获取缴款记录列表
        /// </summary>
        /// <returns>获取未缴款记录 已经缴款记录 </returns>
        [WCFMethod]
        public ServiceResponseData QueryAccountList()
        {
            int empID = requestData.GetData<int>(0);//人员ID
            DateTime bdate = requestData.GetData<DateTime>(1);//开始日期
            DateTime edate = requestData.GetData<DateTime>(2);//结束日期

            //获取未缴款记录
            List<OP_Account> notAccountList = NewObject<AccountProcess>().GetNotAccountByEmp(empID);
            responseData.AddData(notAccountList);

            //获取已经缴款记录
            List<OP_Account> historyAccountList = NewObject<AccountProcess>().GetHistoryAccountByEmp(empID, bdate, edate);
            responseData.AddData(historyAccountList);
            return responseData;
        }

        /// <summary>
        /// 获取缴款明细数据
        /// </summary>
        /// <returns>缴款明细数据</returns>
        [WCFMethod]
        public ServiceResponseData GetAccountData()
        {
            try
            {
                int accountId = requestData.GetData<int>(0);//结账ID
                int accountFlag = requestData.GetData<int>(1);//结账标志
                DataTable dtAccountInvoice = NewObject<AccountProcess>().GetAccountInvoiceData(accountId);//票据信息
                DataTable dtpayMentInfo = NewObject<AccountProcess>().GetAccountPatmentInfo(accountId, accountFlag);//支付方式信息
                DataTable dtItemData = NewObject<AccountProcess>().GetAccountItemData(accountId);//项目明细信息
                responseData.AddData(dtAccountInvoice);
                responseData.AddData(dtpayMentInfo);
                responseData.AddData(dtItemData);              
                decimal regFee = 0;
                decimal balanceFee = 0;
                DataTable dtRegBalance = NewObject<AccountProcess>().GetRegBalanceFee(accountId);
                for (int i = 0; i < dtRegBalance.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtRegBalance.Rows[i]["regFlag"]) == 0)
                    {
                        balanceFee  = Convert.ToDecimal(dtRegBalance.Rows[i]["totalFee"]);
                    }
                    else
                    {
                        regFee = Convert.ToDecimal(dtRegBalance.Rows[i]["totalFee"]);
                    }
                }

                responseData.AddData(regFee);
                responseData.AddData(balanceFee);
                OP_Account curAccount = NewObject<OP_Account>().getmodel(accountId) as OP_Account;
                responseData.AddData(curAccount);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 获取会员充值明细数据
        /// </summary>
        /// <returns>充值明细数据</returns>
        [WCFMethod]
        public ServiceResponseData GetAccountDataME()
        {
            try
            {
                int accountId = requestData.GetData<int>(0);//结账ID
                int accountFlag = requestData.GetData<int>(1);//结账标志
                DataTable dtAccountME = NewObject<AccountProcess>().GetAccountDataME(accountId);//票据信息               
                responseData.AddData(dtAccountME);
                OP_Account curAccount = NewObject<OP_Account>().getmodel(accountId) as OP_Account;
                responseData.AddData(curAccount);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 获取缴款票据明细
        /// </summary>
        /// <returns>缴款票据明细</returns>
        [WCFMethod]
        public ServiceResponseData GetInvoiceDetail()
        {
            int accountId = requestData.GetData<int>(0);//结账ID
            int invoiceID = requestData.GetData<int>(1);//挂号收费标识
            int invoicetype = requestData.GetData<int>(2);//0收费票据 1退费票据
            DataTable dtInvoiceDetail= NewObject<AccountProcess>().GetInvoiceDetail(invoiceID, invoicetype, accountId);            
            responseData.AddData(dtInvoiceDetail);
            return responseData;
        }

        /// <summary>
        /// 提交缴款
        /// </summary>
        /// <returns>返回缴款对象</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SubmitAccount()
        {
            try
            {
                OP_Account curAccount = requestData.GetData<OP_Account>(0);//结账ID
                OP_Account newAccount= NewObject<AccountProcess>().SubmitAccount(curAccount);
                responseData.AddData(newAccount);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
        #endregion

        #region 门诊缴款查询
        /// <summary>
        /// 获取收费员列表
        /// </summary>
        /// <returns>返回收费员列表</returns>
        [WCFMethod]
        public ServiceResponseData GetCashier()
        {         
            //获取收费员列表
            DataTable dt = new DataTable();
            BasicDataManagement basicmanagement = NewObject<BasicDataManagement>();
            dt = basicmanagement.GetBasicData(HIS_PublicManage.ObjectModel.EmpDataSourceType.收费员, false);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 查找所有已经缴款和未缴款记录
        /// </summary>
        /// <returns>已经缴款和未缴款记录</returns>
        [WCFMethod]
        public ServiceResponseData QueryAllAccount()
        {
            DateTime bdate  = requestData.GetData<DateTime>(0);
            DateTime edate = requestData.GetData<DateTime>(1);
            int empid = requestData.GetData<int>(2);
            int status = requestData.GetData<int>(3);
            DataTable dtAllAccountData = NewObject<AccountProcess>().QueryAllAccount(bdate, edate, empid,status);
            DataTable dtAllNotAccountData = NewObject<AccountProcess>().QueryAllNotAccount(bdate, edate, empid);
            responseData.AddData(dtAllAccountData);
            responseData.AddData(dtAllNotAccountData);
            return responseData;
        }

        /// <summary>
        /// 查询缴款明细
        /// </summary>
        /// <returns>缴款明细</returns>
        [WCFMethod]
        public ServiceResponseData QueryAccountInit()
        {
            int accountid = requestData.GetData<int>(0);
            OP_Account opAccount = NewObject<OP_Account>().getmodel(accountid) as OP_Account;  
            responseData.AddData(opAccount);
            return responseData;
        }

        /// <summary>
        /// 收款
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData ReciveAccount()
        {
            string accountid = requestData.GetData<string>(0);
            int operatoreid = requestData.GetData<int>(1);

            List<OP_Account> accountList = NewObject<OP_Account>().getlist<OP_Account>(" accountid in (" + accountid + ")");
            foreach (OP_Account opAccount in accountList)
            {
                if (opAccount.AccountFlag == 0)
                {
                    throw new Exception("还未缴款记录不能收款");
                }

                if (opAccount.ReceivFlag == 1)
                {
                    throw new Exception("缴款单号为【" + opAccount.ReceivBillNO + "】的缴款记录已经收款，不能再收款");
                }

                opAccount.ReceivFlag = 1;
                opAccount.ReceivEmpID = operatoreid;
                opAccount.ReceivDate = DateTime.Now;
                this.BindDb(opAccount);
                opAccount.save();
            }

            responseData.AddData(true);
            return responseData;
        }
        #endregion
    }
}
