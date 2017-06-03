using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
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
    public class MITradeQueryController : WcfServerController
    {
        #region 对账用
        [WCFMethod]
        public ServiceResponseData Mz_GetTradeInfoSummary()
        {
            int iMIID = requestData.GetData<int>(0);
            int iPatientType = requestData.GetData<int>(1);
            string sDeptCode = requestData.GetData<string>(2);
            string sTimeStat = requestData.GetData<string>(3);
            string sTimeStop = requestData.GetData<string>(4);

            DataTable dt = NewDao<IMITradeQuery>().Mz_GetTradeInfoSummary(iMIID, iPatientType, sDeptCode, sTimeStat, sTimeStop);
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
        public ServiceResponseData Mz_GetTradeRecordInfo()
        {
            int iMIID = requestData.GetData<int>(0);
            int iPatientType = requestData.GetData<int>(1);
            string sDeptCode = requestData.GetData<string>(2);
            string sTimeStat = requestData.GetData<string>(3);
            string sTimeStop = requestData.GetData<string>(4);

            DataTable dt = NewDao<IMITradeQuery>().Mz_GetTradeRecordInfo(iMIID, iPatientType, sDeptCode, sTimeStat, sTimeStop);
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
        public ServiceResponseData Mz_GetTradeDetailInfo()
        {
            int iMIID = requestData.GetData<int>(0);
            int iPatientType = requestData.GetData<int>(1);
            string sDeptCode = requestData.GetData<string>(2);
            string sTimeStat = requestData.GetData<string>(3);
            string sTimeStop = requestData.GetData<string>(4);

            DataTable dt = NewDao<IMITradeQuery>().Mz_GetTradeDetailInfo(iMIID, iPatientType, sDeptCode, sTimeStat, sTimeStop);
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
        #endregion
    }
}
