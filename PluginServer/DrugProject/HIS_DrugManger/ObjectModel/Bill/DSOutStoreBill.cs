using System;
using System.Collections.Generic;
using System.Data;
using HIS_DrugManger.Dao;
using HIS_DrugManger.ObjectModel.BaseData;
using HIS_DrugManger.ObjectModel.Store;
using HIS_Entity.DrugManage;
using HIS_PublicManage.ObjectModel;

namespace HIS_DrugManger.ObjectModel.Bill
{
    /// <summary>
    /// 药房出库单处理器
    /// </summary>
    class DSOutStoreBill : DSBill
    {
        /// <summary>
        /// 审核药房出库单
        /// </summary>
        /// <param name="headID">药房出库单表头ID</param>
        /// <param name="auditEmpID">审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <returns>单据处理结果</returns>
        public override DGBillResult AuditBill(int headID, int auditEmpID, string auditEmpName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取药品的包装换算系数
        /// </summary>
        /// <param name="drugId">药品ID</param>
        /// <returns>药品的包装换算系数</returns>
        public decimal GetPackAmount(int drugId)
        {
            return NewDao<IDGDao>().GetPackAmount(drugId);
        }

        /// <summary>
        /// 审核药房出库单
        /// </summary>
        /// <param name="headID">药房出库单表头ID</param>
        /// <param name="auditEmpID">审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <param name="workId">机构ID</param>
        /// <returns>单据处理结果</returns>
        public override DGBillResult AuditBill(int headID, int auditEmpID, string auditEmpName, int workId)
        {
            DS_OutStoreHead outHead = (DS_OutStoreHead)NewObject<DS_OutStoreHead>().getmodel(headID);
            outHead.AuditEmpID = auditEmpID;
            outHead.AuditEmpName = auditEmpName;
            outHead.AuditTime = System.DateTime.Now;
            outHead.AuditFlag = 1;
            outHead.save();
            List<DS_OutStoreDetail> lstDetails = NewObject<DS_OutStoreDetail>().getlist<DS_OutStoreDetail>("OutHeadID=" + headID);
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
                decimal packAmount = GetPackAmount(outDeatils.DrugID);
                storeParam.PackAmount = Convert.ToInt32(packAmount);

                storeParam.Amount = outDeatils.Amount ;//包装数*系数+基本单位数
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
                        notEnough.DrugInfo ="药品编号"+ outDeatils.DrugID+ " 药品批次号:" + outDeatils.BatchNO;
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
                    DGStoreResult vStoreRtn = NewObject<DSStore>().ReduceValidStore(storeParam);
                    if (vStoreRtn.Result != 0)
                    {
                        result.Result = 1;
                        if (vStoreRtn.Result == 1)
                        {
                            result.LstNotEnough = new List<DGNotEnough>();
                            DGNotEnough notEnough = new DGNotEnough();
                            notEnough.DeptID = outHead.DeptID;
                            notEnough.DrugID = outDeatils.DrugID;
                            notEnough.LackAmount = outDeatils.Amount - storeRtn.StoreAmount;
                            notEnough.DrugInfo = "药品编号" + outDeatils.DrugID + " 药品批次号:" + outDeatils.BatchNO;
                            result.LstNotEnough.Add(notEnough);
                            result.ErrMsg = "【" + notEnough.DrugInfo + "】没有有效库存";
                        }
                        else
                        {
                            result.ErrMsg = "药品更新有效库存出错";
                        }

                        return result;
                    }

                    WriteAccount(outHead, outDeatils, storeRtn,packAmount);
                }
            }

            return result;
        }

