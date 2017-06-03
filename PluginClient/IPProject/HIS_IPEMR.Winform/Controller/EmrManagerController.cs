using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using HIS_IPEMR.Winform.IView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.Mongo;
using System.Windows.Forms;

namespace HIS_IPEMR.Winform.Controller
{
    [WinformController(DefaultViewName = "FrmEmrManager")]//在菜单上显示
    [WinformView(Name = "FrmEmrManager", DllName = "HIS_IPEMR.Winform.dll", ViewTypeName = "HIS_IPEMR.Winform.ViewForm.FrmEmrManager")]
    [WinformView(Name = "FrmEmrModel", DllName = "HIS_IPEMR.Winform.dll", ViewTypeName = "HIS_IPEMR.Winform.ViewForm.FrmEmrModel")]
    [WinformView(Name = "FrmMedicalCase", DllName = "HIS_IPEMR.Winform.dll", ViewTypeName = "HIS_IPEMR.Winform.ViewForm.FrmMedicalCase")]

    public class EmrManagerController: WcfClientController
    {
        IFrmEmrManager ifrmEmrManager;
        IFrmEmrModel ifrmEmrModel;
        IFrmMedicalCase ifrmMedicalCase;
        public override void Init()
        {
            ifrmEmrManager = (IFrmEmrManager)iBaseView["FrmEmrManager"];
            ifrmEmrModel = (IFrmEmrModel)iBaseView["FrmEmrModel"];
            ifrmMedicalCase = (IFrmMedicalCase)iBaseView["FrmMedicalCase"];
        }

        [WinformMethod]
        public List<EmrPatData> GetEmrAllfromMongoDB()
        {
            ServiceResponseData retdata = InvokeWcfService("EMRMongoDB.Service", "EMRStoreController", "GetEmrAll");
            List<EmrPatData> emrlist = retdata.GetData<List<EmrPatData>>(0);
            return emrlist;
        }

        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <param name="deptid">科室Id</param>
        /// <param name="deptname">科室名称</param>
        /// <param name="empid">操作员ID</param>
        /// <param name="empname">操作员姓名</param>
        [WinformMethod]
        public void ShowMedicalCaseForm(int patlistid, int deptid, string deptname, int empid, string empname)
        {
            ifrmMedicalCase.GetMedicalCase(patlistid, deptid, deptname, empid, empname);
            (iBaseView["FrmMedicalCase"] as Form).ShowDialog();
        }

        [WinformMethod]
        public void ShowMessage(string message)
        {
            MessageBoxShowSimple(message);
        }
    }
}
