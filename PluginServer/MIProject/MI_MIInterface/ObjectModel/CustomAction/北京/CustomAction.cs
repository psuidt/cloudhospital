using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MI_MIInterface.ObjectModel.BaseClass;
using System.Data;
using HIS_Entity.MIManage;
using MI_MIInterface.ObjectModel.Common;
using static HIS_Entity.MIManage.Common.JsonUtil;
using HIS_Entity.MIManage.Common;
using EFWCoreLib.CoreFrame.Common;
using System.Drawing;

namespace MI_MIInterface.ObjectModel.CustomAction.beijing
{
    public class CustomAction : AbstractMIAction<AbstractHISDao, CustomMIInterfaceDao>
    {
        private bool IsNew = false;
        private Dictionary<string, string> resultDic = new Dictionary<string, string>();
        public override string GetRemoteCommParaDescript()
        {
            return "示例";
        }

        #region 匹配接口
        public override bool M_DownLoadDrugContent(int ybId, int workId)
        {
            //从医保接口下载数据，再保存到HIS数据库
            return true;
        }

        public override bool M_DownLoadMaterialsContent(int ybId, int workId)
        {
            return true;
        }

        public override bool M_DownLoadItemContent(int ybId, int workId)
        {
            return true;
        }
        #endregion

        #region 住院接口
        public override bool Zy_UploadzyPatFee(int iPatientId,int iFlag)
        {
            return hisDao.Zy_UploadzyPatFee(iPatientId, iFlag);
            //return NewDao<AbstractHISDao>().Zy_UploadzyPatFee(iPatientId);
        }
        #endregion

