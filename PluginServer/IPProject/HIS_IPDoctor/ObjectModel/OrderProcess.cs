using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_Entity.IPManage;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPDoctor.ObjectModel
{
    /// <summary>
    /// 医嘱处理业务对象类
    /// </summary>
    public class OrderProcess : AbstractObjectModel
    {
        /// <summary>
        /// 医嘱操作
        /// </summary>
        /// <param name="records">医嘱数据</param>
        /// <param name="operatorType">//1医嘱删除 2医嘱发送 3医嘱停嘱 4医嘱取消停</param>
        /// <param name="empid">操作员ID</param>
        public void OperatorOrder(List<IPD_OrderRecord> records, int operatorType, int empid)
        {
            foreach (IPD_OrderRecord record in records)
            {
                IPD_OrderRecord newRecord = NewObject<IPD_OrderRecord>().getmodel(record.OrderID) as IPD_OrderRecord;
                if (operatorType == 1)
                {
                    if (newRecord.OrderStatus > 1)
                    {
                        throw new Exception("已经转抄，不能删除");
                    }

                    spcialOrderDelete(record, empid);
                    //皮试医嘱删除，连带皮试医嘱产生的关联医嘱一起删除
                    if (record.AstFlag == 0)
                    {
                        //已经转抄的不能删除
                        List<IPD_OrderRecord> astRecords = NewObject<IPD_OrderRecord>().getlist<IPD_OrderRecord>("AstOrderID=" + newRecord.OrderID + " and OrderStatus<=1");
                        foreach (IPD_OrderRecord astRecord in astRecords)
                        {
                            List<IPD_OrderRecord> astSameRecords = NewObject<IPD_OrderRecord>().getlist<IPD_OrderRecord>("GroupID=" + astRecord.GroupID + " and OrderStatus<=1");
                            foreach (IPD_OrderRecord astSameRecord in astSameRecords)
                            {
                                astSameRecord.DeleteFlag = 1;
                                this.BindDb(astSameRecord);
                                astSameRecord.save();
                            }
                            //原来存在费用，原来一组费用删除
                            List<IP_FeeItemGenerate> feeItems = NewObject<IP_FeeItemGenerate>().getlist<IP_FeeItemGenerate>(" GroupID=" + astRecord.GroupID);
                            if (feeItems.Count > 0)
                            {
                                foreach (IP_FeeItemGenerate fee in feeItems)
                                {
                                    this.BindDb(fee);
                                    fee.delete();
                                }
                            }
                        }
                    }

                    //删除的是关联皮试医嘱时，要把原来的医嘱改为免试
                    if (record.AstOrderID > 0)
                    {
                        IPD_OrderRecord aOrderRecord = NewObject<IPD_OrderRecord>().getmodel(record.AstOrderID) as IPD_OrderRecord;
                        aOrderRecord.AstFlag = 3;
                        aOrderRecord.save();
                    }
                }
                else if (operatorType == 3)
                {
                    if (newRecord.OrderStatus != 2)
                    {
                        throw new Exception("不是已经转抄长嘱，不能停嘱");
                    }
                }
                else if (operatorType == 4)
                {
                    if (newRecord.OrderStatus > 3)
                    {
                        throw new Exception("停嘱已经转抄，不能取消停嘱");
                    }
                }

                this.BindDb(record);
                record.save();

                //医嘱删除时，医嘱费用同时删除
                if (operatorType == 1)
                {
                    if (record.OrderID > 0)
                    {
                        //原来存在费用，原来费用删除
                        List<IP_FeeItemGenerate> feeItems = NewObject<IP_FeeItemGenerate>().getlist<IP_FeeItemGenerate>(" OrderID=" + record.OrderID + " and groupid=" + record.GroupID);
                        if (feeItems.Count > 0)
                        {
                            foreach (IP_FeeItemGenerate fee in feeItems)
                            {
                                this.BindDb(fee);
                                fee.delete();
                            }
                        }
                    }
                }
            }

     
        }

        /// <summary>
        /// 特列说明性医嘱删除
        /// </summary>
        /// <param name="record">特列说明性医嘱</param>
        /// <param name="empid">操作员ID</param>
        private void spcialOrderDelete(IPD_OrderRecord record, int empid)
        {
            if (record.OrderType == 7)
            {
                //转科医嘱
                List<IPD_TransDept> trans = NewObject<IPD_TransDept>().getlist<IPD_TransDept>(" orderid=" + record.OrderID + " and patlistid=" + record.PatListID + " and cancelFlag=0 and finishflag=0");
                if (trans == null || trans.Count == 0)
                {
                    throw new Exception("转科信息不存在或已经完成，不能删除");
                }

                IPD_TransDept tranDept = trans[0];
                tranDept.CancelFlag = 1;
                tranDept.CancelDate = DateTime.Now;
                tranDept.CancelEmpID = empid;
                this.BindDb(tranDept);
                tranDept.save();
                //医嘱自动停的恢复
                List<IPD_OrderRecord> records = NewObject<IPD_OrderRecord>().getlist<IPD_OrderRecord>(" patlistid=" + record.PatListID + " and orderStatus=3 and AutoEndFlag=1");
                foreach (IPD_OrderRecord ipdrecord in records)
                {
                    ipdrecord.OrderStatus = 2;
                    ipdrecord.AutoEndFlag = 0;
                    ipdrecord.EOrderDoc = 0;
                    ipdrecord.EOrderDate = Convert.ToDateTime("1900-01-01 00:00:00.000");
                    ipdrecord.TeminalNum = 0;
                    this.BindDb(ipdrecord);
                    ipdrecord.save();
                }
            }
            else if (record.OrderType == 6 || record.OrderType == 5)
            {
                //医嘱自动停的恢复
                List<IPD_OrderRecord> records = NewObject<IPD_OrderRecord>().getlist<IPD_OrderRecord>(" patlistid=" + record.PatListID + " and orderStatus=3 and AutoEndFlag=1");
                foreach (IPD_OrderRecord ipdrecord in records)
                {
                    ipdrecord.OrderStatus = 2;
                    ipdrecord.AutoEndFlag = 0;
                    ipdrecord.EOrderDoc = 0;
                    ipdrecord.EOrderDate = Convert.ToDateTime("1900-01-01 00:00:00.000");
                    ipdrecord.TeminalNum = 0;
                    this.BindDb(ipdrecord);
                    ipdrecord.save();
                }

                IP_PatList plist = NewObject<IP_PatList>().getmodel(record.PatListID) as IP_PatList;
                //plist.Status = 2;
                plist.IsLeaveHosOrder = 0;
                plist.LeaveHDate = Convert.ToDateTime("1900-01-01 00:00:00.000");
                plist.OutSituation = "0";
                this.BindDb(plist);
                plist.save();
                List<IPD_Diagnosis> diags = NewObject<IPD_Diagnosis>().getlist<IPD_Diagnosis>(" orderid=" + record.OrderID);
                foreach (IPD_Diagnosis diag in diags)
                {
                    diag.delete();
                }
            }
        }       

        /// <summary>
        /// 医嘱保存
        /// </summary>
        /// <param name="records">医嘱数据</param>
        public void SaveRecords(List<IPD_OrderRecord> records)
        {            
            IP_PatList ipPatlist = NewObject<IP_PatList>().getmodel(records[0].PatListID) as IP_PatList;
            if (ipPatlist.Status != 2 || ipPatlist.IsLeaveHosOrder == 1)
            {
                throw new Exception("病人已经出院,不能再保存医嘱");
            }

            //判断病人是否存在未完成的转科医嘱
            List<IPD_TransDept> trans = NewObject<IPD_TransDept>().getlist<IPD_TransDept>(" Patlistid=" + records[0].PatListID + " and CancelFlag=0 and FinishFlag=0");
            if (trans != null && trans.Count > 0)
            {
                throw new Exception("病人已转科,不能再保存医嘱");
            }

            FeeItemDataSource feeitem = NewObject<FeeItemDataSource>();
            DataTable dtDrugItem = feeitem.GetFeeItemDataDt(FeeBusinessType.医嘱业务);
            List<int> groupIDs = new List<int>();
            groupIDs.Add(records[0].GroupID);
            for (int i = 1; i < records.Count; i++)
            {
                if (records[i].GroupID != records[i - 1].GroupID)
                {
                    groupIDs.Add(records[i].GroupID);
                }
            }

            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].OrderID > 0)
                {
                    //原来存在费用，原来一组费用删除，重新生成
                    List<IP_FeeItemGenerate> feeItems = NewObject<IP_FeeItemGenerate>().getlist<IP_FeeItemGenerate>(" GroupId=" + records[i].GroupID + " and orderid=" + records[i].OrderID);
                    if (feeItems.Count > 0)
                    {
                        foreach (IP_FeeItemGenerate fee in feeItems)
                        {
                            this.BindDb(fee);
                            fee.delete();
                        }
                    }
                }
            }
            for (int i = 0; i < groupIDs.Count; i++)
            {
                int groupid = groupIDs[i];
                List<IPD_OrderRecord> sameGroupRecord = records.Where(p => p.GroupID == groupid).ToList();
                #region 如果已经转抄，就不能修改 按组转抄，只要判断一条医嘱就可
                if (sameGroupRecord[0].OrderStatus <= 1 && sameGroupRecord[0].OrderID > 0)
                {
                    IPD_OrderRecord newRecord = NewObject<IPD_OrderRecord>().getmodel(sameGroupRecord[0].OrderID) as IPD_OrderRecord;
                    if (newRecord.OrderStatus > 1)
                    {
                        //如果已经转抄，就不能修改
                        break;
                    }
                }
                #endregion
                //原来存在费用，原来一组费用删除，重新生成
                //List<IP_FeeItemGenerate> feeItems = NewObject<IP_FeeItemGenerate>().getlist<IP_FeeItemGenerate>(" GroupID=" + groupid );
                //if (feeItems.Count > 0)
                //{
                //    foreach (IP_FeeItemGenerate fee in feeItems)
                //    {
                //        this.BindDb(fee);
                //        fee.delete();
                //    }
                //}

                foreach (IPD_OrderRecord record in sameGroupRecord)
                {
                    #region 医嘱保存
                    if (record.OrderStatus <= 1 && record.ItemName != string.Empty)
                    {
                        record.PatDeptID = ipPatlist.CurrDeptID;
                        record.WardID = ipPatlist.CurrWardID;
                        this.BindDb(record);
                        record.save();
                    }
                    #endregion
                    GenerateFee(record, dtDrugItem, ipPatlist);
                    if (record.AstFlag == 0)
                    {
                        //需要皮试，保存时增加皮试医嘱
                        InsertPSYZ(dtDrugItem, record, ipPatlist);
                    }
                }

                if (sameGroupRecord[0].ChannelID == 0)
                {
                    continue;
                }
                #region 用法关联费用明细生成
                GenerateChannelFee(sameGroupRecord[0], dtDrugItem, ipPatlist);
                #endregion
            }        
        }

        /// <summary>
        /// 插入皮试医嘱
        /// </summary>
        /// <param name="dtDrugItem">药品项目数据</param>
        /// <param name="astOrderRecord">皮试医嘱</param>
        /// <param name="ipPatlist">病人对象</param>
        private void InsertPSYZ(DataTable dtDrugItem, IPD_OrderRecord astOrderRecord, IP_PatList ipPatlist)
        {
            DataRow[] rows = dtDrugItem.Select(" ItemID=" + astOrderRecord.ItemID );
            if (rows.Length <= 0)
            {
                return;
            }

            DataRow row = rows[0];
            List<IPD_OrderRecord> data = new List<IPD_OrderRecord>();
            IPD_OrderRecord record = new IPD_OrderRecord();
            record.ItemID = Convert.ToInt32(row["itemid"]);
            record.ItemName = row["itemname"].ToString();
            record.ChannelName = "皮试";
            record.ChannelID = 34;
            record.FrenquencyID = 22;
            record.Frequency = "q.d.";
            record.Amount = 1;
            record.Unit = row["MiniUnitName"].ToString();
            record.UnitNO = 1;
            record.StatID = Convert.ToInt32(row["StatID"]);
            record.DoseNum = 1;
            record.OrderBdate = DateTime.Now;
            record.OrderCategory = 1;
            record.OrderType = 0;
            record.Dosage = 1;
            record.AstOrderID = astOrderRecord.OrderID;
            record.Factor = Convert.ToDecimal(row["DoseConvertNum"]);
            record.ItemType = Convert.ToInt32(row["ItemClass"]);
            record.ExecDeptID = astOrderRecord.ExecDeptID;
            record.AstFlag = 0;
            record.ItemPrice = Convert.ToDecimal(row["SellPrice"]);
            record.OrderStatus = 0;
            record.PatListID = astOrderRecord.PatListID;
            record.PresDeptID = astOrderRecord.PresDeptID;
            record.OrderDoc = astOrderRecord.OrderDoc;
            record.OrderID = 0;
            record.PatDeptID = ipPatlist.CurrDeptID;
            record.WardID = ipPatlist.CurrWardID;
            SerialNumberSource serialNumberSource = NewObject<SerialNumberSource>();
            string groupID = serialNumberSource.GetSerialNumber(SnType.医嘱组号);
            record.GroupID = Convert.ToInt32(groupID);
            this.BindDb(record);
            record.save();
            GenerateFee(record, dtDrugItem, ipPatlist);
            GenerateChannelFee(record, dtDrugItem, ipPatlist);
            int actDrugid = Convert.ToInt32(_getActDrugId());
            if (actDrugid != 0)
            {
                DataRow[] drugrows = dtDrugItem.Select("ItemID=" + actDrugid );
                if (drugrows.Length > 0)
                {
                    IPD_OrderRecord recordDrug = new IPD_OrderRecord();
                    recordDrug.ItemID = Convert.ToInt32(drugrows[0]["itemid"]);
                    recordDrug.ItemName = drugrows[0]["itemname"].ToString();
                    recordDrug.ChannelName = record.ChannelName;
                    recordDrug.ChannelID = record.ChannelID;
                    recordDrug.FrenquencyID = record.FrenquencyID;
                    recordDrug.OrderBdate = record.OrderBdate;
                    recordDrug.Frequency = record.Frequency;
                    recordDrug.Amount = 1;
                    recordDrug.Unit = rows[0]["MiniUnitName"].ToString();
                    recordDrug.UnitNO = 1;
                    recordDrug.StatID = Convert.ToInt32(rows[0]["StatID"]);
                    recordDrug.DoseNum = 1;
                    recordDrug.OrderStatus = 0;
                    recordDrug.OrderCategory = 1;
                    recordDrug.OrderType = 0;
                    recordDrug.GroupID = record.GroupID;
                    recordDrug.Dosage = 1;
                    recordDrug.AstOrderID = astOrderRecord.OrderID;
                    recordDrug.Factor = Convert.ToDecimal(rows[0]["DoseConvertNum"]);
                    recordDrug.ItemType = Convert.ToInt32(rows[0]["ItemClass"]);
                    recordDrug.ExecDeptID = astOrderRecord.ExecDeptID;// Convert.ToInt32(rows[0]["ExecDeptId"]);
                    recordDrug.AstFlag = -1;
                    recordDrug.ItemPrice = Convert.ToDecimal(rows[0]["SellPrice"]);
                    recordDrug.PatListID = ipPatlist.PatListID;
                    recordDrug.PresDeptID = record.PresDeptID;
                    recordDrug.OrderDoc = record.OrderDoc;
                    recordDrug.OrderID = 0;
                    recordDrug.Memo = "PsDrug";
                    recordDrug.PatDeptID = ipPatlist.CurrDeptID;
                    recordDrug.WardID = ipPatlist.CurrWardID;
                    this.BindDb(recordDrug);
                    recordDrug.save();
                    GenerateFee(recordDrug, dtDrugItem, ipPatlist);
                }
            }
        }

        /// <summary>
        /// 获取皮试对应的皮试用药ID
        /// </summary>
        /// <returns>皮试用药ID</returns>
        private string _getActDrugId()
        {
            string actDrugId = NewObject<SysConfigManagement>().GetSystemConfigValue("ActOrderDrugID");
            return actDrugId;
        }

        #region 医嘱费用
        /// <summary>
        /// 医嘱本身费用
        /// </summary>
        /// <param name="record">医嘱对象</param>
        /// <param name="dtDrugItem">药品项目数据</param>
        /// <param name="ipPatlist">病人对象</param>
        private void GenerateFee(IPD_OrderRecord record, DataTable dtDrugItem, IP_PatList ipPatlist)
        {
            if (record.ItemType == 4)
            {
                #region 组合项目明细费用生成
                FeeItemDataSource feeDataSource = NewObject<FeeItemDataSource>();
                DataTable dtDetailItem = feeDataSource.GetExamItemDetailDt(record.ItemID);
                for (int index = 0; index < dtDetailItem.Rows.Count; index++)
                {
                    int feeitemId = Convert.ToInt32(dtDetailItem.Rows[index]["ITEMID"]);
                    decimal amount = Convert.ToDecimal(dtDetailItem.Rows[index]["ItemAmount"]);
                    DataRow[] rows = dtDrugItem.Select(" ItemID=" + feeitemId);
                    if (rows.Length < 1)
                    {
                        continue;
                    }

                    IP_FeeItemGenerate feeItem = GenerateRecordFee(amount, 1, record, ipPatlist, rows[0]);
                    feeItem.FeeSource = 1;
                    feeItem.CalCostMode = 0;
                    this.BindDb(feeItem);
                    feeItem.save();
                }
                #endregion
            }
            else
            {
                #region 医嘱本身费用生成
                if (record.ItemID <= 0 || record.StatID == 0 || record.ItemType == 5)
                {
                    return;
                }

                DataRow[] rows = dtDrugItem.Select(" ItemID=" + record.ItemID);
                if (rows.Length < 1)
                {
                    return;
                }

                IP_FeeItemGenerate feeItem = GenerateRecordFee(record.Amount * record.UnitNO, Convert.ToInt32(record.DoseNum), record, ipPatlist, rows[0]);
                feeItem.FeeSource = 0;
                feeItem.CalCostMode = 0;
                this.BindDb(feeItem);
                feeItem.save();
                #endregion
            }
        }

        /// <summary>
        /// 一组医嘱用法关联的费用
        /// </summary>
        /// <param name="record">医嘱对象</param>
        /// <param name="dtDrugItem">药品项目数据</param>
        /// <param name="ipPatlist">病人对象</param>
        private void GenerateChannelFee(IPD_OrderRecord record, DataTable dtDrugItem, IP_PatList ipPatlist)
        {
            #region 用法关联费用明细生成
            //用法关联费用明细
            List<Basic_ChannelFee> channelFees = NewObject<Basic_ChannelFee>().getlist<Basic_ChannelFee>("ChannelID=" + record.ChannelID);
            foreach (Basic_ChannelFee chanelFee in channelFees)
            {
                DataRow[] rows = dtDrugItem.Select(" ItemID=" + chanelFee.ItemID);
                if (rows.Length < 1)
                {
                    continue;
                }

                IP_FeeItemGenerate feeItem = GenerateRecordFee(chanelFee.ItemAmount, 1, record, ipPatlist, rows[0]);
                feeItem.FeeSource = 2;
                feeItem.CalCostMode = chanelFee.CalCostMode;
                this.BindDb(feeItem);
                feeItem.save();
            }
            #endregion
        }

        /// <summary>
        /// 医嘱费用生成
        /// </summary>
        /// <param name="amount">数量</param>
        /// <param name="doseNum">付数</param>
        /// <param name="record">医嘱对象</param>
        /// <param name="ipPatlist">病人对象</param>
        /// <param name="row">药品项目明细</param>
        /// <returns>费用对象</returns>
        private IP_FeeItemGenerate GenerateRecordFee(decimal amount, int doseNum, IPD_OrderRecord record, IP_PatList ipPatlist, DataRow row)
        {
            IP_FeeItemGenerate feeItem = new IP_FeeItemGenerate();
            feeItem.PatListID = record.PatListID;
            feeItem.PatName = ipPatlist.PatName;
            feeItem.PatDeptID = ipPatlist.CurrDeptID;
            feeItem.PatDoctorID = ipPatlist.CurrDoctorID;
            feeItem.PatNurseID = ipPatlist.CurrNurseID;
            feeItem.BabyID = 0;
            feeItem.ItemID = Convert.ToInt32(row["ItemID"]);
            feeItem.ItemName = row["ItemName"].ToString();
            feeItem.FeeClass = Convert.ToInt32(row["ItemClass"]);
            feeItem.StatID = Convert.ToInt32(row["StatID"]);
            feeItem.Spec = row["Standard"].ToString();
            feeItem.Unit = row["MiniUnitName"].ToString();
            feeItem.PackUnit = row["MiniUnitName"].ToString();
            feeItem.PackAmount = amount;
            feeItem.InPrice = Convert.ToDecimal(row["InPrice"]);
            feeItem.SellPrice = Convert.ToDecimal(row["SellPrice"]);
            feeItem.Amount = Convert.ToInt32(amount);
            feeItem.DoseAmount = doseNum;
            feeItem.TotalFee = (Convert.ToDecimal(row["SellPrice"]) * feeItem.Amount) / Convert.ToDecimal(row["MiniConvertNum"]);
            //if (record.OrderType == 2)
            //{
            //    feeItem.TotalFee = 0;
            //    feeItem.Amount = 0;
            //}
            //else
            //{
            //    feeItem.TotalFee = (Convert.ToDecimal(row["SellPrice"]) * feeItem.Amount)/Convert.ToDecimal( row["MiniConvertNum"]);
            //}
            feeItem.PresDeptID = record.PresDeptID;
            feeItem.PresDoctorID = record.OrderDoc;
            if (Convert.ToInt32(row["ExecDeptId"]) > 0)
            {
                feeItem.ExecDeptDoctorID = record.ExecDeptID;// Convert.ToInt32(row["ExecDeptId"]);
            }
            else
            {
                feeItem.ExecDeptDoctorID = record.PresDeptID;
            }

            feeItem.PresDate = record.OrderBdate;
            feeItem.MarkDate = record.OrderBdate;
            feeItem.MarkEmpID = record.OrderDoc;
            feeItem.OrderID = record.OrderID;
            feeItem.OrderType = record.OrderCategory;
            feeItem.FrequencyID = record.FrenquencyID;
            feeItem.FrequencyName = record.Frequency;
            feeItem.ChannelName = record.ChannelName;
            feeItem.ChannelID = record.ChannelID;
            feeItem.GroupID = record.GroupID;
            return feeItem;
        }
        #endregion

       /// <summary>
       /// 转科医嘱
       /// </summary>
       /// <param name="patlistid">病人ID</param>
       /// <param name="list">需自动停嘱对象</param>
       /// <param name="transDate">转科日期</param>
       /// <param name="transDeptID">转科科室</param>
       /// <param name="oprator">操作员</param>
       /// <param name="spciRecord">转科医嘱对象</param>
        public void TransDeptOrder(int patlistid, List<IPD_OrderRecord> list,DateTime transDate, int transDeptID, int oprator, IPD_OrderRecord spciRecord)
        {
            //医嘱自动停保存
            foreach (IPD_OrderRecord record in list)
            {
                this.BindDb(record);
                record.save();
            }

            IP_PatList patlist = NewObject<IP_PatList>().getmodel(patlistid) as IP_PatList;
            if (patlist.Status != 2 || patlist.IsLeaveHosOrder == 1)
            {
                throw new Exception("病人状态已经不在床或已经开出院医嘱，不能开转科医嘱");
            }

            List<IPD_TransDept> listTrans = NewObject<IPD_TransDept>().getlist<IPD_TransDept>(" patlistid=" + patlistid + " and cancelFlag=0 and finishFlag=0");
            if (listTrans.Count > 0)
            {
                throw new Exception("该病人存在未完成的转科医嘱，不能再开转科医嘱");
            }

            SerialNumberSource serialNumberSource = NewObject<SerialNumberSource>();
            string groupID = serialNumberSource.GetSerialNumber(SnType.医嘱组号);
            //生成说明性临嘱保存
            spciRecord.GroupID = Convert.ToInt32(groupID);
            spciRecord.PatDeptID = patlist.CurrDeptID;
            spciRecord.WardID = patlist.CurrWardID;
            spciRecord.Dosage = 1;
            this.BindDb(spciRecord);
            spciRecord.save();
            //转入信息保存
            IPD_TransDept transDept = new IPD_TransDept();
            transDept.Operator = oprator;
            transDept.PatListID = patlistid;
            transDept.OldDeptID = patlist.CurrDeptID;
            transDept.NewDeptID = transDeptID;
            transDept.TransDate = transDate;
            transDept.OrderID = spciRecord.OrderID;
            transDept.OperDate = DateTime.Now;
            transDept.FinishFlag = 0;
            this.BindDb(transDept);
            transDept.save();
        }
      
        /// <summary>
        /// 出院/死亡医嘱
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <param name="list">需自动停嘱对象</param>
        /// <param name="outDate">出院日期</param>
        /// <param name="outDiseaseName">出院诊断</param>
        /// <param name="outDiseaseCode">诊断编码</param>
        /// <param name="outSituation">出院情况</param>
        /// <param name="oprator">操作员</param>
        /// <param name="spciRecord">出院医嘱</param>
        public void OutHospOrder(int patlistid, List<IPD_OrderRecord> list, DateTime outDate, string outDiseaseName, string outDiseaseCode, string outSituation, int oprator, IPD_OrderRecord spciRecord)
        {
            //医嘱自动停保存
            foreach (IPD_OrderRecord record in list)
            {
                this.BindDb(record);
                record.save();
            }

            IP_PatList patlist = NewObject<IP_PatList>().getmodel(patlistid) as IP_PatList;
            if (patlist.Status != 2 || patlist.IsLeaveHosOrder == 1)
            {
                throw new Exception("病人状态已经不在床或已经开出院医嘱，不能开出院医嘱");
            }

            SerialNumberSource serialNumberSource = NewObject<SerialNumberSource>();
            string groupID = serialNumberSource.GetSerialNumber(SnType.医嘱组号);
            //生成说明性临嘱保存
            spciRecord.GroupID = Convert.ToInt32(groupID);
            spciRecord.PatDeptID = patlist.CurrDeptID;
            spciRecord.WardID = patlist.CurrWardID;
            spciRecord.Dosage = 1;
            this.BindDb(spciRecord);
            spciRecord.save();
            //病人状态修改
            // patlist.Status = 5;
            patlist.IsLeaveHosOrder = 1;
            patlist.OutSituation = outSituation;
            patlist.LeaveHDate = outDate;
            this.BindDb(patlist);
            patlist.save();
            //诊断表保存
            IPD_Diagnosis ipdDiag = new IPD_Diagnosis();
            ipdDiag.PatListID = patlist.PatListID;
            ipdDiag.DeptID = patlist.CurrDeptID;
            ipdDiag.DgsDocID = oprator;
            ipdDiag.DiagnosisTime = DateTime.Now;
            ipdDiag.DiagnosisName = outDiseaseName;
            ipdDiag.ICDCode = outDiseaseCode;
            ipdDiag.OrderID = spciRecord.OrderID;
            if (spciRecord.OrderType == 5)
            {
                //出院诊断
                ipdDiag.DiagnosisClass = 67334;
            }
            else if (spciRecord.OrderType == 6)
            {
                //死亡诊断
                ipdDiag.DiagnosisClass = 67342;
            }

            this.BindDb(ipdDiag);
            ipdDiag.save();
        }
    }
}
