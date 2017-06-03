using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPManage.Winform.IView.IPCostSearch;

namespace HIS_IPManage.Winform.ViewForm.IPCostSearch
{
    /// <summary>
    /// 费用明细界面
    /// </summary>
    public partial class FrmCostDetail : BaseFormBusiness,IFrmCostDetail
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmCostDetail()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 绑定结算费用明细数据
        /// </summary>
        /// <param name="dtData">结算费用明细数据</param>
        public void BindData(DataTable dtData)
        {
            dgCostDetail.DataSource = dtData;
        }
    }
}
