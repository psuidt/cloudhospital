using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.MemberManage;

namespace HIS_MemberManage.Winform.Controller
{
    /// <summary>
    /// 获取礼品控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmGetGift")]//在菜单上显示
    [WinformView(Name = "FrmGetGift", DllName = "HIS_MemberManage.Winform.dll", ViewTypeName = "HIS_MemberManage.Winform.ViewForm.FrmGetGift")]
    public class GetGiftController: WcfClientController
    {
        /// <summary>
        /// 绑定卡类型信息
        /// </summary>
        /// <returns>卡类型信息</returns>
        [WinformMethod]
        public DataTable BindCardTypeInfo()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.WorkId);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "GiftController", "GetCardTypeForWork", requestAction);
            return retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 取得交易信息
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="cardCode">卡编号</param>
        /// <returns>交易信息</returns>
        [WinformMethod]
        public DataTable GetExchargeInfo(int cardTypeID,string cardCode)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(cardTypeID);
                request.AddData(cardCode);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "GetGiftController", "GetPointsExchange", requestAction);
            return retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 获取登录用户与指定帐户类型的礼品列表
        /// </summary>
        /// <param name="cardTypeID">卡类型ID</param>
        /// <returns>礼品数据</returns>
        [WinformMethod]
        public DataTable GetGiftForWorkID(int cardTypeID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.WorkId);
                request.AddData(cardTypeID);
                request.AddData(1);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "GiftController", "GetGiftInfo", requestAction);
            return retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 保存礼品兑换
        /// </summary>
        /// <param name="accountID">帐户ID</param>
        /// <param name="accountScore">帐户兑换礼品后的剩余积分</param>
        /// <param name="giftID">礼品ID</param>
        ///  <param name="amount">兑换礼品数量</param>
        /// <param name="giftScore">兑换礼品所花费的积分</param>
        /// <returns>1成功0失败</returns>
        [WinformMethod]
        public int SaveExchange(int accountID,int accountScore,int giftID,int amount,int giftScore)
        {
            //1、保存最新帐户信息
            //2、保存礼品兑换明细表信息
            ME_PointsExchange exchangeEntity = new ME_PointsExchange();
            exchangeEntity.GiftID = giftID;
            exchangeEntity.AccountID = accountID;
            exchangeEntity.Score = giftScore;
            exchangeEntity.Amount = amount;
            exchangeEntity.OperateDate = DateTime.Now;
            exchangeEntity.OperateID = LoginUserInfo.UserId;

            //3、保存积分明细表
            ME_ScoreList scoreListEntity = new ME_ScoreList();
            scoreListEntity.AccountID = accountID;

            //兑换礼品减积分
            scoreListEntity.ScoreType = 5;   
            scoreListEntity.Score = giftScore;
            scoreListEntity.OperateDate = exchangeEntity.OperateDate;
            scoreListEntity.OperateID= LoginUserInfo.UserId;

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(accountID);
                request.AddData(accountScore);
                request.AddData(exchangeEntity);
                request.AddData(scoreListEntity);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "GetGiftController", "SaveExchange", requestAction);

            return retdata.GetData<int>(0);
        }

        /// <summary>
        /// 会员资格或帐户是否停用
        /// </summary>
        /// <param name="cardTypeID">卡类型Id</param>
        /// <param name="accountCode">卡号</param>
        /// <returns>false停用true启用</returns>
        [WinformMethod]
        public bool CheckFlag(int cardTypeID, string accountCode)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(cardTypeID);
                request.AddData(accountCode);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "GetGiftController", "CheckFlag", requestAction);
            return retdata.GetData<bool>(0);
        }

        /// <summary>
        /// 取得会员信息
        /// </summary>
        /// <param name="cardTypeID">卡类型ID</param>
        /// <param name="accountCode">帐户编码</param>
        /// <returns>会员信息数据</returns>
        [WinformMethod]
        public DataTable GetMemberInfo(int cardTypeID, string accountCode)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(cardTypeID);
                request.AddData(accountCode);             
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "GetGiftController", "GetMemberInfo", requestAction);
            return retdata.GetData<DataTable>(0);
        }
    }
}
