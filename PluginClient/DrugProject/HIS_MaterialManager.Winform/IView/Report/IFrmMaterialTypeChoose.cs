using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_MaterialManage.Winform.IView.Report
{
    /// <summary>
    /// 物资类型选择接口
    /// </summary>
    public interface IFrmMaterialTypeChoose: IBaseView
    {
        /// <summary>
        /// 物资类型加载
        /// </summary>
        /// <param name="dtTypeList">物资类型</param>
        void LoadMaterialType(DataTable dtTypeList);
    }
}
