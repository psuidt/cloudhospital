using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    public partial class FrmPatientBedChanging : BaseFormBusiness, IPatientBedChanging
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmPatientBedChanging()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 绑定空床床位号列表
        /// </summary>
        /// <param name="bedNoDt">床位号列表</param>
        /// <param name="bedNo">床位号</param>
        /// <param name="patName">病人姓名</param>
        /// <param name="serialNumber">病人住院号</param>
        public void Bind_BedNoList(DataTable bedNoDt, string bedNo, string patName, string serialNumber)
        {
            cboNewBedNo.DataSource = bedNoDt;
            cboNewBedNo.ValueMember = "BedId";
            cboNewBedNo.DisplayMember = "BedNo";
            cboNewBedNo.SelectedIndex = 0;

            txtOldBedNo.Text = bedNo.ToString();
            txtPatName.Text = patName;
            txtSerialNumber.Text = serialNumber;
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        public void ClosrForm()
        {
            this.Close();
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClosrForm();
        }

        /// <summary>
        /// 保存换床信息
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboNewBedNo.Text.Trim()))
            {
                InvokeController("SaveBedChanging", cboNewBedNo.Text.Trim());
            }
            else
            {
                InvokeController("MessageShow", "请选择一个有效的空床！");
            }
        }
    }
}