using System;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    public partial class FrmUpdatePatientInfo : BaseFormBusiness, IUpdatePatientInfo
    {
        /// <summary>
        /// 医生Id
        /// </summary>
        public int DoctorID
        {
            get
            {
                return cboDoctor.SelectedValue == null ? 0 : Convert.ToInt32(cboDoctor.SelectedValue);
            }
        }

        /// <summary>
        /// 护士ID
        /// </summary>
        public int NurseId
        {
            get
            {
                return cboNurse.SelectedValue == null ? 0 : Convert.ToInt32(cboNurse.SelectedValue);
            }
        }

        /// <summary>
        /// 旧医生ID
        /// </summary>
        private int oldDoctorId = 0;

        /// <summary>
        /// 旧护士ID
        /// </summary>
        private int oldNurserId = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmUpdatePatientInfo()
        {
            InitializeComponent();
            frmUpdPatDoctorNurse.AddItem(cboDoctor, string.Empty);
            frmUpdPatDoctorNurse.AddItem(cboNurse, string.Empty);
        }

        /// <summary>
        /// 绑定医生裂变
        /// </summary>
        /// <param name="doctorDt">医生列表</param>
        /// <param name="doctorId">默认医生ID</param>
        public void Bind_CurrDoctor(DataTable doctorDt, int doctorId)
        {
            cboDoctor.DataSource = doctorDt;
            cboDoctor.ValueMember = "EmpId";
            cboDoctor.DisplayMember = "Name";
            cboDoctor.SelectedValue = doctorId;
            oldDoctorId = doctorId;
        }

        /// <summary>
        /// 绑定护士列表
        /// </summary>
        /// <param name="nurseDt">护士列表</param>
        /// <param name="nurseId">默认护士ID</param>
        /// <param name="patName">病人名</param>
        public void Bind_CurrNurse(DataTable nurseDt, int nurseId, string patName)
        {
            cboNurse.DataSource = nurseDt;
            cboNurse.ValueMember = "EmpId";
            cboNurse.DisplayMember = "Name";
            cboNurse.SelectedValue = nurseId;
            txtPatName.Text = patName;
            oldNurserId = nurseId;
        }

        /// <summary>
        /// 保存更换后的医生或护士数据
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnSavePat_Click(object sender, EventArgs e)
        {
            if ((cboDoctor.SelectedValue == null ? 0 : Convert.ToInt32(cboDoctor.SelectedValue)) != oldDoctorId ||
                (cboNurse.SelectedValue == null ? 0 : Convert.ToInt32(cboNurse.SelectedValue)) != oldNurserId)
            {
                InvokeController("SaveUpdatePatient");
            }
            else
            {
                MessageBox.Show("医生和护士没有修改！");
            }
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ColseForm();// 关闭窗体
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        public void ColseForm()
        {
            this.Close();
        }
    }
}