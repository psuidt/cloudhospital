using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.WinformFrame.Controller;
using System.Data;

namespace HIS_MIInterface.Winform.IView
{
    public interface IFrmMIMatch : IBaseView
    {
        void LoadHISCatalogInfo(DataTable dt);

        void LoadMICatalogInfo(DataTable dt);

        void LoadMatchCatalogInfo(DataTable dt);

        void LoadMIType(DataTable dt);

        void ReFresh();
    }
}
