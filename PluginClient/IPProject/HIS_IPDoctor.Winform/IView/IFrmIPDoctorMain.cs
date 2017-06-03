using System.Data;

namespace HIS_IPDoctor.Winform.IView
{
    /// <summary>
    /// 住院医生站主界面接口
    /// </summary>
    interface IFrmIPDoctorMain
    {
        /// <summary>
        /// 绑定我的病人床位信息
        /// </summary>
        /// <param name="dtMyPatient">我的病人</param>
        void BindMyBedPatient(DataTable dtMyPatient);

        /// <summary>
        /// 绑定科室病人信息
        /// </summary>
        /// <param name="dtDeptPatient">科室病人</param>
        void BindDeptBedPatient(DataTable dtDeptPatient);

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="dtDept">科室列表</param>
        void BindDept(DataTable dtDept);

        /// <summary>
        /// 选择的科室ID
        /// </summary>
        int DeptId { get; set; }

        /// <summary>
        /// 选择的病人ID
        /// </summary>
        int SelectPatListID { get; }

        /// <summary>
        /// 是否加载完成
        /// </summary>
        bool LoadCompleted { get; set; }

        /// <summary>
        /// 血糖Url
        /// </summary>
        string BloodUrl { get; set; }
    }
}
