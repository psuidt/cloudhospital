using System;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 预交金管理接口
    /// </summary>
    public interface IPrepaidPaymentSys : IBaseView
    {
        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="patListDt">病人列表</param>
        void Binding_GrdPatList(DataTable patListDt);

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="deptDt">科室列表</param>
        void Bind_txtDeptList(DataTable deptDt);

        /// <summary>
        /// 绑定病人预交金缴纳记录列表
        /// </summary>
        /// <param name="payListDt">预交金缴纳记录列表</param>
        /// <param name="isAdd">true：收款/false：退款</param>
        /// <param name="depositFee">预交金总额</param>
        /// <param name="sumFee">病人总费用</param>
        void Binding_grdPayDetailList(DataTable payListDt, bool isAdd, decimal depositFee, decimal sumFee);

        /// <summary>
        /// 开始时间
        /// </summary>
        DateTime StartDateTime { get; }

        /// <summary>
        /// 结束时间
        /// </summary>
        DateTime EndDateTime { get; }

        /// <summary>
        /// 科室ID
        /// </summary>
        int DeptID { get; }

        /// <summary>
        /// 检索条件
        /// </summary>
        string SeachParm { get; }

        /// <summary>
        /// 病人状态
        /// </summary>
        string PatStatus { get; }

        /// <summary>
        /// 住院流水号
        /// </summary>
        decimal SerialNumber { get; }

        /// <summary>
        /// 会员ID
        /// </summary>
        int MemberID { get; }

        /// <summary>
        /// 病人登记ID
        /// </summary>
        int PatListID { get; }

        /// <summary>
        /// 病人登记科室ID
        /// </summary>
        int PatDeptID { get; }

        /// <summary>
        /// 设置网格中退费数据的颜色
        /// </summary>
        void SetGridColor();
    }
}
