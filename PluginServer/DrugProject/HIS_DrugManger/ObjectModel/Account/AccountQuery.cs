using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_DrugManger.ObjectModel.Account
{
    /// <summary>
    /// 账目查询
    /// </summary>
    abstract class AccountQuery: AbstractObjectModel
    {
        /// <summary>
        /// 查询药品流水账
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>流水账列表</returns>
        public abstract DataTable AccountReport(Dictionary<string ,string> condition);

        /// <summary>
        /// 查询进销存账
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>流水账列表</returns>
        public abstract DataTable CSPJReport(Dictionary<string, string> condition);

        /// <summary>
        /// 查询药品分类明细账
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>分类明细账列表</returns>
        public abstract DataTable OrderAccountReport(Dictionary<string, string> condition);

        /// <summary>
        /// 查询收发存汇总
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>收发存汇总表</returns>
        public abstract DataTable RPT_CSPJAccoount(Dictionary<string, string> condition);
    }
}
