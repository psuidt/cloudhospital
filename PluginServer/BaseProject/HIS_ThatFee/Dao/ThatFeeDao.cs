using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_Entity.MIManage.Common;

namespace HIS_ThatFee.Dao
{
    /// <summary>
    /// 医技确费
    /// </summary>
    public class ThatFeeDao : AbstractDao, IThatFeeDao
    {
        #region 医技确费
        /// <summary>
        /// 获取医技确费网格数据
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="systemType">0门诊1住院</param>
        /// <returns>医技确费数据</returns>
        public DataTable GetThatFee(StringBuilder strWhere, int systemType)
        {
            string strSql = string.Empty;
            if (systemType == 0)
            {
                strSql = @"SELECT 0 AS ck,a.ApplyHeadID,c.PatName,dbo.fnGetDeptName(b.ExecuteDeptID) AS ExcuteName,dbo.fnGetEmpName(b.ApplyDoctorID) as RegDocName,dbo.fnGetDeptName(b.ApplyDeptID) as RegDeptName,c.VisitNO,a.ItemName,a.ItemID,a.Price,a.Amount,b.ApplyDate,
              CASE WHEN (a.ApplyStatus=2) THEN e.ConfirDoctorName ELSE '' END AS ConfirDoctorName,
              CASE WHEN (a.ApplyStatus=2) THEN e.ConfirDate ELSE '' END AS ConfirDate,
              e.CancelFlag,a.PresDetailID,a.ApplyStatus,CASE WHEN (a.ApplyStatus=2) THEN '已确费' ELSE '未确费' END AS Status FROM EXA_MedicalApplyDetail a 
              LEFT JOIN EXA_MedicalApplyHead b ON a.ApplyHeadID=b.ApplyHeadID
              LEFT JOIN OP_PatList c ON b.PatListID=c.PatListID 
              LEFT JOIN EXA_MedicalConfir e ON a.PresDetailID=e.PresDetailID WHERE (e.IsCancel!=1 OR e.IsCancel IS NULL) AND a.WorkID={0}";
            }
            else
            {
                strSql = @"SELECT 0 AS ck,a.ApplyHeadID,c.PatName,dbo.fnGetDeptName(b.ExecuteDeptID) AS ExcuteName,dbo.fnGetEmpName(b.ApplyDoctorID) as RegDocName, dbo.fnGetDeptName(b.ApplyDeptID) as RegDeptName,c.SerialNumber as VisitNO,a.ItemName,a.ItemID,a.Price,a.Amount,b.ApplyDate,
              CASE WHEN (a.ApplyStatus=2) THEN e.ConfirDoctorName ELSE '' END AS ConfirDoctorName,
              CASE WHEN (a.ApplyStatus=2) THEN e.ConfirDate ELSE '' END AS ConfirDate,
              e.CancelFlag,a.PresDetailID,a.ApplyStatus,CASE WHEN (a.ApplyStatus=2) THEN '已确费' ELSE '未确费' END AS Status FROM EXA_MedicalApplyDetail a 
              LEFT JOIN EXA_MedicalApplyHead b ON a.ApplyHeadID=b.ApplyHeadID 
              LEFT JOIN IP_PatList c ON b.PatListID=c.PatListID 
              LEFT JOIN BaseDept d ON b.ExecuteDeptID=d.DeptId
              LEFT JOIN EXA_MedicalConfir e ON a.PresDetailID=e.PresDetailID WHERE (e.IsCancel!=1 OR e.IsCancel IS NULL) AND a.WorkID={0}";
            }

            strSql += strWhere;
            return oleDb.GetDataTable(string.Format(strSql, oleDb.WorkId));
        }

        /// <summary>
        /// 获取执行科室
        /// </summary>
        /// <returns>执行科室数据</returns>
        public DataTable GetDept()
        {
            string strSql = "SELECT DeptId,Name FROM BaseDept WHERE DeptId IN (SELECT ExecDeptID FROM Basic_ExamType WHERE WorkId={0} GROUP BY ExecDeptID)";
            return oleDb.GetDataTable(string.Format(strSql, oleDb.WorkId));
        }

        /// <summary>
        /// 获取门诊费用明细
        /// </summary>
        /// <param name="presId">明细ID</param>
        /// <returns>门诊费用明细数据</returns>
        public DataTable GetOPFee(int presId)
        {
            string strSql = " SELECT b.* FROM OP_FeeItemHead a ,OP_FeeItemDetail b  WHERE b.DocPresDetailID={0} and a.FeeItemHeadID=b.FeeItemHeadID and a.ChargeFlag=1 and a.ChargeStatus=0";
            return oleDb.GetDataTable(string.Format(strSql, presId));
        }

        /// <summary>
        /// 获取住院费用明细
        /// </summary>
        /// <param name="presId">明细ID</param>
        /// <returns>住院费用明细数据</returns>
        public DataTable GetIPFee(int presId)
        {
            string strSql = "SELECT * FROM IP_FeeItemGenerate WHERE OrderID={0} AND FeeSource=1";
            return oleDb.GetDataTable(string.Format(strSql, presId));
        }

