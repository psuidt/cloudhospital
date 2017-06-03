using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.DrugManage;
using HIS_Entity.MaterialManage;
using HIS_Entity.SqlAly;
using HIS_MaterialManager.Dao;
using HIS_MaterialManager.ObjectModel.BaseData;
using HIS_MaterialManager.ObjectModel.Store;
using HIS_PublicManage.ObjectModel;

namespace HIS_MaterialManager.ObjectModel.Bill
{
    /// <summary>
    /// 物资入库单处理器
    /// </summary>
   public class MwInstoreBill: AbstractObjectModel,IMwBill
    {
        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="headID">头id</param>
        /// <param name="auditEmpID">审核人id</param>
        /// <param name="auditEmpName">审核人名称</param>
        /// <returns>审核返回结果</returns>
        public MWBillResult AuditBill(int headID, int auditEmpID, string auditEmpName)
        {
            MW_InStoreHead head = (MW_InStoreHead)NewObject<MW_InStoreHead>().getmodel(headID);
            MWBillResult result = new MWBillResult();

            if (!NewObject<MaterialDeptMgr>().IsDeptChecked(head.DeptID))
            {
                result.Result = 1;
                result.ErrMsg = "当前科室处于盘点状态或者没有设置科室的盘点状态 不能处理业务操作";
                return result;
            }

            if (head.AuditFlag == 1)
            {
                result.Result = 1;
                result.ErrMsg = "物资已经被审核,请刷新数据";
                return result;
            }

            if (head != null)
            {
                List<MW_InStoreDetail> lstDetails = NewObject<MW_InStoreDetail>().getlist<MW_InStoreDetail>("InHeadID=" + headID);
                head.AuditEmpID = auditEmpID;
                head.AuditEmpName = auditEmpName;
                head.AuditTime = System.DateTime.Now;
                head.AuditFlag = 1;
                head.save();

                foreach (MW_InStoreDetail detail in lstDetails)
                {
                    StoreParam storeParam = new StoreParam();

                    storeParam.BatchNO = detail.BatchNo;
                    storeParam.DeptID = head.DeptID;
                    storeParam.MaterialId = detail.MaterialID;
                   
                    storeParam.RetailPrice = detail.RetailPrice;
                    storeParam.StockPrice = detail.StockPrice;
                    storeParam.UnitID = detail.UnitID;
                    storeParam.UnitName = detail.UnitName;
                    storeParam.ValidityTime = detail.ValidityDate;
                    storeParam.UnitAmount = 1;

                    storeParam.Amount = detail.Amount;
                    storeParam.PackAmount = 1;
              
                    storeParam.BussConstant = head.BusiType;
                    MWStoreResult storeRtn = NewObject<MwStore>().AddStore(storeParam);

                    if (storeRtn.Result != 0)
                    {
                        result.Result = 1;
                        if (storeRtn.Result == 1)
                        {
                            result.LstNotEnough = new List<MWNotEnoughInfo>();
                            MWNotEnoughInfo notEnough = new MWNotEnoughInfo();
                            notEnough.DeptID = head.DeptID;
                            notEnough.MaterialId = detail.MaterialID;
                            notEnough.LackAmount = storeRtn.StoreAmount + detail.Amount;
                            notEnough.MaterialInfo = "药品批次号:" + detail.BillNo;
                            result.LstNotEnough.Add(notEnough);
                            result.ErrMsg = "【" + notEnough.MaterialId + "】库存不足";
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

            return result;
        }

        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="id">主键ID</param>
        public void DeleteBill(int id)
        {
            MW_InStoreHead inHead = (MW_InStoreHead)NewObject<MW_InStoreHead>().getmodel(id);
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
        /// 加载明细数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>入库明细数据</returns>
        public DataTable LoadDetails(Dictionary<string, string> condition)
        {
            return NewDao<IMWDao>().LoadInStoreDetail(condition);
        }

        /// <summary>
        /// 加载入库头数据
        /// </summary>
        /// <param name="andWhere">查询条件</param>
        /// <returns>入库头数据</returns>
        public DataTable LoadHead(List<Tuple<string, string, SqlOperator>> andWhere = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询药房入库单头表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>药房入库单表头</returns>
        public  DataTable LoadHead(Dictionary<string, string> condition)
        {
            //写SQL查询单据头
            return NewDao<IMWDao>().LoadInStoreHead(condition);
        }

        /// <summary>
        /// 保存入库单
        /// </summary>
        /// <typeparam name="THead">头实体</typeparam>
        /// <typeparam name="TDetail">明细实体列表</typeparam>
        /// <param name="billHead">单据头</param>
        /// <param name="billDetails">单据明细</param>
        public void SaveBill<THead, TDetail>(THead billHead, List<TDetail> billDetails)
        {
            MW_InStoreHead head = billHead as MW_InStoreHead;
            List<MW_InStoreDetail> inDetals = billDetails as List<MW_InStoreDetail>;
            head.RegTime = System.DateTime.Now;
            string serialNO = NewObject<SerialNumberSource>().GetSerialNumber(SnType.物资, head.DeptID, head.BusiType);
            head.BillNo = Convert.ToInt64(serialNO);
            BindDb(head);
            head.save();
            if (head.InHeadID > 0)
            {
                foreach (MW_InStoreDetail detail in inDetals)
                {
                    detail.InHeadID = head.InHeadID;
                    detail.BillNo = head.BillNo;
                    detail.DeptID = head.DeptID;
                    BindDb(detail);
                    detail.save();
                }
            }
        }

        /// <summary>
        /// 写台帐
        /// </summary>
        /// <typeparam name="THead">头实体类</typeparam>
        /// <typeparam name="TDetail">明细实体类</typeparam>
        /// <param name="billHead">单据头</param>
        /// <param name="billDetails">单据明细</param>
        /// <param name="storeResult">库存结果信息</param>
        public void WriteAccount<THead, TDetail>(THead billHead, TDetail billDetails, MWStoreResult storeResult)
        {
            MW_InStoreDetail detail = billDetails as MW_InStoreDetail;
            MW_InStoreHead head = billHead as MW_InStoreHead;
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
            account.BatchNO = detail.BatchNo;
            account.BusiType = head.BusiType;
            account.DeptID = head.DeptID;
            account.DetailID = detail.InDetailID;
            account.MaterialID = detail.MaterialID;
            account.UnitID = detail.UnitID;
            account.UnitName = detail.UnitName;
            account.LendAmount = detail.Amount;
            account.BillNO = detail.BillNo;
            account.RegTime = DateTime.Now;
            if (head.BusiType == MWConstant.OP_MW_BACKSTORE)
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
    }
}
