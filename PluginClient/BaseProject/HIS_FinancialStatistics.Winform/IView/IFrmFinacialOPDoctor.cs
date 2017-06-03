using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_FinancialStatistics.Winform.IView
{
    /// <summary>
    /// 门诊医生工作量统计接口
    /// </summary>
    public interface IFrmFinacialOPDoctor
    {
        /// <summary>
        /// 绑定所有医疗机构名称
        /// </summary>
        /// <param name="dtWorker">组织机构数据</param>
        void SetWorkers(DataTable dtWorker);
    }
}
