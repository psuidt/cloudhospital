using System;
using System.Collections.Generic;
using EFWCoreLib.WinformFrame.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_MIInterface.Winform.IView
{
    public interface IFrmCommitTradeState : IBaseView
    {
        void LoadTradeInfo(DataTable dt);

        void LoadCatalogInfo(DataTable dt);
    }
}
