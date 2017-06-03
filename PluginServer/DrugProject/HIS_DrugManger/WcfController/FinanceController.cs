using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_DrugManger.Dao;
using HIS_DrugManger.ObjectModel.BaseData;
using HIS_DrugManger.ObjectModel.Bill;
using HIS_Entity.DrugManage;

namespace HIS_DrugManger.WcfController
{
    /// <summary>
    /// 财务管理控制器（月结、付款）
    /// </summary>
    [WCFController]
    public class FinanceController : WcfServerController
    {
        /// <summary>
        /// 获取药剂科室列表用于选择
        /// </summary>
        /// <returns>返回值</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugDeptList()
        {
            int deptType = requestData.GetData<int>(0);
            DataTable dt = NewObject<DrugDeptMgr>().GetDrugDeptList(deptType);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取供应商列表用于绑定
        /// </summary>
        /// <returns>供应商列表</returns>
        [WCFMethod]
        public ServiceResponseData GetSupplyForShowCard()
        {
            DataTable dt = NewObject<DrugSupplyMgr>().GetSupplyForShowCard();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 加载入库单表头
        /// </summary>
        /// <returns>入库单表头信息</returns>
        [WCFMethod]
        public ServiceResponseData LoadBillHead()
        {
            var opType = requestData.GetData<string>(0);
            var queryCondition = requestData.GetData<Dictionary<string, string>>(1);
            IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(opType);
            DataTable dtRtn = iProcess.LoadHead(queryCondition);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 加载入库单明细
        /// </summary>
        /// <returns>入库单明细信息</returns>
        [WCFMethod]
        public ServiceResponseData LoadBillDetails()
        {
            var opType = requestData.GetData<string>(0);
            var queryCondition = requestData.GetData<Dictionary<string, string>>(1);
            IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(opType);
            DataTable dtRtn = iProcess.LoadDetails(queryCondition);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 付款操作
        /// </summary>
        /// <returns>返回处理结果</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData BillPay()
        {
            var payrecord = requestData.GetData<DG_PayRecord>(0);
            var inHeadID = requestData.GetData<string>(1);
            this.BindDb(payrecord);
            int result = payrecord.save();
            if (result > 0)
            {
                result = NewDao<IDWDao>().UpdateStoreHead(inHeadID, payrecord.InvoiceNO, payrecord.PayTime, payrecord.PayRecordID, 1);
            }

            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 取消付款操作
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData CancelBillPay()
        {
            var payRecordID = requestData.GetData<string>(0);
            int result = NewDao<IDWDao>().UpdatePayRecord(payRecordID);
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 加载付款记录信息
        /// </summary>
        /// <returns>付款记录信息</returns>
        [WCFMethod]
        public ServiceResponseData LoadPayRecord()
        {
            var queryCondition = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dtRtn = NewDao<IDWDao>().LoadPayRecord(queryCondition);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 获取打印付款记录信息
        /// </summary>
        /// <returns>打印付款记录信息</returns>
        [WCFMethod]
        public ServiceResponseData PrintPayRecord()
        {
            var queryCondition = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dtRtn = NewDao<IDWDao>().PrintPayRecord(queryCondition);
            responseData.AddData(dtRtn);
            return responseData;
        }
    }
}
