using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using EfwControls.Common;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPNurse.Winform.IView;

namespace HIS_IPNurse.Winform.ViewForm
{
    /// <summary>
    /// 医嘱费用核对
    /// </summary>
    public partial class FrmDocOrderExpenseCheck : BaseFormBusiness, IDocOrderExpenseCheck
    {
        #region "属性"

        /// <summary>
        /// 定义出院标志
        /// </summary>
        private int isLeaveHosOrder = 0;

        /// <summary>
        /// 是否双击了病人列表
        /// </summary>
        private bool isCellDoubleClick = false;

        /// <summary>
        /// 设置长期账单网格状态
        /// </summary>
        public bool SetOrderDetailstGridState
        {
            set
            {
                bool b = value;
                if (b == false)
                {
                    grdOrderDetails.ReadOnly = false;
                    grdOrderDetails.Columns[0].ReadOnly = true;
                    grdOrderDetails.Columns[1].ReadOnly = false;
                    grdOrderDetails.Columns[2].ReadOnly = true;
                    grdOrderDetails.Columns[3].ReadOnly = true;
                    grdOrderDetails.Columns[4].ReadOnly = true;
                    grdOrderDetails.Columns[5].ReadOnly = true;
                    grdOrderDetails.Columns[6].ReadOnly = false;
                    grdOrderDetails.Columns[7].ReadOnly = true;
                    grdOrderDetails.Columns[8].ReadOnly = true;
                    grdOrderDetails.Columns[9].ReadOnly = true;
                    grdOrderDetails.Columns[10].ReadOnly = true;
                }
                else
                {
                    grdOrderDetails.ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// 设置临时账单网格状态
        /// </summary>
        public bool SetTempDetailstGridState
        {
            set
            {
                bool b = value;
                if (b == false)
                {
                    grdTempDetails.ReadOnly = false;
                    grdTempDetails.Columns[0].ReadOnly = true;
                    grdTempDetails.Columns[1].ReadOnly = false;
                    grdTempDetails.Columns[2].ReadOnly = true;
                    grdTempDetails.Columns[3].ReadOnly = true;
                    grdTempDetails.Columns[4].ReadOnly = true;
                    grdTempDetails.Columns[5].ReadOnly = true;
                    grdTempDetails.Columns[6].ReadOnly = false;
                    grdTempDetails.Columns[7].ReadOnly = true;
                    grdTempDetails.Columns[8].ReadOnly = true;
                    grdTempDetails.Columns[9].ReadOnly = true;
                    grdTempDetails.Columns[10].ReadOnly = false;
                }
                else
                {
                    grdTempDetails.ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// 长期医嘱列表
        /// </summary>
        private DataTable mLongOrderList = new DataTable();

        /// <summary>
        /// 账单记账列表
        /// </summary>
        public DataTable LongOrderList
        {
            get
            {
                return mLongOrderList;
            }

            set
            {
                mLongOrderList = value;
            }
        }

        /// <summary>
        /// 科室ID
        /// </summary>
        public int DeptId
        {
            get
            {
                if (txtDeptList.MemberValue != null)
                {
                    return Convert.ToInt32(txtDeptList.MemberValue.ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 病人状态
        /// </summary>
        public int PatStatus
        {
            get
            {
                // 在院
                if (rdoBeInBed.Checked)
                {
                    return 2;
                }
                else if (rdoDefinedDischarge.Checked)
                {
                    // 出院未结算
                    return 3;
                }

                return 0;
            }
        }

        /// <summary>
        /// 病人登记ID
        /// </summary>
        private int mPatListID = 0;

        /// <summary>
        /// 病人登记ID
        /// </summary>
        public int PatListID
        {
            get
            {
                return mPatListID;
            }

            set
            {
                mPatListID = value;
            }
        }

        /// <summary>
        /// 医嘱类型
        /// </summary>
        public int OrderType
        {
            get
            {
                return tabControl1.SelectedTabIndex;
            }
        }

        /// <summary>
        /// 医嘱ID
        /// </summary>
        private int mOrderID = 0;

        /// <summary>
        /// 医嘱ID
        /// </summary>
        public int OrderID
        {
            get
            {
                return mOrderID;
            }

            set
            {
                mOrderID = value;
            }
        }

        /// <summary>
        /// 医嘱分组ID
        /// </summary>
        private int mGroupID = 0;

        /// <summary>
        /// 医嘱分组ID
        /// </summary>
        public int GroupID
        {
            get
            {
                return mGroupID;
            }

            set
            {
                mGroupID = value;
            }
        }
        #endregion

        #region "事件"

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmDocOrderExpenseCheck()
        {
            InitializeComponent();
            // 注册画组线事件
            grdLongOrderList.GroupLine = new EfwControls.CustomControl.PaintGroupLineHandle(PaintLongOrderGroupLine);
            grdTempOrderList.GroupLine = new EfwControls.CustomControl.PaintGroupLineHandle(PaintTempOrderGroupLine);
        }

        /// <summary>
        /// 注册键盘事件
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void FrmDocOrderExpenseCheck_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F6:
                    btnNew_Click(sender, e); // 新增账单
                    break;
                case Keys.F7:
                    btnDel_Click(sender, e); // 删除账单
                    break;
                case Keys.F8:
                    btnSave_Click(sender, e); // 保存账单
                    break;
                case Keys.F9:
                    btnAccounting_Click(sender, e);// 费用记账
                    break;
                case Keys.F10:
                    btnBedFeeAccount_Click(sender, e);// 床位费记账
                    break;
                case Keys.F11:
                    btnStopBill_Click(sender, e);// 停用账单
                    break;
                case Keys.F12:
                    btnRefresh_Click(sender, e);// 刷新
                    break;
            }
        }

        /// <summary>
        /// 窗体打开之前加载事件
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void FrmDocOrderExpenseCheck_OpenWindowBefore(object sender, EventArgs e)
        {
            // 加载科室列表
            InvokeController("GetDeptList");
            // 默认选择在院
            rdoBeInBed.Checked = true;
            // 设置默认选中长期医嘱
            tabControl1.SelectedTabIndex = 0;
            // 设置账单补录Tab默认选中临时账单
            tabControl2.SelectedTabIndex = 0;
            // 设置病人列表选中行
            bindGridSelectIndex(grdPatList);
            // 设置长期医嘱选中行
            bindGridSelectIndex(grdLongOrderList);
            // 设置长期医嘱费用列表选中行
            bindGridSelectIndex(grdLongOrderFee);
            // 设置长期医嘱费用汇总列表选中行
            bindGridSelectIndex(grdLongOrderSumFee);
            // 设置临时医嘱选中行
            bindGridSelectIndex(grdTempOrderList);
            // 设置临时医嘱费用列表选中行
            bindGridSelectIndex(grdTempOrderFee);
            // 设置临时医嘱费用汇总列表选中行
            bindGridSelectIndex(grdTempOrderSumFee);
            // 设置账单费用列表选中行
            bindGridSelectIndex(grdFeeList);
            // 设置账单费用汇总列表选中行
            bindGridSelectIndex(grdFeeSumList);
            // 设置长期账单选中行
            bindGridSelectIndex(grdOrderDetails);
            // 设置临时账单选中行
            bindGridSelectIndex(grdTempDetails);

            // 打开界面默认执行查询操作
            btnQueryPatList_Click(null, null);
        }

        /// <summary>
        /// 查询病人列表
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnQueryPatList_Click(object sender, EventArgs e)
        {
            // 清空当前选中的病人ID
            mPatListID = 0;
            // 查询病人列表
            InvokeController("GetPatList");
            // 清空病人基本信息
            lblPatType.Text = string.Empty;
            lblPatName.Text = string.Empty;
            // 清空病人费用信息
            lblSumDeposit.Text = string.Empty;
            // 累计记账金额
            lblSumFee.Text = string.Empty;
            // 累计床位费
            lblBedFee.Text = string.Empty;
            // 余额
            lblBalance.Text = string.Empty;
            // 今日费用
            lblDaySumFee.Text = string.Empty;

            isCellDoubleClick = true;
            grdPatList_CellDoubleClick(null, null);
            isCellDoubleClick = false;
        }

        /// <summary>
        /// Tab切换查询数据
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void tabControl1_SelectedTabChanged(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
        {
            // 没有选中病人时，不执行查询
            if (mPatListID == 0)
            {
                return;
            }

            // 长期或临时医嘱
            if (tabControl1.SelectedTabIndex == 0 || tabControl1.SelectedTabIndex == 1)
            {
                // 获取病人医嘱信息
                InvokeController("GetPatOrderList");
                // Tab切换时清空费用数据
                if (tabControl1.SelectedTabIndex == 0)
                {
                    // 清空长期医嘱费用数据
                    grdLongOrderFee.DataSource = new DataTable();
                    grdLongOrderSumFee.DataSource = new DataTable();
                }
                else if (tabControl1.SelectedTabIndex == 1)
                {
                    // 清空临时医嘱费用数据
                    grdTempOrderFee.DataSource = new DataTable();
                    grdTempOrderSumFee.DataSource = new DataTable();
                }
            }
            else if (tabControl1.SelectedTabIndex == 2)
            {
                // 账单费用冲销
                // 取得勾选的费用类型
                string orderType = GetOrderType();
                // 没有勾选任何账单类型时不执行查询
                if (!string.IsNullOrEmpty(orderType))
                {
                    // 取得勾选的账单类型费用列表
                    InvokeController("GetCostList", orderType);
                }
            }
            else if (tabControl1.SelectedTabIndex == 3)
            {
                // 查询长期或临时账单数据 2长期账单 3临时账单
                InvokeController("GetPatLongFeeItemGenerate", tabControl2.SelectedTabIndex == 0 ? 2 : 3);
            }
        }

        #endregion

        #region "长期医嘱"

        /// <summary>
        /// 选中长期医嘱获取医嘱关联费用数据
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void grdLongOrderList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdLongOrderList.CurrentCell != null)
            {
                int rowIndex = grdLongOrderList.CurrentCell.RowIndex;
                DataTable longOrder = grdLongOrderList.DataSource as DataTable;
                mOrderID = Convert.ToInt32(longOrder.Rows[rowIndex]["OrderID"]);
                mGroupID = Convert.ToInt32(longOrder.Rows[rowIndex]["GroupID"]);
                // 查询长期医嘱费用数据
                InvokeController("GetOrderFeeList");
            }
        }

        /// <summary>
        /// 长期医嘱费用数据勾选
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void grdLongOrderFee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int rowIndex = grdLongOrderFee.CurrentCell.RowIndex;
                DataTable longOrderFeeList = grdLongOrderFee.DataSource as DataTable;
                // 状态为退费状态的数据不允许选中
                if (Convert.ToInt32(longOrderFeeList.Rows[rowIndex]["RecordFlag"].ToString()) == 1 ||
                    Convert.ToInt32(longOrderFeeList.Rows[rowIndex]["RecordFlag"].ToString()) == 9)
                {
                    return;
                }

                if (int.Parse(longOrderFeeList.Rows[rowIndex]["CheckFlg"].ToString()) == 0)
                {
                    longOrderFeeList.Rows[rowIndex]["CheckFlg"] = 1;
                }
                else
                {
                    longOrderFeeList.Rows[rowIndex]["CheckFlg"] = 0;
                }
            }
        }

        /// <summary>
        /// 长期医嘱费用数据冲账
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnLongFeeSAB_Click(object sender, EventArgs e)
        {
            if (mPatListID != 0)
            {
                DataTable dt = grdLongOrderFee.DataSource as DataTable;
                // 没有费用数据
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return;
                }

                List<string> msgList = new List<string>();
                DataTable tempdt = dt.Clone();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // 当前循环数据行是否被勾选
                    if (Convert.ToInt32(dt.Rows[i]["CheckFlg"].ToString()) == 1)
                    {
                        // 勾选了已经被冲账的数据
                        if (Convert.ToInt32(dt.Rows[i]["RecordFlag"].ToString()) != 0)
                        {
                            msgList.Add("[" + dt.Rows[i]["ItemName"].ToString() + "]、");
                            continue;
                        }

                        tempdt.Rows.Add(dt.Rows[i].ItemArray);
                    }
                }

                if (msgList.Count > 0)
                {
                    string msg = string.Join("、", msgList.ToArray());
                    msg = msg.Substring(0, msg.Length - 1);
                    InvokeController("MessageShow", msg + "等项目已冲账，请不要重复操作！");
                    return;
                }

                if (tempdt != null && tempdt.Rows.Count > 0)
                {
                    InvokeController("OrderStrikeABalance", tempdt, true, -1, false);
                }
            }
        }

        /// <summary>
        /// 长期医嘱费用数据取消冲账
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnCancelLongFeeSAB_Click(object sender, EventArgs e)
        {
            if (mPatListID != 0)
            {
                DataTable dt = grdLongOrderFee.DataSource as DataTable;
                // 没有费用数据
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return;
                }

                List<string> msgList = new List<string>();
                DataTable tempdt = dt.Clone();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // 当前循环数据行是否被勾选了
                    if (Convert.ToInt32(dt.Rows[i]["CheckFlg"].ToString()) == 1)
                    {
                        // 是否勾选了不是已冲正的数据
                        if (Convert.ToInt32(dt.Rows[i]["RecordFlag"].ToString()) != 2)
                        {
                            msgList.Add("[" + dt.Rows[i]["ItemName"].ToString() + "]、");
                            continue;
                        }

                        tempdt.Rows.Add(dt.Rows[i].ItemArray);
                    }
                }

                if (msgList.Count > 0)
                {
                    string msg = string.Join("、", msgList.ToArray());
                    msg = msg.Substring(0, msg.Length - 1);
                    InvokeController("MessageShow", msg + "等项目未冲账，请重新选择！");
                    return;
                }

                if (tempdt != null && tempdt.Rows.Count > 0)
                {
                    InvokeController("CancelOrderStrikeABalance", tempdt, true, -1, false);
                }
            }
        }

        /// <summary>
        /// 长期医嘱费用全选反选
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void chkLongOrder_CheckedChanged(object sender, EventArgs e)
        {
            // 是否选中了病人
            if (mPatListID != 0)
            {
                DataTable dt = grdLongOrderFee.DataSource as DataTable;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // 状态为退费状态的数据不允许选中
                        if (Convert.ToInt32(dt.Rows[i]["RecordFlag"].ToString()) == 1 ||
                            Convert.ToInt32(dt.Rows[i]["RecordFlag"].ToString()) == 9)
                        {
                            continue;
                        }

                        dt.Rows[i]["CheckFlg"] = chkLongOrder.Checked ? 1 : 0;
                    }
                }
            }
        }

