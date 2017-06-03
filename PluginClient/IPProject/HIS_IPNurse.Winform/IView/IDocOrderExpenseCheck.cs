using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPNurse.Winform.IView
{
    /// <summary>
    /// 医嘱费用核对接口
    /// </summary>
    public interface IDocOrderExpenseCheck : IBaseView
    {
        /// <summary>
        /// 科室ID
        /// </summary>
        int DeptId { get; }

        /// <summary>
        /// 病人状态
        /// </summary>
        int PatStatus { get; }

        /// <summary>
        /// 出院开始时间
        /// </summary>
        //DateTime StartTime { get; }

        /// <summary>
        /// 出院结束时间
        /// </summary>
        //DateTime EndTime { get; }

        /// <summary>
        /// 病人登记ID
        /// </summary>
        int PatListID { get; set; }

        /// <summary>
        /// 医嘱ID
        /// </summary>
        int OrderID { get; set; }

        /// <summary>
        /// 医嘱分组ID
        /// </summary>
        int GroupID { get; set; }

        /// <summary>
        /// 长期账单列表
        /// </summary>
        DataTable LongOrderList { get; set; }

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="deptDt">科室列表</param>
        /// <param name="deptId">默认科室ID</param>
        void bind_DeptList(DataTable deptDt, int deptId);

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="patListDt">病人列表</param>
        void bind_PatList(DataTable patListDt);

        /// <summary>
        /// 医嘱类型（长期/临时）
        /// </summary>
        int OrderType { get; }

        /// <summary>
        /// 绑定病人费用信息
        /// </summary>
        /// <param name="sumDeposit">累计预交金总额</param>
        /// <param name="patSumFee">累计记账金额</param>
        /// <param name="patBedFee">床位费总额</param>
        /// <param name="daySumFee">今日费用</param>
        void bind_PatFeeInfo(decimal sumDeposit, decimal patSumFee, decimal patBedFee, decimal daySumFee);

        /// <summary>
        /// 绑定病人医嘱列表
        /// </summary>
        /// <param name="orderListDt">医嘱列表</param>
        void bind_PatOrderList(DataTable orderListDt);

        /// <summary>
        /// 绑定长期医嘱关联费用列表
        /// </summary>
        /// <param name="orderFeeList">长期医嘱明细费用数据</param>
        /// <param name="orderSumFeeList">长期医嘱汇总费用数据</param>
        void bind_OrderFeeList(DataTable orderFeeList, DataTable orderSumFeeList);

        /// <summary>
        /// 绑定病人账单费用列表
        /// </summary>
        /// <param name="feeDt">账单费用列表</param>
        /// <param name="sumFeeDt">汇总费用</param>
        void bind_CostList(DataTable feeDt, DataTable sumFeeDt);

        /// <summary>
        /// 绑定病人长期账单列表
        /// </summary>
        /// <param name="longOrderDt">长期账单列表</param>
        void bind_LongOrderData(DataTable longOrderDt);

        /// <summary>
        /// 绑定弹出网格费用列表
        /// </summary>
        /// <param name="feeDt">费用列表</param>
        void bind_SimpleFeeItemData(DataTable feeDt);
    }
}
