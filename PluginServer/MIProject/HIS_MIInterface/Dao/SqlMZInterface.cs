using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MIManage;

namespace HIS_MIInterface.Dao
{
    public class SqlMZInterface : AbstractDao, IMZInterface
    {
        //public bool Mz_Register(RegInfo regInfo)
        //{
        //    string sInsertRegister = @" insert into MI_Register(IdentityNum,WorkID,PatientType,PatientID,BedNo
        //                                                                    PatientName,SerialNO,SocialCreateNum,MedicalClass,
        //                                                                    StaffID,PayOffHistory,RegTime,ValidFlag)
        //                                                             values('{0}','{1}','{2}','{3}',
        //                                                                    '{4}','{5}','{6}','{7}',
        //                                                                    '{8}','{9}','{10}','{11}')  ";
        //    sInsertRegister = String.Format(sInsertRegister, regInfo.IdentityNum, regInfo.WorkID,regInfo.PatientType,regInfo.PatientID,regInfo.BedNO,
        //                                                     regInfo.PatientName,regInfo.RegisterNo,regInfo.SocialCreateNum,regInfo.MedicalClass,
        //                                                     regInfo.StaffID,"",regInfo.RegisterTime,0);
        //    return oleDb.DoCommand(sInsertRegister) > 0;
        //}
        public DataTable Mz_GetPayRecordDetailForPrint(int PayRecordId)
        {
            DataTable dt = null;
            string sInsertRegister = @"SELECT miprd.itemname ItemName,mimmh.Spec Spec,mimmh.Unit Unit,miprd.unitprice UnitPrice,miprd.count Count,miprd.fee Fee,mimmh.YBItemLevel YBItemLevel
                                        FROM dbo.MI_MIPayRecordDetail miprd
                                        LEFT JOIN dbo.MI_Match_HIS mimmh ON miprd.hiscode=mimmh.ItemCode AND miprd.WorkID=mimmh.WorkID
                                        WHERE PayRecordId={0}   ";
            sInsertRegister = String.Format(sInsertRegister, PayRecordId);
            dt= oleDb.GetDataTable(sInsertRegister);
            return dt;
        }


        public bool MZ_ClearData()
        {
            bool b = false;
            try
            {
                string sql = "delete from dbo.MI_Register ";
                oleDb.DoCommand(sql);

                sql = "delete from dbo.MI_MIPayRecordHead ";
                oleDb.DoCommand(sql);

                sql = "delete from dbo.MI_MIPayRecordDetail ";
                oleDb.DoCommand(sql);
                b = true;
            }
            catch (Exception )
            {
                b = false;
            }

            return b;

        }

        /// <summary>
        /// 获取基础数据匹配
        /// </summary>
        /// <returns></returns>
        public DataTable MZ_GetMIDataMatch()
        {
            DataTable dt = null;
            string sql = @"SELECT a.MIDataID, a.DataType, a.HospDataID,a.MIID, b.code, b.name,c.PatTypeID
                                        FROM dbo.MI_DataDictionary_Match a
                                  INNER JOIN dbo.MI_DataDictionary b ON a.midataid=b.id AND a.DataType=b.DataType AND a.workid=b.workid
                                  INNER JOIN dbo.MI_MedicalInsuranceType c ON a.MIID=c.id AND a.workid=c.workid AND c.VaildFlag=1
                                        WHERE a.WorkID={0}   ";
            sql = String.Format(sql, WorkId);
            dt = oleDb.GetDataTable(sql);
            return dt;
        }
    }
}
