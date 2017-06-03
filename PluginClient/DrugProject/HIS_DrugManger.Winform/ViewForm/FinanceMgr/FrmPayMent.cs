using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.FinanceMgr;

namespace HIS_DrugManage.Winform.ViewForm
{
    public partial class FrmPayMent : BaseFormBusiness, IFrmPayMent
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmPayMent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets当前行对象
        /// </summary>
        /// <value>当前行</value>
        public DataRow currentRow { get; set; }

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
        /// 绑定药剂科室控件
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
            queryCondition.Add(string.Empty, "BillTime between '" + dtpBillDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '" + dtpBillDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'");
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
            queryCondition.Add(string.Empty, "PayTime between '" + dtpBillDates.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '" + dtpBillDates.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'");
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
        /// <param name="dg">网格数据源</param>
        /// <returns>获取ID</returns>
        public Dictionary<string, string> GetHeadID(EfwControls.CustomControl.DataGrid dg)
        {
            int currentIndex = dg.CurrentCell.RowIndex;
            DataRow currentRows = ((DataTable)(dg.DataSource)).Rows[currentIndex];
            currentRow = currentRows;
            Dictionary<string, string> rtn = new Dictionary<string, string>();
            rtn.Add("InHeadID", currentRow["InHeadID"].ToString());
            return rtn;
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
        /// <param name="dtpayRecordData">付款记录表数据源</param>
        public void BindInPayRecordGrid(DataTable dtpayRecordData)
        {
            dgPayRecord.DataSource = dtpayRecordData;
        }

        /// <summary>
        /// 检测查询条件
        /// </summary>
        /// <param name="type">类型</param>
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
        #endregion

        #region 事件
        /// <summary>
        /// 打印事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            System.Data.DataTable printDt = InvokeController("PrintPayRecord", cmbDept.SelectedValue.ToString()) as System.Data.DataTable;
            if (printDt.Rows.Count <= 0)
            {
                MessageBoxEx.Show("没有可以打印的记录", "提示");
                return;
            }

            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            myDictionary.Add("开始时间", dtpBillDate.Bdate.Value);
            myDictionary.Add("结束时间", dtpBillDate.Edate.Value);
            myDictionary.Add("经销商", txtSupport.Text == string.Empty ? "所有经销商" : txtSupport.Text);
            myDictionary.Add("查询人", (InvokeController("this") as AbstractController).LoginUserInfo.EmpName);
            myDictionary.Add("查询时间", DateTime.Now);
            ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 4035, 0, myDictionary, printDt).PrintPreview(true);
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender">对象</param>
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
        /// 查询事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnSearchs_Click(object sender, EventArgs e)
        {
            if (CheckCondition(1))
            {
                InvokeController("LoadPayRecord", cmbDept.SelectedValue.ToString());
            }
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 第一次打开界面事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmPayMent_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("GetSupplyForShowCard");
            InvokeController("GetDrugDept");
            dtpBillDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpBillDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            dtpBillDates.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpBillDates.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
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
        /// 网格切换选中行事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgBillHead_CurrentCellChanged(object sender, EventArgs e)
        {
            InvokeController("LoadBillDetails", 0);
        }

        /// <summary>
        /// 付款事件
        /// </summary>
        /// <param name="sender">对象</param>
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
                        System.Data.DataTable dt = dgBillHead.DataSource as System.Data.DataTable;
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
        /// 选项卡切换事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void superTabItem2_Click(object sender, EventArgs e)
        {
            InvokeController("GetSupplyForShowCard");
            InvokeController("GetDrugDept");
            //dtpBillDates.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            //dtpBillDates.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            InvokeController("LoadPayRecord", cmbDept.SelectedValue.ToString());
        }

        /// <summary>
        /// 选项卡切换事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void superTabItem1_Click(object sender, EventArgs e)
        {
            InvokeController("GetSupplyForShowCard");
            InvokeController("GetDrugDept");
            //dtpBillDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            //dtpBillDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            InvokeController("LoadBillHead", cmbDept.SelectedValue.ToString(), 0);
        }

        /// <summary>
        /// 网格改变选中行事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgPayRecord_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgPayRecord.CurrentCell != null)
            {
                if (dgPayRecord.CurrentCell.RowIndex >= 0)
                {
                    int currentIndex = dgPayRecord.CurrentCell.RowIndex;
                    DataRow currentRow = ((System.Data.DataTable)(dgPayRecord.DataSource)).Rows[currentIndex];
                    string payRecordID = currentRow["PayRecordID"].ToString();
                    InvokeController("LoadBillHead", payRecordID, 1);
                }
            }
        }

        /// <summary>
        /// 网格改变选中行事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgBillHeads_CurrentCellChanged(object sender, EventArgs e)
        {
            InvokeController("LoadBillDetails", 1);
        }

        /// <summary>
        /// 取消付款事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnBackPay_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确实取消付款吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dgPayRecord.Rows.Count > 0)
                {
                    string ids = string.Empty;
                    System.Data.DataTable dt = dgPayRecord.DataSource as System.Data.DataTable;
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
        /// 键盘事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmPayMent_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    {
                        btnBackPay_Click(null, null);
                    }

                    break;
                case Keys.F3:
                    {
                        btnPay_Click(null, null);
                    }

                    break;
            }
        }

