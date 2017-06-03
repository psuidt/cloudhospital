using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_Entity.IPManage;
using HIS_IPManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPManage.ObjectModel
{
    /// <summary>
    /// 住院病人对象
    /// </summary>
    public class IpPatien : AbstractObjectModel
    {
        /// <summary>
        /// 保存病人入院登记信息
        /// </summary>
        /// <param name="ip_PatList">入院登记信息</param>
        /// <param name="ip_PatientInfo">病人基本信息</param>
        /// <param name="isNewPatient">true：新入院/false：修改病人信息</param>
        /// <param name="inpatientReg">是否为住院证登记</param>
        /// <returns>错误消息</returns>
        public string PatientRegistration(IP_PatList ip_PatList, IP_PatientInfo ip_PatientInfo, bool isNewPatient, bool inpatientReg)
        {
            if (isNewPatient)
            {
                // 检查当前会员是否已办理入院
                bool result = NewDao<IIPManageDao>().CheckPatientInTheHospital(ip_PatList.CardNO);
                if (!result)
                {
                    return "病人已在院,入院登记失败！";
                }
                // 新入院病人是否为住院证登记
                if (inpatientReg)
                {
                    // 修改住院证信息
                    NewDao<IIPManageDao>().UpdateInpatientReg(ip_PatList.MemberID);
                }
            }
            // 保存病人登记信息
            ip_PatList.OutSituation = ip_PatList.EnterSituation;
            this.BindDb(ip_PatList);
            ip_PatList.save();
            // 保存病人基本信息
            ip_PatientInfo.PatListID = ip_PatList.PatListID;
            this.BindDb(ip_PatientInfo);
            ip_PatientInfo.save();
            // 新入院病人保存诊断信息
            if (isNewPatient)
            {
                if (!string.IsNullOrEmpty(ip_PatList.EnterDiseaseName)&& !string.IsNullOrEmpty(ip_PatList.EnterDiseaseCode))
                {
                    // 保存诊断信息
                    IPD_Diagnosis diagnosis = NewObject<IPD_Diagnosis>();
                    diagnosis.PatListID = ip_PatList.PatListID;
                    diagnosis.DeptID = ip_PatList.CurrDeptID;
                    diagnosis.DgsDocID = ip_PatList.CurrDoctorID;
                    diagnosis.DiagnosisTime = DateTime.Now;
                    diagnosis.DiagnosisClass = 67329;
                    diagnosis.Main = 0;
                    diagnosis.DiagnosisName = ip_PatList.EnterDiseaseName;
                    diagnosis.DiagnosisID = 0;
                    diagnosis.ICDCode = ip_PatList.EnterDiseaseCode;
                    this.BindDb(diagnosis);
                    diagnosis.save();
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 取消入院
        /// </summary>
        /// <param name="patListID">病人登记Id</param>
        /// <param name="patName">病人名</param>
        /// <returns>true:取消成功</returns>
        public bool CancelAdmission(int patListID, string patName)
        {
            DataTable dt = NewDao<IIPManageDao>().GetPatientCostList(patListID);
            // 病人入院已产生费用
            if (dt != null && dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[0]["DepoCount"].ToString()) > 0 ||
                    Convert.ToInt32(dt.Rows[0]["FeeCount"].ToString()) > 0 ||
                    Convert.ToInt32(dt.Rows[0]["BedInfoCount"].ToString()) > 0)
                {
                    return false;
                }
            }

            return NewDao<IIPManageDao>().CancelAdmission(patListID, patName);
        }

        /// <summary>
        /// 会员号读卡入院
        /// </summary>
        /// <param name="cardNO">会员卡号</param>
        /// <param name="ip_Patient">病人基本信息</param>
        /// <param name="ipList">病人登记信息</param>
        /// <param name="deposit">预交金信息</param>
        /// <returns>true:已开住院证/false：未开住院证</returns>
        public bool QueryMemberInfo(int memberID, IP_PatientInfo ip_Patient, IP_PatList ipList, out decimal deposit)
        {
            deposit = 0;
            bool inpatientReg = false;
            // 根据会员卡号读取会员ID
            DataTable dtPatInfo = NewDao<IIPManageDao>().QueryMemberInfo(memberID);
            // 检查是否为二次入院
            DataTable dtCaseNumber = NewDao<IIPManageDao>().GetCaseNumberByCardNO(memberID);
            string caseNumber = string.Empty;
            int times = 1;
            if (dtCaseNumber != null && dtCaseNumber.Rows.Count > 0)
            {
                caseNumber = dtCaseNumber.Rows[0]["CaseNumber"].ToString();
                times = dtCaseNumber.Rows.Count + 1;
            }
            
            // 根据会员ID查询是否当前会员是否已开住院证
            if (dtPatInfo == null || dtPatInfo.Rows.Count == 0)
            {
                throw new Exception("找不到该卡号病人信息");
            }
            else
            {
                DataTable inpatientRegDt = NewDao<IIPManageDao>().GetInpatientReg(Convert.ToInt32(dtPatInfo.Rows[0]["MemberID"]));
                if (inpatientRegDt != null && inpatientRegDt.Rows.Count > 0)
                {
                    //  已开住院证
                    inpatientReg = true;
                    if (inpatientRegDt.Rows[0]["Deposit"] != null && inpatientRegDt.Rows[0]["Deposit"] != DBNull.Value)
                    {
                        if (Convert.ToDecimal(inpatientRegDt.Rows[0]["Deposit"]) > 0)
                        {
                            deposit = Convert.ToDecimal(inpatientRegDt.Rows[0]["Deposit"]);
                        }
                    }
                }

                SetPatientInfo(ip_Patient, dtPatInfo);
                SetPatListInfo(ipList, dtPatInfo, caseNumber, times, inpatientReg, inpatientRegDt);
            }

            return inpatientReg;
        }

        /// <summary>
        /// 做成病人登记基本信息
        /// </summary>
        /// <param name="ipList">病人登记基本信息</param>
        /// <param name="dtMemberInfo">会员基本信息</param>/// 
        /// <param name="caseNumber">住院病案号</param>
        /// <param name="times">住院次数</param>
        /// <param name="inpatientReg">是否已开住院证</param>
        /// <param name="inpatientRegDt">住院证信息</param>
        private void SetPatListInfo(IP_PatList ipList, DataTable dtMemberInfo, string caseNumber, int times, bool inpatientReg, DataTable inpatientRegDt)
        {
            ipList.MemberID = int.Parse(dtMemberInfo.Rows[0]["MemberID"].ToString());
            ipList.MemberAccountID = Convert.ToInt32(dtMemberInfo.Rows[0]["AccountID"].ToString());//AccountID
            ipList.CardNO = dtMemberInfo.Rows[0]["CardNO"].ToString();
            ipList.CaseNumber = caseNumber;
            ipList.Times = times;
            ipList.Sex = dtMemberInfo.Rows[0]["SexCode"].ToString();
            ipList.MakerDate = DateTime.Now;  // 登记时间
            ipList.Birthday = !string.IsNullOrEmpty(dtMemberInfo.Rows[0]["Birthday"].ToString()) ?
                DateTime.Parse(dtMemberInfo.Rows[0]["Birthday"].ToString()) : DateTime.Now;   // 出生日期
            ipList.PatDatCardNo = dtMemberInfo.Rows[0]["CardNO"].ToString();  // 诊疗卡号
            ipList.PatName = dtMemberInfo.Rows[0]["MemberName"].ToString(); // 病人姓名
            ipList.PatTypeID = int.Parse(dtMemberInfo.Rows[0]["PatTypeID"].ToString()); // 病人类型ID
            ipList.Sex = dtMemberInfo.Rows[0]["Sex"].ToString();
            ipList.MedicareCard = dtMemberInfo.Rows[0]["MedicareCard"].ToString();
            ipList.EnterHDate = DateTime.Now;
            ipList.EnterSituation = "3";
            if (inpatientReg)
            {
                ipList.EnterDiseaseCode = inpatientRegDt.Rows[0]["HospitalCode"].ToString();// 入院诊断
                ipList.EnterSituation = inpatientRegDt.Rows[0]["ConditionStu"].ToString();// 入院情况
                ipList.EnterDeptID = Convert.ToInt32(inpatientRegDt.Rows[0]["InDeptID"]); // 入院科室
                ipList.CurrDeptID = Convert.ToInt32(inpatientRegDt.Rows[0]["InDeptID"]); // 入院科室
                ipList.DietType = inpatientRegDt.Rows[0]["Diet"].ToString(); // 饮食级别
            }
        }

        /// <summary>
        /// 做成病人基本信息
        /// </summary>
        /// <param name="ip_Patient">病人基本信息</param>
        /// <param name="dtMemberInfo">会员基本信息</param>
        private void SetPatientInfo(IP_PatientInfo ip_Patient, DataTable dtMemberInfo)
        {
            ip_Patient.IdentityNum = dtMemberInfo.Rows[0]["IDNumber"].ToString(); // 身份证号
            ip_Patient.Nationality = dtMemberInfo.Rows[0]["Nationality"].ToString(); // 国籍
            ip_Patient.Nation = dtMemberInfo.Rows[0]["Nation"].ToString(); // 名族
            ip_Patient.Occupation = dtMemberInfo.Rows[0]["Occupation"].ToString(); // 职业
            ip_Patient.CulturalLevel = dtMemberInfo.Rows[0]["Degree"].ToString(); // 文化程度
            ip_Patient.Birthplace = dtMemberInfo.Rows[0]["CityCode"].ToString(); // 出生地址
            ip_Patient.DRegisterAddr = dtMemberInfo.Rows[0]["CityCode"].ToString(); // 户籍地址
            ip_Patient.NAddress = dtMemberInfo.Rows[0]["CityCode"].ToString(); // 现住地址
            ip_Patient.Phone = dtMemberInfo.Rows[0]["Mobile"].ToString();// 联系电话
            ip_Patient.UnitName = dtMemberInfo.Rows[0]["WorkUnit"].ToString();// 单位名称
            ip_Patient.UnitPhone = dtMemberInfo.Rows[0]["WorkTele"].ToString(); // 单位电话
            ip_Patient.RelationName = dtMemberInfo.Rows[0]["RelationName"].ToString(); // 联系人
            ip_Patient.Relation = dtMemberInfo.Rows[0]["Relation"].ToString();  // 关系
            ip_Patient.RPhone = dtMemberInfo.Rows[0]["RelationTele"].ToString(); // 联系人电话
            ip_Patient.Matrimony = dtMemberInfo.Rows[0]["Matrimony"].ToString();
        }
    }

    /// <summary>
    /// 病人状态
    /// </summary>
    public enum PatientStatus
    {
        新入院, 在床, 出院未结算, 出院结算, 取消入院
    }
}
