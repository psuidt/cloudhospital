using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.IPManage;
using HIS_IPManage.Dao;
using HIS_IPManage.ObjectModel;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPManage.WcfController
{
    /// <summary>
    /// 病床维护控制器
    /// </summary>
    [WCFController]
    public class WardMaintenanceController: WcfServerController
    {
        /// <summary>
        /// 获取床位费用列表
        /// </summary>
        /// <returns>床位费用列表</returns>
        [WCFMethod]
        public ServiceResponseData GetSimpleFeeItemData()
        {
            List<SimpleFeeItemObject> feeItemDataList = NewObject<FeeItemDataSource>().GetSimpleFeeItemData(FeeClass.收费项目);
            DataTable feeItemDataDt = ConvertExtend.ToDataTable(feeItemDataList);
            responseData.AddData(feeItemDataDt);
            return responseData;
        }

        /// <summary>
        /// 保存床位以及床位费用数据
        /// </summary>
        /// <returns>true:保存成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))] 
        public ServiceResponseData SaveBedInfo()
        {
            IP_BedInfo ipBedInfo = requestData.GetData<IP_BedInfo>(0);
            DataTable bedFree = requestData.GetData<DataTable>(1);
            bool isAddBed = requestData.GetData<bool>(2);
            string msg = NewObject<BedManagement>().SaveBedInfo(ipBedInfo, bedFree, isAddBed);
            responseData.AddData(msg);
            return responseData;
        }

        /// <summary>
        /// 根据病区ID获取床位列表
        /// </summary>
        /// <returns>床位列表</returns>
        [WCFMethod]
        public ServiceResponseData GetBedList()
        {
            int wardId = requestData.GetData<int>(0);
            DataTable dt = NewDao<IIPManageDao>().GetBedList(wardId);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 取得病区列表
        /// </summary>
        /// <returns>病区列表</returns>
        [WCFMethod]
        public ServiceResponseData GetWardDept()
        {
            DataTable basicDataDt = NewObject<BasicDataManagement>().GetBasicData(DeptDataSourceType.住院病区, true);
            responseData.AddData(basicDataDt);
            return responseData;
        }

        /// <summary>
        /// 根据床位ID获取费用列表
        /// </summary>
        /// <returns>费用列表</returns>
        [WCFMethod]
        public ServiceResponseData GetBedFreeList()
        {
            int bedID = requestData.GetData<int>(0);
            DataTable dt = NewDao<IIPManageDao>().GetBedFreeList(bedID);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 停用或启用床位
        /// </summary>
        /// <returns>错误消息</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData StoppedOrEnabledBed()
        {
            int isStoped = requestData.GetData<int>(0);// 状态
            int bedID = requestData.GetData<int>(1);// 床位ID
            string msg = NewObject<BedManagement>().StoppedOrEnabledBed(isStoped, bedID);
            responseData.AddData(msg);
            return responseData;
        }
    }
}
