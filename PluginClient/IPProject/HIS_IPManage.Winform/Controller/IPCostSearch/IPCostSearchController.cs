using System;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_IPManage.Winform.IView.IPCostSearch;

namespace HIS_IPManage.Winform.Controller.IPCostSearch
{
    /// <summary>
    /// 住院病人费用查询控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmIPCostSearch")]//在菜单上显示
    [WinformView(Name = "FrmIPCostSearch", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.IPCostSearch.FrmIPCostSearch")]
    [WinformView(Name = "FrmCostDetail", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.IPCostSearch.FrmCostDetail")]
    public class IPCostSearchController : WcfClientController
    {
        /// <summary>
        /// 住院病人费用查询接口
        /// </summary>
        IIPCostSearch mIIPCostSearch;

        /// <summary>
        /// 住院病人费用明细接口
        /// </summary>
        IFrmCostDetail mIFrmCostDetail;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            mIIPCostSearch = (IIPCostSearch)DefaultView;
            mIFrmCostDetail = (IFrmCostDetail)iBaseView["FrmCostDetail"];
        }

        /// <summary>
        /// 加载查询条件基础数据
        /// </summary>
        [WinformMethod]
        public void SetMaterData()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "IPCostSearchController", "SetMaterData");
            DataSet ds = retdata.GetData<DataSet>(0);
            mIIPCostSearch.Bind_ChareEmpList(ds.Tables["CashierDt"]); // 绑定收费员列表
            mIIPCostSearch.Bind_DeptList(ds.Tables["deptDt"]); // 绑定科室列表
            mIIPCostSearch.Bind_PatTypeList(ds.Tables["patTypeList"]); // 绑定病人类型列表
            mIIPCostSearch.Bind_DoctorList(ds.Tables["EmpDataDt"]); // 绑定就诊医生列表
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        [WinformMethod]
        public void IpCostSearchQuery()
        {
            mIIPCostSearch.QueryDictionary.Add("WorkID", LoginUserInfo.WorkId);
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIIPCostSearch.QueryDictionary);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "IPCostSearchController", "IpCostSearchQuery", requestAction);
            DataTable payTypeDt = retdata.GetData<DataTable>(0);
            DataTable itemTypeDt = retdata.GetData<DataTable>(1);
            mIIPCostSearch.Bind_CostData(payTypeDt, itemTypeDt);
        }

        /// <summary>
        /// 打开结算费用明细界面
        /// </summary>
        /// <param name="costHeadid">结算头ID</param>
        [WinformMethod]
        public void CostSearchDetail(int costHeadid)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(costHeadid);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "IPCostSearchController", "GetCostDetail", requestAction);
            DataTable feeDt = retdata.GetData<DataTable>(0);

            if (feeDt != null && feeDt.Rows.Count > 0)
            {
                for (int i = 0; i < feeDt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(feeDt.Rows[i]["InvoiceClass"].ToString()))
                    {
                        feeDt.Rows[i]["ItemName"] = DBNull.Value;
                        feeDt.Rows[i]["Spec"] = DBNull.Value;
                        feeDt.Rows[i]["PackAmount"] = DBNull.Value;
                        feeDt.Rows[i]["PackUnit"] = DBNull.Value;
                        feeDt.Rows[i]["Amount"] = DBNull.Value;
                        feeDt.Rows[i]["TotalFee"] = DBNull.Value;
                    }
                    else
                    {
                        if (feeDt.Rows[i]["PackUnit"].ToString().Contains("合计"))
                        {
                            feeDt.Rows[i]["InvoiceClass"] = DBNull.Value;
                            feeDt.Rows[i]["ItemName"] = DBNull.Value;
                            feeDt.Rows[i]["Spec"] = DBNull.Value;
                            feeDt.Rows[i]["PackAmount"] = DBNull.Value;
                            feeDt.Rows[i]["Amount"] = DBNull.Value;
                        }
                    }
                }

                mIFrmCostDetail.BindData(feeDt);
            }

            ((Form)iBaseView["FrmCostDetail"]).ShowDialog();
        }

        /// <summary>
        /// 消息提示
        /// </summary>
        /// <param name="msg">消息内容</param>
        [WinformMethod]
        public void MessageShow(string msg)
        {
            MessageBoxShowSimple(msg);
        }
    }
}
