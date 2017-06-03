using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.BaseData;

namespace HIS_DrugManage.Winform.Controller
{
    /// <summary>
    /// 药理分类维护控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmPharmacy")] //在菜单上显示
    [WinformView(Name = "FrmPharmacy", DllName = "HIS_DrugManage.Winform.dll",
        ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmPharmacy")] //控制器关联的界面
    public class PharmacyController : WcfClientController
    {
        /// <summary>
        /// 药理分类对象
        /// </summary>
        IFrmPharmacy frmAccount;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public override void Init()
        {
            frmAccount = (IFrmPharmacy)iBaseView["FrmPharmacy"];
        }

        /// <summary>
        /// 读取药理分类树节点数据
        /// </summary>
        [WinformMethod]
        public void LoadTree()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "PharmacyController", "GetAllPharmacy");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmAccount.BindTree(dt);
        }

        /// <summary>
        /// 读取网格数据
        /// </summary>
        [WinformMethod]
        public void LoadGrid()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmAccount.QueryCondition);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "PharmacyController", "GetGridByParentId", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmAccount.LoadGrid(dt);
        }

        /// <summary>
        /// 删除药理分类
        /// </summary>
        [WinformMethod]
        public void DeletePharmacy()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmAccount.CurrentData);
            });
            InvokeWcfService("DrugProject.Service", "PharmacyController", "DeletePharmacy", requestAction);
        }

        /// <summary>
        /// 保存药理分类
        /// </summary>
        /// <returns>返回结果</returns>
        [WinformMethod]
        public bool SavePharmacy()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmAccount.CurrentData);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "PharmacyController", "SavePharmacy", requestAction);
            int k = retdata.GetData<int>(0);
            bool flag = retdata.GetData<bool>(1);
            if (flag)
            {
                frmAccount.GetDG_PharmacologyKey(k); //返回药理ID
                MessageBoxShowSimple("保存成功");
            }
            else
            {
                MessageBoxShowError("存在相同记录");
            }

            return flag;
        }
    }
}
