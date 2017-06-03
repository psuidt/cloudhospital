using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 发票补打接口
    /// </summary>
    public interface IInvoiceFill: IBaseView
    {
        /// <summary>
        /// 补打发票号
        /// </summary>
        string InvoiceNO { get; }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        void CloseForm();
    }
}
