using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManger.Dao;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManger.ObjectModel.Bill
{
    /// <summary>
    /// 药品单据转换类
    /// </summary>
    internal class DGBillConverter : AbstractObjectModel
    {
        #region 药房--药库转换
        /// <summary>
        /// 药库出库头转药房入库头
        /// </summary>
        /// <param name="dwhead">药库出库主实体</param>
        /// <param name="userId">操作用户ID</param>
        /// <param name="userName">操作用户名</param>
        /// <param name="bussType">业务类型</param>
        /// <returns>入库头对象</returns>
        public DS_InstoreHead ConvertInFromDWOutHead(DW_OutStoreHead dwhead, int userId, string userName, string bussType)
        {
            DS_InstoreHead dshead = new DS_InstoreHead();
            dshead.DelFlag = 0;
            dshead.BusiType = bussType;
            dshead.BillTime = System.DateTime.Now;
            dshead.DeptID = dwhead.ToDeptID;
            dshead.RegEmpID = userId;
            dshead.RegEmpName = userName;
            dshead.RegTime = DateTime.Now;
            dshead.StockFee = dwhead.StockFee;
            dshead.RetailFee = dwhead.RetailFee;
            dshead.OutStoreHeadID = dwhead.OutStoreHeadID;
            return dshead;
        }

        /// <summary>
        /// 药库出库明细转入库明细
        /// </summary>
        /// <param name="outHeadId">出库头ID</param>
        /// <returns>入库明细对象集</returns>
        public List<DS_InStoreDetail> ConvertInFromDwStoreDetail(int outHeadId)
        {
            List<DS_InStoreDetail> lst = NewObject<IDSDao>().GetInStoreFromOutStore(outHeadId);
            return lst;
        }

        /// <summary>
        /// 药库出库头转药房出库头
        /// </summary>
        /// <param name="dwhead">药库出库主实体</param>
        /// <param name="userId">操作用户ID</param>
        /// <param name="userName">操作用户名</param>
        /// <returns>药房出库头对象集</returns>
        public DS_OutStoreHead ConvertOutFromDwOutHead(DW_OutStoreHead dwhead, int userId, string userName)
        {
            DS_OutStoreHead dshead = new DS_OutStoreHead();
            dshead.DelFlag = 0;
            dshead.BusiType = DGConstant.OP_DS_RETURNSOTRE;
            dshead.BillTime = System.DateTime.Now;
            dshead.DeptID = dwhead.ToDeptID;
            dshead.RegEmpID = userId;
            dshead.RegEmpName = userName;
            dshead.RegTime = DateTime.Now;
            dshead.StockFee = dwhead.StockFee;
            dshead.RetailFee = dwhead.RetailFee;
            dshead.OutStoreHeadID = dwhead.OutStoreHeadID;
            return dshead;
        }

        /// <summary>
        /// 药库出库明细转 药房出库明细
        /// </summary>
        /// <param name="outHeadId">出库头ID</param>
        /// <returns>出库明细对象集</returns>
        public List<DS_OutStoreDetail> ConvertOutFromDwStoreDetail(int outHeadId)
        {
            List<DS_OutStoreDetail> lst = NewObject<IDSDao>().GetOutStoreFromOutStore(outHeadId);
            return lst;
        }
        #endregion

        #region 药库--药库转换
        /// <summary>
        /// 药库入库单明细转药库出库单明细
        /// </summary>
        /// <param name="billNo">单据号</param>
        /// <returns>出库单明细</returns>
        public DataTable ConvertDwOutFromDwIn(string billNo)
        {
            List<Tuple<string, string, SqlOperator>> andWhere = new List<Tuple<string, string, SqlOperator>>();
            andWhere.Add(Tuple.Create("BillNo", billNo,SqlOperator.Equal));
            return NewObject<IDWDao>().GetOutStoreFromInStore(andWhere, null);
        }
        #endregion

        /// <summary>
        /// 领药单转出库单
        /// </summary>
        /// <param name="applyHeadId">头ID</param>
        /// <returns>出库单数据源</returns>
        public DataTable CovertDWOutFromApply(int applyHeadId)
        {
            List<Tuple<string, string, SqlOperator>> andWhere = new List<Tuple<string, string, SqlOperator>>();
            andWhere.Add(Tuple.Create("ApplyHeadID", applyHeadId.ToString(), SqlOperator.Equal));
            return NewObject<IDWDao>().GetOutFromApply(andWhere, null);
        }
    }
}
