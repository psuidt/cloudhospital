using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.IView.FinanceMgr
{
    /// <summary>
    /// 采购计划接口
    /// </summary>
    interface IFrmPurchase : IBaseView
    {
        /// <summary>
        /// 绑定审核状态
        /// </summary>
        /// <param name="dt">单据数据集</param>
        void BindAuditStatus(DataTable dt);

        /// <summary>
        /// 绑定采购计划头表格
        /// </summary>
        /// <param name="dt">采购计划头表数据集</param>
        void BindPlanHeadGrid(DataTable dt);

        /// <summary>
        /// 绑定采购计划明细表表格
        /// </summary>
        /// <param name="dt">采购明细数据集</param>
        void BindPlanDetailGrid(DataTable dt);

        /// <summary>
        /// 取得登录用户对象
        /// </summary>
        /// <param name="loginUserInfo">登录用户对象</param>
        void GetLoginUserInfo(SysLoginRight loginUserInfo);

        /// <summary>
        /// 绑定库房下拉框数据
        /// </summary>
        /// <param name="dtStoreRoom">药库数据集</param>
        void BindStoreRoomComboxList(DataTable dtStoreRoom);

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>表头查询条件</returns>
        Dictionary<string, string> GetQueryCondition();

        /// <summary>
        /// 获取选中表头ID
        /// </summary>
        /// <returns>当前选中表头ID</returns>
        Dictionary<string, string> GetCurrentHeadID();

        /// <summary>
        /// 绑定药品信息选择卡片
        /// </summary>
        /// <param name="dtDrugInfo">药品信息</param>
        void BindDrugInfoCard(DataTable dtDrugInfo);

        /// <summary>
        /// 获取删除的单据明细ID
        /// </summary>
        /// <returns>已删除的单据明细ID</returns>
        List<int> GetDeleteDetails();

        /// <summary>
        /// 显示入库单总金额
        /// </summary>
        /// <param name="stockFee">进货金额</param>
        /// <param name="retailFee">零售金额</param>
        void ShowTotalFee(decimal stockFee, decimal retailFee);

        /// <summary>
        /// 取得采购计划头表信息
        /// </summary>
        /// <returns>采购计划表头实体</returns>
        DW_PlanHead GetPlanHeadInfo();

        /// <summary>
        /// 取得当前编辑单据明细
        /// </summary>
        /// <returns>单据明细</returns>
        DataTable GetPlanDetailInfo();

        /// <summary>
        /// 插入低于下限的药品
        /// </summary>
        /// <param name="dtRtn">药品数据</param>
        void InsertLessLowerLimitDrugData(DataTable dtRtn);

        /// <summary>
        /// 插入低于上限的药品数据
        /// </summary>
        /// <param name="dtRtn">药品数据</param>
        void InsertLessUpperLimitDrugData(DataTable dtRtn);
    }
}
