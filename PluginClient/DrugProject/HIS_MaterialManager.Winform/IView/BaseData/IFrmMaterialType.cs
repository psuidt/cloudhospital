using System.Collections.Generic;
using System.Data;
using HIS_Entity.MaterialManage;

namespace HIS_MaterialManage.Winform.IView.BaseData
{
    /// <summary>
    /// 物资分类维护接口
    /// </summary>
    public interface IFrmMaterialType
    {
        #region 药品类型

        /// <summary>
        /// 物资类型
        /// </summary>
        MW_TypeDic CurrentDataP { get; set; }

        /// <summary>
        /// 物资类型查询条件
        /// </summary>
        Dictionary<string, string> QueryConditionP { get; set; }

        /// <summary>
        /// 物资类型加载
        /// </summary>
        /// <param name="typelist">物资类型</param>
        void LoadMaterialType(DataTable typelist);

        #endregion
        #region 药品子类型
        /// <summary>
        /// 物资子类型加载
        /// </summary>
        /// <param name="dt">物资子类型</param>
        void LoadChildMaterialType(DataTable dt);

        /// <summary>
        /// 物资子类型
        /// </summary>
        MW_TypeDic CurrentDataC { get; set; }

        /// <summary>
        /// 物资子类型查询条件
        /// </summary>
        Dictionary<string, string> QueryConditionC { get; set; }
        #endregion
    }
}
