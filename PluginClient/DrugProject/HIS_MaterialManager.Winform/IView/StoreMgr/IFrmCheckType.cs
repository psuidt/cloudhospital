using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_MaterialManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 盘点提取数据界面接口
    /// </summary>
    interface IFrmCheckType : IBaseView
    {
        #region 绑定控件
        /// <summary>
        /// 绑定药品类型
        /// </summary>
        /// <param name="dtDrugType">药品类型</param>
        void BindMaterialType(DataTable dtDrugType);

        #endregion

        #region 获取界面数据
        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>表头查询条件</returns>
        Dictionary<string, string> GetQueryCondition(int deptID);
        #endregion
    }
}
