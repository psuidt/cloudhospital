using System;
using System.Collections.Generic;
using System.Data;
using HIS_Entity.IPManage;

namespace HIS_IPManage.Dao
{
    /// <summary>
    /// 住院护士站SQL接口
    /// </summary>
    public interface IIPManageDao
    {
        /// <summary>
        /// 住院病人列表取得
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="dept">入院科室</param>
        /// <param name="patType">病人类型</param>
        /// <param name="selectParm">检索条件(住院号、病案号、床位号)</param>
        /// <param name="patStatus">病人状态</param>
        /// <param name="isPay">预交金界面查询标志</param>
        /// <returns>病人列表</returns>
        DataTable GetPatientList(
            DateTime startTime,
            DateTime endTime,
            int dept,
            int patType,
            string selectParm,
            string patStatus,
            bool isPay);

        /// <summary>
        /// 获取入院病人信息
        /// </summary>
        /// <param name="patientID">病人ID</param>
        /// <returns>入院病人信息</returns>
        DataTable GetPatientInfo(int patientID);

        /// <summary>
        /// 获取入院病人登记信息
        /// </summary>
        /// <param name="patListID">病人登记信息ID</param>
        /// <returns>入院病人登记信息</returns>
        DataTable GetPatListInfo(int patListID);

        /// <summary>
        /// 取消入院
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="patName">病人名</param>
        /// <returns>true：取消成功</returns>
        bool CancelAdmission(int patListID, string patName);

        /// <summary>
        /// 根据入院病人登记ID获取病人预交金以及费用记录信息
        /// </summary>
        /// <param name="patListID">病人入院登记ID</param>
        /// <returns>病人预交金以及费用记录信息</returns>
        DataTable GetPatientCostList(int patListID);

        /// <summary>
        /// 根据ID获取预交金记录
        /// </summary>
        /// <param name="depositID">预交金ID</param>
        /// <returns>预交金记录</returns>
        DataTable GetPayADeposit(int depositID);

        /// <summary>
        /// 根据ID修改打印次数
        /// </summary>
        /// <param name="depositID">预交金ID</param>
        /// <returns>true:修改成功</returns>
        int UpdatePrintTime(int depositID);

        /// <summary>
        /// 获取病人预交金缴纳记录
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="serialNumber">住院号</param>
        /// <param name="isSettlement">是否是住院结算时查询</param>
        /// <returns>病人预交金缴纳记录</returns>
        DataTable GetPaymentList(int patListID, decimal serialNumber, bool isSettlement);

        /// <summary>
        /// 预交金退费
        /// </summary>
        /// <param name="depositID">预交金ID</param>
        /// <returns>退费成功或失败</returns>
        bool Refund(int depositID);

        /// <summary>
        /// 写入预交金退费记录
        /// </summary>
        /// <param name="depositID">预交金记录ID</param>
        /// <returns>true:保存成功</returns>
        bool RefundInsert(int depositID);

        /// <summary>
        /// 获取预交金结算记录
        /// </summary>
        /// <param name="depositID">预交金ID</param>
        /// <returns>预交金结算记录</returns>
        DataTable GetCostHeadInfo(int depositID);

        /// <summary>
        /// 根据病人登记ID取得病人入院状态
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人入院状态集合</returns>
        DataTable GetPatStatus(int patListID);

        /// <summary>
        /// 查询同病区同房间是否存在相同的床位号
        /// </summary>
        /// <param name="wardID">病区ID</param>
        /// <param name="roomNo">房间号</param>
        /// <param name="bedNo">床位号</param>
        /// <returns>是否存在相同床位号-true:不存在/false：存在</returns>
        bool IsExistenceCheck(int wardID, string roomNo, string bedNo);

        /// <summary>
        /// 根据病区ID获取床位列表 
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <returns>床位列表 </returns>
        DataTable GetBedList(int wardId);

        /// <summary>
        /// 根据床位ID获取费用列表
        /// </summary>
        /// <param name="bedID">床位ID</param>
        /// <returns>费用列表</returns>
        DataTable GetBedFreeList(int bedID);

