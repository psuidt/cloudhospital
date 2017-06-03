using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_Entity.OPManage;
using HIS_OPManage.Dao;
using HIS_PublicManage.ObjectModel;
using static HIS_Entity.OPManage.OP_Enum;

namespace HIS_OPManage.ObjectModel
{
    /// <summary>
    /// 处方处理业务对象
    /// </summary>
    public class PrescriptionProcess : AbstractObjectModel
    {
        /// <summary>
        /// 获取病人处方
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <param name="status">状态</param>
        /// <param name="isCharge">是否收费处检索</param>
        /// <returns>处方信息</returns>
        public List<Prescription> GetPrescriptions(int patlistid, OP_Enum.PresStatus status, bool isCharge)
        {
            return GetPrescriptions(patlistid, status, isCharge, string.Empty, string.Empty, string.Empty, 0);
        }

        /// <summary>
        /// 获取病人处方
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <param name="status">状态</param>
        /// <param name="isCharge">是否收费处检索</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="invoiceNo">发票号</param>
        /// <param name="execDeptCode">执行科室ID</param>
        /// <returns>处方信息</returns>
        public List<Prescription> GetPrescriptions(int patlistid, OP_Enum.PresStatus status, bool isCharge, string beginDate, string endDate, string invoiceNo, int execDeptCode)
        {
            BasicDataManagement basicdata = NewObject<BasicDataManagement>();
            List<Prescription> preslist = new List<Prescription>();
            string condiction = string.Empty;
            switch (status)
            {
                case PresStatus.全部:
                    condiction = " PatListID = " + patlistid + " AND ChargeStatus in (0,1)";
                    break;
                case PresStatus.未收费:
                    condiction = " PatListID = " + patlistid + " AND ChargeFlag = 0 AND ChargeStatus = 0";
                    break;
                case PresStatus.已收费未发药:
                case PresStatus.已收费已退药:
                    condiction = " PatListID = " + patlistid + " AND ChargeFlag = 1 AND ChargeStatus = 0 And DistributeFlag = 0";
                    break;
                case PresStatus.已收费已发药:
                    condiction = " PatListID = " + patlistid + " AND ChargeFlag = 1 AND ChargeStatus = 0 And DistributeFlag = 1";
                    break;
            }

            condiction += " and DocPresHeadID=0 ";
            if (execDeptCode != 0)
            {
                condiction = condiction + " and ExecDeptID = " + execDeptCode;
            }

            if (string.IsNullOrEmpty( invoiceNo))
            {
                if (!string.IsNullOrEmpty( beginDate.Trim() ))
                {
                    condiction = condiction + " and PresDate>='" + beginDate + "'";
                }

                if (endDate.Trim() != string.Empty)
                {
                    condiction = condiction + " and PresDate<='" + endDate + "'";
                }
            }

            if (!string.IsNullOrEmpty( invoiceNo))
            {
                condiction = condiction + " and regflag=0 and CostHeadID in (select CostHeadID from OP_CostHead where EndInvoiceNO='" + invoiceNo + "' and CostStatus IN (0,1))";
            }
            int presGroupID = 0;
            List<OP_FeeItemHead> presMastList = NewObject<OP_FeeItemHead>().getlist<OP_FeeItemHead>(condiction);
            if (presMastList.Count > 0)
            {
                DataTable dtPres = NewDao<IOPManageDao>().GetPrescription(condiction);
                List<Prescription> oldPres = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<Prescription>(dtPres);
                for (int i = 0; i < presMastList.Count; i++)
                {
                    List<Prescription> presDetailList = oldPres.Where(p => p.FeeItemHeadID == presMastList[i].FeeItemHeadID).ToList();
                    if (presDetailList.Count == 0)
                    {
                        continue;
                    }
                    presGroupID += 1;
                    for (int j = 0; j < presDetailList.Count; j++)
                    {
                        #region 明细
                        presDetailList[j].PrescGroupID = presGroupID;
                        presDetailList[j].presNO = j == 0 ? presGroupID : 0;
                        presDetailList[j].PresDeptID = presMastList[i].PresDeptID;
                        presDetailList[j].PresEmpID = presMastList[i].PresEmpID;
                        presDetailList[j].ExecDeptID = presMastList[i].ExecDeptID;
                        presDetailList[j].ModifyFlag = 0;
                        presDetailList[j].Selected = 1;
                        if (Convert.ToInt32(presDetailList[j].ItemType) == (int)OP_Enum.ItemType.收费项目 || Convert.ToInt32(presDetailList[j].ItemType) == (int)OP_Enum.ItemType.组合项目)
                        {
                            presDetailList[j].MiniAmount = presDetailList[j].Amount;
                            presDetailList[j].PackAmount = (presDetailList[j].Amount - presDetailList[j].MiniAmount) / presDetailList[j].UnitNO;
                        }
                        else
                        {
                            presDetailList[j].MiniAmount = presDetailList[j].Amount % presDetailList[j].UnitNO;
                            presDetailList[j].PackAmount = (presDetailList[j].Amount - presDetailList[j].MiniAmount) / presDetailList[j].UnitNO;
                        }

                        presDetailList[j].DocPresHeadID = presMastList[i].DocPresHeadID;
                        #endregion
                        preslist.Add(presDetailList[j]);
                    }

                    Prescription presTotal = new Prescription();
                    presTotal.ExecDetpName = "小 计";
                    presTotal.SubTotalFlag = 1;
                    presTotal.PrescGroupID = presGroupID;
                    presTotal.presNO = 0;
                    presTotal.TotalFee = presMastList[i].TotalFee;
                    presTotal.Selected = 1;
                    preslist.Add(presTotal);
                }
            }

            //如果有医生站处方，则读取医生站处方  
            string obj = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.HasOpDSystem);
            if (Convert.ToInt32(obj) == 1)
            {
                presGroupID += 1;
                List<Prescription> docPresList = GetOpdPres(patlistid,presGroupID);
                foreach (Prescription docPres in docPresList)
                {
                    preslist.Add(docPres);
                }
            }

