using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.MIManage;
using HIS_MIInterface.Interface;
using HIS_MIInterface.Winform.IView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS_MIInterface.Winform.Controller
{
    [WinformController(DefaultViewName = "FrmCommitTradeState")]//与系统菜单对应
    [WinformView(Name = "FrmCommitTradeState", DllName = "HIS_MIInterface.Winform.dll", ViewTypeName = "HIS_MIInterface.Winform.ViewForm.FrmCommitTradeState")]

    public class CommitTradeStateController : WcfClientController
    {
        IFrmCommitTradeState iIFrmCommitTradeState;
        public override void Init()
        {
            iIFrmCommitTradeState = (IFrmCommitTradeState)iBaseView["FrmCommitTradeState"];
        }

        [WinformMethod]
        public void Mz_GetTradeInfoByCon(string sSerialNO, string sInvoiceNo, string sTradeNo)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(sSerialNO);
                request.AddData(sInvoiceNo);
                request.AddData(sTradeNo);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "CommitTradeStateController", "Mz_GetTradeInfoByCon", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            if (b)
            {
                DataTable dtMemberInfo = retdataMember.GetData<DataTable>(1);
                iIFrmCommitTradeState.LoadTradeInfo(dtMemberInfo);
            }
            else
            {
                MessageBoxShowError("无数据！");
            }

        }

        [WinformMethod]
        public void Mz_GetTradeInfoByCard(string sCardNo,DateTime dTime)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(sCardNo);
                request.AddData(dTime);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "CommitTradeStateController", "Mz_GetTradeInfoByCard", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            if (b)
            {
                DataTable dtMemberInfo = retdataMember.GetData<DataTable>(1);
                iIFrmCommitTradeState.LoadTradeInfo(dtMemberInfo);
            }
            else
            {
                MessageBoxShowError("无数据！");
            }

        }


        [WinformMethod]
        public void MZ_RePrintInvoice(string tradeNo, string invoiceNo)
        {
            Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
            dicStr.Add(InputType.TradeNo, tradeNo);
            dicStr.Add(InputType.InvoiceNo, invoiceNo);
            dicStr.Add(InputType.SerialNO, invoiceNo);
            InputClass input = new InputClass();
            input.SInput = dicStr;

            ResultClass resultClass = MIInterFaceFactory.MZ_PrintInvoice(input);
            if (!resultClass.bSucess)
            {
                MessageBoxShowError(resultClass.sRemarks);
            }
        }

        [WinformMethod]
        public void MZ_RePrintMXInvoice(string tradeNo, string invoiceNo)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(tradeNo);
                request.AddData(invoiceNo);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "CommitTradeStateController", "MZ_RePrintMXInvoice", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            if (b)
            {
                DataTable dtMedicalInsurancePayRecord = retdataMember.GetData<DataTable>(1);
                DataTable dtMIPayRecordHead = retdataMember.GetData<DataTable>(2);
                DataTable dtMIPayRecordDetailList = retdataMember.GetData<DataTable>(3);

                MZ_MITradeMXPrint(dtMedicalInsurancePayRecord, dtMIPayRecordHead, dtMIPayRecordDetailList);
            }
            else
            {
                MessageBoxShowError("无数据！");
            }
        }

        [WinformMethod]
        public void MZ_CommitTradeState(string tradeNo, string invoiceNo)
        {
            Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
            dicStr.Add(InputType.TradeNo, tradeNo);
            InputClass input = new InputClass();
            input.SInput = dicStr;

            ResultClass resultClass = MIInterFaceFactory.MZ_CommitTradeState(input);

            if (resultClass.bSucess)
            {
                if (resultClass.sRemarks.Equals("ok"))
                {
                    if (MessageBoxShowYesNo("该交易已完成是否打印？") == System.Windows.Forms.DialogResult.Yes)
                    {
                        MZ_RePrintInvoice(tradeNo, invoiceNo);
                    }
                    Mz_UpdateTradeRecord(tradeNo, true);
                }
                else //cancel
                {
                    MessageBoxShowError("个人账户扣减回退成功！,请重新结算！");
                    Mz_UpdateTradeRecord(tradeNo, false);
                }
            }
            else
            {
                MessageBoxShowError("查询出错！：" + resultClass.sRemarks);
            }
        }


        [WinformMethod]
        /// <summary>
        /// 调用读取卡片信息接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        public void Mz_GetCardInfo(string sCardNo)
        {
            Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
            dicStr.Add(InputType.CardNo, sCardNo);
            InputClass input = new InputClass();
            input.SInput = dicStr;

            ResultClass resultClass = MIInterFaceFactory.MZ_GetCardInfo(input);

            if (resultClass.bSucess)
            {
                DataTable dtMemberInfo = (DataTable)resultClass.oResult;
                iIFrmCommitTradeState.LoadCatalogInfo(dtMemberInfo);
            }
            else
            {
                MessageBoxShowError("异常！" + resultClass.sRemarks);
            }
        }

        [WinformMethod]
        /// <summary>
        /// 调用读取卡片信息接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        public void MZ_ExportJzxx(DateTime sDate)
        {
            List<DataTable> dtList = new List<DataTable>();
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(sDate);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "CommitTradeStateController", "MZ_ExportJzxx", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            if (b)
            {
                DataTable dt = retdataMember.GetData<DataTable>(1);
                dtList.Add(dt);
            }
            else
            {
                MessageBoxShowError("无数据！");
                return;
            }



            string sColumnName = "";
            try
            {
                string sType = M_GetMedicalInsuranceData(1, "ExportJZXX", "filetype");

                sColumnName = M_GetMedicalInsuranceData(1, "ExportJZXX", "ColumnNames");         

                string sSeparator = M_GetMedicalInsuranceData(1, "ExportJZXX", "separator");

                List<Dictionary<string, string>> columnNamesList = new List<Dictionary<string, string>>();

                string[] sColumnNames = sColumnName.Split('^');
                for (int i = 0; i < sColumnNames.Length; i++)
                {
                    Dictionary<string, string> dss = new Dictionary<string, string>();
                    string[] sColumnNamess = sColumnNames[i].Split('|');

                    foreach (string s in sColumnNamess)
                    {
                        dss.Add(s.Split(',')[0], s.Split(',')[1]);
                    }
                    columnNamesList.Add(dss);
                }

                ExportFile doExport = new ExportFile("jzxx", columnNamesList, sSeparator);
                ExportType _exportType = sType.Trim() == "1" ? ExportType.Excel : ExportType.Txt;
                if (doExport.InitShowDialog(_exportType))
                {
                    if (doExport.DoExportWork(dtList))
                        doExport.OpenFile();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("导出失败！:" + e.Message);
                return;
            }           
        }

        private void Mz_UpdateTradeRecord(string sTradeNo,bool bFlag)
        {
            Dictionary<InputType, object> dicStr = new Dictionary<InputType, object>();
            dicStr.Add(InputType.TradeNo, sTradeNo);
            dicStr.Add(InputType.bFlag, bFlag);
            InputClass input = new InputClass();
            input.SInput = dicStr;

            ResultClass resultClass = MIInterFaceFactory.Mz_UpdateTradeRecord(input);
        }

        /// <summary>
        /// 补打明细 补打明细 InputType.TradeNo InputType.InvoiceNo InputType.SerialNO
        /// </summary>
        /// <param name="dtMedicalInsurancePayRecord"></param>
        /// <param name="dtMIPayRecordHead"></param>
        /// <param name="dtMIPayRecordDetailList"></param>
        /// <returns></returns>
        private bool MZ_MITradeMXPrint(DataTable dtMedicalInsurancePayRecord, DataTable dtMIPayRecordHead, DataTable dtMIPayRecordDetailList)
        {
            MI_MedicalInsurancePayRecord medicalInsurancePayRecord = ConvertExtend.ToList<MI_MedicalInsurancePayRecord>(dtMedicalInsurancePayRecord)[0];
            MI_MIPayRecordHead mIPayRecordHead = ConvertExtend.ToList<MI_MIPayRecordHead>(dtMIPayRecordHead)[0];

            #region 参数字段
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            myDictionary.Add("HospitalName", LoginUserInfo.WorkName);
            myDictionary.Add("TradeNO", medicalInsurancePayRecord.TradeNO);
            myDictionary.Add("Id", medicalInsurancePayRecord.ID);
            myDictionary.Add("PatientName", medicalInsurancePayRecord.PatientName);
            myDictionary.Add("ApplyNO", medicalInsurancePayRecord.ApplyNO);
            myDictionary.Add("StaffName", LoginUserInfo.EmpName);
            myDictionary.Add("FeeMIIn", medicalInsurancePayRecord.FeeMIIn);
            myDictionary.Add("FeeFund", medicalInsurancePayRecord.FeeFund);
            myDictionary.Add("FeeCash", medicalInsurancePayRecord.FeeCash);
            myDictionary.Add("PersonCountPay", medicalInsurancePayRecord.PersonCountPay);
            myDictionary.Add("PersonCount", medicalInsurancePayRecord.PersonCount);
            myDictionary.Add("FeeBigPay", medicalInsurancePayRecord.FeeBigPay);
            myDictionary.Add("FeeAll", medicalInsurancePayRecord.FeeAll);

            myDictionary.Add("FeeMIOut", medicalInsurancePayRecord.FeeMIOut);
            myDictionary.Add("FeeDeductible", medicalInsurancePayRecord.FeeDeductible);
            myDictionary.Add("FeeSelfPay", medicalInsurancePayRecord.FeeSelfPay);
            myDictionary.Add("FeeBigSelfPay", medicalInsurancePayRecord.FeeBigSelfPay);
            myDictionary.Add("FeeOutOFPay", medicalInsurancePayRecord.FeeOutOFPay);
            myDictionary.Add("Feebcbx", medicalInsurancePayRecord.Feebcbx);
            myDictionary.Add("Feejcbz", medicalInsurancePayRecord.Feejcbz);
            #endregion

            #region 明细表
            DataTable dtPrint = dtMIPayRecordDetailList.Clone();
            foreach (DataColumn dc in dtMIPayRecordDetailList.Columns)
            {
                dtPrint.Columns.Add(dc.ColumnName + "1", dc.DataType);
            }

            DataTable dtCount = dtMIPayRecordDetailList.Clone();
            #region 添加汇总信息
            DataRow dr = dtCount.NewRow();
            dr["ItemName"] = "西药";
            dr["Fee"] = mIPayRecordHead.medicine;
            dtCount.Rows.Add(dr);

            DataRow dr1 = dtCount.NewRow();
            dr1["ItemName"] = "中成药";
            dr1["Fee"] = mIPayRecordHead.tmedicine;
            dtCount.Rows.Add(dr1);

            DataRow dr2 = dtCount.NewRow();
            dr2["ItemName"] = "中草药";
            dr2["Fee"] = mIPayRecordHead.therb;
            dtCount.Rows.Add(dr2);

            DataRow dr3 = dtCount.NewRow();
            dr3["ItemName"] = "化验";
            dr3["Fee"] = mIPayRecordHead.labexam;
            dtCount.Rows.Add(dr3);

            DataRow dr4 = dtCount.NewRow();
            dr4["ItemName"] = "检查";
            dr4["Fee"] = mIPayRecordHead.examine;
            dtCount.Rows.Add(dr4);

            DataRow dr5 = dtCount.NewRow();
            dr5["ItemName"] = "放射";
            dr5["Fee"] = mIPayRecordHead.xray;
            dtCount.Rows.Add(dr5);

            DataRow dr6 = dtCount.NewRow();
            dr6["ItemName"] = "B超";
            dr6["Fee"] = mIPayRecordHead.ultrasonic;
            dtCount.Rows.Add(dr6);

            DataRow dr7 = dtCount.NewRow();
            dr7["ItemName"] = "CT";
            dr7["Fee"] = mIPayRecordHead.CT;
            dtCount.Rows.Add(dr7);

            DataRow dr8 = dtCount.NewRow();
            dr8["ItemName"] = "核磁";
            dr8["Fee"] = mIPayRecordHead.mri;
            dtCount.Rows.Add(dr8);

            DataRow dr9 = dtCount.NewRow();
            dr9["ItemName"] = "治疗费";
            dr9["Fee"] = mIPayRecordHead.treatment;
            dtCount.Rows.Add(dr9);

            DataRow dr10 = dtCount.NewRow();
            dr10["ItemName"] = "材料费";
            dr10["Fee"] = mIPayRecordHead.material;
            dtCount.Rows.Add(dr10);

            DataRow dr11 = dtCount.NewRow();
            dr11["ItemName"] = "手术费";
            dr11["Fee"] = mIPayRecordHead.operation;
            dtCount.Rows.Add(dr11);

            DataRow dr12 = dtCount.NewRow();
            dr12["ItemName"] = "输氧费";
            dr12["Fee"] = mIPayRecordHead.oxygen;
            dtCount.Rows.Add(dr12);

            DataRow dr13 = dtCount.NewRow();
            dr13["ItemName"] = "输血费";
            dr13["Fee"] = mIPayRecordHead.bloodt;
            dtCount.Rows.Add(dr13);

            DataRow dr14 = dtCount.NewRow();
            dr14["ItemName"] = "正畸费";
            dr14["Fee"] = mIPayRecordHead.orthodontics;
            dtCount.Rows.Add(dr14);

            DataRow dr15 = dtCount.NewRow();
            dr15["ItemName"] = "镶牙费";
            dr15["Fee"] = mIPayRecordHead.prosthesis;
            dtCount.Rows.Add(dr15);


            DataRow dr16 = dtCount.NewRow();
            dr16["ItemName"] = "司法鉴定";
            dr16["Fee"] = mIPayRecordHead.forensic_expertise;
            dtCount.Rows.Add(dr16);


            DataRow dr17 = dtCount.NewRow();
            dr17["ItemName"] = "其他";
            dr17["Fee"] = mIPayRecordHead.other;
            dtCount.Rows.Add(dr17);
            #endregion
            DataRow[] drs = dtCount.Select(" Fee>0");
            for (int i = 0; i < dtMIPayRecordDetailList.Rows.Count; i++)
            {
                string yblevel = dtMIPayRecordDetailList.Rows[i]["YBItemLevel"].ToString().Trim();
                dtMIPayRecordDetailList.Rows[i]["YBItemLevel"] = yblevel.Equals("1") ? "◇" : (yblevel.Equals("2") ? "△" : yblevel.Equals("3") ? "□" : "□");
                //dtPrint.ImportRow(dtMIPayRecordDetailList.Rows[i]);
            }
            for (int k = 0; k < drs.Length; k++)
            {
                if (!Convert.ToBoolean(k % 2))  //偶数行
                {
                    dtPrint.ImportRow(drs[k]);
                }
                else
                {
                    int j = k / 2;
                    dtPrint.Rows[j]["ItemName1"] = drs[k]["ItemName"];
                    dtPrint.Rows[j]["Fee1"] = drs[k]["Fee"];
                }
            }

            int iRowCount = dtPrint.Rows.Count;
            for (int i = 0; i < dtMIPayRecordDetailList.Rows.Count; i++)
            {
                if (!Convert.ToBoolean(i % 2))  //偶数行
                {
                    dtPrint.ImportRow(dtMIPayRecordDetailList.Rows[i]);
                }
                else
                {
                    int j = i / 2 + iRowCount;
                    dtPrint.Rows[j]["ItemName1"] = dtMIPayRecordDetailList.Rows[i]["ItemName"];
                    dtPrint.Rows[j]["Spec1"] = dtMIPayRecordDetailList.Rows[i]["Spec"];
                    dtPrint.Rows[j]["Unit1"] = dtMIPayRecordDetailList.Rows[i]["Unit"];
                    dtPrint.Rows[j]["UnitPrice1"] = dtMIPayRecordDetailList.Rows[i]["UnitPrice"];
                    dtPrint.Rows[j]["Count1"] = dtMIPayRecordDetailList.Rows[i]["Count"];
                    dtPrint.Rows[j]["Fee1"] = dtMIPayRecordDetailList.Rows[i]["Fee"];
                    dtPrint.Rows[j]["YBItemLevel1"] = dtMIPayRecordDetailList.Rows[i]["YBItemLevel"];
                }
            }
            #endregion

            //EfwControls.CustomControl.ReportTool.GetReport(1, 2026, 0, myDictionary, dtPrint).Print(true);
            EfwControls.CustomControl.ReportTool.GetReport(1, 2007, 0, myDictionary, dtPrint).Print(false);
            return true;
        }

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
