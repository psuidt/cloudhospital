using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.StoreMgr;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 库存上下限
    /// </summary>
    public partial class FrmSetLimit : BaseFormBusiness, IFrmSetLimit
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmSetLimit()
        {
            InitializeComponent();
        }

        #region 自定义属性和方法
        /// <summary>
        /// 操作状态枚举
        /// </summary>
        public enum OperatorStatus
        {
            AddBill,//添加
            SaveBill//保存
        }

        /// <summary>
        /// 验证明细数据
        /// </summary>
        /// <returns>是否合法</returns>
        private bool CheckData()
        {
            //明细数据验证
            DataTable dtDetails = (DataTable)dgLimit.DataSource;
            if (dtDetails.Rows.Count == 0)
            {
                MessageBoxEx.Show("没有数据需要提交");
                return true;
            }

            for (int index = 0; index < dtDetails.Rows.Count; index++)
            {
                DataRow currentRow = dtDetails.Rows[index];
                int intCheck = 0;

                #region 上下限验证
                if (!int.TryParse(currentRow["UpperLimit"].ToString(), out intCheck))
                {
                    MessageBoxEx.Show("库存上限必须为整数");
                    dgLimit.CurrentCell = dgLimit["UpperLimit", index];
                    return true;
                }
                else
                {
                    if (intCheck < 0)
                    {
                        MessageBoxEx.Show("库存上限不能为零");
                        dgLimit.CurrentCell = dgLimit["UpperLimit", index];
                        return true;
                    }
                }

                if (!int.TryParse(currentRow["LowerLimit"].ToString(), out intCheck))
                {
                    MessageBoxEx.Show("库存下限必须为整数");
                    dgLimit.CurrentCell = dgLimit["LowerLimit", index];
                    return true;
                }
                else
                {
                    if (intCheck < 0)
                    {
                        MessageBoxEx.Show("库存下限不能小于0");
                        dgLimit.CurrentCell = dgLimit["LowerLimit", index];
                        return true;
                    }
                }

                if (Convert.ToInt32(currentRow["LowerLimit"]) > Convert.ToInt32(currentRow["UpperLimit"]))
                {
                    MessageBoxEx.Show("库存下限不能大于上限");
                    dgLimit.CurrentCell = dgLimit["LowerLimit", index];
                    return true;
                }
                #endregion              
            }

            Dictionary<string, string> dicExpress = new Dictionary<string, string>();
            dicExpress.Add("UpperLimit", @"^\+?(0|[1-9][0-9]*)$");//正整数
            dicExpress.Add("LowerLimit", @"^\+?(0|[1-9][0-9]*)$");//正整数        
            SetExpress(dicExpress, dtDetails);
            string errMsg = string.Empty;
            string errCol = string.Empty;
            int errRow = -1;
            bool rtn = CheckDetails(dtDetails, null, new string[1] { "DrugID" }, out errMsg, out errCol, out errRow);
            if (!rtn)
            {
                MessageBoxEx.Show(string.Format(errMsg, dgLimit.Columns[errCol].HeaderText));
                dgLimit.CurrentCell = dgLimit[errCol, errRow];
                return true;
            }

            return false;
        }

        /// <summary>
        /// 设置控件使用状态
        /// </summary>
        /// <param name="operateStatus">操作状态</param>
        private void SetOperatorStatus(OperatorStatus operateStatus)
        {
            switch (operateStatus)
            {
                case OperatorStatus.AddBill:
                    btn_Cancel.Enabled = true;
                    btn_Save.Enabled = true;
                    btn_Setting.Enabled = false;
                    dgLimit.ReadOnly = false;
                    SetColumnReadOnly();
                    break;
                case OperatorStatus.SaveBill:
                    btn_Cancel.Enabled = false;
                    btn_Save.Enabled = false;
                    btn_Setting.Enabled = true;
                    dgLimit.ReadOnly = true;
                    break;
            }
        }

        /// <summary>
        /// 设置表格只读属性
        /// </summary>
        private void SetColumnReadOnly()
        {
            dgLimit.Columns["DrugID"].ReadOnly = true;
            dgLimit.Columns["ChemName"].ReadOnly = true;
            dgLimit.Columns["Spec"].ReadOnly = true;
            dgLimit.Columns["ProductName"].ReadOnly = true;
            dgLimit.Columns["Place"].ReadOnly = true;
            dgLimit.Columns["Amount"].ReadOnly = true;
            dgLimit.Columns["UnitName"].ReadOnly = true;
            dgLimit.Columns["UpperLimit"].ReadOnly = false;
            dgLimit.Columns["LowerLimit"].ReadOnly = false;
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
        /// 查询库存数据
        /// </summary>
        private void LoadData()
        {
            if (cmb_StoreRoom.SelectedIndex < 0)
            {
                MessageBox.Show("没有选择采购药库，请先选择药库！");
                return;
            }

            //查询
            InvokeController("GetStoreLimitData", frmName);
        }

        /// <summary>
        /// 创建查询类型
        /// </summary>
        /// <returns>返回表</returns>
        private DataTable CreateQueryType()
        {
            DataTable dtData = new DataTable();
            dtData.Columns.Add("ID");
            dtData.Columns.Add("Name");
            DataRow drData;
            drData = dtData.NewRow();
            drData[0] = 0;
            drData[1] = "全部";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 1;
            drData[1] = "未设置";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 2;
            drData[1] = "已设置";
            dtData.Rows.Add(drData);
            return dtData;
        }
        #endregion 

        #region 接口
        /// <summary>
        /// 绑定供库房控件
        /// </summary>
        /// <param name="dtDept">数据源</param>
        /// <param name="loginDeptId">登录部门ID</param>
        public void BindStoreRoomCombox(DataTable dtDept, int loginDeptId)
        {
            cmb_StoreRoom.DataSource = dtDept;
            cmb_StoreRoom.ValueMember = "DeptID";
            cmb_StoreRoom.DisplayMember = "DeptName";
            DataRow[] rows = dtDept.Select("DeptID=" + loginDeptId);
            if (rows.Length > 0)
            {
                cmb_StoreRoom.SelectedValue = loginDeptId;
                return;
            }

            if (dtDept.Rows.Count > 0)
            {
                cmb_StoreRoom.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定药品选择卡片
        /// </summary>
        /// <param name="dtDrug">药品信息</param>
        public void BindDrugSelectCard(DataTable dtDrug)
        {
            txt_Code.DisplayField = "ChemName";
            txt_Code.MemberField = "DrugID";
            txt_Code.CardColumn = "DrugID|编码|55,ChemName|化学名称|auto,Spec|规格|120,ProductName|生产厂家|120,PackUnit|单位|40";
            txt_Code.QueryFieldsString = "ChemName,TradeName,PYCode,WBCode,TPYCode,TWBCode";
            txt_Code.ShowCardWidth = 500;
            txt_Code.ShowCardDataSource = dtDrug;
        }

        /// <summary>
        /// 绑定库存上下限grid
        /// </summary>
        /// <param name="dtDrugLimit">库存数据</param>
        public void BindStoreLimitGrid(DataTable dtDrugLimit)
        {
            dgLimit.DataSource = dtDrugLimit;
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        public Dictionary<string, string> GetQueryCondition()
        {
            int deptId = 0;//药库ID
            deptId = Convert.ToInt32(cmb_StoreRoom.SelectedValue);
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            queryCondition.Add("a.DeptID", deptId.ToString());

            //药品ID
            if (!string.IsNullOrEmpty(txt_Code.Text))
            {
                queryCondition.Add("a.DrugID", txt_Code.MemberValue.ToString());//药品ID
            }

            if (cmbStatus.SelectedValue.ToString() == "0")
            {
            }
            else if (cmbStatus.SelectedValue.ToString() == "1")
            {
                queryCondition.Add(string.Empty, "(IsNull(a.UpperLimit,0)<=0 )");//药品ID
            }
            else if (cmbStatus.SelectedValue.ToString() == "2")
            {
                queryCondition.Add(string.Empty, "(IsNull(a.UpperLimit,0) > 0 )");//药品ID
            }

            return queryCondition;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="selectedValue">参数</param>
        private void txt_Code_AfterSelectedRow(object sender, object selectedValue)
        {
            LoadData();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmb_StoreRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmSetLimit_OpenWindowBefore(object sender, EventArgs e)
        {
            cmbStatus.DataSource = CreateQueryType();
            cmbStatus.DisplayMember = "Name";
            cmbStatus.ValueMember = "ID";
            cmbStatus.SelectedIndex = 0;

            //获取库房信息
            InvokeController("GetStoreRoomData", frmName);

            //获取药品数据
            InvokeController("GetDrugShowCardInfo", frmName);

            //查询库存数据
            InvokeController("GetStoreLimitData", frmName);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void cmb_StoreRoom_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btn_Setting_Click(object sender, EventArgs e)
        {
            if (cmb_StoreRoom.SelectedValue == null)
            {
                MessageBoxEx.Show("请选择库房");
                return;
            }

            if (dgLimit.Rows.Count <= 0)
            {
                MessageBoxEx.Show("没有查询到药品信息，不能进行设置");
                return;
            }

            //设置按钮状态
            SetOperatorStatus(OperatorStatus.AddBill);
            dgLimit.Rows[0].Selected = true;
            dgLimit.CurrentCell = dgLimit.Rows[0].Cells[7];
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            SetOperatorStatus(OperatorStatus.SaveBill);
            LoadData();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btn_Save_Click(object sender, EventArgs e)
        {
            dgLimit.EndEdit();

            //单据内容检查
            if (CheckData())
            {
                return;
            }

            //单据保存
            InvokeController("SaveStoreLimit", frmName, (DataTable)dgLimit.DataSource);

            //设置操作按钮状态
            SetOperatorStatus(OperatorStatus.SaveBill);

            //刷新
            LoadData();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="colIndex">列索引</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="jumpStop">是否合法</param>
        private void dgLimit_DataGridViewCellPressEnterKey(object sender, int colIndex, int rowIndex, ref bool jumpStop)
        {
            if (colIndex == 8 && rowIndex == dgLimit.Rows.Count - 1)
            {
                jumpStop = true;
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgLimit_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btn_Close_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }
        #endregion
    }
}
