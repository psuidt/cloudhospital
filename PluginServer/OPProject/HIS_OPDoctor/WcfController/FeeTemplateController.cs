using System.Collections.Generic;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.ClinicManage;
using HIS_OPDoctor.Dao;
using HIS_OPDoctor.ObjectModel;

namespace HIS_OPDoctor.WcfController
{
    /// <summary>
    /// 费用模板服务控制器
    /// </summary>
    [WCFController]
    public class FeeTemplateController : WcfServerController
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

            var list = NewObject<FeeTemplateMrg>().GetPresTemplate(intLevel, workerId, presType, deptID, empID);
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
            var list = NewObject<FeeTemplateMrg>().SaveMouldInfo(head, out resFlag);
            responseData.AddData(list);
            responseData.AddData(resFlag);
            return responseData;
        }

        /// <summary>
        /// 删除模板信息
        /// </summary>
        /// <returns>成功标识</returns>
        [WCFMethod]
        public ServiceResponseData DeleteMoudelInfo()
        {
            var head = requestData.GetData<OPD_PresMouldHead>(0);
            var resFlag = NewObject<FeeTemplateMrg>().DeleteMoudel(head);
            responseData.AddData(resFlag);
            return responseData;
        }

        /// <summary>
        /// 检验名称是否重复
        /// </summary>
        /// <returns>true存在重复</returns>
        [WCFMethod]
        public ServiceResponseData CheckName()
        {
            var name = requestData.GetData<string>(0);
            var presType = requestData.GetData<int>(1);
            var level = requestData.GetData<int>(2);
            var pid = requestData.GetData<int>(3);
            var id = requestData.GetData<int>(4);
            var resBool = NewObject<FeeTemplateMrg>().CheckName(name, presType, level, pid, id);
            responseData.AddData(resBool);
            return responseData;
        }

        /// <summary>
        /// 读取ShowCard数据
        /// </summary>
        /// <returns>费用数据</returns>
        [WCFMethod]
        public ServiceResponseData LoadFeeInfoCard()
        {
            var workerId = requestData.GetData<int>(0);
            var fees = NewDao<IOPDDao>().LoadFeeInfoCard(workerId);
            responseData.AddData(fees);
            return responseData;
        }

        /// <summary>
        /// 获取费用模板头信息
        /// </summary>
        /// <returns>费用模板头信息</returns>
        [WCFMethod]
        public ServiceResponseData LoadMouldHead()
        {
            var headId = requestData.GetData<int>(0);
            var head = NewDao<IOPDDao>().LoadMouldHead(headId);
            responseData.AddData(head);
            return responseData;
        }

        /// <summary>
        /// 获取费用模板明细信息
        /// </summary>
        /// <returns>费用模板信息</returns>
        [WCFMethod]
        public ServiceResponseData LoadMouldDetail()
        {
            var headId = requestData.GetData<int>(0);
            var fees = NewDao<IOPDDao>().LoadMouldDetail(headId);
            responseData.AddData(fees);
            return responseData;
        }

        /// <summary>
        /// 保存明细
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData SaveDetail()
        {
            var mouldList = requestData.GetData<List<OPD_PresMouldDetail>>(0);
            SetWorkId(oleDb.WorkId);
            var result = NewObject<FeeTemplateMrg>().SaveDetail(mouldList);
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 删除费用模板信息
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData DelDetail()
        {
            var detailId = requestData.GetData<int>(0);
            var result = NewDao<IOPDDao>().DelDetail(detailId);
            responseData.AddData(result);
            return responseData;
        }
    }
}
