using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.BaseData;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.Controller
{
    /// <summary>
    /// 药品生产商维护控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmProduct")]//与系统菜单对应
    [WinformView(Name = "FrmProduct", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmProduct")]
    public class ProductController : WcfClientController
    {
        /// <summary>
        /// 生产商对象
        /// </summary>
        IFrmProduct frmProduct;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public override void Init()
        {
            frmProduct = (IFrmProduct)DefaultView;
        }

        /// <summary>
        /// 保存生产厂家
        /// </summary>
        [WinformMethod]
        public void SaveProduct()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmProduct.CurrentData);
                request.AddData(LoginUserInfo.WorkId);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ProductController", "SaveProduct", requestAction);
            DGBillResult k = retdata.GetData<DGBillResult>(0);
            if (k.Result == 0)
            {
                GetProduct();
                MessageBoxShowSimple("厂家信息保存成功");
            }
            else
            {
                MessageBoxShowSimple(k.ErrMsg);
            }
        }

        /// <summary>
        /// 获取生产厂家
        /// </summary>
        [WinformMethod]
        public void GetProduct()
        {
            //通过wcf服务调用bookWcfController控制器中的GetBooks方法
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmProduct.QueryCondition);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ProductController", "GetProductUserOr", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmProduct.LoadProduct(dt);
        }

        /// <summary>
        /// 删除生产厂家
        /// </summary>
        [WinformMethod]
        public void DeletePruduct()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmProduct.CurrentData);
            });
            InvokeWcfService("DrugProject.Service", "ProductController", "DeleteProduct", requestAction);

            GetProduct();
            MessageBoxShowSimple("厂家信息删除成功");
        }

        /// <summary>
        /// 读取药剂数据
        /// </summary>
        [WinformMethod]
        public void LoadDiseaseData()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ProductController", "GetProductUserOr");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmProduct.BindDisease_textboxcard(dt);
        }
    }
}
