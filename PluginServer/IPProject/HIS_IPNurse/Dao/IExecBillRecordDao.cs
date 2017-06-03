using System;
using System.Collections.Generic;
using System.Data;

namespace HIS_IPNurse.Dao
{
    /// <summary>
    /// 打印执行单数据接口
    /// </summary>
    public interface IExecBillRecordDao
    {
        /// <summary>
        /// 获取执行单类型
        /// </summary>
        /// <returns>执行单类型</returns>
        DataTable GetReportTypeList();

        /// <summary>
        /// 获取执行单数据
        /// </summary>
        /// <param name="iDeptId">科室ID</param>
        /// <param name="iType">执行单类型</param>
        /// <param name="dFeeDate">费用日期</param>
        /// <param name="iOrderCategory">长临嘱 0-长嘱 1-临嘱</param>
        /// <param name="iState">打印状态 0-未打印 1-已打印 -1-全部</param>
        /// <returns>执行单数据</returns>
        DataTable GetExcuteList(int iDeptId, int iType, DateTime dFeeDate, int iOrderCategory, int iState,bool typeName);

        /// <summary>
        /// 设置执行单状态
        /// </summary>
        /// <param name="iExecIdList">需要设置的id集</param>
        /// <param name="iState">设置状态 0：未打印 1：打印 </param>
        /// <param name="iEmpId">修改人id</param>
        /// <returns>执行单状态</returns>
        bool SetExcuteList(List<int> iExecIdList, int iState, int iEmpId);
    }
}
