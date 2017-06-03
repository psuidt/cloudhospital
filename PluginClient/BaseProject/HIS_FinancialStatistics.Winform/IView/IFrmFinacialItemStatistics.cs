using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_FinancialStatistics.Winform.IView
{
    /// <summary>
    /// 单项目统计界面接口
    /// </summary>
    public interface IFrmFinacialItemStatistics
    {
        /// <summary>
        /// 设置组织机构
        /// </summary>
        /// <param name="dt">组织机构</param>
        void SetWork(DataTable dt);

        /// <summary>
        /// Gets or sets项目ID
        /// </summary>
        /// <value>项目编码.</value>
        string ItemID { get; set; }
    }
}
