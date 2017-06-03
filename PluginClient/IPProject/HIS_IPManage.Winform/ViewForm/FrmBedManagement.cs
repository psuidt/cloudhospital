using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EfwControls.Common;
using EfwControls.HISControl.BedCard.Controls;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    /// <summary>
    /// 床位一览界面
    /// </summary>
    public partial class FrmBedManagement : BaseFormBusiness, IBedManagement
    {
        /// <summary>
        /// 是否为首次打开界面
        /// </summary>
        private bool firstOpen = false;

        /// <summary>
        /// 床位对象
        /// </summary>
        public BedInfo BedInfo = new BedInfo();

        /// <summary>
        /// 床位对象
        /// </summary>
        public BedInfo Bed
        {
            get
            {
                if (bedCardControl1.SelectedBed != null && !string.IsNullOrEmpty(bedCardControl1.SelectedBed.BedNo))
                {
                    BedInfo = bedCardControl1.SelectedBed;
                }

                return BedInfo;
            }
        }

        /// <summary>
        /// 总床位数
        /// </summary>
        private decimal sumBedCount = 0;

        /// <summary>
        /// 已使用床位数
        /// </summary>
        private decimal occupyBedCount = 0;

        /// <summary>
        /// 病人状态
        /// </summary>
        public int PatStatus
        {
            get
            {
                if (rdoNew.Checked)
                {
                    return 1;  // 新入院
                }
                else if (rdoBeInBed.Checked)
                {
                    return 2;// 在院
                }
                else if (rdoDefinedDischarge.Checked)
                {
                    return 3;  // 出院未结算
                }
                else if (rdoLeaveHospital.Checked)
                {
                    return 4;  // 出院/转科
                }

                return 0;
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
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return rdoLeaveHospital.Checked ? sdtEnterHDate.Bdate.Value.Date : DateTime.MinValue;
            }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return rdoLeaveHospital.Checked ? sdtEnterHDate.Edate.Value.Date : DateTime.MinValue;
            }
        }

        /// <summary>
        /// 是否只查询可催款病人
        /// </summary>
        public bool IsReminder
        {
            get
            {
                return chkIsReminder.Checked;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmBedManagement()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 绑定病区列表数据
        /// </summary>
        /// <param name="wardDt">病区列表</param>
        public void Bind_WardDept(DataTable wardDt)
        {
            if (wardDt != null && wardDt.Rows.Count > 0)
            {
                cboInpatientAreaList.ComboBoxEx.DataSource = wardDt;
                cboInpatientAreaList.ComboBoxEx.ValueMember = "DeptId";
                firstOpen = true;
                cboInpatientAreaList.ComboBoxEx.DisplayMember = "Name";
            }
        }

        /// <summary>
        /// 窗体打开之前加载数据
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmBedManagement_OpenWindowBefore(object sender, EventArgs e)
        {
            // 加载病区数据
            InvokeController("GetWardDept");
            tabControl1.SelectedTabIndex = 0;
            // 设置床位控件显示内容
            bedCardControl1.BedContextFields = new List<ContextField>();
            bedCardControl1.BedContextFields.Add(new ContextField("住院号", "CaseNumber"));
            bedCardControl1.BedContextFields.Add(new ContextField("科室", "Dept"));
            bedCardControl1.BedContextFields.Add(new ContextField("医生", "Doctor"));
            bedCardControl1.BedContextFields.Add(new ContextField("护士", "Nurse"));
            bedCardControl1.BedContextFields.Add(new ContextField("类型", "PatTypeName"));
            bedCardControl1.BedContextFields.Add(new ContextField("入科", "EnterTime"));
            bedCardControl1.BedContextFields.Add(new ContextField("诊断", "Diagnosis"));
            // 病人列表界面默认选中在院按钮
            rdoNew.Checked = true;
            sdtEnterHDate.Bdate.Value = DateTime.Now;
            sdtEnterHDate.Edate.Value = DateTime.Now;
            sdtEnterHDate.Enabled = false;
        }

        /// <summary>
        /// 根据选择的病区查询床位列表以及床位分配情况
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void cboInpatientAreaList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 根据病区加载床位
            if (firstOpen)
            {
                InvokeController("GetBedList", Convert.ToInt32(cboInpatientAreaList.ComboBoxEx.SelectedValue));
            }
        }

        /// <summary>
        /// 注册功能键
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmBedManagement_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    btnDept_Click(sender, e); // F5病人转科
                    break;
                case Keys.F6:
                    btnDistributionBed_Click(sender, e);  // F6分配床位
                    break;
                case Keys.F7:
                    btnCancelTheBed_Click(sender, e);  // F7取消床位分配
                    break;
                case Keys.F8:
                    btnUpdDoctorNurse_Click(sender, e); // F8更换医生护士
                    break;
                case Keys.F9:
                    btnUpdBed_Click(sender, e);  // F9换床
                    break;
                case Keys.F10:
                    btnPackBed_Click(sender, e); // 包床
                    break;
                case Keys.F11:
                    btnCancelPackBed_Click(sender, e); // 取消包床
                    break;
                case Keys.F12:
                    btnOutHospital_Click(sender, e); // 出院
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 绑定床位列表
        /// </summary>
        /// <param name="bedListDt">床位列表</param>
        public void Bind_BedList(DataTable bedListDt)
        {
            // 定义床位集合
            List<BedInfo> list = new List<BedInfo>();
            // 当前病区有床位的场合
            if (bedListDt != null && bedListDt.Rows.Count > 0)
            {
                // 循环显示床位
                for (int i = 0; i < bedListDt.Rows.Count; i++)
                {
                    BedInfo bed = new BedInfo();
                    // 床位相关数据
                    bed.BedNo = bedListDt.Rows[i]["BedNO"].ToString(); // 床位号
                    bed.WardCode = bedListDt.Rows[i]["WardID"].ToString(); // 病区ID

                    // 病人住院信息相关数据
                    if (Convert.ToInt32(bedListDt.Rows[i]["PatListID"].ToString()) > 0)
                    {
                        bed.PatientID = Convert.ToInt32(bedListDt.Rows[i]["PatListID"].ToString());  // 病人登记ID
                        bed.PatientNum = bedListDt.Rows[i]["SerialNumber"].ToString();   // 住院流水号
                        bed.CaseNumber = Tools.ToDecimal(bedListDt.Rows[i]["CaseNumber"]);   // 住院流水号
                        bed.PatientName = bedListDt.Rows[i]["PatName"].ToString(); // 病人姓名
                        bed.Sex = bedListDt.Rows[i]["PatSex"].ToString(); // 性别
                        bed.Age = GetAge(bedListDt.Rows[i]["Age"].ToString());  // 年龄
                        bed.Diagnosis = bedListDt.Rows[i]["EnterDiseaseName"].ToString(); // 入院诊断
                        bed.Dept = bedListDt.Rows[i]["DeptName"].ToString(); // 入院科室
                        bed.DeptCode = bedListDt.Rows[i]["PatDeptID"].ToString(); // 入院科室
                        bed.Doctor = bedListDt.Rows[i]["DoctorName"].ToString(); // 医生
                        bed.Nurse = bedListDt.Rows[i]["NurseName"].ToString(); // 护士
                        bed.EnterTime = DateTime.Parse(bedListDt.Rows[i]["EnterHDate"].ToString()).ToString("yyyy-MM-dd HH:mm"); // 入院时间
                        bed.Situation = bedListDt.Rows[i]["OutSituation"].ToString(); // 病人情况
                        bed.Step = Convert.ToInt32(bedListDt.Rows[i]["IsLeaveHosOrder"].ToString()); // 是否出院医嘱
                        bed.DietType = bedListDt.Rows[i]["DietType"].ToString(); // 饮食级别
                        bed.NursingLever = bedListDt.Rows[i]["NursingLever"].ToString(); // 护理级别
                        bed.PatTypeName = bedListDt.Rows[i]["PatTypeName"].ToString(); // 病人类型
                        bed.Group = bedListDt.Rows[i]["IsPack"].ToString().Trim() == "1";
                    }

                    list.Add(bed);
                }
            }

            bedCardControl1.DataSource = list;
            StatisticalBedCount();

            if (sumBedCount > 0)
            {
                decimal occupancyRate = 0;

                if (occupyBedCount > 0)
                {
                    decimal tempOccupancyRate = ((occupyBedCount / sumBedCount) * 100);
                    occupancyRate = Math.Round(tempOccupancyRate, 2);
                }

                lblBedStatistical.Text = string.Format(
                    "        总床位数：{0}    占用床位：{1}    占床比例：{2}%   可用床位：{3}",
                    sumBedCount, 
                    occupyBedCount, 
                    occupancyRate, 
                    sumBedCount - occupyBedCount);
            }
            else
            {
                lblBedStatistical.Text = string.Format(
                    "        总床位数：{0}    占用床位：{1}    占床比例：{2}%   可用床位：{3}", 
                    0, 
                    0, 
                    0, 
                    0);
            }
        }

        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="age">待转换的年龄</param>
        /// <returns>转换后的年龄</returns>
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
        /// 分配床位
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnDistributionBed_Click(object sender, EventArgs e)
        {
            if (bedCardControl1.SelectedBed != null && !string.IsNullOrEmpty(bedCardControl1.SelectedBed.BedNo))
            {
                if (bedCardControl1.SelectedBed.PatientID == 0)
                {
                    // 打开分配床位界面
                    InvokeController("ShowFrmBedAllocation");
                }
                else
                {
                    // 提示病床已有病人Msg
                    InvokeController("MessageShow", "当前床位已分配了病人，请重新选择床位！");
                }
            }
            else
            {
                // 提示选中床位Msg
                InvokeController("MessageShow", "请选择需要分配病人的床位！");
            }
        }

        /// <summary>
        /// 取消床位分配
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnCancelTheBed_Click(object sender, EventArgs e)
        {
            if (bedCardControl1.SelectedBed != null && !string.IsNullOrEmpty(bedCardControl1.SelectedBed.BedNo))
            {
                if (bedCardControl1.SelectedBed.PatientID != 0)
                {
                    // 打开分配床位界面
                    InvokeController(
                        "CancelTheBed", 
                        bedCardControl1.SelectedBed.PatientID,
                        int.Parse(bedCardControl1.SelectedBed.WardCode),
                        bedCardControl1.SelectedBed.BedNo);
                }
                else
                {
                    // 提示病床上没有病人
                    InvokeController("MessageShow", "当前床位没有分配病人，不需要取消分配！");
                }
            }
            else
            {
                // 提示选中床位Msg
                InvokeController("MessageShow", "请选择需要取消分配的床位！");
            }
        }

        /// <summary>
        /// 取消包床
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnCancelPackBed_Click(object sender, EventArgs e)
        {
            if (bedCardControl1.SelectedBed != null && !string.IsNullOrEmpty(bedCardControl1.SelectedBed.BedNo))
            {
                if (bedCardControl1.SelectedBed.PatientID != 0)
                {
                    InvokeController(
                        "CancelPackBed", 
                        bedCardControl1.SelectedBed.PatientID,
                        int.Parse(bedCardControl1.SelectedBed.WardCode),
                        bedCardControl1.SelectedBed.BedNo);
                }
                else
                {
                    // 提示病床上没有病人
                    InvokeController("MessageShow", "当前床位没有分配病人，无法进行取消包床操作！");
                }
            }
            else
            {
                // 提示选中床位Msg
                InvokeController("MessageShow", "请选择需要取消包床的床位！");
            }
        }

        /// <summary>
        /// 换床
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnUpdBed_Click(object sender, EventArgs e)
        {
            if (bedCardControl1.SelectedBed != null && !string.IsNullOrEmpty(bedCardControl1.SelectedBed.BedNo))
            {
                if (bedCardControl1.SelectedBed.PatientID != 0 && bedCardControl1.SelectedBed.Group == false)
                {
                    // 打开换床界面
                    InvokeController("ShowFrmPatientBedChanging");
                }
                else if (bedCardControl1.SelectedBed.Group == true)
                {
                    // 提示选床位Msg
                    InvokeController("MessageShow", "您选择的病人已经包床，不能进行换床操作！");
                }
                else
                {
                    // 提示病床上没有病人
                    InvokeController("MessageShow", "当前床位没有分配病人，不能进行换床！");
                }
            }
            else
            {
                // 提示选床位Msg
                InvokeController("MessageShow", "请选择需要换床的床位！");
            }
        }

        /// <summary>
        /// 修改医生或护士
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnUpdDoctorNurse_Click(object sender, EventArgs e)
        {
            if (bedCardControl1.SelectedBed != null && !string.IsNullOrEmpty(bedCardControl1.SelectedBed.BedNo))
            {
                if (bedCardControl1.SelectedBed.PatientID != 0)
                {
                    // 打开换床界面
                    InvokeController("ShowFrmUpdatePatient");
                }
                else
                {
                    // 提示病床上没有病人
                    InvokeController("MessageShow", "当前床位没有分配病人！");
                }
            }
            else
            {
                // 提示选床位Msg
                InvokeController("MessageShow", "请选择需要医生或护士的床位！");
            }
        }

        /// <summary>
        /// 病人定义出院
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnOutHospital_Click(object sender, EventArgs e)
        {
            if (bedCardControl1.SelectedBed != null && !string.IsNullOrEmpty(bedCardControl1.SelectedBed.BedNo))
            {
                if (bedCardControl1.SelectedBed.PatientID != 0)
                {
                    // 打开换床界面
                    InvokeController("PatientOutHospital");
                }
                else
                {
                    // 提示病床上没有病人
                    InvokeController("MessageShow", "当前床位没有分配病人！");
                }
            }
            else
            {
                // 提示选床位Msg
                InvokeController("MessageShow", "请选择需要定义出院的床位！");
            }
        }

        /// <summary>
        /// 右键定义病人出院
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void tolOutHospital_Click(object sender, EventArgs e)
        {
            btnOutHospital_Click(sender, e);
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
        /// 包床
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnPackBed_Click(object sender, EventArgs e)
        {
            if (bedCardControl1.SelectedBed != null && !string.IsNullOrEmpty(bedCardControl1.SelectedBed.BedNo))
            {
                if (bedCardControl1.SelectedBed.PatientID == 0)
                {
                    // 打开换床界面
                    InvokeController("ShowFrmPackBed");
                }
                else
                {
                    // 提示病床上没有病人
                    InvokeController("MessageShow", "当前床位已分配病人，请重新选择！");
                }
            }
            else
            {
                // 提示选床位Msg
                InvokeController("MessageShow", "请选择一个空床位！");
            }
        }
        
        /// <summary>
        /// 统计床位数
        /// </summary>
        public void StatisticalBedCount()
        {
            sumBedCount = 0;
            occupyBedCount = 0;
            List<BedInfo> bedList = bedCardControl1.DataSource as List<BedInfo>;
            sumBedCount = bedList.Count;
            foreach (BedInfo bed in bedList)
            {
                if (bed.PatientID > 0)
                {
                    occupyBedCount++;
                }
            }
        }

        #region "临床部分代码"
        
        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="deptDt">科室列表</param>
        /// <param name="deptId">默认科室ID</param>
        public void Bind_DeptList(DataTable deptDt, int deptId)
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
        /// 查询病人列表
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnQueryPatList_Click(object sender, EventArgs e)
        {
            InvokeController("GetNursingStationPatList");
        }

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="patListDt">病人列表</param>
        public void Bind_PatList(DataTable patListDt)
        {
            grdPatList.DataSource = patListDt;
        }

        #endregion

        /// <summary>
        /// 批量发送医嘱
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnDeptSend_Click(object sender, EventArgs e)
        {
            if (rdoBeInBed.Checked && grdPatList.Rows.Count > 0)
            {
                List<int> iPatientList = new List<int>();
                DataTable dt = grdPatList.DataSource as DataTable;

                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToInt32(dr["CheckFlg"]) == 1)
                    {
                        int iPatient = Convert.ToInt32(dr["PatListID"]);
                        iPatientList.Add(iPatient);
                    }
                }

                // 打开发送界面
                InvokeController("DeptOrderCheck", iPatientList);
            }
        }

        /// <summary>
        /// 网格选中事件
        /// </summary>
        /// <param name="sender">grdPatList</param>
        /// <param name="e">事件参数</param>
        private void grdPatList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdPatList.Rows.Count > 0)
            {
                if (e.ColumnIndex == 0)
                {
                    int rowIndex = grdPatList.CurrentCell.RowIndex;
                    DataTable dt = grdPatList.DataSource as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // 去掉选中
                        if (Convert.ToInt32(dt.Rows[rowIndex]["CheckFlg"]) == 1)
                        {
                            dt.Rows[rowIndex]["CheckFlg"] = 0;
                        }
                        else
                        {
                            dt.Rows[rowIndex]["CheckFlg"] = 1;
                        }
                    }
                }

                grdPatList.EndEdit();
            }
        }

        /// <summary>
        /// 病人全选反选
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void chkPatAll_CheckedChanged(object sender, EventArgs e)
        {
            DataTable longOrderDt = grdPatList.DataSource as DataTable;
            if (longOrderDt.Rows.Count > 0)
            {
                for (int i = 0; i < longOrderDt.Rows.Count; i++)
                {
                    longOrderDt.Rows[i]["CheckFlg"] = chkPatAll.Checked ? 1 : 0;
                }
            }
        }

        /// <summary>
        /// 打印催款单
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void tolPrintReminder_Click(object sender, EventArgs e)
        {
            InvokeController("PrintReminder");
        }

        /// <summary>
        /// 批量打印催款单
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnPrintReminder_Click(object sender, EventArgs e)
        {
            if (rdoBeInBed.Checked && grdPatList.Rows.Count > 0)
            {
                string patIDList = string.Empty;
                DataTable patListDt = grdPatList.DataSource as DataTable;
                if (patListDt.Rows.Count > 0)
                {
                    for (int i = 0; i < patListDt.Rows.Count; i++)
                    {
                        if (Tools.ToInt32(patListDt.Rows[i]["CheckFlg"]) == 1)
                        {
                            patIDList += Tools.ToString(patListDt.Rows[i]["PatListID"]) + ",";
                        }
                    }
                    // 没有勾选病人不打印催款单
                    if (!string.IsNullOrEmpty(patIDList))
                    {
                        patIDList = patIDList.Substring(0, patIDList.Length - 1);
                        InvokeController("BatchPrintReminder", patIDList);
                    }
                }
            }
        }

        /// <summary>
        /// 病人转科
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnDept_Click(object sender, EventArgs e)
        {
            if (bedCardControl1.SelectedBed != null && !string.IsNullOrEmpty(bedCardControl1.SelectedBed.BedNo))
            {
                if (bedCardControl1.SelectedBed.PatientID != 0)
                {
                    // 打开换床界面
                    InvokeController("PatTransDept");
                }
                else
                {
                    // 提示病床上没有病人
                    InvokeController("MessageShow", "当前床位没有分配病人！");
                }
            }
            else
            {
                // 提示选床位Msg
                InvokeController("MessageShow", "请选择需要转科的床位！");
            }
        }

        /// <summary>
        /// 右键转科
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void tolDept_Click(object sender, EventArgs e)
        {
            btnDept_Click(sender, e);
        }

        /// <summary>
        /// 右键取消包床
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void tolCancelPackBed_Click(object sender, EventArgs e)
        {
            btnCancelPackBed_Click(null, null);
        }

        /// <summary>
        /// 查看血糖图表
        /// </summary>
        /// <param name="sender">查看血糖图表ToolStripMenuItem</param>
        /// <param name="e">事件所需参数</param>
        private void 查看血糖图表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeController("OpenBloodGlucoseChart");
        }

        /// <summary>
        /// 勾选新入院
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void rdoNew_CheckedChanged(object sender, EventArgs e)
        {
            btnDeptSend.Enabled = false;
            btnPrintReminder.Enabled = false;
            chkIsReminder.Checked = false;
        }

        /// <summary>
        /// 勾选在床
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void rdoBeInBed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsReminder.Checked)
            {
                btnPrintReminder.Enabled = true;
            }

            btnDeptSend.Enabled = true;
        }

        /// <summary>
        /// 勾选出院未结算
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void rdoDefinedDischarge_CheckedChanged(object sender, EventArgs e)
        {
            btnDeptSend.Enabled = false;
            btnPrintReminder.Enabled = false;
            chkIsReminder.Checked = false;
        }

        /// <summary>
        /// 勾选出院按钮启用或停用时间
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void rdoLeaveHospital_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoLeaveHospital.Checked)
            {
                sdtEnterHDate.Enabled = true;
            }
            else
            {
                sdtEnterHDate.Enabled = false;
            }

            btnDeptSend.Enabled = false;
            btnPrintReminder.Enabled = false;
            chkIsReminder.Checked = false;
        }

        /// <summary>
        /// 是否为查询可催款病人
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void chkIsReminder_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdoBeInBed.Checked)
            {
                chkIsReminder.Checked = false;
                return;
            }

            if (chkIsReminder.Checked)
            {
                btnPrintReminder.Enabled = true;
            }
            else
            {
                btnPrintReminder.Enabled = false;
            }
        }

        /// <summary>
        /// 查看三测单数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void tolSeeTemperature_Click(object sender, EventArgs e)
        {
            if (bedCardControl1.SelectedBed != null && !string.IsNullOrEmpty(bedCardControl1.SelectedBed.BedNo))
            {
                if (bedCardControl1.SelectedBed.PatientID != 0)
                {
                    // 打开分配床位界面
                    InvokeController(
                        "ShowFemTemperature",
                        bedCardControl1.SelectedBed.PatientID,
                        bedCardControl1.SelectedBed.BedNo,
                        bedCardControl1.SelectedBed.PatientName,
                        Tools.ToDecimal(bedCardControl1.SelectedBed.PatientNum),
                        bedCardControl1.SelectedBed.CaseNumber);
                }
                else
                {
                    // 提示病床上没有病人
                    InvokeController("MessageShow", "当前床位没有分配病人，没有数据！");
                }
            }
            else
            {
                // 提示选中床位Msg
                InvokeController("MessageShow", "没有选择床位！");
            }
        }
    }
}