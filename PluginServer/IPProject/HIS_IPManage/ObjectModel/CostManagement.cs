using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.Common;
using HIS_Entity.BasicData;
using HIS_Entity.IPManage;
using HIS_Entity.MemberManage;
using HIS_IPManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPManage.ObjectModel
{
    /// <summary>
    /// 结算处理对象
    /// </summary>
    public class CostManagement : AbstractObjectModel
    {
        /// <summary>
        /// 获取操作员当前可用发票张数和当前票据号
        /// </summary>
        /// <param name="invoiceType">票据类型</param>
        /// <param name="empID">操作员Id</param>
        /// <param name="curInvoiceNO">当前票据号</param>
        /// <returns>发票张数</returns>
        public int GetInvoiceInfo(InvoiceType invoiceType, int empID, out string curInvoiceNO)
        {
            InvoiceManagement invoicemanagement = NewObject<InvoiceManagement>();
            int invoiceCount = invoicemanagement.GetInvoiceNumberOfCanUse(invoiceType, empID);
            string pefChar = string.Empty;
            curInvoiceNO = invoicemanagement.GetInvoiceCurNo(invoiceType, empID, out pefChar);
            curInvoiceNO = pefChar + curInvoiceNO;
            return invoiceCount;
        }

        /// <summary>
        /// 住院结算
        /// </summary>
        /// <param name="patEnterHDate">病人入院日期</param>
        /// <param name="costHead">结算头信息</param>
        /// <param name="costPayList">支付方式列表</param>
        /// <param name="resDiscountInfo">优惠信息数据</param>
        /// <param name="workID">机构ID</param>
        /// <returns>错误消息</returns>
        public string DischargeSettlement(DateTime patEnterHDate, IP_CostHead costHead, List<IP_CostPayment> costPayList, DiscountInfo resDiscountInfo, int workID)
        {
            // 检查最新费用是否发生过变化
            // 获取最新费用总额
            DataTable tempDt = NewDao<IIPManageDao>().GetPatDepositFee(costHead.PatListID);
            if (tempDt != null)
            {
                // 最新住院费用总额
                decimal tempDepositFee = Convert.ToDecimal(tempDt.Rows[0][0]);
                // 最新预交金总额
                decimal tempTotalFee = Convert.ToDecimal(tempDt.Rows[1][0]);
                if (costHead.TotalFee != tempTotalFee || costHead.DeptositFee != tempDepositFee)
                {
                    return "当前病人的费用数据已发生改变，请刷新费用列表后重新结算！";
                }
            }
            else
            {
                return "当前病人的费用已被结算！";
            }
            // 取得病人上一次结算时间
            DataTable costDateDt = NewDao<IIPManageDao>().GetPatLastCostDate(costHead.PatListID);
            // 如果病人存在结算记录，结算起始日期为上一次结算时间
            if (costDateDt != null && costDateDt.Rows.Count > 0)
            {
                costHead.CostBeginDate = Convert.ToDateTime(costDateDt.Rows[0][0]);
            }
            else
            {
                // 病人没有结算记录，结算起始时间为入院时间
                costHead.CostBeginDate = patEnterHDate;
            }

            costHead.CostEndDate = DateTime.Now;
            // 取得票据ID
            InvoiceManagement invoicemanagement = NewObject<InvoiceManagement>();
            Basic_Invoice invoice = invoicemanagement.GetInvoiceCurNo(InvoiceType.住院结算, costHead.CostEmpID);
            costHead.InvoiceID = invoice.ID; // 票据ID
            // 写入结算头表数据
            this.BindDb(costHead);
            costHead.save();

            string perfChar = string.Empty;
            // 使用票据号
            NewObject<InvoiceManagement>().GetInvoiceCurNOAndUse(InvoiceType.住院结算, costHead.CostEmpID, out perfChar);
            // 写入结算明细表数据
            bool result = NewDao<IIPManageDao>().SaveCostDetail(costHead.CostHeadID, costHead.InvoiceID, costHead.InvoiceNO, workID, costHead.PatListID);

            // 写入支付记录表数据
            if (costPayList.Count > 0)
            {
                foreach (IP_CostPayment costpay in costPayList)
                {
                    costpay.CostHeadID = costHead.CostHeadID;
                    // 取得支付方式名
                    Basic_Payment basePayment = NewObject<Basic_Payment>().getmodel(costpay.PaymentID) as Basic_Payment;
                    costpay.PayName = basePayment.PayName;
                    // 保存支付记录表数据
                    this.BindDb(costpay);
                    costpay.save();
                }
            }

            // 修改预交金表的结算ID
            NewDao<IIPManageDao>().CostDeposit(costHead.PatListID, costHead.CostHeadID, costHead.CostType, false);

            // 修改费用明细表数据的结算ID
            NewDao<IIPManageDao>().CostFeeItemRecord(costHead.PatListID, costHead.CostHeadID, costHead.CostType, false);

            // 出院结算和欠费结算的场合，修改病人状态
            if (costHead.CostType == 2 || costHead.CostType == 3)
            {
                NewDao<IIPManageDao>().UpdatePatStatus(costHead.PatListID, 4);
            }

            // 保存积分数据
            NewObject<MemberManagement>().SaveAddScoreList(costHead.MemberAccountID, costHead.TotalFee, 3, costHead.CostHeadID.ToString(), costHead.CostEmpID);
            // 写入优惠明细数据
            // 将结算主表ID写入优惠明细数据
            if (costHead.PromFee > 0)
            {
                // 生效优惠数据
                NewObject<PromotionManagement>().UpdateDiscountInfo(costHead.CostHeadID, resDiscountInfo.AccID);
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取当前票据号
        /// </summary>
        /// <param name="invoiceType">票据类型</param>
        /// <param name="operatorid">票据ID</param>
        /// <returns>票据号</returns>
        public string GetCurInvoiceNO(InvoiceType invoiceType, int operatorid)
        {
            InvoiceManagement invoicemanagement = NewObject<InvoiceManagement>();
            string pefChar = string.Empty;
            string curInvoiceNO = invoicemanagement.GetInvoiceCurNo(invoiceType, operatorid, out pefChar);
            curInvoiceNO = pefChar + curInvoiceNO;
            return curInvoiceNO;
        }

        /// <summary>
        /// 取消结算
        /// </summary>
        /// <param name="patListID">病人ID</param>
        /// <param name="costHeadID">结算ID</param>
        /// <param name="costType">结算类型</param>
        /// <returns>true：取消成功</returns>
        public bool CancelSettlement(int patListID, int costHeadID, int costType)
        {
            int tempCostHeadID = 0;
            switch (costType)
            {
                case 1:
                    // 中途结算
                    // 取得病人最后一次结算的结算ID
                    DataTable costDt = NewDao<IIPManageDao>().GetLastHoleCostHeadID(patListID);
                    if (costDt.Rows.Count > 0)
                    {
                        tempCostHeadID = Convert.ToInt32(costDt.Rows[0][0]);
                        // 选中的结算记录不是最后一次结算记录
                        if (costHeadID != tempCostHeadID)
                        {
                            return false;
                        }
                    }

                    break;
                case 2:
                case 3:
                    // 出院结算、中途结算
                    // 将病人状态修改会出院未结算(3)
                    NewDao<IIPManageDao>().UpdatePatStatus(patListID, 3);
                    break;
            }

            // 修改预交金表的结算ID
            NewDao<IIPManageDao>().CostDeposit(patListID, costHeadID, 0, true);

            // 修改费用明细表数据的结算ID
            NewDao<IIPManageDao>().CostFeeItemRecord(patListID, costHeadID, 0, true);

            // 根据结算ID取得结算记录
            IP_CostHead costHead = NewObject<IP_CostHead>();
            DataTable costHeadDt = NewDao<IIPManageDao>().GetCostHeadByHeadID(costHeadID);
            if (costHeadDt.Rows.Count > 0)
            {
                costHead = ConvertExtend.ToObject<IP_CostHead>(costHeadDt, 0);
                costHead.Status = 1;
                this.BindDb(costHead);
                costHead.save();
                // 产生一条红冲的负记录
                costHead.CostHeadID = 0;
                costHead.TotalFee = 0 - costHead.TotalFee;
                costHead.DeptositFee = 0 - costHead.DeptositFee;
                costHead.BalanceFee = 0 - costHead.BalanceFee;
                costHead.CashFee = 0 - costHead.CashFee;
                costHead.PosFee = 0 - costHead.PosFee;
                costHead.PromFee = 0 - costHead.PromFee;
                costHead.RoundingFee = 0 - costHead.RoundingFee;
                costHead.Status = 2;
                costHead.OldCostHeadID = costHeadID;
                costHead.AccountID = 0;
                this.BindDb(costHead);
                costHead.save();
            }
            // 往支付记录表中插入负记录
            NewDao<IIPManageDao>().CancelCostUpdCostPayment(costHeadID, costHead.CostHeadID);

            // 往住院结算明细费用汇总表中插入负记录
            NewDao<IIPManageDao>().CancelCostUpdCostDetail(costHeadID, costHead.CostHeadID);
            return true;
        }
    }
}
