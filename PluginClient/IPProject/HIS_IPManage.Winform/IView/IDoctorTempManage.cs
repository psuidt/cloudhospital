using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.IPManage;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 账单模板接口
    /// </summary>
    public interface IDoctorTempManage : IBaseView
    {
        /// <summary>
        /// 绑定模板头列表
        /// </summary>
        /// <param name="feeTempList">费用模板列表</param>
        /// <param name="tempHeadID">模板头ID</param>
        void Bind_FeeTempList(List<IP_FeeItemTemplateHead> feeTempList, int tempHeadID);

        /// <summary>
        /// 模板头
        /// </summary>
        IP_FeeItemTemplateHead FeeItemTemplateHead { get; }

        /// <summary>
        /// 模板明细
        /// </summary>
        DataTable FeeTempDetailDt { get; }

        /// <summary>
        /// 绑定账单模板明细列表
        /// </summary>
        /// <param name="feeDetailDt">账单模板明细列表</param>
        void Bind_RegItemShowCard(DataTable feeDetailDt);

        /// <summary>
        /// 绑定模板明细数据列表
        /// </summary>
        /// <param name="feeTempDetailsDt">模板明细数据列表</param>
        void Bind_FeeTempDetails(DataTable feeTempDetailsDt);

        /// <summary>
        /// 设置网格编辑状态
        /// </summary>
        /// <param name="state">状态</param>
        void SetGrdTempListState(bool state);
    }
}
