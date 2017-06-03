using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.BasicData;
using HIS_Entity.DrugManage;
using HIS_Entity.MaterialManage;
using HIS_MaterialManager.Dao;

namespace HIS_MaterialManager.ObjectModel.Account
{
    /// <summary>
    /// 物资月结类
    /// </summary>
   class MaterialBalance: AbstractObjectModel
    {
        /// <summary>
        /// 获取月结日
        /// </summary>
        /// <param name="deptId">月结库房ID</param>
        /// <returns>月结日</returns>
        public int GetAccountDay(int deptId)
        {
            Basic_SystemConfig sc = NewDao<IMWDao>().GetDeptParameters(deptId, "BalanceDay");
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
            NewDao<IMWDao>().SaveParameters(modelList);
        }

        /// <summary>
        /// 机构 部门 月结记录
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <returns>月结记录</returns>
        public DataTable GetMonthBalaceByDept(int deptId)
        {
            return NewDao<IMWDao>().GetMonthBalaceByDept(deptId);
        }

        /// <summary>
        /// 药剂科室月结
        /// </summary>
        /// <param name="empID">月结操作人员</param>
        /// <param name="deptID">月结科室ID</param>
        /// <param name="workId">机构id</param>
        /// <returns>药剂科室月结实体</returns>
        public MWBillResult MonthAccount(int empID, int deptID, int workId)
        {
            return NewObject<IMWDao>().ExcutMonthBalance(workId, deptID, empID);
        }

        /// <summary>
        /// 系统对账
        /// </summary>
        /// <param name="deptId">药剂科室ID</param>
        /// <param name="workId">机构id</param>
        /// <returns>错误账目信息表</returns>
        public MWSpResult SystemCheckAccount(int deptId, int workId)
        {
            return NewObject<IMWDao>().ExcutSystemCheckAccount(workId, deptId);
        }

        /// <summary>
        /// 当月是否已经月结过
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <returns>月结过 true   没有FALSE</returns>
        public  bool IsMonthAccount(int deptId)
        {
            bool flag = false;
            MW_BalanceRecord record = NewDao<IMWDao>().GetMaxBlanceRecord(deptId);
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
