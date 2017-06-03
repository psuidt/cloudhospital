using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_IPManage.Dao
{
    /// <summary>
    /// 住院病人结算查询
    /// </summary>
    public class IPCostSearchDao : AbstractDao, IIPCostSearchDao
    {
        /// <summary>
        /// 按支付方式统计
        /// </summary>
        /// <param name="paramsDic">检索条件</param>
        /// <returns>统计结果</returns>
        public DataTable GetCostData(Dictionary<string, object> paramsDic)
        {
            string strSql = @"SELECT  C.MedicareCard ,
                            A.CostDate ,
                            A.CostHeadID,
                            CASE A.CostType
                              WHEN '1' THEN '中途结算'
                              WHEN 2 THEN '出院结算'
                              WHEN 3 THEN '欠费结算'
                            END AS CostTypeName ,
                            A.InvoiceNO ,
                            dbo.fnGetEmpName(A.CostEmpID) AS DoctName ,
                            C.SerialNumber ,
                            C.PatName ,
                            dbo.fnGetPatTypeName(A.PatTypeID) AS PatTypeName ,
                            dbo.fnGetEmpName(C.CurrDoctorID) AS DoctorName ,
                            C.EnterHDate ,
                            C.LeaveHDate ,
		                    CASE A.Status
                              WHEN 0 THEN '正常'
                              WHEN 1 THEN '被退'
                              WHEN 2 THEN '红冲'
                            END AS StatusName,
                            dbo.GetHospitalizationDays (A.PatListID) AS HospitalizationDays,
                            A.TotalFee ,
		                    A.DeptositFee,
		                    B.PayName,
		                    B.CostMoney
                            FROM    IP_CostHead A
                                    LEFT JOIN dbo.IP_CostPayment B ON B.CostHeadID = A.CostHeadID
                                    LEFT JOIN dbo.IP_PatList C ON A.PatListID = C.PatListID
                            WHERE   {0}
                            ORDER BY A.CostHeadID";
            strSql = string.Format(strSql, SetWhere(paramsDic));
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 按项目分类统计
        /// </summary>
        /// <param name="paramsDic">统计条件</param>
        /// <returns>统计结果</returns>
        public DataTable GetCostDataGroupItem(Dictionary<string, object> paramsDic)
        {
            string strSql = @"SELECT  C.MedicareCard ,
                            A.CostDate ,
                            A.CostHeadID,
                            CASE A.CostType
                              WHEN '1' THEN '中途结算'
                              WHEN 2 THEN '出院结算'
                              WHEN 3 THEN '欠费结算'
                            END AS CostTypeName ,
                            A.InvoiceNO ,
                            dbo.fnGetEmpName(A.CostEmpID) AS DoctName ,
                            C.SerialNumber ,
                            C.PatName ,
                            dbo.fnGetPatTypeName(A.PatTypeID) AS PatTypeName ,
                            dbo.fnGetEmpName(C.CurrDoctorID) AS DoctorName ,
                            C.EnterHDate ,
                            C.LeaveHDate ,
		                    CASE A.Status
                              WHEN 0 THEN '正常'
                              WHEN 1 THEN '被退'
                              WHEN 2 THEN '红冲'
                            END AS StatusName,
                            dbo.GetHospitalizationDays (A.PatListID) AS HospitalizationDays,
                            A.TotalFee ,
		                    A.DeptositFee,
		                    E.SubName FpItemName,
		                    B.TotalFee ItemFee
                            FROM    IP_CostHead A
                                    LEFT JOIN dbo.IP_CostDetail B ON A.CostHeadID = B.CostHeadID
                                    LEFT JOIN dbo.IP_PatList C ON A.PatListID = C.PatListID
                            		LEFT JOIN Basic_StatItem D ON B.StatID=D.StatID AND D.WorkID={0}
                            		LEFT JOIN Basic_StatItemSubclass E ON D.InFpItemID = E.SubID
                            WHERE   {1}
                            ORDER BY A.CostHeadID";
            strSql = string.Format(strSql, paramsDic["WorkID"], SetWhere(paramsDic));
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 查询结算明细记录
        /// </summary>
        /// <param name="costHeadID">结算ID</param>
        /// <returns>结算明细记录</returns>
        public DataTable GetCostDetail(int costHeadID)
        {
            IDbCommand cmd = oleDb.GetProcCommand("IP_GetFeeDetails");
            oleDb.AddInParameter(cmd as DbCommand, "@CostHeadID", DbType.Int32, costHeadID);
            oleDb.DoCommand(cmd);
            return oleDb.GetDataTable(cmd);
        }

        /// <summary>
        /// 拼接查询条件
        /// </summary>
        /// <param name="paramsDic">查询条件字典</param>
        /// <returns>查询条件</returns>
        private string SetWhere(Dictionary<string, object> paramsDic)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" A.WorkID = {0} ", paramsDic["WorkID"]);
            // 结算时间
            if ((bool)paramsDic["ChargeData"] == true)
            {
                strWhere.AppendFormat(" AND A.CostDate BETWEEN '{0}' AND '{1}' ", paramsDic["Bdate"], paramsDic["Edate"]);
            }

            // 缴款时间
            if ((bool)paramsDic["AccountData"] == true)
            {
                strWhere.AppendFormat(" AND A.AccountID IN (SELECT AccountID FROM IP_Account WHERE AccountDate BETWEEN '{0}' AND '{1}' ) ", paramsDic["Bdate"], paramsDic["Edate"]);
            }

            // 收费员ID
            strWhere.AppendFormat("AND (A.CostEmpID = {0} OR (-1)={0}) ", paramsDic["ChargerEmpID"]);
            // 就诊科室ID
            strWhere.AppendFormat("AND (C.CurrDeptID = {0} OR (-1)={0}) ", paramsDic["PresDeptID"]);
            // 病人类型ID
            strWhere.AppendFormat("AND (A.PatTypeID = {0} OR (-1)={0}) ", paramsDic["PayTypeID"]);
            // 就诊医生ID
            strWhere.AppendFormat("AND (C.CurrDoctorID = {0} OR (-1)={0}) ", paramsDic["PrsEmpID"]);
            // 记录状态
            strWhere.AppendFormat(" AND A.Status IN({0}) ",paramsDic["Satus"]);
            // 结算类型
            strWhere.AppendFormat(" AND A.CostType = {0} ", paramsDic["CostType"]);
            // 检索条件
            if (!string.IsNullOrEmpty(paramsDic["QueryCondition"].ToString()))
            {
                strWhere.AppendFormat(" AND ( C.SerialNumber LIKE '%{0}%' OR A.PatName LIKE '%{0}%' OR A.InvoiceNO LIKE '%{0}%' )", paramsDic["QueryCondition"]);
            }

            // 发票号
            if (!string.IsNullOrEmpty(paramsDic["BeInvoiceNO"].ToString()))
            {
                strWhere.AppendFormat(" AND A.InvoiceNO >= '{0}' ", paramsDic["BeInvoiceNO"]);
            }

            if (!string.IsNullOrEmpty(paramsDic["EndInvoiceNO"].ToString()))
            {
                strWhere.AppendFormat(" AND A.InvoiceNO <= '{0}' ", paramsDic["EndInvoiceNO"]);
            }

            return strWhere.ToString();
        }
    }
}
