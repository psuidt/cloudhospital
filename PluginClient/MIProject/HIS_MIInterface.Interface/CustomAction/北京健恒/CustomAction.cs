using EFWCoreLib.CoreFrame.Common;
using HIS_Entity.MIManage;
using HIS_Entity.MIManage.Common;
using HIS_MIInterface.Interface.BaseClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HIS_Entity.MIManage.Common.JsonUtil;
using MedicareComLib;

namespace HIS_MIInterface.Interface.CustomAction.北京健恒
{
    public class CustomAction : AbstractMIAction<AbstractHISDao, CustomMIInterfaceDao>
    {
        public int _WorkId = 0;
        private DataTable _dtDataMatch;

        public CustomAction(int WorkId)
        {
            AddLog("北京-医保", Color.Blue);
            _WorkId = WorkId;
            hisDao.WorkId(WorkId);
            _dtDataMatch = hisDao.MZ_GetMIDataMatch();
        }
        OutpatientClass sDll;
        private bool IsNew = false;

        #region 门诊
        #region 读卡
        /// <summary>
        /// 读卡
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public override ResultClass MZ_GetCardInfo(InputClass inputClass)
        {
            sDll = new OutpatientClass();
            string sCardNo = inputClass.SInput.ContainsKey(InputType.CardNo) ? inputClass.SInput[InputType.CardNo].ToString() : "";
            return ybInterfaceDao.Mz_GetCardInfo(sDll, sCardNo);
        }
        /// <summary>
        /// 读病人信息
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public override ResultClass MZ_GetPersonInfo(InputClass inputClass)
        {
            sDll = new OutpatientClass();
            string sCardNo = inputClass.SInput.ContainsKey(InputType.CardNo) ? inputClass.SInput[InputType.CardNo].ToString() : "";
            return ybInterfaceDao.Mz_GetPersonInfo(sDll, sCardNo);
        }
        #endregion

        #region 挂号

        /// <summary>
        /// 预挂号并保存挂号信息
        /// </summary>
        /// <param name="inputClass">register 挂号类,dtInfo 挂号明细</param>
        /// <returns></returns>
        public override ResultClass MZ_PreviewRegister(InputClass inputClass)
        {
            InputClass input = new InputClass();

            ResultClass resultClass=new ResultClass();
            MI_Register register = inputClass.SInput.ContainsKey(InputType.MI_Register) ? JsonHelper.DeserializeJsonToObject<MI_Register>(inputClass.SInput[InputType.MI_Register].ToString()):null;
            DataTable dtInfo = inputClass.SInput.ContainsKey(InputType.DataTable) ? (DataTable)inputClass.SInput[InputType.DataTable] : null;
            string feeno = inputClass.SInput.ContainsKey(InputType.InvoiceNo) ? inputClass.SInput[InputType.InvoiceNo].ToString() : "0";
            if (dtInfo == null || dtInfo.Rows.Count <= 0)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = "医保预登记失败，无费用明细数据！请先维护！";
                AddLog("医保预登记失败，无费用明细数据！", Color.Red);
                return resultClass;
            }

