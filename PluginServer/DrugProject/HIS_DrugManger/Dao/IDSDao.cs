using System.Collections.Generic;
using System.Data;
using HIS_Entity.DrugManage;

namespace HIS_DrugManger.Dao
{
    /// <summary>
    /// 药房系统数据库访问
    /// </summary>
    public interface IDSDao
    {
        #region 出库
        /// <summary>
        /// 查新出库头表数据
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>出库头表数据</returns>
        DataTable LoadOutStoreHead(Dictionary<string, string> queryCondition);

        /// <summary>
        /// 出库明细单数据
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>获取出库明细数据集</returns>
        DataTable LoadOutStoreDetail(Dictionary<string, string> queryCondition);

        /// <summary>
        /// 获取科室的库存药品信息
        /// </summary>
        /// <param name="dept">科室ID</param>
        /// <returns>库位药品库存数据集</returns>
        DataTable GetStoreDrugInFo(int dept);
        #endregion

        #region 领药申请
        /// <summary>
        /// 查询领药申请头表数据
        /// </summary>
        /// <param name="queryConditon">查询条件</param>
        /// <returns>领药申请头表数据</returns>
        DataTable LoadApplyStoreHead(Dictionary<string, string> queryConditon);

        /// <summary>
        /// 查询领药申请明细数据
        /// </summary>
        /// <param name="queryConditon">查询条件</param>
        /// <returns>领药申请明细数据</returns>
        DataTable LoadApplyStoreDetail(Dictionary<string, string> queryConditon);
        #endregion

        #region 入库单
        /// <summary>
        /// 查询入库单表头
        /// </summary>
        /// <param name="queryConditon">查询条件</param>
        /// <returns>入库单表头</returns>
        DataTable LoadInStoreHead(Dictionary<string, string> queryConditon);
        
        /// <summary>
        /// 查询入库单明细
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>入库单明细</returns>
        DataTable LoadInStoreDetail(Dictionary<string, string> queryCondition);

        /// <summary>
        /// 加载药品批次信息【ShowCard用】
        /// </summary>
        /// <param name="deptID">科室编码</param>
        /// <returns>药品批次信息</returns>
        DataTable GetBatchForInstoreShowCard(int deptID);

        /// <summary>
        /// 加载入库药品【ShowCard用】
        /// </summary>
        /// <param name="isRet">是否退库</param>
        /// <param name="deptID">科室ID</param>
        /// <returns>药房入库药品</returns>
        DataTable GetDrugDicForInStoreShowCard(bool isRet, int deptID);
        #endregion

        #region 库存上下限设置
        /// <summary>
        /// 查询库存上下限数据
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>库存数据</returns>
        DataTable GetLoadStoreLimitData(Dictionary<string, string> queryCondition);

        /// <summary>
        /// 保存库存上下限
        /// </summary>
        /// <param name="details">库存数据列表</param>
        void SaveStoreLimit(List<DS_Storage> details);
        #endregion

        #region 库存查询
        /// <summary>
        /// 查询药品库存信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>库存信息表</returns>
        DataTable LoadDrugStorage(Dictionary<string, string> condition);

        /// <summary>
        /// 查询药品库存信息（带批次）
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>药品库存数据集</returns>
        DataTable LoadDrugStorages(Dictionary<string, string> condition);

        /// <summary>
        /// 查询拆零库存信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>库存信息表</returns>
        DataTable LoadResolve(Dictionary<string, string> condition);

        /// <summary>
        /// 查询药品批次信息
        /// </summary>
        /// <param name="storageID">库存ID</param>
        /// <returns>药品批次信息列表</returns>
        DataTable LoadDrugBatch(int storageID);

        /// <summary>
        /// 根据编码和批次获取批次信息
        /// </summary>
        /// <param name="batchNO">批次号</param>
        /// <param name="drugID">药品ID</param>
        /// <returns>获取药品批次对象集</returns>
        List<DS_Batch> GetBatchList(string batchNO, int drugID);

        /// <summary>
        /// 根据编码和批次获取批次药品类型
        /// </summary>
        /// <param name="batchNO">批次号</param>
        /// <param name="drugID">药品ID</param>
        /// <returns>获取药品类型</returns>
        int GetTypeId(string batchNO, int drugID);

        /// <summary>
        /// 修改库存禁用状态
        /// </summary>
        /// <param name="storageId">库存ID</param>
        /// <param name="delFlag">禁用标识</param>
        /// <returns></returns>
        bool UpdateStorageFlag(int storageId, int delFlag);

