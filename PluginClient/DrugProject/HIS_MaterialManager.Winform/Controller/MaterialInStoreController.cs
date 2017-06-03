using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.DrugManage;
using HIS_Entity.MaterialManage;
using HIS_MaterialManage.Winform.IView.StoreMgr;

namespace HIS_MaterialManage.Winform.Controller
{
    /// <summary>
    /// 入库处理控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmInStore")]//在菜单上显示
    [WinformView(Name = "FrmInStore", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmInStore")]//
    [WinformView(Name = "FrmInStoreDetail", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmInStoreDetail")]
    public class MaterialInStoreController : WcfClientController
    {
        /// <summary>
        /// 物资入库接口
        /// </summary>
        IFrmInStore iFrmInstore;

        /// <summary>
        /// 入库单明细窗体
        /// </summary>
        IFrmInstoreDetail ifrmInstoreDetail;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            iFrmInstore = (IFrmInStore)iBaseView["FrmInStore"];
            ifrmInstoreDetail = (IFrmInstoreDetail)iBaseView["FrmInStoreDetail"];
        }

        #region 私有变量
        /// <summary>
        /// 当前单据编辑状态
        /// </summary>
        private MWEnum.BillEditStatus billStatus;

        /// <summary>
        /// 药品批次信息表
        /// </summary>
        private DataTable dtBatchInfo;

        /// <summary>
        /// 当前选中科室
        /// </summary>
        private int selectedDeptID = 0;

        /// <summary>
        /// 当前编辑药库单据头实体
        /// </summary>
        private MW_InStoreHead currentHead;

        /// <summary>
        /// 当前编辑药库单据明细列表
        /// </summary>
        private DataTable currentDetails;

        #endregion

        /// <summary>
        /// 获取供应商列表绑定ShowCard
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void GetSupplyForShowCard(string frmName)
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialInStoreController", "GetSupplyForShowCard");
            DataTable dt = retdata.GetData<DataTable>(0);
            switch (frmName)
            {
                case "FrmInStore":
                    iFrmInstore.BindSupply(dt);
                    break;
                case "FrmInStoreDetail":
                    ifrmInstoreDetail.BindSupply(dt);
                    break;
                default:
                    break;
            }
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
        /// 构建药品业务类型数据源
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void BuildOpType(string frmName)
        {
            DataTable dtOpType = new DataTable();
            dtOpType.Columns.Add("opType");
            dtOpType.Columns.Add("opTypeName");
            dtOpType.Rows.Add(new object[2] { MWConstant.OP_MW_FIRSTIN, "期初入库" });
            dtOpType.Rows.Add(new object[2] { MWConstant.OP_MW_BUYINSTORE, "采购入库" });
            dtOpType.Rows.Add(new object[2] { MWConstant.OP_MW_BACKSTORE, "物资退货" });
            if (frmName == "FrmInStore")
            {
                iFrmInstore.BindOpType(dtOpType);
            }
            else
            {
                ifrmInstoreDetail.BindOpType(dtOpType);
            }
        }

        /// <summary>
        /// 获取物资库科室列表
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void GetDrugDept(string frmName)
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialInStoreController", "GetDrugDeptList");
            DataTable dt = retdata.GetData<DataTable>(0);
            iFrmInstore.BindDrugDept(dt);
        }

        /// <summary>
        /// 计算汇总金额并展示
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void ComputeTotalFee(string frmName)
        {
            if (currentDetails != null)
            {
                decimal totalRetailFee = 0;
                decimal totalStockFee = 0;
                for (int index = 0; index < currentDetails.Rows.Count; index++)
                {
                    DataRow dRow = currentDetails.Rows[index];
                    totalRetailFee += (dRow["RetailFee"] == DBNull.Value ? 0 : Convert.ToDecimal(dRow["RetailFee"]));
                    totalStockFee += (dRow["StockFee"] == DBNull.Value ? 0 : Convert.ToDecimal(dRow["StockFee"]));
                }

                ifrmInstoreDetail.ShowTotalFee(totalStockFee, totalRetailFee);
            }
        }

        /// <summary>
        /// 切换批次绑定数据源
        /// </summary>
        /// <param name="materialID">选定药品ID</param>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void ChangeBatchDrug(int materialID, string frmName)
        {
            DataTable dtCurrentBatchNO = null;
            DataRow[] selectRows = dtBatchInfo.Select("MaterialID=" + materialID.ToString());
            if (selectRows.Length > 0)
            {
                dtCurrentBatchNO = selectRows.CopyToDataTable();
            }
            else
            {
                dtCurrentBatchNO = dtBatchInfo.Clone();
            }

            ifrmInstoreDetail.BindMaterialBatchCard(dtCurrentBatchNO);
        }

        /// <summary>
        /// 获取物资参数
        /// </summary>
        [WinformMethod]
        public void GetDeptParameters()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(0);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialPramentController", "GetPublicParameters", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            ifrmInstoreDetail.BindDeptParameters(dt);
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
            currentHead.StockFee = 0;
            currentHead.RetailFee = 0;
            List<MW_InStoreDetail> lstDetails = new List<MW_InStoreDetail>();
            for (int index = 0; index < currentDetails.Rows.Count; index++)
            {
                MW_InStoreDetail detail = ConvertExtend.ToObject<MW_InStoreDetail>(currentDetails, index);
                decimal pAmount = 0;
                if (currentDetails.Rows[index]["pAmount"] != DBNull.Value)
                {
                    pAmount = Convert.ToDecimal(currentDetails.Rows[index]["pAmount"].ToString());
                }

                detail.Amount = pAmount;
                currentHead.StockFee += detail.StockFee;
                currentHead.RetailFee += detail.RetailFee;
                lstDetails.Add(detail);
            }

            requestAction = ((ClientRequestData request) =>
            {
                request.AddData(MWConstant.MW_IN_SYSTEM);
                request.AddData(currentHead.BusiType);
                request.AddData(currentHead);
                request.AddData<List<MW_InStoreDetail>>(lstDetails);
                request.AddData<List<int>>(ifrmInstoreDetail.GetDeleteDetails());
            });

            retdata = InvokeWcfService("DrugProject.Service", "MaterialInStoreController", "SaveBill", requestAction);
            DGBillResult result = retdata.GetData<DGBillResult>(0);
            if (result.Result == 0)
            {
                MessageBoxShowSimple("单据已经保存成功，如果没有配置自动审核.请及时审核单据");
                if (billStatus == MWEnum.BillEditStatus.ADD_STATUS)
                {
                    ifrmInstoreDetail.NewBillClear();
                }
                else
                {
                    ifrmInstoreDetail.CloseCurrentWindow();
                }
            }
            else
            {
                string rtnMsg = result.ErrMsg;
                MessageBoxShowSimple("单据保存失败:" + result.ErrMsg);
            }
        }

        /// <summary>
        /// 从界面信息更新单据头
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void RefreshHead(string frmName)
        {
            MW_InStoreHead editHead = ifrmInstoreDetail.GetInHeadInfo();
            currentHead.InvoiceTime = editHead.InvoiceTime;
            currentHead.BillTime = editHead.BillTime;
            currentHead.InvoiceNo = editHead.InvoiceNo;
            currentHead.DeliveryNo = editHead.DeliveryNo;
            currentHead.SupplierID = editHead.SupplierID;
            currentHead.SupplierName = editHead.SupplierName;
            currentHead.BusiType = editHead.BusiType;
        }

        /// <summary>
        /// 初始化单据头实体信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="billEditStatus">单据编辑状态</param>
        /// <param name="currentBillID">当前单据ID</param>
        [WinformMethod]
        public void InitBillHead(string frmName, MWEnum.BillEditStatus billEditStatus, int currentBillID)
        {
            billStatus = billEditStatus;
            if (billStatus == MWEnum.BillEditStatus.ADD_STATUS)
            {
                MW_InStoreHead inHead = new MW_InStoreHead();
                inHead.AuditFlag = 0;
                inHead.AuditEmpID = 0;
                inHead.AuditEmpName = string.Empty;
                inHead.Remark = string.Empty;
                inHead.OpEmpID = LoginUserInfo.EmpId;
                inHead.DeptID = selectedDeptID;
                inHead.BusiType = DGConstant.OP_DW_BUYINSTORE;
                inHead.BillTime = System.DateTime.Now;
                inHead.RegEmpID = GetUserInfo().EmpId;
                inHead.RegEmpName = GetUserInfo().EmpName;
                currentHead = inHead;
                ifrmInstoreDetail.BindInHeadInfo(currentHead);
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(MWConstant.OP_MW_INSTORE);
                    request.AddData(currentBillID);
                });

                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialInStoreController", "GetEditBillHead", requestAction);
                MW_InStoreHead inStoreHead = retdata.GetData<MW_InStoreHead>(0);
                currentHead.DeptID = selectedDeptID;
                currentHead = inStoreHead;
                ifrmInstoreDetail.BindInHeadInfo(currentHead);
            }

            ifrmInstoreDetail.InitControStatus(billEditStatus);
        }

