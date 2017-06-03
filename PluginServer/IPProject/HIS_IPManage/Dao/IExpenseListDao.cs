using System.Data;

namespace HIS_IPManage.Dao
{
    /// <summary>
    /// 费用清单IDAO
    /// </summary>
    public interface IExpenseListDao
    {
        /// <summary>
        /// 获取科室病人费用信息
        /// </summary>
        /// <param name="sDeptCode">科室编码</param>
        /// <param name="sDTInBegin">入院开始日期</param>
        /// <param name="sDTInEnd">入院结束日期</param>
        /// <param name="iPatientState">病人状态</param>
        /// <param name="sPatient">病人名或住院号</param>
        /// <param name="iJsId">结算ID</param>
        /// <returns>科室病人费用信息</returns>
        DataTable GetDeptPatientInfoList(string sDeptCode,string sDTInBegin,string sDTInEnd,int iPatientState, string sPatient, int iJsId);

        /// <summary>
        /// 获取病人费用清单数据
        /// </summary>
        /// <param name="iWorkId">机构ID</param>
        /// <param name="iPatientId">病人ID</param>
        /// <param name="iListType">清单类型  1-项目明细  2-一日清单  3-发票项目  4-项目汇总</param>
        /// <param name="sBDate">开始时间</param>
        /// <param name="sEDate">结束事件</param>
        /// <param name="iJsState">计算状态 0.未结算 1，中途，2，出院，3，欠费</param>
        /// <param name="iDateType">0.记账时间 1.费用时间</param>
        /// <param name="iCostHeadId">结算头ID</param>
        /// <returns>病人费用清单数据</returns>
        DataTable GetPatientFeeInfo(int iWorkId, int iPatientId, int iListType, string sBDate, string sEDate, int iJsState, int iDateType,int iCostHeadId);

        /// <summary>
        /// 获取病人费用信息汇总
        /// </summary>
        /// <param name="iWorkId">机构ID</param>
        /// <param name="iPatientId">病人登记ID</param>
        /// <param name="iCostHeadId">结算头ID</param>
        /// <returns>病人费用汇总信息</returns>
        DataTable GetPatientFeeSumByPatientId(int iWorkId, int iPatientId, int iCostHeadId);
    }
}
