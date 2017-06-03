using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_FinancialStatistics.Winform.IView
{
    /// <summary>
    /// 住院收入统计接口
    /// </summary>
    public interface IFrmIpatientRevenue
    {
        /// <summary>
        /// 设置组织机构
        /// </summary>
        /// <param name="dtWorker">组织机构数据</param>
        void SetWorkers(DataTable dtWorker);
    }
}
