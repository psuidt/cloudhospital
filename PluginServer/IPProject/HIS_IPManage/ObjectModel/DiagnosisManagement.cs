using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.IPDoctor;

namespace HIS_IPManage.ObjectModel
{
    /// <summary>
    /// 诊断管理
    /// </summary>
    public class DiagnosisManagement: AbstractObjectModel
    {
        /// <summary>
        /// 保存诊断信息
        /// </summary>
        /// <param name="diagInfo">诊断信息</param>
        /// <returns>true：保存成功</returns>
        public int SaveDiagnosisInfo(IPD_Diagnosis diagInfo)
        {
            this.BindDb(diagInfo);
           return diagInfo.save();
        }
    }
}
