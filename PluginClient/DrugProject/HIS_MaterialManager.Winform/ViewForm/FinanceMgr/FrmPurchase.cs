using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MaterialManage;
using HIS_MaterialManage.Winform.IView;

namespace HIS_MaterialManage.Winform.ViewForm
{
    /// <summary>
    /// 采购计划
    /// </summary>
    public partial class FrmPurchase : BaseFormBusiness, IFrmPurchase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmPurchase()
        {
            InitializeComponent();
        }

        #region 自定义属性方法

        /// <summary>
        /// 当前登录用户信息
        /// </summary>
        private SysLoginRight loginUserInfo;

        /// <summary>
        /// 当前操作状态
        /// </summary>
        private OperatorStatus currentOperateStatus = OperatorStatus.SaveBill;

        /// <summary>
        /// 操作状态枚举
        /// </summary>
        public enum OperatorStatus
        {
            AddBill,//添加单据
            EditBill,//修改单据
            SaveBill//保存单据
        }

        /// <summary>
        /// 当新增，修改单据的时候，记录删除的记录
        /// </summary>
        private List<int> lstDeleteDetails = new List<int>();

        /// <summary>
        /// 设置控件使用状态
        /// </summary>
        /// <param name="operateStatus">操作状态</param>
        private void SetOperatorStatus(OperatorStatus operateStatus)
        {
            switch (operateStatus)
            {
                case OperatorStatus.AddBill:
                    SetAddBillStatus();
                    break;
                case OperatorStatus.EditBill:
                    SetUpdateBillStatus();
                    break;
                case OperatorStatus.SaveBill:
                    SetSaveBillStatus();
                    break;
            }
        }

        /// <summary>
        /// 设置明细表格只读属性
        /// </summary>
        private void SetPlanDetailGridColumnStaus()
        {
            dgDetail.Columns["MaterialID"].ReadOnly = false;
            dgDetail.Columns["CenterMatName"].ReadOnly = true;
            dgDetail.Columns["Spec"].ReadOnly = true;
            dgDetail.Columns["ProductName"].ReadOnly = true;
            dgDetail.Columns["Amount"].ReadOnly = false;
            dgDetail.Columns["UnitName"].ReadOnly = true;
            dgDetail.Columns["StockPrice"].ReadOnly = false;
            dgDetail.Columns["RetailPrice"].ReadOnly = false;
            dgDetail.Columns["StockFee1"].ReadOnly = true;
            dgDetail.Columns["RetailFee1"].ReadOnly = true;
        }

        /// <summary>
        /// 设置新增单据状态
        /// </summary>
        private void SetAddBillStatus()
        {
            btnAddHead.Enabled = false;
            btnModifyHead.Enabled = false;
            btnDeleteHead.Enabled = false;
            btn_Check.Enabled = false;
            btn_cancel.Enabled = true;
            btnAddDetail.Enabled = true;
            btnDeleteDetail.Enabled = true;
            btnSave.Enabled = true;
            btnLowerLimit.Enabled = true;
            btnUpperLowerLimit.Enabled = true;
            dgDetail.ReadOnly = false;
            dgHead.ReadOnly = false;
            SetPlanDetailGridColumnStaus();
            currentOperateStatus = OperatorStatus.AddBill;
            SetHeadGridReadOnly();
        }

        /// <summary>
        /// 设置网格只读
        /// </summary>
        private void SetHeadGridReadOnly()
        {
            dgHead.Columns[0].ReadOnly = true;
            dgHead.Columns[1].ReadOnly = true;
            dgHead.Columns[2].ReadOnly = true;
            dgHead.Columns[3].ReadOnly = true;
            dgHead.Columns[4].ReadOnly = true;
            dgHead.Columns[5].ReadOnly = true;
            dgHead.Columns[6].ReadOnly = true;
            dgHead.Columns[7].ReadOnly = true;
            dgHead.Columns[8].ReadOnly = true;
        }

