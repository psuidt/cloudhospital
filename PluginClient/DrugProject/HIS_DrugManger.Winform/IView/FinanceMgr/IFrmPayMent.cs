using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_DrugManage.Winform.IView.FinanceMgr
{
    /// <summary>
    /// 财务付款
    /// </summary>
    public interface IFrmPayMent : IBaseView
    {
        /// <summary>
        /// 当前行对象
        /// </summary>
        DataRow currentRow { get; set; }

        /// <summary>
        /// 绑定入库明细信息
        /// </summary>
        /// <param name="dtInstoreDetails">入库明细数据源</param>
        void BindInDetailGrid(DataTable dtInstoreDetails);

        /// <summary>
        /// 绑定药剂科室控件
        /// </summary>
        /// <param name="dtDrugDept">药剂科室数据源</param>
        void BindDrugDept(DataTable dtDrugDept);

        /// <summary>
        /// 获取未付款查询条件
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>未付款查询条件</returns>
        Dictionary<string, string> GetQueryCondition(int deptID);

        /// <summary>
        /// 获取已付款查询条件
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>未付款查询条件</returns>
        Dictionary<string, string> GetQueryConditions(int deptID);

        /// <summary>
        /// 绑定供应商控件
        /// </summary>
        /// <param name="dtSupply">供应商数据源</param>
        void BindSupply(DataTable dtSupply);

        /// <summary>
        /// 绑定头表
        /// </summary>
        /// <param name="dtInstoreHead">入库头表数据源</param>
        void BindInHeadGrid(DataTable dtInstoreHead);

        /// <summary>
        /// 获取选中表头ID
        /// </summary>
        /// <returns>当前选中表头ID</returns>
        Dictionary<string, string> GetCurrentHeadID();

        /// <summary>
        /// 绑定付款记录表
        /// </summary>
        /// <param name="dtpayRecordData">付款记录数据源</param>
        void BindInPayRecordGrid(DataTable dtpayRecordData);

        /// <summary>
        /// 绑定入库头表信息
        /// </summary>
        /// <param name="dtInstoreHead">头表数据源</param>
        void BindInHeadGrids(DataTable dtInstoreHead);

        /// <summary>
        /// 绑定入库明细表信息
        /// </summary>
        /// <param name="dtInstoreDetails">明细数据源</param>
        void BindInDetailGrids(DataTable dtInstoreDetails);

        /// <summary>
        /// 获取当前选中行信息
        /// </summary>
        /// <returns>当前选中行信息</returns>
        Dictionary<string, string> GetCurrentHeadIDs();
    }
}