        /// <summary>
        /// 修改有效库存判断是否有未发药的,如果存在未发药的情况则不让修改
        /// </summary>
        /// <param name="deptId">当前执行科室</param>
        /// <param name="DrugID">厂家ID</param>
        /// <returns>返回是否存在数据</returns>
        bool UpdateValidateStorage(int deptId, int DrugID);
        #endregion

        #region 库房盘点
        /// <summary>
        /// 获取库房盘点状态
        /// </summary>
        /// <param name="deptId">库房Id</param>
        /// <returns>盘点状态</returns>
        int GetStoreRoomStatus(int deptId);

        /// <summary>
        /// 查询盘点单表头
        /// </summary>
        /// <param name="queryConditon">查询条件</param>
        /// <returns>盘点单表头</returns>
        DataTable LoadCheckHead(Dictionary<string, string> queryConditon);

        /// <summary>
        /// 查询盘点单明细
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>盘点单明细</returns>
        DataTable LoadCheckDetail(Dictionary<string, string> queryCondition);

        /// <summary>
        /// 取得药库盘点药品选择卡片数据
        /// </summary>
        /// <param name="deptID">药库ID</param>
        /// <returns>药品字典</returns>
        DataTable GetDrugDicForCheckShowCard(int deptID);

        /// <summary>
        /// 提取库存药品
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>盘点单空表数据</returns>
        DataTable LoadStorageData(Dictionary<string, string> condition);

        /// <summary>
        /// 加载盘点审核单表头
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>盘点审核单表头</returns>
        DataTable LoadAudtiCheckHead(Dictionary<string, string> condition);

        /// <summary>
        /// 加载盘点审核单明细
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>盘点审核单明细</returns>
        DataTable LoadAuditCheckDetail(Dictionary<string, string> condition);

        /// <summary>
        /// 汇总所有未审核单据明细并提取
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="haveBatchNO">是否按批次汇总</param>
        /// <returns>汇总盘点信息</returns>
        DataTable LoadAllNotAuditDetail(Dictionary<string, string> condition, bool haveBatchNO);

        /// <summary>
        /// 计算批次账存金额
        /// </summary>
        /// <param name="storageId">库存Id</param>
        /// <returns>返回零售金额，进货金额数组</returns>
        decimal[] CalculateBatchActFee(int storageId);

        /// <summary>
        /// 取得库存批次数据
        /// </summary>
        /// <param name="storageId">库存Id</param>
        /// <returns>库存批次数据</returns>
        DataTable GetStorageBatch(int storageId);

        /// <summary>
        /// 更新盘点头表审核状态信息
        /// </summary>
        /// <param name="head">盘点头表</param>
        /// <returns>小于0失败</returns>
        int UpdateCheckHeadStatus(DS_CheckHead head);

        /// <summary>
        /// 删除所有未审核盘点单
        /// </summary>
        /// <param name="deptID">库房Id</param>
        /// <returns>返回结果</returns>
        int DeleteAllNotAuditCheckHead(int deptID);
        #endregion

        #region 药房月结
        /// <summary>
        /// 月结对账
        /// </summary>
        /// <param name="workId">机构ID</param>
        /// <param name="deptId">科室ID</param>
        /// <returns>返回月结对账结果</returns>
        DgSpResult ExcutSystemCheckAccount(int workId, int deptId);

        /// <summary>
        /// 月结操作
        /// </summary>
        /// <param name="workId">机构ID</param>
        /// <param name="deptId">科室ID</param>
        /// <param name="empId">操作人ID</param>
        /// <returns>返回月结对账结果</returns>
        DGBillResult ExcutMonthBalance(int workId, int deptId, int empId);

        /// <summary>
        /// 药房科室的月结记录
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <returns>月结数据集</returns>
        DataTable GetMonthBalaceByDept(int deptId);
        #endregion

        #region 出入库转换
        /// <summary>
        /// 获取批次库存量
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="drugId">药品ID</param>
        /// <param name="batchNo">批次号</param>
        /// <returns>批次信息</returns>
        DS_Batch GetBatchAmount(int deptId, int drugId, string batchNo);

        /// <summary>
        /// 增加批次数量
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <param name="batchNO">批次号</param>
        /// <param name="amount">数量</param>
        /// <returns>返回结构</returns>
        bool AddBatchAmount(int deptID, int drugID, string batchNO, decimal amount);

        /// <summary>
        /// 出库单明细生成入库单明细
        /// </summary>
        /// <param name="outHeadId">出库单表头</param>
        /// <returns>返回出库明细数据集</returns>
        List<DS_InStoreDetail> GetInStoreFromOutStore(int outHeadId);

