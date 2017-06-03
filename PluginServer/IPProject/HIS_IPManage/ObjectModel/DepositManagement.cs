using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.IPManage;
using HIS_IPManage.Dao;

namespace HIS_IPManage.ObjectModel
{
    /// <summary>
    /// 预交金管理
    /// </summary>
    public class DepositManagement : AbstractObjectModel
    {
        /// <summary>
        /// 缴纳预交金
        /// </summary>
        /// <param name="depositList">预交金</param>
        /// <returns>错误消息</returns>
        public string PayADeposit(IP_DepositList depositList)
        {
            DataTable dt = NewDao<IIPManageDao>().GetPatStatus(depositList.PatListID);
            if (dt.Rows.Count > 0)
            {
                this.BindDb(depositList);
                depositList.save();
                return string.Empty;
            }
            else
            {
                return "该病人不存在或已取消入院，请确认！";
            }
        }

        /// <summary>
        /// 取得病人预交金列表
        /// </summary>
        /// <param name="patListID">病人入院登记ID</param>
        /// <param name="serialNumber">病人住院流水号</param>
        /// <returns>预交金列表</returns>
        public DataTable GetPaymentList(int patListID, decimal serialNumber)
        {
            return NewDao<IIPManageDao>().GetPaymentList(patListID, serialNumber, false);
        }

        /// <summary>
        /// 预交金退费
        /// </summary>
        /// <param name="depositID">预交金ID</param>
        /// <returns>错误消息</returns>
        public string Refund(int depositID)
        {
            DataTable dt = NewDao<IIPManageDao>().GetCostHeadInfo(depositID);
            if (dt.Rows.Count <= 0)
            {
                return "此条记录已经被退费或结算，不能操作！";
            }

            NewDao<IIPManageDao>().Refund(depositID);
            NewDao<IIPManageDao>().RefundInsert(depositID);

            return string.Empty;
        }
    }
}
