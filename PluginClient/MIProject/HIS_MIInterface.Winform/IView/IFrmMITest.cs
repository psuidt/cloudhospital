using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.WinformFrame.Controller;
using System.Data;
using HIS_Entity.MIManage;

namespace HIS_MIInterface.Winform.IView
{
    public interface IFrmMITest : IBaseView
    {
        void LoadCatalogInfo(DataTable dt);

        void LoadPatientInfo(PatientInfo patientInfo);

        void LoadRegisterInfo(Dictionary<string, string> dic);

        void LoadTradeInfo(Dictionary<string, string> dic);

        void LoadFee(DataTable dt);

        void PreviewCharge(Dictionary<string, string> dic);

        void LoadTrade(decimal d);
    }
}
