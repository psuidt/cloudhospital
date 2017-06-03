using System;
using System.Collections.Generic;
using System.Data;
using HIS_Entity.IPDoctor;
using HIS_Entity.IPManage;

namespace HIS_IPNurse.Dao
{
    /// <summary>
    /// 医嘱发送接口类
    /// </summary>
    public interface IOrderCheckDao
    {
        /// <summary>
        /// 获取未转抄医嘱病人列表
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="orderCategory">医嘱类别(长期/临时)</param>
        /// <param name="orderStatus">医嘱类型(新开/新停)</param>
        /// <returns>未转抄医嘱病人列表</returns>
        DataTable GetOrderCheckPatList(int deptId, string orderCategory, string orderStatus);

        /// <summary>
        /// 获取未转抄医嘱列表
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="orderCategory">医嘱类别(长期/临时)</param>
        /// <param name="orderStatus">医嘱类型(新开/新停)</param>
        /// <returns>未转抄医嘱列表</returns>
        DataTable GetOrederCheckInfo(int deptId, string orderCategory, string orderStatus);

        /// <summary>
        /// 通过GroupList获取接口类和开始结束时间
        /// </summary>
        /// <param name="groupIdList">组号集合</param>
        /// <param name="endTime">发送结束时间</param>
        /// <returns>接口类和开始结束时间</returns>
        DataTable GetIPOrderCheckByGroupId(List<int> groupIdList, DateTime endTime);

        /// <summary>
        /// 查询药品库存
        /// </summary>
        /// <returns>药品库存信息</returns>
        List<IP_DrugStore> GetDrugStore();

        /// <summary>
        /// 查询项目
        /// </summary>
        /// <returns>项目列表</returns>
        List<IP_DrugStore> GetItemInfo();

        /// <summary>
        /// 获取病人医嘱关联的费用列表
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="groupID">分组ID</param>
        /// <returns>病人医嘱关联的费用列表</returns>
        DataTable GetOrderRelationFeeList(int patListID, int groupID);

        /// <summary>
        /// 获取用法执行单表
        /// </summary>
        /// <returns>用法执行单表</returns>
        DataTable GetExecuteBillChannel();

        /// <summary>
        /// 获取药品医嘱
        /// </summary>
        /// <param name="groupIdList">组号列表</param>
        /// <returns>药品医嘱</returns>
        List<IPD_OrderRecord> GetIPDOrderByGroupId(List<int> groupIdList);

        /// <summary>
        /// 获取总量取整的药品
        /// </summary>
        /// <returns>总量取整的药品信息</returns>
        DataTable GetDrugs();

        /// <summary>
        /// 获取实时项目药品的价格
        /// </summary>
        /// <returns>实时项目药品的价格信息</returns>
        DataTable GetStorePrice();

        /// <summary>
        /// 自动发送-获取
        /// </summary>
        /// <param name="endTime">结束时间</param>
        /// <returns>自动发送的数据</returns>
        DataTable GetIPOrderCheckGroupList(DateTime endTime);

        /// <summary>
        /// 获取发送的GroupId
        /// </summary>
        /// <param name="iPatientId">病人登记ID</param>
        /// <param name="iOrderCategory">医嘱类别</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>组号列表</returns>
        DataTable GetIPOrderCheckGroupListByPatListId(int iPatientId, int iOrderCategory, DateTime endTime);

        /// <summary>
        /// 发送完更新医嘱表
        /// </summary>
        /// <param name="iGroupId">组号</param>
        /// <param name="execDate">执行时间</param>
        /// <param name="empId">操作员ID</param>
        /// <returns>true：更新成功</returns>
        bool UpdateOrderSend(int iGroupId, DateTime execDate, int empId);

        #region 账单相关
        /// <summary>
        /// 获取病人的费用账单
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="orderType">2-长期账单 3-临时账单</param>
        /// <returns>病人的费用账单</returns>
        DataTable GetPatFeeAccount(int patListID, int orderType);

        /// <summary>
        /// 获取可发送账单的生成表ID
        /// </summary>
        /// <param name="iPatientId">病人登记ID</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>账单ID集合</returns>
        DataTable GetFeeAccount(int iPatientId, DateTime endTime);

        /// <summary>
        /// 获取可发送床位费的病人ID
        /// </summary>
        /// <param name="iPatientID">病人登记ID</param>
        /// <returns>病人ID集合</returns>
        DataTable GetBedPatientIdList(int iPatientID);
        #endregion

        /// <summary>
        /// 按科室获取自动发送的GroupId
        /// </summary>
        /// <param name="iDeptId">科室ID</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>组号集合</returns>
        DataTable GetDeptIPOrderCheckGroupList(int iDeptId, DateTime endTime);
    }
}