        /// <summary>
        /// 绑定病人医嘱列表
        /// </summary>
        /// <param name="orderListDt">病人医嘱列表</param>
        public void bind_PatOrderList(DataTable orderListDt)
        {
            // 同组医嘱合并部分字段
            if (orderListDt != null && orderListDt.Rows.Count > 0)
            {
                int tempGroupID = 0;
                for (int i = 0; i < orderListDt.Rows.Count; i++)
                {
                    if (Convert.ToDateTime(orderListDt.Rows[i]["EOrderDate"]).ToString("yyyy-MM-dd").Contains("1900-01-01"))
                    {
                        orderListDt.Rows[i]["EOrderDate"] = DBNull.Value;
                    }

                    if (tabControl1.SelectedTabIndex == 1)
                    {
                        if (Tools.ToInt32(orderListDt.Rows[i]["StatID"]) != 102)
                        {
                            orderListDt.Rows[i]["DoseNum"] = 0;
                        }
                    }

                    if (i == 0)
                    {
                        tempGroupID = Convert.ToInt32(orderListDt.Rows[i]["GroupID"]);
                        continue;
                    }

                    if (Convert.ToInt32(orderListDt.Rows[i]["GroupID"]) == tempGroupID)
                    {
                        orderListDt.Rows[i]["OrderBdate"] = DBNull.Value;
                        orderListDt.Rows[i]["DoctorName"] = DBNull.Value;
                        orderListDt.Rows[i]["ChannelName"] = DBNull.Value;
                        orderListDt.Rows[i]["EOrderDate"] = DBNull.Value;
                    }

                    tempGroupID = Convert.ToInt32(orderListDt.Rows[i]["GroupID"]);
                }
            }

            if (tabControl1.SelectedTabIndex == 0)
            {
                grdLongOrderList.DataSource = orderListDt;
                setGridSelectIndex(grdLongOrderList);
            }
            else if (tabControl1.SelectedTabIndex == 1)
            {
                grdTempOrderList.DataSource = orderListDt;
                setGridSelectIndex(grdTempOrderList);
            }
        }

        /// <summary>
        /// 绑定医嘱费用数据列表
        /// </summary>
        /// <param name="orderFeeList">医嘱明细费用数据列表</param>
        /// <param name="orderSumFeeList">医嘱汇总费用数据列表</param>
        public void bind_OrderFeeList(DataTable orderFeeList, DataTable orderSumFeeList)
        {
            //  长期医嘱费用数据
            if (tabControl1.SelectedTabIndex == 0)
            {
                grdLongOrderFee.DataSource = orderFeeList;
                grdLongOrderSumFee.DataSource = orderSumFeeList;
                setGridSelectIndex(grdLongOrderFee);
                setGridSelectIndex(grdLongOrderSumFee);
                SetLongGridColor();
            }
            else if (tabControl1.SelectedTabIndex == 1)
            {
                // 临时医嘱费用数据
                grdTempOrderFee.DataSource = orderFeeList;
                grdTempOrderSumFee.DataSource = orderSumFeeList;
                setGridSelectIndex(grdTempOrderFee);
                setGridSelectIndex(grdTempOrderSumFee);
                SetTempGridColor();
            }
        }

