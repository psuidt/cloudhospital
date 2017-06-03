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
    /// 医技工作量统计接口
    /// </summary>
    public interface IFrmThatFeeCount : IBaseView
    {
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
        /// 绑定执行科室
        /// </summary>
        /// <param name="dtDept">科室数据源</param>
        void BindDept(DataTable dtDept);

        /// <summary>
        /// 获取医技确费统计信息
        /// </summary>
        /// <param name="dt">医技确费统计数据</param>
        void BindThatFeeCount(DataTable dt);

        /// <summary>
        /// 获取查询条件语句
        /// </summary>
        void GetQueryWhere();
    }
}
