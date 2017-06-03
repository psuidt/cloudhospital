﻿using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.MaterialManage;
using HIS_MaterialManager.Dao;
using HIS_MaterialManager.ObjectModel.BaseData;
using HIS_MaterialManager.ObjectModel.Bill;

namespace HIS_MaterialManage.WcfController
{
    /// <summary>
    /// 物资盘点控制器
    /// </summary>
    [WCFController]
    public class MaterialCheckStoreController : WcfServerController
    {
        /// <summary>
        /// 获取物资科室列表用于选择
        /// </summary>
        /// <returns>返回值</returns>
        [WCFMethod]
        public ServiceResponseData GetMaterialDeptList()
        {
            int deptType = requestData.GetData<int>(0);
            DataTable dt = NewObject<MaterialDeptMgr>().GetMaterialDept();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 加载盘点单表头
        /// </summary>
        /// <returns>盘点单表头信息</returns>
        [WCFMethod]
        public ServiceResponseData LoadBillHead()
        {
            var opType = requestData.GetData<string>(0);
            var queryCondition = requestData.GetData<Dictionary<string, string>>(1);
            IMwBill iProcess = NewObject<MwBillFactory>().GetBillProcess(opType);
            DataTable dtRtn = iProcess.LoadHead(queryCondition);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 保存盘点单据
        /// </summary>
        /// <returns>保存结果</returns>
        [WCFMethod]
        public ServiceResponseData SaveBill()
        {
            string belongSys = requestData.GetData<string>(0);
            string busiType = requestData.GetData<string>(1);
            IMwBill iProcess = NewObject<MwBillFactory>().GetBillProcess(busiType);

            MW_CheckHead head = requestData.GetData<MW_CheckHead>(2);
            List<MW_CheckDetail> details = requestData.GetData<List<MW_CheckDetail>>(3);
            List<int> deleteDetails = requestData.GetData<List<int>>(4);
            oleDb.BeginTransaction();
            try
            {
                foreach (int detailID in deleteDetails)
                {
                    NewObject<MW_CheckDetail>().delete(detailID);
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
        /// 删除盘点单据
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
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
                throw error;
            }

            return responseData;
        }

        /// <summary>
        /// 清除盘点状态
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData ClearCheckStatus()
        {
            try
            {
                int deptID = requestData.GetData<int>(0);
                NewObject<MwCheckBill>().ClearCheckStatus(deptID);
                responseData.AddData(true);
            }
            catch (Exception error)
            {
                responseData.AddData(false);
                responseData.AddData(error.Message);
                throw error;
            }

            return responseData;
        }

        /// <summary>
        /// 加载盘点单明细
        /// </summary>
        /// <returns>盘点单明细信息</returns>
        [WCFMethod]
        public ServiceResponseData LoadBillDetails()
        {
            var opType = requestData.GetData<string>(0);
            var queryCondition = requestData.GetData<Dictionary<string, string>>(1);
            IMwBill iProcess = NewObject<MwBillFactory>().GetBillProcess(opType);
            DataTable dtRtn = iProcess.LoadDetails(queryCondition);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 获取可盘点物资信息
        /// </summary>
        /// <returns>可盘点物资信息</returns>
        [WCFMethod]
        public ServiceResponseData GetCheckDrugInfo()
        {
            string belongSys = requestData.GetData<string>(0);
            string busiType = requestData.GetData<string>(1);
            int deptID = requestData.GetData<int>(2);
            DataTable dtRtn = new DataTable();
            dtRtn = NewDao<IMWDao>().GetDrugDicForCheckShowCard(deptID);            
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 获取物资批次信息
        /// </summary>
        /// <returns>物资批次信息</returns>
        [WCFMethod]
        public ServiceResponseData GetInStoreBatchInfo()
        {
            string belongSys = requestData.GetData<string>(0);
            int deptID = requestData.GetData<int>(1);
            DataTable dtRtn = null;
            dtRtn = NewDao<IMWDao>().GetBatchForInstoreShowCard(deptID);            
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 获取修改物资单据表头实体
        /// </summary>
        /// <returns>盘点单表头实体</returns>
        [WCFMethod]
        public ServiceResponseData GetEditBillHead()
        {
            string belongSys = requestData.GetData<string>(0);
            int billID = requestData.GetData<int>(1);
            MW_CheckHead head = (MW_CheckHead)NewObject<MW_CheckHead>().getmodel(billID);
            responseData.AddData(head);
            return responseData;
        }

        /// <summary>
        /// 设置物资科室盘点状态
        /// </summary>
        /// <returns>处理啊结果</returns>
        [WCFMethod]
        public ServiceResponseData SetCheckStatus()
        {
            try
            {
                int deptId = requestData.GetData<int>(0);
                int status = requestData.GetData<int>(1);
                NewDao<IMWDao>().SetCheckStatus(deptId, status);
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
        /// 提取库存物资
        /// </summary>
        /// <returns>盘点单空表数据</returns>
        [WCFMethod]
        public ServiceResponseData LoadStorageData()
        {
            DataTable dtRtn = new DataTable();
            var opType = requestData.GetData<string>(0);
            var queryCondition = requestData.GetData<Dictionary<string, string>>(1);
            dtRtn = NewObject<MwCheckBill>().LoadStorageData(queryCondition);                   
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 取得物资一级类型
        /// </summary>
        /// <returns>物资一级类型数据</returns>
        [WCFMethod]
        public ServiceResponseData GetMaterialType()
        {
            DataTable dtRtn = NewDao<IMWDao>().GetMaterialType();
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 取得盘点审核待审的状态信息
        /// 库房状态，待审单据数
        /// </summary>
        /// <returns>库房状态，待审单据数</returns>
        [WCFMethod]
        public ServiceResponseData CheckStatusInfos()
        {
            var opType = requestData.GetData<string>(0);
            int deptId = requestData.GetData<int>(1);
            string showMessage = string.Empty;
            string status = string.Empty;
            string notAuditBillCnt = "0";

            DataTable dt = NewObject<MW_DeptDic>().gettable("DeptID=" + deptId + "  and WorkID=" + oleDb.WorkId);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["CheckStatus"].ToString() == "0")
                {
                    status = "运营中";
                }
                else
                {
                    status = "盘点中";
                }
            }

            DataTable dtNotAuditBillCnt = NewObject<MW_CheckHead>().gettable("DelFlag=0 AND AuditFlag=0 AND DeptID=" + deptId + " AND WorkID=" + oleDb.WorkId);
            if (dtNotAuditBillCnt != null && dtNotAuditBillCnt.Rows.Count > 0)
            {
                notAuditBillCnt = dtNotAuditBillCnt.Rows.Count.ToString();
            }

            showMessage = string.Format("库房状态：{0}；未审单据数：{1}张", status, notAuditBillCnt);
            responseData.AddData(showMessage);
            return responseData;
        }

        #region 盘点审核
        /// <summary>
        /// 加载盘点审核单表头
        /// </summary>
        /// <returns>盘点审核单表头</returns>
        [WCFMethod]
        public ServiceResponseData LoadAudtiCheckHead()
        {
            DataTable dtRtn = new DataTable();
            var opType = requestData.GetData<string>(0);//单据类型
            var queryCondition = requestData.GetData<Dictionary<string, string>>(1);
            dtRtn = NewObject<MwCheckBill>().LoadAudtiCheckHead(queryCondition);           
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 加载盘点审核单明细
        /// </summary>
        /// <returns>盘点审核单明细</returns>
        [WCFMethod]
        public ServiceResponseData LoadAuditCheckDetail()
        {
            DataTable dtRtn = new DataTable();
            var opType = requestData.GetData<string>(0);//单据类型
            var queryCondition = requestData.GetData<Dictionary<string, string>>(1);

            //药库
            if (opType == MWConstant.OP_MW_CHECK)
            {
                dtRtn = NewObject<MwCheckBill>().LoadAuditCheckDetail(queryCondition);
            }  
                    
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 汇总所有未审核单据明细并提取
        /// </summary>
        /// <returns>汇总盘点信息</returns>
        [WCFMethod]
        public ServiceResponseData LoadAllNotAuditDetail()
        {            
            DataTable dtRtn = new DataTable();
            var opType = requestData.GetData<string>(0);//单据类型
            var queryCondition = requestData.GetData<Dictionary<string, string>>(1);
            //物资库
            if (opType == MWConstant.OP_MW_CHECK)
            {
                dtRtn = NewObject<MwCheckBill>().LoadAllNotAuditDetail(queryCondition,true);
            }  
                     
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 审核盘点单
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData AuditBill()
        {
            string busiType = requestData.GetData<string>(0);
            int deptId = requestData.GetData<int>(1);
            int auditEmpID = requestData.GetData<int>(2);
            string auditEmpName = requestData.GetData<string>(3);
            IMwBill iProcess = NewObject<MwBillFactory>().GetBillProcess(busiType);
            MWBillResult rtn = new MWBillResult();
            try
            {
                rtn = iProcess.AuditBill(deptId, auditEmpID, auditEmpName);
                responseData.AddData(rtn);
            }
            catch (Exception error)
            {
                rtn.ErrMsg = error.Message;
                rtn.Result = 2;
                responseData.AddData(rtn);
                throw error;
            }

            return responseData;
        }
        #endregion
    }
}
