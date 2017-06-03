using System.Data;

namespace HIS_IPNurse.Winform.IView
{
    /// <summary>
    /// 执行单打印接口
    /// </summary>
    public interface IExecBillRecord
    {
        /// <summary>
        /// 绑定科室
        /// </summary>
        /// <param name="dtDept">科室列表</param>
        /// <param name="iDeptId">默认科室ID</param>
        void Bind_DeptList(DataTable dtDept, int iDeptId);

        /// <summary>
        /// 绑定执行单类型
        /// </summary>
        /// <param name="dtReport">执行单列表</param>
        void Bind_ReportTypeList( DataTable dtReport);

        /// <summary>
        /// 绑定执行单数据
        /// </summary>
        /// <param name="dtPatList">病人列表</param>
        /// <param name="dtReport">执行单数据</param>
        void Bind_ExcuteList(DataTable dtPatList, DataTable dtReport);

        /// <summary>
        /// 刷新执行单数据
        /// </summary>
        void RefreshExcuteList();
    }
}