            return preslist;
        }

        /// <summary>
        /// 获取门诊医生站处方
        /// </summary>
        /// <param name="patlistID">病人ID</param>
        /// <returns>返回门诊医生站处方</returns>
        private List<Prescription> GetOpdPres(int patlistID,int prescGroupID)
        {
            List<Prescription> preslist = new List<Prescription>();
            DataTable dtPres = NewDao<IOPManageDao>().GetDocPrescription(patlistID);
            if (dtPres.Rows.Count == 0)
            {
                return preslist;
            }

            List<int> presHeadIDs = GetPrescriptionPresHeadList(dtPres);
            //int prescGroupID = 1;
            foreach (int presHeadID in presHeadIDs)
            {
                List<int> feeHeadCount = GetPrescriptionPresNOList(dtPres, presHeadID);
                decimal sumFee = 0;
                for (int i = 0; i < feeHeadCount.Count; i++)
                {
                    sumFee = 0;
                    int presNO = feeHeadCount[i];
                    DataRow []rows = dtPres.Select(" PresHeadID="+presHeadID+" and PresNO="+presNO);
                    for (int j = 0; j < rows.Length; j++)
                    {
                        //医技 界面转化成明细
                        if (Convert.ToInt32( rows[j]["PresType"]) == 4)
                        {
                            DataTable dtDetailItem = NewDao<IOPManageDao>().GetExamItemDetailDt(Convert.ToInt32(rows[j]["ItemID"]));
                         
                            for (int index = 0; index < dtDetailItem.Rows.Count; index++)
                            {
                                int feeitemId = Convert.ToInt32(dtDetailItem.Rows[index]["ITEMID"]);
                                decimal amount = Convert.ToDecimal(dtDetailItem.Rows[index]["ItemAmount"]);
                                Prescription pres = new Prescription();
                                pres.PatListID = patlistID;
                                pres.PrescGroupID = prescGroupID;
                                pres.presNO = index == 0 ? prescGroupID : 0;
                                pres.Memo = rows[j]["ItemName"].ToString();
                                pres.ExamItemID =Convert.ToInt32( rows[j]["ItemID"]);
                                OpdFeeAdd(pres, rows[j], dtDetailItem.Rows[index]);                            
                                if (Convert.ToInt32(pres.ItemType) == (int)OP_Enum.ItemType.收费项目 || Convert.ToInt32(pres.ItemType) == (int)OP_Enum.ItemType.组合项目)
                                {
                                    pres.MiniAmount = pres.Amount;
                                    pres.PackAmount = (pres.Amount - pres.MiniAmount) / pres.UnitNO;
                                }
                                else
                                {
                                    pres.MiniAmount = pres.Amount % pres.UnitNO;
                                    pres.PackAmount = (pres.Amount - pres.MiniAmount) / pres.UnitNO;
                                }

                                pres.DocPresHeadID = presHeadID;
                                preslist.Add(pres);
                                sumFee += pres.TotalFee;
                            }
                        }
                        else
                        {
                            Prescription pres = new Prescription();
                            pres.PatListID = patlistID;
                            pres.PrescGroupID = prescGroupID;
                            pres.presNO = j == 0 ? prescGroupID : 0;
                            pres.ExamItemID = 0;
                            pres.Memo = string.Empty;
                            OpdFeeAdd(pres, rows[j], null);
                            if (Convert.ToInt32(pres.ItemType) == (int)OP_Enum.ItemType.收费项目 || Convert.ToInt32(pres.ItemType) == (int)OP_Enum.ItemType.组合项目)
                            {
                                pres.MiniAmount = Convert.ToInt32(rows[j]["ChargeAmount"]);
                                pres.PackAmount = (Convert.ToInt32(rows[j]["ChargeAmount"]) - pres.MiniAmount) / Convert.ToInt32(rows[j]["ChargeAmount"]);
                            }
                            else
                            {
                                pres.MiniAmount = Convert.ToInt32(rows[j]["ChargeAmount"]) % pres.UnitNO;
                                pres.PackAmount = (Convert.ToInt32(rows[j]["ChargeAmount"]) - pres.MiniAmount) / pres.UnitNO;
                            }

                            pres.DocPresHeadID = presHeadID;
                            preslist.Add(pres);
                            sumFee += pres.TotalFee;
                        }
                    }

                    if (sumFee > 0)
                    {
                        Prescription presTotal = new Prescription();
                        presTotal.ExecDetpName = "小 计";
                        presTotal.SubTotalFlag = 1;
                        presTotal.PrescGroupID = prescGroupID;
                        presTotal.presNO = 0;
                        presTotal.TotalFee = sumFee;
                        presTotal.Selected = 1;

                        //小计行显示组合项目名称
                        if (preslist.Count > 0)
                        {
                            if (preslist[preslist.Count - 1].ExamItemID > 0)
                            {
                                presTotal.ItemName = preslist[preslist.Count - 1].Memo;
                            }
                        }

                        preslist.Add(presTotal);
                        prescGroupID++;
                    }
                }
            } 
                    
            return preslist;
        }    
      
