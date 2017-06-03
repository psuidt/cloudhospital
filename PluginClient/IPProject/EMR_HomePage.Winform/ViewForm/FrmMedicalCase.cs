using EFWCoreLib.CoreFrame.Business;
using EMR_HomePage.Winform.IView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EMR_HomePage.Winform.ViewForm
{
    public partial class FrmMedicalCase : BaseFormBusiness, IFrmMedicalCase
    {
        public FrmMedicalCase()
        {
            InitializeComponent();
        }

        public void GetMedicalCase(int patlistid, int deptid, string deptname, int empid, string empname)
        {
            dcControl.GetMedicalCase(patlistid, deptid, deptname, empid, empname);                  
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dcControl.SaveCaseFile())
            {
                InvokeController("ShowMessage", "保存成功");
            }
            else
            {
                InvokeController("ShowMessage", "保存失败");
            }            
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            dcControl.ExportMedicalCase();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            dcControl.PrintMedicalCase();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
