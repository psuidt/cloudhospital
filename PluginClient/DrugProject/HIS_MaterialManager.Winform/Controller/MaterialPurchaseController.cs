using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.MaterialManage;
using HIS_MaterialManage.Winform.IView;

namespace HIS_MaterialManage.Winform.Controller
{
    /// <summary>
    /// 采购计划控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmPurchase")]//在菜单上显示
    [WinformView(Name = "FrmPurchase", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmPurchase")]//
    public class MaterialPurchaseController : WcfClientController
    {
        /// <summary>
        /// 采购计划接口
        /// </summary>
        IFrmPurchase iFrmPurchase;

        /// <summary>
        /// 当前编辑药库采购单据明细列表
        /// </summary>
        private DataTable currentDetails;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            iFrmPurchase = (IFrmPurchase)iBaseView["FrmPurchase"];
        }

        /// <summary>
        /// 获取当前登录用户对象
        /// </summary>
        [WinformMethod]
        public void GetLoginUserInfo()
        {
            iFrmPurchase.GetLoginUserInfo(LoginUserInfo);
        }

        /// <summary>
        /// 获取审核状态数据
        /// </summary>
        [WinformMethod]
        public void GetCheckAuditData()
        {
            DataTable dt = CreateCheckAudit();
            iFrmPurchase.BindAuditStatus(dt);
        }

        /// <summary>
        /// 取得物资库数据
        /// </summary>
        [WinformMethod]
        public void GetWareRoomData()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialPurchaseController", "GetWareRoomData");
            DataTable dt = retdata.GetData<DataTable>(0);
            iFrmPurchase.BindStoreRoomComboxList(dt);
        }

        /// <summary>
        /// 查询采购计划单表头数据
        /// </summary>
        [WinformMethod]
        public void GetPlanHeadData()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(MWConstant.OP_MW_STOCKPLAN);//采购计划
                request.AddData(iFrmPurchase.GetQueryCondition());
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialPurchaseController", "GetPlanHeadData", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            iFrmPurchase.BindPlanHeadGrid(dtRtn);
        }

