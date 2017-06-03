using System.Data;

namespace HIS_IPManage.Winform.IView.IPCostSearch
{
    /// <summary>
    /// 住院病人结算费用明细接口
    /// </summary>
    public interface IFrmCostDetail
    {
        /// <summary>
        /// 绑定结算数据
        /// </summary>
        /// <param name="dtData">结算数据</param>
        void BindData(DataTable dtData);
    }
}
