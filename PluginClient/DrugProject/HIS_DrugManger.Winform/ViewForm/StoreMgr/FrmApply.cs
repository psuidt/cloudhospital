using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.StoreMgr;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 领药申请
    /// </summary>
    public partial class FrmApply : BaseFormBusiness, IFrmApply
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmApply()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmApply_OpenWindowBefore(object sender, System.EventArgs e)
        {
            InvokeController("GetDrugDept", frmName);
            timeBillDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            timeBillDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
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
                    rtn.Add("ApplyHeadID", currentRow["ApplyHeadID"].ToString());
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
        /// 绑定表头数据
        /// </summary>
        /// <param name="dt">表头数据</param>
        public void BindApplyHead(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row["AuditTime"] != DBNull.Value && row["AuditTime"].ToString().Contains("1900-01-01"))
                {
                    row["AuditTime"] = DBNull.Value;
                }
            }

            dgBillHead.DataSource = dt;
            if (dt.Rows.Count == 0)
            {
                InvokeController("LoadBillDetails", frmName);
            }
        }

        /// <summary>
        /// 绑定明细数据
        /// </summary>
        /// <param name="dt">明细数据</param>
        public void BindApplyDetail(DataTable dt)
        {
            this.dgDetails.DataSource = dt;
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>表头查询条件</returns>
        public Dictionary<string, string> GetQueryCondition()
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            queryCondition.Add(string.Empty, "RegTime between '" + timeBillDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '" + timeBillDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'");
            queryCondition.Add("AuditFlag", ckNoAudit.Checked ? "0" : "1");
            return queryCondition;
        }
        #endregion

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnQuery_Click(object sender, System.EventArgs e)
        {
            if (CheckCondition())
            {
                InvokeController("LoadBillHead");
            }
        }

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

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, System.EventArgs e)
        {
            dgDetails.DataSource = null;
            InvokeController("Close", this);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            if ((bool)InvokeController("InitBillHead", string.Empty, "0", string.Empty, 0))
            {
                InvokeController("Show", "FrmApplyDetail");
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
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgBillHead.CurrentCell != null)
            {
                if (dgBillHead.CurrentCell.RowIndex >= 0)
                {
                    int rowIndex = dgBillHead.CurrentCell.RowIndex;
                    DataRow dRow = ((DataTable)dgBillHead.DataSource).Rows[rowIndex];
                    bool flag = (bool)InvokeController("InitBillHead", dRow["Remark"].ToString(), dRow["todeptid"].ToString(), dRow["todeptname"].ToString(), Convert.ToInt32(dRow["ApplyHeadID"]));
                    if (flag)
                    {
                        InvokeController("Show", "FrmApplyDetail");
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
                        InvokeController("DeleteBill", Convert.ToInt32(dRow["ApplyHeadID"]));
                    }
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmApply_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    btnAdd_Click(null, null);
                    break;
                case Keys.F3:
                    btnEdit_Click(null, null);
                    break;
                case Keys.F4:
                    btnDelete_Click(null, null);
                    break;
            }
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
