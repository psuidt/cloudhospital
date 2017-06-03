using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.Report;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 进销存统计
    /// </summary>
    public partial class FrmInventoryRpt : BaseFormBusiness, IFrmInventoryRpt
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmInventoryRpt()
        {
            InitializeComponent();
        }

        #region 自定义属性和方法
        #endregion

        #region 接口
        /// <summary>
        /// 绑定库房下拉框
        /// </summary>
        /// <param name="dtDept">科室数据</param>
        public void BindDeptRoom(DataTable dtDept)
        {
            cmbDept.DataSource = dtDept;
            cmbDept.ValueMember = "DeptID";
            cmbDept.DisplayMember = "DeptName";
            if (dtDept.Rows.Count > 0)
            {
                cmbDept.SelectedIndex = 0;
                //InvokeController("GetAcountYears", frmName, Convert.ToInt32(dtDept.Rows[0]["DeptID"]));
            }
        }

        /// <summary>
        /// 绑定药品类型下拉控件
        /// </summary>
        /// <param name="dt">数据源</param>
        public void BindDrugTypeDicComboBox(DataTable dt)
        {
            cmbQueryType.DataSource = dt;
            cmbQueryType.ValueMember = "TypeID";
            cmbQueryType.DisplayMember = "TypeName";
            if (dt.Rows.Count > 0)
            {
                cmbQueryType.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定会计年
        /// </summary>
        /// <param name="dtYear">年</param>
        public void BindYear(DataTable dtYear)
        {
            if (dtYear == null)
            {
                return;
            }

            cmbQueryYear.DataSource = dtYear;
            cmbQueryYear.ValueMember = "ID";
            cmbQueryYear.DisplayMember = "Name";
            DataRow[] rows = dtYear.Select("ID=" + DateTime.Now.Year);
            if (rows.Length > 0)
            {
                cmbQueryYear.SelectedValue = DateTime.Now.Year;
                InvokeController("GetAcountMonths", frmName, Convert.ToInt32(cmbDept.SelectedValue), Convert.ToInt32(cmbQueryYear.SelectedValue));
                return;
            }

            if (dtYear.Rows.Count > 0)
            {
                cmbQueryYear.SelectedIndex = 0;
                InvokeController("GetAcountMonths", frmName, Convert.ToInt32(cmbDept.SelectedValue), Convert.ToInt32(cmbQueryYear.SelectedValue));
            }
        }

        /// <summary>
        /// 绑定会计月
        /// </summary>
        /// <param name="dtMonth">年</param>
        public void BindMonth(DataTable dtMonth)
        {
            DataView dv = dtMonth.DefaultView;
            dv.Sort = "ID asc";
            cmbQueryMonth.DataSource = dtMonth;
            cmbQueryMonth.ValueMember = "ID";
            cmbQueryMonth.DisplayMember = "Name";
            DataRow[] rows = dtMonth.Select("ID=" + DateTime.Now.Month);
            if (rows.Length > 0)
            {
                cmbQueryMonth.SelectedValue = DateTime.Now.Month;
                return;
            }

            if (dtMonth.Rows.Count > 0)
            {
                cmbQueryMonth.SelectedIndex = 0;
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmInventoryStatistic_OpenWindowBefore(object sender, EventArgs e)
        {
            //绑定库房

            //cmbDept.SelectedIndexChanged -= new EventHandler(cmbDept_SelectedIndexChanged);
            InvokeController("GetStoreRoom", frmName);

            //cmbDept.SelectedIndexChanged += new EventHandler(cmbDept_SelectedIndexChanged);
            //绑定药品类型   
            InvokeController("GetDrugType", frmName);
            //加载报表
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmbQueryType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                btnQuery.Focus();
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDept.SelectedValue != null)
            {
                cmbQueryYear.SelectedIndexChanged -= new EventHandler(cmbQueryYear_SelectedIndexChanged);
                InvokeController("GetAcountYears", frmName, Convert.ToInt32(cmbDept.SelectedValue));
                cmbQueryYear.SelectedIndexChanged += new EventHandler(cmbQueryYear_SelectedIndexChanged);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmbQueryYear_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (axGRDisplayViewer.Report == null)
                {
                    return;
                }

                axGRDisplayViewer.PostColumnLayout();
                axGRDisplayViewer.Report.PrintPreview(true);
            }
            catch (Exception error)
            {
                MessageBoxEx.Show(error.Message);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmInventoryStatistic_FormClosed(object sender, FormClosedEventArgs e)
        {
            axGRDisplayViewer.Stop();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                axGRDisplayViewer.Stop();
                this.Cursor = Cursors.WaitCursor;
                int queryYear = Convert.ToInt32(cmbQueryYear.SelectedValue);
                int queryMonth = Convert.ToInt32(cmbQueryMonth.SelectedValue);
                int deptId = Convert.ToInt32(cmbDept.SelectedValue);
                int typeId = Convert.ToInt32(cmbQueryType.SelectedValue);
                string currentUserName = (string)InvokeController("GetCurrentUserName");
                DataTable dtReport = (DataTable)InvokeController("InventoryStatistic", frmName, deptId, queryYear, queryMonth, typeId);
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                myDictionary.Add("会计年", cmbQueryYear.Text);
                myDictionary.Add("会计月", cmbQueryMonth.Text);
                myDictionary.Add("查询科室", cmbDept.Text);
                myDictionary.Add("类型", cmbQueryType.Text);
                myDictionary.Add("查询人", currentUserName);
                myDictionary.Add("HospitalName", (InvokeController("this") as AbstractController).LoginUserInfo.WorkName + "药品进销存报表");
                GridReport gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 4016, 0, myDictionary, dtReport);
                axGRDisplayViewer.Report = gridreport.Report;
                axGRDisplayViewer.Start();
                axGRDisplayViewer.Refresh();
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
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmbQueryYear_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbQueryYear.SelectedValue != null)
            {
                InvokeController("GetAcountMonths", frmName, Convert.ToInt32(cmbDept.SelectedValue), Convert.ToInt32(cmbQueryYear.SelectedValue));
            }
        }
        #endregion
    }
}
