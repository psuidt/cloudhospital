using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.DbProvider.SqlPagination;
using HIS_Entity.BasicData;
using HIS_Entity.DrugManage;
using HIS_Entity.MaterialManage;
using HIS_Entity.SqlAly;

namespace HIS_MaterialManager.Dao
{
    /// <summary>
    /// 物资数据库访问接口
    /// </summary>
    public interface IMWDao
    {
        #region 公共获取数据方法
        /// <summary>
        /// 单表查询
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="andWhere">and 查询条件</param>
        /// <param name="orWhere">or 查询条件</param>
        /// <returns>查询数据结果</returns>
        DataTable GetDataTable<T>(List<Tuple<string, string, SqlOperator>> andWhere, List<Tuple<string, string, SqlOperator>> orWhere) where T : AbstractEntity;

        /// <summary>
        /// 分页的单表查询
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="andWhere">于条件</param>
        /// <param name="orWhere">或条件</param>
        /// <param name="page">分页方法</param>
        /// <returns>查询数据结果</returns>
        DataTable GetDataTable<T>(List<Tuple<string, string, SqlOperator>> andWhere, List<Tuple<string, string, SqlOperator>> orWhere, PageInfo page) where T : AbstractEntity;

        /// <summary>
        /// 获取实体类型
        /// </summary>
        /// <typeparam name="T">泛型类型T</typeparam>
        /// <param name="andWhere">and 查询条件</param>
        /// <param name="orWhere">or 查询条件</param>
        /// <returns>查询数据结果</returns>
        IEnumerable<T> GetEntityType<T>(List<Tuple<string, string, SqlOperator>> andWhere = null, List<Tuple<string, string, SqlOperator>> orWhere = null) where T : AbstractEntity;

        #endregion

        #region 物资供应商
        /// <summary>
        /// 加载供应商【ShowCard专用】
        /// </summary>
        /// <returns>供应商列表</returns>
        DataTable GetSupplyForShowCard();
        #endregion

        #region 物资类型
        /// <summary>
        /// 获取物资子类型
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>物资子类型数据</returns>
        DataTable GetChildMaterialType(Dictionary<string, string> queryCondition);

        /// <summary>
        /// 获取物资子类型ShowCard数据
        /// </summary>
        /// <returns>物资子类型ShowCard数据</returns>
        DataTable GetMaterialTypeShowCard();
        #endregion

        #region 物资字典
        /// <summary>
        /// 获取物资字典
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns>物资字典数据</returns>
        DataTable GetMaterialDic(string strSql);

        /// <summary>
        /// 审核物资字典
        /// </summary>
        /// <param name="meterialID">物资id</param>
        /// <returns>1成功</returns>
        int AuditDic(int meterialID);

        /// <summary>
        /// 根据物资字典ID获取厂家信息
        /// </summary>
        /// <param name="centerMatID">中心物资id</param>
        /// <returns>物资厂家典数据</returns>
        DataTable LoadHisDic(int centerMatID);
        #endregion

        #region 物资库房设置
        /// <summary>
        /// 是否存在物资库房
        /// </summary>
        /// <param name="deptId">库房id</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool ExistMaterialDept(int deptId);

        /// <summary>
        /// 取得物资科室数据
        /// </summary>
        /// <returns>物资科室数据集</returns>
        DataTable GetDeptDicData();

        /// <summary>
        /// 按类型获取物资科室列表
        /// </summary>
        /// <param name="deptType">类型ID</param>
        /// <returns>物资科室列表</returns>
        DataTable GetDrugDeptList(int deptType);

        /// <summary>
        /// 初始化使用单据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="deptType">科室类型</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool InitSerialNumber(int deptId, int deptType);

        /// <summary>
        /// 取得物资科室单据数据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>物资科室数据集</returns>
        DataTable GetDrugDeptBill(int deptId);

        /// <summary>
        /// 修改启用标志
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>小于0失败</returns>
        int UpdateStartStatus(int deptId);

        /// <summary>
        /// 停用物资科室
        /// </summary>
        /// <param name="deptDicID">物资科室id</param>
        /// <param name="deptId">科室id</param>
        /// <returns>小于0失败</returns>
        int StopUseDrugDept(int deptDicID, int deptId);

        /// <summary>
        /// 设置物资科室盘点状态
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="status">状态1开启盘点，0未开启</param>
        void SetCheckStatus(int deptId, int status);

        /// <summary>
        /// 取得物资类型典
        /// </summary>
        /// <returns>药品类型字典数据集</returns>
        DataTable GetMWTypeDic();

