using System;
using System.Collections.Generic;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManger.ObjectModel.Store;
using HIS_Entity.DrugManage;
using HIS_Entity.IPManage;
using HIS_PublicManage.ObjectModel;

namespace HIS_DrugManger.ObjectModel.Bill
{
    /// <summary>
    /// 住院发药处理器
    /// </summary>
    class IPDispBill : AbstractObjectModel
    {
        /// <summary>
        /// 药房库存处理器
        /// </summary>
        private DSStore dsStore = new DSStore();

        /// <summary>
        /// 获取库房当前会计年、月
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="errMsg">错误信息</param>
        /// <param name="actYear">会计年份</param>
        /// <param name="actMonth">会计月份</param>
        /// <returns>库房当前会计年、月</returns>
        public bool GetAccountTime(int deptID, out string errMsg, out int actYear, out int actMonth)
        {
            errMsg = string.Empty;
            DS_BalanceRecord record = NewDao<Dao.IDSDao>().GetMaxBlanceRecord(deptID);
            if (record == null)
            {
                errMsg = "当前库房没有进行初始化月结，请联系管理员";
                actYear = 0;
                actMonth = 0;
                return false;
            }
            else
            {
                if (System.DateTime.Now >= record.EndTime)
                {
                    actMonth = record.BalanceMonth == 12 ? 1 : record.BalanceMonth + 1;
                    actYear = record.BalanceMonth == 12 ? record.BalanceYear + 1 : record.BalanceYear;
                    return true;
                }
                else
                {
                    actMonth = record.BalanceMonth;
                    actYear = record.BalanceYear;
                }

                return true;
            }
        }

        /// <summary>
        /// 台账写入--批次发退药数量台账
        /// </summary>
        /// <param name="billHead">发药单表头</param>
        /// <param name="billDetails">发药单明细</param>
        /// <param name="batchAllot">批次</param>
        /// <param name="refundFlag">退药标志</param>
        /// <param name="storeParam">库存处理参数</param>
        public void WriteAccount(DS_IPDispHead billHead, DS_IPDispDetail billDetails, DGBatchAllot batchAllot,int refundFlag, StoreParam storeParam)
        {
            int actYear;
            int actMonth;
            string errMsg;
            if (!GetAccountTime(billHead.ExecDeptID, out errMsg, out actYear, out actMonth))
            {
                throw new Exception(errMsg);
            }

            DS_Account account = NewObject<DS_Account>();
            account.BalanceYear = actYear;
            account.BalanceMonth = actMonth;
            account.AccountType = 0;//药房
            account.BalanceFlag = 0;
            account.BatchNO = billDetails.BatchNO;
            account.BusiType = billHead.BusiType;
            account.CTypeID = billDetails.CTypeID;
            account.DeptID = billDetails.DeptID;
            account.DetailID = billDetails.DispDetailID;
            account.DrugID = billDetails.DrugID;
            account.UnitID = billDetails.UnitID;
            account.UnitName = billDetails.UnitName;
            account.RegTime = DateTime.Now;

            //盘盈 借方
            //盘亏 贷方
            if (refundFlag==1)
            {
                //退药 借方
                account.StockPrice = storeParam.StockPrice;
                account.RetailPrice = storeParam.RetailPrice;
                account.LendAmount = Math.Abs(billDetails.DispAmount);
                account.LendRetailFee = batchAllot.DispRetailFee; 
                account.LendStockFee = batchAllot.DispStockFee;

                account.OverAmount = batchAllot.StoreAmount;
                //account.OverRetailFee = batchAllot.StoreAmount * (batchAllot.RetailPrice / batchAllot.UnitAmount);
                //account.OverStockFee = batchAllot.StoreAmount * (batchAllot.StockPrice / batchAllot.UnitAmount);
                
                //当药品批次价格和实际价格不一致时，退药按实际价格退
                if (storeParam.RetailPrice.Equals(batchAllot.RetailPrice) == false)
                {
                    account.OverRetailFee = batchAllot.StoreAmount * (storeParam.RetailPrice / storeParam.UnitAmount);
                }
                else
                {
                    account.OverRetailFee = batchAllot.StoreAmount * (batchAllot.RetailPrice / batchAllot.UnitAmount);
                }

                if (storeParam.StockPrice.Equals(batchAllot.StockPrice) == false)
                {
                    account.OverStockFee = batchAllot.StoreAmount * (storeParam.StockPrice / storeParam.UnitAmount);
                }
                else
                {
                    account.OverStockFee = batchAllot.StoreAmount * (batchAllot.StockPrice / batchAllot.UnitAmount);
                }
            }
            else 
            {
                //发药 贷方
                account.StockPrice = storeParam.StockPrice;
                account.RetailPrice = storeParam.RetailPrice;
                account.DebitAmount = billDetails.DispAmount;
                account.DebitRetailFee = batchAllot.DispRetailFee;
                account.DebitStockFee = batchAllot.DispStockFee;

                account.OverAmount = batchAllot.StoreAmount;
                //account.OverRetailFee = batchAllot.StoreAmount * (batchAllot.RetailPrice / batchAllot.UnitAmount);
                //account.OverStockFee = batchAllot.StoreAmount * (batchAllot.StockPrice / batchAllot.UnitAmount);
               
                //当发药时价格和实际批次价格不一致时,剩余价格等于加上要发出的数量*调价后的金额减去发出药量的总金额(发出药量是按批次表原有的价格算的)
                if (storeParam.RetailPrice.Equals(batchAllot.RetailPrice) == false)
                {
                    account.OverRetailFee = ((batchAllot.StoreAmount + billDetails.DispAmount) * (batchAllot.RetailPrice / batchAllot.UnitAmount)) - batchAllot.DispRetailFee;
                }
                else
                {
                    account.OverRetailFee = batchAllot.StoreAmount * (batchAllot.RetailPrice / batchAllot.UnitAmount);
                }

                if (storeParam.StockPrice.Equals(batchAllot.StockPrice) == false)
                {
                    account.OverStockFee = ((batchAllot.StoreAmount + billDetails.DispAmount) * (batchAllot.StockPrice / batchAllot.UnitAmount)) - batchAllot.DispStockFee;
                }
                else
                {
                    account.OverStockFee = batchAllot.StoreAmount * (batchAllot.StockPrice / batchAllot.UnitAmount);
                }
            }

            account.save();
        }

