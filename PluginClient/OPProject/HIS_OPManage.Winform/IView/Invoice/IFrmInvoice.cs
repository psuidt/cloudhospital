using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    /// 票据管理界面接口类
    /// </summary>
    public interface IFrmInvoice : IBaseView
    {
        /// <summary>
        /// 给网格加载数据
        /// </summary>
        /// <param name="dt">票据数据</param>
        /// <param name="filter">过滤条件</param>
        void LoadInvoice(DataTable dt, string filter);

        /// <summary>
        /// 获取当前票据ID
        /// </summary>
        /// <returns>当前票据ID</returns>
        int GetCurInvoiceID();

        /// <summary>
        /// 获取界面查询条件
        /// </summary>
        /// <returns>获取查询条件</returns>
        string GetQueryString();

        /// <summary>
        /// 绑定收费员列表
        /// </summary>
        /// <param name="dt">收费员列表</param>
        void BindTollcollector_txtEmp(DataTable dt);

        /// <summary>
        /// 票据总金额
        /// </summary>
        string TotalMoney { set; }

        /// <summary>
        /// 票据使用张数
        /// </summary>
        string TotalCont { set; }

        /// <summary>
        /// 退费张数
        /// </summary>
        string RefundCount { set; }

        /// <summary>
        /// 退费金额
        /// </summary>
        string RefundMoney { set; }

        /// <summary>
        /// 票据总张数
        /// </summary>
        string AllCount { set; }

        /// <summary>
        /// 设置网格显示颜色
        /// </summary>
        void SetGridColor();
    }    
}
