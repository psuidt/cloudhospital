using System;
using System.Data;
using System.Data.Common;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_IPManage.Dao
{
    /// <summary>
    /// 费用清单
    /// </summary>
    public class SqlExpenseListDao : AbstractDao, IExpenseListDao
    {
        /// <summary>
        /// 获取科室病人费用信息
        /// </summary>
        /// <param name="sDeptCode">科室编码</param>
        /// <param name="sDTInBegin">入院开始日期</param>
        /// <param name="sDTInEnd">入院结束日期</param>
        /// <param name="iPatientState">病人状态</param>
        /// <param name="sPatient">病人名或住院号</param>
        /// <param name="iJsId">结算ID</param>
        /// <returns>科室病人费用信息</returns>
        public DataTable GetDeptPatientInfoList(string sDeptCode, string sDTInBegin, string sDTInEnd, int iPatientState, string sPatient,int iJsId)
        {
            string sql = @"SELECT ipp.PatListID,ipp.SerialNumber,ipp.BedNo,ipp.PatName,ipp.PatTypeID,dbo.fnGetPatTypeName(ipp.PatTypeID) PatTypeName,
				ISNULL((SELECT SUM(a.TotalFee) FROM IP_DepositList a WHERE a.PatListID=ipp.PatListID and (({5}=0 and a.CostHeadID=0) or ({5}>0 and a.CostHeadID>0))),0) TotalDespoit,
				SUM(ipf.TotalFee) TotalFee,
                (ISNULL((SELECT SUM(a.TotalFee) FROM IP_DepositList a WHERE a.PatListID=ipp.PatListID and (({5}=0 and a.CostHeadID=0) or ({5}>0 and a.CostHeadID>0))),0)-ISNULL(SUM(ipf.TotalFee),0)) YE,
                ipp.EnterHDate,ipp.LeaveHDate,DATEDIFF(DAY,EnterHDate,GETDATE()) AS InDays,
				ipp.Age,(CASE ipp.Sex WHEN 1 THEN '男' ELSE '女' END ) Sex,ipp.CurrDeptID	,dbo.fnGetDeptName(ipp.CurrDeptID) DeptName	
				FROM dbo.IP_PatList ipp			
				INNER JOIN 	dbo.IP_PatientInfo ippi ON ipp.PatListID=ippi.PatListID
				LEFT JOIN dbo.IP_FeeItemRecord ipf ON ipf.PatListID=ipp.PatListID  AND (({5}=0 and ipf.CostHeadID=0 ) OR ({5}>0 and ipf.CostHeadID>0 ) )
                where (ipp.CurrDeptID='{0}' or '{0}'=-1) 
                    --and (({3}<>4 and ipp.EnterHDate>='{1}' and ipp.EnterHDate<='{2}' )
                    --    or 
                    --    ({3}=4 and ipp.LeaveHDate>='{1}' and ipp.LeaveHDate<='{2}' ))
                    and ipp.Status={3} 
                    and (ipp.SerialNumber like '%{4}%' or ipp.PatName like '%{4}%' )                   
				GROUP BY ipp.PatListID,ipp.PatTypeID,ipp.CurrDeptID,ipp.BedNo,ipp.PatName,ipp.SerialNumber,ipp.Age,ipp.Sex,ipp.EnterHDate,ipp.LeaveHDate";
            sql = string.Format(sql,  sDeptCode,  sDTInBegin,  sDTInEnd,  iPatientState, sPatient, iJsId);
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 获取病人费用信息汇总
        /// </summary>
        /// <param name="iWorkId">机构ID</param>
        /// <param name="iPatientId">病人登记ID</param>
        /// <param name="iCostHeadId">结算头ID</param>
        /// <returns>病人费用汇总信息</returns>
        public DataTable GetPatientFeeSumByPatientId(int iWorkId, int iPatientId,int iCostHeadId)
        {
            string sql = @"	SELECT  ipp.PatListID,ipp.CaseNumber,ipp.EnterDiseaseName,
                                    ipp.SerialNumber,ipp.BedNo,ipp.PatName,
				                    ISNULL((SELECT SUM(a.TotalFee) FROM IP_DepositList a WHERE a.PatListID=ipp.PatListID AND (a.CostHeadID={2} or ( {2}=-1 and a.CostHeadID=0))),0) TotalDespoit,
				                    SUM(ISNULL(ipf.TotalFee,0)) TotalFee,
                                    (ISNULL((SELECT SUM(a.TotalFee) FROM IP_DepositList a WHERE a.PatListID=ipp.PatListID AND (a.CostHeadID={2} or ( {2}=-1 and a.CostHeadID=0))),0) )-ISNULL(SUM(ipf.TotalFee),0) YE  ,
                                    ipp.EnterHDate,ipp.LeaveHDate,DATEDIFF(DAY,EnterHDate,GETDATE()) AS InDays,   
                                    ipp.Age,(CASE ipp.Sex WHEN 1 THEN '男' ELSE '女' END ) Sex,dbo.fnGetDeptName(ipp.CurrDeptID) DeptName	     
				            FROM dbo.IP_PatList ipp	
                       LEFT JOIN dbo.IP_FeeItemRecord ipf ON ipf.PatListID=ipp.PatListID AND (ipf.CostHeadID={2} or ( {2}=-1 and ipf.CostHeadID=0))
                           where ipp.WorkID={0} AND  ipp.PatListID={1} 
				        GROUP BY ipp.PatListID ,ipp.CaseNumber,ipp.EnterDiseaseName,ipp.SerialNumber,ipp.BedNo,ipp.PatName,ipp.EnterHDate,ipp.LeaveHDate,ipp.Age,ipp.Sex,ipp.CurrDeptID";
            sql = string.Format(sql, iWorkId, iPatientId, iCostHeadId);
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 获取病人费用清单数据
        /// </summary>
        /// <param name="iWorkId">机构ID</param>
        /// <param name="iPatientId">病人ID</param>
        /// <param name="iListType">清单类型  1-项目明细  2-一日清单  3-发票项目  4-项目汇总</param>
        /// <param name="sBDate">开始时间</param>
        /// <param name="sEDate">结束事件</param>
        /// <param name="iJsState">计算状态 0.未结算 1，中途，2，出院，3，欠费</param>
        /// <param name="iDateType">0.记账时间 1.费用时间</param>
        /// <param name="iCostHeadId">结算头ID</param>
        /// <returns>病人费用清单数据</returns>
        public DataTable GetPatientFeeInfo(int iWorkId,int iPatientId,int iListType,string sBDate,string sEDate,int iJsState,int iDateType,int iCostHeadId)
        {
            try
            {
                IDbCommand cmd = oleDb.GetProcCommand("SP_IP_GetPatientCostDetail");
                oleDb.AddInParameter(cmd as DbCommand, "@WorkID", DbType.Int32, iWorkId);
                oleDb.AddInParameter(cmd as DbCommand, "@PatientID", DbType.Int32, iPatientId);
                oleDb.AddInParameter(cmd as DbCommand, "@ListType", DbType.Int32, iListType);
                oleDb.AddInParameter(cmd as DbCommand, "@BeginTime", DbType.String, sBDate);
                oleDb.AddInParameter(cmd as DbCommand, "@EndTime", DbType.String, sEDate);
                oleDb.AddInParameter(cmd as DbCommand, "@JsState", DbType.Int32, iJsState);
                oleDb.AddInParameter(cmd as DbCommand, "@DateType", DbType.Int32, iDateType);
                oleDb.AddInParameter(cmd as DbCommand, "@AcctualCost", DbType.Int32, 1);
                oleDb.AddInParameter(cmd as DbCommand, "@CostHeadID", DbType.Int32, iCostHeadId);
                oleDb.AddOutParameter(cmd as DbCommand, "ErrCode", DbType.Int32, 5);
                oleDb.AddOutParameter(cmd as DbCommand, "ErrMsg", DbType.AnsiString, 200);
                oleDb.DoCommand(cmd);

                DataTable dt = oleDb.GetDataTable(cmd);
                return dt;
            }
            catch (Exception ex)
            {
                //return null;
                throw ex;
            }
        }
    }
}
