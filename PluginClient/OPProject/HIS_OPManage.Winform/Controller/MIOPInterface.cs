using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static HIS_Entity.MIManage.Common.JsonUtil;
using HIS_Entity.ClinicManage;
using HIS_Entity.MIManage;
using HIS_Entity.OPManage;
using HIS_MIInterface.Interface;

namespace HIS_OPManage.Winform.Controller
{
    public static  class MIOPInterface
    {
        /// <summary>
        /// 读医保卡
        /// </summary>
        /// <returns>PatientInfo</returns>
        public static PatientInfo ReadMediaCard()
        {
            HIS_Entity.MIManage.InputClass inputClasss = new HIS_Entity.MIManage.InputClass();
            Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
            inputClasss.SInput = dicStr;
            HIS_Entity.MIManage.ResultClass resultClass = MIInterFaceFactory.MZ_GetPersonInfo(inputClasss);
            if (resultClass.bSucess == true)
            {
                if (resultClass.sRemarks != string.Empty)
                {
                    MessageBox.Show(resultClass.sRemarks);
                }

                List<PatientInfo> patientInfoList = (List<PatientInfo>)resultClass.oResult;
                string patientState = string.Empty;
                if (patientInfoList[0].IsSpecifiedHosp.Trim() != string.Empty)
                {
                    patientState = patientInfoList[0].IsSpecifiedHosp.Contains("0") ? "本地红名单" : (patientInfoList[0].IsSpecifiedHosp.Contains("1") ? "本人定点医院" : (patientInfoList[0].IsSpecifiedHosp.Contains("2") ? "不是本人定点医院" : "转诊"));
                }

                string MedicardInfo = "类型:" + patientInfoList[0].FundType +
                                      "姓名:" + patientInfoList[0].PersonName +
                                      " 卡号:" + patientInfoList[0].CardNo +
                                      " 余额:" + patientInfoList[0].PersonCount +
                                      " 定点医院:" + patientState +
                                      " 红名单:" + patientInfoList[0].IsInredList +
                                      " 身份证号码:" + patientInfoList[0].IdNo;
                
                if (patientInfoList[0].IsInredList.Contains("false"))
                {
                    MessageBox.Show("卡号:" + patientInfoList[0].CardNo + " 姓名:" + patientInfoList[0].PersonName + " 定点医院:" + patientState + " 该病人非本院红名单！");
                }

                if (patientInfoList[0].HospFlag.Trim() == "1")
                {
                    MessageBox.Show("卡号:" + patientInfoList[0].CardNo + " 姓名:" + patientInfoList[0].PersonName + " 定点医院:" + patientState + " 该病人已在院！");
                }

                patientInfoList[0].ShowMiPatInfo = MedicardInfo;//界面显示信息
                return patientInfoList[0];
            }
            else
            {
                throw new Exception("异常！" + resultClass.sRemarks);
            }
        }

        /// <summary>
        /// HIS病人信息和医保卡信息对比
        /// </summary>
        /// <param name="opPatName">病人信息</param>
        /// <param name="opMediCard">病人医保卡号</param>
        /// <param name="opIDNumber">病人身份证号</param>
        /// <param name="miPatInfo">医保病人对象</param>
        /// <returns>true通过false不通过</returns>
        public static bool PatCheck(string opPatName,string opMediCard,string opIDNumber,PatientInfo miPatInfo)
        {
            if (!opPatName.Equals(miPatInfo.PersonName))
            {
                MessageBox.Show("医保卡信息与院内信息不一致：姓名不符！请修改院内信息！");
                return false;
            }

            if (!opMediCard.Equals(miPatInfo.CardNo))
            {
                MessageBox.Show("卡号与原卡不符！");
                return false;
            }

            if (!opIDNumber.Equals(miPatInfo.IdNo))
            {
                MessageBox.Show("医保卡信息与院内信息不一致：身份证不符！请修改院内信息！");
                return false;
            }

            return true;
        }

