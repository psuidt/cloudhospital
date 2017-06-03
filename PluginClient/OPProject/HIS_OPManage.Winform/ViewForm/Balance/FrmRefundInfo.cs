using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.OPManage;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    public partial class FrmRefundInfo : BaseFormBusiness,IFrmRefundInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmRefundInfo()
        {
            InitializeComponent();
        }
        #region 接口实现
        /// <summary>
        /// 退费处方
        /// </summary>
        private List<Prescription> balancePresc;

        /// <summary>
        /// 退费处方
        /// </summary>
        public List<Prescription> BalancePresc
        {
            get
            {
                return balancePresc;
            }

            set
            {
                balancePresc = value;
            }
        }

        /// <summary>
        /// 结算总金额
        /// </summary>
        public string CostTotalFee
        {
            get
            {
                return txtTotalFee.Text.Trim();
            }

            set
            {
                txtTotalFee.Text = value;
            }
        }

        /// <summary>
        /// 是否全退
        /// </summary>
        private bool isrefundall;

        /// <summary>
        /// 是否全退
        /// </summary>
        public bool IsRefundAll
        {
            get
            {
                return isrefundall;
            }

            set
            {
                isrefundall = value;
            }
        }

        /// <summary>
        /// 退票据号
        /// </summary>
        public string RefundInvoiceNO
        {
            get
            {
                return txtInvoice.Text.Trim();
            }

            set
            {
                txtInvoice.Text = value;
            }
        }

        /// <summary>
        /// 支付方式信息
        /// </summary>
        public string StrPayInfo
        {
            get
            {
                return lblPayInfo.Text.Trim();
            }

            set
            {
                lblPayInfo.Text = value;
            }
        }

        /// <summary>
        /// 退现金
        /// </summary>
        public string StrRefundCash
        {
            get
            {
                return txtRefundCash.Text.Trim();
            }

            set
            {
                txtRefundCash.Text = value;
            }
        }

        /// <summary>
        ///  是否是医保病人
        /// </summary>
        private bool ismediapat;

        /// <summary>
        ///  是否是医保病人
        /// </summary>
        public bool IsMediaPat
        {
            get
            {
                return ismediapat;
            }

            set
            {
                ismediapat = value;
            }
        }

        /// <summary>
        /// 退费结算ID
        /// </summary>
        private int refundcostheadid;

        /// <summary>
        /// 退费结算ID
        /// </summary>
        public int RefundCostHeadID
        {
            get
            {
                return refundcostheadid;
            }

            set
            {
                refundcostheadid = value;
            }
        }

        /// <summary>
        /// 设置显示医保刷卡信息
        /// </summary>
        public string StrMediaReadInfo
        {
            get
            {
                return txtMediaInfo.Text.Trim();
            }

            set
            {
                txtMediaInfo.Text = value;
            }
        }
        #endregion

        /// <summary>
        /// 半闭按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 窗体Load事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmRefundInfo_Load(object sender, EventArgs e)
        {
            txtMediaInfo.Clear();
            if (ismediapat)
            {
                btnReadMediaCard.Enabled = true;
            }
            else
            {
                btnReadMediaCard.Enabled = false;
            }
        }

        /// <summary>
        /// 退费按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnRefund_Click(object sender, EventArgs e)
        {
            if (IsMediaPat)
            {
                if (txtMediaInfo.Text.Trim() == string.Empty)
                {
                    MessageBoxEx.Show("因原交费有医保支付，需要刷医保卡才能退费");
                    return;
                }
            }

            if ((bool)InvokeController("RefundFee"))
            {
                this.Close();
                if (balancePresc != null && balancePresc.Count > 0)
                {
                    InvokeController("PayInfo", 1);
                }
                else
                {
                    InvokeController("ClearInfo");
                }
            }
        }

        /// <summary>
        /// 医保读卡按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnReadMediaCard_Click(object sender, EventArgs e)
        {
            InvokeController("RefundReadMediaCard");
        }

        /// <summary>
        ///  窗体KeyUp事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmRefundInfo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
