using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.Controller
{
    /// <summary>
    /// 住院收费查询控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmDischargeRecall")]
    [WinformView(Name = "FrmDischargeRecall", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmDischargeRecall")]
    [WinformView(Name = "FrmInvoiceFill", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmInvoiceFill")]
    public class DischargeRecallController : WcfClientController
    {
        /// <summary>
        /// 住院收费查询接口
        /// </summary>
        IDischargeRecall mIDischargeRecall;

        /// <summary>
        /// 发票补打接口
        /// </summary>
        IInvoiceFill mIInvoiceFill;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            mIDischargeRecall = (IDischargeRecall)DefaultView;
            mIInvoiceFill = iBaseView["FrmInvoiceFill"] as IInvoiceFill;
        }

        /// <summary>
        /// 绑定收费员列表
        /// </summary>
        [WinformMethod]
        public void GetCashier()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DischargeRecallController", "GetCashier");
            DataTable cashierDt = retdata.GetData<DataTable>(0);
            mIDischargeRecall.Bind_CashierList(cashierDt);
        }

        /// <summary>
        /// 取得费用已结算费用列表
        /// </summary>
        [WinformMethod]
        public void GetCostFeeList()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIDischargeRecall.CostBeginDate);
                request.AddData(mIDischargeRecall.CostEndDate);
                request.AddData(mIDischargeRecall.SqlectParam);
                request.AddData(mIDischargeRecall.EmpId);
                request.AddData(mIDischargeRecall.Status);
                request.AddData(mIDischargeRecall.IsAccount);
                request.AddData(mIDischargeRecall.CostType);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DischargeRecallController", "GetCostFeeList", requestAction);
            DataTable tempDt = retdata.GetData<DataTable>(0);
            mIDischargeRecall.Bind_CostFeeList(tempDt);
        }

        /// <summary>
        /// 取消结算
        /// </summary>
        [WinformMethod]
        public void CancelSettlement()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIDischargeRecall.PatListID);
                request.AddData(mIDischargeRecall.CostHeadID);
                request.AddData(mIDischargeRecall.CancelCostType);
            });

            // 取得病人住院费用以及预交金列表
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DischargeRecallController", "CancelSettlement", requestAction);
            bool result = retdata.GetData<bool>(0);

            if (result)
            {
                MessageBoxShowSimple("取消结算成功！");
                GetCostFeeList();
            }
            else
            {
                // 选中的中途结算记录不是病人最后一次结算记录
                MessageBoxShowSimple("取消结算失败！当前选中记录，不是病人最近一次中途结算的记录，请重新选择！");
            }
        }

        /// <summary>
        /// 住院押金查询
        /// </summary>
        [WinformMethod]
        public void GetAllDepositList()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIDischargeRecall.CostBeginDate);
                request.AddData(mIDischargeRecall.CostEndDate);
                request.AddData(mIDischargeRecall.SqlectParam);
                request.AddData(mIDischargeRecall.EmpId);
                request.AddData(mIDischargeRecall.Status);
                request.AddData(mIDischargeRecall.IsAccount);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DischargeRecallController", "GetAllDepositList", requestAction);
            DataTable tempDt = retdata.GetData<DataTable>(0);
            mIDischargeRecall.Bind_DepositList(tempDt);
        }

        /// <summary>
        /// 打开发票补打界面
        /// </summary>
        [WinformMethod]
        public void ShowInvoiceFill()
        {
            ((Form)iBaseView["FrmInvoiceFill"]).ShowDialog();
        }

        /// <summary>
        /// 打印发票
        /// </summary>
        [WinformMethod]
        public void InvoiceFill()
        {
            mIDischargeRecall.PrintInvoiceInfo.NewInvoiceNumber = mIInvoiceFill.InvoiceNO;
            mIDischargeRecall.PrintInvoiceInfo.PrintEmpID = LoginUserInfo.EmpId;

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIDischargeRecall.PatListID);
                request.AddData(mIDischargeRecall.CostHeadID);
                request.AddData(mIDischargeRecall.PrintInvoiceInfo);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DischargeRecallController", "GetInvoiceFillData", requestAction);
            DataTable cost = retdata.GetData<DataTable>(0);
            DataTable costDetails = retdata.GetData<DataTable>(1);
            DataTable costInvoiceDt = SetCostInvoiceDt();
            DataRow costDr = costInvoiceDt.NewRow();
            costDr["InvoiceNo"] = mIInvoiceFill.InvoiceNO;
            costDr["PatDept"] = cost.Rows[0]["DeptName"];
            costDr["SerialNumber"] = cost.Rows[0]["CaseNumber"];
            costDr["CostYear"] = DateTime.Now.ToString("yyyy");
            costDr["CostMM"] = DateTime.Now.ToString("MM");
            costDr["CostDay"] = DateTime.Now.ToString("dd");
            costDr["PatName"] = cost.Rows[0]["PatName"];
            costDr["EnterHDate"] = cost.Rows[0]["EnterHDate"];
            DateTime enterHDate = Convert.ToDateTime(cost.Rows[0]["EnterHDate"]);
            DateTime leaveHDate = Convert.ToDateTime(cost.Rows[0]["LeaveHDate"]);

            if (Convert.ToInt32(cost.Rows[0]["CostType"]) == 1)
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
                costDr["LeaveHDate"] = Convert.ToDateTime(cost.Rows[0]["LeaveHDate"]).ToString("yyyy年MM月dd日");
                int days = new TimeSpan(leaveHDate.Ticks - enterHDate.Ticks).Days;

                if (days == 0)
                {
                    days = 1;
                }

                costDr["HospitalDays"] = days + 1;
            }

            //costDr["BedFee"] = costDetails.Compute("SUM(TotalFee)", "SubName='床位费'");
            //costDr["OperationCost"] = costDetails.Compute("SUM(TotalFee)", "SubName='手术费'");
            //costDr["WesternMedicineFee"] = costDetails.Compute("SUM(TotalFee)", "SubName='西药费'");
            //costDr["TheExamination"] = costDetails.Compute("SUM(TotalFee)", "SubName='诊查费'");
            //costDr["LaboratoryFee"] = costDetails.Compute("SUM(TotalFee)", "SubName='化验费'");
            //costDr["MediumCost"] = costDetails.Compute("SUM(TotalFee)", "SubName='中成药费'");
            //costDr["InspectionFee"] = costDetails.Compute("SUM(TotalFee)", "SubName='检查费'");
            //costDr["MaterialCost"] = costDetails.Compute("SUM(TotalFee)", "SubName='材料费'");
            //costDr["GrassFee"] = costDetails.Compute("SUM(TotalFee)", "SubName='中草药费'");
            //costDr["TreatmentCost"] = costDetails.Compute("SUM(TotalFee)", "SubName='治疗费'");
            //costDr["BloodTransfusion"] = costDetails.Compute("SUM(TotalFee)", "SubName='输血费'");
            //costDr["AnesthesiaCharge"] = costDetails.Compute("SUM(TotalFee)", "SubName='麻醉费'");
            //costDr["NursingCost"] = costDetails.Compute("SUM(TotalFee)", "SubName='护理费'");
            //costDr["OxygenFee"] = costDetails.Compute("SUM(TotalFee)", "SubName='输氧费'");
            //costDr["OtherFees"] = costDetails.Compute("SUM(TotalFee)", "SubName='其他费'");
            costDr["DepositFeeCapital"] = cost.Rows[0]["DeptositFee"];
            costDr["DepositFee"] = cost.Rows[0]["DeptositFee"];

            if (Convert.ToDecimal(cost.Rows[0]["BalanceFee"]) > 0)
            {
                costDr["Refundable"] = cost.Rows[0]["BalanceFee"];
            }
            else
            {
                if (Convert.ToInt32(cost.Rows[0]["CostType"]) == 3)
                {
                    costDr["Arrears"] = cost.Rows[0]["BalanceFee"];
                }
                else
                {
                    costDr["UpClose"] = cost.Rows[0]["BalanceFee"];
                }
            }

            costDr["PromFee"] = cost.Rows[0]["PromFee"];

            if (Convert.ToDecimal(cost.Rows[0]["PromFee"]) > 0)
            {
                costDr["TotalFeeCapital"] = Convert.ToDecimal(cost.Rows[0]["TotalFee"]) - Convert.ToDecimal(cost.Rows[0]["PromFee"]);
                costDr["TotalFee"] = Convert.ToDecimal(cost.Rows[0]["TotalFee"]) - Convert.ToDecimal(cost.Rows[0]["PromFee"]);
            }
            else
            {
                costDr["TotalFeeCapital"] = cost.Rows[0]["TotalFee"];
                costDr["TotalFee"] = cost.Rows[0]["TotalFee"];
            }

            costDr["EmpName"] = cost.Rows[0]["EmpName"];
            costInvoiceDt.Rows.Add(costDr);
            // 结算发票
            Dictionary<string, object> dic = new Dictionary<string, object>();

            if (costInvoiceDt.Rows.Count > 0)
            {
                if (costInvoiceDt.Rows.Count > 0)
                {
                    dic.Add("Head", LoginUserInfo.WorkName);//发票号 
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

                    dic.Add("西药", costDetails.Compute("SUM(TotalFee)", "SubName='西药'"));
                    dic.Add("中成药", costDetails.Compute("SUM(TotalFee)", "SubName='中成药'"));
                    dic.Add("中草药", costDetails.Compute("SUM(TotalFee)", "SubName='中草药'"));
                    dic.Add("化验", costDetails.Compute("SUM(TotalFee)", "SubName='化检'"));
                    dic.Add("治疗", costDetails.Compute("SUM(TotalFee)", "SubName='治疗'"));
                    dic.Add("床费", costDetails.Compute("SUM(TotalFee)", "SubName='床费'"));
                    dic.Add("检查", costDetails.Compute("SUM(TotalFee)", "SubName='检查'"));
                    dic.Add("材料费", costDetails.Compute("SUM(TotalFee)", "SubName='材料费'"));
                    dic.Add("护理费", costDetails.Compute("SUM(TotalFee)", "SubName='护理费'"));
                    dic.Add("心电", costDetails.Compute("SUM(TotalFee)", "SubName='心电'"));
                    dic.Add("B超", costDetails.Compute("SUM(TotalFee)", "SubName='B超'"));
                    dic.Add("会诊", costDetails.Compute("SUM(TotalFee)", "SubName='会诊'"));
                    dic.Add("其他", costDetails.Compute("SUM(TotalFee)", "SubName='其他'"));
                    dic.Add("医事服务费", costDetails.Compute("SUM(TotalFee)", "SubName='医事服务费'"));

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

            mIInvoiceFill.CloseForm();
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

        /// <summary>
        /// 打印报销单
        /// </summary>
        [WinformMethod]
        public void PrintPatientFeeInfo()
        {
            InvokeController(this._pluginName, "ExpenseListController", "PrintPatientFeeInfo", mIDischargeRecall.PatListID, mIDischargeRecall.CostHeadID);
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
