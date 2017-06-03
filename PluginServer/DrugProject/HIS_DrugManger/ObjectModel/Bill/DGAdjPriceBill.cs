using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManger.Dao;
using HIS_DrugManger.ObjectModel.BaseData;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;
using HIS_PublicManage.ObjectModel;

namespace HIS_DrugManger.ObjectModel.Bill
{
    /// <summary>
    /// 药品调价单处理器
    /// </summary>
    class DGAdjPriceBill : AbstractObjectModel, IDGBill
    {
        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="headID">单据头ID</param>
        /// <param name="auditEmpID">审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <returns>返回结果</returns>
        public DGBillResult AuditBill(int headID, int auditEmpID, string auditEmpName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="headID">单据头ID</param>
        /// <param name="auditEmpID">审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <param name="workId">机构ID</param>
        /// <returns>返回结果</returns>
        public DGBillResult AuditBill(int headID, int auditEmpID, string auditEmpName, int workId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除调价单
        /// </summary>
        /// <param name="billID">单据头信息</param>
        public void DeleteBill(int billID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取入库打印报表数据
        /// </summary>
        /// <param name="andWhere">查询条件</param>
        /// <returns>入库打印报表数据</returns>
        public DataTable GetInstoreReport(List<Tuple<string, string, SqlOperator>> andWhere = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 加载调价单明细
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>单据明细列表</returns>
        public DataTable LoadDetails(Dictionary<string, string> condition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取表头数据
        /// </summary>
        /// <param name="andWhere">查询条件</param>
        /// <returns>表头数据</returns>
        public DataTable LoadHead(List<Tuple<string, string, SqlOperator>> andWhere = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 加载调价单表头
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>调价单表头列表</returns>
        public DataTable LoadHead(Dictionary<string, string> condition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 保存调价单
        /// </summary>
        /// <typeparam name="THead">调价单表头模板</typeparam>
        /// <typeparam name="TDetail">调价单明细模板</typeparam>
        /// <param name="billHead">调价单表头</param>
        /// <param name="billDetails">调价单明细</param>
        /// <param name="workId">机构ID</param>
        /// <returns>返回结果</returns>
        public DGBillResult SaveBill<THead, TDetail>(THead billHead, List<TDetail> billDetails, int workId)
        {
            DG_AdjHead adjHead = billHead as DG_AdjHead;
            List<DG_AdjDetail> adjDetails = billDetails as List<DG_AdjDetail>;
            DGBillResult result = new DGBillResult();
            if (!NewObject<DrugDeptMgr>().IsDeptChecked(adjHead.DeptID, workId))
            {
                result.Result = 1;
                result.ErrMsg = "当前科室处于盘点状态或者没有设置科室的盘点状态 不能处理业务操作";
                return result;
            }

            string serialNO = NewObject<SerialNumberSource>().GetSerialNumber(SnType.药品, adjHead.DeptID, adjHead.BusiType);
            adjHead.BillNO = Convert.ToInt64(serialNO);
            this.BindDb(adjHead);
            int headid = adjHead.save();
            if (headid > 0)
            {
                adjHead.AdjHeadID = headid;
                foreach (DG_AdjDetail currentDetail in adjDetails)
                {
                    WriteAccount(adjHead, currentDetail, null);
                }
            }

            result.Result = 0;
            return result;
        }

        /// <summary>
        /// 写入调价单台账
        /// </summary>
        /// <typeparam name="THead">调价单表头</typeparam>
        /// <typeparam name="TDetail">调价单明细</typeparam>
        /// <param name="billHead">单据头</param>
        /// <param name="billDetails">单据明细</param>
        /// <param name="storeResult">库存处理结果</param>
        public void WriteAccount<THead, TDetail>(THead billHead, TDetail billDetails, DGStoreResult storeResult)
        {
            DG_AdjHead adjHead = billHead as DG_AdjHead;
            DG_AdjDetail currentDetail = billDetails as DG_AdjDetail;
            currentDetail.AdjHeadID = adjHead.AdjHeadID;
            currentDetail.BillNO = adjHead.BillNO;
            #region 操作药库批次和明细表
            List<DW_Batch> dwlist = NewDao<IDWDao>().GetBatchList(currentDetail.BatchNO, currentDetail.DrugID);
            foreach (DW_Batch dwbatch in dwlist)
            {
                SaveAccout(dwbatch, currentDetail);
            }
            #endregion

            #region 操作药房批次和明细表
            List<DS_Batch> dslist = NewDao<IDSDao>().GetBatchList(currentDetail.BatchNO, currentDetail.DrugID);
            foreach (DS_Batch dsbatch in dslist)
            {
                SaveAccout(dsbatch, currentDetail);
            }
            #endregion
        }

        /// <summary>
        /// 获取库房当前会计年、月
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="errMsg">错误信息</param>
        /// <param name="actYear">会计年份</param>
        /// <param name="actMonth">会计月份</param>
        /// <param name="actId">月结历史ID</param>
        /// <returns>当前会计年、月</returns>
        public bool GetDSAccountTime(int deptID, out string errMsg, out int actYear, out int actMonth, out int actId)
        {
            errMsg = string.Empty;
            DS_BalanceRecord record = NewDao<Dao.IDSDao>().GetMaxBlanceRecord(deptID);
            if (record == null)
            {
                errMsg = "当前药房没有进行初始化月结，请联系管理员";
                actYear = 0;
                actMonth = 0;
                actId = 0;
                return false;
            }
            else
            {
                if (System.DateTime.Now >= record.EndTime)
                {
                    actMonth = record.BalanceMonth == 12 ? 1 : record.BalanceMonth + 1;
                    actYear = record.BalanceMonth == 12 ? record.BalanceYear + 1 : record.BalanceYear;
                    actId = record.BalanceID;
                    return true;
                }
                else
                {
                    actMonth = record.BalanceMonth;
                    actYear = record.BalanceYear;
                    actId = record.BalanceID;
                }

                return true;
            }
        }

        /// <summary>
        /// 获取库房当前会计年、月
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="errMsg">错误信息</param>
        /// <param name="actYear">会计年份</param>
        /// <param name="actMonth">会计月份</param>
        /// <param name="actId">月结ID</param>
        /// <returns>会计年、月</returns>
        public bool GetDWAccountTime(int deptID, out string errMsg, out int actYear, out int actMonth, out int actId)
        {
            errMsg = string.Empty;
            DW_BalanceRecord record = NewDao<Dao.IDWDao>().GetMaxBlanceRecord(deptID);
            if (record == null)
            {
                errMsg = "当前药库没有进行初始化月结，请联系管理员";
                actYear = 0;
                actMonth = 0;
                actId = 0;
                return false;
            }
            else
            {
                if (System.DateTime.Now >= record.EndTime)
                {
                    actMonth = record.BalanceMonth == 12 ? 1 : record.BalanceMonth + 1;
                    actYear = record.BalanceMonth == 12 ? record.BalanceYear + 1 : record.BalanceYear;
                    actId = record.BalanceID;
                    return true;
                }
                else
                {
                    actMonth = record.BalanceMonth;
                    actYear = record.BalanceYear;
                    actId = record.BalanceID;
                }

                return true;
            }
        }

        /// <summary>
        /// 保存台账
        /// </summary>
        /// <typeparam name="TBatch">批次类型</typeparam>
        /// <param name="batchs">批次对象</param>
        /// <param name="currentDetail">当前调价明细单对象</param>
        public void SaveAccout<TBatch>(TBatch batchs, DG_AdjDetail currentDetail)
        {
            string errMsg = string.Empty;
            int actYear = 0;
            int actMonth = 0;
            int actId = 0;
            if (batchs.GetType() == typeof(DW_Batch))
            {
                DW_Batch batch = batchs as DW_Batch;
                currentDetail.AdjAmount = batch.BatchAmount;
                batch.RetailPrice = currentDetail.NewRetailPrice;
                this.BindDb(batch);
                int dwresult = batch.save();
                if (dwresult > 0)
                {
                    this.BindDb(currentDetail);
                    int detailresult = currentDetail.save();
                    if (detailresult > 0)
                    {
                        if (!GetDWAccountTime(batch.DeptID, out errMsg, out actYear, out actMonth, out actId))
                        {
                            throw new Exception(errMsg);
                        }

                        DW_Account newaccount = new DW_Account();
                        newaccount.AccountType = 0;
                        newaccount.BalanceFlag = 0;
                        newaccount.BalanceID = actId;
                        newaccount.BalanceMonth = actMonth;
                        newaccount.BalanceYear = actYear;
                        newaccount.BatchNO = batch.BatchNO;
                        newaccount.BillNO = currentDetail.BillNO;
                        newaccount.BusiType = DGConstant.OP_DW_ADJPRICE;
                        newaccount.CTypeID = NewDao<IDWDao>().GetTypeId(batch.BatchNO, batch.DrugID);
                        newaccount.LendRetailFee = currentDetail.NewRetailPrice > currentDetail.OldRetailPrice ? (currentDetail.NewRetailPrice - currentDetail.OldRetailPrice) * currentDetail.AdjAmount : 0;
                        newaccount.DebitRetailFee = currentDetail.NewRetailPrice < currentDetail.OldRetailPrice ? (currentDetail.OldRetailPrice - currentDetail.NewRetailPrice) * currentDetail.AdjAmount : 0;
                        newaccount.OverRetailFee = currentDetail.NewRetailPrice * currentDetail.AdjAmount;
                        newaccount.DebitAmount = 0;
                        newaccount.LendAmount = 0;
                        newaccount.OverAmount = currentDetail.AdjAmount;
                        newaccount.OverStockFee = batch.StockPrice * currentDetail.AdjAmount;
                        newaccount.DebitStockFee = 0;
                        newaccount.LendStockFee = 0;
                        newaccount.DeptID = batch.DeptID;
                        newaccount.DetailID = detailresult;
                        newaccount.DrugID = batch.DrugID;
                        newaccount.UnitName = currentDetail.PackUnitName;
                        newaccount.UnitID = currentDetail.UnitID;
                        newaccount.StockPrice = batch.StockPrice;
                        newaccount.RegTime = DateTime.Now;
                        newaccount.RetailPrice = currentDetail.NewRetailPrice;
                        this.BindDb(newaccount);
                        newaccount.save();
                    }
                }
            }
            else
            {
                DS_Batch batch = batchs as DS_Batch;
                currentDetail.AdjAmount = batch.BatchAmount;
                batch.RetailPrice = currentDetail.NewRetailPrice;
                this.BindDb(batch);
                int dsresult = batch.save();
                if (dsresult > 0)
                {
                    this.BindDb(currentDetail);
                    int detailresult = currentDetail.save();
                    if (detailresult > 0)
                    {
                        if (!GetDSAccountTime(batch.DeptID, out errMsg, out actYear, out actMonth, out actId))
                        {
                            throw new Exception(errMsg);
                        }

                        DS_Account newaccount = new DS_Account();
                        newaccount.AccountType = 0;
                        newaccount.BalanceFlag = 0;
                        newaccount.BalanceID = actId;
                        newaccount.BalanceMonth = actMonth;
                        newaccount.BalanceYear = actYear;
                        newaccount.BatchNO = batch.BatchNO;
                        newaccount.BillNO = currentDetail.BillNO;
                        newaccount.BusiType = DGConstant.OP_DS_ADJPRICE;
                        newaccount.CTypeID = NewDao<IDSDao>().GetTypeId(batch.BatchNO, batch.DrugID); 
                        newaccount.LendRetailFee = (currentDetail.NewRetailPrice > currentDetail.OldRetailPrice) ? ((currentDetail.NewRetailPrice - currentDetail.OldRetailPrice) * (currentDetail.AdjAmount / batch.UnitAmount)) : 0;
                        newaccount.DebitRetailFee = (currentDetail.NewRetailPrice < currentDetail.OldRetailPrice) ? ((currentDetail.OldRetailPrice - currentDetail.NewRetailPrice) * (currentDetail.AdjAmount / batch.UnitAmount)) : 0;
                        newaccount.OverRetailFee = (currentDetail.NewRetailPrice * (currentDetail.AdjAmount / batch.UnitAmount));
                        newaccount.DebitAmount = 0;
                        newaccount.LendAmount = 0;
                        newaccount.OverAmount = currentDetail.AdjAmount;
                        newaccount.OverStockFee = (batch.StockPrice * (currentDetail.AdjAmount / batch.UnitAmount));
                        newaccount.DebitStockFee = 0;
                        newaccount.LendStockFee = 0;
                        newaccount.DeptID = batch.DeptID;
                        newaccount.DetailID = detailresult;
                        newaccount.DrugID = batch.DrugID;
                        newaccount.UnitName = currentDetail.UnitName;
                        newaccount.UnitID = currentDetail.UnitID;
                        newaccount.StockPrice = batch.StockPrice;
                        newaccount.RegTime = DateTime.Now;
                        newaccount.RetailPrice = currentDetail.NewRetailPrice;
                        this.BindDb(newaccount);
                        newaccount.save();
                    }
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <typeparam name="THead">调价表头</typeparam>
        /// <typeparam name="TDetail">调价明细</typeparam>
        /// <param name="billHead">调价表头对象</param>
        /// <param name="billDetails">调价明细对象</param>
        public void SaveBill<THead, TDetail>(THead billHead, List<TDetail> billDetails)
        {
            throw new NotImplementedException();
        }
    }
}