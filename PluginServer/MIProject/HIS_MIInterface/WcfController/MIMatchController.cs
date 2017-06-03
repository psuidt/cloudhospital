using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using System.Data;
using HIS_MIInterface.Dao;

namespace HIS_MIInterface.WcfController
{
    [WCFController]
    public class MIMatchController : WcfServerController
    {
        /// <summary>
        /// 获取HIS目录
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData M_GetHISCatalogInfo()
        {
            int catalogType = requestData.GetData<int>(0);
            int stopFlag = requestData.GetData<int>(1);
            int matchFlag = requestData.GetData<int>(2);
            int ybID = requestData.GetData<int>(3);

            DataTable dt = NewDao<IMatchInterface>().M_GetHISCatalogInfo(catalogType, stopFlag, matchFlag, ybID);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取医保目录
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData M_GetMICatalogInfo()
        {
            int catalogType = requestData.GetData<int>(0);
            int ybId = requestData.GetData<int>(1);

            DataTable dt = NewDao<IMatchInterface>().M_GetMICatalogInfo(catalogType, ybId);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取匹配目录
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData M_GetMatchCatalogInfo()
        {
            int catalogType = requestData.GetData<int>(0);
            int ybId = requestData.GetData<int>(1);
            int auditFlag = requestData.GetData<int>(2);
            DataTable dt = NewDao<IMatchInterface>().M_GetMatchCatalogInfo(catalogType, ybId,auditFlag);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取医保类型
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData M_GetMIType()
        {
            DataTable dt = NewDao<IMatchInterface>().M_GetMIType();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 清空匹配数据
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData M_DeleteMatchLogs()
        {
            int ybId = requestData.GetData<int>(0);
            string itemType = requestData.GetData<string>(1);
            bool bResult = NewDao<IMatchInterface>().M_DeleteMatchLogs(ybId, itemType);
            responseData.AddData(bResult);
            return responseData;
        }
        /// <summary>
        /// 保存匹配目录
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData M_SaveMatchLogs()
        {
            DataTable dt = requestData.GetData<DataTable>(0);
            int ybId = requestData.GetData<int>(1);

            bool bResult = NewDao<IMatchInterface>().M_SaveMatchLogs(dt, ybId);
            responseData.AddData(bResult);
            return responseData;
        }
        /// <summary>
        /// 更新匹配目录
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData M_UpdateMatchLogs()
        {
            string id = requestData.GetData<string>(0);
            string auditFlag = requestData.GetData<string>(1);

            bool bResult = NewDao<IMatchInterface>().M_UpdateMatchLogs(id, auditFlag);
            responseData.AddData(bResult);
            return responseData;
        }
        /// <summary>
        /// 更新所有目录
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData M_UpdateAllMatchLogs()
        {
            int ybId = requestData.GetData<int>(0);

            bool bResult = NewDao<IMatchInterface>().M_UpdateAllMatchLogs(ybId);
            responseData.AddData(bResult);
            return responseData;
        }

        /// <summary>
        /// 更新材料目录级别
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData M_UpdateMWLogLevel()
        {
            int ybId = requestData.GetData<int>(0);

            bool bResult = NewDao<IMatchInterface>().M_UpdateMWLogLevel(ybId);

            responseData.AddData(bResult);
            return responseData;
        }

        /// <summary>
        /// 更新药品目录级别
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData M_UpdateDrugLogLevel()
        {
            int ybId = requestData.GetData<int>(0);

            bool bResult = NewDao<IMatchInterface>().M_UpdateDrugLogLevel(ybId);

            responseData.AddData(bResult);
            return responseData;
        }

        /// <summary>
        /// 更新项目目录级别
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData M_UpdateFeeItemLogLevel()
        {
            int ybId = requestData.GetData<int>(0);

            bool bResult = NewDao<IMatchInterface>().M_UpdateFeeItemLogLevel(ybId);

            responseData.AddData(bResult);
            return responseData;
        }

        #region HIS库中医保数据操作
        /// <summary>
        /// 清空医保数据
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData M_DeleteMILog()
        {
            int ybId = requestData.GetData<int>(0);
            bool bResult = NewDao<IMatchInterface>().M_DeleteMILog(ybId);
            responseData.AddData(bResult);
            return responseData;
        }
        /// <summary>
        /// 保存医保目录
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData M_SaveMILog()
        {
            DataTable dt = requestData.GetData<DataTable>(0);
            int ybId = requestData.GetData<int>(1);

            bool bResult = NewDao<IMatchInterface>().M_SaveMILog(dt, ybId);
            responseData.AddData(bResult);
            return responseData;
        }
        #endregion
    }
}
