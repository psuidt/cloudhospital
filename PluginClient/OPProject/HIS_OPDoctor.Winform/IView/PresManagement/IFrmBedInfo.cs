using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_OPDoctor.Winform.IView
{
    /// <summary>
    /// 获取床位主界面接口
    /// </summary>
    public interface IFrmBedInfo : IBaseView
    {
        /// <summary>
        /// 绑定床位信息
        /// </summary>
        /// <param name="dtBedInfo">床位数据</param>
        void BindBedInfo(DataTable dtBedInfo);
    }
}
