using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_MaterialManage.Winform.IView.Report;
using HIS_MaterialManage.Winform.IView.StoreMgr;
using HIS_MaterialManage.Winform.ViewForm;

namespace HIS_MaterialManage.Winform.Controller
{
    /// <summary>
    /// 物资库存维护控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmStoreQuery")]//在菜单上显示
    [WinformView(Name = "FrmStoreQuery", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmStoreQuery")]//
    [WinformView(Name = "FrmMaterialTypeChoose", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmMaterialTypeChoose")]
    public class MaterialQueryStoreController : WcfClientController
    {
        /// <summary>
        /// 物资库存接口
        /// </summary>
        IFrmStoreQuery frmStoreQuery;

        /// <summary>
        /// 物资类型选择接口
        /// </summary>
        IFrmMaterialTypeChoose iFrmMaterialTypeChoose;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            frmStoreQuery = (IFrmStoreQuery)iBaseView["FrmStoreQuery"];
            iFrmMaterialTypeChoose = (IFrmMaterialTypeChoose)iBaseView["FrmMaterialTypeChoose"];
        }

        /// <summary>
        /// 获取库房信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDeptRoomData(string frmName)
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialQueryStoreController", "GetDeptRoomData");
            frmStoreQuery.BindDeptRoom(retdata.GetData<DataTable>(0), LoginUserInfo.DeptId);
        }

        /// <summary>
        /// 查询物资库存信息
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        /// <param name="typeId">科室ID</param>
        [WinformMethod]
        public void LoadMaterialStorage(string frmName, string typeId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmStoreQuery.GetQueryCondition());
                request.AddData(typeId);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialQueryStoreController", "LoadMaterialStorage", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            frmStoreQuery.BindStoreGrid(dtRtn);
        }

        /// <summary>
        /// 查询药品批次信息
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void LoadMaterialBatch(string frmName)
        {
            int storageID = frmStoreQuery.GetStorageID();
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(storageID);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialQueryStoreController", "LoadMaterialBatch", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            frmStoreQuery.BindStoreBatchGrid(dtRtn);
        }

        /// <summary>
        /// 打印库存单
        /// </summary>
        /// <param name="deptName">部门名称</param>
        /// <param name="dgStore">库存数据</param>
        [WinformMethod]
        public void PrintMaterialStore(string deptName, DataTable dgStore)
        {
            if (dgStore.Rows.Count > 0)
            {
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                myDictionary.Add("HospitalName", LoginUserInfo.WorkName);
                myDictionary.Add("DeptName", deptName);
                myDictionary.Add("Printer", LoginUserInfo.EmpName);
                myDictionary.Add("PrintTime", DateTime.Now);
                EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 4001, 0, myDictionary, dgStore).PrintPreview(true);
            }
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
                frmStoreQuery.BindMaterialTypeTextBox(dialog.TypeId, dialog.TypeName);
            }
        }
    }
}
