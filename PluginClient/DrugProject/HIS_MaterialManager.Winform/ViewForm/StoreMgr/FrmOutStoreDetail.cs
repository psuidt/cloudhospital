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
    /// 出库单编辑
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
        }

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
                    var dataGridViewColumn = dgDetails.Columns["DrugID"];
                    if (dataGridViewColumn != null)
                    {
                        dataGridViewColumn.ReadOnly = false;
                    }

                    var gridViewColumn = dgDetails.Columns["MatName"];
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
        /// 界面加载
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmOutStoreDetail_OpenWindowBefore(object sender, EventArgs e)
        {
            GridStatus = MWEnum.DetailsEditiStatus.UPDATING;
            cmbOpType.SelectedIndexChanged -= cmbOpType_SelectedIndexChanged;
            //绑定业务类型
            InvokeController("BuildOpType", frmName);
            cmbOpType.SelectedIndexChanged += cmbOpType_SelectedIndexChanged;
            cmbOpType_SelectedIndexChanged(null, null);
            MWEnum.BillEditStatus editStus = (MWEnum.BillEditStatus)InvokeController("GetBillEditStatus");
            LoadMWOutStore();
            InvokeController("LoadBillDetails", frmName);
            SetGridExpress();
            //表格验证
            timeOutData.Focus();
        }

        /// <summary>
        /// 加载绑定物资信息
        /// </summary>
        private void LoadMWOutStore()
        {
            //绑定药品信息
            InvokeController("GetOutStoreDrugInfo", frmName);
        }

        #region 私有变量
        /// <summary>
        /// 当前明细网格编辑状态
        /// </summary>
        private MWEnum.DetailsEditiStatus gridStatus;

        /// <summary>
        /// 需从后台删除的单据明细
        /// </summary>
        private List<int> lstDeleteDetails = new List<int>();
        #endregion

        #region 方法

        /// <summary>
        /// 审查是否选择科室
        /// </summary>
        /// <returns>false：未选择</returns>
        private bool CheckDepte()
        {
            //报损
            if (this.cmbOpType.SelectedValue.ToString() != MWConstant.OP_MW_REPORTLOSS && (txtDept.MemberValue == null || txtDept.MemberValue.ToString() == "0"))
            {
                MessageBoxEx.Show("请选择往来科室");
                txtDept.Focus();
                return false;
            }

            return true;
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
        /// <returns>false：不通过</returns>
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
        /// 大小比较
        /// </summary>
        /// <typeparam name="T">比较接口</typeparam>
        /// <param name="t1">开始时间</param>
        /// <param name="t2">结束时间</param>
        /// <returns>true：开始时间大于结束时间</returns>
        public bool CampareLarge<T>(T t1, T t2) where T : IComparable
        {
            return t1.CompareTo(t2) > 0 ? true : false;
        }

        #endregion

        #region 接口
        /// <summary>
        /// 选中的出库单数据
        /// </summary>
        public MW_OutStoreHead CurretOutStoreHead { get; set; }

        /// <summary>
        /// 往来科室数据绑定
        /// </summary>
        /// <param name="dt">往来科室数据</param>
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
        /// 绑定明细表信息
        /// </summary>
        /// <param name="dt">明细数据源</param>
        public void BindDetailsGrid(DataTable dt)
        {
            if (dt != null)
            {
                dgDetails.EndEdit();
                if (dt.Rows.Count > 0)
                {
                    dgDetails.DataSource = dt;
                    dgDetails.CurrentCell = dgDetails[1, 0];
                }
                else
                {
                    dgDetails.DataSource = dt;
                }
            }
        }

        /// <summary>
        /// 绑定入库单录入ShowCard
        /// </summary>
        /// <param name="dt">物资表</param>
        public void BindMaterialInfoCard(DataTable dt)
        {
            dgDetails.SelectionCards[0].BindColumnIndex = 0;
            dgDetails.SelectionCards[0].CardColumn = "MaterialID|编码|60,MatName|名称|auto,Spec|规格|120,ProductName|生产厂家|120,BatchNO|批号|70,ValidityTime|到效日期|auto,BatchAmount|批次数量|auto";
            dgDetails.SelectionCards[0].CardSize = new System.Drawing.Size(750, 276);
            dgDetails.SelectionCards[0].QueryFieldsString = "MatName,PYCode,WBCode";
            dgDetails.SelectionCards[0].SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByFirstChar;
            dgDetails.BindSelectionCardDataSource(0, dt);
        }

        /// <summary>
        /// 绑定业务类型
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
        /// 绑定出库实体
        /// </summary>
        /// <typeparam name="THead">出库类型</typeparam>
        /// <param name="inHead">出库实体</param>
        public void BindOutHeadInfo<THead>(THead inHead)
        {
            if (inHead != null)
            {
                if (inHead as MW_OutStoreHead != null)
                {
                    CurretOutStoreHead = inHead as MW_OutStoreHead;
                }
            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        public void CloseCurrentWindow()
        {
            InvokeController("Close", this);
            InvokeController("Show", "FrmOutStore");
        }

        /// <summary>
        /// 删除的明细数据
        /// </summary>
        /// <returns>明细集合</returns>
        public List<int> GetDeleteDetails()
        {
            return lstDeleteDetails;
        }

        /// <summary>
        /// 获取出库单头数据
        /// </summary>
        /// <returns>出库单头数据</returns>
        public MW_OutStoreHead GetHeadInfo()
        {
            MW_OutStoreHead inHead = new MW_OutStoreHead();
            frmCommon.GetValue<MW_OutStoreHead>(inHead);
            return inHead;
        }

        /// <summary>
        /// 初始化界面控件
        /// </summary>
        /// <param name="billStatus">状态</param>
        public void InitControStatus(MWEnum.BillEditStatus billStatus)
        {
            if (billStatus == MWEnum.BillEditStatus.ADD_STATUS)
            {
                btnNewBill.Enabled = true;
                cmbOpType.Enabled = true;
                txtDept.Enabled = true;
            }
            else
            {
                btnNewBill.Enabled = false;
                cmbOpType.Enabled = false;
                txtDept.Enabled = false;
            }
        }

        /// <summary>
        /// 清空界面数据
        /// </summary>
        public void NewBillClear()
        {
            InvokeController("LoadBillDetails", frmName);
            InvokeController("InitBillHead", "FrmOutStoreDS", MWEnum.BillEditStatus.ADD_STATUS, 0);
            cmbOpType.Focus();
        }

        /// <summary>
        /// 验证网格数据
        /// </summary>
        public void SetGridExpress()
        {
            Dictionary<string, string> dicExpress = new Dictionary<string, string>();
            dicExpress.Add("MaterialID", @"^[1-9]\d*$");//正整数
            dicExpress.Add("BatchNO", @"\S");//批号
            if (cmbOpType.SelectedValue.ToString() == MWConstant.OP_MW_RETURNSTORE)
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
        /// 金额计算
        /// </summary>
        /// <param name="stockFee">零售金额</param>
        /// <param name="retailFee">进货金额</param>
        public void ShowTotalFee(decimal stockFee, decimal retailFee)
        {
            string strFee = "合计零售金额：{0}  合计进货金额：{1}";
            strFee = string.Format(strFee, retailFee.ToString("0.00"), stockFee.ToString("0.00"));
            pnlStatus.Text = strFee;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 明细网格弹出数据选择
        /// </summary>
        /// <param name="selectedValue">选中的数据</param>
        /// <param name="stop">Stop标志</param>
        /// <param name="customNextColumnIndex">下一个得到焦点的列</param>
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
                dtSource.Rows[rowId]["BatchNO"] = selectRow["BatchNO"];
                dtSource.Rows[rowId]["ValidityDate"] = selectRow["ValidityTime"];
                dtSource.Rows[rowId]["StockPrice"] = selectRow["StockPrice"];
                dtSource.Rows[rowId]["StockFee"] = 0.00;
                dtSource.Rows[rowId]["RetailPrice"] = selectRow["RetailPrice"];
                dtSource.Rows[rowId]["RetailFee"] = 0.00;
                dtSource.Rows[rowId]["Amount"] = 0;
                dtSource.Rows[rowId]["UnitID"] = selectRow["UnitID"];
                dtSource.Rows[rowId]["UnitName"] = selectRow["UnitName"];
                dtSource.Rows[rowId]["totalNum"] = selectRow["totalNum"];
                InvokeController("ComputeTotalFee", frmName);
            }

            dgDetails.Refresh();
        }

        /// <summary>
        /// 切换业务类型
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void cmbOpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOpType.SelectedValue != null)
            {
                //报损出库
                if (cmbOpType.SelectedValue.ToString() == MWConstant.OP_MW_REPORTLOSS) 
                {
                    this.txtDept.Enabled = false;
                }
                else
                {
                    this.txtDept.Enabled = true;
                    InvokeController("GetDrugRelateDept", frmName, cmbOpType.SelectedValue.ToString());
                    txtDept.MemberValue = CurretOutStoreHead.ToDeptID;
                    txtRemark.Text = CurretOutStoreHead.Remark;
                    timeOutData.Value = CurretOutStoreHead.RegTime;
                }

                InvokeController("LoadBillDetails", frmName);
                InvokeController("RefreshHead", frmName); //填充出库的主表信息
                SetGridExpress();
            }
        }

        /// <summary>
        /// 编辑明细数据
        /// </summary>
        /// <param name="sender">网格控件</param>
        /// <param name="colIndex">列</param>
        /// <param name="rowIndex">行</param>
        /// <param name="jumpStop">禁止跳转标志</param>
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
        }

        /// <summary>
        /// 改变数量计算金额
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void dgDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            //数量改变
            if (e.ColumnIndex == 5 )
            {
                DataRow currentRow = ((DataTable)dgDetails.DataSource).Rows[e.RowIndex];
                decimal amount = currentRow["pAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["pAmount"]);
                //如果是药库退货，自动变负数
                if (cmbOpType.SelectedValue.ToString() == MWConstant.OP_MW_RETURNSTORE)
                {
                    if (amount > 0)
                    {
                        amount = -amount;
                        currentRow["pAmount"] = amount;
                    }
                }

                currentRow["Amount"] = amount;
                decimal stockPrice = currentRow["StockPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["StockPrice"]);
                decimal retailPrice = currentRow["RetailPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["RetailPrice"]);
                currentRow["StockFee"] = stockPrice * amount;
                currentRow["RetailFee"] = retailPrice * amount;
                InvokeController("ComputeTotalFee", frmName);
                dgDetails.Refresh();
            }
        }

        #endregion

        /// <summary>
        /// 保存出库单
        /// </summary>
        /// <param name="sender">控件</param>
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

                if (dtSource.Rows[dtSource.Rows.Count - 1]["MaterialID"] == DBNull.Value)
                {
                    dtSource.Rows.RemoveAt(dtSource.Rows.Count - 1);
                }

                dgDetails.EndEdit();
                if (!CheckDetails(
                    dtSource, 
                    null, 
                    new string[2] { "MaterialID", "BatchNO" },
                    out errMsg,
                    out errCol,
                    out errRow))
                {
                    MessageBoxEx.Show(string.Format(errMsg, dgDetails.Columns[errCol].HeaderText));
                    dgDetails.CurrentCell = dgDetails[errCol, errRow];
                    return;
                }

                InvokeController("SaveBill", frmName);
            }
        }

        /// <summary>
        /// 新增出库单
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnNewBill_Click(object sender, EventArgs e)
        {
            NewBillClear();
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
            InvokeController("Show", "FrmOutStore");
        }

        /// <summary>
        /// 新增明细
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            dgDetails.AddRow();
        }

        /// <summary>
        /// 删除明细
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgDetails.CurrentCell != null)
            {
                int rowid = this.dgDetails.CurrentCell.RowIndex;
                DataTable dt = (DataTable)dgDetails.DataSource;
                if (dt.Rows[rowid]["OutDetailId"] == DBNull.Value
                    || dt.Rows[rowid]["OutDetailId"].ToString() == "0")
                {
                    dt.Rows.RemoveAt(rowid);
                }
                else
                {
                    lstDeleteDetails.Add(Convert.ToInt32(dt.Rows[rowid]["OutDetailId"]));
                    dt.Rows.RemoveAt(rowid);
                }

                InvokeController("ComputeTotalFee", frmName);
            }
        }
    }
}
