using EFWCoreLib.WinformFrame.Controller;

namespace HIS_MemberManage.Winform.IView
{
    /// <summary>
    /// 积分兑换明细接口
    /// </summary>
    public interface IFrmConvertPointsInfo : IBaseView
    {
        /// <summary>
        /// Gets or sets 兑换ID
        /// </summary>
        /// <value>兑换ID</value>
        int ConvertID { get; set; }

        /// <summary>
        /// Gets or sets 机构id
        /// </summary>
        /// <value>机构id</value>
        int WorkID { get; set; }

        /// <summary>
        /// Gets or sets 卡类型id
        /// </summary>
        /// <value>卡类型id</value>
        int CardTypeID { get; set; }

        /// <summary>
        /// Gets or sets 现金
        /// </summary>
        /// <value>现金</value>
        int Cash { get; set; }

        /// <summary>
        /// Gets or sets 积分
        /// </summary>
        /// <value>积分</value>
        int Score { get; set; }

        /// <summary>
        /// Gets or sets 行索引
        /// </summary>
        /// <value>行索引</value>
        int RowIndex { get; set; }

        /// <summary>
        /// Gets or sets 卡类型名称
        /// </summary>
        /// <value>卡类型名称</value>
        string CardTypeName { get; set; }
    }
}
