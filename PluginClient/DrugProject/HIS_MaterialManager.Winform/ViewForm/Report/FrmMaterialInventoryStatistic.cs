using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_MaterialManage.Winform.IView.Report;

namespace HIS_MaterialManage.Winform.ViewForm
{
    /// <summary>
    /// 进销存统计
    /// </summary>
    public partial class FrmMaterialInventoryStatistic : BaseFormBusiness, IFrmMaterialInventoryStatistic
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmMaterialInventoryStatistic()
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
            }
        }

        /// <summary>
        /// 绑定物资类型TextBox控件
        /// </summary>
        /// <param name="typeId">物资类型Id</param>
        /// <param name="typeName">物资类型名称</param>
        public void BindMaterialTypeTextBox(int typeId, string typeName)
        {
            txtType.Text = typeName;
            txtType.Tag = typeId;
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
            cmbQueryMonth.DataSource = dv.ToTable();
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
        /// 打开界面加载数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmInventoryStatistic_OpenWindowBefore(object sender, EventArgs e)
        {
            //绑定库房
            InvokeController("GetStoreRoom", frmName);
            txtType.Tag = 0;
            txtType.Text = "全部";
        }

        /// <summary>
        /// 选择科室
        /// </summary>
        /// <param name="sender">控件</param>
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
        /// 切换会计年
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void cmbQueryYear_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender">控件</param>
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
        /// 关闭界面释放报表控件
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmInventoryStatistic_FormClosed(object sender, FormClosedEventArgs e)
        {
            axGRDisplayViewer.Stop();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">控件</param>
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
                int typeId = Convert.ToInt32(txtType.Tag);
                string currentUserName = (string)InvokeController("GetCurrentUserName");
                DataTable dtReport = (DataTable)InvokeController("InventoryStatistic", deptId, queryYear, queryMonth, typeId);
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                myDictionary.Add("会计年", cmbQueryYear.Text);
                myDictionary.Add("会计月", cmbQueryMonth.Text);
                myDictionary.Add("查询科室", cmbDept.Text);
                myDictionary.Add("类型", txtType.Text);
                myDictionary.Add("查询人", currentUserName);
                myDictionary.Add("HospitalName", (InvokeController("this") as AbstractController).LoginUserInfo.WorkName + "物资进销存报表");
                GridReport gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 5009, 0, myDictionary, dtReport);
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
        /// 选中会计年
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void cmbQueryYear_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbQueryYear.SelectedValue != null)
            {
                InvokeController("GetAcountMonths", frmName, Convert.ToInt32(cmbDept.SelectedValue), Convert.ToInt32(cmbQueryYear.SelectedValue));
            }
        }
        #endregion

        /// <summary>
        /// 选择物资类型
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnShowTypeTree_Click(object sender, EventArgs e)
        {
            InvokeController("OpenMaterialTypeDialog", frmName);
        }
    }
}
