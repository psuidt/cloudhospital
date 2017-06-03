using System;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.IPManage;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    /// <summary>
    /// 账单模板
    /// </summary>
    public partial class FrmAddDoctorTemp : BaseFormBusiness, IAddDoctorTemp
    {
        /// <summary>
        /// 模板主表对象
        /// </summary>
        private IP_FeeItemTemplateHead mFeeItemTemplateHead = new IP_FeeItemTemplateHead();

        /// <summary>
        /// 模板主表对象
        /// </summary>
        public IP_FeeItemTemplateHead FeeItemTemplateHead
        {
            get
            {
                frmAddTemp.GetValue<IP_FeeItemTemplateHead>(mFeeItemTemplateHead);
                return mFeeItemTemplateHead;
            }

            set
            {
                mFeeItemTemplateHead = value;
                frmAddTemp.Load(mFeeItemTemplateHead);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmAddDoctorTemp()
        {
            InitializeComponent();
            frmAddTemp.AddItem(txtTempName, "TempName");
            frmAddTemp.AddItem(txtTempMemo, "Memo");
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender">触发事件的按钮</param>
        /// <param name="e">事件参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ColseForm(); // 关闭当前窗体
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        public void ColseForm()
        {
            this.Close();
        }

        /// <summary>
        /// 保存模板数据
        /// </summary>
        /// <param name="sender">触发事件的按钮</param>
        /// <param name="e">事件参数</param>
        private void btnSaveTemp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTempName.Text.Trim()))
            {
                MessageBox.Show("请输入有效模板名！");
                return;
            }

            InvokeController("SaveFeeItemTemplateHead");
        }
    }
}
