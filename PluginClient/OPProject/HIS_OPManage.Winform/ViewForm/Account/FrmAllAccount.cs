using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
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
        /// 绑定数据，动态增加列
        /// </summary>
        /// <param name="dtAllAccount">已经缴款数据</param>
        /// <param name="dtAllNotAccount">未缴款数据</param>
        public void BindQueryData(DataTable dtAllAccount, DataTable dtAllNotAccount)
        {
            for (int i = 12; i < dgAllAccount.Columns.Count;i++)
            {
                dgAllAccount.Columns.RemoveAt(i);
            }

            for (int i = 14; i < dtAllAccount.Columns.Count; i++)
            {
                if (!dgAllAccount.Columns.Contains(dtAllAccount.Columns[i].ColumnName))
                {
                    DataGridViewColumn col = new DataGridViewColumn();
                    col.HeaderText = dtAllAccount.Columns[i].ColumnName;
                    col.DataPropertyName = dtAllAccount.Columns[i].ColumnName;
                    col.Name= dtAllAccount.Columns[i].ColumnName;
                    DataGridViewCell dgvcell = new DataGridViewTextBoxCell();
                    col.CellTemplate = dgvcell;
                    col.DefaultCellStyle.Format = "N2";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgAllAccount.Columns.Add(col);
                }  
            }

            dgAllAccount.DataSource = dtAllAccount;
            for (int i = 6; i < dgAllNotAccount.Columns.Count; i++)
            {
                dgAllNotAccount.Columns.RemoveAt(i);
            }

            for (int i = 11; i < dtAllNotAccount.Columns.Count; i++)
            {
                if (!dgAllNotAccount.Columns.Contains(dtAllNotAccount.Columns[i].ColumnName))
                {
                    DataGridViewColumn col = new DataGridViewColumn();
                    col.HeaderText = dtAllNotAccount.Columns[i].ColumnName;
                    col.DataPropertyName = dtAllNotAccount.Columns[i].ColumnName;
                    col.Name = dtAllNotAccount.Columns[i].ColumnName;
                    DataGridViewCell dgvcell = new DataGridViewTextBoxCell();
                    col.CellTemplate = dgvcell;
                    col.DefaultCellStyle.Format = "N2";
                    col.DefaultCellStyle.Alignment= DataGridViewContentAlignment.MiddleRight;
                    dgAllNotAccount.Columns.Add(col);
                }
            }

            dgAllNotAccount.DataSource = dtAllNotAccount;
        }

        /// <summary>
        /// 绑定收费员
        /// </summary>
        /// <param name="dtCashier">收费员数据</param>
        public void loadCashier(DataTable dtCashier)
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
        /// 关闭事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 打开窗体前调用事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void FrmAllAccount_OpenWindowBefore(object sender, EventArgs e)
        {
            sdtDate.Bdate.Value = DateTime.Now.AddDays(-1);
            sdtDate.Edate.Value = DateTime.Now;
            cmbStatus.SelectedIndex = 0;
            InvokeController("GetCashier");    
        }

        /// <summary>
        ///  查询
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnStatic_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// 数据刷新
        /// </summary>
        private void Refresh()
        {
            DateTime bdate = Convert.ToDateTime(sdtDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00"));
            DateTime edate = Convert.ToDateTime(sdtDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59"));
            int empid = 0;
            if (chkEmp.Checked)
            {
                if (txtEmp.Text.Trim() != string.Empty)
                {
                    empid = Convert.ToInt32(txtEmp.MemberValue);
                }
            }

            InvokeController("QueryAllAccount", bdate, edate, empid, cmbStatus.SelectedIndex);
        }

        /// <summary>
        /// 收费员复选框选择切换事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void chkEmp_CheckedChanged(object sender, EventArgs e)
        {
            txtEmp.Enabled = chkEmp.Checked;
        }

        /// <summary>
        /// 双击查看缴款单明细
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgAllAccount_DoubleClick(object sender, EventArgs e)
        {
            if (dgAllAccount.CurrentCell != null)
            {
                object obj = dgAllAccount["accountid", dgAllAccount.CurrentCell.RowIndex].Value;
                int accountid = Convert.ToInt32(obj == DBNull.Value ? 0 : obj);
                InvokeController("QueryAccountDetail", accountid);
            }
        }

        /// <summary>
        /// 网格双击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgAllNotAccount_DoubleClick(object sender, EventArgs e)
        {
            if (dgAllNotAccount.CurrentCell != null)
            {
                object obj = dgAllNotAccount["accountid1", dgAllNotAccount.CurrentCell.RowIndex].Value;
                int accountid = Convert.ToInt32(obj==DBNull.Value?0:obj);
                InvokeController("QueryAccountDetail", accountid);
            }
        }

        /// <summary>
        /// 收款
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnRecive_Click(object sender, EventArgs e)
        {
            if (dgAllAccount.CurrentCell != null)
            {
                DataTable dt =(DataTable) dgAllAccount.DataSource;
                string accountid = "0";
                for (int i = 0; i < dt.Rows.Count-1; i++)
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
                        Refresh();                    
                    }
                }
                //int curRowindex = dgAllAccount.CurrentCell.RowIndex;
                //object obj = dgAllAccount["accountid", curRowindex].Value;
                //int accountid = Convert.ToInt32(obj == DBNull.Value ? 0 : obj);
                //if (accountid > 0)
                //{
                //    if (Convert.ToInt32(dgAllAccount["ReceivFlag", curRowindex].Value) > 0)
                //    {
                //        MessageBoxEx.Show("此缴款记录已经收款");
                //        return;
                //    }
                //    if (MessageBoxEx.Show("您确定收款吗", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //    {
                //        if ((bool)InvokeController("ReciveAccount", accountid))
                //        {
                //            btnStatic_Click(null, null);
                //            dgAllAccount.CurrentCell = dgAllAccount[0, curRowindex];
                //        }
                //    }
                //}
            }
        }

        /// <summary>
        /// 网格单元格点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
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
                if(rowindex==dgAllAccount.Rows.Count-1)
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
    }
}
