using System;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    public partial class FrmInputPrintInvoice : BaseFormBusiness, IFrmInputPrintInvoice
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmInputPrintInvoice()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体Load事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmInputPrintInvoice_Load(object sender, EventArgs e)
        {           
            txtPerfChar.Text = string.Empty;
            txtNO.Text = string.Empty;
            txtPerfChar.Focus();
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtPerfChar.Text.Trim() == string.Empty)
            {
                MessageBoxEx.Show("请输入票号前缀");
                txtPerfChar.Focus();
                return;
            }

            if (txtNO.Text.Trim() == string.Empty)
            {
                MessageBoxEx.Show("请输入新票号");
                txtNO.Focus();
                return;
            }

            if ((bool)InvokeController("PrintInvoiceAgain", txtPerfChar.Text.Trim(), txtNO.Text.Trim()))
            {
                this.Close();
            }
        }

        /// <summary>
        /// 票据前缀KeyUp事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void txtPerfChar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtPerfChar.Text.Trim() != string.Empty)
                {
                    txtNO.Focus();
                }
            }
        }

        /// <summary>
        /// 票据号KeyUp事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void txtNO_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(txtNO.Text.Trim()!=string.Empty)
                {
                    btnOK.Focus();
                }
            }
        }

        /// <summary>
        /// 取消按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 窗体KeyUp事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmInputPrintInvoice_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// 窗体Shown事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmInputPrintInvoice_Shown(object sender, EventArgs e)
        {
            txtPerfChar.Focus();
        }
    }
}
