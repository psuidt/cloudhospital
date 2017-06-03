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
    /// 门诊病历书写服务控制器
    /// </summary>
    [WCFController]
    public class OMRController : WcfServerController
    {
        /// <summary>
        /// 获取模板头信息
        /// </summary>
        /// <returns>模板头信息</returns>
        [WCFMethod]
        public ServiceResponseData GetOMRTemplate()
        {
            var workerId = requestData.GetData<int>(0);
            var intLevel = requestData.GetData<int>(1);
            var deptID = requestData.GetData<int>(2);
            var empID = requestData.GetData<int>(3);
            var list = NewObject<OMRTplManager>().GetOMRTemplate(intLevel, workerId, deptID, empID);
            responseData.AddData(list);
            return responseData;
        }

        /// <summary>
        /// 保存模板头信息
        /// </summary>
        /// <returns>模板头实体</returns>
        [WCFMethod]
        public ServiceResponseData SaveTempInfo()
        {
            int resFlag = 0;
            var head = requestData.GetData<OPD_OMRTmpHead>(0);
            var list = NewObject<OMRTplManager>().SaveMouldInfo(head, out resFlag);
            responseData.AddData(list);
            responseData.AddData(resFlag);
            return responseData;
        }

        /// <summary>
        /// 删除模板信息
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData DeleteMoudelInfo()
        {
            var head = requestData.GetData<OPD_OMRTmpHead>(0);
            var resFlag = NewObject<OMRTplManager>().DeleteMoudel(head);
            responseData.AddData(resFlag);
            return responseData;
        }

        /// <summary>
        /// 检验名称是否重复
        /// </summary>
        /// <returns>true存在</returns>
        [WCFMethod]
        public ServiceResponseData CheckName()
        {
            var name = requestData.GetData<string>(0);
            var level = requestData.GetData<int>(1);
            var pid = requestData.GetData<int>(2);
            var id = requestData.GetData<int>(3);
            var resBool = NewObject<OMRTplManager>().CheckName(name, level, pid, id);
            responseData.AddData(resBool);
            return responseData;
        }

        /// <summary>
        /// 获取病历模板头信息
        /// </summary>
        /// <returns>病历模板头信息</returns>
        [WCFMethod]
        public ServiceResponseData LoadMouldHead()
        {
            var headId = requestData.GetData<int>(0);
            var head = NewDao<IOPDDao>().LoadOMRMouldHead(headId);
            responseData.AddData(head);
            return responseData;
        }

        /// <summary>
        /// 保存病历模板
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData AsSaveTmp()
        {
            var headModel = requestData.GetData<OPD_OMRTmpHead>(0);
            var detailModel = requestData.GetData<OPD_OMRTmpDetail>(1);
            bool resFlag = NewObject<OMRTplManager>().AsSaveTmp(headModel, detailModel);
            responseData.AddData(resFlag);
            return responseData;
        }

        /// <summary>
        /// 取得门诊病历模板明细
        /// </summary>
        /// <returns>诊病历模板明细</returns>
        [WCFMethod]
        public ServiceResponseData GetOMRTemplateDetail()
        {
            var headId = requestData.GetData<int>(0);
            OPD_OMRTmpDetail head = NewObject<OMRTplManager>().GetOMRTemplateDetail(headId);
            responseData.AddData(head);
            return responseData;
        }
    }
}
