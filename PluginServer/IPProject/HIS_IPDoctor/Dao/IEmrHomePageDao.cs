using System.Data;

namespace HIS_IPDoctor.Dao
{
    /// <summary>
    /// IEmrHomePageDao接口定义
    /// </summary>
    public interface IEmrHomePageDao
    {
        /// <summary>
        /// 获取病案首页病人基本信息
        /// </summary>
        /// <param name="patlistID">病人ID</param>
        /// <returns>病人基本信息</returns>
        DataTable GetCasePatientInfo(int patlistID);

        /// <summary>
        /// 获取病人转科信息
        /// </summary>
        /// <param name="patlistID">病人ID</param>
        /// <returns>转科信息</returns>
        DataTable GetCasePatTransDeptInfo(int patlistID);

        /// <summary>
        /// 获取病人诊断信息
        /// </summary>
        /// <param name="patlistID">病人ID</param>
        /// <returns>诊断信息</returns>
        DataTable GetCasePatDiagInfo(int patlistID);

        /// <summary>
        /// 获取病案首页住院病人费用总信息
        /// </summary>
        /// <param name="patlistID">病人ID</param>
        /// <returns>病人费用总信息</returns>
        DataTable GetCasePatTotalFee(int patlistID);

        /// <summary>
        /// 获取病案首页住院病人费用信息
        /// </summary>
        /// <param name="patlistID">病人ID</param>
        /// <returns>病人费用信息</returns>
        DataTable GetCasePatFee(int patlistID);

        /// <summary>
        /// 获取抗菌药物费用
        /// </summary>
        /// <param name="patlistID">病人ID</param>
        /// <returns>抗菌药物费用</returns>
        decimal GetAntFee(int patlistID);

        /// <summary>
        /// 获取在院病人费用信息
        /// </summary>
        /// <param name="patlistID">病人ID</param>
        /// <returns>在院病人费用信息</returns>
        DataTable GetCasePatFeeInHospital(int patlistID);

        /// <summary>
        /// 获取在院病人总费用信息
        /// </summary>
        /// <param name="patlistID">病人ID</param>
        /// <returns>总费用信息</returns>
        DataTable GetCasePatTotalFeeInHospital(int patlistID);
    }
}
