using System;
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
    /// 药品盘点控制器
    /// </summary>
    [WCFController]
    public class CheckStoreController : WcfServerController
    {
        /// <summary>
        /// 获取药剂科室列表用于选择
        /// </summary>
        /// <returns>返回值</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugDeptList()
        {
            int deptType = requestData.GetData<int>(0);
            DataTable dt = NewObject<DrugDeptMgr>().GetDrugDeptList(deptType);
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
            DataTable dt = NewObject<DrugSupplyMgr>().GetSupplyForShowCard();
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
            IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(opType);
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
            IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(busiType);
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                DW_CheckHead head = requestData.GetData<DW_CheckHead>(2);
                List<DW_CheckDetail> details = requestData.GetData<List<DW_CheckDetail>>(3);
                List<int> deleteDetails = requestData.GetData<List<int>>(4);
                oleDb.BeginTransaction();
                try
                {
                    foreach (int detailID in deleteDetails)
                    {
                        NewObject<DW_CheckDetail>().delete(detailID);
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
            }
            else
            {
                DS_CheckHead head = requestData.GetData<DS_CheckHead>(2);
                List<DS_CheckDetail> details = requestData.GetData<List<DS_CheckDetail>>(3);
                List<int> deleteDetails = requestData.GetData<List<int>>(4);
                oleDb.BeginTransaction();
                try
                {
                    foreach (int detailID in deleteDetails)
                    {
                        NewObject<DS_CheckDetail>().delete(detailID);
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
                IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(busiType);
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
                string busiType = requestData.GetData<string>(0);
                int deptID = requestData.GetData<int>(1);
                if (busiType == DGConstant.OP_DS_CHECK)
                {
                    NewObject<DSCheckBill>().ClearCheckStatus(deptID);
                }
                else
                {
                    NewObject<DWCheckBill>().ClearCheckStatus(deptID);
                }

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
            IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(opType);
            DataTable dtRtn = iProcess.LoadDetails(queryCondition);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 获取可盘点药品信息
        /// </summary>
        /// <returns>可盘点药品信息</returns>
        [WCFMethod]
        public ServiceResponseData GetCheckDrugInfo()
        {
            string belongSys = requestData.GetData<string>(0);
            string busiType = requestData.GetData<string>(1);
            int deptID = requestData.GetData<int>(2);
            DataTable dtRtn = new DataTable();
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                dtRtn = NewDao<IDWDao>().GetDrugDicForCheckShowCard(deptID);
            }
            else
            {
                dtRtn = NewDao<IDSDao>().GetDrugDicForCheckShowCard(deptID);
            }

            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 获取药品批次信息
        /// </summary>
        /// <returns>药品批次信息</returns>
        [WCFMethod]
        public ServiceResponseData GetInStoreBatchInfo()
        {
            string belongSys = requestData.GetData<string>(0);
            int deptID = requestData.GetData<int>(1);
            DataTable dtRtn = null;
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                dtRtn = NewDao<IDWDao>().GetBatchForInstoreShowCard(deptID);
            }
            else
            {
            }

            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 获取修改药品单据表头实体
        /// </summary>
        /// <returns>盘点单表头实体</returns>
        [WCFMethod]
        public ServiceResponseData GetEditBillHead()
        {
            string belongSys = requestData.GetData<string>(0);
            int billID = requestData.GetData<int>(1);
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                DW_CheckHead head = (DW_CheckHead)NewObject<DW_CheckHead>().getmodel(billID);
                responseData.AddData(head);
            }
            else
            {
                DS_CheckHead head = (DS_CheckHead)NewObject<DS_CheckHead>().getmodel(billID);
                responseData.AddData(head);
            }

            return responseData;
        }

        /// <summary>
        /// 设置药剂科室盘点状态
        /// </summary>
        ///<returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData SetCheckStatus()
        {
            try
            {
                int deptId = requestData.GetData<int>(0);
                int status = requestData.GetData<int>(1);
                NewDao<IDGDao>().SetCheckStatus(deptId, status, 0);
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
        /// 提取库存药品
        /// </summary>
        /// <returns>库存药品</returns>
        [WCFMethod]
        public ServiceResponseData LoadStorageData()
        {
            DataTable dtRtn = new DataTable();
            var opType = requestData.GetData<string>(0);
            var queryCondition = requestData.GetData<Dictionary<string, string>>(1);
            if (opType == DGConstant.OP_DW_CHECK)
            {
                //药库
                dtRtn = NewObject<DWCheckBill>().LoadStorageData(queryCondition);
            }
            else
            {
                //药房
                dtRtn = NewObject<DSCheckBill>().LoadStorageData(queryCondition);
            }

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
            if (opType == DGConstant.OP_DW_CHECK)
            {
                //药库
                DataTable dt = NewObject<DG_DeptDic>().gettable("DeptID=" + deptId + " and DeptType=1 and WorkID=" + oleDb.WorkId);
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

                DataTable dtNotAuditBillCnt = NewObject<DW_CheckHead>().gettable("DelFlag=0 AND AuditFlag=0 AND DeptID=" + deptId + " AND WorkID=" + oleDb.WorkId);
                if (dtNotAuditBillCnt != null && dtNotAuditBillCnt.Rows.Count > 0)
                {
                    notAuditBillCnt = dtNotAuditBillCnt.Rows.Count.ToString();
                }
            }
            else
            {
                //药房
                DataTable dt = NewObject<DG_DeptDic>().gettable("DeptID=" + deptId + " and DeptType=0 and WorkID=" + oleDb.WorkId);
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

                DataTable dtNotAuditBillCnt = NewObject<DS_CheckHead>().gettable("DelFlag=0 AND AuditFlag=0 AND DeptID=" + deptId + " AND WorkID=" + oleDb.WorkId);
                if (dtNotAuditBillCnt != null && dtNotAuditBillCnt.Rows.Count > 0)
                {
                    notAuditBillCnt = dtNotAuditBillCnt.Rows.Count.ToString();
                }
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
            if (opType == DGConstant.OP_DW_CHECK)
            {
                //药库
                dtRtn = NewObject<DWCheckBill>().LoadAudtiCheckHead(queryCondition);
            }
            else
            {
                //药房
                dtRtn = NewObject<DSCheckBill>().LoadAudtiCheckHead(queryCondition);
            }

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
            if (opType == DGConstant.OP_DW_CHECK)
            {
                //药库
                dtRtn = NewObject<DWCheckBill>().LoadAuditCheckDetail(queryCondition);
            }
            else
            {
                //药房
                dtRtn = NewObject<DSCheckBill>().LoadAuditCheckDetail(queryCondition);
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
            if (opType == DGConstant.OP_DW_CHECK)
            {
                //药库
                dtRtn = NewObject<DWCheckBill>().LoadAllNotAuditDetail(queryCondition, true);
            }
            else
            {
                //药房
                dtRtn = NewObject<DSCheckBill>().LoadAllNotAuditDetail(queryCondition, true);
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
            IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(busiType);
            DGBillResult rtn = new DGBillResult();
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
