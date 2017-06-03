using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    /// 票据号调整界面接口类
    /// </summary>
   public interface IFrmAdjustInvoice:IBaseView
    {
        /// <summary>
        /// 当前票据号
        /// </summary>
        string curInvoiceNO { get; set; }
    }
}
