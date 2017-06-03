using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EfwControls.Common;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.IPManage;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    /// <summary>
    /// 病人入院登记界面
    /// </summary>
    public partial class FrmPatientInfo : BaseFormBusiness, IPatientInfo
    {
        #region "属性"

        /// <summary>
        /// 是否可交预交金
        /// </summary>
        private bool invoiceControlEnabled = false;

        /// <summary>
        /// 病人基本信息
        /// </summary>
        private IP_PatientInfo patientInfo = new IP_PatientInfo();

        /// <summary>
        /// 住院病人信息
        /// </summary>
        public IP_PatientInfo PatientInfo
        {
            get
            {
                frmPatient.GetValue<IP_PatientInfo>(patientInfo);
                return patientInfo;
            }

            set
            {
                patientInfo = value;
                frmPatient.Load(patientInfo);
            }
        }

        /// <summary>
        /// 住院登记记录信息
        /// </summary>
        private IP_PatList patList = new IP_PatList();

        /// <summary>
        /// 住院登记记录信息
        /// </summary>
        public IP_PatList PatList
        {
            get
            {
                frmPatient.GetValue<IP_PatList>(patList);
                patList.CaseNumber = txtPatCaseNo.Text.Trim();
                patList.EnterDiseaseName = txtPatDisease.Text;
                patList.Age = GetAge();
                return patList;
            }

            set
            {
                patList = value;
                AgeValue ag = AgeExtend.GetAgeValue(this.dtpPatbriDate.Value);
                string age = ag.ReturnAgeStr_EN();

                if (!string.IsNullOrEmpty(age) && age.Length > 1)
                {
                    txtPatAge.Text = age.Substring(1);
                    SetAge(ag.ReturnAgeStr_EN());
                }

                txtPatCaseNo.Text = patList.CaseNumber;
                frmPatient.Load(patList);
            }
        }

        /// <summary>
        /// 病人新入院标志
        /// </summary>
        private bool mIsNewPatient = false;

        /// <summary>
        /// 病人新入院标志
        /// </summary>
        public bool IsNewPatient
        {
            get
            {
                return mIsNewPatient;
            }

            set
            {
                mIsNewPatient = value;
            }
        }

        /// <summary>
        /// 获取卡类型
        /// </summary>
        public int GetCardTypeID
        {
            get
            {
                return cboCardType.SelectedValue == null ? -1 : Convert.ToInt32(cboCardType.SelectedValue);
            }
        }

        /// <summary>
        /// 病人新入院标志
        /// </summary>
        public string CardNo
        {
            get
            {
                return txtCardNo.Text.Trim();
            }
        }

        /// <summary>
        /// 是否为住院证登记
        /// </summary>
        private bool mInpatientReg = false;

        /// <summary>
        /// 是否为住院证登记
        /// </summary>
        public bool InpatientReg
        {
            get
            {
                return mInpatientReg;
            }

            set
            {
                mInpatientReg = value;
            }
        }

        /// <summary>
        /// 预交金金额
        /// </summary>
        private decimal mDeposit = 0;

        /// <summary>
        /// 预交金金额
        /// </summary>
        public decimal Deposit
        {
            get
            {
                return mDeposit;
            }

            set
            {
                mDeposit = value;
            }
        }

        /// <summary>
        /// 预交金票据号
        /// </summary>
        public string InvoiceNO
        {
            get
            {
                return txtBillNumber.Text.Trim(); // 票据号;
            }
        }

        /// <summary>
        /// 预交金支付方式
        /// </summary>
        public string PayType
        {
            get
            {
                return cboPaymentMethod.SelectedValue.ToString().Trim();
            }
        }

        /// <summary>
        /// 预交金金额
        /// </summary>
        public decimal TotalFee
        {
            get
            {
                if (!string.IsNullOrEmpty(txtDeposit.Text.Trim()))
                {
                    return decimal.Parse(txtDeposit.Text.Trim());
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 获取年龄（包括年龄类型）
        /// </summary>
        /// <returns>带前缀（Y、M、D、H）的年龄值</returns>
        private string GetAge()
        {
            string age = string.Empty;
            switch (cboPatAgeType.SelectedIndex)
            {
                // 岁
                case 0:
                    age = string.Format("Y{0}", txtPatAge.Text.Trim());
                    break;
                // 月
                case 1:
                    age = string.Format("M{0}", txtPatAge.Text.Trim());
                    break;
                // 天
                case 2:
                    age = string.Format("D{0}", txtPatAge.Text.Trim());
                    break;
                // 时
                case 3:
                    age = string.Format("H{0}", txtPatAge.Text.Trim());
                    break;
            }

            return age;
        }

        /// <summary>
        /// 设置界面年龄下拉框显示内容
        /// </summary>
        /// <param name="age">年龄类型</param>
        private void SetAge(string age)
        {
            string value = string.Empty;
            if (age.Contains("Y"))
            {
                value = "Y";
                cboPatAgeType.SelectedIndex = 0;
            }
            else if (age.Contains("M"))
            {
                value = "M";
                cboPatAgeType.SelectedIndex = 1;
            }
            else if (age.Contains("D"))
            {
                value = "D";
                cboPatAgeType.SelectedIndex = 2;
            }
            else if (age.Contains("H"))
            {
                value = "H";
                cboPatAgeType.SelectedIndex = 3;
            }
            else
            {
                cboPatAgeType.SelectedIndex = 0;
            }

            if (!string.IsNullOrEmpty(age))
            {
                txtPatAge.Text = age.Replace(value, string.Empty);
            }
        }
        #endregion

        #region "事件"

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmPatientInfo()
        {
            InitializeComponent();
            // 病人基本信息
            frmPatient.AddItem(cboCardType, string.Empty);   // 卡类型
            frmPatient.AddItem(txtPatCureNum, "Times");  // 住院次数
            frmPatient.AddItem(txtCardNo, "CardNO"); // 卡号
            frmPatient.AddItem(txtPatName, "PatName");   // 病人姓名
            frmPatient.AddItem(txtPatSex, "Sex");  // 性别
            frmPatient.AddItem(dtpPatbriDate, "Birthday");  // 出生年月
            frmPatient.AddItem(txtPatAge, string.Empty);  // 年龄
            frmPatient.AddItem(txtPatNumber, "IdentityNum");  // 身份证号
            frmPatient.AddItem(txtPatNationality, "Nationality");  // 国籍
            frmPatient.AddItem(txtPatPlaceOfOrigin, "Native");  // 籍贯
            frmPatient.AddItem(txtNation, "Nation");  // 民族
            frmPatient.AddItem(
                txtPatTel,
                "Phone",
                "请输入正确的电话号码！",
                InvalidType.Custom,
                @"^[1][358]\d{9}$|^(0\d{2,3}-)?(\d{7,8})(-(\d{1,3}))?$");  // 联系电话
            frmPatient.AddItem(txtEnglishName, "EName");  // 病人英文名
            frmPatient.AddItem(txtCulturalLevel, "CulturalLevel");  // 文化程度
            frmPatient.AddItem(txtPatJob, "Occupation");  // 职业
            frmPatient.AddItem(txtMatrimony, "Matrimony");  // 婚姻状况
            frmPatient.AddItem(txtPatDatCardNo, "PatDatCardNo"); // 诊疗卡号
            frmPatient.AddItem(txtBirthplace, "Birthplace");  // 出生地址
            frmPatient.AddItem(txtPatCaseNo, "CaseNumber");  // 病案流水号
            frmPatient.AddItem(txtBirthplaceDetail, "BirthplaceDetail");  // 详细出生地址
            frmPatient.AddItem(txtPatPresentAddress, "NAddress");  // 现住地址
            frmPatient.AddItem(
                txtPatPresentAddressNo,
                "NZipCode",
                "请输入正确的邮编！",
                InvalidType.Custom,
                @"^[1-9]\d{5}(?!\d)");  // 现住地址邮编
            frmPatient.AddItem(txtPatPresentAddressDetail, "NAddressDetail");  // 现住详细地址
            frmPatient.AddItem(txtPatContacts, "RelationName");  // 紧急联系人
            frmPatient.AddItem(txtRelation, "Relation");  // 与本人关系
            frmPatient.AddItem(
                txtPatContactsTel,
                "RPhone",
                "请输入正确的电话号码！",
                InvalidType.Custom,
                @"^[1][358]\d{9}$|^(0\d{2,3}-)?(\d{7,8})(-(\d{1,3}))?$");  // 联系人电话
            frmPatient.AddItem(txtPatHouseholdReg, "DRegisterAddr");  // 户籍地址
            frmPatient.AddItem(txtPatHouseholdRegNo, "DZipCode", "请输入正确的邮编！", InvalidType.Custom, @"^[1-9]\d{5}(?!\d)");  // 户籍邮编
            frmPatient.AddItem(txtPatHouseholdRegDetail, "DRegisterAddrDetail");  // 户籍详细地址
            frmPatient.AddItem(txtPatWork, "UnitName");  // 工作单位
            frmPatient.AddItem(
                txtPatWorkTel,
                "UnitPhone",
                "请输入正确的电话号码！",
                InvalidType.Custom,
                @"^[1][358]\d{9}$|^(0\d{2,3}-)?(\d{7,8})(-(\d{1,3}))?$");  // 单位电话 
            frmPatient.AddItem(txtPatWorkNo, "UZipCode", "请输入正确的邮编！", InvalidType.Custom, @"^[1-9]\d{5}(?!\d)");  // 单位邮编
            frmPatient.AddItem(txtPatContactsAddress, "RAddress");  // 联系人地址
            frmPatient.AddItem(txtPatContactsAddressDetail, "RAddressDetail");  // 联系人详细地址
                                                                                // 入院信息
            frmPatient.AddItem(txtSerialNumber, "SerialNumber");  // 住院流水号
            frmPatient.AddItem(txtPatDisease, "EnterDiseaseCode");  // 入院诊断
            frmPatient.AddItem(txtEnterWardID, "EnterWardID");  // 入院病区
            frmPatient.AddItem(txtMedicareCardNo, "MedicareCard");  // 医保卡号
            frmPatient.AddItem(txtDeptList, "EnterDeptID");  // 入院科室
            frmPatient.AddItem(txtSourceWay, "SourceWay");  // 病人来源
            frmPatient.AddItem(txtSourcePerson, "SourcePerson");  // 推荐人
            frmPatient.AddItem(txtEnterSituation, "EnterSituation");  // 入院情况
            frmPatient.AddItem(txtCurrNurse, "EnterNurseID");  // 责任护士
            frmPatient.AddItem(lblRegisterTime, "MakerDate");  // 登记时间
            frmPatient.AddItem(txtPatType, "PatTypeID");  // 病人类型
            frmPatient.AddItem(txtCurrDoctor, "EnterDoctorID");  // 主管医生
            frmPatient.AddItem(dtpAdmissionTime, "EnterHDate");  // 入院时间
        }

        /// <summary>
        /// 入院时间改变保存按钮得到焦点
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        public void dtpAdmissionTime_KeyPress(object sender, EventArgs e)
        {
            btnSave.Focus();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmPatientInfo_Load(object sender, EventArgs e)
        {
            if (!mIsNewPatient)
            {
                SetControlEnabledState(true);
                SetPatFrmControlEnabled();
                //btnReadCard.Enabled = false;
                btnHandleCard.Enabled = false;
                cboCardType.Enabled = false;
                txtCardNo.Enabled = false;
                txtDeposit.Text = string.Empty;
                txtDeposit.Enabled = false;
            }
            else
            {
                SetControlEnabledState(false);
                cboCardType.Enabled = true;
                txtCardNo.Enabled = true;
                txtCardNo.Focus();
                InvokeController("RegDataInit");
                btnHandleCard.Enabled = true;
                //btnReadCard.Enabled = true;
            }
            // 已在床，定义出院，出院的病人不允许修改病人登记信息
            if (patList.Status == 2 || patList.Status == 3 || patList.Status == 4)
            {
                txtSerialNumber.Enabled = false;
                txtPatDisease.Enabled = false;
                txtEnterWardID.Enabled = false;
                //txtMedicareCardNo.Enabled = false;
                txtDeptList.Enabled = false;
                txtSourceWay.Enabled = false;
                txtSourcePerson.Enabled = false;
                txtEnterSituation.Enabled = false;
                txtCurrNurse.Enabled = false;
                txtCurrDoctor.Enabled = false;
            }

            InvokeController("GetPaymentMethod");
            InvokeController("GetInvoiceCurNO");
        }

        /// <summary>
        /// 界面关闭
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormClose();
        }

        /// <summary>
        /// 保存新入院病人信息
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // 界面必须输入项检查
            if (ControlValueCheck())
            {
                if (frmPatient.Validate())
                {
                    InvokeController("SavePatientInfo"/*, cbkIsDeposit.Checked*/);
                }
            }
        }

        /// <summary>
        /// 国籍和名族验证
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void txtPatNationality_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPatNationality.Text.Trim()))
            {
                if (txtPatNationality.Text.Trim() != "中国")
                {
                    txtNation.MemberValue = "60";
                }
                else
                {
                    txtNation.MemberValue = "01";
                }
            }
        }

        /// <summary>
        /// 国籍和名族验证
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void txtNation_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNation.Text.Trim()))
            {
                if (txtPatNationality.Text.Trim() != "中国" && !string.IsNullOrEmpty(txtPatNationality.Text.Trim()))
                {
                    txtNation.MemberValue = "60";
                }
            }
        }

        /// <summary>
        /// 病人类型选择医保时管理医保号
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void txtPatType_TextChanged(object sender, EventArgs e)
        {
            if (txtPatType.MemberValue != null)
            {
                if (txtPatType.MemberValue.Equals(101) ||
                    txtPatType.MemberValue.Equals(100) ||
                    txtPatType.MemberValue.Equals(102) ||
                    txtPatType.MemberValue.Equals(104) ||
                    txtPatType.MemberValue.Equals(107) ||
                    string.IsNullOrEmpty(txtPatType.Text))
                {
                    txtMedicareCardNo.ReadOnly = false;
                    txtMedicareCardNo.TabStop = true;
                }
                else
                {
                    txtMedicareCardNo.ReadOnly = true;
                    txtMedicareCardNo.Text = string.Empty;
                    txtMedicareCardNo.TabStop = false;
                }
            }
        }

        /// <summary>
        /// 限制年龄控件只能输入有效数字
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void txtPatAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' && this.txtPatAge.Text.IndexOf(".") != -1)
            {
                e.Handled = true;
            }

            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == '.' || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 控制邮编控件输入正确的值
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void txtPatHouseholdRegNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' && this.txtPatHouseholdRegNo.Text.IndexOf(".") != -1)
            {
                e.Handled = true;
            }

            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == '.' || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 控制邮编控件输入正确的值
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void txtPatPresentAddressNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' && this.txtPatPresentAddressNo.Text.IndexOf(".") != -1)
            {
                e.Handled = true;
            }

            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == '.' || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 控制邮编控件输入正确的值
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void txtPatWorkNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' && this.txtPatWorkNo.Text.IndexOf(".") != -1)
            {
                e.Handled = true;
            }

            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == '.' || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 根据会员卡号获取病人信息
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtCardNo.Text.Trim()))
            //{
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        InvokeController("QueryMemberInfo");
            //        txtPatDatCardNo.ReadOnly = true;
            //    }
            //}
        }

        #endregion

        #region "私有方法"

        /// <summary>
        /// 界面必须输入项检查
        /// </summary>
        /// <returns>检查成功或失败</returns>
        private bool ControlValueCheck()
        {
            // 姓名
            if (string.IsNullOrEmpty(txtPatName.Text.Trim()))
            {
                InvokeController("MessageShow", "姓名不能为空！");
                return false;
            }
            // 出生年月
            if (dtpPatbriDate.Value != null)
            {
                if (dtpPatbriDate.Value > DateTime.Now || dtpPatbriDate.Value == DateTime.MinValue)
                {
                    InvokeController("MessageShow", "请输入正确的出生日期！");
                    return false;
                }
            }
            // 身份证
            if (!string.IsNullOrEmpty(txtPatNumber.Text.Trim()))
            {
                if (!CheckIDCard(txtPatNumber.Text.Trim()))
                {
                    InvokeController("MessageShow", "请输入有效身份证件！");
                    return false;
                }
            }
            //else
            //{
            //    InvokeController("MessageShow", "身份证不能为空！");
            //    return false;
            //}
            // 性别
            if (string.IsNullOrEmpty(txtPatSex.Text.Trim()))
            {
                InvokeController("MessageShow", "请选择正确的性别！");
                return false;
            }
            // 病区
            if (string.IsNullOrEmpty(txtEnterWardID.Text.Trim()))
            {
                InvokeController("MessageShow", "请选择病区！");
                return false;
            }
            // 病人类型
            if (string.IsNullOrEmpty(txtPatType.Text.Trim()))
            {
                InvokeController("MessageShow", "请选择病人类型！");
                return false;
            }
            // 入院科室
            if (string.IsNullOrEmpty(txtDeptList.Text.Trim()))
            {
                InvokeController("MessageShow", "请选择科室！");
                return false;
            }
            // 病人来源
            if (string.IsNullOrEmpty(txtSourceWay.Text.Trim()))
            {
                InvokeController("MessageShow", "请选病人来源！");
                return false;
            }
            // 入院情况
            if (string.IsNullOrEmpty(txtEnterSituation.Text.Trim()))
            {
                InvokeController("MessageShow", "请选入院情况！");
                return false;
            }

            // 病案号
            if (string.IsNullOrEmpty(txtPatCaseNo.Text.Trim()))
            {
                InvokeController("MessageShow", "病案号不能为空！");
                return false;
            }

            // 责任护士
            //if (string.IsNullOrEmpty(txtCurrNurse.Text.Trim()))
            //{
            //    InvokeController("MessageShow", "请选责任护士！");
            //    return false;
            //}
            // 入院诊断
            //if (string.IsNullOrEmpty(txtPatDisease.Text.Trim()))
            //{
            //    InvokeController("MessageShow", "请选入院诊断！");
            //    return false;
            //}
            // 主管医生
            //if (string.IsNullOrEmpty(txtCurrDoctor.Text.Trim()))
            //{
            //    InvokeController("MessageShow", "请选主管医生！");
            //    return false;
            //}
            return true;
        }

        /// <summary>
        /// 身份证验证
        /// </summary>
        /// <param name="id">身份证号</param>
        /// <returns>身份证验证通过或不通过</returns>
        private bool CheckIDCard(string id)
        {
            if (id.Length == 18)
            {
                bool check = CheckIDCard18(id);
                return check;
            }
            else if (id.Length == 15)
            {
                bool check = CheckIDCard15(id);
                return check;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 18位身份证验证
        /// </summary>
        /// <param name="id">身份证号</param>
        /// <returns>身份证验证通过或不通过</returns>
        private bool CheckIDCard18(string id)
        {
            long n = 0;
            if (long.TryParse(id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(id.Remove(2)) == -1)
            {
                return false;//省份验证
            }

            string birth = id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }

            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] ai = id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(wi[i]) * int.Parse(ai[i].ToString());
            }

            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }

            return true;//符合GB11643-1999标准
        }

        /// <summary>
        /// 15位身份证验证
        /// </summary>
        /// <param name="id">身份证号</param>
        /// <returns>身份证验证通过或不通过</returns>
        private bool CheckIDCard15(string id)
        {
            long n = 0;
            if (long.TryParse(id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(id.Remove(2)) == -1)
            {
                return false;//省份验证
            }

            string birth = id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }

            return true;//符合15位身份证标准
        }

        /// <summary>
        /// 关闭新病人入院信息录入界面
        /// </summary>
        public void FormClose()
        {
            this.Close();
        }

        /// <summary>
        /// 获取病人国籍列表
        /// </summary>
        /// <param name="nationalityDt">病人国籍列表</param>
        public void Bind_txtPatNationality(DataTable nationalityDt)
        {
            txtPatNationality.MemberField = "Code";
            txtPatNationality.DisplayField = "Name";
            txtPatNationality.CardColumn = "Name|名称|auto";
            txtPatNationality.QueryFieldsString = "Code,Name,Pym,Wbm,Szm";
            txtPatNationality.ShowCardWidth = 80;
            txtPatNationality.ShowCardDataSource = nationalityDt;
        }

        /// <summary>
        /// 获取病人民族列表
        /// </summary>
        /// <param name="nationDt">病人民族列表</param>
        public void Bind_txtNation(DataTable nationDt)
        {
            txtNation.MemberField = "Code";
            txtNation.DisplayField = "Name";
            txtNation.CardColumn = "Name|名称|auto";
            txtNation.QueryFieldsString = "Code,Name,Pym,Wbm,Szm";
            txtNation.ShowCardWidth = 80;
            txtNation.ShowCardDataSource = nationDt;
        }

        /// <summary>
        /// 获取病人职业列表
        /// </summary>
        /// <param name="jodListDt">病人职业列表</param>
        public void Bind_txtPatJob(DataTable jodListDt)
        {
            txtPatJob.MemberField = "Code";
            txtPatJob.DisplayField = "Name";
            txtPatJob.CardColumn = "Code|编码|100,Name|名称|auto";
            txtPatJob.QueryFieldsString = "Code,Name,Pym,Wbm,Szm";
            txtPatJob.ShowCardWidth = 350;
            txtPatJob.ShowCardDataSource = jodListDt;
        }

        /// <summary>
        /// 获取病人教育程度列表
        /// </summary>
        /// <param name="culturalLevelDt">病人教育程度列表</param>
        public void Bind_txtCulturalLevel(DataTable culturalLevelDt)
        {
            txtCulturalLevel.MemberField = "Code";
            txtCulturalLevel.DisplayField = "Name";
            txtCulturalLevel.CardColumn = "Name|名称|auto";
            txtCulturalLevel.QueryFieldsString = "Code,Name,Pym,Wbm,Szm";
            txtCulturalLevel.ShowCardWidth = 80;
            txtCulturalLevel.ShowCardDataSource = culturalLevelDt;
        }

        /// <summary>
        /// 获取病人婚姻状况列表
        /// </summary>
        /// <param name="matrimonyDt">病人婚姻状况列表</param>
        public void Bind_Matrimony(DataTable matrimonyDt)
        {
            txtMatrimony.MemberField = "Code";
            txtMatrimony.DisplayField = "Name";
            txtMatrimony.CardColumn = "Name|名称|auto";
            txtMatrimony.QueryFieldsString = "Name,Pym,Wbm,Szm";
            txtMatrimony.ShowCardWidth = 100;
            txtMatrimony.ShowCardDataSource = matrimonyDt;
        }

        /// <summary>
        /// 获取病人与联系人关系列表
        /// </summary>
        /// <param name="relationDt">关系列表</param>
        public void Bind_txtRelation(DataTable relationDt)
        {
            txtRelation.MemberField = "Code";
            txtRelation.DisplayField = "Name";
            txtRelation.CardColumn = "Name|名称|100";
            txtRelation.QueryFieldsString = "Code,Name,Pym,Wbm,Szm";
            txtRelation.ShowCardWidth = 100;
            txtRelation.ShowCardDataSource = relationDt;
        }

        /// <summary>
        /// 获取病人性别列表
        /// </summary>
        /// <param name="sexDt">病人性别列表</param>
        public void Bind_PatSex(DataTable sexDt)
        {
            txtPatSex.MemberField = "Code";
            txtPatSex.DisplayField = "Name";
            txtPatSex.CardColumn = "Name|名称|50";
            txtPatSex.QueryFieldsString = "Name,Pym,Wbm,Szm";
            txtPatSex.ShowCardWidth = 50;
            txtPatSex.ShowCardDataSource = sexDt;
        }

        /// <summary>
        /// 获取病人入院情况列表
        /// </summary>
        /// <param name="enterSituationDt">病人入院情况列表</param>
        public void Bind_EnterSituation(DataTable enterSituationDt)
        {
            txtEnterSituation.MemberField = "Code";
            txtEnterSituation.DisplayField = "Name";
            txtEnterSituation.CardColumn = "Code|编码|100,Name|名称|auto";
            txtEnterSituation.QueryFieldsString = "Code,Name,Pym,Wbm,Szm";
            txtEnterSituation.ShowCardWidth = 350;
            txtEnterSituation.ShowCardDataSource = enterSituationDt;
        }

        /// <summary>
        /// 获取病人地区编码列表
        /// </summary>
        /// <param name="regionCodeDt">病人地区编码列表</param>
        public void Bind_RegionCode(DataTable regionCodeDt)
        {
            // 出生地址
            txtBirthplace.MemberField = "Code";
            txtBirthplace.DisplayField = "Name";
            txtBirthplace.CardColumn = "Code|编码|100,Name|名称|auto";
            txtBirthplace.QueryFieldsString = "Code,Name,Pym,Wbm,Szm";
            txtBirthplace.ShowCardWidth = 350;
            txtBirthplace.ShowCardDataSource = regionCodeDt;

            // 户籍地址
            txtPatHouseholdReg.MemberField = "Code";
            txtPatHouseholdReg.DisplayField = "Name";
            txtPatHouseholdReg.CardColumn = "Code|编码|100,Name|名称|auto";
            txtPatHouseholdReg.QueryFieldsString = "Code,Name,Pym,Wbm,Szm";
            txtPatHouseholdReg.ShowCardWidth = 350;
            txtPatHouseholdReg.ShowCardDataSource = regionCodeDt;

            // 现住地址
            txtPatPresentAddress.MemberField = "Code";
            txtPatPresentAddress.DisplayField = "Name";
            txtPatPresentAddress.CardColumn = "Code|编码|100,Name|名称|auto";
            txtPatPresentAddress.QueryFieldsString = "Code,Name,Pym,Wbm,Szm";
            txtPatPresentAddress.ShowCardWidth = 350;
            txtPatPresentAddress.ShowCardDataSource = regionCodeDt;

            // 联系人地址
            txtPatContactsAddress.MemberField = "Code";
            txtPatContactsAddress.DisplayField = "Name";
            txtPatContactsAddress.CardColumn = "Code|编码|100,Name|名称|auto";
            txtPatContactsAddress.QueryFieldsString = "Code,Name,Pym,Wbm,Szm";
            txtPatContactsAddress.ShowCardWidth = 350;
            txtPatContactsAddress.ShowCardDataSource = regionCodeDt;
        }

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="deptListDt">科室列表</param>
        public void Bind_cboDeptList(DataTable deptListDt)
        {
            txtDeptList.MemberField = "DeptId";
            txtDeptList.DisplayField = "Name";
            txtDeptList.CardColumn = "Name|名称|auto";
            txtDeptList.QueryFieldsString = "Name,Pym,Wbm,Szm";
            txtDeptList.ShowCardWidth = 350;
            txtDeptList.ShowCardDataSource = deptListDt;
        }

        /// <summary>
        /// 绑定病人类型列表
        /// </summary>
        /// <param name="patTypeListDt">病人类型列表</param>
        public void Bind_cboPatType(DataTable patTypeListDt)
        {
            txtPatType.MemberField = "PatTypeID";
            txtPatType.DisplayField = "PatTypeName";
            txtPatType.CardColumn = "PatTypeName|名称|auto";
            txtPatType.QueryFieldsString = "PatTypeName,PYCode";
            txtPatType.ShowCardWidth = 350;
            txtPatType.ShowCardDataSource = patTypeListDt;
        }

        /// <summary>
        /// 绑定诊断列表
        /// </summary>
        /// <param name="diseaseDt">诊断列表</param>
        public void Bind_txtPatDisease(DataTable diseaseDt)
        {
            txtPatDisease.MemberField = "ICDCode";
            txtPatDisease.DisplayField = "Name";
            txtPatDisease.CardColumn = "ICDCode|编码|95,Name|名称|auto";
            txtPatDisease.QueryFieldsString = "ICDCode,Name,PYCode,WBCode";
            txtPatDisease.ShowCardWidth = 350;
            txtPatDisease.ShowCardDataSource = diseaseDt;
        }

        /// <summary>
        /// 绑定医生列表
        /// </summary>
        /// <param name="currDoctorDt">医生列表</param>
        public void Bind_txtCurrDoctor(DataTable currDoctorDt)
        {
            txtCurrDoctor.MemberField = "EmpId";
            txtCurrDoctor.DisplayField = "Name";
            txtCurrDoctor.CardColumn = "Name|名称|auto";
            txtCurrDoctor.QueryFieldsString = "Name,Pym,Wbm,Szm";
            txtCurrDoctor.ShowCardWidth = 80;
            txtCurrDoctor.ShowCardDataSource = currDoctorDt;
        }

        /// <summary>
        /// 绑定护士列表
        /// </summary>
        /// <param name="currNurseDt">护士列表</param>
        public void Bind_txtCurrNurse(DataTable currNurseDt)
        {
            txtCurrNurse.MemberField = "EmpId";
            txtCurrNurse.DisplayField = "Name";
            txtCurrNurse.CardColumn = "Name|名称|auto";
            txtCurrNurse.QueryFieldsString = "Name,Pym,Wbm,Szm";
            txtCurrNurse.ShowCardWidth = 80;
            txtCurrNurse.ShowCardDataSource = currNurseDt;
        }

        /// <summary>
        /// 获取病人来源列表
        /// </summary>
        /// <param name="sourceWayDt">病人来源列表</param>
        public void Bind_txtSourceWay(DataTable sourceWayDt)
        {
            txtSourceWay.MemberField = "Code";
            txtSourceWay.DisplayField = "Name";
            txtSourceWay.CardColumn = "Code|编码|65,Name|名称|auto";
            txtSourceWay.QueryFieldsString = "Code,Name,Pym,Wbm,Szm";
            txtSourceWay.ShowCardWidth = 150;
            txtSourceWay.ShowCardDataSource = sourceWayDt;
        }

        /// <summary>
        /// 获取病区列表
        /// </summary>
        /// <param name="enterWardIDDt">病区列表</param>
        public void Bind_txtEnterWardID(DataTable enterWardIDDt)
        {
            txtEnterWardID.MemberField = "DeptId";
            txtEnterWardID.DisplayField = "Name";
            txtEnterWardID.CardColumn = "Name|名称|auto";
            txtEnterWardID.QueryFieldsString = "Name,Pym,Wbm,Szm";
            txtEnterWardID.ShowCardWidth = 150;
            txtEnterWardID.ShowCardDataSource = enterWardIDDt;
        }

        /// <summary>
        /// 绑定卡类型列表
        /// </summary>
        /// <param name="dtCardType">卡类型列表</param>
        public void LoadCardType(DataTable dtCardType)
        {
            cboCardType.DataSource = dtCardType;
            cboCardType.ValueMember = "CardTypeId";
            cboCardType.DisplayMember = "CardTypeName";
        }
        #endregion

        /// <summary>
        /// 设置界面控件是否可用
        /// </summary>
        /// <param name="controlEnabledState">控件状态</param>
        private void SetControlEnabledState(bool controlEnabledState)
        {
            foreach (Control item in panelEx2.Controls)
            {
                if (item is LabelX)
                {
                    continue;
                }

                item.Enabled = controlEnabledState;
            }

            foreach (Control item in tabControlPanel1.Controls)
            {
                if (item is LabelX)
                {
                    continue;
                }

                item.Enabled = controlEnabledState;
            }

            txtSerialNumber.Enabled = true;
            txtSerialNumber.ReadOnly = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        /// <summary>
        /// 办卡
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnHandleCard_Click(object sender, EventArgs e)
        {
            InvokeController("AddMemberInfo");
        }

        /// <summary>
        /// 读卡、办卡后设置部分控件可用
        /// </summary>
        public void SetPatFrmControlEnabled()
        {
            SetControlEnabledState(true);
            txtPatCureNum.Enabled = false; //住院次数
            txtPatDatCardNo.Enabled = true; //诊疗卡号
            txtPatDatCardNo.ReadOnly = true;
            //txtPatCaseNo.Enabled = false; //病案流水号
            cboCardType.Enabled = false; //卡类型
            txtCardNo.Enabled = false; //卡号
            btnHandleCard.Enabled = false; //办卡按钮
            //btnReadCard.Enabled = false; //读卡按钮
            //txtPatAge.Enabled = false; //年龄按钮
            txtPatAge.ReadOnly = true;
            cboPatAgeType.Enabled = false; //年龄选择控件
            txtSerialNumber.ReadOnly = true; //住院流水号
            this.ActiveControl = txtPatName;
            txtPatName.Focus();
            SetInvoiceControlEnabled(invoiceControlEnabled, false);
        }

        /// <summary>
        /// 界面打开后设置焦点
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmPatientInfo_Shown(object sender, EventArgs e)
        {
            if (mIsNewPatient)
            {
                txtCardNo.Focus();
            }
            else
            {
                txtPatName.Focus();
            }
        }

        /// <summary>
        /// 读卡、办卡后设置部分控件可用
        /// </summary>
        public void SetCardControlEnabled()
        {
            cboCardType.Enabled = true;
            txtCardNo.Enabled = true;
            btnHandleCard.Enabled = true;
            //btnReadCard.Enabled = true;
        }

        /// <summary>
        /// 当医保控件不可用时，为科室控件设置焦点
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void txtMedicareCardNo_Enter(object sender, EventArgs e)
        {
            if (txtMedicareCardNo.ReadOnly)
            {
                txtDeptList.Focus();
            }
        }

        /// <summary>
        /// 登记时间回车设置焦点
        /// </summary>
        /// <param name="sender">lblRegisterTime</param>
        /// <param name="e">事件参数</param>
        private void lblRegisterTime_Enter(object sender, EventArgs e)
        {
            txtPatType.Focus();
        }

        /// <summary>
        /// 详细联系人地址输入值光标跳转
        /// </summary>
        /// <param name="sender">txtPatContactsAddressDetail</param>
        /// <param name="e">事件参数</param>
        private void txtPatContactsAddressDetail_KeyDown(object sender, KeyEventArgs e)
        {
            txtSerialNumber.Focus();
        }

        /// <summary>
        /// 选定民族光标跳转
        /// </summary>
        /// <param name="sender">txtNation</param>
        /// <param name="e">事件参数</param>
        private void txtNation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPatDatCardNo.Focus();
            }
        }

        /// <summary>
        /// 选定生日光标跳转
        /// </summary>
        /// <param name="sender">dtpPatbriDate</param>
        /// <param name="e">事件参数</param>
        private void dtpPatbriDate_ValueChanged(object sender, EventArgs e)
        {
            EfwControls.Common.AgeValue ag = EfwControls.Common.AgeExtend.GetAgeValue(this.dtpPatbriDate.Value);
            string age = ag.ReturnAgeStr_EN();
            SetAge(ag.ReturnAgeStr_EN());
        }

        /// <summary>
        /// 验证预交金格式
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void txtDeposit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' && this.txtDeposit.Text.IndexOf(".") != -1)
            {
                e.Handled = true;
            }

            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == '.' || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 绑定预交金金额
        /// </summary>
        public void Bind_Deposit()
        {
            txtDeposit.Text = mDeposit.ToString();
        }

        /// <summary>
        /// 设置预交金票据号
        /// </summary>
        /// <param name="billNumber">预交金票据号</param>
        public void SetBillNumber(string billNumber)
        {
            txtBillNumber.Text = billNumber.ToString();
        }

        /// <summary>
        /// 绑定预交金支付方式
        /// </summary>
        /// <param name="patMethodDt">预交金支付方式列表</param>
        public void Binding_PaymentMethod(DataTable patMethodDt)
        {
            cboPaymentMethod.DataSource = patMethodDt;
            cboPaymentMethod.ValueMember = "Code";
            cboPaymentMethod.DisplayMember = "Name";
            cboPaymentMethod.SelectedIndex = 0;
        }

        /// <summary>
        /// 设置是否可收取预交金
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="isLast">首次加载</param>
        public void SetInvoiceControlEnabled(bool status, bool isLast)
        {
            if (isLast)
            {
                invoiceControlEnabled = status;
            }

            txtDeposit.Enabled = status;
            txtBillNumber.Enabled = status;
            if (!status)
            {
                txtDeposit.Text = string.Empty;
                txtBillNumber.Text = string.Empty;
            }
        }

        /// <summary>
        /// 根据病人姓名、手机号、身份证号、医保卡号检索病人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCardNo.Text.Trim() != string.Empty)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //MedicardReadInfo = string.Empty;//清空医保显示信息
                    //SetMedicardReadInfo = string.Empty;
                    //SetTimeRange();
                    InvokeController("ShowFrmQueryMenber", txtCardNo.Text.Trim());
                    //txtPatDatCardNo.ReadOnly = true;
                    //txtCardNO.Clear();
                    //txtRegType.Focus();
                }
            }
        }
    }
}