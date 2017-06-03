using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.DrugManage;

namespace HIS_DrugManger.ObjectModel.Account
{
    /// <summary>
    /// 药品付款处理器
    /// </summary>
    public class DrugPay : AbstractObjectModel
    {
        /// <summary>
        /// 加载已付款记录
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>已付款记录</returns>
        public DataTable LoadPayRecord(Dictionary<string, string> condition)
        {
            return null;
        }

        /// <summary>
        /// 药品入库单单付款
        /// </summary>
        /// <param name="lstInstoreHead">入库单表头列表</param>
        /// <param name="invoiceNO">发票号</param>
        /// <param name="empId">付款人ID</param>
        /// <param name="payDate">开票日期</param>
        /// <returns>入库单单付款</returns>
        public int PayInstore(List<DS_InstoreHead> lstInstoreHead, string invoiceNO, int empId, DateTime payDate)
        {
            return 0;
        }
    }
}
