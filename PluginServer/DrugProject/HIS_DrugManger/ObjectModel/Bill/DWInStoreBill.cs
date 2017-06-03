using System;
using System.Collections.Generic;
using System.Data;
using HIS_DrugManger.Dao;
using HIS_DrugManger.ObjectModel.BaseData;
using HIS_DrugManger.ObjectModel.Store;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;
using HIS_PublicManage.ObjectModel;

namespace HIS_DrugManger.ObjectModel.Bill
{
    /// <summary>
    /// 药库入库单处理器
    /// </summary>
    class DWInStoreBill : DWBill
    {
        /// <summary>
        /// 审核药库入库单
        /// </summary>
        /// <param name="headID">药库入库单表头</param>
        /// <param name="auditEmpID">审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <returns>单据处理结果</returns>
        public override DGBillResult AuditBill(int headID, int auditEmpID, string auditEmpName)
        {
            DW_InStoreHead head = (DW_InStoreHead)NewObject<DW_InStoreHead>().getmodel(headID);
            List<DW_InStoreDetail> lstDetails = NewObject<DW_InStoreDetail>().getlist<DW_InStoreDetail>("InHeadID=" + headID);
            head.AuditEmpID = auditEmpID;
            head.AuditEmpName = auditEmpName;
            head.AuditTime = System.DateTime.Now;
            head.AuditFlag = 1;
            head.save();
            DGBillResult result = new DGBillResult();
            foreach (DW_InStoreDetail detail in lstDetails)
            {
                StoreParam storeParam = new StoreParam();
                storeParam.Amount = detail.Amount;
                storeParam.BatchNO = detail.BatchNo;
                storeParam.DeptID = head.DeptID;
                storeParam.DrugID = detail.DrugID;
                storeParam.RetailPrice = detail.RetailPrice;
                storeParam.StockPrice = detail.StockPrice;
                storeParam.UnitID = detail.UnitID;
                storeParam.UnitName = detail.UnitName;
                storeParam.ValidityTime = detail.ValidityDate;
                storeParam.BussConstant = head.BusiType;
                //storeParam.PackUnit = detail.PackUnit;
                DGStoreResult storeRtn = iStore.AddStore(storeParam);
                if (storeRtn.Result != 0)
                {
                    result.Result = 1;
                    if (storeRtn.Result == 1)
                    {
                        result.LstNotEnough = new List<DGNotEnough>();
                        DGNotEnough notEnough = new DGNotEnough();
                        notEnough.DeptID = head.DeptID;
                        notEnough.DrugID = detail.DrugID;
                        notEnough.LackAmount = storeRtn.StoreAmount + detail.Amount;
                        notEnough.DrugInfo = "药品编号"+ detail.DrugID+"药品批次号:"+detail.BatchNo;
                        result.LstNotEnough.Add(notEnough);
                        result.ErrMsg = "【"+notEnough.DrugInfo+"】库存不足";
                    }
                    else
                    {
                        result.ErrMsg = "药品更新库存出错";
                    }

                    return result;
                }
                else
                {
                    WriteAccount(head, detail, storeRtn);
                }
            }

            result.Result = 0;
            return result;
        }

