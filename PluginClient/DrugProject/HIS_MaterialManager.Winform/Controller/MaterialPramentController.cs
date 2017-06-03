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
    /// 物资系统参数设置
    /// </summary>
    [WinformController(DefaultViewName = "FrmMaterialPrament")]//在菜单上显示
    [WinformView(Name = "FrmMaterialPrament", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmMaterialPrament")]//物资系统参数设置
    public class MaterialPramentController: WcfClientController
    {
        /// <summary>
        /// 物资系统参数设置接口
        /// </summary>
        IFrmMaterialPrament frmMaterialPrament;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            frmMaterialPrament = (IFrmMaterialPrament)iBaseView["FrmMaterialPrament"];            
        }

        /// <summary>
        /// 取得启用的科室
        /// </summary>
        [WinformMethod]
        public void GetUsedDeptData()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialPramentController", "GetUsedDeptData");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmMaterialPrament.BindDrugDeptList(dt);
        }

        /// <summary>
        /// 获取物资公共参数
        /// </summary>
        [WinformMethod]
        public void GetPublicParameters()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialPramentController", "GetPublicParameters");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmMaterialPrament.BindPublicParameters(dt);
        }

        /// <summary>
        /// 获取科室参数
        /// </summary>
        /// <param name="deptId">科室Id</param>
        [WinformMethod]
        public void GetDeptParameters(int deptId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptId);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialPramentController", "GetDeptParameters", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmMaterialPrament.BindDeptParameters(dt);
        }

        /// <summary>
        /// 保存物资系统参数
        /// </summary>
        /// <param name="modelList">参数模型列表</param>
        [WinformMethod]
        public void SaveParameters(List<Basic_SystemConfig> modelList)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(modelList);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialPramentController", "SaveParameters", requestAction);
        }
    }
}
