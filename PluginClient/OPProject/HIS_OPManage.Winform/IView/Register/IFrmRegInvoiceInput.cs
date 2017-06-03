using System.Data;
using HIS_Entity.OPManage;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    /// 挂号退费界面接口类
    /// </summary>
    public interface IFrmRegInvoiceInput
    {
        /// <summary>
        /// 退费票据号
        /// </summary>
        string  InputVoiceNO { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        /// <param name="dtPayInfo">支付方式数据</param>
        void LoadPayInfo(DataTable dtPayInfo);

        /// <summary>
        /// 是否是医保病人
        /// </summary>
        bool HaveMedicarePay { get; set; }

        /// <summary>
        /// 医保刷卡信息
        /// </summary>
        string MedicareInfo { get; set; }

        /// <summary>
        /// 退费挂号对象
        /// </summary>
        OP_PatList RegPatList { get; set; }

        /// <summary>
        /// 退费成功与否
        /// </summary>
        bool IsBackRegOK { get; }
    }
}
