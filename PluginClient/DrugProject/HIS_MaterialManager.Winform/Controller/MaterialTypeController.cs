using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_MaterialManage.Winform.IView.BaseData;

namespace HIS_MaterialManage.Winform.Controller
{
    /// <summary>
    /// 物资分类维护控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmMaterialType")]//与系统菜单对应                          HIS_MaterialManage.Winform.ViewForm
    [WinformView(Name = "FrmMaterialType", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmMaterialType")]
    public class MaterialTypeController : WcfClientController
    {
        /// <summary>
        /// 物资分类维护接口
        /// </summary>
        private IFrmMaterialType frmMaterialType;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            frmMaterialType = (IFrmMaterialType)DefaultView;
        }

        /// <summary>
        /// 获取物资分类
        /// </summary>
        [WinformMethod]
        public void GetMaterialType()
        {
            //通过wcf服务调用bookWcfController控制器中的GetBooks方法
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmMaterialType.QueryConditionP);
            });

            ServiceResponseData retdata = InvokeWcfService(
                "DrugProject.Service",
                "MaterialTypeController",
                "GetMaterialType",
                requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmMaterialType.LoadMaterialType(dt);
        }

        /// <summary>
        /// 保存物资分类
        /// </summary>
        [WinformMethod]
        public void SaveMaterialType()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmMaterialType.CurrentDataP);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialTypeController", "SaveMaterialType", requestAction);
            bool flag = retdata.GetData<bool>(0);
            if (flag)
            {
                GetMaterialType();
                MessageBoxShowSimple("保存成功");
            }
            else
            {
                MessageBoxShowError("存在相同记录");
            }
        }

        /// <summary>
        /// 获取物资类型列表
        /// </summary>
        [WinformMethod]
        public void GetChildMaterialType()
        {
            //通过wcf服务调用bookWcfController控制器中的GetBooks方法
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmMaterialType.QueryConditionC);
            });

            ServiceResponseData retdata = InvokeWcfService(
                "DrugProject.Service", 
                "MaterialTypeController",
                "GetChildMaterialType", 
                requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmMaterialType.LoadChildMaterialType(dt);
        }

        /// <summary>
        /// 保存物资类型
        /// </summary>
        [WinformMethod]
        public void SaveChildMaterialType()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmMaterialType.CurrentDataC);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialTypeController", "SaveChildMaterialType", requestAction);
            bool flag = retdata.GetData<bool>(0);
            if (flag)
            {
                GetChildMaterialType();
                MessageBoxShowSimple("保存成功");
            }
            else
            {
                MessageBoxShowError("存在相同记录");
            }
        }

        /// <summary>
        /// 删除物资类型
        /// </summary>
        [WinformMethod]
        public void DelChildMaterialType()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmMaterialType.CurrentDataC);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialTypeController", "DelChildMaterialType", requestAction);
            int result = retdata.GetData<int>(0);
            if (result > 0)
            {
                GetChildMaterialType();
                MessageBoxShowSimple("删除成功");
            }
            else
            {
                MessageBoxShowError("删除失败");
            }
        }
    }
}