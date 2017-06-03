using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.BasicData;
using HIS_OPManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_OPManage.WcfController
{
    /// <summary>
    /// 票据管理控制器类
    /// </summary>
    [WCFController]
    public class InvoiceController : WcfServerController
    {
        /// <summary>
        /// 获取票据信息
        /// </summary>
        /// <returns>返回票据信息</returns>
        [WCFMethod]
        public ServiceResponseData GetInvoices()
        {         
            DataTable dt = NewDao<IOPManageDao>().GetInvoices();
            responseData.AddData(dt);
            return responseData;       
        }

        /// <summary>
        /// 停用标据
        /// </summary>
        /// <returns>true成功false失败</returns>
        [WCFMethod]
        public ServiceResponseData StopInvoice()
        {
            int invoiceId = requestData.GetData<int>(0);
            HIS_Entity.BasicData.Basic_Invoice currInvoice = (Basic_Invoice)NewObject<Basic_Invoice>().getmodel(invoiceId);
            if (currInvoice != null)
            {
                if (currInvoice.EndNO == currInvoice.CurrentNO
                    && currInvoice.Status == 1)
                {
                    throw new Exception("本卷发票已经使用完，不能再停用！");
                }

                currInvoice.Status = 3;             
                currInvoice.save();
                responseData.AddData(true);
            }
            else
            {
                responseData.AddData(false);
            }         
           
            return responseData;
        }

        /// <summary>
        /// 删除票据
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        public ServiceResponseData DeleteInvoice()
        {
            int invoiceId = requestData.GetData<int>(0);
            HIS_Entity.BasicData.Basic_Invoice invoice = (Basic_Invoice)NewObject<Basic_Invoice>().getmodel(invoiceId);
            if (invoice != null)
            {
                if (invoice.Status == 0)
                {
                    throw new Exception("该卷发票正在使用中，不能删除");
                }

                if (invoice.Status == 1)
                {
                    throw new Exception ("该卷发票已经有使用记录，不能删除");
                }

                if (invoice.Status == 3 && invoice.StartNO != invoice.CurrentNO)
                {
                    throw new Exception("该卷发票已停用，但有部分票据已经使用过，不能删除！\r\n如果要使用未用的票据号，请将这段票据号重新分配");
                }
            }  
                    
            invoice.delete();
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 检查新增票据
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        public ServiceResponseData CheckInvoiceExsist()
        {         
            int invoicetype=requestData.GetData<int>(0);
            string perfchar = requestData.GetData<string>(1);
            int start = requestData.GetData<int>(2);
            int end = requestData.GetData<int>(3);
            List<Basic_Invoice> invoices = NewObject<Basic_Invoice>().getlist<Basic_Invoice>(" invoicetype="+invoicetype+" and perfchar='"+perfchar+"'");
            for (int i = 0; i < invoices.Count; i++)
            {
                switch (invoices[i].Status)
                {
                    case 0:
                        //在用状态,比较范围该卷开始号到结束号
                        if (start >= invoices[i].StartNO && start <= invoices[i].EndNO)
                        {
                            throw new  Exception("输入的开始号" + start + "已经包含在第" + invoices[i].ID + "卷中,并且当前正在使用");
                        }

                        if (end >= invoices[i].StartNO && end <= invoices[i].EndNO)
                        {
                            throw new  Exception("输入的结束号" + start + "已经包含在第" + invoices[i].ID + "卷中,并且当前正在使用");
                        }

                        break;
                    case 1:
                        //用完状态,比较范围该卷开始号到结束号
                        if (start >= invoices[i].StartNO && start <= invoices[i].EndNO)
                        {
                            throw new  Exception("输入的开始号" + start + "已经包含在第" + invoices[i].ID + "卷中,并已经使用过");
                        }

                        if (end >= invoices[i].StartNO && end <= invoices[i].EndNO)
                        {
                            throw new Exception("输入的结束号" + start + "已经包含在第" + invoices[i].ID + "卷中,并已经使用过");
                        }

                        break;
                    case 2:

                        //备用状态,比较范围该卷开始号到结束号
                        if (start >= invoices[i].StartNO && start <= invoices[i].EndNO)
                        {
                            throw new Exception ("输入的开始号" + start + "已经包含在第" + invoices[i].ID + "卷备用票据中");
                        }

                        if (end >= invoices[i].StartNO && end <= invoices[i].EndNO)
                        {
                            throw new Exception("输入的结束号" + start + "已经包含在第" + invoices[i].ID + "卷备用票据中");
                        }

                        break;
                    case 3:
                        //停用状态,比较范围该卷开始号到停用时的当前号
                        if (start >= invoices[i].StartNO && start <= invoices[i].CurrentNO)
                        {
                            throw new Exception("输入的开始号" + start + "已经包含在第" + invoices[i].ID + "卷停用的票据中，如果要分配的票据号在停用卷的未使用号段中，请重新分配");
                        }

                        if (end >= invoices[i].StartNO && end <= invoices[i].CurrentNO)
                        {
                            throw new Exception ("输入的结束号" + start + "已经包含在第" + invoices[i].ID + "卷停用的票据中，如果要分配的票据号在停用卷的未使用号段中，请重新分配");
                        }

                        break;
                }
            }

            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 保存新增票据
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        public ServiceResponseData SaveNewAllot()
        {
            Basic_Invoice invoice = requestData.GetData<Basic_Invoice>(0);
            invoice.AllotDate = DateTime.Now;                  
            this.BindDb(invoice);
            invoice.save();
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 获取票据使用信息
        /// </summary>
        /// <returns>票据使用信息</returns>
        [WCFMethod]
        public ServiceResponseData GetInvoiceListInfo()
        {
            int invoicetype = requestData.GetData<int>(0);
            string perferchar = requestData.GetData<string>(1);
            string start = requestData.GetData<string>(2);
            string end = requestData.GetData<string>(3);

            decimal totalMoney = 0;
            int totalCount = 0;
            decimal refundMoney = 0;
            int refundCount = 0;
            if (invoicetype < 2)
            {
                NewDao<IOPManageDao>().GetInvoiceListInfo(perferchar, start, end, out totalMoney, out totalCount, out refundMoney, out refundCount);
            }

            responseData.AddData(totalMoney);
            responseData.AddData(totalCount);
            responseData.AddData(refundMoney);
            responseData.AddData(refundCount);
            return responseData;
        }

        /// <summary>
        /// 获取所有收费员
        /// </summary>
        /// <returns>返回收费员列表</returns>
        [WCFMethod]
        public ServiceResponseData LoadCashier()
        {
            DataTable dt = new DataTable();
            BasicDataManagement basicmanagement = NewObject<BasicDataManagement>();
            dt = basicmanagement.GetBasicData(HIS_PublicManage.ObjectModel.EmpDataSourceType.收费员, false);
            responseData.AddData(dt);
            return responseData;
        }
    }
}