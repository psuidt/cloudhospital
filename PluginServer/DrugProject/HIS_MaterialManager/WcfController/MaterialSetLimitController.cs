using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.MaterialManage;
using HIS_MaterialManager.Dao;
using HIS_MaterialManager.ObjectModel.BaseData;

namespace HIS_MaterialManage.WcfController
{
    /// <summary>
    /// 库存上下限控制器
    /// </summary>
    [WCFController]
    public  class MaterialSetLimitController : WcfServerController
    {
        /// <summary>
        /// 获取物资显示卡数据
        /// </summary>
        /// <returns>物资信息</returns>
        [WCFMethod]
        public ServiceResponseData GetMaterialDicShowCard()
        {
            DataTable dtRtn = null;
            dtRtn = NewDao<IMWDao>().GetMaterialDicShowCard(false, 0);            
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 取得库房数据
        /// </summary>
        /// <returns>库房数据</returns>
        [WCFMethod]
        public ServiceResponseData GetStoreRoomData()
        {
            DataTable dt = NewObject<MaterialDeptMgr>().GetMaterialDept();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 查询库存
        /// </summary>
        /// <returns>库存数据</returns>
        [WCFMethod]
        public ServiceResponseData LoadStoreLimitData()
        {
            DataTable dtRtn = new DataTable();
            var queryCondition = requestData.GetData<Dictionary<string, string>>(0);            
            dtRtn = NewDao<IMWDao>().GetStoreLimitData(queryCondition);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 保存上下限
        /// </summary>
        /// <returns>保存结果</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveStoreLimit()
        {
            List<MW_Storage> details = requestData.GetData<List<MW_Storage>>(0);
            try
            {
                NewDao<IMWDao>().SaveStoreLimit(details);
                responseData.AddData(true);
            }
            catch (Exception error)
            {
                responseData.AddData(false);
                responseData.AddData(error.Message);
            }

            return responseData;
        }
    }
}
