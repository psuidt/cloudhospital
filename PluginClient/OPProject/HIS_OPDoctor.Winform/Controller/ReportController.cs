using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_OPDoctor.Winform.IView;

namespace HIS_OPDoctor.Winform.Controller
{
    /// <summary>
    /// 报表控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmDocWorkQuery")]//与系统菜单对应
    //门诊医生工作量统计
    [WinformView(Name = "FrmDocWorkQuery", DllName = "HIS_OPDoctor.Winform.dll", ViewTypeName = "HIS_OPDoctor.Winform.ViewForm.FrmDocWorkQuery")]
    //住院医生工作量统计
    [WinformView(Name = "FrmIPDocWorkQuery", DllName = "HIS_OPDoctor.Winform.dll", ViewTypeName = "HIS_OPDoctor.Winform.ViewForm.FrmIPDocWorkQuery")]
    public class ReportController : WcfClientController
    {
        /// <summary>
        /// 门诊医生个人工作量界面接口
        /// </summary>
        IFrmDocWorkQuery iFrmDocWorkQuery;

        /// <summary>
        /// 初始化菜单
        /// </summary>
        public override void Init()
        {
            iFrmDocWorkQuery = (IFrmDocWorkQuery)iBaseView["FrmDocWorkQuery"];
        }

        /// <summary>
        /// 查询门诊医生工作量
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <returns>门诊医生工作量数据</returns>
        [WinformMethod]
        public DataTable GetOPDoctorWorkLoad(DateTime bdate, DateTime edate)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.EmpId);
                request.AddData(bdate);
                request.AddData(edate);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "ReportController", "GetOPDoctorWorkLoad", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }

        /// <summary>
        /// 查询住院医生工作量
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <returns>住院医生工作量数据</returns>
        [WinformMethod]
        public DataTable GetIPDoctorWorkLoad(DateTime bdate, DateTime edate)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.EmpId);
                request.AddData(Convert.ToDateTime( bdate.ToString("yyyy-MM-dd HH:mm:ss")));
                request.AddData(Convert.ToDateTime( edate.ToString("yyyy-MM-dd HH:mm:ss")));
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "ReportController", "GetIPDoctorWorkLoad", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }
    }
}
