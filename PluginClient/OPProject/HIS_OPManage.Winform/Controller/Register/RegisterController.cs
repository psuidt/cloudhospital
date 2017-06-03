using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.MIManage;
using HIS_Entity.OPManage;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.Controller
{
    [WinformController(DefaultViewName = "FrmRegister")]//与系统菜单对应
    [WinformView(Name = "FrmRegister", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmRegister")]
    //退号
    [WinformView(Name = "FrmRegInvoiceInput", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmRegInvoiceInput")]
    //挂号收银
    [WinformView(Name = "FrmRegPayMentInfo", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmRegPayMentInfo")]

    //查找病人
    [WinformView(Name = "FrmQueryMember", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmqueryMember")]
   
    /// <summary>
    /// 挂号界面控制器
    /// </summary>
    public class RegisterController:WcfClientController
    {
        /// <summary>
        /// 病人信息查询界面
        /// </summary>
        IFrmQueryMember ifrmquery;

        /// <summary>
        /// 挂号界面
        /// </summary>
        IFrmRegister ifrmRegister;

        /// <summary>
        /// 挂号支付界面
        /// </summary>
        IFrmRegPayMentInfo ifrmRegPayMentInfo;

        /// <summary>
        /// 退号界面
        /// </summary>
        IFrmRegInvoiceInput ifrmRegInvoiceInput;

        /// <summary>
        /// 界面初始化
        /// </summary>
        public override void Init()
        {
            ifrmRegister = (IFrmRegister)iBaseView["FrmRegister"];
            ifrmquery = (IFrmQueryMember)iBaseView["FrmQueryMember"];
            ifrmRegPayMentInfo = (IFrmRegPayMentInfo)iBaseView["FrmRegPayMentInfo"];
            ifrmRegInvoiceInput = (IFrmRegInvoiceInput)iBaseView["FrmRegInvoiceInput"];
        } 

        #region 挂号主界面显示信息
        /// <summary>
        /// 挂号界面数据初始化
        /// </summary>
        /// <param name="type">0第一次进入界面 1不是第一次</param>
        [WinformMethod]
        public void RegDataInit(int type)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.EmpId);             
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "RegisterController", "RegDataInit",requestAction);
           
            DataTable dtRegType = retdata.GetData<DataTable>(0); //挂号类别    
            DataTable dtCardType = retdata.GetData<DataTable>(1); //卡类型
            DataTable dtPatType = retdata.GetData<DataTable>(2); //病人类别  

            string regMoningBTime = retdata.GetData<string>(3);//上午开始时间
            string regAfternoonBtime = retdata.GetData<string>(4);//下午开始时间
            string regNightBtime = retdata.GetData<string>(5);//晚上开始时间
            if (type == 0)
            {
                //进入界面的初始化发票票据数提示
                int invoiceNum = retdata.GetData<int>(6);//可用票据张数
                if (invoiceNum == 0)
                {
                    MessageBoxShowError("当前可用票据数为0，请先分配票据号");
                    return;
                }

                string curInvoiceNo = retdata.GetData<string>(7);//当前票据号
                MessageBoxShowSimple("当前可用票据数为" + invoiceNum + "张，当前票据号为" + curInvoiceNo +string.Empty);
            }
             
            ifrmRegister.RegMoningBTime = regMoningBTime;
            ifrmRegister.RegAfternoonBtime = regAfternoonBtime;
            ifrmRegister.RegNightBtime = regNightBtime;
            ifrmRegister.LoadRegType(dtRegType);
            ifrmRegister.LoadCardType(dtCardType);
            ifrmRegister.LoadPatType(dtPatType);
        }
     
        /// <summary>
        /// 通过挂号时段获取排班信息
        /// </summary>
        [WinformMethod]
        public void SelectSchedual()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(ifrmRegister.timeRangeIndex);             
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "RegisterController", "SelectSchedual", requestAction);
            DataTable dtDept = retdata.GetData<DataTable>(0);//科室
            DataTable dtDoctor = retdata.GetData<DataTable>(1);//医生             
            ifrmRegister.LoadRegDepts(dtDept);
            ifrmRegister.dtAllDoctor = dtDoctor;
           ifrmRegister.LoadRegDoctors(dtDoctor);            
        }

        /// <summary>
        /// 通过会员卡号获取卡信息
        /// </summary>
        [WinformMethod]
        public void QueryMemberInfo()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(OP_Enum.MemberQueryType.账户号码);
                request.AddData(ifrmRegister.cardNo);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "RegisterController", "QueryMemberInfo", requestAction);
            DataTable dtMemberInfo = retdata.GetData<DataTable>(0);
            if (dtMemberInfo == null || dtMemberInfo.Rows.Count == 0)
            {
                MessageBoxShowError("找不到该卡号病人信息");
                return;
            }

            SetPatListBasic(dtMemberInfo, 0);
        }

        /// <summary>
        /// 将获取到的病人信息列表赋值到界面
        /// </summary>
        /// <param name="dtMemberInfo">病人信息datatable</param>
        /// <param name="rowindex">行</param>
        private void SetPatListBasic(DataTable dtMemberInfo, int rowindex)
        {
            try
            {
                //LoginUserInfo.WorkId
                OP_PatList patlist = new OP_PatList();
                ifrmRegister.SetMedicardReadInfo =string.Empty;//清空医保显示信息
                ifrmRegister.SetMedicardReadInfo = ifrmRegister.MedicardReadInfo;
                patlist.MemberID = Convert.ToInt32(dtMemberInfo.Rows[rowindex]["MemberID"]);
                patlist.PatName = dtMemberInfo.Rows[rowindex]["MemberName"].ToString();
                patlist.PatSex = dtMemberInfo.Rows[rowindex]["SexName"].ToString();
                patlist.CardNO = dtMemberInfo.Rows[rowindex]["CardNO"].ToString();
                patlist.Birthday = Convert.ToDateTime(dtMemberInfo.Rows[rowindex]["Birthday"]);
                patlist.IDNumber = dtMemberInfo.Rows[rowindex]["IDNumber"].ToString();
                patlist.Allergies = dtMemberInfo.Rows[rowindex]["Allergies"].ToString();
                patlist.WorkUnit = dtMemberInfo.Rows[rowindex]["WorkUnit"].ToString();
                patlist.PatTypeID = Convert.ToInt32(dtMemberInfo.Rows[rowindex]["PatTypeID"]);
                patlist.MedicareCard= dtMemberInfo.Rows[rowindex]["MedicareCard"].ToString();
                ifrmRegister.Mobile = dtMemberInfo.Rows[rowindex]["Mobile"].ToString();
                ifrmRegister.Address = dtMemberInfo.Rows[rowindex]["Address"].ToString();

                string age = GetAge(patlist.Birthday, DateTime.Now);
                int length = age.Length;          
                string strPer = "Y";
                if (ifrmRegister.AgeUnit == "岁")
                {
                    strPer = "Y";
                }
                else if (ifrmRegister.AgeUnit == "月")
                {
                    strPer = "M";
                }
                else if (ifrmRegister.AgeUnit == "天")
                {
                    strPer = "D";
                }

                patlist.Age = strPer + ifrmRegister.Age;
                patlist.MemberAccountID = Convert.ToInt32(dtMemberInfo.Rows[rowindex]["AccountID"]);
                ifrmRegister.CurPatlist = patlist;
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
            }
        }

        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="dtBirthday">出生日期</param>
        /// <param name="dtNow">当前时间</param>
        /// <returns>返回年龄</returns>
        private string GetAge(DateTime dtBirthday, DateTime dtNow)
        {
            EfwControls.Common.AgeValue agevalue = EfwControls.Common.AgeExtend.GetAgeValue(dtBirthday);
            string strAge = string.Empty;            
                           
            // 格式化年龄输出
            if (agevalue.Y_num >= 1)                                           
            {
                // 年份输出
                strAge = agevalue.Y_num.ToString() + "岁";
                ifrmRegister.Age = agevalue.Y_num.ToString();
                ifrmRegister.AgeUnit = "岁";
            }

            if (agevalue.M_num > 0 && agevalue.Y_num < 1)                           
            {
                // 五岁以下可以输出月数
                strAge += agevalue.M_num.ToString() + "月";
                ifrmRegister.Age = agevalue.M_num.ToString();
                ifrmRegister.AgeUnit = "月";
            }

            if (agevalue.D_num >= 0 && agevalue.Y_num < 1)                              
            {
                // 一岁以下可以输出天数
                if (strAge.Length == 0 && agevalue.D_num >= 0)
                {
                    strAge += agevalue.D_num.ToString() + "日";
                    ifrmRegister.Age = agevalue.D_num.ToString();
                    ifrmRegister.AgeUnit = "日";
                }
            } 
                     
            return strAge;
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
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "RegisterController", "GetMemberInfoByOther", requestAction);
            DataTable dtPatInfo = retdata.GetData<DataTable>(0);
            ifrmquery.LoadPatInfo(dtPatInfo);
        }

        /// <summary>
        /// 获取选择到的病人信息赋值到挂号界面
        /// </summary>
        [WinformMethod]
        public void GetSelectQueryMember()
        {
            DataTable dtPatInfo = ifrmquery.GetPatInfoDatable();
            int curRowindex = ifrmquery.GetCurRowIndex;
            if (dtPatInfo != null && dtPatInfo.Rows.Count > 0)
            {
                if (curRowindex >= 0)
                {
                    SetPatListBasic(dtPatInfo, curRowindex);
                }
            }
        }   

        /// <summary>
        /// 获取操作员当天的挂号信息
        /// </summary>
        [WinformMethod]
        public void GetRegInfoByOperator()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.EmpId);            
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "RegisterController", "GetRegInfoByOperator", requestAction);
            DataTable dtRegInfo = retdata.GetData<DataTable>(0);
            ifrmRegister.BindRegInfoByOperator(dtRegInfo);
            ifrmRegister.SetGridColor();       
        }

        /// <summary>
        /// 挂号界面读医保卡
        /// </summary>
        [WinformMethod]
        public void ReadMedicareCard()
        {
            try
            {          
                //调用医保读卡接口，获取医保卡号和医保卡相关信息
                PatientInfo miPatInfo = MIOPInterface.ReadMediaCard();
                string medicareCardNO = miPatInfo.CardNo;
                ifrmRegPayMentInfo.SocialCard = miPatInfo.CardNo;
                ifrmRegPayMentInfo.IdentityNum = miPatInfo.IcNo;              
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    // 医保卡 类型是3，根据公共方法里面的参数而来
                    request.AddData(OP_Enum.MemberQueryType.医保卡号);
                    request.AddData(medicareCardNO);
                });
                ServiceResponseData retdataMember = InvokeWcfService("OPProject.Service", "RegisterController", "QueryMemberInfo", requestAction);
                DataTable dtMemberInfo = retdataMember.GetData<DataTable>(0);
                if (dtMemberInfo == null || dtMemberInfo.Rows.Count == 0)
                {
                    //MessageBoxShowError("系统中没有该医保卡号病人信息");
                    //return;
                    //没有此医保卡病人信息时，默认弹出新病人窗口
                    AddMemberInfo();
                }
                else
                {
                    if (dtMemberInfo.Rows.Count > 0)
                    {
                        if (!MIOPInterface.PatCheck(dtMemberInfo.Rows[0]["MemberName"].ToString(), dtMemberInfo.Rows[0]["MedicareCard"].ToString(), dtMemberInfo.Rows[0]["IDNumber"].ToString(), miPatInfo))
                        {
                            return;
                        }
                    }

                    ifrmRegister.MedicardReadInfo = miPatInfo.ShowMiPatInfo;//显示医保信息
                    if (dtMemberInfo.Rows.Count > 1)
                    {
                        ((IFrmQueryMember)iBaseView["FrmQueryMember"]).LoadPatInfo(dtMemberInfo);//把病人数据源直接赋值到界面
                        (iBaseView["FrmQueryMember"] as Form).Text = "选择账户";
                        (iBaseView["FrmQueryMember"] as Form).ShowDialog();
                    }
                    else
                    {
                        SetPatListBasic(dtMemberInfo, 0);
                    }
                }
            }
            catch (Exception err)
            {
                MessageBoxShow(err.Message, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion

        #region 挂号收银

        /// <summary>
        /// 挂号收费
        /// </summary>
        /// <returns>DialogResult</returns>
        [WinformMethod]
        public DialogResult RegisterPayMent()
        {
            if (RegPayDataInit())
            {
                var dialog = iBaseView["FrmRegPayMentInfo"] as Form;
                if (dialog == null)
                {
                    return DialogResult.None;
                }

                return dialog.ShowDialog();
            }

            return DialogResult.None;
        }

        /// <summary>
        /// 挂号支付界面初始化
        /// </summary> 
        /// <returns>返回初始化是否成功</returns>
        private bool RegPayDataInit()
        {
            try
            {
                OP_PatList curPatlist = ifrmRegister.CurPatlist;
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(LoginUserInfo.EmpId);//人员ID
                    request.AddData(curPatlist.RegTypeID);//挂号类别ID
                    request.AddData(curPatlist.PatTypeID);//病人类型ID
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "RegisterController", "RegPayMentInit", requestAction);
                string invoiceNO = retdata.GetData<string>(0);//票据号
                decimal regFee = retdata.GetData<decimal>(1);//挂号费
                DataTable dtPayMent = retdata.GetData<DataTable>(2);
                bool isMedicardPat = retdata.GetData<bool>(3);
                ifrmRegPayMentInfo.RegDeptName = curPatlist.RegDeptName;
                ifrmRegPayMentInfo.MedicarePay = 0;
                ifrmRegPayMentInfo.MedicareMIPay = 0;
                ifrmRegPayMentInfo.MedicarePersPay = 0;
                ifrmRegPayMentInfo.PromFee = 0;
                ifrmRegPayMentInfo.GetRegPayment(dtPayMent);
                ifrmRegPayMentInfo.RegTotalFee = Convert.ToDecimal(regFee.ToString("0.00"));               
                ifrmRegPayMentInfo.InvoiceNO = invoiceNO;
                ifrmRegPayMentInfo.ActPay = ifrmRegPayMentInfo.RegTotalFee;

                //如果病人类型是医保类型，则直接调用医保挂号预算接口，算出医保预算支付金额
                if (isMedicardPat == true)
                {
                    if (string.IsNullOrEmpty(ifrmRegister.MedicardReadInfo))
                    {
                        PatientInfo miPatInfo = MIOPInterface.ReadMediaCard();
                        if (!MIOPInterface.PatCheck(curPatlist.PatName, curPatlist.MedicareCard, curPatlist.IDNumber, miPatInfo))
                        {
                            return false;
                        }

                        string medicareCardNO = miPatInfo.CardNo;
                        ifrmRegPayMentInfo.SocialCard = miPatInfo.CardNo;
                        ifrmRegPayMentInfo.IdentityNum = miPatInfo.IcNo;
                        curPatlist.MedicareCard = miPatInfo.CardNo;
                        ifrmRegister.MedicardReadInfo = miPatInfo.ShowMiPatInfo;
                        //PatientInfo miPatInfo = MIOPInterface.ReadMediaCard();
                        //string MedicareCardNO = miPatInfo.CardNo;// "12345678900987654";
                        //string MedicardInfo = "卡号:" + miPatInfo.CardNo + " 姓名:" + miPatInfo.PersonName + " 医保类别:" + miPatInfo.PersonType + " 身份证号码:" + miPatInfo.IdNo + " 余额:" + miPatInfo.PersonCount; //"12345678900987654";
                        //ifrmRegister.MedicardReadInfo = MedicardInfo;
                        //curPatlist.MedicareCard = miPatInfo.CardNo;
                    }

                    Action<ClientRequestData> requestAction1 = ((ClientRequestData request) =>
                    {
                        request.AddData(curPatlist.RegTypeID);
                    });
                    ServiceResponseData retdata1 = InvokeWcfService("OPProject.Service", "RegisterController", "GetRegInfoByRegId", requestAction1);
                    DataTable dtRegInfo = retdata1.GetData<DataTable>(0);
                    //医保预算
                    Dictionary<string, string> dic = MIOPInterface.MiRegBuget(ifrmRegPayMentInfo.SocialCard, LoginUserInfo.EmpName, LoginUserInfo.EmpId, curPatlist, regFee, dtRegInfo, ifrmRegPayMentInfo.InvoiceNO, ifrmRegPayMentInfo.IdentityNum);
                    decimal dMedicarePay = Convert.ToDecimal(dic["MedicarePay"].ToString());// ifrmRegPayMentInfo.RegTotalFee;//假数据   
                    ifrmRegPayMentInfo.MedicarePay = dMedicarePay;
                    decimal dMedicareMIPay = Convert.ToDecimal(dic["MedicareMIPay"].ToString());// ifrmRegPayMentInfo.RegTotalFee;//假数据
                    ifrmRegPayMentInfo.MedicareMIPay = dMedicareMIPay;// ifrmRegPayMentInfo.RegTotalFee;//假数据     
                    decimal dMedicarePersPay = Convert.ToDecimal(dic["MedicarePersPay"].ToString());// ifrmRegPayMentInfo.RegTotalFee;//假数据 
                    ifrmRegPayMentInfo.MedicarePersPay = dMedicarePersPay;


                    ifrmRegPayMentInfo.SetMedicardInfo = dic["MedicardInfo"].ToString();// "43625436";
                    ifrmRegPayMentInfo.MIBudgetID = Convert.ToInt32(dic["ID"]);//医保预结算ID 
                }

                else
                {
                    ifrmRegPayMentInfo.ShoudPay = Convert.ToDecimal(regFee.ToString("0.00")) - Convert.ToDecimal(ifrmRegPayMentInfo.MedicarePay);//应付金额
                }
                ifrmRegPayMentInfo.ReadMedicareEnabled = isMedicardPat;
                ifrmRegPayMentInfo.ExpMedicardInfoVisible = isMedicardPat;
                return true;
            }
            catch (Exception err)
            {
                MessageBoxShow(err.Message, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return false;
            }
        }

        /// <summary>
        /// 医保试算
        /// </summary>
        [WinformMethod]
        public void GetRegMedicareCaculate()
        {
            try
            {
                //调用医保读卡和试算接口，算出医保金额
                PatientInfo miPatInfo= MIOPInterface.ReadMediaCard();
                if (!MIOPInterface.PatCheck(ifrmRegister.CurPatlist.PatName, ifrmRegister.CurPatlist.MedicareCard, ifrmRegister.CurPatlist.IDNumber, miPatInfo))
                {
                    return;
                }

                ifrmRegister.MedicardReadInfo = miPatInfo.ShowMiPatInfo;
                Action<ClientRequestData> requestAction1 = ((ClientRequestData request) =>
                {
                    request.AddData(ifrmRegister.CurPatlist.RegTypeID);
                });
                ServiceResponseData retdata1 = InvokeWcfService("OPProject.Service", "RegisterController", "GetRegInfoByRegId", requestAction1);
                DataTable dtRegInfo = retdata1.GetData<DataTable>(0);
                Dictionary<string, string> dic = MIOPInterface.MiRegBuget(ifrmRegPayMentInfo.SocialCard, LoginUserInfo.EmpName, LoginUserInfo.EmpId, ifrmRegister.CurPatlist, ifrmRegPayMentInfo.RegTotalFee, dtRegInfo,ifrmRegPayMentInfo.InvoiceNO, ifrmRegPayMentInfo.IdentityNum);              
                ifrmRegPayMentInfo.MedicarePay = Convert.ToDecimal(dic["MedicarePay"]);// ifrmRegPayMentInfo.RegTotalFee;//假数据      
                ifrmRegPayMentInfo.MedicareMIPay = Convert.ToDecimal(dic["MedicareMIPay"]);// ifrmRegPayMentInfo.RegTotalFee;//假数据     
                ifrmRegPayMentInfo.MedicarePersPay = Convert.ToDecimal(dic["MedicarePersPay"]);// ifrmRegPayMentInfo.RegTotalFee;//假数据                  
                ifrmRegPayMentInfo.SetMedicardInfo = dic["MedicardInfo"].ToString();// "43625436";
                ifrmRegPayMentInfo.MIBudgetID = Convert.ToInt32(dic["ID"]);//医保预结算ID
            }
            catch (Exception err)
            {
                MessageBoxShow(err.Message, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// 挂号提交
        /// </summary>
        /// <returns>保存挂号是否成功</returns>
        [WinformMethod]
        public bool SaveRegister()
        {
            //调用医保读卡接口，获取医保卡号和医保卡相关信息
            OP_PatList curPatlist = ifrmRegister.CurPatlist;
            curPatlist.OperatorID = LoginUserInfo.EmpId;
            if (!string.IsNullOrEmpty( ifrmRegister.MedicardReadInfo)) 
            {
                try
                {
                    //调用医保接口正式结算 如果医保结算失败，直接return
                    MIOPInterface.MiRegister(ifrmRegPayMentInfo.MIBudgetID,string.Empty);
                }
                catch (Exception err)
                {
                    MessageBoxShow(err.Message, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return false;
                }
            }

            try
            {
                decimal totalRegFee = ifrmRegPayMentInfo.RegTotalFee;
                string regPayMentCode = ifrmRegPayMentInfo.GetPatMentCode;
                //decimal medicarcPay = ifrmRegPayMentInfo.MedicarePay;
                decimal medicarcPay = ifrmRegPayMentInfo.MedicareMIPay;
                decimal medicarePersPay = ifrmRegPayMentInfo.MedicarePersPay;
                decimal shouldPay = ifrmRegPayMentInfo.ShoudPay;
                decimal promFee = ifrmRegPayMentInfo.PromFee;
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(curPatlist);//病人对象
                    request.AddData(totalRegFee);//挂号总金额
                    request.AddData(regPayMentCode);//支付方式Code
                    request.AddData(medicarcPay);//医保统筹支付金额
                    request.AddData(shouldPay);//应付金额 
                    request.AddData(promFee);//优惠金额 
                    request.AddData(medicarePersPay);//医保个账支付金额              
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "RegisterController", "SaveRegister", requestAction);
                DataTable dtRegPrint = retdata.GetData<DataTable>(0);
                OP_PatList backPatlist = retdata.GetData<OP_PatList>(1);
                if (dtRegPrint == null || dtRegPrint.Rows.Count == 0)
                {
                    MessageBoxShowError("找不到打印数据");
                    return false;
                }

                if (!string.IsNullOrEmpty(ifrmRegister.MedicardReadInfo)) 
                {
                    MIOPInterface.MiRegisterComplete(ifrmRegPayMentInfo.MIBudgetID, backPatlist.VisitNO, dtRegPrint.Rows[0]["invoiceNO"].ToString());
                }

                //票据打印
                RegInvoicePrint(dtRegPrint);
                return true;
            }
            catch (Exception err)
            {
                MessageBoxShow(err.Message, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (ifrmRegPayMentInfo.MIBudgetID > 0)
                {
                    //需要回退医保挂号
                    MIOPInterface.MiRefundRegister(string.Empty, ifrmRegPayMentInfo.MIBudgetID.ToString(),string.Empty);
                }

                return false;
            }
        }

        /// <summary>
        /// 挂号票据打印
        /// </summary>
        /// <param name="dtRegPrint">挂号数据</param>
        private void RegInvoicePrint(DataTable dtRegPrint)
        {
            return;
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            myDictionary.Add("patname", dtRegPrint.Rows[0]["patname"]);
            myDictionary.Add("invoiceNO", dtRegPrint.Rows[0]["invoiceNO"]);
            myDictionary.Add("visitNO", dtRegPrint.Rows[0]["visitNO"]);
            myDictionary.Add("regDeptName", dtRegPrint.Rows[0]["regDeptName"]);

            myDictionary.Add("regDocName", dtRegPrint.Rows[0]["regDocName"]);
            myDictionary.Add("regTypeName", dtRegPrint.Rows[0]["regTypeName"]);
            myDictionary.Add("xxhj", CmycurD(Convert.ToDecimal(dtRegPrint.Rows[0]["totalFee"])));
            myDictionary.Add("totalFee", dtRegPrint.Rows[0]["totalFee"]);

            myDictionary.Add("operator", LoginUserInfo.EmpName);
            myDictionary.Add("HospName", LoginUserInfo.WorkName);
            myDictionary.Add("cashFee", dtRegPrint.Rows[0]["cashFee"]);
            myDictionary.Add("medicareFee", Convert.ToDecimal(dtRegPrint.Rows[0]["totalFee"]) - Convert.ToDecimal(dtRegPrint.Rows[0]["cashFee"]));
            myDictionary.Add("regDate",Convert.ToDateTime( dtRegPrint.Rows[0]["regDate"]).ToString("yyyy-MM-dd HH:mm:ss"));
            EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 2025, 0, myDictionary, null).Print(false);
        }

        /// <summary>
        /// 转换人民币大小金额
        /// </summary>
        /// <param name="num">金额</param>
        /// <returns>返回大写形式</returns>
        private string CmycurD(decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字
            string str3 =string.Empty;    //从原num值中取出的值
            string str4 =string.Empty;    //数字的字符串形式
            string str5 =string.Empty;  //人民币大写金额形式
            int i;    //循环变量
            int j;    //num的值乘以100的字符串长度
            string ch1 =string.Empty;    //数字的汉语读法
            string ch2 =string.Empty;    //数字位的汉字读法
            int nzero = 0;  //用来计算连续的零值是几个
            int temp;            //从原num值中取出的值

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数
            str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式
            j = str4.Length;      //找出最高位
            if (j > 15)
            {
                return "溢出";
            }

            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分

            //循环取出每一位需要转换的值
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //取出需转换的某一位的值
                temp = Convert.ToInt32(str3);      //转换为数字
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时
                    if (str3 == "0")
                    {
                        ch1 =string.Empty;
                        ch2 =string.Empty;
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 =string.Empty;
                                ch2 =string.Empty;
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 =string.Empty;
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 =string.Empty;
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }

                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上
                    ch2 = str2.Substring(i, 1);
                }

                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整”
                    str5 = str5 + '整';
                }
            }

            if (num == 0)
            {
                str5 = "零元整";
            }

            return str5;
        }

        /// <summary>
        /// 挂号完成清空界面
        /// </summary>
        [WinformMethod]
        public void RegComplete()
        {
            GetRegInfoByOperator();
            ifrmRegister.CurPatlist = new OP_PatList();
            ifrmRegister.Address =string.Empty;
            ifrmRegister.Age =string.Empty;
            ifrmRegister.Mobile =string.Empty;
            ifrmRegister.MedicardReadInfo =string.Empty;
            ifrmRegister.SetMedicardReadInfo =string.Empty;
            ifrmRegister.ClearInfo();
        }
        #endregion

        #region 退号
        /// <summary>
        /// 退号
        /// </summary>      
        [WinformMethod]
        public void BackRegister()
        {
            var dialog = iBaseView["FrmRegInvoiceInput"] as Form;
            dialog.ShowDialog();
            if (ifrmRegInvoiceInput.IsBackRegOK)
            {
                MessageBoxShowSimple("退号成功");
            }
        }

        /// <summary>
        /// 退号界面初始化
        /// </summary>
        [WinformMethod]
        public void InitFrmRegInvoice()
        {          
            ifrmRegInvoiceInput.InputVoiceNO =string.Empty;
        }

        /// <summary>
        /// 判断输入票据号是过否可以退号
        /// </summary>
        /// <returns>返回是否可退号</returns>
        [WinformMethod]
        public bool GetRegInfoByInvoiceNO()
        {
            try
            {
                string invoiceNO = ifrmRegInvoiceInput.InputVoiceNO;
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(invoiceNO);//票据号               
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "RegisterController", "GetRegInfoByInvoiceNO", requestAction);
                bool result = retdata.GetData<bool>(0);
                if (result)
                {
                    PayMentInfoList payInfoList = retdata.GetData<PayMentInfoList>(1);
                    DataTable dtPayInfo = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable(payInfoList.paymentInfolist);
                    ifrmRegInvoiceInput.LoadPayInfo(dtPayInfo);
                    bool haveMedicarePay = retdata.GetData<bool>(2);
                    ifrmRegInvoiceInput.HaveMedicarePay = haveMedicarePay;
                    ifrmRegInvoiceInput.RegPatList = retdata.GetData<OP_PatList>(3);
                }

                return true;
            }
            catch (Exception err)
            {
                MessageBoxShow(err.Message, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return false;
            }
        }

        /// <summary>
        /// 退号提交
        /// </summary>
        /// <returns>返回退号是否成功</returns>
        [WinformMethod]
        public bool BackReg()
        {
            if (ifrmRegInvoiceInput.HaveMedicarePay)
            {
                try
                {
                    //先调用医保接口退号
                    MIOPInterface.MiRefundRegister(ifrmRegInvoiceInput.RegPatList.VisitNO,string.Empty,ifrmRegInvoiceInput.InputVoiceNO);
                }
                catch (Exception err)
                {
                    MessageBoxShow(err.Message, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return false;
                }
            }

            try
            {
                string invoiceNO = ifrmRegInvoiceInput.InputVoiceNO;
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(invoiceNO);//票据号   
                    request.AddData(LoginUserInfo.EmpId);//操作员ID          
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "RegisterController", "BackReg", requestAction);
              
                return true;
            }
            catch (Exception err)
            {
                MessageBoxShow(err.Message, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return false;
            }
        }

        /// <summary>
        /// 退号界面读取医保卡
        /// </summary>
        /// <param name="sPatName">病人姓名</param>
        [WinformMethod]
        public void GetBacRegReadCard(string sPatName)
        {
            ///调用医保读卡接口
            PatientInfo miPatInfo= MIOPInterface.ReadMediaCard();
            if (!miPatInfo.PersonName.Equals(sPatName))
            {
                MessageBoxShowError("此卡与挂号的卡不符！");
                return;
            }
            //string MedicareCardNO = miPatInfo.CardNo;
            //string MedicardInfo = "卡号:" + miPatInfo.CardNo + "姓名:" + miPatInfo.PersonName + "医保类别:" + miPatInfo.PersonType + "身份证号码:" + miPatInfo.IdNo + "余额:" + miPatInfo.PersonCount; //"12345678900987654";
            ifrmRegInvoiceInput.MedicareInfo = miPatInfo.ShowMiPatInfo;
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
                int memberid = (int)InvokeController(this._pluginName, "NewMemberController", "ShowMemberInfo", 5, 0,string.Empty, 0,0,0,0);
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
                SetPatListBasic(dtMemberInfo, 0);
                ifrmRegister.AddMemberSetFocus();
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
            }
        }
    }
}
