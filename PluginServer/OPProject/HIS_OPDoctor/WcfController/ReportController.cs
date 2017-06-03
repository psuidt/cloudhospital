using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_OPDoctor.Dao;

namespace HIS_OPDoctor.WcfController
{
    /// <summary>
    /// 报表服务控制器
    /// 1.医生个人工作量统计
    /// </summary>
    [WCFController]
    public class ReportController : WcfServerController
    {
        /// <summary>
        /// 门诊医生工作量统计
        /// </summary>
        /// <returns>门诊医生工作量统计数据</returns>
        [WCFMethod]
        public ServiceResponseData GetOPDoctorWorkLoad()
        {
            try
            {
                int doctorId = requestData.GetData<int>(0);//医生Id
                DateTime bdate = requestData.GetData<DateTime>(1);//开始日期
                DateTime edate = requestData.GetData<DateTime>(2);//结束日期
                DataTable dt =NewDao<IOPDDao>().GetOPDoctorWorkLoad(doctorId,bdate, edate);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //添加合计
                    dt = AddSum(dt);
                }

                responseData.AddData(dt);
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 住院医生工作量统计
        /// </summary>
        /// <returns>住院医生工作量统计数据</returns>
        [WCFMethod]
        public ServiceResponseData GetIPDoctorWorkLoad()
        {
            try
            {
                int doctorId = requestData.GetData<int>(0);//医生Id
                DateTime bdate = requestData.GetData<DateTime>(1);//开始日期
                DateTime edate = requestData.GetData<DateTime>(2);//结束日期
                DataTable dt = NewDao<IOPDDao>().GetIPDoctorWorkLoad(doctorId, bdate, edate);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //添加合计
                    dt = AddSum(dt);
                }

                responseData.AddData(dt);
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 添加总计行
        /// </summary>
        /// <param name="dt">统计数据</param>
        /// <returns>添加完成绩的统计数据</returns>
        private DataTable AddSum(DataTable dt)
        {
           decimal totalFee = Convert.ToDecimal(dt.Compute("sum(Fees)", string.Empty));
            DataRow newRow = dt.NewRow();
            newRow["ItemType"] = "总计";
            newRow["Fees"] = totalFee;
            newRow["Flag"] = 1;//总计
            dt.Rows.Add(newRow);
            return dt;
        }
    }
}
