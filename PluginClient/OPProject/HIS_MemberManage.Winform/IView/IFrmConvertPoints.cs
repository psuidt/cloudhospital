using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_MemberManage.Winform.IView
{
    /// <summary>
    /// 兑换积分接口
    /// </summary>
    public interface IFrmConvertPoints : IBaseView
    {
        /// <summary>
        /// Gets or sets 帐户类型数据
        /// </summary>
        /// <value>帐户类型数据</value>
        DataTable DtCardTypeInfo { get; set; }

        /// <summary>
        /// 绑定卡类型
        /// </summary>
        /// <param name="dt">卡类型</param>
        /// <param name="index">索引</param>
        void BindCardTypeInfo(DataTable dt,int index);

        /// <summary>
        /// 绑定帐户网格
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="rowIndex">行索引</param>
        void BinddgAccount(int cardTypeID, int rowIndex);

        /// <summary>
        /// Gets or sets 卡类型id
        /// </summary>
        /// <value>卡类型id</value>
        int CardTypeID { get; set; }
    }
}
