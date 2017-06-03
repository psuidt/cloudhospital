using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_MemberManage.Winform.ViewForm
{
    /// <summary>
    /// 会员接口
    /// </summary>
    public interface IFrmMember : IBaseView
    {
        /// <summary>
        /// Gets or sets 查询条件
        /// </summary>
        /// <value>查询条件</value>
        string SqlCondition { get; set; }

        /// <summary>
        /// Gets or sets 基础数据
        /// </summary>
        /// <value>基础数据</value>
        DataSet BaseData { get; set; }

        /// <summary>
        /// Gets or sets 会员名称
        /// </summary>
        /// <value>会员名称</value>
        string MemberName { get; set; }

        /// <summary>
        /// Gets or sets 机构id
        /// </summary>
        /// <value>机构id</value>
        int WorkID { get; set; }

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
        ///Gets or sets  结束日期
        /// </summary>
        /// <value>结束日期</value>
        string EndDate { get; set; }

        /// <summary>
        /// Gets or sets 总数
        /// </summary>
        /// <value>总数</value>
        int Total { get; set; }

        /// <summary>
        /// Gets or sets 会员信息网络选中行索引
        /// </summary>
        /// <value>会员信息网络选中行索引</value>
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
        /// 绑定会员信息
        /// </summary>
        /// <param name="dt">会员信息</param>
        /// <param name="pageNO">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="index">索引</param>
        void BindMemberInfo(DataTable dt, int pageNO, int pageSize, int index);

        /// <summary>
        /// 绑定帐户信息
        /// </summary>
        /// <param name="index">索引</param>
        void BindAccountInfo(int index);

        /// <summary>
        /// Gets or sets 用于绑定帐户信息网络
        /// </summary>
        /// <value>用于绑定帐户信息网络</value>
        DataTable AccountTable { get; set; }
    }
}
