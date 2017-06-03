using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MaterialManage;
using HIS_Entity.SqlAly;
using HIS_MaterialManager.Dao;
using HIS_MaterialManager.ObjectModel.Store;
using HIS_PublicManage.ObjectModel;

namespace HIS_MaterialManager.ObjectModel.Bill
{
    /// <summary>
    /// 物资库盘点单处理器
    /// </summary>
    class MwCheckBill : AbstractObjectModel, IMwBill
    {
        /// <summary>
        /// 审核物资库盘点单
        /// </summary>
        /// <param name="deptId">库房ID</param>
        /// <param name="auditEmpID">审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <returns>单据处理结果</returns>
        public  MWBillResult AuditBill(int deptId, int auditEmpID, string auditEmpName)
        {
            string serialNO = string.Empty;//审核单据号
            //1.检查库房状态是否处于盘点状态
            int checkStatus = NewDao<SqlMWDao>().GetStoreRoomStatus(deptId);
            if (checkStatus == 0)
            {
                throw new Exception("系统没有进入盘点状态，请启用盘点状态");
            }

            //2.提取所有未审核的单据返回DataTable;
            DataTable dtNotAuditDetail = NewDao<SqlMWDao>().GetAllNotAuditDetail(deptId);
            //3.创建盘点审核单据头
            MW_AuditHead auditHead = NewObject<MW_AuditHead>();
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
            serialNO = NewObject<SerialNumberSource>().GetSerialNumber(SnType.物资, deptId, MWConstant.OP_MW_AUDITCHECK);
            auditHead.BillNO = Convert.ToInt64(serialNO);
            auditHead.EmpID = auditEmpID;
            auditHead.EmpName = auditEmpName;
            auditHead.AuditTime = System.DateTime.Now;
            auditHead.Remark = string.Empty;
            auditHead.DelFlag = 0;
            auditHead.AuditFlag = 1;
            auditHead.BusiType = MWConstant.OP_MW_AUDITCHECK;
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
            //5、按盘点审核单内容更新物资库库存
            MWBillResult result = new MWBillResult();
            foreach (DataRow drNotAuditRow in dtNotAuditDetail.Rows)
            {
                MW_AuditDetail auditdetail = NewObject<MW_AuditDetail>();
                auditdetail.StorageID = Convert.ToInt32(drNotAuditRow["StorageID"]);              
                auditdetail.MaterialID = Convert.ToInt32(drNotAuditRow["MaterialID"]);
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
                storeParam.MaterialId = auditdetail.MaterialID;
                storeParam.RetailPrice = auditdetail.RetailPrice;
                storeParam.StockPrice = auditdetail.StockPrice;
                storeParam.UnitID = auditdetail.UnitID;
                storeParam.UnitName = auditdetail.UnitName;
                storeParam.ValidityTime = auditdetail.ValidityDate;
                MWStoreResult storeRtn = NewObject<MwStore>().AddStore(storeParam);
                if (storeRtn.Result != 0)
                {
                    result.Result = 1;
                    if (storeRtn.Result == 1)
                    {
                        result.LstNotEnough = new List<MWNotEnoughInfo>();
                        MWNotEnoughInfo notEnough = new MWNotEnoughInfo();
                        notEnough.DeptID = auditdetail.DeptID;
                        notEnough.MaterialId = auditdetail.MaterialID;
                        notEnough.LackAmount = storeRtn.StoreAmount + auditdetail.FactAmount - auditdetail.ActAmount;
                        notEnough.MaterialInfo = "物资批次号:" + auditdetail.BatchNO.ToString();
                        result.LstNotEnough.Add(notEnough);
                        result.ErrMsg = "【" + notEnough.MaterialInfo + "】库存不足";
                    }
                    else
                    {
                        result.ErrMsg = "物资更新库存出错";
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
            MW_CheckHead checkHead = NewObject<MW_CheckHead>();
            checkHead.AuditEmpID = auditEmpID;
            checkHead.AuditEmpName = auditEmpName;
            checkHead.AuditHeadID = auditHead.AuditHeadID;
            checkHead.AuditNO = auditHead.BillNO;
            checkHead.DeptID = deptId;
            int ret = NewDao<IMWDao>().UpdateCheckHeadStatus(checkHead);
            if (ret > 0)
            {
                //8、设置库房盘点状态为运营状态
                NewDao<IMWDao>().SetCheckStatus(deptId, 0);
                result.Result = 0;
            }
            else
            {
                result.Result = 1;
            }

            return result;
        }

        /// <summary>
        /// 删除物资库盘点单
        /// </summary>
        /// <param name="billID">物资库盘点单头ID</param>
        public  void DeleteBill(int billID)
        {
            MW_CheckHead inHead = (MW_CheckHead)NewObject<MW_CheckHead>().getmodel(billID);
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
        /// 查询物资库盘点单明细表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>物资库盘点明细表</returns>
        public  DataTable LoadDetails(Dictionary<string, string> condition)
        {
            return NewDao<IMWDao>().LoadCheckDetail(condition);
        }

        /// <summary>
        /// 清除盘点状态
        /// </summary>
        /// <param name="deptID">库房ID</param>
        public void ClearCheckStatus(int deptID)
        {
            NewDao<IMWDao>().DeleteAllNotAuditCheckHead(deptID);
            NewDao<IMWDao>().SetCheckStatus(deptID, 0);
        }

        /// <summary>
        /// 查询物资库盘点头表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>物资库盘点单头表</returns>
        public  DataTable LoadHead(Dictionary<string, string> condition)
        {
            return NewDao<IMWDao>().LoadCheckHead(condition);
        }

        /// <summary>
        /// 保存盘点录入单
        /// </summary>
        /// <typeparam name="THead">物资库盘点单表头模板</typeparam>
        /// <typeparam name="TDetail">物资库盘点单明细模板</typeparam>
        /// <param name="billHead">物资库盘点单头</param>
        /// <param name="billDetails">物资库盘点单明细</param>
        public  void SaveBill<THead, TDetail>(THead billHead, List<TDetail> billDetails)
        {
            MW_CheckHead inHead = billHead as MW_CheckHead;
            List<MW_CheckDetail> inDetals = billDetails as List<MW_CheckDetail>;
            inHead.RegTime = System.DateTime.Now;
            string serialNO = NewObject<SerialNumberSource>().GetSerialNumber(SnType.物资, inHead.DeptID, inHead.BusiType);
            inHead.BillNO = Convert.ToInt64(serialNO);
            BindDb(inHead);
            inHead.save();
            if (inHead.CheckHeadID > 0)
            {
                foreach (MW_CheckDetail detail in inDetals)
                {
                    detail.CheckHeadID = inHead.CheckHeadID;
                    BindDb(detail);
                    detail.save();
                }
            }

            //设置盘点状态：将库房的盘点状态修改成1
            NewDao<IMWDao>().SetCheckStatus(inHead.DeptID, 1);
        }

        /// <summary>
        /// 台账写入
        /// </summary>
        /// <typeparam name="THead">物资库盘点单表头模板</typeparam>
        /// <typeparam name="TDetail">物资库盘点单明细模板</typeparam>
        /// <param name="billHead">物资库盘点单头</param>
        /// <param name="billDetails">物资库盘点单明细</param>
        /// <param name="storeResult">库存返回结果</param>
        public  void WriteAccount<THead, TDetail>(THead billHead, TDetail billDetails, MWStoreResult storeResult)
        {
            MW_AuditDetail detail = billDetails as MW_AuditDetail;
            MW_AuditHead head = billHead as MW_AuditHead;
            int actYear;
            int actMonth;
            string errMsg;
            if (!GetAccountTime(head.DeptID, out errMsg, out actYear, out actMonth))
            {
                throw new Exception(errMsg);
            }

            MW_Account account = NewObject<MW_Account>();
            account.BalanceYear = actYear;
            account.BalanceMonth = actMonth;
            account.AccountType = 0;
            account.BalanceFlag = 0;
            account.BatchNO = detail.BatchNO;
            account.BusiType = head.BusiType;
            account.DeptID = head.DeptID;
            account.DetailID = detail.AuditDetailID;
            account.MaterialID = detail.MaterialID;
            account.UnitID = detail.UnitID;
            account.UnitName = detail.UnitName;
            account.BillNO = head.BillNO;
            account.RegTime = DateTime.Now;
            //盘盈 借方
            //盘亏 贷方
            //借方
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
        /// 获取库房当前会计年、月
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="errMsg">错误信息</param>
        /// <param name="actYear">会计年份</param>
        /// <param name="actMonth">会计月份</param>
        /// <returns>false未月结</returns>
        public bool GetAccountTime(int deptID, out string errMsg, out int actYear, out int actMonth)
        {
            errMsg = string.Empty;
            MW_BalanceRecord record = NewDao<IMWDao>().GetMaxBlanceRecord(deptID);
            if (record == null)
            {
                errMsg = "当前库房没有进行初始化月结，请联系管理员";
                actYear = 0;
                actMonth = 0;
                return false;
            }
            else
            {
                if (System.DateTime.Now >= record.EndTime)
                {
                    actMonth = record.BalanceMonth == 12 ? 1 : record.BalanceMonth + 1;
                    actYear = record.BalanceMonth == 12 ? record.BalanceYear + 1 : record.BalanceYear;
                    return true;
                }
                else
                {
                    actMonth = record.BalanceMonth;
                    actYear = record.BalanceYear;
                }

                return true;
            }
        }

        /// <summary>
        /// 提取库存药品
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>盘点单空表数据</returns>
        public DataTable LoadStorageData(Dictionary<string, string> condition)
        {
            return NewDao<IMWDao>().LoadStorageData(condition);
        }

        /// <summary>
        /// 加载盘点审核单表头
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>盘点审核单表头</returns>
        public DataTable LoadAudtiCheckHead(Dictionary<string, string> condition)
        {
            return NewDao<IMWDao>().LoadAudtiCheckHead(condition);
        }

        /// <summary>
        /// 加载盘点审核单明细
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>盘点审核单明细</returns>
        public DataTable LoadAuditCheckDetail(Dictionary<string, string> condition)
        {
            return NewDao<IMWDao>().LoadAuditCheckDetail(condition);
        }

        /// <summary>
        /// 汇总所有未审核单据明细并提取
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="haveBatchNO">是否按批次汇总</param>
        /// <returns>汇总盘点信息</returns>
        public DataTable LoadAllNotAuditDetail(Dictionary<string, string> condition, bool haveBatchNO)
        {
            return NewDao<IMWDao>().LoadAllNotAuditDetail(condition, haveBatchNO);
        }

        /// <summary>
        /// 加载头信息
        /// </summary>
        /// <param name="andWhere">查询条件</param>
        /// <returns>头信息</returns>
        public DataTable LoadHead(List<Tuple<string, string, SqlOperator>> andWhere = null)
        {
            throw new NotImplementedException();
        }
    }
}
