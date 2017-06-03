using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_Entity.DrugManage;
using HIS_Entity.OPManage.BusiEntity;

namespace HIS_DrugManage.Winform.Controller
{
    /// <summary>
    /// 门诊发药控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmOPDisp")]//在菜单上显示
    [WinformView(Name = "FrmOPDisp", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmOPDisp")]//药房发药
    [WinformView(Name = "FrmOPRefund", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmOPRefund")]//药房退药
    public class OPDispController : WcfClientController
    {
        /// <summary>
        /// 当前选中科室
        /// </summary>
        private int selectedDeptID = 0;

        /// <summary>
        /// 门诊发药窗口接口
        /// </summary>
        IFrmOPDisp frmOPDisp;

        /// <summary>
        /// 门诊退药接口
        /// </summary>
        IFrmOPRefund frmOPRefund;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public override void Init()
        {
            frmOPDisp = (IFrmOPDisp)iBaseView["FrmOPDisp"];
            frmOPRefund = (IFrmOPRefund)iBaseView["FrmOPRefund"];
            frmOPDisp.SendEmployeeName = LoginUserInfo.EmpName;
            frmOPRefund.ReturnEmployeeName = LoginUserInfo.EmpName;
        }

        #region 门诊发药

        /// <summary>
        /// 获取当前选中的药剂科室ID
        /// </summary>
        /// <param name="deptID">药剂科室ID</param>
        /// <param name="deptName">药房名称</param>
        [WinformMethod]
        public void SetSelectedDept(int deptID, string deptName)
        {
            selectedDeptID = deptID;
            frmOPRefund.SetDrugStoreName(deptName);
        }

        /// <summary>
        /// 取得药库数据
        /// </summary>
        [WinformMethod]
        public void GetDrugStoreData()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "OpDispController", "GetDrugStoreData");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmOPDisp.BindStoreRoomComboxList(dt);
        }

        /// <summary>
        /// 药房公共接口
        /// </summary>  
        /// <param name="code">发票号或会员卡号</param>
        /// <param name="execDeptID">执行科室Id</param>
        /// <param name="distributeFlag">发药标志</param>
        [WinformMethod]
        public void QueryPatientInfo(string code, int execDeptID, int distributeFlag)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(code);
                request.AddData(execDeptID);
                request.AddData(distributeFlag);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "OpDispController", "QueryPatientInfo", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmOPDisp.BindPatientAndInvoceGrid(dt);
        }

        /// <summary>
        /// 获取收费主表
        /// </summary>
        /// <param name="invoiceNO">发票号</param>
        /// <param name="deptId">科室Id</param>
        [WinformMethod]
        public void GetFeeItemHead(string invoiceNO,int deptId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(invoiceNO);
                request.AddData(deptId);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "OpDispController", "GetFeeItemHead", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmOPDisp.BindFeeHeadGrid(dt);
        }

        /// <summary>
        /// 获取处方打印信息
        /// </summary>
        /// <param name="preHeadId">处方头表ID</param>
        /// <param name="preNo">处方号</param>
        /// <returns>返回数据集</returns>
        [WinformMethod]
        public DataTable GetPrintPresData(int preHeadId, int preNo)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(preHeadId);
                request.AddData(preNo);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "GetPrintPresData", requestAction);
            DataTable presData = retdata.GetData<DataTable>(0);
            return presData;
        }

        /// <summary>
        /// 处方打印
        /// </summary>
        /// <param name="presPrint">打印参数对象</param>
        /// <param name="presData">打印数据源</param>
        [WinformMethod]
        public void PrintPres(PresPrint presPrint, DataTable presData)
        {
            InvokeController("OPProject.UI", "PresManageController", "PrintPres", presPrint, presData, true);
        }

