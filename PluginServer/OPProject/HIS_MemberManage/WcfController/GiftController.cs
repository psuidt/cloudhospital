using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.MemberManage;
using HIS_MemberManage.ObjectModel;

namespace HIS_MemberManage.WcfController
{
    /// <summary>
    /// 礼品控制器
    /// </summary>
    [WCFController]
    public class GiftController: WcfServerController
    {
        /// <summary>
        /// 获取帐户类型信息
        /// </summary>
        /// <returns>帐户类型信息</returns>
        [WCFMethod]
        public ServiceResponseData GetCardTypeForWork( )
        {
            int workID = requestData.GetData<int>(0);
            DataTable dt = NewObject<GiftManagement>().GetCardTypeForWork(workID);
            responseData.AddData<DataTable>(dt);
            return responseData;             
        }

        /// <summary>
        /// 获取礼品信息
        /// </summary>
        /// <returns>礼品信息</returns>
        [WCFMethod]
        public ServiceResponseData GetGiftInfo( )
        {
            int workID = requestData.GetData<int>(0);
            int cardTypeID = requestData.GetData<int>(1);
            int useFlag = requestData.GetData<int>(2);
            DataTable dt = NewObject<GiftManagement>().GetGiftInfo(workID, cardTypeID, useFlag);
            responseData.AddData<DataTable>(dt);
            return responseData;
        }

        /// <summary>
        /// 保存礼品信息
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData SaveGiftInfo()
        {
            ME_Gift giftInfo= requestData.GetData<ME_Gift>(0);
            int res = NewObject<GiftManagement>().SaveGiftInfo(giftInfo);
            responseData.AddData<int>(res);
            return responseData;
        }

        /// <summary>
        /// 检查礼品重复性
        /// </summary>
        /// <returns>false存在</returns>
        [WCFMethod]
        public ServiceResponseData CheckGiftNameForADD()
        {
            int workID = requestData.GetData<int>(0);
            int cardTypeID = requestData.GetData<int>(1);
            string giftName = requestData.GetData<string>(2);

            bool res = NewObject<GiftManagement>().CheckGiftNameForADD(workID, cardTypeID, giftName);
            responseData.AddData<bool>(res);
            return responseData;
        }

        /// <summary>
        /// 检查礼品名称重复性
        /// </summary>
        /// <returns>false存在</returns>
        [WCFMethod]
        public ServiceResponseData CheckGiftNameForEdit()
        {
            int workID = requestData.GetData<int>(0);
            int cardTypeID = requestData.GetData<int>(1);
            string giftName = requestData.GetData<string>(2);
            int giftID = requestData.GetData<int>(3);

            bool res = NewObject<GiftManagement>().CheckGiftNameForEdit(workID, cardTypeID, giftName,giftID);
            responseData.AddData<bool>(res);
            return responseData;
        }

        /// <summary>
        /// 更新礼品标识
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData UpdateGiftFlag()
        {
            int giftID = requestData.GetData<int>(0);
            int useFlag = requestData.GetData<int>(1);
            int operateID = requestData.GetData<int>(2);
            int res = NewObject<GiftManagement>().UpdateGiftFlag(giftID, useFlag, operateID);
            responseData.AddData<int>(res);
            return responseData;
        }
    }
}
