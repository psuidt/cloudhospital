using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    /// 退费消息界面接口类
    /// </summary>
    interface IFrmRefundMessage : IBaseView
    {
        /// <summary>
        /// 退费处方信息
        /// </summary>
        DataTable DtRefundPresc { get; set; }

        /// <summary>
        /// 显示病人信息
        /// </summary>
        /// <param name="row">病人信息</param>
        void SetPatInfo(DataRow row);

        /// <summary>
        /// 设置只读
        /// </summary>
        /// <param name="type">0退基本数只读1退包装数只读2已发药中药不能退3未发药中药可能按付退 4未发药开了包装数和基本数的</param>
        void SetReadOnly(int type);

        /// <summary>
        /// 退费消息查询界面
        /// </summary>
         DataTable dtQueryRefundPresc { get; set; }

        /// <summary>
        /// 原票据总金额
        /// </summary>
        string OldtotalFee { set; }

        /// <summary>
        /// 医技项目退费表
        /// </summary>
        DataTable DtRefundMedical { get; set; }
    }
}
