using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_DrugManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 门诊退药
    /// </summary>
    interface IFrmOPRefund : IBaseView
    {
        /// <summary>
        /// Gets or sets发药人
        /// </summary>
        string ReturnEmployeeName { get; set; }

        /// <summary>
        /// 设置药房名称
        /// </summary>
        /// <param name="deptName">药房名称</param>
        void SetDrugStoreName(string deptName);

        /// <summary>
        /// 取得退药查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        Dictionary<string, string> GetRefundCondition();

        /// <summary>
        /// 取得退药查询-查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        Dictionary<string, string> GetRefundQueryCondition();

        /// <summary>
        /// 绑定退药网格
        /// </summary>
        /// <param name="dt">待退药记录</param>
        void BindRefundGrid(DataTable dt);

        /// <summary>
        /// 绑定退药查询网格
        /// </summary>
        /// <param name="dt">退药记录</param>
        void BindRefundQueryGrid(DataTable dt);
    }
}
