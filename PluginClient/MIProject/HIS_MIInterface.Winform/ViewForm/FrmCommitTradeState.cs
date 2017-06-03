using EFWCoreLib.CoreFrame.Business;
using HIS_MIInterface.Winform.IView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS_MIInterface.Winform.ViewForm
{
    public partial class FrmCommitTradeState : BaseFormBusiness, IFrmCommitTradeState
    {
        public FrmCommitTradeState()
        {
            InitializeComponent();
            dgvTradeInfo.AutoGenerateColumns = false;
        }

        private void FrmCommitTradeState_OpenWindowBefore(object sender, EventArgs e)
        {
            cmbType.SelectedIndex = 0;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string sCardNo = "-1";
            string sInvoiceNo = "-1";
            string sTradeNo = "-1";

            if (cbTradeNo.Checked)
            {
                sTradeNo = txtTradeNo.Text;
            }
            else
            {
                if (cmbType.SelectedIndex == 0)
                {
                    sInvoiceNo = txtInvoice.Text;
                }
                else
                {
                    sCardNo = txtInvoice.Text;
                }
            }

            InvokeController("Mz_GetTradeInfoByCon", sCardNo, sInvoiceNo, sTradeNo);
        }

        private void btnYBQuery_Click(object sender, EventArgs e)
        {
            if (dgvTradeInfo.CurrentRow != null)
            {
                int rowindex = dgvTradeInfo.CurrentCell.RowIndex;
                string tradeNo = dgvTradeInfo.Rows[rowindex].Cells["交易流水号"].Value.ToString().Trim();
                string tradeType = dgvTradeInfo.Rows[rowindex].Cells["交易类型"].Value.ToString().Trim();
                string FeeNo = "0";
                if (tradeType.Contains("挂号"))
                {
                    FeeNo = dgvTradeInfo.Rows[rowindex].Cells["HIS流水号"].Value.ToString().Trim();
                }
                else
                {
                    FeeNo = dgvTradeInfo.Rows[rowindex].Cells["收费单据号"].Value.ToString().Trim();
                }
                InvokeController("MZ_CommitTradeState", tradeNo, FeeNo);
            }
        }

        private void BtnRefundment_Click(object sender, EventArgs e)
        {
            if (dgvTradeInfo.CurrentRow != null)
            {
                int rowindex = dgvTradeInfo.CurrentCell.RowIndex;
                string tradeNo = dgvTradeInfo.Rows[rowindex].Cells["交易流水号"].Value.ToString().Trim();
                string tradeType = dgvTradeInfo.Rows[rowindex].Cells["交易类型"].Value.ToString().Trim();
                string FeeNo = "0";
                if (tradeType.Contains("挂号"))
                {
                    FeeNo = dgvTradeInfo.Rows[rowindex].Cells["收费单据号"].Value.ToString().Trim();
                }
                else
                {
                    FeeNo = dgvTradeInfo.Rows[rowindex].Cells["收费单据号"].Value.ToString().Trim();
                }
                InvokeController("MZ_CommitTradeState", tradeNo, FeeNo);
            }
        }
        private void btnRePrintInvoice_Click(object sender, EventArgs e)
        {
            if (dgvTradeInfo.CurrentRow != null)
            {
                int rowindex = dgvTradeInfo.CurrentCell.RowIndex;
                string tradeNo = dgvTradeInfo.Rows[rowindex].Cells["交易流水号"].Value.ToString().Trim();
                string tradeType = dgvTradeInfo.Rows[rowindex].Cells["交易类型"].Value.ToString().Trim();
                string FeeNo = "0";
                if (tradeType.Contains("挂号"))
                {
                    FeeNo = dgvTradeInfo.Rows[rowindex].Cells["收费单据号"].Value.ToString().Trim();
                        //dgvTradeInfo.Rows[rowindex].Cells["HIS流水号"].Value.ToString().Trim();
                }
                else
                {
                    FeeNo = dgvTradeInfo.Rows[rowindex].Cells["收费单据号"].Value.ToString().Trim();
                }
                InvokeController("MZ_RePrintInvoice", tradeNo, FeeNo);
            }
        }

        private void btnRePrintMXInvoice_Click(object sender, EventArgs e)
        {
            if (dgvTradeInfo.CurrentRow != null)
            {
                int rowindex = dgvTradeInfo.CurrentCell.RowIndex;
                string tradeNo = dgvTradeInfo.Rows[rowindex].Cells["交易流水号"].Value.ToString().Trim();
                string tradeType = dgvTradeInfo.Rows[rowindex].Cells["交易类型"].Value.ToString().Trim();
                string FeeNo = "0";
                if (!tradeType.Contains("挂号"))
                {
                    FeeNo = dgvTradeInfo.Rows[rowindex].Cells["收费单据号"].Value.ToString().Trim();
                    InvokeController("MZ_RePrintMXInvoice", tradeNo, FeeNo);
                }                
            }
        }
        private void buttonX3_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }
        private void BtnQueryByCard_Click(object sender, EventArgs e)
        {
            string sCardNo = txtCardNo.Text.Trim();
            DateTime dTime = dateTimePicker1.Value;
            if (sCardNo.Equals(""))
            {
                MessageBox.Show("请先读卡！");
                return;
            }
            
            InvokeController("Mz_GetTradeInfoByCard", sCardNo, dTime);
        }

        private void cbTradeNo_CheckedChanged(object sender, EventArgs e)
        {
            txtTradeNo.Enabled = cbTradeNo.Checked;
        }

        public void LoadTradeInfo(DataTable dt)
        {
            dgvTradeInfo.DataSource = dt;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            txtIdCard.Text = "";
            txtNo.Text = "";
            txtName.Text = "";
            txtSex.Text = "";
            txtIdCard.Text = "";
            txtBirth.Text = "";
            InvokeController("Mz_GetCardInfo", "");
        }

        public void LoadCatalogInfo(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                txtCardNo.Text = dt.Rows[0]["card_no"].ToString();
                txtNo.Text = dt.Rows[0]["ic_no"].ToString();
                txtName.Text = dt.Rows[0]["personname"].ToString();
                txtSex.Text = dt.Rows[0]["sex"].ToString().Trim()=="1"?"男":"女";
                txtIdCard.Text = dt.Rows[0]["id_no"].ToString();
                txtBirth.Text = dt.Rows[0]["birthday"].ToString();
            }
        }

        private void btnExportJzxx_Click(object sender, EventArgs e)
        {
            InvokeController("MZ_ExportJzxx", dateTimePicker1.Value);
        }
    }
}
