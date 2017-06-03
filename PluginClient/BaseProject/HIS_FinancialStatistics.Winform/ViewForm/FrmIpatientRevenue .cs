using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using gregn6Lib;
using HIS_FinancialStatistics.Winform.IView;

namespace HIS_FinancialStatistics.Winform.ViewForm
{
    public partial class FrmIpatientRevenue : BaseFormBusiness, IFrmIpatientRevenue
    {
        /// <summary>
        /// 报表控件
        /// </summary>
        GridReport gridreport;

        /// <summary>
        /// 构造
        /// Initializes a new instance of the<see cref="FrmIpatientRevenue" /> class.
        /// </summary>
        public FrmIpatientRevenue()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置组织机构
        /// </summary>
        /// <param name="dtWorker">组织机构信息</param>
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
        private void FrmIpatientRevenue_OpenWindowBefore(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);//当前月第一天
            DateTime d2 = d1.AddMonths(1).AddDays(-1);//当前月最后一天
            sdtDate.Bdate.Value = d1;
            sdtDate.Edate.Value = d2;
            cmbTimeType.SelectedIndex = 0;
            cmbRowGroupType.SelectedIndex = 0;
            cmbColGroupType.SelectedIndex = 0;
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
                int queryTimeType = cmbTimeType.SelectedIndex;//0记费时间 1结算时间 2缴款时间
                int rowGroupType = cmbRowGroupType.SelectedIndex;//0开方医生 1开方科室 2执行科室 3主治医生 4病人类型 5病人所在科室
                int colGroupType = cmbColGroupType.SelectedIndex;//0统计大项目 1核算分类 2财务分类 3住院发票分类 4支付方式
                string currentUserName = (string)InvokeController("GetCurrentUserName");
                DataTable dtReport = (DataTable)InvokeController("GetFinacialIPRevenueData", frmName, bdate, edate, queryWorkId, queryTimeType, rowGroupType, colGroupType);
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                myDictionary.Add("Title", cmbWorker.Text.Trim() + "住院收入统计");
                myDictionary.Add("DateRange", sdtDate.Bdate.Value.ToString("yyyy-MM-dd") + "至" + sdtDate.Edate.Value.ToString("yyyy-MM-dd"));
                myDictionary.Add("Printer", currentUserName);           
                gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 3014, 0, myDictionary, dtReport);
                gridreport.Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(ReportInitialize);
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
        /// 初始化报表
        /// </summary>
        private void ReportInitialize()
        {
            //0开方医生 1开方科室 2执行科室 3主治医生 4病人类型 5病人所在科室
            gridreport.Report.ColumnByName("PresDoctorName").Visible = false;
            gridreport.Report.ColumnByName("PresDeptName").Visible = false;
            gridreport.Report.ColumnByName("ExecDeptName").Visible = false;
            gridreport.Report.ColumnByName("PatTypeName").Visible = false;
            gridreport.Report.ColumnByName("PatDoctorName").Visible = false;
            gridreport.Report.ColumnByName("PatDeptName").Visible = false;
            int rowGroupType = cmbRowGroupType.SelectedIndex;
            if (rowGroupType == 0)
            {
                gridreport.Report.ColumnByName("PresDoctorName").Visible = true;
            }
            else if (rowGroupType == 1)
            {
                gridreport.Report.ColumnByName("PresDeptName").Visible = true;
            }
            else if (rowGroupType == 2)
            {
                gridreport.Report.ColumnByName("ExecDeptName").Visible = true;
            }
            else if (rowGroupType == 3)
            {
                gridreport.Report.ColumnByName("PatDoctorName").Visible = true;
            }
            else if (rowGroupType == 4)
            {
                gridreport.Report.ColumnByName("PatTypeName").Visible = true;
            }
            else if (rowGroupType == 5)
            {
                gridreport.Report.ColumnByName("PatDeptName").Visible = true;
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
        private void FrmIpatientRevenue_FormClosed(object sender, FormClosedEventArgs e)
        {
            GridViewResult.Stop();
        }

        /// <summary>
        /// 索引改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void CmbTimeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTimeType.SelectedIndex == 0)
            {
                if (cmbColGroupType.Items.Count > 4)
                {
                    cmbColGroupType.Items.RemoveAt(4);
                    cmbColGroupType.SelectedIndex = 0;
                }
            }
            else
            {
                if (cmbColGroupType.Items.Count < 5)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Text = "支付方式";
                    cmbColGroupType.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// 索引改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void CmbRowGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
