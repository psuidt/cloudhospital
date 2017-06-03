using System.Collections.Generic;
using EfwControls.HISControl.UCPayMode;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.IPManage;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 费用结算接口
    /// </summary>
    public interface IPayMentInfo : IBaseView
    {
        /// <summary>
        /// 结算病人类型ID
        /// </summary>
        int CostPatTypeID { get; set; }

        /// <summary>
        /// 结算病人登记ID
        /// </summary>
        int CurPatListID { get; set; }

        /// <summary>
        /// 住院病人最新预交金总额
        /// </summary>
        decimal DepositFee { get; set; }

        /// <summary>
        /// 结算总金额
        /// </summary>
        decimal TotalFee { get; set; }

        /// <summary>
        /// 结算信息
        /// </summary>
        CostFeeStyle CostFee { get; set; }

        /// <summary>
        /// 住院结算主表
        /// </summary>
        IP_CostHead CostHead { get; set; }

        /// <summary>
        /// 支付方式记录
        /// </summary>
        List<IP_CostPayment> CostPayList { get; set; }

        /// <summary>
        /// 结算类型
        /// </summary>
        int CostType { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        decimal PromFee { get; set; }

        /// <summary>
        /// 关闭结算界面
        /// </summary>
        void CloseForm();
    }
}