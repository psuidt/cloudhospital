using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MaterialManage;
using HIS_MaterialManage.Winform.IView.StoreMgr;

namespace HIS_MaterialManage.Winform.ViewForm
{
    /// <summary>
    /// 物资入库退库
    /// </summary>
    public partial class FrmInStoreDetail : BaseFormBusiness, IFrmInstoreDetail
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmInStoreDetail()
        {
            InitializeComponent();
            frmFormInHead.AddItem(cmbOpType, "BusiType");
            frmFormInHead.AddItem(dtpBillDate, "BillTime");
            frmFormInHead.AddItem(dtpInvoiceDate, "InvoiceTime");
            frmFormInHead.AddItem(txtInvoiceNO, "InvoiceNo");
            frmFormInHead.AddItem(txtDeliveryNO, "DeliveryNo");
            frmFormInHead.AddItem(txtSupport, "SupplierID");
        }

        #region
        /// <summary>
        /// 销售价比例
        /// </summary>
        decimal wmp = 0;

        /// <summary>
        /// 当前明细网格编辑状态
        /// </summary>
        private MWEnum.DetailsEditiStatus gridStatus;

        /// <summary>
        /// 需从后台删除的单据明细
        /// </summary>
        private List<int> lstDeleteDetails = new List<int>();

