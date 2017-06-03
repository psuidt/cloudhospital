using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_MemberManage.Dao
{
    /// <summary>
    /// 优惠方案Dao
    /// </summary>
    public class SqlOPPromotionProject: AbstractDao, IOPPromotionProject
    {
        /// <summary>
        /// 获取优惠方案头信息
        /// </summary>
        /// <param name="workID">机构Id</param>
        /// <param name="stDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>优惠方案头信息</returns>
        public DataTable GetPromotionProjectHeadInfo(int workID, string stDate, string endDate)
        {
            string sql = @" select * from V_ME_PromotionProjectHead where 1=1 ";
            if (workID>0)
            {
                sql = sql + " AND workID=" + workID;
            }

            sql = sql + " AND StartDate>='" + stDate + "' AND StartDate<='" + endDate + "' ";
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 获取方案名称
        /// </summary>
        /// <param name="name">方案名称</param>
        /// <returns>方案记录</returns>
        public DataTable ChecPromName(string name)
        {
            string sql = @" select * from V_ME_PromotionProjectHead where PromName='" + name + "'";
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 删除头信息
        /// </summary>
        /// <param name="promID">方案id</param>
        /// <returns>1成功</returns>
        public int DeleteHead(int promID)
        {
            string sql = @" DELETE FROM ME_PromotionProjectHead where PromID=" + promID;
            return oleDb.DoCommand(sql);
        }

        /// <summary>
        /// 删除明细
        /// </summary>
        /// <param name="promID">方案id</param>
        /// <returns>1成功</returns>
        public int DeleteDetail(int promID)
        {
            string sql = @" DELETE FROM ME_PromotionProjectDetail where PromID=" + promID;
            return oleDb.DoCommand(sql);
        }

        /// <summary>
        /// 更改头标识
        /// </summary>
        /// <param name="promID">方案id</param>
        /// <param name="flag">启用标识</param>
        /// <param name="operateID">操作人id</param>
        /// <returns>1成功</returns>
        public int UpdateHeadUseFlag(int promID, int flag,int operateID)
        {
            string sql = @" update ME_PromotionProjectHead set UseFlag ="+ flag+ ",OperateDate='"+DateTime.Now.ToString()+"',"
                     + "OperateID="+ operateID+ " WHERE PromID="+ promID;
            return oleDb.DoCommand(sql);
        }

        /// <summary>
        /// 检查方案开始日期
        /// </summary>
        /// <param name="stDate">开始日期</param>
        /// <param name="workID">机构id</param>
        /// <returns>方案信息</returns>
        public DataTable CheckPromStartDate(string stDate,int workID)
        {
            string sql = @" select * from ME_PromotionProjectHead where useflag=1 and workID="+ workID + " AND EndDate>'"+ stDate+"'";
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 检查总额优惠方案
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="patTypeID">病人类型id</param>
        /// <param name="costTypeID">费用类型</param>
        /// <param name="promTypeID">优惠类型id</param>
        /// <param name="promID">方案id</param>
        /// <param name="promSunID">方案明细id</param>
        /// <returns>总额优惠方案数据</returns>
        public DataTable CheckDetailForAmount(int cardTypeID,int patTypeID,int costTypeID,int promTypeID, int promID, int promSunID)
        {
            string sql = @" select * from ME_PromotionProjectDetail where CardTypeID="+ cardTypeID+ " AND PatientType="+ patTypeID
                              + " AND CostType="+ costTypeID+ " AND PromTypeID=" + promTypeID+ " AND PromID=" + promID;
           if (promSunID>0)
            {
                sql = sql + @" and PromSunID="+ promSunID;
            }

            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 检查类型优惠方案
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="patTypeID">病人类型id</param>
        /// <param name="costTypeID">费用类型</param>
        /// <param name="promTypeID">优惠类型id</param>
        /// <param name="classID">类型id</param>
        /// <param name="promID">方案id</param>
        /// <param name="promSunID">方案明细id</param>
        /// <returns>类型优惠方案记录</returns>
        public DataTable CheckDetailForClass(int cardTypeID, int patTypeID, int costTypeID, int promTypeID, int classID, int promID, int promSunID)
        {
            string sql = @" select * from ME_PromotionProjectDetail where CardTypeID=" + cardTypeID + " AND PatientType=" + patTypeID + " AND CostType=" + costTypeID + " AND PromTypeID=" + promTypeID
                        + " and PromClass=" + classID + " AND PromID=" + promID;
            if (promSunID > 0)
            {
                sql = sql + @" and PromSunID=" + promSunID;
            }

            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 检查项目优惠方案
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="patTypeID">病人类型id</param>
        /// <param name="costTypeID">费用类型</param>
        /// <param name="promTypeID">优惠类型id</param>
        /// <param name="itemID">项目id</param>
        /// <param name="promID">方案id</param>
        /// <param name="promSunID">方案明细id</param>
        /// <returns>项目优惠方案记录</returns>
        public DataTable CheckDetailForItem(int cardTypeID, int patTypeID, int costTypeID, int promTypeID, int itemID, int promID, int promSunID)
        {
            string sql = @" select * from ME_PromotionProjectDetail where CardTypeID=" + cardTypeID + " AND PatientType=" + patTypeID + " AND CostType=" + costTypeID + " AND PromTypeID=" + promTypeID
                          + " and PromPro=" + itemID+  " AND PromID=" + promID;
            if (promSunID > 0)
            {
                sql = sql + @" and PromSunID=" + promSunID;
            }

            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 获取方案明细
        /// </summary>
        /// <param name="promID">方案id</param>
        /// <returns>方案明细</returns>
        public DataTable GetPromotionProjectDetail(int promID)
        {
            string sql = @" select * from V_ME_PromotionProjectDetail where PromID="+ promID+ " order by PromSunID";
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 更改明细方案启用标识
        /// </summary>
        /// <param name="promSunID">明细方案id</param>
        /// <param name="flag">标识</param>
        /// <returns>1成功</returns>
        public int UpdateDetailFlag(int promSunID, int flag)
        {
            string sql = @" update  ME_PromotionProjectDetail set useflag="+flag+ " where PromSunID=" + promSunID ;
            return oleDb.DoCommand(sql);
        }

        /// <summary>
        /// 获取优惠方案
        /// </summary>
        /// <param name="promID">方案id</param>
        /// <returns>优惠方案</returns>
        public DataTable GetPromotionProject(int promID)
        {
            string sql = @" select * from ME_PromotionProjectHead where PromID="+ promID;
            return oleDb.GetDataTable(sql);
        }
    }
}
