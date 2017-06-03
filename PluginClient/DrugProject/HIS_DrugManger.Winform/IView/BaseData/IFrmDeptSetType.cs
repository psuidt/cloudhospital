using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_DrugManage.Winform.IView.BaseData
{
    /// <summary>
    ///科室类型
    /// </summary>
    public interface IFrmDeptSetType: IBaseView
    {
        /// <summary>
        /// 绑定科室类型数据
        /// </summary>
        /// <param name="dt">数据源</param>
       void BindGrid(DataTable dt);
    }
}
