using System;
using System.Collections.Generic;
using System.Data;

namespace HIS_IPNurse.Dao
{
    /// <summary>
    /// 打印申请单
    /// </summary>
    public interface IExamListDao
    {
        /// <summary>
        /// 获取申请单数据
        /// </summary>
        /// <param name="iDeptId">科室ID</param>
        /// <param name="iApplyType">申请类型</param>
        /// <param name="dApplyDate">申请日期</param>
        /// <param name="iOrderCategory">长临嘱 0-长嘱 1-临嘱</param>
        /// <param name="iState">打印状态 0-未打印 1-已打印 -1-全部</param>
        /// <returns>申请单数据</returns>
        DataTable GetExamList(int iDeptId, int iApplyType, DateTime dApplyDate, int iOrderCategory, int iState);

        /// <summary>
        /// 更新打印状态
        /// </summary>
        /// <param name="iApplyHeadIDList">申请单ID集合</param>
        /// <param name="iEmpId">操作员ID</param>
        /// <returns>true：更新成功</returns>
        bool UpdateApplyPrint(List<int> iApplyHeadIDList, int iEmpId);
    }
}
