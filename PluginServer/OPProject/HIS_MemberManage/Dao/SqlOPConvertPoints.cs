using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_MemberManage.Dao
{
    /// <summary>
    /// 积分转换Dao
    /// </summary>
    public class SqlOPConvertPoints : AbstractDao, IOPConvertPoints
    {
        /// <summary>
        /// 返回帐户类型信息
        /// </summary>
        /// <returns>帐户类型信息</returns>
        public DataTable GetAccountTypeInfo()
        {
            string sql = @" select * from V_ME_CardTypeList";
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 根椐帐户类型名称获取帐户类型数氢
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>卡类型信息</returns>
        public DataTable CheckCardType(string name)
        {
            string sql = @" select * from V_ME_CardTypeList where CardTypeName='" + name + "'";
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 更新帐户类型使用状态
        /// </summary>
        /// <param name="id">帐户类型ID</param>
        /// <param name="flag">状态标志</param>
        /// <param name="operateID">操作员ID</param>
        /// <returns>1成功</returns>
        public int UpdateUseFlag(int id, int flag, int operateID)
        {
            string sql = @" update ME_CardTypeList set  OperateDate ='" + DateTime.Now.ToString() + "', OperateID=" + operateID + ", UseFlag=" + flag + " where CardTypeID=" + id;
            return oleDb.DoCommand(sql);
        }

        /// <summary>
        /// 取得转换积分信息
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <returns>积分规则信息</returns>
        public DataTable GetConvertPointsInfo(int cardTypeID)
        {
            string sql = @" select * from V_ME_ConvertPoints where CardTypeID=" + cardTypeID;
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 检查积分转换规则
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="workID">组织机构id</param>
        /// <returns>积分转换规则</returns>
        public DataTable CheckConvertPoints(int cardTypeID, int workID)
        {
            string sql = @" select * from V_ME_ConvertPoints where CardTypeID=" + cardTypeID + " AND UseWork=" + workID;
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 检查卡前缀
        /// </summary>
        /// <param name="prsfixDesc">前缀描述</param>
        /// <returns>卡类型数据</returns>
        public DataTable CheckCardPrefix(string prsfixDesc)
        {
            string sql = @" select * from ME_CardTypeList where CardPrefix='"+ prsfixDesc+"'";
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 更改积分转换规则启用标识
        /// </summary>
        /// <param name="pointsID">积分设置id</param>
        /// <param name="useflag">启用标识</param>
        /// <param name="operateID">操作人id</param>
        /// <returns>1成功</returns>
        public int UpdatePointsUseFlag(int pointsID, int useflag, int operateID)
        {
            string sql = @" update ME_ConvertPoints set useflag=" + useflag + ",OperateDate='" + DateTime.Now + "',OperateID=" + operateID + " where ConvertID=" + pointsID;
            return oleDb.DoCommand(sql);
        }
    }
}