        /// <summary>
        /// 门诊医生处方赋值收费明细
        /// </summary>
        /// <param name="pres">明细处方对象</param>
        /// <param name="row">门诊医生处方明细</param>
        /// <param name="examRow">医技项目明细</param>
        private void OpdFeeAdd(Prescription pres, DataRow row,DataRow examRow)
        {         
            pres.PresDetailID = 0;
            pres.FeeItemHeadID = 0;
            
            pres.DocPresDetailID = Convert.ToInt32(row["PresDetailID"]);// presDetail.PresDetailID;// presDetails[j].PresDetailID;          
            pres.PresDeptID = Convert.ToInt32(row["PresDeptID"]);// presDetail.PresDeptID;// presDetails[j].PresDeptID;
            pres.PresEmpID = Convert.ToInt32(row["PresDoctorID"]);// presDetail.PresDoctorID;// presDetails[j].PresDoctorID;  
            pres.DocPresNO = Convert.ToInt32(row["PresNO"]);// presDetail.PresNO;
            pres.DocPresDate = Convert.ToDateTime(row["PresDate"]);// presDetail.PresDate;
            pres.ExecDeptID = Convert.ToInt32(row["ExecDeptID"]);
            pres.PresDocName = row["PresDocName"].ToString();
            pres.ExecDetpName =row["ExecDetpName"].ToString();
            pres.IsReimbursement =Convert.ToInt32( row["IsReimbursement"]);//医保是否报销
            pres.ModifyFlag = 1;
            pres.Selected = 1;
            pres.PresAmount = Convert.ToInt32(row["dosenum"]) == 0 ? 1 : Convert.ToInt32(row["dosenum"]);
            pres.StatID = Convert.ToInt32(row["StatID"]);

            pres.Dosage = row["Dosage"].ToString();
            pres.Days = Convert.ToInt32(row["Days"]);
            pres.FrequencyName = row["FrequencyName"].ToString();
            pres.DosageName = row["DosageName"].ToString();
            pres.DosageId = Convert.ToInt32(row["DosageID"]);
            pres.FrequencyID = Convert.ToInt32(row["FrequencyID"]);

            pres.DosageUnit = row["DosageUnit"].ToString();
            pres.Factor= row["Factor"].ToString();
            if (examRow == null)
            {
                pres.ItemID = Convert.ToInt32(row["ItemID"]);
                pres.ItemName = row["ItemName"].ToString();
                pres.Spec = row["Standard"].ToString();
                pres.PackUnit = row["UnPickUnit"].ToString();//  presDetails[j].PackUnit;
                pres.UnitNO = Convert.ToDecimal(row["MiniConvertNum"]);// presDetails[j].PresFactor;
                pres.StockPrice = Convert.ToDecimal(row["InPrice"]);
                pres.Amount = Convert.ToDecimal(row["ChargeAmount"]);
                pres.MiniUnit = row["MiniUnitName"].ToString();
                pres.RetailPrice = Convert.ToDecimal(row["SellPrice"]);
                pres.ItemType = row["ItemClass"].ToString();
                pres.DrugApprovalnumber = row["NationalCode"] == DBNull.Value ? string.Empty : row["NationalCode"].ToString().Trim();
                pres.MedicareItemName = row["MedicareItemName"].ToString();
            }
            else
            {
                pres.ItemID = Convert.ToInt32(examRow["ItemID"]);
                pres.ItemName = examRow["ItemName"].ToString();
                pres.Spec = examRow["Standard"].ToString();
                pres.PackUnit = examRow["UnPickUnit"].ToString();//  presDetails[j].PackUnit;
                pres.UnitNO = Convert.ToDecimal(examRow["MiniConvertNum"]);// presDetails[j].PresFactor;
                pres.StockPrice = Convert.ToDecimal(examRow["InPrice"]);
                pres.Amount = Convert.ToDecimal(row["ChargeAmount"])* Convert.ToDecimal(examRow["ItemAmount"]);
                pres.MiniUnit = examRow["MiniUnitName"].ToString();
                pres.RetailPrice = Convert.ToDecimal(examRow["SellPrice"]);
                pres.ItemType = examRow["ItemClass"].ToString();
                pres.DrugApprovalnumber = examRow["NationalCode"] == DBNull.Value ? string.Empty : examRow["NationalCode"].ToString().Trim();
                pres.MedicareItemName = examRow["MedicareItemName"].ToString();

            }

            //计算行合计金额
            decimal rowTotal = Convert.ToDecimal(((pres.RetailPrice / pres.UnitNO) * pres.Amount * pres.PresAmount).ToString("0.00"));
            pres.TotalFee = rowTotal;
        }

