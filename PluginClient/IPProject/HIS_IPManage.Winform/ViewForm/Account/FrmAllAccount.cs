using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    /// <summary>
    /// 缴款查询界面
    /// </summary>
    public partial class FrmAllAccount : BaseFormBusiness, IFrmAllAccount
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmAllAccount()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体打开前加载数据
        /// </summary>
        /// <param name="sender">触发事件的按钮</param>
        /// <param name="e">事件参数</param>
        private void FrmAllAccount_OpenWindowBefore(object sender, EventArgs e)
        {
            sdtDate.Bdate.Value = DateTime.Now.AddDays(-1);
            sdtDate.Edate.Value = DateTime.Now;
            cmbStatus.SelectedIndex = 0;
            InvokeController("GetCashier");
        }

        /// <summary>
        /// 绑定数据，动态增加列
        /// </summary>
        /// <param name="dtAllAccount">所有已缴款数据</param>
        /// <param name="dtAllNotAccount">所有未缴款数据</param>
        public void BindQueryData(DataTable dtAllAccount, DataTable dtAllNotAccount)
        {
            // 绑定已缴款数据
            dgAllAccount.DataSource = dtAllAccount;
            // 绑定未缴款数据
            dgAllNotAccount.DataSource = dtAllNotAccount;
        }

        /// <summary>
        /// 绑定缴款人列表
        /// </summary>
        /// <param name="dtCashier">缴款人列表</param>
        public void LoadCashier(DataTable dtCashier)
        {
            txtEmp.DisplayField = "Name";
            txtEmp.MemberField = "EmpID";
            txtEmp.CardColumn = "Name|姓名|auto";
            txtEmp.QueryFieldsString = "Name,pym,wbm";
            txtEmp.ShowCardHeight = 130;
            txtEmp.ShowCardWidth = 160;
            txtEmp.ShowCardDataSource = dtCashier;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">btnStatic</param>
        /// <param name="e">事件参数</param>
        private void btnStatic_Click(object sender, EventArgs e)
        {
            AccountRefresh();
        }

        /// <summary>
        /// 收款
        /// </summary>
        /// <param name="sender">btnRecive</param>
        /// <param name="e">事件参数</param>
        private void btnRecive_Click(object sender, EventArgs e)
        {
            if (dgAllAccount.CurrentCell != null)
            {
                DataTable dt = (DataTable)dgAllAccount.DataSource;
                string accountid = "0";
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["Selected"]) == 1)
                    {
                        accountid += "," + dt.Rows[i]["accountid"];
                    }
                }

                if (accountid == "0")
                {
                    MessageBoxEx.Show("没有选择需要收款的记录");
                    return;
                }

                if (MessageBoxEx.Show("您确定收款吗", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if ((bool)InvokeController("ReciveAccount", accountid))
                    {
                        AccountRefresh();
                    }
                }
            }
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender">btnClose</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        private void AccountRefresh()
        {
            DateTime bdate = Convert.ToDateTime(sdtDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00"));
            DateTime edate = Convert.ToDateTime(sdtDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59"));
            int iEmpId = 0;
            if (chkEmp.Checked)
            {
                // if (txtEmp.Text.Trim() != "")
                if (!string.IsNullOrEmpty(txtEmp.Text.Trim()))
                {
                    iEmpId = Convert.ToInt32(txtEmp.MemberValue);
                }
            }

            InvokeController("QueryAllAccount", bdate, edate, iEmpId, cmbStatus.SelectedIndex);
        }

        /// <summary>
        /// 勾选是个根据缴款人查询
        /// </summary>
        /// <param name="sender">chkEmp</param>
        /// <param name="e">事件参数</param>
        private void chkEmp_CheckedChanged(object sender, EventArgs e)
        {
            txtEmp.Enabled = chkEmp.Checked;
        }

        /// <summary>
        /// 缴款记录选择操作
        /// </summary>
        /// <param name="sender">dgAllAccount</param>
        /// <param name="e">事件参数</param>
        private void dgAllAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgAllAccount.CurrentCell == null)
            {
                return;
            }

            if (dgAllAccount.CurrentCell.ColumnIndex == dgAllAccount.Columns[Selected.Name].Index)
            {
                int rowindex = dgAllAccount.CurrentCell.RowIndex;
                if (rowindex == dgAllAccount.Rows.Count - 1)
                {
                    return;
                }

                if (Convert.ToInt32(dgAllAccount[ReceivFlag.Index, rowindex].Value) == 1)
                {
                    dgAllAccount[Selected.Index, rowindex].Value = 0;
                }
                else
                {
                    dgAllAccount[Selected.Index, rowindex].Value = Convert.ToInt32(dgAllAccount[Selected.Index, rowindex].Value) == 0 ? 1 : 0;
                }
            }
        }

        /// <summary>
        /// 查看缴款单明细
        /// </summary>
        /// <param name="sender">触发事件的按钮</param>
        /// <param name="e">事件参数</param>
        private void dgAllAccount_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgAllAccount.CurrentCell != null)
            {
                object obj = dgAllAccount["accountid", dgAllAccount.CurrentCell.RowIndex].Value;
                int accountid = Convert.ToInt32(obj == DBNull.Value ? 0 : obj);

                object oAccountType = dgAllAccount["AccountType", dgAllAccount.CurrentCell.RowIndex].Value;
                string sAccountType = oAccountType==DBNull.Value?"0": oAccountType.ToString();

                object oEmpId = dgAllAccount["AccountEmpID", dgAllAccount.CurrentCell.RowIndex].Value;
                int iEmpId = oEmpId==DBNull.Value?0:Convert.ToInt32(oEmpId);
                InvokeController("QueryAccountDetail", iEmpId, accountid, sAccountType.Contains("预交金") ? 1 : 0);
            }
        }
    }
}
