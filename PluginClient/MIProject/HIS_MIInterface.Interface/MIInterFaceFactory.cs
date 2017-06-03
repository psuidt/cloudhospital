using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.Common;
using HIS_Entity.MIManage;
using HIS_MIInterface.Interface.BaseClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_MIInterface.Interface
{
    public static class MIInterFaceFactory
    {
        public static bool bPrinter = false;
        public static SysLoginRight SysLoginRight
        {
            get
            {
                return (SysLoginRight)EFWCoreLib.CoreFrame.Init.AppGlobal.cache.GetData("RoleUser");
            }
        }
        static AbstractAction abstractAction;
        #region 门诊
        static AbstractAction AbstractAction
        {
            get
            {
                switch ("北京健恒")
                {
                    case "例子":
                        if (abstractAction is CustomAction.例子.CustomAction)
                        {
                            return abstractAction;
                        }
                        else
                        {
                            abstractAction = new CustomAction.例子.CustomAction(SysLoginRight.WorkId);
                            return abstractAction;
                        }
                    case "北京":
                        if (abstractAction is CustomAction.北京.CustomAction)
                        {
                            return abstractAction;
                        }
                        else
                        {
                            abstractAction = new CustomAction.北京.CustomAction(SysLoginRight.WorkId);
                            return abstractAction;
                        }
                    case "北京健恒":
                        if (abstractAction is CustomAction.北京健恒.CustomAction)
                        {
                            return abstractAction;
                        }
                        else
                        {
                            abstractAction = new CustomAction.北京健恒.CustomAction(SysLoginRight.WorkId);
                            return abstractAction;
                        }
                    default:
                        return null;
                }
            }
        }
        #region 读卡
        /// <summary>
        /// 读卡测试，查看该卡是否有效
        /// </summary>
        /// <param name="inputClass">InputType.CardNo 医保卡号，直接读卡则实例化不赋值</param>
        /// <returns>返回ResultClass oResult为DataTable</returns>
        public static ResultClass MZ_GetCardInfo(InputClass inputClass)
        {
            return ((MIMzInterface)AbstractAction).MZ_GetCardInfo(inputClass);
        }
        /// <summary>
        /// 获取卡病人信息，查看该卡是否有效
        /// </summary>
        /// <param name="inputClass">InputType.CardNo 医保卡号，直接读卡则实例化不赋值</param>
        /// <returns>返回ResultClass oResult为PatientInfo</returns>
        public static ResultClass MZ_GetPersonInfo(InputClass inputClass)
        {
            return ((MIMzInterface)AbstractAction).MZ_GetPersonInfo(inputClass);
        }
        #endregion

        #region 挂号
        /// <summary>
        /// 门诊预登记
        /// </summary>
        /// <param name="inputClass">InputType.MI_Register 实体类</param>
        /// <returns>返回ResultClass oResult为Dictionary<string, string> Id:交易ID; tradeno：医保流水号; FeeAll:总额; fund:统筹; cash:现金 ; personcountpay:个帐支付 DataTable</returns>
        public static ResultClass MZ_PreviewRegister(InputClass inputClass)
        {
            return ((MIMzInterface)AbstractAction).MZ_PreviewRegister(inputClass);
        }
        /// <summary>
        /// 门诊确认登记
        /// </summary>
        /// <param name="inputClass">InputType.registerId 登记ID InputType.serialNO门诊号/住院号</param>
        /// <returns>返回ResultClass oResult为Dictionary<string,string>  personcount:个帐余额</returns>
        public static ResultClass MZ_Register(InputClass inputClass)
        {
            ResultClass resultClass = ((MIMzInterface)AbstractAction).MZ_Register(inputClass);
            return resultClass;
        }
        ///// <summary>
        ///// 回滚门诊确认登记
        ///// </summary>
        ///// <param name="inputClass">InputType.registerId 登记ID InputType.serialNO门诊号/住院号</param>
        ///// <returns>返回ResultClass 无object</returns>
        //public static ResultClass MZ_RegisterRollBack(InputClass inputClass)
        //{
        //    return ((MIMzInterface)AbstractAction).MZ_RegisterRollBack(inputClass);
        //}
        /// <summary>
        /// 挂号完成之后更新登记表
        /// </summary>
        /// <param name="inputClass">InputType.registerId 登记ID InputType.serialNO门诊号/住院号</param>
        /// <returns>仅返回成功失败 无object</returns>
        public static ResultClass MZ_UpdateRegister(InputClass inputClass)
        {
            ResultClass resultClass = ((MIMzInterface)AbstractAction).MZ_UpdateRegister(inputClass);
            try
            {
                if (resultClass.bSucess)
                {
                    string sTradeNo = resultClass.sRemarks;
                    List<DataTable> objects = (List<DataTable>)resultClass.oResult;
                    if (inputClass.SInput.ContainsKey(InputType.TradeNo))
                    {
                        inputClass.SInput[InputType.TradeNo] = sTradeNo;
                    }
                    else
                    {
                        inputClass.SInput.Add(InputType.TradeNo, sTradeNo);
                    }

                    if (bPrinter)
                    {
                        ResultClass resultPrint = MZ_PrintInvoice(inputClass);

                        //if (!resultPrint.bSucess)
                        //{ 
                        //    DataTable dtPayrecordDetail = objects[2];
                        //    MZ_MITradePrint(objects[0], objects[1], dtPayrecordDetail);
                        //}
                    }
                    string sPersonCount = objects[0].Rows[0]["PersonCount"].ToString();
                    resultClass.sRemarks = sPersonCount;
                }
            }
            catch (Exception e)
            {
                resultClass.sRemarks = e.Message;
            }
            return resultClass;
        }
        #endregion

        #region 退号
        /// <summary>
        /// 取消登记
        /// </summary>
        /// <param name="inputClass">InputType.RegisterId中间表ID InputType.serialNO门诊号</param>
        /// <returns>返回ResultClass 无object</returns>
        public static ResultClass MZ_CancelRegister(InputClass inputClass)
        {
            //取消挂号试算
            ResultClass resultClass = ((MIMzInterface)AbstractAction).MZ_CancelRegister(inputClass);
            if (resultClass.bSucess)
            {
                #region 确认取消
                if (inputClass.SInput.ContainsKey(InputType.TradeRecordId))
                {
                    inputClass.SInput[InputType.TradeRecordId] = resultClass.oResult;
                }
                else
                {
                    inputClass.SInput.Add(InputType.TradeRecordId, resultClass.oResult);
                }
                //取消挂号确认
                ResultClass resultClass1 = ((MIMzInterface)AbstractAction).MZ_CancelRegisterCommit(inputClass);
                #endregion


                if (resultClass1.bSucess)
                {
                    resultClass.sRemarks = resultClass.sRemarks;
                    List<DataTable> objects = (List<DataTable>)resultClass1.oResult;

                    DataTable dtPayrecordDetail = objects[2];

                    string sTradeNo = objects[0].Rows[0]["TradeNO"].ToString();
                    if (inputClass.SInput.ContainsKey(InputType.TradeNo))
                    {
                        inputClass.SInput[InputType.TradeNo] = sTradeNo;
                    }
                    else
                    {
                        inputClass.SInput.Add(InputType.TradeNo, sTradeNo);
                    }

                    if (bPrinter)
                    {
                        ResultClass resultPrint = MZ_PrintInvoice(inputClass);
                    }
                    //string sPersonCount = objects[0].Rows[0]["PersonCount"].ToString();
                    resultClass.sRemarks = resultClass1.sRemarks;

                    //if (!resultPrint.bSucess)
                    //{
                    //    MZ_MITradePrint(objects[0], objects[1], dtPayrecordDetail);
                    //    return resultClass;
                    //}
                }
                else
                {
                    return resultClass1;
                }
            }
            return resultClass;
        }
        #endregion

        #region 收费
        /// <summary>
        /// 预收费
        /// </summary>
        /// <param name="inputClass">InputType.TradeData 实体类</param>
        /// <returns>返回ResultClass oResult为Dictionary<string, string> ID:交易ID; tradeno：医保流水号; FeeAll:总额; fund:统筹; cash:现金 ; personcountpay:个帐支付</returns>
        public static ResultClass MZ_PreviewCharge(InputClass inputClass)
        {            
            //bool bFlag = inputClass.SInput.ContainsKey(InputType.bFlag) ? (bool)inputClass.SInput[InputType.bFlag] : false;
            //if (true)
            //{ }
            //else
            //{ }
            return ((MIMzInterface)AbstractAction).MZ_PreviewCharge(inputClass);
        }
        /// <summary>
        /// 确认收费
        /// </summary>
        /// <param name="inputClass">InputType.TradeRecordId交易ID InputType.InvoiceNo发票号</param>
        /// <returns> 返回ResultClass ResultClass.oResult 为decimal</returns>
        public static ResultClass MZ_Charge(InputClass inputClass)
        {

            ResultClass resultClass = ((MIMzInterface)AbstractAction).MZ_Charge(inputClass);
            try
            {
                if (resultClass.bSucess)
                {
                    List<DataTable> objects = (List<DataTable>)resultClass.oResult;

                    MI_MedicalInsurancePayRecord medicalInsurancePayRecord = ConvertExtend.ToList<MI_MedicalInsurancePayRecord>(objects[0])[0];

                    if (inputClass.SInput.ContainsKey(InputType.TradeNo))
                    {
                        inputClass.SInput[InputType.TradeNo] = medicalInsurancePayRecord.TradeNO;
                    }
                    else
                    {
                        inputClass.SInput.Add(InputType.TradeNo, medicalInsurancePayRecord.TradeNO);
                    }

                    if (inputClass.SInput.ContainsKey(InputType.InvoiceNo))
                    {
                        inputClass.SInput[InputType.InvoiceNo] = medicalInsurancePayRecord.FeeNO;
                    }
                    else
                    {
                        inputClass.SInput.Add(InputType.InvoiceNo, medicalInsurancePayRecord.FeeNO);
                    }

                    if (inputClass.SInput.ContainsKey(InputType.SerialNO))
                    {
                        inputClass.SInput[InputType.SerialNO] = medicalInsurancePayRecord.FeeNO;
                    }
                    else
                    {
                        inputClass.SInput.Add(InputType.SerialNO, medicalInsurancePayRecord.FeeNO);
                    }

                    MI_MIPayRecordHead mIPayRecordHead = ConvertExtend.ToList<MI_MIPayRecordHead>(objects[1])[0];
                    DataTable dtPayrecordDetail = objects[2];

                    if (bPrinter)
                    {
                        ResultClass resultPrint = MZ_PrintInvoice(inputClass);
                        if (resultPrint.oResult != null && resultPrint.oResult.ToString().Equals("1"))
                        {
                            MZ_MITradePrint(objects[0], objects[1], dtPayrecordDetail);
                            resultClass.oResult = medicalInsurancePayRecord.PersonCount;
                        }
                    }
                    else if(dtPayrecordDetail.Rows.Count>19)
                    {
                        MZ_MITradePrint(objects[0], objects[1], dtPayrecordDetail);
                        resultClass.oResult = medicalInsurancePayRecord.PersonCount;
                    }

                    string sPersonCount = objects[0].Rows[0]["PersonCount"].ToString();
                    resultClass.sRemarks = sPersonCount;
                }
            }
            catch (Exception e)
            {
                resultClass.sRemarks = e.Message;
            }
            return resultClass;
        }
        /// <summary>
        /// 取消收费
        /// </summary>
        /// <param name="inputClass">InputType.InvoiceNo发票号</param>
        /// <returns>返回ResultClass 无object</returns>
        public static ResultClass MZ_CancelCharge(InputClass inputClass)
        {
            ResultClass resultClass = ((MIMzInterface)AbstractAction).MZ_CancelCharge(inputClass);
            if (resultClass.bSucess)
            {
                ResultClass resultClass1 = ((MIMzInterface)AbstractAction).MZ_CancelChargeCommit(inputClass);
                if (resultClass1.bSucess)
                {
                    List<DataTable> objects = (List<DataTable>)resultClass1.oResult;
                    if (objects != null)
                    {
                        MI_MedicalInsurancePayRecord medicalInsurancePayRecord = ConvertExtend.ToList<MI_MedicalInsurancePayRecord>(objects[0])[0];
                        MI_MIPayRecordHead mIPayRecordHead = ConvertExtend.ToList<MI_MIPayRecordHead>(objects[1])[0];

                        DataTable dtPayrecordDetail = objects[2];
                        //MZ_MIChangePrint(medicalInsurancePayRecord, mIPayRecordHead, dtPayrecordDetail);

                        string sTradeNo = objects[0].Rows[0]["TradeNO"].ToString();
                        if (inputClass.SInput.ContainsKey(InputType.TradeNo))
                        {
                            inputClass.SInput[InputType.TradeNo] = sTradeNo;
                        }
                        else
                        {
                            inputClass.SInput.Add(InputType.TradeNo, sTradeNo);
                        }

                        //string sFeeNO = objects[0].Rows[0]["FeeNO"].ToString();
                        //if (inputClass.SInput.ContainsKey(InputType.InvoiceNo))
                        //{
                        //    inputClass.SInput[InputType.InvoiceNo] = sFeeNO;
                        //}
                        //else
                        //{
                        //    inputClass.SInput.Add(InputType.InvoiceNo, sFeeNO);
                        //}
                        if (bPrinter)
                        {
                            ResultClass resultPrint = MZ_PrintInvoice(inputClass);

                            if (resultPrint.oResult != null && resultPrint.oResult.ToString().Equals("1"))
                            {
                                MZ_MITradePrint(objects[0], objects[1], dtPayrecordDetail);
                                resultClass.oResult = medicalInsurancePayRecord.PersonCount;
                            }
                        }
                        else if(dtPayrecordDetail.Rows.Count>19)
                        {
                            MZ_MITradePrint(objects[0], objects[1], dtPayrecordDetail);
                            resultClass.oResult = medicalInsurancePayRecord.PersonCount;
                        }
                    }

                    string sPersonCount = objects[0].Rows[0]["PersonCount"].ToString();
                    resultClass.sRemarks = sPersonCount;

                    return resultClass;
                }
                else
                {
                    return resultClass1;
                }
            }
            return resultClass;
        }
        #endregion
        #endregion

        /// <summary>
        /// 重打功能
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public static ResultClass MZ_RePrintInvoice(InputClass inputClass)
        {
            return MZ_PrintInvoice(inputClass);
        }
        /// <summary>
        /// 打印发票 tradeNo 社保交易号 invoiceNo HIS发票号 businessinfo 业务流水号
        /// </summary>
        /// <param name="inputClass">tradeNo 社保交易号 invoiceNo HIS发票号 businessinfo 业务流水号</param>
        /// <returns></returns>
        public static ResultClass MZ_PrintInvoice(InputClass inputClass)
        {
            return ((MIMzInterface)AbstractAction).MZ_PrintInvoice(inputClass);
        }
        public static ResultClass MZ_CommitTradeState(InputClass inputClass)
        {
            return ((MIMzInterface)AbstractAction).MZ_CommitTradeState(inputClass);
        }

        private static bool MZ_MIChangePrint(MI_MedicalInsurancePayRecord medicalInsurancePayRecord, MI_MIPayRecordHead mIPayRecordHead, DataTable dtMIPayRecordDetailList)
        {
            #region 参数字段
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            myDictionary.Add("Title", medicalInsurancePayRecord.TradeNO);
            myDictionary.Add("TradeNO", medicalInsurancePayRecord.TradeNO);
            myDictionary.Add("Id", medicalInsurancePayRecord.ID);
            myDictionary.Add("PatientName", medicalInsurancePayRecord.PatientName);
            myDictionary.Add("ApplyNO", medicalInsurancePayRecord.ApplyNO);
            myDictionary.Add("FeeMIIn", medicalInsurancePayRecord.FeeMIIn);
            myDictionary.Add("FeeFund", medicalInsurancePayRecord.FeeFund);
            //myDictionary.Add("个人负担-Parameter4", medicalInsurancePayRecord);
            myDictionary.Add("FeeCash", medicalInsurancePayRecord.FeeCash);
            myDictionary.Add("PersonCountPay", medicalInsurancePayRecord.PersonCountPay);
            myDictionary.Add("PersonCount", medicalInsurancePayRecord.PersonCount);
            myDictionary.Add("FeeBigPay", medicalInsurancePayRecord.FeeBigPay);
            //myDictionary.Add("StaffName", medicalInsurancePayRecord.);
            myDictionary.Add("FeeAll", medicalInsurancePayRecord.FeeAll);

            myDictionary.Add("medicine", mIPayRecordHead.medicine);
            myDictionary.Add("tmedicine", mIPayRecordHead.tmedicine);
            myDictionary.Add("therb", mIPayRecordHead.therb);
            myDictionary.Add("examine", mIPayRecordHead.examine);
            myDictionary.Add("labexam", mIPayRecordHead.labexam);
            myDictionary.Add("treatment", mIPayRecordHead.treatment);
            myDictionary.Add("operation", mIPayRecordHead.operation);
            myDictionary.Add("material", mIPayRecordHead.material);
            myDictionary.Add("other", mIPayRecordHead.other);
            myDictionary.Add("xray", mIPayRecordHead.xray);
            myDictionary.Add("ultrasonic", mIPayRecordHead.ultrasonic);
            myDictionary.Add("CT", mIPayRecordHead.CT);
            myDictionary.Add("mri", mIPayRecordHead.mri);
            myDictionary.Add("oxygen", mIPayRecordHead.oxygen);
            myDictionary.Add("bloodt", mIPayRecordHead.bloodt);
            myDictionary.Add("orthodontics", mIPayRecordHead.orthodontics);
            myDictionary.Add("prosthesis", mIPayRecordHead.prosthesis);
            myDictionary.Add("forensic_expertise", mIPayRecordHead.forensic_expertise);
            #endregion

            #region 明细表
            DataTable dtPrint = dtMIPayRecordDetailList.Clone();
            foreach (DataColumn dc in dtMIPayRecordDetailList.Columns)
            {
                dtPrint.Columns.Add(dc.ColumnName + "1", dc.DataType);
            }
            for (int i = 0; i < dtMIPayRecordDetailList.Rows.Count; i++)
            {
                if (!Convert.ToBoolean(i % 2))  //偶数行
                {
                    dtPrint.ImportRow(dtMIPayRecordDetailList.Rows[i]);
                }
                else
                {
                    int j = i / 2;
                    dtPrint.Rows[j]["ItemName1"] = dtMIPayRecordDetailList.Rows[i]["ItemName"];
                    dtPrint.Rows[j]["Spec1"] = dtMIPayRecordDetailList.Rows[i]["Spec"];
                    dtPrint.Rows[j]["Unit1"] = dtMIPayRecordDetailList.Rows[i]["Unit"];
                    dtPrint.Rows[j]["UnitPrice1"] = dtMIPayRecordDetailList.Rows[i]["UnitPrice"];
                    dtPrint.Rows[j]["Count1"] = dtMIPayRecordDetailList.Rows[i]["Count"];
                    dtPrint.Rows[j]["Fee1"] = dtMIPayRecordDetailList.Rows[i]["Fee"];
                    dtPrint.Rows[j]["YBItemLevel1"] = dtMIPayRecordDetailList.Rows[i]["YBItemLevel"];
                }
            }
            #endregion

            EfwControls.CustomControl.ReportTool.GetReport(1, 2026, 0, myDictionary, dtPrint).Print(true);
            //EfwControls.CustomControl.ReportTool.GetReport("医保收费.grf", 0, myDictionary, dtPrint).Print(true);
            return true;
        }

        private static bool MZ_MITradePrint(DataTable dtMedicalInsurancePayRecord, DataTable dtMIPayRecordHead, DataTable dtMIPayRecordDetailList)
        {
            MI_MedicalInsurancePayRecord medicalInsurancePayRecord = ConvertExtend.ToList<MI_MedicalInsurancePayRecord>(dtMedicalInsurancePayRecord)[0];
            MI_MIPayRecordHead mIPayRecordHead = ConvertExtend.ToList<MI_MIPayRecordHead>(dtMIPayRecordHead)[0];

            #region 参数字段
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            myDictionary.Add("HospitalName", SysLoginRight.WorkName);
            myDictionary.Add("TradeNO", medicalInsurancePayRecord.TradeNO);
            myDictionary.Add("Id", medicalInsurancePayRecord.ID);
            myDictionary.Add("PatientName", medicalInsurancePayRecord.PatientName);
            myDictionary.Add("ApplyNO", medicalInsurancePayRecord.ApplyNO);
            myDictionary.Add("StaffName", SysLoginRight.EmpName);
            myDictionary.Add("FeeMIIn", medicalInsurancePayRecord.FeeMIIn);
            myDictionary.Add("FeeFund", medicalInsurancePayRecord.FeeFund);
            myDictionary.Add("FeeCash", medicalInsurancePayRecord.FeeCash);
            myDictionary.Add("PersonCountPay", medicalInsurancePayRecord.PersonCountPay);
            myDictionary.Add("PersonCount", medicalInsurancePayRecord.PersonCount);
            myDictionary.Add("FeeBigPay", medicalInsurancePayRecord.FeeBigPay);
            myDictionary.Add("FeeAll", medicalInsurancePayRecord.FeeAll);

            myDictionary.Add("FeeMIOut", medicalInsurancePayRecord.FeeMIOut);
            myDictionary.Add("FeeDeductible", medicalInsurancePayRecord.FeeDeductible);
            myDictionary.Add("FeeSelfPay", medicalInsurancePayRecord.FeeSelfPay);
            myDictionary.Add("FeeBigSelfPay", medicalInsurancePayRecord.FeeBigSelfPay);
            myDictionary.Add("FeeOutOFPay", medicalInsurancePayRecord.FeeOutOFPay);
            myDictionary.Add("Feebcbx", medicalInsurancePayRecord.Feebcbx);
            myDictionary.Add("Feejcbz", medicalInsurancePayRecord.Feejcbz);
            #endregion

            #region 明细表
            DataTable dtPrint = dtMIPayRecordDetailList.Clone();
            foreach (DataColumn dc in dtMIPayRecordDetailList.Columns)
            {
                dtPrint.Columns.Add(dc.ColumnName + "1", dc.DataType);
            }

            DataTable dtCount = dtMIPayRecordDetailList.Clone();
            #region 添加汇总信息
            DataRow dr = dtCount.NewRow();
            dr["ItemName"] = "西药";
            dr["Fee"] = mIPayRecordHead.medicine;
            dtCount.Rows.Add(dr);

            DataRow dr1 = dtCount.NewRow();
            dr1["ItemName"] = "中成药";
            dr1["Fee"] = mIPayRecordHead.tmedicine;
            dtCount.Rows.Add(dr1);

            DataRow dr2 = dtCount.NewRow();
            dr2["ItemName"] = "中草药";
            dr2["Fee"] = mIPayRecordHead.therb;
            dtCount.Rows.Add(dr2);

            DataRow dr3 = dtCount.NewRow();
            dr3["ItemName"] = "化验";
            dr3["Fee"] = mIPayRecordHead.labexam;
            dtCount.Rows.Add(dr3);

            DataRow dr4 = dtCount.NewRow();
            dr4["ItemName"] = "检查";
            dr4["Fee"] = mIPayRecordHead.examine;
            dtCount.Rows.Add(dr4);

            DataRow dr5 = dtCount.NewRow();
            dr5["ItemName"] = "放射";
            dr5["Fee"] = mIPayRecordHead.xray;
            dtCount.Rows.Add(dr5);

            DataRow dr6 = dtCount.NewRow();
            dr6["ItemName"] = "B超";
            dr6["Fee"] = mIPayRecordHead.ultrasonic;
            dtCount.Rows.Add(dr6);

            DataRow dr7 = dtCount.NewRow();
            dr7["ItemName"] = "CT";
            dr7["Fee"] = mIPayRecordHead.CT;
            dtCount.Rows.Add(dr7);

            DataRow dr8 = dtCount.NewRow();
            dr8["ItemName"] = "核磁";
            dr8["Fee"] = mIPayRecordHead.mri;
            dtCount.Rows.Add(dr8);

            DataRow dr9 = dtCount.NewRow();
            dr9["ItemName"] = "治疗费";
            dr9["Fee"] = mIPayRecordHead.treatment;
            dtCount.Rows.Add(dr9);

            DataRow dr10 = dtCount.NewRow();
            dr10["ItemName"] = "材料费";
            dr10["Fee"] = mIPayRecordHead.material;
            dtCount.Rows.Add(dr10);

            DataRow dr11 = dtCount.NewRow();
            dr11["ItemName"] = "手术费";
            dr11["Fee"] = mIPayRecordHead.operation;
            dtCount.Rows.Add(dr11);

            DataRow dr12 = dtCount.NewRow();
            dr12["ItemName"] = "输氧费";
            dr12["Fee"] = mIPayRecordHead.oxygen;
            dtCount.Rows.Add(dr12);

            DataRow dr13 = dtCount.NewRow();
            dr13["ItemName"] = "输血费";
            dr13["Fee"] = mIPayRecordHead.bloodt;
            dtCount.Rows.Add(dr13);

            DataRow dr14 = dtCount.NewRow();
            dr14["ItemName"] = "正畸费";
            dr14["Fee"] = mIPayRecordHead.orthodontics;
            dtCount.Rows.Add(dr14);

            DataRow dr15 = dtCount.NewRow();
            dr15["ItemName"] = "镶牙费";
            dr15["Fee"] = mIPayRecordHead.prosthesis;
            dtCount.Rows.Add(dr15);


            DataRow dr16 = dtCount.NewRow();
            dr16["ItemName"] = "司法鉴定";
            dr16["Fee"] = mIPayRecordHead.forensic_expertise;
            dtCount.Rows.Add(dr16);


            DataRow dr17 = dtCount.NewRow();
            dr17["ItemName"] = "其他";
            dr17["Fee"] = mIPayRecordHead.other;
            dtCount.Rows.Add(dr17);
            #endregion
            DataRow[] drs = dtCount.Select(" Fee>0");
            int iRowCount = dtPrint.Rows.Count;
            for (int i = 0; i < dtMIPayRecordDetailList.Rows.Count; i++)
            {
                string yblevel = dtMIPayRecordDetailList.Rows[i]["YBItemLevel"].ToString().Trim();
                dtMIPayRecordDetailList.Rows[i]["YBItemLevel"] = yblevel.Equals("1") ? "◇" : (yblevel.Equals("2") ? "△" : yblevel.Equals("3") ? "□" : "□");
                //dtPrint.ImportRow(dtMIPayRecordDetailList.Rows[i]);
            }

            for (int k = 0; k < drs.Length; k++)
            {
                if (!Convert.ToBoolean(k % 2))  //偶数行
                {
                    dtPrint.ImportRow(drs[k]);
                }
                else
                {
                    int j = k / 2;
                    dtPrint.Rows[j]["ItemName1"] = drs[k]["ItemName"];
                    dtPrint.Rows[j]["Fee1"] = drs[k]["Fee"];
                }
            }

            for (int i = 0; i < dtMIPayRecordDetailList.Rows.Count; i++)
            {
                if (!Convert.ToBoolean(i % 2))  //偶数行
                {
                    dtPrint.ImportRow(dtMIPayRecordDetailList.Rows[i]);
                }
                else
                {
                    int j = i / 2 + iRowCount;
                    dtPrint.Rows[j]["ItemName1"] = dtMIPayRecordDetailList.Rows[i]["ItemName"];
                    dtPrint.Rows[j]["Spec1"] = dtMIPayRecordDetailList.Rows[i]["Spec"];
                    dtPrint.Rows[j]["Unit1"] = dtMIPayRecordDetailList.Rows[i]["Unit"];
                    dtPrint.Rows[j]["UnitPrice1"] = dtMIPayRecordDetailList.Rows[i]["UnitPrice"];
                    dtPrint.Rows[j]["Count1"] = dtMIPayRecordDetailList.Rows[i]["Count"];
                    dtPrint.Rows[j]["Fee1"] = dtMIPayRecordDetailList.Rows[i]["Fee"];
                    dtPrint.Rows[j]["YBItemLevel1"] = dtMIPayRecordDetailList.Rows[i]["YBItemLevel"];
                }
            }
            #endregion

            //EfwControls.CustomControl.ReportTool.GetReport(1, 2026, 0, myDictionary, dtPrint).Print(true);
            EfwControls.CustomControl.ReportTool.GetReport(1, 2007, 0, myDictionary, dtPrint).Print(false);
            return true;
        }

        public static ResultClass Mz_UpdateTradeRecord(InputClass inputClass)
        {
            ResultClass resultClass = ((MIMzInterface)AbstractAction).Mz_UpdateTradeRecord(inputClass);
          
            return resultClass;
        }

        /// <summary>
        /// 根据就诊号 获取挂号交易流水号
        /// </summary>
        /// <param name="sSerialNO">挂号就诊号</param>
        /// <returns></returns>
        public static ResultClass Mz_GetRegisterTradeNo(string sSerialNO)
        {
            ResultClass resultClass = ((MIMzInterface)AbstractAction).Mz_GetRegisterTradeNo(sSerialNO);

            return resultClass;
        }

    }
}