        /// <summary>
        /// 设置网格状态
        /// </summary>
        public MWEnum.DetailsEditiStatus GridStatus
        {
            get
            {
                return gridStatus;
            }

            set
            {
                gridStatus = value;
                if (gridStatus == MWEnum.DetailsEditiStatus.UPDATING)
                {
                    dgDetails.ReadOnly = false;
                    dgDetails.Columns["MaterialID"].ReadOnly = false;
                    dgDetails.Columns["MatName"].ReadOnly = true;
                    dgDetails.Columns["Spec"].ReadOnly = true;
                    dgDetails.Columns["ProductName"].ReadOnly = true;
                    dgDetails.Columns["BatchNO"].ReadOnly = false;
                    dgDetails.Columns["ValidityDate"].ReadOnly = false;
                    dgDetails.Columns["UnitName"].ReadOnly = true;
                    var dataGridViewColumn = dgDetails.Columns["Amount"];
                    if (dataGridViewColumn != null)
                    {
                        dataGridViewColumn.ReadOnly = false;
                    }

                    var dataGridViewColumn1 = dgDetails.Columns["pAmount"];
                    if (dataGridViewColumn1 != null)
                    {
                        dataGridViewColumn1.ReadOnly = false;
                    }

                    var dataGridViewColumn2 = dgDetails.Columns["uAmount"];
                    if (dataGridViewColumn2 != null)
                    {
                        dataGridViewColumn2.ReadOnly = false;
                    }

                    dgDetails.Columns["StockPrice"].ReadOnly = false;
                    dgDetails.Columns["StockFee"].ReadOnly = true;
                    dgDetails.Columns["RetailPrice"].ReadOnly = true;
                    dgDetails.Columns["RetailFee"].ReadOnly = true;
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
        /// <returns>true：检查通过</returns>
        public bool CheckDetails(
            DataTable dtSource,
            List<int> checkRows,
            string[] uniqueCol,
            out string errMsg,
            out string errCol,
            out int errRow)
        {
            errMsg = string.Empty;
            errCol = string.Empty;
            errRow = -1;
            for (int index = 0; index < dtSource.Rows.Count; index++)
            {
                if (checkRows != null && checkRows.FindIndex((x) => { return x == index; }) < 0)
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
                                errMsg = "【{0}】的录入数据格式不正确，请重新录入";
                                errCol = dtSource.Columns[count].ColumnName;
                                errRow = index;
                                return false;
                            }
                        }
                    }
                }

                if (Convert.ToDateTime(dRow["ValidityDate"].ToString()) < Convert.ToDateTime(System.DateTime.Now.Date.ToLongDateString()))
                {
                    errMsg = "时间不能小于当前时间";
                    errCol = dtSource.Columns["ValidityDate"].ColumnName;
                    errRow = index;
                    return false;
                }

                if (CampareLarge(Math.Abs(Convert.ToDecimal(dRow["StockPrice"].ToString())), Math.Abs(Convert.ToDecimal(dRow["RetailPrice"].ToString()))))
                {
                    errMsg = "进货价不能大于售货价格";
                    errCol = dtSource.Columns["StockPrice"].ColumnName;
                    errRow = index;
                    return false;
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
            if (cmbOpType.SelectedValue == null)
            {
                MessageBoxEx.Show("业务类型不能为空");
                cmbOpType.Focus();
                return false;

            }

            //表头数据验证
            if (Convert.ToInt32(txtSupport.MemberValue) == 0)
            {
                MessageBoxEx.Show("供应商不能为空");
                txtSupport.Focus();
                return false;
            }

            //明细数据验证
            DataTable dtDetails = (DataTable)dgDetails.DataSource;
            if (dtDetails.Rows.Count == 0)
            {
                MessageBoxEx.Show("没有数据需要提交");
                return false;
            }

            Dictionary<string, string> dicExpress = new Dictionary<string, string>();
            dicExpress.Add("MaterialID", @"^[1-9]\d*$");//正整数
            dicExpress.Add("BatchNO", @"\S");//批号
            dicExpress.Add("ValidityDate", @"(\d{4}-\d{2}|\d{1}-\d{2}|\d{1})|(\d{4}/\d{2}|\d{1}/\d{2}|\d{1})(\s+\d{2}:\d{2}:\d{2})?");//时间格式
            if (cmbOpType.SelectedValue.ToString() == MWConstant.OP_MW_BACKSTORE)
            {
                dicExpress.Add("pAmount", @"^-[1-9]\d*$");//非0负整数
            }
            else if (cmbOpType.SelectedValue.ToString() == MWConstant.OP_MW_FIRSTIN)
            {
                dicExpress.Add("pAmount", @"^-?[1-9]\d*.\d*|0.\d*[1-9]\d*$|^[1-9]\d*$");//正负整数
                if (frmName == "FrmInStoreDetailDS")
                {
                    dicExpress.Add("uAmount", @"^-?\d*$");//正整数
                }
            }
            else
            {
                dicExpress.Add("pAmount", @"^[1-9]\d*$");//非0正整数
            }

            //期初入库 允许你正负数
            dicExpress.Add("StockPrice", @"^[1-9]\d*.\d*|0.\d*[1-9]\d*$|^[1-9]\d*$");//正浮点数
            dicExpress.Add("RetailPrice", @"^[1-9]\d*.\d*|0.\d*[1-9]\d*$|^[1-9]\d*$");//正浮点数
            SetExpress(dicExpress, dtDetails);
            string errMsg = string.Empty;
            string errCol = string.Empty;
            int errRow = -1;
            bool rtn = CheckDetails(dtDetails, null, new string[2] { "MaterialID", "BatchNO" }, out errMsg, out errCol, out errRow);
            if (!rtn)
            {
                MessageBoxEx.Show(string.Format(errMsg, dgDetails.Columns[errCol].HeaderText));
                dgDetails.CurrentCell = dgDetails[errCol, errRow];
            }

            return rtn;
        }

        /// <summary>
        /// 大小比较
        /// </summary>
        /// <typeparam name="T">比较接口</typeparam>
        /// <param name="t1">开始时间</param>
        /// <param name="t2">结束时间</param>
        /// <returns>true:开始时间大于结束时间</returns>
        public bool CampareLarge<T>(T t1, T t2) where T : IComparable
        {
            return t1.CompareTo(t2) > 0 ? true : false;
        }
        #endregion

        #region 接口实现
        /// <summary>
        /// 绑定药品批次信息
        /// </summary>
        /// <param name="dtBatchInfo">批次信息</param>
        public void BindMaterialBatchCard(DataTable dtBatchInfo)
        {
            dgDetails.SelectionCards[1].BindColumnIndex = 4;
            dgDetails.SelectionCards[1].CardColumn = "BatchNO|批号|100,ValidityTime|到效日期|auto";
            dgDetails.SelectionCards[1].CardSize = new System.Drawing.Size(250, 276);
            dgDetails.SelectionCards[1].QueryFieldsString = "ChemName,PYCode,WBCode";
            dgDetails.BindSelectionCardDataSource(4, dtBatchInfo);
        }

        /// <summary>
        /// 绑定科室参数
        /// </summary>
        /// <param name="dt">数据源</param>
        public void BindDeptParameters(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ParaID"].ToString() == "MWPricePercent")
                {
                    wmp = (Convert.ToDecimal(dt.Rows[i]["Value"]) / 100) + 1;
                }
            }
        }

