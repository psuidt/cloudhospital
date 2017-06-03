using EFWCoreLib.CoreFrame.Common;
using HIS_Entity.MIManage;
using HIS_Entity.MIManage.Common;
using HIS_MIInterface.Interface.BaseClass;
using SiInterfaceDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HIS_Entity.MIManage.Common.JsonUtil;

namespace HIS_MIInterface.Interface.CustomAction.北京
{
    public class CustomAction : AbstractMIAction<AbstractHISDao, CustomMIInterfaceDao>
    {
        public int _WorkId = 0;
        public CustomAction(int WorkId)
        {
            AddLog("北京-医保", Color.Blue);
            _WorkId = WorkId;
            hisDao.WorkId(WorkId);
        }
        SiInterfaceDll sDll;
        private bool IsNew = false;
        #region 门诊
        public override ResultClass MZ_GetCardInfo(InputClass inputClass)
        {
            sDll = new SiInterfaceDll();
            string sCardNo = inputClass.SInput.ContainsKey(InputType.CardNo) ? inputClass.SInput[InputType.CardNo].ToString() : "";
            return ybInterfaceDao.Mz_GetCardInfo(sDll, sCardNo);
        }

        public override ResultClass MZ_GetPersonInfo(InputClass inputClass)
        {
            sDll = new SiInterfaceDll();
            string sCardNo = inputClass.SInput.ContainsKey(InputType.CardNo) ? inputClass.SInput[InputType.CardNo].ToString() : "";
            return ybInterfaceDao.Mz_GetPersonInfo(sDll, sCardNo);
        }

        public override ResultClass MZ_PreviewRegister(InputClass inputClass)
        {
            InputClass input = new InputClass();

            ResultClass resultClass=new ResultClass();
            MI_Register register = JsonHelper.DeserializeJsonToObject<MI_Register>(inputClass.SInput[InputType.MI_Register].ToString());
            try
            {
                resultClass = ybInterfaceDao.MZ_PreviewRegister(sDll, PreviewRegisterToInput(register));
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks ="医保预登记报错："+ e.Message;
                AddLog("医保预登记报错：" + e.Message, Color.Red);
                return resultClass;
            }

            if (resultClass.bSucess)
            {
                try
                {
                    AddLog("医保预登记完成,开始解析医保登记数据", Color.Blue);
                    DivideResult.root root = (DivideResult.root)resultClass.oResult;
                    MI_MedicalInsurancePayRecord medicalInsurancePayRecord = ResultToPayRecord(root.output);
                    MI_MIPayRecordHead mIPayRecordHead = ResultToPayRecordHead(root.output);
                    List<MI_MIPayRecordDetail> mIPayRecordDetailList = ResultToPayRecordDetail(root.output);

                    Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
                    dicStr.Add(InputType.MI_Register, JsonHelper.SerializeObject(register));
                    dicStr.Add(InputType.MI_MedicalInsurancePayRecord, JsonHelper.SerializeObject(medicalInsurancePayRecord));
                    dicStr.Add(InputType.MI_MIPayRecordHead, JsonHelper.SerializeObject(mIPayRecordHead));
                    dicStr.Add(InputType.MI_MIPayRecordDetailList, JsonHelper.SerializeObject(mIPayRecordDetailList));
                    input.SInput = dicStr;
                    AddLog("解析医保预登记数据完成,开始保存数据到HIS数据库", Color.Blue);
                }
                catch (Exception e)
                {
                    resultClass.bSucess = false;
                    resultClass.sRemarks = "解析预登记数据报错：" + e.Message;
                    AddLog("解析预登记数据报错：" + e.Message, Color.Red);
                    return resultClass;
                }

                try
                {
                    if (hisDao != null)
                    {
                        resultClass = hisDao.MZ_PreviewRegister(input);
                    }
                    else
                    {
                        resultClass.bSucess = false;
                        resultClass.sRemarks = "hisDao为空";
                        AddLog("hisDao为空", Color.Red);
                    }
                }
                catch (Exception e)
                {
                    resultClass.bSucess = false;
                    resultClass.sRemarks = "插入预登记数据报错：" + e.Message;
                    AddLog("插入预登记数据报错：" + e.Message, Color.Red);
                    return resultClass;
                }
                return resultClass;
            }
            else
            {
                return resultClass;
            }            
        }

