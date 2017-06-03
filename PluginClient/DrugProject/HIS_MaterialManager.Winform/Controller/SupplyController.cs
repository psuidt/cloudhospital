using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;

namespace HIS_MaterialManage.Winform.Controller
{
    /// <summary>
    /// 物资供应商维护控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmPurchase")]//在菜单上显示
    [WinformView(Name = "FrmPurchase", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmPurchase")]//
    public class SupplyController: WcfClientController
    {
    }
}
