using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_MaterialManage.Winform.ViewForm
{
    /// <summary>
    /// 药品类型选择接口
    /// </summary>
    public interface IFrmDeptSetType: IBaseView
    {
        /// <summary>
        /// 绑定类型列表
        /// </summary>
        /// <param name="dt">类型列表</param>
        void BindGrid(DataTable dt);
    }
}
