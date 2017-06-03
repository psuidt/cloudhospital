using System;
using System.Collections.Generic;
using System.Data;
using HIS_DrugManger.Dao;
using HIS_DrugManger.ObjectModel.Store;
using HIS_Entity.DrugManage;
using HIS_PublicManage.ObjectModel;

namespace HIS_DrugManger.ObjectModel.Bill
{
    /// <summary>
    /// 药库盘点单处理器
    /// </summary>
    class DWCheckBill : DWBill
    {
        /// <summary>
        /// 审核药库盘点单
        /// </summary>
        /// <param name="deptId">库房ID</param>
        /// <param name="auditEmpID">审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <returns>单据处理结果</returns>
        public override DGBillResult AuditBill(int deptId, int auditEmpID, string auditEmpName)
        {
            string serialNO = string.Empty;//审核单据号

            //1.检查库房状态是否处于盘点状态
            int checkStatus = NewDao<SqlDWDao>().GetStoreRoomStatus(deptId);
            if (checkStatus == 0)
            {
                throw new Exception("系统没有进入盘点状态，请启用盘点状态");
            }

            //2.提取所有未审核的单据返回DataTable;
            DataTable dtNotAuditDetail = NewDao<SqlDWDao>().GetAllNotAuditDetail(deptId);

            //3.创建盘点审核单据头
            DW_AuditHead auditHead = NewObject<DW_AuditHead>();
            decimal profitRetailFee = 0, profitStockFee = 0, lossRetailFee = 0, lossStockFee = 0;
            decimal checkStockFee = 0, actStockFee = 0, checkRetailFee = 0, actRetailFee = 0;
            checkStockFee = Convert.ToDecimal(dtNotAuditDetail.Compute("sum(FactStockFee)", "true"));//盘存进货金额
            actStockFee = Convert.ToDecimal(dtNotAuditDetail.Compute("sum(ActStockFee)", "true")); //账存进货金额
            checkRetailFee = Convert.ToDecimal(dtNotAuditDetail.Compute("sum(FactRetailFee)", "true"));//盘存零售金额
            actRetailFee = Convert.ToDecimal(dtNotAuditDetail.Compute("sum(ActRetailFee)", "true")); //账存零售金额
            profitRetailFee = checkRetailFee - actRetailFee > 0 ? checkRetailFee - actRetailFee : 0;//盘盈零售金额
            profitStockFee = checkStockFee - actStockFee > 0 ? checkStockFee - actStockFee : 0;//盘盈进货金额
            lossRetailFee = checkRetailFee - actRetailFee < 0 ? Math.Abs(checkRetailFee - actRetailFee) : 0; //盘亏零售金额
            lossStockFee = checkStockFee - actStockFee < 0 ? Math.Abs(checkStockFee - actStockFee) : 0;//盘亏进货金额
            serialNO = NewObject<SerialNumberSource>().GetSerialNumber(SnType.药品, deptId, DGConstant.OP_DW_AUDITCHECK);
            auditHead.BillNO = Convert.ToInt64(serialNO);
            auditHead.EmpID = auditEmpID;
            auditHead.EmpName = auditEmpName;
            auditHead.AuditTime = System.DateTime.Now;
            auditHead.Remark = string.Empty;
            auditHead.DelFlag = 0;
            auditHead.AuditFlag = 1;
            auditHead.BusiType = DGConstant.OP_DW_AUDITCHECK;
            auditHead.DeptID = deptId;
            auditHead.ProfitRetailFee = profitRetailFee;
            auditHead.ProfitStockFee = profitStockFee;
            auditHead.LossRetailFee = lossRetailFee;
            auditHead.LossStockFee = lossStockFee;
            auditHead.CheckStockFee = checkStockFee;
            auditHead.ActStockFee = actStockFee;
            auditHead.CheckRetailFee = checkRetailFee;
            auditHead.ActRetailFee = actRetailFee;
            BindDb(auditHead);
            auditHead.save();

            //循环DataTable,根据DataTable的每一行的值去构建盘点审核单明细和盘点审核单头;
            //4.保存盘点审核单表头和明细
            //5、按盘点审核单内容更新药库库存
            DGBillResult result = new DGBillResult();
            foreach (DataRow drNotAuditRow in dtNotAuditDetail.Rows)
            {
                DW_AuditDetail auditdetail = NewObject<DW_AuditDetail>();
                auditdetail.DrugStoreID = Convert.ToInt32(drNotAuditRow["DrugStoreID"]);
                auditdetail.CTypeID = Convert.ToInt32(drNotAuditRow["CTypeID"]);
                auditdetail.DrugID = Convert.ToInt32(drNotAuditRow["DrugID"]);
                auditdetail.Place = drNotAuditRow["Place"].ToString();
                auditdetail.BatchNO = drNotAuditRow["BatchNO"].ToString();
                auditdetail.ValidityDate = Convert.ToDateTime(drNotAuditRow["ValidityDate"]);
                auditdetail.DeptID = deptId;
                auditdetail.BillNO = Convert.ToInt64(serialNO);
                auditdetail.FactAmount = Convert.ToDecimal(drNotAuditRow["FactAmount"]);
                auditdetail.FactStockFee = Convert.ToDecimal(drNotAuditRow["FactStockFee"]);
                auditdetail.FactRetailFee = Convert.ToDecimal(drNotAuditRow["FactRetailFee"]);
                auditdetail.ActAmount = Convert.ToDecimal(drNotAuditRow["ActAmount"]);
                auditdetail.ActStockFee = Convert.ToDecimal(drNotAuditRow["ActStockFee"]);
                auditdetail.ActRetailFee = Convert.ToDecimal(drNotAuditRow["ActRetailFee"]);
                auditdetail.UnitID = Convert.ToInt32(drNotAuditRow["UnitID"]);
                auditdetail.UnitName = drNotAuditRow["UnitName"].ToString();
                auditdetail.RetailPrice = Convert.ToDecimal(drNotAuditRow["RetailPrice"]);
                auditdetail.StockPrice = Convert.ToDecimal(drNotAuditRow["StockPrice"]);
                auditdetail.AuditHeadID = auditHead.AuditHeadID;
                BindDb(auditdetail);
                auditdetail.save();
                StoreParam storeParam = new StoreParam();
                storeParam.Amount = auditdetail.FactAmount - auditdetail.ActAmount;
                storeParam.BatchNO = auditdetail.BatchNO;
                storeParam.DeptID = auditdetail.DeptID;
                storeParam.DrugID = auditdetail.DrugID;
                storeParam.RetailPrice = auditdetail.RetailPrice;
                storeParam.StockPrice = auditdetail.StockPrice;
                storeParam.UnitID = auditdetail.UnitID;
                storeParam.UnitName = auditdetail.UnitName;
                storeParam.ValidityTime = auditdetail.ValidityDate;
                DGStoreResult storeRtn = iStore.AddStore(storeParam);
                if (storeRtn.Result != 0)
                {
                    result.Result = 1;
                    if (storeRtn.Result == 1)
                    {
                        result.LstNotEnough = new List<DGNotEnough>();
                        DGNotEnough notEnough = new DGNotEnough();
                        notEnough.DeptID = auditdetail.DeptID;
                        notEnough.DrugID = auditdetail.DrugID;
                        notEnough.LackAmount = storeRtn.StoreAmount + auditdetail.FactAmount - auditdetail.ActAmount;
                        notEnough.DrugInfo = "药品批次号:" + auditdetail.BatchNO.ToString();
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
                    //6、按盘点审核单内容写入台账
                    WriteAccount(auditHead, auditdetail, storeRtn);
                }
            }

            //7、更新所有未审核的盘点录入单状态
            DW_CheckHead checkHead = NewObject<DW_CheckHead>();
            checkHead.AuditEmpID = auditEmpID;
            checkHead.AuditEmpName = auditEmpName;
            checkHead.AuditHeadID = auditHead.AuditHeadID;
            checkHead.AuditNO = auditHead.BillNO;
            checkHead.DeptID = deptId;
            int ret = NewDao<IDWDao>().UpdateCheckHeadStatus(checkHead);
            if (ret > 0)
            {
                //8、设置库房盘点状态为运营状态
                NewDao<IDGDao>().SetCheckStatus(deptId, 0, 1);
                result.Result = 0;
            }
            else
            {
                result.Result = 1;
            }

            return result;
        }

        /// <summary>
        /// 审核药库单据
        /// </summary>
        /// <param name="headID">药库出库单表头ID</param>
        /// <param name="auditEmpID">审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <param name="workId">机构ID</param>
        /// <returns>单据处理结果</returns>
        public override DGBillResult AuditBill(int headID, int auditEmpID, string auditEmpName, int workId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除药库盘点单
        /// </summary>
        /// <param name="billID">药库盘点单头ID</param>
        public override void DeleteBill(int billID)
        {
            DW_CheckHead inHead = (DW_CheckHead)NewObject<DW_CheckHead>().getmodel(billID);
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
        /// 查询药库盘点单明细表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>药库盘点明细表</returns>
        public override DataTable LoadDetails(Dictionary<string, string> condition)
        {
            return NewDao<IDWDao>().LoadCheckDetail(condition);
        }

        /// <summary>
        /// 清除盘点状态
        /// </summary>
        /// <param name="deptID">库房ID</param>
        public void ClearCheckStatus(int deptID)
        {
            NewDao<IDWDao>().DeleteAllNotAuditCheckHead(deptID);
            NewDao<IDGDao>().SetCheckStatus(deptID, 0, 1);
        }

        /// <summary>
        /// 查询药库盘点头表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>药库盘点单头表</returns>
        public override DataTable LoadHead(Dictionary<string, string> condition)
        {
            return NewDao<IDWDao>().LoadCheckHead(condition);
        }

        /// <summary>
        /// 保存药库盘点录入单
        /// </summary>
        /// <typeparam name="THead">药库盘点单表头模板</typeparam>
        /// <typeparam name="TDetail">药库盘点单明细模板</typeparam>
        /// <param name="billHead">药库盘点单头</param>
        /// <param name="billDetails">药库盘点单明细</param>
        public override void SaveBill<THead, TDetail>(THead billHead, List<TDetail> billDetails)
        {
            DW_CheckHead inHead = billHead as DW_CheckHead;
            List<DW_CheckDetail> inDetals = billDetails as List<DW_CheckDetail>;
            inHead.RegTime = System.DateTime.Now;
            string serialNO = NewObject<SerialNumberSource>().GetSerialNumber(SnType.药品, inHead.DeptID, inHead.BusiType);
            inHead.BillNO = Convert.ToInt64(serialNO);
            BindDb(inHead);
            inHead.save();
            if (inHead.CheckHeadID > 0)
            {
                foreach (DW_CheckDetail detail in inDetals)
                {
                    detail.CheckHeadID = inHead.CheckHeadID;
                    BindDb(detail);
                    detail.save();
                }
            }

            //设置盘点状态：将库房的盘点状态修改成1
            NewDao<IDGDao>().SetCheckStatus(inHead.DeptID, 1, 1);
        }

        /// <summary>
        /// 台账写入
        /// </summary>
        /// <typeparam name="THead">药库盘点单表头模板</typeparam>
        /// <typeparam name="TDetail">药库盘点单明细模板</typeparam>
        /// <param name="billHead">药库盘点单头</param>
        /// <param name="billDetails">药库盘点单明细</param>
        /// <param name="storeResult">处理结果</param>
        public override void WriteAccount<THead, TDetail>(THead billHead, TDetail billDetails, DGStoreResult storeResult)
        {
            DW_AuditDetail detail = billDetails as DW_AuditDetail;
            DW_AuditHead head = billHead as DW_AuditHead;
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
            account.BatchNO = detail.BatchNO;
            account.BusiType = head.BusiType;
            account.CTypeID = detail.CTypeID;
            account.DeptID = head.DeptID;
            account.DetailID = detail.AuditDetailID;
            account.DrugID = detail.DrugID;
            account.UnitID = detail.UnitID;
            account.UnitName = detail.UnitName;
            account.BillNO = head.BillNO;
            account.RegTime = DateTime.Now;

            //盘盈 借方
            //盘亏 贷方
            if (detail.FactAmount - detail.ActAmount > 0)
            {
                account.StockPrice = detail.StockPrice;
                account.RetailPrice = detail.RetailPrice;
                account.LendAmount = detail.FactAmount - detail.ActAmount;
                account.LendRetailFee = (detail.FactAmount - detail.ActAmount) * detail.RetailPrice;
                account.LendStockFee = (detail.FactAmount - detail.ActAmount) * detail.StockPrice;

                account.OverAmount = storeResult.StoreAmount;
                account.OverRetailFee = storeResult.StoreAmount * account.RetailPrice;
                account.OverStockFee = storeResult.StoreAmount * account.StockPrice;
            }
            else
            {
                //贷方
                account.StockPrice = detail.StockPrice;
                account.RetailPrice = detail.RetailPrice;
                account.DebitAmount = detail.ActAmount - detail.FactAmount;
                account.DebitRetailFee = (detail.ActAmount - detail.FactAmount) * detail.RetailPrice;
                account.DebitStockFee = (detail.ActAmount - detail.FactAmount) * detail.StockPrice;
                account.OverAmount = storeResult.StoreAmount;
                account.OverRetailFee = storeResult.StoreAmount * account.RetailPrice;
                account.OverStockFee = storeResult.StoreAmount * account.StockPrice;
            }

            account.save();
        }

        /// <summary>
        /// 提取库存药品
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>盘点单空表数据</returns>
        public DataTable LoadStorageData(Dictionary<string, string> condition)
        {
            return NewDao<IDWDao>().LoadStorageData(condition);
        }

        /// <summary>
        /// 加载盘点审核单表头
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>盘点审核单表头</returns>
        public DataTable LoadAudtiCheckHead(Dictionary<string, string> condition)
        {
            return NewDao<IDWDao>().LoadAudtiCheckHead(condition);
        }

        /// <summary>
        /// 加载盘点审核单明细
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>盘点审核单明细</returns>
        public DataTable LoadAuditCheckDetail(Dictionary<string, string> condition)
        {
            return NewDao<IDWDao>().LoadAuditCheckDetail(condition);
        }

        /// <summary>
        /// 汇总所有未审核单据明细并提取
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="haveBatchNO">是否按批次汇总</param>
        /// <returns>汇总盘点信息</returns>
        public DataTable LoadAllNotAuditDetail(Dictionary<string, string> condition, bool haveBatchNO)
        {
            return NewDao<IDWDao>().LoadAllNotAuditDetail(condition, haveBatchNO);
        }
    }
}