        #region 医保挂号
        /// <summary>
        /// 医保挂号预算
        /// </summary>
        /// <param name="userName">操作员姓名</param>
        /// <param name="empID">操作员ID</param>
        /// <param name="curPatList">当前病人挂号对象</param>
        /// <param name="totalFee">挂号总金额</param>
        /// <param name="dtRegInfo"></param>
        /// <param name="invoiceNO">发票号</param>
        public static Dictionary<string, string> MiRegBuget(string sCardNum,string userName,int empID,OP_PatList curPatList,decimal totalFee, DataTable dtRegInfo,string invoiceNO,string IdentityNum)
        {
            MI_Register register = new MI_Register();
            register.StaffName = userName;
            register.RegTime = System.DateTime.Now;
            register.StaffID = empID.ToString();
            register.BedNo = string.Empty;
            register.ICDCode = string.Empty;
            register.DiagnDocID = curPatList.CureEmpID.ToString();
            register.DiagnosisName = string.Empty;
            register.SocialCreateNum = string.Empty;
            register.DeptID = curPatList.RegDeptID;
            register.DeptName = curPatList.RegDeptName;
            register.Doctor = curPatList.RegDocName;
            register.PatientName = curPatList.PatName;
            register.GHFee = totalFee;
            register.JCFee = 0;
            register.MedicalClass = curPatList.PatTypeID.ToString();
            register.SerialNO = string.Empty;
            register.PersonalCode = sCardNum;
            register.IdentityNum = IdentityNum;

            Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
            dicStr.Clear();
            dicStr.Add(InputType.MI_Register, JsonHelper.SerializeObject(register));

            #region 增加挂号明细数据
            DataTable dt = new DataTable();
            DataColumn dcItemCode = new DataColumn("ItemCode", Type.GetType("System.String"));
            DataColumn dcItemName = new DataColumn("ItemName", Type.GetType("System.String"));
            DataColumn dcPrice = new DataColumn("Price", Type.GetType("System.String"));
            DataColumn dcCount = new DataColumn("Count", Type.GetType("System.String"));
            DataColumn dcFee = new DataColumn("Fee", Type.GetType("System.String"));

            dt.Columns.Add(dcItemCode);
            dt.Columns.Add(dcItemName);
            dt.Columns.Add(dcPrice);
            dt.Columns.Add(dcCount);
            dt.Columns.Add(dcFee);

            foreach (DataRow dr in dtRegInfo.Rows)
            {
                DataRow drNew = dt.NewRow();
                drNew["ItemCode"] = dr["itemid"];
                drNew["ItemName"] = dr["itemname"];
                drNew["Price"] = dr["sellprice"];
                drNew["Count"] = "1";
                drNew["Fee"] = dr["sellprice"];
                dt.Rows.Add(drNew);
            }           
            dicStr.Add(InputType.DataTable, dt);
            #endregion
            dicStr.Add(InputType.InvoiceNo, invoiceNO);

            InputClass input = new InputClass();
            input.SInput = dicStr;

            ResultClass resultClass = MIInterFaceFactory.MZ_PreviewRegister(input);
            if (resultClass.bSucess)
            {
                Dictionary<string, string> resultDic = (Dictionary<string, string>)resultClass.oResult;
                Dictionary<string, string> myDic = new Dictionary<string, string>();
                myDic.Add("ID", resultDic["Id"]);//医保预结算ID
                decimal medicarepay = Convert.ToDecimal(resultDic["fund"]) + Convert.ToDecimal(resultDic["personcountpay"]);
                myDic.Add("MedicarePay", medicarepay.ToString("0.00"));
                myDic.Add("MedicareMIPay", Convert.ToDecimal(resultDic["fund"]).ToString("0.00"));
                myDic.Add("MedicarePersPay", Convert.ToDecimal(resultDic["personcountpay"]).ToString("0.00"));
                StringBuilder strBuild = new StringBuilder();
                strBuild.Append("统筹支付:" + Convert.ToDecimal(resultDic["fund"]).ToString("0.00") + "\n");
                strBuild.Append("现金支付:" + Convert.ToDecimal(resultDic["cash"]).ToString("0.00") + "\n");
                strBuild.Append("个帐支付:" + Convert.ToDecimal(resultDic["personcountpay"]).ToString("0.00") + "\n");

                myDic.Add("MedicardInfo", strBuild.ToString());
                return myDic;
            }
            else
            {
                throw new Exception("异常！" + resultClass.sRemarks);
            }
        }
  
