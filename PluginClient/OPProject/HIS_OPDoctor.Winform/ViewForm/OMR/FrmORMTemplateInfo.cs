using System;
using System.Windows.Forms;
using DevComponents.AdvTree;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_OPDoctor.Winform.IView.Template;

namespace HIS_OPDoctor.Winform.ViewForm
{
    /// <summary>
    /// 门诊病历模板信息窗体
    /// </summary>
    public partial class FrmORMTemplateInfo : BaseFormBusiness, IFrmORMTemplateInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmORMTemplateInfo()
        {
            InitializeComponent();
        }

        #region 自定义属性和方法
        /// <summary>
        /// 模板头
        /// </summary>
        private OPD_OMRTmpHead head;

        /// <summary>
        /// 模板头
        /// </summary>
        public OPD_OMRTmpHead Head
        {
            get { return head; }
            set { head = value; }
        }

        /// <summary>
        /// 模板类是否选中
        /// </summary>
        public bool ResNode
        {
            get { return cbNode.Checked; }
            set { cbNode.Checked = value; }
        }

        /// <summary>
        /// 模板是否选中
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
        /// 模板名称
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
        #endregion

        #region 事件
        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) == true)
            {
                MessageBox.Show("模板名称不能为空！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            if (Convert.ToBoolean(InvokeController("CheckName", txtName.Text, Head.ModulLevel, Head.PID, Head.OMRTmpHeadID)) == false)
            {
                MessageBox.Show("同级别模板名称不能重复！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        #endregion
    }
}
