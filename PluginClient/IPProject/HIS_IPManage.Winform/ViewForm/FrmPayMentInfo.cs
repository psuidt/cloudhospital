using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EfwControls.HISControl.UCPayMode;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.IPManage;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    public partial class FrmPayMentInfo : BaseFormBusiness, IPayMentInfo
    {
        #region 接口参数

        /// <summary>
        ///  结算病人登记ID
        /// </summary>
        private int mCurPatListID;

        /// <summary>
        /// 结算病人登记ID
        /// </summary>
        public int CurPatListID
        {
            get
            {
                return mCurPatListID;
            }

            set
            {
                mCurPatListID = value;
            }
        }

        /// <summary>
        /// 结算病人类型ID
        /// </summary>
        private int mCostPatTypeID;

        /// <summary>
        /// 结算病人类型ID
        /// </summary>
        public int CostPatTypeID
        {
            get
            {
                return mCostPatTypeID;
            }

            set
            {
                mCostPatTypeID = value;
            }
        }

        /// <summary>
        /// 住院费用总金额
        /// </summary>
        private decimal mTotalFee;

        /// <summary>
        /// 住院费用总金额
        /// </summary>
        public decimal TotalFee
        {
            get
            {
                return mTotalFee;
            }

            set
            {
                mTotalFee = value;
            }
        }

        /// <summary>
        /// 住院病人预交金总额
        /// </summary>
        private decimal mDepositFee;

        /// <summary>
        /// 住院病人预交金总额
        /// </summary>
        public decimal DepositFee
        {
            get
            {
                return mDepositFee;
            }

            set
            {
                mDepositFee = value;
            }
        }

        /// <summary>
        /// 住院结算主表
        /// </summary>
        private IP_CostHead mCostHead = new IP_CostHead();

        /// <summary>
        /// 住院结算主表
        /// </summary>
        public IP_CostHead CostHead
        {
            get
            {
                return mCostHead;
            }

            set
            {
                mCostHead = value;
            }
        }

        /// <summary>
        /// 支付方式记录表
        /// </summary>
        private List<IP_CostPayment> mCostPayList = new List<IP_CostPayment>();

        /// <summary>
        /// 支付方式记录表
        /// </summary>
        public List<IP_CostPayment> CostPayList
        {
            get
            {
                return mCostPayList;
            }

            set
            {
                mCostPayList = value;
            }
        }

        /// <summary>
        /// 费用结算情况
        /// </summary>
        private CostFeeStyle mCostFee = new CostFeeStyle();

        /// <summary>
        /// 费用结算情况
        /// </summary>
        public CostFeeStyle CostFee
        {
            get
            {
                return mCostFee;
            }

            set
            {
                mCostFee = value;
            }
        }

        /// <summary>
        /// 结算类型
        /// </summary>
        private int mCostType;

        /// <summary>
        /// 结算类型
        /// </summary>
        public int CostType
        {
            get
            {
                return mCostType;
            }

            set
            {
                mCostType = value;
            }
        }

        /// <summary>
        /// 优惠金额
        /// </summary>
        private decimal mPromFee;

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal PromFee
        {
            get
            {
                return mPromFee;
            }

            set
            {
                mPromFee = value;
            }
        }

        #endregion

        #region 窗口初始化

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmPayMentInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmPayMentInfo_Load(object sender, EventArgs e)
        {
            // 获取WorkID
            int workid = (InvokeController("this") as AbstractController).WorkId;
            // 支付控件数
            int iPayControls = CostPayManager.InitUCPayModeControl(workid, flpPayCtrl, mCostPatTypeID, 1);
            if (iPayControls > 0)
            {
                //根据支付方式控件多少，自动设置界面高度
                Height = groupPanel2.Location.Y + 120 + Math.Max(2, iPayControls) * 40 + 15;
                groupPanel2.Height = Math.Max(2, iPayControls) * 40 + 32 + 15;
            }
            //实列化委托，各项金额实时显示
            SetFeeValueDelegate setFeeValue = new SetFeeValueDelegate(SetLabelText);
            //实列化对象，支付方式金额计算
            PaymentAutoCalculate autoCalculate = new PaymentAutoCalculate(mCurPatListID, mCostPatTypeID, null, InvokeController);
            // 自动计算支付金额
            CostPayManager.StartExecPay(mTotalFee, mDepositFee, autoCalculate, setFeeValue, false);

            if (CostHead.CostType == 3)
            {
                CostPayManager.ArrearageCost();
            }
        }

        #endregion

        /// <summary>
        /// 实时显示各项金额
        /// </summary>
        /// <param name="costFee">各项金额对象</param>
        private void SetLabelText(CostFeeStyle costFee)
        {
            txtAmount.Text = string.Format("{0:N}", costFee.PayTotalFee);  // 住院总金额
            txtFavorableSum.Text = string.Format("{0:N}", costFee.FavorableTotalFee);  // 优惠金额
            txtPersonalSum.Text = string.Format("{0:N}", costFee.SelfTotalFee);  // 自付金额
            txtAccountSum.Text = string.Format("{0:N}", costFee.AccountTotalFee);  // 记账金额
            txtPaymentTotal.Text = string.Format("{0:N}", costFee.zyDepositFee);  // 预交金总额
            txtTheKnotComplement.Text = string.Format("{0:N}", costFee.zyChargeFee);  // 结补
            txtBack.Text = string.Format("{0:N}", costFee.zyRefundFee);  // 结退
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (mCostHead.CostType == 3)
            {
                if (CostPayManager.CostFee.zyRefundFee > 0)
                {
                    InvokeController("MessageShow", "当前病人的预交金余额大于费用总金额，无法进行前欠费结算，请重新选择出院结算！");
                    return;
                }
            }

            if (CostPayManager.CostFee.ChangeFee < 0)
            {
                InvokeController("MessageShow", "支付金额不足!");
                return;
            }

            if (CostPayManager.CostFee.SelfTotalFee + CostPayManager.CostFee.RoundFee < 0)
            {
                InvokeController("MessageShow", "记账金额加优惠金额不能超过总金额!");
                return;
            }

            if (CostPayManager.CostFee.AccountTotalFee > CostPayManager.CostFee.PayTotalFee)
            {
                InvokeController("MessageShow", "总记账支付金额不能超过总金额");
                return;
            }

            // 保存支付方式数据
            foreach (PayModeFee pay in CostPayManager.CostFee.payList)
            {
                if (pay.PayFee != 0)
                {
                    IP_CostPayment payment = new IP_CostPayment();
                    payment.CostHeadID = 0;
                    payment.PatListID = mCurPatListID;
                    payment.PaymentID = pay.PayMethodID;
                    payment.PatTypeID = mCostPatTypeID;
                    //payment.PayName = pay.;
                    payment.CostMoney = pay.PayFee;
                    mCostPayList.Add(payment);
                }
            }

            // 计算预交金结余
            // 结补和结退都=0时预交金余额为0
            if (Convert.ToDecimal(txtTheKnotComplement.Text.Trim()) != 0
                || Convert.ToDecimal(txtBack.Text.Trim()) != 0)
            {
                if (Convert.ToDecimal(txtTheKnotComplement.Text.Trim()) != 0)
                {
                    mCostHead.BalanceFee = 0 - Convert.ToDecimal(txtTheKnotComplement.Text.Trim());
                }
                else if (Convert.ToDecimal(txtBack.Text.Trim()) != 0)
                {
                    mCostHead.BalanceFee = Convert.ToDecimal(txtBack.Text.Trim());
                }
            }

            mCostHead.CashFee = CostPayManager.CostFee.CashFee;//现金
            mCostHead.PosFee = CostPayManager.CostFee.PosFee;    // POS金额
            mCostHead.PromFee = CostPayManager.CostFee.FavorableTotalFee;  // 优惠金额
            mPromFee = CostPayManager.CostFee.FavorableTotalFee;  // 优惠金额
            mCostHead.RoundingFee = CostPayManager.CostFee.RoundFee;  // 凑整金额
            mCostFee.PayTotalFee = CostPayManager.CostFee.PayTotalFee;  // 费用金额
            mCostFee.FavorableTotalFee = CostPayManager.CostFee.FavorableTotalFee;  // 优惠金额
            mCostFee.PosFee = CostPayManager.CostFee.PosFee;    // POS金额
            mCostFee.CashFee = CostPayManager.CostFee.CashFee;//现金
            mCostFee.RoundFee = CostPayManager.CostFee.RoundFee;  // 凑整金额
            mCostFee.ChangeFee = CostPayManager.CostFee.ChangeFee;  // 找零金额
            mCostFee.SelfTotalFee = CostPayManager.CostFee.SelfTotalFee;  // 自付金额
            mCostFee.zyDepositFee = CostPayManager.CostFee.zyDepositFee;  // 预交金
            mCostFee.zyChargeFee = CostPayManager.CostFee.zyChargeFee;  // 结补
            mCostFee.zyRefundFee = CostPayManager.CostFee.zyRefundFee;  // 结退

            // 调用结算
            InvokeController("DischargeSetrlement");
        }

        /// <summary>
        /// 关闭结算窗体
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnGiveUp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 监听键盘事件
        /// </summary>
        /// <param name="keyData">Key</param>
        /// <returns>执行成功或失败</returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            bool bRet = false;
            switch (keyData)
            {
                case Keys.PageUp:
                    bRet = ProcessTabKey(false);
                    break;
                case Keys.PageDown:
                    bRet = ProcessTabKey(true);
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.Enter:
                    if (btnGiveUp.Focused == true)
                    {
                        btnGiveUp_Click(null, null);
                    }
                    else
                    {
                        if (btnConfirm.Focused == false)
                        { 
                            btnConfirm.Focus();
                        }
                        else
                        { 
                            btnConfirm_Click(null, null);
                        }
                    }

                    break;
                default:
                    bRet = base.ProcessDialogKey(keyData);
                    break;
            }

            return bRet;
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        public void CloseForm()
        {
            this.Close();
        }

        /// <summary>
        /// 界面显示时设置焦点
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmPayMentInfo_Shown(object sender, EventArgs e)
        {
            if (flpPayCtrl.Controls.Count > 0)
            {
                CostPayManager.CashFocus();
            }
            else
            {
                MessageBox.Show("请设置相关的支付类型");
            }
        }
    }
}

