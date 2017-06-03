using System;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_MaterialManage.Winform.IView.FinanceMgr;

namespace HIS_MaterialManage.Winform.ViewForm
{
    /// <summary>
    /// 物资月结
    /// </summary>
    public partial class FrmMaterialBalance : BaseFormBusiness, IFrmMaterialBalance
    {
        #region 窗体
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmMaterialBalance()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打开界面加载数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmBalance_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("GetDrugDept", frmName);
            InvokeController("GetMonthBalaceByDept", frmName);
            InvokeController("GetAccountDay", frmName);
        }
        #endregion

        #region 事件

        /// <summary>
        /// 设置月结日期
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnSetting_Click(object sender, EventArgs e)
        {
            InvokeController("SetAccountDay", frmName, this.txtDays.Value);
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
        /// 自动对账
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnCheckAccount_Click(object sender, EventArgs e)
        {
            InvokeController("SystemCheckAccount", frmName);
        }

        /// <summary>
        /// 月结
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnMonth_Click(object sender, EventArgs e)
        {
            InvokeController("MonthAccount", frmName);
        }

        /// <summary>
        /// 情况物资信息
        /// </summary>
        /// <param name="sender">控件</param>
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
        /// 切换库房
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void cmbDept_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbDept.SelectedValue != null)
            {
                InvokeController("SetSelectedDept", Convert.ToInt32(cmbDept.SelectedValue));
                InvokeController("GetMonthBalaceByDept", frmName);
                InvokeController("GetAccountDay", frmName);
            }
        }
        #endregion

        #region 接口
        /// <summary>
        /// 绑定库房科室控件
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
        /// 绑定上次月结时间
        /// </summary>
        /// <param name="t1">上次月结开始时间</param>
        /// <param name="t2">上次月结结束时间</param>
        public void BindBalanceTimes(DateTime t1, DateTime t2)
        {
            this.timeBegin.Value = t1;
            this.timeBegin.Value = t2;
        }

        /// <summary>
        /// 绑定月结天
        /// </summary>
        /// <param name="day">每月月结日期</param>
        public void BindBalanceDays(int day)
        {
            if (day > 0)
            {
                this.txtDays.Value = day;
            }
        }

        /// <summary>
        /// 绑定月结记录网格记录
        /// </summary>
        /// <param name="dt">月结记录</param>
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
        /// 绑定物资记录网格信息
        /// </summary>
        /// <param name="dt">物资信息</param>
        public void BindCheckAccount(DataTable dt)
        {
            dgDetails.DataSource = dt;
        }

        /// <summary>
        /// 这只界面控件是否可用
        /// </summary>
        /// <param name="flag">状态</param>
        public void SetBtnEnable(bool flag)
        {
            this.btnMonth.Enabled = flag;
            this.btnCheckAccount.Enabled = flag;
        }

        /// <summary>
        /// 设置操作结果
        /// </summary>
        /// <param name="t">操作结果信息</param>
        public void SetLabelText(string t)
        {
            this.lbResult.Text = t;
        }

        #endregion
    }
}
