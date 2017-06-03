using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using EfwControls.HISControl.UCPayMode;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.OPManage;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    public partial class FrmPayMentInfo : BaseFormBusiness, IFrmPayMentInfo
    {
        #region 接口参数
        /// <summary>
        /// 结算按大项目统计明细数据
        /// </summary>
        private DataTable dtinvoiceStatDetail;

        /// <summary>
        /// 结算按大项目统计明细数据
        /// </summary>
        public DataTable DtInvoiceStatDetail
        {
            get
            {
                return dtinvoiceStatDetail;
            }

            set
            {
                dtinvoiceStatDetail = value;
            }
        }

        /// <summary>
        /// 收费成功后返回的票据打印信息
        /// </summary>
        private ChargeInfo backchargeifno;

        /// <summary>
        /// 收费成功后返回的票据打印信息
        /// </summary>
        public ChargeInfo BackChargeInfo
        {
            get
            {
                return backchargeifno;
            }

            set
            {
                backchargeifno = value;
            }
        }

        /// <summary>
        /// 结算票据打印数据
        /// </summary>
        private DataTable dtprintInvoice;

        /// <summary>
        /// 结算票据打印数据
        /// </summary>
        public DataTable DtPrintInvoice
        {
            get
            {
                return dtprintInvoice;
            }

            set
            {
                dtprintInvoice = value;
            }
        }

        /// <summary>
        /// 结算收费明细打印数据
        /// </summary>
        private DataTable dtprintinvoiceDetail;

        /// <summary>
        /// 结算收费明细打印数据
        /// </summary>
        public DataTable DtPrintInvoiceDetail
        {
            get
            {
                return dtprintinvoiceDetail;
            }

            set
            {
                dtprintinvoiceDetail = value;
            }
        }

        /// <summary>
        /// 自付金额
        /// </summary>
        public decimal SelfFee
        {
            get
            {
                return  Convert.ToDecimal(txtAmount.Text)- Convert.ToDecimal(lbAccountSum.Text);
            }
        }

        /// <summary>
        /// 0正常收费，1退费后补收
        /// </summary>
        private int balancetype;

        /// <summary>
        /// 0正常收费 1退费后补收
        /// </summary>
        public int BalanceType
        {
            get
            {
                return balancetype;
            }

            set
            {
                balancetype = value;
            }
        }

        /// <summary>
        /// 是否医保病人
        /// </summary>
        private bool ismediapat;

        /// <summary>
        /// 是否医保病人
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
        /// 医保刷卡信息
        /// </summary>
        public string MediaInfo
        {
            get
            {
                return lblMediaInfo.Text;
            }

            set
            {
                lblMediaInfo.Text = value;
            }
        }

        /// <summary>
        /// 预算处方明细
        /// </summary>
        private List<Prescription> budgePres;

        /// <summary>
        /// 预算处方明细
        /// </summary>
        public List<Prescription> BudgePres
        {
            get
            {
                return budgePres;
            }

            set
            {
                budgePres = value;
            }
        }

        /// <summary>
        /// 当前病人ID
        /// </summary>
        private int curpatlistid;

        /// <summary>
        /// 当前病人ID
        /// </summary>
        public int CurPatlistId
        {
            get
            {
                return curpatlistid;
            }

            set
            {
                curpatlistid = value;
            }
        }

        /// <summary>
        /// 结算病人类型ID
        /// </summary>
        private int costpattypeid;

        /// <summary>
        /// 结算病人类型ID
        /// </summary>
        public int CostPatTypeId
        {
            get
            {
                return costpattypeid;
            }

            set
            {
                costpattypeid = value;
            }
        }

        /// <summary>
        /// 结算对应的费用头ID
        /// </summary>
        private int[] costfeeids;

        /// <summary>
        /// 结算对应的费用头ID
        /// </summary>
        public int[] FeeHeadIds
        {
            get
            {
                return costfeeids;
            }

            set
            {
                costfeeids = value;
            }
        }

        /// <summary>
        /// 结算总金额
        /// </summary>
        private decimal totalfee;

        /// <summary>
        /// 结算总金额
        /// </summary>
        public decimal TotalFee
        {
            get
            {
                return totalfee;
            }

            set
            {
                totalfee = value;
            }
        }

        /// <summary>
        /// 预算返回的预结算对象
        /// </summary>
        private List<ChargeInfo> budgeInfo;

        /// <summary>
        /// 预算返回的预结算对象
        /// </summary>
        public List<ChargeInfo> BudgeInfo
        {
            get
            {
                return budgeInfo;
            }

            set
            {
                budgeInfo = value;
            }
        }

        /// <summary>
        /// 医保预结算ID
        /// </summary>
        private int miblancebudgetid;

        /// <summary>
        /// 医保预结算ID
        /// </summary>
        public int MIBlanceBudgetID
        {
            get
            {
                return miblancebudgetid;
            }

            set
            {
                miblancebudgetid = value;
            }
        }

        /// <summary>
        /// 显示医保信息
        /// </summary>
        /// <param name="text">医保信息</param>
        public void SetMediaInfo(object text)
        {
            Thread t = new Thread(new ParameterizedThreadStart(SetTextBoxValue));
            t.Start(text);
        }

        /// <summary>
        /// 定义委托
        /// </summary>
        /// <param name="obj">入参</param>
        delegate void D(object obj);

        /// <summary>
        /// 给控件赋值
        /// </summary>
        /// <param name="obj">值</param>
        void SetTextBoxValue(object obj)
        {
            if (lblMediaInfo.InvokeRequired)
            {
                D d = new D(DelegateSetValue);
                lblMediaInfo.Invoke(d, obj);
            }
            else
            {
                this.lblMediaInfo.Text = obj.ToString();
            }
        }

        /// <summary>
        /// 委托赋值
        /// </summary>
        /// <param name="obj">对象</param>
        void DelegateSetValue(object obj)
        {
            this.lblMediaInfo.Text = obj.ToString();
        }

        /// <summary>
        /// 0正常收费，1退费后补收
        /// </summary>
        private bool ysFlag;

        /// <summary>
        /// 0正常收费 1退费后补收
        /// </summary>
        public bool YSFlag
        {
            get
            {
                return ysFlag;
            }

            set
            {
                ysFlag = value;
            }
        }

        /// <summary>
        /// 医保预算报销金额
        /// </summary>
        private decimal medicareMIPay;

        /// <summary>
        /// 医保预算报销金额
        /// </summary>
        public decimal MedicareMIPay
        {
            get
            {
                return medicareMIPay;
            }

            set
            {
                medicareMIPay = value;
            }
        }
        /// <summary>
        /// 医保预算卡支付金额
        /// </summary>
        private decimal medicarePersPay;

        /// <summary>
        /// 医保预算卡支付金额
        /// </summary>
        public decimal MedicarePersPay
        {
            get
            {
                return medicarePersPay;
            }

            set
            {
                medicarePersPay = value;
            }
        }
        #endregion

        /// <summary>
        /// 定认收费是否成功
        /// </summary>
        private bool balanceSuccess;

        /// <summary>
        /// 是否窗后加载后第一次执行
        /// </summary>
        private bool run;       

        #region 窗口初始化
        /// <summary>
        /// 窗体构造函数
        /// </summary>
        public FrmPayMentInfo()
        {
            InitializeComponent();
            YSFlag = false;
            balanceSuccess = false;           
            this.Load += new EventHandler(FrmPayMentInfo_Load);
            btnConfirm.Click += new EventHandler(btnConfirm_Click);
            btnGiveUp.Click += new EventHandler(btnGiveUp_Click);
            
        }

        /// <summary>
        /// 窗体Load事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmPayMentInfo_Load(object sender, EventArgs e)
        {
            balanceSuccess = false;
            run = true;
            int workid = (InvokeController("this") as AbstractController).WorkId;
            int iPayControls = CostPayManager.InitUCPayModeControl(workid, flpPayCtrl, costpattypeid, 0);
            lblMediaInfo.Text = string.Empty;
            if (iPayControls > 0)
            {
                //根据支付方式控件多少，自动设置界面高度
                Height = groupPanel2.Location.Y + 120 + Math.Max(2, iPayControls) * 40 + 15;
                groupPanel2.Height = Math.Max(2, iPayControls) * 40 + 32 + 15;
            }

            SetFeeValueDelegate setfeeValue = new SetFeeValueDelegate(SetLabelText);//实列化委托，各项金额实时显示
            PaymentAutoCalculate autocal = new PaymentAutoCalculate(curpatlistid, costpattypeid, costfeeids, InvokeController);//实列化对象，支付方式金额计算
            CostPayManager.StartExecPay(totalfee, 0, autocal, setfeeValue, false);
            if (IsMediaPat)
            {
                //医保病人不需要打费用清单
                chkFeePrint.Checked = false;
            }
            else
            {
                chkFeePrint.Checked = true;
            }               
        }
        #endregion

        /// <summary>
        /// 实时显示各项金额
        /// </summary>
        /// <param name="costFee">费用对象</param>
        private void SetLabelText(CostFeeStyle costFee)
        {
            txtAmount.Text = costFee.PayTotalFee.ToString();
            lbFavorableSum.Text = costFee.FavorableTotalFee.ToString();
            lbPersonalSum.Text = costFee.SelfTotalFee.ToString();
            lbAccountSum.Text = costFee.AccountTotalFee.ToString();
        }

        /// <summary>
        /// 结算按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (IsMediaPat)
            {
                if (miblancebudgetid <= 0)
                {
                    MessageBox.Show("医保病人需医保预结算完成才能结算");
                    return;
                }
            }

            if (CostPayManager.CostFee.ChangeFee < 0)
            {
                MessageBox.Show("支付金额不足!");
                return;
            }

            if (CostPayManager.CostFee.SelfTotalFee + CostPayManager.CostFee.RoundFee < 0)
            {
                MessageBox.Show("记账支付金额不能超过总金额!");
                return;
            }

            if (CostPayManager.CostFee.AccountTotalFee > CostPayManager.CostFee.PayTotalFee)
            {
                MessageBox.Show("总记账支付金额不能超过总金额");
                return;
            }

            List<OP_CostPayMentInfo> paylist = new List<OP_CostPayMentInfo>();
            foreach (PayModeFee pay in CostPayManager.CostFee.payList)
            {
                if (pay.PayFee != 0)
                {
                    OP_CostPayMentInfo payment = new OP_CostPayMentInfo();
                    payment.PayMentID = pay.PayMethodID;
                    payment.PayMentMoney = pay.PayFee;
                    paylist.Add(payment);
                }
            }

            budgeInfo[0].PayInfoList = paylist;
            budgeInfo[0].PayTotalFee = CostPayManager.CostFee.PayTotalFee;
            budgeInfo[0].FavorableTotalFee = CostPayManager.CostFee.FavorableTotalFee;
            budgeInfo[0].PosFee = CostPayManager.CostFee.PosFee;
            budgeInfo[0].CashFee = CostPayManager.CostFee.CashFee;//现金
            budgeInfo[0].RoundFee = CostPayManager.CostFee.RoundFee;
            budgeInfo[0].ChangeFee = CostPayManager.CostFee.ChangeFee;
            if ((bool)InvokeController("Balance"))
            {
                balanceSuccess = true;
                InvokeController("BalanceComplete");
                this.Close();
                InvokeController("BalancePrint");
                if (chkFeePrint.Checked)
                {
                    InvokeController("PresFeePrintDetail");
                }
            }
        }

        /// <summary>
        /// 取消结算按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnGiveUp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 监听键盘事件
        /// </summary>
        /// <param name="keyData">事件参数</param>
        /// <returns>bool</returns>
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
                            btnConfirm.Focus();
                        else
                            btnConfirm_Click(null, null);
                    }

                    break;
                default:
                    bRet = base.ProcessDialogKey(keyData);
                    break;
            }

            return bRet;
        }

        /// <summary>
        /// 设置焦点
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmSettlementPanel_Shown(object sender, EventArgs e)
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

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmPayMentInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (balancetype == 1)
            {
                if (!balanceSuccess)
                {
                    if (run)
                    {
                        run = false;
                        InvokeController("RefundBalanceFaild");
                    }
                }
            }
        }     
    }
}