        /// <summary>
        /// 取得物资科室
        /// </summary>
        /// <returns>物资科室数据</returns>
        DataTable GetMaterialDept();
        #endregion

        #region 物资参数设置
        /// <summary>
        /// 取得启用的科室
        /// </summary>
        /// <returns>启用科室数据集</returns>
        DataTable GetUsedDeptData();

        /// <summary>
        /// 获取公共参数
        /// </summary>
        /// <returns>公共参数集</returns>
        DataTable GetPublicParameters();

        /// <summary>
        /// 获取科室参数
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>科室参数集</returns>
        DataTable GetDeptParameters(int deptId);

        /// <summary>
        /// 获取科室参数
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <param name="paraId">参数id</param>
        /// <returns>参数实体</returns>
        Basic_SystemConfig GetDeptParameters(int deptId, string paraId);

        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="parameterList">参数列表</param>
        /// <returns>小于0失败</returns>
        int SaveParameters(List<Basic_SystemConfig> parameterList);

        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>小于0失败</returns>
        int SaveParameters(int deptId);
        #endregion

        #region 往来科室设置
        /// <summary>
        ///  获取往来科室信息
        /// </summary>
        /// <param name="andWhere">and 查询条件</param>
        /// <param name="orWhere">or 查询条件</param>
        /// <returns>相关科室数据集</returns>
        DataTable GetRelateDeptData(List<Tuple<string, string, SqlOperator>> andWhere, List<Tuple<string, string, SqlOperator>> orWhere);

        /// <summary>
        /// 取得往来科室数据
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <returns>相关科室数据集</returns>
        DataTable GetRelateDeptData(int deptId);

        /// <summary>
        /// 批量保存往来科室
        /// </summary>
        /// <param name="dtSave">往来科室数据集</param>
        /// <param name="empId">操作员Id</param>
        /// <returns>小于0失败</returns>
        int BatchSaveRelateDept(DataTable dtSave, int empId);

        /// <summary>
        /// 删除往来科室
        /// </summary>
        /// <param name="drugDeptID">药剂科室Id</param>
        /// <param name="relationDeptID">往来科室Id</param>
        /// <returns>小于0失败</returns>
        int DeleteRelateDept(int drugDeptID, int relationDeptID);
        #endregion

        #region 验收付款
        /// <summary>
        /// 取消付款,并删除付款记录
        /// </summary>
        /// <param name="payRecordID">付款记录id</param>
        /// <returns>1成功</returns>
        int UpdatePayRecord(string payRecordID);

        /// <summary>
        /// 查询付款记录
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>付款记录</returns>
        DataTable LoadPayRecord(Dictionary<string, string> queryCondition);

        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="inHeadID">入库单头id</param>
        /// <param name="invoiceNO">发票号</param>
        /// <param name="payTime">付款时间</param>
        /// <param name="payRecordID">付款记录id</param>
        /// <param name="type">付款标识0未付款1已付款</param>
        /// <returns>1成功</returns>
        int UpdateStoreHead(string inHeadID, string invoiceNO, DateTime payTime, int payRecordID, int type);

        /// <summary>
        /// 查询入库表头
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>入库单明细</returns>
        DataTable LoadInStoreHead(Dictionary<string, string> queryCondition);

        /// <summary>
        /// 查询入库单明细
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>入库单明细</returns>
        DataTable LoadInStoreDetail(Dictionary<string, string> queryCondition);
        #endregion

        #region 出入库物资
        /// <summary>
        /// 查询入库物资ShowCard
        /// </summary>
        /// <param name="isRet">是否退库</param>
        /// <param name="deptID">科室ID</param>
        /// <returns>物资ShowCard数据源</returns>
        DataTable GetDrugDicForInStoreShowCard(bool isRet, int deptID);

        /// <summary>
        /// 查询药品入库批次ShowCard
        /// </summary>
        /// <param name="deptID">科室id</param>
        /// <returns>批次ShowCard数据源</returns>
        DataTable GetBatchForInstoreShowCard(int deptID);

        /// <summary>
        /// 出库物资
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>出库单数据</returns>
        DataTable LoadOutStoreHead(Dictionary<string, string> queryCondition);

        /// <summary>
        /// 出库物资明细
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>出库明细数据</returns>
        DataTable LoadOutStoreDetail(Dictionary<string, string> queryCondition);

        /// <summary>
        /// 科室的库存物资 showcard
        /// </summary>
        /// <param name="dept">科室id</param>
        /// <returns>科室的库存物资数据</returns>
        DataTable GetStoreMWInFo(int dept);
        #endregion

