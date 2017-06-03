using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.SqlPagination;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.MemberManage;
using HIS_MemberManage.Dao;
using HIS_MemberManage.ObjectModel;
using HIS_PublicManage.ObjectModel;

namespace HIS_MemberManage.WcfController
{
    /// <summary>
    /// 会员控制器
    /// </summary>
    [WCFController]
    public class MemberController : WcfServerController
    {
        /// <summary>
        /// 获取符合条件的会员信息
        /// </summary>
        /// <returns>会员信息</returns>
        [WCFMethod]
        public ServiceResponseData GetMemberInfoPage()
        {
            int pageNo = requestData.GetData<int>(0);  //页码
            int pageSize = requestData.GetData<int>(1); //每页有多少条记录
            string sqlCondition = requestData.GetData<string>(2);
            
            PageInfo page = null;
            DataTable dt = NewObject<Memberanagement>().QueryMemberInfo(pageNo, pageSize, sqlCondition, out page);

            responseData.AddData(dt);
            responseData.AddData(page.totalRecord);

            return responseData;
        }

        /// <summary>
        /// 获取所有机构信息
        /// </summary>
        /// <returns>所有机构信息</returns>
        [WCFMethod]
        public ServiceResponseData GetWorkInfo()
        {
            bool newFlag = requestData.GetData<bool>(0);
            DataTable dt = NewObject<BasicDataManagement>().GetWorkers(newFlag);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取所有基础信息
        /// </summary>
        /// <returns>所有基础信息</returns>
        [WCFMethod]
        public ServiceResponseData GetAllInfo()
        {
            DataTable dtPatType = NewObject<BasicDataManagement>().GetPatType();

            DataTable dtSex = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.性别);

            DataTable dtNation = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.民族);

            DataTable dtCity = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.地区编码);

