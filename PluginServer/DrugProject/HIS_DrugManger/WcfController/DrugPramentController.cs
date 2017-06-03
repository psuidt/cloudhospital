using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_DrugManger.Dao;
using HIS_Entity.BasicData;

namespace HIS_DrugManger.WcfController
{
    /// <summary>
    /// 药品参数设置控制器
    /// </summary>
    [WCFController]
    public class DrugPramentController : WcfServerController
    {
        /// <summary>
        /// 取得启用的药剂科室
        /// </summary>
        /// <returns>启用的药剂科室</returns>
        [WCFMethod]
        public ServiceResponseData GetUsedDrugDeptData()
        {
            DataTable dt = NewDao<IDGDao>().GetUsedDrugDeptData();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取药品公共参数
        /// </summary>
        /// <returns>药品公共参数</returns>
        [WCFMethod]
        public ServiceResponseData GetPublicParameters()
        {
            DataTable dt = NewDao<IDGDao>().GetPublicParameters();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取药剂科室参数
        /// </summary>
        /// <returns>药剂科室参数</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptParameters()
        {
            int deptId = requestData.GetData<int>(0);
            DataTable dt = NewDao<IDGDao>().GetDeptParameters(deptId);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 保存药品参数
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveDrugParameters()
        {
            List<Basic_SystemConfig> modelList = requestData.GetData<List<Basic_SystemConfig>>(0);
           int ret= NewDao<IDGDao>().SaveDrugParameters(modelList);
            responseData.AddData(ret);
            return responseData;
        }
    }   
}
