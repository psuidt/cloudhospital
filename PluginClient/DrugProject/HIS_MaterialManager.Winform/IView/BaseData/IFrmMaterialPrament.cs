using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_MaterialManage.Winform.IView.BaseData
{
    /// <summary>
    /// 物资参数设置借口
    /// </summary>
    interface IFrmMaterialPrament : IBaseView
    {
        /// <summary>
        ///  绑定公共物资参数
        /// </summary>
        /// <param name="dt">公用物资参数集</param>
        void BindPublicParameters(DataTable dt);

        /// <summary>
        /// 绑定科室参数
        /// </summary>
        /// <param name="dt">科室参数集</param>
        void BindDeptParameters(DataTable dt);

        /// <summary>
        /// 绑定药剂科室列表
        /// </summary>
        /// <param name="dt">药剂科室集</param>
        void BindDrugDeptList(DataTable dt);
    }
}
