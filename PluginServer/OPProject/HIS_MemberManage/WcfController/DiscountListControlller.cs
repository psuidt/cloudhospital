using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_MemberManage.ObjectModel;

namespace HIS_MemberManage.WcfController
{
    /// <summary>
    /// 优惠信息控制器
    /// </summary>
    [WCFController]
    public class DiscountListControlller : WcfServerController
    {
        /// <summary>
        /// 取得优惠信息
        /// </summary>
        /// <returns>优惠信息</returns>
        [WCFMethod]
        public ServiceResponseData GetDiscountListInfo()
        {
            string stDate = requestData.GetData<string>(0);
            string endDate = requestData.GetData<string>(1);
            int workID = requestData.GetData<int>(2);
            int cardTypeID = requestData.GetData<int>(3);
            string cardNO = requestData.GetData<string>(4);

            DataTable dt = NewObject<DiscountListManagement>().GetDiscountListInfo(stDate, endDate, workID, cardTypeID, cardNO);
            responseData.AddData<DataTable>(dt);
            return responseData;
        }
    }
}
