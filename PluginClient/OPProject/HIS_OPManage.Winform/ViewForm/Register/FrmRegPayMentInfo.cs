using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    public partial class FrmRegPayMentInfo : BaseFormBusiness,IFrmRegPayMentInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmRegPayMentInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  实付金额
        /// </summary>
        public decimal ActPay
        {
            get
            {
                return Convert.ToDecimal(txtActPayFee.Text);
            }

            set
            {
                txtActPayFee.Text = value.ToString();
            }
        }

        /// <summary>
        ///  优惠金额
        /// </summary>
        public decimal PromFee
        {
            get
            {
                return Convert.ToDecimal(txtPromFee.Text);
            }

            set
            {
                txtPromFee.Text = value.ToString();
            }
        }

        /// <summary>
        /// 医保支付金额
        /// </summary>
        public decimal MedicarePay
        {
            get
            {
                return Convert.ToDecimal(txtInsurePay.Text);
            }

            set
            {
                string s= value.ToString();
                txtInsurePay.Text = s;
            }
        }

        /// <summary>
        /// 医保统筹金额
        /// </summary>
        public decimal MedicareMIPay
        {
            get
            {
                return Convert.ToDecimal(txtInsureMIPay.Text);
            }

            set
            {
                txtInsureMIPay.Text = value.ToString();
            }
        }

        /// <summary>
        /// 医保个帐金额
        /// </summary>
        public decimal MedicarePersPay
        {
            get
            {
                return Convert.ToDecimal(txtInsurePersPay.Text);
            }

            set
            {
                txtInsurePersPay.Text = value.ToString();
            }
        }
        /// <summary>
        /// 当前发票号
        /// </summary>
        public string InvoiceNO
        {
            get
            {
                return txtInvoiceNO.Text;
            }

            set
            {
                txtInvoiceNO.Text = value;
            }
        }

        /// <summary>
        /// 挂号科室
        /// </summary>
        public string RegDeptName
        {
            get
            {
                return txtRegDept.Text;
            }

            set
            {
                txtRegDept.Text = value;
            }
        }

        /// <summary>
        /// 挂号金额
        /// </summary>
        public decimal RegTotalFee
        {
            get
            {
                return  Convert.ToDecimal( txtRegTotalFee.Text);
            }

            set
            {
                txtRegTotalFee.Text = value.ToString();
            }
        }

        /// <summary>
        /// 应付金额
        /// </summary>
        public decimal ShoudPay
        {
            get
            {
                return Convert.ToDecimal(txtShouldPayFee.Text);
            }

            set
            {
                txtShouldPayFee.Text = value.ToString();
            }
        }

        /// <summary>
        /// 医保预算ID
        /// </summary>
        private int miBudgetid;

        /// <summary>
        /// 医保预算ID
        /// </summary>
        public int MIBudgetID
        {
            get
            {
                return miBudgetid;
            }

            set
            {
                miBudgetid = value;
            }
        }

        /// <summary>
        /// 医保卡号
        /// </summary>
        private string sSocialCard =string.Empty;

        /// <summary>
        /// 医保卡号
        /// </summary>
        public string SocialCard
        {
            get
            {
                return sSocialCard;
            }

            set
            {
                sSocialCard = value;
            }
        }

        /// <summary>
        /// 医保应用号
        /// </summary>
        private string sIdentityNum = string.Empty;

        /// <summary>
        /// 医保应用号
        /// </summary>
        public string IdentityNum
        {
            get
            {
                return sIdentityNum;
            }
            set
            {
                sIdentityNum = value;
            }
        }

        /// <summary>
        /// 获取支付方式ID
        /// </summary>
        public string GetPatMentCode
        {
            get
            {
                return cmbPayMentType.SelectedValue.ToString();
            }
        }

        /// <summary>
        /// 医保试算按钮是否可用
        /// </summary>
        public bool ReadMedicareEnabled
        {
            set
            {
                btnReadMedicareCard.Enabled = value;
            }
        }

        /// <summary>
        ///  exepanel是否可见
        /// </summary>
       public bool ExpMedicardInfoVisible
        {
            set
            {
                expMedicareInfo.Visible = value;
            }
        }

        /// <summary>
        /// 医保试算信息
        /// </summary>
        public string SetMedicardInfo
        {
            set
            {
                rTxtInsureInfo.Text = value;
                expMedicareInfo.Expanded = true;
            }
        }

        /// <summary>
        /// 支付方式
        /// </summary>
        /// <param name="dtRegPayment">支付方式列表</param> 
        public void GetRegPayment(DataTable dtRegPayment)
        {
            cmbPayMentType.DataSource = dtRegPayment;
            cmbPayMentType.DisplayMember = "Name";
            cmbPayMentType.ValueMember = "Code";
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
        /// 实收金额KeyDown事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void txtActPayFee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.Focus();
            }
        }

        /// <summary>
        /// 医保读卡按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnReadInsureCard_Click(object sender, EventArgs e)
        {
            expMedicareInfo.Expanded = true;
            InvokeController("GetRegMedicareCaculate");
        }

        /// <summary>
        /// 医保金额变化事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void txtInsurePay_TextChanged(object sender, EventArgs e)
        {
            decimal lastFee = Convert.ToDecimal(txtRegTotalFee.Text) - Convert.ToDecimal(txtInsurePay.Text);
            if (lastFee < 5)
            {
                txtPromFee.Text = lastFee.ToString();
            }
            txtShouldPayFee.Text = (lastFee - Convert.ToDecimal(txtPromFee.Text.Trim())).ToString();
            if (Convert.ToDecimal(txtShouldPayFee.Text) == 0)
            {
                btnOK.Focus();
            }

            txtActPayFee.Text = txtShouldPayFee.Text;
            btnOK.Focus();
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtActPayFee.Text ==string.Empty)
            {
                MessageBoxEx.Show("请输入实收金额");
                return;
            }

            if (Convert.ToDecimal(txtActPayFee.Text) < Convert.ToDecimal(txtShouldPayFee.Text))
            {
                MessageBoxEx.Show("实收金额不能小于应收金额");
                return;
            }

           bool result=(bool)InvokeController("SaveRegister");
            if (result)
            {
                this.DialogResult = DialogResult.OK;
                InvokeController("RegComplete");
                this.Close();
            }
        }

        /// <summary>
        /// 窗体Activated事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmRegPayMentInfo_Activated(object sender, EventArgs e)
        {
           // txtActPayFee.Text = "0";
            txtChangeFee.Text = "0";
            if (Convert.ToDecimal(txtInsurePay.Text) == Convert.ToDecimal(txtRegTotalFee.Text))
            {
                btnOK.Focus();
            }
            else
            {
                txtActPayFee.Focus();
            }
        }

        /// <summary>
        /// 窗体KeyUp事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmRegPayMentInfo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// 实付金额改变事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void txtActPayFee_ValueChanged(object sender, EventArgs e)
        {
            decimal actfee = 0;
            try
            {
                actfee = Convert.ToDecimal(txtActPayFee.Text);
            }
            catch
            {
                actfee = 0;
            }

            txtChangeFee.Text = (actfee - Convert.ToDecimal(txtShouldPayFee.Text)).ToString();
        }
    }
}
