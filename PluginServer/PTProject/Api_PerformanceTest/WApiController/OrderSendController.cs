using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WebFrame.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Api_PerformanceTest.WApiController
{
    /// <summary>
    ///医嘱发送性能测试接口
    /// http://localhost:8021/HISApi/PTProject.Service/OrderSend/Test
    /// http://localhost:8021/HISApi/PTProject.Service/OrderSend/OrderSend?iDeptId=
    /// </summary>
    [efwplusApiController(PluginName = "PTProject.Service")]
    public class OrderSendController : WebApiController
    {
        [HttpGet]
        public string Test()
        {
            string hello = "Hello";
            return hello + " WebApi";
        }

        [HttpGet]
        public string OrderSend(int iDeptId)
        {
            try
            {
                //获取病人信息
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(iDeptId);
                });
                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderCheckController", "AutoDeptSendOrderCheckList", requestAction);
                return retdata.GetData<string>(0);
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
