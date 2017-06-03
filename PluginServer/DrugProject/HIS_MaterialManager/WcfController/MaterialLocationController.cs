using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.MaterialManage;
using HIS_MaterialManager.Dao;
using HIS_MaterialManager.ObjectModel.BaseData;

namespace HIS_MaterialManage.WcfController
{
    /// <summary>
    /// 库位管理控制器
    /// </summary>
    [WCFController]
    class MaterialLocationController : WcfServerController
    {
        /// <summary>
        /// 保存库位
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData SaveLocation()
        {
            MW_Location location = requestData.GetData<MW_Location>(0);
            MW_Location oldlocation = NewDao<IMWDao>().GetLocationByName(location.ParentID, location.LocationName);
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
        /// 获取库位列表
        /// </summary>
        /// <returns>库位列表</returns>
        [WCFMethod]
        public ServiceResponseData GetLocationList()
        {
            var deptid = requestData.GetData<int>(0);
            var locationlist = NewDao<IMWDao>()
                .GetLocationList(deptid);
            responseData.AddData(locationlist);
            return responseData;
        }

        /// <summary>
        /// 删除库位
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData DelLoacation()
        {
            MW_Location location = requestData.GetData<MW_Location>(0);
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
         /// 获取库位信息
         /// </summary>
         /// <returns>库位信息</returns>
        [WCFMethod]
        public ServiceResponseData GetLocationInfo()
        {
            var locationid = requestData.GetData<int>(0);
            var location = NewDao<IMWDao>()
                .GetLocationInfo(locationid);
            responseData.AddData(location);
            return responseData;
        }

        /// <summary>
        /// 归库操作
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData UpdateStorage()
        {
            var locationid = requestData.GetData<int>(0);
            var ids = requestData.GetData<string>(1);
            var frmName = requestData.GetData<string>(2);
            var location = NewDao<IMWDao>()
                .UpdateStorage(locationid, ids, frmName);
            responseData.AddData(location);
            return responseData;
        }
    }
}
