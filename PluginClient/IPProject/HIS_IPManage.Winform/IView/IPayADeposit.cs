using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.IPManage;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 收取预交金接口
    /// </summary>
    public interface IPayADeposit: IBaseView
    {
        /// <summary>
        /// 设置预交金票据号
        /// </summary>
        /// <param name="billNumber">预交金票据号</param>
        void SetBillNumber(string billNumber);

        /// <summary>
        /// 绑定预交金支付方式
        /// </summary>
        /// <param name="patMethodDt">支付方式列表</param>
        void Binding_PaymentMethod(DataTable patMethodDt);

        /// <summary>
        /// 预交金
        /// </summary>
        IP_DepositList DepositList { get; set; }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        void ClosePayADposit();
    }
}
