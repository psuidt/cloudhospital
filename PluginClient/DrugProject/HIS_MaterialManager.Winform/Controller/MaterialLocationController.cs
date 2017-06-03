using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_Entity.MaterialManage;
using HIS_MaterialManage.Winform.IView.Report;
using HIS_MaterialManage.Winform.IView.StoreMgr;
using HIS_MaterialManage.Winform.ViewForm;

namespace HIS_MaterialManage.Winform.Controller
{
    /// <summary>
    /// 库位管理控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmLocation")]//在菜单上显示
    [WinformView(Name = "FrmLocation", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmLocation")]
    [WinformView(Name = "FrmLocationInfo", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmLocationInfo")]
    [WinformView(Name = "FrmMaterialTypeChoose", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmMaterialTypeChoose")]
    public class MaterialLocationController : WcfClientController
    {
        /// <summary>
        /// 物资库位管理接口
        /// </summary>
        IFrmLocation frmLocation;

        /// <summary>
        /// 库位信息管理接口
        /// </summary>
        IFrmLocationInfo frmLocationInfo;

        /// <summary>
        /// 物资类型选择接口
        /// </summary>
        IFrmMaterialTypeChoose iFrmMaterialTypeChoose;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            frmLocation = (IFrmLocation)iBaseView["FrmLocation"];
            frmLocationInfo = (IFrmLocationInfo)iBaseView["FrmLocationInfo"];
            iFrmMaterialTypeChoose = (IFrmMaterialTypeChoose)iBaseView["FrmMaterialTypeChoose"];
        }

        /// <summary>
        /// 获取库房信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDeptRoomData(string frmName)
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialLocationController", "GetDeptRoomData");
            frmLocation.BindDeptRoom(retdata.GetData<DataTable>(0), LoginUserInfo.DeptId);
        }

        /// <summary>
        /// 保存库位信息
        /// </summary>
        /// <param name="location">物资库位</param>
        [WinformMethod]
        public void SaveLocation(MW_Location location)
        {
            location.LocationID = frmLocation.LocationID;
            location.DeptID = frmLocation.DeptId;
            location.ParentID = frmLocation.ParentId;
            var retdata = InvokeWcfService(
            "DrugProject.Service",
            "MaterialLocationController",
            "SaveLocation",
             (request) =>
             {
                 request.AddData(location);
             });

            var result = retdata.GetData<bool>(0);
            frmLocationInfo.SaveComplete(result);
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
            "MaterialLocationController",
             "GetLocationList",
             (request) =>
             {
                 request.AddData(deptId);
             });

            var locationlist = retdata.GetData<List<MW_Location>>(0);
            frmLocation.GetLocationList(locationlist);
        }

        /// <summary>
        /// 根据库位ID获取库位信息
        /// </summary>
        [WinformMethod]
        public void GetLocationInfo()
        {
            int locationid = 0;
            locationid = frmLocation.LocationID;
            var retdata = InvokeWcfService(
                "DrugProject.Service",
                "MaterialLocationController",
                "GetLocationInfo",
                (request) =>
                {
                    request.AddData(locationid);
                });
            var locationinfo = retdata.GetData<MW_Location>(0);
            frmLocationInfo.GetLocationInfo(locationinfo);
        }

        /// <summary>
        /// 删除库位节点
        /// </summary>
        /// <param name="location">物资库位</param>
        /// <returns>操作结果消息</returns>
        [WinformMethod]
        public string DelLoacation(MW_Location location)
        {
            var retdata = InvokeWcfService(
            "DrugProject.Service",
            "MaterialLocationController",
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
        /// <param name="ids">物资ID</param>
        /// <param name="frmName">窗体入口</param>
        /// <returns>操作结果消息</returns>
        [WinformMethod]
        public string UpdateStorage(int locationid, string ids, string frmName)
        {
            var retdata = InvokeWcfService(
            "DrugProject.Service",
            "MaterialLocationController",
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
        /// 查询物资库存信息
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        /// <param name="typeId">类型ID</param>
        [WinformMethod]
        public void LoadMaterialStorage(string frmName, string typeId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmLocation.GetQueryCondition());
                request.AddData(typeId);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialLocationController", "LoadMaterialStorage", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            frmLocation.BindStoreGrid(dtRtn);
        }

        /// <summary>
        /// 获取物资类型
        /// </summary>
        [WinformMethod]
        public void GetMaterialTypeList()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(new Dictionary<string, string>());
            });

            ServiceResponseData retdata = InvokeWcfService(
                "DrugProject.Service",
                "MaterialTypeController", 
                "GetMaterialType",
                requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            iFrmMaterialTypeChoose.LoadMaterialType(dt);
        }

        /// <summary>
        /// 选择物资类型
        /// </summary>
        /// <param name="fatherFrmname">父窗体名称</param>
        [WinformMethod]
        public void OpenMaterialTypeDialog(string fatherFrmname)
        {
            var dialog = iBaseView["FrmMaterialTypeChoose"] as FrmMaterialTypeChoose;
            dialog.ShowDialog();
            if (dialog.Result == 1)
            {
                frmLocation.BindMaterialTypeTextBox(dialog.TypeId, dialog.TypeName);
            }
        }
    }
}
