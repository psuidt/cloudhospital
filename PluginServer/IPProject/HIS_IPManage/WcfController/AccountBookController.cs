using System;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_IPManage.Dao;

namespace HIS_IPNurse.WcfController
{    
    /// <summary>
     /// 住院现金流量日记账服务端控制器
     /// </summary>
    [WCFController]
    public class AccountBookController : WcfServerController
    {
        #region 定时发送接口

        /// <summary>
        /// 住院每日收入流水定时发送接口  日期直接取当天24点 
        /// </summary>
        /// <returns>发送成功或失败</returns>
        [WCFMethod]
        //[AOP(typeof(AopTransaction))]
        public ServiceResponseData AutoCostRunninAccount()
        {
            try
            {
                int[] iWorkIDList = requestData.GetData<int[]>(0);
                foreach (int iWorkId in iWorkIDList)
                {
                    SetWorkId(iWorkId);
                    SetWorkId(iWorkId);

                    MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                    bool b = NewDao<IIPManageDao>().InsertIPAccountBookData(0);
                    MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, (b ? System.Drawing.Color.Blue : System.Drawing.Color.Red), DateTime.Now.ToString("yyyy-MM-dd") + (b ? " 住院每日收入流水发送成功！" : " 住院每日收入流水发送失败！"));
                    MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                }

                return responseData;
            }
            catch (Exception e)
            {
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, DateTime.Now.ToString("yyyy-MM-dd") + " 住院每日收入流水发送异常！");
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "住院每日收入流水发送错误信息：");
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "  " + e.Message);
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                return null;
            }
        }

        /// <summary>
        /// 住院每日预交金流水定时发送接口  日期直接取当天24点
        /// </summary>
        /// <returns>发送成功或失败</returns>
        [WCFMethod]
        //[AOP(typeof(AopTransaction))]
        public ServiceResponseData AutoDepositFeeRunninAccount()
        {
            try
            {
                int[] iWorkIDList = requestData.GetData<int[]>(0);

                foreach (int iWorkId in iWorkIDList)
                {
                    SetWorkId(iWorkId);

                    MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                    bool b = NewDao<IIPManageDao>().InsertIPAccountBookData(1);
                    MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true,(b?System.Drawing.Color.Blue: System.Drawing.Color.Red), DateTime.Now.ToString("yyyy-MM-dd") + (b ? " 住院每日预交金流水发送成功！" : " 住院每日预交金流水发送失败！"));
                    MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                 }

                return responseData;
            }
            catch (Exception e)
            {
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, DateTime.Now.ToString("yyyy-MM-dd") + " 住院每日预交金流水发送异常！");
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "住院每日预交金流水自动发送错误信息：");
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "  " + e.Message);
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                return null;
            }
        }
        #endregion
    }
}