        /// <summary>
        /// 设置长期医嘱费用网格颜色
        /// </summary>
        public void SetLongGridColor()
        {
            if (tabControl1.SelectedTabIndex == 0)
            {
                // 长期账单
                DataTable feeItemDt = grdLongOrderFee.DataSource as DataTable;
                for (int i = 0; i < feeItemDt.Rows.Count; i++)
                {
                    Color foreColor = Color.Blue;
                    int recordFlag = Convert.ToInt32(feeItemDt.Rows[i]["RecordFlag"]);
                    switch (recordFlag)
                    {
                        case 1:
                        case 9:
                            foreColor = Color.Red;
                            break;
                        case 2:
                            foreColor = Color.Green;
                            break;
                    }

                    grdLongOrderFee.SetRowColor(i, foreColor, true);
                }
            }
        }

        #endregion

        #region "临时医嘱"

        /// <summary>
        /// 选中临时医嘱获取医嘱关联费用数据
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void grdTempOrderList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdTempOrderList.CurrentCell != null)
            {
                int rowIndex = grdTempOrderList.CurrentCell.RowIndex;
                DataTable tempOrder = grdTempOrderList.DataSource as DataTable;
                mOrderID = Convert.ToInt32(tempOrder.Rows[rowIndex]["OrderID"]);
                mGroupID = Convert.ToInt32(tempOrder.Rows[rowIndex]["GroupID"]);
                InvokeController("GetOrderFeeList");
            }
        }

        /// <summary>
        /// 临时医嘱费用数据勾选
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void grdTempOrderFee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int rowIndex = grdTempOrderFee.CurrentCell.RowIndex;
                DataTable tempOrderFeeList = grdTempOrderFee.DataSource as DataTable;
                // 状态为退费状态的数据不允许选中
                if (Convert.ToInt32(tempOrderFeeList.Rows[rowIndex]["RecordFlag"].ToString()) == 1 ||
                    Convert.ToInt32(tempOrderFeeList.Rows[rowIndex]["RecordFlag"].ToString()) == 9)
                {
                    return;
                }
                // 如果选中的是检查检验治疗申请医嘱，选中一个默认就全选所有数据
                DataTable orderDt = (DataTable)grdTempOrderList.DataSource;
                int orderIndex = grdTempOrderList.CurrentCell.RowIndex;
                int itemClass = Tools.ToInt32(orderDt.Rows[orderIndex]["ItemType"]);
                // 如果选中的是检查检验治疗申请医嘱，选中一个默认就全选所有数据
                if (itemClass == 4)
                {
                    for (int i = 0; i < tempOrderFeeList.Rows.Count; i++)
                    {
                        // 状态为退费状态的数据不允许选中
                        if (Convert.ToInt32(tempOrderFeeList.Rows[i]["RecordFlag"].ToString()) == 1 ||
                            Convert.ToInt32(tempOrderFeeList.Rows[i]["RecordFlag"].ToString()) == 9)
                        {
                            continue;
                        }

                        tempOrderFeeList.Rows[i]["CheckFlg"] = Tools.ToInt32(tempOrderFeeList.Rows[i]["CheckFlg"]) == 0 ? 1 : 0;
                    }

                    return;
                }

                if (int.Parse(tempOrderFeeList.Rows[rowIndex]["CheckFlg"].ToString()) == 0)
                {
                    tempOrderFeeList.Rows[rowIndex]["CheckFlg"] = 1;
                }
                else
                {
                    tempOrderFeeList.Rows[rowIndex]["CheckFlg"] = 0;
                }
            }
        }

        /// <summary>
        /// 临时医嘱费用数据冲账
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnTempFeeSAB_Click(object sender, EventArgs e)
        {
            if (mPatListID != 0)
            {
                DataTable dt = grdTempOrderFee.DataSource as DataTable;
                // 没有费用数据
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return;
                }

                List<string> msgList = new List<string>();
                DataTable tempdt = dt.Clone();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // 当前循环数据行是否被勾选
                    if (Convert.ToInt32(dt.Rows[i]["CheckFlg"].ToString()) == 1)
                    {
                        // 勾选了已经被冲账的数据
                        if (Convert.ToInt32(dt.Rows[i]["RecordFlag"].ToString()) != 0)
                        {
                            msgList.Add("[" + dt.Rows[i]["ItemName"].ToString() + "]、");
                            continue;
                        }

                        tempdt.Rows.Add(dt.Rows[i].ItemArray);
                    }
                }

                if (msgList.Count > 0)
                {
                    string msg = string.Join("、", msgList.ToArray());
                    msg = msg.Substring(0, msg.Length - 1);
                    InvokeController("MessageShow", msg + "等项目已冲账，请不要重复操作！");
                    return;
                }

                if (tempdt != null && tempdt.Rows.Count > 0)
                {
                    // 如果选中的是检查检验治疗申请医嘱，选中一个默认就全选所有数据
                    DataTable orderDt = (DataTable)grdTempOrderList.DataSource;
                    int orderIndex = grdTempOrderList.CurrentCell.RowIndex;
                    int itemClass = Tools.ToInt32(orderDt.Rows[orderIndex]["ItemType"]);
                    InvokeController(
                        "OrderStrikeABalance",
                        tempdt,
                        true,
                        Tools.ToInt32(orderDt.Rows[orderIndex]["OrderID"]),
                        itemClass == 4 ? true : false);
                }
            }
        }

        /// <summary>
        /// 临时医嘱费用数据取消冲账
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnCancelTempFeeSAB_Click(object sender, EventArgs e)
        {
            if (mPatListID != 0)
            {
                DataTable dt = grdTempOrderFee.DataSource as DataTable;
                // 没有费用数据
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return;
                }

                List<string> msgList = new List<string>();
                DataTable tempdt = dt.Clone();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // 当前循环数据行是否被勾选了
                    if (Convert.ToInt32(dt.Rows[i]["CheckFlg"].ToString()) == 1)
                    {
                        // 是否勾选了不是已冲正的数据
                        if (Convert.ToInt32(dt.Rows[i]["RecordFlag"].ToString()) != 2)
                        {
                            msgList.Add("[" + dt.Rows[i]["ItemName"].ToString() + "]、");
                            continue;
                        }

                        tempdt.Rows.Add(dt.Rows[i].ItemArray);
                    }
                }

                if (msgList.Count > 0)
                {
                    string msg = string.Join("、", msgList.ToArray());
                    msg = msg.Substring(0, msg.Length - 1);
                    InvokeController("MessageShow", msg + "等项目未冲账，请重新选择！");
                    return;
                }

                if (tempdt != null && tempdt.Rows.Count > 0)
                {
                    // 如果选中的是检查检验治疗申请医嘱，选中一个默认就全选所有数据
                    DataTable orderDt = (DataTable)grdTempOrderList.DataSource;
                    int orderIndex = grdTempOrderList.CurrentCell.RowIndex;
                    int itemClass = Tools.ToInt32(orderDt.Rows[orderIndex]["ItemType"]);
                    InvokeController(
                        "CancelOrderStrikeABalance",
                        tempdt,
                        true,
                        Tools.ToInt32(orderDt.Rows[orderIndex]["OrderID"]),
                        itemClass == 4 ? true : false);
                }
            }
        }

        /// <summary>
        /// 临时医嘱费用全选反选
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void chkTempOrder_CheckedChanged(object sender, EventArgs e)
        {
            // 是否选中了病人
            if (mPatListID != 0)
            {
                DataTable dt = grdTempOrderFee.DataSource as DataTable;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // 状态为退费状态的数据不允许选中
                        if (Convert.ToInt32(dt.Rows[i]["RecordFlag"].ToString()) == 1 ||
                            Convert.ToInt32(dt.Rows[i]["RecordFlag"].ToString()) == 9)
                        {
                            continue;
                        }

                        dt.Rows[i]["CheckFlg"] = chkTempOrder.Checked ? 1 : 0;
                    }
                }
            }
        }

        /// <summary>
        /// 设置临时医嘱费用网格颜色
        /// </summary>
        public void SetTempGridColor()
        {
            if (tabControl1.SelectedTabIndex == 1)
            {
                // 长期账单
                DataTable feeItemDt = grdTempOrderFee.DataSource as DataTable;
                for (int i = 0; i < feeItemDt.Rows.Count; i++)
                {
                    Color foreColor = Color.Blue;
                    int recordFlag = Convert.ToInt32(feeItemDt.Rows[i]["RecordFlag"]);
                    switch (recordFlag)
                    {
                        case 1:
                        case 9:
                            foreColor = Color.Red;
                            break;
                        case 2:
                            foreColor = Color.Green;
                            break;
                    }

                    grdTempOrderFee.SetRowColor(i, foreColor, true);
                }
            }
        }

        #endregion

        #region "账单费用"

        /// <summary>
        /// 长期账单勾选
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void chkLongBill_CheckedChanged(object sender, EventArgs e)
        {
            string orderType = GetOrderType();
            // 没有勾选任何账单类型时不执行查询
            if (!string.IsNullOrEmpty(orderType))
            {
                InvokeController("GetCostList", orderType);
            }
        }

        /// <summary>
        /// 临时账单勾选
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void chkTempBill_CheckedChanged(object sender, EventArgs e)
        {
            string orderType = GetOrderType();
            // 没有勾选任何账单类型时不执行查询
            if (!string.IsNullOrEmpty(orderType))
            {
                InvokeController("GetCostList", orderType);
            }
        }

        /// <summary>
        /// 床位费勾选
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void chkBedFee_CheckedChanged(object sender, EventArgs e)
        {
            string orderType = GetOrderType();
            // 没有勾选任何账单类型时不执行查询
            if (!string.IsNullOrEmpty(orderType))
            {
                InvokeController("GetCostList", orderType);
            }
        }

        /// <summary>
        /// 账单费用一览全选反选
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void cbkBillSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            // 是否选中了病人
            if (mPatListID != 0)
            {
                DataTable dt = grdFeeList.DataSource as DataTable;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // 状态为退费状态的数据不允许选中
                        if (Convert.ToInt32(dt.Rows[i]["RecordFlag"].ToString()) == 1 ||
                            Convert.ToInt32(dt.Rows[i]["RecordFlag"].ToString()) == 9)
                        {
                            continue;
                        }

                        dt.Rows[i]["CheckFlg"] = cbkBillSelectAll.Checked ? 1 : 0;
                    }
                }
            }
        }

        /// <summary>
        /// 账单费用冲账
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnBillFeeSAB_Click(object sender, EventArgs e)
        {
            if (mPatListID != 0)
            {
                DataTable dt = grdFeeList.DataSource as DataTable;
                // 没有费用数据
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return;
                }

                List<string> msgList = new List<string>();
                DataTable tempdt = dt.Clone();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // 当前循环数据行是否被勾选
                    if (Convert.ToInt32(dt.Rows[i]["CheckFlg"].ToString()) == 1)
                    {
                        // 勾选了已经被冲账的数据
                        if (Convert.ToInt32(dt.Rows[i]["RecordFlag"].ToString()) != 0)
                        {
                            msgList.Add("[" + dt.Rows[i]["ItemName"].ToString() + "]、");
                            continue;
                        }

                        tempdt.Rows.Add(dt.Rows[i].ItemArray);
                    }
                }

                if (msgList.Count > 0)
                {
                    string msg = string.Join("、", msgList.ToArray());
                    msg = msg.Substring(0, msg.Length - 1);
                    InvokeController("MessageShow", msg + "等项目已冲账，请不要重复操作！");
                    return;
                }

                if (tempdt != null && tempdt.Rows.Count > 0)
                {
                    InvokeController("OrderStrikeABalance", tempdt, false, -1, false);
                    InvokeController("GetCostList", GetOrderType());
                }
            }
        }

        /// <summary>
        /// 账单费用取消冲账
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnCancelBillFeeSAB_Click(object sender, EventArgs e)
        {
            if (mPatListID != 0)
            {
                DataTable dt = grdFeeList.DataSource as DataTable;
                // 没有费用数据
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return;
                }

                List<string> msgList = new List<string>();
                DataTable tempdt = dt.Clone();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // 当前循环数据行是否被勾选了
                    if (Convert.ToInt32(dt.Rows[i]["CheckFlg"].ToString()) == 1)
                    {
                        // 是否勾选了不是已冲正的数据
                        if (Convert.ToInt32(dt.Rows[i]["RecordFlag"].ToString()) != 2)
                        {
                            msgList.Add("[" + dt.Rows[i]["ItemName"].ToString() + "]、");
                            continue;
                        }

                        tempdt.Rows.Add(dt.Rows[i].ItemArray);
                    }
                }

                if (msgList.Count > 0)
                {
                    string msg = string.Join("、", msgList.ToArray());
                    msg = msg.Substring(0, msg.Length - 1);
                    InvokeController("MessageShow", msg + "等项目未冲账，请重新选择！");
                    return;
                }

                if (tempdt != null && tempdt.Rows.Count > 0)
                {
                    // 取消冲账
                    InvokeController("CancelOrderStrikeABalance", tempdt, false, -1, false);
                    // 重新获取账单费用数据
                    InvokeController("GetCostList", GetOrderType());
                }
            }
        }

        /// <summary>
        /// 费用一览CheckBox点击事件
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void grdFeeList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int rowIndex = grdFeeList.CurrentCell.RowIndex;
                DataTable dt = grdFeeList.DataSource as DataTable;
                // 状态为退费状态的数据不允许选中
                if (Convert.ToInt32(dt.Rows[rowIndex]["RecordFlag"].ToString()) == 1 ||
                    Convert.ToInt32(dt.Rows[rowIndex]["RecordFlag"].ToString()) == 9)
                {
                    return;
                }

                if (int.Parse(dt.Rows[rowIndex]["CheckFlg"].ToString()) == 0)
                {
                    dt.Rows[rowIndex]["CheckFlg"] = 1;
                }
                else
                {
                    dt.Rows[rowIndex]["CheckFlg"] = 0;
                }
            }
        }

        /// <summary>
        /// 获取账单费用过滤条件
        /// </summary>
        /// <returns>账单费用过滤条件</returns>
        private string GetOrderType()
        {
            string orderType = string.Empty;
            if (chkLongBill.Checked)
            {
                orderType = "2";
            }

            if (chkTempBill.Checked)
            {
                if (string.IsNullOrEmpty(orderType))
                {
                    orderType = "3";
                }
                else
                {
                    orderType += ",3";
                }
            }

            if (chkBedFee.Checked)
            {
                if (string.IsNullOrEmpty(orderType))
                {
                    orderType = "4";
                }
                else
                {
                    orderType += ",4";
                }
            }

            return orderType;
        }

        /// <summary>
        /// 绑定账单费用明细数据
        /// </summary>
        /// <param name="feeDt">账单费用明细数据</param>
        /// <param name="sumFeeDt">账单费用汇总数据</param>
        public void bind_CostList(DataTable feeDt, DataTable sumFeeDt)
        {
            grdFeeList.DataSource = feeDt;
            setGridSelectIndex(grdFeeList);
            // 设置费用网格显示颜色
            SetFeeGridColor();
            grdFeeSumList.DataSource = sumFeeDt;
            setGridSelectIndex(grdFeeSumList);
        }

        /// <summary>
        /// 设置账单费用网格颜色
        /// </summary>
        public void SetFeeGridColor()
        {
            if (tabControl1.SelectedTabIndex == 2)
            {
                // 长期账单
                DataTable feeItemDt = grdFeeList.DataSource as DataTable;
                for (int i = 0; i < feeItemDt.Rows.Count; i++)
                {
                    Color foreColor = Color.Blue;
                    int recordFlag = Convert.ToInt32(feeItemDt.Rows[i]["RecordFlag"]);
                    switch (recordFlag)
                    {
                        case 1:
                        case 9:
                            foreColor = Color.Red;
                            break;
                        case 2:
                            foreColor = Color.Green;
                            break;
                    }

                    grdFeeList.SetRowColor(i, foreColor, true);
                }
            }
        }

        #endregion

        #region "病人列表"

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="deptDt">科室列表</param>
        /// <param name="deptId">默认科室ID</param>
        public void bind_DeptList(DataTable deptDt, int deptId)
        {
            txtDeptList.MemberField = "DeptId";
            txtDeptList.DisplayField = "Name";
            txtDeptList.CardColumn = "Name|名称|auto";
            txtDeptList.QueryFieldsString = "Name,Pym,Wbm,Szm";
            txtDeptList.ShowCardWidth = 350;
            txtDeptList.ShowCardDataSource = deptDt;
            if (deptId > 0)
            {
                // 检查当前用户所属科室是否存在住院科室列表中
                DataRow[] dr = deptDt.Select(string.Format("DeptId={0}", deptId));
                if (dr.Length > 0)
                {
                    txtDeptList.MemberValue = deptId;
                }
                else
                {
                    // 如果不存在住院科室列表中，则默认选择第一条数据
                    txtDeptList.MemberValue = Convert.ToInt32(deptDt.Rows[0]["DeptId"]);
                }
            }
            else
            {
                if (deptDt != null && deptDt.Rows.Count > 0)
                {
                    txtDeptList.MemberValue = Convert.ToInt32(deptDt.Rows[0]["DeptId"]);
                }
            }
        }

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="patListDt">病人列表</param>
        public void bind_PatList(DataTable patListDt)
        {
            grdPatList.DataSource = patListDt;
            setGridSelectIndex(grdPatList);
        }

        /// <summary>
        /// 双击选中病人
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void grdPatList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdPatList.CurrentCell != null)
            {
                if (!isCellDoubleClick)
                {
                    int rowIndex = grdPatList.CurrentCell.RowIndex;
                    DataTable patDt = grdPatList.DataSource as DataTable;
                    mPatListID = Convert.ToInt32(patDt.Rows[rowIndex]["PatListID"]);
                    // 根据病人ID获取病人状态

                    //if (rdoBeInBed.Checked)
                    //{
                    //    isLeaveHosOrder = 0;
                    //}
                    //else if (rdoDefinedDischarge.Checked)
                    //{
                    //    isLeaveHosOrder = 1;
                    //}
                    //isLeaveHosOrder = Convert.ToInt32(patDt.Rows[rowIndex]["IsLeaveHosOrder"]);
                    isLeaveHosOrder = 0;
                    DataTable patDtStatus = (DataTable)InvokeController("GetPatientStatus", mPatListID);
                    if (patDtStatus != null && patDtStatus.Rows.Count > 0)
                    {
                        int status = Tools.ToInt32(patDtStatus.Rows[0]["Status"]);
                        if (status != 2)
                        {
                            isLeaveHosOrder = 1;
                        }
                    }
                    lblPatType.Text = patDt.Rows[rowIndex]["PatTypeName"].ToString();
                    lblPatName.Text = patDt.Rows[rowIndex]["PatName"].ToString();
                }

                // 获取病人费用信息
                InvokeController("GetPatFeeInfo");
                if (tabControl1.SelectedTabIndex == 0 || tabControl1.SelectedTabIndex == 1)
                {
                    // 获取病人医嘱信息
                    InvokeController("GetPatOrderList");
                    if (tabControl1.SelectedTabIndex == 0)
                    {
                        // 清空长期医嘱费用数据
                        grdLongOrderFee.DataSource = new DataTable();
                        grdLongOrderSumFee.DataSource = new DataTable();
                    }
                    else if (tabControl1.SelectedTabIndex == 1)
                    {
                        // 清空临时医嘱费用数据
                        grdTempOrderFee.DataSource = new DataTable();
                        grdTempOrderSumFee.DataSource = new DataTable();
                    }
                }
                else if (tabControl1.SelectedTabIndex == 2)
                {
                    // 查询账单费用一览
                    // 账单费用冲销
                    // 取得勾选的费用类型
                    string orderType = GetOrderType();
                    // 没有勾选任何账单类型时不执行查询
                    if (!string.IsNullOrEmpty(orderType))
                    {
                        // 根据勾选的费用类型查询费用列表
                        InvokeController("GetCostList", orderType);
                    }
                }
                else if (tabControl1.SelectedTabIndex == 3)
                {
                    // 检查是否存在未保存的账单
                    if (DtNotSaveCheck())
                    {
                        // 根据选中的类型查询对应的账单数据 2长期账单，3临时账单
                        InvokeController("GetPatLongFeeItemGenerate", tabControl2.SelectedTabIndex == 0 ? 2 : 3);
                    }
                }
            }
        }

        /// <summary>
        /// 绑定病人费用信息
        /// </summary>
        /// <param name="sumDeposit">预交金总额</param>
        /// <param name="patSumFee">累计记账金额</param>
        /// <param name="patBedFee">累计床位费</param>
        /// <param name="daySumFee">今日费用</param>
        public void bind_PatFeeInfo(decimal sumDeposit, decimal patSumFee, decimal patBedFee, decimal daySumFee)
        {
            if (mPatListID == 0)
            {
                // 预交金总额
                lblSumDeposit.Text = string.Empty;
                // 累计记账金额
                lblSumFee.Text = string.Empty;
                // 累计床位费
                lblBedFee.Text = string.Empty;
                // 余额
                lblBalance.Text = string.Empty;
                // 今日费用
                lblDaySumFee.Text = string.Empty;
                return;
            }

            // 预交金总额
            lblSumDeposit.Text = string.Format("{0:N}", sumDeposit);
            // 累计记账金额
            lblSumFee.Text = string.Format("{0:N}", patSumFee);
            // 累计床位费
            lblBedFee.Text = string.Format("{0:N}", patBedFee);
            // 余额
            lblBalance.Text = string.Format("{0:N}", sumDeposit - (patSumFee));
            // 今日费用
            lblDaySumFee.Text = string.Format("{0:N}", daySumFee);
        }

        #endregion

        #region "绘制分组线"

        /// <summary>
        /// 绘制长期医嘱分组线
        /// </summary>
        /// <param name="rowIndex">行号</param>
        /// <param name="colIndex">列</param>
        /// <param name="groupFlag">分组标志</param>
        private void PaintLongOrderGroupLine(int rowIndex, out int colIndex, out int groupFlag)
        {
            // 绘制分组线的列
            colIndex = LongDocName.Index;
            DataTable docList = grdLongOrderList.DataSource as DataTable;
            groupFlag = GetGroupFlag(rowIndex, docList);
        }

        /// <summary>
        /// 绘制临时医嘱分组线
        /// </summary>
        /// <param name="rowIndex">行号</param>
        /// <param name="colIndex">列</param>
        /// <param name="groupFlag">分组标志</param>
        private void PaintTempOrderGroupLine(int rowIndex, out int colIndex, out int groupFlag)
        {
            // 绘制分组线的列
            colIndex = TempDocName.Index;
            DataTable docList = grdTempOrderList.DataSource as DataTable;
            groupFlag = GetGroupFlag(rowIndex, docList);
        }

        /// <summary>
        /// 祖先符号
        /// </summary>
        private int mLastDocGroupFlag = 0;

        /// <summary>
        /// 获取分组线符号
        /// </summary>
        /// <param name="rowIndex">行号</param>
        /// <param name="docList">数据源</param>
        /// <returns>分组线符号</returns>
        private int GetGroupFlag(int rowIndex, DataTable docList)
        {
            int groupID = Convert.ToInt32(docList.Rows[rowIndex]["GroupID"]);
            // 判断是否为第一行
            if ((rowIndex - 1) == -1)
            {
                // 如果下一行和第一行是同组
                // 判断是否存在多行数据
                if (rowIndex < docList.Rows.Count - 1)
                {
                    if (Convert.ToInt32(docList.Rows[rowIndex + 1]["GroupID"]) == groupID)
                    {
                        //groupFlag = 1;
                        mLastDocGroupFlag = 1;
                    }
                    else
                    {
                        //groupFlag = 0;
                        mLastDocGroupFlag = 0;
                    }
                }
            }
            else
            {
                // 判断是否为最后一行
                if ((rowIndex + 1) == docList.Rows.Count)
                {
                    // 如果上一行和最后一行是同组
                    if (Convert.ToInt32(docList.Rows[rowIndex - 1]["GroupID"]) == groupID)
                    {
                        mLastDocGroupFlag = 3;
                    }
                    else
                    {
                        mLastDocGroupFlag = 0;
                    }
                }
                else
                {
                    // 中间的行
                    // 如果上一行绘制的是开始线或者上一行绘制的是中间竖线
                    if (mLastDocGroupFlag == 1 || mLastDocGroupFlag == 2)
                    {
                        // 判断下一行是否还是同组
                        if (Convert.ToInt32(docList.Rows[rowIndex + 1]["GroupID"]) == groupID)
                        {
                            //groupFlag = 2;
                            mLastDocGroupFlag = 2;
                        }
                        else
                        {
                            //groupFlag = 3;
                            mLastDocGroupFlag = 3;
                        }
                    }
                    else if (mLastDocGroupFlag == 3 || mLastDocGroupFlag == 0)
                    {
                        // 如果上一行绘制的是结束线，或者没有绘制分组线
                        // 判断下一行是否还是同组
                        if (Convert.ToInt32(docList.Rows[rowIndex + 1]["GroupID"]) == groupID)
                        {
                            //groupFlag = 1;
                            mLastDocGroupFlag = 1;
                        }
                        else
                        {
                            //groupFlag = 0;
                            mLastDocGroupFlag = 0;
                        }
                    }
                }
            }

            return mLastDocGroupFlag;
        }

        #endregion

        #region "补录账单"

        /// <summary>
        /// 新增账单
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            if (mPatListID != 0)
            {
                // 定义出院的病人不允许补录账单
                if (isLeaveHosOrder == 1)
                {
                    InvokeController("MessageShow", "该病人已定义出院，不允许开账单！");
                    return;
                }

                // 长期账单
                if (tabControl2.SelectedTabIndex == 0)
                {
                    // 设置长期账单网格为可编辑状态
                    SetOrderDetailstGridState = false;
                    // 新增长期账单
                    grdOrderDetails.AddRow();
                }
                else if (tabControl2.SelectedTabIndex == 1)
                {
                    // 临时账单
                    // 设置临时账单网格为可编辑状态
                    SetTempDetailstGridState = false;
                    // 新增临时账单
                    grdTempDetails.AddRow();
                }
            }
            else
            {
                // 没有选择病人时，提示Msg
                InvokeController("MessageShow", "请选择需要新开账单的病人！");
            }
        }

        /// <summary>
        /// 删除账单
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (mPatListID != 0)
            {
                // 长期账单删除
                if (tabControl2.SelectedTabIndex == 0)
                {
                    string msg = string.Empty;
                    DataTable dt = grdOrderDetails.DataSource as DataTable;
                    DataTable tempDt = dt.Clone();
                    DataTable feeItemDt = dt.Clone();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // 记账标识为空时，默认为未记账
                        if (string.IsNullOrEmpty(dt.Rows[i]["StrikeABalanceFLG"].ToString()))
                        {
                            dt.Rows[i]["StrikeABalanceFLG"] = 0;
                        }

                        feeItemDt.Rows.Add(dt.Rows[i].ItemArray);
                        if (!string.IsNullOrEmpty(dt.Rows[i]["CheckFlg"].ToString()))
                        {
                            // 是否为选中的数据
                            if (int.Parse(dt.Rows[i]["CheckFlg"].ToString()) == 1)
                            {
                                // 已记账的账单不允许删除
                                if (Convert.ToInt32(dt.Rows[i]["StrikeABalanceFLG"].ToString()) == 1)
                                {
                                    msg += "[" + dt.Rows[i]["ItemName"].ToString() + "]、";
                                }

                                tempDt.Rows.Add(dt.Rows[i].ItemArray);
                            }
                        }
                    }

                    // 已记账的账单不允许删除
                    if (!string.IsNullOrEmpty(msg))
                    {
                        msg = msg.Substring(0, msg.Length - 1);
                        InvokeController("MessageShow", msg + "等项目已记账，无法执行删除操作，请确认！");
                        return;
                    }

                    if (tempDt != null && tempDt.Rows.Count > 0)
                    {
                        InvokeController("DelFeeLongOrderData", tempDt, feeItemDt);
                    }
                }
                else if (tabControl2.SelectedTabIndex == 1)
                {
                    // 临时账单删除
                    if (grdTempDetails.CurrentCell != null)
                    {
                        DataTable dt = grdTempDetails.DataSource as DataTable;
                        DataTable tempDt = dt.Clone();
                        DataTable feeItemDt = dt.Clone();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            feeItemDt.Rows.Add(dt.Rows[i].ItemArray);
                            if (!string.IsNullOrEmpty(dt.Rows[i]["CheckFlg"].ToString()))
                            {
                                if (int.Parse(dt.Rows[i]["CheckFlg"].ToString()) == 1)
                                {
                                    tempDt.Rows.Add(dt.Rows[i].ItemArray);
                                }
                            }
                        }

                        if (tempDt != null && tempDt.Rows.Count > 0)
                        {
                            InvokeController("DelFeeLongOrderData", tempDt, feeItemDt);
                        }
                    }
                }
            }
            else
            {
                // 没有选择病人时，提示Msg
                InvokeController("MessageShow", "请选择需要删除账单的病人！");
            }
        }

        /// <summary>
        /// 保存账单
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (mPatListID != 0)
            {
                // 保存长期账单
                if (tabControl2.SelectedTabIndex == 0)
                {
                    grdOrderDetails.EndEdit();
                    DataTable dt = grdOrderDetails.DataSource as DataTable;
                    InvokeController("SaveLongOrderData", dt, 2);
                }
                else if (tabControl2.SelectedTabIndex == 1)
                {
                    // 保存临时账单
                    grdTempDetails.EndEdit();
                    DataTable dt = grdTempDetails.DataSource as DataTable;
                    InvokeController("SaveLongOrderData", dt, 3);
                }
            }
            else
            {
                // 没有选择病人时，提示Msg
                InvokeController("MessageShow", "没有需要保存的账单！");
            }
        }

        /// <summary>
        /// 账单记账
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnAccounting_Click(object sender, EventArgs e)
        {
            if (mPatListID != 0)
            {
                if (tabControl2.SelectedTabIndex == 0)
                {
                    if (grdOrderDetails.DataSource != null && grdOrderDetails.CurrentCell != null)
                    {
                        grdOrderDetails.EndEdit();
                        int rowIndex = grdOrderDetails.CurrentCell.RowIndex;
                        DataTable dt = grdOrderDetails.DataSource as DataTable;
                        //DataTable TempDt = dt.Clone();
                        mLongOrderList = dt.Clone();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i]["CheckFlg"].ToString()))
                            {
                                if (int.Parse(dt.Rows[i]["CheckFlg"].ToString()) == 1)
                                {
                                    mLongOrderList.Rows.Add(dt.Rows[i].ItemArray);
                                }
                            }
                        }

                        if (mLongOrderList != null && mLongOrderList.Rows.Count > 0)
                        {
                            InvokeController("ShowFrmFeePresDate", false);
                        }
                    }
                }
                else if (tabControl2.SelectedTabIndex == 1)
                {
                    if (grdTempDetails.DataSource != null && grdTempDetails.CurrentCell != null)
                    {
                        grdTempDetails.EndEdit();
                        int rowIndex = grdTempDetails.CurrentCell.RowIndex;
                        DataTable dt = grdTempDetails.DataSource as DataTable;
                        //DataTable TempDt = dt.Clone();
                        mLongOrderList = dt.Clone();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i]["CheckFlg"].ToString()))
                            {
                                if (int.Parse(dt.Rows[i]["CheckFlg"].ToString()) == 1)
                                {
                                    mLongOrderList.Rows.Add(dt.Rows[i].ItemArray);
                                }
                            }
                        }

                        if (mLongOrderList != null && mLongOrderList.Rows.Count > 0)
                        {
                            InvokeController("FeeItemAccounting", 3);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 床位费记账
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnBedFeeAccount_Click(object sender, EventArgs e)
        {
            if (mPatListID != 0)
            {
                InvokeController("ShowFrmFeePresDate", true);
            }
        }

        /// <summary>
        /// 停用账单
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnStopBill_Click(object sender, EventArgs e)
        {
            if (mPatListID != 0)
            {
                if (tabControl2.SelectedTabIndex == 0)
                {
                    string msg = string.Empty;
                    // 取得网格列表
                    DataTable dt = grdOrderDetails.DataSource as DataTable;
                    DataTable feeItemDt = dt.Clone();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // 记账标识为空时，默认为未记账
                        if (string.IsNullOrEmpty(dt.Rows[i]["StrikeABalanceFLG"].ToString()))
                        {
                            dt.Rows[i]["StrikeABalanceFLG"] = 0;
                        }
                        // 判断当前数据行是否已被勾选
                        if (!string.IsNullOrEmpty(dt.Rows[i]["CheckFlg"].ToString()))
                        {
                            if (int.Parse(dt.Rows[i]["CheckFlg"].ToString()) == 1)
                            {
                                // 判断当前数据行是否已记账
                                if (Convert.ToInt32(dt.Rows[i]["StrikeABalanceFLG"].ToString()) == 0)
                                {
                                    // 未记账时需提示用户
                                    msg += "[" + dt.Rows[i]["ItemName"].ToString() + "]、";
                                }

                                feeItemDt.Rows.Add(dt.Rows[i].ItemArray);
                            }
                        }
                    }
                    // 如果勾选了未记账的账单
                    if (!string.IsNullOrEmpty(msg))
                    {
                        msg = msg.Substring(0, msg.Length - 1);
                        InvokeController("MessageShow", msg + "等项目未记账，无法执行停用操作，请确认！");
                        return;
                    }

                    if (feeItemDt != null && feeItemDt.Rows.Count > 0)
                    {
                        InvokeController("StopFeeLongOrderData", feeItemDt, 2);
                    }
                }
            }
            else
            {
                // 没有选择病人时，提示Msg
                InvokeController("MessageShow", "请选择需要停用账单的病人！");
            }
        }

        /// <summary>
        /// 刷新账单列表
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (tabControl2.SelectedTabIndex == 0)
            {
                InvokeController("GetPatLongFeeItemGenerate", 2);
            }
            else if (tabControl2.SelectedTabIndex == 1)
            {
                InvokeController("GetPatLongFeeItemGenerate", 3);
            }
        }

        /// <summary>
        /// 绑定弹出网格费用列表
        /// </summary>
        /// <param name="feeDt">费用列表</param>
        public void bind_SimpleFeeItemData(DataTable feeDt)
        {
            // 长期医嘱
            grdOrderDetails.SelectionCards[0].BindColumnIndex = ItemCode.Index;
            grdOrderDetails.SelectionCards[0].CardColumn = "ItemCode|编码|60,ItemName|项目名称|240,Standard|规格|90,UnitPrice|单价|80,StoreAmount|库存数|80,ExecDeptName|执行科室|auto";
            grdOrderDetails.SelectionCards[0].CardSize = new System.Drawing.Size(700, 180);
            grdOrderDetails.SelectionCards[0].QueryFieldsString = "ItemCode,ItemName,Pym,Wbm";
            grdOrderDetails.BindSelectionCardDataSource(0, feeDt);
            // 临时医嘱
            grdTempDetails.SelectionCards[0].BindColumnIndex = TempItemCode.Index;
            grdTempDetails.SelectionCards[0].CardColumn = "ItemCode|编码|60,ItemName|项目名称|240,Standard|规格|90,UnitPrice|单价|80,StoreAmount|库存数|80,ExecDeptName|执行科室|auto";
            grdTempDetails.SelectionCards[0].CardSize = new System.Drawing.Size(700, 180);
            grdTempDetails.SelectionCards[0].QueryFieldsString = "ItemCode,ItemName,Pym,Wbm";
            grdTempDetails.BindSelectionCardDataSource(0, feeDt);
        }

        /// <summary>
        /// 绑定病人账单列表
        /// </summary>
        /// <param name="longOrderDt">长期账单列表</param>
        public void bind_LongOrderData(DataTable longOrderDt)
        {
            if (tabControl2.SelectedTabIndex == 0)
            {
                grdOrderDetails.EndEdit();
                grdOrderDetails.DataSource = longOrderDt;
                if (grdOrderDetails.DataSource != null)
                {
                    setGridSelectIndex(grdOrderDetails);
                }

                SetOrderDetailstGridState = false;
                cbkLongOrderAll.Checked = false;
            }
            else if (tabControl2.SelectedTabIndex == 1)
            {
                grdTempDetails.EndEdit();
                grdTempDetails.DataSource = longOrderDt;
                if (grdTempDetails.DataSource != null)
                {
                    setGridSelectIndex(grdTempDetails);
                }

                SetTempDetailstGridState = false;
                cbkTempOrderAll.Checked = false;
            }

            SetOrderGridColor();
        }

        /// <summary>
        /// 设置账单网格颜色
        /// </summary>
        private void SetOrderGridColor()
        {
            if (tabControl2.SelectedTabIndex == 0)
            {
                // 长期账单
                DataTable feeItemDt = grdOrderDetails.DataSource as DataTable;
                for (int i = 0; i < feeItemDt.Rows.Count; i++)
                {
                    Color foreColor = Color.Blue;
                    if (!string.IsNullOrEmpty(feeItemDt.Rows[i]["StrikeABalanceFLG"].ToString()))
                    {
                        int flg = Convert.ToInt32(feeItemDt.Rows[i]["StrikeABalanceFLG"].ToString());

                        if (flg == 1)
                        {
                            foreColor = Color.Green;
                        }
                    }

                    grdOrderDetails.SetRowColor(i, foreColor, true);
                }
            }
            else if (tabControl2.SelectedTabIndex == 1)
            {
                // 临时账单
                DataTable feeItemDt = grdTempDetails.DataSource as DataTable;
                for (int i = 0; i < feeItemDt.Rows.Count; i++)
                {
                    Color foreColor = Color.Blue;
                    if (!string.IsNullOrEmpty(feeItemDt.Rows[i]["StrikeABalanceFLG"].ToString()))
                    {
                        int flg = Convert.ToInt32(feeItemDt.Rows[i]["StrikeABalanceFLG"].ToString());
                        if (flg == 1)
                        {
                            foreColor = Color.Green;
                        }
                    }

                    grdTempDetails.SetRowColor(i, foreColor, true);
                }
            }
        }

        /// <summary>
        /// 长期账单弹出网格选择事件
        /// </summary>
        /// <param name="selectedValue">弹出网格选中的数据</param>
        /// <param name="stop">停止标识</param>
        /// <param name="customNextColumnIndex">选中数据后光标位置</param>
        private void grdOrderDetails_SelectCardRowSelected(object selectedValue, ref bool stop, ref int customNextColumnIndex)
        {
            try
            {
                DataRow row = (DataRow)selectedValue;
                int rowid = this.grdOrderDetails.CurrentCell.RowIndex;
                DataTable dt = (DataTable)grdOrderDetails.DataSource;
                SetFeeItemDt(dt.Rows[rowid], row);
                SetOrderGridColor();
                grdOrderDetails.EndEdit();
                grdOrderDetails.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }

        /// <summary>
        /// 临时账单弹出网格选择事件
        /// </summary>
        /// <param name="selectedValue">弹出网格选中的数据</param>
        /// <param name="stop">停止标识</param>
        /// <param name="customNextColumnIndex">选中数据后光标位置</param>
        private void grdTempDetails_SelectCardRowSelected(object selectedValue, ref bool stop, ref int customNextColumnIndex)
        {
            try
            {
                DataRow row = (DataRow)selectedValue;
                int rowid = this.grdTempDetails.CurrentCell.RowIndex;
                DataTable dt = (DataTable)grdTempDetails.DataSource;
                SetFeeItemDt(dt.Rows[rowid], row);
                SetOrderGridColor();
                grdTempDetails.EndEdit();
                grdTempDetails.Refresh();
                //setGridSelectIndex(grdTempDetails, grdTempDetails.CurrentCell.RowIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }

        /// <summary>
        /// 账单录入
        /// </summary>
        /// <param name="orderdr">费用生成数据</param>
        /// <param name="tempDr">弹出网格选择的数据</param>
        private void SetFeeItemDt(DataRow orderdr, DataRow tempDr)
        {
            if (string.IsNullOrEmpty(orderdr["GenerateID"].ToString()))
            {
                orderdr["GenerateID"] = 0;
            }

            orderdr["StrikeABalanceFLG"] = 0;  // 记账ID
            orderdr["PatListID"] = mPatListID;  // 病人登记ID
            orderdr["BabyID"] = 0;   // BadyId
            orderdr["ItemID"] = tempDr["ItemID"]; // 项目ID
            orderdr["ItemCode"] = tempDr["ItemCode"]; // 项目Code
            orderdr["ItemName"] = tempDr["ItemName"]; // 项目名
            orderdr["FeeClass"] = tempDr["ItemClass"]; // 项目类型
            orderdr["StatID"] = tempDr["StatID"]; // 大项目ID
            orderdr["Spec"] = tempDr["Standard"]; // 规格
            orderdr["Unit"] = tempDr["MiniUnitName"]; // 单位
            orderdr["PackAmount"] = tempDr["MiniConvertNum"]; // 划价系数
            orderdr["InPrice"] = tempDr["InPrice"];  // 批发价
            orderdr["SellPrice"] = tempDr["SellPrice"]; // 销售价
            orderdr["StoreAmount"] = tempDr["StoreAmount"]; // 库存数
            orderdr["Amount"] = 0;  // 数量
            orderdr["TotalFee"] = 0; // 总金额
            orderdr["DoseAmount"] = 0;// 处方帖数
            int itemClass = Convert.ToInt32(tempDr["ItemClass"]);
            if (itemClass == 2)
            {
                if (txtDeptList.MemberValue != null)
                {
                    orderdr["ExecDeptDoctorID"] = Convert.ToInt32(txtDeptList.MemberValue.ToString()); // 执行科室ID
                    orderdr["ExecDeptName"] = txtDeptList.Text; // 执行科室名
                }
                else
                {
                    orderdr["ExecDeptDoctorID"] = 0; // 执行科室ID
                    orderdr["ExecDeptName"] = string.Empty; // 执行科室名
                }
            }
            else
            {
                orderdr["ExecDeptDoctorID"] = tempDr["ExecDeptId"]; // 执行科室ID
                orderdr["ExecDeptName"] = tempDr["ExecDeptName"]; // 执行科室名
            }

            orderdr["PresDate"] = DateTime.Now.ToString("yyyy-MM-dd"); // 处方日期
            orderdr["MarkDate"] = DateTime.Now; // 划价时间
            if (tabControl2.SelectedTabIndex == 0)
            {
                // 长期账单
                orderdr["OrderType"] = 2;
            }
            else if (tabControl2.SelectedTabIndex == 1)
            {
                // 临时账单
                orderdr["OrderType"] = 3;
            }

            orderdr["IsStop"] = 0;
            orderdr["CheckFLG"] = 0;
            orderdr["FeeSource"] = 3;
            orderdr["IsUpdate"] = 1;
        }

        /// <summary>
        /// 长期根据数量计算总价格
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void grdOrderDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                return;
            }

            if (grdOrderDetails.CurrentCell != null)
            {
                int rowIndex = grdOrderDetails.CurrentCell.RowIndex;
                if (grdOrderDetails.CurrentCell.DataGridView[e.ColumnIndex, rowIndex].Value is DBNull)
                {
                    return;
                }

                DataTable dt = grdOrderDetails.DataSource as DataTable;
                if (!string.IsNullOrEmpty(dt.Rows[rowIndex]["Amount"].ToString())
                    && !string.IsNullOrEmpty(dt.Rows[rowIndex]["PackAmount"].ToString()))
                {
                    if (Convert.ToInt32(dt.Rows[rowIndex]["Amount"].ToString()) > 0)
                    {
                        // 根据数量计算总价格（数量*单价/划价系数）
                        int count = int.Parse(dt.Rows[rowIndex]["Amount"].ToString());
                        decimal packAmount = decimal.Parse(dt.Rows[rowIndex]["PackAmount"].ToString());
                        decimal price = decimal.Parse(dt.Rows[rowIndex]["SellPrice"].ToString());
                        dt.Rows[rowIndex]["TotalFee"] = Math.Round(count * price / packAmount, 2);
                        dt.Rows[rowIndex]["IsUpdate"] = 1;
                        // 设置网格显示颜色
                        SetOrderGridColor();
                        // 刷新网格
                        grdOrderDetails.Refresh();
                    }
                }

                setGridSelectIndex(grdOrderDetails, grdOrderDetails.CurrentCell.RowIndex);
            }
        }

        /// <summary>
        /// 临时账单根据数量计算总金额
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void grdTempDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                return;
            }

            if (grdTempDetails.CurrentCell != null)
            {
                int rowIndex = grdTempDetails.CurrentCell.RowIndex;
                if (grdTempDetails.CurrentCell.DataGridView[e.ColumnIndex, rowIndex].Value is DBNull)
                {
                    return;
                }

                DataTable dt = grdTempDetails.DataSource as DataTable;
                if (!string.IsNullOrEmpty(dt.Rows[rowIndex]["Amount"].ToString())
                    && !string.IsNullOrEmpty(dt.Rows[rowIndex]["PackAmount"].ToString()))
                {
                    if (Convert.ToInt32(dt.Rows[rowIndex]["Amount"].ToString()) > 0)
                    {
                        // 根据数量计算总价格（数量*单价）
                        int count = int.Parse(dt.Rows[rowIndex]["Amount"].ToString());
                        decimal packAmount = decimal.Parse(dt.Rows[rowIndex]["PackAmount"].ToString());
                        decimal price = decimal.Parse(dt.Rows[rowIndex]["SellPrice"].ToString());
                        dt.Rows[rowIndex]["TotalFee"] = Math.Round(count * price / packAmount, 2);
                        dt.Rows[rowIndex]["IsUpdate"] = 1;
                        // 设置网格显示颜色
                        SetOrderGridColor();
                        // 刷新网格
                        grdTempDetails.Refresh();
                    }
                }

                setGridSelectIndex(grdTempDetails, grdTempDetails.CurrentCell.RowIndex);
            }
        }

        /// <summary>
        /// 长期账单-网格CheckBox选中事件
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void grdOrderDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int rowIndex = grdOrderDetails.CurrentCell.RowIndex;
                DataTable dt = grdOrderDetails.DataSource as DataTable;
                // 选中标识为空时，默认为未选中
                if (string.IsNullOrEmpty(dt.Rows[rowIndex]["CheckFLG"].ToString()))
                {
                    dt.Rows[rowIndex]["CheckFLG"] = 0;
                }

                if (int.Parse(dt.Rows[rowIndex]["CheckFLG"].ToString()) == 0)
                {
                    dt.Rows[rowIndex]["CheckFLG"] = 1;
                }
                else
                {
                    dt.Rows[rowIndex]["CheckFLG"] = 0;
                }

                SetOrderGridColor();
                setGridSelectIndex(grdOrderDetails, grdOrderDetails.CurrentCell.RowIndex);
            }
        }

        /// <summary>
        /// 临时账单-网格CheckBox选中事件
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void grdTempDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int rowIndex = grdTempDetails.CurrentCell.RowIndex;
                DataTable dt = grdTempDetails.DataSource as DataTable;
                // 选中标识为空时，默认为未选中
                if (string.IsNullOrEmpty(dt.Rows[rowIndex]["CheckFLG"].ToString()))
                {
                    dt.Rows[rowIndex]["CheckFLG"] = 0;
                }

                if (int.Parse(dt.Rows[rowIndex]["CheckFLG"].ToString()) == 0)
                {
                    dt.Rows[rowIndex]["CheckFLG"] = 1;
                }
                else
                {
                    dt.Rows[rowIndex]["CheckFLG"] = 0;
                }

                SetOrderGridColor();
                setGridSelectIndex(grdTempDetails, grdTempDetails.CurrentCell.RowIndex);
            }
        }

        /// <summary>
        /// 设置长期账单已记账数据不能编辑
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void grdOrderDetails_CurrentCellChanged(object sender, EventArgs e)
        {
            if (grdOrderDetails.CurrentCell != null)
            {
                int rowIndex = grdOrderDetails.CurrentCell.RowIndex;
                DataTable feeItemDt = grdOrderDetails.DataSource as DataTable;
                if (!string.IsNullOrEmpty(feeItemDt.Rows[rowIndex]["StrikeABalanceFLG"].ToString()))
                {
                    if (Convert.ToInt32(feeItemDt.Rows[rowIndex]["StrikeABalanceFLG"].ToString()) == 1)
                    {
                        SetOrderDetailstGridState = true;
                    }
                    else
                    {
                        SetOrderDetailstGridState = false;
                    }
                }

                if (!this.grdOrderDetails.CurrentCell.ReadOnly
                   && this.grdOrderDetails.CurrentCell.ColumnIndex != 1)
                {
                    this.grdOrderDetails.BeginEdit(true);
                }
            }
        }

        /// <summary>
        /// 设置临时账单已记账数据不能编辑
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void grdTempDetails_CurrentCellChanged(object sender, EventArgs e)
        {
            if (grdTempDetails.CurrentCell != null)
            {
                int rowIndex = grdTempDetails.CurrentCell.RowIndex;
                DataTable feeItemDt = grdTempDetails.DataSource as DataTable;
                if (!string.IsNullOrEmpty(feeItemDt.Rows[rowIndex]["StrikeABalanceFLG"].ToString()))
                {
                    if (Convert.ToInt32(feeItemDt.Rows[rowIndex]["StrikeABalanceFLG"].ToString()) == 1)
                    {
                        SetTempDetailstGridState = true;
                    }
                    else
                    {
                        SetTempDetailstGridState = false;
                    }
                }
            }
        }

        /// <summary>
        /// 窗体状态改变
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void FrmDocOrderExpenseCheck_VisibleChanged(object sender, EventArgs e)
        {
            grdOrderDetails.EndEdit();
            grdTempDetails.EndEdit();
        }

        /// <summary>
        /// 关闭当前界面
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnColse_Click(object sender, EventArgs e)
        {
            if (DtNotSaveCheck())
            {
                InvokeController("Close", this);
            }
        }

        /// <summary>
        /// 检查是否有未保存的账单
        /// </summary>
        /// <returns>false存在未保存的账单</returns>
        private bool DtNotSaveCheck()
        {
            // 判断上一个选中的病人是否有未保存的账单
            DataTable longDt = grdOrderDetails.DataSource as DataTable;
            DataTable tempDt = grdTempDetails.DataSource as DataTable;
            if (longDt != null && longDt.Rows.Count > 0)
            {
                DataRow[] longArrayDr = longDt.Select("IsUpdate=1");
                if (longArrayDr.Length > 0)
                {
                    if (MessageBox.Show("当前病人有未保存的长期账单，确定要切换病人吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            if (tempDt != null && tempDt.Rows.Count > 0)
            {
                DataRow[] tempArrayDr = tempDt.Select("IsUpdate=1");
                if (tempArrayDr.Length > 0)
                {
                    if (MessageBox.Show("当前病人有未保存的临时账单，确定要切换病人吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 长期账单全选反选
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void cbkLongOrderAll_CheckedChanged(object sender, EventArgs e)
        {
            // 是否选中了病人
            if (mPatListID != 0)
            {
                DataTable longOrderDt = grdOrderDetails.DataSource as DataTable;
                if (longOrderDt.Rows.Count > 0)
                {
                    for (int i = 0; i < longOrderDt.Rows.Count; i++)
                    {
                        longOrderDt.Rows[i]["CheckFLG"] = cbkLongOrderAll.Checked ? 1 : 0;
                    }
                }
            }
        }

        /// <summary>
        /// 临时账单全选反选
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void cbkTempOrderAll_CheckedChanged(object sender, EventArgs e)
        {
            // 是否选中了病人
            if (mPatListID != 0)
            {
                DataTable tempOrderDt = grdTempDetails.DataSource as DataTable;
                if (tempOrderDt.Rows.Count > 0)
                {
                    for (int i = 0; i < tempOrderDt.Rows.Count; i++)
                    {
                        tempOrderDt.Rows[i]["CheckFLG"] = cbkTempOrderAll.Checked ? 1 : 0;
                    }
                }
            }
        }

        /// <summary>
        /// 账单Tab切换
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void tabControl2_SelectedTabChanged(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
        {
            // 长期账单
            if (tabControl2.SelectedTabIndex == 0)
            {
                btnStopBill.Enabled = true;
                SetTempDetailstGridState = true;
                grdTempDetails.EndEdit();
                if (/*grdOrderDetails.DataSource == null || */mPatListID != 0)
                {
                    InvokeController("GetPatLongFeeItemGenerate", 2);
                }
            }
            else if (tabControl2.SelectedTabIndex == 1)
            {
                // 临时账单
                SetOrderDetailstGridState = true;
                grdOrderDetails.EndEdit();
                btnStopBill.Enabled = false;
                if (mPatListID != 0)
                {
                    InvokeController("GetPatLongFeeItemGenerate", 3);
                }
            }
        }
        #endregion
    }
}
