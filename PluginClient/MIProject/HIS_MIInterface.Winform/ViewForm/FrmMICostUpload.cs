using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_MIInterface.Winform.IView;
using DevComponents.DotNetBar.Controls;

namespace HIS_MIInterface.Winform.ViewForm
{
    public partial class FrmMICostUpload : BaseFormBusiness, IFrmMICostUpload
    {
        private int _ybId = 0;

        private int _iPatientID = 0;
        private string _sCaseNumber = "";
        public FrmMICostUpload()
        {
            InitializeComponent();
            dgvPatientMsg.AutoGenerateColumns = false;
            dgvCostMsg.AutoGenerateColumns = false;
            dgvCostMsgMz.AutoGenerateColumns = false;
        }
        //界面打开之前调用初始化
        private void FrmMICostUpload_OpenWindowBefore(object sender, EventArgs e)
        {
            dgvPatientMsg.DataSource = null;
            dgvCostMsg.DataSource = null;
            lbTotalFee.Text = "0";
            InitComBox();
            InvokeController("M_GetMIDept");
            cmbFeeType.SelectedIndex = 1;
            cmbMIType.SelectedIndex = 0;
            if(cmbMITypeMz.Controls.Count>0)
                cmbMITypeMz.SelectedIndex = 0;
            cmbFeeType.SelectedIndex = 1;
            cmbFeeTypeMz.SelectedIndex = 1;
            Refresh();
        }
        private void InitComBox()
        {
            cmbMIType.Items.Clear();
            cmbMITypeMz.Items.Clear();
            cmbDept.Items.Clear();
            cmbFeeType.Items.Clear();
            cmbFeeTypeMz.Items.Clear();

            InvokeController("M_GetMIType");
            
            ComboBoxItem cbxItem11 = new ComboBoxItem();
            cbxItem11.Text = "全部";
            cbxItem11.Tag = -1;
            cmbFeeType.Items.Add(cbxItem11);

            ComboBoxItem cbxItem12 = new ComboBoxItem();
            cbxItem12.Text = "未上传";
            cbxItem12.Tag = 0;
            cmbFeeType.Items.Add(cbxItem12);

            ComboBoxItem cbxItem13 = new ComboBoxItem();
            cbxItem13.Text = "已上传";
            cbxItem13.Tag = 1;
            cmbFeeType.Items.Add(cbxItem13);

            ComboBoxItem cbxItem111 = new ComboBoxItem();
            cbxItem111.Text = "全部";
            cbxItem111.Tag = -1;
            cmbFeeTypeMz.Items.Add(cbxItem111);

            ComboBoxItem cbxItem112 = new ComboBoxItem();
            cbxItem112.Text = "未上传";
            cbxItem112.Tag = 0;
            cmbFeeTypeMz.Items.Add(cbxItem112);

            ComboBoxItem cbxItem113 = new ComboBoxItem();
            cbxItem113.Text = "已上传";
            cbxItem113.Tag = 1;
            cmbFeeTypeMz.Items.Add(cbxItem113);
        }

        public void LoadDept(DataTable dtDept)
        {
            foreach (DataRow dr in dtDept.Rows)
            {
                ComboBoxItem cbxItem = new ComboBoxItem();
                cbxItem.Text = dr["Name"].ToString();
                cbxItem.Tag = dr["DeptId"];
                cmbDept.Items.Add(cbxItem);
            }

            if(dtDept.Rows.Count>0)
            {
                cmbDept.SelectedIndex = 0;
            }
        }
        public void LoadMIType(DataTable dtMemberInfoMz, DataTable dtMemberInfoZy)
        {
            foreach (DataRow dr in dtMemberInfoMz.Rows)
            {
                ComboBoxItem cbxItem = new ComboBoxItem();
                cbxItem.Text = dr["MIName"].ToString();
                cbxItem.Tag = dr["ID"];
                cbxItem.Name = dr["PatTypeID"].ToString();
                cmbMITypeMz.Items.Add(cbxItem);
            }

            foreach (DataRow dr in dtMemberInfoZy.Rows)
            {
                ComboBoxItem cbxItem = new ComboBoxItem();
                cbxItem.Text = dr["MIName"].ToString();
                cbxItem.Tag = dr["ID"];
                cbxItem.Name = dr["PatTypeID"].ToString();
                cmbMIType.Items.Add(cbxItem);
            }
        }

        public void LoadMIPatient(DataTable dt)
        {
            dgvPatientMsg.DataSource = dt;
        }

        public void LoadPatientInfo(DataTable dt)
        {
            dgvCostMsg.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                decimal dTotalFee = Convert.ToDecimal(dt.Compute("SUM(TotalFee)", ""));
                lbTotalFee.Text = dTotalFee.ToString("0.00");
            }
            else
            {
                lbTotalFee.Text ="0.00";
            }
            
        }
        private void btnPatientQuery_Click(object sender, EventArgs e)
        {
            _iPatientID = 0;
            int iMiType = 1;
            if (cmbMIType.SelectedIndex > -1)
                iMiType = Convert.ToInt32(((BaseItem)cmbMIType.SelectedItem).Tag.ToString());

            int iDeptCode = 1017;            
            if (cmbDept.SelectedIndex > -1)
                iDeptCode = Convert.ToInt32(((BaseItem)cmbDept.SelectedItem).Tag.ToString());

            InvokeController("Zy_GetMIPatient", iMiType, iDeptCode);            
        }