        #region 门诊接口
        public override ResultClass MZ_GetCardInfo(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public override ResultClass MZ_GetPersonInfo(InputClass inputClass)//string sCardNo)
        {
            throw new NotImplementedException();
        }

        public override ResultClass MZ_PreviewRegister(InputClass inputClass)//int registerId, string serialNO)
        {
            ResultClass resultClass = new ResultClass();            
            try
            {
                resultDic.Clear();
                MI_Register register = JsonHelper.DeserializeJsonToObject<MI_Register>(inputClass.SInput[InputType.MI_Register].ToString());
                MI_MedicalInsurancePayRecord medicalInsurancePayRecord = JsonHelper.DeserializeJsonToObject<MI_MedicalInsurancePayRecord>(inputClass.SInput[InputType.MI_MedicalInsurancePayRecord].ToString());
                MI_MIPayRecordHead mIPayRecordHead = JsonHelper.DeserializeJsonToObject<MI_MIPayRecordHead>(inputClass.SInput[InputType.MI_MIPayRecordHead].ToString());
                List<MI_MIPayRecordDetail> mIPayRecordDetailList = JsonHelper.DeserializeJsonToList<MI_MIPayRecordDetail>(inputClass.SInput[InputType.MI_MIPayRecordDetailList].ToString());
                AddLog("解析完成，开始保存登记信息");
                register.SocialCreateNum = medicalInsurancePayRecord.TradeNO;
                //获取通过病人类型获取MIID
                MI_MedicalInsuranceType medicalInsuranceType = hisDao.Mz_GetMITypeInfo(register.MedicalClass);
                if (medicalInsuranceType != null && medicalInsuranceType.ID > 0)
                {
                    register.MIID = medicalInsuranceType.ID;
                    register.PatientType = medicalInsuranceType.ID;
                    ResultClass regResultClass = hisDao.Mz_SaveRegister(register);
                    if (regResultClass.bSucess)
                    {
                        MI_Register registerResult = regResultClass.oResult as MI_Register;
                        AddLog("保存登记完成，开始保存交易信息");
                        medicalInsurancePayRecord.RegId = registerResult.ID;

                        MI_MedicalInsurancePayRecord medicalInsurancePayRecordResult = hisDao.SaveTradeInfo(medicalInsurancePayRecord, mIPayRecordHead, mIPayRecordDetailList);
                        AddLog("保存交易信息完成，开始返回");
                        if (medicalInsurancePayRecordResult.ID > 0)
                        {
                            //返回数据到前台界面，只需金额
                            resultDic.Add("Id", registerResult.ID.ToString());
                            resultDic.Add("tradeno", medicalInsurancePayRecord.TradeNO);
                            resultDic.Add("FeeAll", medicalInsurancePayRecord.FeeAll.ToString());
                            resultDic.Add("fund", medicalInsurancePayRecord.FeeFund.ToString());
                            resultDic.Add("cash", medicalInsurancePayRecord.FeeCash.ToString());
                            resultDic.Add("personcountpay", medicalInsurancePayRecord.PersonCountPay.ToString());
                            resultClass.bSucess = true;
                            resultClass.oResult = resultDic;
                        }
                        else
                        {
                            resultClass.bSucess = false;
                            resultClass.sRemarks = "保存交易数据失败，返回ID为零";
                            resultClass.oResult = null; ;
                        }
                    }
                    else
                    {
                        AddLog("开始保存登记失败");
                        resultClass.bSucess = false;
                        resultClass.sRemarks = regResultClass.sRemarks;
                        resultClass.oResult = null; ;
                    }
                }
                else
                {
                    resultClass.bSucess = false;
                    resultClass.sRemarks = "找不到病人类型的医保类型表数据！";
                    resultClass.oResult = null; ;
                }         

            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
            }
            return resultClass;
        }


        public override ResultClass MZ_Register(InputClass inputClass)//int registerId, string serialNO)
        {
            ResultClass resultClass = new ResultClass();
            resultDic.Clear();
            decimal personcount = inputClass.SInput.ContainsKey(InputType.Money) ? Tools.ToDecimal(inputClass.SInput[InputType.Money].ToString(), 0) : 0;
            int registerId = inputClass.SInput.ContainsKey(InputType.RegisterId) ? Tools.ToInt32(inputClass.SInput[InputType.RegisterId].ToString(), 0) : 0;
            string serialNO = inputClass.SInput.ContainsKey(InputType.SerialNO) ? inputClass.SInput[InputType.SerialNO].ToString() : "";
            
            try
            {
                //解析返回结果到类，保存信息
                MI_Register register = hisDao.Mz_Getregister(registerId, "");
                register.ValidFlag = 1;
                register.SerialNO = serialNO;
                ResultClass regResultClass = hisDao.Mz_SaveRegister(register);
                //更新交易信息
                if (regResultClass.bSucess)
                {
                    MI_MedicalInsurancePayRecord medicalInsurancePayRecord = hisDao.Mz_GetPayRecord(1, register.ID.ToString(), 1, 0);
                    if (medicalInsurancePayRecord != null)
                    {
                        medicalInsurancePayRecord.PersonCount = personcount;
                        medicalInsurancePayRecord.state = 1;
                        ResultClass recordResultClass = hisDao.Mz_SavePayRecord(medicalInsurancePayRecord);

                        if (recordResultClass.bSucess)
                        {   //返回数据到前台界面，只需金额
                            resultClass.bSucess = true;
                            //返回数据到前台界面，只需金额
                            resultDic.Add("personcount", personcount.ToString());
                            resultDic.Add("serialNO", serialNO);
                            resultDic.Add("tradeNo", medicalInsurancePayRecord.TradeNO);
                            resultDic.Add("invoiceNo", medicalInsurancePayRecord.FeeNO);
                        }
                        else
                        {
                            resultClass.bSucess = false;
                            resultClass.sRemarks = "更新交易信息失败！";
                        }
                    }
                    else
                    {
                        resultClass.bSucess = false;
                        resultClass.sRemarks = "未获取到交易数据！";
                    }
                }
                else
                {
                    resultClass.bSucess = false;
                    resultClass.sRemarks = "更新登记信息失败！";
                }
                resultClass.oResult = resultDic;
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = "更新中间表数据报错:" + e.Message;
            }
            return resultClass;
        }
        public override ResultClass MZ_UpdateRegister(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            int registerId = inputClass.SInput.ContainsKey(InputType.RegisterId) ? Tools.ToInt32(inputClass.SInput[InputType.RegisterId].ToString(), 0) : 0;
            string serialNO = inputClass.SInput.ContainsKey(InputType.SerialNO) ? inputClass.SInput[InputType.SerialNO].ToString() : "";
            string invoiceNo = inputClass.SInput.ContainsKey(InputType.InvoiceNo) ? inputClass.SInput[InputType.InvoiceNo].ToString() : "";

            try
            {
                //解析返回结果到类，保存信息
                MI_Register register = hisDao.Mz_Getregister(registerId, "");
                register.ValidFlag = 1;
                register.SerialNO = serialNO;

                ResultClass regResultClass = hisDao.Mz_SaveRegister(register);
                //更新交易信息
                //解析返回结果到类，保存
                MI_MedicalInsurancePayRecord medicalInsurancePayRecord = hisDao.Mz_GetPayRecord(1, registerId.ToString(), 1, 1);
               
                medicalInsurancePayRecord.FeeNO = invoiceNo;
                resultClass = hisDao.Mz_SavePayRecord(medicalInsurancePayRecord);

                if (resultClass.bSucess)
                {
                    MI_MIPayRecordHead mIPayRecordHead = hisDao.Mz_GetPayRecordHead(medicalInsurancePayRecord.ID);
                    DataTable dtPayrecordDetail = hisDao.Mz_GetPayRecordDetailForPrint(medicalInsurancePayRecord.ID);

                    List<MI_MedicalInsurancePayRecord> result1 = new List<MI_MedicalInsurancePayRecord>();
                    result1.Add(medicalInsurancePayRecord);
                    DataTable dtPayrecord = ConvertExtend.ToDataTable(result1);

                    List<MI_MIPayRecordHead> result2 = new List<MI_MIPayRecordHead>();
                    result2.Add(mIPayRecordHead);
                    DataTable dtPayrecordHead = ConvertExtend.ToDataTable(result2);

                    List<DataTable> objects = new List<DataTable>();
                    objects.Add(dtPayrecord);
                    objects.Add(dtPayrecordHead);
                    objects.Add(dtPayrecordDetail);

                    //返回数据到前台界面，只需金额
                    resultClass.bSucess = true;
                    resultClass.sRemarks= medicalInsurancePayRecord.TradeNO;
                    resultClass.oResult = objects;
                }
                else
                {
                    resultClass.bSucess = false;
                    resultClass.sRemarks = "更新登记信息失败！";
                }

            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = "更新中间表数据报错:" + e.Message;
            }

            return resultClass;
        }

        //public override ResultClass Mz_CancelRegister(InputClass inputClass)//string serialNO)
        //{
        //    ResultClass resultClass = new ResultClass();
        //    try
        //    {
        //        resultDic.Clear();
        //        MI_Register register = JsonHelper.DeserializeJsonToObject<MI_Register>(inputClass.SInput[InputType.MI_Register].ToString());
        //        MI_MedicalInsurancePayRecord medicalInsurancePayRecord = JsonHelper.DeserializeJsonToObject<MI_MedicalInsurancePayRecord>(inputClass.SInput[InputType.MI_MedicalInsurancePayRecord].ToString());
        //        MI_MIPayRecordHead mIPayRecordHead = JsonHelper.DeserializeJsonToObject<MI_MIPayRecordHead>(inputClass.SInput[InputType.MI_MIPayRecordHead].ToString());
        //        List<MI_MIPayRecordDetail> mIPayRecordDetailList = JsonHelper.DeserializeJsonToList<MI_MIPayRecordDetail>(inputClass.SInput[InputType.MI_MIPayRecordDetailList].ToString());


        //        //更新登记表
        //        ResultClass regResultClass = hisDao.Mz_SaveRegister(register);
        //        //更新交易表
        //        MI_MedicalInsurancePayRecord medicalInsurancePayRecordResult = hisDao.Mz_GetPayRecord(1, register.ID.ToString(), 1, 1);
        //        if (medicalInsurancePayRecordResult != null)
        //        {
        //            medicalInsurancePayRecordResult.state = 3;
        //            hisDao.Mz_SavePayRecord(medicalInsurancePayRecordResult);
        //        }

        //        medicalInsurancePayRecord.FeeNO = medicalInsurancePayRecordResult.FeeNO;

        //        if (regResultClass.bSucess)
        //        {
        //            if (hisDao.SaveTradeInfo(medicalInsurancePayRecord, mIPayRecordHead, mIPayRecordDetailList) == null)
        //            {
        //                resultClass.bSucess = false;
        //                resultClass.sRemarks = "更新交易信息失败！";
        //            }
        //            else
        //            {
        //                //返回数据到前台界面，只需金额
        //                resultClass.bSucess = true;
        //            }
        //        }
        //        else
        //        {
        //            resultClass.bSucess = false;
        //            resultClass.sRemarks = "更新登记信息失败！";
        //        }

        //        resultClass.oResult = resultDic;
        //    }
        //    catch (Exception e)
        //    {
        //        resultClass.bSucess = false;
        //        resultClass.sRemarks = e.Message;
        //    }
        //    return resultClass;
        //}
        /// <summary>
        /// 预取消挂号：保存预取消信息 并返回记录ID
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public override ResultClass Mz_CancelRegister(InputClass inputClass)//string serialNO)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                MI_Register register = JsonHelper.DeserializeJsonToObject<MI_Register>(inputClass.SInput[InputType.MI_Register].ToString());
                MI_MedicalInsurancePayRecord medicalInsurancePayRecord = JsonHelper.DeserializeJsonToObject<MI_MedicalInsurancePayRecord>(inputClass.SInput[InputType.MI_MedicalInsurancePayRecord].ToString());
                MI_MIPayRecordHead mIPayRecordHead = JsonHelper.DeserializeJsonToObject<MI_MIPayRecordHead>(inputClass.SInput[InputType.MI_MIPayRecordHead].ToString());
                List<MI_MIPayRecordDetail> mIPayRecordDetailList = JsonHelper.DeserializeJsonToList<MI_MIPayRecordDetail>(inputClass.SInput[InputType.MI_MIPayRecordDetailList].ToString());

                MI_MedicalInsurancePayRecord medicalInsurancePayRecordNew = hisDao.SaveTradeInfo(medicalInsurancePayRecord, mIPayRecordHead, mIPayRecordDetailList);
                if (medicalInsurancePayRecordNew == null)
                {
                    resultClass.bSucess = false;
                    resultClass.sRemarks = "更新交易信息失败！";
                }
                else
                {
                    //返回新数据ID
                    resultClass.bSucess = true;
                    resultClass.oResult = medicalInsurancePayRecordNew.ID;
                }

            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
            }
            return resultClass;
        }
        /// <summary>
        /// 确认取消挂号
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public override ResultClass MZ_CancelRegisterCommit(InputClass inputClass)//)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                resultDic.Clear();
                decimal personcount = inputClass.SInput.ContainsKey(InputType.Money) ? Tools.ToDecimal(inputClass.SInput[InputType.Money].ToString(), 0) : 0;
                int registerId = inputClass.SInput.ContainsKey(InputType.RegisterId) ? Tools.ToInt32(inputClass.SInput[InputType.RegisterId].ToString(), 0) : 0;
                string serialNO = inputClass.SInput.ContainsKey(InputType.SerialNO) ? inputClass.SInput[InputType.SerialNO].ToString() : "";
                int iTradeRecordId = inputClass.SInput.ContainsKey(InputType.TradeRecordId) ? Tools.ToInt32(inputClass.SInput[InputType.TradeRecordId].ToString(), 0) : 0;

