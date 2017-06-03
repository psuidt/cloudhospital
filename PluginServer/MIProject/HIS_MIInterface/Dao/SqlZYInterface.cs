using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_MIInterface.Dao
{
    public class SqlZYInterface : AbstractDao, IZYInterface
    {
        //获取医保病人
        public DataTable Zy_GetMIPatient(int iMiType, int iDeptCode)
        {
            string strSql = @" SELECT a.*,(a.TotalDespoit-a.TotalFee) YeCost
                           FROM V_MI_PatientTotalFee a where a.MIID=" + iMiType + " and EnterDeptID="+ iDeptCode;
            return oleDb.GetDataTable(strSql);
        }


        //获取病人费用
        public DataTable Zy_GetPatientInfo(int PatientId, int iFeeType)
        {
            string strSql = @"SELECT ipf.PatListID,ipf.FeeRecordID,ipf.ItemID,mimh.YBItemCode,
                                    --ipf.ItemName,
                                    (CASE ipf.StatID WHEN 102 THEN '中药饮片及药材' ELSE ipf.ItemName END) ItemName,
                                    (CASE ipf.FeeClass WHEN 1 THEN 0 ELSE (case ipf.statid when 600 then 2 else 1 end ) END ) FeeClass,
                                    --(CASE ipf.FeeClass WHEN 1 THEN '西药' WHEN 2 THEN '材料' WHEN  3 THEN '项目' END) FeeClassName ,
                                        (CASE ipf.FeeClass WHEN 1 THEN '西药' WHEN 2 THEN '材料' WHEN  3 THEN '项目' END) FeeClassName ,
                                    ipf.StatID,bss.StatName,
                                    (CASE ipf.StatID WHEN 102 THEN ipf.ItemName ELSE ipf.Spec END) Spec,
                                    --ipf.Spec,
                                    ipf.Unit,
                                    ipf.TotalFee/ipf.Amount SellPrice,
                                    --ipf.SellPrice,
                                    ipf.Amount,ipf.TotalFee, ipf.UploadID ,
                                    (case ipf.UploadID when 1 then '已上传' else '未上传'  end) UploadState,replace(replace(replace(CONVERT(varchar, ipf.PresDate, 120 ),'-',''),' ',''),':','') ChargeDate,
                                     '' Dosage,'1' IsOut,0 Memo,mir.SocialCreateNum,ISNULL(dhm.NationalCode,'') NationalCode
						 FROM  IP_FeeItemRecord ipf  
                         INNER JOIN Basic_StatItem bss ON ipf.StatID=bss.StatID
					     INNER JOIN  IP_PatList ipp ON ipf.PatListID=ipp.PatListID
						 INNER JOIN MI_MedicalInsuranceType mim ON ipp.PatTypeID=mim.PatTypeID
                         LEFT JOIN DG_HospMakerDic dhm ON ipf.ItemID=dhm.DrugID AND ipf.workid=dhm.workid 
						 Left JOIN MI_Match_HIS mimh ON mimh.MIID=mim.ID AND mimh.ItemCode=ipf.ItemID
                         LEFT JOIN MI_Register mir on mir.SerialNO=convert(varchar(20),ipp.SerialNumber) and mir.PatientID=ipp.PatListID
                        WHERE ipf.Amount<>0  and RecordFlag=0
                        and ipp.PatListID=" + PatientId+ " and (ipf.UploadID="+ iFeeType + " or -1="+ iFeeType + ")  order by ipf.FeeRecordID";
            return oleDb.GetDataTable(strSql);
        }
        /// <summary>
        /// 更新病人费用上传标志
        /// </summary>
        /// <param name="PatientId"></param>
        /// /// <param name="iFlag">0-未传 1-已传</param>
        /// <returns></returns>
        public bool Zy_UploadzyPatFee(int PatientId,int iFlag)
        {
            string strSql = @"update IP_FeeItemRecord set UploadID="+ iFlag + " where PatListID="+ PatientId;
            return oleDb.DoCommand(strSql)>0;
        }

        public bool Zy_ReSetzyPatFee(int feeRecordID)
        {
            string strSql = @"update IP_FeeItemRecord set UploadID=0 where FeeRecordID=" + feeRecordID;
            return oleDb.DoCommand(strSql) > 0;
        }


        //获取医保类型
        public DataTable M_GetMIType(string sRoute)
        {
            string strSql = @" SELECT ID,MIName,PatTypeID
                           FROM dbo.MI_MedicalInsuranceType  WHERE VaildFlag=1 and WorkId="+oleDb.WorkId+" and (Route='"+ sRoute + "' or '"+sRoute+"'='-1')";
            return oleDb.GetDataTable(strSql);
        }


        //获取医保类型
        public DataTable Mz_GetOutPatientFee(int iPatType, DateTime dDate)
        {
            string strSql = @" SELECT d.MedicareCard,d.IDNumber,d.name,d.Sex,a.CardNO,dn.DiagnosisCode, dn.DiagnosisName
                                     ,b.InvoiceNO,CASE a.RegFlag WHEN 1 THEN '挂号' ELSE '收费' END  FeeType
	                                 ,c.FeeItemHeadID , c.PresDetailID,c.ItemID,c.ItemType,c.ItemName ,g.YBItemCode,g.YBItemName,g.YBItemLevel
                                     ,'' AS Dosa,e.Spec,c.MIniUnit,c.RetailPrice,c.amount,c.PresAmount,c.TotalFee
                                     ,f1.Code ChanID,f1.NAME ChanName,e.Dosage,c.PackUnit,e.PresFactor,e.DAYS,a.CostDate
                                     ,f3.Code MICode,f3.Name MIName
                                     ,e.presDoctorid,dbo.fnGetEmpName(e.presDoctorid) DocName,b.PresDeptID,dbo.fnGetDeptName(b.PresDeptID) DeptName,f2.code MIDeptId,f2.NAME MIDeptName
                                     ,c.ExamItemId,b.ChargeEmpID,dbo.fnGetEmpName(b.ChargeEmpID) ChargeName
                               FROM dbo.OP_CostHead a 
                         INNER JOIN dbo.OP_FeeItemHead b ON a.CostHeadID=b.CostHeadID
                         INNER JOIN dbo.OP_FeeItemDetail c ON b.FeeItemHeadID=c.FeeItemHeadID
						 INNER JOIN dbo.ME_MemberInfo d ON d.MemberID=a.MemberID
                         INNER JOIN dbo.MI_Match_HIS g ON g.ItemCode=c.ItemID AND g.workid=c.workid
                         INNER JOIN (SELECT cc.PatListID, cc.DiagnosisCode, cc.DiagnosisName FROM dbo.OPD_DiagnosisRecord cc WHERE cc.SortNo=1 ) dn ON dn.PatListID=a.PatListID
						 LEFT JOIN dbo.OPD_presDetail e ON c.DocPresDetailID=e.PresDetailID
						 LEFT JOIN (SELECT bb.HospDataID,aa.Code,aa.Name FROM dbo.MI_DataDictionary aa INNER JOIN  dbo.MI_DataDictionary_Match bb ON aa.id=bb.MIDataID AND aa.DataType=2) f1 ON e.FrequencyID=f1.HospDataID 
						 LEFT JOIN (SELECT bb.HospDataID,aa.Code,aa.Name FROM dbo.MI_DataDictionary aa INNER JOIN  dbo.MI_DataDictionary_Match bb ON aa.id=bb.MIDataID AND aa.DataType=5) f2 ON b.PresDeptID=f2.HospDataID 
						 LEFT JOIN (SELECT bb.HospDataID,aa.Code,aa.Name FROM dbo.MI_DataDictionary aa INNER JOIN  dbo.MI_DataDictionary_Match bb ON aa.id=bb.MIDataID AND aa.DataType=6) f3 ON c.statID=f3.HospDataID 
                              WHERE a.coststatus IN (0,1,2) and a.WorkId={0} and a.pattypeid={1} and a.CostDate>='{2}' and a.CostDate<='{3}' ";
            strSql = string.Format(strSql, oleDb.WorkId, iPatType, dDate.ToString("yyyy-MM-dd 00:00:00"), dDate.ToString("yyyy-MM-dd 23:59:59"));
            return oleDb.GetDataTable(strSql);
        }

    }
}
