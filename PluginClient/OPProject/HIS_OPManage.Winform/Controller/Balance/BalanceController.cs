using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.ClinicManage;
using HIS_Entity.MIManage;
using HIS_Entity.OPManage;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.Controller
{
    //与系统菜单对应
    [WinformController(DefaultViewName = "FrmBalance")]
    [WinformView(Name = "FrmBalance", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmBalance")]
 
    //收银
    [WinformView(Name = "FrmPayMentInfo", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmPayMentInfo")]
   
    //发票调整
    [WinformView(Name = "FrmAdjustInvoice", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmAdjustInvoice")]

    //查找病人
    [WinformView(Name = "FrmOutPatient", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmOutPatient")]
   
    //退费提示
    [WinformView(Name = "FrmRefundInfo", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmRefundInfo")]

    //退费提示
    [WinformView(Name = "FrmRegList", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmRegList")]

    /// <summary>
    /// 收费控制器类
    /// </summary>   
    public class BalanceController : WcfClientController
    {
        /// <summary>
        /// 收费界面接口
        /// </summary>
        IFrmBalance ifrmBalance;

        /// <summary>
        /// 收费结算界面接口
        /// </summary>
        IFrmPayMentInfo ifrmPayMentInfo;

        /// <summary>
        /// 收费界面调整发票号界面接口
        /// </summary>
        IFrmAdjustInvoice ifrmAdjustInvoice;

        /// <summary>
        /// 病人信息查询界面接口
        /// </summary>
        IFrmOutPatient ifrmOutPatient;

        /// <summary>
        /// 退费界面接口
        /// </summary>
        IFrmRefundInfo ifrmRefundInfo;

        /// <summary>
        /// 挂号列表选择界面接口
        /// </summary>
        IFrmRegList ifrmRegList;

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            ifrmBalance = (IFrmBalance)iBaseView["FrmBalance"];
            ifrmPayMentInfo = (IFrmPayMentInfo)iBaseView["FrmPayMentInfo"];
            ifrmAdjustInvoice = (IFrmAdjustInvoice)iBaseView["FrmAdjustInvoice"];
            ifrmOutPatient = (IFrmOutPatient)iBaseView["FrmOutPatient"];
            ifrmRefundInfo = (IFrmRefundInfo)iBaseView["FrmRefundInfo"];
            ifrmRegList = (IFrmRegList)iBaseView["FrmRegList"];
        }

        #region 弹出窗体

        /// <summary>
        /// 调整票据号
        /// </summary>
        /// <returns>DialogResult</returns>
        [WinformMethod]
        public DialogResult AdjustInvoice()
        {
            var dialog = iBaseView["FrmAdjustInvoice"] as Form;
            if (dialog==null)
            {
                return DialogResult.None;
            }

            return dialog.ShowDialog();
        }

        /// <summary>
        /// 查询病人信息
        /// </summary>
        /// <returns>DialogResult</returns>
        [WinformMethod]
        public DialogResult GetOutPatient()
        {
            var dialog = iBaseView["FrmOutPatient"] as Form;
            if (dialog == null)
            {
                return DialogResult.None;
            }

            return dialog.ShowDialog();
        }
        #endregion

        #region 窗体初始化
        /// <summary>
        /// 卡类型
        /// </summary>
        DataTable dtCardType;

        /// <summary>
        /// 病人类别
        /// </summary>
        DataTable dtPatType;

        /// <summary>
        /// 科室
        /// </summary>
        DataTable dtDept;

        /// <summary>
        /// 医生
        /// </summary>
        DataTable dtDoctor;

        /// <summary>
        /// 药品项目选项卡数据
        /// </summary>
        DataTable dtDrugItem;

        /// <summary>
        /// 诊断表
        /// </summary>
        DataTable dtDisease;

        /// <summary>
        /// 收费界面基础数据获取
        /// </summary>
        [WinformMethod]
        public void BalanceDataInit()
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(LoginUserInfo.EmpId);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "BalanceController", "BalanceDataInit", requestAction);
                dtCardType = retdata.GetData<DataTable>(0); //卡类型
                DataRow dr = dtCardType.NewRow();
                object obj = dtCardType.Compute("max(CardType)", string.Empty);
                dr["cardtype"] = Convert.ToInt32(obj) + 100;
                dr["CardTypeName"] = "发票号";
                dtCardType.Rows.Add(dr.ItemArray);
                dtPatType = retdata.GetData<DataTable>(1); //病人类别  
                dtDept = retdata.GetData<DataTable>(2); //科室
                dtDoctor = retdata.GetData<DataTable>(3);//医生          
                int invoiceNum = retdata.GetData<int>(4);//可用票据张数            
                if (invoiceNum == 0)
                {
                    MessageBoxShowError("当前可用票据数为0，请先分配票据号");                  
                }

                string curInvoiceNo = retdata.GetData<string>(5);//当前票据号
                MessageBoxShowSimple("当前可用票据数为" + invoiceNum + "张，当前票据号为" + curInvoiceNo + string.Empty);
                int isAddOpdoctor = retdata.GetData<int>(6);//是否上门诊医生站
                ifrmBalance.CurInvoiceNO = curInvoiceNo;
                ifrmBalance.LoadCardType(dtCardType);
                ifrmBalance.LoadPatType(dtPatType);
                ifrmBalance.LoadPrescDepts(dtDept);
                ifrmBalance.LoadPrescDoctors(dtDoctor);
                ifrmBalance.SetexPlLastPayExpanded = false;
                ifrmBalance.AllPrescriptionTotalFee = 0;
                ifrmBalance.IsAddOpdoctor = isAddOpdoctor;
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);              
                return;
            }
        }

        /// <summary>
        /// 异步获取数据
        /// </summary>
        public override void AsynInit()
        {
            ifrmBalance.SetBarEnabled(false);
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "BalanceController", "GetDiseaseShowCard");
            dtDisease = retdata.GetData<DataTable>(0);//诊断
            retdata = InvokeWcfService("OPProject.Service", "BalanceController", "GetDrugItemShowCard");
            dtDrugItem = retdata.GetData<DataTable>(0);//药品项目选项卡数据      
        }
        #endregion

        /// <summary>
        /// 异步完成
        /// </summary>
        public override void AsynInitCompleted()
        {                   
            ifrmBalance.LoadDiagnose(dtDisease);
            ifrmBalance.BindDrugItemShowCard(dtDrugItem);
            ifrmBalance.SetBarEnabled(true);       
        }

        #region 获取挂号病人信息
        /// <summary>
        /// 获取挂号病人信息
        /// </summary>
        /// <param name="queryType">查找类型</param>
        /// <param name="content">查询条件</param>
        [WinformMethod]
        public void GetPatListByAccountNO(OP_Enum.MemberQueryType queryType, string content)
        {
            ifrmBalance.SetMedicardReadInfo = string.Empty;
            string medicareCardNO = string.Empty;
            PatientInfo miPatInfo = new PatientInfo(); 
            if (queryType == OP_Enum.MemberQueryType.医保卡号)
            {
                try
                {
                    //调用医保接口，读出医保卡号                   
                     miPatInfo = MIOPInterface.ReadMediaCard();
                     medicareCardNO = miPatInfo.CardNo;
                     content = medicareCardNO;
                }
                catch (Exception err)
                {
                    MessageBoxShow(err.Message, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }                
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(queryType);
                request.AddData(content);
            });

            //退费查询
            if (queryType == OP_Enum.MemberQueryType.退费发票号)
            {
                try
                {
                    ServiceResponseData backretdata = InvokeWcfService("OPProject.Service", "BalanceController", "GetBackFeeByInvoiceNO", requestAction);
                    OP_PatList curpatlist = backretdata.GetData<OP_PatList>(0);
                    ifrmBalance.CurPatList = curpatlist;
                    int agenum = 0;
                    string ageUnit = "岁";
                    ConvertAge(curpatlist.Age, out agenum, out ageUnit);
                    ifrmBalance.Age = agenum;
                    ifrmBalance.AgeUnit = ageUnit;
                    DataTable dtPres = backretdata.GetData<DataTable>(1);
                    int costpatTypeid = backretdata.GetData<int>(2);
                    ifrmBalance.CostPatTypeid = costpatTypeid;//取结算时的病人类型
                    ifrmBalance.SetexPlLastPayExpanded = false;
                    for (int i = 0; i < dtPres.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dtPres.Rows[i]["presNO"]) == 0)
                        {
                            dtPres.Rows[i]["presNO"] = DBNull.Value;
                        }

                        if (Convert.ToInt32(dtPres.Rows[i]["SubTotalFlag"]) == 1)
                        {
                            dtPres.Rows[i]["presNO"] = DBNull.Value;
                            dtPres.Rows[i]["RetailPrice"] = DBNull.Value;
                            dtPres.Rows[i]["PresAmount"] = DBNull.Value;
                            dtPres.Rows[i]["MiniAmount"] = DBNull.Value;
                            dtPres.Rows[i]["PackAmount"] = DBNull.Value;
                            dtPres.Rows[i]["RefundMiniAmount"] = DBNull.Value;
                            dtPres.Rows[i]["RefundPackAmount"] = DBNull.Value;
                            dtPres.Rows[i]["ItemID"] = DBNull.Value;
                        }
                    }

                    ifrmRefundInfo.RefundInvoiceNO = content;//退费票据号
                    ifrmBalance.SetPresDataSource ( dtPres);                
                    CalculateAllPrescriptionFee();
                    return;
                }
                catch (Exception err)
                {
                    MessageBoxShowError(err.Message);
                    ifrmBalance.SetPresDataSource(new DataTable ());                  
                    ifrmBalance.CurPatList = new OP_PatList();
                    return;
                }
            }
            else
            {
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "BalanceController", "GetRegPatListByCardNo", requestAction);
                List<OP_PatList> patlist = retdata.GetData<List<OP_PatList>>(0);
                if (patlist == null || patlist.Count == 0)
                {
                    MessageBoxShowError("找不到该病人信息");
                    ifrmBalance.CurPatList = new OP_PatList();
                    ifrmBalance.Age = 0;
                    ifrmBalance.AgeUnit = string.Empty;
                    return;                
                }

                if (patlist.Count == 1)
                {
                    ifrmBalance.CurPatList = patlist[0];

                    //判断和医保信息是否一致
                    if (!string.IsNullOrEmpty(medicareCardNO))
                    {
                        if (!MIOPInterface.PatCheck(patlist[0].PatName, patlist[0].MedicareCard, patlist[0].IDNumber, miPatInfo))
                        {
                            return;
                        }

                        ifrmBalance.MedicardReadInfo = miPatInfo.ShowMiPatInfo;
                    }

                    int agenum = 0;
                    string ageUnit = "岁";
                    ConvertAge(patlist[0].Age, out agenum, out ageUnit);
                    ifrmBalance.Age = agenum;
                    ifrmBalance.AgeUnit = ageUnit;
                    GetPrescriptions();                 
                }
                else
                {
                    ((IFrmRegList)iBaseView["FrmRegList"]).SetPatLists(patlist);
                    (iBaseView["FrmRegList"] as Form).ShowDialog();
                }
            }            
        }

        /// <summary>
        /// 年齡转换
        /// </summary>
        /// <param name="age">年龄</param>
        /// <param name="ageNum">返回年龄</param>
        /// <param name="ageUnit">返回年龄单位</param>
        private void ConvertAge(string age, out int ageNum, out string ageUnit)
        {
            ageNum = 0;
            ageUnit = string.Empty;
            ageNum = Convert.ToInt32(age.Substring(1, age.Length - 1));
            string strAge = age.Substring(0, 1);
            if (strAge == "Y")
            {
                ageUnit = "岁";
            }
            else if (strAge == "M")
            {
                ageUnit = "月";
            }
            else if (strAge == "D")
            {
                ageUnit = "天";
            }
        }

       /// <summary>
       /// 病人信息显示
       /// </summary>
        [WinformMethod]
        public void GetSelectPatlist()
        {
            ifrmBalance.CurPatList = ifrmRegList.GetcurPatlist;
            int agenum = 0;
            string ageUnit = "岁";
            ConvertAge(ifrmRegList.GetcurPatlist.Age, out agenum, out ageUnit);
            ifrmBalance.Age = agenum;
            ifrmBalance.AgeUnit = ageUnit;
            GetPrescriptions();
        }

        /// <summary>
        /// 通过条件查找病人信息
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="tel">电话号码</param>
        /// <param name="idNumber">身份证号</param>
        /// <param name="mediCard">医保卡号</param>
        /// <param name="bdate">就诊开始日期</param>
        /// <param name="edate">就诊结束日期</param>
        [WinformMethod]
        public void GetRegInfoByOther( string name,string tel,string idNumber,string mediCard,DateTime bdate, DateTime edate)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(name);
                request.AddData(tel);
                request.AddData(idNumber);
                request.AddData(mediCard);
                request.AddData(bdate);
                request.AddData(edate);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "BalanceController", "GetRegPatListByOther", requestAction);
            List<OP_PatList> patlist = retdata.GetData<List<OP_PatList>>(0);
            if (patlist == null || patlist.Count == 0)
            {
                MessageBoxShowError("找不到该病人信息");
                return;
            }

            ifrmOutPatient.SetPatLists(patlist);
        }

        /// <summary>
        /// 多条挂号记录时，选择一条挂号记录查询
        /// </summary>
        [WinformMethod]
        public void GetOutSelectPatlist()
        {
            ifrmBalance.CurPatList = ifrmOutPatient.GetcurPatlist;
            int agenum = 0;
            string ageUnit = "岁";
            ConvertAge(ifrmOutPatient.GetcurPatlist.Age, out agenum, out ageUnit);
            ifrmBalance.Age = agenum;
            ifrmBalance.AgeUnit = ageUnit;
            GetPrescriptions();
        }
        #endregion

        #region 界面按钮操作
        /// <summary>
        /// 刷新药品项目
        /// </summary>
        [WinformMethod]
        public void GetDrugItemShowCard()
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "BalanceController", "GetDrugItemShowCard");
            dtDrugItem = retdata.GetData<DataTable>(0);//药品项目选项卡数据  
            ifrmBalance.BindDrugItemShowCard(dtDrugItem);
        }

        /// <summary>
        ///  清屏
        /// </summary>
        [WinformMethod]
        public void ClearInfo()
        {
            ifrmBalance.CurPatList = new OP_PatList();
            ifrmBalance.CardNo = string.Empty;
            ifrmBalance.Age = 0;
            ifrmBalance.AgeUnit = string.Empty;
            ifrmBalance.SetCardTypeSelIndex();
            ifrmBalance.SetPresDataSource(new DataTable ());
            ifrmBalance.AllPrescriptionTotalFee = 0;
            ifrmBalance.SetMedicardReadInfo = string.Empty;
        }
        #endregion

        #region 网格操作
        /// <summary>
        /// 选项卡数据填充到指定行
        /// </summary>
        /// <param name="selectedRow">选中行</param>
        /// <param name="rowIndex">当前行</param>
        /// <returns>返回成功和失败</returns>
        [WinformMethod]
        public bool FillSelectedRowData(DataRow selectedRow, int rowIndex)
        {
            DataTable dt = ifrmBalance.Prescriptions;
            List<Prescription> prescriptions = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<Prescription>(dt);
            string result = ValidateRestrictBeforeFillSelectedRowData(selectedRow, rowIndex);
            if (result == "1")
            {
                int itemtype = Convert.ToInt32(selectedRow["ItemClass"]);
                if (itemtype != (int)OP_Enum.ItemType.组合项目)
                {
                    FillOneRowData(selectedRow, rowIndex);
                    SetReadOnly(rowIndex);
                }
                else
                {
                    int currowIndex = rowIndex;
                    DataTable dtExamDetails = new DataTable();
                    int examItemID = Convert.ToInt32(selectedRow["ItemID"]);
                    //调用后台通过组合项目ID获取组合项目明细
                    Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(examItemID);
                    });
                    ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "BalanceController", "GetExamItemDetailDt", requestAction);
                    dtExamDetails = retdata.GetData<DataTable>(0);
                    for (int i = 0; i < dtExamDetails.Rows.Count; i++)
                    {
                        int itemId = Convert.ToInt32(dtExamDetails.Rows[i]["ITEMID"]);
                        int complexId = examItemID;
                        decimal amount = Convert.ToDecimal(dtExamDetails.Rows[i]["ItemAmount"]);
                        string filter = "ITEMID = " + itemId + " AND itemclass = " + (int)OP_Enum.ItemType.收费项目 + string.Empty;
                        DataRow[] drItem = dtDrugItem.Select(filter);
                        if (drItem.Length > 0)
                        {
                            FillOneRowData(drItem[0], currowIndex);
                            dt.Rows[currowIndex]["MiniAmount"] = amount;
                            dt.Rows[currowIndex]["ExamItemID"] = examItemID;
                            dt.Rows[currowIndex]["ItemType"] = (int)OP_Enum.ItemType.组合项目;
                            if (selectedRow["ExecDeptId"].ToString() != "0")
                            {
                                dt.Rows[currowIndex]["ExecDeptId"] = Convert.ToInt32(selectedRow["ExecDeptId"]);
                                dt.Rows[currowIndex]["ExecDetpName"] = selectedRow["ExecDeptName"].ToString();
                            }

                            CalculateRowTotalFee(currowIndex);

                            //插入新的行
                            currowIndex = currowIndex + 1;
                            AddEmptyPrescriptionDetail(currowIndex);
                        }
                    }
                }

                return true;
            }
            else if (result == "2")
            {
                return false;
            }
            else
            {
                if (MessageBoxShowYesNo(result) == DialogResult.Yes)
                {
                    EndPrescription();
                    if (rowIndex != dt.Rows.Count - 1)
                    {
                        DataTable dtt = ifrmBalance.Prescriptions;
                        AddEmptyPrescriptionDetail(dtt.Rows.Count);
                        ifrmBalance.SetDgPresfocus(dtt.Rows.Count-1);
                        FillSelectedRowData(selectedRow, dtt.Rows.Count-1);
                        return true;
                    }
                    else
                    {
                        //增加空的处方明细行供录入
                        AddEmptyPrescriptionDetail(rowIndex + 1);
                        ifrmBalance.SetDgPresfocus(rowIndex + 1);
                        FillSelectedRowData(selectedRow, rowIndex + 1);
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// 选项卡数据选中赋值
        /// </summary>
        /// <param name="selectedRow">选中行</param>
        /// <param name="rowindex">当前行号</param>
        private void FillOneRowData(DataRow selectedRow, int rowindex)
        {
            DataTable tbPresc = ifrmBalance.Prescriptions;
            DataRow dr = tbPresc.Rows[rowindex];
            dr["ItemID"] = selectedRow["ItemID"];
            dr["ItemName"] = selectedRow["ItemName"].ToString();
            dr["Spec"] = selectedRow["Standard"].ToString();
            dr["RetailPrice"] = Convert.ToDecimal(selectedRow["SellPrice"]);
            dr["StockPrice"] = Convert.ToDecimal(selectedRow["InPrice"]);
            dr["PackUnit"] = selectedRow["UnPickUnit"].ToString();
            dr["MiniUnit"] = selectedRow["MiniUnitName"].ToString();
            dr["UnitNO"] = Convert.ToDecimal(selectedRow["MiniConvertNum"]);
            dr["StatID"] = selectedRow["StatID"].ToString();

            //项目类型 1药品 2物资 3-收费项目 4组合项目 5说明性医嘱          
            dr["ItemType"] = selectedRow["ItemClass"].ToString(); 

            //查找当前行所在的处方的医生、处方ID，
            #region .................
            int start, end, subTotalRow;
            GetPrescriptionSectionStartRow(rowindex, out start, out end, out subTotalRow);
            if (subTotalRow == -1)
            {
                if (start == end)
                {
                    //如果是新开处方的第一行,处方医生和科室取当前选择的医生和科室
                    dr["PresEmpID"] = ifrmBalance.GetPresDocid;
                    dr["PresDeptID"] = ifrmBalance.GetPresDeptid;
                    dr["PresDocName"] = ifrmBalance.GetPresDocName;
                    dr["FeeItemHeadID"] = 0;
                }
                else
                {
                    //取其他行的医生和科室
                    for (int i = start; i <= end; i++)
                    {
                        if (!Convert.IsDBNull(tbPresc.Rows[i]["PresEmpID"])
                            && tbPresc.Rows[i]["PresEmpID"].ToString().Trim() != string.Empty)
                        {
                            dr["PresEmpID"] = tbPresc.Rows[i]["PresEmpID"];
                            dr["PresDeptID"] = tbPresc.Rows[i]["PresDeptID"];
                            dr["PresDocName"] = tbPresc.Rows[i]["PresDocName"];
                            dr["FeeItemHeadID"] = tbPresc.Rows[i]["FeeItemHeadID"];
                            dr["FeeNo"] = tbPresc.Rows[i]["FeeNo"];
                            break;
                        }
                    }
                }
            }
            else
            {
                //取其他行的医生和科室
                for (int i = start; i <= end; i++)
                {
                    if (!Convert.IsDBNull(tbPresc.Rows[i]["PresEmpID"])
                        && tbPresc.Rows[i]["PresEmpID"].ToString().Trim() != string.Empty)
                    {
                        dr["PresEmpID"] = tbPresc.Rows[i]["PresEmpID"];
                        dr["PresDeptID"] = tbPresc.Rows[i]["PresDeptID"];
                        dr["PresDocName"] = tbPresc.Rows[i]["PresDocName"];
                        dr["FeeItemHeadID"] = tbPresc.Rows[i]["FeeItemHeadID"];
                        dr["FeeNo"] = tbPresc.Rows[i]["FeeNo"];
                        break;
                    }
                }
            }
            #endregion
            if (selectedRow["ExecDeptId"].ToString() == "0")
            {
                dr["ExecDetpName"] = ifrmBalance.GetPresDeptName;
                dr["ExecDeptId"] = ifrmBalance.GetPresDeptid;
            }
            else
            {
                dr["ExecDeptID"] = Convert.ToInt32(selectedRow["ExecDeptId"]);
                dr["ExecDetpName"] = selectedRow["ExecDeptName"].ToString();
            }

            dr["DocPresHeadID"] = 0;
            dr["DocPresDetailID"] = 0;
            dr["SubTotalFlag"] = 0;
            dr["DrugApprovalnumber"] = selectedRow["NationalCode"] == DBNull.Value ? string.Empty : selectedRow["NationalCode"].ToString().Trim();
        }

        /// <summary>
        /// 是否允许开始对处方进行操作
        /// </summary>
        /// <returns>bool</returns>
        [WinformMethod]
        public bool AllowBeginPrescriptionHandle()
        {
            if (ifrmBalance.CurPatList == null || ifrmBalance.CurPatList.PatListID == 0)
            {
                MessageBoxShowError("操作处方前请先选择病人。\r\n1、你可以选择需要的查询方式后输入检索码后按Enter进行检索\r\n2、点击查找病人按钮，找到目标病人后确定");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 在指定位置增加空的明细行
        /// </summary>
        /// <param name="destRowIndex">目的行索引</param>
        [WinformMethod]
        public void AddEmptyPrescriptionDetail(int destRowIndex)
        {
            DataTable tbPresc = ifrmBalance.Prescriptions;
            DataRow dr = tbPresc.NewRow();
            dr["presNO"] = DBNull.Value;
            dr["ItemID"] = 0;
            dr["ItemName"] = string.Empty;
            dr["PackUnit"] = string.Empty;
            dr["RetailPrice"] = "0.00";
            dr["PackAmount"] = 0;
            dr["PackUnit"] = string.Empty;
            dr["MiniAmount"] = 0;
            dr["PresAmount"] = 1;
            dr["MiniUnit"] = string.Empty;
            dr["PresDocName"] = string.Empty;
            dr["ExecDetpName"] = string.Empty;
            dr["TotalFee"] = 0;
            dr["SubTotalFlag"] = 0;
            dr["ExamItemID"] = 0;
            dr["FeeItemHeadID"] = 0;
            dr["PresDetailID"] = 0;
            dr["Selected"] = true;
            dr["DocPresHeadID"] = 0;
            dr["ExecDeptID"] = 0;
            dr["PresDeptID"] = 0;
            dr["PresEmpID"] = 0;
            dr["ModifyFlag"] = 1;
            tbPresc.Rows.InsertAt(dr, destRowIndex);
            dr["PrescGroupID"] = GetPrescriptionAmbitAfterInsertEmptyRow(tbPresc.Rows.IndexOf(dr));
            int start, end, subTotalRow;
            GetPrescriptionSectionStartRow(destRowIndex, out start, out end, out subTotalRow);
            if (start == end)
            {
                dr["presNO"] = GetMaxAmbit();
            }       
        }

        /// <summary>
        /// 设置修改状态
        /// </summary>
        /// <param name="rowIndex">当前行号</param>
        [WinformMethod]
        public void SetPrescriptionModifyStatus(int rowIndex)
        {
            DataTable tbPresc = ifrmBalance.Prescriptions;
            if (tbPresc.Rows.Count == 0)
            {
                return;
            }

            tbPresc.Rows[rowIndex]["ModifyFlag"] = (short)1;
            ifrmBalance.SetGridColor();
        }

        /// <summary>
        /// 计算指定行的金额
        /// </summary>
        /// <param name="rowIndex">当前行号</param>
        [WinformMethod]
        public void CalculateRowTotalFee(int rowIndex)
        {
            DataTable dt = ifrmBalance.Prescriptions;
            if (dt.Rows.Count == 0)
            {
                return;
            }

            //得到行合计
            dt.Rows[rowIndex]["TotalFee"] = _calculateRowTotalFee(rowIndex);

            //重新计算当前处方合计
            CalculatePrescriptionFee(rowIndex);

            //重新计算所有处方总金额
            CalculateAllPrescriptionFee();
        }

        /// <summary>
        /// 结束处方
        /// </summary>
        [WinformMethod]
        public void EndPrescription()
        {
            //查找未结束的处方
            DataTable tbPresc = ifrmBalance.Prescriptions;
            List<int> prescNums = GetPrescriptionAmbitList();
            int notEndPrescAmbit = 0;

            //保存未结束的处方的处方张数标志
            for (int i = 0; i < prescNums.Count; i++)
            {
                int ambit = Convert.ToInt32(prescNums[i]);
                DataRow[] drs = tbPresc.Select("PrescGroupID = " + ambit + string.Empty);
                bool prescEnd = false;
                for (int j = 0; j < drs.Length; j++)
                {
                    int subtotal_flag = Convert.ToInt32(drs[j]["SubTotalFlag"]);
                    if (subtotal_flag == 1)
                    {
                        prescEnd = true;
                        break;
                    }
                }

                if (prescEnd == false)
                {
                    notEndPrescAmbit = ambit;
                    break;
                }
            }
            //处理未结束的处方
            if (notEndPrescAmbit != 0)
            {
                DataTable dtCopy = tbPresc.Clone();
                dtCopy.Clear();
                for (int i = 0; i < tbPresc.Rows.Count; i++)
                {
                    dtCopy.Rows.Add(tbPresc.Rows[i].ItemArray);
                }

                for (int i = 0; i < dtCopy.Rows.Count; i++)
                {
                    int subtotal_flag = Convert.ToInt32(dtCopy.Rows[i]["SubTotalFlag"]);
                    if (subtotal_flag == 0 && (Convert.IsDBNull(dtCopy.Rows[i]["ItemID"]) ? 0 : Convert.ToInt32(dtCopy.Rows[i]["itemid"])) == 0)
                    {
                        tbPresc.Rows.RemoveAt(i);
                    }
                }

                DataRow[] drNotEndPresc = tbPresc.Select("PrescGroupID=" + notEndPrescAmbit + string.Empty);
                if (drNotEndPresc.Length > 0)
                {
                    dtCopy.Clear();
                    for (int i = 0; i < tbPresc.Rows.Count; i++)
                    {
                        dtCopy.Rows.Add(tbPresc.Rows[i].ItemArray);
                    }

                    int lastIndex = dtCopy.Rows.Count - 1;
                    for (int i = 0; i < dtCopy.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dtCopy.Rows[i]["PrescGroupID"]) == notEndPrescAmbit)
                        {
                            lastIndex = i;
                        }
                    }

                    DataRow drSubTotal = tbPresc.NewRow();
                    drSubTotal["ExecDetpName"] = "小  计";
                    drSubTotal["RetailPrice"] = DBNull.Value;
                    drSubTotal["PackAmount"] = DBNull.Value;
                    drSubTotal["MiniAmount"] = DBNull.Value;
                    drSubTotal["SubTotalFlag"] = 1;
                    drSubTotal["Selected"] = true;
                    drSubTotal["PrescGroupID"] = notEndPrescAmbit;
                    tbPresc.Rows.InsertAt(drSubTotal, lastIndex + 1);
                    CalculatePrescriptionFee(lastIndex);
                }
            }

           ifrmBalance.SetGridColor();
        }

        /// <summary>
        /// 只读设置
        /// </summary>
        /// <param name="rowIndex">当前行号</param>
        [WinformMethod]
        public void SetReadOnly(int rowIndex)
        {
            try
            {
                DataTable tbPresc = ifrmBalance.Prescriptions;
                if (rowIndex >= tbPresc.Rows.Count)
                {
                    return;
                }

                if (ifrmBalance.BalanceMode == 1)
                {
                    ifrmBalance.SetReadOnly(ReadOnlyType.全部只读);
                    return;
                }

                if (Convert.ToInt32(tbPresc.Rows[rowIndex]["SubTotalFlag"]) == 1)
                {
                    ifrmBalance.SetReadOnly(ReadOnlyType.不能修改);
                    return;
                }

                if (Convert.ToInt32(tbPresc.Rows[rowIndex]["DocPresHeadID"]) != 0)
                {
                    ifrmBalance.SetReadOnly(ReadOnlyType.不能修改);
                    return;
                }

                if ((Convert.IsDBNull(tbPresc.Rows[rowIndex]["ItemID"]) ? 0 : Convert.ToInt32(tbPresc.Rows[rowIndex]["ItemID"])) == 0)
                {
                    ifrmBalance.SetReadOnly(ReadOnlyType.新开);
                    return;
                }

                if (Convert.ToInt32(tbPresc.Rows[rowIndex]["PresDetailID"]) > 0 && Convert.ToInt32(tbPresc.Rows[rowIndex]["ModifyFlag"]) == 0)
                {
                    ifrmBalance.SetReadOnly(ReadOnlyType.不能修改);
                    return;
                }

                string bigitemcode = tbPresc.Rows[rowIndex]["statid"].ToString().Trim();
                int itemtype = Convert.ToInt32(tbPresc.Rows[rowIndex]["itemtype"]);
                if (itemtype != (int)OP_Enum.ItemType.药品)
                {
                    ifrmBalance.SetReadOnly(ReadOnlyType.项目);
                    return;
                }

                string filter = "ITEMID = " + tbPresc.Rows[rowIndex]["itemid"] + " AND itemclass =  " + itemtype + string.Empty;
                DataRow[] drItem = dtDrugItem.Select(filter);
                if (bigitemcode == "102")
                {
                    ifrmBalance.SetReadOnly(ReadOnlyType.中草药);
                    return;
                }

                //1 可拆零
                if (Convert.ToInt32(drItem[0]["ResolveFlag"]) == 1) 
                {
                    ifrmBalance.SetReadOnly(ReadOnlyType.药品可拆零);
                    return;
                }
                else
                {
                    ifrmBalance.SetReadOnly(ReadOnlyType.药品不可拆零);
                    return;
                }
            }
            catch
            {
                ifrmBalance.SetReadOnly(ReadOnlyType.新开);
                return;
            }
        }

        /// <summary>
        /// 是否允许增加空的明细行
        /// </summary>
        /// <param name="crossRowIndex">参照行，一般为当前所在行</param>       
        /// <returns>bool</returns>
        [WinformMethod]
        public bool AllowAddEmptyPrescriptionDetail(int crossRowIndex)
        {
            DataTable tbPresc = ifrmBalance.Prescriptions;
            if (tbPresc.Rows.Count == 0)
            {
                return true;
            }

            bool isLastRow = (crossRowIndex == (tbPresc.Rows.Count - 1) ? true : false);
            int subTotalRowFlag = Convert.ToInt32(tbPresc.Rows[crossRowIndex]["SubTotalFlag"]);
            if (subTotalRowFlag == 1)
            {
                MessageBoxShowError("小计行不能进行任何操作");
                return false;
            }
            else
            {
                int doctor_presc_id = Convert.ToInt32(tbPresc.Rows[crossRowIndex]["DocPresHeadID"]);
                if (doctor_presc_id != 0)
                {
                    MessageBoxShowError("医生处方不允许进行追加，删除行操作！");
                    return false;
                }

                int itemId = Convert.ToInt32(tbPresc.Rows[crossRowIndex]["itemid"]);
                if (isLastRow && itemId == 0)
                {
                    MessageBoxShowError("最后一行已经是空行");
                    return false;
                }

                if (ifrmBalance.GetPresDocid == 0 || ifrmBalance.GetPresDeptid == 0)
                {
                    MessageBoxShowError("新增处方或处方明细前请先指定处方医生");
                    return false;
                }
                else
                {
                    int start, end, subtotalRow;
                    GetPrescriptionSectionStartRow(crossRowIndex, out start, out end, out subtotalRow);
                    Hashtable doctorIds = new Hashtable();
                    int presambit = Convert.ToInt32(tbPresc.Rows[crossRowIndex]["PrescGroupID"]);
                    DataRow[] drs = tbPresc.Select("PrescGroupID=" + presambit + " AND  SubTotalFlag = 0");
                    for (int i = 0; i < drs.Length; i++)
                    {
                        int docId = Convert.ToInt32(drs[i]["PresEmpID"]);
                        if (!doctorIds.ContainsKey(docId))
                        {
                            doctorIds.Add(docId, drs[i]["PresDocName"].ToString().Trim());
                        }
                    }

                    if (doctorIds.Count > 0)
                    {
                        if (!doctorIds.ContainsKey(ifrmBalance.GetPresDocid))
                        {
                            MessageBoxShowError("相同处方内处方医生必须一致");
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 在填充空行前的约束检查
        /// 原则:
        /// 1、项目和药品不能在同一处方中
        /// 2、如果是项目，执行科室不同不能在同一处方中
        /// 3、西成药与中草药不能在同一处方中
        /// 4、在填充前检查选择的处方医生是否一致
        /// 5、处方医生是否填写
        /// 6、药品要进行0库存判断
        /// </summary>
        /// <param name="selectedRow">将要填充的选择的行记录</param>
        /// <param name="rowIndex">要填充的行</param>      
        /// <returns>错误提示信息</returns>
        private string ValidateRestrictBeforeFillSelectedRowData(DataRow selectedRow, int rowIndex)
        {
            DataTable tbPresc = ifrmBalance.Prescriptions;

            //处方明细空，允许
            if (tbPresc.Rows.Count == 0)
            {
                return "1";
            }

            string bigitemcode = selectedRow["statid"].ToString().Trim();
            string exec_dept_code = selectedRow["ExecDeptId"].ToString().Trim();
            int itemtype = Convert.ToInt32(selectedRow["ItemClass"]);
            decimal storenum = Convert.ToDecimal(selectedRow["StoreAmount"]);

            //约束6:判断0库存
            if (itemtype == (int)OP_Enum.ItemType.药品)
            {
                //需调用药房实时有效库存判断
                if (storenum <= 0)
                {
                    MessageBoxShowError("【" + selectedRow["ITEMNAME"].ToString().Trim() + "】库存为零。请通知药房及时入库");
                    return "2";
                }
            }

            int start, end, subtotalrow;
            GetPrescriptionSectionStartRow(rowIndex, out start, out end, out subtotalrow);
            if (rowIndex == start && start == end)
            {
                //是处方的第一行并且只有处方只有一个空行（新开的处方）
                if (ifrmBalance.GetPresDocid == 0)
                {
                    MessageBoxShowError("没有选择处方医生！");
                    return "2";
                }

                return "1";
            }

            Hashtable doctorIds = new Hashtable();
            for (int i = start; i <= end; i++)
            {
                if (i == rowIndex)
                {
                    continue;
                }

                int docId = Convert.ToInt32(tbPresc.Rows[i]["PresEmpID"]);
                if (docId != 0)
                {
                    if (!doctorIds.ContainsKey(docId))
                    {
                        doctorIds.Add(docId, tbPresc.Rows[i]["PresDocName"]);
                    }
                }

                if (Convert.ToInt32(tbPresc.Rows[i]["itemid"]) != 0)
                {
                    string bigitemcode2 = tbPresc.Rows[i]["statid"].ToString().Trim();
                    string exec_dept_code2 = tbPresc.Rows[i]["ExecDeptId"].ToString().Trim();
                    bool selectedIsdrug = bigitemcode == "100" || bigitemcode == "101" || bigitemcode == "102";
                    bool detailIsdrug = bigitemcode2 == "100" || bigitemcode2 == "101" || bigitemcode2 == "102";
                    #region 原则1、3判断
                    if (detailIsdrug && selectedIsdrug)
                    {
                        if (bigitemcode == "102" && (bigitemcode2 == "100" || bigitemcode2 == "101"))
                        {
                            return "中草药与西药成药不能开在同一处方！是否需要自动分方?";
                        }
                        else
                        {
                            if (bigitemcode2 == "102" && (bigitemcode == "100" || bigitemcode == "101"))
                            {
                                return "中草药与西药成药不能开在同一处方！是否需要自动分方?";
                            }
                        }
                    }
                    else
                    {
                        if ((detailIsdrug && !selectedIsdrug) || (!detailIsdrug && selectedIsdrug))
                        {
                            return "药品和项目不能开在同一处方！是否需要自动分方?";
                        }
                    }
                    #endregion

                    #region 原则2判断     
                    if (!string.IsNullOrEmpty(exec_dept_code) && exec_dept_code != "0" && exec_dept_code2 != exec_dept_code)
                    {
                        return "执行科室不同不能开在同一处方！是否需要自动分方";
                    }
                    #endregion
                }
            }
            //验证4约束
            if (doctorIds.Count > 0 && !doctorIds.ContainsKey(ifrmBalance.GetPresDocid))
            {
                MessageBoxShowError("当前的处方医生不是指定的医生，同一处方只允许一个医生");
                return "2";
            }

            return "1";
        } 

        /// <summary>
        /// 计算指定行处的处方的总金额
        /// </summary>
        /// <param name="rowIndex">当前行</param>        
        public void CalculatePrescriptionFee(int rowIndex)
        {
            DataTable dt = ifrmBalance.Prescriptions;
            if (dt.Rows.Count == 0)
            {
                return;
            }

            int start, end, subRowTotal;
            GetPrescriptionSectionStartRow(rowIndex, out start, out end, out subRowTotal);
            if (subRowTotal != -1)
            {
                dt.Rows[subRowTotal]["TotalFee"] = _calculatePrescriptionFee(rowIndex);
            }
        }

        /// <summary>
        /// 设置当前行所在的处方的选中状态
        /// </summary>
        /// <param name="rowIndex">当前行号</param>
        [WinformMethod]
        public void SetPrescriptionSelectedStatus(int rowIndex)
        {
            DataTable tbPresc = ifrmBalance.Prescriptions;
            if (tbPresc.Rows.Count == 0)
            {
                return;
            }

            int start, end, subTotalRow;
            GetPrescriptionSectionStartRow(rowIndex, out start, out end, out subTotalRow);
            int presAmbit = Convert.ToInt32(tbPresc.Rows[rowIndex]["PrescGroupID"]);
            int currentSelectedStatus = Convert.ToInt32(tbPresc.Rows[rowIndex]["Selected"]);
            DataRow[] drsPresc = tbPresc.Select("PrescGroupID=" + presAmbit);
            for (int i = 0; i < drsPresc.Length; i++)
            {
                drsPresc[i]["Selected"] = currentSelectedStatus == 1 ? 0 : 1;
            }
        }

        /// <summary>
        /// 计算所有处方的总金额
        /// </summary>      
        [WinformMethod]
        public void CalculateAllPrescriptionFee()
        {
            ifrmBalance.AllPrescriptionTotalFee = _calculateAllPrescriptionFee();
        }

        #region 私有方法
        /// <summary>
        /// 在执行插入空行操作后得到改行的处方张数标志
        /// </summary>
        /// <param name="rowIndex">插入行的行索引</param>
        /// <returns>处方张数</returns>
        private int GetPrescriptionAmbitAfterInsertEmptyRow(int rowIndex)
        {
            DataTable dt = ifrmBalance.Prescriptions;
            List<Prescription> prescriptions = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<Prescription>(dt);
            if (rowIndex >= prescriptions.Count)
            {
                return 0;
            }

            if (rowIndex == 0)
            {
                //如果是第一行，返回1
                return 1;
            }
            else
            {
                if (prescriptions[rowIndex - 1].SubTotalFlag == 1)
                {
                    //如果上一行是小计行，返回新的标志值
                    int ret = GetMaxAmbit() + 1;
                    return ret;
                }
                else
                {
                    //直接返回上一行的标识值
                    return Convert.ToInt32(prescriptions[rowIndex - 1].PrescGroupID);
                }
            }
        }

        /// <summary>
        /// 计算指定行的金额
        /// </summary>
        /// <param name="rowIndex">指定行</param>
        /// <returns>返回指定行金额</returns>
        private decimal _calculateRowTotalFee(int rowIndex)
        {
            DataTable dt = ifrmBalance.Prescriptions;
            List<Prescription> prescriptions = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<Prescription>(dt);
            if (prescriptions.Count == 0)
            {
                return 0;
            }

            int start, end, subRowTotal;
            GetPrescriptionSectionStartRow(rowIndex, out start, out end, out subRowTotal);
            if (rowIndex == subRowTotal)
            {
                return 0;
            }

            int item_id = Convert.ToInt32(prescriptions[rowIndex].ItemID);
            if (item_id == 0)
            {
                return 0;
            }

            if (Convert.IsDBNull(prescriptions[rowIndex].MiniAmount))
            {
                prescriptions[rowIndex].MiniAmount = 0;
            }

            if (Convert.IsDBNull(prescriptions[rowIndex].PackAmount))
            {
                prescriptions[rowIndex].PackAmount = 0;
            }

            decimal base_num = Convert.ToDecimal(prescriptions[rowIndex].MiniAmount);
            decimal pack_num = Convert.ToDecimal(prescriptions[rowIndex].PackAmount);
            decimal relation_num = Convert.ToDecimal(prescriptions[rowIndex].UnitNO);
            decimal sell_price = Convert.ToDecimal(prescriptions[rowIndex].RetailPrice);
            decimal presAmount = Convert.ToDecimal(prescriptions[rowIndex].PresAmount);

            //计算行合计金额
            decimal rowTotal = (sell_price / relation_num) * ((relation_num * pack_num) + base_num) * presAmount;
           
            //保留两位小数
            return decimal.Round(rowTotal, 2);
        }

        /// <summary>
        /// 计算指定行处的处方的总金额
        /// </summary>
        /// <param name="rowIndex">当前行</param>
        /// <returns>返回处方总金额</returns>
        private decimal _calculatePrescriptionFee(int rowIndex)
        {
            DataTable dt = ifrmBalance.Prescriptions;
            List<Prescription> prescriptions = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<Prescription>(dt);
            if (prescriptions.Count == 0)
            {
                return 0;
            }

            int start, end, subRowTotal;
            GetPrescriptionSectionStartRow(rowIndex, out start, out end, out subRowTotal);

            //计算当前处方金额
            List<Prescription> details = new List<Prescription>();
            for (int i = start; i <= end; i++)
            {
                if (Convert.ToInt32(prescriptions[i].ItemID) == 0 || prescriptions[i].SubTotalFlag == 1)
                {
                    continue;
                }

                details.Add(prescriptions[i]);
            }

            decimal prescTotal = GetPrescriptionTotalMoney(details);
            return prescTotal;
        }

        /// <summary>
        /// 计算指定处方总金额
        /// </summary>
        /// <param name="prescDetails">处方数据</param>
        /// <returns>返回总金额</returns>
        private decimal GetPrescriptionTotalMoney(List<Prescription> prescDetails)
        {
            decimal presTotalFee = 0;
            Hashtable htBigitemFee = new Hashtable();
            for (int i = 0; i < prescDetails.Count; i++)
            {
                decimal total = prescDetails[i].TotalFee;
                presTotalFee = presTotalFee + total;              
            }  
                   
            return decimal.Round(presTotalFee, 2);
        }

        /// <summary>
        /// 计算所有处方的总金额
        /// </summary>
        /// <returns>总金额</returns>
        private decimal _calculateAllPrescriptionFee()
        {
            DataTable dt = ifrmBalance.Prescriptions;
            if (dt.Rows.Count == 0)
            {
                return 0;
            }

            object obj= dt.Compute("Sum(TotalFee)", " SubTotalFlag=1 and Selected=1");
            if (obj == null || obj==DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(obj);
            }       
        }

        /// <summary>
        /// 得到处方张数标志(不判断结束标志位)
        /// </summary>
        /// <returns>处方张数</returns>
        private List<int> GetPrescriptionAmbitList()
        {
            List<int> prescNums = new List<int>();
            DataTable dt = ifrmBalance.Prescriptions;
            List<Prescription> prescriptions = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<Prescription>(dt);
            for (int i = 0; i < prescriptions.Count; i++)
            {
                if (Convert.IsDBNull(prescriptions[i].PrescGroupID))
                {
                    continue;
                }

                int ambit = Convert.ToInt32(prescriptions[i].PrescGroupID);
                if (prescNums.Where(p => p == ambit).ToList().Count == 0)
                {
                    prescNums.Add(ambit);
                }
            }

            return prescNums;
        }

        /// <summary>
        /// 获取张数标志的最大值
        /// </summary>
        /// <returns>张数标志的最大值</returns>
        private int GetMaxAmbit()
        {
            List<int> prescNums = GetPrescriptionAmbitList();
            if (prescNums.Count == 0)
            {
                return 0;
            }

            return prescNums.Max();
        }

        /// <summary>
        /// 获取处方起始行信息
        /// </summary>
        /// <param name="currentRowIndex">当前行</param>
        /// <param name="startRowIndex">当前处方的开始行</param>
        /// <param name="endRowIndex">当前处方的结束行</param>
        /// <param name="subTotalRow">当前处方的小计行，如果处方未结束，则小计行为-1</param>
        private void GetPrescriptionSectionStartRow(int currentRowIndex, out int startRowIndex, out int endRowIndex, out int subTotalRow)
        {
            startRowIndex = -1;
            endRowIndex = -1;
            subTotalRow = -1;
            DataTable dt = ifrmBalance.Prescriptions;
            List<Prescription> prescriptions = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<Prescription>(dt);
            if (prescriptions.Count == 0)
            {
                return;
            }

            if (currentRowIndex >= prescriptions.Count)
            {
                return;
            }

            //获得当前行所在的处方的处方界限标志
            int prescGroupId = Convert.ToInt32(prescriptions[currentRowIndex].PrescGroupID);
            List<Prescription> drsSubTotal = prescriptions.Where(p => p.PrescGroupID == prescGroupId && p.SubTotalFlag == 1).ToList();
            if (drsSubTotal.Count > 0)
            {
                subTotalRow = prescriptions.IndexOf(drsSubTotal[0]);
            }

            List<Prescription> drsDetail = prescriptions.Where(p => p.PrescGroupID == prescGroupId && p.SubTotalFlag == 0).ToList();
            if (drsDetail.Count > 0)
            {
                startRowIndex = prescriptions.IndexOf(drsDetail[0]);
                endRowIndex = prescriptions.IndexOf(drsDetail[drsDetail.Count - 1]);
            }
        }

        /// <summary>
        /// 获得指定处方的处方张数标志
        /// </summary>
        /// <param name="appointedRowIndex">指定的行索引,该索引包含在处方所有的行的索引中</param>
        /// <returns>指定处方的处方张数标志</returns>
        private int GetAppointedPrescriptionAmbit(int appointedRowIndex)
        {
            DataTable tbPresc = ifrmBalance.Prescriptions;
            if (tbPresc.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(tbPresc.Rows[appointedRowIndex]["PrescGroupID"]);
            }
        }
        #endregion
        #endregion

        #region 处方数据库操作，获取，新增，修改，删除，保存
        /// <summary>
        /// 获取病人处方信息
        /// </summary>
        [WinformMethod]
        public void GetPrescriptions()
        {
            ifrmBalance.SetexPlLastPayExpanded = false;
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(ifrmBalance.CurPatList.PatListID);//病人Id
                request.AddData(OP_Enum.PresStatus.未收费);//处方状态
                request.AddData(ifrmBalance.CostPatTypeid);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "BalanceController", "GetPatPrescription", requestAction);
            DataTable dtPres = retdata.GetData<DataTable>(0);
            List<OPD_DiagnosisRecord> diagnoseList = retdata.GetData<List<OPD_DiagnosisRecord>>(1);//诊断信息
            for (int i = 0; i < dtPres.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtPres.Rows[i]["presNO"]) == 0)
                {
                    dtPres.Rows[i]["presNO"] = DBNull.Value;
                }

                if (Convert.ToInt32(dtPres.Rows[i]["SubTotalFlag"]) == 1)
                {
                    dtPres.Rows[i]["presNO"] = DBNull.Value;
                    dtPres.Rows[i]["RetailPrice"] = DBNull.Value;
                    dtPres.Rows[i]["PresAmount"] = DBNull.Value;
                    dtPres.Rows[i]["MiniAmount"] = DBNull.Value;
                    dtPres.Rows[i]["PackAmount"] = DBNull.Value;
                    dtPres.Rows[i]["ItemID"] = DBNull.Value;
                }
            }

            ifrmBalance.SetPresDataSource(dtPres);
            ifrmBalance.DiagnosisList = diagnoseList;
            CalculateAllPrescriptionFee();
        }

        /// <summary>
        /// 是否允许删除处方明细
        /// </summary>
        /// <param name="rowIndex">要删除的明细所在的行索引</param>      
        /// <returns>bool</returns>
        [WinformMethod]
        public bool AllowDeletePrescriptionDetail(int rowIndex)
        {
            DataTable tbPresc = ifrmBalance.Prescriptions;
            if (tbPresc.Rows.Count == 0)
            {
                return false;
            }

            int subTotalRowFlag = Convert.ToInt32(tbPresc.Rows[rowIndex]["SubTotalFlag"]);
            if (subTotalRowFlag == 1)
            {
                //小计行不能删除
                MessageBoxShowError("小计行不能进行任何操作");
                return false;
            }

            int isDoctorPresc = Convert.ToInt32(tbPresc.Rows[rowIndex]["DocPresHeadID"]);
            if (isDoctorPresc != 0)
            {
                //医生开出的处方不能删除
                MessageBoxShowError("医生站开出的处方不能由划价收费操作员删除");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 删除一条处方明细
        /// </summary>
        /// <param name="rowIndex">要删除的明细所在的行索引</param>
        [WinformMethod]
        public void DeletePrescriptionDetail(int rowIndex)
        {
            DataTable tbPresc = ifrmBalance.Prescriptions;
            if (tbPresc.Rows.Count == 0)
            {
                return;
            }

            int start, end, subtotal;
            GetPrescriptionSectionStartRow(rowIndex, out start, out end, out subtotal);
            if (start == end)
            {
                //如果处方的开始行等于处方的结束行，说明当前处方只有一条记录，直接调用删除整张处方功能
                DeletePrescription(rowIndex);
            }
            else
            {
                if (Convert.ToInt32(tbPresc.Rows[rowIndex]["ExamItemID"]) > 0)
                {
                    MessageBoxShowError("删除行属于组合项目，不能删除一行，只能整张处方删除");
                    return;
                }

                int count = rowIndex + 1;
                if (MessageBoxShowYesNo("确认要删除第" + count + "行的处方吗？") != DialogResult.Yes)
                {
                    return;
                }

                int detailId = Convert.ToInt32(tbPresc.Rows[rowIndex]["PresDetailID"]);
                try
                {
                    if (detailId > 0)
                    {
                        Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                        {
                            request.AddData(detailId);
                        });
                        ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "BalanceController", "DeletePrescriptionDetail", requestAction);
                       
                        //调用后台删除
                        MessageBoxShowSimple("删除成功");
                    }
                }
                catch (Exception err)
                {
                    MessageBoxShowError(err.Message);
                    return;
                }

                tbPresc.Rows.RemoveAt(rowIndex);
            }

            ifrmBalance.SetGridColor();
        }

        /// <summary>
        /// 删除一张处方
        /// </summary>
        /// <param name="rowIndex">选中的处方所包含的行索引</param>
        [WinformMethod]
        public void DeletePrescription(int rowIndex)
        {
            DataTable tbPresc = ifrmBalance.Prescriptions;
            if (tbPresc.Rows.Count == 0)
            {
                return;
            }

            int start, end, subtotal;
            GetPrescriptionSectionStartRow(rowIndex, out start, out end, out subtotal);
            int count = rowIndex + 1;
            if (MessageBoxShowYesNo("确认要删除第" + count + "行的整张处方吗？") != DialogResult.Yes)
            {
                return;
            }

            try
            {
                int presId = Convert.ToInt32(tbPresc.Rows[rowIndex]["FeeItemHeadID"]);
                if (presId > 0)
                {
                    Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(presId);
                    });
                    ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "BalanceController", "DeletePrescription", requestAction);
                    //调用后台删除
                    MessageBoxShowSimple("删除成功");
                }
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
                return;
            }

            //获得处方张数标志
            int presAmbit = GetAppointedPrescriptionAmbit(rowIndex);

            //得到指定的处方的所有明细
            DataRow[] drsNeedRemove = tbPresc.Select("PrescGroupID=" + presAmbit);
            for (int i = 0; i < drsNeedRemove.Length; i++)
            {
                tbPresc.Rows.Remove(drsNeedRemove[i]);
            }

            ifrmBalance.SetGridColor();
        }

        /// <summary>
        /// 保存处方
        /// </summary>
        /// <returns>返回是否成功</returns>
        [WinformMethod]
        public bool SavePrescription()
        {
            DataTable tbPresc = ifrmBalance.Prescriptions;
            if (tbPresc.Rows.Count == 0)
            {
                return false;
            }

            try
            {
                List<int> listHeadCount = GetPrescriptionAmbitList();
                List<Prescription> prescriptions = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<Prescription>(tbPresc);
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(ifrmBalance.CurPatList);//当前病人对象
                    request.AddData(prescriptions);//界面处方信息
                    request.AddData(listHeadCount);//处方数
                    request.AddData(LoginUserInfo.EmpId);//操作员ID
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "BalanceController", "SavePrescription", requestAction);
                List < Prescription > savePrescriptions= retdata.GetData<List<Prescription>>(0);
                DataTable dtPres = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable(savePrescriptions);
                for (int i = 0; i < dtPres.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtPres.Rows[i]["presNO"]) == 0)
                    {
                        dtPres.Rows[i]["presNO"] = DBNull.Value;
                    }

                    if (Convert.ToInt32(dtPres.Rows[i]["SubTotalFlag"]) == 1)
                    {
                        dtPres.Rows[i]["presNO"] = DBNull.Value;
                        dtPres.Rows[i]["RetailPrice"] = DBNull.Value;
                        dtPres.Rows[i]["PresAmount"] = DBNull.Value;
                        dtPres.Rows[i]["MiniAmount"] = DBNull.Value;
                        dtPres.Rows[i]["PackAmount"] = DBNull.Value;
                        dtPres.Rows[i]["ItemID"] = DBNull.Value;
                    }
                }

                ifrmBalance.SetPresDataSource(dtPres);
                CalculateAllPrescriptionFee();                
                return true;
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
                return false;
            }            
        }

        /// <summary>
        /// 检查数据正确性
        /// </summary>
        /// <returns>返回是否正确</returns>
        [WinformMethod]
        public bool CheckDataValidity()
        {
            DataTable tbPresc = ifrmBalance.Prescriptions;
            if (tbPresc.Rows.Count == 0)
            {
                MessageBoxShowSimple("没有需要保存的处方！");
                return false;
            }

            //1、检查数量是否合法
            DataRow[] drs = tbPresc.Select("SubTotalFlag= 0");
            for (int i = 0; i < drs.Length; i++)
            {
                int itemId = Convert.ToInt32(drs[i]["ItemID"]);
                if (itemId == 0)
                {
                    tbPresc.Rows.Remove(drs[i]);
                }
            }

            drs = tbPresc.Select("SubTotalFlag= 0");
            for (int i = 0; i < drs.Length; i++)
            {
                string item_name = drs[i]["ItemName"].ToString().Trim();
                decimal baseNum = Convert.ToDecimal(drs[i]["MiniAmount"]);
                decimal packNum = Convert.ToDecimal(drs[i]["PackAmount"]);
                decimal total_fee = Convert.ToDecimal(drs[i]["TotalFee"]);
                decimal price = Convert.ToDecimal(drs[i]["RetailPrice"]);
                int presDoctorId = Convert.ToInt32(drs[i]["PresEmpID"]);
                int docDeptId = Convert.ToInt32(drs[i]["PresDeptID"]);
                if (presDoctorId == 0)
                {
                    MessageBoxShowError("【" + item_name + "】处方医生填写不正确！");
                    return false;
                }

                if (docDeptId == 0)
                {
                    MessageBoxShowError("【" + item_name + "】医生科室填写不正确");
                    return false;
                }

                if (price <= 0)
                {
                    MessageBoxShowError("【" + item_name + "】价格设置不正确!,请联系管理员");
                    return false;
                }

                if (baseNum <= 0 && packNum <= 0)
                {
                    MessageBoxShowError("【" + item_name + "】数量填写不正确！");
                    return false;
                }

                if (total_fee <= 0)
                {
                    MessageBoxShowError("【" + item_name + "】合计金额不正确，请检查数量是否填写正确");
                    return false;
                }
            }

            drs = tbPresc.Select("SubTotalFlag= 1");
            for (int i = 0; i < drs.Length; i++)
            {
                if (Convert.ToDecimal(drs[i]["TotalFee"]) <= 0)
                {
                    MessageBoxShowError("有处方小计为0的处方！");
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region 发票号调整
        /// <summary>
        /// 发票号调整
        /// </summary>
        [WinformMethod]
        public void AdjustInvoiceInit()
        {
            ifrmAdjustInvoice.curInvoiceNO = ifrmBalance.CurInvoiceNO;
        }

        /// <summary>
        /// 设置新的票据号
        /// </summary>
        /// <param name="pefChar">票据前缀</param>
        /// <param name="invoiceNO">票据号</param>
        /// <returns>返回是否成功</returns>
        [WinformMethod]
        public bool AdjustInvoiceSet(string pefChar,string invoiceNO)
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(pefChar);
                    request.AddData(invoiceNO);
                    request.AddData(LoginUserInfo.EmpId);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "BalanceController", "AdjustInvoice", requestAction);
                string  curInvoice = retdata.GetData<string>(0);
                ifrmBalance.CurInvoiceNO = curInvoice;
                return true;
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
                return false;
            }
        }
        #endregion

        #region 收费处理
        /// <summary>
        /// 医保读卡预算
        /// </summary>
        /// <returns>医保金额</returns>
        [WinformMethod]
        public decimal MedieaReadAndBudge()
        {
            if (ifrmPayMentInfo.IsMediaPat)
            {
                try
                {
                    //调用医保读卡                  
                    PatientInfo miPatInfo= MIOPInterface.ReadMediaCard();
                    if (!MIOPInterface.PatCheck(ifrmBalance.CurPatList.PatName, ifrmBalance.CurPatList.MedicareCard, ifrmBalance.CurPatList.IDNumber, miPatInfo))
                    {
                        return 0;
                    }

                    ifrmBalance.MedicardReadInfo = miPatInfo.ShowMiPatInfo;

                    //调用医保预算
                    Dictionary<string, string> dic = MIOPInterface.MIBalanceBuget(LoginUserInfo.WorkId, EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<Prescription>(ifrmBalance.Prescriptions), ifrmBalance.CurPatList, ifrmPayMentInfo.CostPatTypeId,ifrmBalance.DiagnosisList,ifrmBalance.CurInvoiceNO);
                    decimal medicarePay = Convert.ToDecimal(dic["MedicarePay"]);
                    ifrmPayMentInfo.MedicareMIPay = Convert.ToDecimal(dic["MedicareMIPay"]);
                    ifrmPayMentInfo.MedicarePersPay = Convert.ToDecimal(dic["MedicarePersPay"]);

                    ifrmPayMentInfo.SetMediaInfo(dic["MedicardInfo"].ToString());

                    //医保预结算ID
                    ifrmPayMentInfo.MIBlanceBudgetID = Convert.ToInt32(dic["ID"]);
                    ifrmPayMentInfo.YSFlag = true;
                    return medicarePay;
                }
                catch (Exception err)
                {
                    MessageBoxShow(err.Message, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 优惠金额计算
        /// </summary>
        /// <returns>优惠金额</returns>
        [WinformMethod]
        public decimal PromFeeCaculate()
        {
            try
            {
                if (ifrmPayMentInfo.SelfFee == 0)
                {
                    return 0;
                }

                int costPatTypeid = ifrmPayMentInfo.CostPatTypeId;
                var retdata = InvokeWcfService(
                    "OPProject.Service",
                    "BalanceController",
                    "PromFeeCaculate",
                    (request) =>
                    {
                        request.AddData(costPatTypeid);
                        request.AddData(ifrmBalance.CurPatList.MemberAccountID);
                        request.AddData(ifrmPayMentInfo.SelfFee);
                        request.AddData(ifrmPayMentInfo.BudgePres);
                        request.AddData(LoginUserInfo.EmpId);
                        request.AddData(ifrmPayMentInfo.BudgeInfo[0].CostHeadID);//结算ID
                    });
                decimal promFee = retdata.GetData<decimal>(0);
                return promFee;               
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
                return 0;
            }
        }

        /// <summary>
        /// 收银
        /// </summary>
        /// <param name="type">0正常收费 1收银后补收</param>
        /// <returns>弹出窗口</returns>
        [WinformMethod]
        public DialogResult PayInfo(int type)
        {
            try
            {
                ifrmPayMentInfo.BackChargeInfo = new ChargeInfo();
                ifrmPayMentInfo.DtPrintInvoice = new DataTable();
                ifrmPayMentInfo.DtPrintInvoiceDetail = new DataTable();
                ifrmPayMentInfo.DtInvoiceStatDetail = new DataTable();
                ifrmPayMentInfo.BalanceType = type;
                ifrmPayMentInfo.MedicareMIPay = 0;
                ifrmPayMentInfo.MedicarePersPay = 0;

                //调用预算
                BudgeBalance(type);
                if (ifrmPayMentInfo.IsMediaPat)
                {
                    //医保病人读取医保卡
                    PatientInfo miPatInfo = MIOPInterface.ReadMediaCard();

                    //判断医保卡信息和HIs是否一致
                    if (!MIOPInterface.PatCheck(ifrmBalance.CurPatList.PatName, ifrmBalance.CurPatList.MedicareCard, ifrmBalance.CurPatList.IDNumber, miPatInfo))
                    {
                        return DialogResult.None;
                    }
                    //todo 在这调用医保预算并将值保存
                    MedieaReadAndBudge();
                }

                var dialog = iBaseView["FrmPayMentInfo"] as Form;
                if ( dialog==null)
                {
                    return DialogResult.None;
                }

                return dialog.ShowDialog();
            }
            catch (Exception err)
            {
                MessageBoxShow(err.Message, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return DialogResult.None;
            }
        }

        [WinformMethod]
        public decimal GetMedicareMIPay()
        {
            return ifrmPayMentInfo.MedicareMIPay;
        }
        [WinformMethod]
        public decimal GetMedicarePersPay()
        {
            return ifrmPayMentInfo.MedicarePersPay;
        }
        /// <summary>
        /// 预算
        /// </summary>
        /// <param name="type">0正常收费 1收银后补收</param>
        private void BudgeBalance( int type)
        {
            try
            {
                List<Prescription> budgePres=new List<Prescription>();
                List<int> budgeNums = new List<int>();
                if (type == 0)
                {
                    //正常收费
                    DataTable dtPresc = ifrmBalance.Prescriptions;
                    List<Prescription> prescriptions = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<Prescription>(dtPresc);
                    budgeNums = GetBudgePrescriptionAmbitList(prescriptions);
                    if (budgeNums.Count == 0)
                    {
                        throw new Exception("没有选中的处方需要收费");
                    }

                    budgePres = prescriptions.Where(p => p.Selected == 1 && p.SubTotalFlag == 0).ToList();
                }
                else if (type == 1)
                {
                    //退费后补收
                    budgePres = ifrmRefundInfo.BalancePresc;
                    budgeNums = GetBudgePrescriptionAmbitList(budgePres);
                }

                int costPatTypeid = ifrmBalance.CostPatTypeid;                                
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(budgePres);//操作员ID
                    request.AddData(LoginUserInfo.EmpId);//操作员ID
                    request.AddData(ifrmBalance.CurPatList);//当前病人对象
                    request.AddData(costPatTypeid);//结算病人类型 
                    request.AddData(budgeNums);//选中的处方张数
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "BalanceController", "BudgeBalance", requestAction);
                List<ChargeInfo> budgeInfo = retdata.GetData<List<ChargeInfo>>(0);
                ifrmPayMentInfo.CurPatlistId = ifrmBalance.CurPatList.PatListID;
                ifrmPayMentInfo.CostPatTypeId = costPatTypeid;
                ifrmPayMentInfo.FeeHeadIds = budgeInfo[0].FeeItemHeadIDs;
                ifrmPayMentInfo.TotalFee = budgeInfo.Sum(p => p.TotalFee);// budgeInfo[0].TotalFee;    
                ifrmPayMentInfo.BudgeInfo = budgeInfo;//预算信息
                ifrmPayMentInfo.BudgePres = budgePres;
                bool isMediaPat = retdata.GetData<bool>(1);//是否是医保病人
                ifrmPayMentInfo.IsMediaPat = isMediaPat;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }     
        }

        /// <summary>
        /// 获取选中的处方张数
        /// </summary>
        /// <param name="prescriptions">处方明细</param>
        /// <returns>选中的处方张数</returns>       
        private List<int> GetBudgePrescriptionAmbitList(List<Prescription> prescriptions)
        {
            List<int> prescNums = new List<int>();         
            for (int i = 0; i < prescriptions.Count; i++)
            {
                if (prescriptions[i].Selected == 0)
                {
                    continue;
                }

                if (Convert.IsDBNull(prescriptions[i].PrescGroupID))
                {
                    continue;
                }

                int ambit = Convert.ToInt32(prescriptions[i].PrescGroupID);
                if (prescNums.Where(p => p == ambit).ToList().Count == 0)
                {
                    prescNums.Add(ambit);
                }
            }

            return prescNums;
        }

        /// <summary>
        /// 正式结算
        /// </summary>
        /// <returns>返回成功与否true成功false失败</returns>
        [WinformMethod]
        public bool Balance()
        {
            if (ifrmPayMentInfo.IsMediaPat && ifrmPayMentInfo.YSFlag)
            {
                //调用医保接口正式结算
                try
                {
                    MIOPInterface.MIBalance(ifrmBalance.CurInvoiceNO, ifrmPayMentInfo.MIBlanceBudgetID);
                }
                catch (Exception err)
                {
                    MessageBoxShow(err.Message, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return false;
                } 
            }

            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(ifrmPayMentInfo.BudgePres);//费用明细对象
                    request.AddData(LoginUserInfo.EmpId);//操作员ID
                    request.AddData(ifrmBalance.CurPatList);//当前病人对象          
                    request.AddData(ifrmPayMentInfo.BudgeInfo);//预算对象   
                    request.AddData(LoginUserInfo.WorkId);
                    request.AddData(LoginUserInfo.EmpId);
                    request.AddData(LoginUserInfo.DeptId);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "BalanceController", "Balance", requestAction);
                List<ChargeInfo> backBudgeInfo = retdata.GetData<List<ChargeInfo>>(0);
                string curinvoiceNO = retdata.GetData<string>(1);
                DataTable dtInvoice = retdata.GetData<DataTable>(2);
                DataTable dtInvoiceDetail = retdata.GetData<DataTable>(3);
                DataTable dtInvoiceStatDetail = retdata.GetData<DataTable>(4);
                ifrmBalance.CurInvoiceNO = curinvoiceNO;
                StringBuilder sb = new StringBuilder();
                sb.Append("上一病人： " + ifrmBalance.CurPatList.PatName + "\n\n");
                sb.Append("票据张数： " + backBudgeInfo[0].InvoiceCount + "\n\n");
                sb.Append("总 金 额： " + backBudgeInfo[0].TotalFee.ToString("0.00") + "\n\n");
                foreach (OP_CostPayMentInfo payment in backBudgeInfo[0].PayInfoList)
                {
                    sb.Append(payment.PayMentName + "： " + payment.PayMentMoney.ToString("0.00") + "\n\n");
                }

                decimal actPaycash = backBudgeInfo[0].ChangeFee + backBudgeInfo[0].CashFee;
                sb.Append("实付现金： " + actPaycash.ToString("0.00") + "\n\n");
                sb.Append("找零金额： " + backBudgeInfo[0].ChangeFee.ToString("0.00") + "\n\n");
                sb.Append("凑整金额： " + backBudgeInfo[0].RoundFee.ToString("0.00") + "\n\n");
                if (ifrmPayMentInfo.BalanceType == 1)
                {
                    decimal refundcash = Convert.ToDecimal(ifrmRefundInfo.StrRefundCash) - (backBudgeInfo[0].CashFee + backBudgeInfo[0].RoundFee);
                    sb.Append("退费现金： " + refundcash.ToString("0.00") + "\n\n");
                }

                ifrmBalance.StrPayInfo = sb.ToString();

                //打印数据获取
                ifrmPayMentInfo.DtPrintInvoice = dtInvoice;
                ifrmPayMentInfo.DtPrintInvoiceDetail = dtInvoiceDetail;
                ifrmPayMentInfo.BackChargeInfo = backBudgeInfo[0];
                ifrmPayMentInfo.DtInvoiceStatDetail = dtInvoiceStatDetail;
                return true;
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);

                //HIS收费矢败 有医保结算的需取消医保结算
                if (ifrmPayMentInfo.IsMediaPat && ifrmPayMentInfo.MediaInfo != string.Empty)
                {
                    //调用医保接口取消正式结算
                    MIOPInterface.MIRefundBalance(ifrmBalance.CurInvoiceNO);
                }

                return false;
            }
        }

        /// <summary>
        /// 票据打印
        /// </summary>
        [WinformMethod]
        public void BalancePrint()
        {
            try
            {
                DataTable dt = ifrmPayMentInfo.DtPrintInvoice;
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBoxShowError("收费成功，获取票据数据失败，请到票据补打处补打票据");
                    return;
                }

                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "BalanceController", "GetBillPrintType");
                int printtype = retdata.GetData<int>(0);
                BalancePrint print = new BalancePrint();
                if (printtype == 0)
                {
                    //按项目分类打印
                    print.BillPrintStatDetail(ifrmPayMentInfo.DtPrintInvoice, ifrmPayMentInfo.DtPrintInvoiceDetail, ifrmPayMentInfo.DtInvoiceStatDetail, ifrmPayMentInfo.BackChargeInfo, LoginUserInfo.WorkName);
                }
                else if (printtype == 1)
                {
                    //按明细打印
                    print.BillPrintDetail(ifrmPayMentInfo.DtPrintInvoice, ifrmPayMentInfo.DtPrintInvoiceDetail, ifrmPayMentInfo.DtInvoiceStatDetail, ifrmPayMentInfo.BackChargeInfo, LoginUserInfo.WorkName);
                }
                else
                {
                    return;
                }
            }
            catch (Exception err)
            {
                MessageBoxShowError("收费成功，票据打印失败，请到票据补打处补打票据");
                return;
            }
        }

        /// <summary>
        /// 费用清单打印
        /// </summary>
        [WinformMethod]
        public void PresFeePrintDetail()
        {
            BalancePrint print = new BalancePrint();
            print.PresFeePrintDetail(ifrmPayMentInfo.DtPrintInvoice, ifrmPayMentInfo.DtPrintInvoiceDetail, ifrmPayMentInfo.DtInvoiceStatDetail, ifrmPayMentInfo.BackChargeInfo, LoginUserInfo.WorkName);
        }

        /// <summary>
        /// 收费成功后重新获取病人处方
        /// </summary>
        [WinformMethod]
        public void BalanceComplete()
        {
            GetPrescriptions();
            if (ifrmBalance.Prescriptions == null || ifrmBalance.Prescriptions.Rows.Count == 0)
            {
                ClearInfo();
                ifrmBalance.CardNoFocus();
            }

            ifrmBalance.SetexPlLastPayExpanded = true;
        }
        #endregion

        #region 退费
        /// <summary>
        /// 退费窗口
        /// </summary>
        /// <returns>DialogResult</returns>
        [WinformMethod]
        public DialogResult GetRefundInfo()
        {
            GetRefundCostInfo();
            var dialog = iBaseView["FrmRefundInfo"] as Form;
            if ( dialog==null)
            {
                return DialogResult.None;
            }

            return dialog.ShowDialog();            
        }

        /// <summary>
        /// 获取原来结算信息
        /// </summary>
        private void GetRefundCostInfo()
        {
            DataTable dtPresc = ifrmBalance.Prescriptions;
            int costHeadID = Convert.ToInt32(dtPresc.Rows[0]["CostHeadID"]);           
            var retdata = InvokeWcfService(
                "OPProject.Service",
                "BalanceController",
                "RefundInit",
                (request) =>
                {
                    request.AddData(costHeadID);                    
                });
            bool isMediaPat = retdata.GetData<bool>(0);
            List<OP_CostPayMentInfo> listPayInfo = retdata.GetData<List<OP_CostPayMentInfo>>(1);
            OP_CostHead costHead = retdata.GetData<OP_CostHead>(2);
            int refundPosType = retdata.GetData<int>(3);
            ifrmRefundInfo.CostTotalFee = costHead.TotalFee.ToString(".00");
            if (refundPosType == 0)
            {
                ifrmRefundInfo.StrRefundCash = (costHead.CashFee + costHead.PosFee + costHead.RoundingFee).ToString("0.00");
            }
            else if (refundPosType == 1)
            {
                ifrmRefundInfo.StrRefundCash = (costHead.CashFee + costHead.RoundingFee).ToString();
            }
            ifrmRefundInfo.RefundCostHeadID = costHead.CostHeadID;
            StringBuilder sb = new StringBuilder();          
            foreach (OP_CostPayMentInfo payinfo in listPayInfo)
            {
                sb.Append(payinfo.PayMentName+":"+payinfo.PayMentMoney + "\n");
            }

            ifrmRefundInfo.StrPayInfo = sb.ToString();
            ifrmRefundInfo.IsMediaPat = isMediaPat;
        }

        /// <summary>
        /// 退费时读取医保卡
        /// </summary>
        [WinformMethod]
        public void RefundReadMediaCard()
        {
            //调用医保读卡和试算接口
            PatientInfo miPatInfo = MIOPInterface.ReadMediaCard();
            if (!MIOPInterface.PatCheck(ifrmBalance.CurPatList.PatName, ifrmBalance.CurPatList.MedicareCard, ifrmBalance.CurPatList.IDNumber, miPatInfo))
            {
                return;
            }

            ifrmRefundInfo.StrMediaReadInfo = miPatInfo.ShowMiPatInfo;
        }

        /// <summary>
        /// 提交退费
        /// </summary>
        /// <returns>返回是否成功</returns>
        [WinformMethod]
        public bool RefundFee()
        {
            if (ifrmRefundInfo.IsMediaPat)
            {
                //调用医保退费接口 如果调用失败，则return false
                try
                {
                    MIOPInterface.MIRefundBalance(ifrmRefundInfo.RefundInvoiceNO);
                }
                catch (Exception err)
                {
                    MessageBoxShow(err.Message, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return false;
                }
            }

            try
            {
                DataTable dtPresc = ifrmBalance.Prescriptions;
                List<Prescription> prescriptions = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<Prescription>(dtPresc);
                List<Prescription> refundPres = prescriptions.Where(p => p.Selected == 1 && p.SubTotalFlag == 0).ToList();
           
                //获取退费处方信息
                DataTable dtRefund = ifrmBalance.Prescriptions;
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    //退费处方
                    request.AddData(refundPres);
                    request.AddData(LoginUserInfo.EmpId);
                    request.AddData(ifrmRefundInfo.RefundCostHeadID);
                    request.AddData(ifrmRefundInfo.RefundInvoiceNO);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "BalanceController", "RefundFee", requestAction);
                bool isAllRefund = retdata.GetData<bool>(0);
                List<Prescription> balancePresc = retdata.GetData<List<Prescription>>(1);
                ifrmRefundInfo.IsRefundAll = isAllRefund;
                if (isAllRefund)
                {
                    MessageBoxShow("全退成功", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return true;
                }
                else
                {
                    ifrmRefundInfo.BalancePresc = balancePresc;
                    MessageBoxShow("全退成功,有部分退,请在收银窗口补收", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return true;
                }
            }
            catch (Exception err)
            {
                MessageBoxShow(err.Message, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return false;
            }
        }

        /// <summary>
        /// 全退补失败后刷新门诊收费界面，显示未补收的处方信息
        /// </summary>
        [WinformMethod]
        public void RefundBalanceFaild()
        {
            MessageBoxShow("部分退取消补收,可继续点【处方收银】补收", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            string content = ifrmBalance.CurPatList.VisitNO;
            ClearInfo();
            GetPatListByAccountNO(OP_Enum.MemberQueryType.门诊就诊号, content);
        }
        #endregion
    }
}
