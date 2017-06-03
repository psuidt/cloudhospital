using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.BasicData.BusiEntity;

namespace HIS_FinancialStatistics.Dao
{
    /// <summary>
    /// 经管工作量统计dao
    /// </summary>
    public class SqlWorkLoadDao : AbstractDao, IWorkLoadDao
    {
        /// <summary>
        /// 住院医生工作量统计
        /// </summary>
        /// <param name="workID">组织机构id</param>
        /// <param name="beDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="docTypeValue">人员类型</param>
        /// <param name="timeTypeValue">时间类型</param>
        /// <returns>住院医生工作量统计数据</returns>
        public DataTable QueryInpatientWorkLoad(int workID, string beDate, string endDate, string docTypeValue, string timeTypeValue)
        {
            try
            {
                IDbCommand cmd = oleDb.GetProcCommand("SP_Finacial_WorkloadStatistics");
                oleDb.AddInParameter(cmd as DbCommand, "@WorkID", DbType.Int32, workID);
                oleDb.AddInParameter(cmd as DbCommand, "@DocType", DbType.AnsiString, docTypeValue);
                oleDb.AddInParameter(cmd as DbCommand, "@TimeType", DbType.AnsiString, timeTypeValue);
                oleDb.AddInParameter(cmd as DbCommand, "@StDate", DbType.AnsiString, beDate);
                oleDb.AddInParameter(cmd as DbCommand, "@EndDate", DbType.AnsiString, endDate);
                oleDb.AddOutParameter(cmd as DbCommand, "@ErrMsg", DbType.AnsiString, 2000);
                return oleDb.GetDataTable(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 查询项目信息
        /// </summary>
        /// <returns>查询项目信息数据</returns>
        public DataTable QueryItemInfo()
        {
            string sql = @" select * from ViewFeeItem_SimpleList where ItemClass<>1";
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 获取单项目统计数据
        /// </summary>
        /// <param name="workID">组织机构id</param>
        /// <param name="patType">病人类型</param>
        /// <param name="timeType">时间类型</param>
        /// <param name="beDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="beDateMOM">开始月份</param>
        /// <param name="endDateMOM">结束月份</param>
        /// <param name="beDateYOY">开始年份</param>
        /// <param name="endDateYOY">结束年份</param>
        /// <param name="itemInfo">项目信息</param>
        /// <returns>单项目统计数据</returns>
        public DataTable GetItmeItemStatistics(int workID,int patType,string timeType, string beDate, string endDate, string beDateMOM, string endDateMOM, string beDateYOY, string endDateYOY, string itemInfo)
        {
            try
            {
                IDbCommand cmd = oleDb.GetProcCommand("SP_Finacial_ItemStatistics");
                oleDb.AddInParameter(cmd as DbCommand, "@WorkID", DbType.Int32, workID);
                oleDb.AddInParameter(cmd as DbCommand, "@PatType", DbType.Int32, patType);
                oleDb.AddInParameter(cmd as DbCommand, "@TimeType", DbType.AnsiString, timeType);
                oleDb.AddInParameter(cmd as DbCommand, "@BeDate", DbType.AnsiString, beDate);
                oleDb.AddInParameter(cmd as DbCommand, "@EndDate", DbType.AnsiString, endDate);
                oleDb.AddInParameter(cmd as DbCommand, "@BeDateMOM", DbType.AnsiString, beDateMOM);
                oleDb.AddInParameter(cmd as DbCommand, "@EndDateMOM", DbType.AnsiString, endDateMOM);
                oleDb.AddInParameter(cmd as DbCommand, "@BeDateYOY", DbType.AnsiString, beDateYOY);
                oleDb.AddInParameter(cmd as DbCommand, "@EndDateYOY", DbType.AnsiString, endDateYOY);
                oleDb.AddInParameter(cmd as DbCommand, "@ItemID", DbType.AnsiString, itemInfo);
                oleDb.AddOutParameter(cmd as DbCommand, "@Sql", DbType.AnsiString, 2000);
                return oleDb.GetDataTable(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region 住院收入统计
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
        public DataTable GetFinacialIPRevenueData(int workId, int queryTimeType, DateTime bdate, DateTime edate, int rowGroupType, int colGroupType)
        {
            try
            {
                IDbCommand cmd = oleDb.GetProcCommand("SP_Finacial_IPRevenue");
                oleDb.AddInParameter(cmd as DbCommand, "@WorkID", DbType.Int32, workId);
                oleDb.AddInParameter(cmd as DbCommand, "@Type", DbType.Int32, queryTimeType);
                oleDb.AddInParameter(cmd as DbCommand, "@BDate", DbType.AnsiString, bdate);
                oleDb.AddInParameter(cmd as DbCommand, "@EDate", DbType.AnsiString, edate);
                oleDb.AddInParameter(cmd as DbCommand, "@RowGroupType", DbType.Int32, rowGroupType);
                oleDb.AddInParameter(cmd as DbCommand, "@ColGroupType", DbType.Int32, colGroupType);
                oleDb.AddOutParameter(cmd as DbCommand, "@ErrCode", DbType.Int32, 5);
                oleDb.AddOutParameter(cmd as DbCommand, "@ErrMsg", DbType.AnsiString, 200);
                return oleDb.GetDataTable(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 住院应收款统计
        /// <summary>
        /// 住院应收款统计
        /// </summary>
        /// <param name="workId">机构Id</param>
        /// <param name="queryType">统计类型 0收入流水账1预交金流水账</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <returns>住院应收款统计数据</returns>
        public DataTable GetFinacialIPAccountBookData(int workId, int queryType, DateTime bdate, DateTime edate)
        {
            try
            {
                IDbCommand cmd = oleDb.GetProcCommand("SP_Finacial_IPAccountBookData");
                oleDb.AddInParameter(cmd as DbCommand, "@WorkID", DbType.Int32, workId);
                oleDb.AddInParameter(cmd as DbCommand, "@Type", DbType.Int32, queryType); //统计类型 0收入流水账1预交金流水账
                oleDb.AddInParameter(cmd as DbCommand, "@BDate", DbType.AnsiString, bdate.ToString("yyyyMMdd"));
                oleDb.AddInParameter(cmd as DbCommand, "@EDate", DbType.AnsiString, edate.ToString("yyyyMMdd"));
                oleDb.AddOutParameter(cmd as DbCommand, "@ErrCode", DbType.Int32, 5);
                oleDb.AddOutParameter(cmd as DbCommand, "@ErrMsg", DbType.AnsiString, 200);
                return oleDb.GetDataTable(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
