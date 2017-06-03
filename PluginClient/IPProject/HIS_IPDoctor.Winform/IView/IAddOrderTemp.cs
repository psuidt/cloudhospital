using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.ClinicManage;

namespace HIS_IPDoctor.Winform.IView
{
    /// <summary>
    /// 新增模板界面接口
    /// </summary>
    public interface IAddOrderTemp: IBaseView
    {
        /// <summary>
        /// 模板主表对象
        /// </summary>
        IPD_OrderModelHead OrderModelHead { get; set; }

        /// <summary>
        /// 关闭当前界面
        /// </summary>
        void ColseForm();
    }
}
