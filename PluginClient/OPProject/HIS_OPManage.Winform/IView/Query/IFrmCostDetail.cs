using System.Data;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    /// 结算明细界面接口
    /// </summary>
    interface IFrmCostDetail
    {
        /// <summary>
        /// 结算明细显示
        /// </summary>
        /// <param name="dtData">结算明细数据</param>
        void BindData(DataTable dtData);

        /// <summary>
        /// 设置网格颜色
        /// </summary>
        void  SetGridColor();
    }
}
