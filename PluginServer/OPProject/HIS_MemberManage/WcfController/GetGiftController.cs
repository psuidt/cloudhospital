using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.MemberManage;
using HIS_MemberManage.ObjectModel;
using HIS_PublicManage.Dao;

namespace HIS_MemberManage.WcfController
{
    /// <summary>
    /// 查询礼品控制器
    /// </summary>
    [WCFController]
    public class GetGiftController : WcfServerController
    {
        /// <summary>
        /// 根椐帐户类型和帐户号码获取礼品兑换列表
        /// </summary>
        /// <returns>礼品兑换列表</returns>
        [WCFMethod]
        public ServiceResponseData GetPointsExchange()
        {
            int accountid = requestData.GetData<int>(0);
            string accountCode = requestData.GetData<string>(1);
            DataTable dt = NewObject<GetGiftManagement>().GetPointsExchange(accountid, accountCode);
            responseData.AddData<DataTable>(dt);
            return responseData;
        }

        /// <summary>
        /// 保存礼品兑换信息
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData SaveExchange()
        {
            int accountID = requestData.GetData<int>(0);
            int accountScore = requestData.GetData<int>(1);
            ME_PointsExchange exchangeEntity = requestData.GetData<ME_PointsExchange>(2);
            ME_ScoreList scoreListEntity = requestData.GetData<ME_ScoreList>(3);
            int res = NewObject<GetGiftManagement>().SaveExchange(accountID, accountScore, exchangeEntity, scoreListEntity);
            responseData.AddData<int>(res);
            return responseData;
        }
        
        /// <summary>
        /// 检查礼物是否启用
        /// </summary>
        /// <returns>true启用</returns>
        [WCFMethod]
        public ServiceResponseData CheckFlag()
        {
            int cardTypeID = requestData.GetData<int>(0);
            string accountCode = requestData.GetData<string>(1);
            bool res = NewObject<GetGiftManagement>().CheckFlag(cardTypeID, accountCode);
            responseData.AddData<bool>(res);
            return responseData;
        }

        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <returns>会员信息</returns>
        [WCFMethod]
        public ServiceResponseData GetMemberInfo()
        {
            int cardTypeID = requestData.GetData<int>(0);
            string accountCode = requestData.GetData<string>(1);
            DataTable dt = NewDao<IMemberInfoDao>().QueryMemberInfoForCardType(cardTypeID, accountCode);
            responseData.AddData<DataTable>(dt);
            return responseData;
        }
    }
}
