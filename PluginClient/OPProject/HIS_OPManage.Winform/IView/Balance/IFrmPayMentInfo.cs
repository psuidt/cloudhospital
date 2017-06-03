using System.Collections.Generic;
using System.Data;
using HIS_Entity.OPManage;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    /// 门诊收费结算界面接口类
    /// </summary>
    interface IFrmPayMentInfo
    {
        /// <summary>
        /// 当前病人ID
        /// </summary>
        int CurPatlistId { get; set; }

        /// <summary>
        /// 结算病人类型ID
        /// </summary>
        int CostPatTypeId { get; set; }

        /// <summary>
        /// 结算对应的费用头ID
        /// </summary>
        int[] FeeHeadIds { get; set; }

        /// <summary>
        /// 结算总金额
        /// </summary>
        decimal TotalFee { get; set; }

        /// <summary>
        /// 预算返回的预结算对象
        /// </summary>
        List<ChargeInfo> BudgeInfo { get; set; }

        /// <summary>
        /// 预算处方明细
        /// </summary>
        List<Prescription> BudgePres { get; set; }

        /// <summary>
        /// 是否医保病人
        /// </summary>
        bool IsMediaPat { get; set; }

        /// <summary>
        /// 医保刷卡信息
        /// </summary>
        string MediaInfo { get; set; }

        /// <summary>
        /// 0正常收费 1退费后补收
        /// </summary>
        int BalanceType { get; set; }

        /// <summary>
        /// 收费成功后返回的票据打印信息
        /// </summary>
        ChargeInfo BackChargeInfo { get; set; }

        /// <summary>
        /// 结算票据打印数据
        /// </summary>
        DataTable DtPrintInvoice { get; set; }

        /// <summary>
        /// 结算收费明细打印数据
        /// </summary>
        DataTable DtPrintInvoiceDetail { get; set; }

        /// <summary>
        /// 结算按大项目统计明细数据
        /// </summary>
        DataTable DtInvoiceStatDetail { get; set; }

        /// <summary>
        /// 医保预结算ID
        /// </summary>
        int MIBlanceBudgetID { get; set; }

        /// <summary>
        /// 显示医保信息
        /// </summary>
        /// <param name="text">医保信息</param>
        void SetMediaInfo(object text);

        /// <summary>
        /// 自付金额
        /// </summary>
        decimal SelfFee { get; }
        /// <summary>
        /// 医保预算报销金额
        /// </summary>
        decimal MedicareMIPay { get; set; }
        /// <summary>
        /// 医保预算卡支付金额
        /// </summary>
        decimal MedicarePersPay { get; set; }

        bool YSFlag { get; set; }
    }
}
