using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_DrugManage.Winform.IView.Report
{
    /// <summary>
    ///采购入库对比表
    /// </summary>
    public interface IFrmBuyComparison : IBaseView
    {
        /// <summary>
        /// 绑定库房
        /// </summary>
        /// <param name="dtDept">科室数据</param>
        /// <param name="loginDeptID">当前登陆用户科室Id</param>
        void BindDeptRoom(DataTable dtDept, int loginDeptID);

        /// <summary>
        /// 绑定网格数据
        /// </summary>
        /// <param name="dt">统计数据</param>
        void BindGridData(DataTable dt);
    }
}
