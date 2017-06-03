using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    /// 门诊结算明细查询界面接口
    /// </summary>
    public   interface IFrmOpCostSearch:IBaseView
    {
        /// <summary>
        /// 绑定收费员
        /// </summary>
        /// <param name="dtCharger">收费员</param>
         void BindCharge(DataTable dtCharger);

        /// <summary>
        /// 绑定病人类型
        /// </summary>
        /// <param name="dtPatType">病人类型</param>
        void BindPatType(DataTable dtPatType);

        /// <summary>
        /// 绑定科室
        /// </summary>
        /// <param name="dtDepts">科室列表</param>
        void BindDepts(DataTable dtDepts);

        /// <summary>
        /// 绑定医生
        /// </summary>
        /// <param name="dtDoctors">医生列表</param>
        void BindDoctors(DataTable dtDoctors);

        /// <summary>
        /// 获取查询条件
        /// </summary>
        Dictionary<string, object> QueryDictionary { get; set; }

        /// <summary>
        /// 重置检索条件
        /// </summary>
        void ClearQuerySet();

        /// <summary>
        /// 绑定查询结果
        /// </summary>
        /// <param name="dtPayMentInfo">支付数据</param>
        /// <param name="dtItemInfo">项目数据</param>
        void BindQueryData(DataTable dtPayMentInfo, DataTable dtItemInfo);

        /// <summary>
        /// 补打票据的结算ID
        /// </summary>
        int PrintCostHeadId { get; set; }
    }
}
