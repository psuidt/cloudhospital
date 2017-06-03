using System.Collections.Generic;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.ClinicManage;
using HIS_OPDoctor.Dao;
using HIS_OPDoctor.ObjectModel;

namespace HIS_OPDoctor.WcfController
{
    /// <summary>
    /// 处方模板服务控制器
    /// </summary>
    [WCFController]
    public class PresTemplateController : WcfServerController
    {
        /// <summary>
        /// 获取模板头信息
        /// </summary>
        /// <returns>模板头信息</returns>
        [WCFMethod]
        public ServiceResponseData GetPresTemplate()
        {
            var workerId = requestData.GetData<int>(0);
            var intLevel = requestData.GetData<int>(1);
            var presType = requestData.GetData<int>(2);
            var deptID = requestData.GetData<int>(3);
            var empID = requestData.GetData<int>(4);

            var list= NewObject<PresTemplateMrg>().GetPresTemplate(intLevel,workerId, presType, deptID, empID);
            responseData.AddData(list);
            return responseData;
        }

        /// <summary>
        /// 保存模板头信息
        /// </summary>
        /// <returns>模板头信息和成功标识</returns>
        [WCFMethod]
        public ServiceResponseData SaveTempInfo()
        {
            int resFlag = 0;
            var head = requestData.GetData<OPD_PresMouldHead>(0);
            var list = NewObject<PresTemplateMrg>().SaveMouldInfo(head,out resFlag);
            responseData.AddData(list);
            responseData.AddData(resFlag);
            return responseData;
        }

        /// <summary>
        /// 删除模板信息
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData DeleteMoudelInfo()
        {
            var head = requestData.GetData<OPD_PresMouldHead>(0);
            var resFlag = NewObject<PresTemplateMrg>().DeleteMoudel(head);
            responseData.AddData(resFlag);
            return responseData;
        }

        /// <summary>
        /// 检验名称是否重复
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData CheckName()
        {
            var name = requestData.GetData<string>(0);
            var presType = requestData.GetData<int>(1);
            var level = requestData.GetData<int>(2);
            var pid = requestData.GetData<int>(3);
            var id = requestData.GetData<int>(4);
            var resBool = NewObject<PresTemplateMrg>().CheckName(name,presType,level,pid,id);
            responseData.AddData(resBool);
            return responseData;
        }

        /// <summary>
        /// 获取处方模板数据
        /// </summary>
        /// <returns>处方模板数据</returns>
        [WCFMethod]
        public ServiceResponseData GetPresTemplateData()
        {
            var presHeadID = requestData.GetData<int>(0);
            var presType = requestData.GetData<int>(1);
            var resDt = NewObject<IOPPresTemplate>().GetPresTemplateData(presHeadID);
            responseData.AddData(resDt);
            return responseData;
        }

        /// <summary>
        /// 删除模板明细ID
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData DeletePrescriptionData()
        {
            var presMouldDetailID = requestData.GetData<int>(0);
            var resDt = NewObject<IOPPresTemplate>().DeletePrescriptionData(presMouldDetailID);
            responseData.AddData(resDt);
            return responseData;
        }

        /// <summary>
        /// 删除一组处方
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData DeleteGroupPrescriptionData()
        {
            var presMouldHeadID = requestData.GetData<int>(0);
            int presNo = requestData.GetData<int>(1);
            bool bRtn = NewObject<IOPPresTemplate>().DeletePrescriptionData(presMouldHeadID, presNo);
            responseData.AddData(bRtn);
            return responseData;
        }

        /// <summary>
        /// 保存处方模板内容
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SavePresTemplateData()
        {            
            List<OPD_PresMouldDetail> list = requestData.GetData<List<OPD_PresMouldDetail>>(0);
            int bRtn = NewObject<PresTemplateMrg>().SavePresTemplateData(list);
            responseData.AddData(bRtn);
            return responseData;
        }

        /// <summary>
        /// 更新处方号和组Id
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData UpdatePresNoAndGroupId()
        {
            List<OPD_PresMouldDetail> list = requestData.GetData<List<OPD_PresMouldDetail>>(0);
            bool bRtn = NewObject<IOPPresTemplate>().UpdatePresNoAndGroupId(list);
            responseData.AddData(bRtn);
            return responseData;
        }
    }
}
