using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.IPManage;
using HIS_IPManage.Dao;
using HIS_IPManage.ObjectModel;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPManage.WcfController
{
    /// <summary>
    /// 医嘱计费控制器
    /// </summary>
    [WCFController]
    public class DoctorsOrderSccountingController : WcfServerController
    {
        /// <summary>
        /// 账单管理--查询在床病人列表
        /// </summary>
        /// <returns>病人列表</returns>
        [WCFMethod]
        public ServiceResponseData GetPatientList()
        {
            int deptID = requestData.GetData<int>(0);
            string param = requestData.GetData<string>(1);
            DataTable dt = NewDao<IIPManageDao>().GetPatientList(deptID, param);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取病人累计缴费金额
        /// </summary>
        /// <returns>累计缴费金额</returns>
        [WCFMethod]
        public ServiceResponseData GetPatSumPay()
        {
            int patListID = requestData.GetData<int>(0);
            DataTable dt = NewDao<IIPManageDao>().GetPatSumPay(patListID);
            DataTable feeDt = NewDao<IIPManageDao>().GetPatLongOrderSumPay(patListID);
            responseData.AddData(Convert.ToDecimal(dt.Rows[0][0].ToString()));
            responseData.AddData(Convert.ToDecimal(feeDt.Rows[0][0].ToString()));
            responseData.AddData(Convert.ToDecimal(feeDt.Rows[1][0].ToString()));
            responseData.AddData(Convert.ToDecimal(feeDt.Rows[2][0].ToString()));
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
        /// 查询病人账单列表
        /// </summary>
        /// <returns>账单列表</returns>
        [WCFMethod]
        public ServiceResponseData GetPatFeeItemGenerate()
        {
            int patListID = requestData.GetData<int>(0);
            int orderType = requestData.GetData<int>(1);
            DataTable dt = NewDao<IIPManageDao>().GetPatFeeItemGenerate(patListID, orderType);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 保存住院费用生成数据
        /// </summary>
        /// <returns>true：保存成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveLongOrderData()
        {
            DataTable longFeeOrderDt = requestData.GetData<DataTable>(0);
            int markEmpID = requestData.GetData<int>(1);
            string result = NewObject<FeeItemManagement>().SaveLongOrderData(longFeeOrderDt, markEmpID);
            responseData.AddData(result);
            responseData.AddData(longFeeOrderDt);
            return responseData;
        }

        /// <summary>
        /// 删除选中的费用记录
        /// </summary>
        /// <returns>true：删除成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData DelFeeLongOrderData()
        {
            DataTable delFeeItemDt = requestData.GetData<DataTable>(0);
            string result = NewObject<FeeItemManagement>().DelFeeLongOrderData(delFeeItemDt);
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 取得所有未停用的账单模板
        /// </summary>
        /// <returns>未停用的账单模板</returns>
        [WCFMethod]
        public ServiceResponseData GetFeeItemTempList()
        {
            int wardID = requestData.GetData<int>(0);
            int deptId = requestData.GetData<int>(1);
            int empId = requestData.GetData<int>(2);
            List<IP_FeeItemTemplateHead> feeItemList = NewDao<IIPManageDao>().GetFeeItemTempList(wardID, deptId, empId);
            responseData.AddData(feeItemList);
            return responseData;
        }

        /// <summary>
        /// 根据模板ID查询模板对应的账单明细数据
        /// </summary>
        /// <returns>账单明细数据</returns>
        [WCFMethod]
        public ServiceResponseData GetFeeItemTempDetails()
        {
            int tempHeadID = requestData.GetData<int>(0);
            DataTable feeDetailsDt = NewDao<IIPManageDao>().GetFeeItemTempDetails(tempHeadID);
            responseData.AddData(feeDetailsDt);
            return responseData;
        }

        /// <summary>
        /// 账单记账
        /// </summary>
        /// <returns>true：记账成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData FeeItemAccounting()
        {
            DataTable feeItemAccDt = requestData.GetData<DataTable>(0);
            int empID = requestData.GetData<int>(1);
            DateTime startTime = requestData.GetData<DateTime>(2);
            DateTime endTime = requestData.GetData<DateTime>(3);
            bool isBedFee = requestData.GetData<bool>(4);
            bool isLongFee = requestData.GetData<bool>(5);
            List<string> msgList = new List<string>();
            bool result = NewObject<FeeItemManagement>().FeeItemAccounting(feeItemAccDt, empID, startTime, endTime, isBedFee, isLongFee, msgList);
            responseData.AddData(result);
            responseData.AddData(msgList);
            return responseData;
        }

        /// <summary>
        /// 停用账单
        /// </summary>
        /// <returns>true：账单停用成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData StopFeeLongOrderData()
        {
            DataTable stopFeeDt = requestData.GetData<DataTable>(0);
            string result = NewObject<FeeItemManagement>().StopFeeLongOrderData(stopFeeDt);
            responseData.AddData(result);
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
            int orderType = requestData.GetData<int>(1);
            int itemID = requestData.GetData<int>(2);
            DateTime startTime = requestData.GetData<DateTime>(3);
            DateTime endTime = requestData.GetData<DateTime>(4);
            DataTable feeList = NewDao<IIPManageDao>().GetCostList(patListID, orderType, itemID, startTime, endTime);
            responseData.AddData(feeList);
            return responseData;
        }

        /// <summary>
        /// 费用冲账
        /// </summary>
        /// <returns>true：冲账成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData StrikeABalance()
        {
            DataTable tempDt = requestData.GetData<DataTable>(0);
            List<string> msgList = new List<string>();
            bool result = NewObject<FeeItemManagement>().StrikeABalance(tempDt, msgList);
            responseData.AddData(result);
            responseData.AddData(msgList);
            return responseData;
        }

        /// <summary>
        /// 取消费用冲账
        /// </summary>
        /// <returns>true：取消冲账成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData CancelStrikeABalance()
        {
            DataTable tempDt = requestData.GetData<DataTable>(0);
            List<string> msgList = new List<string>();
            bool result = NewObject<FeeItemManagement>().CancelStrikeABalance(tempDt, msgList);
            responseData.AddData(result);
            responseData.AddData(msgList);
            return responseData;
        }

        /// <summary>
        /// 定时任务自动滚帐（账单和床位费）
        /// </summary>
        /// <returns>滚账结果</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData AutoFeeItemAccounting()
        {
            try
            {
                return responseData;
            }
            catch (Exception e)
            {
                responseData.AddData(e);
                return responseData;
            }
        }
    }
}
