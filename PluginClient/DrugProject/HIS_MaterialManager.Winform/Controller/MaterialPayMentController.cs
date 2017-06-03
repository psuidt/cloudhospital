using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.MaterialManage;
using HIS_MaterialManage.Winform.IView.FinanceMgr;

namespace HIS_MaterialManage.Winform.Controller
{
    /// <summary>
    /// 物资付款控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmMaterialPayMent")]//与系统菜单对应                          HIS_MaterialManage.Winform.ViewForm
    [WinformView(Name = "FrmMaterialPayMent", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmMaterialPayMent")]
    public class MaterialPayMentController : WcfClientController
    {
        /// <summary>
        /// 物资付款接口
        /// </summary>
        IFrmMaterialPayMent frmMaterialPayMent;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            frmMaterialPayMent = (IFrmMaterialPayMent)iBaseView["FrmMaterialPayMent"];
        }

        /// <summary>
        /// 获取药剂科室列表
        /// </summary>
        [WinformMethod]
        public void GetMaterialDept()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(1);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialFinanceController", "GetMaterialDeptList", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmMaterialPayMent.BindDrugDept(dt);
        }

        /// <summary>
        /// 获取供应商列表绑定ShowCard
        /// </summary>
        [WinformMethod]
        public void GetSupplyForShowCard()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialFinanceController", "GetSupplyForShowCard");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmMaterialPayMent.BindSupply(dt);
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
                if (type == 0)
                {
                    request.AddData(frmMaterialPayMent.GetQueryCondition(selectedDeptID));
                }
                else
                {
                    Dictionary<string, string> queryCondition = new Dictionary<string, string>();
                    queryCondition.Add("PayRecordID", deptID);
                    request.AddData(queryCondition);
                }
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialFinanceController", "LoadBillHead", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            if (type == 0)
            {
                frmMaterialPayMent.BindInHeadGrid(dtRtn);
            }
            else
            {
                frmMaterialPayMent.BindInHeadGrids(dtRtn);
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
                headInfo = frmMaterialPayMent.GetCurrentHeadID();
            }
            else
            {
                headInfo = frmMaterialPayMent.GetCurrentHeadIDs();
            }

            if (headInfo != null)
            {
                requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(headInfo);
                });

                retdata = InvokeWcfService("DrugProject.Service", "MaterialFinanceController", "LoadBillDetails", requestAction);
                if (type == 0)
                {
                    frmMaterialPayMent.BindInDetailGrid(retdata.GetData<DataTable>(0));
                }
                else
                {
                    frmMaterialPayMent.BindInDetailGrids(retdata.GetData<DataTable>(0));
                }
            }
            else
            {
                if (type == 0)
                {
                    frmMaterialPayMent.BindInDetailGrid(null);
                }
                else
                {
                    frmMaterialPayMent.BindInDetailGrids(null);
                }
            }
        }

        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="invoiceNO">单据号</param>
        /// <param name="ids">单据集合</param>
        /// <param name="totalRetailFee">零售金额</param>
        /// <param name="totalStockFee">进货金额</param>
        [WinformMethod]
        public void BillPay(string deptID, string invoiceNO, string ids, decimal totalRetailFee, decimal totalStockFee)
        {
            int selectedDeptID = 0;
            if (int.TryParse(deptID, out selectedDeptID))
            {
            }

            MW_PayRecord payrecord = new MW_PayRecord();
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
            if (frmMaterialPayMent.CurrentRow != null)
            {
                if (int.TryParse(frmMaterialPayMent.CurrentRow["SupplierID"].ToString(), out supplierID))
                {
                }

                payrecord.SupportName = supplierID;
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(payrecord);
                request.AddData(ids);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialFinanceController", "BillPay", requestAction);
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
                request.AddData(frmMaterialPayMent.GetQueryConditions(selectedDeptID));
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialFinanceController", "LoadPayRecord", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            frmMaterialPayMent.BindInPayRecordGrid(dtRtn);
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

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialFinanceController", "CancelBillPay", requestAction);
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
