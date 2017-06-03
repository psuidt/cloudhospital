using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EfwControls.Common;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.Controller
{
    /// <summary>
    /// 床位管理控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmBedManagement")]//在菜单上显示
    [WinformView(Name = "FrmBedManagement", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmBedManagement")]
    [WinformView(Name = "FrmBedAllocation", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmBedAllocation")]
    [WinformView(Name = "FrmUpdatePatientInfo", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmUpdatePatientInfo")]
    [WinformView(Name = "FrmPatientBedChanging", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmPatientBedChanging")]
    [WinformView(Name = "FrmPackBed", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmPackBed")]
    [WinformView(Name = "FrmDischargeConfirmation", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmDischargeConfirmation")]
    [WinformView(Name = "FrmDeptOrderCheck", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmDeptOrderCheck")]
    public class BedManagementController : WcfClientController
    {
        /// <summary>
        /// 床位分配一览
        /// </summary>
        IBedManagement bedManagement;

        /// <summary>
        /// 修改病人入院信息
        /// </summary>
        IUpdatePatientInfo updatePatientInfo;

        /// <summary>
        /// 病人换床
        /// </summary>
        IPatientBedChanging patientBedChanging;

        /// <summary>
        /// 床位分配
        /// </summary>
        IBedAllocation bedAllocation;

        /// <summary>
        /// 包床
        /// </summary>
        IPackBed packBed;

        /// <summary>
        /// 出院未处理数据接口（医嘱、药品、账单）
        /// </summary>
        IDischargeConfirmation mDischargeConfirmation;

        /// <summary>
        /// 全科医嘱发送
        /// </summary>
        IDeptOrderCheck mIDeptOrderCheck;

        /// <summary>
        /// 科室列表
        /// </summary>
        private DataTable deptDt = new DataTable();

        /// <summary>
        /// 床位ID
        /// </summary>
        private int bedId = 0;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            bedManagement = (IBedManagement)DefaultView;
            bedAllocation = iBaseView["FrmBedAllocation"] as IBedAllocation;
            updatePatientInfo = iBaseView["FrmUpdatePatientInfo"] as IUpdatePatientInfo;
            patientBedChanging = iBaseView["FrmPatientBedChanging"] as IPatientBedChanging;
            packBed = iBaseView["FrmPackBed"] as IPackBed;
            mDischargeConfirmation = iBaseView["FrmDischargeConfirmation"] as IDischargeConfirmation;
            mIDeptOrderCheck = iBaseView["FrmDeptOrderCheck"] as IDeptOrderCheck;
        }

        /// <summary>
        /// 数据加载
        /// </summary>
        public override void AsynInit()
        {
            GetDeptList();// 科室列表
        }

        /// <summary>
        /// 将数据绑定到界面控件
        /// </summary>
        public override void AsynInitCompleted()
        {
            bedManagement.Bind_DeptList(deptDt, LoginUserInfo.DeptId); // 绑定科室列表
        }

        /// <summary>
        /// 获取病区列表
        /// </summary>
        [WinformMethod]
        public void GetWardDept()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetWardDept");
            DataTable dt = retdata.GetData<DataTable>(0);
            bedManagement.Bind_WardDept(dt); // 绑定病区列表数据
        }

        /// <summary>
        /// 根据病区ID查询床位列表
        /// </summary>
        /// <param name="wardID">病区ID</param>
        [WinformMethod]
        public void GetBedList(int wardID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(wardID);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "BedManagementController", "GetBedManageList", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            // 绑定床位列表网格数据
            bedManagement.Bind_BedList(dt);
        }

        /// <summary>
        /// 打开床位分配界面
        /// </summary>
        [WinformMethod]
        public void ShowFrmBedAllocation()
        {
            bedAllocation.Bed = bedManagement.Bed;
            // 根据病区ID床位号取得当前床位绑定的护士和医生
            DataTable dt = GetDoctorNurseID(int.Parse(bedManagement.Bed.WardCode), bedManagement.Bed.BedNo);

            if (dt != null && dt.Rows.Count > 0)
            {
                bedId = int.Parse(dt.Rows[0][0].ToString());
                DataTable doctorDt = GetEmpDataSourceType(1);  // 获取医生列表
                bedAllocation.Bind_txtCurrDoctor(doctorDt, int.Parse(dt.Rows[0][1].ToString()));
                DataTable nurseDt = GetEmpDataSourceType(2);  // 获取护士列表
                bedAllocation.Bind_txtCurrNurse(nurseDt, int.Parse(dt.Rows[0][2].ToString()));
            }

            bedAllocation.WardID = int.Parse(bedManagement.Bed.WardCode);
            GetNotHospitalPatList(int.Parse(bedManagement.Bed.WardCode), "1");
            ((Form)iBaseView["FrmBedAllocation"]).ShowDialog();
        }

        /// <summary>
        /// 获取医生或者护士列表
        /// </summary>
        /// <param name="empType">操作员类型（1：医生/2：护士）</param>
        /// <returns>医生或护士列表</returns>
        public DataTable GetEmpDataSourceType(int empType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(empType);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetEmpDataSourceType", requestAction);
            return retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 查询所有未分配床位的病人
        /// </summary>
        /// <param name="wardID">病区ID</param>
        /// <param name="status">病人状态</param>
        [WinformMethod]
        public void GetNotHospitalPatList(int wardID, string status)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(wardID);
                request.AddData(status);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "BedManagementController", "GetNotHospitalPatList", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);

            if (dt != null)
            {
                // 未分配床位病人列表
                bedAllocation.Bind_NotHospitalPatList(dt);
            }
        }

        /// <summary>
        /// 查询所有转科病人
        /// </summary>
        /// <param name="wardID">病区ID</param>
        [WinformMethod]
        public void GetTransferPatList(int wardID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(wardID);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "BedManagementController", "GetTransferPatList", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);

            if (dt != null)
            {
                // 绑定转科病人列表
                bedAllocation.Bind_NotHospitalPatList(dt);
            }
        }

        /// <summary>
        /// 保存床位分配数据
        /// </summary>
        /// <param name="dt">病人入院登记信息</param>
        [WinformMethod]
        public void SaveBedAllocation(DataTable dt)
        {
            if (MessageBoxShowYesNo(string.Format("确定要为病人【{0}】分配床位吗？", dt.Rows[0]["PatName"].ToString())) == DialogResult.Yes)
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(dt);
                    request.AddData(bedId);
                    request.AddData(int.Parse(bedAllocation.Bed.WardCode));
                    request.AddData(bedAllocation.Bed.BedNo);
                    request.AddData(LoginUserInfo.EmpId);
                });

                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "BedManagementController", "SaveBedAllocation", requestAction);
                // 判断床位保存成功或失败
                string result = retdata.GetData<string>(0);

                if (string.IsNullOrEmpty(result))
                {
                    bedAllocation.CloseForm();
                    MessageBox.Show("床位分配成功！");
                    GetBedList(int.Parse(bedAllocation.Bed.WardCode));
                }
                else
                {
                    MessageBoxShowSimple(result);
                }
            }
        }

        /// <summary>
        /// 根基病区ID床位号查询
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        /// <returns>当前床位绑定的护士医生</returns>
        private DataTable GetDoctorNurseID(int wardId, string bedNo)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(wardId);
                request.AddData(bedNo);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "BedManagementController", "GetDoctorNurseID", requestAction);
            return retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 取消床位分配
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        [WinformMethod]
        public void CancelTheBed(int patListId, int wardId, string bedNo)
        {
            if (MessageBoxShowYesNo("确定要取消分配吗？") == DialogResult.Yes)
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                  {
                      request.AddData(patListId);
                      request.AddData(wardId);
                      request.AddData(bedNo);
                  });

                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "BedManagementController", "CancelTheBed", requestAction);
                bool result = retdata.GetData<bool>(0);

                if (result)
                {
                    MessageBoxShowSimple("取消床位分配成功！");
                    GetBedList(wardId);
                }
                else
                {
                    MessageBoxShowSimple("床位病人已产生费用,取消床位分配失败！");
                }
            }
        }

        /// <summary>
        /// 取消包床
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        [WinformMethod]
        public void CancelPackBed(int patListId, int wardId, string bedNo)
        {
            if (MessageBoxShowYesNo("确定要取消包床吗？") == DialogResult.Yes)
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(patListId);
                    request.AddData(wardId);
                    request.AddData(bedNo);
                });

                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "BedManagementController", "CancelPackBed", requestAction);
                string result = retdata.GetData<string>(0);

                if (string.IsNullOrEmpty(result))
                {
                    MessageBoxShowSimple("取消包床成功！");
                    GetBedList(wardId);
                }
                else
                {
                    MessageBoxShowSimple(result);
                }
            }
        }

        /// <summary>
        /// 获取当前病区所有空床床位号
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        /// <param name="patName">病人姓名</param>
        /// <param name="serialNumber">病人住院号</param>
        /// <returns>true：取得成功/false：取得失败</returns>
        public bool GetBedNoList(int wardId, string bedNo, string patName, string serialNumber)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(wardId);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "BedManagementController", "GetBedNoList", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);

            if (dt != null && dt.Rows.Count > 0)
            {
                patientBedChanging.Bind_BedNoList(dt, bedNo, patName, serialNumber);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 打开换床界面
        /// </summary>
        [WinformMethod]
        public void ShowFrmPatientBedChanging()
        {
            if (GetBedNoList(Convert.ToInt32(bedManagement.Bed.WardCode), bedManagement.Bed.BedNo, bedManagement.Bed.PatientName, bedManagement.Bed.PatientNum))
            {
                ((Form)iBaseView["FrmPatientBedChanging"]).ShowDialog();
            }
            else
            {
                MessageBoxShowSimple("当前病区已经没有空床，不能进行换床！");
            }
        }

        /// <summary>
        /// 保存换床数据
        /// </summary>
        /// <param name="newBedNo">新床位号</param>
        [WinformMethod]
        public void SaveBedChanging(string newBedNo)
        {
            if (MessageBoxShowYesNo(string.Format("确定要为病人【{0}】换床吗？", bedManagement.Bed.PatientName)) == DialogResult.Yes)
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(newBedNo); // 新床位号
                    request.AddData(bedManagement.Bed.BedNo); // 旧床位号
                    request.AddData(bedManagement.Bed.PatientID); // 病人登记ID
                    request.AddData(int.Parse(bedManagement.Bed.WardCode)); // 病区ID
                    request.AddData(LoginUserInfo.EmpId); // 操作人ID
                    request.AddData(LoginUserInfo.WorkId); // 操作人ID
                });

                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "BedManagementController", "SaveBedChanging", requestAction);
                string msg = retdata.GetData<string>(0);

                if (!string.IsNullOrEmpty(msg))
                {
                    MessageBoxShowSimple(msg);
                }
                else
                {
                    MessageBoxShowSimple("换床成功！");
                    patientBedChanging.ClosrForm();
                    GetBedList(int.Parse(bedManagement.Bed.WardCode));
                }
            }
        }

        /// <summary>
        /// 获取年龄（包括年龄类型）
        /// </summary>
        /// <param name="ageValue">未处理年龄</param>
        /// <returns>带前缀(Y、M、D、H)的年龄信息</returns>
        private string GetAge(string ageValue)
        {
            string age = string.Empty;
            if (!string.IsNullOrEmpty(ageValue))
            {
                switch (ageValue.Substring(ageValue.Length - 1))
                {
                    // 岁
                    case "岁":
                        age = string.Format("Y{0}", ageValue.Substring(0, ageValue.Length - 1));
                        break;
                    // 月
                    case "月":
                        age = string.Format("M{0}", ageValue.Substring(0, ageValue.Length - 1));
                        break;
                    // 天
                    case "天":
                        age = string.Format("D{0}", ageValue.Substring(0, ageValue.Length - 1));
                        break;
                    // 时
                    case "时":
                        age = string.Format("H{0}", ageValue.Substring(0, ageValue.Length - 1));
                        break;
                }
            }

            return age;
        }

        /// <summary>
        /// 修改医生或护士--绑定界面医生和护士列表
        /// </summary>
        [WinformMethod]
        public void ShowFrmUpdatePatient()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(int.Parse(bedManagement.Bed.WardCode));
                request.AddData(bedManagement.Bed.BedNo);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "BedManagementController", "GetPatDoctorNurseID", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataTable doctorDt = GetEmpDataSourceType(1);  // 获取医生列表
                updatePatientInfo.Bind_CurrDoctor(doctorDt, int.Parse(dt.Rows[0][1].ToString()));
                DataTable nurseDt = GetEmpDataSourceType(2);  // 获取护士列表
                updatePatientInfo.Bind_CurrNurse(nurseDt, int.Parse(dt.Rows[0][2].ToString()), bedManagement.Bed.PatientName);
                ((Form)iBaseView["FrmUpdatePatientInfo"]).ShowDialog();
            }
        }

        /// <summary>
        /// 更换医生或护士--保存新的医生护士数据
        /// </summary>
        [WinformMethod]
        public void SaveUpdatePatient()
        {
            if (MessageBoxShowYesNo("确定要更换医生或护士吗？") == DialogResult.Yes)
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(bedManagement.Bed.PatientID);   // 病人登记ID
                    request.AddData(int.Parse(bedManagement.Bed.WardCode));// 病区ID
                    request.AddData(bedManagement.Bed.BedNo);// 床位号
                    request.AddData(updatePatientInfo.DoctorID); // 新医生ID
                    request.AddData(updatePatientInfo.NurseId);// 新护士ID
                });

                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "BedManagementController", "SaveUpdatePatient", requestAction);
                bool result = retdata.GetData<bool>(0);

                if (result)
                {
                    MessageBoxShowSimple("更换医生护士成功！");
                    updatePatientInfo.ColseForm();
                    GetBedList(int.Parse(bedManagement.Bed.WardCode));
                }
                else
                {
                    MessageBoxShowSimple("更换医生护士失败！");
                }
            }
        }

        /// <summary>
        /// 打开包床界面
        /// </summary>
        [WinformMethod]
        public void ShowFrmPackBed()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(int.Parse(bedManagement.Bed.WardCode));// 病区ID
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "BedManagementController", "GetInTheHospitalPatList", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            packBed.Bind_InHospitalPatList(dt);
            ((Form)iBaseView["FrmPackBed"]).ShowDialog();
        }

        /// <summary>
        /// 保存包床数据
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">旧床号</param>
        [WinformMethod]
        public void SavePackBedData(int wardId, string bedNo)
        {
            if (MessageBoxShowYesNo("确定要包床吗？") == DialogResult.Yes)
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(bedManagement.Bed.BedNo);// 包床床号
                    request.AddData(wardId);
                    request.AddData(bedNo);
                    request.AddData(LoginUserInfo.EmpId);
                    request.AddData(LoginUserInfo.WorkId);
                });

                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "BedManagementController", "SavePackBedData", requestAction);
                string msg = retdata.GetData<string>(0);

                if (!string.IsNullOrEmpty(msg))
                {
                    MessageBoxShowSimple(msg);
                }
                else
                {
                    MessageBoxShowSimple("包床成功！");
                    packBed.CloseForm();
                    GetBedList(int.Parse(bedManagement.Bed.WardCode));
                }
            }
        }

        /// <summary>
        /// 病人定义出院
        /// </summary>
        [WinformMethod]
        public void PatientOutHospital()
        {
            // 检查是否已开出院医嘱
            Action<ClientRequestData> patrequestAction = ((ClientRequestData request) =>
            {
                request.AddData(bedManagement.Bed.PatientID);
            });

            // 获取病人出院医嘱数据
            ServiceResponseData responData = InvokeWcfService(
                "IPProject.Service",
                "BedManagementController",
                "IsExistenceDischargeOrder",
                patrequestAction);
            DataTable patDt = responData.GetData<DataTable>(0);
            int isLeaveHosOrder = Tools.ToInt32(patDt.Rows[0]["IsLeaveHosOrder"]);
            DateTime leaveHDate = Convert.ToDateTime(patDt.Rows[0]["LeaveHDate"]);

            if (isLeaveHosOrder == 0)
            {
                MessageBoxShowSimple("定义出区失败，当前病人医生未开出区医嘱！");
                return;
            }

            // 不在指定日期内办理出区
            if (DateTime.Compare(
                Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")),
                Convert.ToDateTime(leaveHDate.ToString("yyyy-MM-dd"))) > 0)
            {
                if (MessageBoxShowYesNo("当前日期已超出出区医嘱中的出区日期，确定继续办理出区吗？") == DialogResult.No)
                {
                    return;
                }
            }
            else if (DateTime.Compare(
                Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")),
                Convert.ToDateTime(leaveHDate.ToString("yyyy-MM-dd"))) < 0)
            {
                if (MessageBoxShowYesNo("当前日期小于出区医嘱中的出区日期，确定继续办理出区吗？") == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                if (MessageBoxShowYesNo("确定要办理出院吗？") == DialogResult.No)
                {
                    return;
                }
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(bedManagement.Bed.PatientID);
                request.AddData(Convert.ToInt32(bedManagement.Bed.WardCode));
                request.AddData(0);
            });

            ServiceResponseData retdata = InvokeWcfService(
                "IPProject.Service",
                "BedManagementController",
                "PatientOutHospital",
                requestAction);

            bool result = retdata.GetData<bool>(0);
            DataTable reportDt = retdata.GetData<DataTable>(1);

            if (!result)
            {
                // 病人有医嘱、账单未停，或者药品未统领发药
                if (reportDt.Rows.Count > 0)
                {
                    mDischargeConfirmation.Bind_NotStopOrder(reportDt, false);
                }

                ((Form)iBaseView["FrmDischargeConfirmation"]).ShowDialog();
            }
            else
            {
                MessageBoxShowSimple("病人定义出院成功！");
                // 打印出院通知单
                Dictionary<string, object> dic = new Dictionary<string, object>();
                if (reportDt.Rows.Count > 0)
                {
                    if (reportDt.Rows.Count > 0)
                    {
                        dic.Add("Head", LoginUserInfo.WorkName + "出院通知单");
                        dic.Add("PatName", reportDt.Rows[0]["PatName"]);
                        dic.Add("PatSex", reportDt.Rows[0]["Sex"]);
                        dic.Add("PatAge", SetAge(reportDt.Rows[0]["Age"].ToString()));
                        dic.Add("PatDept", reportDt.Rows[0]["Name"]);
                        dic.Add("PatSerialNumber", reportDt.Rows[0]["CaseNumber"]);
                        dic.Add("PatAddress", reportDt.Rows[0]["NAddress"]);
                        dic.Add("PatEnterDiseaseName", reportDt.Rows[0]["EnterDiseaseName"]);
                        dic.Add("LeaveHDateYear", Convert.ToDateTime(reportDt.Rows[0]["LeaveHDate"]).Year);
                        dic.Add("LeaveHDateMonth", Convert.ToDateTime(reportDt.Rows[0]["LeaveHDate"]).Month);
                        dic.Add("LeaveHDateDay", Convert.ToDateTime(reportDt.Rows[0]["LeaveHDate"]).Day);
                        dic.Add("LeaveHDateHH", Convert.ToDateTime(reportDt.Rows[0]["LeaveHDate"]).Hour);
                        dic.Add("OutSituation", reportDt.Rows[0]["OutSituation"]);
                        ReportTool.GetReport(LoginUserInfo.WorkId, 3003, 0, dic, null).PrintPreview(true);
                    }
                }

                GetBedList(int.Parse(bedManagement.Bed.WardCode));
            }
        }

        /// <summary>
        /// 病人转科
        /// </summary>
        [WinformMethod]
        public void PatTransDept()
        {
            // 验证病人是否存在转科医嘱
            Action<ClientRequestData> patRequestAction = ((ClientRequestData request) =>
            {
                request.AddData(bedManagement.Bed.PatientID);
                request.AddData(Convert.ToInt32(bedManagement.Bed.WardCode));
                request.AddData(true);
            });

            // 获取病人转科医嘱数据
            ServiceResponseData patRetdata = InvokeWcfService(
                "IPProject.Service",
                "BedManagementController",
                "CheckPatTransDept",
                patRequestAction);
            DataTable deptResult = patRetdata.GetData<DataTable>(0);

            if (deptResult == null || deptResult.Rows.Count <= 0)
            {
                MessageBoxShowSimple("转科失败，当前病人医生未开转科医嘱！");
                return;
            }

            // 转科时间
            DateTime transDate = Convert.ToDateTime(deptResult.Rows[0]["TransDate"]);
            // 不在指定日期内办理转科
            if (DateTime.Compare(
                Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")),
                Convert.ToDateTime(transDate.ToString("yyyy-MM-dd"))) > 0)
            {
                if (MessageBoxShowYesNo("当前日期已超出转科医嘱中的出区日期，确定继续办理转科吗？") == DialogResult.No)
                {
                    return;
                }
            }
            else if (DateTime.Compare(
                Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")),
                Convert.ToDateTime(transDate.ToString("yyyy-MM-dd"))) < 0)
            {
                if (MessageBoxShowYesNo("当前日期小于转科医嘱中的出区日期，确定继续办理转科吗？") == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                if (MessageBoxShowYesNo("确定要办理转科吗？") == DialogResult.No)
                {
                    return;
                }
            }
            // 执行转科
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(bedManagement.Bed.PatientID);
                request.AddData(Convert.ToInt32(bedManagement.Bed.WardCode));
                request.AddData(Tools.ToInt32(deptResult.Rows[0]["NewDeptID"]));
            });

            ServiceResponseData retdata = InvokeWcfService(
                "IPProject.Service",
                "BedManagementController",
                "PatientOutHospital",
                requestAction);

            bool result = retdata.GetData<bool>(0);
            DataTable reportDt = retdata.GetData<DataTable>(1);

            if (!result)
            {
                // 病人有医嘱、账单未停，或者药品未统领发药
                if (reportDt.Rows.Count > 0)
                {
                    mDischargeConfirmation.Bind_NotStopOrder(reportDt, true);
                }

                ((Form)iBaseView["FrmDischargeConfirmation"]).ShowDialog();
            }
            else
            {
                // 转科成功
                MessageBoxShowSimple("病人转科成功，请登录新科室为病人分配床位！");
                GetBedList(int.Parse(bedManagement.Bed.WardCode));
            }
        }

        /// <summary>
        /// 将年龄前缀转换成文字
        /// </summary>
        /// <param name="age">带前缀的年龄信息</param>
        /// <returns>转换后的年龄</returns>
        private string SetAge(string age)
        {
            string temp = age.Substring(0, 1);
            string strAge = string.Empty;
            switch (temp)
            {
                case "Y":
                    strAge = age.Substring(1) + "岁";
                    break;
                case "M":
                    strAge = age.Substring(1) + "月";
                    break;
                case "D":
                    strAge = age.Substring(1) + "天";
                    break;
                case "H":
                    strAge = age.Substring(1) + "时";
                    break;
            }

            return strAge;
        }

        #region "临床部分代码"
        /// <summary>
        /// 获取科室列表
        /// </summary>
        //[WinformMethod]
        private void GetDeptList()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(false);
            });

            // 通过WCF调用服务端控制器取得住院临床科室列表
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetDeptBasicData", requestAction);
            deptDt = retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 查询病人列表
        /// </summary>
        [WinformMethod]
        public void GetNursingStationPatList()
        {
            if (bedManagement.PatStatus != 2)
            {
                if (bedManagement.IsReminder)
                {
                    MessageBoxShowSimple("请选择在院状态，然后再进行催款病人查询！");
                    return;
                }
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(bedManagement.PatStatus);
                request.AddData(bedManagement.DeptId);
                request.AddData(bedManagement.StartTime);
                request.AddData(bedManagement.EndTime);
                request.AddData(bedManagement.IsReminder);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "BedManagementController", "GetNursingStationPatList", requestAction);
            DataTable patListDt = retdata.GetData<DataTable>(0);
            bedManagement.Bind_PatList(patListDt);
        }

        #endregion

        #region "共用方法"

        /// <summary>
        /// 提示消息
        /// </summary>
        /// <param name="msg">消息内容</param>
        [WinformMethod]
        public void MessageShow(string msg)
        {
            MessageBoxShowSimple(msg);
        }

        #endregion

        /// <summary>
        /// 打印催款单
        /// </summary>
        [WinformMethod]
        public void PrintReminder()
        {
            // 选中的床位上没有分配病人
            if (bedManagement.Bed.PatientID <= 0 && string.IsNullOrEmpty(bedManagement.Bed.PatientName))
            {
                return;
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(bedManagement.Bed.PatientID);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "BedManagementController", "GetReminderConfigInfo", requestAction);
            // 获取催款数据
            DataTable feeDt = retdata.GetData<DataTable>(0);
            string reminderMoney = retdata.GetData<string>(1);  // 继续交款金额

            // 打印催款单
            DataTable reminderDt = new DataTable();
            reminderDt.Columns.Add("DeptName");
            reminderDt.Columns.Add("SerialNumber");
            reminderDt.Columns.Add("BedNo");
            reminderDt.Columns.Add("PatName");
            reminderDt.Columns.Add("SumFee");
            reminderDt.Columns.Add("DepositFee");
            reminderDt.Columns.Add("ReminderMoney");
            reminderDt.Columns.Add("ReminderData");
            reminderDt.Columns.Add("PrintReminderData");
            DataRow reminderDr = reminderDt.NewRow();
            reminderDr["DeptName"] = bedManagement.Bed.Dept;
            reminderDr["SerialNumber"] = bedManagement.Bed.PatientNum;
            reminderDr["BedNo"] = bedManagement.Bed.BedNo;
            reminderDr["PatName"] = bedManagement.Bed.PatientName;
            reminderDr["SumFee"] = feeDt.Rows[1]["TotalFee"];
            reminderDr["DepositFee"] = feeDt.Rows[0]["TotalFee"];
            reminderDr["ReminderMoney"] = reminderMoney;
            reminderDr["ReminderData"] = DateTime.Now;
            reminderDr["PrintReminderData"] = DateTime.Now;
            reminderDt.Rows.Add(reminderDr);
            ReportTool.GetReport(LoginUserInfo.WorkId, 3202, 0, null, reminderDt).PrintPreview(true);
        }

        /// <summary>
        /// 批量打印催款单
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        [WinformMethod]
        public void BatchPrintReminder(string patListID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patListID);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "BedManagementController", "GetReminderDataList", requestAction);
            DataTable reminderDataDt = retdata.GetData<DataTable>(0);

            if (reminderDataDt.Rows.Count <= 0)
            {
                MessageBoxShowSimple("所选病人已交款或余额充足，无需打印催款单！");
                return;
            }

            ReportTool.GetReport(LoginUserInfo.WorkId, 3202, 0, null, reminderDataDt).PrintPreview(true);
        }

        /// <summary>
        /// 科室发送界面
        /// </summary>
        /// <param name="iPatientList">病人列表</param>
        [WinformMethod]
        public void DeptOrderCheck(List<int> iPatientList)
        {
            if (iPatientList.Count > 0)
            {
                mIDeptOrderCheck.iPatientList = iPatientList;
                mIDeptOrderCheck.InitWindow();
                ((Form)iBaseView["FrmDeptOrderCheck"]).ShowDialog();
            }
            else
            {
                MessageBoxShowError("请选择需要发送的病人");
            }
        }

        /// <summary>
        /// 科室发送执行
        /// </summary>
        /// <param name="iOrderCategory">医嘱类型</param>
        /// <param name="bAccountCategory">是否发送账单</param>
        /// <param name="bBed">是否发送床位费</param>
        /// <param name="enddate">发送结束时间</param>
        [WinformMethod]
        public void DoDeptOrderCheck(int iOrderCategory, bool bAccountCategory, bool bBed, DateTime enddate)
        {
            List<string> sError = new List<string>();
            try
            {
                if (enddate.Date < DateTime.Now.Date)
                {
                    MessageBoxShowError("截止日期不合理！请确认！");
                    mIDeptOrderCheck.InitWindow();
                    return;
                }

                for (int i = 0; i < mIDeptOrderCheck.iPatientList.Count; i++)
                {
                    #region 1.先发送医嘱
                    Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(mIDeptOrderCheck.iPatientList[i]);
                        request.AddData(iOrderCategory);
                        request.AddData(enddate);
                        request.AddData(LoginUserInfo.EmpId);
                    });

                    ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderCheckController", "DeptSendOrderCheckList", requestAction);
                    bool b = retdata.GetData<bool>(0);

                    if (b)
                    {
                        List<string> sResult = retdata.GetData<List<string>>(1);
                        sError.AddRange(sResult);
                    }
                    else
                    {
                        sError.AddRange(retdata.GetData<List<string>>(1));
                    }

                    #endregion

                    mIDeptOrderCheck.Progress = Convert.ToInt32((i + 1) * 50 / mIDeptOrderCheck.iPatientList.Count);

                    #region 2.发送账单
                    if (bAccountCategory)
                    {
                        //2.发送账单
                        Action<ClientRequestData> requestAction1 = ((ClientRequestData request) =>
                        {
                            request.AddData(mIDeptOrderCheck.iPatientList[i]);
                            request.AddData(enddate);
                            request.AddData(LoginUserInfo.EmpId);
                        });

                        ServiceResponseData retdata1 = InvokeWcfService("IPProject.Service", "OrderCheckController", "DeptSendAccountCheckList", requestAction1);
                        bool b1 = retdata.GetData<bool>(0);

                        if (b1)
                        {
                            List<string> sResult = retdata.GetData<List<string>>(1);
                            sError.AddRange(sResult);
                        }
                        else
                        {
                            sError.AddRange(retdata.GetData<List<string>>(1));
                        }
                    }
                    #endregion

                    #region 2.发送床位费
                    if (bBed)
                    {
                        //2.发送床位费
                        Action<ClientRequestData> requestAction1 = ((ClientRequestData request) =>
                        {
                            request.AddData(mIDeptOrderCheck.iPatientList[i]);
                            request.AddData(enddate);
                            request.AddData(LoginUserInfo.EmpId);
                        });

                        ServiceResponseData retdata1 = InvokeWcfService("IPProject.Service", "OrderCheckController", "DeptSendAccountCheckList", requestAction1);
                        bool b1 = retdata.GetData<bool>(0);

                        if (b1)
                        {
                            List<string> sResult = retdata.GetData<List<string>>(1);
                            sError.AddRange(sResult);
                        }
                        else
                        {
                            sError.AddRange(retdata.GetData<List<string>>(1));
                        }
                    }
                    #endregion
                    mIDeptOrderCheck.Progress = Convert.ToInt32((i + 1) * 100 / mIDeptOrderCheck.iPatientList.Count);
                }

                mIDeptOrderCheck.RTxtError = sError;
                mIDeptOrderCheck.FreshSendState(1);
            }
            catch (Exception e)
            {
                MessageBoxShowError(e.Message);
                mIDeptOrderCheck.InitWindow();
            }
        }

        /// <summary>
        /// 打开院内血糖图表
        /// </summary>
        [WinformMethod]
        public void OpenBloodGlucoseChart()
        {
            string visitId = Newtonsoft.Json.JsonConvert.SerializeObject(Convert.ToDecimal(bedManagement.Bed.PatientNum));
            string patientName = bedManagement.Bed.PatientName;
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderMainController", "GetBloodUrl");
            string bloodUrl = retdata.GetData<string>(0) + visitId;
            InvokeController("MainFrame.UI", "wcfclientLoginController", "ShowBrowser", "血糖图表：" + patientName, bloodUrl);
        }

        /// <summary>
        /// 显示三测单界面
        /// </summary>
        /// <param name="patListID"></param>
        /// <param name="bedNo"></param>
        /// <param name="patName"></param>
        /// <param name="SerialNumber"></param>
        /// <param name="CaseNumber"></param>
        [WinformMethod]
        public void ShowFemTemperature(int patListID, string bedNo, string patName, decimal serialNumber, decimal caseNumber)
        {
            InvokeController(
                "EMRDocProject.UI", 
                "TemperatureManageController", 
                "ShowFemTemperature", 
                patListID);
        }

    }
}
