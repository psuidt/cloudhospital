using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 库位管理
    /// </summary>
    interface IFrmLocation : IBaseView
    {
        /// <summary>
        /// 库位ID
        /// </summary>
        int LocationID { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>
        int DeptId { get; set; }

        /// <summary>
        /// 库位等级
        /// </summary>
        int Level { get; set; }

        /// <summary>
        /// 库位父级ID
        /// </summary>
        int ParentId { get; set; }

        /// <summary>
        /// 绑定库房选择框控件
        /// </summary>
        /// <param name="dt">库房数据</param>
        /// <param name="loginDeptID">当前登录科室ID</param>
        void BindDeptRoom(DataTable dt, int loginDeptID);

        /// <summary>
        /// 获取库位节点信息
        /// </summary>
        /// <param name="locationlist">库位节点信息集</param>
        void GetLocationList(List<DG_Location> locationlist);

        /// <summary>
        /// 绑定药品类型下拉框控件
        /// </summary>
        /// <param name="dt">药品类型</param>
        void BindTypeCombox(DataTable dt);

        /// <summary>
        /// 绑定剂型选择卡片控件
        /// </summary>
        /// <param name="dt">药品剂型</param>
        void BindDosageShowCard(DataTable dt);

        /// <summary>
        /// 绑定库存表格
        /// </summary>
        /// <param name="dt">库存数据</param>
        void BindStoreGrid(DataTable dt);

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>表头查询条件</returns>
        Dictionary<string, string> GetQueryCondition();
    }
}
