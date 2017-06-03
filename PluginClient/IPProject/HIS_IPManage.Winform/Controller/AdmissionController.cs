using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EfwControls.Common;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.IPManage;
using HIS_Entity.OPManage;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.Controller
{
    /// <summary>
    /// 入院病人登记，病人列表查询控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmAdmissionRegistration")]//在菜单上显示
    [WinformView(Name = "FrmAdmissionRegistration", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmAdmissionRegistration")]
    [WinformView(Name = "FrmPatientInfo", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmPatientInfo")]
    //[WinformView(Name = "FrmNursePatientInfo", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmPatientInfo")]
    [WinformView(Name = "FrmPayADeposit", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmPayADeposit")]
    [WinformView(Name = "FrmQueryMenber", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmQueryMenber")]
    public class AdmissionController : WcfClientController
    {
        #region "变量"
        /// <summary>
        ///  病人列表接口
        /// </summary>
        IAdmissionRegistration iAdmissionRegistration;

        /// <summary>
        /// 病人入院接口
        /// </summary>
        IPatientInfo ipatientInfo;

        /// <summary>
        /// 预交金
        /// </summary>
        IPayADeposit ipayADeposit;

        /// <summary>
        /// 待登记病人列表
        /// </summary>
        IQueryMenber iqueryMenber;

        /// <summary>
        /// 下拉列表数据集
        /// </summary>
        private DataSet ds;

        /// <summary>
        /// 是否为二次入院病人
        /// </summary>
        private bool twoAdmission = false;

        #endregion

        #region "初始化重写"
        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            iAdmissionRegistration = (IAdmissionRegistration)DefaultView;
            ipatientInfo = iBaseView["FrmPatientInfo"] as IPatientInfo;
            ipayADeposit = iBaseView["FrmPayADeposit"] as IPayADeposit;
            iqueryMenber = iBaseView["FrmQueryMenber"] as IQueryMenber;
        }

        /// <summary>
        /// 数据加载
        /// </summary>
        public override void AsynInit()
        {
            ds = new DataSet();
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetMasterData");

            // 国籍
            DataTable patNationalityDt = retdata.GetData<DataTable>(0);
            patNationalityDt.TableName = "PatNationalityDt";
            ds.Tables.Add(patNationalityDt);

            // 民族
            DataTable nationDt = retdata.GetData<DataTable>(1);
            nationDt.TableName = "NationDt";
            ds.Tables.Add(nationDt);

            // 职业
            DataTable patJobDt = retdata.GetData<DataTable>(2);
            patJobDt.TableName = "PatJobDt";
            ds.Tables.Add(patJobDt);

            // 教育程度
            DataTable culturalLevelDt = retdata.GetData<DataTable>(3);
            culturalLevelDt.TableName = "CulturalLevelDt";
            ds.Tables.Add(culturalLevelDt);

            // 婚姻状况
            DataTable matrimonyDt = retdata.GetData<DataTable>(4);
            matrimonyDt.TableName = "MatrimonyDt";
            ds.Tables.Add(matrimonyDt);

            // 关系
            DataTable relationDt = retdata.GetData<DataTable>(5);
            relationDt.TableName = "RelationDt";
            ds.Tables.Add(relationDt);

            // 性别
            DataTable patSexDt = retdata.GetData<DataTable>(6);
            patSexDt.TableName = "PatSexDt";
            ds.Tables.Add(patSexDt);

            // 入院情况
            DataTable enterSituationDt = retdata.GetData<DataTable>(7);
            enterSituationDt.TableName = "EnterSituationDt";
            ds.Tables.Add(enterSituationDt);

            // 地区编码
            DataTable regionCodeDt = retdata.GetData<DataTable>(8);
            regionCodeDt.TableName = "RegionCodeDt";
            ds.Tables.Add(regionCodeDt);

            // 病人来源列表
            DataTable sourceWayDt = retdata.GetData<DataTable>(9);
            sourceWayDt.TableName = "SourceWayDt";
            ds.Tables.Add(sourceWayDt);

            // 病区列表
            DataTable enterWardDt = retdata.GetData<DataTable>(10);
            enterWardDt.TableName = "EnterWardDt";
            ds.Tables.Add(enterWardDt);

            // 科室列表
            DataTable deptList = retdata.GetData<DataTable>(11);
            deptList.TableName = "DeptList";
            ds.Tables.Add(deptList);

            // 病人类型列表
            DataTable patTypeDt = retdata.GetData<DataTable>(12);
            patTypeDt.TableName = "PatTypeDt";
            ds.Tables.Add(patTypeDt);

            // 诊断列表
            DataTable patDiseaseDt = retdata.GetData<DataTable>(13);
            patDiseaseDt.TableName = "PatDiseaseDt";
            ds.Tables.Add(patDiseaseDt);

            // 医生列表
            DataTable currDoctorDt = retdata.GetData<DataTable>(14);
            currDoctorDt.TableName = "CurrDoctorDt";
            ds.Tables.Add(currDoctorDt);

            // 护士列表
            DataTable currNurseDt = retdata.GetData<DataTable>(15);
            currNurseDt.TableName = "CurrNurseDt";
            ds.Tables.Add(currNurseDt);
        }

        /// <summary>
        /// 将数据绑定到界面控件
        /// </summary>
        public override void AsynInitCompleted()
        {
            ipatientInfo.Bind_txtPatNationality(ds.Tables["PatNationalityDt"]); // 国籍
            ipatientInfo.Bind_txtNation(ds.Tables["NationDt"]);// 民族
            ipatientInfo.Bind_txtPatJob(ds.Tables["PatJobDt"]);// 职业
            ipatientInfo.Bind_txtCulturalLevel(ds.Tables["CulturalLevelDt"]);// 教育程度
            ipatientInfo.Bind_Matrimony(ds.Tables["MatrimonyDt"]);// 婚姻状况
            ipatientInfo.Bind_txtRelation(ds.Tables["RelationDt"]);// 关系
            ipatientInfo.Bind_PatSex(ds.Tables["PatSexDt"]);// 性别
            ipatientInfo.Bind_EnterSituation(ds.Tables["EnterSituationDt"]);// 入院情况
            ipatientInfo.Bind_RegionCode(ds.Tables["RegionCodeDt"]);// 地区编码
            ipatientInfo.Bind_txtSourceWay(ds.Tables["SourceWayDt"]);// 病人来源列表
            ipatientInfo.Bind_txtEnterWardID(ds.Tables["EnterWardDt"]);// 病区列表
            ipatientInfo.Bind_cboDeptList(ds.Tables["DeptList"]);// 科室列表
            ipatientInfo.Bind_cboPatType(ds.Tables["PatTypeDt"]);// 病人类型列表
            ipatientInfo.Bind_txtPatDisease(ds.Tables["PatDiseaseDt"]);// 诊断列表
            ipatientInfo.Bind_txtCurrDoctor(ds.Tables["CurrDoctorDt"]);// 医生列表
            ipatientInfo.Bind_txtCurrNurse(ds.Tables["CurrNurseDt"]);// 护士列表
            iAdmissionRegistration.SetControlEnabled();
        }
        #endregion

        #region "病人列表"
        /// <summary>
        /// 打开新病人入院界面
        /// </summary>
        /// <param name="isNewPatient">true：新病人登记/false：修改病人信息</param>
        [WinformMethod]
        public void ShowFrmPatientInfo(bool isNewPatient)
        {
            ipatientInfo.IsNewPatient = isNewPatient;

            // 新入院病人
            if (isNewPatient)
            {
                IP_PatientInfo ip_Patient = new IP_PatientInfo();
                ip_Patient.Nationality = "156";  // 国籍默认"中国"
                ip_Patient.Nation = "01";    //民族默认"汉族"
                ip_Patient.CulturalLevel = "91"; //教育程度默认"不详"
                ip_Patient.Birthplace = "110102000"; // 地址默认"北京"
                ip_Patient.DRegisterAddr = "110102000"; // 地址默认"北京"
                ip_Patient.NAddress = "110102000"; // 地址默认"北京"
                ip_Patient.RAddress = "110102000"; // 地址默认"北京"
                ip_Patient.Relation = "01";
                ip_Patient.Occupation = "Y10";
                ipatientInfo.PatientInfo = ip_Patient;
                IP_PatList ipList = new IP_PatList();
                ipList.MakerDate = DateTime.Now;
                ipList.Birthday = DateTime.Now;
                ipList.EnterHDate = DateTime.Now;
                ipList.PatTypeID = 106;
                ipList.EnterSituation = "3";
                ipatientInfo.PatList = ipList;
                ((Form)iBaseView["FrmPatientInfo"]).ShowDialog();
            }
            else
            {
                // 修改病人信息
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(iAdmissionRegistration.PatientID);
                    request.AddData(iAdmissionRegistration.PatListID);
                });

                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetPatientInfo", requestAction);
                DataTable patientInfoDt = retdata.GetData<DataTable>(0);
                DataTable patListDt = retdata.GetData<DataTable>(1);

                if (patientInfoDt != null && patientInfoDt.Rows.Count > 0)
                {
                    ipatientInfo.PatientInfo = ConvertExtend.ToObject<IP_PatientInfo>(patientInfoDt, 0);
                }

                if (patListDt != null && patListDt.Rows.Count > 0)
                {
                    ipatientInfo.PatList = ConvertExtend.ToObject<IP_PatList>(patListDt, 0);
                }

                ((Form)iBaseView["FrmPatientInfo"]).ShowDialog();
            }
        }

        /// <summary>
        /// 取消入院
        /// </summary>
        /// <param name="patListID">登记信息ID</param>
        /// <param name="patName">病人名</param>
        [WinformMethod]
        public void CancelAdmission(int patListID, string patName)
        {
            if (MessageBoxShowYesNo("确定要取消入院吗！") == DialogResult.Yes)
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(patListID);
                    request.AddData(patName);
                });

                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "CancelAdmission", requestAction);
                bool result = retdata.GetData<bool>(0);

                if (result)
                {
                    MessageBoxShowSimple("取消入院成功！");
                }
                else
                {
                    MessageBoxShowSimple("该病人已产生了费用或已分配了床位，不允许取消入院！");
                }
            }
        }

        /// <summary>
        /// 查询病人列表
        /// </summary>
        /// <param name="isAdd">是否为新入院病人</param>
        [WinformMethod]
        public void GetPatientList(bool isAdd)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iAdmissionRegistration.StartTime);
                request.AddData(iAdmissionRegistration.EndTime);
                request.AddData(iAdmissionRegistration.Dept);
                request.AddData(iAdmissionRegistration.PatType);
                request.AddData(iAdmissionRegistration.SelectParm);
                request.AddData(iAdmissionRegistration.PatStatus);
                request.AddData(false);
            });

            // 通过WCF调用服务端控制器取得住院病人列表
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetPatientList", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            iAdmissionRegistration.Bind_grdPatList(dt, isAdd);
        }
        #endregion

        #region "入院登记"

        /// <summary>
        /// 保存新入院病人信息
        /// </summary>
        [WinformMethod]
        public void SavePatientInfo()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                ipatientInfo.PatList.MakerEmpID = LoginUserInfo.EmpId;  // 登记人
                ipatientInfo.PatList.PYCode = SpellAndWbCode.GetSpellCode(ipatientInfo.PatList.PatName, 0, 10);// 拼音码
                ipatientInfo.PatList.WBCode = SpellAndWbCode.GetWBCode(ipatientInfo.PatList.PatName, 0, 10); // 五笔码
                ipatientInfo.PatList.CurrDeptID = ipatientInfo.PatList.EnterDeptID;  // 当前科室

                if (ipatientInfo.IsNewPatient)
                {
                    ipatientInfo.PatList.Status = 1; // 病人状态
                }

                ipatientInfo.PatList.CurrWardID = ipatientInfo.PatList.EnterWardID; // 当前病区代码
                ipatientInfo.PatList.CurrDoctorID = ipatientInfo.PatList.EnterDoctorID; // 当前医生代码
                ipatientInfo.PatList.CurrNurseID = ipatientInfo.PatList.EnterNurseID; // 当前护士代码
                ipatientInfo.PatList.LeaveHDate = DateTime.Now; // 出院日期
                request.AddData(ipatientInfo.PatientInfo);
                request.AddData(ipatientInfo.PatList);
                request.AddData(ipatientInfo.IsNewPatient);
                request.AddData(twoAdmission);
                request.AddData(ipatientInfo.InpatientReg);
                request.AddData(LoginUserInfo.WorkId);
                request.AddData(LoginUserInfo.EmpId);
                request.AddData(LoginUserInfo.DeptId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "SavePatientInfo", requestAction);
            string result = retdata.GetData<string>(0);

            if (!string.IsNullOrEmpty(result))
            {
                // 当前会员已办理过住院登记
                MessageBoxShowSimple(result);
                ipatientInfo.SetCardControlEnabled();
                return;
            }
            else
            {
                MessageBox.Show("保存病人信息成功！");
            }

            // 关闭新病人入院信息录入界面
            ipatientInfo.FormClose();

            if (ipatientInfo.TotalFee > 0)
            {
                // 收取预交金
                int patListID = retdata.GetData<int>(1);
                IP_DepositList depositList = new IP_DepositList();
                depositList.MemberID = ipatientInfo.PatList.MemberID; //会员ID
                depositList.PatListID = patListID; //登记ID
                depositList.DeptID = ipatientInfo.PatList.CurrDeptID;   //科室ID

                if (ipatientInfo.IsNewPatient)
                {
                    depositList.SerialNumber = retdata.GetData<decimal>(2); //住院流水号
                }
                else
                {
                    depositList.SerialNumber = ipatientInfo.PatList.SerialNumber; //住院流水号
                }

                depositList.InvoiceNO = ipatientInfo.InvoiceNO;
                depositList.PayType = ipatientInfo.PayType;
                depositList.TotalFee = ipatientInfo.TotalFee;
                PayADeposit(depositList);
            }

            // 重新加载病人列表
            GetPatientList(ipatientInfo.IsNewPatient);
        }

        /// <summary>
        /// 取得所有科室列表
        /// </summary>
        /// <param name="isAll">是否显示全部选项</param>
        [WinformMethod]
        public void GetDeptList(bool isAll)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(isAll);
            });

            // 通过WCF调用服务端控制器取得住院临床科室列表
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetDeptBasicData", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);

            // 病人列表界面
            if (isAll)
            {
                iAdmissionRegistration.Bind_cboDeptList(dt);
            }
            else
            {
                dt.TableName = "DeptList";
                ds.Tables.Add(dt);
            }
        }

        /// <summary>
        /// 取得病人类型列表
        /// </summary>
        /// <param name="isAll">是否显示全部选项</param>
        [WinformMethod]
        public void GetPatType(bool isAll)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(isAll);
            });

            // 通过WCF调用服务端控制器取得住院临床科室列表
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetPatType", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);

            // 病人列表界面
            if (isAll)
            {
                iAdmissionRegistration.Bind_cboPatType(dt);
            }
            else
            {
                dt.TableName = "PatTypeDt";
                ds.Tables.Add(dt);
            }
        }

        /// <summary>
        /// 通过界面查询条件获取会员基本信息
        /// </summary>
        /// <param name="queryContent">查询条件</param>
        [WinformMethod]
        public void GetMemberInfoByOther(string queryContent)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(queryContent);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetMemberInfoByOther", requestAction);
            DataTable dtPatInfo = retdata.GetData<DataTable>(0);
            iqueryMenber.Bind_PatList(dtPatInfo);
        }

        /// <summary>
        /// 通过姓名，电话号码，身份证号组合条件或取会员信息
        /// </summary>
        /// <param name="strPatInfo">姓名，电话号码，身份证号、医保卡号</param>
        [WinformMethod]
        public void ShowFrmQueryMenber(string strPatInfo)
        {
            // 根据病人信息查询病人列表
            twoAdmission = false;
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(strPatInfo);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetMemberInfoByOther", requestAction);
            DataTable patDt = retdata.GetData<DataTable>(0);
            // 存在病人，弹出病人选择界面
            if (patDt != null && patDt.Rows.Count > 0)
            {
                iqueryMenber.Bind_PatList(patDt);
                ((Form)iBaseView["FrmQueryMenber"]).ShowDialog();
            }
            else
            {
                // 没有病人提示操作员点击新病人
                MessageBoxShowSimple("病人不存在，请点击新病人录入病人信息！");
            }
        }

        /// <summary>
        /// 获取选择到的病人信息赋值到挂号界面
        /// </summary>
        [WinformMethod]
        public void GetSelectQueryMember()
        {
            DataTable dtPatInfo = iqueryMenber.GetPatInfoDatable();
            int curRowindex = iqueryMenber.GetCurRowIndex;
            if (dtPatInfo != null && dtPatInfo.Rows.Count > 0)
            {
                if (curRowindex >= 0)
                {
                    QueryMemberInfo(Tools.ToInt32(dtPatInfo.Rows[curRowindex]["MemberID"]));
                }
            }
        }

        /// <summary>
        /// 通过卡号获取卡信息
        /// </summary>
        [WinformMethod]
        public void QueryMemberInfo(int memberID)
        {
            twoAdmission = false;
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(memberID);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "QueryMemberInfo", requestAction);
            bool result = retdata.GetData<bool>(0);
            ipatientInfo.InpatientReg = result;
            ipatientInfo.PatientInfo = retdata.GetData<IP_PatientInfo>(1);
            ipatientInfo.PatList = retdata.GetData<IP_PatList>(2);
            ipatientInfo.Deposit = retdata.GetData<decimal>(3); // 预交金金额
            if (ipatientInfo.PatList.Times > 1)
            {
                twoAdmission = true;
            }

            ipatientInfo.SetPatFrmControlEnabled();

            // 存在预交金
            if (ipatientInfo.Deposit > 0)
            {
                // 绑定预交金金额
                ipatientInfo.Bind_Deposit();
            }
        }

        /// <summary>
        /// 读卡或办卡后绑定病人基本信息
        /// </summary>
        /// <param name="dtMemberInfo">病人基本信息</param>
        /// <param name="caseNumber">病案号</param>
        /// <param name="times">入院次数</param>
        private void SetPatientData(DataTable dtMemberInfo, string caseNumber, int times)
        {
            IP_PatientInfo ip_Patient = new IP_PatientInfo();
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
            ipatientInfo.PatientInfo = ip_Patient;
            IP_PatList ipList = new IP_PatList();
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
            ipatientInfo.PatList = ipList;
        }

        /// <summary>
        /// 获取卡类型列表
        /// </summary>
        [WinformMethod]
        public void RegDataInit()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "RegDataInit");
            DataTable dtCardType = retdata.GetData<DataTable>(0); //卡类型
            ipatientInfo.LoadCardType(dtCardType);
        }

        #endregion

        #region "预交金"

        /// <summary>
        /// 获取预交金票据号
        /// </summary>
        [WinformMethod]
        public void GetInvoiceCurNO()
        {
            try
            {
                ipatientInfo.SetInvoiceControlEnabled(true, true);
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(LoginUserInfo.EmpId);
                });

                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetInvoiceCurNO", requestAction);
                string billNumber = retdata.GetData<string>(0);
                ipatientInfo.SetBillNumber(billNumber);
            }
            catch (Exception ex)
            {
                ipatientInfo.SetInvoiceControlEnabled(false, true);
                MessageBoxShowError(ex.Message);
            }
        }

        /// <summary>
        /// 获取预交金支付方式
        /// </summary>
        [WinformMethod]
        public void GetPaymentMethod()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetPaymentMethod");
            DataTable patMethodDt = retdata.GetData<DataTable>(0);
            ipatientInfo.Binding_PaymentMethod(patMethodDt);
        }

        /// <summary>
        /// 缴纳预交金
        /// </summary>
        /// <param name="depositList">待保存预交金数据</param>
        private void PayADeposit(IP_DepositList depositList)
        {
            depositList.MakerEmpID = LoginUserInfo.EmpId;  //收费人
            depositList.MakerDate = DateTime.Now;  // 收费时间
            depositList.Status = 0;
            depositList.PrintTimes = 1;
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(depositList);
            });

            if (depositList.TotalFee <= 0)
            {
                MessageBoxShowSimple("请输入正确的金额");
                return;
            }

            if (MessageBoxShowYesNo(string.Format("确定要该病人收费[{0}]吗？", depositList.TotalFee)) == DialogResult.Yes)
            {
                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "PayADeposit", requestAction);
                string msg = retdata.GetData<string>(0);
                int depositID = retdata.GetData<int>(1);

                if (depositID <= 0)
                {
                    MessageBoxShowSimple(msg);
                }
                else
                {
                    Action<ClientRequestData> derequestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(depositID);
                    });

                    ServiceResponseData deretdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetPayADeposit", derequestAction);
                    DataTable dt = deretdata.GetData<DataTable>(0);
                    Dictionary<string, object> dic = new Dictionary<string, object>();

                    if (dt.Rows.Count > 0 && dt.Rows[0]["Status"].ToString() == "正常")
                    {
                        dt.Rows[0]["Head"] = LoginUserInfo.WorkName + "预交金缴款单";
                        string serialNumber = dt.Rows[0]["SerialNumber"].ToString();
                        string patName = dt.Rows[0]["PatName"].ToString();
                        dt.Rows[0]["SerialNumberName"] = patName + "(住院号" + serialNumber + ")";

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for (int j = 0; j < dt.Rows[i].ItemArray.Length; j++)
                            {
                                dic.Add(dt.Columns[j].ColumnName, dt.Rows[i][j]);
                            }

                            dic.Add("Year", Convert.ToDateTime(dt.Rows[i]["MakerDate"]).Year);
                            dic.Add("Month", Convert.ToDateTime(dt.Rows[i]["MakerDate"]).Month);
                            dic.Add("Day", Convert.ToDateTime(dt.Rows[i]["MakerDate"]).Day);
                            dic.Add("TotalFees", dt.Rows[i]["TotalFee"].ToString());
                        }

                        ReportTool.GetReport(LoginUserInfo.WorkId, 3204, 0, dic, null).PrintPreview(true);
                    }
                    else
                    {
                        DevComponents.DotNetBar.MessageBoxEx.Show("已退费不能打印");
                    }
                }
            }
        }
        #endregion

        #region "共用方法"

        /// <summary>
        /// 提示消息
        /// </summary>
        /// <param name="msg">Msg内容</param>
        [WinformMethod]
        public void MessageShow(string msg)
        {
            MessageBoxShowSimple(msg);
        }

        #endregion

        /// <summary>
        /// 调用会员新增界面新增会员卡
        /// </summary>
        [WinformMethod]
        public void AddMemberInfo()
        {
            try
            {
                int memberid = (int)InvokeController("OPProject.UI", "NewMemberController", "ShowMemberInfo", 5, 0, string.Empty, 0, 0, 0, 0);

                if (memberid <= 0)
                {
                    return;
                }

                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(OP_Enum.MemberQueryType.会员ID);
                    request.AddData(memberid);
                });

                ServiceResponseData retdataMember = InvokeWcfService("OPProject.Service", "RegisterController", "QueryMemberInfo", requestAction);
                DataTable dtMemberInfo = retdataMember.GetData<DataTable>(0);
                SetPatientData(dtMemberInfo, string.Empty, 1);
                ipatientInfo.SetPatFrmControlEnabled();
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
            }
        }
    }
}
