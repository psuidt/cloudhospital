using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_DrugManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 住院发药接口
    /// </summary>
    interface IFrmIPDisp: IBaseView
    {
        /// <summary>
        /// 绑定临床科室ShowCard
        /// </summary>
        /// <param name="dtClinicalDept">临床科室数据</param>
        void BindClinicalDeptShowCard(DataTable dtClinicalDept);

        /// <summary>
        /// 绑定统领单类型
        /// </summary>
        /// <param name="dtBillType">统领单药品类型</param>
        void BindIPDrugBillTypeComboBox(DataTable dtBillType);

        /// <summary>
        /// 绑定待发药树控件
        /// </summary>
        /// <param name="dtHead">统领单头</param>
        void LoadBillTree(DataTable dtHead);

        /// <summary>
        /// 绑定已发药树控件
        /// </summary>
        /// <param name="dtHead">统领单头</param>
        void LoadCompleteBillTree(DataTable dtHead);

        /// <summary>
        /// 取得待发药单据头查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        Dictionary<string, string> GetIPBillHeadCondition();

        /// <summary>
        /// 取得待发药明细查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        Dictionary<string, string> GetIPBillDetailCondition();

        /// <summary>
        /// 取得已发药单据头查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        Dictionary<string, string> GetDispBillHeadCondition();

        /// <summary>
        /// 取得已发药明细查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        Dictionary<string, string> GetDispBillDetailCondition();

        /// <summary>
        /// 设置发药明细变量
        /// </summary>
        /// <param name="dt">发药明细数据集</param>
        void SetSendBillDetail(DataTable dt);

        /// <summary>
        /// 绑定待发药表头Grid
        /// </summary>
        /// <param name="dtBillHead">待发药表头数据集</param>
        void BindBillHeadGrid(DataTable dtBillHead);

        /// <summary>
        /// 绑定待发药单据明细Grid
        /// </summary>
        /// <param name="dtBillDetail">待发药单据明细数据集</param>
        void BindBillDetailGrid(DataTable dtBillDetail);

        /// <summary>
        /// 绑定已发药单据头Grid
        /// </summary>
        /// <param name="dtBillHead">已发药单据表头数据集</param>
        void BindDispHeadGrid(DataTable dtBillHead);

        /// <summary>
        /// 绑定已发药单据明细Grid
        /// </summary>
        /// <param name="dtBillDetail">已发药单据明细数据集</param>
        void BindDispDetailGrid(DataTable dtBillDetail);

        /// <summary>
        /// 绑定库房下拉框
        /// </summary>
        /// <param name="dtStoreRoom">库房数据集</param>
        void BindStoreRoomCombobox(DataTable dtStoreRoom);

        /// <summary>
        /// 通过单据头Ｉｄ获取单据明细，绑定明细grid
        /// </summary>
        /// <param name="dt">明细</param>
        void BindBillDetailByHeadIdGrid(DataTable dt);

        /// <summary>
        /// 处理过滤的明细选中情况
        /// </summary>
        /// <param name="selectedDetail">过滤数据</param>
        void HandleFilterDetail(DataTable selectedDetail);

        /// <summary>
        /// 通过树节点过滤已发药明细
        /// </summary>
        /// <param name="dt">已发药明细数据集</param>
        void BindDispDetalByTreeNodeGrid(DataTable dt);

        /// <summary>
        /// 病人费用信息
        /// </summary>
        DataTable dtFee { get; set; }
        
    }
}
