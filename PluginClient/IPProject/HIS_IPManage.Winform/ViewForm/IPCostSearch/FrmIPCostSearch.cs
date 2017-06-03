using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView.IPCostSearch;

namespace HIS_IPManage.Winform.ViewForm.IPCostSearch
{
    /// <summary>
    /// 住院病人费用查询界面
    /// </summary>
    public partial class FrmIPCostSearch : BaseFormBusiness, IIPCostSearch
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmIPCostSearch()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 检索条件
        /// </summary>
        private Dictionary<string, object> mQueryDictionary = new Dictionary<string, object>();

        /// <summary>
        /// 检索条件
        /// </summary>
        public Dictionary<string, object> QueryDictionary
        {
            get
            {
                return mQueryDictionary;
            }

            set
            {
                mQueryDictionary = value;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            mQueryDictionary.Clear();
            mQueryDictionary.Add("ChargeData", radChargeDate.Checked);  // 收费时间
            mQueryDictionary.Add("AccountData", radAccountDate.Checked);  // 缴款时间
            mQueryDictionary.Add("Bdate", dtpBdate.Value); // 开始时间
            mQueryDictionary.Add("Edate", dtpEdate.Value);  // 结束时间
            mQueryDictionary.Add("ChargerEmpID", txtChareEmp.MemberValue);  // 收费员ID
            mQueryDictionary.Add("PayTypeID", txtPatType.MemberValue);    // 病人类型
            mQueryDictionary.Add("PresDeptID", txtPresDept.MemberValue);  // 就诊科室
            mQueryDictionary.Add("PrsEmpID", txtPresEmp.MemberValue);  // 就诊医生
            mQueryDictionary.Add("QueryCondition", txtQueryContent.Text.Trim()); // 检索条件
            mQueryDictionary.Add("BeInvoiceNO", txtBeInvoiceNO.Text.Trim());  // 开始发票号
            mQueryDictionary.Add("EndInvoiceNO", txtEndInvoiceNO.Text.Trim()); // 结束发票号
            if (radAllStatus.Checked)
            {
                mQueryDictionary.Add("Satus", "0,1,2");  // 全部
            }
            else if (radNormalStatus.Checked)
            {
                mQueryDictionary.Add("Satus", 0);// 正常
            }
            else if (radRefundStatus.Checked)
            {
                mQueryDictionary.Add("Satus", "1,2"); // 被退
            }
            // 结算类型
            if (rdoHalfwayCost.Checked)
            {
                mQueryDictionary.Add("CostType", 1); // 中途结算
            }
            else if (rdoNormalCost.Checked)
            {
                mQueryDictionary.Add("CostType", 2); // 出院结算
            }
            else if (rdoArrearsCost.Checked)
            {
                mQueryDictionary.Add("CostType", 3);  // 欠费结算
            }

            InvokeController("IpCostSearchQuery");
        }

        /// <summary>
        /// 绑定收费员列表
        /// </summary>
        /// <param name="chareEmpDt">收费员列表</param>
        public void Bind_ChareEmpList(DataTable chareEmpDt)
        {
            txtChareEmp.MemberField = "EmpId";
            txtChareEmp.DisplayField = "Name";
            txtChareEmp.CardColumn = "Name|名称|auto";
            txtChareEmp.QueryFieldsString = "Name,Pym,Wbm,Szm";
            txtChareEmp.ShowCardWidth = 350;
            txtChareEmp.ShowCardDataSource = chareEmpDt;
            txtChareEmp.MemberValue = -1;
        }

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="deptDt">科室列表</param>
        public void Bind_DeptList(DataTable deptDt)
        {
            txtPresDept.MemberField = "DeptId";
            txtPresDept.DisplayField = "Name";
            txtPresDept.CardColumn = "Name|名称|auto";
            txtPresDept.QueryFieldsString = "Name,Pym,Wbm,Szm";
            txtPresDept.ShowCardWidth = 350;
            txtPresDept.ShowCardDataSource = deptDt;
            txtPresDept.MemberValue = -1;
        }

        /// <summary>
        /// 绑定病人类型列表
        /// </summary>
        /// <param name="patTypeDt">病人类型列表</param>
        public void Bind_PatTypeList(DataTable patTypeDt)
        {
            txtPatType.MemberField = "PatTypeID";
            txtPatType.DisplayField = "PatTypeName";
            txtPatType.CardColumn = "PatTypeName|名称|auto";
            txtPatType.QueryFieldsString = "PYCode,WBCode,PatTypeName";
            txtPatType.ShowCardWidth = 350;
            txtPatType.ShowCardDataSource = patTypeDt;
            txtPatType.MemberValue = -1;
        }

        /// <summary>
        /// 绑定医生列表
        /// </summary>
        /// <param name="doctorDt">医生列表</param>
        public void Bind_DoctorList(DataTable doctorDt)
        {
            txtPresEmp.MemberField = "EmpId";
            txtPresEmp.DisplayField = "Name";
            txtPresEmp.CardColumn = "Name|名称|auto";
            txtPresEmp.QueryFieldsString = "Name,Pym,Wbm,Szm";
            txtPresEmp.ShowCardWidth = 350;
            txtPresEmp.ShowCardDataSource = doctorDt;
            txtPresEmp.MemberValue = -1;
        }

        /// <summary>
        /// 窗体打开前加载数据
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmIPCostSearch_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("SetMaterData");
            FormControlInit();
        }

