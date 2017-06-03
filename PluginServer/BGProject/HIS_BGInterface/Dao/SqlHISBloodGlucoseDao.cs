using EFWCoreLib.CoreFrame.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS_Entity.IPManage;

namespace HIS_BGInterface.Dao
{
    public class SqlHISBloodGlucoseDao : AbstractDao, IHISBloodGlucoseDao
    {
        /// <summary>
        /// 1.获取在院患者主索引基本信息 ok
        /// </summary>
        /// <returns></returns>
        public DataTable GetInPatientBaseInfo(int iWorkId)
        {
            try
            {
                /*
                patientId	(32)	N	病人唯一标识号，可以由用户赋予具体的含义，如：病案号，门诊号等
                visitId	(16)	N	住院号（可选的病人标识）
                name	128	N	姓名
                sex	1	Y	性别（1男2女0未知）
                height		Y	身高（CM)
                weight		Y	体重（kg)
                birthday	date	Y	出生日期
                birthplace	12	Y	出生地
                idNo	18	N	身份证号
                mailingAddress	128	Y	通讯地址
                zipCode	6	Y	邮政编码
                phoneNumberHome	16	Y	家庭电话
                phoneNumberBusiness	16	Y	工作电话
                nextOfKin	128	Y	亲属姓名
                relationship	11	Y	关系
                nextOfKinAddr	128	Y	亲属地址
                nextOfKinZipCode	6	Y	亲属邮编
                nextOfKinPhone	16	Y	亲属电话
                */
                if (iWorkId == -1)
                {
                    iWorkId = oleDb.WorkId;
                }
                string strsql = @"SELECT	b.PatListID patientId,
		                                   cast(b.SerialNumber as varchar) visitId,
		                                    b.PatName NAME,
		                                    b.sex	,
                                            0 height,
                                            0 weight,
                                            b.birthday,
                                            a.birthplace,
                                            a.IdentityNum idNo,
                                            a.NAddressDetail mailingAddress,
                                            a.NZipCode zipCode,
                                            a.Phone phoneNumberHome,
                                            a.UnitPhone phoneNumberBusiness,
                                            a.RelationName nextOfKin,
                                            a.Relation relationship,
                                            a.RAddressDetail nextOfKinAddr,
                                            '' nextOfKinZipCode,
                                            a.RPhone nextOfKinPhone
                                  FROM dbo.IP_PatientInfo a
                            INNER JOIN dbo.IP_PatList b ON a.PatListID=b.PatListID AND a.WorkID=b.WorkID
                                WHERE a.WorkId={0} 
    							 AND  EXISTS(SELECT 1 FROM IP_BedInfo c WHERE c.PatListID=b.PatListID) ";
                strsql = string.Format(strsql, iWorkId);
                return oleDb.GetDataTable(strsql);
            }
            catch (Exception e)
            {
                throw new Exception("err");
            }
        }

        /// <summary>
        /// 2.获取在院患者住院信息 ok
        /// </summary>
        /// <returns></returns>
        public DataTable GetInPatientHospitalizationInfo(int iWorkId)
        {
            try
            {
                /*
                patientId	(32)	N	病人唯一标识号，可以由用户赋予具体的含义，如：病案号，门诊号等
                visitId	(16)	N	住院号（可选的病人标识）
                wardId	128	N	病区
                admDateTime	DATE	N	入院时间
                admWardDateTime	DATE	Y	入科时间
                diagnosis	128	Y	入院诊断
                doctorInCharge	32	N	主治医生（同步用户的用户ID）
                bedCode	32	N	床位号（与病区ID确定唯一床位）
                */
                if (iWorkId == -1)
                {
                    iWorkId = oleDb.WorkId;
                }
                string strsql = @"SELECT	b.PatListID patientId,
		                                   cast(b.SerialNumber as varchar) visitId,
		                                    b.CurrWardID wardId,
		                                    b.EnterHDate admDateTime,
		                                    b.MakerDate admWardDateTime,
		                                    b.EnterDiseaseName diagnosis,
		                                    b.CurrDoctorID doctorInCharge,
		                                    b.BedNo bedCode
                                  FROM dbo.IP_PatientInfo a
                            INNER JOIN dbo.IP_PatList b ON a.PatListID=b.PatListID AND a.WorkID=b.WorkID
                                WHERE a.WorkId={0} 
                                    AND  EXISTS(SELECT 1 FROM IP_BedInfo c WHERE c.PatListID=b.PatListID) ";
                strsql = string.Format(strsql, iWorkId);
                return oleDb.GetDataTable(strsql);
            }
            catch (Exception e)
            {
                throw new Exception("err");
            }
        }

