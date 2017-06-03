using System;
using System.Data;
using System.Linq;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_Entity.IPManage;
using HIS_OPDoctor.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_OPDoctor.ObjectModel
{
    /// <summary>
    /// 医技申请对象
    /// </summary>
    [Serializable]
    public class MedicalApply : AbstractObjectModel
    {
        /// <summary>
        /// 保存医技申请
        /// </summary>
        /// <param name="head">申请头实体</param>
        /// <param name="itemData">项目数据</param>
        /// <param name="dt">明细数据</param>
        /// <returns>申请头id</returns>
        public int SaveMedicalApply(EXA_MedicalApplyHead head, DataTable itemData, DataTable dt)
        {
            DataRow dr = null;
            head.Remark = GetItemName(itemData);
            DeleteData(itemData, dt);
            if (dt != null)
            {
                head.ApplyHeadID = Convert.ToInt32(dt.Rows[0]["ApplyHeadID"].ToString());
            }

            this.BindDb(head);
            head.save();
            int headid = head.ApplyHeadID;
            OPD_PresHead presHead = new OPD_PresHead();
            if (head.SystemType == 0)
            {
                if (dt != null)
                {
                    presHead.PresHeadID = Convert.ToInt32(dt.Rows[0]["PresHeadID"].ToString());
                }

                presHead.MemberID = head.MemberID;
                presHead.PatListID = head.PatListID;
                presHead.PresType = 4;
                BindDb(presHead);
                int presheadid = presHead.save();
                NewObject<PrescriptionProcess>().UpdatePatCurrentDoctorID(head.PatListID, head.ApplyDoctorID, head.ApplyDeptID);
            }

            for (int i = 0; i < itemData.Rows.Count; i++)
            {
                dr = NewDao<IOPDDao>().GetPresNO().Rows[0];
                int predetailid = 0;
                OPD_PresDetail presdetail = new OPD_PresDetail();
                IPD_OrderRecord orderRecord = new IPD_OrderRecord();
                DataRow detaildr = null;
                DataTable orderdt = NewDao<IOPDDao>().GetStatID(itemData.Rows[i]["ExamItemID"].ToString());
                if (dt != null)
                {
                    detaildr = dt.Select("ItemID=" + Convert.ToInt32(itemData.Rows[i]["ExamItemID"].ToString())).FirstOrDefault();
                }

                if (head.SystemType == 0)
                {
                    if (detaildr != null)
                    {
                        presdetail.PresDetailID = Convert.ToInt32(detaildr["PresDetailID"]);
                    }

                    if (head.ApplyType == 2)
                    {
                        presdetail.Price = Convert.ToDecimal(itemData.Rows[i]["Price"].ToString()) * Convert.ToInt32(itemData.Rows[i]["Amount"].ToString());
                        presdetail.ChargeAmount = Convert.ToInt32(itemData.Rows[i]["Amount"].ToString());
                        presdetail.PresAmount = Convert.ToInt32(itemData.Rows[i]["Amount"].ToString());
                    }
                    else
                    {
                        presdetail.Price = Convert.ToDecimal(itemData.Rows[i]["Price"].ToString());
                        presdetail.ChargeAmount = 1;
                        presdetail.PresAmount = 1;
                    }

                    presdetail.DoseNum = 1;
                    presdetail.PresHeadID = presHead.PresHeadID;
                    presdetail.ItemID = Convert.ToInt32(itemData.Rows[i]["ExamItemID"].ToString());
                    presdetail.ItemName = itemData.Rows[i]["ExamItemName"].ToString();
                    presdetail.ExecDeptID = head.ExecuteDeptID;
                    presdetail.StatID = Convert.ToInt32(orderdt.Rows[0]["StatID"]);
                    if (dr != null)
                    {
                        if (dr["PresNO"] == null || dr["PresNO"].ToString() == string.Empty)
                        {
                            presdetail.PresNO = 1;
                        }
                        else
                        {
                            presdetail.PresNO = Convert.ToInt32(dr["PresNO"]) + 1;
                        }
                    }

                    presdetail.IsEmergency = 0;
                    presdetail.IsLunacyPosion = 0;
                    presdetail.PresDate = head.ApplyDate;
                    presdetail.PresDeptID = head.ApplyDeptID;
                    presdetail.PresDoctorID = head.ApplyDoctorID;
                    BindDb(presdetail);
                    predetailid = presdetail.save();
                    if (presdetail.PresDetailID > 0)
                    {
                        predetailid = presdetail.PresDetailID;
                    }
                }
                else
                {
                    if (detaildr != null)
                    {
                        orderRecord.OrderID = Convert.ToInt32(detaildr["PresDetailID"]);
                    }

                    DataTable ipPatlist = NewDao<IOPDDao>().GetInBedPatient(head.PatListID);
                    SerialNumberSource serialNumberSource = NewObject<SerialNumberSource>();
                    string groupID = serialNumberSource.GetSerialNumber(SnType.医嘱组号);
                    orderRecord.GroupID = Convert.ToInt32(groupID);
                    orderRecord.ExecDeptID = head.ExecuteDeptID;
                    orderRecord.OrderCategory = 1;
                    orderRecord.CancelFlag = 0;
                    orderRecord.DeleteFlag = 0;
                    orderRecord.AstFlag = -1;
                    orderRecord.DoseNum = 1;
                    orderRecord.Dosage = 1;
                   // orderRecord.DosageUnit = itemData.Rows[i][""].ToString();
                    orderRecord.OrderStatus = 1;
                    orderRecord.PatDeptID = Convert.ToInt32(ipPatlist.Rows[0]["CurrDeptID"]);
                    orderRecord.WardID = Convert.ToInt32(ipPatlist.Rows[0]["EnterWardID"]);
                    orderRecord.StatID = Convert.ToInt32(orderdt.Rows[0]["StatID"]);
                    orderRecord.ItemID = Convert.ToInt32(itemData.Rows[i]["ExamItemID"].ToString());
                    orderRecord.PresDeptID = head.ApplyDeptID;
                    orderRecord.OrderDoc = head.ApplyDoctorID;
                    orderRecord.ItemName = itemData.Rows[i]["ExamItemName"].ToString();
                    orderRecord.Amount = Convert.ToInt32(itemData.Rows[i]["Amount"].ToString());
                    orderRecord.OrderBdate = head.CheckDate;  //DateTime.Now; 20170418改成检查时间
                    if (head.ApplyType == 2)
                    {
                        orderRecord.ItemPrice = Convert.ToDecimal(itemData.Rows[i]["Price"].ToString()) * Convert.ToInt32(itemData.Rows[i]["Amount"].ToString());
                    }
                    else
                    {
                        orderRecord.ItemPrice = Convert.ToDecimal(itemData.Rows[i]["Price"].ToString());
                    }

                    orderRecord.ItemType = 4;
                    orderRecord.PatListID = head.PatListID;
                    BindDb(orderRecord);
                    predetailid = orderRecord.save();
                    //插入费用明细
                    FeeItemDataSource feeDataSource = NewObject<FeeItemDataSource>();
                    DataTable dtDetailItem = feeDataSource.GetExamItemDetailDt(orderRecord.ItemID);

                    for (int index = 0; index < dtDetailItem.Rows.Count; index++)
                    {
                        int feeitemId = Convert.ToInt32(dtDetailItem.Rows[index]["ITEMID"]);
                        DataTable dtDrugItem = NewDao<IOPDDao>().GetFeeItemData(feeitemId);
                        decimal amount = Convert.ToDecimal(dtDetailItem.Rows[index]["ItemAmount"]);
                        DataRow[] rows = dtDrugItem.Select(" ItemID=" + feeitemId);
                        if (rows.Length < 1)
                        {
                            continue;
                        }

                        IP_FeeItemGenerate feeItem = GenerateRecordFee(amount, 1, orderRecord, ipPatlist, rows[0]);
                        feeItem.FeeSource = 1;
                        feeItem.CalCostMode = 0;
                        this.BindDb(feeItem);
                        feeItem.save();
                    }
                }

                EXA_MedicalApplyDetail detail = new EXA_MedicalApplyDetail();
                if (detaildr != null)
                {
                    detail.ApplyDetailID = Convert.ToInt32(detaildr["ApplyDetailID"]);
                }

                detail.SystemType = head.SystemType;
                detail.PresDetailID = predetailid;
                detail.ApplyHeadID = head.ApplyHeadID;
                detail.ItemID = Convert.ToInt32(itemData.Rows[i]["ExamItemID"].ToString());
                detail.ItemName = itemData.Rows[i]["ExamItemName"].ToString();
                detail.Price = Convert.ToDecimal(itemData.Rows[i]["Price"].ToString());
                if (head.ApplyType == 2)
                {
                    detail.Amount = Convert.ToInt32(itemData.Rows[i]["Amount"].ToString());
                    detail.TotalFee = Convert.ToDecimal(itemData.Rows[i]["Price"].ToString()) * detail.Amount;
                }
                else
                {
                    detail.Amount = 1;
                    detail.TotalFee = Convert.ToDecimal(itemData.Rows[i]["Price"].ToString());
                }

                BindDb(detail);
                detail.save();
            }

            return headid;
        }

        /// <summary>
        /// 删除明细数据
        /// </summary>
        /// <param name="itemData">组合项目数据</param>
        /// <param name="dt">收费项目数据</param>
        public void DeleteData(DataTable itemData, DataTable dt)
        {
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow detaildr = itemData.Select("ExamItemID=" + dt.Rows[i]["ItemID"].ToString()).FirstOrDefault();
                    if (detaildr == null)
                    {
                        EXA_MedicalApplyDetail detail = new EXA_MedicalApplyDetail();
                        OPD_PresDetail presdetail = new OPD_PresDetail();
                        BindDb(detail);
                        detail.ApplyDetailID = Convert.ToInt32(dt.Rows[i]["ApplyDetailID"]);
                        detail.delete();
                        BindDb(presdetail);
                        presdetail.PresDetailID = Convert.ToInt32(dt.Rows[i]["PresDetailID"]);
                        presdetail.delete();
                    }
                }
            }
        }

        /// <summary>
        /// 医嘱费用生成
        /// </summary>
        /// <param name="amount">数量</param>
        /// <param name="doseNum">剂数</param>
        /// <param name="record">医嘱记录</param>
        /// <param name="ipPatlist">病人列表</param>
        /// <param name="row">项目数据行</param>
        /// <returns>费用生成实体</returns>
        private IP_FeeItemGenerate GenerateRecordFee(decimal amount, int doseNum, IPD_OrderRecord record, DataTable ipPatlist, DataRow row)
        {
            IP_FeeItemGenerate feeItem = new IP_FeeItemGenerate();
            feeItem.PatListID = record.PatListID;
            feeItem.PatName = Convert.ToString(ipPatlist.Rows[0]["PatName"]);
            feeItem.PatDeptID = Convert.ToInt32(ipPatlist.Rows[0]["CurrDeptID"]);
            feeItem.PatDoctorID = Convert.ToInt32(ipPatlist.Rows[0]["CurrDoctorID"]);
            feeItem.PatNurseID = Convert.ToInt32(ipPatlist.Rows[0]["CurrNurseID"]);
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
            feeItem.PresDeptID = record.PresDeptID;
            feeItem.PresDoctorID = record.OrderDoc;
            if (Convert.ToInt32(row["ExecDeptId"]) > 0)
            {
                feeItem.ExecDeptDoctorID = Convert.ToInt32(row["ExecDeptId"]);
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

        /// <summary>
        /// 获取项目名称
        /// </summary>
        /// <param name="itemData">组合项目数据</param>
        /// <returns>项目名称</returns>
        public string GetItemName(DataTable itemData)
        {
            string itemNames = string.Empty;
            for (int i = 0; i < itemData.Rows.Count; i++)
            {
                itemNames += itemData.Rows[i]["ExamItemName"].ToString() + ",";
            }

            if (!string.IsNullOrEmpty(itemNames))
            {
                itemNames = itemNames.Substring(0, itemNames.Length - 1);
            }

            return itemNames;
        }
    }
}
