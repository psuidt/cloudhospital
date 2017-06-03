using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using System.Data;
using HIS_MIInterface.Dao;
using HIS_Entity.MIManage;
using HIS_MIInterface.ObjectModel;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.Common;
using System.Drawing;

namespace HIS_MIInterface.WcfController
{
    [WCFController]
    public class MIMzController : WcfServerController
    {
        #region 医保接口用
        /// <summary>
        /// ID或者门诊号/住院号获取登记信息
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData Mz_Getregister()
        {
            int regId = requestData.GetData<int>(0);
            string serialNO = requestData.GetData<string>(1);
            int workId = WorkId;
            if (regId > 0)
            {
                MI_Register register = NewObject<MI_Register>().getmodel(regId) as MI_Register;
                responseData.AddData(true);
                responseData.AddData(register);
            }
            else
            {
                List<MI_Register> registerList = NewObject<MI_Register>().getlist<MI_Register>(" SerialNO='" + serialNO + "' and WorkId=" + workId + " and ValidFlag=1");
                if (registerList.Count > 0)
                {
                    MI_Register register = registerList[0];
                    responseData.AddData(true);
                    responseData.AddData(register);
                }
                else
                {
                    responseData.AddData(false);
                }
            }

            return responseData;
        }

        /// <summary>
        /// 保存登记信息，并返回该信息
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData Mz_SaveRegister()
        {
            try
            {
                MI_Register register = requestData.GetData<MI_Register>(0);
                this.BindDb(register);
                register.save();
                responseData.AddData(true);
                responseData.AddData(register);
            }
            catch (Exception e)
            {
                responseData.AddData(false);
                responseData.AddData(e.Message);
            }
            return responseData;
        }

        /// <summary>
        /// 增加交易记录
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveTradeInfo()
        {
            try
            {
                MI_MedicalInsurancePayRecord medicalInsurancePayRecord = requestData.GetData<MI_MedicalInsurancePayRecord>(0);
                MI_MIPayRecordHead mIPayRecordHead = requestData.GetData<MI_MIPayRecordHead>(1);
                List<MI_MIPayRecordDetail> mIPayRecordDetailList = requestData.GetData<List<MI_MIPayRecordDetail>>(2);

                MI_MedicalInsurancePayRecord medicalInsurancePayRecordResult = NewObject<TradeProcess>().SaveTradeInfo(medicalInsurancePayRecord, mIPayRecordHead, mIPayRecordDetailList);

                responseData.AddData(true);
                responseData.AddData(medicalInsurancePayRecordResult);
            }
            catch (Exception e)
            {
                responseData.AddData(false);
                responseData.AddData(e.Message);
            }
            return responseData;
        }

        [WCFMethod]
        public ServiceResponseData Mz_GetPayRecord()
        {
            int getWay = requestData.GetData<int>(0);
            string cValue = requestData.GetData<string>(1);
            int type = requestData.GetData<int>(2);
            int state = requestData.GetData<int>(3);
            List<MI_MedicalInsurancePayRecord> medicalInsurancePayRecordList;
            switch (getWay)
            {
                case 1:
                    medicalInsurancePayRecordList = NewObject<MI_MedicalInsurancePayRecord>().getlist<MI_MedicalInsurancePayRecord>(" regId=" + cValue + " and TradeType="+type+"  and state=" + state);
                    break;
                case 2:
                    medicalInsurancePayRecordList = NewObject<MI_MedicalInsurancePayRecord>().getlist<MI_MedicalInsurancePayRecord>(" FeeNO='" + cValue + "' and state=" + state);
                    break;
                case 3:
                    medicalInsurancePayRecordList = NewObject<MI_MedicalInsurancePayRecord>().getlist<MI_MedicalInsurancePayRecord>(" TradeNO='" + cValue + "' and state=" + state);
                    break;
                default:
                    medicalInsurancePayRecordList = NewObject<MI_MedicalInsurancePayRecord>().getlist<MI_MedicalInsurancePayRecord>(" Id=" + cValue + " and state=" + state);
                    break;
            }

            if (medicalInsurancePayRecordList.Count > 0)
            {
                MI_MedicalInsurancePayRecord medicalInsurancePayRecord = medicalInsurancePayRecordList[0];
                responseData.AddData(true);
                responseData.AddData(medicalInsurancePayRecord);
            }
            else
            {
                responseData.AddData(false);
            }
            return responseData;
        }

