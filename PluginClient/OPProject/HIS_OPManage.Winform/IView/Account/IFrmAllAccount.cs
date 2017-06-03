using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    /// 缴款查询界面接口类
    /// </summary>
    public interface IFrmAllAccount:IBaseView
    {
        /// <summary>
        /// 加载收费员信息
        /// </summary>
        /// <param name="dtCashier">收费员数据</param>
        void loadCashier(DataTable dtCashier);

        /// <summary>
        /// 绑定查询数据
        /// </summary>
        /// <param name="dtAllAccount">已缴款数据</param>
        /// <param name="dtAllNotAccount">未缴款数据</param>
        void BindQueryData(DataTable dtAllAccount, DataTable dtAllNotAccount);
    }
}
