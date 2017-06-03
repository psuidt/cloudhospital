using System;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.MemberManage;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 出院结算接口
    /// </summary>
    public interface IDischargeSettlement : IBaseView
    {
        /// <summary>
        /// 入院开始时间
        /// </summary>
        DateTime StartDateTime { get; }

        /// <summary>
        /// 入院结束时间
        /// </summary>
        DateTime EndDateTime { get; }

        /// <summary>
        /// 入院科室ID
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
        /// 病人登记ID
        /// </summary>
        int PatListID { get; }

        /// <summary>
        /// 病人姓名
        /// </summary>
        string PatName { get; }

        /// <summary>
        /// 病人入院科室ID
        /// </summary>
        int PatDeptID { get; }

        /// <summary>
        /// 病人住院号
        /// </summary>
        decimal SerialNumber { get; }

        /// <summary>
        /// 当前票据号
        /// </summary>
        string InvoiceNo { get; set; }

        /// <summary>
        /// 结算类型
        /// </summary>
        int CostType { get; set; }

        /// <summary>
        /// 病人类型ID
        /// </summary>
        int PatTypeID { get; }

        /// <summary>
        /// 病人入院时间
        /// </summary>
        DateTime PatEnterHDate { get; }

        /// <summary>
        /// 会员账号ID
        /// </summary>
        int MemberAccountID { get; }

        /// <summary>
        /// 会员ID
        /// </summary>
        int MemberID { get; }

        /// <summary>
        /// 会员卡号
        /// </summary>
        string CardNO { get; }

        /// <summary>
        /// 优惠明细数据
        /// </summary>
        DiscountInfo ResDiscountInfo { get; set; }

        /// <summary>
        /// 预交金总额
        /// </summary>
        decimal DepositFee { get; }

        /// <summary>
        /// 住院费用总额
        /// </summary>
        decimal TotalFee { get; }

        /// <summary>
        /// 当期病人
        /// </summary>
        DataTable PatientDt { get; }

        /// <summary>
        /// 费用分类列表
        /// </summary>
        DataTable PatFeeDt { get; }

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="patList">病人列表</param>
        void Binding_GrdPatList(DataTable patList);

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="deptDt">科室列表</param>
        void Bind_txtDeptList(DataTable deptDt);

        /// <summary>
        /// 绑定费用分类表列表
        /// </summary>
        /// <param name="feeTypeDt">费用分类表列表</param>
        void Bind_FeeTypeList(DataTable feeTypeDt);

        /// <summary>
        /// 绑定病人预交金列表
        /// </summary>
        /// <param name="payList">预交金列表</param>
        /// <param name="sumFee">病人住院总金额</param>
        /// <param name="sumPay">病人已交预交金总额</param>
        void Bind_PrepaidPaymentList(DataTable payList, decimal sumFee, decimal sumPay);

        /// <summary>
        /// 设置上一次结算病人信息
        /// </summary>
        /// <param name="costData">结算病人信息</param>
        void SetLastPatCostData(string costData);
    }
}
