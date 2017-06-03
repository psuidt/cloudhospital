using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS_Entity.BasicData.BusiEntity;

namespace HIS_FinancialStatistics.Dao
{
    /// <summary>
    /// 经管工作量统计Dao接口
    /// </summary>
    public interface IWorkLoadDao
    {
        /// <summary>
        /// 病人工作量统计
        /// </summary>
        /// <param name="workID">组织机构ID</param>
        /// <param name="beDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="docTypeValue">医生类型</param>
        /// <param name="timeTypeValue">时间类型</param>
        /// <returns>病人工作量统计数据</returns>
        DataTable QueryInpatientWorkLoad(int workID, string beDate, string endDate, string docTypeValue, string timeTypeValue);

        /// <summary>
        /// 查询项目信息
        /// </summary>
        /// <returns>项目信息</returns>
        DataTable QueryItemInfo();

        /// <summary>
        /// 取得项目统计数据
        /// </summary>
        /// <param name="workID">组织机构id</param>
        /// <param name="patType">病人类型</param>
        /// <param name="timeType">项目类型</param>
        /// <param name="beDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="beDateMOM">开始月份</param>
        /// <param name="endDateMOM">结束月份</param>
        /// <param name="beDateYOY">开始年份</param>
        /// <param name="endDateYOY">结束年份</param>
        /// <param name="itemInfo">项目信息</param>
        /// <returns>项目统计数据</returns>
        DataTable GetItmeItemStatistics(int workID, int patType, string timeType, string beDate, string endDate, string beDateMOM, string endDateMOM, string beDateYOY, string endDateYOY, string itemInfo);

        /// <summary>
        /// 住院收入统计
        /// </summary>
        /// <param name="workId">机构ID</param>
        /// <param name="queryTimeType">查询时间类型0记费时间 1结算时间 2缴款时间</param>
        /// <param name="bdate">开始时间</param>
        /// <param name="edate">结束时间</param>
        /// <param name="rowGroupType">行统计类别 0开方医生 1开方科室 2执行科室 3主治医生 4病人类型 5病人所在科室</param>
        /// <param name="colGroupType">列统计方式 0统计大项目 1核算分类 2财务分类 3住院发票分类 4支付方式</param>
        /// <returns>住院收入统计数据</returns>
        DataTable GetFinacialIPRevenueData(int workId, int queryTimeType, DateTime bdate, DateTime edate, int rowGroupType, int colGroupType);
        
        /// <summary>
        /// 住院应收款统计
        /// </summary>
        /// <param name="workId">机构Id</param>
        /// <param name="queryType">统计类型 0收入流水账1预交金流水账</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <returns>住院应收款统计数据</returns>
        DataTable GetFinacialIPAccountBookData(int workId, int queryType, DateTime bdate, DateTime edate);
    }
}
