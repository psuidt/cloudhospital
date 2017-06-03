using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_IPNurse.Dao
{
    /// <summary>
    /// 打印执行单
    /// </summary>
    public class SqlExamListDao : AbstractDao, IExamListDao
    {
        /// <summary>
        /// 获取申请单数据
        /// </summary>
        /// <param name="iDeptId">科室ID</param>
        /// <param name="iApplyType">申请类型</param>
        /// <param name="dApplyDate">申请日期</param>
        /// <param name="iOrderCategory">长临嘱 0-长嘱 1-临嘱</param>
        /// <param name="iState">打印状态 0-未打印 1-已打印 -1-全部</param>
        /// <returns>申请单数据</returns>
        public DataTable GetExamList(int iDeptId,int iApplyType, DateTime dApplyDate, int iOrderCategory, int iState)
        {
            string strSql = @"SELECT 1 checked, d.BedNo,d.PatName,d.CaseNumber,d.SerialNumber,d.Age,(CASE d.Sex when 1 then '男' when 2 then '女' else '未知' end) Sex,d.EnterDiseaseName,
		                            (CASE a.ApplyType WHEN 1 THEN '化验' WHEN 0 THEN '检查' ELSE '治疗' END) ApplyType,
		                            b.ItemName,b.TotalFee,
		                            f.Name as ApplyDeptDoctor,e.Name as ApplyDeptName,dbo.fnGetDeptName(a.ExecuteDeptID) ExecuteDeptName,
		                            a.ApplyDate,a.ApplyContent,a.ApplyHeadID
                            FROM dbo.EXA_MedicalApplyHead a 
                            INNER JOIN dbo.EXA_MedicalApplyDetail b ON a.ApplyHeadID=b.ApplyHeadID
                            INNER JOIN dbo.IPD_OrderRecord c ON b.PresDetailID=c.OrderID
                            INNER JOIN dbo.IP_PatList d ON c.PatListID=d.PatListID AND d.Status IN(1,2)
                            INNER JOIN BaseDept e ON a.ApplyDeptID=e.DeptId
                            INNER JOIN BaseEmployee f ON a.ApplyDoctorID=f.EmpId
                            WHERE a.SystemType=1 
                              and b.ApplyStatus>0
                              and a.ApplyDeptID={0}
                              and a.ApplyDate>='{1}'
                              and a.ApplyDate<='{2}'
                              and (c.OrderCategory = {3} or {3}=-1 )
                              and a.ApplyType={4}
                              and a.SystemType=1";
            if (iState == 0)
            {
                strSql += " and b.Printer=0 ";
            }
            else if (iState == 1)
            {
                strSql += " and b.Printer>0 ";
            }

            return oleDb.GetDataTable(string.Format(strSql, iDeptId, dApplyDate.ToString("yyyy-MM-dd 00:00:00"), dApplyDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00"),iOrderCategory, iApplyType));
        }

        /// <summary>
        /// 更新打印状态
        /// </summary>
        /// <param name="iApplyHeadIDList">申请单ID集合</param>
        /// <param name="iEmpId">操作员ID</param>
        /// <returns>true：更新成功</returns>
        public bool UpdateApplyPrint(List<int> iApplyHeadIDList,int iEmpId)
        {
            string sApplyId = string.Join(",", iApplyHeadIDList.ToArray());
            string sqlUp = @"update dbo.EXA_MedicalApplyDetail
                                set Printer={1}
                              where ApplyHeadID in ({0})";
            sqlUp = string.Format(sqlUp, sApplyId, iEmpId);
            return oleDb.DoCommand(sqlUp)>0;
        }
    }
}
