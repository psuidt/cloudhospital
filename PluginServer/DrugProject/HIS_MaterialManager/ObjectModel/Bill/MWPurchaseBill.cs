using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MaterialManage;
using HIS_Entity.SqlAly;
using HIS_MaterialManager.Dao;

namespace HIS_MaterialManager.ObjectModel.Bill
{
    /// <summary>
    /// 物资采购计划单处理器
    /// </summary>
    public class MwPurchaseBill : AbstractObjectModel, IMwBill
    {
        /// <summary>
        /// 审核采购计划（不实现）
        /// </summary>
        /// <param name="headID">采购计划单表头ID</param>
        /// <param name="auditEmpID">采购计划审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <returns>审核结果信息</returns>
        public MWBillResult AuditBill(int headID, int auditEmpID, string auditEmpName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 审核单据
        /// </summary>
        /// <typeparam name="THead">单据头模板</typeparam>
        /// <param name="headID">单据头ID</param>
        /// <param name="auditEmpID">审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <param name="workId">机构ID</param>
        /// <returns>审核结果信息</returns>
        public MWBillResult AuditBill(int headID, int auditEmpID, string auditEmpName, int workId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除采购计划
        /// </summary>
        /// <param name="billID">采购计划表头ID</param>
        public void DeleteBill(int billID)
        {
            MW_PlanHead head = (MW_PlanHead)NewObject<MW_PlanHead>().getmodel(billID);
            if (head.AuditFlag == 1)
            {
                throw new Exception("当前单据已经审核,无法删除");
            }
            else
            {
                //调用删除采购计划单方法
                bool rtn = NewDao<IMWDao>().DeleteMWPlanBill(billID);
                if (rtn == false)
                {
                    throw new Exception("删除采购计划单失败");
                }
            }
        }

        /// <summary>
        /// 加载采购计划明细
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>采购计划明细</returns>
        public DataTable LoadDetails(Dictionary<string, string> condition)
        {
            return NewDao<IMWDao>().GetPlanDetailData(condition);
        }

        /// <summary>
        /// 加载采购头信息
        /// </summary>
        /// <param name="andWhere">查询条件</param>
        /// <returns>采购头信息</returns>
        public DataTable LoadHead(List<Tuple<string, string, SqlOperator>> andWhere = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 加载采购计划表头
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>采购计划表头列表</returns>
        public DataTable LoadHead(Dictionary<string, string> condition)
        {
            return NewDao<IMWDao>().GetPlanHeadData(condition);
        }

        /// <summary>
        /// 保存采购计划单
        /// </summary>
        /// <typeparam name="THead">采购计划头模板</typeparam>
        /// <typeparam name="TDetail">采购计划明细模板</typeparam>
        /// <param name="billHead">采购计划表头</param>
        /// <param name="billDetails">采购计划明细</param>
        public void SaveBill<THead, TDetail>(THead billHead, List<TDetail> billDetails)
        {
            MW_PlanHead head = billHead as MW_PlanHead;
            List<MW_PlanDetail> details = billDetails as List<MW_PlanDetail>;
            if (head.PlanHeadID == 0)
            {
                head.RegTime = System.DateTime.Now;
                head.UpdateTime = System.DateTime.Now;
            }
            else
            {
                head.UpdateTime = System.DateTime.Now;
            }

            BindDb(head);
            head.save();
            if (head.PlanHeadID > 0)
            {
                foreach (MW_PlanDetail detail in details)
                {
                    detail.PlanHeadID = head.PlanHeadID;
                    BindDb(detail);
                    detail.save();
                }
            }
        }

        /// <summary>
        /// 写入采购计划台账（不实现）
        /// </summary>
        /// <typeparam name="THead">采购计划头模板</typeparam>
        /// <typeparam name="TDetail">采购计划明细模板</typeparam>
        /// <param name="billHead">采购计划表头</param>
        /// <param name="billDetails">采购计划明细</param>
        /// <param name="storeResult">库存处理结果</param>
        public void WriteAccount<THead, TDetail>(THead billHead, TDetail billDetails, MWStoreResult storeResult)
        {
            throw new NotImplementedException();
        }
    }
}