        /// <summary>
        /// 停用或启用病床
        /// </summary>
        /// <param name="isStoped">状态</param>
        /// <param name="bedId">病床ID</param>
        /// <returns>true：停用或启用成功</returns>
        bool StoppedOrEnabledBed(int isStoped, int bedId);

        /// <summary>
        /// 查询病床是否已分配病人
        /// </summary>
        /// <param name="bedID">病床ID</param>
        /// <returns>true:已分配/false:未分配</returns>
        bool BedIsUsed(int bedID);

        /// <summary>
        /// 修改床位信息时，先删除数据库中现有的床位费用数据
        /// </summary>
        /// <param name="bedID">床位ID</param>
        /// <returns>true：删除成功</returns>
        bool DeleteBedFreeList(int bedID);

        /// <summary>
        /// 根据病区ID查询床位以及床位分配情况
        /// </summary>
        /// <param name="wardID">病区ID</param>
        /// <returns>床位列表</returns>
        DataTable GetBedManageList(int wardID);

        /// <summary>
        /// 查询所有未分配床位的病人
        /// </summary>
        /// <param name="wardid">病区ID</param>
        /// <param name="status">病人状态</param>
        /// <returns>未分配床位病人列表</returns>
        DataTable GetNotHospitalPatList(int wardid, string status);

        /// <summary>
        /// 获取床位绑定的医生以及护士ID
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        /// <returns>医生和护士Id</returns>
        DataTable GetDoctorNurseID(int wardId, string bedNo);

        /// <summary>
        /// 修改入院病人床位分配信息
        /// </summary>
        /// <param name="bedNo">床位号</param>
        /// <param name="doctorId">医生ID</param>
        /// <param name="nurseId">护士ID</param>
        /// <param name="patListId">病人登记ID</param>
        /// <returns>修改成功或失败</returns>
        bool UpdatePatList(string bedNo, int doctorId, int nurseId, int patListId);

        /// <summary>
        /// 修改床位分配信息
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="patName">病人名</param>
        /// <param name="sex">性别</param>
        /// <param name="deptId">科室ID</param>
        /// <param name="doctorId">医生Id</param>
        /// <param name="nurseId">护士ID</param>
        /// <param name="bedId">床位ID</param>
        /// <returns>修改成功或失败</returns>
        bool UpdatePatBedInfo(int patListID, string patName, string sex, int deptId, int doctorId, int nurseId, int bedId);

        /// <summary>
        /// 检查床位是否已被占用
        /// </summary>
        /// <param name="bedId">床位ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        /// <returns>>true：空床/false：被占用</returns>
        bool IsBedOccupy(int bedId, int wardId, string bedNo);

        /// <summary>
        /// 取消分床修改病人登记信息
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <returns>true：修改成功</returns>
        bool CancelBedUpdIpPatList(int patListId);

        /// <summary>
        /// 取消分床修改床位信息
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        /// <returns>true：修改成功</returns>
        bool CancelBedUpdIpBedInfo(int wardId, string bedNo);

        /// <summary>
        /// 病人换床--根据病区ID查询病区所有空床
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <returns>空床列表</returns>
        DataTable GetBedNoList(int wardId);

        /// <summary>
        /// 换床--记录新病床分配日志
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        /// <param name="newBedNo">新床位号</param>
        /// <param name="empId">操作员Id</param>
        /// <param name="workID">机构ID</param>
        /// <returns>true:保存成功</returns>
        bool SaveNewBedLogInfo(int wardId, string bedNo, string newBedNo, int empId, int workID);

        /// <summary>
        /// 病人换床--更新新床位的数据
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <param name="newBedNo">新床位号</param>
        /// <param name="oldBedNo">旧床位号</param>
        /// <param name="isPackBed">是否为加床</param>
        /// <returns>true：保存成功</returns>
        bool UpdateNewBed(int wardId, string newBedNo, string oldBedNo, int isPackBed);

