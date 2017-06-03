using System.Data;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.DbProvider.SqlPagination;
using HIS_Entity.BasicData;
using HIS_Entity.MemberManage;
using HIS_PublicManage.ObjectModel;

namespace HIS_MemberManage.Dao
{
    /// <summary>
    /// 会员信息Dao
    /// </summary>
    public class SqlOPMemberInfoDao:AbstractDao, IOPMemberInfoDao
    {
        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="page">分页信息</param>
        /// <returns>会员信息</returns>
        public DataTable GetMemberInfo(string condition, PageInfo page)
        {
            string sql = @" select * from V_ME_MemberInfo where 1=1 " + condition+ " order by OpenDate desc";
            return oleDb.GetDataTable(SqlPage.FormatSql(sql, page, oleDb));
        }

        /// <summary>
        /// 获取会员途径
        /// </summary>
        /// <returns>会员途径</returns>
        public DataTable GetRouteInfo()
        {
            string sql = @" select * from me_route";
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 检测该帐户类型的卡号是否正在使用中
        /// </summary>
        /// <param name="accountType">帐户类型</param>
        /// <param name="cardNO">号码</param>
        /// <returns>帐户类型数据</returns>
        public DataTable CheckAccountNO(int accountType, string cardNO)
        {
            string sql = @" select accountID, CardNO from ME_MemberAccount where UseFlag=1 and CardTypeID="+ accountType.ToString()
                        + " and CardNO='"+ cardNO.ToString()+"'";
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 更改会员状态
        /// </summary>
        /// <param name="memberID">会员id</param>
        /// <param name="useFlag">启用标识</param>
        /// <returns>返回1成功</returns>
        public int UpdataMemberUseFlag(int memberID,int useFlag)
        {
            string sql = @" update ME_MemberInfo set UseFlag="+ useFlag+ " where MemberID="+ memberID;
            return oleDb.DoCommand(sql);
        }

        /// <summary>
        /// 修改门诊病人信息
        /// </summary>
        /// <param name="memberEntity">会员信息</param>
        public void UpdateOPPatientInfo(ME_MemberInfo memberEntity)
        {
            string sql = @" UPDATE OP_CostHead SET PatName='{0}',PatTypeID={1} WHERE MemberID={2}";
            sql = string.Format(sql, memberEntity.Name, memberEntity.PatType, memberEntity.MemberID);
            oleDb.DoCommand(sql);
            sql = @" UPDATE OP_FeeItemHead SET PatName='{0}' WHERE MemberID={1}";
            sql = string.Format(sql, memberEntity.Name, memberEntity.MemberID);
            oleDb.DoCommand(sql);

            sql = @" UPDATE OP_FeeRefundHead SET PatName='{0}' WHERE PatListID IN (SELECT PatListID FROM OP_PatList WHERE MemberID={1})";
            sql = string.Format(sql, memberEntity.Name, memberEntity.MemberID);
            oleDb.DoCommand(sql);
            string sex = string.Empty;
            if (memberEntity.Sex == "1")
            {
                sex = "男";
            }
            else if (memberEntity.Sex == "2")
            {
                sex = "女";
            }

            AgeValue ag = AgeExtend.GetAgeValue(memberEntity.Birthday);
            string age = ag.ReturnAgeStr_EN();
            string patTypeName = string.Empty;
            Basic_PatType model = (Basic_PatType)NewObject<Basic_PatType>().getmodel(memberEntity.PatType);
            patTypeName = model.PatTypeName;
            sql = @" UPDATE OP_PatList SET PatTypeID={0},PatName='{1}',PatSex='{2}',Birthday='{3}',Age='{4}',Allergies='{5}',WorkUnit='{6}',PatTypeName='{7}' WHERE MemberID={8}";
            sql = string.Format(sql, memberEntity.PatType, memberEntity.Name, sex, memberEntity.Birthday.ToString("yyyy-MM-dd HH:mm:ss"), age, memberEntity.Allergies, memberEntity.WorkUnit,patTypeName, memberEntity.MemberID);
            oleDb.DoCommand(sql);
        }
    }
}
