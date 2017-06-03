using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    public partial class FrmOpCostSearch : BaseFormBusiness, IFrmOpCostSearch
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmOpCostSearch()
        {
            InitializeComponent();
        }

        #region 接口实现
        /// <summary>
        /// 绑定收费员
        /// </summary>
        /// <param name="dtCharger">收费员数据</param>
        public void BindCharge(DataTable dtCharger)
        {
            txtChareEmp.DisplayField = "Name";
            txtChareEmp.MemberField = "EmpID";
            txtChareEmp.CardColumn = "Name|姓名|auto";
            txtChareEmp.QueryFieldsString = "Name,pym,wbm";
            txtChareEmp.ShowCardHeight = 160;
            txtChareEmp.ShowCardWidth = 140;
            txtChareEmp.ShowCardDataSource = dtCharger;
        }

        /// <summary>
        /// 绑定病人类型
        /// </summary>
        /// <param name="dtPatType">病人类型数据</param>
        public void BindPatType(DataTable dtPatType)
        {
            cmbPatType.DataSource = dtPatType;
            cmbPatType.ValueMember = "PatTypeID";
            cmbPatType.DisplayMember = "PatTypeName";
        }

        /// <summary>
        /// 绑定科室
        /// </summary>
        /// <param name="dtDepts">科室数据</param>
        public void BindDepts(DataTable dtDepts)
        {
            txtPresDept.DisplayField = "Name";
            txtPresDept.MemberField = "deptid";
            txtPresDept.CardColumn = "Name|名称|auto";
            txtPresDept.QueryFieldsString = "Name,pym,wbm";
            txtPresDept.ShowCardHeight = 130;
            txtPresDept.ShowCardWidth = 140;
            txtPresDept.ShowCardDataSource = dtDepts;
        }

        /// <summary>
        /// 绑定医生
        /// </summary>
        /// <param name="dtDoctors">医生数据</param>
        public void BindDoctors(DataTable dtDoctors)
        {
            txtPresEmp.DisplayField = "Name";
            txtPresEmp.MemberField = "EmpID";
            txtPresEmp.CardColumn = "Name|姓名|60,DocProfessionName|职称|auto";
            txtPresEmp.QueryFieldsString = "Name,pym,wbm";
            txtPresEmp.ShowCardHeight = 160;
            txtPresEmp.ShowCardWidth = 140;
            txtPresEmp.ShowCardDataSource = dtDoctors;
        }

        /// <summary>
        /// 补打票据的结算ID
        /// </summary>
        private int printcostheadid;

        /// <summary>
        /// 补打票据的结算ID
        /// </summary>
        public int PrintCostHeadId
        {
            get
            {
                return printcostheadid;
            }

            set
            {
                printcostheadid = value;
            }
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        Dictionary<string, object> myDictionary = new Dictionary<string, object>();

        /// <summary>
        /// 查询条件
        /// </summary>
        public Dictionary<string, object> QueryDictionary
        {
            get
            {
                return myDictionary;
            }

            set
            {
                myDictionary = value;
            }
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public void ClearQuerySet()
        {
            radChargeDate.Checked = true;
            dtpBdate.Value =Convert.ToDateTime( DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpEdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            txtChareEmp.MemberValue = -1;
            txtPresDept.MemberValue = -1;
            txtPresEmp.MemberValue = -1;
            cmbPatType.SelectedValue = -1;
            radAllStatus.Checked = true;
            radAllRecord.Checked = true;
            txtQueryContent.Clear();
            txtBeInvoiceNO.Clear();
            txtEndInvoiceNO.Clear();
            dgPayMentInfo.DataSource = new DataTable();
            dgItemInfo.DataSource = new DataTable();
            lblPayMentInfo.Text = string.Empty;
            lblItemInfo.Text = string.Empty;
            expPayMentInfo.Expanded = false;
            expPayMentInfo.Expanded = false;
        }

        /// <summary>
        /// 绑定查询结果
        /// </summary>
        /// <param name="dtPayMentInfo">支付数据</param>
        /// <param name="dtItemInfo">项目数据</param>
        public void BindQueryData(DataTable dtPayMentInfo, DataTable dtItemInfo)
        {
            for (int i = 16; i < dgPayMentInfo.Columns.Count; i++)
            {
                dgPayMentInfo.Columns.RemoveAt(i);
            }

            if (dtPayMentInfo.Rows.Count == 0 || dtItemInfo.Rows.Count == 0)
            {
                dgPayMentInfo.DataSource = new DataTable();
                dgItemInfo.DataSource = new DataTable();
                lblPayMentInfo.Text = string.Empty;
                lblItemInfo.Text = string.Empty;
                return;
            }

            for (int i = 18; i < dtPayMentInfo.Columns.Count; i++)
            {
                if (!dgPayMentInfo.Columns.Contains(dtPayMentInfo.Columns[i].ColumnName))
                {
                    DataGridViewColumn col = new DataGridViewColumn();
                    col.HeaderText = dtPayMentInfo.Columns[i].ColumnName;
                    col.DataPropertyName = dtPayMentInfo.Columns[i].ColumnName;
                    col.Name = dtPayMentInfo.Columns[i].ColumnName;
                    DataGridViewCell dgvcell = new DataGridViewTextBoxCell();
                    col.CellTemplate = dgvcell;
                    col.DefaultCellStyle.Format = "N2";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;                    
                    dgPayMentInfo.Columns.Add(col);
                }
            }

            DataRow row = dtPayMentInfo.Rows[dtPayMentInfo.Rows.Count - 1];
            row[0] = DBNull.Value;
            row[2] = DBNull.Value;
            dgPayMentInfo.DataSource = dtPayMentInfo;           
            string strPayInfo = string.Empty;
            for (int i = 18; i < dtPayMentInfo.Columns.Count; i++)
            {
                strPayInfo += dtPayMentInfo.Columns[i].ColumnName + ":" + Convert.ToDecimal(row[dtPayMentInfo.Columns[i].ColumnName]).ToString("0.00")+"\n\n";
            }

            strPayInfo += "总 金 额:" + Convert.ToDecimal(row[dtPayMentInfo.Columns[16].ColumnName]).ToString("0.00");
            lblPayMentInfo.Text = strPayInfo;
            expPayMentInfo.Expanded = true;
            for (int i = 16; i < dgItemInfo.Columns.Count; i++)
            {
                dgItemInfo.Columns.RemoveAt(i);
            }

            for (int i = 18; i < dtItemInfo.Columns.Count; i++)
            {
                if (!dgItemInfo.Columns.Contains(dtItemInfo.Columns[i].ColumnName))
                {
                    DataGridViewColumn col = new DataGridViewColumn();
                    col.HeaderText = dtItemInfo.Columns[i].ColumnName;
                    col.DataPropertyName = dtItemInfo.Columns[i].ColumnName;
                    col.Name = dtItemInfo.Columns[i].ColumnName;
                    DataGridViewCell dgvcell = new DataGridViewTextBoxCell();
                    col.CellTemplate = dgvcell;
                    col.DefaultCellStyle.Format = "N2";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    col.Width = 80;
                    dgItemInfo.Columns.Add(col);
                }
            }

            DataRow itemRow = dtItemInfo.Rows[dtItemInfo.Rows.Count - 1];
            itemRow[0] = DBNull.Value;
            itemRow[2] = DBNull.Value;
            string strItemInfo = string.Empty;
            for (int i = 18; i < dtItemInfo.Columns.Count; i++)
            {
                strItemInfo += dtItemInfo.Columns[i].ColumnName + ":" + Convert.ToDecimal(itemRow[dtItemInfo.Columns[i].ColumnName]).ToString("0.00");
                if (i + 1 < dtItemInfo.Columns.Count)
                {
                    i += 1;
                    strItemInfo +="  "+ dtItemInfo.Columns[i].ColumnName + ":" + Convert.ToDecimal(itemRow[dtItemInfo.Columns[i].ColumnName]).ToString("0.00")+ "\n\n";
                }
            }

            strItemInfo +="\n"+"总金额:" + Convert.ToDecimal(itemRow[dtItemInfo.Columns[16].ColumnName]).ToString("0.00");
            lblItemInfo.Text = strItemInfo;
            expItemInfo.Expanded = true;
            dgItemInfo.DataSource = dtItemInfo;            
        }
        #endregion

        /// <summary>
        /// 设置支付明细网格颜色
        /// </summary>
        private void SetPayMentGridColor()
        {
            if (dgPayMentInfo != null && dgPayMentInfo.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgPayMentInfo.Rows)
                {
                    string status_flag = row.Cells["CostStatus"].Value.ToString();
                    Color foreColor = Color.Black;
                    switch (status_flag)
                    {
                        case "正常":
                            foreColor = Color.Black;
                            break;
                        case "被退费":
                            foreColor = Color.Red;
                            break;
                        case "退费":
                            foreColor = Color.Red;
                            break;
                    }

                    dgPayMentInfo.SetRowColor(row.Index, foreColor, true);
                }
            }
        }

        /// <summary>
        /// 设置项目明细网格颜色
        /// </summary>
        private void SetItemGridColor()
        {
            if (dgItemInfo != null && dgItemInfo.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgItemInfo.Rows)
                {
                    string status_flag = row.Cells["CostStatusItem"].Value.ToString();
                    Color foreColor = Color.Black;
                    switch (status_flag)
                    {
                        case "正常":
                            foreColor = Color.Black;
                            break;
                        case "被退费":
                            foreColor = Color.Red;
                            break;
                        case "退费":
                            foreColor = Color.Red;
                            break;
                    }

                    dgItemInfo.SetRowColor(row.Index, foreColor, true);
                }
            }
        }

        /// <summary>
        /// 查看明细
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnCostDetail_Click(object sender, EventArgs e)
        {
            int costHeadid = 0;
            if (TabControl.SelectedTabIndex == 0)
            {
                if (dgPayMentInfo.CurrentCell == null)
                {
                    return;
                }

                costHeadid = Convert.ToInt32( dgPayMentInfo[CostHeadID.Index, dgPayMentInfo.CurrentCell.RowIndex].Value);
            }
            else if (TabControl.SelectedTabIndex == 1)
            {
                if (dgItemInfo.CurrentCell == null)
                {
                    return;
                }

                costHeadid = Convert.ToInt32(dgItemInfo[CostHeadIDItem.Index, dgItemInfo.CurrentCell.RowIndex].Value);
            }

            InvokeController("CostSearchDetail", costHeadid);
        }

        /// <summary>
        /// 窗体OpenWindowBefore事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmOpCostSearch_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("OpCostSearchDataInit");
            ClearQuerySet();
        }

        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            myDictionary.Clear();
            myDictionary.Add("ChargeData", radChargeDate.Checked);
            myDictionary.Add("AccountData", radAccountDate.Checked);
            myDictionary.Add("Bdate",dtpBdate.Value);
            myDictionary.Add("Edate", dtpEdate.Value);
            myDictionary.Add("ChargerEmpID", txtChareEmp.Text.Trim() == string.Empty ? -1 : txtChareEmp.MemberValue);
            myDictionary.Add("PayTypeID", cmbPatType.Text.Trim() == string.Empty ? -1:cmbPatType.SelectedValue);
            myDictionary.Add("PresDeptID",txtPresDept.Text.Trim()==string.Empty?-1:txtPresDept.MemberValue);
            myDictionary.Add("PrsEmpID", txtPresEmp.Text.Trim() == string.Empty ? -1:txtPresEmp.MemberValue);
            myDictionary.Add("QueryCondition", txtQueryContent.Text.Trim());
            myDictionary.Add("BeInvoiceNO", txtBeInvoiceNO.Text.Trim());
            myDictionary.Add("EndInvoiceNO", txtEndInvoiceNO.Text.Trim());
            myDictionary.Add("AllSatus", radAllStatus.Checked);
            myDictionary.Add("NormalStatus", radNormalStatus.Checked);
            myDictionary.Add("RefundStatus", radRefundStatus.Checked);
            myDictionary.Add("AllRecord", radAllRecord.Checked);
            myDictionary.Add("BalanceRecord", radBalanceRecord.Checked);
            myDictionary.Add("RegRecord", radRegRecord.Checked);
            InvokeController("OpCostSearchQuery");
            SetPayMentGridColor();
            SetItemGridColor();
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 查询条件初始化事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnSetBegin_Click(object sender, EventArgs e)
        {
            ClearQuerySet();
        }

        /// <summary>
        /// 初始化SaveFileDialog
        /// </summary>
        /// <param name="sExportName">导出文件名</param>
        /// <param name="sSaveFullPath">导出文件路径</param>
        /// <returns>{确认导出：True;否则:false}</returns>
        public bool InitShowDialog(string sExportName,out string sSaveFullPath)
        {
            sSaveFullPath = string.Empty;
            SaveFileDialog dlgSave = new SaveFileDialog();
            dlgSave.FileName = sExportName;
            dlgSave.Filter = "Microsoft Excel|*.xls";
            if (dlgSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sSaveFullPath = dlgSave.FileName;               
                return true;
            }

            return false;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (TabControl.SelectedTabIndex == 0)
            {                
                if (dgPayMentInfo != null && dgPayMentInfo.Rows.Count > 0)
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    for (int i = 0; i < dgPayMentInfo.Columns.Count; i++)
                    {
                        if (dgPayMentInfo.Columns[i].Visible)
                        {
                            dictionary.Add(dgPayMentInfo.Columns[i].DataPropertyName, dgPayMentInfo.Columns[i].HeaderText);
                        }
                    }

                    string sSaveFullPath = string.Empty;
                    DataTable dt = (DataTable)dgPayMentInfo.DataSource;
                    if (InitShowDialog("病人费用查询（按支付方式)", out sSaveFullPath) == true)
                    {
                        EFWCoreLib.CoreFrame.Common.ExcelHelper.Export(dt, "病人费用查询（按支付方式）",dictionary, sSaveFullPath);
                    }
                }
            }
            else if (TabControl.SelectedTabIndex == 1)
            {
                if (dgItemInfo != null && dgItemInfo.Rows.Count > 0)
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    for (int i = 0; i < dgItemInfo.Columns.Count; i++)
                    {
                        if (dgItemInfo.Columns[i].Visible)
                        {
                            dictionary.Add(dgItemInfo.Columns[i].DataPropertyName, dgItemInfo.Columns[i].HeaderText);
                        }
                    }

                    string sSaveFullPath = string.Empty;
                    DataTable dt = (DataTable)dgItemInfo.DataSource;
                    if (InitShowDialog("病人费用查询（项目)", out sSaveFullPath) == true)
                    {
                        EFWCoreLib.CoreFrame.Common.ExcelHelper.Export(dt, "病人费用查询（项目）", dictionary, sSaveFullPath);
                    }
                }
            }            
        }

        /// <summary>
        /// 支付方式网格双击事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgPayMentInfo_DoubleClick(object sender, EventArgs e)
        {
            if (dgPayMentInfo.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgPayMentInfo.CurrentCell.RowIndex;
            if (rowindex != dgPayMentInfo.Rows.Count - 1)
            {
                int costHeadid = Convert.ToInt32(dgPayMentInfo[CostHeadID.Index, rowindex].Value);
                InvokeController("CostSearchDetail", costHeadid);
            }
        }

        /// <summary>
        /// 项目明细网格双击事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgItemInfo_DoubleClick(object sender, EventArgs e)
        {
            if (dgItemInfo.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgItemInfo.CurrentCell.RowIndex;
            if (rowindex != dgItemInfo.Rows.Count - 1)
            {
                int costHeadid = Convert.ToInt32(dgItemInfo[CostHeadIDItem.Index, rowindex].Value);
                InvokeController("CostSearchDetail", costHeadid);
            }
        }

        /// <summary>
        /// tab切换事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TabControl_SelectedTabChanged(object sender, DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs e)
        {
            if (TabControl.SelectedTabIndex == 1)
            {
                SetItemGridColor();
            }
        }

        /// <summary>
        /// 票据补打按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnPrintAgain_Click(object sender, EventArgs e)
        {
            if (TabControl.SelectedTabIndex == 0)
            {
                if (dgPayMentInfo.CurrentCell == null)
                {
                    return;
                }

                int rowindex = dgPayMentInfo.CurrentCell.RowIndex;
                if (rowindex != dgPayMentInfo.Rows.Count - 1)
                {
                    string status = dgPayMentInfo[CostStatusItem.Index, rowindex].Value.ToString();
                    if (status != "正常")
                    {
                        MessageBoxEx.Show("已退费票据不能补打");
                        return;
                    }

                    string regStatus = dgPayMentInfo[regflag.Index, rowindex].Value.ToString();
                    if (regStatus == "挂号")
                    {
                        MessageBoxEx.Show("挂号票据不能补打");
                        return;
                    }

                    printcostheadid = Convert.ToInt32(dgPayMentInfo[CostHeadID.Index, rowindex].Value);
                    InvokeController("PrintInvoiceInput");
                }
            }
            else if (TabControl.SelectedTabIndex == 1)
            {
                if (dgItemInfo.CurrentCell == null)
                {
                    return;
                }

                int rowindex = dgItemInfo.CurrentCell.RowIndex;
                if (rowindex != dgItemInfo.Rows.Count - 1)
                {
                    string status = dgItemInfo[CostStatusItem.Index, rowindex].Value.ToString();
                    if (status != "正常")
                    {
                        MessageBoxEx.Show("已退费票据不能补打");
                        return;
                    }

                    string regStatus = dgItemInfo[regItemflag.Index, rowindex].Value.ToString();
                    if (regStatus == "挂号")
                    {
                        MessageBoxEx.Show("挂号票据不能补打");
                        return;
                    }

                    printcostheadid = Convert.ToInt32(dgItemInfo[CostHeadIDItem.Index, rowindex].Value);
                    InvokeController("PrintInvoiceInput");
                }
            }
        }

        /// <summary>
        /// 费用清单补打按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnPresDetailPrintAgain_Click(object sender, EventArgs e)
        {
            if (TabControl.SelectedTabIndex == 0)
            {
                if (dgPayMentInfo.CurrentCell == null)
                {
                    return;
                }

                int rowindex = dgPayMentInfo.CurrentCell.RowIndex;
                if (rowindex != dgPayMentInfo.Rows.Count - 1)
                {
                    string status = dgPayMentInfo[CostStatusItem.Index, rowindex].Value.ToString();
                    if (status != "正常")
                    {
                        MessageBoxEx.Show("已退费票据不能补打");
                        return;
                    }

                    string regStatus = dgPayMentInfo[regflag.Index, rowindex].Value.ToString();
                    if (regStatus == "挂号")
                    {
                        MessageBoxEx.Show("挂号票据不能补打");
                        return;
                    }

                    printcostheadid = Convert.ToInt32(dgPayMentInfo[CostHeadID.Index, rowindex].Value);
                    InvokeController("PrintPrescAgain");
                }
            }
            else if (TabControl.SelectedTabIndex == 1)
            {
                if (dgItemInfo.CurrentCell == null)
                {
                    return;
                }

                int rowindex = dgItemInfo.CurrentCell.RowIndex;
                if (rowindex != dgItemInfo.Rows.Count - 1)
                {
                    string status = dgItemInfo[CostStatusItem.Index, rowindex].Value.ToString();
                    if (status != "正常")
                    {
                        MessageBoxEx.Show("已退费票据不能补打");
                        return;
                    }

                    string regStatus = dgItemInfo[regItemflag.Index, rowindex].Value.ToString();
                    if (regStatus == "挂号")
                    {
                        MessageBoxEx.Show("挂号票据不能补打");
                        return;
                    }

                    printcostheadid = Convert.ToInt32(dgItemInfo[CostHeadIDItem.Index, rowindex].Value);
                    InvokeController("PrintPrescAgain");
                }
            }
        }
    }
}