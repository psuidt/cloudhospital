using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using HIS_Entity.DrugManage;
using HIS_Entity.IPDoctor;
using HIS_Entity.IPManage;
using HIS_Entity.IPNurse;
using HIS_IPNurse.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPNurse.ObjectModel
{
    /// <summary>
    /// 发送算法：分块生成费用 然后foreach组 以组为单位进行事物的判断库存和扣减库存 以及插入数据
    /// </summary>
    public class OrderSendProcess : AbstractObjectModel
    {
        /// <summary>
        /// 正常医嘱口服药结束时间  true：当天24点 false：次日12点
        /// </summary>
        private bool kfDateFlag = true;

        /// <summary>
        /// 结束时间
        /// </summary>
        private DateTime kfEndDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00"));

        /// <summary>
        /// 结束时间1
        /// </summary>
        private DateTime kfEndDate1 = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 12:00:00"));
        
        /// <summary>
        /// 正常医嘱非口服结束时间  1：次日24点 
        /// </summary>
        private DateTime fkFEndDate = Convert.ToDateTime(DateTime.Now.AddDays(2).ToString("yyyy-MM-dd 00:00:00"));
        
        /// <summary>
        /// 项目结束时间  1：次日0点 
        /// </summary>
        private DateTime xmEndDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00"));
        
        /// <summary>
        /// 是否考虑拆零标志 
        /// </summary>
        private bool clFlag = false;
        
        /// <summary>
        /// 用法执行单配置表
        /// </summary>
        private DataTable dtChannel;
        
        /// <summary>
        /// 西药医嘱
        /// </summary>
        private List<IPD_OrderRecord> ipdOrderRecordList;
        
        /// <summary>
        /// 可用项目表
        /// </summary>
        private List<IP_DrugStore> ipItemStore;
        
        /// <summary>
        /// 库存量
        /// </summary>
        private List<IP_DrugStore> ipDrugStoreList;
        
        /// <summary>
        /// 药品集
        /// </summary>
        private DataTable dtDrugs = new DataTable();
        
        /// <summary>
        /// 项目药品实时价格集
        /// </summary>
        private DataTable dtStorePrice = new DataTable();
        
        /// <summary>
        /// 生成的费用清单
        /// </summary>
        private List<IP_FeeItemRecord> ipFeeItemRecordList = new List<IP_FeeItemRecord>();
        
        /// <summary>
        /// 生成的费用执行单
        /// </summary>
        List<IPN_OrderExecBillRecord> ipFeeItemRecordListExcute = new List<IPN_OrderExecBillRecord>();
       
        /// <summary>
        /// 缺药结果集
        /// </summary>
        private List<IP_OrderCheckError> ipOrderCheckError = new List<IP_OrderCheckError>();

        /// <summary>
        /// 操作员ID
        /// </summary>
        private int iEmpId = 0;

        /// <summary>
        /// 发送算法入口
        /// </summary>
        /// <param name="iGroupIDList">GroupID的List GroupID唯一</param>
        /// <param name="endTime">发送结束时间</param>
        /// <param name="iEmpId">操作员ID</param>
        /// <param name="orderCheckError">处理失败的医嘱列表</param>
        /// <param name="sError">错误消息</param>
        /// <returns>true：处理成功</returns>
        public bool AdviceExecute_Entrance(List<int> iGroupIDList, DateTime endTime, int iEmpId, out List<IP_OrderCheckError> orderCheckError, out string sError)
        {
            orderCheckError = new List<IP_OrderCheckError>();
            sError = string.Empty;
            this.iEmpId = iEmpId;
            if (endTime.Date != DateTime.Now.Date)
            {
                kfEndDate = endTime;
                kfEndDate1 = endTime;
                fkFEndDate = endTime;
                xmEndDate = endTime;
            }

            LogHelper("------------------------------------------------------------------------------------");
            LogHelper("1.获取基础数据");

            dtStorePrice = NewDao<IOrderCheckDao>().GetStorePrice();
            dtDrugs = NewDao<IOrderCheckDao>().GetDrugs();
            dtChannel = NewDao<IOrderCheckDao>().GetExecuteBillChannel();
            ipItemStore = NewDao<IOrderCheckDao>().GetItemInfo();
            ipDrugStoreList = NewDao<IOrderCheckDao>().GetDrugStore();
            ipdOrderRecordList = GetIPDOrderByGroupId(iGroupIDList);
            LogHelper("1.获取基础数据完成！获取医嘱组信息");
            //0.根据iGroupIDList获取接口类List
            List<IP_OrderCheck> sourceOrderChecks = GetIPOrderCheckByGroupId(iGroupIDList, endTime);
            LogHelper("2.获取医嘱组信息完成！生成费用清单");
            //1.生成费用清单
            bool b = AdviceExecute_CreatFeeList(sourceOrderChecks, out sError);
            LogHelper("3.生成费用清单完成！开始插入");
            if (b)
            {
                //2.插入费用
                b = AdviceExecute_InsertCost(sourceOrderChecks, out sError);
                if (b)
                {
                    orderCheckError.AddRange(ipOrderCheckError);
                }
            }
            else
            {
            }

            LogHelper("完成！");
            return true;
        }

        /// <summary>
        /// 医嘱发送操作-计算费用清单   
        /// </summary>
        /// <param name="sourceOrderChecks">组号不可重复</param>
        /// <param name="sError">错误消息</param>
        /// <returns>费用明细List-List</returns>
        private bool AdviceExecute_CreatFeeList(List<IP_OrderCheck> sourceOrderChecks, out string sError)
        {
            try
            {
                //1.新建中草药；临嘱项目；出院带药；交病人；正常等List
                List<IP_OrderCheck> noFeeList = new List<IP_OrderCheck>();
                List<IP_OrderCheck> herbalList = new List<IP_OrderCheck>();
                List<IP_OrderCheck> tempItemList = new List<IP_OrderCheck>();
                List<IP_OrderCheck> outTakeList = new List<IP_OrderCheck>();
                List<IP_OrderCheck> gaveList = new List<IP_OrderCheck>();
                List<IP_OrderCheck> normalList = new List<IP_OrderCheck>();

                //2.将源List分别插入到目标List  过滤皮试未通过的皮试医嘱  暂时不考虑  因为发送界面考虑
                //西药需要判断库存？还是材料需要判断库存？
                foreach (IP_OrderCheck orderCheck in sourceOrderChecks)
                {
                    if (orderCheck.ItemType == 5)
                    {
                        //说明性医嘱
                        noFeeList.Add(orderCheck);
                    }
                    else if (orderCheck.ZCYFlag == 1)
                    {
                        //中草药
                        herbalList.Add(orderCheck);
                    }
                    else if (orderCheck.OrderCategory == 1 && (orderCheck.ItemType == 3 || orderCheck.ItemType == 4))
                    {
                        //临嘱 收费或组合项目
                        tempItemList.Add(orderCheck);
                    }
                    else if (orderCheck.OrderType == 3)
                    {
                        //出院带药
                        outTakeList.Add(orderCheck);
                    }
                    else if (orderCheck.OrderType == 1)
                    {
                        //交病人
                        gaveList.Add(orderCheck);
                    }
                    else
                    {
                        //正常
                        normalList.Add(orderCheck);
                    }
                }

                //3.说明性医嘱
                NoFeeHandle(noFeeList);
                //3.处理中草药：中草药 无关频次 只关注付数  中草药不能退药  直接生成一条 数量*付数
                HerbalHandle(herbalList);
                //4.处理临嘱项目：临嘱项目 不关注频次，直接取数量，仅生成一条
                TempItemHandle(tempItemList);
                //5.处理出院带药: 出院带药 无需执行，直接生成一条数据，修改频次为数量即可
                OutTakeHandle(outTakeList);
                //6.处理交病人：交病人 无需执行，直接生成一条数据，修改频次为数量即可
                GaveHandle(gaveList);
                //7.处理正常：
                NormalHandle(normalList);
                sError = string.Empty;
                return true;
            }
            catch (Exception e)
            {
                sError = "1.生成费用数据失败：" + e.Message;
                return false;
            }
        }

        /// <summary>
        /// 插入费用 按组插入
        /// </summary>
        /// <param name="sourceOrderChecks">组号不可重复</param>
        /// <param name="sError">错误消息</param>
        /// <returns>true：处理成功</returns>
        private bool AdviceExecute_InsertCost(List<IP_OrderCheck> sourceOrderChecks, out string sError)
        {
            sError = string.Empty;
            bool b = true;
            //1.插入费用并生成执行单
            try
            {
                //按组判断库存并插入
                foreach (IP_OrderCheck orderCheck in sourceOrderChecks)
                {
                    CommandInsertCost(orderCheck);
                }

                b = true;
            }
            catch (Exception e)
            {
                b = false;
                sError = "2.插入费用数据失败：" + e.Message;
            }

            //2.插入执行单
            try
            {
                //按组判断库存并插入
                foreach (IP_OrderCheck orderCheck in sourceOrderChecks)
                {
                    CommandInsertExcuteList(orderCheck);
                }

                b = true;
            }
            catch
            {
            }

            return b;
        }

        #region 生成费用清单子方法

        /// <summary>
        /// 3处理说明性医嘱
        /// </summary>
        /// <param name="orderChecks">组号列表</param>
        /// <returns>true：处理成功</returns>
        private bool NoFeeHandle(List<IP_OrderCheck> orderChecks)
        {
            try
            {
                bool bStore = true;
                foreach (IP_OrderCheck orderCheck in orderChecks)
                {
                    #region 更新医嘱状态
                    //更新医嘱的执行状态
                    if (bStore)
                    {
                        List<IPD_OrderRecord> orderRecordList = NewObject<IPD_OrderRecord>().getlist<IPD_OrderRecord>(" GroupID='" + orderCheck.GroupID + "' and DeleteFlag=0  ");
                        foreach (IPD_OrderRecord orderRecord in orderRecordList)
                        {
                            orderRecord.OrderStatus = 5;
                            orderRecord.ExecFlag = 1;
                            orderRecord.ExecDate = orderCheck.ECheckTime;
                            orderRecord.ExecNurse = iEmpId;

                            this.BindDb(orderRecord);
                            orderRecord.save();
                        }
                    }
                    #endregion
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 3处理中草药
        /// </summary>
        /// <param name="orderChecks">组号</param>
        /// <returns>true处理成功</returns>
        private bool HerbalHandle(List<IP_OrderCheck> orderChecks)
        {
            try
            {
                foreach (IP_OrderCheck orderCheck in orderChecks)
                {
                    //获取费用生成表
                    List<IP_FeeItemGenerate> feeItemGenerates = GetFeeItemGenerateByGroupId(orderCheck.GroupID);
                    foreach (IP_FeeItemGenerate feeItemGenerate in feeItemGenerates)
                    {
                        //中草药不关注频次  只将数量乘以付数
                        IP_FeeItemRecord feeItemRecord = CreateFee(feeItemGenerate, feeItemGenerate.Amount * feeItemGenerate.DoseAmount, feeItemGenerate.TotalFee * feeItemGenerate.DoseAmount, orderCheck.ECheckTime);
                        //将计算出来的费用加入到清单
                        ipFeeItemRecordList.Add(feeItemRecord);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 4处理临嘱项目
        /// </summary>
        /// <param name="orderChecks">组号列表</param>
        /// <returns>处理成功</returns>
        private bool TempItemHandle(List<IP_OrderCheck> orderChecks)
        {
            try
            {
                foreach (IP_OrderCheck orderCheck in orderChecks)
                {
                    //获取费用生成表
                    List<IP_FeeItemGenerate> feeItemGenerates = GetFeeItemGenerateByGroupId(orderCheck.GroupID);
                    foreach (IP_FeeItemGenerate feeItemGenerate in feeItemGenerates)
                    {
                        //临嘱项目 不关注频次，直接取数量，仅生成一条
                        IP_FeeItemRecord feeItemRecord = CreateFee(feeItemGenerate, feeItemGenerate.Amount, feeItemGenerate.TotalFee, orderCheck.ECheckTime);

                        //将计算出来的费用加入到清单
                        ipFeeItemRecordList.Add(feeItemRecord);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 5处理出院带药
        /// </summary>
        /// <param name="orderChecks">组号</param>
        /// <returns>true处理成功</returns>
        private bool OutTakeHandle(List<IP_OrderCheck> orderChecks)
        {
            try
            {
                foreach (IP_OrderCheck orderCheck in orderChecks)
                {
                    //获取费用生成表
                    List<IP_FeeItemGenerate> feeItemGenerates = GetFeeItemGenerateByGroupId(orderCheck.GroupID);
                    foreach (IP_FeeItemGenerate feeItemGenerate in feeItemGenerates)
                    {
                        int iAmount = NewObject<FrequencyManagement>().GetExecCount(orderCheck.FrenquencyID, orderCheck.BCheckTime, orderCheck.BCheckTime.AddDays(1));
                        // 出院带药 无需执行，直接生成一条数据，修改频次为数量即可
                        IP_FeeItemRecord feeItemRecord = CreateFee(feeItemGenerate, feeItemGenerate.Amount * iAmount, feeItemGenerate.TotalFee * iAmount, orderCheck.ECheckTime);
                        //将计算出来的费用加入到清单
                        ipFeeItemRecordList.Add(feeItemRecord);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 6处理交病人
        /// </summary>
        /// <param name="orderChecks">医嘱信息</param>
        /// <returns>true：处理成功</returns>
        private bool GaveHandle(List<IP_OrderCheck> orderChecks)
        {
            try
            {
                foreach (IP_OrderCheck orderCheck in orderChecks)
                {
                    //获取费用生成表
                    List<IP_FeeItemGenerate> feeItemGenerates = GetFeeItemGenerateByGroupId(orderCheck.GroupID);
                    foreach (IP_FeeItemGenerate feeItemGenerate in feeItemGenerates)
                    {
                        //交病人 无需执行，直接生成一条数据，修改频次为数量即可
                        int iAmount = NewObject<FrequencyManagement>().GetExecCount(orderCheck.FrenquencyID, orderCheck.BCheckTime, orderCheck.BCheckTime.AddDays(1));
                        IP_FeeItemRecord feeItemRecord = CreateFee(feeItemGenerate, feeItemGenerate.Amount * iAmount, feeItemGenerate.TotalFee * iAmount, orderCheck.ECheckTime);
                        //将计算出来的费用加入到清单
                        ipFeeItemRecordList.Add(feeItemRecord);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 7处理正常
        /// </summary>
        /// <param name="orderChecks">医嘱信息</param>
        /// <returns>true处理成功</returns>
        private bool NormalHandle(List<IP_OrderCheck> orderChecks)
        {
            try
            {
                //0.处理口服的结束时间
                //1.处理取整方式
                //2.处理周期生成
                foreach (IP_OrderCheck orderCheck in orderChecks)
                {
                    //如果是非停嘱且为口服药则停止时间为第二天的中午
                    if (orderCheck.OrderStatus != 4 && orderCheck.OrderCategory == 0)
                    {
                        string sFilter = " ChannelID=" + orderCheck.ChannelID + " And BillName like '%口服%'";
                        if (dtChannel.Select(sFilter).Length > 0)
                        {
                            orderCheck.ECheckTime = kfDateFlag ? kfEndDate : kfEndDate1;
                        }
                        else
                        {
                            if (orderCheck.ItemType == 3 || orderCheck.ItemType == 4)
                            {
                                orderCheck.ECheckTime = xmEndDate;
                            }
                            else
                            {
                                orderCheck.ECheckTime = fkFEndDate;
                            }
                        }
                    }
                    //获取费用次数
                    List<FrequencyTiming> frequencyTimingList = NewObject<FrequencyManagement>().GetTiming(
                        orderCheck.FrenquencyID, 
                        orderCheck.BCheckTime, 
                        orderCheck.ECheckTime, 
                        orderCheck.FirstNum, 
                        orderCheck.TeminalNum);

                    if (frequencyTimingList.Count <= 0 && orderCheck.TeminalNum < 0)
                    {
                        IP_OrderCheckError orderCheckError = new IP_OrderCheckError();
                        orderCheckError.BedNo = orderCheck.BedNo;
                        orderCheckError.ErrorMessage = orderCheck.GroupID + " 组号频次未维护！";
                        orderCheckError.GroupID = orderCheck.GroupID;
                        orderCheckError.NeedAmount = 0;
                        orderCheckError.OrderID = 0;
                        orderCheckError.OrderName = string.Empty;
                        ipOrderCheckError.Add(orderCheckError);
                        continue;
                    }
                    //获取费用生成表
                    List<IP_FeeItemGenerate> feeItemGenerates = GetFeeItemGenerateByGroupId(orderCheck.GroupID);

                    //1.处理首次 将Group转到正常医嘱
                    //如果首末次小于频次次数，直接取频次的前几次；如果大于频次次数，取所有频次时间；多余的取最后一个时间然后+1s一次 频次算法已处理
                    //2.处理末次
                    if (orderCheck.OrderStatus == 4)
                    {
                        #region 如果是末次 则先判断是否已生成费用 将已生成的红冲
                        //如果开始时间在停嘱时间之前，直接红冲开始时间之后的所有费用，再按频次时间点生成
                        if (orderCheck.BCheckTime < orderCheck.ECheckTime)
                        {
                            List<IP_FeeItemRecord> feeItemRecordNeedRedList = NewObject<IP_FeeItemRecord>().getlist<IP_FeeItemRecord>("  GroupID = " + orderCheck.GroupID + " and PresDate>'" + orderCheck.BCheckTime + "'");
                            #region 红冲
                            CreateFeeRed(feeItemRecordNeedRedList);
                            #endregion
                        }
                        else
                        {
                            //如果开始时间在停嘱时间之后，频次时间点返回为空，直接红冲停嘱当天以及之后的所有费用，再直接生成末次的条数，其时间为停嘱时间依次减少1S
                            //查询停嘱当天以及之后的所有
                            List<IP_FeeItemRecord> feeItemRecordNeedRedList = NewObject<IP_FeeItemRecord>().getlist<IP_FeeItemRecord>("  GroupID = " + orderCheck.GroupID + " and PresDate>'" + orderCheck.ECheckTime.ToString("yyyy-MM-dd 00:00:00") + "'");
                            #region 红冲
                            CreateFeeRed(feeItemRecordNeedRedList);
                            #endregion

                            //获取费用生成表补录末次
                            List<IP_FeeItemGenerate> feeItemGeneratesbl = GetFeeItemGenerateByGroupId(orderCheck.GroupID);
                            if (orderCheck.ItemType != 1)
                            {
                                for (int i = 0; i < orderCheck.TeminalNum; i++)
                                {
                                    foreach (IP_FeeItemGenerate feeItemGenerate in feeItemGeneratesbl)
                                    {
                                        IP_FeeItemRecord feeItemRecord = CreateFee(feeItemGenerate, feeItemGenerate.Amount, feeItemGenerate.TotalFee, orderCheck.ECheckTime.AddSeconds(i * -1));
                                        //将计算出来的费用加入到清单
                                        ipFeeItemRecordList.Add(feeItemRecord);
                                    }
                                }
                            }
                            else
                            {
                                //0.先把非医嘱本身的费用插入
                                for (int i = 0; i < orderCheck.TeminalNum; i++)
                                {
                                    foreach (IP_FeeItemGenerate feeItemGenerate in feeItemGenerates)
                                    {
                                        //医嘱本身的费用不插
                                        if (feeItemGenerate.FeeSource == 0)
                                        {
                                            continue;
                                        }

                                        IP_FeeItemRecord feeItemRecord = CreateFee(
                                            feeItemGenerate, 
                                            feeItemGenerate.Amount, 
                                            feeItemGenerate.TotalFee, 
                                            orderCheck.ECheckTime.AddSeconds(i * -1));
                                        //将计算出来的费用加入到清单
                                        ipFeeItemRecordList.Add(feeItemRecord);
                                    }
                                }

                                //1.获取组号的医嘱信息
                                List<IPD_OrderRecord> orderRecordList = ipdOrderRecordList.FindAll(x => x.GroupID == orderCheck.GroupID);
                                //2.计算医嘱费用
                                List<IP_FeeItemRecord> feeItemRecordListByOrder = CreateFeeByOrderTeminalNum(orderRecordList, feeItemGenerates);
                                //3.加到费用清单
                                ipFeeItemRecordList.AddRange(feeItemRecordListByOrder);
                            }
                        }
                        #endregion
                    }
                    else if (orderCheck.ItemType == 1)
                    {
                        //药品需要判断总量取整（用量）和自备 故需要按医嘱判断插入  其他的仅用生成周期处理
                        //0.先把非医嘱本身的费用插入
                        foreach (FrequencyTiming frequencyTiming in frequencyTimingList)
                        {
                            foreach (IP_FeeItemGenerate feeItemGenerate in feeItemGenerates)
                            {
                                //非首次的周期费用不插 医嘱本身的费用不插
                                if ((!frequencyTiming.PeriodFlag && feeItemGenerate.CalCostMode == 1) || feeItemGenerate.FeeSource == 0)
                                {
                                    continue;
                                }

                                IP_FeeItemRecord feeItemRecord = CreateFee(feeItemGenerate, feeItemGenerate.Amount, feeItemGenerate.TotalFee, frequencyTiming.executeTime);
                                //将计算出来的费用加入到清单
                                ipFeeItemRecordList.Add(feeItemRecord);
                            }
                        }
                        
                        //1.获取组号的医嘱信息
                        List<IPD_OrderRecord> orderRecordList = ipdOrderRecordList.FindAll(x => x.GroupID == orderCheck.GroupID);
                        //2.计算医嘱费用
                        List<IP_FeeItemRecord> feeItemRecordListByOrder = CreateFeeByOrder(orderRecordList, frequencyTimingList, feeItemGenerates);
                        //3.加到费用清单
                        ipFeeItemRecordList.AddRange(feeItemRecordListByOrder);
                    }
                    else
                    {
                        foreach (FrequencyTiming frequencyTiming in frequencyTimingList)
                        {
                            foreach (IP_FeeItemGenerate feeItemGenerate in feeItemGenerates)
                            {
                                //非首次的周期费用不插
                                if (!frequencyTiming.PeriodFlag && feeItemGenerate.CalCostMode == 1)
                                {
                                    continue;
                                }

                                IP_FeeItemRecord feeItemRecord = CreateFee(
                                    feeItemGenerate, 
                                    feeItemGenerate.Amount, 
                                    feeItemGenerate.TotalFee, 
                                    frequencyTiming.executeTime);
                                //将计算出来的费用加入到清单
                                ipFeeItemRecordList.Add(feeItemRecord);
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        /// <summary>
        /// 按组生成费用和执行单并更新医嘱
        /// </summary>
        /// <param name="orderCheck">医嘱信息</param>
        /// <returns>true生成成功</returns>
        [AOP(typeof(AopTransaction))]
        private bool CommandInsertCost(IP_OrderCheck orderCheck)
        {
            try
            {
                bool bStore = true;
                //需要考虑停嘱无需生成费用的
                if (ipFeeItemRecordList.Count > 0 || orderCheck.OrderStatus == 4)
                {
                    //找出该组
                    List<IP_FeeItemRecord> feeItemRecordListByG = ipFeeItemRecordList.FindAll(x => x.GroupID == orderCheck.GroupID);

                    #region 判断库存并扣减
                    if (orderCheck.ItemType == 1)
                    {
                        //判断库存
                        bStore = AdviceExecute_StockJudge(orderCheck, feeItemRecordListByG.FindAll(x => x.FeeClass == 1));
                        if (bStore)
                        {
                            //扣减库存  
                            bStore = AdviceExecute_StockBalance(orderCheck, feeItemRecordListByG.FindAll(x => x.FeeClass == 1));
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (orderCheck.ItemType == 3 || orderCheck.ItemType == 4)
                    {
                        //判断项目停用
                        bStore = AdviceExecute_ItemJudge(orderCheck, feeItemRecordListByG);
                    }
                    #endregion

                    #region 插入费用 并生成执行单数据
                    if (bStore)
                    {
                        foreach (IP_FeeItemRecord i in feeItemRecordListByG)
                        {
                            int iFeeSoure = i.FeeSource;
                            i.FeeSource = 1;
                            //插入
                            this.BindDb(i);
                            i.save();

                            //只添加医嘱本身
                            if (iFeeSoure == 0)
                            {
                                //加入费用执行单
                                CreateFeeExcuteList(i);
                            }
                        }
                    }
                    #endregion

                    #region 更新医嘱状态
                    //更新医嘱的执行状态
                    if (bStore)
                    {
                        NewDao<IOrderCheckDao>().UpdateOrderSend(orderCheck.GroupID, orderCheck.ECheckTime, iEmpId);
                    }
                    #endregion
                }

                return bStore;
            }
            catch (Exception ex)
            {
                IP_OrderCheckError orderCheckError = new IP_OrderCheckError();
                orderCheckError.BedNo = orderCheck.BedNo;
                orderCheckError.GroupID = orderCheck.GroupID;
                orderCheckError.OrderID = 0;
                orderCheckError.OrderName = string.Empty;
                orderCheckError.NeedAmount = 0;
                orderCheckError.ErrorMessage = ex.Message;
                ipOrderCheckError.Add(orderCheckError);
                throw ex;
            }
        }

        /// <summary>
        /// 按组插入执行单
        /// </summary>
        /// <param name="orderCheck">医嘱信息</param>
        [AOP(typeof(AopTransaction))]
        private void CommandInsertExcuteList(IP_OrderCheck orderCheck)
        {
            try
            {
                if (ipFeeItemRecordListExcute.Count > 0)
                {
                    //找出该组
                    List<IPN_OrderExecBillRecord> orderExecBillRecordByG = ipFeeItemRecordListExcute.FindAll(x => x.GroupID == orderCheck.GroupID);

                    #region 插入执行单 
                    foreach (IPN_OrderExecBillRecord orderExecBillRecord in orderExecBillRecordByG)
                    {
                        //插入
                        this.BindDb(orderExecBillRecord);
                        int i = orderExecBillRecord.save();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                IP_OrderCheckError orderCheckError = new IP_OrderCheckError();
                orderCheckError.BedNo = orderCheck.BedNo;
                orderCheckError.GroupID = orderCheck.GroupID;
                orderCheckError.OrderID = 0;
                orderCheckError.OrderName = string.Empty;
                orderCheckError.NeedAmount = 0;
                orderCheckError.ErrorMessage = ex.Message;
                ipOrderCheckError.Add(orderCheckError);
                throw ex;
            }
        }

        #region 库存和停用判断  扣减库存（针对长嘱）

        /// <summary>
        /// 判断项目是否停用
        /// </summary>
        /// <param name="orderCheck">医嘱信息</param>
        /// <param name="feeItemRecordListJ">医嘱关联费用数据</param>
        /// <returns>false：停用</returns>
        private bool AdviceExecute_ItemJudge(IP_OrderCheck orderCheck, List<IP_FeeItemRecord> feeItemRecordListJ)
        {
            foreach (IP_FeeItemRecord feeItemRecord in feeItemRecordListJ)
            {
                IP_DrugStore drugStoreResult = ipItemStore.Find(
                                        delegate (IP_DrugStore drugStore)
                                        {
                                            return drugStore.DrugID == feeItemRecord.ItemID;
                                        });

                if (drugStoreResult == null)
                {
                    IP_OrderCheckError orderCheckError = new IP_OrderCheckError();
                    orderCheckError.BedNo = orderCheck.BedNo;
                    orderCheckError.GroupID = orderCheck.GroupID;
                    orderCheckError.OrderID = 0;
                    orderCheckError.OrderName = feeItemRecord.ItemName;
                    orderCheckError.ErrorMessage = orderCheckError.OrderName + "项目停用";
                    ipOrderCheckError.Add(orderCheckError);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 判断库存 以组为单位
        /// </summary>
        /// <param name="orderCheck">医嘱信息</param>
        /// <param name="feeItemRecordListJ">关联的费用信息</param>
        /// <returns>true通过</returns>
        private bool AdviceExecute_StockJudge(IP_OrderCheck orderCheck, List<IP_FeeItemRecord> feeItemRecordListJ)
        {
            //2.1药品按组及执行科室计算所需库存结果集   过滤掉红冲的和退费的
            var q = from item in feeItemRecordListJ
                    where item.FeeClass == 1 && item.RecordFlag == 0
                    group item by new { item.GroupID, item.ItemID, item.ItemName, item.ExecDeptID } into g
                    select new
                    {
                        GroupID = g.Key.GroupID,
                        ItemID = g.Key.ItemID,
                        ItemName = g.Key.ItemName,
                        ExecDeptID = g.Key.ExecDeptID,
                        NeedNum = g.Sum(x => x.Amount)
                    };
            //2.2遍历所需库存结果集
            foreach (var v in q)
            {
                IP_DrugStore drugStoreResult = ipDrugStoreList.Find(
                                    delegate (IP_DrugStore drugStore)
                                    {
                                        return drugStore.DrugID == v.ItemID && drugStore.ExecDeptId == v.ExecDeptID;
                                    });

                //2.3 只有有库存切库存大于所需才通过
                if (drugStoreResult != null && drugStoreResult.StoreAmount > v.NeedNum)
                {
                    continue;
                }
                else
                {
                    IP_OrderCheckError orderCheckError = new IP_OrderCheckError();
                    orderCheckError.BedNo = orderCheck.BedNo;
                    orderCheckError.GroupID = orderCheck.GroupID;
                    orderCheckError.OrderID = 0;
                    orderCheckError.OrderName = v.ItemName;
                    orderCheckError.NeedAmount = v.NeedNum;
                    orderCheckError.ErrorMessage = orderCheckError.OrderName + "缺药或药品停用";
                    ipOrderCheckError.Add(orderCheckError);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 扣减库存  这里需要事务
        /// </summary>
        /// <param name="orderCheck">医嘱信息</param>
        /// <param name="feeItemRecordListB">关联的费用信息</param>
        /// <returns>true扣减成功</returns>
        private bool AdviceExecute_StockBalance(IP_OrderCheck orderCheck, List<IP_FeeItemRecord> feeItemRecordListB)
        {
            try
            {
                foreach (IP_FeeItemRecord feeItemRecord in feeItemRecordListB)
                {
                    //退费的不需处理
                    if (feeItemRecord.RecordFlag != 2)
                    {
                        List<DS_ValidStorage> validStorage = NewObject<DS_ValidStorage>().getlist<DS_ValidStorage>("DrugID =" + feeItemRecord.ItemID + " AND DeptID=" + feeItemRecord.ExecDeptID);
                        if (feeItemRecord.RecordFlag == 0)
                        {
                            if (validStorage.Count > 0 && validStorage[0].ValidAmount > feeItemRecord.Amount)
                            {
                                validStorage[0].ValidAmount -= feeItemRecord.Amount;
                                this.BindDb(validStorage[0]);
                                validStorage[0].save();
                            }
                            else
                            {
                                //回滚全部
                                IP_OrderCheckError orderCheckError = new IP_OrderCheckError();
                                orderCheckError.BedNo = orderCheck.BedNo;
                                orderCheckError.GroupID = orderCheck.GroupID;
                                orderCheckError.OrderID = feeItemRecord.OrderID;
                                orderCheckError.OrderName = feeItemRecord.ItemName;
                                orderCheckError.NeedAmount = feeItemRecord.Amount;
                                orderCheckError.ErrorMessage = orderCheckError.OrderName + "实际库存不足！";
                                ipOrderCheckError.Add(orderCheckError);
                                throw new Exception("实际库存不足！");
                            }
                        }
                        else
                        {
                            if (validStorage.Count > 0)
                            {
                                validStorage[0].ValidAmount += feeItemRecord.Amount;
                                this.BindDb(validStorage[0]);
                                validStorage[0].save();
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                IP_OrderCheckError orderCheckError = new IP_OrderCheckError();
                orderCheckError.BedNo = orderCheck.BedNo;
                orderCheckError.GroupID = orderCheck.GroupID;
                orderCheckError.OrderID = 0;
                orderCheckError.OrderName = string.Empty;
                orderCheckError.NeedAmount = 0;
                orderCheckError.ErrorMessage = ex.Message;
                ipOrderCheckError.Add(orderCheckError);
                throw ex;
            }
        }
        #endregion

        #region 获取费用生成信息和医嘱信息 并按执行科室更新最新价格

        /// <summary>
        /// 根据组号检查医嘱
        /// </summary>
        /// <param name="iGroupIDList">组号列表</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>医嘱信息</returns>
        private List<IP_OrderCheck> GetIPOrderCheckByGroupId(List<int> iGroupIDList, DateTime endTime)
        {
            List<IP_OrderCheck> feeIPOrderCheckList = new List<IP_OrderCheck>();
            DataTable dt = NewDao<IOrderCheckDao>().GetIPOrderCheckByGroupId(iGroupIDList, endTime);
            foreach (DataRow dr in dt.Rows)
            {
                IP_OrderCheck orderCheck = new IP_OrderCheck();
                orderCheck.BedNo = dr["BedNo"].ToString();
                orderCheck.GroupID = Convert.ToInt32(dr["GroupID"]);
                orderCheck.OrderCategory = Convert.ToInt32(dr["OrderCategory"]);
                orderCheck.OrderStatus = Convert.ToInt32(dr["OrderStatus"]);
                orderCheck.OrderType = Convert.ToInt32(dr["OrderType"]);
                orderCheck.ZCYFlag = Convert.ToInt32(dr["ZCYFlag"]);
                orderCheck.ItemType = Convert.ToInt32(dr["ItemType"]);
                orderCheck.ChannelID = Convert.ToInt32(dr["ChannelID"]);
                orderCheck.FrenquencyID = Convert.ToInt32(dr["FrenquencyID"]);
                orderCheck.ExecFlag = Convert.ToInt32(dr["ExecFlag"]);

                if (orderCheck.OrderCategory == 1)
                {
                    orderCheck.FirstNum = -1;
                    orderCheck.TeminalNum = -1;
                    //新医嘱: OrderBdate + 1s
                    orderCheck.BCheckTime = Convert.ToDateTime(dr["OrderBdate"]).AddSeconds(1);
                    //结束时间 OrderStatus=4-转抄停嘱:停嘱时间 EOrderDate; OrderStatus=2-已经转抄:结束发送时间-界面选择
                    orderCheck.ECheckTime = Convert.ToDateTime(dr["OrderBdate"]).AddDays(1);
                }
                else
                {
                    orderCheck.FirstNum = orderCheck.ExecFlag == 1 ? -1 : Convert.ToInt32(dr["FirstNum"]);
                    orderCheck.TeminalNum = orderCheck.OrderStatus == 4 ? Convert.ToInt32(dr["TeminalNum"]) : -1;
                    //开始时间 ExecFlag = 1发送过: 上次结束时间 ExecDate+1s; ExecFlag = 0新医嘱: OrderBdate + 1s
                    orderCheck.BCheckTime = orderCheck.ExecFlag == 1 ? Convert.ToDateTime(dr["ExecDate"]).AddSeconds(1) : Convert.ToDateTime(dr["OrderBdate"]).AddSeconds(1);
                    //结束时间 OrderStatus=4-转抄停嘱:停嘱时间 EOrderDate; OrderStatus=2-已经转抄:结束发送时间-界面选择
                    orderCheck.ECheckTime = orderCheck.OrderStatus == 4 ? Convert.ToDateTime(dr["EOrderDate"]).AddSeconds(1) : endTime.AddSeconds(1);
                }

                feeIPOrderCheckList.Add(orderCheck);
            }

            return feeIPOrderCheckList;
        }

        /// <summary>
        /// 通过GroupId获取费用生成信息并更新最新价格  计费模式，0按频次 1按周期
        /// </summary>
        /// <param name="groupId">组号</param>
        /// <returns>费用最新价格信息</returns>
        private List<IP_FeeItemGenerate> GetFeeItemGenerateByGroupId(int groupId)
        {
            List<IP_FeeItemGenerate> feeItemGenerateList = NewObject<IP_FeeItemGenerate>().getlist<IP_FeeItemGenerate>(" GroupID=" + groupId);
            foreach (IP_FeeItemGenerate feeItemGenerate in feeItemGenerateList)
            {
                DataRow[] drs = dtStorePrice.Select(" ItemID=" + feeItemGenerate.ItemID + " and  ExecDeptId=" + feeItemGenerate.ExecDeptDoctorID);
                if (drs.Length > 0)
                {
                    feeItemGenerate.TotalFee = feeItemGenerate.TotalFee / feeItemGenerate.SellPrice * Convert.ToDecimal(drs[0]["SellPrice"]);
                    feeItemGenerate.SellPrice = Convert.ToDecimal(drs[0]["SellPrice"]);
                }
            }

            return feeItemGenerateList;
        }

        /// <summary>
        /// 通过GroupId获取医嘱信息并更新最新价格
        /// </summary>
        /// <param name="iGroupIDList">组号列表</param>
        /// <returns>费用最新价格信息</returns>
        private List<IPD_OrderRecord> GetIPDOrderByGroupId(List<int> iGroupIDList)
        {
            List<IPD_OrderRecord> orderRecordList = NewDao<IOrderCheckDao>().GetIPDOrderByGroupId(iGroupIDList);
            foreach (IPD_OrderRecord orderRecord in orderRecordList)
            {
                DataRow[] drs = dtStorePrice.Select(" ItemID=" + orderRecord.ItemID + " and  ExecDeptId=" + orderRecord.ExecDeptID);
                if (drs.Length > 0)
                {
                    orderRecord.ItemPrice = Convert.ToDecimal(drs[0]["SellPrice"]);
                    orderRecord.UnitNO = Convert.ToInt32(drs[0]["MiniConvertNum"]);
                }
            }

            return orderRecordList;
        }
        #endregion

        #region 代码复用方法
        /// <summary>
        /// 费用生成表到费用表转换
        /// </summary>
        /// <param name="feeItemGenerate">费用生成数据</param>
        /// <param name="iAmount">数量</param>
        /// <param name="dTotalFee">金额</param>
        /// <param name="feeDate">费用日期</param>
        /// <returns>费用信息</returns>
        private IP_FeeItemRecord CreateFee(IP_FeeItemGenerate feeItemGenerate, int iAmount, decimal dTotalFee, DateTime feeDate)
        {
            IP_FeeItemRecord feeItemRecord = new IP_FeeItemRecord();
            feeItemRecord.GenerateID = feeItemGenerate.GenerateID;
            feeItemRecord.PatListID = feeItemGenerate.PatListID;
            feeItemRecord.PatName = feeItemGenerate.PatName;
            feeItemRecord.PatDeptID = feeItemGenerate.PatDeptID;
            feeItemRecord.PatDoctorID = feeItemGenerate.PatDoctorID;
            feeItemRecord.PatNurseID = feeItemGenerate.PatNurseID;
            feeItemRecord.BabyID = feeItemGenerate.BabyID;
            feeItemRecord.ItemID = feeItemGenerate.ItemID;
            feeItemRecord.ItemName = feeItemGenerate.ItemName;
            feeItemRecord.FeeClass = feeItemGenerate.FeeClass;
            feeItemRecord.FeeSource = feeItemGenerate.FeeSource;
            feeItemRecord.StatID = feeItemGenerate.StatID;
            feeItemRecord.Spec = feeItemGenerate.Spec;
            feeItemRecord.Unit = feeItemGenerate.Unit;
            feeItemRecord.PackAmount = feeItemGenerate.PackAmount;
            feeItemRecord.InPrice = feeItemGenerate.InPrice;
            feeItemRecord.SellPrice = feeItemGenerate.SellPrice;
            feeItemRecord.PresDeptID = feeItemGenerate.PresDeptID;
            feeItemRecord.PresDoctorID = feeItemGenerate.PresDoctorID;
            feeItemRecord.ExecDeptID = feeItemGenerate.ExecDeptDoctorID;
            feeItemRecord.ChargeDate = DateTime.Now;
            feeItemRecord.DrugFlag = 0;
            feeItemRecord.RecordFlag = 0;
            feeItemRecord.OldFeeRecordID = 0;
            feeItemRecord.CostHeadID = 0;
            feeItemRecord.CostType = 0;
            feeItemRecord.UploadID = 0;
            feeItemRecord.PackUnit = feeItemGenerate.PackUnit;
            feeItemRecord.OrderID = feeItemGenerate.OrderID;
            feeItemRecord.GroupID = feeItemGenerate.GroupID;
            feeItemRecord.OrderType = feeItemGenerate.OrderType;  //0长嘱  1临嘱
            feeItemRecord.FrequencyID = feeItemGenerate.FrequencyID;
            feeItemRecord.FrequencyName = feeItemGenerate.FrequencyName;
            feeItemRecord.ChannelID = feeItemGenerate.ChannelID;
            feeItemRecord.ChannelName = feeItemGenerate.ChannelName;
            feeItemRecord.DoseAmount = feeItemGenerate.DoseAmount;
            //交病人 无需执行，直接生成一条数据，修改频次为数量即可
            feeItemRecord.PresDate = feeDate;
            feeItemRecord.Amount = iAmount;
            feeItemRecord.TotalFee = dTotalFee;

            return feeItemRecord;
        }

        /// <summary>
        /// 红冲费用表
        /// </summary>
        /// <param name="feeItemRecordNeedRedList">费用明细列表</param>
        private void CreateFeeRed(List<IP_FeeItemRecord> feeItemRecordNeedRedList)
        {
            foreach (IP_FeeItemRecord feeItemRecordNeedRed in feeItemRecordNeedRedList)
            {
                feeItemRecordNeedRed.RecordFlag = 1;

                IP_FeeItemRecord feeItemRecordNew = feeItemRecordNeedRed.Clone() as IP_FeeItemRecord;
                feeItemRecordNew.ChargeDate = DateTime.Now;
                feeItemRecordNew.RecordFlag = 2;
                feeItemRecordNew.OldFeeRecordID = feeItemRecordNeedRed.FeeRecordID;
                feeItemRecordNew.UploadID = 0;
                feeItemRecordNew.Amount = feeItemRecordNeedRed.Amount * (-1);
                feeItemRecordNew.TotalFee = feeItemRecordNeedRed.TotalFee * (-1);

                //将计算出来的费用加入到清单
                ipFeeItemRecordList.Add(feeItemRecordNeedRed);
                ipFeeItemRecordList.Add(feeItemRecordNew);
            }
        }

        /// <summary>
        /// 西药处理之后的费用单
        /// </summary>
        /// <param name="orderRecordList">医嘱列表</param>
        /// <param name="frequencyTimingList">频次列表</param>
        /// <param name="feeItemGenerates">医嘱关联费用信息</param>
        /// <returns>费用清单</returns>
        private List<IP_FeeItemRecord> CreateFeeByOrder(List<IPD_OrderRecord> orderRecordList, List<FrequencyTiming> frequencyTimingList, List<IP_FeeItemGenerate> feeItemGenerates)
        {
            int iAllAmount = 0;
            List<IP_FeeItemRecord> feeItemRecordList = new List<IP_FeeItemRecord>();
            try
            {
                foreach (IPD_OrderRecord orderRecord in orderRecordList)
                {
                    iAllAmount = 0;
                    //取出医嘱本身费用
                    IP_FeeItemGenerate feeItemGenerate = feeItemGenerates.Find(x => x.OrderID == orderRecord.OrderID && x.FeeSource == 0);
                    //0.自备 直接将数量和金额改为零 而后插入
                    if (orderRecord.OrderType == 2)
                    {
                        foreach (FrequencyTiming frequencyTiming in frequencyTimingList)
                        {
                            IP_FeeItemRecord feeItemRecord = CreateFee(feeItemGenerate, 0, 0, frequencyTiming.executeTime);
                            feeItemRecordList.Add(feeItemRecord);
                        }
                    }
                    else if (orderRecord.OrderCategory == 0 && dtDrugs.Select(" RoundingMode=0 AND DrugID=" + orderRecord.ItemID).Length > 0)
                    {
                        //1.总量取整的
                        //按药品汇总
                        var fL = from item in frequencyTimingList
                                 group item by new { item.Identify } into g
                                 select new
                                 {
                                     Identify = g.Key.Identify
                                 };

                        foreach (var f in fL)
                        {
                            List<FrequencyTiming> frequencyTimingByIList = frequencyTimingList.FindAll(x => x.Identify == f.Identify);
                            //算出总量向上取整 总基本单位数量=次数*剂量/含量
                            //不拆零 ： Dosage向上取整*包装系数，
                            iAllAmount = Convert.ToInt32(Math.Ceiling((frequencyTimingByIList.Count * orderRecord.Dosage) / orderRecord.Factor));
                            //单次用量  用的是基本单位数量
                            int iAmount = Convert.ToInt32(orderRecord.Amount);
                            foreach (FrequencyTiming frequencyTimingByI in frequencyTimingByIList)
                            {
                                if (iAmount <= iAllAmount)
                                {
                                    iAllAmount -= iAmount;
                                }
                                else
                                {
                                    iAmount = iAllAmount;
                                    iAllAmount = 0;
                                }

                                //金额应该是 单价*（基本单位数量/单位数量系数）
                                IP_FeeItemRecord feeItemRecord = CreateFee(feeItemGenerate, iAmount, feeItemGenerate.SellPrice * iAmount / orderRecord.UnitNO, frequencyTimingByI.executeTime);
                                feeItemRecordList.Add(feeItemRecord);
                            }
                        }
                    }
                    else
                    {
                        //2.其他的不做处理  直接插入
                        foreach (FrequencyTiming frequencyTiming in frequencyTimingList)
                        {
                            IP_FeeItemRecord feeItemRecord = CreateFee(feeItemGenerate, feeItemGenerate.Amount, feeItemGenerate.TotalFee, frequencyTiming.executeTime);
                            feeItemRecordList.Add(feeItemRecord);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return feeItemRecordList;
        }

        /// <summary>
        /// 西药处理之后的费用单-末次
        /// </summary>
        /// <param name="orderRecordList">医嘱列表</param>
        /// <param name="feeItemGenerates">医嘱关联费用信息</param>
        /// <returns>费用清单</returns>
        private List<IP_FeeItemRecord> CreateFeeByOrderTeminalNum(List<IPD_OrderRecord> orderRecordList, List<IP_FeeItemGenerate> feeItemGenerates)
        {
            int iAllAmount = 0;
            List<IP_FeeItemRecord> feeItemRecordList = new List<IP_FeeItemRecord>();
            try
            {
                foreach (IPD_OrderRecord orderRecord in orderRecordList)
                {
                    iAllAmount = 0;
                    //取出医嘱本身费用
                    IP_FeeItemGenerate feeItemGenerate = feeItemGenerates.Find(x => x.OrderID == orderRecord.OrderID && x.FeeSource == 0);
                    //0.自备 直接将数量和金额改为零 而后插入
                    if (orderRecord.OrderType == 2)
                    {
                        for (int i = 1; i <= orderRecord.TeminalNum; i++)
                        {
                            IP_FeeItemRecord feeItemRecord = CreateFee(feeItemGenerate, 0, 0, orderRecord.EOrderDate.AddSeconds(i * -1));
                            feeItemRecordList.Add(feeItemRecord);
                        }
                    }
                    else if (dtDrugs.Select(" RoundingMode=0 AND DrugID=" + orderRecord.ItemID).Length > 0)
                    {
                        //1.总量取整的
                        //算出总量向上取整 总数量=次数*剂量/含量
                        iAllAmount = Convert.ToInt32(Math.Ceiling((orderRecord.TeminalNum * orderRecord.Dosage) / orderRecord.Factor));
                        //单次用量
                        int iAmount = Convert.ToInt32(orderRecord.Amount);
                        for (int i = 0; i < orderRecord.TeminalNum; i++)
                        {
                            if (iAmount >= iAllAmount)
                            {
                                iAllAmount -= iAmount;
                            }
                            else
                            {
                                iAmount = iAllAmount;
                                iAllAmount = 0;
                            }

                            IP_FeeItemRecord feeItemRecord = CreateFee(feeItemGenerate, iAmount, feeItemGenerate.SellPrice * iAmount / orderRecord.UnitNO, orderRecord.EOrderDate.AddSeconds(i * -1));
                            feeItemRecordList.Add(feeItemRecord);
                        }
                    }
                    else
                    {
                        //2.其他的不做处理  直接插入
                        for (int i = 0; i < orderRecord.TeminalNum; i++)
                        {
                            IP_FeeItemRecord feeItemRecord = CreateFee(feeItemGenerate, 0, 0, orderRecord.EOrderDate.AddSeconds(i * -1));
                            feeItemRecordList.Add(feeItemRecord);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return feeItemRecordList;
        }

        /// <summary>
        /// 生成费用清单
        /// </summary>
        /// <param name="feeItemRecord">费用明细数据</param>
        private void CreateFeeExcuteList(IP_FeeItemRecord feeItemRecord)
        {
            IPN_OrderExecBillRecord orderExecBillRecord = new IPN_OrderExecBillRecord();
            orderExecBillRecord.RecordID = feeItemRecord.FeeRecordID;
            orderExecBillRecord.OrderCategory = feeItemRecord.OrderType;
            orderExecBillRecord.ExeBillTypeID = 0;
            orderExecBillRecord.PatListID = feeItemRecord.PatListID;
            orderExecBillRecord.GroupID = feeItemRecord.GroupID;
            orderExecBillRecord.OrderID = feeItemRecord.OrderID;
            orderExecBillRecord.OrderName = feeItemRecord.ItemName;
            orderExecBillRecord.PresDeptID = feeItemRecord.PresDeptID;
            orderExecBillRecord.PresDoctorID = feeItemRecord.PresDoctorID;
            orderExecBillRecord.Spec = feeItemRecord.Spec;
            orderExecBillRecord.Dosage = 0;
            orderExecBillRecord.Unit = feeItemRecord.Unit;
            orderExecBillRecord.ChannelID = feeItemRecord.ChannelID;
            orderExecBillRecord.FrenquencyID = feeItemRecord.FrequencyID;
            orderExecBillRecord.ExecDate = feeItemRecord.PresDate;

            ipFeeItemRecordListExcute.Add(orderExecBillRecord);
        }
        #endregion

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="sLog">日志内容</param>
        private void LogHelper(string sLog)
        {
            EFWCoreLib.CoreFrame.Common.MiddlewareLogHelper.WriterLog(EFWCoreLib.CoreFrame.Common.LogType.TimingTaskLog, true, System.Drawing.Color.Red, sLog);
        }
    }
}