        /// <summary>
        /// 病人换床--修改病人登记床位数据
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <param name="bedNo">床位号</param>
        /// <returns>true：保存成功</returns>
        bool PatBedChanging(int patListId, string bedNo);

        /// <summary>
        /// 更换医生或护士保存病人登记信息
        /// </summary>
        /// <param name="patListId">病人登记信息</param>
        /// <param name="doctorid">医生ID</param>
        /// <param name="nurseId">护士Id</param>
        /// <returns>true：修改成功</returns>
        bool UpdatePatListDoctor(int patListId, int doctorid, int nurseId);

        /// <summary>
        /// 更换医生或护士保存床位信息
        /// </summary>
        /// <param name="doctorid">医生ID</param>
        /// <param name="nnrseId">护士Id</param>
        /// <param name="bedId">床位ID</param>
        /// <returns>true：修改成功</returns>
        bool UpdateBedDoctor(int doctorid, int nnrseId, int bedId);

        /// <summary>
        /// 包床--查询所有已分配床位的病人
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <returns>已分配床位病人列表</returns>
        DataTable GetInTheHospitalPatList(int wardId);

        /// <summary>
        /// 获取病人包床的床位列表
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人包床的床位列表</returns>
        DataTable GetPatPackBedList(int patListID);

        /// <summary>
        /// 查询医嘱账单模板列表
        /// </summary>
        /// <param name="workID">机构ID</param>
        /// <param name="deptId">科室ID</param>
        /// <param name="empId">用户ID</param>
        /// <returns>医嘱账单模板列表</returns>
        List<IP_FeeItemTemplateHead> GetIPFeeItemTempList(int workID, int deptId, int empId);

        /// <summary>
        /// 根据模板ID删除模板明细数据
        /// </summary>
        /// <param name="tempHeadID">模板ID</param>
        void DelFeeTempDetail(int tempHeadID);

        /// <summary>
        /// 查询模板明细数据
        /// </summary>
        /// <param name="tempHeadID">模板ID</param>
        /// <returns>模板明细数据</returns>
        DataTable GetFeeTempDetails(int tempHeadID);

        /// <summary>
        /// 获取床位关联的医生和护士ID
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        /// <returns>床位关联的医生和护士ID信息</returns>
        DataTable GetPatDoctorNurseID(int wardId, string bedNo);

        /// <summary>
        /// 账单管理--查询在床病人列表
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="param">检索条件</param>
        /// <returns>病人列表</returns>
        DataTable GetPatientList(int deptId, string param);

        /// <summary>
        /// 获取病人累计缴费金额
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人累计缴费金额</returns>
        DataTable GetPatSumPay(int patListID);

        /// <summary>
        /// 查询病人账单列表
        /// </summary>
        /// <param name="patListID">账单列表</param>
        /// <param name="orderType">账单类型</param>
        /// <returns>病人账单列表</returns>
        DataTable GetPatFeeItemGenerate(int patListID, int orderType);

        /// <summary>
        /// 检查费用记录是否已记账
        /// </summary>
        /// <param name="generateId">费用ID</param>
        /// <returns>true:已记账</returns>
        bool IsFeeCharge(int generateId);

        /// <summary>
        /// 取得所有未停用的账单模板
        /// </summary>
        /// <param name="workID">机构ID</param>
        /// <param name="deptId">科室ID</param>
        /// <param name="empId">操作员ID</param>
        /// <returns>所有未停用的账单模板</returns>
        List<IP_FeeItemTemplateHead> GetFeeItemTempList(int workID, int deptId, int empId);

        /// <summary>
        /// 根据模板ID查询模板对应的账单明细数据
        /// </summary>
        /// <param name="tempHeadID">模板ID</param>
        /// <returns>模板明细数据列表</returns>
        DataTable GetFeeItemTempDetails(int tempHeadID);

        /// <summary>
        /// 获取病人已记账的长期和临时账单金额
        /// </summary>
        /// <param name="patListID">病人登记Id</param>
        /// <returns>病人已记账的长期和临时账单金额</returns>
        DataTable GetPatLongOrderSumPay(int patListID);

