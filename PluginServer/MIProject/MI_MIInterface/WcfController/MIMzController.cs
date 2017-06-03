using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using MI_MIInterface.ObjectModel;
using HIS_Entity.MIManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MI_MIInterface.ObjectModel.Common;

namespace MI_MIInterface.WcfController
{
    [WCFController]
    public class MIMzController : WcfServerController
    {        
        /// <summary>
        /// 调用读取卡片信息接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData Mz_GetCardInfo()
        {
            InputClass inputClass = requestData.GetData<InputClass>(0);
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, "读卡,获取卡片信息开始,卡号："+ inputClass.SInput[InputType.CardNo].ToString());
            
            ResultClass resultClass=NewObject< ActionObjectFactory>().Mz_GetCardInfo(inputClass);
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, "获取卡片信息结束，结果：" + resultClass.bSucess);
            responseData.AddData(resultClass.bSucess);
            responseData.AddData(resultClass.sRemarks);
            responseData.AddData(resultClass.oResult);
            return responseData;
        }
        /// <summary>
        /// 调用获取个人信息接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData Mz_GetPersonInfo()
        {
            InputClass inputClass = requestData.GetData<InputClass>(0);

            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, "读卡,获取病人信息开始,卡号：");

            ResultClass resultClass = NewObject<ActionObjectFactory>().MZ_GetPersonInfo(inputClass);
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, "读卡,获取病人信息结束，结果：" + resultClass.bSucess);
            responseData.AddData(resultClass.bSucess);
            responseData.AddData(resultClass.sRemarks);
            responseData.AddData(resultClass.oResult);
            return responseData; 
        }

        /// <summary>
        /// 门诊预登记返回Dictionary
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData MZ_PreviewRegister()
        {
            InputClass inputClass = requestData.GetData<InputClass>(0);
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, "预登记开始，病人社保编号：");
            ResultClass resultClass = NewObject<ActionObjectFactory>().MZ_PreviewRegister(inputClass);
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, "预登记结束，结果：" + resultClass.bSucess+" 备注："+ resultClass.sRemarks);
            responseData.AddData(resultClass.bSucess);
            responseData.AddData(resultClass.sRemarks);
            responseData.AddData(resultClass.oResult);
            return responseData;
        }

        /// <summary>
        /// 门诊登记确认返回Dictionary
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData MZ_Register()
        {
            InputClass inputClass = requestData.GetData<InputClass>(0);

            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, "确认登记开始，病人门诊号：" );
            ResultClass resultClass = NewObject<ActionObjectFactory>().MZ_Register(inputClass);
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, "确认登记结束，结果：" + resultClass.bSucess);
            responseData.AddData(resultClass.bSucess);
            responseData.AddData(resultClass.sRemarks);
            responseData.AddData(resultClass.oResult);
            return responseData;
        }

        /// <summary>
        /// 门诊登记确认返回null
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData MZ_UpdateRegister()
        {
            InputClass inputClass = requestData.GetData<InputClass>(0);

            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, "等级完成，更新门诊号：");
            ResultClass resultClass = NewObject<ActionObjectFactory>().MZ_UpdateRegister(inputClass);
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, "等级完成，更新门诊号结束，结果：" + resultClass.bSucess);
            responseData.AddData(resultClass.bSucess);
            responseData.AddData(resultClass.sRemarks);
            responseData.AddData(resultClass.oResult);
            return responseData;
        }


        /// <summary>
        /// 取消门诊登记
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData Mz_CancelRegister()
        {
            InputClass inputClass = requestData.GetData<InputClass>(0);
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, "取消登记开始，病人门诊号：" );
            ResultClass resultClass = NewObject<ActionObjectFactory>().Mz_CancelRegister(inputClass);
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, "取消登记结束，结果：" + resultClass.bSucess);
            responseData.AddData(resultClass.bSucess);
            responseData.AddData(resultClass.sRemarks);
            responseData.AddData(resultClass.oResult);
            return responseData;
        }
        /// <summary>
        /// 确认取消门诊登记
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData MZ_CancelRegisterCommit()
        {
            InputClass inputClass = requestData.GetData<InputClass>(0);
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, "取消登记开始，病人门诊号：");
            ResultClass resultClass = NewObject<ActionObjectFactory>().MZ_CancelRegisterCommit(inputClass);
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, "取消登记结束，结果：" + resultClass.bSucess);
            responseData.AddData(resultClass.bSucess);
            responseData.AddData(resultClass.sRemarks);
            responseData.AddData(resultClass.oResult);
            return responseData;
        }
        /// <summary>
        /// 调用费用预结算接口
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData MZ_PreviewCharge()
        {
            InputClass inputClass = requestData.GetData<InputClass>(0);

            AddLog("收费预算开始");
            ResultClass resultClass = NewObject<ActionObjectFactory>().MZ_PreviewCharge(inputClass);
            AddLog("收费预算结束，结果：" + resultClass.bSucess);
            responseData.AddData(resultClass.bSucess);
            responseData.AddData(resultClass.sRemarks);
            responseData.AddData(resultClass.oResult);

            return responseData;
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData MZ_Charge()
        {
            InputClass inputClass = requestData.GetData<InputClass>(0);

            AddLog("确认收费开始，发票号：");
            ResultClass resultClass = NewObject<ActionObjectFactory>().MZ_Charge(inputClass);
            AddLog("确认收费结束，结果：" + resultClass.bSucess);
            responseData.AddData(resultClass.bSucess);
            responseData.AddData(resultClass.sRemarks);
            responseData.AddData(resultClass.oResult);
            return responseData;
        }
        /// <summary>
        /// 取消门诊结算
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData MZ_CancelCharge()
        {
            InputClass inputClass = requestData.GetData<InputClass>(0);
            AddLog("退费开始");
            ResultClass resultClass = NewObject<ActionObjectFactory>().MZ_CancelCharge(inputClass);
            AddLog("退费结束，结果：" + resultClass.bSucess);
            responseData.AddData(resultClass.bSucess);
            responseData.AddData(resultClass.sRemarks);
            responseData.AddData(resultClass.oResult);
            return responseData;
        }
        /// <summary>
        /// 确认取消门诊收费
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData MZ_CancelChargeCommit()
        {
            InputClass inputClass = requestData.GetData<InputClass>(0);
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, "取消登记开始，病人门诊号：");
            ResultClass resultClass = NewObject<ActionObjectFactory>().MZ_CancelChargeCommit(inputClass);
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, "取消登记结束，结果：" + resultClass.bSucess);
            responseData.AddData(resultClass.bSucess);
            responseData.AddData(resultClass.sRemarks);
            responseData.AddData(resultClass.oResult);
            return responseData;
        }
        /// <summary>
        /// 上传门诊病人费用
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData MZ_UploadzyPatFee() { throw new NotImplementedException(); }
        /// <summary>
        /// 下载门诊病人费用数据
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData MZ_DownloadzyPatFee() { throw new NotImplementedException(); }

        //获取已结算费用
        [WCFMethod]
        public ServiceResponseData MZ_LoadFeeDetailByTicketNum() { throw new NotImplementedException(); }

        /// <summary>
        /// 根据医保交易状态更新交易记录
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData Mz_UpdateTradeRecord()
        {
            InputClass inputClass = requestData.GetData<InputClass>(0);

            ResultClass resultClass = NewObject<ActionObjectFactory>().Mz_UpdateTradeRecord(inputClass);

            responseData.AddData(resultClass.bSucess);
            responseData.AddData(resultClass.sRemarks);
            responseData.AddData(resultClass.oResult);
            return responseData;
        }

        /// <summary>
        /// 根据医保交易状态更新交易记录
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData Mz_GetRegisterTradeNo()
        {
            string sSerialNO = requestData.GetData<string>(0);

            ResultClass resultClass = NewObject<ActionObjectFactory>().Mz_GetRegisterTradeNo(sSerialNO);

            responseData.AddData(resultClass.bSucess);
            responseData.AddData(resultClass.sRemarks);
            responseData.AddData(resultClass.oResult);
            return responseData;
        }
        private void AddLog(string s)
        {
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, s);
        }
    }
}
