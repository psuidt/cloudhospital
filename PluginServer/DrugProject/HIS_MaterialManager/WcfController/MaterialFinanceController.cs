using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.MaterialManage;
using HIS_MaterialManager.Dao;
using HIS_MaterialManager.ObjectModel.Account;

namespace HIS_MaterialManage.WcfController
{
    /// <summary>
    /// 财务管理控制器（月结、付款）
    /// </summary>
    [WCFController]
    public class MaterialFinanceController : WcfServerController
    {
        /// <summary>
        /// 获取药剂科室列表用于选择
        /// </summary>
        /// <returns>返回值</returns>
        [WCFMethod]
        public ServiceResponseData GetMaterialDeptList()
        {
            int deptType = requestData.GetData<int>(0);
            DataTable dt = NewDao<IMWDao>().GetMaterialDept();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取供应商列表用于绑定
        /// </summary>
        /// <returns>供应商列表</returns>
        [WCFMethod]
        public ServiceResponseData GetSupplyForShowCard()
        {
            DataTable dt = NewDao<IMWDao>().GetSupplyForShowCard();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 加载入库单表头
        /// </summary>
        /// <returns>入库单表头信息</returns>
        [WCFMethod]
        public ServiceResponseData LoadBillHead()
        {
            var queryCondition = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dtRtn = NewDao<IMWDao>().LoadInStoreHead(queryCondition);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 加载入库单明细
        /// </summary>
        /// <returns>入库单明细信息</returns>
        [WCFMethod]
        public ServiceResponseData LoadBillDetails()
        {
            var queryCondition = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dtRtn = NewDao<IMWDao>().LoadInStoreDetail(queryCondition);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 付款操作
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData BillPay()
        {
            var payrecord = requestData.GetData<MW_PayRecord>(0);
            var inHeadID = requestData.GetData<string>(1);
            this.BindDb(payrecord);
            int result = payrecord.save();
            if (result > 0)
            {
                result = NewDao<IMWDao>().UpdateStoreHead(inHeadID, payrecord.InvoiceNO, payrecord.PayTime, payrecord.PayRecordID, 1);
            }

            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 取消付款操作
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData CancelBillPay()
        {
            var payRecordID = requestData.GetData<string>(0);
            int result = NewDao<IMWDao>().UpdatePayRecord(payRecordID);
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 加载付款记录信息
        /// </summary>
        /// <returns>付款信息数据</returns>
        [WCFMethod]
        public ServiceResponseData LoadPayRecord()
        {
            var queryCondition = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dtRtn = NewDao<IMWDao>().LoadPayRecord(queryCondition);
            responseData.AddData(dtRtn);
            return responseData;
        }
    }
}
