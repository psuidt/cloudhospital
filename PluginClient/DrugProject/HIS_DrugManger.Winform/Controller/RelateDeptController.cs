using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.BaseData;
using HIS_Entity.BasicData;

namespace HIS_DrugManage.Winform.Controller
{
    /// <summary>
    /// 往来科室维护控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmRelateDept")]//在菜单上显示
    [WinformView(Name = "FrmRelateDept", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmRelateDept")]//往来科室设置-药房
    [WinformView(Name = "FrmRelateDeptYK", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmRelateDept")]//往来科室设置-药库
    public class RelateDeptController : WcfClientController
    {
        /// <summary>
        /// 往来科室对象
        /// </summary>
        IFrmRelateDept frmAccount;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public override void Init()
        {
            frmAccount = (IFrmRelateDept)iBaseView["FrmRelateDept"];
        }

        /// <summary>
        /// 改变窗体
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void ChangeView(string frmName)
        {
            if (frmName == "FrmRelateDept")
            {
                frmAccount= (IFrmRelateDept)iBaseView["FrmRelateDept"];
            }
            else if (frmName == "FrmRelateDeptYK")
            {
                frmAccount = (IFrmRelateDept)iBaseView["FrmRelateDeptYK"];
            }
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
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "RelateDeptController", "BatchSaveRelateDept", requestAction);
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
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "RelateDeptController", "DeleteRelateDept", requestAction);
        } 

        /// <summary>
        /// 获取所有科室数据
        /// </summary>
        [WinformMethod]
        public void GetAllDeptData()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "RelateDeptController", "GetAllDeptData");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmAccount.BindDept(dt);
        }

        /// <summary>
        /// 取得库房数据
        /// </summary>
        /// <param name="menuTypeFlag">菜单类型0药房往来科室维护，1药库往来科室维护</param>
        [WinformMethod]
        public void GetStoreRoomData(int menuTypeFlag)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(menuTypeFlag);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "RelateDeptController", "GetStoreRoomData", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmAccount.BindStoreRoom(dt);
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
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "RelateDeptController", "GetRelateDeptData", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmAccount.BindRelateDeptGrid(dt);
            frmAccount.BindDeptType();
        }

        /// <summary>
        /// 获取全部科室树形数据
        /// </summary>
        [WinformMethod]
        public void LoadDeptTreeData()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "RelateDeptController", "LoadDeptTreeData");
            List<BaseDeptLayer> layerlist = retdata.GetData<List<BaseDeptLayer>>(0);
            List<BaseDept> deptlist = retdata.GetData<List<BaseDept>>(1);
            frmAccount.LoadDeptTree(layerlist, deptlist, LoginUserInfo.DeptId);
        }
    }   
}
