using System;
using System.Data;
using System.Text;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_IPNurse.Dao
{
    /// <summary>
    /// 医嘱费用核对
    /// </summary>
    public class SqlDocOrderExpenseCheckDao : AbstractDao, IDocOrderExpenseCheckDao
    {
        /// <summary>
        /// 获取病人状态
        /// </summary>
        /// <param name="patlistID">病人ID</param>
        /// <returns>病人状态信息</returns>
        public DataTable GetPatientStatus(int patlistID)
        {
            string strSql = string.Format("SELECT Status FROM IP_PatList WHERE PatListID = {0}", patlistID);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取医嘱病人列表
        /// </summary>
        /// <param name="deptId">入院科室ID</param>
        /// <param name="status">病人状态</param>
        /// <returns>医嘱病人列表</returns>
        public DataTable GetPatientList(int deptId, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT PatListID, ");
            strSql.Append(" A.CurrDeptID, ");
            strSql.Append(" A.BedNo, ");
            strSql.Append(" CASE A.Sex ");
            strSql.Append(" WHEN 1 THEN '男' ");
            strSql.Append(" WHEN 2 THEN '女' ");
            strSql.Append(" END SexName, ");
            strSql.Append(" A.PatName, ");
            strSql.Append(" A.PatTypeID, "); // 病人类型ID
            strSql.Append(" B.PatTypeName, "); // 病人类型名
            strSql.Append(" A.SerialNumber, ");
            strSql.Append(" A.IsLeaveHosOrder ");
            strSql.Append(" FROM IP_PatList A ");
            strSql.Append(" LEFT JOIN Basic_PatType B ON A.PatTypeID = B.PatTypeID ");
            strSql.AppendFormat(" WHERE A.CurrDeptID = {0} ", deptId);
            strSql.AppendFormat(" AND A.Status = {0} ", status);
            strSql.AppendFormat(" AND A.WorkID = {0} ", oleDb.WorkId);
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 获取病人已记账的长期临时账单费用以及床位费
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人已记账的长期临时账单费用以及床位费</returns>
        public DataTable GetPatLongOrderSumPay(int patListID)
        {
            string strSql = @"SELECT CASE WHEN SUM(TotalFee) >0 THEN SUM(TotalFee) ELSE 0 END TotalFee FROM 
                        IP_FeeItemRecord WHERE PatListID={0} AND OrderType IN (0,1,2,3,4) AND RecordFlag=0 AND CostHeadID=0 AND WorkID = {1}
                        UNION ALL
                        SELECT CASE WHEN SUM(TotalFee) >0 THEN SUM(TotalFee) ELSE 0 END TotalFee FROM 
                        IP_FeeItemRecord WHERE PatListID={0} AND OrderType=4 AND RecordFlag=0 AND CostHeadID=0 AND WorkID = {1}
                        UNION ALL
                        SELECT  CASE WHEN SUM(TotalFee) > 0 THEN SUM(TotalFee) ELSE 0 END TotalFee FROM IP_FeeItemRecord
                        WHERE   PatListID = {0} AND OrderType IN ( 0, 1, 2, 3, 4 ) AND 
                        PresDate BETWEEN '{2}' AND '{3}' AND RecordFlag = 0 AND CostHeadID = 0 AND WorkID = {1}";
            string sTime = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
            string eTime = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            strSql = string.Format(strSql, patListID, oleDb.WorkId, sTime, eTime);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取病人预交金累计交费金额
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人预交金累计交费金额</returns>
        public DataTable GetPatSumPay(int patListID)
        {
            string strSql = @"SELECT CASE WHEN SUM(TotalFee) >0 
                            THEN SUM(TotalFee) ELSE 0 END AS TotalFee 
                            FROM IP_DepositList WHERE Status = 0 AND PatListID={0} AND CostHeadID=0 AND WorkID = {1} ";
            strSql = string.Format(strSql, patListID, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取病人医嘱列表
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="orderType">医嘱类型</param>
        /// <returns>病人医嘱列表</returns>
        public DataTable GetPatOrderList(int patListID, int orderType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT  A.OrderID , ");
            strSql.Append(" A.PatListID , ");
            strSql.Append(" A.GroupID , ");
            strSql.Append(" A.OrderBdate , ");
            strSql.Append(" A.OrderDoc , ");
            strSql.Append(" B.Name AS DoctorName , ");
            strSql.Append(" A.ItemName + CASE A.Spec ");
            strSql.Append(" WHEN '' THEN '' ");
            strSql.Append(" ELSE '【' + A.Spec + '】' ");
            strSql.Append(" END ItemName , ");
            strSql.Append(" A.Spec , ");
            strSql.Append(" A.DoseNum, ");
            strSql.Append(" A.StatID, ");
            strSql.Append(" A.Dosage , ");
            strSql.Append(" A.DosageUnit , ");
            strSql.Append(" A.ChannelName , ");
            strSql.Append(" A.FirstNum , ");
            strSql.Append(" A.Entrust, ");
            strSql.Append(" A.TeminalNum , ");
            strSql.Append(" A.ItemType , ");
            strSql.Append(" A.EOrderDate , ");
            strSql.Append(" A.Frequency, ");
            strSql.Append(" CASE A.OrderType ");
            strSql.Append(" WHEN 2 THEN '是' ");
            strSql.Append(" ELSE '' ");
            strSql.Append(" END AS SelfDrug , ");
            strSql.Append(" CASE A.OrderType ");
            strSql.Append(" WHEN 3 THEN '是' ");
            strSql.Append(" ELSE '' ");
            strSql.Append(" END AS BeltDrug ");
            strSql.Append(" FROM    IPD_OrderRecord A ");
            strSql.Append(" LEFT JOIN BaseEmployee B ON A.OrderDoc = B.EmpId ");
            strSql.AppendFormat(" WHERE   A.PatListID = {0} ", patListID);
            strSql.Append(" AND A.OrderStatus IN ( 2, 3, 4, 5 ) ");
            strSql.AppendFormat(" AND A.OrderCategory = {0} ", orderType);
            strSql.AppendFormat(" AND A.WorkID = {0} ", oleDb.WorkId);
            strSql.Append(" AND A.DeleteFlag =0 ");
            strSql.Append(" ORDER BY A.OrderBdate ");
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 获取医嘱关联记账费用列表
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="orderID">医嘱Id</param>
        /// <param name="groupID">医嘱分组ID</param>
        /// <returns>医嘱关联记账费用列表</returns>
        public DataTable GetOrderFeeList(int patListID, int orderID, int groupID)
        {
            StringBuilder newStrSql = new StringBuilder();
            newStrSql.Append(" SELECT ");
            newStrSql.Append(" 0 CheckFlg , ");
            newStrSql.Append(" C.UnitPrice, ");
            newStrSql.Append(" B.Name AS NurseName, ");
            newStrSql.Append(" CASE A.PresNurseID WHEN 0 THEN  dbo.fnGetEmpName(D.CurrNurseID)ELSE  dbo.fnGetEmpName(A.PresNurseID) END PresNurseName, ");
            newStrSql.Append(" A.ItemName + CASE A.Spec ");
            newStrSql.Append(" WHEN '' THEN '' ");
            newStrSql.Append(" ELSE '【' + A.Spec + '】' ");
            newStrSql.Append(" END ItemName , ");
            newStrSql.Append(" A.* ");
            newStrSql.Append(" FROM ");
            newStrSql.Append(" IP_FeeItemRecord A  ");
            newStrSql.Append(" LEFT JOIN BaseEmployee B ON A.PresDoctorID = B.EmpId  ");
            newStrSql.Append(" LEFT JOIN ViewFeeItem_List C ON A.ItemID = C.ItemID ");
            newStrSql.Append(" LEFT JOIN IP_PatList D ON A.PatListID = D.PatListID  ");
            newStrSql.Append(" WHERE ");
            newStrSql.AppendFormat(" A.PatListID = {0} ", patListID);
            newStrSql.Append(" AND A.FeeSource = 1 ");
            newStrSql.AppendFormat(" AND A.GroupID = {0} ", groupID);
            newStrSql.Append(" AND A.CostHeadID = 0 ");
            newStrSql.AppendFormat(" AND A.WorkID = {0} ", oleDb.WorkId);
            newStrSql.Append(" ORDER BY A.PresDate ");
            return oleDb.GetDataTable(newStrSql.ToString());
        }

        /// <summary>
        /// 获取医嘱关联记账费用按项目汇总列表
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="orderID">医嘱Id</param>
        /// <param name="groupID">医嘱分组ID</param>
        /// <returns>项目汇总列表</returns>
        public DataTable GetOrderSumFeeList(int patListID, int orderID, int groupID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT A.ItemID,A.ItemName, ");
            strSql.Append(" A.Spec, ");
            strSql.Append(" MIN(A.FeeRecordID) FeeRecordID, ");
            strSql.Append(" MIN(A.SellPrice) SellPrice, ");
            strSql.Append(" SUM(A.Amount) Amount, ");
            strSql.Append(" A.Unit, ");
            strSql.Append(" MIN(C.UnitPrice) UnitPrice , ");
            strSql.Append(" SUM(A.TotalFee) TotalFee ");
            strSql.Append(" FROM    IP_FeeItemRecord A ");
            strSql.Append(" LEFT JOIN ViewFeeItem_List C ON A.ItemID = C.ItemID ");
            strSql.AppendFormat(" WHERE A.PatListID = {0} ", patListID);
            strSql.AppendFormat(" AND A.GroupID = {0} ", groupID);
            strSql.Append(" AND A.CostHeadID = 0  AND A.FeeSource = 1 ");
            strSql.AppendFormat(" AND A.WorkID = {0} ", oleDb.WorkId);
            strSql.Append(" GROUP BY A.ItemID,A.ItemName,A.Spec,A.Unit ");
            strSql.Append(" ORDER BY FeeRecordID ");
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 获取费用对象
        /// </summary>
        /// <param name="feeRecordID">费用ID</param>
        /// <returns>费用对象</returns>
        public DataTable GetIPFeeItemRecordInfo(int feeRecordID)
        {
            string strSql = string.Format("SELECT * FROM IP_FeeItemRecord WHERE FeeRecordID = {0}", feeRecordID);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 检查药品是否已发药、检查项目是否已做检查
        /// </summary>
        /// <param name="feeRecordID">费用明细ID</param>
        /// <returns>true：未发药/false：已发药</returns>
        public bool CheckIsMedicine(int feeRecordID)
        {
            string strSql = string.Format("SELECT FeeRecordID FROM IP_FeeItemRecord WHERE FeeRecordID={0} AND DrugFlag=0", feeRecordID);
            return oleDb.GetDataTable(strSql).Rows.Count > 0;
        }

        /// <summary>
        /// 根据处方明细ID查询处方是否已生成统领单
        /// </summary>
        /// <param name="feeRecordID">处方明细ID</param>
        /// <returns>true已生成</returns>
        public bool CheckIsGenerateDrugBillDetail(int feeRecordID)
        {
            string strSql = string.Format("SELECT BillDetailID FROM IP_DrugBillDetail WHERE FeeRecordID={0}", feeRecordID);
            return oleDb.GetDataTable(strSql).Rows.Count > 0;
        }

        /// <summary>
        /// 根据统领单明细ID删除统领单明细
        /// </summary>
        /// <param name="feeRecordID">处方明细ID</param>
        /// <returns>true删除成功</returns>
        public bool DelDrugBillDetail(int feeRecordID)
        {
            string strSql = string.Format("DELETE FROM  IP_DrugBillDetail WHERE FeeRecordID={0}", feeRecordID);
            return oleDb.DoCommand(strSql) > 0;
        }

        /// <summary>
        /// 获取费用汇总数据
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="orderType">账单类型</param>
        /// <returns>费用汇总数据</returns>
        public DataTable GetSumCostList(int patListID, string orderType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT A.ItemID , ");
            strSql.Append(" A.ItemName , ");
            strSql.Append(" A.Spec , ");
            strSql.Append(" MIN(A.SellPrice) SellPrice , ");
            strSql.Append(" SUM(A.Amount) Amount , ");
            strSql.Append(" A.Unit , ");
            strSql.Append(" SUM(A.TotalFee) TotalFee ");
            strSql.Append(" FROM    dbo.IP_FeeItemRecord A ");
            strSql.AppendFormat(" WHERE A.PatListID = {0} ", patListID);
            strSql.Append(" AND A.CostHeadID = 0 ");
            strSql.AppendFormat(" AND A.OrderType IN ({0}) ", orderType);
            string feeSource = string.Empty;
            if (orderType.Contains("2") || orderType.Contains("3"))
            {
                feeSource = "0";
            }

            if (orderType.Contains("4"))
            {
                if (string.IsNullOrEmpty(feeSource))
                {
                    feeSource = "2";
                }
                else
                {
                    feeSource += ",2";
                }
            }

            strSql.AppendFormat(" AND A.FeeSource IN ({0}) ", feeSource);
            strSql.Append(" GROUP BY A.ItemID ,A.ItemName,A.Spec,A.Unit ");
            strSql.Append(" ORDER BY A.ItemID ");
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 查询病人所有已记账的费用列表
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="orderType">账单类型</param>
        /// <returns>已记账的费用列表</returns>
        public DataTable GetCostList(int patListID, string orderType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT ");
            strSql.Append(" 0 CheckFlg ,");
            strSql.Append(" A.FeeRecordID ,");
            strSql.Append(" A.GenerateID , ");
            strSql.Append(" A.PatListID , ");
            strSql.Append(" A.PatName , ");
            strSql.Append(" A.PatDeptID , ");
            strSql.Append(" A.PatDoctorID , ");
            strSql.Append(" A.PatNurseID , ");
            strSql.Append(" A.BabyID , ");
            strSql.Append(" A.ItemID , ");
            strSql.Append(" A.ItemName , ");
            strSql.Append(" A.FeeClass , ");
            strSql.Append(" A.FeeSource , ");
            strSql.Append(" A.StatID , ");
            strSql.Append(" A.Spec , ");
            strSql.Append(" A.Unit , ");
            strSql.Append(" A.PackAmount , ");
            strSql.Append(" A.InPrice , ");
            strSql.Append(" A.SellPrice , ");
            strSql.Append(" A.Amount , ");
            strSql.Append(" A.DoseAmount , ");
            strSql.Append(" D.Name AS PresNurseName , ");
            strSql.Append(" A.TotalFee , ");
            strSql.Append(" A.PresDeptID , ");
            strSql.Append(" A.PresDoctorID , ");
            strSql.Append(" A.ExecDeptID , ");
            strSql.Append(" C.Name DeptName, ");
            strSql.Append(" A.PresDate , ");
            strSql.Append(" A.ChargeDate , ");
            strSql.Append(" A.DrugFlag , ");
            strSql.Append(" A.RecordFlag , ");
            strSql.Append(" CASE A.RecordFlag WHEN  0 THEN '正常' ");
            strSql.Append(" WHEN 1 THEN '退费' WHEN 2 THEN '冲正' WHEN 9 THEN '取消冲账' END AS RecordFlagName, ");
            strSql.Append(" A.OldFeeRecordID , ");
            strSql.Append(" A.CostHeadID , ");
            strSql.Append(" A.CostType , ");
            strSql.Append(" A.UploadID , ");
            strSql.Append(" A.PackUnit , ");
            strSql.Append(" A.OrderID , ");
            strSql.Append(" A.GroupID , ");
            strSql.Append(" A.OrderType , ");
            strSql.Append(" A.FrequencyID , ");
            strSql.Append(" A.FrequencyName , ");
            strSql.Append(" A.ChannelID , ");
            strSql.Append(" A.ChannelName , ");
            strSql.Append(" B.Name ");
            strSql.Append(" FROM    IP_FeeItemRecord A ");
            strSql.Append(" LEFT JOIN BaseEmployee B ON A.PresDoctorID=B.EmpId ");
            strSql.Append(" LEFT JOIN BaseDept C ON A.ExecDeptID = C.DeptId ");
            strSql.Append(" LEFT JOIN BaseEmployee D ON A.PresNurseID=D.EmpId ");
            strSql.Append(" WHERE   A.CostHeadID = 0 ");
            strSql.AppendFormat(" AND A.PatListID = {0} ", patListID);
            strSql.AppendFormat(" AND A.OrderType IN ({0}) ", orderType);
            string feeSource = string.Empty;
            if (orderType.Contains("2") || orderType.Contains("3"))
            {
                feeSource = "0";
            }

            if (orderType.Contains("4"))
            {
                if (string.IsNullOrEmpty(feeSource))
                {
                    feeSource = "2";
                }
                else
                {
                    feeSource += ",2";
                }
            }

            strSql.AppendFormat(" AND A.FeeSource IN ({0}) ", feeSource);
            strSql.Append(" ORDER BY FeeRecordID,ChargeDate ");
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 检查费用记录是否已记账
        /// </summary>
        /// <param name="generateId">费用生成ID</param>
        /// <returns>true已记账</returns>
        public bool IsFeeCharge(int generateId)
        {
            string strSql = string.Format("SELECT GenerateID FROM IP_FeeItemRelationship WHERE GenerateID = {0} AND WorkID = {1} ", generateId, oleDb.WorkId);
            return oleDb.GetDataTable(strSql).Rows.Count > 0;
        }

        /// <summary>
        /// 获取入院病人登记信息
        /// </summary>
        /// <param name="patListID">病人登记信息ID</param>
        /// <returns>病人登记信息</returns>
        public DataTable GetPatListInfo(int patListID)
        {
            StringBuilder selectSql = new StringBuilder();
            selectSql.Append(" SELECT ");
            selectSql.Append(" PatListID ");
            selectSql.Append(" ,MemberID ");
            selectSql.Append(" ,MemberAccountID ");
            selectSql.Append(" ,CardNO ");
            selectSql.Append(" ,SerialNumber ");
            selectSql.Append(" ,CaseNumber ");
            selectSql.Append(" ,PatDatCardNo ");
            selectSql.Append(" ,WorkID ");
            selectSql.Append(" ,Times ");
            selectSql.Append(" ,PatTypeID ");
            selectSql.Append(" ,PatName ");
            selectSql.Append(" ,EName ");
            selectSql.Append(" ,PYCode ");
            selectSql.Append(" ,WBCode ");
            selectSql.Append(" ,Sex ");
            selectSql.Append(" ,Birthday ");
            selectSql.Append(" ,Age ");
            selectSql.Append(" ,Status ");
            selectSql.Append(" ,EnterHDate ");
            selectSql.Append(" ,LeaveHDate ");
            selectSql.Append(" ,EnterDeptID ");
            selectSql.Append(" ,EnterWardID ");
            selectSql.Append(" ,EnterDiseaseCode ");
            selectSql.Append(" ,EnterDiseaseName ");
            selectSql.Append(" ,EnterDoctorID ");
            selectSql.Append(" ,EnterNurseID ");
            selectSql.Append(" ,BedNo ");
            selectSql.Append(" ,CurrDeptID ");
            selectSql.Append(" ,CurrWardID ");
            selectSql.Append(" ,CurrDoctorID ");
            selectSql.Append(" ,CurrNurseID ");
            selectSql.Append(" ,EnterSituation ");
            selectSql.Append(" ,OutSituation ");
            selectSql.Append(" ,MakerDate ");
            selectSql.Append(" ,MakerEmpID ");
            selectSql.Append(" ,SourceWay ");
            selectSql.Append(" ,SourcePerson ");
            selectSql.Append(" ,MedicareCard ");
            selectSql.Append(" FROM IP_PatList ");
            selectSql.Append(" WHERE ");
            selectSql.AppendFormat("PatListID = {0}", patListID);
            return oleDb.GetDataTable(selectSql.ToString());
        }

        /// <summary>
        /// 检查床位费是否存在重复计费
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="chargeDate">处方日期</param>
        /// <returns>true不存在</returns>
        public bool IsExistenceBedFeeData(int patListID, DateTime chargeDate)
        {
            string strSql = string.Format(
                @"SELECT ID FROM dbo.IP_FeeItemRelationship WHERE CONVERT(varchar(100), ChargeDate, 23)='{0}' 
                AND PatListID = {1} AND FeeSource=2 AND WorkID = {2} ", 
                chargeDate.ToString("yyyy-MM-dd"), 
                patListID, 
                oleDb.WorkId);
            return oleDb.GetDataTable(strSql).Rows.Count <= 0;
        }

        /// <summary>
        /// 取得病人关联的所有床位
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人关联的所有床位</returns>
        public DataTable GetPatientBedList(int patListID)
        {
            string strSql = string.Format("SELECT BedID,IsPack FROM IP_BedInfo WHERE PatListID={0} AND WorkID = {1}", patListID, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 根据病人ID查询病人的床位费用
        /// </summary>
        /// <param name="bedID">床位ID</param>
        /// <param name="feeType">费用类型</param>
        /// <returns>床位费用列表</returns>
        public DataTable GetBedFeeItemList(int bedID, int feeType)
        {
            string strSql = @"SELECT A.ItemAmount,B.* FROM dbo.IP_BedFee A
                            LEFT JOIN ViewFeeItem_SimpleList B
                            ON A.ItemID = B.ItemID
                            WHERE A.BedID ={0} AND A.WorkID={1} AND A.FeeType={2}";
            strSql = string.Format(strSql, bedID, oleDb.WorkId, feeType);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 检查是否存在重复账单
        /// </summary>
        /// <param name="generateID">费用生成ID</param>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="chargeDate">处方日期</param>
        /// <returns>true不存在</returns>
        public bool IsExistenceItemAccountingData(int generateID, int patListID, DateTime chargeDate)
        {
            string strSql = string.Format(
                            @"SELECT ID FROM dbo.IP_FeeItemRelationship WHERE GenerateID={0} AND PatListID = {2}
                            AND CONVERT(varchar(100), ChargeDate, 23)='{1}' AND FeeSource = 0 
                            AND WorkID = {3} ", 
                            generateID, 
                            chargeDate.ToString("yyyy-MM-dd"), 
                            patListID, 
                            oleDb.WorkId);
            return oleDb.GetDataTable(strSql).Rows.Count <= 0;
        }

        /// <summary>
        /// 根据组合项目ItemID获取组合项目明细列表
        /// </summary>
        /// <param name="itemId">组合项目ID</param>
        /// <returns>组合项目明细列表</returns>
        public DataTable CombinationProjectDetails(int itemId)
        {
            string strSql = string.Format(
                @"SELECT * FROM ViewFeeItem_SimpleList WHERE ItemID IN(SELECT ItemID 
                FROM Basic_ExamItemFee WHERE ExamItemID = {0} AND WorkID = {1}) AND WorkID={1}", 
                itemId, 
                oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 停用长期账单
        /// </summary>
        /// <param name="generateID">账单ID</param>
        public void StopFeeLongOrderData(int generateID)
        {
            string strSql = string.Format("UPDATE IP_FeeItemGenerate SET IsStop = 1 WHERE GenerateID = {0}", generateID);
            oleDb.DoCommand(strSql);
        }

        /// <summary>
        /// 查询病人账单列表
        /// </summary>
        /// <param name="patListID">账单列表</param>
        /// <param name="orderType">账单类型</param>
        /// <returns>病人账单列表</returns>
        public DataTable GetPatFeeItemGenerate(int patListID, int orderType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT DISTINCT CASE WHEN B.ID > 0 THEN 1 ");
            strSql.Append(" ELSE 0 END AS StrikeABalanceFLG , ");
            strSql.Append(" A.GenerateID , ");
            strSql.Append(" 0 CheckFLG, ");
            strSql.Append(" C.ItemCode , ");
            strSql.Append(" A.ItemName , ");
            strSql.Append(" E.Name ExecDeptName , ");
            strSql.Append(" A.Spec , ");
            strSql.Append(" C.UnitPrice , ");
            strSql.Append(" C.StoreAmount , ");
            strSql.Append(" A.Amount , ");
            strSql.Append(" A.Unit , ");
            strSql.Append(" A.TotalFee , ");
            strSql.Append(" D.Name MarkEmpName , ");
            strSql.Append(" A.PresDate , ");
            strSql.Append(" A.PatListID , ");
            strSql.Append(" A.PatName , ");
            strSql.Append(" A.PatDeptID , ");
            strSql.Append(" A.PatDoctorID , ");
            strSql.Append(" A.PatNurseID , ");
            strSql.Append(" A.BabyID , ");
            strSql.Append(" A.ItemID , ");
            strSql.Append(" A.FeeClass , ");
            strSql.Append(" A.StatID , ");
            strSql.Append(" A.PackAmount , ");
            strSql.Append(" A.InPrice , ");
            strSql.Append(" A.SellPrice , ");
            strSql.Append(" A.DoseAmount , ");
            strSql.Append(" A.PresDeptID , ");
            strSql.Append(" A.PresDoctorID , ");
            strSql.Append(" A.ExecDeptDoctorID , ");
            strSql.Append(" A.MarkDate , ");
            strSql.Append(" A.MarkEmpID , ");
            strSql.Append(" A.SortOrder , ");
            strSql.Append(" A.PackUnit , ");
            strSql.Append(" A.OrderID , ");
            strSql.Append(" A.GroupID , ");
            strSql.Append(" A.OrderType , ");
            strSql.Append(" A.FrequencyID , ");
            strSql.Append(" A.FrequencyName , ");
            strSql.Append(" A.ChannelID , ");
            strSql.Append(" A.ChannelName , ");
            strSql.Append(" A.FeeSource , ");
            strSql.Append(" A.IsStop , ");
            strSql.Append(" 0 AS IsUpdate , ");
            strSql.Append(" A.WorkID ");
            strSql.Append(" FROM IP_FeeItemGenerate A ");
            strSql.Append(" LEFT JOIN IP_FeeItemRelationship B ON A.GenerateID = B.GenerateID ");
            strSql.Append(" LEFT JOIN dbo.ViewFeeItem_SimpleList C ON A.ItemID = C.ItemID ");
            strSql.Append(" AND A.ExecDeptDoctorID = C.ExecDeptId ");
            strSql.Append(" AND A.FeeClass = C.ItemClass ");
            strSql.Append(" AND A.StatID = C.StatID ");
            strSql.Append(" LEFT JOIN BaseEmployee D ON A.MarkEmpID = D.EmpId ");
            strSql.Append(" LEFT JOIN BaseDept E ON A.ExecDeptDoctorID=E.DeptId ");
            strSql.Append(" WHERE ");
            strSql.AppendFormat(" A.PatListID = {0} AND A.OrderType = {1} ", patListID, orderType);
            strSql.Append(" AND A.IsStop <> 1");
            strSql.Append(" AND A.FeeSource <> 4 ");
            strSql.AppendFormat(" AND A.WorkID = {0} ", oleDb.WorkId);
            strSql.Append(" ORDER BY MarkDate ");
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 费用冲账或取消冲账时，修改申请表状态
        /// </summary>
        /// <param name="presDetailID">医嘱ID</param>
        /// <param name="isReturns">记录状态</param>
        /// <returns>true修改成功</returns>
        public bool UpdateEXAMedicalApplyDetail(int presDetailID, int isReturns)
        {
            string strSql = string.Format("UPDATE EXA_MedicalApplyDetail SET IsReturns = {0} WHERE PresDetailID = {1}", isReturns, presDetailID);
            return oleDb.DoCommand(strSql) > 0;
        }

        /// <summary>
        /// 获取记账开始时间以及记账天数
        /// </summary>
        /// <param name="generateIdList">费用生成ID列表</param>
        /// <param name="endTime">记账结束时间</param>
        /// <param name="isBedFee">true:床位费/false:账单费用</param>
        /// <param name="patListId">病人登记ID</param>
        /// <returns>记账开始时间以及记账天数</returns>
        public DataTable GetAccountDate(string generateIdList, DateTime endTime, bool isBedFee, int patListId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT A.GenerateID ,MIN(A.FeeClass)AS FeeClass,A.PatListID,MIN(A.OrderType) AS OrderType, ");
            strSql.Append(" CASE WHEN MAX(B.ExecDate) IS NULL THEN MIN(A.PresDate) ");
            strSql.Append(" ELSE DATEADD(DAY, 1, MAX(B.ExecDate)) ");
            strSql.Append(" END AS ExecDate, ");
            strSql.Append(" CASE WHEN MAX(B.ExecDate) IS NULL ");
            strSql.AppendFormat(" THEN DATEDIFF(DAY, MIN(A.PresDate), '{0}') ", endTime);
            strSql.AppendFormat(" ELSE DATEDIFF(DAY, DATEADD(DAY, 1, MAX(B.ExecDate)), '{0}') ", endTime);
            strSql.Append(" END AS[Days] ");
            strSql.Append(" FROM    IP_FeeItemGenerate A ");
            strSql.Append(" LEFT JOIN IP_FeeItemRelationship B ON B.GenerateID = A.GenerateID ");
            if (isBedFee)
            {
                strSql.Append("WHERE A.GenerateID IN(SELECT GenerateID FROM");
                strSql.AppendFormat(" IP_FeeItemGenerate WHERE A.PatListID={0} ", patListId);
                strSql.Append(" AND A.FeeSource = 4 AND IsStop = 0) ");
            }
            else
            {
                strSql.AppendFormat(" WHERE   A.GenerateID IN({0}) AND A.FeeSource <> 4 ", generateIdList);
            }

            strSql.Append(" GROUP BY A.GenerateID,A.PatListID ");
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 账单记账获取费用生成数据并且更新价格
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <param name="generateID">账单Id</param>
        /// <param name="isBedFee">床位费标志</param>
        /// <returns>最新费用数据</returns>
        public DataTable GetFeeItemGenerateData(int patListId, int generateID, bool isBedFee)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT  A.GenerateID , ");
            strSql.Append(" A.PatListID , ");
            strSql.Append(" A.PatName , ");
            strSql.Append(" A.PatDeptID , ");
            strSql.Append(" A.PatDoctorID , ");
            strSql.Append(" A.PatNurseID , ");
            strSql.Append(" A.BabyID , ");
            strSql.Append(" A.ItemID , ");
            strSql.Append(" A.ItemName , ");
            strSql.Append(" A.FeeClass , ");
            strSql.Append(" 0 AS FeeSource , ");
            strSql.Append(" A.StatID , ");
            strSql.Append(" B.Standard AS Spec , ");
            strSql.Append(" A.Unit , ");
            strSql.Append(" B.MiniConvertNum AS PackAmount , ");
            strSql.Append(" B.InPrice , ");
            strSql.Append(" B.SellPrice , ");
            strSql.Append(" A.Amount , ");
            strSql.Append(" 0 AS DoseAmount , ");
            strSql.Append(" A.TotalFee , ");
            strSql.Append(" A.PresDeptID , ");
            strSql.Append(" A.PresDoctorID , ");
            strSql.Append(" A.ExecDeptDoctorID AS ExecDeptID, ");
            strSql.Append(" A.PresDate , ");
            strSql.Append(" GETDATE() AS ChargeDate , ");
            strSql.Append(" 0 AS DrugFlag , ");
            strSql.Append(" 0 AS RecordFlag , ");
            strSql.Append(" 0 AS OldFeeRecordID , ");
            strSql.Append(" 0 AS CostHeadID , ");
            strSql.Append(" 0 AS CostType , ");
            strSql.Append(" 0 AS UploadID , ");
            strSql.Append(" A.PackUnit , ");
            strSql.Append(" 0 AS OrderID , ");
            strSql.Append(" 0 AS GroupID , ");
            strSql.Append(" A.OrderType , ");
            strSql.Append(" 0 AS FrequencyID , ");
            strSql.Append(" '' AS FrequencyName , ");
            strSql.Append(" 0 AS ChannelID , ");
            strSql.Append(" '' AS ChannelName ");
            strSql.Append(" FROM IP_FeeItemGenerate A ");
            strSql.Append(" LEFT JOIN ViewFeeItem_SimpleList B ON A.ItemID = B.ItemID ");
            strSql.AppendFormat(" WHERE A.PatListID = {0} ", patListId);
            strSql.AppendFormat(" AND A.GenerateID = {0} ", generateID);
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 检查病人在院状态
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <returns>病人在院状态</returns>
        public DataTable CheckPatientStatus(int patListId)
        {
            string strSql = string.Format("SELECT Status FROM IP_PatList WHERE PatListID = {0}", patListId);
            return oleDb.GetDataTable(strSql);
        }
    }
}