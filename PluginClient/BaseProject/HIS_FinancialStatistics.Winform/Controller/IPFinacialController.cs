using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_FinancialStatistics.Winform.IView;
using HIS_FinancialStatistics.Winform.ViewForm;

namespace HIS_FinancialStatistics.Winform.Controller
{
    /// <summary>
    /// 经管统计控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmFinacialIPDoctor")]
    [WinformView(Name = "FrmFinacialIPDoctor", DllName = "HIS_FinancialStatistics.Winform.dll", ViewTypeName = "HIS_FinancialStatistics.Winform.ViewForm.FrmFinacialIPDoctor")]
    [WinformView(Name = "FrmFinacialItemStatistics", DllName = "HIS_FinancialStatistics.Winform.dll", ViewTypeName = "HIS_FinancialStatistics.Winform.ViewForm.FrmFinacialItemStatistics")]
    [WinformView(Name = "FrmIpatientRevenue", DllName = "HIS_FinancialStatistics.Winform.dll", ViewTypeName = "HIS_FinancialStatistics.Winform.ViewForm.FrmIpatientRevenue")]
    [WinformView(Name = "FrmIPAccountBook", DllName = "HIS_FinancialStatistics.Winform.dll", ViewTypeName = "HIS_FinancialStatistics.Winform.ViewForm.FrmIPAccountBook")]

    public class IPFinacialController : WcfClientController
    {
        /// <summary>
        /// 住院医生工作量统计
        /// </summary>
        IFrmFinacialIPDoctor iFrmFinacialIPDoctor;

        /// <summary>
        /// 单项目收入统计
        /// </summary>
        IFrmFinacialItemStatistics iFrmFinacialItemStatistics;

        /// <summary>
        /// 门诊收入统计
        /// </summary>
        IFrmIpatientRevenue ifrmipatientRevenue;

        /// <summary>
        /// 住院应收款统计
        /// </summary>
        IFrmFinacialIPAccountBook ifrmfinacialIPAccountBook;

        /// <summary>
        /// 初始化菜单
        /// </summary>
        public override void Init()
        {
            iFrmFinacialIPDoctor = (IFrmFinacialIPDoctor)iBaseView["FrmFinacialIPDoctor"];
            iFrmFinacialItemStatistics = (IFrmFinacialItemStatistics)iBaseView["FrmFinacialItemStatistics"];
            ifrmipatientRevenue = (IFrmIpatientRevenue)iBaseView["FrmIpatientRevenue"];
            ifrmfinacialIPAccountBook = (IFrmFinacialIPAccountBook)iBaseView["FrmIPAccountBook"];
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
        /// <returns>获取当前用户名</returns>
        [WinformMethod]
        public string GetCurrentUserName()
        {
            return LoginUserInfo.EmpName;
        }

        /// <summary>
        /// 获取门诊医生统计数据
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="workid">组织结构ID</param>
        /// <param name="queryTimeType">查询时间类型</param>
        /// <returns>门诊医生统计数据</returns>
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

        /// <summary>
        /// 获取病人工作量统计
        /// </summary>
        /// <param name="workID">组织机构Id</param>
        /// <param name="beDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="docTypeValue">医生类型值</param>
        /// <param name="timeTypeValue">时间类型值</param>
        /// <returns>统计结果集</returns>
        [WinformMethod]
        public DataTable QueryInpatientWorkLoad(int workID, string beDate, string endDate, string docTypeValue, string timeTypeValue)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(beDate);//查询开始日期
                request.AddData(endDate);//查询结束日期
                request.AddData(workID);//机构ID
                request.AddData(docTypeValue);//医生类型
                request.AddData(timeTypeValue);//时间类别 0收费时间1缴款时间
            });
            ServiceResponseData retdata = InvokeWcfService("BaseProject.Service", "InpatientWorkLoadController", "QueryInpatientWorkLoad", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }

        /// <summary>
        /// 查询项目信息
        /// </summary>
        /// <returns>项目信息</returns>
        [WinformMethod]
        public DataTable QueryItemInfo()
        {
            ServiceResponseData retdata = InvokeWcfService("BaseProject.Service", "InpatientWorkLoadController", "QueryItemInfo");
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }

        /// <summary>
        /// 取得项目统计数据
        /// </summary>
        /// <param name="workID">组织机构Id</param>
        /// <param name="queryDate">查询日期</param>
        /// <param name="patType">平人类型</param>
        /// <param name="timeType">时间类型</param>
        /// <param name="itemInfo">项目信息</param>
        /// <returns>统计数据集</returns>
        [WinformMethod]
        public DataTable GetItmeItemStatistics(int workID,DateTime queryDate,int patType,string timeType,string itemInfo)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                //查询开始日期
                request.AddData(workID);

                //查询结束日期
                request.AddData(queryDate);

                //机构ID
                request.AddData(patType);

                //医生类型
                request.AddData(timeType);

                //时间类别 0收费时间1缴款时间
                request.AddData(itemInfo);//时间类别 0收费时间1缴款时间
            });
            ServiceResponseData retdata = InvokeWcfService("BaseProject.Service", "InpatientWorkLoadController", "GetItmeItemStatistics", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }

        #region 住院收入统计
        /// <summary>
        /// 住院收入统计
        /// </summary>
        /// <param name="frmName">窗体入口名称</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="workid">组织机构Id</param>
        /// <param name="queryTimeType">查询时间类型</param>
        /// <param name="rowGroupType">组类型</param>
        /// <param name="colGroupType">列组类型</param>
        /// <returns>统计数据集</returns>
        [WinformMethod]
        public DataTable GetFinacialIPRevenueData(string frmName, DateTime bdate, DateTime edate, int workid, int queryTimeType, int rowGroupType, int colGroupType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(bdate);//查询开始日期
                request.AddData(edate);//查询结束日期
                request.AddData(workid);//机构ID
                request.AddData(queryTimeType);//时间类别 0记费时间 1结算时间 2缴款时间
                request.AddData(rowGroupType);//行统计类别 //0开方医生 1开方科室 2执行科室 3主治医生 4病人类型 5病人所在科室
                request.AddData(colGroupType);//列统计方式 0统计大项目 1核算分类 2财务分类 3住院发票分类 4支付方式
            });
            ServiceResponseData retdata = InvokeWcfService("BaseProject.Service", "InpatientWorkLoadController", "GetFinacialIPRevenueData", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }
        #endregion

        #region 住院应收款统计
        /// <summary>
        /// 住院应收款统计
        /// </summary>
        /// <param name="frmName">窗口入口名称</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="workid">组织机构ID</param>
        /// <param name="queryType">查询类型</param>
        /// <returns>统计数据集</returns>
        [WinformMethod]
        public DataTable GetFinacialIPAccountBookData(string frmName, DateTime bdate, DateTime edate, int workid, int queryType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(bdate);//查询开始日期
                request.AddData(edate);//查询结束日期
                request.AddData(workid);//机构ID
                request.AddData(queryType);//时间类别 0收入流水账1预交金流水账              
            });
            ServiceResponseData retdata = InvokeWcfService("BaseProject.Service", "InpatientWorkLoadController", "GetFinacialIPAccountBookData", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }
        #endregion
    }
}
