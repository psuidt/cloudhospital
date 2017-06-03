using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.BasicData;
using HIS_Entity.MemberManage;
using HIS_Entity.OPManage;
using HIS_FinancialStatistics.Dao;
using HIS_OPManage.ObjectModel;

namespace HIS_FinancialStatistics.WcfController
{
    /// <summary>
    /// 经管统计控制器
    /// </summary>
    [WCFController]
    public class OPFinacialController : WcfServerController
    {
        /// <summary>
        /// 获取机构名称
        /// </summary>
        /// <returns>机构名称数据</returns>
        [WCFMethod]
        public ServiceResponseData GetWorkers()
        {
            DataTable dt = NewObject<BaseWorkers>().gettable(" DelFlag=0");
            responseData.AddData(dt);
            return responseData;
        }

        #region  收费员工作量统计
        /// <summary>
        /// 收费员工作量统计
        /// </summary>
        /// <returns>收费员工作量统计数据</returns>
        [WCFMethod]
        public ServiceResponseData GetFinacialChargerData()
        {
            try
            {
                DateTime bdate = requestData.GetData<DateTime>(0);//开始日期
                DateTime edate = requestData.GetData<DateTime>(1);//结束日期
                int workId = requestData.GetData<int>(2);//机构ID
                int queryType = requestData.GetData<int>(3);//查询类型 0门诊 1住院
                DataTable dt = NewObject<IFinacialDao>().GetFinacialChargerData(workId, queryType, bdate, edate);

                responseData.AddData(dt);
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 门诊医生工作量统计
        /// <summary>
        /// 门诊医生工作量统计
        /// </summary>
        /// <returns>门诊医生工作量统计数据</returns>
        [WCFMethod]
        public ServiceResponseData GetFinacialOPDoctorData()
        {
            try
            {
                DateTime bdate = requestData.GetData<DateTime>(0);//开始日期
                DateTime edate = requestData.GetData<DateTime>(1);//结束日期
                int workId = requestData.GetData<int>(2);//机构ID
                int queryTimeType = requestData.GetData<int>(3);//时间类型 0收费时间 1缴款时间
                DataTable dt = NewObject<IFinacialDao>().GetFinacialOPDoctorData(workId, queryTimeType, bdate, edate);
                responseData.AddData(dt);
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 门诊收入统计
        /// <summary>
        /// 门诊收入统计
        /// </summary>
        /// <returns>门诊收入统计数据</returns>
        [WCFMethod]
        public ServiceResponseData GetFinacialOPRevenueData()
        {
            try
            {
                DateTime bdate = requestData.GetData<DateTime>(0);//开始日期
                DateTime edate = requestData.GetData<DateTime>(1);//结束日期
                int workId = requestData.GetData<int>(2);//机构ID
                int queryTimeType = requestData.GetData<int>(3);//时间类型 0收费时间 1缴款时间
                int rowGroupType = requestData.GetData<int>(4);//行统计类别 0开方医生1开方科室2执行科室3开方科室+开方医生4病人类型
                int colGroupType = requestData.GetData<int>(5);//列统计方式 0统计大项目1核算分类2财务分类3门诊发票分类4支付方式
                DataTable dt = NewObject<IFinacialDao>().GetFinacialOPRevenueData(workId, queryTimeType, bdate, edate, rowGroupType, colGroupType);
                responseData.AddData(dt);
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region  发药人员工作量统计
        /// <summary>
        /// 发药人员工作量统计
        /// </summary>
        /// <returns>发药人员工作量统计数据</returns>
        [WCFMethod]
        public ServiceResponseData GetFinacialDispenseData()
        {
            try
            {
                DateTime bdate = requestData.GetData<DateTime>(0);//开始日期
                DateTime edate = requestData.GetData<DateTime>(1);//结束日期
                int workId = requestData.GetData<int>(2);//机构ID
                DataTable dt = NewObject<IFinacialDao>().GetFinacialDispenseData(workId, bdate, edate);

                responseData.AddData(dt);
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 取得web地址
        /// </summary>
        /// <returns>web地址</returns>
        [WCFMethod]
        public ServiceResponseData GetSsrsWebAddress()
        {
            string ssrsWebAddress = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.SSRSWebAddress);//院长报表SSRSWeb服务地址

            responseData.AddData(ssrsWebAddress);

            return responseData;
        }

        /// <summary>
        /// 取得ssrs用户名
        /// </summary>
        /// <returns>ssrs用户名</returns>
        [WCFMethod]
        public ServiceResponseData GetSsrsUserName()
        {
            string ssrsUserName = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.SSRSUserName);//SSRS访问用户名
            responseData.AddData(ssrsUserName);
            return responseData;
        }

        /// <summary>
        /// 取得密码
        /// </summary>
        /// <returns>密码</returns>
        [WCFMethod]
        public ServiceResponseData GetSsrsPWD()
        {
            string ssrsPWD = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.SSRSPWD);//SSRS访问密码           

            responseData.AddData(ssrsPWD);
            return responseData;
        }

        /// <summary>
        /// 取得报表路径
        /// </summary>
        /// <returns>报表路径</returns>
        [WCFMethod]
        public ServiceResponseData GetDeanReportPath()
        {
            string deanReportPath = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.DeanReportPath);//院长驾驶舱报表地址           

            responseData.AddData(deanReportPath);
            return responseData;
        }

        /// <summary>
        /// 获取报表路径
        /// </summary>
        /// <returns>报表路径</returns>
        [WCFMethod]
        public ServiceResponseData GetRevenueReportPath()
        {
            string revenueReportPath = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.RevenueReportPath);//全院收益分析报表地址           

            responseData.AddData(revenueReportPath);
            return responseData;
        }

        /// <summary>
        /// 获取工作量报表路径
        /// </summary>
        /// <returns>工作量报表路径</returns>
        [WCFMethod]
        public ServiceResponseData GetWorkLoadReportPath()
        {
            string workLoadReportPath = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.WorkLoadReportPath);//全院工作量统计报表地址           

            responseData.AddData(workLoadReportPath);
            return responseData;
        }
    }
}
