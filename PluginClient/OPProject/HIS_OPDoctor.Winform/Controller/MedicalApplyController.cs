using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using HIS_Entity.ClinicManage;
using HIS_Entity.OPManage;
using HIS_OPDoctor.Winform.ViewForm.MedicalApply;
using EFWCoreLib.WcfFrame.DataSerialize;

namespace HIS_OPDoctor.Winform.Controller
{
    /// <summary>
    /// 医技申请控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmMedicalApply")]//与系统菜单对应
    [WinformView(Name = "FrmMedicalApply", DllName = "HIS_OPDoctor.Winform.dll", ViewTypeName = "HIS_OPDoctor.Winform.ViewForm.MedicalApply.FrmMedicalApply")]
    public class MedicalApplyController : WcfClientController
    {
        /// <summary>
        /// 会员id
        /// </summary>
        public int MemberID;

        /// <summary>
        /// 病人id
        /// </summary>
        public int PatListID;

        /// <summary>
        /// 系统类型
        /// </summary>
        public int SystemType;

        /// <summary>
        /// 科室id
        /// </summary>
        public int DeptId;

        /// <summary>
        /// 医技申请
        /// </summary>
        IFrmMedicalApply frmMedicalApplys;

        /// <summary>
        /// 初始化菜单
        /// </summary>
        public override void Init()
        {
            frmMedicalApplys = (IFrmMedicalApply)DefaultView;
        }

        /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="memberId">会员ID</param>
        /// <param name="patlistId">病人ID</param>
        /// <param name="systemType">窗体类型(0门诊 1住院)</param>
        /// <param name="applyheadId">表头ID</param>
        /// <param name="applytype">申请单类型</param>
        /// <param name="deptId">申请科室id</param>
        /// <param name="applyStatus">申请状态</param>
        /// <param name="isReturns">弹出窗体返回值</param>
        [WinformMethod]
        public void ShowMedicalApply(int memberId, int patlistId, int systemType, string applyheadId, string applytype, int deptId, string applyStatus, string isReturns)
        {
            MemberID = memberId;
            PatListID = patlistId;
            SystemType = systemType;
            DeptId = deptId;
            frmMedicalApplys.SystemType = systemType;
            frmMedicalApplys.IsReturns = isReturns;
            frmMedicalApplys.ApplyHeadID = applyheadId;
            frmMedicalApplys.ApplyType = applytype;
            frmMedicalApplys.PatListID = patlistId;
            frmMedicalApplys.ApplyStatu = applyStatus;
            (frmMedicalApplys as Form).ShowDialog();
        }

        /// <summary>
        /// 加载医技申请科室
        /// </summary>
        [WinformMethod]
        public void GetExecDept()
        {
            var retdata = InvokeWcfService(
                "OPProject.Service",
                "MedicalApplyController",
                "GetExecDept",
                (request) =>
                {
                    request.AddData(LoginUserInfo.WorkId);
                    request.AddData(frmMedicalApplys.ExamClass);
                });
            var depts = retdata.GetData<DataTable>(0);
            frmMedicalApplys.BindExecDept(depts);
        }

        /// <summary>
        /// 根据科室获取项目分类
        /// </summary>
        /// <param name="deptId">科室id</param>
        [WinformMethod]
        public void GetExamType(int deptId)
        {
            var retdata = InvokeWcfService(
            "OPProject.Service",
            "MedicalApplyController",
            "GetExamType",
            (request) =>
            {
                request.AddData(deptId);
                request.AddData(frmMedicalApplys.ExamClass);
            });
            var types = retdata.GetData<DataTable>(0);
            frmMedicalApplys.BindExecType(types);
        }

        /// <summary>
        /// 根据科室获取项目分类
        /// </summary>
        /// <param name="typeId">类型id</param>
        [WinformMethod]
        public void GetExamItem(int typeId)
        {
            var retdata = InvokeWcfService(
            "OPProject.Service",
            "MedicalApplyController",
            "GetExamItem",
            (request) =>
            {
                request.AddData(typeId);
            });
            var items = retdata.GetData<DataTable>(0);
            frmMedicalApplys.BindExecItem(items);
        }

        /// <summary>
        /// 根据科室获取项目分类
        /// </summary>
        [WinformMethod]
        public void GetSample()
        {
            var retdata = InvokeWcfService(
            "OPProject.Service",
            "MedicalApplyController",
            "GetSample",
            (request) =>
            {
                request.AddData(LoginUserInfo.WorkId);
            });
            var samples = retdata.GetData<DataTable>(0);
            frmMedicalApplys.BindSample(samples);
        }

