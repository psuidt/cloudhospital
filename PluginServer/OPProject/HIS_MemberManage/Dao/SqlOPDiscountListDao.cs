using System.Data;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_MemberManage.Dao
{
    /// <summary>
    /// 优惠明细Dao
    /// </summary>
    public class SqlOPDiscountListDao : AbstractDao, IOPDiscountListDao
    {
        /// <summary>
        /// 取得优惠明细信息
        /// </summary>
        /// <param name="stDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="workID">组织机构id</param>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="cardNO">卡号</param>
        /// <returns>优惠明细数据</returns>
        public DataTable GetDiscountListInfo(string stDate, string endDate, int workID, int cardTypeID, string cardNO)
        {
            string sql = " select * from V_ME_DiscountList where (OperateDate>='" + stDate + "' and OperateDate<='" + endDate + " 23:59:59')";

            if (workID > 0)
            {
                sql = sql + " and workID="+ workID;
            }

            if (cardTypeID>0)
            {
                sql = sql + " and CardTypeID=" + cardTypeID;
            }

            if (string.IsNullOrEmpty(cardNO)==false)
            {
                sql = sql + " and CardNO='" + cardNO + "'";
            }

            return oleDb.GetDataTable(sql);
        }
    }
}
