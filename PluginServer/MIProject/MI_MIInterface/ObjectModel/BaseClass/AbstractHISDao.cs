using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame;
using HIS_Entity.MIManage;

namespace MI_MIInterface.ObjectModel.BaseClass
{
    public class AbstractHISDao:AbstractDao
    {
        public virtual void BeginInitData(Hashtable param)
        {
        }

        public virtual void EndInitData()
        {

        }

        public  ServiceResponseData InvokeWcfService(string wcfpluginname, string wcfcontroller, string wcfmethod)
        {
            return InvokeWcfService(wcfpluginname, wcfcontroller, wcfmethod, null);
        }

        public  ServiceResponseData InvokeWcfService(string wcfpluginname, string wcfcontroller, string wcfmethod, Action<ClientRequestData> requestAction)
        {
            ClientLink wcfClientLink = ClientLinkManage.CreateConnection(wcfpluginname);
            //绑定LoginRight
            Action<ClientRequestData> _requestAction = ((ClientRequestData request) =>
            {
                request.LoginRight = new EFWCoreLib.CoreFrame.Business.SysLoginRight();
                request.LoginRight.WorkId = WorkId;
                if (requestAction != null)
                    requestAction(request);
            });
            ServiceResponseData retData = wcfClientLink.Request(wcfcontroller, wcfmethod, _requestAction);
            return retData;
        }

        #region 匹配界面
        /// <summary>
        /// 目录匹配信息
        /// </summary>
        /// <param name="HosID"></param>
        /// <returns></returns>
        public virtual DataTable GetMatch(int HosID)
        {
            string SQL = string.Format(@"WorkID  ,MIID   ,ItemType    ,ItemCode      ,HospItemName      ,YBItemCode      ,YBItemName      ,YBItemLevel      ,YBFlag      ,Unit
                                         ,Dosage      ,Spec      ,Factory      ,Price      ,ValidFlag      ,AuditFlag      ,AuditDate      ,SelfScale      ,PYCode      ,WBCode      ,Memo 
                                        FROM CloudHISDB.dbo.MI_Match_HIS WHERE WorkID = {0}",HosID);
            DataTable DT = oleDb.GetDataTable( SQL);
            return DT;
        }
        /// <summary>
        /// 获取HIS目录
        /// </summary>
        /// <param name="catalogType"></param>
        /// <returns></returns>
        public DataTable M_GetHISCatalogInfo(int catalogType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(catalogType);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMatchController", "M_GetHISCatalogInfo", requestAction);
            DataTable dtMemberInfo = retdataMember.GetData<DataTable>(0);
            return dtMemberInfo;
        }
        #endregion

        #region 住院界面
        /// <summary>
        /// 更新病人费用状态
        /// </summary>
        /// <param name="iPatientId"></param>
        /// <returns></returns>
        public bool Zy_UploadzyPatFee(int iPatientId, int iFlag)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iPatientId);
                request.AddData(iFlag);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIZyController", "Zy_UploadzyPatFee", requestAction);
            bool bMemberInfo = retdataMember.GetData<bool>(0);
            return bMemberInfo;

        }
        #endregion

