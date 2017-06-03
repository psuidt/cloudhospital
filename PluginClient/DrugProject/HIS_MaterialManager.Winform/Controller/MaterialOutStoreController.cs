using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.DrugManage;
using HIS_Entity.MaterialManage;
using HIS_Entity.SqlAly;
using HIS_MaterialManage.Winform.IView.StoreMgr;

namespace HIS_MaterialManage.Winform.Controller
{
    /// <summary>
    /// 出库处理控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmOutStore")]//在菜单上显示
    [WinformView(Name = "FrmOutStore", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmOutStore")]
    [WinformView(Name = "FrmOutStoreDetail", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmOutStoreDetail")]
    public class MaterialOutStoreController : WcfClientController
    {
        /// <summary>
        /// 出库单管理接口
        /// </summary>
        IFrmOutStore ifrmOutStore;

        /// <summary>
        /// 出库单明细接口
        /// </summary>
        IFrmOutstoreDetail ifrmOutstoreDetail;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            ifrmOutStore = (IFrmOutStore)iBaseView["FrmOutStore"];
            ifrmOutstoreDetail = (IFrmOutstoreDetail)iBaseView["FrmOutStoreDetail"];
        }

        #region 私有变量

        /// <summary>
        /// 当前选中科室
        /// </summary>
        private int selectedDeptID = 0;

        /// <summary>
        /// 出库单头
        /// </summary>
        private MW_OutStoreHead currentHead;

        /// <summary>
        /// 编辑状态
        /// </summary>
        private MWEnum.BillEditStatus billStatus;

        /// <summary>
        /// 当前编辑药库单据明细列表
        /// </summary>
        private DataTable currentDetails;

        #endregion

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
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void GetDrugDept(string frmName)
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialInStoreController", "GetDrugDeptList");
            DataTable dt = retdata.GetData<DataTable>(0);
            ifrmOutStore.BindDrugDept(dt);
        }

        /// <summary>
        /// 获取编辑状态
        /// </summary>
        /// <returns>编辑状态</returns>
        [WinformMethod]
        public MWEnum.BillEditStatus GetBillEditStatus()
        {
            return billStatus;
        }

        /// <summary>
        /// 构建物资业务类型数据源
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void BuildOpType(string frmName)
        {
            DataTable dtOpType = new DataTable();
            dtOpType.Columns.Add("opType");
            dtOpType.Columns.Add("opTypeName");
            dtOpType.Rows.Add(new object[2] { MWConstant.OP_MW_DEPTDRAW, "内耗出库" });
            dtOpType.Rows.Add(new object[2] { MWConstant.OP_MW_REPORTLOSS, "报损出库" });
            dtOpType.Rows.Add(new object[2] { MWConstant.OP_MW_RETURNSTORE, "物资库退库" });
            //dtOpType.Rows.Add(new object[2] { MWConstant.OP_MW_CIRCULATEOUT, "流通出库" });
            if (frmName == "FrmOutStore")
            {
                ifrmOutStore.BindOpType(dtOpType);
            }
            else
            {
                ifrmOutstoreDetail.BindOpType(dtOpType);
            }
        }

        /// <summary>
        /// 科室的往来科室
        /// </summary>
        /// <param name="frmName">窗体入口</param>
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
                case MWConstant.OP_MW_DEPTDRAW://内耗出库到科室
                    andWhere.Add(Tuple.Create("RelationDeptType", "2", SqlOperator.Equal));
                    break;
                case MWConstant.OP_MW_RETURNSTORE://退库 
                    andWhere.Add(Tuple.Create("RelationDeptType", "2", SqlOperator.Equal));
                    break;
                case MWConstant.OP_MW_REPORTLOSS://报损
                    break;
                default:
                    break;
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(andWhere);
                request.AddData(orWhere);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialRelateDeptController", "GetRelateDeptDataByCon", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            switch (frmName)
            {
                case "FrmOutStore":
                    ifrmOutStore.BindDept(dt);
                    break;
                case "FrmOutStoreDetail":
                    ifrmOutstoreDetail.BindDept(dt);
                    break;
            }
        }

        /// <summary>
        /// 出库单 可以添加的物资 showcard数据
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void GetOutStoreDrugInfo(string frmName)
        {
            RefreshHead(frmName);
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(selectedDeptID);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialOutStoreController", "GetDeptOutMW", requestAction);
            ifrmOutstoreDetail.BindMaterialInfoCard(retdata.GetData<DataTable>(0));
        }

        /// <summary>
        /// 初始化单据头实体信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="billEditStatus">单据编辑状态</param>
        /// <param name="currentBillID">当前单据ID</param>
        /// <returns>单据编辑装态</returns>
        [WinformMethod]
        public bool InitBillHead(
            string frmName,
            MWEnum.BillEditStatus billEditStatus,
            int currentBillID)
        {
            if (LoginUserInfo.DeptId == 0)
            {
                MessageBoxShowSimple("用户没有选择登录科室");
                return false;
            }

            billStatus = billEditStatus;
            if (billStatus == MWEnum.BillEditStatus.ADD_STATUS)
            {
                MW_OutStoreHead outHead = new MW_OutStoreHead();
                outHead.AuditFlag = 0;
                outHead.AuditEmpID = 0;
                outHead.AuditEmpName = string.Empty;
                outHead.Remark = string.Empty;
                outHead.DeptID = selectedDeptID;
                outHead.BusiType = MWConstant.OP_MW_DEPTDRAW;//默认内耗流通出库
                outHead.BillTime = System.DateTime.Now;
                outHead.RegTime = System.DateTime.Now;
                outHead.RegEmpID = GetUserInfo().EmpId;
                outHead.RegEmpName = GetUserInfo().EmpName;
                currentHead = outHead;
                ifrmOutstoreDetail.BindOutHeadInfo(currentHead);
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(MWConstant.MW_Out_SYSTEM);
                    request.AddData(currentBillID);
                });

                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialOutStoreController", "GetEditBillHead", requestAction);
                MW_OutStoreHead inStoreHead = retdata.GetData<MW_OutStoreHead>(0);
                currentHead = inStoreHead;
                ifrmOutstoreDetail.BindOutHeadInfo(currentHead);
            }

            ifrmOutstoreDetail.InitControStatus(billEditStatus);
            return true;
        }

        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        /// <param name="billID">单据ID</param>
        /// <param name="busiType">类型</param>
        [WinformMethod]
        public void DeleteBill(string frmName, int billID, string busiType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(busiType);
                request.AddData(billID);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialOutStoreController", "DeleteBill", requestAction);
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
            var dic = ifrmOutStore.GetQueryCondition(selectedDeptID);
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(MWConstant.OP_MW_DEPTDRAW);
                request.AddData(dic);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialOutStoreController", "LoadBillHead", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            ifrmOutStore.BindHeadGrid(dtRtn);
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
                case "FrmOutStore":
                    headInfo = ifrmOutStore.GetCurrentHeadID();
                    if (headInfo != null)
                    {
                        requestAction = ((ClientRequestData request) =>
                        {
                            request.AddData(MWConstant.OP_MW_DEPTDRAW);
                            request.AddData(headInfo);
                        });

                        retdata = InvokeWcfService("DrugProject.Service", "MaterialOutStoreController", "LoadBillDetails", requestAction);
                        ifrmOutStore.BindDeatailGrids(retdata.GetData<DataTable>(0));
                    }
                    else
                    {
                        ifrmOutStore.BindDeatailGrids(null);
                    }

                    break;
                case "FrmOutStoreDetail":
                    headInfo = new Dictionary<string, string>();
                    headInfo.Add("OutHeadID", currentHead.OutStoreHeadID.ToString());
                    requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(MWConstant.OP_MW_DEPTDRAW);//出库
                        request.AddData(headInfo);
                    });
                    retdata = InvokeWcfService("DrugProject.Service", "MaterialOutStoreController", "LoadBillDetails", requestAction);
                    currentDetails = retdata.GetData<DataTable>(0);
                    ifrmOutstoreDetail.BindDetailsGrid(currentDetails);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 从界面信息更新单据头
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void RefreshHead(string frmName)
        {
            if (billStatus == MWEnum.BillEditStatus.ADD_STATUS)
            {
                MW_OutStoreHead editHead = ifrmOutstoreDetail.GetHeadInfo();
                currentHead.BillTime = editHead.BillTime;
                currentHead.ToDeptID = editHead.ToDeptID;
                currentHead.Remark = editHead.Remark;
                currentHead.BusiType = editHead.BusiType;
            }
        }

        /// <summary>
        /// 报表打印
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        /// <param name="dthead">报表头数据</param>
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
        /// 设置报表数据
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        /// <param name="head">头</param>
        /// <param name="dt">明细</param>
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
                EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 5005, 0, fields, dt).PrintPreview(true);
            }
            else
            {
                MessageBoxShowSimple("没有报表数据");
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
            currentHead.StockFee = 0;
            currentHead.RetailFee = 0;
            List<MW_OutStoreDetail> lstDetails = new List<MW_OutStoreDetail>();
            for (int index = 0; index < currentDetails.Rows.Count; index++)
            {
                MW_OutStoreDetail detail = ConvertExtend.ToObject<MW_OutStoreDetail>(currentDetails, index);
                var pAmount = 0;
                if (currentDetails.Rows[index]["pAmount"] != DBNull.Value)
                {
                    pAmount = Convert.ToInt32(currentDetails.Rows[index]["pAmount"].ToString());
                }

                detail.Amount = pAmount;
                detail.DeptID = currentHead.DeptID;
                detail.ToDeptID = currentHead.ToDeptID;
                currentHead.StockFee += detail.StockFee;
                currentHead.RetailFee += detail.RetailFee;
                lstDetails.Add(detail);
            }

            requestAction = ((ClientRequestData request) =>
            {
                request.AddData(MWConstant.MW_Out_SYSTEM);
                request.AddData(currentHead.BusiType);
                request.AddData(currentHead);
                request.AddData<List<MW_OutStoreDetail>>(lstDetails);
                request.AddData<List<int>>(ifrmOutstoreDetail.GetDeleteDetails());
            });

            retdata = InvokeWcfService("DrugProject.Service", "MaterialOutStoreController", "SaveBill", requestAction);
            MWBillResult result = retdata.GetData<MWBillResult>(0);
            if (result.Result == 0)
            {
                MessageBoxShowSimple("单据已经保存成功，如果没有配置自动审核.请及时审核单据");
                if (billStatus == MWEnum.BillEditStatus.ADD_STATUS)
                {
                    ifrmOutstoreDetail.NewBillClear();
                }
                else
                {
                    ifrmOutstoreDetail.CloseCurrentWindow();
                }
            }
            else
            {
                MessageBoxShowSimple("单据保存失败:" + result.ErrMsg);
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

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialOutStoreController", "AuditBill", requestAction);
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

                ifrmOutstoreDetail.ShowTotalFee(totalStockFee, totalRetailFee);
            }
        }
    }
}
