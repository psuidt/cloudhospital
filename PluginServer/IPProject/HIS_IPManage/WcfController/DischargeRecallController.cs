using System;
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
    /// 收费查询控制器
    /// </summary>
    [WCFController]
    public class DischargeRecallController : WcfServerController
    {
        /// <summary>
        /// 查询收费员列表
        /// </summary>
        /// <returns>收费员列表</returns>
        [WCFMethod]
        public ServiceResponseData GetCashier()
        {
            DataTable cashierDt = NewObject<BasicDataManagement>().GetBasicData(EmpDataSourceType.收费员, true);
            responseData.AddData(cashierDt);
            return responseData;
        }

        /// <summary>
        /// 查询已结算列表
        /// </summary>
        /// <returns>已结算列表</returns>
        [WCFMethod]
        public ServiceResponseData GetCostFeeList()
        {
            DateTime costBeginDate = requestData.GetData<DateTime>(0);
            DateTime costEndDate = requestData.GetData<DateTime>(1);
            string sqlectParam = requestData.GetData<string>(2);
            int empId = requestData.GetData<int>(3);
            int status = requestData.GetData<int>(4);
            int isAccount = requestData.GetData<int>(5);
            string costType = requestData.GetData<string>(6);
            DataTable costList = NewDao<IIPManageDao>().GetCostFeeList(costBeginDate, costEndDate, sqlectParam, empId, status, isAccount, costType);
            responseData.AddData(costList);
            return responseData;
        }

        /// <summary>
        /// 取消结算
        /// </summary>
        /// <returns>true：取消成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData CancelSettlement()
        {
            int patListID = requestData.GetData<int>(0);
            int costHeadID = requestData.GetData<int>(1);
            int costType = requestData.GetData<int>(2);
            bool result = NewObject<CostManagement>().CancelSettlement(patListID, costHeadID, costType);
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 住院押金查询
        /// </summary>
        /// <returns>押金信息</returns>
        [WCFMethod]
        public ServiceResponseData GetAllDepositList()
        {
            DateTime costBeginDate = requestData.GetData<DateTime>(0);
            DateTime costEndDate = requestData.GetData<DateTime>(1);
            string sqlectParam = requestData.GetData<string>(2);
            int empId = requestData.GetData<int>(3);
            int status = requestData.GetData<int>(4);
            int isAccount = requestData.GetData<int>(5);
            DataTable depositList = NewDao<IIPManageDao>().GetAllDepositList(costBeginDate, costEndDate, sqlectParam, empId, status, isAccount);
            responseData.AddData(depositList);
            return responseData;
        }

        /// <summary>
        /// 发票补打获取发票显示数据
        /// </summary>
        /// <returns>发票数据</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData GetInvoiceFillData()
        {
            int patListID = requestData.GetData<int>(0);
            int costHeadID = requestData.GetData<int>(1);
            IP_PrintInvoiceInfo printInvoiceInfo = requestData.GetData<IP_PrintInvoiceInfo>(2);
            // 保存发票补打记录
            this.BindDb(printInvoiceInfo);
            printInvoiceInfo.save();
            DataTable costDt = NewDao<IIPManageDao>().GetInvoiceFillData(costHeadID);
            DataTable feeDt = NewDao<IIPManageDao>().StatisticsFeeByFeeType(patListID, costHeadID);
            responseData.AddData(costDt);
            responseData.AddData(feeDt);
            return responseData;
        }
    }
}
