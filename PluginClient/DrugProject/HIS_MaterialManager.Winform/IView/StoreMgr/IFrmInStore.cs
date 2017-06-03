using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.SqlAly;

namespace HIS_MaterialManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 物资入库接口
    /// </summary>
    interface IFrmInStore : IBaseView
    {
        #region 绑定控件数据源
        /// <summary>
        /// 绑定供应商控件
        /// </summary>
        /// <param name="dtSupply">数据源</param>
        void BindSupply(DataTable dtSupply);

        /// <summary>
        /// 绑定药剂科室控件
        /// </summary>
        /// <param name="dtDrugDept">数据源</param>
        void BindDrugDept(DataTable dtDrugDept);

        /// <summary>
        /// 绑定业务类型
        /// </summary>
        /// <param name="dtOpType">数据源</param>
        void BindOpType(DataTable dtOpType);

        /// <summary>
        /// 绑定头表
        /// </summary>
        /// <param name="dtInstoreHead">表头网格数据源</param>
        void BindInHeadGrid(DataTable dtInstoreHead);

        /// <summary>
        /// 绑定明细
        /// </summary>
        /// <param name="dtInstoreDetails">明细网格数据源</param>
        void BindInDetailGrid(DataTable dtInstoreDetails);
        #endregion

        #region 获取界面数据
        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>查询条件</returns>
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

        #region 报表属性
        /// <summary>
        /// 查询过滤条件
        /// </summary>
        List<Tuple<string, string, SqlOperator>> AndWhere { get; set; }

        #endregion
    }
}
