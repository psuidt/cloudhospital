using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_Entity.DrugManage;
using static HIS_Entity.DrugManage.DGEnum;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 入库单明细
    /// </summary>
    public partial class FrmInStoreDetail : BaseFormBusiness, IFrmInstoreDetail
    {
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
        /// 中药
        /// </summary>
        decimal wmp = 0;

        /// <summary>
        /// 西药
        /// </summary>
        decimal cpmp = 0;

        /// <summary>
        /// 中成药
        /// </summary>
        decimal tcmp = 0;

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }

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
                    dgDetails.ReadOnly = false;
                    dgDetails.Columns["DrugID"].ReadOnly = false;
                    dgDetails.Columns["ChemName"].ReadOnly = true;
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
                    dgDetails.Columns["RetailPrice"].ReadOnly = false;
                    dgDetails.Columns["RetailFee"].ReadOnly = true;
                }
                else if (gridStatus == DetailsEditiStatus.LoadBuyBill)
                {
                    dgDetails.ReadOnly = false;
                    dgDetails.Columns["DrugID"].ReadOnly = true;
                    dgDetails.Columns["ChemName"].ReadOnly = true;
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
                    dgDetails.Columns["RetailPrice"].ReadOnly = false;
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
            if(cmbOpType.SelectedValue==null)
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
            dicExpress.Add("DrugID", @"^[1-9]\d*$");//正整数
            dicExpress.Add("BatchNO", @"\S");//批号
            dicExpress.Add("ValidityDate", @"(\d{4}-\d{2}|\d{1}-\d{2}|\d{1})|(\d{4}/\d{2}|\d{1}/\d{2}|\d{1})(\s+\d{2}:\d{2}:\d{2})?");//时间格式
            if (cmbOpType.SelectedValue == null)
            {
                cmbOpType.SelectedIndex = 0;
            }

            if (cmbOpType.SelectedValue.ToString() == DGConstant.OP_DW_BACKSTORE
                            || cmbOpType.SelectedValue.ToString() == DGConstant.OP_DS_RETURNSOTRE)
            {
                dicExpress.Add("pAmount", @"^-[1-9]\d*$");//非0负整数
            }
            else if (cmbOpType.SelectedValue.ToString() == DGConstant.OP_DW_FIRSTIN ||
           cmbOpType.SelectedValue.ToString() == DGConstant.OP_DS_FIRSTIN)
            {
                dicExpress.Add("pAmount", @"^([1-9][0-9]*|-[1-9][0-9]*)$");//非0正负整数
                if (frmName == "FrmInStoreDetailDS")
                {
                    dicExpress.Add("uAmount", @"^-?\d*$");//正整数
                }
            }
            else
            {
                dicExpress.Add("pAmount", @"^[1-9][0-9]*$");//非0正整数
            }

            //期初入库 允许你正负数
            dicExpress.Add("StockPrice", @"^[1-9]\d*.\d*|0.\d*[1-9]\d*$|^[1-9]\d*$");//正浮点数
            dicExpress.Add("RetailPrice", @"^[1-9]\d*.\d*|0.\d*[1-9]\d*$|^[1-9]\d*$");//正浮点数
            SetExpress(dicExpress, dtDetails);
            string errMsg = string.Empty;
            string errCol = string.Empty;
            int errRow = -1;
            bool rtn = CheckDetails(dtDetails, null, new string[2] { "DrugID", "BatchNO" }, out errMsg, out errCol, out errRow);
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
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t1">泛型1</param>
        /// <param name="t2">泛型2</param>
        /// <returns>是否大于</returns>
        public bool CampareLarge<T>(T t1, T t2) where T : IComparable
        {
            return t1.CompareTo(t2) > 0 ? true : false;
        }

        /// <summary>
        /// 设置到效日期的最小值
        /// </summary>
        private void SetValidTime()
        {
            if (cmbOpType.SelectedValue != null)
            {
                if (!(cmbOpType.SelectedValue.ToString() == DGConstant.OP_DW_BACKSTORE ||
                          cmbOpType.SelectedValue.ToString() == DGConstant.OP_DS_RETURNSOTRE))
                {
                    ((DataGridViewDateTimeInputColumn)dgDetails.Columns["ValidityDate"]).MinDate =
                        Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
                }
                else
                {
                    ((DataGridViewDateTimeInputColumn)dgDetails.Columns["ValidityDate"]).MinDate =
                       DateTime.MinValue;
                }
            }
        }
        #endregion

        #region 接口实现
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
        ///绑定药库表头实体
        /// </summary>
        /// <param name="inHead">表头实体</param>
        public void BindInHeadInfo<THead>(THead inHead)
        {
            if (inHead != null)
            {
                if (inHead as DW_InStoreHead != null)
                {
                    frmFormInHead.Load(inHead);
                }
                else if (inHead as DS_InstoreHead != null)
                {
                    frmFormInHead.Load(inHead);
                }
            }
        }

        /// <summary>
        /// 从界面获取药库入库表头信息
        /// </summary>
        /// <returns>药库入库表头信息</returns>
        public DW_InStoreHead GetInHeadInfoDW()
        {
            DW_InStoreHead inHead = new DW_InStoreHead();
            frmFormInHead.GetValue<DW_InStoreHead>(inHead);
            inHead.SupplierName = txtSupport.Text.Trim();
            return inHead;
        }

        /// <summary>
        /// 从界面获取药房入库表头信息
        /// </summary>
        /// <returns>药房入库表头信息</returns>
        public DS_InstoreHead GetInHeadInfoDS()
        {
            DS_InstoreHead inHead = new DS_InstoreHead();
            frmFormInHead.GetValue<DS_InstoreHead>(inHead);
            inHead.SupplierName = txtSupport.Text.Trim();
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
                    //dgDetails.DataSource = null;
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
            dgDetails.SelectionCards[0].CardColumn = "DrugID|编码|80,NationalCode|国药准字号|160,ChemName|化学名称|auto,Spec|规格|100,ProductName|生产厂家|160,PackUnit|单位|40";
            dgDetails.SelectionCards[0].CardSize = new System.Drawing.Size(850, 276);
            dgDetails.SelectionCards[0].QueryFieldsString = "ChemName,TradeName,PYCode,WBCode,TPYCode,TWBCode";
            dgDetails.SelectionCards[0].SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByFirstChar;
            dgDetails.BindSelectionCardDataSource(0, dtDrugInfo);
        }

        /// <summary>
        /// 绑定药品批次信息
        /// </summary>
        /// <param name="dtBatchInfo">批次信息</param>
        public void BindDrugBatchCard(DataTable dtBatchInfo)
        {
            dgDetails.SelectionCards[1].BindColumnIndex = 4;
            dgDetails.SelectionCards[1].CardColumn = "BatchNO|批号|100,ValidityTime|到效日期|auto,StockPrice|进货价格|80,RetailPrice|零售价格|80";
            dgDetails.SelectionCards[1].CardSize = new System.Drawing.Size(420, 276);
            dgDetails.SelectionCards[1].QueryFieldsString = "BatchNO"; //"ChemName,PYCode,WBCode";
            dgDetails.BindSelectionCardDataSource(1, dtBatchInfo);
        }

        /// <summary>
        /// 创建新单据更新界面
        /// </summary>
        public void NewBillClear()
        {
            InvokeController("LoadBillDetails", frmName);
            if (frmName == "FrmInStoreDetailDW")
            {
                InvokeController("InitBillHead", "FrmInStoreDS", DGEnum.BillEditStatus.ADD_STATUS, 0);
            }
            else
            {
                InvokeController("InitBillHead", "FrmInStoreDW", DGEnum.BillEditStatus.ADD_STATUS, 0);
            }

            cmbOpType.Focus();
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
                cmbOpType.Enabled = true;
            }
            else
            {
                btnNewBill.Visible = false;
                cmbOpType.Enabled = false;
            }
        }

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        public void CloseCurrentWindow()
        {
            InvokeController("Close", this);
            if (frmName == "FrmInStoreDetailDW")
            {
                InvokeController("Show", "FrmInStoreDW");
            }
            else
            {
                InvokeController("Show", "FrmInStoreDS");
            }
        }

        /// <summary>
        /// 转换采购单到入库单
        /// </summary>
        /// <param name="dtBuyPlanDetail">采购单明细</param>
        public void ConvertBuyToInStore(DataTable dtBuyPlanDetail)
        {
            DataTable dtSource = (DataTable)dgDetails.DataSource;
            if (dtSource.Rows.Count > 0)
            {
                if (dtSource.Rows[dtSource.Rows.Count - 1]["DrugID"] == DBNull.Value)
                {
                    dtSource.Rows.RemoveAt(dtSource.Rows.Count - 1);
                }
            }

            if (dtBuyPlanDetail != null && dtBuyPlanDetail.Rows.Count >= 0)
            {
                foreach (DataRow dr in dtBuyPlanDetail.Rows)
                {
                    DataRow row = dtSource.NewRow();
                    row["DrugID"] = dr["DrugID"];
                    row["CTypeID"] = dr["CTypeID"];
                    row["ChemName"] = dr["ChemName"];
                    row["Spec"] = dr["Spec"];
                    row["ProductName"] = dr["ProductName"];
                    row["UnitID"] = dr["UnitID"];
                    row["UnitName"] = dr["UnitName"];
                    row["PackUnitName"] = dr["UnitName"];
                    row["BatchNO"] = string.Empty;
                    row["ValidityDate"] = DBNull.Value;
                    row["StockPrice"] = dr["StockPrice"];
                    row["StockFee"] = 0.00;
                    row["RetailPrice"] = dr["RetailPrice"];
                    row["RetailFee"] = 0.00;
                    row["Amount"] = 0;
                    row["pAmount"] = dr["Amount"];
                    InvokeController("ComputeTotalFee", frmName);
                    InvokeController("ChangeBatchDrug", Convert.ToInt32(dr["DrugID"]), frmName);
                    DataRow[] rowArr = dtSource.Select("DrugID=" + dr["DrugID"].ToString());
                    if (rowArr.Length == 0)
                    {
                        dtSource.Rows.Add(row);
                    }
                }
            }

            //定位
            if (dtSource.Rows.Count > 0)
            {
                this.dgDetails.ClearSelection();
                dgDetails.Rows[0].Cells[3].Selected = true;
                dgDetails.CurrentCell = dgDetails[3, 0];
                this.dgDetails.BeginEdit(true);
                dgDetails.Focus();
            }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmInStoreDetail()
        {
            InitializeComponent();
            frmFormInHead.AddItem(cmbOpType, "BusiType");
            frmFormInHead.AddItem(dtpBillDate, "BillTime");
            frmFormInHead.AddItem(dtpInvoiceDate, "InvoiceDate");
            frmFormInHead.AddItem(txtInvoiceNO, "InvoiceNo");
            frmFormInHead.AddItem(txtDeliveryNO, "DeliveryNo");
            frmFormInHead.AddItem(txtSupport, "SupplierID");
            frmFormInHead.AddItem(txtInvoiceNO, "InvoiceNO");
            frmFormInHead.AddItem(txtDeliveryNO, "DeliveryNO");
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmInStoreDetail_OpenWindowBefore(object sender, EventArgs e)
        {
            GridStatus = DetailsEditiStatus.UPDATING;
            InvokeController("LoadBillDetails", frmName);

            //绑定供应商列表
            InvokeController("GetSupplyForShowCard", frmName);
            cmbOpType.SelectedIndexChanged -= cmbOpType_SelectedIndexChanged;

            //绑定业务类型
            InvokeController("BuildOpType", frmName, frmName == "FrmInStoreDetailDW" ? DGConstant.OP_DW_SYSTEM : DGConstant.OP_DS_SYSTEM);
            cmbOpType.SelectedIndexChanged += cmbOpType_SelectedIndexChanged;

            //绑定入库药品信息
            InvokeController("GetInStoreDrugInfo", frmName);

            //获取药品批次信息
            //InvokeController("GetDrugBatchInfo", frmName);
            InvokeController("GetDeptParameters", frmName);

            //SetValidTime();
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

                btnLoadBuyBill.Visible = true;
            }
            else
            {
                btnLoadBuyBill.Visible = false;
            }

            ShowTotalFee(0, 0);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnRefresth_Click(object sender, EventArgs e)
        {
            InvokeController("GetInStoreDrugInfo", frmName);
            //InvokeController("GetDrugBatchInfo", frmName);
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
        private void btnSaveBill_Click(object sender, EventArgs e)
        {
            DataTable dtDetails = (DataTable)dgDetails.DataSource;
            if (dtDetails.Rows.Count == 0)
            {
                return;
            }

            if (dtDetails.Rows[dtDetails.Rows.Count - 1]["DrugID"] == DBNull.Value)
            {
                dtDetails.Rows.RemoveAt(dtDetails.Rows.Count - 1);
            }

            dgDetails.EndEdit();
            //单据内容检查
            if (CheckBill())
            {
                //单据保存
                InvokeController("SaveBill", frmName);
                //InvokeController("GetDrugBatchInfo", frmName);
                GridStatus = DetailsEditiStatus.UPDATING;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="selectedValue">选中项</param>
        private void txtSupport_AfterSelectedRow(object sender, object selectedValue)
        {
            if (selectedValue != null)
            {
                btnNewDetail.Focus();
            }
            else
            {
                txtSupport.MemberValue = null;
                txtSupport.Name = string.Empty;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnNewDetail_Click(object sender, EventArgs e)
        {
            GridStatus = DetailsEditiStatus.UPDATING;
            InvokeController("RefushBatchDrug", frmName);
            dgDetails.AddRow();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="selectedValue">选中项</param>
        /// <param name="stop">返回结果</param>
        /// <param name="customNextColumnIndex">列索引</param>
        private void dgDetails_SelectCardRowSelected(object selectedValue, ref bool stop, ref int customNextColumnIndex)
        {
            DataRow selectRow = (DataRow)selectedValue;
            int colId = dgDetails.CurrentCell.ColumnIndex;
            int rowId = dgDetails.CurrentCell.RowIndex;
            DataTable dtSource = (DataTable)dgDetails.DataSource;

            if (frmName == "FrmInStoreDetailDW")
            {
                if (customNextColumnIndex == 0)
                {
                    dtSource.Rows[rowId]["DrugID"] = selectRow["DrugID"];
                    dtSource.Rows[rowId]["CTypeID"] = selectRow["CTypeID"];
                    dtSource.Rows[rowId]["ChemName"] = selectRow["ChemName"];
                    dtSource.Rows[rowId]["Spec"] = selectRow["Spec"];
                    dtSource.Rows[rowId]["ProductName"] = selectRow["ProductName"];
                    dtSource.Rows[rowId]["UnitID"] = selectRow["PackUnitID"];
                    dtSource.Rows[rowId]["UnitName"] = selectRow["PackUnit"];
                    dtSource.Rows[rowId]["PackUnitName"] = selectRow["PackUnit"];
                    dtSource.Rows[rowId]["BatchNO"] = string.Empty;
                    dtSource.Rows[rowId]["ValidityDate"] = DBNull.Value;
                    dtSource.Rows[rowId]["StockPrice"] = selectRow["StockPrice"];
                    dtSource.Rows[rowId]["StockFee"] = 0.00;
                    dtSource.Rows[rowId]["RetailPrice"] = selectRow["RetailPrice"];
                    dtSource.Rows[rowId]["RetailFee"] = 0.00;
                    dtSource.Rows[rowId]["Amount"] = 0;
                    InvokeController("ComputeTotalFee", frmName);
                    InvokeController("ChangeBatchDrug", Convert.ToInt32(selectRow["DrugID"]), frmName);
                }
                else if (customNextColumnIndex == 4)
                {
                    dtSource.Rows[rowId]["BatchNo"] = selectRow["BatchNo"];
                    dtSource.Rows[rowId]["ValidityDate"] = selectRow["ValidityTime"];
                    dtSource.Rows[rowId]["StockPrice"] = selectRow["StockPrice"];
                    dtSource.Rows[rowId]["RetailPrice"] = selectRow["RetailPrice"];
                }
            }
            else
            {
                if (customNextColumnIndex == 0)
                {
                    dtSource.Rows[rowId]["DrugID"] = selectRow["DrugID"];
                    dtSource.Rows[rowId]["CTypeID"] = selectRow["CTypeID"];
                    dtSource.Rows[rowId]["ChemName"] = selectRow["ChemName"];
                    dtSource.Rows[rowId]["Spec"] = selectRow["Spec"];
                    dtSource.Rows[rowId]["ProductName"] = selectRow["ProductName"];
                    dtSource.Rows[rowId]["UnitID"] = selectRow["MiniUnitID"];
                    dtSource.Rows[rowId]["UnitName"] = selectRow["MiniUnit"];

                    dtSource.Rows[rowId]["packamount"] = selectRow["packamount"];//换算系数

                    dtSource.Rows[rowId]["PackUnit"] = selectRow["PackUnit"];
                    dtSource.Rows[rowId]["PackUnitName"] = selectRow["PackUnit"];

                    dtSource.Rows[rowId]["BatchNO"] = string.Empty;
                    dtSource.Rows[rowId]["ValidityDate"] = DBNull.Value;
                    dtSource.Rows[rowId]["StockPrice"] = selectRow["StockPrice"];
                    dtSource.Rows[rowId]["StockFee"] = 0.00;
                    dtSource.Rows[rowId]["RetailPrice"] = selectRow["RetailPrice"];
                    dtSource.Rows[rowId]["RetailFee"] = 0.00;
                    dtSource.Rows[rowId]["Amount"] = 0;
                    dtSource.Rows[rowId]["UnitAmount"] = selectRow["PackAmount"];
                    InvokeController("ComputeTotalFee", frmName);
                    InvokeController("ChangeBatchDrug", Convert.ToInt32(selectRow["DrugID"]), frmName);
                }
                else if (customNextColumnIndex == 4)
                {
                    dtSource.Rows[rowId]["BatchNo"] = selectRow["BatchNo"];
                    dtSource.Rows[rowId]["ValidityDate"] = selectRow["ValidityTime"];
                    dtSource.Rows[rowId]["StockPrice"] = selectRow["StockPrice"];
                    dtSource.Rows[rowId]["RetailPrice"] = selectRow["RetailPrice"];
                }
            }
            dgDetails.Refresh();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
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
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
            if (frmName == "FrmInStoreDetailDW")
            {
                InvokeController("Show", "FrmInStoreDW");
            }
            else
            {
                InvokeController("Show", "FrmInStoreDS");
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
            ShowTotalFee(0, 0);
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

            dgDetails.EndEdit();
            //如果是数量、进货价、零售价三列
            if (e.ColumnIndex == 6 || e.ColumnIndex == 8 || e.ColumnIndex == 10 || e.ColumnIndex == 12)
            {
                if (frmName == "FrmInStoreDetailDW")
                {
                    ProcessDwPrice(e);
                }
                else
                {
                    ProcesDsPrice(e);
                }
            }
        }

        /// <summary>
        /// 计算价格
        /// </summary>
        /// <param name="e">参数</param>
        private void ProcesDsPrice(DataGridViewCellEventArgs e)
        {
            DataTable dt = (DataTable)dgDetails.DataSource;
            DataRow currentRow = ((DataTable)dgDetails.DataSource).Rows[e.RowIndex];
            decimal amount = currentRow["pAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["pAmount"]);
            decimal unitAmonut = currentRow["uAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["uAmount"]);
            decimal packAmount = currentRow["packamount"] == DBNull.Value ? 1 : Convert.ToDecimal(currentRow["packamount"]);

            ////如果是药库退货，自动变负数
            //if (cmbOpType.SelectedValue.ToString() == DGConstant.OP_DS_RETURNSOTRE)
            //{
            //    if (amount > 0)
            //    {
            //        amount = -amount;
            //        currentRow["Amount"] = amount;
            //    }
            //}
            decimal stockPrice = currentRow["StockPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["StockPrice"]);
            if (e.ColumnIndex == 10)
            {
                string typeid = currentRow["CTypeID"].ToString();
                InvokeController("GetTypeName", typeid, frmName);
                switch (TypeName)
                {
                    case "西药":
                        currentRow["RetailPrice"] = (stockPrice * wmp).ToString("0.00");
                        break;
                    case "中成药":
                        currentRow["RetailPrice"] = (stockPrice * cpmp).ToString("0.00");
                        break;
                    case "中药":
                        currentRow["RetailPrice"] = (stockPrice * tcmp).ToString("0.00");
                        break;
                }
            }

            decimal retailPrice = currentRow["RetailPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["RetailPrice"]);
            currentRow["StockFee"] = (Math.Round(stockPrice, 2) * amount + Convert.ToDecimal((stockPrice / packAmount).ToString("0.00")) * unitAmonut).ToString("0.00");
            currentRow["RetailFee"] = (Math.Round(retailPrice, 2) * amount + Convert.ToDecimal((retailPrice / packAmount).ToString("0.00")) * unitAmonut).ToString("0.00");
            InvokeController("ComputeTotalFee", frmName);
            dgDetails.Refresh();
        }

        /// <summary>
        /// 计算价格
        /// </summary>
        /// <param name="e">参数</param>
        private void ProcessDwPrice(DataGridViewCellEventArgs e)
        {
            DataRow currentRow = ((DataTable)dgDetails.DataSource).Rows[e.RowIndex];
            decimal amount = currentRow["pAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["pAmount"]);

            if(cmbOpType.SelectedValue==null)
            {
                MessageBox.Show("业务类型不能为空");
                return;
            }

            //如果是药库退货，自动变负数
            if (cmbOpType.SelectedValue.ToString() == DGConstant.OP_DW_BACKSTORE)
            {
                if (amount > 0)
                {
                    amount = -amount;
                    currentRow["pAmount"] = amount;
                }
            }

            currentRow["Amount"] = amount;
            decimal stockPrice = currentRow["StockPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["StockPrice"]);
            if (e.ColumnIndex == 10)
            {
                string typeid = currentRow["CTypeID"].ToString();
                InvokeController("GetTypeName", typeid, frmName);
                switch (TypeName)
                {
                    case "西药":
                        currentRow["RetailPrice"] = (stockPrice * wmp).ToString("0.00");
                        break;
                    case "中成药":
                        currentRow["RetailPrice"] = (stockPrice * cpmp).ToString("0.00");
                        break;
                    case "中药":
                        currentRow["RetailPrice"] = (stockPrice * tcmp).ToString("0.00");
                        break;
                }
            }

            decimal retailPrice = currentRow["RetailPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["RetailPrice"]);
            currentRow["StockFee"] = (Math.Round(stockPrice, 2) * amount).ToString("0.00");
            currentRow["RetailFee"] = (Math.Round(retailPrice, 2) * amount).ToString("0.00");
            InvokeController("ComputeTotalFee", frmName);
            dgDetails.Refresh();
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
            //如果是批次选项卡
            if (colIndex == 4)
            {
                dgDetails.EndEdit();
                DataTable dtSource = (DataTable)dgDetails.DataSource;
                DataRow currentRow = dtSource.Rows[rowIndex];
                for (int index = 0; index < dtSource.Rows.Count; index++)
                {
                    if (dtSource.Rows[index]["DrugID"].ToString() == currentRow["DrugID"].ToString()
                        && dtSource.Rows[index]["BatchNO"].ToString().Trim() == currentRow["BatchNO"].ToString().Trim()
                        && rowIndex != index)
                    {
                        MessageBox.Show("错误，不能添加重复的药品信息");
                        jumpStop = true;
                    }
                }

                dgDetails.BeginEdit(true);
            }
            else if (colIndex == RetailPrice.Index)
            {
                if (GridStatus == DetailsEditiStatus.LoadBuyBill)
                {
                    if (dgDetails.Rows.Count - 1 == dgDetails.CurrentCell.RowIndex)
                    {
                        jumpStop = true;
                    }
                }
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
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmbOpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InvokeController("LoadBillDetails", frmName);
            InvokeController("RefreshHead", frmName);

            //绑定入库药品信息
            InvokeController("GetInStoreDrugInfo", frmName);
            //InvokeController("GetDrugBatchInfo", frmName); //因为空表格无法回车,所以暂时屏蔽该功能
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmInStoreDetail_KeyDown(object sender, KeyEventArgs e)
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
                case Keys.F6:
                    if (btnLoadBuyBill.Visible)
                    {
                        btnLoadBuyBill_Click(null, null);
                    }

                    break;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void txtSupport_Leave(object sender, EventArgs e)
        {
            //if (txtSupport.SelectedValue == null)
            //{
            //    txtSupport.Text = "";
            //}
            //else
            //{
            //    txtSupport.Text = ((DataRow)txtSupport.SelectedValue)[txtSupport.DisplayField].ToString();
            //}
        }

        /// <summary>
        /// 绑定科室参数
        /// </summary>
        /// <param name="dt">数据源</param>
        public void BindDeptParameters(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ParaID"].ToString() == "WMPricePercent")
                {
                    wmp = (Convert.ToDecimal(dt.Rows[i]["Value"]) / 100) + 1;
                }

                if (dt.Rows[i]["ParaID"].ToString() == "CPMPricePercent")
                {
                    cpmp = (Convert.ToDecimal(dt.Rows[i]["Value"]) / 100) + 1;
                }

                if (dt.Rows[i]["ParaID"].ToString() == "TCMPricePercent")
                {
                    tcmp = (Convert.ToDecimal(dt.Rows[i]["Value"]) / 100) + 1;
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnLoadBuyBill_Click(object sender, EventArgs e)
        {
            //设置网格回车跳转顺序
            GridStatus = DetailsEditiStatus.LoadBuyBill;
            InvokeController("OpenBuyBillDialog");
            dgDetails.Focus();
        }
        
        private void dgDetails_CurrentCellChanged(object sender, EventArgs e)
        {
            /*if (dgDetails.CurrentCell != null)
            {
                int cindex = dgDetails.CurrentCell.ColumnIndex;
                int rindex = dgDetails.CurrentRow.Index;
                if (cindex == 4 && rindex != -1)
                {
                    if (dgDetails.Rows[rindex].Cells[0].Value != DBNull.Value)
                    {
                        int DrugID = Convert.ToInt32(dgDetails.Rows[rindex].Cells[0].Value);
                        InvokeController("ChangeBatchDrug", DrugID, frmName);
                    }
                    else
                    {
                        InvokeController("RefushBatchDrug", frmName);
                    }
                }
            }*/
        }
    }
}
