using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_OPDoctor.Winform.IView
{
    /// <summary>
    /// 诊断界面接口
    /// </summary>
    public interface IFrmDiagnosis : IBaseView
    {
        /// <summary>
        /// 绑定诊断showcard
        /// </summary>
        /// <param name="dt">诊断数据</param>
        void BindDiseaseShowCard(DataTable dt);

        /// <summary>
        /// 绑定诊断网格
        /// </summary>
        /// <param name="dtDisease">诊断表</param>
        void BindDiseaseDataGrid(DataTable dtDisease);
    }
}
