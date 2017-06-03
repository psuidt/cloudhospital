using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 领药申请明细
    /// </summary>
    public partial class FrmApplyDetail : BaseFormBusiness, IFrmApplyDetails
    {
        /// <summary>
        /// 需从后台删除的单据明细
        /// </summary>
        private List<int> lstDeleteDetails = new List<int>();

        /// <summary>
        /// 设置网格验证规则
        /// </summary>
        private void SetGridExpress()
        {
            Dictionary<string, string> dicExpress = new Dictionary<string, string>();
            dicExpress.Add("DrugID", @"^[1-9]\d*$");//正整数
            dicExpress.Add("Amount", @"^[1-9]\d*$");//非0正整数
            dgDetails.SetExpress(dicExpress, (DataTable)dgDetails.DataSource);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmApplyDetail()
        {
            InitializeComponent();
            frmCommon.AddItem(timeReg, "RegTime");
            frmCommon.AddItem(cbWareHourse, "ToDeptID");
            frmCommon.AddItem(txtRemark, "Remark");
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmApplyDetail_OpenWindowBefore(object sender, EventArgs e)
        {
            this.timeReg.Value = System.DateTime.Now;
            InvokeController("LoadBillDetails", frmName);
            cbWareHourse.SelectedIndexChanged -= cbWareHourse_SelectedIndexChanged;

            //绑定业务类型
            InvokeController("GetWareHourse");
            cbWareHourse.SelectedIndexChanged += cbWareHourse_SelectedIndexChanged;
            this.txtRemark.Text = string.Empty;
            InvokeController("GetOutStoreDrugInfo", this.cbWareHourse.SelectedValue.ToString());
            SetGridExpress();
        }

        /// <summary>
        /// 初始化控件状态
        /// </summary>
        /// <param name="head">表头对象</param>
        public void InitControStatus(DS_ApplyHead head)
        {
            if (head.ApplyHeadID == 0)
            {
                btnNewBill.Enabled = true;
                cbWareHourse.Enabled = true;
            }
            else
            {
                btnNewBill.Enabled = false;
                cbWareHourse.Enabled = false;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cbWareHourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbWareHourse.SelectedValue != null)
            {
                InvokeController("GetOutStoreDrugInfo", this.cbWareHourse.SelectedValue.ToString());
                InvokeController("LoadBillDetails", frmName);
                InvokeController("RefreshHead", frmName); //填充出库的主表信息
            }
        }
        #region 接口

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        public void CloseCurrentWindow()
        {
            InvokeController("Close", this);
            InvokeController("Show", "FrmApply");
        }

        /// <summary>
        /// 从界面获取药库入库表头信息
        /// </summary>
        /// <returns>药库入库表头信息</returns>
        public DS_ApplyHead GetHeadInfo()
        {
            DS_ApplyHead inHead = new DS_ApplyHead();
            frmCommon.GetValue<DS_ApplyHead>(inHead);
            inHead.ToDeptName = cbWareHourse.Text;
            return inHead;
        }

        /// <summary>
        /// 绑定入库单录入ShowCard
        /// </summary>
        /// <param name="dtDrugInfo">药品信息</param>
        public void BindDrugInfoCard(DataTable dtDrugInfo)
        {
            dgDetails.SelectionCards[0].BindColumnIndex = 0;
            dgDetails.SelectionCards[0].CardColumn = "DrugID|编码|55,ChemName|化学名称|auto,Spec|规格|200,ProductName|生产厂家|200,UnitName|单位|40,BatchNO|批号|100,ValidityTime|到效日期|auto,BatchAmount|批次数量|auto";
            dgDetails.SelectionCards[0].CardSize = new System.Drawing.Size(800, 300);
            dgDetails.SelectionCards[0].QueryFieldsString = "ChemName,PYCode,WBCode";
            dgDetails.SelectionCards[0].SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByFirstChar;
            dgDetails.BindSelectionCardDataSource(0, dtDrugInfo);
        }

        /// <summary>
        /// 药剂 科室
        /// </summary>
        /// <param name="dt">数据源</param>
        public void BindWareHourse(DataTable dt)
        {
            cbWareHourse.DataSource = dt;
            cbWareHourse.DisplayMember = "DeptName";
            cbWareHourse.ValueMember = "DeptID";
            cbWareHourse.SelectedIndex = 0;
        }

        /// <summary>
        /// 删除列
        /// </summary>
        /// <returns>删除列的IDS</returns>
        public List<int> GetDeleteDetails()
        {
            return lstDeleteDetails;
        }

        /// <summary>
        /// 获取领药表头对象
        /// </summary>
        public DS_ApplyHead CurrentApplyHead { get; set; }

        /// <summary>
        /// 获取当前领药明细对象
        /// </summary>
        public DataTable CurrentDetail { get; set; }
        #endregion

        #region 事件
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="colIndex">列索引</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="jumpStop">结果</param>
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
        /// <param name="selectedValue">选中项的值</param>
        /// <param name="stop">是否合法</param>
        /// <param name="customNextColumnIndex">列索引</param>
        private void dgDetails_SelectCardRowSelected(object selectedValue, ref bool stop, ref int customNextColumnIndex)
        {
            DataRow selectRow = (DataRow)selectedValue;
            int colId = dgDetails.CurrentCell.ColumnIndex;
            int rowId = dgDetails.CurrentCell.RowIndex;
            DataTable dtSource = (DataTable)dgDetails.DataSource;
            if (customNextColumnIndex == 0)
            {
                dtSource.Rows[rowId]["DrugID"] = selectRow["DrugID"];
                dtSource.Rows[rowId]["CTypeID"] = selectRow["CTypeID"];
                dtSource.Rows[rowId]["ChemName"] = selectRow["ChemName"];
                dtSource.Rows[rowId]["Spec"] = selectRow["Spec"];
                dtSource.Rows[rowId]["ProductName"] = selectRow["ProductName"];
                dtSource.Rows[rowId]["UnitID"] = selectRow["PackUnitID"];
                dtSource.Rows[rowId]["UnitName"] = selectRow["UnitName"];
                dtSource.Rows[rowId]["BatchNO"] = selectRow["BatchNO"];
                dtSource.Rows[rowId]["FactAmount"] = 0;

                // dtSource.Rows[rowId]["ValidityDate"] = selectRow["ValidityDate"];
                dtSource.Rows[rowId]["StockPrice"] = selectRow["StockPrice"];
                dtSource.Rows[rowId]["StockFee"] = 0.00;
                dtSource.Rows[rowId]["RetailPrice"] = selectRow["RetailPrice"];
                dtSource.Rows[rowId]["RetailFee"] = 0.00;
                dtSource.Rows[rowId]["totalNum"] = selectRow["totalNum"];

                // InvokeController("ComputeTotalFee", frmName);
            }

            dgDetails.Refresh();
        }
        #endregion

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            if (CheckDepte())
            {
                // InvokeController("GetOutStoreDrugInfo", this.cbWareHourse.SelectedValue.ToString());
                dgDetails.AddRow();
            }
        }

        /// <summary>
        /// 检查合法性
        /// </summary>
        /// <returns>是否合法</returns>
        private bool CheckDepte()
        {
            if (this.cbWareHourse.SelectedValue == null)
            {
                MessageBoxEx.Show("请选择药库");
                cbWareHourse.Focus();
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
        /// <returns>是否合法</returns>
        public bool CheckDetails(DataTable dtSource, List<int> checkRows, string[] uniqueCol, out string errMsg, out string errCol, out int errRow)
        {
            errMsg = string.Empty;
            errCol = string.Empty;
            errRow = -1;
            for (int index = 0; index < dtSource.Rows.Count; index++)
            {
                if (dtSource.Rows[index]["amount"] != DBNull.Value && dtSource.Rows[index]["totalNum"] != DBNull.Value)
                {
                    if (Convert.ToInt32(dtSource.Rows[index]["amount"]) > Convert.ToInt32(dtSource.Rows[index]["totalNum"]))
                    {
                        errMsg = "输入数量大于库存总量";
                        errCol = dtSource.Columns[5].ColumnName;
                        errRow = index;
                        return false;
                    }
                }

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
        /// 绑定领药明细数据
        /// </summary>
        /// <param name="dt">领药明细数据</param>
        public void BindApplyDetail(DataTable dt)
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
        /// 绑定领药表头数据
        /// </summary>
        /// <param name="head">领药表头数据</param>
        public void BindInHeadInfo(DS_ApplyHead head)
        {
            CurrentApplyHead = head;
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
        private void btnSaveBill_Click(object sender, EventArgs e)
        {
            dgDetails.EndEdit();
            if (CheckBill())
            {
                InvokeController("SaveBill", frmName);
            }
        }

        /// <summary>
        /// 检查提交单据信息
        /// </summary>
        /// <returns>是否通过</returns>
        private bool CheckBill()
        {
            DataTable dtSource = (DataTable)dgDetails.DataSource;
            string errMsg = string.Empty;
            string errCol = string.Empty;
            int errRow = -1;
            if (dtSource.Rows.Count == 0)
            {
                MessageBoxShowSimple("没有数据需要提交");
                return false;
            }

            if (dtSource.Rows[dtSource.Rows.Count - 1]["DrugID"] == DBNull.Value)
            {
                dtSource.Rows.RemoveAt(dtSource.Rows.Count - 1);
            }

            bool rtn = CheckDetails(dtSource, null, new string[2] { "DrugID", "BatchNO" }, out errMsg, out errCol, out errRow);

            if (!rtn)
            {
                MessageBoxEx.Show(string.Format(errMsg, dgDetails.Columns[errCol].HeaderText));
                dgDetails.CurrentCell = dgDetails[errCol, errRow];
            }

            return rtn;
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
        /// 创建新单据更新界面
        /// </summary>
        public void NewBillClear()
        {
            InvokeController("LoadBillDetails", frmName);
            InvokeController("InitBillHead", string.Empty, "0", string.Empty, 0);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckDepte())
            {
                dgDetails.AddRow();
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void buttonItem4_Click(object sender, EventArgs e)
        {
            if (dgDetails.CurrentCell != null)
            {
                int rowid = this.dgDetails.CurrentCell.RowIndex;
                DataTable dt = (DataTable)dgDetails.DataSource;
                if (dt.Rows[rowid]["ApplyHeadID"] == DBNull.Value
                    || dt.Rows[rowid]["ApplyHeadID"].ToString() == "0")
                {
                    dt.Rows.RemoveAt(rowid);
                }
                else
                {
                    lstDeleteDetails.Add(Convert.ToInt32(dt.Rows[rowid]["ApplyDetailID"]));
                    dt.Rows.RemoveAt(rowid);
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmApplyDetail_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //case Keys.Enter:
                //    break;
                case Keys.F2:
                    btnSaveBill_Click(null, null);
                    break;
                case Keys.F3:
                    btnNewBill_Click(null, null);
                    break;
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

            DataRow currentRow = ((DataTable)dgDetails.DataSource).Rows[e.RowIndex];
            decimal amount = currentRow["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["Amount"]);
            decimal stockPrice = currentRow["StockPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["StockPrice"]);
            decimal retailPrice = currentRow["RetailPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["RetailPrice"]);
            currentRow["StockFee"] = stockPrice * amount;
            currentRow["RetailFee"] = retailPrice * amount;

            // InvokeController("ComputeTotalFee", frmName);
            dgDetails.Refresh();
        }
    }
}
