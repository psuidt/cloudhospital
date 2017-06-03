using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.Controller
{
    /// <summary>
    /// 库位管理控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmLocation")]//在菜单上显示
    [WinformView(Name = "FrmLocation", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmLocation")]//库位管理-药房
    [WinformView(Name = "FrmLocationYK", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmLocation")]//库位管理-药库
    [WinformView(Name = "FrmLocationInfo", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmLocationInfo")]
    public class LocationController : WcfClientController
    {
        /// <summary>
        /// 药房库位对象
        /// </summary>
        IFrmLocation frmLocation;

        /// <summary>
        /// 库位详情对象
        /// </summary>
        IFrmLocationInfo frmLocationInfo;

        /// <summary>
        /// 药库库位对象
        /// </summary>
        IFrmLocation frmlocationyk;

        /// <summary>
        /// 当前窗体名称
        /// </summary>
        private string currentfrmName;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public override void Init()
        {
            frmLocation = (IFrmLocation)iBaseView["FrmLocation"];
            frmLocationInfo = (IFrmLocationInfo)iBaseView["FrmLocationInfo"];
            frmlocationyk = (IFrmLocation)iBaseView["FrmLocationYK"];
        }

        /// <summary>
        /// 药房药库库位切换
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void ChangeView(string frmName)
        {
            currentfrmName = frmName;
        }

        /// <summary>
        /// 保存库位信息
        /// </summary>
        /// <param name="location">库位对象</param>
        [WinformMethod]
        public void SaveLocation(DG_Location location)
        {
            if (currentfrmName == "FrmLocation")
            {
                location.LocationID = frmLocation.LocationID;
                location.DeptID = frmLocation.DeptId;
                location.ParentID = frmLocation.ParentId;
            }
            else
            {
                location.LocationID = frmlocationyk.LocationID;
                location.DeptID = frmlocationyk.DeptId;
                location.ParentID = frmlocationyk.ParentId;
            }

            var retdata = InvokeWcfService(
            "DrugProject.Service",
            "LocationController",
            "SaveLocation",
             (request) =>
             {
                 request.AddData(location);
             });
            var result = retdata.GetData<bool>(0);
            frmLocationInfo.SaveComplete(result);
        }

        /// <summary>
        /// 获取库房信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDeptRoomData(string frmName)
        {
            if (frmName == "FrmLocation")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(DGConstant.OP_DS_SYSTEM);
            });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "LocationController", "GetDeptRoomData", requestAction);
                frmLocation.BindDeptRoom(retdata.GetData<DataTable>(0), LoginUserInfo.DeptId);
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "LocationController", "GetDeptRoomData", requestAction);
                frmlocationyk.BindDeptRoom(retdata.GetData<DataTable>(0), LoginUserInfo.DeptId);
            }
        }

        /// <summary>
        ///  获取库位节点信息
        /// </summary>
        /// <param name="deptId">科室ID</param>
        [WinformMethod]
        public void GetLocationList(int deptId)
        {
            var retdata = InvokeWcfService(
            "DrugProject.Service",
            "LocationController",
             "GetLocationList",
             (request) =>
             {
                 request.AddData(deptId);
             });
            var locationlist = retdata.GetData<List<DG_Location>>(0);
            if (currentfrmName == "FrmLocation")
            {
                frmLocation.GetLocationList(locationlist);
            }
            else
            {
                frmlocationyk.GetLocationList(locationlist);
            }
        }

        /// <summary>
        /// 根据库位ID获取库位信息
        /// </summary>
        [WinformMethod]
        public void GetLocationInfo()
        {
            int locationid = 0;
            if (currentfrmName == "FrmLocation")
            {
                locationid = frmLocation.LocationID;
            }
            else
            {
                locationid = frmlocationyk.LocationID;
            }

            var retdata = InvokeWcfService(
        "DrugProject.Service",
        "LocationController",
         "GetLocationInfo",
         (request) =>
         {
             request.AddData(locationid);
         });
            var locationinfo = retdata.GetData<DG_Location>(0);
            frmLocationInfo.GetLocationInfo(locationinfo);
        }

        /// <summary>
        /// 删除库位节点
        /// </summary>
        /// <param name="location">库位对象</param>
        /// <returns>返回结果</returns>
        [WinformMethod]
        public string DelLoacation(DG_Location location)
        {
            var retdata = InvokeWcfService(
            "DrugProject.Service",
            "LocationController",
             "DelLoacation",
              (request) =>
              {
                  request.AddData(location);
              });
            var ret = retdata.GetData<string>(0);
            return ret;
        }

        /// <summary>
        /// 归库操作
        /// </summary>
        /// <param name="locationid">库位ID</param>
        /// <param name="ids">ID集</param>
        /// <param name="frmName">窗体名称</param>
        /// <returns>返回结果</returns>
        [WinformMethod]
        public string UpdateStorage(int locationid, string ids, string frmName)
        {
            var retdata = InvokeWcfService(
            "DrugProject.Service",
            "LocationController",
             "UpdateStorage",
              (request) =>
              {
                  request.AddData(locationid);
                  request.AddData(ids);
                  request.AddData(frmName);
              });
            var ret = retdata.GetData<string>(0);
            return ret;
        }

        /// <summary>
        /// 取得药品类型典
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDrugTypeDic(string frmName)
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "LocationController", "GetDrugTypeDic");
            DataTable dt = retdata.GetData<DataTable>(0);
            if (frmName == "FrmLocation")
            {
                frmLocation.BindTypeCombox(dt);
            }
            else
            {
                frmlocationyk.BindTypeCombox(dt);
            }
        }

        /// <summary>
        /// 取得药品剂型典，根据药品类型过滤
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="typeId">药品类型Id</param>
        [WinformMethod]
        public void GetDosageDic(string frmName, int typeId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(typeId);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "LocationController", "GetDosageDic", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            if (frmName == "FrmLocation")
            {
                frmLocation.BindDosageShowCard(dt);
            }
            else
            {
                frmlocationyk.BindDosageShowCard(dt);
            }
        }

        /// <summary>
        /// 查询药品库存信息
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void LoadDrugStorage(string frmName)
        {
            if (frmName == "FrmLocation")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(frmLocation.GetQueryCondition());
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "LocationController", "LoadDrugStorage", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmLocation.BindStoreGrid(dtRtn);
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(frmlocationyk.GetQueryCondition());
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "LocationController", "LoadDrugStorage", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmlocationyk.BindStoreGrid(dtRtn);
            }
        }

        /// <summary>
        /// 根据库位查询药品库存信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void LoadDrugStorageByLocation(string frmName)
        {
            if (frmName == "FrmLocation")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(frmLocation.GetQueryCondition());
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "LocationController", "LoadDrugStorageByLocation", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmLocation.BindStoreGrid(dtRtn);
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(frmlocationyk.GetQueryCondition());
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "LocationController", "LoadDrugStorageByLocation", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmlocationyk.BindStoreGrid(dtRtn);
            }
        }
    }
}
