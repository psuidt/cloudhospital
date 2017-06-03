using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.IView.BaseData
{
    /// <summary>
    /// 生产厂家
    /// </summary>
    interface IFrmProduct : IBaseView
    {
        /// <summary>
        /// 读取生产厂家
        /// </summary>
        /// <param name="dt">生产厂家数据源</param>
        void LoadProduct(DataTable dt);

        /// <summary>
        /// 当前生产厂家对象
        /// </summary>
        DG_ProductDic CurrentData { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        Dictionary<string, string> QueryCondition { get; set; }

        /// <summary>
        /// 绑定textboxcard
        /// </summary>
        /// <param name="dt">数据源</param>
        void BindDisease_textboxcard(DataTable dt);
    }
}
