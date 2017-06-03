using System;
using System.Collections.Generic;
using System.Data;
using EfwControls.HISControl.Prescription.Controls.Entity;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.ClinicManage;
using HIS_OPDoctor.Winform.IView;
using HIS_OPDoctor.Winform.ViewForm;

namespace HIS_OPDoctor.Winform.Controller
{
    /// <summary>
    /// 查看历史诊疗记录控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmQueryHisRecords")]//与系统菜单对应
    [WinformView(Name = "FrmQueryHisRecords", DllName = "HIS_OPDoctor.Winform.dll", ViewTypeName = "HIS_OPDoctor.Winform.ViewForm.FrmQueryHisRecords")]
    public class QueryHisRecordsController : WcfClientController
    {
        /// <summary>
        /// 会员id
        /// </summary>
        private int memberID;

        /// <summary>
        /// 病人id
        /// </summary>
        private int patListID;

        /// <summary>
        /// 查看历史诊疗记录界面接口
        /// </summary>
        IFrmQueryHisRecords iFrmQueryHisRecord;

        /// <summary>
        /// 当前处方科室Id
        /// </summary>
        private int currentPresDeptId;

        /// <summary>
        /// 初始化菜单
        /// </summary>
        public override void Init()
        {
            iFrmQueryHisRecord = (IFrmQueryHisRecords)iBaseView["FrmQueryHisRecords"];
        }

        /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="memberId">会员id</param>
        /// <param name="patlistId">病人id</param>
        /// <param name="deptId">科室id</param>
        /// <param name="dtCurrentPatient">当前病人信息</param>
        /// <param name="presDeptId">处方科室</param>
        /// <returns>true成功</returns>
        [WinformMethod]
        public bool ShowHisRecord(int memberId, int patlistId, object deptId, DataTable dtCurrentPatient,int presDeptId)
        {
            memberID = memberId;
            patListID = patlistId;
            iFrmQueryHisRecord.DeptId = (int)deptId;
            iFrmQueryHisRecord.PatListId = patlistId;
            iFrmQueryHisRecord.DtPatient = dtCurrentPatient;
            currentPresDeptId = presDeptId;
            FrmQueryHisRecord frm = (iFrmQueryHisRecord as FrmQueryHisRecord);
            frm.ShowDialog();
            bool copyFlag = frm.CopyFlag;
            if (patListID > 0 && copyFlag)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取病人就诊记录
        /// </summary>
        [WinformMethod]
        public void GetHisRecord()
        {
            var retdata = InvokeWcfService(
            "OPProject.Service",
            "QueryHisRecordController",
            "GetHisRecord",
            (request) =>
            {
                request.AddData(0);
                request.AddData(LoginUserInfo.WorkId);
                request.AddData(iFrmQueryHisRecord.GetQueryWhere());
            });
            var record = retdata.GetData<DataTable>(0);
            iFrmQueryHisRecord.BindHisRecord(record);
        }

        /// <summary>
        /// 获取申请明细表头信息
        /// </summary>
        /// <param name="patid">病人id</param>
        [WinformMethod]
        public void GetApplyHead(int patid)
        {
            if (patid > 0)
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(LoginUserInfo.WorkId);
                    request.AddData(0);
                    request.AddData(patid);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MedicalApplyController", "GetApplyHead", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                iFrmQueryHisRecord.BindApplyHead(dt);
            }
        }

        /// <summary>
        /// 取得医生所在科室信息
        /// </summary>
        [WinformMethod]
        public void GetDocRelateDeptInfo()
        {
            int empId = LoginUserInfo.EmpId;
            int deptId = LoginUserInfo.DeptId;
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(empId);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "GetDocRelateDeptInfo", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            iFrmQueryHisRecord.BindDocInDept(dt, deptId);
        }

