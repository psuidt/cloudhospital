using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.OPManage;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.Controller
{
    [WinformController(DefaultViewName = "FrmAccount")]//与系统菜单对应
    [WinformView(Name = "FrmAccount", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmAccount")]
    [WinformView(Name = "FrmInvoiceDetail", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmInvoiceDetail")]
    [WinformView(Name = "FrmAccountQuery", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmAccount")]
   
    /// <summary>
    /// 缴款控件器类
    /// </summary>
    public class AccountController : WcfClientController
    {
        /// <summary>
        /// 缴款界面接口类
        /// </summary>
        IFrmAccount ifrmAccount;

        /// <summary>
        /// 票据明细界面接口类
        /// </summary>
        IFrmInvoiceDetail ifrmInvoiceDetail;

        /// <summary>
        /// 缴款查询界面接口类
        /// </summary>
        IFrmAccount ifrmAccountQuery;

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            ifrmAccount = (IFrmAccount)iBaseView["FrmAccount"];
            ifrmInvoiceDetail = (IFrmInvoiceDetail)iBaseView["FrmInvoiceDetail"];
        }

        /// <summary>
        /// 缴款界面基础数据获取，获取缴款列表
        /// </summary>
        /// <param name="frmname">窗体名称</param>
        [WinformMethod]
        public void AccountInit(string frmname)
        {
            if (frmname == "FrmAccount")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                   {
                       request.AddData(LoginUserInfo.EmpId);
                       request.AddData(ifrmAccount.bdate);
                       request.AddData(ifrmAccount.edate);
                   });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "AccountController", "AccountInit", requestAction);
                DataTable dtCashier = retdata.GetData<DataTable>(0);
                List<OP_Account> notAccountList = retdata.GetData<List<OP_Account>>(1);//未缴款列表
                List<OP_Account> historyAccountList = retdata.GetData<List<OP_Account>>(2);//历史缴款列表
                ifrmAccount.loadCashier(dtCashier);
                ifrmAccount.GetQueryEmpID = LoginUserInfo.EmpId;
                ifrmAccount.BindTree(notAccountList, historyAccountList);//缴款列表绑定
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(ifrmAccount.QueryAccountId);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "AccountController", "QueryAccountInit", requestAction);
                OP_Account curAccount = retdata.GetData<OP_Account>(0);
                ifrmAccount.CurAccount = curAccount;
                string totalFeeDx = CmycurD(ifrmAccount.CurAccount.TotalFee);
                ifrmAccount.SetAccountDxTotalFee(totalFeeDx);
                GetAccountData();
            }
        }

        /// <summary>
        /// 窗体变换，个人缴款和门诊缴款查询
        /// </summary>
        /// <param name="frmname">窗体名称</param>
        [WinformMethod]
        public void ChangeValue(string frmname)
        {
            if (frmname == "FrmAccount")
            {
                ifrmAccount = (IFrmAccount)iBaseView["FrmAccount"];
            }
            else
            {
                ifrmAccount = (IFrmAccount)iBaseView["FrmAccountQuery"];
            }
        }

        /// <summary>
        /// 重新获取缴款树列表
        /// </summary>
        [WinformMethod]
        public void AccountQuery()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.EmpId);
                request.AddData(ifrmAccount.bdate);
                request.AddData(ifrmAccount.edate);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "AccountController", "QueryAccountList", requestAction);
            List<OP_Account> notAccountList = retdata.GetData<List<OP_Account>>(0);
            List<OP_Account> historyAccountList = retdata.GetData<List<OP_Account>>(1);
            ifrmAccount.BindTree(notAccountList, historyAccountList);
        }

        /// <summary>
        /// 获取结账各项数据
        /// </summary>
        [WinformMethod]
        public void GetAccountData()
        {
            try
            {
                if (ifrmAccount.CurAccount == null)
                {
                    return;
                }

                if (ifrmAccount.CurAccount.AccountType == 0)
                {
                    Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(ifrmAccount.CurAccount.AccountID);//缴款ID
                        request.AddData(ifrmAccount.CurAccount.AccountFlag);
                    });
                    ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "AccountController", "GetAccountData", requestAction);
                    DataTable dtInvoiceData = retdata.GetData<DataTable>(0);//票据数据
                    DataTable dtPayMentData = retdata.GetData<DataTable>(1);//支付信息
                    DataTable dtItemData = retdata.GetData<DataTable>(2);//项目信息

                    ifrmAccount.dtAccountInvoiceData = dtInvoiceData;
                    ifrmAccount.dtAccountItemData = dtItemData;
                    ifrmAccount.dtAccountPaymentData = dtPayMentData;
                    if (dtItemData.Rows.Count > 0)
                    {
                        DataRow dr = dtItemData.NewRow();
                        dr["fpItemName"] = "合计";
                        dr["ItemFee"] = dtItemData.Compute("sum(ItemFee)", string.Empty);
                        dtItemData.Rows.Add(dr);
                    }

                    if (dtPayMentData.Rows.Count > 0)
                    {
                        DataRow row = dtPayMentData.NewRow();
                        row["paymentname"] = "合计";
                        row["paymentmoney"] = dtPayMentData.Compute("sum(paymentmoney)", string.Empty);
                        dtPayMentData.Rows.Add(row);
                    }

                    ifrmAccount.BindAccountData(dtInvoiceData, dtPayMentData, dtItemData);
                    decimal regFee = retdata.GetData<decimal>(3);//总挂号费用
                    decimal balanceFee = retdata.GetData<decimal>(4);//总收费费用
                    ifrmAccount.regFee = regFee.ToString("0.00");
                    ifrmAccount.balanceFee = balanceFee.ToString("0.00");
                    ifrmAccount.CurAccount = retdata.GetData<OP_Account>(5);
                    string totalFeeDx = CmycurD(ifrmAccount.CurAccount.TotalFee);
                    ifrmAccount.SetAccountDxTotalFee(totalFeeDx);
                }
                else
                {
                    Action<ClientRequestData> requestAction1 = ((ClientRequestData request1) =>
                    {
                        request1.AddData(ifrmAccount.CurAccount.AccountID);
                        request1.AddData(ifrmAccount.CurAccount.AccountFlag);
                    });
                    ServiceResponseData retdata1 = InvokeWcfService("OPProject.Service", "AccountController", "GetAccountDataME", requestAction1);
                    DataTable dtDetail = retdata1.GetData<DataTable>(0);
                    ifrmAccount.CurAccount = retdata1.GetData<OP_Account>(1);
                    ifrmAccount.dtAccountItemDataME = dtDetail;
                    ifrmAccount.BindAccountDataME(dtDetail);
                }
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
                return;
            }
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
            string str5 = string.Empty;   //人民币大写金额形式
            int i;    //循环变量
            int j;    //num的值乘以100的字符串长度
            string ch1 = string.Empty;     //数字的汉语读法
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
        /// 显示票据明细
        /// </summary>
        /// <param name="invoiceID">发票卷序号</param>
        /// <param name="invoiceType">票据类型</param>
        [WinformMethod]
        public void GetInvoiceDetail(int invoiceID, int invoiceType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(ifrmAccount.CurAccount.AccountID);
                request.AddData(invoiceID);
                request.AddData(invoiceType);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "AccountController", "GetInvoiceDetail", requestAction);
            DataTable dtInvoiceData = retdata.GetData<DataTable>(0);
            DataRow dr = dtInvoiceData.NewRow();
            dr[1] = "合计";
            for (int i = 5; i < dtInvoiceData.Columns.Count; i++)
            {
                dr[dtInvoiceData.Columns[i].ColumnName] = dtInvoiceData.Compute("sum(" + dtInvoiceData.Columns[i].ColumnName + ")", string.Empty);
            }

            dtInvoiceData.Rows.Add(dr);
            ifrmInvoiceDetail.BindInvoiceDt(dtInvoiceData);
            var dialog = iBaseView["FrmInvoiceDetail"] as Form;
            dialog.ShowDialog();
        }

        /// <summary>
        /// 提交缴款
        /// </summary>
        /// <returns>bool</returns>
        [WinformMethod]
        public bool SubmitAccount()
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(ifrmAccount.CurAccount);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "AccountController", "SubmitAccount", requestAction);
                MessageBoxShow("缴款成功", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                ifrmAccount.CurAccount = retdata.GetData<OP_Account>(0);
                return true;
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
                return false;
            }
        }

        /// <summary>
        /// 门诊缴款查询 查询缴款明细
        /// </summary>
        /// <param name="queryAccountId">缴款ID</param>
        [WinformMethod]
        public void ShowAccount(int queryAccountId)
        {
            ifrmAccountQuery = (IFrmAccount)iBaseView["FrmAccountQuery"];
            ifrmAccountQuery.QueryAccountId = queryAccountId;
            (ifrmAccountQuery as Form).ShowDialog();
        }

        /// <summary>
        /// 缴款单打印
        /// </summary>
        [WinformMethod]
        public void AccountPrint()
        {
            if (ifrmAccount.CurAccount == null || ifrmAccount.CurAccount.AccountID < 1)
            {
                return;
            }

            if (ifrmAccount.CurAccount.AccountFlag == 0)
            {
                MessageBoxShowError("未缴款记录不能打印");
                return;
            }

            if (ifrmAccount.CurAccount.AccountType.ToString().Contains("收费") || ifrmAccount.CurAccount.AccountType.ToString().Contains("0"))
            {
                Print(ifrmAccount.CurAccount, ifrmAccount.dtAccountInvoiceData, ifrmAccount.dtAccountItemData, ifrmAccount.dtAccountPaymentData);
            }
            else
            {
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                myDictionary.Add("Title", LoginUserInfo.WorkName + "会员充值缴款单");
                myDictionary.Add("ReceivBillNO", ifrmAccount.CurAccount.ReceivBillNO);
                myDictionary.Add("DateScope", ifrmAccount.CurAccount.LastDate.ToString("yyyy-MM-dd HH:mm:ss") + "至" + ifrmAccount.CurAccount.AccountDate.ToString("yyyy-MM-dd HH:mm:ss"));
                string invoiceScope = string.Empty;
                for (int i = 0; i < ifrmAccount.dtAccountItemDataME.Rows.Count; i++)
                {
                    invoiceScope += ifrmAccount.dtAccountItemDataME.Rows[i]["RechargeCode"] + "  ";
                }

                myDictionary.Add("InvoiceScope", invoiceScope);
                myDictionary.Add("AccountOperator", LoginUserInfo.EmpName);
                myDictionary.Add("TotalFee", ifrmAccount.CurAccount.TotalFee);
                myDictionary.Add("ShouldAccountFee", ifrmAccount.CurAccount.CashFee);
                EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 2010, 0, myDictionary, ifrmAccount.dtAccountItemDataME).Print(false);
            }
        }

        /// <summary>
        /// 缴款单打印
        /// </summary>
        /// <param name="curAccount">当前缴款对象</param>
        /// <param name="dtInvoiceData">票据信息</param>
        /// <param name="dtItemData">项目信息</param>
        /// <param name="dtPayMentData">支付信息</param>
        private void Print(OP_Account curAccount, DataTable dtInvoiceData, DataTable dtItemData, DataTable dtPayMentData)
        {
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            myDictionary.Add("Title", LoginUserInfo.WorkName + "门诊缴款单");
            myDictionary.Add("ReceivBillNO", curAccount.ReceivBillNO);
            myDictionary.Add("DateScope", curAccount.LastDate.ToString("yyyy-MM-dd HH:mm:ss") + "至" + curAccount.AccountDate.ToString("yyyy-MM-dd HH:mm:ss"));
            string invoiceScope = string.Empty;
            for (int i = 0; i < dtInvoiceData.Rows.Count; i++)
            {
                invoiceScope += dtInvoiceData.Rows[i]["InvoiceNOs"] + "  ";
            }

            myDictionary.Add("InvoiceScope", invoiceScope);
            myDictionary.Add("AccountOperator", LoginUserInfo.EmpName);
            myDictionary.Add("RoundingFee", curAccount.RoundingFee);
            myDictionary.Add("TotalFee", curAccount.TotalFee);
            myDictionary.Add("ShouldAccountFee", curAccount.CashFee);
            myDictionary.Add("DxFee", CmycurD(curAccount.CashFee));
            for (int i = 0; i < dtItemData.Rows.Count - 1; i++)
            {
                myDictionary.Add(dtItemData.Rows[i]["fpItemName"].ToString(), dtItemData.Rows[i]["ItemFee"]);
            }

            for (int i = 0; i < dtPayMentData.Rows.Count - 1; i++)
            {
                myDictionary.Add(dtPayMentData.Rows[i]["paymentname"].ToString(), dtPayMentData.Rows[i]["paymentmoney"]);
            }

            EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 2001, 0, myDictionary, null).Print(false);
        }
    }
}
