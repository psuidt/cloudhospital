using System;
using System.Data;
using System.Data.Common;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.IPManage;

namespace HIS_IPManage.Dao
{
    /// <summary>
    /// 缴款查询
    /// </summary>
    public class SqlSinglePaymentManageDao : AbstractDao, ISinglePaymentManageDao
    {
        /// <summary>
        /// 获取时间段内的交款记录
        /// </summary>
        /// <param name="iWorkId">机构ID</param>
        /// <param name="sBDate">交款开始时间</param>
        /// <param name="sEDate">交款结束时间</param>
        /// <param name="iEmpId">操作员ID -1未所有</param>
        /// <returns>交款记录</returns>
        public DataTable GetAccountListUploaded(int iWorkId, string sBDate,string sEDate,int iEmpId)
        {
            string sql = @"SELECT ipa.AccountEmpID EmpID, ISNULL(dbo.fnGetEmpName(ipa.AccountEmpID),'无名') NAME , ipa.AccountID,ipa.AccountDate,(Case ipa.AccountType when 0 then '预交金' else '结算' end) AccountType,ReceivFlag
                            FROM dbo.IP_Account ipa 
                            WHERE ipa.WorkID={0}
                            AND ipa.AccountDate>='{1}' AND ipa.AccountDate<='{2}'
	                        AND (ipa.AccountEmpID={3} OR -1={3})
	                        ORDER BY ipa.AccountEmpID ,ipa.AccountDate DESC";
            sql = string.Format(sql, iWorkId, sBDate, sEDate, iEmpId);
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 获取所有未缴款的结算操作员
        /// </summary>
        /// <param name="iWorkId">机构ID</param>
        /// <param name="iEmpId">操作员ID -1未所有</param>
        /// <returns>操作员信息</returns>
        public DataTable GetAccountListUnUpload(int iWorkId,  int iEmpId)
        {
            string sql = @"SELECT ipc.CostEmpID EmpID,ISNULL(dbo.fnGetEmpName(ipc.CostEmpID),'无名') NAME ,'结算' AccountType
                            FROM dbo.IP_CostHead ipc 
                            WHERE ipc.AccountID=0
	                        AND ipc.WorkID={0}
                            AND (ipc.CostEmpID={1} OR -1={1})
	                        GROUP BY ipc.CostEmpID
                            union all
                           SELECT ipdl.MakerEmpID EmpID,ISNULL(dbo.fnGetEmpName(ipdl.MakerEmpID),'无名') NAME ,'预交金' AccountType
                            FROM dbo.IP_DepositList ipdl 
                            WHERE ipdl.AccountID=0
	                        AND ipdl.WorkID={0}
                            AND (ipdl.MakerEmpID={1} OR -1={1})
	                        GROUP BY ipdl.MakerEmpID";
            sql = string.Format(sql, iWorkId,  iEmpId);
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 查询预交金信息
        /// </summary>
        /// <param name="iWorkId">机构ID</param>
        /// <param name="iStaffId">操作员ID</param>
        /// <param name="iAccountId">缴款ID</param>
        /// <returns>预交金信息</returns>
        public DataTable GetDepositList(int iWorkId, int iStaffId, int iAccountId)
        {
            string sql = @"  SELECT ippl.SerialNumber,ippl.PatName,ipdl.Status,(CASE ipdl.Status WHEN 0 THEN '收预交金' WHEN 1 THEN  '被退' WHEN 2 THEN '退预交金' END) StatusName  ,
	                                ipdl.InvoiceNO,ipdl.MakerEmpID,dbo.fnGetEmpName(ipdl.MakerEmpID) EmpName,
	                                (SELECT a.Name FROM dbo.BaseDictContent a WHERE a.ClassId=1021 AND a.Code=ipdl.PayType) payType,
	                                 ipdl.MakerDate,ipdl.TotalFee
                                     FROM dbo.IP_DepositList ipdl 
                                     INNER JOIN dbo.IP_PatList ippl ON ipdl.PatListID=ippl.PatListID AND ippl.WorkID=ipdl.WorkID
                             WHERE ipdl.Status IN (0,1,2)
	                           AND ipdl.WorkID={0}
							   AND ipdl.MakerEmpID={1}
							   AND ipdl.AccountID={2}  ";
            sql = string.Format(sql, iWorkId, iStaffId, iAccountId);
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 根据accountid获取票据信息
        /// </summary>
        /// <param name="iWorkId">机构ID</param>
        /// <param name="iStaffId">操作员ID</param>
        /// <param name="iAccountId">缴款ID</param>
        /// <returns>票据信息</returns>
        public DataTable GetPayMentItemFPSum(int iWorkId, int iStaffId, int iAccountId)
        {
            string sql = @"  SELECT '住院结算票' invoicetype,ipc.InvoiceID, MIN(ipc.InvoiceNO) as statno, MAX(ipc.InvoiceNO) as endno,
                                MIN(ipc.InvoiceNO)+' - '+MAX(ipc.InvoiceNO) Num,
                                SUM(CASE ipc.Status WHEN 0 THEN 1 WHEN 1 THEN 1 ELSE 0 END) as invoiceAllcount,
                                (CAST(substring(MAX(ipc.InvoiceNO),patindex( '%[0-9]%',MAX(ipc.InvoiceNO)),LEN(MAX(ipc.InvoiceNO))) AS BIGINT)
                                    -CAST(substring(MIN(ipc.InvoiceNO),patindex( '%[0-9]%',MIN(ipc.InvoiceNO)),LEN(MIN(ipc.InvoiceNO))) AS BIGINT)
                                    +1-CAST(SUM(CASE ipc.Status WHEN 0 THEN 1 WHEN 1 THEN 1 ELSE 0 END) AS BIGINT)
                                ) as BadInvoiceCount,
                                SUM(CASE ipc.Status WHEN 2 THEN 1 ELSE 0 END) as refundinvoicecount,
                                ISNULL(SUM(CASE ipc.Status WHEN 2 THEN ipc.TotalFee ELSE 0 END),0)*-1 as refundFee,                                
								ISNULL(SUM(ipc.TotalFee),0) as TotalFee,
                                ISNULL(SUM(ipc.DeptositFee),0) as TotalDeptositFee,
								ISNULL(SUM(ipc.CashFee),0) AS TotalCashFee,
                                ISNULL(SUM(CASE ipc.CostType WHEN 3 THEN ipc.CashFee ELSE 0 END),0) as TotalowFee,
								ISNULL(SUM(ipc.PosFee),0) AS TotalPosFee,
								ISNULL(SUM(ipc.PromFee),0) AS TotalPromFee,
								ISNULL(SUM(ipc.RoundingFee),0) AS TotalRoundingFee
                            FROM dbo.IP_CostHead ipc 
                            WHERE ipc.WorkID={0}
	                        AND ipc.CostEmpID = {1}
                            AND ipc.AccountID = {2}
	                        GROUP BY ipc.InvoiceID";
            sql = string.Format(sql, iWorkId, iStaffId, iAccountId);
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 获取项目分类数据
        /// </summary>
        /// <param name="iWorkId">机构ID</param>
        /// <param name="iStaffId">操作员ID</param>
        /// <param name="iAccountId">缴款ID</param>
        /// <returns>项目分类数据</returns>
        public DataTable GetPayMentItemFPClass(int iWorkId, int iStaffId, int iAccountId)
        {
            string sql = @"  SELECT tab.subname as fpItemName,sum(ipcd.totalFee) as ItemFee
							   FROM dbo.IP_CostHead ipch
							   INNER JOIN dbo.IP_CostDetail ipcd ON ipch.CostHeadID=ipcd.CostHeadID AND ipcd.WorkID=ipch.WorkID
							   LEFT JOIN (SELECT bsi.StatID,bsi.WorkID,bsis.subname FROM dbo.Basic_StatItem bsi LEFT JOIN dbo.Basic_StatItemSubclass bsis ON bsis.SubID=bsi.InFpItemID AND bsis.WorkID=bsi.WorkID) tab ON ipcd.StatID=tab.StatID AND ipcd.WorkID=tab.WorkID
							   WHERE ipch.Status IN (0,1,2)
	                           AND ipch.WorkID={0}
							   AND ipch.CostEmpID={1}
							   AND ipch.AccountID={2}
							   GROUP BY tab.subname ";
            sql = string.Format(sql, iWorkId, iStaffId, iAccountId);
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 根据accountid获取支付方式信息
        /// </summary>
        /// <param name="iWorkId">机构ID</param>
        /// <param name="iStaffId">操作员ID</param>
        /// <param name="iAccountId">缴款ID</param>
        /// <returns>支付方式信息</returns>
        public DataTable GetPayMentItemAccountClass(int iWorkId, int iStaffId, int iAccountId)
        {
            string sql = string.Empty;
            if (iAccountId > 0)
            {
                sql = @"  SELECT PaymentID,PayMentName PayName,paymentmoney,paymentcount 
                            FROM dbo.IP_AccountPatMentInfo
							where  workid={0} and accountid ={2} ";
            }
            else
            {
                sql = @"  select b.PaymentID,b.PayName,sum(b.CostMoney) as paymentmoney,count(b.CostPaymentID) as paymentcount
                                     from dbo.IP_costhead a
                                     left join dbo.IP_CostPayment b on a.CostHeadID = b.CostHeadID
                                    and a.accountid = b.accountid
									where a.Status in(0, 1, 2) 
                                    and a.workid={0} and a.CostEmpID={1} and a.accountid ={2}  
                                    group by b.PaymentID,b.PayName HAVING count(b.CostPaymentID)>0";
            }

            sql = string.Format(sql, iWorkId, iStaffId, iAccountId);
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 执行缴款
        /// </summary>
        /// <param name="iWorkId">机构Id</param>
        /// <param name="iStaffId">操作员ID</param>
        /// <param name="iAccountType">结算类型0预交金1结算</param>
        /// <param name="dTotalFee">此次缴款总额</param>
        /// <param name="dTotalPaymentFee">实际缴款现金总额</param>
        /// <param name="sErrcode">错误代码</param>
        /// <param name="sErrmsg">错误消息</param>
        /// <returns>缴款信息</returns>
        public IP_Account AccountPayment(int iWorkId, int iStaffId, int iAccountType,decimal dTotalFee,decimal dTotalPaymentFee,out int sErrcode, out string sErrmsg)
        {
            IDbCommand cmd = oleDb.GetProcCommand("SP_IP_AccountPayment");
            oleDb.AddInParameter(cmd as DbCommand, "@WorkID", DbType.Int32, iWorkId);
            oleDb.AddInParameter(cmd as DbCommand, "@StaffId", DbType.Int32, iStaffId);
            oleDb.AddInParameter(cmd as DbCommand, "@AccountType", DbType.Int32, iAccountType);
            oleDb.AddInParameter(cmd as DbCommand, "@TotalFee", DbType.Decimal, dTotalFee);
            oleDb.AddInParameter(cmd as DbCommand, "@TotalPaymentFee", DbType.Decimal, dTotalPaymentFee);
            oleDb.AddOutParameter(cmd as DbCommand, "@ErrCode", DbType.Int32, 5);
            oleDb.AddOutParameter(cmd as DbCommand, "@ErrMsg", DbType.AnsiString, 200);
            object o =oleDb.GetDataResult(cmd);

            sErrmsg = oleDb.GetParameterValue(cmd as DbCommand, "@ErrMsg").ToString();
            sErrcode = Convert.ToInt32(oleDb.GetParameterValue(cmd as DbCommand, "@ErrCode"));

            if (sErrcode == 0)
            {
                int iAccountID = Convert.ToInt32(o);
                IP_Account account = GetAccount(iAccountID);
                return account;
            }
            else
            {
                return null;
            }           
        }

        /// <summary>
        /// 获取票据明细
        /// </summary>
        /// <param name="iWorkId">机构ID</param>
        /// <param name="invoiceID">发票卷号</param>
        /// <param name="invoiceType">类型0-收费 1-退费</param>
        /// <param name="accountid">缴款id</param>
        /// <returns>票据明细</returns>
        public DataTable GetInvoiceDetail(int iWorkId,int invoiceID, int invoiceType, int accountid)
        {
            string strsql = string.Empty;
            if (invoiceType == 0)
            {
                strsql = @" SELECT PatName,CostDate,InvoiceNO,TotalFee,DeptositFee,CashFee,PosFee,PromFee,RoundingFee                              
                            FROM dbo.IP_CostHead 
                            where WorkID="+ iWorkId + @" AND AccountID=" + accountid + @" and InvoiceID=" + invoiceID + @" and Status in(0,1) order by CostDate desc";
            }
            else
            {
                strsql = @"SELECT PatName,CostDate,InvoiceNO,TotalFee,DeptositFee,CashFee,PosFee,PromFee,RoundingFee
                           FROM dbo.IP_CostHead
                           where WorkID=" + iWorkId + @" AND AccountID=" + accountid + @" and InvoiceID=" + invoiceID + @" and Status =2 order by CostDate desc";
            }

            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取缴款实体
        /// </summary>
        /// <param name="iAccountID">缴款ID</param>
        /// <returns>缴款实体</returns>
        public IP_Account GetAccount(int iAccountID)
        {
            return NewObject<IP_Account>().getmodel(iAccountID) as IP_Account;
        }
    }
}
