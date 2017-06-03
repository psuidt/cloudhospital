using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.MaterialManage;

namespace HIS_MaterialManage.Winform.IView.BaseData
{
    /// <summary>
    /// 生产厂家维护接口
    /// </summary>
    public interface IFrmMaterialProduct : IBaseView
    {
        /// <summary>
        /// 绑定厂家列表
        /// </summary>
        /// <param name="dt">厂家列表</param>
        void LoadProduct(DataTable dt);

        /// <summary>
        /// 选中的厂家信息
        /// </summary>
        MW_ProductDic CurrentData { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        Dictionary<string, string> QueryCondition { get; set; }
    }
}
