using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EFWCoreLib.WcfFrame.ClientController;
using HIS_Entity.OPManage;

namespace HIS_OPManage.Winform.Controller
{
    partial class BalancePrint : WcfClientController
    {
        /// <summary>
        ///  小票打印,打明细
        /// </summary>
        /// <param name="dtInvoice">发票表</param>
        /// <param name="dtInvoiceDetail">费用明细表</param>
        /// <param name="dtInvoiceStatDetail">费用明细按大项目统计表</param>
        /// <param name="chargeInfo">收费信息</param>
        /// <param name="workName">医院名称</param>
        public void BillPrintDetail(DataTable dtInvoice, DataTable dtInvoiceDetail, DataTable dtInvoiceStatDetail, ChargeInfo chargeInfo, string workName)
        {
            for (int i = 0; i < dtInvoice.Rows.Count; i++)
            {
                string invoiceno = dtInvoice.Rows[i]["invoiceNO"].ToString();
                decimal totalFee = Convert.ToDecimal(dtInvoice.Rows[i]["TotalFee"]);
                string filter = "invoiceNO = '" + invoiceno + "'";
                DataRow[] dr = dtInvoiceDetail.Select(filter);

                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                myDictionary.Add("PatName", dr[0]["PatName"]);
                myDictionary.Add("InvoiceNO", invoiceno);
                myDictionary.Add("VisitNO", dr[0]["visitNO"]);
                myDictionary.Add("Operator", dr[0]["ChargeName"]);
                myDictionary.Add("ChargeDate", dr[0]["ChargeDate"]);
                myDictionary.Add("TotalFee", totalFee);
                myDictionary.Add("WtotalFee", CmycurD(totalFee));
                myDictionary.Add("HospitalName", workName);
                decimal actPay = 0;//实收金额
                StringBuilder sb = new StringBuilder();
                foreach (OP_CostPayMentInfo payment in chargeInfo.PayInfoList)
                {
                    sb.Append(payment.PayMentName + "： " + payment.PayMentMoney + "\n\n");
                    actPay += payment.PayMentMoney;
                }

                decimal roudingFee =Convert.ToDecimal((actPay-totalFee).ToString("0.00"));
                myDictionary.Add("ActPay", actPay);
                myDictionary.Add("RoundingFee", roudingFee);
                myDictionary.Add("payInfo", sb.ToString());
                DataTable dtPrint = new DataTable();
                DataColumn col = new DataColumn();
                col.ColumnName = "ItemName";
                col.DataType = typeof(string);
                dtPrint.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "Amount";
                col.DataType = typeof(string);
                dtPrint.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "TotalFee";
                col.DataType = typeof(string);
                dtPrint.Columns.Add(col);

                foreach (DataRow row in dr)
                {
                    DataRow printrow = dtPrint.NewRow();
                    printrow["ItemName"] = row["ItemName"];
                    printrow["Amount"] = row["Amount"].ToString() + row["MiniUnit"];
                    printrow["TotalFee"] = row["TotalFee"];
                    
                    dtPrint.Rows.Add(printrow);
                }

                EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 2002, 0, myDictionary, dtPrint).Print(true);
            }
        }

