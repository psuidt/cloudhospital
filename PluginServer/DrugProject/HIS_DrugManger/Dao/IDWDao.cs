using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManger.Dao
{
    /// <summary>
    /// 药库系统数据库访问
    /// </summary>
    public interface IDWDao
    {
        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="dic">查询条件</param>
        /// <param name="tableEntity">泛型对象</param>
        /// <returns>数据集</returns>
        DataTable GeTable<T>(Dictionary<string, string> dic, T tableEntity);

        #region 药品入库
        /// <summary>
        /// 查询入库单表头
        /// </summary>
        /// <param name="queryConditon">查询条件</param>
        /// <returns>入库单表头</returns>
        DataTable LoadInStoreHead(Dictionary<string, string> queryConditon);

        /// <summary>
        /// 查询付款记录表
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>付款记录数据集</returns>
        DataTable LoadPayRecord(Dictionary<string, string> queryCondition);

        /// <summary>
        /// 打印付款记录
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>打印付款记录集</returns>
        DataTable PrintPayRecord(Dictionary<string, string> queryCondition);

        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="inHeadID">付款ID</param>
        /// <param name="invoiceNO">发票号</param>
        /// <param name="payTime">付款时间</param>
        /// <param name="payRecordID">付款记录ID</param>
        /// <param name="type">类型</param>
        /// <returns>返回结果</returns>
        int UpdateStoreHead(string inHeadID, string invoiceNO, DateTime payTime, int payRecordID, int type);

        /// <summary>
        /// 取消付款
        /// </summary>
        /// <param name="payRecordID">付款记录ID</param>
        /// <returns>返回结果</returns>
        int UpdatePayRecord(string payRecordID);

        /// <summary>
        /// 查询入库单明细
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>入库单明细</returns>
        DataTable LoadInStoreDetail(Dictionary<string, string> queryCondition);

        /// <summary>
        /// 加载药库入库药品【ShowCard用】
        /// </summary>
        /// <param name="isRet">是否退库</param>
        /// <param name="deptID">科室ID</param>
        /// <returns>药库入库药品</returns>
        DataTable GetDrugDicForInStoreShowCard(bool isRet, int deptID);

        /// <summary>
        /// 加载药品批次信息【ShowCard用】
        /// </summary>
        /// <param name="deptID">科室编码</param>
        /// <returns>药品批次信息</returns>
        DataTable GetBatchForInstoreShowCard(int deptID);
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
        void SaveStoreLimit(List<DW_Storage> details);
        #endregion

        #region 库存处理
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
        /// 增加批次数量
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <param name="batchNO">批次号</param>
        /// <param name="amount">数量</param>
        /// <returns>批次数量</returns>
        bool AddBatchAmount(int deptID, int drugID, string batchNO, decimal amount);

        /// <summary>
        /// 获取批次库存量
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <param name="batchNO">批次号</param>
        /// <returns>批次信息</returns>
        DW_Batch GetBatchAmount(int deptID, int drugID, string batchNO);

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
        /// <returns>库存信息</returns>
        DW_Storage GetStorageInfo(int deptID, int drugID);
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
        /// <returns>药品库存信息（带批次）</returns>
        DataTable LoadDrugStorages(Dictionary<string, string> condition);

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
        /// <returns>获取批次对象集</returns>
        List<DW_Batch> GetBatchList(string batchNO, int drugID);

        /// <summary>
        /// 根据编码和批次获取批次药品类型
        /// </summary>
        /// <param name="batchNO">批次号</param>
        /// <param name="drugID">药品ID</param>
        /// <returns>获取批次药品ID</returns>
        int GetTypeId(string batchNO, int drugID);
        #endregion

        #region 台账处理
        /// <summary>
        /// 获取最后一次结账记录
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>最后一次结账记录</returns>
        DW_BalanceRecord GetMaxBlanceRecord(int deptID);
        #endregion

        #region 库房盘点
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
        /// 获取库房盘点状态
        /// </summary>
        /// <param name="deptId">库房Id</param>
        /// <returns>盘点状态</returns>
        int GetStoreRoomStatus(int deptId);

        /// <summary>
        /// 取得所有未审核的盘点汇总明细
        /// </summary>
        /// <param name="deptId">库房Id</param>
        /// <returns>未审核盘点汇总明细</returns>
        DataTable GetAllNotAuditDetail(int deptId);

        /// <summary>
        /// 更新盘点头表审核状态信息
        /// </summary>
        /// <param name="head">盘点头表</param>
        /// <returns>小于0失败</returns>
        int UpdateCheckHeadStatus(DW_CheckHead head);

        /// <summary>
        /// 删除所有未审核盘点单
        /// </summary>
        /// <param name="deptID">库房Id</param>
        /// <returns>返回结果</returns>
        int DeleteAllNotAuditCheckHead(int deptID);
        #endregion

        #region 入库报表
        /// <summary>
        /// 入库报表
        /// </summary>
        /// <param name="andWhere">查询条件</param>
        /// <returns>返回入库数据集</returns>
        DataTable GetInstoreReport(List<Tuple<string, string, SqlOperator>> andWhere = null);
        #endregion

        #region 药品出库
        /// <summary>
        /// 获取科室的库存药品信息
        /// </summary>
        /// <param name="dept">科室ID</param>
        /// <returns>获取库存数据集</returns>
        DataTable GetStoreDrugInFo(int dept);

        /// <summary>
        /// 获取科室 可以出库的药品
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <returns>获取出库数据集</returns>
        DataTable GetDeptOutDrug(int deptId);

        /// <summary>
        /// 查询入库单明细
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>入库单明细</returns>
        DataTable LoadOutStoreDetail(Dictionary<string, string> queryCondition);

        /// <summary>
        /// 查询入库单头表
        /// </summary>
        /// <param name="andWhere">查询条件</param>
        /// <returns>入库单头表</returns>
        DataTable LoadOutStoreHead(List<Tuple<string, string, SqlOperator>> andWhere = null);

        /// <summary>
        /// 查询入库单头表
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>入库单头表</returns>
        DataTable LoadOutStoreHead(Dictionary<string, string> queryCondition);
        #endregion

        #region 出入库转换
        /// <summary>
        /// 药库入库单明细转出库单明细
        /// </summary>
        /// <param name="andWhere">and条件</param>
        /// <param name="orWhere">or条件</param>
        /// <returns>入库单明细转出库单明细</returns>
        DataTable GetOutStoreFromInStore(List<Tuple<string, string, SqlOperator>> andWhere = null, List<Tuple<string, string, SqlOperator>> orWhere = null);
        #endregion

        /// <summary>
        /// 药库入库单明细转出库单申请
        /// </summary>
        /// <param name="andWhere">and条件</param>
        /// <param name="orWhere">or条件</param>
        /// <returns>入库单明细转出库单申请</returns>
        DataTable GetOutFromApply(List<Tuple<string, string, SqlOperator>> andWhere = null, List<Tuple<string, string, SqlOperator>> orWhere = null);

        #region 月结
        /// <summary>
        /// 查询科室月结记录
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <returns>科室月结记录</returns>
        DataTable GetMonthBalaceByDept(int deptId);

        /// <summary>
        /// 执行月结
        /// </summary>
        /// <param name="workId">机构ID</param>
        /// <param name="deptId">部门ID</param>
        /// <param name="empId">操作人ID</param>
        /// <returns>月结结果</returns>
        DGBillResult ExcutMonthBalance(int workId, int deptId, int empId);

        /// <summary>
        /// 执行月结
        /// </summary>
        /// <param name="workId">机构ID</param>
        /// <param name="deptId">部门ID</param>
        /// <returns>月结结果</returns>
        DgSpResult ExcutSystemCheckAccount(int workId, int deptId);
        #endregion

        #region 报表统计
        #region 进销存统计报表
        /// <summary>
        /// 获取会计年
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>会计年</returns>
        DataTable GetAcountYears(int deptId);

        /// <summary>
        /// 获取会计月
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="year">年份</param>
        /// <returns>会计月</returns>
        DataTable GetAcountMonths(int deptId, int year);

        /// <summary>
        /// 取得明细账数据
        /// </summary>
        /// <param name="accountType">账本类型</param>
        /// <param name="deptId">科室id</param>
        /// <param name="queryYear">年份</param>
        /// <param name="queryMonth">月份</param>
        /// <param name="typeId">药品类型</param>
        /// <returns>明细账数据</returns>
        DataTable GetAccountData(int accountType, int deptId, int queryYear, int queryMonth, int typeId);

        /// <summary>
        /// 取得明细账数据
        /// </summary>
        /// <param name="accountType">账本类型</param>
        /// <param name="deptId">科室id</param>
        /// <param name="queryYear">年份</param>
        /// <param name="queryMonth">月份</param>
        /// <param name="typeId">药品类型</param>
        /// <param name="busiCode">业务代码</param>
        /// <returns>明细账数据</returns>
        DataTable GetAccountData(int accountType, int deptId, int queryYear, int queryMonth, int typeId, string busiCode);

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
        /// <returns>明细账数据</returns>
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
        /// <returns>盘点信息</returns>
        DataTable GetCheck(Dictionary<string, string> query);

        /// <summary>
        /// 获取调价信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>调价信息</returns>
        DataTable GetAdjPrice(Dictionary<string, string> query);
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
        /// <returns>入库业务信息</returns>
        DataTable GetInStores(Dictionary<string, string> query);

        /// <summary>
        /// 获取出库业务信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>出库业务信息</returns>
        DataTable GetOutStores(Dictionary<string, string> query);

        /// <summary>
        /// 获取盘点信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>盘点信息</returns>
        DataTable GetChecks(Dictionary<string, string> query);

        /// <summary>
        /// 获取调价信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>调价信息</returns>
        DataTable GetAdjPrices(Dictionary<string, string> query);
        #endregion

        #region 采购入库对比
        /// <summary>
        /// 采购入库对比
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <param name="yearMonth">年月</param>
        /// <param name="drugName">药品名称</param>
        /// <returns>采购入库数据集</returns>
        DataTable GetBuyComparison(int deptId, string yearMonth, string drugName);
        #endregion

        #region 新药入库统计
        /// <summary>
        /// 新药入库统计
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <param name="yearMonth">年月</param>
        /// <param name="drugName">药品名称</param>
        /// <returns>新药入库数据集</returns>
        DataTable GetNewDrug(int deptId, string yearMonth, string drugName);
        #endregion
        #endregion
    }
}
