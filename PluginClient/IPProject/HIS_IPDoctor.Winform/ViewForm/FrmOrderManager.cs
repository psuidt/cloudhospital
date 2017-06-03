using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using EfwControls.HISControl.Orders.Controls.Entity;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_IPDoctor.Winform.Controller;
using HIS_IPDoctor.Winform.IView;

namespace HIS_IPDoctor.Winform.ViewForm
{
    public partial class FrmOrderManager : BaseFormBusiness, IFrmOrderManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmOrderManager()
        {
            InitializeComponent();
            dgvLongModel.GroupLine = new EfwControls.CustomControl.PaintGroupLineHandle(PaintGroupLine);
            dgvTempModel.GroupLine = new EfwControls.CustomControl.PaintGroupLineHandle(HasBeenDocPaintGroupLine);
            bindGridSelectIndex(dgDiagnosis);
        }

        #region 接口实现
        /// <summary>
        /// 科室ID
        /// </summary>
        public int DeptId
        {
            get
            {
                return Convert.ToInt32(cmbDept.SelectedValue);
            }

            set
            {
                cmbDept.SelectedValue = value;
            }
        }

        /// <summary>
        /// 当前科室名称
        /// </summary>
        public string presDeptName
        {
            get
            {
                return cmbDept.Text.Trim();
            }
        }

        /// <summary>
        /// 诊断录入状态 0新增 1编辑
        /// </summary>
        private int diagFlag;

        /// <summary>
        /// 诊断录入状态 0新增 1编辑
        /// </summary>
        public int DiagFlag
        {
            get
            {
                return diagFlag;
            }

            set
            {
                diagFlag = value;
            }
        }

        /// <summary>
        /// 当前病人ID
        /// </summary>
        private int curpatlistid;

        /// <summary>
        /// 当前病人Id
        /// </summary>
        public int CurPatListId
        {
            get
            {
                return curpatlistid;
            }

            set
            {
                curpatlistid = value;
            }
        }

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="dtDept">科室数据</param>
        public void BindDept(DataTable dtDept)
        {
            cmbDept.DisplayMember = "Name";
            cmbDept.ValueMember = "deptID";
            cmbDept.DataSource = dtDept;
        }

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="dtpatInfo">病人信息</param>
        public void BindPatInfo(DataTable dtpatInfo)
        {
            dgvPatInfo.DataSource = dtpatInfo;
        }

        /// <summary>
        /// 出院单选选中改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void radOutHosp_CheckedChanged(object sender, EventArgs e)
        {
            stdDate.Enabled = radOutHosp.Checked;
        }

        #region 默认药房选择
        /// <summary>
        /// 默认药房ID
        /// </summary>
        private string defaultDrugStore;

        /// <summary>
        /// 默认药房ID
        /// </summary>
        public string DefaultDrugStore
        {
            get
            {
                return defaultDrugStore;
            }

            set
            {
                defaultDrugStore = value;
            }
        }

        /// <summary>
        /// 药房选择控件是否可见
        /// </summary>
        public bool DrugStoreVisible
        {
            get
            {
                return cmbDrugStore.Visible;
            }

            set
            {
                cmbDrugStore.Visible = value;
                lblYf.Visible = value;
            }
        }

        /// <summary>
        /// 默认药房绑定
        /// </summary>
        /// <param name="dtDrugStore">药房数据</param>
        public void DrugStoreDataBind(DataTable dtDrugStore)
        {
            this.cmbDrugStore.Visible = true;
            lblYf.Visible = true;
            this.cmbDrugStore.DisplayMember = "Name";
            this.cmbDrugStore.ValueMember = "DeptIDs";
            cmbDrugStore.DataSource = dtDrugStore;
            this.cmbDrugStore.SelectedIndex = 0;
            DefaultDrugStore = dtDrugStore.Rows[0]["DeptIDs"].ToString();
        }
        #endregion

        /// <summary>
        /// 病人科室ID
        /// </summary>
        private int patdeptid;

        /// <summary>
        /// 病人科室ID
        /// </summary>
        public int patDeptID
        {
            get
            {
                return patdeptid;
            }

            set
            {
                patdeptid = value;
            }
        }

        /// <summary>
        /// 获取选择的药房ID
        /// </summary>
        public int GetDrugStoreID
        {
            get
            {
                return Convert.ToInt32(cmbDrugStore.SelectedValue);
            }
        }

