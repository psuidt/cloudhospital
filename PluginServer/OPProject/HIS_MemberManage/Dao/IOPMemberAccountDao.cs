using System;
using System.Data;
using HIS_Entity.MemberManage;

namespace HIS_MemberManage.Dao
{
    /// <summary>
    /// 会员帐户DAO
    /// </summary>
    public interface IOPMemberAccountDao
    {
        /// <summary>
        /// 获取会员帐户信息
        /// </summary>
        /// <param name="memberID">会员id</param>
        /// <returns>会员帐户信息</returns>
        DataTable GetMemberAccountInfo(int memberID);

        /// <summary>
        /// 更新卡号
        /// </summary>
        /// <param name="accountEntity">帐户实体</param>
        /// <param name="accountID">帐户id</param>
        /// <returns>1成功</returns>
        int UpdateCardNO(ME_MemberAccount accountEntity,int accountID);

        /// <summary>
        /// 更改帐户使用标识
        /// </summary>
        /// <param name="accountID">帐户id</param>
        /// <param name="useFlag">使用标识</param>
        /// <param name="opeateID">操作人id</param>
        /// <returns>1成功</returns>
        int UpdateAccountUseFlag(int accountID, int useFlag,int opeateID);

        /// <summary>
        /// 校验卡类型
        /// </summary>
        /// <param name="memberID">会员id</param>
        /// <param name="cardTypeID">卡类型id</param>
        /// <returns>卡类型信息</returns>
        DataTable CheckCardType(int memberID, int cardTypeID);

        /// <summary>
        /// 清除指定帐户积分
        /// </summary>
        /// <param name="accountID">帐户id</param>
        /// <returns>1成功</returns>
        int ClearAccountScore(int accountID);

        /// <summary>
        /// 所有帐户积分清零
        /// </summary>
        /// <param name="workID">组织机构id</param>
        /// <param name="operateID">操作人id</param>
        /// <returns>1成功</returns>
        int ClearAllAccountScore(int workID,int operateID);

        /// <summary>
        /// 获取转换积分规则
        /// </summary>
        /// <param name="workID">组织机构id</param>
        /// <param name="cardType">卡类型</param>
        /// <returns>积分规则信息</returns>
        DataTable GetConvertPoints(int workID, int cardType);

        /// <summary>
        /// 更新帐户表积分
        /// </summary>
        /// <param name="accountID">帐户id</param>
        /// <param name="score">分数</param>
        /// <param name="operateDate">操作日期</param>
        /// <param name="operateID">操作id</param>
        /// <returns>1成功</returns>
        int UpdateAccountScore(int accountID, int score,DateTime operateDate,int operateID);

        /// <summary>
        /// 查询账户积分列表
        /// </summary>
        /// <param name="accountID">帐户id</param>
        /// <param name="stDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="flag">标识</param>
        /// <returns>帐户积分列表数据</returns>
        DataTable QueryAccountScoreList(int accountID, string stDate, string endDate, int flag);

        /// <summary>
        /// 插入所有积分
        /// </summary>
        /// <param name="workID">组织机构id</param>
        /// <param name="operateId">操作人id</param>
        /// <returns>1成功</returns>
        int InsertALLScoreList(int workID, int operateId);

        /// <summary>
        /// 获得换卡列表
        /// </summary>
        /// <param name="accountID">帐户id</param>
        /// <returns>换卡数据</returns>
        DataTable GetChangeCardList(int accountID);

        /// <summary>
        /// 更细帐户信息
        /// </summary>
        /// <param name="accountID">帐户id</param>
        /// <param name="newCardNO">新卡号</param>
        /// <param name="opeateID">操作人id</param>
        /// <returns>1成功</returns>
        int UpdateAccountInfo(int accountID, string newCardNO, int opeateID);

        /// <summary>
        /// 获取系统参数
        /// </summary>
        /// <param name="workID">组织机构ID</param>
        /// <returns>参数数据</returns>
        DataTable GetSystemConfig(int workID);
    }
}
