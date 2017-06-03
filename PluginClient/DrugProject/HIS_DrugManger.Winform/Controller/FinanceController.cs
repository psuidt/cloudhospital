using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.FinanceMgr;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.Controller
{
    /// <summary>
    /// 财务管理控制器（付款）
    /// </summary>
    [WinformController(DefaultViewName = "FrmPayMent")]//在菜单上显示
    [WinformView(Name = "FrmPayMent", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmPayMent")]//付款
    public class FinanceController : WcfClientController
    {
        /// <summary>
        /// 财务付款
        /// </summary>
        IFrmPayMent frmPayMent;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public override void Init()
        {
            frmPayMent = (IFrmPayMent)iBaseView["FrmPayMent"];
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
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "FinanceController", "GetDrugDeptList", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmPayMent.BindDrugDept(dt);
        }

        /// <summary>
        /// 获取供应商列表绑定ShowCard
        /// </summary>
        [WinformMethod]
        public void GetSupplyForShowCard()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "FinanceController", "GetSupplyForShowCard");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmPayMent.BindSupply(dt);
        }

        /// <summary>
        /// 查询单据表头
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="type">类型</param>
        [WinformMethod]
        public void LoadBillHead(string deptID, int type)
        {
            int selectedDeptID = 0;
            if (int.TryParse(deptID, out selectedDeptID))
            {
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(DGConstant.OP_DW_INSTORE);
                if (type == 0)
                {
                    request.AddData(frmPayMent.GetQueryCondition(selectedDeptID));
                }
                else
                {
                    Dictionary<string, string> queryCondition = new Dictionary<string, string>();
                    queryCondition.Add("PayRecordID", deptID);
                    request.AddData(queryCondition);
                }
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "FinanceController", "LoadBillHead", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            if (type == 0)
            {
                frmPayMent.BindInHeadGrid(dtRtn);
            }
            else
            {
                frmPayMent.BindInHeadGrids(dtRtn);
            }
        }

        /// <summary>
        /// 查询单据明细
        /// </summary>
        /// <param name="type">类型</param>
        [WinformMethod]
        public void LoadBillDetails(int type)
        {
            Dictionary<string, string> headInfo = null;
            Action<ClientRequestData> requestAction = null;
            ServiceResponseData retdata = null;
            if (type == 0)
            {
                headInfo = frmPayMent.GetCurrentHeadID();
            }
            else
            {
                headInfo = frmPayMent.GetCurrentHeadIDs();
            }

            if (headInfo != null)
            {
                requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_INSTORE);
                    request.AddData(headInfo);
                });
                retdata = InvokeWcfService("DrugProject.Service", "FinanceController", "LoadBillDetails", requestAction);
                if (type == 0)
                {
                    frmPayMent.BindInDetailGrid(retdata.GetData<DataTable>(0));
                }
                else
                {
                    frmPayMent.BindInDetailGrids(retdata.GetData<DataTable>(0));
                }
            }
            else
            {
                if (type == 0)
                {
                    frmPayMent.BindInDetailGrid(null);
                }
                else
                {
                    frmPayMent.BindInDetailGrids(null);
                }
            }
        }

        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="invoiceNO">单据号</param>
        /// <param name="ids">选中ID集</param>
        /// <param name="totalRetailFee">零售价</param>
        /// <param name="totalStockFee">进价</param>
        [WinformMethod]
        public void BillPay(string deptID, string invoiceNO, string ids, decimal totalRetailFee, decimal totalStockFee)
        {
            int selectedDeptID = 0;
            if (int.TryParse(deptID, out selectedDeptID))
            {
            }

            DG_PayRecord payrecord = new DG_PayRecord();
            payrecord.PayDeptID = selectedDeptID;
            payrecord.PayEmpID = LoginUserInfo.EmpId;
            payrecord.PayEmpName = LoginUserInfo.EmpName;
            payrecord.PayTime = DateTime.Now;
            payrecord.Remark = "付款";
            payrecord.DelFlag = 0;
            payrecord.InvoiceNO = invoiceNO;
            payrecord.TotalRetailFee = totalRetailFee;
            payrecord.TotalStockFee = totalStockFee;
            int supplierID = 0;
            if (frmPayMent.currentRow != null)
            {
                if (int.TryParse(frmPayMent.currentRow["SupplierID"].ToString(), out supplierID))
                {
                }

                payrecord.SupportName = supplierID;
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(payrecord);
                request.AddData(ids);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "FinanceController", "BillPay", requestAction);
            int result = retdata.GetData<int>(0);
            if (result > 0)
            {
                MessageBoxShowSimple("付款成功");
            }
            else
            {
                MessageBoxShowSimple("付款失败");
            }
        }

        /// <summary>
        /// 查询付款记录
        /// </summary>
        /// <param name="deptID">科室ID</param>
        [WinformMethod]
        public void LoadPayRecord(string deptID)
        {
            int selectedDeptID = 0;
            if (int.TryParse(deptID, out selectedDeptID))
            {
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmPayMent.GetQueryConditions(selectedDeptID));
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "FinanceController", "LoadPayRecord", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            frmPayMent.BindInPayRecordGrid(dtRtn);
        }

        /// <summary>
        ///  查询付款记录
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>付款记录数据集</returns>
        [WinformMethod]
        public DataTable PrintPayRecord(string deptID)
        {
            int selectedDeptID = 0;
            if (int.TryParse(deptID, out selectedDeptID))
            {
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmPayMent.GetQueryConditions(selectedDeptID));
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "FinanceController", "PrintPayRecord", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            return dtRtn;
        }

        /// <summary>
        /// 取消付款
        /// </summary>
        /// <param name="payRecordID">付款记录ID</param>
        [WinformMethod]
        public void CancelBillPay(string payRecordID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(payRecordID);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "FinanceController", "CancelBillPay", requestAction);
            int result = retdata.GetData<int>(0);
            if (result > 0)
            {
                MessageBoxShowSimple("取消成功");
            }
            else
            {
                MessageBoxShowSimple("取消失败");
            }
        }
    }
}
