using System.Collections.Generic;
using System.Data;

namespace HIS_MaterialManage.Winform.IView.FinanceMgr
{
    /// <summary>
    /// 物资付款接口
    /// </summary>
    public interface IFrmMaterialPayMent
    {
        /// <summary>
        /// 获取当前选中行
        /// </summary>
        DataRow CurrentRow { get; set; }

        /// <summary>
        /// 绑定明细
        /// </summary>
        /// <param name="dtInstoreDetails">明细网格数据源</param>
        void BindInDetailGrid(DataTable dtInstoreDetails);

        /// <summary>
        /// 绑定药剂科室
        /// </summary>
        /// <param name="dtDrugDept">数据源</param>
        void BindDrugDept(DataTable dtDrugDept);

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>表头查询条件</returns>
        Dictionary<string, string> GetQueryCondition(int deptID);

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>表头查询条件</returns>
        Dictionary<string, string> GetQueryConditions(int deptID);

        /// <summary>
        /// 绑定供应商控件
        /// </summary>
        /// <param name="dtSupply">数据源</param>
        void BindSupply(DataTable dtSupply);

        /// <summary>
        /// 绑定头表
        /// </summary>
        /// <param name="dtInstoreHead">表头网格数据源</param>
        void BindInHeadGrid(DataTable dtInstoreHead);

        /// <summary>
        /// 获取选中表头ID
        /// </summary>
        /// <returns>当前选中表头ID</returns>
        Dictionary<string, string> GetCurrentHeadID();

        /// <summary>
        /// 绑定付款记录表
        /// </summary>
        /// <param name="dtpayRecordData">付款记录列表</param>
        void BindInPayRecordGrid(DataTable dtpayRecordData);

        /// <summary>
        /// 绑定头表
        /// </summary>
        /// <param name="dtInstoreHead">表头网格数据源</param>
        void BindInHeadGrids(DataTable dtInstoreHead);

        /// <summary>
        /// 绑定明细
        /// </summary>
        /// <param name="dtInstoreDetails">明细网格数据源</param>
        void BindInDetailGrids(DataTable dtInstoreDetails);

        /// <summary>
        /// 获取选中表头ID
        /// </summary>
        /// <returns>当前选中表头ID</returns>
        Dictionary<string, string> GetCurrentHeadIDs();
    }
}
