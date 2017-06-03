using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.OPManage;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    public partial class FrmRegister : BaseFormBusiness, IFrmRegister
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmRegister()
        {
            InitializeComponent();
        }

        #region 接口实现
        /// <summary>
        /// 获取选择的卡类型
        /// </summary>
        public int GetCardTypeID
        {
            get
            {
                return cmbCardType.SelectedValue == null ? -1 : Convert.ToInt32(cmbCardType.SelectedValue);
            }
        }

        /// <summary>
        /// 获取卡号码
        /// </summary>
        public string cardNo
        {
            get
            {
                return txtCardNO.Text.Trim();
            }
        }

        /// <summary>
        /// 当天挂号所有科室
        /// </summary>
        DataTable dtallDept;

        /// <summary>
        /// 当天挂号所有科室
        /// </summary>
        public DataTable dtAllDept
        {
            get
            {
                return dtallDept;
            }

            set
            {
                dtallDept = value;
            }
        }

        /// <summary>
        /// 当天挂号所有医生
        /// </summary>
        DataTable dtalldoctor;

        /// <summary>
        /// 当天挂号所有医生
        /// </summary>
        public DataTable dtAllDoctor
        {
            get
            {
                return dtalldoctor;
            }

            set
            {
                dtalldoctor = value;
            }
        }

        /// <summary>
        /// 当前挂号类别ID
        /// </summary>
        public int GetCurRegTypeid
        {
            get
            {
                return Convert.ToInt32(txtRegType.MemberValue);
            }
        }

        /// <summary>
        /// 时间 1上午2下午3晚上
        /// </summary>
        public int timeRangeIndex
        {
            get
            {
                return cmbTimeRange.SelectedIndex + 1;
            }

            set
            {
                cmbTimeRange.SelectedIndex = value;
            }
        }

        /// <summary>
        /// 医保刷卡显示信息
        /// </summary>
        private string medicardReadInfo;

        /// <summary>
        /// 医保刷卡显示信息
        /// </summary>
        public string MedicardReadInfo
        {
            get
            {
                return medicardReadInfo;
            }

            set
            {
                medicardReadInfo = value;
            }
        }

        /// <summary>
        /// 显示医保刷卡显示信息
        /// </summary>
       public string SetMedicardReadInfo
        {
            set
            {
                lblMedicardInfo.Text = value;
            }
        }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Mobile
        {
            get
            {
                return txtTel.Text.Trim();
            }

            set
            {
                txtTel.Text = value;
            }
        }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            get
            {
                return txtAddr.Text.Trim();
            }

            set
            {
                txtAddr.Text = value;
            }
        }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age
        {
            get
            {
                return txtAge.Text.Trim();
            }

            set
            {
                txtAge.Text = value;
            }
        }

        /// <summary>
        /// 年龄单位
        /// </summary>
        public string AgeUnit
        {
            get
            {
                return cmbAgeUnit.Text.Trim();
            }

            set
            {
                cmbAgeUnit.Text = value;
            }
        }

        /// <summary>
        /// 挂完号后清空界面控件显示信息
        /// </summary>
        public void ClearInfo()
        {
            txtRegDetp.MemberValue = string.Empty;
            txtRegDetp.Text = string.Empty;
            txtRegDoc.MemberValue = string.Empty;
            txtRegDoc.Text = string.Empty;
            txtDocProfessor.Text = string.Empty;
        }

        /// <summary>
        /// 早上开始时间
        /// </summary>
        private string regMoningBTime;

        /// <summary>
        /// 下午开始时间
        /// </summary>
        private string regAfternoonBtime;

        /// <summary>
        /// 晚上开始时间
        /// </summary>
        private string regNightBtime;

        /// <summary>
        /// 早上开始时间
        /// </summary>
        public string RegMoningBTime 
        {
            get
            {
                return regMoningBTime;
            }

            set
            {
                regMoningBTime = value;
            }
        }

        /// <summary>
        /// 下午开始时间
        /// </summary>
        public string RegAfternoonBtime 
        {
            get
            {
                return regAfternoonBtime;
            }

            set
            {
                regAfternoonBtime = value;
            }
        }

        /// <summary>
        /// 晚上开始时间
        /// </summary>
        public string RegNightBtime
        {
            get
            {
                return regNightBtime;
            }

            set
            {
                regNightBtime = value;
            }
        }

        /// <summary>
        /// 绑定当前操作员的挂号记录
        /// </summary>
        /// <param name="dtRegInfo">挂号记录</param>
        public void BindRegInfoByOperator(DataTable dtRegInfo)
        {
            if (dtRegInfo != null && dtRegInfo.Rows.Count>0)
            {
                dgRegInfo.DataSource = dtRegInfo.DefaultView;
            }
            else
            {
                dgRegInfo.DataSource = null;
            }
        }

        /// <summary>
        /// 当前挂号病人对象
        /// </summary>
        private OP_PatList curPatlist;

        /// <summary>
        /// 当前挂号病人对象
        /// </summary>
        public OP_PatList CurPatlist
        {
            get
            {
                if (curPatlist == null)
                {
                    return null;
                }

                curPatlist.RegTypeID = txtRegType.MemberValue == null || txtRegType.MemberValue.ToString() == string.Empty ? -1 :Convert.ToInt32( txtRegType.MemberValue);
                curPatlist.RegTypeName = txtRegType.Text.Trim();
                curPatlist.RegDeptID = txtRegDetp.MemberValue == null || txtRegDetp.MemberValue.ToString()== string.Empty ? -1 : Convert.ToInt32(txtRegDetp.MemberValue);
                curPatlist.RegDeptName = txtRegDetp.Text.Trim();
                curPatlist.RegTimeRange = cmbTimeRange.SelectedIndex + 1;
                curPatlist.RegEmpID = txtRegDoc.MemberValue == null || txtRegDoc.MemberValue.ToString() ==string.Empty? -1 : Convert.ToInt32(txtRegDoc.MemberValue);
                curPatlist.RegDocName = txtRegDoc.Text.Trim();
                curPatlist.PatTypeID =Convert.ToInt32( cmbPatType.SelectedValue);
                curPatlist.PatTypeName = cmbPatType.Text.ToString();
                return curPatlist;
            }

            set
            {
                curPatlist = value;
                txtPatName.Text = curPatlist.PatName;
                txtAccountNO.Text = curPatlist.CardNO;
                cmbPatSex.Text = curPatlist.PatSex;
                txtAllergic.Text = curPatlist.Allergies;
                txtIDNumber.Text = curPatlist.IDNumber;
                cmbPatType.SelectedValue = curPatlist.PatTypeID;               
            }
        }

        /// <summary>
        /// 新增会员后焦点定位
        /// </summary>
        public void AddMemberSetFocus()
        {
            txtRegType.Focus();
        }

        /// <summary>
        /// 获取卡类型
        /// </summary>
        /// <param name="dtCardType">卡类型</param>
        public void LoadCardType(DataTable dtCardType)
        {
            cmbCardType.DataSource = dtCardType;
            cmbCardType.ValueMember = "cardtypeid";
            cmbCardType.DisplayMember = "cardtypename";
        }

        /// <summary>
        /// 获取病人类型
        /// </summary>
        /// <param name="dtPatType">病人类型</param>
        public void LoadPatType(DataTable dtPatType)
        {
            cmbPatType.DataSource = dtPatType;
            cmbPatType.ValueMember = "PatTypeID";
            cmbPatType.DisplayMember = "PatTypeName";
        }

        /// <summary>
        /// 绑定挂号科室
        /// </summary>
        /// <param name="dtRegDepts">挂号科室</param>
        public void LoadRegDepts(DataTable dtRegDepts)
        {
            txtRegDetp.DisplayField = "Name";
            txtRegDetp.MemberField = "deptid";
            txtRegDetp.CardColumn = "Name|名称|auto";
            txtRegDetp.QueryFieldsString = "Name,pym,wbm";
            txtRegDetp.ShowCardHeight = 130;
            txtRegDetp.ShowCardWidth = 127;
            txtRegDetp.ShowCardDataSource = dtRegDepts;
        }

        /// <summary>
        /// 绑定挂号医生
        /// </summary>
        /// <param name="dtRegDoctors">挂号医生</param>
        public void LoadRegDoctors(DataTable dtRegDoctors)
        {
            txtRegDoc.DisplayField = "Name";
            txtRegDoc.MemberField = "EmpID";
            txtRegDoc.CardColumn = "Name|姓名|60,DocProfessionName|职称|auto";
            txtRegDoc.QueryFieldsString = "Name,pym,wbm";
            txtRegDoc.ShowCardHeight = 160;
            txtRegDoc.ShowCardWidth = 140;
            txtRegDoc.ShowCardDataSource = dtRegDoctors;
        }

        /// <summary>
        /// 绑定挂号类别
        /// </summary>
        /// <param name="dtRegType">挂号类别</param>
        public void LoadRegType(DataTable dtRegType)
        {
            txtRegType.DisplayField = "RegTypeName";
            txtRegType.MemberField = "RegTypeID";
            txtRegType.CardColumn = "RegTypeName|名称|auto";
            txtRegType.QueryFieldsString = "RegTypeName,pycode,wbcode";
            txtRegType.ShowCardHeight = 130;
            txtRegType.ShowCardWidth = 140;
            txtRegType.ShowCardDataSource = dtRegType;
        }
        #endregion

        #region 按钮事件
        /// <summary>
        /// 退号按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnBackReg_Click(object sender, EventArgs e)
        {
            InvokeController("BackRegister");
            InvokeController("GetRegInfoByOperator");
        }

        /// <summary>
        /// 挂号按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (curPatlist == null)
            {
                MessageBoxEx.Show("请先输入病人信息");
                return;
            }

            if (string.IsNullOrEmpty( curPatlist.PatName))
            {
                MessageBoxEx.Show("请先输入病人信息");
                return;
            }

            if (txtRegType.Text.Trim() == string.Empty)
            {
                MessageBoxEx.Show("请选择挂号类别");
                return;
            }

            if (txtRegDetp.Text.Trim() == string.Empty)
            {
                MessageBoxEx.Show("请选择挂号科室");
                return;
            }

            if(txtRegDoc.Text.Trim()==string.Empty)
            {
                MessageBoxEx.Show("请选择挂号医生");
                return;
            }

            if (cmbPatType.Text.Trim() == string.Empty)
            {
                MessageBoxEx.Show("请选择病人类型");
                return;
            }

            txtCardNO.Focus();
            InvokeController("RegisterPayMent");
            txtCardNO.Focus();
            BindSchedualDoc(-1);
        }

        /// <summary>
        /// 卡号回车事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void txtCardNO_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCardNO.Text.Trim() != string.Empty)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    MedicardReadInfo = string.Empty;//清空医保显示信息
                    SetMedicardReadInfo = string.Empty;
                    SetTimeRange();
                    InvokeController("QueryMemberInfo");
                    txtCardNO.Clear();
                    txtRegType.Focus();                   
                }
            }
        }

        /// <summary>
        /// 设置挂号时间段
        /// </summary>
        private void SetTimeRange()
        {
            DateTime moningBtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")+" "+regMoningBTime);
            DateTime afterNoonBtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"+" "+regAfternoonBtime));
            DateTime nightBTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"+" "+regNightBtime));
            if (DateTime.Now > moningBtime && DateTime.Now < afterNoonBtime)
            {
                if (cmbTimeRange.SelectedIndex != 0)
                {
                    cmbTimeRange.SelectedIndex = 0;
                    cmbTimeRange_SelectionChangeCommitted(null, null);
                }
            }
            else if (DateTime.Now > afterNoonBtime && DateTime.Now < nightBTime)
            {
                if (cmbTimeRange.SelectedIndex != 1)
                {
                    cmbTimeRange.SelectedIndex = 1;
                    cmbTimeRange_SelectionChangeCommitted(null, null);
                }
            }
            else
            {
                if (cmbTimeRange.SelectedIndex != 2)
                {
                    cmbTimeRange.SelectedIndex = 2;
                    cmbTimeRange_SelectionChangeCommitted(null, null);
                }
            }
        }

        /// <summary>
        /// 时间段选择检查事件
        /// </summary>
        /// <param name="index">选择的下接Index</param>
        /// <returns>bool</returns>
        private bool TimeSelectCheck(int index)
        {
            DateTime moningBtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + regMoningBTime);
            DateTime afterNoonBtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd" + " " + regAfternoonBtime));
            DateTime nightBTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd" + " " + regNightBtime));
            if (DateTime.Now > moningBtime && DateTime.Now < afterNoonBtime)
            { 
            }
            else if (DateTime.Now > afterNoonBtime && DateTime.Now < nightBTime)
            {
                if (index == 0)
                {
                    if (MessageBoxEx.Show("现在是下午时间，您确定要挂上午的号吗？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (index != 2)
                {
                    if (MessageBoxEx.Show("现在是晚上时间，您确定要挂上午或下午的号吗？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 窗体OpenWindowBefore事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmRegister_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("RegDataInit",0);
            txtCardNO.Focus();
            SetTimeRange(); 
            InvokeController("GetRegInfoByOperator");     
        }

        /// <summary>
        /// 时间段选择事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void cmbTimeRange_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (TimeSelectCheck(cmbTimeRange.SelectedIndex))
            {
                InvokeController("SelectSchedual");
                txtRegDetp.MemberValue = string.Empty;
                txtRegDetp.Text = string.Empty;
                txtRegDoc.MemberValue = string.Empty;
                txtRegDoc.Text = string.Empty;
                txtDocProfessor.Text = string.Empty;
            }
        }

        /// <summary>
        /// 病人查询
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnGetOutpatient_Click(object sender, EventArgs e)
        {
            MedicardReadInfo = string.Empty;//清空医保显示信息
            SetMedicardReadInfo = string.Empty;
            InvokeController("ShowDialog", "FrmQueryMember");
        }

        /// <summary>
        /// 挂号类别选项卡选择事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="SelectedValue">选中行</param>
        private void txtRegType_AfterSelectedRow(object sender, object SelectedValue)
        {
            cmbTimeRange.Focus();
        }

        /// <summary>
        /// 时间段选择改变事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void cmbTimeRange_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRegDetp.Focus();
            }
        }

        /// <summary>
        /// 通过选择科室过滤科室医生
        /// </summary>
        /// <param name="schedualDeptID">科室ID</param>
        private void BindSchedualDoc(int schedualDeptID)
        {
            txtRegDoc.DisplayField = "Name";
            txtRegDoc.MemberField = "EmpID";
            txtRegDoc.CardColumn = "Name|姓名|60,DocProfessionName|职称|auto";
            txtRegDoc.QueryFieldsString = "Name,pym,wbm";
            txtRegDoc.ShowCardHeight = 160;
            txtRegDoc.ShowCardWidth = 140;
            if (schedualDeptID > 0)
            {
                // 过滤数据
                DataTable dtDoc = dtAllDoctor.Clone();
                dtAllDoctor.TableName = "dtAllDoctor";

                // 过滤明细数据
                DataView docView = new DataView(dtAllDoctor);
                string sqlWhere = " DeptId = " + schedualDeptID + string.Empty;
                docView.RowFilter = sqlWhere;
                dtDoc.Merge(docView.ToTable());
                txtRegDoc.ShowCardDataSource = dtDoc;
            }
            else
            {
                txtRegDoc.ShowCardDataSource = dtAllDoctor;
            }
        }

        /// <summary>
        /// 挂号科室选项卡选择事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="SelectedValue">选中行</param>
        private void txtRegDetp_AfterSelectedRow(object sender, object SelectedValue)
        {           
            DataRow dr = (DataRow)SelectedValue;
            BindSchedualDoc(Convert.ToInt32(dr["DeptID"]));
            txtRegDoc.Focus();
        }

        /// <summary>
        /// 挂号医生选项卡选择事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="SelectedValue">选中行</param>
        private void txtRegDoc_AfterSelectedRow(object sender, object SelectedValue)
        {
            DataRow row = (DataRow)SelectedValue;
            if (txtRegDetp.Text.Trim() == string.Empty)
            {
                txtRegDetp.MemberValue = row["DeptID"];
            }

            txtDocProfessor.Text = row["DocProfessionName"].ToString();
            btnRegister.Focus();
            btnRegister_Click(null, null);
        }

        /// <summary>
        /// 刷新按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InvokeController("RegDataInit",1);
            txtCardNO.Focus();
            SetTimeRange();
            InvokeController("SelectSchedual");
            InvokeController("RegComplete");
        }

        /// <summary>
        /// 设置网格颜色
        /// </summary>
        public void SetGridColor()
        {
            foreach (DataGridViewRow row in dgRegInfo.Rows)
            {
                short status_flag = Convert.ToInt16(row.Cells["regstatus"].Value);
                Color foreColor = Color.Black;
                switch (status_flag)
                {
                    case 0:
                        foreColor = Color.Black;
                        break;
                    case 1:
                        foreColor = Color.Red;
                        break;
                }

                dgRegInfo.SetRowColor(row.Index, foreColor, true);
            }
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 医保读卡按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnReadCard_Click(object sender, EventArgs e)
        {
            MedicardReadInfo = string.Empty;//清空医保显示信息
            SetMedicardReadInfo = string.Empty;
            InvokeController("ReadMedicareCard");
            txtRegType.Focus();
        }

        /// <summary>
        /// 新办会员按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnAddMember_Click(object sender, EventArgs e)
        {
            InvokeController("AddMemberInfo");
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// 窗体KeyUp事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmRegister_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2: 
                    //办卡
                    btnAddMember_Click(sender, e);
                    break;

                case Keys.F8:
                    //挂号
                    btnRegister_Click(sender, e);
                    break;

                case Keys.F4:
                    //退号
                    btnBackReg_Click(sender, e);
                    break;

                case Keys.F5:
                    //刷新
                    btnRefresh_Click(sender, e);
                    break;

                case Keys.F11:
                   btnClose_Click(sender, e);
                    break;
            }
        }
        #endregion
    }
}
