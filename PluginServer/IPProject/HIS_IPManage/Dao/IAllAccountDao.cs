using System;
using System.Data;

namespace HIS_IPManage.Dao
{
    /// <summary>
    /// 缴款
    /// </summary>
    public interface IAllAccountDao
    {
        /// <summary>
        /// 查询所有已经缴款记录
        /// </summary>
        /// <param name="bdate">缴款开始时间</param>
        /// <param name="edate">缴款结束时间</param>
        /// <param name="empid">操作员ID</param>
        /// <param name="status">状态</param>
        /// <returns>缴款记录</returns>
        DataTable GetAllAccountData(DateTime bdate, DateTime edate, int empid, int status);

        /// <summary>
        /// 获取缴款支付方式表
        /// </summary>
        /// <param name="bdate">缴款开始时间</param>
        /// <param name="edate">缴款结束时间</param>
        /// <param name="empid">操作员ID</param>
        /// <returns>缴款支付方式表</returns>
        DataTable GetAllAccountPayment(DateTime bdate, DateTime edate, int empid);

        /// <summary>
        /// 查询所有未缴款记录
        /// </summary>
        /// <param name="bdate">结算开始时间</param>
        /// <param name="edate">结算结束时间</param>
        /// <param name="empid">操作员ID</param>
        /// <returns>所有未缴款记录</returns>
        DataTable GetAllNotAccountData(DateTime bdate, DateTime edate, int empid);
    }
}
