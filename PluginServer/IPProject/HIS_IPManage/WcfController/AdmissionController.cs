using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.IPManage;
using HIS_Entity.MemberManage;
using HIS_IPManage.Dao;
using HIS_IPManage.ObjectModel;
using HIS_OPManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPManage.WcfController
{
    /// <summary>
    /// 入院登记控制器
    /// </summary>
    [WCFController]
    public class AdmissionController : WcfServerController
    {
        /// <summary>
        /// 住院病人列表取得
        /// </summary>
        /// <returns>住院病人列表</returns>
        [WCFMethod]
        public ServiceResponseData GetPatientList()
        {
            DateTime startTime = requestData.GetData<DateTime>(0); // 开始时间
            DateTime endTime = requestData.GetData<DateTime>(1);  // 结束时间
            int dept = requestData.GetData<int>(2);  // 入院科室
            int patType = requestData.GetData<int>(3);  // 病人类型
            string selectParm = requestData.GetData<string>(4);  // 检索条件(住院号、病案号、床位号)
            string patStatus = requestData.GetData<string>(5); // 病人状态
            bool isPay = requestData.GetData<bool>(6); // 预交金界面病人列表查询

            // 取得住院病人列表
            DataTable patDt = NewDao<IIPManageDao>().GetPatientList(
                startTime, 
                endTime,
                dept, 
                patType, 
                selectParm, 
                patStatus, 
                isPay);
            responseData.AddData(patDt);
            return responseData;
        }

        /// <summary>
        /// 获取病人入院信息，登记信息
        /// </summary>
        /// <returns>住院病人登记信息</returns>
        [WCFMethod]
        public ServiceResponseData GetPatientInfo()
        {
            int patientID = requestData.GetData<int>(0);
            int patListID = requestData.GetData<int>(1);
            DataTable patientInfoDt = NewDao<IIPManageDao>().GetPatientInfo(patientID);
            responseData.AddData(patientInfoDt);
            DataTable patListInfoDt = NewDao<IIPManageDao>().GetPatListInfo(patListID);
            responseData.AddData(patListInfoDt);
            return responseData;
        }

        /// <summary>
        /// 保存新入院病人信息
        /// </summary>
        /// <returns>保存成功或失败</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SavePatientInfo()
        {
            // 是否为新入院病人
            bool isNewPatient = requestData.GetData<bool>(2);
            // 住院登记记录信息
            IP_PatList ip_PatList = requestData.GetData<IP_PatList>(1);
            // 是否为二次入院病人Flg
            bool twoAdmission = requestData.GetData<bool>(3);
            bool inpatientReg = requestData.GetData<bool>(4);
            // 新入院病人需要生成流水号和病案号
            if (isNewPatient)
            {
                ip_PatList.SerialNumber = decimal.Parse(NewObject<SerialNumberSource>().GetSerialNumber(SnType.住院流水号));
                //if (!twoAdmission)
                //{
                //    ip_PatList.CaseNumber = NewObject<SerialNumberSource>().GetSerialNumber(SnType.病案号);
                //}
            }
            // 住院病人信息
            IP_PatientInfo ip_PatientInfo = requestData.GetData<IP_PatientInfo>(0);
            string result = NewObject<IpPatien>().PatientRegistration(ip_PatList, ip_PatientInfo, isNewPatient, inpatientReg);
            if (string.IsNullOrEmpty(result))
            {
                if (isNewPatient)
                {
                    #region "保存业务消息数据 --Add By ZhangZhong"
                    // 保存业务消息数据
                    Dictionary<string, string> msgDic = new Dictionary<string, string>();
                    int workId = requestData.GetData<int>(5);
                    int userId = requestData.GetData<int>(6);
                    int deptId = requestData.GetData<int>(7);
                    msgDic.Add("WorkID", workId.ToString()); // 消息机构ID
                    msgDic.Add("SendUserId", userId.ToString()); // 消息生成人ID
                    msgDic.Add("SendDeptId", deptId.ToString()); // 消息生成科室ID
                    msgDic.Add("PatListID", ip_PatList.PatListID.ToString()); // 病人登记ID
                    NewObject<BusinessMessage>().GenerateBizMessage(MessageType.病人新入院, msgDic);
                    #endregion
                }
            }

            responseData.AddData(result);
            responseData.AddData(ip_PatList.PatListID);
            if (isNewPatient)
            {
                responseData.AddData(ip_PatList.SerialNumber);
            }

            return responseData;
        }

        /// <summary>
        /// 取消入院
        /// </summary>
        /// <returns>取消成功或失败</returns>
        [WCFMethod]
        public ServiceResponseData CancelAdmission()
        {
            int patListID = requestData.GetData<int>(0);
            string patName = requestData.GetData<string>(1);
            responseData.AddData(NewObject<IpPatien>().CancelAdmission(patListID, patName));
            return responseData;
        }

        /// <summary>
        /// 取得所有住院临床科室
        /// </summary>
        /// <returns>住院临床科室列表</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptBasicData()
        {
            bool isAll = requestData.GetData<bool>(0);
            // 调用共通接口查询科室列表
            DataTable deptDt = NewObject<BasicDataManagement>().GetBasicData(DeptDataSourceType.住院临床科室, isAll);
            responseData.AddData(deptDt);
            return responseData;
        }

        /// <summary>
        /// 取得病人类型列表
        /// </summary>
        /// <returns>病人类型列表</returns>
        [WCFMethod]
        public ServiceResponseData GetPatType()
        {
            bool isAll = requestData.GetData<bool>(0);
            // 调用共通接口查询病人类型列表
            DataTable patTypeList = NewObject<BasicDataManagement>().GetPatType(isAll);
            responseData.AddData(patTypeList);
            return responseData;
        }

        /// <summary>
        /// 病人信息相关，带“全部”项的下拉选项
        /// </summary>
        /// <returns>病人国籍列表</returns>
        [WCFMethod]
        public ServiceResponseData GetBasicData()
        {
            int dataType = requestData.GetData<int>(0);  // 病人信息相关
            DataTable basicDataDt = NewObject<BasicDataManagement>().GetBasicData((PatientInfoDataSourceType)dataType, false);
            responseData.AddData(basicDataDt);
            return responseData;
        }

        /// <summary>
        /// 获取下拉框基础数据
        /// </summary>
        /// <returns>基础数据列表</returns>
        [WCFMethod]
        public ServiceResponseData GetMasterData()
        {
            DataTable patNationalityDt = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.国籍, false);
            responseData.AddData(patNationalityDt);

            DataTable nationDt = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.民族, false);
            responseData.AddData(nationDt);

            DataTable patJobDt = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.职业, false);
            responseData.AddData(patJobDt);

            DataTable culturalLevelDt = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.教育程度, false);
            responseData.AddData(culturalLevelDt);

            DataTable matrimonyDt = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.婚姻状况, false);
            responseData.AddData(matrimonyDt);

            DataTable relationDt = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.关系, false);
            responseData.AddData(relationDt);

            DataTable patSexDt = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.性别, false);
            responseData.AddData(patSexDt);

            DataTable enterSituationDt = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.入院情况, false);
            responseData.AddData(enterSituationDt);

            DataTable regionCodeDt = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.地区编码, false);
            responseData.AddData(regionCodeDt);

            DataTable sourceWayDt = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.病人来源, false);
            responseData.AddData(sourceWayDt);

            // 获取病区列表
            DataTable enterWardDt = NewObject<BasicDataManagement>().GetBasicData(DeptDataSourceType.住院病区, false);
            responseData.AddData(enterWardDt);

            // 获取所有住院临床科室
            DataTable deptList = NewObject<BasicDataManagement>().GetBasicData(DeptDataSourceType.住院临床科室, false);
            responseData.AddData(deptList);

            // 获取所有病人类型
            DataTable patTypeDt = NewObject<BasicDataManagement>().GetPatType(false);
            responseData.AddData(patTypeDt);

            // 获取诊断列表
            DataTable diseaseDt = NewObject<BasicDataManagement>().GetDisease();
            responseData.AddData(diseaseDt);

            // 获取医生列表
            DataTable currDoctorDt = NewObject<BasicDataManagement>().GetBasicData(EmpDataSourceType.医生, false);
            responseData.AddData(currDoctorDt);

            // 获取护士列表
            DataTable currNurseDt = NewObject<BasicDataManagement>().GetBasicData(EmpDataSourceType.护士, false);
            responseData.AddData(currNurseDt);
            return responseData;
        }

        /// <summary>
        /// 获取诊断
        /// </summary>
        /// <returns>诊断列表</returns>
        [WCFMethod]
        public ServiceResponseData GetDisease()
        {
            DataTable diseaseDt = NewObject<BasicDataManagement>().GetDisease();
            responseData.AddData(diseaseDt);
            return responseData;
        }

        /// <summary>
        /// 获取医生或者护士列表
        /// </summary>
        /// <returns>医生或者护士列表</returns>
        [WCFMethod]
        public ServiceResponseData GetEmpDataSourceType()
        {
            int dataType = requestData.GetData<int>(0);  // 用户类型
            DataTable empDataDt = NewObject<BasicDataManagement>().GetBasicData((EmpDataSourceType)dataType, false);
            responseData.AddData(empDataDt);
            return responseData;
        }

        /// <summary>
        /// 取得病区列表
        /// </summary>
        /// <returns>病区列表</returns>
        [WCFMethod]
        public ServiceResponseData GetWardDept()
        {
            DataTable basicDataDt = NewObject<BasicDataManagement>().GetBasicData(DeptDataSourceType.住院病区, false);
            responseData.AddData(basicDataDt);
            return responseData;
        }

        /// <summary>
        /// 获取预交金票据号
        /// </summary>
        /// <returns>预交金票据号</returns>
        [WCFMethod]
        public ServiceResponseData GetInvoiceCurNO()
        {
            int operatorId = requestData.GetData<int>(0);  // 入院科室
            string perfChar = string.Empty;
            string billNumber = NewObject<InvoiceManagement>().GetInvoiceCurNo(InvoiceType.住院预交金, operatorId, out perfChar);
            responseData.AddData(billNumber);
            return responseData;
        }

        /// <summary>
        /// 获取预交金支付方式
        /// </summary>
        /// <returns>预交金支付方式</returns>
        [WCFMethod]
        public ServiceResponseData GetPaymentMethod()
        {
            DataTable paymentMethodDt = NewDao<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.预交金支付方式, false);
            responseData.AddData(paymentMethodDt);
            return responseData;
        }

        /// <summary>
        /// 缴纳预交金
        /// </summary>
        /// <returns>收取成功或失败</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData PayADeposit()
        {
            // 住院登记记录信息
            IP_DepositList depositList = requestData.GetData<IP_DepositList>(0);
            string msg = NewObject<DepositManagement>().PayADeposit(depositList);
            if (string.IsNullOrEmpty(msg))
            {
                string perfChar = string.Empty;
                NewObject<InvoiceManagement>().GetInvoiceCurNOAndUse(InvoiceType.住院预交金, depositList.MakerEmpID, out perfChar);
            }

            responseData.AddData(msg);
            responseData.AddData(depositList.DepositID);
            return responseData;
        }

        /// <summary>
        /// 通过姓名，电话号码，身份证号组合条件或取会员信息
        /// </summary>
        /// <returns>会员信息</returns>
        [WCFMethod]
        public ServiceResponseData GetMemberInfoByOther()
        {
            string strPatInfo = requestData.GetData<string>(0);
            DataTable dtPatinfo = NewObject<IOPManageDao>().GetMemberInfoByQueryConte(strPatInfo);
            responseData.AddData(dtPatinfo);
            return responseData;
        }

        /// <summary>
        /// 通过卡号获取会员基本信息
        /// </summary>
        /// <returns>会员基本信息</returns>
        [WCFMethod]
        public ServiceResponseData QueryMemberInfo()
        {
            int member = requestData.GetData<int>(0);
            //string cardNO = requestData.GetData<string>(0);
            decimal deposit = 0;
            IP_PatientInfo ip_Patient = NewObject<IP_PatientInfo>();
            IP_PatList ipList = NewObject<IP_PatList>();
            bool result = NewObject<IpPatien>().QueryMemberInfo(member, ip_Patient, ipList, out deposit);
            responseData.AddData(result);
            responseData.AddData(ip_Patient);
            responseData.AddData(ipList);
            responseData.AddData(deposit);
            return responseData;
        }

        /// <summary>
        /// 通过ID获取预交金信息
        /// </summary>
        /// <returns>预交金信息</returns>
        [WCFMethod]
        public ServiceResponseData GetPayADeposit()
        {
            int depositID = requestData.GetData<int>(0);
            DataTable dtPaymentInfo = NewObject<IIPManageDao>().GetPayADeposit(depositID);//卡号
            responseData.AddData(dtPaymentInfo);
            return responseData;
        }

        /// <summary>
        /// 增加打印次数
        /// </summary>
        /// <returns>打印次数修改成功或失败</returns>
        [WCFMethod]
        public ServiceResponseData UpdatePrintTime()
        {
            int depositID = requestData.GetData<int>(0);
            int printtime = NewObject<IIPManageDao>().UpdatePrintTime(depositID);//卡号
            responseData.AddData(printtime);
            return responseData;
        }

        /// <summary>
        /// 获取卡类型列表
        /// </summary>
        /// <returns>卡类型列表</returns>
        [WCFMethod]
        public ServiceResponseData RegDataInit()
        {
            DataTable dtCardType = NewObject<ME_CardTypeList>().gettable(" UseFlag=1");
            responseData.AddData(dtCardType);//卡类型
            return responseData;
        }
    }
}