        #region 病人详细信息显示
        /// <summary>
        /// 显示病人明细信息
        /// </summary>
        /// <param name="drPatInfo">病人信息</param>
        /// <param name="dtPatFee">费用信息</param>
        public void ShowPatDetailInfo(DataRow drPatInfo, DataTable dtPatFee)
        {
            if (drPatInfo == null)
            {
                lblCureno.Text =string.Empty;
                lblPatName.Text =string.Empty;
                lblAge.Text =string.Empty;
                lblInNums.Text =string.Empty;
                lblInhospStatus.Text =string.Empty;
                lblPatType.Text =string.Empty;
                lblNusingLevel.Text =string.Empty;
                lblBedNO.Text =string.Empty;
                lblDoctor.Text =string.Empty;
                lblNuseName.Text =string.Empty;
                lblDeptName.Text =string.Empty;
                lblInhospDate.Text =string.Empty;
                lblDialoge.Text =string.Empty;
                lblDepositFee.Text =string.Empty;
                lblChargeFee.Text =string.Empty;
                lblLastFee.Text =string.Empty;
            }
            else
            {
                lblCureno.Text = drPatInfo["SerialNumber"].ToString();
                lblPatName.Text = drPatInfo["PatName"].ToString();
                lblAge.Text = GetAge(drPatInfo["Age"].ToString());
                lblInNums.Text = drPatInfo["Times"].ToString();
                lblInhospStatus.Text = drPatInfo["EnterSituationName"].ToString();
                lblPatType.Text = drPatInfo["patTypeName"].ToString();
                lblNusingLevel.Text = drPatInfo["NursingLever"].ToString();
                lblBedNO.Text = drPatInfo["BedNo"].ToString();
                lblDoctor.Text = drPatInfo["doctorName"].ToString();
                lblNuseName.Text = drPatInfo["NurseName"].ToString();
                lblDeptName.Text = drPatInfo["deptName"].ToString();
                lblInhospDate.Text = DateTime.Parse(drPatInfo["EnterHDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                lblDialoge.Text = drPatInfo["EnterDiseaseName"].ToString();
                lblDepositFee.Text =Convert.ToDecimal( dtPatFee.Rows[0][0]).ToString("0.00");
                lblChargeFee.Text =Convert.ToDecimal( dtPatFee.Rows[1][0]).ToString("0.00");
                lblLastFee.Text = (Convert.ToDecimal(lblDepositFee.Text) - Convert.ToDecimal(lblChargeFee.Text)).ToString("0.00");
            }
        }

        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="age">传入年龄</param>
        /// <returns>返回转换后年龄</returns>
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
        #endregion

        #endregion
        /// <summary>
        /// 病人列表刷新
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnRefreshPatient_Click(object sender, EventArgs e)
        {
            if (radOutHosp.Checked)
            {
                if (stdDate.Bdate.Value > stdDate.Edate.Value)
                {
                    MessageBoxEx.Show("先择开始时间不能大于结束时间");
                    return;
                }
            }

            InvokeController("GetPatInfoList", stdDate.Bdate.Value, stdDate.Edate.Value, radOutHosp.Checked, radMyPatient.Checked, txtQueryContent.Text.ToString().Trim());
        }

        /// <summary>
        /// 窗体Show事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmOrderManager_Shown(object sender, EventArgs e)
        {
            radInHosp.Checked = true;
            radMyPatient.Checked = true;
            stdDate.Bdate.Value = DateTime.Now.AddDays(-7);
            stdDate.Edate.Value = DateTime.Now;
            txtQueryContent.Clear();
        }

        /// <summary>
        /// 是否定义出院
        /// </summary>
        private int isLeaveHosOrder;

        /// <summary>
        /// 是否有未完成的转科医嘱
        /// </summary>
        private bool hasnotfinisTans;

        /// <summary>
        /// 显示病人医嘱界面
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <param name="isLeaveHosOrder">是否开出院医嘱</param>
        /// <param name="presdeptid">开方科室ID</param>
        /// <param name="presDeptName">开方科室名称</param>
        /// <param name="presdocId">开方医生ID</param>
        /// <param name="presDocName">开方医生名称</param>
        /// <param name="patDeptID">病人科室ID</param>
        /// <param name="hasNotFinisTrans">是否有未完成转科医嘱</param>
        public void LoadPatData(int patlistid, int isLeaveHosOrder, int presdeptid, string presDeptName, int presdocId, string presDocName, int patDeptID, bool hasNotFinisTrans)
        {
            LongOrderControl.IsShowAllOrder = radAllOrder.Checked;
            TempOrderControl.IsShowAllOrder = radAllOrder.Checked;
            int workid = (InvokeController("this") as AbstractController).LoginUserInfo.WorkId;
            string workname= (InvokeController("this") as AbstractController).LoginUserInfo.WorkName;
            LongOrderControl.LoadPatData(patlistid, isLeaveHosOrder, presdeptid, presDeptName, presdocId, presDocName, patDeptID, DefaultDrugStore, lblInhospDate.Text, hasNotFinisTrans,workid,workname);
            TempOrderControl.LoadPatData(patlistid, isLeaveHosOrder, presdeptid, presDeptName, presdocId, presDocName, patDeptID, DefaultDrugStore, lblInhospDate.Text, hasNotFinisTrans,workid,workname);
            this.isLeaveHosOrder = isLeaveHosOrder;
            hasnotfinisTans = hasNotFinisTrans;
            InvokeController("GetApplyHead", 1);
            InvokeController("LoadDiagnosisInfo");
        }

        /// <summary>
        /// 双击病人列表显示
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgvPatInfo_DoubleClick(object sender, EventArgs e)
        {
            if (dgvPatInfo != null && dgvPatInfo.CurrentCell != null)
            {
                DataTable dt = (DataTable)dgvPatInfo.DataSource;
                int rowindex = dgvPatInfo.CurrentCell.RowIndex;
                CurPatListId = Convert.ToInt32(dt.Rows[rowindex]["patlistid"]);
                //InvokeController("ShowPatDetail",dt.Rows[rowindex]);
                InvokeController("FreshPatDetail");
            }
        }

        /// <summary>
        /// 窗体OpenWindowBefore事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmOrderManager_OpenWindowBefore(object sender, EventArgs e)
        {           
            TabControlOrder.SelectedTabIndex = 0;
            btnStopOrder.Visible = true;
            bar1.Refresh();            
            InvokeController("GetOrderTempList", 0);
        }

        /// <summary>
        /// 控件数据异步加载
        /// </summary>
        public void BindControlData()
        {
            IPDOrderDbHelper presHelper = new Controller.IPDOrderDbHelper();
            LongOrderControl.InitDbHelper(presHelper);
            TempOrderControl.InitDbHelper(presHelper);          
        }

        /// <summary>
        /// 异步加载完成事件
        /// </summary>
        public void BindControlDataComplete()
        {
            LongOrderControl.InitializeCardData();
            TempOrderControl.InitializeCardData();
            bar1.Enabled = true;           
        }

        #region 医嘱操作
        /// <summary>
        /// 新开医嘱
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            if (CurPatListId == 0)
            {
                return;
            }

            if (TabControlOrder.SelectedTabIndex == 0)
            {
                LongOrderControl.NewOrder(false);
            }
            else if (TabControlOrder.SelectedTabIndex == 2)
            {
                if (isLeaveHosOrder == 0 && !hasnotfinisTans)
                {
                    InvokeController("ShowApply",string.Empty, "0", "0", "0");
                }
                else
                {
                    InvokeController("ShowMessage", "该病人已经出院或者转科，不能开申请单");
                }
            }
            else if (TabControlOrder.SelectedTabIndex == 3)   
            {
                //诊断管理
                //    tbcDiagName.Text =string.Empty;
                //    txtICD.Text =string.Empty;
                //    txtCusDiag.Text =string.Empty;
                //    txtEffect.Text =string.Empty;
                //    DiagFlag = 0;  //诊断进入新增状态
            }
            else
            {
                TempOrderControl.NewOrder(false);
            }
        }

        /// <summary>
        /// 刷新医嘱
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnRefreshOrder_Click(object sender, EventArgs e)
        {
            if (CurPatListId == 0)
            {
                return;
            }

            if (TabControlOrder.SelectedTabIndex == 0)
            {
                LongOrderControl.RefreshOrder();
            }
            else if (TabControlOrder.SelectedTabIndex == 2)
            {
                InvokeController("GetApplyHead", 1);
            }
            else if (TabControlOrder.SelectedTabIndex == 3)
            {
                InvokeController("LoadDiagnosisInfo");
            }
            else
            {
                TempOrderControl.RefreshOrder();
            }
        }

        /// <summary>
        /// 医嘱保存
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CurPatListId == 0)
            {
                return;
            }

