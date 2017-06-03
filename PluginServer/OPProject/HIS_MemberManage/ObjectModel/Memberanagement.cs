using System.Data;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.DbProvider.SqlPagination;
using HIS_Entity.MemberManage;
using HIS_MemberManage.Dao;

namespace HIS_MemberManage.ObjectModel
{
    /// <summary>
    /// 会员管理
    /// </summary>
    public class Memberanagement: AbstractObjectModel
    {
        /// <summary>
        /// 根权参数返回查询条件
        /// </summary>
        /// <param name="workID">注册机构ID</param>
        /// <param name="stDate">注册开始时间</param>
        /// <param name="endDate">注册终止时间</param>
        /// <param name="memberName">会员姓名</param>
        /// <param name="mobile">会员手机号码</param>
        /// <returns>查询条件</returns>
        public string SetCondition(string workID, string stDate, string endDate, string memberName, string mobile)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(stDate) == false)
            { 
                sql = " and OpenDate>='" + stDate + "' and OpenDate<='" + endDate + "'";
            }

            if (workID!="-1")
            {
                sql = sql + " and RegisterWork=" + workID;
            }

            if (string.IsNullOrEmpty(memberName) ==false)
            {
                sql = sql + " and Name like '%" + memberName + "%'";
            }

            if (string.IsNullOrEmpty(mobile) == false)
            {
                sql = sql + " and Mobile = '" + mobile + "'";
            }

            return sql;
        }

        /// <summary>
        /// 查询会员信息
        /// </summary>
        /// <param name="pageNo">页次</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="condition">查询条件</param>
        /// <param name="pageInfo">查询页面信息</param>
        /// <returns>会员信息</returns>
        public DataTable QueryMemberInfo(int pageNo, int pageSize, string condition,out PageInfo pageInfo)
        {
            PageInfo page = new PageInfo(pageSize, pageNo);
            page.KeyName = "MemberID";

            DataTable dt = NewDao<IOPMemberInfoDao>().GetMemberInfo(condition, page);
            pageInfo = page;
            return dt;
        }

        /// <summary>
        /// 保存会员信息并产生一条默认帐户信息
        /// </summary>
        /// <param name="memberEntity">会员实体</param>
        /// <param name="accountEntity">帐户实体</param>
        /// <param name="newFlag">新增标识</param>
        /// <param name="accountID">帐户id</param>
        /// <returns>1成功</returns>
        public int RegMemberInfo(ME_MemberInfo memberEntity,ME_MemberAccount accountEntity,int newFlag,int accountID)
        {
            int res=-1;
            switch (newFlag)
            {
                case 1:    //新增会员信息同时新增帐户信息
                    this.BindDb(memberEntity);
                    memberEntity.save();
                    accountEntity.MemberID = memberEntity.MemberID;
                    accountEntity.Score = 0;
                    accountEntity.UseFlag = 1;
                    this.BindDb(accountEntity);
                    accountEntity.save();
                    if (accountEntity.CardNO == string.Empty)
                    {
                        accountEntity.CardNO = accountEntity.AccountID.ToString();
                        this.BindDb(accountEntity);
                        accountEntity.save();
                    }
                    res= memberEntity.MemberID;
                    break;
                case 2:   //保存会员修改信息
                    this.BindDb(memberEntity);
                    memberEntity.save();
                    res= memberEntity.MemberID;
                    break;
                case 3:  //保存新增帐户信息
                    this.BindDb(accountEntity);
                    accountEntity.save();
                    if (accountEntity.CardNO == string.Empty)
                    {
                        accountEntity.CardNO = accountEntity.AccountID.ToString();
                        this.BindDb(accountEntity);
                        accountEntity.save();
                    }
                    res = accountEntity.AccountID;
                    break;
                case 4:  //保存修改帐户信息
                    this.BindDb(accountEntity);
                    if (accountEntity.CardNO == string.Empty)
                    {
                        accountEntity.CardNO = accountEntity.AccountID.ToString();
                    }
                    NewObject<MemberAccountManagement>().UpdateCardNO(accountEntity, accountID);
                    res= accountID;
                    break;
                case 5:  //其他界面调用新增会员功能时保存会员信息
                    this.BindDb(memberEntity);
                    memberEntity.save();
                    accountEntity.MemberID = memberEntity.MemberID;
                    accountEntity.Score = 0;
                    accountEntity.UseFlag = 1;
                    this.BindDb(accountEntity);
                    accountEntity.save();
                    if (accountEntity.CardNO == string.Empty)
                    {
                        accountEntity.CardNO = accountEntity.AccountID.ToString();
                        this.BindDb(accountEntity);
                        accountEntity.save();
                    }
                    res = memberEntity.MemberID;
                    break;
                case 6: //其他界面调用修改会员功能时保存会员信息
                    this.BindDb(memberEntity);
                    memberEntity.save();
                    res = memberEntity.MemberID;
                    UpdateOPPatientInfo(memberEntity);
                    break;
            }

            return res;
        }

        /// <summary>
        /// 修改病人信息
        /// </summary>
        /// <param name="memberEntity">会员实体</param>
        public void UpdateOPPatientInfo(ME_MemberInfo memberEntity)
        {
            NewDao<IOPMemberInfoDao>().UpdateOPPatientInfo(memberEntity);
        }

        /// <summary>
        /// 检测该帐户类型的卡号是否在使用中
        /// </summary>
        /// <param name="cardType">帐户类型</param>
        /// <param name="cardNO">帐户号码</param>
        /// <returns>true,使用中；false,未使用</returns>
        public bool CheckCardNO(int cardType,string cardNO)
        {
            DataTable dt = NewDao<IOPMemberInfoDao>().CheckAccountNO(cardType, cardNO);

            if (dt.Rows.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///  更新会员状态
        /// </summary>
        /// <param name="memberID">会员ID</param>
        /// <param name="useFlag">状态标志</param>
        /// <returns>1成功</returns>
        public int UpdateMemberUseFlag(int memberID,int useFlag)
        {
            return NewDao<IOPMemberInfoDao>().UpdataMemberUseFlag(memberID, useFlag);
        }  
    }
}