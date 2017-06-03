using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_MIInterface.Winform.IView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS_MIInterface.Interface;
using HIS_Entity.MIManage;
using System.Windows.Forms;

namespace HIS_MIInterface.Winform.Controller
{
    [WinformController(DefaultViewName = "FrmMITradeQuery")]//与系统菜单对应
    [WinformView(Name = "FrmMITradeQuery", DllName = "HIS_MIInterface.Winform.dll", ViewTypeName = "HIS_MIInterface.Winform.ViewForm.FrmMITradeQuery")]
    public class MITradeQueryController : WcfClientController
    {
        IFrmMITradeQuery iFrmMITradeQuery;
        private Dictionary<string, string> columnNames = new Dictionary<string, string>();
        public override void Init()
        {
            iFrmMITradeQuery = (IFrmMITradeQuery)iBaseView["FrmMITradeQuery"];
        }

        [WinformMethod]
        public void M_GetMIType()
        {
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMatchController", "M_GetMIType");
            DataTable dtMemberInfo = retdataMember.GetData<DataTable>(0);

            iFrmMITradeQuery.LoadMIType(dtMemberInfo);
        }

        [WinformMethod]
        public void Mz_GetTradeInfoSummary(int iMIID, int iPatientType, string sDeptCode, string sTimeStat, string sTimeStop)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iMIID);
                request.AddData(iPatientType);
                request.AddData(sDeptCode);
                request.AddData(sTimeStat);
                request.AddData(sTimeStop);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MITradeQueryController", "Mz_GetTradeInfoSummary", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            if (b)
            {
                DataTable dtMemberInfo = retdataMember.GetData<DataTable>(1);
                iFrmMITradeQuery.LoadTradeInfoSummary(dtMemberInfo);
            }
            else
            {
                MessageBoxShowError("无数据！");
            }

        }

        [WinformMethod]
        public void Mz_GetTradeRecordInfo(int iMIID, int iPatientType, string sDeptCode, string sTimeStat, string sTimeStop)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iMIID);
                request.AddData(iPatientType);
                request.AddData(sDeptCode);
                request.AddData(sTimeStat);
                request.AddData(sTimeStop);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MITradeQueryController", "Mz_GetTradeRecordInfo", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            if (b)
            {
                DataTable dtMemberInfo = retdataMember.GetData<DataTable>(1);
                iFrmMITradeQuery.LoadTradeRecordInfo(dtMemberInfo);
            }
        }

        [WinformMethod]
        public void Mz_GetTradeDetailInfo(int iMIID, int iPatientType, string sDeptCode, string sTimeStat, string sTimeStop)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iMIID);
                request.AddData(iPatientType);
                request.AddData(sDeptCode);
                request.AddData(sTimeStat);
                request.AddData(sTimeStop);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MITradeQueryController", "Mz_GetTradeDetailInfo", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            if (b)
            {
                DataTable dtMemberInfo = retdataMember.GetData<DataTable>(1);
                iFrmMITradeQuery.LoadTradeRecordInfo(dtMemberInfo);
            }
        }


        [WinformMethod]
        public void Mz_TradeInfoSummaryprint(Dictionary<string, object> myDictionary)
        {
            myDictionary["TiTle"] = LoginUserInfo.WorkName + "实时刷卡结算收费统计";
            myDictionary["StaffName"] = LoginUserInfo.EmpName;
            EfwControls.CustomControl.ReportTool.GetReport("医保收费统计.grf", 0, myDictionary, null).Print(true);
        }

        [WinformMethod]
        public void MZ_PrintInvoice(string tradeNo, string invoiceNo)
        {
            Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
            dicStr.Add(InputType.TradeNo, tradeNo);
            dicStr.Add(InputType.InvoiceNo, invoiceNo);
            InputClass input = new InputClass();
            input.SInput = dicStr;

            ResultClass resultClass = MIInterFaceFactory.MZ_PrintInvoice(input);
        }



        /// <summary>
        /// 导入审核结果
        /// </summary>
        /// <param name="ybId">医保类型</param>
        /// <param name="iTypeId">记录类型 0头表1明细</param>
        [WinformMethod]
        public void M_ImportMIRecord(int ybId, int iTypeId)
        {
            string sType = M_GetMedicalInsuranceData(ybId, "ImportMIRecord", "filetype");
            string sSeparator = M_GetMedicalInsuranceData(ybId, "ImportMIRecord", "separator");

            if (sType.Trim() == "1")
            {
                string sCName = "columnNamesHead";
                if (iTypeId == 0)
                {
                    sCName = "columnNamesHead";
                }
                else
                {
                    sCName = "columnNamesDetail";
                }

                string sColumnName = M_GetMedicalInsuranceData(ybId, "ImportMIRecord", sCName);

                OpenFileDialog open = new OpenFileDialog();
                open.Title = "请选择要导入的Excel文件";
                open.Filter = "xls files (*.xls)|*.xls";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    string[] sColumnNames = sColumnName.Split('|');
                    columnNames.Clear();
                    foreach (string s in sColumnNames)
                    {
                        columnNames.Add(s.Split(',')[0], s.Split(',')[1]);
                    }

                    string fileName = open.FileName;

                    DataTable dtMemberInfo = ExcelHelper.Import(fileName, columnNames);
                    if (dtMemberInfo.Columns.Contains("ItemType"))
                    {
                        foreach (DataRow dr in dtMemberInfo.Rows)
                        {
                            dr["ItemType"] = iTypeId;
                        }
                    }
                    iFrmMITradeQuery.LoadTradeRecordInfoMI(dtMemberInfo);
                }
            }
            else
            {
                string sCName = "columnNamesHead";
                if (iTypeId == 0)
                {
                    sCName = "columnNamesHead";
                }
                else
                {
                    sCName = "columnNamesDetail";
                }

                string sColumnName = M_GetMedicalInsuranceData(ybId, "ImportMIRecord", sCName);
                OpenFileDialog open = new OpenFileDialog();
                open.Title = "请选择要导入的Text|Out文件";
                open.Filter = "(*.out)|*.out|(*.txt)|*.txt";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    string[] sColumnNames = sColumnName.Split('|');
                    columnNames.Clear();
                    foreach (string s in sColumnNames)
                    {
                        columnNames.Add(s.Split(',')[0], s.Split(',')[1]);
                    }

                    string fileName = open.FileName;

                    DataTable dtMemberInfo = ExcelHelper.ImportText(fileName, columnNames);
                    //if (dtMemberInfo.Columns.Contains("ItemType"))
                    //{
                    //    foreach (DataRow dr in dtMemberInfo.Rows)
                    //    {
                    //        dr["ItemType"] = iTypeId;
                    //    }
                    //}
                    iFrmMITradeQuery.LoadTradeRecordInfoMI(dtMemberInfo);
                }
            }
        }


        /// <summary>
        /// 获取配置文件数据
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="method"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string M_GetMedicalInsuranceData(int typeId, string method, string name)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(typeId);
                request.AddData(method);
                request.AddData(name);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMatchController", "M_GetMedicalInsuranceData", requestAction);
            string sMemberInfo = retdataMember.GetData<string>(0);
            return sMemberInfo;
        }
    }
}
