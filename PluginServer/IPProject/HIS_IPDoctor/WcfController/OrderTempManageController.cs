using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.ClinicManage;
using HIS_IPDoctor.Dao;
using HIS_IPDoctor.ObjectModel;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPDoctor.WcfController
{
    /// <summary>
    /// 医嘱模板控制器
    /// </summary>
    [WCFController]
    public class OrderTempManageController : WcfServerController
    {
        /// <summary>
        /// 获取医嘱模板列表
        /// </summary>
        /// <returns>模板列表</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData GetOrderTempList()
        {
            int tempLevel = requestData.GetData<int>(0);
            int deptId = requestData.GetData<int>(1);
            int empId = requestData.GetData<int>(2);
            List<IPD_OrderModelHead> orderTempList = NewObject<OrderTempManage>().GetOrderTempList(tempLevel, deptId, empId);
            responseData.AddData(orderTempList);
            return responseData;
        }

        /// <summary>
        /// 保存模板数据
        /// </summary>
        /// <returns>保存后的模板头ID</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveOrderTemplateHead()
        {
            IPD_OrderModelHead orderTemp = requestData.GetData<IPD_OrderModelHead>(0);
            this.BindDb(orderTemp);
            orderTemp.save();
            responseData.AddData(true);
            responseData.AddData(orderTemp.ModelHeadID);
            return responseData;
        }

        /// <summary>
        /// 删除医嘱模板
        /// </summary>
        /// <returns>ResponseData</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData DelOrderTemp()
        {
            IPD_OrderModelHead orderTemp = requestData.GetData<IPD_OrderModelHead>(0);
            // 删除模板头数据
            this.BindDb(orderTemp);
            orderTemp.delete();
            //{
            //    // 如果删除的是模板分类则删除模板分类下所有的模板
            //    // 1.删除分类下所有模板的明细数据
            //    NewDao<IOrderTempManageDao>().DelOrderTempDetails(OrderTemp.ModelHeadID,true);
            //    // 2.删除分类系所有的模板头数据
            //    NewDao<IOrderTempManageDao>().DelOrderTempByModelType(OrderTemp.ModelHeadID);
            //}
            //else
            if (orderTemp.ModelType == 1)
            {
                // 删除模板明细数据
                NewDao<IOrderTempManageDao>().DelOrderTempDetails(orderTemp.ModelHeadID, false);
            }

            return responseData;
        }

        /// <summary>
        /// 获取医嘱ShowCard基础数据
        /// </summary>
        /// <returns>ShowCard基础数据</returns>
        [WCFMethod]
        public ServiceResponseData GetMasterData()
        {
            // 获取药品项目等数据源
            DataTable dtDrugItem = NewObject<FeeItemDataSource>().GetFeeItemDataDt(FeeBusinessType.医嘱业务);
            responseData.AddData(dtDrugItem);

            // 获取用法数据源
            List<Basic_Channel> list = NewObject<Basic_Channel>().getlist<Basic_Channel>(" DelFlag=0 and InUsed=1");
            responseData.AddData(list);

            // 获取频次数据源
            List<Basic_Frequency> listFre = NewObject<Basic_Frequency>().getlist<Basic_Frequency>(" DelFlag=0");
            responseData.AddData(listFre);

            // 获取嘱托数据源
            List<Basic_Entrust> listEntrust = NewObject<Basic_Entrust>().getlist<Basic_Entrust>(" DelFlag=0");
            responseData.AddData(listEntrust);
            return responseData;
        }

        /// <summary>
        /// 刷新药品基础数据
        /// </summary>
        /// <returns>药品基础数据</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugMaster()
        {
            // 获取药品项目等数据源
            DataTable dtDrugItem = NewObject<FeeItemDataSource>().GetFeeItemDataDt(FeeBusinessType.医嘱业务);
            responseData.AddData(dtDrugItem);
            return responseData;
        }

        /// <summary>
        /// 获取模板明细数据
        /// </summary>
        /// <returns>模板明细数据</returns>
        [WCFMethod]
        public ServiceResponseData GetOrderTempDetail()
        {
            int modelHeadID = requestData.GetData<int>(0);
            DataTable orderTempDetail = NewDao<IOrderTempManageDao>().GetOrderTempDetail(modelHeadID);
            responseData.AddData(orderTempDetail);
            return responseData;
        }

        /// <summary>
        /// 保存医嘱模板明细数据
        /// </summary>
        /// <returns>bool</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveOrderDetailsData()
        {
            DataTable longOrder = requestData.GetData<DataTable>(0);
            DataTable tempOrder = requestData.GetData<DataTable>(1);
            bool result = NewObject<OrderTempManage>().SaveOrderDetailsData(longOrder, tempOrder);
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 删除模板明细数据
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData DelOrderDetailsData()
        {
            string modelDetailID = requestData.GetData<string>(0);
            NewDao<IOrderTempManageDao>().DelOrderDetailsData(modelDetailID);
            responseData.AddData(true);
            return responseData;
        }
    }
}