        /// <summary>
        /// 设置修改单据状态
        /// </summary>
        private void SetUpdateBillStatus()
        {
            btnAddHead.Enabled = false;
            btnModifyHead.Enabled = false;
            btnDeleteHead.Enabled = false;
            btn_Check.Enabled = false;
            btn_cancel.Enabled = true;
            btnAddDetail.Enabled = true;
            btnDeleteDetail.Enabled = true;
            btnSave.Enabled = true;
            btnLowerLimit.Enabled = true;
            btnUpperLowerLimit.Enabled = true;
            dgDetail.ReadOnly = false;
            dgHead.ReadOnly = false;
            currentOperateStatus = OperatorStatus.EditBill;
            SetPlanDetailGridColumnStaus();
            SetHeadGridReadOnly();
        }

        /// <summary>
        /// 设置保存后单据状态
        /// </summary>
        private void SetSaveBillStatus()
        {
            btnAddHead.Enabled = true;
            btnModifyHead.Enabled = true;
            btnDeleteHead.Enabled = true;
            btn_Check.Enabled = true;
            btn_cancel.Enabled = false;
            btnAddDetail.Enabled = false;
            btnDeleteDetail.Enabled = false;
            btnSave.Enabled = false;
            btnLowerLimit.Enabled = false;
            btnUpperLowerLimit.Enabled = false;
            dgDetail.ReadOnly = true;
            dgHead.ReadOnly = true;
            dgHead.Enabled = true;
            currentOperateStatus = OperatorStatus.SaveBill;
            lstDeleteDetails.Clear();
        }

        /// <summary>
        /// 查询采购计划单据
        /// </summary>
        private void LoadPlanHeadData()
        {
            if (cmbStoreRoom.SelectedIndex < 0)
            {
                MessageBox.Show("没有选择采购物资库，请先选择物资库！");
                return;
            }

            if (dtpBillDate.Bdate.Value > dtpBillDate.Edate.Value)
            {
                MessageBox.Show("开始日期大于结束日期，请修改查询日期");
                return;
            }

            //查询采购计划单
            InvokeController("GetPlanHeadData");
        }

        /// <summary>
        /// 判断是否有重复的物资添加
        /// </summary>
        /// <param name="dtSource">物资明细</param>
        /// <param name="materialID">物资编码</param>
        /// <param name="rowIndex">行号</param>
        /// <returns>true：重复</returns>
        private bool IsRepeatMaterial(DataTable dtSource, string materialID, int rowIndex)
        {
            bool ret = false;
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                if (dtSource.Rows[i]["MaterialID"].ToString() == materialID && rowIndex != i)
                {
                    ret = true;
                }
            }

