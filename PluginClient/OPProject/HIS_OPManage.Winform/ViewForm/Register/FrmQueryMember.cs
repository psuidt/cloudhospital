using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    public partial class FrmQueryMember : BaseFormBusiness,IFrmQueryMember
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmQueryMember()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取当前选定的行
        /// </summary>
        public int GetCurRowIndex
        {
            get
            {
                if (dgPatInfo.CurrentCell != null)
                {
                    return dgPatInfo.CurrentCell.RowIndex;
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 获取当前会员信息数据源
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetPatInfoDatable()
        {
            return (DataTable)dgPatInfo.DataSource;
        }

        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {        
            if (txtQueryContent.Text.Trim()==string.Empty)
            {
                MessageBoxEx.Show("请输入查询条件");
                return;
            }

            InvokeController("GetMemberInfoByOther",txtQueryContent.Text.Trim());
        }

        /// <summary>
        /// 绑定查询到的会员信息列表
        /// </summary>
        /// <param name="dtPatInfo">病人信息表</param>
        public void LoadPatInfo(DataTable dtPatInfo)
        {
            dgPatInfo.DataSource = dtPatInfo;
        }

        /// <summary>
        /// 窗体Load事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmQueryMember_Load(object sender, EventArgs e)
        {
            txtQueryContent.Clear();
            txtQueryContent.Focus();
            dgPatInfo.DataSource = null;
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
        /// 病人列表网格双击事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgPatInfo_DoubleClick(object sender, EventArgs e)
        {
            InvokeController("GetSelectQueryMember");//获取选取会员信息
            this.Close();
        }

        /// <summary>
        /// 选择按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            InvokeController("GetSelectQueryMember");//获取选取会员信息
            this.Close();
        }

        /// <summary>
        /// 窗体KeyUp事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmQueryMember_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// 查询条件KeyDown事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void txtQueryContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtQueryContent.Text.Trim() != string.Empty && e.KeyCode == Keys.Enter)
            {
                btnQuery.Focus();
            }
        }

        /// <summary>
        /// 病人信息网格KeyDown事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgPatInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                InvokeController("GetSelectQueryMember");//获取选取会员信息
                this.Close();
            }
        }
    }
}
