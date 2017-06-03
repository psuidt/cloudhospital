using System.Data;

namespace HIS_MemberManage.Dao
{
    /// <summary>
    /// 转换积分接口
    /// </summary>
    public interface IOPConvertPoints
    {
        /// <summary>
        /// 取得帐户类型信息
        /// </summary>
        /// <returns>帐户类型信息</returns>
        DataTable GetAccountTypeInfo();

        /// <summary>
        /// 检查卡类型
        /// </summary>
        /// <param name="name">卡类型名</param>
        /// <returns>卡类型信息</returns>
        DataTable CheckCardType(string name);

        /// <summary>
        /// 更新使用标识
        /// </summary>
        /// <param name="id">编码</param>
        /// <param name="flag">使用标识</param>
        /// <param name="operateID">操作人id</param>
        /// <returns>1成功</returns>
        int UpdateUseFlag(int id, int flag, int operateID);

        /// <summary>
        /// 取得转换积分信息
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <returns>转换积分数据</returns>
        DataTable GetConvertPointsInfo(int cardTypeID);

        /// <summary>
        ///检查转换积分
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="workID">组织机构id</param>
        /// <returns>转换积分信息</returns>
        DataTable CheckConvertPoints(int cardTypeID, int workID);

        /// <summary>
        /// 检查卡前缀
        /// </summary>
        /// <param name="prsfixDesc">前缀信息</param>
        /// <returns>卡信息</returns>
        DataTable CheckCardPrefix(string prsfixDesc);

        /// <summary>
        /// 更改使用标识
        /// </summary>
        /// <param name="pointsID">积分规则id</param>
        /// <param name="useflag">使用标识</param>
        /// <param name="operateID">操作人id</param>
        /// <returns>1成功</returns>
        int UpdatePointsUseFlag(int pointsID, int useflag, int operateID);
    }
}