        /// <summary>
        /// 查询单据表头
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void LoadBillHead(string frmName)
        {
            //如果是药房入库
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(MWConstant.OP_MW_INSTORE);
                request.AddData(iFrmInstore.GetQueryCondition(selectedDeptID));
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialInStoreController", "LoadBillHead", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            iFrmInstore.BindInHeadGrid(dtRtn);
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
                //入库主表
                case "FrmInStore":
                    headInfo = iFrmInstore.GetCurrentHeadID();
                    if (headInfo != null)
                    {
                        requestAction = ((ClientRequestData request) =>
                        {
                            request.AddData(MWConstant.OP_MW_INSTORE);
                            request.AddData(headInfo);
                        });

                        retdata = InvokeWcfService("DrugProject.Service", "MaterialInStoreController", "LoadBillDetails", requestAction);
                        iFrmInstore.BindInDetailGrid(retdata.GetData<DataTable>(0));
                    }
                    else
                    {
                        iFrmInstore.BindInDetailGrid(null);
                    }

                    break;
                //入库明细
                case "FrmInStoreDetail":
                    headInfo = new Dictionary<string, string>();
                    headInfo.Add("InHeadID", currentHead.InHeadID.ToString());
                    requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(MWConstant.OP_MW_INSTORE);
                        request.AddData(headInfo);
                    });

                    retdata = InvokeWcfService("DrugProject.Service", "MaterialInStoreController", "LoadBillDetails", requestAction);
                    currentDetails = retdata.GetData<DataTable>(0);
                    ifrmInstoreDetail.BindInDetails(currentDetails);
                    break;
                default:
                    break;
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

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialInStoreController", "DeleteBill", requestAction);
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
        /// 单据审核
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
                request.AddData(LoginUserInfo.EmpId);
                request.AddData(LoginUserInfo.EmpName);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialInStoreController", "AuditBill", requestAction);
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
        /// 获取批次数据
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDrugBatchInfo(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(MWConstant.MW_IN_SYSTEM);
                request.AddData(selectedDeptID);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialInStoreController", "GetInStoreBatchInfo", requestAction);
            dtBatchInfo = retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 获取入库单药品数据
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetInStoreDrugInfo(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(MWConstant.MW_IN_SYSTEM);
                request.AddData(currentHead.BusiType);
                request.AddData(selectedDeptID);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialInStoreController", "GetInStoreDrugInfo", requestAction);
            ifrmInstoreDetail.BindMaterialInfoCard(retdata.GetData<DataTable>(0));
        }

        #region 报表
        /// <summary>
        /// 打印报表
        /// </summary>
        /// <param name="frmName">窗体入库</param>
        /// <param name="dthead">头部数据</param>
        /// <param name="dtDetails">明细数据</param>
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
        /// 报表内容设置
        /// </summary>
        /// <param name="frmName">窗体入库</param>
        /// <param name="head">头</param>
        /// <param name="dt">明细</param>
        public void SetReportTable(string frmName, DataRow head, DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                Dictionary<string, object> fields = new Dictionary<string, object>();
                fields.Add("TotalFee", (object)head["RetailFee"].ToString());
                fields.Add("InstoreFee", (object)head["StockFee"].ToString());
                fields.Add("ExeTime", (object)head["RegTime"].ToString());
                fields.Add(
                    "DiffFee",
                    Convert.ToDecimal(head["RetailFee"].ToString()) -
                    Convert.ToDecimal(head["StockFee"].ToString()));
                fields.Add("BillNo", (object)head["BillNo"].ToString());
                fields.Add("DeliverNo", (object)head["DeliveryNo"].ToString());
                fields.Add("Supporter", (object)head["SupplierName"].ToString());
                fields.Add("CurrentDept", (object)head["Name"].ToString());
                fields.Add("CurrentUser", (object)LoginUserInfo.EmpName);
                fields.Add("HospitalName", LoginUserInfo.WorkName + "物资入库单据");
                EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 5004, 0, fields, dt).PrintPreview(true);
            }
            else
            {
                MessageBoxShowSimple("没有报表数据");
            }
        }
        #endregion
    }
}