        /// <summary>
        /// 删除药房出库单
        /// </summary>
        /// <param name="billID">药房出库单表头ID</param>
        public override void DeleteBill(int billID)
        {
            DS_OutStoreHead inHead = (DS_OutStoreHead)NewObject<DS_OutStoreHead>().getmodel(billID);
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
        /// 查询药房出库单明细列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>药房出库单明细列表</returns>
        public override DataTable LoadDetails(Dictionary<string, string> condition)
        {
            return NewDao<IDSDao>().LoadOutStoreDetail(condition);
        }

        /// <summary>
        /// 查询药房出库单表头列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>药房出库单表头列表</returns>
        public override DataTable LoadHead(Dictionary<string, string> condition)
        {
            return NewDao<IDSDao>().LoadOutStoreHead(condition);
        }

        /// <summary>
        /// 保存药房出库单
        /// </summary>
        /// <typeparam name="THead">药房出库单表头模板</typeparam>
        /// <typeparam name="TDetail">药房出库单明细模板</typeparam>
        /// <param name="billHead">药房出库单表头</param>
        /// <param name="billDetails">药房出库单明细</param>
        public override void SaveBill<THead, TDetail>(THead billHead, List<TDetail> billDetails)
        {
            DS_OutStoreHead outHead = billHead as DS_OutStoreHead;
            List<DS_OutStoreDetail> inDetals = billDetails as List<DS_OutStoreDetail>;
            outHead.RegTime = System.DateTime.Now;
            string serialNO = NewObject<SerialNumberSource>().GetSerialNumber(SnType.药品, outHead.DeptID, outHead.BusiType);
            outHead.BillNO = Convert.ToInt64(serialNO);
            BindDb(outHead);
            outHead.save();
            if (outHead.OutStoreHeadID > 0)
            {
                foreach (DS_OutStoreDetail detail in inDetals)
                {
                    detail.OutHeadID = outHead.OutStoreHeadID;
                    detail.BillNO = outHead.BillNO;
                    BindDb(detail);
                    detail.save();
                }
            }
        }

        /// <summary>
        /// 台账写入
        /// </summary>
        /// <typeparam name="THead">药房出库单表头模板</typeparam>
        /// <typeparam name="TDetail">药房出库单明细模板</typeparam>
        /// <param name="billHead">药房出库单表头</param>
        /// <param name="billDetails">药房出库单明细</param>
        /// <param name="storeResult">库存处理结果</param>
        public override void WriteAccount<THead, TDetail>(THead billHead, TDetail billDetails, DGStoreResult storeResult)
        {
            WriteAccount(billHead, billDetails, storeResult, 0);
        }

        /// <summary>
        /// 台账写入
        /// </summary>
        /// <typeparam name="THead">药房出库单表头模板</typeparam>
        /// <typeparam name="TDetail">药房出库单明细模板</typeparam>
        /// <param name="billHead">药房出库单表头</param>
        /// <param name="billDetails">药房出库单明细</param>
        /// <param name="storeResult">库存处理结果</param>
        /// <param name="packAmount">包装数量</param>
        public void WriteAccount<THead, TDetail>(THead billHead, TDetail billDetails, DGStoreResult storeResult,decimal packAmount)
        {
            //1、构建台账表实体；
            //2、根据台账表内容填写相应的信息；
            //3、保存台账表
            DS_OutStoreDetail detail = billDetails as DS_OutStoreDetail;
            DS_OutStoreHead head = billHead as DS_OutStoreHead;
            int actYear;
            int actMonth;
            string errMsg;
            if (!GetAccountTime(head.DeptID, out errMsg, out actYear, out actMonth))
            {
                throw new Exception(errMsg);
            }

            packAmount = packAmount > 0 ? packAmount : GetPackAmount(detail.DrugID);
            DS_Account account = NewObject<DS_Account>();
            account.BalanceYear = actYear;
            account.BalanceMonth = actMonth;
            account.AccountType = 0;
            account.BalanceFlag = 0;
            account.BillNO = head.BillNO;
            account.BatchNO = detail.BatchNO;
            account.BusiType = head.BusiType;
            account.CTypeID = detail.CTypeID;
            account.DeptID = head.DeptID;
            account.DetailID = detail.OutDetailID;
            account.DrugID = detail.DrugID;
            account.UnitID = detail.UnitID;
            account.UnitName = detail.UnitName;
            account.RegTime = System.DateTime.Now;
            account.UnitAmount = Convert.ToInt32(packAmount);
            if (head.BusiType == DGConstant.OP_DS_RETURNSOTRE)
            {
                account.StockPrice = storeResult.BatchAllot[0].StockPrice;
                account.RetailPrice = storeResult.BatchAllot[0].RetailPrice;   
                account.DebitAmount = detail.Amount;
                account.DebitRetailFee = account.DebitAmount * (account.RetailPrice/packAmount);
                account.DebitStockFee = account.DebitAmount * (account.StockPrice/packAmount);
            }
            else
            {
                account.StockPrice = detail.StockPrice;
                account.RetailPrice = detail.RetailPrice;
                account.DebitAmount = detail.Amount;
                account.DebitRetailFee = account.DebitAmount * (account.RetailPrice / packAmount);
                account.DebitStockFee = account.DebitAmount * (account.StockPrice / packAmount);
            }

            account.OverAmount = storeResult.BatchAllot[0].StoreAmount;
            account.OverStockFee = storeResult.BatchAllot[0].StoreAmount * (account.RetailPrice / packAmount);
            account.OverRetailFee = storeResult.BatchAllot[0].StoreAmount * (account.StockPrice / packAmount);
            account.save();
        }
    }
}
