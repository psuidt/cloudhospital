using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_FinancialStatistics.Winform.IView;

namespace HIS_FinancialStatistics.Winform.ViewForm
{
    public partial class FrmFinacialCharger : BaseFormBusiness, IFrmFinacialCharger
    {
        /// <summary>
        /// 构造
        /// Initializes a new instance of the<see cref="FrmFinacialCharger" /> class.
        /// </summary>
        public FrmFinacialCharger()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置组织机构
        /// </summary>
        /// <param name="dtWorker">组织机构数据</param>
        public void SetWorkers(DataTable dtWorker)
        {
            cmbWorker.DisplayMember = "WorkName";
            cmbWorker.ValueMember = "WorkId";
            cmbWorker.DataSource = dtWorker;
        }

        /// <summary>
        /// 窗体打开事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmFinacialCharger_OpenWindowBefore(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);//当前月第一天
            DateTime d2 = d1.AddMonths(1).AddDays(-1);//当前月最后一天
            sdtDate.Bdate.Value = d1;
            sdtDate.Edate.Value = d2;
            cmbQueryType.SelectedIndex = 0;
            DataTable dt = (DataTable)InvokeController("GetWorker");
            SetWorkers(dt);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewResult.Stop();
                this.Cursor = Cursors.WaitCursor;
                DateTime bdate = Convert.ToDateTime(sdtDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00"));
                DateTime edate = Convert.ToDateTime(sdtDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59"));
                int queryWorkId = Convert.ToInt32(cmbWorker.SelectedValue);
                int queryType = cmbQueryType.SelectedIndex;//0门诊 1住院
                string currentUserName = (string)InvokeController("GetCurrentUserName");
                DataTable dtReport = (DataTable)InvokeController("GetFinacialChargerData", frmName, bdate, edate, queryWorkId, queryType);
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                string str = queryType == 0 ? "门诊" : "住院";
                myDictionary.Add("Title", cmbWorker.Text.Trim() + str + "收费员工作量统计");
                myDictionary.Add("DateRange", sdtDate.Bdate.Value.ToString("yyyy-MM-dd") + "至" + sdtDate.Edate.Value.ToString("yyyy-MM-dd"));
                myDictionary.Add("Printer", currentUserName);
                GridReport gridreport = new GridReport();
                if (queryType == 0)
                {
                    gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 2004, 0, myDictionary, dtReport);
                }
                else
                {
                    gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 3010, 0, myDictionary, dtReport);
                }

                GridViewResult.Report = gridreport.Report;
                GridViewResult.Start();
                GridViewResult.Refresh();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewResult.PostColumnLayout();
                GridViewResult.Report.PrintPreview(true);
            }
            catch (Exception error)
            {
                MessageBoxEx.Show(error.Message);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmFinacialCharger_FormClosed(object sender, FormClosedEventArgs e)
        {
            GridViewResult.Stop();
        }
    }
}
