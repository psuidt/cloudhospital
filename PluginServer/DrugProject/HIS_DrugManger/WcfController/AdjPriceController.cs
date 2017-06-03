using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_DrugManger.Dao;
using HIS_DrugManger.ObjectModel.BaseData;
using HIS_DrugManger.ObjectModel.Bill;
using HIS_Entity.DrugManage;

namespace HIS_DrugManger.WcfController
{
    /// <summary>
    /// 药品调价控制器
    /// </summary>
    [WCFController]
    public class AdjPriceController : WcfServerController
    {
        /// <summary>
        /// 获取药剂科室列表用于选择
        /// </summary>
        /// <returns>药剂科室数据</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugDeptList()
        {
            int deptType = requestData.GetData<int>(0);
            DataTable dt = NewObject<DrugDeptMgr>().GetDrugDeptList(deptType);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 读取表头记录列表
        /// </summary>
        /// <returns>调价表头数据</returns>
        [WCFMethod]
        public ServiceResponseData LoadAdjHead()
        {
            var queryCondition = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewDao<IDGDao>().LoadAdjHead(queryCondition);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 读取表详细记录列表
        /// </summary>
        /// <returns>调价明细数据</returns>
        [WCFMethod]
        public ServiceResponseData LoadAdjDetail()
        {
            var queryCondition = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewDao<IDGDao>().LoadAdjDetail(queryCondition);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取库存药品信息
        /// </summary>
        /// <returns>库存药品信息</returns>
        [WCFMethod]
        public ServiceResponseData GetStoreDrugInFo()
        {
            int deptID = requestData.GetData<int>(0);
            DataTable dtRtn = NewDao<IDGDao>().GetStoreDrugInFo(deptID);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 执行调价单
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData ExcutePrice()
        {
            List<DG_AdjDetail> details = requestData.GetData<List<DG_AdjDetail>>(0);
            DG_AdjHead head = requestData.GetData<DG_AdjHead>(1);
            int workId= requestData.GetData<int>(2);
            DGBillResult result = NewObject<DGAdjPriceBill>().SaveBill(head, details, workId);
            responseData.AddData(result);
            return responseData;
        }
    }
}
