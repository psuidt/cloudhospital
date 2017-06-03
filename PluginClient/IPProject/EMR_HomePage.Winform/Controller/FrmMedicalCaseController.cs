using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EMR_HomePage.Winform.IView;
using EMR_HomePage.Winform.ViewForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EMR_HomePage.Winform.Controller
{
    [WinformController(DefaultViewName = "FrmMedicalCase")]
    [WinformView(Name = "FrmMedicalCase", DllName = "EMR_HomePage.Winform", ViewTypeName = "EMR_HomePage.Winform.ViewForm.FrmMedicalCase")]
    public class FrmMedicalCaseController : WcfClientController
    {
        /// <summary>
        /// 窗体界面接口类
        /// </summary>
        IFrmMedicalCase ifrmMedicalCase;

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            ifrmMedicalCase = (FrmMedicalCase)iBaseView["FrmMedicalCase"];            
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
        public void ShowMedicalCaseForm(int patlistid,int deptid,string deptname,int empid,string empname)
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
