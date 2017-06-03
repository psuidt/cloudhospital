using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.WcfFrame;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.MIManage;
using HIS_MIInterface.Interface.BaseClass;
using SiInterfaceDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HIS_MIInterface.Interface
{
    public static class MIInterFace
    {
        public static int WorkId
        {
            set;
            get;
        }

        private static string _cardNo = "111111111111";
        static List<string> _sColumns = new List<string>();

        #region 门诊接口

        /// <summary>
        /// 调用读取卡片信息接口
        /// 返回ResultClass oResult为DataTable
        /// </summary>
        /// <param name="sCardNo">医保卡号，直接读卡则为""</param>
        public static ResultClass Mz_GetCardInfo(string sCardNo)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                SiInterfaceDll sDll = new SiInterfaceDll();
                resultClass = OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    resultClass = GetCardInfo(sDll, sCardNo);
                    CloseDevice(sDll);
                }
                else
                {
                    resultClass.oResult = null;
                }
                sDll = null;
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
                resultClass.oResult = null;
            }
            return resultClass;
        }

        /// <summary>
        /// 调用获取个人信息接口
        /// 返回ResultClass oResult为PatientInfo
        /// </summary>
        /// <param name="sCardNo">医保卡号，直接读卡则为""</param>

        public static ResultClass Mz_GetPersonInfo(string sCardNo)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                SiInterfaceDll sDll = new SiInterfaceDll();
                resultClass = OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    resultClass = GetPersonInfo(sDll, sCardNo);
                    CloseDevice(sDll);
                }
                else
                {
                    resultClass.oResult = null;
                }
                sDll = null;
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
                resultClass.oResult = null;
            }
            return resultClass;
        }


        /// <summary>
        /// 门诊预登记
        /// 返回ResultClass oResult为Dictionary<string, string> ID:交易ID; tradeno：医保流水号; FeeAll:总额; fund:统筹; cash:现金 ; personcountpay:个帐支付 DataTable
        /// </summary>
        /// <param name="register">MI_Register</param>

        public static ResultClass MZ_PreviewRegister(MI_Register register)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(register);
                });
                ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "MZ_PreviewRegister", requestAction);
                resultClass.bSucess = retdataMember.GetData<bool>(0);
                resultClass.sRemarks = retdataMember.GetData<string>(1);
                if (resultClass.bSucess)
                {
                    Dictionary<string, string> resultDic = retdataMember.GetData<Dictionary<string, string>>(2);
                    resultClass.oResult = resultDic;
                    //iFrmMITest.LoadRegisterInfo(resultDic);
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
        /// 门诊登记
        /// 返回ResultClass oResult为Dictionary<string, string>  personcount:个帐余额
        /// </summary>
        /// <param name="registerId">登记表id</param>
        /// <param name="serialNO">挂号就诊号</param>

        public static ResultClass MZ_Register(int registerId, string serialNO)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(registerId);
                    request.AddData(serialNO);
                });
                ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "MZ_Register", requestAction);
                resultClass.bSucess = retdataMember.GetData<bool>(0);
                resultClass.sRemarks = retdataMember.GetData<string>(1);
                if (resultClass.bSucess)
                {
                    Dictionary<string, string> resultDic = retdataMember.GetData<Dictionary<string, string>>(2);
                    resultClass.oResult = resultDic;
                    //iFrmMITest.LoadTradeInfo(resultDic);
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
        /// 取消门诊登记
        /// 返回ResultClass 无object
        /// </summary>
        /// <param name="serialNO">挂号就诊号</param>

        public static ResultClass Mz_CancelRegister(string serialNO)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(serialNO);
                });
                ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "Mz_CancelRegister", requestAction);
                resultClass.bSucess = retdataMember.GetData<bool>(0);
                resultClass.sRemarks = retdataMember.GetData<string>(1);
                if (resultClass.bSucess)
                {
                    //MessageBoxShowError("退号成功！");
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
        /// 调用费用预结算接口
        /// 返回ResultClass oResult为Dictionary<string, string> ID:交易ID; tradeno：医保流水号; FeeAll:总额; fund:统筹; cash:现金 ; personcountpay:个帐支付
        /// </summary>
        /// <param name="tradeData">TradeData</param>

        public static ResultClass MZ_PreviewCharge(TradeData tradeData)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(tradeData);
                });
                ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "MZ_PreviewCharge", requestAction);
                resultClass.bSucess = retdataMember.GetData<bool>(0);
                resultClass.sRemarks = retdataMember.GetData<string>(1);
                if (resultClass.bSucess)
                {
                    Dictionary<string, string> resultDic = retdataMember.GetData<Dictionary<string, string>>(2);
                    resultClass.oResult = resultDic;
                    //iFrmMITest.PreviewCharge(resultDic);
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
        /// 结算
        /// 返回ResultClass ResultClass.oResult 为decimal
        /// </summary>
        /// <param name="tradeRecordId">交易记录ID</param>
        /// <param name="fph">发票号</param>

        public static ResultClass MZ_Charge(int tradeRecordId, string fph)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(tradeRecordId);
                    request.AddData(fph);
                });
                ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "MZ_Charge", requestAction);
                resultClass.bSucess = retdataMember.GetData<bool>(0);
                resultClass.sRemarks = retdataMember.GetData<string>(1);
                if (resultClass.bSucess)
                {
                    List<DataTable> objects = retdataMember.GetData<List<DataTable>>(2);

                    MI_MedicalInsurancePayRecord medicalInsurancePayRecord = ConvertExtend.ToList<MI_MedicalInsurancePayRecord>(objects[0])[0];
                    MI_MIPayRecordHead mIPayRecordHead = ConvertExtend.ToList<MI_MIPayRecordHead>(objects[1])[0];

                    DataTable dtPayrecordDetail = objects[2];
                    MZ_MIPrinter(medicalInsurancePayRecord, mIPayRecordHead, dtPayrecordDetail);
                    resultClass.oResult = medicalInsurancePayRecord.PersonCount;
                    //iFrmMITest.LoadTrade(medicalInsurancePayRecord.PersonCount);
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
        /// 取消门诊结算
        /// 返回ResultClass 无object
        /// </summary>
        /// <param name="fph">发票号</param>

        public static ResultClass MZ_CancelCharge(string fph)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(fph);
                });
                ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "MZ_CancelCharge", requestAction);
                resultClass.bSucess = retdataMember.GetData<bool>(0);
                resultClass.sRemarks = retdataMember.GetData<string>(1);
                if (resultClass.bSucess)
                {
                    //MessageBoxShowError("退费成功！");
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
        /// 上传门诊病人费用
        /// </summary>
        /// <returns></returns>
        public static void MZ_UploadzyPatFee() { throw new NotImplementedException(); }
        /// <summary>
        /// 下载门诊病人费用数据
        /// </summary>
        /// <returns></returns>
        public static void MZ_DownloadzyPatFee() { throw new NotImplementedException(); }
        //获取已结算费用
        public static void MZ_LoadFeeDetailByTicketNum() { throw new NotImplementedException(); }
        #endregion

        #region 公用方法
        private static bool MZ_MIPrinter(MI_MedicalInsurancePayRecord medicalInsurancePayRecord, MI_MIPayRecordHead mIPayRecordHead, DataTable dtMIPayRecordDetailList)
        {
            #region 参数字段
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
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

            EfwControls.CustomControl.ReportTool.GetReport(1, 2007, 0, myDictionary, dtPrint).Print(true);
            return true;
        }
        public static ServiceResponseData InvokeWcfService(string wcfpluginname, string wcfcontroller, string wcfmethod)
        {
            return InvokeWcfService(wcfpluginname, wcfcontroller, wcfmethod, null);
        }
        public static ServiceResponseData InvokeWcfService(string wcfpluginname, string wcfcontroller, string wcfmethod, Action<ClientRequestData> requestAction)
        {
            ClientLink wcfClientLink = ClientLinkManage.CreateConnection(wcfpluginname);
            //绑定LoginRight
            Action<ClientRequestData> _requestAction = ((ClientRequestData request) =>
            {
                request.LoginRight = new EFWCoreLib.CoreFrame.Business.SysLoginRight();
                request.LoginRight.WorkId = WorkId;
                if (requestAction != null)
                    requestAction(request);
            });
            ServiceResponseData retData = wcfClientLink.Request(wcfcontroller, wcfmethod, _requestAction);
            return retData;
        }

        /// <summary>
        /// 调用打开读卡设备接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        public static ResultClass OpenDevice(SiInterfaceDll sDll)
        {
            string sMome = "";
            bool bRet = false;
            String sOut;
            sDll.Open(out sOut);
            XmlDocument xmlDoc = GetXmlDoc(sOut);
            CheckOutputState(xmlDoc, out bRet, out sMome);
            xmlDoc = null;
            ResultClass resultClassTemp = new ResultClass();
            resultClassTemp.bSucess = bRet;
            resultClassTemp.sRemarks = sMome;
            resultClassTemp.oResult = null;
            return resultClassTemp;
        }

        /// <summary>
        /// 调用关闭读卡设备接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        public static ResultClass CloseDevice(SiInterfaceDll sDll)
        {
            string sMome = "";
            bool bRet = false;
            String sOut;
            sDll.Close(out sOut);
            XmlDocument xmlDoc = GetXmlDoc(sOut);
            CheckOutputState(xmlDoc, out bRet, out sMome);
            xmlDoc = null;
            ResultClass resultClassTemp = new ResultClass();
            resultClassTemp.bSucess = bRet;
            resultClassTemp.sRemarks = sMome;
            resultClassTemp.oResult = null;
            return resultClassTemp;
        }

        /// <summary>
        /// 调用读取卡片信息接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        public static ResultClass GetCardInfo(SiInterfaceDll sDll, string cardNo)
        {
            string sMome = "";
            bool bRet = false;
            DataTable dt = new DataTable();
            String sOut;
            _sColumns.Clear();
            _sColumns.Add("card_no");
            _sColumns.Add("ic_no");
            _sColumns.Add("id_no");
            _sColumns.Add("personname");
            _sColumns.Add("sex");
            _sColumns.Add("birthday");
            dt = CreatDataTable(_sColumns);

            sDll.GetCardInfo(out sOut, cardNo);

            XmlDocument xmlDoc = GetXmlDoc(sOut);

            CheckOutputState(xmlDoc, out bRet, out sMome);
            if (bRet)
            {
                DataRow dr = dt.NewRow();
                XmlNode dataNode = GetNodeFromPath(xmlDoc.DocumentElement, "output/ic");
                foreach (string s in _sColumns)
                {
                    dr[s] = dataNode.SelectNodes(s)[0].InnerText;
                }
                dt.Rows.Add(dr);
                AddLog("解析读取卡片信息完成");
            }
            _sColumns.Clear();
            xmlDoc = null;

            ResultClass resultClassTemp = new ResultClass();
            resultClassTemp.bSucess = bRet;
            resultClassTemp.sRemarks = sMome;
            resultClassTemp.oResult = dt;
            return resultClassTemp;
        }

        /// <summary>
        /// 调用获取个人信息接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        public static ResultClass GetPersonInfo(SiInterfaceDll sDll, string cardNo)
        {
            _cardNo = cardNo;

            string sMome = "";
            bool bRet = false;
            string sOut = "";
            DataTable dt = new DataTable();
            _sColumns.Clear();
            //ic卡
            _sColumns.Add("card_no");   //社保卡卡号
            _sColumns.Add("ic_no");
            _sColumns.Add("id_no");
            _sColumns.Add("personname");
            _sColumns.Add("sex");
            _sColumns.Add("birthday");
            _sColumns.Add("fromhosp");
            _sColumns.Add("fromhospdate");
            _sColumns.Add("fundtype");
            _sColumns.Add("isyt");
            _sColumns.Add("jclevel");
            _sColumns.Add("hospflag");
            //网络信息 
            _sColumns.Add("persontype");
            _sColumns.Add("isinredlist");
            _sColumns.Add("isspecifiedhosp");
            _sColumns.Add("ischronichosp");
            _sColumns.Add("personcount");
            _sColumns.Add("chroniccode");
            dt = CreatDataTable(_sColumns);

            AddLog("调用获取个人信息接口");
            AddLog("输入参数：无");

            sDll.GetPersonInfo(out sOut, cardNo);

            AddLog("输出数据：");
            AddLog(sOut);

            XmlDocument xmlDoc = GetXmlDoc(sOut);
            CheckOutputState(xmlDoc, out bRet, out sMome);

            if (bRet)
            {
                DataRow dr = dt.NewRow();
                XmlNode dataNode = GetNodeFromPath(xmlDoc.DocumentElement, "output/ic");
                XmlNode dataNodeNet = GetNodeFromPath(xmlDoc.DocumentElement, "output/net");
                foreach (string s in _sColumns)
                {
                    XmlNodeList xNodeList = dataNode.SelectNodes(s);
                    if (xNodeList.Count > 0)
                    {
                        dr[s] = xNodeList[0].InnerText;
                    }
                    else
                    {
                        dr[s] = dataNodeNet.SelectNodes(s)[0].InnerText;
                    }
                }
                dt.Rows.Add(dr);
                AddLog("解析获取个人信息接口完成");
            }
            xmlDoc = null;

            List<PatientInfo> patientInfoList = new List<PatientInfo>();
            foreach (DataRow dr in dt.Rows)
            {
                PatientInfo patientInfo = new PatientInfo();
                patientInfo.CardNo = dr["card_no"].ToString();
                patientInfo.IcNo = dr["ic_no"].ToString();
                patientInfo.IdNo = dr["id_no"].ToString();
                patientInfo.PersonName = dr["personname"].ToString();
                patientInfo.Sex = dr["sex"].ToString();
                patientInfo.Birthday = dr["birthday"].ToString();
                patientInfo.FromHosp = dr["fromhosp"].ToString();
                patientInfo.FromHospDate = dr["fromhospdate"].ToString();
                patientInfo.FundType = dr["fundtype"].ToString();
                patientInfo.IsYT = dr["isyt"].ToString();
                patientInfo.JcLevel = dr["jclevel"].ToString();
                patientInfo.HospFlag = dr["hospflag"].ToString();
                patientInfo.PersonType = dr["persontype"].ToString();
                patientInfo.IsInredList = dr["isinredlist"].ToString();
                patientInfo.IsSpecifiedHosp = dr["isspecifiedhosp"].ToString();
                patientInfo.IsChronicHosp = dr["ischronichosp"].ToString();
                patientInfo.PersonCount = dr["personcount"].ToString();
                patientInfo.ChronIcCode = dr["chroniccode"].ToString();
                patientInfoList.Add(patientInfo);
            }

            ResultClass resultClassTemp = new ResultClass();
            resultClassTemp.bSucess = bRet;
            resultClassTemp.sRemarks = sMome;
            resultClassTemp.oResult = patientInfoList;

            return resultClassTemp;
        }

        private static XmlDocument GetXmlDoc(string sXML)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sXML);
            return xmlDoc;
        }
        /// <summary>
        /// 1.读取调用信息，如果失败.读取错误信息；如果成功.读取警告信息；读取信息
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        private static void CheckOutputState(XmlDocument xmlDoc, out bool bRet, out string sMemo)
        {
            string sState = GetNodeFromPath(xmlDoc.DocumentElement, "state").Attributes["success"].InnerText;
            bRet = false;
            sMemo = "";

            if (sState.Equals("true"))
            {
                bRet = true;
                AddLog("调用返回状态：成功");
                //读取警告信息
                XmlNodeList warNodes = GetNodeFromPath(xmlDoc.DocumentElement, "state").SelectNodes("warning");
                for (int i = 0; i < warNodes.Count; i++)
                {
                    if (warNodes[i].Attributes.Count > 0)
                    {
                        string sWarNO = warNodes[i].Attributes["no"].InnerText;
                        string sWarMsg = warNodes[i].Attributes["info"].InnerText;
                        AddLog("调用返回警告：编号 [" + sWarNO + "] -- 描述 [" + sWarMsg + "]");
                        sMemo = "调用返回警告：编号 [" + sWarNO + "] -- 描述 [" + sWarMsg + "]";
                    }
                }

                ////读取信息
                //XmlNodeList infNodes = GetNodeFromPath(xmlDoc.DocumentElement, "state").SelectNodes("output");
                //for (int i = 0; i < infNodes.Count; i++)
                //{
                //    if (infNodes[i].Attributes.Count > 0)
                //    {
                //        string sInfNO = infNodes[i].Attributes["no"].InnerText;
                //        string sInfMsg = infNodes[i].Attributes["info"].InnerText;
                //        AddLog("调用返回信息：编号 [" + sInfNO + "] -- 描述 [" + sInfMsg + "]");
                //        sInformation = "调用返回信息：编号 [" + sInfNO + "] -- 描述 [" + sInfMsg + "]";
                //    }
                //}
            }
            else
            {
                bRet = false;
                AddLog("调用返回状态：失败");
                //读取错误信息
                XmlNodeList errNodes = GetNodeFromPath(xmlDoc.DocumentElement, "state").SelectNodes("error");
                for (int i = 0; i < errNodes.Count; i++)
                {
                    if (errNodes[i].Attributes.Count > 0)
                    {
                        string sErrNO = errNodes[i].Attributes["no"].InnerText;
                        string sErrMsg = errNodes[i].Attributes["info"].InnerText;
                        AddLog("调用返回错误：编号 [" + sErrNO + "] -- 描述 [" + sErrMsg + "]");
                        sMemo = "调用返回错误：编号 [" + sErrNO + "] -- 描述 [" + sErrMsg + "]";
                    }
                }
            }
        }
        /// <summary>
        /// 读取结果
        /// </summary>
        /// <param name="oParentNode"></param>
        /// <param name="sPath"></param>
        /// <returns></returns>
        private static XmlNode GetNodeFromPath(XmlNode oParentNode, string sPath)
        {
            XmlNode tmpNode = oParentNode.SelectNodes(sPath)[0];
            return tmpNode;
        }

        private static DataTable CreatDataTable(List<string> sColumns)
        {
            DataTable dt = new DataTable();
            foreach (string s in sColumns)
            {
                DataColumn dc = new DataColumn(s, Type.GetType("System.String"));
                dt.Columns.Add(dc);
            }
            return dt;
        }
        private static void AddLog(string s)
        { }

        #endregion
    }
}