        /// <summary>
        /// 网格点击事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgBillHead_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                System.Data.DataTable dt = dgBillHead.DataSource as System.Data.DataTable;
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
        /// 网格点击事件
        /// </summary>
        /// <param name="sender">对象</param>
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

        /// <summary>
        /// 键盘事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void txtInvoiceNO_KeyUp(object sender, KeyEventArgs e)
        {
            string pattern = @"^[A-Za-z0-9]+$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(txtInvoiceNO.Text))
            {
                MessageBoxEx.Show("发票号只能为字母或数字");
                txtInvoiceNO.Text = string.Empty;
            }
        }

        /// <summary>
        /// 键盘事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void txtInvoiceNOs_KeyUp(object sender, KeyEventArgs e)
        {
            string pattern = @"^[A-Za-z0-9]+$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(txtInvoiceNOs.Text))
            {
                MessageBoxEx.Show("发票号只能为字母或数字");
                txtInvoiceNOs.Text = string.Empty;
            }
        }

        /// <summary>
        /// 导出事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnOut_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"ExportFile\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "财务付款明细表.xls";
            DataTable printDt = InvokeController("PrintPayRecord", cmbDept.SelectedValue.ToString()) as DataTable;
            if (printDt.Rows.Count <= 0)
            {
                MessageBoxEx.Show("没有可以导出的记录", "提示");
                return;
            }

            string strPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }

            Dictionary<string, string> dicColumns = new Dictionary<string, string>();
            dicColumns.Add("BillTime", "入库时间");
            dicColumns.Add("BillNo", "入库单号");
            dicColumns.Add("TotalRetailFee", "金额");
            dicColumns.Add("InvoiceNO", "发票号");
            Dictionary<string, string> dicDataFormat = new Dictionary<string, string>();
            dicDataFormat.Add("TotalRetailFee", "0.00");
            EFWCoreLib.CoreFrame.Common.ExcelHelper.Export(printDt, "财务付款明细表\n", dicColumns, dicDataFormat, path);
            MessageBoxEx.Show("财务付款明细表，保存路径为：\r\n" + path);
            System.Diagnostics.Process.Start("explorer.exe", AppDomain.CurrentDomain.BaseDirectory + "ExportFile");
        }
        #endregion

        private void ckAll_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgBillHead.DataSource as DataTable;
            int ischeck = 0;
            if (ckAll.Checked)
            {
                ischeck = 1;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["ck"] = ischeck;
            }
        }
    }
}
