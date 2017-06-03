using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.Controller.Account
{
    /// <summary>
    /// 交款管理控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmChargeDetail")]
    public class ChargeDetailController: WcfClientController
    {
        /// <summary>
        /// 缴款明细数据显示接口
        /// </summary>
        IChargeDetail iChargeDetail;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            iChargeDetail = (IChargeDetail)DefaultView;
        }
    }
}
