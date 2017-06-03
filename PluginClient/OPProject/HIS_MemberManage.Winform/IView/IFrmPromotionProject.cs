using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_MemberManage.Winform.IView
{
    /// <summary>
    /// 优惠方案接口
    /// </summary>
    public interface IFrmPromotionProject : IBaseView
    {
        /// <summary>
        /// Gets or sets 优惠明细ID
        /// </summary>
        /// <value>优惠明细ID</value>
        int PromSunID { get; set; }

        /// <summary>
        /// Gets or sets 头标识id
        /// </summary>
        /// <value>优惠明细ID</value>
        int HeadFlag { get; set; }

        /// <summary>
        /// Gets or sets 方案id
        /// </summary>
        /// <value>方案id</value>
        int PromID { get; set; }

        /// <summary>
        /// Gets or sets 头名称
        /// </summary>
        /// <value>头名称</value>
        string HeadName { get; set; }

        /// <summary>
        /// Gets or sets 开始日期
        /// </summary>
        /// <value>开始日期</value>
        string StDate { get; set; }

        /// <summary>
        /// Gets or sets 结束日期
        /// </summary>
        /// <value>结束日期</value>
        string EndsDate { get; set; }

        /// <summary>
        /// 绑定优惠方案头信息
        /// </summary>
        /// <param name="dt">优惠项目头信息</param>
        void BindPromotionProjectHeadInfo(DataTable dt);

        /// <summary>
        /// Gets or sets  明细标识
        /// </summary>
        /// <value>明细标识</value>
        int DetailFlag { get; set; }
    }
}
