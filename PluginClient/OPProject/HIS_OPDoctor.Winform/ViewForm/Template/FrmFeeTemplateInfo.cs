using System;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_OPDoctor.Winform.IView.Template;

namespace HIS_OPDoctor.Winform.ViewForm.Template
{
    /// <summary>
    /// 模板信息
    /// </summary>
    public partial class FrmFeeTemplateInfo : BaseFormBusiness, IFrmFeeTemplateInfo
    {
        /// <summary>
        /// 处方模板头
        /// </summary>
        private OPD_PresMouldHead head;

        /// <summary>
        /// 处方模板头
        /// </summary>
        public OPD_PresMouldHead Head
        {
            get { return head; }
            set { head = value; }
        }

        /// <summary>
        /// 是否模板分类
        /// </summary>
        public bool ResNode
        {
            get { return cbNode.Checked; }
            set { cbNode.Checked = value; }
        }

        /// <summary>
        /// 是否处方模板
        /// </summary>
        public bool ResTemp
        {
            get { return cbTemplate.Checked; }
            set { cbTemplate.Checked = value; }
        }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string TempName
        {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }

        /// <summary>
        /// 操作状态
        /// </summary>
        private bool isNewFlag;

        /// <summary>
        /// 操作状态
        /// </summary>
        public bool IsNewFlag
        {
            get { return isNewFlag; }
            set { isNewFlag = value; }
        }

        /// <summary>
        /// 选中节点
        /// </summary>
        private Node selectNode;

        /// <summary>
        /// 模板名称
        /// </summary>
        public Node SelectNode
        {
            get { return selectNode; }
            set { selectNode = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmFeeTemplateInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) == true)
            {
                MessageBoxEx.Show("模板名称不能为空！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbNode.Checked)
            {
                Head.MouldType = 0;
            }

            if (cbTemplate.Checked)
            {
                Head.MouldType = 1;
            }

            if (Convert.ToBoolean(InvokeController("CheckName", txtName.Text, Head.PresType, Head.ModulLevel, Head.PID, Head.PresMouldHeadID)) == false)
            {
                MessageBoxEx.Show("同级别模板名称不能重复！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Head.ModuldName = txtName.Text;
            if (IsNewFlag == true)
            {
                Head.DelFlag = 0;
            }

            InvokeController("SaveTempInfo", Head);
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmFeeTemplateInfo_Load(object sender, EventArgs e)
        {
            if (IsNewFlag == true)
            {
                cbNode.Enabled = true;
                cbTemplate.Enabled = true;
            }
            else
            {
                cbNode.Enabled = false;
                cbTemplate.Enabled = false;
            }
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
