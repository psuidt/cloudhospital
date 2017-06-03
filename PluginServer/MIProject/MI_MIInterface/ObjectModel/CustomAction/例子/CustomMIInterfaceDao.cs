using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using MI_MIInterface.ObjectModel.BaseClass;
using SiInterfaceDLL;
using HIS_Entity.MIManage;

namespace MI_MIInterface.ObjectModel.CustomAction.例子
{
    /// <summary>
    /// 医保接口操作类
    /// </summary>
    public class CustomMIInterfaceDao: AbstractMIInterfaceDao
    {
        private string _cardNo = "111111111111";
        private SiInterface iface = new SiInterface();

        public override void BeginInitData(Hashtable param)
        {
        }
        public override void EndInitData()
        {
            base.EndInitData();
        }
        
        #region 门诊
        /// <summary>
        /// 调用读取卡片信息接口
        /// </summary>
        /// <param name="b"></param>
        /// <param name="s"></param>
        /// <param name="dt"></param>
        public ResultClass Mz_GetCardInfo(string cardNo)
        {
            ResultClass resultClass = new ResultClass();
            try
            {                
                SiInterfaceDll sDll = new SiInterfaceDll();
                resultClass=iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    resultClass = iface.GetCardInfo(sDll, cardNo);
                    iface.CloseDevice(sDll);
                }
                else
                {
                    resultClass.oResult= null;
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
        public ResultClass Mz_GetPersonInfo(string cardNo)
        {
            _cardNo = cardNo;
            ResultClass resultClass = new ResultClass();
            try
            {
                SiInterfaceDll sDll = new SiInterfaceDll();
                resultClass=iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    resultClass=iface.GetPersonInfo(sDll, cardNo);
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
        public ResultClass MZ_PreviewRegister(HIS_Entity.MIManage.Common.Reg.root root)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                SiInterfaceDll sDll = new SiInterfaceDll();
                resultClass = iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    iface.GetPersonInfo(sDll, _cardNo);
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
        public ResultClass MZ_Register()
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                SiInterfaceDll sDll = new SiInterfaceDll();
                resultClass=iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    iface.GetPersonInfo(sDll, _cardNo);
                    resultClass =iface.Trade(sDll);
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
        public ResultClass Mz_CancelRegister(string tradeNo)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                SiInterfaceDll sDll = new SiInterfaceDll();
                resultClass = iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    iface.GetPersonInfo(sDll, _cardNo);
                    resultClass = iface.RefundmentDivide(sDll,tradeNo);
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
        public ResultClass MZ_PreviewCharge(HIS_Entity.MIManage.Common.Reg.root root)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                SiInterfaceDll sDll = new SiInterfaceDll();
                resultClass = iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    iface.GetPersonInfo(sDll, _cardNo);
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
        public ResultClass MZ_Charge()
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                SiInterfaceDll sDll = new SiInterfaceDll();
                resultClass = iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    iface.GetPersonInfo(sDll, _cardNo);
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
        public ResultClass MZ_CancelCharge(string tradeNo)
        {
            ResultClass resultClass = new ResultClass();
            try
            {
                SiInterfaceDll sDll = new SiInterfaceDll();
                resultClass = iface.OpenDevice(sDll);
                if (resultClass.bSucess)
                {
                    iface.GetPersonInfo(sDll, _cardNo);
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

        #region 公用方法
        public string DataTableToXml(DataTable dt)
        {
            StringBuilder strXml = new StringBuilder();
            strXml.AppendLine("<XmlTable>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strXml.AppendLine("<rows>");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    strXml.AppendLine("<" + dt.Columns[j].ColumnName + ">" + dt.Rows[i][j] + "</" + dt.Columns[j].ColumnName + ">");
                }
                strXml.AppendLine("</rows>");
            }
            strXml.AppendLine("</XmlTable>");
            return strXml.ToString();
        }

        //public string CreatXml(DataTable dt)
        //{
        //    XmlSerializer serializer = new XmlSerializer(t);
        //    FileStream stream = new FileStream(filePath, FileMode.Open);
        //    Department dep = (Department)serializer.Deserialize(stream);
        //    stream.Close();
        //}
        #endregion
    }
}