        /// <summary>
        /// 3.获取在用科室信息 ok
        /// </summary>
        /// <returns></returns>
        public DataTable GetWorkDeptInfo(int iWorkId)
        {
            try
            {
                /*
                deptId	(32)	N	科室主键
                deptName	128	N	科室名称
                remark	128	Y	备注说明
                */
                if (iWorkId == -1)
                {
                    iWorkId = oleDb.WorkId;
                }
                string strsql = @"SELECT a.deptId,a.Name deptName,ISNULL(a.Memo,'') remark 
                                FROM BaseDept a 
                           INNER JOIN BaseDeptDetails b ON a.DeptId=b.DeptID
                                WHERE a.WorkId={0} AND a.DelFlag=0 
                                ORDER BY a.SortOrder ASC";
                strsql = string.Format(strsql, iWorkId);
                return oleDb.GetDataTable(strsql);
            }
            catch (Exception e)
            {
                throw new Exception("err");
            }
        }

        /// <summary>
        /// 4.获取在用病区信息 ok
        /// </summary>
        /// <returns></returns>
        public DataTable GetWorkAreaInfo(int iWorkId)
        {
            try
            {
                /*
                wardId	(32)	N	主键ID
                deptId	128	N	所属科室ID
                remark	128	Y	备注说明
                wardName	128	N	病区名称
                */
                if (iWorkId == -1)
                {
                    iWorkId = oleDb.WorkId;
                }
                string strsql = @"SELECT WardID,(SELECT DeptID FROM BaseWardDept WHERE WardID=a.WardID ) deptId,ISNULL(Memo,'') remark,WardName
                                FROM BaseWard a 
                                WHERE a.WorkId={0} AND a.DelFlag=0";
                strsql = string.Format(strsql, iWorkId);
                return oleDb.GetDataTable(strsql);
            }
            catch (Exception e)
            {
                throw new Exception("err");
            }
        }

        /// <summary>
        /// 5.获取在用床位信息 ok
        /// </summary>
        /// <returns></returns>
        public DataTable GetWorkBedInfo(int iWorkId)
        {
            try
            {
                /*
                id	(32)	N	主键ID
                bedCode	128	N	床位编码
                wardId	128	N	病区ID
                priority	128	Y	床位排序
                */
                if (iWorkId == -1)
                {
                    iWorkId = oleDb.WorkId;
                }
                string strsql = @"SELECT BedID as id,BedNO as bedCode,WardID,'' priority
                                FROM IP_BedInfo
                                WHERE WorkId={0} AND IsStoped=0";
                strsql = string.Format(strsql, iWorkId);
                return oleDb.GetDataTable(strsql);
            }
            catch (Exception e)
            {
                throw new Exception("err");
            }

        }

        /// <summary>
        /// 6.获取在用用户信息 ok
        /// </summary>
        /// <returns></returns>
        public DataTable GetWorkUserInfo(int iWorkId)
        {
            try
            {
                /*
                userId	(32)	N	主键ID
                nickname	128	N	用户名
                account	128	N	用户登录名
                email	128	Y	邮件
                roleType		N	角色类型（0护士，1医生， 3 其他）
                deptId		N	所属病区
                */
                if (iWorkId == -1)
                {
                    iWorkId = oleDb.WorkId;
                }
                string strsql = @"SELECT b.EmpId as userId,b.Name as nickname,a.Code as account,'' as email,(case UserType when 3 then 0 when 2 then 1 else 3 end) as roleType ,d.WardID deptId
                                FROM BaseUser a 
                          INNER JOIN BaseEmployee b ON a.EmpID=b.EmpId and a.WorkId=b.WorkId
                          INNER JOIN BaseEmpDept c ON a.EmpID=c.EmpId  and a.WorkId=c.WorkId and c.DefaultFlag=1
                           LEFT JOIN dbo.BaseWardDept d ON c.DeptId=d.DeptID AND c.WorkId=d.WorkID 
                                WHERE a.WorkId={0} AND a.Lock=0 AND b.DelFlag=0";
                strsql = string.Format(strsql, iWorkId);
                return oleDb.GetDataTable(strsql);
            }
            catch (Exception e)
            {
                throw new Exception("err");
            }
        }

        /// <summary>
        /// 获取血糖记录ID
        /// </summary>
        /// <param name="record">血糖记录</param>
        /// <returns></returns>
        public int GetPluRecordID(IP_PluRecord record)
        {
            string strsql = @"SELECT top 1 ID FROM IP_PluRecord WHERE PatlistID={0}  AND BloodID={1}";
            object val = oleDb.GetDataResult(string.Format(strsql, record.PatlistID, record.BloodID));
            if (val == null)
                return 0;
            else
                return Convert.ToInt32(val);
        }
    }
}
