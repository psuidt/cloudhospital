using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPNurse.Winform.IView
{
    /// <summary>
    /// 瓶签打印确认接口
    /// </summary>
    public interface IPrintExamConfirm : IBaseView
    {
        /// <summary>
        /// 绑定报表打印数据
        /// </summary>
        /// <param name="printDt">待打印数据</param>
        void Bind_PrintDt(DataTable printDt);

        /// <summary>
        /// 关闭当前界面
        /// </summary>
        void CloseForm();
    }
}
