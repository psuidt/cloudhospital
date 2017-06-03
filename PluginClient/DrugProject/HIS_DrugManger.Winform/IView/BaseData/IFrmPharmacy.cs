using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.IView.BaseData
{
    /// <summary>
    /// 药理分类
    /// </summary>
    interface IFrmPharmacy: IBaseView
    {
        /// <summary>
        /// 获取药理分类数据
        /// </summary>
        /// <param name="dt">数据源</param>
        void LoadGrid(DataTable dt);

        /// <summary>
        /// 获取当前药品药理分类对象
        /// </summary>
        DG_Pharmacology CurrentData { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        Dictionary<string, string> QueryCondition { get; set; }

        /// <summary>
        /// 绑定树节点数据
        /// </summary>
        /// <param name="dt">数据源</param>
        void BindTree(DataTable dt);

        /// <summary>
        /// 获取树节点Key
        /// </summary>
        /// <param name="k">Key值</param>
        void GetDG_PharmacologyKey(int k);
    }
}
