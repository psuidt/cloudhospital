using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_MaterialManager.Dao;
using HIS_MaterialManager.ObjectModel.BaseData;

namespace HIS_MaterialManage.WcfController
{
    /// <summary>
    /// 物资库存查询控制器
    /// </summary>
    [WCFController]
    public class MaterialQueryStoreController : WcfServerController
    {
        /// <summary>
        /// 取得库房数据
        /// </summary>
        /// <returns>库房数据</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptRoomData()
        {
            DataTable dt = NewObject<MaterialDeptMgr>().GetMaterialDept();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 取得物资类型典
        /// </summary>
        /// <returns>物资类型典</returns>
        [WCFMethod]
        public ServiceResponseData GetMaterialTypeDic()
        {
            DataTable dtTypeDic = NewObject<IMWDao>().GetMaterialTypeShowCard();
            responseData.AddData(dtTypeDic);
            return responseData;
        }

        /// <summary>
        /// 查询物资库存信息
        /// </summary>
        /// <returns>库存信息表</returns>
        [WCFMethod]
        public ServiceResponseData LoadMaterialStorage()
        {
            var queryCondition = requestData.GetData<Dictionary<string, string>>(0);
            var typeId = requestData.GetData<string>(1);
            DataTable dtRtn = NewDao<IMWDao>().LoadMaterialStorage(queryCondition, typeId); 
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 查询物资批次信息
        /// </summary>
        /// <returns>药品批次信息列表</returns>
        [WCFMethod]
        public ServiceResponseData LoadMaterialBatch()
        {
            int storageID = requestData.GetData<int>(0);
            DataTable dtRtn = NewDao<IMWDao>().LoadMaterialBatch(storageID);
            responseData.AddData(dtRtn);
            return responseData;
        }
    }
}
