using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.IPManage;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 新增账单模板接口
    /// </summary>
    public interface IAddDoctorTemp: IBaseView
    {
        /// <summary>
        /// 账单模板头
        /// </summary>
        IP_FeeItemTemplateHead FeeItemTemplateHead { get; set; }

        /// <summary>
        /// 关闭当前界面
        /// </summary>
        void ColseForm();
    }
}
