using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.Controller
{
    [WinformController(DefaultViewName = "FrmAllAccount")]//与系统菜单对应
    [WinformView(Name = "FrmAllAccount", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmAllAccount")]
  
    /// <summary>
    /// 缴款查询控制器类
    /// </summary>
    public class AllAccountController : WcfClientController
    {
        /// <summary>
        /// 界面接口类
        /// </summary>
        IFrmAllAccount ifrmAllAccount; 
           
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            ifrmAllAccount = (IFrmAllAccount)iBaseView["FrmAllAccount"];           
        }

        /// <summary>
        /// 获取收费员列表
        /// </summary>
        [WinformMethod]
        public void GetCashier()
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "AccountController", "GetCashier");
            DataTable dtCashier = retdata.GetData<DataTable>(0);
            ifrmAllAccount.loadCashier(dtCashier);
        }

        /// <summary>
        /// 查询所有缴款记录
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="empid">人员ID</param>
        /// <param name="status">0未收款1已经收款2全部</param>
        [WinformMethod]
        public void QueryAllAccount(DateTime bdate, DateTime edate,int empid,int status)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(bdate);
                request.AddData(edate);
                request.AddData(empid);
                request.AddData(status);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "AccountController", "QueryAllAccount", requestAction);
            DataTable dtAllAccount = retdata.GetData<DataTable>(0);
            if (dtAllAccount != null && dtAllAccount.Rows.Count > 0)
            {
                DataRow dr = dtAllAccount.NewRow();
                dr[2] = "合计";
                for (int i = 12; i < dtAllAccount.Columns.Count; i++)
                {
                    dr[dtAllAccount.Columns[i].ColumnName] = dtAllAccount.Compute("sum(" + dtAllAccount.Columns[i].ColumnName + ")", string.Empty);
                }

                dtAllAccount.Rows.Add(dr);
            }

            DataTable dtAllNotAccount = retdata.GetData<DataTable>(1);
            if (dtAllNotAccount != null && dtAllNotAccount.Rows.Count > 0)
            {
                DataRow dr = dtAllNotAccount.NewRow();
                for (int i = 8; i < dtAllNotAccount.Columns.Count; i++)
                {
                    dr[dtAllNotAccount.Columns[i].ColumnName] = dtAllNotAccount.Compute("sum(" + dtAllNotAccount.Columns[i].ColumnName + ")", string.Empty);
                }

                dr[1] = "合计";
                dtAllNotAccount.Rows.Add(dr);
            }

            ifrmAllAccount.BindQueryData(dtAllAccount, dtAllNotAccount);
        }

        /// <summary>
        /// 查询缴款明细，调用个人缴款页面
        /// </summary>
        /// <param name="accountid">缴款ID</param>
        [WinformMethod]
        public void QueryAccountDetail(int accountid)
        {
            if (accountid > 0)
            {
                InvokeController("OPProject.UI", "AccountController", "ShowAccount", accountid);
            }
        }

        /// <summary>
        /// 收款
        /// </summary>
        /// <param name="accountid">缴款ID</param>
        /// <returns>bool true成功false失败</returns>
        [WinformMethod]
        public bool ReciveAccount(string  accountid)
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(accountid);
                    request.AddData(LoginUserInfo.EmpId);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "AccountController", "ReciveAccount", requestAction);
                MessageBoxShowSimple("收款成功");
                return true;
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
                return false;
            }
        }
    }
}
