using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_IPManage.Dao
{
    /// <summary>
    /// 缴款管理
    /// </summary>
    public class SqlAllAccountDao : AbstractDao, IAllAccountDao
    {
        #region 门诊缴款查询

        /// <summary>
        /// 查询所有已经缴款记录
        /// </summary>
        /// <param name="bdate">缴款开始时间</param>
        /// <param name="edate">缴款结束时间</param>
        /// <param name="empid">操作员ID</param>
        /// <param name="status">状态</param>
        /// <returns>缴款记录</returns>
        public DataTable GetAllAccountData(DateTime bdate, DateTime edate, int empid, int status)
        {
            DataTable dt = new DataTable();
            string strsql = string.Empty;

            strsql = @"select case when ReceivFlag=0 then 1 else 0 end as Selected,
                            ReceivBillNO,(CASE AccountType WHEN 0 THEN '预交金' ELSE '住院结算' END) AccountType,
                            AccountEmpID,
                           dbo.fnGetEmpName(AccountEmpID) as empName,
                           ReceivFlag,
                           case when ReceivFlag = 0 then '未收款' else '已收款' end as ReceivFlagName,
                           AccountID,
                           LastDate,
                           AccountDate,
                           ReceivDate,
                           dbo.fnGetEmpName(ReceivEmpID) as ReceivEmpName,
                           TotalFee
						   from ip_account 
                           where 
                           accountdate>='" + bdate.ToString("yyyy-MM-dd HH:mm:ss") + @"' and accountdate<='" + edate.ToString("yyyy-MM-dd HH:mm:ss") + @"'
                           and (ReceivFlag=" + status + @"  or " + status + @"=2)
                           and (AccountEmpID=" + empid + @" or " + empid + @"=0) 
						   and workid=" + oleDb.WorkId + " order by ReceivFlag,AccountEmpID,ReceivBillNO ";

            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取缴款支付方式表
        /// </summary>
        /// <param name="bdate">缴款开始时间</param>
        /// <param name="edate">缴款结束时间</param>
        /// <param name="empid">操作员ID</param>
        /// <returns>缴款支付方式表</returns>
        public DataTable GetAllAccountPayment(DateTime bdate, DateTime edate, int empid)
        {
            string strsql = string.Empty;

            strsql = @"select * from IP_AccountPatMentInfo 
                              where accountid in
                             (select accountid from ip_account
                             where accountdate>='" + bdate.ToString("yyyy-MM-dd HH:mm:ss") + @"' and accountdate<='" + edate.ToString("yyyy-MM-dd HH:mm:ss") + @"'
                             and (AccountEmpID=" + empid + @" or " + empid + @"=0) and workid=" + oleDb.WorkId + ")";

            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 查询所有未缴款记录
        /// </summary>
        /// <param name="bdate">结算开始时间</param>
        /// <param name="edate">结算结束时间</param>
        /// <param name="empid">操作员ID</param>
        /// <returns>所有未缴款记录</returns>
        public DataTable GetAllNotAccountData(DateTime bdate, DateTime edate, int empid)
        {
            DataTable dt = new DataTable();
            string strsql = string.Empty;
            //INNER JOIN dbo.IP_CostDetail ipcd ON ipc.CostHeadID=ipcd.CostHeadID
            strsql = @"SELECT 0 ReceivBillNO,
                                '住院结算' FeeType,ISNULL(dbo.fnGetEmpName(ipc.CostEmpID),'无名') empName ,0 ReceivFlag,0 ReceivFlagName,0 AccountID,(SELECT MAX(AccountDate) FROM dbo.IP_Account WHERE AccountEmpID=ipc.CostEmpID AND AccountType=1 AND WorkID=ipc.WorkID) LastDate,
                                null AccountDate,0 AccountFlag, 
                                SUM(ipc.TotalFee) TotalFee,SUM(ipc.DeptositFee) 冲预交金,SUM(ipc.PromFee) 优惠, SUM(ipc.RoundingFee) 凑整金额,SUM(ipc.CashFee) 现金,SUM(ipc.PosFee) Pos,0 AS 支票
                            FROM dbo.IP_CostHead ipc 
							
                            WHERE ipc.AccountID=0
	                        AND ipc.WorkID={0}
                            AND (ipc.CostEmpID={1} OR 0={1}) 
	                        GROUP BY ipc.CostEmpID,ipc.WorkID
                            union ALL
                            SELECT 0 ReceivBillNO,
                                FeeType,empName,0 ReceivFlag,0 ReceivFlagName,0 AccountID,LastDate,null AccountDate,0 AccountFlag, 
                                SUM(TotalFee) TotalFee,0 冲预交金,0 优惠,0 凑整金额,SUM(CASE Name WHEN '现金' THEN TotalFee ELSE 0 END) 现金,SUM(CASE Name WHEN 'POS' THEN TotalFee ELSE 0 END) Pos,SUM(CASE Name WHEN '支票' THEN TotalFee ELSE 0 END) 支票
                            FROM
                            (
                                SELECT ISNULL(dbo.fnGetEmpName(ipdl.MakerEmpID),'无名') empName ,'预交金' FeeType,(SELECT MAX(AccountDate) FROM dbo.IP_Account WHERE AccountEmpID=ipdl.MakerEmpID AND AccountType=2 AND WorkID=ipdl.WorkID) LastDate,
                                bdc.Name,SUM(ipdl.TotalFee) TotalFee
                                FROM dbo.IP_DepositList ipdl 
							    INNER JOIN  dbo.BaseDictContent bdc ON ipdl.PayType=bdc.Code AND ipdl.WorkID=bdc.WorkID AND bdc.DelFlag=0 AND bdc.ClassId=1021
                                WHERE ipdl.AccountID=0
	                            AND ipdl.WorkID={0}
                                AND (ipdl.MakerEmpID={1} OR 0={1})
	                            GROUP BY ipdl.MakerEmpID,ipdl.WorkID,bdc.Name
                            ) tab
                            GROUP BY empName,FeeType,LastDate
                            ";
            strsql = string.Format(strsql, oleDb.WorkId, empid);
            
            return oleDb.GetDataTable(strsql);
        }
        #endregion
    }
}
