using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_DrugManage.Winform.IView.Report
{
    /// <summary>
    /// 药房发药统计
    /// </summary>
    public interface IFrmDispRpt : IBaseView
    {
        /// <summary>
        /// 药房发药表头行对象
        /// </summary>
        DataRow CurrentHead { get; set; }

        /// <summary>
        /// 绑定科室信息
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
        /// 查询条件
        /// </summary>
        /// <returns>获取查询条件</returns>
        Dictionary<string, string> GetQueryCondition();

        /// <summary>
        /// 绑定药房药品
        /// </summary>
        /// <param name="dt">药房药品数据源</param>
        void BindDGDrug(DataTable dt);

        /// <summary>
        /// 绑定药房数据
        /// </summary>
        /// <param name="dt">药房数据源</param>
        void BindDGDept(DataTable dt);
    }
}
