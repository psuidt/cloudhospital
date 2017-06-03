using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using System.Data;
using MI_MIInterface.Dao;
using MI_MIInterface.ObjectModel;
using EFWCoreLib.CoreFrame.Common;
using System.Drawing;

namespace MI_MIInterface.WcfController
{
    [WCFController]
    public class MIMatchController : WcfServerController
    {
        /// <summary>
        /// 获取HIS目录
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData M_GetHISCatalogInfo()
        {
            bool dtPayMentInfo = NewObject<ActionObjectFactory>().M_DownLoadHospContent();
            responseData.AddData(dtPayMentInfo);
            return responseData;
        }
        /// <summary>
        /// 获取医保配置文件数据
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData M_GetMedicalInsuranceData()
        {
            int typeId = requestData.GetData<int>(0);
            string method = requestData.GetData<string>(1);
            string name = requestData.GetData<string>(2);

            string s= ObjectModel.Common.ActionMappingConfig.GetMedicalInsuranceData( typeId,  method,  name);
            responseData.AddData(s);
            return responseData;
        }
        /// <summary>
        /// 更新医保目录
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData M_UpdateMIlog()
        {
            int ybId = requestData.GetData<int>(0);
            int workId = requestData.GetData<int>(1);
            int logType = requestData.GetData<int>(2);
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, "接口测试,医保ID："+ ybId);
            bool b = NewObject<ActionObjectFactory>().M_UpdateMIlog(ybId, workId, logType);
            responseData.AddData(b);
            MiddlewareLogHelper.WriterLog(LogType.MILog, false, Color.Blue, "接口测试：" + ybId);
            return responseData;

        }
    }
}
