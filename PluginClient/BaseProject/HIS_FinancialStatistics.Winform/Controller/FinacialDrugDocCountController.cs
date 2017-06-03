using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_FinancialStatistics.Winform.IView;

namespace HIS_FinancialStatistics.Winform.Controller
{    
    /// <summary>
    /// 医生工作量统计控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmFinacialDrugDocCount")]
    [WinformView(Name = "FrmFinacialDrugDocCount", DllName = "HIS_FinancialStatistics.Winform.dll", ViewTypeName = "HIS_FinancialStatistics.Winform.ViewForm.FrmFinacialDrugDocCount")]
    public class FinacialDrugDocCountController : WcfClientController
    {
        /// <summary>
        /// 药品医生数量统计
        /// </summary>
        IFrmFinacialDrugDocCount iFrmFinacialDrugDocCount;

        /// <summary>
        /// 初始化菜单
        /// </summary>
        public override void Init()
        {
            iFrmFinacialDrugDocCount = (IFrmFinacialDrugDocCount)iBaseView["FrmFinacialDrugDocCount"];
        }

        /// <summary>
        /// 获取医疗机构
        /// </summary>
        [WinformMethod]
        public void GetWorker()
        {
            ServiceResponseData retdata = InvokeWcfService("BaseProject.Service", "FinacialDrugDocCountController", "GetWorkers");
            DataTable dtWork = retdata.GetData<DataTable>(0);
            iFrmFinacialDrugDocCount.SetWorkers(dtWork);
        }

        /// <summary>
        /// 获取医生
        /// </summary>
        /// <param name="iWorkId">组织机构Id</param>
        [WinformMethod]
        public void GetDoctor(int iWorkId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iWorkId);
            });
            ServiceResponseData retdata = InvokeWcfService("BaseProject.Service", "FinacialDrugDocCountController", "GetDoctors", requestAction);
            DataTable dtDoctor = retdata.GetData<DataTable>(0);
            iFrmFinacialDrugDocCount.SetDoctors(dtDoctor);
        }

        /// <summary>
        /// 获取报表数据-药品医生开方数量
        /// </summary>
        /// <param name="iworkId">组织机构Id</param>
        /// <param name="iDocId">医生Id</param>
        /// <param name="iType">类型</param>
        /// <param name="bDate">开始日期</param>
        /// <param name="eDate">结束日期</param>
        [WinformMethod]
        public void GetDrugDocCount(int iworkId,int iDocId,int iType,DateTime bDate,DateTime eDate)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iworkId);
                request.AddData(iDocId);
                request.AddData(iType);
                request.AddData(bDate);
                request.AddData(eDate);
            });

            ServiceResponseData retdata = InvokeWcfService("BaseProject.Service", "FinacialDrugDocCountController", "GetDrugDocCount", requestAction);
            DataTable dtReport = retdata.GetData<DataTable>(0);
            iFrmFinacialDrugDocCount.LoadData(dtReport);
        }

        /// <summary>
        /// 弹窗报错
        /// </summary>
        /// <param name="sErr">错误字符串</param>
        [WinformMethod]
        public void MessageBoxErr(string sErr)
        {
            MessageBoxShowError(sErr);
        }
    }
}
