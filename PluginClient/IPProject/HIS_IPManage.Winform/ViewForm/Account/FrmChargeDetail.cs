using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.ViewForm
{
    /// <summary>
    /// 收费明细界面
    /// </summary>
    public partial class FrmChargeDetail : BaseFormBusiness, IChargeDetail
    {
        /// <summary>
        /// 明细数据
        /// </summary>
        private DataTable dtDetail;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmChargeDetail()
        {
            InitializeComponent();
            spGrid.AutoGenerateColumns = false;
        }

        /// <summary>
        /// 绑定收费明细数据
        /// </summary>
        /// <param name="dtDetail">收费明细数据</param>
        public void BindDetailSource(DataTable dtDetail)
        {
            this.dtDetail = dtDetail;
        }

        /// <summary>
        /// 打开界面加载收费明细数据
        /// </summary>
        /// <param name="sender">FrmChargeDetail</param>
        /// <param name="e">事件参数</param>
        private void FrmChargeDetail_Load(object sender, EventArgs e)
        {
            spGrid.DataSource = dtDetail;
        }
    }
}