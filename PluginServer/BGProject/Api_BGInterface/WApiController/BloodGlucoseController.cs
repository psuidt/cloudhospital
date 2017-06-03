using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WebFrame.WebAPI;
using System.Data;
using Newtonsoft.Json;
using HIS_Entity.IPManage;

namespace Api_BGInterface.WApiController
{

    /// <summary>
    /// 血糖数据接口
    /// http://localhost:8021/HISApi/BGService.Service/BloodGlucose/Test
    /// http://localhost:8021/HISApi/BGService.Service/BloodGlucose/GetInPatientBaseInfo?iWorkId=1
    /// http://localhost:8021/HISApi/BGService.Service/BloodGlucose/GetInPatientHospitalizationInfo?iWorkId=1
    /// http://localhost:8021/HISApi/BGService.Service/BloodGlucose/GetWorkDeptInfo?iWorkId=1
    /// http://localhost:8021/HISApi/BGService.Service/BloodGlucose/GetWorkAreaInfo?iWorkId=1
    /// http://localhost:8021/HISApi/BGService.Service/BloodGlucose/GetWorkBedInfo?iWorkId=1
    /// http://localhost:8021/HISApi/BGService.Service/BloodGlucose/GetWorkUserInfo?iWorkId=1
    /// 血糖数据上传到HIS数据库
    /// http://localhost:8021/HISApi/BGService.Service/BloodGlucose/PostPluRecord?iWorkId=1
    /// http://localhost:8021/HISApi/BGService.Service/BloodGlucose/PostPluRecordList?iWorkId=1
    /// POST提交
    /// Accept: application/json
    /// Content-Type: application/json
    /// Request Body:{"PatlistID":"113","OpreratorDate":"2016-11-07 00:00:00","DateType":"1","PluValue":"3.1","PluRange":"1.0-5.0","HighLow":"0","OpreratorEmpID":"1002","Memo":""}
    /// </summary>
    [efwplusApiController(PluginName = "BGService.Service")]
    public class BloodGlucoseController: WebApiController
    {
        [HttpGet]
        public string Test()
        {
            ServiceResponseData retdata = InvokeWcfService("BGProject.Service", "HISBloodGlucoseController", "GetHello");
            string hello = retdata.GetData<string>(0);
            return hello + " WebApi";
        }

        /// <summary>
        /// 1.获取在院患者主索引基本信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetInPatientBaseInfo(int iWorkId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iWorkId);
            });

            ServiceResponseData retdata = InvokeWcfService("BGProject.Service", "HISBloodGlucoseController", "GetInPatientBaseInfo", requestAction);
            DataTable dtInPatientBaseInfo = retdata.GetData<DataTable>(0);
            return JsonConvert.SerializeObject(dtInPatientBaseInfo, Formatting.Indented);
        }

        /// <summary>
        /// 2.获取在院患者住院信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetInPatientHospitalizationInfo(int iWorkId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iWorkId);
            });

            ServiceResponseData retdata = InvokeWcfService("BGProject.Service", "HISBloodGlucoseController", "GetInPatientHospitalizationInfo", requestAction);
            DataTable dtInPatientHospitalizationInfo = retdata.GetData<DataTable>(0);
            return JsonConvert.SerializeObject(dtInPatientHospitalizationInfo, Formatting.Indented);
        }

        /// <summary>
        /// 3.获取在用科室信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetWorkDeptInfo(int iWorkId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iWorkId);
            });

            ServiceResponseData retdata = InvokeWcfService("BGProject.Service", "HISBloodGlucoseController", "GetWorkDeptInfo", requestAction);
            DataTable dtDeptInfo = retdata.GetData<DataTable>(0);
            return JsonConvert.SerializeObject(dtDeptInfo, Formatting.Indented);
        }

        /// <summary>
        /// 4.获取在用病区信息
        /// 科室和病区并没有硬性的从属关系，这里需要商榷
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetWorkAreaInfo(int iWorkId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iWorkId);
            });

            ServiceResponseData retdata = InvokeWcfService("BGProject.Service", "HISBloodGlucoseController", "GetWorkAreaInfo", requestAction);
            DataTable dtAreaInfo = retdata.GetData<DataTable>(0);
            return JsonConvert.SerializeObject(dtAreaInfo, Formatting.Indented);
        }

        /// <summary>
        /// 5.获取在用床位信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetWorkBedInfo(int iWorkId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iWorkId);
            });

            ServiceResponseData retdata = InvokeWcfService("BGProject.Service", "HISBloodGlucoseController", "GetWorkBedInfo", requestAction);
            DataTable dtBedInfo = retdata.GetData<DataTable>(0);
            return JsonConvert.SerializeObject(dtBedInfo, Formatting.Indented);
        }

        /// <summary>
        /// 6.获取在用用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetWorkUserInfo(int iWorkId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iWorkId);
            });

            ServiceResponseData retdata = InvokeWcfService("BGProject.Service", "HISBloodGlucoseController", "GetWorkUserInfo", requestAction);
            DataTable dtUserInfo = retdata.GetData<DataTable>(0);
            return JsonConvert.SerializeObject(dtUserInfo, Formatting.Indented);
        }



        /// <summary>
        /// 7.血糖数据上传到HIS
        /// </summary>
        /// <param name="workid"></param>
        /// <param name="pluRecord"></param>
        /// <returns></returns>
        [HttpPost]
        public bool PostPluRecord(int iWorkId, [FromBody]IP_PluRecord pluRecord)
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(iWorkId);
                    request.AddData(pluRecord);
                });

                ServiceResponseData retdata = InvokeWcfService("BGProject.Service", "HISBloodGlucoseController", "InsertPluRecord", requestAction);
                bool result = retdata.GetData<bool>(0);
                return result;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 7.血糖数据上传到HIS
        /// </summary>
        /// <param name="workid"></param>
        /// <param name="pluRecord"></param>
        /// <returns></returns>
        [HttpPost]
        public bool PostPluRecordList(int iWorkId, [FromBody]List<IP_PluRecord> pluRecordList)
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(iWorkId);
                    request.AddData(pluRecordList);
                });

                ServiceResponseData retdata = InvokeWcfService("BGProject.Service", "HISBloodGlucoseController", "InsertPluRecordList", requestAction);
                bool result = retdata.GetData<bool>(0);
                return result;
            }
            catch
            {
                return false;
            }
        }

       
    }
}
