using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using EfwControls.Common;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    /// <summary>
    /// 预交金管理
    /// </summary>
    public partial class FrmPrepaidPaymentSys : BaseFormBusiness, IPrepaidPaymentSys
    {
        #region "属性"

        /// <summary>
        /// 科室ID 
        /// </summary>
        public int DeptID
        {
            get
            {
                return int.Parse(txtDeptList.MemberValue.ToString());
            }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDateTime
        {
            get
            {
                return chkMakerDate.Checked ? sdtpMakerDate.Edate.Value.Date : DateTime.MinValue;
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
                    // 新入院
                    patStatus = "1";
                }

                if (chkDefinedDischarge.Checked)
                {
                    // 出院未结算
                    patStatus = "3";
                }

                if (chkLeaveHospital.Checked)
                {
                    // 出院已结算
                    patStatus = "4";
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
                return txtSeleceParm.Text.Trim();
            }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDateTime
        {
            get
            {
                return chkMakerDate.Checked ? sdtpMakerDate.Bdate.Value.Date : DateTime.MinValue;
            }
        }

        /// <summary>
        /// 住院流水号
        /// </summary>
        public decimal SerialNumber
        {
            get
            {
                if (grdPatList.CurrentCell != null)
                {
                    int rowIndex = grdPatList.CurrentCell.RowIndex;
                    DataTable dt = grdPatList.DataSource as DataTable;
                    return Convert.ToDecimal(dt.Rows[rowIndex]["SerialNumber"]);
                }
                else
                {
                    return 0;
                }
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
                    DataTable dt = grdPatList.DataSource as DataTable;
                    return int.Parse(dt.Rows[rowIndex]["MemberID"].ToString());
                }
                else
                {
                    return 0;
                }
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
                    DataTable dt = grdPatList.DataSource as DataTable;
                    // 病人登记ID
                    return int.Parse(dt.Rows[rowIndex]["PatListID"].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 病人登记科室ID
        /// </summary>
        public int PatDeptID
        {
            get
            {
                if (grdPatList.CurrentCell != null)
                {
                    int rowIndex = grdPatList.CurrentCell.RowIndex;
                    DataTable dt = grdPatList.DataSource as DataTable;
                    // 病人登记科室ID
                    return int.Parse(dt.Rows[rowIndex]["CurrDeptID"].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        #endregion

        #region "事件"

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmPrepaidPaymentSys()
        {
            InitializeComponent();
            frmPayment.AddItem(txtDeptList, string.Empty);
            frmPayment.AddItem(txtSeleceParm, string.Empty);
            frmPayment.AddItem(btnSeach, string.Empty);
        }

        /// <summary>
        /// 检索病人列表
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnSeach_Click(object sender, EventArgs e)
        {
            InvokeController("GetPatientList");
            grdPayDetailList.DataSource = new DataTable();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmPrepaidPaymentSys_OpenWindowBefore(object sender, EventArgs e)
        {
            bindGridSelectIndex(grdPayDetailList);
            bindGridSelectIndex(grdPatList);
            sdtpMakerDate.Bdate.Value = DateTime.Now;
            sdtpMakerDate.Edate.Value = DateTime.Now;
            // 加载所有住院临床科室
            InvokeController("GetDeptList");
            txtDeptList.Focus();
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
                sdtpMakerDate.Enabled = true;
                sdtpMakerDate.Bdate.Value = DateTime.Now;
                sdtpMakerDate.Edate.Value = DateTime.Now;
            }
            else
            {
                sdtpMakerDate.Enabled = false;
            }
        }

        /// <summary>
        /// 注册功能快捷键
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmPrepaidPaymentSys_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F6:
                    btnCharge_Click(sender, e);
                    break;
                case Keys.F7:
                    btnVoided_Click(sender, e);
                    break;
                case Keys.F8:
                    btnPrint_Click(sender, e);
                    break;
            }
        }

        /// <summary>
        /// 弹出预交金缴纳界面
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnCharge_Click(object sender, EventArgs e)
        {
            if (grdPatList.CurrentCell != null)
            {
                int rowIndex = grdPatList.CurrentCell.RowIndex;
                DataTable tempDt = grdPatList.DataSource as DataTable;
                if (Convert.ToInt32(tempDt.Rows[rowIndex]["Status"]) == 4)
                {
                    InvokeController("MessageShow", "当前病人已出院，无法进行预交金收退操作！");
                    return;
                }
            }

            if (string.IsNullOrEmpty(lblPatName.Text.Trim()) ||
                string.IsNullOrEmpty(lblSerialNumber.Text.Trim()))
            {
                InvokeController("MessageShow", "请选择缴纳预交金的病人！");
                return;
            }

            InvokeController("SavePatientInfo");
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (grdPayDetailList.CurrentCell != null)
            {
                var rowIndex = grdPayDetailList.CurrentRow.Index;
                var dataSource = grdPayDetailList.DataSource as DataTable;
                int depositID = (int)dataSource.Rows[rowIndex]["DepositID"];
                InvokeController("PrintDepositInfo", depositID);
            }
        }

        /// <summary>
        /// 预交金退费
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnVoided_Click(object sender, EventArgs e)
        {
            if (grdPatList.CurrentCell != null)
            {
                int index = grdPatList.CurrentCell.RowIndex;
                DataTable tempDt = grdPatList.DataSource as DataTable;
                if (Convert.ToInt32(tempDt.Rows[index]["Status"]) == 4)
                {
                    InvokeController("MessageShow", "当前病人已出院，无法进行预交金收退操作！");
                    return;
                }
            }

            if (grdPayDetailList.CurrentCell == null)
            {
                InvokeController("MessageShow", "请选择需要退费的交费记录！");
                return;
            }

            int rowIndex = grdPayDetailList.CurrentCell.RowIndex;
            DataTable dt = grdPayDetailList.DataSource as DataTable;
            InvokeController("Refund", int.Parse(dt.Rows[rowIndex]["DepositID"].ToString()));
        }

        /// <summary>
        /// 关闭预交金管理界面
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        #endregion

        #region "私有方法"

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
            txtDeptList.ShowCardWidth = 193;
            txtDeptList.ShowCardDataSource = deptDt;
            txtDeptList.MemberValue = -1;
        }

        /// <summary>
        /// 绑定病人预交金缴纳记录列表
        /// </summary>
        /// <param name="patListDt">病人列表</param>
        /// <param name="isAdd">是否为新收费重新绑定</param>
        /// <param name="depositFee">预交金总额</param>
        /// <param name="sumFee">住院总费用</param>
        public void Binding_grdPayDetailList(DataTable patListDt, bool isAdd, decimal depositFee, decimal sumFee)
        {
            // 显示病人入院登记基本信息
            if (grdPatList.CurrentCell != null)
            {
                int rowIndex = grdPatList.CurrentCell.RowIndex;
                DataTable dt = grdPatList.DataSource as DataTable;
                // 住院流水号
                lblSerialNumber.Text = dt.Rows[rowIndex]["SerialNumber"].ToString();
                // 姓名
                lblPatName.Text = dt.Rows[rowIndex]["PatName"].ToString();
                // 科室
                lblPatDept.Text = dt.Rows[rowIndex]["DeptName"].ToString();
                // 床位号
                lblPatBedNo.Text = dt.Rows[rowIndex]["BedNo"].ToString();
            }
            // 累计缴费
            lblPatSumPayment.Text = string.Format("{0:N}", depositFee);
            // 累计记账
            lblPatSumAccounting.Text = string.Format("{0:N}", sumFee);
            // 余额
            lblPatBalance.Text = string.Format("{0:N}", depositFee - sumFee);
            // 绑定预交金网格列表数据
            grdPayDetailList.DataSource = patListDt;
            if (patListDt != null && patListDt.Rows.Count > 0)
            {
                for (int i = 0; i < patListDt.Rows.Count; i++)
                {
                    if (Tools.ToInt32(patListDt.Rows[i]["AccountID"]) <= 0)
                    {
                        patListDt.Rows[i]["AccountDate"] = DBNull.Value;
                    }
                }

                if (isAdd)
                {
                    if (grdPayDetailList.DataSource != null)
                    {
                        setGridSelectIndex(grdPayDetailList, patListDt.Rows.Count - 1);
                    }
                }
                else
                {
                    if (grdPayDetailList.DataSource != null)
                    {
                        setGridSelectIndex(grdPayDetailList);
                    }
                }
            }
        }

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="patListDt">病人列表</param>
        public void Binding_GrdPatList(DataTable patListDt)
        {
            grdPatList.DataSource = patListDt;
            if (grdPatList.DataSource != null)
            {
                setGridSelectIndex(grdPatList);
            }
            // 住院流水号
            lblSerialNumber.Text = string.Empty;
            // 姓名
            lblPatName.Text = string.Empty;
            // 科室
            lblPatDept.Text = string.Empty;
            // 床位号
            lblPatBedNo.Text = string.Empty;
            // 累计交费金额
            lblPatSumPayment.Text = string.Empty;
            // 累计记账金额
            lblPatSumAccounting.Text = string.Empty;
            // 余额
            lblPatBalance.Text = string.Empty;
        }

        /// <summary>
        /// 设置网格文字显示颜色
        /// </summary>
        public void SetGridColor()
        {
            foreach (DataGridViewRow row in grdPayDetailList.Rows)
            {
                short status_flag = Convert.ToInt16(row.Cells["states"].Value);

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

        #endregion

        /// <summary>
        /// 选中病人
        /// </summary>
        /// <param name="sender">grdPatList</param>
        /// <param name="e">事件参数</param>
        private void grdPatList_CurrentCellChanged(object sender, EventArgs e)
        {
            if (grdPatList.CurrentCell != null)
            {
                InvokeController("GetPaymentList", false);
                if (grdPayDetailList.DataSource != null)
                {
                    setGridSelectIndex(grdPayDetailList, 0);
                }
            }
        }
    }
}