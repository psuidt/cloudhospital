using System;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPNurse.Winform.IView;

namespace HIS_IPNurse.Winform.ViewForm
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
        //public DateTime StartTime
        //{
        //    get
        //    {
        //        return dtkPresDate.Bdate.Value;
        //    }
        //}

        /// <summary>
        /// 处方结束时间
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return dtEndTine.Value;
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
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void FrmFeePresDate_Shown(object sender, EventArgs e)
        {
            //chkBedOrder.Checked = true;
            //chkPresDate.Checked = true;
            //dtkPresDate.Bdate.Value = DateTime.Now;
            dtEndTine.Value = DateTime.Now;
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 长期账单记账时间确认
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dtEndTine.Value == DateTime.MinValue)
            {
                InvokeController("MessageShow", "请选择正确的处方日期！");
                return;
            }

            if (DateTime.Compare(
                Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")),
                Convert.ToDateTime(dtEndTine.Value.ToString("yyyy-MM-dd"))) > 0)
            {
                InvokeController("MessageShow", "请选择正确的处方日期！");
                return;
            }

            InvokeController("FeeItemAccounting", 2);
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        public void CloseForm()
        {
            this.Close();
        }

        /// <summary>
        /// 设置界面控件可用
        /// </summary>
        /// <param name="isBedFee">是否可用标志</param>
        public void SetControlEnabled(bool isBedFee)
        {
            if (isBedFee)
            {
                chkBedOrder.Checked = true;
                chkBedOrder.Enabled = true;
                chkPresDate.Checked = false;
                chkPresDate.Enabled = false;
            }
            else
            {
                chkBedOrder.Checked = false;
                chkBedOrder.Enabled = false;
                chkPresDate.Checked = true;
                chkPresDate.Enabled = true;
            }
        }
    }
}
