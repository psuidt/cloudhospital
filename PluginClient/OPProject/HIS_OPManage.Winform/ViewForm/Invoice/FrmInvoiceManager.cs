using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    public partial class FrmInvoiceManager : BaseFormBusiness,IFrmInvoice
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmInvoiceManager()
        {
            InitializeComponent();
        }

        #region 接口方法
        
        /// <summary>
        /// 当前票据ID
        /// </summary>
        private int curInvoiceID;

        /// <summary>
        /// 票据总金额
        /// </summary>
        public string TotalMoney
        {
            set
            {
                txtTotalMoney.Text = value;
            }
        }

        /// <summary>
        /// 票据使用张数
        /// </summary>
        public string TotalCont
        {
            set
            {
                txtCount.Text = value;
            }
        }

        /// <summary>
        /// 退费张数
        /// </summary>
        public string RefundCount
        {
            set
            {
                txtRefundCount.Text = value;
            }
        }

        /// <summary>
        /// 退费金额
        /// </summary>
        public string RefundMoney
        {
            set
            {
                txtRefundMoney.Text = value;
            }
        }

        /// <summary>
        /// 票据总张数
        /// </summary>
        public string AllCount
        {
            set
            {
                txtAllCount.Text = value;
            }
        }

        /// <summary>
        /// 给网格加载数据
        /// </summary>
        /// <param name="dt">票据数据</param>
        /// <param name="filter">过滤条件</param>
        public void LoadInvoice(DataTable dt, string filter)
        {            
            this.dgInvoice.DataSource = dt.DefaultView;
            if (dt != null && dt.Rows.Count > 0)
            {
                ((DataView)dgInvoice.DataSource).RowFilter = filter;
            }
        }

        /// <summary>
        /// 绑定收费员列表
        /// </summary>
        /// <param name="dt">收费员列表</param>
        public void BindTollcollector_txtEmp(DataTable dt)
        {
            txtEmp.DisplayField = "Name";
            txtEmp.MemberField = "EmpID";
            txtEmp.CardColumn = "EmpID|编码|100,Name|姓名|auto";
            txtEmp.QueryFieldsString = "Name,pym,wbm";
            txtEmp.ShowCardWidth = 200;
            txtEmp.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 获取当前票据ID
        /// </summary>
        /// <returns>当前票据ID</returns>
        public int GetCurInvoiceID()        
        {
            return curInvoiceID;           
        }

        /// <summary>
        /// 设置网格显示颜色
        /// </summary>
        public void SetGridColor()
        {
            foreach (DataGridViewRow row in dgInvoice.Rows)
            {
                short status_flag = Convert.ToInt16(row.Cells["status"].Value);
                Color foreColor = Color.Black;
                switch (status_flag)
                {
                    case 0:
                        foreColor = Color.Black;
                        break;

                    case 1:
                        foreColor = Color.Gray;
                        break;

                    case 2:
                        foreColor = Color.DarkGreen;
                        break;

                    case 3:
                        foreColor = Color.Red;
                        break;
                }

                dgInvoice.SetRowColor(row.Index, foreColor, true);
            }
        }

        /// <summary>
        /// 获取过滤条件
        /// </summary>
        /// <returns>过滤条件</returns>
        public string GetStatusString()
        { 
            string status = string.Empty;
            if (chkInUse.Checked)
            {
                status = " status=0 or ";
            }

            if (chkUseAll.Checked)
            {
                status += " status=1 or ";
            }

            if (chkNoUse.Checked)
            {
                status += " status=2 or ";
            }

            if (chkStopUse.Checked)
            {
                status += " status=3 or ";
            }

            if (status != string.Empty)
            {
                status = status.Remove(status.Length - 3, 2);
                return "(" + status + ")";
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        /// <summary>
        /// 新分配按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnNewAllot_Click(object sender, EventArgs e)
        {
            InvokeController("AddAllot");            
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {          
            InvokeController("LoadInvoices");
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        public string GetQueryString()
        {
            string filter = GetStatusString();
            if (chkAllotDate.Checked)
            {
                if (filter != string.Empty)
                {
                    filter += " and allotdate >='" + dtAllotDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' and allotdate<='" + dtAllotDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'";
                }
                else
                {
                    filter += " allotdate >='" + dtAllotDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' and allotdate<='" + dtAllotDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'";
                }
            }

            if (chkEmp.Checked && txtEmp.Text.Trim() != string.Empty)
            {
                if (filter != string.Empty)
                {
                    filter += " and empNAME ='" + txtEmp.Text + "'";
                }
                else
                {
                    filter += " empNAME ='" + txtEmp.Text + "'";
                }
            }

            return filter;
        }

        /// <summary>
        /// 窗休Load事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmInvoiceManager_Load(object sender, EventArgs e)
        {
            InvokeController("loadCashier");
            InvokeController("LoadInvoices");
            chkInUse.CheckedChanged += new EventHandler(chkStatusCheckBox_CheckedChanged);
            chkUseAll.CheckedChanged += new EventHandler(chkStatusCheckBox_CheckedChanged);
            chkNoUse.CheckedChanged += new EventHandler(chkStatusCheckBox_CheckedChanged);
            chkStopUse.CheckedChanged += new EventHandler(chkStatusCheckBox_CheckedChanged);
        }

        /// <summary>
        /// 日期选择事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void chkAllotDate_CheckedChanged(object sender, EventArgs e)
        {
            dtAllotDate.Enabled = chkAllotDate.Checked;
        }

        /// <summary>
        /// 人员复选框选择事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void chkEmp_CheckedChanged(object sender, EventArgs e)
        {
            txtEmp.Enabled = chkEmp.Checked;
        }

        /// <summary>
        /// 刷新按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InvokeController("LoadInvoices");
        }

        /// <summary>
        /// 票据状态选择事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void chkStatusCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            InvokeController("LoadInvoices");
        }

        /// <summary>
        /// 停用按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnStopUse_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBoxEx.Show("确定要停用该卷发票吗？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                if (dgInvoice.Rows.Count == 0)
                {
                    return;
                }

                curInvoiceID= Convert.ToInt32(dgInvoice["ID", dgInvoice.CurrentCell.RowIndex].Value);
                if ((bool)InvokeController("StopInvoice"))
                {
                    InvokeController("LoadInvoices");
                }        
            }
            catch (Exception err)
            {
                MessageBoxEx.Show(err.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 删除按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgInvoice.Rows.Count == 0)
            {
                return;
            }

            if (MessageBoxEx.Show("确定要删除该卷发票记录吗？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                curInvoiceID = Convert.ToInt32(dgInvoice["ID", dgInvoice.CurrentRow.Index].Value);
                if ((bool)InvokeController("DeleteInvoice"))
                {
                    InvokeController("LoadInvoices");
                }
            }
            catch (Exception err)
            {
                MessageBoxEx.Show(err.Message, string.Empty,  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 网格单击事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>  
        private void dgInvoice_Click(object sender, EventArgs e)
        {
            if (dgInvoice.CurrentCell == null)
            {
                return;
            }

            if (dgInvoice.Rows.Count == 0)
            {
                return;
            }

            int row = dgInvoice.CurrentCell.RowIndex;
            string startNo = dgInvoice["StartNo", row].Value.ToString().Trim();
            string endNo = dgInvoice["EndNO", row].Value.ToString().Trim();
            string perfChar = dgInvoice["PerfChar", row].Value.ToString().Trim();
            int invoicetype = Convert.ToInt32(dgInvoice["invoicetype", row].Value.ToString().Trim());
            txtStart.Text = startNo;
            txtEnd.Text = endNo;
            try
            {
                InvokeController("GetInvoiceListInfo", invoicetype, perfChar, startNo, endNo);
            }
            catch (Exception err)
            {
                MessageBoxEx.Show(err.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
