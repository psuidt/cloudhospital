using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    /// <summary>
    /// 出院未处理数据确认界面
    /// </summary>
    public partial class FrmDischargeConfirmation : BaseFormBusiness, IDischargeConfirmation
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmDischargeConfirmation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 绑定未停用账单列表
        /// </summary>
        /// <param name="notStopOrderDt">未停用账单列表</param>
        /// <param name="isTransDept">true:转科/false:出区</param>
        public void Bind_NotStopOrder(DataTable notStopOrderDt, bool isTransDept)
        {
            if (isTransDept)
            {
                lblMsg.Text = "转科失败！病人有药未统领，医嘱未转抄、未发送或存在未停用的账单！";
            }
            else
            {
                lblMsg.Text = "定义出区失败！病人有药未统领，医嘱未转抄、未发送或存在未停用的账单！";
            }

            grdNotStopOrder.DataSource = notStopOrderDt;
        }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        /// <param name="sender">btnClose</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