        /// <summary>
        /// 检查当前会员是否已办理入院
        /// </summary>
        /// <param name="cardNO">会员卡号</param>
        /// <returns>true:未登记</returns>
        bool CheckPatientInTheHospital(string cardNO);

        /// <summary>
        /// 根据会员卡号获取病案号
        /// </summary>
        /// <param name="memberID">会员ID</param>
        /// <returns>病案号信息</returns>
        DataTable GetCaseNumberByCardNO(int memberID);

        /// <summary>
        /// 停用长期账单
        /// </summary>
        /// <param name="generateID">长期账单ID</param>
        void StopFeeLongOrderData(int generateID);

        /// <summary>
        /// 根据组合项目ItemID获取组合项目明细列表
        /// </summary>
        /// <param name="itemId">组合项目ID</param>
        /// <returns>组合项目明细列表</returns>
        DataTable CombinationProjectDetails(int itemId);

        /// <summary>
        /// 检查是否存在重复账单
        /// </summary>
        /// <param name="generateID">费用生成ID</param>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="chargeDate">处方日期</param>
        /// <returns>true:不存在</returns>
        bool IsExistenceItemAccountingData(int generateID, int patListID, DateTime chargeDate);

        /// <summary>
        /// 检查床位费是否存在重复计费
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="chargeDate">处方日期</param>
        /// <returns>true:不存在</returns>
        bool IsExistenceBedFeeData(int patListID, DateTime chargeDate);

        /// <summary>
        /// 根据病人ID查询病人的床位费用
        /// </summary>
        /// <param name="bedID">床位ID</param>
        /// <param name="feeType">费用类型</param>
        /// <returns>床位费信息</returns>
        DataTable GetBedFeeItemList(int bedID, int feeType);

        /// <summary>
        /// 查询病人所有已记账的费用列表
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="orderType">账单类型</param>
        /// <param name="itemID">项目ID</param>
        /// <param name="startTime">处方开始时间</param>
        /// <param name="endTime">处方结束时间</param>
        /// <returns>已记账的费用列表</returns>
        DataTable GetCostList(int patListID, int orderType, int itemID, DateTime startTime, DateTime endTime);

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
        /// <returns>true:已生成</returns>
        bool CheckIsGenerateDrugBillDetail(int feeRecordID);

        /// <summary>
        /// 根据统领单明细ID删除统领单明细
        /// </summary>
        /// <param name="feeRecordID">统领单明细ID</param>
        /// <returns>true:删除成功</returns>
        bool DelDrugBillDetail(int feeRecordID);

        /// <summary>
        /// 根据科室查询药品统领列表
        /// </summary>
        /// <param name="patDeptID">入院科室</param>
        /// <param name="execDeptID">执行科室</param>
        /// <param name="commandStatus">统领状态</param>
        /// <returns>药品统领列表</returns>
        DataTable GetCommandSheetList(int patDeptID, int execDeptID, bool commandStatus);

        /// <summary>
        /// 取得统领单类型列表
        /// </summary>
        /// <param name="isBillRule">统领类型（true：退药/false：发药）</param>
        /// <returns>统领单类型列表</returns>
        DataTable GetDrugBillTypeList(bool isBillRule);

        /// <summary>
        /// 获取所有已发送统领的住院病人列表
        /// </summary>
        /// <returns>所有已发送统领的住院病人列表</returns>
        DataTable GetHasBeenSentDrugbillPatList();

        /// <summary>
        /// 查询统领单列表
        /// </summary>
        /// <param name="deptId">入院科室ID</param>
        /// <param name="orderStatus">单据状态</param>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="startDate">统领发送开始时间</param>
        /// <param name="endDate">统领发送结束时间</param>
        /// <returns>统领单列表</returns>
        DataTable GetDrugbillOrderList(int deptId, bool orderStatus, int patListID, DateTime startDate, DateTime endDate);

