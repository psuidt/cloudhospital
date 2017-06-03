using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_DrugManger.Dao;
using HIS_DrugManger.ObjectModel.BaseData;
using HIS_DrugManger.ObjectModel.Store;
using HIS_Entity.DrugManage;

namespace HIS_DrugManger.WcfController
{
    /// <summary>
    /// 库存查询控制器
    /// </summary>
    [WCFController]
    public class QueryStoreController: WcfServerController
    {
        /// <summary>
        /// 取得库房数据
        /// </summary>
        /// <returns>库房数据</returns>
        [WCFMethod]
        public ServiceResponseData GetStoreRoomData()
        {
            int menuTypeFlag = 0;
            string belongSys = requestData.GetData<string>(0);
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                //药库
                menuTypeFlag = 1;
            }
            else
            {
                //药房
                menuTypeFlag = 0;
            }

            DataTable dt = NewObject<RelateDeptMgr>().GetStoreRoomData(menuTypeFlag);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 取得药品类型典
        /// </summary>
        /// <returns>药品类型典</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugTypeDic()
        {
            DataTable dtTypeDic = NewObject<DG_TypeDic>().gettable();
            responseData.AddData(dtTypeDic);
            return responseData;
        }

        /// <summary>
        /// 取得药品剂型典，根据药品类型过滤
        /// </summary>
        /// <returns>药品剂型典</returns>
        [WCFMethod]
        public ServiceResponseData GetDosageDic()
        {
            string where = string.Empty;
            int typeId = requestData.GetData<int>(0);
            where = "TypeID=" + typeId + " and DelFlag=0";
            DataTable dtDosageDic = NewObject<DG_DosageDic>().gettable(where);
            responseData.AddData(dtDosageDic);
            return responseData;
        }

        /// <summary>
        /// 查询药品库存信息
        /// </summary>
        /// <returns>库存信息表</returns>
        [WCFMethod]
        public ServiceResponseData LoadDrugStorage()
        {
            var systemType = requestData.GetData<string>(0);
            var queryCondition = requestData.GetData<Dictionary<string, string>>(1);
            IStore iProcess = NewObject<StoreFactory>().GetStoreProcess(systemType);
            DataTable dtRtn = iProcess.LoadDrugStorage(queryCondition);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 查询药品批次信息
        /// </summary>
        /// <returns>药品批次信息列表</returns>
        [WCFMethod]
        public ServiceResponseData LoadDrugBatch()
        {
            var systemType = requestData.GetData<string>(0);
            int storageID = requestData.GetData<int>(1);
            IStore iProcess = NewObject<StoreFactory>().GetStoreProcess(systemType);
            DataTable dtRtn = iProcess.LoadDrugBatch(storageID);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 修改库存停用标识
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData UpdateFlag()
        {
            int DelFlag= requestData.GetData<int>(0);
            int storageID = requestData.GetData<int>(1);
            bool Flag = NewDao<IDSDao>().UpdateStorageFlag(storageID, DelFlag);
            responseData.AddData(Flag);
            return responseData;
        }

        /// <summary>
        /// 修改有效库存
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData UpdateValidateStore()
        {
            int DrugID= requestData.GetData<int>(0);
            int DeptID = requestData.GetData<int>(1);

            bool Flag = NewDao<IDSDao>().UpdateValidateStorage(DeptID, DrugID);
            responseData.AddData(Flag);

            if (Flag==false)
            {
                responseData.AddData("[门诊/住院部]存在该药品的待发记录,所以不允许修改有效库存！");
            }
            return responseData;
        }
    }
}
