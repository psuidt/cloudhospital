using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.OPManage;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    ///门诊缴款界面接口类
    /// </summary>
    public interface IFrmAccount:IBaseView
    {
        /// <summary>
        /// 绑定收费员
        /// </summary>
        /// <param name="dtCashier">收费员数据</param>
        void loadCashier(DataTable dtCashier);

        /// <summary>
        /// 构造树
        /// </summary>
        /// <param name="notAccountList">未缴款数据</param>
        /// <param name="historyAccountList">历史缴款数据</param>
        void BindTree(List<OP_Account> notAccountList, List<OP_Account> historyAccountList);

        /// <summary>
        /// 当前缴款的人员ID
        /// </summary>
        int GetQueryEmpID { get; set; }

        /// <summary>
        /// 查询开始日期
        /// </summary>
        DateTime bdate { get; set; }

        /// <summary>
        /// 查询结束日期
        /// </summary>
        DateTime edate { get; set; }   
            
        /// <summary>
        /// 挂号费用
        /// </summary>
        string regFee { get; set; }

        /// <summary>
        /// 收费费用
        /// </summary>
        string balanceFee { get; set; }

        /// <summary>
        /// 绑定结账数据
        /// </summary>
        /// <param name="dtInvoiceData">发票数据</param>
        /// <param name="dtPaymentData">支付方式数据</param>
        /// <param name="dtItemData">项目分类数据</param>
        void BindAccountData(DataTable dtInvoiceData,DataTable dtPaymentData,DataTable dtItemData);

        /// <summary>
        /// 绑定充值数据
        /// </summary>
        /// <param name="dtME">充值数据</param>
        void BindAccountDataME(DataTable dtME);

        /// <summary>
        /// 缴款大写金额
        /// </summary>
        /// <param name="dxFee">大写金额</param>
        void SetAccountDxTotalFee( string dxFee);

        /// <summary>
        /// 当前缴款对象
        /// </summary>
         OP_Account CurAccount{ get; set; }
         
        /// <summary>
        /// 缴款票据详细信息
        /// </summary>
        DataTable dtAccountInvoiceData { get; set; }

        /// <summary>
        /// 缴款支付详细信息
        /// </summary>
        DataTable dtAccountPaymentData { get; set; }

        /// <summary>
        /// 缴款项目详细信息
        /// </summary>
        DataTable dtAccountItemData { get; set; }  
           
        /// <summary>
        /// 当前缴款Id
        /// </summary>
        int QueryAccountId { get; set; }

        /// <summary>
        /// 会员充值明细
        /// </summary>
        DataTable dtAccountItemDataME { get; set; }
    }
}
