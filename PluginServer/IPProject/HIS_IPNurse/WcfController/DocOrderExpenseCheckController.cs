using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_IPNurse.Dao;
using HIS_IPNurse.ObjectModel;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPNurse.WcfController
{
    /// <summary>
    /// 医嘱费用核对控制器
    /// </summary>
    [WCFController]
    class DocOrderExpenseCheckController : WcfServerController
    {
        /// <summary>
        /// 获取病人状态
        /// </summary>
        /// <returns>病人信息</returns>
        [WCFMethod]
        public ServiceResponseData GetPatientStatus()
        {
            int patlistID = requestData.GetData<int>(0);
            DataTable patDt = NewDao<IDocOrderExpenseCheckDao>().GetPatientStatus(patlistID);
            responseData.AddData(patDt);
            return responseData;
        }

        /// <summary>
        /// 获取医嘱病人列表
        /// </summary>
        /// <returns>医嘱病人列表</returns>
        [WCFMethod]
        public ServiceResponseData GetPatList()
        {
            int deptId = requestData.GetData<int>(0);
            int status = requestData.GetData<int>(1);
            DataTable patListDt = NewDao<IDocOrderExpenseCheckDao>().GetPatientList(deptId, status/*, StartTime, EndTime*/);
            responseData.AddData(patListDt);
            return responseData;
        }

        /// <summary>
        /// 获取病人费用明细汇总
        /// </summary>
        /// <returns>病人费用明细汇总</returns>
        [WCFMethod]
        public ServiceResponseData GetPatFeeInfo()
        {
            int patListID = requestData.GetData<int>(0);
            // 预交金总额
            DataTable sumDepositDt = NewDao<IDocOrderExpenseCheckDao>().GetPatSumPay(patListID);
            // 记账金额
            DataTable patSumFeeDt = NewDao<IDocOrderExpenseCheckDao>().GetPatLongOrderSumPay(patListID);
            responseData.AddData(sumDepositDt);
            responseData.AddData(patSumFeeDt);
            return responseData;
        }

        /// <summary>
        /// 获取病人医嘱列表
        /// </summary>
        /// <returns>病人医嘱列表</returns>
        [WCFMethod]
        public ServiceResponseData GetPatOrderList()
        {
            int patListId = requestData.GetData<int>(0);
            int orderType = requestData.GetData<int>(1);
            DataTable patOrderListDt = NewDao<IDocOrderExpenseCheckDao>().GetPatOrderList(patListId, orderType);
            responseData.AddData(patOrderListDt);
            return responseData;
        }

        /// <summary>
        /// 获取医嘱关联费用列表
        /// </summary>
        /// <returns>医嘱关联费用列表</returns>
        [WCFMethod]
        public ServiceResponseData GetOrderFeeList()
        {
            int patListID = requestData.GetData<int>(0);
            int orderID = requestData.GetData<int>(1);
            int groupID = requestData.GetData<int>(2);
            //获取医嘱关联记账费用列表
            DataTable orderFeeList = NewDao<IDocOrderExpenseCheckDao>().GetOrderFeeList(patListID, orderID, groupID);
            // 获取医嘱关联记账费用按项目汇总列表
            DataTable orderSumFeeList = NewDao<IDocOrderExpenseCheckDao>().GetOrderSumFeeList(patListID, orderID, groupID);
            responseData.AddData(orderFeeList);
            responseData.AddData(orderSumFeeList);
            return responseData;
        }

        /// <summary>
        /// 费用冲账
        /// </summary>
        /// <returns>true冲账成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData OrderStrikeABalance()
        {
            DataTable tempDt = requestData.GetData<DataTable>(0);
            int orderID = requestData.GetData<int>(1);
            bool isUpdateExa = requestData.GetData<bool>(2);
            List<string> msgList = new List<string>();
            bool result = NewObject<DocOrderExpenseCheck>().StrikeABalance(tempDt, msgList, orderID, isUpdateExa);
            responseData.AddData(result);
            responseData.AddData(msgList);
            return responseData;
        }

        /// <summary>
        /// 取消费用冲账
        /// </summary>
        /// <returns>true取消成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData CancelOrderStrikeABalance()
        {
            DataTable tempDt = requestData.GetData<DataTable>(0);
            int orderID = requestData.GetData<int>(1);
            bool isUpdateExa = requestData.GetData<bool>(2);
            List<string> msgList = new List<string>();
            bool result = NewObject<DocOrderExpenseCheck>().CancelStrikeABalance(tempDt, msgList, orderID, isUpdateExa);
            responseData.AddData(result);
            responseData.AddData(msgList);
            return responseData;
        }

        /// <summary>
        /// 查询病人所有已记账的费用列表
        /// </summary>
        /// <returns>病人所有已记账的费用列表</returns>
        [WCFMethod]
        public ServiceResponseData GetCostList()
        {
            int patListID = requestData.GetData<int>(0);
            string orderType = requestData.GetData<string>(1);
            DataTable feeList = NewDao<IDocOrderExpenseCheckDao>().GetCostList(patListID, orderType);
            DataTable sumFeeList = NewDao<IDocOrderExpenseCheckDao>().GetSumCostList(patListID, orderType);
            responseData.AddData(feeList);
            responseData.AddData(sumFeeList);
            return responseData;
        }

        /// <summary>
        /// 删除选中的费用记录
        /// </summary>
        /// <returns>true删除成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData DelFeeLongOrderData()
        {
            DataTable delFeeItemDt = requestData.GetData<DataTable>(0);
            string result = NewObject<DocOrderExpenseCheck>().DelFeeLongOrderData(delFeeItemDt);
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 取得医嘱业务相关数据
        /// </summary>
        /// <returns>医嘱业务相关数据</returns>
        [WCFMethod]
        public ServiceResponseData GetSimpleFeeItemDataDt()
        {
            DataTable dt = NewObject<FeeItemDataSource>().GetSimpleFeeItemDataDt(FeeBusinessType.医嘱业务);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 保存住院费用生成数据
        /// </summary>
        /// <returns>true保存成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveLongOrderData()
        {
            DataTable longFeeOrderDt = requestData.GetData<DataTable>(0);
            int markEmpID = requestData.GetData<int>(1);
            string result = NewObject<DocOrderExpenseCheck>().SaveLongOrderData(longFeeOrderDt, markEmpID);
            responseData.AddData(result);
            responseData.AddData(longFeeOrderDt);
            return responseData;
        }

        /// <summary>
        /// 账单记账
        /// </summary>
        /// <returns>记账结果（错误消息等）</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData FeeItemAccounting()
        {
            DataTable feeItemAccDt = requestData.GetData<DataTable>(0);
            int empID = requestData.GetData<int>(1);
            DateTime endTime = requestData.GetData<DateTime>(2);
            bool isBedFee = requestData.GetData<bool>(3);
            bool isLongFee = requestData.GetData<bool>(4);
            int patListID = requestData.GetData<int>(5);
            List<string> msgList = new List<string>();
            bool result = NewObject<DocOrderExpenseCheck>().FeeItemAccounting(feeItemAccDt, empID, endTime, isBedFee, isLongFee, msgList, patListID);
            responseData.AddData(result);
            responseData.AddData(msgList);
            return responseData;
        }

        /// <summary>
        /// 停用账单
        /// </summary>
        /// <returns>true停用成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData StopFeeLongOrderData()
        {
            DataTable stopFeeDt = requestData.GetData<DataTable>(0);
            string result = NewObject<DocOrderExpenseCheck>().StopFeeLongOrderData(stopFeeDt);
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 查询病人账单列表
        /// </summary>
        /// <returns>病人账单列表</returns>
        [WCFMethod]
        public ServiceResponseData GetPatFeeItemGenerate()
        {
            int patListID = requestData.GetData<int>(0);
            int orderType = requestData.GetData<int>(1);
            DataTable dt = NewDao<IDocOrderExpenseCheckDao>().GetPatFeeItemGenerate(patListID, orderType);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 检查病人状态
        /// </summary>
        /// <returns>病人状态</returns>
        [WCFMethod]
        public ServiceResponseData CheckPatientStatus()
        {
            int patListId = requestData.GetData<int>(0);
            DataTable patDt = NewDao<IDocOrderExpenseCheckDao>().CheckPatientStatus(patListId);
            if (patDt.Rows.Count > 0)
            {
                responseData.AddData(Convert.ToInt32(patDt.Rows[0]["Status"]));
            }
            else
            {
                responseData.AddData(-1);
            }

            return responseData;
        }
    }
}
