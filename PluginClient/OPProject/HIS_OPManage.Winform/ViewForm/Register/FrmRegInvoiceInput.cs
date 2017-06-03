using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.OPManage;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    public partial class FrmRegInvoiceInput : BaseFormBusiness,IFrmRegInvoiceInput
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmRegInvoiceInput()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 退费成功与否
        /// </summary>
        private bool isbackregOK;

        /// <summary>
        /// 退费成功与否
        /// </summary>
        public bool IsBackRegOK
        {
            get
            {
                return isbackregOK;
            }
        }

        /// <summary>
        /// 退费票据号
        /// </summary>
        public string InputVoiceNO
        {
            get
            {
                return txtInvoiceNo.Text.Trim();
            }

            set
            {
                txtInvoiceNo.Text = value;
            }
        }

        /// <summary>
        /// 医保刷卡信息
        /// </summary>
        public string MedicareInfo
        {
            get
            {
                return txtMedicareCardInfo.Text.Trim();
            }

            set
            {
                txtMedicareCardInfo.Text = value;
            }
        }

        /// <summary>
        /// 退费挂号对象
        /// </summary>
        private OP_PatList regpatlist;

        /// <summary>
        /// 退费挂号对象
        /// </summary>
        public OP_PatList RegPatList
        {
            get
            {
                return regpatlist;
            }

            set
            {
                regpatlist = value;
            }
        }

        /// <summary>
        /// 支付方式
        /// </summary>
        /// <param name="dtPayInfo">支付方式数据</param>
        public void LoadPayInfo(DataTable dtPayInfo)
        {
            string str = string.Empty;
            for (int rowindex = 0; rowindex < dtPayInfo.Rows.Count; rowindex++)
            {
                str +="  "+ dtPayInfo.Rows[rowindex]["PayMentName"].ToString()+":";
                str += dtPayInfo.Rows[rowindex]["PayMentMoney"].ToString() + "元";
            }

            lblPayInfo.Text = str;
            lblPatInfo.Text = dtPayInfo.Rows[0]["PatName"].ToString();
        }

        /// <summary>
        /// 是否是医保病人
        /// </summary>
        private bool haveMedicarePay;

        /// <summary>
        /// 是否是医保病人
        /// </summary>
        public bool HaveMedicarePay
        {
            get
            {
                return haveMedicarePay;
            }

            set
            {
                haveMedicarePay = value;
            }
        }

        /// <summary>
        /// 票据号回车事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void txtInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtInvoiceNo.Text.Trim() != string.Empty)
                {
                    if ((bool)InvokeController("GetRegInfoByInvoiceNO"))
                    {
                        if (haveMedicarePay)
                        {
                            //有医保支付的需调医保接口
                            btnReadMedicareCard.Visible = true;
                            txtMedicareCardInfo.Visible = true;
                            btnReadMedicareCard.Focus();
                        }
                        else
                        {
                            btnOK.Focus();
                        }
                    }

                    btnOK.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 窗体Load事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmRegInvoiceInput_Load(object sender, EventArgs e)
        {
            InvokeController("InitFrmRegInvoice");
            lblPayInfo.Text = string.Empty;
            lblPatInfo.Text = string.Empty;
            btnReadMedicareCard.Visible = false;
            txtMedicareCardInfo.Clear();
            txtMedicareCardInfo.Visible = false;
            txtInvoiceNo.Focus();
            isbackregOK = false;
            btnOK.Enabled = false;
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
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtInvoiceNo.Text.Trim() == string.Empty)
            {
                MessageBoxEx.Show("请输入票据号");
                txtInvoiceNo.Focus();
                return;
            }

            if (haveMedicarePay)
            {
                if (txtMedicareCardInfo.Text.Trim() == string.Empty)
                {
                    MessageBoxEx.Show("原有医保支付的需要先读医保卡");
                    btnReadMedicareCard.Focus();
                    return;
                }
            }

            if ((bool)InvokeController("BackReg"))
            {
                isbackregOK = true;
                this.Close();
            }
            else
            {
                txtInvoiceNo.Focus();
            }
        }

        /// <summary>
        /// 医保读卡按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnReadMedicareCard_Click(object sender, EventArgs e)
        {
            string sPatName = lblPatInfo.Text;
            InvokeController("GetBacRegReadCard", sPatName);
        }

        /// <summary>
        /// 票据号KeyUp事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmRegInvoiceInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
