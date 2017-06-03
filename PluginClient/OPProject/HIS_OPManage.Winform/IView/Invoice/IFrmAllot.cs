using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    /// 票据分配界面接口
    /// </summary>
   public  interface IFrmAllot
    {
        /// <summary>
        ///  获取收费员列表和设置当前操作员ID
        /// </summary>
        /// <param name="dtemp">收费员列表</param>
        /// <param name="opempid">当前操作员ID</param>
        void LoadAllotView(DataTable dtemp,int opempid);

        /// <summary>
        /// 当前操作的票据对象
        /// </summary>
         HIS_Entity.BasicData.Basic_Invoice CurrInvoice { get; }

        /// <summary>
        /// 是否分配成功
        /// </summary>
          bool isAddOK { get;}
    }
}