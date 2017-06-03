using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManage.Winform.Controller
{
    /// <summary>
    /// 出库处理控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmOutStoreDS")]//在菜单上显示

    [WinformView(Name = "FrmOutStoreDW", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmOutStore")]//药房出库-药库
    [WinformView(Name = "FrmOutStoreDS", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmOutStore")]//药品出库-药房

    [WinformView(Name = "FrmOutStoreDetailDW", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmOutStoreDetail")]//药品入库-药房
    [WinformView(Name = "FrmOutStoreDetailDS", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmOutStoreDetail")]//药品入库-药库

    [WinformView(Name = "FrmSelectBillNo", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmSelectBillNo")]//药品入库-出库

    public class OutStoreController : WcfClientController
    {
        /// <summary>
        /// 药房出库对象
        /// </summary>
        IFrmOutStore frmOutStoreDS;

        /// <summary>
        /// 药库出库对象
        /// </summary>
        IFrmOutStore frmOutStoreDW;

        /// <summary>
        /// 药库出库详情对象
        /// </summary>
        IFrmOutstoreDetail frmOutstoreDetailDW;

        /// <summary>
        /// 药房出库详情对象
        /// </summary>
        IFrmOutstoreDetail frmOutstoreDetailDS;

        #region 私有变量

        /// <summary>
        /// 当前选中科室
        /// </summary>
        private int selectedDeptID = 0;

        /// <summary>
        /// 药品批次信息表
        /// </summary>
        private DataTable dtBatchInfo;

        /// <summary>
        /// 当前药库出库表头对象
        /// </summary>
        private DW_OutStoreHead currentDWHead;

        /// <summary>
        /// 网格编辑状态对象
        /// </summary>
        private DGEnum.BillEditStatus billStatus;

        /// <summary>
        /// 当前编辑药库单据明细列表
        /// </summary>
        private DataTable currentDWDetails;

        /// <summary>
        /// 当前药房出库表头对象
        /// </summary>
        private DS_OutStoreHead currentDSHead;

        /// <summary>
        /// 当前药房详情数据源
        /// </summary>
        private DataTable currentDSDetails;

        #endregion

        /// <summary>
        /// 初始化函数
        /// </summary>
        public override void Init()
        {
            frmOutStoreDS = (IFrmOutStore)iBaseView["FrmOutStoreDS"];
            frmOutStoreDW = (IFrmOutStore)iBaseView["FrmOutStoreDW"];
            frmOutstoreDetailDW = (IFrmOutstoreDetail)iBaseView["FrmOutStoreDetailDW"];
            frmOutstoreDetailDS = (IFrmOutstoreDetail)iBaseView["FrmOutStoreDetailDS"];
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
        /// 操作科室
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDrugDept(string frmName)
        {
            if (frmName == "FrmOutStoreDW")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(1);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "GetDrugDeptList", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                frmOutStoreDW.BindDrugDept(dt);
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(0);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "GetDrugDeptList", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                frmOutStoreDS.BindDrugDept(dt);
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
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                dtOpType.Rows.Add(new object[2] { DGConstant.OP_DW_CIRCULATEOUT, "流通出库类型" });
                dtOpType.Rows.Add(new object[2] { DGConstant.OP_DW_DEPTDRAW, "内耗出库业务类型" });
                dtOpType.Rows.Add(new object[2] { DGConstant.OP_DW_REPORTLOSS, "报损出库业务类型" });
                dtOpType.Rows.Add(new object[2] { DGConstant.OP_DW_RETURNSTORE, "退库" });
                if (frmName == "FrmOutStoreDW")
                {
                    frmOutStoreDW.BindOpType(dtOpType);
                }
                else
                {
                    frmOutstoreDetailDW.BindOpType(dtOpType);
                }
            }
            else if (belongSys == DGConstant.OP_DS_SYSTEM)
            {
                dtOpType.Rows.Add(new object[2] { DGConstant.OP_DS_DEPTDRAW, "内耗出库" });

                //dtOpType.Rows.Add(new object[2] { DGConstant.OP_DS_REPORTLOSS, "报损出库" });
                if (frmName == "FrmOutStoreDS")
                {
                    frmOutStoreDS.BindOpType(dtOpType);
                }
                else
                {
                    frmOutstoreDetailDS.BindOpType(dtOpType);
                }
            }
        }

        /// <summary>
        /// 科室的往来科室
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="deptType">科室类型</param>
        [WinformMethod]
        public void GetDrugRelateDept(string frmName, string deptType)
        {
            List<Tuple<string, string, SqlOperator>> andWhere = new List<Tuple<string, string, SqlOperator>>();
            List<Tuple<string, string, SqlOperator>> orWhere = new List<Tuple<string, string, SqlOperator>>();
            andWhere.Add(Tuple.Create("DrugDeptID", selectedDeptID.ToString(), SqlOperator.Equal));
            andWhere.Add(Tuple.Create("WorkID", LoginUserInfo.WorkId.ToString(), SqlOperator.Equal));

            //根据业务类型不同 加载不同的科室信息
            switch (deptType)
            {
                case "121":

                    //流通出库到药房
                    andWhere.Add(Tuple.Create("RelationDeptType", "1", SqlOperator.Equal));
                    break;
                case "122":

                    //内耗出库到科室
                    andWhere.Add(Tuple.Create("RelationDeptType", "2", SqlOperator.Equal));
                    break;
                case "124":

                    //退库 
                    andWhere.Add(Tuple.Create("RelationDeptType", "1", SqlOperator.Equal));
                    break;
                case "123":

                    //报损
                    break;
                case DGConstant.OP_DS_DEPTDRAW://药房内耗出库
                    andWhere.Add(Tuple.Create("RelationDeptType", "2", SqlOperator.Equal));
                    break;
                default:
                    break;
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
               {
                   request.AddData(andWhere);
                   request.AddData(orWhere);
               });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "RelateDeptController", "GetRelateDeptDataByCon", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            switch (frmName)
            {
                case "FrmOutStoreDetailDS":
                    frmOutstoreDetailDS.BindDept(dt);
                    break;
                case "FrmOutStoreDS":
                    frmOutStoreDS.BindDept(dt);
                    break;
                case "FrmOutStoreDW":
                    frmOutStoreDW.BindDept(dt);
                    break;
                case "FrmOutStoreDetailDW":
                    frmOutstoreDetailDW.BindDept(dt);
                    break;
            }
        }

        /// <summary>
        /// 获取网格编辑状态
        /// </summary>
        /// <returns>返回网格编辑状态</returns>
        [WinformMethod]
        public DGEnum.BillEditStatus GetBillEditStatus()
        {
            return billStatus;
        }

        /// <summary>
        /// 出库单 可以添加的药品 showcard数据
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetOutStoreDrugInfo(string frmName)
        {
            RefreshHead(frmName);

            //药库
            if (frmName == "FrmOutStoreDetailDW")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(selectedDeptID);
                    request.AddData(currentDWHead.BusiType);
                    request.AddData(currentDWHead.ToDeptID);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "OutStoreController", "GetDeptOutDrug", requestAction);
                frmOutstoreDetailDW.BindDrugInfoCard(retdata.GetData<DataTable>(0));
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(selectedDeptID);
                    request.AddData(currentDSHead.BusiType);
                    request.AddData(currentDSHead.ToDeptID);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "OutStoreController", "GetDeptOutDrug", requestAction);
                frmOutstoreDetailDS.BindDrugInfoCard(retdata.GetData<DataTable>(0));
            }
        }

        /// <summary>
        /// 药品批次信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDrugBatchInfo(string frmName)
        {
            if (frmName == "FrmOutStoreDetailDW")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(selectedDeptID);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "GetInStoreBatchInfo", requestAction);
                dtBatchInfo = retdata.GetData<DataTable>(0);
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
                //药房出库
                case "FrmOutStoreDS":
                    headInfo = frmOutStoreDS.GetCurrentHeadID();
                    if (headInfo != null)
                    {
                        requestAction = ((ClientRequestData request) =>
                        {
                            request.AddData(DGConstant.OP_DS_DEPTDRAW);
                            request.AddData(headInfo);
                        });
                        retdata = InvokeWcfService("DrugProject.Service", "OutStoreController", "LoadBillDetails", requestAction);
                        frmOutStoreDS.BindDeatailGrids(retdata.GetData<DataTable>(0));
                    }
                    else
                        frmOutStoreDS.BindDeatailGrids(null);
                    break;

                //药库出库
                case "FrmOutStoreDW":
                    headInfo = frmOutStoreDW.GetCurrentHeadID();
                    if (headInfo != null)
                    {
                        headInfo.Add("deptId", selectedDeptID.ToString());
                        requestAction = ((ClientRequestData request) =>
                        {
                            request.AddData(DGConstant.OP_DW_DEPTDRAW);
                            request.AddData(headInfo);
                        });
                        retdata = InvokeWcfService("DrugProject.Service", "OutStoreController", "LoadBillDetails", requestAction);
                        frmOutStoreDW.BindDeatailGrids(retdata.GetData<DataTable>(0));
                    }
                    else
                        frmOutStoreDW.BindDeatailGrids(null);
                    break;

                //药库出库明细
                case "FrmOutStoreDetailDW":
                    headInfo = new Dictionary<string, string>();
                    headInfo.Add("OutHeadID", currentDWHead.OutStoreHeadID.ToString());
                    requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(DGConstant.OP_DW_DEPTDRAW);//出库
                        request.AddData(headInfo);
                    });
                    retdata = InvokeWcfService("DrugProject.Service", "OutStoreController", "LoadBillDetails", requestAction);
                    currentDWDetails = retdata.GetData<DataTable>(0);
                    frmOutstoreDetailDW.BindDetailsGrid(currentDWDetails);
                    break;

                //药房出库明细
                case "FrmOutStoreDetailDS":
                    headInfo = new Dictionary<string, string>();
                    headInfo.Add("OutHeadID", currentDSHead.OutStoreHeadID.ToString());
                    requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(DGConstant.OP_DS_DEPTDRAW);
                        request.AddData(headInfo);
                    });
                    retdata = InvokeWcfService("DrugProject.Service", "OutStoreController", "LoadBillDetails", requestAction);
                    currentDSDetails = retdata.GetData<DataTable>(0);
                    frmOutstoreDetailDS.BindDetailsGrid(currentDSDetails);
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
        /// <returns>是否获取到数据</returns>
        [WinformMethod]
        public bool InitBillHead(string frmName, DGEnum.BillEditStatus billEditStatus, int currentBillID)
        {
            if (LoginUserInfo.DeptId == 0)
            {
                MessageBoxShowSimple("用户没有选择登录科室");
                return false;
            }

            billStatus = billEditStatus;
            if (frmName == "FrmOutStoreDW")
            {
                if (billStatus == DGEnum.BillEditStatus.ADD_STATUS)
                {
                    DW_OutStoreHead outHead = new DW_OutStoreHead();
                    outHead.AuditFlag = 0;
                    outHead.AuditEmpID = 0;
                    outHead.AuditEmpName = string.Empty;
                    outHead.Remark = string.Empty;
                    outHead.DeptID = selectedDeptID;
                    outHead.BusiType = DGConstant.OP_DW_DEPTDRAW;//默认内耗流通出库
                    outHead.BillTime = System.DateTime.Now;
                    outHead.RegTime = System.DateTime.Now;
                    outHead.RegEmpID = GetUserInfo().EmpId;
                    outHead.RegEmpName = GetUserInfo().EmpName;
                    currentDWHead = outHead;
                    frmOutstoreDetailDW.BindInHeadInfo(currentDWHead);
                }
                else
                {
                    Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(DGConstant.OP_DW_SYSTEM);
                        request.AddData(currentBillID);
                    });
                    ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "OutStoreController", "GetEditBillHead", requestAction);
                    DW_OutStoreHead inStoreHead = retdata.GetData<DW_OutStoreHead>(0);
                    currentDWHead = inStoreHead;
                    if (currentDWHead.ApplyHeadId != 0)
                    {
                        frmOutstoreDetailDW.IsApplyStatus = true;
                    }

                    frmOutstoreDetailDW.BindInHeadInfo(currentDWHead);
                }

                frmOutstoreDetailDW.InitControStatus(billEditStatus);
            }
            else
            {
                if (billStatus == DGEnum.BillEditStatus.ADD_STATUS)
                {
                    DS_OutStoreHead outHead = new DS_OutStoreHead();
                    outHead.AuditFlag = 0;
                    outHead.AuditEmpID = 0;
                    outHead.AuditEmpName = string.Empty;
                    outHead.Remark = string.Empty;
                    outHead.DeptID = selectedDeptID;
                    outHead.BusiType = DGConstant.OP_DS_DEPTDRAW;//默认内耗流通出库
                    outHead.BillTime = System.DateTime.Now;
                    outHead.RegTime = System.DateTime.Now;
                    outHead.RegEmpID = GetUserInfo().EmpId;
                    outHead.RegEmpName = GetUserInfo().EmpName;
                    currentDSHead = outHead;
                    frmOutstoreDetailDS.BindInHeadInfo(currentDSHead);
                }
                else
                {
                    Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(DGConstant.OP_DS_SYSTEM);
                        request.AddData(currentBillID);
                    });
                    ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "OutStoreController", "GetEditBillHead", requestAction);
                    DS_OutStoreHead outStoreHead = retdata.GetData<DS_OutStoreHead>(0);
                    outStoreHead.DeptID = selectedDeptID;
                    currentDSHead = outStoreHead;
                    frmOutstoreDetailDS.BindInHeadInfo(currentDSHead);
                }

                frmOutstoreDetailDS.InitControStatus(billEditStatus);
            }

            return true;
        }

        /// <summary>
        /// 计算汇总金额并展示
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void ComputeTotalFee(string frmName)
        {
            if (frmName == "FrmOutStoreDetailDS")
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

                    frmOutstoreDetailDS.ShowTotalFee(totalStockFee, totalRetailFee);
                }
            }

            if (frmName == "FrmOutStoreDetailDW")
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

                    frmOutstoreDetailDW.ShowTotalFee(totalStockFee, totalRetailFee);
                }
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
            if (frmName == "FrmOutStoreDetailDW")
            {
                currentDWHead.StockFee = 0;
                currentDWHead.RetailFee = 0;
                List<DW_OutStoreDetail> lstDetails = new List<DW_OutStoreDetail>();
                for (int index = 0; index < currentDWDetails.Rows.Count; index++)
                {
                    DW_OutStoreDetail detail = ConvertExtend.ToObject<DW_OutStoreDetail>(currentDWDetails, index);
                    var pAmount = 0;
                    if (currentDWDetails.Rows[index]["pAmount"] != DBNull.Value)
                    {
                        pAmount = Convert.ToInt32(currentDWDetails.Rows[index]["pAmount"].ToString());
                    }

                    detail.Amount = pAmount;
                    detail.DeptID = currentDWHead.DeptID;
                    detail.ToDeptID = currentDWHead.ToDeptID;
                    currentDWHead.StockFee += detail.StockFee;
                    currentDWHead.RetailFee += detail.RetailFee;
                    lstDetails.Add(detail);
                }

                requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(currentDWHead.BusiType);
                    request.AddData(currentDWHead);
                    request.AddData<List<DW_OutStoreDetail>>(lstDetails);
                    request.AddData<List<int>>(frmOutstoreDetailDW.GetDeleteDetails());
                });

                if (frmOutstoreDetailDW.IsApplyStatus == true)
                {
                    retdata = InvokeWcfService("DrugProject.Service", "OutStoreController", "SaveBillFromApply", requestAction);
                }
                else
                {
                    retdata = InvokeWcfService("DrugProject.Service", "OutStoreController", "SaveBill", requestAction);
                }

                DGBillResult result = retdata.GetData<DGBillResult>(0);
                if (result.Result == 0)
                {
                    frmOutstoreDetailDW.IsApplyStatus = false;
                    MessageBoxShowSimple("单据已经保存成功，如果没有配置自动审核.请及时审核单据");
                    if (billStatus == DGEnum.BillEditStatus.ADD_STATUS)
                    {
                        frmOutstoreDetailDW.NewBillClear();
                    }
                    else
                    {
                        frmOutstoreDetailDW.CloseCurrentWindow();
                    }
                }
                else
                {
                    MessageBoxShowSimple("单据保存失败:" + result.ErrMsg);
                }
            }
            else
            {
                currentDSHead.StockFee = 0;
                currentDSHead.RetailFee = 0;
                List<DS_OutStoreDetail> lstDetails = new List<DS_OutStoreDetail>();
                for (int index = 0; index < currentDSDetails.Rows.Count; index++)
                {
                    DS_OutStoreDetail detail = ConvertExtend.ToObject<DS_OutStoreDetail>(currentDSDetails, index);

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

                    var packAmount = currentDSDetails.Rows[index]["packAmount"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(currentDSDetails.Rows[index]["packAmount"]);

                    detail.Amount = uAmount + (pAmount * packAmount);

                    detail.DeptID = currentDSHead.DeptID;
                    detail.ToDeptID = currentDSHead.ToDeptID;
                    currentDSHead.StockFee += detail.StockFee;
                    currentDSHead.RetailFee += detail.RetailFee;
                    lstDetails.Add(detail);
                }

                requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(currentDSHead.BusiType);
                    request.AddData(currentDSHead);
                    request.AddData<List<DS_OutStoreDetail>>(lstDetails);
                    request.AddData<List<int>>(frmOutstoreDetailDS.GetDeleteDetails());
                });
                retdata = InvokeWcfService("DrugProject.Service", "OutStoreController", "SaveBill", requestAction);

                DGBillResult result = retdata.GetData<DGBillResult>(0);
                if (result.Result == 0)
                {
                    MessageBoxShowSimple("单据已经保存成功，如果没有配置自动审核.请及时审核单据");
                    if (billStatus == DGEnum.BillEditStatus.ADD_STATUS)
                    {
                        frmOutstoreDetailDS.NewBillClear();
                    }
                    else
                    {
                        frmOutstoreDetailDS.CloseCurrentWindow();
                    }
                }
                else
                {
                    MessageBoxShowSimple("单据保存失败:" + result.ErrMsg);
                }
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
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "OutStoreController", "DeleteBill", requestAction);
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
        /// 查询单据表头
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void LoadBillHead(string frmName)
        {
            if (frmName == "FrmOutStoreDS")
            {
                var dic =
                frmOutStoreDS.GetQueryCondition(selectedDeptID);
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_DEPTDRAW);
                    request.AddData(dic);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "OutStoreController", "LoadBillHead", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmOutStoreDS.BindHeadGrid(dtRtn);
            }
            else
            {
                var dic =
             frmOutStoreDW.GetQueryCondition(selectedDeptID);
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_DEPTDRAW);
                    request.AddData(dic);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "OutStoreController", "LoadBillHead", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmOutStoreDW.BindHeadGrid(dtRtn);
            }
        }

        /// <summary>
        /// 从界面信息更新单据头
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void RefreshHead(string frmName)
        {
            if (frmName == "FrmOutStoreDetailDW")
            {
                if (billStatus == DGEnum.BillEditStatus.ADD_STATUS)
                {
                    DW_OutStoreHead editHead = frmOutstoreDetailDW.GetInHeadInfoDW();
                    currentDWHead.BillTime = editHead.BillTime;
                    currentDWHead.ToDeptID = editHead.ToDeptID;
                    currentDWHead.Remark = editHead.Remark;
                    currentDWHead.BusiType = editHead.BusiType;
                    currentDWHead.LostReason = editHead.LostReason;
                    currentDWHead.ApplyHeadId = editHead.ApplyHeadId;
                }
            }
            else
            {
                if (billStatus == DGEnum.BillEditStatus.ADD_STATUS)
                {
                    DS_OutStoreHead editHead = frmOutstoreDetailDS.GetHeadInfoDS();
                    currentDSHead.BillTime = editHead.BillTime;
                    currentDSHead.ToDeptID = editHead.ToDeptID;
                    currentDSHead.Remark = editHead.Remark;
                    currentDSHead.BusiType = editHead.BusiType;
                }
            }
        }

        /// <summary>
        /// 单据审核
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="billID">单据ID</param>
        /// <param name="busiType">业务类型</param>
        /// <param name="deptID">科室ID</param>
        [WinformMethod]
        public void AuditBill(string frmName, int billID, string busiType, int deptID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(busiType);
                request.AddData(billID);
                request.AddData(LoginUserInfo.EmpId);
                request.AddData(LoginUserInfo.EmpName);
                request.AddData(deptID);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "OutStoreController", "AuditBill", requestAction);
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
        /// 入库单据转出库单
        /// </summary>
        /// <param name="billNo">入库单据号</param>
        [WinformMethod]
        public void ConvertDwOutFromDwIn(string billNo)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(billNo);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "OutStoreController", "ConvertDwOutFromDwIn", requestAction);
            currentDWDetails = retdata.GetData<DataTable>(0);
            if (currentDWDetails.Rows.Count != 0)
            {
                frmOutstoreDetailDW.BindDetailsGrid(currentDWDetails);
            }
            else
            {
                MessageBoxShowSimple("此单号没有数据");
            }

            var form = iBaseView["FrmSelectBillNo"] as Form;
            if (form != null)
            {
                form.Close();
            }
        }

        /// <summary>
        /// 打印数据
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="dthead">盘点药品数据</param>
        /// <param name="dtDetails">详情数据集</param>
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
        /// 打印
        /// </summary>
        /// <param name="dthead">表头数据源</param>
        /// <param name="dtDetails">详情数据源</param>
        [WinformMethod]
        public void Print(DataTable dthead, DataTable dtDetails)
        {
            if (dtDetails.Rows.Count > 0)
            {
                Dictionary<string, object> fields = new Dictionary<string, object>();
                fields.Add("HospitalName", LoginUserInfo.WorkName);
                fields.Add("RegPeople", LoginUserInfo.EmpName);
                fields.Add("BillNo", (object)dthead.Rows[0]["BillNO"].ToString());
                fields.Add("Remark", (object)dthead.Rows[0]["Remark"].ToString());
                fields.Add("TotalRetailFee", (object)dthead.Rows[0]["RetailFee"].ToString());
                fields.Add("TotalTradeFee", (object)dthead.Rows[0]["StockFee"].ToString());
                fields.Add("TotalDiffFee", (object)dthead.Rows[0]["DiffMoney"].ToString());
                fields.Add("ExeTime", (object)dthead.Rows[0]["RegTime"].ToString());
                fields.Add("OutDept", (object)dthead.Rows[0]["Name"].ToString());
                fields.Add("CurrentDept", (object)dthead.Rows[0]["CurrentDept"].ToString());

                // EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, reportNo, 0, myDictionary, dtDetails).Print(true);
            }
            else
            {
                MessageBoxShowSimple("请先提取数据");
            }
        }

        /// <summary>
        /// 设置报表打印数据源
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="head">表头行对象</param>
        /// <param name="dt">数据源</param>
        public void SetReportTable(string frmName, DataRow head, DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                Dictionary<string, object> fields = new Dictionary<string, object>();
                fields.Add("HospitalName", LoginUserInfo.WorkName);
                fields.Add("RegPeople", LoginUserInfo.EmpName);
                fields.Add("BillNo", (object)head["BillNO"].ToString());
                fields.Add("Remark", (object)head["Remark"].ToString());
                fields.Add("TotalRetailFee", (object)head["RetailFee"].ToString());
                fields.Add("TotalTradeFee", (object)head["StockFee"].ToString());
                fields.Add("TotalDiffFee", (object)head["DiffMoney"].ToString());
                fields.Add("ExeTime", (object)head["RegTime"].ToString());
                fields.Add("OutDept", (object)head["Name"].ToString());
                fields.Add("CurrentDept", (object)head["CurrentDept"].ToString());
                fields.Add("LostReason", (object)head["LostReason"].ToString());
                string busiType = head["BusiType"].ToString();
                if (frmName == "FrmOutStoreDS")
                {
                    EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 4010, 0, fields, dt).PrintPreview(true);
                }
                else
                {
                    if (busiType == "123")
                    {
                        EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 4011, 0, fields, dt).PrintPreview(true);
                    }
                    else
                    {
                        EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 4012, 0, fields, dt).PrintPreview(true);
                    }
                }
            }
            else
            {
                MessageBoxShowSimple("没有报表数据");
            }
        }

        #region  申请单转出库单
        /// <summary>
        /// 申请单转出库单
        /// </summary>
        [WinformMethod]
        public void InvokeController()
        {
            InvokeController("DrugProject.UI", "ApplyStoreController", "Show", selectedDeptID);
        }

        /// <summary>
        /// 申请单 转入库单 
        /// </summary>
        /// <param name="rowHead">申请单表头信息</param>
        /// <param name="dt">出库单明细数据</param>
        [WinformMethod]
        public void BindDataTable(DataRow rowHead, DataTable dt)
        {
            frmOutstoreDetailDW.SetHeadValue(rowHead);
            currentDWDetails = dt;
            frmOutstoreDetailDW.IsApplyStatus = true;
            frmOutstoreDetailDW.BindDetailsGrid(dt);
            frmOutstoreDetailDW.SetGridExpress();
        }

        #endregion
    }
}
