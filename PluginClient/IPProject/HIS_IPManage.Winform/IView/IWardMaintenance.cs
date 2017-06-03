using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.IPManage;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 床位维护接口
    /// </summary>
    public interface IWardMaintenance : IBaseView
    {
        /// <summary>
        /// 绑定病区列表
        /// </summary>
        /// <param name="wardDt">病区列表</param>
        void Bind_WardDeptList(DataTable wardDt);

        /// <summary>
        /// 病区ID
        /// </summary>
        int WardID { get; }

        /// <summary>
        /// 绑定床位列表
        /// </summary>
        /// <param name="bedDt">床位列表</param>
        void Bind_BedList(DataTable bedDt);

        /// <summary>
        /// 绑定床位费用列表
        /// </summary>
        /// <param name="bedFreeDt">床位费用列表</param>
        void Bind_BedFreeList(DataTable bedFreeDt);

        /// <summary>
        /// 病床信息
        /// </summary>
        IP_BedInfo IPBedInfo { get;}

        /// <summary>
        /// 后台数据没加载完时不允许进行床位维护操作
        /// </summary>
        void SetControlEnabled();
    }
}
