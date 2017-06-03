using System;
using System.Collections.Generic;
using System.Data;
using HIS_DrugManger.Dao;
using HIS_DrugManger.ObjectModel.BaseData;
using HIS_DrugManger.ObjectModel.Store;
using HIS_Entity.BasicData;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;
using HIS_PublicManage.ObjectModel;

namespace HIS_DrugManger.ObjectModel.Bill
{
    /// <summary>
    /// 药库出库单处理器
    /// </summary>
    class DWOutStoreBill : DWBill
    {
        /// <summary>
        /// 审核药库出库单
        /// </summary>
        /// <param name="headID">药库出库单表头ID</param>
        /// <param name="auditEmpID">审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <returns>单据处理结果</returns>
        public override DGBillResult AuditBill(int headID, int auditEmpID, string auditEmpName)
        {
            DW_OutStoreHead outHead = (DW_OutStoreHead)NewObject<DW_OutStoreHead>().getmodel(headID);

            outHead.AuditEmpID = auditEmpID;
            outHead.AuditEmpName = auditEmpName;
            outHead.AuditTime = System.DateTime.Now;
            outHead.AuditFlag = 1;
            outHead.save();
            List<DW_OutStoreDetail> lstDetails = NewObject<DW_OutStoreDetail>().getlist<DW_OutStoreDetail>("OutHeadID=" + headID);
            DGBillResult result = new DGBillResult();
            foreach (var outDeatils in lstDetails)
            {
                StoreParam storeParam = new StoreParam();
                storeParam.Amount = outDeatils.Amount;
                storeParam.BatchNO = outDeatils.BatchNO;
                storeParam.DeptID = outDeatils.DeptID;
                storeParam.DrugID = outDeatils.DrugID;
                storeParam.RetailPrice = outDeatils.RetailPrice;
                storeParam.StockPrice = outDeatils.StockPrice;
                storeParam.UnitID = outDeatils.UnitID;
                storeParam.UnitName = outDeatils.UnitName;
                storeParam.ValidityTime = outDeatils.ValidityDate;
                DGStoreResult storeRtn = iStore.ReduceStore(storeParam);
                if (storeRtn.Result != 0)
                {
                    result.Result = 1;
                    if (storeRtn.Result == 1)
                    {
                        result.LstNotEnough = new List<DGNotEnough>();
                        DGNotEnough notEnough = new DGNotEnough();
                        notEnough.DeptID = outHead.DeptID;
                        notEnough.DrugID = outDeatils.DrugID;
                        notEnough.LackAmount = outDeatils.Amount - storeRtn.StoreAmount;
                        notEnough.DrugInfo = "药品批次号:" + outDeatils.BatchNO;
                        result.LstNotEnough.Add(notEnough);
                        result.ErrMsg = "【" + notEnough.DrugInfo + "】库存不足";
                    }
                    else
                    {
                        result.ErrMsg = "药品更新库存出错";
                    }

                    return result;
                }
                else
                {
                    WriteAccount(outHead, outDeatils, storeRtn);
                }
            }

            if (outHead.BusiType == DGConstant.OP_DW_CIRCULATEOUT|| outHead.BusiType == DGConstant.OP_DW_RETURNSTORE) 
            {
                //流通出库业务和退库业务
                var t = outHead.BusiType== DGConstant.OP_DW_CIRCULATEOUT ? DGConstant.OP_DS_CIRCULATEIN:DGConstant.OP_DS_RETURNSOTRE;
                DS_InstoreHead dshead = NewObject<DGBillConverter>().ConvertInFromDWOutHead(outHead, auditEmpID, auditEmpName, t);
                IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(t);
                List<DS_InStoreDetail> dsInStore = NewObject<DGBillConverter>().ConvertInFromDwStoreDetail(headID);
                iProcess.SaveBill(dshead, dsInStore);//药房入库
                Basic_SystemConfig config = NewObject<IDGDao>().GetDeptParameters(dshead.DeptID, "AutoAuditInstore");
                if (config != null)
                {
                    //药房是否需要审核
                    if (config.Value == "1")
                    {
                        result= iProcess.AuditBill(dshead.InHeadID, auditEmpID, auditEmpName);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 审核药库出库单
        /// </summary>
        /// <param name="headID">药库出库单表头ID</param>
        /// <param name="auditEmpID">审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <param name="workId">机构ID</param>
        /// <returns>单据处理结果</returns>
        public override DGBillResult AuditBill(int headID, int auditEmpID, string auditEmpName, int workId)
        {
            DW_OutStoreHead outHead = (DW_OutStoreHead)NewObject<DW_OutStoreHead>().getmodel(headID);
            outHead.AuditEmpID = auditEmpID;
            outHead.AuditEmpName = auditEmpName;
            outHead.AuditTime = System.DateTime.Now;
            outHead.AuditFlag = 1;
            outHead.save();
            List<DW_OutStoreDetail> lstDetails = NewObject<DW_OutStoreDetail>().getlist<DW_OutStoreDetail>("OutHeadID=" + headID);
            DGBillResult result = new DGBillResult();
            if (!NewObject<DrugDeptMgr>().IsDeptChecked(outHead.DeptID, workId))
            {
                result.Result = 1;
                result.ErrMsg = "当前科室处于盘点状态或者没有设置科室的盘点状态 不能处理业务操作";
                return result;
            }

            foreach (var outDeatils in lstDetails)
            {
                StoreParam storeParam = new StoreParam();
                storeParam.Amount = outDeatils.Amount;
                storeParam.BatchNO = outDeatils.BatchNO;
                storeParam.DeptID = outDeatils.DeptID;
                storeParam.DrugID = outDeatils.DrugID;
                storeParam.RetailPrice = outDeatils.RetailPrice;
                storeParam.StockPrice = outDeatils.StockPrice;
                storeParam.UnitID = outDeatils.UnitID;
                storeParam.UnitName = outDeatils.UnitName;
                storeParam.ValidityTime = outDeatils.ValidityDate;
                DGStoreResult storeRtn = iStore.ReduceStore(storeParam);

                if (storeRtn.Result != 0)
                {
                    result.Result = 1;
                    if (storeRtn.Result == 1)
                    {
                        result.LstNotEnough = new List<DGNotEnough>();
                        DGNotEnough notEnough = new DGNotEnough();
                        notEnough.DeptID = outHead.DeptID;
                        notEnough.DrugID = outDeatils.DrugID;
                        notEnough.LackAmount = outDeatils.Amount - storeRtn.StoreAmount;
                        notEnough.DrugInfo ="药品编号"+ outDeatils.DrugID + " 药品批次号:" + outDeatils.BatchNO;
                        result.LstNotEnough.Add(notEnough);
                        result.ErrMsg = "【" + notEnough.DrugInfo + "】库存不足";
                    }
                    else
                    {
                        result.ErrMsg = "药品更新库存出错";
                    }

                    return result;
                }
                else
                {
                    WriteAccount(outHead, outDeatils, storeRtn);
                }
            }

            if (outHead.BusiType == DGConstant.OP_DW_CIRCULATEOUT || outHead.BusiType == DGConstant.OP_DW_RETURNSTORE) 
            {
                //流通出库业务和退库业务
                var t = outHead.BusiType == DGConstant.OP_DW_CIRCULATEOUT ? DGConstant.OP_DS_CIRCULATEIN : DGConstant.OP_DS_RETURNSOTRE;
                DS_InstoreHead dshead = NewObject<DGBillConverter>().ConvertInFromDWOutHead(outHead, auditEmpID, auditEmpName, t);
                IDGBill iProcess = NewObject<DGBillFactory>().GetBillProcess(t);
                string serialNO = NewObject<SerialNumberSource>().GetSerialNumber(SnType.药品, dshead.DeptID, t);
                dshead.BillNO = Convert.ToInt64(serialNO);
                List<DS_InStoreDetail> dsInStore = NewObject<DGBillConverter>().ConvertInFromDwStoreDetail(headID);
                iProcess.SaveBill(dshead, dsInStore);//药房入库
                Basic_SystemConfig config = NewObject<IDGDao>().GetDeptParameters(dshead.DeptID, "AutoAuditInstore");
                if (config != null)
                {
                    //药房是否需要审核
                    if (config.Value == "1")
                    {
                        result = iProcess.AuditBill(dshead.InHeadID, auditEmpID, auditEmpName, workId);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 删除药库出库单
        /// </summary>
        /// <param name="billID">药库出库单表头ID</param>
        public override void DeleteBill(int billID)
        {
            DW_OutStoreHead inHead = (DW_OutStoreHead)NewObject<DW_OutStoreHead>().getmodel(billID);
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

        /// <summary>
        /// 查询药库出库单明细列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>药库出库单明细列表</returns>
        public override DataTable LoadDetails(Dictionary<string, string> condition)
        {
            return NewDao<IDWDao>().LoadOutStoreDetail(condition);
        }

        /// <summary>
        /// 查询药库出库单表头列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>药库出库单表头列表</returns>
        public override DataTable LoadHead(Dictionary<string, string> condition)
        {
            return NewDao<IDWDao>().LoadOutStoreHead(condition);
        }

        /// <summary>
        /// 保存药库出库单
        /// </summary>
        /// <typeparam name="THead">药库出库单表头模板</typeparam>
        /// <typeparam name="TDetail">药库出库单明细模板</typeparam>
        /// <param name="billHead">药库出库单表头</param>
        /// <param name="billDetails">药库出库单明细</param>
        public override void SaveBill<THead, TDetail>(THead billHead, List<TDetail> billDetails)
        {
            DW_OutStoreHead inHead = billHead as DW_OutStoreHead;
            List<DW_OutStoreDetail> inDetals = billDetails as List<DW_OutStoreDetail>;
            inHead.RegTime = System.DateTime.Now;
            string serialNO = NewObject<SerialNumberSource>().GetSerialNumber(SnType.药品, inHead.DeptID, inHead.BusiType);
            inHead.BillNO = Convert.ToInt64(serialNO);
            BindDb(inHead);
            inHead.save();
            if (inHead.OutStoreHeadID > 0)
            {
                foreach (DW_OutStoreDetail detail in inDetals)
                {
                    detail.OutHeadID = inHead.OutStoreHeadID;
                    detail.BillNO = inHead.BillNO;
                    BindDb(detail);
                    detail.save();
                }
            }
        }

        /// <summary>
        /// 台账写入
        /// </summary>
        /// <typeparam name="THead">药库出库单表头模板</typeparam>
        /// <typeparam name="TDetail">药库出库单明细模板</typeparam>
        /// <param name="billHead">药库出库单表头</param>
        /// <param name="billDetails">药库出库单明细</param>
        /// <param name="storeResult">库存处理结果</param>
        public override void WriteAccount<THead, TDetail>(THead billHead, TDetail billDetails, DGStoreResult storeResult)
        {
            DW_OutStoreDetail detail = billDetails as DW_OutStoreDetail;
            DW_OutStoreHead head = billHead as DW_OutStoreHead;
            int actYear;
            int actMonth;
            string errMsg;
            if (!GetAccountTime(head.DeptID, out errMsg, out actYear, out actMonth))
            {
                throw new Exception(errMsg);
            }

            DW_Account account = NewObject<DW_Account>();
            account.BalanceYear = actYear;
            account.BalanceMonth = actMonth;
            account.AccountType = 0;
            account.BalanceFlag = 0;
            account.BillNO = detail.BillNO;
            account.BatchNO = detail.BatchNO;
            account.BillNO = detail.BillNO;
            account.BusiType = head.BusiType;
            account.CTypeID = detail.CTypeID;
            account.DeptID = head.DeptID;
            account.DetailID = detail.OutDetailID;
            account.DrugID = detail.DrugID;
            account.UnitID = detail.UnitID;
            account.UnitName = detail.UnitName;
            account.DebitAmount = detail.Amount;//贷方数量
            account.RegTime = System.DateTime.Now;
            if (head.BusiType == DGConstant.OP_DW_BACKSTORE)
            {
                account.StockPrice = storeResult.BatchAllot[0].StockPrice;
                account.RetailPrice = storeResult.BatchAllot[0].RetailPrice;
                account.DebitAmount = detail.Amount;
                account.DebitRetailFee = account.DebitAmount * account.RetailPrice;
                account.DebitStockFee = account.DebitAmount * account.StockPrice;
                account.OverAmount = storeResult.BatchAllot[0].StoreAmount;
                account.OverRetailFee = storeResult.BatchAllot[0].StoreAmount * storeResult.BatchAllot[0].RetailPrice;
                account.OverStockFee = storeResult.BatchAllot[0].StoreAmount * storeResult.BatchAllot[0].StockPrice;
            }
            else
            {
                account.StockPrice = detail.StockPrice;
                account.RetailPrice = detail.RetailPrice;
                account.DebitAmount = detail.Amount;
                account.DebitRetailFee = detail.RetailFee;
                account.DebitStockFee = detail.StockFee;
                account.OverAmount = storeResult.BatchAllot[0].StoreAmount;
                account.OverRetailFee = storeResult.BatchAllot[0].StoreAmount * account.RetailPrice;
                account.OverStockFee = storeResult.BatchAllot[0].StoreAmount * account.StockPrice;
            }

            account.save();
        }

        /// <summary>
        /// 查询入库单头表
        /// </summary>
        /// <param name="andWhere">查询条件</param>
        /// <returns>入库单头表</returns>
        public override DataTable LoadHead(List<Tuple<string, string, SqlOperator>> andWhere = null)
        {
            return NewDao<IDWDao>().LoadOutStoreHead(andWhere);
        }
    }
}
