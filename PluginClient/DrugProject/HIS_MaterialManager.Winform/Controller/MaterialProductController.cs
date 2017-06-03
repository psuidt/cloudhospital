using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_MaterialManage.Winform.IView.BaseData;

namespace HIS_MaterialManage.Winform.Controller
{
    /// <summary>
    /// 生产厂家
    /// </summary>
    [WinformController(DefaultViewName = "FrmMaterialProduct")]//与系统菜单对应                     
    [WinformView(Name = "FrmMaterialProduct", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmMaterialProduct")]
    
    public class MaterialProductController: WcfClientController
    {
        /// <summary>
        /// 生产厂家维护接口
        /// </summary>
        IFrmMaterialProduct frmProduct;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            frmProduct = (IFrmMaterialProduct)DefaultView;
        }

        /// <summary>
        /// 保存厂家信息
        /// </summary>
        [WinformMethod]
        public void SaveProduct()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmProduct.CurrentData);
                request.AddData(LoginUserInfo.WorkId);
            });

            //通过wcf服务调用WcfController控制器中的Save方法，并传递参数Book对象
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialProductController", "SaveProduct", requestAction);
            bool flag = retdata.GetData<bool>(0);
            if (flag)
            {
                MessageBoxShowSimple("厂家信息保存成功");
                GetProduct();
            }
            else
            {
                MessageBoxShowError("存在相同记录");
            }
        }

        /// <summary>
        /// 获取厂家信息
        /// </summary>
        [WinformMethod]
        public void GetProduct()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmProduct.QueryCondition);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialProductController", "GetProductUserOr", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmProduct.LoadProduct(dt);
        }

        /// <summary>
        /// 删除厂家信息
        /// </summary>
        [WinformMethod]
        public void DeletePruduct()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmProduct.CurrentData);
            });

            InvokeWcfService("DrugProject.Service", "MaterialProductController", "DeleteProduct", requestAction);
            GetProduct();
            MessageBoxShowSimple("厂家信息删除成功");
        }
    }
}
