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
    public partial class FrmMIMatch : BaseFormBusiness, IFrmMIMatch
    {
        private DataTable _dtHISCatalogInfo=null;
        private DataTable _dtMICatalogInfo = null;
        private DataTable _dtMatchCatalogInfo = null;
        private int _ybId = 0;
        public FrmMIMatch()
        {
            InitializeComponent();
            dgvExamGroup.AutoGenerateColumns = false;
            dgvYbItemList.AutoGenerateColumns = false;
            dgvMatchList.AutoGenerateColumns = false;
        }
        private void FrmMIMatch_OpenWindowBefore(object sender, EventArgs e)
        {
            tbHosSerch.Text = "";
            tbMISerch.Text = "";
            tbMatch.Text = "";

            InitComBox();
            cmbMILogType.SelectedIndex = 0;
            cmbHosLogType.SelectedIndex = 2;
            cmbMatchState.SelectedIndex = 0;
            cmbMIType.SelectedIndex = 0;
        }
        private void InitComBox()
        {
            cmbMIType.Items.Clear();
            cmbHosLogType.Items.Clear();
            cmbMILogType.Items.Clear();

            InvokeController("M_GetMIType");

            ComboBoxItem cbxItem1 = new ComboBoxItem();
            cbxItem1.Text = "西药";
            cbxItem1.Tag = 1;
            cmbHosLogType.Items.Add(cbxItem1);

            ComboBoxItem cbxItem2 = new ComboBoxItem();
            cbxItem2.Text = "材料";
            cbxItem2.Tag = 2;
            cmbHosLogType.Items.Add(cbxItem2);

            ComboBoxItem cbxItem3 = new ComboBoxItem();
            cbxItem3.Text = "项目";
            cbxItem3.Tag = 3;
            cmbHosLogType.Items.Add(cbxItem3);

            ComboBoxItem cbxItem4 = new ComboBoxItem();
            cbxItem4.Text = "中草药";
            cbxItem4.Tag = 4;
            cmbHosLogType.Items.Add(cbxItem4);

            ComboBoxItem cbxItem11 = new ComboBoxItem();
            cbxItem11.Text = "西药";
            cbxItem11.Tag = 1;
            cmbMILogType.Items.Add(cbxItem11);

            ComboBoxItem cbxItem12 = new ComboBoxItem();
            cbxItem12.Text = "材料";
            cbxItem12.Tag = 2;
            cmbMILogType.Items.Add(cbxItem12);

            ComboBoxItem cbxItem13 = new ComboBoxItem();
            cbxItem13.Text = "项目";
            cbxItem13.Tag = 3;
            cmbMILogType.Items.Add(cbxItem13);
        }
        /// <summary>
        /// 根据医保类型设置控件的显示与隐藏
        /// </summary>
        /// <param name="iMode"></param>
        private void InitVisible(int iMode)
        {
            switch (iMode)
            {
                case 1:
                    btnHisLogExport.Visible = false;
                    btnMatch.Visible = true;
                    btnMILogImport.Visible = false;
                    btnSaveMiLog.Visible = false;
                    btnUpdateMIlog.Visible = true;
                    btnAutoMatch.Visible = true;
                    btnMatchDel.Visible = true;
                    btnMatchExport.Visible = false;
                    //btnImportMatchLog.Visible = false;
                    btnDrugImport.Visible = true;
                    btnItemImport.Visible = true;
                    btnSaveMatch.Visible = false;
                    btnUploadMatch.Visible = true;
                    btnDownloadAudit.Visible = true;

                    panelEx4.Width = panelEx2.Width/2;
                    expandableSplitter2.Enabled = true;
                    break;
                case 2:
                    btnHisLogExport.Visible = false;
                    btnMatch.Visible = true;
                    btnMILogImport.Visible = true;
                    btnSaveMiLog.Visible = true;
                    btnUpdateMIlog.Visible = false;
                    btnAutoMatch.Visible = true;
                    btnMatchDel.Visible = true;
                    btnMatchExport.Visible = true;
                    //btnImportMatchLog.Visible = true;
                    btnDrugImport.Visible = true;
                    btnItemImport.Visible = true;
                    btnSaveMatch.Visible = true;
                    btnUploadMatch.Visible = false;
                    btnDownloadAudit.Visible = false;

                    panelEx4.Width = panelEx2.Width / 2;
                    expandableSplitter2.Enabled = true;
                    break;
                case 3:
                    btnHisLogExport.Visible = true;
                    btnMatch.Visible = false;
                    btnMILogImport.Visible = false;
                    btnSaveMiLog.Visible = false;
                    btnUpdateMIlog.Visible = false;
                    btnAutoMatch.Visible = false;
                    btnMatchDel.Visible = false;
                    btnMatchExport.Visible = false;
                    //btnImportMatchLog.Visible = true;
                    btnDrugImport.Visible = true;
                    btnItemImport.Visible = true;
                    btnSaveMatch.Visible = true;
                    btnUploadMatch.Visible = false;
                    btnDownloadAudit.Visible = false;

                    panelEx4.Width = 0;
                    expandableSplitter2.Enabled = false;
                    break;
            }

             bar1.Refresh();
        }
        private void cmbMIType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMIType.Items.Count > 0)
            {
                _ybId = Convert.ToInt32(((ComboBoxItem)cmbMIType.SelectedItem).Tag);
                int iMode= Convert.ToInt32(((ComboBoxItem)cmbMIType.SelectedItem).Name);
                InitVisible(iMode);
            }

           // ReFresh();
        }
        /// <summary>
        /// 刷新全部数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ReFresh();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        #region 获取数据库数据
        /// <summary>
        /// 获取HIS目录
        /// </summary>
        /// <param name="catalogType">目录类型</param>
        private void GetHISCatalogInfo(int catalogType)
        {
            int stopFlag = cbUnStop.Checked ? 1 : 0;
            int matchFlag = cbUnMatch.Checked ? 1 : 0;
            InvokeController("M_GetHISCatalogInfo", catalogType, stopFlag, matchFlag, _ybId);
        }
        /// <summary>
        /// 获取医保目录
        /// </summary>
        /// <param name="catalogType">目录类型</param>
        private void GetMICatalogInfo(int catalogType)
        {
            InvokeController("M_GetMICatalogInfo", catalogType, _ybId);
        }
        /// <summary>
        /// 获取匹配目录
        /// </summary>
        /// <param name="catalogType">目录类型</param>
        private void GetMatchCatalogInfo(int catalogType)
        {
            int auditFlag = cmbMatchState.SelectedIndex - 1;
            InvokeController("M_GetMatchCatalogInfo", catalogType, _ybId, auditFlag);
        }
        #endregion

        #region 医院目录相关按键事件   
        private void btnHisLogExport_Click(object sender, EventArgs e)
        {
            int iType = Convert.ToInt32(((ComboBoxItem)cmbHosLogType.SelectedItem).Tag);
            List<DataTable> dtList = new List<DataTable>();
            dtList.Add((DataTable)dgvExamGroup.DataSource);
            InvokeController("M_ExportHisLog", _ybId, dtList, iType);
        }
        private void btnHisLogQuery_Click(object sender, EventArgs e)
        {
            RefreshHisLog();
        }

        private void RefreshHisLog()
        {
            if (cmbHosLogType.SelectedIndex == 2)
            {
                dgvExamGroup.Columns["hSpec"].Visible = false;
                dgvExamGroup.Columns["hDosage"].Visible = false;
            }
            else if (cmbHosLogType.SelectedIndex == 0)
            {
                dgvExamGroup.Columns["hSpec"].Visible = true;
                dgvExamGroup.Columns["hDosage"].Visible = true;
            }
            else
            {
                dgvExamGroup.Columns["hSpec"].Visible = true;
                dgvExamGroup.Columns["hDosage"].Visible = false;
            }

            GetHISCatalogInfo(cmbHosLogType.SelectedIndex + 1);
        }
        #endregion

        #region 医保目录相关按键事件
        private void btnMILogImport_Click(object sender, EventArgs e)
        {           
            InvokeController("M_ImportMILog", _ybId);
        }
        private void btnSaveMiLog_Click(object sender, EventArgs e)
        {
            if (dgvYbItemList.Rows.Count > 0)
            {
                if (MessageBox.Show("该操作将会把该医保类型的医保目录数据覆盖！是否确定继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataTable dt = new DataTable();
                    for (int i = 0; i < dgvYbItemList.Columns.Count; i++)
                    {
                        dt.Columns.Add(dgvYbItemList.Columns[i].DataPropertyName);
                    }

                    for (int r = 0; r < dgvYbItemList.Rows.Count; r++)
                    {
                        DataRow dr = dt.NewRow();
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            dr[c] = dgvYbItemList.Rows[r].Cells[c].Value;
                        }
                        dt.Rows.Add(dr);
                    }
                    InvokeController("M_SaveMILog", dt, _ybId);
                }
            }
            else
            {
                MessageBox.Show("无匹配数据！");
            }
        }
        #endregion

        #region 匹配目录相关按键事件

        private void ImportMatchLog_Click(object sender, EventArgs e)
        {
            InvokeController("M_ImportMatchLog", _ybId,-1);
            //btnSaveMatch.Enabled = true;
        }
        private void btnSaveMatch_Click(object sender, EventArgs e)
        {
            if (dgvMatchList.Rows.Count > 0)
            {
                if (MessageBox.Show("该操作将会把该医保类型的匹配数据和已审核数据覆盖！是否确定继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataTable dt = new DataTable();
                    for (int i = 0; i < dgvMatchList.Columns.Count; i++)
                    {
                        dt.Columns.Add(dgvMatchList.Columns[i].DataPropertyName);
                    }

                    for (int r = 0; r < dgvMatchList.Rows.Count; r++)
                    {
                        DataRow dr = dt.NewRow();
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            dr[c] = dgvMatchList.Rows[r].Cells[c].Value;
                        }
                        dt.Rows.Add(dr);
                    }
                    InvokeController("M_SaveMatchLogs", dt, _ybId);
                }
            }
            else
            {
                MessageBox.Show("无匹配数据！");
            }


        }

        private void btnMatchDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除该匹配数据？是否确定继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int rowindex = dgvMatchList.CurrentCell.RowIndex;
                string id = dgvMatchList.Rows[rowindex].Cells["ID"].Value.ToString().Trim();
                string auditFlag = dgvMatchList.Rows[rowindex].Cells["AuditFlag"].Value.ToString().Trim();
                InvokeController("M_UpdateMatchLogs", id, auditFlag);
            }
        }

        private void btnMatchExport_Click(object sender, EventArgs e)
        {

        }

        private void btnAuditReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定重置全部审核数据？该操作将删除所有审核信息！是否确定继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int rowindex = dgvMatchList.CurrentCell.RowIndex;
                string id = dgvMatchList.Rows[rowindex].Cells["ID"].Value.ToString().Trim();
                string auditFlag = dgvMatchList.Rows[rowindex].Cells["AuditFlag"].Value.ToString().Trim();
                InvokeController("M_UpdateMatchLogs", id, auditFlag);
            }
        }

        private void btnAllReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定重置全部审核数据？该操作将删除所有审核信息！是否确定继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                InvokeController("M_UpdateAllMatchLogs",_ybId);
            }
        }

        private void btnUploadMatch_Click(object sender, EventArgs e)
        {

        }

        private void btnDownloadAudit_Click(object sender, EventArgs e)
        {

        }
        private void btnMatchQuery_Click(object sender, EventArgs e)
        {
            GetMatchCatalogInfo(0);
        }
        #endregion

        #region 更新社保目录
        /// <summary>
        /// 更新社保目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateMIlog_Click(object sender, EventArgs e)
        {
            InvokeController("M_UpdateMIlog",_ybId);
        }
        #endregion

        /// <summary>
        /// 根据所录检索目录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbHosSerch_KeyUp(object sender, KeyEventArgs e)
        {
           //if (e.KeyValue != 13)
           //     return;
            LocationLog(dgvExamGroup, _dtHISCatalogInfo, string.Format("  ItemName like '%{0}%' or PyCode like '%{0}%' or WbCode like '%{0}%'",tbHosSerch.Text.Trim()));
            LocationLog(dgvYbItemList, _dtMICatalogInfo, string.Format("  ItemName like '%{0}%' or PyCode like '%{0}%' or WbCode like '%{0}%'", tbHosSerch.Text.Trim()));
            LocationLog(dgvMatchList, _dtMatchCatalogInfo, string.Format("  HospItemName like '%{0}%' or PyCode like '%{0}%' or WbCode like '%{0}%'", tbHosSerch.Text.Trim()));
        }
        private void expandableSplitter2_ExpandedChanged(object sender, ExpandedChangeEventArgs e)
        {
            if (expandableSplitter2.Expanded)
            {
                panelEx4.Width = panelEx2.Width/2;
            }
            else
            {
                panelEx4.Width = 0;
            }
        }

        private void expandableSplitter1_ExpandedChanged(object sender, ExpandedChangeEventArgs e)
        {
            if (expandableSplitter1.Expanded)
            {
                panelEx2.Height = panelEx1.Height / 2;
            }
            else
            {
                panelEx2.Height = 0;
            }
        }
        /// <summary>
        /// 根据所选行的审核状态更改按钮权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMatchList_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgvMatchList.Rows.Count > 0 && dgvMatchList.CurrentCell != null)
            {
                //int rowindex = dgvMatchList.CurrentCell.RowIndex;
                //string auditFlag = dgvMatchList.Rows[rowindex].Cells["AuditFlag"].Value.ToString().Trim();
                //switch (auditFlag) {
                //    case "0":
                //        btnMatchDel.Enabled = true;
                //        btnAuditReset.Enabled = false;
                //        break;
                //    default:
                //        btnMatchDel.Enabled = false;
                //        btnAuditReset.Enabled = true;
                //        break;
                //}
            }
        }
        /// <summary>
        /// 根据审核状态显示数据颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMatchList_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //if (e.RowIndex < dgvMatchList.Rows.Count )
            //{
            //    DataGridViewRow dgrSingle = dgvMatchList.Rows[e.RowIndex];
            //    try
            //    {
            //        if (dgrSingle.Cells["AuditFlag"].Value.ToString().Contains("2"))
            //        {
            //            dgrSingle.DefaultCellStyle.ForeColor = Color.Red;
            //        }
            //        else if (dgrSingle.Cells["AuditFlag"].Value.ToString().Contains("0"))
            //        {
            //            dgrSingle.DefaultCellStyle.ForeColor = Color.Blue;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
        }
        
        #region 控制器所用方法
        public void LoadHISCatalogInfo(DataTable dt)
        {
            _dtHISCatalogInfo = dt;
            dgvExamGroup.DataSource=dt;
        }

        public void LoadMICatalogInfo(DataTable dt)
        {
            _dtMICatalogInfo = dt;
            dgvYbItemList.DataSource = dt;
        }

        public void LoadMatchCatalogInfo(DataTable dt)
        {
            //dgvMatchList.CurrentCellChanged -= new System.EventHandler(this.dgvMatchList_CurrentCellChanged);
            _dtMatchCatalogInfo = dt;
            dgvMatchList.DataSource = dt;
            //dgvMatchList.CurrentCellChanged += new System.EventHandler(this.dgvMatchList_CurrentCellChanged);
        }

        public void LoadMIType(DataTable dtMemberInfo)
        {
            foreach (DataRow dr in dtMemberInfo.Rows)
            {
                ComboBoxItem cbxItem = new ComboBoxItem();
                cbxItem.Text = dr["MIName"].ToString();
                cbxItem.Tag = dr["ID"];
                cbxItem.Name= dr["MatchMode"].ToString();
                cmbMIType.Items.Add(cbxItem);
            }
        }

        public void ReFresh()
        {
            btnMatchDel.Enabled = false;
            btnAuditReset.Enabled = false;
            GetHISCatalogInfo(cmbHosLogType.SelectedIndex + 1);
            GetMICatalogInfo(cmbHosLogType.SelectedIndex + 1);
            GetMatchCatalogInfo(0);

        }        
        #endregion

        /// <summary>
        /// 过滤目录的方法
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="dt"></param>
        /// <param name="s"></param>
        private void LocationLog(DataGridViewX dgv,DataTable dt,string s)
        {
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = s;
                dgv.DataSource = dv.ToTable();
            }
        }

        private void cmbHosLogType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshHisLog();
        }

        private void btnDrugImport_Click(object sender, EventArgs e)
        {
            InvokeController("M_ImportMatchLog", _ybId,0);
        }

        private void btnItemImport_Click(object sender, EventArgs e)
        {
            InvokeController("M_ImportMatchLog", _ybId,1);
        }

        private void btnUpdateLevel_Click(object sender, EventArgs e)
        {
            InvokeController("M_UpdateDrugLogLevel", _ybId);
            InvokeController("M_UpdateFeeItemLogLevel", _ybId);
            InvokeController("M_UpdateMWLogLevel", _ybId);
        }
    }
}
