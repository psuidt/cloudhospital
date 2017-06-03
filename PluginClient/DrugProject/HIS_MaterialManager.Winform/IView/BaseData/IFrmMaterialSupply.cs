using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.MaterialManage;
using HIS_Entity.SqlAly;

namespace HIS_MaterialManage.Winform.IView.BaseData
{
    /// <summary>
    /// 物资供应商维护
    /// </summary>
    public interface IFrmMaterialSupply: IBaseView
    {
        /// <summary>
        /// 读取供应商数据
        /// </summary>
        /// <param name="dt">供应商列表</param>
        /// <param name="total">默认选择ID</param>
        void LoadSupply(DataTable dt, int total);

        /// <summary>
        /// 选中的供应商
        /// </summary>
        MW_SupportDic CurrentData { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        List<Tuple<string, string, SqlOperator>> AndWhere { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        List<Tuple<string, string, SqlOperator>> OrWhere { get; set; }
    }
}
