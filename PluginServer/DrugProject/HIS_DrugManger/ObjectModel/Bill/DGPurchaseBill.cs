using System;
using System.Collections.Generic;
using System.Data;
using HIS_DrugManger.Dao;
using HIS_Entity.DrugManage;

namespace HIS_DrugManger.ObjectModel.Bill
{
    /// <summary>
    /// 药品采购计划单处理器
    /// </summary>
    class DGPurchaseBill : DWBill
    {
        /// <summary>
        /// 审核采购计划（不实现）
        /// </summary>
        /// <param name="headID">采购计划单表头ID</param>
        /// <param name="auditEmpID">采购计划审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <returns>返回结果对象</returns>
        public override DGBillResult AuditBill(int headID, int auditEmpID, string auditEmpName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 审核采购计划
        /// </summary>
        /// <param name="headID">采购计划单表头ID</param>
        /// <param name="auditEmpID">采购计划审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <param name="workId">机构ID</param>
        /// <returns>返回结果对象</returns>
        public override DGBillResult AuditBill(int headID, int auditEmpID, string auditEmpName,int workId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除采购计划
        /// </summary>
        /// <param name="billID">采购计划表头ID</param>
        public override void DeleteBill(int billID)
        {
            DW_PlanHead head = (DW_PlanHead)NewObject<DW_PlanHead>().getmodel(billID);
            if (head.AuditFlag == 1)
            {
                throw new Exception("当前单据已经审核,无法删除");
            }
            else
            {
               //调用删除采购计划单方法
               bool rtn= NewDao<IDGDao>().DeleteDWPlanBill(billID);
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
        public override DataTable LoadDetails(Dictionary<string, string> condition)
        {
            return NewDao<IDGDao>().GetPlanDetailData(condition);
        }

        /// <summary>
        /// 加载采购计划表头
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>采购计划表头列表</returns>
        public override DataTable LoadHead(Dictionary<string, string> condition)
        {
            return NewDao<IDGDao>().GetPlanHeadData(condition);
        }

        /// <summary>
        /// 保存采购计划单
        /// </summary>
        /// <typeparam name="THead">采购计划头模板</typeparam>
        /// <typeparam name="TDetail">采购计划明细模板</typeparam>
        /// <param name="billHead">采购计划表头</param>
        /// <param name="billDetails">采购计划明细</param>
        public override void SaveBill<THead, TDetail>(THead billHead, List<TDetail> billDetails)
        {
            DW_PlanHead head = billHead as DW_PlanHead;
            List<DW_PlanDetail> details = billDetails as List<DW_PlanDetail>;
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
                foreach (DW_PlanDetail detail in details)
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
        public override void WriteAccount<THead, TDetail>(THead billHead, TDetail billDetails, DGStoreResult storeResult)
        {
            throw new NotImplementedException();
        }
    }
}
