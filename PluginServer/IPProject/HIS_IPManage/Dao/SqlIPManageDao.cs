using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.IPManage;

namespace HIS_IPManage.Dao
{
    /// <summary>
    /// 住院护士站sql
    /// </summary>
    public class SqlIPManageDao : AbstractDao, IIPManageDao
    {
        #region "住院登记"

        /// <summary>
        /// 住院病人列表取得
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="dept">入院科室</param>
        /// <param name="patType">病人类型</param>
        /// <param name="selectParm">检索条件(住院号、病案号、床位号)</param>
        /// <param name="patStatus">病人状态</param>
        /// <param name="isPay">预交金界面查询标志</param>
        /// <returns>病人列表</returns>
        public DataTable GetPatientList(
            DateTime startTime,
            DateTime endTime,
            int dept,
            int patType,
            string selectParm,
            string patStatus,
            bool isPay)
        {
            StringBuilder selectSql = new StringBuilder();
            selectSql.Append(" SELECT top 100 ");
            selectSql.Append(" A.PatListID "); // 病人登记信息ID
            selectSql.Append(" ,A.MemberID ");  // 会员ID
            selectSql.Append(" ,A.MemberAccountID "); // 会员账号ID
            selectSql.Append(" ,A.CardNO "); // 会员卡号
            selectSql.Append(" ,A.CurrDeptID ");  // 当前科室ID
            selectSql.Append(" ,D.PatientID "); // 新入院病人ID
            selectSql.Append(" ,A.CaseNumber ");// 住院病案号
            selectSql.Append(" ,A.SerialNumber ");// 住院流水号
            selectSql.Append(" ,A.PatName"); // 姓名
            selectSql.Append(" ,A.Status"); // 病人状态
            selectSql.Append(" ,CASE A.Sex"); // 性别
            selectSql.Append(" WHEN '1' THEN '男' ");
            selectSql.Append(" WHEN '2' THEN '女' ");
            selectSql.Append(" ELSE '未知' END AS Sex ");
            selectSql.Append(" ,B.Name DeptName "); // 科室
            selectSql.Append(" ,C.Name DoctorName "); // 医生
            selectSql.Append(" ,A.EnterHDate "); // 入院日期
            selectSql.Append(" ,A.LeaveHDate "); // 出院日期
            selectSql.Append(" ,A.BedNo "); // 床位号
            selectSql.Append(" ,A.PatTypeID "); // 病人类型ID
            selectSql.Append(" ,E.PatTypeName "); // 病人类型名
            selectSql.Append(" ,A.EnterDiseaseName "); // 入院诊断
            selectSql.Append(" FROM ");
            selectSql.Append(" IP_PatList A ");  // 住院登记记录信息表
            selectSql.Append(" LEFT JOIN ");
            selectSql.Append(" BaseDept B ");    // 科室表
            selectSql.AppendFormat(" ON A.CurrDeptID = B.DeptId ");
            selectSql.Append(" LEFT JOIN ");
            selectSql.Append(" BaseEmployee C ");  //人员表
            selectSql.Append(" ON ");
            selectSql.AppendFormat(" A.CurrDoctorID = C.EmpId ");
            selectSql.Append(" LEFT JOIN ");
            selectSql.Append(" IP_PatientInfo D ");  //人员表
            selectSql.Append(" ON ");
            selectSql.AppendFormat(" A.PatListID = D.PatListID ");
            selectSql.AppendFormat(" LEFT JOIN Basic_PatType E ON A.PatTypeID = E.PatTypeID ");
            selectSql.Append(" WHERE ");
            // 入院科室
            selectSql.AppendFormat(" (A.CurrDeptID = {0} OR (-1)={0}) ", dept);
            if (patType != 0)
            {
                // 病人类型
                selectSql.AppendFormat(" AND (A.PatTypeID = {0} OR (-1) = {0}) ", patType);
            }

            if (!string.IsNullOrEmpty(patStatus))
            {
                string[] c = patStatus.Split(',');
                // 病人状态
                selectSql.Append(" AND A.Status IN ( ");
                for (int i = 0; i < c.Length; i++)
                {
                    selectSql.Append(c[i]);
                    if (i < c.Length - 1)
                    {
                        selectSql.Append(",");
                    }
                }

                if (isPay)
                {
                    if (patStatus == "1")
                    {
                        selectSql.Append(",2");
                    }
                }

                selectSql.Append(") ");
            }

            selectSql.Append(" AND A.Status != 9 ");
            if (startTime != DateTime.MinValue && endTime != DateTime.MinValue)
            {
                string sTime = startTime.ToString("yyyy-MM-dd") + " 00:00:00";
                string eTime = endTime.ToString("yyyy-MM-dd") + " 23:59:00";
                selectSql.AppendFormat(" AND A.MakerDate BETWEEN '{0}' AND '{1}' ", sTime, eTime);
            }

            // 检索条件(住院号、病案号、病人姓名、床位号)
            if (!string.IsNullOrEmpty(selectParm))
            {
                selectSql.AppendFormat(" AND (A.SerialNumber Like '%{0}%' ", selectParm);  // 住院流水号
                selectSql.AppendFormat(" OR A.CaseNumber LIKE '%{0}%' ", selectParm);   // 住院病案号
                selectSql.AppendFormat(" OR A.PatName LIKE '%{0}%' ", selectParm);  // 病人姓名
                selectSql.AppendFormat(" OR A.BedNo LIKE '%{0}%') ", selectParm); // 床位号
            }

            selectSql.AppendFormat(" AND A.WorkID = {0} ", oleDb.WorkId);
            selectSql.Append(" ORDER BY A.EnterHDate ");
            // 住院病人列表`
            return oleDb.GetDataTable(selectSql.ToString());
        }

        /// <summary>
        /// 获取入院病人信息
        /// </summary>
        /// <param name="patientID">病人ID</param>
        /// <returns>入院病人信息</returns>
        public DataTable GetPatientInfo(int patientID)
        {
            StringBuilder selectSql = new StringBuilder();
            selectSql.Append(" SELECT  ");
            selectSql.Append(" PatientID ");
            selectSql.Append(" ,PatListID ");
            selectSql.Append(" ,IdentityNum ");
            selectSql.Append(" ,Nationality ");
            selectSql.Append(" ,Nation ");
            selectSql.Append(" ,Native ");
            selectSql.Append(" ,Matrimony ");
            selectSql.Append(" ,Occupation ");
            selectSql.Append(" ,CulturalLevel ");
            selectSql.Append(" ,Birthplace ");
            selectSql.Append(" ,BirthplaceDetail ");
            selectSql.Append(" ,Phone ");
            selectSql.Append(" ,DRegisterAddr ");
            selectSql.Append(" ,DZipCode ");
            selectSql.Append(" ,DRegisterAddrDetail ");
            selectSql.Append(" ,NAddress ");
            selectSql.Append(" ,NZipCode ");
            selectSql.Append(" ,NAddressDetail ");
            selectSql.Append(" ,UnitName ");
            selectSql.Append(" ,UnitPhone ");
            selectSql.Append(" ,UZipCode ");
            selectSql.Append(" ,RelationName ");
            selectSql.Append(" ,Relation ");
            selectSql.Append(" ,RPhone ");
            selectSql.Append(" ,RAddress ");
            selectSql.Append(" ,RAddressDetail ");
            selectSql.Append(" ,WorkID ");
            selectSql.Append(" FROM IP_PatientInfo ");
            selectSql.Append(" WHERE ");
            selectSql.AppendFormat(" PatientID = {0} AND WorkID = {1} ", patientID, oleDb.WorkId);
            return oleDb.GetDataTable(selectSql.ToString());
        }

        /// <summary>
        /// 获取入院病人登记信息
        /// </summary>
        /// <param name="patListID">病人登记信息ID</param>
        /// <returns>入院病人登记信息</returns>
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
        /// 根据ID获取预交金记录
        /// </summary>
        /// <param name="depositID">预交金ID</param>
        /// <returns>预交金记录</returns>
        public DataTable GetPayADeposit(int depositID)
        {
            string selectSql = @"SELECT ''Head,''SerialNumberName,* FROM V_IP_PaymentList WHERE DepositID = {0} AND WorkID = {1} ";
            selectSql = string.Format(selectSql, depositID, oleDb.WorkId);
            return oleDb.GetDataTable(selectSql);
        }

        /// <summary>
        /// 根据ID修改打印次数
        /// </summary>
        /// <param name="depositID">预交金ID</param>
        /// <returns>true:修改成功</returns>
        public int UpdatePrintTime(int depositID)
        {
            string updSql = string.Format(@"UPDATE IP_DepositList SET PrintTimes = PrintTimes+1 WHERE DepositID = {0}", depositID);
            return oleDb.DoCommand(updSql);
        }

        /// <summary>
        /// 检查当前会员是否已办理入院
        /// </summary>
        /// <param name="cardNO">会员卡号</param>
        /// <returns>true:未登记</returns>
        public bool CheckPatientInTheHospital(string cardNO)
        {
            string strSql = string.Format("SELECT PatListId FROM IP_PatList WHERE Status IN (1,2,3) AND CardNO='{0}' AND WorkID = {1} ", cardNO, oleDb.WorkId);
            return oleDb.GetDataTable(strSql).Rows.Count <= 0;
        }

        /// <summary>
        /// 住院登记成功后修改住院证信息
        /// </summary>
        /// <param name="memberID">会员ID</param>
        public void UpdateInpatientReg(int memberID)
        {
            string strSql = string.Format(" UPDATE OPD_InpatientReg SET RegStatus = 1 WHERE MemberID = {0} AND RegStatus = 0 ", memberID);
            oleDb.DoCommand(strSql);
        }

        /// <summary>
        /// 根据入院病人登记ID获取病人预交金以及费用记录信息
        /// </summary>
        /// <param name="patListID">病人入院登记ID</param>
        /// <returns>病人预交金以及费用记录信息</returns>
        public DataTable GetPatientCostList(int patListID)
        {
            StringBuilder selectSql = new StringBuilder();
            selectSql.Append(" SELECT COUNT(A.PatListID)AS PatListCount, ");
            selectSql.Append(" COUNT(B.PatListID) AS DepoCount, ");
            selectSql.Append(" COUNT(C.PatListID) AS FeeCount, ");
            selectSql.Append(" COUNT(D.PatListID) AS BedInfoCount ");
            selectSql.Append(" FROM    IP_PatList A ");
            selectSql.AppendFormat(" LEFT JOIN IP_DepositList B ON A.PatListID = B.PatListID AND B.WorkID = {0} ", oleDb.WorkId);
            selectSql.AppendFormat(" LEFT JOIN IP_FeeItemRecord C ON A.PatListID = C.PatListID AND C.WorkID = {0} ", oleDb.WorkId);
            selectSql.AppendFormat(" LEFT JOIN IP_BedInfo D ON A.PatListID = D.PatListID AND D.WorkID = {0} ", oleDb.WorkId);
            selectSql.AppendFormat(" WHERE A.PatListID = {0} AND A.WorkID = {1} ", patListID, oleDb.WorkId);
            return oleDb.GetDataTable(selectSql.ToString());
        }

        /// <summary>
        /// 取消入院
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="patName">病人名</param>
        /// <returns>true：取消成功</returns>
        public bool CancelAdmission(int patListID, string patName)
        {
            string updSql = string.Format(
                @"UPDATE IP_PatList SET Status = 9,LeaveHDate='{2}' WHERE PatListID = {0} AND PatName = '{1}' AND WorkID = {3} ", 
                patListID, 
                patName, 
                DateTime.Now, 
                oleDb.WorkId);
            return oleDb.DoCommand(updSql) > 0;
        }

