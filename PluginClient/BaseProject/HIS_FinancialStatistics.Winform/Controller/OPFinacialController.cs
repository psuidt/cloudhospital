using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_FinancialStatistics.Winform.IView;

namespace HIS_FinancialStatistics.Winform.Controller
{
    /// <summary>
    /// 经管门诊核算控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmOPFinacial")]
    [WinformView(Name = "FrmFinacialCharger", DllName = "HIS_FinancialStatistics.Winform.dll", ViewTypeName = "HIS_FinancialStatistics.Winform.ViewForm.FrmFinacialCharger")]
    [WinformView(Name = "FrmFinacialOPDoctor", DllName = "HIS_FinancialStatistics.Winform.dll", ViewTypeName = "HIS_FinancialStatistics.Winform.ViewForm.FrmFinacialOPDoctor")]
    [WinformView(Name = "FrmFinacialOPRevenue", DllName = "HIS_FinancialStatistics.Winform.dll", ViewTypeName = "HIS_FinancialStatistics.Winform.ViewForm.FrmFinacialOPRevenue")]
    [WinformView(Name = "FrmFinacialDispense", DllName = "HIS_FinancialStatistics.Winform.dll", ViewTypeName = "HIS_FinancialStatistics.Winform.ViewForm.FrmFinacialDispense")]
    [WinformView(Name = "FrmDeanQuery", DllName = "HIS_FinancialStatistics.Winform.dll", ViewTypeName = "HIS_FinancialStatistics.Winform.ViewForm.FrmDeanQuery")]
    [WinformView(Name = "FrmAllRevenuReport", DllName = "HIS_FinancialStatistics.Winform.dll", ViewTypeName = "HIS_FinancialStatistics.Winform.ViewForm.FrmAllRevenuReport")]
    [WinformView(Name = "FrmAllWorkLoadReport", DllName = "HIS_FinancialStatistics.Winform.dll", ViewTypeName = "HIS_FinancialStatistics.Winform.ViewForm.FrmAllWorkLoadReport")]
    public class OPFinacialController: WcfClientController
    {
        /// <summary>
        /// 收费员工作量统计
        /// </summary>
        IFrmFinacialCharger ifrmFinacialCharger;

        /// <summary>
        /// 医生工作量统计
        /// </summary>
        IFrmFinacialOPDoctor ifrmOpFinacialDoctor;

        /// <summary>
        /// 门诊收入统计
        /// </summary>
        IFrmFinacialOPRevenue ifrmFicacialOpRevenue;

        /// <summary>
        /// 发药人员工作量统计
        /// </summary>
        IFrmFinacialDispense  ifrmFinacialDispense;

        /// <summary>
        /// 院长驾驶舱
        /// </summary>
        IFrmDeanQuery ifrmDeanQuery;

        /// <summary>
        /// 全院收入分析
        /// </summary>
        IFrmAllRevenuReport ifrmAllRevenureport;

        /// <summary>
        /// 全院工作量统计
        /// </summary>
        IFrmAllWorkLoadReport ifrmAllWorkLoadReport;

        /// <summary>
        /// 初始化菜单
        /// </summary>
        public override void Init()
        {
            ifrmFinacialCharger = (IFrmFinacialCharger)iBaseView["FrmFinacialCharger"];
            ifrmOpFinacialDoctor = (IFrmFinacialOPDoctor)iBaseView["FrmFinacialOPDoctor"];
            ifrmFicacialOpRevenue = (IFrmFinacialOPRevenue)iBaseView["FrmFinacialOPRevenue"];
            ifrmFinacialDispense = (IFrmFinacialDispense)iBaseView["FrmFinacialDispense"];
            ifrmDeanQuery = (IFrmDeanQuery)iBaseView["FrmDeanQuery"];
            ifrmAllRevenureport = (IFrmAllRevenuReport)iBaseView["FrmAllRevenuReport"];
            ifrmAllWorkLoadReport = (IFrmAllWorkLoadReport)iBaseView["FrmAllWorkLoadReport"];
        }

        /// <summary>
        /// 获取医疗机构
        /// </summary>
        /// <returns>组织机构数据</returns>
        [WinformMethod]
        public DataTable GetWorker()
        {
            ServiceResponseData retdata = InvokeWcfService("BaseProject.Service", "OPFinacialController", "GetWorkers");
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }

        /// <summary>
        /// 获取当前用户姓名
        /// </summary>
        /// <returns>用户名</returns>
        [WinformMethod]
        public string GetCurrentUserName()
        {
            return LoginUserInfo.EmpName;
        }
        #region 收费员工作量统计
        /// <summary>
        /// 收费员工作量统计
        /// </summary>
        /// <param name="frmName">窗体入口名</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="workid">组织机构id</param>
        /// <param name="queryType">查询类型</param>
        /// <returns>收费员工作量统计数据</returns>
        [WinformMethod]
        public DataTable GetFinacialChargerData(string frmName, DateTime bdate,DateTime edate,int workid,int queryType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(bdate);//查询开始日期
                request.AddData(edate);//查询结束日期
                request.AddData(workid);//机构ID
                request.AddData(queryType);//查询类别 0门诊1住院
            });
            ServiceResponseData retdata = InvokeWcfService("BaseProject.Service", "OPFinacialController", "GetFinacialChargerData", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }
        #endregion

