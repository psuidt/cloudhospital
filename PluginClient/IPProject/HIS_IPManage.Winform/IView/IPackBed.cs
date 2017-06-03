using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 床位一览-包床接口
    /// </summary>
    public interface IPackBed : IBaseView
    {
        /// <summary>
        /// 绑定已分配床位病人列表
        /// </summary>
        /// <param name="patListDt">已分配床位病人列表</param>
        void Bind_InHospitalPatList(DataTable patListDt);

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        void CloseForm();
    }
}
