using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.IView.FinanceMgr
{
    /// <summary>
    /// 药品调价明细
    /// </summary>
    public interface IFrmAdjPriceDetail : IBaseView
    {
        /// <summary>
        /// 科室ID
        /// </summary>
        int DeptID { get; set; }

        /// <summary>
        /// 当前HeadId
        /// </summary>
        int CurrentHead { get; set; }

        /// <summary>
        /// 绑定药品ShowCard
        /// </summary>
        /// <param name="dtDrugInfo">药品数据源</param>
        void BindDrugInfoCard(DataTable dtDrugInfo);

        /// <summary>
        ///  绑定调价明细表数据
        /// </summary>
        /// <param name="inDetails">调价明细表数据源</param>
        void BindDetailsGrid(DataTable inDetails);

        /// <summary>
        ///  重置网格状态
        /// </summary>
        /// <param name="billStatus">网格状态对象</param>
        void InitControStatus(DGEnum.BillEditStatus billStatus);

        /// <summary>
        /// 绑定调价头表信息
        /// </summary>
        /// <param name="inHead">泛型对象</param>
        void BindInHeadInfo<THead>(THead inHead);

        /// <summary>
        /// 调价完成函数
        /// </summary>
        /// <param name="result">结果信息对象</param>
        void ExcuteComplete(DGBillResult result);
    }
}
