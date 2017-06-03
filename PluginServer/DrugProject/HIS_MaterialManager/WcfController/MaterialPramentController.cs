using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.BasicData;
using HIS_MaterialManager.Dao;
using HIS_MaterialManager.ObjectModel.BaseData;

namespace HIS_MaterialManage.WcfController
{
    /// <summary>
    /// 物资参数设置控制器
    /// </summary>
    [WCFController]
    public class MaterialPramentController : WcfServerController
    {
        /// <summary>
        /// 取得启用的物资科室
        /// </summary>
        /// <returns>启用的物资科室</returns>
        [WCFMethod]
        public ServiceResponseData GetUsedDeptData()
        {
            DataTable dt = NewObject<MaterialDeptMgr>().GetMaterialDept();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取公共参数
        /// </summary>
        /// <returns>公共参数</returns>
        [WCFMethod]
        public ServiceResponseData GetPublicParameters()
        {
            DataTable dt = NewDao<IMWDao>().GetPublicParameters();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取科室参数
        /// </summary>
        /// <returns>科室参数</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptParameters()
        {
            int deptId = requestData.GetData<int>(0);
            DataTable dt = NewDao<IMWDao>().GetDeptParameters(deptId);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 保存参数
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveParameters()
        {
            List<Basic_SystemConfig> modelList = requestData.GetData<List<Basic_SystemConfig>>(0);
           int ret= NewDao<IMWDao>().SaveParameters(modelList);
            responseData.AddData(ret);
            return responseData;
        }
    }        
}
