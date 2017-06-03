using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_DrugManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 库存查询
    /// </summary>
    interface IFrmStoreQuery : IBaseView
    {
        #region 绑定控件
        /// <summary>
        /// 绑定库房选择框控件
        /// </summary>
        /// <param name="dt">库房数据</param>
        /// <param name="loginDeptID">当前登录科室ID</param>
        void BindStoreRoomCombox(DataTable dt, int loginDeptID);

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
        /// 绑定批次数据
        /// </summary>
        /// <param name="dt">药品【批次</param>
        void BindStoreBatchGrid(DataTable dt);

        #endregion

        #region 获取界面数据
        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>表头查询条件</returns>
        Dictionary<string, string> GetQueryCondition();

        /// <summary>
        /// 获取库存标识ID
        /// </summary>
        /// <returns>库存标识ID</returns>
        int GetStorageID();

        /// <summary>
        /// 取得登录人姓名
        /// </summary>
        /// <param name="empName">登录人姓名</param>
        /// <param name="hospitalName">医院名称</param>
        void GetLoginName(string  empName,string hospitalName);
        #endregion
    }
}
