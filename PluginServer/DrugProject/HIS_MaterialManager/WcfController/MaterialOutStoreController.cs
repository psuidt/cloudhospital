using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.BasicData;
using HIS_Entity.DrugManage;
using HIS_Entity.MaterialManage;
using HIS_MaterialManager.Dao;
using HIS_MaterialManager.ObjectModel.Bill;

namespace HIS_MaterialManage.WcfController
{
    /// <summary>
    /// 出库处理控制器
    /// </summary>
    [WCFController]
 
    public class MaterialOutStoreController : WcfServerController
    {
        /// <summary>
        /// 获取修改物资出库实体
        /// </summary>
        /// <returns>出库单表头实体</returns>
        [WCFMethod]
        public ServiceResponseData GetEditBillHead()
        {
            string belongSys = requestData.GetData<string>(0);
            int billID = requestData.GetData<int>(1);
            MW_OutStoreHead head = (MW_OutStoreHead)NewObject<MW_OutStoreHead>().getmodel(billID);
            responseData.AddData(head);
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
            IMwBill iProcess = NewObject<MwBillFactory>().GetBillProcess(opType);
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
            IMwBill iProcess = NewObject<MwBillFactory>().GetBillProcess(opType);
            DataTable dtRtn = iProcess.LoadDetails(queryCondition);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 科室可以出库的物资showcard
        /// </summary>
        /// <returns>出库的物资showcard数据</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptOutMW()
        {
            int deptId = requestData.GetData<int>(0);
            DataTable dt = NewObject<IMWDao>().GetStoreMWInFo(deptId);
            responseData.AddData(dt);
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
            string busiType = requestData.GetData<string>(1);
            IMwBill iProcess = NewObject<MwBillFactory>().GetBillProcess(busiType);
            MWBillResult result = new MWBillResult();
            MW_OutStoreHead head = requestData.GetData<MW_OutStoreHead>(2);
            List<MW_OutStoreDetail> details = requestData.GetData<List<MW_OutStoreDetail>>(3);
            List<int> deleteDetails = requestData.GetData<List<int>>(4);
            oleDb.BeginTransaction();
            try
            {
                foreach (int detailID in deleteDetails)
                {
                    NewObject<MW_OutStoreDetail>().delete(detailID);
                }

                iProcess.SaveBill(head, details);

                Basic_SystemConfig config = NewObject<IMWDao>().GetDeptParameters(head.DeptID, "AutoAuditOutStore");

                if (config != null)
                {
                    if (config.Value == "1")
                    {
                        result = iProcess.AuditBill(head.OutStoreHeadID, LoginUserInfo.EmpId, LoginUserInfo.EmpName);
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
            IMwBill iProcess = NewObject<MwBillFactory>().GetBillProcess(busiType);
            MWBillResult rtn = new MWBillResult();
            try
            {
                oleDb.BeginTransaction();
                rtn = iProcess.AuditBill(billID, auditEmpID, auditEmpName);
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
    }
}