        /// <summary>
        /// 读取病人信息
        /// </summary>
        /// <param name="patId">病人ID</param>
        [WinformMethod]
        public void GetPatInfo(int patId)
        {
            if (patId > 0)
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(patId);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "LoadPatientInfo", requestAction);
                DataTable dtPatient = retdata.GetData<DataTable>(0);
                DataTable dtDisea = retdata.GetData<DataTable>(1);
                string strDisease = GetDiseaseString(dtDisea);
                frmOPDisp.BindPatientInfo(dtPatient, strDisease);
            }
        }

        /// <summary>
        /// 获取执行单配置信息
        /// </summary>
        /// <returns>执行单配置信息</returns>
        [WinformMethod]
        public DataTable GetExecuteBills()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "OpDispController", "GetExecuteBills");
            DataTable presData = retdata.GetData<DataTable>(0);
            return presData;
        }

        /// <summary>
        /// 取得诊断字符串
        /// </summary>
        /// <param name="dtDisea">诊断信息表</param>
        /// <returns>诊断字符串</returns>
        private string GetDiseaseString(DataTable dtDisea)
        {
            string str = string.Empty;
            for (int i = 0; i < dtDisea.Rows.Count; i++)
            {
                if (i == dtDisea.Rows.Count - 1)
                {
                    str += dtDisea.Rows[i]["DiagnosisName"].ToString();
                }
                else
                {
                    str += dtDisea.Rows[i]["DiagnosisName"].ToString() + "、";
                }
            }

            return str;
        }

        /// <summary>
        /// 获取费用明细
        /// </summary>
        /// <param name="feeItemHeadId">费用主表Id</param>
        [WinformMethod]
        public void GetFeeItemDetail(int feeItemHeadId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(feeItemHeadId);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "OpDispController", "GetFeeItemDetail", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmOPDisp.BindFeeDetailGrid(dt);
        }

        /// <summary>
        /// 门诊药房发药
        /// </summary>
        /// <param name="dtFeeHead">处方头表</param>
        /// <param name="deptId">科室Id</param>
        [WinformMethod]
        public void OPDisp(DataTable dtFeeHead, int deptId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(dtFeeHead);
                request.AddData(GetUserInfo().EmpId);
                request.AddData(GetUserInfo().EmpName);
                request.AddData(deptId);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "OpDispController", "OPDisp", requestAction);
            DGBillResult result = retdata.GetData<DGBillResult>(0);
            if (result.Result == 0)
            {
                MessageBoxShowSimple("发药成功...");
            }
            else
            {
                string rtnMsg = result.ErrMsg;
                string message = string.Empty;
                if (result.LstNotEnough == null)
                {
                    MessageBoxShowSimple("发药失败:\r\n" + rtnMsg);
                }
                else
                {
                    foreach (DGNotEnough m in result.LstNotEnough)
                    {
                        message = message + m.DrugInfo + "\r\n";
                    }

                    MessageBoxShowSimple("发药失败，以下药品库存不足:\r\n" + message);
                }
            }
        }
        #endregion

        #region 门诊退药

        /// <summary>
        /// 门诊退药
        /// </summary>
        /// <param name="dtRefund">退药明细表</param>
        [WinformMethod]
        public void OPRefund(DataTable dtRefund)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(dtRefund);
                request.AddData(GetUserInfo().EmpId);
                request.AddData(GetUserInfo().EmpName);
                request.AddData(selectedDeptID);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "OpDispController", "OPRefund", requestAction);
            DGBillResult result = retdata.GetData<DGBillResult>(0);
            if (result.Result == 0)
            {
                MessageBoxShowSimple("退药成功...");
            }
            else
            {
                string rtnMsg = result.ErrMsg;
                MessageBoxShowSimple("退药失败:" + result.ErrMsg);
            }
        }

        /// <summary>
        /// 获取退药明细
        /// </summary>
        [WinformMethod]
        public void GetRefundDetail()
        {
            Dictionary<string, string> condition = frmOPRefund.GetRefundCondition();
            condition.Add("c.ExecDeptID", selectedDeptID.ToString());
            condition.Add("b.RefundFlag", "0");
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(condition);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "OpDispController", "GetRefundDetail", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmOPRefund.BindRefundGrid(dt);
        }

        /// <summary>
        /// 获取退药查询明细
        /// </summary>
        [WinformMethod]
        public void GetRefundQueryDetail()
        {
            Dictionary<string, string> condition = frmOPRefund.GetRefundQueryCondition();
            condition.Add("c.ExecDeptID", selectedDeptID.ToString());
            condition.Add("b.RefundFlag", "1");
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(condition);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "OpDispController", "GetRefundQueryDetail", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmOPRefund.BindRefundQueryGrid(dt);
        }
        #endregion
    }
}
