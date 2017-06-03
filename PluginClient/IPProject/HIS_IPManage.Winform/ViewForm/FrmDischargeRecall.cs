using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.IPManage;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    public partial class FrmDischargeRecall : BaseFormBusiness, IDischargeRecall
    {
        /// <summary>
        /// 发票补打记录对象
        /// </summary>
        private IP_PrintInvoiceInfo mPrintInvoiceInfo = new IP_PrintInvoiceInfo();

        /// <summary>
        /// 发票补打记录对象
        /// </summary>
        public IP_PrintInvoiceInfo PrintInvoiceInfo
        {
            get
            {
                return mPrintInvoiceInfo;
            }

            set
            {
                mPrintInvoiceInfo = value;
            }
        }

        /// <summary>
        /// 收费开始时间
        /// </summary>
        public DateTime CostBeginDate
        {
            get
            {
                return sdtTime.Bdate.Value;
            }
        }

        /// <summary>
        /// 收费结束时间
        /// </summary>
        public DateTime CostEndDate
        {
            get
            {
                return sdtTime.Edate.Value;
            }
        }

        /// <summary>
        /// 检索条件
        /// </summary>
        public string SqlectParam
        {
            get
            {
                return txtSelectParam.Text.Trim();
            }
        }

        /// <summary>
        /// 收费员ID
        /// </summary>
        public int EmpId
        {
            get
            {
                return int.Parse(txtEmpList.MemberValue.ToString());
            }
        }

        /// <summary>
        /// 结算状态
        /// </summary>
        public int Status
        {
            get
            {
                if (rdoNormal.Checked)
                {
                    return 1;
                }
                else if (rdoToVoid.Checked)
                {
                    return 2;
                }

                return 0;
            }
        }

        /// <summary>
        /// 是否缴款
        /// </summary>
        public int IsAccount
        {
            get
            {
                if (rdoNoPayment.Checked)
                {
                    return 1;
                }
                else if (rdoAlreadyPaid.Checked)
                {
                    return 2;
                }

                return 0;
            }
        }

        /// <summary>
        /// 结算类型
        /// </summary>
        public string CostType
        {
            get
            {
                string status = string.Empty;  // 病人状态
                if (chkNormalCost.Checked)
                {
                    status = "2,";
                }

                if (chkHalfwayCost.Checked)
                {
                    status += "1,";
                }

                if (chkArrearsCost.Checked)
                {
                    status += "3,";
                }

                if (status.Length >= 2)
                {
                    status = status.Substring(0, status.Length - 1);
                }

                return status;
            }
        }

        /// <summary>
        /// 选中结算记录的病人登记ID
        /// </summary>
        public int PatListID
        {
            get
            {
                if (grdFeeList.CurrentCell != null)
                {
                    int rowIndex = grdFeeList.CurrentCell.RowIndex;
                    DataTable tempDt = grdFeeList.DataSource as DataTable;
                    return Convert.ToInt32(tempDt.Rows[rowIndex]["PatListID"]);
                }

                return 0;
            }
        }

        /// <summary>
        /// 选中结算记录的结算ID
        /// </summary>
        public int CostHeadID
        {
            get
            {
                if (grdFeeList.CurrentCell != null)
                {
                    int rowIndex = grdFeeList.CurrentCell.RowIndex;
                    DataTable tempDt = grdFeeList.DataSource as DataTable;
                    return Convert.ToInt32(tempDt.Rows[rowIndex]["CostHeadID"]);
                }

                return 0;
            }
        }

        /// <summary>
        /// 取选中结算记录的消结算类型
        /// </summary>
        public int CancelCostType
        {
            get
            {
                if (grdFeeList.CurrentCell != null)
                {
                    int rowIndex = grdFeeList.CurrentCell.RowIndex;
                    DataTable tempDt = grdFeeList.DataSource as DataTable;
                    return Convert.ToInt32(tempDt.Rows[rowIndex]["CostType"]);
                }

                return 0;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmDischargeRecall()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体打开前加载数据
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmDischargeRecall_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("GetCashier");
            sdtTime.Bdate.Value = DateTime.Now;
            sdtTime.Edate.Value = DateTime.Now;
            tabControl1.SelectedTabIndex = 0;
            bindGridSelectIndex(grdFeeList);
            bindGridSelectIndex(grdDepositList);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTabIndex == 0)
            {
                InvokeController("GetCostFeeList");
            }
            else
            {
                InvokeController("GetAllDepositList");
            }
        }

        /// <summary>
        /// Tab选择禁用按钮
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void tabControl1_SelectedTabChanged(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
        {
            if (tabControl1.SelectedTabIndex == 1)
            {
                btnCancelSettlement.Enabled = false;
                btnInvoiceFill.Enabled = false;
                btnPrintReimbursement.Enabled = false;
                chkNormalCost.Enabled = false;
                chkHalfwayCost.Enabled = false;
                chkArrearsCost.Enabled = false;
            }
            else
            {
                btnCancelSettlement.Enabled = true;
                btnInvoiceFill.Enabled = true;
                btnPrintReimbursement.Enabled = true;
                chkNormalCost.Enabled = true;
                chkHalfwayCost.Enabled = true;
                chkArrearsCost.Enabled = true;
            }
        }

        /// <summary>
        /// 发票补打
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnInvoiceFill_Click(object sender, EventArgs e)
        {
            if (grdFeeList.CurrentCell == null)
            {
                InvokeController("MessageShow", "请选择要补打的记录！");
                return;
            }
            else
            {
                int rowIndex = grdFeeList.CurrentCell.RowIndex;
                DataTable tempDt = grdFeeList.DataSource as DataTable;

                int status = Convert.ToInt32(tempDt.Rows[rowIndex]["Status"]);
                if (status != 0)
                {
                    InvokeController("MessageShow", "当前记录已取消结算，无法补打发票！");
                    return;
                }
                //m_PrintInvoiceInfo.FeeItemHeadID = Convert.ToInt16(TempDt.Rows[rowIndex]["CostHeadID"]);
                mPrintInvoiceInfo.OldInvoiceNumber = tempDt.Rows[rowIndex]["InvoiceNO"].ToString();
                mPrintInvoiceInfo.PrintDate = DateTime.Now;
                mPrintInvoiceInfo.InvoiceFee = Convert.ToDecimal(tempDt.Rows[rowIndex]["TotalFee"]);
                mPrintInvoiceInfo.PatListID = Convert.ToInt32(tempDt.Rows[rowIndex]["PatListID"]);
                mPrintInvoiceInfo.PatName = tempDt.Rows[rowIndex]["PatName"].ToString();
                mPrintInvoiceInfo.PrintType = 0;
                mPrintInvoiceInfo.CostHeadID = Convert.ToInt16(tempDt.Rows[rowIndex]["CostHeadID"]);
            }

            InvokeController("ShowInvoiceFill");
        }

        /// <summary>
        /// 打印报销单
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnPrintReimbursement_Click(object sender, EventArgs e)
        {
            if (grdFeeList.CurrentCell == null)
            {
                InvokeController("MessageShow", "请选择要打印报销单的记录！");
                return;
            }
            else
            {
                int rowIndex = grdFeeList.CurrentCell.RowIndex;
                DataTable tempDt = grdFeeList.DataSource as DataTable;
                int status = Convert.ToInt32(tempDt.Rows[rowIndex]["Status"]);
                if (status != 0)
                {
                    InvokeController("MessageShow", "当前记录已取消结算，无法打印报销单！");
                    return;
                }
            }

            InvokeController("PrintPatientFeeInfo");
        }

        /// <summary>
        /// 取消结算
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnCancelSettlement_Click(object sender, EventArgs e)
        {
            if (grdFeeList.CurrentCell == null)
            {
                InvokeController("MessageShow", "请选择要取消结算的记录！");
                return;
            }
            else
            {
                int rowIndex = grdFeeList.CurrentCell.RowIndex;
                DataTable tempDt = grdFeeList.DataSource as DataTable;
                int status = Convert.ToInt32(tempDt.Rows[rowIndex]["Status"]);
                if (status != 0)
                {
                    InvokeController("MessageShow", "当前记录已取消结算，请不要重复操作！");
                    return;
                }
            }

            InvokeController("CancelSettlement");
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 绑定收费员列表
        /// </summary>
        /// <param name="cashierDt">收费员列表</param>
        public void Bind_CashierList(DataTable cashierDt)
        {
            txtEmpList.MemberField = "EmpId";
            txtEmpList.DisplayField = "Name";
            txtEmpList.CardColumn = "Name|名称|auto";
            txtEmpList.QueryFieldsString = "Name,Pym,Wbm,Szm";
            txtEmpList.ShowCardWidth = 350;
            txtEmpList.ShowCardDataSource = cashierDt;
            txtEmpList.MemberValue = -1;
        }

        /// <summary>
        /// 绑定已结算费用列表
        /// </summary>
        /// <param name="feeList">已结算费用列表</param>
        public void Bind_CostFeeList(DataTable feeList)
        {
            grdFeeList.DataSource = feeList;
            if (feeList != null && feeList.Rows.Count > 0)
            {
                setGridSelectIndex(grdFeeList);
                SetFeeGridColor();
            }
        }

        /// <summary>
        /// 绑定住院押金列表
        /// </summary>
        /// <param name="depositList">住院押金列表</param>
        public void Bind_DepositList(DataTable depositList)
        {
            grdDepositList.DataSource = depositList;
            if (depositList != null && depositList.Rows.Count > 0)
            {
                setGridSelectIndex(grdDepositList);
                SetDepositListGridColor();
            }
        }

        /// <summary>
        /// 设置费用网格显示颜色
        /// </summary>
        public void SetFeeGridColor()
        {
            foreach (DataGridViewRow row in grdFeeList.Rows)
            {
                short status_flag = Convert.ToInt16(row.Cells["CostStatus"].Value);
                Color foreColor = Color.Blue;
                switch (status_flag)
                {
                    case 1:
                        foreColor = Color.Green;
                        break;
                    case 2:
                        foreColor = Color.Red;
                        break;
                }

                grdFeeList.SetRowColor(row.Index, foreColor, true);
            }
        }

        /// <summary>
        /// 设置预交金网格显示颜色
        /// </summary>
        public void SetDepositListGridColor()
        {
            foreach (DataGridViewRow row in grdDepositList.Rows)
            {
                short status_flag = Convert.ToInt16(row.Cells["States"].Value);

                Color foreColor = Color.Blue;
                switch (status_flag)
                {
                    case 1:
                        foreColor = Color.Green;
                        break;
                    case 2:
                        foreColor = Color.Red;
                        break;
                }

                grdDepositList.SetRowColor(row.Index, foreColor, true);
            }
        }
    }
}
