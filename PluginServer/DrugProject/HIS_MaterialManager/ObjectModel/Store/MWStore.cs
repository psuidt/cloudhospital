using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.DrugManage;
using HIS_Entity.MaterialManage;
using HIS_MaterialManager.Dao;

namespace HIS_MaterialManager.ObjectModel.Store
{
    /// <summary>
    /// 物资库存处理器
    /// </summary>
    public class MwStore : AbstractObjectModel, IStore
    {
        /// <summary>
        /// 按批次增加库存
        /// </summary>
        /// <param name="storeParam">药品库存入参</param>
        /// <returns>库存处理结果</returns>
        public MWStoreResult AddStore(StoreParam storeParam)
        {
            MWStoreResult storeResult = new MWStoreResult();
            if (storeParam != null)
            {
                MW_Storage storage = NewDao<IMWDao>().GetStorageInfo(storeParam.DeptID, storeParam.MaterialId);
                if (storage != null)
                {
                    //获取批次数据
                    MW_Batch batch = NewDao<IMWDao>().GetBatchAmount(storeParam.DeptID, storeParam.MaterialId, storeParam.BatchNO);
                    if (batch != null)
                    {
                        //如果批次数量加退货数量大于0
                        if (batch.BatchAmount + storeParam.Amount >= 0)
                        {
                            bool addRtn = NewDao<IMWDao>().AddStoreAmount(storeParam.DeptID, storeParam.MaterialId, storeParam.Amount);

                            addRtn = NewDao<IMWDao>().AddBatchAmount(storeParam.DeptID, storeParam.MaterialId, storeParam.BatchNO, storeParam.Amount);

                            if (addRtn)
                            {
                                //添加成功
                                MWBatchAllot batchAllot = new MWBatchAllot();
                                batchAllot.BatchNO = storeParam.BatchNO;
                                batchAllot.RetailPrice = storeParam.RetailPrice;
                                batchAllot.StockPrice = batch.StockPrice;
                                batchAllot.StoreAmount = batch.BatchAmount + storeParam.Amount;
                                batchAllot.StorageID = storage.StorageID;
                                storeResult.BatchAllot = new MWBatchAllot[1];
                                storeResult.BatchAllot[0] = batchAllot;
                                storeResult.StoreAmount = storage.Amount + storeParam.Amount;
                                storeResult.UnitAmount = 1;
                                storeResult.StorageID = storage.StorageID;
                                return storeResult;
                            }
                            else
                            {
                                //添加失败
                                storeResult.Result = 2;
                                storeResult.StoreAmount = storage.Amount;
                                storeResult.UnitAmount = 1;
                                storeResult.StorageID = storage.StorageID;
                                return storeResult;
                            }
                        }
                        else
                        {
                            storeResult.Result = 1;
                            storeResult.StoreAmount = storage.Amount;
                            storeResult.UnitAmount = 1;
                            storeResult.StorageID = storage.StorageID;
                            return storeResult;
                        }
                    }
                    else 
                    {
                        //批次不存在
                        //如果退库业务
                        if (storeParam.BussConstant == MWConstant.OP_MW_BACKSTORE)
                        {
                            storeResult.Result = 1;
                            return storeResult;
                        }

                        if (storeParam.Amount < 0)
                        {
                            storeResult.Result = 1;
                            return storeResult;
                        }

                        //新建批次
                        MW_Batch dwBatch = NewObject<MW_Batch>();
                        dwBatch.StorageID = storage.StorageID;
                        dwBatch.BatchAmount = storeParam.Amount;
                        dwBatch.BatchNO = storeParam.BatchNO;
                        dwBatch.ValidityTime = storeParam.ValidityTime;
                        dwBatch.DelFlag = 0;
                        dwBatch.DeptID = storeParam.DeptID;
                        dwBatch.MaterialID = storeParam.MaterialId;
                        dwBatch.DelFlag = 0;
                        dwBatch.InstoreTime = System.DateTime.Now;
                        dwBatch.UnitID = storeParam.UnitID;
                        dwBatch.UnitName = storeParam.UnitName;
                        dwBatch.StockPrice = storeParam.StockPrice;
                        dwBatch.save();

                        bool addRtn = NewDao<IMWDao>().AddStoreAmount(storeParam.DeptID, storeParam.MaterialId, storeParam.Amount);//改库存
                        if (addRtn)
                        {
                            //构建返回值
                            MWBatchAllot batchAllot = new MWBatchAllot();
                            batchAllot.BatchNO = storeParam.BatchNO;
                            batchAllot.StockPrice = dwBatch.StockPrice;
                            batchAllot.StoreAmount = dwBatch.BatchAmount;
                            batchAllot.StorageID = storage.StorageID;
                            storeResult.BatchAllot = new MWBatchAllot[1];
                            storeResult.BatchAllot[0] = batchAllot;
                            storeResult.StoreAmount = storage.Amount + storeParam.Amount;
                            storeResult.UnitAmount = 1;
                            storeResult.StorageID = storage.StorageID;
                        }
                        else
                        {
                            //添加失败
                            storeResult.Result = 2;
                            storeResult.StoreAmount = storage.Amount;
                            storeResult.UnitAmount = 1;
                            storeResult.StorageID = storage.StorageID;
                            return storeResult;
                        }
                    }
                }
                else
                {
                    if (storeParam.Amount < 0)
                    {
                        storeResult.Result = 1;
                        return storeResult;
                    }

                    //新建库存记录
                    MW_Storage dwStorage = NewObject<MW_Storage>();
                    dwStorage.MaterialID = storeParam.MaterialId;
                    dwStorage.DeptID = storeParam.DeptID;
                    dwStorage.DelFlag = 0;
                    dwStorage.UnitID = storeParam.UnitID;
                    dwStorage.UnitName = storeParam.UnitName;
                    dwStorage.Amount = storeParam.Amount;
                    dwStorage.UpperLimit = 0;
                    dwStorage.LowerLimit = 0;
                    dwStorage.LocationID = 0;
                    dwStorage.Place = string.Empty;
                    dwStorage.RegTime = System.DateTime.Now;
                    dwStorage.LStockPrice = 0;
                    dwStorage.save();
                    //新建批次记录
                    MW_Batch dwBatch = NewObject<MW_Batch>();
                    dwBatch.StorageID = dwStorage.StorageID;
                    dwBatch.BatchAmount = storeParam.Amount;
                    dwBatch.BatchNO = storeParam.BatchNO;
                    dwBatch.ValidityTime = storeParam.ValidityTime;
                    dwBatch.DelFlag = 0;
                    dwBatch.DeptID = storeParam.DeptID;
                    dwBatch.MaterialID = storeParam.MaterialId;
                    dwBatch.DelFlag = 0;
                    dwBatch.InstoreTime = dwStorage.RegTime;
                    dwBatch.UnitID = storeParam.UnitID;
                    dwBatch.UnitName = storeParam.UnitName;
                    //dwBatch.RetailPrice = storeParam.RetailPrice;
                    dwBatch.StockPrice = storeParam.StockPrice;
                    dwBatch.save();
                    //构建返回值
                    MWBatchAllot batchAllot = new MWBatchAllot();
                    batchAllot.BatchNO = storeParam.BatchNO;
                    batchAllot.RetailPrice = storeParam.RetailPrice;
                    batchAllot.StockPrice = storeParam.StockPrice;
                    batchAllot.StoreAmount = storeParam.Amount;
                    batchAllot.StorageID = dwStorage.StorageID;
                    storeResult.BatchAllot = new MWBatchAllot[1] { batchAllot };
                    storeResult.StoreAmount = storeParam.Amount;
                    storeResult.UnitAmount = 1;
                    storeResult.StorageID = dwStorage.StorageID;
                    return storeResult;
                }
            }

            return storeResult;
        }

