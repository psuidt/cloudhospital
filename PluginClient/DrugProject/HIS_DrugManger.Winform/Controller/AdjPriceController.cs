using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.FinanceMgr;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.Controller
{
    /// <summary>
    /// 药品调价控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmAdjPrice")]//在菜单上显示
    [WinformView(Name = "FrmAdjPrice", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmAdjPrice")]//控制器关联的界面
    [WinformView(Name = "FrmAdjPriceDetail", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmAdjPriceDetail")]//控制器关联的界面
    public class AdjPriceController : WcfClientController
    {
        /// <summary>
        ///  网格编辑状态
        /// </summary>
        private DGEnum.BillEditStatus billStatus;

        /// <summary>
        /// 当前调价表头对象
        /// </summary>
        private DG_AdjHead curretDGAdjHead;

        /// <summary>
        /// 调价接口对象
        /// </summary>
        IFrmAdjPrice frmAdjPrice;

        /// <summary>
        /// 调价明细接口对象
        /// </summary>
        IFrmAdjPriceDetail frmAdjPriceDetail;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public override void Init()
        {
            frmAdjPrice = (IFrmAdjPrice)iBaseView["FrmAdjPrice"];
            frmAdjPriceDetail = (IFrmAdjPriceDetail)iBaseView["FrmAdjPriceDetail"];
        }

        /// <summary>
        /// 获取药剂科室列表
        /// </summary>
        [WinformMethod]
        public void GetDrugDept()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(1);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "AdjPriceController", "GetDrugDeptList", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmAdjPrice.BindDrugDept(dt);
        }

        /// <summary>
        /// 获取药品调价表头信息
        /// </summary>
        /// <param name="deptID">库房ID</param>
        [WinformMethod]
        public void LoadAdjHead(string deptID)
        {
            int selectedDeptID = 0;
            if (int.TryParse(deptID, out selectedDeptID))
            {
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                frmAdjPriceDetail.DeptID = selectedDeptID;
                request.AddData(frmAdjPrice.GetQueryCondition(selectedDeptID));
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "AdjPriceController", "LoadAdjHead", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            frmAdjPrice.BindInHeadGrid(dtRtn);
        }

        /// <summary>
        /// 读取药品调价表详细
        /// </summary>
        [WinformMethod]
        public void LoadAdjDetail()
        {
            Dictionary<string, string> headInfo = null;
            Action<ClientRequestData> requestAction = null;
            ServiceResponseData retdata = null;
            headInfo = frmAdjPrice.GetCurrentHeadID();
            if (headInfo != null)
            {
                requestAction = ((ClientRequestData request) =>
            {
                request.AddData(headInfo);
            });
                retdata = InvokeWcfService("DrugProject.Service", "AdjPriceController", "LoadAdjDetail", requestAction);
                frmAdjPrice.BindInDetailGrid(retdata.GetData<DataTable>(0));
            }
            else
            {
                frmAdjPrice.BindInDetailGrid(null);
            }
        }

        /// <summary>
        /// 获取入库单药品数据
        /// </summary>
        [WinformMethod]
        public void GetOutStoreDrugInfo()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmAdjPrice.DeprtID);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "AdjPriceController", "GetStoreDrugInFo", requestAction);
            frmAdjPriceDetail.BindDrugInfoCard(retdata.GetData<DataTable>(0));
        }

        /// <summary>
        /// 初始化单据头实体信息
        /// </summary>
        /// <param name="billEditStatus">单据编辑状态</param>
        /// <param name="currentBillID">当前单据ID</param>
        [WinformMethod]
        public void InitBillHead(DGEnum.BillEditStatus billEditStatus, int currentBillID)
        {
            billStatus = billEditStatus;
            if (billStatus == DGEnum.BillEditStatus.ADD_STATUS)
            {
                DG_AdjHead initHead = new DG_AdjHead();
                initHead.AuditFlag = 0;
                initHead.RegTime = System.DateTime.Now;
                initHead.RegEmpID = GetUserInfo().EmpId;
                initHead.Remark = string.Empty;
                initHead.ExecFlag = 0;
                initHead.DeptID = LoginUserInfo.DeptId;
                initHead.DelFlag = 0;
                initHead.BusiType = DGConstant.OP_DW_ADJPRICE;
                curretDGAdjHead = initHead;
                frmAdjPriceDetail.BindInHeadInfo(curretDGAdjHead);
            }

            frmAdjPriceDetail.InitControStatus(billEditStatus);
        }

        /// <summary>
        /// 查询单据明细
        /// </summary>
        [WinformMethod]
        public void LoadAdjDetails()
        {
            Dictionary<string, string> headInfo = null;
            Action<ClientRequestData> requestAction = null;
            ServiceResponseData retdata = null;
            headInfo = new Dictionary<string, string>();
            headInfo.Add("AdjHeadID", curretDGAdjHead.AdjHeadID.ToString());
            requestAction = ((ClientRequestData request) =>
            {
                request.AddData(headInfo);
            });
            retdata = InvokeWcfService("DrugProject.Service", "AdjPriceController", "LoadAdjDetail", requestAction);
            frmAdjPriceDetail.BindDetailsGrid(retdata.GetData<DataTable>(0));
        }

        /// <summary>
        /// 执行调价表
        /// </summary>
        /// <param name="adjCode">文书编号</param>
        /// <param name="remark">备注</param>
        /// <param name="detaildt">调价明细数据源</param>
        [WinformMethod]
        public void ExcutePrice(string adjCode, string remark, DataTable detaildt)
        {
            DG_AdjHead newhead = new DG_AdjHead();
            newhead.AdjCode = adjCode;
            newhead.Remark = remark;
            newhead.BillNO = 0;
            newhead.ExecTime = DateTime.Now;
            newhead.ExecFlag = 1;
            newhead.DelFlag = 0;
            newhead.AuditFlag = 1;
            newhead.BusiType = DGConstant.OP_DW_ADJPRICE;
            newhead.RegEmpID = LoginUserInfo.EmpId;
            newhead.RegTime = DateTime.Now;
            newhead.DeptID = frmAdjPriceDetail.DeptID;
            List<DG_AdjDetail> lstDetails = new List<DG_AdjDetail>();
            for (int index = 0; index < detaildt.Rows.Count; index++)
            {
                DG_AdjDetail detail = ConvertExtend.ToObject<DG_AdjDetail>(detaildt, index);
                detail.UnitID = Convert.ToInt32(detaildt.Rows[index]["MiniUnitID"]);
                detail.UnitName = detaildt.Rows[index]["MiniUnit"].ToString();
                detail.PackUnitID = Convert.ToInt32(detaildt.Rows[index]["PackUnitID"]);
                detail.PackUnitName = detaildt.Rows[index]["PackUnit"].ToString();
                lstDetails.Add(detail);
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(lstDetails);
                request.AddData(newhead);
                request.AddData(LoginUserInfo.WorkId);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "AdjPriceController", "ExcutePrice", requestAction);
            frmAdjPriceDetail.ExcuteComplete(retdata.GetData<DGBillResult>(0));
        }
    }
}