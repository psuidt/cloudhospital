using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_DrugManger.Dao;
using HIS_DrugManger.ObjectModel.Bill;
using HIS_Entity.DrugManage;

namespace HIS_DrugManger.WcfController
{
    /// <summary>
    /// 采购计划控制器
    /// </summary>
    [WCFController]
    public class PurchaseController: WcfServerController
    {
        /// <summary>
        /// 获取药库数据
        /// </summary>
        /// <returns>药库数据集</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugWareRoomData()
        {
            DataTable dt =NewObject<DG_DeptDic>().gettable("DeptType=1 AND StopFlag=0");
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 加载采购计划单表头
        /// </summary>
        /// <returns>采购计划单表头信息</returns>
        [WCFMethod]
        public ServiceResponseData GetPlanHeadData()
        {
            var opType = requestData.GetData<string>(0);
            var queryCondition = requestData.GetData<Dictionary<string, string>>(1);
            IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(opType);
            DataTable dtRtn = iProcess.LoadHead(queryCondition);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 加载采购计划单明细
        /// </summary>
        /// <returns>采购计划单明细信息</returns>
        [WCFMethod]
        public ServiceResponseData GetPlanDetailData()
        {
            var opType = requestData.GetData<string>(0);
            var queryCondition = requestData.GetData<Dictionary<string, string>>(1);
            IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(opType);
            DataTable dtRtn = iProcess.LoadDetails(queryCondition);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 取得药品字典选择卡片数据
        /// </summary>
        /// <returns>药品字典信息</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugDicShowCard()
        {
            DataTable dtRtn = NewDao<IDGDao>().GetDrugDicShowCard();
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 取得小于下限的库存药品
        /// </summary>
        /// <returns>药品数据</returns>
        [WCFMethod]
        public ServiceResponseData GetLessLowerLimitDrugData()
        {
            int deptId = requestData.GetData<int>(0);
            DataTable dtRtn = NewDao<IDGDao>().GetLessLowerLimitDrugData(deptId);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 取得小于上限的库存药品
        /// </summary>
        /// <returns>药品数据</returns>
        [WCFMethod]
        public ServiceResponseData GetLessUpperLimitDrugData()
        {
            int deptId = requestData.GetData<int>(0);
            DataTable dtRtn = NewDao<IDGDao>().GetLessUpperLimitDrugData(deptId);
            responseData.AddData(dtRtn);
            return responseData;
        }
        
        /// <summary>
        /// 保存采购计划单据
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData SaveBill()
        {
            string busiType = requestData.GetData<string>(0);
            IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(busiType);
            DW_PlanHead head = requestData.GetData<DW_PlanHead>(1);
            List<DW_PlanDetail> details = requestData.GetData<List<DW_PlanDetail>>(2);
            List<int> deleteDetails = requestData.GetData<List<int>>(3);
            oleDb.BeginTransaction();
            try
            {
                foreach (int detailID in deleteDetails)
                {
                    NewObject<DW_PlanDetail>().delete(detailID);
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

        /// <summary>
        /// 删除采购单据
        /// </summary>
        /// <returns>成功返回true，失败返回false</returns>
        [WCFMethod]
        public ServiceResponseData DeleteBill()
        {
            try
            {
                string busiType = requestData.GetData<string>(0);
                int billID = requestData.GetData<int>(1);
                IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(busiType);
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
        /// 审核采购单据
        /// </summary>
        /// <returns>成功返回true，失败返回false</returns>
        [WCFMethod]
        public ServiceResponseData AuditBill()
        {
            try
            {
                DW_PlanHead head = requestData.GetData<DW_PlanHead>(0);
                head.AuditTime = System.DateTime.Now;
                this.BindDb(head);
                head.save();
                responseData.AddData(true);
            }
            catch (Exception error)
            {
                responseData.AddData(false);
                responseData.AddData(error.Message);
            }

            return responseData;
        }
    }
}