/// <summary>
/// 进程金额计算
/// </summary>
public class PaymentAutoCalculate : AsynPaymentCalculate
{
    /// <summary>
    /// 控件器
    /// </summary>
    private ControllerEventHandler invokeController;

    /// <summary>
    /// 是否第一次打开界面
    /// </summary>
    private bool isFirst;

    /// <summary>
    /// 自动计算
    /// </summary>
    /// <param name="patListID">病人ID</param>
    /// <param name="iPatType">病人类型</param>
    /// <param name="feeIDs">费用ID</param>
    /// <param name="invokeController">控制器</param>
    public PaymentAutoCalculate(int patListID, int iPatType, int[] feeIDs, ControllerEventHandler invokeController) : base(patListID, iPatType, feeIDs)
    {
        isFirst = true;
        this.invokeController = invokeController;
    }

    /// <summary>
    /// 优惠计算
    /// </summary>
    /// <param name="ticketNo>票据号</param>
    /// <param name="procStep">进程</param>
    /// <returns>优惠计算金额</returns>
    public override decimal FavorableCalculate(out string ticketNo, out PAY_PROCSTEP procStep)
    {
        ticketNo = "优惠111";
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


    //public void 
    /// <summary>
    /// 医保金额计算
    /// </summary>
    /// <param name="ticketNo">票据号</param>
    /// <param name="procStep">procStep</param>
    /// <returns>返回医保金额</returns>
    public override decimal MedicalinsuranceCalculate(out string ticketNo, out PAY_PROCSTEP procStep)
    {
        decimal mediaFee = (decimal)invokeController("GetMedicareMIPay"); //调用控制器处理
        ticketNo = "医保计算111";
        procStep = PAY_PROCSTEP.ppsAutoFinshed;
        return mediaFee;
    }

    public override decimal MedicalinsuranceCalculatePers(out string ticketNo, out PAY_PROCSTEP procStep)
    {
        decimal mediaFee = (decimal)invokeController("GetMedicarePersPay"); //调用控制器处理
        ticketNo = "医保计算111";
        procStep = PAY_PROCSTEP.ppsAutoFinshed;
        return mediaFee;
    }
}