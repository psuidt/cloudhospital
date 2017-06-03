using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 缴款查询接口
    /// </summary>
    public interface IFrmAllAccount : IBaseView
    {
        /// <summary>
        /// 绑定缴款人列表
        /// </summary>
        /// <param name="dtCashier">缴款人列表</param>
        void LoadCashier(DataTable dtCashier);

        /// <summary>
        /// 绑定数据，动态增加列
        /// </summary>
        /// <param name="dtAllAccount">所有已缴款数据</param>
        /// <param name="dtAllNotAccount">所有未缴款数据</param>
        void BindQueryData(DataTable dtAllAccount, DataTable dtAllNotAccount);
    }
}
