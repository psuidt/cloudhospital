using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.IView.BaseData
{
    /// <summary>
    /// 药品类型
    /// </summary>
    interface IFrmDrugType
    {
        #region 药品类型

        /// <summary>
        /// Gets or sets药品类型
        /// </summary>
        DG_TypeDic CurrentDataP
        {
            get; set; 
        }

        /// <summary>
        /// Gets or sets药品类型查询条件
        /// </summary>
        Dictionary<string, string> QueryConditionP
        {
            get; set; 
        }

        /// <summary>
        /// 药品类型加载
        /// </summary>
        /// <param name="dt">药品类型数据集</param>
        void LoadDrugType(DataTable dt);

        #endregion
        #region 药品子类型
        /// <summary>
        /// 药品子类型加载
        /// </summary>
        /// <param name="dt">药品子类型数据集</param>
        void LoadChildDrugType(DataTable dt);

        /// <summary>
        /// Gets or sets  药品子类型
        /// </summary>
        DG_ChildTypeDic CurrentDataC { get; set; }

        /// <summary>
        /// Gets or sets  药品子类型查询条件
        /// </summary>
        Dictionary<string, string> QueryConditionC { get; set; }
        #endregion
    }
}
