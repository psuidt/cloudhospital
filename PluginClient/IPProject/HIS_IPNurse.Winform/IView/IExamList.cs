using System.Data;

namespace HIS_IPNurse.Winform.IView
{
    /// <summary>
    /// 新开检查检验接口
    /// </summary>
    public interface IExamList
    {
        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="dtDept">科室列表</param>
        /// <param name="iDeptId">默认科室ID</param>
        void Bind_DeptList(DataTable dtDept, int iDeptId);

        /// <summary>
        /// 加载执行单数据
        /// </summary>
        /// <param name="dtPatList">病人列表</param>
        /// <param name="dtExcuteList">执行单列表</param>
        void LoadExamList(DataTable dtPatList, DataTable dtExcuteList);

        /// <summary>
        /// 获取申请单数据
        /// </summary>
        void RefreshExamList();
    }
}
