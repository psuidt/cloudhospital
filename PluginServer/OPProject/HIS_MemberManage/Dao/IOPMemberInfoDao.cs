using System.Data;
using EFWCoreLib.CoreFrame.DbProvider.SqlPagination;
using HIS_Entity.MemberManage;

namespace HIS_MemberManage.Dao
{
    /// <summary>
    /// 会员信息Dao
    /// </summary>
    public interface IOPMemberInfoDao
    {
        /// <summary>
        /// 取得会员信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="page">分页信息</param>
        /// <returns>会员信息数据</returns>
        DataTable GetMemberInfo(string condition, PageInfo page);

        /// <summary>
        /// 获取知晓途径信息
        /// </summary>
        /// <returns>知晓途径信息</returns>
        DataTable GetRouteInfo();

        /// <summary>
        /// 检测帐户号码是否有效
        /// </summary>
        /// <param name="accountType">帐户类型</param>
        /// <param name="cardNO">帐户号码</param>
        /// <returns>帐户数据</returns>
        DataTable CheckAccountNO(int accountType, string cardNO);

        /// <summary>
        /// 更新会员状态
        /// </summary>
        /// <param name="memberID">会员id</param>
        /// <param name="useFlag">使用标识</param>
        /// <returns>1成功</returns>
        int UpdataMemberUseFlag(int memberID, int useFlag);

        /// <summary>
        /// 修改门诊病人信息
        /// </summary>
        /// <param name="memberEntity">会员信息</param>
        void UpdateOPPatientInfo(ME_MemberInfo memberEntity);
    }
}