        /// <summary>
        /// 小票打印，按大项目分类打印
        /// </summary>
        /// <param name="dtInvoice">发票表</param>
        /// <param name="dtInvoiceDetail">费用明细表</param>
        /// <param name="dtInvoiceStatDetail">费用明细按大项目统计表</param>
        /// <param name="chargeInfo">收费信息</param>
        /// <param name="workName">医院名称</param>
        public void BillPrintStatDetail(DataTable dtInvoice, DataTable dtInvoiceDetail, DataTable dtInvoiceStatDetail, ChargeInfo chargeInfo, string workName)
        {
            for (int i = 0; i < dtInvoice.Rows.Count; i++)
            {
                string invoiceno = dtInvoice.Rows[i]["invoiceNO"].ToString();
                decimal totalFee = Convert.ToDecimal(dtInvoice.Rows[i]["TotalFee"]);
                string filter = "invoiceNO = '" + invoiceno + "'";
                DataRow[] dr = dtInvoiceDetail.Select(filter);

                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                myDictionary.Add("PatName", dr[0]["PatName"]);
                myDictionary.Add("InvoiceNO", invoiceno);
                myDictionary.Add("VisitNO", dr[0]["visitNO"]);
                myDictionary.Add("Operator", dr[0]["ChargeName"]);
                myDictionary.Add("ChargeDate", dr[0]["ChargeDate"]);
                myDictionary.Add("TotalFee", totalFee);
                myDictionary.Add("WtotalFee", CmycurD(totalFee));
                myDictionary.Add("HospitalName", workName);
                decimal actPay = 0;//实收金额
                StringBuilder sb = new StringBuilder();
                foreach (OP_CostPayMentInfo payment in chargeInfo.PayInfoList)
                {
                    sb.Append(payment.PayMentName + "： " + payment.PayMentMoney + "\n\n");
                    actPay += payment.PayMentMoney;
                }

                decimal roudingFee = Convert.ToDecimal((actPay - totalFee).ToString("0.00"));
                myDictionary.Add("ActPay", actPay);
                myDictionary.Add("RoundingFee", roudingFee);
                myDictionary.Add("payInfo", sb.ToString());
                DataTable dtPrint = new DataTable();
                DataColumn col = new DataColumn();
                col.ColumnName = "ItemName";
                col.DataType = typeof(string);
                dtPrint.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "Amount";
                col.DataType = typeof(string);
                dtPrint.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "TotalFee";
                col.DataType = typeof(string);
                dtPrint.Columns.Add(col);


                DataRow[] statRow = dtInvoiceStatDetail.Select(filter);
                foreach (DataRow row in statRow)
                {
                    DataRow printrow = dtPrint.NewRow();
                    printrow["ItemName"] = row["statname"];
                    printrow["Amount"] = Convert.ToDecimal(row["TotalFee"]).ToString("0.00");
                    printrow["TotalFee"] =Convert.ToDecimal( row["TotalFee"]).ToString("0.00");
                    dtPrint.Rows.Add(printrow);
                }

                EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 2003, 0, myDictionary, dtPrint).Print(true);
            }
        }

        /// <summary>
        /// 发票打印
        /// </summary>
        /// <param name="dtInvoice">原票据表</param>
        /// <param name="dtInvoiceDetail">明细</param>
        /// <param name="dtInvoiceStatDetail">项目分类数据</param>
        /// <param name="invoiceNO">补打票据号</param>
        /// <param name="chargeInfo">支付信息</param>
        /// <param name="workName">机构名称</param>
        /// <param name="empName">补打人员</param>
        public void InvoicePrint(DataTable dtInvoice, DataTable dtInvoiceDetail, DataTable dtInvoiceStatDetail, string invoiceNO, ChargeInfo chargeInfo, string workName, string empName)
        {
            decimal toTalFee = Convert.ToDecimal(dtInvoiceDetail.Compute("sum(TotalFee)", string.Empty));
            if (dtInvoiceDetail.Rows.Count == 0)
            {
                return;
            }

            DataRow dr = dtInvoiceDetail.Rows[0];
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            myDictionary.Add("PatName", dr["PatName"]);
            myDictionary.Add("InvoiceNO", invoiceNO);
            myDictionary.Add("VisitNO", dr["visitNO"]);
            myDictionary.Add("Operator", dr["ChargeName"]);
            myDictionary.Add("ChargeDate", dr["ChargeDate"]);
            myDictionary.Add("TotalFee", toTalFee);
            myDictionary.Add("WtotalFee", CmycurD(toTalFee));
            myDictionary.Add("HospitalName", workName);

            myDictionary.Add("PrintAgain", "补打票据");
            myDictionary.Add("PrintAgainEmpName", "补打人" + empName);
            decimal actPay = 0;//实收金额
            StringBuilder sb = new StringBuilder();
            foreach (OP_CostPayMentInfo payment in chargeInfo.PayInfoList)
            {
                sb.Append(payment.PayMentName + "： " + payment.PayMentMoney + "\n\n");
                actPay += payment.PayMentMoney;
            }

            decimal roudingFee = Convert.ToDecimal((actPay - toTalFee).ToString("0.00"));
            myDictionary.Add("ActPay", actPay);
            myDictionary.Add("RoundingFee", roudingFee);
            myDictionary.Add("payInfo", sb.ToString());
            DataTable dtPrint = new DataTable();
            DataColumn col = new DataColumn();
            col.ColumnName = "ItemName";
            col.DataType = typeof(string);
            dtPrint.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "Amount";
            col.DataType = typeof(string);
            dtPrint.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "TotalFee";
            col.DataType = typeof(string);
            dtPrint.Columns.Add(col);
            foreach (DataRow row in dtInvoiceDetail.Rows)
            {
                DataRow printrow = dtPrint.NewRow();
                printrow["ItemName"] = row["ItemName"];
                printrow["Amount"] = row["Amount"].ToString() + row["MiniUnit"];
                printrow["TotalFee"] = row["TotalFee"];
                dtPrint.Rows.Add(printrow);
            }

            EfwControls.CustomControl.ReportTool.GetReport(
                         LoginUserInfo.WorkId, 2002, 0, myDictionary, dtPrint).Print(true);
        }

        /// <summary>
        /// 转换人民币大小金额
        /// </summary>
        /// <param name="num">金额</param>
        /// <returns>返回大写形式</returns>
        private string CmycurD(decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字
            string str3 = string.Empty;    //从原num值中取出的值
            string str4 = string.Empty;    //数字的字符串形式
            string str5 = string.Empty;  //人民币大写金额形式
            int i;    //循环变量
            int j;    //num的值乘以100的字符串长度
            string ch1 = string.Empty;    //数字的汉语读法
            string ch2 = string.Empty;    //数字位的汉字读法
            int nzero = 0;  //用来计算连续的零值是几个
            int temp;            //从原num值中取出的值

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数
            str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式
            j = str4.Length;      //找出最高位
            if (j > 15)
            {
                return "溢出";
            }

            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分

            //循环取出每一位需要转换的值
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //取出需转换的某一位的值
                temp = Convert.ToInt32(str3);      //转换为数字
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时
                    if (str3 == "0")
                    {
                        ch1 = string.Empty;
                        ch2 = string.Empty;
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = string.Empty;
                                ch2 = string.Empty;
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = string.Empty;
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = string.Empty;
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }

                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上
                    ch2 = str2.Substring(i, 1);
                }

                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整”
                    str5 = str5 + '整';
                }
            }

            if (num == 0)
            {
                str5 = "零元整";
            }

            return str5;
        }

        /// <summary>
        /// 费用清单打印
        /// </summary>
        /// <param name="dtInvoice">发票号表</param>
        /// <param name="dtInvoiceDetail">费用明细数据</param>
        /// <param name="dtInvoiceStatDetail">费用明细按大项目数据</param>
        /// <param name="chargeInfo">收费信息</param>
        /// <param name="workName">医院名称</param>
        public void PresFeePrintDetailOld(DataTable dtInvoice, DataTable dtInvoiceDetail, DataTable dtInvoiceStatDetail, ChargeInfo chargeInfo, string workName)
        {
            var result2 = from r in dtInvoiceDetail.AsEnumerable()
                          group r by new
                          {
                              invoiceNO = r["invoiceNO"].ToString(),
                              ItemID = Convert.ToInt32(r["ItemID"]),
                              ItemName = r["ItemName"].ToString(),
                              Spec = r["Spec"].ToString(),
                              MiniUnit = r["MiniUnit"].ToString(),
                              PatName = r["PatName"].ToString(),
                              VisitNO = r["VisitNO"].ToString(),
                              Operator = r["ChargeName"].ToString(),
                              ChargeDate = r["ChargeDate"].ToString(),
                              RetailPrice = r["RetailPrice"].ToString(),
                              YBItemLevel =r["YBItemLevel"].ToString()
                          } 
                          into g
                          select new
                          {
                              invoiceNO = g.Key.invoiceNO,
                              ItemID = g.Key.ItemID,
                              ItemName = g.Key.ItemName,
                              Spec = g.Key.Spec,
                              MiniUnit = g.Key.MiniUnit,
                              PatName = g.Key.PatName,
                              VisitNO = g.Key.VisitNO,
                              Operator = g.Key.Operator,
                              ChargeDate = g.Key.ChargeDate,
                              RetailPrice = g.Key.RetailPrice,
                              YBItemLevel=g.Key.YBItemLevel,
                              Amount = g.Sum(r => Convert.ToDecimal(r["Amount"])),
                              TotalFee = g.Sum(r => Convert.ToDecimal(r["TotalFee"]))
                          };

            for (int i = 0; i < dtInvoice.Rows.Count; i++)
            {
                string invoiceno = dtInvoice.Rows[i]["invoiceNO"].ToString();
                decimal totalFee = Convert.ToDecimal(dtInvoice.Rows[i]["TotalFee"]);
                string filter = "invoiceNO = '" + invoiceno + "'";
                DataRow[] dr = dtInvoiceDetail.Select(filter);
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                myDictionary.Add("PatName", dr[0]["PatName"]);
                /////////////////////////////////////////
                myDictionary.Add("IDNumber", dr[0]["IDNumber"]);
                myDictionary.Add("MedicareCard", dr[0]["MedicareCard"]);
                /////////////////////////////////////////
                myDictionary.Add("VisitNO", dr[0]["visitNO"]);
                myDictionary.Add("Operator", dr[0]["ChargeName"]);
                myDictionary.Add("ChargeDate", dr[0]["ChargeDate"]);
                myDictionary.Add("TotalFee", totalFee);
                myDictionary.Add("WtotalFee", CmycurD(totalFee));
                myDictionary.Add("HospitalName", workName);
                DataTable dtPrint = new DataTable();
                DataColumn col = new DataColumn();
                col.ColumnName = "ItemName";
                col.DataType = typeof(string);
                dtPrint.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "Spec";
                col.DataType = typeof(string);
                dtPrint.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "Amount";
                col.DataType = typeof(string);
                dtPrint.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "TotalFee";
                col.DataType = typeof(string);
                dtPrint.Columns.Add(col);
                ///增加单价和费用类别
                col = new DataColumn();
                col.ColumnName = "Price";
                col.DataType = typeof(string);
                dtPrint.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "Feetype";
                col.DataType = typeof(string);
                dtPrint.Columns.Add(col);

                foreach ( var row in result2)
                {
                    if (row.invoiceNO == invoiceno)
                    {
                        DataRow printrow = dtPrint.NewRow();
                        printrow["ItemName"] = row.ItemName;// row["ItemName"];
                        printrow["Spec"] = row.Spec;// row["Spec"];
                        printrow["Amount"] = row.Amount + row.MiniUnit;// row["Amount"].ToString() + row["MiniUnit"];
                        printrow["TotalFee"] = row.TotalFee.ToString("0.00");// row["TotalFee"];
                                                                             
                        printrow["Price"] = row.RetailPrice;
                        printrow["Feetype"] = row.YBItemLevel;
                        dtPrint.Rows.Add(printrow);
                    }
                }

                EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 2019, 0, myDictionary, dtPrint).Print(true);
            }
        }

        public void PresFeePrintDetail(DataTable dtInvoice, DataTable dtInvoiceDetail, DataTable dtInvoiceStatDetail, ChargeInfo chargeInfo, string workName)
        {
            var result2 = from r in dtInvoiceDetail.AsEnumerable()
                          group r by new
                          {
                              invoiceNO = r["invoiceNO"].ToString(),
                              ItemID = Convert.ToInt32(r["ItemID"]),
                              ItemName = r["ItemName"].ToString(),
                              Spec = r["Spec"].ToString(),
                              MiniUnit = r["MiniUnit"].ToString(),
                              PatName = r["PatName"].ToString(),
                              VisitNO = r["VisitNO"].ToString(),
                              Operator = r["ChargeName"].ToString(),
                              ChargeDate = r["ChargeDate"].ToString(),
                              RetailPrice = r["RetailPrice"].ToString(),
                              YBItemLevel = r["YBItemLevel"].ToString()
                          }
                          into g
                          select new
                          {
                              invoiceNO = g.Key.invoiceNO,
                              ItemID = g.Key.ItemID,
                              ItemName = g.Key.ItemName,
                              Spec = g.Key.Spec,
                              MiniUnit = g.Key.MiniUnit,
                              PatName = g.Key.PatName,
                              VisitNO = g.Key.VisitNO,
                              Operator = g.Key.Operator,
                              ChargeDate = g.Key.ChargeDate,
                              RetailPrice = g.Key.RetailPrice,
                              YBItemLevel = g.Key.YBItemLevel,
                              Amount = g.Sum(r => Convert.ToDecimal(r["Amount"])),
                              TotalFee = g.Sum(r => Convert.ToDecimal(r["TotalFee"]))
                          };

            for (int i = 0; i < dtInvoice.Rows.Count; i++)
            {
                string invoiceno = dtInvoice.Rows[i]["invoiceNO"].ToString();
                decimal totalFee = Convert.ToDecimal(dtInvoice.Rows[i]["TotalFee"]);
                string filter = "invoiceNO = '" + invoiceno + "'";
                DataRow[] dr = dtInvoiceDetail.Select(filter);
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                myDictionary.Add("PatName", dr[0]["PatName"]);
                /////////////////////////////////////////
                myDictionary.Add("IDNumber", dr[0]["IDNumber"]);
                myDictionary.Add("MedicareCard", dr[0]["MedicareCard"]);
                myDictionary.Add("InvoiceNO", invoiceno);
                /////////////////////////////////////////
                myDictionary.Add("VisitNO", dr[0]["visitNO"]);
                myDictionary.Add("Operator", dr[0]["ChargeName"]);
                myDictionary.Add("ChargeDate", dr[0]["ChargeDate"]);
                myDictionary.Add("TotalFee", totalFee);
                myDictionary.Add("WtotalFee", CmycurD(totalFee));
                myDictionary.Add("HospitalName", workName);
                DataTable dtPrint = new DataTable();
                DataColumn col = new DataColumn();
                col.ColumnName = "ItemName";
                col.DataType = typeof(string);
                dtPrint.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "Spec";
                col.DataType = typeof(string);
                dtPrint.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "Amount";
                col.DataType = typeof(string);
                dtPrint.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "TotalFee";
                col.DataType = typeof(string);
                dtPrint.Columns.Add(col);
                ///增加单价和费用类别
                col = new DataColumn();
                col.ColumnName = "Price";
                col.DataType = typeof(string);
                dtPrint.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "Feetype";
                col.DataType = typeof(string);
                dtPrint.Columns.Add(col);

                col = new DataColumn();
                col.ColumnName = "Unit";
                col.DataType = typeof(string);
                dtPrint.Columns.Add(col);

                foreach (var row in result2)
                {
                    if (row.invoiceNO == invoiceno)
                    {
                        DataRow printrow = dtPrint.NewRow();
                        printrow["ItemName"] = row.ItemName;// row["ItemName"];
                        printrow["Spec"] = row.Spec;// row["Spec"];
                        printrow["Amount"] = row.Amount + row.MiniUnit;// row["Amount"].ToString() + row["MiniUnit"];
                        printrow["TotalFee"] = row.TotalFee.ToString("0.00");// row["TotalFee"];
                        printrow["Unit"] = row.MiniUnit;
                        printrow["Price"] = row.RetailPrice;
                        printrow["Feetype"] = row.YBItemLevel;
                        dtPrint.Rows.Add(printrow);
                    }
                }
                DataRow[] statRow = dtInvoiceStatDetail.Select(filter);
                foreach (DataRow row in statRow)
                {                
                    myDictionary.Add(row["statname"].ToString().Trim(), Convert.ToDecimal(row["TotalFee"]).ToString("0.00"));
                }
                EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 2019, 0, myDictionary, dtPrint).Print(false);
            }          
        }
    }
}
