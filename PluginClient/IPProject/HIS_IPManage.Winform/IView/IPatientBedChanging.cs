using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 病人换床接口
    /// </summary>
    public interface IPatientBedChanging : IBaseView
    {
        /// <summary>
        /// 绑定空床床号列表
        /// </summary>
        /// <param name="bedNoDt">空床列表</param>
        /// <param name="bedNo">床位号</param>
        /// <param name="patName">病人姓名</param>
        /// <param name="serialNumber">住院号</param>
        void Bind_BedNoList(DataTable bedNoDt, string bedNo, string patName, string serialNumber);

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        void ClosrForm();
    }
}
