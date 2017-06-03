using System;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    /// <summary>
    /// 发票号调整界面
    /// </summary>
    public partial class FrmAdjustInvoice : BaseFormBusiness, IAdjustInvoice
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmAdjustInvoice()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 当前票据号
        /// </summary>
        public string InvoiceNo
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
        /// 新票据号
        /// </summary>
        public string NewInvoiceNo
        {
            get
            {
                return txtNO.Text.Trim();
            }
        }

        /// <summary>
        /// 新票据号前缀
        /// </summary>
        public string NewPerfChar
        {
            get
            {
                return txtPerfChar.Text.Trim();
            }
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件参数</param>
        private void FrmAdjustInvoice_Load(object sender, EventArgs e)
        {
            txtPerfChar.Text = string.Empty;
            txtNO.Text = string.Empty;
        }

        /// <summary>
        /// 更换票据号
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件参数</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPerfChar.Text.Trim()))
            {
                MessageBoxEx.Show("请输入新票号前缀");
                txtPerfChar.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtNO.Text.Trim()))
            {
                MessageBoxEx.Show("请输入新票号");
                txtNO.Focus();
                return;
            }

            if ((bool)InvokeController("AdjustInvoiceSet"))
            {
                this.Close();
            }
        }

        /// <summary>
        /// 前缀文本框回车事件
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件参数</param>
        private void txtPerfChar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtPerfChar.Text.Trim()))
                {
                    txtNO.Focus();
                }
            }
        }

        /// <summary>
        /// 票号文本框回车事件
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件参数</param>
        private void txtNO_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtNO.Text.Trim()))
                {
                    btnOK.Focus();
                }
            }
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 打开界面设置焦点
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件参数</param>
        private void FrmAdjustInvoice_Shown(object sender, EventArgs e)
        {
            txtPerfChar.Focus();
        }
    }
}