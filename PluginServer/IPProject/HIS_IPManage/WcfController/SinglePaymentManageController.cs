using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.IPManage;
using HIS_IPManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPManage.WcfController
{
    /// <summary>
    /// 交款控制器
    /// </summary>
    [WCFController]
    public class SinglePaymentManageController : WcfServerController
    {
        /// <summary>
        /// 获取收费员列表
        /// </summary>
        /// <returns>收费员列表</returns>
        [WCFMethod]
        public ServiceResponseData GetStaff()
        {
            DataTable dt = new DataTable();
            BasicDataManagement basicmanagement = NewObject<BasicDataManagement>();
            dt = basicmanagement.GetBasicData(HIS_PublicManage.ObjectModel.EmpDataSourceType.收费员, true);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取树菜单的缴款记录
        /// </summary>
        /// <returns>缴款记录</returns>
        [WCFMethod]
        public ServiceResponseData GetAccountList()
        {
            int iWorkId = requestData.GetData<int>(0);
            string sBDate = requestData.GetData<string>(1);
            string sEDate = requestData.GetData<string>(2);
            int iEmpId = requestData.GetData<int>(3);

            DataTable dtUploaded = NewDao<ISinglePaymentManageDao>().GetAccountListUploaded(iWorkId,  sBDate,  sEDate,  iEmpId);
            DataTable dtUnUpload = NewDao<ISinglePaymentManageDao>().GetAccountListUnUpload(iWorkId,  iEmpId);
            responseData.AddData(dtUnUpload);
            responseData.AddData(dtUploaded);
            return responseData;
        }

        /// <summary>
        /// 获取每条缴款单的发票总数、发票分类、账目分类
        /// </summary>
        /// <returns>缴款单信息</returns>
        [WCFMethod]
        public ServiceResponseData GetPayMentItem()
        {
            int iWorkId = requestData.GetData<int>(0);
            int iStaffId = requestData.GetData<int>(1);
            int iAccountId = requestData.GetData<int>(2);

            DataTable dtFPSum = NewDao<ISinglePaymentManageDao>().GetPayMentItemFPSum(iWorkId, iStaffId, iAccountId);
            DataTable dtFPClass = NewDao<ISinglePaymentManageDao>().GetPayMentItemFPClass(iWorkId, iStaffId,iAccountId);
            DataTable dtAccountClass = NewDao<ISinglePaymentManageDao>().GetPayMentItemAccountClass(iWorkId, iStaffId, iAccountId);
            IP_Account account = NewDao<ISinglePaymentManageDao>().GetAccount(iAccountId);
            responseData.AddData(dtFPSum);
            responseData.AddData(dtFPClass);
            responseData.AddData(dtAccountClass);
            responseData.AddData(account);
            return responseData;
        }

        /// <summary>
        /// 获取每条缴款单的发票总数、发票分类、账目分类
        /// </summary>
        /// <returns>缴款单信息</returns>
        [WCFMethod]
        public ServiceResponseData GetDepositList()
        {
            int iWorkId = requestData.GetData<int>(0);
            int iStaffId = requestData.GetData<int>(1);
            int iAccountId = requestData.GetData<int>(2);

            DataTable dtDepositList = NewDao<ISinglePaymentManageDao>().GetDepositList(iWorkId, iStaffId, iAccountId);
            IP_Account account = NewDao<ISinglePaymentManageDao>().GetAccount(iAccountId);
            responseData.AddData(dtDepositList);
            responseData.AddData(account);
            return responseData;
        }

        /// <summary>
        /// 执行缴款
        /// </summary>
        /// <returns>true：缴款成功</returns>
        [WCFMethod]
        public ServiceResponseData DoAccountPayment()
        {
            try
            {
                int iWorkId = requestData.GetData<int>(0);
                int iStaffId = requestData.GetData<int>(1);
                int iAccountType = requestData.GetData<int>(2);
                decimal dTotalFee = requestData.GetData<decimal>(3);
                decimal dTotalPaymentFee = requestData.GetData<decimal>(4);
                int iErrcode = 0;
                string sErrmsg = string.Empty;
                IP_Account account = NewDao< ISinglePaymentManageDao>().AccountPayment(iWorkId, iStaffId, iAccountType, dTotalFee, dTotalPaymentFee, out iErrcode, out sErrmsg);                
                responseData.AddData(iErrcode==0);
                responseData.AddData(account);
                return responseData;
            }
            catch (Exception e)
            {
                responseData.AddData(false);
                responseData.AddData(e.Message);
                return responseData;
            }
        }

        /// <summary>
        /// 获取发票明细
        /// </summary>
        /// <returns>发票明细</returns>
        [WCFMethod]
        public ServiceResponseData GetInvoiceDetail()
        {
            int iWorkId = requestData.GetData<int>(0);
            int invoiceID = requestData.GetData<int>(1);
            int invoiceType = requestData.GetData<int>(2);
            int accountid = requestData.GetData<int>(3);
            DataTable dtInvoiceDetail = NewDao<ISinglePaymentManageDao>().GetInvoiceDetail(iWorkId, invoiceID, invoiceType, accountid);
            responseData.AddData(dtInvoiceDetail);
            return responseData;
        }
    }
}