using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.MIManage;
using HIS_MIInterface.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_MIInterface.WcfController
{
    [WCFController]
    public class CommitTradeStateController : WcfServerController
    {
        [WCFMethod]
        public ServiceResponseData Mz_GetTradeInfoByCon()
        {
            string sSerialNO = requestData.GetData<string>(0);
            string sInvoiceNo = requestData.GetData<string>(1);
            string sTradeNo = requestData.GetData<string>(2);

            DataTable dt = NewDao<ICommitTradeState>().Mz_GetTradeInfoByCon(sSerialNO, sInvoiceNo, sTradeNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                responseData.AddData(true);
                responseData.AddData(dt);
            }
            else
            {
                responseData.AddData(false);
            }

            return responseData;
        }


        [WCFMethod]
        public ServiceResponseData Mz_GetTradeInfoByCard()
        {
            string sCardNO = requestData.GetData<string>(0);
            DateTime Time = requestData.GetData<DateTime>(1);

            DataTable dt = NewDao<ICommitTradeState>().Mz_GetTradeInfoByCon(sCardNO, Time);
            if (dt != null && dt.Rows.Count > 0)
            {
                responseData.AddData(true);
                responseData.AddData(dt);
            }
            else
            {
                responseData.AddData(false);
            }

            return responseData;
        }

        [WCFMethod]
        public ServiceResponseData MZ_RePrintMXInvoice()
        {
            string tradeNo = requestData.GetData<string>(0);
            string invoiceNo = requestData.GetData<string>(1);

            //解析返回结果到类，保存
            MI_MedicalInsurancePayRecord medicalInsurancePayRecord = Mz_GetPayRecord(3, tradeNo, 2, 0);
            
            if (medicalInsurancePayRecord != null)
            {
                MI_MIPayRecordHead mIPayRecordHead = Mz_GetPayRecordHead(medicalInsurancePayRecord.ID);
                DataTable dtPayrecordDetail = Mz_GetPayRecordDetailForPrint(medicalInsurancePayRecord.ID);

                List<MI_MedicalInsurancePayRecord> result1 = new List<MI_MedicalInsurancePayRecord>();
                result1.Add(medicalInsurancePayRecord);
                DataTable dtPayrecord = ConvertExtend.ToDataTable(result1);

                List<MI_MIPayRecordHead> result2 = new List<MI_MIPayRecordHead>();
                result2.Add(mIPayRecordHead);
                DataTable dtPayrecordHead = ConvertExtend.ToDataTable(result2);

                responseData.AddData(true);
                responseData.AddData(dtPayrecord);
                responseData.AddData(dtPayrecordHead);
                responseData.AddData(dtPayrecordDetail);
            }
            else
            {
                responseData.AddData(false);
            }

            return responseData;
        }

        [WCFMethod]
        public ServiceResponseData MZ_ExportJzxx()
        {
            DateTime tradeDate = requestData.GetData<DateTime>(0);

            DataTable dt = NewDao<ICommitTradeState>().MZ_ExportJzxx(tradeDate);
            if (dt != null && dt.Rows.Count > 0)
            {
                responseData.AddData(true);
                responseData.AddData(dt);
            }
            else
            {
                responseData.AddData(false);
            }

            return responseData;
        }

        public MI_MedicalInsurancePayRecord Mz_GetPayRecord(int getWay, string cValue, int type, int state)
        {
            List<MI_MedicalInsurancePayRecord> medicalInsurancePayRecordList;
            switch (getWay)
            {
                case 1:
                    medicalInsurancePayRecordList = NewObject<MI_MedicalInsurancePayRecord>().getlist<MI_MedicalInsurancePayRecord>(" regId=" + cValue + " and TradeType=" + type + "  and state=" + state);
                    break;
                case 2:
                    medicalInsurancePayRecordList = NewObject<MI_MedicalInsurancePayRecord>().getlist<MI_MedicalInsurancePayRecord>(" FeeNO='" + cValue + "' and state=" + state);
                    break;
                case 3:
                    medicalInsurancePayRecordList = NewObject<MI_MedicalInsurancePayRecord>().getlist<MI_MedicalInsurancePayRecord>(" TradeNO='" + cValue + "'");
                    break;
                default:
                    medicalInsurancePayRecordList = NewObject<MI_MedicalInsurancePayRecord>().getlist<MI_MedicalInsurancePayRecord>(" Id=" + cValue + " and state=" + state);
                    break;
            }

            if (medicalInsurancePayRecordList.Count > 0)
            {
                return medicalInsurancePayRecordList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 登记号获取交易头表信息
        /// </summary>
        /// <returns></returns>
        public MI_MIPayRecordHead Mz_GetPayRecordHead(int PayRecordId)
        {
            List<MI_MIPayRecordHead> mIPayRecordHeadList = NewObject<MI_MIPayRecordHead>().getlist<MI_MIPayRecordHead>(" PayRecordId=" + PayRecordId);
            if (mIPayRecordHeadList.Count > 0)
            {
                return mIPayRecordHeadList[0];
            }
            else
            {
                return new MI_MIPayRecordHead();
            }         
        }

        /// <summary>
        /// 登记号获取交易明细表信息
        /// </summary>
        /// <returns></returns>
        public DataTable Mz_GetPayRecordDetailForPrint(int PayRecordId)
        {
            DataTable dt = NewDao<ICommitTradeState>().Mz_GetPayRecordDetailForPrint(PayRecordId);
            return dt;
        }
    }
}
