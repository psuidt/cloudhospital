using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_MemberManage.Winform.IView
{
    /// <summary>
    /// 换卡接口
    /// </summary>
    public interface IFrmChangeCard: IBaseView
    {
        /// <summary>
        /// Gets or sets 帐户id
        /// </summary>
        /// <value>帐户id</value>
        int AccountID { get; set; }

        /// <summary>
        /// Gets or sets 会员名称
        /// </summary>
        ///  <value>会员名称</value>
        string MemberName { get; set; }

        /// <summary>
        /// Gets or sets 帐户类型名称
        /// </summary>
        /// <value>帐户类型名称</value>
        string AccountTypeName { get; set; }

        /// <summary>
        /// Gets or sets 原卡号
        /// </summary>
        /// <value>原卡号</value>
        string OldCardNO { get; set; }

        /// <summary>
        /// Gets or sets 帐户类型id
        /// </summary>
        /// <value>帐户类型id</value>
        int AccountTypeID { get; set; }

        /// <summary>
        /// 绑定列表
        /// </summary>
        void BindList();

        /// <summary>
        /// 设置原卡
        /// </summary>
        /// <param name="code">卡号</param>
        void SetOldCard(string code);

        /// <summary>
        /// Gets or sets 会员id
        /// </summary>
        /// <value>会员id</value>
        int MemberID { get; set; }
    }
}
