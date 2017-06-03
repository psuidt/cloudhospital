using System;
using System.Data;
using EfwControls.HISControl.BedCard.Controls;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 床位一览管理接口
    /// </summary>
    public interface IBedManagement : IBaseView
    {
        /// <summary>
        /// 绑定病区列表数据
        /// </summary>
        /// <param name="wardDt">病区列表</param>
        void Bind_WardDept(DataTable wardDt);

        /// <summary>
        /// 绑定床位列表
        /// </summary>
        /// <param name="bedListDt">床位列表</param>
        void Bind_BedList(DataTable bedListDt);

        /// <summary>
        /// 床位对象
        /// </summary>
        BedInfo Bed { get; }

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="deptDt">科室列表</param>
        /// <param name="deptId">默认科室ID</param>
        void Bind_DeptList(DataTable deptDt, int deptId);

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="patListDt">病人列表</param>
        void Bind_PatList(DataTable patListDt);

        /// <summary>
        /// 病人状态
        /// </summary>
        int PatStatus { get; }

        /// <summary>
        /// 科室ID
        /// </summary>
        int DeptId { get; }

        /// <summary>
        /// 开始时间
        /// </summary>
        DateTime StartTime { get; }

        /// <summary>
        /// 结束时间
        /// </summary>
        DateTime EndTime { get; }

        /// <summary>
        /// 是否只查询能催款病人
        /// </summary>
        bool IsReminder { get; }
    }
}