        /// <summary>
        /// 台账写入--批次退药药价格差额台账
        /// </summary>
        /// <param name="billHead">发药单表头</param>
        /// <param name="billDetails">发药单明细</param>
        /// <param name="storeParam">库存参数</param>
        /// <param name="batchAllot">批次</param>
        public void WriteAccountRefund(DS_IPDispHead billHead, DS_IPDispDetail billDetails, StoreParam storeParam, DGBatchAllot batchAllot)
        {
            int actYear;
            int actMonth;
            string errMsg;
            if (!GetAccountTime(billHead.ExecDeptID, out errMsg, out actYear, out actMonth))
            {
                throw new Exception(errMsg);
            }

            DS_Account account = NewObject<DS_Account>();
            account.BalanceYear = actYear;
            account.BalanceMonth = actMonth;
            account.AccountType = 3;//药房
            account.BalanceFlag = 0;
            account.BatchNO = billDetails.BatchNO;
            account.BusiType = billHead.BusiType;
            account.CTypeID = billDetails.CTypeID;
            account.DeptID = billDetails.DeptID;
            account.DetailID = billDetails.DispDetailID;
            account.DrugID = billDetails.DrugID;
            account.UnitID = billDetails.UnitID;
            account.UnitName = billDetails.UnitName;
            account.RegTime = DateTime.Now;

            //盘盈 借方
            //盘亏 贷方
            if (storeParam.RetailPrice > billDetails.RetailPrice)
            {
                //该批次大于处方零售价写入 贷方
                account.StockPrice = billDetails.StockPrice;
                account.RetailPrice = billDetails.RetailPrice;
                account.DebitAmount = 0; //Math.Abs(billDetails.DispAmount);
                account.DebitRetailFee = Math.Abs(GetFee(Math.Abs(billDetails.DispAmount), billDetails.UnitAmount, billDetails.RetailPrice, billDetails.Dosage) - GetFee(Math.Abs(billDetails.DispAmount), billDetails.UnitAmount, storeParam.RetailPrice, billDetails.Dosage));
                account.DebitStockFee = Math.Abs(GetFee(Math.Abs(billDetails.DispAmount), billDetails.UnitAmount, billDetails.StockPrice, billDetails.Dosage) - GetFee(Math.Abs(billDetails.DispAmount), billDetails.UnitAmount, storeParam.StockPrice, billDetails.Dosage));

                account.OverAmount = batchAllot.StoreAmount;
                account.OverRetailFee = batchAllot.StoreAmount * (account.RetailPrice / batchAllot.UnitAmount);
                account.OverStockFee = batchAllot.StoreAmount * (account.StockPrice / batchAllot.UnitAmount);
            }
            else 
            {
                //发药 借方
                account.StockPrice = billDetails.StockPrice;
                account.RetailPrice = billDetails.RetailPrice;
                account.LendAmount = 0; //Math.Abs(billDetails.DispAmount);
                account.LendRetailFee = Math.Abs(GetFee(Math.Abs(billDetails.DispAmount), billDetails.UnitAmount, storeParam.RetailPrice, billDetails.Dosage) - GetFee(Math.Abs(billDetails.DispAmount), billDetails.UnitAmount, billDetails.RetailPrice, billDetails.Dosage));
                account.LendStockFee = Math.Abs(GetFee(Math.Abs(billDetails.DispAmount), billDetails.UnitAmount, storeParam.StockPrice, billDetails.Dosage) - GetFee(Math.Abs(billDetails.DispAmount), billDetails.UnitAmount, billDetails.StockPrice, billDetails.Dosage));

                account.OverAmount = batchAllot.StoreAmount;
                account.OverRetailFee = batchAllot.StoreAmount * (account.RetailPrice / batchAllot.UnitAmount);
                account.OverStockFee = batchAllot.StoreAmount * (account.StockPrice / batchAllot.UnitAmount);
            }

            account.save();
        }

