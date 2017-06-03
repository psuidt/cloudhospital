using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_MaterialManage.Winform.IView.Report
{
    /// <summary>
    /// 物资汇总接口
    /// </summary>
    public interface IFrmGoodsCollection : IBaseView
    {
        /// <summary>
        /// 绑定月结信息
        /// </summary>
        /// <param name="dt">月结信息</param>
        void BindBalance(DataTable dt);

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
        /// 绑定来往科室
        /// </summary>
        /// <param name="dt">来往科室</param>
        void BindDept(DataTable dt);

        /// <summary>
        /// 绑定供应商
        /// </summary>
        /// <param name="dt">供应商</param>
        void BindSupport(DataTable dt);

        /// <summary>
        /// 绑定报表数据
        /// </summary>
        /// <param name="dt">报表数据</param>
        void BindDgData(DataTable dt);

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>表头查询条件</returns>
        Dictionary<string, string> GetQueryCondition();
    }
}
