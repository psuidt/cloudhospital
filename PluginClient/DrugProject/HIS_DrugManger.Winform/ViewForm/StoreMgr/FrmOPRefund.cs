using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.StoreMgr;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 药房退药
    /// </summary>
    public partial class FrmOPRefund : BaseFormBusiness, IFrmOPRefund
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmOPRefund()
        {
            InitializeComponent();
        }

        #region 自定义方法和属性
        /// <summary>
        /// 发药人
        /// </summary>
        public string ReturnEmployeeName
        {
            get
            {
                return txtReturnEmp.Text;
            }

            set
            {
                txtReturnEmp.Text = value;
            }
        }
        #endregion

        #region 接口
        /// <summary>
        /// 设置药房名称
        /// </summary>
        /// <param name="deptName">药房名称</param>
        public void SetDrugStoreName(string deptName)
        {
            txtDrugStoreName.Text = deptName;
        }

        /// <summary>
        /// 取得退药查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        public Dictionary<string, string> GetRefundCondition()
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            if (txtInvoice.Text.Trim() != string.Empty)
            {
                queryCondition.Add("a.InvoiceNum", "'" + txtInvoice.Text.Trim() + "'");
            }

            //在控制器中增加科室ID条件
            return queryCondition;
        }

        /// <summary>
        /// 取得退药查询-查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        public Dictionary<string, string> GetRefundQueryCondition()
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            string subCondition = " EXISTS(SELECT RecipeID FROM DS_OPDispHead h WHERE h.RefundFlag = 1 AND c.FeeItemHeadID = h.RecipeID AND  h.DispTime BETWEEN '" + sdtDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + sdtDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "')";
            queryCondition.Add(string.Empty, subCondition);
            if (txtRetInvoice.Text.Trim() != string.Empty)
            {
                queryCondition.Add("a.InvoiceNum", "'" + txtRetInvoice.Text.Trim() + "'");
            }

            //在控制器中增加科室ID条件
            return queryCondition;
        }

        /// <summary>
        /// 绑定退药网格
        /// </summary>
        /// <param name="dt">待退药记录</param>
        public void BindRefundGrid(DataTable dt)
        {
            dgDetail.DataSource = dt;
        }

        /// <summary>
        /// 绑定退药查询网格
        /// </summary>
        /// <param name="dt">退药记录</param>
        public void BindRefundQueryGrid(DataTable dt)
        {
            dgRetDetail.DataSource = dt;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnRetClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnRetQuery_Click(object sender, EventArgs e)
        {
            InvokeController("GetRefundQueryDetail");
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtInvoice.Text.Trim() == string.Empty)
            {
                MessageBoxShowSimple("请输入发票号");
                txtInvoice.Focus();
                return;
            }

            InvokeController("GetRefundDetail");
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dgDetail != null && dgDetail.Rows.Count > 0)
            {
                DataTable dtRefund = (DataTable)dgDetail.DataSource;
                InvokeController("OPRefund", dtRefund);
                btnQuery_Click(null, null);
            }
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
        private void FrmOPRefund_OpenWindowBefore(object sender, EventArgs e)
        {
            //给查询日期设置默认值
            sdtDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            sdtDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));

            //设置焦点
            txtRetInvoice.Focus();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void tabControlRet_SelectedTabChanged(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
        {
            if (tabControlRet.SelectedTabIndex == 1)
            {
                txtRetInvoice.Focus();
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void txtInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuery_Click(null, null);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void txtRetInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRetQuery_Click(null, null);
            }
        }
        #endregion
    }
}
