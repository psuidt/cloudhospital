using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 票据号调整接口
    /// </summary>
    public interface IAdjustInvoice : IBaseView
    {
        /// <summary>
        /// 当前票据号
        /// </summary>
        string InvoiceNo { get; set; }

        /// <summary>
        /// 新票据号
        /// </summary>
        string NewInvoiceNo { get; }

        /// <summary>
        /// 新票据号前缀
        /// </summary>
        string NewPerfChar { get; }
    }
}
