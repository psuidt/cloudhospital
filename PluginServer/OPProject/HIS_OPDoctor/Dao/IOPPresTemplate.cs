using System.Collections.Generic;
using System.Data;
using HIS_Entity.ClinicManage;

namespace HIS_OPDoctor.Dao
{
    /// <summary>
    /// 门诊处方模板Dao接口
    /// </summary>
    public interface IOPPresTemplate
    {
        /// <summary>
        /// 获取模板信息
        /// </summary>
        /// <param name="intLevel">模板级别</param>
        /// <param name="workID">机构id</param>
        /// <param name="presType">处方类型</param>
        /// <param name="deptID">科室id</param>
        /// <param name="empID">人员id</param>
        /// <returns>模板信息</returns>
        List<OPD_PresMouldHead> GetPresTemplate(int intLevel, int workID, int presType, int deptID, int empID);
       
        /// <summary>
        /// 检验同级别下名称是否重复
        /// </summary>
        /// <param name="name">模板名称</param>
        /// <param name="presType">处方类型</param>
        /// <param name="level">模板级别</param>
        /// <param name="pid">父id</param>
        /// <returns>名称数据</returns>
        DataTable CheckMoudelName(string name, int presType, int level, int pid);

        /// <summary>
        /// 获取处方模板内容
        /// </summary>
        /// <param name="presHeadID">处方头id</param>
        /// <returns>处方模板内容</returns>
        DataTable GetPresTemplateData(int presHeadID);

        /// <summary>
        /// 删除处方模板明细
        /// </summary>
        /// <param name="presMouldDetailID">处方模板明细ID</param>
        /// <returns>true成功</returns>
        bool DeletePrescriptionData(int presMouldDetailID);

        /// <summary>
        /// 删除指定处方数据
        /// </summary>
        /// <param name="presMouldHeadID">模板ID</param>
        /// <param name="presNo">处方编号</param>
        /// <returns>true成功</returns>
        bool DeletePrescriptionData(int presMouldHeadID, int presNo);

        /// <summary>
        /// 更新处方组号
        /// </summary>
        /// <param name="list">模板明细列表</param>
        /// <returns>true成功</returns>
        bool UpdatePresNoAndGroupId(List<OPD_PresMouldDetail> list);
    }
}
