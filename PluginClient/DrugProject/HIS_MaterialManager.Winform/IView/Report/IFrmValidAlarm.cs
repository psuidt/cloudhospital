using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_MaterialManage.Winform.IView.Report
{
    /// <summary>
    /// 物资库存报警接口
    /// </summary>
    public interface IFrmValidAlarm : IBaseView
    {
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
        /// 获取查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        Dictionary<string, string> GetQueryCondition();
    }
}
