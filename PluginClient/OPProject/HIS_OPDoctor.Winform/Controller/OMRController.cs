using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;

namespace HIS_OPDoctor.Winform.Controller
{
    /// <summary>
    /// 门诊病历书写模板控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmOMR")]//与系统菜单对应
    [WinformView(Name = "FrmOMR", DllName = "HIS_OPDoctor.Winform.dll", ViewTypeName = "HIS_OPDoctor.Winform.ViewForm.FrmOMR")]
    public class OMRController : WcfClientController
    {
        /// <summary>
        /// 初始化菜单
        /// </summary>
        public override void Init()
        {
        }
    }
}
