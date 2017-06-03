using System.Collections.Generic;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.OPManage;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    /// 退费界面接口
    /// </summary>
    interface IFrmRefundInfo : IBaseView
    {
        /// <summary>
        /// 退票据号
        /// </summary>
        string RefundInvoiceNO { get; set; }

        /// <summary>
        /// 结算总金额
        /// </summary>
        string CostTotalFee { get; set; }

        /// <summary>
        /// 支付方式信息
        /// </summary>
        string StrPayInfo { get; set; }

        /// <summary>
        /// 退现金
        /// </summary>
        string StrRefundCash { get; set; }

        /// <summary>
        ///  是否是医保病人
        /// </summary>
        bool IsMediaPat { get; set; }

        /// <summary>
        /// 设置显示医保刷卡信息
        /// </summary>
        string StrMediaReadInfo { get; set; }

        /// <summary>
        /// 退费结算ID
        /// </summary>
        int RefundCostHeadID { get; set; }

        /// <summary>
        /// 退费处方
        /// </summary>
        List<Prescription> BalancePresc { get; set; }

        /// <summary>
        /// 是否全退
        /// </summary>
        bool IsRefundAll { get; set; }
    }
}
