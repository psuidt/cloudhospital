using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.IPManage;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    public partial class FrmDoctorsOrderSccounting : BaseFormBusiness, IDoctorsOrderSccounting
    {
        #region "属性"

        /// <summary>
        /// 网格上一次选中行
        /// </summary>
        private int gridCellRowIndex = 0;

        /// <summary>
        /// 设置网格状态
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
        /// 设置网格状态
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
                    grdTempDetails.Columns[10].ReadOnly = true;
                }
                else
                {
                    grdTempDetails.ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// 数据未加载完成不触发病人选中事件
        /// </summary>
        private bool isGridCurrentCell = false;

        /// <summary>
        /// 数据未加载完不触发Tab选择事件
        /// </summary>
        private bool isTabSelect = false;

        /// <summary>
        /// 科室ID
        /// </summary>
        public int DeptId
        {
            get
            {
                return txtDeptList.MemberValue == null ? 0 : int.Parse(txtDeptList.MemberValue.ToString());
            }
        }

        /// <summary>
        /// 检索条件
        /// </summary>
        public string Patam
        {
            get
            {
                return txtPatParam.Text.Trim();
            }
        }

        /// <summary>
        /// 记账人代码
        /// </summary>
        private int mMarkEmpID = 0;

        /// <summary>
        /// 记账人代码
        /// </summary>
        public int MarkEmpID
        {
            get
            {
                return mMarkEmpID;
            }

            set
            {
                mMarkEmpID = value;
            }
        }

        /// <summary>
        /// 记账人姓名
        /// </summary>
        private string mMarkEmpName = string.Empty;
        
        /// <summary>
        /// 记账人姓名
        /// </summary>
        public string MarkEmpName
        {
            get
            {
                return mMarkEmpName;
            }

            set
            {
                mMarkEmpName = value;
            }
        }

        /// <summary>
        /// 长期账单记账列表
        /// </summary>
        private DataTable mLongOrderList = new DataTable();

        /// <summary>
        /// 长期账单记账列表
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
        /// 病人登记ID
        /// </summary>
        private int mPatListId = 0;

        /// <summary>
        /// 病人登记ID
        /// </summary>
        public int PatListID
        {
            get
            {
                return mPatListId;
            }

            set
            {
                mPatListId = value;
            }
        }

        /// <summary>
        /// 账单类型
        /// </summary>
        public int OrderType
        {
            get
            {
                if (cboOrderType.SelectedIndex == 0)
                {
                    return -1;
                }
                else if (cboOrderType.SelectedIndex == 1)
                {
                    return 2;
                }
                else if (cboOrderType.SelectedIndex == 2)
                {
                    return 3;
                }
                else if (cboOrderType.SelectedIndex == 3)
                {
                    return 4;
                }

                return -1;
            }
        }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ItemID
        {
            get
            {
                return txtFeeDetailList.MemberValue == null ? 0 : Convert.ToInt32(txtFeeDetailList.MemberValue);
            }
        }

        /// <summary>
        /// 费用开始时间
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return dtTime.Bdate.Value;
            }
        }

        /// <summary>
        /// 费用结束时间
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return dtTime.Edate.Value;
            }
        }
        #endregion

        #region "事件"
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmDoctorsOrderSccounting()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// 窗体打开前事件
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmDoctorsOrderSccounting_OpenWindowBefore(object sender, EventArgs e)
        {
            // 获取科室列表
            InvokeController("GetDeptList");
            InvokeController("GetSimpleFeeItemData");
            // 加载账单模板列表
            InvokeController("GetIPFeeItemTempList");
            tabControl2.SelectedTabIndex = 0;
            bindGridSelectIndex(grdPatList);
            bindGridSelectIndex(grdOrderDetails);
            bindGridSelectIndex(grdTempDetails);
            bindGridSelectIndex(grdFeeList);
            isTabSelect = true;
        }

        /// <summary>
        /// 注册功能键
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmDoctorsOrderSccounting_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F6:
                    btnNew_Click(sender, e);
                    break;
                case Keys.F7:
                    btnDel_Click(sender, e);
                    break;
                case Keys.F8:
                    btnSave_Click(sender, e);
                    break;
                case Keys.F9:
                    btnAccounting_Click(sender, e);
                    break;
                case Keys.F10:
                    btnStopBill_Click(sender, e);
                    break;
                case Keys.F11:
                    btnRefresh_Click(sender, e);
                    break;
                case Keys.F12:
                    //btnCancelStrikeABalance_Click(sender, e);
                    break;
            }
        }

        /// <summary>
        /// 获取病人列表
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            isGridCurrentCell = false;
            InvokeController("GetPatientList");
        }

        /// <summary>
        /// 新开账单
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            if (grdPatList.CurrentCell != null)
            {
                // 长期账单
                if (tabControl2.SelectedTabIndex == 0)
                {
                    // 设置长期账单网格为可编辑状态
                    SetOrderDetailstGridState = false;
                    // 新增长期账单
                    grdOrderDetails.AddRow();
                    DataTable dt = grdOrderDetails.DataSource as DataTable;
                    // 设置新增的长期账单为未记账状态
                    //dt.Rows[grdOrderDetails.CurrentCell.RowIndex]["StrikeABalanceFLG"] = 0;
                    // 设置网格当前行选中Index
                    // Update by -- ZZ 影响新开账单的网格弹出
                    //setGridSelectIndex(grdOrderDetails, grdOrderDetails.CurrentCell.RowIndex);
                }
                else if (tabControl2.SelectedTabIndex == 1)
                {
                    // 临时账单
                    // 设置临时账单网格为可编辑状态
                    SetTempDetailstGridState = false;
                    // 新增临时账单
                    grdTempDetails.AddRow();
                    DataTable dt = grdTempDetails.DataSource as DataTable;
                    // 将新增的临时账单设置成未记账状态
                    //dt.Rows[grdTempDetails.CurrentCell.RowIndex]["StrikeABalanceFLG"] = 0;
                    // 设置网格当前行选中Index
                    // Update by -- ZZ 影响新开账单的网格弹出
                    //setGridSelectIndex(grdTempDetails, grdTempDetails.CurrentCell.RowIndex);
                }
            }
            else
            {
                // 没有选择病人时，提示Msg
                InvokeController("MessageShow", "请选择需要新开账单的病人！");
            }
        }

        /// <summary>
        /// 停用账单
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnStopBill_Click(object sender, EventArgs e)
        {
            if (grdPatList.CurrentCell != null)
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
        /// 删除账单
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (grdPatList.CurrentCell != null)
            {
                // 长期账单
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
                            if (int.Parse(dt.Rows[i]["CheckFlg"].ToString()) == 1)
                            {
                                if (Convert.ToInt32(dt.Rows[i]["StrikeABalanceFLG"].ToString()) == 1)
                                {
                                    msg += "[" + dt.Rows[i]["ItemName"].ToString() + "]、";
                                }

                                tempDt.Rows.Add(dt.Rows[i].ItemArray);
                            }
                        }
                    }

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
                    // 临时账单
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
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (grdPatList.CurrentCell != null)
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
        /// 记账
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnAccounting_Click(object sender, EventArgs e)
        {
            if (grdPatList.CurrentCell != null)
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
                            InvokeController("ShowFrmFeePresDate");
                        }
                    }
                }
                else if (tabControl2.SelectedTabIndex == 1)
                {
                    if (grdTempDetails.DataSource != null && grdTempDetails.CurrentCell != null)
                    {
                        grdOrderDetails.EndEdit();
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
        /// 刷新
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (tabControl2.SelectedTabIndex == 0)
            {
                InvokeController("GetPatLongFeeItemGenerate", mPatListId, 2);
            }
            else if (tabControl2.SelectedTabIndex == 1)
            {
                InvokeController("GetPatLongFeeItemGenerate", mPatListId, 3);
            }
            else if (tabControl2.SelectedTabIndex == 2)
            {
                //InvokeController("GetPatTempFeeItemGenerate");
            }
            // 加载账单模板列表
            InvokeController("GetIPFeeItemTempList");
        }

        /// <summary>
        /// 冲账
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnStrikeABalance_Click(object sender, EventArgs e)
        {
            if (grdPatList.CurrentCell != null)
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
                    // 是否勾选了当前数据行
                    if (Convert.ToInt32(dt.Rows[i]["CheckFlg"].ToString()) == 1)
                    {
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

                InvokeController("StrikeABalance", tempdt);
            }
        }

        /// <summary>
        /// 取消冲账
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnCancelStrikeABalance_Click(object sender, EventArgs e)
        {
            if (grdPatList.CurrentCell != null)
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
                    // 是否勾选了当前数据行
                    if (Convert.ToInt32(dt.Rows[i]["CheckFlg"].ToString()) == 1)
                    {
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

                InvokeController("CancelStrikeABalance", tempdt);
            }
        }

        /// <summary>
        /// 双击选中病人
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void grdPatList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isGridCurrentCell)
            {
                if (grdPatList.CurrentCell != null)
                {
                    grdPatList.CellDoubleClick -= new DataGridViewCellEventHandler(grdPatList_CellDoubleClick);
                    // 检查病人是否存在未保存的账单
                    if (DtNotSaveCheck())
                    {
                        int rowIndex = grdPatList.CurrentCell.RowIndex;
                        DataTable dt = grdPatList.DataSource as DataTable;
                        lblSerialNumber.Text = dt.Rows[rowIndex]["SerialNumber"].ToString(); // 住院流水号
                        lblPatName.Text = dt.Rows[rowIndex]["PatName"].ToString();  // 病人姓名
                        lblPatType.Text = dt.Rows[rowIndex]["PatTypeName"].ToString(); // 病人类型
                        lblPatSex.Text = dt.Rows[rowIndex]["SexName"].ToString(); // 性别
                        lblPatAge.Text = GetAge(dt.Rows[rowIndex]["Age"].ToString()); // 年龄
                        lblPatBedNo.Text = dt.Rows[rowIndex]["BedNo"].ToString();  // 床位号
                        lblPatDept.Text = dt.Rows[rowIndex]["DeptName"].ToString();  // 科室
                        lblPatBillDoctor.Text = dt.Rows[rowIndex]["DoctorName"].ToString();  // 医生
                        lblPatBillTime.Text = DateTime.Now.ToString("yyyy-MM-dd");   // 开方时间
                        mPatListId = int.Parse(dt.Rows[rowIndex]["PatListID"].ToString()); // 病人登记ID
                        InvokeController("GetPatSumPay", mPatListId);
                        if (tabControl2.SelectedTabIndex == 0)
                        {
                            // 获取病人长期账单
                            InvokeController("GetPatLongFeeItemGenerate", mPatListId, 2);
                        }
                        else if (tabControl2.SelectedTabIndex == 1)
                        {
                            // 获取病人临时账单
                            InvokeController("GetPatLongFeeItemGenerate", mPatListId, 3);
                        }
                        else if (tabControl2.SelectedTabIndex == 2)
                        {
                            InvokeController("GetCostList");
                        }

                        gridCellRowIndex = grdPatList.CurrentCell.RowIndex;
                    }
                    else
                    {
                        grdPatList.CurrentCell = grdPatList[0, gridCellRowIndex];
                    }

                    grdPatList.CellDoubleClick += new DataGridViewCellEventHandler(grdPatList_CellDoubleClick);
                }
            }
        }

        /// <summary>
        /// Tab切换刷新账单模板列表
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void tabControl1_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
        {
            if (tabControl1.SelectedTabIndex == 1)
            {
                // 加载账单模板列表
                InvokeController("GetIPFeeItemTempList");
            }
        }

        /// <summary>
        /// TabIndex切换时控制按钮是否可用
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void tabControl2_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
        {
            tabItem2.Visible = true;
            // 费用管理
            if (tabControl2.SelectedTabIndex == 2)
            {
                btnNew.Enabled = false;
                btnDel.Enabled = false;
                btnSave.Enabled = false;
                btnAccounting.Enabled = false;
                btnStopBill.Enabled = false;
                btnStrikeABalance.Enabled = true;
                btnCancelStrikeABalance.Enabled = true;
                tabItem2.Visible = false;
                if (cboOrderType.SelectedIndex == -1)
                {
                    cboOrderType.SelectedIndex = 0;
                }

                if (PatListID != 0)
                {
                    InvokeController("GetCostList");
                }
            }
            else
            {
                // 长期账单
                if (tabControl2.SelectedTabIndex == 0)
                {
                    btnStopBill.Enabled = true;
                    if (isTabSelect)
                    {
                        SetTempDetailstGridState = true;
                        grdTempDetails.EndEdit();
                        if (grdOrderDetails.DataSource == null || mPatListId != 0)
                        {
                            InvokeController("GetPatLongFeeItemGenerate", mPatListId, 2);
                        }
                    }
                }
                else if (tabControl2.SelectedTabIndex == 1)
                {
                    // 临时账单
                    SetOrderDetailstGridState = true;
                    grdOrderDetails.EndEdit();
                    btnStopBill.Enabled = false;
                    if (grdTempDetails.DataSource == null || mPatListId != 0)
                    {
                        InvokeController("GetPatLongFeeItemGenerate", mPatListId, 3);
                    }
                }

                btnNew.Enabled = true;
                btnDel.Enabled = true;
                btnSave.Enabled = true;
                btnAccounting.Enabled = true;
                btnStrikeABalance.Enabled = false;
                btnCancelStrikeABalance.Enabled = false;
            }
        }

        /// <summary>
        /// 长期账单弹出网格选择事件
        /// </summary>
        /// <param name="selectedValue">弹出网格选中的数据</param>
        /// <param name="stop">终止标志</param>
        /// <param name="customNextColumnIndex">绑定数据后光标聚焦的位置</param>
        private void grdOrderDetails_SelectCardRowSelected(object selectedValue, ref bool stop, ref int customNextColumnIndex)
        {
            try
            {
                DataRow row = (DataRow)selectedValue;
                int rowid = this.grdOrderDetails.CurrentCell.RowIndex;
                DataTable dt = (DataTable)grdOrderDetails.DataSource;
                SetFeeItemDt(dt.Rows[rowid], row, false);
                SetGridColor();
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
        /// <param name="stop">终止标志</param>
        /// <param name="customNextColumnIndex">绑定数据后光标聚焦的位置</param>
        private void grdTempDetails_SelectCardRowSelected(object selectedValue, ref bool stop, ref int customNextColumnIndex)
        {
            try
            {
                DataRow row = (DataRow)selectedValue;
                int rowid = this.grdTempDetails.CurrentCell.RowIndex;
                DataTable dt = (DataTable)grdTempDetails.DataSource;
                SetFeeItemDt(dt.Rows[rowid], row, false);
                SetGridColor();
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
        /// 长期根据数量计算总价格
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
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
                        SetGridColor();
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
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
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
                        SetGridColor();
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
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
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

                SetGridColor();
                setGridSelectIndex(grdOrderDetails, grdOrderDetails.CurrentCell.RowIndex);
            }
        }

        /// <summary>
        /// 临时账单-网格CheckBox选中事件
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
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

                SetGridColor();
                setGridSelectIndex(grdTempDetails, grdTempDetails.CurrentCell.RowIndex);
            }
        }

        /// <summary>
        /// 费用一览CheckBox点击事件
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
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
        /// 设置长期账单已记账数据不能编辑
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
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
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
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
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmDoctorsOrderSccounting_VisibleChanged(object sender, EventArgs e)
        {
            grdOrderDetails.EndEdit();
            grdTempDetails.EndEdit();
            dtTime.Bdate.Value = DateTime.Now;
            dtTime.Edate.Value = DateTime.Now;
        }

        /// <summary>
        /// 双击选中模板
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void trvTempList_NodeDoubleClick(object sender, TreeNodeMouseEventArgs e)
        {
            if (grdPatList.CurrentCell != null &&
                    !string.IsNullOrEmpty(lblSerialNumber.Text.Trim()) &&
                    !string.IsNullOrEmpty(lblPatName.Text.Trim()))
            {
                if (trvTempList.SelectedNode.Name == "PTemp" || (trvTempList.SelectedNode.Tag as IP_FeeItemTemplateHead).PTempHeadID == 0)
                {
                    return;
                }
                //if (tabControl2.SelectedTabIndex == 0)
                //{
                InvokeController("GetFeeItemTempDetails", (trvTempList.SelectedNode.Tag as IP_FeeItemTemplateHead).TempHeadID);
                //}
                //else if (tabControl2.SelectedTabIndex == 1)
                //{

                //}
            }
            else
            {
                InvokeController("MessageShow", "请选择要应用模板的病人！");
            }
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (DtNotSaveCheck())
            {
                InvokeController("Close", this);
            }
        }

        #endregion

        #region "数据绑定"
        /// <summary>
        /// 绑定科室列表数据
        /// </summary>
        /// <param name="deptDt">科室列表数据</param>
        public void Bind_DeptList(DataTable deptDt)
        {
            txtDeptList.MemberField = "DeptId";
            txtDeptList.DisplayField = "Name";
            txtDeptList.CardColumn = "Name|名称|auto";
            txtDeptList.QueryFieldsString = "Name,Pym,Wbm,Szm";
            txtDeptList.ShowCardWidth = 350;
            txtDeptList.ShowCardDataSource = deptDt;
            txtDeptList.MemberValue = -1;
        }

        /// <summary>
        /// 绑定在床病人列表
        /// </summary>
        /// <param name="patListDt">在床病人列表</param>
        public void Bind_PatientList(DataTable patListDt)
        {
            grdPatList.DataSource = patListDt;
            isGridCurrentCell = true;
            grdPatList_CellDoubleClick(null, null);
        }

        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="age">待处理的年龄值</param>
        /// <returns>处理后的年龄值</returns>
        private string GetAge(string age)
        {
            string tempAge = string.Empty;
            if (!string.IsNullOrEmpty(age))
            {
                switch (age.Substring(0, 1))
                {
                    // 岁
                    case "Y":
                        if (age.Length > 1)
                        {
                            tempAge = age.Substring(1) + "岁";
                        }

                        break;
                    // 月
                    case "M":
                        if (age.Length > 1)
                        {
                            tempAge = age.Substring(1) + "月";
                        }

                        break;
                    // 天
                    case "D":
                        if (age.Length > 1)
                        {
                            tempAge = age.Substring(1) + "天";
                        }

                        break;
                    // 时
                    case "H":
                        if (age.Length > 1)
                        {
                            tempAge = age.Substring(1) + "时";
                        }

                        break;
                }
            }

            return tempAge;
        }

        /// <summary>
        /// 显示病人的累计交费金额和累计记账金额
        /// </summary>
        /// <param name="sumpay">累计交费金额</param>
        /// <param name="longFeeOrder">长期账单金额</param>
        /// <param name="tempFeeOrder">临时账单金额</param>
        /// <param name="bedFee">床位费</param>
        public void Bind_PatSumPay(decimal sumpay, decimal longFeeOrder, decimal tempFeeOrder, decimal bedFee)
        {
            decimal sumAccounting = longFeeOrder + tempFeeOrder + bedFee;
            // 累计交费金额
            if (sumpay > 0)
            {
                lblPatSumPay.Text = string.Format("{0:N}", sumpay);
            }
            else
            {
                lblPatSumPay.Text = "0.00";
            }

            if (sumAccounting > 0)
            {
                // 累计记账金额
                lblSumAccounting.Text = string.Format("{0:N}", sumAccounting);
                // 余额
                lblBalance.Text = string.Format("{0:N}", sumpay - sumAccounting);
            }
            else
            {
                // 累计记账金额
                lblSumAccounting.Text = "0.00";
                // 余额
                lblBalance.Text = lblPatSumPay.Text;
            }

            // 长期账单金额 
            if (longFeeOrder > 0)
            {
                lblLongBillMoney.Text = string.Format("{0:N}", longFeeOrder);
            }
            else
            {
                lblLongBillMoney.Text = "0.00";
            }

            // 临时账单金额
            if (tempFeeOrder > 0)
            {
                lblTempBillMoney.Text = string.Format("{0:N}", tempFeeOrder);
            }
            else
            {
                lblTempBillMoney.Text = "0.00";
            }

            // 床位费
            if (bedFee > 0)
            {
                lblBedFee.Text = string.Format("{0:N}", bedFee);
            }
            else
            {
                lblBedFee.Text = "0.00";
            }
        }

        /// <summary>
        /// 绑定弹出网格费用列表
        /// </summary>
        /// <param name="feeDt">费用列表</param>
        public void Bind_SimpleFeeItemData(DataTable feeDt)
        {
            // 长期医嘱
            grdOrderDetails.SelectionCards[0].BindColumnIndex = ItemCode.Index;
            grdOrderDetails.SelectionCards[0].CardColumn = "ItemCode|编码|100,ItemName|项目名称|150,UnitPrice|单价|80,StoreAmount|库存数|80,ExecDeptName|执行科室|auto";
            grdOrderDetails.SelectionCards[0].CardSize = new System.Drawing.Size(580, 180);
            grdOrderDetails.SelectionCards[0].QueryFieldsString = "ItemCode,ItemName,Pym,Wbm";
            grdOrderDetails.BindSelectionCardDataSource(0, feeDt);
            // 临时医嘱
            grdTempDetails.SelectionCards[0].BindColumnIndex = TempItemCode.Index;
            grdTempDetails.SelectionCards[0].CardColumn = "ItemCode|编码|100,ItemName|项目名称|150,UnitPrice|单价|80,StoreAmount|库存数|80,ExecDeptName|执行科室|auto";
            grdTempDetails.SelectionCards[0].CardSize = new System.Drawing.Size(580, 180);
            grdTempDetails.SelectionCards[0].QueryFieldsString = "ItemCode,ItemName,Pym,Wbm";
            grdTempDetails.BindSelectionCardDataSource(0, feeDt);

            txtFeeDetailList.MemberField = "ItemID";
            txtFeeDetailList.DisplayField = "ItemName";
            txtFeeDetailList.CardColumn = "ItemID|编码|100,ItemName|名称|auto";
            txtFeeDetailList.QueryFieldsString = "ItemCode,ItemName,Pym,Wbm";
            txtFeeDetailList.ShowCardWidth = 350;
            txtFeeDetailList.ShowCardDataSource = feeDt;
        }

        /// <summary>
        /// 绑定病人账单列表
        /// </summary>
        /// <param name="longOrderDt">长期账单列表</param>
        public void Bind_LongOrderData(DataTable longOrderDt)
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
            }
        }

        /// <summary>
        /// 绑定模板列表
        /// </summary>
        /// <param name="feeTempList">模板列表</param>
        public void Bind_FeeTempList(List<IP_FeeItemTemplateHead> feeTempList)
        {
            trvTempList.Nodes.Clear();
            // 添加根节点
            Node pNode = new Node();
            pNode.Text = "全部模板";
            pNode.Name = "PTemp";
            trvTempList.Nodes.Add(pNode);
            trvTempList.SelectedNode = pNode;
            // 循环显示父节点
            List<IP_FeeItemTemplateHead> feeTempHeadList = feeTempList.Where(item => item.PTempHeadID == 0).ToList();
            foreach (IP_FeeItemTemplateHead feeTemp in feeTempHeadList)
            {
                Node newNode = new Node();
                newNode.Text = feeTemp.TempName;
                newNode.Name = feeTemp.TempHeadID.ToString();
                newNode.Tag = feeTemp;
                trvTempList.SelectedNode.Nodes.Add(newNode);
            }
            // 循环显示子节点
            List<IP_FeeItemTemplateHead> feeTempDetialList = feeTempList.Where(item => item.PTempHeadID > 0).ToList();
            if (feeTempDetialList.Count > 0)
            {
                foreach (IP_FeeItemTemplateHead feeTemp in feeTempDetialList)
                {
                    // 取得当前循环节点的根节点
                    Node selectNode = trvTempList.Nodes.Find(feeTemp.PTempHeadID.ToString(), true).FirstOrDefault();
                    Node newNode = new Node();
                    newNode.Text = feeTemp.TempName;
                    newNode.Name = feeTemp.TempHeadID.ToString();
                    newNode.Tag = feeTemp;
                    if (feeTemp.DelFlag == 1)
                    {
                        newNode.Style = new DevComponents.DotNetBar.ElementStyle(Color.Red);
                    }

                    trvTempList.SelectedNode = selectNode;
                    trvTempList.SelectedNode.Nodes.Add(newNode);
                }
            }
            // 默认选中根节点
            if (trvTempList.Nodes.Count > 0)
            {
                trvTempList.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 根据模板明细绑定账单列表
        /// </summary>
        /// <param name="feeDetailsDt">账单列表</param>
        public void Bind_TempDetailLongOrderData(DataTable feeDetailsDt)
        {
            DataTable feeItemDt = null;
            if (tabControl2.SelectedTabIndex == 0)
            {
                // 长期账单
                feeItemDt = grdOrderDetails.DataSource as DataTable;
                DataRow dr = null;
                if (feeDetailsDt.Rows.Count > 0)
                {
                    for (int i = 0; i < feeDetailsDt.Rows.Count; i++)
                    {
                        dr = feeItemDt.NewRow();
                        SetFeeItemDt(dr, feeDetailsDt.Rows[i], true);
                        feeItemDt.Rows.Add(dr);
                    }

                    SetGridColor();
                }
            }
            else if (tabControl2.SelectedTabIndex == 1)
            {
                // 临时账单
                feeItemDt = grdTempDetails.DataSource as DataTable;
                DataRow dr = null;
                if (feeDetailsDt.Rows.Count > 0)
                {
                    for (int i = 0; i < feeDetailsDt.Rows.Count; i++)
                    {
                        dr = feeItemDt.NewRow();
                        SetFeeItemDt(dr, feeDetailsDt.Rows[i], true);
                        feeItemDt.Rows.Add(dr);
                    }

                    SetGridColor();
                }
            }
        }

        /// <summary>
        /// 账单录入
        /// </summary>
        /// <param name="orderdr">账单数据</param>
        /// <param name="tempDr">弹出网格选中数据</param>
        /// <param name="isApplicationTemp">是否为模板应用</param>
        private void SetFeeItemDt(DataRow orderdr, DataRow tempDr, bool isApplicationTemp)
        {
            if (string.IsNullOrEmpty(orderdr["GenerateID"].ToString()))
            {
                orderdr["GenerateID"] = 0;
            }

            orderdr["StrikeABalanceFLG"] = 0;  // 记账ID
            orderdr["PatListID"] = mPatListId;  // 病人登记ID
            if (grdPatList.CurrentCell != null)
            {
                int rowIndex = grdPatList.CurrentCell.RowIndex;
                DataTable patDt = grdPatList.DataSource as DataTable;
                orderdr["PatName"] = patDt.Rows[rowIndex]["PatName"].ToString(); // 病人名
                orderdr["PatDeptID"] = patDt.Rows[rowIndex]["CurrDeptID"].ToString(); // 病人当前科室ID
                orderdr["PatDoctorID"] = patDt.Rows[rowIndex]["CurrDoctorID"].ToString();  // 病人责任医生ID
                orderdr["PatNurseID"] = patDt.Rows[rowIndex]["CurrNurseID"].ToString();  // 病人责任护士ID
                orderdr["MarkEmpName"] = patDt.Rows[rowIndex]["DoctorName"].ToString();  // 处方医生姓名
            }

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
            if (isApplicationTemp)
            {
                if (!string.IsNullOrEmpty(tempDr["ItemAmount"].ToString()))
                {
                    orderdr["Amount"] = tempDr["ItemAmount"];  // 数量
                    // 数量
                    int count = int.Parse(tempDr["ItemAmount"].ToString());
                    // 销售价
                    decimal packAmount = decimal.Parse(tempDr["SellPrice"].ToString());
                    // 划价系数
                    decimal miniConvertNum = Convert.ToDecimal(tempDr["MiniConvertNum"]);
                    // (数量*金额)/划价系数
                    orderdr["TotalFee"] = Math.Round((count * packAmount) / miniConvertNum, 4);
                }
                else
                {
                    orderdr["Amount"] = 0;  // 数量
                    orderdr["TotalFee"] = 0; // 总金额
                }
            }
            else
            {
                orderdr["Amount"] = 0;  // 数量
                orderdr["TotalFee"] = 0; // 总金额
            }

            orderdr["DoseAmount"] = 0;// 处方帖数
            orderdr["ExecDeptDoctorID"] = tempDr["ExecDeptId"]; // 执行科室ID
            orderdr["ExecDeptName"] = tempDr["ExecDeptName"]; // 执行科室名
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
            orderdr["IsUpdate"] = 1;
        }

        /// <summary>
        /// 检查是否有未保存的账单
        /// </summary>
        /// <returns>true:不保存正在编辑的数据/false：不执行操作</returns>
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
        /// 设置网格颜色
        /// </summary>
        public void SetGridColor()
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
        #endregion

        /// <summary>
        /// 已记账费用查询
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnSeach_Click(object sender, EventArgs e)
        {
            InvokeController("GetCostList");
        }

        /// <summary>
        /// 绑定已记账费用列表
        /// </summary>
        /// <param name="costListDt">已记账费用列表</param>
        public void Bind_CostList(DataTable costListDt)
        {
            grdFeeList.DataSource = costListDt;
            setGridSelectIndex(grdFeeList);
        }

        /// <summary>
        /// 设置费用网格背景色
        /// </summary>
        public void SetFeeListGridColor()
        {
            if (tabControl2.SelectedTabIndex == 2)
            {
                // 长期账单
                DataTable feeItemDt = grdFeeList.DataSource as DataTable;
                for (int i = 0; i < feeItemDt.Rows.Count; i++)
                {
                    Color foreColor = Color.Blue;
                    int recordFlag = Convert.ToInt32(feeItemDt.Rows[i]["RecordFlag"].ToString());
                    if (recordFlag == 1 || recordFlag == 2 || recordFlag == 9)
                    {
                        foreColor = Color.Green;
                    }

                    grdFeeList.SetRowColor(i, foreColor, true);
                }
            }
        }

        /// <summary>
        /// 长期账单全选反选
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void cbkLongOrderAll_CheckedChanged(object sender, EventArgs e)
        {
            // 是否选中了病人
            if (grdPatList.CurrentCell != null)
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
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void cbkTempOrderAll_CheckedChanged(object sender, EventArgs e)
        {
            // 是否选中了病人
            if (grdPatList.CurrentCell != null)
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
        /// 费用一览全选或反选
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void cbkBillSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            // 是否选中了病人
            if (grdPatList.CurrentCell != null)
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

                        dt.Rows[i]["CheckFLG"] = cbkBillSelectAll.Checked ? 1 : 0;
                    }
                }
            }
        }
    }
}
