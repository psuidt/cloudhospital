using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManger.Dao;
using HIS_Entity.DrugManage;

namespace HIS_DrugManger.ObjectModel.Store
{
    /// <summary>
    /// 药库库存处理器
    /// </summary>
    class DWStore : AbstractObjectModel, IStore
    {
        /// <summary>
        /// 按批次增加库存
        /// </summary>
        /// <param name="storeParam">药品库存入参</param>
        /// <returns>库存处理结果</returns>
        public DGStoreResult AddStore(StoreParam storeParam)
        {
            DGStoreResult storeResult = new DGStoreResult();
            if (storeParam != null)
            {
                DW_Storage storage = NewDao<IDWDao>().GetStorageInfo(storeParam.DeptID, storeParam.DrugID);
                if (storage != null)
                {
                    //获取批次数据
                    DW_Batch batch = NewDao<IDWDao>().GetBatchAmount(storeParam.DeptID, storeParam.DrugID, storeParam.BatchNO);
                    if (batch != null)
                    {
                        //如果批次数量加退货数量大于0
                        if (batch.BatchAmount + storeParam.Amount >= 0)
                        {
                            bool addRtn = NewDao<IDWDao>().AddStoreAmount(storeParam.DeptID, storeParam.DrugID, storeParam.Amount);
                            addRtn = NewDao<IDWDao>().AddBatchAmount(storeParam.DeptID, storeParam.DrugID, storeParam.BatchNO, storeParam.Amount);
                            if (addRtn)
                            {
                                //添加成功
                                DGBatchAllot batchAllot = new DGBatchAllot();
                                batchAllot.BatchNO = storeParam.BatchNO;
                                batchAllot.RetailPrice = batch.RetailPrice;
                                batchAllot.StockPrice = batch.StockPrice;
                                batchAllot.StoreAmount = batch.BatchAmount + storeParam.Amount;
                                batchAllot.StorageID = storage.StorageID;
                                storeResult.BatchAllot = new DGBatchAllot[1];
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
                        if (storeParam.Amount < 0)
                        {
                            storeResult.Result = 1;
                            return storeResult;
                        }
                     
                        //新建批次
                        DW_Batch dwBatch = NewObject<DW_Batch>();
                        dwBatch.StorageID = storage.StorageID;
                        dwBatch.BatchAmount = storeParam.Amount;
                        dwBatch.BatchNO = storeParam.BatchNO;
                        dwBatch.ValidityTime = storeParam.ValidityTime;
                        dwBatch.DelFlag = 0;
                        dwBatch.DeptID = storeParam.DeptID;
                        dwBatch.DrugID = storeParam.DrugID;
                        dwBatch.DelFlag = 0;
                        dwBatch.InstoreTime = System.DateTime.Now;
                        dwBatch.UnitID = storeParam.UnitID;
                        dwBatch.UnitName = storeParam.UnitName;
                        dwBatch.RetailPrice = storeParam.RetailPrice;
                        dwBatch.StockPrice = storeParam.StockPrice;
                        dwBatch.save();
                        bool addRtn = NewDao<IDWDao>().AddStoreAmount(storeParam.DeptID, storeParam.DrugID, storeParam.Amount);//改库存
                        if (addRtn)
                        {
                            //构建返回值
                            DGBatchAllot batchAllot = new DGBatchAllot();
                            batchAllot.BatchNO = storeParam.BatchNO;
                            batchAllot.RetailPrice = dwBatch.RetailPrice;
                            batchAllot.StockPrice = dwBatch.StockPrice;
                            batchAllot.StoreAmount = dwBatch.BatchAmount;
                            batchAllot.StorageID = storage.StorageID;
                            storeResult.BatchAllot = new DGBatchAllot[1];
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
                    DW_Storage dwStorage = NewObject<DW_Storage>();
                    dwStorage.DrugID = storeParam.DrugID;
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
                    DW_Batch dwBatch = NewObject<DW_Batch>();
                    dwBatch.StorageID = dwStorage.StorageID;
                    dwBatch.BatchAmount = storeParam.Amount;
                    dwBatch.BatchNO = storeParam.BatchNO;
                    dwBatch.ValidityTime = storeParam.ValidityTime;
                    dwBatch.DelFlag = 0;
                    dwBatch.DeptID = storeParam.DeptID;
                    dwBatch.DrugID = storeParam.DrugID;
                    dwBatch.DelFlag = 0;
                    dwBatch.InstoreTime = dwStorage.RegTime;
                    dwBatch.UnitID = storeParam.UnitID;
                    dwBatch.UnitName = storeParam.UnitName;
                    dwBatch.RetailPrice = storeParam.RetailPrice;
                    dwBatch.StockPrice = storeParam.StockPrice;
                    dwBatch.save();

                    //构建返回值
                    DGBatchAllot batchAllot = new DGBatchAllot();
                    batchAllot.BatchNO = storeParam.BatchNO;
                    batchAllot.RetailPrice = storeParam.RetailPrice;
                    batchAllot.StockPrice = storeParam.StockPrice;
                    batchAllot.StoreAmount = storeParam.Amount;
                    batchAllot.StorageID = dwStorage.StorageID;
                    storeResult.BatchAllot = new DGBatchAllot[1] { batchAllot };
                    storeResult.StoreAmount = storeParam.Amount;
                    storeResult.UnitAmount = 1;
                    storeResult.StorageID = dwStorage.StorageID;
                    return storeResult;
                }
            }

            return storeResult;
        }

        /// <summary>
        /// 按先进先出增加库存
        /// </summary>
        /// <param name="storeParam">药品库存入参</param>
        /// <returns>库存处理结果</returns>
        public DGStoreResult AddStoreAuto(StoreParam storeParam)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 增加库存
        /// </summary>
        /// <param name="storeParam">药品库存入参</param>
        /// <param name="retAllBatch">是否所有批次</param>
        /// <returns>库存处理结果</returns>
        public DGStoreResult AddStoreAuto(StoreParam storeParam, bool retAllBatch)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 清空库位
        /// </summary>
        /// <param name="locationID">库位ID</param>
        public void ClearLoaction(int locationID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取药品库存数量
        /// </summary>
        /// <param name="drugID">药品ID</param>
        /// <param name="deptID">库房ID</param>
        /// <param name="batchNO">批次号</param>
        /// <returns>库存数量</returns>
        public decimal GetDrugStoreAmount(int drugID, int deptID, string batchNO)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询药品批次信息
        /// </summary>
        /// <param name="storageID">库存ID</param>
        /// <returns>药品批次信息列表</returns>
        public DataTable LoadDrugBatch(int storageID)
        {
            return NewDao<IDWDao>().LoadDrugBatch(storageID);
        }

        /// <summary>
        /// 查询药品库存信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>库存信息表</returns>
        public DataTable LoadDrugStorage(Dictionary<string, string> condition)
        {
            return NewDao<IDWDao>().LoadDrugStorage(condition);
        }

        /// <summary>
        /// 查询药品库存信息(带批次)
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>库存信息表</returns>
        public DataTable LoadDrugStorages(Dictionary<string, string> condition)
        {
            return NewDao<IDWDao>().LoadDrugStorages(condition);
        }

        /// <summary>
        /// 查询过期药品信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>过期药品列表</returns>
        public DataTable LoadValidDateDrug(Dictionary<string, string> condition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 按批次减少库存
        /// </summary>
        /// <param name="storeParam">库存处理入参</param>
        /// <returns>库存处理结果</returns>
        public DGStoreResult ReduceStore(StoreParam storeParam)
        {
            DGStoreResult storeResult = new DGStoreResult();
            if (storeParam != null)
            {
               DW_Storage storage = NewDao<IDWDao>().GetStorageInfo(storeParam.DeptID, storeParam.DrugID);
                
                if (storage != null)
                {
                    DW_Batch batch = NewDao<IDWDao>().GetBatchAmount(storeParam.DeptID, storeParam.DrugID, storeParam.BatchNO);
                    
                    if (batch != null)
                    {
                        //本批次存量 和库存总量都必须大于出库数量
                        if (batch.BatchAmount >= storeParam.Amount && storage.Amount >= storeParam.Amount)
                        {
                            storage.Amount = storage.Amount - storeParam.Amount;
                            storage.BindDb(this as AbstractBusines);
                            storage.save();
                            batch.BatchAmount -= storeParam.Amount;
                            batch.BindDb(this as AbstractBusines);
                            batch.save();

                            DGBatchAllot batchAllot = new DGBatchAllot();
                            batchAllot.BatchNO = storeParam.BatchNO;
                            batchAllot.RetailPrice = batch.RetailPrice;
                            batchAllot.StockPrice = batch.StockPrice;
                            batchAllot.StoreAmount = batch.BatchAmount;
                            batchAllot.StorageID = storage.StorageID;
                            storeResult.BatchAllot = new DGBatchAllot[1];
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
        /// 按先进先出减少库存
        /// </summary>
        /// <param name="drugID">药品ID</param>
        /// <param name="deptID">库房ID</param>
        /// <param name="amount">数量</param>
        /// <returns>库存处理结果</returns>
        public DGStoreResult ReduceStoreAuto(int drugID, int deptID, decimal amount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 药品归库
        /// </summary>
        /// <param name="storageID">库存ID</param>
        /// <param name="locationID">库位ID</param>
        public void SetDrugLocation(List<int> storageID, int locationID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 按批次更新库存
        /// </summary>
        /// <param name="drugID">药品ID</param>
        /// <param name="deptID">库房ID</param>
        /// <param name="amount">数量</param>
        /// <param name="batchNO">批次号</param>
        /// <returns>库存处理结果</returns>
        public DGStoreResult UpdateStore(int drugID, int deptID, decimal amount, string batchNO)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新有效库存
        /// </summary>
        /// <param name="drugID">药品ID</param>
        /// <param name="deptID">库房ID</param>
        /// <param name="reduceAmount">数量</param>
        /// <returns>库存处理结果</returns>
        public int UpdateValidStore(int drugID, int deptID, decimal reduceAmount)
        {
            throw new NotImplementedException();
        }
    }
}
