using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
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
    /// 出库处理控制器
    /// </summary>
    [WCFController]
    public class OutStoreController : WcfServerController
    {
        /// <summary>
        /// 获取药品字典数据
        /// </summary>
        /// <returns>药品字典数据</returns>
        [WCFMethod]
        public ServiceResponseData GetConnectDept()
        {
            var andW = requestData.GetData<List<Tuple<string, string, SqlOperator>>>(0);
            DataTable dt = NewObject<DrugSpecDicMgr>().GetDurgDic(andW, null);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 科室可以出库的药品
        /// </summary>
        /// <returns>可以出库的药品</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptOutDrug()
        {
            var opSystem = requestData.GetData<string>(0);
            int deptId = requestData.GetData<int>(1);
            string bussType = requestData.GetData<string>(2);
            int toDeptId = requestData.GetData<int>(3);

            if (opSystem == DGConstant.OP_DW_SYSTEM)
            {
                DataTable dt = NewObject<DrugMakerDicMgr>().GetDurgDic(deptId, bussType, toDeptId);
                responseData.AddData(dt);
            }
            else
            {
                DataTable dt = NewDao<IDSDao>().GetStoreDrugInFo(deptId);
                responseData.AddData(dt);
            }

            return responseData;
        }

        /// <summary>
        /// 加载出库单主表
        /// </summary>
        /// <returns>出库单主表</returns>
        [WCFMethod]
        public ServiceResponseData LoadBillHead()
        {
            var opType = requestData.GetData<string>(0);
            var andW = requestData.GetData<Dictionary<string, string>>(1);
            IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(opType);
            DataTable dtRtn = iProcess.LoadHead(andW);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 加载出库单明细
        /// </summary>
        /// <returns>出库单明细信息</returns>
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
        /// 药库药房出库 保存出库单据
        /// </summary>
        /// <returns>保存结果</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveBill()
        {
            string belongSys = requestData.GetData<string>(0);
            string busiType = requestData.GetData<string>(1);
            IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(busiType);
            DGBillResult result = new DGBillResult();
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                DW_OutStoreHead head = requestData.GetData<DW_OutStoreHead>(2);
                List<DW_OutStoreDetail> details = requestData.GetData<List<DW_OutStoreDetail>>(3);
                List<int> deleteDetails = requestData.GetData<List<int>>(4);
                //oleDb.BeginTransaction();
                try
                {
                    foreach (int detailID in deleteDetails)
                    {
                        NewObject<DW_OutStoreDetail>().delete(detailID);
                    }

                    iProcess.SaveBill(head, details);
                    Basic_SystemConfig config = NewObject<IDGDao>().GetDeptParameters(head.DeptID, "AutoAuditOutStore");
                    if (config != null)
                    {
                        //是否配置需要审核
                        if (config.Value == "1")
                        {
                            result = iProcess.AuditBill(head.OutStoreHeadID, LoginUserInfo.EmpId, LoginUserInfo.EmpName, LoginUserInfo.WorkId);
                        }
                    }

                    /*if (result.Result == 0)
                    {
                        oleDb.CommitTransaction();
                    }
                    else
                    {
                        oleDb.RollbackTransaction();
                    }*/

                    if(result.Result!=0)
                    {
                        throw new Exception(result.ErrMsg);
                    }
                    
                    responseData.AddData(result);

                    //oleDb.CommitTransaction();
                    //responseData.AddData(true);
                }
                catch (Exception error)
                {
                    throw error;
                    //oleDb.RollbackTransaction();
                    //responseData.AddData(false);
                    //responseData.AddData(error.Message);
                }
            }
            else
            {
                //药房
                DS_OutStoreHead head = requestData.GetData<DS_OutStoreHead>(2);
                List<DS_OutStoreDetail> details = requestData.GetData<List<DS_OutStoreDetail>>(3);
                List<int> deleteDetails = requestData.GetData<List<int>>(4);
                //oleDb.BeginTransaction();
                try
                {
                    foreach (int detailID in deleteDetails)
                    {
                        NewObject<DS_OutStoreDetail>().delete(detailID);
                    }

                    iProcess.SaveBill(head, details);
                    Basic_SystemConfig config = NewObject<IDGDao>().GetDeptParameters(head.DeptID, "AutoAuditOutStore");
                    if (config != null)
                    {
                        //是否需要审核
                        if (config.Value == "1")
                        {
                            result = iProcess.AuditBill(head.OutStoreHeadID, LoginUserInfo.EmpId, LoginUserInfo.EmpName, LoginUserInfo.WorkId);
                        }
                    }

                    /*if (result.Result == 0)
                    {
                        oleDb.CommitTransaction();
                    }
                    else
                    {
                        oleDb.RollbackTransaction();
                    }*/

                    if (result.Result != 0)
                    {
                        throw new Exception(result.ErrMsg);
                    }

                    responseData.AddData(result);
                }
                catch (Exception error)
                {
                    throw error;
                    //oleDb.RollbackTransaction();
                    //responseData.AddData(false);
                    //responseData.AddData(error.Message);
                }
            }

            return responseData;
        }

        /// <summary>
        /// 保存单据
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData SaveBillFromApply()
        {
            string belongSys = requestData.GetData<string>(0);
            string busiType = requestData.GetData<string>(1);
            IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(busiType);
            DGBillResult result = new DGBillResult();
            DW_OutStoreHead head = requestData.GetData<DW_OutStoreHead>(2);
            List<DW_OutStoreDetail> details = requestData.GetData<List<DW_OutStoreDetail>>(3);
            List<int> deleteDetails = requestData.GetData<List<int>>(4);
            oleDb.BeginTransaction();
            try
            {
                foreach (int detailID in deleteDetails)
                {
                    NewObject<DW_OutStoreDetail>().delete(detailID);
                }

                iProcess.SaveBill(head, details);
                DS_ApplyHead applyHead = (DS_ApplyHead)NewObject<DS_ApplyHead>().getmodel(head.ApplyHeadId);
                applyHead.OutStoreHeadID = head.OutStoreHeadID;
                applyHead.AuditFlag = 1;
                applyHead.AuditEmpID = LoginUserInfo.EmpId;
                applyHead.AuditEmpName = LoginUserInfo.EmpName;
                applyHead.AuditTime = DateTime.Now;
                applyHead.save();

                foreach (var s in details)
                {
                    DS_ApplyDetail detailApply= NewObject<DS_ApplyDetail>().getlist<DS_ApplyDetail>("ApplyHeadId = "+ applyHead.ApplyHeadID + " and DrugID=" + s.DrugID +" and BatchNO= '"+s.BatchNO+" '").FirstOrDefault();
                    if (detailApply != null)
                    {
                        detailApply.FactAmount = s.Amount;
                        detailApply.save();
                    }
                }

                Basic_SystemConfig config = NewObject<IDGDao>().GetDeptParameters(head.DeptID, "AutoAuditOutStore");
                if (config != null)
                {
                    //是否配置需要审核
                    if (config.Value == "1")
                    {
                        result = iProcess.AuditBill(head.OutStoreHeadID, LoginUserInfo.EmpId, LoginUserInfo.EmpName, LoginUserInfo.WorkId);
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
                responseData.AddData(false);
                responseData.AddData(error.Message);
            }

            return responseData;
        }

        /// <summary>
        /// 删除出库单据
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
        /// 获取修改药品单据表头实体
        /// </summary>
        /// <returns>出库单表头实体</returns>
        [WCFMethod]
        public ServiceResponseData GetEditBillHead()
        {
            string belongSys = requestData.GetData<string>(0);
            int billID = requestData.GetData<int>(1);
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                DW_OutStoreHead head = (DW_OutStoreHead)NewObject<DW_OutStoreHead>().getmodel(billID);
                responseData.AddData(head);
            }
            else
            {
                DS_OutStoreHead head = (DS_OutStoreHead)NewObject<DS_OutStoreHead>().getmodel(billID);
                responseData.AddData(head);
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
            int deptId = requestData.GetData<int>(4);
            IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(busiType);
            DGBillResult rtn = new DGBillResult();

            if (!NewObject<DrugDeptMgr>().IsDeptChecked(deptId, LoginUserInfo.WorkId))
            {
                rtn.Result = 1;
                rtn.ErrMsg = "当前科室处于盘点状态或者没有设置科室的盘点状态 不能处理业务操作";
                responseData.AddData(rtn);
                return responseData;
            }

            try
            {
                oleDb.BeginTransaction();
                rtn = iProcess.AuditBill(billID, auditEmpID, auditEmpName, LoginUserInfo.WorkId);
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
        /// 获取入库明细转出库明细
        /// </summary>
        /// <returns>入库明细转出库明细</returns>
        [WCFMethod]
        public ServiceResponseData ConvertDwOutFromDwIn()
        {
            var billNo = requestData.GetData<string>(0);
            DataTable dtRtn = NewObject<DGBillConverter>().ConvertDwOutFromDwIn(billNo);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 药库领药单转出库单
        /// </summary>
        /// <returns>领药单转出库单</returns>
        [WCFMethod]
        public ServiceResponseData ConvertDwOutFromApply()
        {
            var applyHead = requestData.GetData<int>(0);
            DataTable dtRtn = NewObject<DGBillConverter>().CovertDWOutFromApply(applyHead);
            responseData.AddData(dtRtn);
            return responseData;
        }
    }
}
