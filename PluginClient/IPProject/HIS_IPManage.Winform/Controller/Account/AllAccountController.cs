using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EfwControls.Common;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.IPManage;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.Controller
{
    /// <summary>
    /// 缴款查询控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmAllAccount")]//与系统菜单对应
    [WinformView(Name = "FrmAllAccount", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmAllAccount")]
    [WinformView(Name = "FrmChargeDetail", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmChargeDetail")]
    [WinformView(Name = "FrmAccountDetail", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmAccountDetail")]
    public class AllAccountController : WcfClientController
    {
        /// <summary>
        /// 缴款查询接口
        /// </summary>
        IFrmAllAccount ifrmAllAccount;

        /// <summary>
        /// 缴款明细数据显示接口
        /// </summary>
        IChargeDetail iChargeDetail;

        /// <summary>
        /// 缴款明细接口
        /// </summary>
        IAccountDetail iAccountDetail;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            ifrmAllAccount = (IFrmAllAccount)iBaseView["FrmAllAccount"];
            iChargeDetail = (IChargeDetail)iBaseView["FrmChargeDetail"];
            iAccountDetail = (IAccountDetail)iBaseView["FrmAccountDetail"];
        }

        /// <summary>
        /// 获取收费员列表
        /// </summary>
        [WinformMethod]
        public void GetCashier()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AllAccountController", "GetCashier");
            DataTable dtCashier = retdata.GetData<DataTable>(0);
            ifrmAllAccount.LoadCashier(dtCashier);
        }

        /// <summary>
        /// 查询所有缴款记录
        /// </summary>
        /// <param name="bdate">缴款开始时间</param>
        /// <param name="edate">缴款结束时间</param>
        /// <param name="empid">操作员ID</param>
        /// <param name="status">缴款状态</param>
        [WinformMethod]
        public void QueryAllAccount(DateTime bdate, DateTime edate, int empid, int status)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(bdate);
                request.AddData(edate);
                request.AddData(empid);
                request.AddData(status);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AllAccountController", "QueryAllAccount", requestAction);
            DataTable dtAllAccount = retdata.GetData<DataTable>(0);
            Tools.ChangeDateTimeNullValue(ref dtAllAccount, new List<string> { "ReceivDate" });
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
            Tools.ChangeDateTimeNullValue(ref dtAllNotAccount, new List<string> { "LastDate" });
            if (dtAllNotAccount != null && dtAllNotAccount.Rows.Count > 0)
            {
                DataRow dr = dtAllNotAccount.NewRow();
                for (int i = 9; i < dtAllNotAccount.Columns.Count; i++)
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
        /// <param name="iStaffId">操作员ID</param>
        /// <param name="accountid">缴款ID</param>
        /// <param name="iAccountType">缴款类型（预交金/结算费用）</param>
        [WinformMethod]
        public void QueryAccountDetail(int iStaffId, int accountid, int iAccountType)
        {
            if (accountid > 0)
            {
                iAccountDetail.BindAccountInfo(iStaffId, accountid, iAccountType);
                var dialog = iBaseView["FrmAccountDetail"] as Form;
                dialog.ShowDialog();
            }
        }

        /// <summary>
        /// 收款
        /// </summary>
        /// <param name="accountid">缴款ID</param>
        /// <returns>true:收款成功/false:收款失败</returns>
        [WinformMethod]
        public bool ReciveAccount(string accountid)
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(accountid);
                    request.AddData(LoginUserInfo.EmpId);
                });
                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AllAccountController", "ReciveAccount", requestAction);
                MessageBoxShowSimple("收款成功");
                return true;
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
                return false;
            }
        }

        #region 弹窗用
        /// <summary>
        /// 获取每条缴款的发票等信息
        /// </summary>
        /// <param name="iStaffId">收费员ID</param>
        /// <param name="iAccountId">缴款ID</param>
        [WinformMethod]
        public void GetPayMentItem(int iStaffId, int iAccountId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(WorkId);
                request.AddData(iStaffId);
                request.AddData(iAccountId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "SinglePaymentManageController", "GetPayMentItem", requestAction);
            DataTable dtFPSum = retdata.GetData<DataTable>(0);
            DataTable dtFPClass = retdata.GetData<DataTable>(1);
            DataTable dtAccountClass = retdata.GetData<DataTable>(2);
            IP_Account curAccount = retdata.GetData<IP_Account>(3);

            // 绑定树菜单
            iAccountDetail.ShowPayMentItem(dtFPSum, dtFPClass, dtAccountClass);
        }

        /// <summary>
        /// 获取预交金数据
        /// </summary>
        /// <param name="iStaffId">待缴款数据ID</param>
        /// <param name="iAccountId">缴款ID</param>
        [WinformMethod]
        public void GetDepositList(int iStaffId, int iAccountId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(WorkId);
                request.AddData(iStaffId);
                request.AddData(iAccountId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "SinglePaymentManageController", "GetDepositList", requestAction);
            DataTable dtDepositList = retdata.GetData<DataTable>(0);

            // 绑定树菜单
            iAccountDetail.ShowDepositItem(dtDepositList);
        }

        /// <summary>
        /// 显示票据明细
        /// </summary>
        /// <param name="iAccountID">缴款ID</param>
        /// <param name="invoiceID">票据ID</param>
        /// <param name="invoiceType">票据类型</param>
        [WinformMethod]
        public void GetInvoiceDetail(int iAccountID, int invoiceID, int invoiceType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(WorkId);
                request.AddData(invoiceID);
                request.AddData(invoiceType);
                request.AddData(iAccountID);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "SinglePaymentManageController", "GetInvoiceDetail", requestAction);
            DataTable dtInvoiceData = retdata.GetData<DataTable>(0);
            DataRow dr = dtInvoiceData.NewRow();
            dr[0] = "合计";

            for (int i = 3; i < dtInvoiceData.Columns.Count; i++)
            {
                dr[dtInvoiceData.Columns[i].ColumnName] = dtInvoiceData.Compute("sum(" + dtInvoiceData.Columns[i].ColumnName + ")", string.Empty);
            }

            dtInvoiceData.Rows.Add(dr);
            iChargeDetail.BindDetailSource(dtInvoiceData);
            var dialog = iBaseView["FrmChargeDetail"] as Form;
            dialog.ShowDialog();
        }
        #endregion
    }
}