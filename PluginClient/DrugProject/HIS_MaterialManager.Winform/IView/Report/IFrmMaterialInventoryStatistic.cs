using System.Data;

namespace HIS_MaterialManage.Winform.IView.Report
{
    /// <summary>
    /// 进销存统计接口
    /// </summary>
    interface IFrmMaterialInventoryStatistic
    {
        /// <summary>
        /// 绑定库房下拉框
        /// </summary>
        /// <param name="dtDept">科室数据</param>
        void BindDeptRoom(DataTable dtDept);

        /// <summary>
        /// 绑定会计年
        /// </summary>
        /// <param name="dtYear">年</param>
        void BindYear(DataTable dtYear);

        /// <summary>
        /// 绑定会计月
        /// </summary>
        /// <param name="dtMonth">月</param>
        void BindMonth(DataTable dtMonth);

        /// <summary>
        /// 绑定物资类型TextBox控件
        /// </summary>
        /// <param name="typeId">物资类型Id</param>
        /// <param name="typeName">物资类型名称</param>
        void BindMaterialTypeTextBox(int typeId, string typeName);
    }
}
