using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_ThatFee.Winform.IView
{
    /// <summary>
    /// 医技项目明细查询界面接口
    /// </summary>
    public interface IFrmThatFeeQuery : IBaseView
    {
        /// <summary>
        /// 0门诊 1住院
        /// </summary>
        int SystemType { get; set; }

        /// <summary>
        /// 获取执行科室ID
        /// </summary>
        string ConfirDeptID { get; set; }

        /// <summary>
        /// ///获取开始日期
        /// </summary>
        string BeginDate { get; set; }

        /// <summary>
        /// 获取结束日期
        /// </summary>
        string EndDate { get; set; }

        /// <summary>
        /// 获取选择的项目ID
        /// </summary>
        string ItemIDs { get; set; }

        /// <summary>
        /// 绑定执行科室
        /// </summary>
        /// <param name="dtDept">科室数据源</param>
        void BindDept(DataTable dtDept);

        /// <summary>
        /// 绑定项目搜索ShowCard
        /// </summary>
        /// <param name="dtItem">组合项目数据</param>
        void BindExecShowCard(DataTable dtItem);

        /// <summary>
        /// 获取查询条件语句
        /// </summary>
        void GetQueryWhere();

        /// <summary>
        /// 绑定确费网格信息
        /// </summary>
        /// <param name="dtFee">申请明细数据源</param>
        void BindThatFee(DataTable dtFee);
    }
}
