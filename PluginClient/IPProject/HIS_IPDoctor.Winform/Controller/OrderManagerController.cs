using System;
using System.Collections.Generic;
using System.Data;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.ClinicManage;
using HIS_IPDoctor.Winform.IView;

namespace HIS_IPDoctor.Winform.Controller
{
    [WinformController(DefaultViewName = "FrmIPOrder")]//与系统菜单对应
    [WinformView(Name = "FrmIPDoctorMain", DllName = "HIS_IPDoctor.Winform.dll", ViewTypeName = "HIS_IPDoctor.Winform.ViewForm.FrmIPDoctorMain")]
    //设置
    [WinformView(Name = "FrmOrderManager", DllName = "HIS_IPDoctor.Winform.dll", ViewTypeName = "HIS_IPDoctor.Winform.ViewForm.FrmOrderManager")]

    /// <summary>
    /// 主界面，医嘱管理界面控制器
    /// </summary>
    public class OrderManagerController : WcfClientController
    {
        /// <summary>
        /// 主界面接口
        /// </summary>
        IFrmIPDoctorMain ifrmIPDoctorMain;

        /// <summary>
        /// 医嘱管理界面接口
        /// </summary>
        IFrmOrderManager ifrmOrderManager;

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            ifrmIPDoctorMain = (IFrmIPDoctorMain)iBaseView["FrmIPDoctorMain"];
            ifrmOrderManager = (IFrmOrderManager)iBaseView["FrmOrderManager"];
        }

        /// <summary>
        /// 控件加载数据异步加载
        /// </summary>
        public override void AsynInit()
        {
            ifrmIPDoctorMain.LoadCompleted = false;
            ifrmOrderManager.BindControlData(); 
        }

        /// <summary>
        /// 控件加载数据异步加载完成
        /// </summary>
        public override void AsynInitCompleted()
        {
            ifrmOrderManager.BindControlDataComplete();
            ifrmIPDoctorMain.LoadCompleted = true;
        }

        #region 主界面
        /// <summary>
        /// 获取床位病人列表
        /// </summary>
        [WinformMethod]
        public void GetBedPatient()
        {
            int deptid = ifrmIPDoctorMain.DeptId;
            if (deptid == 0)
            {
                deptid = LoginUserInfo.DeptId;
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.EmpId);
                request.AddData(deptid);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderMainController", "GetBedPatient", requestAction);
            DataTable dtmyPatient = retdata.GetData<DataTable>(0);
            DataTable dtDeptPatient = retdata.GetData<DataTable>(1);
            ifrmIPDoctorMain.BindMyBedPatient(dtmyPatient);
            ifrmIPDoctorMain.BindDeptBedPatient(dtDeptPatient);
        }

