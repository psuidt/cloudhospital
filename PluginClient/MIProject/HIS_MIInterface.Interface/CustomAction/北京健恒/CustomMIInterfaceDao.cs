using HIS_Entity.MIManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicareComLib;

namespace HIS_MIInterface.Interface.CustomAction.北京健恒
{   
    /// <summary>
    /// 医保端的操作，可连接前台接口，也可在这连接后台接口
    /// </summary>
    public class CustomMIInterfaceDao
    {
        private string _cardNo = "111111111111";
        private SiInterface iface = new SiInterface();
        private Reg.root _ghRoot;

        #region 门诊
        /// <summary>
        /// 调用读取卡片信息接口
        /// </summary>
        /// <param name="b"></param>
        /// <param name="s"></param>
        /// <param name="dt"></param>
        public ResultClass Mz_GetCardInfo(OutpatientClass sDll, string cardNo)
        {
            if (cardNo != String.Empty)
            {
                _cardNo = cardNo;
            }
            ResultClass resultClass = new ResultClass();
            try
            {
                resultClass = iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    resultClass = iface.GetCardInfo(sDll, cardNo);
                    iface.CloseDevice(sDll);
                }
                else
                {
                    resultClass.oResult = null;
                }
                sDll = null;
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = "无法读取医保卡信息！请确保医保网络正常！";//e.Message;
                resultClass.oResult = null;
            }
            return resultClass;
        }
        /// <summary>
        /// 获取病人信息
        /// </summary>
        public ResultClass Mz_GetPersonInfo(OutpatientClass sDll, string cardNo)
        {
            if (cardNo != String.Empty)
            {
                _cardNo = cardNo;
            }
            ResultClass resultClass = new ResultClass();
            try
            {
                resultClass = iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    string s = "";
                    if (resultClass.sRemarks != "")
                    {
                        s = resultClass.sRemarks;
                    }
                    resultClass = iface.GetPersonInfo(sDll, _cardNo);
                    if (resultClass.bSucess)
                    {
                        resultClass.sRemarks += s;
                    }
                    else
                    {
                        resultClass.sRemarks += s;
                    }
                    iface.CloseDevice(sDll);
                }
                else
                {
                    resultClass.oResult = null;
                }
                sDll = null;
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = "无法读取病人信息！请确保医保网络正常！"; //e.Message;
                resultClass.oResult = null;
            }
            return resultClass;
        }
        /// <summary>
        /// 门诊预登记
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public ResultClass MZ_PreviewRegister(OutpatientClass sDll, Reg.root root)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                _ghRoot = root;
                resultClass = iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    //resultClass = iface.GetPersonInfo(sDll, _cardNo);
                    resultClass = iface.Divide(sDll, root);
                    iface.CloseDevice(sDll);
                }
                else
                {
                    resultClass.oResult = null;
                }
                sDll = null;
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
                resultClass.oResult = null;
            }
            return resultClass;
        }
       
        /// <summary>
        /// 门诊登记
        /// </summary>
        /// <returns></returns>
        public ResultClass MZ_Register(OutpatientClass sDll)
        {
            //ResultClass resultClass = new ResultClass();
            //try
            //{
            //    resultClass = iface.OpenDevice(sDll);
            //    if (resultClass.bSucess)
            //    {
            //        resultClass = iface.Trade(sDll);
            //        iface.CloseDevice(sDll);
            //    }
            //    else
            //    {
            //        resultClass.oResult = null;
            //    }
            //    sDll = null;
            //}
            //catch (Exception e)
            //{
            //    resultClass.bSucess = false;
            //    resultClass.sRemarks = e.Message;
            //    resultClass.oResult = null;
            //}
            //return resultClass;
            return MZ_CommitTrade(sDll);
        }

        /// <summary>
        /// 取消门诊登记
        /// </summary>
        /// <returns></returns>
        public ResultClass Mz_CancelRegister(OutpatientClass sDll, string tradeNo,string serialNO)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                resultClass = iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    resultClass = iface.GetPersonInfo(sDll, _cardNo);
                    resultClass = iface.RefundmentDivide(sDll, tradeNo, serialNO);
                    //resultClass1=iface.Trade(sDll);
                    iface.CloseDevice(sDll);
                }
                else
                {
                    resultClass.oResult = null;
                }
                sDll = null;
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
                resultClass.oResult = null;
            }
            return resultClass;
        }
        /// <summary>
        /// 预算
        /// </summary>
        /// <param name="b"></param>
        /// <param name="s"></param>
        /// <param name="dt"></param>
        public ResultClass MZ_PreviewCharge(OutpatientClass sDll, Reg.root root)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                resultClass = iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    //resultClass = iface.GetPersonInfo(sDll, _cardNo);
                    resultClass = iface.Divide(sDll, root);
                    iface.CloseDevice(sDll);
                }
                else
                {
                    resultClass.oResult = null;
                }
                sDll = null;
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
                resultClass.oResult = null;
            }
            return resultClass;
        }
        /// <summary>
        /// 结算
        /// </summary>
        /// <returns></returns>
        public ResultClass MZ_Charge(OutpatientClass sDll)
        {
            //ResultClass resultClass = new ResultClass();
            //try
            //{
            //    resultClass = iface.OpenDevice(sDll);
            //    if (resultClass.bSucess)
            //    {
            //        resultClass = iface.Trade(sDll);
            //        iface.CloseDevice(sDll);
            //    }
            //    else
            //    {
            //        resultClass.oResult = null;
            //    }
            //    sDll = null;
            //}
            //catch (Exception e)
            //{
            //    resultClass.bSucess = false;
            //    resultClass.sRemarks = e.Message;
            //    resultClass.oResult = null;
            //}
            //return resultClass;
            return MZ_CommitTrade(sDll);
        }
        /// <summary>
        /// 取消门诊结算
        /// </summary>
        /// <returns></returns>
        public ResultClass MZ_CancelCharge(OutpatientClass sDll, string tradeNo,string invoiceNo)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                resultClass = iface.OpenDevice(sDll);

                if (resultClass.bSucess)
                {
                    resultClass = iface.GetPersonInfo(sDll, _cardNo);
                    resultClass = iface.RefundmentDivide(sDll, tradeNo, invoiceNo);
                    //resultClass1=iface.Trade(sDll);
                    iface.CloseDevice(sDll);
                }
                else
                {
                    resultClass.oResult = null;
                }
                sDll = null;
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
                resultClass.oResult = null;
            }


            return resultClass;
        }
        
        /// <summary>
        /// 上传门诊病人费用
        /// </summary>
        /// <returns></returns>
        public ResultClass MZ_UploadzyPatFee() { throw new NotImplementedException(); }
        /// <summary>
        /// 下载门诊病人费用数据
        /// </summary>
        /// <returns></returns>
        public ResultClass MZ_DownloadzyPatFee() { throw new NotImplementedException(); }
        //获取已结算费用
        public ResultClass MZ_LoadFeeDetailByTicketNum() { throw new NotImplementedException(); }

        public ResultClass MZ_PrintInvoice(OutpatientClass sDll, string sIn)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                resultClass = iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    resultClass = iface.PrintInvoice(sDll, sIn);
                    iface.CloseDevice(sDll);
                }
                else
                {
                    resultClass.oResult = null;
                }
                sDll = null;
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
                resultClass.oResult = null;
            }
            return resultClass;
        }

        public ResultClass RePrintInvoice(OutpatientClass sDll, string tradeNo, string invoiceNo)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                resultClass = iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    resultClass = iface.RePrintInvoice(sDll, tradeNo, invoiceNo);
                    iface.CloseDevice(sDll);
                }
                else
                {
                    resultClass.oResult = null;
                }
                sDll = null;
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
                resultClass.oResult = null;
            }
            return resultClass;

            
            

        }

        public ResultClass MZ_CommitTradeState(OutpatientClass sDll, string tradeNo)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                resultClass = iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    resultClass = iface.CommitTradeState(sDll, tradeNo);
                    iface.CloseDevice(sDll);
                }
                else
                {
                    resultClass.oResult = null;
                }
                sDll = null;
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
                resultClass.oResult = null;
            }
            return resultClass;



        }

        /// <summary>
        /// 确认交易
        /// </summary>
        /// <param name="sDll"></param>
        /// <returns></returns>
        public ResultClass MZ_CommitTrade(OutpatientClass sDll)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                resultClass = iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    resultClass = iface.Trade(sDll);
                    iface.CloseDevice(sDll);
                }
                else
                {
                    resultClass.oResult = null;
                }
                sDll = null;
            }
            catch (Exception e)
            {
                resultClass.bSucess = false;
                resultClass.sRemarks = e.Message;
                resultClass.oResult = null;
            }
            return resultClass;
        }
        #endregion
    }
}
