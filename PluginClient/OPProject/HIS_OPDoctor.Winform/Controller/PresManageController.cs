using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DevComponents.DotNetBar;
using EfwControls.Common;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.ClinicManage;
using HIS_Entity.OPManage;
using HIS_Entity.OPManage.BusiEntity;
using HIS_OPDoctor.Winform.IView;
using Newtonsoft.Json;

namespace HIS_OPDoctor.Winform.Controller
{
    /// <summary>
    /// 处方管理控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmPresMain")]//与系统菜单对应
    [WinformView(Name = "FrmPresMain", DllName = "HIS_OPDoctor.Winform.dll", ViewTypeName = "HIS_OPDoctor.Winform.ViewForm.FrmPresMain")]//诊疗管理（处方）
    [WinformView(Name = "FrmDiagnosis", DllName = "HIS_OPDoctor.Winform.dll", ViewTypeName = "HIS_OPDoctor.Winform.ViewForm.FrmDiagnosis")]//诊断
    [WinformView(Name = "FrmInpatientCert", DllName = "HIS_OPDoctor.Winform.dll", ViewTypeName = "HIS_OPDoctor.Winform.ViewForm.FrmInpatientCert")]//开住院证
    [WinformView(Name = "FrmBedInfo", DllName = "HIS_OPDoctor.Winform.dll", ViewTypeName = "HIS_OPDoctor.Winform.ViewForm.FrmBedInfo")]//床位信息
    public class PresManageController : WcfClientController
    {
        /// <summary>
        /// 当前病人
        /// </summary>
        DataTable DtCurrentPatient { get; set; }

        /// <summary>
        /// 诊疗管理界面接口
        /// </summary>
        IFrmPresMain iFrmPrescription;

        /// <summary>
        /// 诊断界面接口
        /// </summary>
        IFrmDiagnosis iFrmDiagnosis;

        /// <summary>
        /// 开住院证接口
        /// </summary>
        IFrmInpatientCert iFrmInpatientCert;

        /// <summary>
        /// 床位信息接口
        /// </summary>
        IFrmBedInfo iFrmBedInfo;

        /// <summary>
        /// 挂号有效天数
        /// </summary>            
        private int regValidDays;

        /// <summary>
        /// 库房数据
        /// </summary>
        private DataSet dsDrugRoom = null;

        /// <summary>
        /// 化验单属性JSON对象
        /// </summary>
        private TestJson Test { get; set; }

        /// <summary>
        /// 申请单属性JSON对象
        /// </summary>
        private CheckJson Check { get; set; }

        /// <summary>
        /// 化验单属性类
        /// </summary>
        private class TestJson
        {
            /// <summary>
            /// 目标
            /// </summary>
            public string Goal { get; set; }

            /// <summary>
            /// 标本
            /// </summary>
            public int Sample { get; set; }

            /// <summary>
            /// 标本名称
            /// </summary>
            public string SampleName { get; set; }
        }

        /// <summary>
        /// 申请单属性类
        /// </summary>
        private class CheckJson
        {
            /// <summary>
            /// 病史
            /// </summary>
            public string Digest { get; set; }

            /// <summary>
            /// 体征
            /// </summary>
            public string Signs { get; set; }

            /// <summary>
            /// X线结果
            /// </summary>
            public string Xray { get; set; }

            /// <summary>
            /// 化验结果
            /// </summary>
            public string Assay { get; set; }

            /// <summary>
            /// 辅助检查
            /// </summary>
            public string Assist { get; set; }

            /// <summary>
            /// 检查部位
            /// </summary>
            public string Part { get; set; }
        }

        /// <summary>
        /// 处方输入最大条数
        /// </summary>
        private int presCount;

        /// <summary>
        /// 药品重复给予提示
        /// </summary>
        private int drugRepeatWarn;

        /// <summary>
        /// 天数大于30天给予提示
        /// </summary>
        private int dayGreater30;

        /// <summary>
        /// 初始化菜单
        /// </summary>
        public override void Init()
        {
            iFrmPrescription = (IFrmPresMain)iBaseView["FrmPresMain"];
            iFrmDiagnosis = (IFrmDiagnosis)iBaseView["FrmDiagnosis"];
            iFrmInpatientCert = (IFrmInpatientCert)iBaseView["FrmInpatientCert"];
            iFrmBedInfo = (IFrmBedInfo)iBaseView["FrmBedInfo"];
        }

        /// <summary>
        /// 控件加载数据异步加载
        /// </summary>
        public override void AsynInit()
        {
            iFrmPrescription.BindControlData();
        }

        /// <summary>
        /// 异步加载完成
        /// </summary>
        public override void AsynInitCompleted()
        {
            iFrmPrescription.BindControlDataComplete();
        }

        #region 公用方法
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="message">消息内容</param>
        [WinformMethod]
        public void ShowMessage(string message)
        {
            MessageBoxShowSimple(message);
        }

        /// <summary>
        /// 获取病人ID
        /// </summary>
        /// <returns>病人id</returns>
        private int GetPatientListId()
        {
            if (DtCurrentPatient == null)
            {
                MessageBoxEx.Show("请先选择一个病人");
                return 0;
            }
            else
            {
                return Convert.ToInt32(DtCurrentPatient.Rows[0]["PatListID"]);
            }
        }

        /// <summary>
        /// 获取会员ID
        /// </summary>
        /// <returns>会员id</returns>
        private int GetMemberId()
        {
            if (DtCurrentPatient == null)
            {
                MessageBoxEx.Show("该病人还不是会员");
                return 0;
            }
            else
            {
                return Convert.ToInt32(DtCurrentPatient.Rows[0]["MemberID"]);
            }
        }

        /// <summary>
        /// 获取当前病人ID
        /// </summary>
        /// <returns>当前病人ID</returns>
        private int GetCurrentPatientListId()
        {
            if (DtCurrentPatient == null || DtCurrentPatient.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(DtCurrentPatient.Rows[0]["PatListID"]);
            }
        }

        /// <summary>
        /// 获取会员ID
        /// </summary>
        /// <returns>当前会员id</returns>
        private int GetCurrentMemberId()
        {
            if (DtCurrentPatient == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(DtCurrentPatient.Rows[0]["MemberID"]);
            }
        }
        #endregion

        #region 门诊患者查询
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
            iFrmPrescription.BindDocInDept(dt, deptId);
        }