        /// <summary>
        /// 查询统领单汇总数据
        /// </summary>
        /// <param name="billHeadID">统领单头ID</param>
        /// <param name="dispDrugFlag">发药标识</param>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>统领单汇总数据</returns>
        DataTable GetDrugBillSummaryData(int billHeadID, int dispDrugFlag, int patListID);

        /// <summary>
        /// 查询统领单明细数据
        /// </summary>
        /// <param name="billHeadID">统领单头ID</param>
        /// <param name="dispDrugFlag">发药标识</param>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>统领单明细数据</returns>
        DataTable GetDrugBillDetailData(int billHeadID, int dispDrugFlag, int patListID);

        /// <summary>
        /// 重新发送统领单，修改缺药标识
        /// </summary>
        /// <param name="billHeadID">统领单头表ID</param>
        void AgainSendOrder(int billHeadID);

        /// <summary>
        /// 根据统领单头表ID查询明细是否存在已发药的数据
        /// </summary>
        /// <param name="billHeadID">统领单头表ID</param>
        /// <returns>true:存在/false:不存在</returns>
        bool CheckOrderDetailDispDrugFlag(int billHeadID);

        /// <summary>
        /// 删除统领单头表数据
        /// </summary>
        /// <param name="billHeadID">统领单头表ID</param>
        void DelDrugBillHeadData(int billHeadID);

        /// <summary>
        /// 删除统领单明细数据
        /// </summary>
        /// <param name="billHeadID">统领单头表ID</param>
        void DelDrugBillDetailData(int billHeadID);

        /// <summary>
        /// 查询病人所有未停用账单
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人所有未停用账单</returns>
        DataTable GetNotStopOrder(int patListID);

        /// <summary>
        /// 查询所有未统领的账单
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>所有未统领的账单</returns>
        DataTable GetNotGuideOrder(int patListID);

        /// <summary>
        /// 查询病人所有已统领未发药的药品列表
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人所有已统领未发药的药品列表</returns>
        DataTable GetNotDispDrugList(int patListID);

        /// <summary>
        /// 查询病人所有未转抄或未发送医嘱
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>所有未转抄或未发送医嘱</returns>
        DataTable GetNotExecOrder(int patListID);

        /// <summary>
        /// 病人定义出院，清空床位信息
        /// </summary>
        /// <param name="wardID">病区ID</param>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>true:清空成功</returns>
        bool PatOutHospitalUpdateBedData(int wardID, int patListID);

        /// <summary>
        /// 病人定义出院，修改病人登记信息
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>true:修改成功</returns>
        bool PatOutHospitalUpdatePatListData(int patListID);

        /// <summary>
        /// 定义出院获取出院通知单基本数据
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>出院通知单基本数据</returns>
        DataTable GetPatOutHospitalData(int patListID);

        /// <summary>
        /// 取消分床检查病人是否已产生费用
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>true：已产生/false：未产生</returns>
        bool CheckPatIsCostIncurred(int patListID);

        /// <summary>
        /// 获取费用对象
        /// </summary>
        /// <param name="feeRecordID">费用ID</param>
        /// <returns>费用对象</returns>
        DataTable GetIPFeeItemRecordInfo(int feeRecordID);

        /// <summary>
        /// 按费用类型统计费用
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="costHeadID">结算ID</param>
        /// <returns>统计费用信息</returns>
        DataTable StatisticsFeeByFeeType(int patListID, int costHeadID);

        /// <summary>
        /// 获取病人最新预交金总额以及未结算费用总额
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人最新预交金总额以及未结算费用总额</returns>
        DataTable GetPatDepositFee(int patListID);

        /// <summary>
        /// 优惠计算--根据大项目ID分组查询病人费用数据
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人费用数据</returns>
        DataTable GetFeeItemRecordGroupByStatID(int patListID);

        /// <summary>
        /// 优惠计算--查询费用明细数据
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>费用明细数据</returns>
        DataTable GetFeeItemRecordDetails(int patListID);

        /// <summary>
        /// 获取计算病人上一次结算时间
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人上一次结算时间</returns>
        DataTable GetPatLastCostDate(int patListID);

