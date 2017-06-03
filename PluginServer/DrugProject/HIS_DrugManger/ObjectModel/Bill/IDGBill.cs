using System;
using System.Collections.Generic;
using System.Data;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManger.ObjectModel.Bill
{
    /// <summary>
    /// 药品单据处理器
    /// </summary>
    public interface IDGBill
    {
        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="billID">单据头ID</param>
        void DeleteBill(int billID);

        /// <summary>
        /// 保存单据
        /// </summary>
        /// <typeparam name="THead">单据头模板</typeparam>
        /// <typeparam name="TDetail">单据明细模板</typeparam>
        /// <param name="billHead">单据头</param>
        /// <param name="billDetails">单据明细</param>
        void SaveBill<THead, TDetail>(THead billHead, List<TDetail> billDetails);

        /// <summary>
        /// 保存单据
        /// </summary>
        /// <typeparam name="THead">单据头模板</typeparam>
        /// <typeparam name="TDetail">单据明细模板</typeparam>
        /// <param name="billHead">单据头</param>
        /// <param name="billDetails">单据明细</param>
        /// <param name="workId">机构ID</param>
        /// <returns>处理结果</returns>
        DGBillResult SaveBill<THead, TDetail>(THead billHead, List<TDetail> billDetails, int workId);

        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="headID">单据头ID</param>
        /// <param name="auditEmpID">审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <returns>处理结果</returns>
        DGBillResult AuditBill(int headID, int auditEmpID, string auditEmpName);

        /// <summary>
        /// 审核单据
        /// </summary>
        /// <typeparam name="THead">单据头模板</typeparam>
        /// <param name="headID">单据头ID</param>
        /// <param name="auditEmpID">审核人ID</param>
        /// <param name="auditEmpName">审核人姓名</param>
        /// <param name="workId">机构ID</param>
        /// <returns>处理结果</returns>
        DGBillResult AuditBill(int headID, int auditEmpID, string auditEmpName, int workId);

        /// <summary>
        /// 台账写入
        /// </summary>
        /// <typeparam name="THead">单据头模板</typeparam>
        /// <typeparam name="TDetail">单据明细模板</typeparam>
        /// <param name="billHead">单据头</param>
        /// <param name="billDetails">单据明细</param>
        /// <param name="storeResult">库存处理结果</param>
        void WriteAccount<THead, TDetail>(THead billHead, TDetail billDetails, DGStoreResult storeResult);

        /// <summary>
        /// 加载单据头
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>单据头表</returns>
        DataTable LoadHead(Dictionary<string, string> condition);

        /// <summary>
        /// 加载单据明细
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>单据明细表</returns>
        DataTable LoadDetails(Dictionary<string, string> condition);

        /// <summary>
        /// 加载单据头
        /// </summary>
        /// <param name="andWhere">查询条件</param>
        /// <returns>单据头表</returns>
        DataTable LoadHead(List<Tuple<string, string, SqlOperator>> andWhere = null);
    }
}