        /// <summary>
        /// 审核药库单据
        /// </summary>
        /// <param name="headID">药库入库单表头ID</param>
        /// <param name="auditEmpID">审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <param name="workId">机构ID</param>
        /// <returns>单据处理结果</returns>
        public override DGBillResult AuditBill(int headID, int auditEmpID, string auditEmpName, int workId)
        {
            DW_InStoreHead head = (DW_InStoreHead)NewObject<DW_InStoreHead>().getmodel(headID);
            List<DW_InStoreDetail> lstDetails = NewObject<DW_InStoreDetail>().getlist<DW_InStoreDetail>("InHeadID=" + headID);
            head.AuditEmpID = auditEmpID;
            head.AuditEmpName = auditEmpName;
            head.AuditTime = System.DateTime.Now;
            head.AuditFlag = 1;
            head.save();
            DGBillResult result = new DGBillResult();
            if (!NewObject<DrugDeptMgr>().IsDeptChecked(head.DeptID, workId))
            {
                result.Result = 1;
                result.ErrMsg = "当前科室处于盘点状态或者没有设置科室的盘点状态 不能处理业务操作";
                return result;
            }

            foreach (DW_InStoreDetail detail in lstDetails)
            {
                //获取批次数据
                DW_Batch batch = NewDao<IDWDao>().GetBatchAmount(head.DeptID, detail.DrugID, detail.BatchNo);

                if (batch != null)
                {
                    if (detail.Amount < 0)
                    {

                        if ((batch.RetailPrice.Equals(detail.RetailPrice) == false) ||
                            (batch.StockPrice.Equals(detail.StockPrice) == false))
                        {
                            result.Result = 1;
                            result.ErrMsg = "编码【" + detail.DrugID.ToString() + "的（进货价/零售价）】与【" + batch.BatchNO + "】批次价格不一致(进货价：" + batch.StockPrice.ToString()
                                + ",零售价:" + batch.RetailPrice.ToString() + ")，请核查库存！";

                            return result;
                        }

                    }
                    else
                    {
                        if ((batch.RetailPrice.Equals(detail.RetailPrice) == false) ||
                            (batch.StockPrice.Equals(detail.StockPrice) == false))
                        {
                            result.Result = 1;
                            result.ErrMsg = "编码【" + detail.DrugID.ToString() + "的（进货价/零售价）】与【" + batch.BatchNO + "】批次价格不一致(进货价："+batch.StockPrice.ToString()
                                +",零售价:"+ batch.RetailPrice.ToString()+")，请核查库存！";

                            return result;
                        }
                    }
                }

                StoreParam storeParam = new StoreParam();
                storeParam.Amount = detail.Amount;
                storeParam.BatchNO = detail.BatchNo;
                storeParam.DeptID = head.DeptID;
                storeParam.DrugID = detail.DrugID;
                storeParam.RetailPrice = detail.RetailPrice;
                storeParam.StockPrice = detail.StockPrice;
                storeParam.UnitID = detail.UnitID;
                storeParam.UnitName = detail.UnitName;
                storeParam.ValidityTime = detail.ValidityDate;
                storeParam.BussConstant = head.BusiType;

                //storeParam.PackUnit = detail.PackUnit;
                DGStoreResult storeRtn = iStore.AddStore(storeParam);
                if (storeRtn.Result != 0)
                {
                    result.Result = 1;
                    if (storeRtn.Result == 1)
                    {
                        result.LstNotEnough = new List<DGNotEnough>();
                        DGNotEnough notEnough = new DGNotEnough();
                        notEnough.DeptID = head.DeptID;
                        notEnough.DrugID = detail.DrugID;
                        notEnough.LackAmount = storeRtn.StoreAmount + detail.Amount;
                        notEnough.DrugInfo = "药品编号"+ detail.DrugID + " 药品批次号:" + detail.BatchNo;
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
                    WriteAccount(head, detail, storeRtn);
                }
            }

            result.Result = 0;
            return result;
        }

        /// <summary>
        /// 删除药库入库单
        /// </summary>
        /// <param name="billID">药库入库单表头ID</param>
        public override void DeleteBill(int billID)
        {
            DW_InStoreHead inHead = (DW_InStoreHead)NewObject<DW_InStoreHead>().getmodel(billID);
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
        /// 查询药库入库单明细
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>药库入库单明细列表</returns>
        public override DataTable LoadDetails(Dictionary<string, string> condition)
        {
            return NewDao<IDWDao>().LoadInStoreDetail(condition);           
        }

        /// <summary>
        /// 查询药库入库单头表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>药库入库单表头</returns>
        public override DataTable LoadHead(Dictionary<string, string> condition)
        {
            return NewDao<IDWDao>().LoadInStoreHead(condition);
        }

        /// <summary>
        /// 保存药库入库单
        /// </summary>
        /// <typeparam name="THead">药库入库单表头模板</typeparam>
        /// <typeparam name="TDetail">药库入库单明细模板</typeparam>
        /// <param name="billHead">药库入库单表头</param>
        /// <param name="billDetails">药库入库单明细</param>
        public override void SaveBill<THead, TDetail>(THead billHead, List<TDetail> billDetails)
        {
            DW_InStoreHead inHead = billHead as DW_InStoreHead;
            List<DW_InStoreDetail> inDetals = billDetails as List<DW_InStoreDetail>;
            inHead.RegTime = System.DateTime.Now;
            string serialNO = NewObject<SerialNumberSource>().GetSerialNumber(SnType.药品, inHead.DeptID, inHead.BusiType);
            inHead.BillNo = Convert.ToInt64(serialNO);
            BindDb(inHead);
            inHead.save();
            if (inHead.InHeadID > 0)
            {
                foreach (DW_InStoreDetail detail in inDetals)
                {
                    detail.InHeadID = inHead.InHeadID;
                    detail.BillNo = inHead.BillNo;
                    detail.DeptID = inHead.DeptID;
                    BindDb(detail);
                    detail.save();
                }
            }
        }

        /// <summary>
        /// 台账写入
        /// </summary>
        /// <typeparam name="THead">药库入库单表头模板</typeparam>
        /// <typeparam name="TDetail">药库入库单明细模板</typeparam>
        /// <param name="billHead">药库入库单表头</param>
        /// <param name="billDetails">药库入库单明细</param>
        /// <param name="storeResult">库存处理结果</param>
        public override void WriteAccount<THead, TDetail>(THead billHead, TDetail billDetails, DGStoreResult storeResult)
        {
            DW_InStoreDetail detail = billDetails as DW_InStoreDetail;
            DW_InStoreHead head = billHead as DW_InStoreHead;
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
            account.BatchNO = detail.BatchNo;
            account.BusiType = head.BusiType;
            account.CTypeID = detail.CTypeID;
            account.DeptID = head.DeptID;
            account.DetailID = detail.InDetailID;
            account.DrugID = detail.DrugID;
            account.UnitID = detail.UnitID;
            account.UnitName = detail.UnitName;
            account.LendAmount = detail.Amount;
            account.BillNO = detail.BillNo;
            account.RegTime = DateTime.Now;
            if (head.BusiType == DGConstant.OP_DW_BACKSTORE)
            {
                account.StockPrice = storeResult.BatchAllot[0].StockPrice;
                account.RetailPrice = storeResult.BatchAllot[0].RetailPrice;
                account.LendAmount = detail.Amount;
                account.LendRetailFee = account.LendAmount * account.RetailPrice;
                account.LendStockFee = account.LendAmount * account.StockPrice;

                account.OverAmount = storeResult.BatchAllot[0].StoreAmount;
                account.OverRetailFee = storeResult.BatchAllot[0].StoreAmount * account.RetailPrice;
                account.OverStockFee = storeResult.BatchAllot[0].StoreAmount * account.StockPrice;
            }
            else
            {
                account.StockPrice = detail.StockPrice;
                account.RetailPrice = detail.RetailPrice;
                account.LendAmount = detail.Amount;
                account.LendRetailFee = detail.RetailFee;
                account.LendStockFee = detail.StockFee;

                account.OverAmount = storeResult.BatchAllot[0].StoreAmount;
                account.OverRetailFee = storeResult.BatchAllot[0].StoreAmount * account.RetailPrice;
                account.OverStockFee = storeResult.BatchAllot[0].StoreAmount * account.StockPrice;
            }

            account.save();
        }

        /// <summary>
        /// 入库报表
        /// </summary>
        /// <param name="andWhere">查询条件</param>
        /// <returns>入库报表数据源</returns>
        public DataTable GetInstoreReport(List<Tuple<string, string, SqlOperator>> andWhere = null)
        {
            return NewDao<IDWDao>().GetInstoreReport(andWhere);
        }
    }
}
