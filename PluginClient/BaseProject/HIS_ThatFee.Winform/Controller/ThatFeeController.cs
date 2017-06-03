using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using HIS_ThatFee.Winform.IView;

namespace HIS_ThatFee.Winform.Controller
{
    /// <summary>
    /// 医技确费控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmThatFee")]
    [WinformView(Name = "FrmThatFee", DllName = "HIS_ThatFee.Winform.dll", ViewTypeName = "HIS_ThatFee.Winform.ViewForm.FrmThatFee")]
    [WinformView(Name = "FrmThatFeeCount", DllName = "HIS_ThatFee.Winform.dll", ViewTypeName = "HIS_ThatFee.Winform.ViewForm.FrmThatFeeCount")]
    [WinformView(Name = "FrmThatFeeQuery", DllName = "HIS_ThatFee.Winform.dll", ViewTypeName = "HIS_ThatFee.Winform.ViewForm.FrmThatFeeQuery")]
    public class ThatFeeController : WcfClientController
    {
        /// <summary>
        /// 医技确费界面接口
        /// </summary>
        IFrmThatFee ifrmThatFee;

        /// <summary>
        /// 医技项目工作量统计界面接口
        /// </summary>
        IFrmThatFeeCount ifrmThatFeeCount;

        /// <summary>
        /// 医技项目明细查询界面接口
        /// </summary>
        IFrmThatFeeQuery ifrmThatFeeQuery;

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            ifrmThatFee = (IFrmThatFee)iBaseView["FrmThatFee"];
            ifrmThatFeeCount = (IFrmThatFeeCount)iBaseView["FrmThatFeeCount"];
            ifrmThatFeeQuery = (IFrmThatFeeQuery)iBaseView["FrmThatFeeQuery"];
        }
        #region 医技确费

        /// <summary>
        /// 获取确费网格信息
        /// </summary>
        [WinformMethod]
        public void GetThatFee()
        {
            ifrmThatFee.GetQueryWhere();
            var retdata = InvokeWcfService(
                "BaseProject.Service",
                "ThatFeeController",
                "GetThatFee",
                (request) =>
                {
                    request.AddData(ifrmThatFee.DeptId);
                    request.AddData(ifrmThatFee.SystemType);
                    request.AddData(ifrmThatFee.ClincDeptId);
                    request.AddData(ifrmThatFee.IsCheck);
                    request.AddData(ifrmThatFee.IsTest);
                    request.AddData(ifrmThatFee.IsTreat);
                    request.AddData(ifrmThatFee.IsNotThatFee);
                    request.AddData(ifrmThatFee.IsThatFee);
                    request.AddData(ifrmThatFee.StrNO);
                });
            var dt = retdata.GetData<DataTable>(0);
            ifrmThatFee.BindThatFee(dt);
        }

        /// <summary>
        /// 获取执行科室
        /// </summary>
        /// <param name="frmName">窗体入口名称</param>
        [WinformMethod]
        public void GetDept(string frmName)
        {
            var retdata = InvokeWcfService(
                "BaseProject.Service",
                "ThatFeeController",
                "GetDept");
            var dt = retdata.GetData<DataTable>(0);
            if (frmName == "FrmThatFee")
            {
                ifrmThatFee.BindDept(dt);
            }
            else if (frmName == "FrmThatFeeQuery")
            {
                ifrmThatFeeQuery.BindDept(dt);
            }
            else
            {
                ifrmThatFeeCount.BindDept(dt);
            }
        }

        /// <summary>
        /// 获取开放科室
        /// </summary>
        /// <param name="systemType">系统类型</param>
        [WinformMethod]
        public void GetClincDept(int systemType)
        {
            var retdata = InvokeWcfService(
                "BaseProject.Service",
                "ThatFeeController",
                "GetClincDept",
                (request) =>
                {
                    request.AddData(systemType);
                });
            var dt = retdata.GetData<DataTable>(0);
            ifrmThatFee.BindClincDept(dt);
        }

        /// <summary>
        /// 获取门诊费用明细
        /// </summary>
        /// <param name="presId">处方id</param>
        [WinformMethod]
        public void GetOPFee(int presId)
        {
            var retdata = InvokeWcfService(
                "BaseProject.Service",
                "ThatFeeController",
                "GetOPFee",
                (request) =>
                {
                    request.AddData(presId);
                });
            var dt = retdata.GetData<DataTable>(0);
            ifrmThatFee.BindFee(dt);
        }

