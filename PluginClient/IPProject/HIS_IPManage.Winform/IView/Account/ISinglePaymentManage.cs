using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.IPManage;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 缴款接口
    /// </summary>
    public interface ISinglePaymentManage : IBaseView
    {
        /// <summary>
        /// 刷新界面
        /// </summary>
        void AccountRefresh();

        /// <summary>
        /// 绑定收费员
        /// </summary>
        /// <param name="dtStaff">收费员列表</param>
        void BindStaffInfo(DataTable dtStaff);

        /// <summary>
        /// 绑定树控件数据
        /// </summary>
        /// <param name="dtUnUpload">未缴款数据列表</param>
        /// <param name="dtUploaded">已缴款数据列表</param>
        void BindPayInfoList(DataTable dtUnUpload, DataTable dtUploaded);

        /// <summary>
        /// 绑定三个数据框的数据
        /// </summary>
        /// <param name="dtFPSum">发票总数</param>
        /// <param name="dtFPClass">发票分类</param>
        /// <param name="dtAccountClass">账目分类</param>
        void ShowPayMentItem(DataTable dtFPSum, DataTable dtFPClass, DataTable dtAccountClass);

        /// <summary>
        /// 绑定预交金数据
        /// </summary>
        /// <param name="dtDepositList">预交金数据列表</param>
        void ShowDepositItem(DataTable dtDepositList);

        /// <summary>
        /// 住院缴款数据
        /// </summary>
        IP_Account CurAccount { get; set; }
    }
}
