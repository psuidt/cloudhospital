using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManger.Dao;
using HIS_Entity.BasicData;
using HIS_Entity.DrugManage;

namespace HIS_DrugManger.ObjectModel.Account
{
    /// <summary>
    /// 药品月结处理器
    /// </summary>
    abstract class DrugBalance : AbstractObjectModel
    {
        /// <summary>
        /// 获取月结日
        /// </summary>
        /// <param name="deptId">月结库房ID</param>
        /// <returns>月结日</returns>
        public int GetAccountDay(int deptId)
        {
            Basic_SystemConfig sc = NewDao<IDGDao>().GetDeptParameters(deptId, "BalanceDay");
            if (sc != null && sc.Value != null)
            {
                int result;
                int.TryParse(sc.Value, out result);
                return result;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 设置月结日
        /// </summary>
        /// <param name="accountDay">新设置的月结日</param>
        /// <param name="deptId">部门ID</param>
        public void SetAccountDay(int accountDay, int deptId)
        {
            List<Basic_SystemConfig> modelList = new List<Basic_SystemConfig>();
            modelList.Add(new Basic_SystemConfig
            {
                DataType = 0,
                DeptID = deptId,
                ParaID = "BalanceDay",
                ParaName = "默认月结时间",
                Value = accountDay.ToString(),
                Memo = "默认月结时间(每月多少号)",
                SystemType = 3,
                Prompt = string.Empty
            });
            NewDao<IDGDao>().SaveDrugParameters(modelList);
        }

        /// <summary>
        /// 获取上次月结日期
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <returns>上次月结日期</returns>
        public abstract DataTable GetMonthBalaceByDept(int deptId);

        /// <summary>
        /// 药剂科室月结
        /// </summary>
        /// <param name="empID">月结操作人员</param>
        /// <param name="deptID">月结科室ID</param>
        /// <param name="workId">机构ID</param>
        /// <returns>处理结果</returns>
        public abstract DGBillResult MonthAccount(int empID, int deptID, int workId);

        /// <summary>
        /// 取消月结
        /// </summary>
        /// <param name="empID">月结操作员</param>
        /// <param name="deptID">月结药剂科室ID</param>
        public abstract void CancelMonthAccount(int empID, int deptID);

        /// <summary>
        /// 系统对账
        /// </summary>
        /// <param name="deptId">药剂科室ID</param>
        /// <param name="workId">机构ID</param>
        /// <returns>错误账目信息表</returns>
        public abstract DgSpResult SystemCheckAccount(int deptId, int workId);

        /// <summary>
        /// 是否是系统月结
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <returns>返回结果</returns>
        public abstract bool IsMonthAccount(int deptId);
    }
}
