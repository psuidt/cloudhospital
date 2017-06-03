using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_DrugManage.Winform.IView.Report
{
    /// <summary>
    /// 新药入库
    /// </summary>
    public interface IFrmNewDrug : IBaseView
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