        /// <summary>
        /// 获取医技信息
        /// </summary>
        /// <param name="presIds">多个明细ID字符串</param>
        /// <returns>医技信息</returns>
        public DataTable ConfigInfo(string presIds)
        {
            string strSql = @"SELECT a.ApplyDetailID,b.ApplyType,b.ExamTypeID,a.TotalFee,a.ItemID,a.ItemName,b.MemberID,a.ApplyStatus,
                                        b.PatListID,a.PresDetailID,b.ExecuteDeptID,c.Name AS DeptName,a.IsReturns 
                                        FROM dbo.EXA_MedicalApplyDetail a 
                                        LEFT JOIN dbo.EXA_MedicalApplyHead b ON a.ApplyHeadID=b.ApplyHeadID 
                                        LEFT JOIN dbo.BaseDept c ON b.ExecuteDeptID=c.DeptId
                                        WHERE a.PresDetailID in ({0})";
            return oleDb.GetDataTable(string.Format(strSql, presIds));
        }

        /// <summary>
        /// 获取医技确费明细表
        /// </summary>
        /// <param name="presIds">明细ID</param>
        /// <returns>医技确费实体</returns>
        public EXA_MedicalConfir GetConfir(int presIds)
        {
            string strSql = @"SELECT * FROM dbo.EXA_MedicalConfir WHERE PresDetailID={0} AND IsCancel=0";
            return oleDb.Query<EXA_MedicalConfir>(string.Format(strSql, presIds), string.Empty).FirstOrDefault();
        }

        /// <summary>
        /// 修改医技作废状态
        /// </summary>
        /// <param name="presIds">明细ID</param>
        public void UpdateConfir(int presIds)
        {
            string strSql = @"UPDATE EXA_MedicalConfir SET IsCancel=1 WHERE PresDetailID={0}";
            oleDb.DoCommand(strSql);
        }

        /// <summary>
        /// 获取医技确费明细列表
        /// </summary>
        /// <param name="ids">确费明细ID</param>
        /// <returns>医技确费明细列表</returns>
        public List<EXA_MedicalConfir> GetConfirList(string ids)
        {
            string strSql = @"SELECT * FROM dbo.EXA_MedicalConfir WHERE PresDetailID in (" + ids + ")";
            return oleDb.Query<EXA_MedicalConfir>(strSql, string.Empty).ToList();
        }

        /// <summary>
        /// 根据医技确费头表ID获取明细
        /// </summary>
        /// <param name="confirId">确费id</param>
        /// <returns>却覅明细列表</returns>
        public List<EXA_MedicalConfirDetail> GetConfirDetailList(int confirId)
        {
            string strSql = @"SELECT * FROM dbo.EXA_MedicalConfirDetail WHERE ConfirID={0}";
            return oleDb.Query<EXA_MedicalConfirDetail>(string.Format(strSql, confirId), string.Empty).ToList();
        }

        /// <summary>
        /// 修改医技确费状态
        /// </summary>
        /// <param name="presIds">明细ID</param>
        /// <param name="applyStatus">申请状态</param>
        /// <param name="distributeFlag">确费标识</param>
        /// <param name="systemType">0门诊1住院</param>
        public void UpdateStatus(int presIds, int applyStatus, int distributeFlag, int systemType)
        {
            string strSql = @"UPDATE EXA_MedicalApplyDetail SET ApplyStatus={1} WHERE PresDetailID={0}";
            oleDb.DoCommand(string.Format(strSql, presIds, applyStatus));
            strSql = @"UPDATE OP_FeeItemHead SET DistributeFlag={1} WHERE FeeItemHeadID in (SELECT FeeItemHeadID FROM dbo.OP_FeeItemDetail WHERE DocPresDetailID={0})";
            if (systemType == 1)
            {
                strSql = @"UPDATE IP_FeeItemRecord SET DrugFlag={1} WHERE OrderID={0}";
            }

            oleDb.DoCommand(string.Format(strSql, presIds, distributeFlag));
        }

        /// <summary>
        /// 获取是否申请退款
        /// </summary>
        /// <param name="presId">处方ID</param>
        /// <returns>申请退款数据</returns>
        public DataTable GetPayFlag(int presId)
        {
            string strSql = @"SELECT c.RefundPayFlag FROM OP_FeeItemDetail a 
                                        LEFT JOIN OP_FeeRefundDetail b ON a.PresDetailID=b.FeeItemDetailID 
                                        LEFT JOIN dbo.OP_FeeRefundHead c ON b.ReHeadID=c.ReHeadID 
                                        WHERE a.DocPresDetailID={0}";
            return oleDb.GetDataTable(string.Format(strSql, presId));
        }

        #endregion

