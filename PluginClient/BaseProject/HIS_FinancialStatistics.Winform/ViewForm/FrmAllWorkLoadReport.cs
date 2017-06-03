using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_FinancialStatistics.Winform.IView;
using Microsoft.Reporting.WinForms;

namespace HIS_FinancialStatistics.Winform.ViewForm
{
    public partial class FrmAllWorkLoadReport : BaseFormBusiness, IFrmAllWorkLoadReport
    {
        /// <summary>
        /// 构造
        /// Initializes a new instance of the <see cref="FrmAllWorkLoadReport" /> class.
        /// </summary>
        public FrmAllWorkLoadReport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体打开事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmAllWorkLoadReport_OpenWindowBefore(object sender, EventArgs e)
        {
            this.Controls.Add(reportViewer1);
            reportViewer1.Dock = DockStyle.Fill;

            //设置成网络访问
            reportViewer1.ProcessingMode = ProcessingMode.Remote;

            //设置参数是否可见
            this.reportViewer1.ShowParameterPrompts = true;
            string ssrsWebAddress = (string)InvokeController("GetSsrsWebAddress");
            string ssrsUserName = (string)InvokeController("GetSsrsUserName");
            string ssrsPwd = (string)InvokeController("GetSsrsPWD");
            string reportPath = (string)InvokeController("GetWorkLoadReportPath");

            //设置报表Web服务地址
            reportViewer1.ServerReport.ReportServerUrl = new Uri(ssrsWebAddress);

            //设置用户权限
            ReportSCredentials reportSCredentials = new ReportSCredentials(ssrsUserName, ssrsPwd);
            reportViewer1.ServerReport.ReportServerCredentials.NetworkCredentials = reportSCredentials;

            //设置用户访问的报表路径，第一个/不可少
            reportViewer1.ServerReport.ReportPath = reportPath;

            //刷新显示报表
            this.reportViewer1.RefreshReport();
        }
    }
}
