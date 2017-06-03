using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using MedicareComLib;
using System.Xml;
using EFWCoreLib.CoreFrame.Common;
using System.Drawing;

namespace MI_MIInterface.ObjectModel.CustomAction.beijing
{
    /// <summary>
    /// 医保接口动态库的类引用，这是dll类型的
    /// </summary>
    //[Serializable()]
    public class SiInterface
    {
        /// <summary>
        /// 调用打开读卡设备接口
        /// </summary>
        /// <param name="hObj"></param>
        /// <returns></returns>
        private bool OpenDevice(OutpatientClass hObj)
        {
            String sOut;
            AddLog("调用打开读卡设备接口");
            AddLog("输入参数：无");

            hObj.Open(out sOut);

            AddLog("输出数据：");
            AddLog(sOut);

            XmlDocument xmlDoc = GetXmlDoc(sOut);

            bool bRet = CheckOutputState(xmlDoc);
            xmlDoc = null;

            AddLog("\r\n");
            return bRet;
        }

        /// <summary>
        /// 调用关闭读卡设备接口
        /// </summary>
        /// <param name="hObj"></param>
        /// <returns></returns>
        private bool CloseDevice(OutpatientClass hObj)
        {
            String sOut;
            AddLog("调用关闭读卡设备接口");
            AddLog("输入参数：无");

            hObj.Close(out sOut);

            AddLog("输出数据：");
            AddLog(sOut);

            XmlDocument xmlDoc = GetXmlDoc(sOut);

            bool bRet = CheckOutputState(xmlDoc);
            xmlDoc = null;

            AddLog("\r\n");

            return bRet;
        }

        /// <summary>
        /// 调用读取卡片信息接口
        /// </summary>
        /// <param name="hObj"></param>
        /// <returns></returns>
        private bool GetCardInfo(OutpatientClass hObj)
        {
            String sOut;
            AddLog("调用读取卡片信息接口");
            AddLog("输入参数：无");

            hObj.GetCardInfo(out sOut);

            AddLog("输出数据：");
            AddLog(sOut);

            XmlDocument xmlDoc = GetXmlDoc(sOut);

            bool bRet = CheckOutputState(xmlDoc);
            if (bRet)
            {
                string sIC_NO, sPersonName;
                XmlNode dataNode = GetNodeFromPath(xmlDoc.DocumentElement, "output/ic");
                sIC_NO = dataNode.SelectNodes("ic_no")[0].InnerText;
                sPersonName = dataNode.SelectNodes("personname")[0].InnerText;
                AddLog("解析XML结果：\r\n医保应用号：" + sIC_NO + "\r\n姓名：" + sPersonName);
            }

            xmlDoc = null;

            AddLog("\r\n");

            return bRet;
        }

        /// <summary>
        /// 调用获取个人信息接口
        /// </summary>
        /// <param name="hObj"></param>
        /// <returns></returns>
        private bool GetPersonInfo(OutpatientClass hObj)
        {
            String sOut;
            AddLog("调用获取个人信息接口");
            AddLog("输入参数：无");

            hObj.GetPersonInfo(out sOut);

            AddLog("输出数据：");
            AddLog(sOut);

            XmlDocument xmlDoc = GetXmlDoc(sOut);

            bool bRet = CheckOutputState(xmlDoc);

            if (bRet)
            {
                string sIC_NO, sPersonName;
                XmlNode dataNode = GetNodeFromPath(xmlDoc.DocumentElement, "output/ic");
                sIC_NO = dataNode.SelectNodes("ic_no")[0].InnerText;
                sPersonName = dataNode.SelectNodes("personname")[0].InnerText;
                AddLog("解析XML结果：\r\n医保应用号：" + sIC_NO + "\r\n姓名：" + sPersonName);
            }

            xmlDoc = null;

            AddLog("\r\n");

            return bRet;
        }

