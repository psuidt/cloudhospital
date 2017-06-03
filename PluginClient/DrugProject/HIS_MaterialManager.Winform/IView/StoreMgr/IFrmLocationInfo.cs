using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.MaterialManage;

namespace HIS_DrugManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 库位信息管理接口
    /// </summary>
    interface IFrmLocationInfo : IBaseView
    {
        /// <summary>
        /// 绑定库位信息
        /// </summary>
        /// <param name="location">库位信息</param>
        void GetLocationInfo(MW_Location location);

        /// <summary>
        /// 保存库位信息
        /// </summary>
        /// <param name="statu">库位信息</param>
        void SaveComplete(bool statu);
    }
}
