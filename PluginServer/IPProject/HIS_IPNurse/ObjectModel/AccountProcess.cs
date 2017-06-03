using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.Common;
using HIS_Entity.IPManage;
using HIS_IPManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPNurse.ObjectModel
{
    /// <summary>
    /// 医嘱费用核对
    /// </summary>
    public class AccountProcess : AbstractObjectModel
    {
        /// <summary>
        /// 账单记账
        /// </summary>
        /// <param name="feeItemAccDt">待记账数据集合</param>
        /// <param name="empID">操作员ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isBedFee">是否记床位费</param>
        /// <param name="isLongFee">是否记长期账单</param>
        /// <param name="msgList">错误消息</param>
        /// <returns>true：记账成功</returns>
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
            // 写入处方明细表数据
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
                // 组合项目时，根据基本数计算=基本数*划价系数
                //feeItemRecord.TotalFee = Math.Round(feeItemRecord.Amount * Convert.ToDecimal(feeDr["PackAmount"].ToString()), 2);
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

        /// <summary>
        /// 保存记账数据
        /// </summary>
        /// <param name="feeItem">处方生成对象</param>
        /// <param name="feeItemAccDt">处方生成列表</param>
        /// <param name="rowIndex">处方生成Index</param>
        /// <param name="interval">处方天数</param>
        /// <param name="presDate">处方开始日期</param>
        /// <param name="msgList">错误消息</param>
        /// <returns>true：记账成功</returns>
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
                    //feeItemRecord.TotalFee = Math.Round(feeItemRecord.Amount * Convert.ToDecimal(FeeItemAccDt.Rows[rowIndex]["PackAmount"].ToString()), 2);
                    //feeItemRecord.PackAmount = Convert.ToDecimal(FeeItemAccDt.Rows[rowIndex]["PackAmount"].ToString());  // 划价系数
                    this.BindDb(feeItemRecord);
                    feeItemRecord.save();
                }
            }

            return true;
        }
    }
}