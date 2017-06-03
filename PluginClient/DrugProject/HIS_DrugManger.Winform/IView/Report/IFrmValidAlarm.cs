using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_DrugManage.Winform.IView.Report
{
    /// <summary>
    /// 有效期预警
    /// </summary>
    public interface IFrmValidAlarm : IBaseView
    {
        /// <summary>
        /// 绑定类型
        /// </summary>
        /// <param name="dt">数据源</param>
        void BindTypeCombox(DataTable dt);

        /// <summary>
        /// 绑定药品数据
        /// </summary>
        /// <param name="dt">数据源</param>
        void BindStoreGrid(DataTable dt);

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <returns>获取查询条件</returns>
        Dictionary<string, string> GetQueryCondition();
    }
}
