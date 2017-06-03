using EFWCoreLib.WinformFrame.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_MIInterface.Winform.IView
{
    public interface IFrmMITradeQuery : IBaseView
    {
        void LoadMIType(DataTable dtMemberInfo);

        void LoadTradeInfoSummary(DataTable dtTradeInfoSummary);

        void LoadTradeRecordInfo(DataTable dtTradeRecordInfo);

        void LoadTradeRecordInfoMI(DataTable dtTradeRecordInfoMI);

    }
}
