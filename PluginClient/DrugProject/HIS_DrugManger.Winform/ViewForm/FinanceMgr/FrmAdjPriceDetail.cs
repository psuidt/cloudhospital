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
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.FinanceMgr;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.ViewForm
{
    public partial class FrmAdjPriceDetail : BaseFormBusiness, IFrmAdjPriceDetail
    {
        /// <summary>
        /// 科室ID
        /// </summary>
        public int DeptID { get; set; }

        /// <summary>
        /// 当前选中调价头ID
        /// </summary>
        public int CurrentHead { get; set; }

        #region 加载函数方法

        /// <summary>
        /// 需从后台删除的单据明细
        /// </summary>
        private List<int> lstDeleteDetails = new List<int>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmAdjPriceDetail()
        {
            InitializeComponent();
            frmAdjPrice.AddItem(txtAdjCode, "txtAdjCode");
            frmAdjPrice.AddItem(txtAdjRemark, "txtAdjRemark");
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
        /// Gets or sets获取当前选中行数信息
        /// </summary>
        /// <value>当前选中调价头对象</value>
        public DG_AdjHead CurretDGAdjHead { get; set; }

        /// <summary>
        /// 初始化网格状态
        /// </summary>
        /// <param name="billStatus">网格状态</param>
        public void InitControStatus(DGEnum.BillEditStatus billStatus)
        {
            if (billStatus == DGEnum.BillEditStatus.ADD_STATUS)
            {
                btnNewDetail.Enabled = true;
            }
            else
            {
                btnNewDetail.Enabled = false;
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
        /// <returns>返回结果</returns>
        public bool CheckDetails(DataTable dtSource, List<int> checkRows, string[] uniqueCol, out string errMsg, out string errCol, out int errRow)
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
            }

            return true;
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

        /// <summary>
        ///绑定药库表头实体
        /// </summary>
        /// <param name="inHead">表头实体</param>
        public void BindInHeadInfo<THead>(THead inHead)
        {
            if (inHead != null)
            {
                if (inHead as DG_AdjHead != null)
                {
                    CurretDGAdjHead = inHead as DG_AdjHead;
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
            dgDetails.SelectionCards[0].CardColumn = "DrugID|编码|55,ChemName|化学名称|auto,Spec|规格|120,ProductName|生产厂家|120,UnitName|单位|40,BatchNO|批号|100,ValidityTime|到效日期|auto";
            dgDetails.SelectionCards[0].CardSize = new System.Drawing.Size(700, 300);
            dgDetails.SelectionCards[0].QueryFieldsString = "ChemName,PYCode,WBCode";
            dgDetails.SelectionCards[0].SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByFirstChar;
            dgDetails.BindSelectionCardDataSource(0, dtDrugInfo);
        }

        /// <summary>
        /// 执行完成函数
        /// </summary>
        /// <param name="result">结果</param>
        public void ExcuteComplete(DGBillResult result)
        {
            if (result.Result == 0)
            {
                MessageBoxEx.Show("执行调价成功");
                btnRefreshDetail_Click(null, null);
                txtAdjCode.Text = string.Empty;
                txtAdjRemark.Text = string.Empty;
            }
            else
            {
                MessageBoxEx.Show(result.ErrMsg);
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 改变选中行事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 11)
            {
                DataRow currentRow = ((DataTable)dgDetails.DataSource).Rows[e.RowIndex];
                decimal adjAmount = currentRow["AdjAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["AdjAmount"]);
                decimal oldRetailPrice = currentRow["OldRetailPrice"] == DBNull.Value
                    ? 0
                    : Convert.ToDecimal(currentRow["OldRetailPrice"]);
                decimal newRetailPrice = currentRow["NewRetailPrice"] == DBNull.Value
                    ? 0
                    : Convert.ToDecimal(currentRow["NewRetailPrice"]);
                if (newRetailPrice > 0)
                {
                    btnExcute.Enabled = true;
                    currentRow["AdjRetailFee"] = (Math.Round(newRetailPrice, 2) - oldRetailPrice) * adjAmount;
                }
                else
                {
                    btnExcute.Enabled = false;
                    MessageBoxEx.Show("价格不能为负数");
                }

                dgDetails.Refresh();
            }
        }

        /// <summary>
        /// 打开窗体
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmAdjPriceDetail_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("LoadAdjDetails");
            InvokeController("GetOutStoreDrugInfo");
            btnExcute.Enabled = false;
        }

        /// <summary>
        /// 执行调价函数
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnExcute_Click(object sender, EventArgs e)
        {
            dgDetails.EndEdit();
            if (btnExcute.Enabled)
            {
                DataTable dt = dgDetails.DataSource as DataTable;
                bool isVail = true;
                if (dt.Rows.Count == 0)
                {
                    isVail = false;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(dt.Rows[i]["NewRetailPrice"].ToString()))
                    {
                        isVail = false;
                    }
                }

                if (isVail)
                {
                    try
                    {
                        bool isPass = true;
                        string pattern = @"^[A-Za-z0-9]+$";
                        Regex regex = new Regex(pattern);
                        if (!string.IsNullOrEmpty(txtAdjCode.Text) && regex.IsMatch(txtAdjCode.Text))
                        {
                            isPass = false;
                        }

                        if (isPass)
                        {
                            DataTable dtSource = (DataTable)dgDetails.DataSource;
                            string errMsg = string.Empty;
                            string errCol = string.Empty;
                            int errRow = -1;
                            if (!CheckDetails(dtSource, null, new string[2] { "DrugID", "BatchNO" }, out errMsg, out errCol, out errRow))
                            {
                                MessageBoxEx.Show(string.Format(errMsg, dgDetails.Columns[errCol].HeaderText));
                                dgDetails.CurrentCell = dgDetails[errCol, errRow];
                                return;
                            }

                            InvokeController("ExcutePrice", txtAdjCode.Text.Trim(), txtAdjRemark.Text.Trim(), dt);
                        }
                        else
                        {
                            MessageBoxEx.Show("编号必须是数字或字母");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxEx.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBoxEx.Show("现零售价不能为空");
                }
            }
            else
            {
                MessageBoxEx.Show("当前没有可执行调价记录");
            }
        }

        /// <summary>
        /// 删除调价明细操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnDelDetail_Click(object sender, EventArgs e)
        {
            if (dgDetails.CurrentCell != null)
            {
                int rowid = this.dgDetails.CurrentCell.RowIndex;
                DataTable dt = (DataTable)dgDetails.DataSource;
                dt.Rows.RemoveAt(rowid);
                if (dgDetails.Rows.Count > 0)
                {
                    btnExcute.Enabled = true;
                }
                else
                {
                    btnExcute.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 刷新调价明细网格
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnRefreshDetail_Click(object sender, EventArgs e)
        {
            lstDeleteDetails.Clear();
            InvokeController("LoadAdjDetails");
            btnExcute.Enabled = false;
        }

        /// <summary>
        ///  网格选择项后执行的事件
        /// </summary>
        /// <param name="selectedValue">选择项的值</param>
        /// <param name="stop">是否停止</param>
        /// <param name="customNextColumnIndex">索引</param>
        private void dgDetails_SelectCardRowSelected(object selectedValue, ref bool stop, ref int customNextColumnIndex)
        {
            DataRow selectRow = (DataRow)selectedValue;
            int colId = dgDetails.CurrentCell.ColumnIndex;
            int rowId = dgDetails.CurrentCell.RowIndex;
            DataTable dtSource = (DataTable)dgDetails.DataSource;
            if (customNextColumnIndex == 0)
            {
                dtSource.Rows[rowId]["DrugID"] = selectRow["DrugID"];
                dtSource.Rows[rowId]["ChemName"] = selectRow["ChemName"];
                dtSource.Rows[rowId]["Spec"] = selectRow["Spec"];
                dtSource.Rows[rowId]["BatchNO"] = selectRow["BatchNO"];
                dtSource.Rows[rowId]["ProductName"] = selectRow["ProductName"];
                dtSource.Rows[rowId]["PackUnit"] = selectRow["PackUnit"];
                dtSource.Rows[rowId]["MiniUnit"] = selectRow["MiniUnit"];
                dtSource.Rows[rowId]["PackUnitID"] = selectRow["PackUnitID"];
                dtSource.Rows[rowId]["MiniUnitID"] = selectRow["MiniUnitID"];
                dtSource.Rows[rowId]["OldRetailPrice"] = selectRow["BatchRetail"];
                dtSource.Rows[rowId]["AdjAmount"] = selectRow["totalNum"];
            }

            dgDetails.Refresh();
        }

        /// <summary>
        ///  新增调价明细操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnNewDetail_Click(object sender, EventArgs e)
        {
            dgDetails.AddRow();
            btnExcute.Enabled = true;
        }
        #endregion
    }
}