        #region 门诊医生工作量统计
        /// <summary>
        /// 门诊医生工作量统计
        /// </summary>
        /// <param name="frmName">窗体入口名</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="workid">组织机构id</param>
        /// <param name="queryTimeType">查询时间类型</param>
        /// <returns>门诊医生工作量统计数据</returns>
        [WinformMethod]
        public DataTable GetFinacialOPDoctorData(string frmName, DateTime bdate, DateTime edate, int workid, int queryTimeType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(bdate);//查询开始日期
                request.AddData(edate);//查询结束日期
                request.AddData(workid);//机构ID
                request.AddData(queryTimeType);//时间类别 0收费时间1缴款时间
            });
            ServiceResponseData retdata = InvokeWcfService("BaseProject.Service", "OPFinacialController", "GetFinacialOPDoctorData", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }
        #endregion

        #region 门诊收入统计
        /// <summary>
        /// 门诊收入统计
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="workid">组织机构id</param>
        /// <param name="queryTimeType">查询时间类型</param>
        /// <param name="rowGroupType">行组类型</param>
        /// <param name="colGroupType">列祖类型</param>
        /// <returns>门诊收入统计数据</returns>
        [WinformMethod]
        public DataTable GetFinacialOPRevenueData(string frmName, DateTime bdate, DateTime edate, int workid, int queryTimeType,int rowGroupType,int colGroupType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(bdate);//查询开始日期
                request.AddData(edate);//查询结束日期
                request.AddData(workid);//机构ID
                request.AddData(queryTimeType);//时间类别 0收费时间1缴款时间
                request.AddData(rowGroupType);//行统计类别 0开方医生1开方科室2执行科室3开方科室+开方医生4病人类型
                request.AddData(colGroupType);//列统计方式 0统计大项目1核算分类2财务分类3门诊发票分类4支付方式
            });
            ServiceResponseData retdata = InvokeWcfService("BaseProject.Service", "OPFinacialController", "GetFinacialOPRevenueData", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }
        #endregion

        #region 发药人员工作量统计
        /// <summary>
        /// 发药人员工作量统计
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="workid">组织机构id</param>
        /// <returns>发药人员工作量统计数据</returns>
        [WinformMethod]
        public DataTable GetFinacialDispenseData(string frmName, DateTime bdate, DateTime edate, int workid)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(bdate);//查询开始日期
                request.AddData(edate);//查询结束日期
                request.AddData(workid);//机构ID
            });
            ServiceResponseData retdata = InvokeWcfService("BaseProject.Service", "OPFinacialController", "GetFinacialDispenseData", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }
        #endregion

        /// <summary>
        /// 院长报表SSRSWeb服务地址
        /// </summary>
        /// <returns>院长报表SSRSWeb服务地址字符串</returns>
        [WinformMethod]
        public string GetSsrsWebAddress()
        {         
            ServiceResponseData retdata = InvokeWcfService("BaseProject.Service", "OPFinacialController", "GetSsrsWebAddress");
            string ssrsWebAddress = retdata.GetData<string >(0);
            return ssrsWebAddress;
        }

        /// <summary>
        /// SSRS访问用户名
        /// </summary>
        /// <returns>用户名字符串</returns>
        [WinformMethod]
        public string GetSsrsUserName()
        {
            ServiceResponseData retdata = InvokeWcfService("BaseProject.Service", "OPFinacialController", "GetSsrsUserName");
            string ssrsWebAddress = retdata.GetData<string>(0);
            return ssrsWebAddress;
        }

        /// <summary>
        /// SSRS访问密码
        /// </summary>
        /// <returns>密码字符串</returns>
        [WinformMethod]
        public string GetSsrsPWD()
        {
            ServiceResponseData retdata = InvokeWcfService("BaseProject.Service", "OPFinacialController", "GetSsrsPWD");
            string ssrsWebAddress = retdata.GetData<string>(0);
            return ssrsWebAddress;
        }

        /// <summary>
        /// 院长驾驶舱报表地址
        /// </summary>
        /// <returns>院长驾驶舱报表地址字符串</returns>
        [WinformMethod]
        public string GetDeanReportPath()
        {
            ServiceResponseData retdata = InvokeWcfService("BaseProject.Service", "OPFinacialController", "GetDeanReportPath");
            string ssrsWebAddress = retdata.GetData<string>(0);
            return ssrsWebAddress;
        }

        /// <summary>
        /// 全院收益分析报表地址
        /// </summary>
        /// <returns>全院收益分析报表地址字符串</returns>
        [WinformMethod]
        public string GetRevenueReportPath()
        {
            ServiceResponseData retdata = InvokeWcfService("BaseProject.Service", "OPFinacialController", "GetRevenueReportPath");
            string ssrsWebAddress = retdata.GetData<string>(0);
            return ssrsWebAddress;
        }

        /// <summary>
        /// 全院工作量统计报表地址
        /// </summary>
        /// <returns>全院工作量统计报表地址字符串</returns>
        [WinformMethod]
        public string GetWorkLoadReportPath()
        {
            ServiceResponseData retdata = InvokeWcfService("BaseProject.Service", "OPFinacialController", "GetWorkLoadReportPath");
            string ssrsWebAddress = retdata.GetData<string>(0);
            return ssrsWebAddress;
        }
    }
}