        /// <summary>
        /// 台账写入--批次发药价格差额台账
        /// </summary>
        /// <param name="billHead">发药单表头</param>
        /// <param name="billDetails">发药单明细</param>
        /// <param name="storeParam">库存参数</param>
        /// <param name="batchAllot">批次</param>
        public void WriteAccount(DS_IPDispHead billHead, DS_IPDispDetail billDetails, StoreParam storeParam, DGBatchAllot batchAllot)
        {
            int actYear;
            int actMonth;
            string errMsg;
            if (!GetAccountTime(billHead.ExecDeptID, out errMsg, out actYear, out actMonth))
            {
                throw new Exception(errMsg);
            }

            DS_Account account = NewObject<DS_Account>();
            account.BalanceYear = actYear;
            account.BalanceMonth = actMonth;
            account.AccountType = 3;//药房
            account.BalanceFlag = 0;
            account.BatchNO = billDetails.BatchNO;
            account.BusiType = billHead.BusiType;
            account.CTypeID = billDetails.CTypeID;
            account.DeptID = billDetails.DeptID;
            account.DetailID = billDetails.DispDetailID;
            account.DrugID = billDetails.DrugID;
            account.UnitID = billDetails.UnitID;
            account.UnitName = billDetails.UnitName;
            account.RegTime = DateTime.Now;

            //盘盈 借方
            //盘亏 贷方
            if (storeParam.RetailPrice < billDetails.RetailPrice)
            {
                //该批次大于处方零售价写入 贷方
                account.StockPrice = billDetails.StockPrice;
                account.RetailPrice = billDetails.RetailPrice;
                //发生调整时，数量为0，只调价格
                account.DebitAmount = 0; //Math.Abs(billDetails.DispAmount);
                account.DebitRetailFee = GetFee(Math.Abs(billDetails.DispAmount), billDetails.UnitAmount, billDetails.RetailPrice, billDetails.Dosage) - GetFee(Math.Abs(billDetails.DispAmount), billDetails.UnitAmount, storeParam.RetailPrice, billDetails.Dosage);
                account.DebitStockFee = GetFee(Math.Abs(billDetails.DispAmount), billDetails.UnitAmount, billDetails.StockPrice, billDetails.Dosage) - GetFee(Math.Abs(billDetails.DispAmount), billDetails.UnitAmount, storeParam.StockPrice, billDetails.Dosage);

                account.OverAmount = batchAllot.StoreAmount;
                account.OverRetailFee = batchAllot.StoreAmount * (account.RetailPrice / batchAllot.UnitAmount);
                account.OverStockFee = batchAllot.StoreAmount * (account.StockPrice / batchAllot.UnitAmount);
            }
            else 
            {
                //发药 借方
                account.StockPrice = billDetails.StockPrice;
                account.RetailPrice = billDetails.RetailPrice;
                //发生调整时，数量为0，只调价格
                account.LendAmount = 0; //Math.Abs(billDetails.DispAmount);
                account.LendRetailFee = GetFee(Math.Abs(billDetails.DispAmount), billDetails.UnitAmount, storeParam.RetailPrice, billDetails.Dosage) - GetFee(Math.Abs(billDetails.DispAmount), billDetails.UnitAmount, billDetails.RetailPrice, billDetails.Dosage);
                account.LendStockFee = GetFee(Math.Abs(billDetails.DispAmount), billDetails.UnitAmount, storeParam.StockPrice, billDetails.Dosage) - GetFee(Math.Abs(billDetails.DispAmount), billDetails.UnitAmount, billDetails.StockPrice, billDetails.Dosage);

                account.OverAmount = batchAllot.StoreAmount;
                account.OverRetailFee = batchAllot.StoreAmount * (account.RetailPrice / batchAllot.UnitAmount);
                account.OverStockFee = batchAllot.StoreAmount * (account.StockPrice / batchAllot.UnitAmount);
            }

            account.save();
        }

