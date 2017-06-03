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
    /// （月结、付款）
    /// </summary>
    [WinformController(DefaultViewName = "FrmBalance")] //在菜单上显示
    [WinformView(Name = "FrmBalance", DllName = "HIS_DrugManage.Winform.dll",
        ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmBalance")] //药库月结

    [WinformView(Name = "FrmBalanceDS", DllName = "HIS_DrugManage.Winform.dll",
        ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmBalance")] //药房月结
    public class BalanceController : WcfClientController
    {
        /// <summary>
        /// 月结对象
        /// </summary>
        IFrmBalance frmBalance;

        /// <summary>
        /// 药房月结对象
        /// </summary>
        IFrmBalance frmBalanceDS;

        /// <summary>
        /// 药库月结对象
        /// </summary>
        IFrmBalance frmBalanceDW;

        /// <summary>
        /// 当前选中科室
        /// </summary>
        private int selectedDeptID = 0;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public override void Init()
        {
            frmBalanceDW = (IFrmBalance)iBaseView["FrmBalance"];
            frmBalanceDS = (IFrmBalance)iBaseView["FrmBalanceDS"];
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
        /// 获取药剂科室列表
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void GetDrugDept(string frmName)
        {
            if (frmName == "FrmBalance")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(1);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "GetDrugDeptList", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                frmBalanceDW.BindDrugDept(dt);
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(0);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "GetDrugDeptList", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                frmBalanceDS.BindDrugDept(dt);
            }
        }

        /// <summary>
        /// 月结时间
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetMonthBalaceByDept(string frmName)
        {
            if (frmName == "FrmBalance")
            {
                //药库
                frmBalance = frmBalanceDW;
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_MONTHACCOUNT);
                    request.AddData(selectedDeptID);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "BalanceController", "GetMonthBalaceByDept", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmBalance.BindDataGrid(dtRtn);
            }
            else
            {
                frmBalance = frmBalanceDS;
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_MONTHACCOUNT);
                    request.AddData(selectedDeptID);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "BalanceController", "GetMonthBalaceByDept", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                frmBalanceDS.BindDataGrid(dtRtn);
            }
        }

        /// <summary>
        ///  月结日期设置
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="value">日期</param>
        [WinformMethod]
        public void SetAccountDay(string frmName, int value)
        {
            if (frmName == "FrmBalance")
            {
                //药库
                frmBalance = frmBalanceDW;
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_MONTHACCOUNT);
                    request.AddData(selectedDeptID);
                    request.AddData(value);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "BalanceController", "SetAccountDay", requestAction);
                bool result = retdata.GetData<bool>(0);
                if (result)
                {
                    MessageBoxShowSimple("月结时间设置成功");
                }
                else
                {
                    MessageBoxShowSimple("月结时间设置失败");
                }
            }
            else
            {
                frmBalance = frmBalanceDS;
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_MONTHACCOUNT);
                    request.AddData(selectedDeptID);
                    request.AddData(value);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "BalanceController", "SetAccountDay", requestAction);
                bool result = retdata.GetData<bool>(0);
                if (result)
                {
                    MessageBoxShowSimple("月结时间设置成功");
                }
                else
                {
                    MessageBoxShowSimple("月结时间设置失败");
                }
            }
        }

        /// <summary>
        /// 获取日结日期
        /// </summary>
        ///<param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetAccountDay(string frmName)
        {
            Action<ClientRequestData> requestAction = null;
            if (frmName == "FrmBalance")
            {
                frmBalance = frmBalanceDW;
                requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_MONTHACCOUNT);
                    request.AddData(selectedDeptID);
                });
            }
            else
            {
                frmBalance = frmBalanceDS;
                requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_MONTHACCOUNT);
                    request.AddData(selectedDeptID);
                });
            }

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "BalanceController", "GetAccountDay", requestAction);
            int days = retdata.GetData<int>(0);
            if (days > 0)
            {
                frmBalance.BindBalanceDays(days);
            }
            else
            {
                MessageBoxShowSimple("月结日期没有设置，请设置");
            }
        }

        /// <summary>
        /// 月结记录
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void MonthAccount(string frmName)
        {
            Action<ClientRequestData> requestAction = null;
            if (frmName == "FrmBalance")
            {
                frmBalance = frmBalanceDW;
                requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_MONTHACCOUNT);
                    request.AddData(selectedDeptID);
                    request.AddData(LoginUserInfo.EmpId);
                });
            }
            else
            {
                frmBalance = frmBalanceDS;
                requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_MONTHACCOUNT);
                    request.AddData(selectedDeptID);
                    request.AddData(LoginUserInfo.EmpId);
                });
            }

            frmBalance.SetBtnEnable(false);
            frmBalance.SetLabelText("月结操作执行中，请等待");
            try
            {
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "BalanceController", "MonthAccount", requestAction);
                DGBillResult result = retdata.GetData<DGBillResult>(0);
                if (result.Result == 0)
                {
                    GetMonthBalaceByDept(frmName);
                    MessageBoxShowSimple("月结操作成功");
                }
                else
                {
                    MessageBoxShowSimple("月结操作失败:" + result.ErrMsg);
                }
            }
            finally
            {
                frmBalance.SetBtnEnable(true);
                frmBalance.SetLabelText(string.Empty);
            }
        }

        /// <summary>
        /// 系统月结
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void SystemCheckAccount(string frmName)
        {
            Action<ClientRequestData> requestAction = null;
            if (frmName == "FrmBalance")
            {
                frmBalance = frmBalanceDW;
                requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_MONTHACCOUNT);
                    request.AddData(selectedDeptID);
                    request.AddData(LoginUserInfo.EmpId);
                });
            }
            else
            {
                frmBalance = frmBalanceDS;
                requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_MONTHACCOUNT);
                    request.AddData(selectedDeptID);
                    request.AddData(LoginUserInfo.EmpId);
                });
            }

            frmBalance.SetBtnEnable(false);
            frmBalance.SetLabelText("对账操作执行中，请等待");
            try
            {
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "BalanceController", "SystemCheckAccount", requestAction);
                DgSpResult result = retdata.GetData<DgSpResult>(0);
                if (result.Result == 0)
                {
                    frmBalance.BindCheckAccount(result.Table);
                    MessageBoxShowSimple("对账操作成功");
                }
                else
                {
                    MessageBoxShowSimple("对账操作失败:" + result.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                MessageBoxShowSimple(ex.Message);
            }
            finally
            {
                frmBalance.SetBtnEnable(true);
                frmBalance.SetLabelText(string.Empty);
            }
        }
    }
}