            try
            {
                resultClass = ybInterfaceDao.MZ_PreviewRegister(sDll, PreviewRegisterToInput(feeno,register, dtInfo));
                //resultClass = ybInterfaceDao.MZ_PreviewRegister(sDll, TradeDataToInput(register));
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

                    medicalInsurancePayRecord.state = 0;
                    medicalInsurancePayRecord.TradeType = 1;
                    medicalInsurancePayRecord.MedicalType = "17";

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
        /// <summary>
        /// 确认挂号并更新挂号信息-状态
        /// </summary>
        /// <param name="inputClass">registerId 挂号ID serialNO挂号序号</param>
        /// <returns></returns>
        public override ResultClass MZ_Register(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            decimal personcount = -1;
            int registerId = inputClass.SInput.ContainsKey(InputType.RegisterId) ? Tools.ToInt32(inputClass.SInput[InputType.RegisterId].ToString(), 0) : 0;
            string serialNO = inputClass.SInput.ContainsKey(InputType.SerialNO) ? inputClass.SInput[InputType.SerialNO].ToString() : "";
            //医保登记并返回结果
            #region 医保操作
            try
            {
                resultClass = ybInterfaceDao.MZ_Register(sDll);
                if (resultClass.bSucess)
                {
                    personcount = Tools.ToDecimal(resultClass.oResult.ToString(), 0);
                }
                else
                {
                    return resultClass;
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = "医保登记报错："+e.ToString();
                AddLog("医保登记报错：" + e.Message, Color.Red);
                return resultClass;
            }
            #endregion

            try
            {
                AddLog("医保登记完成,开始解析医保登记数据", Color.Blue);

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
        /// <summary>
        /// HIS更新挂号发票
        /// </summary>
        /// <param name="inputClass">registerId 挂号ID serialNO挂号序号</param>
        /// <returns></returns>
        public override ResultClass MZ_UpdateRegister(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                int registerId = inputClass.SInput.ContainsKey(InputType.RegisterId) ? Tools.ToInt32(inputClass.SInput[InputType.RegisterId].ToString(), 0) : -1;
                string serialNO = inputClass.SInput.ContainsKey(InputType.SerialNO) ? inputClass.SInput[InputType.SerialNO].ToString() : "";
                string invoiceNo = inputClass.SInput.ContainsKey(InputType.InvoiceNo) ? inputClass.SInput[InputType.InvoiceNo].ToString() : "";


                InputClass input = new InputClass();
                Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
                dicStr.Add(InputType.RegisterId, registerId);
                dicStr.Add(InputType.SerialNO, serialNO);
                dicStr.Add(InputType.InvoiceNo, invoiceNo);

                input.SInput = dicStr;
                resultClass = hisDao.MZ_UpdateRegister(input);
                //MZ_PrintInvoice(inputClass);


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
        #endregion

        #region 退号
        /// <summary>
        /// 取消挂号信息
        /// </summary>
        /// <param name="inputClass">registerId 挂号ID serialNO挂号序号</param>
        /// <returns></returns>
        //public override ResultClass MZ_CancelRegister(InputClass inputClass)
        //{
        //    ResultClass resultClass = new ResultClass();
        //    int registerId = inputClass.SInput.ContainsKey(InputType.RegisterId) ? Tools.ToInt32(inputClass.SInput[InputType.RegisterId].ToString(), -1) : -1;
        //    string serialNO = inputClass.SInput.ContainsKey(InputType.SerialNO) ? inputClass.SInput[InputType.SerialNO].ToString() : "";
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
        //            bool b = hisDao.Mz_JudgeRegister(register.ID);
        //            if (b)
        //            {
        //                //取消医保登记并返回结果
        //                resultClass = ybInterfaceDao.Mz_CancelRegister(sDll,register.SocialCreateNum, serialNO);
        //                //resultClass = ybInterfaceDao.Mz_CancelRegister(sDll, register.SocialCreateNum, serialNO);
        //                if (resultClass.bSucess)
        //                {
        //                    if (inputClass.SInput.ContainsKey(InputType.InvoiceNo))
        //                    {
        //                        inputClass.SInput[InputType.InvoiceNo] = register.SerialNO;
        //                    }
        //                    else
        //                    {
        //                        inputClass.SInput.Add(InputType.InvoiceNo, register.SerialNO);
        //                    }


        //                    if (inputClass.SInput.ContainsKey(InputType.TradeNo))
        //                    {
        //                        inputClass.SInput[InputType.TradeNo] = resultClass.sRemarks;
        //                    }
        //                    else
        //                    {
        //                        inputClass.SInput.Add(InputType.TradeNo, resultClass.sRemarks);
        //                    }

        //                    MZ_RePrintInvoice(inputClass);



        //                    DivideResult.root root = (DivideResult.root)resultClass.oResult;
        //                    //解析返回数据，并保存退费记录
        //                    MI_MedicalInsurancePayRecord medicalInsurancePayRecord = ResultToPayRecord(root.output);
        //                    MI_MIPayRecordHead mIPayRecordHead = ResultToPayRecordHead(root.output);
        //                    List<MI_MIPayRecordDetail> mIPayRecordDetailList = ResultToPayRecordDetail(root.output);
        //                    register.ValidFlag = 2;
        //                    medicalInsurancePayRecord.state = 2;
        //                    medicalInsurancePayRecord.RegId = register.ID;
        //                    medicalInsurancePayRecord.TradeType = 1;

        //                    InputClass input = new InputClass();
        //                    Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
        //                    dicStr.Add(InputType.MI_Register, JsonHelper.SerializeObject(register));
        //                    dicStr.Add(InputType.MI_MedicalInsurancePayRecord, JsonHelper.SerializeObject(medicalInsurancePayRecord));
        //                    dicStr.Add(InputType.MI_MIPayRecordHead, JsonHelper.SerializeObject(mIPayRecordHead));
        //                    dicStr.Add(InputType.MI_MIPayRecordDetailList, JsonHelper.SerializeObject(mIPayRecordDetailList));
        //                    input.SInput = dicStr;
        //                    resultClass = hisDao.Mz_CancelRegister(input);
        //                }
        //            }
        //            else
        //            {
        //                resultClass.bSucess = false;
        //                resultClass.sRemarks = "存在已结算的收费数据！";
        //                AddLog("HIS存在已结算的收费数据！", Color.Red);
        //            }
        //        }                
        //    }
        //    catch (Exception e)
        //    {
        //        resultClass.bSucess = false;
        //        resultClass.sRemarks = e.Message;
        //        AddLog("取消登记报错：" + e.Message, Color.Red);
        //    }
        //    return resultClass;
        //}
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
                        resultClass = ybInterfaceDao.Mz_CancelRegister(sDll, register.SocialCreateNum, serialNO);
                        if (resultClass.bSucess)
                        {             
                            DivideResult.root root = (DivideResult.root)resultClass.oResult;
                            //解析返回数据，并保存退费记录
                            MI_MedicalInsurancePayRecord medicalInsurancePayRecord = ResultToPayRecord(root.output);
                            MI_MIPayRecordHead mIPayRecordHead = ResultToPayRecordHead(root.output);
                            List<MI_MIPayRecordDetail> mIPayRecordDetailList = ResultToPayRecordDetail(root.output);
                            //register.ValidFlag = 2;
                            medicalInsurancePayRecord.MedicalType = "17";
                            medicalInsurancePayRecord.state = 0;
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
        /// <summary>
        /// 确认取消挂号
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public override ResultClass MZ_CancelRegisterCommit(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            decimal personcount = -1;
            int registerId = inputClass.SInput.ContainsKey(InputType.RegisterId) ? Tools.ToInt32(inputClass.SInput[InputType.RegisterId].ToString(), 0) : 0;
            string serialNO = inputClass.SInput.ContainsKey(InputType.SerialNO) ? inputClass.SInput[InputType.SerialNO].ToString() : "";
            int iTradeRecordId = inputClass.SInput.ContainsKey(InputType.TradeRecordId) ? Tools.ToInt32(inputClass.SInput[InputType.TradeRecordId].ToString(), 0) : 0;
            //医保登记并返回结果
            #region 医保操作
            try
            {
                resultClass = ybInterfaceDao.MZ_CommitTrade(sDll);
                if (resultClass.bSucess)
                {
                    personcount = Tools.ToDecimal(resultClass.oResult.ToString(), 0);
                }
                else
                {
                    return resultClass;
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = "取消医保登记报错：" + e.ToString();
                AddLog("取消医保登记报错：" + e.Message, Color.Red);
                return resultClass;
            }
            #endregion

            try
            {
                AddLog("取消医保登记完成,开始解析取消医保登记数据", Color.Blue);
                
                InputClass input = new InputClass();
                Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
                dicStr.Add(InputType.RegisterId, registerId);
                dicStr.Add(InputType.SerialNO, serialNO);
                dicStr.Add(InputType.Money, personcount);
                dicStr.Add(InputType.TradeRecordId, iTradeRecordId);
                AddLog("解析取消医保登记数据完成,开始保存数据到HIS数据库", Color.Blue);
                input.SInput = dicStr;
                resultClass = hisDao.MZ_CancelRegisterCommit(input);
                if(resultClass.bSucess)
                {
                    resultClass.sRemarks = personcount.ToString();
                }
            }
            catch(Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = "更新中间表报错：" + e.ToString();
                AddLog("更新中间表报错：" + e.Message, Color.Red);
                return resultClass;
            }
            return resultClass;
        }
        #endregion

        #region 收费
        /// <summary>
        /// 预收费
        /// </summary>
        /// <param name="inputClass">tradedata 收费信息</param>
        /// <returns></returns>
        public override ResultClass MZ_PreviewCharge(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            TradeData tradedata = inputClass.SInput.ContainsKey(InputType.TradeData) ?JsonHelper.DeserializeJsonToObject<TradeData>(inputClass.SInput[InputType.TradeData].ToString()) : null;
            
            #region  医保操作
            MI_Register register = hisDao.Mz_Getregister(-1, tradedata.SerialNo);
            try
            {
                if (register == null)
                {
                    resultClass.bSucess = false;
                    resultClass.sRemarks = "医院编号：" + tradedata.WorkID + ",门诊就诊号：" + tradedata.SerialNo + ",未进行医保统筹登记";                    
                    AddLog("医院编号：" + tradedata.WorkID + ",门诊就诊号：" + tradedata.SerialNo + ",未进行医保统筹登记",Color.Red);
                }
                else
                {
                    if (register.ValidFlag != 1)
                    {
                        resultClass.bSucess = false;
                        resultClass.sRemarks = "医院编号：" + tradedata.WorkID + ",门诊就诊号：" + tradedata.SerialNo + ",医保统筹登记已取消";                       
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
            #endregion  

            try
            {
                if (resultClass.bSucess)
                {
                    AddLog("医保预算并返回结果成功，开始解析", Color.Blue);
                    DivideResult.root root = (DivideResult.root)resultClass.oResult;
                    //解析返回结果到类，保存
                    MI_MedicalInsurancePayRecord medicalInsurancePayRecord = ResultToPayRecord(root.output);
                    medicalInsurancePayRecord.state = 0;
                    medicalInsurancePayRecord.RegId = register.ID;
                    medicalInsurancePayRecord.MedicalType = "11";
                    medicalInsurancePayRecord.TradeType = 2;
                    medicalInsurancePayRecord.PatientName = register.PatientName;
                    MI_MIPayRecordHead mIPayRecordHead = ResultToPayRecordHead(root.output);
                    List<MI_MIPayRecordDetail> mIPayRecordDetailList = ResultToPayRecordDetail(root.output);
                    AddLog("医保预算解析完成，开始保存", Color.Blue);

                    InputClass input = new InputClass();
                    Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
                    dicStr.Add(InputType.MI_MedicalInsurancePayRecord, JsonHelper.SerializeObject(medicalInsurancePayRecord));
                    dicStr.Add(InputType.MI_MIPayRecordHead, JsonHelper.SerializeObject(mIPayRecordHead));
                    dicStr.Add(InputType.MI_MIPayRecordDetailList, JsonHelper.SerializeObject(mIPayRecordDetailList));
                    input.SInput = dicStr;
                    string sError = "";
                    if (!resultClass.sRemarks.Equals(""))
                    {
                        sError = resultClass.sRemarks;
                    }

                    resultClass = hisDao.MZ_PreviewCharge(input);
                    resultClass.sRemarks += sError;
                }
                else
                {
                    return resultClass;
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = "HIS插入医保预算数据报错" + e.Message;
                AddLog("HIS插入医保预算数据报错", Color.Red);
            }
            return resultClass;
        }
        /// <summary>
        /// 确认收费
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public override ResultClass MZ_Charge(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();

            try
            {
                int tradeRecordId = inputClass.SInput.ContainsKey(InputType.TradeRecordId) ? Tools.ToInt32(inputClass.SInput[InputType.TradeRecordId].ToString(), 0) : 0;
                string fph = inputClass.SInput.ContainsKey(InputType.InvoiceNo) ? inputClass.SInput[InputType.InvoiceNo].ToString() : "";
                //医保登记并返回结果
                resultClass = ybInterfaceDao.MZ_Charge(sDll);
                if (resultClass.bSucess)
                {
                    decimal personcount = Tools.ToDecimal(resultClass.oResult.ToString(), 0);
                    AddLog("医保结算解析完成，开始保存", Color.Blue);
                    inputClass.SInput.Add(InputType.Money, personcount.ToString());

                    //解析返回结果到类，保存
                    resultClass = hisDao.MZ_Charge(inputClass);
                }
                else
                {
                    return resultClass;
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
                AddLog("医保结算失败，报错："+ e.Message, Color.Red);
            }
            return resultClass;
        }
        #endregion

        #region 退费
        /// <summary>
        /// 取消收费
        /// </summary>
        /// <param name="inputClass">invoiceNo HIS发票号</param>
        /// <returns></returns>
        //public override ResultClass MZ_CancelCharge(InputClass inputClass)
        //{
        //    ResultClass resultClass = new ResultClass();
        //    try
        //    {
        //        string fph = inputClass.SInput.ContainsKey(InputType.InvoiceNo) ? inputClass.SInput[InputType.InvoiceNo].ToString() : "";
        //        MI_MedicalInsurancePayRecord medicalInsurancePayRecordOld = hisDao.Mz_GetPayRecord(2, fph, 2, 1);

        //        if (medicalInsurancePayRecordOld == null)
        //        {
        //            resultClass.bSucess = false;
        //            resultClass.sRemarks = "获取收费信息失败！";
        //            AddLog("获取收费信息失败!", Color.Red);
        //        }
        //        else
        //        {
        //            //取消医保结算并返回结果
        //            resultClass = ybInterfaceDao.MZ_CancelCharge(sDll,medicalInsurancePayRecordOld.TradeNO,fph);
        //            if (resultClass.bSucess)
        //            {

        //                AddLog("取消结算完成，开始解析", Color.Blue);
        //                DivideResult.root root = (DivideResult.root)resultClass.oResult;
        //                MI_MedicalInsurancePayRecord medicalInsurancePayRecord = ResultToPayRecord(root.output);
        //                medicalInsurancePayRecord.RegId = medicalInsurancePayRecordOld.RegId;
        //                medicalInsurancePayRecord.FeeNO = medicalInsurancePayRecordOld.FeeNO;
        //                medicalInsurancePayRecord.state = 2;
        //                medicalInsurancePayRecord.TradeType = 2;
        //                MI_MIPayRecordHead mIPayRecordHead = ResultToPayRecordHead(root.output);
        //                List<MI_MIPayRecordDetail> mIPayRecordDetailList = ResultToPayRecordDetail(root.output);
        //                //更新交易表
        //                medicalInsurancePayRecordOld.state = 3;


        //                List<MI_MedicalInsurancePayRecord> medicalInsurancePayRecordList = new List<MI_MedicalInsurancePayRecord>();
        //                medicalInsurancePayRecordList.Add(medicalInsurancePayRecordOld);
        //                medicalInsurancePayRecordList.Add(medicalInsurancePayRecord);

        //                InputClass input = new InputClass();
        //                Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
        //                dicStr.Add(InputType.MI_MedicalInsurancePayRecordList, JsonHelper.SerializeObject(medicalInsurancePayRecordList));
        //                dicStr.Add(InputType.MI_MIPayRecordHead, JsonHelper.SerializeObject(mIPayRecordHead));
        //                dicStr.Add(InputType.MI_MIPayRecordDetailList, JsonHelper.SerializeObject(mIPayRecordDetailList));
        //                input.SInput = dicStr;
        //                AddLog("取消结算完成，解析完成，保存HIS开始", Color.Blue);
        //                resultClass = hisDao.MZ_CancelCharge(input);
        //            }

        //            if (resultClass.bSucess)
        //            {
        //                if (inputClass.SInput.ContainsKey(InputType.InvoiceNo))
        //                {
        //                    inputClass.SInput[InputType.InvoiceNo] = fph;
        //                }
        //                else
        //                {
        //                    inputClass.SInput.Add(InputType.InvoiceNo, fph);
        //                }


        //                if (inputClass.SInput.ContainsKey(InputType.TradeNo))
        //                {
        //                    inputClass.SInput[InputType.TradeNo] = medicalInsurancePayRecordOld.TradeNO;
        //                }
        //                else
        //                {
        //                    inputClass.SInput.Add(InputType.TradeNo, medicalInsurancePayRecordOld.TradeNO);
        //                }

        //                MZ_PrintInvoice(inputClass);
        //            }

        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        resultClass.bSucess = false;
        //        resultClass.sRemarks = e.Message;
        //        AddLog("取消结算失败!错误信息："+e.Message, Color.Red);
        //    }
        //    return resultClass;
        //}
        public override ResultClass MZ_CancelCharge(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            string fph = inputClass.SInput.ContainsKey(InputType.InvoiceNo) ? inputClass.SInput[InputType.InvoiceNo].ToString() : "";
            try
            {
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
                    resultClass = ybInterfaceDao.MZ_CancelCharge(sDll, medicalInsurancePayRecordOld.TradeNO, fph);
                    if (resultClass.bSucess)
                    {

                        AddLog("取消结算完成，开始解析", Color.Blue);
                        DivideResult.root root = (DivideResult.root)resultClass.oResult;
                        MI_MedicalInsurancePayRecord medicalInsurancePayRecord = ResultToPayRecord(root.output);
                        medicalInsurancePayRecord.RegId = medicalInsurancePayRecordOld.RegId;
                        medicalInsurancePayRecord.FeeNO = medicalInsurancePayRecordOld.FeeNO;
                        medicalInsurancePayRecord.state = 5;
                        medicalInsurancePayRecord.TradeType = 2;
                        medicalInsurancePayRecord.MedicalType = "11";
                        MI_MIPayRecordHead mIPayRecordHead = ResultToPayRecordHead(root.output);
                        List<MI_MIPayRecordDetail> mIPayRecordDetailList = ResultToPayRecordDetail(root.output);
                        //更新交易表
                        InputClass input = new InputClass();
                        Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
                        dicStr.Add(InputType.MI_MedicalInsurancePayRecord, JsonHelper.SerializeObject(medicalInsurancePayRecord));
                        dicStr.Add(InputType.MI_MIPayRecordHead, JsonHelper.SerializeObject(mIPayRecordHead));
                        dicStr.Add(InputType.MI_MIPayRecordDetailList, JsonHelper.SerializeObject(mIPayRecordDetailList));
                        input.SInput = dicStr;
                        AddLog("取消结算完成，解析完成，保存HIS开始", Color.Blue);
                        resultClass = hisDao.MZ_CancelCharge(input);
                    }
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
                AddLog("取消结算失败!错误信息：" + e.Message, Color.Red);
            }
            return resultClass;
        }
        /// <summary>
        /// 确认取消收费 todo
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public override ResultClass MZ_CancelChargeCommit(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            decimal personcount = -1;
            string fph = inputClass.SInput.ContainsKey(InputType.InvoiceNo) ? inputClass.SInput[InputType.InvoiceNo].ToString() : "";
            //医保登记并返回结果
            #region 医保操作
            try
            {

                resultClass = ybInterfaceDao.MZ_CommitTrade(sDll);
                if (resultClass.bSucess)
                {
                    personcount = Tools.ToDecimal(resultClass.oResult.ToString(), 0);
                }
                else
                {
                    return resultClass;
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = "取消医保收费报错：" + e.ToString();
                AddLog("取消医保收费报错：" + e.Message, Color.Red);
                return resultClass;
            }
            #endregion

            try
            {
                AddLog("确认取消医保收费完成，更新中间表", Color.Blue);

                InputClass input = new InputClass();
                Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
                dicStr.Add(InputType.InvoiceNo, fph);
                dicStr.Add(InputType.Money, personcount);
                input.SInput = dicStr;
                resultClass = hisDao.MZ_CancelChargeCommit(input);
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = "确认取消医保收费更新中间表报错：" + e.ToString();
                AddLog("确认取消医保收费更新中间表报错：" + e.Message, Color.Red);
                return resultClass;
            }
            return resultClass;
        }
        #endregion

        /// <summary>
        /// 更新交易状态
        /// </summary>
        /// <param name="inputClass">tradeNo 社保交易号</param>
        /// <returns></returns>
        public override ResultClass MZ_CommitTradeState(InputClass inputClass)
        {
            sDll = new OutpatientClass();
            ResultClass resultClass = new ResultClass();
            string tradeNo = inputClass.SInput.ContainsKey(InputType.TradeNo) ? inputClass.SInput[InputType.TradeNo].ToString().ToString() : "0";            
            resultClass = ybInterfaceDao.MZ_CommitTradeState(sDll, tradeNo);

            return resultClass;
        }
        /// <summary>
        /// 重新打印发票
        /// </summary>
        /// <param name="inputClass">tradeNo 社保交易号 invoiceNo HIS发票号</param>
        /// <returns></returns>
        public override ResultClass MZ_RePrintInvoice(InputClass inputClass)
        {
            sDll = new OutpatientClass();
            ResultClass resultClass = new ResultClass();
            string tradeNo = inputClass.SInput.ContainsKey(InputType.TradeNo) ? inputClass.SInput[InputType.TradeNo].ToString().ToString() : "0";
            string invoiceNo = inputClass.SInput.ContainsKey(InputType.InvoiceNo) ? inputClass.SInput[InputType.InvoiceNo].ToString() : "";
            resultClass = ybInterfaceDao.RePrintInvoice(sDll, tradeNo, invoiceNo);

            return resultClass;
        }
        /// <summary>
        /// 打印发票
        /// </summary>
        /// <param name="inputClass">tradeNo 社保交易号 invoiceNo HIS发票号 businessinfo 业务流水号</param>
        /// <returns></returns>
        public override ResultClass MZ_PrintInvoice(InputClass inputClass)
        {
            sDll = new OutpatientClass();
            ResultClass resultClass = new ResultClass();
            string tradeNo = inputClass.SInput.ContainsKey(InputType.TradeNo) ? inputClass.SInput[InputType.TradeNo].ToString().ToString() : "0";
            string invoiceNo = inputClass.SInput.ContainsKey(InputType.InvoiceNo) ? inputClass.SInput[InputType.InvoiceNo].ToString() : "0";
            string businessinfo = inputClass.SInput.ContainsKey(InputType.SerialNO) ? inputClass.SInput[InputType.SerialNO].ToString() : invoiceNo;


            string sIn = "";
            sIn = sIn + "<?xml version=\"1.0\" encoding=\"utf-16\" standalone=\"yes\" ?>";
            sIn = sIn + " <root version=\"2.003\" sender=\"BJSNJHTN00002.100162\">";
            sIn = sIn + " <input>";
            sIn = sIn + "     <tradeno>{0}</tradeno>";
            sIn = sIn + "     <invoiceno>{1}</invoiceno>";
            sIn = sIn + "     <businessinfo>{2}</businessinfo>";
            sIn = sIn + "     <hosptype>{3}</hosptype>";
            sIn = sIn + " </input>";
            sIn = sIn + "</root>";

            sIn = string.Format(sIn, tradeNo, invoiceNo, businessinfo, "未知");

            resultClass = ybInterfaceDao.MZ_PrintInvoice(sDll, sIn);

            //resultClass = ybInterfaceDao.RePrintInvoice(sDll, tradeNo, invoiceNo);

            return resultClass;
        }

        /// <summary>
        /// HIS自行打印发票
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public override ResultClass MZ_HISPrintInvoice(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();

            try
            {
                int tradeRecordId = inputClass.SInput.ContainsKey(InputType.TradeRecordId) ? Tools.ToInt32(inputClass.SInput[InputType.TradeRecordId].ToString(), 0) : 0;
                string fph = inputClass.SInput.ContainsKey(InputType.InvoiceNo) ? inputClass.SInput[InputType.InvoiceNo].ToString() : "";
                //医保登记并返回结果
                resultClass = ybInterfaceDao.MZ_Charge(sDll);
                if (resultClass.bSucess)
                {
                    decimal personcount = Tools.ToDecimal(resultClass.oResult.ToString(), 0);
                    AddLog("医保结算解析完成，开始保存", Color.Blue);
                    inputClass.SInput.Add(InputType.Money, personcount.ToString());

                    //解析返回结果到类，保存
                    resultClass = hisDao.MZ_Charge(inputClass);
                }
                else
                {
                    return resultClass;
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
                AddLog("医保结算失败，报错：" + e.Message, Color.Red);
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

        /// <summary>
        /// 根据医保交易状态更新交易记录
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public override ResultClass Mz_UpdateTradeRecord(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                resultClass = hisDao.Mz_UpdateTradeRecord(inputClass);
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = "更新记录状态报错：" + e.ToString();
                AddLog("更新记录状态报错：" + e.Message, Color.Red);
                return resultClass;
            }

            return resultClass;
        }
        #endregion

        public override ResultClass Mz_GetRegisterTradeNo(string sSerialNO)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                resultClass = hisDao.Mz_GetRegisterTradeNo(sSerialNO);
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = "获取挂号信息异常：" + e.ToString();
                AddLog("获取挂号信息异常：" + e.Message, Color.Red);
                return resultClass;
            }

            return resultClass;
        }

        #region 公用方法
        private Reg.root PreviewRegisterToInput(string feeno,MI_Register register,DataTable dtInfo)
        {
            Reg.root root = new Reg.root();
            Reg.input input = new Reg.input();
            root.input = input;
            #region tradeinfo 交易信息 curetype/billtype 必填
            Reg.tradeinfo tradeinfo = new Reg.tradeinfo();
            tradeinfo.curetype = "17";
            tradeinfo.illtype = "0";

            tradeinfo.feeno = feeno;
            tradeinfo.operator1 = register.StaffName;
            input.tradeinfo = tradeinfo;
            #endregion

            #region  处方信息 diagnoseno/recipeno/recipedate/recipetype/helpmedicineflag 必填
            Reg.recipearray recipearray = new Reg.recipearray();
            Reg.recipe recipe = new Reg.recipe();
            recipe.diagnoseno = "1";
            recipe.recipeno = "1";
            recipe.recipedate = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            recipe.recipetype = "1";
            recipe.helpmedicineflag = "0";

            recipe.sectioncode = GetMatchData(0, "5", register.MedicalClass, register.DeptID.ToString(), "0307");
                //(_dtDataMatch==null || _dtDataMatch.Rows.Count<=0)? "0307":"";
            recipe.sectionname = GetMatchData(1, "5", register.MedicalClass,  register.DeptID.ToString(), "内分泌专业");
            //"内分泌专业";
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
            if (dtInfo != null && dtInfo.Rows.Count > 0)
            {
                Reg.feeitem[] feeitems = new Reg.feeitem[dtInfo.Rows.Count];
                for (int iCount = 0; iCount < dtInfo.Rows.Count; iCount++)
                {
                    Reg.feeitem feeitem = new Reg.feeitem();
                    feeitem.itemno = iCount.ToString();
                    feeitem.recipeno = "1";
                    feeitem.hiscode = dtInfo.Rows[iCount]["ItemCode"].ToString();
                    feeitem.itemname = dtInfo.Rows[iCount]["ItemName"].ToString();
                    feeitem.itemtype = "2";
                    feeitem.unitprice = dtInfo.Rows[iCount]["Price"].ToString();
                    feeitem.count = dtInfo.Rows[iCount]["Count"].ToString();
                    feeitem.fee = dtInfo.Rows[iCount]["Fee"].ToString();
                    feeitem.usedate = DateTime.Now.ToString("yyyyMMddHHmmss");
                    feeitem.dose = "";
                    feeitem.specification = "";
                    feeitem.unit = "";
                    feeitem.howtouse = "";
                    feeitem.dosage = "";
                    feeitem.packaging = "";
                    feeitem.minpackage = "";
                    feeitem.conversion = "1";
                    feeitem.days = "";
                    feeitem.babyflag = "0";

                    feeitems[iCount] = feeitem;
                }
                feeitemarray.feeitem = feeitems;
            }
            input.feeitemarray = feeitemarray;
            #endregion
            return root;
        }

        private string PreviewRegisterToXml(MI_Register register)
        {
            Reg.root root = new Reg.root();
            Reg.input input = new Reg.input();
            root.input = input;
            #region tradeinfo 交易信息 curetype/billtype 必填
            Reg.tradeinfo tradeinfo = new Reg.tradeinfo();
            tradeinfo.curetype = "17";
            tradeinfo.illtype = "0";

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
            //return root;

            string sIn;

            //此处写的XML仅供测试接口使用，具体格式应以接口文档为准，且在生成此XML文档时应使用XML DOM对象生成，自行拼串需要处理特殊的XML字符转义
            sIn = "";
            sIn = sIn + "<?xml version=\"1.0\" encoding=\"utf-16\" standalone=\"yes\" ?>";
            sIn = sIn + " <root version=\"2.003\" sender=\"Test His 1.0\">";
            sIn = sIn + " <input>";
            sIn = sIn + "   <tradeinfo>";
            sIn = sIn + "     <curetype>11</curetype>";
            sIn = sIn + "     <illtype>0</illtype>";
            sIn = sIn + "     <feeno>xxxxx</feeno>";
            sIn = sIn + "     <operator>xxxxx</operator>";
            sIn = sIn + "   </tradeinfo>";
            sIn = sIn + "   <recipearray>";
            sIn = sIn + "     <recipe>";
            sIn = sIn + "       <diagnoseno>1</diagnoseno>";
            sIn = sIn + "       <recipeno>1</recipeno>";
            sIn = sIn + "       <recipedate>20080808</recipedate>";
            sIn = sIn + "       <diagnosename>啊</diagnosename>";
            sIn = sIn + "       <diagnosecode>01</diagnosecode>";
            sIn = sIn + "       <medicalrecord>阿斯顿</medicalrecord>";
            sIn = sIn + "       <sectioncode>01</sectioncode>";
            sIn = sIn + "       <sectionname>内科</sectionname>";
            sIn = sIn + "       <hissectionname>内科2</hissectionname>";
            sIn = sIn + "       <drid>0999</drid>";
            sIn = sIn + "       <drname>甲乙</drname>";
            sIn = sIn + "       <recipetype>1</recipetype>";
            sIn = sIn + "     </recipe>";
            sIn = sIn + "   </recipearray>";
            sIn = sIn + "   <feeitemarray>";
            sIn = sIn +
                  "<feeitem  itemno=\"0\" recipeno=\"123\" hiscode=\"7117\" itemname=\"罗红霉素片\" itemtype=\"1\" unitprice=\"111.00\" count=\"6\" fee=\"666.00\" dose=\"010100\" specification=\"规格\"  unit=\"单位\" howtouse=\"01\" dosage=\"单次用量\" packaging=\"包装单位\"  minpackage=\"最小包装\" conversion=\"1\" days=\"1\"/>";
            sIn = sIn + "   </feeitemarray>";
            sIn = sIn + " </input>";
            sIn = sIn + " ";
            sIn = sIn + "</root>";
            return sIn;
        }

        private Reg.root TradeDataToInput(TradeData tradeData)  
        {
            Reg.root root = new Reg.root();
            Reg.input input = new Reg.input();
            root.input = input;

            #region tradeinfo 交易信息 curetype/billtype 必填
            Reg.tradeinfo tradeinfo = new Reg.tradeinfo();
            tradeinfo.curetype = ((int)tradeData.tradeinfo.tradeType).ToString();
            tradeinfo.illtype = tradeData.tradeinfo.billtype;

            tradeinfo.feeno = tradeData.tradeinfo.feeno;
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
                recipe.diagnosecode = tradeData.recipeList.recipes[i].diagnosecode;
                recipe.diagnosename = tradeData.recipeList.recipes[i].diagnosename;
                recipe.sectioncode = GetMatchData(0, "5", tradeData.MIID.ToString(), tradeData.recipeList.recipes[i].sectioncode, "0307");
                //"0307";//tradeData.recipeList.recipes[i].sectioncode;
                recipe.sectionname = GetMatchData(1, "5", tradeData.MIID.ToString(), tradeData.recipeList.recipes[i].sectioncode, "内分泌专业");
                //"内分泌专业";//tradeData.recipeList.recipes[i].sectionname;
                recipe.hissectionname = tradeData.recipeList.recipes[i].hissectionname;
                recipe.drid = tradeData.recipeList.recipes[i].drid;
                recipe.drname = tradeData.recipeList.recipes[i].drname;
                recipe.recipetype = tradeData.recipeList.recipes[i].recipetype;
                recipe.helpmedicineflag = tradeData.recipeList.recipes[i].helpmedicineflag;
                recipe.recipetype = tradeData.recipeList.recipes[i].recipetype;

                recipe.registertradeno = tradeData.recipeList.recipes[i].registertradeno;
                recipe.billstype = tradeData.recipeList.recipes[i].billstype; ;
                recipes[i] = recipe;
            }

            recipearray.recipe = recipes;
            input.recipearray = recipearray;
            #endregion

            #region 明细信息 itemno/recipeno/hiscode/itemname/itemtype/unitprice/count/fee/babyflag/drugapprovalnumber 必填
            Reg.feeitemarray feeitemarray = new Reg.feeitemarray();
            Reg.feeitem[] feeitems = new Reg.feeitem[tradeData.feeitemList.feeitems.Count];
            for (int i = 0; i < tradeData.feeitemList.feeitems.Count; i++)
            {
                Reg.feeitem feeitem = new Reg.feeitem();
                feeitem.itemno = tradeData.feeitemList.feeitems[i].itemno;
                feeitem.recipeno = tradeData.feeitemList.feeitems[i].recipeno;
                feeitem.usedate = DateTime.Now.ToString("yyyyMMddHHmmss");
                feeitem.hiscode = tradeData.feeitemList.feeitems[i].hiscode;

                feeitem.itemname = tradeData.feeitemList.feeitems[i].itemname;
                feeitem.itemtype = tradeData.feeitemList.feeitems[i].itemtype;
                feeitem.unitprice = tradeData.feeitemList.feeitems[i].unitprice;
                feeitem.count = tradeData.feeitemList.feeitems[i].count;
                feeitem.fee = tradeData.feeitemList.feeitems[i].fee;
                feeitem.drugapprovalnumber = tradeData.feeitemList.feeitems[i].drugapprovalnumber;

                feeitem.dosage = tradeData.feeitemList.feeitems[i].dosage;
                feeitem.dose = GetMatchData(0, "1",  tradeData.MIID.ToString(), tradeData.feeitemList.feeitems[i].dose, tradeData.feeitemList.feeitems[i].dose);
                //tradeData.feeitemList.feeitems[i].dose;
                feeitem.days = tradeData.feeitemList.feeitems[i].days;
                feeitem.howtouse = GetMatchData(0, "2",  tradeData.MIID.ToString(), tradeData.feeitemList.feeitems[i].howtouse, tradeData.feeitemList.feeitems[i].howtouse);
                //tradeData.feeitemList.feeitems[i].howtouse;
                feeitem.specification = tradeData.feeitemList.feeitems[i].specification;
                feeitem.unit = tradeData.feeitemList.feeitems[i].unit;

                feeitem.packaging = tradeData.feeitemList.feeitems[i].packaging;
                feeitem.minpackage = tradeData.feeitemList.feeitems[i].minpackage;
                feeitem.conversion = tradeData.feeitemList.feeitems[i].conversion;

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
                medicalInsurancePayRecord.ApplyNO = output.tradeinfo.ic_no;
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
            //if (IsNew)
            if (1==1)
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
            tradeinfo.illtype = "0";
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
        /// <summary>
        /// 获取匹配值
        /// </summary>
        /// <param name="iValue">0：code 1：name</param>
        /// <param name="sDataType">字典类型1.剂型2.频次3.参保人员类别4收费等级5就诊科别</param>
        /// <param name="sPatTypeID"></param>
        /// <param name="sHospDataID"></param>
        /// <param name="sDefaut"></param>
        /// <returns></returns>
        private string GetMatchData(int iValue,string sDataType, string sPatTypeID,string sHospDataID, string sDefaut)
        {
            string sValue = sDefaut;
            if (_dtDataMatch != null && _dtDataMatch.Rows.Count > 0)
            {
                DataRow[] drs = _dtDataMatch.Select(" DataType="+ sDataType + " and PatTypeID="+ sPatTypeID + "  and  HospDataID=" + sHospDataID );
                if (iValue == 0)
                {
                    sValue = drs.Length > 0 ? drs[0]["code"].ToString() : sDefaut;
                }
                else
                {
                    sValue = drs.Length > 0 ? drs[0]["name"].ToString() : sDefaut;
                }
            }

            return sValue;

        }
        private void AddLog(string s,Color color)
        {
            //MiddlewareLogHelper.WriterLog(LogType.MILog, true, color, s);
        }
    }
}
