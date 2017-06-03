using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_IPNurse.Dao
{
    /// <summary>
    /// 执行单接口实现
    /// </summary>
    public class SqlExecBillRecordDao : AbstractDao, IExecBillRecordDao
    {
        /// <summary>
        /// 获取执行单类型
        /// </summary>
        /// <returns>执行单类型</returns>
        public DataTable GetReportTypeList()
        {
            string strSql = @" SELECT  ID , BillName , ReportFile 
                                FROM  dbo.Basic_ExecuteBills 
                                where DelFlag=0 and WorkID={0}";
            strSql = string.Format(strSql, WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取执行单数据
        /// </summary>
        /// <param name="iDeptId">科室ID</param>
        /// <param name="iType">执行单类型</param>
        /// <param name="dFeeDate">费用日期</param>
        /// <param name="iOrderCategory">长临嘱 0-长嘱 1-临嘱 -1-全部</param>
        /// <param name="iState">打印状态 0-未打印 1-已打印 -1-全部</param>
        /// <returns>执行单数据</returns>
        public DataTable GetExcuteList(int iDeptId, int iType, DateTime dFeeDate, int iOrderCategory, int iState, bool typeName)
        {
            string strSql = @" SELECT  1 checked,'' OrderContents,  ipnoe.PatListID  , ippl.BedNo BedCode, (ippl.PatName+(CASE ippl.Sex WHEN 1 THEN '(男)' WHEN 2 THEN '(女)' ELSE '' END )) PatName , ippl.SerialNumber,ippl.CaseNumber,ippl.Age
	                            , (CASE ipnoe.OrderCategory WHEN 0 THEN '长' ELSE '临' END) OrderCategory
	                            , ipnoe.ExecDate  , dbo.fnGetEmpName(ipnoe.PresDoctorID) DocName 
	                            , ipdor.ItemName+'(' +ISNULL(dghmd.TradeName,'')+')'+ipnoe.Spec +' '+ipdor.Entrust OrderContent
                                , cast(ipdor.Dosage as varchar(6))+ipdor.DosageUnit Amount, ipdor.ChannelName Usage, ipdor.Frequency
                                , dbo.fnGetEmpName(ipnoe.PrintEmpID) Printer    , ipnoe.PrintDate
	                            ,	ipnoe.ID      , ipnoe.RecordID      , ipnoe.GroupID, ipdor.Entrust,ipdor.DropSpec,dbo.fnGetDeptName(ipdor.PatDeptID) DeptName
                            FROM  dbo.IPN_OrderExecBillRecord ipnoe
                        INNER JOIN dbo.IPD_OrderRecord ipdor ON ipnoe.OrderID=ipdor.OrderID AND ipnoe.WorkID=ipdor.WorkID
                        LEFT JOIN dbo.DG_HospMakerDic dghmd ON ipdor.ItemID=dghmd.Drugid
                        INNER JOIN dbo.IP_PatList ippl ON ipnoe.PatListID=ippl.PatListID AND ippl.WorkID = ipnoe.WorkID AND ippl.STATUS<3
                        INNER JOIN (SELECT  beb.WorkID,beb.ID , beb.BillName , beb.ReportFile ,bebc.ChannelID
				                    FROM  dbo.Basic_ExecuteBills beb
		                            INNER JOIN dbo.Basic_ExecuteBillChannel bebc ON beb.ID=bebc.ExecBillID AND beb.WorkID=bebc.WorkID where beb.DelFlag=0 ) tab1 ON tab1.ChannelID = ipnoe.ChannelID AND tab1.WorkID = ipnoe.WorkID
                                    INNER JOIN IP_FeeItemRecord feeitem ON ipnoe.RecordID = feeitem.FeeRecordID AND feeitem.RecordFlag = 0                            
                                    WHERE  ipnoe.WorkID={0}
                               AND ipnoe.PresDeptID = {1}
                               AND tab1.ID = {2}
                               AND ipnoe.ExecDate >= '{3}'
                               AND ipnoe.ExecDate <= '{4}'
                               AND (ipnoe.OrderCategory={5} or {5}=-1 )";
            if (iState == 0)
            {
                strSql += " AND ipnoe.PrintEmpID=0 ";
            }
            else if (iState == 1)
            {
                strSql += " AND ipnoe.PrintEmpID>0 ";
            }

            strSql += " Order by ipnoe.GroupID,ipnoe.ExecDate  ";
            //if (typeName)
            //{
            strSql = string.Format(strSql, WorkId, iDeptId, iType, dFeeDate.ToString("yyyy-MM-dd 12:01:00"), dFeeDate.AddDays(1).ToString("yyyy-MM-dd 12:00:01"), iOrderCategory);
            //}
            //else
            //{
            //    strSql = string.Format(strSql, WorkId, iDeptId, iType, dFeeDate.ToString("yyyy-MM-dd 00:00:00"), dFeeDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00"), iOrderCategory);
            //}

            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 设置执行单状态
        /// </summary>
        /// <param name="iExecIdList">需要设置的id集</param>
        /// <param name="iState">设置状态 0：未打印 1：打印 </param>
        /// <param name="iEmpId">修改人id</param>
        /// <returns>true设置成功</returns>
        public bool SetExcuteList(List<int> iExecIdList, int iState, int iEmpId)
        {
            string sIds = string.Join(",", iExecIdList.ToArray());
            string strSql = @" UPDATE dbo.IPN_OrderExecBillRecord SET PrintDate=GETDATE() ,PrintEmpID={0}  WHERE id IN({1})";
            if (iState == 0)
            {
                iEmpId = 0;
            }

            strSql = string.Format(strSql, iEmpId, sIds);
            return oleDb.DoCommand(strSql) > 0;
        }
    }
}
