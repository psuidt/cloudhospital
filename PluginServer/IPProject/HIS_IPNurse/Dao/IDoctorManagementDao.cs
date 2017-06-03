using System;
using System.Data;

namespace HIS_IPNurse.Dao
{
    /// <summary>
    /// 护士站医嘱转抄SQL操作接口
    /// </summary>
    public interface IDoctorManagementDao
    {
        /// <summary>
        /// 获取未转抄医嘱病人列表
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="orderCategory">医嘱类别(长期/临时)</param>
        /// <param name="orderStatus">医嘱类型(新开/新停)</param>
        /// <param name="astFlag">皮试医嘱</param>
        /// <param name="isTrans">是否已转抄</param>
        /// <returns>病人列表</returns>
        DataTable GetDocPatList(int deptId, string orderCategory, string orderStatus, string astFlag, bool isTrans);

        /// <summary>
        /// 获取未转抄医嘱列表
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="orderCategory">医嘱类别(长期/临时)</param>
        /// <param name="orderStatus">医嘱类型(新开/新停)</param>
        /// <param name="astFlag">皮试医嘱</param>
        /// <param name="isTrans">是否已转抄</param>
        /// <returns>医嘱列表</returns>
        DataTable GetPatNotCopiedDocList(int deptId, string orderCategory, string orderStatus, string astFlag, bool isTrans);

        /// <summary>
        /// 获取费用项目列表
        /// </summary>
        /// <returns>费用项目列表</returns>
        DataTable GetDocFeeItemList();

        /// <summary>
        /// 获取病人医嘱关联的费用列表
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="groupID">分组ID</param>
        /// <returns>病人医嘱关联的费用列表</returns>
        DataTable GetPatDocRelationFeeList(int patListID, int groupID);

        /// <summary>
        /// 验证医嘱是否已转抄，已转抄的医嘱不允许补录费用
        /// </summary>
        /// <param name="patListID">病人ID</param>
        /// <param name="groupID">组号ID</param>
        /// <returns>true：已转抄</returns>
        DataTable CheckOrderStatus(int patListID, int groupID);

        /// <summary>
        /// 获取病人登记信息
        /// </summary>
        /// <param name="patListID">病人入院登记ID</param>
        /// <returns>病人登记信息</returns>
        DataTable GetPatientInfo(int patListID);

        /// <summary>
        /// 获取医嘱信息
        /// </summary>
        /// <param name="patListID">病人入院登记ID</param>
        /// <param name="groupID">组号</param>
        /// <returns>医嘱信息</returns>
        DataTable GetOrderRecord(int patListID, int groupID);

        /// <summary>
        /// 根据医嘱Id获取医嘱皮试状态
        /// </summary>
        /// <param name="orderID">医嘱ID</param>
        /// <returns>医嘱信息</returns>
        DataTable GetOrderRecordAstFlag(string orderID);

        /// <summary>
        /// 医嘱转抄
        /// </summary>
        /// <param name="arrayOrderID">医嘱ID列表</param>
        /// <param name="empID">转抄护士</param>
        void UpdateDocOrder(string arrayOrderID, int empID);

        /// <summary>
        /// 根据病人ID以及分组ID查出整组医嘱的医嘱ID号
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="groupID">分组ID</param>
        /// <returns>医嘱ID号集合</returns>
        DataTable GetOrderIDList(int patListID, int groupID);

        /// <summary>
        /// 取消转抄
        /// </summary>
        /// <param name="arrayOrderID">医嘱ID集合</param>
        void CancelTransDocOrder(string arrayOrderID);

        /// <summary>
        /// 检查医嘱是否已发送
        /// </summary>
        /// <param name="arrayOrderID">医嘱ID列表</param>
        /// <returns>已发送医嘱列表</returns>
        DataTable IsCheckOrderSend(string arrayOrderID);

        /// <summary>
        /// 获取医嘱病人列表
        /// </summary>
        /// <param name="deptId">入院科室ID</param>
        /// <param name="status">病人状态</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>病人列表</returns>
        DataTable GetPatientList(int deptId, int status, DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取病人已记账的长期临时账单费用以及床位费
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人已记账的长期临时账单费用以及床位费列表</returns>
        DataTable GetPatLongOrderSumPay(int patListID);

        /// <summary>
        /// 获取病人预交金累计交费金额
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人预交金累计交费金额信息</returns>
        DataTable GetPatSumPay(int patListID);

        /// <summary>
        /// 删除医嘱关联费用数据
        /// </summary>
        /// <param name="generateID">费用生成ID</param>
        void DelFeeItemGenerate(int generateID);

        #region 皮试
        /// <summary>
        /// 获取皮试数据
        /// </summary>
        /// <param name="iDeptID">科室ID</param>
        /// <param name="bIsCheckeed">是否已标注</param>
        /// <param name="sBDate">开始时间</param>
        /// <param name="sEDate">结束时间</param>
        /// <returns>皮试数据</returns>
        DataTable QuerySkinTestData(int iDeptID, bool bIsCheckeed, string sBDate, string sEDate);
        #endregion
    }
}