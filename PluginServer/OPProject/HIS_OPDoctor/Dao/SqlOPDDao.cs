using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_Entity.OPManage;
using HIS_OPDoctor.ObjectModel;

namespace HIS_OPDoctor.Dao
{
    /// <summary>
    /// 门诊医生站SQLSERVER数据库访问类
    /// </summary>
    public class SqlOPDDao : AbstractDao, IOPDDao
    {
        #region 门诊患者查询
        /// <summary>
        /// 取得医生所在科室信息
        /// </summary>
        /// <param name="empId">医生Id</param>
        /// <returns>医生所在科室数据</returns>
        public DataTable GetDocRelateDeptInfo(int empId)
        {
            string strSql = @"SELECT  a.DeptId ,
                                    b.Name ,
                                    a.DefaultFlag
                            FROM    BaseEmpDept a
                                    LEFT JOIN BaseDept b ON a.DeptId = b.DeptId
                                    LEFT JOIN BaseDeptDetails c ON b.DeptId = c.DeptID
                            WHERE   c.OutUsed = 1 AND b.DelFlag=0
                                    AND a.EmpId = {0}
		                            AND a.WorkId={1}";
            strSql = string.Format(strSql, empId, oleDb.WorkId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 加载病人列表
        /// </summary>
        /// <param name="docId">医生Id</param>
        /// <param name="deptId">科室Id</param>
        /// <param name="regBeginDate">挂号开始日期</param>
        /// <param name="regEndDate">挂号结束日期</param>
        /// <param name="visitStatus">就诊状态</param>
        /// <param name="belong">病人所属</param>
        /// <returns>病人列表数据</returns>
        public DataTable LoadPatientList(int docId, int deptId, string regBeginDate, string regEndDate, int visitStatus, int belong)
        {
            string strSql = @"SELECT   a.MemberID ,
                                        a.PatListID ,
                                        a.CardNO ,
                                        a.PatName ,
                                        a.PatSex ,
                                        a.Age ,
                                        dbo.fnGetPatTypeName(a.PatTypeID) PatTypeName ,
                                        a.PatTypeID,
		                                b.Mobile,
		                                b.[Address],
		                                a.VisitNO,
		                                a.Birthday,
		                                a.Allergies,
		                                a.DiseaseName,
		                                a.RegDate,
		                                a.CureDeptID,
		                                dbo.fnGetDeptName(a.CureDeptID) AS DocDeptName,
		                                a.CureEmpID,
		                                dbo.fnGetEmpName(a.CureEmpID) AS DocName,
		                                a.RegDeptID,
		                                a.RegEmpID,
                                        a.VisitStatus
                                FROM    OP_PatList a LEFT JOIN 
                                        ME_MemberInfo b ON a.MemberID=b.MemberID
                            WHERE a.RegStatus=0 AND a.WorkID={0} AND a.RegDate BETWEEN '{1}' AND '{2}' AND a.VisitStatus={3} ";
            strSql = string.Format(strSql, oleDb.WorkId, regBeginDate, regEndDate, visitStatus);
            string whereStr = string.Empty;
            if (belong == 0)
            {
                whereStr = " and a.CureEmpID=" + docId.ToString() + @" and a.CureDeptID in (SELECT  a.DeptId 
                            FROM    BaseEmpDept a
                                    LEFT JOIN BaseDept b ON a.DeptId = b.DeptId
                                    LEFT JOIN BaseDeptDetails c ON b.DeptId = c.DeptID
                            WHERE c.OutUsed = 1
                                    AND a.EmpId = " + docId + ")";
            }
            else if (belong == 1)
            {
                whereStr = " and  a.CureDeptID=" + deptId.ToString();
            }
            else if (belong == 2)
            {
                whereStr = @" and a.CureDeptID in (SELECT  a.DeptId 
                            FROM    BaseEmpDept a
                                    LEFT JOIN BaseDept b ON a.DeptId = b.DeptId
                                    LEFT JOIN BaseDeptDetails c ON b.DeptId = c.DeptID
                            WHERE c.OutUsed = 1
                                    AND a.EmpId = " + docId + ")";
            }

            DataTable dt = oleDb.GetDataTable(strSql + whereStr + " order by a.RegDate");
            return dt;
        }

        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <param name="patid">病人Id</param>
        /// <returns>病人信息</returns>
        public DataTable LoadPatientInfo(int patid)
        {
            string strSql = @"SELECT   a.MemberID ,
                                        a.PatListID ,
                                        a.CardNO ,
                                        a.PatName ,
                                        a.PatSex ,
                                        a.Age ,
                                        dbo.fnGetPatTypeName(a.PatTypeID) PatTypeName ,
                                        a.PatTypeID,
		                                b.Mobile,
		                                b.[Address],
		                                a.VisitNO,
		                                a.Birthday,
		                                a.Allergies,
		                                a.DiseaseName
                                FROM    OP_PatList a LEFT JOIN 
                                        ME_MemberInfo b ON a.MemberID=b.MemberID
                            WHERE a.PatListID={1} AND a.WorkID={0}";
            DataTable dt = oleDb.GetDataTable(string.Format(strSql, oleDb.WorkId, patid));
            return dt;
        }

