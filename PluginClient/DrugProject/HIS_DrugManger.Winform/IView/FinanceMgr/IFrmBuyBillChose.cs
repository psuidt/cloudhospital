using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_DrugManage.Winform.IView.FinanceMgr
{
    /// <summary>
    /// 采购计划
    /// </summary>
    interface IFrmBuyBillChose : IBaseView
    {
        /// <summary>
        /// 绑定采购计划头表格
        /// </summary>
        /// <param name="dt">采购计划头表数据集</param>
        void BindPlanHeadGrid(DataTable dt);

        /// <summary>
        /// 绑定采购计划明细表表格
        /// </summary>
        /// <param name="dt">采购明细数据集</param>
        void BindPlanDetailGrid(DataTable dt);

        /// <summary>
        /// 获取选中表头ID
        /// </summary>
        /// <returns>当前选中表头ID</returns>
        Dictionary<string, string> GetCurrentHeadID();
    }
}
