using System.Collections.Generic;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_OPDoctor.Dao;

namespace HIS_OPDoctor.WcfController
{
    /// <summary>
    /// 查看历史诊疗记录服务控制器
    /// </summary>
    [WCFController]
    public class QueryHisRecordController : WcfServerController
    {
        /// <summary>
        /// 根据科室获取项目分类
        /// </summary>
        /// <returns>项目分类信息</returns>
        [WCFMethod]
        public ServiceResponseData GetHisRecord()
        {
            var memId = requestData.GetData<int>(0);
            var workId = requestData.GetData<int>(1);
            var queryWhere = requestData.GetData<Dictionary<string, string>>(2);
            var record = NewDao<IOPDDao>()
                .GetHisRecord(memId, queryWhere);
            responseData.AddData(record);
            return responseData;
        }

        /// <summary>
        /// 获取科室下的医生
        /// </summary>
        /// <returns>科室下的医生</returns>
        [WCFMethod]
        public ServiceResponseData GetDoctor()
        {
            var deptId = requestData.GetData<int>(0);
            var doctor = NewDao<IOPDDao>().GetDoctor(deptId);
            responseData.AddData(doctor);
            return responseData;
        }
    }
}
