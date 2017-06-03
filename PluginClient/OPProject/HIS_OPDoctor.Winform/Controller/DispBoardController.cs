using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;

namespace HIS_OPDoctor.Winform.Controller
{
    /// <summary>
    /// 糖尿病专科看板控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmDispBoard")]//与系统菜单对应
    [WinformView(Name = "FrmDispBoard", DllName = "HIS_OPDoctor.Winform.dll", ViewTypeName = "HIS_OPDoctor.Winform.ViewForm.FrmDispBoard")]
    public class DispBoardController : WcfClientController
    {
        /// <summary>
        /// 初始化菜单
        /// </summary>
        public override void Init()
        {
        }
    }
}
