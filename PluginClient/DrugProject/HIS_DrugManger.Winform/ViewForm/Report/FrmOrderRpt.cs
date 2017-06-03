using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.Report;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 药品明细账查询
    /// </summary>
    public partial class FrmOrderRpt : BaseFormBusiness, IFrmOrderRpt
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmOrderRpt()
        {
            InitializeComponent();
        }

        #region 自定义属性和方法
        /// <summary>
        /// 明细数据集
        /// </summary>
        private DataTable orderDt;

        /// <summary>
        /// 设置时间
        /// </summary>
        private void SetDateWay()
        {
            if (ratAccount.Checked)
            {
                lblYear.Visible = true;
                lblMonth.Visible = true;
                cmbQueryYear.Visible = true;
                cmbQueryMonth.Visible = true;

                lblDate.Visible = false;
                dtpDate.Visible = false;
            }
            else
            {
                lblYear.Visible = false;
                lblMonth.Visible = false;
                cmbQueryYear.Visible = false;
                cmbQueryMonth.Visible = false;

                lblDate.Visible = true;
                dtpDate.Visible = true;
            }
        }

        /// <summary>
        /// 显示单据信息
        /// </summary>
        /// <param name="shower">对象</param>
        private void ShowBillInfo(BillMasterShower shower)
        {
            if (shower != null)
            {
                pnlBillInfo.Visible = true;
                lblRegTime.Text = shower.RegTime.ToString("yyyy-MM-dd HH:mm:ss");
                lblAuditTime.Text = shower.AuditTime.ToString("yyyy-MM-dd HH:mm:ss");
                lblBillNo.Text = shower.BillNo;
                lblPatientNo.Text = shower.RelationPeopleNo;
                lblRelationPeople.Text = shower.RelationPeople;
                lblRelationUnit.Text = shower.RelationUnit;
                lblRetailFee.Text = shower.RetailFee.ToString("0.00");
                lblStockFee.Text = shower.StockFee.ToString("0.00");
                lblOpType.Text = shower.OpType;
                lblRegPeople.Text = shower.RegPeople;
                lblRemark.Text = shower.Remark;
            }
        }

        /// <summary>
        /// 获取查询时间
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        private void GetQueryTime(ref DateTime beginTime, ref DateTime endTime)
        {
            if (ratNature.Checked)
            {
                beginTime = Convert.ToDateTime(dtpDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00"));
                endTime = Convert.ToDateTime(dtpDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59"));
                return;
            }

            if (ratAccount.Checked)
            {
                DataTable dtActHis = (DataTable)InvokeController("GetBalanceDate", frmName, Convert.ToInt32(cmbDept.SelectedValue), Convert.ToInt32(cmbQueryYear.Text), Convert.ToInt32(cmbQueryMonth.Text));
                if (dtActHis != null && dtActHis.Rows.Count > 0)
                {
                    beginTime = Convert.ToDateTime(dtActHis.Rows[0][0]);
                    endTime = Convert.ToDateTime(dtActHis.Rows[0][0]);
                }
                else
                {
                    throw new Exception("查询的会计月尚未月结，无法统计");
                }
            }
        }
        #endregion

        #region 接口
        /// <summary>
        /// 绑定会计年
        /// </summary>
        /// <param name="dtYear">年</param>
        public void BindYear(DataTable dtYear)
        {
            if (dtYear == null)
            {
                return;
            }

            cmbQueryYear.DataSource = dtYear;
            cmbQueryYear.ValueMember = "ID";
            cmbQueryYear.DisplayMember = "Name";
            DataRow[] rows = dtYear.Select("ID=" + DateTime.Now.Year);
            if (rows.Length > 0)
            {
                cmbQueryYear.SelectedValue = DateTime.Now.Year;
                InvokeController("GetAcountMonths", frmName, Convert.ToInt32(cmbDept.SelectedValue), Convert.ToInt32(cmbQueryYear.SelectedValue));
                return;
            }

            if (dtYear.Rows.Count > 0)
            {
                cmbQueryYear.SelectedIndex = 0;
                InvokeController("GetAcountMonths", frmName, Convert.ToInt32(cmbDept.SelectedValue), Convert.ToInt32(cmbQueryYear.SelectedValue));
            }
        }

        /// <summary>
        /// 绑定会计月
        /// </summary>
        /// <param name="dtMonth">年</param>
        public void BindMonth(DataTable dtMonth)
        {
            DataView dv = dtMonth.DefaultView;
            dv.Sort = "ID asc";
            cmbQueryMonth.DataSource = dtMonth;
            cmbQueryMonth.ValueMember = "ID";
            cmbQueryMonth.DisplayMember = "Name";
            DataRow[] rows = dtMonth.Select("ID=" + DateTime.Now.Month);
            if (rows.Length > 0)
            {
                cmbQueryMonth.SelectedValue = DateTime.Now.Month;
                return;
            }

            if (dtMonth.Rows.Count > 0)
            {
                cmbQueryMonth.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定库房下拉框
        /// </summary>
        /// <param name="dtDept">科室数据</param>
        public void BindDeptRoom(DataTable dtDept)
        {
            cmbDept.DataSource = dtDept;
            cmbDept.ValueMember = "DeptID";
            cmbDept.DisplayMember = "DeptName";
            if (dtDept.Rows.Count > 0)
            {
                cmbDept.SelectedIndex = 0;
                InvokeController("GetAcountMonths", frmName, Convert.ToInt32(cmbDept.SelectedValue), Convert.ToInt32(cmbQueryYear.SelectedValue));
            }
        }

        /// <summary>
        /// 绑定药品信息ShowCard
        /// </summary>
        /// <param name="dt">数据源</param>
        public void BindDrugInfoCard(DataTable dt)
        {
            txtCode.DisplayField = "ChemName";
            txtCode.MemberField = "DrugID";
            txtCode.CardColumn = "DrugID|编码|55,ChemName|化学名称|240,Spec|规格|200,ProductName|生产厂家|120,PackUnit|单位|40";
            txtCode.QueryFieldsString = "ChemName,TradeName,PYCode,WBCode,TPYCode,TWBCode";
            txtCode.ShowCardWidth = 800;
            txtCode.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 绑定汇总信息
        /// </summary>
        /// <param name="dt">数据源</param>
        public void BindTotalInfo(DataTable dt)
        {
            dataGrid.DataSource = dt;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmDrugDetailBill_OpenWindowBefore(object sender, EventArgs e)
        {
            dtpDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            InvokeController("GetStoreRoom", frmName);
            InvokeController("GetDrugInfo", frmName);
            foreach (DataGridViewColumn dv in dataGrid.Columns)
            {
                dv.Width = Convert.ToInt32(Convert.ToDouble(dataGrid.Width) * 0.125);
            }

            if (cmbDept.SelectedValue != null)
            {
                //cmbQueryYear.SelectedIndexChanged -= new EventHandler(cmbQueryYear_SelectedIndexChanged);
                InvokeController("GetAcountYears", frmName, Convert.ToInt32(cmbDept.SelectedValue));

                //cmbQueryYear.SelectedIndexChanged += new EventHandler(cmbQueryYear_SelectedIndexChanged);
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
        private void ratNature_CheckedChanged(object sender, EventArgs e)
        {
            SetDateWay();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void ratAccount_CheckedChanged(object sender, EventArgs e)
        {
            SetDateWay();
            if (cmbQueryYear.SelectedValue != null)
            {
                InvokeController("GetAcountMonths", frmName, Convert.ToInt32(cmbDept.SelectedValue), Convert.ToInt32(cmbQueryYear.SelectedValue));
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void axGRDisplayViewer_ContentCellDblClick(object sender, Axgregn6Lib._IGRDisplayViewerEvents_ContentCellDblClickEvent e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int currentIndex = axGRDisplayViewer.SelRowNo;
                if (currentIndex >= 0 && orderDt != null)
                {
                    DataRow currentRow = orderDt.Rows[currentIndex];
                    if (currentRow["AccountType"].ToString() == "发生")
                    {
                        string opType = currentRow["BusiType"].ToString();//业务类型代码
                        int deptId = Convert.ToInt32(currentRow["DeptID"]);
                        int detailID = Convert.ToInt32(currentRow["DetailID"]);
                        BillMasterShower shower = (BillMasterShower)InvokeController("GetBillHeadInfo", frmName, opType, deptId, detailID);
                        ShowBillInfo(shower);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                this.Cursor = DefaultCursor;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (axGRDisplayViewer.Report == null)
                {
                    return;
                }

                axGRDisplayViewer.PostColumnLayout();
                axGRDisplayViewer.Report.PrintPreview(true);
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
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime beginTime = new DateTime();
                DateTime endTime = new DateTime();
                GetQueryTime(ref beginTime, ref endTime);
                axGRDisplayViewer.Stop();
                if (txtCode.Text.Trim() == string.Empty)
                {
                    MessageBoxEx.Show("请选择药品");
                    txtCode.Focus();
                    return;
                }

                int queryType = 1;
                if (ratAccount.Checked)
                {
                    if (cmbQueryYear.Text == string.Empty)
                    {
                        MessageBoxEx.Show("会计年不能为空");
                        cmbQueryYear.Focus();
                        return;
                    }

                    if (cmbQueryMonth.Text == string.Empty)
                    {
                        MessageBoxEx.Show("会计月不能为空");
                        cmbQueryMonth.Focus();
                        return;
                    }

                    queryType = 2;
                }
                else
                {
                    queryType = 1;
                }

                int deptId = Convert.ToInt32(cmbDept.SelectedValue);
                int drugId = Convert.ToInt32(txtCode.MemberValue);
                int queryYear = Convert.ToInt32(cmbQueryYear.SelectedValue);
                int queryMonth = Convert.ToInt32(cmbQueryMonth.SelectedValue);
                DataTable dtReport = (DataTable)InvokeController("GetAccountDetail", frmName, deptId, queryYear, queryMonth, beginTime.ToString("yyyy-MM-dd HH:mm:ss"), endTime.ToString("yyyy-MM-dd HH:mm:ss"), drugId, queryType);
                orderDt = dtReport;
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                myDictionary.Add("开始时间", beginTime.ToString("yyyy-MM-dd HH:mm:ss"));
                myDictionary.Add("结束时间", endTime.ToString("yyyy-MM-dd HH:mm:ss"));
                myDictionary.Add("名称", txtCode.Text);
                myDictionary.Add("生产厂家", ((DataRow)txtCode.SelectedValue)["ProductName"].ToString());
                myDictionary.Add("HospitalName", (InvokeController("this") as AbstractController).LoginUserInfo.WorkName + "药品明细账报表");
                GridReport gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 4017, 0, myDictionary, dtReport);
                axGRDisplayViewer.Report = gridreport.Report;
                axGRDisplayViewer.Start();
                axGRDisplayViewer.Refresh();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmbQueryYear_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbQueryYear.SelectedValue != null)
            {
                InvokeController("GetAcountMonths", frmName, Convert.ToInt32(cmbDept.SelectedValue), Convert.ToInt32(cmbQueryYear.SelectedValue));
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmDrugDetailBill_FormClosed(object sender, FormClosedEventArgs e)
        {
            axGRDisplayViewer.Stop();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmbDept_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbDept.SelectedValue != null)
            {
                //cmbQueryYear.SelectedIndexChanged -= new EventHandler(cmbQueryYear_SelectedIndexChanged);
                InvokeController("GetAcountYears", frmName, Convert.ToInt32(cmbDept.SelectedValue));

                //cmbQueryYear.SelectedIndexChanged += new EventHandler(cmbQueryYear_SelectedIndexChanged);
            }
        }
        #endregion
    }
}
