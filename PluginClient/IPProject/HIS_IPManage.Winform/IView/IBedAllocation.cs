using System.Data;
using EfwControls.HISControl.BedCard.Controls;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 床位分配接口
    /// </summary>
    public interface IBedAllocation : IBaseView
    {
        /// <summary>
        /// 床位对象
        /// </summary>
        BedInfo Bed { get; set; }

        /// <summary>
        /// 绑定未分配床位病人列表
        /// </summary>
        /// <param name="bedListDt">未分配床位病人列表</param>
        void Bind_NotHospitalPatList(DataTable bedListDt);

        /// <summary>
        /// 绑定医生列表
        /// </summary>
        /// <param name="doctorDt">医生列表</param>
        /// <param name="doctorId">床位默认医生ID</param>
        void Bind_txtCurrDoctor(DataTable doctorDt, int doctorId);

        /// <summary>
        /// 绑定护士列表
        /// </summary>
        /// <param name="nurseDt">护士列表</param>
        /// <param name="nurseId">床位默认护士ID</param>
        void Bind_txtCurrNurse(DataTable nurseDt, int nurseId);

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        void CloseForm();

        /// <summary>
        /// 病区ID
        /// </summary>
        int WardID { get; set; }
    }
}
