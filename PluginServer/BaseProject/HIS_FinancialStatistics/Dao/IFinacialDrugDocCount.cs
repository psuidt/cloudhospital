using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_FinancialStatistics.Dao
{
    /// <summary>
    /// 经管核算Dao接口
    /// </summary>
    public interface IFinacialDrugDocCount
    {
        /// <summary>
        /// 获取药品医生开方数量
        /// </summary>
        /// <param name="iworkId">组织机构ID</param>
        /// <param name="iDocId">医生ID</param>
        /// <param name="iType">类型</param>
        /// <param name="bDate">开始日期</param>
        /// <param name="eDate">结束日期</param>
        /// <returns>药品医生开方数量数据</returns>
        DataTable GetDrugDocCount(int iworkId,int iDocId,int iType,DateTime bDate,DateTime eDate);
    }
}
