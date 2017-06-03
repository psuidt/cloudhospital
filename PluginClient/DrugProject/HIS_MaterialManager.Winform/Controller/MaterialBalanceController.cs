using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.MaterialManage;
using HIS_MaterialManage.Winform.IView.FinanceMgr;

namespace HIS_MaterialManage.Winform.Controller
{
    /// <summary>
    /// 物资月结
    /// </summary>
    [WinformController(DefaultViewName = "FrmMaterialBalance")]//与系统菜单对应                           HIS_MaterialManage.Winform.ViewForm
    [WinformView(Name = "FrmMaterialBalance", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmMaterialBalance")]
    public class MaterialBalanceController: WcfClientController
    {
        /// <summary>
        /// 物资月结接口
        /// </summary>
        IFrmMaterialBalance frmBalance;

        /// <summary>
        /// 当前选中科室
        /// </summary>
        private int selectedDeptID = 0;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            frmBalance = (IFrmMaterialBalance)iBaseView["FrmMaterialBalance"];
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
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialInStoreController", "GetDrugDeptList");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmBalance.BindDrugDept(dt);
        }

        /// <summary>
        /// 月结时间
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void GetMonthBalaceByDept(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(MWConstant.OP_MW_MONTHACCOUNT);
                request.AddData(selectedDeptID);
            });
            ServiceResponseData retdata = InvokeWcfService(
                "DrugProject.Service",
                "MatBalanceController",
                "GetMonthBalaceByDept",
                requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            frmBalance.BindDataGrid(dtRtn);
        }

        /// <summary>
        ///  月结日期设置
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        /// <param name="value">天</param>
        [WinformMethod]
        public void SetAccountDay(string frmName, int value)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(MWConstant.OP_MW_MONTHACCOUNT);
                request.AddData(selectedDeptID);
                request.AddData(value);
            });

            ServiceResponseData retdata = InvokeWcfService(
                "DrugProject.Service", 
                "MatBalanceController",
                "SetAccountDay",
                requestAction);
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

        /// <summary>
        /// 获取日结日期
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void GetAccountDay(string frmName)
        {
            Action<ClientRequestData> requestAction = null;

            requestAction = ((ClientRequestData request) =>
            {
                request.AddData(MWConstant.OP_MW_MONTHACCOUNT);
                request.AddData(selectedDeptID);
            });

            ServiceResponseData retdata = InvokeWcfService(
                "DrugProject.Service", 
                "MatBalanceController",
                "GetAccountDay", 
                requestAction);
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
        /// 月结操作
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void MonthAccount(string frmName)
        {
            Action<ClientRequestData> requestAction = null;
            requestAction = ((ClientRequestData request) =>
            {
                request.AddData(MWConstant.OP_MW_MONTHACCOUNT);
                request.AddData(selectedDeptID);
                request.AddData(LoginUserInfo.EmpId);
            });

            frmBalance.SetBtnEnable(false);
            frmBalance.SetLabelText("月结操作执行中，请等待");
            try
            {
                ServiceResponseData retdata = InvokeWcfService(
                    "DrugProject.Service",
                    "MatBalanceController",
                    "MonthAccount",
                    requestAction);
                MWBillResult result = retdata.GetData<MWBillResult>(0);
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
        /// 对账操作
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void SystemCheckAccount(string frmName)
        {
            Action<ClientRequestData> requestAction = null;
            requestAction = ((ClientRequestData request) =>
            {
                request.AddData(MWConstant.OP_MW_MONTHACCOUNT);
                request.AddData(selectedDeptID);
                request.AddData(LoginUserInfo.EmpId);
            });
            frmBalance.SetBtnEnable(false);
            frmBalance.SetLabelText("对账操作执行中，请等待");
            try
            {
                ServiceResponseData retdata = InvokeWcfService(
                    "DrugProject.Service", 
                    "MatBalanceController",
                    "SystemCheckAccount", 
                    requestAction);
                MWSpResult result = retdata.GetData<MWSpResult>(0);
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
