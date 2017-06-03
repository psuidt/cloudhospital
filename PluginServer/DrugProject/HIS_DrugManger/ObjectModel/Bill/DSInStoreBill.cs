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
    /// 药房入库单处理器
    /// </summary>
    class DSInStoreBill : DSBill
    {
        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="headID">药房入库单表头ID</param>
        /// <param name="auditEmpID">审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <returns>处理结果</returns>
        public override DGBillResult AuditBill(int headID, int auditEmpID, string auditEmpName)
        {
            DS_InstoreHead head = (DS_InstoreHead)NewObject<DS_InstoreHead>().getmodel(headID);
            DGBillResult result = new DGBillResult();
            if (head != null)
            {
                List<DS_InStoreDetail> lstDetails = NewObject<DS_InStoreDetail>().getlist<DS_InStoreDetail>("InHeadID=" + headID);
                head.AuditEmpID = auditEmpID;
                head.AuditEmpName = auditEmpName;
                head.AuditTime = System.DateTime.Now;
                head.AuditFlag = 1;
                head.save();

                foreach (DS_InStoreDetail detail in lstDetails)
                {
                    StoreParam storeParam = new StoreParam();
                    storeParam.BatchNO = detail.BatchNO;
                    storeParam.DeptID = head.DeptID;
                    storeParam.DrugID = detail.DrugID;
                    decimal packAmount = GetPackAmount(detail.DrugID);
                    storeParam.RetailPrice = detail.RetailPrice;
                    storeParam.StockPrice = detail.StockPrice;
                    storeParam.UnitID = detail.UnitID;
                    storeParam.UnitName = detail.UnitName;
                    storeParam.ValidityTime = detail.ValidityDate;
                    storeParam.UnitAmount = detail.UnitAmount;
                    storeParam.Amount = detail.Amount;//包装数*系数+基本单位数
                    storeParam.PackUnit = detail.PackUnit;
                    storeParam.BussConstant = head.BusiType;
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
                            notEnough.DrugInfo = "药品编号" + detail.DrugID + "药品批次号:" + detail.BatchNO;
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
                        WriteAccount(head, detail, storeRtn, packAmount);
                    }
                }

                result.Result = 0;
                return result;
            }

            return result;
        }

        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="headID">药房入库单表头ID</param>
        /// <param name="auditEmpID">审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <param name="workId">机构ID</param>
        /// <returns>单据结果对象</returns>
        public override DGBillResult AuditBill(int headID, int auditEmpID, string auditEmpName, int workId)
        {
            DS_InstoreHead head = (DS_InstoreHead)NewObject<DS_InstoreHead>().getmodel(headID);
            DGBillResult result = new DGBillResult();

            if (!NewObject<DrugDeptMgr>().IsDeptChecked(head.DeptID, workId))
            {
                result.Result = 1;
                result.ErrMsg = "当前科室处于盘点状态或者没有设置科室的盘点状态 不能处理业务操作";
                return result;
            }

            if (head != null)
            {
                if (head.AuditFlag == 1)
                {
                    result.Result = 1;
                    result.ErrMsg = "当前数据已经被审核，请确认";
                    return result;
                }

                List<DS_InStoreDetail> lstDetails = NewObject<DS_InStoreDetail>().getlist<DS_InStoreDetail>("InHeadID=" + headID);
                head.AuditEmpID = auditEmpID;
                head.AuditEmpName = auditEmpName;
                head.AuditTime = System.DateTime.Now;
                head.AuditFlag = 1;
                head.save();

                foreach (DS_InStoreDetail detail in lstDetails)
                {
                    DS_Batch batch = NewDao<IDSDao>().GetBatchAmount(Convert.ToInt32(head.DeptID), detail.DrugID, detail.BatchNO);
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
                                result.ErrMsg = "编码【" + detail.DrugID.ToString() + "的（进货价/零售价）】与【" + batch.BatchNO + "】批次价格不一致(进货价：" + batch.StockPrice.ToString()
                                + ",零售价:" + batch.RetailPrice.ToString() + ")，请核查库存！";

                                return result;
                            }
                        }
                    }

                    StoreParam storeParam = new StoreParam();

                    storeParam.BatchNO = detail.BatchNO;
                    storeParam.DeptID = head.DeptID;
                    storeParam.DrugID = detail.DrugID;
                    decimal packAmount = GetPackAmount(detail.DrugID);
                    storeParam.RetailPrice = detail.RetailPrice;
                    storeParam.StockPrice = detail.StockPrice;
                    storeParam.UnitID = detail.UnitID;
                    storeParam.UnitName = detail.UnitName;
                    storeParam.ValidityTime = detail.ValidityDate;
                    storeParam.UnitAmount = Convert.ToInt32(packAmount);
                    storeParam.Amount = detail.Amount;//包装数*系数+基本单位数
                    storeParam.PackAmount = Convert.ToInt32(packAmount);
                    storeParam.PackUnit = detail.PackUnit;
                    storeParam.BussConstant = head.BusiType;
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
                            notEnough.DrugInfo = "药品编号" + detail.DrugID + "药品批次号:" + detail.BatchNO;
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
                        DGStoreResult vStoreRtn = NewObject<DSStore>().AddValidStore(storeParam);

                        WriteAccount(head, detail, storeRtn, packAmount);
                    }
                }

                result.Result = 0;
                return result;
            }

            return result;
        }

        /// <summary>
        /// 删除药房入库单
        /// </summary>
        /// <param name="billID">药房入库单表头ID</param>
        public override void DeleteBill(int billID)
        {
            DS_InstoreHead inHead = (DS_InstoreHead)NewObject<DS_InstoreHead>().getmodel(billID);
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
        /// 查询药房入库单明细
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>药房入库单明细列表</returns>
        public override DataTable LoadDetails(Dictionary<string, string> condition)
        {
            //写SQL查询单据明细
            return NewDao<IDSDao>().LoadInStoreDetail(condition);
        }

        /// <summary>
        /// 查询药房入库单头表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>药房入库单表头</returns>
        public override DataTable LoadHead(Dictionary<string, string> condition)
        {
            //写SQL查询单据头
            return NewDao<IDSDao>().LoadInStoreHead(condition);
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
            DS_InstoreHead inHead = billHead as DS_InstoreHead;
            List<DS_InStoreDetail> inDetals = billDetails as List<DS_InStoreDetail>;
            inHead.RegTime = System.DateTime.Now;
            string serialNO = NewObject<SerialNumberSource>().GetSerialNumber(SnType.药品, inHead.DeptID, inHead.BusiType);
            inHead.BillNO = Convert.ToInt64(serialNO);
            BindDb(inHead);
            inHead.save();
            if (inHead.InHeadID > 0)
            {
                foreach (DS_InStoreDetail detail in inDetals)
                {
                    detail.InHeadID = inHead.InHeadID;
                    detail.BillNO = inHead.BillNO;
                    detail.DeptID = inHead.DeptID;
                    BindDb(detail);
                    detail.save();
                }
            }
        }

        /// <summary>
        /// 台账写入
        /// </summary>
        /// <typeparam name="THead">药房入库单表头模板</typeparam>
        /// <typeparam name="TDetail">药房入库单明细模板</typeparam>
        /// <param name="billHead">药房入库单表头</param>
        /// <param name="billDetails">药房入库单明细</param>
        /// <param name="storeResult">库存处理结果</param>
        /// <param name="packAmount">包装系数</param>
        public void WriteAccount<THead, TDetail>(THead billHead, TDetail billDetails, DGStoreResult storeResult, decimal packAmount)
        {
            //1、构建台账表实体；
            //2、根据台账表内容填写相应的信息；
            //3、保存台账表
            DS_InStoreDetail detail = billDetails as DS_InStoreDetail;
            DS_InstoreHead head = billHead as DS_InstoreHead;

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
            account.DetailID = detail.InDetailID;
            account.DrugID = detail.DrugID;
            account.UnitID = detail.UnitID;
            account.UnitName = detail.UnitName;
            account.RegTime = System.DateTime.Now;
            account.UnitAmount = Convert.ToInt32(packAmount);

            if (head.BusiType == DGConstant.OP_DS_RETURNSOTRE)
            {
                account.StockPrice = storeResult.BatchAllot[0].StockPrice;
                account.RetailPrice = storeResult.BatchAllot[0].RetailPrice;
                account.LendAmount = detail.Amount;
                account.LendRetailFee = detail.Amount * (account.RetailPrice / packAmount);
                account.LendStockFee = detail.Amount * (account.StockPrice / packAmount);
            }
            else
            {
                account.StockPrice = detail.StockPrice;
                account.RetailPrice = detail.RetailPrice;
                account.LendAmount = detail.Amount;
                account.LendRetailFee =
                                        detail.Amount * (account.RetailPrice / packAmount);
                account.LendStockFee = detail.Amount * (account.StockPrice / packAmount);
            }

            account.OverAmount = storeResult.BatchAllot[0].StoreAmount;
            account.OverStockFee = storeResult.BatchAllot[0].StoreAmount * (account.StockPrice / packAmount);
            account.OverRetailFee = storeResult.BatchAllot[0].StoreAmount * (account.RetailPrice / packAmount);
            account.save();
        }

        /// <summary>
        /// 台账写入
        /// </summary>
        /// <typeparam name="THead">药房入库单表头模板</typeparam>
        /// <typeparam name="TDetail">药房入库单明细模板</typeparam>
        /// <param name="billHead">药房入库单表头</param>
        /// <param name="billDetails">药房入库单明细</param>
        /// <param name="storeResult">库存处理结果</param>
        public override void WriteAccount<THead, TDetail>(THead billHead, TDetail billDetails, DGStoreResult storeResult)
        {
            WriteAccount(billHead, billDetails, storeResult, 0);
        }

        /// <summary>
        /// 获取药品的包装换算系数
        /// </summary>
        /// <param name="drugId">药品ID</param>
        /// <returns>包装换算系数</returns>
        public decimal GetPackAmount(int drugId)
        {
            return NewDao<IDGDao>().GetPackAmount(drugId);
        }

        /// <summary>
        /// 获取库房当前会计年、月
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="errMsg">错误信息</param>
        /// <param name="actYear">会计年份</param>
        /// <param name="actMonth">会计月份</param>
        /// <returns>当前会计年、月</returns>
        public bool GetAccountTime(int deptID, out string errMsg, out int actYear, out int actMonth)
        {
            errMsg = string.Empty;
            DS_BalanceRecord record = NewDao<Dao.IDSDao>().GetMaxBlanceRecord(deptID);
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
    }
}