        /// <summary>
        /// 添加库存
        /// </summary>
        /// <param name="storeParam">库存参数</param>
        /// <param name="retAllBatch">是否返回所有参数</param>
        /// <returns>返回结果</returns>
        public MWStoreResult AddStoreAuto(StoreParam storeParam, bool retAllBatch)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 清空库位
        /// </summary>
        /// <param name="locationID">库位id</param>
        public void ClearLoaction(int locationID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 取得药品库存数
        /// </summary>
        /// <param name="drugID">药品id</param>
        /// <param name="deptID">科室id</param>
        /// <param name="batchNO">批次号</param>
        /// <returns>库存数</returns>
        public decimal GetDrugStoreAmount(int drugID, int deptID, string batchNO)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 记载库存批次数据
        /// </summary>
        /// <param name="storageID">库存id</param>
        /// <returns>库存批次数据</returns>
        public DataTable LoadDrugBatch(int storageID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 加载药品库存数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>库存数据</returns>
        public DataTable LoadDrugStorage(Dictionary<string, string> condition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 加载药品库存数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>库存数据</returns>
        public DataTable LoadDrugStorages(Dictionary<string, string> condition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 加载有效日期药品
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>有效日期药品</returns>
        public DataTable LoadValidDateDrug(Dictionary<string, string> condition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 减库存
        /// </summary>
        /// <param name="storeParam">药品库存参数</param>
        /// <returns>返回库存结果</returns>
        public MWStoreResult ReduceStore(StoreParam storeParam)
        {
            MWStoreResult storeResult = new MWStoreResult();
            if (storeParam != null)
            {
                MW_Storage storage = NewDao<IMWDao>().GetStorageInfo(storeParam.DeptID, storeParam.MaterialId);
                if (storage != null)
                {
                    MW_Batch batch = NewDao<IMWDao>().GetBatchAmount(storeParam.DeptID, storeParam.MaterialId, storeParam.BatchNO);
                    if (batch != null)
                    {
                        //本批次存量 和库存总量都必须大于出库数量
                        if (batch.BatchAmount >= storeParam.Amount && storage.Amount >= storeParam.Amount)
                        {
                            storage.Amount = storage.Amount - storeParam.Amount;
                            storage.save();
                            batch.BatchAmount -= storeParam.Amount;
                            batch.save();

                            MWBatchAllot batchAllot = new MWBatchAllot();
                            batchAllot.BatchNO = storeParam.BatchNO;
                            batchAllot.RetailPrice = storeParam.RetailPrice;
                            batchAllot.StockPrice = batch.StockPrice;
                            batchAllot.StoreAmount = batch.BatchAmount;
                            batchAllot.StorageID = storage.StorageID;
                            storeResult.BatchAllot = new MWBatchAllot[1];
                            storeResult.BatchAllot[0] = batchAllot;
                            storeResult.StoreAmount = storage.Amount;
                            storeResult.UnitAmount = storeParam.UnitAmount;
                            storeResult.StorageID = storage.StorageID;
                            return storeResult;
                        }
                        else
                        {
                            storeResult.Result = 1;
                            storeResult.StoreAmount = storage.Amount;
                            storeResult.UnitAmount = 1;
                            storeResult.StorageID = storage.StorageID;
                            return storeResult;
                        }
                    }
                    else
                    {
                        storeResult.Result = 1;
                        storeResult.StoreAmount = storage.Amount;
                        storeResult.UnitAmount = 1;
                        storeResult.StorageID = storage.StorageID;
                        return storeResult;
                    }
                }
                else
                {
                    storeResult.Result = 1;
                    return storeResult;
                }
            }

            return storeResult;
        }

        /// <summary>
        /// 减库存
        /// </summary>
        /// <param name="drugID">物资id</param>
        /// <param name="deptID">科室id</param>
        /// <param name="amount">数量</param>
        /// <returns>结果</returns>
        public MWStoreResult ReduceStoreAuto(int drugID, int deptID, decimal amount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 设置库位
        /// </summary>
        /// <param name="storageID">库存id</param>
        /// <param name="locationID">库位id</param>
        public void SetDrugLocation(List<int> storageID, int locationID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新库存
        /// </summary>
        /// <param name="drugID">物资id</param>
        /// <param name="deptID">科室id</param>
        /// <param name="amount">数量</param>
        /// <param name="batchNO">批次号</param>
        /// <returns>返回结果</returns>
        public MWStoreResult UpdateStore(int drugID, int deptID, decimal amount, string batchNO)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新有效库存
        /// </summary>
        /// <param name="drugID">物资id</param>
        /// <param name="deptID">科室id</param>
        /// <param name="reduceAmount">数量</param>
        /// <returns>1成功</returns>
        public int UpdateValidStore(int drugID, int deptID, decimal reduceAmount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 按批次更新库存
        /// </summary>
        /// <param name="storeParam">药品库存入参</param>
        /// <returns>库存处理结果</returns>
        public MWStoreResult UpdateStore(StoreParam storeParam)
        {
            MWStoreResult storeResult = new MWStoreResult();
            if (storeParam != null)
            {
                MW_Storage storage = NewDao<IMWDao>().GetStorageInfo(storeParam.DeptID, storeParam.MaterialId);
                if (storage != null)
                {
                    //获取批次数据
                    MW_Batch batch = NewDao<IMWDao>().GetBatchAmount(storeParam.DeptID, storeParam.MaterialId, storeParam.BatchNO);
                    if (batch != null)
                    {
                        //如果批次数量加退货数量大于0
                        if (batch.BatchAmount + storeParam.Amount >= 0)
                        {
                            bool addRtn = NewDao<IMWDao>().AddStoreAmount(storeParam.DeptID, storeParam.MaterialId, storeParam.Amount);

                            addRtn = NewDao<IMWDao>().AddBatchAmount(storeParam.DeptID, storeParam.MaterialId, storeParam.BatchNO, storeParam.Amount);

                            if (addRtn)
                            {
                                //添加成功
                                MWBatchAllot batchAllot = new MWBatchAllot();
                                batchAllot.BatchNO = storeParam.BatchNO;
                                batchAllot.StoreAmount = batch.BatchAmount + storeParam.Amount;
                                batchAllot.StorageID = storage.StorageID;
                                storeResult.BatchAllot = new MWBatchAllot[1];
                                storeResult.BatchAllot[0] = batchAllot;
                                storeResult.StoreAmount = storage.Amount + storeParam.Amount;
                                storeResult.UnitAmount = 1;
                                storeResult.StorageID = storage.StorageID;
                                return storeResult;
                            }
                            else
                            {
                                //添加失败
                                storeResult.Result = 2;
                                storeResult.StoreAmount = storage.Amount;
                                storeResult.UnitAmount = 1;
                                storeResult.StorageID = storage.StorageID;
                                return storeResult;
                            }
                        }
                        else
                        {
                            storeResult.Result = 1;
                            storeResult.StoreAmount = storage.Amount;
                            storeResult.UnitAmount = 1;
                            storeResult.StorageID = storage.StorageID;
                            return storeResult;
                        }
                    }                    
                }                
            }

            return storeResult;
        }
    }
}