        /// <summary>
        /// 获取处方中不同的处方号列表
        /// </summary>
        /// <param name="presDetailList">处方明细</param>
        /// <returns>不同的处方号列表</returns>
        private List<int> GetPrescriptionPresNOList(List<OPD_PresDetail> presDetailList)
        {
            List<int> prescNums = new List<int>();
            for (int i = 0; i < presDetailList.Count; i++)
            {
                if (Convert.IsDBNull(presDetailList[i].PresNO))
                {
                    continue;
                }

                int ambit = Convert.ToInt32(presDetailList[i].PresNO);
                if (prescNums.Where(p => p == ambit).ToList().Count == 0)
                {
                    prescNums.Add(ambit);
                }
            }

            return prescNums;
        }

        /// <summary>
        /// 获取处方中不同的处方头ID
        /// </summary>
        /// <param name="presDetail">处方明细</param>
        /// <returns>不同的处方头ID</returns>
        private List<int> GetPrescriptionPresHeadList(DataTable presDetail)
        {
            List<int> prescHeadIDs = new List<int>();
            for (int i = 0; i < presDetail.Rows.Count; i++)
            {              
                int ambit = Convert.ToInt32(presDetail.Rows[i]["PresHeadID"]);
                if (prescHeadIDs.Where(p => p == ambit).ToList().Count == 0)
                {
                    prescHeadIDs.Add(ambit);
                }
            }

            return prescHeadIDs;
        }

