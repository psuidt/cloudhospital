using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.Controller
{
    /// <summary>
    /// 预交金管理控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmPrepaidPaymentSys")]
    [WinformView(Name = "FrmPrepaidPaymentSys", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmPrepaidPaymentSys")]
    [WinformView(Name = "FrmPayADeposit", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmPayADeposit")]
    public class PrepaidPaymentSysController : WcfClientController
    {
        /// <summary>
        /// 预交金管理接口
        /// </summary>
        IPrepaidPaymentSys iprepaidPaymentSys;

        /// <summary>
        /// 预交金收取接口
        /// </summary>
        IPayADeposit ipayADeposit;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            iprepaidPaymentSys = (IPrepaidPaymentSys)DefaultView;
            ipayADeposit = iBaseView["FrmPayADeposit"] as IPayADeposit;
        }

        /// <summary>
        /// 取得所有科室列表
        /// </summary>
        [WinformMethod]
        public void GetDeptList()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(true);
            });

            // 通过WCF调用服务端控制器取得住院临床科室列表
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetDeptBasicData", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            iprepaidPaymentSys.Bind_txtDeptList(dt);
        }

        /// <summary>
        /// 获取病人列表
        /// </summary>
        [WinformMethod]
        public void GetPatientList()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iprepaidPaymentSys.StartDateTime);
                request.AddData(iprepaidPaymentSys.EndDateTime);
                request.AddData(iprepaidPaymentSys.DeptID);
                request.AddData(0);
                request.AddData(iprepaidPaymentSys.SeachParm);
                request.AddData(iprepaidPaymentSys.PatStatus);
                request.AddData(true);
            });

            // 通过WCF调用服务端控制器取得住院病人列表
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetPatientList", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            iprepaidPaymentSys.Binding_GrdPatList(dt);
        }

        /// <summary>
        /// 弹出预交金缴纳界面
        /// </summary>
        [WinformMethod]
        public void SavePatientInfo()
        {
            ipayADeposit.DepositList.MemberID = iprepaidPaymentSys.MemberID; //会员ID
            ipayADeposit.DepositList.PatListID = iprepaidPaymentSys.PatListID; //登记ID
            ipayADeposit.DepositList.DeptID = iprepaidPaymentSys.PatDeptID;   //科室ID
            ipayADeposit.DepositList.SerialNumber = iprepaidPaymentSys.SerialNumber; //住院流水号
            ((Form)iBaseView["FrmPayADeposit"]).ShowDialog();
            GetPaymentList(true);
        }

        /// <summary>
        /// 缴纳预交金
        /// </summary>
        [WinformMethod]
        public void PayADeposit()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                ipayADeposit.DepositList.MakerEmpID = LoginUserInfo.EmpId;  //收费人
                ipayADeposit.DepositList.MakerDate = DateTime.Now;  // 收费时间
                ipayADeposit.DepositList.Status = 0;
                ipayADeposit.DepositList.PrintTimes = 1;
                request.AddData(ipayADeposit.DepositList);
            });

            if (ipayADeposit.DepositList.TotalFee <= 0)
            {
                MessageBoxShowSimple("请输入正确的金额");
                return;
            }

            if (MessageBoxShowYesNo(string.Format("确定要该病人收费[{0}]吗？", ipayADeposit.DepositList.TotalFee)) == DialogResult.Yes)
            {
                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "PayADeposit", requestAction);
                string msg = retdata.GetData<string>(0);
                int depositID = retdata.GetData<int>(1);

                if (depositID <= 0)
                {
                    MessageBoxShowSimple(msg);
                }
                else
                {
                    Action<ClientRequestData> derequestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(depositID);
                    });

                    ServiceResponseData deretdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetPayADeposit", derequestAction);
                    DataTable dt = deretdata.GetData<DataTable>(0);
                    Dictionary<string, object> dic = new Dictionary<string, object>();

                    if (dt.Rows.Count > 0 && dt.Rows[0]["Status"].ToString() == "正常")
                    {
                        dt.Rows[0]["Head"] = LoginUserInfo.WorkName + "预交金缴款单";
                        string serialNumber = dt.Rows[0]["SerialNumber"].ToString();
                        string patName = dt.Rows[0]["PatName"].ToString();
                        dt.Rows[0]["SerialNumberName"] = patName + "(住院号" + serialNumber + ")";

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for (int j = 0; j < dt.Rows[i].ItemArray.Length; j++)
                            {
                                dic.Add(dt.Columns[j].ColumnName, dt.Rows[i][j]);
                            }

                            dic.Add("Year", Convert.ToDateTime(dt.Rows[i]["MakerDate"]).Year);
                            dic.Add("Month", Convert.ToDateTime(dt.Rows[i]["MakerDate"]).Month);
                            dic.Add("Day", Convert.ToDateTime(dt.Rows[i]["MakerDate"]).Day);
                            dic.Add("TotalFees", dt.Rows[i]["TotalFee"].ToString());
                        }

                        ReportTool.GetReport(LoginUserInfo.WorkId, 3204, 0, dic, null).PrintPreview(true);
                    }
                    else
                    {
                        MessageBoxEx.Show("已退费不能打印");
                    }

                    ipayADeposit.ClosePayADposit();
                }
            }
        }

        /// <summary>
        /// 打印当前选中记录
        /// </summary>
        /// <param name="depositID">预交金ID</param>
        [WinformMethod]
        public void PrintDepositInfo(int depositID)
        {
            Action<ClientRequestData> derequestAction = ((ClientRequestData request) =>
            {
                request.AddData(depositID);
            });

            ServiceResponseData deretdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetPayADeposit", derequestAction);
            DataTable dt = deretdata.GetData<DataTable>(0);
            Dictionary<string, object> dic = new Dictionary<string, object>();

            if (dt.Rows.Count > 0 && dt.Rows[0]["Status"].ToString() == "正常")
            {
                dt.Rows[0]["Head"] = LoginUserInfo.WorkName + "预交金缴款单";
                string serialNumber = dt.Rows[0]["SerialNumber"].ToString();
                string patName = dt.Rows[0]["PatName"].ToString();
                dt.Rows[0]["SerialNumberName"] = patName + "(住院号" + serialNumber + ")";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Rows[i].ItemArray.Length; j++)
                    {
                        dic.Add(dt.Columns[j].ColumnName, dt.Rows[i][j]);
                    }

                    dic.Add("Year", Convert.ToDateTime(dt.Rows[i]["MakerDate"]).Year);
                    dic.Add("Month", Convert.ToDateTime(dt.Rows[i]["MakerDate"]).Month);
                    dic.Add("Day", Convert.ToDateTime(dt.Rows[i]["MakerDate"]).Day);
                    dic.Add("TotalFees", dt.Rows[i]["TotalFee"].ToString());
                }

                ReportTool.GetReport(LoginUserInfo.WorkId, 3204, 0, dic, null).PrintPreview(true);
                InvokeWcfService("IPProject.Service", "AdmissionController", "UpdatePrintTime", derequestAction);
            }
        }

        /// <summary>
        /// 消息提示
        /// </summary>
        /// <param name="msg">消息内容</param>
        [WinformMethod]
        public void MessageShow(string msg)
        {
            MessageBoxShowSimple(msg);
        }

        /// <summary>
        /// 获取病人预交金缴纳记录列表
        /// </summary>
        /// <param name="isAdd">true：交费/false：退费</param>
        [WinformMethod]
        public void GetPaymentList(bool isAdd)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iprepaidPaymentSys.PatListID);
                request.AddData(iprepaidPaymentSys.SerialNumber);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "PrepaidPaymentSysController", "GetPaymentList", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            DataTable sumFeeDt = retdata.GetData<DataTable>(1);
            decimal depositFee = Convert.ToDecimal(sumFeeDt.Rows[0][0]);
            decimal sumFee = Convert.ToDecimal(sumFeeDt.Rows[1][0]);
            iprepaidPaymentSys.Binding_grdPayDetailList(dt, isAdd, depositFee, sumFee);
            iprepaidPaymentSys.SetGridColor();
        }

        /// <summary>
        /// 获取预交金支付方式
        /// </summary>
        [WinformMethod]
        public void GetPaymentMethod()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetPaymentMethod");
            DataTable patMethodDt = retdata.GetData<DataTable>(0);
            ipayADeposit.Binding_PaymentMethod(patMethodDt);
        }

        /// <summary>
        /// 获取预交金票据号
        /// </summary>
        [WinformMethod]
        public void GetInvoiceCurNO()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.EmpId);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetInvoiceCurNO", requestAction);
            string billNumber = retdata.GetData<string>(0);
            ipayADeposit.SetBillNumber(billNumber);
        }

        /// <summary>
        /// 预交金退费
        /// </summary>
        /// <param name="depositID">预交金ID</param>
        [WinformMethod]
        public void Refund(int depositID)
        {
            if (MessageBoxShowYesNo("确定要退费吗？") == DialogResult.Yes)
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(depositID);
                });

                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "PrepaidPaymentSysController", "Refund", requestAction);
                string msg = retdata.GetData<string>(0);

                if (!string.IsNullOrEmpty(msg))
                {
                    MessageBoxShowSimple(msg);
                    return;
                }

                GetPaymentList(false);
            }
        }
    }
}