using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 药品出库明细单
    /// </summary>
    interface IFrmOutstoreDetail : IBaseView
    {
        /// <summary>
        /// 正则加载 
        /// </summary>
        void SetGridExpress();

        /// <summary>
        /// Gets or sets出库状态是否是申请单转出库单
        /// </summary>
        /// <returns>返回是否是申请单转出库单</returns>
        bool IsApplyStatus { get; set; }

        /// <summary>
        /// 设置表头控件的值
        /// </summary>
        /// <param name="row">表头行对象</param>
        void SetHeadValue(DataRow row);

        /// <summary>
        /// 绑定出库单录入ShowCard
        /// </summary>
        /// <param name="dtDrugInfo">药品信息</param>
        void BindDrugInfoCard(DataTable dtDrugInfo);

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
        DW_OutStoreHead GetInHeadInfoDW();

        /// <summary>
        /// 从界面获取药房出库表头信息
        /// </summary>
        /// <returns>药房出库表头信息</returns>
        DS_OutStoreHead GetHeadInfoDS();

        /// <summary>
        /// Gets or sets药库主表实体对象
        /// </summary>
        DW_OutStoreHead CurretDwOutStoreHead { get; set; }

        /// <summary>
        /// Gets or sets药房出库主表实体对象
        /// </summary>
        DS_OutStoreHead CurrentDSOuttoreHead { get; set; }

        /// <summary>
        /// 获取删除的明细数据
        /// </summary>
        /// <returns>删除的明细数据</returns>
        List<int> GetDeleteDetails();

        /// <summary>
        /// 根据单据状态更新控件状态
        /// </summary>
        /// <param name="billStatus">单据编辑状态</param>
        void InitControStatus(DGEnum.BillEditStatus billStatus);

        /// <summary>
        /// 显示出库单总金额
        /// </summary>
        /// <param name="stockFee">进货金额</param>
        /// <param name="retailFee">零售金额</param>
        void ShowTotalFee(decimal stockFee, decimal retailFee);

        /// <summary>
        /// 绑定出库明细表
        /// </summary>
        /// <param name="dt">出库明细表数据源</param>
        void BindDetailsGrid(DataTable dt);

        /// <summary>
        ///绑定药库表头实体
        /// </summary>
        /// <param name="inHead">表头实体</param>
        void BindInHeadInfo<THead>(THead inHead);

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
