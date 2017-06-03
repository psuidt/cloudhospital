using System;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 药品统领接口
    /// </summary>
    public interface ICommandManagement : IBaseView
    {
        /// <summary>
        /// 绑定执行科室列表
        /// </summary>
        /// <param name="execDeptDt">执行科室列表</param>
        void Bind_ExecDeptList(DataTable execDeptDt);

        /// <summary>
        /// 绑定入院科室列表
        /// </summary>
        /// <param name="deptList">入院科室列表</param>
        void Bind_DeptList(DataTable deptList);

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="patList">病人列表</param>
        void Bind_PatList(DataTable patList);

        /// <summary>
        /// 绑定药品统领列表
        /// </summary>
        /// <param name="commandList">药品统领列表</param>
        void Bind_CommandList(DataTable commandList);

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="patDt">病人列表</param>
        void Bind_HasBeenSentDrugbillPatList(DataTable patDt);

        /// <summary>
        /// 绑定统领单列表
        /// </summary>
        /// <param name="orderDt">统领单列表</param>
        void Bind_OrderList(DataTable orderDt);

        /// <summary>
        /// 查询统领单汇总数据
        /// </summary>
        /// <param name="summaryDt">汇总数据</param>
        void Bind_DurgBillSummaryList(DataTable summaryDt);

        /// <summary>
        /// 查询统领单明细数据
        /// </summary>
        /// <param name="detailDt">明细数据</param>
        void Bind_DurgBillDetailList(DataTable detailDt);

        /// <summary>
        /// 病人入院科室ID
        /// </summary>
        int PatDeptID { get; }

        /// <summary>
        /// 药品执行科室ID
        /// </summary>
        int ExecDeptID { get; }

        /// <summary>
        /// 发药或退药统领
        /// </summary>
        bool CommandStatus { get; }

        /// <summary>
        /// 全部药品
        /// </summary>
        bool Whole { get; }

        /// <summary>
        /// 口服药
        /// </summary>
        bool IsOralMedicine { get; }

        /// <summary>
        /// 针剂
        /// </summary>
        bool IsInjection { get; }

        /// <summary>
        /// 大输液
        /// </summary>
        bool IsLargeInfusion { get; }

        /// <summary>
        /// 中草药
        /// </summary>
        bool IsChineseHerbalMedicine { get; }

        /// <summary>
        /// 发药或退药统领
        /// </summary>
        bool IsMedicineCommand { get; set; }

        /// <summary>
        /// 统领单头ID
        /// </summary>
        int BillHeadID { get; }

        /// <summary>
        /// 单据状态
        /// </summary>
        int DispDrugFlag { get; }

        #region "统领单列表查询条件"

        /// <summary>
        /// 入院科室ID
        /// </summary>
        int OrderDeptId { get; }

        /// <summary>
        /// 单据状态
        /// </summary>
        bool OrderStatus { get; }

        /// <summary>
        /// 病人登记ID
        /// </summary>
        int PatListId { get; }

        /// <summary>
        /// 统领单据发送开始时间
        /// </summary>
        DateTime StartDate { get; }

        /// <summary>
        /// 统领单据发送结束时间
        /// </summary>
        DateTime EndDate { get; }
        #endregion
    }
}
