using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MemberManage;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    public partial class FrmDischargeSettlement : BaseFormBusiness, IDischargeSettlement
    {
        /// <summary>
        /// 病人入院科室
        /// </summary>
        public int DeptID
        {
            get
            {
                return Convert.ToInt32(txtDeptList.MemberValue.ToString());
            }
        }

        /// <summary>
        /// 入院时间
        /// </summary>
        public DateTime EndDateTime
        {
            get
            {
                return chkMakerDate.Checked ? sdtAdmissionTime.Edate.Value.Date : DateTime.MinValue;
            }
        }

        /// <summary>
        /// 病人状态
        /// </summary>
        public string PatStatus
        {
            get
            {
                // 病人状态
                string patStatus = string.Empty;
                if (chkInTheHospital.Checked)
                {
                    // 在床
                    patStatus = "2";
                }

                if (chkDefinedDischarge.Checked)
                {
                    // 出院未结算
                    patStatus = "3";
                }

                return patStatus;
            }
        }

        /// <summary>
        /// 检索条件
        /// </summary>
        public string SeachParm
        {
            get
            {
                return txtSelectParam.Text.Trim();
            }
        }

        /// <summary>
        /// 入院时间
        /// </summary>
        public DateTime StartDateTime
        {
            get
            {
                return chkMakerDate.Checked ? sdtAdmissionTime.Bdate.Value.Date : DateTime.MinValue;
            }
        }

        /// <summary>
        /// 病人登记ID
        /// </summary>
        public int PatListID
        {
            get
            {
                if (grdPatList.CurrentCell != null)
                {
                    int rowIndex = grdPatList.CurrentCell.RowIndex;
                    DataTable patList = grdPatList.DataSource as DataTable;
                    return Convert.ToInt32(patList.Rows[rowIndex]["PatListID"]);
                }

                return 0;
            }
        }

        /// <summary>
        /// 病人住院号
        /// </summary>
        public decimal SerialNumber
        {
            get
            {
                if (grdPatList.CurrentCell != null)
                {
                    int rowIndex = grdPatList.CurrentCell.RowIndex;
                    DataTable patList = grdPatList.DataSource as DataTable;
                    return Convert.ToDecimal(patList.Rows[rowIndex]["SerialNumber"]);
                }

                return 0;
            }
        }

        /// <summary>
        /// 票据号
        /// </summary>
        public string InvoiceNo
        {
            get
            {
                return txtInvoiceNo.Text;
            }

            set
            {
                txtInvoiceNo.Text = value;
            }
        }

        /// <summary>
        /// 结算类型
        /// </summary>
        private int mCostType = 0;

        /// <summary>
        /// 结算类型
        /// </summary>
        public int CostType
        {
            get
            {
                return mCostType;
            }

            set
            {
                mCostType = value;
            }
        }

        /// <summary>
        /// 住院登记病人类型ID
        /// </summary>
        public int PatTypeID
        {
            get
            {
                if (grdPatList.CurrentCell != null)
                {
                    int rowIndex = grdPatList.CurrentCell.RowIndex;
                    DataTable patList = grdPatList.DataSource as DataTable;
                    return Convert.ToInt32(patList.Rows[rowIndex]["PatTypeID"]);
                }

                return 0;
            }
        }

        /// <summary>
        /// 会员账号ID
        /// </summary>
        public int MemberAccountID
        {
            get
            {
                if (grdPatList.CurrentCell != null)
                {
                    int rowIndex = grdPatList.CurrentCell.RowIndex;
                    DataTable patList = grdPatList.DataSource as DataTable;
                    return Convert.ToInt32(patList.Rows[rowIndex]["MemberAccountID"]);
                }

                return 0;
            }
        }

        /// <summary>
        /// 优惠金额计算后的数据
        /// </summary>
        private DiscountInfo mResDiscountInfo = new DiscountInfo();

        /// <summary>
        /// 优惠金额计算后的数据
        /// </summary>
        public DiscountInfo ResDiscountInfo
        {
            get
            {
                return mResDiscountInfo;
            }

            set
            {
                mResDiscountInfo = value;
            }
        }

        /// <summary>
        /// 预交金总额
        /// </summary>
        public decimal DepositFee
        {
            get
            {
                if (!string.IsNullOrEmpty(lblSumDeposit.Text.Trim()))
                {
                    return Convert.ToDecimal(lblSumDeposit.Text.Trim());
                }

                return 0;
            }
        }

        /// <summary>
        /// 住院总费用
        /// </summary>
        public decimal TotalFee
        {
            get
            {
                if (!string.IsNullOrEmpty(lblSumFee.Text.Trim()))
                {
                    return Convert.ToDecimal(lblSumFee.Text.Trim());
                }

                return 0;
            }
        }

        /// <summary>
        /// 病人姓名
        /// </summary>
        public string PatName
        {
            get
            {
                if (grdPatList.CurrentCell != null)
                {
                    int rowIndex = grdPatList.CurrentCell.RowIndex;
                    DataTable patList = grdPatList.DataSource as DataTable;
                    return patList.Rows[rowIndex]["PatName"].ToString();
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// 住院科室ID
        /// </summary>
        public int PatDeptID
        {
            get
            {
                if (grdPatList.CurrentCell != null)
                {
                    int rowIndex = grdPatList.CurrentCell.RowIndex;
                    DataTable patList = grdPatList.DataSource as DataTable;
                    return Convert.ToInt32(patList.Rows[rowIndex]["CurrDeptID"]);
                }

                return 0;
            }
        }

        /// <summary>
        /// 病人入院时间
        /// </summary>
        public DateTime PatEnterHDate
        {
            get
            {
                if (grdPatList.CurrentCell != null)
                {
                    int rowIndex = grdPatList.CurrentCell.RowIndex;
                    DataTable patList = grdPatList.DataSource as DataTable;
                    return Convert.ToDateTime(patList.Rows[rowIndex]["EnterHDate"]);
                }

                return DateTime.Now;
            }
        }

        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID
        {
            get
            {
                if (grdPatList.CurrentCell != null)
                {
                    int rowIndex = grdPatList.CurrentCell.RowIndex;
                    DataTable patList = grdPatList.DataSource as DataTable;
                    return Convert.ToInt32(patList.Rows[rowIndex]["MemberID"]);
                }

                return 0;
            }
        }

        /// <summary>
        /// 会员卡号
        /// </summary>
        public string CardNO
        {
            get
            {
                if (grdPatList.CurrentCell != null)
                {
                    int rowIndex = grdPatList.CurrentCell.RowIndex;
                    DataTable patList = grdPatList.DataSource as DataTable;
                    return patList.Rows[rowIndex]["CardNO"].ToString();
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// 病人列表
        /// </summary>
        private DataTable mPatientDt = new DataTable();

        /// <summary>
        /// 病人列表
        /// </summary>
        public DataTable PatientDt
        {
            get
            {
                //if (grdPatList.CurrentCell != null)
                //{
                //    int rowIndex = grdPatList.CurrentCell.RowIndex;
                //    DataTable tempDt = grdPatList.DataSource as DataTable;
                //    mPatientDt = tempDt.Clone();
                //    mPatientDt.Rows.Add(tempDt.Rows[rowIndex].ItemArray);
                //}

                return mPatientDt;
            }
        }

        /// <summary>
        /// 费用分类列表
        /// </summary>
        private DataTable patFeedt = new DataTable();

        /// <summary>
        /// 费用分类列表
        /// </summary>
        public DataTable PatFeeDt
        {
            get
            {
                return patFeedt;
            }
        }

        /// <summary>
        /// 窗体构造函数
        /// </summary>
        public FrmDischargeSettlement()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体打开之前加载数据操作
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmDischargeSettlement_OpenWindowBefore(object sender, EventArgs e)
        {
            sdtAdmissionTime.Bdate.Value = DateTime.Now;
            sdtAdmissionTime.Edate.Value = DateTime.Now;
            bindGridSelectIndex(grdPatList);
            // 获取科室列表
            InvokeController("GetDeptList");
            InvokeController("GetInvoiceNO", false);
        }

        /// <summary>
        /// 查询病人列表
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            InvokeController("GetPatientList");
        }

        /// <summary>
        /// 勾选日期CheckBox事件
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void chkMakerDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMakerDate.Checked)
            {
                sdtAdmissionTime.Enabled = true;
                sdtAdmissionTime.Bdate.Value = DateTime.Now;
                sdtAdmissionTime.Edate.Value = DateTime.Now;
            }
            else
            {
                sdtAdmissionTime.Enabled = false;
            }
        }

        /// <summary>
        /// 选中病人
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void grdPatList_CurrentCellChanged(object sender, EventArgs e)
        {
            if (grdPatList.CurrentCell != null)
            {
                int rowIndex = grdPatList.CurrentCell.RowIndex;
                DataTable tempDt = grdPatList.DataSource as DataTable;
                lblSerialNumber.Text = tempDt.Rows[rowIndex]["SerialNumber"].ToString();  // 住院号
                lblPatName.Text = tempDt.Rows[rowIndex]["PatName"].ToString();  // 病人名
                lblPatType.Text = tempDt.Rows[rowIndex]["PatTypeName"].ToString(); // 病人类型
                lblPatDept.Text = tempDt.Rows[rowIndex]["DeptName"].ToString();// 科室名
                lblEnterHDate.Text = Convert.ToDateTime(tempDt.Rows[rowIndex]["EnterHDate"]).ToString("yyyy-MM-dd HH:mm");  // 入院日期
                int patStatus = Convert.ToInt32(tempDt.Rows[rowIndex]["Status"].ToString()); // 病人状态
                // 出院未结算病人才显示出院日期
                if (patStatus == 3)
                {
                    lblLeaveHDate.Text = Convert.ToDateTime(tempDt.Rows[rowIndex]["LeaveHDate"]).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    lblLeaveHDate.Text = string.Empty;
                }
                // 加载病人费用和预交金数据
                InvokeController("StatisticsFeeByFeeType");
            }
        }

        /// <summary>
        /// 注册键盘事件
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmDischargeSettlement_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F6:
                    btnHalfwaySettlement_Click(null, null);
                    break;
                case Keys.F7:
                    btnDischargeSettlement_Click(null, null);
                    break;
                case Keys.F8:
                    btnSettlementOfArrears_Click(null, null);
                    break;
            }
        }

        /// <summary>
        /// 中途结算
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnHalfwaySettlement_Click(object sender, EventArgs e)
        {
            if (grdPatList.CurrentCell != null)
            {
                DataTable feedt = grdFeeTypeList.DataSource as DataTable;
                if (feedt == null || feedt.Rows.Count <= 0)
                {
                    InvokeController("MessageShow", "当前病人已结算或没有需要结算的费用！");
                    return;
                }
                
                int rowIndex = grdPatList.CurrentCell.RowIndex;
                DataTable patListDt = grdPatList.DataSource as DataTable;
                mPatientDt = patListDt.Clone();
                mPatientDt.Rows.Add(patListDt.Rows[rowIndex].ItemArray);
                int patStatus = Convert.ToInt32(patListDt.Rows[rowIndex]["Status"].ToString()); // 病人状态
                if (patStatus != 2)
                {
                    InvokeController("MessageShow", "请不要对已定义出院的病人进行中途结算！");
                    return;
                }

                mCostType = 1;
                patFeedt = grdFeeTypeList.DataSource as DataTable;
                InvokeController("ShowPayMentInfo");
            }
            else
            {
                InvokeController("MessageShow", "请选择需要结算的病人！");
            }
        }

        /// <summary>
        /// 出院结算
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnDischargeSettlement_Click(object sender, EventArgs e)
        {
            if (grdPatList.CurrentCell != null)
            {
                int rowIndex = grdPatList.CurrentCell.RowIndex;
                DataTable patListDt = grdPatList.DataSource as DataTable;
                mPatientDt = patListDt.Clone();
                mPatientDt.Rows.Add(patListDt.Rows[rowIndex].ItemArray);
                int patStatus = Convert.ToInt32(patListDt.Rows[rowIndex]["Status"].ToString()); // 病人状态
                if (patStatus != 3)
                {
                    InvokeController("MessageShow", "请不要对未定义出院的病人进行出院结算！");
                    return;
                }

                mCostType = 2;
                patFeedt = grdFeeTypeList.DataSource as DataTable;
                InvokeController("ShowPayMentInfo");
            }
            else
            {
                InvokeController("MessageShow", "请选择需要结算的病人！");
            }
        }

        /// <summary>
        /// 欠费结算
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnSettlementOfArrears_Click(object sender, EventArgs e)
        {
            if (grdPatList.CurrentCell != null)
            {
                int rowIndex = grdPatList.CurrentCell.RowIndex;
                DataTable patListDt = grdPatList.DataSource as DataTable;
                mPatientDt = patListDt.Clone();
                mPatientDt.Rows.Add(patListDt.Rows[rowIndex].ItemArray);
                int patStatus = Convert.ToInt32(patListDt.Rows[rowIndex]["Status"].ToString()); // 病人状态
                if (patStatus != 3)
                {
                    InvokeController("MessageShow", "请不要对未定义出院的病人进行欠费结算！");
                    return;
                }

                mCostType = 3;
                patFeedt = grdFeeTypeList.DataSource as DataTable;
                InvokeController("ShowPayMentInfo");
            }
            else
            {
                InvokeController("MessageShow", "请选择需要结算的病人！");
            }
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="patList">病人列表</param>
        public void Binding_GrdPatList(DataTable patList)
        {
            if (patList.Rows.Count <= 0)
            {
                lblSerialNumber.Text = string.Empty;
                lblPatName.Text = string.Empty;
                lblSumDeposit.Text = string.Empty;
                lblPatType.Text = string.Empty;
                lblPatDept.Text = string.Empty;
                lblSumFee.Text = string.Empty;
                lblEnterHDate.Text = string.Empty;
                lblLeaveHDate.Text = string.Empty;
                lblBalance.Text = string.Empty;
                grdPayDetailList.DataSource = new DataTable();
                grdFeeTypeList.DataSource = new DataTable();
            }

            grdPatList.DataSource = patList;
            if (patList != null && patList.Rows.Count > 0)
            {
                setGridSelectIndex(grdPatList);
            }
        }

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="deptDt">科室列表</param>
        public void Bind_txtDeptList(DataTable deptDt)
        {
            txtDeptList.MemberField = "DeptId";
            txtDeptList.DisplayField = "Name";
            txtDeptList.CardColumn = "Name|名称|auto";
            txtDeptList.QueryFieldsString = "Name,Pym,Wbm,Szm";
            txtDeptList.ShowCardWidth = 295;
            txtDeptList.ShowCardDataSource = deptDt;
            txtDeptList.MemberValue = -1;
        }

        /// <summary>
        /// 绑定费用分类表列表
        /// </summary>
        /// <param name="feeTypeDt">费用分类表列表</param>
        public void Bind_FeeTypeList(DataTable feeTypeDt)
        {
            grdFeeTypeList.DataSource = feeTypeDt;
        }

        /// <summary>
        /// 绑定病人预交金列表
        /// </summary>
        /// <param name="payList">预交金列表</param>
        /// <param name="sumFee">住院总金额</param>
        /// <param name="sumPay">预交金总额</param>
        public void Bind_PrepaidPaymentList(DataTable payList, decimal sumFee, decimal sumPay)
        {
            grdPayDetailList.DataSource = payList;
            SetGridColor();
            // 预交金总额
            lblSumDeposit.Text = string.Format("{0:N}", sumPay);
            // 住院总费用
            lblSumFee.Text = string.Format("{0:N}", sumFee);
            // 余额
            lblBalance.Text = string.Format("{0:N}", sumPay - sumFee);
        }

        /// <summary>
        /// 显示上一次的结算信息
        /// </summary>
        /// <param name="costData">上一次的结算信息</param>
        public void SetLastPatCostData(string costData)
        {
            if (!string.IsNullOrEmpty(costData))
            {
                lblPayInfo.Text = costData;
            }
        }

        /// <summary>
        /// 调整票据号
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnAdjustment_Click(object sender, EventArgs e)
        {
            InvokeController("ShowAdjustInvoice");
        }

        /// <summary>
        /// 设置网格行字体颜色
        /// </summary>
        public void SetGridColor()
        {
            foreach (DataGridViewRow row in grdPayDetailList.Rows)
            {
                short status_flag = Convert.ToInt16(row.Cells["States"].Value);

                Color foreColor = Color.Blue;
                switch (status_flag)
                {
                    case 1:
                    case 2:
                        foreColor = Color.Green;
                        break;
                }

                grdPayDetailList.SetRowColor(row.Index, foreColor, true);
            }
        }
    }
}
