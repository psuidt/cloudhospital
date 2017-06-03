using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_DrugManage.Winform.IView.Report
{
    /// <summary>
    /// 药品分类流水
    /// </summary>
    public interface IFrmFlowAccount : IBaseView
    {
        /// <summary>
        ///  绑定库房下拉框
        /// </summary>
        /// <param name="dtDept">科室数据源</param>
        /// <param name="loginDeptID">登录科室</param>
        void BindDeptRoom(DataTable dtDept, int loginDeptID);

        /// <summary>
        ///  绑定类型数据源
        /// </summary>
        /// <param name="dt">数据源</param>
        void BindTypeCombox(DataTable dt);

        /// <summary>
        /// 绑定药品子类型
        /// </summary>
        /// <param name="dt">数据源</param>
        void BindChildDrugType(DataTable dt);

        /// <summary>
        /// 绑定科室
        /// </summary>
        /// <param name="dt">数据源</param>
        void BindDept(DataTable dt);

        /// <summary>
        /// 绑定供应商
        /// </summary>
        /// <param name="dt">数据源</param>
        void BindSupport(DataTable dt);

        /// <summary>
        /// 绑定药品数据
        /// </summary>
        /// <param name="dt">数据源</param>
        void BindDgData(DataTable dt);

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <returns>获取查询条件</returns>
        Dictionary<string, string> GetQueryCondition();

        /// <summary>
        /// 绑定药房月结数据
        /// </summary>
        /// <param name="dt">数据源</param>
        void BindBalance(DataTable dt);
    }
}