            if (TabControlOrder.SelectedTabIndex != 3)
            {
                if (LongOrderControl.SaveOrder())
                {
                    TempOrderControl.SaveOrder();
                }
            }
            else
            {
                //txtEffect.Focus();
                //SaveDiagInfo();
            }
        }

        /// <summary>
        /// 医嘱确认
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            LongOrderControl.SendOrder();
            TempOrderControl.SendOrder();
        }
        
        /// <summary>
        /// 医嘱停嘱
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnStopOrder_Click(object sender, EventArgs e)
        {
            if (CurPatListId == 0)
            {
                return;
            }

            LongOrderControl.StopOrder();
        }

        /// <summary>
        /// 转科医嘱
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnTransefer_Click(object sender, EventArgs e)
        {
            if (CurPatListId == 0)
            {
                return;
            }

            if (LongOrderControl.GetNotSaveOrders().Count > 0)
            {
                MessageBoxEx.Show("还有新开长嘱医嘱未保存，不允许开转科医嘱");
                return;
            }
            else if (TempOrderControl.GetNotSaveOrders().Count > 0)
            {
                MessageBoxEx.Show("还有新开临嘱医嘱未保存，不允许开转科医嘱");
                return;
            }

            if (TempOrderControl.TransferDept(lblCureno.Text.Trim(), lblPatName.Text.Trim(),string.Empty, lblBedNO.Text.Trim(), lblDialoge.Text.Trim()))
            {
                InvokeController("FreshPatDetail");
            }
        }

        /// <summary>
        /// 出院医嘱
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnOutOrder_Click(object sender, EventArgs e)
        {
            if (CurPatListId == 0)
            {
                return;
            }

            if (LongOrderControl.GetNotSaveOrders().Count > 0)
            {
                MessageBoxEx.Show("还有新开长嘱医嘱未保存，不允许开出院医嘱");
                return;
            }
            else if (TempOrderControl.GetNotSaveOrders().Count > 0)
            {
                MessageBoxEx.Show("还有新开临嘱医嘱未保存，不允许开出院医嘱");
                return;
            }

            if (TempOrderControl.OutHospital(lblCureno.Text.Trim(), lblPatName.Text.Trim(),string.Empty, lblBedNO.Text.Trim(), lblDialoge.Text.Trim()))
            {
                InvokeController("FreshPatDetail");
            }
        }

        /// <summary>
        /// 死亡医嘱
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void buttonItem2_Click(object sender, EventArgs e)
        {
            if (CurPatListId == 0)
            {
                return;
            }

            if (LongOrderControl.GetNotSaveOrders().Count > 0)
            {
                MessageBoxEx.Show("还有新开长嘱医嘱未保存，不允许开死亡医嘱");
                return;
            }
            else if (TempOrderControl.GetNotSaveOrders().Count > 0)
            {
                MessageBoxEx.Show("还有新开临嘱医嘱未保存，不允许开死亡医嘱");
                return;
            }

            if (TempOrderControl.DeathOrder(lblCureno.Text.Trim(), lblPatName.Text.Trim(),string.Empty, lblBedNO.Text.Trim(), lblDialoge.Text.Trim(), Convert.ToDateTime(lblInhospDate.Text)))
            {
                InvokeController("FreshPatDetail");
            }
        }

        /// <summary>
        /// 选项卡数据源刷新
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnRefreshShowCard_Click(object sender, EventArgs e)
        {
            if (TabControlOrder.SelectedTabIndex == 0)
            {
                LongOrderControl.RefreshShowCardFromDataBase();
            }
            else if (TabControlOrder.SelectedTabIndex == 1)
            {
                TempOrderControl.RefreshShowCardFromDataBase();
            }
        }

        #endregion

        #region 控件委拖事件
        /// <summary>
        /// 刷新病人详细信息
        /// </summary>
        private void LongOrderControl_Freshlinkage()
        {
            InvokeController("FreshPatDetail");
        }

        /// <summary>
        /// 病人信息和医嘱刷新
        /// </summary>
        private void TempOrderControl_Freshlinkage()
        {
            InvokeController("FreshPatDetail");
        }

        /// <summary>
        /// 临嘱皮试关联显示
        /// </summary>
        /// <param name="patListId">病人ID</param>
        /// <param name="data">医嘱数据</param>
        private void TempOrderControl_Astoflinkage(int patListId, List<OrderRecord> data)
        {
            DataTable dt = TempOrderControl.GetGridOrder;
            foreach (OrderRecord actRecord in data)
            {
                int astGroupID = 0;
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (Convert.ToInt32(dt.Rows[i]["AstOrderID"]) == actRecord.OrderID)
                    {
                        astGroupID = Convert.ToInt32(dt.Rows[i]["GroupID"]);
                        break;
                    }
                }

                if (astGroupID > 0)
                {
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        if (Convert.ToInt32(dt.Rows[i]["GroupID"]) == astGroupID)
                        {
                            //dt.Rows.RemoveAt(i);
                            dt.Rows[i].Delete();
                        }
                    }

                    dt.AcceptChanges();
                    TempOrderControl.LoadGridOrderData(dt);
                }
            }
        }

        /// <summary>
        /// 长嘱皮试关联显示
        /// </summary>
        /// <param name="patListId">病人ID</param>
        /// <param name="data">医嘱数据</param>
        private void LongOrderControl_Astoflinkage(int patListId, List<OrderRecord> data)
        {
            DataTable dt = TempOrderControl.GetGridOrder;
            foreach (OrderRecord actRecord in data)
            {
                int astGroupID = 0;
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (Convert.ToInt32(dt.Rows[i]["AstOrderID"]) == actRecord.OrderID)
                    {
                        astGroupID = Convert.ToInt32(dt.Rows[i]["GroupID"]);
                        break;
                    }
                }

                if (astGroupID > 0)
                {
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        if (Convert.ToInt32(dt.Rows[i]["GroupID"]) == astGroupID)
                        {
                            //dt.Rows.RemoveAt(i);
                            dt.Rows[i].Delete();
                        }
                    }

                    dt.AcceptChanges();
                    TempOrderControl.LoadGridOrderData(dt);
                }
            }

            //DataTable dtAct = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable(data);
            //foreach (DataRow dr in dtAct.Rows)
            //{
            //    if (Convert.ToInt32(dr["GroupFlag"]) == 0)
            //    {
            //        dr["ShowOrderBdate"] = DBNull.Value;                   
            //    }
            //    dr["ExecDate"] = DBNull.Value;
            //    dt.Rows.Add(dr.ItemArray);
            //}
        }

        /// <summary>
        /// 长嘱医嘱保存提示
        /// </summary>
        /// <param name="orderCategory">医嘱类别</param>
        /// <param name="rowindex">行号</param>
        /// <param name="colName">列号</param>
        private void LongOrderControl_SaveCheckoflinkage(int orderCategory, int rowindex, string colName)
        {
            if (orderCategory == 0)
            {
                TabControlOrder.SelectedTabIndex = 0;
                this.LongOrderControl.Focus();
                this.LongOrderControl.SetGridCurrentCell(rowindex, colName);
            }
        }

        /// <summary>
        /// 临嘱医嘱保存提示
        /// </summary>
        /// <param name="orderCategory">医嘱类别</param>
        /// <param name="rowindex">行号</param>
        /// <param name="colName">列号</param>
        private void TempOrderControl_SaveCheckoflinkage(int orderCategory, int rowindex, string colName)
        {
            if (orderCategory == 1)
            {
                TabControlOrder.SelectedTabIndex = 1;
                this.TempOrderControl.Focus();
                this.TempOrderControl.SetGridCurrentCell(rowindex, colName);
            }
        }

        /// <summary>
        /// 右下角弹框显示
        /// </summary>
        /// <param name="message">提示消息</param>
        private void LongOrderControl_MessageShowoflinkage(string message)
        {
            InvokeController("ShowMessage", message);
        }

        /// <summary>
        /// 右下角弹框显示
        /// </summary>
        /// <param name="message">提示消息</param>
        private void TempOrderControl_MessageShowoflinkage(string message)
        {
            InvokeController("ShowMessage", message);
        }

        /// <summary>
        /// 医嘱费用显示
        /// </summary>
        /// <param name="patListID">病人ID</param>
        /// <param name="groupID">组ID</param>
        /// <param name="execDeptID">执行科室ID</param>
        /// <param name="execDeptName">执行科室名称</param>
        /// <param name="orderStatus">医嘱状态</param>
        /// <param name="orderType">医嘱类别</param>
        private void LongOrderControl_FeeShowoflinkage(int patListID, int groupID, int execDeptID, string execDeptName, int orderStatus, int orderType)
        {
            if (groupID == 0)
            {
                orderFeeControl.LoadOrderFeeGridData(-1, 0, execDeptID, execDeptName);
                orderFeeControl.SetButtonEnabled(false);
                return;
            }

            if (orderType >= 4)
            {
                orderFeeControl.SetButtonEnabled(false);
            }

            if (orderStatus < 2)
            {
                orderFeeControl.SetButtonEnabled(true);
            }
            else
            {
                orderFeeControl.SetButtonEnabled(false);
            }

            if (orderFeeControl.GroupID != groupID)
            {
                orderFeeControl.LoadOrderFeeGridData(patListID, groupID, execDeptID, execDeptName);
            }
        }

        /// <summary>
        /// 医嘱费用显示
        /// </summary>
        /// <param name="patListID">病人ID</param>
        /// <param name="groupID">组ID</param>
        /// <param name="execDeptID">执行科室ID</param>
        /// <param name="execDeptName">执行科室名称</param>
        /// <param name="orderStatus">医嘱状态</param>
        /// <param name="orderType">医嘱类别</param>
        private void TempOrderControl_FeeShowoflinkage(int patListID, int groupID, int execDeptID, string execDeptName, int orderStatus, int orderType)
        {
            if (groupID == 0)
            {
                orderFeeControl.LoadOrderFeeGridData(-1, 0, execDeptID, execDeptName);
                orderFeeControl.SetButtonEnabled(false);
                return;
            }

            if (orderType >= 4)
            {
                orderFeeControl.SetButtonEnabled(false);
            }

            if (orderStatus < 2)
            {
                orderFeeControl.SetButtonEnabled(true);
            }
            else
            {
                orderFeeControl.SetButtonEnabled(false);
            }

            if (orderFeeControl.GroupID != groupID)
            {
                orderFeeControl.LoadOrderFeeGridData(patListID, groupID, execDeptID, execDeptName);
            }
        }
        #endregion

        #region 其他
        /// <summary>
        /// Tab控件切换事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TabControlOrder_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            if (TabControlOrder.SelectedTabIndex == 0)
            {
                orderFeeControl.LoadOrderFeeGridData(-1, 0, 0,string.Empty);
                orderFeeControl.SetButtonEnabled(false);
                LongOrderControl.GridEndEdit();
                btnStopOrder.Visible = true;
                expandableSplitter1.Show();
                orderFeeControl.Show();
                btnOutOrder.Enabled = true;
                btnStopOrder.Enabled = true;
                btnSend.Enabled = true;
                btnSave.Enabled = true;
                panelOrderFee.Visible = true;
                btnRefreshOrder.Enabled = true;
                btnDelOrder.Enabled = true;
                cmbDrugStore.Enabled = true;
                radAllOrder.Enabled = true;
                radValid.Enabled = true;
                btnRefreshShowCard.Enabled = true;
                btnNew.Enabled = true;
            }
            else if (TabControlOrder.SelectedTabIndex == 2)
            {
                expandableSplitter1.Hide();
                orderFeeControl.Hide();
                btnOutOrder.Enabled = false;
                btnStopOrder.Enabled = false;
                btnSend.Enabled = false;
                btnSave.Enabled = false;
                btnNew.Enabled = true;
                panelOrderFee.Visible = false;
                btnDelOrder.Enabled = false;
                cmbDrugStore.Enabled = false;
                radAllOrder.Enabled = false;
                radValid.Enabled = false;
                btnRefreshShowCard.Enabled = false;
                InvokeController("GetApplyHead", 1);
            }
            else if (TabControlOrder.SelectedTabIndex == 3)
            {
                //诊断管理
                expandableSplitter1.Hide();
                orderFeeControl.Hide();
                btnNew.Enabled = false;
                btnSave.Enabled = false;
                btnOutOrder.Enabled = false;
                btnStopOrder.Enabled = false;
                btnSend.Enabled = false;
                chbICD.Checked = true;
                panelOrderFee.Visible = false;
                btnDelOrder.Enabled = false;
                cmbDrugStore.Enabled = false;
                radAllOrder.Enabled = false;
                radValid.Enabled = false;
                btnRefreshShowCard.Enabled = false;
                InvokeController("LoadDiagnosisInfo");
            }
            else
            {
                orderFeeControl.LoadOrderFeeGridData(-1, 0, 0,string.Empty);
                orderFeeControl.SetButtonEnabled(false);
                TempOrderControl.GridEndEdit();
                btnStopOrder.Visible = false;
                TempOrderControl.SetGridColor();
                expandableSplitter1.Show();
                orderFeeControl.Show();
                btnNew.Enabled = true;
                btnOutOrder.Enabled = true;
                btnStopOrder.Enabled = true;
                btnSend.Enabled = true;
                btnSave.Enabled = true;
                panelOrderFee.Visible = true;
                btnRefreshOrder.Enabled = true;
                btnDelOrder.Enabled = true;
                cmbDrugStore.Enabled = true;
                radAllOrder.Enabled = true;
                radValid.Enabled = true;
                btnRefreshShowCard.Enabled = true;
            }

            bar1.Refresh();
        }

        /// <summary>
        /// 费用控件面板Click事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void expandableSplitter1_Click(object sender, EventArgs e)
        {
            panelOrderFee.Expanded = !panelOrderFee.Expanded;
        }

        /// <summary>
        /// 窗体注册键盘事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmOrderManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.C)
            {
                if (txtQueryContent.Focus())
                {
                    return;
                }

                int tabSelectedIndex = TabControlOrder.SelectedTabIndex;
                switch (tabSelectedIndex)
                {
                    case 0:
                        LongOrderControl.OrderCopy();
                        break;
                    case 1:
                        TempOrderControl.OrderCopy();
                        break;
                }
            }

            if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.V)
            {
                if (txtQueryContent.Focus())
                {
                    return;
                }

                int tabSelectedIndex = TabControlOrder.SelectedTabIndex;
                switch (tabSelectedIndex)
                {
                    case 0:
                        LongOrderControl.OrderPaster();
                        break;
                    case 1:
                        TempOrderControl.OrderPaster();
                        break;
                }
            }
        }

        /// <summary>
        /// 默认药房选择
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cmbDrugStore_SelectedIndexChanged(object sender, EventArgs e)
        {           
            if (TabControlOrder.SelectedTabIndex == 0)
            {              
                TempOrderControl.RefreshDrugData(cmbDrugStore.SelectedValue.ToString());
                LongOrderControl.RefreshDrugData(cmbDrugStore.SelectedValue.ToString());
            }
            else
            {
                LongOrderControl.RefreshDrugData(cmbDrugStore.SelectedValue.ToString());
                TempOrderControl.RefreshDrugData(cmbDrugStore.SelectedValue.ToString());
            }
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (LongOrderControl.CloseCheck())
            {
                if (TempOrderControl.CloseCheck())
                {
                    InvokeController("Close", this);
                }
            }
        }
        #endregion

        #region 模板模板
        /// <summary>
        /// 定义模板对象
        /// </summary>
        private IPD_OrderModelHead mOrderModelHead = new IPD_OrderModelHead();

        /// <summary>
        /// 绑定模板列表
        /// </summary>
        /// <param name="orderTempList">模板列表</param>
        /// <param name="tempHeadID">模板头ID</param>
        public void bind_FeeTempList(List<IPD_OrderModelHead> orderTempList, int tempHeadID)
        {
            // 清空现有节点
            advTempDetails.Nodes.Clear();
            // advTempDetails.ImageList = imageList1;
            // 显示根节点
            List<IPD_OrderModelHead> orderTempHeadList = orderTempList.Where(item => item.PID == 0).ToList();
            Node newNode = new Node();
            newNode.Text = orderTempHeadList[0].ModelName;
            newNode.Name = orderTempHeadList[0].ModelHeadID.ToString();
            newNode.Tag = orderTempHeadList[0];
            newNode.ImageIndex = 0;
            advTempDetails.Nodes.Add(newNode);
            advTempDetails.SelectedNode = newNode;
            // 显示子节点
            List<IPD_OrderModelHead> tempDetialList = orderTempList.Where(item => item.PID > 0).ToList();
            if (tempDetialList.Count > 0)
            {
                foreach (IPD_OrderModelHead feeTemp in tempDetialList)
                {
                    // 取得当前循环节点的父节点
                    Node selectNode = advTempDetails.Nodes.Find(feeTemp.PID.ToString(), true).FirstOrDefault();
                    // 如果根节点不存在则默认上级节点为根节点
                    if (selectNode == null)
                    {
                        selectNode = advTempDetails.Nodes.Find("0", true).FirstOrDefault();
                    }

                    Node detailNode = new Node();
                    detailNode.Text = feeTemp.ModelName;
                    detailNode.Name = feeTemp.ModelHeadID.ToString();
                    detailNode.Tag = feeTemp;
                    if (feeTemp.ModelType == 0)
                    {
                        detailNode.ImageIndex = 0;
                    }
                    else
                    {
                        detailNode.ImageIndex = 1;
                    }

                    advTempDetails.SelectedNode = selectNode;
                    advTempDetails.SelectedNode.Nodes.Add(detailNode);
                }
            }

            if (tempHeadID != 0)
            {
                mOrderModelHead.ModelHeadID = tempHeadID;
            }
            // 重新选中上一次的节点
            if (mOrderModelHead != null && mOrderModelHead.ModelHeadID != 0)
            {
                Node selectNode = advTempDetails.Nodes.Find(mOrderModelHead.ModelHeadID.ToString(), true).FirstOrDefault();
                advTempDetails.SelectedNode = selectNode;
            }
            else
            {
                // 默认选中根节点
                if (advTempDetails.Nodes.Count > 0)
                {
                    advTempDetails.SelectedIndex = 0;
                }
            }
        }
        
        /// <summary>
        /// 我的模板选中改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void radSelf_CheckedChanged(object sender, EventArgs e)
        {
            if (radSelf.Checked)
            {
                InvokeController("GetOrderTempList", 2);
            }
        }

        /// <summary>
        /// 科室模板选中改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void radDept_CheckedChanged(object sender, EventArgs e)
        {
            if (radDept.Checked)
            {
                InvokeController("GetOrderTempList", 1);
            }
        }

        /// <summary>
        /// 全院模板选中改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void radAll_CheckedChanged(object sender, EventArgs e)
        {
            if (radAll.Checked)
            {
                InvokeController("GetOrderTempList", 0);
            }
        }
       
        /// <summary>
        /// 绑定医嘱明细数据
        /// </summary>
        /// <param name="longOrderDt">长期医嘱列表</param>
        /// <param name="tempOrderDt">临时医嘱列表</param>
        public void Bind_OrderDetails(DataTable longOrderDt, DataTable tempOrderDt)
        {
            // 同组数据合并
            TabControlModel.SelectedTabIndex = 0;
            DataGroup(longOrderDt);
            DataGroup(tempOrderDt);
            dgvLongModel.DataSource = longOrderDt;
            dgvTempModel.DataSource = tempOrderDt;
        }

        /// <summary>
        /// 合并同组部分数据()
        /// </summary>
        /// <param name="orderDetails">明细数据</param>
        private void DataGroup(DataTable orderDetails)
        {
            if (orderDetails.Rows.Count > 0)
            {
                int tempGroupID = 0;
                for (int i = 0; i < orderDetails.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        tempGroupID = Convert.ToInt32(orderDetails.Rows[i]["GroupID"]);
                        orderDetails.Rows[i]["IsLast"] = 1;
                        continue;
                    }

                    if (Convert.ToInt32(orderDetails.Rows[i]["GroupID"]) == tempGroupID)
                    {
                        orderDetails.Rows[i]["ChannelName"] = DBNull.Value;
                        orderDetails.Rows[i]["Frenquency"] = DBNull.Value;
                        orderDetails.Rows[i]["DropSpec"] = DBNull.Value;
                        orderDetails.Rows[i]["Entrust"] = DBNull.Value;
                        orderDetails.Rows[i]["IsLast"] = 0;  // 同组第一条
                    }

                    tempGroupID = Convert.ToInt32(orderDetails.Rows[i]["GroupID"]);
                }
            }
        }

        /// <summary>
        /// 长期绘制分组线
        /// </summary>
        /// <param name="rowIndex">行号</param>
        /// <param name="colIndex">列号</param>
        /// <param name="groupFlag">组标记</param>
        private void PaintGroupLine(int rowIndex, out int colIndex, out int groupFlag)
        {
            // 绘制分组线的列
            colIndex = 1;
            DataTable docList = dgvLongModel.DataSource as DataTable;
            groupFlag = GetGroupFlag(rowIndex, docList);
        }

        /// <summary>
        /// 临时绘制已转抄医嘱分组线
        /// </summary>
        /// <param name="rowIndex">行号</param>
        /// <param name="colIndex">列号</param>
        /// <param name="groupFlag">组标记</param>
        private void HasBeenDocPaintGroupLine(int rowIndex, out int colIndex, out int groupFlag)
        {
            // 绘制分组线的列
            colIndex = 1;
            DataTable docList = dgvTempModel.DataSource as DataTable;
            groupFlag = GetGroupFlag(rowIndex, docList);
        }

        /// <summary>
        /// 组标记
        /// </summary>
        private int mLastDocGroupFlag = 0;

        /// <summary>
        /// 获取分组线符号
        /// </summary>
        /// <param name="rowIndex">当前行号</param>
        /// <param name="docList">datatable数据</param>
        /// <returns>分组标志</returns>
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
                else
                {
                    mLastDocGroupFlag = 0;
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

        /// <summary>
        /// 模板级别选择后事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void advTempDetails_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
        {
            if (advTempDetails.SelectedNode != null)
            {
                mOrderModelHead = advTempDetails.SelectedNode.Tag as IPD_OrderModelHead;
                InvokeController("GetOrderTempDetail", mOrderModelHead.ModelHeadID);
            }
        }

        /// <summary>
        /// 选中事件
        /// </summary>
        /// <param name="dt">网格数据</param>
        /// <param name="rowid">当前行ID</param>
        private void XdControl(DataTable dt, int rowid)
        {
            string groupid = dt.Rows[rowid]["groupid"].ToString().Trim();
            int beginNum = 0;
            int endNum = 0;
            this.FindModelBeginEnd(rowid, dt, ref beginNum, ref endNum);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["groupid"].ToString().Trim() == groupid)
                {
                    this.CellXD(i, true, dt);
                }
            }
        }

        /// <summary>
        /// 选中和反选
        /// </summary>
        /// <param name="rowid">当前行</param>
        /// <param name="b">true 反选 false全选</param>
        /// <param name="dt">数据源</param>
        private void CellXD(int rowid, bool b, DataTable dt)
        {
            if (rowid > -1)
            {
                if (Convert.ToInt32(dt.Rows[rowid]["Sel"]) == 1)
                {
                    if (b)
                    {
                        dt.Rows[rowid]["Sel"] = 0;
                    }
                }
                else
                {
                    dt.Rows[rowid]["Sel"] = 1;
                }
            }
        }

        /// <summary>
        /// 找到所在行的一组医嘱的起始和结束行
        /// </summary>
        /// <param name="nrow">行号</param>
        /// <param name="myTb">数据</param>
        /// <param name="beginNum">开始行号</param>
        /// <param name="endNum">结束行号</param>
        private void FindModelBeginEnd(int nrow, DataTable myTb, ref int beginNum, ref int endNum)
        {
            int i = 0;
            beginNum = nrow;
            endNum = nrow;
            string groupid = myTb.Rows[nrow]["groupid"].ToString();
            for (i = nrow; i <= myTb.Rows.Count - 1; i++)
            {
                if (myTb.Rows[i]["groupid"].ToString() == groupid)
                {
                    endNum = i;
                }
                else
                {
                    break;
                }

                if (i + 1 == myTb.Rows.Count)
                {
                    break;
                }
            }

            for (i = nrow - 1; i >= 0; i--)
            {
                if (i == 0 && myTb.Rows[i]["groupid"].ToString() == groupid)
                {
                    beginNum = 0;
                    break;
                }

                if (myTb.Rows[i]["groupid"].ToString() == groupid)
                {
                    beginNum = i;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 长嘱模板CellClick事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgvLongModel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvLongModel == null || dgvLongModel.Rows.Count == 0)
            //{
            //    return;
            //}
            //if (e.ColumnIndex == 0)
            //{
            //    int rowid = e.RowIndex;
            //    int brow = rowid;
            //    int erow = rowid;
            //    if (rowid < 0)
            //    {
            //        return;
            //    }
            //    DataTable dtt = (DataTable)dgvLongModel.DataSource;
            //    XdControl(dtt, rowid);
            //}
        }

        /// <summary>
        /// 临嘱模板CellClick事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgvTempModel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvTempModel == null || dgvTempModel.Rows.Count == 0)
            //{
            //    return;
            //}
            //if (e.ColumnIndex == 0)
            //{
            //    int rowid = e.RowIndex;
            //    int brow = rowid;
            //    int erow = rowid;
            //    if (rowid < 0)
            //    {
            //        return;
            //    }
            //    DataTable dtt = (DataTable)dgvTempModel.DataSource;
            //    XdControl(dtt, rowid);
            //}
        }

        /// <summary>
        /// 刷新模板按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnRefreshModel_Click(object sender, EventArgs e)
        {
            if (radDept.Checked)
            {
                InvokeController("GetOrderTempList", 1);
            }

            if (radAll.Checked)
            {
                InvokeController("GetOrderTempList", 0);
            }

            if (radSelf.Checked)
            {
                InvokeController("GetOrderTempList", 2);
            }
        }

        /// <summary>
        /// 模板级别双击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void advTempDetails_DoubleClick(object sender, EventArgs e)
        {
            if (advTempDetails.SelectedNode == null)
            {
                return;
            }

            DataTable dt = new DataTable();
            if (TabControlOrder.SelectedTabIndex == 0)
            {
                dt = (DataTable)this.dgvLongModel.DataSource;
            }
            else if (TabControlOrder.SelectedTabIndex == 1)
            {
                dt = (DataTable)this.dgvTempModel.DataSource;
            }

            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }

            List<OrderRecord> list = new List<OrderRecord>();
            int beginNum = 0;
            int endNum = 0;
            for (int index = 0; index < dt.Rows.Count; index++)
            {
                list = new List<OrderRecord>();
                FindModelBeginEnd(index, dt, ref beginNum, ref endNum);
                for (int i = beginNum; i <= endNum; i++)
                {
                    OrderRecord record = new OrderRecord();
                    record = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<OrderRecord>(dt, i);
                    record.Frequency = dt.Rows[i]["Frenquency"] == DBNull.Value ? string.Empty : dt.Rows[i]["Frenquency"].ToString();
                    if (i == beginNum)
                    {
                        record.GroupFlag = 1;
                    }
                    else
                    {
                        record.GroupFlag = 0;
                    }

                    list.Add(record);
                }

                if (TabControlOrder.SelectedTabIndex == 0)
                {
                    TabControlOrder.SelectedTabIndex = 0;
                    LongOrderControl.AddTempOrder(list);
                }
                else if (TabControlOrder.SelectedTabIndex == 1)
                {
                    TabControlOrder.SelectedTabIndex = 1;
                    TempOrderControl.AddTempOrder(list);
                }

                index = endNum;
            }          
        }
      
        /// <summary>
        /// 长嘱双击一组应用
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgvLongModel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TabControlOrder.SelectedTabIndex = 0;
            if (dgvLongModel == null || dgvLongModel.Rows.Count == 0)
            {
                return;
            }

            int rowid = dgvLongModel.CurrentCell.RowIndex;
            if (rowid < 0)
            {
                return;
            }

            List<OrderRecord> list = new List<OrderRecord>();
            DataTable dt = (DataTable)dgvLongModel.DataSource;
            int beginNum = 0;
            int endNum = 0;
            FindModelBeginEnd(rowid, dt, ref beginNum, ref endNum);
            for (int i = beginNum; i <= endNum; i++)
            {
                // dt.Rows[i]["Sel"] = 1;
                OrderRecord record = new OrderRecord();
                record = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<OrderRecord>(dt, i);
                record.Frequency = dt.Rows[i]["Frenquency"] == DBNull.Value ?string.Empty : dt.Rows[i]["Frenquency"].ToString();
                if (i == beginNum)
                {
                    record.GroupFlag = 1;
                }
                else
                {
                    record.GroupFlag = 0;
                }

                list.Add(record);
            }

            LongOrderControl.AddTempOrder(list);
        }

        /// <summary>
        /// 临嘱双击一组应用
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgvTempModel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TabControlOrder.SelectedTabIndex = 1;
            if (dgvTempModel == null || dgvTempModel.Rows.Count == 0)
            {
                return;
            }

            int rowid = dgvTempModel.CurrentCell.RowIndex;
            if (rowid < 0)
            {
                return;
            }

            List<OrderRecord> list = new List<OrderRecord>();
            DataTable dt = (DataTable)dgvTempModel.DataSource;
            int beginNum = 0;
            int endNum = 0;
            FindModelBeginEnd(rowid, dt, ref beginNum, ref endNum);
            for (int i = beginNum; i <= endNum; i++)
            {
                // dt.Rows[i]["Sel"] = 1;
                OrderRecord record = new OrderRecord();
                record = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<OrderRecord>(dt, i);
                record.Frequency = dt.Rows[i]["Frenquency"] == DBNull.Value ?string.Empty : dt.Rows[i]["Frenquency"].ToString();
                if (i == beginNum)
                {
                    record.GroupFlag = 1;
                }
                else
                {
                    record.GroupFlag = 0;
                }

                list.Add(record);
            }

            TempOrderControl.AddTempOrder(list);
        }

        /// <summary>
        /// 右键应用按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void 应用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (TabControlModel.SelectedTabIndex == 0)
            {
                dt = (DataTable)this.dgvLongModel.DataSource;
            }
            else if (TabControlModel.SelectedTabIndex == 1)
            {
                dt = (DataTable)this.dgvTempModel.DataSource;
            }

            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }

            List<OrderRecord> list = new List<OrderRecord>();
            int beginNum = 0;
            int endNum = 0;
            for (int index = 0; index < dt.Rows.Count; index++)
            {
                if (Convert.ToInt32(dt.Rows[index]["Sel"]) == 1)
                {
                    FindModelBeginEnd(index, dt, ref beginNum, ref endNum);
                    for (int i = beginNum; i <= endNum; i++)
                    {
                        dt.Rows[i]["Sel"] = 1;
                        OrderRecord record = new OrderRecord();
                        record = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<OrderRecord>(dt, i);
                        record.Frequency = dt.Rows[i]["Frenquency"] == DBNull.Value ?string.Empty : dt.Rows[i]["Frenquency"].ToString();
                        if (i == beginNum)
                        {
                            record.GroupFlag = 1;
                        }
                        else
                        {
                            record.GroupFlag = 0;
                        }

                        list.Add(record);
                    }

                    index = endNum + 1;
                }
            }

            if (TabControlModel.SelectedTabIndex == 0)
            {
                TabControlOrder.SelectedTabIndex = 0;
                LongOrderControl.AddTempOrder(list);
            }
            else if (TabControlModel.SelectedTabIndex == 1)
            {
                TabControlOrder.SelectedTabIndex = 1;
                TempOrderControl.AddTempOrder(list);
            }
        }

        /// <summary>
        /// 右键全选按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControlModel.SelectedTabIndex == 0)
            {
                for (int i = 0; i < this.dgvLongModel.RowCount; i++)
                {
                    CellXD(i, false, (DataTable)dgvLongModel.DataSource);
                }
            }
            else if (TabControlModel.SelectedTabIndex == 1)
            {
                for (int i = 0; i < this.dgvTempModel.RowCount; i++)
                {
                    CellXD(i, false, (DataTable)dgvTempModel.DataSource);
                }
            }
        }

        /// <summary>
        /// 右键全选按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void 反选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControlModel.SelectedTabIndex == 0)
            {
                for (int i = 0; i < this.dgvLongModel.RowCount; i++)
                {
                    CellXD(i, true, (DataTable)dgvLongModel.DataSource);
                }
            }
            else if (TabControlModel.SelectedTabIndex == 1)
            {
                for (int i = 0; i < this.dgvTempModel.RowCount; i++)
                {
                    CellXD(i, true, (DataTable)dgvTempModel.DataSource);
                }
            }
        }
        #endregion

        #region 申请单
        /// <summary>
        /// 绑定申请头表数据
        /// </summary>
        /// <param name="dt">申请头表数据</param>
        public void BindApplyHead(DataTable dt)
        {
            dgApplyHead.DataSource = dt;
        }

        /// <summary>
        /// 申请单CellClick事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgApplyHead_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex <= -1 || e.RowIndex <= -1)
            {
                return;
            }

            string buttonText = this.dgApplyHead.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
            if (buttonText == "移除")
            {
                if (MessageBox.Show("确定要移除吗？", "移除前确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    DataTable dt = this.dgApplyHead.DataSource as DataTable;
                    DataRow dr = dt.Rows[e.RowIndex];
                    DataTable newdt = (InvokeController("GetApplyStatus", Convert.ToInt32(dr["ApplyHeadID"])) as DataTable);
                    if (newdt != null)
                    {
                        if (newdt.Rows.Count > 0)
                        {
                            if (newdt.Rows[0]["ApplyStatus"].ToString() == "1" || newdt.Rows[0]["ApplyStatus"].ToString() == "2")
                            {
                                MessageBoxEx.Show("该记录已收费或确费不能移除");
                            }
                            else
                            {
                                InvokeController("DelApplyHead", dr["ApplyHeadID"].ToString());
                            }
                        }
                    }
                    else
                    {
                        MessageBoxEx.Show("该记录无法删除");
                    }
                }
            }
        }

        /// <summary>
        /// 申请单网格CellDoubleClick事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgApplyHead_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dt = dgApplyHead.DataSource as DataTable;
            if (dgApplyHead.CurrentRow != null)
            {
                DataRow dr = dt.Rows[dgApplyHead.CurrentRow.Index];
                InvokeController("ShowApply", dr["ApplyHeadID"].ToString(), dr["ApplyType"].ToString(), dr["ApplyStatu"].ToString(), dr["IsReturns"].ToString());
            }
        }

        #endregion    

        #region 诊断管理
        /// <summary>
        /// 绑定诊断页面
        /// </summary>
        /// <param name="dtDiag">诊断</param>
        /// <param name="dtClass">诊断类型</param>
        /// <param name="dtDiagCode">诊断明细</param>
        public void LoadDiagInfo(DataTable dtDiag, DataTable dtClass, DataTable dtDiagCode)
        {
            //绑定诊断网格
            dgDiagnosis.DataSource = dtDiag;
            if (dtDiag.Rows.Count > 0)
            {
                DiagFlag = 1;   //诊断处于编辑状态
            }
            else
            {
                DiagFlag = 0;   //诊断处于新增状态
            }
            //绑定诊断类型
            cbbDiagClass.DisplayMember = "name";
            cbbDiagClass.ValueMember = "ID";
            cbbDiagClass.DataSource = dtClass;

            //绑定诊断明细
            tbcDiagName.DisplayField = "Name";
            tbcDiagName.MemberField = "ID";
            tbcDiagName.CardColumn = "ICDCode|ICD编码|60,Name|诊断名称|auto";
            tbcDiagName.QueryFieldsString = "ICDCode,Name,PYCode,WBCode";
            tbcDiagName.ShowCardWidth = 200;
            tbcDiagName.ShowCardDataSource = dtDiagCode;

            cmbCondition.SelectedIndex = 0;
        }

        /// <summary>
        /// 诊断网格CurrentCellChanged事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgDiagnosis_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgDiagnosis.CurrentCell != null)
            {
                int rowIndex = dgDiagnosis.CurrentCell.RowIndex;
                DataTable dt = (DataTable)dgDiagnosis.DataSource;
                cbbDiagClass.SelectedValue = Convert.ToInt32(dt.Rows[rowIndex]["DiagnosisClass"]);
                if (!string.IsNullOrEmpty(dt.Rows[rowIndex]["ICDCode"].ToString()))
                {
                    tbcDiagName.Text = Convert.ToString(dt.Rows[rowIndex]["DiagnosisName"]);
                    chbICD.Checked = true;
                    tbcDiagName.Enabled = true;
                    txtCusDiag.Enabled = false;
                }
                else
                {
                    txtCusDiag.Text = Convert.ToString(dt.Rows[rowIndex]["DiagnosisName"]);
                    chbICD.Checked = false;
                    tbcDiagName.Enabled = false;
                    txtCusDiag.Enabled = true;
                }

                tbcDiagName.Tag = Convert.ToString(dt.Rows[rowIndex]["ICDCode"]);
                cmbCondition.SelectedIndex =Convert.ToInt32(string.IsNullOrEmpty(dt.Rows[rowIndex]["Effect"].ToString())?1: dt.Rows[rowIndex]["Effect"])-1;
                chbMain.Checked = (Convert.ToUInt32(dt.Rows[rowIndex]["Main"]) == 1) ? true : false;
                DiagFlag = 1;   //诊断处于编辑状态
            }
            else
            {
                DiagFlag = 0;   //诊断处于新增状态
            }
        }

        /// <summary>
        /// 保存诊断信息
        /// </summary>
        public void SaveDiagInfo()
        {
            if (chbICD.Checked == true)
            {
                if (string.IsNullOrEmpty(tbcDiagName.Text) == true)
                {
                    MessageBoxEx.Show("诊断名称不能为空");
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtCusDiag.Text) == true)
                {
                    MessageBoxEx.Show("诊断名称不能为空");
                    return;
                }
            }

            int id = 0;

            if (DiagFlag == 1)
            {
                if (dgDiagnosis.CurrentCell != null)
                {
                    int rowIndex = dgDiagnosis.CurrentCell.RowIndex;
                    DataTable dt = (DataTable)dgDiagnosis.DataSource;
                    id = Convert.ToInt32(dt.Rows[rowIndex]["ID"]);
                }
            }

            int main = (chbMain.Checked) ? 1 : 0;
            int diagID = Convert.ToInt32(tbcDiagName.MemberValue);
            if (Convert.ToBoolean(InvokeController("CheckDiagnosisInfo", DiagFlag, main, tbcDiagName.Text, id)) == false)
            {
                MessageBoxEx.Show("已经存在主诊断，不能再录主诊断");
                return;
            }

            string diagName =string.Empty;
            string icdcode =string.Empty;
            if (chbICD.Checked == false)
            {
                diagName = txtCusDiag.Text;
            }
            else
            {
                diagName = tbcDiagName.Text;
                icdcode = tbcDiagName.Tag.ToString();
            }

            int resFlag = Convert.ToInt32(InvokeController("SaveDiagInfo", cbbDiagClass.SelectedValue, main, diagID, diagName, icdcode, (cmbCondition.SelectedIndex+1).ToString(), chbICD.Checked, id));
            InvokeController("LoadDiagnosisInfo");
            if (DiagFlag == 1)
            {
                setGridSelectIndex(dgDiagnosis);
            }
        }

        /// <summary>
        /// 诊断选项卡选择事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="SelectedValue">选择行</param>
        private void tbcDiagName_AfterSelectedRow(object sender, object SelectedValue)
        {
            tbcDiagName.Tag = Convert.ToString(((DataRow)SelectedValue)["ICDCode"]);
        }

        /// <summary>
        /// ICD编码选中改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void chbICD_CheckValueChanged(object sender, EventArgs e)
        {
            if (chbICD.Checked == true)
            {
                tbcDiagName.Enabled = true;
                txtCusDiag.Enabled = false;
            }
            else
            {
                txtCusDiag.Enabled = true;
                tbcDiagName.Enabled = false;
            }
        }
        #endregion

        /// <summary>
        /// 删除诊断按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnDel_Click_1(object sender, EventArgs e)
        {
            if (dgDiagnosis.CurrentCell != null)
            {
                if (MessageBoxEx.Show("确定要删除该诊断吗？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                int rowIndex = dgDiagnosis.CurrentCell.RowIndex;
                DataTable dt = (DataTable)dgDiagnosis.DataSource;
                int id = Convert.ToInt32(dt.Rows[rowIndex]["ID"]);
                InvokeController("DeleteDiagInfo", id);
                InvokeController("LoadDiagnosisInfo");
            }
        }

        /// <summary>
        /// 所有医嘱选中改变事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void radAllOrder_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            LongOrderControl.IsShowAllOrder = radAllOrder.Checked;
            TempOrderControl.IsShowAllOrder = radAllOrder.Checked;
            if (TabControlOrder.SelectedTabIndex == 0)
            {
                TempOrderControl.RefreshOrder();
                LongOrderControl.RefreshOrder();
            }
            else if (TabControlOrder.SelectedTabIndex == 1)
            {
                LongOrderControl.RefreshOrder();
                TempOrderControl.RefreshOrder();
            }
        }

        /// <summary>
        /// 新增诊断按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnAddDiag_Click(object sender, EventArgs e)
        {
            tbcDiagName.Text =string.Empty;
            chbICD.Checked = true;
            txtCusDiag.Enabled = false;
            tbcDiagName.Enabled = true;
            txtCusDiag.Text =string.Empty;
            chbMain.Checked = false;
            cmbCondition.SelectedIndex = 3;
            DiagFlag = 0;  //诊断进入新增状态
            cbbDiagClass.Focus();
        }

        /// <summary>
        /// 保存诊断按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnSaveDiag_Click(object sender, EventArgs e)
        {
            cbbDiagClass.Focus();
            SaveDiagInfo();
        }
    
        /// <summary>
        /// 长嘱药品信息显示
        /// </summary>
        /// <param name="rowDrug">药品信息</param>
        /// <param name="execDeptName">执行科室</param>
        private void LongOrderControl_DetailShowlinkage(DataRow rowDrug, string execDeptName)
        {
            DrugDetailShow(rowDrug, execDeptName);
        }

        /// <summary>
        /// 长嘱药品信息显示
        /// </summary>
        /// <param name="rowDrug">药品信息</param>
        /// <param name="execDeptName">执行科室</param>
        private void TempOrderControl_DetailShowlinkage(DataRow rowDrug, string execDeptName)
        {
            DrugDetailShow(rowDrug, execDeptName);
        }

        /// <summary>
        /// 药品详细信息显示
        /// </summary>
        /// <param name="drug">药品信息</param>
        /// <param name="execDeptName">执行科室</param>
        private void DrugDetailShow(DataRow drug, string execDeptName)
        {
            if (drug == null)
            {
                this.lbdrugname.Text =string.Empty;
                this.lbdrugunit.Text =string.Empty;
                this.lbdrugtype.Text =string.Empty;              
                this.lbdrugprice.Text =string.Empty;
                this.lbdrugdept.Text =string.Empty;
                this.lbaddress.Text =string.Empty;
                this.lbspec.Text =string.Empty;
                this.cbdu.Checked = false;
                this.cbma.Checked = false;
                this.cbgu.Checked = false;
                this.cbjs.Checked = false;
            }
            else
            {
                this.lbdrugname.Text = drug["ItemName"].ToString();
                this.lbdrugunit.Text = drug["MiniUnitName"].ToString();
                this.lbdrugtype.Text = drug["MedicareName"].ToString();               
                this.lbdrugprice.Text =Convert.ToDecimal( drug["SellPrice"]).ToString("0.00");
                this.lbdrugdept.Text = execDeptName;
                this.lbaddress.Text = drug["Address"].ToString();
                this.lbspec.Text = drug["Standard"].ToString();
                this.cbdu.Checked = drug["VirulentFlag"].ToString() == "1" ? true : false;
                this.cbma.Checked = drug["NarcoticFlag"].ToString() == "1" ? true : false;
                this.cbgu.Checked = drug["CostlyFlag"].ToString() == "1" ? true : false;
                this.cbjs.Checked = drug["LunacyFlag"].ToString() == "1" ? true : false;
            }
        }

        private void 病案首页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvPatInfo.DataSource;
            int rowindex = dgvPatInfo.CurrentCell.RowIndex;
            InvokeController("ShowMedicalCasePage", Convert.ToInt32(dt.Rows[rowindex]["patlistid"]), dt.Rows[rowindex]["PatDeptID"].ToString(), dt.Rows[rowindex]["deptName"].ToString());
        }
    }
}