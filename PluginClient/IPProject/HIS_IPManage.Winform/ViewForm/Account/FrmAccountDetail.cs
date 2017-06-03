using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    /// <summary>
    /// 缴款控界面
    /// </summary>
    public partial class FrmAccountDetail : BaseFormBusiness, IAccountDetail
    {
        /// <summary>
        /// 操作员ID
        /// </summary>
        private int iStaffId = 0;

        /// <summary>
        /// 缴款ID
        /// </summary>
        private int iAccountId = 0;

        /// <summary>
        /// 缴款类型
        /// </summary>
        private int iAccountType = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmAccountDetail()
        {
            InitializeComponent();
            ucAccountTab1.InitUC();
        }

        /// <summary>
        /// 绑定缴款数据
        /// </summary>
        /// <param name="iStaffId">操作员ID</param>
        /// <param name="iAccountId">缴款ID</param>
        /// <param name="iAccountType">缴款类型</param>
        public void BindAccountInfo(int iStaffId, int iAccountId, int iAccountType)
        {
            this.iStaffId = iStaffId;
            this.iAccountId = iAccountId;
            this.iAccountType = iAccountType;            

            ucAccountTab1.TabSelectIndex = iAccountType;
            ucAccountTab1.InvokeController = InvokeController;
        }

        /// <summary>
        /// 界面打开加载数据
        /// </summary>
        /// <param name="sender">FrmAccountDetail</param>
        /// <param name="e">事件参数</param>
        private void FrmAccountDetail_Load(object sender, EventArgs e)
        {
            if (iAccountType == 1)
            {
                InvokeController("GetDepositList", iStaffId, iAccountId);
            }
            else
            {
                InvokeController("GetPayMentItem", iStaffId, iAccountId);
            }
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="dtFPSum">发票总数</param>
        /// <param name="dtFPClass">发票类型</param>
        /// <param name="dtAccountClass">支付方式类型</param>
        public void ShowPayMentItem(DataTable dtFPSum, DataTable dtFPClass, DataTable dtAccountClass)
        {
            ucAccountTab1.AccountId = iAccountId;
            ucAccountTab1.DTInvCount = dtFPSum;
            ucAccountTab1.DTInvoiceDetail = dtFPClass;
            ucAccountTab1.DTAccount = dtAccountClass;
        }

        /// <summary>
        /// 显示预交金数据
        /// </summary>
        /// <param name="dtDepositList">预交金信息</param>
        public void ShowDepositItem(DataTable dtDepositList)
        {
            ucAccountTab1.AccountId = iAccountId;
            ucAccountTab1.DTDepositList = dtDepositList;
        }
    }
}
