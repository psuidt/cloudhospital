using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.BasicData.BusiEntity;
using HIS_FinancialStatistics.Dao;

namespace HIS_FinancialStatistics.ObjectModel
{
    /// <summary>
    /// 住院工作量统计管理
    /// </summary>
    public class InpatientWorkLoadManagement : AbstractObjectModel
    {
        /// <summary>
        /// 统计住院病人工作量
        /// </summary>
        /// <param name="workID">组织机构id</param>
        /// <param name="beDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="docTypeValue">医生类型</param>
        /// <param name="timeTypeValue">时间类型</param>
        /// <returns>住院工作量</returns>
        public DataTable QueryInpatientWorkLoad(int workID,string beDate,string endDate,string docTypeValue, string timeTypeValue)
        {
             DataTable dt=NewDao<IWorkLoadDao>().QueryInpatientWorkLoad(workID, beDate, endDate,  docTypeValue, timeTypeValue);
            return dt;
        }

        /// <summary>
        /// 查询项目信息
        /// </summary>
        /// <returns>项目信息</returns>
        public DataTable QueryItemInfo()
        {
            return NewDao<IWorkLoadDao>().QueryItemInfo();
        }

        /// <summary>
        /// 获取项目统计数据
        /// </summary>
        /// <param name="workID">组织机构id</param>
        /// <param name="month">月份</param>
        /// <param name="patType">病人类型</param>
        /// <param name="timeType">时间类型</param>
        /// <param name="itemInfo">项目信息</param>
        /// <returns>项目统计数据</returns>
        public DataTable GetItmeItemStatistics(int workID,DateTime month,int patType,string timeType,string itemInfo)
        {
            DateTime tempDate = new DateTime(month.Year, month.Month, 1);
            string beDate= tempDate.ToString("yyyy-MM-dd")+" 00:00:00";
            string endDate = (new DateTime(tempDate.Year, tempDate.Month, DateTime.DaysInMonth(tempDate.Year, tempDate.Month))).ToString("yyyy-MM-dd") + " 23:59:59"; 

            DateTime dateMOM = DateTime.Now.AddMonths(-1);

            string beDateMOM= (new DateTime(dateMOM.Year, dateMOM.Month, 1)).ToString(); 
            string endDateMOM= (new DateTime(dateMOM.Year, dateMOM.Month, DateTime.DaysInMonth(dateMOM.Year, dateMOM.Month))).ToString("yyyy-MM-dd") + " 23:59:59"; 

            DateTime dateYOY= DateTime.Now.AddYears(-1);
            string beDateYOY = (new DateTime(dateYOY.Year, dateYOY.Month, 1) ).ToString();
            string endDateYOY = (new DateTime(dateYOY.Year, dateYOY.Month, DateTime.DaysInMonth(dateYOY.Year, dateYOY.Month))).ToString("yyyy-MM-dd") + " 23:59:59";

            DataTable dt = NewDao<IWorkLoadDao>().GetItmeItemStatistics(workID,patType,timeType,beDate,endDate,beDateMOM,endDateMOM, beDateYOY, endDateYOY, itemInfo);
            return dt;
        }
    }
}
