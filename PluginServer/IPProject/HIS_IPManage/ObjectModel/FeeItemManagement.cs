﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.Common;
using HIS_Entity.IPManage;
using HIS_IPManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPManage.ObjectModel
{
    /// <summary>
    /// 费用管理
    /// </summary>
    public class FeeItemManagement : AbstractObjectModel
    {
        #region "保存费用生成数据"

        /// <summary>
        /// 保存住院费用生成数据
        /// </summary>
        /// <param name="longFeeOrderDt">住院费用生成数据</param>
        /// <param name="markEmpID">记账人ID</param>
        /// <returns>错误消息</returns>
        public string SaveLongOrderData(DataTable longFeeOrderDt, int markEmpID)
        {
            try
            {
                if (longFeeOrderDt != null && longFeeOrderDt.Rows.Count > 0)
                {
                    IP_FeeItemGenerate feeItem = null;
                    // 查询病人登记信息
                    DataTable patDt = NewDao<IIPManageDao>().GetPatListInfo(Convert.ToInt32(longFeeOrderDt.Rows[0]["PatListID"]));
                    IP_PatList pat = ConvertExtend.ToObject<IP_PatList>(patDt, 0);
                    for (int i = 0; i < longFeeOrderDt.Rows.Count; i++)
                    {
                        feeItem = NewObject<IP_FeeItemGenerate>();
                        feeItem = ConvertExtend.ToObject<IP_FeeItemGenerate>(longFeeOrderDt, i);
                        feeItem.PatListID = pat.PatListID;
                        feeItem.PatName = pat.PatName;
                        feeItem.PatDeptID = pat.CurrDeptID;
                        feeItem.PatDoctorID = pat.CurrDoctorID;
                        feeItem.PatNurseID = pat.CurrNurseID;
                        feeItem.MarkEmpID = markEmpID;
                        feeItem.PresDoctorID = pat.CurrDoctorID;
                        feeItem.PresDeptID = feeItem.PatDeptID;
                        feeItem.FeeSource = 3;
                        if (feeItem.ExecDeptDoctorID == 0)
                        {
                            feeItem.ExecDeptDoctorID = pat.CurrDeptID;
                        }

                        this.BindDb(feeItem);
                        feeItem.save();
                        longFeeOrderDt.Rows[i]["GenerateID"] = feeItem.GenerateID;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return string.Empty;
        }

        /// <summary>
        /// 删除费用数据
        /// </summary>
        /// <param name="delFeeItemDt">待删除费用数据</param>
        /// <returns>错误消息</returns>
        public string DelFeeLongOrderData(DataTable delFeeItemDt)
        {
            string msg = string.Empty;
            try
            {
                IP_FeeItemGenerate feeItem = null;
                for (int i = 0; i < delFeeItemDt.Rows.Count; i++)
                {
                    feeItem = NewObject<IP_FeeItemGenerate>();
                    feeItem = ConvertExtend.ToObject<IP_FeeItemGenerate>(delFeeItemDt, i);
                    bool result = NewDao<IIPManageDao>().IsFeeCharge(feeItem.GenerateID);
                    if (result)
                    {
                        msg += "[" + feeItem.ItemName + "]、";
                        continue;
                    }

                    this.BindDb(feeItem);
                    feeItem.delete();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            if (!string.IsNullOrEmpty(msg))
            {
                msg = msg.Substring(0, msg.Length - 1);
                msg += "等项目已经被记账无法删除！";
            }

            return msg;
        }

        #endregion

        #region "记账"

        /// <summary>
        /// 账单记账
        /// </summary>
        /// <param name="feeItemAccDt">记账明细数据</param>
        /// <param name="empID">记账人ID</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isBedFee">是否记床位费</param>
        /// <param name="isLongFee">是否记账单</param>
        /// <returns>true：记账成功</returns>
        public bool FeeItemAccounting(
                                        DataTable feeItemAccDt,
                                        int empID,
                                        DateTime endTime,
                                        bool isBedFee,
                                        bool isLongFee)
        {
            // 是否勾选了账单
            if (isLongFee)
            {
                #region "保存费用生成数据"
                List<int> generateIdList = new List<int>();
                // 保存未保存的账单
                if (feeItemAccDt != null && feeItemAccDt.Rows.Count > 0)
                {
                    IP_FeeItemGenerate feeItem = null;
                    for (int i = 0; i < feeItemAccDt.Rows.Count; i++)
                    {
                        feeItem = NewObject<IP_FeeItemGenerate>();
                        feeItem = ConvertExtend.ToObject<IP_FeeItemGenerate>(feeItemAccDt, i);
                        // 如果当前记录在费用生成表中不存在，则先记录保存到费用生成表中，然后在进行记账操作
                        if (feeItem.GenerateID == 0)
                        {
                            // 查询病人登记信息
                            DataTable patDt = NewDao<IIPManageDao>().GetPatListInfo(Convert.ToInt32(feeItemAccDt.Rows[0]["PatListID"]));
                            IP_PatList pat = ConvertExtend.ToObject<IP_PatList>(patDt, 0);
                            feeItem.PatListID = pat.PatListID;
                            feeItem.PatName = pat.PatName;
                            feeItem.PatDeptID = pat.CurrDeptID;
                            feeItem.PatDoctorID = pat.CurrDoctorID;
                            feeItem.PatNurseID = pat.CurrNurseID;
                            feeItem.MarkEmpID = empID;
                            feeItem.PresDoctorID = pat.CurrDoctorID;
                            feeItem.PresDeptID = feeItem.PatDeptID;
                            feeItem.FeeSource = 3;
                            if (feeItem.ExecDeptDoctorID == 0)
                            {
                                feeItem.ExecDeptDoctorID = pat.CurrDeptID;
                            }

                            this.BindDb(feeItem);
                            feeItem.save();
                        }

                        generateIdList.Add(feeItem.GenerateID);
                    }

                    // 费用记账--保存费用明细数据
                    FeeItemAccounting(generateIdList, endTime, empID);
                }
                #endregion
            }

            // 是否勾选了床位费
            if (isBedFee)
            {
                BedFeeAccounting(Convert.ToInt32(feeItemAccDt.Rows[0]["PatListID"]), endTime, empID);
            }

            return true;
        }

        /// <summary>
        /// 保存费用明细数据
        /// </summary>
        /// <param name="generateIdList">费用生成ID集合</param>
        /// <param name="endTime">记账结束时间</param>
        /// <param name="empID">操作员ID</param>
        /// <returns>true:保存成功</returns>
        public bool FeeItemAccounting(List<int> generateIdList, DateTime endTime, int empID)
        {
            // 不存在需要记账的数据
            if (generateIdList.Count <= 0)
            {
                return true;
            }
            // 获取账单记账开始时间以及记账天数
            // 取得费用生成ID列表
            string strGenerateIdList = string.Join(",", generateIdList.ToArray());
            DataTable accountDateDt = NewDao<IIPManageDao>().GetAccountDate(strGenerateIdList, endTime, false, -1);
            if (accountDateDt != null && accountDateDt.Rows.Count > 0)
            {
                int patListId = Convert.ToInt32(accountDateDt.Rows[0]["PatListID"]);
                for (int i = 0; i < accountDateDt.Rows.Count; i++)
                {
                    // 取得记账天数
                    int days = Convert.ToInt32(accountDateDt.Rows[i]["Days"]);
                    if (days < 0)
                    {
                        continue;
                    }
                    // 保存费用明细表数据
                    // 费用生成ID
                    int generateID = Convert.ToInt32(accountDateDt.Rows[i]["GenerateID"]);
                    // 重新获取价格
                    DataTable feeItemGenerateDt = NewDao<IIPManageDao>().GetFeeItemGenerateData(patListId, generateID);
                    IP_FeeItemRecord feeItemRecord = NewObject<IP_FeeItemRecord>();
                    feeItemRecord = ConvertExtend.ToObject<IP_FeeItemRecord>(feeItemGenerateDt, 0);
                    // 临时账单只记账一次，执行时间为费用生成时间
                    if (Convert.ToInt32(accountDateDt.Rows[i]["OrderType"]) == 3)
                    {
                        // 根据最新价格重新计算金额
                        feeItemRecord.TotalFee = Math.Round(feeItemRecord.Amount * feeItemRecord.SellPrice / feeItemRecord.PackAmount, 2);
                        // 保存费用明细数据
                        this.BindDb(feeItemRecord);
                        feeItemRecord.save();
                        // 临时账单记账后直接停用账单
                        NewDao<IIPManageDao>().StopTempFeeItemGenerate(feeItemRecord.GenerateID);
                        continue;
                    }
                    // 临时账单记账，根据开始日期以及记账天数记账
                    for (int j = 0; j <= days; j++)
                    {
                        #region "组合项目拆分明细注释代码"
                        //// 取得费用明细类型
                        //int feeClass = Convert.ToInt32(accountDateDt.Rows[i]["FeeClass"]);
                        //// 如果为组合项目，则先拆分成明细再保存数据
                        //if (feeClass == 4)
                        //{
                        //    // 根据组合项目ItemID获取组合项目明细列表
                        //    DataTable CombinationProjectDt = NewDao<IIPManageDao>().CombinationProjectDetails(feeItemRecord.ItemID);
                        //    for (int k = 0; k < CombinationProjectDt.Rows.Count; k++)
                        //    {
                        //        // 保存费用明细数据
                        //        feeItemRecord.FeeRecordID = 0;
                        //        // 项目ID
                        //        feeItemRecord.ItemID = Convert.ToInt32(CombinationProjectDt.Rows[k]["ItemID"]);
                        //        // 项目名
                        //        feeItemRecord.ItemName = CombinationProjectDt.Rows[k]["ItemName"].ToString();
                        //        // 项目类型
                        //        feeItemRecord.FeeClass = Convert.ToInt32(CombinationProjectDt.Rows[k]["ItemClass"]);
                        //        // 大项目ID
                        //        feeItemRecord.StatID = Convert.ToInt32(CombinationProjectDt.Rows[k]["StatID"]);
                        //        // 规格
                        //        feeItemRecord.Spec = CombinationProjectDt.Rows[k]["Standard"].ToString();
                        //        // 单位
                        //        feeItemRecord.Unit = CombinationProjectDt.Rows[k]["MiniUnitName"].ToString();
                        //        // 划价系数
                        //        feeItemRecord.PackAmount = Convert.ToDecimal(CombinationProjectDt.Rows[k]["MiniConvertNum"]);
                        //        // 批发价
                        //        feeItemRecord.InPrice = Convert.ToDecimal(CombinationProjectDt.Rows[k]["InPrice"]);
                        //        // 销售价
                        //        feeItemRecord.SellPrice = Convert.ToDecimal(CombinationProjectDt.Rows[k]["SellPrice"]);
                        //        // 执行科室
                        //        feeItemRecord.ExecDeptID = Convert.ToInt32(CombinationProjectDt.Rows[k]["ExecDeptId"]);
                        //    }
                        //}
                        #endregion
                        feeItemRecord.FeeRecordID = 0;
                        if (j == 0)
                        {
                            // 直接取开始时间
                            feeItemRecord.PresDate = Convert.ToDateTime(accountDateDt.Rows[i]["ExecDate"]);
                        }
                        else
                        {
                            // 开始时间累加1
                            feeItemRecord.PresDate = Convert.ToDateTime(accountDateDt.Rows[i]["ExecDate"]).AddDays(j);
                        }
                        // 根据最新价格重新计算金额
                        feeItemRecord.TotalFee = Math.Round(feeItemRecord.Amount * feeItemRecord.SellPrice / feeItemRecord.PackAmount, 2);
                        // 保存费用明细数据
                        this.BindDb(feeItemRecord);
                        feeItemRecord.save();

                        // 保存处方明细关系数据
                        IP_FeeItemRelationship feeItemRelationship = NewObject<IP_FeeItemRelationship>();
                        feeItemRelationship.PatListID = feeItemRecord.PatListID;
                        feeItemRelationship.OrderID = 0;
                        feeItemRelationship.GenerateID = feeItemRecord.GenerateID;
                        feeItemRelationship.FeeSource = 0;
                        feeItemRelationship.ExecDate = feeItemRecord.PresDate;
                        feeItemRelationship.ChargeDate = DateTime.Now;
                        feeItemRelationship.ChargeEmpID = empID;
                        this.BindDb(feeItemRelationship);
                        feeItemRelationship.save();
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 床位费记账
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="endTime">记账截止时间</param>
        /// <param name="empID">记账人ID</param>
        /// <returns>true：记账成功</returns>
        public bool BedFeeAccounting(int patListID, DateTime endTime, int empID)
        {
            // 根据病人ID查询病人所有未停用的床位费用
            DataTable accountDateDt = NewDao<IIPManageDao>().GetAccountDate(string.Empty, endTime, true, patListID);
            if (accountDateDt != null && accountDateDt.Rows.Count > 0)
            {
                for (int i = 0; i < accountDateDt.Rows.Count; i++)
                {
                    // 取得记账天数
                    int days = Convert.ToInt32(accountDateDt.Rows[i]["Days"]);
                    if (days < 0)
                    {
                        continue;
                    }
                    // 保存费用明细表数据
                    // 费用生成ID
                    int generateID = Convert.ToInt32(accountDateDt.Rows[i]["GenerateID"]);
                    // 重新获取价格
                    DataTable feeItemGenerateDt = NewDao<IIPManageDao>().GetFeeItemGenerateData(patListID, generateID);
                    IP_FeeItemRecord feeItemRecord = NewObject<IP_FeeItemRecord>();
                    feeItemRecord = ConvertExtend.ToObject<IP_FeeItemRecord>(feeItemGenerateDt, 0);
                    // 床位费记账，根据开始日期以及记账天数记账
                    for (int j = 0; j <= days; j++)
                    {
                        feeItemRecord.FeeRecordID = 0;
                        if (j == 0)
                        {
                            // 直接取开始时间
                            feeItemRecord.PresDate = Convert.ToDateTime(accountDateDt.Rows[i]["ExecDate"]);
                        }
                        else
                        {
                            // 开始时间累加1
                            feeItemRecord.PresDate = Convert.ToDateTime(accountDateDt.Rows[i]["ExecDate"]).AddDays(j);
                        }
                        // 根据最新价格重新计算金额
                        feeItemRecord.TotalFee = Math.Round(feeItemRecord.Amount * feeItemRecord.SellPrice, 2);
                        // 保存费用明细数据
                        this.BindDb(feeItemRecord);
                        feeItemRecord.save();

                        // 保存处方明细关系数据
                        IP_FeeItemRelationship feeItemRelationship = NewObject<IP_FeeItemRelationship>();
                        feeItemRelationship.PatListID = feeItemRecord.PatListID;
                        feeItemRelationship.OrderID = 0;
                        feeItemRelationship.GenerateID = feeItemRecord.GenerateID;
                        feeItemRelationship.FeeSource = 0;
                        feeItemRelationship.ExecDate = feeItemRecord.PresDate;
                        feeItemRelationship.ChargeDate = DateTime.Now;
                        feeItemRelationship.ChargeEmpID = empID;
                        this.BindDb(feeItemRelationship);
                        feeItemRelationship.save();
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 账单记账
        /// </summary>
        /// <param name="feeItemAccDt">记账数据</param>
        /// <param name="empID">操作人ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isBedFee">床位费标志</param>
        /// <param name="isLongFee">长期账单标志</param>
        /// <param name="msgList">错误消息</param>
        /// <returns>true:记账成功</returns>
        public bool FeeItemAccounting(
                                        DataTable feeItemAccDt,
                                        int empID,
                                        DateTime startTime,
                                        DateTime endTime,
                                        bool isBedFee,
                                        bool isLongFee,
                                        List<string> msgList)
        {
            // 获取需要记账的天数
            int interval = new TimeSpan(Convert.ToDateTime(endTime.ToString("yyyy-MM-dd")).Ticks -
                Convert.ToDateTime(startTime.ToString("yyyy-MM-dd")).Ticks).Days + 1;
            // 是否勾选了账单
            if (isLongFee)
            {
                if (feeItemAccDt != null && feeItemAccDt.Rows.Count > 0)
                {
                    IP_FeeItemGenerate feeItem = null;
                    for (int i = 0; i < feeItemAccDt.Rows.Count; i++)
                    {
                        feeItem = NewObject<IP_FeeItemGenerate>();
                        feeItem = ConvertExtend.ToObject<IP_FeeItemGenerate>(feeItemAccDt, i);
                        // 如果当前记录在费用生成表中不存在，则先记录保存到费用生成表中，然后在进行记账操作
                        if (feeItem.GenerateID == 0)
                        {
                            // 查询病人登记信息
                            DataTable patDt = NewDao<IIPManageDao>().GetPatListInfo(Convert.ToInt32(feeItemAccDt.Rows[0]["PatListID"]));
                            IP_PatList pat = ConvertExtend.ToObject<IP_PatList>(patDt, 0);
                            feeItem.PatListID = pat.PatListID;
                            feeItem.PatName = pat.PatName;
                            feeItem.PatDeptID = pat.CurrDeptID;
                            feeItem.PatDoctorID = pat.CurrDoctorID;
                            feeItem.PatNurseID = pat.CurrNurseID;
                            feeItem.MarkEmpID = empID;
                            feeItem.PresDoctorID = pat.CurrDoctorID;
                            feeItem.PresDeptID = feeItem.PatDeptID;
                        }

                        this.BindDb(feeItem);
                        feeItem.save();
                        // 保存费用数据
                        bool result = SaveFeeItemAccountingData(feeItem, feeItemAccDt, i, interval, startTime, msgList);
                    }
                }
            }

            // 是否需要记床位费
            if (isBedFee)
            {
                DateTime presDate = startTime;
                for (int s = 0; s < interval; s++)
                {
                    if (s > 0)
                    {
                        // 每循环一次处方日期加1
                        presDate = Convert.ToDateTime(presDate.AddDays(1).ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        presDate = Convert.ToDateTime(presDate.ToString("yyyy-MM-dd"));
                    }

                    // 检查当日是否已记床位费
                    if (NewDao<IIPManageDao>().IsExistenceBedFeeData(Convert.ToInt32(feeItemAccDt.Rows[0]["PatListID"].ToString()), presDate))
                    {
                        IP_FeeItemGenerate feeItem = ConvertExtend.ToObject<IP_FeeItemGenerate>(feeItemAccDt, 0);
                        // 取得病人关联的所有床位
                        DataTable bedList = NewDao<IIPManageDao>().GetPatientBedList(Convert.ToInt32(feeItemAccDt.Rows[0]["PatListID"].ToString()));
                        DataTable bedFeeDt = new DataTable();
                        for (int i = 0; i < bedList.Rows.Count; i++)
                        {
                            DataTable tempDt = null;
                            if (Convert.ToInt32(bedList.Rows[i]["IsPack"]) == 0)
                            {
                                // 根据床位ID查询床位费用
                                tempDt = NewDao<IIPManageDao>().GetBedFeeItemList(Convert.ToInt32(bedList.Rows[i]["BedID"].ToString()), 0);
                            }
                            else
                            {
                                // 根据床位ID查询床位费用
                                tempDt = NewDao<IIPManageDao>().GetBedFeeItemList(Convert.ToInt32(bedList.Rows[i]["BedID"].ToString()), 1);
                            }

                            if (i == 0)
                            {
                                bedFeeDt = tempDt.Clone();
                            }

                            bedFeeDt.Merge(tempDt);
                        }

                        // 记床位费账单
                        if (bedFeeDt != null && bedFeeDt.Rows.Count > 0)
                        {
                            for (int i = 0; i < bedFeeDt.Rows.Count; i++)
                            {
                                // 保存处方明细关系数据
                                IP_FeeItemRelationship feeItemRelationship = NewObject<IP_FeeItemRelationship>();
                                feeItemRelationship.GenerateID = 0;
                                feeItemRelationship.ChargeDate = presDate;
                                //feeItemRelationship
                                feeItemRelationship.ChargeEmpID = empID;
                                feeItemRelationship.PatListID = Convert.ToInt32(feeItemAccDt.Rows[0]["PatListID"].ToString());
                                feeItemRelationship.FeeSource = 2;
                                this.BindDb(feeItemRelationship);
                                feeItemRelationship.save();

                                // 写入处方明细表数据
                                IP_FeeItemRecord feeItemRecord = NewObject<IP_FeeItemRecord>();
                                SetFeeItemRecord(
                                    feeItemRecord, 
                                    bedFeeDt.Rows[i], 
                                    feeItem,
                                    Convert.ToInt32(bedFeeDt.Rows[i]["ItemClass"].ToString()),
                                    presDate,
                                    Convert.ToInt32(bedFeeDt.Rows[i]["ItemAmount"].ToString()), 
                                    true);
                                feeItemRecord.TotalFee = Math.Round(feeItemRecord.Amount * Convert.ToDecimal(bedFeeDt.Rows[i]["UnitPrice"].ToString()), 2);
                                feeItemRecord.PackAmount = Convert.ToDecimal(bedFeeDt.Rows[i]["UnitPrice"].ToString());  // 划价系数
                                feeItemRecord.GenerateID = 0;   // 费用生成ID
                                feeItemRecord.PresDeptID = feeItem.PatDeptID;
                                feeItemRecord.ExecDeptID = feeItem.PatDeptID;
                                feeItemRecord.PresDoctorID = empID;
                                feeItemRecord.ExecDeptID = Convert.ToInt32(bedFeeDt.Rows[i]["ExecDeptId"].ToString()); // 执行科室ID
                                feeItemRecord.OrderType = 4;
                                this.BindDb(feeItemRecord);
                                feeItemRecord.save();
                            }
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 保存记账数据
        /// </summary>
        /// <param name="feeItem">处方生成对象</param>
        /// <param name="feeItemAccDt">处方生成列表</param>
        /// <param name="rowIndex">处方生成Index</param>
        /// <param name="interval">处方天数</param>
        /// <param name="presDate">处方开始日期</param>
        /// <param name="msgList">错误消息</param>
        /// <returns>true:记账成功</returns>
        private bool SaveFeeItemAccountingData(
            IP_FeeItemGenerate feeItem, 
            DataTable feeItemAccDt, 
            int rowIndex, 
            int interval, 
            DateTime presDate, 
            List<string> msgList)
        {
            // 根据选择的时间区间记录账单
            for (int s = 0; s < interval; s++)
            {
                if (s > 0)
                {
                    // 每循环一次处方日期加1
                    presDate = Convert.ToDateTime(presDate.AddDays(1).ToString("yyyy-MM-dd"));
                }
                else
                {
                    presDate = Convert.ToDateTime(presDate.ToString("yyyy-MM-dd"));
                }
                // 检查是否存在重复记账
                if (!NewDao<IIPManageDao>().IsExistenceItemAccountingData(feeItem.GenerateID, feeItem.PatListID, presDate))
                {
                    continue;
                }

                // 1.对药品记账时，记账成功后要减去有效库存。
                // ItemClass=1为药品，需要减去有效库存
                if (feeItem.FeeClass == 1)
                {
                    try
                    {
                        // 减去有效库存
                        NewObject<DrugStoreManagement>().UpdateStorage(feeItem.ItemID, feeItem.ExecDeptDoctorID, feeItem.Amount - (feeItem.Amount * 2));
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("库存数不够"))
                        {
                            msgList.Add("[" + feeItem.ItemName + "]");
                            continue;
                        }
                    }
                }
                // 写入关系表数据
                // 判断是否为组合项目，如果为组合项目需先拆成明细在写入
                if (feeItem.FeeClass == 4)
                {
                    // 根据组合项目ItemID获取组合项目明细列表
                    DataTable combinationProjectDt = NewDao<IIPManageDao>().CombinationProjectDetails(feeItem.ItemID);
                    for (int j = 0; j < combinationProjectDt.Rows.Count; j++)
                    {
                        // 保存处方明细关系数据
                        IP_FeeItemRelationship feeItemRelationship = NewObject<IP_FeeItemRelationship>();
                        feeItemRelationship.GenerateID = feeItem.GenerateID;
                        feeItemRelationship.ChargeDate = presDate;
                        feeItemRelationship.ChargeEmpID = feeItem.MarkEmpID;
                        feeItemRelationship.PatListID = feeItem.PatListID;
                        feeItemRelationship.FeeSource = 0;
                        this.BindDb(feeItemRelationship);
                        feeItemRelationship.save();

                        // 写入处方明细表数据
                        IP_FeeItemRecord feeItemRecord = NewObject<IP_FeeItemRecord>();
                        SetFeeItemRecord(
                            feeItemRecord, 
                            combinationProjectDt.Rows[j], 
                            feeItem,
                            Convert.ToInt32(combinationProjectDt.Rows[j]["ItemClass"].ToString()), 
                            presDate, 
                            0, 
                            false);
                        this.BindDb(feeItemRecord);
                        feeItemRecord.save();
                    }
                }
                else
                {
                    // 不是组合项目直接写入账单
                    // 保存处方明细关系数据
                    IP_FeeItemRelationship feeItemRelationship = NewObject<IP_FeeItemRelationship>();
                    feeItemRelationship.GenerateID = feeItem.GenerateID;
                    feeItemRelationship.ChargeDate = presDate;
                    feeItemRelationship.ChargeEmpID = feeItem.MarkEmpID;
                    feeItemRelationship.PatListID = feeItem.PatListID;
                    feeItemRelationship.FeeSource = 0;
                    this.BindDb(feeItemRelationship);
                    feeItemRelationship.save();

                    // 写入处方明细表数据
                    IP_FeeItemRecord feeItemRecord = NewObject<IP_FeeItemRecord>();
                    SetFeeItemRecord(feeItemRecord, feeItemAccDt.Rows[rowIndex], feeItem, feeItem.FeeClass, presDate, 0, false);
                    this.BindDb(feeItemRecord);
                    feeItemRecord.save();
                }
            }

            return true;
        }

        /// <summary>
        /// 做成账单明细数据
        /// </summary>
        /// <param name="feeItemRecord">账单明细</param>
        /// <param name="feeDr">费用数据</param>
        /// <param name="feeItem">费用生成</param>
        /// <param name="feeClass">费用类型</param>
        /// <param name="presDate">处方时间</param>
        /// <param name="amount">基本数</param>
        /// <param name="isBedFee">是否记床位费</param>
        private void SetFeeItemRecord(
            IP_FeeItemRecord feeItemRecord, 
            DataRow feeDr, 
            IP_FeeItemGenerate feeItem, 
            int feeClass, 
            DateTime presDate, 
            int amount, 
            bool isBedFee)
        {
            // 写入费用明细表数据
            feeItemRecord.GenerateID = feeItem.GenerateID;   // 费用生成ID
            feeItemRecord.PatListID = feeItem.PatListID;   // 病人ID
            feeItemRecord.PatName = feeItem.PatName;   // 病人名
            feeItemRecord.PatDeptID = feeItem.PatDeptID;   // 病人入院科室
            feeItemRecord.PatDoctorID = feeItem.PatDoctorID;   // 病人责任医生
            feeItemRecord.PatNurseID = feeItem.PatNurseID;    // 病人责任护士
            feeItemRecord.BabyID = feeItem.BabyID;       // BabyID
            feeItemRecord.ItemID = Convert.ToInt32(feeDr["ItemID"].ToString());   // 项目ID
            feeItemRecord.ItemName = feeDr["ItemName"].ToString();   // 项目名
            feeItemRecord.StatID = Convert.ToInt32(feeDr["StatID"].ToString());     // StatID
            feeItemRecord.InPrice = Convert.ToDecimal(feeDr["InPrice"].ToString());   // 批发价
            feeItemRecord.SellPrice = Convert.ToDecimal(feeDr["SellPrice"].ToString());  // 销售价
            feeItemRecord.FeeClass = feeClass;
            feeItemRecord.PackAmount = feeItem.PackAmount;
            if (!isBedFee)
            {
                feeItemRecord.Amount = feeItem.Amount;   // 费用生成表的基本数
                feeItemRecord.Spec = feeItem.Spec;     // 规格
                feeItemRecord.Unit = feeItem.Unit;        // 单位
                feeItemRecord.FeeSource = 0;  // 0账单
            }
            else
            {
                feeItemRecord.Amount = amount;   // 费用生成表的基本数
                feeItemRecord.Spec = feeDr["Standard"].ToString();     // 规格
                feeItemRecord.Unit = feeDr["UnPickUnit"].ToString();        // 单位
                feeItemRecord.FeeSource = 2;  // 2床位费
            }

            feeItemRecord.TotalFee = Math.Round(feeItemRecord.Amount * feeItemRecord.SellPrice / feeItemRecord.PackAmount, 2);
            feeItemRecord.DoseAmount = 0; // 默认0
            feeItemRecord.PresDeptID = feeItem.PresDeptID;  // 划价科室ID
            feeItemRecord.PresDoctorID = feeItem.PresDoctorID; // 划价医生ID
            feeItemRecord.ExecDeptID = feeItem.ExecDeptDoctorID; // 执行科室ID
            feeItemRecord.PresDate = presDate;   // 处方日期
            feeItemRecord.ChargeDate = DateTime.Now;  // 记账日期
            feeItemRecord.DrugFlag = 0;   // 发药标识
            feeItemRecord.RecordFlag = 0;  // 记录状态
            feeItemRecord.OldFeeRecordID = 0;  // 退费明细ID
            feeItemRecord.CostHeadID = 0;
            feeItemRecord.CostType = 0;
            feeItemRecord.UploadID = 0;
            feeItemRecord.OrderType = feeItem.OrderType;
        }

        #endregion

        #region "冲账"

        /// <summary>
        /// 费用冲账
        /// </summary>
        /// <param name="strikeABalanceDt">冲账数据</param>
        /// <param name="msgList">错误消息</param>
        /// <returns>true:冲账成功</returns>
        public bool StrikeABalance(DataTable strikeABalanceDt, List<string> msgList)
        {
            // 项目已做不能冲账提示消息。
            StringBuilder strMsg = new StringBuilder();
            bool isStrikeABalance = true;
            if (strikeABalanceDt != null && strikeABalanceDt.Rows.Count > 0)
            {
                for (int i = 0; i < strikeABalanceDt.Rows.Count; i++)
                {
                    int feeRecordID = Convert.ToInt32(strikeABalanceDt.Rows[i]["FeeRecordID"].ToString());
                    DataTable feeItemRecordDt = NewDao<IIPManageDao>().GetIPFeeItemRecordInfo(feeRecordID);
                    IP_FeeItemRecord feeItemRecord = ConvertExtend.ToObject<IP_FeeItemRecord>(feeItemRecordDt, 0);
                    // 检查药品是否已发药、检查项目是否已做检查
                    bool result = NewDao<IIPManageDao>().CheckIsMedicine(Convert.ToInt32(strikeABalanceDt.Rows[i]["FeeRecordID"].ToString()));
                    // 取得账单类型
                    int feeClass = Convert.ToInt32(strikeABalanceDt.Rows[i]["FeeClass"].ToString());
                    // 未发药
                    if (result)
                    {
                        switch (feeClass)
                        {
                            // 药品
                            case 1:
                                // 检查是否已生成统领单，如果已生成统领单，则需要删除统领单对应的信息
                                bool isDrugBillDetail = NewDao<IIPManageDao>().CheckIsGenerateDrugBillDetail(feeRecordID);
                                if (isDrugBillDetail)
                                {
                                    // 删除统领单明细数据
                                    NewDao<IIPManageDao>().DelDrugBillDetail(feeRecordID);
                                }

                                feeItemRecord.RecordFlag = 1;
                                feeItemRecord.ChargeDate = DateTime.Now;
                                this.BindDb(feeItemRecord);
                                feeItemRecord.save();

                                // 冲账时产生负数记录
                                feeItemRecord = feeItemRecord.Clone() as IP_FeeItemRecord;
                                feeItemRecord.OldFeeRecordID = feeRecordID;
                                feeItemRecord.FeeRecordID = 0;
                                feeItemRecord.RecordFlag = 2;
                                feeItemRecord.ChargeDate = DateTime.Now;
                                feeItemRecord.Amount = 0 - feeItemRecord.Amount;
                                feeItemRecord.TotalFee = 0 - feeItemRecord.TotalFee;
                                this.BindDb(feeItemRecord);
                                feeItemRecord.save();
                                // 追加有效库存
                                NewObject<DrugStoreManagement>().UpdateStorage(
                                    feeItemRecord.ItemID,
                                    feeItemRecord.ExecDeptID, 
                                    Math.Abs(feeItemRecord.Amount));
                                break;
                            // 材料/项目
                            case 2:
                            case 3:
                                // 处方明细冲正
                                feeItemRecord.RecordFlag = 1;
                                feeItemRecord.ChargeDate = DateTime.Now;
                                this.BindDb(feeItemRecord);
                                feeItemRecord.save();

                                // 冲账时产生负数记录
                                feeItemRecord = feeItemRecord.Clone() as IP_FeeItemRecord;
                                feeItemRecord.OldFeeRecordID = feeRecordID;
                                feeItemRecord.FeeRecordID = 0;
                                feeItemRecord.RecordFlag = 2;
                                feeItemRecord.ChargeDate = DateTime.Now;
                                feeItemRecord.Amount = 0 - feeItemRecord.Amount;
                                feeItemRecord.TotalFee = 0 - feeItemRecord.TotalFee;
                                this.BindDb(feeItemRecord);
                                feeItemRecord.save();
                                break;
                            // 组合项目/医嘱
                            case 4:
                            case 5:
                            default:
                                break;
                        }
                    }
                    else
                    {
                        // 已发药
                        switch (feeClass)
                        {
                            // 药品
                            case 1:
                                // 处方明细冲正
                                feeItemRecord.RecordFlag = 1;
                                feeItemRecord.ChargeDate = DateTime.Now;
                                this.BindDb(feeItemRecord);
                                feeItemRecord.save();

                                // 冲账时产生负数记录
                                feeItemRecord = feeItemRecord.Clone() as IP_FeeItemRecord;
                                feeItemRecord.OldFeeRecordID = feeRecordID;
                                feeItemRecord.FeeRecordID = 0;
                                feeItemRecord.RecordFlag = 2;
                                feeItemRecord.ChargeDate = DateTime.Now;
                                feeItemRecord.Amount = 0 - feeItemRecord.Amount;
                                feeItemRecord.TotalFee = 0 - feeItemRecord.TotalFee;
                                this.BindDb(feeItemRecord);
                                feeItemRecord.save();
                                break;
                            // 材料/项目
                            case 2:
                            case 3:
                                // 项目已做不能冲账
                                strMsg.Append("[" + strikeABalanceDt.Rows[i]["ItemName"].ToString() + "]、");
                                break;
                            // 组合项目/医嘱
                            case 4:
                            case 5:
                            default:
                                break;
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(strMsg.ToString()))
            {
                msgList.Add(strMsg.ToString());
                isStrikeABalance = false;
            }

            return isStrikeABalance;
        }

        /// <summary>
        /// 取消冲账
        /// </summary>
        /// <param name="strikeABalanceDt">取消冲账的数据</param>
        /// <param name="msgList">错误消息</param>
        /// <returns>true：取消冲账成功</returns>
        public bool CancelStrikeABalance(DataTable strikeABalanceDt, List<string> msgList)
        {
            // 项目已做不能冲账提示消息。
            StringBuilder strMsg = new StringBuilder();
            bool isStrikeABalance = true;
            if (strikeABalanceDt != null && strikeABalanceDt.Rows.Count > 0)
            {
                for (int i = 0; i < strikeABalanceDt.Rows.Count; i++)
                {
                    int feeRecordID = Convert.ToInt32(strikeABalanceDt.Rows[i]["FeeRecordID"].ToString());
                    DataTable feeItemRecordDt = NewDao<IIPManageDao>().GetIPFeeItemRecordInfo(feeRecordID);
                    IP_FeeItemRecord feeItemRecord = ConvertExtend.ToObject<IP_FeeItemRecord>(feeItemRecordDt, 0);
                    // 检查药品是否已发药、检查项目是否已做检查
                    bool result = NewDao<IIPManageDao>().CheckIsMedicine(Convert.ToInt32(strikeABalanceDt.Rows[i]["FeeRecordID"].ToString()));
                    // 取得账单类型
                    int feeClass = Convert.ToInt32(strikeABalanceDt.Rows[i]["FeeClass"].ToString());
                    // 未发药
                    if (result)
                    {
                        switch (feeClass)
                        {
                            // 药品
                            case 1:
                                // 处方明细取消冲正
                                try
                                {
                                    // 减去有效库存
                                    NewObject<DrugStoreManagement>().UpdateStorage(feeItemRecord.ItemID, feeItemRecord.ExecDeptID, feeItemRecord.Amount);
                                }
                                catch (Exception ex)
                                {
                                    if (ex.Message.Contains("库存数不够"))
                                    {
                                        strMsg.Append("[" + feeItemRecord.ItemName + "]、");
                                        continue;
                                    }
                                }
                                // 将选中记录改成9，（9=已取消冲账）
                                feeItemRecord.RecordFlag = 9;
                                feeItemRecord.ChargeDate = DateTime.Now;
                                this.BindDb(feeItemRecord);
                                feeItemRecord.save();
                                // 重新走记账流程重新生成费用数据
                                feeItemRecord = feeItemRecord.Clone() as IP_FeeItemRecord;
                                feeItemRecord.OldFeeRecordID = 0;
                                feeItemRecord.FeeRecordID = 0;
                                feeItemRecord.RecordFlag = 0;
                                feeItemRecord.ChargeDate = DateTime.Now;
                                feeItemRecord.Amount = Math.Abs(feeItemRecord.Amount);
                                feeItemRecord.TotalFee = Math.Abs(feeItemRecord.TotalFee);
                                this.BindDb(feeItemRecord);
                                feeItemRecord.save();
                                break;
                            // 材料/项目
                            case 2:
                            case 3:
                                // 处方明细取消冲账
                                // 将选中记录改成9，（9=已取消冲账）
                                feeItemRecord.RecordFlag = 9;
                                feeItemRecord.ChargeDate = DateTime.Now;
                                this.BindDb(feeItemRecord);
                                feeItemRecord.save();
                                // 重新走记账流程重新生成费用数据
                                feeItemRecord = feeItemRecord.Clone() as IP_FeeItemRecord;
                                feeItemRecord.OldFeeRecordID = 0;
                                feeItemRecord.FeeRecordID = 0;
                                feeItemRecord.RecordFlag = 0;
                                feeItemRecord.ChargeDate = DateTime.Now;
                                if (feeItemRecord.Amount < 0)
                                {
                                    feeItemRecord.Amount = Math.Abs(feeItemRecord.Amount);
                                }

                                feeItemRecord.TotalFee = Math.Abs(feeItemRecord.TotalFee);
                                this.BindDb(feeItemRecord);
                                feeItemRecord.save();
                                break;
                            // 组合项目/医嘱
                            case 4:
                            case 5:
                            default:
                                break;
                        }
                    }
                    else
                    {
                        // 已发药
                        switch (feeClass)
                        {
                            // 药品
                            case 1:
                                // 处方明细取消冲正
                                try
                                {
                                    // 减去有效库存
                                    NewObject<DrugStoreManagement>().UpdateStorage(feeItemRecord.ItemID, feeItemRecord.ExecDeptID, feeItemRecord.Amount);
                                }
                                catch (Exception ex)
                                {
                                    if (ex.Message.Contains("库存数不够"))
                                    {
                                        strMsg.Append("[" + feeItemRecord.ItemName + "]、");
                                        continue;
                                    }
                                }
                                // 将选中记录改成9，（9=已取消冲账）
                                feeItemRecord.RecordFlag = 9;
                                feeItemRecord.ChargeDate = DateTime.Now;
                                this.BindDb(feeItemRecord);
                                feeItemRecord.save();
                                feeItemRecord = feeItemRecord.Clone() as IP_FeeItemRecord;
                                feeItemRecord.OldFeeRecordID = 0;
                                feeItemRecord.FeeRecordID = 0;
                                feeItemRecord.RecordFlag = 0;
                                feeItemRecord.DrugFlag = 0;
                                feeItemRecord.ChargeDate = DateTime.Now;
                                feeItemRecord.Amount = Math.Abs(feeItemRecord.Amount);
                                feeItemRecord.TotalFee = Math.Abs(feeItemRecord.TotalFee);
                                this.BindDb(feeItemRecord);
                                feeItemRecord.save();
                                break;
                            // 材料/项目
                            case 2:
                            case 3:
                                // 项目已做不能冲账
                                // 提示消息
                                strMsg.Append("[" + strikeABalanceDt.Rows[i]["ItemName"].ToString() + "]、");
                                break;
                            // 组合项目/医嘱
                            case 4:
                            case 5:
                            default:
                                break;
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(strMsg.ToString()))
            {
                msgList.Add(strMsg.ToString());
                isStrikeABalance = false;
            }

            return isStrikeABalance;
        }

        #endregion

        /// <summary>
        /// 停用账单
        /// </summary>
        /// <param name="stopFeeDt">需要停用的账单列表</param>
        /// <returns>错误消息</returns>
        public string StopFeeLongOrderData(DataTable stopFeeDt)
        {
            try
            {
                if (stopFeeDt.Rows.Count > 0)
                {
                    for (int i = 0; i < stopFeeDt.Rows.Count; i++)
                    {
                        NewDao<IIPManageDao>().StopFeeLongOrderData(Convert.ToInt32(stopFeeDt.Rows[i]["GenerateID"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return string.Empty;
        }
    }
}
