using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.OPManage;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    public partial class FrmOutPatient : BaseFormBusiness, IFrmOutPatient
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmOutPatient()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 界面绑定病人列表
        /// </summary>
        /// <param name="oplist">病人列表</param>
        public void SetPatLists(List<OP_PatList> oplist)
        {
            dgPatInfo.DataSource = oplist;
        }

        /// <summary>
        /// 获取当前选中的病人对象
        /// </summary>
        private OP_PatList curPatlist;

        /// <summary>
        /// 获取当前选中的病人对象
        /// </summary>
        public OP_PatList GetcurPatlist
        {
            get
            {
                if (dgPatInfo.CurrentCell != null)
                {
                    int rowindex = dgPatInfo.CurrentCell.RowIndex;
                    List<OP_PatList> listregtype = (List<OP_PatList>)dgPatInfo.DataSource;
                    curPatlist = listregtype[rowindex];
                }
                else
                {
                    curPatlist = new OP_PatList();
                }

                return curPatlist;
            }
        }

        /// <summary>
        /// 取消按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dgPatInfo.CurrentCell != null)
            {
                InvokeController("GetOutSelectPatlist");
                this.Close();
            }
        }

        /// <summary>
        /// 网格双击事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgPatInfo_DoubleClick(object sender, EventArgs e)
        {
            if (dgPatInfo.CurrentCell != null)
            {
                InvokeController("GetOutSelectPatlist");
                this.Close();
            }
        }

        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtPatName.Text.Trim() == string.Empty && txtTel.Text.Trim() == string.Empty && txtIDNumber.Text.Trim() == string.Empty && txtMediCard.Text.Trim()==string.Empty)
            {
                MessageBoxEx.Show("请输入查询条件");
                return;
            }

            InvokeController("GetRegInfoByOther", txtPatName.Text.Trim(), txtTel.Text.Trim(), txtIDNumber.Text.Trim(),txtMediCard.Text.Trim(), dtTime.Bdate.Value,dtTime.Edate.Value.AddDays(1));
        }

        /// <summary>
        /// 窗体Load事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmOutPatient_Load(object sender, EventArgs e)
        {
            dtTime.Bdate.Value =Convert.ToDateTime( DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"));
            dtTime.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            dgPatInfo.DataSource = null;
            txtPatName.Focus();
        }

        /// <summary>
        /// KeyUp事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmOutPatient_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// 窗体Shown事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmOutPatient_Shown(object sender, EventArgs e)
        {
            txtPatName.Focus();
        }
    }
}
