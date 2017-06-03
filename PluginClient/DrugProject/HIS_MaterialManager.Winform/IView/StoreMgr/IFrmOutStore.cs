using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_MaterialManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 出库单管理接口
    /// </summary>
    interface IFrmOutStore : IBaseView
    {
        /// <summary>
        /// 获取选中表头ID
        /// </summary>
        /// <returns>当前选中表头ID</returns>
        Dictionary<string, string> GetCurrentHeadID();

        /// <summary>
        /// 绑定物资科室
        /// </summary>
        /// <param name="dtDrugDept">物资科室</param>
        void BindDrugDept(DataTable dtDrugDept);

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <returns>查询条件</returns>
        Dictionary<string, string> GetQueryCondition(int deptId);

        /// <summary>
        /// 绑定业务类型
        /// </summary>
        /// <param name="dtOpType">数据源</param>
        void BindOpType(DataTable dtOpType);

        /// <summary>
        /// 绑定往来科室
        /// </summary>
        /// <param name="dt">往来科室</param>
        void BindDept(DataTable dt);

        /// <summary>
        /// 绑定出库主表信息
        /// </summary>
        /// <param name="dt">出库主表信息</param>
        void BindHeadGrid(DataTable dt);

        /// <summary>
        /// 绑定从表数据
        /// </summary>
        /// <param name="dt">从表数据</param>
        void BindDeatailGrids(DataTable dt);
    }
}
