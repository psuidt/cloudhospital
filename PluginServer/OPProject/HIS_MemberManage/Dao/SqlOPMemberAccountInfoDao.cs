using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MemberManage;

namespace HIS_MemberManage.Dao
{
    /// <summary>
    /// 会员帐户Dao
    /// </summary>
    public class SqlOPMemberAccountInfoDao : AbstractDao, IOPMemberAccountDao
    {
        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="memberID">会员id</param>
        /// <returns>会员信息</returns>
        public DataTable GetMemberAccountInfo(int memberID)
        {
            string sql = @" select * from V_ME_MemberAccount where MemberID="+ memberID;
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 更新帐户信息
        /// </summary>
        /// <param name="accountEntity">庄户信息</param>
        /// <param name="accountID">帐户id</param>
        /// <returns>1成功</returns>
        public int UpdateCardNO(ME_MemberAccount accountEntity, int accountID)
        {
            int cardTypeID = accountEntity.CardTypeID;
            string cardNO = accountEntity.CardNO;

            string sql = @" update ME_MemberAccount set CardTypeID=" + cardTypeID+ ", CardNO='"+ cardNO.Trim()+ "' where AccountID="+ accountID;
            return oleDb.DoCommand(sql);
        }

        /// <summary>
        /// 更换帐户卡号
        /// </summary>
        /// <param name="accountID">帐户id</param>
        /// <param name="newCardNO">新卡号</param>
        /// <param name="opeateID">操作人id</param>
        /// <returns>1成功</returns>
        public int UpdateAccountInfo(int accountID, string newCardNO,int opeateID)
        {
            string sql = @" update ME_MemberAccount set CardNO='" + newCardNO.Trim() + "',OperateDate='" + System.DateTime.Now + "',operateid=" + opeateID + " where AccountID=" + accountID;
            return oleDb.DoCommand(sql);
        }

        /// <summary>
        /// 更新帐户启用标识
        /// </summary>
        /// <param name="accountID">帐户id</param>
        /// <param name="useFlag">启用标识</param>
        /// <param name="opeateID">操作人id</param>
        /// <returns>1成功</returns>
        public int UpdateAccountUseFlag(int accountID, int useFlag,int opeateID)
        {
            string sql = @" update ME_MemberAccount set  useflag =" + useFlag+ ",OperateDate='" + System.DateTime.Now+"',operateid="+ opeateID + " where AccountID=" + accountID;
            return oleDb.DoCommand(sql);
        }

        /// <summary>
        /// 新增帐户时验证该会员是否有同类型的有效帐户
        /// </summary>
        /// <param name="memberID">会员id</param>
        /// <param name="cardTypeID">卡类型id</param>
        /// <returns>帐户类型数据</returns>
        public DataTable CheckCardType(int memberID,int cardTypeID)
        {
            string sql=@" select * from ME_MemberAccount where useflag=1 and CardTypeID=" + cardTypeID + " and memberID=" + memberID;
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 指定帐户积分归零
        /// </summary>
        /// <param name="accountID">帐户id</param>
        /// <returns>1成功</returns>
        public int ClearAccountScore(int accountID)
        {
            string sql = @" update ME_MemberAccount set  score =0 where AccountID=" + accountID;
            return oleDb.DoCommand(sql);
        }

        /// <summary>
        /// 指定机构所有帐户积分归零
        /// </summary>
        /// <param name="workID">机构id</param>
        /// <param name="operateID">操作人id</param>
        /// <returns>1成功</returns>
        public int ClearAllAccountScore(int workID, int operateID)
        {
            string sql = @" update ME_MemberAccount set  score =0,OperateDate='"+DateTime.Now.ToString()+ "',OperateID="+ operateID + " where workid=" + workID;
            return oleDb.DoCommand(sql);
        }

        /// <summary>
        /// 获取帐户类型积分转换规则
        /// </summary>
        /// <param name="workID">机构id</param>
        /// <param name="cardType">卡类型</param>
        /// <returns>积分转换规则数据</returns>
        public DataTable GetConvertPoints(int workID, int cardType)
        {
            string sql = @" select * from ME_ConvertPoints where useflag=1 and CardTypeID="+cardType+ " and WorkID="+workID;
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 更新帐户积分
        /// </summary>
        /// <param name="accountID">帐户id</param>
        /// <param name="score">积分</param>
        /// <param name="operateDate">操作日期</param>
        /// <param name="operateID">操作人id</param>
        /// <returns>1成功</returns>
        public int UpdateAccountScore(int accountID, int score,DateTime operateDate,int operateID)
        {
            string sql = @" update ME_MemberAccount set score=  "+ score+ ",OperateDate='"+ operateDate.ToString() 
                        + "',OperateID="+ operateID + "  where AccountID=" + accountID;
            return oleDb.DoCommand(sql);
        }

        /// <summary>
        /// 查询账户积分
        /// </summary>
        /// <param name="accountID">帐户id</param>
        /// <param name="stDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="flag">标识</param>
        /// <returns>帐户积分数据</returns>
        public DataTable QueryAccountScoreList(int accountID, string stDate, string endDate, int flag)
        {
            string sql = @" select * from V_ME_ScoreList where AccountID=" + accountID + " and OperateDate>='" + stDate + "' and OperateDate<='"
                         + endDate + "'";
            if (flag!=0)
            {
                sql = sql + " and Type='" + flag+"'";
            }

            sql=sql+ " order by  OperateDate";
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 插入所有积分清零的明细记录
        /// </summary>
        /// <param name="workID">机构ID</param>
        /// <param name="operateId">操作员ID</param>
        /// <returns>1成功</returns>
        public int InsertALLScoreList(int workID, int operateId)
        {
            string sql = @" insert into ME_ScoreList (AccountID,ScoreType,DocumentNo,Score,OperateDate,OperateID,WorkID)"
                        + " (select AccountID,4 as ScoreType,'' as DocumentNo,-Score, getdate()," + operateId + "," + workID
                        + " from ME_MemberAccount where WorkID=" + workID+ " and Score>0) ";
            return oleDb.DoCommand(sql);
        }

        /// <summary>
        /// 获取换卡历史列表
        /// </summary>
        /// <param name="accountID">帐户ID</param>
        /// <returns>换卡历史数据</returns>
        public DataTable GetChangeCardList(int accountID)
        {
            string sql = @"select ChangeID,AccountID,OldCardNO,NewCardNO,Amount,CONVERT(varchar(100), OperateDate, 23) as OperateDate,OperateID,WorkID 
                        from ME_ChangeCardList where AccountID=" + accountID+ " order by OperateDate desc";
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 系统参数
        /// </summary>
        /// <param name="workID">机构id</param>
        /// <returns>系统参数数据</returns>
        public DataTable GetSystemConfig(int workID)
        {
            string sql = @"select * from Basic_SystemConfig where ParaID='ChangeCard' and workid=" + workID;
            return oleDb.GetDataTable(sql);
        } 
    }
}
