using System;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    /// <summary>
    /// 入院登记界面
    /// </summary>
    public partial class FrmAdmissionRegistration : BaseFormBusiness, IAdmissionRegistration
    {
        #region "属性"

        /// <summary>
        /// 新入院病人ID
        /// </summary>
        public int PatientID
        {
            get
            {
                if (grdPatList.CurrentCell != null)
                {
                    int rowIndex = grdPatList.CurrentCell.RowIndex;
                    DataTable dt = grdPatList.DataSource as DataTable;
                    return Convert.ToInt32(dt.Rows[rowIndex]["PatientID"]);
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 新入院病人登记信息ID
        /// </summary>
        public int PatListID
        {
            get
            {
                if (grdPatList.CurrentCell != null)
                {
                    int rowIndex = grdPatList.CurrentCell.RowIndex;
                    DataTable dt = grdPatList.DataSource as DataTable;
                    return Convert.ToInt32(dt.Rows[rowIndex]["PatListID"]);
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return chkMakerDate.Checked ? sdtpMakerDate.Bdate.Value.Date : DateTime.MinValue;
            }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return chkMakerDate.Checked ? sdtpMakerDate.Edate.Value.Date : DateTime.MinValue;
            }
        }

        /// <summary>
        /// 科室
        /// </summary>
        public int Dept
        {
            get
            {
                return int.Parse(cboDeptList.SelectedValue.ToString().Trim());
            }
        }

        /// <summary>
        /// 病人类型
        /// </summary>
        public int PatType
        {
            get
            {
                return int.Parse(cboPatType.SelectedValue.ToString().Trim());
            }
        }

        /// <summary>
        /// 检索条件
        /// </summary>
        public string SelectParm
        {
            get
            {
                return txtSearchParm.Text.Trim();
            }
        }

        /// <summary>
        /// 病人状态
        /// </summary>
        public string PatStatus
        {
            get
            {
                string patStatus = string.Empty; // 病人状态
                if (chkNewHospital.Checked)
                {
                    patStatus = "1,"; // 新入院
                }

                if (chkBeInBed.Checked)
                {
                    patStatus += "2,"; // 入院在床
                }

                if (chkDefinedDischarge.Checked)
                {
                    patStatus += "3,";// 出院未结算
                }

                if (chkLeaveHospital.Checked)
                {
                    patStatus += "4,";    // 出院已结算
                }

                if (patStatus.Length >= 2)
                {
                    patStatus = patStatus.Substring(0, patStatus.Length - 1);
                }

                return patStatus;
            }
        }
        #endregion

        #region "事件"

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmAdmissionRegistration()
        {
            InitializeComponent();
            frmAdmission.AddItem(chkMakerDate, string.Empty);
            frmAdmission.AddItem(sdtpMakerDate, string.Empty);
            frmAdmission.AddItem(cboDeptList, string.Empty);
            frmAdmission.AddItem(cboPatType, string.Empty);
            frmAdmission.AddItem(txtSearchParm, string.Empty);
            frmAdmission.AddItem(chkNewHospital, string.Empty);
            frmAdmission.AddItem(chkBeInBed, string.Empty);
            frmAdmission.AddItem(chkDefinedDischarge, string.Empty);
            frmAdmission.AddItem(chkLeaveHospital, string.Empty);
        }

        /// <summary>
        /// F1-F12功能键注册
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件参数</param>
        private void FrmAdmissionRegistration_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F6:
                    // 调用控制器打开病人基本信息录入界面
                    InvokeController("ShowFrmPatientInfo", true);
                    break;
                case Keys.F7:
                    if (grdPatList.CurrentCell != null)
                    {
                        int rowIndex = grdPatList.CurrentCell.RowIndex;
                        DataTable dt = grdPatList.DataSource as DataTable;
                        InvokeController(
                            "CancelAdmission", 
                            int.Parse(dt.Rows[rowIndex]["PatListID"].ToString().Trim()),
                            dt.Rows[rowIndex]["PatName"].ToString().Trim());

                        // 查询病人列表
                        InvokeController("GetPatientList", false);
                    }

                    break;
                case Keys.F8:
                    InvokeController("ShowFrmPatientInfo", false);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件参数</param>
        private void FrmAdmissionRegistration_OpenWindowBefore(object sender, EventArgs e)
        {
            bindGridSelectIndex(grdPatList);// 绑定网格选中行
            InvokeController("GetDeptList", true); // 加载所有住院临床科室
            InvokeController("GetPatType", true);// 加载病人类型列表
            txtSearchParm.Focus();
            sdtpMakerDate.Bdate.Value = DateTime.Now;
            sdtpMakerDate.Edate.Value = DateTime.Now;
            chkNewHospital.Checked = true;
            chkBeInBed.Checked = true;
        }

        /// <summary>
        /// 释放资源，关闭当前窗口
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 病人新入院
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            // 调用控制器打开病人基本信息录入界面
            InvokeController("ShowFrmPatientInfo", true);
        }

        /// <summary>
        /// 勾选日期CheckBox事件
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void chkTime_CheckedChanged(object sender, EventArgs e)
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
        /// 修改病人信息
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            InvokeController("ShowFrmPatientInfo", false);
        }

        /// <summary>
        /// 取消入院
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (grdPatList.CurrentCell != null)
            {
                int rowIndex = grdPatList.CurrentCell.RowIndex;
                DataTable dt = grdPatList.DataSource as DataTable;
                InvokeController(
                    "CancelAdmission", 
                    int.Parse(dt.Rows[rowIndex]["PatListID"].ToString().Trim()),
                    dt.Rows[rowIndex]["PatName"].ToString().Trim());
                // 查询病人列表
                InvokeController("GetPatientList", false);
            }
        }

        /// <summary>
        /// 查询病人列表
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 查询病人列表
            InvokeController("GetPatientList", false);
        }
        #endregion

        #region "私有方法"

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="patList">病人列表</param>
        /// <param name="isAdd">true：有病人新入院后重新绑定/false：修改病人信息</param>
        public void Bind_grdPatList(DataTable patList, bool isAdd)
        {
            grdPatList.DataSource = patList;
            if (isAdd)
            {
                if (patList != null && patList.Rows.Count > 0)
                {
                    setGridSelectIndex(grdPatList, patList.Rows.Count - 1);
                }
            }
            else
            {
                if (patList != null && patList.Rows.Count > 0)
                {
                    setGridSelectIndex(grdPatList);
                }
            }
        }

        /// <summary>
        /// 绑定科室列表数据
        /// </summary>
        /// <param name="deptListDt">科室列表</param>
        public void Bind_cboDeptList(DataTable deptListDt)
        {
            cboDeptList.DataSource = deptListDt;
            cboDeptList.ValueMember = "DeptId";
            cboDeptList.DisplayMember = "Name";
            cboDeptList.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定病人类型列表
        /// </summary>
        /// <param name="patTypeListDt">病人类型列表</param>
        public void Bind_cboPatType(DataTable patTypeListDt)
        {
            cboPatType.DataSource = patTypeListDt;
            cboPatType.ValueMember = "PatTypeID";
            cboPatType.DisplayMember = "PatTypeName";
            cboPatType.SelectedIndex = 0;
        }

        /// <summary>
        /// 数据加载完成后设置界面按钮可用
        /// </summary>
        public void SetControlEnabled()
        {
            btnNew.Enabled = true;
            btnCancel.Enabled = true;
            btnUpdate.Enabled = true;
        }

        #endregion
    }
}