        /// <summary>
        /// 分解费用
        /// </summary>
        /// <param name="hObj"></param>
        /// <returns></returns>
        private bool Divide(OutpatientClass hObj)
        {
            string sOut;
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

            AddLog("调用费用分解接口");
            AddLog("输入参数：");
            AddLog(sIn);

            hObj.Divide(sIn, out sOut);

            AddLog("输出数据：");
            AddLog(sOut);

            XmlDocument xmlDoc = GetXmlDoc(sOut);

            bool bRet = CheckOutputState(xmlDoc);
            if (bRet)
            {
                string sTradeNO, sFeeNO, sRecipeNO;
                XmlNode dataNode = GetNodeFromPath(xmlDoc.DocumentElement, "output/tradeinfo");
                sTradeNO = dataNode.SelectNodes("tradeno")[0].InnerText;
                sFeeNO = dataNode.SelectNodes("feeno")[0].InnerText;

                dataNode = GetNodeFromPath(xmlDoc.DocumentElement, "output/feeitemarray");
                sRecipeNO = dataNode.SelectNodes("feeitem")[0].Attributes["recipeno"].InnerText;

                AddLog("解析XML结果：\r\n交易流水号：" + sTradeNO + "\r\n收费单据号：" + sFeeNO + "\r\n处方号：" + sRecipeNO);
            }

            xmlDoc = null;

            AddLog("\r\n");
            return bRet;
        }

        /// <summary>
        /// 调用交易确认接口
        /// </summary>
        /// <param name="hObj"></param>
        /// <returns></returns>
        private bool Trade(OutpatientClass hObj)
        {
            string sOut;

            AddLog("调用交易确认接口");
            AddLog("输入参数：");

            hObj.Trade(out sOut);

            AddLog("输出数据：");
            AddLog(sOut);

            XmlDocument xmlDoc = GetXmlDoc(sOut);

            bool bRet = CheckOutputState(xmlDoc);
            if (bRet)
            {
                string sPersonCountAfterSub;
                XmlNode dataNode = GetNodeFromPath(xmlDoc.DocumentElement, "output");
                sPersonCountAfterSub = dataNode.SelectNodes("personcountaftersub")[0].InnerText;
                AddLog("解析XML结果：\r\n本次结算后个人账户余额：" + sPersonCountAfterSub);
            }

            xmlDoc = null;

            AddLog("\r\n");

            return bRet;
        }

        /// <summary>
        /// 调用收据重打接口
        /// </summary>
        /// <param name="hObj"></param>
        /// <returns></returns>
        private bool RePrintInvoice(OutpatientClass hObj)
        {
            string sOut;
            string sDealID, sFeeNO;
            sDealID = "011100030A090115000014";
            sFeeNO = "xxxxxx";

            AddLog("调用收据重打接口");
            AddLog("输入参数：");
            AddLog("交易流水号：" + sDealID);
            AddLog("收费单据号：" + sFeeNO);

            hObj.RePrintInvoice(sDealID, sFeeNO, out sOut);

            AddLog("输出数据：");
            AddLog(sOut);

            XmlDocument xmlDoc = GetXmlDoc(sOut);

            bool bRet = CheckOutputState(xmlDoc);

            xmlDoc = null;

            AddLog("\r\n");
            return bRet;
        }

        /// <summary>
        /// 调用费用退费分解接口
        /// </summary>
        /// <param name="hObj"></param>
        /// <returns></returns>
        private bool RefundmentDivide(OutpatientClass hObj)
        {
            string sOut;
            string sDealID = "011100030A090115000017";

            AddLog("调用费用退费分解接口");
            AddLog("输入参数：");
            AddLog("交易流水号：" + sDealID);

            hObj.RefundmentDivide(sDealID, out sOut);

            AddLog("输出数据：");
            AddLog(sOut);

            XmlDocument xmlDoc = GetXmlDoc(sOut);
            bool bRet = CheckOutputState(xmlDoc);
            if (bRet)
            {
                string sTradeNO, sIC_NO;
                XmlNode dataNode = GetNodeFromPath(xmlDoc.DocumentElement, "output/tradeinfo");
                sTradeNO = dataNode.SelectNodes("tradeno")[0].InnerText;
                sIC_NO = dataNode.SelectNodes("icno")[0].InnerText;
                AddLog("解析XML结果：\r\n交易流水号：" + sTradeNO + "\r\n医保应用号：" + sIC_NO);
            }

            xmlDoc = null;

            AddLog("\r\n");
            return bRet;
        }

