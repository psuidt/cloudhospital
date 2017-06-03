using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using EfwControls.CustomControl;
using EfwControls.HISControl.UCPayMode;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.IPManage;
using HIS_Entity.MemberManage;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.Controller
{
    /// <summary>
    /// 出院结算控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmDischargeSettlement")]
    [WinformView(Name = "FrmDischargeSettlement", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmDischargeSettlement")]
    [WinformView(Name = "FrmPayMentInfo", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmPayMentInfo")]
    [WinformView(Name = "FrmAdjustInvoice", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmAdjustInvoice")]
    public class DischargeSettlementController : WcfClientController
    {
        /// <summary>
        /// 出院结算接口
        /// </summary>
        IDischargeSettlement mIDischargeSettlement;

        /// <summary>
        /// 费用结算接口
        /// </summary>
        IPayMentInfo mIPayMentInfo;

        /// <summary>
        /// 票号调整接口
        /// </summary>
        IAdjustInvoice mIAdjustInvoice;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            mIDischargeSettlement = (IDischargeSettlement)DefaultView;
            mIPayMentInfo = iBaseView["FrmPayMentInfo"] as IPayMentInfo;
            mIAdjustInvoice = iBaseView["FrmAdjustInvoice"] as IAdjustInvoice;
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
            mIDischargeSettlement.Bind_txtDeptList(dt);
        }

        /// <summary>
        /// 查询当前票据号，以及可用票据张数
        /// </summary>
        /// <param name="costSuccess">结算结果</param>
        [WinformMethod]
        public void GetInvoiceNO(bool costSuccess)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.EmpId);
            });

            // 获取票据号和可用票据张数
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DischargeSettlementController", "GetInvoiceNO", requestAction);
            int invoiceNum = retdata.GetData<int>(0);//可用票据张数

            if (invoiceNum == 0)
            {
                MessageBoxShowError("当前可用票据数为0，请先分配票据号");
                return;
            }

            string curInvoiceNo = retdata.GetData<string>(1);//当前票据号

            if (!costSuccess)
            {
                MessageBoxShowSimple("当前可用票据数为" + invoiceNum + "张，当前票据号为" + curInvoiceNo + string.Empty);
            }

            mIDischargeSettlement.InvoiceNo = curInvoiceNo;
        }

        /// <summary>
        /// 获取病人列表
        /// </summary>
        [WinformMethod]
        public void GetPatientList()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIDischargeSettlement.StartDateTime);
                request.AddData(mIDischargeSettlement.EndDateTime);
                request.AddData(mIDischargeSettlement.DeptID);
                request.AddData(0);
                request.AddData(mIDischargeSettlement.SeachParm);
                request.AddData(mIDischargeSettlement.PatStatus);
                request.AddData(false);
            });

            // 通过WCF调用服务端控制器取得住院病人列表
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetPatientList", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            mIDischargeSettlement.Binding_GrdPatList(dt);
        }

        /// <summary>
        /// 选中病人加载病人基本信息以及费用数据
        /// </summary>
        [WinformMethod]
        public void StatisticsFeeByFeeType()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIDischargeSettlement.PatListID);
                request.AddData(mIDischargeSettlement.SerialNumber);
            });

            // 取得病人住院费用以及预交金列表
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DischargeSettlementController", "StatisticsFeeByFeeType", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            DataTable payDt = retdata.GetData<DataTable>(1);
            decimal sumFee = 0;
            decimal sumPay = 0;

            // 住院总费用
            if (dt.Rows.Count > 0)
            {
                sumFee = Convert.ToDecimal(dt.Compute("SUM(TotalFee)", "true"));
            }

            // 预交金总额
            if (payDt.Rows.Count > 0)
            {
                sumPay = Convert.ToDecimal(payDt.Compute("SUM(TotalFee)", "true"));
            }

            mIDischargeSettlement.Bind_FeeTypeList(dt);
            mIDischargeSettlement.Bind_PrepaidPaymentList(payDt, sumFee, sumPay);
        }

        /// <summary>
        /// 打开结算界面
        /// </summary>
        [WinformMethod]
        public void ShowPayMentInfo()
        {
            // 获取病人最新预交金总额以及未结算费用总额
            GetPatDepositFee();

            if (mIDischargeSettlement.DepositFee != mIPayMentInfo.DepositFee ||
                mIDischargeSettlement.TotalFee != mIPayMentInfo.TotalFee)
            {
                MessageBoxShowSimple("当前病人的费用数据已发生改变，请刷新后重新结算！");
                return;
            }

            mIPayMentInfo.CostPatTypeID = mIDischargeSettlement.PatTypeID;  // 病人类型ID
            mIPayMentInfo.CurPatListID = mIDischargeSettlement.PatListID;  // 病人登记ID
            mIPayMentInfo.CostHead = new IP_CostHead();
            mIPayMentInfo.CostHead.MemberID = mIDischargeSettlement.MemberID;  // 会员ID
            mIPayMentInfo.CostHead.MemberAccountID = mIDischargeSettlement.MemberAccountID;  // 会员账号
            mIPayMentInfo.CostHead.CardNO = mIDischargeSettlement.CardNO;  // 会员卡号
            mIPayMentInfo.CostHead.PatListID = mIDischargeSettlement.PatListID; // 结算病人ID
            mIPayMentInfo.CostHead.PatName = mIDischargeSettlement.PatName; // 结算病人名
            mIPayMentInfo.CostHead.DeptID = mIDischargeSettlement.PatDeptID; // 结算病人入院科室ID
            mIPayMentInfo.CostHead.PatTypeID = mIDischargeSettlement.PatTypeID; // 结算病人类型ID
            mIPayMentInfo.CostHead.CostType = mIDischargeSettlement.CostType; // 结算类型
            mIPayMentInfo.CostHead.CostDate = DateTime.Now; // 结算日期
            mIPayMentInfo.CostHead.CostEmpID = LoginUserInfo.EmpId; // 结算人ID
            mIPayMentInfo.CostHead.InvoiceNO = mIDischargeSettlement.InvoiceNo; // 结算票据号
            mIPayMentInfo.CostHead.DeptositFee = mIPayMentInfo.DepositFee; // 结算预交金总额
            mIPayMentInfo.CostHead.TotalFee = mIPayMentInfo.TotalFee; // 结算费用总额
            mIPayMentInfo.CostHead.Status = 0; // 记录状态
            mIPayMentInfo.CostPayList = new List<IP_CostPayment>();
            mIPayMentInfo.CostFee = new CostFeeStyle();
            ((Form)iBaseView["FrmPayMentInfo"]).ShowDialog();
        }

        /// <summary>
        /// 获取病人最新预交金总额以及未结算费用总额
        /// </summary>
        private void GetPatDepositFee()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIDischargeSettlement.PatListID);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DischargeSettlementController", "GetPatDepositFee", requestAction);
            DataTable deeDt = retdata.GetData<DataTable>(0);

            if (deeDt != null)
            {
                // 最新预交金总额
                mIPayMentInfo.DepositFee = Convert.ToDecimal(deeDt.Rows[0][0]);
                // 最新住院费用总额
                mIPayMentInfo.TotalFee = Convert.ToDecimal(deeDt.Rows[1][0]);
            }
        }

        /// <summary>
        /// 优惠金额计算
        /// </summary>
        /// <returns>优惠金额</returns>
        [WinformMethod]
        public decimal PromFeeCaculate()
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(mIDischargeSettlement.PatTypeID);  // 病人登记ID
                    request.AddData(mIDischargeSettlement.MemberAccountID);  // 会员账号ID
                    request.AddData(mIPayMentInfo.TotalFee);   // 住院费用总金额
                    request.AddData(LoginUserInfo.EmpId);  // 操作员ID
                    request.AddData(mIDischargeSettlement.PatListID);  // 病人登记ID
                });

                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DischargeSettlementController", "PromFeeCaculate", requestAction);
                DiscountInfo resDiscountInfo = retdata.GetData<DiscountInfo>(0);
                mIDischargeSettlement.ResDiscountInfo = resDiscountInfo;
                return resDiscountInfo.DisAmount;
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
                return 0;
            }
        }

        /// <summary>
        /// 结算
        /// </summary>
        [WinformMethod]
        public void DischargeSetrlement()
        {
            //做成结算数据
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIPayMentInfo.CostHead);  // 结算记录主表数据
                request.AddData(mIPayMentInfo.CostPayList);  // 支付记录数据
                request.AddData(mIDischargeSettlement.ResDiscountInfo);  // 优惠明细数据
                request.AddData(LoginUserInfo.WorkId);  // 优惠明细数据
                request.AddData(mIDischargeSettlement.PatEnterHDate); // 病人入院时间
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DischargeSettlementController", "DischargeSettlement", requestAction);
            string result = retdata.GetData<string>(0);
            if (!string.IsNullOrEmpty(result))
            {
                MessageBoxShowSimple(result);
            }
            else
            {
                // 关闭结算界面
                mIPayMentInfo.CloseForm();
                mIPayMentInfo.CostPayList.Clear();
                mIPayMentInfo.CostPayList = retdata.GetData<List<IP_CostPayment>>(1);
                CostAfterSuccess(); // 结算成功后的处理
            }
        }

        /// <summary>
        /// 结算成功后的处理
        /// </summary>
        private void CostAfterSuccess()
        {
            MessageBox.Show("结算成功！");
            DataTable costInvoiceDt = SetCostInvoiceDt();
            DataRow costDr = costInvoiceDt.NewRow();
            costDr["InvoiceNo"] = mIDischargeSettlement.InvoiceNo;
            costDr["PatDept"] = mIDischargeSettlement.PatientDt.Rows[0]["DeptName"];
            costDr["SerialNumber"] = mIDischargeSettlement.PatientDt.Rows[0]["CaseNumber"];
            costDr["CostYear"] = DateTime.Now.ToString("yyyy");
            costDr["CostMM"] = DateTime.Now.ToString("MM");
            costDr["CostDay"] = DateTime.Now.ToString("dd");
            costDr["PatName"] = mIDischargeSettlement.PatientDt.Rows[0]["PatName"];
            costDr["EnterHDate"] = mIDischargeSettlement.PatientDt.Rows[0]["EnterHDate"];
            DateTime enterHDate = Convert.ToDateTime(mIDischargeSettlement.PatientDt.Rows[0]["EnterHDate"]);
            DateTime leaveHDate = Convert.ToDateTime(mIDischargeSettlement.PatientDt.Rows[0]["LeaveHDate"]);

            if (mIDischargeSettlement.CostType == 1)
            {
                int days = new TimeSpan(DateTime.Now.Ticks - enterHDate.Ticks).Days;

                if (days == 0)
                {
                    days = 1;
                }

                costDr["HospitalDays"] = days + 1;
            }
            else
            {
                costDr["LeaveHDate"] = costDr["LeaveHDate"] = Convert.ToDateTime(mIDischargeSettlement.PatientDt.Rows[0]["LeaveHDate"]).ToString("yyyy年MM月dd日");
                int days = new TimeSpan(leaveHDate.Ticks - enterHDate.Ticks).Days;

                if (days == 0)
                {
                    days = 1;
                }

                costDr["HospitalDays"] = days + 1;
            }

            //costDr["BedFee"] = mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='床位费'");
            //costDr["OperationCost"] = mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='手术费'");
            //costDr["WesternMedicineFee"] = mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='西药费'");
            //costDr["TheExamination"] = mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='诊查费'");
            //costDr["LaboratoryFee"] = mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='化验费'");
            //costDr["MediumCost"] = mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='中成药费'");
            //costDr["InspectionFee"] = mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='检查费'");
            //costDr["MaterialCost"] = mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='材料费'");
            //costDr["GrassFee"] = mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='中草药费'");
            //costDr["TreatmentCost"] = mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='治疗费'");
            //costDr["BloodTransfusion"] = mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='输血费'");
            //costDr["AnesthesiaCharge"] = mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='麻醉费'");
            //costDr["NursingCost"] = mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='护理费'");
            //costDr["OxygenFee"] = mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='输氧费'");
            //costDr["OtherFees"] = mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='其他费'");
            costDr["TotalFeeCapital"] = mIDischargeSettlement.TotalFee;
            costDr["TotalFee"] = mIDischargeSettlement.TotalFee;
            costDr["DepositFeeCapital"] = mIDischargeSettlement.DepositFee;
            costDr["DepositFee"] = mIDischargeSettlement.DepositFee;
            costDr["Refundable"] = mIPayMentInfo.CostFee.zyRefundFee;

            if (mIDischargeSettlement.CostType == 3)
            {
                costDr["Arrears"] = mIPayMentInfo.CostFee.zyChargeFee;
            }
            else
            {
                costDr["UpClose"] = mIPayMentInfo.CostFee.zyChargeFee;
            }

            costDr["PromFee"] = mIPayMentInfo.PromFee;
            costDr["EmpName"] = LoginUserInfo.EmpName;
            costInvoiceDt.Rows.Add(costDr);

            // 结算成功后刷新病人费用列表
            StatisticsFeeByFeeType();

            if (mIDischargeSettlement.CostType == 2 || mIDischargeSettlement.CostType == 3)
            {
                GetPatientList();
            }

            StringBuilder lastPatCostData = new StringBuilder();
            lastPatCostData.Append("上一病人： " + mIPayMentInfo.CostHead.PatName + "\n\n");
            lastPatCostData.Append("票 据 号： " + mIDischargeSettlement.InvoiceNo + "\n\n");
            lastPatCostData.Append("总 金 额： " + string.Format("{0:N}", mIPayMentInfo.CostFee.PayTotalFee) + "\n\n");

            foreach (IP_CostPayment payment in mIPayMentInfo.CostPayList)
            {
                lastPatCostData.Append(payment.PayName + "： " + string.Format("{0:N}", payment.CostMoney) + "\n\n");
            }

            lastPatCostData.Append("预 交 金：" + string.Format("{0:N}", mIPayMentInfo.CostFee.zyDepositFee) + "\n\n");
            decimal actPaycash = mIPayMentInfo.CostFee.ChangeFee + mIPayMentInfo.CostFee.CashFee;
            lastPatCostData.Append("实付现金： " + string.Format("{0:N}", actPaycash) + "\n\n");
            lastPatCostData.Append("找零金额： " + string.Format("{0:N}", mIPayMentInfo.CostFee.ChangeFee) + "\n\n");
            decimal balanceFee = 0;

            if (mIPayMentInfo.CostFee.zyChargeFee != 0 || mIPayMentInfo.CostFee.zyRefundFee != 0)
            {
                if (mIPayMentInfo.CostFee.zyChargeFee != 0)
                {
                    balanceFee = 0 - mIPayMentInfo.CostFee.zyChargeFee;
                }
                else if (mIPayMentInfo.CostFee.zyRefundFee != 0)
                {
                    balanceFee = mIPayMentInfo.CostFee.zyRefundFee;
                }
            }

            lastPatCostData.Append("预交金结余： " + string.Format("{0:N}", balanceFee) + "\n\n");
            lastPatCostData.Append("凑整金额： " + string.Format("{0:N}", mIPayMentInfo.CostFee.RoundFee) + "\n\n");
            mIDischargeSettlement.SetLastPatCostData(lastPatCostData.ToString());
            GetInvoiceNO(true);

            // 结算发票
            Dictionary<string, object> dic = new Dictionary<string, object>();

            if (costInvoiceDt.Rows.Count > 0)
            {
                if (costInvoiceDt.Rows.Count > 0)
                {
                    dic.Add("Head", LoginUserInfo.WorkName);// 机构名 
                    dic.Add("InvoiceNo", costInvoiceDt.Rows[0]["InvoiceNo"]);//发票号 
                    dic.Add("PatDept", costInvoiceDt.Rows[0]["PatDept"]);//科室 
                    dic.Add("SerialNumber", costInvoiceDt.Rows[0]["SerialNumber"]);//住院号 
                    dic.Add("CostYear", costInvoiceDt.Rows[0]["CostYear"]);//结算年 
                    dic.Add("CostMM", costInvoiceDt.Rows[0]["CostMM"]);//结算月 
                    dic.Add("CostDay", costInvoiceDt.Rows[0]["CostDay"]);//结算日 
                    dic.Add("PatName", costInvoiceDt.Rows[0]["PatName"]);//病人姓名 
                    dic.Add("EnterHDate", costInvoiceDt.Rows[0]["EnterHDate"]);//入院日期 
                    dic.Add("LeaveHDate", costInvoiceDt.Rows[0]["LeaveHDate"]);//出院日期 
                    dic.Add("HospitalDays", costInvoiceDt.Rows[0]["HospitalDays"]);//住院天数 

                    dic.Add("西药", mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='西药'"));
                    dic.Add("中成药", mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='中成药'"));
                    dic.Add("中草药", mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='中草药'"));
                    dic.Add("化验", mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='化检'"));
                    dic.Add("治疗", mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='治疗'"));
                    dic.Add("床费", mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='床费'"));
                    dic.Add("检查", mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='检查'"));
                    dic.Add("材料费", mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='材料费'"));
                    dic.Add("护理费", mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='护理费'"));
                    dic.Add("心电", mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='心电'"));
                    dic.Add("B超", mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='B超'"));
                    dic.Add("会诊", mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='会诊'"));
                    dic.Add("其他", mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='其他'"));
                    dic.Add("医事服务费", mIDischargeSettlement.PatFeeDt.Compute("SUM(TotalFee)", "SubName='医事服务费'"));

                    //dic.Add("BedFee", costInvoiceDt.Rows[0]["BedFee"]);//床位费 
                    //dic.Add("OperationCost", costInvoiceDt.Rows[0]["OperationCost"]);//手术费 
                    //dic.Add("WesternMedicineFee", costInvoiceDt.Rows[0]["WesternMedicineFee"]);//西药费 
                    //dic.Add("TheExamination", costInvoiceDt.Rows[0]["TheExamination"]);//诊查费 
                    //dic.Add("LaboratoryFee", costInvoiceDt.Rows[0]["LaboratoryFee"]);//化验费 
                    //dic.Add("MediumCost", costInvoiceDt.Rows[0]["MediumCost"]);//中成药 
                    //dic.Add("InspectionFee", costInvoiceDt.Rows[0]["InspectionFee"]);//检查费 
                    //dic.Add("MaterialCost", costInvoiceDt.Rows[0]["MaterialCost"]);//材料费 
                    //dic.Add("GrassFee", costInvoiceDt.Rows[0]["GrassFee"]);//中草药 
                    //dic.Add("TreatmentCost", costInvoiceDt.Rows[0]["TreatmentCost"]);//治疗费 
                    //dic.Add("BloodTransfusion", costInvoiceDt.Rows[0]["BloodTransfusion"]);//输血费 
                    //dic.Add("AnesthesiaCharge", costInvoiceDt.Rows[0]["AnesthesiaCharge"]);//麻醉费 
                    //dic.Add("NursingCost", costInvoiceDt.Rows[0]["NursingCost"]);//护理费 
                    //dic.Add("OxygenFee", costInvoiceDt.Rows[0]["OxygenFee"]);//输氧费 
                    //dic.Add("OtherFees", costInvoiceDt.Rows[0]["OtherFees"]);//其他 
                    dic.Add("TotalFeeCapital", costInvoiceDt.Rows[0]["TotalFeeCapital"]);//总费用金额大写 
                    dic.Add("TotalFee", costInvoiceDt.Rows[0]["TotalFee"]);//总费用金额 
                    dic.Add("DepositFeeCapital", costInvoiceDt.Rows[0]["DepositFeeCapital"]);//预交金金额大写 
                    dic.Add("DepositFee", costInvoiceDt.Rows[0]["DepositFee"]);//预交金金额 
                    dic.Add("UpClose", costInvoiceDt.Rows[0]["UpClose"]);//应收 
                    dic.Add("Refundable", costInvoiceDt.Rows[0]["Refundable"]);//应退 
                    dic.Add("Arrears", costInvoiceDt.Rows[0]["Arrears"]);//欠费 
                    dic.Add("PromFee", costInvoiceDt.Rows[0]["PromFee"]);//欠费 
                    dic.Add("EmpName", costInvoiceDt.Rows[0]["EmpName"]);//收费员 
                    ReportTool.GetReport(LoginUserInfo.WorkId, 3004, 0, dic, null).PrintPreview(true);
                }
            }
        }

        /// <summary>
        /// 弹出发票号调整界面
        /// </summary>
        [WinformMethod]
        public void ShowAdjustInvoice()
        {
            mIAdjustInvoice.InvoiceNo = mIDischargeSettlement.InvoiceNo;
            ((Form)iBaseView["FrmAdjustInvoice"]).ShowDialog();
        }

        /// <summary>
        /// 更换票据号
        /// </summary>
        /// <returns>true：调整成功/false：调整失败</returns>
        [WinformMethod]
        public bool AdjustInvoiceSet()
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(mIAdjustInvoice.NewPerfChar);
                    request.AddData(mIAdjustInvoice.NewInvoiceNo);
                    request.AddData(LoginUserInfo.EmpId);
                });

                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DischargeSettlementController", "AdjustInvoice", requestAction);
                string curInvoice = retdata.GetData<string>(0);
                mIDischargeSettlement.InvoiceNo = curInvoice;
                return true;
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
                return false;
            }
        }

        /// <summary>
        /// 做成住院发票DataTable
        /// </summary>
        /// <returns>住院发票信息</returns>
        public DataTable SetCostInvoiceDt()
        {
            DataTable costInvoiceDt = new DataTable();
            costInvoiceDt.Columns.Add("InvoiceNo", typeof(string));// 发票号 
            costInvoiceDt.Columns.Add("PatDept", typeof(string));// 科室 
            costInvoiceDt.Columns.Add("SerialNumber", typeof(string));// 住院号 
            costInvoiceDt.Columns.Add("CostYear", typeof(string));// 结算年 
            costInvoiceDt.Columns.Add("CostMM", typeof(string));// 结算月 
            costInvoiceDt.Columns.Add("CostDay", typeof(string));// 结算日 
            costInvoiceDt.Columns.Add("PatName", typeof(string));// 病人姓名 
            costInvoiceDt.Columns.Add("EnterHDate", typeof(DateTime));// 入院日期 
            costInvoiceDt.Columns.Add("LeaveHDate", typeof(DateTime));// 出院日期 
            costInvoiceDt.Columns.Add("HospitalDays", typeof(string));// 住院天数 
            costInvoiceDt.Columns.Add("BedFee", typeof(decimal));// 床位费 
            costInvoiceDt.Columns.Add("OperationCost", typeof(decimal));// 手术费 
            costInvoiceDt.Columns.Add("WesternMedicineFee", typeof(decimal));// 西药费 
            costInvoiceDt.Columns.Add("TheExamination", typeof(decimal));// 诊查费 
            costInvoiceDt.Columns.Add("LaboratoryFee", typeof(decimal));// 化验费 
            costInvoiceDt.Columns.Add("MediumCost", typeof(decimal));// 中成药 
            costInvoiceDt.Columns.Add("InspectionFee", typeof(decimal));// 检查费 
            costInvoiceDt.Columns.Add("MaterialCost", typeof(decimal));// 材料费 
            costInvoiceDt.Columns.Add("GrassFee", typeof(decimal));// 中草药 
            costInvoiceDt.Columns.Add("TreatmentCost", typeof(decimal));// 治疗费 
            costInvoiceDt.Columns.Add("BloodTransfusion", typeof(decimal));// 输血费 
            costInvoiceDt.Columns.Add("AnesthesiaCharge", typeof(decimal));// 麻醉费 
            costInvoiceDt.Columns.Add("NursingCost", typeof(decimal));// 护理费 
            costInvoiceDt.Columns.Add("OxygenFee", typeof(decimal));// 输氧费 
            costInvoiceDt.Columns.Add("OtherFees", typeof(decimal));// 其他 
            costInvoiceDt.Columns.Add("TotalFeeCapital", typeof(string));// 总费用金额大写 
            costInvoiceDt.Columns.Add("TotalFee", typeof(decimal));// 总费用金额 
            costInvoiceDt.Columns.Add("DepositFeeCapital", typeof(string));// 预交金金额大写 
            costInvoiceDt.Columns.Add("DepositFee", typeof(decimal));// 预交金金额 
            costInvoiceDt.Columns.Add("UpClose", typeof(decimal));// 应收 
            costInvoiceDt.Columns.Add("Refundable", typeof(decimal));// 应退 
            costInvoiceDt.Columns.Add("Arrears", typeof(decimal));// 欠费 
            costInvoiceDt.Columns.Add("PromFee", typeof(decimal));// 优惠金额 
            costInvoiceDt.Columns.Add("EmpName", typeof(string));// 收费员 
            return costInvoiceDt;
        }

        #region "共用方法"

        /// <summary>
        /// 提示消息
        /// </summary>
        /// <param name="msg">消息内容</param>
        [WinformMethod]
        public void MessageShow(string msg)
        {
            MessageBoxShowSimple(msg);
        }

        #endregion
    }
}
