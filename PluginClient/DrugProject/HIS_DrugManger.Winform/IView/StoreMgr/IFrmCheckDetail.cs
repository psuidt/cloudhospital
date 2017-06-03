using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 盘点单明细界面接口
    /// </summary>
    interface IFrmCheckDetail: IBaseView
    {      
        /// <summary>
        ///绑定盘点表头实体
        /// </summary>
        /// <param name="inHead">表头实体</param>
        void BindInHeadInfo<THead>(THead inHead);

        /// <summary>
        /// 绑定明细表信息
        /// </summary>
        /// <param name="inDetals">明细数据源</param>
        void BindInDetails(DataTable inDetals);

        /// <summary>
        /// 绑定盘点单录入ShowCard
        /// </summary>
        /// <param name="dtDrugInfo">药品信息</param>
        void BindDrugInfoCard(DataTable dtDrugInfo);

        /// <summary>
        /// 绑定药品定位查询ShowCard
        /// </summary>
        /// <param name="dtDrugInfo">药品信息</param>
        void BindDrugPositFindCard(DataTable dtDrugInfo);

        /// <summary>
        /// 显示盘点单总金额
        /// </summary>
        /// <param name="actSum">账存金额</param>
        /// <param name="factSum">盘存金额</param>
        void ShowTotalFee(decimal actSum, decimal factSum);

        /// <summary>
        /// 从界面获取盘点表头信息
        /// </summary>
        /// <returns>药库入库表头信息</returns>
        DW_CheckHead GetInHeadInfoDW();

        /// <summary>
        /// 获取删除的单据明细ID
        /// </summary>
        /// <returns>已删除的单据明细ID</returns>
        List<int> GetDeleteDetails();

        /// <summary>
        /// 从界面获取盘点表头信息
        /// </summary>
        /// <returns>药房入库表头信息</returns>
        DS_CheckHead GetInHeadInfoDS();

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
        /// 插入提取的库存数据
        /// </summary>
        /// <param name="dtRtn">库存药品信息</param>
        void InsertGetStorageData(DataTable dtRtn);
    }
}
