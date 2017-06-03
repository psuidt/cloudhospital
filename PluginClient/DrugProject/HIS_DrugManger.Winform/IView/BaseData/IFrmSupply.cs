using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManage.Winform.IView.BaseData
{
    /// <summary>
    /// 供应商
    /// </summary>
    interface IFrmSupply : IBaseView
    {
        /// <summary>
        /// 当前供应商对象
        /// </summary>
        DG_SupportDic CurrentData { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        List<Tuple<string, string, SqlOperator>> AndWhere { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        List<Tuple<string, string, SqlOperator>> OrWhere { get; set; }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="dt">表格</param>
        /// <param name="total">总数</param>
        void LoadSupply(DataTable dt, int total);
    }
}
