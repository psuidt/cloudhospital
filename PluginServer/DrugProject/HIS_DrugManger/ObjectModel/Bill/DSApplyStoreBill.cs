using System;
using System.Collections.Generic;
using System.Data;
using HIS_DrugManger.Dao;
using HIS_Entity.DrugManage;
using HIS_PublicManage.ObjectModel;

namespace HIS_DrugManger.ObjectModel.Bill
{
    /// <summary>
    /// 药房申请单处理器
    /// </summary>
    class DSApplyStoreBill:DSInStoreBill
    {
        /// <summary>
        /// 查询药房申请主表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>药房入库单表头</returns>
        public override DataTable LoadHead(Dictionary<string, string> condition)
        {
            //写SQL查询单据头
            return NewDao<IDSDao>().LoadApplyStoreHead(condition);
        }

        /// <summary>
        /// 查询药房申请明细
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>药房入库单明细</returns>
        public override DataTable LoadDetails(Dictionary<string, string> condition)
        {
            //写SQL查询单据明细
            return NewDao<IDSDao>().LoadApplyStoreDetail(condition);
        }

        /// <summary>
        /// 保存药房入库单
        /// </summary>
        /// <typeparam name="THead">药房入库单表头模板</typeparam>
        /// <typeparam name="TDetail">药房入库单明细模板</typeparam>
        /// <param name="billHead">药房入库单表头</param>
        /// <param name="billDetails">药房入库单明细</param>
        public override void SaveBill<THead, TDetail>(THead billHead, List<TDetail> billDetails)
        {
            DS_ApplyHead inHead = billHead as DS_ApplyHead;
            List<DS_ApplyDetail> inDetals = billDetails as List<DS_ApplyDetail>;
            inHead.RegTime = System.DateTime.Now;
            string serialNO = NewObject<SerialNumberSource>().GetSerialNumber(SnType.药品, inHead.ApplyDeptID,DGConstant.OP_DS_APPLYPLAN);
            inHead.BillNO = Convert.ToInt64(serialNO);
            BindDb(inHead);
            inHead.save();
            if (inHead.ApplyHeadID > 0)
            {
                foreach (DS_ApplyDetail detail in inDetals)
                {
                    detail.ApplyHeadID = inHead.ApplyHeadID;
                    BindDb(detail);
                    detail.save();
                }
            }
        }

        /// <summary>
        /// 删除申请单
        /// </summary>
        /// <param name="billID">主键KEY</param>
        public override void DeleteBill(int billID)
        {
            DS_ApplyHead inHead = (DS_ApplyHead)NewObject<DS_ApplyHead>().getmodel(billID);
            if (inHead.AuditFlag == 1)
            {
                throw new Exception("当前单据已经审核,无法删除");
            }
            else
            {
                inHead.DelFlag = 1;
                inHead.save();
            }
        }
    }
}
