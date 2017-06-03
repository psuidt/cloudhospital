using System;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.IPManage;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 住院收费查询接口
    /// </summary>
    public interface IDischargeRecall : IBaseView
    {
        /// <summary>
        /// 收费开始时间
        /// </summary>
        DateTime CostBeginDate { get; }

        /// <summary>
        /// 收费结束时间
        /// </summary>
        DateTime CostEndDate { get; }

        /// <summary>
        /// 检索条件
        /// </summary>
        string SqlectParam { get; }

        /// <summary>
        /// 收费员ID
        /// </summary>
        int EmpId { get; }

        /// <summary>
        /// 结算状态
        /// </summary>
        int Status { get; }

        /// <summary>
        /// 是否缴款
        /// </summary>
        int IsAccount { get; }

        /// <summary>
        /// 结算类型
        /// </summary>
        string CostType { get; }

        /// <summary>
        /// 选中结算记录的病人登记ID
        /// </summary>
        int PatListID { get; }

        /// <summary>
        /// 选中结算记录的结算ID
        /// </summary>
        int CostHeadID { get; }

        /// <summary>
        /// 取选中结算记录的消结算类型
        /// </summary>
        int CancelCostType { get; }

        /// <summary>
        /// 发票补打记录对象
        /// </summary>
        IP_PrintInvoiceInfo PrintInvoiceInfo { get; set; }

        /// <summary>
        /// 绑定收费员列表
        /// </summary>
        /// <param name="cashierDt">收费员列表</param>
        void Bind_CashierList(DataTable cashierDt);

        /// <summary>
        /// 绑定已结算费用列表
        /// </summary>
        /// <param name="feeList">已结算费用列表</param>
        void Bind_CostFeeList(DataTable feeList);

        /// <summary>
        /// 绑定住院押金列表
        /// </summary>
        /// <param name="depositList">住院押金列表</param>
        void Bind_DepositList(DataTable depositList);
    }
}