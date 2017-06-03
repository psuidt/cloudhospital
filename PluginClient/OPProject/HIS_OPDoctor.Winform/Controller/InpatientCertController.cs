using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;

namespace HIS_OPDoctor.Winform.Controller
{
    /// <summary>
    /// 住院证控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmInpatientCert")]//与系统菜单对应
    [WinformView(Name = "FrmInpatientCert", DllName = "HIS_OPDoctor.Winform.dll", ViewTypeName = "HIS_OPDoctor.Winform.ViewForm.FrmInpatientCert")]
    public class InpatientCertController : WcfClientController
    {
        /// <summary>
        /// 初始化菜单
        /// </summary>
        public override void Init()
        {
        }
    }
}
