using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 领药申请明细
    /// </summary>
    interface IFrmApplyDetails:IBaseView
    {
        /// <summary>
        /// 创建新单据更新界面
        /// </summary>
        void NewBillClear();

        /// <summary>
        /// 获取删除的明细数据
        /// </summary>
        /// <returns>删除的明细数据集</returns>
        List<int> GetDeleteDetails();

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        void CloseCurrentWindow();

        /// <summary>
        /// 绑定科室
        /// </summary>
        /// <param name="dt">科室数据集</param>
        void BindWareHourse(DataTable dt);

        /// <summary>
        /// Gets or sets申请单实体类
        /// </summary>
        DS_ApplyHead CurrentApplyHead { get; set; }

        /// <summary>
        /// 绑定出库单录入ShowCard
        /// </summary>
        /// <param name="dtDrugInfo">药品信息</param>
        void BindDrugInfoCard(DataTable dtDrugInfo);

        /// <summary>
        ///Gets or sets 明细表 数据
        /// </summary>
        /// <returns>数据集</returns>
        DataTable CurrentDetail { get; set; }

        /// <summary>
        /// 补充表头信息
        /// </summary>
        /// <returns>表头信息</returns>
        DS_ApplyHead GetHeadInfo();

        /// <summary>
        /// 绑定从表
        /// </summary>
        /// <param name="dt">从表数据集</param>
        void BindApplyDetail(DataTable dt);

        /// <summary>
        /// 绑定头表信息
        /// </summary>
        /// <param name="head">头表数据源</param>
        void BindInHeadInfo(DS_ApplyHead head);

        /// <summary>
        /// 初始化状态
        /// </summary>
        /// <param name="head">头表数据源</param>
        void InitControStatus(DS_ApplyHead head);
    }
}
