using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_IPManage.Dao;
using HIS_IPManage.ObjectModel;

namespace HIS_IPManage.WcfController
{
    /// <summary>
    /// 预交金管理控制器
    /// </summary>
    [WCFController]
    public class PrepaidPaymentSysController : WcfServerController
    {
        /// <summary>
        /// 获取病人预交金缴纳记录
        /// </summary>
        /// <returns>预交金缴纳记录</returns>
        [WCFMethod]
        public ServiceResponseData GetPaymentList()
        {
            int patListID = requestData.GetData<int>(0);
            decimal serialNumber = requestData.GetData<decimal>(1);
            DataTable payDt = NewObject<DepositManagement>().GetPaymentList(patListID, serialNumber);
            responseData.AddData(payDt);
            DataTable feeDt = NewDao<IIPManageDao>().GetPatDepositFee(patListID);
            responseData.AddData(feeDt);
            return responseData;
        }

        /// <summary>
        /// 预交金退费
        /// </summary>
        /// <returns>退费成功或失败</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData Refund()
        {
            int depositID = requestData.GetData<int>(0);
            string msg = NewObject<DepositManagement>().Refund(depositID);
            responseData.AddData(msg);
            return responseData;
        }
    }
}
