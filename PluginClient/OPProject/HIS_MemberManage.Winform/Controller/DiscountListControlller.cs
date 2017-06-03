using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;

namespace HIS_MemberManage.Winform.Controller
{
    /// <summary>
    /// 优惠明细控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmDiscountList")]
    [WinformView(Name = "FrmDiscountList", DllName = "HIS_MemberManage.Winform.dll", ViewTypeName = "HIS_MemberManage.Winform.ViewForm.FrmDiscountList")]
    public class DiscountListControlller: WcfClientController
    {
        /// <summary>
        /// 绑定组织机构
        /// </summary>
        /// <returns>组织机构数据</returns>
        [WinformMethod]
        public DataTable BindWorkInfo()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(false);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "MemberController", "GetWorkInfo", requestAction);
            return retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 绑定帐户类型信息
        /// </summary>
        /// <param name="workID">组织机构ID</param>
        /// <returns>帐户类型信息</returns>
        [WinformMethod]
        public DataTable BindCardTypeInfo(int workID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(workID);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "GiftController", "GetCardTypeForWork", requestAction);
            return retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 取得优惠方案信息
        /// </summary>
        /// <param name="stDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="workID">组织机构ID</param>
        /// <param name="cardTypeID">卡类型ID</param>
        /// <param name="cardNO">卡号</param>
        /// <returns>优惠方案数据</returns>
        [WinformMethod]
        public DataTable GetDiscountListInfo(string stDate, string endDate, int workID, int cardTypeID, string cardNO)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(stDate);
                request.AddData(endDate);
                request.AddData(workID);
                request.AddData(cardTypeID);
                request.AddData(cardNO);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "DiscountListControlller", "GetDiscountListInfo", requestAction);
            return retdata.GetData<DataTable>(0);
        }
    }
}
