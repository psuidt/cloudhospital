using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.DrugManage;
using HIS_Entity.MaterialManage;
using HIS_MaterialManage.Winform.IView.StoreMgr;
using HIS_MaterialManage.Winform.ViewForm;

namespace HIS_MaterialManage.Winform.Controller
{
    /// <summary>
    /// 物资盘点控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmCheck")]//在菜单上显示   
    [WinformView(Name = "FrmCheck", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmCheck")]//物资盘点    
    [WinformView(Name = "FrmCheckDetail", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmCheckDetail")]//物资盘点明细
    [WinformView(Name = "FrmCheckType", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmCheckType")]//提取盘点数据，物资类型选择    
    [WinformView(Name = "FrmCheckAudit", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmCheckAudit")]//物资盘点审核
    public class MaterialCheckStoreController : WcfClientController
    {
        #region 物资盘点主界面
        /// <summary>
        /// 当前单据编辑状态
        /// </summary>
        private DGEnum.BillEditStatus billStatus;

        /// <summary>
        /// 当前选中科室
        /// </summary>
        private int selectedDeptID = 0;

        /// <summary>
        /// 物资库盘点窗体
        /// </summary>
        IFrmCheck frmCheck;

        /// <summary>
        /// 物资类型选择窗体
        /// </summary>
        IFrmCheckType frmCheckType;

        /// <summary>
        /// 物资库盘点审核窗体
        /// </summary>
        IFrmCheckAudit frmCheckAudit;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            frmCheck = (IFrmCheck)iBaseView["FrmCheck"];
            frmCheckDetail = (IFrmCheckDetail)iBaseView["FrmCheckDetail"];
            frmCheckType = (IFrmCheckType)iBaseView["FrmCheckType"];
            frmCheckAudit = (IFrmCheckAudit)iBaseView["FrmCheckAudit"];
        }

        /// <summary>
        /// 获取物资科室列表
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void GetDrugDept(string frmName)
        {
            if (frmName == "FrmCheck")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(1);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialCheckStoreController", "GetMaterialDeptList", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                frmCheck.BindMaterialDept(dt);
            }
            else if (frmName == "FrmCheckAudit")
            {
                //物资库盘点审核
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(1);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialCheckStoreController", "GetMaterialDeptList", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                frmCheckAudit.BindMaterialDept(dt);
            }
        }

        /// <summary>
        /// 查询单据表头
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void LoadBillHead(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(MWConstant.OP_MW_CHECK);
                request.AddData(frmCheck.GetQueryCondition(selectedDeptID));
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialCheckStoreController", "LoadBillHead", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            frmCheck.BindInHeadGrid(dtRtn);
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
                //物资库盘点
                case "FrmCheck":
                    headInfo = frmCheck.GetCurrentHeadID();
                    if (headInfo != null)
                    {
                        requestAction = ((ClientRequestData request) =>
                        {
                            request.AddData(MWConstant.OP_MW_CHECK);
                            request.AddData(headInfo);
                        });

                        retdata = InvokeWcfService("DrugProject.Service", "MaterialCheckStoreController", "LoadBillDetails", requestAction);
                        frmCheck.BindInDetailGrid(retdata.GetData<DataTable>(0));
                    }
                    else
                    {
                        frmCheck.BindInDetailGrid(null);
                    }

                    break;
                //物资库盘点明细
                case "FrmCheckDetail":
                    headInfo = new Dictionary<string, string>();
                    headInfo.Add("CheckHeadID", currentMWHead.CheckHeadID.ToString());
                    requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(MWConstant.OP_MW_CHECK);
                        request.AddData(headInfo);
                    });

                    retdata = InvokeWcfService("DrugProject.Service", "MaterialCheckStoreController", "LoadBillDetails", requestAction);
                    currentMWDetails = retdata.GetData<DataTable>(0);
                    frmCheckDetail.BindInDetails(currentMWDetails);
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
        public void InitBillHead(
            string frmName,
            DGEnum.BillEditStatus billEditStatus,
            int currentBillID)
        {
            billStatus = billEditStatus;
            if (frmName == "FrmCheck")
            {
                if (billStatus == DGEnum.BillEditStatus.ADD_STATUS)
                {
                    MW_CheckHead inHead = new MW_CheckHead();
                    inHead.AuditFlag = 0;
                    inHead.AuditEmpID = 0;
                    inHead.AuditEmpName = string.Empty;
                    inHead.Remark = string.Empty;
                    inHead.DelFlag = 0;
                    inHead.DeptID = selectedDeptID;
                    inHead.BusiType = MWConstant.OP_MW_CHECK;
                    inHead.RegTime = System.DateTime.Now;
                    inHead.RegEmpID = GetUserInfo().EmpId;
                    inHead.RegEmpName = GetUserInfo().EmpName;
                    inHead.BillNO = 0;
                    currentMWHead = inHead;
                    frmCheckDetail.BindInHeadInfo(currentMWHead);
                }
                else
                {
                    Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(MWConstant.MW_IN_SYSTEM);
                        request.AddData(currentBillID);
                    });

                    ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialCheckStoreController", "GetEditBillHead", requestAction);
                    MW_CheckHead inStoreHead = retdata.GetData<MW_CheckHead>(0);
                    currentMWHead = inStoreHead;
                    frmCheckDetail.BindInHeadInfo(currentMWHead);
                }

                frmCheckDetail.InitControStatus(billEditStatus);
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

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialCheckStoreController", "DeleteBill", requestAction);
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
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(selectedDeptID);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialCheckStoreController", "ClearCheckStatus", requestAction);
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
        [WinformMethod]
        public void SetCheckStatus()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(selectedDeptID);
                request.AddData(1);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialCheckStoreController", "SetCheckStatus", requestAction);
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

        #region 物资盘点明细界面
        /// <summary>
        /// 物资库盘点单明细窗体
        /// </summary>
        IFrmCheckDetail frmCheckDetail;

        /// <summary>
        /// 当前编辑物资库单据头实体
        /// </summary>
        private MW_CheckHead currentMWHead;

        /// <summary>
        /// 当前编辑物资库单据明细列表
        /// </summary>
        private DataTable currentMWDetails;

        /// <summary>
        /// 物资批次信息表
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
        /// 获取当前选中的物资科室ID
        /// </summary>
        /// <param name="deptID">物资科室ID</param>
        [WinformMethod]
        public void SetSelectedDept(int deptID)
        {
            selectedDeptID = deptID;
        }

        /// <summary>
        /// 计算汇总金额并展示
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void ComputeTotalFee(string frmName)
        {
            if (frmName == "FrmCheckDetail")
            {
                if (currentMWDetails != null)
                {
                    decimal totalAct = 0;
                    decimal totalFact = 0;
                    decimal retailPrice = 0;
                    decimal factAmount = 0;
                    decimal actAmount = 0;
                    for (int index = 0; index < currentMWDetails.Rows.Count; index++)
                    {
                        DataRow dRow = currentMWDetails.Rows[index];
                        retailPrice = dRow["RetailPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(dRow["RetailPrice"]);
                        factAmount = dRow["FactAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(dRow["FactAmount"]);
                        actAmount = dRow["ActAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(dRow["ActAmount"]);
                        totalAct += retailPrice * actAmount;
                        totalFact += retailPrice * factAmount;
                    }

                    frmCheckDetail.ShowTotalFee(totalAct, totalFact);
                }
            }
        }

        /// <summary>
        /// 获取盘点单物资数据
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetCheckDrugInfo(string frmName)
        {
            if (frmName == "FrmCheckDetail")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(MWConstant.MW_IN_SYSTEM);
                    request.AddData(currentMWHead.BusiType);
                    request.AddData(selectedDeptID);
                });

                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialCheckStoreController", "GetCheckDrugInfo", requestAction);
                frmCheckDetail.BindMaterialInfoCard(retdata.GetData<DataTable>(0));
                frmCheckDetail.BindDrugPositFindCard(retdata.GetData<DataTable>(0));
            }
            else if (frmName == "FrmCheckAudit")
            {
                //物资库盘点审核
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(MWConstant.MW_IN_SYSTEM);
                    request.AddData(MWConstant.OP_MW_CHECK);
                    request.AddData(selectedDeptID);
                });

                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialCheckStoreController", "GetCheckDrugInfo", requestAction);
                frmCheckAudit.BindDrugPositFindCard(retdata.GetData<DataTable>(0));
            }
        }

        /// <summary>
        /// 获取批次数据
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDrugBatchInfo(string frmName)
        {
            if (frmName == "FrmCheckDetail")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(MWConstant.MW_IN_SYSTEM);
                    request.AddData(selectedDeptID);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialCheckStoreController", "GetInStoreBatchInfo", requestAction);
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
            if (frmName == "FrmCheckDetail")
            {
                List<MW_CheckDetail> lstDetails = new List<MW_CheckDetail>();
                for (int index = 0; index < currentMWDetails.Rows.Count; index++)
                {
                    MW_CheckDetail detail = ConvertExtend.ToObject<MW_CheckDetail>(currentMWDetails, index);
                    lstDetails.Add(detail);
                }

                requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(MWConstant.MW_IN_SYSTEM);
                    request.AddData(currentMWHead.BusiType);
                    request.AddData(currentMWHead);
                    request.AddData<List<MW_CheckDetail>>(lstDetails);
                    request.AddData<List<int>>(frmCheckDetail.GetDeleteDetails());
                });

                retdata = InvokeWcfService("DrugProject.Service", "MaterialCheckStoreController", "SaveBill", requestAction);
                bool result = retdata.GetData<bool>(0);
                if (result)
                {
                    MessageBoxShowSimple("单据保存盘点，请及时审核...");
                    if (billStatus == DGEnum.BillEditStatus.ADD_STATUS)
                    {
                        frmCheckDetail.NewBillClear();
                    }
                    else
                    {
                        frmCheckDetail.CloseCurrentWindow();
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
            if (frmName == "FrmCheckDetail")
            {
                MW_CheckHead editHead = frmCheckDetail.GetHeadInfoMW();
                currentMWHead.RegTime = editHead.RegTime;
            }
        }

        /// <summary>
        /// 切换批次绑定数据源
        /// </summary>
        /// <param name="materialID">选定物资ID</param>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void ChangeBatchMaterial(int materialID, string frmName)
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
        }

        /// <summary>
        /// 打印盘点空表
        /// </summary>
        /// <param name="dataSource">盘点物资数据</param>
        /// <param name="frmName">进入窗口</param>
        [WinformMethod]
        public void Print(DataTable dataSource, string frmName)
        {
            if (frmName == "FrmCheckDetail")
            {
                if (dataSource.Rows.Count > 0)
                {
                    Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                    myDictionary.Add("HospitalName", LoginUserInfo.WorkName);
                    myDictionary.Add("RegDept", LoginUserInfo.DeptName);
                    myDictionary.Add("RegPeople", LoginUserInfo.EmpName);
                    myDictionary.Add("doseName", string.Empty);
                    EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 5001, 0, myDictionary, dataSource).PrintPreview(true);
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
        /// 取得物资类型典
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDrugTypeDic(string frmName)
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialCheckStoreController", "GetMaterialType");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmCheckType.BindMaterialType(dt);
        }

        /// <summary>
        /// 提取盘点库存数据
        /// </summary>
        /// <param name="frmName">父窗体窗体入口</param>
        [WinformMethod]
        public void GetCheckStorageData(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(MWConstant.OP_MW_CHECK);
                request.AddData(frmCheckType.GetQueryCondition(selectedDeptID));
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialCheckStoreController", "LoadStorageData", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            frmCheckDetail.InsertGetStorageData(dtRtn);
        }

        /// <summary>
        /// 选择物资类型
        /// </summary>
        /// <param name="fatherFrmname">父窗体名称</param>
        /// <returns>子窗体关闭状态</returns>
        [WinformMethod]
        public DialogResult OpenDrugTypeDialog(string fatherFrmname)
        {
            var dialog = iBaseView["FrmCheckType"] as FrmCheckType;
            dialog.FatherFrmname = fatherFrmname;
            if (null == dialog)
            {
                return DialogResult.None;
            }

            return dialog.ShowDialog();
        }
        #endregion

        #region 物资盘点审核界面
        /// <summary>
        /// 取得盘点审核待审的状态信息、库房状态，待审单据数
        /// </summary>
        /// <param name="frmName">窗口入口</param>
        /// <param name="deptId">库房ID</param>
        [WinformMethod]
        public void CheckStatusInfos(string frmName, int deptId)
        {
            var busiType = string.Empty;
            if (frmName == "FrmCheckAudit")
            {
                busiType = MWConstant.OP_MW_CHECK;
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(busiType);
                    request.AddData(deptId);
                });

                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialCheckStoreController", "CheckStatusInfos", requestAction);
                frmCheckAudit.BindShowTip(retdata.GetData<string>(0));
            }
        }

        /// <summary>
        /// 加载盘点审核单表头
        /// </summary>
        /// <param name="frmName">进入窗口名称</param>
        [WinformMethod]
        public void LoadAudtiCheckHead(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(MWConstant.OP_MW_CHECK);
                request.AddData(frmCheckAudit.GetAuditHeadQueryCondition());
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialCheckStoreController", "LoadAudtiCheckHead", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            frmCheckAudit.BindAuditHeadGrid(dtRtn);
        }

        /// <summary>
        /// 加载盘点审核单明细
        /// </summary>
        /// <param name="frmName">进入窗口名称</param>
        [WinformMethod]
        public void LoadAuditCheckDetail(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(MWConstant.OP_MW_CHECK);
                request.AddData(frmCheckAudit.GetAuditDetailQueryCondition());
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialCheckStoreController", "LoadAuditCheckDetail", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            frmCheckAudit.BindAuditDetailGrid(dtRtn);
        }

        /// <summary>
        /// 汇总所有未审核单据明细并提取
        /// </summary>
        /// <param name="frmName">进入窗口名称</param>
        [WinformMethod]
        public void LoadAllNotAuditDetail(string frmName)
        {
            if (frmName == "FrmCheckAudit")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(MWConstant.OP_MW_CHECK);
                    request.AddData(frmCheckAudit.GetAllNotAuditDetailQueryCondition());
                });

                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialCheckStoreController", "LoadAllNotAuditDetail", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmCheckAudit.BindCheckDetailGrid(dtRtn);
                string message = ComputeAuditTotalFee(frmName, dtRtn);
                frmCheckAudit.ShowAuditCompute(message);
            }
        }

        /// <summary>
        /// 计算汇总值
        /// </summary>
        /// <param name="frmName">窗体入库</param>
        /// <param name="currentDetails">明细数据</param>
        /// <returns>操作消息</returns>
        public string ComputeAuditTotalFee(string frmName, DataTable currentDetails)
        {
            string showMessage = string.Empty;

            if (frmName == "FrmCheckAudit")
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
            if (frmName == "FrmCheckAudit")
            {
                busiType = MWConstant.OP_MW_CHECK;
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(busiType);
                request.AddData(deptId);
                request.AddData(GetUserInfo().EmpId);
                request.AddData(GetUserInfo().EmpName);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialCheckStoreController", "AuditBill", requestAction);
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
                
                if (frmName == "FrmCheckAudit")
                {
                    EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 5002, 0, myDictionary, dtAuditDetail).PrintPreview(true);
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
                EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 5003, 0, myDictionary, dtAuditDetail).PrintPreview(true);
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
                EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 5003, 0, myDictionary, dtAuditDetail).PrintPreview(true);
            }
        }
        #endregion
    }
}
