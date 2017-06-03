using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.MemberManage;
using HIS_MemberManage.ObjectModel;
using HIS_PublicManage.ObjectModel;

namespace HIS_MemberManage.WcfController
{
    /// <summary>
    /// 优惠方案控制器
    /// </summary>
    [WCFController]
    public class PromotionProjectController : WcfServerController
    {
        /// <summary>
        /// 获取优惠方案头表信息
        /// </summary>
        /// <returns>优惠方案头表信息</returns>
        [WCFMethod]
        public ServiceResponseData GetPromotionProjectHeadInfo()
        {
            int workID = requestData.GetData<int>(0);
            string stDate = requestData.GetData<string>(1);
            string endDate = requestData.GetData<string>(2);
            DataTable dt = NewObject<PromotionProjectManagement>().GetPromotionProjectHeadInfo(workID, stDate, endDate);
            responseData.AddData<DataTable>(dt);
            return responseData;
        }

        /// <summary>
        /// 新增方案头表时校验方案名称是否有效
        /// </summary>
        /// <returns>true有效</returns>
        [WCFMethod]
        public ServiceResponseData CheckPromNameForADD()
        {
            string name = requestData.GetData<string>(0);
            bool res = NewObject<PromotionProjectManagement>().CheckPromNameForADD(name);
            responseData.AddData<bool>(res);
            return responseData;
        }

        /// <summary>
        /// 编辑方案头表时校验方案名称是否有效
        /// </summary>
        /// <returns>true有效</returns>
        [WCFMethod]
        public ServiceResponseData CheckPromNameForEdit()
        {
            int headID = requestData.GetData<int>(0);
            string name = requestData.GetData<string>(1);
            bool res = NewObject<PromotionProjectManagement>().CheckPromNameForEdit(headID, name);
            responseData.AddData<bool>(res);
            return responseData;
        }

        /// <summary>
        /// 保存优惠方案头表
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData SaveHeadInfo()
        {
            ME_PromotionProjectHead headEntity = requestData.GetData<ME_PromotionProjectHead>(0);
            int res = NewObject<PromotionProjectManagement>().SaveHeadInfo(headEntity);
            responseData.AddData<int>(res);
            return responseData;
        }

        /// <summary>
        /// 删除优惠方案
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData DeletePromPro()
        {
            int headID = requestData.GetData<int>(0);
            int res = NewObject<PromotionProjectManagement>().DeletePromPro(headID);
            responseData.AddData<int>(res);
            return responseData;
        }

        /// <summary>
        /// 更新优惠头表状态
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData UpdateHeadUseFlag()
        {
            int promID = requestData.GetData<int>(0);
            int flag = requestData.GetData<int>(1);
            int operateID = requestData.GetData<int>(2);
            int res = NewObject<PromotionProjectManagement>().UpdateHeadUseFlag(promID, flag, operateID);
            responseData.AddData<int>(res);
            return responseData;
        }

        /// <summary>
        /// 获取病人费用类型
        /// </summary>
        /// <returns>病人费用类型</returns>
        [WCFMethod]
        public ServiceResponseData GetPatFeeType()
        {
            DataTable dtPatType = NewObject<BasicDataManagement>().GetPatType();
            responseData.AddData<DataTable>(dtPatType);
            return responseData;
        }

        /// <summary>
        /// 检查指定时间范围内是否在用的方案
        /// </summary>
        /// <returns>true有效</returns>
        [WCFMethod]
        public ServiceResponseData CheckPromDate()
        {
            string stDate = requestData.GetData<string>(0);
            int workID = requestData.GetData<int>(1);
            bool res = NewObject<PromotionProjectManagement>().CheckPromDate(stDate, workID);
            responseData.AddData<bool>(res);
            return responseData;
        }

        /// <summary>
        /// 获取方案明细数据
        /// </summary>
        /// <returns>方案明细数据</returns>
        [WCFMethod]
        public ServiceResponseData GetPromotionProjectDetail()
        {
            int promID = requestData.GetData<int>(0);
            DataTable res = NewObject<PromotionProjectManagement>().GetPromotionProjectDetail(promID);
            responseData.AddData<DataTable>(res);
            return responseData;
        }

