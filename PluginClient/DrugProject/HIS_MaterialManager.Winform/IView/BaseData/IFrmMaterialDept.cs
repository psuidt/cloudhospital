using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.MaterialManage;

namespace HIS_MaterialManage.Winform.IView.BaseData
{
    /// <summary>
    /// 物资库管管理接口
    /// </summary>
    interface IFrmMaterialDept: IBaseView
    {
        /// <summary>
        /// 绑定科室选项卡
        /// </summary>
        /// <param name="dt">科室数据集</param>
        void BindDep(DataTable dt);

        /// <summary>
        /// 绑定物资科室Grid
        /// </summary>
        /// <param name="dt">物资科室</param>
        void BindDrugDeptGrid(DataTable dt);

        /// <summary>
        /// 绑定物资科室单据Grid
        /// </summary>
        /// <param name="dt">物资科室单据</param>
        void BindDrugDeptBillGrid(DataTable dt);

        /// <summary>
        /// 当前药品库房信息表
        /// </summary>
        MW_DeptDic CurrtDeptDic { get; set; }
    }
}
