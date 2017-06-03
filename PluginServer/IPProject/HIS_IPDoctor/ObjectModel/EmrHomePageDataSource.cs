using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPDoctor.Dao;

namespace HIS_IPDoctor.ObjectModel
{
    /// <summary>
    /// 病案首页数据源获取
    /// </summary>
    public class EmrHomePageDataSource : AbstractObjectModel
    {
        /// <summary>
        ///  获取病人基本信息
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <returns>病人信息</returns>
        public DataTable GetCasePatientInfo(int patlistid)
        {
            DataTable dt = NewDao<IEmrHomePageDao>().GetCasePatientInfo(patlistid);
            return dt;
        }

        /// <summary>
        /// 获取病人转科信息
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <returns>转科信息</returns>
        public DataTable GetCaseTransDeptInfo(int patlistid)
        {
            DataTable dt = NewDao<IEmrHomePageDao>().GetCasePatTransDeptInfo(patlistid);
            return dt;
        }

        /// <summary>
        /// 获取病人诊断信息
        /// </summary>
        /// <param name="patlistid">病人Id</param>
        /// <returns>诊断信息</returns>
        public DataTable GetCasediagInfo(int patlistid)
        {
            DataTable dt = NewDao<IEmrHomePageDao>().GetCasePatDiagInfo(patlistid);
            return dt;
        }

        /// <summary>
        /// 获取病案首页住院病人费用信息
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <returns>住院病人费用信息</returns>
        public DataTable GetCasePatFee(int patlistid)
        {
            DataTable dt = NewDao<IEmrHomePageDao>().GetCasePatFee(patlistid);
            return dt;
        }

        /// <summary>
        /// 获取病人总费用信息
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <returns>返回病人总费用信息</returns>
        public DataTable GetCasePatTotalFee(int patlistid)
        {
            DataTable dt = NewDao<IEmrHomePageDao>().GetCasePatTotalFee(patlistid);
            return dt;
        }

        /// <summary>
        /// 获取抗菌药物费用
        /// </summary>
        /// <param name="patlistID">病人ID</param>
        /// <returns>抗菌药物费用</returns>
        public decimal GetAntFee(int patlistID)
        {
            return NewDao<IEmrHomePageDao>().GetAntFee(patlistID);
        }

        /// <summary>
        /// 获取在院病人费用信息
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <returns>在院病人费用信息</returns>
        public DataTable GetCasePatInHospFee(int patlistid)
        {
            DataTable dt = NewDao<IEmrHomePageDao>().GetCasePatFeeInHospital(patlistid);
            return dt;
        }

        /// <summary>
        /// 获取在院病人总费用信息
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <returns>总费用信息</returns>
        public DataTable GetCasePatInHospTotalFee(int patlistid)
        {
            DataTable dt = NewDao<IEmrHomePageDao>().GetCasePatTotalFeeInHospital(patlistid);
            return dt;
        }
    }
}
