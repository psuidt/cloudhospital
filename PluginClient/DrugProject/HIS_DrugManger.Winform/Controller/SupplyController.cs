using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.BaseData;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManage.Winform.Controller
{
    /// <summary>
    /// 药品供应商维护控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmSupplyDW")]//与系统菜单对应
    [WinformView(Name = "FrmSupplyDW", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmSupply")]
    public class SupplyController : WcfClientController
    {
        /// <summary>
        /// 供应商对象
        /// </summary>
        IFrmSupply frmSupply;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public override void Init()
        {
            frmSupply = (IFrmSupply)DefaultView;
        }

        /// <summary>
        /// 获取供应商
        /// </summary>
        /// <param name="pageNO">分页页码</param>
        /// <param name="pageSize">分页数量</param>
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
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "SupplyController", "GetSupply", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmSupply.LoadSupply(dt, retdata.GetData<int>(1));
        }

        /// <summary>
        /// 删除供应商
        /// </summary>
        /// <param name="pageNO">分页页码</param>
        /// <param name="pageSize">分页数量</param>
        [WinformMethod]
        public void DeleteSupply(int pageNO, int pageSize)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmSupply.CurrentData);
            });
            InvokeWcfService("DrugProject.Service", "SupplyController", "DeleteSupply", requestAction);
            GetSupply(pageNO, pageSize);
            MessageBoxShowSimple("供应商信息删除成功");
        }

        /// <summary>
        /// 保存供应商
        /// </summary>
        /// <param name="pageNO">分页页码</param>
        /// <param name="pageSize">分页数量</param>
        [WinformMethod]
        public void SaveSupply(int pageNO, int pageSize)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmSupply.CurrentData);
                request.AddData(LoginUserInfo.WorkId);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "SupplyController", "SaveSupply", requestAction);
            DGBillResult result = retdata.GetData<DGBillResult>(0);
            if (result.Result == 0)
            {
                MessageBoxShowSimple("保存成功");
                GetSupply(pageNO, pageSize);
            }
            else
            {
                MessageBoxShowError(result.ErrMsg);
            }
        }
    }
}
