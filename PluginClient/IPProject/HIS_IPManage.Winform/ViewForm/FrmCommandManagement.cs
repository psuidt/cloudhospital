using System;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    public partial class FrmCommandManagement : BaseFormBusiness, ICommandManagement
    {
        #region "属性"

        /// <summary>
        /// 统领数据列表
        /// </summary>
        private DataTable mCommandList = new DataTable();

        /// <summary>
        /// 入院科室ID
        /// </summary>
        public int PatDeptID
        {
            get
            {
                return txtDeptList.MemberValue == null ? 0 : int.Parse(txtDeptList.MemberValue.ToString());
            }
        }

        /// <summary>
        /// 执行科室ID
        /// </summary>
        public int ExecDeptID
        {
            get
            {
                return txtExecDeptList.MemberValue == null ? 0 : int.Parse(txtExecDeptList.MemberValue.ToString());
            }
        }

        /// <summary>
        /// 发药或退药统领
        /// </summary>
        public bool CommandStatus
        {
            get
            {
                if (cboMedicine.Checked)
                {
                    return true;
                }
                else if (cboDrugWithdrawal.Checked)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// 全部药品
        /// </summary>
        public bool Whole
        {
            get
            {
                return chkWhole.Checked;
            }
        }

        /// <summary>
        /// 口服药
        /// </summary>
        public bool IsOralMedicine
        {
            get
            {
                return chkOralMedicine.Checked;
            }
        }

        /// <summary>
        /// 针剂
        /// </summary>
        public bool IsInjection
        {
            get
            {
                return chkInjection.Checked;
            }
        }

        /// <summary>
        /// 大输液
        /// </summary>
        public bool IsLargeInfusion
        {
            get
            {
                return chkLargeInfusion.Checked;
            }
        }

        /// <summary>
        /// 中草药
        /// </summary>
        public bool IsChineseHerbalMedicine
        {
            get
            {
                return chkChineseHerbalMedicine.Checked;
            }
        }

        /// <summary>
        /// 发药或退药统领
        /// </summary>
        private bool mIsMedicineCommand = false;

        /// <summary>
        /// 发药或退药统领
        /// </summary>
        public bool IsMedicineCommand
        {
            get
            {
                return mIsMedicineCommand;
            }

            set
            {
                mIsMedicineCommand = value;
            }
        }

        /// <summary>
        /// 入院科室ID
        /// </summary>
        public int OrderDeptId
        {
            get
            {
                return txtDept.MemberValue == null ? 0 : int.Parse(txtDept.MemberValue.ToString());
            }
        }

        /// <summary>
        /// 单据状态
        /// </summary>
        public bool OrderStatus
        {
            get
            {
                if (cboOrderStatus.SelectedIndex == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 病人登记ID
        /// </summary>
        public int PatListId
        {
            get
            {
                return txtPatList.MemberValue == null ? 0 : int.Parse(txtPatList.MemberValue.ToString());
            }
        }

        /// <summary>
        /// 统领单据发送开始时间
        /// </summary>
        public DateTime StartDate
        {
            get
            {
                return sdtDate.Bdate.Value;
            }
        }

        /// <summary>
        /// 统领单据发送结束时间
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return sdtDate.Edate.Value;
            }
        }

        /// <summary>
        /// 统领单头ID
        /// </summary>
        public int BillHeadID
        {
            get
            {
                if (grdOrderrList.CurrentCell != null)
                {
                    int rowIndex = grdOrderrList.CurrentCell.RowIndex;
                    DataTable dt = grdOrderrList.DataSource as DataTable;
                    return Convert.ToInt32(dt.Rows[rowIndex]["BillHeadID"].ToString());
                }

                return 0;
            }
        }

        /// <summary>
        /// 单据状态
        /// </summary>
        public int DispDrugFlag
        {
            get
            {
                if (grdOrderrList.CurrentCell != null)
                {
                    int rowIndex = grdOrderrList.CurrentCell.RowIndex;
                    DataTable dt = grdOrderrList.DataSource as DataTable;
                    return Convert.ToInt32(dt.Rows[rowIndex]["DispDrugFlag"].ToString());
                }

                return 0;
            }
        }
        #endregion

        #region "事件"

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmCommandManagement()
        {
            InitializeComponent();
            frmSendDrugBill.AddItem(txtDeptList, string.Empty);
            frmSendDrugBill.AddItem(txtExecDeptList, string.Empty);

            frmDrugBillList.AddItem(txtDept, string.Empty);
            frmDrugBillList.AddItem(txtPatList, string.Empty);
            frmDrugBillList.AddItem(cboOrderStatus, string.Empty);
            frmDrugBillList.AddItem(sdtDate, string.Empty);
        }

        /// <summary>
        /// 窗体打开之前加载基础数据
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmCommandManagement_OpenWindowBefore(object sender, EventArgs e)
        {
            // 获取并绑定执行科室列表
            InvokeController("GetDeptDataSourceList");
            // 获取并绑定入院科室列表
            InvokeController("GetDeptList");
            // 绑定病人列表
            InvokeController("GetHasBeenSentDrugbillPatList");
            // 发退药申请默认选择发药
            cboMedicine.Checked = true;
            // 药品类型默认选择全部
            chkWhole.Checked = true;
            // 默认科室获得焦点
            txtDeptList.Focus();
            // 发送统领单日期
            sdtDate.Bdate.Value = DateTime.Now;
            sdtDate.Edate.Value = DateTime.Now;
            // 单据状态
            cboOrderStatus.SelectedIndex = 0;
            // 设置Grid选中行
            bindGridSelectIndex(grdOrderrList);
            bindGridSelectIndex(grdDurgBillSummary);
            bindGridSelectIndex(grdDurgBillDetail);
        }

        /// <summary>
        /// 查询统领列表
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnSeach_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDeptList.Text.Trim()))
            {
                InvokeController("MessageShow", "请选择入院科室！");
                return;
            }

            if (string.IsNullOrEmpty(txtExecDeptList.Text.Trim()))
            {
                InvokeController("MessageShow", "请选择执行药房！");
                return;
            }

            InvokeController("GetCommandSheetList");
        }

        /// <summary>
        /// 选择病人过滤统领列表
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void grdPatList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int rowIndex = grdPatList.CurrentCell.RowIndex;
                DataTable dt = grdPatList.DataSource as DataTable;
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[rowIndex]["CheckFLG"]) == 1)
                    {
                        dt.Rows[rowIndex]["CheckFLG"] = 0;
                        // 从统领列表中去掉对应的数据
                        DataTable commandDt = grdCommandList.DataSource as DataTable;
                        commandDt.TableName = "CommandList";
                        DataView view = new DataView(commandDt);
                        string sqlWhere = string.Format("PatListId <> {0}", Convert.ToInt32(dt.Rows[rowIndex]["PatListId"]));
                        view.RowFilter = sqlWhere;
                        view.Sort = "PresDate ASC";
                        grdCommandList.DataSource = view.ToTable();

                        // 存在一条不选中的数据，则将全选按钮去掉勾选
                        isAllCheck = true;
                        cbPatList.Checked = false;
                        isAllCheck = false;
                    }
                    else
                    {
                        dt.Rows[rowIndex]["CheckFLG"] = 1;
                        // 将对应病人的数据追加到统领列表中
                        mCommandList.TableName = "CommandList";
                        DataView view = new DataView(mCommandList);
                        view.RowFilter = string.Format("PatListId = {0}", Convert.ToInt32(dt.Rows[rowIndex]["PatListId"]));
                        view.Sort = "PresDate ASC";
                        DataTable commandDt = grdCommandList.DataSource as DataTable;
                        commandDt.Merge(view.ToTable());

                        // 判断是否勾选了所有数据
                        DataRow[] patDr = dt.Select("CheckFLG=0");
                        if (patDr.Length <= 0)
                        {
                            isAllCheck = true;
                            cbPatList.Checked = true;
                            isAllCheck = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 统领数据选择事件
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void grdCommandList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int rowIndex = grdCommandList.CurrentCell.RowIndex;
                DataTable dt = grdCommandList.DataSource as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    // 去掉选中
                    if (Convert.ToInt32(dt.Rows[rowIndex]["CheckFLG"]) == 1)
                    {
                        dt.Rows[rowIndex]["CheckFLG"] = 0;

                        // 去掉全选按钮的勾选
                        isAllCheck = true;
                        chkDurgAll.Checked = false;
                        isAllCheck = false;
                    }
                    else
                    {
                        // 选中数据
                        dt.Rows[rowIndex]["CheckFLG"] = 1;
                        // 如果所有数据都已勾选则默认勾选全选
                        DataRow[] docDt = dt.Select("CheckFlg=0");
                        if (docDt.Length <= 0)
                        {
                            isAllCheck = true;
                            chkDurgAll.Checked = true;
                            isAllCheck = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 发送统领单
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnSendCommandList_Click(object sender, EventArgs e)
        {
            DataTable dt = grdCommandList.DataSource as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.TableName = "CommandList";
                DataView view = new DataView(dt);
                // 过滤所有选中的数据
                view.RowFilter = "CheckFlg = 1";
                DataTable temp = view.ToTable();
                if (temp != null && temp.Rows.Count > 0)
                {
                    InvokeController("SendCommandList", temp);
                }
            }
        }

        /// <summary>
        /// 查询统领单列表
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnOrderSelect_Click(object sender, EventArgs e)
        {
            // 执行科室
            if (string.IsNullOrEmpty(txtDept.Text.Trim()))
            {
                InvokeController("MessageShow", "请选择科室！");
                return;
            }

            // 统领单发送时间
            if (sdtDate.Bdate.Value == DateTime.MinValue || sdtDate.Edate.Value == DateTime.MinValue)
            {
                InvokeController("MessageShow", "请选择正确的统领日期！");
                return;
            }

            // 统领单发送时间
            if (Convert.ToDateTime(sdtDate.Bdate.Value.ToString("yyyy-MM-dd")) >
                Convert.ToDateTime(sdtDate.Edate.Value.ToString("yyyy-MM-dd")))
            {
                InvokeController("MessageShow", "统领开始日期不能大于结束日期！");
                return;
            }

            InvokeController("GetDrugbillOrderList");
        }

        /// <summary>
        /// 统领单选中事件
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void grdOrderrList_CurrentCellChanged(object sender, EventArgs e)
        {
            // 选中统领单显示明细和汇总数据
            if (grdOrderrList.CurrentCell != null)
            {
                InvokeController("GetDrugBillData");
            }
        }

        /// <summary>
        /// 重新发送统领单
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnAgainSend_Click(object sender, EventArgs e)
        {
            // 重新发送选中的统领单
            if (grdOrderrList.CurrentCell != null)
            {
                int rowIndex = grdOrderrList.CurrentCell.RowIndex;
                DataTable dt = grdOrderrList.DataSource as DataTable;
                string orderName = dt.Rows[rowIndex]["BillTypeName"].ToString();
                InvokeController("AgainSendOrder", orderName);
            }
        }

        /// <summary>
        /// 取消发送统领单
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnCancelSend_Click(object sender, EventArgs e)
        {
            // 取消发送选中的统领单
            if (grdOrderrList.CurrentCell != null)
            {
                int rowIndex = grdOrderrList.CurrentCell.RowIndex;
                DataTable dt = grdOrderrList.DataSource as DataTable;
                string orderName = dt.Rows[rowIndex]["BillTypeName"].ToString();
                InvokeController("CancelSendOrder", orderName);
            }
        }

        /// <summary>
        /// Tab切换
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void tabControl1_SelectedTabChanged(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
        {
            if (tabControl1.SelectedTabIndex == 0)
            {
                txtDeptList.Focus();
            }
            else
            {
                txtDept.Focus();
            }
        }

        /// <summary>
        /// 切换单据状态时控制按钮显示
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void cboOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOrderStatus.SelectedIndex == 1)
            {
                btnCancelSend.Enabled = false;
                btnAgainSend.Enabled = false;
            }
            else
            {
                btnCancelSend.Enabled = true;
                btnAgainSend.Enabled = true;
            }
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender">btnExit</param>
        /// <param name="e">事件参数</param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }
        #endregion

        #region "私有方法"
        /// <summary>
        /// 绑定执行科室列表
        /// </summary>
        /// <param name="execDeptDt">执行科室列表</param>
        public void Bind_ExecDeptList(DataTable execDeptDt)
        {
            txtExecDeptList.MemberField = "DeptId";
            txtExecDeptList.DisplayField = "Name";
            txtExecDeptList.CardColumn = "Name|名称|auto";
            txtExecDeptList.QueryFieldsString = "Name,Pym,Wbm";
            txtExecDeptList.ShowCardWidth = 100;
            txtExecDeptList.ShowCardDataSource = execDeptDt;
        }

        /// <summary>
        /// 绑定入院科室列表
        /// </summary>
        /// <param name="deptList">入院科室列表</param>
        public void Bind_DeptList(DataTable deptList)
        {
            // 入院科室
            txtDeptList.MemberField = "DeptId";
            txtDeptList.DisplayField = "Name";
            txtDeptList.CardColumn = "Name|名称|auto";
            txtDeptList.QueryFieldsString = "Name,Pym,Wbm";
            txtDeptList.ShowCardWidth = 100;
            txtDeptList.ShowCardDataSource = deptList;
            // 入院科室
            txtDept.MemberField = "DeptId";
            txtDept.DisplayField = "Name";
            txtDept.CardColumn = "Name|名称|auto";
            txtDept.QueryFieldsString = "Name,Pym,Wbm";
            txtDept.ShowCardWidth = 100;
            txtDept.ShowCardDataSource = deptList;
        }

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="patDt">病人列表</param>
        public void Bind_HasBeenSentDrugbillPatList(DataTable patDt)
        {
            txtPatList.MemberField = "PatListID";
            txtPatList.DisplayField = "PatName";
            txtPatList.CardColumn = "SerialNumber|住院号|120,PatName|姓名|100,BedNo|床号|60,DeptName|科室|auto";
            txtPatList.QueryFieldsString = "SerialNumber,PatName,PYCode,WBCode";
            txtPatList.ShowCardWidth = 450;
            txtPatList.ShowCardDataSource = patDt;
        }

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="patList">病人列表</param>
        public void Bind_PatList(DataTable patList)
        {
            grdPatList.DataSource = patList;
        }

        /// <summary>
        /// 绑定药品统领列表
        /// </summary>
        /// <param name="commandList">药品统领列表</param>
        public void Bind_CommandList(DataTable commandList)
        {
            grdCommandList.DataSource = commandList;
            mCommandList = commandList;
        }

        /// <summary>
        /// 绑定统领单列表
        /// </summary>
        /// <param name="orderDt">统领单列表</param>
        public void Bind_OrderList(DataTable orderDt)
        {
            grdOrderrList.DataSource = orderDt;
            if (orderDt != null && orderDt.Rows.Count > 0)
            {
                setGridSelectIndex(grdOrderrList);
            }
        }

        /// <summary>
        /// 绑定统领汇总数据
        /// </summary>
        /// <param name="summaryDt">统领汇总数据列表</param>
        public void Bind_DurgBillSummaryList(DataTable summaryDt)
        {
            grdDurgBillSummary.DataSource = summaryDt;
            if (summaryDt != null && summaryDt.Rows.Count > 0)
            {
                setGridSelectIndex(grdDurgBillSummary);
            }
        }

        /// <summary>
        /// 绑定统领明细数据
        /// </summary>
        /// <param name="detailDt">统领明细列表</param>
        public void Bind_DurgBillDetailList(DataTable detailDt)
        {
            grdDurgBillDetail.DataSource = detailDt;
            if (detailDt != null && detailDt.Rows.Count > 0)
            {
                setGridSelectIndex(grdDurgBillDetail);
            }
        }
        #endregion

        #region "CheckBox事件"

        /// <summary>
        /// 勾选全部
        /// </summary>
        /// <param name="sender">chkWhole</param>
        /// <param name="e">事件参数</param>
        private void chkWhole_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWhole.Checked)
            {
                chkChineseHerbalMedicine.Checked = false;
                chkInjection.Checked = false;
                chkLargeInfusion.Checked = false;
                chkOralMedicine.Checked = false;
            }
        }

        /// <summary>
        /// 勾选口服药
        /// </summary>
        /// <param name="sender">chkOralMedicine</param>
        /// <param name="e">事件参数</param>
        private void chkOralMedicine_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOralMedicine.Checked)
            {
                chkWhole.Checked = false;
            }
            else
            {
                if (!chkInjection.Checked &&
                    !chkLargeInfusion.Checked &&
                    !chkChineseHerbalMedicine.Checked)
                {
                    chkWhole.Checked = true;
                }
            }
        }

        /// <summary>
        /// 勾选针剂
        /// </summary>
        /// <param name="sender">chkInjection</param>
        /// <param name="e">事件参数</param>
        private void chkInjection_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInjection.Checked)
            {
                chkWhole.Checked = false;
            }
            else
            {
                if (!chkOralMedicine.Checked &&
                    !chkLargeInfusion.Checked &&
                    !chkChineseHerbalMedicine.Checked)
                {
                    chkWhole.Checked = true;
                }
            }
        }

        /// <summary>
        /// 勾选大输液
        /// </summary>
        /// <param name="sender">chkLargeInfusion</param>
        /// <param name="e">事件参数</param>
        private void chkLargeInfusion_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLargeInfusion.Checked)
            {
                chkWhole.Checked = false;
            }
            else
            {
                if (!chkInjection.Checked &&
                    !chkOralMedicine.Checked &&
                    !chkChineseHerbalMedicine.Checked)
                {
                    chkWhole.Checked = true;
                }
            }
        }

        /// <summary>
        /// 勾选中草药
        /// </summary>
        /// <param name="sender">chkChineseHerbalMedicine</param>
        /// <param name="e">事件参数</param>
        private void chkChineseHerbalMedicine_CheckedChanged(object sender, EventArgs e)
        {
            if (chkChineseHerbalMedicine.Checked)
            {
                chkWhole.Checked = false;
            }
            else
            {
                if (!chkInjection.Checked &&
                    !chkLargeInfusion.Checked &&
                    !chkOralMedicine.Checked)
                {
                    chkWhole.Checked = true;
                }
            }
        }
        #endregion

        private bool isAllCheck = false;

        /// <summary>
        /// 全选病人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbPatList_CheckedChanged(object sender, EventArgs e)
        {
            if (!isAllCheck)
            {
                DataTable patList = grdPatList.DataSource as DataTable;
                if (patList != null && patList.Rows.Count > 0)
                {
                    for (int i = 0; i < patList.Rows.Count; i++)
                    {
                        patList.Rows[i]["CheckFLG"] = cbPatList.Checked ? 1 : 0;
                    }
                }

                if (cbPatList.Checked)
                {
                    grdCommandList.DataSource = mCommandList;
                }
                else
                {
                    DataTable commandList = mCommandList.Clone();
                    grdCommandList.DataSource = commandList;
                }
            }
        }

        /// <summary>
        /// 全选药品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkDurgAll_CheckedChanged(object sender, EventArgs e)
        {
            if (!isAllCheck)
            {
                DataTable drugList = grdCommandList.DataSource as DataTable;
                if (drugList != null && drugList.Rows.Count > 0)
                {
                    for (int i = 0; i < drugList.Rows.Count; i++)
                    {
                        drugList.Rows[i]["CheckFLG"] = chkDurgAll.Checked ? 1 : 0;
                    }
                }
            }
        }
    }
}
