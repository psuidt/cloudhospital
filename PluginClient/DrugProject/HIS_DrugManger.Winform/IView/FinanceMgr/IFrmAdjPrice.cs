using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_DrugManage.Winform.IView.FinanceMgr
{
    /// <summary>
    /// 药品调价
    /// </summary>
    public interface IFrmAdjPrice: IBaseView
    {
        /// <summary>
        /// 科室ID
        /// </summary>
        string DeprtID { get; set; }

        /// <summary>
        /// 绑定科室
        /// </summary>
        /// <param name="dtDrugDept">科室数据集</param>
        void BindDrugDept(DataTable dtDrugDept);

        /// <summary>
        /// 绑定药品调价表头           
        /// </summary>
        /// <param name="dtInadjHead">调价表头数据集</param>
        void BindInHeadGrid(DataTable dtInadjHead);

        /// <summary>
        /// 绑定药品调价明细
        /// </summary>
        /// <param name="dtInadjDetail">调价明细数据集</param>
        void BindInDetailGrid(DataTable dtInadjDetail);

        /// <summary>
        /// 获取表头ID
        /// </summary>
        /// <returns>表头ID</returns>
        Dictionary<string, string> GetCurrentHeadID();

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>获取查询条件</returns>
        Dictionary<string, string> GetQueryCondition(int deptID);
    }
}