        /// <summary>
        /// 住院统领发药
        /// </summary>
        /// <param name="dispHead">发药单表头</param>
        /// <param name="dispDetail">发药单明细</param>
        /// <returns>返回结果对象</returns>
        public DGBillResult IPDisp(DS_IPDispHead dispHead, List<DS_IPDispDetail> dispDetail)
        {
            //药品单据处理结果
            DGBillResult result = new DGBillResult();
            if (dispHead != null)
            {
                //1.判断药房是否处于盘点状态中
                if (NewObject<DrugStoreManagement>().IsCheckStatus(dispHead.ExecDeptID, 0))
                {
                    throw new Exception("药房当前正在盘点中，不能发药");
                }

                //生成单据号
                string serialNO = NewObject<SerialNumberSource>().GetSerialNumber(SnType.药品, dispHead.ExecDeptID, DGConstant.OP_DS_IPDISPENSE);
                dispHead.BillNO = Convert.ToInt64(serialNO);
                dispHead.DispTime = System.DateTime.Now;
                BindDb(dispHead);
                dispHead.save();

                //发药如果跨批次的化，台账需要记录差额调整记录
                foreach (DS_IPDispDetail ispDetailModel in dispDetail)
                {
                    decimal dispRetailPrice = ispDetailModel.RetailPrice;

                    //5发药明细设置发药头表ID，保存数据在库存处理方法中
                    ispDetailModel.DispHeadID = dispHead.DispHeadID;

                    //6减库存处理
                    StoreParam storeParam = new StoreParam();
                    storeParam.Amount = ispDetailModel.DispAmount;
                    storeParam.BatchNO = ispDetailModel.BatchNO;
                    storeParam.DeptID = ispDetailModel.DeptID;
                    storeParam.DrugID = ispDetailModel.DrugID;
                    storeParam.RetailPrice = ispDetailModel.RetailPrice;
                    storeParam.StockPrice = ispDetailModel.StockPrice;
                    storeParam.UnitID = ispDetailModel.UnitID;
                    storeParam.UnitName = ispDetailModel.UnitName;
                    if (storeParam.Amount > 0)
                    {
                        DGStoreResult storeRtn = NewObject<DSStore>().ReduceStoreAuto(storeParam, true);
                        if (storeRtn.Result != 0)
                        {
                            result.Result = 1;
                            if (storeRtn.Result == 1)
                            {
                                result.LstNotEnough = new List<DGNotEnough>();
                                DGNotEnough notEnough = new DGNotEnough();
                                notEnough.DeptID = ispDetailModel.DeptID;
                                notEnough.DrugID = ispDetailModel.DrugID;
                                notEnough.LackAmount = ispDetailModel.DispAmount - storeRtn.StoreAmount;
                                notEnough.DrugInfo = "药品批次号:" + ispDetailModel.BatchNO.ToString();
                                result.LstNotEnough.Add(notEnough);
                                result.ErrMsg = "【" + notEnough.DrugInfo + "】库存不足";
                            }
                            else
                            {
                                result.ErrMsg = "药品更新库存出错";
                            }

                            return result;
                        }
                        else
                        {
                            //NewObject<DSStore>().UpdateValidStore(ispDetailModel.DrugID, ispDetailModel.DeptID,  - Math.Abs(ispDetailModel.DispAmount));
                            foreach (DGBatchAllot batchAllot in storeRtn.BatchAllotList)
                            {
                                //保存发药明细数据
                                ispDetailModel.DispDetailID = 0;
                                ispDetailModel.DispAmount = batchAllot.DispAmount;
                                if (batchAllot.DispAmount == 0)
                                {
                                    continue;
                                }

                                ispDetailModel.RetailPrice = batchAllot.RetailPrice;
                                ispDetailModel.StockPrice = batchAllot.StockPrice;
                                ispDetailModel.RetailFee = batchAllot.DispRetailFee;
                                ispDetailModel.StockFee = batchAllot.DispStockFee;
                                ispDetailModel.BatchNO = batchAllot.BatchNO;
                                ispDetailModel.RetFlag = 0;
                                BindDb(ispDetailModel);
                                ispDetailModel.save();
                                if (ispDetailModel.RetailPrice != dispRetailPrice)
                                {
                                    //零售价不一致，调整金额差额台账 
                                    WriteAccount(dispHead, ispDetailModel, storeParam, batchAllot);
                                }

                                //写减批次库存台账
                                WriteAccount(dispHead, ispDetailModel, batchAllot,0, storeParam);
                            }
                        }
                    }
                    else
                    {
                        //退药
                        DGStoreResult storeRtn = NewObject<DSStore>().AddStoreAuto(storeParam, false);
                        if (storeRtn.Result != 0)
                        {
                            result.Result = 1;
                            if (storeRtn.Result == 1)
                            {
                                result.ErrMsg = "药品更新库存出错";
                            }

                            return result;
                        }

                        //更新有效库存
                        NewObject<DSStore>().UpdateValidStore(ispDetailModel.DrugID, ispDetailModel.DeptID, Math.Abs(ispDetailModel.DispAmount));
                        foreach (DGBatchAllot batchAllot in storeRtn.BatchAllotList)
                        {
                            //保存发药明细数据
                            ispDetailModel.DispDetailID = 0;
                            ispDetailModel.DispAmount =  - Math.Abs( batchAllot.DispAmount);
                            ispDetailModel.RetailPrice = batchAllot.RetailPrice;
                            ispDetailModel.StockPrice = batchAllot.StockPrice;
                            ispDetailModel.RetailFee = batchAllot.DispRetailFee;
                            ispDetailModel.StockFee = batchAllot.DispStockFee;
                            ispDetailModel.BatchNO = batchAllot.BatchNO;
                            ispDetailModel.RetFlag = 1;
                            BindDb(ispDetailModel);
                            ispDetailModel.save();
                            ispDetailModel.DispAmount = Math.Abs(ispDetailModel.DispAmount);
                            if (ispDetailModel.RetailPrice != dispRetailPrice)
                            {
                                //零售价不一致，调整金额差额台账 
                                WriteAccountRefund(dispHead, ispDetailModel, storeParam, batchAllot);
                            }

                            //写减批次库存台账
                            WriteAccount(dispHead, ispDetailModel, batchAllot, 1, storeParam);
                        }
                    }

                    //回写住院统领单状态
                    IP_DrugBillDetail ipDrugBillDetail = (IP_DrugBillDetail)NewObject<IP_DrugBillDetail>().getmodel(ispDetailModel.MsgDetaillID);
                    ipDrugBillDetail.DispDrugFlag = 1;
                    ipDrugBillDetail.DispHeadID = dispHead.DispHeadID;
                    ipDrugBillDetail.save();

                    //回写住院费用明细单
                    IP_FeeItemRecord feeItemRecord = (IP_FeeItemRecord)NewObject<IP_FeeItemRecord>().getmodel(ipDrugBillDetail.FeeRecordID);
                    feeItemRecord.DrugFlag = 1;
                    feeItemRecord.save();
                }
            }

            //发药成功
            result.Result = 0;
            result.DispHeadID = dispHead.DispHeadID;
            return result;
        }

        /// <summary>
        /// 计算费用
        /// </summary>
        /// <param name="amount">扣减数量</param>
        /// <param name="unitAmount">系数</param>
        /// <param name="price">价格</param>
        /// <param name="num">付数</param>
        /// <returns>计算费用值</returns>
        private decimal GetFee(decimal amount, decimal unitAmount, decimal price, decimal num)
        {
            if (num == 0)
            {
                num = 1;
            }

            decimal base_num = 0;//基本数--转换成包装数还剩多少基本数
            decimal totalFee = 0;
            base_num = Convert.ToInt32(amount) % Convert.ToInt32(unitAmount);
            decimal relation_num = Convert.ToDecimal(unitAmount);
            decimal presAmount = amount;
            decimal pack_num = Convert.ToDecimal((presAmount - base_num) / relation_num);
            totalFee = (pack_num * price + (price / relation_num) * base_num) * num;
            return totalFee;
        }
    }
}
