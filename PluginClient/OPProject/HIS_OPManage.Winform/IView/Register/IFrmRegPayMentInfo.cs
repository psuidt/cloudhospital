using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    /// 挂号支付界面接口类
    /// </summary>
    interface IFrmRegPayMentInfo : IBaseView
    {
        /// <summary>
        /// 挂号科室
        /// </summary>
        string RegDeptName { get; set; } 

        /// <summary>
        /// 挂号金额
        /// </summary>
        decimal RegTotalFee { get; set; }

        /// <summary>
        /// 当前发票号
        /// </summary>
        string InvoiceNO { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        /// <param name="dtRegPayment">支付方式列表</param>      
        void GetRegPayment(DataTable dtRegPayment);

        /// <summary>
        /// 医保支付金额
        /// </summary>
        decimal MedicarePay { get; set; }
        /// <summary>
        /// 医保统筹金额
        /// </summary>
        decimal MedicareMIPay { get; set; }

        /// <summary>
        /// 医保个帐金额
        /// </summary>
        decimal MedicarePersPay { get; set; }

        /// <summary>
        /// 应付金额
        /// </summary>
        decimal ShoudPay { get; set; }

        /// <summary>
        ///  实付金额
        /// </summary>
        decimal ActPay { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        decimal PromFee { get; set; }

        /// <summary>
        /// 医保试算按钮是否可用
        /// </summary>
        bool ReadMedicareEnabled { set; }

        /// <summary>
        ///  exepanel是否可见
        /// </summary>
        bool ExpMedicardInfoVisible { set; }

        /// <summary>
        /// 医保试算信息
        /// </summary>
        string SetMedicardInfo
        {
            set;
        }

        /// <summary>
        /// 获取支付方式ID
        /// </summary>
        string GetPatMentCode { get; }

        /// <summary>
        /// 医保预算ID
        /// </summary>
        int MIBudgetID { get; set; }

        /// <summary>
        /// 医保卡号
        /// </summary>
        string SocialCard { get; set; }

        /// <summary>
        /// 医保应用号
        /// </summary>
        string IdentityNum { get; set; }
    }
}