        private void dgvPatientMsg_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void cmbFeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_iPatientID == 0)
                return;

            int iFeeType = -1;
            if (cmbFeeType.SelectedIndex > -1)
                iFeeType = Convert.ToInt32(((BaseItem)cmbFeeType.SelectedItem).Tag.ToString());

            InvokeController("Zy_GetPatientInfo", _iPatientID, iFeeType);
            if (cmbFeeType.SelectedIndex == 1)
            {
                btnExportPatientFee.Enabled = true;
            }
            else
            {
                btnExportPatientFee.Enabled = false;
            }
        }

        private void dgvPatientMsg_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgvPatientMsg.Rows.Count > 0 && dgvPatientMsg.CurrentCell != null)
            {
                int iFeeType = -1;
                if (cmbFeeType.SelectedIndex > -1)
                    iFeeType = Convert.ToInt32(((BaseItem)cmbFeeType.SelectedItem).Tag.ToString());

                int rowindex = dgvPatientMsg.CurrentCell.RowIndex;
                _iPatientID = Convert.ToInt32(dgvPatientMsg.Rows[rowindex].Cells["PatListID"].Value);
                _sCaseNumber = dgvPatientMsg.Rows[rowindex].Cells["CaseNumber"].Value.ToString();

                InvokeController("Zy_GetPatientInfo", _iPatientID, iFeeType);
            }
        }

        private void btnExportPatientFee_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvPatientMsg.DataSource;
            DataTable dtCopy = dt.Copy();
            dtCopy.DefaultView.RowFilter = " PatListID=" + _iPatientID;

            List<DataTable> dts = new List<DataTable>();
            dts.Add(dtCopy.DefaultView.ToTable());
            dts.Add((DataTable)dgvCostMsg.DataSource);

            InvokeController("Zy_ExportPatientFee", _ybId, dts,_sCaseNumber);
        }

        private void cmbMIType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMIType.Items.Count > 0)
            {
                _ybId = Convert.ToInt32(((ComboBoxItem)cmbMIType.SelectedItem).Tag);
                int iMode = Convert.ToInt32(((ComboBoxItem)cmbMIType.SelectedItem).Name);
                InitVisible(iMode);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        public void Refresh()
        {
            btnPatientQuery_Click(null, null);
        }
        /// <summary>
        /// 根据接口类型设置按钮可见
        /// </summary>
        /// <param name="iMode">1：集成2：外挂</param>
        private void InitVisible(int iMode)
        {
            switch (iMode)
            {
                case 1:
                    btnUpload.Visible = true;
                    btnUploadAll.Visible = true;
                    btnWriteOff.Visible = true;
                    btnExportPatientFee.Visible = false;
                    break;
                case 2:
                    btnUpload.Visible = false;
                    btnUploadAll.Visible = false;
                    btnWriteOff.Visible = false;
                    btnExportPatientFee.Visible = true;
                    break;
            }

            bar1.Refresh();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (dgvCostMsg.Rows.Count > 0 && dgvCostMsg.CurrentCell != null)
            {
                int iFeeType = -1;
                if (cmbFeeType.SelectedIndex > -1)
                    iFeeType = Convert.ToInt32(((BaseItem)cmbFeeType.SelectedItem).Tag.ToString());

                int rowindex = dgvCostMsg.CurrentCell.RowIndex;
                int feeRecordID = Convert.ToInt32(dgvCostMsg.Rows[rowindex].Cells["FeeRecordID"].Value);

                InvokeController("Zy_ReSetzyPatFee", feeRecordID);
            }
        }

        private void dgvCostMsg_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgvCostMsg.Rows.Count > 0 && dgvCostMsg.CurrentCell != null)
            {
                if (cmbFeeType.SelectedIndex == 1)
                    return;

                int rowindex = dgvCostMsg.CurrentCell.RowIndex;
                string uploadState = dgvCostMsg.Rows[rowindex].Cells["UploadState"].Value.ToString();

                if (uploadState.Contains("未上传"))
                {
                    btnReset.Enabled = false;
                }
                else
                {
                    btnReset.Enabled = true;
                }
            }
        }

        private void dgvPatientMsg_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex < dgvPatientMsg.Rows.Count)
            {
                DataGridViewRow dgrSingle = dgvPatientMsg.Rows[e.RowIndex];
                try
                {
                    if (dgrSingle.Cells["SocialCreateNum"].Value.ToString().Trim()=="")
                    {
                        dgrSingle.DefaultCellStyle.ForeColor = Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #region 门诊
        private void btnQuery_Click(object sender, EventArgs e)
        {
            int iPatType = 1;
            if (cmbMITypeMz.SelectedIndex > -1)
                iPatType = Convert.ToInt32(((BaseItem)cmbMITypeMz.SelectedItem).Name.ToString());
            DateTime dDate = dtpFeeDate.Value;

            InvokeController("Mz_GetOutPatientFee", iPatType, dDate);
        }

        private void btnExportPatientFeeMz_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvCostMsgMz.DataSource;
            //根据PatListID 获取诊断信息OPD_DiagnosisRecord DiagnosisCode DiagnosisName

            InvokeController("Zy_ExportPatientFee", _ybId, dt);
        }

        public void LoadOutPatientFee(DataTable dt)
        {
            dgvCostMsgMz.DataSource = dt;
        }
        #endregion

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定将该条费用置为未传?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
            }
        }

        private void btnUploadAll_Click(object sender, EventArgs e)
        {
            InvokeController("Zy_UploadzyPatFee", _iPatientID, 0);
        }
    }
}