        /// <summary>
        /// 调用医保查询接口
        /// </summary>
        /// <param name="hObj"></param>
        private void MedicareQuery(OutpatientClass hObj)
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

            hObj.MedicareQuery(sIn, out sOut);

            AddLog("输出数据：");
            AddLog(sOut);

            AddLog("\r\n");
        }

        /// <summary>
        /// 调用交易状态查询及回退接口
        /// </summary>
        /// <param name="hObj"></param>
        /// <returns></returns>
        private bool CommitTradeState(OutpatientClass hObj)
        {
            string sOut;
            string sDealID = "011100030A090308000007";

            AddLog("调用交易状态查询及回退接口");
            AddLog("输入参数：");
            AddLog("交易流水号：" + sDealID);

            hObj.CommitTradeState(sDealID, out sOut);

            AddLog("输出数据：");
            AddLog(sOut);

            XmlDocument xmlDoc = GetXmlDoc(sOut);

            bool bRet = CheckOutputState(xmlDoc);
            if (bRet)
            {
                string sTradeState;
                XmlNode dataNode = GetNodeFromPath(xmlDoc.DocumentElement, "output");
                sTradeState = dataNode.SelectNodes("tradestate")[0].InnerText;
                AddLog("解析XML结果：\r\n交易状态：" + sTradeState);
            }

            xmlDoc = null;

            AddLog("\r\n");

            return bRet;
        }

        #region 方法
        private bool CheckOutputState(XmlDocument xmlDoc)
        {
            string sState = GetNodeFromPath(xmlDoc.DocumentElement, "state").Attributes["success"].InnerText;
            bool bRet = false;
            if (sState.Equals("true"))
            {
                bRet = true;
                AddLog("调用返回状态：成功");
            }
            else
            {
                bRet = false;
                AddLog("调用返回状态：失败");
            }
            //读取错误信息
            XmlNodeList errNodes = GetNodeFromPath(xmlDoc.DocumentElement, "state").SelectNodes("error");
            for (int i = 0; i < errNodes.Count; i++)
            {
                if (errNodes[i].Attributes.Count > 0)
                {
                    string sErrNO = errNodes[i].Attributes["no"].InnerText;
                    string sErrMsg = errNodes[i].Attributes["info"].InnerText;
                    AddLog("调用返回错误：编号 [" + sErrNO + "] -- 描述 [" + sErrMsg + "]");
                }
            }

            //读取警告信息
            XmlNodeList warNodes = GetNodeFromPath(xmlDoc.DocumentElement, "state").SelectNodes("warning");
            for (int i = 0; i < warNodes.Count; i++)
            {
                if (warNodes[i].Attributes.Count > 0)
                {
                    string sWarNO = warNodes[i].Attributes["no"].InnerText;
                    string sWarMsg = warNodes[i].Attributes["info"].InnerText;
                    AddLog("调用返回警告：编号 [" + sWarNO + "] -- 描述 [" + sWarMsg + "]");
                }
            }

            //读取信息
            XmlNodeList infNodes = GetNodeFromPath(xmlDoc.DocumentElement, "state").SelectNodes("information");
            for (int i = 0; i < infNodes.Count; i++)
            {
                if (infNodes[i].Attributes.Count > 0)
                {
                    string sInfNO = infNodes[i].Attributes["no"].InnerText;
                    string sInfMsg = infNodes[i].Attributes["info"].InnerText;
                    AddLog("调用返回信息：编号 [" + sInfNO + "] -- 描述 [" + sInfMsg + "]");
                }
            }
            return bRet;
        }

        private XmlDocument GetXmlDoc(string sXML)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sXML);
            return xmlDoc;
        }

        private XmlNode GetNodeFromPath(XmlNode oParentNode, string sPath)
        {
            XmlNode tmpNode = oParentNode.SelectNodes(sPath)[0];
            return tmpNode;
        }

        private void AddLog(string s)
        {
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, s);
        }
        #endregion
    }
}
