using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;

namespace HIS_MaterialManage.Winform.Controller
{
    /// <summary>
    /// 财务管理控制器（月结、付款）
    /// </summary>
    [WinformController(DefaultViewName = "FrmPurchase")]//在菜单上显示
    [WinformView(Name = "FrmPurchase", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmPurchase")]//
    public class FinanceController: WcfClientController
    {
    }
}