        /// <summary>
        /// 获取处方头ID
        /// </summary>
        /// <param name="presDetail">处方明细</param>
        /// <param name="presHeadID">处方头ID</param>
        /// <returns>处方头ID列表</returns>
        private List<int> GetPrescriptionPresNOList(DataTable presDetail,int presHeadID)
        {
            List<int> prescNums = new List<int>();
            for (int i = 0; i < presDetail.Rows.Count; i++)
            {
                int ambit = Convert.ToInt32(presDetail.Rows[i]["PresNO"]);
                int presheadID = Convert.ToInt32(presDetail.Rows[i]["PresHeadID"]);
                if (presheadID == presHeadID)
                {
                    if (prescNums.Where(p => p == ambit).ToList().Count == 0)
                    {
                        prescNums.Add(ambit);
                    }
                }
            }

            return prescNums;
        }

        /// <summary>
        /// 根据发票号获取处方
        /// </summary>
        /// <param name="invoiceNo">票据号</param>
        /// <param name="patlistid">病人ID</param>
        /// <returns>处方对象</returns>
        public List<Prescription> GetPrescriptionByInvoiceNo(string invoiceNo, int patlistid)
        {
            List<Prescription> preslist = new List<Prescription>();
            BasicDataManagement basicdata = NewObject<BasicDataManagement>();
            string condiction = string.Empty;
            condiction = " PatListID = " + patlistid + " AND ChargeFlag = 1 AND ChargeStatus = 0 And invoiceNO='" + invoiceNo + "' and regflag=0";
            condiction = condiction + " order by feeitemheadid";

            //得到实体列表
            List<OP_FeeItemHead> presMastList = NewObject<OP_FeeItemHead>().getlist<OP_FeeItemHead>(condiction);
            if (presMastList.Count == 0)
            {
                throw new Exception("找不到发票信息！\r\n1、请确认发票号是否正确。\r\n2、请确认该发票是否已退费。");
            }

            for (int i = 0; i < presMastList.Count; i++)
            {
                decimal refundfee = 0;
                List<OP_FeeItemDetail> presDetailList = NewObject<OP_FeeItemDetail>().getlist<OP_FeeItemDetail>(" FeeItemHeadID=" + presMastList[i].FeeItemHeadID + " order by PresDetailID");
                for (int j = 0; j < presDetailList.Count; j++)
                {
                    #region 明细
                    Prescription pres = new Prescription();
                    pres.PresDetailID = presDetailList[j].PresDetailID;
                    pres.FeeItemHeadID = presDetailList[j].FeeItemHeadID;
                    pres.PatListID = presDetailList[j].PatListID;
                    pres.ItemID = presDetailList[j].ItemID;
                    pres.ItemName = presDetailList[j].ItemName;
                    pres.Spec = presDetailList[j].Spec;
                    pres.PackUnit = presDetailList[j].PackUnit;
                    pres.UnitNO = presDetailList[j].UnitNO;
                    pres.StockPrice = presDetailList[j].StockPrice;
                    pres.Amount = presDetailList[j].Amount;
                    pres.PresAmount = presDetailList[j].PresAmount;
                    pres.TotalFee = presDetailList[j].TotalFee;
                    pres.ExamItemID = presDetailList[j].ExamItemID;
                    pres.DocPresDetailID = presDetailList[j].DocPresDetailID;
                    pres.MiniUnit = presDetailList[j].MiniUnit;
                    pres.RetailPrice = presDetailList[j].RetailPrice;
                    pres.StatID = presDetailList[j].StatID;
                    pres.ItemType = presDetailList[j].ItemType;

                    pres.PrescGroupID = i + 1;
                    pres.presNO = j == 0 ? i + 1 : 0;
                    pres.PresDeptID = presMastList[i].PresDeptID;
                    pres.PresEmpID = presMastList[i].PresEmpID;
                    pres.ExecDeptID = presMastList[i].ExecDeptID;
                    pres.PresDocName = basicdata.GetEmpName(presMastList[i].PresEmpID);
                    pres.ExecDetpName = basicdata.GetDeptName(presMastList[i].ExecDeptID);
                    pres.PresType = presMastList[i].PresType;
                    pres.ModifyFlag = 0;
                    pres.Selected = 1;
                    pres.CostHeadID = presMastList[i].CostHeadID;

                    pres.DocPresNO = presMastList[i].DocPresNO;
                    pres.DocPresHeadID = presMastList[i].DocPresHeadID;
                    pres.DocPresDetailID = presDetailList[j].DocPresDetailID;
                    List<OP_FeeRefundHead> refundHeadList = NewObject<OP_FeeRefundHead>().getlist<OP_FeeRefundHead>(" invoiceNum='" + invoiceNo + "' and flag=0");
                    List<OP_FeeRefundDetail> refundDetailList = NewObject<OP_FeeRefundDetail>().getlist<OP_FeeRefundDetail>(" ReHeadID=" + refundHeadList[0].ReHeadID + " and FeeItemDetailID=" + presDetailList[j].PresDetailID + " ");
                    if (refundDetailList.Count == 0)
                    {
                        continue;
                    }

                    OP_FeeRefundDetail refundDetail = refundDetailList[0] as OP_FeeRefundDetail;
                    pres.Refundamount = refundDetail.RefundAmount;
                    pres.Refundfee = refundDetail.RefundFee;
                    if (Convert.ToInt32(pres.ItemType) == (int)OP_Enum.ItemType.收费项目 || Convert.ToInt32(pres.ItemType) == (int)OP_Enum.ItemType.组合项目)
                    {
                        pres.MiniAmount = presDetailList[j].Amount;
                        pres.PackAmount = (presDetailList[j].Amount - pres.MiniAmount) / pres.UnitNO;

                        pres.RefundMiniAmount = refundDetail.RefundAmount;
                        pres.RefundPackAmount = (refundDetail.RefundAmount - pres.RefundMiniAmount) / pres.UnitNO;
                    }
                    else
                    {
                        pres.MiniAmount = presDetailList[j].Amount % presDetailList[j].UnitNO;
                        pres.PackAmount = (presDetailList[j].Amount - pres.MiniAmount) / pres.UnitNO;

                        pres.RefundMiniAmount = refundDetail.RefundAmount % pres.UnitNO;
                        pres.RefundPackAmount = (refundDetail.RefundAmount - pres.RefundMiniAmount) / pres.UnitNO;
                    }

                    pres.DocPresHeadID = presMastList[i].DocPresHeadID;
                    pres.Refundfee = refundDetail.RefundFee;
                    refundfee += pres.Refundfee;
                    #endregion
                    preslist.Add(pres);
                }

                Prescription presTotal = new Prescription();
                presTotal.ExecDetpName = "小 计";
                presTotal.SubTotalFlag = 1;
                presTotal.PrescGroupID = i + 1;
                presTotal.presNO = 0;
                presTotal.TotalFee = presMastList[i].TotalFee;
                presTotal.Refundfee = refundfee;
                presTotal.Selected = 1;
                preslist.Add(presTotal);
            }

            return preslist;
        }