        /// <summary>
        /// 绑定结算数据
        /// </summary>
        /// <param name="payTypeDt">支付方式分类列表</param>
        /// <param name="itemTypeDt">项目类型列表</param>
        public void Bind_CostData(DataTable payTypeDt, DataTable itemTypeDt)
        {
            // 删除支付方式分类自动生成列
            for (int i = 14; i < dgPayMentInfo.Columns.Count; i++)
            {
                dgPayMentInfo.Columns.RemoveAt(i);
            }
            // 删除项目方式分类自动生成列
            for (int i = 15; i < dgItemInfo.Columns.Count; i++)
            {
                dgItemInfo.Columns.RemoveAt(i);
            }
            // 
            if (payTypeDt.Rows.Count == 0 || itemTypeDt.Rows.Count == 0)
            {
                dgPayMentInfo.DataSource = new DataTable();
                dgItemInfo.DataSource = new DataTable();
                return;
            }

            for (int i = 16; i < payTypeDt.Columns.Count; i++)
            {
                if (!dgPayMentInfo.Columns.Contains(payTypeDt.Columns[i].ColumnName))
                {
                    DataGridViewColumn col = new DataGridViewColumn();
                    col.HeaderText = payTypeDt.Columns[i].ColumnName;
                    col.DataPropertyName = payTypeDt.Columns[i].ColumnName;
                    col.Name = payTypeDt.Columns[i].ColumnName;
                    DataGridViewCell dgvcell = new DataGridViewTextBoxCell();
                    col.CellTemplate = dgvcell;
                    col.DefaultCellStyle.Format = "N2";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgPayMentInfo.Columns.Add(col);
                }
            }

            DataRow dr = payTypeDt.Rows[payTypeDt.Rows.Count - 1];
            dr["CostDate"] = DBNull.Value;
            dr["SerialNumber"] = DBNull.Value;
            dr["EnterHDate"] = DBNull.Value;
            dr["LeaveHDate"] = DBNull.Value;
            dr["HospitalizationDays"] = DBNull.Value;
            dgPayMentInfo.DataSource = payTypeDt;
            SetPayMentGridColor();

            for (int i = 16; i < itemTypeDt.Columns.Count; i++)
            {
                if (!dgItemInfo.Columns.Contains(itemTypeDt.Columns[i].ColumnName))
                {
                    DataGridViewColumn col = new DataGridViewColumn();
                    col.HeaderText = itemTypeDt.Columns[i].ColumnName;
                    col.DataPropertyName = itemTypeDt.Columns[i].ColumnName;
                    col.Name = itemTypeDt.Columns[i].ColumnName;
                    DataGridViewCell dgvcell = new DataGridViewTextBoxCell();
                    col.CellTemplate = dgvcell;
                    col.DefaultCellStyle.Format = "N2";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    col.Width = 80;
                    dgItemInfo.Columns.Add(col);
                }
            }

            DataRow itemDr = itemTypeDt.Rows[itemTypeDt.Rows.Count - 1];
            itemDr["CostDate"] = DBNull.Value;
            itemDr["SerialNumber"] = DBNull.Value;
            itemDr["EnterHDate"] = DBNull.Value;
            itemDr["LeaveHDate"] = DBNull.Value;
            itemDr["HospitalizationDays"] = DBNull.Value;
            dgItemInfo.DataSource = itemTypeDt;
            SetItemGridColor();
        }

