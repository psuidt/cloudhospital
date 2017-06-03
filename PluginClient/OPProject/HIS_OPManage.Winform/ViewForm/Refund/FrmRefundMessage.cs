using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    public partial class FrmRefundMessage : BaseFormBusiness,IFrmRefundMessage
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmRefundMessage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 退费处方信息
        /// </summary>
        public DataTable DtRefundPresc
        {
            get
            {
                return (DataTable)dgRefundPresc.DataSource;
            }

            set
            {
                dgRefundPresc.DataSource = value;
                SetRefundGridColor();
            }
        }

        /// <summary>
        /// 医技项目退费表
        /// </summary>
        public DataTable DtRefundMedical
        {
            get
            {
                return (DataTable)dgMedicalRefund.DataSource;
            }

            set
            {
                dgMedicalRefund.DataSource = value;
            }
        }

        /// <summary>
        /// 原票据总金额
        /// </summary>
        public string OldtotalFee
        {
            set
            {
                lblTotalFee.Text = value;
            }
        }

        /// <summary>
        /// 退费消息查询界面
        /// </summary>
        public DataTable dtQueryRefundPresc
        {
            get
            {
                return (DataTable)dgQueryMessage.DataSource;
            }

            set
            {
                dgQueryMessage.DataSource = value;
            }
        }

        /// <summary>
        /// 显示病人信息
        /// </summary>
        /// <param name="row">病人信息</param>
        public void SetPatInfo(DataRow row)
        {
            if (row == null)
            {
                lblBackInvoiceNO.Text = string.Empty;
                lblPatName.Text = string.Empty;
                lblCostType.Text = string.Empty;
                lblTotalFee.Text = string.Empty;
                lblPresDept.Text = string.Empty;
                lblChargeDate.Text = string.Empty;
            }
            else
            {
                lblBackInvoiceNO.Text = row["invoiceNO"].ToString();
                lblPatName.Text = row["patname"].ToString();
                lblCostType.Text = row["PatTypeName"].ToString();
                string money= Convert.ToDecimal(row["totalFee"]).ToString("0.00");
                lblTotalFee.Text = money;
                lblPresDept.Text = row["presDeptName"].ToString();
                lblChargeDate.Text =Convert.ToDateTime( row["ChargeDate"]).ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// 设置只读
        /// </summary>
        /// <param name="type">0退基本数只读1退包装数只读2已发药中药不能退3未发药中药可能按付退 4未发药开了包装数和基本数的</param>
        public void SetReadOnly(int type)
        {
            RefundMiniNum.ReadOnly = true;
            RefundPackNum.ReadOnly = true;
            RefundPresAmount.ReadOnly = true;
            //if (type == 0)
            //{
            //    //退基本数不能输
            //    RefundMiniNum.ReadOnly = true;
            //    RefundPackNum.ReadOnly = false;
            //    RefundPresAmount.ReadOnly = true;
            //}
            //else if (type == 1)
            //{
            //    //退包装数不能输
            //    RefundPackNum.ReadOnly = true;
            //    RefundMiniNum.ReadOnly = false;
            //    RefundPresAmount.ReadOnly = true;
            //}
            //else if (type == 2)
            //{
            //    //已发药中药不能退
            //    RefundPackNum.ReadOnly = true;
            //    RefundMiniNum.ReadOnly = true;
            //    RefundPresAmount.ReadOnly = true;
            //}
            //else if (type == 3)
            //{
            //    //未发药中药可能按付退
            //    RefundPackNum.ReadOnly = true;
            //    RefundMiniNum.ReadOnly = true;
            //    RefundPresAmount.ReadOnly = false;
            //}
            //else if (type == 4)
            //{
            //    //未发药开了包装数和基本数的
            //    RefundPackNum.ReadOnly = false;
            //    RefundMiniNum.ReadOnly = false;
            //    RefundPresAmount.ReadOnly = false;
            //}
        }

        /// <summary>
        /// 票据号回车事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void txtBackInvoiceNO_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtBackInvoiceNO.Text.Trim() != string.Empty)
                {
                    InvokeController("QueryPresByInvoiceNO",txtBackInvoiceNO.Text.Trim());
                    txtBackInvoiceNO.Clear();
                }
            }
        }

        /// <summary>
        /// 窗体OpenWindowBefore事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmRefundMessage_OpenWindowBefore(object sender, EventArgs e)
        {
            superTabControl1.SelectedTab = tabRefund;
            SetPatInfo(null);          
            dgRefundPresc.DataSource = new DataTable();
            dtTime.Bdate.Value =Convert.ToDateTime( DateTime.Now.ToString("yyyy-MM-dd"));
            dtTime.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            dgQueryMessage.DataSource = new DataTable();
            txtQueryCondition.Text = string.Empty;
            superTabControl2.SelectedTabIndex = 0;
        }

        /// <summary>
        /// 窗体Shown事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmRefundMessage_Shown(object sender, EventArgs e)
        {
            txtBackInvoiceNO.Focus();
        }

        /// <summary>
        /// 网格当前单元格改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void dgRefundPresc_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgRefundPresc.CurrentCell != null)
            {
                int currentRowIndex = dgRefundPresc.CurrentCell.RowIndex;
                InvokeController("SetReadOnly", currentRowIndex);
            }
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
               txtBackInvoiceNO.Focus();
                InvokeController("SaveReundMessage");
        }

        /// <summary>
        /// 网格CellValueChanged事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgRefundPresc_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgRefundPresc.CurrentCell != null)
            {
                if (e.ColumnIndex == dgRefundPresc.Columns[RefundPresAmount.Name].Index)
                {
                    InvokeController("SetRefundPresNums", e.RowIndex);
                    dgRefundPresc.Refresh();
                }
                else if (e.ColumnIndex == dgRefundPresc.Columns[RefundPackNum.Name].Index || e.ColumnIndex == dgRefundPresc.Columns[RefundMiniNum.Name].Index)
                {
                    dgRefundPresc.BeginEdit(true);
                    InvokeController("CalculateRefundTotalFee", e.RowIndex);
                    dgRefundPresc.Refresh();
                }
            }
        }

        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtBackInvoiceNO.Text.Trim() != string.Empty)
            {
                InvokeController("QueryPresByInvoiceNO", txtBackInvoiceNO.Text.Trim());
                txtBackInvoiceNO.Clear();
            }
        }

        /// <summary>
        /// 刷新按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (lblBackInvoiceNO.Text.Trim() != string.Empty)
            {
                InvokeController("QueryPresByInvoiceNO", lblBackInvoiceNO.Text.Trim());
            }
        }

        /// <summary>
        /// 设置网格颜色
        /// </summary>
        private void SetRefundGridColor()
        {
            if (dgRefundPresc != null && dgRefundPresc.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgRefundPresc.Rows)
                {
                    string status_flag = row.Cells["ItemName"].Value.ToString();
                    Color foreColor = Color.Black;
                    if (status_flag == "小   计")
                    {
                        foreColor = Color.Blue;
                    }

                    dgRefundPresc.SetRowColor(row.Index, foreColor, true);
                }
            }
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 退费消息查询
        /// <summary>
        /// 退出按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnQuite_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 退费消息查询按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            DateTime bdate = Convert.ToDateTime(dtTime.Bdate.Value.ToString("yyyy-MM-dd 00:00:00"));
            DateTime edate = Convert.ToDateTime(dtTime.Edate.Value.ToString("yyyy-MM-dd 23:59:59"));
            string queryCondition = txtQueryCondition.Text.Trim();
            InvokeController("QueryRefundMessage", bdate, edate, queryCondition);
            SetGridColor();
        }

        /// <summary>
        /// 设置网格颜色
        /// </summary>
        private void SetGridColor()
        {
           if (dgQueryMessage != null && dgQueryMessage.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgQueryMessage.Rows)
                {
                    string status_flag = row.Cells["refundpackAmount"].Value.ToString();
                    Color foreColor = Color.Black;
                    if (status_flag == "小   计")
                    {
                        foreColor = Color.Blue;
                    }

                    dgQueryMessage.SetRowColor(row.Index, foreColor, true);
                }
            }
        }

        /// <summary>
        /// 查询条件KeyUp事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void txtQueryCondition_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtQueryCondition.Text.Trim() != string.Empty)
                {
                    btnQuery.Focus();
                }
            }
        }

        /// <summary>
        /// 删除按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgQueryMessage.CurrentCell != null)
            {
                int rowIndex = dgQueryMessage.CurrentCell.RowIndex;
                if ((bool)InvokeController("DeleteRefundMessage", rowIndex))
                {
                    btnQuery_Click(null, null);
                }
            }
        }
        #endregion

        /// <summary>
        /// 网格DataError事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgRefundPresc_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == RefundPackNum.Index || e.ColumnIndex == RefundMiniNum.Index || e.ColumnIndex == RefundPresAmount.Index)
            {
                dgRefundPresc.Rows[e.RowIndex].ErrorText = "输入数字格式不正确";
                dgRefundPresc.CancelEdit();
            }           
        }

        /// <summary>
        /// 项目退费网格CellClick事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgMedicalRefund_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgMedicalRefund.Rows.Count > 0)
            {
                if (e.ColumnIndex == 0)
                {
                    int rowIndex = dgMedicalRefund.CurrentCell.RowIndex;
                    DataTable dt = dgMedicalRefund.DataSource as DataTable;                  
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dt.Rows[rowIndex]["Sel"]) == 1)
                        {
                            dt.Rows[rowIndex]["Sel"] = 0;
                        }
                        else
                        {
                            if (Convert.ToInt32(dt.Rows[rowIndex]["DistributeFlag"]) == 1)
                            {
                                InvokeController("ShowMess", "已经确费项目请先取消确费再退费");
                                return;
                            }

                            dt.Rows[rowIndex]["Sel"] = 1;
                        }
                    }
                }
            }
        }
    }
}