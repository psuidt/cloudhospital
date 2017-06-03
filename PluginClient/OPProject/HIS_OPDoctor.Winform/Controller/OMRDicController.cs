using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;

namespace HIS_OPDoctor.Winform.Controller
{
    /// <summary>
    /// 病历库字典控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmOMRDic")]//与系统菜单对应
    [WinformView(Name = "FrmOMRDic", DllName = "HIS_OPDoctor.Winform.dll", ViewTypeName = "HIS_OPDoctor.Winform.ViewForm.FrmOMRDic")]
    public class OMRDicController : WcfClientController
    {
        /// <summary>
        /// 初始化菜单
        /// </summary>
        public override void Init()
        {
        }
    }
}
