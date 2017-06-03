using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_FinancialStatistics.Dao
{
    /// <summary>
    /// 经管核算Dao
    /// </summary>
    public class SqlFinacialDao : AbstractDao, IFinacialDao
    {
        #region 收费员工作量统计
        /// <summary>
        /// 收费员工作量统计
        /// </summary>
        /// <param name="workId">机构ID</param>
        /// <param name="queryType">查询类别 0门诊1住院</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <returns>收费员工作量统计数据</returns>
        public DataTable GetFinacialChargerData(int workId, int queryType, DateTime bdate, DateTime edate)
        {
            try
            {
                IDbCommand cmd = oleDb.GetProcCommand("SP_Finacial_Charger");
                oleDb.AddInParameter(cmd as DbCommand, "@WorkID", DbType.Int32, workId);
                oleDb.AddInParameter(cmd as DbCommand, "@Type", DbType.Int32, queryType);
                oleDb.AddInParameter(cmd as DbCommand, "@BDate", DbType.Date, bdate);
                oleDb.AddInParameter(cmd as DbCommand, "@EDate", DbType.Date, edate);
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

        #region 门诊医生工作量统计
        /// <summary>
        /// 门诊医生工作量统计
        /// </summary>
        /// <param name="workId">机构ID</param>
        /// <param name="queryTimeType">时间类别 0收费时间 1缴款时间</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <returns>门诊医生工作量统计数据</returns>
        public DataTable GetFinacialOPDoctorData(int workId, int queryTimeType, DateTime bdate, DateTime edate)
        {
            try
            {
                IDbCommand cmd = oleDb.GetProcCommand("SP_Finacial_OPDoctor");
                oleDb.AddInParameter(cmd as DbCommand, "@WorkID", DbType.Int32, workId);
                oleDb.AddInParameter(cmd as DbCommand, "@Type", DbType.Int32, queryTimeType);
                oleDb.AddInParameter(cmd as DbCommand, "@BDate", DbType.AnsiString, bdate);
                oleDb.AddInParameter(cmd as DbCommand, "@EDate", DbType.AnsiString, edate);
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
        #region 门诊收入统计
        /// <summary>
        /// 门诊收入统计
        /// </summary>
        /// <param name="workId">机构ID</param>
        /// <param name="queryTimeType">查询时间类型0收费时间1缴款时间</param>
        /// <param name="bdate">开始时间</param>
        /// <param name="edate">结束时间</param>
        /// <param name="rowGroupType">行统计类别 0开方医生1开方科室2执行科室3开方科室+开方医生4病人类型</param>
        /// <param name="colGroupType">列统计方式 0统计大项目1核算分类2财务分类3门诊发票分类4支付方式</param>
        /// <returns>门诊收入统计数据</returns>
        public DataTable GetFinacialOPRevenueData(int workId, int queryTimeType, DateTime bdate, DateTime edate,int rowGroupType,int colGroupType)
        {
            try
            {
                IDbCommand cmd = oleDb.GetProcCommand("SP_Finacial_OPRevenue");
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

        #region 发药人员工作量统计
        /// <summary>
        /// 发药人员工作量统计
        /// </summary>
        /// <param name="workId">机构ID</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <returns>发药人员工作量统计数据</returns>
        public DataTable GetFinacialDispenseData(int workId, DateTime bdate, DateTime edate)
        {
            try
            {
                IDbCommand cmd = oleDb.GetProcCommand("SP_Finacial_Dispensing");
                oleDb.AddInParameter(cmd as DbCommand, "@WorkID", DbType.Int32, workId);
                oleDb.AddInParameter(cmd as DbCommand, "@BDate", DbType.Date, bdate);
                oleDb.AddInParameter(cmd as DbCommand, "@EDate", DbType.Date, edate);
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
