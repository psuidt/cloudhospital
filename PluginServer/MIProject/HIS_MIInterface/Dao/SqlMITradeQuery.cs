using EFWCoreLib.CoreFrame.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_MIInterface.Dao
{
    public class SqlMITradeQuery : AbstractDao, IMITradeQuery
    {
        public DataTable Mz_GetTradeInfoSummary(int iMIID, int iPatientType, string sDeptCode, string sTimeStat, string sTimeStop)
        {
            DataTable dt = null;
            string sInsertRegister = @"SELECT mir.id 登记ID,mir.DeptName 科室,mir.SerialNO HIS流水号,mir.PatientName 病人姓名,mir.PersonalCode 医保卡号,mir.RegTime 登记时间,mir.SocialCreateNum 医保登记号,(case mir.ValidFlag when 1 then '挂号' when 2 then '退号' end) 状态,
                                        mipr.id 交易ID,mipr.RegId 登记ID,mipr.TradeNO 交易流水号,mipr.TradeTime 交易时间,mipr.FeeAll 交易总额,mipr.FeeFund 统筹支付,mipr.FeeCash 现金支付,mipr.PersonCountPay 个帐支付,mipr.PersonCount 个帐余额,mipr.FeeMIIn 医保内总额,mipr.FeeMIOut 医保外总额,mipr.FeeSelfPay ,mipr.FeeBigPay,mipr.FeeBigSelfPay,mipr.FeeOutOFPay,mipr.Feebcbx,mipr.Feejcbz,mipr.FeeDeductible 起付线,mipr.FeeNO 收费单据号,(case mipr.TradeType when 1 then '挂号' else '收费' end) 交易类型,(case mipr.state when 1 then '已结算' when 2 then '退费' when 3 then '被退' end ) 交易状态,
                                        miprh.medicine 报表西药,miprh.tmedicine 报表中成药,miprh.therb 报表中草药,miprh.examine 报表检查费,miprh.labexam 报表化验,miprh.treatment 报表治疗费,miprh.operation 报表手术费,miprh.material 报表材料费,miprh.other 报表其他,miprh.xray 报表放射,miprh.ultrasonic 报表B超,miprh.CT 报表CT,miprh.mri 报表核磁,miprh.oxygen 报表输氧费,miprh.bloodt 报表输血费,miprh.orthodontics 报表正畸费,miprh.prosthesis 报表镶牙费,miprh.forensic_expertise 报表司法鉴定,miprh.diagnosis 报表诊察费,miprh.medicalservice 报表医事服务费,miprh.commonservice 报表一般诊疗费,miprh.registfee 报表挂号费,
                                        miprd.PayRecordId 交易主ID,miprd.itemno 项目序号,miprd.recipeno 处方序号,miprd.hiscode HIS编码,miprd.itemcode 医保编码,miprd.itemname 项目名称,miprd.itemtype 项目类型,miprd.unitprice 单价,miprd.count 数量,miprd.fee 总金额,miprd.feein 医保内金额,miprd.feeout 医保外金额
                                        FROM dbo.MI_Register mir
                                        INNER JOIN dbo.MI_MedicalInsurancePayRecord mipr ON mipr.WorkID = mir.WorkID AND mir.id=mipr.RegId AND mipr.state>0
                                        LEFT JOIN dbo.MI_MIPayRecordHead miprh on mipr.WorkID = miprh.WorkID AND mipr.id=miprh.PayRecordId
                                        LEFT JOIN dbo.MI_MIPayRecordDetail miprd ON mipr.WorkID = miprd.WorkID AND miprd.PayRecordId=mipr.id
                                        WHERE mir.ValidFlag>0 and mir.MIID={0} and  mir.PatientType={1} and (mir.DeptName='{2}' or '全部'='{2}') and mipr.TradeTime BETWEEN '{3}' AND '{4}'
                                        order by  mir.id,mipr.RegId";
            sInsertRegister = String.Format(sInsertRegister, iMIID, iPatientType, sDeptCode, sTimeStat, sTimeStop);
            dt = oleDb.GetDataTable(sInsertRegister);
            return dt;
        }

        public DataTable Mz_GetTradeRecordInfo(int iMIID, int iPatientType, string sDeptCode, string sTimeStat, string sTimeStop)
        {
            DataTable dt = null;
            string sInsertRegister = @"SELECT mdip.TradeNO
	                                ,rg.PersonalCode
	                                ,rg.IdentityNum
	                                ,rg.PatientName
	                                ,mdip.FeeNO	  
                                    ,replace(replace(replace(CONVERT(varchar, mdip.TradeTime, 120 ),'-',''),' ',''),':','') TradeTime
	                                ,mdip.MedicalType	  
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mdip.FeeAll))  FeeAll
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mdip.FeeFund))  FeeFund
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mdip.FeeCash))  FeeCash
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mdip.PersonCountPay))  PersonCountPay
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mdip.PersonCount))  PersonCount
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mdip.FeeMIIn))  FeeMIIn
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mdip.FeeMIOut))  FeeMIOut
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mdip.FeeDeductible))  FeeDeductible	  
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mdip.FeeSelfPay))  FeeSelfPay
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mdip.FeeBigPay))  FeeBigPay
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mdip.FeeBigSelfPay))  FeeBigSelfPay
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mdip.FeeOutOFPay))  FeeOutOFPay
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mdip.Feebcbx))  Feebcbx
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mdip.Feejcbz))  Feejcbz
	  
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mprh.medicine)) medicine
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mprh.tmedicine)) tmedicine
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mprh.therb)) therb
	                                ,CONVERT(varchar,CONVERT(decimal(18,2),mprh.labexam)) labexam
	                                ,CONVERT(varchar,CONVERT(decimal(18,2),mprh.xray)) xray
	                                ,CONVERT(varchar,CONVERT(decimal(18,2),mprh.ultrasonic)) ultrasonic
	                                ,CONVERT(varchar,CONVERT(decimal(18,2),mprh.CT)) CT
	                                ,CONVERT(varchar,CONVERT(decimal(18,2),mprh.mri)) mri
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mprh.examine)) examine      
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mprh.treatment)) treatment
	                                ,CONVERT(varchar,CONVERT(decimal(18,2),mprh.material)) material
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mprh.operation)) operation
	                                ,CONVERT(varchar,CONVERT(decimal(18,2),mprh.oxygen)) oxygen
	                                ,CONVERT(varchar,CONVERT(decimal(18,2),mprh.bloodt)) bloodt 
	                                ,CONVERT(varchar,CONVERT(decimal(18,2),mprh.orthodontics)) orthodontics
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mprh.prosthesis)) prosthesis
	                                ,CONVERT(varchar,CONVERT(decimal(18,2),mprh.forensic_expertise)) forensic_expertise
                                    ,CONVERT(varchar,CONVERT(decimal(18,2),mprh.other)) other  
                            FROM dbo.MI_MedicalInsurancePayRecord mdip
                      inner join dbo.MI_MIPayRecordHead mprh on mdip.id=mprh.PayRecordId and mdip.WorkID=mprh.WorkID
                      inner join dbo.MI_Register rg on rg.id=mdip.RegId and rg.ValidFlag>0
                      where mdip.state>0
                        and rg.MIID={0} and  rg.PatientType={1} and (rg.DeptName='{2}' or '全部'='{2}') and mdip.TradeTime BETWEEN '{3}' AND '{4}'
                        order by  mdip.TradeNO";
            sInsertRegister = String.Format(sInsertRegister, iMIID, iPatientType, sDeptCode, sTimeStat, sTimeStop);
            dt = oleDb.GetDataTable(sInsertRegister);
            return dt;
        }

        public DataTable Mz_GetTradeDetailInfo(int iMIID, int iPatientType, string sDeptCode, string sTimeStat, string sTimeStop)
        {
            DataTable dt = null;
            string sInsertRegister = @"SELECT 
                                         mprd.tradeno
                                        ,mprd.itemno  
                                        ,mprd.hiscode
                                        ,mprd.itemcode
                                        ,mprd.itemname
                                        ,mprd.itemtype
                                        ,CONVERT(varchar,CONVERT(decimal(18,4),mprd.unitprice)) unitprice
                                        ,CONVERT(varchar,CONVERT(decimal(18,2),mprd.count)) count
                                        ,CONVERT(varchar,CONVERT(decimal(18,4),mprd.fee)) fee
                                        ,CONVERT(varchar,CONVERT(decimal(18,4),mprd.feein)) feein
                                        ,CONVERT(varchar,CONVERT(decimal(18,4),mprd.feeout)) feeout
                                        ,CONVERT(varchar,CONVERT(decimal(18,4),mprd.selfpay2)) selfpay2
                                        ,mprd.state 
                                        ,mprd.feetype
                                        ,CONVERT(varchar,CONVERT(decimal(18,4),mprd.preferentialfee)) preferentialfee
                                        ,mprd.preferentialscale
	                                    -- ,recipeno
                                    FROM dbo.MI_MIPayRecordDetail mprd
                              INNER JOIN dbo.MI_MedicalInsurancePayRecord mdip on mprd.PayRecordId=mdip.id and mdip.state>0
                              inner join dbo.MI_Register rg on rg.id=mdip.RegId and rg.ValidFlag>0
                                        WHERE rg.MIID={0} and  rg.PatientType={1} and (rg.DeptName='{2}' or '全部'='{2}') and mdip.TradeTime BETWEEN '{3}' AND '{4}'
                                        order by  mprd.tradeno";
            sInsertRegister = String.Format(sInsertRegister, iMIID, iPatientType, sDeptCode, sTimeStat, sTimeStop);
            dt = oleDb.GetDataTable(sInsertRegister);
            return dt;
        }
    }
}
