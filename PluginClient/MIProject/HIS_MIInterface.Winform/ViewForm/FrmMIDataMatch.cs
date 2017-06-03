using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using EFWCoreLib.CoreFrame.Business;
using HIS_MIInterface.Winform.IView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HIS_MIInterface.Winform.ViewForm
{
    public partial class FrmMIDataMatch : BaseFormBusiness, IFrmMIDataMatch
    {
        private int _ybId = 1;
        private int _dataType = 1;
        private DataTable _dtHISDataInfo = null;
        private DataTable _dtMIDataInfo = null;

        public FrmMIDataMatch()
        {
            InitializeComponent();
        }
        private void FrmMIDataMatch_OpenWindowBefore(object sender, EventArgs e)
        {
            InitComBox();

        }
        private void InitComBox()
        {
            cmbMIType.Items.Clear();
            cmbDataType.Items.Clear();           

            InvokeController("M_GetMIType");

            //字典类型1.剂型2.频次 3.参保人员类别 4 收费等级 5 就诊科别
            ComboBoxItem cbxItem1 = new ComboBoxItem();
            cbxItem1.Text = "剂型";
            cbxItem1.Tag = 1;
            cmbDataType.Items.Add(cbxItem1);

            ComboBoxItem cbxItem2 = new ComboBoxItem();
            cbxItem2.Text = "频次";
            cbxItem2.Tag = 2;
            cmbDataType.Items.Add(cbxItem2);

            //ComboBoxItem cbxItem3 = new ComboBoxItem();
            //cbxItem3.Text = "参保人员类别";
            //cbxItem3.Tag = 3;
            //cmbDataType.Items.Add(cbxItem3);

            //ComboBoxItem cbxItem4 = new ComboBoxItem();
            //cbxItem4.Text = "收费等级";
            //cbxItem4.Tag = 4;
            //cmbDataType.Items.Add(cbxItem4);

            ComboBoxItem cbxItem15 = new ComboBoxItem();
            cbxItem15.Text = "就诊科别";
            cbxItem15.Tag = 5;
            cmbDataType.Items.Add(cbxItem15);

            ComboBoxItem cbxItem16 = new ComboBoxItem();
            cbxItem16.Text = "门诊收据分类";
            cbxItem16.Tag = 6;
            cmbDataType.Items.Add(cbxItem16);

            cmbDataType.SelectedIndex = 0;
        }

        #region 获取数据库数据
        /// <summary>
        /// 获取HIS目录
        /// </summary>
        private void M_GetHISDataInfo()
        {
            InvokeController("M_GetHISDataInfo",  _ybId, _dataType);
        }
        /// <summary>
        /// 获取医保目录
        /// </summary>
        private void M_GetMIDataInfo()
        {
            InvokeController("M_GetMIDataInfo", _ybId, _dataType);
        }
        /// <summary>
        /// 获取匹配目录
        /// </summary>
        private void M_GetMatchDataInfo()
        {
            InvokeController("M_GetMatchDataInfo", _ybId, _dataType);
        }
        #endregion

        #region 接口实现
        public void LoadMIType(DataTable dtMemberInfo)
        {
            foreach (DataRow dr in dtMemberInfo.Rows)
            {
                ComboBoxItem cbxItem = new ComboBoxItem();
                cbxItem.Text = dr["MIName"].ToString();
                cbxItem.Tag = dr["ID"];
                cbxItem.Name = dr["MatchMode"].ToString();
                cmbMIType.Items.Add(cbxItem);
            }

            if (dtMemberInfo.Rows.Count>0)
            {
                cmbMIType.SelectedIndex = 0;                
            }
        }

        public void LoadHISDataInfo(DataTable dt)
        {
            _dtHISDataInfo = dt;
            dgvHospItemList.DataSource = dt;
        }

        public void LoadMatchDataInfo(DataTable dt)
        {
            dgvMatchList.DataSource = dt;
        }

        public void LoadMIDataInfo(DataTable dt)
        {
            _dtMIDataInfo = dt;
            dgvYbItemList.DataSource = dt;
        }



        public bool PromptController(string text)
        {
            throw new NotImplementedException();
        }

        public void ReFresh()
        {
            tbHosSerch.Text = "";
            tbMISerch.Text = "";
            M_GetHISDataInfo();
            dgvYbItemList.DataSource = _dtMIDataInfo;
            M_GetMatchDataInfo();
        }
        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tbHosSerch.Text = "";
            tbMISerch.Text = "";
            M_GetHISDataInfo();
            M_GetMIDataInfo();
            M_GetMatchDataInfo();
        }

        private void cmbMIType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ybId = Convert.ToInt32(((ComboBoxItem)cmbMIType.SelectedItem).Tag);
        }

        private void cmbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dataType = Convert.ToInt32(((ComboBoxItem)cmbDataType.SelectedItem).Tag);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        private void btnMatch_Click(object sender, EventArgs e)
        {
            int iMIDataID = Convert.ToInt32(dgvYbItemList.CurrentRow.Cells["YBDataID"].Value.ToString().Trim());
            int iHospDataID = Convert.ToInt32(dgvHospItemList.CurrentRow.Cells["HospDataID"].Value.ToString().Trim());

            InvokeController("M_SaveMatchData", iMIDataID, _dataType, iHospDataID, _ybId);
        }

        private void btnDelMatch_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除该匹配数据？是否确定继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int rowindex = dgvMatchList.CurrentCell.RowIndex;
                string id = dgvMatchList.Rows[rowindex].Cells["ID"].Value.ToString().Trim();
                InvokeController("M_DeleteMatchData", id);
            }
        }


        private void tbHosSerch_KeyUp(object sender, KeyEventArgs e)
        {
            LocationLog(dgvHospItemList, _dtHISDataInfo, string.Format("  HospDataName like '%{0}%' or PyCode like '%{0}%' or WbCode like '%{0}%'", tbHosSerch.Text.Trim()));
        }

        private void tbMISerch_KeyUp(object sender, KeyEventArgs e)
        {
            LocationLog(dgvYbItemList, _dtMIDataInfo, string.Format("  YBDataName like '%{0}%' or PyCode like '%{0}%' or WbCode like '%{0}%'", tbMISerch.Text.Trim()));
        }        
        
        /// <summary>
        /// 过滤目录的方法
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="dt"></param>
        /// <param name="s"></param>
        private void LocationLog(DataGridViewX dgv, DataTable dt, string s)
        {
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = s;
                dgv.DataSource = dv.ToTable();
            }
        }

    }
}
