using HIS_Entity.MIManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiInterfaceDLL;

namespace HIS_MIInterface.Interface.CustomAction.例子
{
    public class CustomMIInterfaceDao
    {
        private string _cardNo = "111111111111";
        private SiInterface iface = new SiInterface();
        #region 门诊
        /// <summary>
        /// 调用读取卡片信息接口
        /// </summary>
        /// <param name="b"></param>
        /// <param name="s"></param>
        /// <param name="dt"></param>
        public ResultClass Mz_GetCardInfo(SiInterfaceDll sDll,string cardNo)
        {
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
                resultClass.sRemarks = e.Message;
                resultClass.oResult = null;
            }
            return resultClass;
        }
        /// <summary>
        /// 获取病人信息
        /// </summary>
        public ResultClass Mz_GetPersonInfo(SiInterfaceDll sDll,string cardNo)
        {
            _cardNo = cardNo;
            ResultClass resultClass = new ResultClass();
            try
            {
                resultClass = iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    resultClass = iface.GetPersonInfo(sDll, cardNo);
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
        /// 门诊预登记
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public ResultClass MZ_PreviewRegister(SiInterfaceDll sDll,Reg.root root)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                resultClass = iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    resultClass = iface.GetPersonInfo(sDll, _cardNo);
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
        public ResultClass MZ_Register(SiInterfaceDll sDll)
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

        /// <summary>
        /// 取消门诊登记
        /// </summary>
        /// <returns></returns>
        public ResultClass Mz_CancelRegister(SiInterfaceDll sDll,string tradeNo)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                resultClass = iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    resultClass = iface.RefundmentDivide(sDll, tradeNo);
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
        public ResultClass MZ_PreviewCharge(SiInterfaceDll sDll,Reg.root root)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                resultClass = iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
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
        public ResultClass MZ_Charge(SiInterfaceDll sDll)
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
        /// <summary>
        /// 取消门诊结算
        /// </summary>
        /// <returns></returns>
        public ResultClass MZ_CancelCharge(SiInterfaceDll sDll,string tradeNo)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                resultClass = iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    resultClass = iface.RefundmentDivide(sDll, tradeNo);
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

        #endregion
    }
}
