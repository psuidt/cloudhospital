using System.Data;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    /// 病人信息查询界面接口类
    /// </summary>
    interface IFrmQueryMember
    {
        /// <summary>
        /// 绑定查询到的会员信息列表
        /// </summary>
        /// <param name="dtPatInfo">病人信息表</param>
        void LoadPatInfo(DataTable dtPatInfo);

        /// <summary>
        /// 获取当前选定的行
        /// </summary>
        int GetCurRowIndex { get; }

        /// <summary>
        /// 获取界面会员信息数据源
        /// </summary>
        /// <returns>DataTable</returns>
        DataTable GetPatInfoDatable();        
    }
}
