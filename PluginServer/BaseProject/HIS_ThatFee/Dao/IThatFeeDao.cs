using System.Collections.Generic;
using System.Data;
using System.Text;
using HIS_Entity.ClinicManage;

namespace HIS_ThatFee.Dao
{
    /// <summary>
    /// 医技确费
    /// </summary>
    public interface IThatFeeDao
    {
        #region 医技确费
        /// <summary>
        /// 获取医技确费网格数据
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="systemType">0门诊1住院</param>
        /// <returns>确费数据</returns>
        DataTable GetThatFee(StringBuilder strWhere, int systemType);

        /// <summary>
        /// 获取执行科室
        /// </summary>
        /// <returns>执行科室数据</returns>
        DataTable GetDept();

        /// <summary>
        /// 获取门诊费用明细
        /// </summary>
        /// <param name="presId">明细ID</param>
        /// <returns>门诊费用明细数据</returns>
        DataTable GetOPFee(int presId);

        /// <summary>
        /// 获取住院费用明细
        /// </summary>
        /// <param name="presId">明细ID</param>
        /// <returns>住院费用明细数据</returns>
        DataTable GetIPFee(int presId);

        /// <summary>
        /// 获取医技信息
        /// </summary>
        /// <param name="presIds">多个明细ID字符串</param>
        /// <returns>医技信息</returns>
        DataTable ConfigInfo(string presIds);

        /// <summary>
        /// 获取医技确费明细表
        /// </summary>
        /// <param name="presIds">明细ID</param>
        /// <returns>医技确费实体</returns>
        EXA_MedicalConfir GetConfir(int presIds);

        /// <summary>
        /// 获取医技确费明细列表
        /// </summary>
        /// <param name="ids">确费id</param>
        /// <returns>医技确费明细列表</returns>
        List<EXA_MedicalConfir> GetConfirList(string ids);

        /// <summary>
        /// 修改医技确费状态
        /// </summary>
        /// <param name="presIds">明细ID</param>
        /// <param name="applyStatus">申请状态</param>
        /// <param name="distributeFlag">确费标识</param>
        /// <param name="systemType">0门诊1住院</param>
        void UpdateStatus(int presIds, int applyStatus, int distributeFlag, int systemType);

        /// <summary>
        /// 修改医技作废状态
        /// </summary>
        /// <param name="presIds">明细ID</param>
        void UpdateConfir(int presIds);

        /// <summary>
        /// 根据医技确费头表ID获取明细
        /// </summary>
        /// <param name="confirId">确费id</param>
        /// <returns>确费明细列表</returns>
        List<EXA_MedicalConfirDetail> GetConfirDetailList(int confirId);

        #endregion

        #region 医技确费工作量统计
        /// <summary>
        /// 获取医技工作量统计信息
        /// </summary>
        /// <param name="confirDeptID">确费部门id</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="type">类型</param>
        /// <returns>医技工作量统计信息数据</returns>
        DataTable GetThatFeeCount(string confirDeptID, string beginDate, string endDate, int type);

        /// <summary>
        /// 根据医技项目获取医技总金额
        /// </summary>
        /// <param name="id">项目id</param>
        /// <param name="type">门诊确费=0,住院确费=1</param>
        /// <returns>总额</returns>
        string GetThatFeeTotal(string id, int type);

        /// <summary>
        /// 获取是否申请退款
        /// </summary>
        /// <param name="presId">处方ID</param>
        /// <returns>申请退款数据</returns>
        DataTable GetPayFlag(int presId);
        #endregion

        #region 医技明细查询
        /// <summary>
        /// 根据执行科室ID获取医技项目
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <returns>医技项目数据</returns>
        DataTable GetExamItem(string deptId);

        /// <summary>
        /// 获取医技确费网格数据
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="systemType">0门诊1住院</param>
        /// <returns>确费数据</returns>
        DataTable GetThatFeeDetail(StringBuilder strWhere, int systemType);
        #endregion
    }
}
