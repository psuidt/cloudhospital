using EfwControls.Common;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.WcfFrame;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.MIManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_MIInterface.Interface.BaseClass
{
    /// <summary>
    /// HIS端的操作，连接后台 服务
    /// </summary>
    public class AbstractHISDao
    {
        public static int _WorkId = 0;

        public void WorkId(int WorkId)
        {
            _WorkId = WorkId;
        }
        #region 挂号
        /// <summary>
        /// 预登记
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public ResultClass MZ_PreviewRegister(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(inputClass);
                });
                ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "MZ_PreviewRegister", requestAction);
                resultClass.bSucess = retdataMember.GetData<bool>(0);
                resultClass.sRemarks = retdataMember.GetData<string>(1);
                if (resultClass.bSucess)
                {
                    Dictionary<string, string> resultDic = retdataMember.GetData<Dictionary<string, string>>(2);
                    resultClass.oResult = resultDic;
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = "连接医保后台服务报错："+e.Message;
            }
            return resultClass;
        }
        /// <summary>
        /// 确认登记
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public ResultClass MZ_Register(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(inputClass);
                });
                ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "MZ_Register", requestAction);
                resultClass.bSucess = retdataMember.GetData<bool>(0);
                resultClass.sRemarks = retdataMember.GetData<string>(1);
                if (resultClass.bSucess)
                {
                    Dictionary<string, string> resultDic = retdataMember.GetData<Dictionary<string, string>>(2);
                    resultClass.oResult = resultDic;
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
            }
            return resultClass;
        }
        /// <summary>
        /// 取消登记
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public ResultClass Mz_CancelRegister(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(inputClass);
                });
                ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "Mz_CancelRegister", requestAction);
                resultClass.bSucess = retdataMember.GetData<bool>(0);
                resultClass.sRemarks = retdataMember.GetData<string>(1);
                if (resultClass.bSucess)
                {
                    int newRecordId = retdataMember.GetData<int>(2);
                    resultClass.oResult = newRecordId;
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
            }
            return resultClass;
        }
        /// <summary>
        /// 确认取消挂号 todo
        /// </summary>
        public ResultClass MZ_CancelRegisterCommit(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(inputClass);
                });
                ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "MZ_CancelRegisterCommit", requestAction);
                resultClass.bSucess = retdataMember.GetData<bool>(0);
                resultClass.sRemarks = retdataMember.GetData<string>(1);
                if (resultClass.bSucess)
                {
                    List<DataTable> objects = retdataMember.GetData<List<DataTable>>(2);
                    resultClass.oResult = objects;
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
            }
            return resultClass;
        }
        #endregion

        #region 收费
        /// <summary>
        /// 预结算
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public ResultClass MZ_PreviewCharge(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(inputClass);
                });
                ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "MZ_PreviewCharge", requestAction);
                resultClass.bSucess = retdataMember.GetData<bool>(0);
                resultClass.sRemarks = retdataMember.GetData<string>(1);
                if (resultClass.bSucess)
                {
                    Dictionary<string, string> resultDic = retdataMember.GetData<Dictionary<string, string>>(2);
                    resultClass.oResult = resultDic;
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
            }
            return resultClass;
        }
        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public ResultClass MZ_Charge(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(inputClass);
                });
                ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "MZ_Charge", requestAction);
                resultClass.bSucess = retdataMember.GetData<bool>(0);
                resultClass.sRemarks = retdataMember.GetData<string>(1);
                if (resultClass.bSucess)
                {
                    List<DataTable> objects = retdataMember.GetData<List<DataTable>>(2);
                    resultClass.oResult = objects;
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
            }
            return resultClass;
        }
        /// <summary>
        /// 挂号成功更新登记表
        /// </summary>
        /// <param name="inputClass">RegID，SerNO</param>
        /// <returns>bool,string</returns>
        public ResultClass MZ_UpdateRegister(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(inputClass);
                });
                ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "MZ_UpdateRegister", requestAction);
                resultClass.bSucess = retdataMember.GetData<bool>(0);
                resultClass.sRemarks = retdataMember.GetData<string>(1);
                if (resultClass.bSucess)
                {
                    List<DataTable> objects = retdataMember.GetData<List<DataTable>>(2);
                    resultClass.oResult = objects;
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
            }
            return resultClass;
        }
        /// <summary>
        /// 取消结算
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public ResultClass MZ_CancelCharge(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(inputClass);
                });
                ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "MZ_CancelCharge", requestAction);
                resultClass.bSucess = retdataMember.GetData<bool>(0);
                resultClass.sRemarks = retdataMember.GetData<string>(1);
                List<DataTable> objects = retdataMember.GetData<List<DataTable>>(2);
                resultClass.oResult = objects;
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
            }
            return resultClass;
        }

        /// <summary>
        /// 确认取消结算 todo
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public ResultClass MZ_CancelChargeCommit(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(inputClass);
                });
                ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "MZ_CancelChargeCommit", requestAction);
                resultClass.bSucess = retdataMember.GetData<bool>(0);
                resultClass.sRemarks = retdataMember.GetData<string>(1);
                if (resultClass.bSucess)
                {
                    List<DataTable> objects = retdataMember.GetData<List<DataTable>>(2);
                    resultClass.oResult = objects;
                }
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
            }
            return resultClass;
        }
        #endregion

        public MI_Register Mz_Getregister(int registerId, string serialNO)
        {            
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(registerId);
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
            catch (Exception e)
            {
                 return null; 
            }
        }

        /// <summary>
        /// 根据医保交易状态更新交易记录
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public ResultClass Mz_UpdateTradeRecord(InputClass inputClass)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(inputClass);
                });
                ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "Mz_UpdateTradeRecord", requestAction);
                resultClass.bSucess = retdataMember.GetData<bool>(0);
                resultClass.sRemarks = retdataMember.GetData<string>(1);                
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
            }
            return resultClass;
        }

        public ResultClass Mz_GetRegisterTradeNo(string sSerialNO)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(sSerialNO);
                });
                ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMzController", "Mz_GetRegisterTradeNo", requestAction);
                resultClass.bSucess = retdataMember.GetData<bool>(0);
                resultClass.sRemarks = retdataMember.GetData<string>(1);
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
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

        public MI_MedicalInsurancePayRecord Mz_GetPayRecord(int getWay, string cValue, int type, int state)
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
        public DataTable MZ_GetMIDataMatch()
        {
            ServiceResponseData retdataMember = InvokeWcfService("MIProject.Service", "MIMzController", "MZ_GetMIDataMatch");
            DataTable dt = retdataMember.GetData<DataTable>(0);
            return dt;
        }
        public static ServiceResponseData InvokeWcfService(string wcfpluginname, string wcfcontroller, string wcfmethod)
        {
            return InvokeWcfService(wcfpluginname, wcfcontroller, wcfmethod, null);
        }
        public static ServiceResponseData InvokeWcfService(string wcfpluginname, string wcfcontroller, string wcfmethod, Action<ClientRequestData> requestAction)
        {
            ClientLink wcfClientLink = ClientLinkManage.CreateConnection(wcfpluginname);
            //绑定LoginRight
            Action<ClientRequestData> _requestAction = ((ClientRequestData request) =>
            {
                request.LoginRight = new EFWCoreLib.CoreFrame.Business.SysLoginRight();
                request.LoginRight.WorkId = _WorkId;
                if (requestAction != null)
                    requestAction(request);
            });
            ServiceResponseData retData = wcfClientLink.Request(wcfcontroller, wcfmethod, _requestAction);
            return retData;
        }



        //public string M_GetMedicalInsuranceData(int typeId, string method, string name)
        //{
        //    Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
        //    {
        //        request.AddData(typeId);
        //        request.AddData(method);
        //        request.AddData(name);
        //    });

        //    ServiceResponseData retdataMember = InvokeWcfService("MIService.Service", "MIMatchController", "M_GetMedicalInsuranceData", requestAction);
        //    string sMemberInfo = retdataMember.GetData<string>(0);
        //    return sMemberInfo;
        //}

    }
}
