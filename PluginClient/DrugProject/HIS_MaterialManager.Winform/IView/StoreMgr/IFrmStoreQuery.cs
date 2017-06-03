using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_MaterialManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 物资库存接口
    /// </summary>
    public interface IFrmStoreQuery : IBaseView
    {
        /// <summary>
        /// 绑定科室选择框控件
        /// </summary>
        /// <param name="dtDept">科室数据</param>
        /// <param name="loginDeptID">当前登录科室ID</param>
        void BindDeptRoom(DataTable dtDept, int loginDeptID);

        /// <summary>
        /// 绑定物资类型TextBox控件
        /// </summary>
        /// <param name="typeId">物资类型Id</param>
        /// <param name="typeName">物资类型名称</param>
        void BindMaterialTypeTextBox(int typeId, string typeName);

        /// <summary>
        /// 绑定库存表格
        /// </summary>
        /// <param name="dt">库存数据</param>
        void BindStoreGrid(DataTable dt);

        /// <summary>
        /// 绑定批次数据
        /// </summary>
        /// <param name="dt">物资批次</param>
        void BindStoreBatchGrid(DataTable dt);

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        Dictionary<string, string> GetQueryCondition();

        /// <summary>
        /// 获取库存标识ID
        /// </summary>
        /// <returns>库存标识ID</returns>
        int GetStorageID();
    }
}
