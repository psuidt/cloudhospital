using EFWCoreLib.WinformFrame.Controller;

namespace HIS_MemberManage.Winform.IView
{
    /// <summary>
    /// 礼品接口
    /// </summary>
    public interface IFrmGetGift : IBaseView
    {
        /// <summary>
        /// Gets or sets 礼品id
        /// </summary>
        /// <value>礼品id</value>
        int GiftID { get; set; }

        /// <summary>
        /// Gets or sets 帐户id
        /// </summary>
        /// <value>帐户id</value>
        int AccountID { get; set; }

        /// <summary>
        /// Gets or sets 帐户代码
        /// </summary>
        /// <value>帐户代码</value>
        string AccountCode { get; set; }
    }
}
