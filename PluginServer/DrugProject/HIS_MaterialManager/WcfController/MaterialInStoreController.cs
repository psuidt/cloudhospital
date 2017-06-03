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
using HIS_Entity.MaterialManage;
using HIS_Entity.SqlAly;
using HIS_MaterialManager.Dao;
using HIS_MaterialManager.ObjectModel.BaseData;
using HIS_MaterialManager.ObjectModel.Bill;

namespace HIS_MaterialManage.WcfController
{
    /// <summary>
    /// 入库处理控制器
    /// </summary>
    [WCFController]
    public class MaterialInStoreController : WcfServerController
    {
        /// <summary>
        /// 加载入库单表头
        /// </summary>
        /// <returns>入库单表头信息</returns>
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
        /// 获取供应商列表用于绑定
        /// </summary>
        /// <returns>供应商列表</returns>
        [WCFMethod]
        public ServiceResponseData GetSupplyForShowCard()
        {
            DataTable dt = NewObject<MaterialSupplyMgr>().GetSupplyForShowCard();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取药剂科室列表用于选择
        /// </summary>
        /// <returns>返回值</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugDeptList()
        {
            DataTable dt = NewObject<MaterialDeptMgr>().GetMaterialDept();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 保存入库单据
        /// </summary>
        /// <returns>保存结果</returns>
        [WCFMethod]
        public ServiceResponseData SaveBill()
        {
            string belongSys = requestData.GetData<string>(0);
            string busiType = requestData.GetData<string>(1);
            IMwBill iProcess = NewObject<MwBillFactory>().GetBillProcess(busiType);

            MWBillResult result = new MWBillResult();

            MW_InStoreHead head = requestData.GetData<MW_InStoreHead>(2);
            List<MW_InStoreDetail> details = requestData.GetData<List<MW_InStoreDetail>>(3);
            List<int> deleteDetails = requestData.GetData<List<int>>(4);
            oleDb.BeginTransaction();
            try
            {
                foreach (int detailID in deleteDetails)
                {
                    NewObject<MW_InStoreDetail>().delete(detailID);
                }

                iProcess.SaveBill(head, details);
                Basic_SystemConfig config = NewObject<IMWDao>().GetDeptParameters(head.DeptID, "AutoAuditInstore");
                if (config != null)
                {
                    if (config.Value == "1")
                    {
                        result = iProcess.AuditBill(head.InHeadID, LoginUserInfo.EmpId, LoginUserInfo.EmpName);
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
            }

            responseData.AddData(result);
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

        /// <summary>
        /// 加载入库单明细
        /// </summary>
        /// <returns>入库单明细信息</returns>
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

            if (busiType != MWConstant.OP_MW_BACKSTORE)
            {
                dtRtn = NewDao<IMWDao>().GetDrugDicForInStoreShowCard(false, deptID);
            }
            else
            {
                dtRtn = NewDao<IMWDao>().GetDrugDicForInStoreShowCard(true, deptID);
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

            dtRtn = NewDao<IMWDao>().GetBatchForInstoreShowCard(deptID);

            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 获取修改药品单据表头实体
        /// </summary>
        /// <returns>入库单表头实体</returns>
        [WCFMethod]
        public ServiceResponseData GetEditBillHead()
        {
            string belongSys = requestData.GetData<string>(0);
            int billID = requestData.GetData<int>(1);

            MW_InStoreHead head = (MW_InStoreHead)NewObject<MW_InStoreHead>().getmodel(billID);
            responseData.AddData(head);

            return responseData;
        }
    }
}
