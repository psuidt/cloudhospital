using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_DrugManger.ObjectModel.BaseData;
using HIS_DrugManger.ObjectModel.Bill;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManger.WcfController
{
    /// <summary>
    /// 领药申请控制器
    /// </summary>
    [WCFController]
    public class ApplyStoreController : WcfServerController
    {
        /// <summary>
        /// 月结操作
        /// </summary>
        /// <returns>月结记录</returns>
        [WCFMethod]
        public ServiceResponseData GetWareHourse()
        {
            List<Tuple<string, string, SqlOperator>> andWhere = requestData.GetData<List<Tuple<string, string, SqlOperator>>>(0);
            DataTable dt = NewObject<DrugDeptMgr>().GetDeptDicData(andWhere);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 科室可以出库的药品
        /// </summary>
        /// <returns>出库药品</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptOutDrug()
        {
            int deptId = requestData.GetData<int>(0);
            string bussType = requestData.GetData<string>(1);
            DataTable dt = NewObject<DrugMakerDicMgr>().GetDurgDic(deptId, bussType, 0);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 加载出库单主表
        /// </summary>
        /// <returns>出库单主表</returns>
        [WCFMethod]
        public ServiceResponseData LoadBillHead()
        {
            // var opType = requestData.GetData<string>(0);
            var andW = requestData.GetData<Dictionary<string, string>>(1);
            IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(DGConstant.OP_DS_APPLYPLAN);
            DataTable dtRtn = iProcess.LoadHead(andW);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 加载出库单明细
        /// </summary>
        /// <returns>出库单明细</returns>
        [WCFMethod]
        public ServiceResponseData LoadBillDetial()
        {
            // var opType = requestData.GetData<string>(0);
            var andW = requestData.GetData<Dictionary<string, string>>(1);
            IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(DGConstant.OP_DS_APPLYPLAN);
            DataTable dtRtn = iProcess.LoadDetails(andW);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 删除申请单单
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData DeleteBill()
        {
            try
            {
                int billID = requestData.GetData<int>(0);
                IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(DGConstant.OP_DS_APPLYPLAN);
                iProcess.DeleteBill(billID);
                responseData.AddData(true);
            }
            catch (Exception error)
            {
                responseData.AddData(false);
                responseData.AddData(error.Message);
            }

            return responseData;
        }

        /// <summary>
        /// 获取修改药品单据表头实体
        /// </summary>
        /// <returns>出库单表头实体</returns>
        [WCFMethod]
        public ServiceResponseData GetEditBillHead()
        {
            int billID = requestData.GetData<int>(0);
            DS_ApplyHead head = (DS_ApplyHead)NewObject<DS_ApplyHead>().getmodel(billID);
            responseData.AddData(head);
            return responseData;
        }

        /// <summary>
        /// 药库药房出库 保存出库单据
        /// </summary>
        /// <returns>保存结果</returns>
        [WCFMethod]
        public ServiceResponseData SaveBill()
        {
            string belongSys = requestData.GetData<string>(0);
            DS_ApplyHead head = requestData.GetData<DS_ApplyHead>(1);
            IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(DGConstant.OP_DS_APPLYPLAN);
            List<DS_ApplyDetail> details = requestData.GetData<List<DS_ApplyDetail>>(2);
            List<int> deleteDetails = requestData.GetData<List<int>>(3);
            oleDb.BeginTransaction();
            try
            {
                foreach (int detailID in deleteDetails)
                {
                    NewObject<DS_ApplyDetail>().delete(detailID);
                }

                iProcess.SaveBill(head, details);
                oleDb.CommitTransaction();
                responseData.AddData(true);
            }
            catch (Exception error)
            {
                oleDb.RollbackTransaction();
                responseData.AddData(false);
                responseData.AddData(error.Message);
            }

            return responseData;
        }
    }
}