        /// <summary>
        /// 获取住院费用明细
        /// </summary>
        /// <param name="presId">处方id</param>
        [WinformMethod]
        public void GetIPFee(int presId)
        {
            var retdata = InvokeWcfService(
                "BaseProject.Service",
                "ThatFeeController",
                "GetIPFee",
                (request) =>
                {
                    request.AddData(presId);
                });
            var dt = retdata.GetData<DataTable>(0);
            ifrmThatFee.BindFee(dt);
        }

        /// <summary>
        /// 医技确费
        /// </summary>
        /// <param name="ids">确费明细ID</param>
        [WinformMethod]
        public void ThatFee(string ids)
        {
            var retdata = InvokeWcfService(
            "BaseProject.Service",
            "ThatFeeController",
            "ThatFee",
           (request) =>
           {
               request.AddData(ids);
               request.AddData(LoginUserInfo.EmpId);
               request.AddData(LoginUserInfo.EmpName);
               request.AddData(ifrmThatFee.SystemType);
           });
            var result = retdata.GetData<string>(0);
            ifrmThatFee.CompleteMessage(result);
        }

        /// <summary>
        /// 医技取消确费
        /// </summary>
        /// <param name="ids">确费明细ID</param>
        [WinformMethod]
        public void CancelThatFee(string ids)
        {
            var retdata = InvokeWcfService(
            "BaseProject.Service",
            "ThatFeeController",
            "CancelThatFee",
           (request) =>
           {
               request.AddData(ids);
               request.AddData(LoginUserInfo.EmpId);
               request.AddData(LoginUserInfo.EmpName);
               request.AddData(ifrmThatFee.SystemType);
           });
            var result = retdata.GetData<string>(0);
            ifrmThatFee.CompleteMessage(result);
        }

        #endregion

        #region 医技工作量统计
        /// <summary>
        /// 获取医技工作量统计信息
        /// </summary>
        [WinformMethod]
        public void GetThatFeeCount()
        {
            ifrmThatFeeCount.GetQueryWhere();
            var retdata = InvokeWcfService(
                "BaseProject.Service",
                "ThatFeeController",
                "GetThatFeeCount",
                (request) =>
                {
                    request.AddData(ifrmThatFeeCount.ConfirDeptID);
                    request.AddData(ifrmThatFeeCount.BeginDate);
                    request.AddData(ifrmThatFeeCount.EndDate);
                });
            var dt = retdata.GetData<DataTable>(0);
            ifrmThatFeeCount.BindThatFeeCount(dt);
        }
        #endregion

        #region 医技明细查询
        /// <summary>
        /// 根据执行科室ID获取医技项目
        /// </summary>
        /// <param name="deptId">执行科室id</param>
        [WinformMethod]
        public void GetExamItem(string deptId)
        {
            var retdata = InvokeWcfService(
                "BaseProject.Service",
                "ThatFeeController",
                "GetExamItem",
                (request) =>
                {
                    request.AddData(deptId);
                });
            var dt = retdata.GetData<DataTable>(0);
            ifrmThatFeeQuery.BindExecShowCard(dt);
        }

        /// <summary>
        /// 获取确费网格信息
        /// </summary>
        [WinformMethod]
        public void GetThatFeeDetail()
        {
            ifrmThatFeeQuery.GetQueryWhere();
            var retdata = InvokeWcfService(
                "BaseProject.Service",
                "ThatFeeController",
                "GetThatFeeDetail",
                (request) =>
                {
                    request.AddData(ifrmThatFeeQuery.ConfirDeptID);
                    request.AddData(ifrmThatFeeQuery.SystemType);
                    request.AddData(ifrmThatFeeQuery.BeginDate);
                    request.AddData(ifrmThatFeeQuery.EndDate);
                    request.AddData(ifrmThatFeeQuery.ItemIDs);
                });
            var dt = retdata.GetData<DataTable>(0);
            ifrmThatFeeQuery.BindThatFee(dt);
        }
        #endregion
    }
}