        /// <summary>
        /// 初始化SaveFileDialog
        /// </summary>
        /// <param name="sExportName">报表名</param>
        /// <param name="sSaveFullPath">保存路径</param>
        /// <returns>{确认导出：True;否则:false}</returns>
        public bool InitShowDialog(string sExportName, out string sSaveFullPath)
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
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
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
                        EFWCoreLib.CoreFrame.Common.ExcelHelper.Export(dt, "病人费用查询（按支付方式）", dictionary, sSaveFullPath);
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
        /// 设置网格数据显示颜色
        /// </summary>
        private void SetPayMentGridColor()
        {
            if (dgPayMentInfo != null && dgPayMentInfo.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgPayMentInfo.Rows)
                {
                    string status_flag = row.Cells["StatusName"].Value.ToString();
                    Color foreColor = Color.Blue;
                    switch (status_flag)
                    {
                        case "正常":
                            foreColor = Color.Blue;
                            break;
                        case "被退":
                            foreColor = Color.Red;
                            break;
                        case "红冲":
                            foreColor = Color.Red;
                            break;
                    }

                    dgPayMentInfo.SetRowColor(row.Index, foreColor, true);
                }
            }
        }

        /// <summary>
        /// 设置网格数据显示颜色
        /// </summary>
        private void SetItemGridColor()
        {
            if (dgItemInfo != null && dgItemInfo.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgItemInfo.Rows)
                {
                    string status_flag = row.Cells["StatusNameItem"].Value.ToString();
                    Color foreColor = Color.Blue;
                    switch (status_flag)
                    {
                        case "正常":
                            foreColor = Color.Blue;
                            break;
                        case "被退":
                            foreColor = Color.Red;
                            break;
                        case "红冲":
                            foreColor = Color.Red;
                            break;
                    }

                    dgItemInfo.SetRowColor(row.Index, foreColor, true);
                }
            }
        }

        /// <summary>
        /// Tab选择加载网格数据显示颜色
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void TabControl_SelectedTabChanged(object sender, DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs e)
        {
            if (TabControl.SelectedTabIndex == 0)
            {
                SetPayMentGridColor();
            }
            else if (TabControl.SelectedTabIndex == 1)
            {
                SetItemGridColor();
            }
        }

        /// <summary>
        /// 双击查看明细
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void dgPayMentInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgPayMentInfo.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgPayMentInfo.CurrentCell.RowIndex;
            if (rowindex != dgPayMentInfo.Rows.Count - 1)
            {
                DataTable dt = dgPayMentInfo.DataSource as DataTable;
                if (!dt.Rows[rowindex]["StatusName"].ToString().Equals("正常"))
                {
                    InvokeController("MessageShow", "请选择未取消结算的记录查看费用明细！");
                    return;
                }

                int costHeadid = Convert.ToInt32(dt.Rows[rowindex]["CostHeadID"]);
                InvokeController("CostSearchDetail", costHeadid);
            }
        }

        /// <summary>
        /// 双击查看明细
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void dgItemInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgItemInfo.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgItemInfo.CurrentCell.RowIndex;
            if (rowindex != dgItemInfo.Rows.Count - 1)
            {
                DataTable dt = dgItemInfo.DataSource as DataTable;
                if (!dt.Rows[rowindex]["StatusName"].ToString().Equals("正常"))
                {
                    InvokeController("MessageShow", "请选择未取消结算的记录查看费用明细！");
                    return;
                }

                int costHeadid = Convert.ToInt32(dt.Rows[rowindex]["CostHeadID"]);
                InvokeController("CostSearchDetail", costHeadid);
            }
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 重置查询条件
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnSetBegin_Click(object sender, EventArgs e)
        {
            FormControlInit();
        }

        /// <summary>
        /// 初始化控件默认值
        /// </summary>
        private void FormControlInit()
        {
            dtpBdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpEdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            txtQueryContent.Text = string.Empty;
            txtBeInvoiceNO.Text = string.Empty;
            txtEndInvoiceNO.Text = string.Empty;
            rdoHalfwayCost.Checked = true;
            radAllStatus.Checked = true;
            txtChareEmp.MemberValue = -1;
            txtPresDept.MemberValue = -1;
            txtPatType.MemberValue = -1;
            txtPresEmp.MemberValue = -1;
        }
    }
}
