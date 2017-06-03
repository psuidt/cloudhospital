using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 出库单明细
    /// </summary>
    public partial class FrmOutStoreDetail : BaseFormBusiness, IFrmOutstoreDetail
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmOutStoreDetail()
        {
            InitializeComponent();
            frmCommon.AddItem(cmbOpType, "BusiType");
            frmCommon.AddItem(timeOutData, "BillTime");
            frmCommon.AddItem(txtDept, "ToDeptID");
            frmCommon.AddItem(txtRemark, "Remark");
            frmCommon.AddItem(txtLostReason, "LostReason");
        }

        #region 私有变量
        /// <summary>
        /// 当前明细网格编辑状态
        /// </summary>
        private DGEnum.DetailsEditiStatus gridStatus;

        /// <summary>
        /// 需从后台删除的单据明细
        /// </summary>
        private List<int> lstDeleteDetails = new List<int>();
        #endregion

        #region 接口方法\属性
        /// <summary>
        ///绑定药库表头实体
        /// </summary>
        /// <param name="inHead">表头实体</param>
        public void BindInHeadInfo<THead>(THead inHead)
        {
            if (inHead != null)
            {
                if (inHead as DW_OutStoreHead != null)
                {
                    CurretDwOutStoreHead = inHead as DW_OutStoreHead;
                }
                else if (inHead as DS_OutStoreHead != null)
                {
                    CurrentDSOuttoreHead = inHead as DS_OutStoreHead;
                }
            }
        }

        /// <summary>
        /// 单前药房主表实体对象
        /// </summary>
        public DW_OutStoreHead CurretDwOutStoreHead { get; set; }

        /// <summary>
        /// 单前药库主表实体对象
        /// </summary>
        public DS_OutStoreHead CurrentDSOuttoreHead { get; set; }

        /// <summary>
        /// 申请单转出库单 设置状态
        /// </summary>
        /// <param name="row">行对象</param>
        public void SetHeadValue(DataRow row)
        {
            this.cmbOpType.SelectedValue = DGConstant.OP_DW_CIRCULATEOUT;
            this.txtDept.MemberValue = row["applydeptId"].ToString();
            var gridColumn = dgDetails.Columns["factAmount"];
            if (gridColumn != null)
            {
                gridColumn.Visible = true;
            }
        }

        /// <summary>
        /// 状态标识是否申请单转出库单 
        /// </summary>
        public bool IsApplyStatus { get; set; } = false;

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
        /// 删除列
        /// </summary>
        /// <returns>结果</returns>
        public List<int> GetDeleteDetails()
        {
            return lstDeleteDetails;
        }

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        public void CloseCurrentWindow()
        {
            InvokeController("Close", this);
            if (frmName == "FrmOutStoreDetailDW")
            {
                InvokeController("Show", "FrmOutStoreDW");
            }
            else
            {
                InvokeController("Show", "FrmOutStoreDS");
            }
        }

        /// <summary>
        /// 从界面获取药库入库表头信息  
        /// </summary>
        /// <returns>药库入库表头信息</returns>
        public DW_OutStoreHead GetInHeadInfoDW()
        {
            DW_OutStoreHead inHead = new DW_OutStoreHead();
            frmCommon.GetValue<DW_OutStoreHead>(inHead);
            DataTable dt = (DataTable)dgDetails.DataSource;
            if (IsApplyStatus == true)
            {
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["ApplyHeadID"] != null)
                {
                    inHead.ApplyHeadId = Convert.ToInt32(dt.Rows[0]["ApplyHeadID"].ToString());
                }
            }

            return inHead;
        }

        /// <summary>
        /// 药房主表信息
        /// </summary>
        /// <returns>药房主表</returns>
        public DS_OutStoreHead GetHeadInfoDS()
        {
            DS_OutStoreHead inHead = new DS_OutStoreHead();
            frmCommon.GetValue<DS_OutStoreHead>(inHead);
            return inHead;
        }

        /// <summary>
        /// 往来科室数据绑定
        /// </summary>
        /// <param name="dt">往来科室</param>
        public void BindDept(DataTable dt)
        {
            this.txtDept.DisplayField = "RelationDeptName";
            txtDept.MemberField = "RelationDeptID";
            txtDept.CardColumn = "RelationDeptID|编码|50,RelationDeptName|科室名称|auto";
            txtDept.QueryFieldsString = "RelationDeptName";
            txtDept.ShowCardWidth = 250;
            txtDept.ShowCardDataSource = dt;
        }

        /// <summary>
        ///业务类型数据绑定 
        /// </summary>
        /// <param name="dtOpType">业务类型</param>
        public void BindOpType(DataTable dtOpType)
        {
            cmbOpType.DataSource = dtOpType;
            cmbOpType.DisplayMember = "opTypeName";
            cmbOpType.ValueMember = "opType";
            cmbOpType.SelectedIndex = 0;
        }

        /// <summary>
        /// 初始化控件状态
        /// </summary>
        /// <param name="billStatus">状态值</param>
        public void InitControStatus(DGEnum.BillEditStatus billStatus)
        {
            if (billStatus == DGEnum.BillEditStatus.ADD_STATUS)
            {
                btnNewBill.Enabled = true;
                cmbOpType.Enabled = true;
                btnReadInStore.Enabled = true;
                btnApply.Enabled = true;
                txtDept.Enabled = true;
            }
            else
            {
                btnNewBill.Enabled = false;
                cmbOpType.Enabled = false;
                btnReadInStore.Enabled = false;
                btnApply.Enabled = false;
                txtDept.Enabled = false;
            }
        }

        /// <summary>
        /// 正则匹配
        /// </summary>
        public void SetGridExpress()
        {
            Dictionary<string, string> dicExpress = new Dictionary<string, string>();
            dicExpress.Add("DrugID", @"^[1-9]\d*$");//正整数
            dicExpress.Add("BatchNO", @"\S");//批号
            if (cmbOpType.SelectedValue.ToString() == DGConstant.OP_DW_RETURNSTORE
                           || cmbOpType.SelectedValue.ToString() == DGConstant.OP_DS_RETURNSOTRE)
            {
                dicExpress.Add("pAmount", @"^-[1-9]\d*$");//非0负整数
            }
            else
            {
                dicExpress.Add("pAmount", @"^[1-9]\d*$");//非0正整数
            }

            dgDetails.SetExpress(dicExpress, (DataTable)dgDetails.DataSource);
        }

        /// <summary>
        /// 创建新单据更新界面
        /// </summary>
        public void NewBillClear()
        {
            InvokeController("LoadBillDetails", frmName);
            if (frmName == "FrmOutStoreDetailDW")
            {
                InvokeController("InitBillHead", "FrmOutStoreDS", DGEnum.BillEditStatus.ADD_STATUS, 0);
            }
            else
            {
                InvokeController("InitBillHead", "FrmOutStoreDW", DGEnum.BillEditStatus.ADD_STATUS, 0);
            }

            cmbOpType.Focus();
        }

        /// <summary>
        /// 设置网格状态
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
                if (gridStatus == DGEnum.DetailsEditiStatus.UPDATING)
                {
                    dgDetails.ReadOnly = false;
                    var dataGridViewColumn = dgDetails.Columns["DrugID"];
                    if (dataGridViewColumn != null)
                    {
                        dataGridViewColumn.ReadOnly = false;
                    }

                    var gridViewColumn = dgDetails.Columns["ChemName"];
                    if (gridViewColumn != null)
                    {
                        gridViewColumn.ReadOnly = true;
                    }

                    var viewColumn = dgDetails.Columns["Spec"];
                    if (viewColumn != null)
                    {
                        viewColumn.ReadOnly = true;
                    }

                    var column = dgDetails.Columns["ProductName"];
                    if (column != null)
                    {
                        column.ReadOnly = true;
                    }

                    var o = dgDetails.Columns["BatchNO"];
                    if (o != null)
                    {
                        o.ReadOnly = true;
                    }

                    var dataGridViewColumn1 = dgDetails.Columns["UnitName"];
                    if (dataGridViewColumn1 != null)
                    {
                        dataGridViewColumn1.ReadOnly = true;
                    }

                    var gridViewColumn1 = dgDetails.Columns["PackUnitName"];
                    if (gridViewColumn1 != null)
                    {
                        gridViewColumn1.ReadOnly = true;
                    }

                    var viewColumn1 = dgDetails.Columns["Amount"];
                    if (viewColumn1 != null)
                    {
                        viewColumn1.ReadOnly = false;
                    }

                    var c1 = dgDetails.Columns["pAmount"];
                    if (c1 != null)
                    {
                        c1.ReadOnly = false;
                    }

                    var c2 = dgDetails.Columns["uAmount"];
                    if (c2 != null)
                    {
                        c2.ReadOnly = false;
                    }

                    dgDetails.Columns["StockPrice"].ReadOnly = true;
                    dgDetails.Columns["StockFee"].ReadOnly = true;
                    dgDetails.Columns["RetailPrice"].ReadOnly = true;
                    dgDetails.Columns["RetailFee"].ReadOnly = true;
                    var column1 = dgDetails.Columns["totalNum"];
                    if (column1 != null)
                    {
                        column1.ReadOnly = true;
                    }

                    var column2 = dgDetails.Columns["factAmount"];
                    if (column2 != null)
                    {
                        column2.ReadOnly = true;
                    }
                }
                else
                {
                    dgDetails.ReadOnly = true;
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
            dgDetails.SelectionCards[0].CardColumn = "DrugID|编码|55,ChemName|化学名称|auto,Spec|规格|160,ProductName|生产厂家|160,UnitName|单位|40,BatchNO|批号|100,ValidityTime|到效日期|90,BatchAmount|批次数量|60";
            dgDetails.SelectionCards[0].CardSize = new System.Drawing.Size(900, 276);
            dgDetails.SelectionCards[0].QueryFieldsString = "ChemName,PYCode,WBCode";
            dgDetails.SelectionCards[0].SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByFirstChar;
            dgDetails.BindSelectionCardDataSource(0, dtDrugInfo);
        }
        #endregion

        /// <summary>
        /// 检查网格
        /// </summary>
        /// <param name="dtSource">数据源</param>
        /// <param name="checkRows">检查行(为空表全部)</param>
        /// <param name="uniqueCol">唯一列名(为空则没有主键)</param>
        /// <param name="errMsg">错误信息</param>
        /// <param name="errCol">错误列名</param>
        /// <param name="errRow">错误行索引</param>
        /// <returns>是否合法</returns>
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
            }

            return true;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmOutStoreDetail_OpenWindowBefore(object sender, EventArgs e)
        {
            this.txtLostReason.Visible = false;
            this.lbLostReason.Visible = false;
            GridStatus = DGEnum.DetailsEditiStatus.UPDATING;
            cmbOpType.SelectedIndexChanged -= cmbOpType_SelectedIndexChanged;

            //绑定业务类型
            InvokeController("BuildOpType", frmName, frmName == "FrmOutStoreDetailDW" ? DGConstant.OP_DW_SYSTEM : DGConstant.OP_DS_SYSTEM);
            cmbOpType.SelectedIndexChanged += cmbOpType_SelectedIndexChanged;
            DGEnum.BillEditStatus editStus = (DGEnum.BillEditStatus)InvokeController("GetBillEditStatus");
            if (frmName == "FrmOutStoreDetailDW")
            {
                cmbOpType.SelectedValue = CurretDwOutStoreHead.BusiType;
                cmbOpType_SelectedIndexChanged(null, null);
                var dataGridViewColumn = dgDetails.Columns["UnitName"];
                if (dataGridViewColumn != null)
                {
                    dataGridViewColumn.Visible = false;
                }

                var gridViewColumn = dgDetails.Columns["uAmount"];
                if (gridViewColumn != null)
                {
                    gridViewColumn.Visible = false;
                }
            }
            else
            {
                cmbOpType_SelectedIndexChanged(null, null);
                var dataGridViewColumn = dgDetails.Columns["UnitName"];
                if (dataGridViewColumn != null)
                {
                    dataGridViewColumn.Visible = true;
                }

                var gridViewColumn = dgDetails.Columns["UnitAmount"];
                if (gridViewColumn != null)
                {
                    gridViewColumn.Visible = true;
                }
            }

            //药库可出库药品
            LoadDrugOutStore();

            ////获取药品批次信息
            InvokeController("GetDrugBatchInfo", frmName);
            InvokeController("LoadBillDetails", frmName);
            SetGridExpress();

            //表格验证
            // SetValidTime();
            timeOutData.Focus();
        }

        #region 事件

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmbOpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOpType.SelectedValue != null)
            {
                if (frmName == "FrmOutStoreDetailDW")
                {
                    if (cmbOpType.SelectedValue.ToString() == DGConstant.OP_DW_REPORTLOSS)
                    {
                        this.txtDept.Enabled = false;
                        this.txtLostReason.Visible = true;
                        this.lbLostReason.Visible = true;
                    }
                    else
                    {
                        this.txtDept.Enabled = true;
                        InvokeController("GetDrugRelateDept", frmName, cmbOpType.SelectedValue.ToString());
                        txtDept.MemberValue = CurretDwOutStoreHead.ToDeptID;
                        txtRemark.Text = CurretDwOutStoreHead.Remark;
                        txtLostReason.Text = CurretDwOutStoreHead.LostReason;
                        timeOutData.Value = CurretDwOutStoreHead.RegTime;
                        this.txtLostReason.Visible = false;
                        this.lbLostReason.Visible = false;
                    }

                    InvokeController("LoadBillDetails", frmName);
                    InvokeController("RefreshHead", frmName); //填充出库的主表信息
                }
                else
                {
                    this.lbLostReason.Visible = false;
                    this.btnReadInStore.Visible = false;
                    this.btnApply.Visible = false;
                    this.txtDept.Enabled = true;
                    this.txtLostReason.Visible = false;
                    InvokeController("GetDrugRelateDept", frmName, cmbOpType.SelectedValue.ToString());
                    txtDept.MemberValue = CurrentDSOuttoreHead.ToDeptID;
                    txtRemark.Text = CurrentDSOuttoreHead.Remark;
                    timeOutData.Value = CurrentDSOuttoreHead.RegTime;
                    InvokeController("LoadBillDetails", frmName);
                    InvokeController("RefreshHead", frmName); //填充出库的主表信息
                }

                SetGridExpress();
            }
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

            if (e.ColumnIndex == 5 || e.ColumnIndex == 7)
            {
                if (frmName == "FrmOutStoreDetailDW")
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
        /// 价格计算
        /// </summary>
        /// <param name="e">参数</param>
        private void ProcesDsPrice(DataGridViewCellEventArgs e)
        {
            DataRow currentRow = ((DataTable)dgDetails.DataSource).Rows[e.RowIndex];
            decimal amount = currentRow["pAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["pAmount"]);
            decimal unitAmonut = currentRow["uAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["uAmount"]);
            decimal packAmount = currentRow["packamount"] == DBNull.Value ? 1 : Convert.ToDecimal(currentRow["packamount"]);
            decimal stockPrice = currentRow["StockPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["StockPrice"]);
            decimal retailPrice = currentRow["RetailPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["RetailPrice"]);
            currentRow["StockFee"] = stockPrice * amount + (stockPrice / packAmount) * unitAmonut;
            currentRow["RetailFee"] = retailPrice * amount + (retailPrice / packAmount) * unitAmonut;
            InvokeController("ComputeTotalFee", frmName);
            dgDetails.Refresh();
        }

        /// <summary>
        /// 价格计算
        /// </summary>
        /// <param name="e">参数</param>
        private void ProcessDwPrice(DataGridViewCellEventArgs e)
        {
            DataRow currentRow = ((DataTable)dgDetails.DataSource).Rows[e.RowIndex];
            decimal amount = currentRow["pAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["pAmount"]);
            decimal stockPrice = currentRow["StockPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["StockPrice"]);
            decimal retailPrice = currentRow["RetailPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["RetailPrice"]);
            //如果是药库退货，自动变负数
            if (cmbOpType.SelectedValue.ToString() == DGConstant.OP_DW_RETURNSTORE)
            {
                if (amount > 0)
                {
                    amount = -amount;
                    currentRow["pAmount"] = amount;
                }
            }

            currentRow["Amount"] = amount;
            currentRow["StockFee"] = stockPrice * amount;
            currentRow["RetailFee"] = retailPrice * amount;
            InvokeController("ComputeTotalFee", frmName);
            dgDetails.Refresh();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="colIndex">列索引</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="jumpStop">是否合法</param>
        private void dgDetails_DataGridViewCellPressEnterKey(object sender, int colIndex, int rowIndex, ref bool jumpStop)
        {
            if (colIndex == 5)
            {
                dgDetails.EndEdit();
                DataTable dtSource = (DataTable)dgDetails.DataSource;
                DataRow currentRow = dtSource.Rows[rowIndex];
                decimal amount = currentRow["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["Amount"]);
                decimal totoal = currentRow["totalNum"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["totalNum"]);
                if (System.Math.Abs(amount) > totoal)
                {
                    MessageBoxEx.Show("错误，当前数量大于库存总量");
                    jumpStop = true;
                }

                dgDetails.BeginEdit(true);
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
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgDetails.CurrentCell != null)
            {
                int rowid = this.dgDetails.CurrentCell.RowIndex;
                DataTable dt = (DataTable)dgDetails.DataSource;
                if (dt.Rows[rowid]["OutDetailID"] == DBNull.Value
                    || dt.Rows[rowid]["OutDetailID"].ToString() == "0")
                {
                    dt.Rows.RemoveAt(rowid);
                }
                else
                {
                    lstDeleteDetails.Add(Convert.ToInt32(dt.Rows[rowid]["OutDetailID"]));
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
        private void btnSaveBill_Click(object sender, EventArgs e)
        {
            if (CheckDepte())
            {
                DataTable dtSource = (DataTable)dgDetails.DataSource;
                string errMsg = string.Empty;
                string errCol = string.Empty;
                int errRow = -1;
                if (dtSource.Rows.Count == 0)
                {
                    return;
                }

                if (dtSource.Rows[dtSource.Rows.Count - 1]["DrugID"] == DBNull.Value)
                {
                    dtSource.Rows.RemoveAt(dtSource.Rows.Count - 1);
                }

                dgDetails.EndEdit();
                if (!CheckDetails(dtSource, null, new string[2] { "DrugID", "BatchNO" }, out errMsg, out errCol, out errRow))
                {
                    MessageBoxEx.Show(string.Format(errMsg, dgDetails.Columns[errCol].HeaderText));
                    dgDetails.CurrentCell = dgDetails[errCol, errRow];
                    return;
                }

                InvokeController("SaveBill", frmName);
            }
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
        /// 加载绑定药品信息
        /// </summary>
        private void LoadDrugOutStore()
        {
            //绑定药品信息
            InvokeController("GetOutStoreDrugInfo", frmName);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            if (CheckDepte())
            {
                dgDetails.AddRow();
            }
        }

        /// <summary>
        /// 检查往来科室
        /// </summary>
        /// <returns>是否通过</returns>
        private bool CheckDepte()
        {
            if (this.cmbOpType.SelectedValue.ToString() != "123" && (txtDept.MemberValue == null || txtDept.MemberValue.ToString() == "0"))
            {
                MessageBoxEx.Show("请选择往来科室");
                txtDept.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="selectedValue">选中项值</param>
        /// <param name="stop">是否合法</param>
        /// <param name="customNextColumnIndex">列索引</param>
        private void dgDetails_SelectCardRowSelected(object selectedValue, ref bool stop, ref int customNextColumnIndex)
        {
            DataRow selectRow = (DataRow)selectedValue;
            int colId = dgDetails.CurrentCell.ColumnIndex;
            int rowId = dgDetails.CurrentCell.RowIndex;
            DataTable dtSource = (DataTable)dgDetails.DataSource;
            if (frmName == "FrmOutStoreDetailDW")
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
                    dtSource.Rows[rowId]["BatchNO"] = selectRow["BatchNO"];
                    dtSource.Rows[rowId]["ValidityDate"] = selectRow["ValidityTime"];
                    dtSource.Rows[rowId]["StockPrice"] = selectRow["StockPrice"];
                    dtSource.Rows[rowId]["StockFee"] = 0.00;
                    dtSource.Rows[rowId]["RetailPrice"] = selectRow["RetailPrice"];
                    dtSource.Rows[rowId]["RetailFee"] = 0.00;
                    dtSource.Rows[rowId]["Amount"] = 0;
                    dtSource.Rows[rowId]["totalNum"] = selectRow["BatchAmount"];// selectRow["totalNum"];
                    dtSource.Rows[rowId]["CtypeID"] = selectRow["CtypeID"];
                    InvokeController("ComputeTotalFee", frmName);
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
                    dtSource.Rows[rowId]["Amount"] = 0;
                    dtSource.Rows[rowId]["UnitAmount"] = selectRow["packamount"];
                    dtSource.Rows[rowId]["BatchNO"] = selectRow["BatchNO"];
                    dtSource.Rows[rowId]["ValidityDate"] = selectRow["ValidityTime"];
                    dtSource.Rows[rowId]["StockPrice"] = selectRow["StockPrice"];
                    dtSource.Rows[rowId]["StockFee"] = 0.00;
                    dtSource.Rows[rowId]["RetailPrice"] = selectRow["RetailPrice"];
                    dtSource.Rows[rowId]["RetailFee"] = 0.00;
                    dtSource.Rows[rowId]["totalNum"] = selectRow["BatchAmount"];// selectRow["totalNum"];
                    dtSource.Rows[rowId]["CtypeID"] = selectRow["CtypeID"];
                    InvokeController("ComputeTotalFee", frmName);
                }
            }

            dgDetails.Refresh();
        }

        /// <summary>
        /// 绑定明细表信息
        /// </summary>
        /// <param name="inDetails">明细数据源</param>
        public void BindDetailsGrid(DataTable inDetails)
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
        #endregion

        /// <summary>
        /// 快捷键盘
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmOutStoreDetail_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    btnSaveBill_Click(null, null);
                    break;
                case Keys.F3:
                    btnNewBill_Click(null, null);
                    break;
            }
        }

        /// <summary>
        /// 数据入库单
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnReadInStore_Click(object sender, EventArgs e)
        {
            if (CheckDepte())
            {
                InvokeController("ShowDialog", "FrmSelectBillNo");
            }
        }

        /// <summary>
        /// 申请单转出库单
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            InvokeController("InvokeController");
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddDetail_Click(null, null);
            }
        }
    }
}
