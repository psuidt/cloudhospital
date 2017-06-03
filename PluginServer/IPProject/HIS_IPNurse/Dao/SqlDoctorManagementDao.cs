using System;
using System.Data;
using System.Text;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_IPNurse.Dao
{
    /// <summary>
    /// 护士站医嘱转抄SQL操作实现类
    /// </summary>
    public class SqlDoctorManagementDao : AbstractDao, IDoctorManagementDao
    {
        /// <summary>
        /// 获取费用项目列表
        /// </summary>
        /// <returns>费用项目列表</returns>
        public DataTable GetDocFeeItemList()
        {
            string strsql = @"SELECT * FROM ViewFeeItem_SimpleList WHERE ItemClass <> 1 AND WorkId={0}";
            strsql = string.Format(strsql, oleDb.WorkId);
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取未转抄医嘱病人列表
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="orderCategory">医嘱类别(长期/临时)</param>
        /// <param name="orderStatus">医嘱类型(新开/新停)</param>
        /// <param name="astFlag">皮试医嘱</param>
        /// <param name="isTrans">是否已转抄</param>
        /// <returns>病人列表</returns>
        public DataTable GetDocPatList(int deptId, string orderCategory, string orderStatus, string astFlag, bool isTrans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT  1 CheckFlg , ");
            strSql.Append(" BedNo , ");
            strSql.Append(" PatListID , ");
            strSql.Append(" PatName , ");
            strSql.Append(" SerialNumber , ");
            strSql.Append(" Status , ");
            strSql.Append(" CASE Sex ");
            strSql.Append(" WHEN 1 THEN '男' ");
            strSql.Append(" WHEN 2 THEN '女' ");
            strSql.Append(" END AS SexName ");
            strSql.Append(" FROM    dbo.IP_PatList ");
            strSql.Append(" WHERE   PatListID IN ( SELECT   PatListID ");
            strSql.Append(" FROM     IPD_OrderRecord ");
            strSql.AppendFormat(" WHERE DeleteFlag =0 AND PatDeptID = {0} ", deptId);
            strSql.AppendFormat(" AND (OrderCategory = {0} OR {0}=-1)", orderCategory); // 医嘱类别
            if (isTrans)
            {
                if (orderStatus.Equals("0"))
                {
                    strSql.Append(" AND OrderStatus in (1,3) ");
                }
                else if (orderStatus.Equals("1"))
                {
                    strSql.Append(" AND OrderStatus = 1 ");
                }
                else if (orderStatus.Equals("2"))
                {
                    strSql.Append(" AND OrderStatus = 3 ");
                }

                if (!string.IsNullOrEmpty(astFlag))
                {
                    strSql.AppendFormat(" AND AstFlag IN ({0})", astFlag);
                }
            }
            else
            {
                if (orderStatus.Equals("0"))
                {
                    strSql.Append(" AND OrderStatus in (2,4) ");
                }
                else if (orderStatus.Equals("1"))
                {
                    strSql.Append(" AND OrderStatus = 2 ");
                }
                else if (orderStatus.Equals("2"))
                {
                    strSql.Append(" AND OrderStatus = 4 ");
                }

                if (!string.IsNullOrEmpty(astFlag))
                {
                    strSql.AppendFormat(" AND AstFlag IN ({0})", astFlag);
                }
            }

            strSql.Append(" AND (ExecFlag = ( CASE OrderStatus WHEN 1 THEN 0 WHEN 2 THEN 0 ELSE -1 END )OR -1 = -1 ) ");
            strSql.Append(" ) ORDER BY PatListID ");
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 根据病人ID以及分组ID获取医嘱信息
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="groupID">组号</param>
        /// <returns>医嘱信息</returns>
        public DataTable GetOrderRecord(int patListID, int groupID)
        {
            string strSql = string.Format("SELECT TOP 1 * FROM IPD_OrderRecord WHERE PatListID ={0} AND GroupID ={1} ORDER BY GroupSerailID ASC ", patListID, groupID);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取病人医嘱关联的费用列表
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="groupID">分组ID</param>
        /// <returns>医嘱关联的费用列表</returns>
        public DataTable GetPatDocRelationFeeList(int patListID, int groupID)
        {
            string strSql = @"SELECT 0 CheckFlg ,0 UpdFlg ,B.Name AS ExecDeptName,A.*, 
                CASE A.CalCostMode WHEN 0 THEN '按频次' WHEN 1 THEN '按周期' END CalCostModeName FROM IP_FeeItemGenerate A
                LEFT JOIN BaseDept B ON A.ExecDeptDoctorID = B.DeptId
                WHERE A.PatListID = {0}
                AND A.GroupID = {1} AND A.WorkID = {2}";
            strSql = string.Format(strSql, patListID, groupID, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 验证医嘱是否已转抄，已转抄的医嘱不允许补录费用
        /// </summary>
        /// <param name="patListID">病人ID</param>
        /// <param name="groupID">组号ID</param>
        /// <returns>true：已转抄</returns>
        public DataTable CheckOrderStatus(int patListID, int groupID)
        {
            string strSql = @"SELECT * FROM IPD_OrderRecord WHERE PatListID = {0} AND GroupID = {1} AND OrderStatus IN (0,1)";
            strSql = string.Format(strSql, patListID, groupID);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 根据病人ID获取病人登记信息
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人登记信息</returns>
        public DataTable GetPatientInfo(int patListID)
        {
            string strSql = string.Format("SELECT * FROM IP_PatList WHERE PatListID = {0}", patListID);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取未转抄医嘱列表
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="orderCategory">医嘱类别(长期/临时)</param>
        /// <param name="orderStatus">医嘱类型(新开/新停)</param>
        /// <param name="astFlag">皮试医嘱</param>
        /// <param name="isTrans">是否已转抄</param>
        /// <returns>医嘱列表</returns>
        public DataTable GetPatNotCopiedDocList(int deptId, string orderCategory, string orderStatus, string astFlag, bool isTrans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT  1 CheckFlg , ");
            strSql.Append(" CASE A.OrderCategory ");
            strSql.Append(" WHEN 0 THEN '长' ");
            strSql.Append(" WHEN 1 THEN '临' ");
            strSql.Append(" END AS OrderCategory , ");
            if (isTrans)
            {
                strSql.Append(" CASE A.OrderStatus ");
                strSql.Append(" WHEN 1 THEN '开' ");
                strSql.Append(" WHEN 3 THEN '停' ");
                strSql.Append(" END AS OrderStatus, ");
            }
            else
            {
                strSql.Append(" CASE A.OrderStatus ");
                strSql.Append(" WHEN 2 THEN '开' ");
                strSql.Append(" WHEN 4 THEN '停' ");
                strSql.Append(" END AS OrderStatus, ");
            }

            strSql.Append(" B.BedNo, ");
            strSql.Append(" A.PatListID, ");
            strSql.Append(" A.GroupID, ");
            strSql.Append(" A.OrderID, ");
            strSql.Append(" B.PatName, ");
            strSql.Append(" B.SerialNumber, ");
            strSql.Append(" A.OrderBdate, ");
            strSql.Append(" C.Name AS DocName, ");
            strSql.Append(" D.Name AS NurseName, ");
            strSql.Append(" A.ItemName + CASE A.Spec ");
            strSql.Append(" WHEN '' THEN '' ");
            strSql.Append(" ELSE '【' + A.Spec + '】' ");
            strSql.Append(" END ItemName , ");
            strSql.Append(" A.Spec, ");
            strSql.Append(" A.Dosage, ");
            strSql.Append(" A.Entrust, ");
            strSql.Append(" A.DosageUnit, ");
            strSql.Append(" A.ChannelName, ");
            strSql.Append(" A.Frequency, ");
            strSql.Append(" CASE A.AstFlag WHEN 1 THEN '阴性(-)' WHEN 2 THEN '阳性(+)' END AS AstFlg, ");
            strSql.Append(" A.FirstNum, ");
            strSql.Append(" A.TeminalNum, ");
            strSql.Append(" A.EOrderDate, ");
            strSql.Append(" CASE A.OrderType WHEN 2 THEN '是' ELSE '' END AS SelfDrug, ");
            strSql.Append(" CASE A.OrderType WHEN 3 THEN '是' ELSE '' END AS BeltDrug ");
            strSql.Append(" FROM    IPD_OrderRecord A ");
            strSql.Append(" LEFT JOIN IP_PatList B ON A.PatListID=B.PatListID ");
            strSql.Append(" LEFT JOIN BaseEmployee C ON A.OrderDoc=C.EmpId ");
            strSql.Append(" LEFT JOIN BaseEmployee D ON A.TransNurse=D.EmpId ");

            strSql.AppendFormat(" WHERE A.DeleteFlag =0 AND A.PatDeptID = {0} ", deptId);
            strSql.AppendFormat(" AND (A.OrderCategory = {0} OR {0}=-1)", orderCategory); // 医嘱类别
            if (isTrans)
            {
                if (orderStatus.Equals("0"))
                {
                    strSql.Append(" AND A.OrderStatus in (1,3) ");
                }
                else if (orderStatus.Equals("1"))
                {
                    strSql.Append(" AND A.OrderStatus = 1 ");
                }
                else if (orderStatus.Equals("2"))
                {
                    strSql.Append(" AND A.OrderStatus = 3 ");
                }

                if (!string.IsNullOrEmpty(astFlag))
                {
                    strSql.AppendFormat(" AND A.AstFlag IN ({0})", astFlag);
                }
            }
            else
            {
                if (orderStatus.Equals("0"))
                {
                    strSql.Append(" AND OrderStatus IN ( 2, 4 ) ");
                }
                else if (orderStatus.Equals("1"))
                {
                    strSql.Append(" AND A.OrderStatus = 2 ");
                }
                else if (orderStatus.Equals("2"))
                {
                    strSql.Append(" AND A.OrderStatus = 4 ");
                }

                if (!string.IsNullOrEmpty(astFlag))
                {
                    strSql.AppendFormat(" AND A.AstFlag IN ({0})", astFlag);
                }
            }

            strSql.Append(" AND (A.ExecFlag = ( CASE OrderStatus WHEN 1 THEN 0 WHEN 2 THEN 0 ELSE -1 END )OR -1 = -1 ) ");
            strSql.Append(" ORDER BY A.PatListID,A.GroupID ");
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 根据医嘱Id获取医嘱皮试状态
        /// </summary>
        /// <param name="orderID">医嘱ID</param>
        /// <returns>医嘱皮试状态信息</returns>
        public DataTable GetOrderRecordAstFlag(string orderID)
        {
            string strSql = string.Format(
                @"SELECT OrderID,ItemName,PatListID,GroupID FROM IPD_OrderRecord 
                WHERE AstOrderID = 0 AND OrderID IN ({0})  AND OrderStatus IN (1,3) AND AstFlag IN (0,2)", 
                orderID);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 医嘱转抄
        /// </summary>
        /// <param name="arrayOrderID">医嘱ID列表</param>
        /// <param name="empID">转抄护士</param>
        public void UpdateDocOrder(string arrayOrderID, int empID)
        {
            string strSql = @"UPDATE  dbo.IPD_OrderRecord
                        SET     TransNurse = ( CASE OrderStatus
                         WHEN 1 THEN {0}
                         ELSE TransNurse
                       END ) ,
                        TransDate = ( CASE OrderStatus
                        WHEN 1 THEN GETDATE()
                        ELSE TransDate
                      END ) ,
                         EOrderTsNurse = ( CASE OrderStatus
                            WHEN 3 THEN {0}
                            ELSE EOrderTsNurse
                          END ) ,
                        EOrderTsDate = ( CASE OrderStatus
                           WHEN 3 THEN GETDATE()
                           ELSE TransDate
                         END ) ,
                        OrderStatus = ( CASE OrderStatus
                          WHEN 1 THEN 2
                          WHEN 3 THEN 4
                        END )
                        WHERE   OrderID IN ({1}) AND OrderStatus IN (1,3)";
            strSql = string.Format(strSql, empID, arrayOrderID);
            oleDb.DoCommand(strSql);
        }

        /// <summary>
        /// 根据病人ID以及分组ID查出整组医嘱的医嘱ID号
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="groupID">分组ID</param>
        /// <returns>整组医嘱的医嘱ID号</returns>
        public DataTable GetOrderIDList(int patListID, int groupID)
        {
            string strSql = string.Format("SELECT OrderID FROM IPD_OrderRecord WHERE PatListID={0} AND GroupID ={1}", patListID, groupID);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 取消转抄
        /// </summary>
        /// <param name="arrayOrderID">医嘱ID集合</param>
        public void CancelTransDocOrder(string arrayOrderID)
        {
            string strSql = @"UPDATE  IPD_OrderRecord
                        SET     OrderStatus = ( CASE OrderStatus
                        WHEN 2 THEN 1
                        WHEN 4 THEN 3
                        END )
                        WHERE   OrderID IN ({0})
                        AND OrderStatus IN ( 2, 4 ) AND ExecFlag = 0";
            strSql = string.Format(strSql, arrayOrderID);
            oleDb.DoCommand(strSql);
        }

        /// <summary>
        /// 检查医嘱是否已发送
        /// </summary>
        /// <param name="arrayOrderID">医嘱ID列表</param>
        /// <returns>已发送医嘱列表</returns>
        public DataTable IsCheckOrderSend(string arrayOrderID)
        {
            string strSql = @"SELECT  OrderID ,ItemName,
                            OrderCategory ,
                            OrderStatus ,
                            ExecFlag
                            FROM IPD_OrderRecord
                            WHERE OrderID IN ( {0} )
                            AND ExecFlag = 1";
            strSql = string.Format(strSql, arrayOrderID);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取医嘱病人列表
        /// </summary>
        /// <param name="deptId">入院科室ID</param>
        /// <param name="status">病人状态</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>医嘱病人列表</returns>
        public DataTable GetPatientList(int deptId, int status, DateTime startTime, DateTime endTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT PatListID, ");
            strSql.Append(" CurrDeptID, ");
            strSql.Append(" BedNo, ");
            strSql.Append(" CASE Sex ");
            strSql.Append(" WHEN 1 THEN '男' ");
            strSql.Append(" WHEN 2 THEN '女' ");
            strSql.Append(" END SexName, ");
            strSql.Append(" PatName, ");
            strSql.Append(" SerialNumber ");
            strSql.Append(" FROM IP_PatList ");
            strSql.AppendFormat(" WHERE CurrDeptID = {0} ", deptId);
            strSql.AppendFormat(" AND Status = {0} ", status);
            if (startTime != DateTime.MinValue && endTime != DateTime.MinValue)
            {
                string sTime = startTime.ToString("yyyy-MM-dd") + " 00:00:00";
                string eTime = endTime.ToString("yyyy-MM-dd") + " 23:59:00";
                strSql.AppendFormat(" AND A.MakerDate BETWEEN '{0}' AND '{1}' ", sTime, eTime);
            }

            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 获取病人已记账的长期临时账单费用以及床位费
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人已记账的长期临时账单费用以及床位费信息</returns>
        public DataTable GetPatLongOrderSumPay(int patListID)
        {
            string strSql = @"SELECT CASE WHEN SUM(TotalFee) >0 THEN SUM(TotalFee) ELSE 0 END TotalFee FROM 
                        IP_FeeItemRecord WHERE PatListID={0} AND OrderType IN (2,3) AND RecordFlag=0 AND CostHeadID=0 AND WorkID = {1}
                        UNION ALL
                        SELECT CASE WHEN SUM(TotalFee) >0 THEN SUM(TotalFee) ELSE 0 END TotalFee FROM 
                        IP_FeeItemRecord WHERE PatListID={0} AND OrderType=4 AND RecordFlag=0 AND CostHeadID=0 AND WorkID = {1}";
            strSql = string.Format(strSql, patListID, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取病人预交金累计交费金额
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人预交金累计交费金额信息</returns>
        public DataTable GetPatSumPay(int patListID)
        {
            string strSql = @"SELECT CASE WHEN SUM(TotalFee) >0 
                            THEN SUM(TotalFee) ELSE 0 END AS TotalFee 
                            FROM IP_DepositList WHERE Status = 0 AND PatListID={0} AND CostHeadID=0 AND WorkID = {1} ";
            strSql = string.Format(strSql, patListID, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取皮试数据
        /// </summary>
        /// <param name="iDeptID">科室ID</param>
        /// <param name="bIsCheckeed">是否已标注</param>
        /// <param name="sBDate">开始时间</param>
        /// <param name="sEDate">结束时间</param>
        /// <returns>皮试数据</returns>
        public DataTable QuerySkinTestData(int iDeptID, bool bIsCheckeed, string sBDate, string sEDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT A.OrderID,");
            strSql.Append(" B.BedNo, ");
            strSql.Append(" B.PatName, ");
            strSql.Append(" B.SerialNumber, ");
            strSql.Append(" CASE A.OrderCategory ");
            strSql.Append(" WHEN 0 THEN '长' ");
            strSql.Append(" WHEN 1 THEN '临' ");
            strSql.Append(" END AS OrderCategory , ");
            strSql.Append(" A.OrderBdate, ");
            strSql.Append(" A.ItemName, ");
            strSql.Append(" A.Spec, ");
            strSql.Append("dbo.fnGetDeptName(A.PatDeptID) AS DeptName, ");
            strSql.Append("dbo.fnGetEmpName(A.OrderDoc) AS DocName, ");
            strSql.Append("D.ExecDate AS ExecDate,   ");
            strSql.Append("dbo.fnGetEmpName(D.ExecEmpID) AS ExecEmp, ");
            strSql.Append(" CASE A.AstFlag WHEN 1 THEN '阴性(-)' WHEN 2 THEN '阳性(+)' ELSE '' END AS AstFlag ");
            strSql.Append(" FROM    dbo.IPD_OrderRecord A ");
            strSql.Append(" LEFT JOIN IP_PatList B ON A.PatListID=B.PatListID ");
            strSql.Append(" LEFT JOIN BaseEmployee C ON A.OrderDoc=C.EmpId ");
            strSql.Append(" LEFT JOIN IPN_OrderAstResult D ON A.OrderID=D.OrderID ");
            strSql.AppendFormat(" WHERE  A.OrderStatus=5 AND A.ExecFlag =1 AND  A.PatDeptID = {0} ", iDeptID);
            if (bIsCheckeed)
            {
                strSql.Append(" AND    A.AstFlag in (1,2) ");
            }
            else
            {
                strSql.Append(" AND    A.AstFlag = 0 ");
            }

            strSql.Append(" AND    A.AstOrderID > 0 ");
            strSql.AppendFormat(" AND    A.OrderBdate >= '{0}' ", sBDate);
            strSql.AppendFormat(" AND    A.OrderBdate <= '{0}' ", sEDate);

            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 删除医嘱关联费用数据
        /// </summary>
        /// <param name="generateID">费用生成ID</param>
        public void DelFeeItemGenerate(int generateID)
        {
            string strSql = string.Format("DELETE FROM IP_FeeItemGenerate WHERE GenerateID = {0}", generateID);
            oleDb.DoCommand(strSql);
        }
    }
}