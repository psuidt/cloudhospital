using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_DrugManger.Dao;
using HIS_DrugManger.ObjectModel.BaseData;
using HIS_DrugManger.ObjectModel.Bill;
using HIS_Entity.BasicData;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManger.WcfController
{
    /// <summary>
    /// 入库处理控制器
    /// </summary>
    [WCFController]
    public class InStoreController: WcfServerController
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
        /// 加载入库单表头
        /// </summary>
        /// <returns>入库单表头信息</returns>
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
        /// 保存入库单据
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData SaveBill()
        {
            string belongSys = requestData.GetData<string>(0);
            string busiType = requestData.GetData<string>(1);
            IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(busiType);      
            DGBillResult result = new DGBillResult();
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                DW_InStoreHead head = requestData.GetData<DW_InStoreHead>(2);
                List<DW_InStoreDetail> details = requestData.GetData<List<DW_InStoreDetail>>(3);
                List<int> deleteDetails = requestData.GetData<List<int>>(4);
                oleDb.BeginTransaction();
                try
                {
                    foreach (int detailID in deleteDetails)
                    {
                        NewObject<DW_InStoreDetail>().delete(detailID);
                    }

                    iProcess.SaveBill(head, details);
                    Basic_SystemConfig config = NewObject<IDGDao>().GetDeptParameters(head.DeptID, "AutoAuditInstore");
                    if (config != null)
                    {
                        //是否配置需要审核
                        if (config.Value == "1")
                        {
                            result = iProcess.AuditBill(head.InHeadID, LoginUserInfo.EmpId,LoginUserInfo.EmpName, LoginUserInfo.WorkId);
                        }
                    }

                    if (result.Result == 0)
                    {
                        oleDb.CommitTransaction();
                    }
                    else
                    {
                        oleDb.RollbackTransaction();
                    }

                    responseData.AddData(result);
                }
                catch (Exception error)
                {
                    oleDb.RollbackTransaction();
                    result.Result = 1;
                    result.ErrMsg = error.Message;
                    responseData.AddData(result);
                }
            }
            else
            {
                DS_InstoreHead head = requestData.GetData<DS_InstoreHead>(2);
                List<DS_InStoreDetail> details = requestData.GetData<List<DS_InStoreDetail>>(3);
                List<int> deleteDetails = requestData.GetData<List<int>>(4);
                oleDb.BeginTransaction();
                try
                {
                    foreach (int detailID in deleteDetails)
                    {
                        NewObject<DS_InStoreDetail>().delete(detailID);
                    }
                    
                    iProcess.SaveBill(head, details);
                    Basic_SystemConfig config = NewObject<IDGDao>().GetDeptParameters(head.DeptID, "AutoAuditInstore");
                    if (config != null)
                    {
                        //是否直接审核
                        if (config.Value == "1")
                        {
                            result = iProcess.AuditBill(head.InHeadID, LoginUserInfo.EmpId,LoginUserInfo.EmpName, LoginUserInfo.WorkId);
                        }
                    }

                    if (result.Result == 0)
                    {
                        oleDb.CommitTransaction();
                    }
                    else
                    {
                        oleDb.RollbackTransaction();
                    }

                    responseData.AddData(result);
                }
                catch (Exception error)
                {
                    oleDb.RollbackTransaction();
                    result.Result = 1;
                    result.ErrMsg = error.Message;
                    responseData.AddData(result);
                }
            }

            return responseData;
        }

        /// <summary>
        /// 删除入库单据
        /// </summary>
        /// <returns>处理结果</returns>
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
        /// 审核入库单据
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData AuditBill()
        {
            string busiType = requestData.GetData<string>(0);
            int billID = requestData.GetData<int>(1);
            int auditEmpID = requestData.GetData<int>(2);
            string auditEmpName = requestData.GetData<string>(3);
            IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(busiType);
            DGBillResult rtn = new DGBillResult();
            try
            {
                oleDb.BeginTransaction();
                rtn = iProcess.AuditBill(billID, auditEmpID, auditEmpName,LoginUserInfo.WorkId);
                if (rtn.Result == 0)
                {
                    oleDb.CommitTransaction();
                }
                else
                {
                    oleDb.RollbackTransaction();
                }
            
                responseData.AddData(rtn);
            }
            catch (Exception error)
            {
                oleDb.RollbackTransaction();
                rtn.ErrMsg = error.Message;
                rtn.Result = 2;
                responseData.AddData(rtn);
            }

            return responseData;          
        }

        /// <summary>
        /// 加载入库单明细
        /// </summary>
        /// <returns>入库单明细信息</returns>
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
        /// 获取可入库药品信息
        /// </summary>
        /// <returns>可入库药品信息</returns>
        [WCFMethod]
        public ServiceResponseData GetInStoreDrugInfo()
        {
            string belongSys = requestData.GetData<string>(0);
            string busiType = requestData.GetData<string>(1);
            int deptID = requestData.GetData<int>(2);
            DataTable dtRtn = new DataTable();
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                if (busiType != DGConstant.OP_DW_BACKSTORE)
                {
                    dtRtn = NewDao<IDWDao>().GetDrugDicForInStoreShowCard(false, deptID);
                }
                else
                {
                    dtRtn = NewDao<IDWDao>().GetDrugDicForInStoreShowCard(true, deptID);
                }
            }
            else
            {
                if (busiType != DGConstant.OP_DS_RETURNSOTRE)
                {
                    dtRtn = NewDao<IDSDao>().GetDrugDicForInStoreShowCard(false, deptID);
                }
                else
                {
                    dtRtn = NewDao<IDSDao>().GetDrugDicForInStoreShowCard(true, deptID);
                }
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
                dtRtn = NewDao<IDSDao>().GetBatchForInstoreShowCard(deptID);
            }

            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 获取修改药品单据表头实体
        /// </summary>
        /// <returns>药品单据表头实体</returns>
        [WCFMethod]
        public ServiceResponseData GetEditBillHead()
        {
            string belongSys = requestData.GetData<string>(0);
            int billID = requestData.GetData<int>(1);
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                DW_InStoreHead head = (DW_InStoreHead)NewObject<DW_InStoreHead>().getmodel(billID);
                responseData.AddData(head);
            }
            else
            {
                DS_InstoreHead head = (DS_InstoreHead)NewObject<DS_InstoreHead>().getmodel(billID);
                responseData.AddData(head);
            }

            return responseData;
        }

        /// <summary>
        /// 获取药品类型名称
        /// </summary>
        /// <returns>药品类型名称</returns>
        [WCFMethod]
        public ServiceResponseData GetTypeName()
        {
            var cTypeId = requestData.GetData<string>(0);
            string typeName = NewObject<IDGDao>().GetTypeName(cTypeId);
            responseData.AddData(typeName);
            return responseData;
        }

        /// <summary>
        /// 获取入库单报表
        /// </summary>
        /// <returns>入库单报表</returns>
        [WCFMethod]
        public ServiceResponseData GetInstoreReport()
        {
            var opType = requestData.GetData<string>(0);
            var andW = requestData.GetData<List<Tuple<string, string, SqlOperator>>>(1);
            DataTable dtRtn = NewObject<IDWDao>().GetInstoreReport(andW);
            responseData.AddData(dtRtn);
            return responseData;
        }
    }
}
