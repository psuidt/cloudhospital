using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_FinancialStatistics.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_FinancialStatistics.WcfController
{
    /// <summary>
    /// 经管药品医生统计控制器
    /// </summary>
    [WCFController]
    public class FinacialDrugDocCountController : WcfServerController
    {
        /// <summary>
        /// 获取机构名称
        /// </summary>
        /// <returns>机构名称</returns>
        [WCFMethod]
        public ServiceResponseData GetWorkers()
        {            
            DataTable dt = NewObject<BasicDataManagement>().GetWorkers();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取医生
        /// </summary>
        /// <returns>医生数据</returns>
        [WCFMethod]
        public ServiceResponseData GetDoctors()
        {
            int iworkId = requestData.GetData<int>(0);  // 机构
            // 获取医生列表
            DataTable currDoctorDt = NewObject<BasicDataManagement>().GetBasicData(EmpDataSourceType.医生, iworkId, true);
            responseData.AddData(currDoctorDt);
            return responseData;
        }

        /// <summary>
        /// 获取报表数据-药品医生开方数量
        /// </summary>
        /// <returns>药品医生开方数量</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugDocCount()
        {            
            int iworkId = requestData.GetData<int>(0); 
            int iDocId = requestData.GetData<int>(1); 
            int iType = requestData.GetData<int>(2); 
            DateTime bDate = requestData.GetData<DateTime>(3); 
            DateTime eDate = requestData.GetData<DateTime>(4);  
    
            // 取得住院病人列表
            DataTable dtDrugDocCount = NewDao<IFinacialDrugDocCount>().GetDrugDocCount(iworkId, iDocId, iType, bDate, eDate);
            responseData.AddData(dtDrugDocCount);
            return responseData;
        }
    }
}
