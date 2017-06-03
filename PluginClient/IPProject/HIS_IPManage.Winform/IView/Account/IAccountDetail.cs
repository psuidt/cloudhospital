using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 缴款明细接口
    /// </summary>
    public interface IAccountDetail : IBaseView
    {
        /// <summary>
        /// 绑定缴款数据
        /// </summary>
        /// <param name="iStaffId">操作员ID</param>
        /// <param name="iAccountId">缴款ID</param>
        /// <param name="iAccountType">缴款类型</param>
        void BindAccountInfo(int iStaffId, int iAccountId, int iAccountType);

        /// <summary>
        /// 获取每条缴款的发票等信息
        /// </summary>
        /// <param name="dtFPSum">发票总数</param>
        /// <param name="dtFPClass">发票分类</param>
        /// <param name="dtAccountClass">支付方式信息</param>
        void ShowPayMentItem(DataTable dtFPSum, DataTable dtFPClass, DataTable dtAccountClass);

        /// <summary>
        /// 显示预交金信息
        /// </summary>
        /// <param name="dtDepositList">预交金列表</param>
        void ShowDepositItem(DataTable dtDepositList);
    }
}
