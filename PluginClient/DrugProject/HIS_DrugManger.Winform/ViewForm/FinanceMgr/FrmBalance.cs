using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.FinanceMgr;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 月结
    /// </summary>
    public partial class FrmBalance : BaseFormBusiness, IFrmBalance
    {
        /// <summary>
        /// 天数
        /// </summary>
        public int Days = 0;

        #region 窗体
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmBalance()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmBalance_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("GetDrugDept", frmName);
            InvokeController("GetMonthBalaceByDept", frmName);
            InvokeController("GetAccountDay", frmName);
            Days = this.txtDays.Value;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnSetting_Click(object sender, EventArgs e)
        {
            InvokeController("SetAccountDay", frmName, this.txtDays.Value);
            Days = this.txtDays.Value;
        }

        /// <summary>
        /// 关闭事件
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
        private void btnCheckAccount_Click(object sender, EventArgs e)
        {
            InvokeController("SystemCheckAccount", frmName);
        }

        /// <summary>
        /// 月结事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnMonth_Click(object sender, EventArgs e)
        {
            if (DateTime.Now.Day == Days)
            {
                InvokeController("MonthAccount", frmName);
            }
            else
            {
                MessageBoxEx.Show("不是月结时间,不能进行月结");
            }
        }

        #endregion
        #region 接口

        /// <summary>
        /// 绑定药剂科室控件
        /// </summary>
        /// <param name="dtDrugDept">数据源</param>
        public void BindDrugDept(DataTable dtDrugDept)
        {
            if (dtDrugDept != null)
            {
                cmbDept.DataSource = dtDrugDept;
                if (dtDrugDept.Rows.Count > 0)
                {
                    cmbDept.SelectedIndex = 0;
                }
            }
        }
        
        /// <summary>
        /// 绑定月结时间
        /// </summary>
        /// <param name="t1">开始时间</param>
        /// <param name="t2">结束时间</param>
        public void BindBalanceTimes(DateTime t1, DateTime t2)
        {
            this.timeBegin.Value = t1;
            this.timeBegin.Value = t2;
        }

        /// <summary>
        /// 绑定月结天数
        /// </summary>
        /// <param name="day">天数</param>
        public void BindBalanceDays(int day)
        {
            if (day > 0)
            {
                this.txtDays.Value = day;
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="dt">数据源</param>
        public void BindDataGrid(DataTable dt)
        {
            dgBalance.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["BeginTime"] != null)
                {
                    this.timeBegin.Value = Convert.ToDateTime(dt.Rows[0]["BeginTime"]);
                    this.timeEnd.Value = Convert.ToDateTime(dt.Rows[0]["EndTime"]);
                }
            }
            else
            {
                this.timeBegin.Text = null;
                this.timeEnd.Text = null;
            }
        }

        /// <summary>
        /// 获取网格数据
        /// </summary>
        /// <param name="dt">数据源</param>
        public void BindCheckAccount(DataTable dt)
        {
            dgDetails.DataSource = dt;
        }

        /// <summary>
        /// 设置按钮是否可用
        /// </summary>
        /// <param name="flag">是否可用</param>
        public void SetBtnEnable(bool flag)
        {
            this.btnMonth.Enabled = flag;
            this.btnCheckAccount.Enabled = flag;
        }

        /// <summary>
        /// 设置显示文本
        /// </summary>
        /// <param name="t">文本</param>
        public void SetLabelText(string t)
        {
            this.lbResult.Text = t;
        }
        #endregion

        /// <summary>
        /// 选择变化事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgBalance_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        /// <summary>
        /// 选择变化事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgBalance_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgBalance.CurrentCell == null)
            {
                return;
            }

            this.dgDetails.DataSource = null;
        }

        /// <summary>
        /// 选择变化事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmbDept_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbDept.SelectedValue != null)
            {
                InvokeController("SetSelectedDept",Convert.ToInt32(cmbDept.SelectedValue));
                InvokeController("GetMonthBalaceByDept", frmName);
                InvokeController("GetAccountDay", frmName);
                Days = this.txtDays.Value;
            }
        }
    }
}
