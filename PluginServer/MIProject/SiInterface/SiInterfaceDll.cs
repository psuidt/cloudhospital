using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using SiInterfaceDLL.Common;


namespace SiInterfaceDLL
{
    public class SiInterfaceDll 
    {
        DataTable _dtPerson;
        public DataTable GetDataTable(string sql)
        {
            SqlConnection conn = new SqlConnection("server=192.168.187.17;database=CloudHISDB;uid=sa;pwd=123sn!@#;");
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "s");
            return ds.Tables["s"];
        }

        public bool insert(string sql)
        {
            SqlConnection conn = new SqlConnection("server=192.168.187.17;database=CloudHISDB;uid=sa;pwd=123sn!@#;");
            conn.Open();
            SqlCommand command = new SqlCommand(sql, conn);
            int i = command.ExecuteNonQuery() ;
            conn.Close();
            return i > 0;
        }

        public bool update(string sql)
        {
            SqlConnection conn = new SqlConnection("server=192.168.187.17;database=CloudHISDB;uid=sa;pwd=123sn!@#;");
            conn.Open();
            SqlCommand command = new SqlCommand(sql, conn);
            int i = command.ExecuteNonQuery();
            conn.Close();
            return i > 0;
        }

        #region 匹配
        /// <summary>
        /// 下载中心药品目录
        /// </summary>
        /// <returns></returns>
        public bool M_DownLoadDrugContent(int ybId, int workId) { return true; }
        /// <summary>
        /// 下载中心项目目录
        /// </summary>
        /// <returns></returns>
        public bool M_DownLoadItemContent(int ybId, int workId) { return true; }

        /// <summary>
        /// 下载中心材料目录
        /// </summary>
        /// <returns></returns>
        public bool M_DownLoadMaterialsContent(int ybId, int workId) { return true; }

        /// <summary>
        /// 判断单条目录是否已匹配
        /// </summary>
        /// <returns></returns>
        public bool M_JudgeSingleContentMatch() { return true; }
        /// <summary>
        /// 上传匹配的目录
        /// </summary>
        /// <returns></returns>
        public bool M_UploadMatchContent() { return true; }
        /// <summary>
        /// 重置匹配目录
        /// </summary>
        /// <returns></returns>
        public bool M_ResetMatchContent() { return true; }
        /// <summary>
        /// 删除匹配目录
        /// </summary>
        /// <returns></returns>
        public bool M_DeleteMatchContent() { return true; }
        /// <summary>
        /// 下载医院审核项目信息
        /// </summary>
        /// <returns></returns>
        public bool M_DownLoadHospContent() { return true; }

        /// <summary>
        /// 上传科室
        /// </summary>
        public bool M_UpLoadDept() { return true; }

        /// <summary>
        /// 上传人员信息
        /// </summary>
        public bool M_UpLoadStaff() { return true; }
        #endregion

        #region 门诊
        public virtual void Open(out string OutInfo)
        {
            StringBuilder xmlResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>");
            xmlResult.AppendFormat("<root version =\"2.003\" >");
            xmlResult.AppendFormat("<state success =\"true\" >");
            xmlResult.AppendFormat("<error no =\"0\" info =\"打开读卡设备失败!\" />");
            xmlResult.AppendFormat("<warning no =\"0\" info =\"\" />");
            xmlResult.AppendFormat("</state >");
            xmlResult.AppendFormat("</root >");

            OutInfo = xmlResult.ToString();

        }
        public virtual void Close(out string OutInfo)
        {
            StringBuilder xmlResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>");
            xmlResult.AppendFormat("<root version =\"2.003\" >");
            xmlResult.AppendFormat("<state success =\"true\" >");
            xmlResult.AppendFormat("<error no =\"0 \" info =\"关闭读卡设备失败!\" />");
            xmlResult.AppendFormat("<warning no =\"0 \" info =\"\" />");
            xmlResult.AppendFormat("</state >");
            xmlResult.AppendFormat("</root >");

            OutInfo = xmlResult.ToString();

        }
        
