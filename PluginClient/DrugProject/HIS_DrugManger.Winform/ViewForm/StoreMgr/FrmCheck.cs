using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 盘点单管理
    /// </summary>
    public partial class FrmCheck : BaseFormBusiness, IFrmCheck
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmCheck()
        {
            InitializeComponent();
        }

        #region 自定义方法
        /// <summary>
        /// 根据不同的窗体名称设置列显示隐藏
        /// </summary>
        private void HideColumn()
        {
            if (frmName == "FrmCheckDS")
            {
                dgBillDetail.Columns["ValidityDate"].Visible = false;
                dgBillDetail.Columns["BatchNO"].Visible = false;
                dgBillDetail.Columns["ActAmount"].Visible = false;
                dgBillDetail.Columns["UnitName"].Visible = false;
                dgBillDetail.Columns["FactAmount"].Visible = false;
                dgBillDetail.Columns["ActAmountShow"].Visible = true;
                dgBillDetail.Columns["FactAmountShow"].Visible = true;
            }
            else
            {
                dgBillDetail.Columns["ValidityDate"].Visible = true;
                dgBillDetail.Columns["BatchNO"].Visible = true;
                dgBillDetail.Columns["ActAmount"].Visible = true;
                dgBillDetail.Columns["UnitName"].Visible = true;
                dgBillDetail.Columns["FactAmount"].Visible = true;
                dgBillDetail.Columns["ActAmountShow"].Visible = false;
                dgBillDetail.Columns["FactAmountShow"].Visible = false;
            }
        }

        /// <summary>
        /// 检测查询条件
        /// </summary>
        /// <returns>检测结果</returns>
        private bool CheckCondition()
        {
            if (chkRegTime.Checked)
            {
                if (dtpRegDate.Bdate.Value.CompareTo(dtpRegDate.Edate.Value) > 0)
                {
                    MessageBoxEx.Show("开始时间不能小于结束时间", "错误提示");
                    dtpRegDate.Focus();
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region 接口实现      
        /// <summary>
        /// 绑定药剂科室控件
        /// </summary>
        /// <param name="dtDrugDept">数据源</param>
        public void BindDrugDept(DataTable dtDrugDept)
        {
            if (dtDrugDept != null)
            {
                cmbDept.DataSource = dtDrugDept;
                if (dtDrugDept.Rows.Count > 0)
                {
                    cmbDept.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 绑定头表
        /// </summary>
        /// <param name="dtHead">表头网格数据源</param>
        public void BindInHeadGrid(DataTable dtHead)
        {
            foreach (DataRow row in dtHead.Rows)
            {
                if (row["AuditTime"] != DBNull.Value && row["AuditTime"].ToString().Contains("1900-01-01"))
                {
                    row["AuditTime"] = DBNull.Value;
                }

                if (row["AuditNO"] != DBNull.Value && row["AuditNO"].ToString() == "0")
                {
                    row["AuditNO"] = DBNull.Value;
                }
            }

            dgBillHead.DataSource = dtHead;
        }

        /// <summary>
        /// 绑定明细
        /// </summary>
        /// <param name="dtDetails">明细网格数据源</param>
        public void BindInDetailGrid(DataTable dtDetails)
        {
            dgBillDetail.DataSource = dtDetails;
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>表头查询条件</returns>
        public Dictionary<string, string> GetQueryCondition(int deptID)
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            queryCondition.Add("DeptID", deptID.ToString());
            queryCondition.Add("AuditFlag", radAudit.Checked ? "1" : "0");
            if (chkRegTime.Checked)
            {
                if (radNotAudit.Checked)
                {
                    queryCondition.Add(string.Empty, "RegTime between '" + dtpRegDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '" + dtpRegDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'");
                }
                else
                {
                    queryCondition.Add(string.Empty, "AuditTime between '" + dtpRegDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '" + dtpRegDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'");
                }
            }

            if (chkCheckBillNO.Checked)
            {
                queryCondition.Add("AuditNO", "'" + txtCheckBillNo.Text.Trim() + "'");
            }

            return queryCondition;
        }

        /// <summary>
        /// 获取选中表头ID
        /// </summary>
        /// <returns>当前选中表头ID</returns>
        public Dictionary<string, string> GetCurrentHeadID()
        {
            if (dgBillHead.CurrentCell != null)
            {
                if (dgBillHead.CurrentCell.RowIndex >= 0)
                {
                    int currentIndex = dgBillHead.CurrentCell.RowIndex;
                    DataRow currentRow = ((DataTable)(dgBillHead.DataSource)).Rows[currentIndex];
                    Dictionary<string, string> rtn = new Dictionary<string, string>();
                    rtn.Add("CheckHeadID", currentRow["CheckHeadID"].ToString());
                    return rtn;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取单据表头网格的数据源
        /// </summary>
        /// <returns>单据表头网格的数据源</returns>
        public DataTable GetDgHeadSource()
        {
            return (DataTable)dgBillHead.DataSource;
        }

        /// <summary>
        /// 插入提取的
        /// </summary>
        /// <param name="dtRtn">插入提取数据</param>
        public void InsertGetStorageData(DataTable dtRtn)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 事件
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmCheck_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    btnAddBill_Click(null, null);
                    break;
                case Keys.F3:
                    btnUpdateBill_Click(null, null);
                    break;
                case Keys.F4:
                    btnDelBill_Click(null, null);
                    break;
                case Keys.F5:
                    btnClearStatus_Click(null, null);
                    break;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmCheck_OpenWindowBefore(object sender, EventArgs e)
        {
            //设置列隐藏
            HideColumn();
            InvokeController("GetDrugDept", frmName);
            dtpRegDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpRegDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            btnQuery_Click(null, null);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void chkRegTime_CheckedChanged(object sender, EventArgs e)
        {
            dtpRegDate.Enabled = chkRegTime.Checked;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void chkCheckBillNO_CheckedChanged(object sender, EventArgs e)
        {
            txtCheckBillNo.Enabled = chkCheckBillNO.Checked;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void radNotAudit_CheckedChanged(object sender, EventArgs e)
        {
            if (radNotAudit.Checked)
            {
                btnDelBill.Visible = true;
                btnUpdateBill.Visible = true;
                barBillMgr.Refresh();
                chkRegTime.Text = "登记日期";
            }
            else
            {
                btnDelBill.Visible = false;
                btnUpdateBill.Visible = false;
                barBillMgr.Refresh();
                chkRegTime.Text = "审核日期";
            }

            btnQuery_Click(null, null);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgBillHead_CurrentCellChanged(object sender, EventArgs e)
        {
            InvokeController("LoadBillDetails", frmName);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDept.SelectedValue != null)
            {
                InvokeController("SetSelectedDept", Convert.ToInt32(cmbDept.SelectedValue), frmName);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmCheck_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnAddBill_Click(object sender, EventArgs e)
        {
            InvokeController("InitBillHead", frmName, DGEnum.BillEditStatus.ADD_STATUS, 0);
            if (frmName == "FrmCheckDW")
            {
                InvokeController("Show", "FrmCheckDetailDW");
            }
            else
            {
                InvokeController("Show", "FrmCheckDetailDS");
            }

            InvokeController("SetCheckStatus", frmName);
            //MessageBoxEx.Show("请注意，盘点状态已经启用，各种与库房往来的业务将会暂停......");
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (CheckCondition())
            {
                InvokeController("LoadBillHead", frmName);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnUpdateBill_Click(object sender, EventArgs e)
        {
            if (dgBillHead.CurrentCell != null)
            {
                if (dgBillHead.CurrentCell.RowIndex >= 0)
                {
                    int rowIndex = dgBillHead.CurrentCell.RowIndex;
                    DataRow dRow = ((DataTable)dgBillHead.DataSource).Rows[rowIndex];
                    if (dRow["AuditFlag"].ToString() == "1")
                    {
                        MessageBoxEx.Show("您选择的单据已经审核，不能修改");
                        return;
                    }

                    InvokeController("InitBillHead", frmName, DGEnum.BillEditStatus.UPDATE_STATUS, Convert.ToInt32(dRow["CheckHeadID"]));
                    if (frmName == "FrmCheckDW")
                    {
                        InvokeController("Show", "FrmCheckDetailDW");
                    }
                    else
                    {
                        InvokeController("Show", "FrmCheckDetailDS");
                    }
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnDelBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgBillHead.CurrentCell != null)
                {
                    if (dgBillHead.CurrentCell.RowIndex >= 0)
                    {
                        int rowIndex = dgBillHead.CurrentCell.RowIndex;
                        DataRow dRow = ((DataTable)dgBillHead.DataSource).Rows[rowIndex];
                        if (dRow["AuditFlag"].ToString() == "1")
                        {
                            MessageBoxEx.Show("您选择的单据已经审核，不能删除");
                            return;
                        }

                        if (MessageBox.Show("您确认要删除所选的单据吗?", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                        {
                            InvokeController("DeleteBill", frmName, Convert.ToInt32(dRow["CheckHeadID"]), dRow["BusiType"].ToString());
                            if (dgBillHead.Rows.Count > 0)
                            {
                                dgBillHead.Rows[dgBillHead.RowCount - 1].Selected = true;
                                dgBillHead.CurrentCell = dgBillHead.Rows[this.dgBillHead.Rows.Count - 1].Cells[1];
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                MessageBoxEx.Show(error.Message);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnAuditBill_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnClearStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("系统将删除所有未审核单据，您确认要清除盘点状态吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    //调用清除盘点状态方法
                    InvokeController("ClearCheckStatus", frmName);
                    if (dgBillHead.Rows.Count > 0)
                    {
                        dgBillHead.Rows[dgBillHead.RowCount - 1].Selected = true;
                        dgBillHead.CurrentCell = dgBillHead.Rows[this.dgBillHead.Rows.Count - 1].Cells[1];
                    }
                }
            }
            catch (Exception error)
            {
                MessageBoxEx.Show(error.Message);
            }
        }
        #endregion      
    }
}
