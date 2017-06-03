using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_DrugManage.Winform.IView.Report
{
    /// <summary>
    /// 进销存统计
    /// </summary>
    interface IFrmInventoryRpt : IBaseView
    {
        /// <summary>
        /// 绑定库房下拉框
        /// </summary>
        /// <param name="dtDept">科室数据</param>
        void BindDeptRoom(DataTable dtDept);

        /// <summary>
        /// 绑定药品类型下拉控件
        /// </summary>
        /// <param name="dt">药品类型</param>
        void BindDrugTypeDicComboBox(DataTable dt);

        /// <summary>
        /// 绑定会计年
        /// </summary>
        /// <param name="dtYear">年</param>
        void BindYear(DataTable dtYear);

        /// <summary>
        /// 绑定会计月
        /// </summary>
        /// <param name="dtMonth">年</param>
        void BindMonth(DataTable dtMonth);
    }
}
