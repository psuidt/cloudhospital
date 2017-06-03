using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using gregn6Lib;
using HIS_FinancialStatistics.Winform.IView;

namespace HIS_FinancialStatistics.Winform.ViewForm
{
    public partial class FrmFinacialIPDoctor : BaseFormBusiness, IFrmFinacialIPDoctor
    {
        /// <summary>
        /// 构造
        /// Initializes a new instance of the<see cref="FrmFinacialIPDoctor" /> class.
        /// </summary>
        public FrmFinacialIPDoctor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置组织机构
        /// </summary>
        /// <param name="dtWorker">组织机构信息</param>
        public void SetWorkers(DataTable dtWorker)
        {
            cbbWork.DisplayMember = "WorkName";
            cbbWork.ValueMember = "WorkId";
            cbbWork.DataSource = dtWorker;
        }

        /// <summary>
        /// 绑定医生类型
        /// </summary>
        public void BindDocType()
        {
            var datasource = new[] 
            {
                new { Text = "开方医生", Value = "PresDoctorID" },
                new { Text = "主治医生", Value = "CurrDoctorID" },
            };
            cbbDocType.ValueMember = "Value";
            cbbDocType.DisplayMember = "Text";
            cbbDocType.DataSource = datasource;
        }

        /// <summary>
        /// 绑定事件类型
        /// </summary>
        public void BindTimeType()
        {
            var datasource = new[]
            {
                new { Text = "结算时间", Value = "CostDate" },
                new { Text = "缴款时间", Value = "AccountDate" },
                new { Text = "记账时间", Value = "ChargeDate" },
            };
            cbbTimeType.ValueMember = "Value";
            cbbTimeType.DisplayMember = "Text";
            cbbTimeType.DataSource = datasource;
        }

        /// <summary>
        /// 窗体打开事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FemFinacialIPDoctor_OpenWindowBefore(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);//当前月第一天
            DateTime d2 = d1.AddMonths(1).AddDays(-1);//当前月最后一天
            dtTimer.Bdate.Value = d1;
            dtTimer.Edate.Value = d2;
            BindTimeType();
            BindDocType();
            DataTable dt = (DataTable)InvokeController("GetWorker");
            SetWorkers(dt);
        }

        /// <summary>
        /// 停用
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtnStop_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmFinacialIPDoctor_FormClosed(object sender, FormClosedEventArgs e)
        {
            GridViewResult.Stop();
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
                DateTime bdate = dtTimer.Bdate.Value;
                DateTime edate = dtTimer.Edate.Value;
                int queryWorkId = Convert.ToInt32(cbbWork.SelectedValue);
                string queryTimeType = Convert.ToString(cbbTimeType.SelectedValue);
                string qyeryDocType = Convert.ToString(cbbDocType.SelectedValue);
                string currentUserName = (string)InvokeController("GetCurrentUserName");
                DataTable dtReport = (DataTable)InvokeController("QueryInpatientWorkLoad", queryWorkId, bdate.ToString("yyyy-MM-dd 00:00:00"), edate.ToString("yyyy-MM-dd 23:59:59"), qyeryDocType, queryTimeType);
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                myDictionary.Add("Title", cbbWork.Text.Trim() + "住院医生工作量统计");
                myDictionary.Add("DateRange", dtTimer.Bdate.Value.ToString("yyyy-MM-dd") + "至" + dtTimer.Edate.Value.ToString("yyyy-MM-dd"));
                myDictionary.Add("Printer", currentUserName);
                myDictionary.Add("DocType", "医生类型:" + cbbDocType.Text);
                myDictionary.Add("TimeType", "时间类型:" + cbbTimeType.Text);
                GridReport gridreport = new GridReport();               
                gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 3011, 0, myDictionary, dtReport);
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
    }
}
