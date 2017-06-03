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
using SiInterfaceDLL;

namespace MI_MIInterface.WcfController
{
    [WCFController]
    public class MIZyController : WcfServerController
    {
        SiInterfaceDll SiInterface = new SiInterfaceDll();
        [WCFMethod]
        public ServiceResponseData Zy_UploadzyPatFee()
        {
            int iPatientId = requestData.GetData<int>(0);
            int iFlag = requestData.GetData<int>(1);

            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, "上传病人费用,病人ID：" + iPatientId+" 开始");
            bool b =  NewObject<ActionObjectFactory>().Zy_UploadzyPatFee(iPatientId, iFlag);
            string s = "";
            SiInterface.Open(out s);

            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, "上传病人费用,病人ID：" + iPatientId+" 结束，结果："+b);

            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Red, s);
            responseData.AddData(b);
            return responseData;
        }


    }
}