        public override ResultClass MZ_Register(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            decimal personcount = -1;
            //医保登记并返回结果
            try
            {
                resultClass = ybInterfaceDao.MZ_Register(sDll);
                personcount = Tools.ToDecimal(resultClass.oResult.ToString(), 0);
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = "医保登记报错："+e.ToString();
                AddLog("医保登记报错：" + e.Message, Color.Red);
                return resultClass;
            }

            try
            {
                AddLog("医保登记完成,开始解析医保登记数据", Color.Blue);
                int registerId = inputClass.SInput.ContainsKey(InputType.RegisterId) ? Tools.ToInt32(inputClass.SInput[InputType.RegisterId].ToString(), 0) : 0;
                string serialNO = inputClass.SInput.ContainsKey(InputType.SerialNO) ? inputClass.SInput[InputType.SerialNO].ToString() : "";

                InputClass input = new InputClass();
                Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
                dicStr.Add(InputType.RegisterId, registerId);
                dicStr.Add(InputType.SerialNO, serialNO);
                dicStr.Add(InputType.Money, personcount);
                AddLog("解析医保登记数据完成,开始保存数据到HIS数据库", Color.Blue);
                input.SInput = dicStr;
                resultClass = hisDao.MZ_Register(input);
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = "更新中间表报错：" + e.ToString();
                AddLog("更新中间表报错：" + e.Message, Color.Red);
                return resultClass;
            }

            return resultClass;

        }

        //public override ResultClass MZ_RegisterRollBack(InputClass inputClass)
        //{
        //    ResultClass resultClass = new ResultClass();
        //    string serialNO = inputClass.SInput.ContainsKey(InputType.SerialNO) ? inputClass.SInput[InputType.SerialNO].ToString() : "";
        //    int registerId = inputClass.SInput.ContainsKey(InputType.RegisterId) ? Convert.ToInt32(inputClass.SInput[InputType.RegisterId]) : -1;
        //    try
        //    {
        //        MI_Register register = hisDao.Mz_Getregister(registerId, serialNO);
        //        if (register == null)
        //        {
        //            resultClass.bSucess = false;
        //            resultClass.sRemarks = "获取登记信息失败！";
        //            AddLog("获取HIS登记信息失败！", Color.Red);
        //        }
        //        else
        //        {
        //            //取消医保登记并返回结果
        //            resultClass = ybInterfaceDao.Mz_CancelRegister(sDll, register.SocialCreateNum);
        //            DivideResult.root root = (DivideResult.root)resultClass.oResult;
        //            //解析返回数据，并保存退费记录
        //            MI_MedicalInsurancePayRecord medicalInsurancePayRecord = ResultToPayRecord(root.output);
        //            MI_MIPayRecordHead mIPayRecordHead = ResultToPayRecordHead(root.output);
        //            List<MI_MIPayRecordDetail> mIPayRecordDetailList = ResultToPayRecordDetail(root.output);
        //            register.ValidFlag = 2;
        //            medicalInsurancePayRecord.state = 2;
        //            medicalInsurancePayRecord.RegId = register.ID;
        //            medicalInsurancePayRecord.TradeType = 1;

        //            InputClass input = new InputClass();
        //            Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
        //            dicStr.Add(InputType.MI_Register, JsonHelper.SerializeObject(register));
        //            dicStr.Add(InputType.MI_MedicalInsurancePayRecord, JsonHelper.SerializeObject(medicalInsurancePayRecord));
        //            dicStr.Add(InputType.MI_MIPayRecordHead, JsonHelper.SerializeObject(mIPayRecordHead));
        //            dicStr.Add(InputType.MI_MIPayRecordDetailList, JsonHelper.SerializeObject(mIPayRecordDetailList));
        //            input.SInput = dicStr;
        //            resultClass = hisDao.Mz_CancelRegister(input);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        resultClass.bSucess = false;
        //        resultClass.sRemarks = e.Message;
        //        AddLog("回滚登记报错：" + e.Message, Color.Red);
        //    }
        //    return resultClass;
        //}