        /// <summary>
        /// 医保挂号正式结算
        /// </summary>
        /// <param name="registerId">医保预结算ID</param>
        /// <param name="serialNO">门诊号 不填写</param>
        /// <returns></returns>
        public static Dictionary<string, string> MiRegister(int registerId, string serialNO)
        {
            Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();            
            dicStr.Add(InputType.RegisterId, registerId);
            dicStr.Add(InputType.SerialNO, serialNO);
            InputClass input = new InputClass();
            input.SInput = dicStr;
            ResultClass resultClass = MIInterFaceFactory.MZ_Register(input);
            if (resultClass.bSucess)
            {
                Dictionary<string, string> resultDic = (Dictionary<string, string>)resultClass.oResult;
                MessageBox.Show("医保卡余额:" + resultDic["personcount"]); 
                return resultDic;
            }
            else
            {
                throw new Exception("异常！" + resultClass.sRemarks);
            }
        }

        /// <summary>
        /// 挂号完成后回写门诊就诊号
        /// </summary>
        /// <param name="regisgetID">医保预结算ID</param>
        /// <param name="serialNO">门诊就诊号</param>
        /// <param name="invoiceNo">票据号</param>
        public static void MiRegisterComplete(int regisgetID, string serialNO, string invoiceNo)
        {
            InputClass input = new InputClass();
            Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
            dicStr.Add(InputType.RegisterId, regisgetID);
            dicStr.Add(InputType.SerialNO, serialNO);
            dicStr.Add(InputType.InvoiceNo, invoiceNo);
            input.SInput = dicStr;
            ResultClass resultClass = MIInterFaceFactory.MZ_UpdateRegister(input);
            if (resultClass.bSucess)
            {
                MessageBox.Show("医保卡余额" + resultClass.sRemarks);
            }
            else
            {
                throw new Exception("异常！" + resultClass.sRemarks);
            }
        }

        /// <summary>
        /// 挂号退号
        /// </summary>
        /// <param name="visitNO">就诊号</param>
        /// <param name="budgeid">预算ID</param>
        /// <param name="invoiceNO">票据号</param>
        /// <returns></returns>
        public static bool MiRefundRegister(string visitNO,string budgeid,string invoiceNO)
        {
            InputClass input = new InputClass();
            Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
            if (!string.IsNullOrEmpty(visitNO))
            {
                dicStr.Add(InputType.SerialNO, visitNO);
            }

            if (!string.IsNullOrEmpty(budgeid))
            {
                dicStr.Add(InputType.RegisterId, budgeid);
            }

            if (!string.IsNullOrEmpty(invoiceNO))
            {
                dicStr.Add(InputType.InvoiceNo, invoiceNO);
            }
            input.SInput = dicStr;
            ResultClass resultClass= MIInterFaceFactory.MZ_CancelRegister(input);
            if (resultClass.bSucess)
            {
                MessageBox.Show("医保卡余额" + resultClass.sRemarks);
                return true;
            }
            else
            {
                throw new Exception("异常！" + resultClass.sRemarks);
            }
        }
        #endregion

