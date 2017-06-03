using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMR_HomePage.Winform.IView
{
    interface IFrmMedicalCase
    {
        void GetMedicalCase(int patlistid,int deptid,string deptname,int empid,string empname);
    }
}
