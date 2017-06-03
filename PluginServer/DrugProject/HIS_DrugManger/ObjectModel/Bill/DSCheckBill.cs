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
    /// 药房盘点单处理器
    /// </summary>
    class DSCheckBill : DSBill
    {
        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="auditEmpID">审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <returns>处理结果</returns>
        public override DGBillResult AuditBill(int deptId, int auditEmpID, string auditEmpName)
        {
            string serialNO = string.Empty;//审核单据号

            //1.检查库房状态是否处于盘点状态
            int checkStatus = NewDao<SqlDSDao>().GetStoreRoomStatus(deptId);
            if (checkStatus == 0)
            {
                throw new Exception("药房系统没有进入盘点状态，请启用盘点状态");
            }

            //2.提取所有未审核的单据返回DataTable;
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            queryCondition.Add("a.DeptID", deptId.ToString());
            DataTable dtNotAuditDetail = LoadAllNotAuditDetail(queryCondition,true);

            //3.创建盘点审核单据头
            DS_AuditHead auditHead = NewObject<DS_AuditHead>();
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
            serialNO = NewObject<SerialNumberSource>().GetSerialNumber(SnType.药品, deptId, DGConstant.OP_DS_AUDITCHECK);
            auditHead.BillNO = Convert.ToInt64(serialNO);
            auditHead.EmpID = auditEmpID;
            auditHead.EmpName = auditEmpName;
            auditHead.AuditTime = System.DateTime.Now;
            auditHead.Remark = string.Empty;
            auditHead.DelFlag = 0;
            auditHead.AuditFlag = 1;
            auditHead.BusiType = DGConstant.OP_DS_AUDITCHECK;
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
                StoreParam storeParam = new StoreParam();               
                storeParam.DeptID = auditHead.DeptID;
                storeParam.DrugID = Convert.ToInt32(drNotAuditRow["DrugID"]);
                storeParam.RetailPrice = Convert.ToDecimal(drNotAuditRow["RetailPrice"]);
                storeParam.StockPrice = Convert.ToDecimal(drNotAuditRow["StockPrice"]);
                storeParam.FactAmount = Convert.ToDecimal(drNotAuditRow["FactAmount"]);
                storeParam.ActAmount  = Convert.ToDecimal(drNotAuditRow["ActAmount"]);
                DGStoreResult storeRtn = NewObject<DSStore>().AddStoreAuto(storeParam,true);
                if (storeRtn.Result != 0)
                {
                    result.Result = 1;
                    if (storeRtn.Result == 1)
                    {
                        result.LstNotEnough = new List<DGNotEnough>();
                        DGNotEnough notEnough = new DGNotEnough();
                        notEnough.DeptID =Convert.ToInt32(drNotAuditRow["DeptID"]);
                        notEnough.DrugID = Convert.ToInt32(drNotAuditRow["DrugID"]);
                        notEnough.DrugInfo = "药品名称:" + drNotAuditRow["ChemName"].ToString();
                        result.LstNotEnough.Add(notEnough);
                        result.ErrMsg = "【" + notEnough.DrugInfo + "】保存错误";
                    }
                    else
                    {
                        result.ErrMsg = "药品更新库存出错";
                    }

                    return result;
                }
                else
                {
                    foreach (DGBatchAllot batchAllot in storeRtn.BatchAllotList)
                    {
                        DS_AuditDetail auditdetail = NewObject<DS_AuditDetail>();
                        auditdetail.StorageID = Convert.ToInt32(drNotAuditRow["StorageID"]);
                        auditdetail.CTypeID = Convert.ToInt32(drNotAuditRow["CTypeID"]);
                        auditdetail.DrugID = Convert.ToInt32(drNotAuditRow["DrugID"]);
                        auditdetail.Place = drNotAuditRow["Place"].ToString();
                        auditdetail.DeptID = auditHead.DeptID;
                        auditdetail.BillNO = auditHead.BillNO;                     

                        auditdetail.UnitID = Convert.ToInt32(drNotAuditRow["UnitID"]);
                        auditdetail.UnitName = drNotAuditRow["UnitName"].ToString();
                        auditdetail.PackUnit = drNotAuditRow["PackUnit"].ToString();
                        auditdetail.UnitAmount = Convert.ToInt32(drNotAuditRow["UnitAmount"].ToString());
                        auditdetail.AuditHeadID = auditHead.AuditHeadID;

                        auditdetail.BatchNO = batchAllot.BatchNO;
                        auditdetail.ValidityDate = batchAllot.ValidityDate;
                        auditdetail.RetailPrice = batchAllot.RetailPrice;
                        auditdetail.StockPrice = batchAllot.StockPrice;
                        auditdetail.FactAmount = batchAllot.FactAmount;
                        auditdetail.FactStockFee = batchAllot.FactStockFee;
                        auditdetail.FactRetailFee = batchAllot.FactRetailFee;
                        auditdetail.ActAmount = batchAllot.ActAmount;
                        auditdetail.ActStockFee = batchAllot.ActStockFee;
                        auditdetail.ActRetailFee = batchAllot.ActRetailFee;
                        BindDb(auditdetail);
                        auditdetail.save();

                        //写台账表
                        WriteAccount(auditHead, auditdetail, batchAllot);
                    }
                }
            }

            //7、更新所有未审核的盘点录入单状态
            DS_CheckHead checkHead = NewObject<DS_CheckHead>();
            checkHead.AuditEmpID = auditEmpID;
            checkHead.AuditEmpName = auditEmpName;
            checkHead.AuditHeadID = auditHead.AuditHeadID;
            checkHead.AuditNO = (int)auditHead.BillNO;
            checkHead.DeptID = deptId;
            int ret = NewDao<IDSDao>().UpdateCheckHeadStatus(checkHead);
            if (ret > 0)
            {
                //8、设置库房盘点状态为运营状态
                NewDao<IDGDao>().SetCheckStatus(deptId, 0, 0);
                result.Result = 0;
            }
            else
            {
                result.Result = 1;
            }

            return result;
        }

        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="headID">单据头表ID</param>
        /// <param name="auditEmpID">审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <param name="workId">机构ID</param>
        /// <returns>单据结果对象</returns>
        public override DGBillResult AuditBill(int headID, int auditEmpID, string auditEmpName, int workId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除药房盘点单
        /// </summary>
        /// <param name="billID">药房盘点单头ID</param>
        public override void DeleteBill(int billID)
        {
            DS_CheckHead inHead = (DS_CheckHead)NewObject<DS_CheckHead>().getmodel(billID);
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
        /// 查询药房盘点单明细表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>药房盘点明细表</returns>
        public override DataTable LoadDetails(Dictionary<string, string> condition)
        {
            return NewDao<IDSDao>().LoadCheckDetail(condition);
        }

        /// <summary>
        /// 查询药房盘点头表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>药房盘点单头表</returns>
        public override DataTable LoadHead(Dictionary<string, string> condition)
        {
            return NewDao<IDSDao>().LoadCheckHead(condition);
        }

        /// <summary>
        /// 保存药房盘点录入单
        /// </summary>
        /// <typeparam name="THead">药房盘点单表头模板</typeparam>
        /// <typeparam name="TDetail">药房盘点单明细模板</typeparam>
        /// <param name="billHead">药房盘点单头</param>
        /// <param name="billDetails">药房盘点单明细</param>
        public override void SaveBill<THead, TDetail>(THead billHead, List<TDetail> billDetails)
        {
            DS_CheckHead inHead = billHead as DS_CheckHead;
            List<DS_CheckDetail> inDetals = billDetails as List<DS_CheckDetail>;
            inHead.RegTime = System.DateTime.Now;
            string serialNO = NewObject<SerialNumberSource>().GetSerialNumber(SnType.药品, inHead.DeptID, inHead.BusiType);
            inHead.BillNO = Convert.ToInt64(serialNO);
            BindDb(inHead);
            inHead.save();
            if (inHead.CheckHeadID > 0)
            {
                foreach (DS_CheckDetail detail in inDetals)
                {
                    detail.CheckHeadID = inHead.CheckHeadID;
                    BindDb(detail);
                    detail.save();
                }
            }

            //设置盘点状态：将库房的盘点状态修改成1
            NewDao<IDGDao>().SetCheckStatus(inHead.DeptID, 1,0);
        }

        /// <summary>
        /// 清除盘点状态
        /// </summary>
        /// <param name="deptID">库房id</param>
        public void ClearCheckStatus(int deptID)
        {
            NewDao<IDSDao>().DeleteAllNotAuditCheckHead(deptID);
            NewDao<IDGDao>().SetCheckStatus(deptID, 0,0);
        }

        /// <summary>
        /// 台账写入
        /// </summary>
        /// <typeparam name="THead">药房盘点单表头模板</typeparam>
        /// <typeparam name="TDetail">药房盘点单明细模板</typeparam>
        /// <param name="billHead">药房盘点单头</param>
        /// <param name="billDetails">药房盘点单明细</param>
        /// <param name="storeResult">结果对象</param>
        public override void WriteAccount<THead, TDetail>(THead billHead, TDetail billDetails, DGStoreResult storeResult)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 台账写入
        /// </summary>
        /// <typeparam name="THead">药房盘点单表头模板</typeparam>
        /// <typeparam name="TDetail">药房盘点单明细模板</typeparam>
        /// <param name="billHead">药房盘点单头</param>
        /// <param name="billDetails">药房盘点单明细</param>
        /// <param name="batchAllot">批次分类对象</param>
        public void WriteAccount<THead, TDetail>(THead billHead, TDetail billDetails, DGBatchAllot batchAllot)
        {
            DS_AuditDetail detail = billDetails as DS_AuditDetail;
            DS_AuditHead head = billHead as DS_AuditHead;
            int actYear;
            int actMonth;
            string errMsg;
            if (!GetAccountTime(head.DeptID, out errMsg, out actYear, out actMonth))
            {
                throw new Exception(errMsg);
            }

            DS_Account account = NewObject<DS_Account>();
            account.BalanceYear = actYear;
            account.BalanceMonth = actMonth;
            account.AccountType = 0;
            account.BalanceFlag = 0;
            account.RegTime= System.DateTime.Now;
            account.BillNO = head.BillNO;
            account.BatchNO = detail.BatchNO;
            account.BusiType = head.BusiType;
            account.CTypeID = detail.CTypeID;
            account.DeptID = head.DeptID;
            account.DetailID = detail.AuditDetailID;
            account.DrugID = detail.DrugID;
            account.UnitID = detail.UnitID;
            account.UnitName = detail.UnitName;

            //盘盈 借方
            //盘亏 贷方
            if (detail.FactAmount - detail.ActAmount > 0)
            {
                //借方
                account.StockPrice = detail.StockPrice;
                account.RetailPrice = detail.RetailPrice;
                account.LendAmount = detail.FactAmount - detail.ActAmount;
                account.LendRetailFee = detail.FactRetailFee - detail.ActRetailFee;
                account.LendStockFee = detail.FactStockFee - detail.ActStockFee;
                account.OverAmount = batchAllot.StoreAmount;
                account.OverRetailFee = batchAllot.StoreAmount * (batchAllot.RetailPrice/ batchAllot.UnitAmount);
                account.OverStockFee = batchAllot.StoreAmount * (batchAllot.StockPrice / batchAllot.UnitAmount);
            }
            else 
            {
                //贷方
                account.StockPrice = detail.StockPrice;
                account.RetailPrice = detail.RetailPrice;
                account.DebitAmount = detail.ActAmount - detail.FactAmount;
                account.DebitRetailFee = detail.ActRetailFee - detail.FactRetailFee;
                account.DebitStockFee = detail.ActStockFee - detail.FactStockFee;
                account.OverAmount = batchAllot.StoreAmount;
                account.OverRetailFee = batchAllot.StoreAmount * (batchAllot.RetailPrice / batchAllot.UnitAmount);
                account.OverStockFee = batchAllot.StoreAmount * (batchAllot.StockPrice / batchAllot.UnitAmount);
            }

            account.save();
        }

        /// <summary>
        /// 提取库存药品
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>盘点单空表数据</returns>
        public DataTable LoadStorageData(Dictionary<string , string> condition)
        {
           return NewDao<IDSDao>().LoadStorageData(condition);
        }

        /// <summary>
        /// 加载盘点审核单表头
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>盘点审核单表头</returns>
        public DataTable LoadAudtiCheckHead(Dictionary<string, string> condition)
        {
            return NewDao<IDSDao>().LoadAudtiCheckHead(condition);
        }

        /// <summary>
        /// 加载盘点审核单明细
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>盘点审核单明细</returns>
        public DataTable LoadAuditCheckDetail(Dictionary<string, string> condition)
        {
            return NewDao<IDSDao>().LoadAuditCheckDetail(condition);
        }

        /// <summary>
        /// 汇总所有未审核单据明细并提取
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="haveBatchNO">是否按批次汇总</param>
        /// <returns>汇总盘点信息</returns>
        public DataTable LoadAllNotAuditDetail(Dictionary<string, string> condition, bool haveBatchNO)
        {
            DataTable dtNotAutditDetail =  NewDao<IDSDao>().LoadAllNotAuditDetail(condition,haveBatchNO);

            //调用算法计算账存金额，盘存金额（4个金额）循环更新DataTable
            foreach (DataRow row in dtNotAutditDetail.Rows)
            {
                //账存零售金额
                decimal actRetailFee = 0;

                //账存进货金额
                decimal actStockFee = 0;
                int storageId = Convert.ToInt32(row["StorageID"]);

                //账存数
                decimal actAmount  = Convert.ToDecimal(row["ActAmount"]);

                //盘存数
                decimal factAmount = Convert.ToDecimal(row["FactAmount"]);
                decimal[] actFees = NewDao<IDSDao>().CalculateBatchActFee(storageId);
                actRetailFee = actFees[0];
                actStockFee = actFees[1];

                //设置账存零售金额
                row["ActRetailFee"] = actRetailFee;

                //设置账存进货金额
                row["ActStockFee"] = actStockFee;
                if (actAmount == factAmount)
                {
                    //设置盘存零售金额
                    row["FactRetailFee"] = actRetailFee;

                    //设置盘存进货金额
                    row["FactStockFee"] = actStockFee;
                }
                else
                {
                    //按照先进先出原则计算盘存金额
                    decimal[] factFees = CalculateBatchFactFee(storageId, actAmount, factAmount);

                    //设置盘存零售金额
                    row["FactRetailFee"] = factFees[0];

                    //设置盘存进货金额
                    row["FactStockFee"] = factFees[1];
                }
            }

            return dtNotAutditDetail;
        }

        /// <summary>
        /// 计算批次盘存金额
        /// </summary>
        /// <param name="storageId">库存Id</param>
        /// <param name="actAmount">账存数量</param>
        /// <param name="factAmount">盘存数量</param>
        /// <returns>零售金额，进货金额数组</returns>
        private decimal[] CalculateBatchFactFee(int storageId, decimal actAmount, decimal factAmount)
        {
            decimal totalRetailFee = 0;
            decimal totalStockFee = 0;
            decimal[] factFees = new decimal[2];
            DataTable dtBatch = NewDao<IDSDao>().GetStorageBatch(storageId);
            decimal tempAmount = factAmount;
            decimal profitAmount = factAmount - actAmount;           
            for (int i = dtBatch.Rows.Count-1; i >= 0; i--)
            {
                decimal batchAmount = Convert.ToDecimal(dtBatch.Rows[i]["BatchAmount"]);
                decimal retailPrice = Convert.ToDecimal(dtBatch.Rows[i]["RetailPrice"]);
                decimal stockPrice = Convert.ToDecimal(dtBatch.Rows[i]["StockPrice"]);
                decimal unitAmount = Convert.ToDecimal(dtBatch.Rows[i]["UnitAmount"]); 
                if (tempAmount >= batchAmount)
                {
                    //按照批次数计算金额
                    totalRetailFee += CalFactFee(retailPrice, unitAmount, batchAmount);
                    totalStockFee+= CalFactFee(stockPrice, unitAmount, batchAmount);
                    tempAmount -= batchAmount;
                }
                else
                {
                    //按照剩余数算
                    totalRetailFee += CalFactFee(retailPrice, unitAmount, tempAmount);
                    totalStockFee += CalFactFee(stockPrice, unitAmount, tempAmount);
                    tempAmount = 0;
                }
            }

            //盘赢
            if (factAmount > actAmount && tempAmount>0)
            {
                decimal batchAmount = Convert.ToDecimal(dtBatch.Rows[dtBatch.Rows.Count - 1]["BatchAmount"]);
                decimal retailPrice = Convert.ToDecimal(dtBatch.Rows[dtBatch.Rows.Count - 1]["RetailPrice"]);
                decimal stockPrice = Convert.ToDecimal(dtBatch.Rows[dtBatch.Rows.Count - 1]["StockPrice"]);
                decimal unitAmount = Convert.ToDecimal(dtBatch.Rows[dtBatch.Rows.Count - 1]["UnitAmount"]);

                //累计到最后一批profitAmount
                totalRetailFee += CalFactFee(retailPrice, unitAmount, factAmount - actAmount);
                totalStockFee += CalFactFee(stockPrice, unitAmount, factAmount - actAmount);
            }

            //将金额取两位小数
            factFees[0] = Math.Round(totalRetailFee, 2);
            factFees[1] = Math.Round(totalStockFee, 2);
            return factFees;
        }

        /// <summary>
        /// 计算金额
        /// </summary>
        /// <param name="price">包装单位价格</param>
        /// <param name="unitAmount">包装系数</param>
        /// <param name="amount">基本单位数量</param>
        /// <returns>计算金额数值</returns>
        private decimal CalFactFee(decimal price, decimal unitAmount, decimal amount)
        {
            decimal dResult = (price / unitAmount) * amount;
            return dResult;
        }
    }
}
