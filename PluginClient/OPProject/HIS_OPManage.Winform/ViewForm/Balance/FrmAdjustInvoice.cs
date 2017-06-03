using System;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    public partial class FrmAdjustInvoice : BaseFormBusiness, IFrmAdjustInvoice
    {
        /// <summary>
        /// 票据号调整构造函数
        /// </summary>
        public FrmAdjustInvoice()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 当前票据号
        /// </summary>
        public string curInvoiceNO
        {
            get
            {
                return txtCurInvoiceNO.Text.Trim();
            }

            set
            {
                txtCurInvoiceNO.Text = value;
            }
        }

        /// <summary>
        /// 窗体Load事件
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">事件能数</param>
        private void FrmAdjustInvoice_Load(object sender, EventArgs e)
        {
            InvokeController("AdjustInvoiceInit");
            txtPerfChar.Text =string.Empty;
            txtNO.Text =string.Empty;          
        }

        /// <summary>
        /// 确定事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtPerfChar.Text.Trim() ==string.Empty)
            {
                MessageBoxEx.Show("请输入新票号前缀");
                txtPerfChar.Focus();
                return;
            }

            if (txtNO.Text.Trim() ==string.Empty)
            {
                MessageBoxEx.Show("请输入新票号");
                txtNO.Focus();
                return;
            }

            if ((bool)InvokeController("AdjustInvoiceSet", txtPerfChar.Text.Trim(), txtNO.Text.Trim()))
            {
                this.Close();
            }
        }

        /// <summary>
        /// 票据前缀回车事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void txtPerfChar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtPerfChar.Text.Trim() !=string.Empty)
                {
                    txtNO.Focus();
                }
            }
        }

        /// <summary>
        /// 票据号回车事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
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
        /// 取消事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 窗体Shown事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">按件参数</param>
        private void FrmAdjustInvoice_Shown(object sender, EventArgs e)
        {
            txtPerfChar.Focus();
        }
    }
}