        /// <summary>
        /// 保存申请单信息
        /// </summary>
        /// <param name="head">医技申请头实体</param>
        /// <param name="dt">项目数据</param>
        [WinformMethod]
        public void SaveExam(EXA_MedicalApplyHead head, DataTable dt)
        {
            try
            {
                if (frmMedicalApplys.SaveItemData != null && frmMedicalApplys.SaveItemData.Rows.Count > 0)
                {
                    //不能取当前登录科室ID
                    head.ApplyDeptID = DeptId;// LoginUserInfo.DeptId;
                    head.SystemType = SystemType;
                    head.PatListID = PatListID;
                    head.MemberID = MemberID;
                    head.ApplyDoctorID = LoginUserInfo.EmpId;
                    var retdata = InvokeWcfService(
                    "OPProject.Service",
                    "MedicalApplyController",
                    "SaveExam",
                    (request) =>
                    {
                        request.AddData(head);
                        request.AddData(LoginUserInfo.WorkId);
                        request.AddData(frmMedicalApplys.SaveItemData);
                        if (dt != null)
                        {
                            request.AddData(dt);
                        }
                        else
                        {
                            request.AddData(new DataTable());
                        }
                    });
                    var result = retdata.GetData<int>(0);
                    if (result > 0)
                    {
                        MessageBoxShowSimple("提交成功");
                        switch (SystemType)
                        {
                            case 0:
                                InvokeController("OPProject.UI", "PresManageController", "GetApplyHead", SystemType);
                                break;
                            case 1:
                                InvokeController("IPProject.UI", "OrderManagerController", "GetApplyHead", SystemType);
                                break;
                        }

                        if (string.IsNullOrEmpty(frmMedicalApplys.ApplyHeadID))
                        {
                            frmMedicalApplys.ApplyHeadID = result.ToString();
                        }

                        GetHeadDetail();
                    }
                    else if (result == -1)
                    {
                        MessageBoxEx.Show(retdata.GetData<string>(1));
                    }
                }
                else
                {
                    MessageBoxEx.Show("请至少添加一个项目");
                }
            }
            catch (Exception e)
            {
                MessageBoxEx.Show(e.Message);
            }
        }

        /// <summary>
        /// 获取表头和明细
        /// </summary>
        [WinformMethod]
        public void GetHeadDetail()
        {
            var retdata = InvokeWcfService(
            "OPProject.Service",
            "MedicalApplyController",
            "GetHeadDetail",
            (request) =>
            {
                request.AddData(frmMedicalApplys.ApplyHeadID);
            });
            var headdetail = retdata.GetData<DataTable>(0);
            frmMedicalApplys.BindHeadDetail(headdetail);
        }

        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="age">年龄</param>
        /// <returns>返回转换好的年龄</returns>
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
        /// 获取病人病历信息
        /// </summary>
        /// <param name="patListId">病人id</param>
        /// <returns>检验检查记录</returns>
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

        /// <summary>
        /// 打印信息
        /// </summary>
        /// <param name="updatedata">打印数据实体模型</param>
        [WinformMethod]
        public void PrintData(DataTable updatedata)
        {
            decimal totalFee = 0;
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            var retdata = InvokeWcfService(
            "OPProject.Service",
            "MedicalApplyController",
            "GetPatientList",
            (request) =>
            {
                request.AddData(PatListID.ToString());
                request.AddData(SystemType);
            });
            var plistdt = retdata.GetData<DataTable>(0);
            myDictionary.Add("PatName", plistdt.Rows[0]["PatName"]);
            myDictionary.Add("Age", GetAge(plistdt.Rows[0]["Age"].ToString()));
            myDictionary.Add("PatSex", plistdt.Rows[0]["PatSex"]);
            myDictionary.Add("DiseaseName", plistdt.Rows[0]["DiseaseName"]);
            if (SystemType == 1)
            {
                myDictionary.Add("SerialNumber", plistdt.Rows[0]["CaseNumber"]);
                myDictionary.Add("BedNo", plistdt.Rows[0]["BedNo"]);
            }
            else
            {
                myDictionary.Add("VisitNO", plistdt.Rows[0]["VisitNO"]);
            }

            string itemName = string.Empty;
            if (updatedata != null)
            {
                for (int i = 0; i < updatedata.Rows.Count; i++)
                {
                    if (frmMedicalApplys.ApplyType == "2")
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
            switch (frmMedicalApplys.ApplyType)
            {
                case "0":
                    myDictionary.Add("Assay", frmMedicalApplys.Check.Assay);
                    myDictionary.Add("Assist", frmMedicalApplys.Check.Assist);
                    myDictionary.Add("Digest", frmMedicalApplys.Check.Digest);
                    myDictionary.Add("Signs", frmMedicalApplys.Check.Signs);
                    myDictionary.Add("Xray", frmMedicalApplys.Check.Xray);
                    myDictionary.Add("Part", frmMedicalApplys.Check.Part);
                    EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, SystemType == 0 ? (int)OP_Enum.PrintReport.门诊检查申请单 : (int)OP_Enum.PrintReport.住院检查申请单, 0, myDictionary, null).Print(false);
                    break;
                case "1":
                    myDictionary.Add("Sample", frmMedicalApplys.Test.SampleName);
                    myDictionary.Add("Purpose", frmMedicalApplys.Test.Goal);
                    EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, SystemType == 0 ? (int)OP_Enum.PrintReport.门诊化验申请单 : (int)OP_Enum.PrintReport.住院化验申请单, 0, myDictionary, null).Print(false);
                    break;
                case "2":
                    EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, SystemType == 0 ? (int)OP_Enum.PrintReport.门诊治疗申请单 : (int)OP_Enum.PrintReport.住院治疗申请单, 0, myDictionary, null).Print(false);
                    break;
            }
        }

        [WinformMethod]
        public DataTable GetExamItemDetail(int examItemID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(examItemID);//组合项目ID               
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MedicalApplyController", "GetExamItemDetailDt", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }
    }
}