        /// <summary>
        /// 加载病人列表
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="regBeginDate">挂号开始日期</param>
        /// <param name="regEndDate">挂号结束日期</param>
        /// <param name="visitStatus">就诊状态</param>
        /// <param name="belong">病人所属</param>
        [WinformMethod]
        public void LoadPatientList(int deptId, string regBeginDate, string regEndDate, int visitStatus, int belong)
        {
            int empId = LoginUserInfo.EmpId;
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(empId);
                request.AddData(deptId);
                request.AddData(regBeginDate);
                request.AddData(regEndDate);
                request.AddData(visitStatus);
                request.AddData(belong);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "LoadPatientList", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            iFrmPrescription.BindPatientList(dt);
        }

        /// <summary>
        /// 通过卡号就诊号查询病人信息
        /// </summary>
        /// <param name="id">查询信息</param>
        /// <param name="type">0卡号1就诊号2病人Id</param>
        [WinformMethod]
        public void GetPatientInfo(string id, int type)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(id);
                request.AddData(type);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "GetPatientInfo", requestAction);
            DataTable dtPatient = retdata.GetData<DataTable>(0);
            DtCurrentPatient = dtPatient;
            DataTable dtDisea = retdata.GetData<DataTable>(1);

            iFrmPrescription.BindPatientInfo(dtPatient, dtDisea);

            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                DateTime regDate = Convert.ToDateTime(dtPatient.Rows[0]["RegDate"]);
                DateTime tempDate = regDate.AddDays(regValidDays - 1);
                bool isvalid = true;
                if (tempDate < DateTime.Now)
                {
                    isvalid = false;
                    iFrmPrescription.EnablePresButton(isvalid);
                }
                else
                {
                    isvalid = true;
                    iFrmPrescription.EnablePresButton(isvalid);
                }
            }

            iFrmPrescription.ShowDiseaseInfo(GetDiseaseString(dtDisea));
            GetPatientOMRData();
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

        #region 修改病人信息

        /// <summary>
        /// 调用会员修改界面
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="memberId">会员id</param>
        [WinformMethod]
        public void ShowMemberInfo(string cardNo, int memberId,int patlistid)
        {
            try
            {
                int memberid = (int)InvokeController(this._pluginName, "NewMemberController", "ShowMemberInfo", 6, 0, string.Empty, memberId, 0, 0, 0);
                if (memberid <= 0)
                {
                    return;
                }

                //GetPatientInfo(cardNo, 0);
                GetPatientInfo(patlistid.ToString(), 2);
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
            }
        }
        #endregion

        #region 下诊断
        /// <summary>
        /// 获取诊断
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void GetDisease(string frmName)
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "GetDisease");
            DataTable dt = retdata.GetData<DataTable>(0);
            if (frmName == "FrmDiagnosis")
            {
                iFrmDiagnosis.BindDiseaseShowCard(dt);
            }
            else
            {
                iFrmInpatientCert.BindDiseaseShowCard(dt);
            }
        }

        /// <summary>
        /// 获取诊断窗体接口
        /// </summary>
        /// <returns>诊断窗体接口</returns>
        [WinformMethod]
        public IFrmDiagnosis GetDiagnosisForm()
        {
            return iFrmDiagnosis;
        }

        /// <summary>
        /// 加载诊断列表
        /// </summary>
        [WinformMethod]
        public void LoadDiagnosisList()
        {
            int patListID = GetPatientListId();
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patListID);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "LoadDiagnosisList", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            iFrmDiagnosis.BindDiseaseDataGrid(dt);
            iFrmPrescription.ShowDiseaseInfo(GetDiseaseString(dt));
        }

        /// <summary>
        /// 取得诊断列表
        /// </summary>
        /// <returns>诊断列表</returns>
        [WinformMethod]
        public DataTable GetDiagnosisList()
        {
            int patListID = GetPatientListId();
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patListID);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "LoadDiagnosisList", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }

        /// <summary>
        /// 添加诊断
        /// </summary>
        /// <param name="code">诊断代码</param>
        /// <param name="name">诊断名称</param>
        [WinformMethod]
        public void AddDiagnosis(string code, string name)
        {
            int patListID = GetPatientListId();
            OPD_DiagnosisRecord model = new OPD_DiagnosisRecord();
            model.MemberID = Convert.ToInt32(DtCurrentPatient.Rows[0]["MemberID"]);
            model.PatListID = Convert.ToInt32(DtCurrentPatient.Rows[0]["PatListID"]);
            model.DiagnosisCode = code;
            model.DiagnosisName = name;
            model.PresDoctorID = LoginUserInfo.EmpId;
            model.PresDeptID = LoginUserInfo.DeptId;
            model.DiagnosisDate = DateTime.Now;
            model.SortNo = 0;
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(model);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "AddDiagnosis", requestAction);
            LoadDiagnosisList();
        }

        /// <summary>
        /// 添加诊断
        /// </summary>
        /// <param name="code">诊断代码</param>
        /// <param name="name">诊断名称</param>
        [WinformMethod]
        public void AddDiagnosisFromCommon(string code, string name)
        {
            int patListID = GetPatientListId();
            OPD_DiagnosisRecord model = new OPD_DiagnosisRecord();
            model.MemberID = Convert.ToInt32(DtCurrentPatient.Rows[0]["MemberID"]);
            model.PatListID = Convert.ToInt32(DtCurrentPatient.Rows[0]["PatListID"]);
            model.DiagnosisCode = code;
            model.DiagnosisName = name;
            model.PresDoctorID = LoginUserInfo.EmpId;
            model.PresDeptID = LoginUserInfo.DeptId;
            model.DiagnosisDate = DateTime.Now;
            model.SortNo = 0;
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(model);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "AddDiagnosis", requestAction);
            requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patListID);
            });
            ServiceResponseData retdatas = InvokeWcfService("OPProject.Service", "PresManageController", "LoadDiagnosisList", requestAction);
            DataTable dt = retdatas.GetData<DataTable>(0);
            iFrmPrescription.ShowDiseaseInfo(GetDiseaseString(dt));
        }

        /// <summary>
        /// 删除诊断
        /// </summary>
        /// <param name="diagnosisId">诊断Id</param>
        [WinformMethod]
        public void DeleteDiagnosis(int diagnosisId)
        {
            int patListID = GetPatientListId();
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(diagnosisId);
                request.AddData(patListID);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "DeleteDiagnosis", requestAction);
            LoadDiagnosisList();
        }

        /// <summary>
        /// 诊断排序
        /// </summary>
        /// <param name="dtDiagnosis">诊断记录</param>
        [WinformMethod]
        public void SortDiagnosis(DataTable dtDiagnosis)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(dtDiagnosis);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "SortDiagnosis", requestAction);
            int patListID = GetPatientListId();
            Action<ClientRequestData> requestActionTemp = ((ClientRequestData request) =>
            {
                request.AddData(patListID);
            });
            retdata = InvokeWcfService("OPProject.Service", "PresManageController", "LoadDiagnosisList", requestActionTemp);
            DataTable dt = retdata.GetData<DataTable>(0);
            iFrmPrescription.ShowDiseaseInfo(GetDiseaseString(dt));
        }
        #endregion

        #region 查询历史就诊记录
        /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <param name="presDeptId">处方科室</param>
        [WinformMethod]
        public void ShowHisRecord(object deptId, int presDeptId)
        {
            int memberid = GetCurrentMemberId();
            int patientid = GetCurrentPatientListId();
            bool flag = (bool)InvokeController("OPProject.UI", "QueryHisRecordController", "ShowHisRecord", memberid, patientid, deptId, DtCurrentPatient, presDeptId);
            if (flag)
            {
                iFrmPrescription.RefreshOneCopyControl(flag);
                GetPatientOMRData();
            }
        }
        #endregion

        #region 检查检验申请单
        /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="applyheadId">申请头id</param>
        /// <param name="applyType">申请类型</param>
        /// <param name="applyStatus">申请状态</param>
        /// <param name="isReturns">窗体放回状态</param>
        [WinformMethod]
        public void ShowApply(string applyheadId, string applyType, string applyStatus, string isReturns)
        {
            int memberid = GetMemberId();
            int patientid = GetPatientListId();
            if (memberid > 0 && patientid > 0)
            {
                InvokeController("OPProject.UI", "MedicalApplyController", "ShowMedicalApply", memberid, patientid, 0, applyheadId, applyType, iFrmPrescription.DeptId, applyStatus, isReturns);
            }           
        }

        /// <summary>
        /// 获取申请明细表头信息
        /// </summary>
        /// <param name="systemType">系统类型</param>
        [WinformMethod]
        public void GetApplyHead(int systemType)
        {
            int patid = GetPatientListId();
            if (patid > 0)
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(LoginUserInfo.WorkId);
                    request.AddData(systemType);
                    request.AddData(patid);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MedicalApplyController", "GetApplyHead", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                iFrmPrescription.BindApplyHead(dt);
            }
        }

        /// <summary>
        /// 获取表头和明细
        /// </summary>
        /// <param name="applyheadId">申请头id</param>
        /// <returns>申请头明细信息</returns>
        [WinformMethod]
        public DataTable GetHeadDetail(string applyheadId)
        {
            var retdata = InvokeWcfService(
            "OPProject.Service",
            "MedicalApplyController",
            "GetHeadDetail",
            (request) =>
            {
                request.AddData(applyheadId);
            });
            var headdetail = retdata.GetData<DataTable>(0);
            return headdetail;
        }

        /// <summary>
        /// 打印申请单数据
        /// </summary>
        /// <param name="applyheadId">申请头id</param>
        /// <param name="applytype">申请类型</param>
        [WinformMethod]
        public void PrintData(string applyheadId, string applytype)
        {
            DataTable updatedata = GetHeadDetail(applyheadId);
            if (updatedata != null)
            {
                if (updatedata.Rows.Count > 0)
                {
                    switch (updatedata.Rows[0]["ApplyType"].ToString())
                    {
                        case "0":
                            Check = JsonConvert.DeserializeObject<CheckJson>(updatedata.Rows[0]["ApplyContent"].ToString());
                            break;
                        case "1":
                            Test = JsonConvert.DeserializeObject<TestJson>(updatedata.Rows[0]["ApplyContent"].ToString());
                            break;
                    }
                }
            }

            decimal totalFee = 0;
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            var plistdt = DtCurrentPatient;
            myDictionary.Add("PatName", plistdt.Rows[0]["PatName"]);
            myDictionary.Add("Age", GetAge(plistdt.Rows[0]["Age"].ToString()));
            myDictionary.Add("PatSex", plistdt.Rows[0]["PatSex"]);
            myDictionary.Add("DiseaseName", plistdt.Rows[0]["DiseaseName"]);
            myDictionary.Add("VisitNO", plistdt.Rows[0]["VisitNO"]);
            string itemName = string.Empty;
            if (updatedata != null)
            {
                for (int i = 0; i < updatedata.Rows.Count; i++)
                {
                    if (applytype == "2")
                    {
                        totalFee += Convert.ToDecimal(updatedata.Rows[i]["Price"]) * Convert.ToInt32(updatedata.Rows[i]["Amount"]); 
                    }
                    else
                    {
                        totalFee += Convert.ToDecimal(updatedata.Rows[i]["Price"]);
                    }
                }

                myDictionary.Add("ExcuteDeptName", updatedata.Rows[0]["ExcuteDeptName"]);
                myDictionary.Add("ApplyDeptName", updatedata.Rows[0]["ApplyDeptName"]);
                myDictionary.Add("ApplyDeptDoctor", updatedata.Rows[0]["ApplyDeptDoctor"]);
                myDictionary.Add("CheckDate", updatedata.Rows[0]["CheckDate"]);
                myDictionary.Add("ApplyDate", updatedata.Rows[0]["ApplyDate"]);
                for (int i = 0; i < updatedata.Rows.Count; i++)
                {
                    itemName += updatedata.Rows[i]["ItemName"] + ",";
                }
            }
            else
            {
                myDictionary.Add("ApplyDeptName", LoginUserInfo.DeptName);
                myDictionary.Add("ApplyDeptDoctor", LoginUserInfo.EmpName);
                myDictionary.Add("CheckDate", DateTime.Now);
                myDictionary.Add("ApplyDate", DateTime.Now);
            }

            if (itemName.Length > 0)
            {
                itemName = itemName.Substring(0, itemName.Length - 1);
            }

            myDictionary.Add("TotalFee", totalFee);
            myDictionary.Add("ItemName", itemName);
            myDictionary.Add("EmpName", LoginUserInfo.EmpName);
            myDictionary.Add("WorkName", LoginUserInfo.WorkName);
            myDictionary.Add("PrintDate", DateTime.Now.ToString());
            switch (applytype)
            {
                case "0":
                    myDictionary.Add("Assay", Check.Assay);
                    myDictionary.Add("Assist", Check.Assist);
                    myDictionary.Add("Digest", Check.Digest);
                    myDictionary.Add("Signs", Check.Signs);
                    myDictionary.Add("Xray", Check.Xray);
                    myDictionary.Add("Part", Check.Part);
                    EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, (int)OP_Enum.PrintReport.门诊检查申请单, 0, myDictionary, null).Print(false);
                    break;
                case "1":
                    myDictionary.Add("Sample", Test.SampleName);
                    myDictionary.Add("Purpose", Test.Goal);
                    EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, (int)OP_Enum.PrintReport.门诊化验申请单, 0, myDictionary, null).Print(false);
                    break;
                case "2":
                    EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, (int)OP_Enum.PrintReport.门诊治疗申请单, 0, myDictionary, null).Print(false);
                    break;
            }
        }

        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="age">年龄字符串</param>
        /// <returns>年龄</returns>
        public string GetAge(string age)
        {
            string tempAge = string.Empty;
            if (!string.IsNullOrEmpty(age))
            {
                switch (age.Substring(0, 1))
                {
                    // 岁
                    case "Y":
                        if (age.Length > 1)
                        {
                            tempAge = age.Substring(1) + "岁";
                        }

                        break;
                    // 月
                    case "M":
                        if (age.Length > 1)
                        {
                            tempAge = age.Substring(1) + "月";
                        }

                        break;
                    // 天
                    case "D":
                        if (age.Length > 1)
                        {
                            tempAge = age.Substring(1) + "天";
                        }

                        break;
                    // 时
                    case "H":
                        if (age.Length > 1)
                        {
                            tempAge = age.Substring(1) + "时";
                        }

                        break;
                }
            }

            return tempAge;
        }

        /// <summary>
        /// 获取申请明细表头信息
        /// </summary>
        /// <param name="headId">头id</param>
        /// <returns>申请明细表头信息</returns>
        [WinformMethod]
        public DataTable GetApplyStatus(int headId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(headId);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MedicalApplyController", "GetApplyStatus", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }

        /// <summary>
        /// 删除申请明细表头信息
        /// </summary>
        /// <param name="applyheadId">申请头id</param>
        [WinformMethod]
        public void DelApplyHead(string applyheadId)
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(applyheadId);
                    request.AddData(0);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MedicalApplyController", "DelApplyHead", requestAction);
                int result = retdata.GetData<int>(0);
                if (result > 0)
                {
                    MessageBoxShowSimple("删除成功");
                }
                else
                {
                    MessageBoxShowSimple("删除失败");
                }

                GetApplyHead(0);
            }
            catch (Exception e)
            {
                MessageBoxEx.Show(e.Message);
            }
        }
        #endregion

        #region 处方控件相关
        /// <summary>
        /// 取得药品执行药房
        /// </summary>
        /// <param name="presType">处方类型</param>
        [WinformMethod]
        public void GetDrugStoreRoom(int presType)
        {
            if (dsDrugRoom == null || dsDrugRoom.Tables.Count == 0)
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(0);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "GetDrugStoreRoom", requestAction);
                DataTable dtWest = retdata.GetData<DataTable>(0);
                requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(1);
                });
                retdata = InvokeWcfService("OPProject.Service", "PresManageController", "GetDrugStoreRoom", requestAction);
                DataTable dtChinese = retdata.GetData<DataTable>(0);
                dsDrugRoom = new DataSet();
                dsDrugRoom.Tables.Add(dtWest);
                dsDrugRoom.Tables.Add(dtChinese);
            }

            if (presType == 0)
            {
                iFrmPrescription.BindDrugStoreRoom(dsDrugRoom.Tables[0]);
            }
            else if (presType == 1)
            {
                iFrmPrescription.BindDrugStoreRoom(dsDrugRoom.Tables[1]);
            }
        }

        /// <summary>
        /// 取得科室名称
        /// </summary>
        /// <returns>科室名称</returns>
        [WinformMethod]
        public string GetPresDeptName()
        {
            return LoginUserInfo.DeptName;
        }

        /// <summary>
        /// 获取医生名称
        /// </summary>
        /// <returns>医生名称</returns>
        [WinformMethod]
        public string GetPresDocName()
        {
            return LoginUserInfo.EmpName;
        }

        /// <summary>
        /// 获取科室Id
        /// </summary>
        /// <returns>处方科室id</returns>
        [WinformMethod]
        public int GetPresDeptID()
        {
            return LoginUserInfo.DeptId;
        }

        /// <summary>
        /// 获取医生id
        /// </summary>
        /// <returns>处方医生id</returns>
        [WinformMethod]
        public int GetPresDocID()
        {
            return LoginUserInfo.EmpId;
        }

        /// <summary>
        /// 获取系统参数
        /// </summary>
        [WinformMethod]
        public void GetSystemParameter()
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "GetSystemParamenter");
            int regValidDays = Convert.ToInt32(retdata.GetData<string>(0));
            int presCount = Convert.ToInt32(retdata.GetData<string>(1));
            int drugRepeatWarn = Convert.ToInt32(retdata.GetData<string>(2));
            int dayGreater30 = Convert.ToInt32(retdata.GetData<string>(3));
            int canPrintChargedPres = Convert.ToInt32(retdata.GetData<string>(4));
            this.regValidDays = regValidDays;
            this.presCount = presCount;
            this.drugRepeatWarn = drugRepeatWarn;
            this.dayGreater30 = dayGreater30;
            iFrmPrescription.CanPrintChargedPres = canPrintChargedPres;
        }

        /// <summary>
        /// 结束诊断
        /// </summary>
        /// <param name="patListId">病人id</param>
        [WinformMethod]
        public void CompleteDiagonsis(int patListId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patListId);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "CompleteDiagonsis", requestAction);
        }

        /// <summary>
        /// 获取最大处方数
        /// </summary>
        /// <returns>最大处方数</returns>
        [WinformMethod]
        public int GetPresCount()
        {
            return presCount;
        }

        /// <summary>
        /// 获取药品重复提示
        /// </summary>
        /// <returns>药品重复提示参数</returns>
        [WinformMethod]
        public int GetDrugRepeatWarn()
        {
            return drugRepeatWarn;
        }

        /// <summary>
        /// 处方天数大于30天提示
        /// </summary>
        /// <returns>参数值</returns>
        [WinformMethod]
        public int GetDayGreater30()
        {
            return dayGreater30;
        }

        /// <summary>
        /// 获取处方打印信息
        /// </summary>
        /// <param name="preHeadId">处方头id</param>
        /// <param name="preNo">处方号</param>
        /// <returns>处方打印信息</returns>
        [WinformMethod]
        public DataTable GetPrintPresData(int preHeadId, int preNo)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(preHeadId);
                request.AddData(preNo);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "GetPrintPresData", requestAction);
            DataTable presData = retdata.GetData<DataTable>(0);
            return presData;
        }
        #endregion

        #region 开住院证
        /// <summary>
        /// 读取病人信息
        /// </summary>
        [WinformMethod]
        public void GetPatInfo()
        {
            int patId = GetPatientListId();
            int memid = GetMemberId();
            if (patId > 0)
            {
                iFrmInpatientCert.PatId = patId;
                iFrmInpatientCert.MeId = memid;
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(patId);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "GetPatientData", requestAction);
                DataTable dtPatient = retdata.GetData<DataTable>(0);
                DataTable dtDisea = retdata.GetData<DataTable>(1);
                string strDisease = GetDiseaseString(dtDisea);
                iFrmInpatientCert.BindPatientInfo(dtPatient, strDisease);
            }
        }

        /// <summary>
        /// 取得所有科室列表
        /// </summary>
        [WinformMethod]
        public void GetDeptList()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(true);
            });
            // 通过WCF调用服务端控制器取得住院临床科室列表
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetDeptBasicData", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            iFrmInpatientCert.BindDept(dt);
        }

        /// <summary>
        /// 保存住院证登记
        /// </summary>
        /// <param name="inpatientReg">住院证登记实体</param>
        [WinformMethod]
        public void SaveInpatientReg(OPD_InpatientReg inpatientReg)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(inpatientReg);
                request.AddData(LoginUserInfo.WorkId);
            });
            // 通过WCF调用服务端控制器取得住院临床科室列表
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "SaveInpatientReg", requestAction);
            int result = retdata.GetData<int>(0);
            iFrmInpatientCert.SaveComplete(result);
        }

        /// <summary>
        /// 读取床位信息
        /// </summary>
        [WinformMethod]
        public void GetBedInfo()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.WorkId);
            });
            // 通过WCF调用服务端控制器取得住院临床科室列表
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "GetBedInfo", requestAction);
            DataTable dtBedInfo = retdata.GetData<DataTable>(0);
            iFrmBedInfo.BindBedInfo(dtBedInfo);
        }

        /// <summary>
        /// 读取床位信息
        /// </summary>
        [WinformMethod]
        public void GetInpatientReg()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iFrmInpatientCert.PatId);
            });
            // 通过WCF调用服务端控制器取得住院临床科室列表
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "GetInpatientReg", requestAction);
            OPD_InpatientReg inpatientReg = retdata.GetData<OPD_InpatientReg>(0);
            iFrmInpatientCert.GetInpatientReg(inpatientReg);
        }

        /// <summary>
        /// 根据各方式ID获取名称
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="id">编码</param>
        /// <returns>名称</returns>
        public string GetName(int type, string id)
        {
            string name = string.Empty;
            switch (type)
            {
                case 0:
                    switch (id)
                    {
                        case "3":
                            name = "一般";
                            break;
                        case "4":
                            name = "危";
                            break;
                        case "2":
                            name = "急";
                            break;
                        case "1":
                            name = "重";
                            break;
                    }

                    break;
                case 1:
                    switch (id)
                    {
                        case "1":
                            name = "自动";
                            break;
                        case "2":
                            name = "平卧";
                            break;
                        case "3":
                            name = "半卧";
                            break;
                    }

                    break;
                case 2:
                    switch (id)
                    {
                        case "1":
                            name = "自行";
                            break;
                        case "2":
                            name = "扶行";
                            break;
                        case "3":
                            name = "车送";
                            break;
                        case "4":
                            name = "抬送";
                            break;
                    }

                    break;
                case 3:
                    switch (id)
                    {
                        case "1":
                            name = "普通";
                            break;
                        case "2":
                            name = "半流";
                            break;
                        case "3":
                            name = "全流";
                            break;
                    }

                    break;
                case 4:
                    switch (id)
                    {
                        case "1":
                            name = "勿需隔离";
                            break;
                        case "2":
                            name = "呼吸道隔离";
                            break;
                        case "3":
                            name = "床边隔离";
                            break;
                    }

                    break;
            }

            return name;
        }

        /// <summary>
        /// 打印住院证信息
        /// </summary>
        /// <param name="inpatientReg">住院证实体</param>
        [WinformMethod]
        public void PrintInpatientReg(OPD_InpatientReg inpatientReg)
        {
            DataTable dtPatient = iFrmInpatientCert.DtPatient;
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            myDictionary.Add("MemID", dtPatient.Rows[0]["CardNO"].ToString());
            myDictionary.Add("Attention", inpatientReg.Attention);
            myDictionary.Add("BodyPosition", GetName(1, inpatientReg.BodyPosition));
            myDictionary.Add("ConditionStu", GetName(0, inpatientReg.ConditionStu));
            myDictionary.Add("TransWay", GetName(2, inpatientReg.TransWay));
            myDictionary.Add("Quarantine", GetName(4, inpatientReg.Quarantine));
            myDictionary.Add("Diet", GetName(3, inpatientReg.Diet));
            myDictionary.Add("Deposit", inpatientReg.Deposit.ToString("0.00"));
            myDictionary.Add("HospitalDocDia", inpatientReg.HospitalDocDia);
            myDictionary.Add("InDeptName", inpatientReg.InDeptName);
            myDictionary.Add("OutPatientDocDia", inpatientReg.OutPatientDocDia);
            myDictionary.Add("SignDocName", inpatientReg.SignDocName);
            myDictionary.Add("SignTime", inpatientReg.SignTime);
            myDictionary.Add("RegID", inpatientReg.RegID);
            myDictionary.Add("PatName", dtPatient.Rows[0]["PatName"].ToString());
            myDictionary.Add("Age", iFrmInpatientCert.GetAge(dtPatient.Rows[0]["Age"].ToString()));
            myDictionary.Add("PatSex", dtPatient.Rows[0]["PatSex"].ToString());
            myDictionary.Add("PatTypeName", dtPatient.Rows[0]["PatTypeName"].ToString());
            myDictionary.Add("Address", dtPatient.Rows[0]["CityName"].ToString() + dtPatient.Rows[0]["Address"].ToString());
            myDictionary.Add("Mobile", dtPatient.Rows[0]["Mobile"].ToString());
            myDictionary.Add("WorkName", LoginUserInfo.WorkName);
            EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 2015, 0, myDictionary, null).PrintPreview(false);
        }
        #endregion

        #region 常用诊断
        /// <summary>
        /// 加载诊断列表
        /// </summary>
        [WinformMethod]
        public void LoadCommonDianosis()
        {
            int doctorID = LoginUserInfo.EmpId;
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(doctorID);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "LoadCommonDianosis", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            iFrmPrescription.BindCommonDiagnosisGrid(dt);
        }

        /// <summary>
        /// 删除常用诊断
        /// </summary>
        /// <param name="commonDiagnosisID">常用诊断id</param>
        /// <returns>true成功</returns>
        [WinformMethod]
        public bool DeleteCommonDianosis(int commonDiagnosisID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(commonDiagnosisID);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "DeleteCommonDianosis", requestAction);
            bool bRtn = retdata.GetData<bool>(0);
            return bRtn;
        }
        #endregion

        #region 处方模板
        /// <summary>
        /// 获取处方模板头信息
        /// </summary>
        /// <param name="intLevel">权限级别</param>
        /// <param name="presType">处方类型</param>
        /// <returns>处方模板头列表</returns>
        [WinformMethod]
        public List<OPD_PresMouldHead> GetPresTemplate(int intLevel, int presType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.WorkId);
                request.AddData(intLevel);
                request.AddData(presType);
                request.AddData(LoginUserInfo.DeptId);
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresTemplateController", "GetPresTemplate", requestAction);
            return retdata.GetData<List<OPD_PresMouldHead>>(0);
        }

        /// <summary>
        /// 获取处方模板
        /// </summary>
        /// <param name="tplId">处方模板Id</param>
        [WinformMethod]
        public void GetPresTemplateDetail(int tplId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(tplId);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "GetPresTemplate", requestAction);
            DataTable dt = retData.GetData<DataTable>(0);
            iFrmPrescription.BindTemplateDetailGrid(dt);
        }
        #endregion

        #region 门诊病历
        /// <summary>
        /// 查询门诊病历
        /// </summary>
        [WinformMethod]
        public void GetPatientOMRData()
        {
            int patListId = GetCurrentPatientListId();
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patListId);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "GetPatientOMRData", requestAction);
            OPD_MedicalRecord modelOMR = retdata.GetData<OPD_MedicalRecord>(0);

            //绑定门诊病历信息
            iFrmPrescription.BindOMRInfo(modelOMR);
        }

        /// <summary>
        /// 保存门诊病历
        /// </summary>
        /// <param name="modelOMR">病历实体模型</param>
        /// <returns>true成功</returns>
        [WinformMethod]
        public bool SaveOMRData(OPD_MedicalRecord modelOMR)
        {
            int patListId = GetCurrentPatientListId();
            modelOMR.PatListID = patListId;
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(modelOMR);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "SaveOMRData", requestAction);
            bool bRtn = retdata.GetData<bool>(0);
            if (bRtn)
            {
                MessageBoxShowSimple("病历保存成功");
            }
            else
            {
                MessageBoxShowSimple("病历保存失败");
            }

            return bRtn;
        }

        /// <summary>
        /// 取得特殊字符
        /// </summary>
        [WinformMethod]
        public void GetSymbolData()
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "GetSymbolData");
            DataTable dtSymbolType = retdata.GetData<DataTable>(0);
            DataTable dtSymbolContent = retdata.GetData<DataTable>(1);
            iFrmPrescription.BindOMRSymbolComboBox(dtSymbolType, dtSymbolContent);
        }

        /// <summary>
        /// 显示存为模板窗体
        /// </summary>
        /// <param name="detailModel">门诊病历信息模板</param>
        [WinformMethod]
        public void ShowOMRTplDialog(OPD_OMRTmpDetail detailModel)
        {
            int memberid = GetCurrentMemberId();
            int patientid = GetCurrentPatientListId();
            int flag = (int)InvokeController("OPProject.UI", "OMRTemplateController", "ShowOMRTplDialog", detailModel);
            if (flag == 1)
            {
                MessageBoxShowSimple("模板保存成功");
                iFrmPrescription.FreshOMRTplTree();
            }
            else if (flag != 2)
            {
                MessageBoxShowSimple("模板保存失败");
            }
        }

        /// <summary>
        /// 获取处方模板头信息
        /// </summary>
        /// <param name="intLevel">模板级别</param>
        /// <returns>处方模板头信息</returns>
        [WinformMethod]
        public List<OPD_OMRTmpHead> GetOMRTemplate(int intLevel)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.WorkId);
                request.AddData(intLevel);
                request.AddData(LoginUserInfo.DeptId);
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "OMRController", "GetOMRTemplate", requestAction);
            List<OPD_OMRTmpHead> tempList = retdata.GetData<List<OPD_OMRTmpHead>>(0);

            OPD_OMRTmpHead head = new OPD_OMRTmpHead();

            head.ModuldName = "全部模板";
            head.ModulLevel = intLevel;
            head.MouldType = 0;
            head.OMRTmpHeadID = 0;
            head.PID = -1;
            head.CreateDeptID = LoginUserInfo.DeptId;
            head.CreateEmpID = LoginUserInfo.EmpId;
            tempList.Add(head);

            return tempList;
        }

        /// <summary>
        /// 获取病历模板明细
        /// </summary>
        /// <param name="moduleHeadId">模板头Id</param>
        [WinformMethod]
        public void GetOMRTemplateDetail(int moduleHeadId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(moduleHeadId);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "OMRController", "GetOMRTemplateDetail", requestAction);
            OPD_OMRTmpDetail tmpDetail = retdata.GetData<OPD_OMRTmpDetail>(0);
            iFrmPrescription.BindTplContentControl(tmpDetail);
        }

        /// <summary>
        /// 打印病历
        /// </summary>
        /// <param name="omr">病历实体</param>
        /// <param name="age">年龄</param>
        [WinformMethod]
        public void OMRPrint(OPD_MedicalRecord omr, string age)
        {
            //取得打印处方数据，西成药，中草药
            int patListId = GetPatientListId();
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patListId);
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "PresManageController", "GetOMRPrintPresData", requestAction);
            DataTable dtPres = retData.GetData<DataTable>(0);
            //拼接处方字符串
            string tempStr = string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dtPres.Rows)
            {
                tempStr = dr["ItemName"].ToString() + "  " + dr["Spec"].ToString() + "  " + dr["PresDesc"].ToString() + "\r\n";
                sb.Append(tempStr);
            }

            //设置打印参数
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            //处方
            myDictionary.Add("PresDesc", sb.ToString());
            //主诉
            myDictionary.Add("Symptoms", omr.Symptoms);
            //病史
            myDictionary.Add("SicknessHistory", omr.SicknessHistory);
            //体查
            myDictionary.Add("PhysicalExam", omr.PhysicalExam);
            //诊断
            myDictionary.Add("Disease", omr.DocAdvise);
            //辅助检查
            myDictionary.Add("AuxiliaryExam", omr.AuxiliaryExam);
            //医院名称
            myDictionary.Add("WorkerName", LoginUserInfo.WorkName);
            //年龄
            myDictionary.Add("Age", age);
            ReportTool.GetReport(LoginUserInfo.WorkId, PrintEnumValue.OPDOMRPRINT, 0, myDictionary, DtCurrentPatient).PrintPreview(false);
        }

        /// <summary>
        /// 获取是否是医保病人
        /// </summary>
        /// <param name="patId">病人id</param>
        /// <returns>true医保病人</returns>
        [WinformMethod]
        public bool GetMediaPat(string patId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                if (DtCurrentPatient == null)
                {
                    Action<ClientRequestData> requestAction1 = ((ClientRequestData request1) =>
                    {
                        request1.AddData(patId);
                        request1.AddData(2);
                    });
                    ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "GetPatientInfo", requestAction1);
                    DataTable dtPatient = retdata.GetData<DataTable>(0);
                    DtCurrentPatient = dtPatient;
                }

                request.AddData(Tools.ToInt32(DtCurrentPatient.Rows[0]["PatTypeID"]));
            });
            ServiceResponseData retData = InvokeWcfService("OPProject.Service", "BalanceController", "IsMediaPat", requestAction);
            bool isAll = retData.GetData<bool>(0);
            return isAll;
        }

        #endregion

        #region 打印
        /// <summary>
        /// 处方打印
        /// </summary>
        /// <param name="presPrint"> 处方打印实体类</param>
        /// <param name="presData">处方数据</param>
        [WinformMethod]
        public void PrintPres(PresPrint presPrint, DataTable presData, bool isDoublePrint)
        {
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            myDictionary.Add("PatType", presPrint.PatType);
            myDictionary.Add("PatName", presPrint.PatName);
            myDictionary.Add("VisitNO", presPrint.VisitNO);
            myDictionary.Add("Sex", presPrint.Sex);
            myDictionary.Add("Age", presPrint.Age);
            myDictionary.Add("Diagnosis", presPrint.Diagnosis);
            myDictionary.Add("Address", presPrint.Address);
            myDictionary.Add("TelPhone", presPrint.TelPhone);
            myDictionary.Add("PresNO", presPrint.PresNO);
            myDictionary.Add("PresType", presPrint.PresType);
            decimal totalFee = 0;
            for (int i = 0; i < presData.Rows.Count; i++)
            {
                if (presData.Rows[i]["IsTake"].ToString() == "0")
                {
                    //totalFee += Convert.ToDecimal(presData.Rows[i]["TotalFee"]);
                    if (Convert.ToInt32(presData.Rows[i]["DoseNum"]) > 0)
                    {
                        totalFee += Convert.ToDecimal(presData.Rows[i]["TotalFee"]) * Convert.ToDecimal(presData.Rows[i]["DoseNum"]);
                    }
                    else
                    {
                        totalFee += Convert.ToDecimal(presData.Rows[i]["TotalFee"]);
                    }
                }
            }

            myDictionary.Add("TotalFee", totalFee);
            myDictionary.Add("WorkerID", presPrint.WorkerID);
            myDictionary.Add("DeptName", presPrint.DeptName);
            myDictionary.Add("PresDate", presPrint.PresDate);
            myDictionary.Add("DoctorName", presPrint.DoctorName);
            myDictionary.Add("ChannelName", presPrint.ChannelName);
            myDictionary.Add("FrequencyName", presPrint.FrequencyName);
            myDictionary.Add("DoseNum", presPrint.DoseNum);
            string patTypeStr = string.Empty;
            bool isAll = GetMediaPat(presPrint.PatId);
            if (isAll)
            {
                patTypeStr = "医保处方";
            }
            else
            {
                patTypeStr = "处方";
            }

            if (presPrint.PrintType == "1")
            {
                myDictionary.Add("WorkerName", presPrint.WorkerName + patTypeStr + "(正方)");
                DataTable chinesedt = GetChineseData(presData);
                ReportTool.GetReport(LoginUserInfo.WorkId, (int)OP_Enum.PrintReport.新中草药, 0, myDictionary, chinesedt).Print(false);//PrintPreview(true);

                if (isDoublePrint == true)
                {
                    myDictionary["WorkerName"] = presPrint.WorkerName + patTypeStr + "(副方)";
                    ReportTool.GetReport(LoginUserInfo.WorkId, (int)OP_Enum.PrintReport.新中草药, 0, myDictionary, chinesedt).Print(false);
                }
            }
            else if (presPrint.PrintType == "0")
            {
                myDictionary.Add("WorkerName", presPrint.WorkerName + patTypeStr + "(正方)");
                DataTable westdt = GetWestData(presData);
                ReportTool.GetReport(LoginUserInfo.WorkId, (int)OP_Enum.PrintReport.新西成药, 0, myDictionary, westdt).Print(false);//PrintPreview(true);

                if (isDoublePrint == true)
                {
                    myDictionary["WorkerName"] = presPrint.WorkerName + patTypeStr + "(副方)";
                    ReportTool.GetReport(LoginUserInfo.WorkId, (int)OP_Enum.PrintReport.新西成药, 0, myDictionary, westdt).Print(false);
                }
            }
            else if (presPrint.PrintType == "2")
            {
                myDictionary.Add("WorkerName", presPrint.WorkerName + "门诊费用");

                DataTable feedt =GetFeeData(presData);
                ReportTool.GetReport(LoginUserInfo.WorkId, (int)OP_Enum.PrintReport.门诊医生费用, 0, myDictionary, feedt).Print(false);//PrintPreview(true);

                if (isDoublePrint == true)
                {
                    myDictionary["WorkerName"] = presPrint.WorkerName + "门诊费用";
                    ReportTool.GetReport(LoginUserInfo.WorkId, (int)OP_Enum.PrintReport.门诊医生费用, 0, myDictionary, feedt).Print(false);
                }
            }
        }

        /// <summary>
        /// 获取西药打印数据
        /// </summary>
        /// <param name="presData">处方数据</param>
        /// <returns>西药打印数据</returns>
        private DataTable GetWestData(DataTable presData)
        {
            int currentgroupid = 0;
            int ordeyno = 0;
            presData.Columns.Add("GroupLineUp");
            presData.Columns.Add("GroupLineDown");
            for (int index = 0; index < presData.Rows.Count; index++)
            {
                int groupid = Convert.ToInt32(presData.Rows[index]["GroupID"]);
                int groupcount = presData.Select("GroupID=" + groupid ).Count();
                if (currentgroupid != groupid)
                {
                    if (groupcount > 1)
                    {
                        currentgroupid = groupid;
                        ordeyno = 1;
                        presData.Rows[index]["GroupLineUp"] = "┍";
                        presData.Rows[index]["GroupLineDown"] = "│";
                    }
                    else
                    {
                        presData.Rows[index]["GroupLineUp"] = string.Empty;
                        presData.Rows[index]["GroupLineDown"] = string.Empty;
                    }
                }
                else
                {
                    if (ordeyno == groupcount)
                    {
                        presData.Rows[index]["GroupLineUp"] = "│";
                        presData.Rows[index]["GroupLineDown"] = "┕";
                    }
                    else
                    {
                        presData.Rows[index]["GroupLineUp"] = "│";
                        presData.Rows[index]["GroupLineDown"] = "│";
                    }
                }

                ordeyno++;
            }

            return presData;
        }

        /// <summary>
        /// 获取西药打印数据
        /// </summary>
        /// <param name="presData">处方数据</param>
        /// <returns>西药打印数据</returns>
        private DataTable GetFeeData(DataTable presData)
        {
            int currentgroupid = 0;
            int ordeyno = 0;
            presData.Columns.Add("GroupLineUp");
            presData.Columns.Add("GroupLineDown");
            for (int index = 0; index < presData.Rows.Count; index++)
            {
                int groupid = Convert.ToInt32(presData.Rows[index]["GroupID"]);
                int groupcount = presData.Select("GroupID=" + groupid).Count();
                if (currentgroupid != groupid)
                {
                    if (groupcount > 1)
                    {
                        currentgroupid = groupid;
                        ordeyno = 1;
                        presData.Rows[index]["GroupLineUp"] = "┍";
                        presData.Rows[index]["GroupLineDown"] = "│";
                    }
                    else
                    {
                        presData.Rows[index]["GroupLineUp"] = string.Empty;
                        presData.Rows[index]["GroupLineDown"] = string.Empty;
                    }
                }
                else
                {
                    if (ordeyno == groupcount)
                    {
                        presData.Rows[index]["GroupLineUp"] = "│";
                        presData.Rows[index]["GroupLineDown"] = "┕";
                    }
                    else
                    {
                        presData.Rows[index]["GroupLineUp"] = "│";
                        presData.Rows[index]["GroupLineDown"] = "│";
                    }
                }

                ordeyno++;
            }

            return presData;
        }

        /// <summary>
        /// 获取中药打印数据
        /// </summary>
        /// <param name="presData">处方数据</param>
        /// <returns>中药打印数据</returns>
        private DataTable GetChineseData(DataTable presData)
        {
            int rowsCount = presData.Rows.Count;
            DataTable dt = new DataTable();
            dt.Columns.Add("Item_Name_Left");
            dt.Columns.Add("Usage_Unit_Left");
            dt.Columns.Add("Usage_Amount_Left");
            dt.Columns.Add("TotalFee_Left");
            dt.Columns.Add("MedicareID_Left");
            dt.Columns.Add("Item_Name_Right");
            dt.Columns.Add("Usage_Unit_Right");
            dt.Columns.Add("Usage_Amount_Right");
            dt.Columns.Add("TotalFee_Right");
            dt.Columns.Add("MedicareID_Right");

            dt.Columns.Add("Item_Name_RightThree");
            dt.Columns.Add("Usage_Unit_RightThree");
            dt.Columns.Add("Usage_Amount_RightThree");

            dt.Columns.Add("Item_Name_RightFour");
            dt.Columns.Add("Usage_Unit_RightFour");
            dt.Columns.Add("Usage_Amount_RightFour");
            for (int i = 0; i < rowsCount; i += 4)
            {
                DataRow dr = dt.NewRow();
                dr["Item_Name_Left"] = presData.Rows[i]["ItemName"];
                dr["Usage_Unit_Left"] = presData.Rows[i]["DosageUnit"];
                dr["Usage_Amount_Left"] = presData.Rows[i]["Dosage"];
                dr["TotalFee_Left"] = presData.Rows[i]["TotalFee"];
                dr["MedicareID_Left"] = presData.Rows[i]["MedicareID"];
                if ((i + 1) < rowsCount)
                {
                    dr["Item_Name_Right"] = presData.Rows[i + 1]["ItemName"];
                    dr["Usage_Unit_Right"] = presData.Rows[i + 1]["DosageUnit"];
                    dr["Usage_Amount_Right"] = presData.Rows[i + 1]["Dosage"];
                    dr["TotalFee_Right"] = presData.Rows[i + 1]["TotalFee"];
                    dr["MedicareID_Right"] = presData.Rows[i + 1]["MedicareID"];
                }
                if ((i + 2) < rowsCount)
                {
                    dr["Item_Name_RightThree"] = presData.Rows[i + 2]["ItemName"];
                    dr["Usage_Unit_RightThree"] = presData.Rows[i + 2]["DosageUnit"];
                    dr["Usage_Amount_RightThree"] = presData.Rows[i + 2]["Dosage"];
                    //dr["TotalFee_RightThree"] = presData.Rows[i + 2]["TotalFee"];
                    //dr["MedicareID_RightThree"] = presData.Rows[i + 2]["MedicareID"];
                }
                if ((i + 3) < rowsCount)
                {
                    dr["Item_Name_RightFour"] = presData.Rows[i + 3]["ItemName"];
                    dr["Usage_Unit_RightFour"] = presData.Rows[i + 3]["DosageUnit"];
                    dr["Usage_Amount_RightFour"] = presData.Rows[i + 3]["Dosage"];
                    //dr["TotalFee_RightFour"] = presData.Rows[i + 3]["TotalFee"];
                    //dr["MedicareID_RightFour"] = presData.Rows[i + 3]["MedicareID"];
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }
        #endregion

        /// <summary>
        /// 医技退费
        /// </summary>
        /// <param name="applyHeadID">申请头id</param>
        [WinformMethod]
        public void RefundMediFee(int applyHeadID)
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(applyHeadID);
                });
                ServiceResponseData retData = InvokeWcfService("OPProject.Service", "RefundController", "GetInvoiceNOExaApplyHeadID", requestAction);
                string invoiceNO = retData.GetData<string>(0);
                InvokeController("OPProject.UI", "RefundController", "ShowRefundMessage", invoiceNO);//调用退费界面
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
            }
        }
    }
}