        #region 库存处理
        /// <summary>
        /// 月结记录
        /// </summary>
        /// <param name="deptID">科室id</param>
        /// <returns>月结实体对象</returns>
        MW_BalanceRecord GetMaxBlanceRecord(int deptID);

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
        /// <returns>true成功</returns>
        bool AddBatchAmount(int deptID, int drugID, string batchNO, decimal amount);

        /// <summary>
        /// 获取批次库存量
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <param name="batchNO">批次号</param>
        /// <returns>批次信息</returns>
        MW_Batch GetBatchAmount(int deptID, int drugID, string batchNO);

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
        /// <param name="deptID">科室id</param>
        /// <param name="drugID">药品id</param>
        /// <returns>库存实体对象</returns>
        MW_Storage GetStorageInfo(int deptID, int drugID);
        #endregion

        #region 库存查询
        /// <summary>
        /// 查询库存信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="typeId">类型id</param>
        /// <returns>库存信息表</returns>
        DataTable LoadMaterialStorage(Dictionary<string, string> condition,string typeId);

        /// <summary>
        /// 查询库存信息（带批次）
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="typeId">物资类型id</param>
        /// <returns>库存信息（带批次）</returns>
        DataTable LoadMaterialStorages(Dictionary<string, string> condition, string typeId);

        /// <summary>
        /// 查询物资批次信息
        /// </summary>
        /// <param name="storageID">库存id</param>
        /// <returns>物资批次信息</returns>
        DataTable LoadMaterialBatch(int storageID);
        #endregion

        #region 库位管理
        /// <summary>
        /// 获取库位详细信息
        /// </summary>
        /// <param name="locationid">库位id</param>
        /// <returns>库位详细信息</returns>
        MW_Location GetLocationInfo(int locationid);

        /// <summary>
        /// 根据父类和库位名称获取库位
        /// </summary>
        /// <param name="parentid">父id</param>
        /// <param name="locationname">库位名称</param>
        /// <returns>库位实体对象</returns>
        MW_Location GetLocationByName(int parentid, string locationname);

        /// <summary>
        /// 获取库位节点信息
        /// </summary>
        /// <param name="deptid">科室id</param>
        /// <returns>库位节点信息</returns>
        List<MW_Location> GetLocationList(int deptid);

        /// <summary>
        /// 归库操作
        /// </summary>
        /// <param name="locationid">库位id</param>
        /// <param name="ids">库存id字符串</param>
        /// <param name="frmName">窗体入口</param>
        /// <returns>1成功</returns>
        int UpdateStorage(int locationid, string ids, string frmName);
        #endregion

        #region 库存上下限设置
        /// <summary>
        /// 查询库存上下限数据
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>库存数据</returns>
        DataTable GetStoreLimitData(Dictionary<string, string> queryCondition);

        /// <summary>
        /// 保存库存上下限
        /// </summary>
        /// <param name="details">库存数据列表</param>
        void SaveStoreLimit(List<MW_Storage> details);

        /// <summary>
        /// 查询物资ShowCard
        /// </summary>
        /// <param name="isRet">是否退库</param>
        /// <param name="deptID">科室ID</param>
        /// <returns>物资ShowCard数据源</returns>
        DataTable GetMaterialDicShowCard(bool isRet, int deptID);
        #endregion

        #region 采购计划
        /// <summary>
        /// 查询采购计划头表
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>采购计划单</returns>
        DataTable GetPlanHeadData(Dictionary<string, string> queryCondition);

        /// <summary>
        /// 查询采购计划单据明细
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>采购计划明细单</returns>
        DataTable GetPlanDetailData(Dictionary<string, string> queryCondition);

        /// <summary>
        /// 删除采购计划单 
        /// </summary>
        /// <param name="billID">采购计划单表头Id</param>
        /// <returns>1成功</returns>
        bool DeleteMWPlanBill(int billID);

        /// <summary>
        /// 取得小于下限的库存药品
        /// </summary>
        /// <param name="deptId">药库ID</param>
        /// <returns>药品数据</returns>
        DataTable GetLessLowerLimitData(int deptId);

        /// <summary>
        /// 取得小于上限的库存药品
        /// </summary>
        /// <param name="deptId">药库ID</param>
        /// <returns>药品数据</returns>
        DataTable GetLessUpperLimitData(int deptId);
        #endregion

        #region 报表统计
        #region 进销存统计报表
        /// <summary>
        /// 获取会计年
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>会计年数据</returns>
        DataTable GetAcountYears(int deptId);