            return ret;
        }

        /// <summary>
        /// 验证采购计划明细
        /// </summary>
        /// <returns>true：验证不通过</returns>
        private bool CheckBill()
        {
            //明细数据验证
            DataTable dtDetails = (DataTable)dgDetail.DataSource;
            if (dtDetails.Rows.Count == 0)
            {
                MessageBoxEx.Show("没有数据需要提交");
                return true;
            }

            for (int index = 0; index < dtDetails.Rows.Count; index++)
            {
                DataRow currentRow = dtDetails.Rows[index];
                int intCheck = 0;
                decimal decCheck = 0;
                #region 物资选项
                if (currentRow["MaterialID"].ToString() == string.Empty)
                {
                    MessageBoxEx.Show("物资选项不能为空");
                    dgDetail.CurrentCell = dgDetail["MaterialID", index];
                    return true;
                }
                #endregion

                #region 采购数量
                if (!int.TryParse(currentRow["Amount"].ToString(), out intCheck))
                {
                    MessageBoxEx.Show("采购数量必须为整数");
                    dgDetail.CurrentCell = dgDetail["Amount", index];
                    return true;
                }
                else
                {
                    if (intCheck == 0)
                    {
                        MessageBoxEx.Show("采购数量不能等于零");
                        dgDetail.CurrentCell = dgDetail["Amount", index];
                        return true;
                    }
                    else if (intCheck < 0)
                    {
                        MessageBoxEx.Show("采购数量不能小于零");
                        dgDetail.CurrentCell = dgDetail["Amount", index];
                        return true;
                    }
                }
                #endregion

                #region 物资价格
                if (!decimal.TryParse(currentRow["StockPrice"].ToString(), out decCheck))
                {
                    MessageBoxEx.Show("进货价格格式不正确");
                    dgDetail.CurrentCell = dgDetail["StockPrice", index];
                    return true;
                }
                else
                {
                    if (decCheck <= 0)
                    {
                        MessageBoxEx.Show("进货价格必须大于0");
                        dgDetail.CurrentCell = dgDetail["StockPrice", index];
                        return true;
                    }
                }

                if (!decimal.TryParse(currentRow["RetailPrice"].ToString(), out decCheck))
                {
                    MessageBoxEx.Show("零售价格格式不正确");
                    dgDetail.CurrentCell = dgDetail["RetailPrice", index];
                    return true;
                }
                else
                {
                    if (decCheck <= 0)
                    {
                        MessageBoxEx.Show("零售价格必须大于0");
                        dgDetail.CurrentCell = dgDetail["RetailPrice", index];
                        return true;
                    }
                }
                #endregion
            }

            return false;
        }
        #endregion  

        #region 事件

        /// <summary>
        /// 打开界面加载数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmPurchase_OpenWindowBefore(object sender, EventArgs e)
        {
            SetPlanDetailGridColumnStaus();
            //从控制器取得当前登录用户对象
            InvokeController("GetLoginUserInfo");
            //审核状态
            InvokeController("GetCheckAuditData");
            //获取库房数据
            InvokeController("GetWareRoomData");
            //绑定物资选项卡数据
            InvokeController("GetMaterialDicShowCard");
            //初始化查询日期-默认当天
            dtpBillDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpBillDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            //根据查询条件获取采购计划表头数据
            LoadPlanHeadData();
        }

        /// <summary>
        /// 弹出网格选中数据
        /// </summary>
        /// <param name="selectedValue">选中的数据</param>
        /// <param name="stop">Stop标志</param>
        /// <param name="customNextColumnIndex">下一个得到焦点的列</param>
        private void gridBoxCard_detail_SelectCardRowSelected(object selectedValue, ref bool stop, ref int customNextColumnIndex)
        {
            DataRow selectRow = (DataRow)selectedValue;
            int colId = dgDetail.CurrentCell.ColumnIndex;
            int rowId = dgDetail.CurrentCell.RowIndex;
            DataTable dtSource = (DataTable)dgDetail.DataSource;
            if (customNextColumnIndex == 0)
            {
                if (IsRepeatMaterial(dtSource, selectRow["MaterialID"].ToString(), rowId) == true)
                {
                    stop = true;
                    string materialName = selectRow["CenterMatName"].ToString();
                    MessageBox.Show("您已经添加了【" + materialName + "】物资");
                    dgDetail[colId, rowId].Selected = true;
                    return;
                }

                dtSource.Rows[rowId]["MaterialID"] = selectRow["MaterialID"];
                dtSource.Rows[rowId]["CenterMatName"] = selectRow["CenterMatName"];
                dtSource.Rows[rowId]["Spec"] = selectRow["Spec"];
                dtSource.Rows[rowId]["ProductName"] = selectRow["ProductName"];
                dtSource.Rows[rowId]["UnitID"] = selectRow["UnitID"];
                dtSource.Rows[rowId]["UnitName"] = selectRow["UnitName"];
                dtSource.Rows[rowId]["RetailPrice"] = selectRow["StockPrice"];
                dtSource.Rows[rowId]["StockPrice"] = selectRow["RetailPrice"];
                dtSource.Rows[rowId]["PlanDetailID"] = 0;
                dtSource.Rows[rowId]["PlanHeadID"] = 0;//采购计划头表ID
            }
            dgDetail.Refresh();
        }

        /// <summary>
        /// 新增采购计划头
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_addHead_Click(object sender, EventArgs e)
        {
            //清除控件内容
            txtRemark.Text = string.Empty;
            if (dgDetail != null && dgDetail.Rows.Count > 0)
            {
                DataTable dt = dgDetail.DataSource as DataTable;
                dt.Rows.Clear();
                dgDetail.DataSource = dt;
            }

            //校验
            if (cmbStoreRoom.SelectedIndex < 0)
            {
                MessageBox.Show("没有选择采购物资库，请先选择物资库！");
                return;
            }

            int deptId = Convert.ToInt32(cmbStoreRoom.SelectedValue);
            string deptName = cmbStoreRoom.Text;
            //初始化化采购计划表结构
            InvokeController("GetPlanDetailData");
            //设置按钮状态
            SetOperatorStatus(OperatorStatus.AddBill);
            //自动生成采购计划单，在头表中增加一条记录
            dgHead.ReadOnly = false;
            dgHead.Enabled = true;
            dgHead.EndEdit();
            //int index = this.dataGrid_head.AddRow();
            DataTable dtHead = (DataTable)dgHead.DataSource;
            DataRow dr = dtHead.NewRow();
            dr["deptname"] = deptName;
            dr["RegTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dr["RegEmpName"] = loginUserInfo.EmpName;
            dr["AuditFlagName"] = "未审";
            dr["PlanHeadID"] = 0;
            dr["DeptID"] = deptId;
            dr["AuditFlag"] = 0;
            dr["RegEmpID"] = loginUserInfo.EmpId;
            dr["Remark"] = string.Empty;
            dtHead.Rows.Add(dr);
            dgHead.Refresh();
            dgHead.Rows[dtHead.Rows.Count - 1].Selected = true;
            this.dgHead.CurrentCell = this.dgHead.Rows[dtHead.Rows.Count - 1].Cells[0];
            dgHead.Enabled = false;
            dgHead.SetRowColor(dgHead.Rows.Count - 1, Color.Blue, true);
            //设置焦点
            txtRemark.Focus();
        }

        /// <summary>
        /// 修改采购计划头单据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_modifyHead_Click(object sender, EventArgs e)
        {
            //校验
            if (dgHead.Rows.Count <= 0 || dgHead.CurrentRow == null)
            {
                MessageBox.Show("您没有选中一条采购计划单");
                return;
            }

            //判断是否是审核状态
            int auditStatus = Convert.ToInt32(dgHead.CurrentRow.Cells["AuditFlag"].Value);
            if (auditStatus == 1)
            {
                MessageBox.Show("您要修改的单据已经审核，不能修改");
                return;
            }

            //设置按钮状态
            SetOperatorStatus(OperatorStatus.EditBill);
        }

        /// <summary>
        /// 删除采购计划头
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_deleteHead_Click(object sender, EventArgs e)
        {
            //校验
            if (dgHead.Rows.Count <= 0 || dgHead.CurrentRow == null)
            {
                MessageBoxEx.Show("您没有选中一条采购计划单");
                return;
            }

            //判断是否是审核状态
            int auditStatus = Convert.ToInt32(dgHead.CurrentRow.Cells["AuditFlag"].Value);
            if (auditStatus == 1)
            {
                MessageBoxEx.Show("您要删除的单据已经审核，不能删除");
                return;
            }

            if (MessageBox.Show(
                "您确认要删除所选的单据吗?", 
                "提示",
                MessageBoxButtons.OKCancel, 
                MessageBoxIcon.Question, 
                MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                int rowIndex = dgHead.CurrentCell.RowIndex;
                DataRow dRow = ((DataTable)dgHead.DataSource).Rows[rowIndex];
                InvokeController("DeleteBill", Convert.ToInt32(dRow["PlanHeadID"]));
            }
        }

        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_Check_Click(object sender, EventArgs e)
        {
            //校验
            if (dgHead.Rows.Count <= 0 || dgHead.CurrentRow == null)
            {
                MessageBox.Show("您没有选中一条采购计划单");
                return;
            }

            //判断是否是审核状态
            int auditStatus = Convert.ToInt32(dgHead.CurrentRow.Cells["AuditFlag"].Value);
            if (auditStatus == 1)
            {
                MessageBox.Show("您要审核的单据已经审核，不能再次审核");
                return;
            }

            if (MessageBox.Show(
                "您确认要审核所选的单据吗?",
                "提示",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                int rowIndex = dgHead.CurrentCell.RowIndex;
                DataTable dt = ((DataTable)(dgHead.DataSource));
                MW_PlanHead head = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<MW_PlanHead>(dt, rowIndex);
                InvokeController("AuditBill", head);
            }
        }

        /// <summary>
        /// 新增采购计划明细
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_addDetail_Click(object sender, EventArgs e)
        {
            dgDetail.AddRow();
        }

        /// <summary>
        /// 删除采购计划明细
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_deleteDetail_Click(object sender, EventArgs e)
        {
            if (dgDetail.CurrentCell != null)
            {
                int rowid = this.dgDetail.CurrentCell.RowIndex;
                DataTable dt = (DataTable)dgDetail.DataSource;
                if (MessageBox.Show(
                    "您确认要删除所选的物资吗?", 
                    "提示",
                    MessageBoxButtons.OKCancel, 
                    MessageBoxIcon.Question, 
                    MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    if (dt.Rows[rowid]["PlanDetailID"] == DBNull.Value || Convert.ToInt32(dt.Rows[rowid]["PlanDetailID"]) == 0)
                    {
                        dt.Rows.RemoveAt(rowid);
                    }
                    else
                    {
                        lstDeleteDetails.Add(Convert.ToInt32(dt.Rows[rowid]["PlanDetailID"]));
                        dt.Rows.RemoveAt(rowid);
                    }

                    InvokeController("ComputeTotalFee", (DataTable)dgDetail.DataSource);
                }
            }
        }

        /// <summary>
        /// 保存采购计划明细
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_save_Click(object sender, EventArgs e)
        {
            int index = dgHead.CurrentRow.Index;
            DataTable dtDetails = (DataTable)dgDetail.DataSource;
            if (dtDetails.Rows.Count == 0)
            {
                MessageBoxEx.Show("您没有输入明细记录不能保存，请输入明细！");
                return;
            }

            if (dtDetails.Rows[dtDetails.Rows.Count - 1]["MaterialID"] == DBNull.Value)
            {
                dtDetails.Rows.RemoveAt(dtDetails.Rows.Count - 1);
            }

            dgDetail.EndEdit();
            //单据内容检查
            if (CheckBill())
            {
                return;
            }

            //单据保存
            InvokeController("SaveBill");
            //设置操作按钮状态
            SetOperatorStatus(OperatorStatus.SaveBill);
            //刷新
            LoadPlanHeadData();
            dgHead[0, index].Selected = true;
        }

        /// <summary>
        /// 按下限提取数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_LowerLimit_Click(object sender, EventArgs e)
        {
            int deptId = Convert.ToInt32(cmbStoreRoom.SelectedValue);
            InvokeController("GetLessLowerLimitData", deptId);
        }

        /// <summary>
        /// 按上限提取数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_UpperLowerLimit_Click(object sender, EventArgs e)
        {
            int deptId = Convert.ToInt32(cmbStoreRoom.SelectedValue);
            InvokeController("GetLessUpperLimitData", deptId);
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void buttonX3_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_query_Click(object sender, EventArgs e)
        {
            if (currentOperateStatus != OperatorStatus.SaveBill)
            {
                if (MessageBox.Show(
                    "当前处于编辑状态，查询编辑单据数据将会丢失，您确认进行查询吗?",
                    "提示",
                    MessageBoxButtons.OKCancel, 
                    MessageBoxIcon.Question, 
                    MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    btn_cancel_Click(null, null);
                }
                else
                {
                    return;
                }
            }

            //加载头表数据
            LoadPlanHeadData();
        }

        /// <summary>
        /// 备注回车新增明细
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void txt_Remark_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btn_addDetail_Click(null, null);
            }
        }

        /// <summary>
        /// 编辑明细
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void gridBoxCard_detail_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 4 || e.ColumnIndex == 6 || e.ColumnIndex == 7)
            {
                DataRow currentRow = ((DataTable)dgDetail.DataSource).Rows[e.RowIndex];
                decimal amount = currentRow["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["Amount"]);
                decimal stockPrice = currentRow["StockPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["StockPrice"]);
                decimal retailPrice = currentRow["RetailPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(currentRow["RetailPrice"]);
                currentRow["StockFee"] = stockPrice * amount;
                currentRow["RetailFee"] = retailPrice * amount;
                InvokeController("ComputeTotalFee", (DataTable)dgDetail.DataSource);
                dgDetail.Refresh();
            }
        }

        /// <summary>
        /// 编辑头
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void dataGrid_head_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                InvokeController("GetPlanDetailData");
                InvokeController("ComputeTotalFee", (DataTable)dgDetail.DataSource);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        /// <summary>
        /// 明细键盘事件
        /// </summary>
        /// <param name="sender">网格控件</param>
        /// <param name="colIndex">列</param>
        /// <param name="rowIndex">行</param>
        /// <param name="jumpStop">是否跳转到下一个控件</param>
        private void gridBoxCard_detail_DataGridViewCellPressEnterKey(object sender, int colIndex, int rowIndex, ref bool jumpStop)
        {
            if (colIndex == 0)
            {
                dgDetail.EndEdit();
                DataTable dtSource = (DataTable)dgDetail.DataSource;
                DataRow currentRow = dtSource.Rows[rowIndex];
                for (int index = 0; index < dtSource.Rows.Count; index++)
                {
                    if (dtSource.Rows[index]["MaterialID"].ToString() == currentRow["MaterialID"].ToString()
                        && rowIndex != index)
                    {
                        MessageBox.Show("错误，不能添加重复的物资信息");
                        jumpStop = true;
                    }
                }

                dgDetail.BeginEdit(true);
            }
        }

        /// <summary>
        /// 取消编辑
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            LoadPlanHeadData();
            SetOperatorStatus(OperatorStatus.SaveBill);
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_export_Click(object sender, EventArgs e)
        {
            DataTable dtDetail = (DataTable)dgDetail.DataSource;
            if (dtDetail.Rows.Count < 0)
            {
                MessageBoxEx.Show("物资明细表中没有要导出的记录", "提示");
            }

            string path = AppDomain.CurrentDomain.BaseDirectory + @"ExportFile\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "采购计划单.xls";
            string strPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }

            Dictionary<string, string> dicColumns = new Dictionary<string, string>();
            dicColumns.Add("MaterialID", "编码");
            dicColumns.Add("CenterMatName", "物资名称");
            dicColumns.Add("Spec", "规格");
            dicColumns.Add("ProductName", "厂家");
            dicColumns.Add("Amount", "数量");
            dicColumns.Add("UnitName", "单位");
            dicColumns.Add("RetailPrice", "零售价");
            dicColumns.Add("StockPrice", "批发价");
            dicColumns.Add("RetailFee", "零售金额");
            dicColumns.Add("StockFee", "批发金额");
            Dictionary<string, string> dicDataFormat = new Dictionary<string, string>();
            dicDataFormat.Add("RetailPrice", "0.0000");
            dicDataFormat.Add("StockPrice", "0.0000");
            dicDataFormat.Add("RetailFee", "0.00");
            dicDataFormat.Add("StockFee", "0.00");
            EFWCoreLib.CoreFrame.Common.ExcelHelper.Export(dtDetail, "物资采购计划单", dicColumns, dicDataFormat, path);
            MessageBoxEx.Show("导出采购计划单成功，保存路径为：\r\n" + path);
            System.Diagnostics.Process.Start("explorer.exe", AppDomain.CurrentDomain.BaseDirectory + "ExportFile");
        }

        /// <summary>
        /// 注册键盘事件
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (btnAddHead.Focused)
                    {
                        btn_addHead_Click(null, null);
                    }

                    break;
                case Keys.F2:
                    btn_addHead_Click(null, null);
                    break;
                case Keys.F3:
                    btn_modifyHead_Click(null, null);
                    break;
                case Keys.F4:
                    btn_deleteHead_Click(null, null);
                    break;
                case Keys.F5:
                    btn_Check_Click(null, null);
                    break;
                case Keys.F6:
                    btn_cancel_Click(null, null);
                    break;
            }
        }

        /// <summary>
        /// 切换审核状态
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void cbx_AuditStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAuditStatus.SelectedValue.ToString() == "0")
            {
                btnAddHead.Enabled = true;
                btnModifyHead.Enabled = true;
                btnDeleteHead.Enabled = true;
                btn_Check.Enabled = true;
            }
            else
            {
                btnAddHead.Enabled = false;
                btnModifyHead.Enabled = false;
                btnDeleteHead.Enabled = false;
                btn_Check.Enabled = false;
            }
        }

        #endregion

        #region 接口
        /// <summary>
        /// 插入低于下限的物资
        /// </summary>
        /// <param name="dtRtn">物资数据</param>
        public void InsertLessLowerLimitData(DataTable dtRtn)
        {
            DataTable dtDetail = (DataTable)dgDetail.DataSource;
            if (dtDetail.Rows.Count > 0)
            {
                MessageBoxEx.Show("物资明细表中已经添加了物资信息，请清空后再添加", "提示");
                return;
            }

            InsertDetailData(dtRtn);
        }

        /// <summary>
        /// 插入明细数据
        /// </summary>
        /// <param name="dtRtn">明细数据</param>
        private void InsertDetailData(DataTable dtRtn)
        {
            decimal buyAmount = 0;
            decimal stockPrice = 0;
            decimal retailPrice = 0;
            decimal stockFee = 0;
            decimal setailFee = 0;
            DataTable dtDetail = (DataTable)dgDetail.DataSource;
            foreach (DataRow selectRow in dtRtn.Rows)
            {
                DataRow insertRow = dtDetail.NewRow();
                insertRow["MaterialID"] = selectRow["MaterialID"];
                insertRow["CenterMatName"] = selectRow["CenterMatName"];
                insertRow["Spec"] = selectRow["Spec"];
                insertRow["ProductName"] = selectRow["ProductName"];
                insertRow["UnitID"] = selectRow["UnitID"];
                insertRow["UnitName"] = selectRow["UnitName"];
                buyAmount = selectRow["BuyAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(selectRow["BuyAmount"]);
                stockPrice = selectRow["StockPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(selectRow["StockPrice"]);
                retailPrice = selectRow["RetailPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(selectRow["RetailPrice"]);
                insertRow["Amount"] = selectRow["BuyAmount"];
                insertRow["RetailPrice"] = selectRow["StockPrice"];
                insertRow["StockPrice"] = selectRow["RetailPrice"];
                insertRow["PlanDetailID"] = 0;
                insertRow["PlanHeadID"] = 0;//采购计划头表ID
                stockFee = buyAmount * stockPrice;
                setailFee = buyAmount * retailPrice;
                insertRow["StockFee"] = stockFee;
                insertRow["RetailFee"] = setailFee;
                dtDetail.Rows.Add(insertRow);
                InvokeController("ComputeTotalFee", dtDetail);
            }
        }

        /// <summary>
        /// 插入低于上限的物资数据
        /// </summary>
        /// <param name="dtRtn">物资数据</param>
        public void InsertLessUpperLimitData(DataTable dtRtn)
        {
            DataTable dtDetail = (DataTable)dgDetail.DataSource;
            if (dtDetail.Rows.Count > 0)
            {
                MessageBoxEx.Show("物资明细表中已经添加了物资信息，请清空后再添加", "提示");
                return;
            }

            InsertDetailData(dtRtn);
        }

        /// <summary>
        /// 绑定审核状态
        /// </summary>
        /// <param name="dt">审核状态数据集</param>
        public void BindAuditStatus(DataTable dt)
        {
            cbxAuditStatus.DataSource = dt;
            cbxAuditStatus.ValueMember = "ID";
            cbxAuditStatus.DisplayMember = "Name";
            cbxAuditStatus.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定采购计划头表网格
        /// </summary>
        /// <param name="dt">采购计划头表数据集</param>
        public void BindPlanHeadGrid(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row["AuditTime"] != DBNull.Value && row["AuditTime"].ToString().Contains("1900-01-01"))
                {
                    row["AuditTime"] = DBNull.Value;
                }
            }

            dgHead.DataSource = dt;
        }

        /// <summary>
        /// 绑定采购计划明细表格
        /// </summary>
        /// <param name="dt">采购计划明细表数据集</param>
        public void BindPlanDetailGrid(DataTable dt)
        {
            if (dt != null)
            {
                dgDetail.EndEdit();
                dgDetail.DataSource = dt;
            }
        }

        /// <summary>
        /// 取得登录用户信息
        /// </summary>
        /// <param name="loginUserInfo">登录用户对象</param>
        public void GetLoginUserInfo(SysLoginRight loginUserInfo)
        {
            this.loginUserInfo = loginUserInfo;
        }

        /// <summary>
        /// 获取删除的单据明细ID
        /// </summary>
        /// <returns>明细ID列表</returns>
        public List<int> GetDeleteDetails()
        {
            return lstDeleteDetails;
        }

        /// <summary>
        /// 取得采购计划头表信息
        /// </summary>
        /// <returns>采购计划表头实体</returns>
        public MW_PlanHead GetPlanHeadInfo()
        {
            int currentIndex = dgHead.CurrentCell.RowIndex;
            DataTable dt = ((DataTable)(dgHead.DataSource));
            MW_PlanHead head = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<MW_PlanHead>(dt, currentIndex);
            head.Remark = txtRemark.Text;
            return head;
        }

        /// <summary>
        /// 取得当前编辑单据明细
        /// </summary>
        /// <returns>单据明细</returns>
        public DataTable GetPlanDetailInfo()
        {
            DataTable dtPanDetail = ((DataTable)(dgDetail.DataSource));
            return dtPanDetail;
        }

        /// <summary>
        /// 绑定物资库下拉框
        /// </summary>
        /// <param name="dtStoreRoom">物资库数据集</param>
        public void BindStoreRoomComboxList(DataTable dtStoreRoom)
        {
            cmbStoreRoom.DataSource = dtStoreRoom;
            cmbStoreRoom.ValueMember = "DeptID";
            cmbStoreRoom.DisplayMember = "DeptName";
            if (dtStoreRoom != null && dtStoreRoom.Rows.Count > 0)
            {
                cmbStoreRoom.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定物资ShowCard
        /// </summary>
        /// <param name="dtMaterialInfo">物资信息表</param>
        public void BindMaterialInfoCard(DataTable dtMaterialInfo)
        {
            dgDetail.SelectionCards[0].BindColumnIndex = 0;
            dgDetail.SelectionCards[0].CardColumn = "MaterialID|编码|55,CenterMatName|名称|180,Spec|规格|140,ProductName|生产厂家|120,UnitName|单位|40";
            dgDetail.SelectionCards[0].CardSize = new System.Drawing.Size(500, 276);
            dgDetail.SelectionCards[0].QueryFieldsString = "MaterialID,CenterMatName,MatName,MatCode,AliasName,PYCode,WBCode,TPYCode,TWBCode";
            dgDetail.BindSelectionCardDataSource(0, dtMaterialInfo);
        }

        /// <summary>
        /// 获取选中表头ID
        /// </summary>
        /// <returns>当前选中表头ID</returns>
        public Dictionary<string, string> GetCurrentHeadID()
        {
            if (dgHead.CurrentCell != null)
            {
                if (dgHead.CurrentCell.RowIndex >= 0)
                {
                    int currentIndex = dgHead.CurrentCell.RowIndex;
                    DataRow currentRow = ((DataTable)(dgHead.DataSource)).Rows[currentIndex];
                    Dictionary<string, string> rtn = new Dictionary<string, string>();
                    string planHeadId = currentRow["PlanHeadID"].ToString();
                    if (planHeadId == string.Empty)
                    {
                        planHeadId = "0";
                    }

                    rtn.Add("PlanHeadID", planHeadId);
                    txtRemark.Text = currentRow["Remark"].ToString();
                    return rtn;
                }
                else
                {
                    Dictionary<string, string> rtn = new Dictionary<string, string>();
                    rtn.Add("PlanHeadID", "-1");
                    return rtn;
                }
            }
            else
            {
                Dictionary<string, string> rtn = new Dictionary<string, string>();
                rtn.Add("PlanHeadID", "-1");
                return rtn;
            }
        }

        /// <summary>
        /// 显示金额信息
        /// </summary>
        /// <param name="stockFee">零售金额</param>
        /// <param name="retailFee">进货金额</param>
        public void ShowTotalFee(decimal stockFee, decimal retailFee)
        {
            string strFee = "合计零售金额：{0}  合计进货金额：{1}";
            strFee = string.Format(strFee, retailFee.ToString("0.00"), stockFee.ToString("0.00"));
            pnlStatus.Text = strFee;
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>查询条件</returns>
        public Dictionary<string, string> GetQueryCondition()
        {
            int deptId = 0;//物资库ID
            string beginDate = string.Empty;//开始日期
            string endDate = string.Empty;//结束日期
            int auditFlag = 0;//审核状态

            beginDate = dtpBillDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00");
            endDate = dtpBillDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59");
            auditFlag = Convert.ToInt32(cbxAuditStatus.SelectedValue);
            deptId = Convert.ToInt32(cmbStoreRoom.SelectedValue);
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            queryCondition.Add("a.DeptID", deptId.ToString());
            if (auditFlag == 0)
            {
                //按单据登记日期查询
                queryCondition.Add(string.Empty, "a.RegTime between '" + beginDate + "' and '" + endDate + "'");
            }
            else if (auditFlag == 1)
            {
                //按单据审核日期查询
                queryCondition.Add(string.Empty, "a.AuditTime between '" + beginDate + "' and '" + endDate + "'");
            }

            queryCondition.Add("a.AuditFlag", auditFlag.ToString());//审核状态
            return queryCondition;
        }
        #endregion
    }
}
