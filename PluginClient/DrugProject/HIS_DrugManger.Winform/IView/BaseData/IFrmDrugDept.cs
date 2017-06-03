using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.IView.BaseData
{
    /// <summary>
    /// 药品科室
    /// </summary>
    interface IFrmDrugDept: IBaseView
    {
        /// <summary>
        /// 绑定科室
        /// </summary>
        /// <param name="dt">数据集</param>
        void BindDep(DataTable dt);

        /// <summary>
        /// 绑定药剂科室表格
        /// </summary>
        /// <param name="dt">药剂科室数据集</param>
        void BindDrugDeptGrid(DataTable dt);

        /// <summary>
        /// 绑定单据表格
        /// </summary>
        /// <param name="dt">单据数据集</param>
        void BindDrugDeptBillGrid(DataTable dt);

        /// <summary>
        /// Gets or sets当前药品库房信息表
        /// </summary>
        DG_DeptDic CurrtDeptDic { get; set; }
    }
}
