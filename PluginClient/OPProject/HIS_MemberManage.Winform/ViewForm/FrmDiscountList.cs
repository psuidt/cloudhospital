using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_MemberManage.Winform.IView;

namespace HIS_MemberManage.Winform.ViewForm
{
    public partial class FrmDiscountList :  BaseFormBusiness, IFrmDiscountList
    {
        /// <summary>
        /// 构造
        /// Initializes a new instance of the<see cref="FrmDiscountList" /> class.
        /// </summary>
        public FrmDiscountList()
        {
            InitializeComponent();
            statRegDate.Bdate.Text = Convert.ToString(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
            statRegDate.Edate.Text = Convert.ToString(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1));
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmDiscountList_OpenWindowBefore(object sender, EventArgs e)
        {
            DataTable dt =(DataTable) InvokeController("BindWorkInfo");
            cbbWork.DisplayMember = "WorkName";
            cbbWork.ValueMember = "WorkId";
            cbbWork.DataSource = dt;

            int workID = Convert.ToInt32(cbbWork.SelectedValue);
            int cardTypeID= Convert.ToInt32(cbbCardType.SelectedValue);
            string cardNO = txtCardCode.Text.Trim();

            BindGrid(statRegDate.Bdate.Text, statRegDate.Edate.Text, workID, cardTypeID, cardNO);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void CbbWork_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)InvokeController("BindCardTypeInfo", Convert.ToInt32(cbbWork.SelectedValue));
            cbbCardType.DisplayMember = "CardTypeName";
            cbbCardType.ValueMember = "CardTypeID";
            cbbCardType.DataSource = dt;
        }

        /// <summary>
        /// 绑定网格
        /// </summary>
        /// <param name="stDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="workID">机构id</param>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="cardNO">卡号</param>
        public void BindGrid(string stDate, string endDate, int workID, int cardTypeID, string cardNO)
        {
            DataTable dt = (DataTable)InvokeController("GetDiscountListInfo", stDate,  endDate,  workID,  cardTypeID,  cardNO);
            dgDiscountList.DataSource = dt;
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            int workID = Convert.ToInt32(cbbWork.SelectedValue);
            int cardTypeID = Convert.ToInt32(cbbCardType.SelectedValue);
            string cardNO = txtCardCode.Text.Trim();

            BindGrid(statRegDate.Bdate.Text, statRegDate.Edate.Text, workID, cardTypeID, cardNO);
        }
    }
}
