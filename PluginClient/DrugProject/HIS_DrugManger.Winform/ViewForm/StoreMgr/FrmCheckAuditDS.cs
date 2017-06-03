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
    /// 盘点审核
    /// </summary>
    public partial class FrmCheckAuditDS : BaseFormBusiness, IFrmCheckAudit
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmCheckAuditDS()
        {
            InitializeComponent();
        }

        #region 自定义属性和方法
        /// <summary>
        /// 取得选择的科室ID
        /// </summary>
        /// <returns>选择的科室ID</returns>
        private int GetSelectedDeptId()
        {
            int deptId = 0;
            if (cmbDept.SelectedValue != null)
            {
                deptId = Convert.ToInt32(cmbDept.SelectedValue);
            }

            return deptId;
        }
        #endregion

        #region 接口
        /// <summary>
        /// 显示汇总信息
        /// </summary>
        /// <param name="message">显示信息</param>
        public void ShowAuditCompute(string message)
        {
            plSum.Text = message;
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

            DataTable dtAuditDept = dtDrugDept.Copy();
            if (dtAuditDept != null)
            {
                cmbAuditStoreRoom.DataSource = dtDrugDept;
                if (dtAuditDept.Rows.Count > 0)
                {
                    cmbAuditStoreRoom.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 绑定提示信息
        /// </summary>
        /// <param name="tip">库房状态，待审单据数量</param>
        public void BindShowTip(string tip)
        {
            lblShowMessage.Text = tip;
        }

        /// <summary>
        /// 绑定药品定位查询ShowCard
        /// </summary>
        /// <param name="dtDrugInfo">药品信息</param>
        public void BindDrugPositFindCard(DataTable dtDrugInfo)
        {
            //待审核明细定位ShowCard
            txtCode.DisplayField = "ChemName";
            txtCode.MemberField = "DrugID";
            txtCode.CardColumn = "DrugID|编码|55,ChemName|化学名称|180,Spec|规格|120,ProductName|生产厂家|120,PackUnit|单位|40";
            txtCode.QueryFieldsString = "ChemName,TradeName,PYCode,WBCode,TPYCode,TWBCode";
            txtCode.ShowCardWidth = 349;
            txtCode.ShowCardDataSource = dtDrugInfo;

            //已审核明细定位ShowCard
            txtAuditCode.DisplayField = "ChemName";
            txtAuditCode.MemberField = "DrugID";
            txtAuditCode.CardColumn = "DrugID|编码|55,ChemName|化学名称|180,Spec|规格|120,ProductName|生产厂家|120,PackUnit|单位|40";
            txtAuditCode.QueryFieldsString = "ChemName,TradeName,PYCode,WBCode,TPYCode,TWBCode";
            txtAuditCode.ShowCardWidth = 349;
            txtAuditCode.ShowCardDataSource = dtDrugInfo;
        }

        /// <summary>
        /// 绑定盘点汇总明细
        /// </summary>
        /// <param name="dtDetails">盘点汇总明细网格数据源</param>
        public void BindCheckDetailGrid(DataTable dtDetails)
        {
            dgDetail.DataSource = dtDetails;
        }

        /// <summary>
        /// 绑定盘点审核头表
        /// </summary>
        /// <param name="dtHeads">盘点审核头表网格数据源</param>
        public void BindAuditHeadGrid(DataTable dtHeads)
        {
            dgAuditHead.DataSource = dtHeads;
            if (dgAuditHead.Rows.Count > 0)
            {
                dgAuditHead.Rows[0].Selected = true;
                dgAuditHead.CurrentCell = dgAuditHead.Rows[0].Cells[0];
            }
            else
            {
                DataTable dt = (DataTable)dgAuditDetail.DataSource;
                if (dt != null)
                {
                    dt.Clear();
                }
            }
        }

        /// <summary>
        /// 绑定盘点审核明细
        /// </summary>
        /// <param name="dtDetails">盘点审核明细网格数据源</param>
        public void BindAuditDetailGrid(DataTable dtDetails)
        {
            dgAuditDetail.DataSource = dtDetails;
        }

        /// <summary>
        /// 取得盘点审核头查询条件
        /// </summary>
        /// <returns>表头查询条件</returns>
        public Dictionary<string, string> GetAuditHeadQueryCondition()
        {
            string deptId = cmbAuditStoreRoom.SelectedValue.ToString();
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            queryCondition.Add("DeptID", deptId);
            queryCondition.Add(string.Empty, "AuditTime between '" + dtpAuditDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '" + dtpAuditDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'");
            return queryCondition;
        }

        /// <summary>
        /// 取得盘点审核明细查询条件
        /// </summary>
        /// <returns>审核明细查询条件</returns>
        public Dictionary<string, string> GetAuditDetailQueryCondition()
        {
            if (dgAuditHead.CurrentCell != null)
            {
                if (dgAuditHead.CurrentCell.RowIndex >= 0)
                {
                    int currentIndex = dgAuditHead.CurrentCell.RowIndex;
                    DataRow currentRow = ((DataTable)(dgAuditHead.DataSource)).Rows[currentIndex];
                    Dictionary<string, string> rtn = new Dictionary<string, string>();
                    rtn.Add("a.AuditHeadID", currentRow["AuditHeadID"].ToString());
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
        /// 取得未审核汇总明细查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        public Dictionary<string, string> GetAllNotAuditDetailQueryCondition()
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            queryCondition.Add("a.DeptID", GetSelectedDeptId().ToString());
            return queryCondition;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmCheckAudit_OpenWindowBefore(object sender, EventArgs e)
        {
            //绑定库房下拉框
            InvokeController("GetDrugDept", frmName);

            //绑定盘点药品信息
            InvokeController("GetCheckDrugInfo", frmName);

            //绑定为什汇总数据
            btnGetNotCheckBills_Click(null, null);

            //绑定状态信息
            InvokeController("CheckStatusInfos", frmName, GetSelectedDeptId());

            //设置审核日期
            dtpAuditDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpAuditDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnPrintCheckBill_Click(object sender, EventArgs e)
        {
            if (dgAuditHead.CurrentCell != null)
            {
                if (dgAuditHead.CurrentCell.RowIndex >= 0)
                {
                    int rowIndex = dgAuditHead.CurrentCell.RowIndex;
                    DataRow dRow = ((DataTable)dgAuditHead.DataSource).Rows[rowIndex];
                    DataTable dtAuditHead = (DataTable)dgAuditHead.DataSource;
                    DataTable dtAuditDetail = (DataTable)dgAuditDetail.DataSource;
                    InvokeController("PrintCheckBill", frmName, cmbDept.Text, GetSelectedDeptId(), dRow, dtAuditHead, dtAuditDetail);
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnPrintLossBill_Click(object sender, EventArgs e)
        {
            if (dgAuditHead.CurrentCell != null)
            {
                if (dgAuditHead.CurrentCell.RowIndex >= 0)
                {
                    int rowIndex = dgAuditHead.CurrentCell.RowIndex;
                    DataRow dRow = ((DataTable)dgAuditHead.DataSource).Rows[rowIndex];
                    DataTable dtAuditHead = (DataTable)dgAuditHead.DataSource;
                    DataTable dtAuditDetail = (DataTable)dgAuditDetail.DataSource;
                    DataView dv = dtAuditDetail.DefaultView;
                    dv.RowFilter = "DiffNum<0";
                    DataTable dtLossBill = dv.ToTable();
                    InvokeController("PrintLossCheckBill", frmName, cmbDept.Text, GetSelectedDeptId(), dRow, dtAuditHead, dtLossBill);
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnPrintOverBill_Click(object sender, EventArgs e)
        {
            if (dgAuditHead.CurrentCell != null)
            {
                if (dgAuditHead.CurrentCell.RowIndex >= 0)
                {
                    int rowIndex = dgAuditHead.CurrentCell.RowIndex;
                    DataRow dRow = ((DataTable)dgAuditHead.DataSource).Rows[rowIndex];
                    DataTable dtAuditHead = (DataTable)dgAuditHead.DataSource;
                    DataTable dtAuditDetail = (DataTable)dgAuditDetail.DataSource;
                    DataView dv = dtAuditDetail.DefaultView;
                    dv.RowFilter = "DiffNum>0";
                    DataTable dtOverBill = dv.ToTable();
                    InvokeController("PrintOverCheckBill", frmName, cmbDept.Text, GetSelectedDeptId(), dRow, dtAuditHead, dtOverBill);
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnAuditClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnGetNotCheckBills_Click(object sender, EventArgs e)
        {
            //绑定未审核汇总明细
            InvokeController("LoadAllNotAuditDetail", frmName);

            //绑定状态信息
            InvokeController("CheckStatusInfos", frmName, GetSelectedDeptId());
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                //校验是否有待审单据
                if (dgDetail.Rows.Count <= 0)
                {
                    MessageBoxEx.Show("没有待审核的单据");
                    return;
                }

                if (MessageBox.Show("您确认要审核盘点单据吗?", "审核提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    InvokeController("AuditBill", frmName, GetSelectedDeptId());

                    //审核完刷新网格数据
                    btnGetNotCheckBills_Click(null, null);
                    //将选项卡定位到已审页面
                    tabControlCheck.SelectedTabIndex = 1;
                    //绑定状态信息
                    InvokeController("CheckStatusInfos", frmName, GetSelectedDeptId());
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception error)
            {
                this.Cursor = Cursors.Default;
                MessageBoxEx.Show(error.Message);
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
        private void dgAuditHead_CurrentCellChanged(object sender, EventArgs e)
        {
            InvokeController("LoadAuditCheckDetail", frmName);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void tabControlCheck_SelectedTabChanged(object sender, DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs e)
        {
            if (tabControlCheck.SelectedTabIndex == 1)
            {
                InvokeController("LoadAudtiCheckHead", frmName);
            }
            else
            {
                InvokeController("LoadAudtiCheckHead", frmName);
            }
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
        /// <param name="selectedValue">参数</param>
        private void txtCode_AfterSelectedRow(object sender, object selectedValue)
        {
            //根据药品编码和批次定位行列
            if (selectedValue != null)
            {
                DataRow dr = (DataRow)selectedValue;
                string drugId = dr["DrugID"].ToString();

                //明细数据
                DataTable dtDetails = (DataTable)dgDetail.DataSource;
                if (dtDetails == null)
                {
                    return;
                }

                if (dtDetails.Rows.Count == 0)
                {
                    MessageBoxEx.Show("没有数据无法定位查找");
                    return;
                }

                for (int index = 0; index < dtDetails.Rows.Count; index++)
                {
                    DataRow currentRow = dtDetails.Rows[index];
                    if (currentRow["DrugID"].ToString() == drugId)
                    {
                        dgDetail.CurrentCell = dgDetail["NotChemName", index];
                        break;
                    }
                    else
                    {
                        foreach (DataGridViewRow dgvr in dgDetail.Rows)
                        {
                            dgvr.Selected = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="selectedValue">参数</param>
        private void txtAuditCode_AfterSelectedRow(object sender, object selectedValue)
        {
            //根据药品编码和批次定位行列
            if (selectedValue != null)
            {
                DataRow dr = (DataRow)selectedValue;
                string drugId = dr["DrugID"].ToString();

                //明细数据
                DataTable dtDetails = (DataTable)dgAuditDetail.DataSource;
                if (dtDetails == null || dtDetails.Rows.Count == 0)
                {
                    MessageBoxEx.Show("没有数据无法定位查找");
                    return;
                }

                for (int index = 0; index < dtDetails.Rows.Count; index++)
                {
                    DataRow currentRow = dtDetails.Rows[index];
                    if (currentRow["DrugID"].ToString() == drugId)
                    {
                        dgAuditDetail.CurrentCell = dgAuditDetail["AuditChemName", index];
                        break;
                    }
                    else
                    {
                        foreach (DataGridViewRow dgvr in dgAuditDetail.Rows)
                        {
                            dgvr.Selected = false;
                        }
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
            if (dtpAuditDate.Bdate.Value > dtpAuditDate.Edate.Value)
            {
                MessageBoxEx.Show("审核日期开始日期不能大于结束日期");
                return;
            }

            InvokeController("LoadAudtiCheckHead", frmName);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnCloses_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }
        #endregion
    }
}
