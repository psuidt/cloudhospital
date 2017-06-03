using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MaterialManage;
using HIS_Entity.SqlAly;
using HIS_MaterialManage.Winform.IView.StoreMgr;

namespace HIS_MaterialManage.Winform.ViewForm
{
    /// <summary>
    /// 物资入库
    /// </summary>
    public partial class FrmInStore : BaseFormBusiness, IFrmInStore
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmInStore()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmInStore_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("GetSupplyForShowCard", frmName);
            InvokeController("GetDrugDept", frmName);
            InvokeController("BuildOpType", frmName);
            dtpBillDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpBillDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            cmbPayStatus.SelectedIndex = 0;
            btnQuery_Click(null, null);
        }
        #region 私有方法

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

        #region 接口

        /// <summary>
        /// 检索条件
        /// </summary>
        public List<Tuple<string, string, SqlOperator>> AndWhere { get; set; }

        /// <summary>
        /// 绑定物资科室
        /// </summary>
        /// <param name="dtDrugDept">物资科室</param>
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
        /// 绑定入库明细网格信息
        /// </summary>
        /// <param name="dtInstoreDetails">入库明细数据</param>
        public void BindInDetailGrid(DataTable dtInstoreDetails)
        {
            dgBillDetail.DataSource = dtInstoreDetails;
        }

        /// <summary>
        /// 绑定入库表头网格信息
        /// </summary>
        /// <param name="dtInstoreHead">入库头表数据</param>
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
        /// 绑定类型
        /// </summary>
        /// <param name="dtOpType">单据类型</param>
        public void BindOpType(DataTable dtOpType)
        {
            cmbOpType.DataSource = dtOpType;
            cmbOpType.DisplayMember = "opTypeName";
            cmbOpType.ValueMember = "opType";
            cmbOpType.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定供应商下拉信息
        /// </summary>
        /// <param name="dtSupply">供应商信息</param>
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
        /// 获取当前选中表头ID
        /// </summary>
        /// <returns>头ID集合</returns>
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
        /// 取得入库头表数据
        /// </summary>
        /// <returns>入库头表数据</returns>
        public DataTable GetDgHeadSource()
        {
            return (DataTable)dgBillHead.DataSource;
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>查询条件</returns>
        public Dictionary<string, string> GetQueryCondition(int deptID)
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();

            if (radAudit.Checked == true)
            {
                queryCondition.Add(
                    string.Empty,
                    "AuditTime between '" +
                    dtpBillDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") +
                    "' and '" +
                    dtpBillDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") +
                    "'");
                queryCondition.Add("AuditFlag", "1");
            }
            else
            {
                queryCondition.Add(
                    string.Empty,
                    "BillTime between '" +
                    dtpBillDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") +
                    "' and '" +
                    dtpBillDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") +
                    "'");
                queryCondition.Add("AuditFlag", "0");
            }

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

        #endregion

        /// <summary>
        /// 勾选供货商单位
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void chkSupport_CheckedChanged(object sender, EventArgs e)
        {
            txtSupport.Enabled = chkSupport.Checked;
        }

        /// <summary>
        /// 勾选支付方式
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void chkPayStatus_CheckedChanged(object sender, EventArgs e)
        {
            cmbPayStatus.Enabled = chkPayStatus.Checked;
        }

        /// <summary>
        /// 勾选业务类型
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void chkOpType_CheckedChanged(object sender, EventArgs e)
        {
            cmbOpType.Enabled = chkOpType.Checked;
        }

        /// <summary>
        /// 勾选发票号
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void chkInoviceNO_CheckedChanged(object sender, EventArgs e)
        {
            txtInvoiceNO.Enabled = chkInoviceNO.Checked;
        }

        /// <summary>
        /// 勾选单据号
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void chkBillNO_CheckedChanged(object sender, EventArgs e)
        {
            txtBillNO.Enabled = chkBillNO.Checked;
        }

        /// <summary>
        /// 勾选未审核单据
        /// </summary>
        /// <param name="sender">控件</param>
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
        /// 关闭界面
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (CheckCondition())
            {
                InvokeController("LoadBillHead", frmName);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnNewBill_Click(object sender, EventArgs e)
        {
            InvokeController("InitBillHead", frmName, MWEnum.BillEditStatus.ADD_STATUS, 0);
            InvokeController("Show", "FrmInStoreDetail");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnDelBill_Click(object sender, EventArgs e)
        {
            if (dgBillHead.CurrentCell != null)
            {
                if (dgBillHead.CurrentCell.RowIndex >= 0)
                {
                    if (MessageBox.Show(
                        "您确认要删除所选的单据吗?",
                        "删除提示",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        int rowIndex = dgBillHead.CurrentCell.RowIndex;
                        DataRow dRow = ((DataTable)dgBillHead.DataSource).Rows[rowIndex];
                        InvokeController("DeleteBill", frmName, Convert.ToInt32(dRow["InHeadID"]), dRow["BusiType"].ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 修改单据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnUpdateBill_Click(object sender, EventArgs e)
        {
            if (dgBillHead.CurrentCell != null)
            {
                if (dgBillHead.CurrentCell.RowIndex >= 0)
                {
                    int rowIndex = dgBillHead.CurrentCell.RowIndex;
                    DataRow dRow = ((DataTable)dgBillHead.DataSource).Rows[rowIndex];
                    InvokeController("InitBillHead", frmName, MWEnum.BillEditStatus.UPDATE_STATUS, Convert.ToInt32(dRow["InHeadID"]));
                    InvokeController("Show", "FrmInStoreDetail");
                }
            }
        }

        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnAuditBill_Click(object sender, EventArgs e)
        {
            if (dgBillHead.CurrentCell != null)
            {
                if (dgBillHead.CurrentCell.RowIndex >= 0)
                {
                    if (MessageBox.Show("您确认要审核所选的单据吗?", "审核提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int rowIndex = dgBillHead.CurrentCell.RowIndex;
                        DataRow dRow = ((DataTable)dgBillHead.DataSource).Rows[rowIndex];
                        InvokeController("AuditBill", frmName, Convert.ToInt32(dRow["InHeadID"]), dRow["BusiType"].ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 打印单据
        /// </summary>
        /// <param name="sender">控件</param>
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
                    MessageBoxEx.Show("请选出库单记录");
                }
            }
        }

        /// <summary>
        /// 选中单据加载明细
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void dgBillHead_CurrentCellChanged(object sender, EventArgs e)
        {
            InvokeController("LoadBillDetails", frmName);
        }

        /// <summary>
        /// 切换库房
        /// </summary>
        /// <param name="sender">控件</param>
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
