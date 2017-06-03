using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_DrugManage.Winform.ViewForm;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.Controller
{
    /// <summary>
    /// 药品盘点控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmCheckDS")]//在菜单上显示
    [WinformView(Name = "FrmCheckDS", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmCheck")]//药房盘点-药房
    [WinformView(Name = "FrmCheckDW", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmCheck")]//药品盘点-药库
    [WinformView(Name = "FrmCheckDetailDS", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmCheckDetailDS")]//药品盘点明细
    [WinformView(Name = "FrmCheckDetailDW", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmCheckDetail")]//药品盘点明细
    [WinformView(Name = "FrmCheckType", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmCheckType")]//提取盘点数据，药品类型选择
    [WinformView(Name = "FrmCheckAuditDS", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmCheckAuditDS")]//药房盘点审核-药房
    [WinformView(Name = "FrmCheckAuditDW", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmCheckAudit")]//药品盘点审核-药库
    public class CheckStoreController : WcfClientController
    {
        #region 药品盘点主界面
        /// <summary>
        /// 当前单据编辑状态
        /// </summary>
        private DGEnum.BillEditStatus billStatus;

        /// <summary>
        /// 当前选中科室
        /// </summary>
        private int selectedDeptIDDS = 0;

        /// <summary>
        /// 当前选中科室
        /// </summary>
        private int selectedDeptIDDW = 0;

        /// <summary>
        /// 药房盘点窗体
        /// </summary>
        IFrmCheck frmCheckDS;

        /// <summary>
        /// 药库盘点窗体
        /// </summary>
        IFrmCheck frmCheckDW;

        /// <summary>
        /// 药品类型选择窗体
        /// </summary>
        IFrmCheckType frmCheckType;

        /// <summary>
        /// 药房盘点审核窗体
        /// </summary>
        IFrmCheckAudit frmCheckAuditDS;

        /// <summary>
        /// 药库盘点审核窗体
        /// </summary>
        IFrmCheckAudit frmCheckAuditDW;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            frmCheckDS = (IFrmCheck)iBaseView["FrmCheckDS"];
            frmCheckDW = (IFrmCheck)iBaseView["FrmCheckDW"];
            frmCheckDetailDW = (IFrmCheckDetail)iBaseView["FrmCheckDetailDW"];
            frmCheckDetailDS = (IFrmCheckDetail)iBaseView["FrmCheckDetailDS"];
            frmCheckType = (IFrmCheckType)iBaseView["FrmCheckType"];
            frmCheckAuditDS = (IFrmCheckAudit)iBaseView["FrmCheckAuditDS"];
            frmCheckAuditDW = (IFrmCheckAudit)iBaseView["FrmCheckAuditDW"];
        }

        /// <summary>
        /// 获取药剂科室列表
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void GetDrugDept(string frmName)
        {
            if (frmName == "FrmCheckDW")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(1);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "GetDrugDeptList", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                frmCheckDW.BindDrugDept(dt);
            }
            else if (frmName == "FrmCheckAuditDS")
            {
                //药房盘点审核
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(0);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "GetDrugDeptList", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                frmCheckAuditDS.BindDrugDept(dt);
            }
            else if (frmName == "FrmCheckAuditDW")
            {
                //药库盘点审核
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(1);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "GetDrugDeptList", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                frmCheckAuditDW.BindDrugDept(dt);
            }
            else if (frmName == "FrmCheckDS")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(0);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "GetDrugDeptList", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                frmCheckDS.BindDrugDept(dt);
            }
        }

        /// <summary>
        /// 查询单据表头
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void LoadBillHead(string frmName)
        {
            if (frmName == "FrmCheckDS")
            {
                //如果是药房盘点
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_CHECK);
                    request.AddData(frmCheckDS.GetQueryCondition(selectedDeptIDDS));
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "LoadBillHead", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmCheckDS.BindInHeadGrid(dtRtn);
            }
            else
            {
                //如果是药库盘点
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_CHECK);
                    request.AddData(frmCheckDW.GetQueryCondition(selectedDeptIDDW));
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "LoadBillHead", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmCheckDW.BindInHeadGrid(dtRtn);
            }
        }

        /// <summary>
        /// 查询单据明细
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void LoadBillDetails(string frmName)
        {
            Dictionary<string, string> headInfo = null;
            Action<ClientRequestData> requestAction = null;
            ServiceResponseData retdata = null;
            switch (frmName)
            {
                //药房盘点
                case "FrmCheckDS":
                    headInfo = frmCheckDS.GetCurrentHeadID();
                    if (headInfo != null)
                    {
                        requestAction = ((ClientRequestData request) =>
                        {
                            request.AddData(DGConstant.OP_DS_CHECK);
                            request.AddData(headInfo);
                        });
                        retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "LoadBillDetails", requestAction);
                        frmCheckDS.BindInDetailGrid(retdata.GetData<DataTable>(0));
                    }
                    else
                        frmCheckDS.BindInDetailGrid(null);
                    break;

                //药库盘点
                case "FrmCheckDW":
                    headInfo = frmCheckDW.GetCurrentHeadID();
                    if (headInfo != null)
                    {
                        requestAction = ((ClientRequestData request) =>
                        {
                            request.AddData(DGConstant.OP_DW_CHECK);
                            request.AddData(headInfo);
                        });
                        retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "LoadBillDetails", requestAction);
                        frmCheckDW.BindInDetailGrid(retdata.GetData<DataTable>(0));
                    }
                    else
                        frmCheckDW.BindInDetailGrid(null);
                    break;

                //药库盘点明细
                case "FrmCheckDetailDW":
                    headInfo = new Dictionary<string, string>();
                    headInfo.Add("CheckHeadID", currentDWHead.CheckHeadID.ToString());
                    requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(DGConstant.OP_DW_CHECK);
                        request.AddData(headInfo);
                    });
                    retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "LoadBillDetails", requestAction);
                    currentDWDetails = retdata.GetData<DataTable>(0);
                    frmCheckDetailDW.BindInDetails(currentDWDetails);
                    break;

                //药房盘点明细
                case "FrmCheckDetailDS":
                    headInfo = new Dictionary<string, string>();
                    headInfo.Add("CheckHeadID", currentDSHead.CheckHeadID.ToString());
                    requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(DGConstant.OP_DS_CHECK);
                        request.AddData(headInfo);
                    });
                    retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "LoadBillDetails", requestAction);
                    currentDSDetails = retdata.GetData<DataTable>(0);
                    frmCheckDetailDS.BindInDetails(currentDSDetails);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 初始化单据头实体信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="billEditStatus">单据编辑状态</param>
        /// <param name="currentBillID">当前单据ID</param>
        [WinformMethod]
        public void InitBillHead(string frmName, DGEnum.BillEditStatus billEditStatus, int currentBillID)
        {
            billStatus = billEditStatus;
            if (frmName == "FrmCheckDW")
            {
                if (billStatus == DGEnum.BillEditStatus.ADD_STATUS)
                {
                    DW_CheckHead inHead = new DW_CheckHead();
                    inHead.AuditFlag = 0;
                    inHead.AuditEmpID = 0;
                    inHead.AuditEmpName = string.Empty;
                    inHead.Remark = string.Empty;
                    inHead.DelFlag = 0;
                    inHead.DeptID = selectedDeptIDDW;
                    inHead.BusiType = DGConstant.OP_DW_CHECK;
                    inHead.RegTime = System.DateTime.Now;
                    inHead.RegEmpID = GetUserInfo().EmpId;
                    inHead.RegEmpName = GetUserInfo().EmpName;
                    inHead.BillNO = 0;
                    currentDWHead = inHead;
                    frmCheckDetailDW.BindInHeadInfo(currentDWHead);
                }
                else
                {
                    Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(DGConstant.OP_DW_SYSTEM);
                        request.AddData(currentBillID);
                    });
                    ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "GetEditBillHead", requestAction);
                    DW_CheckHead inStoreHead = retdata.GetData<DW_CheckHead>(0);
                    currentDWHead = inStoreHead;
                    frmCheckDetailDW.BindInHeadInfo(currentDWHead);
                }

                frmCheckDetailDW.InitControStatus(billEditStatus);
            }
            else
            {
                if (billStatus == DGEnum.BillEditStatus.ADD_STATUS)
                {
                    DS_CheckHead inHead = new DS_CheckHead();
                    inHead.AuditFlag = 0;
                    inHead.AuditEmpID = 0;
                    inHead.AuditEmpName = string.Empty;
                    inHead.AuditNO = 0;
                    inHead.Remark = string.Empty;
                    inHead.DelFlag = 0;
                    inHead.DeptID = selectedDeptIDDS;
                    inHead.BusiType = DGConstant.OP_DS_CHECK;
                    inHead.RegTime = System.DateTime.Now;
                    inHead.RegEmpID = GetUserInfo().EmpId;
                    inHead.RegEmpName = GetUserInfo().EmpName;
                    inHead.BillNO = 0;
                    currentDSHead = inHead;
                    frmCheckDetailDS.BindInHeadInfo(currentDSHead);
                }
                else
                {
                    Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(DGConstant.OP_DS_SYSTEM);
                        request.AddData(currentBillID);
                    });
                    ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "GetEditBillHead", requestAction);
                    DS_CheckHead inStoreHead = retdata.GetData<DS_CheckHead>(0);
                    currentDSHead = inStoreHead;
                    frmCheckDetailDS.BindInHeadInfo(currentDSHead);
                }

                frmCheckDetailDS.InitControStatus(billEditStatus);
            }
        }

        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        /// <param name="billID">单据ID</param>
        /// <param name="busiType">业务类型</param>
        [WinformMethod]
        public void DeleteBill(string frmName, int billID, string busiType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(busiType);
                request.AddData(billID);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "DeleteBill", requestAction);
            bool result = retdata.GetData<bool>(0);
            if (result)
            {
                MessageBoxShowSimple("单据已经成功删除");
                LoadBillHead(frmName);
            }
            else
            {
                string rtnMsg = retdata.GetData<string>(1);
                MessageBoxShowSimple("单据删除失败:" + rtnMsg);
            }
        }

        /// <summary>
        /// 清除盘点状态
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void ClearCheckStatus(string frmName)
        {
            string busiType = string.Empty;
            int selectedDeptID = 0;
            if (frmName == "FrmCheckDW")
            {
                busiType = DGConstant.OP_DW_CHECK;
                selectedDeptID = selectedDeptIDDW;
            }
            else
            {
                busiType = DGConstant.OP_DS_CHECK;
                selectedDeptID = selectedDeptIDDS;
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(busiType);
                request.AddData(selectedDeptID);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "ClearCheckStatus", requestAction);
            bool result = retdata.GetData<bool>(0);
            if (result)
            {
                MessageBoxShowSimple("清除盘点状态成功");
                LoadBillHead(frmName);
            }
            else
            {
                string rtnMsg = retdata.GetData<string>(1);
                MessageBoxShowSimple("清除盘点状态失败:" + rtnMsg);
            }
        }

        /// <summary>
        /// 设置盘点状态
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void SetCheckStatus(string frmName)
        {
            int selectedDeptID = 0;
            if (frmName == "FrmCheckDS")
            {
                selectedDeptID = selectedDeptIDDS;
            }
            else
            {
                selectedDeptID = selectedDeptIDDW;
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(selectedDeptID);
                request.AddData(1);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "SetCheckStatus", requestAction);
            bool result = retdata.GetData<bool>(0);
            if (result)
            {
            }
            else
            {
                string rtnMsg = retdata.GetData<string>(1);
                MessageBoxShowSimple("盘点标志设置失败:" + rtnMsg);
            }
        }
        #endregion

        #region 药品盘点明细界面
        /// <summary>
        /// 药房盘点单明细窗体
        /// </summary>
        IFrmCheckDetail frmCheckDetailDS;

        /// <summary>
        /// 药库盘点单明细窗体
        /// </summary>
        IFrmCheckDetail frmCheckDetailDW;

        /// <summary>
        /// 当前编辑药库单据头实体
        /// </summary>
        private DW_CheckHead currentDWHead;

        /// <summary>
        /// 当前编辑药库单据明细列表
        /// </summary>
        private DataTable currentDWDetails;

        /// <summary>
        /// 当前编辑药房单据头实体
        /// </summary>
        private DS_CheckHead currentDSHead;

        /// <summary>
        /// 当前编辑药房单据明细列表
        /// </summary>
        private DataTable currentDSDetails;

        /// <summary>
        /// 药品批次信息表
        /// </summary>
        private DataTable dtBatchInfo;

        /// <summary>
        /// 获取单据编辑状态
        /// </summary>
        /// <returns>单据编辑状态</returns>
        [WinformMethod]
        public DGEnum.BillEditStatus GetBillStatus()
        {
            return billStatus;
        }

        /// <summary>
        /// 获取当前选中的药剂科室ID
        /// </summary>
        /// <param name="deptID">药剂科室ID</param>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void SetSelectedDept(int deptID, string frmName)
        {
            if (frmName == "FrmCheckDS")
            {
                selectedDeptIDDS = deptID;
            }
            else if (frmName == "FrmCheckAuditDS")
            {
                selectedDeptIDDS = deptID;
            }
            else if (frmName == "FrmCheckDW")
            {
                selectedDeptIDDW = deptID;
            }
            else if (frmName == "FrmCheckAuditDW")
            {
                selectedDeptIDDW = deptID;
            }
        }

        /// <summary>
        /// 计算汇总金额并展示
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void ComputeTotalFee(string frmName)
        {
            if (frmName == "FrmCheckDetailDW")
            {
                if (currentDWDetails != null)
                {
                    decimal totalAct = 0;
                    decimal totalFact = 0;
                    decimal retailPrice = 0;
                    decimal factAmount = 0;
                    decimal actAmount = 0;
                    for (int index = 0; index < currentDWDetails.Rows.Count; index++)
                    {
                        DataRow dRow = currentDWDetails.Rows[index];
                        retailPrice = dRow["RetailPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(dRow["RetailPrice"]);
                        factAmount = dRow["FactAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(dRow["FactAmount"]);
                        actAmount = dRow["ActAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(dRow["ActAmount"]);
                        totalAct += retailPrice * actAmount;
                        totalFact += retailPrice * factAmount;
                    }

                    frmCheckDetailDW.ShowTotalFee(totalAct, totalFact);
                }
            }

            if (frmName == "FrmCheckDetailDS")
            {
                if (currentDSDetails != null)
                {
                    decimal totalAct = 0;
                    decimal totalFact = 0;
                    decimal actRetailFee = 0;
                    decimal factRetailFee = 0;
                    for (int index = 0; index < currentDSDetails.Rows.Count; index++)
                    {
                        DataRow dRow = currentDSDetails.Rows[index];
                        actRetailFee = dRow["ActRetailFee"] == DBNull.Value ? 0 : Convert.ToDecimal(dRow["ActRetailFee"]);
                        factRetailFee = dRow["FactRetailFee"] == DBNull.Value ? 0 : Convert.ToDecimal(dRow["FactRetailFee"]);

                        totalAct += actRetailFee;
                        totalFact += factRetailFee;
                    }

                    frmCheckDetailDS.ShowTotalFee(totalAct, totalFact);
                }
            }
        }

        /// <summary>
        /// 获取盘点单药品数据
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetCheckDrugInfo(string frmName)
        {
            if (frmName == "FrmCheckDetailDW")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(currentDWHead.BusiType);
                    request.AddData(selectedDeptIDDW);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "GetCheckDrugInfo", requestAction);
                frmCheckDetailDW.BindDrugInfoCard(retdata.GetData<DataTable>(0));
                frmCheckDetailDW.BindDrugPositFindCard(retdata.GetData<DataTable>(0));
            }
            else if (frmName == "FrmCheckDetailDS")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(currentDSHead.BusiType);
                    request.AddData(selectedDeptIDDS);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "GetCheckDrugInfo", requestAction);
                frmCheckDetailDS.BindDrugInfoCard(retdata.GetData<DataTable>(0));
                frmCheckDetailDS.BindDrugPositFindCard(retdata.GetData<DataTable>(0));
            }
            else if (frmName == "FrmCheckAuditDS")
            {
                //药房盘点审核
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(DGConstant.OP_DS_CHECK);
                    request.AddData(selectedDeptIDDS);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "GetCheckDrugInfo", requestAction);
                frmCheckAuditDS.BindDrugPositFindCard(retdata.GetData<DataTable>(0));
            }
            else if (frmName == "FrmCheckAuditDW")
            {
                //药库盘点审核
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(DGConstant.OP_DW_CHECK);
                    request.AddData(selectedDeptIDDW);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "GetCheckDrugInfo", requestAction);
                frmCheckAuditDW.BindDrugPositFindCard(retdata.GetData<DataTable>(0));
            }
        }

        /// <summary>
        /// 获取批次数据
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDrugBatchInfo(string frmName)
        {
            if (frmName == "FrmCheckDetailDW")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(selectedDeptIDDW);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "GetInStoreBatchInfo", requestAction);
                dtBatchInfo = retdata.GetData<DataTable>(0);
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(selectedDeptIDDS);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "GetInStoreBatchInfo", requestAction);
                dtBatchInfo = retdata.GetData<DataTable>(0);
            }
        }

        /// <summary>
        /// 保存单据
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void SaveBill(string frmName)
        {
            Action<ClientRequestData> requestAction = null;
            ServiceResponseData retdata = null;
            RefreshHead(frmName);
            if (frmName == "FrmCheckDetailDW")
            {
                List<DW_CheckDetail> lstDetails = new List<DW_CheckDetail>();
                for (int index = 0; index < currentDWDetails.Rows.Count; index++)
                {
                    DW_CheckDetail detail = ConvertExtend.ToObject<DW_CheckDetail>(currentDWDetails, index);
                    lstDetails.Add(detail);
                }

                requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(currentDWHead.BusiType);
                    request.AddData(currentDWHead);
                    request.AddData<List<DW_CheckDetail>>(lstDetails);
                    request.AddData<List<int>>(frmCheckDetailDW.GetDeleteDetails());
                });
                retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "SaveBill", requestAction);
                bool result = retdata.GetData<bool>(0);
                if (result)
                {
                    MessageBoxShowSimple("单据保存盘点，请及时审核...");
                    if (billStatus == DGEnum.BillEditStatus.ADD_STATUS)
                    {
                        frmCheckDetailDW.NewBillClear();
                    }
                    else
                    {
                        frmCheckDetailDW.CloseCurrentWindow();
                    }
                }
                else
                {
                    string rtnMsg = retdata.GetData<string>(1);
                    MessageBoxShowSimple("单据保存失败:" + rtnMsg);
                }
            }
            else
            {
                List<DS_CheckDetail> lstDetails = new List<DS_CheckDetail>();
                for (int index = 0; index < currentDSDetails.Rows.Count; index++)
                {
                    DS_CheckDetail detail = ConvertExtend.ToObject<DS_CheckDetail>(currentDSDetails, index);
                    lstDetails.Add(detail);
                }

                requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(currentDSHead.BusiType);
                    request.AddData(currentDSHead);
                    request.AddData<List<DS_CheckDetail>>(lstDetails);
                    request.AddData<List<int>>(frmCheckDetailDS.GetDeleteDetails());
                });
                retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "SaveBill", requestAction);
                bool result = retdata.GetData<bool>(0);
                if (result)
                {
                    MessageBoxShowSimple("单据保存盘点，请及时审核...");
                    if (billStatus == DGEnum.BillEditStatus.ADD_STATUS)
                    {
                        frmCheckDetailDS.NewBillClear();
                    }
                    else
                    {
                        frmCheckDetailDS.CloseCurrentWindow();
                    }
                }
                else
                {
                    string rtnMsg = retdata.GetData<string>(1);
                    MessageBoxShowSimple("单据保存失败:" + rtnMsg);
                }
            }
        }

        /// <summary>
        /// 从界面信息更新单据头
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void RefreshHead(string frmName)
        {
            if (frmName == "FrmCheckDetailDW")
            {
                DW_CheckHead editHead = frmCheckDetailDW.GetInHeadInfoDW();
                currentDWHead.RegTime = editHead.RegTime;
            }
            else
            {
                DS_CheckHead editHead = frmCheckDetailDS.GetInHeadInfoDS();
                currentDSHead.RegTime = editHead.RegTime;
            }
        }

        /// <summary>
        /// 切换批次绑定数据源
        /// </summary>
        /// <param name="drugID">选定药品ID</param>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void ChangeBatchDrug(int drugID, string frmName)
        {
            DataTable dtCurrentBatchNO = null;
            DataRow[] selectRows = dtBatchInfo.Select("DrugID=" + drugID.ToString());
            if (selectRows.Length > 0)
            {
                dtCurrentBatchNO = selectRows.CopyToDataTable();
            }
            else
            {
                dtCurrentBatchNO = dtBatchInfo.Clone();
            }
        }

        /// <summary>
        /// 打印盘点空表
        /// </summary>
        /// <param name="dataSource">盘点药品数据</param>
        /// <param name="frmName">进入窗口</param>
        [WinformMethod]
        public void Print(DataTable dataSource, string frmName)
        {
            if (frmName == "FrmCheckDetailDW")
            {
                if (dataSource.Rows.Count > 0)
                {
                    Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                    myDictionary.Add("HospitalName", LoginUserInfo.WorkName);
                    myDictionary.Add("RegDept", LoginUserInfo.DeptName);
                    myDictionary.Add("RegPeople", LoginUserInfo.EmpName);
                    myDictionary.Add("doseName", string.Empty);
                    EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 4002, 0, myDictionary, dataSource).PrintPreview(true);
                }
                else
                {
                    MessageBoxShowSimple("请先提取数据");
                }
            }
            else
            {
                if (dataSource.Rows.Count > 0)
                {
                    Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                    myDictionary.Add("HospitalName", LoginUserInfo.WorkName);
                    myDictionary.Add("RegDept", LoginUserInfo.DeptName);
                    myDictionary.Add("RegPeople", LoginUserInfo.EmpName);
                    myDictionary.Add("doseName", string.Empty);
                    EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 4003, 0, myDictionary, dataSource).PrintPreview(true);
                }
                else
                {
                    MessageBoxShowSimple("请先提取数据");
                }
            }
        }
        #endregion

        #region 提取盘点数据
        /// <summary>
        /// 取得药品类型典
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDrugTypeDic(string frmName)
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "QueryStoreController", "GetDrugTypeDic");
            DataTable dt = retdata.GetData<DataTable>(0);
            if (frmName == "FrmCheckType")
            {
                //药库
                frmCheckType.BindDrugType(dt);
            }
            else
            {
                //药房
                frmCheckType.BindDrugType(dt);
            }
        }

        /// <summary>
        /// 取得药品剂型典，根据药品类型过滤
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="typeId">药品类型Id</param>
        [WinformMethod]
        public void GetDosageDic(string frmName, int typeId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(typeId);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "QueryStoreController", "GetDosageDic", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            if (frmName == "FrmCheckType")
            {
                //药库
                frmCheckType.BindDrugDosageShowCard(dt);
            }
            else
            {
                frmCheckType.BindDrugDosageShowCard(dt);
            }
        }

        /// <summary>
        /// 提取盘点库存数据
        /// </summary>
        /// <param name="frmName">父窗体窗体入口</param>
        [WinformMethod]
        public void GetCheckStorageData(string frmName)
        {
            if (frmName == "FrmCheckDetailDS")
            {
                //如果是药房盘点
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_CHECK);
                    request.AddData(frmCheckType.GetQueryCondition(selectedDeptIDDS));
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "LoadStorageData", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmCheckDetailDS.InsertGetStorageData(dtRtn);
            }
            else
            {
                //如果是药库盘点
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_CHECK);
                    request.AddData(frmCheckType.GetQueryCondition(selectedDeptIDDW));
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "LoadStorageData", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmCheckDetailDW.InsertGetStorageData(dtRtn);
            }
        }

        /// <summary>
        /// 选择药品类型
        /// </summary>
        /// <param name="fatherFrmname">父窗体名称</param>
        /// <returns>返回结果</returns>
        [WinformMethod]
        public DialogResult OpenDrugTypeDialog(string fatherFrmname)
        {
            var dialog = iBaseView["FrmCheckType"] as FrmCheckType;
            dialog.FatherFrmname = fatherFrmname;
            if (dialog == null)
            {
                return DialogResult.None;
            }

            return dialog.ShowDialog();
        }
        #endregion

        #region 药品盘点审核界面
        /// <summary>
        /// 取得盘点审核待审的状态信息
        /// 库房状态，待审单据数
        /// </summary>
        /// /// <param name="frmName">窗口入口</param>
        /// <param name="deptId">库房ID</param>
        [WinformMethod]
        public void CheckStatusInfos(string frmName, int deptId)
        {
            var busiType = string.Empty;
            if (frmName == "FrmCheckAuditDW")
            {
                busiType = DGConstant.OP_DW_CHECK;
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(busiType);
                    request.AddData(deptId);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "CheckStatusInfos", requestAction);
                frmCheckAuditDW.BindShowTip(retdata.GetData<string>(0));
            }
            else
            {
                busiType = DGConstant.OP_DS_CHECK;
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(busiType);
                    request.AddData(deptId);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "CheckStatusInfos", requestAction);
                frmCheckAuditDS.BindShowTip(retdata.GetData<string>(0));
            }
        }

        /// <summary>
        /// 加载盘点审核单表头
        /// </summary>
        /// <param name="frmName">进入窗口名称</param>
        [WinformMethod]
        public void LoadAudtiCheckHead(string frmName)
        {
            if (frmName == "FrmCheckAuditDS")
            {
                //如果是药房盘点审核
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_CHECK);
                    request.AddData(frmCheckAuditDS.GetAuditHeadQueryCondition());
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "LoadAudtiCheckHead", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmCheckAuditDS.BindAuditHeadGrid(dtRtn);
            }
            else
            {
                //如果是药库盘点审核
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_CHECK);
                    request.AddData(frmCheckAuditDW.GetAuditHeadQueryCondition());
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "LoadAudtiCheckHead", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmCheckAuditDW.BindAuditHeadGrid(dtRtn);
            }
        }

        /// <summary>
        /// 加载盘点审核单明细
        /// </summary>
        /// <param name="frmName">进入窗口名称</param>
        [WinformMethod]
        public void LoadAuditCheckDetail(string frmName)
        {
            if (frmName == "FrmCheckAuditDS")
            {
                //如果是药房盘点审核
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_CHECK);
                    request.AddData(frmCheckAuditDS.GetAuditDetailQueryCondition());
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "LoadAuditCheckDetail", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmCheckAuditDS.BindAuditDetailGrid(dtRtn);
            }
            else
            {
                //如果是药库盘点审核
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_CHECK);
                    request.AddData(frmCheckAuditDW.GetAuditDetailQueryCondition());
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "LoadAuditCheckDetail", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmCheckAuditDW.BindAuditDetailGrid(dtRtn);
            }
        }

        /// <summary>
        /// 汇总所有未审核单据明细并提取
        /// </summary>
        /// <param name="frmName">进入窗口名称</param>
        [WinformMethod]
        public void LoadAllNotAuditDetail(string frmName)
        {
            if (frmName == "FrmCheckAuditDS")
            {
                //如果是药房盘点审核
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_CHECK);
                    request.AddData(frmCheckAuditDS.GetAllNotAuditDetailQueryCondition());
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "LoadAllNotAuditDetail", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmCheckAuditDS.BindCheckDetailGrid(dtRtn);
                string message = ComputeAuditTotalFee(frmName, dtRtn);
                frmCheckAuditDS.ShowAuditCompute(message);
            }
            else
            {
                //如果是药库盘点审核
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_CHECK);
                    request.AddData(frmCheckAuditDW.GetAllNotAuditDetailQueryCondition());
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "LoadAllNotAuditDetail", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmCheckAuditDW.BindCheckDetailGrid(dtRtn);
                string message = ComputeAuditTotalFee(frmName, dtRtn);
                frmCheckAuditDW.ShowAuditCompute(message);
            }
        }

        /// <summary>
        /// 计算汇总值
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="currentDetails">明细数据集</param>
        /// <returns>汇总值</returns>
        public string ComputeAuditTotalFee(string frmName, DataTable currentDetails)
        {
            string showMessage = string.Empty;

            //            SUM(a.FactAmount) AS FactAmount,
            //SUM(a.FactStockFee)AS FactStockFee,
            //SUM(a.FactRetailFee) AS FactRetailFee,
            //a.ActAmount,
            //a.ActStockFee,
            //a.ActRetailFee,盘存进货金额：0.00元；账存进货金额：0.00元；盘存零售金额：0.00元；账存零售金额：0.00元；盘盈：0.00元；盘亏：0.00元
            if (frmName == "FrmCheckAuditDW" || frmName == "FrmCheckAuditDS")
            {
                if (currentDetails != null)
                {
                    decimal actStockFeeTotal = 0;//账存进货总额
                    decimal actRetailFeeTotal = 0;//账存零售金额
                    decimal factStockFeeTotal = 0;//盘存进货金额
                    decimal factRetailFeeTotal = 0;//盘存零售金额
                    decimal profitSum = 0;//盘盈
                    decimal lossSum = 0;//盘亏
                    for (int index = 0; index < currentDetails.Rows.Count; index++)
                    {
                        DataRow dRow = currentDetails.Rows[index];
                        actStockFeeTotal += dRow["ActStockFee"] == DBNull.Value ? 0 : Convert.ToDecimal(dRow["ActStockFee"]);
                        actRetailFeeTotal += dRow["ActRetailFee"] == DBNull.Value ? 0 : Convert.ToDecimal(dRow["ActRetailFee"]);
                        factStockFeeTotal += dRow["FactStockFee"] == DBNull.Value ? 0 : Convert.ToDecimal(dRow["FactStockFee"]);
                        factRetailFeeTotal += dRow["FactRetailFee"] == DBNull.Value ? 0 : Convert.ToDecimal(dRow["FactRetailFee"]);
                    }

                    if (factRetailFeeTotal > actRetailFeeTotal)
                    {
                        profitSum = factRetailFeeTotal - actRetailFeeTotal;
                        lossSum = 0;
                    }
                    else
                    {
                        profitSum = 0;
                        lossSum = actRetailFeeTotal - factRetailFeeTotal;
                    }

                    showMessage = string.Format("盘存进货金额：{0}元；账存进货金额：{1}元；盘存零售金额：{2}元；账存零售金额：{3}元；盘盈：{4}元；盘亏：{5}元", Math.Round(factStockFeeTotal, 2), Math.Round(actStockFeeTotal, 2), Math.Round(factRetailFeeTotal, 2), Math.Round(actRetailFeeTotal, 2), Math.Round(profitSum, 2), Math.Round(lossSum, 2));
                }
            }

            return showMessage;
        }

        /// <summary>
        /// 单据审核
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="deptId">单据ID</param>
        [WinformMethod]
        public void AuditBill(string frmName, int deptId)
        {
            string busiType = string.Empty;
            if (frmName == "FrmCheckAuditDW")
            {
                busiType = DGConstant.OP_DW_CHECK;
            }
            else
            {
                busiType = DGConstant.OP_DS_CHECK;
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(busiType);
                request.AddData(deptId);
                request.AddData(GetUserInfo().EmpId);
                request.AddData(GetUserInfo().EmpName);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "CheckStoreController", "AuditBill", requestAction);
            DGBillResult result = retdata.GetData<DGBillResult>(0);
            if (result.Result == 0)
            {
                MessageBoxShowSimple("单据已经成功审核，请确认库存是否更新...");
            }
            else
            {
                string rtnMsg = result.ErrMsg;
                MessageBoxShowSimple("单据审核失败:" + result.ErrMsg);
            }
        }

        /// <summary>
        /// 打印盘点单
        /// </summary>
        /// <param name="frmName">窗体进入入口</param>
        /// <param name="deptName">库房名称</param>
        /// <param name="deptId">库房Id</param>
        /// <param name="auditHeadRow">审核头表行对象</param>
        /// <param name="dtAuditHead">审核头表</param>
        /// <param name="dtAuditDetail">审核明细表</param>
        [WinformMethod]
        public void PrintCheckBill(string frmName, string deptName, int deptId, DataRow auditHeadRow, DataTable dtAuditHead, DataTable dtAuditDetail)
        {
            if (auditHeadRow != null)
            {
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                myDictionary.Add("CheckDate", Convert.ToDateTime(auditHeadRow["AuditTime"]));//审核日期
                myDictionary.Add("BillNo", auditHeadRow["BillNO"].ToString());//审核单据号
                myDictionary.Add("HospitalName", LoginUserInfo.WorkName);//医院名称
                myDictionary.Add("Printer", LoginUserInfo.EmpName);  //打印人
                myDictionary.Add("PrintTime", System.DateTime.Now);//打印时间
                myDictionary.Add("AuditPeople", auditHeadRow["EmpName"].ToString());
                myDictionary.Add("AuditDept", deptName + "盘点单据");//审核部门
                myDictionary.Add("FtTradeFee", auditHeadRow["CheckStockFee"].ToString());//盘存批发金额-合计
                myDictionary.Add("FtRetailFee", auditHeadRow["CheckRetailFee"].ToString());//盘存零售金额-合计
                myDictionary.Add("XYFtTradeFee", dtAuditDetail.Compute("sum(FactStockFee)", "TypeID=1").ToString());//盘存批发金额-西药
                myDictionary.Add("XYFtRetailFee", dtAuditDetail.Compute("sum(FactRetailFee)", "TypeID=1").ToString());//盘存零售金额-西药
                myDictionary.Add("ZCYFtTradeFee", dtAuditDetail.Compute("sum(FactStockFee)", "TypeID=2").ToString());//盘存批发金额-中成药
                myDictionary.Add("ZCYFtRetailFee", dtAuditDetail.Compute("sum(FactRetailFee)", "TypeID=2").ToString());//盘存零售金额-中成药
                myDictionary.Add("ZYFtTradeFee", dtAuditDetail.Compute("sum(FactStockFee)", "TypeID=3").ToString());//盘存批发金额-中草药
                myDictionary.Add("ZYFtRetailFee", dtAuditDetail.Compute("sum(FactRetailFee)", "TypeID=3").ToString());//盘存零售金额-中草药
                myDictionary.Add("WZFtTradeFee", string.Empty);
                myDictionary.Add("WZFtRetailFee", string.Empty);
                myDictionary.Add("CkTradeFee", dtAuditDetail.Compute("sum(ActStockFee)", string.Empty).ToString());//账存批发金额-合计
                myDictionary.Add("CkRetailFee", dtAuditDetail.Compute("sum(ActRetailFee)", string.Empty).ToString());//账存零售金额-合计
                myDictionary.Add("XYCkTradeFee", dtAuditDetail.Compute("sum(ActStockFee)", "TypeID=1").ToString());//账存批发金额-西药
                myDictionary.Add("XYCkRetailFee", dtAuditDetail.Compute("sum(ActRetailFee)", "TypeID=1").ToString());//账存零售金额-西药
                myDictionary.Add("ZCYCkTradeFee", dtAuditDetail.Compute("sum(ActStockFee)", "TypeID=2").ToString());//账存批发金额-中成药
                myDictionary.Add("ZCYCkRetailFee", dtAuditDetail.Compute("sum(ActRetailFee)", "TypeID=2").ToString());//账存零售金额-中成药
                myDictionary.Add("ZYCkTradeFee", dtAuditDetail.Compute("sum(ActStockFee)", "TypeID=3").ToString());//账存批发金额-中草药
                myDictionary.Add("ZYCkRetailFee", dtAuditDetail.Compute("sum(ActRetailFee)", "TypeID=3").ToString());//账存零售金额-中草药
                myDictionary.Add("WZCkTradeFee", string.Empty);
                myDictionary.Add("WZCkRetailFee", string.Empty);
                myDictionary.Add("MoreRetailFee", auditHeadRow["ProfitRetailFee"].ToString());//盘盈零售金额-合计
                myDictionary.Add("MoreTradeFee", auditHeadRow["ProfitStockFee"].ToString());//盘盈进货金额-合计
                myDictionary.Add("XYMoreRetailFee", dtAuditDetail.Compute("sum(DIFFFEE)", "TypeID=1 and difffee>0").ToString());//盘盈零售金额-西药
                myDictionary.Add("XYMoreTradeFee", dtAuditDetail.Compute("sum(DIFFTRADEFEE)", "TypeID=1 and difftradefee>0").ToString()); //盘盈进货金额 - 西药
                myDictionary.Add("ZCYMoreRetailFee", dtAuditDetail.Compute("sum(DIFFFEE)", "TypeID=2 and difffee>0").ToString());//盘盈零售金额-中成药
                myDictionary.Add("ZCYMoreTradeFee", dtAuditDetail.Compute("sum(DIFFTRADEFEE)", "TypeID=2 and difftradefee>0").ToString());//盘盈进货金额 - 中成药
                myDictionary.Add("ZYMoreRetailFee", dtAuditDetail.Compute("sum(DIFFFEE)", "TypeID=3 and difffee>0").ToString());//盘盈零售金额-中草药
                myDictionary.Add("ZYMoreTradeFee", dtAuditDetail.Compute("sum(DIFFTRADEFEE)", "TypeID=3 and difftradefee>0").ToString());//盘盈进货金额 - 中草药
                myDictionary.Add("WZMoreRetailFee", string.Empty);
                myDictionary.Add("WZMoreTradeFee", string.Empty);
                myDictionary.Add("LessRetailFee", (-System.Math.Abs(Convert.ToDecimal(auditHeadRow["LossRetailFee"]))).ToString());
                myDictionary.Add("LessTradeFee", (-System.Math.Abs(Convert.ToDecimal(auditHeadRow["LossStockFee"]))).ToString());
                myDictionary.Add("XYLessRetailFee", dtAuditDetail.Compute("sum(DIFFFEE)", "TypeID=1 and difffee<0").ToString());
                myDictionary.Add("XYLessTradeFee", dtAuditDetail.Compute("sum(DIFFTRADEFEE)", "TypeID=1 and difftradefee<0").ToString());
                myDictionary.Add("ZCYLessRetailFee", dtAuditDetail.Compute("sum(DIFFFEE)", "TypeID=2 and difffee<0").ToString());
                myDictionary.Add("ZCYLessTradeFee", dtAuditDetail.Compute("sum(DIFFTRADEFEE)", "TypeID=2 and difftradefee<0").ToString());
                myDictionary.Add("ZYLessRetailFee", dtAuditDetail.Compute("sum(DIFFFEE)", "TypeID=3 and difffee<0").ToString());
                myDictionary.Add("ZYLessTradeFee", dtAuditDetail.Compute("sum(DIFFTRADEFEE)", "TypeID=3 and difftradefee<0").ToString());
                myDictionary.Add("WZLessRetailFee", string.Empty);
                myDictionary.Add("WZLessTradeFee", string.Empty);

                if (frmName == "FrmCheckAuditDW")
                {
                    EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 4004, 0, myDictionary, dtAuditDetail).PrintPreview(true);
                }
                else
                {
                    EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 4005, 0, myDictionary, dtAuditDetail).PrintPreview(true);
                }
            }
        }

        /// <summary>
        /// 打印盘盈单
        /// </summary>
        /// <param name="frmName">窗体进入入口</param>
        /// <param name="deptName">库房名称</param>
        /// <param name="deptId">库房Id</param>
        /// <param name="auditHeadRow">审核头表行对象</param>
        /// <param name="dtAuditHead">审核头表</param>
        /// <param name="dtAuditDetail">审核明细表</param>
        [WinformMethod]
        public void PrintOverCheckBill(string frmName, string deptName, int deptId, DataRow auditHeadRow, DataTable dtAuditHead, DataTable dtAuditDetail)
        {
            if (auditHeadRow != null)
            {
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                myDictionary.Add("CheckDate", Convert.ToDateTime(auditHeadRow["AuditTime"]));//审核日期
                myDictionary.Add("BillNo", auditHeadRow["BillNO"].ToString());//审核单据号
                myDictionary.Add("HospitalName", LoginUserInfo.WorkName + "(" + deptName + ")盘盈单");//医院名称
                myDictionary.Add("Printer", LoginUserInfo.EmpName);  //打印人
                myDictionary.Add("PrintTime", System.DateTime.Now);//打印时间
                myDictionary.Add("AuditPeople", auditHeadRow["EmpName"].ToString());
                myDictionary.Add("AuditDept", deptName + "盘点单据");//审核部门               
                EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 4006, 0, myDictionary, dtAuditDetail).PrintPreview(true);
            }
        }

        /// <summary>
        /// 打印盘亏单
        /// </summary>
        /// <param name="frmName">窗体进入入口</param>
        /// <param name="deptName">库房名称</param>
        /// <param name="deptId">库房Id</param>
        /// <param name="auditHeadRow">审核头表行对象</param>
        /// <param name="dtAuditHead">审核头表</param>
        /// <param name="dtAuditDetail">审核明细表</param>
        [WinformMethod]
        public void PrintLossCheckBill(string frmName, string deptName, int deptId, DataRow auditHeadRow, DataTable dtAuditHead, DataTable dtAuditDetail)
        {
            if (auditHeadRow != null)
            {
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                myDictionary.Add("CheckDate", Convert.ToDateTime(auditHeadRow["AuditTime"]));//审核日期
                myDictionary.Add("BillNo", auditHeadRow["BillNO"].ToString());//审核单据号
                myDictionary.Add("HospitalName", LoginUserInfo.WorkName + "(" + deptName + ")盘亏单");//医院名称
                myDictionary.Add("Printer", LoginUserInfo.EmpName);  //打印人
                myDictionary.Add("PrintTime", System.DateTime.Now);//打印时间
                myDictionary.Add("AuditPeople", auditHeadRow["EmpName"].ToString());
                myDictionary.Add("AuditDept", deptName + "盘点单据");//审核部门               
                EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 4006, 0, myDictionary, dtAuditDetail).PrintPreview(true);
            }
        }
        #endregion
    }
}
