﻿using System;
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
    /// 出库单管理
    /// </summary>
    public partial class FrmOutStore : BaseFormBusiness, IFrmOutStore
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmOutStore()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmOutStore_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("GetDrugDept", frmName);
            InvokeController("BuildOpType", frmName, frmName == "FrmOutStoreDW" ? DGConstant.OP_DW_SYSTEM : DGConstant.OP_DS_SYSTEM);
            InvokeController("GetDrugRelateDept", frmName, string.Empty);
            timeBillDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            timeBillDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            if (frmName == "FrmOutStoreDW")
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

        #region 接口
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
        /// 获取查询条件
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>表头查询条件</returns>
        public Dictionary<string, string> GetQueryCondition(int deptID)
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            queryCondition.Add(string.Empty, "RegTime between '" + timeBillDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '" + timeBillDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'");
            queryCondition.Add("AuditFlag", ckNoAudit.Checked ? "0" : "1");
            queryCondition.Add("DeptID", deptID.ToString());
            if (ckBuss.Checked)
            {
                queryCondition.Add("BusiType", "'" + cmbOpType.SelectedValue.ToString() + "'");
            }

            if (ckBillNo.Checked)
            {
                queryCondition.Add("BillNO", "'" + txtBillNo.Text + "'");
            }

            if (ckDept.Checked && txtDept.MemberValue != null)
            {
                queryCondition.Add("ToDeptID", txtDept.MemberValue.ToString());
            }

            return queryCondition;
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
        /// 主表数据
        /// </summary>
        /// <param name="dt">数据源</param>
        public void BindHeadGrid(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row["AuditTime"] != DBNull.Value && row["AuditTime"].ToString().Contains("1900-01-01"))
                {
                    row["AuditTime"] = DBNull.Value;
                }
            }

            dgBillHead.DataSource = dt;
        }

        /// <summary>
        /// 从表数据
        /// </summary>
        /// <param name="dt">数据源</param>
        public void BindDeatailGrids(DataTable dt)
        {
            dgBillDetail.DataSource = dt;
        }

        /// <summary>
        /// 往来科室
        /// </summary>
        /// <param name="dt">往来科室数据源</param>
        public void BindDept(DataTable dt)
        {
            this.txtDept.DisplayField = "RelationDeptName";
            txtDept.MemberField = "RelationDeptID";
            txtDept.CardColumn = "RelationDeptID|编码|50,RelationDeptName|科室名称|auto";
            txtDept.QueryFieldsString = "RelationDeptName";
            txtDept.ShowCardWidth = 250;
            txtDept.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 获取表头ID
        /// </summary>
        /// <returns>表头ID</returns>
        public Dictionary<string, string> GetCurrentHeadID()
        {
            if (dgBillHead.CurrentCell != null)
            {
                if (dgBillHead.CurrentCell.RowIndex >= 0)
                {
                    int currentIndex = dgBillHead.CurrentCell.RowIndex;
                    DataRow currentRow = ((DataTable)(dgBillHead.DataSource)).Rows[currentIndex];
                    Dictionary<string, string> rtn = new Dictionary<string, string>();
                    rtn.Add("OutHeadID", currentRow["OutStoreHeadID"].ToString());
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
        #endregion

        #region 事件
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbDept.SelectedValue != null)
            {
                InvokeController("SetSelectedDept", Convert.ToInt32(cmbDept.SelectedValue));
            }

            if ((bool)InvokeController("InitBillHead", frmName, DGEnum.BillEditStatus.ADD_STATUS, 0))
            {
                if (frmName == "FrmOutStoreDW")
                {
                    InvokeController("Show", "FrmOutStoreDetailDW");
                }
                else
                {
                    InvokeController("Show", "FrmOutStoreDetailDS");
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnEdit_Click(object sender, EventArgs e)
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
                    InvokeController("InitBillHead", frmName, DGEnum.BillEditStatus.UPDATE_STATUS, Convert.ToInt32(dRow["OutStoreHeadID"]));
                    if (frmName == "FrmOutStoreDW")
                    {
                        InvokeController("Show", "FrmOutStoreDetailDW");
                    }
                    else
                    {
                        InvokeController("Show", "FrmOutStoreDetailDS");
                    }
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgBillHead.CurrentCell != null)
            {
                if (dgBillHead.CurrentCell.RowIndex >= 0)
                {
                    if (MessageBox.Show("您确认要删除所选的单据吗?", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        int rowIndex = dgBillHead.CurrentCell.RowIndex;
                        DataRow dRow = ((DataTable)dgBillHead.DataSource).Rows[rowIndex];
                        InvokeController("DeleteBill", frmName, Convert.ToInt32(dRow["OutStoreHeadID"]), dRow["BusiType"].ToString());
                    }
                }
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
        private void ckDept_CheckedChanged(object sender, EventArgs e)
        {
            this.txtDept.Enabled = ckDept.Checked;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void ckBillNo_CheckedChanged(object sender, EventArgs e)
        {
            this.txtBillNo.Enabled = ckBillNo.Checked;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void ckBuss_CheckedChanged(object sender, EventArgs e)
        {
            this.cmbOpType.Enabled = ckBuss.Checked;
        }
        #endregion

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void ckNoAudit_CheckedChanged(object sender, EventArgs e)
        {
            if (ckNoAudit.Checked)
            {
                this.btnAudit.Enabled = true;
                this.btnEdit.Enabled = true;
                this.btnDelete.Enabled = true;
            }
            else
            {
                this.btnAudit.Enabled = false;
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
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

        #region 
        /// <summary>
        /// 检测查询条件
        /// </summary>
        /// <returns>检测结果</returns>
        private bool CheckCondition()
        {
            if (this.timeBillDate.Bdate.Value.CompareTo(timeBillDate.Edate.Value) > 0)
            {
                MessageBoxEx.Show("开始时间不能小于结束时间", "错误提示");
                timeBillDate.Focus();
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
        private void btnAudit_Click(object sender, EventArgs e)
        {
            if (dgBillHead.CurrentCell != null)
            {
                if (dgBillHead.CurrentCell.RowIndex >= 0)
                {
                    int rowIndex = dgBillHead.CurrentCell.RowIndex;
                    DataRow dRow = ((DataTable)dgBillHead.DataSource).Rows[rowIndex];
                    InvokeController("AuditBill", frmName, Convert.ToInt32(dRow["OutStoreHeadID"]), dRow["BusiType"].ToString(), Convert.ToInt32(cmbDept.SelectedValue));
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnPrint_Click(object sender, EventArgs e)
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmbDept_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbDept.SelectedValue != null)
            {
                InvokeController("SetSelectedDept", Convert.ToInt32(cmbDept.SelectedValue));
            }
        }
    }
}
