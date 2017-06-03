using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManger.ObjectModel.Store;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManger.ObjectModel.Bill
{
    /// <summary>
    /// 药房单据处理器
    /// </summary>
    abstract class DSBill : AbstractObjectModel, IDGBill
    {
        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="headID">单据头ID</param>
        /// <param name="auditEmpID">审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <returns>返回结果</returns>
        public abstract DGBillResult AuditBill(int headID, int auditEmpID, string auditEmpName);

        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="headID">单据头ID</param>
        /// <param name="auditEmpID">审核人ID</param>
        /// <param name="auditEmpName">审核人名称</param>
        /// <param name="workId">机构ID</param>
        /// <returns>返回结果</returns>
        public abstract DGBillResult AuditBill(int headID, int auditEmpID, string auditEmpName, int workId);

        /// <summary>
        /// 删除药房单据
        /// </summary>
        /// <param name="billID">药房单据头ID</param>
        public abstract void DeleteBill(int billID);

        /// <summary>
        /// 加载药房单据明细
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>药房单据明细表</returns>
        public abstract DataTable LoadDetails(Dictionary<string, string> condition);

        /// <summary>
        /// 加载药房单据表头
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>药房单据头表</returns>
        public abstract DataTable LoadHead(Dictionary<string, string> condition);

        /// <summary>
        /// 保存药房单据
        /// </summary>
        /// <typeparam name="THead">药房单据头模板</typeparam>
        /// <typeparam name="TDetail">药房单据明细模板</typeparam>
        /// <param name="billHead">药房单据头</param>
        /// <param name="billDetails">药房单据明细</param>
        public abstract void SaveBill<THead, TDetail>(THead billHead, List<TDetail> billDetails);

        /// <summary>
        /// 写入药房台账
        /// </summary>
        /// <typeparam name="THead">药房单据头模板</typeparam>
        /// <typeparam name="TDetail">药房单据明细模板</typeparam>
        /// <param name="billHead">药房单据头</param>
        /// <param name="billDetails">药房单据明细</param>
        /// <param name="storeResult">库存处理结果</param>
        public abstract void WriteAccount<THead, TDetail>(THead billHead, TDetail billDetails, DGStoreResult storeResult);

        /// <summary>
        /// 读取表头数据
        /// </summary>
        /// <param name="andWhere">查询条件</param>
        /// <returns>表头数据</returns>
        public DataTable LoadHead(List<Tuple<string, string, SqlOperator>> andWhere = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets 药房库存处理器
        /// </summary>
        protected IStore iStore
        {
            get
            {
                return NewObject<StoreFactory>().GetStoreProcess(DGConstant.OP_DS_SYSTEM);
            }
        }

        /// <summary>
        /// 获取库房当前会计年、月
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="errMsg">错误信息</param>
        /// <param name="actYear">会计年份</param>
        /// <param name="actMonth">会计月份</param>
        /// <returns>库房当前会计年、月</returns>
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

        /// <summary>
        /// 保存单据
        /// </summary>
        /// <typeparam name="THead">药房单据头模板</typeparam>
        /// <typeparam name="TDetail">药房单据明细模板</typeparam>
        /// <param name="billHead">药房单据头</param>
        /// <param name="billDetails">药房单据明细</param>
        /// <param name="workId">机构ID</param>
        /// <returns>处理结果</returns>
        public DGBillResult SaveBill<THead, TDetail>(THead billHead, List<TDetail> billDetails, int workId)
        {
            throw new NotImplementedException();
        }
    }
}
