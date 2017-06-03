using System;
using System.Data;
using HIS_DrugManger.Dao;
using HIS_Entity.DrugManage;

namespace HIS_DrugManger.ObjectModel.Account
{
    /// <summary>
    /// 药库月结类
    /// </summary>
    class DWBalance : DrugBalance
    {
        /// <summary>
        /// 取消月结
        /// </summary>
        /// <param name="empID">月结操作员</param>
        /// <param name="deptID">月结药剂科室ID</param>
        public override void CancelMonthAccount(int empID, int deptID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据科室查询月结记录
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <returns>查询月结记录</returns>
        public override DataTable GetMonthBalaceByDept(int deptId)
        {
            return NewDao<IDWDao>().GetMonthBalaceByDept(deptId);
        }

        /// <summary>
        /// 当月是否已经月结果
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <returns>是否已经月结果</returns>
        public override bool IsMonthAccount(int deptId)
        {
            bool flag = false;
            DW_BalanceRecord record = NewDao<Dao.IDWDao>().GetMaxBlanceRecord(deptId);

            if (record != null)
            {
                if (record.BalanceYear == System.DateTime.Now.Year && record.BalanceMonth == System.DateTime.Now.Month)
                {
                    flag = true;
                }
            }

            return flag;
        }

        /// <summary>
        /// 药剂科室月结
        /// </summary>
        /// <param name="empID">月结操作人员</param>
        /// <param name="deptID">月结科室ID</param>
        /// <param name="workId">机构ID</param>
        /// <returns>处理结果</returns>
        public override DGBillResult MonthAccount(int empID, int deptID, int workId)
        {
            return NewObject<IDWDao>().ExcutMonthBalance(workId, deptID, empID);
        }

        /// <summary>
        /// 系统对账
        /// </summary>
        /// <param name="deptId">药剂科室ID</param>
        /// <param name="workId">机构ID</param>
        /// <returns>错误账目信息表</returns>
        public override DgSpResult SystemCheckAccount(int deptId, int workId)
        {
            return NewObject<IDWDao>().ExcutSystemCheckAccount(workId, deptId);
        }
    }
}
