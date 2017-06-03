using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_DrugManage.Winform.IView.BaseData
{
    /// <summary>
    /// 药品参数设置借口
    /// </summary>
    interface IFrmDrugPrament: IBaseView
    {
        /// <summary>
        ///  绑定公共药品参数
        /// </summary>
        /// <param name="dt">公用药品参数集</param>
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
