using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 入库单管理
    /// </summary>
    public partial class FrmInStore : BaseFormBusiness, IFrmInStore
    {
        #region 属性
        /// <summary>
        /// 获取查询条件
        /// </summary>
        public List<Tuple<string, string, SqlOperator>> AndWhere { get; set; }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmInStore()
        {
            InitializeComponent();
        }

        #region 接口实现
        /// <summary>
        /// 刷新选中科室
        /// </summary>
        public void RefreshSelecedDept()
        {
            if (cmbDept.SelectedValue != null)
            {
                InvokeController("SetSelectedDept", Convert.ToInt32(cmbDept.SelectedValue));
            }
        }

        /// <summary>
        /// 绑定供应商控件
        /// </summary>
        /// <param name="dtSupply">数据源</param>
        public void BindSupply(DataTable dtSupply)
        {
            txtSupport.DisplayField = "SupportName";
            txtSupport.MemberField = "SupplierID";
            txtSupport.CardColumn = "SupplierID|编码|50,SupportName|供应商名称|auto";
            txtSupport.QueryFieldsString = "SupportName,PYCode,WBCode";
            txtSupport.ShowCardWidth = 250;
            txtSupport.ShowCardDataSource = dtSupply;
        }

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
        /// 绑定业务类型
        /// </summary>
        /// <param name="dtOpType">数据源</param>
        public void BindOpType(DataTable dtOpType)
        {
            cmbOpType.DataSource = dtOpType;
            cmbOpType.DisplayMember = "opTypeName";
            cmbOpType.ValueMember = "opType";
            cmbOpType.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定头表
        /// </summary>
        /// <param name="dtInstoreHead">表头网格数据源</param>
        public void BindInHeadGrid(DataTable dtInstoreHead)
        {
            foreach (DataRow row in dtInstoreHead.Rows)
            {
                if (row["AuditTime"] != DBNull.Value && row["AuditTime"].ToString().Contains("1900-01-01"))
                {
                    row["AuditTime"] = DBNull.Value;
                }
            }

            dgBillHead.DataSource = dtInstoreHead;
        }

        /// <summary>
        /// 绑定明细
        /// </summary>
        /// <param name="dtInstoreDetails">明细网格数据源</param>
        public void BindInDetailGrid(DataTable dtInstoreDetails)
        {
            dgBillDetail.DataSource = dtInstoreDetails;
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>表头查询条件</returns>
        public Dictionary<string, string> GetQueryCondition(int deptID)
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            queryCondition.Add(string.Empty, "BillTime between '" + dtpBillDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '" + dtpBillDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'");
            queryCondition.Add("AuditFlag", radAudit.Checked ? "1" : "0");
            queryCondition.Add("DeptID", deptID.ToString());
            if (chkBillNO.Checked)
            {
                queryCondition.Add("BillNO", "'" + txtBillNO.Text.Trim() + "'");
            }

            if (chkInoviceNO.Checked)
            {
                queryCondition.Add("InvoiceNo", "'" + txtInvoiceNO.Text.Trim() + "'");
            }

            if (chkSupport.Checked && txtSupport.MemberValue != null)
            {
                queryCondition.Add("SupplierID", txtSupport.MemberValue.ToString());
            }

            if (chkOpType.Checked && cmbOpType.SelectedIndex >= 0)
            {
                queryCondition.Add("BusiType", "'" + cmbOpType.SelectedValue.ToString() + "'");
            }

            if (chkPayStatus.Checked && cmbPayStatus.SelectedIndex >= 0)
            {
                queryCondition.Add("PayFlag", cmbPayStatus.SelectedIndex.ToString());
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
                    rtn.Add("InHeadID", currentRow["InHeadID"].ToString());
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
        #endregion

        #region 自定义方法
        /// <summary>
        /// 检测查询条件
        /// </summary>
        /// <returns>检测结果</returns>
        private bool CheckCondition()
        {
            if (dtpBillDate.Bdate.Value.CompareTo(dtpBillDate.Edate.Value) > 0)
            {
                MessageBoxEx.Show("开始时间不能小于结束时间", "错误提示");
                dtpBillDate.Focus();
                return false;
            }

            return true;
        }
        #endregion

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnNewBill_Click(object sender, EventArgs e)
        {
            if (cmbDept.SelectedValue != null)
            {
                InvokeController("SetSelectedDept", Convert.ToInt32(cmbDept.SelectedValue));
            }

            InvokeController("InitBillHead", frmName, DGEnum.BillEditStatus.ADD_STATUS, 0);

            if (frmName == "FrmInStoreDW")
            {
                InvokeController("Show", "FrmInStoreDetailDW");
            }
            else
            {
                InvokeController("Show", "FrmInStoreDetailDS");
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (cmbDept.SelectedValue != null)
            {
                InvokeController("SetSelectedDept", Convert.ToInt32(cmbDept.SelectedValue));
            }

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
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnDelBill_Click(object sender, EventArgs e)
        {
            if (dgBillHead.CurrentCell != null)
            {
                if (dgBillHead.CurrentCell.RowIndex >= 0)
                {
                    if (MessageBox.Show("您确认要删除所选的单据吗?", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        int rowIndex = dgBillHead.CurrentCell.RowIndex;
                        DataRow dRow = ((DataTable)dgBillHead.DataSource).Rows[rowIndex];
                        InvokeController("DeleteBill", frmName, Convert.ToInt32(dRow["InHeadID"]), dRow["BusiType"].ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnUpdateBill_Click(object sender, EventArgs e)
        {
            if (cmbDept.SelectedValue != null)
            {
                InvokeController("SetSelectedDept", Convert.ToInt32(cmbDept.SelectedValue));
            }

            if (dgBillHead.CurrentCell != null)
            {
                if (dgBillHead.CurrentCell.RowIndex >= 0)
                {
                    int rowIndex = dgBillHead.CurrentCell.RowIndex;
                    DataRow dRow = ((DataTable)dgBillHead.DataSource).Rows[rowIndex];
                    InvokeController("InitBillHead", frmName, DGEnum.BillEditStatus.UPDATE_STATUS, Convert.ToInt32(dRow["InHeadID"]));
                    if (frmName == "FrmInStoreDW")
                    {
                        InvokeController("Show", "FrmInStoreDetailDW");
                    }
                    else
                    {
                        InvokeController("Show", "FrmInStoreDetailDS");
                    }
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnAuditBill_Click(object sender, EventArgs e)
        {
            if (dgBillHead.CurrentCell != null)
            {
                if (dgBillHead.CurrentCell.RowIndex >= 0)
                {
                    if (MessageBox.Show("您确认要审核所选的单据吗?", "审核提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        int rowIndex = dgBillHead.CurrentCell.RowIndex;
                        DataRow dRow = ((DataTable)dgBillHead.DataSource).Rows[rowIndex];
                        InvokeController("AuditBill", frmName, Convert.ToInt32(dRow["InHeadID"]), dRow["BusiType"].ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmInStore_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("GetSupplyForShowCard", frmName);
            InvokeController("GetDrugDept", frmName);
            InvokeController("BuildOpType", frmName, frmName == "FrmInStoreDW" ? DGConstant.OP_DW_SYSTEM : DGConstant.OP_DS_SYSTEM);
            dtpBillDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpBillDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            cmbPayStatus.SelectedIndex = 0;
            btnQuery_Click(null, null);

            if (frmName == "FrmInStoreDW")
            {
                var dataGridViewColumn = dgBillDetail.Columns["uAmount"];
                if (dataGridViewColumn != null)
                {
                    dataGridViewColumn.Visible = false;
                }

                var gridViewColumn = dgBillDetail.Columns["UnitName"];
                if (gridViewColumn != null)
                {
                    gridViewColumn.Visible = false;
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void chkSupport_CheckedChanged(object sender, EventArgs e)
        {
            txtSupport.Enabled = chkSupport.Checked;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void chkPayStatus_CheckedChanged(object sender, EventArgs e)
        {
            cmbPayStatus.Enabled = chkPayStatus.Checked;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void chkOpType_CheckedChanged(object sender, EventArgs e)
        {
            cmbOpType.Enabled = chkOpType.Checked;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void chkInoviceNO_CheckedChanged(object sender, EventArgs e)
        {
            txtInvoiceNO.Enabled = chkInoviceNO.Checked;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void chkBillNO_CheckedChanged(object sender, EventArgs e)
        {
            txtBillNO.Enabled = chkBillNO.Checked;
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
                btnAuditBill.Visible = true;
                btnUpdateBill.Visible = true;
                barBillMgr.Refresh();
                btnQuery_Click(null, null);
                lblBillDate.Text = "单据日期";
            }
            else
            {
                btnDelBill.Visible = false;
                btnAuditBill.Visible = false;
                btnUpdateBill.Visible = false;
                barBillMgr.Refresh();
                btnQuery_Click(null, null);
                lblBillDate.Text = "审核日期";
            }
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
                InvokeController("SetSelectedDept", Convert.ToInt32(cmbDept.SelectedValue));
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void ReportPrint_Click(object sender, EventArgs e)
        {
            DataTable dthead = (DataTable)dgBillHead.DataSource;
            if (dgBillHead.CurrentCell != null)
            {
                if (dgBillHead.CurrentCell.RowIndex >= 0)
                {
                    DataTable dtDetails = (DataTable)dgBillDetail.DataSource;
                    int currentIndex = dgBillHead.CurrentCell.RowIndex;
                    DataRow currentRow = ((DataTable)(dgBillHead.DataSource)).Rows[currentIndex];
                    InvokeController("Print", frmName, currentRow, dtDetails);
                }
                else
                {
                    MessageBoxEx.Show("请选择头表记录");
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmInStore_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    btnNewBill_Click(null, null);
                    break;
                case Keys.F3:
                    btnDelBill_Click(null, null);
                    break;
                case Keys.F4:
                    btnUpdateBill_Click(null, null);
                    break;
                case Keys.F6:
                    btnAuditBill_Click(null, null);
                    break;
                case Keys.F7:
                    ReportPrint_Click(null, null);
                    break;
            }
        }
    }
}
