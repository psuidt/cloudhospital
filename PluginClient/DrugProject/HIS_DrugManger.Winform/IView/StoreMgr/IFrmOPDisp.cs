using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_DrugManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 门诊发药
    /// </summary>
    interface IFrmOPDisp : IBaseView
    {
        /// <summary>
        /// 绑定病人信息和发票表格
        /// </summary>
        /// <param name="dt">发票表</param>
        void BindPatientAndInvoceGrid(DataTable dt);

        /// <summary>
        /// 绑定处方表头表格
        /// </summary>
        /// <param name="dt">处方表头</param>
        void BindFeeHeadGrid(DataTable dt);

        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <param name="dtPatient">病人信息</param>
        /// <param name="dtDiseases">病人诊断信息</param>
        void BindPatientInfo(DataTable dtPatient, string dtDiseases);

        /// <summary>
        /// 绑定处方明细表格
        /// </summary>
        /// <param name="dt">处方明细</param>
        void BindFeeDetailGrid(DataTable dt);

        /// <summary>
        /// 绑定药房下拉列表
        /// </summary>
        /// <param name="dt">药房数据</param>
        void BindStoreRoomComboxList(DataTable dt);

        /// <summary>
        /// Gets or sets发药人
        /// </summary>
        string SendEmployeeName { get; set; }

        /// <summary>
        /// Gets or sets当前科室Id
        /// </summary>
        int DeptId { get; set; }
    }
}
