using EFWCoreLib.WinformFrame.Controller;

namespace HIS_MemberManage.Winform.IView
{
    /// <summary>
    /// 礼品接口
    /// </summary>
    public interface IFrmConversionRull: IBaseView
    {
        /// <summary>
        /// Gets or sets 礼品id
        /// </summary>
        ///  <value>礼品id</value>
        int GiftID { get; set; }
    }
}
