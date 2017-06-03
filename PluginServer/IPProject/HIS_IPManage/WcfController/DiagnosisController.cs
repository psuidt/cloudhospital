using System;
using System.Collections.Generic;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.ClinicManage;
using HIS_IPManage.Dao;

namespace HIS_IPManage.WcfController
{
    /// <summary>
    /// 住院诊断控制器
    /// </summary>
    [WCFController]
    public class DiagnosisController : WcfServerController
    {
        /// <summary>
        /// 加载诊断数据
        /// </summary>
        /// <returns>诊断数据列表</returns>
        [WCFMethod]
        public ServiceResponseData LoadDiagnosisInfo()
        {
            var patListID = requestData.GetData<int>(0);

            var dt = NewDao<IDiagnosisDao>().LoadDiagnosisInfo(patListID);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取诊断类型列表
        /// </summary>
        /// <returns>诊断类型列表</returns>
        [WCFMethod]
        public ServiceResponseData GetDiagnosisClass()
        {
            var dt = NewDao<IDiagnosisDao>().GetDiagnosisClass();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取诊断信息
        /// </summary>
        /// <returns>诊断信息</returns>
        [WCFMethod]
        public ServiceResponseData GetBasicDiagnosis()
        {
            var dt = NewDao<IDiagnosisDao>().GetBasicDiagnosis();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 保存病人诊断信息
        /// </summary>
        /// <returns>true：保存成功</returns>
        [WCFMethod]
        public ServiceResponseData SaveDiagInfo()
        {
            var diagInfo = requestData.GetData<IPD_Diagnosis>(0);
            List<IPD_Diagnosis> list = NewObject<IPD_Diagnosis>().getlist<IPD_Diagnosis>(" ID!= " + diagInfo.ID+"  and  patlistid="+diagInfo.PatListID+" and  DiagnosisName='" + diagInfo.DiagnosisName+"'");
            if (list.Count > 0)
            {
                throw new Exception("已经存在" + diagInfo.DiagnosisName + "的相同诊断");
            }

            this.BindDb(diagInfo);
            responseData.AddData(diagInfo.save());
            return responseData;
        }

        /// <summary>
        /// 删除诊断信息
        /// </summary>
        /// <returns>true：删除成功</returns>
        [WCFMethod]
        public ServiceResponseData DeleteDiagnosis()
        {
            var id = requestData.GetData<int>(0);
            var resFlag = NewDao<IDiagnosisDao>().DeleteDiag(id);
            responseData.AddData(resFlag);
            return responseData;
        }

        /// <summary>
        /// 验证诊断信息
        /// </summary>
        /// <returns>true：验证通过</returns>
        [WCFMethod]
        public ServiceResponseData CheckDiagnosisInfo()
        {
            var patListID = requestData.GetData<int>(0);
            var flag = requestData.GetData<int>(1);
            var diagID = requestData.GetData<int>(2);
            var main = requestData.GetData<int>(3);
            var diagName = requestData.GetData<string>(4);
            var id = requestData.GetData<int>(5);

            var resflag = NewDao<IDiagnosisDao>().CheckDiagnosisInfo( patListID,  flag,  diagID,  main,  diagName,  id);
            responseData.AddData(resflag);
            return responseData;
        }
    }
}
