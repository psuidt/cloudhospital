using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_MemberManage.Winform.IView
{
    /// <summary>
    /// 帐户明细接口
    /// </summary>
    public interface IFrmAccountDetail: IBaseView
    {
        /// <summary>
        /// Gets or sets开始日期
        /// </summary>
        /// <value>开始日期</value>
        string BDate { get; set; }

        /// <summary>
        /// Gets or sets结束日期
        /// </summary>
        /// <value>结束日期</value>
        string EDate { get; set; }

        /// <summary>
        /// Gets or sets帐户id
        /// </summary>
        /// <value>帐户id</value>
        int AccountID { get; set; }

        /// <summary>
        /// Gets or sets积分表
        /// </summary>
        /// <value>积分表</value>
        DataTable ScoreTable { get; set; }
    }
}
