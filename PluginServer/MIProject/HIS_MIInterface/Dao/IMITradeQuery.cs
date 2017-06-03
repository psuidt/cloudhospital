using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_MIInterface.Dao
{
    public interface IMITradeQuery
    {
        DataTable Mz_GetTradeInfoSummary(int iMIID, int iPatientType, string sDeptCode, string sTimeStat, string sTimeStop);

        DataTable Mz_GetTradeRecordInfo(int iMIID, int iPatientType, string sDeptCode, string sTimeStat, string sTimeStop);

        DataTable Mz_GetTradeDetailInfo(int iMIID, int iPatientType, string sDeptCode, string sTimeStat, string sTimeStop);
    }
}
