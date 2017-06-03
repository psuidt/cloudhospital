using System.Data;

namespace HIS_MemberManage.Dao
{
    /// <summary>
    /// 优惠方案Dao
    /// </summary>
    public interface IOPDiscountListDao
    {
        /// <summary>
        /// 取得优惠列表数据
        /// </summary>
        /// <param name="stDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="workID">组织机构id</param>
        /// <param name="cardTypeID">卡类型ID</param>
        /// <param name="cardNO">卡号</param>
        /// <returns>优惠列表数据</returns>
        DataTable GetDiscountListInfo(string stDate, string endDate, int workID, int cardTypeID, string cardNO);     
    }
}