        #region 医保收费
        /// <summary>
        /// 医保收费预算
        /// </summary>
        /// <param name="workid"></param>
        /// <param name="presList">处方数据</param>
        /// <param name="curPatList">病人对象</param>
        /// <param name="costPatTypeid">收费病人类型</param>
        /// <param name="diagnosisList">诊断信息</param>
        /// <param name="invoiceNO">发票号</param>
        /// <returns></returns>
        public static Dictionary<string, string> MIBalanceBuget(int workid,List<Prescription> presList,OP_PatList curPatList,int costPatTypeid,List<OPD_DiagnosisRecord> diagnosisList,string invoiceNO)
        {
            ResultClass resultClass = new ResultClass();
            string sSocialCreateNumSocialCreateNum = string.Empty;

            resultClass = MIInterFaceFactory.Mz_GetRegisterTradeNo(curPatList.VisitNO);
            if (resultClass.bSucess)
            {
                sSocialCreateNumSocialCreateNum = resultClass.sRemarks;
            }
            else
            {
                throw new Exception("异常！" + resultClass.sRemarks);
            }
            InputClass input = new InputClass();
            Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();           
            TradeData tradeData = new TradeData();
            Tradeinfo tradeinfo = new Tradeinfo();
            RecipeList recipeList = new RecipeList();
            FeeitemList feeitemList = new FeeitemList();
            tradeinfo.tradeType = TradeType.普通门诊;
            tradeinfo.billtype = "0";
            tradeinfo.feeno = invoiceNO;

            List<Recipe> recipes = new List<Recipe>();
            List<Feeitem> feeitems = new List<Feeitem>();
            int diagnoseCount = diagnosisList.Count > 3 ? 3 : diagnosisList.Count;//最多取三条诊断
            List<Prescription> feeItemHeadIDList = GetPresHeadList(presList);
            for (int i = 0; i < diagnoseCount; i++)
            {
                int diagNo = i + 1;
                foreach (Prescription pres in feeItemHeadIDList)
                {
                    Recipe recipe = new Recipe();

                    recipe.diagnoseno = diagNo.ToString();
                    recipe.recipeno = pres.FeeItemHeadID.ToString();
                    recipe.recipedate = DateTime.Now.ToString("yyyyMMddHHmmss");
                    recipe.drid = "0999";
                    recipe.drname = curPatList.RegDocName.ToString();
                    recipe.sectioncode = curPatList.RegDeptID.ToString();
                    recipe.sectionname = curPatList.RegDeptName.ToString();
                    recipe.hissectionname = curPatList.RegDeptName.ToString();
                    recipe.diagnosecode = diagnosisList[i].DiagnosisCode.Equals("")?"0000": diagnosisList[i].DiagnosisCode;
                    recipe.diagnosename = diagnosisList[i].DiagnosisName;
                    recipe.registertradeno = sSocialCreateNumSocialCreateNum;
                    recipe.recipetype = pres.IsReimbursement==1?"2":"1";
                    if (pres.StatID == 100 || pres.StatID == 101)
                    {
                        recipe.billstype = "2";
                    }
                    else if (pres.StatID == 102)
                    {
                        recipe.billstype = "4";
                    }
                    else
                    {
                        recipe.billstype = "5";
                    }
                    recipes.Add(recipe);
                }
            }

            foreach (Prescription pres in presList)
            {
                if (pres.SubTotalFlag == 0 && pres.Selected == 1)
                {
                    Feeitem feeitem = new Feeitem();
                    feeitem.itemno = pres.PresDetailID.ToString();// dr["PresDetailID"].ToString();
                    feeitem.recipeno = pres.FeeItemHeadID.ToString();
                    feeitem.hiscode = pres.ItemID.ToString();
                    if (pres.StatID == 102)
                    {
                        feeitem.itemname = "中药饮片及药材";
                        feeitem.specification = pres.ItemName.ToString().Trim();
                        feeitem.unitprice = (pres.RetailPrice/pres.UnitNO).ToString("0.000");
                        feeitem.count = (pres.TotalFee* pres.UnitNO / pres.RetailPrice).ToString("0.00"); //pres.Amount.ToString();
                        decimal totalfee =Convert.ToDecimal( feeitem.unitprice)* Convert.ToDecimal( feeitem.count);

                        feeitem.unit = pres.MiniUnit;
                        feeitem.days = pres.PresAmount.ToString();
                        feeitem.fee = totalfee.ToString("0.00");

                        feeitem.packaging = pres.MiniUnit;
                        feeitem.minpackage = pres.MiniUnit;
                        feeitem.conversion = "1";
                    }
                    else if(pres.ItemType.Equals("1"))
                    {
                        feeitem.itemname = pres.ItemName.ToString().Trim();
                        feeitem.specification = pres.Spec.ToString();

                        feeitem.unitprice = pres.RetailPrice.ToString("0.000");
                        feeitem.count = (pres.TotalFee / pres.RetailPrice).ToString("0.00"); //pres.Amount.ToString();
                        feeitem.unit = pres.PackUnit;
                        feeitem.days = pres.Days.ToString();
                        feeitem.fee = pres.TotalFee.ToString("0.00");
                        feeitem.packaging = pres.PackUnit;
                        feeitem.minpackage = pres.DosageUnit;
                        feeitem.conversion = (Convert.ToDecimal(pres.Factor)* pres.UnitNO).ToString();
                        //待测试的
                        //feeitem.unitprice = (pres.RetailPrice / pres.UnitNO).ToString("0.000");
                        //feeitem.count = (pres.TotalFee * pres.UnitNO / pres.RetailPrice).ToString("0.00"); //pres.Amount.ToString();
                        //decimal totalfee = Convert.ToDecimal(feeitem.unitprice) * Convert.ToDecimal(feeitem.count);

                        //feeitem.unit = pres.MiniUnit;
                        //feeitem.days = pres.Days.ToString();
                        //feeitem.fee = totalfee.ToString("0.00");
                    }
                    else
                    {
                        feeitem.itemname = pres.ItemName.ToString().Trim();
                        feeitem.specification = pres.Spec.ToString();

                        feeitem.unitprice = pres.RetailPrice.ToString("0.000");
                        feeitem.count = (pres.TotalFee / pres.RetailPrice).ToString("0.00"); //pres.Amount.ToString();
                        feeitem.unit = pres.PackUnit;
                        feeitem.days = pres.Days.ToString();
                        feeitem.fee = pres.TotalFee.ToString("0.00");
                        feeitem.packaging = feeitem.unit;
                        feeitem.minpackage = feeitem.unit;
                        feeitem.conversion = "1";
                        //待测试的
                        //feeitem.unitprice = (pres.RetailPrice / pres.UnitNO).ToString("0.000");
                        //feeitem.count = (pres.TotalFee * pres.UnitNO / pres.RetailPrice).ToString("0.00"); //pres.Amount.ToString();
                        //decimal totalfee = Convert.ToDecimal(feeitem.unitprice) * Convert.ToDecimal(feeitem.count);

                        //feeitem.unit = pres.MiniUnit;
                        //feeitem.days = pres.Days.ToString();
                        //feeitem.fee = totalfee.ToString("0.00");
                    }

                    feeitem.itemtype = Convert.ToInt32(pres.ItemType) == Convert.ToInt32(OP_Enum.ItemType.药品) ? "0" : "1";
                    
                    feeitem.babyflag = "0";
                    
                    feeitem.dosage = pres.Dosage.ToString(); 
                    feeitem.dose = pres.DosageId.ToString();
                    
                    feeitem.howtouse = pres.FrequencyID.ToString();//"02";//pres.FrequencyName;
                    

                    if (feeitem.itemtype == "0")
                    {
                        feeitem.drugapprovalnumber = pres.DrugApprovalnumber;
                    }
                    else
                    {
                        feeitem.drugapprovalnumber = string.Empty;
                    }
                    feeitems.Add(feeitem);
                }
            }
            feeitemList.feeitems = feeitems;
            recipeList.recipes = recipes;
            #region 新版，诊断分类型增加
            //List<Recipe> recipes = new List<Recipe>();
            //List<Feeitem> feeitems = new List<Feeitem>();
            ////1按项目类型分类
            //var fL = from item in presList
            //         group item by new { item.ItemType } into g
            //        select new
            //        {
            //            ItemType = g.Key.ItemType
            //        };
            //int recCount = 0;
            ////2.分类型插入诊断和项目
            //foreach (var f in fL)
            //{
            //    //2.1添加诊断
            //    Recipe recipe = new Recipe();
            //    recipe.diagnoseno = recCount.ToString();
            //    recipe.recipeno = recCount.ToString();
            //    recipe.recipedate = DateTime.Now.ToString("yyyyMMddHHmmss");
            //    recipe.drid = "0999";//curPatList.RegEmpID.ToString();
            //    recipe.drname = curPatList.RegDocName.ToString();
            //    recipe.sectioncode = curPatList.RegDeptID.ToString();
            //    recipe.sectionname = curPatList.RegDeptName.ToString();
            //    recipe.hissectionname = curPatList.RegDeptName.ToString();
            //    recipe.diagnosecode = curPatList.DiseaseCode;
            //    recipe.diagnosename = curPatList.DiseaseName;
            //    recipe.registertradeno = sSocialCreateNumSocialCreateNum;
            //    recipe.billstype = f.ItemType.Equals("1") ? "2" : "5";
            //    recipes.Add(recipe);

            //    //2.2添加明细
            //    List<Prescription> PrescriptionList = presList.FindAll(x => x.ItemType == f.ItemType);
            //    foreach (Prescription pres in PrescriptionList)
            //    {
            //        if (pres.SubTotalFlag == 0 && pres.Selected == 1)
            //        {
            //            Feeitem feeitem = new Feeitem();
            //            feeitem.itemno = pres.PresDetailID.ToString();// dr["PresDetailID"].ToString();
            //            feeitem.recipeno = recCount.ToString();//pres.FeeItemHeadID.ToString();
            //            feeitem.hiscode = pres.ItemID.ToString();
            //            feeitem.itemname = pres.ItemName.ToString().Trim();
            //            feeitem.itemtype = Convert.ToInt32(pres.ItemType) == Convert.ToInt32(OP_Enum.ItemType.药品) ? "0" : "1";
            //            feeitem.unitprice = pres.RetailPrice.ToString("0.000");
            //            feeitem.count = (pres.TotalFee / pres.RetailPrice).ToString("0.00"); //pres.Amount.ToString();
            //            feeitem.fee = pres.TotalFee.ToString("0.00");
            //            feeitem.babyflag = "0";

            //            feeitem.dosage = pres.Dosage;
            //            feeitem.dose = pres.DosageName;
            //            feeitem.days = pres.Days.ToString();
            //            feeitem.howtouse = pres.FrequencyName;
            //            feeitem.specification = pres.Spec;
            //            feeitem.unit = pres.MiniUnit;


            //            if (feeitem.itemtype == "0")
            //            {
            //                feeitem.drugapprovalnumber = pres.DrugApprovalnumber;
            //            }
            //            else
            //            {
            //                feeitem.drugapprovalnumber = string.Empty;
            //            }
            //            feeitems.Add(feeitem);
            //        }
            //    }

            //    recCount += 1;
            //}
            //recipeList.recipes = recipes;
            //feeitemList.feeitems = feeitems;
            #endregion

            #region 老版，只传一个诊断
            //List<Recipe> recipes = new List<Recipe>();
            //Recipe recipe = new Recipe();
            //recipe.diagnoseno = "1";
            //recipe.recipeno = "1";
            //recipe.recipedate = DateTime.Now.ToString("yyyyMMddHHmmss");
            //recipe.drid = "0999";//curPatList.RegEmpID.ToString();
            //recipe.drname = curPatList.RegDocName.ToString();
            //recipe.sectioncode = curPatList.RegDeptID.ToString();
            //recipe.sectionname = curPatList.RegDeptName.ToString();
            //recipe.hissectionname = curPatList.RegDeptName.ToString();
            //recipe.diagnosecode = curPatList.DiseaseCode;
            //recipe.diagnosename = curPatList.DiseaseName;
            //recipe.registertradeno = string.Empty;
            //recipes.Add(recipe);
            //recipeList.recipes = recipes;

            //List<Feeitem> feeitems = new List<Feeitem>();
            //foreach (Prescription pres in presList)
            //{
            //    if (pres.SubTotalFlag == 0 && pres.Selected==1)
            //    {
            //        Feeitem feeitem = new Feeitem();
            //        feeitem.itemno = pres.PresDetailID.ToString();// dr["PresDetailID"].ToString();
            //        feeitem.recipeno = "1";//pres.FeeItemHeadID.ToString();
            //        feeitem.hiscode = pres.ItemID.ToString();
            //        feeitem.itemname = pres.ItemName.ToString().Trim();
            //        feeitem.itemtype = Convert.ToInt32(pres.ItemType) == Convert.ToInt32(OP_Enum.ItemType.药品) ? "0" : "1";
            //        feeitem.unitprice = pres.RetailPrice.ToString("0.000");
            //        feeitem.count = (pres.TotalFee / pres.RetailPrice).ToString("0.00"); //pres.Amount.ToString();
            //        feeitem.fee = pres.TotalFee.ToString("0.00");
            //        feeitem.babyflag = "0";

            //        if (feeitem.itemtype == "0")
            //        {
            //            feeitem.drugapprovalnumber = pres.DrugApprovalnumber;
            //        }
            //        else
            //        {
            //            feeitem.drugapprovalnumber = string.Empty;
            //        }
            //        feeitems.Add(feeitem);
            //    }
            //}
            //feeitemList.feeitems = feeitems;
            #endregion

            tradeData.MIID = costPatTypeid;
            tradeData.SerialNo = curPatList.VisitNO;

            tradeData.tradeinfo = tradeinfo;
            tradeData.recipeList = recipeList;
            tradeData.feeitemList = feeitemList;

            dicStr.Add(InputType.TradeData, JsonHelper.SerializeObject(tradeData));
            dicStr.Add(InputType.bFlag, true);
            input.SInput = dicStr;
            resultClass= MIInterFaceFactory.MZ_PreviewCharge(input);
            if (resultClass.bSucess)
            {
                Dictionary<string, string> resultDic = (Dictionary<string, string>)resultClass.oResult;
                Dictionary<string, string> myDic = new Dictionary<string, string>();
                myDic.Add("ID", resultDic["Id"]);//医保预结算ID
                decimal medicarepay = Convert.ToDecimal(resultDic["fund"]) + Convert.ToDecimal(resultDic["personcountpay"]);
                myDic.Add("MedicarePay", medicarepay.ToString("0.00"));
                myDic.Add("MedicareMIPay", Convert.ToDecimal(resultDic["fund"]).ToString("0.00"));
                myDic.Add("MedicarePersPay", Convert.ToDecimal(resultDic["personcountpay"]).ToString("0.00"));
                StringBuilder strBuild = new StringBuilder();
                strBuild.Append("统筹支付:" + Convert.ToDecimal(resultDic["fund"]).ToString("0.00") + " ");
                strBuild.Append("现金支付:" + Convert.ToDecimal(resultDic["cash"]).ToString("0.00") + " ");
                strBuild.Append("个帐支付:" + Convert.ToDecimal(resultDic["personcountpay"]).ToString("0.00") + " ");
                myDic.Add("MedicardInfo", strBuild.ToString());
                if (!resultClass.sRemarks.Equals(""))
                {
                    MessageBox.Show("警告：" + resultClass.sRemarks);
                }
                return myDic;
            }
            else
            {
                throw new Exception("异常！" + resultClass.sRemarks);
            }
        }
       
