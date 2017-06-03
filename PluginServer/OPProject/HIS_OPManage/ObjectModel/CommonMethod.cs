using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.BasicData;
using HIS_Entity.OPManage;
using HIS_PublicManage.ObjectModel;

namespace HIS_OPManage.ObjectModel
{
    /// <summary>
    /// 公共方法类
    /// </summary>
    [Serializable]
    public class CommonMethod : AbstractObjectModel
    {
        /// <summary>
        /// 获取有效的挂号类别
        /// </summary>
        /// <returns>DataTable挂号类别</returns>
        public DataTable GetRegType()
        {
            DataTable regtypes = NewObject<OP_RegType>().gettable(" flag=1");
            return regtypes;
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="configCode">参数名称</param>
        /// <returns>参数值</returns>
        public string GetOpConfig(string configCode)
        {
           return  NewObject<SysConfigManagement>().GetSystemConfigValue(configCode);          
        }

        /// <summary>
        /// 获取当前结账ID
        /// </summary>
        /// <param name="empid">操作员Id</param>
        /// <param name="iAccountType">0收费1会员办卡</param>
        /// <returns>当前结账ID</returns>
        public int GetAccountId(int empid,int iAccountType)
        {
            int accountid = 0;
            List<OP_Account> account = NewObject<OP_Account>().getlist<OP_Account>(" AccountEmpID=" + empid + " and AccountType="+ iAccountType + " and accountFlag=0");
            if (account != null && account.Count > 0)
            {
                accountid = account[0].AccountID;
            }
            else
            {
                OP_Account newaccount = new OP_Account();
                newaccount.AccountEmpID = empid;
                newaccount.AccountFlag = 0;
                newaccount.TotalFee = 0;
                newaccount.CashFee = 0;
                newaccount.PosFee = 0;
                newaccount.PromFee = 0;
                newaccount.InvoiceCount = 0;
                newaccount.RefundInvoiceCount = 0;
                newaccount.RoundingFee = 0;
                newaccount.AccountType = 0;
                List<OP_Account> accountList = NewObject<OP_Account>().getlist<OP_Account>(" AccountEmpID=" + empid + " and AccountType=" + iAccountType + " and accountFlag=1");
                if (accountList == null || accountList.Count == 0)
                {
                    newaccount.LastDate = DateTime.Now;
                }
                else
                {
                    accountList = accountList.OrderByDescending(p => p.AccountID).ToList(); //获取上次缴款日期
                    newaccount.LastDate = accountList[0].AccountDate;
                }

                this.BindDb(newaccount);
                newaccount.save();
                accountid = newaccount.AccountID;
            }

            return accountid;
        }

        /// <summary>
        /// 每笔记录汇总插入结账表
        /// </summary>
        /// <param name="costHead">结算对象</param>
        /// <param name="curAccountId">当前缴款ID</param>
        /// <param name="invoiceCount">收费票据张数</param>
        /// <param name="refundInvoiceCount">退费票据张数</param>
        public void AddAccoutFee(OP_CostHead costHead, int curAccountId, int invoiceCount, int refundInvoiceCount)
        {
            OP_Account opaccount = NewObject<OP_Account>().getmodel(curAccountId) as OP_Account;
            opaccount.TotalFee += costHead.TotalFee;
            opaccount.CashFee += costHead.CashFee;
            opaccount.PosFee += costHead.PosFee;
            opaccount.PromFee += costHead.PromFee;
            opaccount.RoundingFee += costHead.RoundingFee;
            opaccount.InvoiceCount += invoiceCount;
            opaccount.RefundInvoiceCount += refundInvoiceCount;
            this.BindDb(opaccount);
            opaccount.save();
        }

        /// <summary>
        /// 获取支付方式Code对应的支付对象
        /// </summary>
        /// <param name="payMentCode">支付Code</param>
        /// <returns>返回支付对象</returns>
        public Basic_Payment GetPayMentByCode(string payMentCode)
        {
            List<Basic_Payment> paymentList = NewObject<Basic_Payment>().getlist<Basic_Payment>(" PayCode='" + payMentCode + "'");
            return paymentList[0];
        }

        /// <summary>
        /// 根据病人类型ID判断是否属于医保病人
        /// </summary>
        /// <param name="pattypeid">病人类型ID</param>
        /// <returns>bool true是医保病人 false不是医保病人</returns>
        public bool IsMedicarePat(int pattypeid)
        {
            bool result = false;

            //判断所选的病人类型是否是医保病人       
            string pattypes =GetOpConfig(OpConfigConstant.IsMedicarePat);//获取医保对应的病人类型ID，多个病人类弄中间用,隔开  
            string[] types = pattypes.Split(',');
            foreach (string pattype in types)
            {
                if (pattype == pattypeid.ToString())
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 获取操作员当前可用发票张数和当前票据号
        /// </summary>
        ///  <param name="invoiceType">票据类型</param>
        /// <param name="operatorid">操作员Id</param>
        /// <param name="curInvoiceNO">当前票据号</param>
        /// <returns>int票据张数</returns>
        public int GetInvoiceInfo(InvoiceType invoiceType, int operatorid, out string curInvoiceNO)
        {
            InvoiceManagement invoicemanagement = NewObject<InvoiceManagement>();
            int invoiceCount = invoicemanagement.GetInvoiceNumberOfCanUse(invoiceType, operatorid);
            string pefChar = string.Empty;
            curInvoiceNO = invoicemanagement.GetInvoiceCurNo(invoiceType, operatorid, out pefChar);
            curInvoiceNO = pefChar + curInvoiceNO;
            return invoiceCount;
        }

        /// <summary>
        /// 获取当前票据号
        /// </summary>
        /// <param name="invoiceType">票据类型</param>
        /// <param name="operatorid">操作员ID</param>
        /// <returns>string当前票据号</returns>
        public string GetCurInvoiceNO(InvoiceType invoiceType,int operatorid)
        {
            InvoiceManagement invoicemanagement = NewObject<InvoiceManagement>();
            string pefChar = string.Empty;
            string curInvoiceNO = invoicemanagement.GetInvoiceCurNo(invoiceType, operatorid, out pefChar);
            curInvoiceNO = pefChar + curInvoiceNO;
            return curInvoiceNO;
        }

        /// <summary>
        /// 获取当前可用票据对象
        /// </summary>
        /// <param name="invoiceType">票据类型</param>
        /// <param name="operatorid">操作员ID</param>
        /// <returns>当前票据对象</returns>
        public Basic_Invoice GetCurInvoice(InvoiceType invoiceType, int operatorid)
        {
            InvoiceManagement invoicemanagement = NewObject<InvoiceManagement>();
            return invoicemanagement.GetInvoiceCurNo(invoiceType, operatorid);
        }
    }
}