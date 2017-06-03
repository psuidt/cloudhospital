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
    [WinformController(DefaultViewName = "FrmCostSearch")]//与系统菜单对应
    //查看明细
    [WinformView(Name = "FrmCostDetail", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmCostDetail")]
    //处方查询
    [WinformView(Name = "FrmRecipteQuery", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmRecipteQuery")]
    [WinformView(Name = "FrmOpCostSearch", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmOpCostSearch")]
    //发票补打输入补打票据号
    [WinformView(Name = "FrmInputPrintInvoice", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmInputPrintInvoice")]

    /// <summary>
    /// 处方查询，结算信息查询界面控件器
    /// </summary>
    public class QueryController:WcfClientController
    {
        /// <summary>
        /// 处方查询接口
        /// </summary>
        IFrmRecipteQuery ifrmRecipterQuery;

        /// <summary>
        /// 结算信息查询接口
        /// </summary>
        IFrmOpCostSearch ifrmOpCostSearch;

        /// <summary>
        /// 处方明细查询界面接口
        /// </summary>
        IFrmCostDetail ifrmCostDetail;

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {           
            ifrmRecipterQuery = (IFrmRecipteQuery)iBaseView["FrmRecipteQuery"];
            ifrmOpCostSearch = (IFrmOpCostSearch)iBaseView["FrmOpCostSearch"];
            ifrmCostDetail = (IFrmCostDetail)iBaseView["FrmCostDetail"];
        }
        #region 处方查询

        /// <summary>
        /// 处方查询
        /// </summary>
        [WinformMethod]
        public void RecipterInit()
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "QueryController", "RecipterInit");
            DataTable dtCharger = retdata.GetData<DataTable>(0);
            DataTable dtPatType = retdata.GetData<DataTable>(1);
            DataTable dtPayMent = retdata.GetData<DataTable>(2);
            ifrmRecipterQuery.BindCharger(dtCharger);
            ifrmRecipterQuery.BindPatType(dtPatType);            
        }

        /// <summary>
        /// 处方明细查看
        /// </summary>
        /// <param name="strFeeItemHeadIDs">费用头ID</param>
        [WinformMethod]
        public void QueryReciptDetail(string strFeeItemHeadIDs)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(strFeeItemHeadIDs);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "QueryController", "QueryReciptDetail", requestAction);
            DataTable dtData = retdata.GetData<DataTable>(0);
            DataView dv = dtData.DefaultView;
            dv.Sort = "feeItemHeadID Asc";
            DataTable dtDetail = dv.ToTable();
            DataRow dr = dtDetail.NewRow();
            dr["ItemName"] = "合 计";
            dr["TotalFee"] = dtDetail.Compute("sum(totalFee)", string.Empty);                   
            dtDetail.Rows.Add(dr);
            ifrmRecipterQuery.BindDetailData(dtDetail);           
        }

        /// <summary>
        /// 处方查询
        /// </summary>
        [WinformMethod]
        public void ReciptQuery()
        {
            try
            {
                Dictionary<string, object> queryDictionary = ifrmRecipterQuery.QueryDictionary;
                if (queryDictionary == null || queryDictionary.Count == 0)
                {
                    MessageBoxShowError("请至少输入一个查询条件");
                    return;
                }

                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(queryDictionary);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "QueryController", "ReciptQuery", requestAction);
                DataTable dtData = retdata.GetData<DataTable>(0);
                ifrmRecipterQuery.BindQueryData(dtData);
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
            }
        }
        #endregion

        #region 门诊病人费用查询
        /// <summary>
        /// 门诊病人费用数据查询
        /// </summary>
        [WinformMethod]
        public void OpCostSearchDataInit()
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "QueryController", "OpCostSearchDataInit");
            DataTable dtCharger = retdata.GetData<DataTable>(0);//收费员
            DataTable dtPatType = retdata.GetData<DataTable>(1);//病人类型
            DataTable dtDept = retdata.GetData<DataTable>(2);//科室
            DataTable dtDoctor = retdata.GetData<DataTable>(3);//医生
            ifrmOpCostSearch.BindCharge(dtCharger);
            ifrmOpCostSearch.BindPatType(dtPatType);
            ifrmOpCostSearch.BindDepts(dtDept);
            ifrmOpCostSearch.BindDoctors(dtDoctor);
        }

        /// <summary>
        /// 门诊病人费用查询
        /// </summary>
        [WinformMethod]
        public void OpCostSearchQuery()
        {
            Dictionary<string, object> queryDictionary = ifrmOpCostSearch.QueryDictionary;           
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(queryDictionary);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "QueryController", "OpCostSearchQuery", requestAction);
            DataTable dtPayMentData = retdata.GetData<DataTable>(0);
            for (int i = 0; i < dtPayMentData.Rows.Count-1; i++)
            {
                dtPayMentData.Rows[i]["Age"] = GetAge(Convert.ToDateTime(dtPayMentData.Rows[i]["BirthDay"]));
            }

            DataTable dtItemData =retdata.GetData<DataTable>(1);
            for (int i = 0; i < dtItemData.Rows.Count-1; i++)
            {
                dtItemData.Rows[i]["Age"] = GetAge(Convert.ToDateTime(dtItemData.Rows[i]["BirthDay"]));
            }

            ifrmOpCostSearch.BindQueryData(dtPayMentData, dtItemData);
        }

        /// <summary>
        /// 年龄转换
        /// </summary>
        /// <param name="dtBirthday">出生日期</param>
        /// <returns>返回龄</returns>
        private string GetAge(DateTime dtBirthday)
        {
            EfwControls.Common.AgeValue agevalue = EfwControls.Common.AgeExtend.GetAgeValue(dtBirthday);

            // 年龄的字符串表示    
            string strAge = string.Empty;                            
            // 格式化年龄输出
            if (agevalue.Y_num >= 1)                                           
            {
                // 年份输出
                strAge = agevalue.Y_num.ToString() + "岁";
            }

            if (agevalue.M_num > 0 && agevalue.Y_num < 1)                          
            {
                // 五岁以下可以输出月数
                strAge += agevalue.M_num.ToString() + "月";
            }

            // 一岁以下可以输出天数
            if (agevalue.D_num >= 0 && agevalue.Y_num < 1)                             
            {
                if (strAge.Length == 0 || agevalue.D_num > 0)
                {
                    strAge += agevalue.D_num.ToString() + "日";
                }
            }

            return strAge;
        }

        /// <summary>
        /// 查看费用明细
        /// </summary>
        /// <param name="costHeadId">结算ID</param>
        [WinformMethod]
        public void CostSearchDetail(int costHeadId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(costHeadId);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "QueryController", "CostSearchDetail", requestAction);
            DataTable dtDetail = retdata.GetData<DataTable>(0);
            if (dtDetail.Rows.Count == 0)
            {
                return;
            }

            DataRow dr = dtDetail.NewRow();
            dr["ItemName"] = "合 计";
            dr["TotalFee"] = dtDetail.Compute("sum(totalFee)", string.Empty);
            DataTable dtCopy = dtDetail.Clone();
            dtCopy.Clear();
            dtCopy.Rows.Add(dtDetail.Rows[0].ItemArray);
            for (int i = 1; i < dtDetail.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtDetail.Rows[i]["FeeItemHeadID"]) != Convert.ToInt32(dtDetail.Rows[i - 1]["FeeItemHeadID"]))
                {
                    DataRow row = dtCopy.NewRow();
                    row["ItemName"] = "小 计";
                    row["TotalFee"] = dtDetail.Compute("sum(totalFee)", "FeeItemHeadID=" + dtDetail.Rows[i - 1]["FeeItemHeadID"]);
                    dtCopy.Rows.Add(row);
                }

                dtCopy.Rows.Add(dtDetail.Rows[i].ItemArray);
                if (i == dtDetail.Rows.Count - 1)
                {
                    DataRow row = dtCopy.NewRow();
                    row["ItemName"] = "小 计";
                    row["TotalFee"] = dtDetail.Compute("sum(totalFee)", "FeeItemHeadID=" + dtDetail.Rows[i]["FeeItemHeadID"]);
                    dtCopy.Rows.Add(row);
                }
            }

            dtCopy.Rows.Add(dr.ItemArray);
            ifrmCostDetail.BindData(dtCopy);          
            var dialog = iBaseView["FrmCostDetail"] as Form;
            dialog.ShowDialog();            
        }
        #endregion
        #region 票据补打       
        /// <summary>
        /// 票据补打调用窗口
        /// </summary>
        [WinformMethod]
        public void PrintInvoiceInput()
        {
            var dialog = iBaseView["FrmInputPrintInvoice"] as Form;
            dialog.ShowDialog();                 
        }

        /// <summary>
        /// 费用清单补打
        /// </summary>
        /// <returns>是否补打成功</returns>
        [WinformMethod]
        public bool PrintPrescAgain()
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(ifrmOpCostSearch.PrintCostHeadId);                   
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "QueryController", "PrintPrescAgain", requestAction);
                DataTable dtInvoice = retdata.GetData<DataTable>(0);
                DataTable dtInvoiceDetail = retdata.GetData<DataTable>(1);
                DataTable dtInvoiceStatDetail = retdata.GetData<DataTable>(2);
                ChargeInfo chargeInfo = retdata.GetData<ChargeInfo>(3);
                BalancePrint print = new BalancePrint();               
                print.PresFeePrintDetail(dtInvoice, dtInvoiceDetail, dtInvoiceStatDetail, chargeInfo, LoginUserInfo.WorkName);             
                return true;
            }
            catch (Exception err)
            {
                MessageBoxShow(err.Message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
        }

        /// <summary>
        /// 票据重打
        /// </summary>
        /// <param name="perfchar">前缀</param>
        /// <param name="invoiceNO">票据号</param>
        /// <returns>返回是否成功</returns>
        [WinformMethod]
        public bool PrintInvoiceAgain(string perfchar, string invoiceNO)
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(ifrmOpCostSearch.PrintCostHeadId);
                    request.AddData(perfchar);
                    request.AddData(invoiceNO);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "QueryController", "PrintInvoiceAgain", requestAction);
                DataTable dtInvoice = retdata.GetData<DataTable>(0);
                DataTable dtInvoiceDetail = retdata.GetData<DataTable>(1);
                DataTable dtInvoiceStatDetail = retdata.GetData<DataTable>(2);
                ChargeInfo chargeInfo = retdata.GetData<ChargeInfo>(3);
                BalancePrint print = new BalancePrint();
                string newinvoiceNo = perfchar + invoiceNO;
                print.InvoicePrint(dtInvoice, dtInvoiceDetail, dtInvoiceStatDetail, newinvoiceNo, chargeInfo, LoginUserInfo.WorkName, LoginUserInfo.EmpName);
                Action<ClientRequestData> requestActionPrint = ((ClientRequestData request) =>
                {
                    request.AddData(ifrmOpCostSearch.PrintCostHeadId);
                    request.AddData(newinvoiceNo);
                    request.AddData(LoginUserInfo.EmpId);
                });
                InvokeWcfService("OPProject.Service", "QueryController", "PrintInvoiceAgainSave", requestActionPrint);
                return true;
            }
            catch (Exception err)
            {
                MessageBoxShow(err.Message, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
        }
        #endregion
    }
}
