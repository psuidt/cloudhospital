using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.IPManage;
using HIS_Entity.MemberManage;
using HIS_IPManage.Dao;
using HIS_IPManage.ObjectModel;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPManage.WcfController
{
    /// <summary>
    /// 结算控制器
    /// </summary>
    [WCFController]
    public class DischargeSettlementController : WcfServerController
    {
        /// <summary>
        /// 获取当前票据号以及可用票据张数
        /// </summary>
        /// <returns>当前票据号以及可用票据张数</returns>
        [WCFMethod]
        public ServiceResponseData GetInvoiceNO()
        {
            int empID = requestData.GetData<int>(0);
            string curInvoiceNO = string.Empty;
            int invoiceCount = NewObject<CostManagement>().GetInvoiceInfo(InvoiceType.住院结算, empID, out curInvoiceNO);
            responseData.AddData(invoiceCount);// 可用票据张数
            responseData.AddData(curInvoiceNO);// 当前可用票据号
            return responseData;
        }

        /// <summary>
        /// 按费用类型统计费用
        /// </summary>
        /// <returns>费用统计结果</returns>
        [WCFMethod]
        public ServiceResponseData StatisticsFeeByFeeType()
        {
            int patListId = requestData.GetData<int>(0);
            decimal serialNumber = requestData.GetData<decimal>(1);
            // 分类合计
            DataTable feeDt = NewDao<IIPManageDao>().StatisticsFeeByFeeType(patListId, 0);
            // 未结算预交金明细
            DataTable paymentDt = NewDao<IIPManageDao>().GetPaymentList(patListId, serialNumber, true);
            responseData.AddData(feeDt);
            responseData.AddData(paymentDt);
            return responseData;
        }

        /// <summary>
        /// 获取病人最新预交金总额以及未结算费用总额
        /// </summary>
        /// <returns>病人最新预交金总额以及未结算费用总额</returns>
        [WCFMethod]
        public ServiceResponseData GetPatDepositFee()
        {
            int patListID = requestData.GetData<int>(0);
            DataTable tempDt = NewDao<IIPManageDao>().GetPatDepositFee(patListID);
            responseData.AddData(tempDt);
            return responseData;
        }

        /// <summary>
        /// 结算--优惠金额计算
        /// </summary>
        /// <returns>优惠信息</returns>
        [WCFMethod]
        public ServiceResponseData PromFeeCaculate()
        {
            int patTypeID = requestData.GetData<int>(0); // 病人类型ID
            int memberAccountID = requestData.GetData<int>(1); // 会员账号ID
            decimal totalFee = requestData.GetData<decimal>(2); // 费用总金额
            int empId = requestData.GetData<int>(3); // 操作员ID
            int patListID = requestData.GetData<int>(4); // 病人登记ID
            DataTable largeProjectDt = NewDao<IIPManageDao>().GetFeeItemRecordGroupByStatID(patListID);
            DataTable feeItemDt = NewDao<IIPManageDao>().GetFeeItemRecordDetails(patListID);
            DiscountInfo discountinfo = new DiscountInfo();
            discountinfo.AccountID = memberAccountID;
            discountinfo.CostType = patTypeID;
            discountinfo.PatientType = 2;
            discountinfo.Amount = totalFee;
            discountinfo.OperateID = empId;
            discountinfo.SettlementNO = "0";
            discountinfo.DtDetail = feeItemDt;
            discountinfo.DtClass = largeProjectDt;
            discountinfo.IsSave = true;
            // 生成时间戳
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long aaa = Convert.ToInt64(ts.TotalSeconds);
            int te = Convert.ToInt32(ts.TotalSeconds);
            discountinfo.AccID = te;
            DiscountInfo resDiscountInfo = NewObject<PromotionManagement>().CalculationPromotion(discountinfo);
            responseData.AddData(resDiscountInfo);
            return responseData;
        }

        /// <summary>
        /// 住院结算
        /// </summary>
        /// <returns>true：结算成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData DischargeSettlement()
        {
            IP_CostHead costHead = requestData.GetData<IP_CostHead>(0);
            List<IP_CostPayment> costPayList = requestData.GetData<List<IP_CostPayment>>(1);
            DiscountInfo resDiscountInfo = requestData.GetData<DiscountInfo>(2);
            int workID = requestData.GetData<int>(3);
            DateTime patEnterHDate = requestData.GetData<DateTime>(4);
            string result = NewObject<CostManagement>().DischargeSettlement(patEnterHDate, costHead, costPayList, resDiscountInfo, workID);
            responseData.AddData(result);
            responseData.AddData(costPayList);
            return responseData;
        }

        /// <summary>
        /// 调整发票号
        /// </summary>
        /// <returns>新发票号</returns>
        [WCFMethod]
        public ServiceResponseData AdjustInvoice()
        {
            try
            {
                string perfChar = requestData.GetData<string>(0);
                string invoiceNO = requestData.GetData<string>(1);
                int operatorid = requestData.GetData<int>(2);
                InvoiceManagement invoiceManager = NewObject<InvoiceManagement>();
                invoiceManager.AdjustInvoiceNo(InvoiceType.住院结算, operatorid, perfChar, invoiceNO);
                string curInvoiceNo = NewObject<CostManagement>().GetCurInvoiceNO(InvoiceType.住院结算, operatorid);
                responseData.AddData(curInvoiceNo);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
