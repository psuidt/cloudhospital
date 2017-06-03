using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.SqlAly;
using HIS_MaterialManage.Winform.IView.BaseData;

namespace HIS_MaterialManage.Winform.Controller
{
    /// <summary>
    /// 物资供应商
    /// </summary>
    [WinformController(DefaultViewName = "FrmMaterialSupply")]//与系统菜单对应                       HIS_DrugManage.Winform.ViewForm
    [WinformView(Name = "FrmMaterialSupply", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmMaterialSupply")]
    public class MaterialSupplyController : WcfClientController
    {
        /// <summary>
        /// 物资供应商维护接口
        /// </summary>
        IFrmMaterialSupply frmSupply;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            frmSupply = (IFrmMaterialSupply)DefaultView;
        }

        /// <summary>
        /// 获取供应商集合
        /// </summary>
        /// <param name="pageNO">页码</param>
        /// <param name="pageSize">总页数</param>
        [WinformMethod]
        public void GetSupply(int pageNO, int pageSize)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(pageNO);
                request.AddData(pageSize);
                frmSupply.AndWhere.Add(Tuple.Create("WorkID", LoginUserInfo.WorkId.ToString(), SqlOperator.Equal));
                request.AddData(frmSupply.AndWhere);
                request.AddData(frmSupply.OrWhere);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialSupplyController", "GetSupply", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmSupply.LoadSupply(dt, retdata.GetData<int>(1));
        }

        /// <summary>
        /// 删除供应商
        /// </summary>
        /// <param name="pageNO">页码</param>
        /// <param name="pageSize">总页数</param>
        [WinformMethod]
        public void DeleteSupply(int pageNO, int pageSize)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmSupply.CurrentData);
            });

            InvokeWcfService("DrugProject.Service", "MaterialSupplyController", "DeleteSupply", requestAction);
            GetSupply(pageNO, pageSize);
            MessageBoxShowSimple("供应商信息删除成功");
        }

        /// <summary>
        /// 保存供应商信息
        /// </summary>
        /// <param name="pageNO">页码</param>
        /// <param name="pageSize">总页数</param>
        [WinformMethod]
        public void SaveSupply(int pageNO, int pageSize)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmSupply.CurrentData);
                request.AddData(LoginUserInfo.WorkId);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialSupplyController", "SaveSupply", requestAction);
            bool flag = retdata.GetData<bool>(0);
            if (flag)
            {
                MessageBoxShowSimple("保存成功");
                GetSupply(pageNO, pageSize);
            }
            else
            {
                MessageBoxShowError("存在相同记录");
            }
        }
    }
}
