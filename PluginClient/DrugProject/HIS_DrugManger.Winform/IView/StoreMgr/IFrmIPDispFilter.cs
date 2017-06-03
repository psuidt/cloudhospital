using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_DrugManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 过滤
    /// </summary>
    interface IFrmIPDispFilter: IBaseView
    {
        /// <summary>
        /// 绑定药品ShowCard
        /// </summary>
        /// <param name="dtDrug">药品数据</param>
        void BindDrugShowCard(DataTable dtDrug);       
    }
}
