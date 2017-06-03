using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    /// 门诊处方查询界面接口类
    /// </summary>
    public interface IFrmRecipteQuery : IBaseView
    {
        /// <summary>
        /// 查询条件
        /// </summary>
        Dictionary<string, object> QueryDictionary { get; set; }

        /// <summary>
        /// 收费员
        /// </summary>
        /// <param name="dtCharger">收费员数据</param>
        void BindCharger(DataTable dtCharger);

        /// <summary>
        /// 病人类型
        /// </summary>
        /// <param name="dtPatType">病人类型数据</param>
        void BindPatType(DataTable dtPatType);

        /// <summary>
        /// 绑定查询数据
        /// </summary>
        /// <param name="dtData">处方头数据</param>
        void BindQueryData(DataTable dtData);

        /// <summary>
        /// 绑定明细数据
        /// </summary>
        /// <param name="dtDetail">处方明细数据</param>
        void BindDetailData(DataTable dtDetail);
    }
}
