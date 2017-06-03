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
    /// 库位管理控制器
    /// </summary> 
    [WCFController]
    class LocationController : WcfServerController
    {
        /// <summary>
        /// 保存库位管理
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData SaveLocation()
        {
            DG_Location location = requestData.GetData<DG_Location>(0);
            DG_Location oldlocation = NewDao<IDGDao>().GetLocationByName(location.ParentID, location.LocationName);
            if (oldlocation == null)
            {
                this.BindDb(location);
                int result = location.save();
                if (result > 0)
                {
                    responseData.AddData(true);
                }
                else
                {
                    responseData.AddData(false);
                }
            }
            else
            {
                if (oldlocation.LocationID == location.LocationID)
                {
                    this.BindDb(location);
                    int result = location.save();
                    if (result > 0)
                    {
                        responseData.AddData(true);
                    }
                    else
                    {
                        responseData.AddData(false);
                    }
                }
                else
                {
                    responseData.AddData(false);
                }
            }

            return responseData;
        }

        /// <summary>
        /// 取得库房数据
        /// </summary>
        /// <returns>库房数据</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptRoomData()
        {
            int menuTypeFlag = 0;
            string belongSys = requestData.GetData<string>(0);
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                menuTypeFlag = 1;//药库
            }
            else
            {
                menuTypeFlag = 0;//药房
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
        /// 获取库位管理数据集
        /// </summary>
        /// <returns>库位管理数据集</returns>
        [WCFMethod]
        public ServiceResponseData GetLocationList()
        {
            var deptid = requestData.GetData<int>(0);
            var locationlist = NewDao<IDGDao>()
                .GetLocationList(deptid);
            responseData.AddData(locationlist);
            return responseData;
        }

        /// <summary>
        /// 删除库位信息
        /// </summary>
        /// <returns>返回处理结果</returns>
        [WCFMethod]
        public ServiceResponseData DelLoacation()
        {
            DG_Location location = requestData.GetData<DG_Location>(0);
            this.BindDb(location);
            int result = location.delete();
            if (result > 0)
            {
                responseData.AddData(true);
            }
            else
            {
                responseData.AddData(false);
            }

            return responseData;
        }

        /// <summary>
        /// 获取库位详情
        /// </summary>
        /// <returns>库位详情</returns>
        [WCFMethod]
        public ServiceResponseData GetLocationInfo()
        {
            var locationid = requestData.GetData<int>(0);
            var location = NewDao<IDGDao>().GetLocationInfo(locationid);
            responseData.AddData(location);
            return responseData;
        }

        /// <summary>
        /// 归库操作
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData UpdateStorage()
        {
            var locationid = requestData.GetData<int>(0);
            var ids = requestData.GetData<string>(1);
            var frmName = requestData.GetData<string>(2);
            var location = NewDao<IDGDao>()
                .UpdateStorage(locationid, ids, frmName);
            responseData.AddData(location);
            return responseData;
        }
    }
}
