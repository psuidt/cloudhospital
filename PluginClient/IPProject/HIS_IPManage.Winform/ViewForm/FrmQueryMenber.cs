using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    /// <summary>
    /// 入院登记病人列表
    /// </summary>
    public partial class FrmQueryMenber : BaseFormBusiness, IQueryMenber
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmQueryMenber()
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
        /// 病人姓名、手机号、医保卡号、身份证号
        /// </summary>
        private string strPatInfo = string.Empty;

        /// <summary>
        /// 病人姓名、手机号、医保卡号、身份证号
        /// </summary>
        public string StrPatInfo
        {
            get
            {
                return strPatInfo;
            }

            set
            {
                strPatInfo = value;
                txtQueryContent.Text = strPatInfo;
            }
        }

        /// <summary>
        /// 通过姓名，电话号码，身份证号组合条件或取会员信息
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtQueryContent.Text.Trim() == string.Empty)
            {
                InvokeController("MessageShow", "请输入查询条件！");
                return;
            }

            InvokeController("GetMemberInfoByOther", txtQueryContent.Text.Trim());
        }

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="patDt">病人列表</param>
        public void Bind_PatList(DataTable patDt)
        {
            if (patDt.Rows.Count > 0)
            {
                dgPatInfo.DataSource = patDt;
            }
            else
            {
                dgPatInfo.DataSource = null;
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
        /// 选择病人
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            InvokeController("GetSelectQueryMember");//获取选取会员信息
            this.Close();
        }

        /// <summary>
        /// 关闭当前界面
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 双击选择病人
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void dgPatInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            InvokeController("GetSelectQueryMember");//获取选取会员信息
            this.Close();
        }

        /// <summary>
        /// 界面加载事件
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmQueryMenber_Load(object sender, EventArgs e)
        {
            txtQueryContent.Clear();
            txtQueryContent.Focus();
        }

        /// <summary>
        /// 按下回车键自动查询
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void txtQueryContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuery_Click(null, null);
            }
        }
    }
}
