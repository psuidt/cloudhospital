using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_DrugManage.Winform.IView.BaseData
{
    /// <summary>
    /// 药品拆零
    /// </summary>
    interface IFrmResolve : IBaseView
    {
        /// <summary>
        /// 药品子类型
        /// </summary>
        /// <param name="dt">药品子类型数据源</param>
        void LoadDrugCType(DataTable dt);

        /// <summary>
        /// 药品类型
        /// </summary>
        /// <param name="dt">药品类型数据源</param>
        void LoadDrugTypeForTb(DataTable dt);

        /// <summary>
        /// 药品剂型
        /// </summary>
        /// <param name="dt">剂型数据源</param>
        void LoadDosage(DataTable dt);

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <returns>返回查询条件</returns>
        Dictionary<string, string> GetQueryCondition();

        /// <summary>
        /// 绑定药品网格数据
        /// </summary>
        /// <param name="dt">药品数据源</param>
        void BindStoreGrid(DataTable dt);

        /// <summary>
        /// 绑定药品科室
        /// </summary>
        /// <param name="dtDrugDept">药品科室数据源</param>
        void BindDrugDept(DataTable dtDrugDept);
    }
}
