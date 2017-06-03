using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_IPManage.Dao;
using HIS_IPManage.ObjectModel;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPManage.WcfController
{
    /// <summary>
    /// 统领药品管理控制器
    /// </summary>
    [WCFController]
    public class CommandManagementController : WcfServerController
    {
        /// <summary>
        /// 获取执行科室列表
        /// </summary>
        /// <returns>执行科室列表</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptDataSourceList()
        {
            DataTable execDeptDt = NewObject<BasicDataManagement>().GetBasicData(DeptDataSourceType.药房科室);
            responseData.AddData(execDeptDt);
            return responseData;
        }

        /// <summary>
        /// 查询药品统领列表
        /// </summary>
        /// <returns>药品统领列表</returns>
        [WCFMethod]
        public ServiceResponseData GetCommandSheetList()
        {
            int patDeptID = requestData.GetData<int>(0);
            int execDeptID = requestData.GetData<int>(1);
            bool commandStatus = requestData.GetData<bool>(2);
            DataTable commandList = NewDao<IIPManageDao>().GetCommandSheetList(patDeptID, execDeptID, commandStatus);
            responseData.AddData(commandList);
            return responseData;
        }

        /// <summary>
        /// 发送药品统领单
        /// </summary>
        /// <returns>true：发送成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SendCommandList()
        {
            DataTable commandList = requestData.GetData<DataTable>(0);
            int makeEmpID = requestData.GetData<int>(1);
            string makeEmpName = requestData.GetData<string>(2);
            bool commandStatus = requestData.GetData<bool>(3);
            int workID = requestData.GetData<int>(4);
            bool result = NewObject<DrugbillManagement>().SendCommandList(commandList, makeEmpID, makeEmpName, commandStatus, workID);
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 获取所有已发送统领的住院病人列表
        /// </summary>
        /// <returns>住院病人列表</returns>
        [WCFMethod]
        public ServiceResponseData GetHasBeenSentDrugbillPatList()
        {
            DataTable resultDt = NewDao<IIPManageDao>().GetHasBeenSentDrugbillPatList();
            responseData.AddData(resultDt);
            return responseData;
        }

        /// <summary>
        /// 查询统领单列表
        /// </summary>
        /// <returns>统领单列表</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugbillOrderList()
        {
            int deptId = requestData.GetData<int>(0);
            bool orderStatus = requestData.GetData<bool>(1);
            int patListID = requestData.GetData<int>(2);
            DateTime startDate = requestData.GetData<DateTime>(3);
            DateTime endDate = requestData.GetData<DateTime>(4);
            DataTable orderList = NewDao<IIPManageDao>().GetDrugbillOrderList(deptId, orderStatus, patListID, startDate, endDate);
            responseData.AddData(orderList);
            return responseData;
        }

        /// <summary>
        /// 查询统领单汇总和明细数据
        /// </summary>
        /// <returns>统领单汇总和明细数据</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugBillData()
        {
            int billHeadID = requestData.GetData<int>(0);
            int dispDrugFlag = requestData.GetData<int>(1);
            int patListID = requestData.GetData<int>(2);
            DataTable summaryDt = NewDao<IIPManageDao>().GetDrugBillSummaryData(billHeadID, dispDrugFlag, patListID);
            DataTable detailDt = NewDao<IIPManageDao>().GetDrugBillDetailData(billHeadID, dispDrugFlag, patListID);
            responseData.AddData(summaryDt);
            responseData.AddData(detailDt);
            return responseData;
        }

        /// <summary>
        /// 重新发送统领单
        /// </summary>
        /// <returns>true：重新发送成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData AgainSendOrder()
        {
            int billHeadID = requestData.GetData<int>(0);
            NewDao<IIPManageDao>().AgainSendOrder(billHeadID);
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 取消发送统领单
        /// </summary>
        /// <returns>true：取消发送成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData CancelSendOrder()
        {
            int billHeadID = requestData.GetData<int>(0);
            string result = NewObject<DrugbillManagement>().CancelSendOrder(billHeadID);
            responseData.AddData(result);
            return responseData;
        }
    }
}
