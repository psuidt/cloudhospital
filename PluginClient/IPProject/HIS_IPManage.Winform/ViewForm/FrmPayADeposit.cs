using System;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.IPManage;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    /// <summary>
    /// 预交金收取弹出界面
    /// </summary>
    public partial class FrmPayADeposit : BaseFormBusiness, IPayADeposit
    {
        #region "属性"
        /// <summary>
        /// 预交金
        /// </summary>
        private IP_DepositList depositList = new IP_DepositList();

        /// <summary>
        /// 预交金
        /// </summary>
        public IP_DepositList DepositList
        {
            get
            {
                depositList.InvoiceNO = txtBillNumber.Text.Trim(); // 票据号
                if (cboPaymentMethod.SelectedValue != null)
                {
                    depositList.PayType = cboPaymentMethod.SelectedValue.ToString().Trim();
                }

                if (!string.IsNullOrEmpty(txtTotalFee.Text.Trim()))
                {
                    depositList.TotalFee = decimal.Parse(txtTotalFee.Text.Trim());
                }

                return depositList;
            }

            set
            {
                depositList = value;
            }
        }
        #endregion

        #region "事件"
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmPayADeposit()
        {
            InitializeComponent();
            frmPay.AddItem(cboPaymentMethod, string.Empty);
            frmPay.AddItem(txtTotalFee, string.Empty);
            frmPay.AddItem(btnSave, string.Empty);
            cboPaymentMethod.Focus();
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void PayADepositForm_Load(object sender, EventArgs e)
        {
            // 支付方式
            InvokeController("GetPaymentMethod");
            txtTotalFee.Text = 0.ToString();
            txtTotalFee.Focus();
            // 票据号
            InvokeController("GetInvoiceCurNO");
        }

        /// <summary>
        /// 关闭预交金收取界面
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClosePayADposit();
        }

        /// <summary>
        /// 保存预交金
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            InvokeController("PayADeposit");
        }
        #endregion

        #region "私有方法" 

        /// <summary>
        /// 关闭预交金收取界面
        /// </summary>
        public void ClosePayADposit()
        {
            cboPaymentMethod.Focus();
            this.Close();
        }

        /// <summary>
        /// 设置预交金票据号
        /// </summary>
        /// <param name="billNumber">预交金票据号</param>
        public void SetBillNumber(string billNumber)
        {
            txtBillNumber.Text = billNumber.ToString();
        }

        /// <summary>
        /// 绑定预交金支付方式
        /// </summary>
        /// <param name="patMethodDt">预交金支付方式</param>
        public void Binding_PaymentMethod(DataTable patMethodDt)
        {
            cboPaymentMethod.DataSource = patMethodDt;
            cboPaymentMethod.ValueMember = "Code";
            cboPaymentMethod.DisplayMember = "Name";
            cboPaymentMethod.SelectedIndex = 0;
        }

        /// <summary>
        /// 验证预交金格式
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void txtTotalFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' && this.txtTotalFee.Text.IndexOf(".") != -1)
            {
                e.Handled = true;
            }

            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == '.' || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }
        #endregion

        /// <summary>
        /// 打开预交金收取界面设置焦点
        /// </summary>
        /// <param name="sender">FrmPayADeposit</param>
        /// <param name="e">事件参数</param>
        private void FrmPayADeposit_Shown(object sender, EventArgs e)
        {
            txtTotalFee.Focus();
        }
    }
}