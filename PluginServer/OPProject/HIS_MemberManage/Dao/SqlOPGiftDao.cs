using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MemberManage;

namespace HIS_MemberManage.Dao
{
    /// <summary>
    /// 礼品Dao
    /// </summary>
    public class SqlOPGiftDao : AbstractDao, IOPGiftDao
    {
        /// <summary>
        /// 获取指定机构的可用的帐户类型
        /// </summary>
        /// <param name="workID">机构id</param>
        /// <returns>帐户类型</returns>
        public DataTable GetCardTypeForWork(int workID)
        {
            string sql = @" select CardTypeID,CardTypeName from V_ME_CardTypeList where useflag=1 and workID=" + workID;
            DataTable dt= oleDb.GetDataTable(sql);

            DataRow dr = dt.NewRow();
            dr["CardTypeID"]=0;
            dr["CardTypeName"] = "全部";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();
            return dt;
        }

        /// <summary>
        /// 获取礼品数据
        /// </summary>
        /// <param name="workID">机构id</param>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="useFlag">启用标识</param>
        /// <returns>礼品信息</returns>
        public DataTable GetGiftInfo(int workID, int cardTypeID,int useFlag)
        {
            string sql = " select * from V_ME_Gift where   workID="+ workID;
            if (cardTypeID!=0)
            {
                sql = sql + " and CardTypeID=" + cardTypeID;
            }

            if (useFlag == 1)
            {
                sql = sql + " and useflag="+useFlag;
            }

            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 保存礼品信息
        /// </summary>
        /// <param name="giftEntity">礼品信息</param>
        /// <returns>1成功</returns>
        public int SaveGiftInfo(ME_Gift giftEntity)
        {
            this.BindDb(giftEntity);
            return giftEntity.save();
        }

        /// <summary>
        /// 检查礼品名称重复
        /// </summary>
        /// <param name="workID">机构id</param>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="giftName">礼品名称</param>
        /// <returns>礼品数据</returns>
        public DataTable CheckGiftName(int workID,int cardTypeID, string giftName)
        {
            string sql = @" select * from ME_Gift where workID="+ workID+ " and GiftName='"+ giftName+ "' and CardTypeID="+ cardTypeID;
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 更新礼品启用标识
        /// </summary>
        /// <param name="giftID">礼品id</param>
        /// <param name="useFlag">启用标识</param>
        /// <param name="operateID">操作人id</param>
        /// <returns>1成功</returns>
        public int UpdateGiftFlag(int giftID, int useFlag, int operateID)
        {
            string sql = @" update ME_Gift set useflag=" + useFlag + ",OperateID=" + operateID+ ",OperateDate='"+DateTime.Now+"' where giftID=" + giftID;
            return oleDb.DoCommand(sql);
        }

        /// <summary>
        /// 获取已兑换礼品列表
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="accountCode">帐户编码</param>
        /// <returns>兑换礼品</returns>
        public DataTable GetPointsExchange(int cardTypeID, string accountCode)
        {
            string sql = @" select * from  V_ME_PointsExchange where  CardTypeID="+ cardTypeID;
            if (string.IsNullOrEmpty(accountCode) ==false)
            {
                sql=sql+" and CardNO = '"+ accountCode+"'";
            }

            sql = sql + " order by OperateDate desc";       
            return oleDb.GetDataTable(sql);
        }               
    }
}
