using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 定义出区错误处理接口
    /// </summary>
    public interface IDischargeConfirmation: IBaseView
    {
        /// <summary>
        /// 绑定未停用账单列表
        /// </summary>
        /// <param name="notStopOrderDt">未停用账单列表</param>
        /// <param name="isTransDept">true:转科/false:出区</param>
        void Bind_NotStopOrder(DataTable notStopOrderDt, bool isTransDept);
    }
}
