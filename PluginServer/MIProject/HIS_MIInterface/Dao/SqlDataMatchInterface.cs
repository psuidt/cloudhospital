using EFWCoreLib.CoreFrame.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_MIInterface.Dao
{
    public class SqlDataMatchInterface : AbstractDao, IDataMatchInterface
    {
        //获取医院目录
        /// <summary>
        /// 获取医院数据
        /// </summary>
        /// <param name="dataType">字典类型1.剂型2.频次3.参保人员类别4收费等级5就诊科别6门诊收据分类</param>
        /// <returns></returns>
        public DataTable M_GetHISDataInfo(int ybId, int dataType)
        {
            string strSql = "";
            switch (dataType)
            {
                case 1:
                    strSql = @"  SELECT DosageID AS HospDataID ,DosageID as HospDataCode ,DosageName AS HospDataName ,PyCode ,WbCode
                           FROM dbo.DG_DosageDic   where DelFlag=0 AND WorkID={1} 
                             AND NOT EXISTS(SELECT 1 FROM MI_DataDictionary_Match c WHERE c.HospDataID=DosageID and c.DataType=1 and  c.workid=workid and c.MIID={0}) ";
                    break;
                case 2:
                    strSql = @" SELECT FrequencyID AS HospDataID ,FrequencyName as HospDataCode ,CName AS HospDataName ,PyCode ,WbCode
                           FROM dbo.Basic_Frequency  WHERE DelFlag=0 AND WorkID={1} 
                             AND NOT EXISTS(SELECT 1 FROM MI_DataDictionary_Match c WHERE c.HospDataID=FrequencyID and c.DataType=2 and  c.workid=workid and c.MIID={0}) ";
                    break;
                case 3:
                    strSql = @" SELECT FrequencyID AS HospDataID ,FrequencyName as HospDataCode ,CName AS HospDataName ,PyCode ,WbCode
                           FROM dbo.Basic_Frequency  WHERE WorkID={1} 
                             AND NOT EXISTS(SELECT 1 FROM MI_DataDictionary_Match c WHERE c.HospDataID=FrequencyID and c.DataType=3 and  c.workid=workid and c.MIID={0}) ";
                    break;
                case 4:
                    strSql = @" SELECT FrequencyID AS HospDataID ,FrequencyName as HospDataCode ,CName AS HospDataName ,PyCode ,WbCode
                           FROM dbo.Basic_Frequency  WHERE WorkID={1}
                             AND NOT EXISTS(SELECT 1 FROM MI_DataDictionary_Match c WHERE c.HospDataID=FrequencyID and c.DataType=4 and   c.workid=workid and c.MIID={0}) ";
                    break;
                case 5:
                    strSql = @" SELECT a.DeptId AS HospDataID ,a.DeptId as HospDataCode ,a.Name AS HospDataName ,'' PyCode ,'' WbCode
                           FROM dbo.BaseDept a LEFT JOIN BaseDeptDetails b  ON a.DeptId=b.DeptID WHERE a.WorkID={1}  AND a.DelFlag=0 AND (b.OutUsed=1 OR b.InUsed=1)
                             AND NOT EXISTS(SELECT 1 FROM MI_DataDictionary_Match c WHERE c.HospDataID=a.DeptId and c.DataType=5 and  c.workid=a.workid and c.MIID={0}) ";
                    break;
                case 6:
                    strSql = @" SELECT a.StatID AS HospDataID ,a.StatID as HospDataCode ,a.StatName AS HospDataName ,'' PyCode ,'' WbCode
                           FROM dbo.Basic_StatItem a  WHERE a.WorkID={1}  AND a.DelFlag=0 AND a.BaItemID>0 AND a.OutFpItemID>0
                             AND NOT EXISTS(SELECT 1 FROM MI_DataDictionary_Match c WHERE c.HospDataID=a.StatID and c.DataType=6 and  c.workid=a.workid and c.MIID={0}) ";
                    break;
                default:
                    strSql = @"  SELECT DosageID AS HospDataID ,DosageID as HospDataCode ,DosageName AS HospDataName ,PyCode ,WbCode
                           FROM dbo.DG_DosageDic   where DelFlag=0 AND WorkID={1} 
                             AND NOT EXISTS(SELECT 1 FROM MI_DataDictionary_Match c WHERE c.HospDataID=DosageID and c.DataType=1 and  c.workid=workid and c.MIID={0}) ";
                    break;
            }

            strSql = String.Format(strSql, ybId, oleDb.WorkId);
            return oleDb.GetDataTable(strSql);
        }

        //获取医院的医保目录
        /// <summary>
        /// 获取医保数据
        /// </summary>
        /// <param name="dataType">字典类型1.剂型2.频次3.参保人员类别4收费等级5就诊科别6门诊收据分类</param>
        /// <param name="ybId">医保类型</param>
        /// <returns></returns>
        public DataTable M_GetMIDataInfo(int ybId, int dataType)
        {
            string strSql = @" SELECT id AS YBDataID,Code AS YBDataCode,Name AS YBDataName, PyCode , WbCode
                           FROM dbo.MI_DataDictionary  WHERE MIID={0}  and WorkID={1}  and DataType={2} ";
            strSql = String.Format(strSql, ybId, oleDb.WorkId, dataType);
            return oleDb.GetDataTable(strSql);
        }
        /// <summary>
        /// 获取匹配数据
        /// </summary>
        /// <param name="dataType">字典类型1.剂型2.频次3.参保人员类别4收费等级5就诊科别6门诊收据分类</param>
        /// <param name="ybId">医保类型</param>
        /// <returns></returns>
        public DataTable M_GetMatchDataInfo(int ybId, int dataType)
        {
            string strCode = "";
            string strName = "";
            string strType = "";
            switch (dataType)
            {
                case 1:
                    strCode = "c.DosageID";
                    strName = "c.DosageName";
                    strType = "INNER JOIN dbo.DG_DosageDic c on a.HospDataID=c.DosageID and a.workid=c.workid";
                    break;
                case 2:
                    strCode = "c.FrequencyName";
                    strName = "c.CName";
                    strType = "INNER JOIN dbo.Basic_Frequency c on a.HospDataID=c.FrequencyID and a.workid=c.workid";
                    break;
                case 3:
                    strCode = "c.FrequencyName";
                    strName = "c.CName";
                    strType = "INNER JOIN dbo.Basic_Frequency c on a.HospDataID=c.FrequencyID and a.workid=c.workid";
                    break;
                case 4:
                    strCode = "c.FrequencyName";
                    strName = "c.CName";
                    strType = "INNER JOIN dbo.Basic_Frequency c on a.HospDataID=c.FrequencyID and a.workid=c.workid";
                    break;
                case 5:
                    strCode = "c.DeptId";
                    strName = "c.Name";
                    strType = "INNER JOIN dbo.BaseDept c on a.HospDataID=c.DeptId and a.workid=c.workid";
                    break;
                case 6:
                    strCode = "c.StatID";
                    strName = "c.StatName";
                    strType = "INNER JOIN dbo.Basic_StatItem c on a.HospDataID=c.StatID and a.workid=c.workid";
                    break;
                default:
                    strCode = "c.FrequencyName";
                    strName = "c.CName";
                    strType = "INNER JOIN dbo.Basic_Frequency c on a.HospDataID=c.FrequencyID and a.workid=c.workid";
                    break;
              }
            string strSql = @" SELECT a.id,{0} AS HISDataCode ,{1} HISDataName ,b.code AS YBDataCode ,b.Name AS YBDataName
                           FROM dbo.MI_DataDictionary_Match  a 
                           INNER JOIN dbo.MI_DataDictionary b on a.MIID=b.MIID and a.WorkID=b.WorkID and a.MIDataID=b.id
                           {2}
                           WHERE a.MIID={3}  and a.WorkID={4}  and a.DataType={5} ";
            strSql = String.Format(strSql, strCode , strName , strType, ybId, oleDb.WorkId, dataType);
            return oleDb.GetDataTable(strSql);
        }


        //获取医保类型
        public DataTable M_GetMIType()
        {
            string strSql = @" SELECT ID,MIName,MatchMode,ZyMode
                           FROM CloudHISDB.dbo.MI_MedicalInsuranceType  WHERE VaildFlag=1 ";
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 删除匹配目录
        /// </summary>
        /// <returns></returns>
        public bool M_DeleteMatchData(int id)
        {
            string sql= " delete from dbo.MI_DataDictionary_Match where id=" + id ;


            if (oleDb.DoCommand(sql) == 0)
                return false;

            return true;
        }
        /// <summary>
        /// 保存基础数据匹配
        /// </summary>
        /// <param name="iMIDataID"></param>
        /// <param name="iDataType"></param>
        /// <param name="iHospDataID"></param>
        /// <param name="iMIID"></param>
        /// <returns></returns>
        public bool M_SaveMatchData(int iMIDataID,int iDataType,int iHospDataID,int iMIID)
        {
            string strSql = @" insert into dbo.MI_DataDictionary_Match([WorkID],[MIDataID],[DataType],[HospDataID],[MIID]) 
                                                             values({0},    {1},        {2},        {3},        {4})";
            strSql = String.Format(strSql, oleDb.WorkId, iMIDataID, iDataType, iHospDataID, iMIID);

            if (oleDb.DoCommand(strSql) == 0)
                return false;

            return true;
        }
    }
}
