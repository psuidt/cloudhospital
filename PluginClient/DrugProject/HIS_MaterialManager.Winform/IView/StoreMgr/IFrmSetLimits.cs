using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_MaterialManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 库存上下线接口
    /// </summary>
    interface IFrmSetLimits : IBaseView
    {
        #region 绑定控件
        /// <summary>
        /// 绑定供库房控件
        /// </summary>
        /// <param name="dtDept">数据源</param>
        /// <param name="loginDeptId">登录部门ID</param>
        void BindStoreRoomCombox(DataTable dtDept, int loginDeptId);

        /// <summary>
        /// 绑定药品选择卡片
        /// </summary>
        /// <param name="dtMaterial">药品信息</param>
        void BindMaterialSelectCard(DataTable dtMaterial);

        /// <summary>
        /// 绑定库存上下限grid
        /// </summary>
        /// <param name="dtLimit">库存数据</param>
        void BindStoreLimitGrid(DataTable dtLimit);

        #endregion

        #region 获取界面数据
        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>表头查询条件</returns>
        Dictionary<string, string> GetQueryCondition();
        #endregion
    }
}
