using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;
using HIS_PublicManage.CommonSql;
using HIS_PublicManage.ObjectModel;

namespace HIS_DrugManger.Dao
{
    /// <summary>
    /// 药库系统数据库访问
    /// </summary>
    public class SqlDWDao : AbstractDao, IDWDao
    {
        /// <summary>
        /// 获取对象DataTable数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dic">查询条件</param>
        /// <param name="tableEntity">对象</param>
        /// <returns>DataTable数据</returns>
        public DataTable GeTable<T>(Dictionary<string, string> dic, T tableEntity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取对象DataTable数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dic">查询条件</param>
        /// <returns>DataTable数据</returns>
        public DataTable GeTable<T>(Dictionary<string, Dictionary<Type, string>> dic) where T : AbstractEntity
        {
            StringBuilder stbSql = new StringBuilder();
            stbSql.AppendFormat("select * from {0}", EntityTableManage.GetTableName<T>());
            stbSql.Append("and 1=1 ");
            foreach (var v in dic)
            {
                stbSql.AppendFormat(" and " + v.Key + "=");
            }

            return oleDb.GetDataTable(stbSql.ToString());
        }

        #region 药品入库
        /// <summary>
        /// 查询入库单明细
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>入库单明细</returns>
        public DataTable LoadInStoreDetail(Dictionary<string, string> queryCondition)
        {
            string strSql = @"	SELECT a.*,b.TradeName,c.ChemName,c.Spec,d.ProductName,e.CTypeName,f.Amount as TotalAmount,
                            a.RetailFee - a.StockFee AS DiffFee,
							c.PackAmount,
                            a.Amount as pAmount,
							c.PackUnit PackUnitName,c.packUnitId,
							c.MiniUnit,c.MiniUnitID
                                FROM DW_InStoreDetail a
                                LEFT JOIN DG_HospMakerDic b
                                ON a.DrugID = b.DrugID
                                LEFT JOIN DG_CenterSpecDic c
                                ON b.CenteDrugID = c.CenteDrugID
                                LEFT JOIN DG_ProductDic d
                                ON b.ProductID = d.ProductID
                                LEFT JOIN DG_ChildTypeDic e
                                ON a.CTypeID = e.CTypeID
                                LEFT JOIN DW_Storage f
								 ON a.DrugID=f.DrugID  AND f.DeptID=a.DeptID  
                            WHERE b.IsStop = 0 AND c.IsStop = 0
                            AND a.WorkID={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in queryCondition)
            {
                strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
            }

            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="inHeadID">付款ID</param>
        /// <param name="invoiceNO">发票号</param>
        /// <param name="payTime">付款时间</param>
        /// <param name="payRecordID">付款记录ID</param>
        /// <param name="type">类型</param>
        /// <returns>返回结果</returns>
        public int UpdateStoreHead(string inHeadID, string invoiceNO, DateTime payTime, int payRecordID, int type)
        {
            string strSql = "UPDATE DW_InStoreHead SET PayFlag={0},InvoiceNO='{2}',InvoiceDate='{3}',PayRecordID={4} WHERE InHeadID in ({1})";
            return oleDb.DoCommand(string.Format(strSql, type, inHeadID, invoiceNO, payTime, payRecordID));
        }

        /// <summary>
        /// 取消付款,并删除付款记录
        /// </summary>
        /// <param name="payRecordID">付款记录ID</param>
        /// <returns>返回结果</returns>
        public int UpdatePayRecord(string payRecordID)
        {
            string strSql = "UPDATE DW_InStoreHead SET PayFlag=0  WHERE PayRecordID in ({0})";
            oleDb.DoCommand(string.Format(strSql, payRecordID));
            strSql = "UPDATE DG_PayRecord SET DelFlag=1 WHERE PayRecordID in ({0})";
            return oleDb.DoCommand(string.Format(strSql, payRecordID));
        }

        /// <summary>
        /// 查询入库表头
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>入库单明细</returns>
        public DataTable LoadInStoreHead(Dictionary<string, string> queryCondition)
        {
            string strSql = @"SELECT  *
                    FROM    ( SELECT    0 AS ck ,
                    a.[InHeadID]
                    ,a.[BusiType]
                    ,a.[OpEmpID]
                    ,a.[OpEmpName]
                    ,a.[AuditFlag]
                    ,a.[DelFlag]
                    ,a.[Remark]
                    ,AuditTime=(CASE a.[AuditFlag] WHEN 0 THEN '' ELSE Convert(varchar(20),a.[AuditTime],120) END)
                    ,a.[AuditEmpID]
                    ,a.[AuditEmpName]
                    ,a.[RegTime]
                    ,a.[RegEmpID]
                    ,a.[RegEmpName]
                    ,a.[BillNo]
                    ,a.[StockFee]
                    ,a.[RetailFee]
                    ,a.[InvoiceNo]
                    ,a.[InvoiceDate]
                    ,a.[BillTime]
                    ,a.[SupplierID]
                    ,a.[SupplierName]
                    ,a.[DeliveryNo]
                    ,a.[PayFlag]
                    ,a.[PayRecordID]
                    ,a.[DeptID]
                    ,a.[WorkID],
                    b.BusiTypeName ,
                    bd.Name
          FROM      DW_InstoreHead a
                    LEFT JOIN DG_BusiType b ON a.BusiType = b.BusiCode
                    LEFT JOIN dbo.BaseDept bd ON bd.DeptId = a.DeptID
        ) AS a
     WHERE a.DelFlag = 0 AND a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in queryCondition)
            {
                if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            strWhere.Append("  order by AuditTime,RegTime ");
            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 查询付款记录
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>付款记录</returns>
        public DataTable LoadPayRecord(Dictionary<string, string> queryCondition)
        {
            string strSql = @"SELECT 0 as cks,* FROM DG_PayRecord WHERE DelFlag=0";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in queryCondition)
            {
                if (pair.Key != string.Empty && pair.Key != "LikeSearch")
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 打印付款记录
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>付款记录</returns>
        public DataTable PrintPayRecord(Dictionary<string, string> queryCondition)
        {
            string strSql = @"SELECT CONVERT(varchar(100), a.BillTime, 20) AS BillTime,CAST(a.BillNo AS VARCHAR(100)) AS BillNo,a.StockFee as TotalRetailFee,b.InvoiceNO FROM dbo.DW_InStoreHead a LEFT JOIN DG_PayRecord b ON a.PayRecordID=b.PayRecordID WHERE TotalRetailFee IS NOT NULL AND a.DelFlag=0";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in queryCondition)
            {
                if (pair.Key != string.Empty && pair.Key != "LikeSearch")
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 查询药品入库单药品ShowCard
        /// </summary>
        /// <param name="isRet">是否退库</param>
        /// <param name="deptID">科室ID</param>
        /// <returns>药品ShowCard数据源</returns>
        public DataTable GetDrugDicForInStoreShowCard(bool isRet, int deptID)
        {
            if (!isRet)
            {
                string strSql = @"SELECT b.DrugID,a.ChemName,b.TradeName,a.Spec,b.ProductID,c.ProductName,
                              a.PYCode,a.WBCode,b.PYCode AS TPYCode, b.WBCode AS TWBCode,a.CTypeID,
                              a.PackUnitID,a.PackUnit,a.PackAmount,
                              a.MiniUnit,a.MiniUnitID,
                              b.StockPrice,b.RetailPrice
                              FROM DG_CenterSpecDic a
                              LEFT JOIN DG_HospMakerDic b
                              ON a.CenteDrugID = b.CenteDrugID
                              LEFT JOIN DG_ProductDic c
                              ON b.ProductID = c.ProductID
                              WHERE a.IsStop = 0 AND b.IsStop = 0 ";
                return oleDb.GetDataTable(strSql);
            }
            else
            {
                string strSql = @"SELECT b.DrugID,a.ChemName,b.TradeName,a.Spec,b.ProductID,c.ProductName,
                              a.PYCode,a.WBCode,b.PYCode AS TPYCode, b.WBCode AS TWBCode,a.CTypeID,
                              a.PackUnitID,a.PackUnit,a.PackAmount,
                              a.MiniUnit,a.MiniUnitID,b.StockPrice,b.RetailPrice
                              FROM DG_CenterSpecDic a
                              LEFT JOIN DG_HospMakerDic b
                              ON a.CenteDrugID = b.CenteDrugID
                              LEFT JOIN DG_ProductDic c
                              ON b.ProductID = c.ProductID
                              LEFT JOIN DW_Storage d
                              ON b.DrugID=d.DrugID
                              WHERE a.IsStop = 0 AND b.IsStop = 0 AND d.DeptID={0}";
                return oleDb.GetDataTable(string.Format(strSql, deptID.ToString()));
            }
        }

        /// <summary>
        /// 查询药品入库批次ShowCard
        /// </summary>
        /// <param name="deptID">批次ShowCard数据源</param>
        /// <returns>药品入库批次</returns>
        public DataTable GetBatchForInstoreShowCard(int deptID)
        {
            string strSql = @"SELECT ValidityTime=Convert(varchar(10),ValidityTime,120),BatchNO,DrugID,StockPrice,RetailPrice FROM DW_Batch
                              WHERE DelFlag = 0 AND DeptID = {0} AND BatchAmount>0 ";
            strSql = string.Format(strSql, deptID.ToString());
            return oleDb.GetDataTable(strSql);
        }
        #endregion

        #region 库存上下限设置
        /// <summary>
        /// 查询库存上下限数据
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>库存数据</returns>
        public DataTable GetLoadStoreLimitData(Dictionary<string, string> queryCondition)
        {
            string strSql = @"SELECT    a.StorageID ,
                                        a.DrugID ,
                                        c.ChemName ,
                                        c.Spec ,
                                        d.ProductName ,
                                        a.Amount ,
                                        a.Place ,
                                        a.LocationID ,
                                        a.LStockPrice ,
                                        a.RegTime ,
                                        a.UpperLimit ,
                                        a.LowerLimit ,
                                        a.UnitID ,
                                        a.UnitName ,
                                        a.DelFlag ,
                                        a.DeptID
                                FROM    DW_Storage a
                                        INNER JOIN DG_HospMakerDic b ON a.DrugID = b.DrugID
                                        INNER JOIN DG_CenterSpecDic c ON c.CenteDrugID = b.CenteDrugID
                                        INNER JOIN DG_ProductDic d ON d.ProductID = b.ProductID
                                WHERE   a.DelFlag = 0  AND a.WorkID={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in queryCondition)
            {
                if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 保存库存上下限
        /// </summary>
        /// <param name="details">库存数据列表</param>
        public void SaveStoreLimit(List<DW_Storage> details)
        {
            string strSql = string.Empty;
            foreach (DW_Storage s in details)
            {
                strSql = "UPDATE DW_Storage SET UpperLimit={0},LowerLimit={1} WHERE StorageID={2}";
                strSql = string.Format(strSql, s.UpperLimit, s.LowerLimit, s.StorageID);
                oleDb.DoCommand(strSql);
            }
        }
        #endregion

        #region 库存处理
        /// <summary>
        /// 判断是否存在库存药品
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <param name="storageID">库存ID</param>
        /// <returns>是否存在</returns>
        public bool ExistStorage(int deptID, int drugID, out int storageID)
        {
            string strSql = "select StorageID from DW_Storage where DeptID={0} and DrugID={1}";
            strSql = string.Format(strSql, deptID.ToString(), drugID.ToString());
            object obj = oleDb.GetDataResult(strSql);
            if (obj != null && obj != DBNull.Value)
            {
                storageID = Convert.ToInt32(obj);
                return true;
            }
            else
            {
                storageID = 0;
                return false;
            }
        }

        /// <summary>
        /// 增加药库库存
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <param name="amount">数量</param>
        /// <returns>返回状态:0正常；1库存不足；2数据库更新失败</returns>
        public bool AddStoreAmount(int deptID, int drugID, decimal amount)
        {
            string strSql = @"update DW_Storage 
                            set Amount=Amount+({0}) 
                            where DeptID={1} and DrugID={2}";
            strSql = string.Format(strSql, amount, deptID, drugID);
            int rtn = oleDb.DoCommand(strSql);
            if (rtn > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 增加批次数量
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <param name="batchNO">批次号</param>
        /// <param name="amount">数量</param>
        /// <returns>返回状态</returns>
        public bool AddBatchAmount(int deptID, int drugID, string batchNO, decimal amount)
        {
            //更新库存
            string strSql = @"update DW_Batch set 
                           DelFlag=(case when BatchAmount+({0})=0 then 1 else 0 end),
                           BatchAmount=BatchAmount+({0})
                           where DeptID={1} and DrugID={2} 
                           and BatchNO='{3}'";
            int rtn = oleDb.DoCommand(string.Format(strSql, amount, deptID, drugID, batchNO));
            return rtn > 0 ? true : false;
        }

        /// <summary>
        /// 获取批次库存量
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <param name="batchNO">批次号</param>
        /// <returns>批次库存量</returns>
        public DW_Batch GetBatchAmount(int deptID, int drugID, string batchNO)
        {
            string strSql = @"select * from DW_Batch 
                              where DeptID={0} and DrugID={1}
                              and BatchNO='{2}'";
            strSql = string.Format(strSql, deptID, drugID, batchNO);
            return oleDb.Query<DW_Batch>(strSql, string.Empty).FirstOrDefault();
        }

        /// <summary>
        /// 获取库存信息
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <returns>库存信息</returns>
        public DW_Storage GetStorageInfo(int deptID, int drugID)
        {
            string strSql = @"select * from DW_Storage 
                              where DeptID={0} and DrugID={1}";
            strSql = string.Format(strSql, deptID, drugID);
            return oleDb.Query<DW_Storage>(strSql, string.Empty).FirstOrDefault();
        }

        /// <summary>
        /// 获取当前库存数量
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <returns>药品库存数量</returns>
        public decimal? GetStoreAmount(int deptID, int drugID)
        {
            string strSql = "select Amount from DW_Storage where DeptID={0} and DrugID={1}";
            strSql = string.Format(strSql, deptID.ToString(), drugID.ToString());
            object obj = oleDb.GetDataResult(strSql);
            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToDecimal(obj);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 库存查询
        /// <summary>
        /// 查询药品库存信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>库存信息表</returns>
        public DataTable LoadDrugStorage(Dictionary<string, string> condition)
        {
            string strSql = @"SELECT  0 ck,a.StorageID ,
                                        a.DrugID ,
		                                b.TradeName,
                                        c.ChemName ,
                                        c.Spec ,
                                        d.ProductName ,
                                        0 AS BaseAmount,
		                                a.Amount AS PackAmount,
                                        f.ValidAmount,
		                                dbo.fnGetPackAmount(f.ValidAmount,1,a.UnitName,c.PackUnit) AS ValidPackAmount,
                                        a.Place ,
                                        a.RegTime ,
                                        a.UpperLimit ,
                                        a.LowerLimit ,
                                        c.PackUnit AS UnitName ,
                                        c.MiniUnit,
                                        a.DelFlag ,
                                        a.DeptID ,
                                        dbo.fnGetPackAmount(a.Amount,1,a.UnitName,c.PackUnit) as NewAmount,
                                        ISNULL(e.LocationName,'') as LocationID
                                FROM    DW_Storage a
                                        INNER JOIN DG_HospMakerDic b ON a.DrugID = b.DrugID
                                        INNER JOIN DG_CenterSpecDic c ON c.CenteDrugID = b.CenteDrugID
                                        INNER JOIN DG_ProductDic d ON d.ProductID = b.ProductID
                                        Left JOIN DG_Location e ON a.LocationID=e.LocationID   
										LEFT JOIN DS_ValidStorage f ON a.DrugID=f.DrugID 
                                 WHERE  a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in condition)
            {
                if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 查询药品库存信息（带批次）
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>药品库存信息（带批次）</returns>
        public DataTable LoadDrugStorages(Dictionary<string, string> condition)
        {
            string strSql = @"SELECT  0 ck,a.StorageID ,
                                        a.DrugID ,
		                                b.TradeName,
                                        c.ChemName ,
                                        c.Spec ,
                                        d.ProductName ,
                                        0 AS BaseAmount,
		                                a.Amount AS PackAmount,
                                        a.Place ,
                                        a.RegTime ,
                                        a.UpperLimit ,
                                        a.LowerLimit ,
                                        c.PackUnit AS UnitName ,
                                        f.BatchNO,
                                        f.ValidityTime,
                                        c.MiniUnit,
                                        a.DelFlag ,
                                        a.DeptID ,
                                        ISNULL(e.LocationName,'') as LocationID
                                FROM    DW_Storage a
                                        INNER JOIN DG_HospMakerDic b ON a.DrugID = b.DrugID
                                        INNER JOIN DG_CenterSpecDic c ON c.CenteDrugID = b.CenteDrugID
                                        INNER JOIN DG_ProductDic d ON d.ProductID = b.ProductID
                                        Left JOIN DG_Location e ON a.LocationID=e.LocationID
                                        Left JOIN DW_Batch f ON a.StorageID=f.StorageID
                                WHERE  a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in condition)
            {
                if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 查询药品批次信息
        /// </summary>
        /// <param name="storageID">库存ID</param>
        /// <returns>药品批次信息列表</returns>
        public DataTable LoadDrugBatch(int storageID)
        {
            string strSql = @"SELECT  a.BatchID ,
                                        a.StorageID ,
                                        a.DeptID ,
                                        a.DrugID ,
                                        c.ChemName ,
                                        a.BatchNO ,
                                        a.StockPrice ,
                                        a.RetailPrice ,
                                        a.InstoreTime ,
                                        dbo.fnGetPackAmount(a.BatchAmount,1,a.UnitName,c.PackUnit) as NewBatchAmount,
                                        a.BatchAmount ,
                                        a.UnitID ,
                                        a.UnitName ,
                                        a.ValidityTime ,
                                        a.DelFlag ,
                                        ISNULL(a.StockPrice, 0) * ISNULL(a.BatchAmount, 0) AS StockFee ,
                                        ISNULL(a.RetailPrice, 0) * ISNULL(a.BatchAmount, 0) AS RetailFee ,
                                        ( ISNULL(a.RetailPrice, 0) * ISNULL(a.BatchAmount, 0)
                                          - ISNULL(a.StockPrice, 0) * ISNULL(a.BatchAmount, 0) ) AS FeeDifference
                                FROM    DW_Batch a
                                        INNER JOIN DG_HospMakerDic b ON a.DrugID = b.DrugID
                                        INNER JOIN DG_CenterSpecDic c ON c.CenteDrugID = b.CenteDrugID
                                        INNER JOIN DG_ProductDic d ON d.ProductID = b.ProductID
                                WHERE   a.DelFlag = 0
                                        AND a.StorageID = {0}
                                        AND a.WorkID = {1}";
            strSql = string.Format(strSql, storageID, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取药库批次信息
        /// </summary>
        /// <param name="batchNO">批次号</param>
        /// <param name="drugID">药品ID</param>
        /// <returns>药库批次信息</returns>
        public List<DW_Batch> GetBatchList(string batchNO, int drugID)
        {
            string strSql = "SELECT * FROM DW_Batch WHERE BatchNO='{0}' AND DrugID={1}";
            return oleDb.Query<DW_Batch>(string.Format(strSql, batchNO, drugID), string.Empty).ToList();
        }

        /// <summary>
        /// 获取批次药品类型
        /// </summary>
        /// <param name="batchNO">批次号</param>
        /// <param name="drugID">药品ID</param>
        /// <returns>批次药品类型</returns>
        public int GetTypeId(string batchNO, int drugID)
        {
            string strSql = @"SELECT c.CTypeID FROM DW_Batch a 
                                             LEFT JOIN DG_HospMakerDic h ON h.DrugID = a.DrugID
                                             LEFT JOIN dbo.DG_CenterSpecDic c ON h.CenteDrugID = c.CenteDrugID WHERE a.BatchNO='{0}' AND a.DrugID={1}";
            DataTable dt = oleDb.GetDataTable(string.Format(strSql, batchNO, drugID));
            if (dt.Rows.Count > 0)
            {
                string cTypeId = dt.Rows[0][0].ToString();
                return Convert.ToInt32(cTypeId);
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region 入库报表
        /// <summary>
        /// 入库报表
        /// </summary>
        /// <param name="andWhere">过滤条件</param>
        /// <returns>入库报表数据集</returns>
        public DataTable GetInstoreReport(List<Tuple<string, string, SqlOperator>> andWhere = null)
        {
            StringBuilder stb = new StringBuilder();
            stb.AppendFormat(@"  SELECT    *
  FROM      ( SELECT   
                         d.InHeadID,
						 m.TradeName CHEMNAME ,
                        m.DrugID MAKERDICID ,
                        d.RetailFee TotalFee ,
                        d.StockFee InstoreFee ,
                        d.RetailFee - d.StockFee Difffee ,
                        d.RegTime ExeTime ,
						d.DeliveryNo,
						d.BillNo,
                        p.ProductName ,
                        c.CTypeName ,
                        u.UnitName ,
                        s.Amount INNUM,
                        s.StockPrice ,--进价
                        s.RetailPrice RETAILPRICE ,--卖价
                        s.StockFee ,
                        s.RetailFee ,
                        s.RetailFee - s.StockFee DetailDIFFFEE ,
                        s.VALIDITYDATE ,
						cs.Spec,
						ds.SupportName,
						bd.Name DeptName,
                        s.BatchNO BATCHNUM
              FROM      DW_InStoreHead d
                        INNER JOIN [dbo].[DW_InStoreDetail] s ON s.InHeadID = d.InHeadID
                        LEFT JOIN DG_HospMakerDic m ON m.DrugID = s.DrugID
                        INNER JOIN dbo.DG_ProductDic p ON p.ProductID = m.ProductID
                        LEFT JOIN dbo.DG_ChildTypeDic c ON c.CTypeID = s.CTypeID
                        LEFT JOIN dbo.DG_UnitDic u ON u.UnitID = s.UnitID
						LEFT JOIN DG_CenterSpecDic cs ON cs.CenteDrugID = m.CenteDrugID
						LEFT JOIN dbo.DG_SupportDic ds ON ds.SupplierID=d.SupplierID
						LEFT JOIN BaseDept bd ON bd.DeptId=d.DeptID
            ) t
  WHERE     1 = 1");
            stb.Append(SqlServerProcess.GetCondition(andWhere, null));
            return oleDb.GetDataTable(stb.ToString());
        }
        #endregion

        #region 库存盘点
        /// <summary>
        /// 查询盘点单表头
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>盘点单表头</returns>
        public DataTable LoadCheckHead(Dictionary<string, string> queryCondition)
        {
            string strSql = @"SELECT
                               CheckHeadID,
                               BillNO,
                               RegEmpID,
                               RegEmpName,
                               RegTime,
                               AuditEmpID,
                               AuditEmpName,
                               AuditTime,
                               Remark,
                               DelFlag,
                               AuditFlag,
                               BusiType,
                               DeptID,
                               AuditHeadID,
                               AuditNO,
                               WorkID
                               FROM DW_CheckHead
                               WHERE DelFlag=0 AND WorkID={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in queryCondition)
            {
                if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            strSql = strSql + strWhere.ToString() + " order by BillNO";
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 查询盘点单明细
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>盘点单明细</returns>
        public DataTable LoadCheckDetail(Dictionary<string, string> queryCondition)
        {
            string strSql = @"SELECT 
                               a.CheckDetailID,
                               a.DrugStoreID,
                               a.Place as Place1,
                               h.LocationName as Place,
                               a.DeptID,
                               a.CTypeID,
                               a.DrugID,
                               a.BillNO,
                               a.FactAmount,
                               a.FactStockFee,
                               a.FactRetailFee,
                               a.ActAmount,
                               a.ActStockFee,
                               a.ActRetailFee,
                               a.UnitID,
                               a.UnitName,
                               c.PackUnit,
                               a.AuditFlag,
                               a.BillTime,
                               a.RetailPrice,
                               a.StockPrice,
                               a.CheckHeadID,
                               a.BatchNO,
                               a.ValidityDate,
                               a.WorkID,
                               c.ChemName ,
                               c.Spec ,
                               d.ProductName,
							   f.DosageName,
							   g.TypeName,
                               '' as ActAmountShow,
                               '' as FactAmountShow
                            FROM DW_CheckDetail a
                                INNER JOIN DG_HospMakerDic b ON a.DrugID = b.DrugID
                                INNER JOIN DG_CenterSpecDic c ON c.CenteDrugID = b.CenteDrugID
                                LEFT JOIN DG_DosageDic f ON c.DosageID=f.DosageID
										LEFT JOIN DG_TypeDic  g ON c.TypeID=g.TypeID
                                INNER JOIN DG_ProductDic d ON d.ProductID = b.ProductID 
                                LEFT JOIN DW_Storage as e ON a.DrugID=e.DrugID 
                                LEFT JOIN DG_Location as h ON e.LocationID=h.LocationID
                            WHERE a.WorkID={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in queryCondition)
            {
                if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            var strOrder = " Order By h.LocationName ASC";
            return oleDb.GetDataTable(strSql + strWhere.ToString()+ strOrder.ToString());
        }

        /// <summary>
        /// 取得药库盘点药品选择卡片数据
        /// </summary>
        /// <param name="deptID">药库ID</param>
        /// <returns>药品字典</returns>
        public DataTable GetDrugDicForCheckShowCard(int deptID)
        {
            string strSql = @"SELECT    b.DrugID ,
                                        a.ChemName ,
                                        b.TradeName ,
                                        a.Spec ,
                                        b.ProductID ,
                                        c.ProductName ,
                                        a.PYCode ,
                                        a.WBCode ,
                                        b.PYCode AS TPYCode ,
                                        b.WBCode AS TWBCode ,
                                        a.CTypeID ,
		                                a.TypeID,
                                        a.DosageID,		
                                        a.PackUnitID ,
                                        a.PackUnit ,
		                                a.MiniUnitID,
		                                a.MiniUnit,
                                        a.PackAmount ,
                                        e.StockPrice ,
                                        e.RetailPrice ,
                                        d.DeptID,
		                                d.StorageID,
		                                d.Place as Place1,
                                        h.LocationName as Place,
		                                d.Amount,
		                                e.BatchNO,
		                                e.ValidityTime,
                                        e.BatchAmount,
										f.DosageName,
										g.TypeName
                                FROM    DG_CenterSpecDic a
                                        LEFT JOIN DG_HospMakerDic b ON a.CenteDrugID = b.CenteDrugID
                                        LEFT JOIN DG_ProductDic c ON b.ProductID = c.ProductID
                                        LEFT JOIN DG_DosageDic f ON a.DosageID=f.DosageID
										LEFT JOIN DG_TypeDic  g ON a.TypeID=g.TypeID
                                        RIGHT JOIN DW_Storage d ON b.DrugID = d.DrugID
		                                LEFT JOIN DW_Batch e ON d.StorageID=e.StorageID 
                                        LEFT JOIN DG_Location h ON d.LocationID=h.LocationID 
                                WHERE   a.IsStop = 0
                                        AND b.IsStop = 0
		                                AND e.DelFlag =0
                                        AND d.DeptID = {0} and d.WorkID={1} 
                                        ORDER By h.LocationName asc ";
            return oleDb.GetDataTable(string.Format(strSql, deptID.ToString(), oleDb.WorkId));
        }

        /// <summary>
        /// 提取库存药品
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>盘点单空表数据</returns>
        public DataTable LoadStorageData(Dictionary<string, string> queryCondition)
        {
            string strSql = @"SELECT    b.DrugID ,
                                        a.ChemName ,
                                        b.TradeName ,
                                        a.Spec ,
                                        b.ProductID ,
                                        c.ProductName ,
                                        a.PYCode ,
                                        a.WBCode ,
                                        b.PYCode AS TPYCode ,
                                        b.WBCode AS TWBCode ,
                                        a.CTypeID ,
		                                a.TypeID,
                                        a.DosageID,		
                                        a.PackUnitID ,
                                        a.PackUnit ,
		                                a.MiniUnitID,
		                                a.MiniUnit,
                                        a.PackAmount ,
                                        e.StockPrice ,
                                        e.RetailPrice ,
                                        d.DeptID,
		                                d.StorageID,
		                                d.Place as Place1,
                                        h.LocationName as Place,
		                                d.Amount,
		                                e.BatchNO,
		                                e.ValidityTime,
                                        e.BatchAmount,
										f.DosageName,
										g.TypeName
                                FROM    DG_CenterSpecDic a
                                        LEFT JOIN DG_HospMakerDic b ON a.CenteDrugID = b.CenteDrugID
                                        LEFT JOIN DG_DosageDic f ON a.DosageID=f.DosageID
										LEFT JOIN DG_TypeDic  g ON a.TypeID=g.TypeID
                                        LEFT JOIN DG_ProductDic c ON b.ProductID = c.ProductID
                                        RIGHT JOIN DW_Storage d ON b.DrugID = d.DrugID
		                                LEFT JOIN DW_Batch e ON d.StorageID=e.StorageID  
                                        LEFT JOIN DG_Location h ON d.LocationID=h.LocationID  
                                WHERE   a.IsStop = 0
                                        AND b.IsStop = 0
		                                AND e.DelFlag =0 and d.WorkID={0} ";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in queryCondition)
            {
                if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            strWhere.AppendFormat(" order by h.LocationName asc");
            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 加载盘点审核单表头
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>盘点审核单表头</returns>
        public DataTable LoadAudtiCheckHead(Dictionary<string, string> condition)
        {
            string strSql = @"SELECT 
                               AuditHeadID,
                               BillNO,
                               EmpID,
                               EmpName,
                               AuditTime,
                               Remark,
                               DelFlag,
                               AuditFlag,
                               BusiType,
                               DeptID,
                               WorkID,
                               ProfitRetailFee,
                               ProfitStockFee,
                               LossRetailFee,
                               LossStockFee,
                               CheckStockFee,
                               ActStockFee,
                               CheckRetailFee,
                               ActRetailFee
                            FROM DW_AuditHead
                            WHERE DelFlag=0 AND AuditFlag=1 AND WorkID={0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in condition)
            {
                if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            strSql = strSql + strWhere.ToString() + " order by BillNO";
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 加载盘点审核单明细
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>盘点审核单明细</returns>
        public DataTable LoadAuditCheckDetail(Dictionary<string, string> condition)
        {
            string strSql = @"SELECT   a.AuditDetailID ,
                                        a.DrugStoreID ,
                                        a.CTypeID ,
                                        a.DrugID ,
                                        a.Place ,
                                        a.BatchNO ,
                                        a.ValidityDate ,
                                        a.DeptID ,
                                        a.BillNO ,
                                        a.FactAmount ,
                                        a.FactStockFee ,
                                        a.FactRetailFee ,
                                        a.ActAmount ,
                                        a.ActStockFee ,
                                        a.ActRetailFee ,
                                        a.UnitID ,
                                        a.UnitName ,
                                        a.RetailPrice ,
                                        a.StockPrice ,
                                        a.AuditHeadID,
		                                c.ChemName,
		                                c.Spec,
                                        c.TypeID,
		                                d.ProductName,
                                        (a.FactRetailFee - a.ActRetailFee)  as DIFFFEE,
                                        (a.FactStockFee - a.ActStockFee) as DIFFTRADEFEE,
                                         c.PackAmount*a.ActAmount as BASENUM,
                                         c.MiniUnit,
                                         c.PackAmount*a.FactAmount as CBASENUM,
                                         m.TypeName,
                                         (a.FactAmount - a.ActAmount) as DiffNum
                                FROM    DW_AuditDetail a
                                        LEFT JOIN DG_HospMakerDic b ON a.DrugID = b.DrugID
                                        LEFT JOIN DG_CenterSpecDic c ON c.CenteDrugID = b.CenteDrugID
                                        LEFT JOIN DG_TypeDic m ON c.TypeID=m.TypeID 
                                        LEFT JOIN DG_ProductDic d ON b.ProductID = d.ProductID where 1=1 ";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            if (condition != null)
            {
                foreach (var pair in condition)
                {
                    if (pair.Key != string.Empty)
                    {
                        strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                    }
                    else
                    {
                        strWhere.AppendFormat("AND {0}", pair.Value);
                    }
                }
            }

            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 汇总所有未审核单据明细并提取
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="haveBatchNO">是否按批次汇总</param>
        /// <returns>汇总盘点信息</returns>
        public DataTable LoadAllNotAuditDetail(Dictionary<string, string> condition, bool haveBatchNO)
        {
            string strSql = string.Empty;
            if (haveBatchNO)
            {
                //按批次号汇总
                strSql = @"SELECT                                
                            a.DrugStoreID,
                            a.Place,
                            a.DeptID,
                            a.CTypeID,
                            a.DrugID,
                            SUM(a.FactAmount) AS FactAmount,
                            SUM(a.FactStockFee) AS FactStockFee,
                            SUM(a.FactRetailFee) AS FactRetailFee,
                            a.ActAmount,
                            a.ActStockFee,
                            a.ActRetailFee,
                            a.UnitID,
                            a.UnitName,
                            a.RetailPrice,
                            a.StockPrice,
                            a.BatchNO,
                            a.ValidityDate,
                            c.ChemName ,
                            c.Spec ,
                            d.ProductName,
                            f.DosageName,
                            g.TypeName
                            FROM dbo.DW_CheckHead h LEFT JOIN DW_CheckDetail a ON h.CheckHeadID=a.CheckHeadID
                            INNER JOIN DG_HospMakerDic b ON a.DrugID = b.DrugID
                            INNER JOIN DG_CenterSpecDic c ON c.CenteDrugID = b.CenteDrugID
                            LEFT JOIN DG_DosageDic f ON c.DosageID=f.DosageID
		                            LEFT JOIN DG_TypeDic  g ON c.TypeID=g.TypeID
                            INNER JOIN DG_ProductDic d ON d.ProductID = b.ProductID
                            WHERE a.WorkID={0} AND h.AuditFlag=0 AND h.DelFlag=0 ";
                strSql = string.Format(strSql, oleDb.WorkId);
                StringBuilder strWhere = new StringBuilder();
                foreach (var pair in condition)
                {
                    if (pair.Key != string.Empty)
                    {
                        strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                    }
                    else
                    {
                        strWhere.AppendFormat("AND {0}", pair.Value);
                    }
                }

                string strGroupBy = @" GROUP BY 
                                                a.DrugStoreID, a.Place,a.DeptID,a.CTypeID,a.DrugID,
                                                a.UnitID,a.UnitName,a.RetailPrice,a.StockPrice,
                                                a.ValidityDate,c.ChemName ,c.Spec ,d.ProductName,
                                                f.DosageName, g.TypeName,a.BatchNO, a.ActAmount,
                                                a.ActStockFee,a.ActRetailFee";
                strSql = strSql + strWhere.ToString() + strGroupBy;
            }
            else
            {
            }

            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取库房盘点状态
        /// </summary>
        /// <param name="deptId">库房Id</param>
        /// <returns>盘点状态</returns>
        public int GetStoreRoomStatus(int deptId)
        {
            int status = 0;
            DataTable dt = NewObject<DG_DeptDic>().gettable("DeptID=" + deptId + " and DeptType=1 and WorkID=" + oleDb.WorkId);
            if (dt != null && dt.Rows.Count > 0)
            {
                status = Convert.ToInt32(dt.Rows[0]["CheckStatus"]);
            }

            return status;
        }

        /// <summary>
        /// 取得所有未审核的盘点汇总明细
        /// </summary>
        /// <param name="deptId">库房Id</param>
        /// <returns>未审核盘点汇总明细</returns>
        public DataTable GetAllNotAuditDetail(int deptId)
        {
            string strSql = string.Empty;

            strSql = @"SELECT                                
                            a.DrugStoreID,
                            a.Place,
                            a.DeptID,
                            a.CTypeID,
                            a.DrugID,
                            SUM(a.FactAmount) AS FactAmount,
                            SUM(a.FactStockFee) AS FactStockFee,
                            SUM(a.FactRetailFee) AS FactRetailFee,
                            a.ActAmount,
                            a.ActStockFee,
                            a.ActRetailFee,
                            a.UnitID,
                            a.UnitName,
                            a.RetailPrice,
                            a.StockPrice,
                            a.BatchNO,
                            a.ValidityDate,
                            c.ChemName ,
                            c.Spec ,
                            d.ProductName,
                            f.DosageName,
                            g.TypeName
                            FROM dbo.DW_CheckHead h LEFT JOIN DW_CheckDetail a ON h.CheckHeadID=a.CheckHeadID
                            INNER JOIN DG_HospMakerDic b ON a.DrugID = b.DrugID
                            INNER JOIN DG_CenterSpecDic c ON c.CenteDrugID = b.CenteDrugID
                            LEFT JOIN DG_DosageDic f ON c.DosageID=f.DosageID
		                            LEFT JOIN DG_TypeDic  g ON c.TypeID=g.TypeID
                            INNER JOIN DG_ProductDic d ON d.ProductID = b.ProductID
                            WHERE a.WorkID={0} AND h.AuditFlag=0 AND h.DelFlag=0 and a.DeptID={1}";
            strSql = string.Format(strSql, oleDb.WorkId, deptId);

            string strGroupBy = @" GROUP BY 
                                                a.DrugStoreID, a.Place,a.DeptID,a.CTypeID,a.DrugID,
                                                a.UnitID,a.UnitName,a.RetailPrice,a.StockPrice,
                                                a.ValidityDate,c.ChemName ,c.Spec ,d.ProductName,
                                                f.DosageName, g.TypeName,a.BatchNO, a.ActAmount,
                                                a.ActStockFee,a.ActRetailFee";
            strSql = strSql + strGroupBy;

            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 更新盘点头表审核状态信息
        /// </summary>
        /// <param name="head">盘点头表</param>
        /// <returns>小于0失败</returns>
        public int UpdateCheckHeadStatus(DW_CheckHead head)
        {
            string strSql =
                @"UPDATE DW_CheckHead set AuditEmpID={0},AuditEmpName='{1}',AuditTime=GETDATE(),AuditFlag=1,AuditHeadID={2},AuditNO={3} WHERE DelFlag=0 AND AuditFlag=0 AND DeptID={4} AND WorkID={5}";
            strSql = string.Format(strSql, head.AuditEmpID, head.AuditEmpName, head.AuditHeadID, head.AuditNO, head.DeptID, oleDb.WorkId);
            return oleDb.DoCommand(strSql);
        }

        /// <summary>
        /// 删除所有未审核盘点单
        /// </summary>
        /// <param name="deptID">库房Id</param>
        /// <returns>返回结果</returns>
        public int DeleteAllNotAuditCheckHead(int deptID)
        {
            string strSql = @"UPDATE DW_CheckHead SET DelFlag=1 WHERE AuditFlag=0 AND DeptID={0} AND WorkID={1}";
            strSql = string.Format(strSql, deptID, oleDb.WorkId);
            int iRtn = oleDb.DoCommand(strSql);
            return iRtn;
        }
        #endregion

        #region 台账处理
        /// <summary>
        /// 获取最后一次结账记录
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>最后一次结账记录</returns>
        public DW_BalanceRecord GetMaxBlanceRecord(int deptID)
        {
            string strSql = @"SELECT TOP 1 * FROM DW_BalanceRecord WHERE DeptID = {0} ORDER BY BalanceID DESC";
            return oleDb.Query<DW_BalanceRecord>(string.Format(strSql, deptID), string.Empty).FirstOrDefault();
        }
        #endregion

        #region 药品出库
        /// <summary>
        /// 查询科室库存药品信息
        /// </summary>
        /// <param name="dept">库房Id</param>
        /// <returns>科室库存药品信息</returns>
        public DataTable GetStoreDrugInFo(int dept)
        {
            StringBuilder stb = new StringBuilder();
            stb.AppendFormat(
                @"SELECT  s.DrugID,
        s.Amount totalNum,
        c.ChemName ,
		c.PYCode,
		c.WBCode,
		pd.PYCode SPYCode,
		pd.WBCode SWBCode,
        c.Spec ,
        pd.ProductName ,
        db.StockPrice ,
        db.RetailPrice ,
        c.PackUnitID ,
        c.PackUnit,
        c.MiniUnitID,
        c.CTypeID,
        c.MiniUnit ,
        c.DosageID ,
        ud.UnitName,
		db.BatchNO,
        db.BatchAmount,
        db.ValidityTime
FROM    dbo.DW_Storage s
        INNER JOIN DG_HospMakerDic h ON h.DrugID = s.DrugID
        INNER JOIN dbo.DG_CenterSpecDic c ON h.CenteDrugID = c.CenteDrugID
        LEFT JOIN dbo.DG_ProductDic pd ON pd.ProductID = h.ProductID
        LEFT JOIN DG_UnitDic ud ON ud.UnitID = c.PackUnitID
		LEFT JOIN dbo.DW_Batch db ON db.DrugID=h.DrugID AND db.DeptID=s.DeptID
	WHERE 1 = 1 AND s.DelFlag = 0 and h.IsStop=0 AND c.IsStop=0 AND db.DelFlag=0 AND s.DeptID={0} ",
                dept);
            return oleDb.GetDataTable(stb.ToString());
        }

        /// <summary>
        /// 获取科室出库数据
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <returns>科室出库数据</returns>
        public DataTable GetDeptOutDrug(int deptId)
        {
            StringBuilder stb = new StringBuilder();
            stb.AppendFormat(
@"SELECT  s.Amount ,
        c.ChemName ,
        c.Spec ,
        pd.ProductName ,
        h.StockPrice ,
        h.RetailPrice ,
        c.PackUnitID ,
        c.MiniUnit ,
        c.DosageID ,
        ud.UnitName
FROM    dbo.DW_Storage s
        INNER JOIN DG_HospMakerDic h ON h.DrugID = s.DrugID
        INNER JOIN dbo.DG_CenterSpecDic c ON h.CenteDrugID = c.CenteDrugID
        LEFT JOIN dbo.DG_ProductDic pd ON pd.ProductID = h.ProductID
        LEFT JOIN DG_UnitDic ud ON ud.UnitID = c.PackUnitID
	    WHERE 1=1 AND s.DelFlag=0 AND DeptID={0} ",
                deptId);
            return oleDb.GetDataTable(stb.ToString());
        }

        /// <summary>
        /// 出库明细单数据
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>出库明细</returns>
        public DataTable LoadOutStoreDetail(Dictionary<string, string> queryCondition)
        {
            StringBuilder stb = new StringBuilder();
            stb.AppendFormat(
                @"SELECT * FROM (
SELECT  sd.DrugID ,
        cs.ChemName ,
	    cs.PackAmount,
        sd.Amount pAmount,
        cs.PackUnit PackUnitName,
        cs.packUnitId,
        cs.MiniUnit,
        cs.MiniUnitID,
        cs.Spec ,
        sd.BatchNO ,
        sd.UnitID,
        dp.ProductName ,
        sd.Amount ,
        du.UnitName ,
        ds.Amount AS totalNum ,
        sd.RetailFee ,
        sd.RetailPrice ,
        sd.StockFee ,
        sd.StockPrice,
	    sd.RetailFee-sd.StockFee AS DifMoney,
		sd.OutHeadID,
        sd.OutDetailID,
        sd.ValidityDate,
		sd.CTypeID,
		cd.CTypeName,
		sd.WorkID,
        dh.DeptID,
        dh.ToDeptID,
		td.TypeID,
        ad.Amount AS FactAmount,
		td.TypeName
FROM    DW_OutStoreDetail sd
        left join DW_OutStoreHead dh on dh.OutStoreHeadID=sd.OutHeadID
        LEFT JOIN DW_Storage ds ON ds.DrugID = sd.DrugID AND dh.DeptID=ds.DeptID 
        INNER JOIN dbo.DG_HospMakerDic h ON h.DrugID = sd.DrugID
        INNER JOIN dbo.DG_CenterSpecDic cs ON cs.CenteDrugID = h.CenteDrugID
        INNER JOIN dbo.DG_ProductDic dp ON dp.ProductID = h.ProductID
        INNER JOIN DG_UnitDic du ON du.UnitID = sd.UnitID
		LEFT JOIN dbo.DG_ChildTypeDic cd ON cd.CTypeID=sd.CTypeID 
        left join DG_TypeDic td on td.TypeID=cd.TypeID 
	  LEFT JOIN dbo.DS_ApplyHead ah ON ah.ApplyHeadID=dh.applyheadId
		LEFT JOIN dbo.DS_ApplyDetail ad ON ad.ApplyHeadID=ah.ApplyHeadID
		AND ad.DrugID=sd.DrugID AND ad.BatchNO=sd.BatchNO 
		WHERE h.IsStop=0 AND cs.IsStop=0 and SD.WorkID={0}
		) AS t where 1=1 ",
                oleDb.WorkId);

            foreach (var pair in queryCondition)
            {
                stb.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
            }

            return oleDb.GetDataTable(stb.ToString());
        }

        /// <summary>
        /// 出库主表信息
        /// </summary>
        /// <param name="andWhere">查询条件</param>
        /// <returns>出库主表信息数据集</returns>
        public DataTable LoadOutStoreHead(List<Tuple<string, string, SqlOperator>> andWhere = null)
        {
            StringBuilder stb = new StringBuilder();
            stb.AppendFormat(
                @" SELECT ds.*,db.BusiTypeName,ds.RetailFee-ds.StockFee AS DiffMoney   FROM DW_OutStoreHead ds
		LEFT JOIN DG_BusiType db 
		ON ds.BusiType=db.BusiCode ");
            stb.Append(SqlServerProcess.GetCondition(andWhere, null));
            stb.Append(" order by AuditTime,RegTime  ");
            return oleDb.GetDataTable(stb.ToString());
        }

        /// <summary>
        /// 加载主表
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>数据集</returns>
        public DataTable LoadOutStoreHead(Dictionary<string, string> queryCondition)
        {
            StringBuilder stb = new StringBuilder();
            stb.AppendFormat(@" SELECT  *
        FROM    ( SELECT    ds.* ,
                            db.BusiTypeName ,
                            ds.RetailFee-ds.StockFee AS DiffMoney,
                            d.Name,
                            bd.Name CurrentDept
                  FROM      DW_OutStoreHead ds
                            LEFT JOIN DG_BusiType db ON ds.BusiType = db.BusiCode
                            LEFT JOIN BaseDept d ON ds.ToDeptID = d.DeptId
                            LEFT JOIN BaseDept BD ON ds.DeptId=bd.DeptID
                  WHERE     DS.DelFlag = 0
                ) AS t
         WHERE  1 = 1 ");

            foreach (var pair in queryCondition)
            {
                if (pair.Key != string.Empty)
                {
                    stb.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    stb.AppendFormat("AND {0}", pair.Value);
                }
            }

            return oleDb.GetDataTable(stb.ToString());
        }
        #endregion

        #region 出入库转换
        /// <summary>
        /// 药库入库明细转出库明细
        /// </summary>
        /// <param name="andWhere">and 条件</param>
        /// <param name="orWhere">or 条件</param>
        /// <returns>出库明细</returns>
        public DataTable GetOutStoreFromInStore(List<Tuple<string, string, SqlOperator>> andWhere = null, List<Tuple<string, string, SqlOperator>> orWhere = null)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append(
                @"SELECT  *
    FROM    ( SELECT    d.CTypeID ,
                        h.BillNo ,
                        d.DrugID ,
                        d.BatchNo ,
                        d.ValidityDate ,
                        d.Amount as pAmount,
                        d.UnitName as PackUnitName ,
                        d.UnitID as packUnitId,
                        d.UnitID,
                        d.Amount,
                        d.UnitName,
                        d.StockFee ,
                        d.StockPrice ,
                        d.RetailFee ,
                        d.RetailPrice ,
                        h.DeptID ,
                        cd.ChemName,
                        s.Amount totalNum,
                        p.ProductName ,
                        cd.Spec
              FROM      dbo.DW_InStoreHead h
                        INNER JOIN dbo.DW_InStoreDetail d ON h.InHeadID = d.InHeadID
                        LEFT JOIN dbo.DG_HospMakerDic md ON md.DrugID = d.DrugID
                        LEFT JOIN dbo.DG_CenterSpecDic cd ON cd.CenteDrugID = md.CenteDrugID
                        LEFT JOIN dbo.DG_ProductDic p ON p.ProductID = md.ProductID
                        LEFT JOIN dbo.DW_Storage s ON s.DrugID = d.DrugID
                                                      AND s.DeptID = h.DeptID
            ) AS T where 1=1 ");
            stb.Append(SqlServerProcess.GetCondition(andWhere, orWhere));
            DataTable dt = oleDb.GetDataTable(stb.ToString());
            return dt;
        }

        /// <summary>
        /// 药库入库明细转出库申请
        /// </summary>
        /// <param name="andWhere">and 条件</param>
        /// <param name="orWhere">or 条件</param>
        /// <returns>出库申请</returns>
        public DataTable GetOutFromApply(List<Tuple<string, string, SqlOperator>> andWhere = null, List<Tuple<string, string, SqlOperator>> orWhere = null)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append(@"
SELECT  *
FROM    ( SELECT    sd.DrugID ,
                    cs.ChemName ,
                    cs.PackAmount ,
                    cs.PackUnit PackUnitName ,
                    cs.packUnitId ,
                    cs.MiniUnit ,
                    cs.MiniUnitID ,
                    0 AS Amount ,
                    sd.Amount as pAmount,
                    cs.Spec ,
                    sd.BatchNO ,
                    sd.UnitID ,
                    dp.ProductName ,
                    sd.Amount AS factAmount ,
                    du.UnitName ,
                    ds.Amount AS totalNum ,
                    bh.RetailPrice * sd.Amount AS RetailFee ,
                    bh.RetailPrice ,
					bh.StockPrice * sd.Amount AS StockFee ,
                    bh.StockPrice ,
                    0 AS DifMoney ,
                    sd.ValidDate ,
                    sd.CTypeID ,
                    cd.CTypeName ,
                    sd.WorkID ,
                    dh.ToDeptID DeptID ,
                    dh.ApplyDeptID ToDeptID ,
                    td.TypeID ,
                    td.TypeName,
                    dh.ApplyHeadID,
                    dsh.OutStoreHeadID,
                    null as OutDetailID
          FROM      dbo.DS_ApplyDetail sd
                    LEFT JOIN dbo.DS_ApplyHead dh ON dh.ApplyHeadID = sd.ApplyHeadID
                    LEFT JOIN DW_Storage ds ON ds.DrugID = sd.DrugID
                                               AND dh.ToDeptID = ds.DeptID
                    INNER JOIN dbo.DG_HospMakerDic h ON h.DrugID = sd.DrugID
                    INNER JOIN dbo.DG_CenterSpecDic cs ON cs.CenteDrugID = h.CenteDrugID
                    INNER JOIN dbo.DG_ProductDic dp ON dp.ProductID = h.ProductID
                    INNER JOIN DG_UnitDic du ON du.UnitID = sd.UnitID
                    LEFT JOIN dbo.DG_ChildTypeDic cd ON cd.CTypeID = sd.CTypeID
                    LEFT JOIN DG_TypeDic td ON td.TypeID = cd.TypeID
                    LEFT JOIN dbo.DW_Batch bh ON sd.DrugID = bh.DrugID
                                                 AND sd.BatchNO = bh.BatchNO
                                                  AND bh.DeptID=dh.ToDeptID
                   LEFT JOIN dbo.DW_OutStoreHead dsh ON dsh.ApplyHeadId= dh.ApplyHeadID
            WHERE dh.AuditFlag=0 AND dh.DelFlag=0
        ) AS T where 1=1 ");
            stb.Append(SqlServerProcess.GetCondition(andWhere, orWhere));
            DataTable dt = oleDb.GetDataTable(stb.ToString());
            return dt;
        }
        #endregion

        #region 月结
        /// <summary>
        /// 根据科室获取月结信息
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <returns>月结信息</returns>
        public DataTable GetMonthBalaceByDept(int deptId)
        {
            string strSql = @"SELECT [BalanceID],[BalanceYear],[BalanceMonth],BeginTime=CONVERT(VARCHAR(20),[BeginTime],120),EndTime=CONVERT(VARCHAR(20),[EndTime],120),[RegEmpID],[RegEmpName],[RegTime],[DeptID],[WorkID] FROM DW_BalanceRecord WHERE DeptID = {0} ORDER BY BalanceID DESC; ";
            return oleDb.GetDataTable(string.Format(strSql, deptId));
        }

        /// <summary>
        /// 执行月结
        /// </summary>
        /// <param name="workId">机构ID</param>
        /// <param name="deptId">科室ID</param>
        /// <param name="empId">操作人ID</param>
        /// <returns>月结结果</returns>
        public DGBillResult ExcutMonthBalance(int workId, int deptId, int empId)
        {
            DGBillResult result = new DGBillResult();
            try
            {
                IDbCommand cmd = oleDb.GetProcCommand("SP_DW_MonthBalance");
                oleDb.AddInParameter(cmd as DbCommand, "@WorkID", DbType.Int32, workId);
                oleDb.AddInParameter(cmd as DbCommand, "@DeptID", DbType.Int32, deptId);
                oleDb.AddInParameter(cmd as DbCommand, "@EmpId", DbType.Int32, empId);
                oleDb.AddOutParameter(cmd as DbCommand, "@ErrCode", DbType.Int32, 5);
                oleDb.AddOutParameter(cmd as DbCommand, "@ErrMsg", DbType.AnsiString, 200);
                oleDb.DoCommand(cmd);

                string msg = oleDb.GetParameterValue(cmd as DbCommand, "@ErrMsg").ToString();
                string code = oleDb.GetParameterValue(cmd as DbCommand, "@ErrCode").ToString();

                result.Result = Convert.ToInt32(code);
                result.ErrMsg = msg;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = 2;
                result.ErrMsg = ex.Message;
                throw;
            }
        }

        /// <summary>
        /// 执行系统审核
        /// </summary>
        /// <param name="workId">机构ID</param>
        /// <param name="deptId">科室ID</param>
        /// <returns>返回结果</returns>
        public DgSpResult ExcutSystemCheckAccount(int workId, int deptId)
        {
            DgSpResult result = new DgSpResult();
            try
            {
                IDbCommand cmd = oleDb.GetProcCommand("SP_DW_CheckAccount");
                oleDb.AddInParameter(cmd as DbCommand, "@WorkID", DbType.Int32, workId);
                oleDb.AddInParameter(cmd as DbCommand, "@DeptID", DbType.Int32, deptId);
                oleDb.AddOutParameter(cmd as DbCommand, "@ErrCode", DbType.Int32, 5);
                oleDb.AddOutParameter(cmd as DbCommand, "@ErrMsg", DbType.AnsiString, 200);
                oleDb.DoCommand(cmd);

                string msg = oleDb.GetParameterValue(cmd as DbCommand, "@ErrMsg").ToString();
                string code = oleDb.GetParameterValue(cmd as DbCommand, "@ErrCode").ToString();

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = (SqlCommand)cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);

                result.Result = Convert.ToInt32(code);
                result.ErrMsg = msg;
                result.Table = dt;
                return result;
            }
            catch (Exception ex)
            {
                result.Result = 2;
                result.ErrMsg = ex.Message;
                return result;
                throw;
            }
        }
        #endregion

        #region 报表统计
        #region 进销存统计报表
        /// <summary>
        /// 获取会计年
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>会计年</returns>
        public DataTable GetAcountYears(int deptId)
        {
            DataTable dt = new DataTable();
            DW_BalanceRecord record = NewDao<IDWDao>().GetMaxBlanceRecord(deptId);
            if (record != null)
            {
                string strSql = @"SELECT DISTINCT BalanceYear AS ID,BalanceYear AS Name FROM DW_Account WHERE DeptID={0} ORDER BY BalanceYear DESC";
                strSql = string.Format(strSql, deptId);
                dt = oleDb.GetDataTable(strSql);
                //if (System.DateTime.Now >= record.EndTime)
                //{
                //    if (record.BalanceMonth == 12)
                //    {
                //        DataRow row = dt.NewRow();
                //        row["ID"] = record.BalanceYear + 1;
                //        row["Name"] = record.BalanceYear + 1;
                //        dt.Rows.InsertAt(row, 0);
                //    }
                //}
            }

            return dt;
        }

        /// <summary>
        /// 获取会计月
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="year">年份</param>
        /// <returns>会计月</returns>
        public DataTable GetAcountMonths(int deptId, int year)
        {
            DataTable dt = new DataTable();
            string strSql = @"SELECT DISTINCT BalanceMonth AS ID,BalanceMonth AS Name FROM DW_Account WHERE DeptID={0} AND BalanceYear={1} ORDER BY BalanceMonth";
            strSql = string.Format(strSql, deptId, year);
            dt = oleDb.GetDataTable(strSql);
            if (dt != null && dt.Rows.Count == 0)
            {
                DataRow row = dt.NewRow();
                row["ID"] = DateTime.Now.Month;
                row["Name"] = DateTime.Now.Month;
                dt.Rows.InsertAt(row, 0);
            }

            //if (DateTime.Now.Year == year)
            //{
            //    DataRow row = dt.NewRow();
            //    if (dt.Select("ID=" + DateTime.Now.Month.ToString()).Length == 0)
            //    {
            //        row["ID"] = DateTime.Now.Month;
            //        row["Name"] = DateTime.Now.Month;
            //        dt.Rows.Add(row);
            //    }
            //}

            return dt;
        }

        /// <summary>
        /// 取得明细账数据
        /// </summary>
        /// <param name="accountType">账本类型</param>
        /// <param name="deptId">科室id</param>
        /// <param name="queryYear">年份</param>
        /// <param name="queryMonth">月份</param>
        /// <param name="typeId">药品类型</param>
        /// <returns>明细账数据集</returns>
        public DataTable GetAccountData(int accountType, int deptId, int queryYear, int queryMonth, int typeId)
        {
            string strSql = @"SELECT   a.LendRetailFee ,
                                        a.LendStockFee ,
                                        a.DebitRetailFee ,
                                        a.DebitStockFee ,
                                        a.OverRetailFee ,
                                        a.OverStockFee
                                FROM    DW_Account a LEFT JOIN DG_HospMakerDic m on a.DrugID=m.DrugID 
                                        LEFT join DG_CenterSpecDic b on b.CenteDrugID=m.CenteDrugID
                                WHERE a.AccountType={0} AND a.BalanceYear={1} AND a.BalanceMonth={2} AND a.DeptID={3} AND b.TypeID={4}";
            string flag = "0";
            if (accountType == 2)
            {
                //判断是否月结
                string sql = "SELECT DISTINCT BalanceFlag FROM DW_Account WHERE DeptID={0} AND BalanceYear={1} AND BalanceMonth={2}";
                sql = string.Format(sql, deptId, queryYear, queryMonth);
                DataTable dtBalanceFlag = oleDb.GetDataTable(sql);
                if (dtBalanceFlag.Rows.Count > 0)
                {
                    flag = dtBalanceFlag.Rows[0][0].ToString();
                    if (flag == "0")
                    {
                        //计算
                        strSql = @"SELECT a.BatchAmount*a.RetailPrice AS OverRetailFee ,
                                           a.BatchAmount*a.StockPrice as OverStockFee
                                    FROM    DW_Batch a  
                                            LEFT JOIN DG_HospMakerDic ho ON a.DrugID = ho.DrugID
                                            LEFT JOIN DG_CenterSpecDic cs ON cs.CenteDrugID = ho.CenteDrugID											
                                WHERE  a.DeptID={0} AND cs.TypeID={1}";
                        strSql = string.Format(strSql, deptId, typeId);
                        return oleDb.GetDataTable(strSql);
                    }
                }
            }

            strSql = string.Format(strSql, accountType, queryYear, queryMonth, deptId, typeId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 取得明细账数据
        /// </summary>
        /// <param name="accountType">账本类型</param>
        /// <param name="deptId">科室id</param>
        /// <param name="queryYear">年份</param>
        /// <param name="queryMonth">月份</param>
        /// <param name="typeId">药品类型</param>
        /// <param name="busiCode">业务代码</param>
        /// <returns>明细账数据</returns>
        public DataTable GetAccountData(int accountType, int deptId, int queryYear, int queryMonth, int typeId, string busiCode)
        {
            string strSql = @"SELECT   a.LendRetailFee ,
                                        a.LendStockFee ,
                                        a.DebitRetailFee ,
                                        a.DebitStockFee ,
                                        a.OverRetailFee ,
                                        a.OverStockFee
                                FROM    DW_Account a LEFT JOIN DG_HospMakerDic m on a.DrugID=m.DrugID 
                                        LEFT join DG_CenterSpecDic b on b.CenteDrugID=m.CenteDrugID
                                WHERE a.AccountType={0} AND a.BalanceYear={1} AND a.BalanceMonth={2} AND a.DeptID={3} AND b.TypeID={4} and a.BusiType='{5}'";

            strSql = string.Format(strSql, accountType, queryYear, queryMonth, deptId, typeId, busiCode);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 取得明细账数据
        /// </summary>
        /// <param name="accountType">账本类型</param>
        /// <param name="deptId">科室id</param>
        /// <param name="queryYear">年份</param>
        /// <param name="queryMonth">月份</param>
        /// <param name="typeId">药品类型</param>
        /// <param name="busiCode">业务代码</param>
        /// <param name="busiTypeModel">业务类型实体</param>
        /// <returns>明细账数据</returns>
        public DataTable GetAccountData(int accountType, int deptId, int queryYear, int queryMonth, int typeId, string busiCode, DGBusiType busiTypeModel)
        {
            string strSql = @"SELECT   a.LendRetailFee ,
                                        a.LendStockFee ,
                                        a.DebitRetailFee ,
                                        a.DebitStockFee ,
                                        a.OverRetailFee ,
                                        a.OverStockFee,
                               " + busiTypeModel.DeptFieldName + @"
                                FROM    DW_Account a LEFT JOIN DG_HospMakerDic m on a.DrugID=m.DrugID 
                                        LEFT join DG_CenterSpecDic b on b.CenteDrugID=m.CenteDrugID
                                        left join " + busiTypeModel.DetailTableName + " on a.DetailID=" + busiTypeModel.DetailIdFieldName + @"
                                        left join " + busiTypeModel.HeadTableName + " on " + busiTypeModel.JoinExpress + @"
                                WHERE a.AccountType={0} AND a.BalanceYear={1} AND a.BalanceMonth={2} AND a.DeptID={3} AND b.TypeID={4} and a.BusiType='{5}' and " + busiTypeModel.DeptFieldName.Replace(" as deptname", string.Empty) + " is not null";

            strSql = string.Format(strSql, accountType, queryYear, queryMonth, deptId, typeId, busiCode);
            return oleDb.GetDataTable(strSql);
        }
        #endregion

        #region 药品汇总表
        /// <summary>
        /// 获取入库业务信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>入库业务信息</returns>
        public DataTable GetInStore(Dictionary<string, string> query)
        {
            string strSql = @"SELECT a.DrugID,a.StockPrice as StockPrice,a.StockFee as StockFee,a.RetailPrice as RetailPrice,a.RetailFee as RetailFee,a.UnitAmount as UnitAmount,d.MiniUnit as UnitName,a.SupplierName as DeptName,c.TradeName,d.ChemName,d.Spec,e.ProductName,a.Price as Price,f.BusiTypeName 
              FROM (select a.StockPrice as StockPrice,sum(a.StockFee) as StockFee,a.RetailPrice as RetailPrice,sum(a.RetailFee) as RetailFee,sum(a.Amount) as UnitAmount,sum(a.RetailFee-a.StockFee) as Price,
			  b.SupplierName,a.DrugID,b.BusiType,b.BillTime,a.DeptID,b.SupplierID,a.WorkID from DW_InStoreDetail a LEFT JOIN DW_InStoreHead b ON a.InHeadID=b.InHeadID 
              WHERE b.DelFlag=0  Group By   b.SupplierName,a.DrugID,b.BusiType,b.BillTime,a.DeptID,b.SupplierID,a.StockPrice,a.RetailPrice,a.WorkID) a   
              LEFT JOIN DG_HospMakerDic c ON a.DrugID=c.DrugID 
              LEFT JOIN DG_CenterSpecDic d ON c.CenteDrugID=d.CenteDrugID
              LEFT JOIN DG_ProductDic e ON c.ProductID=e.ProductID
              LEFT JOIN DG_BusiType f ON a.BusiType=f.BusiCode
             WHERE a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in query)
            {
                if (pair.Key == "Search")
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
                else if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 获取出库业务信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>出库业务信息</returns>
        public DataTable GetOutStore(Dictionary<string, string> query)
        {
            string strSql = @"SELECT a.DrugID,a.StockPrice as StockPrice,a.StockFee as StockFee,a.RetailPrice as RetailPrice,a.RetailFee as RetailFee,a.UnitAmount as UnitAmount,d.MiniUnit as UnitName,a.ToDeptName as DeptName,c.TradeName,d.ChemName,d.Spec,e.ProductName,a.Price as Price,f.BusiTypeName 
              FROM (select a.StockPrice as StockPrice,sum(a.StockFee) as StockFee,a.RetailPrice as RetailPrice,sum(a.RetailFee) as RetailFee,sum(a.Amount) as UnitAmount,sum(a.RetailFee-a.StockFee) as Price,
			  b.ToDeptName,a.DrugID,b.BusiType,b.BillTime,a.DeptID,b.ToDeptID,a.WorkID from DW_OutStoreDetail a LEFT JOIN DW_OutStoreHead b ON a.OutHeadID=b.OutStoreHeadID  WHERE b.DelFlag=0  Group By   b.ToDeptName,a.DrugID,b.BusiType,b.BillTime,a.DeptID,b.ToDeptID,a.StockPrice,a.RetailPrice,a.WorkID) a   
              LEFT JOIN DG_HospMakerDic c ON a.DrugID=c.DrugID 
              LEFT JOIN DG_CenterSpecDic d ON c.CenteDrugID=d.CenteDrugID
              LEFT JOIN DG_ProductDic e ON c.ProductID=e.ProductID
              LEFT JOIN DG_BusiType f ON a.BusiType=f.BusiCode
             WHERE a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in query)
            {
                if (pair.Key == "Search")
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
                else if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 获取盘点信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>盘点信息</returns>
        public DataTable GetCheck(Dictionary<string, string> query)
        {
            string strSql = @"SELECT a.DrugID,a.StockPrice as StockPrice,a.StockFee as StockFee,a.RetailPrice as RetailPrice,a.RetailFee as RetailFee,a.UnitAmount as UnitAmount,d.MiniUnit as UnitName,'' as DeptName,c.TradeName,d.ChemName,d.Spec,e.ProductName,a.Price as Price,f.BusiTypeName 
              FROM (select a.StockPrice as StockPrice,sum(a.FactStockFee-a.ActStockFee) as StockFee,a.RetailPrice as RetailPrice,sum(a.FactRetailFee-a.ActRetailFee) as RetailFee,sum(a.FactAmount-a.ActAmount) as UnitAmount,sum((a.FactRetailFee-a.ActRetailFee)-(a.FactStockFee-a.ActStockFee)) as Price,
			  a.DrugID,b.BusiType,b.AuditTime,a.DeptID,a.WorkID from DW_AuditDetail a LEFT JOIN DW_AuditHead b on a.AuditHeadID=b.AuditHeadID   WHERE b.DelFlag=0  Group By   a.DrugID,b.BusiType,b.AuditTime,a.DeptID,a.StockPrice,a.RetailPrice,a.WorkID) a 
              LEFT JOIN DG_HospMakerDic c ON a.DrugID=c.DrugID 
              LEFT JOIN DG_CenterSpecDic d ON c.CenteDrugID=d.CenteDrugID
              LEFT JOIN DG_ProductDic e ON c.ProductID=e.ProductID
              LEFT JOIN DG_BusiType f ON a.BusiType=f.BusiCode
             WHERE a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in query)
            {
                if (pair.Key == "Search")
                {
                    strWhere.AppendFormat(" AND {0}", pair.Value);
                }
                else if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat(" AND {0}", pair.Value);
                }
            }

            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 获取调价信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>调价信息</returns>
        public DataTable GetAdjPrice(Dictionary<string, string> query)
        {
            string strSql = @"SELECT a.DrugID,a.StockPrice,a.StockFee as StockFee,a.NewRetailPrice,a.AdjRetailFee,a.AdjAmount,d.MiniUnit as UnitName,a.DrugID,'' as DeptName,c.TradeName,d.ChemName,d.Spec,e.ProductName,a.Price as Price,g.BusiTypeName 
		      FROM (select 
			  f.StockPrice as StockPrice,sum(f.StockPrice*a.AdjAmount) as StockFee,a.NewRetailPrice as NewRetailPrice,sum(a.AdjRetailFee) as AdjRetailFee,
			  sum(a.AdjAmount) as AdjAmount,sum(a.AdjRetailFee-(f.StockPrice*a.AdjAmount)) as Price,
			  b.BusiType,a.DeptID,a.WorkID,b.ExecTime,a.DrugID 
			  from DG_AdjDetail a LEFT JOIN DG_AdjHead b on a.AdjHeadID=b.AdjHeadID LEFT JOIN DW_Batch f ON a.BatchNO=f.BatchNO WHERE b.DelFlag=0  Group By  b.BusiType,a.DeptID,a.WorkID,b.ExecTime,a.NewRetailPrice,f.StockPrice,a.DrugID) a
              LEFT JOIN DG_HospMakerDic c ON a.DrugID=c.DrugID 
              LEFT JOIN DG_CenterSpecDic d ON c.CenteDrugID=d.CenteDrugID
              LEFT JOIN DG_ProductDic e ON c.ProductID=e.ProductID
              LEFT JOIN DG_BusiType g ON a.BusiType=g.BusiCode
             WHERE a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in query)
            {
                if (pair.Key == "Search")
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
                else if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }
        #endregion

        #region 药品明细查询
        /// <summary>
        /// 获取会计月日期
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>会计月日期</returns>
        public DataTable GetBalanceDate(int deptId, int year, int month)
        {
            string strSql = @"SELECT BeginTime,EndTime FROM DW_BalanceRecord WHERE DeptID={0} AND BalanceYear={1} AND BalanceMonth={2}";
            strSql = string.Format(strSql, deptId, year, month);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 取得药品明细账数据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="beginTime">开始日期</param>
        /// <param name="endTime">结束日期</param>
        /// <param name="drugId">药品Id</param>
        /// <param name="queryType">查询类型1自然月2会计月</param>
        /// <returns>药品明细账数据</returns>
        public DataTable GetAccountDetail(int deptId, int year, int month, string beginTime, string endTime, int drugId, int queryType)
        {
            string dateWhere = string.Empty;
            string strSql = @"SELECT  AccountID ,
                            (CASE AccountType WHEN 0  THEN '发生' WHEN 1 THEN '期初' WHEN 2 THEN '期末' WHEN 3 THEN '调整' END) as AccountType ,
                            b.BusiTypeName AS OpType ,
                            BusiType ,
                            BalanceYear ,
                            BalanceMonth ,
                            RetailPrice ,
                            StockPrice ,
                            BatchNO ,
                            BillNO ,
                            LendAmount ,
                            LendRetailFee ,
                            LendStockFee ,
                            DebitAmount ,
                            DebitRetailFee ,
                            DebitStockFee ,
                            OverAmount ,
                            OverRetailFee ,
                            OverStockFee ,
                            RegTime ,
                            BalanceFlag ,
                            BalanceID ,
                            DrugID ,
                            CTypeID ,
                            DeptID ,
                            DetailID,
		                    UnitName
                    FROM    DW_Account a
                            LEFT JOIN DG_BusiType b ON a.BusiType = b.BusiCode
		                    WHERE DeptID={0} AND DrugID={1} and WorkID={2} ";
            strSql = string.Format(strSql, deptId, drugId, oleDb.WorkId);
            if (queryType == 1)
            {
                dateWhere = " AND RegTime BETWEEN '{0}' AND '{1}' ";
                dateWhere = string.Format(dateWhere, beginTime, endTime);
            }
            else
            {
                dateWhere = " AND BalanceMonth={0} AND BalanceYear={1} ";
                dateWhere = string.Format(dateWhere, month, year);
            }

            strSql = strSql + dateWhere + " order by RegTime desc,OverAmount asc";
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 取得药品明细账汇总数据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="beginTime">开始日期</param>
        /// <param name="endTime">结束日期</param>
        /// <param name="drugId">药品Id</param>
        /// <param name="queryType">查询类型1自然月2会计月</param>
        /// <returns>药品明细账汇总数据</returns>
        public DataTable GetAccountDetailSum(int deptId, int year, int month, string beginTime, string endTime, int drugId, int queryType)
        {
            string dateWhere = string.Empty;
            string strSql = @"SELECT    SUM(ISNULL(OverAmount,0)) - SUM(ISNULL(LendAmount,0)) + SUM(ISNULL(DebitAmount,0)) as OpenAmount,
                                        SUM(ISNULL(OverRetailFee,0)) -SUM(ISNULL(LendRetailFee,0))+SUM(ISNULL(DebitRetailFee,0)) as OpenRetailFee,
                                        SUM(ISNULL(LendAmount,0)) AS LendAmount ,
                                        SUM(ISNULL(LendRetailFee,0)) AS LendRetailFee,
                                        SUM(ISNULL(DebitAmount,0)) AS DebitAmount ,
                                        SUM(ISNULL(DebitRetailFee,0)) AS DebitRetailFee,
                                        SUM(ISNULL(OverAmount,0)) AS OverAmount,
                                        SUM(ISNULL(OverRetailFee,0)) OverRetailFee
                    FROM    DW_Account a
                            LEFT JOIN DG_BusiType b ON a.BusiType = b.BusiCode
		                    WHERE DeptID={0} AND DrugID={1} and WorkID={2} ";
            strSql = string.Format(strSql, deptId, drugId, oleDb.WorkId);
            if (queryType == 1)
            {
                dateWhere = " AND RegTime BETWEEN '{0}' AND '{1}' ";
                dateWhere = string.Format(dateWhere, beginTime, endTime);
            }
            else
            {
                dateWhere = " AND BalanceMonth={0} AND BalanceYear={1} ";
                dateWhere = string.Format(dateWhere, month, year);
            }

            strSql = strSql + dateWhere + " GROUP BY DrugID";
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }
        #endregion

        #region 药品流水账
        /// <summary>
        /// 获取入库业务信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>入库业务信息</returns>
        public DataTable GetInStores(Dictionary<string, string> query)
        {
            string strSql = @"SELECT b.BillTime,a.BillNo,a.DrugID,a.StockPrice,a.StockFee,a.RetailPrice,a.RetailFee,a.Amount as UnitAmount,d.PackUnit as UnitName,a.DrugID,b.SupplierName as DeptName,c.TradeName,d.ChemName,d.Spec,e.ProductName,(a.RetailFee-a.StockFee) as Price,f.BusiTypeName FROM DW_InStoreDetail a 
              LEFT JOIN DW_InStoreHead b ON a.InHeadID=b.InHeadID
              LEFT JOIN DG_HospMakerDic c ON a.DrugID=c.DrugID 
              LEFT JOIN DG_CenterSpecDic d ON c.CenteDrugID=d.CenteDrugID
              LEFT JOIN DG_ProductDic e ON c.ProductID=e.ProductID
              LEFT JOIN DG_BusiType f ON b.BusiType=f.BusiCode
             WHERE b.DelFlag=0 AND  a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in query)
            {
                if (pair.Key == "Search" || pair.Key == "SearchNo")
                {
                    strWhere.AppendFormat(" AND {0}", pair.Value);
                }
                else if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            strWhere.Append(" order by b.BillTime  DESC");
            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 获取出库业务信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>出库业务信息</returns>
        public DataTable GetOutStores(Dictionary<string, string> query)
        {
            string strSql = @"SELECT b.BillTime,a.BillNo,a.DrugID,a.StockPrice,a.StockFee,a.RetailPrice,a.RetailFee,a.Amount as UnitAmount,d.PackUnit as UnitName,a.DrugID,b.ToDeptName as DeptName,c.TradeName,d.ChemName,d.Spec,e.ProductName,(a.RetailFee-a.StockFee) as Price,f.BusiTypeName FROM DW_OutStoreDetail a 
              LEFT JOIN DW_OutStoreHead b on a.OutHeadID=b.OutStoreHeadID 
              LEFT JOIN DG_HospMakerDic c ON a.DrugID=c.DrugID 
              LEFT JOIN DG_CenterSpecDic d ON c.CenteDrugID=d.CenteDrugID
              LEFT JOIN DG_ProductDic e ON c.ProductID=e.ProductID
              LEFT JOIN DG_BusiType f ON b.BusiType=f.BusiCode
             WHERE b.DelFlag=0 AND  a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in query)
            {
                if (pair.Key == "Search" || pair.Key == "SearchNo")
                {
                    strWhere.AppendFormat(" AND {0}", pair.Value);
                }
                else if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            strWhere.Append(" order by b.BillTime  DESC");
            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 获取盘点信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>盘点信息</returns>
        public DataTable GetChecks(Dictionary<string, string> query)
        {
            string strSql = @"SELECT b.AuditTime,a.BillNo,a.DrugID,a.StockPrice,(a.FactStockFee-a.ActStockFee) as StockFee,a.RetailPrice,(a.FactRetailFee-a.ActRetailFee) as RetailFee,(a.FactAmount-a.ActAmount) as UnitAmount,d.PackUnit as UnitName,a.DrugID,'' as DeptName,c.TradeName,d.ChemName,d.Spec,e.ProductName,((a.FactRetailFee-a.ActRetailFee)-(a.FactStockFee-a.ActStockFee)) as Price,f.BusiTypeName FROM DW_AuditDetail a 
              LEFT JOIN DW_AuditHead b on a.AuditHeadID=b.AuditHeadID 
              LEFT JOIN DG_HospMakerDic c ON a.DrugID=c.DrugID 
              LEFT JOIN DG_CenterSpecDic d ON c.CenteDrugID=d.CenteDrugID
              LEFT JOIN DG_ProductDic e ON c.ProductID=e.ProductID
              LEFT JOIN DG_BusiType f ON b.BusiType=f.BusiCode
             WHERE b.DelFlag=0 AND  a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in query)
            {
                if (pair.Key == "Search" || pair.Key == "SearchNo")
                {
                    strWhere.AppendFormat(" AND {0}", pair.Value);
                }
                else if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat(" AND {0}", pair.Value);
                }
            }

            strWhere.Append(" order by b.AuditTime  DESC");
            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 获取调价信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>调价信息</returns>
        public DataTable GetAdjPrices(Dictionary<string, string> query)
        {
            string strSql = @"SELECT b.ExecTime,a.BillNo,a.DrugID,f.StockPrice,(f.StockPrice*a.AdjAmount) as StockFee,a.NewRetailPrice,a.AdjRetailFee,a.AdjAmount,d.PackUnit as UnitName,a.DrugID,'' as DeptName,c.TradeName,d.ChemName,d.Spec,e.ProductName,(a.AdjRetailFee-(f.StockPrice*a.AdjAmount)) as Price,g.BusiTypeName FROM DG_AdjDetail a 
              LEFT JOIN DG_AdjHead b on a.AdjHeadID=b.AdjHeadID 
              LEFT JOIN DG_HospMakerDic c ON a.DrugID=c.DrugID 
              LEFT JOIN DG_CenterSpecDic d ON c.CenteDrugID=d.CenteDrugID
              LEFT JOIN DG_ProductDic e ON c.ProductID=e.ProductID
              LEFT JOIN DW_Batch f ON a.BatchNO=f.BatchNO
              LEFT JOIN DG_BusiType g ON b.BusiType=g.BusiCode
             WHERE b.DelFlag=0 AND  a.WorkID = {0}";
            strSql = string.Format(strSql, oleDb.WorkId);
            StringBuilder strWhere = new StringBuilder();
            foreach (var pair in query)
            {
                if (pair.Key == "Search" || pair.Key == "SearchNo")
                {
                    strWhere.AppendFormat(" AND {0}", pair.Value);
                }
                else if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat("AND {0}", pair.Value);
                }
            }

            strWhere.Append(" order by b.ExecTime DESC");
            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }
        #endregion

        #region 采购入库对比
        /// <summary>
        /// 采购入库对比
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <param name="yearMonth">年月</param>
        /// <param name="drugName">药品名称</param>
        /// <returns>采购入库对比数据集</returns>
        public DataTable GetBuyComparison(int deptId, string yearMonth, string drugName)
        {
            DateTime dt = Convert.ToDateTime(yearMonth + "-01");

            //本月第一天时间    
            DateTime dtFirst = dt.AddDays(-(dt.Day) + 1);

            //将本月月数+1  
            DateTime dt2 = dt.AddMonths(1);

            //本月最后一天时间  
            DateTime dtLast = dt2.AddDays(-(dt.Day));
            string beginDate = string.Empty;
            string endDate = string.Empty;
            beginDate = dtFirst.ToString("yyyy-MM-dd 00:00:00");
            endDate = dtLast.ToString("yyyy-MM-dd 23:59:59");
            string strSql = @"SELECT  ISNULL(k1.DrugID, k2.DrugID) AS DrugID ,
                            ISNULL(k1.ChemName, k2.ChemName) AS ChemName ,
                            ISNULL(k1.Spec, k2.Spec) AS Spec ,
                            ISNULL(k1.ProductName, k2.ProductName) AS ProductName ,
                            ISNULL(k1.UnitName, k2.UnitName) AS UnitName ,
                            ISNULL(k2.Amount, 0) AS InAmount ,
                            ISNULL(k2.StockPrice, 0) AS InStockPrice ,
                            ISNULL(k2.RetailPrice, 0) AS InRetailPrice ,
                            ISNULL(k2.StockFee, 0) AS InStockFee ,
                            ISNULL(k2.RetailFee, 0) AS InRetailFee ,
                            ISNULL(k1.Amount, 0) AS PlanAmount ,
                            ISNULL(k1.StockFee, 0) AS PlanStockFee ,
                            ISNULL(k1.RetailFee, 0) AS RetailFee
                    FROM    ( SELECT    a1.DrugID ,
                                        c1.ChemName ,
                                        c1.Spec ,
                                        d1.ProductName ,
                                        a1.UnitName ,
                                        SUM(a1.Amount) AS Amount ,
                                        SUM(a1.StockFee) AS StockFee ,
                                        SUM(a1.RetailFee) AS RetailFee
                              FROM      DW_PlanDetail a1
                                        INNER JOIN DW_PlanHead e1 ON a1.PlanHeadID = e1.PlanHeadID
                                        LEFT JOIN DG_HospMakerDic b1 ON a1.DrugID = b1.DrugID
                                        LEFT JOIN DG_CenterSpecDic c1 ON b1.CenteDrugID = c1.CenteDrugID
                                        LEFT JOIN DG_ProductDic d1 ON b1.ProductID = d1.ProductID
                              WHERE     b1.IsStop = 0
                                        AND c1.IsStop = 0
                                        AND e1.AuditFlag = 1
                                        AND e1.DeptID = {0}
			                            AND e1.PlanDate BETWEEN '{1}' AND '{2}'
                                        AND ( c1.ChemName LIKE '%{3}%'
                                              OR c1.PYCode LIKE '%{3}%'
                                              OR c1.WBCode LIKE '%{3}%'
                                              OR a1.DrugID LIKE '%{3}%'
                                            )
                              GROUP BY  a1.DrugID ,
                                        c1.ChemName ,
                                        c1.Spec ,
                                        d1.ProductName ,
                                        a1.UnitName
                            ) k1
                            FULL JOIN ( SELECT  a2.DrugID ,
                                                c2.ChemName ,
                                                c2.Spec ,
                                                d2.ProductName ,
                                                a2.UnitName ,
                                                SUM(a2.Amount) AS Amount ,
                                                AVG(a2.StockPrice) AS StockPrice ,
                                                AVG(a2.RetailPrice) AS RetailPrice ,
                                                SUM(a2.StockFee) AS StockFee ,
                                                SUM(a2.RetailFee) AS RetailFee
                                        FROM    DW_InStoreDetail a2
                                                INNER JOIN DW_InStoreHead e2 ON a2.InHeadID = e2.InHeadID
                                                LEFT JOIN DG_HospMakerDic b2 ON a2.DrugID = b2.DrugID
                                                LEFT JOIN DG_CenterSpecDic c2 ON b2.CenteDrugID = c2.CenteDrugID
                                                LEFT JOIN DG_ProductDic d2 ON b2.ProductID = d2.ProductID
                                        WHERE   b2.IsStop = 0
                                                AND c2.IsStop = 0
                                                AND e2.AuditFlag = 1
                                                AND e2.DeptID = {0}
			                                    AND e2.AuditTime BETWEEN '{1}' AND '{2}'
                                                AND ( c2.ChemName LIKE '%{3}%'
                                                      OR c2.PYCode LIKE '%{3}%'
                                                      OR c2.WBCode LIKE '%{3}%'
                                                      OR a2.DrugID LIKE '%{3}%'
                                                    )
                                        GROUP BY a2.DrugID ,
                                                c2.ChemName ,
                                                c2.Spec ,
                                                d2.ProductName ,
                                                a2.UnitName
                                      ) k2 ON k1.DrugID = k2.DrugID";
            strSql = string.Format(strSql, deptId, beginDate, endDate, drugName);
            return oleDb.GetDataTable(strSql);
        }
        #endregion

        #region 新药入库统计
        /// <summary>
        /// 新药入库统计
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <param name="yearMonth">年月</param>
        /// <param name="drugName">药品名称</param>
        /// <returns>新药入库统计数据集</returns>
        public DataTable GetNewDrug(int deptId, string yearMonth, string drugName)
        {
            DateTime dt = Convert.ToDateTime(yearMonth + "-01");

            //本月第一天时间    
            DateTime dtFirst = dt.AddDays(-(dt.Day) + 1);

            //将本月月数+1  
            DateTime dt2 = dt.AddMonths(1);

            //本月最后一天时间  
            DateTime dtLast = dt2.AddDays(-(dt.Day));
            string beginDate = string.Empty;
            string endDate = string.Empty;
            beginDate = dtFirst.ToString("yyyy-MM-dd 00:00:00");
            endDate = dtLast.ToString("yyyy-MM-dd 23:59:59");
            string strSql = @"SELECT * FROM (SELECT a.DrugID,a.RegTime,c.ChemName,c.Spec,d.ProductName,a.Amount,a.UnitName,c.PYCode,c.WBCode,a.DeptID,
                                        AVG(f.RetailPrice) AS RetailPrice,AVG(f.StockPrice) AS StockPrice,
                                        SUM(f.RetailPrice*f.BatchAmount) AS RetailFee,SUM(f.StockPrice*f.BatchAmount) AS StockFee
                                        FROM DW_Storage a
										LEFT JOIN dbo.DW_Batch f ON a.DrugID=f.DrugID
                                        LEFT JOIN DG_HospMakerDic b ON a.DrugID = b.DrugID
                                        LEFT JOIN DG_CenterSpecDic c ON b.CenteDrugID = c.CenteDrugID
                                        LEFT JOIN DG_ProductDic d ON b.ProductID = d.ProductID
                                        WHERE a.WorkID={0} AND (a.RegTime BETWEEN '{1}' AND '{2}') GROUP BY a.DrugID,a.RegTime,c.ChemName,c.Spec,d.ProductName,a.Amount,a.UnitName,c.PYCode,c.WBCode,a.DeptID) AS data
                                        WHERE (ChemName LIKE '%{3}%'
                                              OR PYCode LIKE '%{3}%'
                                              OR WBCode LIKE '%{3}%'
                                            ) AND DeptID = {4}";
            strSql = string.Format(strSql, oleDb.WorkId, beginDate, endDate, drugName, deptId);
            return oleDb.GetDataTable(strSql);
        }
        #endregion
        #endregion
    }
}
