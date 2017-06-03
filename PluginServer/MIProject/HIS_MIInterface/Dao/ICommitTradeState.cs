using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_MIInterface.Dao
{
    public interface ICommitTradeState
    {
        DataTable Mz_GetTradeInfoByCon(string sSerialNO, string sInvoiceNo, string sTradeNo);
        DataTable Mz_GetTradeInfoByCon(string sCardNo, DateTime Time);

        DataTable Mz_GetPayRecordDetailForPrint(int PayRecordId);

        DataTable MZ_ExportJzxx(DateTime tradeDate);
    }
}
