using System;
using System.Collections.Generic;
using System.Data;
using EfwControls.Common;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_IPNurse.Winform.IView;

namespace HIS_IPNurse.Winform.Controller
{
    /// <summary>
    /// 执行单打印控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmExecBillRecord")]//在菜单上显示
    [WinformView(Name = "FrmExecBillRecord", DllName = "HIS_IPNurse.Winform.dll", ViewTypeName = "HIS_IPNurse.Winform.ViewForm.FrmExecBillRecord")]
    public class ExecBillRecordController : WcfClientController
    {
        /// <summary>
        /// 护士站医嘱发送接口
        /// </summary>
        IExecBillRecord mIExecBillRecord;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            mIExecBillRecord = (IExecBillRecord)DefaultView;
        }

        /// <summary>
        /// 获取科室列表
        /// </summary>
        [WinformMethod]
        public void GetDeptList()
        {
            // 通过WCF调用服务端控制器取得住院临床科室列表
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "ExecBillRecordController", "GetDeptList");
            DataTable deptDt = retdata.GetData<DataTable>(0);
            mIExecBillRecord.Bind_DeptList(deptDt, LoginUserInfo.DeptId);
        }

        /// <summary>
        /// 获取执行单类型列表
        /// </summary>
        [WinformMethod]
        public void GetReportTypeList()
        {
            // 通过WCF调用服务端控制器取得住院临床科室列表
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "ExecBillRecordController", "GetReportTypeList");
            DataTable dtReportType = retdata.GetData<DataTable>(0);
            mIExecBillRecord.Bind_ReportTypeList(dtReportType);
        }

        /// <summary>
        /// 获取执行单数据
        /// </summary>
        /// <param name="iDeptId">科室ID</param>
        /// <param name="iType">类型</param>
        /// <param name="dFeeDate">费用日期</param>
        /// <param name="iOrderCategory">医嘱类别</param>
        /// <param name="iState">状态</param>
        [WinformMethod]
        public void GetExcuteList(int iDeptId, int iType, DateTime dFeeDate, int iOrderCategory, int iState,bool typeName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iDeptId);
                request.AddData(iType);
                request.AddData(dFeeDate);
                request.AddData(iOrderCategory);
                request.AddData(iState);
                request.AddData(typeName);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "ExecBillRecordController", "GetExcuteList", requestAction);
            DataTable dtExcuteList = retdata.GetData<DataTable>(0);
            Tools.ChangeDateTimeNullValue(ref dtExcuteList, new List<string> { "PrintDate" });
            DataView dv = new DataView(dtExcuteList);
            string[] colName = { "checked", "PatListID", "BedCode", "PatName", "SerialNumber" };
            DataTable dtPatList = dv.ToTable(true, colName);
            mIExecBillRecord.Bind_ExcuteList(dtPatList, dtExcuteList);
        }

        /// <summary>
        /// 设置执行单状态
        /// </summary>
        /// <param name="iExecIdList">执行单列表</param>
        /// <param name="iState">状态</param>
        [WinformMethod]
        public void SetExcuteList(List<int> iExecIdList, int iState)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iExecIdList);
                request.AddData(iState);
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "ExecBillRecordController", "SetExcuteList", requestAction);
            bool b = retdata.GetData<bool>(0);
            if (b)
            {
                mIExecBillRecord.RefreshExcuteList();
            }
            else
            {
                MessageBoxShowError("设置失败！");
            }
        }
    }
}