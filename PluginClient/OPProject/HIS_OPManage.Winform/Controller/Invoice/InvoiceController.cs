using System;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.Controller
{
    [WinformController(DefaultViewName = "FrmInvoiceManager")]//与系统菜单对应
    [WinformView(Name = "FrmInvoiceManager", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmInvoiceManager")]
    //新分配发票
    [WinformView(Name = "FrmAllot", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmAllot")]
   
    /// <summary>
    /// 票据管理界面控制器类
    /// </summary>
    public class InvoiceController : WcfClientController
    {
        /// <summary>
        /// 票据管理界面接口
        /// </summary>
        IFrmInvoice ifrmInvoice;

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            ifrmInvoice = (IFrmInvoice)iBaseView["FrmInvoiceManager"];
        }

        /// <summary>
        /// 获取发票信息
        /// </summary>
        [WinformMethod]
        public void LoadInvoices()
        {               
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "InvoiceController", "GetInvoices");
            DataTable dt = retdata.GetData<DataTable>(0);
            ifrmInvoice.LoadInvoice(dt, ifrmInvoice.GetQueryString());
            ifrmInvoice.SetGridColor();
        }

        /// <summary>
        /// 获取收费人员列表
        /// </summary>
        [WinformMethod]
        public void loadCashier()
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "InvoiceController", "LoadCashier");
            DataTable dt = retdata.GetData<DataTable>(0);
            ifrmInvoice.BindTollcollector_txtEmp(dt);
        }

        /// <summary>
        /// 弹出新全配票据窗体
        /// </summary>
        [WinformMethod]
        public void AddAllot()
        {         
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "InvoiceController", "LoadCashier");
            DataTable dt = retdata.GetData<DataTable>(0);
            ((IFrmAllot)iBaseView["FrmAllot"]).LoadAllotView(dt, LoginUserInfo.EmpId);
            (iBaseView["FrmAllot"] as Form).Text = "分配票据";
             (iBaseView["FrmAllot"] as Form).ShowDialog();
            if (((IFrmAllot)iBaseView["FrmAllot"]).isAddOK)
            {
                MessageBoxShowSimple("新分配票据成功");
            }
        }

        /// <summary>
        /// 检查新增票据
        /// </summary>
        /// <param name="invoicetype">票据类型</param>
        /// <param name="perfchar">票据前缀</param>
        /// <param name="beNo">票据开始号</param>
        /// <param name="endNo">票据结束号</param>
        /// <returns>返回检查是否成功</returns>
        [WinformMethod]
        public bool CheckInvoiceExsist(int invoicetype, string perfchar, int beNo, int endNo)
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(invoicetype);//票据类型
                    request.AddData(perfchar);//前缀
                    request.AddData(beNo);//开始票号
                    request.AddData(endNo); //结束票号
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "InvoiceController", "CheckInvoiceExsist", requestAction);
                bool result = retdata.GetData<bool>(0);
                return result;
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
            }

            return false;
        }
      
        /// <summary>
        /// 票据保存
        /// </summary>
        /// <returns>保存是否成功</returns>
        [WinformMethod]
        public bool  SaveNewAllot()
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(((IFrmAllot)iBaseView["FrmAllot"]).CurrInvoice);
                });

                //通过wcf服务调用bookWcfController控制器中的SaveBook方法，并传递参数Book对象
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "InvoiceController", "SaveNewAllot", requestAction);
                bool success = retdata.GetData<bool>(0);
                if (success)
                {
                    LoadInvoices();
                }

                return success;
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
                return false;
            }
        }

      /// <summary>
      /// 票据停用
      /// </summary>
      /// <returns>是否停用成功</returns>
        [WinformMethod]
        public bool StopInvoice()
        {
            try
            {
                int invoiceId = ifrmInvoice.GetCurInvoiceID();
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(invoiceId);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "InvoiceController", "StopInvoice", requestAction);
                return true;
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
                return false;
            }
        }

        /// <summary>
        /// 删除票据
        /// </summary>
        /// <returns>返回是否删除成功</returns>
        [WinformMethod]
        public bool DeleteInvoice()
        {
            try
            {
                int invoiceId = ifrmInvoice.GetCurInvoiceID();
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(invoiceId);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "InvoiceController", "DeleteInvoice", requestAction);
                MessageBoxShowSimple("删除票据成功");
                return true;
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
                return false;
            }
        }

        /// <summary>
        /// 获取票据信息
        /// </summary>
        /// <param name="invoiceType">票据类型</param>
        /// <param name="perferChar">票据前缀</param>
        /// <param name="startNo">票据开始号</param>
        /// <param name="endNo">票据结束号</param>
        [WinformMethod]
        public void GetInvoiceListInfo(int invoiceType, string perferChar, string startNo, string endNo)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(invoiceType);
                request.AddData(perferChar);
                request.AddData(startNo);
                request.AddData(endNo);
            });
            ServiceResponseData retdata = new ServiceResponseData();
            retdata = InvokeWcfService("OPProject.Service", "InvoiceController", "GetInvoiceListInfo", requestAction);
            decimal totalMoney = retdata.GetData<decimal>(0);
            int totalCount = retdata.GetData<int>(1);
            decimal refundMoney = retdata.GetData<decimal>(2);
            int refundCount = retdata.GetData<int>(3);
            ifrmInvoice.TotalMoney = totalMoney.ToString();
            ifrmInvoice.TotalCont = totalCount.ToString();
            ifrmInvoice.RefundCount = refundCount.ToString();
            ifrmInvoice.RefundMoney = refundMoney.ToString();
            ifrmInvoice.AllCount = (Convert.ToInt64(endNo) - Convert.ToInt64(startNo) + 1).ToString();
        }
    }
}
