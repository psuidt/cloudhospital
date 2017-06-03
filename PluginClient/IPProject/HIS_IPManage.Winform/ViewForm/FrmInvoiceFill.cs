using System;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    public partial class FrmInvoiceFill : BaseFormBusiness, IInvoiceFill
    {
        /// <summary>
        /// 补打发票号
        /// </summary>
        public string InvoiceNO
        {
            get
            {
                return txtInvoiceNO.Text.Trim();
            }
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmInvoiceFill()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体打开设置焦点
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmInvoiceFill_Shown(object sender, EventArgs e)
        {
            txtInvoiceNO.Text = string.Empty;
            txtInvoiceNO.Focus();
        }

        /// <summary>
        /// 补打发票
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInvoiceNO.Text.Trim()))
            {
                InvokeController("MessageShow", "请输入新的发票号！");
                return;
            }

            InvokeController("InvoiceFill");
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        public void CloseForm()
        {
            this.Close();
        }
    }
}
