using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.IPManage;
using HIS_IPManage.ObjectModel;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPManage.WcfController
{
    /// <summary>
    /// 缴款查询控制器
    /// </summary>
    [WCFController]
    public class AllAccountController : WcfServerController
    {
        #region 门诊缴款查询
        /// <summary>
        /// 获取收费员列表
        /// </summary>
        /// <returns>收费员列表</returns>
        [WCFMethod]
        public ServiceResponseData GetCashier()
        {
            //获取收费员列表
            DataTable dt = new DataTable();
            BasicDataManagement basicmanagement = NewObject<BasicDataManagement>();
            dt = basicmanagement.GetBasicData(HIS_PublicManage.ObjectModel.EmpDataSourceType.收费员, false);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 查找所有已经缴款和未缴款记录
        /// </summary>
        /// <returns>所有已经缴款和未缴款记录</returns>
        [WCFMethod]
        public ServiceResponseData QueryAllAccount()
        {
            DateTime bdate = requestData.GetData<DateTime>(0);
            DateTime edate = requestData.GetData<DateTime>(1);
            int empid = requestData.GetData<int>(2);
            int status = requestData.GetData<int>(3);

            DataTable dtAllAccountData = NewObject<AccountManagement>().QueryAllAccount(bdate, edate, empid, status);
            DataTable dtAllNotAccountData = NewObject<AccountManagement>().QueryAllNotAccount(bdate, edate, empid);

            responseData.AddData(dtAllAccountData);
            responseData.AddData(dtAllNotAccountData);
            return responseData;
        }
       
        /// <summary>
        /// 收款
        /// </summary>
        /// <returns>收款成功或失败</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData ReciveAccount()
        {
            string accountid = requestData.GetData<string>(0);
            int operatoreid = requestData.GetData<int>(1);

            List<IP_Account> accountList = NewObject<IP_Account>().getlist<IP_Account>(" accountid in (" + accountid + ")");
            foreach (IP_Account ipAccount in accountList)
            {
                if (ipAccount.ReceivFlag == 1)
                {
                    throw new Exception("缴款单号为【" + ipAccount.ReceivBillNO + "】的缴款记录已经收款，不能再收款");
                }

                ipAccount.ReceivFlag = 1;
                ipAccount.ReceivEmpID = operatoreid;
                ipAccount.ReceivDate = DateTime.Now;
                this.BindDb(ipAccount);
                ipAccount.save();
            }

            responseData.AddData(true);
            return responseData;
        }
        #endregion
    }
}
