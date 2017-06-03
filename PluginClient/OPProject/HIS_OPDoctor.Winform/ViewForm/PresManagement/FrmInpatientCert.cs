using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_OPDoctor.Winform.IView;

namespace HIS_OPDoctor.Winform.ViewForm
{
    /// <summary>
    /// 住院证
    /// </summary>
    public partial class FrmInpatientCert : BaseFormBusiness, IFrmInpatientCert
    {
        /// <summary>
        /// 是否保存
        /// </summary>
        OPD_InpatientReg isSaveData;

        /// <summary>
        /// 病人数据
        /// </summary>
        public DataTable DtPatient { get; set; }

        /// <summary>
        /// 病人id
        /// </summary>
        public int PatId { get; set; }

        /// <summary>
        /// 会员Id
        /// </summary>
        public int MeId { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmInpatientCert()
        {
            InitializeComponent();
            frmCommon.AddItem(txtDigShowCard, "txtDigShowCard");
            frmCommon.AddItem(txtAttention, "txtAttention");
            frmCommon.AddItem(txtDeposit, "txtDeposit", "请输入正确的金额", InvalidType.Custom, @"^([0-9]+|[0-9]{1,3}(,[0-9]{3})*)(.[0-9]{1,2})?$");
        }

        /// <summary>
        /// 获取病人信息以及会员信息
        /// </summary>
        /// <param name="dtPatient">病人数据</param>
        /// <param name="strDisease">诊断</param>
        public void BindPatientInfo(DataTable dtPatient, string strDisease)
        {
            DtPatient = dtPatient;
            if (dtPatient != null)
            {
                if (dtPatient.Rows.Count > 0)
                {
                    txtName.Text = dtPatient.Rows[0]["PatName"].ToString();
                    txtAge.Text = GetAge(dtPatient.Rows[0]["Age"].ToString());
                    txtSex.Text = dtPatient.Rows[0]["PatSex"].ToString();
                    txtFee.Text = dtPatient.Rows[0]["PatTypeName"].ToString();
                    txtAddress.Text = dtPatient.Rows[0]["CityName"].ToString() + dtPatient.Rows[0]["Address"].ToString();
                    txtHomePhone.Text = dtPatient.Rows[0]["Mobile"].ToString();
                    txtDisease.Text = strDisease;
                }
            }

            txtDocName.Text = (InvokeController("this") as AbstractController).LoginUserInfo.EmpName;
            txtDate.Text = DateTime.Now.ToString();
        }

        /// <summary>
        /// 绑定科室
        /// </summary>
        /// <param name="deptDt">科室数据</param>
        public void BindDept(DataTable deptDt)
        {
            deptDt.Rows.RemoveAt(0);
            cmbDept.DataSource = deptDt;
        }

        /// <summary>
        /// 获取住院证信息
        /// </summary>
        /// <param name="inpatientReg">住院证实体</param>
        public void GetInpatientReg(OPD_InpatientReg inpatientReg)
        {
            isSaveData = inpatientReg;
        }

        /// <summary>
        /// 绑定诊断showcard
        /// </summary>
        /// <param name="dt">诊断数据</param>
        public void BindDiseaseShowCard(DataTable dt)
        {
            txtDigShowCard.DisplayField = "Name";
            txtDigShowCard.MemberField = "ICDCode";
            txtDigShowCard.CardColumn = "ICDCode|ICD编码|100,Name|诊断名称|250";
            txtDigShowCard.QueryFieldsString = "Name,PYCode,WBCode,ICDCode";
            txtDigShowCard.ShowCardWidth = 350;
            txtDigShowCard.ShowCardHeight = 100;
            txtDigShowCard.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 保存完成后执行
        /// </summary>
        /// <param name="result">保存后结果</param>
        public void SaveComplete(int result)
        {
            if (result > 0)
            {
                MessageBoxShowSimple("保存成功");
            }
            else
            {
                MessageBoxShowSimple("保存失败");
            }
        }

        /// <summary>
        /// 根据Pannel获取选中的值
        /// </summary>
        /// <param name="panel">面板</param>
        /// <param name="index">索引</param>
        /// <returns>选中值</returns>
        public string GetRadioButtonValue(Panel panel, int index)
        {
            int selectindex = 0;
            foreach (Control ctrl in panel.Controls)
            {
                if (ctrl is RadioButton)
                {
                    RadioButton radioBtn = ctrl as RadioButton;
                    if (index > 0)
                    {
                        if (radioBtn.TabIndex == index)
                        {
                            radioBtn.Checked = true;
                        }
                    }
                    else
                    {
                        if (radioBtn.Checked)
                        {
                            selectindex = radioBtn.TabIndex;
                        }
                    }
                }
            }

            return selectindex.ToString();
        }

        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="age">年龄字符串</param>
        /// <returns>年龄</returns>
        public string GetAge(string age)
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
        /// 页面加载时获取已保存数据
        /// </summary>
        public void InitData()
        {
            if (isSaveData != null)
            {
                txtDigShowCard.MemberValue = isSaveData.HospitalCode;
                txtDeposit.Text = isSaveData.Deposit.ToString();
                txtAttention.Text = isSaveData.Attention;
                cmbDept.SelectedValue = isSaveData.InDeptID;
                txtDocName.Text = isSaveData.SignDocName;
                txtDate.Text = isSaveData.SignTime.ToString();
                GetRadioButtonValue(Diet, Convert.ToInt32(isSaveData.Diet));
                GetRadioButtonValue(Quarantine, Convert.ToInt32(isSaveData.Quarantine));
                GetRadioButtonValue(TransWay, Convert.ToInt32(isSaveData.TransWay));
                GetRadioButtonValue(BodyPosition, Convert.ToInt32(isSaveData.BodyPosition));
                GetRadioButtonValue(ConditionStu, Convert.ToInt32(isSaveData.ConditionStu));
            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmInpatientCert_Load(object sender, EventArgs e)
        {
            InvokeController("GetPatInfo");
            InvokeController("GetDisease", frmName);
            InvokeController("GetDeptList");
            InvokeController("GetInpatientReg");
            InitData();
        }

        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmCommon.Validate())
                {
                    OPD_InpatientReg inpatientReg = new OPD_InpatientReg();
                    if (isSaveData != null)
                    {
                        inpatientReg = isSaveData;
                    }
                    else
                    {
                        inpatientReg = new OPD_InpatientReg();
                        inpatientReg.PatListID = PatId;
                        inpatientReg.MemberID = MeId;
                        inpatientReg.OutPatientDocDia = txtDisease.Text;
                        inpatientReg.SignDocName = (InvokeController("this") as AbstractController).LoginUserInfo.EmpName;
                        inpatientReg.SignDoctorID = (InvokeController("this") as AbstractController).LoginUserInfo.EmpId;
                        inpatientReg.RegEmpID = (InvokeController("this") as AbstractController).LoginUserInfo.EmpId;
                        inpatientReg.SignTime = Convert.ToDateTime(txtDate.Text);
                        inpatientReg.RegStatus = 0;
                    }

                    inpatientReg.Diet = GetRadioButtonValue(Diet, 0);
                    inpatientReg.Quarantine = GetRadioButtonValue(Quarantine, 0);
                    inpatientReg.TransWay = GetRadioButtonValue(TransWay, 0);
                    inpatientReg.BodyPosition = GetRadioButtonValue(BodyPosition, 0);
                    inpatientReg.ConditionStu = GetRadioButtonValue(ConditionStu, 0);
                    inpatientReg.Attention = txtAttention.Text;
                    decimal deposit = 0;
                    if (decimal.TryParse(txtDeposit.Text, out deposit))
                    {
                    }

                    inpatientReg.Deposit = deposit;
                    if (txtDigShowCard.MemberValue != null)
                    {
                        inpatientReg.HospitalCode = txtDigShowCard.MemberValue.ToString();
                    }
                    else
                    {
                        inpatientReg.HospitalCode = string.Empty;
                    }

                    inpatientReg.HospitalDocDia = txtDigShowCard.Text;
                    int deptid = 0;
                    if (cmbDept.SelectedValue != null)
                    {
                        if (int.TryParse(cmbDept.SelectedValue.ToString(), out deptid))
                        {
                        }
                    }

                    inpatientReg.InDeptID = deptid;
                    inpatientReg.InDeptName = cmbDept.Text;
                    InvokeController("SaveInpatientReg", inpatientReg);
                    isSaveData = inpatientReg;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            frmCommon.Clear();
            this.Close();
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            InvokeController("ShowDialog", "FrmBedInfo");
        }

        /// <summary>
        /// 打印事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (isSaveData != null)
            {
                InvokeController("PrintInpatientReg", isSaveData);
            }
            else
            {
                MessageBoxEx.Show("当前没有可打印数据");
            }
        }
    }
}
