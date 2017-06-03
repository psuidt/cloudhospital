using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HIS_MIInterface.Dao
{
    public interface IMatchInterface
    {
        //获取医院目录
        /// <summary>
        /// 获取医院目录
        /// </summary>
        /// <param name="catalogType">1：药品，2：材料，3：收费项目</param>
        /// <returns></returns>
        DataTable M_GetHISCatalogInfo(int catalogType, int stopFlag, int matchFlag, int ybID);

        //获取医院的医保目录
        /// <summary>
        /// 获取医院目录
        /// </summary>
        /// <param name="catalogType">1：药品，2：材料，3：收费项目</param>
        /// <param name="ybId">医保类型</param>
        /// <returns></returns>
        DataTable M_GetMICatalogInfo(int catalogType, int ybId);
        /// <summary>
        /// 获取匹配信息目录
        /// </summary>
        /// <param name="catalogType">1：药品，2：材料，3：收费项目 0:全部</param>
        /// <param name="ybId">医保类型</param>
        /// <returns></returns>
        DataTable M_GetMatchCatalogInfo(int catalogType, int ybId, int auditFlag);

        //获取医保类型
        DataTable M_GetMIType();

        /// <summary>
        /// 清空匹配数据
        /// </summary>
        /// <param name="ybId"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
        bool M_DeleteMatchLogs(int ybId, string itemType);

        bool M_SaveMatchLogs(DataTable dt, int ybId);


        bool M_UpdateMatchLogs(string id, string auditFlag);

        bool M_UpdateAllMatchLogs(int ybId);
        /// <summary>
        /// 根据医保匹配，更新本院目录级别
        /// </summary>
        /// <param name="ybId"></param>
        /// <returns></returns>
        bool M_UpdateDrugLogLevel(int ybId);

        bool M_UpdateFeeItemLogLevel(int ybId);

        bool M_UpdateMWLogLevel(int ybId);
        

        /// <summary>
        /// 清空医保数据
        /// </summary>
        /// <param name="ybId"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
        bool M_DeleteMILog(int ybId);

        bool M_SaveMILog(DataTable dt, int ybId);
    }
}
