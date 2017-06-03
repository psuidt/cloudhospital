using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.MaterialManage;
using HIS_MaterialManager.Dao;
using HIS_MaterialManager.ObjectModel.BaseData;
using HIS_MaterialManager.ObjectModel.Bill;

namespace HIS_MaterialManage.WcfController
{
    /// <summary>
    /// 采购计划控制器
    /// </summary>
    [WCFController]
    public class MaterialPurchaseController : WcfServerController
    {
        /// <summary>
        /// 获取物资数据
        /// </summary>
        /// <returns>物资数据集</returns>
        [WCFMethod]
        public ServiceResponseData GetWareRoomData()
        {
            DataTable dt = NewObject<MaterialDeptMgr>().GetMaterialDept();
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
            IMwBill iProcess = NewObject<MwBillFactory>().GetBillProcess(opType);
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
            IMwBill iProcess = NewObject<MwBillFactory>().GetBillProcess(opType);
            DataTable dtRtn = iProcess.LoadDetails(queryCondition);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 取得物资字典选择卡片数据
        /// </summary>
        /// <returns>物资字典信息</returns>
        [WCFMethod]
        public ServiceResponseData GetMaterialDicShowCard()
        {
            DataTable dtRtn = NewDao<IMWDao>().GetMaterialDicShowCard(false,0);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 取得小于下限的库存物资
        /// </summary>
        /// <returns>物资数据</returns>
        [WCFMethod]
        public ServiceResponseData GetLessLowerLimitData()
        {
            int deptId = requestData.GetData<int>(0);
            DataTable dtRtn = NewDao<IMWDao>().GetLessLowerLimitData(deptId);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 取得小于上限的库存物资
        /// </summary>
        /// <returns>物资数据</returns>
        [WCFMethod]
        public ServiceResponseData GetLessUpperLimitData()
        {
            int deptId = requestData.GetData<int>(0);
            DataTable dtRtn = NewDao<IMWDao>().GetLessUpperLimitData(deptId);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 保存采购计划单据
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData SaveBill()
        {
            string busiType = requestData.GetData<string>(0);
            IMwBill iProcess = NewObject<MwBillFactory>().GetBillProcess(busiType);
            MW_PlanHead head = requestData.GetData<MW_PlanHead>(1);
            List<MW_PlanDetail> details = requestData.GetData<List<MW_PlanDetail>>(2);
            List<int> deleteDetails = requestData.GetData<List<int>>(3);
            oleDb.BeginTransaction();
            try
            {
                foreach (int detailID in deleteDetails)
                {
                    NewObject<MW_PlanDetail>().delete(detailID);
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
                IMwBill iProcess = NewObject<MwBillFactory>().GetBillProcess(busiType);
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
                MW_PlanHead head = requestData.GetData<MW_PlanHead>(0);
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