using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.BasicData.BusiEntity;
using HIS_FinancialStatistics.Dao;
using HIS_FinancialStatistics.ObjectModel;

namespace HIS_FinancialStatistics.WcfController
{
    /// <summary>
    /// 病人工作量统计控制器
    /// </summary>
    [WCFController]
    public class InpatientWorkLoadController : WcfServerController
    {
        /// <summary>
        /// 查询病人工作量
        /// </summary>
        /// <returns>病人工作量数据</returns>
        [WCFMethod]
        public ServiceResponseData QueryInpatientWorkLoad()
        {
            string bdate = requestData.GetData<string>(0);
            string edate = requestData.GetData<string>(1);
            int workId = requestData.GetData<int>(2);
            string docTypeValue = requestData.GetData<string>(3);
            string timeTypeValue = requestData.GetData<string>(4);

            DataTable dt = NewObject<InpatientWorkLoadManagement>().QueryInpatientWorkLoad(workId, bdate, edate, docTypeValue, timeTypeValue);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 查询项目数量
        /// </summary>
        /// <returns>项目数量数据</returns>
        [WCFMethod]
        public ServiceResponseData  QueryItemCount()
        {
            string bdate = requestData.GetData<string>(0);
            string edate = requestData.GetData<string>(1);
            int workId = requestData.GetData<int>(2);
            string docTypeValue = requestData.GetData<string>(3);
            string timeTypeValue = requestData.GetData<string>(4);

            DataTable dt = NewObject<InpatientWorkLoadManagement>().QueryInpatientWorkLoad(workId, bdate, edate, docTypeValue, timeTypeValue);
            responseData.AddData(dt);
            return responseData;
        }
        
        /// <summary>
        /// 查询项目信息
        /// </summary>
        /// <returns>项目信息</returns>
        [WCFMethod]
        public ServiceResponseData QueryItemInfo()
        {
            DataTable dt = NewObject<InpatientWorkLoadManagement>().QueryItemInfo();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取项目统计数据
        /// </summary>
        /// <returns>项目统计数据</returns>
        [WCFMethod]
        public ServiceResponseData GetItmeItemStatistics()
        {
            int workId = requestData.GetData<int>(0);
            DateTime bdate = requestData.GetData<DateTime>(1);
            int patType= requestData.GetData<int>(2);
            string timeType= requestData.GetData<string>(3);
            string itemInfo = requestData.GetData<string>(4);

            DataTable dt = NewObject<InpatientWorkLoadManagement>().GetItmeItemStatistics(workId, bdate, patType, timeType, itemInfo);
            responseData.AddData(dt);
            return responseData;
        }

        #region 住院收入统计
        /// <summary>
        /// 住院收入统计
        /// </summary>
        /// <returns>住院收入统计数据</returns>
        [WCFMethod]
        public ServiceResponseData GetFinacialIPRevenueData()
        {
            try
            {
                DateTime bdate = requestData.GetData<DateTime>(0);//开始日期
                DateTime edate = requestData.GetData<DateTime>(1);//结束日期
                int workId = requestData.GetData<int>(2);//机构ID
                int queryTimeType = requestData.GetData<int>(3);//时间类别 0记费时间 1结算时间 2缴款时间
                int rowGroupType = requestData.GetData<int>(4);//行统计类别 //0开方医生 1开方科室 2执行科室 3主治医生 4病人类型 5病人所在科室
                int colGroupType = requestData.GetData<int>(5);//列统计方式  0统计大项目 1核算分类 2财务分类 3住院发票分类 4支付方式
                DataTable dt = NewObject<IWorkLoadDao>().GetFinacialIPRevenueData(workId, queryTimeType, bdate, edate, rowGroupType, colGroupType);
                responseData.AddData(dt);
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 住院应收款统计
        /// <summary>
        /// 住院应收款统计
        /// </summary>
        /// <returns>住院应收款统计数据</returns>
        [WCFMethod]
        public ServiceResponseData GetFinacialIPAccountBookData()
        {
            try
            {
                DateTime bdate = requestData.GetData<DateTime>(0);//开始日期
                DateTime edate = requestData.GetData<DateTime>(1);//结束日期
                int workId = requestData.GetData<int>(2);//机构ID
                int queryType = requestData.GetData<int>(3);//时间类别 0记费时间 1结算时间 2缴款时间                
                DataTable dt = NewObject<IWorkLoadDao>().GetFinacialIPAccountBookData(workId, queryType, bdate, edate);
                responseData.AddData(dt);
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
