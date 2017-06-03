using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_MemberManage.Winform.IView
{
    /// <summary>
    /// 添加积分接口
    /// </summary>
    public interface IFrmAddScore : IBaseView
    {
        /// <summary>
        /// 帐户信息行数据
        /// </summary>
        DataRow AccountDr { get; set; }

        /// <summary>
        /// 会员名称
        /// </summary>
        string MemberName { get; set; }

        /// <summary>
        /// 优惠卷编码
        /// </summary>
        string TiecketCode { get; set; }
    }
}
