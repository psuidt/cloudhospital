using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_MemberManage.Dao;

namespace HIS_MemberManage.ObjectModel
{
    /// <summary>
    /// 优惠管理
    /// </summary>
    public class DiscountListManagement : AbstractObjectModel
    {
        /// <summary>
        /// 取得优惠信息
        /// </summary>
        /// <param name="stDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="workID">机构id</param>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="cardNO">卡号</param>
        /// <returns>优惠数据</returns>
        public DataTable GetDiscountListInfo(string stDate, string endDate, int workID, int cardTypeID, string cardNO)
        {
            return NewDao<IOPDiscountListDao>().GetDiscountListInfo(stDate, endDate, workID, cardTypeID, cardNO);
        }
    }
}
