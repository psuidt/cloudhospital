using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_DrugManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 盘点单
    /// </summary>
    interface IFrmCheck : IBaseView
    {
        #region 绑定控件数据源
        /// <summary>
        /// 绑定药剂科室控件
        /// </summary>
        /// <param name="dtDrugDept">数据源</param>
        void BindDrugDept(DataTable dtDrugDept);

        /// <summary>
        /// 绑定头表
        /// </summary>
        /// <param name="dtHead">表头网格数据源</param>
        void BindInHeadGrid(DataTable dtHead);

        /// <summary>
        /// 绑定明细
        /// </summary>
        /// <param name="dtDetails">明细网格数据源</param>
        void BindInDetailGrid(DataTable dtDetails);
        #endregion

        #region 获取界面数据
        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>表头查询条件</returns>
        Dictionary<string, string> GetQueryCondition(int deptID);

        /// <summary>
        /// 获取选中表头ID
        /// </summary>
        /// <returns>当前选中表头ID</returns>
        Dictionary<string, string> GetCurrentHeadID();

        /// <summary>
        /// 获取单据表头网格的数据源
        /// </summary>
        /// <returns>单据表头网格的数据源</returns>
        DataTable GetDgHeadSource();
        #endregion
    }
}
