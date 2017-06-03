using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.OPManage;
using HIS_IPManage.Dao;

namespace HIS_IPManage.ObjectModel
{
    /// <summary>
    /// 交款处理
    /// </summary>
    public class AccountManagement : AbstractObjectModel
    {
        /// <summary>
        /// 查询所有已经缴款记录
        /// </summary>
        /// <param name="bdate">开始时间</param>
        /// <param name="edate">结束时间</param>
        /// <param name="empid">操作员ID</param>
        /// <param name="status">状态</param>
        /// <returns>已经缴款记录</returns>
        public DataTable QueryAllAccount(DateTime bdate, DateTime edate, int empid, int status)
        {
            DataTable dtAllAccount = NewDao<IAllAccountDao>().GetAllAccountData(bdate, edate, empid, status);
            if (dtAllAccount != null && dtAllAccount.Rows.Count > 0)
            {
                DataTable dtPay = NewDao<IAllAccountDao>().GetAllAccountPayment(bdate, edate, empid);
                List<OP_AccountPatMentInfo> payInfoList = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<OP_AccountPatMentInfo>(dtPay);
                for (int i = 0; i < dtAllAccount.Rows.Count; i++)
                {
                    int accountid = Convert.ToInt32(dtAllAccount.Rows[i]["accountid"]);
                    List<OP_AccountPatMentInfo> payInfos = payInfoList.Where(p => p.AccountID == accountid).ToList();
                    foreach (OP_AccountPatMentInfo payInfo in payInfos)
                    {
                        if (dtAllAccount.Columns.Contains(payInfo.PayMentName))
                        {
                            dtAllAccount.Rows[i][payInfo.PayMentName] = payInfo.PayMentMoney;
                        }
                        else
                        {
                            DataColumn col = new DataColumn();
                            col.ColumnName = payInfo.PayMentName;
                            col.DataType = typeof(decimal);
                            dtAllAccount.Columns.Add(col);
                            dtAllAccount.Rows[i][payInfo.PayMentName] = payInfo.PayMentMoney;
                        }
                    }
                }
            }

            return dtAllAccount;
        }

        /// <summary>
        /// 取得所有未缴款数据
        /// </summary>
        /// <param name="bdate">开始时间</param>
        /// <param name="edate">结束时间</param>
        /// <param name="empid">操作员ID</param>
        /// <returns>未缴款记录</returns>
        public DataTable QueryAllNotAccount(DateTime bdate, DateTime edate, int empid)
        {
            DataTable dtAllNotAccount = NewDao<IAllAccountDao>().GetAllNotAccountData(bdate, edate, empid);            

            return dtAllNotAccount;
        }
    }
}
