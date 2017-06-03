using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_MIInterface.Dao
{
    public class SqlMatchInterface : AbstractDao, IMatchInterface
    {
        //获取医院目录
        /// <summary>
        /// 获取医院目录
        /// </summary>
        /// <param name="catalogType">1：药品，2：材料，3：收费项目,4:中草药</param>
        /// <returns></returns>
        public DataTable M_GetHISCatalogInfo(int catalogType, int stopFlag, int matchFlag, int ybID)
        {
            StringBuilder strSql = new StringBuilder();
            if (catalogType == 3)
            {
                strSql.Append("SELECT a.ItemID, a.ItemCode, a.ItemName, '' AS Standard,'' Dosage, b.Unit AS UnPickUnit, a.Price AS SellPrice,c.MedicareName AS ItemClass, a.StatID,'本院' FacName,a.PyCode AS PyCode, a.WbCode AS WbCode,a.MedicareItemCode");
                strSql.Append(" FROM  Basic_HospFeeItem  a INNER JOIN  Basic_CenterFeeItem  b ON a.CenterItemID = b.FeeID LEFT join DG_MedicareDic c on a.MedicateID=c.MedicareID");
                strSql.Append("  where  a.workid=" + oleDb.WorkId + "");
                strSql.Append(stopFlag == 1 ? " And a.IsStop = 0 AND b.IsStop = 0 " : "");
                strSql.Append(matchFlag == 1 ? "  AND NOT EXISTS(SELECT 1 FROM MI_Match_HIS c WHERE c.ItemCode=a.ItemID and  c.workid=a.workid and c.MIID=" + ybID + ")" : "");
            }
            else if (catalogType == 1)
            {
                strSql.Append("SELECT a.DrugID ItemID, b.CenterDrugCode ItemCode, b.ChemName ItemName, b.Spec AS Standard,b.DosageID Dosage, b.DoseUnit AS UnPickUnit, a.RetailPrice AS SellPrice,c.MedicareName AS ItemClass, '' StatID,pd.ProductName FacName,b.PyCode AS PyCode,b.WBCode AS WbCode,a.MedicareItemCode");
                strSql.Append(" FROM DG_HospMakerDic a INNER JOIN DG_CenterSpecDic b ON a.CenteDrugID=b.CenteDrugID  LEFT join DG_MedicareDic c on a.MedicareID=c.MedicareID");
                strSql.Append(" left join  DG_ProductDic  pd  ON a.ProductID = pd.ProductID ");
                strSql.Append(" where a.workid=" + oleDb.WorkId + " AND b.TypeID<>3 ");  //AND b.TypeID=3 
                strSql.Append(stopFlag == 1 ? " And a.IsStop = 0 AND b.IsStop = 0 " : "");
                strSql.Append(matchFlag == 1 ? "  AND NOT EXISTS(SELECT 1 FROM MI_Match_HIS c WHERE c.ItemCode=a.DrugID  and c.workid=a.workid and c.MIID=" + ybID + ")" : "");
            }
            else if(catalogType == 2)
            {
                strSql.Append("SELECT a.MaterialID ItemID, a.MatCode ItemCode, a.MatName ItemName, b.Spec AS Standard,'' Dosage, b.UnitName AS UnPickUnit, a.RetailPrice AS SellPrice,c.MedicareName AS ItemClass, '' StatID,pd.ProductName FacName,b.PyCode AS PyCode,b.WBCode AS WbCode ,a.MedicareItemCode");
                strSql.Append(" FROM MW_HospMakerDic a INNER JOIN MW_CenterSpecDic b ON a.CenterMatID=b.CenterMatID  LEFT join DG_MedicareDic c on a.MedicareID=c.MedicareID");
                strSql.Append(" left join  DG_ProductDic  pd  ON a.ProductID = pd.ProductID ");
                strSql.Append("  where  a.workid=" + oleDb.WorkId + " ");
                strSql.Append(stopFlag == 1 ? " And a.IsStop = 0 AND b.IsStop = 0 " : "");
                strSql.Append(matchFlag == 1 ? "  AND NOT EXISTS(SELECT 1 FROM MI_Match_HIS c WHERE c.ItemCode=a.MaterialID and c.workid=a.workid and c.MIID=" + ybID + ")" : "");
            }
            else
            {
                strSql.Append("SELECT a.DrugID ItemID, b.CenterDrugCode ItemCode, b.ChemName ItemName, '中药饮片及药材' AS Standard,b.DosageID Dosage, b.DoseUnit AS UnPickUnit, a.RetailPrice AS SellPrice,c.MedicareName AS ItemClass, '' StatID,pd.ProductName FacName,b.PyCode AS PyCode,b.WBCode AS WbCode,a.MedicareItemCode");
                strSql.Append(" FROM DG_HospMakerDic a INNER JOIN DG_CenterSpecDic b ON a.CenteDrugID=b.CenteDrugID  LEFT join DG_MedicareDic c on a.MedicareID=c.MedicareID");
                strSql.Append(" left join  DG_ProductDic  pd  ON a.ProductID = pd.ProductID ");
                strSql.Append(" where a.workid=" + oleDb.WorkId + " AND b.TypeID=3 ");  // 
                strSql.Append(stopFlag == 1 ? " And a.IsStop = 0 AND b.IsStop = 0 " : "");
                strSql.Append(matchFlag == 1 ? "  AND NOT EXISTS(SELECT 1 FROM MI_Match_HIS c WHERE c.ItemCode=a.DrugID  and c.workid=a.workid and c.MIID=" + ybID + ")" : "");
            }
            return oleDb.GetDataTable(strSql.ToString());
        }

        //获取医院的医保目录
        /// <summary>
        /// 获取医院目录
        /// </summary>
        /// <param name="catalogType">1：药品，2：材料，3：收费项目</param>
        /// <param name="ybId">医保类型</param>
        /// <returns></returns>
        public DataTable M_GetMICatalogInfo(int catalogType, int ybId)
        {
            string strSql = @" SELECT ItemCode ,ItemName ,ItemType ,Spec ,Unit,Price ,Dosage ,ItemLevel  ,Factory ,PYCode  ,WBCode  ,IsYBFlag 
                           FROM dbo.MI_Catalog  WHERE MIID={0}  and WorkID={1}  and ItemType={2} ";
            strSql = String.Format(strSql, ybId, oleDb.WorkId, catalogType);
            return oleDb.GetDataTable(strSql);
        }

        //获取医院的审核目录
        /// <summary>
        /// 获取医院目录
        /// </summary>
        /// <param name="catalogType">1：药品，2：材料，3：收费项目 0:全部</param>
        /// <param name="ybId">医保类型</param>
        /// <returns></returns>
        public DataTable M_GetMatchCatalogInfo(int catalogType, int ybId, int auditFlag)
        {
            string sAuditFlag = "全部";
            switch (auditFlag)
            {
                case -1:
                    sAuditFlag = "全部";
                    break;
                case 0:
                    sAuditFlag = "已匹配";
                    break;
                case 1:
                    sAuditFlag = "已审核";
                    break;
                case 2:
                    sAuditFlag = "未通过";
                    break;

            }
            string strSql = @" SELECT a.id,a.WorkID,a.MIID,a.ItemCode,a.HospItemName,a.Unit YBUnit,a.Dosage YBDosage,a.Spec YBSpec,a.Price YBPrice,a.YBItemCode,a.YBItemName,a.YBItemLevel,
                                a.AuditFlag,a.AuditDate,a.PYCode,a.WBCode
                                FROM   MI_Match_HIS a
                                LEFT JOIN MI_Catalog b ON a.YBItemCode=b.ItemCode AND a.MIID=b.MIID AND a.WorkID=b.WorkID
                               WHERE a.MIID={0}  and a.WorkID={1} and (a.AuditFlag='{3}' or '全部'='{3}') ";
            strSql = String.Format(strSql, ybId, oleDb.WorkId, catalogType, sAuditFlag);
            return oleDb.GetDataTable(strSql);
        }

        //获取医保类型
        public DataTable M_GetMIType()
        {
            string strSql = @" SELECT ID,MIName,MatchMode,ZyMode
                           FROM dbo.MI_MedicalInsuranceType  WHERE VaildFlag=1 ";
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 清空匹配数据
        /// </summary>
        /// <param name="ybId"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public bool M_DeleteMatchLogs(int ybId,string itemType)
        {
            try
            {
                string sql = "delete from MI_Match_HIS where WorkID=" + oleDb.WorkId + " and MIID=" + ybId + " and ItemType=" + itemType;
                oleDb.DoCommand(sql);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 保存导入的审核目录
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ybId"></param>
        /// <returns></returns>
        public bool M_SaveMatchLogs(DataTable dt, int ybId)
        {
            string sSelect = "";

            foreach (DataRow dr in dt.Rows)
            {
                //dr["Memo"]
                if (sSelect.Equals(""))
                {
                    sSelect += string.Format(" SELECT   {0},{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}', '{9}','{10}','{11}','{12}', '{13}', '{14}', '{15}','{16}' ",
                                              oleDb.WorkId, ybId, dr["ItemCode"], dr["HospItemName"], dr["YBUnit"], dr["YBDosage"], dr["YBSpec"], "", dr["YBPrice"],
                                              dr["YBItemCode"], dr["YBItemName"], dr["YBItemLevel"], dr["YBItemType"],
                                              dr["AuditFlag"], dr["AuditDate"], "", dr["ItemType"]);
                }
                else
                {
                    sSelect += "UNION ALL";
                    sSelect += string.Format(" SELECT   {0},{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}', '{14}', '{15}','{16}' ",
                                              oleDb.WorkId, ybId, dr["ItemCode"], dr["HospItemName"], dr["YBUnit"], dr["YBDosage"], dr["YBSpec"], "", dr["YBPrice"],
                                              dr["YBItemCode"], dr["YBItemName"], dr["YBItemLevel"], dr["YBItemType"],
                                              dr["AuditFlag"], dr["AuditDate"],"", dr["ItemType"]);
                }

            }
            string sqlInsert = @" INSERT INTO MI_Match_HIS(WorkID     ,MIID      ,ItemCode      ,HospItemName,  Unit      ,Dosage      ,Spec      ,Factory      ,Price
                                                           ,YBItemCode      ,YBItemName      ,YBItemLevel      ,YBItemType      
                                                           ,AuditFlag      ,AuditDate      ,Memo,ItemType)  
                                    " + sSelect;
            if (!sSelect.Equals(""))
            {
                if (oleDb.DoCommand(sqlInsert) == 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 单条更新匹配目录（重置/删除）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="auditFlag"></param>
        /// <returns></returns>
        public bool M_UpdateMatchLogs(string id, string auditFlag)
        {
            string sql;
            if (auditFlag.Contains("0"))
            {
                sql = " delete from MI_Match_HIS where id=" + id + " and AuditFlag=" + auditFlag;
            }
            else
            {
                sql = " update  MI_Match_HIS set AuditFlag=0 where id=" + id;
            }

            if (oleDb.DoCommand(sql) == 0)
                return false;

            return true;
        }
        /// <summary>
        /// 全部重置匹配目录
        /// </summary>
        /// <param name="ybId"></param>
        /// <returns></returns>
        public bool M_UpdateAllMatchLogs(int ybId)
        {
            string sql;
            sql = " update  MI_Match_HIS set AuditFlag='未审核' where MIID=" + ybId + " and WorkID=" + oleDb.WorkId;

            try
            {
                oleDb.DoCommand(sql);
            }
            catch
            {
                return false;
            }

            return true;
        }



        /// <summary>
        /// 根据医保匹配，更新本院目录级别
        /// </summary>
        /// <param name="ybId"></param>
        /// <returns></returns>
        public bool M_UpdateDrugLogLevel(int ybId)
        {
            string sql;

            sql = " update  DG_HospMakerDic set  MedicareItemCode=ISNULL(a.YBItemCode,''), MedicareID=ISNULL(a.YBItemLevel,0) FROM MI_Match_HIS a  where DrugID=a.ItemCode AND a.ItemType = 0 AND a.MIID =" + ybId + " and a.WorkID=" + oleDb.WorkId;

            try
            {
                oleDb.DoCommand(sql);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 根据医保匹配，更新本院目录级别
        /// </summary>
        /// <param name="ybId"></param>
        /// <returns></returns>
        public bool M_UpdateFeeItemLogLevel(int ybId)
        {
            string sql;
            sql = "  update  Basic_HospFeeItem  set MedicareItemCode=ISNULL(a.YBItemCode,''), MedicateID=ISNULL(a.YBItemLevel,0) FROM MI_Match_HIS a  where ItemID = a.ItemCode AND a.ItemType = 1 AND a.MIID =" + ybId + " and a.WorkID=" + oleDb.WorkId;
            try
            {
                oleDb.DoCommand(sql);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 根据医保匹配，更新本院目录级别
        /// </summary>
        /// <param name="ybId"></param>
        /// <returns></returns>
        public bool M_UpdateMWLogLevel(int ybId)
        {
            string sql;

            sql = " update  MW_HospMakerDic set  MedicareItemCode=ISNULL(a.YBItemCode,''), MedicareID=ISNULL(a.YBItemLevel,0) FROM MI_Match_HIS a  where MaterialID=a.ItemCode AND a.ItemType = 1 AND a.MIID =" + ybId + " and a.WorkID=" + oleDb.WorkId;

            try
            {
                oleDb.DoCommand(sql);
            }
            catch
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// 清空匹配数据
        /// </summary>
        /// <param name="ybId"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public bool M_DeleteMILog(int ybId)
        {
            try
            {
                string sql = "delete from MI_Catalog where WorkID=" + oleDb.WorkId + " and MIID=" + ybId;
                oleDb.DoCommand(sql);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 保存导入的审核目录
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ybId"></param>
        /// <returns></returns>
        public bool M_SaveMILog(DataTable dt, int ybId)
        {
            string sSelect = "";

            foreach (DataRow dr in dt.Rows)
            {
                if (!sSelect.Equals(""))
                {
                    sSelect += "UNION ALL";
                }

                sSelect += string.Format(" SELECT   {0},{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}', '{9}','{10}','{11}' ",
                          oleDb.WorkId, ybId, dr["ItemType"], dr["ItemCode"], dr["ItemName"], dr["ItemLevel"], 0, dr["Unit"], dr["Dosage"], dr["Spec"], dr["Factory"], dr["Price"]);


            }
            string sqlInsert = @" INSERT INTO MI_Catalog(WorkID  ,MIID  ,ItemType  ,ItemCode  ,ItemName,ItemLevel,IsYBFlag,  Unit   ,Dosage   ,Spec   ,Factory  ,Price)  
                                    " + sSelect;
            if (!sSelect.Equals(""))
            {
                if (oleDb.DoCommand(sqlInsert) == 0)
                    return false;
            }
            return true;
        }
    }
}
