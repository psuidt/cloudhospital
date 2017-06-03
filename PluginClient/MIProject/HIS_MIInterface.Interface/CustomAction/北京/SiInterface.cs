using HIS_Entity.MIManage;
using HIS_Entity.MIManage.Common;
using SiInterfaceDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HIS_MIInterface.Interface.CustomAction.北京
{

    public class SiInterface
    {
        List<string> _sColumns = new List<string>();
        /// <summary>
        /// 调用打开读卡设备接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        public ResultClass OpenDevice(SiInterfaceDll sDll)
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
        public ResultClass CloseDevice(SiInterfaceDll sDll)
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
        public ResultClass GetCardInfo(SiInterfaceDll sDll, string cardNo)
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
        public ResultClass GetPersonInfo(SiInterfaceDll sDll, string cardNo)
        {
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

        /// <summary>
        /// 分解费用
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        public ResultClass Divide(SiInterfaceDll sDll, Reg.root root)
        {
            string sMome = "";
            bool bRet = false;
            string sOut;
            string sIn = XmlUtil.SerializeToXml(root, typeof(Reg.root));

            sDll.Divide(sIn, out sOut);

            AddLog("分解输出数据：");
            AddLog(sOut);
            DivideResult.root divideResult = (DivideResult.root)(XmlUtil.DeserializeFromXml(sOut, typeof(DivideResult.root)));
            XmlDocument xmlDoc = GetXmlDoc(sOut);
            CheckOutputState(xmlDoc, out bRet, out sMome);

            ResultClass resultClassTemp = new ResultClass();
            resultClassTemp.bSucess = bRet;
            resultClassTemp.sRemarks = sMome;

            resultClassTemp.oResult = divideResult;
            return resultClassTemp;
        }

        /// <summary>
        /// 调用交易确认接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        public ResultClass Trade(SiInterfaceDll sDll)
        {
            string sOut;
            string sMome = "";
            bool bRet = false;
            AddLog("调用交易确认接口");
            AddLog("输入参数：");

            sDll.Trade(out sOut);

            AddLog("输出数据：");
            AddLog(sOut);

            XmlDocument xmlDoc = GetXmlDoc(sOut);

            string sPersonCountAfterSub = "0";

            CheckOutputState(xmlDoc, out bRet, out sMome);
            if (bRet)
            {
                XmlNode dataNode = GetNodeFromPath(xmlDoc.DocumentElement, "output");
                sPersonCountAfterSub = dataNode.SelectNodes("personcountaftersub")[0].InnerText;
                AddLog("解析XML结果：\r\n本次结算后个人账户余额：" + sPersonCountAfterSub);
            }

            xmlDoc = null;

            AddLog("\r\n");
            ResultClass resultClassTemp = new ResultClass();
            resultClassTemp.bSucess = bRet;
            resultClassTemp.sRemarks = sMome;
            resultClassTemp.oResult = sPersonCountAfterSub;
            return resultClassTemp;
        }

        /// <summary>
        /// 调用费用退费分解接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        public ResultClass RefundmentDivide(SiInterfaceDll sDll, string tradeNo)
        {
            string sMome = "";
            bool bRet = false;
            string sOut;

            sDll.RefundmentDivide(tradeNo, out sOut);

            XmlDocument xmlDoc = GetXmlDoc(sOut);

            CheckOutputState(xmlDoc, out bRet, out sMome);
            if (bRet)
            {
                string sTradeNO, sIC_NO;
                XmlNode dataNode = GetNodeFromPath(xmlDoc.DocumentElement, "output/tradeinfo");
                sTradeNO = dataNode.SelectNodes("tradeno")[0].InnerText;
            }

            DivideResult.root divideResult = (DivideResult.root)(XmlUtil.DeserializeFromXml(sOut, typeof(DivideResult.root)));
            xmlDoc = null;
            ResultClass resultClassTemp = new ResultClass();
            resultClassTemp.bSucess = bRet;
            resultClassTemp.sRemarks = sMome;
            resultClassTemp.oResult = divideResult;
            return resultClassTemp;
        }

        /// <summary>
        /// 调用收据重打接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        public ResultClass RePrintInvoice(SiInterfaceDll sDll)
        {
            string sMome = "";
            bool bRet = false;

            string sOut;
            string sDealID, sFeeNO;
            sDealID = "011100030A090115000014";
            sFeeNO = "xxxxxx";

            AddLog("调用收据重打接口");
            AddLog("输入参数：");
            AddLog("交易流水号：" + sDealID);
            AddLog("收费单据号：" + sFeeNO);

            sDll.RePrintInvoice(sDealID, sFeeNO, out sOut);

            AddLog("输出数据：");
            AddLog(sOut);

            XmlDocument xmlDoc = GetXmlDoc(sOut);
            CheckOutputState(xmlDoc, out bRet, out sMome);
            xmlDoc = null;
            AddLog("\r\n");
            ResultClass resultClassTemp = new ResultClass();
            resultClassTemp.bSucess = bRet;
            resultClassTemp.sRemarks = sMome;
            resultClassTemp.oResult = null;
            return resultClassTemp;
        }

        /// <summary>
        /// 调用医保查询接口
        /// </summary>
        /// <param name="sDll"></param>
        public void MedicareQuery(SiInterfaceDll sDll)
        {
            string sOut;
            string sIn;

            //此处写的XML仅供测试接口使用，具体格式应以接口文档为准，且在生成此XML文档时应使用XML DOM对象生成，自行拼串需要处理特殊的XML字符转义
            sIn = "";
            sIn = sIn + "<?xml version=\"1.0\" encoding=\"UTF-16\" standalone=\"yes\"?>";
            sIn = sIn + "<root version=\"2.003\" sender=\"\">";
            sIn = sIn + "  </input>";
            sIn = sIn + "</root>";

            AddLog("调用医保查询接口");
            AddLog("输入参数：");
            AddLog(sIn);

            sDll.MedicareQuery(sIn, out sOut);

            AddLog("输出数据：");
            AddLog(sOut);

            AddLog("\r\n");
        }

        /// <summary>
        /// 调用交易状态查询及回退接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        public ResultClass CommitTradeState(SiInterfaceDll sDll)
        {
            string sOut;
            string sDealID = "011100030A090308000007";
            string sMome = "";
            bool bRet = false;
            AddLog("调用交易状态查询及回退接口");
            AddLog("输入参数：");
            AddLog("交易流水号：" + sDealID);

            sDll.CommitTradeState(sDealID, out sOut);

            AddLog("输出数据：");
            AddLog(sOut);

            XmlDocument xmlDoc = GetXmlDoc(sOut);

            CheckOutputState(xmlDoc, out bRet, out sMome);
            if (bRet)
            {
                string sTradeState;
                XmlNode dataNode = GetNodeFromPath(xmlDoc.DocumentElement, "output");
                sTradeState = dataNode.SelectNodes("tradestate")[0].InnerText;
                AddLog("解析XML结果：\r\n交易状态：" + sTradeState);
            }

            xmlDoc = null;

            AddLog("\r\n");
            ResultClass resultClassTemp = new ResultClass();
            resultClassTemp.bSucess = bRet;
            resultClassTemp.sRemarks = sMome;
            resultClassTemp.oResult = null;
            return resultClassTemp;
        }

        #region 方法
        private XmlDocument GetXmlDoc(string sXML)
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
        private void CheckOutputState(XmlDocument xmlDoc, out bool bRet, out string sMemo)
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
        private XmlNode GetNodeFromPath(XmlNode oParentNode, string sPath)
        {
            XmlNode tmpNode = oParentNode.SelectNodes(sPath)[0];
            return tmpNode;
        }

        private void AddLog(string s)
        {
            //MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, s);
        }

        private DataTable CreatDataTable(List<string> sColumns)
        {
            DataTable dt = new DataTable();
            foreach (string s in sColumns)
            {
                DataColumn dc = new DataColumn(s, Type.GetType("System.String"));
                dt.Columns.Add(dc);
            }
            return dt;
        }
        #endregion
    }

}
