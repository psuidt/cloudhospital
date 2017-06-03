using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.DbProvider.SqlPagination;
using HIS_Entity.BasicData;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManger.Dao
{
    /// <summary>
    /// 药品基础数据数据库访问
    /// </summary>
    public interface IDGDao
    {
        #region 药剂科室设置
        /// <summary>
        /// 是否存在药剂科室
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool ExistDrugDept(int deptId);

        /// <summary>
        /// 取得药剂科室数据
        /// </summary>
        /// <returns>药剂科室数据集</returns>
        DataTable GetDeptDicData();

        /// <summary>
        /// 按类型获取药剂科室列表
        /// </summary>
        /// <param name="deptType">类型ID</param>
        /// <returns>药剂科室数据集</returns>
        DataTable GetDrugDeptList(int deptType);

        /// <summary>
        /// 初始化使用单据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="deptType">科室类型</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool InitSerialNumber(int deptId, int deptType);

        /// <summary>
        /// 取得药剂科室单据数据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>药剂科室数据集</returns>
        DataTable GetDrugDeptBill(int deptId);

        /// <summary>
        /// 修改启用标志
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>小于0失败</returns>
        int UpdateStartStatus(int deptId);

        /// <summary>
        /// 停用药剂科室
        /// </summary>
        /// <param name="deptDicID">药剂科室id</param>
        /// <param name="deptId">科室id</param>
        /// <returns>小于0失败</returns>
        int StopUseDrugDept(int deptDicID, int deptId);

        /// <summary>
        /// 设置药剂科室盘点状态
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="status">状态1开启盘点，0未开启</param>
        /// <param name="typeFlag">库房类型0药房，1药库</param>
        void SetCheckStatus(int deptId, int status, int typeFlag);

        /// <summary>
        /// 获取科室的库存药品信息
        /// </summary>
        /// <param name="dept">科室ID</param>
        /// <returns>库存药品信息数据集</returns>
        DataTable GetStoreDrugInFo(int dept);

        #endregion       

        #region 药品参数设置
        /// <summary>
        /// 取得启用的药剂科室
        /// </summary>
        /// <returns>启用药剂科室数据集</returns>
        DataTable GetUsedDrugDeptData();

        /// <summary>
        /// 获取药品公共参数
        /// </summary>
        /// <returns>药品公共参数集</returns>
        DataTable GetPublicParameters();

        /// <summary>
        /// 获取药剂科室参数
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>药剂科室参数集</returns>
        DataTable GetDeptParameters(int deptId);

        /// <summary>
        /// 获取药剂科室参数
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <param name="paraId">参数ID</param>
        /// <returns>药剂科室参数</returns>
        Basic_SystemConfig GetDeptParameters(int deptId, string paraId);

        /// <summary>
        /// 保存药品参数
        /// </summary>
        /// <param name="parameterList">药品参数列表</param>
        /// <returns>小于0失败</returns>
        int SaveDrugParameters(List<Basic_SystemConfig> parameterList);

        /// <summary>
        /// 保存药品参数
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>返回结果</returns>
        int SaveDrugParameters(int deptId);
        #endregion

        #region 药库药房往来科室设置
        /// <summary>
        /// 取得库房数据
        /// </summary>
        /// <param name="menuTypeFlag">菜单类型0药房往来科室维护，1药库往来科室维护</param>
        /// <returns>库房数据集</returns>
        DataTable GetStoreRoomData(int menuTypeFlag);

        /// <summary>
        ///  获取往来科室信息
        /// </summary>
        /// <param name="andWhere">and查询条件</param>
        /// <param name="orWhere">or查询条件</param>
        /// <returns>相关科室数据集</returns>
        DataTable GetRelateDeptData(List<Tuple<string, string, SqlOperator>> andWhere, List<Tuple<string, string, SqlOperator>> orWhere);

        /// <summary>
        /// 获取往来科室信息
        /// </summary>
        /// <param name="deptId">科室ID</param>
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

        #region 剂型、药品类型、子类型、药理、供应商
        /// <summary>
        /// 加载供应商【ShowCard专用】
        /// </summary>
        /// <returns>供应商列表</returns>
        DataTable GetSupplyForShowCard();

        /// <summary>
        /// 根据子类型获取父类型名称
        /// </summary>
        /// <param name="cTypeId">子类型ID</param>
        /// <returns>返回父类型名称</returns>
        string GetTypeName(string cTypeId);

        /// <summary>
        /// 取得药品剂型
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>药品剂型</returns>
        DataTable GetDosageUserOr(Dictionary<string, string> query = null);

        /// <summary>
        /// 药理类型数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>药理类型数据集</returns>
        DataTable GetPharmByParentId(Dictionary<string, string> query);

        /// <summary>
        /// 药品类型和子类型
        /// </summary>
        /// <returns>药品类型和子类型数据集</returns>
        DataTable GetDrugTypeAndChild();

        /// <summary>
        /// 取得药品类型典
        /// </summary>
        /// <returns>药品类型字典数据集</returns>
        DataTable GetTypeDic();

        /// <summary>
        /// 获取药品子类型
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>药品子类型数据集</returns>
        DataTable GetChildDrugType(Dictionary<string, string> query = null);
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
        /// 取得药品字典选择卡片数据
        /// </summary>
        /// <returns>药品字典信息</returns>
        DataTable GetDrugDicShowCard();

        /// <summary>
        /// 删除采购计划单 
        /// </summary>
        /// <param name="billID">采购计划单表头Id</param>
        /// <returns>返回结果</returns>
        bool DeleteDWPlanBill(int billID);

        /// <summary>
        /// 取得小于下限的库存药品
        /// </summary>
        /// <param name="deptId">药库ID</param>
        /// <returns>药品数据</returns>
        DataTable GetLessLowerLimitDrugData(int deptId);

        /// <summary>
        /// 取得小于上限的库存药品
        /// </summary>
        /// <param name="deptId">药库ID</param>
        /// <returns>药品数据</returns>
        DataTable GetLessUpperLimitDrugData(int deptId);
        #endregion

        #region 药品字典
        /// <summary>
        /// 单表查询
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="andWhere">and条件</param>
        /// <param name="orWhere">or条件</param>
        /// <returns>获取药品数据集</returns>
        DataTable GetDataTable<T>(List<Tuple<string, string, SqlOperator>> andWhere, List<Tuple<string, string, SqlOperator>> orWhere) where T : AbstractEntity;

        /// <summary>
        /// 分页的单表查询
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="andWhere">于条件</param>
        /// <param name="orWhere">或条件</param>
        /// <param name="page">分页方法</param>
        /// <returns>获取药品数据集</returns>
        DataTable GetDataTable<T>(List<Tuple<string, string, SqlOperator>> andWhere, List<Tuple<string, string, SqlOperator>> orWhere, PageInfo page) where T : AbstractEntity;

        /// <summary>
        /// 药品的换算系数
        /// </summary>
        /// <param name="drugId">药品ID</param>
        /// <returns>换算系数</returns>
        decimal GetPackAmount(int drugId);

        /// <summary>
        /// 药品字典
        /// </summary>
        /// <param name="andWhere">where 条件</param>
        /// <param name="orWhere">OR 条件</param>
        /// <returns>获取药品字典数据集</returns>
        DataTable GetDrugDic(List<Tuple<string, string, SqlOperator>> andWhere, List<Tuple<string, string, SqlOperator>> orWhere);

        /// <summary>
        /// 药品本院典
        /// </summary>
        /// <param name="andWhere">and条件</param>
        /// <param name="orWhere">or条件</param>
        /// <returns>药品本院典数据集</returns>
        DataTable GetDrugDicHisDataTable(List<Tuple<string, string, SqlOperator>> andWhere, List<Tuple<string, string, SqlOperator>> orWhere);

        /// <summary>
        /// 本院查中点典数据
        /// </summary>
        /// <param name="andWhere">and条件</param>
        /// <param name="orWhere">or条件</param>
        /// <param name="createWorkId">创建机构ID</param>
        /// <returns>获取本院字典数据源</returns>
        DataTable GetDrugDic(List<Tuple<string, string, SqlOperator>> andWhere, List<Tuple<string, string, SqlOperator>> orWhere, int createWorkId);

        /// <summary>
        /// 获取对象集合
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="andWhere">and查询条件</param>
        /// <param name="orWhere">or查询条件</param>
        /// <returns>对象集合</returns>
        IEnumerable<T> GetEntityType<T>(List<Tuple<string, string, SqlOperator>> andWhere = null, List<Tuple<string, string, SqlOperator>> orWhere = null) where T : AbstractEntity;

        /// <summary>
        /// 中心药品是否已经存在库存
        /// </summary>
        /// <param name="centeDrugId">中心药品ID</param>
        /// <returns>true:不存在 false 存在</returns>
        bool StoreExsitDrug(int centeDrugId);
        #endregion

        #region 药品库位 库存
        /// <summary>
        /// 获取库位详细信息
        /// </summary>
        /// <param name="locationid">库位ID</param>
        /// <returns>库位对象</returns>
        DG_Location GetLocationInfo(int locationid);

        /// <summary>
        /// 根据父类和库位名称获取库位
        /// </summary>
        /// <param name="parentid">父级库位ID</param>
        /// <param name="locationname">库位名称</param>
        /// <returns>库位对象</returns>
        DG_Location GetLocationByName(int parentid, string locationname);

        /// <summary>
        /// 获取库位节点信息
        /// </summary>
        /// <param name="deptid">科室ID</param>
        /// <returns>库位对象集</returns>
        List<DG_Location> GetLocationList(int deptid);

        /// <summary>
        /// 归库操作
        /// </summary>
        /// <param name="locationid">库位ID</param>
        /// <param name="ids">ID集</param>
        /// <param name="frmName">窗口名称</param>
        /// <returns>返回结果</returns>
        int UpdateStorage(int locationid, string ids, string frmName);

        /// <summary>
        /// 拆零操作
        /// </summary>
        /// <param name="ids">ID集</param>
        /// <param name="type">类型</param>
        /// <returns>返回结果</returns>
        int UpdateStorages(string ids, int type);
        #endregion

        #region 调价
        /// <summary>
        /// 读取药品调价表头
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>调价表头数据集</returns>
        DataTable LoadAdjHead(Dictionary<string, string> queryCondition);

        /// <summary>
        /// 读取药品调价详情
        /// </summary>
        /// <param name="queryCondition">查询条件</param>
        /// <returns>调价详情数据集</returns>
        DataTable LoadAdjDetail(Dictionary<string, string> queryCondition);

        /// <summary>
        /// 根据ID获取表头信息
        /// </summary>
        /// <param name="headid">表头ID</param>
        /// <returns>调价表头数据集</returns>
        DG_AdjHead LoadAdjHeadById(int headid);
        #endregion

        #region 药房药品显示卡片数据
        /// <summary>
        /// 获取药房药品ShowCard数据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <returns>药房药品数据</returns>
        DataTable GetDrugStoreShowCardData(int deptId);
        #endregion

        #region 公共基础数据
        #endregion
    }
}
