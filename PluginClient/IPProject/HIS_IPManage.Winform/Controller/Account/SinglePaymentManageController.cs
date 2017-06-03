using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.IPManage;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.Controller
{
    /// <summary>
    /// 交款管理控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmSinglePaymentManage")]
    [WinformView(Name = "FrmSinglePaymentManage", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmSinglePaymentManage")]
    [WinformView(Name = "FrmChargeDetail", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmChargeDetail")]
    public class SinglePaymentManageController : WcfClientController
    {
        /// <summary>
        /// 费用清单接口
        /// </summary>
        private ISinglePaymentManage iSinglePaymentManage;

        /// <summary>
        /// 缴款明细数据显示接口
        /// </summary>
        private IChargeDetail iChargeDetail;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            iSinglePaymentManage = (ISinglePaymentManage)DefaultView;
            iChargeDetail = (IChargeDetail)iBaseView["FrmChargeDetail"];
        }

        /// <summary>
        /// 获取收费员列表
        /// </summary>
        [WinformMethod]
        public void GetStaff()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "SinglePaymentManageController", "GetStaff");
            DataTable dtStaff = retdata.GetData<DataTable>(0);

            if (dtStaff != null)
            {
                // 绑定树菜单
                iSinglePaymentManage.BindStaffInfo(dtStaff);
            }
        }

        /// <summary>
        /// 获取交款记录
        /// </summary>
        /// <param name="sBDate">开始时间</param>
        /// <param name="sEDate">结束时间</param>
        /// <param name="iEmpId">收费员ID 0 为所有</param>
        [WinformMethod]
        public void GetPayInfoList(string sBDate, string sEDate, int iEmpId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(WorkId);
                request.AddData(sBDate);
                request.AddData(sEDate);
                request.AddData(iEmpId);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "SinglePaymentManageController", "GetAccountList", requestAction);
            DataTable dtUnUpload = retdata.GetData<DataTable>(0);
            DataTable dtUploaded = retdata.GetData<DataTable>(1);

            if (dtUnUpload != null || dtUploaded != null)
            {
                // 绑定树菜单
                iSinglePaymentManage.BindPayInfoList(dtUnUpload, dtUploaded);
            }
        }

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
            iSinglePaymentManage.CurAccount = retdata.GetData<IP_Account>(3);
            
            // 绑定树菜单
            iSinglePaymentManage.ShowPayMentItem(dtFPSum, dtFPClass, dtAccountClass);
        }

        /// <summary>
        /// 获取预交金数据
        /// </summary>
        /// <param name="iStaffId">待缴款ID</param>
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
            iSinglePaymentManage.CurAccount = retdata.GetData<IP_Account>(1);

            // 绑定树菜单
            iSinglePaymentManage.ShowDepositItem(dtDepositList);
        }

        /// <summary>
        /// 执行缴款
        /// </summary>
        /// <param name="iStaffId">待缴款记录ID</param>
        /// <param name="iAccountType">结算类型0预交金1结算</param>
        /// <param name="dTotalFee">此次缴款总额</param>
        /// <param name="dTotalPaymentFee">实际缴款现金总额</param>
        /// <returns>true：缴款成功/false：缴款失败</returns>
        [WinformMethod]
        public bool DoAccountPayment(int iStaffId, int iAccountType, decimal dTotalFee, decimal dTotalPaymentFee)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(WorkId);
                request.AddData(iStaffId);
                request.AddData(iAccountType);
                request.AddData(dTotalFee);
                request.AddData(dTotalPaymentFee);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "SinglePaymentManageController", "DoAccountPayment", requestAction);
            bool b = retdata.GetData<bool>(0);

            if (b)
            {
                IP_Account account = retdata.GetData<IP_Account>(1);
                iSinglePaymentManage.CurAccount = account;
            }
            else
            {
                string s = retdata.GetData<string>(1);
                MessageBoxShowError(s);
            }

            return b;
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

        /// <summary>
        /// 缴款单打印
        /// </summary>
        /// <param name="myDictionary">待打印缴款数据</param>
        [WinformMethod]
        public void AccountPrint(Dictionary<string, object> myDictionary)
        {
            EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 3001, 0, myDictionary, null).PrintPreview(true);
        }

        /// <summary>
        /// 预交金缴款打印
        /// </summary>
        /// <param name="myDictionary">缴款报表数据</param>
        /// <param name="dtDepositList">预交金详细数据</param>
        [WinformMethod]
        public void DepositPrint(Dictionary<string, object> myDictionary, DataTable dtDepositList)
        {
            EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 3002, 0, myDictionary, dtDepositList).PrintPreview(true);
        }

        #region "共用方法"

        /// <summary>
        /// 提示消息
        /// </summary>
        /// <param name="msg">Msg内容</param>
        [WinformMethod]
        public void MessageShow(string msg)
        {
            MessageBoxShowSimple(msg);
        }

        #endregion
    }
}