        /// <summary>
        /// 获取会计月
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="year">年份</param>
        /// <returns>会计月数据</returns>
        DataTable GetAcountMonths(int deptId, int year);

        /// <summary>
        /// 取得明细账数据
        /// </summary>
        /// <param name="accountType">账本类型</param>
        /// <param name="deptId">科室id</param>
        /// <param name="queryYear">年份</param>
        /// <param name="queryMonth">月份</param>
        /// <param name="typeId">类型id</param>
        /// <returns>明细账数据</returns>
        DataTable GetAccountData(int accountType, int deptId, int queryYear, int queryMonth,int typeId);

        /// <summary>
        /// 取得明细账数据
        /// </summary>
        /// <param name="accountType">账本类型</param>
        /// <param name="deptId">科室id</param>
        /// <param name="queryYear">年份</param>
        /// <param name="queryMonth">月份</param>
        /// <param name="busiCode">业务代码</param>
        /// <param name="typeId">类型id</param>
        /// <returns>明细账数据</returns>
        DataTable GetAccountData(int accountType, int deptId, int queryYear, int queryMonth,  string busiCode, int typeId);

        /// <summary>
        /// 取得明细账数据
        /// </summary>
        /// <param name="accountType">账本类型</param>
        /// <param name="deptId">科室id</param>
        /// <param name="queryYear">年份</param>
        /// <param name="queryMonth">月份</param>
        /// <param name="busiCode">业务代码</param>
        /// <param name="busiTypeModel">业务类型实体</param>
        /// <param name="typeId">类型id</param>
        /// <returns>明细账数据</returns>
        DataTable GetAccountData(int accountType, int deptId, int queryYear, int queryMonth,  string busiCode, DGBusiType busiTypeModel, int typeId);
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

        #region 物资汇总
        /// <summary>
        /// 获取入库业务信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="typeId">类型id</param>
        /// <returns>入库单据数据</returns>
        DataTable GetInStore(Dictionary<string, string> query,string typeId);

        /// <summary>
        /// 获取出库业务信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="typeId">类型id</param>
        /// <returns>出库业务数据</returns>
        DataTable GetOutStore(Dictionary<string, string> query, string typeId);

        /// <summary>
        /// 获取盘点信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="typeId">类型id</param>
        /// <returns>盘点信息</returns>
        DataTable GetCheck(Dictionary<string, string> query, string typeId);

        /// <summary>
        /// 获取调价信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="typeId">类型id</param>
        /// <returns>调价数据</returns>
        DataTable GetAdjPrice(Dictionary<string, string> query, string typeId);
        #endregion

        #region 物资流水账
        /// <summary>
        /// 获取入库业务信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="typeId">类型id</param>
        /// <returns>入库业务数据</returns>
        DataTable GetInStores(Dictionary<string, string> query, string typeId);

        /// <summary>
        /// 获取出库业务信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="typeId">类型id</param>
        /// <returns>出库业务信息</returns>
        DataTable GetOutStores(Dictionary<string, string> query, string typeId);

        /// <summary>
        /// 获取盘点信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="typeId">类型id</param>
        /// <returns>盘点数据</returns>
        DataTable GetChecks(Dictionary<string, string> query, string typeId);

        /// <summary>
        /// 获取调价信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="typeId">类型id</param>
        /// <returns>调价信息</returns>
        DataTable GetAdjPrices(Dictionary<string, string> query, string typeId);
        #endregion
        #endregion

        #region 月结
        /// <summary>
        /// 月结记录
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <returns>月结记录数据</returns>
        DataTable GetMonthBalaceByDept(int deptId);

        /// <summary>
        /// 执行月结
        /// </summary>
        /// <param name="workId">机构id</param>
        /// <param name="deptId">科室id</param>
        /// <param name="empId">人员id</param>
        /// <returns>执行结果</returns>
        MWBillResult ExcutMonthBalance(int workId, int deptId, int empId);

        /// <summary>
        /// 对账
        /// </summary>
        /// <param name="workId">机构id</param>
        /// <param name="deptId">科室id</param>
        /// <returns>对账结果</returns>
        MWSpResult ExcutSystemCheckAccount(int workId, int deptId);
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
        int UpdateCheckHeadStatus(MW_CheckHead head);

        /// <summary>
        /// 取得物资一级类型
        /// </summary>
        /// <returns>物资类型</returns>
        DataTable GetMaterialType();

        /// <summary>
        /// 删除所有未审核盘点单
        /// </summary>
        /// <param name="deptID">库房Id</param>
        /// <returns>1成功</returns>
        int DeleteAllNotAuditCheckHead(int deptID);
        #endregion
    }
}