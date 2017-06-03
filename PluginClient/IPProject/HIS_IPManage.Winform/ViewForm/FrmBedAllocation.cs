using System;
using System.Data;
using System.Windows.Forms;
using EfwControls.Common;
using EfwControls.HISControl.BedCard.Controls;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    /// <summary>
    /// 床位分配界面
    /// </summary>
    public partial class FrmBedAllocation : BaseFormBusiness, IBedAllocation
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmBedAllocation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 床位实体
        /// </summary>
        public BedInfo BedInfo = new BedInfo();

        /// <summary>
        /// 床位对象
        /// </summary>
        public BedInfo Bed
        {
            get
            {
                return BedInfo;
            }

            set
            {
                BedInfo = value;
            }
        }

        /// <summary>
        /// 病区ID
        /// </summary>
        private int mWardID;

        /// <summary>
        /// 病区ID
        /// </summary>
        public int WardID
        {
            get
            {
                return mWardID;
            }

            set
            {
                mWardID = value;
            }
        }

        /// <summary>
        /// 床位医生ID
        /// </summary>
        private int doctorId = 0;

        /// <summary>
        /// 床位护士ID
        /// </summary>
        private int nurseId = 0;

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 绑定未分配床位病人列表
        /// </summary>
        /// <param name="patListDt">未分配床位病人列表</param>
        public void Bind_NotHospitalPatList(DataTable patListDt)
        {
            grdPatList.DataSource = patListDt;
        }

        /// <summary>
        /// 绑定医生列表
        /// </summary>
        /// <param name="doctorDt">医生列表</param>
        /// <param name="doctorId">默认医生ID</param>
        public void Bind_txtCurrDoctor(DataTable doctorDt, int doctorId)
        {
            txtDoctor.MemberField = "EmpId";
            txtDoctor.DisplayField = "Name";
            txtDoctor.CardColumn = "Name|名称|auto";
            txtDoctor.QueryFieldsString = "Name,Pym,Wbm,Szm";
            txtDoctor.ShowCardWidth = 350;
            txtDoctor.ShowCardDataSource = doctorDt;
            txtDoctor.MemberValue = doctorId;
            this.doctorId = doctorId;
        }

        /// <summary>
        /// 绑定护士列表并设置默认值
        /// </summary>
        /// <param name="nurseDt">护士列表</param>
        /// <param name="nurseId">默认护士ID</param>
        public void Bind_txtCurrNurse(DataTable nurseDt, int nurseId)
        {
            txtNurse.MemberField = "EmpId";
            txtNurse.DisplayField = "Name";
            txtNurse.CardColumn = "Name|名称|auto";
            txtNurse.QueryFieldsString = "Name,Pym,Wbm,Szm";
            txtNurse.ShowCardWidth = 350;
            txtNurse.ShowCardDataSource = nurseDt;
            txtNurse.MemberValue = nurseId;
            this.nurseId = nurseId;
        }

        /// <summary>
        /// 查询出院召回的病人
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void chkDefinedDischarge_CheckedChanged(object sender, EventArgs e)
        {
            // 查询新入院和出院召回的病人
            if (chkDefinedDischarge.Checked)
            {
                chkDept.Checked = false;
                InvokeController("GetNotHospitalPatList", mWardID, "3");
            }
            else
            {
                // 只查询新入院的病人
                InvokeController("GetNotHospitalPatList", mWardID, "1");
            }
        }

        /// <summary>
        /// 查询转科病人列表
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void chkDept_CheckedChanged(object sender, EventArgs e)
        {
            // 查询新入院和出院召回的病人
            if (chkDept.Checked)
            {
                chkDefinedDischarge.Checked = false;
                InvokeController("GetTransferPatList", mWardID);
            }
            else
            {
                // 只查询新入院的病人
                InvokeController("GetNotHospitalPatList", mWardID, "1");
            }
        }

        /// <summary>
        /// 保存床位分配数据
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (grdPatList.CurrentCell != null)
            {
                if (string.IsNullOrEmpty(txtDoctor.Text.Trim()))
                {
                    InvokeController("MessageShow", "主管医生不能为空！");
                    return;
                }

                if (string.IsNullOrEmpty(txtNurse.Text.Trim()))
                {
                    InvokeController("MessageShow", "责任护士不能为空！");
                    return;
                }

                int rowIndex = grdPatList.CurrentCell.RowIndex;
                DataTable dt = grdPatList.DataSource as DataTable;
                DataTable tempDt = dt.Clone();
                DataRow dr = tempDt.NewRow();
                dr.ItemArray = dt.Rows[rowIndex].ItemArray;
                dr["CurrDoctorID"] = txtDoctor.MemberValue == null ? 0 : txtDoctor.MemberValue;
                dr["CurrNurseID"] = txtNurse.MemberValue == null ? 0 : txtNurse.MemberValue;
                tempDt.Rows.Add(dr);
                // 保存床位分配
                InvokeController("SaveBedAllocation", tempDt);
            }
            else
            {
                // 提示选择病人Msg
                InvokeController("MessageShow", "请选择一个需要分配床位的病人！");
            }
        }

        /// <summary>
        /// 双击网格分配床位
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void grdPatList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSave_Click(sender, e);
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        public void CloseForm()
        {
            this.Close();
        }

        /// <summary>
        /// 窗体打开前
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmBedAllocation_OpenWindowBefore(object sender, EventArgs e)
        {
            chkDefinedDischarge.Checked = false;
            chkDept.Checked = false;
        }

        /// <summary>
        /// 打开界面设置焦点
        /// </summary>
        /// <param name="sender">FrmBedAllocation</param>
        /// <param name="e">事件参数</param>
        private void FrmBedAllocation_Shown(object sender, EventArgs e)
        {
            txtDoctor.Focus(); 
            chkDefinedDischarge.Checked = false;
            chkDept.Checked = false;
        }

        /// <summary>
        /// 选中病人加载责任医生和护士
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void grdPatList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdPatList.CurrentCell != null)
            {
                int rowIndex = grdPatList.CurrentCell.RowIndex;
                DataTable patListDt = grdPatList.DataSource as DataTable;
                int currDoctorID = Tools.ToInt32(patListDt.Rows[rowIndex]["CurrDoctorID"]);
                int currNurseId = Tools.ToInt32(patListDt.Rows[rowIndex]["CurrNurseID"]);
                // 主管医生
                txtDoctor.MemberValue = currDoctorID > 0 ? currDoctorID : doctorId;
                // 责任护士
                txtNurse.MemberValue = currNurseId > 0 ? currNurseId : nurseId;
            }
        }
    }
}