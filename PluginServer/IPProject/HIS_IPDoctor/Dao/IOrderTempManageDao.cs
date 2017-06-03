using System.Collections.Generic;
using System.Data;
using HIS_Entity.ClinicManage;

namespace HIS_IPDoctor.Dao
{
    /// <summary>
    /// 模板维护数据库访问接口类
    /// </summary>
    public interface IOrderTempManageDao
    {
        /// <summary>
        /// 获取医嘱模板列表
        /// </summary>
        /// <param name="tempLevel">模板级别</param>
        /// <param name="deptId">所属科室</param>
        /// <param name="empId">所属用户</param>
        /// <returns>模板列表</returns>
        List<IPD_OrderModelHead> GetOrderTempList(int tempLevel, int deptId, int empId);

        /// <summary>
        /// 根据模板头ID删除模板明细数据
        /// </summary>
        /// <param name="modelHeadID">模板头ID</param>
        /// <param name="isModelType">是否为模板分类</param>
        void DelOrderTempDetails(int modelHeadID, bool isModelType);

        /// <summary>
        /// 删除模板分类关联的模板
        /// </summary>
        /// <param name="modelHeadID">模板分类ID</param>
        void DelOrderTempByModelType(int modelHeadID);

        /// <summary>
        /// 根据模板头ID获取模板明细
        /// </summary>
        /// <param name="modelHeadID">模板头ID</param>
        /// <returns>模板明细数据</returns>
        DataTable GetOrderTempDetail(int modelHeadID);

        /// <summary>
        /// 根据模板明细ID删除模板明细数据
        /// </summary>
        /// <param name="modelDetailID">模板明细ID</param>
        /// <returns>bool</returns>
        bool DelOrderDetailsData(string modelDetailID);
    }
}
