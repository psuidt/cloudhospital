using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManger.Dao;
using HIS_Entity.DrugManage;

namespace HIS_DrugManger.ObjectModel.Store
{
    /// <summary>
    /// 药房库存处理器
    /// </summary>
    public class DSStore : AbstractObjectModel, IStore
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
                DS_Storage storage = NewDao<IDSDao>().GetStorageInfo(storeParam.DeptID, storeParam.DrugID);
                if (storage != null)
                {
                    storeParam.StorageId = storage.StorageID;

                    //获取批次数据
                    DS_Batch batch = NewDao<IDSDao>().GetBatchAmount(storeParam.DeptID, storeParam.DrugID, storeParam.BatchNO);
                    if (batch != null)
                    {
                        //如果批次数量加退货数量大于0
                        if (batch.BatchAmount + storeParam.Amount >= 0 && storage.Amount + storeParam.Amount >= 0)
                        {
                            if((batch.BatchAmount + storeParam.Amount) ==0)
                            {
                                //当库存量为零时,把批次记录标记为已删除，新增加该批次库存，然后再标记为有效
                                batch.DelFlag = 1;
                            }
                            else
                            {
                                batch.DelFlag = 0;
                            }

                            batch.BatchAmount += storeParam.Amount;
                            batch.BindDb(this as AbstractBusines);
                            batch.save();

                            storage.Amount += storeParam.Amount;
                            storage.BindDb(this as AbstractBusines);
                            storage.save();

                            //添加成功
                            DGBatchAllot batchAllot = new DGBatchAllot();
                            batchAllot.BatchNO = storeParam.BatchNO;
                            batchAllot.RetailPrice = batch.RetailPrice;
                            batchAllot.StockPrice = batch.StockPrice;
                            batchAllot.StoreAmount = batch.BatchAmount;
                            batchAllot.StorageID = storage.StorageID;
                            storeResult.BatchAllot = new DGBatchAllot[1];
                            storeResult.BatchAllot[0] = batchAllot;
                            storeResult.StoreAmount = storage.Amount;
                            storeResult.UnitAmount = 1;
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
                        if (storeParam.BussConstant == DGConstant.OP_DS_RETURNSOTRE)
                        {
                            //退库业务
                            storeResult.Result = 1;
                            return storeResult;
                        }

                        if (storeParam.BussConstant == DGConstant.OP_DS_FIRSTIN)
                        {
                            if (storeParam.Amount < 0)
                            {
                                storeResult.Result = 1;
                                return storeResult;
                            }
                        }

                        //新建批次
                        DS_Batch dwBatch = NewObject<DS_Batch>();
                        dwBatch.StorageID = storage.StorageID;
                        dwBatch.BatchAmount = storeParam.Amount;
                        dwBatch.BatchNO = storeParam.BatchNO;
                        dwBatch.ValidityTime = storeParam.ValidityTime;
                        dwBatch.DelFlag = 0;
                        dwBatch.DeptID = storeParam.DeptID;
                        dwBatch.DrugID = storeParam.DrugID;
                        dwBatch.DelFlag = 0;
                        dwBatch.UnitAmount = storeParam.PackAmount;//单位系数
                        dwBatch.InstoreTime = System.DateTime.Now;
                        dwBatch.PackUnit = storeParam.PackUnit;
                        dwBatch.UnitID = storeParam.UnitID;
                        dwBatch.UnitName = storeParam.UnitName;
                        dwBatch.RetailPrice = storeParam.RetailPrice;
                        dwBatch.StockPrice = storeParam.StockPrice;
                        dwBatch.save();
                        bool addRtn = NewDao<IDSDao>().AddStoreAmount(storeParam.DeptID, storeParam.DrugID, storeParam.Amount);//改库存
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
                            storeResult.UnitAmount = storeParam.PackAmount;
                            storeResult.StorageID = storage.StorageID;
                        }
                        else
                        {
                            //添加失败
                            storeResult.Result = 2;
                            storeResult.StoreAmount = storage.Amount;
                            storeResult.UnitAmount = storeParam.PackAmount;
                            storeResult.StorageID = storage.StorageID;
                            return storeResult;
                        }
                    }
                }
                else
                {
                    if (storeParam.BussConstant == DGConstant.OP_DS_RETURNSOTRE)
                    {
                        //如果返库业务
                        storeResult.Result = 1;
                        return storeResult;
                    }

                    if (storeParam.Amount < 0)
                    {
                        storeResult.Result = 1;
                        return storeResult;
                    }

                    //新建库存记录
                    DS_Storage dwStorage = NewObject<DS_Storage>();
                    dwStorage.DrugID = storeParam.DrugID;
                    dwStorage.DeptID = storeParam.DeptID;
                    dwStorage.DelFlag = 0;
                    dwStorage.UnitAmount = storeParam.UnitAmount;
                    dwStorage.UnitID = storeParam.UnitID;
                    dwStorage.UnitName = storeParam.UnitName;
                    dwStorage.Amount = storeParam.Amount;
                    dwStorage.PackUnit = storeParam.PackUnit;
                    dwStorage.UpperLimit = 0;
                    dwStorage.LowerLimit = 0;
                    dwStorage.LocationID = 0;
                    dwStorage.Place = string.Empty;
                    dwStorage.RegTime = System.DateTime.Now;
                    dwStorage.LastStockPrice = 0;
                    dwStorage.save();
                    storeParam.StorageId = dwStorage.StorageID;

                    //新建批次记录
                    DS_Batch dwBatch = NewObject<DS_Batch>();
                    dwBatch.StorageID = dwStorage.StorageID;
                    dwBatch.BatchAmount = storeParam.Amount;
                    dwBatch.BatchNO = storeParam.BatchNO;
                    dwBatch.ValidityTime = storeParam.ValidityTime;
                    dwBatch.DelFlag = 0;
                    dwBatch.DeptID = storeParam.DeptID;
                    dwBatch.DrugID = storeParam.DrugID;
                    dwBatch.DelFlag = 0;
                    dwBatch.InstoreTime = dwStorage.RegTime;
                    dwBatch.UnitAmount = storeParam.PackAmount;
                    dwBatch.UnitID = storeParam.UnitID;
                    dwBatch.PackUnit = storeParam.PackUnit;
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
                    storeResult.UnitAmount = storeParam.PackAmount;
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
        /// <param name="retAllBatch">true返回所有批次，返回</param>
        /// <returns>库存处理结果</returns>
        public DGStoreResult AddStoreAuto(StoreParam storeParam, bool retAllBatch)
        {
            DGStoreResult storeResult = new DGStoreResult();
            if (storeParam != null)
            {
                if (storeParam.Amount < 0)
                {
                    storeParam.Amount = Math.Abs(storeParam.Amount);
                }

                DS_Storage storage = NewDao<IDSDao>().GetStorageInfo(storeParam.DeptID, storeParam.DrugID);
                if (storage != null)
                {
                    if (!retAllBatch)
                    {
                        //退药模式，返回最近入库批次
                        //获取该药品最近入库批次,将药品加到该批次上
                        DS_Batch latelyBatch = NewDao<IDSDao>().GetLatelyBatchInfo(storeParam.DeptID, storeParam.DrugID);
                        if (latelyBatch != null)
                        {
                            //增加药品库存
                            storage.Amount += storeParam.Amount;
                            int addStorageRtn = storage.save();

                            //增加药品批次库存
                            bool addBatchRtn = NewDao<IDSDao>().UpdateBatchAmount(latelyBatch.DeptID, latelyBatch.DrugID, latelyBatch.BatchNO, storeParam.Amount, 0, 1);

                            //如果批次数量加退货数量大于0
                            if (addStorageRtn >= 0 && addBatchRtn == true)
                            {
                                //添加成功
                                DGBatchAllot batchAllot = new DGBatchAllot();
                                batchAllot.BatchNO = latelyBatch.BatchNO;
                                batchAllot.StoreAmount = latelyBatch.BatchAmount + storeParam.Amount;
                                batchAllot.StorageID = storage.StorageID;
                                batchAllot.RetailPrice = latelyBatch.RetailPrice;
                                batchAllot.StockPrice = latelyBatch.StockPrice;
                                batchAllot.DispAmount = storeParam.Amount;
                                batchAllot.UnitAmount = latelyBatch.UnitAmount;
                                batchAllot.DispRetailFee = batchAllot.CalFee(storeParam.RetailPrice, latelyBatch.UnitAmount, storeParam.Amount);
                                batchAllot.DispStockFee = batchAllot.CalFee(storeParam.StockPrice, latelyBatch.UnitAmount, storeParam.Amount);
                                storeResult.BatchAllotList = new List<DGBatchAllot>();
                                storeResult.BatchAllotList.Add(batchAllot);
                                storeResult.StoreAmount = storage.Amount + storeParam.Amount;
                                storeResult.UnitAmount = 1;
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
                    }
                    else 
                    {
                        //盘点模式返回所有批次
                        decimal profitAmount = storeParam.FactAmount - storeParam.ActAmount;

                        //更新药品库存
                        storage.Amount += profitAmount;
                        int addStorageRtn = storage.save();

                        //更新有效库存
                        if (profitAmount != 0)
                        {
                            UpdateValidStore(storeParam.DrugID, storeParam.DeptID, profitAmount);
                        }

                        //取得所有批次
                        DataTable dtBatch = NewDao<IDSDao>().GetStorageBatch(storage.StorageID);
                        decimal tempAmount = storeParam.FactAmount;
                        storeResult.BatchAllotList = new List<DGBatchAllot>();
                        storeResult.StoreAmount = storage.Amount;
                        storeResult.StorageID = storage.StorageID;
                        for (int i = dtBatch.Rows.Count - 1; i >= 0; i--)
                        {
                            DGBatchAllot batchAllot = new DGBatchAllot();
                            batchAllot.BatchNO = dtBatch.Rows[i]["BatchNO"].ToString();
                            batchAllot.RetailPrice = Convert.ToDecimal(dtBatch.Rows[i]["RetailPrice"]);
                            batchAllot.StockPrice = Convert.ToDecimal(dtBatch.Rows[i]["StockPrice"]);

                            batchAllot.ValidityDate = Convert.ToDateTime(dtBatch.Rows[i]["ValidityTime"]);
                            batchAllot.StorageID = storage.StorageID;
                            batchAllot.DispAmount = storeParam.Amount;
                            batchAllot.UnitAmount = Convert.ToInt32(dtBatch.Rows[i]["UnitAmount"]);

                            decimal batchAmount = Convert.ToDecimal(dtBatch.Rows[i]["BatchAmount"]);
                            decimal retailPrice = Convert.ToDecimal(dtBatch.Rows[i]["RetailPrice"]);
                            decimal stockPrice = Convert.ToDecimal(dtBatch.Rows[i]["StockPrice"]);//盘点按照批次库存价格调整
                            decimal unitAmount = Convert.ToDecimal(dtBatch.Rows[i]["UnitAmount"]);

                            decimal factAmount = 0;
                            decimal factStockFee = 0;
                            decimal factRetailFee = 0;
                            decimal actAmount = 0;
                            decimal actStockFee = 0;
                            decimal actRetailFee = 0;
                            if (tempAmount >= batchAmount)
                            {
                                //按照批次数计算金额
                                if (profitAmount > 0 && i == dtBatch.Rows.Count - 1)
                                {
                                    //盘盈加入到最近入库批次
                                    factAmount = batchAmount + profitAmount;
                                    factStockFee = batchAllot.CalFee(stockPrice, unitAmount, factAmount);
                                    factRetailFee = batchAllot.CalFee(retailPrice, unitAmount, factAmount);
                                    actAmount = batchAmount;
                                    actStockFee = batchAllot.CalFee(stockPrice, unitAmount, actAmount);
                                    actRetailFee = batchAllot.CalFee(retailPrice, unitAmount, actAmount);
                                    batchAllot.StoreAmount = factAmount;

                                    //更改批次库存
                                    //增加药品批次库存
                                    bool addBatchRtn = NewDao<IDSDao>().UpdateBatchAmount(storeParam.DeptID, storeParam.DrugID, dtBatch.Rows[i]["BatchNO"].ToString(), profitAmount, 0, 1);
                                    if (!addBatchRtn)
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
                                    factAmount = batchAmount;
                                    factStockFee = batchAllot.CalFee(stockPrice, unitAmount, factAmount);
                                    factRetailFee = batchAllot.CalFee(retailPrice, unitAmount, factAmount);
                                    actAmount = batchAmount;
                                    actStockFee = factStockFee;
                                    actRetailFee = factRetailFee;
                                    batchAllot.StoreAmount = factAmount;
                                }

                                tempAmount -= batchAmount;
                            }
                            else
                            {
                                if (tempAmount > 0)
                                {
                                    //按照剩余数算
                                    factAmount = tempAmount;
                                    factStockFee = batchAllot.CalFee(stockPrice, unitAmount, factAmount);
                                    factRetailFee = batchAllot.CalFee(retailPrice, unitAmount, factAmount);
                                    actAmount = batchAmount;
                                    actStockFee = batchAllot.CalFee(stockPrice, unitAmount, actAmount);
                                    actRetailFee = batchAllot.CalFee(retailPrice, unitAmount, actAmount);
                                    batchAllot.StoreAmount = tempAmount;
                                }
                                else
                                {
                                    factAmount = 0;
                                    factStockFee = batchAllot.CalFee(stockPrice, unitAmount, factAmount);
                                    factRetailFee = batchAllot.CalFee(retailPrice, unitAmount, factAmount);
                                    actAmount = batchAmount;
                                    actStockFee = batchAllot.CalFee(stockPrice, unitAmount, actAmount);
                                    actRetailFee = batchAllot.CalFee(retailPrice, unitAmount, actAmount);
                                    batchAllot.StoreAmount = 0;
                                }

                                //增加药品批次库存
                                bool addBatchRtn = NewDao<IDSDao>().UpdateBatchAmount(storeParam.DeptID, storeParam.DrugID, dtBatch.Rows[i]["BatchNO"].ToString(), -(batchAmount - tempAmount), 0, 1);
                                if (!addBatchRtn)
                                {
                                    storeResult.Result = 1;
                                    storeResult.StoreAmount = storage.Amount;
                                    storeResult.UnitAmount = 1;
                                    storeResult.StorageID = storage.StorageID;
                                    return storeResult;
                                }

                                tempAmount = 0;
                            }

                            batchAllot.FactAmount = factAmount;
                            batchAllot.FactStockFee = factStockFee;
                            batchAllot.FactRetailFee = factRetailFee;
                            batchAllot.ActAmount = actAmount;
                            batchAllot.ActStockFee = actStockFee;
                            batchAllot.ActRetailFee = actRetailFee;
                            storeResult.BatchAllotList.Add(batchAllot);
                        }
                    }
                }
            }

            return storeResult;
        }

        /// <summary>
        /// 清空库位
        /// </summary>
        /// <param name="locationID">库位ID</param>
        public void ClearLoaction(int locationID)
        {
            NewDao<IDSDao>().ClearLoaction(locationID);
        }

        /// <summary>
        /// 按批次获取药品库存数量
        /// </summary>
        /// <param name="drugID">药品ID</param>
        /// <param name="deptID">库房ID</param>
        /// <param name="batchNO">批次号</param>
        /// <returns>库存数量</returns>
        public decimal GetDrugStoreAmount(int drugID, int deptID, string batchNO)
        {
            return NewDao<IDSDao>().GetDrugStoreAmount(drugID, deptID, batchNO);
        }

        /// <summary>
        /// 获取当前库存数量
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="drugID">药品ID</param>
        /// <returns>药品库存数量</returns>
        public decimal? GetStoreAmount(int deptID, int drugID)
        {
            return NewDao<IDSDao>().GetStoreAmount(deptID, drugID);
        }

        /// <summary>
        /// 查询药品批次信息
        /// </summary>
        /// <param name="storageID">库存ID</param>
        /// <returns>药品批次信息列表</returns>
        public DataTable LoadDrugBatch(int storageID)
        {
            return NewDao<IDSDao>().LoadDrugBatch(storageID);
        }

        /// <summary>
        /// 查询药品库存信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>库存信息表</returns>
        public DataTable LoadDrugStorage(Dictionary<string, string> condition)
        {
            return NewDao<IDSDao>().LoadDrugStorage(condition);
        }

        /// <summary>
        /// 查询药品库存信息(带批次)
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>库存信息表</returns>
        public DataTable LoadDrugStorages(Dictionary<string, string> condition)
        {
            return NewDao<IDSDao>().LoadDrugStorages(condition);
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
        /// 减少库存
        /// </summary>
        /// <param name="storeParam">库存参数</param>
        /// <returns>处理结果</returns>
        public DGStoreResult ReduceStore(StoreParam storeParam)
        {
            DGStoreResult storeResult = new DGStoreResult();
            if (storeParam != null)
            {
                DS_Storage storage = NewDao<IDSDao>().GetStorageInfo(storeParam.DeptID, storeParam.DrugID);
                if (storage != null)
                {
                    DS_Batch batch = NewDao<IDSDao>().GetBatchAmount(storeParam.DeptID, storeParam.DrugID, storeParam.BatchNO);
                    if (batch != null)
                    {
                        //本批次存量 和库存总量都必须大于出库数量
                        if (batch.BatchAmount >= storeParam.Amount && storage.Amount >= storeParam.Amount)
                        {
                            storage.Amount = storage.Amount - storeParam.Amount;
                            storage.save();
                            batch.BatchAmount -= storeParam.Amount;
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
                    storeResult.StoreAmount = storage.Amount;
                    storeResult.UnitAmount = 1;
                    storeResult.StorageID = storage.StorageID;
                    return storeResult;
                }
            }

            return storeResult;
        }

        /// <summary>
        /// 按批次减少库存
        /// </summary>
        /// <param name="drugID">药品ID</param>
        /// <param name="deptID">库房ID</param>
        /// <param name="amount">数量</param>
        /// <param name="batchNO">批次号</param>
        /// <returns>库存处理结果</returns>
        public DGStoreResult ReduceStore(int drugID, int deptID, decimal amount, string batchNO)
        {
            throw new NotImplementedException();
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
        /// 按先进先出减少库存
        /// </summary>
        /// <param name="storeParam">库存参数</param>
        /// <param name="retAllBatch">true返回所有批次，返回</param>
        /// <returns>处理结果</returns>
        public DGStoreResult ReduceStoreAuto(StoreParam storeParam, bool retAllBatch)
        {
            DGStoreResult storeResult = new DGStoreResult();
            if (storeParam != null)
            {
                DS_Storage storage = NewDao<IDSDao>().GetStorageInfo(storeParam.DeptID, storeParam.DrugID);
                if (storage != null)
                {
                    if (storage.Amount < storeParam.Amount)
                    {
                        //药品库存不足
                        //添加失败
                        storeResult.Result = 2;
                        storeResult.StoreAmount = storage.Amount;
                        storeResult.UnitAmount = storage.UnitAmount;
                        storeResult.StorageID = storage.StorageID;
                        return storeResult;
                    }

                    //减少药品库存
                    storage.Amount += -storeParam.Amount;

                    storage.BindDb(this as AbstractBusines);
                    int addStorageRtn = storage.save();

                    //取得所有批次
                    DataTable dtBatch = NewDao<IDSDao>().GetStorageBatch(storage.StorageID);
                    decimal tempAmount = Math.Abs(storeParam.Amount);
                    storeResult.BatchAllotList = new List<DGBatchAllot>();
                    storeResult.StoreAmount = storage.Amount;
                    storeResult.StorageID = storage.StorageID;
                    for (int i = 0; i < dtBatch.Rows.Count; i++)
                    {
                        DGBatchAllot batchAllot = new DGBatchAllot();
                        batchAllot.BatchNO = dtBatch.Rows[i]["BatchNO"].ToString();
                        batchAllot.RetailPrice = Convert.ToDecimal(dtBatch.Rows[i]["RetailPrice"]);
                        batchAllot.StockPrice = Convert.ToDecimal(dtBatch.Rows[i]["StockPrice"]);
                        batchAllot.ValidityDate = Convert.ToDateTime(dtBatch.Rows[i]["ValidityTime"]);
                        batchAllot.StorageID = storage.StorageID;
                        batchAllot.UnitAmount = Convert.ToInt32(dtBatch.Rows[i]["UnitAmount"]);
                        decimal batchAmount = Convert.ToDecimal(dtBatch.Rows[i]["BatchAmount"]);
                        decimal retailPrice = storeParam.RetailPrice;//收费价格
                        decimal stockPrice = storeParam.StockPrice;
                        decimal unitAmount = Convert.ToDecimal(dtBatch.Rows[i]["UnitAmount"]);
                        decimal retailFee = 0;
                        decimal stockFee = 0;
                        decimal dispAmount = 0;
                        if (tempAmount >= batchAmount)
                        {
                            stockFee = batchAllot.CalFee(stockPrice, batchAllot.UnitAmount, batchAmount);
                            retailFee = batchAllot.CalFee(retailPrice, batchAllot.UnitAmount, batchAmount);
                            batchAllot.DispRetailFee = retailFee;
                            batchAllot.DispStockFee = stockFee;
                            batchAllot.StoreAmount = 0;
                            batchAllot.DispAmount = batchAmount;
                            dispAmount = batchAmount;
                            storeResult.BatchAllotList.Add(batchAllot);
                            bool addBatchRtn = NewDao<IDSDao>().UpdateBatchAmount(storeParam.DeptID, storeParam.DrugID, dtBatch.Rows[i]["BatchNO"].ToString(), -dispAmount, 0, 1);
                            if (!addBatchRtn)
                            {
                                storeResult.Result = 1;
                                storeResult.StoreAmount = storage.Amount - storeParam.Amount;
                                storeResult.UnitAmount = 1;
                                storeResult.StorageID = storage.StorageID;
                                return storeResult;
                            }
                        }
                        else
                        {
                            stockFee = batchAllot.CalFee(stockPrice, batchAllot.UnitAmount, tempAmount);
                            retailFee = batchAllot.CalFee(retailPrice, batchAllot.UnitAmount, tempAmount);
                            batchAllot.DispRetailFee = retailFee;
                            batchAllot.DispStockFee = stockFee;
                            batchAllot.StoreAmount = batchAmount - tempAmount;
                            batchAllot.DispAmount = tempAmount;
                            dispAmount = batchAmount;
                            storeResult.BatchAllotList.Add(batchAllot);
                            bool addBatchRtn = NewDao<IDSDao>().UpdateBatchAmount(storeParam.DeptID, storeParam.DrugID, dtBatch.Rows[i]["BatchNO"].ToString(), -tempAmount, 0, 1);
                            if (!addBatchRtn)
                            {
                                storeResult.Result = 1;
                                storeResult.StoreAmount = storage.Amount;
                                storeResult.UnitAmount = 1;
                                storeResult.StorageID = storage.StorageID;
                                return storeResult;
                            }

                            break;
                        }

                        tempAmount -= batchAmount;
                    }
                }
            }

            return storeResult;
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
        /// 加有效库存
        /// </summary>
        /// <param name="storeParam">库存处理参数</param>
        /// <returns>处理结果</returns>
        public DGStoreResult AddValidStore(StoreParam storeParam)
        {
            DGStoreResult result = new DGStoreResult { Result = 1 };//默认

            if (storeParam != null)
            {
                DS_ValidStorage storage = NewDao<IDSDao>().GetValidStoreageInfo(storeParam.DeptID, storeParam.DrugID);
                if (storage == null)
                {
                    if (storeParam.Amount >= 0)
                    {
                        storage = NewObject<DS_ValidStorage>();
                        storage.StorageID = storeParam.StorageId;
                        storage.DeptID = storeParam.DeptID;
                        storage.DrugID = storeParam.DrugID;
                        storage.ValidAmount = storeParam.Amount;
                        int iRtn = storage.save();
                        result.Result = 0;
                    }
                    else
                    {
                        result.Result = 1;
                    }
                }
                else
                {
                    storage.ValidAmount += storeParam.Amount;//不考虑有效库存的正负数
                    int iRtn = storage.save();
                }
            }

            return result;
        }

        /// <summary>
        /// 减有效库存
        /// </summary>
        /// <param name="storeParam">库存参数</param>
        /// <returns>处理结果</returns>
        public DGStoreResult ReduceValidStore(StoreParam storeParam)
        {
            DGStoreResult result = new DGStoreResult { Result = 1 };//默认
            if (storeParam != null)
            {
                DS_ValidStorage storage = NewDao<IDSDao>().GetValidStoreageInfo(storeParam.DeptID, storeParam.DrugID);
                if (storage == null)
                {
                    result.Result = 1;
                }
                else
                {
                    storage.ValidAmount -= storeParam.Amount;
                    storage.save();
                    result.Result = 0;
                }
            }

            return result;
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
            return NewDao<IDSDao>().UpdateValidStore(drugID, deptID, reduceAmount);
        }

        /// <summary>
        /// 更新有效库存
        /// </summary>
        /// <param name="storageID">库存ID</param>
        /// <param name="reduceAmount">数量</param>
        /// <returns>库存处理结果</returns>
        public int UpdateValidStore(int storageID, decimal reduceAmount)
        {
            int iRtn = 0;
            DS_ValidStorage validStorage = (DS_ValidStorage)NewObject<DS_ValidStorage>().getmodel(storageID);
            if (validStorage != null)
            {
                validStorage.ValidAmount = validStorage.ValidAmount + reduceAmount;
                iRtn = validStorage.save();
            }

            return iRtn;
        }
    }
}
