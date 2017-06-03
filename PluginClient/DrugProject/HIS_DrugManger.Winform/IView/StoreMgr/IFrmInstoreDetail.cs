using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 入库单明细界面接口
    /// </summary>
    interface IFrmInstoreDetail : IBaseView
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        string TypeName { get; set; }

        /// <summary>
        /// 绑定科室参数
        /// </summary>
        /// <param name="dt">数据源</param>
        void BindDeptParameters(DataTable dt);

        /// <summary>
        /// 绑定供应商控件
        /// </summary>
        /// <param name="dtSupply">数据源</param>
        void BindSupply(DataTable dtSupply);

        /// <summary>
        ///绑定药库表头实体
        /// </summary>
        /// <param name="inHead">表头实体</param>
        void BindInHeadInfo<THead>(THead inHead);

        /// <summary>
        /// 绑定明细表信息
        /// </summary>
        /// <param name="inDetals">明细数据源</param>
        void BindInDetails(DataTable inDetals);

        /// <summary>
        /// 绑定入库单录入ShowCard
        /// </summary>
        /// <param name="dtDrugInfo">药品信息</param>
        void BindDrugInfoCard(DataTable dtDrugInfo);

        /// <summary>
        /// 绑定药品批次信息
        /// </summary>
        /// <param name="dtBatchInfo">批次信息</param>
        void BindDrugBatchCard(DataTable dtBatchInfo);

        /// <summary>
        /// 绑定业务类型
        /// </summary>
        /// <param name="dtOpType">数据源</param>
        void BindOpType(DataTable dtOpType);

        /// <summary>
        /// 显示入库单总金额
        /// </summary>
        /// <param name="stockFee">进货金额</param>
        /// <param name="retailFee">零售金额</param>
        void ShowTotalFee(decimal stockFee, decimal retailFee);

        /// <summary>
        /// 从界面获取药库入库表头信息
        /// </summary>
        /// <returns>药库入库表头信息</returns>
        DW_InStoreHead GetInHeadInfoDW();

        /// <summary>
        /// 获取删除的单据明细ID
        /// </summary>
        /// <returns>已删除的单据明细ID</returns>
        List<int> GetDeleteDetails();

        /// <summary>
        /// 从界面获取药房入库表头信息
        /// </summary>
        /// <returns>药房入库表头信息</returns>
        DS_InstoreHead GetInHeadInfoDS();

        /// <summary>
        /// 创建新单据更新界面
        /// </summary>
        void NewBillClear();

        /// <summary>
        /// 根据单据状态更新控件状态
        /// </summary>
        /// <param name="billStatus">单据编辑状态</param>
        void InitControStatus(DGEnum.BillEditStatus billStatus);

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        void CloseCurrentWindow();

        /// <summary>
        /// 转换采购单到入库单
        /// </summary>
        /// <param name="dtBuyPlanDetail">采购单明细</param>
        void ConvertBuyToInStore(DataTable dtBuyPlanDetail);
    }
}
