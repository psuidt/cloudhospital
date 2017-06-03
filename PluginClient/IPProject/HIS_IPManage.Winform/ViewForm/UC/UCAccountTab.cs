using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    /// <summary>
    /// 缴款控件
    /// </summary>
    public partial class UCAccountTab : UserControl, IBaseViewBusiness, IUCAccountTab
    {
        /// <summary>
        /// 控件关联控制器
        /// </summary>
        private ControllerEventHandler invokeController;

        /// <summary>
        /// 总费用
        /// </summary>
        private decimal dTotalFee = 0;

        /// <summary>
        /// 总费用
        /// </summary>
        public decimal DTotalFee
        {
            get
            {
                return dTotalFee;
            }

            set
            {
                dTotalFee = value;
            }
        }

        /// <summary>
        /// 支付方式合计
        /// </summary>
        private decimal dTotalPaymentFee = 0;

        /// <summary>
        /// 支付方式合计
        /// </summary>
        public decimal DTotalPaymentFee
        {
            get
            {
                return dTotalPaymentFee;
            }

            set
            {
                dTotalPaymentFee = value;
            }
        }

        /// <summary>
        /// 凑整金额
        /// </summary>
        private decimal dTotalRoundFee = 0;

        /// <summary>
        /// 凑整金额
        /// </summary>
        public decimal DTotalRoundFee
        {
            get
            {
                return dTotalRoundFee;
            }

            set
            {
                dTotalRoundFee = value;
            }
        }

        /// <summary>
        /// 缴款ID
        /// </summary>
        private int iAccountId;

        /// <summary>
        /// 缴款ID
        /// </summary>
        public int AccountId
        {
            get
            {
                return iAccountId;
            }

            set
            {
                iAccountId = value;
            }
        }

        /// <summary>
        /// 缴款类型Tab
        /// </summary>
        public int TabSelectIndex
        {
            set
            {
                tabControl1.SelectedTabIndex = value;
            }
        }

        /// <summary>
        /// 发票总数
        /// </summary>
        public DataTable DTInvCount
        {
            set
            {
                DataTable dtFPSum = value;
                dgvInvCount.DataSource = dtFPSum;

                decimal dTotalFee = 0;
                decimal dTotalDeposit = 0;
                decimal dTotalCashFee = 0;
                decimal dTotalPosFee = 0;
                decimal dTotalPromFee = 0;
                decimal dTotalRoundingFee = 0;
                decimal dTotalowFee = 0;
                dgvInvCount.DataSource = dtFPSum;

                #region 填充发票总数表格
                //填充发票总数表格        
                if (dtFPSum != null)
                {
                    foreach (DataRow dr in dtFPSum.Rows)
                    {
                        dTotalFee += Convert.ToDecimal(dr["TotalFee"]);
                        dTotalDeposit += Convert.ToDecimal(dr["TotalDeptositFee"]);
                        dTotalCashFee += Convert.ToDecimal(dr["TotalCashFee"]);
                        dTotalPosFee += Convert.ToDecimal(dr["TotalPosFee"]);
                        dTotalPromFee += Convert.ToDecimal(dr["TotalPromFee"]);
                        dTotalRoundingFee += Convert.ToDecimal(dr["TotalRoundingFee"]);
                        dTotalowFee += Convert.ToDecimal(dr["TotalowFee"]);
                    }
                }

                txtDeposit.Text = dTotalDeposit.ToString("0.00");
                txtCash.Text = dTotalCashFee.ToString("0.00");
                txtPos.Text = dTotalPosFee.ToString("0.00");
                txtProm.Text = dTotalPromFee.ToString("0.00");
                txtRound.Text = dTotalRoundingFee.ToString("0.00");
                txtTotal.Text = dTotalFee.ToString("0.00");
                txtPayment.Text = (dTotalCashFee - dTotalowFee).ToString("0.00");
                txtOW.Text = dTotalowFee.ToString("0.00");
                #endregion                

                DTotalFee = dTotalFee;
                DTotalPaymentFee = dTotalCashFee;
                DTotalRoundFee = dTotalRoundingFee;
            }
        }

        /// <summary>
        /// 票据明细
        /// </summary>
        public DataTable DTInvoiceDetail
        {
            set
            {
                DataTable dtFPClass = value;
                dgvInvoiceDetail.DataSource = dtFPClass;
            }
        }

        /// <summary>
        /// 缴款数据
        /// </summary>
        public DataTable DTAccount
        {
            set
            {
                DataTable dtAccountClass = value;
                dgvAccount.DataSource = dtAccountClass;
                decimal dAccountSum = 0;
                if (dtAccountClass != null)
                {
                    foreach (DataRow drAccount in dtAccountClass.Rows)
                    {
                        dAccountSum += Convert.ToDecimal(drAccount["paymentmoney"]);
                    }
                }

                txtAccountSum.Text = dAccountSum.ToString("0.00");
            }
        }

        /// <summary>
        /// 预交金数据
        /// </summary>
        public DataTable DTDepositList
        {
            set
            {
                DataTable dtDepositList = value;
                decimal dTotalDe = 0;
                decimal dDeCash = 0;
                decimal dDeRefund = 0;

                dgvDepositList.DataSource = dtDepositList;
                if (dgvDepositList != null)
                {
                    foreach (DataGridViewRow dgvr in dgvDepositList.Rows)
                    {
                        dTotalDe += Convert.ToDecimal(dgvr.Cells["TotalFee"].Value);

                        if (dgvr.Cells["Status"].Value.ToString().Contains("2"))
                        {
                            dgvr.DefaultCellStyle.ForeColor = Color.Red;
                            dDeRefund += dgvr.Cells["payType"].Value.ToString().Contains("现金") ? Convert.ToDecimal(dgvr.Cells["TotalFee"].Value) : 0;
                        }
                        else if (dgvr.Cells["Status"].Value.ToString().Contains("1"))
                        {
                            dDeCash += dgvr.Cells["payType"].Value.ToString().Contains("现金") ? Convert.ToDecimal(dgvr.Cells["TotalFee"].Value) : 0;
                            dgvr.DefaultCellStyle.ForeColor = Color.Blue;
                        }
                        else
                        {
                            dDeCash += dgvr.Cells["payType"].Value.ToString().Contains("现金") ? Convert.ToDecimal(dgvr.Cells["TotalFee"].Value) : 0;
                            dgvr.DefaultCellStyle.ForeColor = Color.Black;
                        }
                    }
                }

                txtTotalDe.Text = dTotalDe.ToString("0.00");
                txtDeCash.Text = dDeCash.ToString("0.00");
                txtDeRefund.Text = (dDeRefund * -1).ToString("0.00");
                txtDePayment.Text = (dDeCash - (dDeRefund * -1)).ToString("0.00");

                DTotalFee = dTotalDe;
                DTotalPaymentFee = (dDeCash - (dDeRefund * -1));
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public UCAccountTab()
        {
            InitializeComponent();
            tabItem1.Tag = 0;
            tabItem2.Tag = 1;
        }

        /// <summary>
        /// 控件初始化
        /// </summary>
        public void InitUC()
        {
            dgvInvCount.AutoGenerateColumns = false;
            dgvInvoiceDetail.AutoGenerateColumns = false;
            dgvAccount.AutoGenerateColumns = false;
            dgvDepositList.AutoGenerateColumns = false;
            AccountId = 0;
            DTInvCount = null;
            DTInvoiceDetail = null;
            DTAccount = null;
            DTDepositList = null;

            DTotalRoundFee = 0;
            DTotalFee = 0;
            DTotalPaymentFee = 0;

            TabSelectIndex = 0;
        }

        /// <summary>
        /// 选中票据
        /// </summary>
        /// <param name="sender">触发事件的按钮</param>
        /// <param name="e">事件参数</param>
        private void dgvInvCount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvInvCount.CurrentCell != null && e.RowIndex > -1)
            {
                if (e.ColumnIndex == invoiceAllcount.Index || e.ColumnIndex == refundinvoicecount.Index)
                {
                    if (Convert.ToInt32(dgvInvCount[dgvInvCount.CurrentCell.ColumnIndex, dgvInvCount.CurrentCell.RowIndex].Value) != 0)
                    {
                        int invoiceID = Convert.ToInt32(dgvInvCount["InvoiceID", e.RowIndex].Value);
                        int invoiceType = 0;

                        if (e.ColumnIndex == refundinvoicecount.Index)
                        {
                            //退票
                            invoiceType = 1;
                        }

                        InvokeController("GetInvoiceDetail", AccountId, invoiceID, invoiceType);
                    }
                }
            }
        }

        /// <summary>
        /// 界面名
        /// </summary>
        private string frmname = "UCAccountTab";

        /// <summary>
        /// 界面名
        /// </summary>
        public string frmName
        {
            get
            {
                return frmname;
            }

            set
            {
                frmname = value;
            }
        }

        /// <summary>
        /// 控制器
        /// </summary>
        public ControllerEventHandler InvokeController
        {
            get
            {
                return invokeController;
            }

            set
            {
                invokeController = value;
            }
        }

        /// <summary>
        /// Tab选中事件
        /// </summary>
        /// <param name="sender">触发事件的按钮</param>
        /// <param name="e">事件参数</param>
        private void tabControl1_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
        {
            tabControl1.TabsVisible = false;
            tabControl1.SelectedTab.Visible = true;
        }
    }
}