        /// <summary>
        /// 获取卡片信息接口测试
        /// </summary>
        /// <param name="CardInfo"></param>
        public virtual void GetCardInfo(out string CardInfo,string cardNo)
        {
            DataTable dt = GetDataTable(@"SELECT   card_no    , ic_no   , id_no    , personname  , sex    , birthday   , fromhosp   , fromhospdate   , fundtype  , isyt 
                        , jclevel   , hospflag  , persontype  , isinredlist  , isspecifiedhosp  , ischronichosp  , personcount  , chroniccode 
                    FROM dbo.TEST_YB_PATIENT where  card_no='" + cardNo + "'");

            StringBuilder xmlResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>");
            xmlResult.AppendFormat("<root version=\"2.003\">");
            if (dt != null && dt.Rows.Count > 0)
            {
                xmlResult.AppendFormat("<state success=\"true\">");
                xmlResult.AppendFormat("<warning no =\"1\" info=\"与医保中心通讯中断,不能取得个人帐户,定点医疗机构等信息,请联系网络管理员查看网络运行状况\" />");
                xmlResult.AppendFormat("</state>");

                xmlResult.AppendFormat("<output name=\"输出部分\">");
                xmlResult.AppendFormat("<ic>");
                xmlResult.AppendFormat("<card_no name=\"社保卡卡号\">{0}</card_no>", dt.Rows[0]["card_no"]);
                xmlResult.AppendFormat("<ic_no name=\"医保应用号\">{0}</ic_no>", dt.Rows[0]["ic_no"]);
                xmlResult.AppendFormat("<id_no name=\"公民身份号码\">{0}</id_no>", dt.Rows[0]["id_no"]);
                xmlResult.AppendFormat("<personname name=\"姓名\">{0}</personname>", dt.Rows[0]["personname"]);
                xmlResult.AppendFormat("<sex name=\"性别\">{0}</sex>", dt.Rows[0]["sex"]);
                xmlResult.AppendFormat("<birthday name=\"出生日期\">{0}</birthday>", dt.Rows[0]["birthday"]);
                xmlResult.AppendFormat("<fromhosp name=\"转诊医院编码\" >{0}</fromhosp>", dt.Rows[0]["fromhosp"]);
                xmlResult.AppendFormat("<fromhospdate name=\"转诊时限\" >{0}</fromhospdate>", dt.Rows[0]["fromhospdate"]);
                xmlResult.AppendFormat("<fundtype name=\"险种类型:老少,居民,职工,公疗\" >{0}</fundtype>", dt.Rows[0]["fundtype"]);
                xmlResult.AppendFormat("<isyt name=\"预提人员标示\" >{0}</isyt>", dt.Rows[0]["isyt"]);
                xmlResult.AppendFormat("<jclevel name=\"军残等级\" >{0}</jclevel>", dt.Rows[0]["jclevel"]);
                xmlResult.AppendFormat("<hospflag name=\"在院标示\" >{0}</hospflag>", dt.Rows[0]["hospflag"]);
                xmlResult.AppendFormat("</ic>");
                xmlResult.AppendFormat("<net>");
                xmlResult.AppendFormat("<persontype name=\"参保人员类别\" >{0}</persontype>", dt.Rows[0]["persontype"]);
                xmlResult.AppendFormat("<isinredlist name=\"是否在红名单\">{0}</isinredlist>", dt.Rows[0]["isinredlist"]);
                xmlResult.AppendFormat("<isspecifiedhosp name=\"本人定点医院状态\">{0}</isspecifiedhosp>", dt.Rows[0]["isspecifiedhosp"]);
                xmlResult.AppendFormat("<ischronichosp name=\"是否本人慢病定点医院\">{0}</ischronichosp>", dt.Rows[0]["ischronichosp"]);
                xmlResult.AppendFormat("<personcount name=\"个人帐户余额\" >{0}</personcount>", dt.Rows[0]["personcount"]);
                xmlResult.AppendFormat("<chroniccode name=\"慢病编码\">{0}</chroniccode>", dt.Rows[0]["chroniccode"]);
                xmlResult.AppendFormat("</net>");
                xmlResult.AppendFormat("</output>");

            }
            else
            {
                xmlResult.AppendFormat("<state success=\"false\">");
                xmlResult.AppendFormat("<error no =\"1\" info=\"读卡失败,请插入社保卡!\" />");
                xmlResult.AppendFormat("</state>");
            }
            xmlResult.AppendFormat("</root>");

            CardInfo = xmlResult.ToString();
        }
        /// <summary>
        /// 获取个人信息接口测试
        /// </summary>
        /// <param name="PersonInfo"></param>
        public virtual void GetPersonInfo(out string PersonInfo, string cardNo)
        {
            _dtPerson = GetDataTable(@"SELECT   card_no    , ic_no   , id_no    , personname  , sex    , birthday   , fromhosp   , fromhospdate   , fundtype  , isyt 
                        , jclevel   , hospflag  , persontype  , isinredlist  , isspecifiedhosp  , ischronichosp  , personcount  , chroniccode 
                    FROM dbo.TEST_YB_PATIENT where  card_no='"+cardNo+"'");

            //CardInfo = ConvertXmlToString("GetCardInfo");
            StringBuilder xmlResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>");
            xmlResult.AppendFormat("<root version=\"2.003\">");
            if (_dtPerson != null && _dtPerson.Rows.Count > 0)
            {
                xmlResult.AppendFormat("<state success=\"true\">");
                xmlResult.AppendFormat("<warning no =\"1\" info=\"与医保中心通讯中断,不能取得个人帐户,定点医疗机构等信息,请联系网络管理员查看网络运行状况\" />");
                xmlResult.AppendFormat("</state>");

                xmlResult.AppendFormat("<output name=\"输出部分\">");
                xmlResult.AppendFormat("<ic>");
                xmlResult.AppendFormat("<card_no name=\"社保卡卡号\">{0}</card_no>", _dtPerson.Rows[0]["card_no"]);
                xmlResult.AppendFormat("<ic_no name=\"医保应用号\">{0}</ic_no>", _dtPerson.Rows[0]["ic_no"]);
                xmlResult.AppendFormat("<id_no name=\"公民身份号码\">{0}</id_no>", _dtPerson.Rows[0]["id_no"]);
                xmlResult.AppendFormat("<personname name=\"姓名\">{0}</personname>", _dtPerson.Rows[0]["personname"]);
                xmlResult.AppendFormat("<sex name=\"性别\">{0}</sex>", _dtPerson.Rows[0]["sex"]);
                xmlResult.AppendFormat("<birthday name=\"出生日期\">{0}</birthday>", _dtPerson.Rows[0]["birthday"]);
                xmlResult.AppendFormat("<fromhosp name=\"转诊医院编码\" >{0}</fromhosp>", _dtPerson.Rows[0]["fromhosp"]);
                xmlResult.AppendFormat("<fromhospdate name=\"转诊时限\" >{0}</fromhospdate>", _dtPerson.Rows[0]["fromhospdate"]);
                xmlResult.AppendFormat("<fundtype name=\"险种类型:老少,居民,职工,公疗\" >{0}</fundtype>", _dtPerson.Rows[0]["fundtype"]);
                xmlResult.AppendFormat("<isyt name=\"预提人员标示\" >{0}</isyt>", _dtPerson.Rows[0]["isyt"]);
                xmlResult.AppendFormat("<jclevel name=\"军残等级\" >{0}</jclevel>", _dtPerson.Rows[0]["jclevel"]);
                xmlResult.AppendFormat("<hospflag name=\"在院标示\" >{0}</hospflag>", _dtPerson.Rows[0]["hospflag"]);
                xmlResult.AppendFormat("</ic>");
                xmlResult.AppendFormat("<net>");
                xmlResult.AppendFormat("<persontype name=\"参保人员类别\" >{0}</persontype>", _dtPerson.Rows[0]["persontype"]);
                xmlResult.AppendFormat("<isinredlist name=\"是否在红名单\">{0}</isinredlist>", _dtPerson.Rows[0]["isinredlist"]);
                xmlResult.AppendFormat("<isspecifiedhosp name=\"本人定点医院状态\">{0}</isspecifiedhosp>", _dtPerson.Rows[0]["isspecifiedhosp"]);
                xmlResult.AppendFormat("<ischronichosp name=\"是否本人慢病定点医院\">{0}</ischronichosp>", _dtPerson.Rows[0]["ischronichosp"]);
                xmlResult.AppendFormat("<personcount name=\"个人帐户余额\" >{0}</personcount>", _dtPerson.Rows[0]["personcount"]);
                xmlResult.AppendFormat("<chroniccode name=\"慢病编码\">{0}</chroniccode>", _dtPerson.Rows[0]["chroniccode"]);
                xmlResult.AppendFormat("</net>");
                xmlResult.AppendFormat("</output>");

            }
            else
            {
                xmlResult.AppendFormat("<state success=\"false\">");
                xmlResult.AppendFormat("<error no =\"1\" info=\"读卡失败,请插入社保卡!\" />");
                xmlResult.AppendFormat("</state>");
            }
            xmlResult.AppendFormat("</root>");

            PersonInfo = xmlResult.ToString();
        }

        /// <summary>
        /// 费用分解接口测试
        /// </summary>
        /// <param name="DivideInfo"></param>
        /// <param name="DivideResult"></param>
        public virtual void Divide(string DivideInfo, out string DivideResult)
        {
            try
            {
                SiInterface.Common.RegClass.root divideIn = (SiInterface.Common.RegClass.root)(XmlUtil.DeserializeFromXml(DivideInfo, typeof(SiInterface.Common.RegClass.root)));
                StringBuilder xmlResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>");
                xmlResult.AppendFormat("<root version=\"2.003\">");
                xmlResult.AppendFormat(ResultToPayRecord(divideIn));
                xmlResult.AppendFormat("</root>");
                DivideResult = xmlResult.ToString();
            }
            catch
            {
                StringBuilder xmlResultError = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>");
                xmlResultError.AppendFormat("<root version=\"2.003\">");
                xmlResultError.AppendFormat("<state success=\"false\">");
                xmlResultError.AppendFormat("<error no =\"1\" info=\"分解失败!\" />");
                xmlResultError.AppendFormat("</state>");
                xmlResultError.AppendFormat("</root>");
                DivideResult = xmlResultError.ToString();               
            }
            //DivideResult = ConvertXmlToString("Divide");
        }

        /// <summary>
        /// 交易确认接口测试
        /// </summary>
        /// <param name="hObj"></param>
        /// <returns></returns>
        public virtual void Trade(out string TradeResult)
        {
            string cardNo = _dtPerson.Rows[0]["card_no"].ToString();
            try
            {
                DataTable dt = GetDataTable(@"SELECT ISNULL(FeeAll,0)*0.1 fee FROM dbo.TEST_YB_PayRecord WHERE id=(SELECT MAX(a.id) FROM dbo.TEST_YB_PayRecord a 
                     WHERE  a.RegId=(select MAX(id) FROM dbo.TEST_YB_REGISTER b WHERE b.cardNo='" + cardNo + "'))");
                double adas = 0;

                if (dt != null && dt.Rows.Count > 0)
                {
                    adas = Convert.ToDouble(dt.Rows[0][0]);
                }

                string sql = string.Format("update dbo.TEST_YB_PATIENT set personcount=personcount-{0} where card_no='{1}'", adas, cardNo);
                bool bReg = update(sql);

                DataTable dt1 = GetDataTable("SELECT personcount FROM dbo.TEST_YB_PATIENT WHERE card_no='" + cardNo + "'");
                if (dt1 != null && dt.Rows.Count > 0)
                {
                    adas = Convert.ToDouble(dt1.Rows[0][0]);
                }
                StringBuilder xmlResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>");
                xmlResult.AppendFormat("<root version=\"2.003\">");
                if (bReg)
                {
                    xmlResult.AppendFormat("<state success=\"true\">");
                    xmlResult.AppendFormat("<error no =\"1\" info=\"读卡失败,请插入社保卡!\" />");
                    xmlResult.AppendFormat("</state>");
                    xmlResult.AppendFormat("<output name=\"输出部分\">");
                    xmlResult.AppendFormat("<personcountaftersub name = \"本次结算后个人帐户余额\">{0}</personcountaftersub >", adas);
                    xmlResult.AppendFormat("<certid name = \"个人数字证书ID\" />");
                    xmlResult.AppendFormat("<sign name = \"交易签名结果\" />");
                    xmlResult.AppendFormat("</output>");
                }
                else
                {
                    xmlResult.AppendFormat("<state success=\"false\">");
                    xmlResult.AppendFormat("<error no =\"1\" info=\"读卡失败,请插入社保卡!\" />");
                    xmlResult.AppendFormat("</state>");
                }
                xmlResult.AppendFormat("</root>");
                TradeResult = xmlResult.ToString();
            }
            catch (Exception e)
            {
                StringBuilder xmlResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>");
                xmlResult.AppendFormat("<root version=\"2.003\">");
                xmlResult.AppendFormat("<state success=\"false\">");
                xmlResult.AppendFormat("<error no =\"1\" info=\"读卡失败,请插入社保卡!\" />");
                xmlResult.AppendFormat("</state>");
                xmlResult.AppendFormat("</root>");
                TradeResult = xmlResult.ToString();
            }
        }
        /// <summary>
        /// 4.2.7退费分解
        /// </summary>
        /// <param name="TradeNo"></param>
        /// <param name="DivideResult"></param>
        public virtual void RefundmentDivide(string TradeNo, out string DivideResult)
        {
            StringBuilder sResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>");
            sResult.AppendFormat("<root version=\"2.003\">");
            try
            {
                DataTable dt = GetDataTable(@"SELECT a.FeeAll,b.itemno,b.recipeno,b.hiscode,b.itemname,b.itemtype,b.unitprice,b.count,b.fee
                                         FROM dbo.TEST_YB_PayRecord a  LEFT JOIN dbo.TEST_YB_FeeItem b ON a.id=b.PayRecordId  WHERE a.id=" + TradeNo + "   ");
                double dFeeAll = 0;
                #region
                if (dt!=null && dt.Rows.Count> 0)
                {
                    sResult.AppendFormat("<state success=\"true\">");
                    sResult.AppendFormat("<warning no =\"1\" info=\"与医保中心通讯中断,不能取得个人帐户,定点医疗机构等信息,请联系网络管理员查看网络运行状况\" />");
                    sResult.AppendFormat("</state>");
                    sResult.AppendFormat("<output name=\"输出部分\">");
                    #region tradeinfo
                    sResult.AppendFormat("<tradeinfo>");
                    sResult.AppendFormat("<tradeno name = \"交易流水号\" >{0}</tradeno >", TradeNo);
                    sResult.AppendFormat("<feeno name = \"收费单据号\" >{0}</feeno >", 0);
                    sResult.AppendFormat("<tradedate name = \"交易日期\" >{0}</tradedate >", System.DateTime.Now.ToString("yyyyMMddhhmmss"));
                    sResult.AppendFormat("</tradeinfo>");
                    #endregion
                    #region feeitemarray
                    sResult.AppendFormat("<feeitemarray>");
                    foreach (DataRow dr in dt.Rows)
                    {
                        dFeeAll += Tools.ToDouble(dr["fee"].ToString(),0);
                        sResult.AppendFormat("<feeitem itemno=\"{0}\" recipeno=\"{1}\" hiscode=\"{2}\" itemcode=\"{3}\" itemname=\"{4}\" itemtype =\"{5}\" unitprice=\"{6}\" count=\"{7}\" fee=\"{8}\" feein=\"{9}\"  feeout=\"{10}\" selfpay2 =\"{11}\" state=\"{12}\" fee_type=\"{13}\" preferentialfee=\"{14}\" preferentialscale=\"{15}\" /> "
                                                  , dr["itemno"], dr["recipeno"], dr["hiscode"], "", dr["itemname"], dr["itemtype"], dr["unitprice"], dr["count"], dr["fee"], "", "", "", "", "", "", "");
                    }
                    sResult.AppendFormat("</feeitemarray>");
                    #endregion
                    #region sumpay
                    sResult.AppendFormat("<sumpay>");
                    sResult.AppendFormat("<feeall name =\"费用总金额\">{0}</feeall>", dFeeAll*-1);
                    sResult.AppendFormat("<fund name =\"基金支付\" >{0}</fund>", dFeeAll * -0.8);
                    sResult.AppendFormat("<cash name =\"现金支付\" >{0}</cash>", dFeeAll * -0.1);
                    sResult.AppendFormat("<personcountpay name =\"个人帐户支付\" >{0}</personcountpay>", dFeeAll * -0.1);
                    sResult.AppendFormat("</sumpay >");
                    #endregion
                    #region payinfo
                    sResult.AppendFormat("<payinfo>");
                    sResult.AppendFormat("<mzfee name =\"\">{0}</mzfee>", dFeeAll);
                    sResult.AppendFormat("<mzfeein name =\"\">{0}</mzfeein>", dFeeAll);
                    sResult.AppendFormat("<mzfeeout name =\"医保外费用\">{0}</mzfeeout>", 0);
                    sResult.AppendFormat("<mzpayfirst name =\"本次付起付线\">{0}</mzpayfirst>", 0);
                    sResult.AppendFormat("<mzselfpay2 name =\"\">{0}</mzselfpay2>", 0);
                    sResult.AppendFormat("<mzbigpay name =\"\">{0}</mzbigpay>", 0);
                    sResult.AppendFormat("<mzbigselfpay name =\"\">{0}</mzbigselfpay>", 0);
                    sResult.AppendFormat("<mzoutofbig name =\"\">{0}</mzoutofbig>", 0);
                    sResult.AppendFormat("<bcpay name =\"补充保险支付\" />");
                    sResult.AppendFormat("<jcbz name =\"军残补助保险支付\" />");
                    sResult.AppendFormat("</payinfo>");
                    #endregion
                    
                    sResult.AppendFormat("</output>");
                }
                else
                {
                    sResult.AppendFormat("<state success=\"false\">");
                    sResult.AppendFormat("<error no =\"1\" info=\"分解失败!\" />");
                    sResult.AppendFormat("</state>");
                }
                #endregion
                
            }
            catch (Exception e)
            {
                sResult.AppendFormat("<state success=\"false\">");
                sResult.AppendFormat("<error no =\"1\" info=\"分解失败!\" />");
                sResult.AppendFormat("</state>");
            }
            sResult.AppendFormat("</root>");
            DivideResult = sResult.ToString();
        }
        
        /// <summary>
        /// 医保查询接口测试
        /// </summary>
        /// <param name="QueryIn"></param>
        /// <param name="QueryOut"></param>
        public virtual void MedicareQuery(string QueryIn, out string QueryOut) { QueryOut = ConvertXmlToString("Close"); }
  
        /// <summary>
        /// 收据重打接口测试
        /// </summary>
        /// <param name="TradeNo"></param>
        /// <param name="InvoiceNo"></param>
        /// <param name="StateInfo"></param>
        public virtual void RePrintInvoice(string TradeNo, string InvoiceNo, out string StateInfo)
        {
            StateInfo = ConvertXmlToString("PrintInvoice");
        }        
        /// <summary>
        /// 交易状态查询及回退接口测试
        /// </summary>
        /// <param name="TradeNo"></param>
        /// <param name="TradeInfo"></param>
        public virtual void CommitTradeState(string TradeNo, out string TradeInfo){   TradeInfo = ConvertXmlToString("Close");    }
        public virtual void TransferTreatment(string HospCode, DateTime TransferDate) { ConvertXmlToString("Close"); }
        #endregion
        
        private string ResultToPayRecord(SiInterface.Common.RegClass.root root)
        {
            StringBuilder sResult = new StringBuilder();
            string sql = "";
            DataTable dt = new DataTable();
            dt.Columns.Add("itemno", Type.GetType("System.String"));
            dt.Columns.Add("recipeno", Type.GetType("System.String"));
            dt.Columns.Add("hiscode", Type.GetType("System.String"));
            dt.Columns.Add("itemname", Type.GetType("System.String"));
            dt.Columns.Add("itemtype", Type.GetType("System.String"));
            dt.Columns.Add("unitprice", Type.GetType("System.String"));
            dt.Columns.Add("count", Type.GetType("System.String"));
            dt.Columns.Add("fee", Type.GetType("System.String"));

            double dXY = 0;
            double dJC = 0;
            double delse = 0;
            double dFeeAll = 0;
            int socioNum = 0;
            int tradeno = 0;
            bool bReg = false;
            if (root.input.tradeinfo != null)
            {
                #region 挂号
                if (root.input.tradeinfo.curetype == "17")
                {
                    sql = string.Format(@"insert into dbo.TEST_YB_REGISTER(RegTime,GHFee,JCFee,cardNo)
                    values ('{0}','{1}','{2}','{3}')", System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), 2, 3, _dtPerson.Rows[0]["card_no"].ToString());

                    bReg = insert(sql);
                    if (bReg)
                    {
                        DataTable dtReg = GetDataTable(" SELECT MAX(ID) id FROM dbo.TEST_YB_REGISTER ");
                        if (dtReg != null && dtReg.Rows.Count > 0)
                        {
                            socioNum = Tools.ToInt32(dtReg.Rows[0][0].ToString(), 0);
                        }
                    }

                    if (root.input.feeitemarray != null)
                    {
                        if (root.input.feeitemarray.feeitem != null)
                        {
                            for (int r = 0; r < root.input.feeitemarray.feeitem.Length; r++)
                            {
                                DataRow dr = dt.NewRow();
                                dr["itemno"] = root.input.feeitemarray.feeitem[r].itemno;
                                dr["recipeno"] = root.input.feeitemarray.feeitem[r].recipeno;
                                dr["hiscode"] = root.input.feeitemarray.feeitem[r].hiscode;
                                dr["itemname"] = root.input.feeitemarray.feeitem[r].itemname;
                                dr["itemtype"] = root.input.feeitemarray.feeitem[r].itemtype;
                                dr["unitprice"] = root.input.feeitemarray.feeitem[r].unitprice;
                                dr["count"] = root.input.feeitemarray.feeitem[r].count;
                                dr["fee"] = root.input.feeitemarray.feeitem[r].fee;
                                dt.Rows.Add(dr);
                                if (root.input.feeitemarray.feeitem[r].itemtype == "1")
                                {
                                    dXY += Tools.ToDouble(root.input.feeitemarray.feeitem[r].fee, 0);
                                }
                                else if (root.input.feeitemarray.feeitem[r].itemtype == "2")
                                {
                                    dJC += Tools.ToDouble(root.input.feeitemarray.feeitem[r].fee, 0);
                                }
                                else
                                {
                                    delse += Tools.ToDouble(root.input.feeitemarray.feeitem[r].fee, 0);
                                }
                            }

                            dFeeAll = dXY + dJC + delse;
                            sql = string.Format(@"insert into dbo.TEST_YB_PayRecord(RegId,FeeAll,FeeFund,FeeCash)
                                values ('{0}','{1}','{2}','{3}')", socioNum, dFeeAll, dFeeAll * 0.9, dFeeAll * 0.1);
                            bReg = insert(sql);

                            if (bReg)
                            {
                                DataTable dtReg = GetDataTable(" SELECT MAX(ID) id FROM dbo.TEST_YB_PayRecord ");
                                if (dtReg != null && dtReg.Rows.Count > 0)
                                {
                                    tradeno = Tools.ToInt32(dtReg.Rows[0][0].ToString(), 0);
                                }
                            }

                            for (int m = 0; m < dt.Rows.Count; m++)
                            {
                                sql = string.Format(@"insert into dbo.TEST_YB_FeeItem(PayRecordId,itemno,recipeno,hiscode,itemname,itemtype,unitprice,count,fee)
                                values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", tradeno, dt.Rows[m]["itemno"], dt.Rows[m]["recipeno"], dt.Rows[m]["hiscode"], dt.Rows[m]["itemname"],
                                    dt.Rows[m]["itemtype"], dt.Rows[m]["unitprice"], dt.Rows[m]["count"], dt.Rows[m]["fee"]);
                                bReg = insert(sql);
                            }
                        }
                        else
                        {
                            sql = string.Format(@"insert into dbo.TEST_YB_PayRecord(RegId,FeeAll,FeeFund,FeeCash)
                                values ('{0}','{1}','{2}','{3}')", socioNum, 5, 2, 3);
                            bReg = insert(sql);
                        }

                        if (bReg)
                        {
                            DataTable dtTrade = GetDataTable(" SELECT MAX(ID) id FROM dbo.TEST_YB_PayRecord ");
                            if (dtTrade != null && dtTrade.Rows.Count > 0)
                            {
                                tradeno = Tools.ToInt32(dtTrade.Rows[0][0].ToString(), 0);
                            }
                        }

                    }
                    if (tradeno > 0)
                    {

                        sResult.AppendFormat("<state success=\"true\">");
                        sResult.AppendFormat("<warning no =\"1\" info=\"与医保中心通讯中断,不能取得个人帐户,定点医疗机构等信息,请联系网络管理员查看网络运行状况\" />");
                        sResult.AppendFormat("</state>");
                        sResult.AppendFormat("<output name=\"输出部分\">");
                        #region tradeinfo
                        sResult.AppendFormat("<tradeinfo>");
                        sResult.AppendFormat("<tradeno name = \"交易流水号\" >{0}</tradeno >", tradeno);
                        sResult.AppendFormat("<feeno name = \"收费单据号\" >{0}</feeno >", 0);
                        sResult.AppendFormat("<tradedate name = \"交易日期\" >{0}</tradedate >", System.DateTime.Now.ToString("yyyyMMddhhmmss"));
                        sResult.AppendFormat("</tradeinfo>");
                        #endregion
                        #region sumpay
                        sResult.AppendFormat("<sumpay>");
                        sResult.AppendFormat("<feeall name =\"费用总金额\">{0}</feeall>", dFeeAll);
                        sResult.AppendFormat("<fund name =\"基金支付\" >{0}</fund>", 2);
                        sResult.AppendFormat("<cash name =\"现金支付\" >{0}</cash>", 0);
                        sResult.AppendFormat("<personcountpay name =\"个人帐户支付\" >{0}</personcountpay>", 0);
                        sResult.AppendFormat("</sumpay >");
                        #endregion
                        #region payinfo
                        sResult.AppendFormat("<payinfo>");
                        sResult.AppendFormat("<mzfee name =\"\">{0}</mzfee>", 0);
                        sResult.AppendFormat("<mzfeein name =\"\">{0}</mzfeein>", 2);
                        sResult.AppendFormat("<mzfeeout name =\"医保外费用\">{0}</mzfeeout>", 0);
                        sResult.AppendFormat("<mzpayfirst name =\"本次付起付线\">{0}</mzpayfirst>", 0);
                        sResult.AppendFormat("<mzselfpay2 name =\"\">{0}</mzselfpay2>", 0);
                        sResult.AppendFormat("<mzbigpay name =\"\">{0}</mzbigpay>", 0);
                        sResult.AppendFormat("<mzbigselfpay name =\"\">{0}</mzbigselfpay>", 0);
                        sResult.AppendFormat("<mzoutofbig name =\"\">{0}</mzoutofbig>", 0);
                        sResult.AppendFormat("<bcpay name =\"补充保险支付\" />");
                        sResult.AppendFormat("<jcbz name =\"军残补助保险支付\" />");
                        sResult.AppendFormat("</payinfo>");
                        #endregion
                        #region feeitemarray
                        sResult.AppendFormat("<feeitemarray>");
                        foreach (DataRow dr in dt.Rows)
                        {
                            sResult.AppendFormat("<feeitem itemno=\"{0}\" recipeno=\"{1}\" hiscode=\"{2}\" itemcode=\"{3}\" itemname=\"{4}\" itemtype =\"{5}\" unitprice=\"{6}\" count=\"{7}\" fee=\"{8}\" feein=\"{9}\"  feeout=\"{10}\" selfpay2 =\"{11}\" state=\"{12}\" fee_type=\"{13}\" preferentialfee=\"{14}\" preferentialscale=\"{15}\" /> "
                                                      , dr["itemno"], dr["recipeno"], dr["hiscode"], "", dr["itemname"], dr["itemtype"], dr["unitprice"], dr["count"], dr["fee"], "", "", "", "", "", "", "");
                        }
                        sResult.AppendFormat("</feeitemarray>");
                        #endregion
                        #region medicatalog
                        sResult.AppendFormat("<medicatalog>");
                        sResult.AppendFormat("<medicine name=\"西药\">{0}</medicine>", dXY);
                        sResult.AppendFormat("<tmedicine name=\"中成药\">{0}</tmedicine>", 0);
                        sResult.AppendFormat("<therb name=\"中草药\">{0}</therb>", 0);
                        sResult.AppendFormat("<examine name=\"检查费\">{0}</examine>", dJC);
                        sResult.AppendFormat("<ct>{0}</ct>", 0);
                        sResult.AppendFormat("<mri name=\"核磁\">{0}</mri>", 0);
                        sResult.AppendFormat("<ultrasonic name=\"b超\">{0}</ultrasonic>", 0);
                        sResult.AppendFormat("<oxygen name=\"输氧费\">{0}</oxygen>", 0);
                        sResult.AppendFormat("<operation name=\"手术费\">{0}</operation>", 0);
                        sResult.AppendFormat("<treatment name=\"治疗费\">{0}</treatment>", 0);
                        sResult.AppendFormat("<xray name=\"放射\">{0}</xray>", 0);
                        sResult.AppendFormat("<labexam name=\"化验\">{0}</labexam>", 0);
                        sResult.AppendFormat("<bloodt name=\"输血费\">{0}</bloodt>", 0);
                        sResult.AppendFormat("<orthodontics name=\"正畸费\">{0}</orthodontics>", 0);
                        sResult.AppendFormat("<prosthesis name=\"镶牙费\">{0}</prosthesis>", 0);
                        sResult.AppendFormat("<forensic_expertise name=\"司法鉴定\">{0}</forensic_expertise>", 0);
                        sResult.AppendFormat("<material name=\"材料费\">{0}</material>", 0);
                        sResult.AppendFormat("<other name=\"其他项目\">{0}</other>", delse);
                        sResult.AppendFormat("</medicatalog>");
                        #endregion
                        sResult.AppendFormat("</output>");
                    }
                    else
                    {
                        sResult.AppendFormat("<state success=\"false\">");
                        sResult.AppendFormat("<error no =\"1\" info=\"分解失败!\" />");
                        sResult.AppendFormat("</state>");
                    }
                }
                #endregion
                #region 收费
                else if (root.input.tradeinfo.curetype == "11")
                {
                    if (root.input.feeitemarray != null)
                    {
                        if (root.input.feeitemarray.feeitem != null)
                        {
                            for (int r = 0; r < root.input.feeitemarray.feeitem.Length; r++)
                            {
                                DataRow dr = dt.NewRow();
                                dr["itemno"] = root.input.feeitemarray.feeitem[r].itemno;
                                dr["recipeno"] = root.input.feeitemarray.feeitem[r].recipeno;
                                dr["hiscode"] = root.input.feeitemarray.feeitem[r].hiscode;
                                dr["itemname"] = root.input.feeitemarray.feeitem[r].itemname;
                                dr["itemtype"] = root.input.feeitemarray.feeitem[r].itemtype;
                                dr["unitprice"] = root.input.feeitemarray.feeitem[r].unitprice;
                                dr["count"] = root.input.feeitemarray.feeitem[r].count;
                                dr["fee"] = root.input.feeitemarray.feeitem[r].fee;
                                dt.Rows.Add(dr);
                                if (root.input.feeitemarray.feeitem[r].itemtype == "1")
                                {
                                    dXY += Tools.ToDouble(root.input.feeitemarray.feeitem[r].fee, 0);
                                }
                                else if (root.input.feeitemarray.feeitem[r].itemtype == "2")
                                {
                                    dJC += Tools.ToDouble(root.input.feeitemarray.feeitem[r].fee, 0);
                                }
                                else
                                {
                                    delse += Tools.ToDouble(root.input.feeitemarray.feeitem[r].fee, 0);
                                }
                            }

                            DataTable dtReg = GetDataTable(" SELECT MAX(ID) id FROM dbo.TEST_YB_REGISTER ");
                            if (dtReg != null && dtReg.Rows.Count > 0)
                            {
                                socioNum = Tools.ToInt32(dtReg.Rows[0][0].ToString(), 0);
                            }

                            dFeeAll = dXY + dJC + delse;
                            sql = string.Format(@"insert into dbo.TEST_YB_PayRecord(RegId,FeeAll,FeeFund,FeeCash)
                                values ('{0}','{1}','{2}','{3}')", socioNum, dFeeAll, dFeeAll * 0.8, dFeeAll * 0.1);
                            bReg = insert(sql);

                            if (bReg)
                            {
                                DataTable dtTrade = GetDataTable(" SELECT MAX(ID) id FROM dbo.TEST_YB_PayRecord ");
                                if (dtTrade != null && dtTrade.Rows.Count > 0)
                                {
                                    tradeno = Tools.ToInt32(dtTrade.Rows[0][0].ToString(), 0);
                                }
                            }

                            for (int m = 0; m < dt.Rows.Count; m++)
                            {
                                sql = string.Format(@"insert into dbo.TEST_YB_FeeItem(PayRecordId,itemno,recipeno,hiscode,itemname,itemtype,unitprice,count,fee)
                                values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", tradeno, dt.Rows[m]["itemno"], dt.Rows[m]["recipeno"], dt.Rows[m]["hiscode"], dt.Rows[m]["itemname"],
                                    dt.Rows[m]["itemtype"], dt.Rows[m]["unitprice"], dt.Rows[m]["count"], dt.Rows[m]["fee"]);
                                bReg = insert(sql);
                            }
                        }
                    }

                    if (tradeno > 0)
                    {
                        sResult.AppendFormat("<state success=\"true\">");
                        sResult.AppendFormat("<warning no =\"1\" info=\"与医保中心通讯中断,不能取得个人帐户,定点医疗机构等信息,请联系网络管理员查看网络运行状况\" />");
                        sResult.AppendFormat("</state>");
                        sResult.AppendFormat("<output name=\"输出部分\">");
                        #region tradeinfo
                        sResult.AppendFormat("<tradeinfo>");
                        sResult.AppendFormat("<tradeno name = \"交易流水号\" >{0}</tradeno >",tradeno);
                        sResult.AppendFormat("<feeno name = \"收费单据号\" >{0}</feeno >",0);
                        sResult.AppendFormat("<tradedate name = \"交易日期\" >{0}</tradedate >",System.DateTime.Now.ToString("yyyyMMddhhmmss"));
                        sResult.AppendFormat("</tradeinfo>");
                        #endregion
                        #region sumpay
                        sResult.AppendFormat("<sumpay>");
                        sResult.AppendFormat("<feeall name =\"费用总金额\">{0}</feeall>", dFeeAll);
                        sResult.AppendFormat("<fund name =\"基金支付\" >{0}</fund>", dFeeAll*0.8);
                        sResult.AppendFormat("<cash name =\"现金支付\" >{0}</cash>", dFeeAll * 0.1);
                        sResult.AppendFormat("<personcountpay name =\"个人帐户支付\" >{0}</personcountpay>", dFeeAll * 0.1);
                        sResult.AppendFormat("</sumpay >");
                        #endregion
                        #region payinfo
                        sResult.AppendFormat("<payinfo>");
                        sResult.AppendFormat("<mzfee name =\"\">{0}</mzfee>", dFeeAll);
                        sResult.AppendFormat("<mzfeein name =\"\">{0}</mzfeein>", dFeeAll);
                        sResult.AppendFormat("<mzfeeout name =\"医保外费用\">{0}</mzfeeout>", 0);
                        sResult.AppendFormat("<mzpayfirst name =\"本次付起付线\">{0}</mzpayfirst>", 0);
                        sResult.AppendFormat("<mzselfpay2 name =\"\">{0}</mzselfpay2>", 0);
                        sResult.AppendFormat("<mzbigpay name =\"\">{0}</mzbigpay>", 0);
                        sResult.AppendFormat("<mzbigselfpay name =\"\">{0}</mzbigselfpay>", 0);
                        sResult.AppendFormat("<mzoutofbig name =\"\">{0}</mzoutofbig>", 0);
                        sResult.AppendFormat("<bcpay name =\"补充保险支付\" />");
                        sResult.AppendFormat("<jcbz name =\"军残补助保险支付\" />");
                        sResult.AppendFormat("</payinfo>");
                        #endregion
                        #region feeitemarray
                        sResult.AppendFormat("<feeitemarray>");
                        foreach (DataRow dr in dt.Rows)
                        {
                            sResult.AppendFormat("<feeitem itemno=\"{0}\" recipeno=\"{1}\" hiscode=\"{2}\" itemcode=\"{3}\" itemname=\"{4}\" itemtype =\"{5}\" unitprice=\"{6}\" count=\"{7}\" fee=\"{8}\" feein=\"{9}\"  feeout=\"{10}\" selfpay2 =\"{11}\" state=\"{12}\" fee_type=\"{13}\" preferentialfee=\"{14}\" preferentialscale=\"{15}\" /> "
                                                      ,dr["itemno"],  dr["recipeno"],  dr["hiscode"],               "", dr["itemname"],  dr["itemtype"],   dr["unitprice"],  dr["count"],  dr["fee"],"","","","","","","");
                        }
                        sResult.AppendFormat("</feeitemarray>");
                        #endregion
                        #region medicatalog
                        sResult.AppendFormat("<medicatalog>");
                        sResult.AppendFormat("<medicine name=\"西药\">{0}</medicine>", dXY);
                        sResult.AppendFormat("<tmedicine name=\"中成药\">{0}</tmedicine>", 0);
                        sResult.AppendFormat("<therb name=\"中草药\">{0}</therb>", 0);
                        sResult.AppendFormat("<examine name=\"检查费\">{0}</examine>", dJC);
                        sResult.AppendFormat("<ct>{0}</ct>", 0);
                        sResult.AppendFormat("<mri name=\"核磁\">{0}</mri>", 0);
                        sResult.AppendFormat("<ultrasonic name=\"b超\">{0}</ultrasonic>", 0);
                        sResult.AppendFormat("<oxygen name=\"输氧费\">{0}</oxygen>", 0);
                        sResult.AppendFormat("<operation name=\"手术费\">{0}</operation>", 0);
                        sResult.AppendFormat("<treatment name=\"治疗费\">{0}</treatment>", 0);
                        sResult.AppendFormat("<xray name=\"放射\">{0}</xray>", 0);
                        sResult.AppendFormat("<labexam name=\"化验\">{0}</labexam>", 0);
                        sResult.AppendFormat("<bloodt name=\"输血费\">{0}</bloodt>", 0);
                        sResult.AppendFormat("<orthodontics name=\"正畸费\">{0}</orthodontics>", 0);
                        sResult.AppendFormat("<prosthesis name=\"镶牙费\">{0}</prosthesis>", 0);
                        sResult.AppendFormat("<forensic_expertise name=\"司法鉴定\">{0}</forensic_expertise>", 0);
                        sResult.AppendFormat("<material name=\"材料费\">{0}</material>", 0);
                        sResult.AppendFormat("<other name=\"其他项目\">{0}</other>", delse);
                        sResult.AppendFormat("</medicatalog>");
                        #endregion
                        sResult.AppendFormat("</output>");
                    }
                    else
                    {
                        sResult.AppendFormat("<state success=\"false\">");
                        sResult.AppendFormat("<error no =\"1\" info=\"分解失败!\" />");
                        sResult.AppendFormat("</state>");
                    }

                }
                #endregion
            }
            return sResult.ToString();
        }

        

        public string ConvertXmlToString(string s)
        {
            XmlDocument xmlDoc = new XmlDocument();

            //xmlDoc.Load(@"../Output/XML/" + s+".xml"); //加载一个xml文件
            xmlDoc.Load("D:\\SinoCloudHIS\\cloudhospital\\PluginServer\\MIProject\\SiInterface\\XML\\" + s + ".xml");
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, null);
            writer.Formatting = Formatting.Indented;
            xmlDoc.Save(writer);
            StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
            stream.Position = 0;
            string xmlString = sr.ReadToEnd();
            sr.Close();
            stream.Close();
            return xmlString;
        }
    }
}
