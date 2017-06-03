using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.IPManage;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 修改病床接口
    /// </summary>
    public interface IUpdateBed : IBaseView
    {
        /// <summary>
        /// 绑定床位费用弹出网格列表
        /// </summary>
        /// <param name="feeItemDataDt">床位费用列表</param>
        void Bind_FeeItemData(DataTable feeItemDataDt);

        /// <summary>
        /// 绑定医生列表
        /// </summary>
        /// <param name="doctorDt">医生列表</param>
        void Bind_txtCurrDoctor(DataTable doctorDt);

        /// <summary>
        /// 绑定护士列表
        /// </summary>
        /// <param name="currNurseDt">护士列表</param>
        void Bind_txtCurrNurse(DataTable currNurseDt);

        /// <summary>
        /// 绑定床位费用列表
        /// </summary>
        /// <param name="bedFreeDt">床位费用列表</param>
        /// <param name="isAdd">是否为加床</param>
        void Bind_BedFreeList(DataTable bedFreeDt,bool isAdd);

        /// <summary>
        /// 是否为新增床位
        /// </summary>
        bool IsAddBed { get; set; }

        /// <summary>
        /// 床位
        /// </summary>
        IP_BedInfo IPBedInfo { get; set; }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        void FormClose();
    }
}
