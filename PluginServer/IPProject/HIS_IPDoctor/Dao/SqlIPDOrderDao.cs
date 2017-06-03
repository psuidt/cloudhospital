using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_IPDoctor.Dao
{
    /// <summary>
    /// 住院医生数据访问接口实现类
    /// </summary>
    public class SqlIPDOrderDao : AbstractDao, IIPDOrderDao
    {
        /// <summary>
        /// 住院医生站主界面获取床位信息
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="doctorID">医生ID</param>
        /// <param name="isMy">true我的病人false科室病人</param>
        /// <returns>病人数据</returns>
        public DataTable GetInBedPatient(int deptID,int doctorID, bool isMy)
        {
            string strWhere = string.Empty;
            if (isMy)
            {
                strWhere += " and a.CurrDoctorID=" + doctorID;
            }

            string strsql = @"select a.BedNo,
                                     a.patlistid,
                                     a.SerialNumber,
                                     a.CaseNumber,
                                     a.PatName,
                                     a.CurrWardID as WardID,
                                     dbo.fnGetWardName(a.CurrWardID) WardName,
                                     dbo.fnGetEmpName(a.CurrDoctorID) DoctorName,
                                     dbo.fnGetDeptName(a.CurrDeptID) DeptName,
                                     dbo.fnGetEmpName(a.CurrNurseID) NurseName,
                                     a.CurrDeptID as PatDeptID,
                                     a.Age,
                                     a.status,
                                     a.IsLeaveHosOrder,
                                     a.NursingLever,
                                     a.DietType,
                                     dbo.fnGetPatTypeName(a.PatTypeID) patTypeName,
                                     case when a.Sex=1 then '男' when a.Sex=2 then '女' else '未知' end as PatSex,
                                     a.EnterDiseaseName,
                                     a.EnterHDate,
                                     a.OutSituation,
                                     c.Name as OutSituationName
                              from IP_PatList a 
                               left join (select Code ,Name FROM BaseDictContent WHERE ClassId=1017 AND DelFlag=0 )c
                              on a.OutSituation=c.Code
                             where  a.workid=" + oleDb.WorkId+@" and  a.status in(2,3,5)
                             and a.CurrDeptID=" + deptID+ " "+strWhere+" order by a.BedNO";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 住院医生站查询病人信息
        /// </summary>
        /// <param name="doctorID">医生ID</param>
        /// <param name="deptID">科室ID</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="isOut">true出院false在院</param>
        /// <param name="isMy">true我的病人false科室病人</param>
        /// <param name="queryContent">查询内容</param>
        /// <param name="patListID">病人ID</param>
        /// <returns>病人信息</returns>
        public DataTable GetPatientInfo(int doctorID, int deptID,DateTime bdate,DateTime edate,bool isOut, bool isMy,string queryContent,int patListID)
        {
            string strWhere = string.Empty;
            if (patListID == 0)
            {
                if (isOut)
                {
                    strWhere += " and a.status=4 and a.EnterHDate>='" + bdate.ToString("yyyy-MM-dd 00:00:00") + "' and a.EnterHDate<='" + edate.ToString("yyyy-MM-dd 23:59:59") + "'";
                }
                else
                {
                    strWhere += "  and a.status in(2,3,5)";
                }

                strWhere += " and a.CurrDeptID = " + deptID;
                if (isMy)
                {
                    strWhere += " and a.CurrDoctorID=" + doctorID;
                }

                if (queryContent != string.Empty)
                {
                    string str = string.Empty;
                    try
                    {
                        str = " and (a.PatName='" + queryContent + "' or a.SerialNumber='" + queryContent + "' or a.BedNo='" + queryContent + "')";
                    }
                    catch
                    {
                        str = " and (a.PatName='" + queryContent + "' or a.BedNo='" + queryContent + "')";
                    }

                    strWhere += str;
                }                
            }
            else
            {
                strWhere += " and  a.patlistid=" + patListID;
            }

            string strsql = @"select 0 as sel, a.BedNo,
                                     a.patlistid,
                                     a.MemberID,
                                     a.SerialNumber,
                                     a.PatName,                                     
                                     dbo.fnGetEmpName(a.CurrDoctorID) doctorName,
                                     dbo.fnGetDeptName(a.CurrDeptID) deptName,
                                     dbo.fnGetEmpName(a.CurrNurseID) NurseName,
                                     a.CurrDeptID as PatDeptID,
                                     a.Age,
                                     a.status,
                                     a.NursingLever,
                                     a.DietType,
                                     dbo.fnGetPatTypeName(a.PatTypeID) patTypeName,
                                     case when a.Sex=1 then '男' when a.Sex=2 then '女' else '未知' end as PatSex,
                                     a.EnterDiseaseName,
                                     a.EnterHDate,
                                     a.Times,
                                     a.IsLeaveHosOrder,
                                     b.Name as EnterSituationName,
                                     a.CaseNumber
                              from IP_PatList a
                              left join (SELECT * FROM BaseDictContent WHERE ClassId=1017) b 
                              on a.EnterSituation=b.Code
                              where  a.workid=" + oleDb.WorkId+""+ strWhere+ " order by a.BedNo";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取病人最新预交金总额以及未结算费用总额
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>费用信息</returns>
        public DataTable GetPatDepositFee(int patListID)
        {
            string strSql = @"SELECT CASE WHEN SUM(TotalFee)>0 THEN SUM(TotalFee) ELSE 0 END TotalFee 
                                FROM IP_DepositList WHERE PatListID={0} AND CostHeadID=0 AND WorkID = {1}
                                UNION ALL
                                SELECT CASE WHEN SUM(TotalFee)>0 THEN SUM(TotalFee) ELSE 0 END TotalFee FROM 
                                IP_FeeItemRecord WHERE PatListID={0} AND CostHeadID =0 AND WorkID = {1}";
            strSql = string.Format(strSql, patListID, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取病人医嘱数据
        /// </summary>
        /// <param name="orderCategory">医嘱类型 0长嘱1临嘱 3医嘱打印查询</param>
        /// <param name="patlistid">病人Id</param>
        /// <param name="orderStatus">医嘱状态<10按指定状态查找 =10查找所有!=10有效医嘱</param>
        /// <param name="deptID">科室ID</param>
        /// <returns>医嘱数据</returns>
        public DataTable GetOrders(int orderCategory, int patlistid, int orderStatus, int deptID)
        {
            if (orderCategory == 3)
            {
                //医嘱打印查询医嘱，长嘱临嘱一起查询
                string strsql = @"select *,'' as BDate,'' as BTime,'' as Edate,'' as ETime,
                             dbo.fnGetEmpName(OrderDoc) as OrderDocName,
                              dbo.fnGetEmpName(ExecNurse) as ExecNurseName,
                             dbo.fnGetEmpName(EOrderDoc) as EOrderDocName,
                               dbo.fnGetEmpName(TransNurse) as TransNurseName,
                              dbo.fnGetDeptName(ExecDeptID) as ExecDeptName,
                              ( select sum(totalfee) from IP_FeeItemRecord where orderid=a.orderid) as sumfee
                              from IPD_OrderRecord a where DeleteFlag=0  and PatListID={0}  AND WorkID = {1} order by OrderCategory,OrderBdate";
                strsql = string.Format(strsql, patlistid, oleDb.WorkId);
                return oleDb.GetDataTable(strsql);
            }
            else
            {
                if (orderStatus < 10)
                {
                    //查找指定状态的医嘱
                    string strsql = @"select *,
                             dbo.fnGetEmpName(OrderDoc) as OrderDocName,
                              dbo.fnGetEmpName(ExecNurse) as ExecNurseName,
                             dbo.fnGetEmpName(EOrderDoc) as EOrderDocName,
                             dbo.fnGetDeptName(ExecDeptID) as ExecDeptName
                              from IPD_OrderRecord where DeleteFlag=0 and OrderCategory={0} and PatListID={1} and orderStatus={2} AND WorkID = {3}";
                    strsql = string.Format(strsql, orderCategory, patlistid, orderStatus, oleDb.WorkId);
                    return oleDb.GetDataTable(strsql);
                }
                else
                {
                    if (orderStatus == 10) 
                    {
                        //所有医嘱
                        string strsql = @"select *,
                             dbo.fnGetEmpName(OrderDoc) as OrderDocName,
                              dbo.fnGetEmpName(ExecNurse) as ExecNurseName,
                             dbo.fnGetEmpName(EOrderDoc) as EOrderDocName,
                             dbo.fnGetDeptName(ExecDeptID) as ExecDeptName
                              from IPD_OrderRecord where DeleteFlag=0 and  OrderCategory={0} and PatListID={1}  AND WorkID = {2}";
                        strsql = string.Format(strsql, orderCategory, patlistid, oleDb.WorkId);
                        return oleDb.GetDataTable(strsql);
                    }
                    else 
                    {
                        //有效医嘱
                        string strsql = @"select *,
                             dbo.fnGetEmpName(OrderDoc) as OrderDocName,
                              dbo.fnGetEmpName(ExecNurse) as ExecNurseName,
                             dbo.fnGetEmpName(EOrderDoc) as EOrderDocName,
                             dbo.fnGetDeptName(ExecDeptID) as ExecDeptName
                              from IPD_OrderRecord where DeleteFlag=0 and orderStatus<5 and  OrderCategory={0} and PatListID={1}  AND WorkID = {2}";
                        strsql = string.Format(strsql, orderCategory, patlistid, oleDb.WorkId);
                        return oleDb.GetDataTable(strsql);
                    }
                }
            }           
        }

        /// <summary>
        /// 获取血糖测量数据
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <returns>返回血糖数据</returns>
        public DataTable GetBloodGluRecord(int patlistid)
        {
            DataTable dtBloodGlu = new DataTable();

          string strsql= @"  select * from
  (SELECT PatlistID, Date, '时间' as  QueryName,MAX(早餐前) 早餐前,MAX(早餐后) 早餐后,MAX(午餐前) 午餐前,MAX(午餐后) 午餐后,MAX(晚餐前) 晚餐前,MAX(晚餐后) 晚餐后,MAX(睡前) 睡前,MAX(夜间) 夜间

                              FROM(
                              select max(a.BloodTimes) BloodTimes, a.PatlistID,
                                     convert(varchar(10), a.opreratorDate, 120) as Date,
                                    max(case when DateType = 1 then convert(varchar(5), a.opreratorDate, 24) ELSE '' end) 早餐前,
                                    max(case when DateType = 2 then convert(varchar(5), a.opreratorDate, 24) ELSE '' end) 早餐后,
                                    max(case when DateType = 3 then convert(varchar(5), a.opreratorDate, 24) ELSE '' end) 午餐前,
                                    max(case when DateType = 4 then convert(varchar(5), a.opreratorDate, 24) ELSE '' end) 午餐后,
                                    max(case when DateType = 5 then convert(varchar(5), a.opreratorDate, 24) ELSE '' end) 晚餐前,
                                    max(case when DateType = 6 then convert(varchar(5), a.opreratorDate, 24) ELSE '' end) 晚餐后,
                                    max(case when DateType = 7 then convert(varchar(5), a.opreratorDate, 24) ELSE '' end) 睡前,
                                    max(case when DateType = 8 then convert(varchar(5), a.opreratorDate, 24) ELSE '' end) 夜间
                              from IP_PluRecord a where a.patlistid=" + patlistid + @"  and a.DelFlag=0 and a.workid=" + oleDb.WorkId + @"
                              GROUP BY a.PatlistID, a.OpreratorDate, a.DateType
                              ) tab1
                              GROUP BY tab1.PatlistID,tab1.Date
                                      UNION ALL
 SELECT PatlistID, Date,'血糖' as  QueryName,convert(varchar(10), MAX(早餐前)) 早餐前,convert(varchar(10), MAX(早餐后)) 早餐后,convert(varchar(10), MAX(午餐前)) 午餐前,convert(varchar(10), MAX(午餐后)) 午餐后,convert(varchar(10), MAX(晚餐前)) 晚餐前,convert(varchar(10), MAX(晚餐后)) 晚餐后,convert(varchar(10), MAX(睡前)) 睡前,convert(varchar(10), MAX(夜间)) 夜间

                              FROM(
                              select max(a.BloodTimes) BloodTimes, a.PatlistID,
                                     convert(varchar(10), a.opreratorDate, 120) as Date,
                                      max(case when DateType = 1 then isnull(a.PluValue, '0') ELSE '0' end) 早餐前,
                                    max(case when DateType = 2 then isnull(a.PluValue, '0') ELSE '0' end) 早餐后,
                                    max(case when DateType = 3 then isnull(a.PluValue, '0') ELSE '0' end) 午餐前,
                                    max(case when DateType = 4 then isnull(a.PluValue, '0') ELSE '0' end) 午餐后,
                                    max(case when DateType = 5 then isnull(a.PluValue, '0') ELSE '0' end) 晚餐前,
                                    max(case when DateType = 6 then isnull(a.PluValue, '0') ELSE '0' end) 晚餐后,
                                    max(case when DateType = 7 then isnull(a.PluValue, '0') ELSE '0' end) 睡前,
                                    max(case when DateType = 8 then isnull(a.PluValue, '0') ELSE '0' end) 夜间
                              from IP_PluRecord a where a.patlistid=" + patlistid + @" and a.DelFlag=0 and a.workid=" + oleDb.WorkId + @"
                              GROUP BY a.PatlistID, a.OpreratorDate, a.DateType
                              ) tab1
                              GROUP BY tab1.PatlistID,tab1.Date
                                UNION ALL
                            
                              SELECT PatlistID, Date, '饮食' as  QueryName,'' 早餐前,'' 早餐后,'' 午餐前,'' 午餐后,'' 晚餐前,'' 晚餐后,'' 睡前,'' 夜间

                              FROM(
                              SELECT DISTINCT max(a.BloodTimes) BloodTimes, a.PatlistID, convert(varchar(10), a.opreratorDate, 120) as Date
                              from IP_PluRecord a where patlistid=" + patlistid + @" and a.DelFlag=0 and workid=" + oleDb.WorkId + @"
                              GROUP BY a.PatlistID,convert(varchar(10), a.opreratorDate, 120)
                              ) tab1)T order by T.Date";             
            dtBloodGlu = oleDb.GetDataTable(strsql);
            return dtBloodGlu;
        }
    }
}