        #region 门诊界面
        /// <summary>
        /// 保存登记信息并返回该信息
        /// </summary>
        /// <param name="registerId"></param>
        /// <param name="serialNO"></param>
        /// <returns></returns>
        public ResultClass Mz_SaveRegister(MI_Register register)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(register);
            });
            ResultClass resultClass = new ResultClass();
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMzController", "Mz_SaveRegister", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            resultClass.bSucess = b;
            if (b)
            {
                MI_Register registerResult = retdataMember.GetData<MI_Register>(1);
                resultClass.oResult = registerResult;
            }
            else
            {
                resultClass.sRemarks = retdataMember.GetData<string>(1);
            }
            return resultClass;
        }
        /// <summary>
        /// 保存交易信息并返回交易表
        /// </summary>
        /// <param name="medicalInsurancePayRecord"></param>
        /// <param name="mIPayRecordHead"></param>
        /// <param name="mIPayRecordDetailList"></param>
        /// <returns></returns>
        public MI_MedicalInsurancePayRecord SaveTradeInfo(MI_MedicalInsurancePayRecord medicalInsurancePayRecord, MI_MIPayRecordHead mIPayRecordHead, List<MI_MIPayRecordDetail> mIPayRecordDetailList)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(medicalInsurancePayRecord);
                request.AddData(mIPayRecordHead);
                request.AddData(mIPayRecordDetailList);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMzController", "SaveTradeInfo", requestAction);
            bool b= retdataMember.GetData<bool>(0);
            if (b)
            {
                MI_MedicalInsurancePayRecord medicalInsurancePayRecordResult = retdataMember.GetData<MI_MedicalInsurancePayRecord>(1);
                return medicalInsurancePayRecordResult;
            }
            else
            {
                return null;
            }           
        }
        /// <summary>
        /// ID或者门诊号/住院号获取登记信息
        /// </summary>
        /// <param name="regId">不需要的时候填0即可</param>
        /// <param name="serialNO">不需要的时候随意</param>
        /// <returns></returns>
        public MI_Register Mz_Getregister(int regId, string serialNO)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(regId);
                request.AddData(serialNO);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMzController", "Mz_Getregister", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            if (b)
            {
                MI_Register register = retdataMember.GetData<MI_Register>(1);
                return register;
            }
            else
            { return null; }
        }
        /// <summary>
        /// 获取交易信息表
        /// </summary>
        /// <param name="getWay">获取方式 1：regId，2：FeeNO，3：TradeNo，defau：ID</param>
        /// <param name="cValue">参数值</param>
        /// <param name="type">类型：1挂号；2：收费，用ID 或者发票号 或者TradeNo 的时候 不需要用到</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public MI_MedicalInsurancePayRecord Mz_GetPayRecord(int getWay, string cValue,int type, int state)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(getWay);
                request.AddData(cValue);
                request.AddData(type);
                request.AddData(state);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMzController", "Mz_GetPayRecord", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            if (b)
            {
                MI_MedicalInsurancePayRecord medicalInsurancePayRecord = retdataMember.GetData<MI_MedicalInsurancePayRecord>(1);
                return medicalInsurancePayRecord;
            }
            else
            { return null; }
        }

        public ResultClass Mz_SavePayRecord(MI_MedicalInsurancePayRecord medicalInsurancePayRecord)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(medicalInsurancePayRecord);
            });
            ResultClass resultClass = new ResultClass();
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMzController", "Mz_SavePayRecord", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            resultClass.bSucess = b;
            if (b)
            {
                MI_MedicalInsurancePayRecord medicalInsurancePayRecordResult = retdataMember.GetData<MI_MedicalInsurancePayRecord>(1);
                resultClass.oResult = medicalInsurancePayRecordResult;
            }
            else
            {
                resultClass.sRemarks = retdataMember.GetData<string>(1);
            }
            return resultClass;
        }

        public bool Mz_JudgeRegister(int regId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(regId);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMzController", "Mz_JudgeRegister", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            return b;
        }

        /// <summary>
        /// 获取交易记录头表
        /// </summary>
        /// <param name="PayRecordId"></param>
        /// <returns></returns>
        public MI_MIPayRecordHead Mz_GetPayRecordHead(int PayRecordId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(PayRecordId);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMzController", "Mz_GetPayRecordHead", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            if (b)
            {
                MI_MIPayRecordHead mIPayRecordHead = retdataMember.GetData<MI_MIPayRecordHead>(1);
                return mIPayRecordHead;
            }
            else
            { return null; }
        }

        /// <summary>
        /// 获取交易记录明细表
        /// </summary>
        /// <param name="PayRecordId"></param>
        /// <returns></returns>
        public List<MI_MIPayRecordDetail> Mz_GetPayRecordDetail(int PayRecordId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(PayRecordId);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMzController", "Mz_GetPayRecordDetail", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            if (b)
            {
                List<MI_MIPayRecordDetail> mIPayRecordDetailList = retdataMember.GetData<List<MI_MIPayRecordDetail>>(1);
                return mIPayRecordDetailList;
            }
            else
            { return null; }
        }


        /// <summary>
        /// 获取交易记录明细表
        /// </summary>
        /// <param name="PayRecordId"></param>
        /// <returns></returns>
        public DataTable Mz_GetPayRecordDetailForPrint(int PayRecordId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(PayRecordId);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMzController", "Mz_GetPayRecordDetailForPrint", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            if (b)
            {
                DataTable dtMIPayRecordDetail = retdataMember.GetData<DataTable>(1);
                return dtMIPayRecordDetail;
            }
            else
            { return null; }
        }

        public MI_MedicalInsuranceType Mz_GetMITypeInfo(string MedicalClass)
        {            
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(MedicalClass);
            });

            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMzController", "Mz_GetMITypeInfo", requestAction);
            bool b = retdataMember.GetData<bool>(0);
            if (b)
            {
                MI_MedicalInsuranceType medicalInsuranceType = retdataMember.GetData<MI_MedicalInsuranceType>(1);
                return medicalInsuranceType;
            }
            else
            { return null; }            
        }



        #endregion
    }
}
