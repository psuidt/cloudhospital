using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    /// 票据详细信息接口
    /// </summary>
    interface IFrmInvoiceDetail : IBaseView
    {
        /// <summary>
        /// 绑定票据详细信息
        /// </summary>
        /// <param name="dtInvoice">票据信息</param>
        void BindInvoiceDt(DataTable dtInvoice);
    }
}
