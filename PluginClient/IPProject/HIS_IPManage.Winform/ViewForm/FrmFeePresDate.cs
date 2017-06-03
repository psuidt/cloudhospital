using System;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    public partial class FrmFeePresDate : BaseFormBusiness, IFeePresDate
    {
        /// <summary>
        /// 床位费
        /// </summary>
        public bool BedOrderChecked
        {
            get
            {
                return chkBedOrder.Checked;
            }
        }

        /// <summary>
        /// 长期账单
        /// </summary>
        public bool PresDateChecked
        {
            get
            {
                return chkPresDate.Checked;
            }
        }

        /// <summary>
        /// 处方开始时间
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return dtkPresDate.Bdate.Value;
            }
        }

        /// <summary>
        /// 处方结束时间
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return dtkPresDate.Edate.Value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmFeePresDate()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 界面显示
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void FrmFeePresDate_Shown(object sender, EventArgs e)
        {
            chkBedOrder.Checked = true;
            chkPresDate.Checked = true;
            dtkPresDate.Bdate.Value = DateTime.Now;
            dtkPresDate.Edate.Value = DateTime.Now;
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 长期账单记账时间确认
        /// </summary>
        /// <param name="sender">触发事件的控件</param>
        /// <param name="e">事件所需参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dtkPresDate.Bdate.Value == DateTime.MinValue || dtkPresDate.Edate.Value == DateTime.MinValue)
            {
                InvokeController("MessageShow", "请选择正确的处方日期！");
                return;
            }

            if (Convert.ToDateTime(dtkPresDate.Bdate.Value.ToString("yyyy-MM-dd")) > 
                Convert.ToDateTime(dtkPresDate.Edate.Value.ToString("yyyy-MM-dd")))
            {
                InvokeController("MessageShow", "处方开始日期不能大于结束日期！");
                return;
            }

            InvokeController("FeeItemAccounting", 2);
        }

        /// <summary>
        /// 关闭当前界面
        /// </summary>
        public void CloseForm()
        {
            this.Close();
        }
    }
}
