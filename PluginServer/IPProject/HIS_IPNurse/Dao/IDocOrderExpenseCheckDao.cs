using System;
using System.Data;

namespace HIS_IPNurse.Dao
{
    /// <summary>
    /// 医嘱费用核对数据库接口
    /// </summary>
    public interface IDocOrderExpenseCheckDao
    {
        /// <summary>
        /// 获取病人状态
        /// </summary>
        /// <param name="patlistID">病人ID</param>
        /// <returns>病人状态信息</returns>
        DataTable GetPatientStatus(int patlistID);

        /// <summary>
        /// 获取医嘱病人列表
        /// </summary>
        /// <param name="deptId">入院科室ID</param>
        /// <param name="status">病人状态</param>
        /// <returns>医嘱病人列表</returns>
        DataTable GetPatientList(int deptId, int status);

        /// <summary>
        /// 获取病人已记账的长期临时账单费用以及床位费
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人已记账的长期临时账单费用以及床位费</returns>
        DataTable GetPatLongOrderSumPay(int patListID);

        /// <summary>
        /// 获取病人预交金累计交费金额
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人预交金累计交费金额</returns>
        DataTable GetPatSumPay(int patListID);

        /// <summary>
        /// 获取病人医嘱列表
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="orderType">医嘱类型</param>
        /// <returns>病人医嘱列表</returns>
        DataTable GetPatOrderList(int patListID, int orderType);

        /// <summary>
        /// 获取医嘱关联记账费用列表
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="orderID">医嘱Id</param>
        /// <param name="groupID">医嘱分组ID</param>
        /// <returns>医嘱关联记账费用列表</returns>
        DataTable GetOrderFeeList(int patListID, int orderID, int groupID);

        /// <summary>
        /// 获取医嘱关联记账费用按项目汇总列表
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="orderID">医嘱Id</param>
        /// <param name="groupID">医嘱分组ID</param>
        /// <returns>项目汇总列表</returns>
        DataTable GetOrderSumFeeList(int patListID, int orderID, int groupID);

        /// <summary>
        /// 获取费用对象
        /// </summary>
        /// <param name="feeRecordID">费用ID</param>
        /// <returns>费用对象</returns>
        DataTable GetIPFeeItemRecordInfo(int feeRecordID);

        /// <summary>
        /// 检查药品是否已发药、检查项目是否已做检查
        /// </summary>
        /// <param name="feeRecordID">费用明细ID</param>
        /// <returns>true：未发药/false：已发药</returns>
        bool CheckIsMedicine(int feeRecordID);

        /// <summary>
        /// 根据处方明细ID查询处方是否已生成统领单
        /// </summary>
        /// <param name="feeRecordID">处方明细ID</param>
        /// <returns>true已生成</returns>
        bool CheckIsGenerateDrugBillDetail(int feeRecordID);

        /// <summary>
        /// 根据统领单明细ID删除统领单明细
        /// </summary>
        /// <param name="feeRecordID">处方明细ID</param>
        /// <returns>true删除成功</returns>
        bool DelDrugBillDetail(int feeRecordID);

        /// <summary>
        /// 查询病人所有已记账的费用列表
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="orderType">账单类型</param>
        /// <returns>已记账的费用列表</returns>
        DataTable GetCostList(int patListID, string orderType);

        /// <summary>
        /// 获取费用汇总数据
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="orderType">账单类型</param>
        /// <returns>费用汇总数据</returns>
        DataTable GetSumCostList(int patListID, string orderType);

        /// <summary>
        /// 检查费用记录是否已记账
        /// </summary>
        /// <param name="generateId">费用生成ID</param>
        /// <returns>true已记账</returns>
        bool IsFeeCharge(int generateId);

        /// <summary>
        /// 获取入院病人登记信息
        /// </summary>
        /// <param name="patListID">病人登记信息ID</param>
        /// <returns>病人登记信息</returns>
        DataTable GetPatListInfo(int patListID);

        /// <summary>
        /// 检查床位费是否存在重复计费
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="chargeDate">处方日期</param>
        /// <returns>true不存在</returns>
        bool IsExistenceBedFeeData(int patListID, DateTime chargeDate);

        /// <summary>
        /// 取得病人关联的所有床位
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人关联的所有床位</returns>
        DataTable GetPatientBedList(int patListID);

        /// <summary>
        /// 根据病人ID查询病人的床位费用
        /// </summary>
        /// <param name="bedID">床位ID</param>
        /// <param name="feeType">费用类型</param>
        /// <returns>床位费用列表</returns>
        DataTable GetBedFeeItemList(int bedID, int feeType);

        /// <summary>
        /// 检查是否存在重复账单
        /// </summary>
        /// <param name="generateID">费用生成ID</param>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="chargeDate">处方日期</param>
        /// <returns>true不存在</returns>
        bool IsExistenceItemAccountingData(int generateID, int patListID, DateTime chargeDate);

        /// <summary>
        /// 根据组合项目ItemID获取组合项目明细列表
        /// </summary>
        /// <param name="itemId">组合项目ID</param>
        /// <returns>组合项目明细列表</returns>
        DataTable CombinationProjectDetails(int itemId);

        /// <summary>
        /// 停用长期账单
        /// </summary>
        /// <param name="generateID">账单ID</param>
        void StopFeeLongOrderData(int generateID);

        /// <summary>
        /// 查询病人账单列表
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="orderType">账单类型</param>
        /// <returns>病人账单列表</returns>
        DataTable GetPatFeeItemGenerate(int patListID, int orderType);

        /// <summary>
        /// 费用冲账或取消冲账时，修改申请表状态
        /// </summary>
        /// <param name="presDetailID">医嘱ID</param>
        /// <param name="isReturns">记录状态</param>
        /// <returns>true修改成功</returns>
        bool UpdateEXAMedicalApplyDetail(int presDetailID, int isReturns);

        /// <summary>
        /// 获取记账开始时间以及记账天数
        /// </summary>
        /// <param name="generateIdList">费用生成ID列表</param>
        /// <param name="endTime">记账结束时间</param>
        /// <param name="isBedFee">true:床位费/false:账单费用</param>
        /// <param name="patListId">病人登记ID</param>
        /// <returns>记账开始时间以及记账天数</returns>
        DataTable GetAccountDate(string generateIdList, DateTime endTime, bool isBedFee, int patListId);

        /// <summary>
        /// 账单记账获取费用生成数据并且更新价格
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <param name="generateID">账单Id</param>
        /// <param name="isBedFee">床位费标志</param>
        /// <returns>最新费用数据</returns>
        DataTable GetFeeItemGenerateData(int patListId, int generateID, bool isBedFee);

        /// <summary>
        /// 检查病人在院状态
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <returns>病人在院状态</returns>
        DataTable CheckPatientStatus(int patListId);
    }
}
