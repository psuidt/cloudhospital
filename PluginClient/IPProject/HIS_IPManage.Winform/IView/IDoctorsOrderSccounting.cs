using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.IPManage;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 账单接口
    /// </summary>
    public interface IDoctorsOrderSccounting : IBaseView
    {
        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="deptDt">科室列表</param>
        void Bind_DeptList(DataTable deptDt);

        /// <summary>
        /// 绑定在床病人列表
        /// </summary>
        /// <param name="patListDt">在床病人列表</param>
        void Bind_PatientList(DataTable patListDt);
        
        /// <summary>
        /// 绑定弹出网格费用列表
        /// </summary>
        /// <param name="feeDt">费用列表</param>
        void Bind_SimpleFeeItemData(DataTable feeDt);

        /// <summary>
        /// 显示病人的累计交费金额和累计记账金额
        /// </summary>
        /// <param name="sumpay">总金额</param>
        /// <param name="longFeeOrder">长期账单金额</param>
        /// <param name="tempFeeOrder">临时账单金额</param>
        /// <param name="bedFee">床位费</param>
        void Bind_PatSumPay(decimal sumpay, decimal longFeeOrder, decimal tempFeeOrder, decimal bedFee);

        /// <summary>
        /// 绑定病人长期账单列表
        /// </summary>
        /// <param name="longOrderDt">长期账单列表</param>
        void Bind_LongOrderData(DataTable longOrderDt);

        /// <summary>
        /// 绑定账单模板列表
        /// </summary>
        /// <param name="feeTempList">账单模板列表</param>
        void Bind_FeeTempList(List<IP_FeeItemTemplateHead> feeTempList);

        /// <summary>
        /// 根据模板明细绑定账单列表
        /// </summary>
        /// <param name="feeDetailsDt">长期账单列表</param>
        void Bind_TempDetailLongOrderData(DataTable feeDetailsDt);

        /// <summary>
        /// 绑定已记账费用列表
        /// </summary>
        /// <param name="costListDt">已记账费用列表</param>
        void Bind_CostList(DataTable costListDt);

        /// <summary>
        /// 长期账单列表
        /// </summary>
        DataTable LongOrderList { get; set; }

        /// <summary>
        /// 设置网格数据显示颜色
        /// </summary>
        void SetFeeListGridColor();

        /// <summary>
        /// 科室ID
        /// </summary>
        int DeptId { get; }

        /// <summary>
        /// 检索条件
        /// </summary>
        string Patam { get; }

        /// <summary>
        /// 设置网格颜色
        /// </summary>
        void SetGridColor();

        /// <summary>
        /// 病人登记ID
        /// </summary>
        int PatListID { get; set; }

        /// <summary>
        /// 账单类型
        /// </summary>
        int OrderType { get; }

        /// <summary>
        /// 项目ID
        /// </summary>
        int ItemID { get; }

        /// <summary>
        /// 处方开始时间
        /// </summary>
        DateTime StartTime { get; }

        /// <summary>
        /// 处方结束时间
        /// </summary>
        DateTime EndTime { get; }
    }
}