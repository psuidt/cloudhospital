using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.MaterialManage;

namespace HIS_MaterialManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 物资库位管理接口
    /// </summary>
    public interface IFrmLocation : IBaseView
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        int LocationID { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>
        int DeptId { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        int Level { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        int ParentId { get; set; }

        /// <summary>
        /// 绑定库房选择框控件
        /// </summary>
        /// <param name="dt">库房数据</param>
        /// <param name="loginDeptID">当前登录科室ID</param>
        void BindDeptRoom(DataTable dt, int loginDeptID);

        /// <summary>
        /// 绑定物资类型TextBox控件
        /// </summary>
        /// <param name="typeId">物资类型Id</param>
        /// <param name="typeName">物资类型名称</param>
        void BindMaterialTypeTextBox(int typeId, string typeName);

        /// <summary>
        /// 加载库位节点信息
        /// </summary>
        /// <param name="locationlist">库位节点信息</param>
        void GetLocationList(List<MW_Location> locationlist);

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