        public override ResultClass MZ_UpdateRegister(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                int registerId = inputClass.SInput.ContainsKey(InputType.RegisterId) ? Tools.ToInt32(inputClass.SInput[InputType.RegisterId].ToString(), 0) : -1;
                string serialNO = inputClass.SInput.ContainsKey(InputType.SerialNO) ? inputClass.SInput[InputType.SerialNO].ToString() : "";

                InputClass input = new InputClass();
                Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
                dicStr.Add(InputType.RegisterId, registerId);
                dicStr.Add(InputType.SerialNO, serialNO);

                input.SInput = dicStr;
                resultClass = hisDao.MZ_UpdateRegister(input);
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = "更新中间表报错：" + e.ToString();
                AddLog("更新中间表报错：" + e.Message, Color.Red);
                return resultClass;
            }

            return resultClass;

        }
        public override ResultClass MZ_CancelRegister(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            int registerId = inputClass.SInput.ContainsKey(InputType.RegisterId) ? Tools.ToInt32(inputClass.SInput[InputType.RegisterId].ToString(), -1) : -1;
            string serialNO = inputClass.SInput.ContainsKey(InputType.SerialNO) ? inputClass.SInput[InputType.SerialNO].ToString() : "";
            try
            {
                MI_Register register = hisDao.Mz_Getregister(registerId, serialNO);
                if (register == null)
                {
                    resultClass.bSucess = false;
                    resultClass.sRemarks = "获取登记信息失败！";
                    AddLog("获取HIS登记信息失败！", Color.Red);
                }
                else
                {
                    bool b = hisDao.Mz_JudgeRegister(register.ID);
                    if (b)
                    {
                        //取消医保登记并返回结果
                        resultClass = ybInterfaceDao.Mz_CancelRegister(sDll,register.SocialCreateNum);
                        DivideResult.root root = (DivideResult.root)resultClass.oResult;
                        //解析返回数据，并保存退费记录
                        MI_MedicalInsurancePayRecord medicalInsurancePayRecord = ResultToPayRecord(root.output);
                        MI_MIPayRecordHead mIPayRecordHead = ResultToPayRecordHead(root.output);
                        List<MI_MIPayRecordDetail> mIPayRecordDetailList = ResultToPayRecordDetail(root.output);
                        register.ValidFlag = 2;
                        medicalInsurancePayRecord.state = 2;
                        medicalInsurancePayRecord.RegId = register.ID;
                        medicalInsurancePayRecord.TradeType = 1;

                        InputClass input = new InputClass();
                        Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
                        dicStr.Add(InputType.MI_Register, JsonHelper.SerializeObject(register));
                        dicStr.Add(InputType.MI_MedicalInsurancePayRecord, JsonHelper.SerializeObject(medicalInsurancePayRecord));
                        dicStr.Add(InputType.MI_MIPayRecordHead, JsonHelper.SerializeObject(mIPayRecordHead));
                        dicStr.Add(InputType.MI_MIPayRecordDetailList, JsonHelper.SerializeObject(mIPayRecordDetailList));
                        input.SInput = dicStr;
                        resultClass = hisDao.Mz_CancelRegister(input);

                    }
                    else
                    {
                        resultClass.bSucess = false;
                        resultClass.sRemarks = "存在已结算的收费数据！";
                        AddLog("HIS存在已结算的收费数据！", Color.Red);
                    }
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
                AddLog("取消登记报错：" + e.Message, Color.Red);
            }
            return resultClass;
        }

        public override ResultClass MZ_PreviewCharge(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            TradeData tradedata = inputClass.SInput.ContainsKey(InputType.TradeData) ?JsonHelper.DeserializeJsonToObject<TradeData>(inputClass.SInput[InputType.TradeData].ToString()) : null;

            MI_Register register = hisDao.Mz_Getregister(-1, tradedata.SerialNo);
            try
            {
                if (register == null)
                {
                    resultClass.bSucess = false;
                    resultClass.sRemarks = "医院编号：" + tradedata.WorkID + ",门诊就诊号：" + tradedata.SerialNo + ",未进行医保统筹登记";
                    //MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Red, "医院编号：" + tradedata.WorkID + ",门诊就诊号：" + tradedata.SerialNo + ",未进行医保统筹登记");
                    AddLog("医院编号：" + tradedata.WorkID + ",门诊就诊号：" + tradedata.SerialNo + ",未进行医保统筹登记",Color.Red);
                }
                else
                {
                    if (register.ValidFlag != 1)
                    {
                        resultClass.bSucess = false;
                        resultClass.sRemarks = "医院编号：" + tradedata.WorkID + ",门诊就诊号：" + tradedata.SerialNo + ",医保统筹登记已取消";
                        //MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Red, "医院编号：" + tradedata.WorkID + ",门诊就诊号：" + tradedata.SerialNo + ",医保统筹登记已取消");
                        AddLog("医院编号：" + tradedata.WorkID + ",门诊就诊号：" + tradedata.SerialNo + ",医保统筹登记已取消", Color.Red);
                    }
                    else
                    {
                        tradedata.SocityCreateNum = register.SocialCreateNum;
                        //医保登记并返回结果                
                        AddLog("医保预算并返回结果开始", Color.Blue);
                        resultClass = ybInterfaceDao.MZ_PreviewCharge(sDll,TradeDataToInput(tradedata));
                    }
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
                AddLog("医保预算报错：", Color.Red);
            }

            try
            {
                AddLog("医保预算并返回结果成功，开始解析", Color.Blue);
                DivideResult.root root = (DivideResult.root)resultClass.oResult;
                //解析返回结果到类，保存
                MI_MedicalInsurancePayRecord medicalInsurancePayRecord = ResultToPayRecord(root.output);
                medicalInsurancePayRecord.state = 0;
                medicalInsurancePayRecord.RegId = register.ID;
                medicalInsurancePayRecord.TradeType = 2;
                MI_MIPayRecordHead mIPayRecordHead = ResultToPayRecordHead(root.output);
                List<MI_MIPayRecordDetail> mIPayRecordDetailList = ResultToPayRecordDetail(root.output);
                AddLog("医保预算解析完成，开始保存", Color.Blue);

                InputClass input = new InputClass();
                Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
                dicStr.Add(InputType.MI_MedicalInsurancePayRecord, JsonHelper.SerializeObject(medicalInsurancePayRecord));
                dicStr.Add(InputType.MI_MIPayRecordHead, JsonHelper.SerializeObject(mIPayRecordHead));
                dicStr.Add(InputType.MI_MIPayRecordDetailList, JsonHelper.SerializeObject(mIPayRecordDetailList));
                input.SInput = dicStr;
                resultClass = hisDao.MZ_PreviewCharge(input);
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = "HIS插入医保预算数据报错" + e.Message;
                AddLog("HIS插入医保预算数据报错", Color.Red);
            }
            return resultClass;
        }
        public override ResultClass MZ_Charge(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();

            try
            {
                int tradeRecordId = inputClass.SInput.ContainsKey(InputType.TradeRecordId) ? Tools.ToInt32(inputClass.SInput[InputType.TradeRecordId].ToString(), 0) : 0;
                string fph = inputClass.SInput.ContainsKey(InputType.InvoiceNo) ? inputClass.SInput[InputType.InvoiceNo].ToString() : "";
                //医保登记并返回结果
                resultClass = ybInterfaceDao.MZ_Charge(sDll);
                decimal personcount = Tools.ToDecimal(resultClass.oResult.ToString(), 0);
                AddLog("医保结算解析完成，开始保存", Color.Blue);
                inputClass.SInput.Add(InputType.Money, personcount.ToString());

                //解析返回结果到类，保存
                resultClass = hisDao.MZ_Charge(inputClass);
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
                AddLog("医保结算失败，报错："+ e.Message, Color.Red);
            }
            return resultClass;
        }

        public override ResultClass MZ_CancelCharge(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                string fph = inputClass.SInput.ContainsKey(InputType.InvoiceNo) ? inputClass.SInput[InputType.InvoiceNo].ToString() : "";
                MI_MedicalInsurancePayRecord medicalInsurancePayRecordOld = hisDao.Mz_GetPayRecord(2, fph, 2, 1);

                if (medicalInsurancePayRecordOld == null)
                {
                    resultClass.bSucess = false;
                    resultClass.sRemarks = "获取收费信息失败！";
                    AddLog("获取收费信息失败!", Color.Red);
                }
                else
                {
                    //取消医保结算并返回结果
                    resultClass = ybInterfaceDao.MZ_CancelCharge(sDll,medicalInsurancePayRecordOld.TradeNO);
                    AddLog("取消结算完成，开始解析", Color.Blue);
                    DivideResult.root root = (DivideResult.root)resultClass.oResult;
                    MI_MedicalInsurancePayRecord medicalInsurancePayRecord = ResultToPayRecord(root.output);
                    medicalInsurancePayRecord.RegId = medicalInsurancePayRecordOld.RegId;
                    medicalInsurancePayRecord.FeeNO = medicalInsurancePayRecordOld.FeeNO;
                    medicalInsurancePayRecord.state = 2;
                    medicalInsurancePayRecord.TradeType = 2;
                    MI_MIPayRecordHead mIPayRecordHead = ResultToPayRecordHead(root.output);
                    List<MI_MIPayRecordDetail> mIPayRecordDetailList = ResultToPayRecordDetail(root.output);
                    //更新交易表
                    medicalInsurancePayRecordOld.state = 3;


                    List<MI_MedicalInsurancePayRecord> medicalInsurancePayRecordList = new List<MI_MedicalInsurancePayRecord>();
                    medicalInsurancePayRecordList.Add(medicalInsurancePayRecordOld);
                    medicalInsurancePayRecordList.Add(medicalInsurancePayRecord);

                    InputClass input = new InputClass();
                    Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
                    dicStr.Add(InputType.MI_MedicalInsurancePayRecordList, JsonHelper.SerializeObject(medicalInsurancePayRecordList));
                    dicStr.Add(InputType.MI_MIPayRecordHead, JsonHelper.SerializeObject(mIPayRecordHead));
                    dicStr.Add(InputType.MI_MIPayRecordDetailList, JsonHelper.SerializeObject(mIPayRecordDetailList));
                    input.SInput = dicStr;
                    AddLog("取消结算完成，解析完成，保存HIS开始", Color.Blue);
                    resultClass = hisDao.MZ_CancelCharge(input);
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
                AddLog("取消结算失败!错误信息："+e.Message, Color.Red);
            }
            return resultClass;
        }






        public override ResultClass MZ_DownloadzyPatFee(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public override ResultClass MZ_LoadFeeDetailByTicketNum(InputClass inputClass)
        {
            throw new NotImplementedException();
        }
        public override ResultClass MZ_UploadzyPatFee(InputClass inputClass)
        {
            throw new NotImplementedException();
        }
        #endregion



        #region 公用方法
        private Reg.root PreviewRegisterToInput(MI_Register register)
        {

            Reg.root root = new Reg.root();
            Reg.input input = new Reg.input();
            root.input = input;
            #region tradeinfo 交易信息 curetype/billtype 必填
            Reg.tradeinfo tradeinfo = new Reg.tradeinfo();
            tradeinfo.curetype = "17";
            tradeinfo.billtype = "0";

            tradeinfo.feeno = "0";
            tradeinfo.operator1 = register.StaffName;
            input.tradeinfo = tradeinfo;
            #endregion

            #region  处方信息 diagnoseno/recipeno/recipedate/recipetype/helpmedicineflag 必填
            Reg.recipearray recipearray = new Reg.recipearray();
            Reg.recipe recipe = new Reg.recipe();
            recipe.diagnoseno = "1";
            recipe.recipeno = "1";
            recipe.recipedate = System.DateTime.Now.ToString("yyyyMMdd hhmmss");
            recipe.recipetype = "1";
            recipe.helpmedicineflag = "0";

            recipe.hissectionname = register.DeptName;
            recipe.drid = register.DiagnDocID;
            recipe.drname = register.Doctor;
            recipe.registertradeno = register.SerialNO.ToString();
            recipe.billstype = "1";
            Reg.recipe[] recipes = { recipe };
            recipearray.recipe = recipes;

            input.recipearray = recipearray;
            #endregion

            #region 明细信息 itemno/recipeno/hiscode/itemname/itemtype/unitprice/count/fee/babyflag 必填
            Reg.feeitemarray feeitemarray = new Reg.feeitemarray();
            Reg.feeitem feeitem = new Reg.feeitem();
            feeitem.itemno = "1";
            feeitem.recipeno = "1";
            feeitem.hiscode = System.DateTime.Now.ToString("yyyyMMdd hhmmss");
            feeitem.itemname = "1";
            feeitem.itemtype = "0";
            feeitem.unitprice = register.DeptName;
            feeitem.count = register.DiagnDocID;
            feeitem.fee = register.Doctor;
            feeitem.babyflag = "0";

            Reg.feeitem[] feeitems = { feeitem };
            recipearray.recipe = recipes;

            input.feeitemarray = feeitemarray;
            #endregion
            return root;
        }

        private Reg.root TradeDataToInput(TradeData tradeData)
        {
            Reg.root root = new Reg.root();
            Reg.input input = new Reg.input();
            root.input = input;

            #region tradeinfo 交易信息 curetype/billtype 必填
            Reg.tradeinfo tradeinfo = new Reg.tradeinfo();
            tradeinfo.curetype = ((int)tradeData.tradeinfo.tradeType).ToString();
            tradeinfo.billtype = tradeData.tradeinfo.billtype;

            tradeinfo.feeno = "0";
            tradeinfo.operator1 = "";
            input.tradeinfo = tradeinfo;
            #endregion

            #region  处方信息 diagnoseno/recipeno/recipedate/recipetype/helpmedicineflag 必填
            Reg.recipearray recipearray = new Reg.recipearray();
            Reg.recipe[] recipes = new Reg.recipe[tradeData.recipeList.recipes.Count];
            for (int i = 0; i < tradeData.recipeList.recipes.Count; i++)
            {
                Reg.recipe recipe = new Reg.recipe();
                recipe.diagnoseno = tradeData.recipeList.recipes[i].diagnoseno;
                recipe.recipeno = tradeData.recipeList.recipes[i].recipeno;
                recipe.recipedate = tradeData.recipeList.recipes[i].recipedate;
                recipe.recipetype = tradeData.recipeList.recipes[i].recipetype;
                recipe.helpmedicineflag = tradeData.recipeList.recipes[i].helpmedicineflag;

                recipe.billstype = "1";
                recipes[i] = recipe;
            }

            recipearray.recipe = recipes;
            input.recipearray = recipearray;
            #endregion

            #region 明细信息 itemno/recipeno/hiscode/itemname/itemtype/unitprice/count/fee/babyflag 必填
            Reg.feeitemarray feeitemarray = new Reg.feeitemarray();
            Reg.feeitem[] feeitems = new Reg.feeitem[tradeData.feeitemList.feeitems.Count];
            for (int i = 0; i < tradeData.feeitemList.feeitems.Count; i++)
            {
                Reg.feeitem feeitem = new Reg.feeitem();
                feeitem.itemno = tradeData.feeitemList.feeitems[i].itemno;
                feeitem.recipeno = tradeData.feeitemList.feeitems[i].recipeno;
                feeitem.hiscode = tradeData.feeitemList.feeitems[i].hiscode;
                feeitem.itemname = tradeData.feeitemList.feeitems[i].itemname;
                feeitem.itemtype = tradeData.feeitemList.feeitems[i].itemtype;
                feeitem.unitprice = tradeData.feeitemList.feeitems[i].unitprice;
                feeitem.count = tradeData.feeitemList.feeitems[i].count;
                feeitem.fee = tradeData.feeitemList.feeitems[i].fee;
                feeitem.babyflag = "0";
                feeitems[i] = feeitem;
            }

            feeitemarray.feeitem = feeitems;

            input.feeitemarray = feeitemarray;
            #endregion
            return root;
        }
        private MI_MedicalInsurancePayRecord ResultToPayRecord(DivideResult.output output)
        {
            MI_MedicalInsurancePayRecord medicalInsurancePayRecord = new MI_MedicalInsurancePayRecord();
            if (output.tradeinfo != null)
            {
                medicalInsurancePayRecord.PatientType = 1;
                medicalInsurancePayRecord.TradeNO = output.tradeinfo.tradeno;
                medicalInsurancePayRecord.FeeNO = output.tradeinfo.feeno;
                medicalInsurancePayRecord.TradeTime = Tools.ToDateTime2(output.tradeinfo.tradedate, DateTime.Now);
            }

            if (output.sumpay != null)
            {
                medicalInsurancePayRecord.FeeAll = Tools.ToDecimal(output.sumpay.feeall, 0);
                medicalInsurancePayRecord.FeeFund = Tools.ToDecimal(output.sumpay.fund, 0);
                medicalInsurancePayRecord.FeeCash = Tools.ToDecimal(output.sumpay.cash, 0);
                medicalInsurancePayRecord.PersonCountPay = Tools.ToDecimal(output.sumpay.personcountpay, 0);
                medicalInsurancePayRecord.PersonCount = 0;
            }
            if (output.payinfo != null)
            {
                medicalInsurancePayRecord.FeeMIIn = Tools.ToDecimal(output.payinfo.mzfeein, 0);
                medicalInsurancePayRecord.FeeMIOut = Tools.ToDecimal(output.payinfo.mzfeeout, 0);
                medicalInsurancePayRecord.FeeDeductible = Tools.ToDecimal(output.payinfo.mzpayfirst, 0);
                medicalInsurancePayRecord.FeeSelfPay = Tools.ToDecimal(output.payinfo.mzselfpay2, 0);
                medicalInsurancePayRecord.FeeBigPay = Tools.ToDecimal(output.payinfo.mzbigpay, 0);
                medicalInsurancePayRecord.FeeBigSelfPay = Tools.ToDecimal(output.payinfo.mzbigselfpay, 0);
                medicalInsurancePayRecord.FeeOutOFPay = Tools.ToDecimal(output.payinfo.mzoutofbig, 0);
                medicalInsurancePayRecord.Feebcbx = Tools.ToDecimal(output.payinfo.bcpay, 0);
                medicalInsurancePayRecord.Feejcbz = Tools.ToDecimal(output.payinfo.jcbz, 0);
            }
            return medicalInsurancePayRecord;
        }

        private MI_MIPayRecordHead ResultToPayRecordHead(DivideResult.output output)
        {
            MI_MIPayRecordHead mIPayRecordHead = new MI_MIPayRecordHead();
            if (IsNew)
            {
                if (output.medicatalog2 != null)
                {
                    mIPayRecordHead.medicine = Tools.ToDecimal(output.medicatalog2.medicine, 0);
                    mIPayRecordHead.therb = Tools.ToDecimal(output.medicatalog2.therb, 0);
                    mIPayRecordHead.tmedicine = Tools.ToDecimal(output.medicatalog2.tmedicine, 0);
                    mIPayRecordHead.examine = Tools.ToDecimal(output.medicatalog2.examine, 0);
                    mIPayRecordHead.labexam = Tools.ToDecimal(output.medicatalog2.labexam, 0);
                    mIPayRecordHead.treatment = Tools.ToDecimal(output.medicatalog2.treatment, 0);
                    mIPayRecordHead.operation = Tools.ToDecimal(output.medicatalog2.operation, 0);
                    mIPayRecordHead.material = Tools.ToDecimal(output.medicatalog2.material, 0);
                    mIPayRecordHead.other = Tools.ToDecimal(output.medicatalog2.otheropfee, 0);

                    mIPayRecordHead.diagnosis = Tools.ToDecimal(output.medicatalog2.diagnosis, 0);
                    mIPayRecordHead.medicalservice = Tools.ToDecimal(output.medicatalog2.medicalservice, 0);
                    mIPayRecordHead.commonservice = Tools.ToDecimal(output.medicatalog2.commonservice, 0);
                    mIPayRecordHead.registfee = Tools.ToDecimal(output.medicatalog2.registfee, 0);

                }
            }
            else
            {
                if (output.medicatalog != null)
                {
                    mIPayRecordHead.medicine = Tools.ToDecimal(output.medicatalog.medicine, 0);
                    mIPayRecordHead.therb = Tools.ToDecimal(output.medicatalog.therb, 0);
                    mIPayRecordHead.tmedicine = Tools.ToDecimal(output.medicatalog.tmedicine, 0);
                    mIPayRecordHead.examine = Tools.ToDecimal(output.medicatalog.examine, 0);
                    mIPayRecordHead.labexam = Tools.ToDecimal(output.medicatalog.labexam, 0);
                    mIPayRecordHead.treatment = Tools.ToDecimal(output.medicatalog.treatment, 0);
                    mIPayRecordHead.operation = Tools.ToDecimal(output.medicatalog.operation, 0);
                    mIPayRecordHead.material = Tools.ToDecimal(output.medicatalog.material, 0);
                    mIPayRecordHead.other = Tools.ToDecimal(output.medicatalog.other, 0);

                    mIPayRecordHead.xray = Tools.ToDecimal(output.medicatalog.xray, 0);
                    mIPayRecordHead.ultrasonic = Tools.ToDecimal(output.medicatalog.ultrasonic, 0);
                    mIPayRecordHead.CT = Tools.ToDecimal(output.medicatalog.ct, 0);
                    mIPayRecordHead.mri = Tools.ToDecimal(output.medicatalog.mri, 0);
                    mIPayRecordHead.oxygen = Tools.ToDecimal(output.medicatalog.oxygen, 0);
                    mIPayRecordHead.bloodt = Tools.ToDecimal(output.medicatalog.bloodt, 0);
                    mIPayRecordHead.orthodontics = Tools.ToDecimal(output.medicatalog.orthodontics, 0);
                    mIPayRecordHead.prosthesis = Tools.ToDecimal(output.medicatalog.prosthesis, 0);
                    mIPayRecordHead.forensic_expertise = Tools.ToDecimal(output.medicatalog.forensic_expertise, 0);
                }
            }
            return mIPayRecordHead;
        }

        private List<MI_MIPayRecordDetail> ResultToPayRecordDetail(DivideResult.output output)
        {
            List<MI_MIPayRecordDetail> mIPayRecordDetailList = new List<MI_MIPayRecordDetail>();
            if (output.feeitems.Length > 0)
            {
                foreach (DivideResult.feeitem item in output.feeitems)
                {
                    if (item != null)
                    {
                        MI_MIPayRecordDetail mIPayRecordDetail = new MI_MIPayRecordDetail();
                        mIPayRecordDetail.itemno = item.itemno;
                        mIPayRecordDetail.recipeno = item.recipeno;
                        mIPayRecordDetail.hiscode = item.hiscode;
                        mIPayRecordDetail.itemcode = item.itemcode;
                        mIPayRecordDetail.itemname = item.itemname;
                        mIPayRecordDetail.itemtype = Tools.ToInt32(item.itemtype, 0);
                        mIPayRecordDetail.unitprice = Tools.ToDecimal(item.unitprice, 0);
                        mIPayRecordDetail.count = Tools.ToDecimal(item.count, 0);
                        mIPayRecordDetail.fee = Tools.ToDecimal(item.fee, 0);
                        mIPayRecordDetail.feein = Tools.ToDecimal(item.feein, 0);
                        mIPayRecordDetail.feeout = Tools.ToDecimal(item.feeout, 0);
                        mIPayRecordDetail.selfpay2 = Tools.ToDecimal(item.selfpay2, 0);
                        mIPayRecordDetail.state = Tools.ToInt32(item.state, 0);
                        mIPayRecordDetail.feetype = item.fee_type;
                        mIPayRecordDetail.preferentialfee = Tools.ToDecimal(item.preferentialfee, 0);
                        mIPayRecordDetail.preferentialscale = Tools.ToDecimal(item.preferentialscale, 0);
                        mIPayRecordDetailList.Add(mIPayRecordDetail);
                    }
                }
            }
            return mIPayRecordDetailList;
        }

        private Reg.root RegisterToInput(MI_Register register)
        {
            Reg.root root = new Reg.root();
            Reg.input input = new Reg.input();
            root.input = input;

            Reg.tradeinfo tradeinfo = new Reg.tradeinfo();
            tradeinfo.curetype = "11";
            tradeinfo.billtype = "0";
            tradeinfo.feeno = "0";
            tradeinfo.operator1 = register.StaffName;
            input.tradeinfo = tradeinfo;

            Reg.recipearray recipearray = new Reg.recipearray();
            Reg.recipe recipe = new Reg.recipe();
            recipe.hissectionname = register.DeptName;
            recipe.drid = register.DiagnDocID;
            recipe.drname = register.Doctor;
            recipe.registertradeno = register.SerialNO.ToString();
            recipe.billstype = "1";
            Reg.recipe[] recipes = { recipe };
            recipearray.recipe = recipes;

            input.recipearray = recipearray;

            Reg.feeitemarray feeitemarray = new Reg.feeitemarray();
            input.feeitemarray = feeitemarray;

            return root;
        }

        #endregion

        private void AddLog(string s,Color color)
        {
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, color, s);
        }
    }
}
