using System;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_DrugManage.Winform.IView.FinanceMgr
{
    /// <summary>
    /// 药品月结
    /// </summary>
    interface IFrmBalance:IBaseView
    {
        /// <summary>
        /// 加载上次月结时间
        /// </summary>
        /// <param name="t1">开始时间</param>
        /// <param name="t2">结束时间</param>
        void BindBalanceTimes(DateTime t1, DateTime t2);

        /// <summary>
        /// 绑定药剂科室控件
        /// </summary>
        /// <param name="dtDrugDept">数据源</param>
        void BindDrugDept(DataTable dtDrugDept);

        /// <summary>
        /// 加载月结设置日期
        /// </summary>
        /// <param name="day">天数</param>
        void BindBalanceDays(int day);

        /// <summary>
        /// 绑定月结时间
        /// </summary>
        /// <param name="dt">月结时间</param>
        void BindDataGrid(DataTable dt);

        /// <summary>
        /// 设置月结按钮是否可用
        /// </summary>
        /// <param name="flag">是否可用</param>
        void SetBtnEnable(bool flag);

        /// <summary>
        /// 绑定对账信息
        /// </summary>
        /// <param name="dt">对账信息</param>
        void BindCheckAccount(DataTable dt);

        /// <summary>
        /// 设置文本显示值
        /// </summary>
        /// <param name="text">文本显示值</param>
        void SetLabelText(string text);
    }
}
