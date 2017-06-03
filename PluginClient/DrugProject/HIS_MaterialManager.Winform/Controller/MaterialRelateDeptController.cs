using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.BasicData;
using HIS_MaterialManage.Winform.IView.BaseData;

namespace HIS_MaterialManage.Winform.Controller
{
    /// <summary>
    /// 往来科室维护控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmMaterialRelateDept")]//在菜单上显示
    [WinformView(Name = "FrmMaterialRelateDept", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmMaterialRelateDept")]//往来科室设置    
    public class MaterialRelateDeptController : WcfClientController
    {
        /// <summary>
        /// 往来科室维护接口
        /// </summary>
        IFrmMaterialRelateDept frmMaterialRelateDept;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            frmMaterialRelateDept = (IFrmMaterialRelateDept)iBaseView["FrmMaterialRelateDept"];
        }
        
        /// <summary>
        /// 保存往来科室数据
        /// </summary>
        /// <param name="dtSave">往来科室数据集</param>
        [WinformMethod]
        public void BatchSaveRelateDept(DataTable dtSave)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(dtSave);
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialRelateDeptController", "BatchSaveRelateDept", requestAction);
        }

        /// <summary>
        /// 删除科室
        /// </summary>
        /// <param name="drugDeptID">库房Id</param>
        /// <param name="relationDeptID">往来科室Id</param>
        [WinformMethod]
        public void DeleteRelateDept(int drugDeptID, int relationDeptID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(drugDeptID);
                request.AddData(relationDeptID);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialRelateDeptController", "DeleteRelateDept", requestAction);
        }

        /// <summary>
        /// 获取所有科室数据
        /// </summary>
        [WinformMethod]
        public void GetAllDeptData()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialRelateDeptController", "GetAllDeptData");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmMaterialRelateDept.BindDept(dt);
        }

        /// <summary>
        /// 取得库房数据
        /// </summary>
        [WinformMethod]
        public void GetStoreRoomData()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(2);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialRelateDeptController", "GetStoreRoomData", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmMaterialRelateDept.BindStoreRoom(dt);
        }

        /// <summary>
        /// 根据科室Id获取往来科室数据
        /// </summary>
        /// <param name="deptId">库房id</param>
        [WinformMethod]
        public void GetRelateDeptData(int deptId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptId);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialRelateDeptController", "GetRelateDeptData", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmMaterialRelateDept.BindRelateDeptGrid(dt);
            frmMaterialRelateDept.BindDeptType();
        }

        /// <summary>
        /// 获取全部科室树形数据
        /// </summary>
        [WinformMethod]
        public void LoadDeptTreeData()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialRelateDeptController", "LoadDeptTreeData");
            List<BaseDeptLayer> layerlist = retdata.GetData<List<BaseDeptLayer>>(0);
            List<BaseDept> deptlist = retdata.GetData<List<BaseDept>>(1);           
            frmMaterialRelateDept.LoadDeptTree(layerlist, deptlist, LoginUserInfo.DeptId);
        }
    }
}