                MI_Register register = hisDao.Mz_Getregister(registerId, serialNO);
                register.ValidFlag = 2;
                //更新登记表
                ResultClass regResultClass = hisDao.Mz_SaveRegister(register);
                //更新原交易表
                MI_MedicalInsurancePayRecord medicalInsurancePayRecordOld = hisDao.Mz_GetPayRecord(1, register.ID.ToString(), 1, 1);
                if (medicalInsurancePayRecordOld != null)
                {
                    medicalInsurancePayRecordOld.state = 3;
                    hisDao.Mz_SavePayRecord(medicalInsurancePayRecordOld);

                    MI_MedicalInsurancePayRecord medicalInsurancePayRecordNew = hisDao.Mz_GetPayRecord(0, iTradeRecordId.ToString(), 1, 0);
                    if (medicalInsurancePayRecordNew != null)
                    {
                        //medicalInsurancePayRecordNew.FeeNO = medicalInsurancePayRecordOld.FeeNO;
                        medicalInsurancePayRecordNew.state = 2;
                        medicalInsurancePayRecordNew.PersonCount = personcount;
                        medicalInsurancePayRecordNew.PersonCountPay = Convert.ToDecimal(medicalInsurancePayRecordOld.PersonCountPay)*-1;
                        hisDao.Mz_SavePayRecord(medicalInsurancePayRecordNew);
                    }

                    MI_MIPayRecordHead mIPayRecordHead = hisDao.Mz_GetPayRecordHead(medicalInsurancePayRecordNew.ID);
                    DataTable dtPayrecordDetail = hisDao.Mz_GetPayRecordDetailForPrint(medicalInsurancePayRecordNew.ID);

                    List<MI_MedicalInsurancePayRecord> result1 = new List<MI_MedicalInsurancePayRecord>();
                    result1.Add(medicalInsurancePayRecordNew);
                    DataTable dtPayrecord = ConvertExtend.ToDataTable(result1);

                    List<MI_MIPayRecordHead> result2 = new List<MI_MIPayRecordHead>();
                    result2.Add(mIPayRecordHead);
                    DataTable dtPayrecordHead = ConvertExtend.ToDataTable(result2);

                    List<DataTable> objects = new List<DataTable>();
                    objects.Add(dtPayrecord);
                    objects.Add(dtPayrecordHead);
                    objects.Add(dtPayrecordDetail);

                    resultClass.bSucess = true;
                    resultClass.oResult = objects;
                }
                else
                {
                    resultClass.bSucess = false;
                    resultClass.sRemarks = "更新登记信息失败！";
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
            }
            return resultClass;
        }

        public override ResultClass MZ_PreviewCharge(InputClass inputClass)//TradeData tradedata)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                resultDic.Clear();
                MI_MedicalInsurancePayRecord medicalInsurancePayRecord = JsonHelper.DeserializeJsonToObject<MI_MedicalInsurancePayRecord>(inputClass.SInput[InputType.MI_MedicalInsurancePayRecord].ToString());
                MI_MIPayRecordHead mIPayRecordHead = JsonHelper.DeserializeJsonToObject<MI_MIPayRecordHead>(inputClass.SInput[InputType.MI_MIPayRecordHead].ToString());
                List<MI_MIPayRecordDetail> mIPayRecordDetailList = JsonHelper.DeserializeJsonToList<MI_MIPayRecordDetail>(inputClass.SInput[InputType.MI_MIPayRecordDetailList].ToString());

                AddLog("解析完成，开始保存");
                MI_MedicalInsurancePayRecord medicalInsurancePayRecordResult = hisDao.SaveTradeInfo(medicalInsurancePayRecord, mIPayRecordHead, mIPayRecordDetailList);
                AddLog("保存完成，开始返回");
                if (medicalInsurancePayRecordResult.ID > 0)
                {
                    //返回数据到前台界面，只需金额
                    resultDic.Add("Id", medicalInsurancePayRecordResult.ID.ToString());
                    resultDic.Add("tradeno", medicalInsurancePayRecord.TradeNO);
                    resultDic.Add("FeeAll", medicalInsurancePayRecord.FeeAll.ToString());
                    resultDic.Add("fund", medicalInsurancePayRecord.FeeFund.ToString());
                    resultDic.Add("cash", medicalInsurancePayRecord.FeeCash.ToString());
                    resultDic.Add("personcountpay", medicalInsurancePayRecord.PersonCountPay.ToString());
                    resultClass.bSucess = true;
                    resultClass.oResult = resultDic;
                }
                else
                {
                    resultClass.bSucess = false;
                    resultClass.sRemarks = "保存失败，返回ID为零";
                    resultClass.oResult = null; ;
                }

            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
            }
            return resultClass;
        }

        public override ResultClass MZ_Charge(InputClass inputClass)//int tradeRecordId, string fph)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                int tradeRecordId = inputClass.SInput.ContainsKey(InputType.TradeRecordId) ? Tools.ToInt32(inputClass.SInput[InputType.TradeRecordId].ToString(), 0) : 0;
                string fph = inputClass.SInput.ContainsKey(InputType.InvoiceNo) ? inputClass.SInput[InputType.InvoiceNo].ToString() : "";
                decimal personcount = inputClass.SInput.ContainsKey(InputType.Money) ? Tools.ToDecimal(inputClass.SInput[InputType.Money].ToString(), 0) : 0;

                //解析返回结果到类，保存
                MI_MedicalInsurancePayRecord medicalInsurancePayRecord = hisDao.Mz_GetPayRecord(0, tradeRecordId.ToString(), 2, 0);

                medicalInsurancePayRecord.PersonCount = personcount;
                medicalInsurancePayRecord.state = 1;
                medicalInsurancePayRecord.FeeNO = fph;
                resultClass = hisDao.Mz_SavePayRecord(medicalInsurancePayRecord);

                if (resultClass.bSucess)
                {
                    MI_MedicalInsurancePayRecord medicalInsurancePayRecordResult = resultClass.oResult as MI_MedicalInsurancePayRecord;
                    MI_MIPayRecordHead mIPayRecordHead = hisDao.Mz_GetPayRecordHead(tradeRecordId);
                    DataTable dtPayrecordDetail = hisDao.Mz_GetPayRecordDetailForPrint(tradeRecordId);
                    List<MI_MedicalInsurancePayRecord> result1 = new List<MI_MedicalInsurancePayRecord>();
                    result1.Add(medicalInsurancePayRecordResult);
                    DataTable dtPayrecord = ConvertExtend.ToDataTable(result1);

                    List<MI_MIPayRecordHead> result2 = new List<MI_MIPayRecordHead>();
                    result2.Add(mIPayRecordHead);
                    DataTable dtPayrecordHead = ConvertExtend.ToDataTable(result2);

                    List<DataTable> objects = new List<DataTable>();
                    objects.Add(dtPayrecord);
                    objects.Add(dtPayrecordHead);
                    objects.Add(dtPayrecordDetail);
                    //返回数据到前台界面，只需金额
                    resultClass.bSucess = true;
                    resultClass.sRemarks = medicalInsurancePayRecord.TradeNO;
                    resultClass.oResult = objects;
                 }
                else
                {
                    resultClass.bSucess = false;
                    resultClass.sRemarks = "更新登记信息失败！";
                }

            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
            }
            return resultClass;
        }
        /// <summary>
        /// 预取消收费
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public override ResultClass MZ_CancelCharge(InputClass inputClass)//string fph)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                MI_MedicalInsurancePayRecord medicalInsurancePayRecord= JsonHelper.DeserializeJsonToObject<MI_MedicalInsurancePayRecord>(inputClass.SInput[InputType.MI_MedicalInsurancePayRecord].ToString());
                MI_MIPayRecordHead mIPayRecordHead = JsonHelper.DeserializeJsonToObject<MI_MIPayRecordHead>(inputClass.SInput[InputType.MI_MIPayRecordHead].ToString());
                List<MI_MIPayRecordDetail> mIPayRecordDetailList = JsonHelper.DeserializeJsonToList<MI_MIPayRecordDetail>(inputClass.SInput[InputType.MI_MIPayRecordDetailList].ToString());

                //保存新信息
                MI_MedicalInsurancePayRecord medicalInsurancePayRecordResult = hisDao.SaveTradeInfo(medicalInsurancePayRecord, mIPayRecordHead, mIPayRecordDetailList);
                
                #region 返回结果用于打印
                MI_MIPayRecordHead mIPayRecordHeadResult = hisDao.Mz_GetPayRecordHead(medicalInsurancePayRecordResult.ID);
                DataTable dtPayrecordDetail = hisDao.Mz_GetPayRecordDetailForPrint(medicalInsurancePayRecordResult.ID);

                List<MI_MedicalInsurancePayRecord> result1 = new List<MI_MedicalInsurancePayRecord>();
                result1.Add(medicalInsurancePayRecordResult);
                DataTable dtPayrecord = ConvertExtend.ToDataTable(result1);

                List<MI_MIPayRecordHead> result2 = new List<MI_MIPayRecordHead>();
                result2.Add(mIPayRecordHeadResult);
                DataTable dtPayrecordHead = ConvertExtend.ToDataTable(result2);

                List<DataTable> objects = new List<DataTable>();
                objects.Add(dtPayrecord);
                objects.Add(dtPayrecordHead);
                objects.Add(dtPayrecordDetail);
                //返回数据到前台界面，只需金额
                resultClass.oResult = objects;
                #endregion

                if (medicalInsurancePayRecordResult != null)
                {   //返回数据到前台界面，只需金额
                    resultClass.bSucess = true;
                }
                else
                {
                    resultClass.bSucess = false;
                    resultClass.sRemarks = "更新收费信息失败！";
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
            }
            return resultClass;
        }
        /// <summary>
        /// 确认取消收费
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public override ResultClass MZ_CancelChargeCommit(InputClass inputClass)//)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                resultDic.Clear();
                decimal personcount = inputClass.SInput.ContainsKey(InputType.Money) ? Tools.ToDecimal(inputClass.SInput[InputType.Money].ToString(), 0) : 0;
                string fph = inputClass.SInput.ContainsKey(InputType.InvoiceNo) ? inputClass.SInput[InputType.InvoiceNo].ToString() : "";

                MI_MedicalInsurancePayRecord medicalInsurancePayRecordOld = hisDao.Mz_GetPayRecord(2, fph, 2, 1);
                if (medicalInsurancePayRecordOld != null)
                {
                    //更新原交易表
                    medicalInsurancePayRecordOld.state = 3;
                    hisDao.Mz_SavePayRecord(medicalInsurancePayRecordOld);
                    //更新新交易表
                    MI_MedicalInsurancePayRecord medicalInsurancePayRecordNew = hisDao.Mz_GetPayRecord(2, fph, 2, 5);
                    if (medicalInsurancePayRecordNew != null)
                    {
                        medicalInsurancePayRecordNew.state = 2;
                        medicalInsurancePayRecordNew.FeeNO = "";
                        medicalInsurancePayRecordNew.PersonCount = personcount;
                        medicalInsurancePayRecordNew.PersonCountPay = medicalInsurancePayRecordOld.PersonCountPay * -1;
                        hisDao.Mz_SavePayRecord(medicalInsurancePayRecordNew);
                    }

                    #region 返回结果用于打印
                    MI_MIPayRecordHead mIPayRecordHeadResult = hisDao.Mz_GetPayRecordHead(medicalInsurancePayRecordNew.ID);
                    DataTable dtPayrecordDetail = hisDao.Mz_GetPayRecordDetailForPrint(medicalInsurancePayRecordNew.ID);

                    List<MI_MedicalInsurancePayRecord> result1 = new List<MI_MedicalInsurancePayRecord>();
                    result1.Add(medicalInsurancePayRecordNew);
                    DataTable dtPayrecord = ConvertExtend.ToDataTable(result1);

                    List<MI_MIPayRecordHead> result2 = new List<MI_MIPayRecordHead>();
                    result2.Add(mIPayRecordHeadResult);
                    DataTable dtPayrecordHead = ConvertExtend.ToDataTable(result2);

                    List<DataTable> objects = new List<DataTable>();
                    objects.Add(dtPayrecord);
                    objects.Add(dtPayrecordHead);
                    objects.Add(dtPayrecordDetail);
                    //返回数据到前台界面，只需金额
                    resultClass.oResult = objects;
                    #endregion

                    resultClass.bSucess = true;
                }
                else
                {
                    resultClass.bSucess = false;
                    resultClass.sRemarks = "更新登记信息失败！";
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
            }
            return resultClass;
        }

        public override ResultClass MZ_UploadzyPatFee(InputClass inputClass)//)
        {
            throw new NotImplementedException();
        }

        public override ResultClass MZ_DownloadzyPatFee(InputClass inputClass)//)
        {
            throw new NotImplementedException();
        }

        public override ResultClass MZ_LoadFeeDetailByTicketNum(InputClass inputClass)//)
        {
            throw new NotImplementedException();
        }

        public override ResultClass Mz_UpdateTradeRecord(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            string sTradeNo = inputClass.SInput.ContainsKey(InputType.TradeNo) ? inputClass.SInput[InputType.TradeNo].ToString(): "0";
            bool bFlag = inputClass.SInput.ContainsKey(InputType.bFlag) ? (bool)inputClass.SInput[InputType.bFlag] : false;

            try
            {
                //更新交易信息
                //解析返回结果到类，保存
                MI_MedicalInsurancePayRecord medicalInsurancePayRecord = hisDao.Mz_GetPayRecord(3, sTradeNo, 1, 0);
                if (bFlag)
                {
                    medicalInsurancePayRecord.state = 1;
                }
                else
                {
                    medicalInsurancePayRecord.state = 4;
                }
                hisDao.Mz_SavePayRecord(medicalInsurancePayRecord);
                resultClass.bSucess = true;
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = "更新交易记录报错:" + e.Message;
            }

            return resultClass;
        }

        public override ResultClass Mz_GetRegisterTradeNo(string sSerialNO)
        {
            ResultClass resultClass = new ResultClass();

            try
            {
                MI_Register register = hisDao.Mz_Getregister(0, sSerialNO);
                if (register!=null)
                {
                    resultClass.bSucess = true;
                    resultClass.sRemarks = register.SocialCreateNum;
                }
                else
                {
                    resultClass.bSucess = false;
                    resultClass.sRemarks = "未进行医保登记！";
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = "获取挂号信息异常" + e.Message;
            }

            return resultClass;
        }
        #endregion




        #region 公用方法
        private HIS_Entity.MIManage.Common.Reg.root PreviewRegisterToInput(MI_Register register)
        {

            HIS_Entity.MIManage.Common.Reg.root root = new HIS_Entity.MIManage.Common.Reg.root();
            HIS_Entity.MIManage.Common.Reg.input input = new HIS_Entity.MIManage.Common.Reg.input();
            root.input = input;
            #region tradeinfo 交易信息 curetype/billtype 必填
            HIS_Entity.MIManage.Common.Reg.tradeinfo tradeinfo = new HIS_Entity.MIManage.Common.Reg.tradeinfo();
            tradeinfo.curetype = "17";
            tradeinfo.billtype = "0";

            tradeinfo.feeno = "0";
            tradeinfo.operator1 = register.StaffName;
            input.tradeinfo = tradeinfo;
            #endregion

            #region  处方信息 diagnoseno/recipeno/recipedate/recipetype/helpmedicineflag 必填
            HIS_Entity.MIManage.Common.Reg.recipearray recipearray = new HIS_Entity.MIManage.Common.Reg.recipearray();
            HIS_Entity.MIManage.Common.Reg.recipe recipe = new HIS_Entity.MIManage.Common.Reg.recipe();
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
            HIS_Entity.MIManage.Common.Reg.recipe[] recipes = { recipe };
            recipearray.recipe = recipes;

            input.recipearray = recipearray;
            #endregion

            #region 明细信息 itemno/recipeno/hiscode/itemname/itemtype/unitprice/count/fee/babyflag 必填
            HIS_Entity.MIManage.Common.Reg.feeitemarray feeitemarray = new HIS_Entity.MIManage.Common.Reg.feeitemarray();
            HIS_Entity.MIManage.Common.Reg.feeitem feeitem = new HIS_Entity.MIManage.Common.Reg.feeitem();
            feeitem.itemno = "1";
            feeitem.recipeno = "1";
            feeitem.hiscode = System.DateTime.Now.ToString("yyyyMMdd hhmmss");
            feeitem.itemname = "1";
            feeitem.itemtype = "0";
            feeitem.unitprice = register.DeptName;
            feeitem.count = register.DiagnDocID;
            feeitem.fee = register.Doctor;
            feeitem.babyflag = "0";

            HIS_Entity.MIManage.Common.Reg.feeitem[] feeitems = { feeitem };
            recipearray.recipe = recipes;

            input.feeitemarray = feeitemarray;
            #endregion
            return root;
        }

        private HIS_Entity.MIManage.Common.Reg.root TradeDataToInput(TradeData tradeData)
        {
            HIS_Entity.MIManage.Common.Reg.root root = new HIS_Entity.MIManage.Common.Reg.root();
            HIS_Entity.MIManage.Common.Reg.input input = new HIS_Entity.MIManage.Common.Reg.input();
            root.input = input;

            #region tradeinfo 交易信息 curetype/billtype 必填
            HIS_Entity.MIManage.Common.Reg.tradeinfo tradeinfo = new HIS_Entity.MIManage.Common.Reg.tradeinfo();
            tradeinfo.curetype = ((int)tradeData.tradeinfo.tradeType).ToString();
            tradeinfo.billtype = tradeData.tradeinfo.billtype;

            tradeinfo.feeno = "0";
            tradeinfo.operator1 = "";
            input.tradeinfo = tradeinfo;
            #endregion

            #region  处方信息 diagnoseno/recipeno/recipedate/recipetype/helpmedicineflag 必填
            HIS_Entity.MIManage.Common.Reg.recipearray recipearray = new HIS_Entity.MIManage.Common.Reg.recipearray();
            HIS_Entity.MIManage.Common.Reg.recipe[] recipes = new HIS_Entity.MIManage.Common.Reg.recipe[tradeData.recipeList.recipes.Count];
            for (int i = 0; i < tradeData.recipeList.recipes.Count; i++)
            {
                HIS_Entity.MIManage.Common.Reg.recipe recipe = new HIS_Entity.MIManage.Common.Reg.recipe();
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
            HIS_Entity.MIManage.Common.Reg.feeitemarray feeitemarray = new HIS_Entity.MIManage.Common.Reg.feeitemarray();
            HIS_Entity.MIManage.Common.Reg.feeitem[] feeitems = new HIS_Entity.MIManage.Common.Reg.feeitem[tradeData.feeitemList.feeitems.Count];
            for (int i = 0; i < tradeData.feeitemList.feeitems.Count; i++)
            {
                HIS_Entity.MIManage.Common.Reg.feeitem feeitem = new HIS_Entity.MIManage.Common.Reg.feeitem();
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
        private MI_MedicalInsurancePayRecord ResultToPayRecord(HIS_Entity.MIManage.Common.DivideResult.output output)
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

        private MI_MIPayRecordHead ResultToPayRecordHead(HIS_Entity.MIManage.Common.DivideResult.output output)
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

        private List<MI_MIPayRecordDetail> ResultToPayRecordDetail(HIS_Entity.MIManage.Common.DivideResult.output output)
        {
            List<MI_MIPayRecordDetail> mIPayRecordDetailList = new List<MI_MIPayRecordDetail>();
            if (output.feeitems.Length > 0)
            {
                foreach (HIS_Entity.MIManage.Common.DivideResult.feeitem item in output.feeitems)
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

        private HIS_Entity.MIManage.Common.Reg.root RegisterToInput(MI_Register register)
        {
            HIS_Entity.MIManage.Common.Reg.root root = new HIS_Entity.MIManage.Common.Reg.root();
            HIS_Entity.MIManage.Common.Reg.input input = new HIS_Entity.MIManage.Common.Reg.input();
            root.input = input;

            HIS_Entity.MIManage.Common.Reg.tradeinfo tradeinfo = new HIS_Entity.MIManage.Common.Reg.tradeinfo();
            tradeinfo.curetype = "11";
            tradeinfo.billtype = "0";
            tradeinfo.feeno = "0";
            tradeinfo.operator1 = register.StaffName;
            input.tradeinfo = tradeinfo;

            HIS_Entity.MIManage.Common.Reg.recipearray recipearray = new HIS_Entity.MIManage.Common.Reg.recipearray();
            HIS_Entity.MIManage.Common.Reg.recipe recipe = new HIS_Entity.MIManage.Common.Reg.recipe();
            recipe.hissectionname = register.DeptName;
            recipe.drid = register.DiagnDocID;
            recipe.drname = register.Doctor;
            recipe.registertradeno = register.SerialNO.ToString();
            recipe.billstype = "1";
            HIS_Entity.MIManage.Common.Reg.recipe[] recipes = { recipe };
            recipearray.recipe = recipes;

            input.recipearray = recipearray;

            HIS_Entity.MIManage.Common.Reg.feeitemarray feeitemarray = new HIS_Entity.MIManage.Common.Reg.feeitemarray();
            input.feeitemarray = feeitemarray;

            return root;
        }

        private void AddLog(string s)
        {
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, s);
        }
        #endregion
    }
}