        /// <summary>
        /// 通过卡号就诊号查询病人信息
        /// </summary>
        /// <param name="id">查询信息</param>
        /// <param name="type">0卡号1就诊号2病人Id</param>
        /// <returns>病人信息</returns>
        public DataTable GetPatientInfo(string id, int type)
        {
            string strSql = @"SELECT   a.MemberID ,
                                        a.PatListID ,
                                        a.CardNO ,
                                        a.PatName ,
                                        a.PatSex ,
                                        a.Age ,
                                        dbo.fnGetPatTypeName(a.PatTypeID) PatTypeName ,
                                        a.PatTypeID,
		                                b.Mobile,
		                                b.[Address],
		                                a.VisitNO,
		                                a.Birthday,
		                                a.Allergies,
		                                a.DiseaseName,
		                                a.RegDate,
		                                a.CureDeptID,
		                                dbo.fnGetDeptName(a.CureDeptID) AS DocDeptName,
		                                a.CureEmpID,
		                                dbo.fnGetEmpName(a.CureEmpID) AS DocName,
		                                a.RegDeptID,
		                                a.RegEmpID,
                                        a.VisitStatus
                                FROM    OP_PatList a LEFT JOIN 
                                        ME_MemberInfo b ON a.MemberID=b.MemberID
                            WHERE a.RegStatus=0 AND a.WorkID={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            if (type == 0)
            {
                string beginDate = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
                string endDate = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
                strSql = strSql + " and a.CardNO='" + id + "' and a.RegDate between '"+ beginDate + "' and '"+ endDate + "'";
            }
            else if (type == 1)
            {
                strSql = strSql + " and a.VisitNO='" + id + "'";
            }
            else if (type == 2)
            {
                strSql = strSql + " and a.PatListID=" + id;
            }

            DataTable dt = oleDb.GetDataTable(strSql);          
            return dt;
        }
        #endregion

        #region 修改病人资料
        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="memberID">会员Id</param>
        /// <returns>会员基本信息</returns>
        public DataTable GetMemberInfo(int memberID)
        {
            string sql = @"select * from V_ME_MemberInfo where MemberID=" + memberID.ToString();
            return oleDb.GetDataTable(sql);
        }
        #endregion

        #region 就诊历史查询
        /// <summary>
        /// 根据会员号获取就诊记录
        /// </summary>
        /// <param name="memId">会员id</param>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>就诊记录</returns>
        public DataTable GetHisRecord(int memId, Dictionary<string, string> queryWhere)
        {
            string strSql = @"SELECT a.*,b.Mobile,b.Address,
                            dbo.fnGetEmpName(a.CureEmpID) as CureDocName,
                             dbo.fnGetDeptName(a.CureDeptID) as DocDeptName
                          FROM OP_PatList a 
                         LEFT JOIN ME_MemberInfo b on a.MemberID=b.MemberID WHERE a.WorkId={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            if (memId > 0)
            {
                strWhere.AppendFormat(" AND a.MemberID={0}", memId);
            }

            foreach (var pair in queryWhere)
            {
                if (pair.Key == "a.PatName")
                {
                    strWhere.AppendFormat(" AND {0} like '%{1}%'", pair.Key, pair.Value);
                }
                else if (pair.Key != string.Empty)
                {
                    if (pair.Key == "a.VisitNO")
                    {
                        strWhere.AppendFormat(" AND {0}='{1}'", pair.Key, pair.Value);
                    }
                    else
                    {
                        strWhere.AppendFormat(" AND {0}={1}", pair.Key, pair.Value);
                    }
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            strSql += strWhere;
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取科室下的医生
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <returns>科室下的医生</returns>
        public DataTable GetDoctor(int deptId)
        {
            string strSql = @"SELECT EmpId,Name FROM BaseEmployee WHERE EmpId IN (SELECT EmpId FROM BaseEmpDept WHERE DeptId={0})";
            return oleDb.GetDataTable(string.Format(strSql, deptId));
        }
        #endregion

        #region 下诊断
        /// <summary>
        /// 加载诊断记录
        /// </summary>
        /// <param name="patListID">病人Id</param>
        /// <returns>诊断列表</returns>
        public DataTable LoadDiagnosisList(int patListID)
        {
            string strSql = @"SELECT DiagnosisRecordID,MemberID,PatListID,DiagnosisCode,DiagnosisName,PresDoctorID,PresDeptID,DiagnosisDate,SortNo FROM OPD_DiagnosisRecord WHERE PatListID={0} AND WorkID={1} ORDER BY SortNo";
            strSql = string.Format(strSql, patListID, oleDb.WorkId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 添加诊断记录
        /// </summary>
        /// <param name="model">诊断记录实体</param>
        /// <returns>true成功</returns>
        public bool AddDiagnosis(OPD_DiagnosisRecord model)
        {
            string strSql = @"SELECT ISNULL(MAX(SortNo),0)+1 FROM OPD_DiagnosisRecord WHERE PatListID=" + model.PatListID;
            int sortNo = Convert.ToInt32(oleDb.GetDataResult(strSql));
            model.DiagnosisDate = DateTime.Now;
            model.SortNo = sortNo;
            BindDb(model);
            int iRtn = model.save();
            if (iRtn > 0)
            {
                UpdateRegPatList(model.PatListID);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除诊断记录
        /// </summary>
        /// <param name="diagnosisId">诊断记录Id</param>
        /// <param name="patListID">病人Id</param>
        /// <returns>true成功</returns>
        public bool DeleteDiagnosis(int diagnosisId, int patListID)
        {
            string strSql = @"DELETE FROM OPD_DiagnosisRecord WHERE DiagnosisRecordID={0}";
            strSql = string.Format(strSql, diagnosisId);
            int iRtn = oleDb.DoCommand(strSql);
            if (iRtn > 0)
            {
                UpdateRegPatList(patListID);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="dtDiagnosis">诊断记录表</param>
        public void SortDiagnosis(DataTable dtDiagnosis)
        {
            for (int i = 0; i < dtDiagnosis.Rows.Count; i++)
            {
                string sql = "UPDATE OPD_DiagnosisRecord SET SortNo={0} WHERE DiagnosisRecordID={1}";
                int sortNo = (i + 1);
                sql = string.Format(sql, sortNo, dtDiagnosis.Rows[i]["DiagnosisRecordID"].ToString());
                oleDb.DoCommand(sql);
            }

            UpdateRegPatList(Convert.ToInt32(dtDiagnosis.Rows[0]["PatListID"]));
        }

        /// <summary>
        /// 更新挂号记录表的诊断信息
        /// </summary>
        /// <param name="patListId">病人Id</param>
        private void UpdateRegPatList(int patListId)
        {
            DataTable dtDiagnosisList = LoadDiagnosisList(patListId);
            string diseaseStr = GetDiseaseString(dtDiagnosisList);
            string code = string.Empty;
            if (dtDiagnosisList.Rows.Count > 0)
            {
                code = dtDiagnosisList.Rows[0]["DiagnosisCode"].ToString();
            }

            string strSql = @"UPDATE dbo.OP_PatList SET DiseaseCode='{0}',DiseaseName='{1}' WHERE PatListID={2}";
            strSql = string.Format(strSql, code, diseaseStr, patListId);
            oleDb.DoCommand(strSql);
        }

        /// <summary>
        /// 取得诊断字符串
        /// </summary>
        /// <param name="dtDisea">诊断信息表</param>
        /// <returns>诊断字符串</returns>
        private string GetDiseaseString(DataTable dtDisea)
        {
            string str = string.Empty;
            for (int i = 0; i < dtDisea.Rows.Count; i++)
            {
                if (i == dtDisea.Rows.Count - 1)
                {
                    str += dtDisea.Rows[i]["DiagnosisName"].ToString();
                }
                else
                {
                    str += dtDisea.Rows[i]["DiagnosisName"].ToString() + "、";
                }
            }

            return str;
        }
        #endregion

        #region 医技申请单
        /// <summary>
        /// 获取医技申请单科室
        /// </summary>
        /// <param name="workId">机构id</param>
        /// <param name="examclass">检查类型</param>
        /// <returns>医技申请单科室</returns>
        public DataTable GetExecDept(int workId, int examclass)
        {
            string strSql = @"SELECT a.ExecDeptID Id,b.Name FROM Basic_ExamType a 
                                            LEFT JOIN BaseDept b ON a.ExecDeptID=b.DeptId 
                                            WHERE a.WorkID={0} and a.DelFlag=0 AND b.DelFlag=0 AND a.ExamClass={1} GROUP BY ExecDeptID,b.Name";
            return oleDb.GetDataTable(string.Format(strSql, workId, examclass));
        }

        /// <summary>
        /// 根据科室获取项目分类
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <param name="examclass">检查类型</param>
        /// <returns>科室获取项目分类</returns>
        public DataTable GetExamType(int deptId, int examclass)
        {
            string strSql = @"SELECT ExamTypeID,ExamTypeName,SampleID FROM Basic_ExamType WHERE ExecDeptID={0} AND DelFlag=0 AND ExamClass={1}";
            return oleDb.GetDataTable(string.Format(strSql, deptId, examclass));
        }

        /// <summary>
        /// 根据项目分类获取项目信息
        /// </summary>
        /// <param name="typeId">类型id</param>
        /// <returns>项目信息</returns>
        public DataTable GetExamItem(int typeId)
        {
            string strSql = @"SELECT a.ExamItemName,a.ExamItemID,SUM(b.ItemAmount*c.UnitPrice) as Price,a.PYCode,a.WBCode
                                        FROM Basic_ExamItem a LEFT JOIN Basic_ExamItemFee b ON a.ExamItemID=b.ExamItemID 
                                        LEFT JOIN ViewFeeItem_List c ON b.ItemID=c.ItemID 
                                        WHERE a.ExamTypeID={0} AND a.DelFlag=0 AND c.UnitPrice>0 GROUP BY a.ExamItemName,a.ExamItemID,a.PYCode,a.WBCode";
            return oleDb.GetDataTable(string.Format(strSql, typeId));
        }

        /// <summary>
        /// 获取检验样本信息
        /// </summary>
        /// <param name="workId">机构id</param>
        /// <returns>样本信息</returns>
        public DataTable GetSample(int workId)
        {
            string strSql = "SELECT Id,Name FROM dbo.BaseDictContent WHERE ClassId=1030 AND WorkId={0}";
            return oleDb.GetDataTable(string.Format(strSql, workId));
        }

        /// <summary>
        /// 获取申请表头信息
        /// </summary>
        /// <param name="workId">机构id</param>
        /// <param name="systemType">系统类型</param>
        /// <param name="patId">病人id</param>
        /// <returns>申请表头信息</returns>
        public DataTable GetApplyHead(int workId, int systemType, int patId)
        {
            string strSql = @"SELECT SUM(d.TotalFee) as TotalFee,b.Name as ApplyDoctor,a.ApplyDate,c.Name as ApplyDept,a.ApplyHeadID,a.ApplyType,e.ExamTypeName,f.Name as ExecuteName,a.Remark as ItemNames,d.IsReturns,d.ApplyStatus as ApplyStatu ,
                                        CASE WHEN (d.ApplyStatus=0) THEN '申请'
										WHEN (d.ApplyStatus=1 AND d.IsReturns=0) THEN '收费'
                                        WHEN (d.ApplyStatus=1 AND d.IsReturns=1) THEN '退费' 
										WHEN d.ApplyStatus=2 THEN '确费'
										ELSE '申请' END as ApplyStatus,
									case when a.SystemType=1 then '移除'else 
										(case	when d.ApplyStatus=1 then  '退费' when d.ApplyStatus=2 then  '退费' else '移除' END ) end as DelFlag,
                                        CASE WHEN a.ApplyType=0 THEN '检查申请单'
										WHEN a.ApplyType=1 THEN '化验申请单'
										WHEN a.ApplyType=2 THEN '治疗申请单'
										ELSE '检查申请单' END as ApplyTypeName
                                        FROM EXA_MedicalApplyHead a LEFT JOIN BaseEmployee b ON a.ApplyDoctorID = b.EmpId
                                        LEFT JOIN BaseDept c ON a.ApplyDeptID = c.DeptId
                                        LEFT JOIN EXA_MedicalApplyDetail d ON a.ApplyHeadID = d.ApplyHeadID
										LEFT JOIN Basic_ExamType e ON a.ExamTypeID=e.ExamTypeID
                                        LEFT JOIN BaseDept f ON a.ExecuteDeptID = f.DeptId
                                        WHERE a.WorkId={0} AND a.SystemType={1} AND a.PatListID={2}						
                                        GROUP BY b.Name,a.ApplyDate,c.Name,d.ApplyStatus,a.ApplyHeadID,e.ExamTypeName,a.ApplyType,f.Name,a.Remark,a.SystemType,d.IsReturns";
            return oleDb.GetDataTable(string.Format(strSql, workId, systemType, patId));
        }

        /// <summary>
        ///  根据申请表头ID获取收费状态
        /// </summary>
        /// <param name="headId">申请头id</param>
        /// <returns>获取收费状态</returns>
        public DataTable GetApplyStatus(int headId)
        {
            string strSql = @"SELECT ApplyStatus FROM EXA_MedicalApplyDetail WHERE ApplyHeadID={0}";
            return oleDb.GetDataTable(string.Format(strSql, headId));
        }

        /// <summary>
        /// 获取收费项目数据
        /// </summary>
        /// <param name="itemId">项目id</param>
        /// <returns>收费项目数据</returns>
        public DataTable GetFeeItemData(int itemId)
        {
            string strsql = @"SELECT * FROM ViewFeeItem_List where ItemID={0} AND WorkId={1}";
            strsql = string.Format(strsql, itemId, oleDb.WorkId);
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 移除申请表头信息
        /// </summary>
        /// <param name="applyheadId">申请头id</param>
        /// <param name="systemType">系统类型</param>
        /// <returns>1成功</returns>
        public int DelApplyHead(int applyheadId, int systemType)
        {
            string strSql = @"SELECT PresDetailID FROM dbo.EXA_MedicalApplyDetail WHERE ApplyHeadID={0}";
            DataTable dt = oleDb.GetDataTable(string.Format(strSql, applyheadId));
            strSql = @"Delete FROM dbo.EXA_MedicalApplyHead WHERE ApplyHeadID={0}";
            oleDb.DoCommand(string.Format(strSql, applyheadId));
            if (dt != null)
            {
                if (systemType == 1)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        strSql = @"Delete FROM dbo.IPD_OrderRecord WHERE OrderID={0}";
                        oleDb.DoCommand(string.Format(strSql, dt.Rows[i]["PresDetailID"]));
                    }
                }
                else
                {
                    if (dt.Rows.Count > 0)
                    {
                        strSql = "SELECT PresHeadID FROM OPD_PresDetail WHERE PresDetailID={0}";
                        DataTable dtPres = oleDb.GetDataTable(string.Format(strSql, dt.Rows[0]["PresDetailID"]));
                        strSql = @"Delete FROM OPD_PresHead WHERE PresHeadID={0}";
                        oleDb.DoCommand(string.Format(strSql, dtPres.Rows[0]["PresHeadID"]));
                        strSql = @"Delete FROM OPD_PresDetail WHERE PresHeadID={0}";
                        oleDb.DoCommand(string.Format(strSql, dtPres.Rows[0]["PresHeadID"]));
                    }
                }
            }

            strSql = @"Delete FROM dbo.EXA_MedicalApplyDetail WHERE ApplyHeadID={0}";
            return oleDb.DoCommand(string.Format(strSql, applyheadId));
        }

        /// <summary>
        /// 获取表头和明细
        /// </summary>
        /// <param name="applyheadId">申请头id</param>
        /// <returns>表头和明细数据</returns>
        public DataTable GetHeadDetail(int applyheadId)
        {
            string strSql = @"SELECT a.ApplyStatus AS ApplyStatu,a.IsReturns,f.Name as ApplyDeptDoctor,e.Name as ApplyDeptName,dbo.fnGetDeptName(b.ExecuteDeptID) as ExcuteDeptName,d.PresHeadID,a.ApplyDetailID,a.PresDetailID,a.ItemID,a.ItemName,a.Price,a.Amount,b.* FROM dbo.EXA_MedicalApplyDetail a 
                                            LEFT JOIN dbo.EXA_MedicalApplyHead b ON a.ApplyHeadID=b.ApplyHeadID 
                                            LEFT JOIN dbo.OPD_PresDetail c ON a.PresDetailID=c.PresDetailID 
                                            LEFT JOIN dbo.OPD_PresHead d ON c.PresHeadID=d.PresHeadID 
                                            LEFT JOIN BaseDept e ON b.ApplyDeptID=e.DeptId
                                            LEFT JOIN BaseEmployee f ON b.ApplyDoctorID=f.EmpId
                                            WHERE a.ApplyHeadID={0}";
            return oleDb.GetDataTable(string.Format(strSql, applyheadId));
        }

        /// <summary>
        /// 通过卡号就诊号查询病人信息
        /// </summary>
        /// <param name="id">查询信息</param>
        /// <returns>病人信息</returns>
        public DataTable GetIPPatientInfo(string id)
        {
            string strSql = @"SELECT CaseNumber,SerialNumber,BedNo,PatName ,case when Sex=1 then '男' when Sex=2 then '女' else '未知' end as PatSex,Age ,
                                        dbo.fnGetPatTypeName(PatTypeID) PatTypeName,EnterDiseaseName as DiseaseName
                                FROM IP_PatList WHERE WorkID={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            strSql = strSql + " and PatListID=" + id ;
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 获取大项目ID
        /// </summary>
        /// <param name="examId">检查项目id</param>
        /// <returns>大项目ID</returns>
        public DataTable GetStatID(string examId)
        {
            string strSql = @"SELECT StatID FROM Basic_ExamItem WHERE ExamItemID={0}";
            return oleDb.GetDataTable(string.Format(strSql, examId));
        }

        /// <summary>
        /// 获取床位病人信息
        /// </summary>
        /// <param name="id">病人id</param>
        /// <returns>床位病人信息</returns>
        public DataTable GetInBedPatient(int id)
        {
            string strWhere = " and  a.patlistid=" + id ;
            string strsql = @"select a.BedNo,
                                     a.patlistid,
                                     a.MemberID,
                                     a.SerialNumber,
                                     a.PatName,                                     
                                     a.CurrDoctorID,
                                     a.CurrDeptID,
                                     a.CurrNurseID,
                                     a.Age,
                                     a.status,
                                     a.NursingLever,
                                     a.DietType,
                                     dbo.fnGetPatTypeName(a.PatTypeID) patTypeName,
                                     case when a.Sex=1 then '男' when a.Sex=2 then '女' else '未知' end as PatSex,
                                     a.EnterDiseaseName,
                                     a.EnterHDate,
                                     a.EnterWardID,
                                     a.Times,
                                      b.Name as EnterSituationName
                              from IP_PatList a
                              left join (SELECT * FROM BaseDictContent WHERE ClassId=1017) b 
                              on a.EnterSituation=b.Code
                              where  a.workid=" + oleDb.WorkId + " " + strWhere + " order by a.BedNo";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取处方表最大处方数
        /// </summary>
        /// <returns>最大处方数</returns>
        public DataTable GetPresNO()
        {
            string strsql = @"SELECT MAX(PresNO) AS PresNO FROM dbo.OPD_PresDetail a LEFT JOIN dbo.OPD_PresHead b ON a.PresHeadID=b.PresHeadID WHERE b.PresType=4 AND b.WorkID={0}";
            return oleDb.GetDataTable(string.Format(strsql, oleDb.WorkId));
        }

        /// <summary>
        /// 获取组合项目明细
        /// </summary>
        /// <param name="examItemId">组合项目ID</param>
        /// <returns>返回组合项目明细</returns>
        public DataTable GetExamItemDetail(int examItemId)
        {
            string strsql = @"select a.ItemID,
                            a.ItemName,
                             a.ItemUnit,
                             a.ItemAmount,
                             b.Price,
                             a.ItemAmount*b.Price as TotalFee
                             from Basic_ExamItemFee a 
                            left join Basic_HospFeeItem b
                            on a.ItemID=b.ItemID
                          where a.ExamItemID={0} AND a.WorkId={1}";
            strsql = string.Format(strsql, examItemId, oleDb.WorkId);
            return oleDb.GetDataTable(strsql);
        }
        #endregion

        #region 处方控件数据源
        /// <summary>
        /// 取得药品执行药房
        /// </summary>
        /// <param name="presType">0西药1中草药</param>
        /// <returns>药房数据</returns>
        public DataTable GetDrugStoreRoom(int presType)
        {
            string strSql = string.Empty;
            if (presType == 0)
            {
                strSql = @"SELECT Value AS DeptID,'默认药房' AS DeptName FROM dbo.Basic_SystemConfig WHERE ParaID = 'WestDefaultExecDeptID'
                            UNION ALL
                            SELECT - 1 AS DeptID,'全部药房' AS DeptName
                            UNION ALL
                            SELECT DeptID, DeptName FROM DG_DeptDic WHERE DeptType = 0 AND StopFlag = 0 AND WorkID = {0}";
            }
            else if (presType == 1)
            {
                strSql = @"SELECT Value AS DeptID,'默认药房' AS DeptName FROM dbo.Basic_SystemConfig WHERE ParaID = 'ChineseDefaultExecDeptID'
                            UNION ALL
                            SELECT - 1 AS DeptID,'全部药房' AS DeptName
                            UNION ALL
                            SELECT DeptID, DeptName FROM DG_DeptDic WHERE DeptType = 0 AND StopFlag = 0 AND WorkID = {0}";
            }

            strSql = string.Format(strSql, oleDb.WorkId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 获取项目ShowCard数据
        /// </summary>
        /// <param name="itemclass">项目类型</param>
        /// <param name="statID">统计大项目Id</param>
        /// <param name="execDeptId">执行科室</param>
        /// <returns>项目ShowCard数据</returns>
        public DataTable GetFeeItemShowCardData(int itemclass, int statID, int execDeptId)
        {
            // 全部 = 0, 西药与中成药 = 1, 中草药 = 2, 收费项目 = 3
            string strsql = string.Empty;
            if (itemclass == 1 || itemclass == 2)
            {
                strsql = @"SELECT * FROM ViewFeeItem_List where ItemClass =1 AND WorkId={0}";
                strsql = string.Format(strsql, oleDb.WorkId);
                if (statID == 100 || statID == 101)
                {
                    strsql = strsql + " and StatID in(100,101)";
                }
                else
                {
                    strsql = strsql + " and StatID in(102)";
                }

                if (execDeptId != -1)
                {
                    strsql = strsql + " and ExecDeptId =" + execDeptId.ToString();
                }
            }
            else
            {
                strsql = @"SELECT * FROM ViewFeeItem_List where ItemClass in(2,3) AND WorkId={0}";
                strsql = string.Format(strsql, oleDb.WorkId);
            }

            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 取出全部数据，前端做缓存
        /// </summary>
        /// <returns>全部项目数据</returns>
        public DataTable GetFeeItemShowCardDatas()
        {
            // 全部 = 0, 西药与中成药 = 1, 中草药 = 2, 收费项目 = 3
            string strsql = string.Empty;
            strsql = @"SELECT * FROM ViewFeeItem_List where ItemClass in(1,2,3) AND WorkId={0}";
            strsql = string.Format(strsql, oleDb.WorkId);
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 取得药品信息
        /// </summary>
        /// <param name="itemId">项目ID</param>
        /// <param name="deptId">科室id</param>
        /// <returns>药品信息</returns>
        public DataTable GetDrugItem(int itemId,int deptId)
        {
            string strSql = string.Empty;
            if (deptId == -1)
            {
                strSql = @"SELECT top 1 * FROM ViewFeeItem_List where ItemID ={0} AND WorkId={1}";
                strSql = string.Format(strSql, itemId, oleDb.WorkId);
            }
            else
            {
                strSql =  @"SELECT top 1 * FROM ViewFeeItem_List where ItemID ={0} AND WorkId={1} and ExecDeptId={2}";
                strSql = string.Format(strSql, itemId, oleDb.WorkId, deptId);
            }

            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 删除处方明细
        /// </summary>
        /// <param name="presDetailId">处方明细Id</param>
        /// <returns>true成功</returns>
        public bool DeletePrescriptionData(int presDetailId)
        {
            string strsql = @"DELETE FROM OPD_PresDetail WHERE PresDetailID ={0} and IsCharged=0";
            strsql = string.Format(strsql, presDetailId);
            int iRtn = oleDb.DoCommand(strsql);
            if (iRtn > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除处方
        /// </summary>
        /// <param name="patListId">病人id</param>
        /// <param name="presType">处方类型</param>
        /// <param name="presNo">处方号</param>
        /// <returns>true成功</returns>
        public bool DeletePrescriptionData(int patListId, int presType, int presNo)
        {
            string strsql = @"DELETE FROM OPD_PresDetail WHERE IsCharged=0 
                                AND PresHeadID IN(SELECT PresHeadID FROM OPD_PresHead WHERE PatListID={0} AND PresType={1})
                                AND PresNO={2}";
            strsql = string.Format(strsql, patListId, presType, presNo);
            oleDb.DoCommand(strsql);
            return true;
        }

        /// <summary>
        /// 获取处方
        /// </summary>
        /// <param name="patListId">病人id</param>
        /// <param name="presType">处方类型</param>
        /// <returns>处方数据</returns>
        public DataTable GetPrescriptionData(int patListId, int presType)
        {
            string strsql = @"SELECT  b.PresDetailID  ,
                                        b.PresHeadID ,
                                        b.ItemID ,
                                        b.ItemName ,
                                       b.StatID,
                                        b.Price ,
                                        b.Entrust,
                                        b.ExecDeptID,
		                                dbo.fnGetDeptName(b.ExecDeptID) ExecDeptName,
                                        b.Spec ,
                                        b.Dosage ,
                                        b.DosageUnit ,
                                        b.Factor ,
                                        b.DoseNum ,
                                        b.ChannelID ,
                                        b.FrequencyID ,
                                        b.Days ,
                                        b.ChargeAmount ,
                                        b.ChargeUnit ,
                                        b.PresAmount ,
                                        b.PresAmountUnit ,
                                        b.PresFactor ,
                                        b.GroupID ,
                                        b.IsAst ,
                                        b.IsTake ,
                                        b.Memo ,
                                        b.PresNO ,
                                        0 OrderNO ,
		                                b.GroupSortNO,
                                        b.IsCharged ,
                                        b.IsCancel ,
                                        c.ChannelName ,
                                        d.FrequencyName ,
                                        d.ExecuteCode ,
                                        1 RoundingMode ,
                                        b.PresDoctorID ,
                                        b.PresDeptID ,
                                        dbo.fnGetEmpName(b.PresDoctorID) PresDoctorName ,
                                        dbo.fnGetDeptName(b.PresDeptID) PresDeptName,
                                        b.DropSpec,
										b.ExecNum,
										b.LinkPresDetailID,
									    b.IsEmergency,
										b.IsLunacyPosion,
										b.PresDate,
                                        (case when b.LinkPresDetailID>0 then '是' else '' end) as Additional,
                                        b.IsReimbursement,
                                        (case when b.IsReimbursement=0 then '' else '保外用' end) as IsReimbursementName,
                                        hm.MedicareID,
                                        hm.RoundingMode FloatFlag
                                FROM    OPD_PresHead a
                                        INNER JOIN OPD_PresDetail b ON a.PresHeadID = b.PresHeadID
                                        LEFT JOIN Basic_Channel AS c ON b.ChannelID = c.ID
                                        LEFT JOIN Basic_Frequency AS d ON b.FrequencyID = d.FrequencyID
                                        LEFT JOIN DG_HospMakerDic  hm ON b.ItemID=hm.drugid
										LEFT JOIN DG_MedicareDic med ON hm.MedicareID=med.MedicareID
                                WHERE   a.PatListID = {0}
                                        AND a.PresType = {1}
                                ORDER BY b.PresNO ,
                                        b.GroupID,b.GroupSortNO";

            strsql = string.Format(strsql, patListId, presType);
            DataTable dt = oleDb.GetDataTable(strsql);
            return dt;
        }

        /// <summary>
        /// 是否结算
        /// </summary>
        /// <param name="ids">处方明细id字符串</param>
        /// <returns>true是收费</returns>
        public bool IsCostPres(string ids)
        {
            string strsql = @"select count(*) num from OPD_PresDetail where PresDetailID IN({0}) AND IsCharged=1";

            strsql = string.Format(strsql, ids);
            object ret = oleDb.GetDataResult(strsql);
            if (Convert.ToInt32(ret) > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 判断库存
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="itemId">项目Id</param>
        /// <param name="presAmount">数量</param>
        /// <returns>true库存存在</returns>
        public bool IsDrugStore(int deptId, int itemId, decimal presAmount)
        {
            string strsql = @"SELECT  top 1 StoreAmount FROM ViewFeeItem_List WHERE ItemID ={0} and ExecDeptId={1}";
            strsql = string.Format(strsql, itemId, deptId);
            object ret = oleDb.GetDataResult(strsql);

            decimal qty = ret == DBNull.Value ? 0 : Convert.ToDecimal(ret);
            if (presAmount > qty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 判断库存
        /// </summary>
        /// <param name="ids">项目Id字符串</param>
        /// <returns>药品库存数据</returns>
        public DataTable IsDrugStore(string ids)
        {
            string strsql = @"SELECT  ItemID,StoreAmount,ExecDeptId FROM ViewFeeItem_List WHERE ItemID IN ({0}) ";
            strsql = string.Format(strsql, ids);
            DataTable dt = oleDb.GetDataTable(strsql);
            return dt;
        }

        /// <summary>
        /// 更新自备
        /// </summary>
        /// <param name="presDetailId">处方明细Id</param>
        /// <param name="flag">标识</param>
        /// <returns>true更新成功</returns>
        public bool UpdatePresSelfDrugFlag(int presDetailId, int flag)
        {
            string strsql = @"UPDATE OPD_PresDetail SET IsTake={1} WHERE PresDetailID={0}";
            strsql = string.Format(strsql, presDetailId, flag);
            oleDb.DoCommand(strsql);
            return true;
        }

        /// <summary>
        /// 更新医保报销标识
        /// </summary>
        /// <param name="list">处方明细列表</param>
        /// <param name="flag">0不报销1报销</param>
        /// <returns>true更新成功</returns>
        public bool UpdatePresReimbursementFlag(List<OPD_PresDetail> list, int flag)
        {           
            List<string> sqllist = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                string strsql = @"UPDATE OPD_PresDetail SET IsReimbursement={1} WHERE PresDetailID={0}";
                strsql = string.Format(strsql, list[i].PresDetailID, flag);
                sqllist.Add(strsql);
            }

            for (int i = 0; i < sqllist.Count; i++)
            {
                oleDb.DoCommand(sqllist[i]);
            }

            return true;
        }

        /// <summary>
        /// 更新处方号和组号
        /// </summary>
        /// <param name="list">处方列表</param>
        /// <returns>true更新成功</returns>
        public bool UpdatePresNoAndGroupId(List<OPD_PresDetail> list)
        {
            List<string> sqllist = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                string strsql = @"UPDATE OPD_PresDetail SET PresNO={1},GroupID={2} WHERE PresDetailID={0}";
                strsql = string.Format(strsql, list[i].PresDetailID, list[i].PresNO, list[i].GroupID);
                sqllist.Add(strsql);
            }

            for (int i = 0; i < sqllist.Count; i++)
            {
                oleDb.DoCommand(sqllist[i]);
            }

            return true;
        }

        /// <summary>
        /// 保存处方
        /// </summary>
        /// <param name="patListId">病人Id</param>
        /// <param name="selectedMemberID">会员id</param>
        /// <param name="presType">处方类型</param>
        /// <param name="list">处方列表</param>
        /// <returns>true成功</returns>
        public bool SavePrescriptionData(int patListId, int selectedMemberID, int presType, List<OPD_PresDetail> list)
        {
            string strsql = @"select PresHeadID from OPD_PresHead where PatListID ={0} AND PresType={1}";
            strsql = string.Format(strsql, patListId, presType);
            object ret = oleDb.GetDataResult(strsql);
            int headId = ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            OPD_PresHead head = new OPD_PresHead();
            if (headId != 0)
            {
                head.PresHeadID = headId;
            }

            OP_PatList op = (OP_PatList)NewObject<OP_PatList>().getmodel(patListId);

            head.MemberID = op.MemberID;
            head.PatListID = patListId;
            head.PresType = presType;
            this.BindDb(head);
            head.save();
            foreach (OPD_PresDetail detaiModel in list)
            {
                detaiModel.PresHeadID = head.PresHeadID;
                this.BindDb(detaiModel);
                detaiModel.save();

                //删除联动费用
                NewObject<PrescriptionProcess>().DeleteLinkFeeItems(detaiModel.PresDetailID);
            }

            return true;
        }

        /// <summary>
        /// 获取处方打印的信息
        /// </summary>
        /// <param name="preHeadId">处方明细头ID</param>
        /// <param name="preNo"> 处方号</param>
        /// <returns>处方打印的信息</returns>
        public DataTable GetPrintPresData(int preHeadId, int preNo)
        {
            string strSql = @"SELECT (a.Price*a.ChargeAmount) as TotalFee,a.PresNO,a.Dosage,a.ItemID,
                                            a.ItemName+CASE WHEN (a.IsTake=1) THEN '[自备]' ELSE '' END as ItemName,a.DoseNum,a.DosageUnit,
                                            a.AstResult,a.Spec,a.PresAmount as Amount,a.PresAmountUnit as Unit,a.Entrust,a.PresDate,a.IsTake,a.GroupID,
                                            dbo.fnGetDeptName(a.PresDeptID) as DeptName,c.ChannelName,d.CName as FrequencyName,dbo.fnGetEmpName(a.PresDoctorID) as DoctorName,a.IsCharged,a.IsEmergency,a.IsLunacyPosion,a.ChannelID,
                                            CASE WHEN (ISNULL(v.MedicareID,0)=3) THEN '□' 
                                             WHEN (ISNULL(v.MedicareID,0)=1) THEN '△'
                                              WHEN( ISNULL(v.MedicareID,0)=2) THEN '◇'
                                              ELSE '□'  END AS MedicareID

                                            FROM OPD_PresDetail a LEFT JOIN OPD_PresHead b ON a.PresHeadID=b.PresHeadID 
                                            LEFT JOIN Basic_Channel c ON  a.ChannelID=c.ID 
                                            LEFT JOIN Basic_Frequency d ON a.FrequencyID=d.FrequencyID 
                                            LEFT JOIN ViewFeeItem_List v ON a.ItemID=v.ItemID
                                            WHERE b.PresHeadID={0}";
            if (preNo > 0)
            {
                strSql += " AND a.PresNO={1}";
                strSql = string.Format(strSql, preHeadId, preNo);
            }
            else
            {
                strSql = string.Format(strSql, preHeadId);
            }

            strSql += " GROUP BY a.PresNO,a.Dosage,a.ItemID,a.ItemName,a.IsTake,a.DoseNum,a.DosageUnit,a.AstResult,a.Spec,a.PresAmount,a.PresAmountUnit,a.Entrust,a.PresDate,a.IsTake,a.GroupID,a.PresDeptID,c.ChannelName,d.CName,a.PresDoctorID,a.IsCharged,a.IsEmergency,a.IsLunacyPosion,a.ChannelID,v.MedicareID,a.Price,a.ChargeAmount";
            strSql += " order by a.PresNO,a.GroupID";
                
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 根据用法ID获取用法联动费用
        /// </summary>
        /// <param name="channelId">用法id</param>
        /// <returns>用法联动费用数据</returns>
        public DataTable GetChannelFees(int channelId)
        {
            var strSql = @"SELECT 
                                       a.ItemName,a.ItemID,a.ItemAmount,a.ItemUnit,b.UnitPrice,
                                        CASE WHEN FeeClass=1 THEN '项目' ELSE '材料' END AS ItemClassName,
                                        CASE WHEN CalCostMode=0 THEN '按频次' ELSE '按周期' END AS ModeName,CalCostMode,
                                        ISNULL(b.ExecDeptId,0) AS ExecDeptId,ISNULL(b.StatID,0) AS StatID,ISNULL(b.[Standard],'') AS spec,ISNULL(b.UnPickUnit,'') AS UnPickUnit 
                                        FROM Basic_ChannelFee a 
                                        LEFT JOIN ViewFeeItem_List b ON a.ItemID=b.ItemID 
                                        WHERE a.ChannelID={0}";
            return oleDb.GetDataTable(string.Format(strSql, channelId));
        }

        /// <summary>
        /// 取得费用处方头Id
        /// </summary>
        /// <param name="patListId">病人Id</param>
        /// <returns>费用处方头Id</returns>
        public int GetFeeHeadId(int patListId)
        {
            string strsql = @"select PresHeadID from OPD_PresHead where PatListID ={0} AND PresType={1}";
            strsql = string.Format(strsql, patListId, 3);
            object ret = oleDb.GetDataResult(strsql);
            int headId = ret == DBNull.Value ? 0 : Convert.ToInt32(ret);
            return headId;
        }

        /// <summary>
        /// 获取处方模板
        /// </summary>
        /// <param name="tplId">模板ID</param>
        /// <returns>处方模板</returns>
        public DataTable GetPresTemplate(int tplId)
        {
            string strsql = string.Empty;
            strsql = @"SELECT TOP 100
                            A.PresMouldDetailID , A.PresMouldHeadID , A.PresNO , A.GroupID , A.GroupSortNO ,
                            A.ItemID , A.ItemName , A.StatID , A.ExecDeptID ,dbo.fnGetDeptName(A.ExecDeptID) AS  ExecDeptName, A.Spec , A.Dosage , A.DosageUnit ,
                            A.Factor , A.ChannelID , A.FrequencyID , A.Entrust , A.DoseNum , A.ChargeAmount ,
                            A.ChargeUnit , d.UnitPrice AS Price ,  A.[Days] , A.PresAmount , A.PresAmountUnit ,
                            A.PresFactor,b.ChannelName,c.FrequencyName,c.ExecuteCode, d.SkinTestFlag AS IsAst,
		                    (CASE WHEN d.LunacyFlag=1 OR d.VirulentFlag=1 THEN 1 ELSE 0 END ) AS IsLunacyPosion,
                            d.FloatFlag

                    FROM    OPD_PresMouldDetail A
                            LEFT JOIN Basic_Channel B ON A.ChannelID = B.ID
                            LEFT JOIN Basic_Frequency C ON A.FrequencyID = C.FrequencyID
                            LEFT JOIN ViewFeeItem_List D ON A.ItemID = D.ItemID AND d.ExecDeptID=a.ExecDeptID
                    WHERE   PresMouldHeadID = {0}
                    ORDER BY A.PresNO ,
                            A.GroupID ,
                            a.GroupSortNO";
            strsql = string.Format(strsql, tplId);

            DataTable dt = oleDb.GetDataTable(strsql);
            return dt;
        }

        /// <summary>
        /// 获取处方模板行
        /// </summary>
        /// <param name="tpldetailIds">处方明细ID数组</param>
        /// <returns>处方模板信息</returns>
        public DataTable GetPresTemplateRow(int[] tpldetailIds)
        {
            string strsql = string.Empty;
            strsql = @"SELECT TOP 100
                            A.PresMouldDetailID , A.PresMouldHeadID , A.PresNO , A.GroupID , A.GroupSortNO ,
                            A.ItemID , A.ItemName , A.StatID , A.ExecDeptID ,dbo.fnGetDeptName(A.ExecDeptID) AS  ExecDeptName, A.Spec , A.Dosage , A.DosageUnit ,
                            A.Factor , A.ChannelID , A.FrequencyID , A.Entrust , A.DoseNum , A.ChargeAmount ,
                            A.ChargeUnit , d.UnitPrice AS Price ,  A.[Days] , A.PresAmount , A.PresAmountUnit ,
                            A.PresFactor,b.ChannelName,c.FrequencyName,c.ExecuteCode, d.SkinTestFlag AS IsAst,
		                    (CASE WHEN d.LunacyFlag=1 OR d.VirulentFlag=1 THEN 1 ELSE 0 END ) AS IsLunacyPosion,
                            d.FloatFlag

                    FROM    OPD_PresMouldDetail A
                            LEFT JOIN Basic_Channel B ON A.ChannelID = B.ID
                            LEFT JOIN Basic_Frequency C ON A.FrequencyID = C.FrequencyID
                            LEFT JOIN ViewFeeItem_List D ON A.ItemID = D.ItemID AND d.ExecDeptID=a.ExecDeptID
                    WHERE   a.PresMouldDetailID IN ({0})
                    ORDER BY A.PresNO ,
                            A.GroupID ,
                            a.GroupSortNO";
            strsql = string.Format(strsql, string.Join(",", Array.ConvertAll<int, string>(tpldetailIds, delegate (int v) { return v.ToString();})));
            DataTable dt = oleDb.GetDataTable(strsql);
            return dt;
        }

        /// <summary>
        /// 根据处方明细ID获取处方明细信息
        /// </summary>
        /// <param name="detailId">处方明细Id</param>
        /// <returns>处方明细信息</returns>
        public OPD_PresDetail GetPresDetail(int detailId)
        {
            string strSql = @"SELECT * FROM OPD_PresDetail WHERE PresDetailID={0}";
            strSql = string.Format(strSql, detailId);
            return oleDb.Query<OPD_PresDetail>(strSql, string.Empty).FirstOrDefault();
        }
        #endregion

        #region 开住院证
        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <param name="patListID">病人id</param>
        /// <returns>病人信息</returns>
        public DataTable GetPatientData(int patListID)
        {
            string strSql = @"SELECT a.PatName,a.Age,a.PatSex,a.PatTypeName,b.CityName,b.Address,b.Mobile,a.CardNO FROM OP_PatList a INNER JOIN ME_MemberInfo b ON a.MemberID=b.MemberID WHERE a.PatListID={0}";
            strSql = string.Format(strSql, patListID);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 获取病人住院证信息
        /// </summary>
        /// <param name="patListID">病人id</param>
        /// <returns>病人住院证信息</returns>
        public OPD_InpatientReg GetInpatientReg(int patListID)
        {
            string strSql = @"SELECT * FROM OPD_InpatientReg WHERE PatListID={0}";
            strSql = string.Format(strSql, patListID);
            OPD_InpatientReg inpatientReg = oleDb.Query<OPD_InpatientReg>(strSql, null).FirstOrDefault();
            return inpatientReg;
        }

        /// <summary>
        /// 获取床位信息
        /// </summary>
        /// <param name="workId">机构id</param>
        /// <returns>床位信息</returns>
        public DataTable GetBedInfo(int workId)
        {
            string strSql = @"SELECT COUNT(a.WardID) as Total,a.WardID,b.WardName,
                                            COUNT(CASE WHEN a.IsUsed=0 THEN a.IsUsed END)as NoUse,
                                            (COUNT(a.WardID)-COUNT(CASE WHEN a.IsUsed=0 THEN a.IsUsed END)) as IsUse 
                                            FROM IP_BedInfo a INNER JOIN BaseWard b ON a.WardID=b.WardID 
                                            WHERE a.WorkID={0} AND a.IsStoped=0 GROUP BY a.WardID,b.WardName";
            strSql = string.Format(strSql, workId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }
        #endregion

        #region 常用诊断
        /// <summary>
        /// 加载常用诊断信息
        /// </summary>
        /// <param name="doctorID">医生Id</param>
        /// <returns>常用诊断数据</returns>
        public DataTable LoadCommonDianosis(int doctorID)
        {
            string strSql = @"SELECT TOP 50 CommonDiagnosisID,DiagnosisCode,DiagnosisName,PYCode,WBCode,RecordDoctorID,Frequency,DelFlag,WorkID FROM OPD_CommonDiagnosis WHERE DelFlag=0 AND RecordDoctorID={0} AND  WorkID={1}  ORDER BY Frequency DESC,CommonDiagnosisID";
            strSql = string.Format(strSql, doctorID, oleDb.WorkId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 判断常用诊断里是否存在
        /// </summary>
        /// <param name="doctorID">医生Id</param>
        /// <param name="name">诊断名称</param>
        /// <returns>true存在</returns>
        public bool ExistCommonDianosis(int doctorID, string name)
        {
            string strSql = @"SELECT COUNT(1) FROM OPD_CommonDiagnosis WHERE DelFlag=0 AND RecordDoctorID={0} AND DiagnosisName='{1}' AND  WorkID={2}";
            strSql = string.Format(strSql, doctorID, name, oleDb.WorkId);
            int iRtn = Convert.ToInt32(oleDb.GetDataResult(strSql));
            if (iRtn > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 修改常用诊断频次
        /// </summary>
        /// <param name="presDoctorID">处方医生id</param>
        /// <param name="diagnosisName">诊断名称</param>
        /// <returns>true修改成功</returns>
        public bool UpdateCommonDiagnosis(int presDoctorID, string diagnosisName)
        {
            string strSql = @"UPDATE OPD_CommonDiagnosis SET Frequency=Frequency+1 WHERE RecordDoctorID={0} and DiagnosisName='{1}' AND DelFlag=0";
            strSql = string.Format(strSql, presDoctorID, diagnosisName);
            int iRtn = oleDb.DoCommand(strSql);
            if (iRtn > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 拷贝历史
        /// <summary>
        /// 获取处方信息
        /// </summary>
        /// <param name="presType">处方类型</param>
        /// <param name="patListId">处方明细id</param>
        /// <returns>处方信息</returns>
        public DataTable GetPresInfo(int presType, int patListId)
        {
            string strSql = @"SELECT  a.PresHeadID,b.PresNO
                                FROM    OPD_PresHead a ,
                                        OPD_PresDetail b
                                WHERE   a.PresHeadID = b.PresHeadID
                                        AND a.PatListID={0} AND a.PresType={1}
                                ORDER BY PresNO";
            strSql = string.Format(strSql, patListId, presType);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }
        #endregion

        #region 费用模板
        /// <summary>
        /// 获取费用模板头信息
        /// </summary>
        /// <param name="headId">费用模板头id</param>
        /// <returns>费用模板头信息</returns>
        public DataTable LoadMouldHead(int headId)
        {
            string strSql = @"SELECT a.MouldType,b.Name as DeptName,c.Name as DoctName FROM OPD_PresMouldHead a LEFT JOIN BaseDept b ON a.CreateDeptID=b.DeptId LEFT JOIN BaseEmployee c ON a.CreateEmpID=c.EmpId WHERE a.PresMouldHeadID={0}";
            return oleDb.GetDataTable(string.Format(strSql, headId));
        }

        /// <summary>
        /// 获取材料showcard
        /// </summary>
        /// <param name="workId">机构id</param>
        /// <returns>材料数据</returns>
        public DataTable LoadFeeInfoCard(int workId)
        {
            string strSql = @"SELECT ItemID,ItemName,Pym,Wbm,MiniUnitName,ItemClass,ItemClassName,UnitPrice,ExecDeptId,ExecDeptName FROM ViewFeeItem_SimpleList WHERE (ItemClass=2 OR ItemClass=3) AND WorkId = {0}";
            return oleDb.GetDataTable(string.Format(strSql, workId));
        }

        /// <summary>
        /// 获取费用模板信息
        /// </summary>
        /// <param name="headId">费用模板头id</param>
        /// <returns>费用模板头信息</returns>
        public DataTable LoadMouldDetail(int headId)
        {
            string strSql = @"SELECT a.PresMouldDetailID,a.ItemID,a.ItemName,a.Dosage,a.DosageUnit,a.PresAmount,a.PresAmountUnit,a.Price,(a.Price*a.PresAmount) as ItemMoney,c.Name as DeptName,d.Name as DocName FROM OPD_PresMouldDetail a LEFT JOIN OPD_PresMouldHead b ON a.PresMouldHeadID=b.PresMouldHeadID LEFT JOIN BaseDept c ON b.CreateDeptID=c.DeptId LEFT JOIN BaseEmployee d ON b.CreateEmpID=d.EmpId  WHERE a.PresMouldHeadID={0} ORDER BY a.PresMouldDetailID";
            return oleDb.GetDataTable(string.Format(strSql, headId));
        }

        /// <summary>
        /// 删除费用模板信息
        /// </summary>
        /// <param name="detailId">模板明细id</param>
        /// <returns>1成功</returns>
        public int DelDetail(int detailId)
        {
            string strSql = @"DELETE FROM OPD_PresMouldDetail WHERE PresMouldDetailID={0}";
            return oleDb.DoCommand(string.Format(strSql, detailId));
        }
        #endregion

        #region 门诊病历
        /// <summary>
        /// 符号类型
        /// </summary>
        /// <returns>符号类型数据</returns>
        public DataTable GetSymbolType()
        {
            string strsql = string.Empty;
            strsql = @"SELECT ClassId,Name FROM dbo.BaseDictClass WHERE Code BETWEEN 132 AND 138 ORDER BY SortOrder";
            DataTable dt = oleDb.GetDataTable(strsql);
            return dt;
        }

        /// <summary>
        /// 符号内容
        /// </summary>
        /// <returns>符号内容数据</returns>
        public DataTable GetSymbolContent()
        {
            string strsql = string.Empty;
            strsql = @"SELECT ClassId,id,Name as SymbolText,Code FROM dbo.BaseDictContent WHERE ClassId IN (SELECT ClassId FROM dbo.BaseDictClass WHERE Code BETWEEN 132 AND 138)";
            DataTable dt = oleDb.GetDataTable(strsql);
            return dt;
        }

        /// <summary>
        /// 取得门诊病历打印处方数据
        /// </summary>
        /// <param name="patListId">病人Id</param>
        /// <returns>门诊病历打印处方数据</returns>
        public DataTable GetOMRPrintPresData(int patListId)
        {
            string strsql = @"SELECT  b.PresDetailID  ,
                                        b.PresHeadID ,
                                        b.ItemID ,
                                        b.ItemName ,
                                       b.StatID,
                                        b.Price ,
                                        b.Entrust,
                                        b.ExecDeptID,
		                                dbo.fnGetDeptName(b.ExecDeptID) ExecDeptName,
                                        b.Spec ,
                                        b.Dosage ,
                                        b.DosageUnit ,
                                        b.Factor ,
                                        b.DoseNum ,
                                        b.ChannelID ,
                                        b.FrequencyID ,
                                        b.Days ,
                                        b.ChargeAmount ,
                                        b.ChargeUnit ,
                                        b.PresAmount ,
                                        b.PresAmountUnit ,
                                        b.PresFactor ,
                                        b.GroupID ,
                                        b.IsAst ,
                                        b.IsTake ,
                                        b.Memo ,
                                        b.PresNO ,
                                        0 OrderNO ,
		                                b.GroupSortNO,
                                        b.IsCharged ,
                                        b.IsCancel ,
                                        c.ChannelName ,
                                        d.FrequencyName ,
                                        d.ExecuteCode ,
                                        1 RoundingMode ,
                                        b.PresDoctorID ,
                                        b.PresDeptID ,
                                        dbo.fnGetEmpName(b.PresDoctorID) PresDoctorName ,
                                        dbo.fnGetDeptName(b.PresDeptID) PresDeptName,
                                        b.DropSpec,
										b.ExecNum,
										b.LinkPresDetailID,
									    b.IsEmergency,
										b.IsLunacyPosion,
										b.PresDate,
                                        (case when b.LinkPresDetailID>0 then '是' else '' end) as Additional,
                                        (CASE when a.PresType=1 then '('+ CAST(CONVERT(DECIMAL(15,2),b.Dosage) AS VARCHAR(50))+ b.DosageUnit+'  '+c.ChannelName+'  '+d.FrequencyName+'  '+CAST(b.Days AS VARCHAR(40))+'天)' ELSE '('+ CAST(CONVERT(DECIMAL(15,2),b.Dosage) AS VARCHAR(50))+ b.DosageUnit+'  '+c.ChannelName+'  '+d.FrequencyName+'  '+CAST(b.DoseNum AS VARCHAR(40))+'剂)' END) AS PresDesc
                                FROM    OPD_PresHead a
                                        INNER JOIN OPD_PresDetail b ON a.PresHeadID = b.PresHeadID
                                        LEFT JOIN Basic_Channel AS c ON b.ChannelID = c.ID
                                        LEFT JOIN Basic_Frequency AS d ON b.FrequencyID = d.FrequencyID
                                WHERE   a.PatListID = {0}
                                        AND a.PresType in(1,2)
                                ORDER BY a.PresType,b.PresNO ,
                                        b.GroupID,b.GroupSortNO";

            strsql = string.Format(strsql, patListId);
            DataTable dt = oleDb.GetDataTable(strsql);
            return dt;
        }
        #endregion

        #region 病历模板
        /// <summary>
        /// 获取病历模板数据
        /// </summary>
        /// <param name="intLevel">模板级别</param>
        /// <param name="workID">机构id</param>
        /// <param name="deptID">科室id</param>
        /// <param name="empID">人员id</param>
        /// <returns>病历模板数据</returns>
        public List<OPD_OMRTmpHead> GetOMRTemplate(int intLevel, int workID, int deptID, int empID)
        {
            string sql = @" select * from OPD_OMRTmpHead where delflag=0 and ModulLevel=" + intLevel;
            sql = sql + " and workID=" + workID;

            switch (intLevel)
            {
                case 0:
                    break;
                case 1:
                    sql = sql + " and CreateDeptID=" + deptID;
                    break;
                case 2:
                    sql = sql + " and CreateEmpID=" + empID;
                    break;
            }

            return oleDb.Query<OPD_OMRTmpHead>(sql, intLevel).ToList();
        }

        /// <summary>
        /// 检验同级别下名称是否重复
        /// </summary>
        /// <param name="name">模板名称</param>
        /// <param name="level">模板级别</param>
        /// <param name="pid">父id</param>
        /// <returns>名称数据</returns>
        public DataTable CheckMoudelName(string name, int level, int pid)
        {
            string sql = @" select * from OPD_OMRTmpHead where delflag=0 and ModulLevel=" + level;
            sql = sql + " and PID=" + pid + " and ModuldName='" + name + "'";
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 获取模板头
        /// </summary>
        /// <param name="headId">模板头id</param>
        /// <returns>模板头信息</returns>
        public DataTable LoadOMRMouldHead(int headId)
        {
            string strSql = @"SELECT a.MouldType,b.Name as DeptName,c.Name as DoctName FROM OPD_OMRTmpHead a LEFT JOIN BaseDept b ON a.CreateDeptID=b.DeptId LEFT JOIN BaseEmployee c ON a.CreateEmpID=c.EmpId WHERE a.OMRTmpHeadID={0}";
            return oleDb.GetDataTable(string.Format(strSql, headId));
        }
        #endregion

        #region 医生个人工作量统计
        /// <summary>
        /// 门诊医生个人工作量统计
        /// </summary>
        /// <param name="doctorId">医生Id</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <returns>门诊医生个人工作量统计数据</returns>
        public DataTable GetOPDoctorWorkLoad(int doctorId, DateTime bdate, DateTime edate)
        {
            try
            {
                IDbCommand cmd = oleDb.GetProcCommand("SP_OPDoctor_WorkLoad");
                oleDb.AddInParameter(cmd as DbCommand, "@WorkId", DbType.Int32, oleDb.WorkId);
                oleDb.AddInParameter(cmd as DbCommand, "@DoctorId", DbType.Int32, doctorId);
                oleDb.AddInParameter(cmd as DbCommand, "@BDate", DbType.AnsiString, Convert.ToDateTime( bdate.ToString("yyyy-MM-dd HH:mm:ss")));
                oleDb.AddInParameter(cmd as DbCommand, "@EDate", DbType.AnsiString,Convert.ToDateTime( edate.ToString("yyyy-MM-dd HH:mm:ss")));
                oleDb.AddOutParameter(cmd as DbCommand, "@ErrCode", DbType.Int32, 5);
                oleDb.AddOutParameter(cmd as DbCommand, "@ErrMsg", DbType.AnsiString, 200);
                return oleDb.GetDataTable(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 住院医生个人工作量统计
        /// </summary>
        /// <param name="doctorId">医生Id</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <returns>住院医生个人工作量统计数据</returns>
        public DataTable GetIPDoctorWorkLoad(int doctorId, DateTime bdate, DateTime edate)
        {
            try
            {
                IDbCommand cmd = oleDb.GetProcCommand("SP_IPDoctor_WorkLoad");
                oleDb.AddInParameter(cmd as DbCommand, "@WorkID", DbType.Int32, oleDb.WorkId);
                oleDb.AddInParameter(cmd as DbCommand, "@DoctorId", DbType.Int32, doctorId);
                oleDb.AddInParameter(cmd as DbCommand, "@BDate", DbType.AnsiString,Convert.ToDateTime( bdate.ToString("yyyy-MM-dd HH:mm:ss")));
                oleDb.AddInParameter(cmd as DbCommand, "@EDate", DbType.AnsiString,Convert.ToDateTime( edate.ToString("yyyy-MM-dd HH:mm:ss")));
                oleDb.AddInParameter(cmd as DbCommand, "@ColGroupType", DbType.Int32, 1);
                oleDb.AddOutParameter(cmd as DbCommand, "@ErrCode", DbType.Int32, 5);
                oleDb.AddOutParameter(cmd as DbCommand, "@ErrMsg", DbType.AnsiString, 200);
                return oleDb.GetDataTable(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