        /// <summary>
        /// 增加和修改交易表
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData Mz_SavePayRecord()
        {
            try
            {
                MI_MedicalInsurancePayRecord medicalInsurancePayRecord = requestData.GetData<MI_MedicalInsurancePayRecord>(0);
                this.BindDb(medicalInsurancePayRecord);
                medicalInsurancePayRecord.save();

                responseData.AddData(true);
                responseData.AddData(medicalInsurancePayRecord);
            }
            catch (Exception e)
            {
                responseData.AddData(false);
                responseData.AddData(e.Message);
            }
            return responseData;
        }
        [WCFMethod]
        public ServiceResponseData Mz_JudgeRegister()
        {
            int RegId = requestData.GetData<int>(0);
            List<MI_MedicalInsurancePayRecord> medicalInsurancePayRecordList = NewObject<MI_MedicalInsurancePayRecord>().getlist<MI_MedicalInsurancePayRecord>(" RegId='" + RegId + "' and state=1 and TradeType=2");
            if (medicalInsurancePayRecordList.Count > 0)
            {
                responseData.AddData(false);
                responseData.AddData("存在未退费的医保已结算数据！");
            }
            else
            {
                responseData.AddData(true);
            }
            return responseData;
        }

        /// <summary>
        /// 登记号获取交易头表信息
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData Mz_GetPayRecordHead()
        {
            int PayRecordId = requestData.GetData<int>(0);

            List<MI_MIPayRecordHead> mIPayRecordHeadList = NewObject<MI_MIPayRecordHead>().getlist<MI_MIPayRecordHead>(" PayRecordId=" + PayRecordId );
            if (mIPayRecordHeadList.Count > 0)
            {
                MI_MIPayRecordHead mIPayRecordHead = mIPayRecordHeadList[0];
                responseData.AddData(true);
                responseData.AddData(mIPayRecordHead);
            }
            else
            {
                responseData.AddData(false);
            }

            return responseData;
        }

        /// <summary>
        /// 登记号获取交易明细表信息
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData Mz_GetPayRecordDetail()
        {
            int PayRecordId = requestData.GetData<int>(0);

            List<MI_MIPayRecordDetail> mIPayRecordDetailList = NewObject<MI_MIPayRecordDetail>().getlist<MI_MIPayRecordDetail>(" PayRecordId=" + PayRecordId);
            if (mIPayRecordDetailList.Count > 0)
            {
                responseData.AddData(true);
                responseData.AddData(mIPayRecordDetailList);
            }
            else
            {
                responseData.AddData(false);
            }

            return responseData;
        }
        [WCFMethod]
        public ServiceResponseData Mz_GetPayRecordDetailForPrint()
        {
            int PayRecordId = requestData.GetData<int>(0);

            DataTable dt= NewDao<IMZInterface>().Mz_GetPayRecordDetailForPrint(PayRecordId);
            if (dt!=null && dt.Rows.Count>0)
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
        public ServiceResponseData Mz_GetMITypeInfo()
        {
            string MedicalClass = requestData.GetData<string>(0);

            List<MI_MedicalInsuranceType> medicalInsuranceTypeList = NewObject<MI_MedicalInsuranceType>().getlist<MI_MedicalInsuranceType>(" PatTypeID='" + MedicalClass+"'");
            if (medicalInsuranceTypeList.Count > 0)
            {
                MI_MedicalInsuranceType medicalInsuranceType = medicalInsuranceTypeList[0];
                responseData.AddData(true);
                responseData.AddData(medicalInsuranceType);
            }
            else
            {
                responseData.AddData(false);
            }

            return responseData;
        }

        [WCFMethod]
        public ServiceResponseData MZ_GetMIDataMatch()
        {
            //string miid = requestData.GetData<string>(0);

            DataTable dt = NewDao<IMZInterface>().MZ_GetMIDataMatch();
            responseData.AddData(dt);           

            return responseData;
        }
        #endregion

        [WCFMethod]
        public ServiceResponseData MZ_ClearData()
        {
            bool b= NewDao<IMZInterface>().MZ_ClearData();
            responseData.AddData(b);
            return responseData;
        }
        private void AddLog(string s)
        {
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, s);
        }
    }
}