        /// <summary>
        /// 根据科室ID获取科室下的医生
        /// </summary>
        /// <param name="deptId">科室id</param>
        [WinformMethod]
        public void GetDoctor(int deptId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptId);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "QueryHisRecordController", "GetDoctor", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            iFrmQueryHisRecord.BindDoctor(dt, LoginUserInfo.EmpId);
        }

        /// <summary>
        /// 获取处方数据
        /// </summary>
        /// <param name="patListId">病人Id</param>
        /// <param name="presType">处方类型</param>
        /// <returns>处方数据</returns>
        [WinformMethod]
        public List<Prescription> GetPrescriptionData(int patListId, int presType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patListId);
                request.AddData(presType);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "GetPrescriptionData", requestAction);
            DataTable dt = retData.GetData<DataTable>(0);
            //int _orderNo = 1;//行号
            List<Prescription> list_Prescription = new List<Prescription>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Prescription mPres = new Prescription();
                mPres.PresListId = Convert.ToInt32(dt.Rows[i]["PresDetailID"]);
                mPres.PresHeadId = Convert.ToInt32(dt.Rows[i]["PresHeadID"]);
                //mPres.OrderNo = i + 1;//行号
                mPres.Item_Id = Convert.ToInt32(dt.Rows[i]["ItemID"]);
                mPres.Item_Name = dt.Rows[i]["ItemName"].ToString();
                mPres.Item_Type = presType;//1西药 2中药 3项目材料
                mPres.StatItem_Code = dt.Rows[i]["StatID"].ToString();
                mPres.Sell_Price = Convert.ToDecimal(dt.Rows[i]["Price"]);
                mPres.Buy_Price = Convert.ToDecimal(dt.Rows[i]["Price"]);
                mPres.Item_Price = Convert.ToDecimal(dt.Rows[i]["Price"]);
                mPres.Standard = dt.Rows[i]["Spec"].ToString();
                mPres.Usage_Amount = Convert.ToDecimal(dt.Rows[i]["Dosage"]);//剂量
                mPres.Usage_Unit = dt.Rows[i]["DosageUnit"].ToString();//剂量单位
                mPres.Usage_Rate = Convert.ToDecimal(dt.Rows[i]["Factor"]);//剂量系数
                mPres.Dosage = Convert.ToInt32(dt.Rows[i]["DoseNum"]); //付数
                mPres.Usage_Id = Convert.ToInt32(dt.Rows[i]["ChannelID"]);
                mPres.Frequency_Id = Convert.ToInt32(dt.Rows[i]["FrequencyID"]);
                mPres.Days = Convert.ToInt32(dt.Rows[i]["Days"]);

                mPres.Amount = Convert.ToDecimal(dt.Rows[i]["ChargeAmount"].ToString());//发药数量
                mPres.Unit = dt.Rows[i]["ChargeUnit"].ToString();//发药单位

                mPres.Item_Amount = Convert.ToInt32(Convert.ToDecimal(dt.Rows[i]["PresAmount"]));//开药数量
                mPres.Item_Unit = dt.Rows[i]["PresAmountUnit"].ToString();//开药单位
                mPres.Item_Rate = Convert.ToInt32(Convert.ToDecimal(dt.Rows[i]["PresFactor"]));//系数

                mPres.Group_Id = Convert.ToInt32(dt.Rows[i]["GroupID"]);//分组组号
                mPres.SkinTest_Flag = Convert.ToInt32(dt.Rows[i]["IsAst"]);//皮试
                mPres.SelfDrug_Flag = Convert.ToInt32(dt.Rows[i]["IsTake"]);//自备
                mPres.Entrust = dt.Rows[i]["Entrust"].ToString();//嘱托

                mPres.FootNote = string.Empty;
                mPres.Tc_Flag = 0;//套餐

                mPres.PresNo = Convert.ToInt32(dt.Rows[i]["PresNO"]);//方号
                mPres.Dept_Id = Convert.ToInt32(dt.Rows[i]["ExecDeptID"]);//执行科室
                mPres.Dept_Name = dt.Rows[i]["ExecDeptName"].ToString();
                mPres.Pres_Dept = Convert.ToInt32(dt.Rows[i]["PresDeptID"]);
                mPres.Pres_DeptName = dt.Rows[i]["PresDeptName"].ToString();
                mPres.Pres_Doc = Convert.ToInt32(dt.Rows[i]["PresDoctorID"]);
                mPres.Pres_DocName = dt.Rows[i]["PresDoctorName"].ToString();
                if (Convert.ToInt32(dt.Rows[i]["IsCancel"]) == 1)
                {
                    mPres.Status = PresStatus.退费状态;
                }
                else if (Convert.ToInt32(dt.Rows[i]["IsCharged"]) == 1)
                {
                    mPres.Status = PresStatus.收费状态;
                }
                else
                {
                    mPres.Status = PresStatus.保存状态;
                }

                mPres.Additional = dt.Rows[i]["Additional"].ToString();//附加
                mPres.Usage_Name = dt.Rows[i]["ChannelName"].ToString();//用法名称
                mPres.Frequency_Name = dt.Rows[i]["FrequencyName"].ToString();//频次名称
                mPres.Frequency_Caption = dt.Rows[i]["ExecuteCode"].ToString();//频次名称

                int execNum, cycleDay;
                CardDataSourceFrequency.Calculate(dt.Rows[i]["ExecuteCode"].ToString(), out execNum, out cycleDay);
                mPres.Frequency_ExecNum = execNum;//执行次数
                mPres.Frequency_CycleDay = cycleDay;//执行周期

                //精毒标志
                mPres.IsLunacyPosion = Convert.ToInt32(dt.Rows[i]["IsLunacyPosion"]);
                //本院执行次数
                mPres.ExecNum = Convert.ToInt32(dt.Rows[i]["ExecNum"]);
                mPres.DropSpec = dt.Rows[i]["DropSpec"].ToString();
                mPres.GroupSortNO = Convert.ToInt32(dt.Rows[i]["GroupSortNO"]);
                mPres.Memo = dt.Rows[i]["Memo"].ToString();
                mPres.CalculateItemMoney();

                list_Prescription.Add(mPres);
            }

            return list_Prescription;
        }

        /// <summary>
        /// 取得病人Id
        /// </summary>
        /// <returns>病人Id</returns>
        [WinformMethod]
        public int GetPatListId()
        {
            return patListID;
        }

        /// <summary>
        /// 一键复制历史就诊记录
        /// </summary>
        /// <param name="bDiseaseHis">病历</param>
        /// <param name="bWest">西药处方</param>
        /// <param name="bChinese">中草药处方</param>
        /// <param name="bFee">费用处方</param>
        /// <param name="currentPatId">当前病人Id</param>
        /// <param name="hisPatListId">历史病人Id</param>
        /// <returns>true成功</returns>
        [WinformMethod]
        public bool OneCopy(bool bDiseaseHis, bool bWest,bool bChinese,bool bFee, int currentPatId, int hisPatListId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(bDiseaseHis);
                request.AddData(bWest);
                request.AddData(bChinese);
                request.AddData(bFee);
                request.AddData(currentPatId);
                request.AddData(hisPatListId);
                request.AddData(LoginUserInfo.EmpId);
                request.AddData(currentPresDeptId);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "OneCopy", requestAction);
            bool bRtn = retData.GetData<bool>(0);
            return bRtn;
        }

        /// <summary>
        /// 获取病人病历信息
        /// </summary>
        /// <param name="patListId">病人id</param>
        /// <returns>病人病历信息</returns>
        [WinformMethod]
        public OPD_MedicalRecord GetMedical(int patListId)
        {
            var retdata = InvokeWcfService(
               "OPProject.Service",
               "PresManageController",
               "GetPatientOMRData",
               (request) =>
               {
                   request.AddData(patListId);
               });
            var medical = retdata.GetData<OPD_MedicalRecord>(0);
            return medical;
        }
    }
}