        #region 医技确费工作量统计
        /// <summary>
        /// 获取医技确费工作量统计
        /// </summary>
        /// <param name="confirDeptID">医技科室</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="type">0门诊确费1住院确费</param>
        /// <returns>医技确费工作量统计数据</returns>
        public DataTable GetThatFeeCount(string confirDeptID, string beginDate, string endDate, int type)
        {
            string strSql = "SELECT ItemID,ItemName,COUNT(ItemName) AS Count FROM dbo.EXA_MedicalConfir WHERE IsCancel=0 AND CancelFlag=1 AND MarkFlag={0} AND WorkID={1}";
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" AND ConfirDate between '{0}' and '{1}'", beginDate, endDate);
            if (!string.IsNullOrEmpty(confirDeptID))
            {
                strWhere.AppendFormat(" AND ConfirDeptID={0}", confirDeptID);
            }

            strSql += strWhere + " GROUP BY ItemID,ItemName";
            return oleDb.GetDataTable(string.Format(strSql, type, oleDb.WorkId));
        }

        /// <summary>
        /// 根据医技项目获取医技总金额
        /// </summary>
        /// <param name="id">项目id</param>
        /// <param name="type">门诊确费=0,住院确费=1</param>
        /// <returns>总额</returns>
        public string GetThatFeeTotal(string id, int type)
        {
            string totalFee = string.Empty;
            string strSql = @" SELECT SUM(TotalFee) AS TotalFee FROM dbo.EXA_MedicalConfirDetail WHERE ExamItemID={0} AND MarkFlag={1}";
            DataTable dt = oleDb.GetDataTable(string.Format(strSql, id, type));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    totalFee = (dt.Rows[0]["TotalFee"] == null || dt.Rows[0]["TotalFee"].ToString() == string.Empty) ? "0.00" : Convert.ToString(dt.Rows[0]["TotalFee"]);
                }
                else
                {
                    totalFee = "0.00";
                }
            }
            else
            {
                totalFee = "0.00";
            }

            return totalFee;
        }

        #endregion

        #region 医技明细查询
        /// <summary>
        /// 根据执行科室ID获取医技项目
        /// </summary>
        /// <param name="deptId">执行科室id</param>
        /// <returns>医技项目数据</returns>
        public DataTable GetExamItem(string deptId)
        {
            string strWhere = string.Empty;
            string strSql = @"SELECT a.ExamItemID,a.ExamItemName,a.PYCode,a.WBCode FROM dbo.Basic_ExamItem a LEFT JOIN dbo.Basic_ExamType b ON a.ExamTypeID=b.ExamTypeID WHERE a.WorkID={0}";
            if (Convert.ToInt32(deptId) > 0)
            {
                strWhere = string.Format(" AND b.ExecDeptID={0}", deptId);
            }

            strSql += strWhere;
            return oleDb.GetDataTable(string.Format(strSql, oleDb.WorkId));
        }

        /// <summary>
        /// 获取医技确费网格数据
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="systemType">0门诊1住院</param>
        /// <returns>医技确费数据</returns>
        public DataTable GetThatFeeDetail(StringBuilder strWhere, int systemType)
        {
            string strSql = string.Empty;
            if (systemType == 0)
            {
                strSql = @"SELECT a.ApplyHeadID,c.PatName,dbo.fnGetDeptName(b.ExecuteDeptID) AS ExcuteName,dbo.fnGetEmpName(b.ApplyDoctorID) as RegDocName,dbo.fnGetDeptName(b.ApplyDeptID) as RegDeptName,c.VisitNO,a.ItemName,a.ItemID,a.Price,a.Amount,b.ApplyDate,
              e.ConfirDoctorName,e.ConfirDate,e.CancelFlag,a.PresDetailID FROM EXA_MedicalApplyDetail a 
              LEFT JOIN EXA_MedicalApplyHead b ON a.ApplyHeadID=b.ApplyHeadID
              LEFT JOIN OP_PatList c ON b.PatListID=c.PatListID 
              LEFT JOIN EXA_MedicalConfir e ON a.PresDetailID=e.PresDetailID WHERE e.IsCancel=0 AND e.CancelFlag=1 AND a.WorkID={0}";
            }
            else
            {
                strSql = @"SELECT a.ApplyHeadID,c.PatName,dbo.fnGetDeptName(b.ExecuteDeptID) AS ExcuteName,dbo.fnGetEmpName(b.ApplyDoctorID) as RegDocName, dbo.fnGetDeptName(b.ApplyDeptID) as RegDeptName,c.SerialNumber as VisitNO,a.ItemName,a.ItemID,a.Price,a.Amount,b.ApplyDate,
              e.ConfirDoctorName,e.ConfirDate,e.CancelFlag,a.PresDetailID FROM EXA_MedicalApplyDetail a 
              LEFT JOIN EXA_MedicalApplyHead b ON a.ApplyHeadID=b.ApplyHeadID 
              LEFT JOIN IP_PatList c ON b.PatListID=c.PatListID 
              LEFT JOIN EXA_MedicalConfir e ON a.PresDetailID=e.PresDetailID WHERE e.IsCancel=0 AND e.CancelFlag=1 AND a.WorkID={0}";
            }

            strSql += strWhere;
            return oleDb.GetDataTable(string.Format(strSql, oleDb.WorkId));
        }
        #endregion
    }
}
