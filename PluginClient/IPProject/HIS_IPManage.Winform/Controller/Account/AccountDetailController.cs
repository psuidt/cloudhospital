using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.IPManage;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.Controller
{
    /// <summary>
    /// 交款管理控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmAccountDetail")]
    public class AccountDetailController : WcfClientController
    {
        /// <summary>
        /// 缴款明细接口
        /// </summary>
        IAccountDetail iAccountDetail;

        /// <summary>
        /// 控制区初始化
        /// </summary>
        public override void Init()
        {
            iAccountDetail = (IAccountDetail)DefaultView;
        }

        /// <summary>
        /// 获取每条缴款的发票等信息
        /// </summary>
        /// <param name="iStaffId">收费员ID</param>
        /// <param name="iAccountId">缴款ID</param>
        [WinformMethod]
        public void GetPayMentItem(int iStaffId, int iAccountId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(WorkId);
                request.AddData(iStaffId);
                request.AddData(iAccountId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "SinglePaymentManageController", "GetPayMentItem", requestAction);
            DataTable dtFPSum = retdata.GetData<DataTable>(0);
            DataTable dtFPClass = retdata.GetData<DataTable>(1);
            DataTable dtAccountClass = retdata.GetData<DataTable>(2);
            IP_Account curAccount = retdata.GetData<IP_Account>(3);

            // 绑定树菜单
            iAccountDetail.ShowPayMentItem(dtFPSum, dtFPClass, dtAccountClass);
        }

        /// <summary>
        /// 获取预交金数据
        /// </summary>
        /// <param name="iStaffId">待缴款数据ID</param>
        /// <param name="iAccountId">缴款ID</param>
        [WinformMethod]
        public void GetDepositList(int iStaffId, int iAccountId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(WorkId);
                request.AddData(iStaffId);
                request.AddData(iAccountId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "SinglePaymentManageController", "GetDepositList", requestAction);
            DataTable dtDepositList = retdata.GetData<DataTable>(0);

            // 绑定树菜单
            iAccountDetail.ShowDepositItem(dtDepositList);
        }
    }
}