using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.MIManage.Common;
using HIS_IPManage.Dao;
using HIS_IPManage.ObjectModel;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPManage.WcfController
{
    /// <summary>
    /// 床位管理控制器
    /// </summary>
    [WCFController]
    public class BedManagementController : WcfServerController
    {
        /// <summary>
        /// 根据病区ID查询床位以及床位分配情况
        /// </summary>
        /// <returns>床位列表</returns>
        [WCFMethod]
        public ServiceResponseData GetBedManageList()
        {
            int wardId = requestData.GetData<int>(0);
            DataTable bedDt = NewDao<IIPManageDao>().GetBedManageList(wardId);
            responseData.AddData(bedDt);
            return responseData;
        }

        /// <summary>
        /// 取得所有未分配床位的病人
        /// </summary>
        /// <returns>未分配床位病人列表</returns>
        [WCFMethod]
        public ServiceResponseData GetNotHospitalPatList()
        {
            int wardid = requestData.GetData<int>(0);
            string status = requestData.GetData<string>(1);
            DataTable dt = NewDao<IIPManageDao>().GetNotHospitalPatList(wardid, status);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取转科病人列表
        /// </summary>
        /// <returns>转科病人列表</returns>
        [WCFMethod]
        public ServiceResponseData GetTransferPatList()
        {
            int wardid = requestData.GetData<int>(0);
            DataTable dt = NewDao<IIPManageDao>().GetTransferPatList(wardid);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 根据病区ID 床位号查询床位绑定的医生护士ID
        /// </summary>
        /// <returns>医生护士ID</returns>
        [WCFMethod]
        public ServiceResponseData GetDoctorNurseID()
        {
            int wardId = requestData.GetData<int>(0);
            string bedNo = requestData.GetData<string>(1);
            DataTable dt = NewDao<IIPManageDao>().GetDoctorNurseID(wardId, bedNo);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 根据病区ID 床位号查询床位绑定的医生护士ID
        /// </summary>
        /// <returns>医生护士ID</returns>
        [WCFMethod]
        public ServiceResponseData GetPatDoctorNurseID()
        {
            int wardId = requestData.GetData<int>(0);
            string bedNo = requestData.GetData<string>(1);
            DataTable dt = NewDao<IIPManageDao>().GetPatDoctorNurseID(wardId, bedNo);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 保存床位分配数据
        /// </summary>
        /// <returns>保存成功或失败</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveBedAllocation()
        {
            DataTable tempDt = requestData.GetData<DataTable>(0);
            int bedId = requestData.GetData<int>(1);
            int wardId = requestData.GetData<int>(2);
            string bedNo = requestData.GetData<string>(3);
            int assignEmpID = requestData.GetData<int>(4);
            string result = NewObject<BedManagement>().SaveBedAllocation(tempDt, bedId, wardId, bedNo, assignEmpID);
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 取消分床
        /// </summary>
        /// <returns>床位取消结果</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData CancelTheBed()
        {
            int patListId = requestData.GetData<int>(0);
            int wardId = requestData.GetData<int>(1);
            string bedNo = requestData.GetData<string>(2);
            bool result = NewObject<BedManagement>().CancelTheBed(patListId, wardId, bedNo);
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 取消包床
        /// </summary>
        /// <returns>包床取消结果</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData CancelPackBed()
        {
            int patListId = requestData.GetData<int>(0);
            int wardId = requestData.GetData<int>(1);
            string bedNo = requestData.GetData<string>(2);
            string result = NewObject<BedManagement>().CancelPackBed(patListId, wardId, bedNo);
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 病人换床--取得当前病区下所有空床床位号
        /// </summary>
        /// <returns>当前病区下所有空床床位号</returns>
        [WCFMethod]
        public ServiceResponseData GetBedNoList()
        {
            int wardId = requestData.GetData<int>(0);
            DataTable dt = NewDao<IIPManageDao>().GetBedNoList(wardId);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 病人换床--保存换床数据
        /// </summary>
        /// <returns>换床数据保存结果</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveBedChanging()
        {
            string newBedNo = requestData.GetData<string>(0);
            string oldBedNo = requestData.GetData<string>(1);
            int patListId = requestData.GetData<int>(2);
            int wardID = requestData.GetData<int>(3);
            int empId = requestData.GetData<int>(4);
            int workID = requestData.GetData<int>(5);
            string result = NewObject<BedManagement>().SaveBedChanging(newBedNo, patListId, wardID, oldBedNo, empId, workID);
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 修改医生或护士--保存新的医生护士数据
        /// </summary>
        /// <returns>新的病床信息保存结果true：成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveUpdatePatient()
        {
            int patListId = requestData.GetData<int>(0);
            int wardId = requestData.GetData<int>(1);
            string bedNo = requestData.GetData<string>(2);
            int doctorId = requestData.GetData<int>(3);
            int nurseId = requestData.GetData<int>(4);
            bool result = NewObject<BedManagement>().SaveUpdatePatient(patListId, wardId, bedNo, doctorId, nurseId);
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 包床--查询所有已分配床位的病人
        /// </summary>
        /// <returns>所有已分配床位的病人</returns>
        [WCFMethod]
        public ServiceResponseData GetInTheHospitalPatList()
        {
            int wardId = requestData.GetData<int>(0);
            DataTable result = NewDao<IIPManageDao>().GetInTheHospitalPatList(wardId);
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 保存包床数据
        /// </summary>
        /// <returns>true：保存成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SavePackBedData()
        {
            string newBedNo = requestData.GetData<string>(0);
            int wardId = requestData.GetData<int>(1);
            string oldBedNo = requestData.GetData<string>(2);
            int empId = requestData.GetData<int>(3);
            int workID = requestData.GetData<int>(4);
            string result = NewObject<BedManagement>().SavePackBedData(newBedNo, wardId, oldBedNo, empId, workID);
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 病人定义出院
        /// </summary>
        /// <returns>true：定义出院成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData PatientOutHospital()
        {
            int patListId = requestData.GetData<int>(0);
            int wardId = requestData.GetData<int>(1);
            int deptId = requestData.GetData<int>(2);
            bool result = false;
            DataTable resultDt = NewObject<BedManagement>().PatientOutHospital(wardId, patListId, out result, deptId);
            responseData.AddData(result);
            responseData.AddData(resultDt);
            return responseData;
        }

        /// <summary>
        /// 住院护士站获取病人列表
        /// </summary>
        /// <returns>病人列表</returns>
        [WCFMethod]
        public ServiceResponseData GetNursingStationPatList()
        {
            int status = requestData.GetData<int>(0);
            int deptID = requestData.GetData<int>(1);
            DateTime startDate = requestData.GetData<DateTime>(2);
            DateTime endDate = requestData.GetData<DateTime>(3);
            bool isReminder = requestData.GetData<bool>(4);
            // 获取催款系统参数
            string strIsReminder = NewObject<SysConfigManagement>().GetSystemConfigValue("IsReminder");
            DataTable patListDt = NewDao<IIPManageDao>().GetNursingStationPatList(status, deptID, startDate, endDate, isReminder, Tools.ToDecimal(strIsReminder, 0));
            responseData.AddData(patListDt);
            return responseData;
        }

        /// <summary>
        /// 获取催款单配置数据
        /// </summary>
        /// <returns>催款单配置数据</returns>
        [WCFMethod]
        public ServiceResponseData GetReminderConfigInfo()
        {
            int patListID = requestData.GetData<int>(0);
            // 获取病人预交金总额和总费用金额
            DataTable feeDt = NewDao<IIPManageDao>().GetPatDepositFee(patListID);
            responseData.AddData(feeDt);
            // 获取催款系统参数
            string reminderMoney = NewObject<SysConfigManagement>().GetSystemConfigValue("ReminderMoney");
            responseData.AddData(reminderMoney);
            return responseData;
        }

        /// <summary>
        /// 获取待打印催款单数据
        /// </summary>
        /// <returns>催款单数据</returns>
        [WCFMethod]
        public ServiceResponseData GetReminderDataList()
        {
            string patListID = requestData.GetData<string>(0);
            // 获取继续交款金额
            string reminderMoney = NewObject<SysConfigManagement>().GetSystemConfigValue("ReminderMoney");
            DataTable reminderDataDt = NewDao<IIPManageDao>().GetReminderDataList(patListID, Tools.ToDecimal(reminderMoney, 0));
            responseData.AddData(reminderDataDt);
            return responseData;
        }

        /// <summary>
        /// 病人出区，验证是否存在出院医嘱，验证出院日期
        /// </summary>
        /// <returns>出院日期</returns>
        [WCFMethod]
        public ServiceResponseData IsExistenceDischargeOrder()
        {
            int patListID = requestData.GetData<int>(0);
            DataTable patDt = NewDao<IIPManageDao>().IsExistenceDischargeOrder(patListID);
            responseData.AddData(patDt);
            return responseData;
        }

        /// <summary>
        /// 检查病人是否存在转科医嘱
        /// </summary>
        /// <returns>转科医嘱信息</returns>
        [WCFMethod]
        public ServiceResponseData CheckPatTransDept()
        {
            int patListID = requestData.GetData<int>(0);
            DataTable result = NewDao<IIPManageDao>().CheckPatTransDept(patListID);
            responseData.AddData(result);
            return responseData;
        }
    }
}
