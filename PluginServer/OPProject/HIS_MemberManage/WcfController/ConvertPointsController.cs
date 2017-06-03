using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.MemberManage;
using HIS_MemberManage.ObjectModel;

namespace HIS_MemberManage.WcfController
{
    /// <summary>
    /// 积分转换设置控制器
    /// </summary>
    [WCFController]
    public class ConvertPointsController : WcfServerController
    {
        /// <summary>
        /// 返回报有帐户类型信息
        /// </summary>
        /// <returns>帐户类型信息</returns>
        [WCFMethod]
        public ServiceResponseData GetAccountTypeInfo()
        {
            DataTable dt = NewObject<ConvertPointsManagement>().GetAccountTypeInfo();
            responseData.AddData<DataTable>(dt);
            return responseData;
        }

        /// <summary>
        /// 保存帐户类型信息
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData SaveCardTypeInfo()
        {
            ME_CardTypeList cardTypeList = requestData.GetData<ME_CardTypeList>(0);
            int res = NewObject<ConvertPointsManagement>().SaveCardTypeInfo(cardTypeList);
            responseData.AddData<int>(res);
            return responseData;
        }

        /// <summary>
        /// 新增帐户类型名称时检验名称的有效性
        /// </summary>
        /// <returns>true有效</returns>
        [WCFMethod]
        public ServiceResponseData CheckCardTypeNameForADD()
        {
            string name = requestData.GetData<string>(0);
            bool res = NewObject<ConvertPointsManagement>().CheckCardTypeNameForADD(name);
            responseData.AddData<bool>(res);
            return responseData;
        }

        /// <summary>
        /// 修改帐户类型时检验名称的有效性
        /// </summary>
        /// <returns>true有效</returns>
        [WCFMethod]
        public ServiceResponseData CheckCardTypeNameForEdit()
        {
            string name = requestData.GetData<string>(0);
            int id = requestData.GetData<int>(1);
            bool res = NewObject<ConvertPointsManagement>().CheckCardTypeNameForEdit(name, id);
            responseData.AddData<bool>(res);
            return responseData;
        }

        /// <summary>
        /// 更新状态标志
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData UpdateUseFlag()
        {
            int id = requestData.GetData<int>(0);
            int flag = requestData.GetData<int>(1);
            int operateID = requestData.GetData<int>(2);
            int res = NewObject<ConvertPointsManagement>().UpdateUseFlag(id, flag, operateID);
            responseData.AddData<int>(res);
            return responseData;
        }

        /// <summary>
        /// 获取指定帐户类型的积分兑换设置信息
        /// </summary>
        /// <returns>帐户类型的积分兑换设置信息</returns>
        [WCFMethod]
        public ServiceResponseData GetConvertPointsInfo()
        {
            int cardTypeID = requestData.GetData<int>(0);
            DataTable dt = NewObject<ConvertPointsManagement>().GetConvertPointsInfo(cardTypeID);
            responseData.AddData<DataTable>(dt);
            return responseData;
        }

        /// <summary>
        /// 保存积分兑换设置
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData SavePoints()
        {
            ME_ConvertPoints convertPoints = requestData.GetData<ME_ConvertPoints>(0);
            int res = NewObject<ConvertPointsManagement>().SavePoints(convertPoints);
            responseData.AddData<int>(res);
            return responseData;
        }

        /// <summary>
        /// 校验新增积分设置是否存在
        /// </summary>
        /// <returns>true存在</returns>
        [WCFMethod]
        public ServiceResponseData CheckPointsForADD()
        {
            int cardTypeID = requestData.GetData<int>(0);
            int workID = requestData.GetData<int>(1);
            bool res = NewObject<ConvertPointsManagement>().CheckPointsForADD(cardTypeID, workID);
            responseData.AddData<bool>(res);
            return responseData;
        }

        /// <summary>
        /// 检查卡前缀
        /// </summary>
        /// <returns>卡信息</returns>
        [WCFMethod]
        public ServiceResponseData CheckCardPrefix()
        {
            string cardTypeID = requestData.GetData<string>(0);
 
            DataTable dt = NewObject<ConvertPointsManagement>().CheckCardPrefix(cardTypeID);
            responseData.AddData<DataTable>(dt);
            return responseData;
        }

        /// <summary>
        /// 更新积分兑换设置表的使用标志
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData UpdatePointsUseFlag()
        {
            int pointsID = requestData.GetData<int>(0);
            int useFlag = requestData.GetData<int>(1);
            int operateID = requestData.GetData<int>(2);

            int res= NewObject<ConvertPointsManagement>().UpdatePointsUseFlag(pointsID, useFlag, operateID);
            responseData.AddData<int>(res);
            return responseData;
        }
    }
}
