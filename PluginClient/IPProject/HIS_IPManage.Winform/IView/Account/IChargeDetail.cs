using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 缴款明细数据显示接口
    /// </summary>
    public interface IChargeDetail : IBaseView
    {
        /// <summary>
        /// 绑定缴款明细数据
        /// </summary>
        /// <param name="dtDetail">明细数据</param>
        void BindDetailSource(DataTable dtDetail);
    }
}