        /// <summary>
        /// 保存住院结算明细费用汇总表数据
        /// </summary>
        /// <param name="costHeadID">结算主表ID</param>
        /// <param name="invoiceID">票据号ID</param>
        /// <param name="invoiceNO">票据号</param>
        /// <param name="workID">机构ID</param>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>true:保存成功</returns>
        bool SaveCostDetail(int costHeadID, int invoiceID, string invoiceNO, int workID, int patListID);

        /// <summary>
        /// 反写住院预交金表结算ID
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="costHeadID">结算头表ID</param>
        /// <param name="costType">结算类型</param>
        /// <param name="isCancel">是否是取消结算</param>
        /// <returns>true：写入成功</returns>
        bool CostDeposit(int patListID, int costHeadID, int costType, bool isCancel);

        /// <summary>
        /// 反写住院费用明细表结算ID
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="costHeadID">结算头表ID</param>
        /// <param name="costType">结算类型</param>
        /// <param name="isCancel">是否是取消结算</param>
        /// <returns>true:写入成功</returns>
        bool CostFeeItemRecord(int patListID, int costHeadID, int costType, bool isCancel);

        /// <summary>
        /// 出院结算、欠费结算修改病人状态
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="patStatus">病人状态</param>
        /// <returns>true：修改成功</returns>
        bool UpdatePatStatus(int patListID, int patStatus);

        /// <summary>
        /// 查询已结算列表
        /// </summary>
        /// <param name="costBeginDate">结算开始时间</param>
        /// <param name="costEndDate">结算结束时间</param>
        /// <param name="sqlectParam">检索条件</param>
        /// <param name="empId">结算人ID</param>
        /// <param name="status">结算状态</param>
        /// <param name="isAccount">是否缴款</param>
        /// <param name="costType">结算类型</param>
        /// <returns>已结算列表</returns>
        DataTable GetCostFeeList(DateTime costBeginDate, DateTime costEndDate, string sqlectParam, int empId, int status, int isAccount, string costType);

        /// <summary>
        /// 根据结算ID查询结算记录
        /// </summary>
        /// <param name="costHeadID">结算ID</param>
        /// <returns>结算记录</returns>
        DataTable GetCostHeadByHeadID(int costHeadID);

        /// <summary>
        /// 查询病人最后一次计算记录的结算ID
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>结算ID</returns>
        DataTable GetLastHoleCostHeadID(int patListID);

        /// <summary>
        /// 取消结算--支付方式记录表产生红冲记录
        /// </summary>
        /// <param name="oldCostHeadID">旧结算ID</param>
        /// <param name="newCostHeadID">新结算ID</param>
        void CancelCostUpdCostPayment(int oldCostHeadID, int newCostHeadID);

        /// <summary>
        /// 取消结算--住院结算明细费用汇总表产生红冲记录
        /// </summary>
        /// <param name="oldCostHeadID">旧结算ID</param>
        /// <param name="newCostHeadID">新结算ID</param>
        void CancelCostUpdCostDetail(int oldCostHeadID, int newCostHeadID);

        /// <summary>
        /// 住院押金查询
        /// </summary>
        /// <param name="costBeginDate">开始时间</param>
        /// <param name="costEndDate">结束时间</param>
        /// <param name="sqlectParam">检索条件</param>
        /// <param name="empId">结算人ID</param>
        /// <param name="status">结算状态</param>
        /// <param name="isAccount">是否缴款</param>
        /// <returns>住院押金列表</returns>
        DataTable GetAllDepositList(DateTime costBeginDate, DateTime costEndDate, string sqlectParam, int empId, int status, int isAccount);

        /// <summary>
        /// 发票补打获取发票显示数据
        /// </summary>
        /// <param name="costHeadID">结算ID</param>
        /// <returns>发票报表数据</returns>
        DataTable GetInvoiceFillData(int costHeadID);

