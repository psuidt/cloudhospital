using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_DrugManage.Winform.ViewForm;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.Controller
{
    /// <summary>
    /// 住院统领发药控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmIPDisp")]//在菜单上显示
    [WinformView(Name = "FrmIPDisp", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmIPDisp")]//住院发药
    [WinformView(Name = "FrmIPDispFilter", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmIPDispFilter")]//过滤
    public class IPDispController : WcfClientController
    {
        /// <summary>
        /// 住院统领发药
        /// </summary>
        IFrmIPDisp frmIPDisp;

        /// <summary>
        /// 住院统领发药过滤
        /// </summary>
        IFrmIPDispFilter frmIPDispFilter;

        /// <summary>
        /// 当前操作的明细表
        /// </summary>
        private DataTable dtCurrentDetail;

        /// <summary>
        /// 当前选中科室-待发药
        /// </summary>
        private int selectedDeptID = 0;

        /// <summary>
        /// 当前选中科室-已发药
        /// </summary>
        private int selectedDispDeptID = 0;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public override void Init()
        {
            frmIPDisp = (IFrmIPDisp)iBaseView["FrmIPDisp"];
            frmIPDispFilter = (IFrmIPDispFilter)iBaseView["FrmIPDispFilter"];
        }

        /// <summary>
        /// 获取当前选中的药剂科室ID
        /// </summary>
        /// <param name="deptID">药剂科室ID</param>
        [WinformMethod]
        public void SetSelectedDept(int deptID)
        {
            selectedDeptID = deptID;
        }

        /// <summary>
        /// 获取当前选中的药剂科室ID
        /// </summary>
        /// <param name="deptID">药剂科室ID</param>
        [WinformMethod]
        public void SetDispSelectedDept(int deptID)
        {
            selectedDispDeptID = deptID;
        }

        /// <summary>
        /// 获取临床科室数据
        /// </summary>
        [WinformMethod]
        public void GetClinicalDeptData()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "IpDispController", "GetClinicalDeptData");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmIPDisp.BindClinicalDeptShowCard(dt);
        }

        /// <summary>
        /// 获取统领单类型
        /// </summary>
        [WinformMethod]
        public void GetIPDrugBillType()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "IpDispController", "GetIPDrugBillType");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmIPDisp.BindIPDrugBillTypeComboBox(dt);
        }

        /// <summary>
        /// 获取药品ShowCard
        /// </summary>
        [WinformMethod]
        public void GetDrugShowCard()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(selectedDeptID);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "IpDispController", "GetDrugShowCard", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmIPDispFilter.BindDrugShowCard(dt);
        }

        /// <summary>
        /// 取得统领单头表
        /// </summary>
        [WinformMethod]
        public void GetIPDrugBillHead()
        {
            Dictionary<string, string> condition = frmIPDisp.GetIPBillHeadCondition();
            condition.Add("a.ExecDeptID", selectedDeptID.ToString());
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(condition);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "IpDispController", "GetIPDrugBillHead", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmIPDisp.LoadBillTree(dt);
        }

        /// <summary>
        /// 取得已发药统领单头表
        /// </summary>
        [WinformMethod]
        public void GetDispIPBillHead()
        {
            Dictionary<string, string> condition = frmIPDisp.GetDispBillHeadCondition();
            condition.Add("a.ExecDeptID", selectedDispDeptID.ToString());
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(condition);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "IpDispController", "GetDispIPBillHead", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmIPDisp.LoadCompleteBillTree(dt);
        }

        /// <summary>
        /// 取得统领单明细表
        /// </summary>
        [WinformMethod]
        public void GetIPDrugBillDetail()
        {
            Dictionary<string, string> condition = frmIPDisp.GetIPBillDetailCondition();
            condition.Add("k.ExecDeptID", selectedDeptID.ToString());
            condition.Add("a.DispDrugFlag", "0");
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(condition);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "IpDispController", "GetIPDrugBillDetail", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            dtCurrentDetail = dt;
            frmIPDisp.SetSendBillDetail(dt);

            //绑定一个空表，初始化表格列
            frmIPDisp.BindBillDetailGrid(dt);
        }

        /// <summary>
        /// 根据表头ID取得统领单明细表
        /// </summary>
        /// <param name="billHeadId">表头ID</param>
        [WinformMethod]
        public void GetIPDrugBillDetailByHeadId(string billHeadId)
        {
            Dictionary<string, string> condition = frmIPDisp.GetIPBillDetailCondition();
            condition.Add("k.ExecDeptID", selectedDeptID.ToString());
            condition.Add("a.DispDrugFlag", "0");
            condition.Add("k.BillHeadID", billHeadId);
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(condition);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "IpDispController", "GetIPDrugBillDetail", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmIPDisp.BindBillDetailByHeadIdGrid(dt);
        }

        /// <summary>
        /// 获取病人费用数据
        /// </summary>
        /// <param name="patListId">病人ID</param>
        [WinformMethod]
        public void GetPatientFeeInfo(int patListId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patListId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetPatientFeeInfo", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmIPDisp.dtFee= dt;
        }
        /// <summary>
        /// 缺药
        /// </summary>
        /// <param name="billDetailId">统领明细ID</param>
        /// <returns>true成功false失败</returns>
        [WinformMethod]
        public bool ShortageDrug(string billDetailId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(billDetailId);
                request.AddData(LoginUserInfo.WorkId);
                request.AddData(LoginUserInfo.EmpId);
                request.AddData(LoginUserInfo.DeptId);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "IpDispController", "ShortageDrug", requestAction);
            bool bRtn = retdata.GetData<bool>(0);
            return bRtn;
        }

        /// <summary>
        /// 取得已发药统领单明细表
        /// </summary>
        [WinformMethod]
        public void GetDispDrugBillDetail()
        {
            Dictionary<string, string> condition = frmIPDisp.GetDispBillDetailCondition();
            condition.Add("k.ExecDeptID", selectedDispDeptID.ToString());
            condition.Add("a.DispDrugFlag", "1");
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(condition);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "IpDispController", "GetIPDrugBillDetail", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 取得已发药统领单明细表
        /// </summary>
        /// <param name="code">过滤字段</param>
        /// <param name="level">0一级节点1二级节点</param>
        [WinformMethod]
        public void GetDispDrugBillDetailByTreeNode(string code, int level)
        {
            Dictionary<string, string> condition = frmIPDisp.GetDispBillDetailCondition();
            condition.Add("k.ExecDeptID", selectedDispDeptID.ToString());
            condition.Add("a.DispDrugFlag", "1");
            if (level == 0)
            {
                condition.Add("k.PresDeptID", code);
            }
            else
            {
                condition.Add("a.DispHeadID", code);
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(condition);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "IpDispController", "GetIPDrugBillDetail", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmIPDisp.BindDispDetalByTreeNodeGrid(dt);
        }

        /// <summary>
        /// 取得当前待发药明细
        /// </summary>
        /// <returns>当前待发药明细数据集</returns>
        [WinformMethod]
        public DataTable GetCurrentDetail()
        {
            return dtCurrentDetail;
        }

        /// <summary>
        /// 取得药库数据
        /// </summary>
        [WinformMethod]
        public void GetDrugStoreData()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "IpDispController", "GetDrugStoreData");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmIPDisp.BindStoreRoomCombobox(dt);
        }

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="dtDetail">明细</param>
        [WinformMethod]
        public void Filter(DataTable dtDetail)
        {
            DialogResult dialogResult;
            var dialog = iBaseView["FrmIPDispFilter"] as FrmIPDispFilter;
            dialog.CurrentDetail = dtDetail;
            if (dialog == null)
            {
                dialogResult = DialogResult.None;
            }

            dialogResult = dialog.ShowDialog();
            if (dialog.Result == 1)
            {
                frmIPDisp.HandleFilterDetail(dialog.SelectedDetail);
            }
        }

        /// <summary>
        /// 确认后过滤结果
        /// </summary>
        /// <param name="result"></param>
        /// <param name="selectedDetail"></param>
        [WinformMethod]
        public void DispFilter(int result ,DataTable selectedDetail)
        {
            if (result == 1)
            {
                frmIPDisp.HandleFilterDetail(selectedDetail);
            }
        }

        /// <summary>
        /// 住院发药
        /// </summary>
        /// <param name="dtDetail">明细</param>
        /// <param name="deptId">药房ID</param>
        [WinformMethod]
        public void IPDisp(DataTable dtDetail, int deptId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(dtDetail);
                request.AddData(GetUserInfo().EmpId);
                request.AddData(GetUserInfo().EmpName);
                request.AddData(deptId);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "IpDispController", "IPDisp", requestAction);
            DGBillResult result = retdata.GetData<DGBillResult>(0);
            if (result.Result == 0)
            {
                MessageBoxShowSimple("发药成功...");

                //打印领药单
                PrintIPDispBill(result.DispHeadID, true);
            }
            else
            {
                string rtnMsg = result.ErrMsg;
                string message = string.Empty;
                if (result.LstNotEnough == null)
                {
                    MessageBoxShowSimple("发药失败:\r\n" + rtnMsg);
                }
                else
                {
                    foreach (DGNotEnough m in result.LstNotEnough)
                    {
                        message = message + m.DrugInfo + "\r\n";
                    }

                    MessageBoxShowSimple("发药失败，以下药品库存不足:\r\n" + message);
                }
            }
        }

        /// <summary>
        /// 打印统领单
        /// </summary>
        /// <param name="dtHead">统领单头</param>
        /// <param name="dtDetail">明细</param>
        /// <param name="deptName">部门名称</param>
        [WinformMethod]
        public void PrintTLBill(DataTable dtHead, DataTable dtDetail, string deptName)
        {
            try
            {
                if (dtHead.Rows.Count > 0)
                {
                    Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                    myDictionary.Add("DeptName", dtDetail.Rows[0]["PresDeptName"].ToString());
                    myDictionary.Add("UserName", dtDetail.Rows[0]["MakeEmpName"].ToString());
                    myDictionary.Add("ExeTime", DateTime.Now.ToString("yyyy-MM-dd"));
                    myDictionary.Add("DispDept", deptName);
                    myDictionary.Add("PrintTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    myDictionary.Add("HospitalName", LoginUserInfo.WorkName + "(" + dtDetail.Rows[0]["BillTypeName"].ToString() + ")统领发药单");
                    myDictionary.Add("Printer", LoginUserInfo.EmpName);
                    myDictionary.Add("PatientNames", string.Empty);
                    EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 4009, 0, myDictionary, dtHead).PrintPreview(true);
                }
            }
            catch (Exception error)
            {
                MessageBoxShowSimple(error.Message);
            }
        }

        /// <summary>
        /// 打印领药单
        /// </summary>
        /// <param name="iDispHeadID">统领单头Id</param>
        /// <param name="isNew">是否是新的</param>
        [WinformMethod]
        public void PrintIPDispBill(int iDispHeadID, bool isNew)
        {
            try
            {
                //0.获取打印数据
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(iDispHeadID);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "IpDispController", "GetIPDrugBillDetailPrint", requestAction);
                DataTable dtDetail = retdata.GetData<DataTable>(0);
                DataTable dtTotalOrder = new DataTable();
                if (dtDetail != null && dtDetail.Rows.Count > 0)
                {
                    //1.解析打印数据
                    Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                    myDictionary.Add("Title", LoginUserInfo.WorkName + "(" + dtDetail.Rows[0]["BillTypeName"].ToString() + ")统领发药单");
                    myDictionary.Add("BillNO", dtDetail.Rows[0]["BillNO"].ToString());
                    myDictionary.Add("Printer", LoginUserInfo.EmpName);
                    myDictionary.Add("ExecDeptID", dtDetail.Rows[0]["ExecDeptName"].ToString());
                    myDictionary.Add("MakeEmpName", dtDetail.Rows[0]["MakeEmpName"].ToString());
                    if (isNew)
                    {
                        myDictionary.Add("IsNew", string.Empty);
                    }
                    else
                    {
                        myDictionary.Add("IsNew", "重打");
                    }

                    int iReportId = Convert.ToInt32(dtDetail.Rows[0]["ReportId"].ToString());
                    dtTotalOrder = GetTotalBill(dtDetail, iReportId);
                    EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, iReportId, 0, myDictionary, dtTotalOrder).PrintPreview(true);
                }
            }
            catch (Exception error)
            {
                MessageBoxShowSimple(error.Message);
            }
        }

        /// <summary>
        ///  汇总Bill
        /// </summary>
        /// <param name="dtDetail">明细数据集</param>
        /// <param name="iReportId">报表ID</param>
        /// <returns>返回数据集</returns>
        private DataTable GetTotalBill(DataTable dtDetail, int iReportId)
        {
            DataTable totalOrder = new DataTable();
            totalOrder.Columns.Add("PatName", Type.GetType("System.String"));
            totalOrder.Columns.Add("BedNO", Type.GetType("System.String"));
            totalOrder.Columns.Add("Age", Type.GetType("System.String"));
            totalOrder.Columns.Add("DrugID", Type.GetType("System.Int32"));
            totalOrder.Columns.Add("ChemName", Type.GetType("System.String"));
            totalOrder.Columns.Add("DrugSpec", Type.GetType("System.String"));
            totalOrder.Columns.Add("Dosage", Type.GetType("System.String"));
            totalOrder.Columns.Add("Frequency", Type.GetType("System.String"));
            totalOrder.Columns.Add("DispAmount", Type.GetType("System.Decimal"));
            totalOrder.Columns.Add("ChannelName", Type.GetType("System.String"));
            totalOrder.Columns.Add("UnitName", Type.GetType("System.String"));
            totalOrder.Columns.Add("SellPrice", Type.GetType("System.Decimal"));
            totalOrder.Columns.Add("SellFee", Type.GetType("System.Decimal"));

            switch (iReportId)
            {
                #region 口服药统领单 出院带药单 处方领药单 中草药单
                case (int)ReportType.口服药统领单:    //  按人按药按计量按频次按用法汇总给
                case (int)ReportType.出院带药单:
                case (int)ReportType.处方领药单:
                case (int)ReportType.中草药单:
                    var result = from r in dtDetail.AsEnumerable()
                                 group r by new
                                 {
                                     PatName = r["PatName"].ToString(),
                                     BedNO = r["BedNO"].ToString(),
                                     Age = r["Age"].ToString(),
                                     DrugID = Convert.ToInt32(r["DrugID"]),
                                     ChemName = r["ChemName"].ToString(),
                                     Spec = r["DrugSpec"].ToString(),
                                     Dosage = r["Dosage"].ToString(),
                                     Frequency = r["Frequency"].ToString(),
                                     UnitName = r["UnitName"].ToString(),
                                     ChannelName = r["ChannelName"].ToString(),
                                     RetailPrice = Convert.ToDecimal(r["SellPrice"]),
                                     SellFee = Convert.ToDecimal(r["SellFee"])
                                 } 
                                 into g
                                 select new
                                 {
                                     PatName = g.Key.PatName,
                                     BedNO = g.Key.BedNO,
                                     Age = g.Key.Age,
                                     DrugID = g.Key.DrugID,
                                     ChemName = g.Key.ChemName,
                                     Spec = g.Key.Spec,
                                     Dosage = g.Key.Dosage,
                                     Frequency = g.Key.Frequency,
                                     UnitName = g.Key.UnitName,
                                     ChannelName = g.Key.ChannelName,
                                     RetailPrice = g.Key.RetailPrice,
                                     SellFee = g.Key.SellFee,
                                     DispAmount = g.Sum(r => Convert.ToDecimal(r["DispAmount"])),

                                     //RetailFee = g.Sum(r => Convert.ToDecimal(r["SellFee"]))
                                 };

                    DataRow dr;
                    foreach (var re in result)
                    {
                        dr = totalOrder.NewRow();
                        dr["PatName"] = re.PatName;
                        dr["BedNO"] = re.BedNO;
                        dr["Age"] = AgeConvert(re.Age);
                        dr["DrugID"] = re.DrugID;
                        dr["ChemName"] = re.ChemName;
                        dr["DrugSpec"] = re.Spec;
                        dr["Dosage"] = re.Dosage;
                        dr["Frequency"] = re.Frequency;
                        dr["DispAmount"] = re.DispAmount;
                        dr["UnitName"] = re.UnitName;
                        dr["ChannelName"] = re.ChannelName;
                        dr["SellPrice"] = re.RetailPrice;
                        dr["SellFee"] = re.SellFee;
                        totalOrder.Rows.Add(dr);
                    }

                    break;
                #endregion
                #region 大输液领药单 针剂领药单
                case (int)ReportType.大输液领药单:
                case (int)ReportType.针剂领药单:
                    var result2 = from r in dtDetail.AsEnumerable()
                                  group r by new
                                  {
                                      DrugID = Convert.ToInt32(r["DrugID"]),
                                      ChemName = r["ChemName"].ToString(),
                                      Spec = r["DrugSpec"].ToString(),
                                      UnitName = r["UnitName"].ToString(),
                                      RetailPrice = Convert.ToDecimal(r["SellPrice"]),
                                      SellFee = Convert.ToDecimal(r["SellFee"])
                                  } 
                                  into g
                                  select new
                                  {
                                      DrugID = g.Key.DrugID,
                                      ChemName = g.Key.ChemName,
                                      Spec = g.Key.Spec,
                                      UnitName = g.Key.UnitName,
                                      RetailPrice = g.Key.RetailPrice,
                                      DispAmount = g.Sum(r => Convert.ToDecimal(r["DispAmount"])),
                                      SellFee = g.Key.SellFee
                                  };

                    DataRow dr2;
                    foreach (var re in result2)
                    {
                        dr2 = totalOrder.NewRow();
                        dr2["DrugID"] = re.DrugID;
                        dr2["ChemName"] = re.ChemName;
                        dr2["DrugSpec"] = re.Spec;
                        dr2["DispAmount"] = re.DispAmount;
                        dr2["UnitName"] = re.UnitName;
                        dr2["SellPrice"] = re.RetailPrice;
                        dr2["SellFee"] = re.SellFee;
                        totalOrder.Rows.Add(dr2);
                    }

                    break;
                    #endregion
            }

            return totalOrder;
        }

        /// <summary>
        /// 打印报表编号枚举
        /// </summary>
        public enum ReportType
        {
            口服药统领单 = 4020,
            针剂领药单 = 4021,
            大输液领药单 = 4022,
            处方领药单 = 4023,
            出院带药单 = 4024,
            中草药单 = 4025
        }

        /// <summary>
        /// 年龄字符串处理函数
        /// </summary>
        /// <param name="sAge">年龄</param>
        /// <returns>处理后的年龄字符串</returns>
        private string AgeConvert(string sAge)
        {
            string sResult = string.Empty;
            try
            {
                string type = sAge.Substring(0, 1);
                string num = sAge.Substring(1, sAge.Length - 1);
                switch (type)
                {
                    case "Y":
                        sResult = num + "岁";
                        break;
                    case "M":
                        sResult = num + "月";
                        break;
                    case "D":
                        sResult = num + "天";
                        break;
                    case "H":
                        sResult = num + "小时";
                        break;
                }

                return sResult;
            }
            catch
            {
                return sResult;
            }
        }
    }
}
