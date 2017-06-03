using System.Data;
using HIS_Entity.MemberManage;

namespace HIS_MemberManage.Dao
{
    /// <summary>
    /// 礼品Dao
    /// </summary>
    public interface IOPGiftDao
    {
        /// <summary>
        /// 获取卡类型
        /// </summary>
        /// <param name="workID">机构Id</param>
        /// <returns>卡类型信息</returns>
        DataTable GetCardTypeForWork(int workID);

        /// <summary>
        /// 礼品信息
        /// </summary>
        /// <param name="workID">组织机构id</param>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="useFlag">使用标识</param>
        /// <returns>礼品信息数据</returns>
        DataTable GetGiftInfo(int workID, int cardTypeID,int useFlag);

        /// <summary>
        /// 保存礼品信息
        /// </summary>
        /// <param name="giftEntity">礼品实体</param>
        /// <returns>1成功</returns>
        int SaveGiftInfo(ME_Gift giftEntity);

        /// <summary>
        /// 检查礼品名称是否重复
        /// </summary>
        /// <param name="workID">组织机构id</param>
        /// <param name="cardTypeID">卡类型ID</param>
        /// <param name="giftName">礼品名称</param>
        /// <returns>礼品信息</returns>
        DataTable CheckGiftName(int workID, int cardTypeID,string giftName);

        /// <summary>
        /// 更噶礼品启用标识
        /// </summary>
        /// <param name="giftID">礼品id</param>
        /// <param name="useFlag">使用标识</param>
        /// <param name="operateID">操作人id</param>
        /// <returns>1成功</returns>
        int UpdateGiftFlag(int giftID, int useFlag, int operateID);

        /// <summary>
        /// 获取积分转换规则
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="accountCode">卡号</param>
        /// <returns>积分转换规则</returns>
        DataTable GetPointsExchange(int cardTypeID,string accountCode);
    }
}
