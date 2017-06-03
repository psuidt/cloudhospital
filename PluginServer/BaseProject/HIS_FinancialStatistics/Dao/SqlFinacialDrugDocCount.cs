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
    /// 经管药品医生统计Dao
    /// </summary>
    public class SqlFinacialDrugDocCount : AbstractDao, IFinacialDrugDocCount
    {
        /// <summary>
        /// 获取药品医生开方数量
        /// </summary>
        /// <param name="iworkId">组织机构id</param>
        /// <param name="iDocId">医生id</param>
        /// <param name="iType">类型</param>
        /// <param name="bDate">开始日期</param>
        /// <param name="eDate">结束日期</param>
        /// <returns>药品医生开方数量数据</returns>
        public DataTable GetDrugDocCount(int iworkId, int iDocId, int iType, DateTime bDate, DateTime eDate)
        {
            try
            {
                IDbCommand cmd = oleDb.GetProcCommand("SP_Finacial_DoctorDrugCount");
                oleDb.AddInParameter(cmd as DbCommand, "@WorkID", DbType.Int32, iworkId);
                oleDb.AddInParameter(cmd as DbCommand, "@DoctorID", DbType.Int32, iDocId);
                oleDb.AddInParameter(cmd as DbCommand, "@Type", DbType.Int32, iType);
                oleDb.AddInParameter(cmd as DbCommand, "@BDate", DbType.String, bDate.ToString("yyyy-MM-dd 00:00:00"));
                oleDb.AddInParameter(cmd as DbCommand, "@EDate", DbType.String, eDate.ToString("yyyy-MM-dd 23:59:59"));
                oleDb.AddOutParameter(cmd as DbCommand, "@ErrCode", DbType.Int32, 5);
                oleDb.AddOutParameter(cmd as DbCommand, "@ErrMsg", DbType.AnsiString, 200);
                return oleDb.GetDataTable(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
