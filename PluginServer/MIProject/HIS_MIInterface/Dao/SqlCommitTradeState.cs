using EFWCoreLib.CoreFrame.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_MIInterface.Dao
{
    public class SqlCommitTradeState : AbstractDao, ICommitTradeState
    {
        public DataTable Mz_GetTradeInfoByCon(string sSerialNO, string sInvoiceNo,string sTradeNo)
        {
            DataTable dt = null;
            string sInsertRegister = @"SELECT mir.id 登记ID,mir.DeptName 科室,mir.SerialNO HIS流水号,mir.PatientName 病人姓名,mir.PersonalCode 医保卡号,mir.RegTime 登记时间,mir.SocialCreateNum 医保登记号,
                                        mipr.id 交易ID,mipr.RegId 登记ID,mipr.TradeNO 交易流水号,mipr.TradeTime 交易时间,mipr.FeeAll 交易总额,mipr.FeeFund 统筹支付,mipr.FeeCash 现金支付,mipr.PersonCountPay 个帐支付,mipr.PersonCount 个帐余额,mipr.FeeMIIn 医保内总额,mipr.FeeMIOut 医保外总额,
                                            mipr.FeeSelfPay ,mipr.FeeBigPay,mipr.FeeBigSelfPay,mipr.FeeOutOFPay,mipr.Feebcbx,mipr.Feejcbz,mipr.FeeDeductible 起付线,mipr.FeeNO 收费单据号,(case mipr.TradeType when 1 then '挂号' else '收费' end) 交易类型,
                                            (case mipr.state when 1 then '已结算' when 2 then '退费' when 3 then '被退' else '挂起' end ) 交易状态
                                        FROM dbo.MI_Register mir
                                        INNER JOIN dbo.MI_MedicalInsurancePayRecord mipr ON mipr.WorkID = mir.WorkID AND mir.id=mipr.RegId 
                                        WHERE 1=1
                                            AND mir.ValidFlag>0 
                                            AND mipr.state IN(0,1,2,3)
                                            AND (mir.SerialNO='{0}' OR '{0}'='-1')
                                            AND (mipr.FeeNO='{1}' OR '{1}'='-1')
                                            AND (mipr.TradeNO='{2}' OR '{2}'='-1')
                                        order by  mir.id,mipr.RegId";
            sInsertRegister = String.Format(sInsertRegister, sSerialNO, sInvoiceNo, sTradeNo);
            dt = oleDb.GetDataTable(sInsertRegister);
            return dt;
        }


        public DataTable Mz_GetTradeInfoByCon(string sCardNo,DateTime Time)
        {
            DataTable dt = null;
            string sInsertRegister = @"SELECT mir.id 登记ID,mir.DeptName 科室,mir.SerialNO HIS流水号,mir.PatientName 病人姓名,mir.PersonalCode 医保卡号,mir.RegTime 登记时间,mir.SocialCreateNum 医保登记号,
                                        mipr.id 交易ID,mipr.RegId 登记ID,mipr.TradeNO 交易流水号,mipr.TradeTime 交易时间,mipr.FeeAll 交易总额,mipr.FeeFund 统筹支付,mipr.FeeCash 现金支付,mipr.PersonCountPay 个帐支付,mipr.PersonCount 个帐余额,mipr.FeeMIIn 医保内总额,mipr.FeeMIOut 医保外总额,
                                        mipr.FeeSelfPay ,mipr.FeeBigPay,mipr.FeeBigSelfPay,mipr.FeeOutOFPay,mipr.Feebcbx,mipr.Feejcbz,mipr.FeeDeductible 起付线,mipr.FeeNO 收费单据号,(case mipr.TradeType when 1 then '挂号' else '收费' end) 交易类型,
                                        (case mipr.state when 1 then '已结算' when 2 then '退费' when 3 then '被退' when 4 then '撤销' else '挂起' end ) 交易状态
                                        FROM dbo.MI_Register mir
                                        inner JOIN dbo.MI_MedicalInsurancePayRecord mipr ON mipr.WorkID = mir.WorkID AND mir.id=mipr.RegId AND mipr.state IN(0,1,2,3,4) AND mipr.TradeTime>'{1}' and mipr.TradeTime<'{2}'
                                        WHERE  mir.PersonalCode='{0}'
                                        order by  mir.id,mipr.RegId";
            sInsertRegister = String.Format(sInsertRegister, sCardNo, Time.ToString("yyyy-MM-dd 00:00:00"), Time.AddDays(1).ToString("yyyy-MM-dd 00:00:00"));
            dt = oleDb.GetDataTable(sInsertRegister);
            return dt;
        }


        /// <summary>
        /// 登记号获取交易明细表信息
        /// </summary>
        /// <returns></returns>
        public DataTable Mz_GetPayRecordDetailForPrint(int PayRecordId)
        {
            DataTable dt = null;
            string sInsertRegister = @"SELECT miprd.itemname ItemName,mimmh.Spec Spec,mimmh.Unit Unit,miprd.unitprice UnitPrice,miprd.count Count,miprd.fee Fee,mimmh.YBItemLevel YBItemLevel
                                        FROM dbo.MI_MIPayRecordDetail miprd
                                        LEFT JOIN dbo.MI_Match_HIS mimmh ON miprd.hiscode=mimmh.ItemCode AND miprd.WorkID=mimmh.WorkID
                                        WHERE PayRecordId={0}   ";
            sInsertRegister = String.Format(sInsertRegister, PayRecordId);
            dt = oleDb.GetDataTable(sInsertRegister);
            return dt;
        }

        public DataTable MZ_ExportJzxx(DateTime tradeDate)
        {
            DataTable dt = null;
            string sInsertRegister = @"SELECT CONVERT(varchar(8),mr.regtime,112) TradeTime,mr.SocialCreateNum tradeno,(CASE mr.ValidFlag WHEN 1 THEN 1 WHEN 2 THEN 0 END) jzflag
                                        FROM dbo.MI_Register   mr                                       
                                        WHERE mr.ValidFlag>0 and mr.regtime>='{0}' and mr.regtime<='{1}'  ";
            sInsertRegister = String.Format(sInsertRegister, tradeDate.ToString("yyyy-MM-dd 00:00:00"), tradeDate.ToString("yyyy-MM-dd 23:59:59"));
            dt = oleDb.GetDataTable(sInsertRegister);
            return dt;
        }
    }
}
