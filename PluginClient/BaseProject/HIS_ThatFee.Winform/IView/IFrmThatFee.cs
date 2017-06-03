using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.WinformFrame.Controller;
using static HIS_ThatFee.Winform.ViewForm.FrmThatFee;

namespace HIS_ThatFee.Winform.IView
{
    /// <summary>
    /// 医技确费接口
    /// </summary>
    public interface IFrmThatFee : IBaseView
    {
        /// <summary>
        /// 0门诊 1住院
        /// </summary>
        int SystemType { get; set; }

        /// <summary>
        /// 执行科室
        /// </summary>
        string DeptId { get; set; }

        /// <summary>
        /// 开方科室
        /// </summary>
        string ClincDeptId { get; set; }

        /// <summary>
        /// 是否选中检查检验
        /// </summary>
        bool IsCheck { get; set; }

        /// <summary>
        /// 是否选中化验检验
        /// </summary>
        bool IsTest { get; set; }

        /// <summary>
        /// 是否选中治疗检验
        /// </summary>
        bool IsTreat { get; set; }

        /// <summary>
        /// 是否选中未确费
        /// </summary>
        bool IsNotThatFee { get; set; }

        /// <summary>
        /// 是否选中确费
        /// </summary>
        bool IsThatFee { get; set; }

        /// <summary>
        /// 申请单号
        /// </summary>
        string StrNO { get; set; }

        /// <summary>
        /// 绑定确费网格信息
        /// </summary>
        /// <param name="dtFee">申请明细数据源</param>
        void BindThatFee(DataTable dtFee);

        /// <summary>
        /// 绑定执行科室
        /// </summary>
        /// <param name="dtDept">科室数据源</param>
        void BindDept(DataTable dtDept);

        /// <summary>
        /// 绑定开方科室
        /// </summary>
        /// <param name="dtDept">科室数据源</param>
        void BindClincDept(DataTable dtDept);

        /// <summary>
        /// 获取查询条件语句
        /// </summary>
        void GetQueryWhere();

        /// <summary>
        /// 绑定费用网格信息
        /// </summary>
        /// <param name="dtFee">费用明细数据源</param>
        void BindFee(DataTable dtFee);

        /// <summary>
        /// 操作完成后提示
        /// </summary>
        /// <param name="message">消息内容</param>
        void CompleteMessage(string message);
    }
}