/// <summary>
/// 结算面板
/// </summary>
public class PaymentAutoCalculate : AsynPaymentCalculate
{
    /// <summary>
    /// 结算控制器
    /// </summary>
    private ControllerEventHandler invokeController;

    /// <summary>
    /// 第一次加载
    /// </summary>
    private bool isFirst;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="patListID">病人登记ID</param>
    /// <param name="iPatType">病人类型</param>
    /// <param name="feeIDs">费用ID集合</param>
    /// <param name="cInvokeController">控制器</param>
    public PaymentAutoCalculate(int patListID, int iPatType, int[] feeIDs, ControllerEventHandler cInvokeController) : base(patListID, iPatType, feeIDs)
    {
        isFirst = true;
        invokeController = cInvokeController;
    }

    /// <summary>
    /// 优惠计算
    /// </summary>
    /// <param name="ticketNo">支付方式名</param>
    /// <param name="procStep">支付方式步骤</param>
    /// <returns>优惠金额</returns>
    public override decimal FavorableCalculate(out string ticketNo, out PAY_PROCSTEP procStep)
    {
        ticketNo = "优惠计算";
        procStep = PAY_PROCSTEP.ppsHandFinsed;
        if (!isFirst)
        {
            decimal promFee = (decimal)invokeController("PromFeeCaculate");
            return promFee;
        }
        else
        {
            isFirst = false;
            return 0;
        }
    }
}
