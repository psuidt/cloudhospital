using HIS_Entity.MIManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_MIInterface.Dao
{
    public interface IMZInterface
    {
        //bool Mz_Register(RegInfo regInfo);
        DataTable Mz_GetPayRecordDetailForPrint(int PayRecordId);
        /// <summary>
        /// 获取基础数据匹配
        /// </summary>
        /// <returns></returns>
        DataTable MZ_GetMIDataMatch();
        bool MZ_ClearData();
    }
}
