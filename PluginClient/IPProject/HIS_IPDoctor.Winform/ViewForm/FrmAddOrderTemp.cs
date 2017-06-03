using System;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_IPDoctor.Winform.IView;

namespace HIS_IPDoctor.Winform.ViewForm
{
    /// <summary>
    /// 新增或修改医嘱模板
    /// </summary>
    public partial class FrmAddOrderTemp : BaseFormBusiness, IAddOrderTemp
    {
        /// <summary>
        /// 模板对象
        /// </summary>
        private IPD_OrderModelHead mOrderModelHead = new IPD_OrderModelHead();

        /// <summary>
        /// 模板主表对象
        /// </summary>
        public IPD_OrderModelHead OrderModelHead
        {
            get
            {
                frmAddTemp.GetValue(mOrderModelHead);
                return mOrderModelHead;
            }

            set
            {
                mOrderModelHead = value;
                frmAddTemp.Load(mOrderModelHead);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmAddOrderTemp()
        {
            InitializeComponent();
            frmAddTemp.AddItem(txtTempName, "ModelName");
            frmAddTemp.AddItem(txtTempMemo, "Memo");
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ColseForm();
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
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnSaveTemp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTempName.Text.Trim()))
            {
                InvokeController("MessageShow", "请输入有效模板名！");
                return;
            }

            InvokeController("SaveOrderTemplateHead");
        }
    }
}