        /// <summary>
        /// 取得小于下限的库存物资
        /// </summary>
        /// <param name="deptId">科室ID</param>
        [WinformMethod]
        public void GetLessLowerLimitData(int deptId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptId);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialPurchaseController", "GetLessLowerLimitData", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            iFrmPurchase.InsertLessLowerLimitData(dtRtn);
        }

        /// <summary>
        /// 取得小于上限的库存物资
        /// </summary>
        /// <param name="deptId">科室ID</param>
        [WinformMethod]
        public void GetLessUpperLimitData(int deptId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptId);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialPurchaseController", "GetLessUpperLimitData", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            iFrmPurchase.InsertLessUpperLimitData(dtRtn);
        }

        /// <summary>
        /// 查询采购计划单明细数据
        /// </summary>
        [WinformMethod]
        public void GetPlanDetailData()
        {
            Dictionary<string, string> headInfo = null;
            headInfo = iFrmPurchase.GetCurrentHeadID();
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(MWConstant.OP_MW_STOCKPLAN);//采购计划明细
                request.AddData(headInfo);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialPurchaseController", "GetPlanDetailData", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            iFrmPurchase.BindPlanDetailGrid(dtRtn);
        }

        /// <summary>
        /// 取得物资字典选择卡片数据
        /// </summary>
        [WinformMethod]
        public void GetMaterialDicShowCard()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialPurchaseController", "GetMaterialDicShowCard");
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            iFrmPurchase.BindMaterialInfoCard(dtRtn);
        }

        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="billID">采购计划头表ID</param>
        [WinformMethod]
        public void DeleteBill(int billID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(MWConstant.OP_MW_STOCKPLAN);
                request.AddData(billID);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialPurchaseController", "DeleteBill", requestAction);
            bool result = retdata.GetData<bool>(0);
            if (result)
            {
                MessageBoxShowSimple("单据已经成功删除");
                //刷新采购计划单
                GetPlanHeadData();
            }
            else
            {
                string rtnMsg = retdata.GetData<string>(1);
                MessageBoxShowSimple("单据删除失败:" + rtnMsg);
            }
        }

        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="head">单据头表</param>
        [WinformMethod]
        public void AuditBill(MW_PlanHead head)
        {
            head.AuditEmpID = LoginUserInfo.EmpId;
            head.AuditEmpName = LoginUserInfo.EmpName;
            head.AuditFlag = 1;
            head.AuditTime = System.DateTime.Now;

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(head);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialPurchaseController", "AuditBill", requestAction);
            bool result = retdata.GetData<bool>(0);
            if (result)
            {
                MessageBoxShowSimple("单据已经成功审核");
                //刷新采购计划单
                GetPlanHeadData();
            }
            else
            {
                string rtnMsg = retdata.GetData<string>(1);
                MessageBoxShowSimple("单据审核失败:" + rtnMsg);
            }
        }

        /// <summary>
        /// 取得表头信息
        /// </summary>
        /// <returns>表头信息</returns>
        [WinformMethod]
        public MW_PlanHead GetPlanHeadInfo()
        {
            MW_PlanHead editHead = iFrmPurchase.GetPlanHeadInfo();
            return editHead;
        }

        /// <summary>
        /// 保存单据
        /// </summary>
        [WinformMethod]
        public void SaveBill()
        {
            Action<ClientRequestData> requestAction = null;
            ServiceResponseData retdata = null;
            MW_PlanHead currentHead = iFrmPurchase.GetPlanHeadInfo();
            currentHead.StockFee = 0;
            currentHead.RetailFee = 0;
            DataTable currentDetail = iFrmPurchase.GetPlanDetailInfo();
            List<MW_PlanDetail> lstDetails = new List<MW_PlanDetail>();
            for (int index = 0; index < currentDetail.Rows.Count; index++)
            {
                MW_PlanDetail detail = ConvertExtend.ToObject<MW_PlanDetail>(currentDetail, index);
                currentHead.StockFee += detail.StockFee;
                currentHead.RetailFee += detail.RetailFee;
                lstDetails.Add(detail);
            }

            requestAction = ((ClientRequestData request) =>
            {
                request.AddData(MWConstant.OP_MW_STOCKPLAN);
                request.AddData(currentHead);
                request.AddData<List<MW_PlanDetail>>(lstDetails);
                request.AddData<List<int>>(iFrmPurchase.GetDeleteDetails());
            });

            retdata = InvokeWcfService("DrugProject.Service", "MaterialPurchaseController", "SaveBill", requestAction);
            bool result = retdata.GetData<bool>(0);
            if (result)
            {
                MessageBoxShowSimple("采购计划单据保存成功，请及时审核...");
            }
            else
            {
                string rtnMsg = retdata.GetData<string>(1);
                MessageBoxShowSimple("单据保存失败:" + rtnMsg);
            }
        }

        /// <summary>
        /// 计算合计
        /// </summary>
        /// <param name="dt">明细数据</param>
        [WinformMethod]
        public void ComputeTotalFee(DataTable dt)
        {
            currentDetails = dt;
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

                iFrmPurchase.ShowTotalFee(totalStockFee, totalRetailFee);
            }
        }

        /// <summary>
        /// 构建审核状态数据表
        /// </summary>
        /// <returns>返回表</returns>
        private DataTable CreateCheckAudit()
        {
            DataTable dtData = new DataTable();
            dtData.Columns.Add("ID");
            dtData.Columns.Add("Name");
            DataRow drData;
            drData = dtData.NewRow();
            drData[0] = 0;
            drData[1] = "未审";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 1;
            drData[1] = "已审";
            dtData.Rows.Add(drData);
            return dtData;
        }
    }
}
