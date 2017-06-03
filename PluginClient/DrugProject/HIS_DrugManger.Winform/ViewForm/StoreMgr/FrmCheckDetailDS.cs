using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_Entity.DrugManage;
using static HIS_Entity.DrugManage.DGEnum;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 盘点单
    /// </summary>
    public partial class FrmCheckDetailDS : BaseFormBusiness, IFrmCheckDetail
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmCheckDetailDS()
        {
            InitializeComponent();
            frmFormInHead.AddItem(txtBillNo, "BillNO");
            frmFormInHead.AddItem(dtiBillDate, "RegTime");
        }

        #region 自定义方法属性
        /// <summary>
        /// 当前明细网格编辑状态
        /// </summary>
        private DGEnum.DetailsEditiStatus gridStatus;

        /// <summary>
        /// 需从后台删除的单据明细
        /// </summary>
        private List<int> lstDeleteDetails = new List<int>();

        /// <summary>
        /// Gets or sets设置网格状态
        /// </summary>
        public DGEnum.DetailsEditiStatus GridStatus
        {
            get
            {
                return gridStatus;
            }

            set
            {
                gridStatus = value;
                if (gridStatus == DetailsEditiStatus.UPDATING)
                {
                    dgDetails.Columns["DrugID"].ReadOnly = false;
                    dgDetails.Columns["ChemName"].ReadOnly = true;
                    dgDetails.Columns["Spec"].ReadOnly = true;
                    dgDetails.Columns["ProductName"].ReadOnly = true;
                    dgDetails.Columns["Place"].ReadOnly = true;
                    dgDetails.Columns["RetailPrice"].ReadOnly = true;
                    dgDetails.Columns["StockPrice"].ReadOnly = true;
                    dgDetails.Columns["ActPackNum"].ReadOnly = true;
                    dgDetails.Columns["PackUnit"].ReadOnly = true;
                    dgDetails.Columns["ActBaseNum"].ReadOnly = true;
                    dgDetails.Columns["UnitName"].ReadOnly = true;
                    dgDetails.Columns["FactPackNum"].ReadOnly = false;
                    dgDetails.Columns["FactBaseNum"].ReadOnly = false;
                }
                else
                {
                    dgDetails.ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// 给网格数据源设置正则表达式
        /// </summary>
        /// <param name="dicExpress">正则表达式(列名+表达式)</param>
        /// <param name="dtSource">数据源</param>
        public void SetExpress(Dictionary<string, string> dicExpress, DataTable dtSource)
        {
            if (dicExpress != null && dtSource != null)
            {
                foreach (var pair in dicExpress)
                {
                    string colName = pair.Key;
                    string express = pair.Value.ToString();
                    if (dtSource.Columns.Contains(colName))
                    {
                        if (dtSource.Columns[colName].ExtendedProperties.ContainsKey("Regex"))
                        {
                            dtSource.Columns[colName].ExtendedProperties.Remove("Regex");
                            dtSource.Columns[colName].ExtendedProperties.Add("Regex", express);
                        }
                        else
                        {
                            dtSource.Columns[colName].ExtendedProperties.Add("Regex", express);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 检查网格
        /// </summary>
        /// <param name="dtSource">数据源</param>
        /// <param name="checkRows">检查行(为空表全部)</param>
        /// <param name="uniqueCol">唯一列名(为空则没有主键)</param>
        /// <param name="errMsg">错误信息</param>
        /// <param name="errCol">错误列名</param>
        /// <param name="errRow">错误行索引</param>
        /// <returns>是否有重复记录</returns>
        public bool CheckDetails(DataTable dtSource, List<int> checkRows, string[] uniqueCol, out string errMsg, out string errCol, out int errRow)
        {
            errMsg = string.Empty;
            errCol = string.Empty;
            errRow = -1;
            for (int index = 0; index < dtSource.Rows.Count; index++)
            {
                if (checkRows != null &&
                    checkRows.FindIndex((x) => { return x == index; }) < 0)
                {
                    continue;
                }

                DataRow dRow = dtSource.Rows[index];
                //重复性检查
                if (uniqueCol != null)
                {
                    string colName = string.Empty;
                    for (int temp = index + 1; temp < dtSource.Rows.Count; temp++)
                    {
                        bool isUnique = false;
                        foreach (string name in uniqueCol)
                        {
                            errCol = name;
                            colName += (name + ",");
                            if (dRow[name].ToString() != dtSource.Rows[temp][name].ToString())
                            {
                                isUnique = true;
                                break;
                            }
                        }

                        if (!isUnique)
                        {
                            errRow = temp;
                            errMsg = "【{0}】不允许重复，请重新录入";
                            return false;
                        }
                    }
                }

                //按每列对正则表达式判断
                for (int count = 0; count < dtSource.Columns.Count; count++)
                {
                    object key = "Regex";
                    if (dtSource.Columns[count].ExtendedProperties.Contains(key))
                    {
                        string express = dtSource.Columns[count].ExtendedProperties[key].ToString();
                        if (express != string.Empty)
                        {
                            if (Regex.IsMatch(dRow[count].ToString(), express))
                            {
                                continue;
                            }
                            else
                            {
                                if ((count == 32 || count == 33) && Convert.ToDecimal(dRow[count]) < 0)
                                {
                                    errMsg = "【{0}】的录入数据格式不正确，请重新录入";
                                    errCol = dtSource.Columns[count].ColumnName;
                                    errRow = index;
                                    return false;
                                }
                                else if(count!=32 && count != 33)
                                {
                                    errMsg = "【{0}】的录入数据格式不正确，请重新录入";
                                    errCol = dtSource.Columns[count].ColumnName;
                                    errRow = index;
                                    return false;
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 检查提交单据信息
        /// </summary>
        /// <returns>是否通过</returns>
        private bool CheckBill()
        {
            //明细数据验证
            DataTable dtDetails = (DataTable)dgDetails.DataSource;
            if (dtDetails.Rows.Count == 0)
            {
                MessageBoxEx.Show("没有数据需要提交");
                return false;
            }

            Dictionary<string, string> dicExpress = new Dictionary<string, string>();
            dicExpress.Add("DrugID", @"^[1-9]\d*$");//正整数
            dicExpress.Add("FactPackNum", @"^\+?(0|[1-9][0-9]*)$");//包含零的正整数 
            dicExpress.Add("FactBaseNum", @"^\+?(0|[1-9][0-9]*)$");//包含零的正整数           
            SetExpress(dicExpress, dtDetails);
            string errMsg = string.Empty;
            string errCol = string.Empty;
            int errRow = -1;
            bool rtn = CheckDetails(dtDetails, null, new string[1] { "DrugID" }, out errMsg, out errCol, out errRow);
            if (!rtn)
            {
                MessageBoxEx.Show(string.Format(errMsg, dgDetails.Columns[errCol].HeaderText));
                dgDetails.CurrentCell = dgDetails[errCol, errRow];
            }

            return rtn;
        }

        /// <summary>
        /// 计算盘存基本数量
        /// </summary>
        private void CalcFactBasicAmount()
        {
            DataTable dt = dgDetails.DataSource as DataTable;
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[dt.Rows.Count - 1][1] == DBNull.Value)
                {
                    dt.Rows.Remove(dt.Rows[dt.Rows.Count - 1]);
                }
            }

            foreach (DataRow currentRow in dt.Rows)
            {
                decimal factPackNum = currentRow["FactPackNum"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["FactPackNum"]);
                decimal factBaseNum = currentRow["FactBaseNum"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["FactBaseNum"]);
                decimal unitAmount = Convert.ToDecimal(currentRow["UnitAmount"]);
                decimal stockPrice = currentRow["StockPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["StockPrice"]);
                decimal retailPrice = currentRow["RetailPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["RetailPrice"]);
                currentRow["FactStockFee"] = (stockPrice / unitAmount) * (factPackNum * unitAmount + factBaseNum);
                currentRow["FactRetailFee"] = (retailPrice / unitAmount) * (factPackNum * unitAmount + factBaseNum);
                currentRow["FactAmount"] = factPackNum * unitAmount + factBaseNum;
                if (currentRow["FactBaseNum"].ToString().Trim() == string.Empty)
                {
                    currentRow["FactBaseNum"] = 0;
                }
            }
        }
        #endregion

        #region 接口实现
        /// <summary>
        /// 插入提取数据
        /// </summary>
        /// <param name="dtRtn">提取数据</param>
        public void InsertGetStorageData(DataTable dtRtn)
        {
            decimal retailPrice = 0;
            decimal stockPrice = 0;
            decimal amount = 0;
            DataTable dtSource = (DataTable)dgDetails.DataSource;
            if (dtRtn != null && dtRtn.Rows.Count >= 0)
            {
                foreach (DataRow dr in dtRtn.Rows)
                {
                    DataRow row = dtSource.NewRow();
                    amount = dr["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Amount"]);
                    retailPrice = dr["RetailPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["RetailPrice"]);
                    stockPrice = dr["StockPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["StockPrice"]);
                    row["StorageID"] = dr["StorageID"];
                    row["Place"] = dr["Place"];
                    row["DeptID"] = dr["DeptID"];
                    row["CTypeID"] = dr["CTypeID"];
                    row["DrugID"] = dr["DrugID"];
                    row["ActAmount"] = dr["Amount"];
                    row["ActStockFee"] = dr["ActStockFee"];
                    row["ActRetailFee"] = dr["ActRetailFee"];
                    row["UnitID"] = dr["MiniUnitID"];
                    row["UnitName"] = dr["MiniUnit"];
                    row["UnitAmount"] = dr["UnitAmount"];
                    row["AuditFlag"] = 0;
                    row["BillTime"] = DateTime.Now;
                    row["RetailPrice"] = retailPrice;
                    row["StockPrice"] = stockPrice;
                    row["PackUnit"] = dr["PackUnit"];

                    row["ChemName"] = dr["ChemName"];
                    row["Spec"] = dr["Spec"];
                    row["ProductName"] = dr["ProductName"];

                    row["DosageName"] = dr["DosageName"];
                    row["TypeName"] = dr["TypeName"];

                    row["ActPackNum"] = dr["PackNum"];
                    row["ActBaseNum"] = dr["BaseNum"];
                    dtSource.Rows.Add(row);
                }
            }

            //触发盘存数默认账存数
            BtnSetFactAmount_Click(null, null);
            InvokeController("ComputeTotalFee", frmName);

            //设置新增单据录入状态
            chkInput.Checked = true;
            chkInput_CheckedChanged(null, null);

            //定位
            if (dtSource.Rows.Count > 0)
            {
                dgDetails.CurrentCell = dgDetails["FactPackNum", 0];
            }
        }

        /// <summary>
        ///绑定盘点表头实体
        /// </summary>
        /// <param name="inHead">表头实体</param>
        public void BindInHeadInfo<THead>(THead inHead)
        {
            if (inHead != null)
            {
                if (inHead as DW_CheckHead != null)
                {
                    frmFormInHead.Load(inHead);
                    txtBillNo.Text = (inHead as DW_CheckHead).BillNO.ToString();
                    dtiBillDate.Value = (inHead as DW_CheckHead).RegTime;
                }
                else if (inHead as DS_CheckHead != null)
                {
                    frmFormInHead.Load(inHead);
                    txtBillNo.Text = (inHead as DS_CheckHead).BillNO.ToString();
                    dtiBillDate.Value = (inHead as DS_CheckHead).RegTime;
                }
            }
        }

        /// <summary>
        /// 从界面获取药库盘点表头信息
        /// </summary>
        /// <returns>药库盘点表头信息</returns>
        public DW_CheckHead GetInHeadInfoDW()
        {
            DW_CheckHead inHead = new DW_CheckHead();
            frmFormInHead.GetValue<DW_CheckHead>(inHead);
            return inHead;
        }

        /// <summary>
        /// 从界面获取药房盘点表头信息
        /// </summary>
        /// <returns>药房盘点表头信息</returns>
        public DS_CheckHead GetInHeadInfoDS()
        {
            DS_CheckHead inHead = new DS_CheckHead();
            frmFormInHead.GetValue<DS_CheckHead>(inHead);
            return inHead;
        }

        /// <summary>
        /// 绑定明细表信息
        /// </summary>
        /// <param name="inDetails">明细数据源</param>
        public void BindInDetails(DataTable inDetails)
        {
            if (inDetails != null)
            {
                dgDetails.EndEdit();
                if (inDetails.Rows.Count > 0)
                {
                    dgDetails.DataSource = inDetails;
                    dgDetails.CurrentCell = dgDetails[1, 0];
                }
                else
                {
                    dgDetails.DataSource = inDetails;
                }
            }
        }

        /// <summary>
        /// 绑定入库单录入ShowCard
        /// </summary>
        /// <param name="dtDrugInfo">药品信息</param>
        public void BindDrugInfoCard(DataTable dtDrugInfo)
        {
            dgDetails.SelectionCards[0].BindColumnIndex = 0;
            dgDetails.SelectionCards[0].CardColumn = "DrugID|编码|55,ChemName|化学名称|auto,Spec|规格|200,ProductName|生产厂家|200,PackUnit|单位|40";
            dgDetails.SelectionCards[0].CardSize = new System.Drawing.Size(720, 276);
            dgDetails.SelectionCards[0].QueryFieldsString = "ChemName,TradeName,PYCode,WBCode,TPYCode,TWBCode";
            dgDetails.SelectionCards[0].SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByFirstChar;
            dgDetails.BindSelectionCardDataSource(0, dtDrugInfo);
        }

        /// <summary>
        /// 绑定药品定位查询ShowCard
        /// </summary>
        /// <param name="dtDrugInfo">药品信息</param>
        public void BindDrugPositFindCard(DataTable dtDrugInfo)
        {
            txtCode.DisplayField = "ChemName";
            txtCode.MemberField = "DrugID";
            txtCode.CardColumn = "DrugID|编码|55,ChemName|化学名称|180,Spec|规格|120,ProductName|生产厂家|120,PackUnit|单位|40";
            txtCode.QueryFieldsString = "DrugID,ChemName,TradeName,PYCode,WBCode,TPYCode,TWBCode";
            txtCode.ShowCardWidth = 349;
            txtCode.ShowCardDataSource = dtDrugInfo;
        }

        /// <summary>
        /// 创建新单据更新界面
        /// </summary>
        public void NewBillClear()
        {
            InvokeController("LoadBillDetails", frmName);
            if (frmName == "FrmCheckDetailDW")
            {
                InvokeController("InitBillHead", "FrmCheckDS", DGEnum.BillEditStatus.ADD_STATUS, 0);
            }
            else
            {
                InvokeController("InitBillHead", "FrmCheckDW", DGEnum.BillEditStatus.ADD_STATUS, 0);
            }

            txtBillNo.Focus();//定位到定位查询文本框
        }

        /// <summary>
        /// 显示盘点单总金额
        /// </summary>
        /// <param name="actSum">账存金额</param>
        /// <param name="factSum">盘存金额</param>
        public void ShowTotalFee(decimal actSum, decimal factSum)
        {
            decimal diffSum = Math.Round(factSum - actSum, 2);
            lblActSum.Text = Math.Round(actSum, 2).ToString() + " 元";
            lblFactSum.Text = Math.Round(factSum, 2).ToString() + " 元";
            lblDiffSum.Text = Math.Round(diffSum, 2).ToString() + " 元";
        }

        /// <summary>
        /// 获取删除的单据明细ID
        /// </summary>
        /// <returns>已删除的单据明细ID</returns>
        public List<int> GetDeleteDetails()
        {
            return lstDeleteDetails;
        }

        /// <summary>
        /// 根据单据状态更新控件状态
        /// </summary>
        /// <param name="billStatus">单据编辑状态</param>
        public void InitControStatus(BillEditStatus billStatus)
        {
            if (billStatus == BillEditStatus.ADD_STATUS)
            {
                btnNewBill.Visible = true;
            }
            else
            {
                btnNewBill.Visible = false;
            }
        }

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        public void CloseCurrentWindow()
        {
            InvokeController("Close", this);
            if (frmName == "FrmCheckDetailDW")
            {
                InvokeController("Show", "FrmCheckDW");
            }
            else
            {
                InvokeController("Show", "FrmCheckDS");
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmCheckDetail_OpenWindowBefore(object sender, EventArgs e)
        {
            lblActSum.Text = string.Empty;
            lblFactSum.Text = string.Empty;
            lblDiffSum.Text = string.Empty;
            GridStatus = DetailsEditiStatus.UPDATING;
            InvokeController("LoadBillDetails", frmName);

            //绑定盘点药品信息
            InvokeController("GetCheckDrugInfo", frmName);
            txtBillNo.Focus();
            if (txtBillNo.Text.Trim().Length > 0)
            {
                chkInput.Checked = true;
                chkInput_CheckedChanged(null, null);
                InvokeController("ComputeTotalFee", frmName);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnRefresth_Click(object sender, EventArgs e)
        {
            InvokeController("GetCheckDrugInfo", frmName);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void gridBoxCard1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnNewBill_Click(object sender, EventArgs e)
        {
            NewBillClear();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
            if (frmName == "FrmCheckDetailDW")
            {
                InvokeController("Show", "FrmCheckDW");
            }
            else
            {
                InvokeController("Show", "FrmCheckDS");
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnNewDetail_Click(object sender, EventArgs e)
        {
            //设置新增单据录入状态
            chkInput.Checked = false;
            chkInput_CheckedChanged(null, null);
            dgDetails.AddRow();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnSaveBill_Click(object sender, EventArgs e)
        {
            try
            {
                dgDetails.EndEdit();
                this.Cursor = Cursors.WaitCursor;
                DataTable dtDetails = (DataTable)dgDetails.DataSource;
                if (dtDetails.Rows.Count == 0)
                {
                    MessageBoxEx.Show("您没有输入盘点明细记录不能保存，请输入盘点明细！");
                    return;
                }

                if (dtDetails.Rows[dtDetails.Rows.Count - 1]["DrugID"] == DBNull.Value)
                {
                    dtDetails.Rows.RemoveAt(dtDetails.Rows.Count - 1);
                }

                CalcFactBasicAmount();

                //单据内容检查
                if (CheckBill())
                {
                    //单据保存
                    InvokeController("SaveBill", frmName);
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception error)
            {
                MessageBoxEx.Show(error.Message);
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="selectedValue">选择项值</param>
        /// <param name="stop">结果</param>
        /// <param name="customNextColumnIndex">列索引</param>
        private void dgDetails_SelectCardRowSelected(object selectedValue, ref bool stop, ref int customNextColumnIndex)
        {
            DataRow selectRow = (DataRow)selectedValue;
            int colId = dgDetails.CurrentCell.ColumnIndex;
            int rowId = dgDetails.CurrentCell.RowIndex;
            DataTable dtSource = (DataTable)dgDetails.DataSource;
            if (customNextColumnIndex == 0)
            {
                decimal retailPrice = 0;
                decimal stockPrice = 0;
                decimal amount = 0;
                amount = selectRow["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(selectRow["Amount"]);
                retailPrice = selectRow["RetailPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(selectRow["RetailPrice"]);
                stockPrice = selectRow["StockPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(selectRow["StockPrice"]);
                dtSource.Rows[rowId]["StorageID"] = selectRow["StorageID"];
                dtSource.Rows[rowId]["Place"] = selectRow["Place"];
                dtSource.Rows[rowId]["DeptID"] = selectRow["DeptID"];
                dtSource.Rows[rowId]["CTypeID"] = selectRow["CTypeID"];
                dtSource.Rows[rowId]["DrugID"] = selectRow["DrugID"];
                dtSource.Rows[rowId]["ActAmount"] = selectRow["Amount"];
                dtSource.Rows[rowId]["ActStockFee"] = selectRow["ActStockFee"];
                dtSource.Rows[rowId]["ActRetailFee"] = selectRow["ActRetailFee"];
                dtSource.Rows[rowId]["UnitID"] = selectRow["MiniUnitID"];
                dtSource.Rows[rowId]["UnitName"] = selectRow["MiniUnit"];
                dtSource.Rows[rowId]["UnitAmount"] = selectRow["UnitAmount"];
                dtSource.Rows[rowId]["AuditFlag"] = 0;
                dtSource.Rows[rowId]["BillTime"] = DateTime.Now;
                dtSource.Rows[rowId]["RetailPrice"] = retailPrice;
                dtSource.Rows[rowId]["StockPrice"] = stockPrice;
                dtSource.Rows[rowId]["PackUnit"] = selectRow["PackUnit"];
                dtSource.Rows[rowId]["ChemName"] = selectRow["ChemName"];
                dtSource.Rows[rowId]["Spec"] = selectRow["Spec"];
                dtSource.Rows[rowId]["ProductName"] = selectRow["ProductName"];
                dtSource.Rows[rowId]["DosageName"] = selectRow["DosageName"];
                dtSource.Rows[rowId]["TypeName"] = selectRow["TypeName"];
                dtSource.Rows[rowId]["ActPackNum"] = selectRow["PackNum"];
                dtSource.Rows[rowId]["ActBaseNum"] = selectRow["BaseNum"];
                dtSource.Rows[rowId]["FactPackNum"] = 0;
                dtSource.Rows[rowId]["FactBaseNum"] = 0;
                InvokeController("ComputeTotalFee", frmName);
            }

            dgDetails.Refresh();

            if (chkInput.Checked)
            {
                if (dgDetails.Rows.Count - 1 == dgDetails.CurrentCell.RowIndex)
                {
                    stop = true;
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnDelDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgDetails.CurrentCell != null)
                {
                    if (MessageBox.Show("您确认要删除所选药品吗?", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        int rowid = this.dgDetails.CurrentCell.RowIndex;
                        DataTable dt = (DataTable)dgDetails.DataSource;
                        if (dt.Rows[rowid]["CheckDetailID"] == DBNull.Value
                            || dt.Rows[rowid]["CheckDetailID"].ToString() == "0")
                        {
                            dt.Rows.RemoveAt(rowid);
                        }
                        else
                        {
                            lstDeleteDetails.Add(Convert.ToInt32(dt.Rows[rowid]["CheckDetailID"]));
                            dt.Rows.RemoveAt(rowid);
                        }

                        InvokeController("ComputeTotalFee", frmName);
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
        private void btnRefreshDetail_Click(object sender, EventArgs e)
        {
            lstDeleteDetails.Clear();
            InvokeController("LoadBillDetails", frmName);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            //如果是数量
            if (e.ColumnIndex == 11 || e.ColumnIndex == 12)
            {
                DataRow currentRow = ((DataTable)dgDetails.DataSource).Rows[e.RowIndex];
                decimal factPackNum = currentRow["FactPackNum"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["FactPackNum"]);
                decimal factBaseNum = currentRow["FactBaseNum"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["FactBaseNum"]);
                decimal unitAmount = Convert.ToDecimal(currentRow["UnitAmount"]);
                decimal stockPrice = currentRow["StockPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["StockPrice"]);
                decimal retailPrice = currentRow["RetailPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["RetailPrice"]);
                currentRow["FactStockFee"] = (stockPrice / unitAmount) * (factPackNum * unitAmount + factBaseNum);
                currentRow["FactRetailFee"] = (retailPrice / unitAmount) * (factPackNum * unitAmount + factBaseNum);
                currentRow["FactPackNum"] = factPackNum;
                currentRow["FactBaseNum"] = factBaseNum;
                InvokeController("ComputeTotalFee", frmName);
                dgDetails.Refresh();
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="colIndex">列索引</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="jumpStop">结果</param>
        private void dgDetails_DataGridViewCellPressEnterKey(object sender, int colIndex, int rowIndex, ref bool jumpStop)
        {
            if (chkInput.Checked && colIndex == FactBaseNum.Index)
            {
                if (dgDetails.Rows.Count - 1 == dgDetails.CurrentCell.RowIndex)
                {
                    jumpStop = true;
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmCheckDetail_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (btnNewDetail.Focused)
                    {
                        btnNewDetail_Click(null, null);
                    }

                    break;
                case Keys.F2:
                    btnSaveBill_Click(null, null);
                    break;
                case Keys.F3:
                    btnNewBill_Click(null, null);
                    break;
                case Keys.F4:
                    btnDelDetail_Click(null, null);
                    break;
                case Keys.F5:
                    btnRefreshDetail_Click(null, null);
                    break;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnGetData_Click(object sender, EventArgs e)
        {
            DataTable dtSource = (DataTable)dgDetails.DataSource;
            if (dtSource.Rows.Count > 0)
            {
                MessageBoxEx.Show("系统会将您当前网格中的数据清掉");
                if (MessageBoxEx.Show("网格中有数据，点确定清掉数据，点取消停止操作。", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }

                dtSource.Clear();//清除掉网格存在的数据
            }

            InvokeController("OpenDrugTypeDialog", frmName);
            chkInput_CheckedChanged(null, null);
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
                DataTable dtDetails = (DataTable)dgDetails.DataSource;
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
                        dgDetails.CurrentCell = dgDetails["FactPackNum", index];
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void BtnSetFactAmount_Click(object sender, EventArgs e)
        {
            DataTable dtSource = (DataTable)dgDetails.DataSource;
            if (dtSource.Rows.Count > 0)
            {
                foreach (DataRow row in dtSource.Rows)
                {
                    row["FactAmount"] = row["ActAmount"];
                    row["FactStockFee"] = row["ActStockFee"];
                    row["FactRetailFee"] = row["ActRetailFee"];
                    row["FactPackNum"] = row["ActPackNum"];
                    row["FactBaseNum"] = row["ActBaseNum"];
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnPrintTable_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtReport = (DataTable)dgDetails.DataSource;
                InvokeController("Print", dtReport, frmName);
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
        private void chkInput_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void chkInput_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            if (chkInput.Checked)
            {
                dgDetails.Columns["DrugID"].ReadOnly = true;
                dgDetails.Columns["ChemName"].ReadOnly = true;
                dgDetails.Columns["Spec"].ReadOnly = true;
                dgDetails.Columns["ProductName"].ReadOnly = true;
                dgDetails.Columns["Place"].ReadOnly = true;
                dgDetails.Columns["RetailPrice"].ReadOnly = true;
                dgDetails.Columns["StockPrice"].ReadOnly = true;
                dgDetails.Columns["ActPackNum"].ReadOnly = true;
                dgDetails.Columns["PackUnit"].ReadOnly = true;
                dgDetails.Columns["ActBaseNum"].ReadOnly = true;
                dgDetails.Columns["UnitName"].ReadOnly = true;
                dgDetails.Columns["FactPackNum"].ReadOnly = false;
                dgDetails.Columns["FactBaseNum"].ReadOnly = false;
            }
            else
            {
                dgDetails.Columns["DrugID"].ReadOnly = false;
                dgDetails.Columns["ChemName"].ReadOnly = true;
                dgDetails.Columns["Spec"].ReadOnly = true;
                dgDetails.Columns["ProductName"].ReadOnly = true;
                dgDetails.Columns["Place"].ReadOnly = true;
                dgDetails.Columns["RetailPrice"].ReadOnly = true;
                dgDetails.Columns["StockPrice"].ReadOnly = true;
                dgDetails.Columns["ActPackNum"].ReadOnly = true;
                dgDetails.Columns["PackUnit"].ReadOnly = true;
                dgDetails.Columns["ActBaseNum"].ReadOnly = true;
                dgDetails.Columns["UnitName"].ReadOnly = true;
                dgDetails.Columns["FactPackNum"].ReadOnly = false;
                dgDetails.Columns["FactBaseNum"].ReadOnly = false;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void txtBillNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnNewDetail.Focus();
            }
        }
        #endregion
    }
}