        /// <summary>
        /// 出库单明细生成药房出库单明细
        /// </summary>
        /// <param name="outHeadId">出库单表头</param>
        /// <returns>返回出库明细数据集</returns>
        List<DS_OutStoreDetail> GetOutStoreFromOutStore(int outHeadId);
        #endregion

        #region 药房处理
        /// <summary>
        /// 获取药品库存数量
        /// </summary>
        /// <param name="drugID">药品ID</param>
        /// <param name="deptID">库房ID</param>
        /// <param name="batchNO">批次号</param>
        /// <returns>库存数量</returns>
        decimal GetDrugStoreAmount(int drugID, int deptID, string batchNO);

        /// <summary>
        /// 清空库位
        /// </summary>
        /// <param name="locationID">库位ID</param>
        void ClearLoaction(int locationID);

        /// <summary>
        /// 判断是否存在库存药品
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <param name="storageID">库存ID</param>
        /// <returns>是否存在</returns>
        bool ExistStorage(int deptID, int drugID, out int storageID);

        /// <summary>
        /// 增加药库库存
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <param name="amount">数量</param>
        /// <returns>成功失败</returns>
        bool AddStoreAmount(int deptID, int drugID, decimal amount);

        /// <summary>
        /// 获取批次库存信息
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <returns>返回批次对象集</returns>
        List<DS_Batch> GetBatchInfos(int deptID, int drugID);

        /// <summary>
        /// 获取最近入库批次库存信息
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <returns>批次对象</returns>
        DS_Batch GetLatelyBatchInfo(int deptID, int drugID);

        /// <summary>
        /// 获取当前库存数量
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <returns>药品库存数量</returns>
        decimal? GetStoreAmount(int deptID, int drugID);

        /// <summary>
        /// 获取库存信息
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <returns>库存对象</returns>
        DS_Storage GetStorageInfo(int deptID, int drugID);

        /// <summary>
        /// 有效库存数量
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="drugId">药品ID</param>
        /// <returns>有效库存对象</returns>
        DS_ValidStorage GetValidStoreageInfo(int deptId, int drugId);

        /// <summary>
        /// 更新批次数量
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <param name="batchNO">批次号</param>
        /// <param name="amount">数量</param>
        /// <param name="delFlag">删除标志，当库存为0时候为1否则为0</param>
        /// <param name="isValidity">有效标志</param>
        /// <returns>返回状态</returns>
        bool UpdateBatchAmount(int deptID, int drugID, string batchNO, decimal amount, int delFlag, int isValidity);

        /// <summary>
        /// 更新退药明细表状态
        /// </summary>
        /// <param name="reDetailId">ID</param>
        /// <returns>结果</returns>
        bool UpdateFeeRefundStatus(int reDetailId);

        /// <summary>
        /// 更新有效库存
        /// </summary>
        /// <param name="drugID">药品ID</param>
        /// <param name="deptID">库房ID</param>
        /// <param name="reduceAmount">数量</param>
        /// <returns>库存处理结果</returns>
        int UpdateValidStore(int drugID, int deptID, decimal reduceAmount);
        #endregion

        #region 台账处理
        /// <summary>
        /// 获取最后一次结账记录
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>最后一次结账记录</returns>
        DS_BalanceRecord GetMaxBlanceRecord(int deptID);
        #endregion

        #region 住院发药
        /// <summary>
        /// 获取药品统领单类型
        /// </summary>
        /// <returns>返回统领单数据集</returns>
        DataTable GetIPDrugBillType();

        /// <summary>
        /// 获取住院发药统计
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>住院发药统计数据集</returns>
        DataTable GetIPDispTotal(Dictionary<string, string> condition);

        /// <summary>
        /// 获取门诊发药统计
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>门诊发药统计数据集</returns>
        DataTable GetOPDispTotal(Dictionary<string, string> condition);

        /// <summary>
        /// 获取住院发药统计详细
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <returns>住院发药统计详细数据集</returns>
        DataTable GetIPDispDetatil(string deptId, string drugID);

        /// <summary>
        /// 获取门诊发药统计详细
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <returns>门诊发药统计详细数据集</returns>
        DataTable GetOPDispDetatil(string deptId, string drugID);
        #endregion

        #region 报表统计
        #region 进销存统计报表
        /// <summary>
        /// 获取会计年
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>会计年数据集</returns>
        DataTable GetAcountYears(int deptId);

        /// <summary>
        /// 获取会计月
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="year">年份</param>
        /// <returns>会计月数据集</returns>
        DataTable GetAcountMonths(int deptId,int year);

