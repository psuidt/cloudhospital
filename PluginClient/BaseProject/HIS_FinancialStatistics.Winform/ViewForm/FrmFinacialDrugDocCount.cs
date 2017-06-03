using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_FinancialStatistics.Winform.IView;

namespace HIS_FinancialStatistics.Winform.ViewForm
{
    public partial class FrmFinacialDrugDocCount : BaseFormBusiness, IFrmFinacialDrugDocCount
    {
        /// <summary>
        /// 报表控件
        /// </summary>
        private GridReport gridreport;

        /// <summary>
        /// 构造
        /// Initializes a new instance of the<see cref="FrmFinacialDrugDocCount" /> class.
        /// </summary>
        public FrmFinacialDrugDocCount()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体打开事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmFinacialDrugDocCount_OpenWindowBefore(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);//当前月第一天
            DateTime d2 = d1.AddMonths(1).AddDays(-1);//当前月最后一天
            sdtDate.Bdate.Value = d1;
            sdtDate.Edate.Value = d2;
            cmbDateType.SelectedIndex = 0;
            InvokeController("GetWorker");
        }

        /// <summary>
        /// 设置机构
        /// </summary>
        /// <param name="dtWorker">机构信息</param>
        public void SetWorkers(DataTable dtWorker)
        {
            cmbWorker.DisplayMember = "WorkName";
            cmbWorker.ValueMember = "WorkId";
            cmbWorker.DataSource = dtWorker;
            cmbWorker.SelectedIndex = 0;
        }

        /// <summary>
        /// 设置医生
        /// </summary>
        /// <param name="dtDoctor">医生信息</param>
        public void SetDoctors(DataTable dtDoctor)
        {
            cmbDocter.DisplayMember = "Name";
            cmbDocter.ValueMember = "EmpId";
            cmbDocter.DataSource = dtDoctor;            
        }

        /// <summary>
        /// 索引改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void CmbWorker_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iWorkId = Convert.ToInt32(cmbWorker.SelectedValue);

            InvokeController("GetDoctor", iWorkId);
        }

        #region 按钮事件
        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            RefreshData();
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
                InvokeController("MessageBoxErr", error.Message);
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

        #endregion

        #region 方法
        /// <summary>
        /// 刷新数据
        /// </summary>
        public void RefreshData()
        {
            GridViewResult.Stop();
            int iWorkId= Convert.ToInt32(cmbWorker.SelectedValue);
            int iDocId = Convert.ToInt32(cmbDocter.SelectedValue);
            int iType = Convert.ToInt32(cmbDateType.SelectedIndex);
            DateTime bdate = Convert.ToDateTime(sdtDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00"));
            DateTime edate = Convert.ToDateTime(sdtDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59"));
            
            InvokeController("GetDrugDocCount", iWorkId, iDocId, iType, bdate, edate);
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="dtData">统计数据</param>
        public void LoadData(DataTable dtData)
        {
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            myDictionary.Add("Title", cmbWorker.Text.Trim() + "药品医生开方统计");
            myDictionary.Add("DateRange", sdtDate.Bdate.Value.ToString("yyyy-MM-dd") + "至" + sdtDate.Edate.Value.ToString("yyyy-MM-dd"));
            myDictionary.Add("Printer", (InvokeController("this") as AbstractController).LoginUserInfo.EmpName);
           
            gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 6001, 0, myDictionary, dtData);
            GridViewResult.Report = gridreport.Report;
            GridViewResult.Start();
            GridViewResult.Refresh();
        }
        #endregion
    }
}