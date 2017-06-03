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
    /// 护士站医嘱发送服务端控制器
    /// </summary>
    [WCFController]
    public class ExamListController : WcfServerController
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
        /// 获取打印数据
        /// </summary>
        /// <returns>打印数据</returns>
        [WCFMethod]
        public ServiceResponseData GetExamList()
        {
            int iDeptID = requestData.GetData<int>(0);
            int iApplyType = requestData.GetData<int>(1);
            DateTime dApplyDate = requestData.GetData<DateTime>(2);
            int iOrderCategory = requestData.GetData<int>(3);
            int iState = requestData.GetData<int>(4);

            DataTable dtExamList = NewDao<IExamListDao>().GetExamList(iDeptID, iApplyType, dApplyDate, iOrderCategory, iState);
            responseData.AddData(dtExamList);
            return responseData;
        }

        /// <summary>
        /// 更新打印状态
        /// </summary>
        /// <returns>true更新成功</returns>
        [WCFMethod]
        public ServiceResponseData UpdateApplyPrint()
        {
            List<int> iApplyHeadIDList = requestData.GetData<List<int>>(0);
            int iEmpId = requestData.GetData<int>(1);
            bool b = NewDao<IExamListDao>().UpdateApplyPrint(iApplyHeadIDList, iEmpId);
            responseData.AddData(b);
            return responseData;
        }
    }
}
