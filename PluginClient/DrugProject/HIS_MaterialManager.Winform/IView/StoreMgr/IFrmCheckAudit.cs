using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_MaterialManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 盘点审核
    /// </summary>
    interface IFrmCheckAudit: IBaseView
    {
        /// <summary>
        /// 绑定物资科室控件
        /// </summary>
        /// <param name="dtDrugDept">数据源</param>
        void BindMaterialDept(DataTable dtDrugDept);

        /// <summary>
        /// 绑定提示信息
        /// </summary>
        /// <param name="tip">库房状态，待审单据数量</param>
        void BindShowTip(string tip);

        /// <summary>
        /// 绑定药品定位查询ShowCard
        /// </summary>
        /// <param name="dtDrugInfo">药品信息</param>
        void BindDrugPositFindCard(DataTable dtDrugInfo);

        /// <summary>
        /// 绑定盘点汇总明细
        /// </summary>
        /// <param name="dtDetails">盘点汇总明细网格数据源</param>
        void BindCheckDetailGrid(DataTable dtDetails);

        /// <summary>
        /// 绑定盘点审核头表
        /// </summary>
        /// <param name="dtHeads">盘点审核头表网格数据源</param>
        void BindAuditHeadGrid(DataTable dtHeads);

        /// <summary>
        /// 绑定盘点审核明细
        /// </summary>
        /// <param name="dtDetails">盘点审核明细网格数据源</param>
        void BindAuditDetailGrid(DataTable dtDetails);

        /// <summary>
        /// 取得盘点审核头查询条件
        /// </summary>
        /// <returns>表头查询条件</returns>
        Dictionary<string, string> GetAuditHeadQueryCondition();
        
        /// <summary>
        /// 取得盘点审核明细查询条件
        /// </summary>
        /// <returns>审核明细查询条件</returns>
        Dictionary<string, string> GetAuditDetailQueryCondition();

        /// <summary>
        /// 取得未审核汇总明细查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        Dictionary<string, string> GetAllNotAuditDetailQueryCondition();

        /// <summary>
        /// 显示汇总信息
        /// </summary>
        /// <param name="message">显示信息</param>
        void ShowAuditCompute(string message);
    }
}
