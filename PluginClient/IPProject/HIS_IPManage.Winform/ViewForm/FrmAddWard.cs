using System;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    /// <summary>
    /// 新增病房界面
    /// </summary>
    public partial class FrmAddWard : BaseFormBusiness, IAddWard
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmAddWard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 病房号
        /// </summary>
        public string RoomNO
        {
            get
            {
                return txtWardNo.Text.Trim();
            }
        }

        /// <summary>
        /// 关闭新增病房界面
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 保存病房-增加病床
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtWardNo.Text.Trim()))
            {
                InvokeController("MessageShow", "请输入病房号");
                return;
            }

            InvokeController("ShowAddBed");
        }
    }
}