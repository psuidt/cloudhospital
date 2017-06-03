using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.IPManage;
using HIS_IPNurse.Winform.IView;

namespace HIS_IPNurse.Winform.Controller
{
    /// <summary>
    /// 医嘱发送控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmOrderCheck")]//在菜单上显示
    [WinformView(Name = "FrmOrderCheck", DllName = "HIS_IPNurse.Winform.dll", ViewTypeName = "HIS_IPNurse.Winform.ViewForm.FrmOrderCheck")]
    public class OrderCheckController : WcfClientController
    {
        /// <summary>
        /// 护士站医嘱发送接口
        /// </summary>
        IOrderCheck mIOrderCheck;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            mIOrderCheck = (IOrderCheck)DefaultView;
        }

        /// <summary>
        /// 获取科室列表
        /// </summary>
        [WinformMethod]
        public void GetDeptList()
        {
            // 通过WCF调用服务端控制器取得住院临床科室列表
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderCheckController", "GetDeptList");
            DataTable deptDt = retdata.GetData<DataTable>(0);
            mIOrderCheck.bind_DeptList(deptDt, LoginUserInfo.DeptId);
        }

        /// <summary>
        /// 获取可发送医嘱列表以及病人列表
        /// </summary>
        /// <param name="iDeptId">科室ID</param>
        /// <param name="sOrderCategory">医嘱类别</param>
        /// <param name="sOrderStatus">状态</param>
        [WinformMethod]
        public void GetOrederCheckInfo(int iDeptId,string sOrderCategory, string sOrderStatus)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iDeptId);
                request.AddData(sOrderCategory);
                request.AddData(sOrderStatus);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderCheckController", "GetOrederCheckInfo", requestAction);
            // 病人列表
            DataTable patListDt = retdata.GetData<DataTable>(0);
            // 未转抄医嘱列表
            DataTable orderListDt = retdata.GetData<DataTable>(1);
            // 数据绑定
            mIOrderCheck.bind_PatList(patListDt);
            mIOrderCheck.bind_OrederCheckInfo(orderListDt);
        }

        /// <summary>
        /// 获取医嘱关联费用列表
        /// </summary>
        /// <param name="iPatListID">病人登记ID</param>
        /// <param name="iGroupID">组号ID</param>
        [WinformMethod]
        public void GetOrderRelationFeeList(int iPatListID,int iGroupID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iPatListID);
                request.AddData(iGroupID);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderCheckController", "GetOrderRelationFeeList", requestAction);
            DataTable patOrderFeeList = retdata.GetData<DataTable>(0);
            mIOrderCheck.bind_PatOrderRelationFeeList(patOrderFeeList);
        }

        /// <summary>
        /// 发送医嘱
        /// </summary>
        /// <param name="iGroupIDList">组号集合</param>
        /// <param name="endTime">结束时间</param>
        [WinformMethod]
        public void SendOrderCheckList(List<int> iGroupIDList, DateTime endTime)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iGroupIDList);
                request.AddData(endTime);
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderCheckController", "SendOrderCheckList", requestAction);
            bool b = retdata.GetData<bool>(0);
            if (b)
            {
                List<IP_OrderCheckError> sendResultList = retdata.GetData<List<IP_OrderCheckError>>(1);
                mIOrderCheck.bind_OrderSendResult(sendResultList);
                mIOrderCheck.GetOrederCheckInfo();
                MessageBoxShowSimple("发送成功!");
            }
            else
            {
                string sSendResult= retdata.GetData<string>(1);
                MessageBoxShowError(sSendResult);
            }
        }
    }
}
