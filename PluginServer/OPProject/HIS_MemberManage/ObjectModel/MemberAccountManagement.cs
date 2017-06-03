using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MemberManage;
using HIS_Entity.OPManage;
using HIS_MemberManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_MemberManage.ObjectModel
{
    /// <summary>
    /// 会员帐户管理
    /// </summary>
    public class MemberAccountManagement : AbstractObjectModel
    {
        /// <summary>
        /// 取得会员帐户信息
        /// </summary>
        /// <param name="memberID">会员id</param>
        /// <returns>会员帐户信息</returns>
        public DataTable GetMemberAccountInfo(int memberID)
        {
            DataTable dt = NewDao<IOPMemberAccountDao>().GetMemberAccountInfo(memberID);
            return dt;
        }

        /// <summary>
        /// 更新帐户信息
        /// </summary>
        /// <param name="accountEntity">帐户信息</param>
        /// <param name="accountID">帐户id</param>
        /// <returns>帐户记录</returns>
        public int UpdateCardNO(ME_MemberAccount accountEntity, int accountID)
        {
            return NewDao<IOPMemberAccountDao>().UpdateCardNO(accountEntity, accountID);
        }

        /// <summary>
        /// 修改帐户启用标识
        /// </summary>
        /// <param name="accountID">帐户id</param>
        /// <param name="useFlag">启用标识</param>
        /// <param name="opeateID">操作人id</param>
        /// <returns>1成功</returns>
        public int UpdateAccountUseFlag(int accountID, int useFlag, int opeateID)
        {
            return NewDao<IOPMemberAccountDao>().UpdateAccountUseFlag(accountID, useFlag, opeateID);
        }

        /// <summary>
        /// 新增帐户信息时校验该会是否有该类型的有效会员卡与新增的帐户号码是否唯一
        /// </summary>
        /// <param name="memberID">会员ID</param>
        /// <param name="cardNO">帐户号码</param>
        /// <param name="cardType">帐户类型</param>
        /// <param name="checkCode">检查编码</param>
        /// <returns>true存在</returns>
        public bool CheckNewAccount(int memberID, string cardNO, int cardType, out bool checkCode)
        {
            DataTable dt = NewDao<IOPMemberAccountDao>().CheckCardType(memberID, cardType);
            checkCode = NewDao<Memberanagement>().CheckCardNO(cardType, cardNO);

            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 将指定帐户积分清零
        /// </summary>
        /// <param name="accountID">帐户id</param>
        /// <param name="score">积分</param>
        /// <param name="operateID">操作人id</param>
        /// <returns>1成功</returns>
        public int ClearAccountScore(int accountID,int score , int operateID)
        {
            string code = string.Empty;

            ME_ScoreList scoreList = new ME_ScoreList();
            scoreList.AccountID = accountID;
            scoreList.ScoreType = 4;
            scoreList.DocumentNo = string.Empty;
            scoreList.Score = 0 - score;
            scoreList.OperateDate = DateTime.Now;
            scoreList.OperateID = operateID;
            return SaveAddScoreInfo(scoreList, 0,3,0,out code);
        }

        /// <summary>
        /// 将帐户所有积分清零
        /// </summary>
        /// <param name="workID">机构id</param>
        /// <param name="operateID">操作人id</param>
        /// <returns>1成功</returns>
        public int ClearAllAccountScore(int workID,int operateID)
        {    
             NewDao<IOPMemberAccountDao>().InsertALLScoreList(workID, operateID);
            return NewDao<IOPMemberAccountDao>().ClearAllAccountScore(workID, operateID);
        }

        /// <summary>
        /// 消费金额转换成积分
        /// </summary>
        /// <param name="workID">机构id</param>
        /// <param name="cardType">卡类型id</param>
        /// <param name="amount">数量</param>
        /// <returns>0成功</returns>
        public int ConvertPoints(int workID,int cardType,decimal amount)
        {
            DataTable dt = NewObject<IOPMemberAccountDao>().GetConvertPoints(workID, cardType);

            if (dt.Rows.Count>0)
            {
                int cash = Convert.ToInt16(dt.Rows[0]["Cash"]);
                int score= Convert.ToInt16(dt.Rows[0]["score"]);
                return 0;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取积分转换规则
        /// </summary>
        /// <param name="workID">机构id</param>
        /// <param name="cardType">卡类型</param>
        /// <returns>积分转换规则</returns>
        public DataTable GetConvertPoints(int workID, int cardType)
        {
             return NewObject<IOPMemberAccountDao>().GetConvertPoints(workID, cardType);
        }

        /// <summary>
        /// 修改帐户功能下的帐户号码有效性检验
        /// </summary>
        /// <param name="accountID">帐户id</param>
        /// <param name="cardType">卡类型</param>
        /// <param name="cardNO">卡号</param>
        /// <returns>true有效</returns>
        public bool CheckCardNOForEdit(int accountID, int cardType, string cardNO)
        {
            DataTable dt = NewDao<IOPMemberInfoDao>().CheckAccountNO(cardType, cardNO);

            if (dt.Rows.Count > 0)
            {
                if (accountID == Convert.ToInt32(dt.Rows[0]["accountID"]))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;  //帐户检验有效
            }
        }

        /// <summary>
        /// 保存积分明细
        /// </summary>
        /// <param name="scoreList">积分明细</param>
        /// <param name="oldScore">原积分</param>
        /// <param name="payType">支付方式</param>
        /// <param name="cash">现金</param>
        /// <param name="code">代码</param>
        /// <returns>1成功</returns>
        public int SaveAddScoreInfo(ME_ScoreList scoreList,int oldScore, int payType,int cash,out string code)
        {
            code = string.Empty;
            BindDb(scoreList);
            scoreList.save();

            if (payType != 3)
            {
                int accID = NewObject<MemberAccountManagement>().GetAccountId(scoreList.OperateID, 1);
                string perfChar = string.Empty;
                string ticketCode = NewObject<InvoiceManagement>().GetInvoiceCurNOAndUse(InvoiceType.账户充值, scoreList.OperateID, out perfChar);
                if (string.IsNullOrEmpty(ticketCode) == false)
                {
                    ticketCode = perfChar + ticketCode;
                }

                ME_Recharge recharge = new HIS_Entity.MemberManage.ME_Recharge();
                recharge.OperateFlag = 0;
                recharge.OperateID = scoreList.OperateID;
                recharge.Money = cash;
                recharge.RechargeCode = ticketCode;   //单据号码
                recharge.TypeID = 2;          //换卡
                recharge.AccountID = scoreList.AccountID;
                recharge.OperateTime = System.DateTime.Now;
                recharge.PayType = payType;
                recharge.Account = accID;
                this.BindDb(recharge);
                recharge.save();
                code = ticketCode;
            }

            return NewObject<IOPMemberAccountDao>().UpdateAccountScore(scoreList.AccountID, oldScore, scoreList.OperateDate, scoreList.OperateID);
        }

        /// <summary>
        /// 获取积分明细
        /// </summary>
        /// <param name="accountID">帐户id</param>
        /// <param name="stDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="flag">标识</param>
        /// <returns>积分明细数据</returns>
        public DataTable QueryAccountScoreList(int accountID,string stDate, string endDate,int flag)
        {
            return NewObject<IOPMemberAccountDao>().QueryAccountScoreList(accountID, stDate, endDate, flag);
        }

        /// <summary>
        /// 保存换卡信息
        /// </summary>
        /// <param name="list">换卡信息</param>
        /// <returns>1成功</returns>
        public int SaveChangeCardList(ME_ChangeCardList list)
        {
            this.BindDb(list);
            return list.save();
        }

        /// <summary>
        /// 获取换卡费用参数
        /// </summary>
        /// <param name="workID">机构id</param>
        /// <returns>参数值</returns>
        public decimal GetSystemConfig(int workID)
        {
            DataTable dt = NewDao<IOPMemberAccountDao>().GetSystemConfig(workID);
            if (dt.Rows.Count>0)
            {
                return Convert.ToDecimal(dt.Rows[0]["value"]);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取帐户id
        /// </summary>
        /// <param name="empid">人员id</param>
        /// <param name="accountType">帐户类型</param>
        /// <returns>帐户id</returns>
        public int GetAccountId(int empid, int accountType)
        {
            int accountid = 0;
            List<OP_Account> account = NewObject<OP_Account>().getlist<OP_Account>(" AccountType=1 AND AccountEmpID=" + empid + " and accountFlag=0");
            if (account != null && account.Count > 0)
            {
                accountid = account[0].AccountID;
            }
            else
            {
                OP_Account newaccount = new OP_Account();
                newaccount.AccountEmpID = empid;
                newaccount.AccountFlag = 0;
                newaccount.TotalFee = 0;
                newaccount.CashFee = 0;
                newaccount.PosFee = 0;
                newaccount.PromFee = 0;
                newaccount.InvoiceCount = 0;
                newaccount.RefundInvoiceCount = 0;
                newaccount.RoundingFee = 0;
                newaccount.AccountType = 1;
                List<OP_Account> accountList = NewObject<OP_Account>().getlist<OP_Account>(" AccountType=1 AND AccountEmpID=" + empid + " and accountFlag=1");
                if (accountList == null || accountList.Count == 0)
                {
                    newaccount.LastDate = DateTime.Now;
                }
                else
                {
                    accountList = accountList.OrderByDescending(p => p.AccountID).ToList(); //获取上次缴款日期
                    newaccount.LastDate = accountList[0].AccountDate;
                }

                this.BindDb(newaccount);
                newaccount.save();
                accountid = newaccount.AccountID;
            }

            return accountid;
        }
    }
}