        /// <summary>
        /// 检查当前病床是否为被包床床位
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        /// <returns>床位信息</returns>
        DataTable CheckBedIsPack(int patListId, int wardId, string bedNo);

        /// <summary>
        /// 取得病人关联的所有床位
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人关联床位列表</returns>
        DataTable GetPatientBedList(int patListID);

        /// <summary>
        /// 护士站获取病人列表
        /// </summary>
        /// <param name="status">病人状态</param>
        /// <param name="deptId">科室ID</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="isReminder">催款查询标志</param>
        /// <param name="reminderLine">催款线</param>
        /// <returns>病人列表</returns>
        DataTable GetNursingStationPatList(int status, int deptId, DateTime startDate, DateTime endDate, bool isReminder, decimal reminderLine);

        /// <summary>
        /// 根据会员ID查询是否当前会员是否已开住院证
        /// </summary>
        /// <param name="memberID">会员ID</param>
        /// <returns>住院证信息</returns>
        DataTable GetInpatientReg(int memberID);

        /// <summary>
        /// 住院登记成功后修改住院证信息
        /// </summary>
        /// <param name="memberID">会员ID</param>
        void UpdateInpatientReg(int memberID);

        /// <summary>
        /// 获取待打印催款单数据
        /// </summary>
        /// <param name="patListID">病人住院登记ID列表</param>
        /// <param name="reminderMoney">继续交款金额</param>
        /// <returns>待打印催款单数据列表</returns>
        DataTable GetReminderDataList(string patListID, decimal reminderMoney);

        /// <summary>
        /// 定义出院检查病人是否存在出院医嘱，并且已经转抄
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <returns>检查结果</returns>
        DataTable IsExistenceDischargeOrder(int patListId);

        /// <summary>
        /// 出院召回的病人修改出院医嘱状态
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        void UpdatePatOrder(int patListId);

        /// <summary>
        /// 获取所有转科病人
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <returns>所有转科病人</returns>
        DataTable GetTransferPatList(int wardId);

        /// <summary>
        /// 检查病人是否存在有效转科医嘱
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <returns>医嘱信息</returns>
        DataTable CheckPatTransDept(int patListId);

        /// <summary>
        /// 转科更改病人当前科室为新科室
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="currDeptId">新科室ID</param>
        void UpdatepatCurrDept(int patListID, int currDeptId);

        /// <summary>
        /// 获取记账开始时间以及记账天数
        /// </summary>
        /// <param name="generateIdList">费用生成ID列表</param>
        /// <param name="endTime">记账结束时间</param>
        /// <param name="isBedFee">true:床位费/false:账单费用</param>
        /// <param name="patListId">病人登记ID</param>
        /// <returns>记账信息</returns>
        DataTable GetAccountDate(string generateIdList, DateTime endTime, bool isBedFee, int patListId);

        /// <summary>
        /// 账单记账获取费用生成数据并且更新价格
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <param name="generateID">账单Id</param>
        /// <returns>最新费用数据</returns>
        DataTable GetFeeItemGenerateData(int patListId, int generateID);

        /// <summary>
        /// 临时账单记账后直接停用
        /// </summary>
        /// <param name="generateID">费用生成ID</param>
        void StopTempFeeItemGenerate(int generateID);

        /// <summary>
        /// 获取病床ID
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        /// <returns>病床信息</returns>
        DataTable GetBedInfoId(int wardId, string bedNo);

        /// <summary>
        /// 取消床位分配/转科/出院/取消包床停用床位账单数据
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <param name="bedId">床位ID</param>
        void StopBedFee(int patListId, int bedId);

        /// <summary>
        /// 插入每日现金流量账表
        /// </summary>
        /// <param name="type">0收入流水账1预交金流水账</param>
        /// <returns>true:插入成功</returns>
        bool InsertIPAccountBookData(int type);

        /// <summary>
        /// 根据会员ID获取会员信息
        /// </summary>
        /// <param name="memberID">会员ID</param>
        /// <returns>会员信息</returns>
        DataTable QueryMemberInfo(int memberID);
    }
}
