using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.FinanceMgr;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_DrugManage.Winform.ViewForm;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.Controller
{
    /// <summary>
    /// 入库处理控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmInStoreDS")]//在菜单上显示
    [WinformView(Name = "FrmInStoreDS", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmInStore")]//药房入库-药房
    [WinformView(Name = "FrmInStoreDW", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmInStore")]//药库入库-药库
    [WinformView(Name = "FrmInStoreDetailDW", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmInStoreDetail")]//药品入库-药房
    [WinformView(Name = "FrmInStoreDetailDS", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmInStoreDetail")]//药品入库-药房
    [WinformView(Name = "FrmBuyBillChose", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmBuyBillChose")]//药品采购选择    
    public class InStoreController : WcfClientController
    {
        #region 药品入库主界面
        /// <summary>
        /// 当前单据编辑状态
        /// </summary>
        private DGEnum.BillEditStatus billStatus;

        /// <summary>
        /// 当前选中科室
        /// </summary>
        private int selectedDeptID = 0;

        /// <summary>
        /// 药房入库窗体
        /// </summary>
        IFrmInStore frmInStoreDS;

        /// <summary>
        /// 药库入库窗体
        /// </summary>
        IFrmInStore frmInStoreDW;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            frmInStoreDS = (IFrmInStore)iBaseView["FrmInStoreDS"];
            frmInStoreDW = (IFrmInStore)iBaseView["FrmInStoreDW"];
            frmInstoreDetailDW = (IFrmInstoreDetail)iBaseView["FrmInStoreDetailDW"];
            frmInstoreDetailDS = (IFrmInstoreDetail)iBaseView["FrmInStoreDetailDS"];
            iFrmBuyBillChose = (IFrmBuyBillChose)iBaseView["FrmBuyBillChose"];
        }

        /// <summary>
        /// 获取药剂科室参数
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDeptParameters(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(0);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugPramentController", "GetDeptParameters", requestAction);

            DataTable dt = retdata.GetData<DataTable>(0);
            switch (frmName)
            {
                case "FrmInStoreDetailDW":
                    frmInstoreDetailDW.BindDeptParameters(dt);
                    break;
                case "FrmInStoreDetailDS":
                    frmInstoreDetailDS.BindDeptParameters(dt);
                    break;
            }
        }

        /// <summary>
        /// 获取供应商列表绑定ShowCard
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void GetSupplyForShowCard(string frmName)
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "GetSupplyForShowCard");
            DataTable dt = retdata.GetData<DataTable>(0);
            switch (frmName)
            {
                case "FrmInStoreDS":
                    frmInStoreDS.BindSupply(dt);
                    break;
                case "FrmInStoreDW":
                    frmInStoreDW.BindSupply(dt);
                    break;
                case "FrmInStoreDetailDW":
                    frmInstoreDetailDW.BindSupply(dt);
                    break;
                case "FrmInStoreDetailDS":
                    frmInstoreDetailDS.BindSupply(dt);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///  获取药剂科室列表
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void GetDrugDept(string frmName)
        {
            if (frmName == "FrmInStoreDW")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(1);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "GetDrugDeptList", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                frmInStoreDW.BindDrugDept(dt);
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(0);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "GetDrugDeptList", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                frmInStoreDS.BindDrugDept(dt);
            }
        }

        /// <summary>
        /// 查询单据表头
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void LoadBillHead(string frmName)
        {
            if (frmName == "FrmInStoreDS")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_INSTORE);
                    request.AddData(frmInStoreDS.GetQueryCondition(selectedDeptID));
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "LoadBillHead", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmInStoreDS.BindInHeadGrid(dtRtn);
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_INSTORE);
                    request.AddData(frmInStoreDW.GetQueryCondition(selectedDeptID));
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "LoadBillHead", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmInStoreDW.BindInHeadGrid(dtRtn);
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
                //药房入库
                case "FrmInStoreDS":
                    headInfo = frmInStoreDS.GetCurrentHeadID();
                    if (headInfo != null)
                    {
                        requestAction = ((ClientRequestData request) =>
                        {
                            request.AddData(DGConstant.OP_DS_INSTORE);
                            request.AddData(headInfo);
                        });
                        retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "LoadBillDetails", requestAction);
                        frmInStoreDS.BindInDetailGrid(retdata.GetData<DataTable>(0));
                    }
                    else
                        frmInStoreDS.BindInDetailGrid(null);
                    break;

                //药库入库
                case "FrmInStoreDW":
                    headInfo = frmInStoreDW.GetCurrentHeadID();
                    if (headInfo != null)
                    {
                        requestAction = ((ClientRequestData request) =>
                        {
                            request.AddData(DGConstant.OP_DW_INSTORE);
                            request.AddData(headInfo);
                        });
                        retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "LoadBillDetails", requestAction);
                        frmInStoreDW.BindInDetailGrid(retdata.GetData<DataTable>(0));
                    }
                    else
                        frmInStoreDW.BindInDetailGrid(null);
                    break;

                //药库入库明细
                case "FrmInStoreDetailDW":
                    headInfo = new Dictionary<string, string>();
                    headInfo.Add("InHeadID", currentDWHead.InHeadID.ToString());
                    requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(DGConstant.OP_DW_INSTORE);
                        request.AddData(headInfo);
                    });
                    retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "LoadBillDetails", requestAction);
                    currentDWDetails = retdata.GetData<DataTable>(0);
                    frmInstoreDetailDW.BindInDetails(currentDWDetails);
                    break;

                //药房入库明细
                case "FrmInStoreDetailDS":
                    headInfo = new Dictionary<string, string>();
                    headInfo.Add("InHeadID", currentDSHead.InHeadID.ToString());
                    requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(DGConstant.OP_DS_INSTORE);
                        request.AddData(headInfo);
                    });
                    retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "LoadBillDetails", requestAction);
                    currentDSDetails = retdata.GetData<DataTable>(0);
                    frmInstoreDetailDS.BindInDetails(currentDSDetails);
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
            if (frmName == "FrmInStoreDW")
            {
                if (billStatus == DGEnum.BillEditStatus.ADD_STATUS)
                {
                    DW_InStoreHead inHead = new DW_InStoreHead();
                    inHead.AuditFlag = 0;
                    inHead.AuditEmpID = 0;
                    inHead.AuditEmpName = string.Empty;
                    inHead.Remark = string.Empty;
                    inHead.OpEmpID = 0;
                    inHead.OpEmpName = string.Empty;
                    inHead.DeptID = selectedDeptID;
                    inHead.BusiType = DGConstant.OP_DW_BUYINSTORE;
                    inHead.BillTime = System.DateTime.Now;
                    inHead.RegEmpID = GetUserInfo().EmpId;
                    inHead.RegEmpName = GetUserInfo().EmpName;
                    currentDWHead = inHead;
                    frmInstoreDetailDW.BindInHeadInfo(currentDWHead);
                }
                else
                {
                    Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(DGConstant.OP_DW_SYSTEM);
                        request.AddData(currentBillID);
                    });
                    ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "GetEditBillHead", requestAction);
                    DW_InStoreHead inStoreHead = retdata.GetData<DW_InStoreHead>(0);
                    currentDWHead = inStoreHead;
                    frmInstoreDetailDW.BindInHeadInfo(currentDWHead);
                }

                frmInstoreDetailDW.InitControStatus(billEditStatus);
            }
            else
            {
                if (billStatus == DGEnum.BillEditStatus.ADD_STATUS)
                {
                    DS_InstoreHead head = new DS_InstoreHead();
                    head.AuditFlag = 0;
                    head.AuditEmpID = 0;
                    head.AuditEmpName = string.Empty;
                    head.Remark = string.Empty;
                    head.OpEmpID = 0;
                    head.OpEmpName = string.Empty;
                    head.DeptID = selectedDeptID;
                    head.BusiType = DGConstant.OP_DW_BUYINSTORE;
                    head.BillTime = System.DateTime.Now;
                    head.RegEmpID = GetUserInfo().EmpId;
                    head.RegEmpName = GetUserInfo().EmpName;
                    currentDSHead = head;
                    frmInstoreDetailDS.BindInHeadInfo(currentDSHead);
                }
                else
                {
                    Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(DGConstant.OP_DS_SYSTEM);
                        request.AddData(currentBillID);
                    });
                    ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "GetEditBillHead", requestAction);
                    DS_InstoreHead inHead = retdata.GetData<DS_InstoreHead>(0);
                    inHead.DeptID = selectedDeptID;
                    inHead.BusiType = DGConstant.OP_DS_FIRSTIN;
                    currentDSHead = inHead;
                    frmInstoreDetailDS.BindInHeadInfo(inHead);
                }

                frmInstoreDetailDS.InitControStatus(billEditStatus);
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
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "DeleteBill", requestAction);
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
        ///  单据审核
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="billID">单据ID</param>
        /// <param name="busiType">业务类型</param>
        [WinformMethod]
        public void AuditBill(string frmName, int billID, string busiType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(busiType);
                request.AddData(billID);
                request.AddData(GetUserInfo().EmpId);
                request.AddData(GetUserInfo().EmpName);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "AuditBill", requestAction);
            DGBillResult result = retdata.GetData<DGBillResult>(0);
            if (result.Result == 0)
            {
                MessageBoxShowSimple("单据已经成功审核，请确认库存是否更新...");
                LoadBillHead(frmName);
            }
            else
            {
                string rtnMsg = result.ErrMsg;
                MessageBoxShowSimple("单据审核失败:" + result.ErrMsg);
            }
        }

        /// <summary>
        /// 构建药品业务类型数据源
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="belongSys">所属系统</param>
        [WinformMethod]
        public void BuildOpType(string frmName, string belongSys)
        {
            DataTable dtOpType = new DataTable();
            dtOpType.Columns.Add("opType");
            dtOpType.Columns.Add("opTypeName");
            if (belongSys == DGConstant.OP_DS_SYSTEM)
            {
                dtOpType.Rows.Add(new object[2] { DGConstant.OP_DS_FIRSTIN, "期初入库" });

                //dtOpType.Rows.Add(new object[2] { DGConstant.OP_DS_CIRCULATEIN, "流通入库" });
                //dtOpType.Rows.Add(new object[2] { DGConstant.OP_DS_RETURNSOTRE, "药品退库" });
                if (frmName == "FrmInStoreDS")
                {
                    frmInStoreDS.BindOpType(dtOpType);
                }
                else
                {
                    frmInstoreDetailDS.BindOpType(dtOpType);
                }
            }
            else if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                dtOpType.Rows.Add(new object[2] { DGConstant.OP_DW_BUYINSTORE, "采购入库" });
                dtOpType.Rows.Add(new object[2] { DGConstant.OP_DW_FIRSTIN, "期初入库" });
                dtOpType.Rows.Add(new object[2] { DGConstant.OP_DW_BACKSTORE, "药品退货" });
                if (frmName == "FrmInStoreDW")
                {
                    frmInStoreDW.BindOpType(dtOpType);
                }
                else
                {
                    frmInstoreDetailDW.BindOpType(dtOpType);
                }
            }
        }
        #endregion

        #region 药品入库明细界面

        /// <summary>
        ///  药房入库单明细窗体
        /// </summary>
        IFrmInstoreDetail frmInstoreDetailDS;

        /// <summary>
        ///  药库入库单明细窗体
        /// </summary>
        IFrmInstoreDetail frmInstoreDetailDW;

        /// <summary>
        ///  采购计划选择窗体
        /// </summary>
        IFrmBuyBillChose iFrmBuyBillChose;

        /// <summary>
        ///  当前编辑药库单据头实体
        /// </summary>
        private DW_InStoreHead currentDWHead;

        /// <summary>
        ///  当前编辑药库单据明细列表
        /// </summary>
        private DataTable currentDWDetails;

        /// <summary>
        ///  当前编辑药房单据头实体
        /// </summary>
        private DS_InstoreHead currentDSHead;

        /// <summary>
        ///  当前编辑药房单据明细列表
        /// </summary>
        private DataTable currentDSDetails;

        /// <summary>
        ///  药品批次信息表
        /// </summary>
        private DataTable dtBatchInfo;

        /// <summary>
        ///  获取单据编辑状态
        /// </summary>
        /// <returns>单据编辑状态</returns>
        [WinformMethod]
        public DGEnum.BillEditStatus GetBillStatus()
        {
            return billStatus;
        }

        /// <summary>
        ///  获取当前选中的药剂科室ID
        /// </summary>
        /// <param name="deptID">药剂科室ID</param>
        [WinformMethod]
        public void SetSelectedDept(int deptID)
        {
            selectedDeptID = deptID;
        }

        /// <summary>
        ///  计算汇总金额并展示
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void ComputeTotalFee(string frmName)
        {
            if (frmName == "FrmInStoreDetailDW")
            {
                if (currentDWDetails != null)
                {
                    decimal totalRetailFee = 0;
                    decimal totalStockFee = 0;
                    for (int index = 0; index < currentDWDetails.Rows.Count; index++)
                    {
                        DataRow dRow = currentDWDetails.Rows[index];
                        totalRetailFee += (dRow["RetailFee"] == DBNull.Value ? 0 : Convert.ToDecimal(dRow["RetailFee"]));
                        totalStockFee += (dRow["StockFee"] == DBNull.Value ? 0 : Convert.ToDecimal(dRow["StockFee"]));
                    }

                    frmInstoreDetailDW.ShowTotalFee(totalStockFee, totalRetailFee);
                }
            }

            if (frmName == "FrmInStoreDetailDS")
            {
                if (currentDSDetails != null)
                {
                    decimal totalRetailFee = 0;
                    decimal totalStockFee = 0;
                    for (int index = 0; index < currentDSDetails.Rows.Count; index++)
                    {
                        DataRow dRow = currentDSDetails.Rows[index];
                        totalRetailFee += (dRow["RetailFee"] == DBNull.Value ? 0 : Convert.ToDecimal(dRow["RetailFee"]));
                        totalStockFee += (dRow["StockFee"] == DBNull.Value ? 0 : Convert.ToDecimal(dRow["StockFee"]));
                    }

                    frmInstoreDetailDS.ShowTotalFee(totalStockFee, totalRetailFee);
                }
            }
        }

        /// <summary>
        ///  获取入库单药品数据
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetInStoreDrugInfo(string frmName)
        {
            if (frmName == "FrmInStoreDetailDW")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(currentDWHead.BusiType);
                    request.AddData(selectedDeptID);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "GetInStoreDrugInfo", requestAction);
                frmInstoreDetailDW.BindDrugInfoCard(retdata.GetData<DataTable>(0));
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(currentDSHead.BusiType);
                    request.AddData(selectedDeptID);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "GetInStoreDrugInfo", requestAction);
                frmInstoreDetailDS.BindDrugInfoCard(retdata.GetData<DataTable>(0));
            }
        }

        /// <summary>
        ///  获取批次数据
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDrugBatchInfo(string frmName)
        {
            if (frmName == "FrmInStoreDetailDW")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(selectedDeptID);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "GetInStoreBatchInfo", requestAction);
                dtBatchInfo = retdata.GetData<DataTable>(0);
                //当空表格时无法回车,所以暂时屏蔽这个功能
                //frmInstoreDetailDW.BindDrugBatchCard(dtBatchInfo);
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(selectedDeptID);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "GetInStoreBatchInfo", requestAction);
                dtBatchInfo = retdata.GetData<DataTable>(0);
                //frmInstoreDetailDS.BindDrugBatchCard(dtBatchInfo);
            }
        }

        /// <summary>
        ///  保存单据
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void SaveBill(string frmName)
        {
            Action<ClientRequestData> requestAction = null;
            ServiceResponseData retdata = null;
            RefreshHead(frmName);
            if (frmName == "FrmInStoreDetailDW")
            {
                currentDWHead.StockFee = 0;
                currentDWHead.RetailFee = 0;
                List<DW_InStoreDetail> lstDetails = new List<DW_InStoreDetail>();
                for (int index = 0; index < currentDWDetails.Rows.Count; index++)
                {
                    DW_InStoreDetail detail = ConvertExtend.ToObject<DW_InStoreDetail>(currentDWDetails, index);

                    decimal pAmount = 0;

                    if (currentDWDetails.Rows[index]["pAmount"] != DBNull.Value)
                    {
                        pAmount = Convert.ToDecimal(currentDWDetails.Rows[index]["pAmount"].ToString());
                    }

                    detail.Amount = pAmount;
                    currentDWHead.StockFee += detail.StockFee;
                    currentDWHead.RetailFee += detail.RetailFee;
                    lstDetails.Add(detail);
                }

                requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(currentDWHead.BusiType);
                    request.AddData(currentDWHead);
                    request.AddData<List<DW_InStoreDetail>>(lstDetails);
                    request.AddData<List<int>>(frmInstoreDetailDW.GetDeleteDetails());
                });
                retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "SaveBill", requestAction);

                DGBillResult result = retdata.GetData<DGBillResult>(0);
                if (result.Result == 0)
                {
                    MessageBoxShowSimple("单据已经保存成功，如果没有配置自动审核.请及时审核单据");
                    if (billStatus == DGEnum.BillEditStatus.ADD_STATUS)
                    {
                        frmInstoreDetailDW.NewBillClear();
                    }
                    else
                    {
                        frmInstoreDetailDW.CloseCurrentWindow();
                    }
                }
                else
                {
                    string rtnMsg = result.ErrMsg;
                    MessageBoxShowSimple("单据保存失败:" + result.ErrMsg);
                }
            }
            else
            {
                currentDSHead.StockFee = 0;
                currentDSHead.RetailFee = 0;
                List<DS_InStoreDetail> lstDetails = new List<DS_InStoreDetail>();
                for (int index = 0; index < currentDSDetails.Rows.Count; index++)
                {
                    DS_InStoreDetail detail = ConvertExtend.ToObject<DS_InStoreDetail>(currentDSDetails, index);

                    var uAmount = 0;
                    if (currentDSDetails.Rows[index]["uAmount"] != DBNull.Value)
                    {
                        uAmount = Convert.ToInt32(currentDSDetails.Rows[index]["uAmount"].ToString());
                    }

                    var pAmount = 0;
                    if (currentDSDetails.Rows[index]["pAmount"] != DBNull.Value)
                    {
                        pAmount = Convert.ToInt32(currentDSDetails.Rows[index]["pAmount"].ToString());
                    }

                    var packAmount = currentDSDetails.Rows[index]["packAmount"] == DBNull.Value ? 0 : Convert.ToInt32(currentDSDetails.Rows[index]["packAmount"]);
                    detail.Amount = uAmount + (pAmount * packAmount);
                    currentDSHead.StockFee += detail.StockFee;
                    currentDSHead.RetailFee += detail.RetailFee;
                    lstDetails.Add(detail);
                }

                requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(currentDSHead.BusiType);
                    request.AddData(currentDSHead);
                    request.AddData<List<DS_InStoreDetail>>(lstDetails);
                    request.AddData<List<int>>(frmInstoreDetailDS.GetDeleteDetails());
                });
                retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "SaveBill", requestAction);

                DGBillResult result = retdata.GetData<DGBillResult>(0);
                if (result.Result == 0)
                {
                    MessageBoxShowSimple("单据已经保存成功，如果没有配置自动审核.请及时审核单据");
                    if (billStatus == DGEnum.BillEditStatus.ADD_STATUS)
                    {
                        frmInstoreDetailDS.NewBillClear();
                    }
                    else
                    {
                        frmInstoreDetailDS.CloseCurrentWindow();
                    }
                }
                else
                {
                    string rtnMsg = result.ErrMsg;
                    MessageBoxShowSimple("单据保存失败:" + result.ErrMsg);
                }

                //bool result = retdata.GetData<bool>(0);
                //if (result)
                //{
                //    MessageBoxShowSimple("单据保存入库，请及时审核...");
                //    if (billStatus == DGEnum.BillEditStatus.ADD_STATUS)
                //        frmInstoreDetailDS.NewBillClear();
                //    else
                //        frmInstoreDetailDS.CloseCurrentWindow();
                //}
                //else
                //{
                //    string rtnMsg = retdata.GetData<string>(1);
                //    MessageBoxShowSimple("单据保存失败:" + rtnMsg);
                //}
            }
        }

        /// <summary>
        /// 获取类型名称
        /// </summary>
        /// <param name="cTypeId">子类型ID</param>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetTypeName(string cTypeId, string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(cTypeId);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "GetTypeName", requestAction);
            if (frmName == "FrmInStoreDetailDW")
            {
                frmInstoreDetailDW.TypeName = retdata.GetData<string>(0);
            }
            else
            {
                frmInstoreDetailDS.TypeName = retdata.GetData<string>(0);
            }
        }

        /// <summary>
        ///  从界面信息更新单据头
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void RefreshHead(string frmName)
        {
            if (frmName == "FrmInStoreDetailDW")
            {
                DW_InStoreHead editHead = frmInstoreDetailDW.GetInHeadInfoDW();
                currentDWHead.InvoiceDate = editHead.InvoiceDate;
                currentDWHead.BillTime = editHead.BillTime;
                currentDWHead.InvoiceNo = editHead.InvoiceNo;
                currentDWHead.DeliveryNo = editHead.DeliveryNo;
                currentDWHead.SupplierID = editHead.SupplierID;
                currentDWHead.SupplierName = editHead.SupplierName;
                currentDWHead.BusiType = editHead.BusiType;
            }
            else
            {
                DS_InstoreHead editHead = frmInstoreDetailDS.GetInHeadInfoDS();
                currentDSHead.InvoiceDate = editHead.InvoiceDate;
                currentDSHead.BillTime = editHead.BillTime;
                currentDSHead.InvoiceNO = editHead.InvoiceNO;
                currentDSHead.DeliveryNO = editHead.DeliveryNO;
                currentDSHead.SupplierID = editHead.SupplierID;
                currentDSHead.SupplierName = editHead.SupplierName;
                currentDSHead.BusiType = editHead.BusiType;
            }
        }

        /// <summary>
        ///  切换批次绑定数据源
        /// </summary>
        /// <param name="drugID">选定药品ID</param>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void ChangeBatchDrug(int drugID, string frmName)
        { 
            /*
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
           
            if (frmName == "FrmInStoreDW")
            {
                frmInstoreDetailDW.BindDrugBatchCard(dtCurrentBatchNO);
            }
            else
            {
                frmInstoreDetailDS.BindDrugBatchCard(dtCurrentBatchNO);
            }*/
        }

        /// <summary>
        /// 新增明细时刷新批次showCard数据
        /// </summary>
        /// <param name="frmName"></param>
        [WinformMethod]
        public void RefushBatchDrug(string frmName)
        {
            /*
            if (frmName == "FrmInStoreDW")
            {
                frmInstoreDetailDW.BindDrugBatchCard(dtBatchInfo);
            }
            else
            {
                frmInstoreDetailDS.BindDrugBatchCard(dtBatchInfo);
            }*/
        }

        /// <summary>
        ///  打开采购计划单选择窗体
        /// </summary>
        /// <returns>弹窗</returns>
        [WinformMethod]
        public DialogResult OpenBuyBillDialog()
        {
            var dialog = iBaseView["FrmBuyBillChose"] as FrmBuyBillChose;
            if (dialog == null)
            {
                return DialogResult.None;
            }

            return dialog.ShowDialog();
        }

        /// <summary>
        ///  查询采购计划单表头数据
        /// </summary>
        [WinformMethod]
        public void GetPlanHeadData()
        {
            DateTime dt = DateTime.Now;

            //本月第一天时间    
            DateTime dtFirst = dt.AddDays(-(dt.Day) + 1);

            //将本月月数+1  
            DateTime dt2 = dt.AddMonths(1);

            //本月最后一天时间  
            DateTime dtLast = dt2.AddDays(-(dt.Day));
            string beginDate = string.Empty;
            string endDate = string.Empty;
            beginDate = dtFirst.ToString("yyyy-MM-dd 00:00:00");
            endDate = dtLast.ToString("yyyy-MM-dd 23:59:59");
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            queryCondition.Add("a.DeptID", selectedDeptID.ToString());
            queryCondition.Add(string.Empty, "a.PlanDate between '" + beginDate + "' and '" + endDate + "'");
            queryCondition.Add("a.AuditFlag", "1");//审核状态
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(DGConstant.OP_DW_STOCKPLAN);//采购计划
                request.AddData(queryCondition);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "PurchaseController", "GetPlanHeadData", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            iFrmBuyBillChose.BindPlanHeadGrid(dtRtn);
        }

        /// <summary>
        ///  查询采购计划单明细数据
        /// </summary>
        [WinformMethod]
        public void GetPlanDetailData()
        {
            Dictionary<string, string> headInfo = null;
            headInfo = iFrmBuyBillChose.GetCurrentHeadID();
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(DGConstant.OP_DW_STOCKPLAN);//采购计划明细
                request.AddData(headInfo);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "PurchaseController", "GetPlanDetailData", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);

            iFrmBuyBillChose.BindPlanDetailGrid(dtRtn);
        }

        /// <summary>
        /// 采购计划入库
        /// </summary>
        /// <param name="dtBuyPlanDetail">采购计划明细数据集</param>
        [WinformMethod]
        public void ConvertBuyToInStore(DataTable dtBuyPlanDetail)
        {
            FrmBuyBillChose frm = iBaseView["FrmBuyBillChose"] as FrmBuyBillChose;
            frm.Close();
            frmInstoreDetailDW.ConvertBuyToInStore(dtBuyPlanDetail);
        }
        #endregion

        #region 报表部分
        /// <summary>
        /// 打印采购计划
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="dthead">采购计划头表数据集</param>
        /// <param name="dtDetails">采购计划明细数据集</param>
        [WinformMethod]
        public void Print(string frmName, DataRow dthead, DataTable dtDetails)
        {
            if (dthead != null)
            {
                if (dtDetails.Rows.Count > 0)
                {
                    SetReportTable(frmName, dthead, dtDetails);
                }
                else
                {
                    MessageBoxShowSimple("没有明细数据");
                }
            }
        }

        /// <summary>
        /// 获取入库打印数据
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetInstoreReport(string frmName)
        {
            if (frmName == "FrmInStoreDS")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_INSTORE);
                    request.AddData(frmInStoreDS.AndWhere);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "GetInstoreReport", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_INSTORE);
                    request.AddData(frmInStoreDW.AndWhere);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "GetInstoreReport", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
            }
        }

        /// <summary>
        /// 设置报表数据
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="head">行对象</param>
        /// <param name="dt">数据源</param>
        public void SetReportTable(string frmName, DataRow head, DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                Dictionary<string, object> fields = new Dictionary<string, object>();
                fields.Add("TotalFee", (object)head["RetailFee"].ToString());
                fields.Add("InstoreFee", (object)head["StockFee"].ToString());
                fields.Add("ExeTime", (object)head["RegTime"].ToString());
                fields.Add("DiffFee", Convert.ToDecimal(head["RetailFee"].ToString()) - Convert.ToDecimal(head["StockFee"].ToString()));
                fields.Add("BillNo", (object)head["BillNo"].ToString());
                fields.Add("DeliverNo", (object)head["DeliveryNo"].ToString());
                fields.Add("Supporter", (object)head["SupplierName"].ToString());
                fields.Add("CurrentDept", (object)head["Name"].ToString());
                fields.Add("CurrentUser", (object)LoginUserInfo.EmpName);
                fields.Add("HospitalName", LoginUserInfo.WorkName + "药品入库单据");
                if (frmName == "FrmInStoreDS")
                {
                    EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 4007, 0, fields, dt).PrintPreview(true);
                }
                else
                {
                    EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 4008, 0, fields, dt).PrintPreview(true);
                }
            }
            else
            {
                MessageBoxShowSimple("没有报表数据");
            }
        }
        #endregion
    }
}
