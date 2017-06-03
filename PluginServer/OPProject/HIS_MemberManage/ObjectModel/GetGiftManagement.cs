using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MemberManage;
using HIS_MemberManage.Dao;
using HIS_PublicManage.Dao;

namespace HIS_MemberManage.ObjectModel
{
    /// <summary>
    /// 礼品管理
    /// </summary>
    public class GetGiftManagement: AbstractObjectModel
    {
        /// <summary>
        /// 获取礼品兑换列表
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="accountCode">帐户编码</param>
        /// <returns>礼品兑换列表</returns>
        public DataTable GetPointsExchange(int cardTypeID,string accountCode)
        {
            return NewDao<IOPGiftDao>().GetPointsExchange(cardTypeID, accountCode);
        }

        /// <summary>
        /// 保存礼品兑换信息
        /// </summary>
        /// <param name="accountID">帐户id</param>
        /// <param name="accountScore">帐户积分</param>
        /// <param name="exchangeEntity">交换信息</param>
        /// <param name="scoreListEntity">积分信息</param>
        /// <returns>1成功</returns>
        public int SaveExchange(int accountID,int accountScore, ME_PointsExchange exchangeEntity, ME_ScoreList scoreListEntity)
        {
            string code = string.Empty;
            NewObject<MemberAccountManagement>().SaveAddScoreInfo(scoreListEntity, accountScore,3,0,out code);
            this.BindDb(exchangeEntity);
            return exchangeEntity.save();
        }

        /// <summary>
        /// 检查启用标识
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="accountCode">帐户编码</param>
        /// <returns>true存在</returns>
        public bool CheckFlag(int cardTypeID, string accountCode)
        {
            DataTable dt= NewDao<IMemberInfoDao>().QueryMemberInfoForCardType(cardTypeID, accountCode);
             
            if (dt.Rows.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}