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
    public partial class FrmIPAccountBook : BaseFormBusiness, IFrmFinacialIPAccountBook
    {
        /// <summary>
        /// 取得期初期末
        /// </summary>
        /// <param name="type">类型0收入流水1预交金流水</param>
        /// <param name="dt">数据集</param>
        /// <returns>取得开始结束金额</returns>
        private decimal[] GetBeginAndEnd(int type, DataTable dt)
        {
            decimal[] beginEnds = new decimal[2];
            if (dt == null || dt.Rows.Count <= 0)
            {
                beginEnds[0] = 0;
                beginEnds[1] = 0;
            }
            else
            {
                if (type == 0)
                {
                    beginEnds[0] = Convert.ToDecimal(dt.Rows[0]["BeInPatRevenue"]);
                    beginEnds[1] = Convert.ToDecimal(dt.Rows[dt.Rows.Count - 1]["EndInPatRevenue"]);
                }
                else
                {
                    beginEnds[0] = Convert.ToDecimal(dt.Rows[0]["BeDepositFee"]);
                    beginEnds[1] = Convert.ToDecimal(dt.Rows[dt.Rows.Count - 1]["EndDepositFee"]);
                }
            }

            return beginEnds;
        }

        /// <summary>
        /// 构造
        /// Initializes a new instance of the<see cref="FrmIPAccountBook" /> class.
        /// </summary>
        public FrmIPAccountBook()
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
        private void FrmIPAccountBook_OpenWindowBefore(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);//当前月第一天
            DateTime d2 = d1.AddMonths(1).AddDays(-1);//当前月最后一天
            superTabControl1.SelectedTabIndex = 0;
            sdtDate.Bdate.Value = d1;
            sdtDate.Edate.Value = d2;
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
                DateTime bdate = Convert.ToDateTime(sdtDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00"));
                DateTime edate = Convert.ToDateTime(sdtDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59"));
                int queryWorkId = Convert.ToInt32(cmbWorker.SelectedValue);
                string currentUserName = (string)InvokeController("GetCurrentUserName");
                int queryType = 0;//0收入流水账 1预交金流水账
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                if (superTabControl1.SelectedTabIndex == 0)
                {
                    GVResultRevenue.Stop();
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dtReport = (DataTable)InvokeController("GetFinacialIPAccountBookData", frmName, bdate, edate, queryWorkId, queryType);
                    
                    myDictionary.Add("Title", cmbWorker.Text.Trim() + "住院收入流水账");
                    myDictionary.Add("DateRange", sdtDate.Bdate.Value.ToString("yyyy-MM-dd") + "至" + sdtDate.Edate.Value.ToString("yyyy-MM-dd"));
                    myDictionary.Add("Printer", currentUserName);
                    decimal[] beginEnd = GetBeginAndEnd(0, dtReport);
                    myDictionary.Add("BeginFeeTotal", beginEnd[0]);
                    myDictionary.Add("EndFeeTotal", beginEnd[1]);
                    GridReport gridreport = new GridReport();
                    gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 3012, 0, myDictionary, dtReport);
                    GVResultRevenue.Report = gridreport.Report;
                    GVResultRevenue.Start();
                    GVResultRevenue.Refresh();
                }
                else
                {
                    queryType = 1;
                    GVResultDeposit.Stop();
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dtReport = (DataTable)InvokeController("GetFinacialIPAccountBookData", frmName, bdate, edate, queryWorkId, queryType);

                    myDictionary.Add("Title", cmbWorker.Text.Trim() + "住院预交金流水账");
                    myDictionary.Add("DateRange", sdtDate.Bdate.Value.ToString("yyyy-MM-dd") + "至" + sdtDate.Edate.Value.ToString("yyyy-MM-dd"));
                    myDictionary.Add("Printer", currentUserName);
                    decimal[] beginEnd = GetBeginAndEnd(1, dtReport);
                    myDictionary.Add("BeginFeeTotal", beginEnd[0]);
                    myDictionary.Add("EndFeeTotal", beginEnd[1]);
                    GridReport gridreport = new GridReport();
                    gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 3013, 0, myDictionary, dtReport);
                    GVResultDeposit.Report = gridreport.Report;
                    GVResultDeposit.Start();
                    GVResultDeposit.Refresh();
                }
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
                if (superTabControl1.SelectedTabIndex == 0)
                {
                    GVResultRevenue.PostColumnLayout();
                    GVResultRevenue.Report.PrintPreview(true);
                }
                else
                {
                    GVResultDeposit.PostColumnLayout();
                    GVResultDeposit.Report.PrintPreview(true);
                }
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
        private void FrmIPAccountBook_FormClosed(object sender, FormClosedEventArgs e)
        {
            GVResultRevenue.Stop();
            GVResultDeposit.Stop();
        }
    }
}
