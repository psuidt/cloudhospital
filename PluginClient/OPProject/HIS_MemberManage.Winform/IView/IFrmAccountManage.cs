using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_MemberManage.Winform.IView
{
    /// <summary>
    /// 帐户管理接口
    /// </summary>
    public interface IFrmAccountManage:IBaseView
    {
        /// <summary>
        /// Gets or sets 会员名称
        /// </summary>
        /// <value>会员名称</value>
        string MemberName { get; set; }

        /// <summary>
        /// Gets or sets 机构id
        /// </summary>
        /// <value>机构id</value>
        string WorkID { get; set; }

        /// <summary>
        /// Gets or sets 手机
        /// </summary>
        /// <value>手机</value>
        string Mobile { get; set; }

        /// <summary>
        /// Gets or sets 开始日期
        /// </summary>
        /// <value>开始日期</value>
        string StDate { get; set; }

        /// <summary>
        /// Gets or sets 结束日期
        /// </summary>
        /// <value>结束日期</value>
        string EndDate { get; set; }

        /// <summary>
        /// Gets or sets 选中的会员索引
        /// </summary>
        /// <value>选中的会员索引</value>
        int SelectMemberIndex { get; set; }

        /// <summary>
        /// Gets or sets 选中的帐户索引
        /// </summary>
        /// <value>选中的帐户索引</value>
        int SelectAccountIndex { get; set; }

        /// <summary>
        /// Gets or sets 会员表
        /// </summary>
        /// <value>会员表</value>
        DataTable MemberTable { get; set; }

        /// <summary>
        /// 绑定机构
        /// </summary>
        /// <param name="dtWorkInfo">机构信息</param>
        void BindWorkInfo(DataTable dtWorkInfo);

        /// <summary>
        ///绑定会员信息
        /// </summary>
        /// <param name="dt">会员数据</param>
        /// <param name="total">总条数</param>
        void BindMemberInfo(DataTable dt,int total);

        /// <summary>
        /// Gets or sets用于绑定帐户信息网络
        /// </summary>
        /// <value>用于绑定帐户信息网络</value>
        DataTable AccountTable
        {
            get;
            set;
        }
    }
}
