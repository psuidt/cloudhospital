using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_DrugManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 领药申请
    /// </summary>
    interface IFrmApply : IBaseView
    {
        /// <summary>
        /// 获取选中表头ID
        /// </summary>
        /// <returns>当前选中表头ID</returns>
        Dictionary<string, string> GetCurrentHeadID();

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <returns>获取查询条件</returns>
        Dictionary<string, string> GetQueryCondition();

        /// <summary>
        /// 绑定操作科室
        /// </summary>
        /// <param name="dt">科室数据源</param>
        void BindDrugDept(DataTable dt);

        /// <summary>
        /// 绑定主表
        /// </summary>
        /// <param name="dt">主表数据源</param>
        void BindApplyHead(DataTable dt);

        /// <summary>
        /// 绑定从表
        /// </summary>
        /// <param name="dt">从表数据源</param>
        void BindApplyDetail(DataTable dt);
    }
}
