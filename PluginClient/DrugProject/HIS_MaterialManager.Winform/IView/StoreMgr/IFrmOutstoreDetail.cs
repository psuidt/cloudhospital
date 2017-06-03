using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.MaterialManage;

namespace HIS_MaterialManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 出库单明细界面接口
    /// </summary>
    interface IFrmOutstoreDetail : IBaseView
    {
        /// <summary>
        /// 正则加载 
        /// </summary>
        void SetGridExpress();

        /// <summary>
        /// 绑定出库单录入ShowCard
        /// </summary>
        /// <param name="dt">药品信息</param>
        void BindMaterialInfoCard(DataTable dt);

        /// <summary>
        /// 绑定科室
        /// </summary>
        /// <param name="dt">科室</param>
        void BindDept(DataTable dt);

        /// <summary>
        /// 绑定业务类型
        /// </summary>
        /// <param name="dtOpType">数据源</param>
        void BindOpType(DataTable dtOpType);

        /// <summary>
        /// 从界面获取药库出库表头信息
        /// </summary>
        /// <returns>药库出库表头信息</returns>
        MW_OutStoreHead GetHeadInfo();

        /// <summary>
        /// 药库主表实体对象
        /// </summary>
        MW_OutStoreHead CurretOutStoreHead { get; set; }

        /// <summary>
        /// 删除的明细数据
        /// </summary>
        /// <returns>明细集合</returns>
        List<int> GetDeleteDetails();

        /// <summary>
        /// 根据单据状态更新控件状态
        /// </summary>
        /// <param name="billStatus">单据编辑状态</param>
        void InitControStatus(MWEnum.BillEditStatus billStatus);

        /// <summary>
        /// 显示出库单总金额
        /// </summary>
        /// <param name="stockFee">进货金额</param>
        /// <param name="retailFee">零售金额</param>
        void ShowTotalFee(decimal stockFee, decimal retailFee);

        /// <summary>
        /// 绑定明细表信息
        /// </summary>
        /// <param name="dt">明细数据源</param>
        void BindDetailsGrid(DataTable dt);

        /// <summary>
        ///绑定药库表头实体
        /// </summary>
        /// <param name="inHead">表头实体</param>
        void BindOutHeadInfo<THead>(THead inHead);

        /// <summary>
        /// 创建新单据更新界面
        /// </summary>
        void NewBillClear();

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        void CloseCurrentWindow();
    }
}
