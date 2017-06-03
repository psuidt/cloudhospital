using System.Collections.Generic;
using System.Data;

namespace HIS_IPManage.Dao
{
    /// <summary>
    /// 住院病人结算查询
    /// </summary>
    public interface IIPCostSearchDao
    {
        /// <summary>
        /// 按支付方式统计
        /// </summary>
        /// <param name="paramsDic">统计参数</param>
        /// <returns>统计结果列表</returns>
        DataTable GetCostData(Dictionary<string, object> paramsDic);

        /// <summary>
        /// 按项目分类统计
        /// </summary>
        /// <param name="paramsDic">统计参数</param>
        /// <returns>分类统计结果</returns>
        DataTable GetCostDataGroupItem(Dictionary<string, object> paramsDic);

        /// <summary>
        /// 查询结算明细记录
        /// </summary>
        /// <param name="costHeadID">结算ID</param>
        /// <returns>结算明细记录</returns>
        DataTable GetCostDetail(int costHeadID);
    }
}
