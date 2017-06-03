using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.Controller
{
    /// <summary>
    /// 费用清单控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmExpenseList")]
    [WinformView(Name = "FrmExpenseList", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmExpenseList")]
    public class ExpenseListController : WcfClientController
    {
        /// <summary>
        /// 费用清单接口
        /// </summary>
        private IExpenseList iExpenseList;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            iExpenseList = (IExpenseList)DefaultView;
        }

        /// <summary>
        /// 获取并绑定入院科室列表
        /// </summary>
        [WinformMethod]
        public void GetWardDept()
        {
            // 通过WCF调用服务端控制器取得住院临床科室列表
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "ExpenseListController", "GetWardDept");
            DataTable deptDt = retdata.GetData<DataTable>(0);
            // 病人列表界面
            if (deptDt != null)
            {
                iExpenseList.LoadDeptList(deptDt);
            }
        }

        /// <summary>
        /// 获取并绑定科室病人信息
        /// </summary>
        /// <param name="sDeptCode">科室编码</param>
        /// <param name="sDTInBegin">入院开始日期</param>
        /// <param name="sDTInEnd">入院结束日期</param>
        /// <param name="iPatientState">病人状态</param>
        /// <param name="sPatient">住院号</param>
        /// <param name="iJSId">结算ID</param>
        [WinformMethod]
        public void GetDeptPatientInfoList(string sDeptCode, string sDTInBegin, string sDTInEnd, int iPatientState, string sPatient, int iJSId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(sDeptCode);
                request.AddData(sDTInBegin);
                request.AddData(sDTInEnd);
                request.AddData(iPatientState);
                request.AddData(sPatient);
                request.AddData(iJSId);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "ExpenseListController", "GetDeptPatientInfoList", requestAction);
            DataTable dtDeptPatientInfoList = retdata.GetData<DataTable>(0);

            if (dtDeptPatientInfoList != null)
            {
                // 绑定执行科室列表
                iExpenseList.LoadDeptPatientInfoList(dtDeptPatientInfoList);
            }
        }

        /// <summary>
        /// 获取病人费用清单数据
        /// </summary>
        /// <param name="iPatientId">病人ID</param>
        /// <param name="iListType">清单类型  1-项目明细  2-一日清单  3-发票项目  4-项目汇总</param>
        /// <param name="sBDate">开始时间</param>
        /// <param name="sEDate">结束事件</param>
        /// <param name="iJsState">计算状态 0.未结算 1，中途，2，出院，3，欠费</param>
        /// <param name="iDateType">0.记账时间 1.费用时间</param>
        [WinformMethod]
        public void GetPatientFeeInfo(int iPatientId, int iListType, string sBDate, string sEDate, int iJsState, int iDateType)
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(WorkId);
                    request.AddData(iPatientId);
                    request.AddData(iListType);
                    request.AddData(sBDate);
                    request.AddData(sEDate);
                    request.AddData(iJsState);
                    request.AddData(iDateType);
                    request.AddData(-1);
                });
                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "ExpenseListController", "GetPatientFeeInfo", requestAction);
                DataTable dtPatientFeeSum = retdata.GetData<DataTable>(0);
                DataTable dtPatientFeeInfo = retdata.GetData<DataTable>(1);

                if (dtPatientFeeInfo != null)
                {
                    // 绑定执行科室列表
                    iExpenseList.LoadPatientFeeInfo(dtPatientFeeSum, dtPatientFeeInfo);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 打印所选病人一日清单
        /// </summary>
        /// <param name="dicList">病人列表</param>
        /// <param name="iListType">打印类型  定位日清单</param>
        /// <param name="iJsState">结算状态 定位-1 全部</param>
        /// <param name="iTimeType">打印类型 定位记账时间</param>
        [WinformMethod]
        public void PrintAllPatientData(List<Dictionary<string, object>> dicList, int iListType, int iJsState, int iTimeType)
        {
            string sBDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00");
            string sEDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:59");
            decimal totalFee = 0;
            int iCostHeadId = -1;

            foreach (Dictionary<string, object> dic in dicList)
            {
                totalFee = 0;

                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(WorkId);
                    request.AddData(dic.Keys.Contains("PatientID") ? Convert.ToUInt32(dic["PatientID"]) : 0);
                    request.AddData(iListType);
                    request.AddData(sBDate);
                    request.AddData(sEDate);
                    request.AddData(iJsState);
                    request.AddData(iTimeType);
                    request.AddData(iCostHeadId);
                });

                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "ExpenseListController", "GetPatientFeeInfo", requestAction);

                DataTable dtPatientFeeSum = retdata.GetData<DataTable>(0);
                DataTable dtPatientFeeInfo = retdata.GetData<DataTable>(1);

                if (dtPatientFeeSum != null && dtPatientFeeSum.Rows.Count > 0)
                {
                    dic["TotalDespoit"] = Convert.ToDecimal(dtPatientFeeSum.Rows[0]["TotalDespoit"]);
                    dic["TotalFee"] = Convert.ToDecimal(dtPatientFeeSum.Rows[0]["TotalFee"]);
                    dic["YE"] = Convert.ToDecimal(dtPatientFeeSum.Rows[0]["YE"]);
                    dic["BANumber"] = dtPatientFeeSum.Rows[0]["CaseNumber"].ToString();
                }

                if (dtPatientFeeInfo != null && dtPatientFeeInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtPatientFeeInfo.Rows)
                    {
                        totalFee += Convert.ToDecimal(dr["TotalFee"]);
                        string sInFpName = dr["InFpName"].ToString();

                        if (dic.Keys.Contains(sInFpName))
                        {
                            dic[sInFpName] = Convert.ToDecimal(dr["TotalFee"]);
                        }
                        else
                        {
                            dic.Add(sInFpName, Convert.ToDecimal(dr["TotalFee"]));
                        }
                    }

                    dic["CostDate"] = sBDate + " 到 " + sEDate;
                    dic["Fee"] = totalFee;
                    EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 3005, 0, dic, dtPatientFeeInfo).Print(true);
                }
            }
        }

        /// <summary>
        /// 出院结算所打印的明细清单
        /// </summary>
        /// <param name="iPatientId">病人登记ID</param>
        /// <param name="iCostHeadId">结算头ID</param>
        [WinformMethod]
        public void PrintPatientFeeInfo(int iPatientId, int iCostHeadId)
        {
            int iListType = 4;
            string sBDate = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            string sEDate = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            int iJsState = -1;
            int iDateType = 1;

            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(WorkId);
                    request.AddData(iPatientId);
                    request.AddData(iListType);
                    request.AddData(sBDate);
                    request.AddData(sEDate);
                    request.AddData(iJsState);
                    request.AddData(iDateType);
                    request.AddData(iCostHeadId);
                });

                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "ExpenseListController", "GetPatientFeeInfo", requestAction);
                DataTable dtPatientFeeSum = retdata.GetData<DataTable>(0);
                DataTable dtPatientFeeInfo = retdata.GetData<DataTable>(1);

                Dictionary<string, object> dic = new Dictionary<string, object>();

                if (dtPatientFeeSum != null && dtPatientFeeSum.Rows.Count > 0)
                {
                    int inDays = Convert.ToInt32(dtPatientFeeSum.Rows[0]["InDays"].ToString());

                    if (inDays == 0)
                    {
                        inDays = 1;
                    }

                    dic.Add("HospitalName", LoginUserInfo.WorkName);
                    dic.Add("Title", "费用清单-住院病人明细(出院)");
                    dic.Add("SerialNumber", dtPatientFeeSum.Rows[0]["SerialNumber"].ToString());
                    dic.Add("BedNo", dtPatientFeeSum.Rows[0]["BedNo"].ToString());
                    dic.Add("PatName", dtPatientFeeSum.Rows[0]["PatName"].ToString());
                    dic.Add("Sex", dtPatientFeeSum.Rows[0]["Sex"].ToString());
                    dic.Add("Age", dtPatientFeeSum.Rows[0]["Age"].ToString());
                    dic.Add("DeptName", dtPatientFeeSum.Rows[0]["DeptName"].ToString());
                    dic.Add("EnterHDate", dtPatientFeeSum.Rows[0]["EnterHDate"].ToString());
                    dic.Add("LeaveHDate", dtPatientFeeSum.Rows[0]["LeaveHDate"].ToString());
                    dic.Add("InDays", inDays);
                    dic.Add("EnterDiseaseName", dtPatientFeeSum.Rows[0]["EnterDiseaseName"].ToString());
                    dic.Add("TotalFee", Convert.ToDecimal(dtPatientFeeSum.Rows[0]["TotalFee"]));
                    dic["BANumber"] = dtPatientFeeSum.Rows[0]["CaseNumber"].ToString();
                    EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 3009, 0, dic, dtPatientFeeInfo).PrintPreview(false);
                }
                else
                {
                    MessageBoxShowError("数据有问题！");
                }
            }
            catch (Exception e)
            {
                MessageBoxShowError(e.Message);
            }
        }
    }
}
