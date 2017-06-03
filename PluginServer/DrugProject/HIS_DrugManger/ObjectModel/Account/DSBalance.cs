using System;
using System.Data;
using HIS_DrugManger.Dao;
using HIS_Entity.DrugManage;

namespace HIS_DrugManger.ObjectModel.Account
{
    /// <summary>
    /// 药房月结类
    /// </summary>
    class DSBalance : DrugBalance
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
        /// 根据科室ID获取月结信息
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <returns>获取月结信息</returns>
        public override DataTable GetMonthBalaceByDept(int deptId)
        {
            return NewDao<IDSDao>().GetMonthBalaceByDept(deptId);
        }

        /// <summary>
        /// 药剂科室月结
        /// </summary>
        /// <param name="empID">月结操作人员</param>
        /// <param name="deptID">月结科室ID</param>
        /// <param name="workId">机构ID</param>
        /// <returns>返回结果对象</returns>
        public override DGBillResult MonthAccount(int empID, int deptID, int workId)
        {
            return NewObject<IDSDao>().ExcutMonthBalance(workId, deptID, empID);
        }

        /// <summary>
        /// 系统对账
        /// </summary>
        /// <param name="deptId">药剂科室ID</param>
        /// <param name="workId">机构ID</param>
        /// <returns>错误账目信息表</returns>
        public override DgSpResult SystemCheckAccount(int deptId, int workId)
        {
            return NewObject<IDSDao>().ExcutSystemCheckAccount(workId, deptId);
        }

        /// <summary>
        /// 当月是否已经月结过
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <returns>月结过 true   没有FALSE</returns>
        public override bool IsMonthAccount(int deptId)
        {
            bool flag = false;
            DS_BalanceRecord record = NewDao<IDSDao>().GetMaxBlanceRecord(deptId);
            if (record != null)
            {
                if (record.BalanceYear == System.DateTime.Now.Year && record.BalanceMonth == System.DateTime.Now.Month)
                {
                    flag = true;
                }
            }

            return flag;
        }
    }
}