        /// <summary>
        /// 根据会员卡号获取病案号
        /// </summary>
        /// <param name="cardNO">会员卡号</param>
        /// <returns>病案号信息</returns>
        public DataTable GetCaseNumberByCardNO(int memberID)
        {
            string strSql = string.Format("SELECT CaseNumber FROM IP_PatList WHERE MemberID = {0} AND WorkID = {1} ", memberID, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 根据会员ID查询是否当前会员是否已开住院证
        /// </summary>
        /// <param name="memberID">会员ID</param>
        /// <returns>住院证信息</returns>
        public DataTable GetInpatientReg(int memberID)
        {
            string strSql = string.Format("SELECT * FROM OPD_InpatientReg WHERE MemberID = {0} AND RegStatus = 0 ", memberID);
            return oleDb.GetDataTable(strSql);
        }

        #endregion

        #region "护士站床位一览管理"

        /// <summary>
        /// 根据病区ID查询床位以及床位分配情况
        /// </summary>
        /// <param name="wardID">病区ID</param>
        /// <returns>床位列表</returns>
        public DataTable GetBedManageList(int wardID)
        {
            StringBuilder selectSql = new StringBuilder();
            selectSql.Append(" (SELECT CONVERT(INT,A.BedNO) AS AAA,A.BedID  ");
            selectSql.Append(" ,A.DeptID ");
            selectSql.Append(" ,A.WardID ");
            selectSql.Append(" ,A.RoomNO ");
            selectSql.Append(" ,A.BedNO ");
            selectSql.Append(" ,A.BedDoctorID ");
            selectSql.Append(" ,A.BedNurseID ");
            selectSql.Append(" ,A.IsPlus ");
            selectSql.Append(" ,CASE A.PatListID WHEN NULL THEN 0  ");
            selectSql.Append(" ELSE A.PatListID END AS PatListID ");
            selectSql.Append(" ,A.PatName ");
            selectSql.Append(" ,CASE A.PatSex ");
            selectSql.Append(" WHEN 1 THEN '男' ");
            selectSql.Append(" WHEN 2 THEN '女' ");
            selectSql.Append(" ELSE '' ");
            selectSql.Append(" END AS PatSex ");
            selectSql.Append(" ,A.PatDeptID ");
            selectSql.Append(" ,E.Name AS DeptName ");
            selectSql.Append(" ,A.PatDoctorID ");
            selectSql.Append(" ,C.Name AS DoctorName ");
            selectSql.Append(" ,A.PatNurseID ");
            selectSql.Append(" ,D.Name AS NurseName ");
            selectSql.Append(" ,A.BabyID ");
            selectSql.Append(" ,A.IsUsed ");
            selectSql.Append(" ,A.IsStoped ");
            selectSql.Append(" ,A.WorkID ");
            selectSql.Append(" ,B.SerialNumber ");
            selectSql.Append(" ,B.CaseNumber ");
            selectSql.Append(" ,B.Age ");
            selectSql.Append(" ,B.EnterHDate ");
            selectSql.Append(" ,B.EnterDiseaseCode ");
            selectSql.Append(" ,B.EnterDiseaseName ");
            selectSql.Append(" ,B.DietType ");
            selectSql.Append(" ,B.NursingLever ");
            selectSql.Append(" ,B.OutSituation ");
            selectSql.Append(" ,B.IsLeaveHosOrder ");
            selectSql.Append(" ,A.IsPack ");
            selectSql.Append(" ,dbo.fnGetPatTypeName(b.PatTypeID) as PatTypeName ");
            selectSql.Append(" FROM IP_BedInfo A ");
            selectSql.Append(" LEFT JOIN IP_PatList B ");
            selectSql.AppendFormat(" ON A.PatListID = B.PatListID ");
            selectSql.Append(" LEFT JOIN BaseEmployee C ");
            selectSql.AppendFormat(" ON A.PatDoctorID = C.EmpId ");
            selectSql.Append(" LEFT JOIN BaseEmployee D ");
            selectSql.AppendFormat(" ON A.PatNurseID = D.EmpId ");
            selectSql.Append(" LEFT JOIN BaseDept E  ");
            selectSql.AppendFormat(" ON A.PatDeptID = E.DeptId ");
            selectSql.Append(string.Format(" WHERE A.WardID = {0} AND A.IsStoped <> 1 AND A.WorkID = {1} ", wardID, oleDb.WorkId));
            selectSql.Append("AND ISNUMERIC(A.BedNO)=1");
            // 数字以外
            selectSql.Append(" UNION ALL ");
            selectSql.Append(" SELECT 9999999999 AS AAA,A.BedID");
            selectSql.Append(" ,A.DeptID ");
            selectSql.Append(" ,A.WardID ");
            selectSql.Append(" ,A.RoomNO ");
            selectSql.Append(" ,A.BedNO ");
            selectSql.Append(" ,A.BedDoctorID ");
            selectSql.Append(" ,A.BedNurseID ");
            selectSql.Append(" ,A.IsPlus ");
            selectSql.Append(" ,CASE A.PatListID WHEN NULL THEN 0  ");
            selectSql.Append(" ELSE A.PatListID END AS PatListID ");
            selectSql.Append(" ,A.PatName ");
            selectSql.Append(" ,CASE A.PatSex ");
            selectSql.Append(" WHEN 1 THEN '男' ");
            selectSql.Append(" WHEN 2 THEN '女' ");
            selectSql.Append(" ELSE '' ");
            selectSql.Append(" END AS PatSex ");
            selectSql.Append(" ,A.PatDeptID ");
            selectSql.Append(" ,E.Name AS DeptName ");
            selectSql.Append(" ,A.PatDoctorID ");
            selectSql.Append(" ,C.Name AS DoctorName ");
            selectSql.Append(" ,A.PatNurseID ");
            selectSql.Append(" ,D.Name AS NurseName ");
            selectSql.Append(" ,A.BabyID ");
            selectSql.Append(" ,A.IsUsed ");
            selectSql.Append(" ,A.IsStoped ");
            selectSql.Append(" ,A.WorkID ");
            selectSql.Append(" ,B.SerialNumber ");
            selectSql.Append(" ,B.CaseNumber ");
            selectSql.Append(" ,B.Age ");
            selectSql.Append(" ,B.EnterHDate ");
            selectSql.Append(" ,B.EnterDiseaseCode ");
            selectSql.Append(" ,B.EnterDiseaseName ");
            selectSql.Append(" ,B.DietType ");
            selectSql.Append(" ,B.NursingLever ");
            selectSql.Append(" ,B.OutSituation ");
            selectSql.Append(" ,B.IsLeaveHosOrder ");
            selectSql.Append(" ,A.IsPack ");
            selectSql.Append(" ,dbo.fnGetPatTypeName(b.PatTypeID) as PatTypeName ");
            selectSql.Append(" FROM IP_BedInfo A ");
            selectSql.Append(" LEFT JOIN IP_PatList B ");
            selectSql.AppendFormat(" ON A.PatListID = B.PatListID ");
            selectSql.Append(" LEFT JOIN BaseEmployee C ");
            selectSql.AppendFormat(" ON A.PatDoctorID = C.EmpId ");
            selectSql.Append(" LEFT JOIN BaseEmployee D ");
            selectSql.AppendFormat(" ON A.PatNurseID = D.EmpId ");
            selectSql.Append(" LEFT JOIN BaseDept E  ");
            selectSql.AppendFormat(" ON A.PatDeptID = E.DeptId ");
            selectSql.Append(string.Format(" WHERE A.WardID = {0} AND A.IsStoped <> 1 AND A.WorkID = {1} ", wardID, oleDb.WorkId));
            selectSql.Append(" AND ISNUMERIC(A.BedNO)=0 ) ORDER BY AAA ,BedNO ");
            //selectSql.Append(" ORDER BY A.BedID ");
            return oleDb.GetDataTable(selectSql.ToString());
        }

        /// <summary>
        /// 查询所有未分配床位的病人
        /// </summary>
        /// <param name="wardid">病区ID</param>
        /// <param name="status">病人状态</param>
        /// <returns>未分配床位病人列表</returns>
        public DataTable GetNotHospitalPatList(int wardid, string status)
        {
            StringBuilder selectSql = new StringBuilder();
            selectSql.Append(" SELECT  A.PatListID ,-1 AS ID, ");
            selectSql.Append(" A.PatName , ");
            selectSql.Append(" A.EnterHDate , ");
            selectSql.Append(" A.SerialNumber , ");
            selectSql.Append(" A.CurrDoctorID , ");
            selectSql.Append(" A.Status , ");
            //selectSql.Append(" C.Name AS DoctorName , ");
            selectSql.Append(" A.CurrNurseID , ");
            //selectSql.Append(" D.Name AS NurseName , ");
            selectSql.Append(" A.Sex, ");
            selectSql.Append(" CASE A.Sex ");
            selectSql.Append(" WHEN 1 THEN '男' ");
            selectSql.Append(" WHEN 2 THEN '女' ");
            selectSql.Append(" ELSE '' ");
            selectSql.Append(" END AS SexName , ");
            selectSql.Append(" A.CurrDeptID , ");
            selectSql.Append(" B.Name AS CurrDeptName , ");
            selectSql.Append(" CASE A.Status ");
            selectSql.Append(" WHEN 1 THEN '新入院' ");
            selectSql.Append(" WHEN 3 THEN '出院未结算' ");
            selectSql.Append(" END AS StatusName ");
            selectSql.Append(" FROM IP_PatList A ");
            selectSql.AppendFormat(" LEFT JOIN BaseDept B ON A.CurrDeptID = B.DeptId ");
            selectSql.AppendFormat(" WHERE A.Status = {0} AND CurrWardID = {1} AND A.WorkID = {2} ", status, wardid, oleDb.WorkId);
            return oleDb.GetDataTable(selectSql.ToString());
        }

        /// <summary>
        /// 获取所有转科病人
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <returns>所有转科病人</returns>
        public DataTable GetTransferPatList(int wardId)
        {
            StringBuilder selectSql = new StringBuilder();
            selectSql.Append(" SELECT A.PatListID ,A.ID, ");
            selectSql.Append(" B.PatName , ");
            selectSql.Append(" B.EnterHDate , ");
            selectSql.Append(" B.SerialNumber , ");
            selectSql.Append(" B.CurrDoctorID , ");
            selectSql.Append(" - 1 AS Status, ");
            selectSql.Append(" B.CurrNurseID , ");
            selectSql.Append(" B.Sex , ");
            selectSql.Append(" CASE B.Sex ");
            selectSql.Append(" WHEN 1 THEN '男' ");
            selectSql.Append(" WHEN 2 THEN '女' ");
            selectSql.Append(" ELSE '' ");
            selectSql.Append(" END AS SexName , ");
            selectSql.Append(" B.CurrDeptID , ");
            selectSql.Append(" C.Name AS CurrDeptName , ");
            selectSql.Append(" '转科' AS StatusName ");
            selectSql.Append(" FROM IPD_TransDept A ");
            selectSql.Append(" LEFT JOIN IP_PatList B ON A.PatListID = B.PatListID AND B.WorkID = A.WorkID ");
            selectSql.Append(" LEFT JOIN BaseDept C ON B.CurrDeptID = C.DeptId  AND C.WorkId = B.WorkID ");
            selectSql.Append(" WHERE B.Status = 2 ");
            selectSql.AppendFormat(" AND B.CurrWardID = {0} AND A.FinishFlag = 0 ", wardId);
            selectSql.AppendFormat(" AND A.WorkID = {0} AND A.CancelFlag = 0 ", oleDb.WorkId);
            return oleDb.GetDataTable(selectSql.ToString());
        }

        /// <summary>
        /// 获取床位绑定的医生以及护士ID
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        /// <returns>医生和护士Id</returns>
        public DataTable GetDoctorNurseID(int wardId, string bedNo)
        {
            string sql = string.Format(
                @"SELECT BedID,BedDoctorID,BedNurseID FROM IP_BedInfo WHERE WardID = {0} 
                            AND BedNO = '{1}' AND WorkID = {2} ", 
                            wardId, 
                            bedNo, 
                            oleDb.WorkId);
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 获取床位关联的医生和护士ID
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        /// <returns>床位关联的医生和护士ID信息</returns>
        public DataTable GetPatDoctorNurseID(int wardId, string bedNo)
        {
            string sql = string.Format(
                @"SELECT BedID,PatDoctorID,PatNurseID FROM IP_BedInfo WHERE 
                            WardID = {0} AND BedNO = '{1}' AND WorkID = {2}", 
                            wardId, 
                            bedNo, 
                            oleDb.WorkId);
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 病人换床查询所有空床
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <returns>空床位号列表</returns>
        public DataTable GetBedNoList(int wardId)
        {
            string selectSql = string.Format(
                @"SELECT BedID, WardID,RoomNO, BedNO FROM IP_BedInfo WHERE WardID={0} 
                            AND IsStoped<>1 AND IsUsed<>1 AND WorkID = {1}", 
                            wardId, 
                            oleDb.WorkId);
            return oleDb.GetDataTable(selectSql);
        }

        /// <summary>
        /// 包床--查询所有已分配床位的病人
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <returns>已分配床位病人列表</returns>
        public DataTable GetInTheHospitalPatList(int wardId)
        {
            StringBuilder selSql = new StringBuilder();
            selSql.Append(" SELECT DISTINCT A.WardID , ");
            selSql.Append(" A.BedNO , ");
            selSql.Append(" A.PatName , ");
            selSql.Append(" CASE A.PatSex ");
            selSql.Append(" WHEN 1 THEN '男' ");
            selSql.Append(" WHEN 2 THEN '女' ");
            selSql.Append(" END AS Sex , ");
            selSql.Append(" A.PatDoctorID , ");
            selSql.Append(" C.Name AS DoctorName , ");
            selSql.Append(" A.PatNurseID , ");
            selSql.Append(" D.Name AS NurseName , ");
            selSql.Append(" A.PatListID , ");
            selSql.Append(" A.PatDeptID , ");
            selSql.Append(" E.Name AS DeptName , ");
            selSql.Append(" A.BabyID ");
            selSql.Append(" FROM IP_BedInfo A ");
            selSql.Append(" LEFT JOIN IP_PatList B ON A.PatListID = B.PatListID ");
            selSql.Append(" LEFT JOIN BaseEmployee C ON A.PatDoctorID = C.EmpId ");
            selSql.Append(" LEFT JOIN BaseEmployee D ON A.PatNurseID = D.EmpId ");
            selSql.Append(" LEFT JOIN BaseDept E ON A.PatDeptID = E.DeptId ");
            selSql.Append(" WHERE A.PatListID <> 0 ");
            selSql.Append(" AND A.PatName <> '' ");
            selSql.AppendFormat(" AND A.WardID = {0} ", wardId);
            selSql.Append(" AND EXISTS (SELECT PatListID FROM IP_PatList G WHERE A.BedNO=B.BedNO) ");
            selSql.AppendFormat(" AND A.WorkID = {0} ", oleDb.WorkId);
            return oleDb.GetDataTable(selSql.ToString());
        }

        /// <summary>
        /// 护士站获取病人列表
        /// </summary>
        /// <param name="status">病人状态</param>
        /// <param name="deptId">科室ID</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="isReminder">催款查询标志</param>
        /// <param name="reminderLine">催款线</param>
        /// <returns>病人列表</returns>
        public DataTable GetNursingStationPatList(
            int status, 
            int deptId, 
            DateTime startDate, 
            DateTime endDate, 
            bool isReminder, 
            decimal reminderLine)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT  0 CheckFlg , ");
            strSql.Append(" A.PatListID , ");
            strSql.Append(" A.BedNo , ");
            strSql.Append(" A.SerialNumber , ");
            strSql.Append(" A.Times , ");
            strSql.Append(" A.PatName , ");
            strSql.Append(" A.Sex , ");
            strSql.Append(" CASE A.Sex ");
            strSql.Append(" WHEN '1' THEN '男' ");
            strSql.Append(" WHEN '2' THEN '女' ");
            strSql.Append(" ELSE '未知' ");
            strSql.Append(" END AS SexName , ");
            strSql.Append(" A.Age , ");
            strSql.Append(" CASE SUBSTRING(A.Age, 1, 1) ");
            strSql.Append(" WHEN 'Y' THEN SUBSTRING(A.Age, 2, 3) + '岁' ");
            strSql.Append(" WHEN 'M' THEN SUBSTRING(A.Age, 2, 3) + '月' ");
            strSql.Append(" WHEN 'D' THEN SUBSTRING(A.Age, 2, 3) + '天' ");
            strSql.Append(" WHEN 'H' THEN SUBSTRING(A.Age, 2, 3) + '时' ");
            strSql.Append(" END AS AgeName , ");
            strSql.Append(" A.EnterHDate , ");
            strSql.Append(" A.CurrDoctorID , ");
            strSql.Append(" B.Name AS DoctorName, ");
            strSql.Append(" A.CurrNurseID , ");
            strSql.Append(" C.Name AS NurseName, ");
            strSql.Append(" A.EnterDiseaseName , ");
            strSql.AppendFormat(" [dbo].[fnGetPatDeposit](A.PatListID, {0}) AS SumDeposit , ", oleDb.WorkId);
            strSql.AppendFormat(" [dbo].[fnGetPatFee](A.PatListID, {0}) AS SumFee, ", oleDb.WorkId);
            strSql.AppendFormat(" [dbo].[fnGetBalance](A.PatListID, {0}) AS Balance, ", oleDb.WorkId);
            strSql.Append(" CASE A.Status WHEN 1 THEN '新入院' WHEN 2 THEN '在院' WHEN 3 THEN '出院未结算' WHEN 4 THEN '出院' END AS PatStatus ");
            strSql.Append(" FROM    IP_PatList A ");
            strSql.Append(" LEFT JOIN BaseEmployee B ON A.CurrDoctorID =B.EmpId ");
            strSql.Append(" LEFT JOIN BaseEmployee C ON A.CurrNurseID =C.EmpId ");
            strSql.Append(" WHERE ");
            strSql.AppendFormat(" A.CurrDeptID = {0} ", deptId);
            if (startDate != DateTime.MinValue && endDate != DateTime.MinValue)
            {
                string sTime = startDate.ToString("yyyy-MM-dd") + " 00:00:00";
                string eTime = endDate.ToString("yyyy-MM-dd") + " 23:59:00";
                strSql.AppendFormat(" AND A.LeaveHDate BETWEEN '{0}' AND '{1}' ", sTime, eTime);
            }

            strSql.AppendFormat(" AND A.Status = {0} ", status);
            // 是否只查询可催款病人
            if (isReminder)
            {
                strSql.AppendFormat(" AND [dbo].[fnGetBalance](A.PatListID, 1) < {0} AND [dbo].[fnGetPatFee](A.PatListID, {1}) > 0 ", reminderLine, oleDb.WorkId);
            }

            strSql.Append(" ORDER BY PatListID ");

            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 获取病人最新预交金总额以及未结算费用总额
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人最新预交金总额以及未结算费用总额</returns>
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
        /// 获取待打印催款单数据
        /// </summary>
        /// <param name="patListID">病人住院登记ID列表</param>
        /// <param name="reminderMoney">继续交款金额</param>
        /// <returns>待打印催款单数据列表</returns>
        public DataTable GetReminderDataList(string patListID, decimal reminderMoney)
        {
            string strSql = @"SELECT A.PatListID, A.PatName ,A.SerialNumber,A.BedNo,B.Name AS DeptName,
                            [dbo].[fnGetPatDeposit](A.PatListID, A.WorkID) DepositFee ,
                            [dbo].[fnGetPatFee](A.PatListID, A.WorkID) AS SumFee ,
		                    GETDATE() AS ReminderData,GETDATE() AS PrintReminderData ,{0} AS ReminderMoney
                            FROM    IP_PatList A LEFT JOIN BaseDept B 
                            ON A.CurrDeptID=B.DeptId AND B.WorkId = A.WorkID WHERE  A.WorkID = {2} AND A.PatListID IN ({1})";
            strSql = string.Format(strSql, reminderMoney, patListID, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 定义出院检查病人是否存在出院医嘱，并且已经转抄
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <returns>检查结果</returns>
        public DataTable IsExistenceDischargeOrder(int patListId)
        {
            string strSql = string.Format(@"SELECT IsLeaveHosOrder,LeaveHDate FROM IP_PatList WHERE PatListID = {0} AND Status = 2", patListId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 检查病人是否存在有效转科医嘱
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <returns>医嘱信息</returns>
        public DataTable CheckPatTransDept(int patListId)
        {
            string strSql = string.Format(
                @"SELECT ID,TransDate,NewDeptID FROM IPD_TransDept WHERE PatListID={0} 
                            AND FinishFlag = 0 AND CancelFlag = 0", 
                patListId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 检查床位是否已被占用
        /// </summary>
        /// <param name="bedId">床位ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        /// <returns>>true：空床/false：被占用</returns>
        public bool IsBedOccupy(int bedId, int wardId, string bedNo)
        {
            string sql = string.Empty;
            if (bedId != 0)
            {
                sql = string.Format(" SELECT BedID FROM IP_BedInfo WHERE IsUsed=0 AND IsStoped=0 AND BedID={0} ", bedId);
            }
            else
            {
                sql = string.Format(
                    @" SELECT BedID FROM IP_BedInfo WHERE IsUsed=0 AND IsStoped=0 AND 
                        WardID={0} AND BedNO = '{1}' AND WorkID = {2} ", 
                        wardId, 
                        bedNo, 
                        oleDb.WorkId);
            }

            return oleDb.GetDataTable(sql).Rows.Count > 0;
        }

        /// <summary>
        /// 出院召回的病人修改出院医嘱状态
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        public void UpdatePatOrder(int patListId)
        {
            string strSql = string.Format(
                @"UPDATE IPD_OrderRecord SET OrderStatus = 1 
                    WHERE PatListID = {0} AND OrderType=5", 
                patListId);
            oleDb.DoCommand(strSql);
        }

        /// <summary>
        /// 修改入院病人床位分配信息
        /// </summary>
        /// <param name="bedNo">床位号</param>
        /// <param name="doctorId">医生ID</param>
        /// <param name="nurseId">护士ID</param>
        /// <param name="patListId">病人登记ID</param>
        /// <returns>修改成功或失败</returns>
        public bool UpdatePatList(string bedNo, int doctorId, int nurseId, int patListId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" UPDATE IP_PatList SET ");
            strSql.Append(" IsLeaveHosOrder = 0, ");
            strSql.Append(" Status=2, ");
            strSql.Append(" EnterHDate=GETDATE(), ");
            strSql.AppendFormat(" BedNo='{0}', ", bedNo);
            strSql.AppendFormat(" CurrDoctorID={0}, ", doctorId);
            strSql.AppendFormat(" CurrNurseID={0} ", nurseId);
            strSql.AppendFormat(" WHERE PatListID={0} ", patListId);
            return oleDb.DoCommand(strSql.ToString()) > 0;
        }

        /// <summary>
        /// 修改床位分配信息
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="patName">病人名</param>
        /// <param name="sex">性别</param>
        /// <param name="deptId">科室ID</param>
        /// <param name="doctorId">医生Id</param>
        /// <param name="nurseId">护士ID</param>
        /// <param name="bedId">床位ID</param>
        /// <returns>修改成功或失败</returns>
        public bool UpdatePatBedInfo(
            int patListID, 
            string patName, 
            string sex, 
            int deptId, 
            int doctorId, 
            int nurseId, 
            int bedId)
        {
            string updSql = @"UPDATE IP_BedInfo SET IsUsed=1,IsPack=0,PatListID={0},PatName='{1}',
                        PatSex='{2}',PatDeptID={3},PatDoctorID={4},PatNurseID={5} WHERE BedID={6}";
            updSql = string.Format(updSql, patListID, patName, sex, deptId, doctorId, nurseId, bedId);
            return oleDb.DoCommand(updSql) > 0;
        }

        /// <summary>
        /// 根据病人ID查询病人的床位费用
        /// </summary>
        /// <param name="bedID">床位ID</param>
        /// <param name="feeType">费用类型</param>
        /// <returns>床位费信息</returns>
        public DataTable GetBedFeeItemList(int bedID, int feeType)
        {
            string strSql = @"SELECT A.ItemAmount,B.* FROM IP_BedFee A
                            LEFT JOIN ViewFeeItem_SimpleList B
                            ON A.ItemID = B.ItemID
                            WHERE A.BedID ={0} AND A.WorkID={1} AND A.FeeType={2} and b.itemname is not null";
            strSql = string.Format(strSql, bedID, oleDb.WorkId, feeType);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 取消分床检查病人是否已产生费用
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>true：已产生/false：未产生</returns>
        public bool CheckPatIsCostIncurred(int patListID)
        {
            string strSql = @"SELECT PatListID FROM IP_PatList A 
                            WHERE EXISTS(SELECT B.PatListID FROM IP_DepositList B 
                            WHERE A.PatListID=B.PatListID  AND A.PatListID={0} AND B.WorkID={1})
                            OR EXISTS(SELECT C.PatListID FROM IP_FeeItemRecord C 
                            WHERE A.PatListID=C.PatListID AND A.PatListID={0} AND C.WorkID={1} ) AND A.WorkID = {1}";
            strSql = string.Format(strSql, patListID, oleDb.WorkId);
            return oleDb.GetDataTable(strSql).Rows.Count > 0;
        }

        /// <summary>
        /// 取消分床修改床位信息
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        /// <returns>true：修改成功</returns>
        public bool CancelBedUpdIpBedInfo(int wardId, string bedNo)
        {
            string updSql = @"UPDATE IP_BedInfo SET IsPack=0,IsUsed=0,PatListID=0,PatName='',PatSex='',
                            PatDeptID=0,PatDoctorID=0,PatNurseID=0 WHERE WardID={0} AND BedNo='{1}' AND WorkID = {2}";
            updSql = string.Format(updSql, wardId, bedNo, oleDb.WorkId);
            return oleDb.DoCommand(updSql) > 0;
        }

        /// <summary>
        /// 取消分床修改病人登记信息
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <returns>true：修改成功</returns>
        public bool CancelBedUpdIpPatList(int patListId)
        {
            string updSql = @"UPDATE IP_PatList SET Status = 1,BedNo='',
                            CurrDoctorID=EnterDoctorID,CurrNurseID=EnterNurseID WHERE PatListID={0}";
            updSql = string.Format(updSql, patListId);
            return oleDb.DoCommand(updSql) > 0;
        }

        /// <summary>
        /// 获取病床ID
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        /// <returns>病床信息</returns>
        public DataTable GetBedInfoId(int wardId, string bedNo)
        {
            string strSql = @"SELECT BedID,PatListID FROM IP_BedInfo WHERE WardID={0} AND BedNO='{1}' AND WorkID={2}";
            strSql = string.Format(strSql, wardId, bedNo, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 取消床位分配/转科/出院/取消包床停用床位账单数据
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <param name="bedId">床位ID</param>
        public void StopBedFee(int patListId, int bedId)
        {
            string strSql = string.Format(
                @"UPDATE IP_FeeItemGenerate SET IsStop = 1 
                            WHERE PatListID={0} AND BedID={1} ", 
                            patListId, 
                            bedId);
            oleDb.DoCommand(strSql);
        }

        /// <summary>
        /// 检查当前病床是否为被包床床位
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        /// <returns>床位信息</returns>
        public DataTable CheckBedIsPack(int patListId, int wardId, string bedNo)
        {
            string strSql = @"SELECT BedID,IsPack FROM IP_BedInfo WHERE WardID={0} 
                            AND BedNO='{1}' AND PatListID={2} AND WorkID={3}";
            strSql = string.Format(strSql, wardId, bedNo, patListId, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 病人换床--更新新床位的数据
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <param name="newBedNo">新床位号</param>
        /// <param name="oldBedNo">旧床位号</param>
        /// <param name="isPackBed">是否为加床</param>
        /// <returns>true：保存成功</returns>
        public bool UpdateNewBed(int wardId, string newBedNo, string oldBedNo, int isPackBed)
        {
            StringBuilder updSql = new StringBuilder();
            updSql.Append(" UPDATE  IP_BedInfo ");
            updSql.Append(" SET IP_BedInfo.PatListID = B.PatListID , ");
            updSql.Append(" IP_BedInfo.PatName = B.PatName, ");
            updSql.Append(" IP_BedInfo.PatSex = B.PatSex , ");
            updSql.Append(" IP_BedInfo.PatDeptID = B.PatDeptID , ");
            updSql.Append(" IP_BedInfo.PatDoctorID = B.PatDoctorID , ");
            updSql.Append(" IP_BedInfo.PatNurseID = B.PatNurseID , ");
            updSql.Append(" IP_BedInfo.IsUsed = 1 , ");
            updSql.AppendFormat(" IP_BedInfo.IsPack = {0} , ", isPackBed);
            updSql.Append(" IP_BedInfo.BabyID = B.BabyID  ");
            updSql.Append(" FROM IP_BedInfo ");
            updSql.AppendFormat(" LEFT JOIN IP_BedInfo B ON B.WardID = {0} ", wardId);
            updSql.AppendFormat(" AND B.BedNO = '{0}' ", oldBedNo);
            updSql.AppendFormat(" WHERE IP_BedInfo.WardID = {0} ", wardId);
            updSql.AppendFormat(" AND IP_BedInfo.BedNO = '{0}' ", newBedNo);
            updSql.AppendFormat(" AND IP_BedInfo.WorkID = {0} ", oleDb.WorkId);
            return oleDb.DoCommand(updSql.ToString()) > 0;
        }

        /// <summary>
        /// 换床--记录新病床分配日志
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        /// <param name="newBedNo">新床位号</param>
        /// <param name="empId">操作员Id</param>
        /// <param name="workID">机构ID</param>
        /// <returns>true:保存成功</returns>
        public bool SaveNewBedLogInfo(int wardId, string bedNo, string newBedNo, int empId, int workID)
        {
            StringBuilder updSql = new StringBuilder();
            updSql.Append(" INSERT INTO IP_BedLog ");
            updSql.Append(" (BedID ");
            updSql.Append(" ,BedNo ");
            updSql.Append(" ,DeptID ");
            updSql.Append(" ,WardID ");
            updSql.Append(" ,PatListID ");
            updSql.Append(" ,PatName ");
            updSql.Append(" ,PatSex ");
            updSql.Append(" ,PatDeptID ");
            updSql.Append(" ,PatDoctorID ");
            updSql.Append(" ,PatNurseID ");
            updSql.Append(" ,BabyID ");
            updSql.Append(" ,AssignDate ");
            updSql.Append(" ,AssignEmpID ");
            updSql.Append(" ,WorkID) ");
            updSql.Append(" SELECT  ");
            updSql.Append(" BedID ");
            updSql.AppendFormat(" ,'{0}' ", newBedNo);
            updSql.Append(" ,0 ");
            updSql.AppendFormat(" ,{0} ", wardId);
            updSql.Append(" ,PatListID ");
            updSql.Append(" ,PatName ");
            updSql.Append(" ,PatSex ");
            updSql.Append(" ,PatDeptID ");
            updSql.Append(" ,PatDoctorID ");
            updSql.Append(" ,PatNurseID ");
            updSql.Append(" ,BabyID ");
            updSql.Append(" ,GETDATE() ");
            updSql.AppendFormat(" ,{0} ", empId);
            updSql.AppendFormat(" ,{0} ", workID);
            updSql.Append(" FROM IP_BedInfo ");
            updSql.AppendFormat(" WHERE WardID={0} AND BedNo='{1}' AND WorkID = {2} ", wardId, bedNo, oleDb.WorkId);

            return oleDb.DoCommand(updSql.ToString()) > 0;
        }

        /// <summary>
        /// 病人换床--修改病人登记床位数据
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <param name="bedNo">床位号</param>
        /// <returns>true：保存成功</returns>
        public bool PatBedChanging(int patListId, string bedNo)
        {
            string updSql = string.Format("UPDATE IP_PatList SET BedNo = '{0}' WHERE PatListID = {1}", bedNo, patListId);
            return oleDb.DoCommand(updSql) > 0;
        }

        /// <summary>
        /// 更换医生或护士保存病人登记信息
        /// </summary>
        /// <param name="patListId">病人登记信息</param>
        /// <param name="doctorid">医生ID</param>
        /// <param name="nurseId">护士Id</param>
        /// <returns>true：修改成功</returns>
        public bool UpdatePatListDoctor(int patListId, int doctorid, int nurseId)
        {
            string updSql = string.Format(
                @" UPDATE IP_PatList SET CurrDoctorID = {0} ,
                            CurrNurseID = {1} WHERE PatListID = {2}", 
                            doctorid, 
                            nurseId, 
                            patListId);
            return oleDb.DoCommand(updSql) > 0;
        }

        /// <summary>
        /// 获取病人包床的床位列表
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人包床的床位列表</returns>
        public DataTable GetPatPackBedList(int patListID)
        {
            string strSql = string.Format(
                @"SELECT BedID,WardID,BedNO FROM IP_BedInfo 
                            WHERE PatListID={0} AND WorkID = {1}",
                            patListID, 
                            oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 更换医生或护士保存床位信息
        /// </summary>
        /// <param name="doctorid">医生ID</param>
        /// <param name="nurseId">护士Id</param>
        /// <param name="bedId">床位ID</param>
        /// <returns>true：修改成功</returns>
        public bool UpdateBedDoctor(int doctorid, int nurseId, int bedId)
        {
            string updSql = string.Format(
                @" UPDATE IP_BedInfo SET PatDoctorID = {0} ,
                            PatNurseID = {1} WHERE BedID = {2}", 
                            doctorid, 
                            nurseId, 
                            bedId);
            return oleDb.DoCommand(updSql) > 0;
        }

        /// <summary>
        /// 查询病人所有未转抄或未发送医嘱
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>所有未转抄或未发送医嘱</returns>
        public DataTable GetNotExecOrder(int patListID)
        {
            string strSql = string.Format(
                @"SELECT PatListID, 
                            ItemID,ItemName,CAST( Dosage AS INT)AS Amount,'医嘱未转抄/未发送' AS NotType,
                            DosageUnit AS Unit  FROM IPD_OrderRecord WHERE OrderStatus <> 5 
                            AND DeleteFlag = 0 AND PatListID = {0} AND WorkID = {1}", 
                patListID, 
                oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 查询病人所有未停用账单
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人所有未停用账单</returns>
        public DataTable GetNotStopOrder(int patListID)
        {
            string strSql = string.Format(
                @"SELECT A.PatListID,A.ItemID,A.ItemName,A.Amount,'账单未停用' AS NotType ,Unit
                                            FROM IP_FeeItemGenerate A WHERE A.PatListID = {0} 
                                            AND A.OrderType = 2 AND A.FeeSource <> 4 AND A.IsStop = 0", 
                patListID);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 查询所有未统领的账单
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>所有未统领的账单</returns>
        public DataTable GetNotGuideOrder(int patListID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT ");
            strSql.Append(" A.PatListID, ");
            strSql.Append(" A.ItemID, ");
            strSql.Append(" A.ItemName, ");
            strSql.Append(" SUM(A.Amount) AS Amount, ");
            strSql.Append(" '药品未统领' AS NotType, ");
            strSql.Append(" A.Unit ");
            strSql.Append(" FROM ");
            strSql.Append(" V_IP_DrugbillManagement A ");
            strSql.Append(" WHERE ");
            strSql.Append(" A.RecordFlag=0 ");
            strSql.Append(" AND A.DrugFlag = 0 ");
            strSql.AppendFormat(" AND A.PatListID={0} ", patListID);
            strSql.Append(" GROUP BY A.PatListID,A.ItemID,A.ItemName,A.Unit ");
            strSql.Append(" UNION ALL ");
            strSql.Append(" SELECT ");
            strSql.Append(" A.PatListID, ");
            strSql.Append(" A.ItemID, ");
            strSql.Append(" A.ItemName, ");
            strSql.Append(" SUM(A.Amount) AS Amount, ");
            strSql.Append(" '药品未统领' AS NotType, ");
            strSql.Append(" A.Unit ");
            strSql.Append(" FROM ");
            strSql.Append(" V_IP_DrugbillManagement A ");
            strSql.Append(" WHERE RecordFlag=2 ");
            strSql.Append(" AND DrugFlag = 1 ");
            strSql.AppendFormat(" AND A.PatListID={0} ", patListID);
            strSql.Append(" GROUP BY A.PatListID,A.ItemID,A.ItemName,A.Unit ");
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 查询病人所有已统领未发药的药品列表
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人所有已统领未发药的药品列表</returns>
        public DataTable GetNotDispDrugList(int patListID)
        {
            string strSql = string.Format(
                @"SELECT PatListID,ItemID,ItemName,SUM(Amount) AS Amount,
                            '药品未发药' AS NotType ,Unit 
                            FROM IP_DrugBillDetail WHERE PatListID = {0} AND DispDrugFlag = 0 
                            GROUP BY PatListID,ItemID,ItemName,Amount,Unit ", 
                patListID);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 病人定义出院，清空床位信息
        /// </summary>
        /// <param name="wardID">病区ID</param>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>true:清空成功</returns>
        public bool PatOutHospitalUpdateBedData(int wardID, int patListID)
        {
            string strSql = @"UPDATE IP_BedInfo SET PatListID=0, PatName='', PatSex='',IsPack=0,
                            PatDeptID=0,PatDoctorID=0,PatNurseID=0,BabyID=0,
                            IsUsed=0 WHERE WardID = {0} AND PatListID = {1}";
            strSql = string.Format(strSql, wardID, patListID);
            return oleDb.DoCommand(strSql) > 0;
        }

        /// <summary>
        /// 病人定义出院，修改病人登记信息
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>true:修改成功</returns>
        public bool PatOutHospitalUpdatePatListData(int patListID)
        {
            string strSql = string.Format(
                @"UPDATE IP_PatList SET BedNo='',Status=3,LeaveHDate=GETDATE() 
                            WHERE PatListID={0} ", 
                patListID);
            return oleDb.DoCommand(strSql) > 0;
        }

        /// <summary>
        /// 定义出院获取出院通知单基本数据
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>出院通知单基本数据</returns>
        public DataTable GetPatOutHospitalData(int patListID)
        {
            string strSql = @"SELECT  A.PatName ,
                             CASE A.Sex
                               WHEN 1 THEN '男'
                               WHEN 2 THEN '女'
                             END Sex ,
                             A.Age ,
                             A.CurrDeptID ,
                             C.Name ,
                             A.SerialNumber ,
                             A.CaseNumber ,
                             D.Name + ' ' + B.NAddressDetail NAddress,
                             dbo.fnGetPatientDiseaseName(A.PatListID) AS EnterDiseaseName ,
                             E.Name OutSituation,
                             A.LeaveHDate
                             FROM    IP_PatList A
                             LEFT JOIN IP_PatientInfo B ON A.PatListID=B.PatListID
							 LEFT JOIN BaseDictContent D ON B.NAddress=D.Code  AND D.ClassId=1004 AND D.DelFlag=0 
                             LEFT JOIN BaseDept C ON A.CurrDeptID=C.DeptId
							 LEFT JOIN BaseDictContent E ON A.OutSituation=E.Code AND E.ClassId=1017 AND E.DelFlag=0 
                             WHERE A.PatListID={0}";
            strSql = string.Format(strSql, patListID);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 转科更改病人当前科室为新科室
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="currDeptId">新科室ID</param>
        public void UpdatepatCurrDept(int patListID, int currDeptId)
        {
            string strSql = string.Format(
                @"UPDATE IP_PatList SET CurrDeptID = {0} 
                            WHERE PatListID = {1} ", 
                currDeptId, 
                patListID);
            oleDb.DoCommand(strSql);
        }

        #endregion

        #region "住院预交金"

        /// <summary>
        /// 获取病人预交金缴纳记录
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="serialNumber">住院号</param>
        /// <param name="isSettlement">是否是住院结算时查询</param>
        /// <returns>病人预交金缴纳记录</returns>
        public DataTable GetPaymentList(int patListID, decimal serialNumber, bool isSettlement)
        {
            StringBuilder selectSql = new StringBuilder();
            selectSql.Append(" SELECT ");
            selectSql.Append(" DepositID, "); // 预交金ID
            selectSql.Append(" InvoiceNO, "); // 票据号
            selectSql.Append(" PatListID, "); // 病人登记ID
            selectSql.Append(" SerialNumber, "); // 住院号
            selectSql.Append(" MakerDate, "); // 交费日期
            selectSql.Append(" DictContentName, "); // 支付方式
            selectSql.Append(" ROUND(TotalFee,2) AS TotalFee, "); // 金额
            selectSql.Append(" MakerEmpName, "); // 收费员
            selectSql.Append(" ReceivEmpName, "); // 结账人
            selectSql.Append(" AccountDate, "); // 结账日期
            selectSql.Append(" Status, ");  // 记录类型
            selectSql.Append(" States, ");  // 记录类型
            selectSql.Append(" EmpId, ");  // 收费人ID
            selectSql.Append(" MemberID, ");
            selectSql.Append(" DeptID, ");
            selectSql.Append(" OldDepositID, ");
            selectSql.Append(" PayType, ");
            selectSql.Append(" MakerEmpID, ");
            selectSql.Append(" CostHeadID, ");
            selectSql.Append(" AccountID, ");
            selectSql.Append(" PrintTimes, ");
            selectSql.Append(" WorkID ");
            selectSql.Append(" FROM V_IP_PaymentList ");
            selectSql.Append(" WHERE ");
            selectSql.AppendFormat(" PatListID = {0} ", patListID);
            selectSql.AppendFormat(" AND SerialNumber = {0} ", serialNumber);
            selectSql.AppendFormat(" AND WorkID = {0} ", oleDb.WorkId);
            // 住院结算查询预交金时
            if (isSettlement)
            {
                selectSql.Append(" AND CostHeadID = 0 ");
            }

            selectSql.Append(" ORDER BY DepositID, MakerDate ");
            return oleDb.GetDataTable(selectSql.ToString());
        }

        /// <summary>
        /// 根据病人登记ID取得病人入院状态
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人入院状态集合</returns>
        public DataTable GetPatStatus(int patListID)
        {
            string sql = string.Format(
                @"SELECT PatListID,PatName FROM IP_PatList 
                        WHERE PatListID = {0} AND Status <> 9 ", 
                patListID);
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 获取预交金结算记录
        /// </summary>
        /// <param name="depositID">预交金ID</param>
        /// <returns>预交金结算记录</returns>
        public DataTable GetCostHeadInfo(int depositID)
        {
            string strSql = string.Format(
                @"SELECT DepositID FROM IP_DepositList 
                            WHERE DepositID={0} AND Status = 0 AND CostHeadID=0", 
                depositID);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 预交金退费
        /// </summary>
        /// <param name="depositID">预交金ID</param>
        /// <returns>退费成功或失败</returns>
        public bool Refund(int depositID)
        {
            string updSql = string.Format(@"UPDATE IP_DepositList SET Status = 1 WHERE DepositID = {0} ", depositID);
            return oleDb.DoCommand(updSql) > 0;
        }

        /// <summary>
        /// 写入预交金退费记录
        /// </summary>
        /// <param name="depositID">预交金记录ID</param>
        /// <returns>true:保存成功</returns>
        public bool RefundInsert(int depositID)
        {
            StringBuilder insertSql = new StringBuilder();
            insertSql.Append(" INSERT INTO ");
            insertSql.Append(" IP_DepositList ");
            insertSql.Append(" (MemberID ");
            insertSql.Append(" ,PatListID ");
            insertSql.Append(" ,DeptID ");
            insertSql.Append(" ,SerialNumber ");
            insertSql.Append(" ,InvoiceNO ");
            insertSql.Append(" ,OldDepositID ");
            insertSql.Append(" ,PayType ");
            insertSql.Append(" ,TotalFee ");
            insertSql.Append(" ,MakerEmpID ");
            insertSql.Append(" ,MakerDate ");
            insertSql.Append(" ,CostHeadID ");
            insertSql.Append(" ,CostType ");
            insertSql.Append(" ,AccountID ");
            insertSql.Append(" ,Status ");
            insertSql.Append(" ,PrintTimes ");
            insertSql.Append(" ,WorkID) ");
            insertSql.Append(" SELECT ");
            insertSql.Append(" MemberID ");
            insertSql.Append(" ,PatListID ");
            insertSql.Append(" ,DeptID ");
            insertSql.Append(" ,SerialNumber ");
            insertSql.Append(" ,InvoiceNO ");
            insertSql.Append(" ,DepositID ");
            insertSql.Append(" ,PayType ");
            insertSql.Append(" ,TotalFee - TotalFee * 2 ");
            insertSql.Append(" ,MakerEmpID ");
            insertSql.Append(" ,GETDATE() AS MakerDate ");
            insertSql.Append(" ,CostHeadID ");
            insertSql.Append(" ,CostType ");
            insertSql.Append(" ,0 AS AccountID ");
            insertSql.Append(" ,2 AS Status ");
            insertSql.Append(" ,1 AS PrintTimes ");
            insertSql.Append(" ,WorkID ");
            insertSql.Append(" FROM ");
            insertSql.Append(" IP_DepositList ");
            insertSql.AppendFormat(" WHERE DepositID = {0} ", depositID);
            return oleDb.DoCommand(insertSql.ToString()) > 0;
        }

        #endregion

        #region "病床维护"

        /// <summary>
        /// 查询同病区同房间是否存在相同的床位号
        /// </summary>
        /// <param name="wardID">病区ID</param>
        /// <param name="roomNo">房间号</param>
        /// <param name="bedNo">床位号</param>
        /// <returns>是否存在相同床位号--true:不存在/false：存在</returns>
        public bool IsExistenceCheck(int wardID, string roomNo, string bedNo)
        {
            string selectSql = @"SELECT BedID FROM IP_BedInfo WHERE 
                            WardID = {0} AND BedNO = '{1}' AND WorkID = {2}";
            selectSql = string.Format(selectSql, wardID, bedNo, oleDb.WorkId);
            if (oleDb.GetDataTable(selectSql).Rows.Count > 0)
            {
                return false; // 存在相同的床位
            }
            else
            {
                return true; // 不存在相同的床位
            }
        }

        /// <summary>
        /// 修改床位信息时，先删除数据库中现有的床位费用数据
        /// </summary>
        /// <param name="bedID">床位ID</param>
        /// <returns>true：删除成功</returns>
        public bool DeleteBedFreeList(int bedID)
        {
            string delSql = string.Format("DELETE FROM IP_BedFee WHERE BedID = {0}", bedID);
            return oleDb.DoCommand(delSql) > 0;
        }

        /// <summary>
        /// 根据病区ID获取床位列表 
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <returns>床位列表 </returns>
        public DataTable GetBedList(int wardId)
        {
            StringBuilder selectSql = new StringBuilder();
            selectSql.Append(" SELECT  A.BedID , ");
            selectSql.Append(" A.DeptID , ");
            selectSql.Append(" A.WardID , ");
            selectSql.Append(" A.RoomNO , ");
            selectSql.Append(" A.BedNO , ");
            selectSql.Append(" A.BedDoctorID , ");
            selectSql.Append(" A.BedNurseID , ");
            selectSql.Append(" B.Name DoctorName , ");
            selectSql.Append(" C.Name NurseName , ");
            selectSql.Append(" A.IsStoped , ");
            selectSql.Append(" A.IsPlus , ");
            selectSql.Append(" A.IsUsed , ");
            selectSql.Append(" A.IsPack , ");
            selectSql.Append(" A.PatListID , ");
            selectSql.Append(" A.PatName , ");
            selectSql.Append(" A.PatSex , ");
            selectSql.Append(" A.PatDeptID , ");
            selectSql.Append(" A.PatDoctorID , ");
            selectSql.Append(" A.PatNurseID , ");
            selectSql.Append(" A.BabyID , ");
            selectSql.Append(" A.WorkID , ");
            selectSql.Append(" CASE A.IsStoped ");
            selectSql.Append(" WHEN 1 THEN '停用' ");
            selectSql.Append(" ELSE '' ");
            selectSql.Append(" END AS IsStopedName , ");
            selectSql.Append(" CASE A.IsPlus ");
            selectSql.Append(" WHEN 1 THEN '加' ");
            selectSql.Append(" ELSE '' ");
            selectSql.Append(" END AS IsPlusName , ");
            selectSql.Append(" CASE A.IsUsed ");
            selectSql.Append(" WHEN 1 THEN '在用' ");
            selectSql.Append(" ELSE '' ");
            selectSql.Append(" END AS IsUsedName ");
            selectSql.Append(" FROM    IP_BedInfo A ");
            selectSql.AppendFormat(" LEFT JOIN BaseEmployee B ON A.BedDoctorID = B.EmpId ");
            selectSql.AppendFormat(" LEFT JOIN BaseEmployee C ON A.BedNurseID = C.EmpId ");
            selectSql.AppendFormat(" WHERE (A.WardID = {0} OR (-1)={0}) AND A.WorkID = {1} ", wardId, oleDb.WorkId);
            selectSql.Append(" ORDER BY A.RoomNO,A.BedNO ");
            return oleDb.GetDataTable(selectSql.ToString());
        }

        /// <summary>
        /// 根据床位ID获取费用列表
        /// </summary>
        /// <param name="bedID">床位ID</param>
        /// <returns>费用列表</returns>
        public DataTable GetBedFreeList(int bedID)
        {
            string selectSql = string.Format(
                                                @"SELECT  A.ID ,
                                                A.BedID ,
                                                A.FeeClass ,
                                                A.ItemID ,
		                                        B.UnitPrice,
                                                A.ItemUnit ,
                                                A.FeeType ,
                                                CASE A.FeeType
                                                  WHEN 0 THEN '床位费'
                                                  ELSE '包床费'
                                                END AS FeeTypeName ,
                                                A.ItemName ,
                                                A.ItemAmount
                                                FROM    IP_BedFee A
                                                LEFT JOIN ViewFeeItem_SimpleList B
                                                ON A.ItemID = B.ItemID
                                                WHERE   A.BedID = {0}
                                                ORDER BY ID", 
                                                bedID);
            return oleDb.GetDataTable(selectSql);
        }

        /// <summary>
        /// 查询病床是否已分配病人
        /// </summary>
        /// <param name="bedID">病床ID</param>
        /// <returns>true:已分配/false:未分配</returns>
        public bool BedIsUsed(int bedID)
        {
            string selectSql = string.Format(@"SELECT BedNO FROM IP_BedInfo WHERE BedID={0} AND IsUsed=1 AND PatListID<>0", bedID);
            return oleDb.GetDataTable(selectSql).Rows.Count > 0;
        }

        /// <summary>
        /// 停用或启用病床
        /// </summary>
        /// <param name="isStoped">状态</param>
        /// <param name="bedId">病床ID</param>
        /// <returns>true：停用或启用成功</returns>
        public bool StoppedOrEnabledBed(int isStoped, int bedId)
        {
            string updSql = string.Format("UPDATE IP_BedInfo SET IsStoped = {0} WHERE BedID = {1}", isStoped, bedId);
            return oleDb.DoCommand(updSql) > 0;
        }

        #endregion

        #region "账单模板"

        /// <summary>
        /// 查询医嘱账单模板列表
        /// </summary>
        /// <param name="workID">机构ID</param>
        /// <param name="deptId">科室ID</param>
        /// <param name="empId">用户ID</param>
        /// <returns>医嘱账单模板列表</returns>
        public List<IP_FeeItemTemplateHead> GetIPFeeItemTempList(int workID, int deptId, int empId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT  * ");
            strSql.Append(" FROM    IP_FeeItemTemplateHead ");
            strSql.Append(" WHERE   TempLevel = 0 ");
            strSql.AppendFormat("         AND WorkID = {0} ", workID);
            strSql.Append("         AND PTempHeadID <> 0 ");
            strSql.Append(" UNION ALL ");
            strSql.Append(" SELECT  * ");
            strSql.Append(" FROM    IP_FeeItemTemplateHead ");
            strSql.Append(" WHERE   TempLevel = 1 ");
            strSql.AppendFormat("         AND WorkID = {0} ", workID);
            strSql.AppendFormat("         AND CreateDeptID = {0} ", deptId);
            strSql.Append("         AND PTempHeadID <> 0 ");
            strSql.Append(" UNION ALL ");
            strSql.Append(" SELECT  * ");
            strSql.Append(" FROM    IP_FeeItemTemplateHead ");
            strSql.Append(" WHERE   TempLevel = 2 ");
            strSql.AppendFormat("         AND WorkID = {0} ", workID);
            strSql.AppendFormat("         AND CreateEmpID = {0} ", empId);
            strSql.Append("         AND PTempHeadID <> 0 ");
            strSql.Append(" UNION ALL ");
            strSql.Append(" SELECT  * ");
            strSql.Append(" FROM    IP_FeeItemTemplateHead ");
            strSql.Append(" WHERE   PTempHeadID = 0 ");
            strSql.AppendFormat("         AND WorkID = {0} ", workID);
            return oleDb.Query<IP_FeeItemTemplateHead>(strSql.ToString(), string.Empty).ToList();
        }

        /// <summary>
        /// 根据模板ID删除模板明细数据
        /// </summary>
        /// <param name="tempHeadID">模板ID</param>
        public void DelFeeTempDetail(int tempHeadID)
        {
            string strSql = string.Format("DELETE FROM IP_FeeItemTemplateDetail WHERE TempHeadID = {0}", tempHeadID);
            oleDb.DoCommand(strSql);
        }

        /// <summary>
        /// 查询模板明细数据
        /// </summary>
        /// <param name="tempHeadID">模板ID</param>
        /// <returns>模板明细数据列表</returns>
        public DataTable GetFeeTempDetails(int tempHeadID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT A.TempDetailID ");
            strSql.Append(" ,A.TempHeadID ");
            strSql.Append(" ,A.ItemClass ");
            strSql.Append(" ,A.ItemID ");
            strSql.Append(" ,A.ItemName ");
            strSql.Append(" ,A.ItemAmount ");
            strSql.Append(" ,A.ItemUnit ");
            strSql.Append(" ,A.ExecDeptID ");
            strSql.Append(" ,A.DelFlag ");
            strSql.Append(" ,A.WorkID ");
            strSql.Append(" ,B.ItemCode ");
            strSql.Append(" ,B.ItemClassName ");
            strSql.Append(" ,B.UnitPrice ");
            strSql.Append(" FROM IP_FeeItemTemplateDetail A ");
            strSql.Append(" LEFT JOIN ViewFeeItem_SimpleList B ");
            strSql.Append(" ON A.ItemID=B.ItemID ");
            strSql.AppendFormat(" WHERE A.TempHeadID = {0} ", tempHeadID);
            strSql.AppendFormat(" AND A.WorkID = {0}", oleDb.WorkId);
            return oleDb.GetDataTable(strSql.ToString());
        }

        #endregion

        #region "账单管理"

        /// <summary>
        /// 账单管理--查询在床病人列表
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="param">检索条件</param>
        /// <returns>病人列表</returns>
        public DataTable GetPatientList(int deptId, string param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT  A.PatListID , ");
            strSql.Append(" A.PatName , ");
            strSql.Append(" A.SerialNumber , ");
            strSql.Append(" A.BedNo , ");
            strSql.Append(" CASE A.Sex");
            strSql.Append(" WHEN '1' THEN '男' ");
            strSql.Append(" WHEN '2' THEN '女' ");
            strSql.Append(" ELSE '' ");
            strSql.Append(" END AS SexName , ");
            strSql.Append(" A.Age, ");
            strSql.Append(" A.BedNo, ");
            strSql.Append(" A.PatTypeID, ");
            strSql.Append(" C.PatTypeName, ");
            strSql.Append(" A.CurrDeptID , ");
            strSql.Append(" B.Name AS DeptName , ");
            strSql.Append(" A.CurrDoctorID , ");
            strSql.Append(" D.Name AS DoctorName , ");
            strSql.Append(" A.CurrNurseID ");
            strSql.Append(" FROM IP_PatList A ");
            strSql.Append(" LEFT JOIN BaseDept B ON A.CurrDeptID = B.DeptId ");
            strSql.Append(" LEFT JOIN Basic_PatType C ON A.PatTypeID=C.PatTypeID ");
            strSql.Append(" LEFT JOIN BaseEmployee D ON A.CurrDoctorID = D.EmpId  ");
            strSql.AppendFormat(" WHERE (A.CurrDeptID = {0} OR (-1)={0}) ", deptId);
            strSql.Append(" AND A.Status = 2");
            if (!string.IsNullOrEmpty(param))
            {
                strSql.AppendFormat(" AND  (A.SerialNumber LIKE '%{0}%' OR A.PatName LIKE '%{0}%' ) ", param);
            }

            strSql.AppendFormat(" AND A.WorkID={0}", oleDb.WorkId);
            strSql.Append(" ORDER BY A.SerialNumber ");
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 获取病人累计缴费金额
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人累计缴费金额</returns>
        public DataTable GetPatSumPay(int patListID)
        {
            string strSql = @"SELECT CASE WHEN SUM(TotalFee) >0 
                            THEN SUM(TotalFee) ELSE 0 END AS TotalFee 
                            FROM IP_DepositList WHERE Status = 0 AND PatListID={0} AND CostHeadID=0 AND WorkID = {1} ";
            strSql = string.Format(strSql, patListID, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取病人已记账的长期和临时账单金额
        /// </summary>
        /// <param name="patListID">病人入院登记ID</param>
        /// <returns>病人已记账的长期和临时账单金额</returns>
        public DataTable GetPatLongOrderSumPay(int patListID)
        {
            string strSql = @"SELECT CASE WHEN SUM(TotalFee) >0 THEN SUM(TotalFee) ELSE 0 END TotalFee FROM 
                        IP_FeeItemRecord WHERE PatListID={0} AND OrderType=2 AND RecordFlag=0 AND CostHeadID=0 AND WorkID = {1}
                        UNION ALL
                        SELECT CASE WHEN SUM(TotalFee) >0 THEN SUM(TotalFee) ELSE 0 END TotalFee FROM 
                        IP_FeeItemRecord WHERE PatListID={0} AND OrderType=3 AND RecordFlag=0 AND CostHeadID=0 AND WorkID = {1}
                        UNION ALL
                        SELECT CASE WHEN SUM(TotalFee) >0 THEN SUM(TotalFee) ELSE 0 END TotalFee FROM 
                        IP_FeeItemRecord WHERE PatListID={0} AND OrderType=4 AND RecordFlag=0 AND CostHeadID=0 AND WorkID = {1}";
            strSql = string.Format(strSql, patListID, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
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
            strSql.Append(" C.ExecDeptName , ");
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
            strSql.Append(" A.IsStop , ");
            strSql.Append(" 0 AS IsUpdate , ");
            strSql.Append(" A.WorkID ");
            strSql.Append(" FROM IP_FeeItemGenerate A ");
            strSql.Append(" LEFT JOIN IP_FeeItemRelationship B ON A.GenerateID = B.GenerateID ");
            strSql.Append(" LEFT JOIN ViewFeeItem_SimpleList C ON A.ItemID = C.ItemID ");
            strSql.Append(" AND A.ExecDeptDoctorID = C.ExecDeptId ");
            strSql.Append(" AND A.FeeClass = C.ItemClass ");
            strSql.Append(" AND A.StatID = C.StatID ");
            strSql.Append(" LEFT JOIN BaseEmployee D ON A.MarkEmpID = D.EmpId ");
            strSql.Append(" WHERE ");
            strSql.AppendFormat(" A.PatListID = {0} AND (A.OrderType = {1} or ({1}=-1 and A.OrderType in (2,3))) ", patListID, orderType);
            strSql.Append(" AND A.IsStop <> 1");
            strSql.Append(" AND A.FeeSource <> 4 "); 
            strSql.AppendFormat(" AND A.WorkID = {0} ", oleDb.WorkId);
            strSql.Append(" ORDER BY MarkDate ");
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 检查费用记录是否已记账
        /// </summary>
        /// <param name="generateId">费用ID</param>
        /// <returns>true:已记账</returns>
        public bool IsFeeCharge(int generateId)
        {
            string strSql = string.Format(
                @"SELECT GenerateID FROM IP_FeeItemRelationship 
                            WHERE GenerateID = {0} AND WorkID = {1} ", 
                generateId, 
                oleDb.WorkId);
            return oleDb.GetDataTable(strSql).Rows.Count > 0;
        }

        /// <summary>
        /// 取得所有未停用的账单模板
        /// </summary>
        /// <param name="workID">机构ID</param>
        /// <param name="deptId">科室ID</param>
        /// <param name="empId">操作员ID</param>
        /// <returns>所有未停用的账单模板</returns>
        public List<IP_FeeItemTemplateHead> GetFeeItemTempList(int workID, int deptId, int empId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT  * ");
            strSql.Append(" FROM    IP_FeeItemTemplateHead ");
            strSql.Append(" WHERE   TempLevel = 0 ");
            strSql.AppendFormat("         AND WorkID = {0} ", workID);
            strSql.Append("         AND PTempHeadID <> 0 AND DelFlag = 0 ");
            strSql.Append(" UNION ALL ");
            strSql.Append(" SELECT  * ");
            strSql.Append(" FROM    IP_FeeItemTemplateHead ");
            strSql.Append(" WHERE   TempLevel = 1 ");
            strSql.AppendFormat("         AND WorkID = {0} ", workID);
            strSql.AppendFormat("         AND CreateDeptID = {0} ", deptId);
            strSql.Append("         AND PTempHeadID <> 0  AND DelFlag = 0 ");
            strSql.Append(" UNION ALL ");
            strSql.Append(" SELECT  * ");
            strSql.Append(" FROM    IP_FeeItemTemplateHead ");
            strSql.Append(" WHERE   TempLevel = 2 ");
            strSql.AppendFormat("         AND WorkID = {0} ", workID);
            strSql.AppendFormat("         AND CreateEmpID = {0} ", empId);
            strSql.Append("         AND PTempHeadID <> 0 AND DelFlag = 0 ");
            strSql.Append(" UNION ALL ");
            strSql.Append(" SELECT  * ");
            strSql.Append(" FROM    IP_FeeItemTemplateHead ");
            strSql.Append(" WHERE   PTempHeadID = 0 ");
            strSql.AppendFormat("         AND WorkID = {0} ", workID);
            return oleDb.Query<IP_FeeItemTemplateHead>(strSql.ToString(), string.Empty).ToList();
        }

        /// <summary>
        /// 根据模板ID查询模板对应的账单明细数据
        /// </summary>
        /// <param name="tempHeadID">模板ID</param>
        /// <returns>模板明细数据列表</returns>
        public DataTable GetFeeItemTempDetails(int tempHeadID)
        {
            string strSql = string.Format(
                @"SELECT A.ItemAmount,B.* FROM IP_FeeItemTemplateDetail A 
                        LEFT JOIN ViewFeeItem_SimpleList B ON A.ItemID = 
                        B.ItemID WHERE A.TempHeadID = {0} AND A.WorkID = {1} ",
                tempHeadID, 
                oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 停用长期账单
        /// </summary>
        /// <param name="generateID">账单ID</param>
        public void StopFeeLongOrderData(int generateID)
        {
            string strSql = string.Format("UPDATE IP_FeeItemGenerate SET IsStop = 1 WHERE GenerateID={0}", generateID);
            oleDb.DoCommand(strSql);
        }

        /// <summary>
        /// 根据组合项目ItemID获取组合项目明细列表
        /// </summary>
        /// <param name="itemId">组合项目ID</param>
        /// <returns>组合项目明细列表</returns>
        public DataTable CombinationProjectDetails(int itemId)
        {
            string strSql = string.Format(
                @"SELECT * FROM ViewFeeItem_SimpleList 
                            WHERE ItemID IN(SELECT ItemID FROM Basic_ExamItemFee 
                            WHERE ExamItemID = {0} AND WorkID = {1}) 
                            AND WorkID={1}", 
                itemId,
                oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 检查是否存在重复账单
        /// </summary>
        /// <param name="generateID">费用生成ID</param>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="chargeDate">处方日期</param>
        /// <returns>true:不存在</returns>
        public bool IsExistenceItemAccountingData(int generateID, int patListID, DateTime chargeDate)
        {
            string strSql = string.Format(
                @"SELECT ID FROM IP_FeeItemRelationship WHERE GenerateID={0} AND PatListID = {2}
                            AND CONVERT(varchar(100), ChargeDate, 23)='{1}' AND FeeSource = 0 
                            AND WorkID = {3} ", 
                generateID, 
                chargeDate.ToString("yyyy-MM-dd"), 
                patListID, 
                oleDb.WorkId);
            return oleDb.GetDataTable(strSql).Rows.Count <= 0;
        }

        /// <summary>
        /// 检查床位费是否存在重复计费
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="chargeDate">处方日期</param>
        /// <returns>true:不存在</returns>
        public bool IsExistenceBedFeeData(int patListID, DateTime chargeDate)
        {
            string strSql = string.Format(
                @"SELECT ID FROM IP_FeeItemRelationship 
                            WHERE CONVERT(varchar(100), ChargeDate, 23)='{0}' 
                                        AND PatListID = {1} AND FeeSource=2 AND WorkID = {2} ", 
                                        chargeDate.ToString("yyyy-MM-dd"), 
                                        patListID, 
                                        oleDb.WorkId);
            return oleDb.GetDataTable(strSql).Rows.Count <= 0;
        }

        /// <summary>
        /// 查询病人所有已记账的费用列表
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="orderType">账单类型</param>
        /// <param name="itemID">项目ID</param>
        /// <param name="startTime">处方开始时间</param>
        /// <param name="endTime">处方结束时间</param>
        /// <returns>已记账的费用列表</returns>
        public DataTable GetCostList(int patListID, int orderType, int itemID, DateTime startTime, DateTime endTime)
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
            strSql.Append(" WHERE   CostHeadID = 0 ");
            strSql.AppendFormat(" AND PatListID = {0} ", patListID);
            // 处方日期
            if (startTime != DateTime.MinValue && endTime != DateTime.MinValue)
            {
                string sTime = startTime.ToString("yyyy-MM-dd") + " 00:00:00";
                string eTime = endTime.ToString("yyyy-MM-dd") + " 23:59:00";
                strSql.AppendFormat(" AND ChargeDate BETWEEN '{0}' AND '{1}' ", sTime, eTime);
            }

            // 账单类型
            if (orderType == -1)
            {
                strSql.Append(" AND OrderType IN (2,3,4) ");
            }
            else
            {
                strSql.AppendFormat(" AND OrderType = {0} ", orderType);
            }

            // 项目ID
            if (itemID > 0)
            {
                strSql.AppendFormat(" AND ItemID= {0} ", itemID);
            }

            strSql.Append(" ORDER BY ChargeDate ");
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 检查药品是否已发药、检查项目是否已做检查
        /// </summary>
        /// <param name="feeRecordID">费用明细ID</param>
        /// <returns>true：未发药/false：已发药</returns>
        public bool CheckIsMedicine(int feeRecordID)
        {
            string strSql = string.Format(
                @"SELECT FeeRecordID FROM IP_FeeItemRecord 
                            WHERE FeeRecordID={0} AND DrugFlag=0", 
                feeRecordID);
            return oleDb.GetDataTable(strSql).Rows.Count > 0;
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
        /// 按费用类型统计费用
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="costHeadID">结算ID</param>
        /// <returns>统计费用信息</returns>
        public DataTable StatisticsFeeByFeeType(int patListID, int costHeadID)
        {
            string strSql = @"SELECT  D.SubName ,
                            MIN(A.PatListID) PatListID ,
                            MIN(A.ItemID) ItemID ,
                            MIN(A.StatID) StatID ,
                            SUM(A.TotalFee) TotalFee
                            FROM    IP_FeeItemRecord A
                            LEFT JOIN ( SELECT  A.* ,
                            B.SubID ,
                            B.SubType ,
                            B.SubName
                            FROM    Basic_StatItem A
                            LEFT JOIN Basic_StatItemSubclass B ON A.InFpItemID = B.SubID
                            WHERE   B.SubType = 6 AND A.WorkID = {2}
                            ) D ON A.StatID = D.StatID
                            WHERE A.PatListID={0} AND A.CostHeadID={1} AND A.WorkID={2}
                            GROUP BY D.SubName";
            strSql = string.Format(strSql, patListID, costHeadID, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 取得病人关联的所有床位
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人关联床位列表</returns>
        public DataTable GetPatientBedList(int patListID)
        {
            string strSql = string.Format("SELECT BedID,IsPack FROM IP_BedInfo WHERE PatListID={0} AND WorkID = {1}", patListID, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取记账开始时间以及记账天数
        /// </summary>
        /// <param name="generateIdList">费用生成ID列表</param>
        /// <param name="endTime">记账结束时间</param>
        /// <param name="isBedFee">true:床位费/false:账单费用</param>
        /// <param name="patListId">病人登记ID</param>
        /// <returns>记账信息</returns>
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
                strSql.AppendFormat(" WHERE   A.GenerateID IN({0}) ", generateIdList);
            }

            strSql.Append(" GROUP BY A.GenerateID,A.PatListID ");
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 账单记账获取费用生成数据并且更新价格
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <param name="generateID">账单Id</param>
        /// <returns>最新费用数据</returns>
        public DataTable GetFeeItemGenerateData(int patListId, int generateID)
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
            strSql.Append(" A.Spec , ");
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
            //strSql.Append(" AND A.FeeSource = 3 ");
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 临时账单记账后直接停用
        /// </summary>
        /// <param name="generateID">费用生成ID</param>
        public void StopTempFeeItemGenerate(int generateID)
        {
            string strSql = string.Format("UPDATE IP_FeeItemGenerate SET IsStop = 1 WHERE GenerateID = {0}", generateID);
            oleDb.DoCommand(strSql);
        }

        #endregion

        #region "药品统领"

        /// <summary>
        /// 根据科室查询药品统领列表
        /// </summary>
        /// <param name="patDeptID">入院科室</param>
        /// <param name="execDeptID">执行科室</param>
        /// <param name="commandStatus">true:统药/false:退药</param>
        /// <returns>药品统领列表</returns>
        public DataTable GetCommandSheetList(int patDeptID, int execDeptID, bool commandStatus)
        {
            string strSql = string.Empty;
            if (commandStatus)
            {
                strSql = @"SELECT B.CenteDrugID,* FROM V_IP_DrugbillManagement A LEFT JOIN DG_HospMakerDic B ON A.ItemId=B.DrugID 
                            WHERE RecordFlag=0 AND DrugFlag = 0 AND PatDeptID={0} AND ExecDeptID={1}";
                strSql = string.Format(strSql, patDeptID, execDeptID);
            }
            else
            {
                strSql = @"SELECT B.CenteDrugID,* FROM V_IP_DrugbillManagement A LEFT JOIN DG_HospMakerDic B ON A.ItemId=B.DrugID 
                            WHERE RecordFlag IN (2,9) AND DrugFlag = 1 AND PatDeptID={0} AND ExecDeptID={1}";
                strSql = string.Format(strSql, patDeptID, execDeptID);
            }

            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 根据处方明细ID查询处方是否已生成统领单
        /// </summary>
        /// <param name="feeRecordID">处方明细ID</param>
        /// <returns>true:已生成</returns>
        public bool CheckIsGenerateDrugBillDetail(int feeRecordID)
        {
            string strSql = string.Format("SELECT BillDetailID FROM IP_DrugBillDetail WHERE FeeRecordID={0}", feeRecordID);
            return oleDb.GetDataTable(strSql).Rows.Count > 0;
        }

        /// <summary>
        /// 根据统领单明细ID删除统领单明细
        /// </summary>
        /// <param name="feeRecordID">统领单明细ID</param>
        /// <returns>true:删除成功</returns>
        public bool DelDrugBillDetail(int feeRecordID)
        {
            string strSql = string.Format("DELETE FROM  IP_DrugBillDetail WHERE FeeRecordID={0}", feeRecordID);
            return oleDb.DoCommand(strSql) > 0;
        }

        /// <summary>
        /// 取得统领单类型列表
        /// </summary>
        /// <param name="isBillRule">统领类型（true：退药/false：发药）</param>
        /// <returns>统领单类型列表</returns>
        public DataTable GetDrugBillTypeList(bool isBillRule)
        {
            string strSql = string.Empty;
            if (!isBillRule)
            {
                strSql = "SELECT * FROM IP_DrugBillType ORDER BY SortOrder";
            }
            else
            {
                strSql = "SELECT * FROM IP_DrugBillType WHERE BillRule='IsBackThere' ORDER BY SortOrder";
            }

            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取所有已发送统领的住院病人列表
        /// </summary>
        /// <returns>所有已发送统领的住院病人列表</returns>
        public DataTable GetHasBeenSentDrugbillPatList()
        {
            string strSql = @"SELECT  A.PatListID ,
                            A.PatName ,
                            A.SerialNumber ,
                            A.BedNo ,
                            A.PYCode ,
                            A.WBCode ,
		                    B.Name DeptName,
                            A.CurrDeptID
                            FROM    IP_PatList A
                            LEFT JOIN BaseDept B ON A.CurrDeptID=B.DeptId
                            WHERE   Status = 2";
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 查询统领单列表
        /// </summary>
        /// <param name="deptId">入院科室ID</param>
        /// <param name="orderStatus">单据状态</param>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="startDate">统领发送开始时间</param>
        /// <param name="endDate">统领发送结束时间</param>
        /// <returns>统领单列表</returns>
        public DataTable GetDrugbillOrderList(
            int deptId, 
            bool orderStatus, 
            int patListID, 
            DateTime startDate, 
            DateTime endDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT A.BillHeadID , ");
            strSql.Append(" MIN(A.MakeDate) MakeDate , ");
            strSql.Append(" MIN(C.BillTypeName) BillTypeName , ");
            strSql.Append(" MIN(B.DispDrugFlag) DispDrugFlag , ");
            strSql.Append(" MIN(B.PatListID) PatListID ");
            strSql.Append(" FROM    IP_DrugBillHead A ");
            strSql.Append(" LEFT JOIN IP_DrugBillDetail B ON A.BillHeadID = B.BillHeadID ");
            strSql.Append(" LEFT JOIN IP_DrugBillType C ON A.BillTypeID = C.BillTypeID ");
            if (orderStatus)
            {
                // 已发药
                strSql.Append(" WHERE B.DispDrugFlag = 1 ");
            }
            else
            {
                // 未发药
                strSql.Append(" WHERE B.DispDrugFlag = 0 ");
            }

            // 处方日期
            if (startDate != DateTime.MinValue && endDate != DateTime.MinValue)
            {
                string sTime = startDate.ToString("yyyy-MM-dd") + " 00:00:00";
                string eTime = endDate.ToString("yyyy-MM-dd") + " 23:59:00";
                strSql.AppendFormat(" AND A.MakeDate BETWEEN '{0}' AND '{1}' ", sTime, eTime);
            }

            if (deptId > 0)
            {
                strSql.AppendFormat(" AND B.PatDeptID = {0} ", deptId);
            }

            if (patListID > 0)
            {
                strSql.AppendFormat(" AND B.PatListID = {0} ", patListID);
            }

            strSql.Append(" GROUP BY A.BillHeadID ORDER BY BillHeadID,MakeDate ");

            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 查询统领单汇总数据
        /// </summary>
        /// <param name="billHeadID">统领单头ID</param>
        /// <param name="dispDrugFlag">发药标识</param>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>统领单汇总数据</returns>
        public DataTable GetDrugBillSummaryData(int billHeadID, int dispDrugFlag, int patListID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT A.ItemID , ");
            strSql.Append(" SUM(A.SellFee) SellFee , ");
            strSql.Append(" A.ItemName , ");
            strSql.Append(" A.Spec , ");
            strSql.Append(" MIN(C.Name) DeptName, ");
            strSql.Append(" SUM(A.Amount) Amount , ");
            strSql.Append(" MIN(A.Unit) Unit ");
            strSql.Append(" FROM    IP_DrugBillDetail A ");
            strSql.Append(" LEFT JOIN IP_DrugBillHead B ON A.BillHeadID = B.BillHeadID ");
            strSql.Append(" LEFT JOIN BaseDept C ON B.ExecDeptID = C.DeptId ");
            strSql.AppendFormat(" WHERE A.BillHeadID = {0} AND A.DispDrugFlag = {1} ", billHeadID, dispDrugFlag);
            if (patListID > 0)
            {
                strSql.AppendFormat("AND A.PatListID = {0}", patListID);
            }

            strSql.Append(" GROUP BY A.ItemID ,A.ItemName ,A.Spec  ");
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 查询统领单明细数据
        /// </summary>
        /// <param name="billHeadID">统领单头ID</param>
        /// <param name="dispDrugFlag">发药标识</param>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>统领单明细数据</returns>
        public DataTable GetDrugBillDetailData(int billHeadID, int dispDrugFlag, int patListID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT  0 AS CheckFlg , ");
            strSql.Append(" CASE A.NoDrugFlag ");
            strSql.Append(" WHEN 1 THEN '缺' ");
            strSql.Append(" ELSE '' ");
            strSql.Append(" END NoDrugFlag , ");
            strSql.Append(" A.BedNO , ");
            strSql.Append(" A.SerialNumber , ");
            strSql.Append(" A.ItemID , ");
            strSql.Append(" A.SellFee , ");
            strSql.Append(" A.ItemName , ");
            strSql.Append(" A.Spec , ");
            strSql.Append(" A.Amount , ");
            strSql.Append(" A.Unit , ");
            strSql.Append(" B.MakeDate , ");
            strSql.Append(" C.Name AS DeptName , ");
            strSql.Append(" A.BillHeadID ");
            strSql.Append(" FROM IP_DrugBillDetail A ");
            strSql.Append(" LEFT JOIN IP_DrugBillHead B ON A.BillHeadID = B.BillHeadID ");
            strSql.Append(" LEFT JOIN BaseDept C ON B.ExecDeptID = C.DeptId ");
            strSql.AppendFormat(" WHERE A.BillHeadID = {0} AND A.DispDrugFlag = {1} ", billHeadID, dispDrugFlag);
            if (patListID > 0)
            {
                strSql.AppendFormat("AND A.PatListID = {0}", patListID);
            }

            strSql.Append(" ORDER BY A.BillDetailID ");
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 重新发送统领单，修改缺药标识
        /// </summary>
        /// <param name="billHeadID">统领单头表ID</param>
        public void AgainSendOrder(int billHeadID)
        {
            string strSql = string.Format(
                @"UPDATE IP_DrugBillDetail SET NoDrugFlag = 0 
                            WHERE BillHeadID = {0} AND DispDrugFlag = 0",
                billHeadID);
            oleDb.DoCommand(strSql);
        }

        /// <summary>
        /// 根据统领单头表ID查询明细是否存在已发药的数据
        /// </summary>
        /// <param name="billHeadID">统领单头表ID</param>
        /// <returns>true:存在/false:不存在</returns>
        public bool CheckOrderDetailDispDrugFlag(int billHeadID)
        {
            string strSql = string.Format(
                @"SELECT BillDetailID FROM IP_DrugBillDetail 
                            WHERE BillHeadID = {0} AND DispDrugFlag = 1", 
                billHeadID);
            return oleDb.GetDataTable(strSql).Rows.Count > 0;
        }

        /// <summary>
        /// 删除统领单头表数据
        /// </summary>
        /// <param name="billHeadID">统领单头表ID</param>
        public void DelDrugBillHeadData(int billHeadID)
        {
            string strSql = string.Format("DELETE FROM IP_DrugBillHead WHERE BillHeadID = {0} ", billHeadID);
            oleDb.DoCommand(strSql);
        }

        /// <summary>
        /// 删除统领单明细数据
        /// </summary>
        /// <param name="billHeadID">统领单头表ID</param>
        public void DelDrugBillDetailData(int billHeadID)
        {
            string strSql = string.Format("DELETE FROM IP_DrugBillDetail WHERE BillHeadID = {0} ", billHeadID);
            oleDb.DoCommand(strSql);
        }

        #endregion

        #region "住院结算"

        /// <summary>
        /// 优惠计算--根据大项目ID分组查询病人费用数据
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人费用数据</returns>
        public DataTable GetFeeItemRecordGroupByStatID(int patListID)
        {
            string strSql = @"SELECT  SUM(TotalFee) AS ClassAmount ,
                            StatID AS ClassTypeID ,
                            0 AS PromAmount
                            FROM    IP_FeeItemRecord
                            WHERE   PatListID = {0}
                            AND CostHeadID = 0
                            AND RecordFlag = 0 AND WorkID = {1}
                            GROUP BY StatID";
            strSql = string.Format(strSql, patListID, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 优惠计算--查询费用明细数据
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>费用明细数据</returns>
        public DataTable GetFeeItemRecordDetails(int patListID)
        {
            string strSql = @"SELECT  ItemID AS ItemTypeID,
                            MIN(FeeRecordID) AS PresDetailId ,
                            SUM(TotalFee) AS ItemAmount ,
                            0 AS PromAmount
                            FROM    IP_FeeItemRecord
                            WHERE   PatListID = {0}
                            AND CostHeadID = 0
                            AND RecordFlag = 0 AND WorkID = {1} GROUP BY ItemID";
            strSql = string.Format(strSql, patListID, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取计算病人上一次结算时间
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人上一次结算时间</returns>
        public DataTable GetPatLastCostDate(int patListID)
        {
            string strSql = string.Format(
                @"SELECT CostDate FROM IP_CostHead  WHERE PatListID ={0} 
                            AND Status=0 AND WorkID={1} ORDER BY CostDate DESC", 
                patListID, 
                oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 保存住院结算明细费用汇总表数据
        /// </summary>
        /// <param name="costHeadID">结算主表ID</param>
        /// <param name="invoiceID">票据号ID</param>
        /// <param name="invoiceNO">票据号</param>
        /// <param name="workID">机构ID</param>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>true:保存成功</returns>
        public bool SaveCostDetail(int costHeadID, int invoiceID, string invoiceNO, int workID, int patListID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" INSERT INTO IP_CostDetail ");
            strSql.Append(" (CostHeadID ");
            strSql.Append(" ,MemberID ");
            strSql.Append(" ,PatListID ");
            strSql.Append(" ,PatName ");
            strSql.Append(" ,DeptID ");
            strSql.Append(" ,DoctorID ");
            strSql.Append(" ,ExecDeptID ");
            strSql.Append(" ,InvoiceID ");
            strSql.Append(" ,InvoiceNO ");
            strSql.Append(" ,StatID ");
            strSql.Append(" ,TotalFee ");
            strSql.Append(" ,WorkID) ");
            strSql.Append(" SELECT ");
            strSql.AppendFormat("{0},", costHeadID);
            strSql.Append(" MIN(B.MemberID) MemberID, ");
            strSql.Append(" MIN(B.PatListID)PatListID, ");
            strSql.Append(" MIN(B.PatName)PatName, ");
            strSql.Append(" MIN(B.CurrDeptID)CurrDeptID, ");
            strSql.Append(" MIN(B.CurrDoctorID)CurrDoctorID, ");
            strSql.Append(" MIN(A.ExecDeptID)ExecDeptID, ");
            strSql.AppendFormat("{0},", invoiceID);
            strSql.AppendFormat("'{0}',", invoiceNO);
            strSql.Append(" A.StatID, ");
            strSql.Append(" SUM(A.TotalFee) TotalFee , ");
            strSql.AppendFormat("{0}", workID);
            strSql.Append(" FROM    IP_FeeItemRecord A ");
            strSql.Append(" LEFT JOIN IP_PatList B ON A.PatListID=B.PatListID ");
            strSql.AppendFormat(" WHERE A.PatListID={0} AND A.CostHeadID=0 AND A.WorkID={1} ", patListID, workID);
            strSql.Append(" GROUP BY StatID ");
            return oleDb.DoCommand(strSql.ToString()) > 0;
        }

        /// <summary>
        /// 反写住院预交金表结算ID
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="costHeadID">结算头表ID</param>
        /// <param name="costType">结算类型</param>
        /// <param name="isCancel">是否是取消结算</param>
        /// <returns>true：写入成功</returns>
        public bool CostDeposit(int patListID, int costHeadID, int costType, bool isCancel)
        {
            string strSql = string.Empty;
            if (!isCancel)
            {
                strSql = string.Format(
                    @"UPDATE IP_DepositList SET CostHeadID = {0},CostType ={1} 
                            WHERE PatListID ={2} AND CostHeadID = 0 AND WorkID={3}", 
                    costHeadID, 
                    costType, 
                    patListID, 
                    oleDb.WorkId);
            }
            else
            {
                strSql = string.Format(
                    @"UPDATE IP_DepositList SET CostHeadID = 0, CostType = 0 
                            WHERE PatListID ={0} AND CostHeadID = {1} AND WorkID = {2}", 
                    patListID, 
                    costHeadID, 
                    oleDb.WorkId);
            }

            return oleDb.DoCommand(strSql) > 0;
        }

        /// <summary>
        /// 反写住院费用明细表结算ID
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="costHeadID">结算头表ID</param>
        /// <param name="costType">结算类型</param>
        /// <param name="isCancel">是否是取消结算</param>
        /// <returns>true:写入成功</returns>
        public bool CostFeeItemRecord(int patListID, int costHeadID, int costType, bool isCancel)
        {
            string strSql = string.Empty;
            if (!isCancel)
            {
                strSql = "UPDATE IP_FeeItemRecord SET CostHeadID = {0}, CostType ={1} WHERE PatListID = {2} AND CostHeadID = 0 AND WorkID = {3}";
                strSql = string.Format(strSql, costHeadID, costType, patListID, oleDb.WorkId);
            }
            else
            {
                strSql = "UPDATE IP_FeeItemRecord SET CostHeadID = 0, CostType = 0 WHERE PatListID = {0} AND CostHeadID = {1} AND WorkID = {2}";
                strSql = string.Format(strSql, patListID, costHeadID, oleDb.WorkId);
            }

            return oleDb.DoCommand(strSql) > 0;
        }

        /// <summary>
        /// 出院结算、欠费结算修改病人状态
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="patStatus">病人状态</param>
        /// <returns>true：修改成功</returns>
        public bool UpdatePatStatus(int patListID, int patStatus)
        {
            string strSql = string.Format("UPDATE IP_PatList SET Status = {1} WHERE PatListID = {0}", patListID, patStatus);
            return oleDb.DoCommand(strSql) > 0;
        }

        /// <summary>
        /// 查询已结算列表
        /// </summary>
        /// <param name="costBeginDate">结算开始时间</param>
        /// <param name="costEndDate">结算结束时间</param>
        /// <param name="sqlectParam">检索条件</param>
        /// <param name="empId">结算人ID</param>
        /// <param name="status">结算状态</param>
        /// <param name="isAccount">是否缴款</param>
        /// <param name="costType">结算类型</param>
        /// <returns>已结算列表</returns>
        public DataTable GetCostFeeList(
            DateTime costBeginDate, 
            DateTime costEndDate, 
            string sqlectParam, 
            int empId, 
            int status, 
            int isAccount, 
            string costType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT  A.CostHeadID , ");
            strSql.Append("         A.InvoiceNO , ");
            strSql.Append("         A.PatListID , ");
            strSql.Append("         A.CostType , ");
            strSql.Append("         B.SerialNumber , ");
            strSql.Append("         A.PatName , ");
            strSql.Append("         C.Name DeptName , ");
            strSql.Append("         D.PatTypeName , ");
            strSql.Append("         CONVERT(VARCHAR(100), B.EnterHDate, 23) EnterHDate , ");
            strSql.Append("         CASE WHEN B.Status = 4 THEN CONVERT(VARCHAR(100), B.LeaveHDate, 23) ");
            strSql.Append("         END LeaveHDate , ");
            strSql.Append("         A.CostDate , ");
            strSql.Append("         CONVERT(VARCHAR(100), A.CostBeginDate, 23) + ' -- ' ");
            strSql.Append("         + CONVERT(VARCHAR(100), A.CostEndDate, 23) AS CostBEDate , ");
            strSql.Append("         A.TotalFee , ");
            strSql.Append("         CASE A.CostType WHEN 1 THEN '中途结算' WHEN 2 THEN '出院结算' WHEN 3 THEN '欠费结算' END AS CostTypeName, ");
            strSql.Append("         E.Name EmpName , ");
            strSql.Append("         CASE A.Status ");
            strSql.Append("           WHEN 0 THEN '正常' ");
            strSql.Append("           WHEN 1 THEN '被退' ");
            strSql.Append("           WHEN 2 THEN '红冲' ");
            strSql.Append("         END AS CostStatus , ");
            strSql.Append("         A.Status ");
            strSql.Append(" FROM    IP_CostHead A ");
            strSql.Append("         LEFT JOIN IP_PatList B ON A.PatListID = B.PatListID ");
            strSql.Append("         LEFT JOIN BaseDept C ON A.DeptID = C.DeptId ");
            strSql.Append("         LEFT JOIN Basic_PatType D ON A.PatTypeID = D.PatTypeID ");
            strSql.Append("         LEFT JOIN BaseEmployee E ON A.CostEmpID = E.EmpId ");
            strSql.Append(" WHERE 1=1 ");
            // 结算时间
            if (costBeginDate != DateTime.MinValue && costEndDate != DateTime.MinValue)
            {
                string sTime = costBeginDate.ToString("yyyy-MM-dd") + " 00:00:00";
                string eTime = costEndDate.ToString("yyyy-MM-dd") + " 23:59:00";
                strSql.AppendFormat(" AND A.CostDate BETWEEN '{0}' AND '{1}' ", sTime, eTime);
            }

            // 检索条件
            if (!string.IsNullOrEmpty(sqlectParam))
            {
                strSql.AppendFormat(" AND (A.PatName Like '%{0}%' ", sqlectParam);  // 病人名
                strSql.AppendFormat(" OR A.InvoiceNO LIKE '%{0}%') ", sqlectParam);   // 发票号
            }

            // 收费员
            strSql.AppendFormat("AND (A.CostEmpID = {0} OR (-1)={0}) ", empId);
            // 结算状态
            if (status == 0)
            {
                // 全部
                strSql.Append(" AND A.Status IN (0,1,2) ");
            }
            else if (status == 1)
            {
                // 正常
                strSql.Append(" AND A.Status = 0 ");
            }
            else if (status == 2)
            {
                // 作废
                strSql.Append(" AND A.Status IN (1,2) ");
            }

            // 是否缴款
            if (isAccount == 1)
            {
                strSql.Append(" AND A.AccountID = 0 ");
            }
            else if (isAccount == 2)
            {
                strSql.Append(" AND A.AccountID > 0 ");
            }

            // 结算类型
            if (!string.IsNullOrEmpty(costType))
            {
                string[] c = costType.Split(',');
                strSql.Append(" AND A.CostType IN ( ");
                for (int i = 0; i < c.Length; i++)
                {
                    strSql.Append(c[i]);
                    if (i < c.Length - 1)
                    {
                        strSql.Append(",");
                    }
                }

                strSql.Append(") ");
            }

            strSql.AppendFormat(" AND A.WorkID={0}", oleDb.WorkId);
            strSql.Append(" ORDER BY A.CostHeadID ");
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 根据结算ID查询结算记录
        /// </summary>
        /// <param name="costHeadID">结算ID</param>
        /// <returns>结算记录</returns>
        public DataTable GetCostHeadByHeadID(int costHeadID)
        {
            string strSql = string.Format(@" SELECT * FROM IP_CostHead WHERE CostHeadID ={0} ", costHeadID);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 查询病人最后一次计算记录的结算ID
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>结算ID</returns>
        public DataTable GetLastHoleCostHeadID(int patListID)
        {
            string strSql = string.Format(
                @" SELECT TOP 1 CostHeadID FROM IP_CostHead WHERE PatListID={0} 
                            AND Status =0 AND WorkID={1} ORDER BY CostHeadID DESC ", 
                patListID, 
                oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 取消结算--支付方式记录表产生红冲记录
        /// </summary>
        /// <param name="oldCostHeadID">旧结算ID</param>
        /// <param name="newCostHeadID">新结算ID</param>
        public void CancelCostUpdCostPayment(int oldCostHeadID, int newCostHeadID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" INSERT INTO IP_CostPayment ");
            strSql.Append(" (CostHeadID ");
            strSql.Append(" ,PatListID ");
            strSql.Append(" ,PaymentID ");
            strSql.Append(" ,PatTypeID ");
            strSql.Append(" ,PayName ");
            strSql.Append(" ,CostMoney ");
            strSql.Append(" ,AccountID ");
            strSql.Append(" ,WorkID) ");
            strSql.Append(" SELECT  ");
            strSql.AppendFormat(" {0} ", newCostHeadID);
            strSql.Append(" ,PatListID ");
            strSql.Append(" ,PaymentID ");
            strSql.Append(" ,PatTypeID ");
            strSql.Append(" ,PayName ");
            strSql.Append(" ,0-CostMoney AS CostMoney ");
            strSql.Append(" ,AccountID ");
            strSql.AppendFormat(" ,WorkID FROM IP_CostPayment WHERE CostHeadID={0} ", oldCostHeadID);
            oleDb.DoCommand(strSql.ToString());
        }

        /// <summary>
        /// 取消结算--住院结算明细费用汇总表产生红冲记录
        /// </summary>
        /// <param name="oldCostHeadID">旧结算头ID</param>
        /// <param name="newCostHeadID">新结算头ID</param>
        public void CancelCostUpdCostDetail(int oldCostHeadID, int newCostHeadID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" INSERT INTO IP_CostDetail ");
            strSql.Append(" (CostHeadID ");
            strSql.Append(" ,MemberID ");
            strSql.Append(" ,PatListID ");
            strSql.Append(" ,PatName ");
            strSql.Append(" ,DeptID ");
            strSql.Append(" ,DoctorID ");
            strSql.Append(" ,ExecDeptID ");
            strSql.Append(" ,InvoiceID ");
            strSql.Append(" ,InvoiceNO ");
            strSql.Append(" ,StatID ");
            strSql.Append(" ,TotalFee ");
            strSql.Append(" ,WorkID) ");
            strSql.Append(" SELECT ");
            strSql.AppendFormat(" {0} ", newCostHeadID);
            strSql.Append(" ,MemberID ");
            strSql.Append(" ,PatListID ");
            strSql.Append(" ,PatName ");
            strSql.Append(" ,DeptID ");
            strSql.Append(" ,DoctorID ");
            strSql.Append(" ,ExecDeptID ");
            strSql.Append(" ,InvoiceID ");
            strSql.Append(" ,InvoiceNO ");
            strSql.Append(" ,StatID ");
            strSql.Append(" ,0-TotalFee AS TotalFee ");
            strSql.AppendFormat(" ,WorkID FROM IP_CostDetail WHERE CostHeadID ={0} ", oldCostHeadID);
            oleDb.DoCommand(strSql.ToString());
        }

        /// <summary>
        /// 住院押金查询
        /// </summary>
        /// <param name="costBeginDate">开始时间</param>
        /// <param name="costEndDate">结束时间</param>
        /// <param name="sqlectParam">检索条件</param>
        /// <param name="empId">结算人ID</param>
        /// <param name="status">结算状态</param>
        /// <param name="isAccount">是否缴款</param>
        /// <returns>住院押金列表</returns>
        public DataTable GetAllDepositList(
            DateTime costBeginDate, 
            DateTime costEndDate, 
            string sqlectParam, 
            int empId, 
            int status, 
            int isAccount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT * FROM V_IP_PaymentList ");
            strSql.Append(" WHERE 1=1 ");
            // 结算时间
            if (costBeginDate != DateTime.MinValue && costEndDate != DateTime.MinValue)
            {
                string sTime = costBeginDate.ToString("yyyy-MM-dd") + " 00:00:00";
                string eTime = costEndDate.ToString("yyyy-MM-dd") + " 23:59:00";
                strSql.AppendFormat(" AND MakerDate BETWEEN '{0}' AND '{1}' ", sTime, eTime);
            }

            // 检索条件
            if (!string.IsNullOrEmpty(sqlectParam))
            {
                strSql.AppendFormat(" AND (PatName Like '%{0}%' ", sqlectParam);  // 病人名
                strSql.AppendFormat(" OR InvoiceNO LIKE '%{0}%' ", sqlectParam);   // 发票号
            }

            // 收费员
            strSql.AppendFormat("AND (MakerEmpID = {0} OR (-1)={0}) ", empId);

            // 结算状态
            if (status == 0)
            {
                // 全部
                strSql.Append(" AND States IN (0,1,2) ");
            }
            else if (status == 1)
            {
                // 正常
                strSql.Append(" AND States = 0 ");
            }
            else if (status == 2)
            {
                // 作废
                strSql.Append(" AND States IN (1,2) ");
            }

            // 是否缴款
            if (isAccount == 1)
            {
                strSql.Append(" AND AccountID = 0 ");
            }
            else if (isAccount == 2)
            {
                strSql.Append(" AND AccountID > 0 ");
            }

            strSql.AppendFormat(" AND WorkID={0}", oleDb.WorkId);
            strSql.Append(" ORDER BY MakerDate ");
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 发票补打获取发票显示数据
        /// </summary>
        /// <param name="costHeadID">结算ID</param>
        /// <returns>发票报表数据</returns>
        public DataTable GetInvoiceFillData(int costHeadID)
        {
            string strSql = @"SELECT  A.CostHeadID ,
                            A.InvoiceNO ,
                            A.DeptID ,
                            C.Name DeptName ,
                            B.SerialNumber ,
                            B.CaseNumber ,
                            A.PatName ,
                            B.EnterHDate ,
                            A.CostType,
                            B.LeaveHDate ,
                            A.TotalFee ,
                            A.DeptositFee ,
                            A.BalanceFee ,
                            A.PromFee ,
                            D.Name EmpName
                            FROM    IP_CostHead A
                            LEFT JOIN IP_PatList B ON A.PatListID = B.PatListID
                            LEFT JOIN BaseDept C ON A.DeptID = C.DeptId
                            LEFT JOIN BaseEmployee D ON A.CostEmpID = D.EmpId WHERE A.CostHeadID={0}";
            strSql = string.Format(strSql, costHeadID);
            return oleDb.GetDataTable(strSql);
        }

        #endregion

        #region 现金流水账
        /// <summary>
        /// 插入每日现金流量账表
        /// </summary>
        /// <param name="type">0收入流水账1预交金流水账</param>
        /// <returns>true:插入成功</returns>
        public bool InsertIPAccountBookData(int type)
        {
            try
            {
                DateTime dtNow = DateTime.Now;
                int dateKey = Convert.ToInt32(dtNow.ToString("yyyyMMdd"));
                //收入流水账
                if (type == 0)
                {
                    IDbCommand cmd = oleDb.GetProcCommand("SP_Finacial_InsertCostRunning");
                    oleDb.AddInParameter(cmd as DbCommand, "@WorkID", DbType.Int32, WorkId);
                    oleDb.AddInParameter(cmd as DbCommand, "@DateKey", DbType.Int32, dateKey); 
                    oleDb.AddOutParameter(cmd as DbCommand, "@ErrCode", DbType.Int32, 5);
                    oleDb.AddOutParameter(cmd as DbCommand, "@ErrMsg", DbType.AnsiString, 200);
                    int iRtn = oleDb.DoCommand(cmd);
                    string msg = oleDb.GetParameterValue(cmd as DbCommand, "@ErrMsg").ToString();
                    string code = oleDb.GetParameterValue(cmd as DbCommand, "@ErrCode").ToString();
                    if (iRtn > 0)
                    {
                        return true;
                    }
                    else
                    { 
                        return false;
                    }
                }
                else
                {
                    //预交金流水账
                    IDbCommand cmd = oleDb.GetProcCommand("SP_Finacial_InsertDepositRunning");
                    oleDb.AddInParameter(cmd as DbCommand, "@WorkID", DbType.Int32, WorkId);
                    oleDb.AddInParameter(cmd as DbCommand, "@DateKey", DbType.Int32, dateKey); 
                    oleDb.AddOutParameter(cmd as DbCommand, "@ErrCode", DbType.Int32, 5);
                    oleDb.AddOutParameter(cmd as DbCommand, "@ErrMsg", DbType.AnsiString, 200);
                    int iRtn = oleDb.DoCommand(cmd);
                    string msg = oleDb.GetParameterValue(cmd as DbCommand, "@ErrMsg").ToString();
                    string code = oleDb.GetParameterValue(cmd as DbCommand, "@ErrCode").ToString();
                    if (iRtn > 0)
                    { 
                        return true;
                    }
                    else
                    { 
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


        /// <summary>
        /// 根据会员ID获取会员信息
        /// </summary>
        /// <param name="memberID">会员ID</param>
        /// <returns>会员信息</returns>
        public DataTable QueryMemberInfo(int memberID)
        {
            string strSql = string.Format("select * from V_ME_AccountInfo where MemberID={0}", memberID);
            return oleDb.GetDataTable(strSql);
        }
    }
}