        /// <summary>
        /// 获取处方号
        /// </summary>
        /// <param name="presList">处方对象</param>
        /// <returns>返回处方号List</returns>
        private static List<Prescription> GetPresHeadList(List<Prescription> presList)
        {
            List<Prescription> feeItemHeadIDList = new List<Prescription>();
            for (int i = 0; i < presList.Count; i++)
            {
                if (presList[i].Selected == 0 || presList[i].SubTotalFlag==1)
                {
                    continue;
                }  
                             
                int feeItemHeadID = Convert.ToInt32(presList[i].FeeItemHeadID);
                if (feeItemHeadIDList.Where(p => p.FeeItemHeadID == feeItemHeadID).ToList().Count == 0)
                {
                    feeItemHeadIDList.Add(presList[i]);
                }
            }

            return feeItemHeadIDList;
        }
      
        /// <summary>
        /// 门诊正式收费
        /// </summary>
        /// <param name="invoiceNO">票据号</param>
        /// <param name="budgetID">预算ID</param>
        /// <returns>返回收费是否成功</returns>
        public static bool MIBalance(string invoiceNO, int budgetID)
        {
            InputClass input = new InputClass();
            Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
            dicStr.Add(InputType.InvoiceNo, invoiceNO);
            dicStr.Add(InputType.TradeRecordId, budgetID);
            input.SInput = dicStr;
            ResultClass resultClass = MIInterFaceFactory.MZ_Charge(input);
            if (resultClass.bSucess)
            {
                MessageBox.Show("医保卡余额" + resultClass.sRemarks);
                return true;
            }
            else
            {              
                throw new Exception("异常！" + resultClass.sRemarks);
            }            
        }
       
        /// <summary>
        ///  医保门诊退费
        /// </summary>
        /// <param name="invoiceNO">退费发票号</param>
        /// <returns>医保退退成功与否true成功false失败</returns>
        public static bool MIRefundBalance(string invoiceNO)
        {
            InputClass input = new InputClass();
            Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
            dicStr.Add(InputType.InvoiceNo, invoiceNO);           
            input.SInput = dicStr;
            ResultClass resultClass = MIInterFaceFactory.MZ_CancelCharge(input);
            if (resultClass.bSucess)
            {
                MessageBox.Show("医保卡余额" + resultClass.sRemarks);
                return true;
            }
            else
            {
                throw new Exception("异常！" + resultClass.sRemarks);
            }
        }
        #endregion
    }
}
