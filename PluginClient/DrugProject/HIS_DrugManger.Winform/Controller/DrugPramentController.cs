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
    /// 药品系统参数设置
    /// </summary>
    [WinformController(DefaultViewName = "FrmDrugPrament")]//在菜单上显示
    [WinformView(Name = "FrmDrugPrament", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmDrugPrament")]//药品系统参数设置
    public class DrugPramentController: WcfClientController
    {
        /// <summary>
        /// 药品系统参数
        /// </summary>
        IFrmDrugPrament frmAccount;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public override void Init()
        {
            frmAccount = (IFrmDrugPrament)iBaseView["FrmDrugPrament"];            
        }

        /// <summary>
        /// 取得启用的药剂科室
        /// </summary>
        [WinformMethod]
        public void GetUsedDrugDeptData()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugPramentController", "GetUsedDrugDeptData");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmAccount.BindDrugDeptList(dt);
        }

        /// <summary>
        /// 获取药品公共参数
        /// </summary>
        [WinformMethod]
        public void GetPublicParameters()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugPramentController", "GetPublicParameters");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmAccount.BindPublicParameters(dt);
        }

        /// <summary>
        /// 获取药剂科室参数
        /// </summary>
        /// <param name="deptId">科室Id</param>
        [WinformMethod]
        public void GetDeptParameters(int deptId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptId);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugPramentController", "GetDeptParameters", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmAccount.BindDeptParameters(dt);
        }

        /// <summary>
        /// 保存药品系统参数
        /// </summary>
        /// <param name="modelList">参数模型列表</param>
        [WinformMethod]
        public void SaveDrugParameters(List<Basic_SystemConfig> modelList)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(modelList);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugPramentController", "SaveDrugParameters", requestAction);
        }
    }
}
