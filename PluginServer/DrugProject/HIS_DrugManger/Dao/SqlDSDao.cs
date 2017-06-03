using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.DrugManage;

namespace HIS_DrugManger.Dao
{
    /// <summary>
    /// 药房系统SQLSERVER数据库访问
    /// </summary>
    public class SqlDSDao : AbstractDao, IDSDao
    {
        #region 出库单
        /// <summary>
        /// 加载主表
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>主表</returns>
        public DataTable LoadOutStoreHead(Dictionary<string, string> queryCondition)
        {
            StringBuilder stb = new StringBuilder();
            stb.AppendFormat(@" SELECT  *
        FROM    ( SELECT    ds.* ,
                            db.BusiTypeName ,
                            ds.RetailFee-ds.StockFee AS DiffMoney,
                            d.Name,
                            bd.Name CurrentDept
                  FROM      DS_OutStoreHead ds
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

            stb.Append(" order by AuditTime,RegTime ");
            return oleDb.GetDataTable(stb.ToString());
        }

        /// <summary>
        /// 出库明细单数据
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>明细单数据</returns>
        public DataTable LoadOutStoreDetail(Dictionary<string, string> queryCondition)
        {
            StringBuilder stb = new StringBuilder();
            stb.AppendFormat(
                @"SELECT * FROM (
      SELECT  sd.* ,
      sd.Amount%cs.PackAmount AS uAmount ,
      CAST(sd.Amount AS INT)/CAST(cs.PackAmount AS INT) as pAmount,
        cs.ChemName ,
        cs.Spec ,
        cs.PackAmount ,
        cs.PackUnit PackUnitName ,
        cs.PackUnitID ,
        dp.ProductName ,
        ds.Amount AS totalNum ,
        sd.RetailFee - sd.StockFee AS DifMoney ,
        cd.CTypeName ,
        td.TypeID ,
        td.TypeName
FROM    DS_OutStoreDetail sd
        LEFT JOIN DS_OutStoreHead dh ON dh.OutStoreHeadID = sd.OutHeadID
        LEFT JOIN DS_Storage ds ON ds.DrugID = sd.DrugID
                                   AND dh.DeptID = ds.DeptID
        INNER JOIN dbo.DG_HospMakerDic h ON h.DrugID = sd.DrugID
        INNER JOIN dbo.DG_CenterSpecDic cs ON cs.CenteDrugID = h.CenteDrugID
        INNER JOIN dbo.DG_ProductDic dp ON dp.ProductID = h.ProductID
        INNER JOIN DG_UnitDic du ON du.UnitID = sd.UnitID
        LEFT JOIN dbo.DG_ChildTypeDic cd ON cd.CTypeID = sd.CTypeID
        LEFT JOIN DG_TypeDic td ON td.TypeID = cd.TypeID 
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
        /// 查询科室库存药品信息
        /// </summary>
        /// <param name="dept">科室ID</param>
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
        c.PackAmount,
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
FROM    dbo.DS_Storage s
        INNER JOIN DG_HospMakerDic h ON h.DrugID = s.DrugID
        INNER JOIN dbo.DG_CenterSpecDic c ON h.CenteDrugID = c.CenteDrugID
        LEFT JOIN dbo.DG_ProductDic pd ON pd.ProductID = h.ProductID
        LEFT JOIN DG_UnitDic ud ON ud.UnitID = c.PackUnitID
		LEFT JOIN dbo.DS_Batch db ON db.DrugID=h.DrugID AND db.DeptID=s.DeptID
	WHERE 1 = 1 AND s.DelFlag = 0 and h.IsStop=0 AND c.IsStop=0 AND db.DelFlag=0 AND s.DeptID={0} ",
    dept);
            return oleDb.GetDataTable(stb.ToString());
        }
        #endregion

        #region 入库单
        /// <summary>
        /// 查询入库单明细
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>入库单表头</returns>
        public DataTable LoadInStoreDetail(Dictionary<string, string> queryCondition)
        {
            string strSql = @"SELECT a.*,c.ChemName,c.Spec,d.ProductName,e.CTypeName,
                             a.Amount%c.PackAmount AS uAmount  ,
                             CAST(a.Amount AS INT)/CAST(c.PackAmount AS INT) as pAmount,
                            a.RetailFee - a.StockFee AS DiffFee,c.PackAmount,
							c.PackUnit PackUnitName,c.packUnitId,
							c.MiniUnit,c.MiniUnitID  
                                FROM DS_InStoreDetail a
                                LEFT JOIN DG_HospMakerDic b
                                ON a.DrugID = b.DrugID
                                LEFT JOIN DG_CenterSpecDic c
                                ON b.CenteDrugID = c.CenteDrugID
                                LEFT JOIN DG_ProductDic d
                                ON b.ProductID = d.ProductID
                                LEFT JOIN DG_ChildTypeDic e
                                ON a.CTypeID = e.CTypeID
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
        /// 查询入库单表头
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>入库单明细</returns>
        public DataTable LoadInStoreHead(Dictionary<string, string> queryCondition)
        {
            string strSql = @"   SELECT  *
                            FROM    ( SELECT    a.[InHeadID]
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
                            ,a.[BillNO]
                            ,a.[StockFee]
                            ,a.[RetailFee]
                            ,a.[InvoiceNO]
                            ,a.[InvoiceDate]
                            ,a.[BillTime]
                            ,a.[SupplierID]
                            ,a.[SupplierName]
                            ,a.[DeliveryNO]
                            ,a.[PayFlag]
                            ,a.[PayRecordID]
                            ,a.[DeptID]
                            ,a.[WorkID]
                            ,a.[OutStoreHeadID],
                            b.BusiTypeName ,
                            bd.Name
                  FROM      DS_InstoreHead a
                            LEFT JOIN DG_BusiType b ON a.BusiType = b.BusiCode
                            LEFT JOIN dbo.BaseDept bd ON bd.DeptId = a.DeptID
                ) AS a
                                WHERE DelFlag = 0 AND WorkID = {0}  ";
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

            strWhere.Append(" order by AuditTime,RegTime ");
            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        ///领药申请主表
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>领药申请</returns>
        public DataTable LoadApplyStoreHead(Dictionary<string, string> queryCondition)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(@"SELECT * FROM DS_ApplyHead a WHERE a.DelFlag = 0 AND a.WorkID = {0} ", oleDb.WorkId);
            foreach (var pair in queryCondition)
            {
                if (pair.Key != string.Empty)
                {
                    strWhere.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
                }
                else
                {
                    strWhere.AppendFormat(" AND {0}", pair.Value);
                }
            }

            strWhere.Append(" ORDER BY ApplyHeadID desc ");

            return oleDb.GetDataTable(strWhere.ToString());
        }

        /// <summary>
        /// 申请表明细
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>表明细</returns>
        public DataTable LoadApplyStoreDetail(Dictionary<string, string> queryCondition)
        {
            StringBuilder stb = new StringBuilder();
            stb.AppendFormat(
                @"SELECT * FROM (
                                    SELECT  sd.DrugID ,
                                    cs.ChemName ,
                                    cs.Spec ,
                                    sd.BatchNO ,
                                    sd.UnitID ,
                                    dp.ProductName ,
                                    sd.Amount ,
                                    sd.FactAmount,
                                    du.UnitName ,
                                    ds.Amount AS totalNum ,
                                    db.RetailPrice ,
                                    db.RetailPrice * sd.Amount AS RetailFee ,
                                    db.StockPrice ,
                                    db.StockPrice * sd.Amount AS StockFee ,
                                    db.RetailPrice * sd.FactAmount AS factRFee ,
                                    db.StockPrice * sd.FactAmount AS factSFee ,
                                    sd.CTypeID ,
                                    cd.CTypeName ,
                                    sd.WorkID ,
                                    dh.ApplyDeptID ,
                                    dh.ApplyDeptName ,
                                    dh.ToDeptID ,
                                    dh.ToDeptName ,
                                    td.TypeID ,
                                    td.TypeName ,
		                            sd.ApplyHeadID,
									sd.ApplyDetailID
                            FROM    DS_ApplyDetail sd
                                    LEFT JOIN DS_ApplyHead dh ON dh.ApplyHeadID = sd.ApplyHeadID
                                    LEFT JOIN DW_Storage ds ON ds.DrugID = sd.DrugID
                                                              AND dh.ToDeptID = ds.DeptID
									LEFT JOIN dbo.DW_Batch db ON db.BatchNO=sd.BatchNO
									AND sd.DrugID=db.DrugID AND dh.ToDeptID=db.DeptID
                                    INNER JOIN dbo.DG_HospMakerDic h ON h.DrugID = sd.DrugID
                                    INNER JOIN dbo.DG_CenterSpecDic cs ON cs.CenteDrugID = h.CenteDrugID
                                    INNER JOIN dbo.DG_ProductDic dp ON dp.ProductID = h.ProductID
                                    INNER JOIN DG_UnitDic du ON du.UnitID = sd.UnitID
                                    LEFT JOIN dbo.DG_ChildTypeDic cd ON cd.CTypeID = sd.CTypeID
                                    LEFT JOIN DG_TypeDic td ON td.TypeID = cd.TypeID
                            WHERE   h.IsStop = 0
                                    AND cs.IsStop = 0
                                    AND SD.WorkID = {0}
		) AS t where 1=1 ",
        oleDb.WorkId);
            foreach (var pair in queryCondition)
            {
                stb.AppendFormat(" AND {0}={1} ", pair.Key, pair.Value);
            }

            return oleDb.GetDataTable(stb.ToString());
        }

        /// <summary>
        /// 查询药品入库批次ShowCard
        /// </summary>
        /// <param name="deptID">批次ShowCard数据源</param>
        /// <returns>药品入库批次</returns>
        public DataTable GetBatchForInstoreShowCard(int deptID)
        {
            string strSql = @"SELECT ValidityTime=Convert(varchar(10),ValidityTime,120),BatchNO,DrugID,StockPrice,RetailPrice FROM DS_Batch
                              WHERE DelFlag = 0 AND DeptID = {0} AND BatchAmount>0 ";
            strSql = string.Format(strSql, deptID.ToString());
            return oleDb.GetDataTable(strSql);
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
                              b.StockPrice,b.RetailPrice,b.NationalCode 
                              FROM DG_CenterSpecDic a
                              LEFT JOIN DG_HospMakerDic b
                              ON a.CenteDrugID = b.CenteDrugID
                              LEFT JOIN DG_ProductDic c
                              ON b.ProductID = c.ProductID
                              WHERE a.IsStop = 0 AND b.IsStop = 0";
                return oleDb.GetDataTable(strSql);
            }
            else
            {
                string strSql = @"SELECT b.DrugID,a.ChemName,b.TradeName,a.Spec,b.ProductID,c.ProductName,
                              a.PYCode,a.WBCode,b.PYCode AS TPYCode, b.WBCode AS TWBCode,a.CTypeID,
                              a.PackUnitID,a.PackUnit,a.PackAmount,b.StockPrice,b.RetailPrice
                              FROM DG_CenterSpecDic a
                              LEFT JOIN DG_HospMakerDic b
                              ON a.CenteDrugID = b.CenteDrugID
                              LEFT JOIN DG_ProductDic c
                              ON b.ProductID = c.ProductID
                              LEFT JOIN DS_Storage d
                              ON b.DrugID=d.DrugID
                              WHERE a.IsStop = 0 AND b.IsStop = 0 AND d.DeptID={0}";
                return oleDb.GetDataTable(string.Format(strSql, deptID.ToString()));
            }
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
                                        a.LastStockPrice ,
                                        a.RegTime ,
                                        a.UpperLimit ,
                                        a.LowerLimit ,
                                        a.UnitID,
                                        c.MiniUnit AS UnitName ,
                                        a.UnitAmount ,
                                        a.DelFlag ,
                                        a.DeptID ,
                                        a.ResolveFlag ,
                                        a.LocationID
                                FROM    DS_Storage a
                                        INNER JOIN DG_HospMakerDic b ON a.DrugID = b.DrugID
                                        INNER JOIN DG_CenterSpecDic c ON c.CenteDrugID = b.CenteDrugID
                                        INNER JOIN DG_ProductDic d ON d.ProductID = b.ProductID
                                WHERE   a.DelFlag = 0
                                        AND a.WorkID = {0}";
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
        public void SaveStoreLimit(List<DS_Storage> details)
        {
            string strSql = string.Empty;
            foreach (DS_Storage s in details)
            {
                strSql = "UPDATE DS_Storage SET UpperLimit={0},LowerLimit={1} WHERE StorageID={2}";
                strSql = string.Format(strSql, s.UpperLimit, s.LowerLimit, s.StorageID);
                oleDb.DoCommand(strSql);
            }
        }
        #endregion

        #region 库存查询
        /// <summary>
        /// 有效库存
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="drugId">药品ID</param>
        /// <returns>有效库存对象</returns>
        public DS_ValidStorage GetValidStoreageInfo(int deptId, int drugId)
        {
            string strSql = "SELECT * FROM DS_ValidStorage WHERE DeptID={0} AND DrugID={1} AND WorkID={2}";
            return oleDb.Query<DS_ValidStorage>(string.Format(strSql, deptId, drugId, oleDb.WorkId), string.Empty).FirstOrDefault();
        }

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
                                        a.Amount AS BaseAmount,
		                                CEILING(a.Amount/(case when (a.UnitAmount is null or a.UnitAmount=0 ) then 1 else a.UnitAmount END) ) AS PackAmount,
										f.ValidAmount,
		                                dbo.fnGetPackAmount(f.ValidAmount,UnitAmount,a.UnitName,c.PackUnit) AS ValidPackAmount,
                                        a.Place ,
                                        a.LastStockPrice ,
                                        a.RegTime ,
                                        a.UpperLimit ,
                                        a.LowerLimit ,
                                        a.UnitID ,
                                        c.PackUnit AS UnitName ,
                                        c.MiniUnit,
                                        a.UnitAmount ,
                                        a.DelFlag ,
                                        a.DeptID ,
                                        dbo.fnGetPackAmount(a.Amount,UnitAmount,a.UnitName,c.PackUnit) as NewAmount,
                                        case when (a.ResolveFlag=1) then '已拆零' else '未拆零' end as ResolveFlag ,										
                                        ISNULL(e.LocationName,'') as LocationID
                                FROM    DS_Storage a
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

            return oleDb.GetDataTable(strSql + strWhere);
        }

        /// <summary>
        /// 查询药品批次信息
        /// </summary>
        /// <param name="storageID">库存ID</param>
        /// <returns>药品批次信息列表</returns>
        public DataTable LoadDrugBatch(int storageID)
        {
            string strSql = @"SELECT  0 ck,a.BatchID ,
                                        a.StorageID ,
                                        a.DeptID ,
                                        a.DrugID ,
                                        c.ChemName ,
                                        a.BatchNO ,
                                        a.StockPrice ,
                                        a.RetailPrice ,
                                        a.InstoreTime ,
                                        dbo.fnGetPackAmount(a.BatchAmount,a.UnitAmount,a.UnitName,c.PackUnit) as NewBatchAmount ,
                                        a.BatchAmount ,
                                        a.UnitID ,
                                        a.UnitName ,
                                        a.ValidityTime ,
                                        a.DelFlag ,
										ISNULL(a.StockPrice, 0)*ISNULL(a.BatchAmount, 0)/ISNULL(a.UnitAmount, 0) as StockFee,
                                        ISNULL(a.RetailPrice, 0)*ISNULL(a.BatchAmount, 0)/ISNULL(a.UnitAmount, 0) as RetailFee,
                                        ((ISNULL(a.RetailPrice, 0)*ISNULL(a.BatchAmount, 0)/ISNULL(a.UnitAmount, 0))
                                          - (ISNULL(a.StockPrice, 0)*ISNULL(a.BatchAmount, 0)/ISNULL(a.UnitAmount, 0))) AS FeeDifference
                                FROM    DS_Batch a
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
        /// 查询药品库存信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>库存信息表</returns>
        public DataTable LoadDrugStorages(Dictionary<string, string> condition)
        {
            string strSql = @"SELECT  0 ck,a.StorageID ,
                                        a.DrugID ,
		                                b.TradeName,
                                        c.ChemName ,
                                        c.Spec ,
                                        d.ProductName ,
                                        a.Amount AS BaseAmount,
		                                CEILING(a.Amount/(case when (a.UnitAmount is null or a.UnitAmount=0 ) then 1 else a.UnitAmount END) ) AS PackAmount,
                                        a.Place ,
                                        a.LastStockPrice ,
                                        a.RegTime ,
                                        a.UpperLimit ,
                                        a.LowerLimit ,
                                        f.BatchNO,
                                        f.ValidityTime,
                                        a.UnitID ,
                                        c.PackUnit AS UnitName ,
                                        c.MiniUnit,
                                        a.UnitAmount ,
                                        a.DelFlag ,
                                        a.DeptID ,
                                        dbo.fnGetPackAmount(a.Amount,a.UnitAmount,a.UnitName,a.PackUnit) as NewAmount,
                                        case when (a.ResolveFlag=1) then '已拆零' else '未拆零' end as ResolveFlag ,
										CASE WHEN (f.UnitAmount>0) THEN SUM(f.StockPrice*f.BatchAmount/f.UnitAmount) ELSE 0 END as StockFee,
                                        CASE WHEN (f.UnitAmount>0) THEN SUM(f.RetailPrice*f.BatchAmount/f.UnitAmount) ELSE 0 END as RetailFee,
                                        CASE WHEN (f.BatchAmount>0) THEN SUM(f.StockPrice*f.BatchAmount)/SUM(f.BatchAmount) ELSE 0 END as StockPrice,
                                        CASE WHEN (f.BatchAmount>0) THEN SUM(f.RetailPrice*f.BatchAmount)/SUM(f.BatchAmount) ELSE 0 END as RetailPrice,
                                        ISNULL(e.LocationName,'') as LocationID
                                FROM    DS_Storage a
                                        INNER JOIN DG_HospMakerDic b ON a.DrugID = b.DrugID
                                        INNER JOIN DG_CenterSpecDic c ON c.CenteDrugID = b.CenteDrugID
                                        INNER JOIN DG_ProductDic d ON d.ProductID = b.ProductID
                                        Left JOIN DG_Location e ON a.LocationID=e.LocationID
                                        Left JOIN DS_Batch f ON a.StorageID=f.StorageID
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

            string strGroupBy = " group by a.StorageID,a.DrugID,b.TradeName,c.ChemName,c.Spec,d.ProductName,a.Amount,a.UnitAmount,a.Place,a.LastStockPrice,a.RegTime,a.UpperLimit,a.LowerLimit,a.UnitID,a.PackUnit,c.PackUnit,c.MiniUnit,a.DelFlag,a.DeptID,a.ResolveFlag,f.BatchNO,f.ValidityTime,e.LocationName,a.UnitName,f.UnitAmount,f.BatchAmount";
            return oleDb.GetDataTable(strSql + strWhere + strGroupBy.ToString());
        }

        /// <summary>
        /// 查询拆零库存信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>库存信息表</returns>
        public DataTable LoadResolve(Dictionary<string, string> condition)
        {
            string strSql = @"SELECT  0 ck,a.StorageID ,
                                        a.DrugID ,
		                                b.TradeName,
                                        c.ChemName ,
                                        c.Spec ,
                                        d.ProductName ,
                                        a.Amount AS BaseAmount,
		                                CEILING(a.Amount/(case when (a.UnitAmount is null or a.UnitAmount=0 ) then 1 else a.UnitAmount END) ) AS PackAmount,
                                        a.Place ,
                                        a.LastStockPrice ,
                                        a.RegTime ,
                                        a.UpperLimit ,
                                        a.LowerLimit ,
                                        a.UnitID ,
                                        c.PackUnit AS UnitName ,
                                        c.MiniUnit,
                                        a.UnitAmount ,
                                        a.DelFlag ,
                                        a.DeptID ,
                                        dbo.fnGetPackAmount(a.Amount,a.UnitAmount,a.UnitName,a.PackUnit) as NewAmount,
                                        case when (a.ResolveFlag=1) then '已拆零' else '未拆零' end as ResolveFlag ,
                                        ISNULL(e.LocationName,'') as LocationID
                                FROM    DS_Storage a
                                        INNER JOIN DG_HospMakerDic b ON a.DrugID = b.DrugID
                                        INNER JOIN DG_CenterSpecDic c ON c.CenteDrugID = b.CenteDrugID
                                        INNER JOIN DG_ProductDic d ON d.ProductID = b.ProductID
                                        Left JOIN DG_Location e ON a.LocationID=e.LocationID
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

            string strGroupBy = " group by a.StorageID,a.DrugID,b.TradeName,c.ChemName,c.Spec,d.ProductName,a.Amount,a.UnitAmount,a.Place,a.LastStockPrice,a.RegTime,a.UpperLimit,a.LowerLimit,a.UnitID,a.PackUnit,c.PackUnit,c.MiniUnit,a.DelFlag,a.DeptID,a.ResolveFlag,e.LocationName,a.UnitName";
            return oleDb.GetDataTable(strSql + strWhere + strGroupBy.ToString());
        }

        /// <summary>
        /// 获取批次
        /// </summary>
        /// <param name="batchNO">批次号</param>
        /// <param name="drugID">药品ID</param>
        /// <returns>批次</returns>
        public List<DS_Batch> GetBatchList(string batchNO, int drugID)
        {
            string strSql = @"SELECT * FROM DS_Batch WHERE BatchNO='{0}' AND DrugID={1}";
            return oleDb.Query<DS_Batch>(string.Format(strSql, batchNO, drugID), string.Empty).ToList();
        }

        /// <summary>
        /// 获取批次药品类型
        /// </summary>
        /// <param name="batchNO">批次号</param>
        /// <param name="drugID">药品ID</param>
        /// <returns>批次药品类型</returns>
        public int GetTypeId(string batchNO, int drugID)
        {
            string strSql = @"SELECT c.CTypeID FROM DS_Batch a 
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


        /// <summary>
        /// 修改有效库存判断是否有未发药的,如果存在未发药的情况则不让修改
        /// </summary>
        /// <param name="deptId">当前执行科室</param>
        /// <param name="DrugID">厂家ID</param>
        /// <returns>返回是否存在数据</returns>
        public bool UpdateValidateStorage(int deptId, int DrugID)
        {
            try
            {
                string strSql = @"SELECT a.FeeItemHeadID AS HeadID  
                                  FROM OP_FeeItemHead AS a 
                                  INNER JOIN OP_FeeItemDetail AS b 
                                  ON a.FeeItemHeadID=b.FeeItemHeadID 
                                  WHERE a.ChargeStatus = 0
		                                AND a.ChargeFlag=1
                                        AND a.RegFlag = 0		
		                                AND a.WorkID={0}
		                                AND a.ExecDeptID={1}
		                                AND a.distributeFlag=0 
		                                AND b.ItemID={2} 
                                 UNION   
                                 SELECT  a.BillHeadID AS HeadID FROM IP_DrugBillHead AS a WHERE 
								  a.WorkID={0} AND EXISTS 
                                 (SELECT * FROM IP_DrugBillDetail w WHERE w.BillHeadID=a.BillHeadID
                                  AND w.DispDrugFlag=0  AND w.NoDrugFlag=0 AND w.Amount<>0 
								  AND w.ItemID={2}) AND a.ExecDeptID={1} ";
                DataTable dt = oleDb.GetDataTable(string.Format(strSql, oleDb.WorkId, deptId, DrugID));

                if (dt.Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    //允许修改的直接将有效库存改为实际库存
                    strSql = @"UPDATE DS_ValidStorage SET ValidAmount=a.Amount FROM DS_Storage AS a 
                               WHERE a.DrugID=DS_ValidStorage.DrugID AND a.DrugID={0}";
                    int result = oleDb.DoCommand(string.Format(strSql, DrugID));
                    return result>0? true : false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region 库存盘点
        /// <summary>
        /// 获取库房盘点状态
        /// </summary>
        /// <param name="deptId">库房Id</param>
        /// <returns>盘点状态</returns>
        public int GetStoreRoomStatus(int deptId)
        {
            int status = 0;
            DataTable dt = NewObject<DG_DeptDic>().gettable("DeptID=" + deptId + " and DeptType=0 and WorkID=" + oleDb.WorkId);
            if (dt != null && dt.Rows.Count > 0)
            {
                status = Convert.ToInt32(dt.Rows[0]["CheckStatus"]);
            }

            return status;
        }

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
                               FROM DS_CheckHead
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
                               a.StorageID,
                               a.Place as Place1,
                               h.LocationName as Place,
                               a.DeptID,
                               a.CTypeID,
                               a.DrugID,
                               '' as BatchNO,
                               a.BillNO,
                               a.FactAmount,
                               a.FactStockFee,
                               a.FactRetailFee,
                               a.ActAmount,
                               a.ActStockFee,
                               a.ActRetailFee,
                               a.UnitID,
                               a.PackUnit,
                               a.UnitAmount,
                               a.UnitName,
                               a.AuditFlag,
                               a.BillTime,
                               a.RetailPrice,
                               a.StockPrice,
                               a.CheckHeadID,
                               c.ChemName ,
                               c.Spec ,
                               d.ProductName,
							   f.DosageName,
							   g.TypeName,
							   dbo.fnGetPackAmount(a.ActAmount,a.UnitAmount,a.UnitName,a.PackUnit) as ActAmountShow,
							   dbo.fnGetPackAmount(a.FactAmount,a.UnitAmount,a.UnitName,a.PackUnit) as FactAmountShow,
                               CAST(a.ActAmount AS int)/CAST(a.UnitAmount AS INT) AS ActPackNum,
							   CAST(a.ActAmount AS int)%CAST(a.UnitAmount AS INT) AS ActBaseNum,
                               CAST(a.FactAmount AS int)/CAST(a.UnitAmount AS INT) AS FactPackNum,
							   CAST(a.FactAmount AS int)%CAST(a.UnitAmount AS INT) AS FactBaseNum
                            FROM DS_CheckDetail a
                                INNER JOIN DG_HospMakerDic b ON a.DrugID = b.DrugID
                                INNER JOIN DG_CenterSpecDic c ON c.CenteDrugID = b.CenteDrugID
                                LEFT JOIN DG_DosageDic f ON c.DosageID=f.DosageID
										LEFT JOIN DG_TypeDic  g ON c.TypeID=g.TypeID
                                INNER JOIN DG_ProductDic d ON d.ProductID = b.ProductID
                                LEFT JOIN DS_Storage as e ON a.DrugID=e.DrugID 
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
                                        d.DeptID,
		                                d.StorageID,
		                                d.Place,
		                                d.Place as Place1,
                                        h.LocationName as Place,
		                                d.Amount,
                                        d.UnitAmount,
										f.DosageName,
										g.TypeName,
                                       dbo.fnGetDrugInPrice(b.DrugID) as StockPrice ,
                                       dbo.fnGetDrugPrice(b.DrugID,d.DeptID) as RetailPrice,
									    dbo.fnGetFee(d.Amount,d.UnitAmount,dbo.fnGetDrugInPrice(b.DrugID)) AS ActStockFee,
										dbo.fnGetFee(d.Amount,d.UnitAmount,dbo.fnGetDrugPrice(b.DrugID,d.DeptID)) AS ActRetailFee,
                                        CAST(d.Amount AS int)/CAST(d.UnitAmount AS INT) AS PackNum,
										CAST(d.Amount AS int)%CAST(d.UnitAmount AS INT) AS BaseNum 
                                FROM    DG_CenterSpecDic a
                                        LEFT JOIN DG_HospMakerDic b ON a.CenteDrugID = b.CenteDrugID
                                        LEFT JOIN DG_ProductDic c ON b.ProductID = c.ProductID
                                        LEFT JOIN DG_DosageDic f ON a.DosageID=f.DosageID
										LEFT JOIN DG_TypeDic  g ON a.TypeID=g.TypeID
                                        RIGHT JOIN DS_Storage d ON b.DrugID = d.DrugID 
                                        LEFT JOIN DG_Location h ON d.LocationID=h.LocationID 
                                WHERE   a.IsStop = 0
                                        AND b.IsStop = 0
                                        AND d.DeptID = {0} and d.WorkID={1} 
                                        ORDER By h.LocationName asc ";
            return oleDb.GetDataTable(string.Format(strSql, deptID.ToString(), oleDb.WorkId));
        }

        /// <summary>
        /// 提取库存药品
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>盘点单空表数据</returns>
        public DataTable LoadStorageData(Dictionary<string, string> condition)
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
                                        d.DeptID,
		                                d.StorageID,
		                                d.Place as Place1,
                                        h.LocationName as Place,
		                                d.Amount,
										d.UnitAmount,
										f.DosageName,
										g.TypeName,
                                       dbo.fnGetDrugInPrice(b.DrugID) as StockPrice ,
                                       dbo.fnGetDrugPrice(b.DrugID,d.DeptID) as RetailPrice,
									    dbo.fnGetFee(d.Amount,a.PackAmount,dbo.fnGetDrugInPrice(b.DrugID)) AS ActStockFee,
										dbo.fnGetFee(d.Amount,a.PackAmount,dbo.fnGetDrugPrice(b.DrugID,d.DeptID)) AS ActRetailFee,
										CAST(d.Amount AS int)/CAST(a.PackAmount AS INT) AS PackNum,
										CAST(d.Amount AS int)%CAST(a.PackAmount AS INT) AS BaseNum
                                FROM    DG_CenterSpecDic a
                                        LEFT JOIN DG_HospMakerDic b ON a.CenteDrugID = b.CenteDrugID
                                        LEFT JOIN DG_ProductDic c ON b.ProductID = c.ProductID
                                        LEFT JOIN DG_DosageDic f ON a.DosageID=f.DosageID
										LEFT JOIN DG_TypeDic  g ON a.TypeID=g.TypeID
                                        RIGHT JOIN DS_Storage d ON b.DrugID = d.DrugID 
                                        LEFT JOIN DG_Location h ON d.LocationID=h.LocationID 
                                WHERE   a.IsStop = 0
                                        AND b.IsStop = 0 and d.WorkId={0} ";
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

            strWhere.AppendFormat(" order by h.LocationName asc ");
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
                                FROM DS_AuditHead
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
                                        a.StorageID ,
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
                                        a.PackUnit ,
                                        a.UnitAmount ,
                                        a.UnitName ,
                                        a.UnitName  as PACKUNITNAME,
                                        a.RetailPrice ,
                                        a.StockPrice ,
                                        a.AuditHeadID ,
                                        c.ChemName ,
                                        c.TypeID,
                                        c.Spec ,
                                        d.ProductName,
                                        (a.FactRetailFee - a.ActRetailFee)  as DIFFFEE,
                                        (a.FactStockFee - a.ActStockFee) as DIFFTRADEFEE,
                                         a.ActAmount as BASENUM,
                                         c.MiniUnit,
                                         a.FactAmount as CBASENUM,
                                         m.TypeName,
                                         (a.FactAmount - a.ActAmount) as DiffNum,
                                        dbo.fnGetPackAmount(a.ActAmount,a.UnitAmount,a.UnitName,a.PackUnit) as ActAmountShow,
							            dbo.fnGetPackAmount(a.FactAmount,a.UnitAmount,a.UnitName,a.PackUnit) as FactAmountShow
                                FROM    DS_AuditDetail a
                                        LEFT JOIN DG_HospMakerDic b ON a.DrugID = b.DrugID
                                        LEFT JOIN DG_CenterSpecDic c ON c.CenteDrugID = b.CenteDrugID
                                        LEFT JOIN DG_TypeDic m ON c.TypeID=m.TypeID 
                                        LEFT JOIN DG_ProductDic d ON b.ProductID = d.ProductID
                                 where 1=1 ";
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

            strSql = strSql + strWhere.ToString();
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

            strSql = @"SELECT                                
                            a.StorageID,
                            a.Place,
                            a.DeptID,
                            a.CTypeID,
                            a.DrugID,
                            SUM(a.FactAmount) AS FactAmount,
                            0.00 AS FactStockFee,
                            0.00 AS FactRetailFee,
                            a.ActAmount,
                            0.00 AS ActStockFee,
                            0.00 AS ActRetailFee,
                            a.UnitID,
                            a.UnitName,
							a.PackUnit,
							a.UnitAmount,
                            a.RetailPrice,
                            a.StockPrice,
                            c.ChemName ,
                            c.Spec ,
                            d.ProductName,
                            f.DosageName,
                            g.TypeName,
                             dbo.fnGetPackAmount(a.ActAmount,a.UnitAmount,a.UnitName,a.PackUnit) as ActAmountShow,
							   dbo.fnGetPackAmount(SUM(a.FactAmount),a.UnitAmount,a.UnitName,a.PackUnit) as FactAmountShow
                            FROM dbo.DS_CheckHead h LEFT JOIN DS_CheckDetail a ON h.CheckHeadID=a.CheckHeadID
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

            string strGroupBy = @"  GROUP BY 
                                                a.StorageID, a.Place,a.DeptID,a.CTypeID,a.DrugID,
                                                a.UnitID,a.UnitName,a.RetailPrice,a.StockPrice,
                                                c.ChemName ,c.Spec ,d.ProductName,
                                                f.DosageName, g.TypeName,a.ActAmount,
                                                a.PackUnit,	a.UnitAmount";
            strSql = strSql + strWhere.ToString() + strGroupBy;
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 计算批次账存金额
        /// </summary>
        /// <param name="storageId">库存Id</param>
        /// <returns>返回零售金额，进货金额数组</returns>
        public decimal[] CalculateBatchActFee(int storageId)
        {
            decimal[] arrRtn = new decimal[2];
            string strSql = @"SELECT  ISNULL(SUM(a.ActRetailFee),0) AS ActRetailFee ,
                                      ISNULL(SUM(a.ActStockFee),0) AS ActStockFee
                                    FROM    ( SELECT    StorageID ,
                                                        dbo.fnGetFee(BatchAmount, UnitAmount, RetailPrice) AS ActRetailFee ,
                                                        dbo.fnGetFee(BatchAmount, UnitAmount, StockPrice) AS ActStockFee
                                              FROM      DS_Batch
                                              WHERE     DelFlag = 0
                                                        AND StorageID = {0}
                                            ) AS a ";
            strSql = string.Format(strSql, storageId);
            DataTable dt = oleDb.GetDataTable(strSql);
            if (dt == null || dt.Rows.Count <= 0)
            {
                arrRtn[0] = 0;
                arrRtn[1] = 1;
            }
            else
            {
                arrRtn[0] = Convert.ToDecimal(dt.Rows[0]["ActRetailFee"]);
                arrRtn[1] = Convert.ToDecimal(dt.Rows[0]["ActStockFee"]);
            }

            return arrRtn;
        }

        /// <summary>
        /// 取得库存批次数据
        /// </summary>
        /// <param name="storageId">库存Id</param>
        /// <returns>库存批次数据</returns>
        public DataTable GetStorageBatch(int storageId)
        {
            string strSql = @"SELECT    a.StorageID ,
                                        a.DrugID ,
                                        a.BatchNO ,
                                        a.BatchAmount ,
                                        a.RetailPrice ,
                                        a.StockPrice ,
                                        b.UnitAmount ,
                                        a.InstoreTime,
                                        a.ValidityTime
                                FROM    DS_Batch a ,
                                        DS_Storage b
                                WHERE   a.StorageID = b.StorageID
                                        AND a.DelFlag = 0 AND a.StorageID={0}
                                ORDER BY a.InstoreTime";
            strSql = string.Format(strSql, storageId);
            DataTable dt = oleDb.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 更新盘点头表审核状态信息
        /// </summary>
        /// <param name="head">盘点头表</param>
        /// <returns>小于0失败</returns>
        public int UpdateCheckHeadStatus(DS_CheckHead head)
        {
            string strSql = @"UPDATE DS_CheckHead set AuditEmpID={0},AuditEmpName='{1}',AuditTime=GETDATE(),AuditFlag=1,AuditHeadID={2},AuditNO={3} WHERE DelFlag=0 AND AuditFlag=0 AND DeptID={4} AND WorkID={5}";
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
            string strSql = @"UPDATE DS_CheckHead SET DelFlag=1 WHERE AuditFlag=0 AND DeptID={0} AND WorkID={1}";
            strSql = string.Format(strSql, deptID, oleDb.WorkId);
            int iRtn = oleDb.DoCommand(strSql);
            return iRtn;
        }
        #endregion

        #region 药房处理
        /// <summary>
        /// 获取药品库存数量
        /// </summary>
        /// <param name="drugID">药品ID</param>
        /// <param name="deptID">库房ID</param>
        /// <param name="batchNO">批次号</param>
        /// <returns>库存数量</returns>
        public decimal GetDrugStoreAmount(int drugID, int deptID, string batchNO)
        {
            string strSql = @"SELECT IsNull(BatchAmount,0) FROM DS_Batch WHERE DelFlag=0 AND DrugID={0} AND DeptID={1} AND BatchNO='{2}' AND WorkID={3}";
            strSql = string.Format(strSql, drugID, deptID, batchNO, oleDb.WorkId);
            return Convert.ToDecimal(oleDb.GetDataResult(strSql));
        }

        /// <summary>
        /// 清空库位
        /// </summary>
        /// <param name="locationID">库位ID</param>
        public void ClearLoaction(int locationID)
        {
            string strSql = @"UPDATE DS_Storage SET Place='' ,LocationID=0 WHERE LocationID={0}";
            strSql = string.Format(strSql, locationID);
            oleDb.DoCommand(strSql);
        }

        /// <summary>
        /// 判断是否存在库存药品
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <param name="storageID">库存ID</param>
        /// <returns>是否存在</returns>
        public bool ExistStorage(int deptID, int drugID, out int storageID)
        {
            string strSql = "select StorageID from DS_Storage where DeptID={0} and DrugID={1} and WorkID={2}";
            strSql = string.Format(strSql, deptID.ToString(), drugID.ToString(), oleDb.WorkId);
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
            string strSql = @"update DS_Storage 
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
            string strSql = @"update DS_Batch set 
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
        public DS_Batch GetBatchAmount(int deptID, int drugID, string batchNO)
        {
            string strSql = @"select * from DS_Batch 
                              where DeptID={0} and DrugID={1}
                              and BatchNO='{2}'";
            strSql = string.Format(strSql, deptID, drugID, batchNO);
            return oleDb.Query<DS_Batch>(strSql, string.Empty).FirstOrDefault();
        }

        /// <summary>
        /// 获取批次库存信息
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <returns>批次库存信息</returns>
        public List<DS_Batch> GetBatchInfos(int deptID, int drugID)
        {
            string strSql = @"select * from DS_Batch 
                              where DeptID={0} and DrugID={1} and DelFlag=0 and WorkID={2} ORDER BY InstoreTime ASC ";
            strSql = string.Format(strSql, deptID, drugID, oleDb.WorkId);
            return oleDb.Query<DS_Batch>(strSql, string.Empty).ToList();
        }

        /// <summary>
        /// 获取最近入库批次库存信息
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <returns>最近入库批次库存信息</returns>
        public DS_Batch GetLatelyBatchInfo(int deptID, int drugID)
        {
            string strSql = @"select * from DS_Batch 
                              where DeptID={0} and DrugID={1} and DelFlag=0 and WorkID={2} ORDER BY InstoreTime desc ";
            strSql = string.Format(strSql, deptID, drugID, oleDb.WorkId);
            return oleDb.Query<DS_Batch>(strSql, string.Empty).FirstOrDefault();
        }

        /// <summary>
        /// 获取当前库存数量
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <returns>药品库存数量</returns>
        public decimal? GetStoreAmount(int deptID, int drugID)
        {
            string strSql = "select Amount from DS_Storage where DeptID={0} and DrugID={1}";
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

        /// <summary>
        /// 更新批次数量
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <param name="batchNO">批次号</param>
        /// <param name="amount">数量</param>
        /// <param name="delFlag">删除标志，当库存为0时候为1否则为0</param>
        /// <param name="isValidity">有效标志</param>
        /// <returns>返回状态</returns>
        public bool UpdateBatchAmount(int deptID, int drugID, string batchNO, decimal amount, int delFlag, int isValidity)
        {
            //更新库存
            string strSql = @"update DS_Batch set 
                           DelFlag={4},
                           IsValidity={5},
                           BatchAmount=BatchAmount+({0})
                           where DeptID={1} and DrugID={2} 
                           and BatchNO='{3}'";
            strSql = string.Format(strSql, amount, deptID, drugID, batchNO, delFlag, isValidity);
            int rtn = oleDb.DoCommand(strSql);
            return rtn > 0 ? true : false;
        }

        /// <summary>
        /// 更新退药明细表状态
        /// </summary>
        /// <param name="reDetailId">ID</param>
        /// <returns>返回结果</returns>
        public bool UpdateFeeRefundStatus(int reDetailId)
        {
            string strSql = @"UPDATE  OP_FeeRefundDetail SET RefundFlag=1 WHERE ReDetailID={0} and WorkID={1}";
            strSql = string.Format(strSql, reDetailId, oleDb.WorkId);
            int rtn = oleDb.DoCommand(strSql);
            return rtn > 0 ? true : false;
        }

        /// <summary>
        /// 更新有效库存
        /// </summary>
        /// <param name="drugID">药品ID</param>
        /// <param name="deptID">库房ID</param>
        /// <param name="amount">数量</param>
        /// <returns>库存处理结果</returns>
        public int UpdateValidStore(int drugID, int deptID, decimal amount)
        {
            string strSql = @"UPDATE DS_ValidStorage SET ValidAmount=ValidAmount + {0} WHERE DeptID={1} AND DrugID={2}";
            strSql = string.Format(strSql, amount, deptID, drugID);
            return oleDb.DoCommand(strSql);
        }
        #endregion

        #region 台账处理
        /// <summary>
        /// 获取最后一次结账记录
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>最后一次结账记录</returns>
        public DS_BalanceRecord GetMaxBlanceRecord(int deptID)
        {
            string strSql = @"SELECT TOP 1 * FROM DS_BalanceRecord WHERE DeptID = {0}
                            ORDER BY BalanceID DESC";
            return oleDb.Query<DS_BalanceRecord>(string.Format(strSql, deptID), string.Empty).FirstOrDefault();
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
            string strSql = @"SELECT [BalanceID],[BalanceYear],[BalanceMonth],BeginTime=Convert(varchar(20),[BeginTime],120),EndTime=Convert(varchar(20),[EndTime],120),[RegEmpID],[RegEmpName],[RegTime],[DeptID],[WorkID] FROM DS_BalanceRecord WHERE DeptID = {0} ORDER BY BalanceID DESC";
            return oleDb.GetDataTable(string.Format(strSql, deptId));
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
                IDbCommand cmd = oleDb.GetProcCommand("SP_DS_CheckAccount");
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
                IDbCommand cmd = oleDb.GetProcCommand("SP_DS_MonthBalance");
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
        #endregion

        #region 药库药房对象
        /// <summary>
        ///  出库转入库
        /// </summary>
        /// <param name="outHeadId">出库头表ID</param>
        /// <returns>入库对象集</returns>
        public List<DS_InStoreDetail> GetInStoreFromOutStore(int outHeadId)
        {
            StringBuilder stb = new StringBuilder();

            // t.Amount* dc.PackAmount AS Amount,
            stb.AppendFormat(
                    @"SELECT  dc.PackUnit ,
                                    t.DrugID,
                                    t.BatchNO,
                                    t.ValidityDate,
                                    dc.PackAmount PackAmount,
                                    dc.PackUnitID PackUnitID,
                                    dc.PackUnit PackUnit,
                                    dc.MiniUnitID AS UnitID,
                                    dc.MiniUnit AS UnitName,
                                    dc.PackAmount as UnitAmount,
                                    t.Amount * dc.PackAmount AS Amount,
                                    t.StockPrice,
                                    t.RetailPrice,
                                    t.StockFee,
                                    t.RetailFee,
                                    t.ToDeptID DeptID,
                                    t.CTypeID
                            FROM    dbo.DW_OutStoreDetail t
                                    INNER JOIN dbo.DG_HospMakerDic hd ON t.DrugID = hd.DrugID
                                    INNER JOIN dbo.DG_CenterSpecDic dc ON dc.CenteDrugID = hd.CenteDrugID
                            WHERE   t.OutHeadID = {0}",
                    outHeadId);
            return oleDb.Query<DS_InStoreDetail>(stb.ToString(), null).ToList();
        }

        /// <summary>
        /// 药库出库单 转药房出库单
        /// </summary>
        /// <param name="outHeadId">出库单ID</param>
        /// <returns>药房出库单</returns>
        public List<DS_OutStoreDetail> GetOutStoreFromOutStore(int outHeadId)
        {
            StringBuilder stb = new StringBuilder();

            //t.Amount* dc.PackAmount AS Amount,
            stb.AppendFormat(
                @"SELECT  dc.PackUnit ,
                                    t.DrugID,
                                    t.BatchNO,
                                    t.ValidityDate,
                                    dc.PackAmount UnitAmount,
                                    dc.PackUnitID PackUnit,
                                    dc.MiniUnitID AS UnitID,
                                    dc.MiniUnit AS UnitName,
                                    t.Amount AS Amount,
                                    t.StockPrice,
                                    t.RetailPrice,
                                    t.StockFee,
                                    t.RetailFee,
                                    t.ToDeptID DeptID,
                                    t.CTypeID
                            FROM    dbo.DW_OutStoreDetail t
                                    INNER JOIN dbo.DG_HospMakerDic hd ON t.DrugID = hd.DrugID
                                    INNER JOIN dbo.DG_CenterSpecDic dc ON dc.CenteDrugID = hd.CenteDrugID
                            WHERE   t.OutHeadID = {0}", outHeadId);
            return oleDb.Query<DS_OutStoreDetail>(stb.ToString(), null).ToList();
        }

        /// <summary>
        /// 获取库存信息
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="drugId">药品ID</param>
        /// <returns>库存信息</returns>
        public DS_Storage GetStorageInfo(int deptId, int drugId)
        {
            string strSql = @"select * from DS_Storage 
                              where DeptID={0} and DrugID={1}";
            strSql = string.Format(strSql, deptId, drugId);
            return oleDb.Query<DS_Storage>(strSql, string.Empty).FirstOrDefault();
        }
        #endregion

        #region 住院发药
        /// <summary>
        /// 获取药品统领单类型
        /// </summary>
        /// <returns>药品统领单类型</returns>
        public DataTable GetIPDrugBillType()
        {
            string strSql = @"SELECT -1 AS BillTypeID,'全部' AS BillTypeName, -1 AS SortOrder
                              UNION ALL
                              SELECT BillTypeID,BillTypeName,SortOrder FROM IP_DrugBillType WHERE WorkID={0}
                              ORDER BY SortOrder";
            strSql = string.Format(strSql, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取住院发药统计
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>住院发药统计</returns>
        public DataTable GetIPDispTotal(Dictionary<string, string> condition)
        {
            string strSql = @"SELECT a.DeptID,b.DrugID,b.ChemName,b.DrugSpec,b.ProductName,SUM(b.DispAmount) AS DispAmount,b.UnitAmount,b.UnitName,SUM(b.UseAmount) as UseAmount,SUM(b.StockPrice) as StockPrice,SUM(b.RetailPrice) as RetailPrice,(SUM(b.RetailPrice)-SUM(b.StockPrice)) as Price,(SUM(b.RetailPrice)/SUM(b.UnitAmount)) as AveRetail,(SUM(b.StockPrice)/SUM(b.UnitAmount)) as AveStock  FROM DS_IPDispHead a INNER JOIN DS_IPDispDetail b ON a.DispHeadID=b.DispHeadID WHERE a.WorkID={0}  AND b.DispAmount>0 ";
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

            strSql = string.Format(strSql + strWhere.ToString(), oleDb.WorkId);
            strSql += " group by b.DrugID,b.ChemName,b.DrugSpec,b.ProductName,b.UnitAmount,b.UnitName,a.DeptID";
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取门诊发药统计
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>门诊发药统计</returns>
        public DataTable GetOPDispTotal(Dictionary<string, string> condition)
        {
            string strSql = @"SELECT a.PresDeptID,b.DrugID,b.ChemName,b.DrugSpec,b.ProductName,SUM(b.DispAmount) AS DispAmount,b.UnitAmount,b.UnitName,SUM(b.UseAmount) as UseAmount,SUM(b.StockPrice) as StockPrice,SUM(b.RetailPrice) as RetailPrice,(SUM(b.RetailPrice)-SUM(b.StockPrice)) as Price,(SUM(b.RetailPrice)/SUM(b.UnitAmount)) as AveRetail,(SUM(b.StockPrice)/SUM(b.UnitAmount)) as AveStock  FROM DS_OPDispHead a INNER JOIN DS_OPDispDetail b ON a.DispHeadID=b.DispHeadID WHERE a.WorkID={0}  AND b.DispAmount>0 ";
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

            strSql = string.Format(strSql + strWhere.ToString(), oleDb.WorkId);
            strSql += " group by b.DrugID,b.ChemName,b.DrugSpec,b.ProductName,b.UnitAmount,b.UnitName,a.PresDeptID";
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取住院发药统计详细
        /// </summary>
        ///<param name="deptId">科室ID</param>
        ///<param name="drugID">药品ID</param>
        /// <returns>住院发药统计详细</returns>
        public DataTable GetIPDispDetatil(string deptId, string drugID)
        {
            string strSql = @"select *,(RetailPrice/UnitAmount) as AveRetail,(StockPrice/UnitAmount) as AveStock,(RetailPrice-StockPrice) as Price,(select BaseDept.Name from BaseDept where BaseDept.DeptId={0}) as DeptName from [DS_IPDispDetail] where DrugID={1}  AND DispAmount>0";
            return oleDb.GetDataTable(string.Format(strSql, deptId, drugID));
        }

        /// <summary>
        /// 获取门诊发药统计详细
        /// </summary>
        ///<param name="deptId">科室ID</param>
        ///<param name="drugID">药品ID</param>
        /// <returns>门诊发药统计详细</returns>
        public DataTable GetOPDispDetatil(string deptId, string drugID)
        {
            string strSql = @"select *,(RetailPrice/UnitAmount) as AveRetail,(StockPrice/UnitAmount) as AveStock,(RetailPrice-StockPrice) as Price,(select BaseDept.Name from BaseDept where BaseDept.DeptId={0}) as DeptName from [DS_OPDispDetail] where DrugID={1} AND DispAmount>0";
            return oleDb.GetDataTable(string.Format(strSql, deptId, drugID));
        }
        #endregion

        #region 报表统计
        #region 进销存统计报表
        /// <summary>
        /// 获取会计年
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>会计年记录集</returns>
        public DataTable GetAcountYears(int deptId)
        {
            DataTable dt = new DataTable();
            DS_BalanceRecord record = NewDao<Dao.IDSDao>().GetMaxBlanceRecord(deptId);
            if (record != null)
            {
                string strSql = @"SELECT DISTINCT BalanceYear AS ID,BalanceYear AS Name FROM DS_Account WHERE DeptID={0} ORDER BY BalanceYear DESC";
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
            string strSql = @"SELECT DISTINCT BalanceMonth AS ID,BalanceMonth AS Name FROM DS_Account WHERE DeptID={0} AND BalanceYear={1} ORDER BY BalanceMonth";
            strSql = string.Format(strSql, deptId, year);
            dt = oleDb.GetDataTable(strSql);
            //if (dt != null && dt.Rows.Count == 0)
            //{
            //    DataRow row = dt.NewRow();
            //    row["ID"] = DateTime.Now.Month;
            //    row["Name"] = DateTime.Now.Month;
            //    dt.Rows.InsertAt(row, 0);
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
        /// <returns>取得明细账数据集</returns>
        public DataTable GetAccountData(int accountType, int deptId, int queryYear, int queryMonth, int typeId)
        {
            string strSql = @"SELECT   a.LendRetailFee ,
                                        a.LendStockFee ,
                                        a.DebitRetailFee ,
                                        a.DebitStockFee ,
                                        a.OverRetailFee ,
                                        a.OverStockFee
                                FROM    DS_Account a LEFT JOIN DG_HospMakerDic m on a.DrugID=m.DrugID 
                                        LEFT join DG_CenterSpecDic b on b.CenteDrugID=m.CenteDrugID
                                WHERE a.AccountType={0} AND a.BalanceYear={1} AND a.BalanceMonth={2} AND a.DeptID={3} AND b.TypeID={4}";
            string flag = "0";
            if (accountType == 2)
            {
                //判断是否月结
                string sql = "SELECT DISTINCT BalanceFlag FROM DS_Account WHERE DeptID={0} AND BalanceYear={1} AND BalanceMonth={2}";
                sql = string.Format(sql, deptId, queryYear, queryMonth);
                DataTable dtBalanceFlag = oleDb.GetDataTable(sql);
                if (dtBalanceFlag.Rows.Count > 0)
                {
                    flag = dtBalanceFlag.Rows[0][0].ToString();
                    if (flag == "0")
                    {
                        //计算
                        strSql = @"SELECT ROUND(a.BatchAmount*(a.RetailPrice/a.UnitAmount),4) AS OverRetailFee ,
                                           ROUND(a.BatchAmount*(a.StockPrice/a.UnitAmount),4) as OverStockFee
                                    FROM    DS_Batch a  
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
            string strSql = string.Empty;
            if (accountType == 0)
            {
                strSql = @"SELECT   a.LendRetailFee ,
                                        a.LendStockFee ,
                                        a.DebitRetailFee ,
                                        a.DebitStockFee ,
                                        a.OverRetailFee ,
                                        a.OverStockFee
                                FROM    DS_Account a LEFT JOIN DG_HospMakerDic m on a.DrugID=m.DrugID 
                                        LEFT join DG_CenterSpecDic b on b.CenteDrugID=m.CenteDrugID
                                WHERE a.AccountType in(0,3) and {0}={0} AND a.BalanceYear={1} AND a.BalanceMonth={2} AND a.DeptID={3} AND b.TypeID={4} and a.BusiType='{5}'";

                strSql = string.Format(strSql, accountType, queryYear, queryMonth, deptId, typeId, busiCode);
            }
            else
            {
                strSql = @"SELECT   a.LendRetailFee ,
                                        a.LendStockFee ,
                                        a.DebitRetailFee ,
                                        a.DebitStockFee ,
                                        a.OverRetailFee ,
                                        a.OverStockFee
                                FROM    DS_Account a LEFT JOIN DG_HospMakerDic m on a.DrugID=m.DrugID 
                                        LEFT join DG_CenterSpecDic b on b.CenteDrugID=m.CenteDrugID
                                WHERE a.AccountType={0} AND a.BalanceYear={1} AND a.BalanceMonth={2} AND a.DeptID={3} AND b.TypeID={4} and a.BusiType='{5}'";

                strSql = string.Format(strSql, accountType, queryYear, queryMonth, deptId, typeId, busiCode);
            }

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
            string strSql = string.Empty;
            if (accountType == 0)
            {
                strSql = @"SELECT   a.LendRetailFee ,
                                        a.LendStockFee ,
                                        a.DebitRetailFee ,
                                        a.DebitStockFee ,
                                        a.OverRetailFee ,
                                        a.OverStockFee,
                               " + busiTypeModel.DeptFieldName + @"
                                FROM    DS_Account a  LEFT JOIN DG_HospMakerDic m on a.DrugID=m.DrugID 
                                        LEFT join DG_CenterSpecDic b on b.CenteDrugID=m.CenteDrugID
                                        left join " + busiTypeModel.DetailTableName + " on a.DetailID=" + busiTypeModel.DetailIdFieldName + @"
                                        left join " + busiTypeModel.HeadTableName + " on " + busiTypeModel.JoinExpress + @"
                                WHERE a.AccountType in(0,3) and {0}={0} AND a.BalanceYear={1} AND a.BalanceMonth={2} AND a.DeptID={3} AND b.TypeID={4} and a.BusiType='{5}'";

                strSql = string.Format(strSql, accountType, queryYear, queryMonth, deptId, typeId, busiCode);
            }
            else
            {
                strSql = @"SELECT   a.LendRetailFee ,
                                        a.LendStockFee ,
                                        a.DebitRetailFee ,
                                        a.DebitStockFee ,
                                        a.OverRetailFee ,
                                        a.OverStockFee,
                               " + busiTypeModel.DeptFieldName + @"
                                FROM    DS_Account a LEFT JOIN DG_HospMakerDic m on a.DrugID=m.DrugID 
                                        LEFT join DG_CenterSpecDic b on b.CenteDrugID=m.CenteDrugID
                                        left join " + busiTypeModel.DetailTableName + " on a.DetailID=" + busiTypeModel.DetailIdFieldName + @"
                                        left join " + busiTypeModel.HeadTableName + " on " + busiTypeModel.JoinExpress + @"
                                WHERE a.AccountType={0} AND a.BalanceYear={1} AND a.BalanceMonth={2} AND a.DeptID={3} AND b.TypeID={4} and a.BusiType='{5}'";

                strSql = string.Format(strSql, accountType, queryYear, queryMonth, deptId, typeId, busiCode);
            }

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
			  b.SupplierName,a.DrugID,b.BusiType,b.BillTime,a.DeptID,b.SupplierID,a.WorkID from DS_InStoreDetail a LEFT JOIN DS_InStoreHead b ON a.InHeadID=b.InHeadID  WHERE b.DelFlag=0  Group By   b.SupplierName,a.DrugID,b.BusiType,b.BillTime,a.DeptID,b.SupplierID,a.StockPrice,a.RetailPrice,a.WorkID) a   
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
			  b.ToDeptName,a.DrugID,b.BusiType,b.BillTime,a.DeptID,b.ToDeptID,a.WorkID from DS_OutStoreDetail a LEFT JOIN DS_OutStoreHead b ON a.OutHeadID=b.OutStoreHeadID WHERE b.DelFlag=0  Group By  b.ToDeptName,a.DrugID,b.BusiType,b.BillTime,a.DeptID,a.StockPrice,a.RetailPrice,b.ToDeptID,a.WorkID) a   
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
			  a.DrugID,b.BusiType,b.AuditTime,a.DeptID,a.WorkID from DS_AuditDetail a LEFT JOIN DS_AuditHead b on a.AuditHeadID=b.AuditHeadID   WHERE b.DelFlag=0  Group By   a.DrugID,b.BusiType,b.AuditTime,a.StockPrice,a.RetailPrice,a.DeptID,a.WorkID) a 
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
			  from DG_AdjDetail a LEFT JOIN DG_AdjHead b on a.AdjHeadID=b.AdjHeadID LEFT JOIN DS_Batch f ON a.BatchNO=f.BatchNO WHERE b.DelFlag=0  Group By   b.BusiType,a.DeptID,a.WorkID,b.ExecTime,f.StockPrice,a.NewRetailPrice,a.DrugID) a
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

        /// <summary>
        /// 获取住院发药信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>住院发药信息</returns>
        public DataTable GetIPDisp(Dictionary<string, string> query)
        {
            string strSql = @"SELECT a.DrugID,a.StockPrice as StockPrice,a.StockFee as StockFee,a.RetailPrice as RetailPrice,a.RetailFee as RetailFee,a.UnitAmount as UnitAmount,d.MiniUnit as UnitName,'' as DeptName,c.TradeName,d.ChemName,d.Spec,e.ProductName,a.Price as Price,f.BusiTypeName 
              FROM (select a.StockPrice as StockPrice,sum(a.StockFee) as StockFee,a.RetailPrice as RetailPrice,sum(a.RetailFee) as RetailFee,sum(a.DispAmount) as UnitAmount,sum(a.RetailFee-a.StockFee) as Price,
			  a.DrugID,b.BusiType,b.DispTime,a.DeptID,a.WorkID from DS_IPDispDetail a LEFT JOIN DS_IPDispHead b on a.DispHeadID=b.DispHeadID Group By  a.DrugID,b.BusiType,b.DispTime,a.DeptID,a.StockPrice,a.RetailPrice,a.WorkID) a  
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
        /// 获取门诊发药信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>门诊发药信息</returns>
        public DataTable GetOPDisp(Dictionary<string, string> query)
        {
            string strSql = @"SELECT a.DrugID,a.StockPrice as StockPrice,a.StockFee as StockFee,a.RetailPrice as RetailPrice,a.RetailFee as RetailFee,a.UnitAmount as UnitAmount,d.MiniUnit as UnitName,'' as DeptName,c.TradeName,d.ChemName,d.Spec,e.ProductName,a.Price as Price,f.BusiTypeName 
              FROM (select sum(a.StockPrice) as StockPrice,sum(a.StockFee) as StockFee,sum(a.RetailPrice) as RetailPrice,sum(a.RetailFee) as RetailFee,sum(a.DispAmount) as UnitAmount,sum(a.RetailFee-a.StockFee) as Price,
			  a.DrugID,b.BusiType,b.DispTime,a.DeptID,a.WorkID from DS_OPDispDetail a LEFT JOIN DS_OPDispHead b on a.DispHeadID=b.DispHeadID Group By   a.DrugID,b.BusiType,b.DispTime,a.DeptID,a.WorkID) a
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
            string strSql = @"SELECT BeginTime,EndTime FROM DS_BalanceRecord WHERE DeptID={0} AND BalanceYear={1} AND BalanceMonth={2}";
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
                    FROM    DS_Account a
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
                    FROM    DS_Account a
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
            string strSql = @"SELECT b.BillTime,a.BillNo,a.DrugID,a.StockPrice,a.StockFee,a.RetailPrice,a.RetailFee,a.Amount as UnitAmount,d.MiniUnit as UnitName,a.DrugID,b.SupplierName as DeptName,c.TradeName,d.ChemName,d.Spec,e.ProductName,(a.RetailFee-a.StockFee) as Price,f.BusiTypeName FROM DS_InStoreDetail a 
              LEFT JOIN DS_InStoreHead b ON a.InHeadID=b.InHeadID 
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
            string strSql = @"SELECT b.BillTime,a.BillNo,a.DrugID,a.StockPrice,a.StockFee,a.RetailPrice,a.RetailFee,a.Amount as UnitAmount,d.MiniUnit as UnitName,a.DrugID,b.ToDeptName as DeptName,c.TradeName,d.ChemName,d.Spec,e.ProductName,(a.RetailFee-a.StockFee) as Price,f.BusiTypeName FROM DS_OutStoreDetail a 
              LEFT JOIN DS_OutStoreHead b on a.OutHeadID=b.OutStoreHeadID 
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
            string strSql = @"SELECT b.AuditTime as BillTime,a.BillNo,a.DrugID,a.StockPrice,(a.FactStockFee-a.ActStockFee) as StockFee,a.RetailPrice,(a.FactRetailFee-a.ActRetailFee) as RetailFee,(a.FactAmount-a.ActAmount) as UnitAmount,d.MiniUnit as UnitName,a.DrugID,'' as DeptName,c.TradeName,d.ChemName,d.Spec,e.ProductName,((a.FactRetailFee-a.ActRetailFee)-(a.FactStockFee-a.ActStockFee)) as Price,f.BusiTypeName FROM DS_AuditDetail a 
              LEFT JOIN DS_AuditHead b on a.AuditHeadID=b.AuditHeadID 
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
            string strSql = @"SELECT b.ExecTime as BillTime,a.BillNo,a.DrugID,f.StockPrice,(f.StockPrice*a.AdjAmount) as StockFee,a.NewRetailPrice,a.AdjRetailFee,a.AdjAmount,d.MiniUnit as UnitName,a.DrugID,'' as DeptName,c.TradeName,d.ChemName,d.Spec,e.ProductName,(a.AdjRetailFee-(f.StockPrice*a.AdjAmount)) as Price,g.BusiTypeName FROM DG_AdjDetail a 
              LEFT JOIN DG_AdjHead b on a.AdjHeadID=b.AdjHeadID 
              LEFT JOIN DG_HospMakerDic c ON a.DrugID=c.DrugID 
              LEFT JOIN DG_CenterSpecDic d ON c.CenteDrugID=d.CenteDrugID
              LEFT JOIN DG_ProductDic e ON c.ProductID=e.ProductID
              LEFT JOIN DS_Batch f ON a.BatchNO=f.BatchNO
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

            strWhere.Append(" order by b.ExecTime  DESC");
            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 获取住院发药信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>住院发药信息</returns>
        public DataTable GetIPDisps(Dictionary<string, string> query)
        {
            string strSql = @"SELECT b.DispTime as BillTime,a.InpatientNO as BillNo,a.DrugID,a.StockPrice,a.StockFee,a.RetailPrice,a.RetailFee,a.DispAmount as UnitAmount,d.MiniUnit as UnitName,a.DrugID,'' as DeptName,c.TradeName,d.ChemName,d.Spec,e.ProductName,(a.RetailFee-a.StockFee) as Price,f.BusiTypeName FROM DS_IPDispDetail a 
              LEFT JOIN DS_IPDispHead b on a.DispHeadID=b.DispHeadID
              LEFT JOIN DG_HospMakerDic c ON a.DrugID=c.DrugID 
              LEFT JOIN DG_CenterSpecDic d ON c.CenteDrugID=d.CenteDrugID
              LEFT JOIN DG_ProductDic e ON c.ProductID=e.ProductID
              LEFT JOIN DG_BusiType f ON b.BusiType=f.BusiCode
             WHERE a.WorkID = {0}";
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

            strWhere.Append(" order by b.DispTime  DESC");
            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 获取门诊发药信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>门诊发药信息</returns>
        public DataTable GetOPDisps(Dictionary<string, string> query)
        {
            string strSql = @"SELECT b.DispTime as BillTime,b.FeeNo as BillNo,a.DrugID,a.StockPrice,a.StockFee,a.RetailPrice,a.RetailFee,a.DispAmount as UnitAmount,d.MiniUnit as UnitName,a.DrugID,'' as DeptName,c.TradeName,d.ChemName,d.Spec,e.ProductName,(a.RetailFee-a.StockFee) as Price,f.BusiTypeName FROM DS_OPDispDetail a 
              LEFT JOIN DS_OPDispHead b on a.DispHeadID=b.DispHeadID 
              LEFT JOIN DG_HospMakerDic c ON a.DrugID=c.DrugID 
              LEFT JOIN DG_CenterSpecDic d ON c.CenteDrugID=d.CenteDrugID
              LEFT JOIN DG_ProductDic e ON c.ProductID=e.ProductID
              LEFT JOIN DG_BusiType f ON b.BusiType=f.BusiCode
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

            strWhere.Append(" order by b.DispTime  DESC");
            return oleDb.GetDataTable(strSql + strWhere.ToString());
        }

        /// <summary>
        /// 修改库存禁用状态
        /// </summary>
        /// <param name="storageId">库存ID</param>
        /// <param name="delFlag">禁用标识</param>
        /// <returns></returns>
        public bool UpdateStorageFlag(int storageId,int delFlag)
        {
            try
            {
                string strSql = string.Format(@"UPDATE DS_Storage SET DelFlag=CASE {0} WHEN 1 THEN 0 ELSE 1 END WHERE StorageID={1}", delFlag, storageId);
                oleDb.DoCommand(strSql);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
        #endregion
    }
}
