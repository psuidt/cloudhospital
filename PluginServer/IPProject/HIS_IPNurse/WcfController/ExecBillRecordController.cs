using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_IPNurse.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPNurse.WcfController
{
    /// <summary>
    /// 护士站执行单服务端控制器
    /// </summary>
    [WCFController]
    public class ExecBillRecordController : WcfServerController
    {
        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <returns>科室列表</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptList()
        {
            // 调用共通接口查询科室列表
            DataTable deptDt = NewObject<BasicDataManagement>().GetBasicData(DeptDataSourceType.住院临床科室, false);
            responseData.AddData(deptDt);
            return responseData;
        }

        /// <summary>
        /// 获取执行单类型列表
        /// </summary>
        /// <returns>执行单类型列表</returns>
        [WCFMethod]
        public ServiceResponseData GetReportTypeList()
        {
            DataTable dtReportTypeList = NewDao<IExecBillRecordDao>().GetReportTypeList();
            responseData.AddData(dtReportTypeList);
            return responseData;
        }

        /// <summary>
        /// 获取执行单数据
        /// </summary>
        /// <returns>执行单数据</returns>
        [WCFMethod]
        public ServiceResponseData GetExcuteList()
        {
            int iDeptId = requestData.GetData<int>(0);
            int iType = requestData.GetData<int>(1);
            DateTime dFeeDate = requestData.GetData<DateTime>(2);
            int iOrderCategory = requestData.GetData<int>(3);
            int iState = requestData.GetData<int>(4);
            bool typeName = requestData.GetData<bool>(5);
            DataTable dtExcuteList = NewDao<IExecBillRecordDao>().GetExcuteList(iDeptId, iType, dFeeDate, iOrderCategory, iState, typeName);
            responseData.AddData(dtExcuteList);
            return responseData;
        }

        /// <summary>
        /// 获取执行单数据
        /// </summary>
        /// <returns>执行单数据</returns>
        [WCFMethod]
        public ServiceResponseData SetExcuteList()
        {
            List<int> iExecIdList = requestData.GetData<List<int>>(0);
            int iState = requestData.GetData<int>(1);
            int iEmpId = requestData.GetData<int>(2);

            bool b = NewDao<IExecBillRecordDao>().SetExcuteList(iExecIdList, iState, iEmpId);
            responseData.AddData(b);
            return responseData;
        }
    }
}
