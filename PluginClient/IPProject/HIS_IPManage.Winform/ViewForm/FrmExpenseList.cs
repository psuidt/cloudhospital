using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    /// <summary>
    /// 费用清单控制器
    /// </summary>
    public partial class FrmExpenseList : BaseFormBusiness, IExpenseList
    {
        /// <summary>
        /// 报表数据
        /// </summary>
        Dictionary<string, object> myDictionary;

        /// <summary>
        /// 病人列表
        /// </summary>
        private DataTable dtPatientInfo;

        /// <summary>
        /// 机构名称
        /// </summary>
        private string workName = string.Empty;

        /// <summary>
        /// 病人列表
        /// </summary>
        public DataTable DtPatientInfo
        {
            get
            {
                return dtPatientInfo;
            }

            set
            {
                dtPatientInfo = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmExpenseList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体打开前加载数据
        /// </summary>
        /// <param name="sender">FrmExpenseList</param>
        /// <param name="e">事件参数</param>
        private void FrmExpenseList_OpenWindowBefore(object sender, EventArgs e)
        {
            InitComBox();
            cmbDept.SelectedIndex = 0;
            cmbListType.SelectedIndex = 0;
            workName = (InvokeController("this") as AbstractController).LoginUserInfo.WorkName;
            sDTIn.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            sDTIn.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            sDTFee.Bdate.Value = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00"));
            sDTFee.Edate.Value = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
            myDictionary = new Dictionary<string, object>();
            myDictionary.Add("HospitalName", workName);
            myDictionary.Add("Title", string.Empty);

            myDictionary.Add("SerialNumber", string.Empty);
            myDictionary.Add("BedNo", string.Empty);
            myDictionary.Add("PatName", string.Empty);
            myDictionary.Add("BANumber", string.Empty);
            myDictionary.Add("CostDate", string.Empty);
            myDictionary.Add("EnterHDate", string.Empty);
            myDictionary.Add("DeptName", string.Empty);

            myDictionary.Add("Fee", 0);
            myDictionary.Add("TotalDespoit", 0);
            myDictionary.Add("TotalFee", 0);
            myDictionary.Add("YE", 0);
        }

        /// <summary>
        /// 初始化下拉控件
        /// </summary>
        private void InitComBox()
        {
            InvokeController("GetWardDept");
            cmbListType.Items.Clear();
            ComboBoxItem cbxItem1 = new ComboBoxItem();
            cbxItem1.Text = "项目明细";
            cbxItem1.Tag = 1;
            cmbListType.Items.Add(cbxItem1);

            ComboBoxItem cbxItem2 = new ComboBoxItem();
            cbxItem2.Text = "一日清单";
            cbxItem2.Tag = 2;
            cmbListType.Items.Add(cbxItem2);

            ComboBoxItem cbxItem3 = new ComboBoxItem();
            cbxItem3.Text = "发票项目";
            cbxItem3.Tag = 3;
            cmbListType.Items.Add(cbxItem3);

            ComboBoxItem cbxItem4 = new ComboBoxItem();
            cbxItem4.Text = "项目汇总";
            cbxItem4.Tag = 4;
            cmbListType.Items.Add(cbxItem4);
        }

        #region 接口加载数据
        /// <summary>
        /// 加载科室列表
        /// </summary>
        /// <param name="deptList">科室列表</param>
        public void LoadDeptList(DataTable deptList)
        {
            cmbDept.Items.Clear();
            foreach (DataRow dr in deptList.Rows)
            {
                ComboBoxItem cbxItem = new ComboBoxItem();
                cbxItem.Text = dr["Name"].ToString();
                cbxItem.Tag = dr["DeptId"];
                cmbDept.Items.Add(cbxItem);
            }
        }

        /// <summary>
        /// 加载病人信息表
        /// </summary>
        /// <param name="dtPatientInfo">病人信息表</param>
        public void LoadDeptPatientInfoList(DataTable dtPatientInfo)
        {
            DtPatientInfo = dtPatientInfo;
            dgPatientList.DataSource = dtPatientInfo;
        }

        /// <summary>
        /// 加载报表数据
        /// </summary>
        /// <param name="dtPatientFeeSum">病人费用数据</param>
        /// <param name="dtPatientFeeInfo">住院总费用</param>
        public void LoadPatientFeeInfo(DataTable dtPatientFeeSum, DataTable dtPatientFeeInfo)
        {
            try
            {
                int iListType = Convert.ToInt32(((ComboBoxItem)cmbListType.SelectedItem).Tag);

                GetDataAndShowFromTable(dgvCostMsg, dtPatientFeeInfo);
                CreateSumCol(iListType);

                decimal totalFee = 0;
                foreach (DataRow dr in dtPatientFeeInfo.Rows)
                {
                    totalFee += Convert.ToDecimal(dr["TotalFee"]);
                }

                if (dtPatientFeeSum != null && dtPatientFeeSum.Rows.Count > 0)
                {
                    myDictionary["TotalDespoit"] = Convert.ToDecimal(dtPatientFeeSum.Rows[0]["TotalDespoit"]);
                    myDictionary["TotalFee"] = Convert.ToDecimal(dtPatientFeeSum.Rows[0]["TotalFee"]);
                    myDictionary["YE"] = Convert.ToDecimal(dtPatientFeeSum.Rows[0]["YE"]);
                    myDictionary["BANumber"] = dtPatientFeeSum.Rows[0]["CaseNumber"].ToString();
                }

                myDictionary["Fee"] = totalFee;

                ReportViewer.Stop();
                this.Cursor = Cursors.WaitCursor;
                GridReport gridreport = new GridReport();
                if (iListType == 1)
                {
                    myDictionary["Title"] = "项目明细清单";
                    gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 3006, 0, myDictionary, dtPatientFeeInfo);
                }
                else if (iListType == 2)
                {
                    myDictionary["Title"] = "一日清单";

                    DataTable dtPrint = new DataTable();
                    DataColumn col = new DataColumn();
                    col.ColumnName = "InFpName";
                    col.DataType = typeof(string);
                    dtPrint.Columns.Add(col);

                    DataColumn col1 = new DataColumn();
                    col1.ColumnName = "TotalFee";
                    col1.DataType = typeof(string);
                    dtPrint.Columns.Add(col1);

                    DataColumn col2 = new DataColumn();
                    col2.ColumnName = "InFpName1";
                    col2.DataType = typeof(string);
                    dtPrint.Columns.Add(col2);

                    DataColumn col3 = new DataColumn();
                    col3.ColumnName = "TotalFee1";
                    col3.DataType = typeof(string);
                    dtPrint.Columns.Add(col3);

                    DataColumn col4 = new DataColumn();
                    col4.ColumnName = "Date";
                    col4.DataType = typeof(string);
                    dtPrint.Columns.Add(col4);

                    for (int i = 0; i < dtPatientFeeInfo.Rows.Count; i++)
                    {
                        string sInFpName = dtPatientFeeInfo.Rows[i]["InFpName"].ToString();
                        decimal dTotalFee = Convert.ToDecimal(dtPatientFeeInfo.Rows[i]["TotalFee"]);
                        if (!Convert.ToBoolean(i % 2))  //偶数行
                        {
                            DataRow dr = dtPrint.NewRow();
                            dr["InFpName"] = sInFpName;
                            dr["TotalFee"] = dTotalFee;
                            dr["Date"] = dtPatientFeeInfo.Rows[i]["Date"];

                            dtPrint.Rows.Add(dr);
                        }
                        else
                        {
                            int j = i / 2;
                            dtPrint.Rows[j]["InFpName1"] = sInFpName;
                            dtPrint.Rows[j]["TotalFee1"] = dTotalFee;
                        }
                    }

                    //foreach (DataRow dr in dtPatientFeeInfo.Rows)
                    //{
                    //    string sInFpName = dr["InFpName"].ToString();
                    //    if (myDictionary.Keys.Contains(sInFpName))
                    //    {
                    //        myDictionary[sInFpName] = Convert.ToDecimal(dr["TotalFee"]);
                    //    }
                    //    else
                    //    {
                    //        myDictionary.Add(sInFpName, Convert.ToDecimal(dr["TotalFee"]));
                    //    }
                    //}



                    gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 3005, 0, myDictionary, dtPrint);
                }
                else if (iListType == 3)
                {
                    myDictionary["Title"] = "发票项目";
                    gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 3007, 0, myDictionary, dtPatientFeeInfo);
                }
                else if (iListType == 4)
                {
                    myDictionary["Title"] = "项目汇总";
                    gridreport = ReportTool.GetReport((InvokeController("this") as AbstractController).LoginUserInfo.WorkId, 3008, 0, myDictionary, dtPatientFeeInfo);
                }

                ReportViewer.Report = gridreport.Report;
                ReportViewer.Start();
                ReportViewer.Refresh();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region 病人查询按钮
        /// <summary>
        /// 查询病人
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnPatientQuery_Click(object sender, EventArgs e)
        {
            string sDeptCode = ((ComboBoxItem)cmbDept.SelectedItem).Tag.ToString();
            string sDTInBegin = sDTIn.Bdate.Value.ToString("yyyy-MM-dd 00:00:00");
            string sDTInEnd = sDTIn.Edate.Value.ToString("yyyy-MM-dd 23:59:59");
            int iPatientState = rcbIn.Checked ? 2 : (rcbOutNoJS.Checked ? 3 : 4);
            string sPatient = txtPatient.Text.Trim();
            int isJstate = 0;
            InvokeController("GetDeptPatientInfoList", sDeptCode, sDTInBegin, sDTInEnd, iPatientState, sPatient, isJstate);
        }

        /// <summary>
        /// 打印勾选的清单数据
        /// </summary>
        /// <param name="sender">btnPrintChecked</param>
        /// <param name="e">事件参数</param>
        private void btnPrintChecked_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int iListType = 2;
                int iJsState = -1;
                int iTimeType = 1;
                List<Dictionary<string, object>> dicList = new List<Dictionary<string, object>>();

                foreach (DataGridViewRow dr in dgPatientList.Rows)
                {
                    if (dr.Cells["Checked"].Value != null && dr.Cells["Checked"].Value.ToString() == "1")
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("HospitalName", workName);
                        dic.Add("Title", "一日清单");
                        dic.Add("PatientID", dr.Cells["PatListID"].Value.ToString());
                        dic.Add("SerialNumber", dr.Cells["SerialNumber"].Value.ToString());
                        dic.Add("BedNo", dr.Cells["BedNo"].Value.ToString());
                        dic.Add("PatName", dr.Cells["PatName"].Value.ToString());
                        dic.Add("BANumber", string.Empty);
                        dic.Add("CostDate", string.Empty);
                        dic.Add("EnterHDate", dr.Cells["EnterHDate"].Value.ToString());
                        dic.Add("DeptName", dr.Cells["DeptName"].Value.ToString());

                        dic.Add("Fee", 0);
                        dic.Add("TotalDespoit", Convert.ToDecimal(dr.Cells["TotalDespoit"].Value));
                        dic.Add("TotalFee", Convert.ToDecimal(dr.Cells["TotalFee"].Value));
                        dic.Add("YE", Convert.ToDecimal(dr.Cells["YE"].Value));
                        dicList.Add(dic);
                    }
                }

                if (dicList.Count <= 0)
                {
                    MessageBoxShowSimple("请先勾选病人！");
                    return;
                }

                InvokeController("PrintAllPatientData", dicList, iListType, iJsState, iTimeType);
            }
            catch (Exception ex)
            {
                MessageBoxShowSimple("打印出错：" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }
        #endregion

        #region 病人费用查询按钮
        /// <summary>
        /// 查询所选病人的费用清单
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnDetailQuery_Click(object sender, EventArgs e)
        {
            if (dgPatientList.Rows.Count > 0)
            {
                int iPatientId = Convert.ToInt32(dgPatientList.CurrentRow.Cells["PatListID"].Value);
                int iJsState = -1;
                GetPatientFeeList(iPatientId, iJsState);
            }
        }

        /// <summary>
        /// 打印某病人的费用清单
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ReportViewer.PostColumnLayout();
                if (ReportViewer.Report != null)
                {
                    ReportViewer.Report.PrintPreview(true);
                }
            }
            catch (Exception error)
            {
                MessageBoxEx.Show(error.Message);
            }
        }
        #endregion

        /// <summary>
        /// 封装获取病人信息
        /// </summary>
        /// <param name="iPatientId">病人入院登记ID</param>
        /// <param name="iJsState">计算状态 0.未结算 1，中途，2，出院，3，欠费</param>
        private void GetPatientFeeList(int iPatientId, int iJsState)
        {
            int iTimeType = ckbCreateDate.Checked ? 1 : 2;
            string sDTFeeBegin = sDTFee.Bdate.Value.ToString("yyyy-MM-dd 00:00:00");
            string sDTFeeEnd = sDTFee.Edate.Value.ToString("yyyy-MM-dd 23:59:59");
            int iListType = Convert.ToInt32(((ComboBoxItem)cmbListType.SelectedItem).Tag);

            CreateDgvHead(iListType);

            myDictionary["SerialNumber"] = dgPatientList.CurrentRow.Cells["SerialNumber"].Value.ToString();
            myDictionary["BedNo"] = dgPatientList.CurrentRow.Cells["BedNo"].Value.ToString();
            myDictionary["PatName"] = dgPatientList.CurrentRow.Cells["PatName"].Value.ToString();
            myDictionary["BANumber"] = string.Empty;
            myDictionary["CostDate"] = sDTFeeBegin + " 到 " + sDTFeeEnd;
            myDictionary["EnterHDate"] = dgPatientList.CurrentRow.Cells["EnterHDate"].Value.ToString();
            myDictionary["DeptName"] = dgPatientList.CurrentRow.Cells["DeptName"].Value.ToString();

            myDictionary["TotalDespoit"] = Convert.ToDecimal(dgPatientList.CurrentRow.Cells["TotalDespoit"].Value);
            myDictionary["TotalFee"] = Convert.ToDecimal(dgPatientList.CurrentRow.Cells["TotalFee"].Value);
            myDictionary["YE"] = Convert.ToDecimal(dgPatientList.CurrentRow.Cells["YE"].Value);

            InvokeController("GetPatientFeeInfo", iPatientId, iListType, sDTFeeBegin, sDTFeeEnd, iJsState, iTimeType);
        }

        /// <summary>
        /// 单选病人状态时 修改界面文字 出院日期/入院日期
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void RcbPatientState_Changed(object sender, CheckBoxChangeEventArgs e)
        {
            lbDtime.Text = rcbOut.Checked ? "出院日期" : "入院日期";
        }

        /// <summary>
        /// 选中病人
        /// </summary>
        /// <param name="sender">gPatientList</param>
        /// <param name="e">事件参数</param>
        private void dgPatientList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgPatientList.CurrentCell != null)
            {
                if (dgPatientList.CurrentCell.ColumnIndex == 0)
                {
                    dgPatientList.CurrentCell.Value = dgPatientList.CurrentCell.Value == null ? 1 : (dgPatientList.CurrentCell.Value.ToString() == "1" ? 0 : 1);
                }

                if (!exPanelFeeList.Expanded)
                { 
                    exPanelFeeList.Expanded = true;
                }

                int iPatientId = Convert.ToInt32(dgPatientList.CurrentRow.Cells["PatListID"].Value);
                int iJsState = -1;
                GetPatientFeeList(iPatientId, iJsState);
            }
        }

        /// <summary>
        /// 全选病人或反选
        /// </summary>
        /// <param name="sender">cbPatientAll</param>
        /// <param name="e">事件参数</param>
        private void cbPatientAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgPatientList.Rows)
            {
                dr.Cells["Checked"].Value = cbPatientAll.Checked ? 1 : 0;
            }
        }

        /// <summary>
        /// 打印病人费用数据
        /// </summary>
        /// <param name="sender">buttonItem1</param>
        /// <param name="e">事件参数</param>
        private void buttonItem1_Click(object sender, EventArgs e)
        {
            InvokeController("PrintPatientFeeInfo", 131, 56);
        }

        #region DataGridView的方法，暂时不用  但也不删  以防客户需要

        /// <summary>
        /// DataGridView的方法，暂时不用  但也不删  以防客户需要
        /// </summary>
        /// <param name="dgv">dgv</param>
        /// <param name="data">data</param>
        /// <returns>NUll</returns>
        private int GetDataAndShowFromTable(DataGridViewX dgv, DataTable data)
        {
            if (dgv == null)
            { 
                return 0;
            }

            if (data == null || data.Rows.Count == 0)
            { 
                return 0;
            }

            if (data.Columns.Count != dgv.Columns.Count)
            {
                DataGridViewColumn col = null;
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    //查找列是否存在
                    bool bNew = true;
                    foreach (DataGridViewColumn acol in dgv.Columns)
                    {
                        if (acol.Name == data.Columns[i].ColumnName)
                        {
                            col = acol;
                            bNew = false;
                            break;
                        }
                    }

                    if (bNew)
                    {
                        col = new DataGridViewTextBoxColumn();
                        col.Name = data.Columns[i].ColumnName;
                        col.Visible = false;      //不用显示
                        col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    col.SortMode = DataGridViewColumnSortMode.Automatic;

                    if (bNew)
                    { 
                        dgv.Columns.Add(col);
                    }

                    col.Dispose();
                }
            }

            dgv.Rows.Clear();

            foreach (DataRow dr in data.Rows)
            {
                dgv.Rows.Add(1);
                int iRow = dgv.Rows.Count - 1;

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    if (dgv.Columns[i].CellType.Name == "DataGridViewCheckBoxCell")
                    {
                        //单独处理CheckBox
                        DataGridViewCheckBoxColumn col = (DataGridViewCheckBoxColumn)dgv.Columns[i];
                        dgv.Rows[iRow].Cells[i].Value = 0;
                    }
                    else
                    {
                        if (dgv.Columns[i].Name == "FillColumn")
                        {
                            dgv.Rows[iRow].Cells[i].Value = string.Empty;
                        }
                        else
                        {
                            if (data.Columns.Contains(dgv.Columns[i].Name))
                            {
                                dgv.Rows[iRow].Cells[dgv.Columns[i].Name].Value = dr[dgv.Columns[i].Name];
                            }
                            else
                            {
                                dgv.Rows[iRow].Cells[dgv.Columns[i].Name].Value = string.Empty;
                            }
                        }
                    }
                }
            }

            return dgv.Rows.Count;
        }

        /// <summary>
        /// DataGridView的方法，暂时不用  但也不删  以防客户需要
        /// </summary>
        /// <param name="iListType">iListType</param>
        private void CreateDgvHead(int iListType)
        {
            dgvCostMsg.Columns.Clear();

            //按发票项目
            if (iListType == 3)
            {
                AddGridColumn("InFpName", "InFpName", "发票名称", -100, DataGridViewContentAlignment.MiddleCenter, string.Empty, false, false, dgvCostMsg, false);
                AddGridColumn("TotalFee", "TotalFee", "总费用", 90, DataGridViewContentAlignment.MiddleRight, "N2", false, false, dgvCostMsg, false);
            }
            else if (iListType == 1)
            {
                //按费用明细
                AddGridColumn("ItemID", "ItemID", "项目编码", 70, DataGridViewContentAlignment.MiddleRight, string.Empty, false, false, dgvCostMsg, false);
                AddGridColumn("ItemName", "ItemName", "项目名称", -200, DataGridViewContentAlignment.MiddleLeft, string.Empty, false, false, dgvCostMsg, false);
                AddGridColumn("Spec", "Spec", "规格", 150, DataGridViewContentAlignment.MiddleLeft, string.Empty, false, false, dgvCostMsg, false);
                AddGridColumn("PackUnit", "PackUnit", "包装单位", 70, DataGridViewContentAlignment.MiddleLeft, string.Empty, false, false, dgvCostMsg, false);
                AddGridColumn("SellPrice", "SellPrice", "价格", 70, DataGridViewContentAlignment.MiddleRight, "N4", false, false, dgvCostMsg, false);
                AddGridColumn("Amount", "Amount", "数量", 40, DataGridViewContentAlignment.MiddleRight, string.Empty, false, false, dgvCostMsg, false);
                AddGridColumn("Unit", "Unit", "单位", 60, DataGridViewContentAlignment.MiddleLeft, string.Empty, false, false, dgvCostMsg, false);
                AddGridColumn("TotalFee", "TotalFee", "费用", 80, DataGridViewContentAlignment.MiddleRight, "N2", false, false, dgvCostMsg, false);
                AddGridColumn("PresDate", "PresDate", "费用时间", 145, DataGridViewContentAlignment.MiddleLeft, string.Empty, false, false, dgvCostMsg, false);
                //AddGridColumn("ChargeDate", "ChargeDate", "记账时间", 145, DataGridViewContentAlignment.MiddleLeft, string.Empty, false, false, dgvCostMsg, false);
                AddGridColumn("IsYBFlag", "IsYBFlag", "医保", 40, DataGridViewContentAlignment.MiddleRight, string.Empty, false, false, dgvCostMsg, false);
            }
            else if (iListType == 4)
            {
                //按项目汇总
                AddGridColumn("InFpName", "InFpName", "发票", 90, DataGridViewContentAlignment.MiddleRight, string.Empty, false, false, dgvCostMsg, false);
                AddGridColumn("ItemID", "ItemID", "项目编码", 70, DataGridViewContentAlignment.MiddleRight, string.Empty, false, false, dgvCostMsg, false);
                AddGridColumn("ItemName", "ItemName", "项目名称", -200, DataGridViewContentAlignment.MiddleLeft, string.Empty, false, false, dgvCostMsg, false);
                AddGridColumn("Spec", "Spec", "规格", 150, DataGridViewContentAlignment.MiddleLeft, string.Empty, false, false, dgvCostMsg, false);
                AddGridColumn("PackUnit", "PackUnit", "包装单位", 70, DataGridViewContentAlignment.MiddleLeft, string.Empty, false, false, dgvCostMsg, false);
                AddGridColumn("SellPrice", "SellPrice", "价格", 70, DataGridViewContentAlignment.MiddleRight, "N4", false, false, dgvCostMsg, false);
                AddGridColumn("Amount", "Amount", "数量", 40, DataGridViewContentAlignment.MiddleRight, string.Empty, false, false, dgvCostMsg, false);
                AddGridColumn("Unit", "Unit", "单位", 60, DataGridViewContentAlignment.MiddleLeft, string.Empty, false, false, dgvCostMsg, false);
                AddGridColumn("TotalFee", "TotalFee", "费用", 80, DataGridViewContentAlignment.MiddleRight, "N2", false, false, dgvCostMsg, false);
                AddGridColumn("IsYBFlag", "IsYBFlag", "医保", 40, DataGridViewContentAlignment.MiddleRight, string.Empty, false, false, dgvCostMsg, false);
            }
            else if (iListType == 2)
            {
                //按一日清单
                AddGridColumn("Date", "Date", "记账日期", 145, DataGridViewContentAlignment.MiddleRight, string.Empty, false, false, dgvCostMsg, false);
                AddGridColumn("InFpName", "InFpName", "项目名称", -100, DataGridViewContentAlignment.MiddleCenter, string.Empty, false, false, dgvCostMsg, false);
                AddGridColumn("TotalFee", "TotalFee", "总费用", 90, DataGridViewContentAlignment.MiddleRight, "N2", false, false, dgvCostMsg, false);
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 创建支付方式列
        /// </summary>
        /// <param name="iListType">支付类型</param>
        private void CreateSumCol(int iListType)
        {
            if (dgvCostMsg.Columns.Count <= 0 || dgvCostMsg.Rows.Count <= 0)
            {
                return;
            }

            // 1-项目明细  2-一日清单  3-发票项目  4-项目汇总
            if (iListType == 1)
            {
                #region 按项目明细
                decimal dTotalFee = 0.00m;
                foreach (DataGridViewRow dgvr in dgvCostMsg.Rows)
                {
                    dTotalFee += Convert.ToDecimal(dgvr.Cells["TotalFee"].Value);
                }

                int iLastRowIndex = dgvCostMsg.Rows.Add(1);
                dgvCostMsg.Rows[iLastRowIndex].Cells["Unit"].Value = "共计：";
                dgvCostMsg.Rows[iLastRowIndex].Cells["TotalFee"].Value = dTotalFee.ToString();
                foreach (DataGridViewCell cell in dgvCostMsg.Rows[iLastRowIndex].Cells)
                {
                    cell.Style.ForeColor = Color.Red;
                    cell.Style.Font = new System.Drawing.Font(
                        "微软雅黑",
                        9F,
                        System.Drawing.FontStyle.Bold,
                        System.Drawing.GraphicsUnit.Point,
                        ((byte)(134)));
                }
                #endregion
            }
            else if ((iListType == 3) || (iListType == 2))
            {
                #region 按发票项目, 按一日清单
                decimal dTotalFee = 0.00m;
                foreach (DataGridViewRow dgvr in dgvCostMsg.Rows)
                {
                    dTotalFee += Convert.ToDecimal(dgvr.Cells["TotalFee"].Value);
                }

                int iLastRowIndex = dgvCostMsg.Rows.Add(1);
                dgvCostMsg.Rows[iLastRowIndex].Cells["InFpName"].Value = "共计：";
                dgvCostMsg.Rows[iLastRowIndex].Cells["TotalFee"].Value = dTotalFee.ToString();

                foreach (DataGridViewCell cell in dgvCostMsg.Rows[iLastRowIndex].Cells)
                {
                    cell.Style.ForeColor = Color.Red;
                    cell.Style.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                }
                #endregion
            }
            else if (iListType == 4)
            {
                #region 按项目汇总
                string strInFpName = Convert.ToString(dgvCostMsg.Rows[0].Cells["InFpName"].Value);
                decimal dEveryInFpTotal = 0.0m;
                decimal dAllTotalFee = 0.0m;
                for (int index = 0; index < dgvCostMsg.Rows.Count; index++)
                {
                    if (Convert.ToString(dgvCostMsg.Rows[index].Cells["InFpName"].Value) == strInFpName)
                    {
                        dAllTotalFee += Convert.ToDecimal(dgvCostMsg.Rows[index].Cells["TotalFee"].Value);
                        dEveryInFpTotal += Convert.ToDecimal(dgvCostMsg.Rows[index].Cells["TotalFee"].Value);
                    }
                    else
                    {
                        dgvCostMsg.Rows.Insert(index, 1);
                        foreach (DataGridViewCell cell in dgvCostMsg.Rows[index].Cells)
                        {
                            cell.Style.ForeColor = Color.Red;
                            cell.Style.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                        }

                        dgvCostMsg.Rows[index].Cells["Unit"].Value = strInFpName;
                        dgvCostMsg.Rows[index].Cells["TotalFee"].Value = Convert.ToString(dEveryInFpTotal);
                        index += 1;
                        strInFpName = Convert.ToString(dgvCostMsg.Rows[index].Cells["InFpName"].Value);
                        dEveryInFpTotal = Convert.ToDecimal(dgvCostMsg.Rows[index].Cells["TotalFee"].Value);
                        dAllTotalFee += Convert.ToDecimal(dgvCostMsg.Rows[index].Cells["TotalFee"].Value);
                    }
                }

                int iLastRowIndex = dgvCostMsg.Rows.Add();
                dgvCostMsg.Rows[iLastRowIndex].Cells["Unit"].Value = strInFpName;
                dgvCostMsg.Rows[iLastRowIndex].Cells["TotalFee"].Value = dEveryInFpTotal.ToString();
                foreach (DataGridViewCell cell in dgvCostMsg.Rows[iLastRowIndex].Cells)
                {
                    cell.Style.ForeColor = Color.Red;
                    cell.Style.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                }

                int iSumLastRowIndex = dgvCostMsg.Rows.Add();
                dgvCostMsg.Rows[iSumLastRowIndex].Cells["Unit"].Value = "合计";
                dgvCostMsg.Rows[iSumLastRowIndex].Cells["TotalFee"].Value = dAllTotalFee.ToString();
                foreach (DataGridViewCell cell in dgvCostMsg.Rows[iSumLastRowIndex].Cells)
                {
                    cell.Style.ForeColor = Color.Red;
                    cell.Style.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                }
                #endregion
            }
        }

        /// <summary>
        /// 动态增加网格列
        /// </summary>
        /// <param name="sColName">列名</param>
        /// <param name="sDataPropertyName">数据属性名</param>
        /// <param name="sHeadText">表头</param>
        /// <param name="iWidth">宽度</param>
        /// <param name="iAlignment">对齐方式</param>
        /// <param name="sStyleFormat">文字格式</param>
        /// <param name="sortable">排序</param>
        /// <param name="bFrozen">是否加粗</param>
        /// <param name="dgv">网格控件</param>
        /// <param name="checkColumn">选中列状态</param>
        private void AddGridColumn(
            string sColName,
            string sDataPropertyName,
            string sHeadText,
            int iWidth,
            DataGridViewContentAlignment iAlignment,
            string sStyleFormat,
            bool sortable,
            bool bFrozen,
            DataGridViewX dgv,
            bool checkColumn)
        {
            DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
            dataGridViewCellStyle.Alignment = iAlignment;
            if (sStyleFormat != string.Empty)
            {
                dataGridViewCellStyle.Format = sStyleFormat;
            }

            if (checkColumn)
            {
                DataGridViewCheckBoxColumn cbcell = new DataGridViewCheckBoxColumn();
                cbcell.Name = sColName;
                cbcell.HeaderText = sHeadText;
                cbcell.DataPropertyName = sDataPropertyName;
                cbcell.FalseValue = 0;
                cbcell.TrueValue = 1;
                if (iWidth > 0)
                {
                    cbcell.Width = iWidth;
                }
                else
                {
                    cbcell.MinimumWidth = iWidth * -1;
                    cbcell.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                }

                cbcell.DefaultCellStyle = dataGridViewCellStyle;
                cbcell.Frozen = bFrozen;
                cbcell.SortMode = sortable ? DataGridViewColumnSortMode.Automatic : DataGridViewColumnSortMode.NotSortable;
                dgv.Columns.Add(cbcell);
            }
            else
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.Name = sColName;
                col.HeaderText = sHeadText;
                col.DataPropertyName = sDataPropertyName;
                if (iWidth > 0)
                {
                    col.Width = iWidth;
                }
                else
                {
                    col.MinimumWidth = iWidth * -1;
                    col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                }

                col.DefaultCellStyle = dataGridViewCellStyle;
                col.Frozen = bFrozen;
                col.SortMode = sortable ? DataGridViewColumnSortMode.Automatic : DataGridViewColumnSortMode.NotSortable;
                dgv.Columns.Add(col);
            }
        }
        #endregion
    }
}
