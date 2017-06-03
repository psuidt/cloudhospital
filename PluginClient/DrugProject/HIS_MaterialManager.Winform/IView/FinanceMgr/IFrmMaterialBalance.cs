using System;
using System.Data;

namespace HIS_MaterialManage.Winform.IView.FinanceMgr
{
    /// <summary>
    /// 物资月结接口
    /// </summary>
    interface IFrmMaterialBalance
    {
        /// <summary>
        /// 绑定上次月结时间
        /// </summary>
        /// <param name="t1">上次月结开始时间</param>
        /// <param name="t2">上次月结结束时间</param>
        void BindBalanceTimes(DateTime t1, DateTime t2);

        /// <summary>
        /// 绑定药剂科室控件
        /// </summary>
        /// <param name="dtDrugDept">数据源</param>
        void BindDrugDept(DataTable dtDrugDept);

        /// <summary>
        /// 绑定月结天
        /// </summary>
        /// <param name="day">每月月结日期</param>
        void BindBalanceDays(int day);

        /// <summary>
        /// 绑定月结记录网格记录
        /// </summary>
        /// <param name="dt">月结记录</param>
        void BindDataGrid(DataTable dt);

        /// <summary>
        /// 这只界面控件是否可用
        /// </summary>
        /// <param name="flag">状态</param>
        void SetBtnEnable(bool flag);

        /// <summary>
        /// 绑定物资记录网格信息
        /// </summary>
        /// <param name="dt">物资信息</param>
        void BindCheckAccount(DataTable dt);

        /// <summary>
        /// 设置操作结果
        /// </summary>
        /// <param name="text">操作结果信息</param>
        void SetLabelText(string text);
    }
}