        /// <summary>
        /// 获取科室列表
        /// </summary>
        [WinformMethod]
        public void GetDept()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderMainController", "GetDeptList", requestAction);
            DataTable dtDept = retdata.GetData<DataTable>(0);
            ifrmIPDoctorMain.BindDept(dtDept);
        }

        /// <summary>
        /// 获取血糖URl
        /// </summary>
        [WinformMethod]
        public void GetBloodUrl()
        {          
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderMainController", "GetBloodUrl");
            string bloodUrl = retdata.GetData<string>(0);
            ifrmIPDoctorMain.BloodUrl = bloodUrl;
        }

        /// <summary>
        /// 打开血糖Url页面
        /// </summary>
        /// <param name="patientNum">住院号</param>
        /// <param name="patName">病人姓名</param>
        [WinformMethod]
        public void OpenBrowser(string patientNum,string patName)
        {
          //  string visitId = Newtonsoft.Json.JsonConvert.SerializeObject(Convert.ToDecimal(patientNum));
            string url = ifrmIPDoctorMain.BloodUrl + patientNum;
            InvokeController("MainFrame.UI", "wcfclientLoginController", "ShowBrowser", "血糖图表："+ patName,  url);
        }

        /// <summary>
        /// 获取出院情况
        /// </summary>
        /// <returns>出院情况</returns>
        [WinformMethod]
        public DataTable  getOutSituation()
        {           
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "getOutSituation");
            DataTable dtOutSituation = retdata.GetData<DataTable>(0);
            return dtOutSituation;
        }

        /// <summary>
        /// 获取护理级别
        /// </summary>
        /// <returns>护理级别</returns>
        [WinformMethod]
        public DataTable getNusingLever()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderMainController", "getNusingLevel");
            DataTable dtNusingLeve = retdata.GetData<DataTable>(0);
            return dtNusingLeve;
        }

        /// <summary>
        /// 获取饮食种类
        /// </summary>
        /// <returns>饮食种类</returns>
        [WinformMethod]
        public DataTable getDietType()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderMainController", "getDietType");
            DataTable dtDietTyp = retdata.GetData<DataTable>(0);
            return dtDietTyp;
        }

        /// <summary>
        /// 修改护理级别
        /// </summary>
        /// <param name="patlistid">病人Id</param>
        /// <param name="nursingName">护理级别</param>
        [WinformMethod]
        public void UpdatePatNursing(int patlistid, string nursingName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patlistid);
                request.AddData(nursingName);
                request.AddData(LoginUserInfo.WorkId);
                request.AddData(LoginUserInfo.EmpId);
                request.AddData(LoginUserInfo.DeptId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderMainController", "UpdatePatNursing", requestAction);
            MessageBoxShowSimple("修改护理等级成功");
        }

        /// <summary>
        /// 修改病人情况
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <param name="situationCode">病人情况Code</param>
        [WinformMethod]
        public void UpdatePatSituation(int patlistid, string situationCode)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patlistid);
                request.AddData(situationCode);
                request.AddData(LoginUserInfo.WorkId);
                request.AddData(LoginUserInfo.EmpId);
                request.AddData(LoginUserInfo.DeptId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderMainController", "UpdatePatSituation", requestAction);
            MessageBoxShowSimple("修改病人情况成功");
        }

        /// <summary>
        /// 修改饮食种类
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <param name="dietTypeName">饮食种类</param>
        [WinformMethod]
        public void UpdatePatDietType(int patlistid, string dietTypeName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patlistid);
                request.AddData(dietTypeName);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderMainController", "UpdatePatDietType", requestAction);
            MessageBoxShowSimple("修改饮食等级成功");
        }
        #endregion

        #region 医嘱管理界面
        /// <summary>
        /// 界面初始化获取数据
        /// </summary>
        [WinformMethod]
        public void OrderManagerInit()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetDeptList", requestAction);
            DataTable dtDept = retdata.GetData<DataTable>(0);
            ifrmOrderManager.BindDept(dtDept);
            ifrmOrderManager.DeptId = ifrmIPDoctorMain.DeptId;
            requestAction = ((ClientRequestData request) =>
             {
                 request.AddData(LoginUserInfo.EmpId);
                 request.AddData(ifrmOrderManager.DeptId);
                 request.AddData(DateTime.Now);
                 request.AddData(DateTime.Now);
                 request.AddData(false);
                 request.AddData(true);
                 request.AddData(string.Empty);
             });
            retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetPatientInfo", requestAction);
            DataTable dtPat = retdata.GetData<DataTable>(0);
            ifrmOrderManager.BindPatInfo(dtPat);
            ifrmOrderManager.ShowPatDetailInfo(null, null);
            if (ifrmIPDoctorMain.SelectPatListID != 0)
            {
                requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(ifrmIPDoctorMain.SelectPatListID);
                });
                retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetPatientInfoByPatListID", requestAction);
                DataTable dtPatInfo = retdata.GetData<DataTable>(0);
                DataTable dtPatFee = retdata.GetData<DataTable>(1);
                ifrmOrderManager.CurPatListId = ifrmIPDoctorMain.SelectPatListID;
                if (ifrmOrderManager.patDeptID != Convert.ToInt32(dtPatInfo.Rows[0]["PatDeptID"]))
                {
                    GetDrugStore();
                }

                bool hasNotFinisTrans = retdata.GetData<bool>(2);
                ifrmOrderManager.patDeptID = Convert.ToInt32(dtPatInfo.Rows[0]["PatDeptID"]);
                ifrmOrderManager.ShowPatDetailInfo(dtPatInfo.Rows[0], dtPatFee);
                ifrmOrderManager.LoadPatData(ifrmOrderManager.CurPatListId, Convert.ToInt32(dtPatInfo.Rows[0]["IsLeaveHosOrder"]), ifrmOrderManager.DeptId, ifrmOrderManager.presDeptName, LoginUserInfo.EmpId, LoginUserInfo.EmpName, Convert.ToInt32(dtPatInfo.Rows[0]["PatDeptID"]), hasNotFinisTrans);
                GetApplyHead(1);
            }
        }

        /// <summary>
        /// 获取病人列表
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="isOut">是否出院false在院true出院</param>
        /// <param name="isMy">true我的病人false科室病人</param>
        /// <param name="queryContent">查询条件</param>
        [WinformMethod]
        public void GetPatInfoList(DateTime bdate, DateTime edate, bool isOut, bool isMy, string queryContent)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.EmpId);
                request.AddData(ifrmOrderManager.DeptId);
                request.AddData(bdate);
                request.AddData(edate);
                request.AddData(isOut);
                request.AddData(isMy);
                request.AddData(queryContent);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetPatientInfo", requestAction);
            DataTable dtPatInfo = retdata.GetData<DataTable>(0);
            ifrmOrderManager.BindPatInfo(dtPatInfo);
        }

        /// <summary>
        /// 显示病人详细信息
        /// </summary>
        /// <param name="drPatInfo">病人信息</param>
        [WinformMethod]
        public void ShowPatDetail(DataRow drPatInfo)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
           {
               request.AddData(ifrmOrderManager.CurPatListId);
           });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetPatientFeeInfo", requestAction);
            DataTable dtPatFee = retdata.GetData<DataTable>(0);
            bool hasNotfinishTrans = retdata.GetData<bool>(1);
            if (ifrmOrderManager.patDeptID != Convert.ToInt32(drPatInfo["PatDeptID"]))
            {
                GetDrugStore();
            }

            ifrmOrderManager.patDeptID = Convert.ToInt32(drPatInfo["PatDeptID"]);
            ifrmOrderManager.ShowPatDetailInfo(drPatInfo, dtPatFee);
            ifrmOrderManager.LoadPatData(ifrmOrderManager.CurPatListId, Convert.ToInt32(drPatInfo["IsLeaveHosOrder"]), ifrmOrderManager.DeptId, ifrmOrderManager.presDeptName, LoginUserInfo.EmpId, LoginUserInfo.EmpName, Convert.ToInt32(drPatInfo["PatDeptID"]), hasNotfinishTrans);
        }

        /// <summary>
        /// 刷新病人信息
        /// </summary>
        [WinformMethod]
        public void FreshPatDetail()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(ifrmOrderManager.CurPatListId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetPatientInfoByPatListID", requestAction);
            DataTable dtPatInfo = retdata.GetData<DataTable>(0);
            DataTable dtPatFee = retdata.GetData<DataTable>(1);
            if (ifrmOrderManager.patDeptID != Convert.ToInt32(dtPatInfo.Rows[0]["PatDeptID"]))
            {
                GetDrugStore();
            }

            bool hasNotFinisTrans = retdata.GetData<bool>(2);
            ifrmOrderManager.patDeptID = Convert.ToInt32(dtPatInfo.Rows[0]["PatDeptID"]);
            ifrmOrderManager.ShowPatDetailInfo(dtPatInfo.Rows[0], dtPatFee);
            ifrmOrderManager.LoadPatData(ifrmOrderManager.CurPatListId, Convert.ToInt32(dtPatInfo.Rows[0]["IsLeaveHosOrder"]), ifrmOrderManager.DeptId, ifrmOrderManager.presDeptName, LoginUserInfo.EmpId, LoginUserInfo.EmpName, Convert.ToInt32(dtPatInfo.Rows[0]["PatDeptID"]), hasNotFinisTrans);
        }

        /// <summary>
        /// 获取默认科室
        /// </summary>
        private void GetDrugStore()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetDrugStore");
            DataTable dtDrugStor = retdata.GetData<DataTable>(0);
            if (dtDrugStor == null || dtDrugStor.Rows.Count == 1)
            {
                ifrmOrderManager.DrugStoreVisible = false;
            }
            else
            {
                int detpid = ifrmOrderManager.DeptId;
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(detpid);
                });
                retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetDrugStoreByDeptID", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                if (ifrmOrderManager.DefaultDrugStore != dt.Rows[0]["DeptIDs"].ToString())
                {
                    ifrmOrderManager.DrugStoreDataBind(dt);
                }
            }
        }

        /// <summary>
        /// 消息提示
        /// </summary>
        /// <param name="message">消息内容</param>
        [WinformMethod]
        public void ShowMessage(string message)
        {
            MessageBoxShowSimple(message);
        }

        #region 医嘱模板
        /// <summary>
        /// 获取模板内容
        /// </summary>
        /// <param name="modelLevel">模板级别</param>
        [WinformMethod]
        public void GetOrderTempList(int modelLevel)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(modelLevel);
                request.AddData(LoginUserInfo.DeptId);
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderTempManageController", "GetOrderTempList", requestAction);
            List<IPD_OrderModelHead> orderTempList = retdata.GetData<List<IPD_OrderModelHead>>(0);
            ifrmOrderManager.bind_FeeTempList(orderTempList, 0);
        }

        /// <summary>
        /// 获取模板明细数据
        /// </summary>
        /// <param name="modelHeadID">模板头ID</param>
        [WinformMethod]
        public void GetOrderTempDetail(int modelHeadID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(modelHeadID);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderTempManageController", "GetOrderTempDetail", requestAction);
            DataTable orderDetails = retdata.GetData<DataTable>(0);
            // 过滤数据
            DataTable longDetails = orderDetails.Clone();
            DataTable tempDetails = orderDetails.Clone();
            orderDetails.TableName = "OrderDetails";
            // 过滤长期医嘱明细数据
            DataView longView = new DataView(orderDetails);
            string longSqlWhere = " OrderCategory = 0";
            longView.RowFilter = longSqlWhere;
            longDetails.Merge(longView.ToTable());
            DataColumn col = new DataColumn();
            col.ColumnName = "Sel";
            col.DataType = typeof(int);
            col.DefaultValue = 1;
            longDetails.Columns.Add(col);

            // 过滤临时医嘱明细数据
            DataView tempView = new DataView(orderDetails);
            string tempSqlWhere = " OrderCategory = 1";
            tempView.RowFilter = tempSqlWhere;
            tempDetails.Merge(tempView.ToTable());
            col = new DataColumn();
            col.ColumnName = "Sel";
            col.DataType = typeof(int);
            col.DefaultValue = 1;
            tempDetails.Columns.Add(col);
            ifrmOrderManager.Bind_OrderDetails(longDetails, tempDetails);
        }
        #endregion
        #endregion

        #region 检查检验申请单
        /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="applyheadId">申诊头ID</param>
        /// <param name="applyType">申请类型</param>
        /// <param name="applyStatus">申请单状态</param>
        /// <param name="isReturns">isReturns</param>
        [WinformMethod]
        public void ShowApply(string applyheadId, string applyType, string applyStatus, string isReturns)
        {
            int memberid = 0;
            int patientid = ifrmOrderManager.CurPatListId;
            if (patientid > 0)
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(patientid);
                });
                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetPatientInfoByPatListID", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        memberid = Convert.ToInt32(dt.Rows[0]["MemberID"].ToString());
                    }
                }

                InvokeController("OPProject.UI", "MedicalApplyController", "ShowMedicalApply", memberid, patientid, 1, applyheadId, applyType, ifrmOrderManager.DeptId, applyStatus, isReturns);
            }
        }

        /// <summary>
        /// 获取申请明细表头信息
        /// </summary>
        /// <param name="headId">申请头ID</param>
        /// <returns>返回明细信息></returns>
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
        /// 获取申请明细表头信息
        /// </summary>
        /// <param name="systemType">1门诊2住院</param>
        [WinformMethod]
        public void GetApplyHead(int systemType)
        {
            int patid = ifrmOrderManager.CurPatListId;
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
                ifrmOrderManager.BindApplyHead(dt);
            }
        }

        /// <summary>
        /// 删除申请明细表头信息
        /// </summary>
        /// <param name="applyheadId">申请头ID</param>
        [WinformMethod]
        public void DelApplyHead(string applyheadId)
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(applyheadId);
                    request.AddData(1);
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
                //GetApplyHead(0);
                OrderManagerInit();
            }
            catch (Exception e)
            {
                MessageBoxEx.Show(e.Message);
            }
        }
        #endregion

        #region 住院病人诊断
        /// <summary>
        /// 获取病人诊断信息
        /// </summary>
        [WinformMethod]     
        public void LoadDiagnosisInfo()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(ifrmOrderManager.CurPatListId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DiagnosisController", "LoadDiagnosisInfo", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);

            //获取诊断类型信息
            ServiceResponseData retDiagClass = InvokeWcfService("IPProject.Service", "DiagnosisController", "GetDiagnosisClass");
            DataTable dtDiagClass = retDiagClass.GetData<DataTable>(0);

            //获取诊断基础数据信息
            ServiceResponseData retDiagBasic = InvokeWcfService("IPProject.Service", "DiagnosisController", "GetBasicDiagnosis");
            DataTable dtDiagBasic = retDiagBasic.GetData<DataTable>(0);
            ifrmOrderManager.LoadDiagInfo(dt, dtDiagClass, dtDiagBasic);
        }

        /// <summary>
        /// 保存诊断信息
        /// </summary>
        /// <param name="diagClass">诊断类型</param>
        /// <param name="main">是否主诊断</param>
        /// <param name="diagID">诊断ID</param>
        /// <param name="diagName">诊断名称</param>
        /// <param name="iCDCode">ICD编码</param>
        /// <param name="effect">情况</param>
        /// <param name="diagFlag">trueICDfalse自定义</param>
        /// <param name="id">诊断表ID</param>
        /// <returns>int</returns>
        [WinformMethod]
        public int SaveDiagInfo(int diagClass, int main, int diagID, string diagName, string iCDCode, string effect, bool diagFlag, int id)
        {
            IPD_Diagnosis diag = new IPD_Diagnosis();

            if (id > 0)
            {
                diag.ID = id;
            }

            diag.PatListID = ifrmOrderManager.CurPatListId;
            diag.DeptID = LoginUserInfo.DeptId;
            diag.DgsDocID = LoginUserInfo.EmpId;
            diag.DiagnosisTime = System.DateTime.Now;
            diag.DiagnosisClass = diagClass;
            diag.Main = main;
            if (diagFlag == true)
            {
                diag.DiagnosisID = diagID;
                diag.ICDCode = iCDCode;
            }

            diag.DiagnosisName = diagName;
            diag.Effect = effect;
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(diag);
                });
                ServiceResponseData retDiagBasic = InvokeWcfService("IPProject.Service", "DiagnosisController", "SaveDiagInfo", requestAction);
                MessageBoxShowSimple("保存成功");
                return retDiagBasic.GetData<int>(0);
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
                return -1;
            }
        }

        /// <summary>
        /// 删除诊断
        /// </summary>
        /// <param name="id">诊断ID</param>
        /// <returns>删除返回成功</returns>
        [WinformMethod]
        public int DeleteDiagInfo(int id)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(id);
            });
            ServiceResponseData retDiagBasic = InvokeWcfService("IPProject.Service", "DiagnosisController", "DeleteDiagnosis", requestAction);
            MessageBoxShowSimple("删除成功");
            return retDiagBasic.GetData<int>(0);
        }

        /// <summary>
        /// 诊断检查
        /// </summary>
        /// <param name="flag">0新增1修改</param>
        /// <param name="main">是否主诊断</param>
        /// <param name="diagName">诊断名称</param>
        /// <param name="id">诊断表ID</param>
        /// <returns>bool</returns>
        [WinformMethod]
        public bool CheckDiagnosisInfo(int flag, int main, string diagName, int id)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(ifrmOrderManager.CurPatListId);
                request.AddData(flag);
                request.AddData(0);
                request.AddData(main);
                request.AddData(diagName);
                request.AddData(id);
            });
            ServiceResponseData retDiagBasic = InvokeWcfService("IPProject.Service", "DiagnosisController", "CheckDiagnosisInfo", requestAction);
            return retDiagBasic.GetData<bool>(0);
        }
        #endregion

        /// <summary>
        /// 显示病案首页页面
        /// </summary>
        /// <param name="patlistid">病人Id</param>
        /// <param name="deptid">科室Id</param>
        /// <param name="deptname">科室名称</param>
        [WinformMethod]
        public void ShowMedicalCasePage(int patlistid,string  deptid,string deptname)
        {
            int _deptid = Convert.ToInt32(deptid);//科室ID
            InvokeController("EMRDocProject.UI", "FrmMedicalCaseController", "ShowMedicalCaseForm", patlistid, _deptid, deptname,LoginUserInfo.EmpId,LoginUserInfo.EmpName);
        }
    }
}
