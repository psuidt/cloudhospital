using System.Data;

namespace HIS_IPDoctor.Winform.IView
{
    /// <summary>
    /// 血糖数据查询界面接口
    /// </summary>
    interface IFrmBloodGlucose
    {
        /// <summary>
        /// 病人信息绑定
        /// </summary>
        /// <param name="dtPatInfo">病人信息数据</param>
        void BindPatInfo(DataTable dtPatInfo);

        /// <summary>
        /// 选择的科室ID
        /// </summary>
        int DeptId { get; set; }

        /// <summary>
        /// 当前病人ID
        /// </summary>
        int CurPatListId { get; set; }

        /// <summary>
        /// 科室数据绑定
        /// </summary>
        /// <param name="dtDept">科室绑定</param>
        void BindDept(DataTable dtDept);

        /// <summary>
        /// 绑定血糖数据
        /// </summary>
        /// <param name="dtBloodGluRecord">血糖数据</param>
        void BindBloodGluRecord(DataTable dtBloodGluRecord);
    }
}
