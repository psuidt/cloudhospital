using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_MaterialManage.Winform.IView.FinanceMgr;

namespace HIS_MaterialManage.Winform.ViewForm
{
    /// <summary>
    /// 物资付款
    /// </summary>
    public partial class FrmMaterialPayMent : BaseFormBusiness, IFrmMaterialPayMent
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmMaterialPayMent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取当前选中行
        /// </summary>
        public DataRow CurrentRow { get; set; }

        #region 函数

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

            txtSupports.DisplayField = "SupportName";
            txtSupports.MemberField = "SupplierID";
            txtSupports.CardColumn = "SupplierID|编码|50,SupportName|供应商名称|auto";
            txtSupports.QueryFieldsString = "SupportName,PYCode,WBCode";
            txtSupports.ShowCardWidth = 250;
            txtSupports.ShowCardDataSource = dtSupply;
        }

        /// <summary>
        /// 绑定药剂科室
        /// </summary>
        /// <param name="dtDrugDept">数据源</param>
        public void BindDrugDept(DataTable dtDrugDept)
        {
            if (dtDrugDept != null)
            {
                cmbDept.DataSource = dtDrugDept;
                cmbDepts.DataSource = dtDrugDept;
                if (dtDrugDept.Rows.Count > 0)
                {
                    cmbDept.SelectedIndex = 0;
                }

                if (dtDrugDept.Rows.Count > 0)
                {
                    cmbDepts.SelectedIndex = 0;
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
            queryCondition.Add(
                string.Empty,
                "BillTime between '" +
                dtpBillDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '"
                + dtpBillDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'");
            queryCondition.Add("DeptID", deptID.ToString());
            queryCondition.Add("PayFlag", "0");

            if (!string.IsNullOrEmpty(txtInvoiceNOs.Text))
            {
                queryCondition.Add("InvoiceNo", "'" + txtInvoiceNOs.Text.Trim() + "'");
            }

            if (txtSupport.MemberValue != null)
            {
                queryCondition.Add("SupplierID", txtSupport.MemberValue.ToString());
            }

            return queryCondition;
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>表头查询条件</returns>
        public Dictionary<string, string> GetQueryConditions(int deptID)
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            queryCondition.Add(
                string.Empty,
                "PayTime between '" +
                dtpBillDates.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '"
                + dtpBillDates.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'");
            queryCondition.Add("PayDeptID", deptID.ToString());
            if (!string.IsNullOrEmpty(txtInvoiceNOs.Text))
            {
                queryCondition.Add("LikeSearch", "InvoiceNO like '%" + txtInvoiceNOs.Text + "%'");
            }

            if (txtSupports.MemberValue != null)
            {
                queryCondition.Add("SupportName", txtSupports.MemberValue.ToString());
            }

            return queryCondition;
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
        /// 绑定明细
        /// </summary>
        /// <param name="dtInstoreDetails">明细网格数据源</param>
        public void BindInDetailGrid(DataTable dtInstoreDetails)
        {
            dgBillDetail.DataSource = dtInstoreDetails;
        }

        /// <summary>
        /// 绑定明细
        /// </summary>
        /// <param name="dtInstoreDetails">明细网格数据源</param>
        public void BindInDetailGrids(DataTable dtInstoreDetails)
        {
            dgBillDetails.DataSource = dtInstoreDetails;
        }

        /// <summary>
        /// 绑定头表
        /// </summary>
        /// <param name="dtInstoreHead">表头网格数据源</param>
        public void BindInHeadGrid(DataTable dtInstoreHead)
        {
            dgBillHead.DataSource = dtInstoreHead;
        }

        /// <summary>
        /// 绑定头表
        /// </summary>
        /// <param name="dtInstoreHead">表头网格数据源</param>
        public void BindInHeadGrids(DataTable dtInstoreHead)
        {
            dgBillHeads.DataSource = dtInstoreHead;
        }

        /// <summary>
        /// 绑定付款记录表
        /// </summary>
        /// <param name="dtpayRecordData">付款记录列表</param>
        public void BindInPayRecordGrid(DataTable dtpayRecordData)
        {
            dgPayRecord.DataSource = dtpayRecordData;
        }

        /// <summary>
        /// 检测查询条件
        /// </summary>
        /// <param name="type">时间类型</param>
        /// <returns>检测结果</returns>
        private bool CheckCondition(int type)
        {
            if (type == 0)
            {
                if (dtpBillDate.Bdate.Value.CompareTo(dtpBillDate.Edate.Value) > 0)
                {
                    MessageBoxEx.Show("开始时间不能小于结束时间", "错误提示");
                    dtpBillDate.Focus();
                    return false;
                }
            }
            else
            {
                if (dtpBillDates.Bdate.Value.CompareTo(dtpBillDates.Edate.Value) > 0)
                {
                    MessageBoxEx.Show("开始时间不能小于结束时间", "错误提示");
                    dtpBillDates.Focus();
                    return false;
                }
            }

            return true;
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
                    return GetHeadID(dgBillHead);
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
        /// 获取选中表头ID
        /// </summary>
        /// <returns>当前选中表头ID</returns>
        public Dictionary<string, string> GetCurrentHeadIDs()
        {
            if (dgBillHeads.CurrentCell != null)
            {
                if (dgBillHeads.CurrentCell.RowIndex >= 0)
                {
                    return GetHeadID(dgBillHeads);
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
        /// 根据传入的网格获取ID
        /// </summary>
        /// <param name="dg">网格控件</param>
        /// <returns>ID字典集合</returns>
        public Dictionary<string, string> GetHeadID(EfwControls.CustomControl.DataGrid dg)
        {
            int currentIndex = dg.CurrentCell.RowIndex;
            DataRow currentRow = ((DataTable)(dg.DataSource)).Rows[currentIndex];
            CurrentRow = currentRow;
            Dictionary<string, string> rtn = new Dictionary<string, string>();
            rtn.Add("InHeadID", currentRow["InHeadID"].ToString());
            return rtn;
        }

        /// <summary>
        /// 验证发票号是否为非法字符
        /// </summary>
        /// <param name="text">发票号</param>
        /// <returns>true：验证通过</returns>
        public bool CheckInvoiceNO(string text)
        {
            string pattern = @"^[A-Za-z0-9]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(text);
        }

        #endregion

        #region 事件

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnPay_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInvoiceNO.Text))
            {
                if (dgBillHead.Rows.Count > 0)
                {
                    if (MessageBoxEx.Show("确实付款吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string ids = string.Empty;
                        DataTable dt = dgBillHead.DataSource as DataTable;
                        decimal totalRetailFee = 0;
                        decimal totalStockFee = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dt.Rows[i]["ck"]) == 1)
                            {
                                decimal currentRetailFee = 0;
                                decimal currentStockFee = 0;
                                if (decimal.TryParse(dt.Rows[i]["RetailFee"].ToString(), out currentRetailFee))
                                {
                                }

                                if (decimal.TryParse(dt.Rows[i]["StockFee"].ToString(), out currentStockFee))
                                {
                                }

                                ids += dt.Rows[i]["InHeadID"] + ",";
                                totalRetailFee += currentRetailFee;
                                totalStockFee += currentStockFee;
                            }
                        }

                        if (ids.Contains(","))
                        {
                            ids = ids.Substring(0, ids.Length - 1);
                        }

                        if (!string.IsNullOrEmpty(ids))
                        {
                            InvokeController("BillPay", cmbDept.SelectedValue.ToString(), txtInvoiceNO.Text, ids, totalRetailFee, totalStockFee);
                            InvokeController("LoadBillHead", cmbDept.SelectedValue.ToString(), 0);
                        }
                        else
                        {
                            MessageBoxEx.Show("请选择需要付款的记录");
                        }
                    }
                }
                else
                {
                    MessageBoxEx.Show("没有可付款记录");
                }
            }
            else
            {
                MessageBoxEx.Show("请输入发票号");
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (CheckCondition(0))
            {
                if (cmbDept.SelectedValue != null)
                {
                    int id = 0;
                    if (int.TryParse(cmbDept.SelectedValue.ToString(), out id))
                    {
                        InvokeController("LoadBillHead", cmbDept.SelectedValue.ToString(), 0);
                    }
                }
                else
                {
                    MessageBoxEx.Show("请选择科室");
                }
            }
        }

        /// <summary>
        /// 窗体初始化数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmMaterialPayMent_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("GetSupplyForShowCard");
            InvokeController("GetMaterialDept");
            dtpBillDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpBillDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            if (cmbDept.SelectedValue != null)
            {
                int id = 0;
                if (int.TryParse(cmbDept.SelectedValue.ToString(), out id))
                {
                    InvokeController("LoadBillHead", cmbDept.SelectedValue.ToString(), 0);
                }
            }
        }

        /// <summary>
        /// 选择交款单头加载明细
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void dgBillHead_CurrentCellChanged(object sender, EventArgs e)
        {
            InvokeController("LoadBillDetails", 0);
        }

        /// <summary>
        /// Tab切换
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void superTabItem1_Click(object sender, EventArgs e)
        {
            InvokeController("GetSupplyForShowCard");
            InvokeController("GetMaterialDept");
            dtpBillDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpBillDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            InvokeController("LoadBillHead", cmbDept.SelectedValue.ToString(), 0);
        }

        /// <summary>
        /// Tab切换
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void superTabItem2_Click(object sender, EventArgs e)
        {
            InvokeController("GetSupplyForShowCard");
            InvokeController("GetMaterialDept");
            dtpBillDates.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpBillDates.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            InvokeController("LoadPayRecord", cmbDept.SelectedValue.ToString());
        }

        /// <summary>
        /// 选择交款记录加载交款明细
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void dgPayRecord_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgPayRecord.CurrentCell != null)
            {
                if (dgPayRecord.CurrentCell.RowIndex >= 0)
                {
                    int currentIndex = dgPayRecord.CurrentCell.RowIndex;
                    DataRow currentRow = ((DataTable)(dgPayRecord.DataSource)).Rows[currentIndex];
                    string payRecordID = currentRow["PayRecordID"].ToString();
                    InvokeController("LoadBillHead", payRecordID, 1);
                }
            }
        }

        /// <summary>
        /// 输入发票号自动验证
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void txtInvoiceNOs_KeyUp(object sender, KeyEventArgs e)
        {
            if (!CheckInvoiceNO(txtInvoiceNOs.Text))
            {
                MessageBoxEx.Show("发票号只能为字母或数字");
                txtInvoiceNOs.Text = string.Empty;
            }
        }

        /// <summary>
        /// 输入发票号自动验证
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void txtInvoiceNO_KeyUp(object sender, KeyEventArgs e)
        {
            if (!CheckInvoiceNO(txtInvoiceNO.Text))
            {
                MessageBoxEx.Show("发票号只能为字母或数字");
                txtInvoiceNO.Text = string.Empty;
            }
        }

        /// <summary>
        /// 选择交款单头加载交款单详情
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void dgBillHead_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataTable dt = dgBillHead.DataSource as DataTable;
                if (Convert.ToInt32(dt.Rows[e.RowIndex]["ck"]) == 0)
                {
                    dt.Rows[e.RowIndex]["ck"] = 1;
                }
                else
                {
                    dt.Rows[e.RowIndex]["ck"] = 0;
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnSearchs_Click(object sender, EventArgs e)
        {
            if (CheckCondition(1))
            {
                InvokeController("LoadPayRecord", cmbDept.SelectedValue.ToString());
            }
        }

        /// <summary>
        /// 取消付款
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnBackPay_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确实取消付款吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dgPayRecord.Rows.Count > 0)
                {
                    string ids = string.Empty;
                    DataTable dt = dgPayRecord.DataSource as DataTable;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dt.Rows[i]["cks"]) == 1)
                        {
                            ids += dt.Rows[i]["PayRecordID"] + ",";
                        }
                    }

                    if (ids.Contains(","))
                    {
                        ids = ids.Substring(0, ids.Length - 1);
                    }

                    if (!string.IsNullOrEmpty(ids))
                    {
                        InvokeController("CancelBillPay", ids);
                        InvokeController("LoadPayRecord", cmbDept.SelectedValue.ToString());
                    }
                    else
                    {
                        MessageBoxEx.Show("请选择需要取消付款的记录");
                    }
                }
                else
                {
                    MessageBoxEx.Show("没有可取消付款记录");
                }
            }
        }

        /// <summary>
        /// 选择付款记录加载详情
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void dgPayRecord_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataTable dt = dgPayRecord.DataSource as DataTable;
                if (Convert.ToInt32(dt.Rows[e.RowIndex]["cks"]) == 0)
                {
                    dt.Rows[e.RowIndex]["cks"] = 1;
                }
                else
                {
                    dt.Rows[e.RowIndex]["cks"] = 0;
                }
            }
        }
        #endregion
    }
}
