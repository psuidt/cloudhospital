using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EfwControls.Common;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_IPNurse.Winform.IView;

namespace HIS_IPNurse.Winform.Controller
{
    /// <summary>
    /// 检查检验控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmExamList")]//在菜单上显示
    [WinformView(Name = "FrmExamList", DllName = "HIS_IPNurse.Winform.dll", ViewTypeName = "HIS_IPNurse.Winform.ViewForm.FrmExamList")]
    [WinformView(Name = "FrmPrintExamConfirm", DllName = "HIS_IPNurse.Winform.dll", ViewTypeName = "HIS_IPNurse.Winform.ViewForm.FrmPrintExamConfirm")]
    public class ExamListController : WcfClientController
    {
        /// <summary>
        /// 护士站医嘱发送接口
        /// </summary>
        IExamList iExamList;

        /// <summary>
        /// 瓶签打印确认接口
        /// </summary>
        IPrintExamConfirm iprintExamConfirm;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            iExamList = (IExamList)DefaultView;
            iprintExamConfirm = iBaseView["FrmPrintExamConfirm"] as IPrintExamConfirm;
        }

        /// <summary>
        /// 获取科室列表
        /// </summary>
        [WinformMethod]
        public void GetDeptList()
        {
            // 通过WCF调用服务端控制器取得住院临床科室列表
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "ExamListController", "GetDeptList");
            DataTable deptDt = retdata.GetData<DataTable>(0);
            iExamList.Bind_DeptList(deptDt, LoginUserInfo.DeptId);
        }

        /// <summary>
        /// 获取执行单数据
        /// </summary>
        /// <param name="iDeptId">科室ID</param>
        /// <param name="iApplyType">执行单类型</param>
        /// <param name="dApplyDate">执行日期</param>
        /// <param name="iOrderCategory">医嘱类别</param>
        /// <param name="iState">状态</param>
        [WinformMethod]
        public void GetExamList(int iDeptId, int iApplyType, DateTime dApplyDate, int iOrderCategory, int iState)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iDeptId);
                request.AddData(iApplyType);
                request.AddData(dApplyDate);
                request.AddData(iOrderCategory);
                request.AddData(iState);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "ExamListController", "GetExamList", requestAction);
            DataTable dtExam = retdata.GetData<DataTable>(0);

            DataView dv = new DataView(dtExam);
            string[] colName = { "checked", "BedNo", "PatName", "SerialNumber" };
            DataTable dtPatList = dv.ToTable(true, colName);
            iExamList.LoadExamList(dtPatList, dtExam);
        }

        /// <summary>
        /// 更新打印状态
        /// </summary>
        /// <param name="iApplyHeadIDList">执行单ID集合</param>
        [WinformMethod]
        public void UpdateApplyPrint(List<int> iApplyHeadIDList)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iApplyHeadIDList);
                request.AddData(LoginUserInfo.EmpId);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "ExamListController", "UpdateApplyPrint", requestAction);
            bool bResult = retdata.GetData<bool>(0);
            if (bResult)
            {
                iExamList.RefreshExamList();
            }
        }

        /// <summary>
        /// 提示消息
        /// </summary>
        /// <param name="msg">消息内容</param>
        [WinformMethod]
        public void MessageShow(string msg)
        {
            MessageBoxShowSimple(msg);
        }

        /// <summary>
        /// 弹出打印确认界面
        /// </summary>
        /// <param name="printDt">待打印数据</param>
        [WinformMethod]
        public void ShowFrmPrintExamConfirm(DataTable printDt)
        {
            iprintExamConfirm.Bind_PrintDt(printDt);
            ((Form)iBaseView["FrmPrintExamConfirm"]).ShowDialog();
        }

        /// <summary>
        /// 打印瓶签报表
        /// </summary>
        /// <param name="dt">报表数据</param>
        /// <param name="isPrint">true：打印/false：预览</param>
        [WinformMethod]
        public void PrintReport(DataTable dt, bool isPrint)
        {
            // 做成报表数据
            DataTable reportDt = dt.Clone();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int num = Tools.ToInt32(dt.Rows[i]["Num"]);
                for (int j = 0; j < num; j++)
                {
                    reportDt.Rows.Add(dt.Rows[i].ItemArray);
                }
            }

            if (reportDt.Rows.Count > 0)
            {
                if (isPrint)
                {
                    // 打印报表
                    ReportTool.GetReport(LoginUserInfo.WorkId, 3019, 0, null, reportDt).Print(true);
                    iprintExamConfirm.CloseForm();  // 关闭界面
                }
                else
                {
                    // 预览报表
                    ReportTool.GetReport(LoginUserInfo.WorkId, 3019, 0, null, reportDt).PrintPreview(true);
                }
            }
        }
    }
}