        /// <summary>
        /// 删除整张处方
        /// </summary>
        /// <param name="feeItemHeadID">要删除的处方ID</param>
        public void DeletePrescription(int feeItemHeadID)
        {
            OP_FeeItemHead op_feeitemhead = NewObject<OP_FeeItemHead>().getmodel(feeItemHeadID) as OP_FeeItemHead;
            if (op_feeitemhead.ChargeFlag == 1)
            {
                throw new Exception("该处方已经收费，不能删除！");
            }

            try
            {
                List<OP_FeeItemDetail> detail = NewObject<OP_FeeItemDetail>().getlist<OP_FeeItemDetail>(" FeeItemHeadID=" + op_feeitemhead.FeeItemHeadID);
                for (int i = 0; i < detail.Count; i++)
                {
                    detail[i].delete();
                }

                op_feeitemhead.delete();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 删除处方明细
        /// </summary>
        /// <param name="prescriptionDetailId">要删除的明细ID</param>       
        public void DeletePrescriptionDetail(int prescriptionDetailId)
        {
            OP_FeeItemDetail feeitemdetail = NewObject<OP_FeeItemDetail>().getmodel(prescriptionDetailId) as OP_FeeItemDetail;
            OP_FeeItemHead feeitemhead = NewObject<OP_FeeItemHead>().getmodel(feeitemdetail.FeeItemHeadID) as OP_FeeItemHead;
            if (feeitemhead.ChargeFlag == 1)
            {
                throw new Exception("该处方已经收费，不能删除！");
            }

            try
            {
                feeitemdetail.delete();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        #region 处方保存
        /// <summary>
        /// 处方保存
        /// </summary>
        /// <param name="curPalist">病人对象</param>
        /// <param name="prescriptions">处方对象</param>
        /// <param name="feeHeadCount">费用头ID</param>
        /// <param name="operatorid">操作员ID</param>
        public void SavePrescription(OP_PatList curPalist, List<Prescription> prescriptions, List<int> feeHeadCount, int operatorid)
        {
            try
            {
                DrugStoreManagement drugManager = NewObject<DrugStoreManagement>();
                DateTime presdate = DateTime.Now;
                for (int i = 0; i < feeHeadCount.Count; i++)
                {
                    int groupid = feeHeadCount[i];
                    List<Prescription> presModifyDetails = prescriptions.Where(p => p.PrescGroupID == groupid && p.SubTotalFlag == 0 && p.ModifyFlag == 1).ToList();
                    if (presModifyDetails.Count == 0)
                    {
                        continue;
                    }

                    List<Prescription> presDetails = prescriptions.Where(p => p.PrescGroupID == groupid && p.SubTotalFlag == 0).ToList();
                    if (presDetails.Count > 0)
                    {
                        OP_FeeItemHead feeitemHead = new OP_FeeItemHead();

                        //OP_FeeItemHead赋值
                        SetFeeHeadValue(feeitemHead, presDetails[0], curPalist);
                        feeitemHead.ChargeEmpID = operatorid;
                        decimal roundingMoney = 0;
                        feeitemHead.TotalFee = NewObject<PrescMoneyCalculate>().GetPrescriptionTotalMoney(presDetails, out roundingMoney);
                        feeitemHead.RoungingFee = roundingMoney;
                        if (feeitemHead.DocPresHeadID > 0)
                        {
                            feeitemHead.PresDate = presDetails[0].DocPresDate;
                        }
                        else
                        {
                            feeitemHead.PresDate = presdate;
                        }

                        feeitemHead.RegFlag = 0;
                        if (feeitemHead.FeeItemHeadID == 0)
                        {
                            feeitemHead.FeeNo = DateTime.Now.Ticks;// Convert.ToDecimal( DateTime.Now.ToString("yyyyMMddHHmmssffff"));// GetTimeStamp();//生成费用流水号
                        }

                        if (Convert.ToInt32(presDetails[0].ItemType) == (int)OP_Enum.ItemType.药品)
                        {
                            if (presDetails[0].StatID == 100)
                            {
                                feeitemHead.PresType = "1";//西药处方
                            }
                            else if (presDetails[0].StatID == 101)
                            {
                                feeitemHead.PresType = "2";//中成药处方
                            }
                            else if (presDetails[0].StatID == 102)
                            {
                                feeitemHead.PresType = "3";//中草药处方
                            }
                        }
                        else
                        {
                            feeitemHead.PresType = "0";//非药品处方
                        }

                        this.BindDb(feeitemHead);
                        feeitemHead.save();

                        for (int j = 0; j < prescriptions.Count; j++)
                        {
                            if (prescriptions[j].PrescGroupID == groupid && prescriptions[j].SubTotalFlag == 0)
                            {
                                OP_FeeItemDetail feeDetail = new OP_FeeItemDetail();

                                //OP_FeeItemDetail赋值
                                SetFeeDetailValue(feeDetail, prescriptions[j], curPalist);
                                if (feeDetail.Amount == 0)
                                {
                                    throw new Exception("【" + feeDetail.ItemName + "】数量为零，请输入一个大于零的数");
                                }

                                if (Convert.ToInt32(feeDetail.ItemType) == (int)ItemType.药品 && prescriptions[j].Selected==1)
                                {
                                    //判断实时库存
                                    decimal storAmount = drugManager.GetStorage(feeDetail.ItemID, feeitemHead.ExecDeptID);
                                    if (storAmount < feeDetail.Amount)
                                    {
                                        throw new Exception("【" + feeDetail.ItemName + "】库存不足，请重新输入");
                                    }
                                }

                                feeDetail.FeeItemHeadID = feeitemHead.FeeItemHeadID;
                                this.BindDb(feeDetail);
                                feeDetail.save();
                                prescriptions[j].FeeItemHeadID = feeitemHead.FeeItemHeadID;
                                prescriptions[j].PresDetailID = feeDetail.PresDetailID;
                                prescriptions[j].FeeNo = feeitemHead.FeeNo;
                                prescriptions[j].ModifyFlag = 0;
                            }
                        }

                        this.BindDb(curPalist);
                        curPalist.save();
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 费用头表赋值
        /// </summary>
        /// <param name="feeitemHead">费用头表ID</param>
        /// <param name="presc">处方信息</param>
        /// <param name="curPatlist">当前病人对象</param>
        private void SetFeeHeadValue(OP_FeeItemHead feeitemHead, Prescription presc, OP_PatList curPatlist)
        {
            feeitemHead.FeeItemHeadID = presc.FeeItemHeadID;
            feeitemHead.MemberID = curPatlist.MemberID;
            feeitemHead.PatListID = curPatlist.PatListID;
            feeitemHead.PatName = curPatlist.PatName;
            feeitemHead.PresEmpID = presc.PresEmpID;
            feeitemHead.PresDeptID = presc.PresDeptID;
            feeitemHead.ExecDeptID = presc.ExecDeptID;
            feeitemHead.PresAmount = presc.PresAmount;
            feeitemHead.OldID = 0;
            feeitemHead.ChargeStatus = 0;
            feeitemHead.ChargeFlag = 0;
            feeitemHead.DistributeFlag = 0;
            feeitemHead.DocPresHeadID = presc.DocPresHeadID;
            feeitemHead.FeeNo = presc.FeeNo;
            feeitemHead.DocPresNO = presc.DocPresNO;
        }

        /// <summary>
        /// 费用明细表赋值
        /// </summary>
        /// <param name="feeitemDetail">费用明细对象</param>
        /// <param name="presc">处方对象</param>
        /// <param name="curPatlist">病人对象</param>
        private void SetFeeDetailValue(OP_FeeItemDetail feeitemDetail, Prescription presc, OP_PatList curPatlist)
        {
            feeitemDetail.PresDetailID = presc.PresDetailID;
            feeitemDetail.DocPresDetailID = presc.DocPresDetailID;
            feeitemDetail.MemberID = curPatlist.MemberID;
            feeitemDetail.PatListID = curPatlist.PatListID;
            feeitemDetail.ItemID = presc.ItemID;
            feeitemDetail.ItemType = presc.ItemType;
            feeitemDetail.StatID = presc.StatID;
            feeitemDetail.ItemName = presc.ItemName;
            feeitemDetail.Spec = presc.Spec;
            feeitemDetail.PackUnit = presc.PackUnit;
            feeitemDetail.UnitNO = presc.UnitNO;
            feeitemDetail.StockPrice = presc.StockPrice;
            feeitemDetail.RetailPrice = presc.RetailPrice;
            feeitemDetail.Amount = (presc.PackAmount * presc.UnitNO) + presc.MiniAmount;
            feeitemDetail.PresAmount = presc.PresAmount;
            feeitemDetail.TotalFee = presc.TotalFee;
            feeitemDetail.ExamItemID = presc.ExamItemID;
            feeitemDetail.DocPresDetailID = presc.DocPresDetailID;
            feeitemDetail.MiniUnit = presc.MiniUnit;
            feeitemDetail.Memo = presc.Memo;            
            feeitemDetail.DocPresDate = presc.DocPresDate;
        }
        #endregion
    }
}
