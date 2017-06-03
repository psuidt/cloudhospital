using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.MemberManage;

namespace HIS_MemberManage.Winform.IView
{
    /// <summary>
    /// 卡类型信息接口
    /// </summary>
    public interface IFrmCardTypeInfo:IBaseView
    {
        /// <summary>
        /// Gets or sets 卡片前缀
        /// </summary>
        /// <value>卡片前缀</value>
        string CardPrefix { get; set; }

        /// <summary>
        /// Gets or sets 卡类型描述
        /// </summary>
        /// <value>卡类型描述</value>
        string CardTypeDesc { get; set; }

        /// <summary>
        /// Gets or sets 卡接口描述
        /// </summary>
        /// <value>卡接口描述</value>
        string CardInterfaceDesc { get; set; }

        /// <summary>
        ///  Gets or sets 卡类型id
        /// </summary>
        /// <value>卡类型id</value>
        int CardTypeID { get; set; }

        /// <summary>
        /// Gets or sets 卡片类型
        /// </summary>
        /// <value>卡片类型</value>
        int CardType { get; set; }

        /// <summary>
        /// Gets or sets 标识
        /// </summary>
        /// <value>标识</value>
        int Flag { get; set; }

        /// <summary>
        /// Gets or sets 卡类型索引
        /// </summary>
        /// <value>卡类型索引</value>
        int CardTypeIndex { get; set; }

        /// <summary>
        /// Gets or sets 会员信息实体类
        /// </summary>
        /// <value>会员信息实体类</value>
        ME_CardTypeList MECardTypeList { get; set; }
    }
}
