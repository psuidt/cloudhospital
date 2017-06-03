using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HIS_IPEMR.Winform.IView
{
   public interface IFrmMedicalCase
    {
        void GetMedicalCase(int patlistid, int deptid, string deptname, int empid, string empname);
    }
}