        /// <summary>
        /// 绑定入库单录入ShowCard
        /// </summary>
        /// <param name="dtDrugInfo">药品信息</param>
        public void BindMaterialInfoCard(DataTable dtDrugInfo)
        {
            dgDetails.SelectionCards[0].BindColumnIndex = 0;
            dgDetails.SelectionCards[0].CardColumn = "MaterialID|编码|55,MatName|名称|auto,Spec|规格|120,ProductName|生产厂家|120";
            dgDetails.SelectionCards[0].CardSize = new System.Drawing.Size(500, 276);
            dgDetails.SelectionCards[0].QueryFieldsString = "MatName,PYCode,WBCode,TPYCode,TWBCode";
            dgDetails.SelectionCards[0].SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByFirstChar;
            dgDetails.BindSelectionCardDataSource(0, dtDrugInfo);
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
        ///绑定药库表头实体
        /// </summary>
        /// <param name="inHead">表头实体</param>
        public void BindInHeadInfo<THead>(THead inHead)
        {
            if (inHead != null)
            {
                if (inHead as MW_InStoreHead != null)
                {
                    frmFormInHead.Load(inHead);
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
        /// 关闭当前窗口
        /// </summary>
        public void CloseCurrentWindow()
        {
            InvokeController("Close", this);
            InvokeController("Show", "FrmInStore");
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
        /// 获取入库单
        /// </summary>
        /// <returns>入库单</returns>
        public MW_InStoreHead GetInHeadInfo()
        {
            MW_InStoreHead inHead = new MW_InStoreHead();
            frmFormInHead.GetValue<MW_InStoreHead>(inHead);
            inHead.SupplierName = txtSupport.Text.Trim();
            return inHead;
        }

        /// <summary>
        /// 根据单据状态更新控件状态
        /// </summary>
        /// <param name="billStatus">单据编辑状态</param>
        public void InitControStatus(MWEnum.BillEditStatus billStatus)
        {
            if (billStatus == MWEnum.BillEditStatus.ADD_STATUS)
            {
                btnNewBill.Visible = true;
                cmbOpType.Enabled = true;
            }
            else
            {
                btnNewBill.Visible = false;
                cmbOpType.Enabled = false;
            }
        }

        /// <summary>
        /// 创建新单据更新界面
        /// </summary>
        public void NewBillClear()
        {
            InvokeController("LoadBillDetails", frmName);
            InvokeController("InitBillHead", "FrmInStore", MWEnum.BillEditStatus.ADD_STATUS, 0);
            cmbOpType.Focus();
        }

        /// <summary>
        /// 显示入库单总金额
        /// </summary>
        /// <param name="stockFee">进货金额</param>
        /// <param name="retailFee">零售金额</param>
        public void ShowTotalFee(decimal stockFee, decimal retailFee)
        {
            string strFee = "合计零售金额：{0}  合计进货金额：{1}";
            strFee = string.Format(strFee, retailFee.ToString("0.00"), stockFee.ToString("0.00"));
            pnlStatus.Text = strFee;
        }
        #endregion

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmInStoreDetail_OpenWindowBefore(object sender, EventArgs e)
        {
            GridStatus = MWEnum.DetailsEditiStatus.UPDATING;
            InvokeController("LoadBillDetails", frmName);
            //绑定供应商列表
            InvokeController("GetSupplyForShowCard", frmName);
            cmbOpType.SelectedIndexChanged -= cmbOpType_SelectedIndexChanged;
            //绑定业务类型
            InvokeController("BuildOpType", frmName);
            cmbOpType.SelectedIndexChanged += cmbOpType_SelectedIndexChanged;
            //绑定入库药品信息
            InvokeController("GetInStoreDrugInfo", frmName);
            //获取药品批次信息
            InvokeController("GetDrugBatchInfo", frmName);
            InvokeController("GetDeptParameters");
            dtpBillDate.Focus();

            if (frmName == "FrmInStoreDetailDW")
            {
                var dataGridViewColumn = dgDetails.Columns["uAmount"];
                if (dataGridViewColumn != null)
                {
                    dataGridViewColumn.Visible = false;
                }

                var gridViewColumn = dgDetails.Columns["UnitName"];
                if (gridViewColumn != null)
                {
                    gridViewColumn.Visible = false;
                }
            }
        }

        #region 事件
        /// <summary>
        /// 切换业务类型
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void cmbOpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InvokeController("LoadBillDetails", frmName);
            InvokeController("RefreshHead", frmName);
            //绑定入库药品信息
            InvokeController("GetInStoreDrugInfo", frmName);
        }
        #endregion

        /// <summary>
        /// 新增明细
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnNewDetail_Click(object sender, EventArgs e)
        {
            dgDetails.AddRow();
        }

        /// <summary>
        /// 删除明细
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnDelDetail_Click(object sender, EventArgs e)
        {
            if (dgDetails.CurrentCell != null)
            {
                int rowid = this.dgDetails.CurrentCell.RowIndex;
                DataTable dt = (DataTable)dgDetails.DataSource;
                if (dt.Rows[rowid]["InDetailID"] == DBNull.Value
                    || dt.Rows[rowid]["InDetailID"].ToString() == "0")
                {
                    dt.Rows.RemoveAt(rowid);
                }
                else
                {
                    lstDeleteDetails.Add(Convert.ToInt32(dt.Rows[rowid]["InDetailID"]));
                    dt.Rows.RemoveAt(rowid);
                }

                InvokeController("ComputeTotalFee", frmName);
            }
        }

        /// <summary>
        /// 刷新明细数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnRefreshDetail_Click(object sender, EventArgs e)
        {
            lstDeleteDetails.Clear();
            InvokeController("LoadBillDetails", frmName);
        }

        /// <summary>
        /// 明细网格弹出数据选中
        /// </summary>
        /// <param name="selectedValue">选中的数据</param>
        /// <param name="stop">Stop标志</param>
        /// <param name="customNextColumnIndex">下一个获得焦点的列</param>
        private void dgDetails_SelectCardRowSelected(object selectedValue, ref bool stop, ref int customNextColumnIndex)
        {
            DataRow selectRow = (DataRow)selectedValue;
            int colId = dgDetails.CurrentCell.ColumnIndex;
            int rowId = dgDetails.CurrentCell.RowIndex;
            DataTable dtSource = (DataTable)dgDetails.DataSource;
            if (customNextColumnIndex == 0)
            {
                dtSource.Rows[rowId]["MaterialID"] = selectRow["MaterialID"];
                dtSource.Rows[rowId]["MatName"] = selectRow["MatName"];
                dtSource.Rows[rowId]["Spec"] = selectRow["Spec"];
                dtSource.Rows[rowId]["ProductName"] = selectRow["ProductName"];
                dtSource.Rows[rowId]["UnitName"] = selectRow["UnitName"];
                dtSource.Rows[rowId]["UnitId"] = selectRow["UnitId"];
                dtSource.Rows[rowId]["BatchNO"] = string.Empty;
                dtSource.Rows[rowId]["ValidityDate"] = DBNull.Value;
                dtSource.Rows[rowId]["StockPrice"] = selectRow["StockPrice"];
                dtSource.Rows[rowId]["StockFee"] = 0.00;
                dtSource.Rows[rowId]["RetailPrice"] = selectRow["RetailPrice"];
                dtSource.Rows[rowId]["RetailFee"] = 0.00;
                dtSource.Rows[rowId]["Amount"] = 0;
                InvokeController("ComputeTotalFee", frmName);
                InvokeController("ChangeBatchDrug", Convert.ToInt32(selectRow["MaterialID"]), frmName);
            }

            dgDetails.Refresh();
        }

        /// <summary>
        /// 编辑明细数据
        /// </summary>
        /// <param name="sender">网格控件</param>
        /// <param name="colIndex">列</param>
        /// <param name="rowIndex">行</param>
        /// <param name="jumpStop">停止跳转标志</param>
        private void dgDetails_DataGridViewCellPressEnterKey(object sender, int colIndex, int rowIndex, ref bool jumpStop)
        {
            //如果是批次选项卡
            if (colIndex == 4)
            {
                dgDetails.EndEdit();
                DataTable dtSource = (DataTable)dgDetails.DataSource;
                DataRow currentRow = dtSource.Rows[rowIndex];
                for (int index = 0; index < dtSource.Rows.Count; index++)
                {
                    if (dtSource.Rows[index]["MaterialID"].ToString() == currentRow["MaterialID"].ToString()
                        && dtSource.Rows[index]["BatchNO"].ToString().Trim() == currentRow["BatchNO"].ToString().Trim()
                        && rowIndex != index)
                    {
                        MessageBox.Show("错误，不能添加重复的药品信息");
                        jumpStop = true;
                    }
                }

                dgDetails.BeginEdit(true);
            }
            else if (colIndex == ValidityDate.Index)
            {
                //将20181010转换日期格式2018-10-10
                DateTime validityDate;
                if (DateTime.TryParseExact(dgDetails[ValidityDate.Index, rowIndex].EditedFormattedValue.ToString(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out validityDate))
                {
                    dgDetails[ValidityDate.Index, rowIndex].Value = validityDate;
                }
            }
        }

        /// <summary>
        /// 编辑明细
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void dgDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            //如果是数量、进货价、零售价三列
            if (e.ColumnIndex == 6 || e.ColumnIndex == 8 || e.ColumnIndex == 10)
            {
                DataRow currentRow = ((DataTable)dgDetails.DataSource).Rows[e.RowIndex];
                decimal amount = currentRow["pAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["pAmount"]);

                if(cmbOpType.SelectedValue == null)
                {
                    MessageBoxEx.Show("业务类型不能为空");
                    return;
                }

                //如果是药库退货，自动变负数
                if (cmbOpType.SelectedValue.ToString() == MWConstant.OP_MW_BACKSTORE)
                {
                    if (amount > 0)
                    {
                        amount = -amount;
                        currentRow["pAmount"] = amount;
                    }
                }

                currentRow["Amount"] = amount;
                decimal stockPrice = currentRow["StockPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["StockPrice"]);
                //currentRow["RetailPrice"] = (stockPrice * wmp).ToString("0.00");
                decimal retailPrice = currentRow["RetailPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["RetailPrice"]);
                currentRow["StockFee"] = stockPrice * amount;
                currentRow["RetailFee"] = retailPrice * amount;
                InvokeController("ComputeTotalFee", frmName);
                dgDetails.Refresh();
            }
        }

        /// <summary>
        /// 保存单据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnSaveBill_Click(object sender, EventArgs e)
        {
            DataTable dtDetails = (DataTable)dgDetails.DataSource;

            if (dtDetails.Rows.Count == 0)
            {
                return;
            }

            if (dtDetails.Rows[dtDetails.Rows.Count - 1]["MaterialID"] == DBNull.Value)
            {
                dtDetails.Rows.RemoveAt(dtDetails.Rows.Count - 1);
            }

            dgDetails.EndEdit();
            //单据内容检查
            if (CheckBill())
            {
                //单据保存
                InvokeController("SaveBill", frmName);
            }
        }

        /// <summary>
        /// 新增单据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnNewBill_Click(object sender, EventArgs e)
        {
            NewBillClear();
        }

        /// <summary>
        /// 刷新单据列表
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnRefresth_Click(object sender, EventArgs e)
        {
            InvokeController("GetInStoreDrugInfo", frmName);
            InvokeController("GetDrugBatchInfo", frmName);
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
            InvokeController("Show", "FrmInStore");
        }
    }
}
