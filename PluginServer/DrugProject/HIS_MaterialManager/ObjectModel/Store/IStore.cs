using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS_Entity.MaterialManage;

namespace HIS_MaterialManager.ObjectModel.Store
{
    /// <summary>
    /// 库存处理器
    /// </summary>
    interface IStore
    {
        /// <summary>
        /// 按批次增加库存
        /// </summary>
        /// <param name="storeParam">药品库存入参</param>
        /// <returns>库存处理结果</returns>
        MWStoreResult AddStore(StoreParam storeParam);

        /// <summary>
        /// 按批次减少库存
        /// </summary>
        /// <param name="sotreParam">库存处理参数实体</param>
        /// <returns>库存处理结果</returns>
        MWStoreResult ReduceStore(StoreParam sotreParam);

        /// <summary>
        /// 按批次更新库存
        /// </summary>
        /// <param name="drugID">药品ID</param>
        /// <param name="deptID">库房ID</param>
        /// <param name="amount">数量</param>
        /// <param name="batchNO">批次号</param>
        /// <returns>库存处理结果</returns>
        MWStoreResult UpdateStore(int drugID, int deptID, decimal amount, string batchNO);

        /// <summary>
        /// 按先进先出增加库存
        /// </summary>
        /// <param name="storeParam">药品库存入参</param>
        /// <param name="retAllBatch">true返回所有批次，返回</param>
        /// <returns>库存处理结果</returns>
        MWStoreResult AddStoreAuto(StoreParam storeParam, bool retAllBatch);

        /// <summary>
        /// 按先进先出减少库存
        /// </summary>
        /// <param name="drugID">药品ID</param>
        /// <param name="deptID">库房ID</param>
        /// <param name="amount">数量</param>
        /// <returns>库存处理结果</returns>
        MWStoreResult ReduceStoreAuto(int drugID, int deptID, decimal amount);

        /// <summary>
        /// 更新有效库存
        /// </summary>
        /// <param name="drugID">药品ID</param>
        /// <param name="deptID">库房ID</param>
        /// <param name="reduceAmount">数量</param>
        /// <returns>库存处理结果</returns>
        int UpdateValidStore(int drugID, int deptID, decimal reduceAmount);

        /// <summary>
        /// 获取药品库存数量
        /// </summary>
        /// <param name="drugID">药品ID</param>
        /// <param name="deptID">库房ID</param>
        /// <param name="batchNO">批次号</param>
        /// <returns>库存数量</returns>
        decimal GetDrugStoreAmount(int drugID, int deptID, string batchNO);

        /// <summary>
        /// 查询药品库存信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>库存信息表</returns>
        DataTable LoadDrugStorage(Dictionary<string, string> condition);

        /// <summary>
        /// 查询药品库存信息(带批次)
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>库存信息表</returns>
        DataTable LoadDrugStorages(Dictionary<string, string> condition);

        /// <summary>
        /// 查询药品批次信息
        /// </summary>
        /// <param name="storageID">库存ID</param>
        /// <returns>药品批次信息列表</returns>
        DataTable LoadDrugBatch(int storageID);

        /// <summary>
        /// 药品归库
        /// </summary>
        /// <param name="storageID">库存ID</param>
        /// <param name="locationID">库位ID</param>
        void SetDrugLocation(List<int> storageID, int locationID);

        /// <summary>
        /// 清空库位
        /// </summary>
        /// <param name="locationID">库位ID</param>
        void ClearLoaction(int locationID);

        /// <summary>
        /// 查询过期药品信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>过期药品列表</returns>
        DataTable LoadValidDateDrug(Dictionary<string, string> condition);
    }
}
