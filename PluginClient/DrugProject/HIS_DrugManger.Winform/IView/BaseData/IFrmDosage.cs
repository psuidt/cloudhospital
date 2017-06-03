using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.IView.BaseData
{
    /// <summary>
    /// 剂型
    /// </summary>
    interface IFrmDosageManage : IBaseView
    {
        /// <summary>
        /// Gets or sets 操作对象
        /// </summary>
        DG_DosageDic CurrentData { get; set; }

        /// <summary>
        /// Gets or sets 查询条件
        /// </summary>
        Dictionary<string, string> QueryCondition { get; set; }

        /// <summary>
        /// 加载列表数据
        /// </summary>
        /// <param name="dt">数据集</param>
        void LoadData(DataTable dt);

        /// <summary>
        /// 绑定下拉列表
        /// </summary>
        /// <param name="dt">数据集</param>
        void BindComBox(DataTable dt);

        /// <summary>
        /// 绑定药品类型下拉框（查询条件）
        /// </summary>
        /// <param name="dtDrugType">药品类型数据集</param>
        void BindComboBoxQuery(DataTable dtDrugType);

        /// <summary>
        /// 获取总数量
        /// </summary>
        /// <param name="dt">数据集</param>
        void GetTotalNum(DataTable dt);
    }
}