        /// <summary>
        /// 保存方案明细
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData SavePromotionProjectDetail()
        {
            ME_PromotionProjectDetail detailEntity = requestData.GetData<ME_PromotionProjectDetail>(0);
            int res = NewObject<PromotionProjectManagement>().SavePromotionProjectDetail(detailEntity);
            responseData.AddData<int>(res);
            return responseData;
        }

        #region 新增或修改时校验优惠明细是否存在重复  
        /// <summary>
        /// 检查总额优惠方案
        /// </summary>
        /// <returns>1成功</returns>      
        [WCFMethod]
        public ServiceResponseData CheckDetailForAmount()
        {
            int cardTypeID = requestData.GetData<int>(0);
            int patTypeID = requestData.GetData<int>(1);
            int costTypeID = requestData.GetData<int>(2);
            int promTypeID = requestData.GetData<int>(3);
            int promID = requestData.GetData<int>(4);
            int promSunID = requestData.GetData<int>(5);
            bool res = NewObject<PromotionProjectManagement>().CheckDetailForAmount(cardTypeID, patTypeID, costTypeID, promTypeID, promID, promSunID);
            responseData.AddData<bool>(res);
            return responseData;
        }

        /// <summary>
        /// 检查类型优惠方案
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData CheckDetailForClass()
        {
            int cardTypeID = requestData.GetData<int>(0);
            int patTypeID = requestData.GetData<int>(1);
            int costTypeID = requestData.GetData<int>(2);
            int promTypeID = requestData.GetData<int>(3);
            int promID = requestData.GetData<int>(4);
            int promSunID = requestData.GetData<int>(5);
            int classID = requestData.GetData<int>(6);
            bool res = NewObject<PromotionProjectManagement>().CheckDetailForClass(cardTypeID, patTypeID, costTypeID, promTypeID, classID, promID, promSunID);
            responseData.AddData<bool>(res);
            return responseData;
        }

        /// <summary>
        /// 检查项目优惠方案
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData CheckDetailForItem()
        {
            int cardTypeID = requestData.GetData<int>(0);
            int patTypeID = requestData.GetData<int>(1);
            int costTypeID = requestData.GetData<int>(2);
            int promTypeID = requestData.GetData<int>(3);
            int promID = requestData.GetData<int>(4);
            int promSunID = requestData.GetData<int>(5);
            int itemID = requestData.GetData<int>(6);
            bool res = NewObject<PromotionProjectManagement>().CheckDetailForItem(cardTypeID, patTypeID, costTypeID, promTypeID, itemID, promID, promSunID);
            responseData.AddData<bool>(res);
            return responseData;
        }
        #endregion

        /// <summary>
        /// 获取大项目数据
        /// </summary>
        /// <returns>大项目数据</returns>
        [WCFMethod]
        public ServiceResponseData GetStatItem()
        {
            DataTable res = NewObject<PromotionProjectManagement>().GetStatItem();
            responseData.AddData<DataTable>(res);
            return responseData;
        }

        /// <summary>
        /// 获取费用项目数据
        /// </summary>
        /// <returns>费用项目数据</returns>
        [WCFMethod]
        public ServiceResponseData GetSimpleFeeItemDataDt()
        {
            DataTable res = NewObject<PromotionProjectManagement>().GetSimpleFeeItemDataDt();
            responseData.AddData<DataTable>(res);
            return responseData;
        }

       /// <summary>
       /// 更新优惠明细表使用状态
       /// </summary>
       /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData UpdateDetailFlag()
        {
             int promSunID = requestData.GetData<int>(0);
            int flag = requestData.GetData<int>(1);
            int res = NewObject<PromotionProjectManagement>().UpdateDetailFlag(promSunID, flag);
            responseData.AddData<int>(res);
            return responseData;
        }

        /// <summary>
        /// 复制优惠方案
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData CopypPromotionProject( )
        {
            int promID = requestData.GetData<int>(0);
            int operateID = requestData.GetData<int>(1);
            int res = NewObject<PromotionProjectManagement>().CopypPromotionProject(promID, operateID);
            responseData.AddData<int>(res);
            return responseData;
        }
    }
}
