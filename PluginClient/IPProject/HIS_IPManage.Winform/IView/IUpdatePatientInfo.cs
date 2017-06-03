using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 修改病人入院医生护士接口
    /// </summary>
    public interface IUpdatePatientInfo: IBaseView
    {
        /// <summary>
        /// 绑定医生列表
        /// </summary>
        /// <param name="doctorDt">医生列表</param>
        /// <param name="doctorId">默认医生ID(当前医生ID)</param>
        void Bind_CurrDoctor(DataTable doctorDt, int doctorId);

        /// <summary>
        /// 绑定护士列表
        /// </summary>
        /// <param name="nurseDt">护士列表</param>
        /// <param name="nurseId">默认护士ID(当前护士ID)</param>
        /// <param name="patName">病人姓名</param>
        void Bind_CurrNurse(DataTable nurseDt, int nurseId, string patName);

        /// <summary>
        /// 医生ID
        /// </summary>
        int DoctorID { get; }

        /// <summary>
        /// 护士ID
        /// </summary>
        int NurseId { get; }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        void ColseForm();
    }
}