            DataTable dtDegree = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.教育程度);

            DataTable dtRelation = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.关系);

            DataTable dtOccupation = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.职业);

            DataTable dtMatrimony = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.婚姻状况);

            DataTable dtCardType = NewObject<MemberManagement>().GetCardTypeList();

            DataTable dtRoute = NewDao<SqlOPMemberInfoDao>().GetRouteInfo();

            DataTable dtgj = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.国籍);

            responseData.AddData(dtPatType);   //病人类型
            responseData.AddData(dtSex);       //性别
            responseData.AddData(dtNation);   //民族
            responseData.AddData(dtCity);     //所属地区
            responseData.AddData(dtDegree);   //文化程度
            responseData.AddData(dtRelation); //关系
            responseData.AddData(dtCardType); //卡类型
            responseData.AddData(dtOccupation); //关系
            responseData.AddData(dtMatrimony); //婚姻状况
            responseData.AddData(dtRoute); //知晓途径
            responseData.AddData(dtgj); //国籍

            return responseData;
        }

        /// <summary>
        /// 获取一部分基础数据
        /// </summary>
        /// <returns>一部分基础数据</returns>
        [WCFMethod]
        public ServiceResponseData GetBaseDataOne()   
        {
            DataTable dtPatType = NewObject<BasicDataManagement>().GetPatType();

            DataTable dtSex = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.性别);

            DataTable dtNation = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.民族);

            DataTable dtCity = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.地区编码);

            DataTable dtDegree = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.教育程度);

            responseData.AddData(dtPatType);   //病人类型
            responseData.AddData(dtSex);       //性别
            responseData.AddData(dtNation);   //民族
            responseData.AddData(dtCity);     //所属地区
            responseData.AddData(dtDegree);   //文化程度

            return responseData;
        }

        /// <summary>
        ///  获取一部分基础数据
        /// </summary>
        /// <returns>一部分基础数据</returns>
        [WCFMethod]
        public ServiceResponseData GetBaseDataTwo()
        {
            DataTable dtRelation = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.关系);

            DataTable dtOccupation = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.职业);

            DataTable dtMatrimony = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.婚姻状况);
   
            DataTable dtRoute = NewDao<SqlOPMemberInfoDao>().GetRouteInfo();

            DataTable dtgj = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.国籍);

            responseData.AddData(dtRelation); //关系
          
            responseData.AddData(dtOccupation); //关系
            responseData.AddData(dtMatrimony); //婚姻状况
            responseData.AddData(dtRoute); //知晓途径
            responseData.AddData(dtgj); //国籍

            return responseData;
        }

        /// <summary>
        /// 保存会员信息
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveMemberInfo()
        {
            ME_MemberInfo memberList = requestData.GetData<ME_MemberInfo>(0);
            ME_MemberAccount accountList = requestData.GetData<ME_MemberAccount>(1);
            int newFlag = requestData.GetData<int>(2);
            int accountID = requestData.GetData<int>(3);
            int res=NewObject<Memberanagement>().RegMemberInfo(memberList, accountList, newFlag, accountID);
            responseData.AddData(true);
            responseData.AddData(res);
            return responseData;
        }

        /// <summary>
        /// 检测卡号是否可用
        /// </summary>
        /// <returns>返回true存在</returns>
        [WCFMethod]
        public ServiceResponseData CheckCardNO()
        {
            int cardType = requestData.GetData<int>(0);
            string cardNO = requestData.GetData<string>(1);
            responseData.AddData(NewObject<Memberanagement>().CheckCardNO(cardType, cardNO));
            return responseData;
        }

        /// <summary>
        /// 更新会员状态标志
        /// </summary>
        /// <returns>返回1成功</returns>
        [WCFMethod]
        public ServiceResponseData UpdateMemberUseFlag()
        {
            int memberID = requestData.GetData<int>(0);
            int useFlag = requestData.GetData<int>(1);
            int res = NewDao<Memberanagement>().UpdateMemberUseFlag(memberID, useFlag);
            responseData.AddData(res);
            return responseData;
        }

        /// <summary>
        /// 根椐会员ID获取帐户信息
        /// </summary>
        /// <returns>帐户信息</returns>
        [WCFMethod]
        public ServiceResponseData GetMemberAccountInfo()
        {
            int memberID = requestData.GetData<int>(0);
            DataTable dt = NewObject<MemberAccountManagement>().GetMemberAccountInfo(memberID);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 保存修改后的帐户信息
        /// </summary>
        /// <returns>返回1成功</returns>
        public ServiceResponseData UpdateCardNO()
        {
            ME_MemberAccount accountEntity = requestData.GetData<ME_MemberAccount>(0);
            int accountID = requestData.GetData<int>(1);
            int res = NewObject<MemberAccountManagement>().UpdateCardNO(accountEntity, accountID);
            responseData.AddData(res);
            return responseData;
        }

        /// <summary>
        /// 更新帐户状态标志
        /// </summary>
        /// <returns>返回1成功</returns>
        [WCFMethod]
        public ServiceResponseData UpdateAccountUseFlag()
        {
            int accountID = requestData.GetData<int>(0);
            int useFlag = requestData.GetData<int>(1);
            int opeateID= requestData.GetData<int>(2);
            int res = NewObject<MemberAccountManagement>().UpdateAccountUseFlag(accountID, useFlag, opeateID);
            responseData.AddData(res);
            return responseData;
        }

        /// <summary>
        /// 新增帐户信息时校验该会是否有该类型的有效会员卡与新增的帐户号码是否唯一
        /// </summary>
        /// <returns>返回1成功</returns>
        [WCFMethod]
        public ServiceResponseData CheckNewAccount()
        {
            int memberID = requestData.GetData<int>(0);
            string cardNO = requestData.GetData<string>(1);
            int cardType = requestData.GetData<int>(2);

            bool cardNOFlag;
            bool res = NewObject<MemberAccountManagement>().CheckNewAccount(memberID, cardNO, cardType, out cardNOFlag);
            responseData.AddData(res);
            responseData.AddData(cardNOFlag);
            return responseData;
        }

        /// <summary>
        /// 指定帐户积分归零
        /// </summary>
        /// <returns>返回1成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData ClearAccountScore()
        {
            int accountID = requestData.GetData<int>(0);
            int score = requestData.GetData<int>(1);
            int operateID = requestData.GetData<int>(2);
            int res = NewObject<MemberAccountManagement>().ClearAccountScore(accountID, score, operateID);
            responseData.AddData(res);
            return responseData;
        }

        /// <summary>
        /// 指定机构所有帐户积分归零
        /// </summary>
        /// <returns>返回1成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData ClearAllAccountScore()
        {
            int workID = requestData.GetData<int>(0);
            int operateID = requestData.GetData<int>(1);

            int res = NewObject<MemberAccountManagement>().ClearAllAccountScore(workID, operateID);
            responseData.AddData(res);
            return responseData;
        }

        /// <summary>
        /// 修改帐户号码时检验新帐户号码的有效性
        /// </summary>
        /// <returns>返回true成功</returns>
        [WCFMethod]
        public ServiceResponseData CheckCardNOForEdit()
        {
            int accountID = requestData.GetData<int>(0);
            int cardType = requestData.GetData<int>(1);
            string cardNO = requestData.GetData<string>(2);
            bool res = NewObject<MemberAccountManagement>().CheckCardNOForEdit(accountID, cardType, cardNO);
            responseData.AddData(res);
            return responseData;
        }

        /// <summary>
        /// 获取积分转换规则
        /// </summary>
        /// <returns>积分转换规则</returns>
        [WCFMethod]
        public ServiceResponseData GetConvertPoints()
        {
            int workID = requestData.GetData<int>(0);
            int cardType = requestData.GetData<int>(1);
            DataTable dt= NewObject<MemberAccountManagement>().GetConvertPoints(workID, cardType);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 保存积分明细
        /// </summary>
        /// <returns>返回1成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveAddScore()
        {
            string code = string.Empty;
            ME_ScoreList scoreList= requestData.GetData<ME_ScoreList>(0);
            int oldScore = requestData.GetData<int>(1);
            int payType= requestData.GetData<int>(2);
            int cash= requestData.GetData<int>(3);
            scoreList.OperateDate = DateTime.Now;
            int res=NewObject<MemberAccountManagement>().SaveAddScoreInfo(scoreList, oldScore, payType,cash,out code);
            responseData.AddData(res);
            responseData.AddData(code);
            return responseData;
        }

        /// <summary>
        /// 查询帐户积分明细
        /// </summary>
        /// <returns>帐户积分明细</returns>
        [WCFMethod]
        public ServiceResponseData QueryAccountScoreList()
        {
            int accountID = requestData.GetData<int>(0);
            string stDate = requestData.GetData<string>(1);
            string endDate = requestData.GetData<string>(2);
            int flag = requestData.GetData<int>(3);
            DataTable dt= NewObject<MemberAccountManagement>().QueryAccountScoreList(accountID, stDate, endDate, flag);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取卡类型
        /// </summary>
        /// <returns>卡类型</returns>
        [WCFMethod]
        public ServiceResponseData GetCardType()
        {
            DataTable dtCardType = NewObject<MemberManagement>().GetCardTypeList();
            responseData.AddData(dtCardType);
            return responseData;
        }

        /// <summary>
        /// 获取帐户卡号更换列表
        /// </summary>
        /// <returns>帐户卡号更换列表</returns>
        [WCFMethod]
        public ServiceResponseData GetChangeCardList()
        {
            int accountID = requestData.GetData<int>(0);
            DataTable dt = NewDao<IOPMemberAccountDao>().GetChangeCardList(accountID);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取系统参数
        /// </summary>
        /// <returns>系统参数</returns>
        [WCFMethod]
        public ServiceResponseData GetSystemConfig()
        {
            int workID = requestData.GetData<int>(0);
            decimal flag = NewObject<MemberAccountManagement>().GetSystemConfig(workID);
            responseData.AddData(flag);
            return responseData;
        }

        /// <summary>
        /// 保存卡号更换信息
        /// </summary>
        /// <returns>卡号更换信息</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveChangeCardList()
        {
            int resFlag = 0;
            ME_ChangeCardList list= requestData.GetData<ME_ChangeCardList>(0);
            int workID = requestData.GetData<int>(1);
            int payType = requestData.GetData<int>(2);

            //1、更新帐户表
            resFlag = NewDao<IOPMemberAccountDao>().UpdateAccountInfo(list.AccountID, list.NewCardNO, list.OperateID);

            //2、新建换卡纪录
            resFlag = NewObject<MemberAccountManagement>().SaveChangeCardList(list);

            //3、新增换卡费用
            int accID = NewObject<MemberAccountManagement>().GetAccountId(list.OperateID, 1);
            string perfChar = string.Empty;
            string ticketCode = NewObject<InvoiceManagement>().GetInvoiceCurNOAndUse(InvoiceType.账户充值, list.OperateID, out perfChar);
            if (string.IsNullOrEmpty(ticketCode)==false)
            {
                ticketCode = perfChar + ticketCode;
            }

            if (list.Amount>0)
            {
                ME_Recharge recharge = new HIS_Entity.MemberManage.ME_Recharge();
                recharge.OperateFlag = 0;
                recharge.OperateID = list.OperateID;
                recharge.Money = list.Amount;
                recharge.RechargeCode = ticketCode;   //单据号码
                recharge.TypeID = 1;          //换卡
                recharge.AccountID = list.AccountID;
                recharge.OperateTime = System.DateTime.Now;
                recharge.PayType = payType;
                recharge.Account = accID;
                this.BindDb(recharge);
                recharge.save();
            }

            responseData.AddData(resFlag);
            responseData.AddData(ticketCode);
            return responseData;
        }
    }
}