        /// <summary>
        /// 取得明细账数据
        /// </summary>
        /// <param name="accountType">账本类型</param>
        /// <param name="deptId">科室id</param>
        /// <param name="queryYear">年份</param>
        /// <param name="queryMonth">月份</param>
        /// <param name="typeId">药品类型</param>
        /// <returns>明细账数据集</returns>
        DataTable GetAccountData(int accountType,int deptId, int queryYear, int queryMonth, int typeId);

        /// <summary>
        /// 取得明细账数据
        /// </summary>
        /// <param name="accountType">账本类型</param>
        /// <param name="deptId">科室id</param>
        /// <param name="queryYear">年份</param>
        /// <param name="queryMonth">月份</param>
        /// <param name="typeId">药品类型</param>
        /// <param name="busiCode">业务代码</param>
        /// <returns>明细账数据集</returns>
        DataTable GetAccountData(int accountType, int deptId, int queryYear, int queryMonth, int typeId,string busiCode);

        /// <summary>
        /// 取得明细账数据
        /// </summary>
        /// <param name="accountType">账本类型</param>
        /// <param name="deptId">科室id</param>
        /// <param name="queryYear">年份</param>
        /// <param name="queryMonth">月份</param>
        /// <param name="typeId">药品类型</param>
        /// <param name="busiCode">业务代码</param>
        /// <param name="busiTypeModel">业务类型实体</param>
        /// <returns>明细账数据集</returns>
        DataTable GetAccountData(int accountType, int deptId, int queryYear, int queryMonth, int typeId, string busiCode, DGBusiType busiTypeModel);
        #endregion

        #region 药品汇总表
        /// <summary>
        /// 获取入库业务信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>入库业务信息</returns>
        DataTable GetInStore(Dictionary<string, string> query);

        /// <summary>
        /// 获取出库业务信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>出库业务信息</returns>
        DataTable GetOutStore(Dictionary<string, string> query);

        /// <summary>
        /// 获取盘点信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>盘点数据集</returns>
        DataTable GetCheck(Dictionary<string, string> query);

        /// <summary>
        /// 获取调价信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>调价数据集</returns>
        DataTable GetAdjPrice(Dictionary<string, string> query);

        /// <summary>
        /// 获取住院发药信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>住院发药数据集</returns>
        DataTable GetIPDisp(Dictionary<string, string> query);

        /// <summary>
        /// 获取门诊发药信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>门诊发药数据集</returns>
        DataTable GetOPDisp(Dictionary<string, string> query);
        #endregion

        #region 药品明细查询
        /// <summary>
        /// 获取会计月日期
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>会计月日期</returns>
        DataTable GetBalanceDate(int deptId, int year, int month);

        /// <summary>
        /// 取得药品明细账数据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="beginTime">开始日期</param>
        /// <param name="endTime">结束日期</param>
        /// <param name="drugId">药品Id</param>
        /// <param name="queryType">查询类型1自然月2会计月</param>
        /// <returns>药品明细账数据</returns>
        DataTable GetAccountDetail(int deptId, int year, int month, string beginTime, string endTime, int drugId, int queryType);

        /// <summary>
        /// 取得药品明细账汇总数据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="beginTime">开始日期</param>
        /// <param name="endTime">结束日期</param>
        /// <param name="drugId">药品Id</param>
        /// <param name="queryType">查询类型1自然月2会计月</param>
        /// <returns>药品明细账汇总数据</returns>
        DataTable GetAccountDetailSum(int deptId, int year, int month, string beginTime, string endTime, int drugId, int queryType);
        #endregion

        #region 药品流水账
        /// <summary>
        /// 获取入库业务信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>入库数据集</returns>
        DataTable GetInStores(Dictionary<string, string> query);

        /// <summary>
        /// 获取出库业务信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>出库数据集</returns>
        DataTable GetOutStores(Dictionary<string, string> query);

        /// <summary>
        /// 获取盘点信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>盘点数据集</returns>
        DataTable GetChecks(Dictionary<string, string> query);

        /// <summary>
        /// 获取调价信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>调价数据集</returns>
        DataTable GetAdjPrices(Dictionary<string, string> query);

        /// <summary>
        /// 获取住院发药信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>住院发药数据集</returns>
        DataTable GetIPDisps(Dictionary<string, string> query);

        /// <summary>
        /// 获取门诊发药信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>门诊发药数据集</returns>
        DataTable GetOPDisps(Dictionary<string, string> query);
        #endregion
        #endregion
    }
}
