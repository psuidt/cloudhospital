using System.Data;

namespace HIS_IPManage.Dao
{
    /// <summary>
    /// 住院诊断接口
    /// </summary>
    public interface IDiagnosisDao
    {
        /// <summary>
        /// 获取诊断信息
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>诊断信息</returns>
        DataTable LoadDiagnosisInfo(int patListID);

        /// <summary>
        /// 校验诊断名称是否重复
        /// </summary>
        /// <param name="patListID">病人入院登记ID</param>
        /// <param name="flag">是否为新增主诊断</param>
        /// <param name="diagID">诊断ID</param>
        /// <param name="main">是否为主诊断</param>
        /// <param name="diagName">诊断名</param>
        /// <param name="id">id</param>
        /// <returns>false：重复</returns>
        bool CheckDiagnosisInfo(int patListID, int flag, int diagID, int main, string diagName, int id);

        /// <summary>
        /// 获取诊断类型信息
        /// </summary>
        /// <returns>诊断类型信息</returns>
        DataTable GetDiagnosisClass();

        /// <summary>
        /// 获取诊断信息
        /// </summary>
        /// <returns>诊断信息</returns>
        DataTable GetBasicDiagnosis();

        /// <summary>
        /// 删除诊断
        /// </summary>
        /// <param name="id">诊断id</param>
        /// <returns>删除行数</returns>
        int DeleteDiag(int id);
    }
}
