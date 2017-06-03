using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using HIS_Entity.BasicData;
using HIS_Entity.OPManage;
using HIS_OPManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_OPManage.ObjectModel
{
    /// <summary>
    /// 多张处方一次结算一张发票
    /// </summary>
    public class BalanceProcess:BaseBalaceProcess
    {    
        /// <summary>
        /// 收费预算
        /// </summary>
        /// <param name="prescriptions">处方对象</param>
        /// <returns>返回预算对象</returns>
        public override List<ChargeInfo> Budget(List<Prescription> prescriptions)
        {
            List<ChargeInfo> listChargeInfos = new List<ChargeInfo>();
            decimal toTalFee = prescriptions.Sum(p=>p.TotalFee);            
            OP_CostHead costHead = new OP_CostHead();   //插入结算头表        
            costHead.MemberID = GetPatient.MemberID;
            costHead.CardNO = GetPatient.CardNO;
            costHead.PatListID = GetPatient.PatListID;
            costHead.MemberAccountID = GetPatient.MemberAccountID;
            costHead.PatName = GetPatient.PatName;
            costHead.PatTypeID = CostTypeid;//结算时选择的病人类型ID
            costHead.BeInvoiceNO = string.Empty;
            costHead.EndInvoiceNO = string.Empty;
            costHead.ChargeEmpID = GetOperatorId;
            costHead.TotalFee = toTalFee;
            costHead.CashFee = 0;
            costHead.PosFee = 0;
            costHead.PromFee = 0;
            costHead.RecipeFlag = 0;            
            costHead.CostStatus =9;//修改预算状态为9
            costHead.OldID = 0;
            costHead.AccountID = 0;
            costHead.RegFlag = 0;
            this.BindDb(costHead);
            costHead.save();
            List<int> feeHeadIDs = new List<int>();
            for (int groupIndex = 0; groupIndex < BudgeGroupID.Count; groupIndex++)
            {
                int groupid = BudgeGroupID[groupIndex];
                List<Prescription> presDetails = prescriptions.Where(p => p.PrescGroupID == groupid).ToList();                
                var result = from p in presDetails.AsEnumerable()
                             group p by p.StatID into g
                             select new
                             {
                                 g.Key,
                                 SumValue = g.Sum(p => p.TotalFee)
                             };
                foreach (var stat in result)
                {
                    OP_CostDetail costDetail = new OP_CostDetail();
                    costDetail.StatID = stat.Key;
                    costDetail.TotalFee = Convert.ToDecimal(stat.SumValue);
                    costDetail.CostHeadID = costHead.CostHeadID;
                    costDetail.FeeItemHeadID = presDetails[0].FeeItemHeadID;
                    costDetail.PresEmpID = presDetails[0].PresEmpID;
                    costDetail.PresDeptID = presDetails[0].PresDeptID;
                    costDetail.ExeDeptID = presDetails[0].ExecDeptID;
                    this.BindDb(costDetail);
                    costDetail.save();
                }

                feeHeadIDs.Add(presDetails[0].FeeItemHeadID);//记录费用头ID
                OP_FeeItemHead feeItemHead = NewObject<OP_FeeItemHead>().getmodel(presDetails[0].FeeItemHeadID) as OP_FeeItemHead;
                if (feeItemHead.ChargeFlag == 1)
                {
                    throw new Exception("处方已被别的收费员收费，请确认！");
                }

                //如果是医生站处方，要判断医生站处方状态，防止医生站已经修改
                feeItemHead.CostHeadID = costHead.CostHeadID;
                feeItemHead.ChargeStatus = 0;//处方状态不修改
                this.BindDb(feeItemHead);
                feeItemHead.save();
            }

            ChargeInfo chargeInfo = new ChargeInfo();
            chargeInfo.TotalFee = toTalFee; //结算总金额
            chargeInfo.CostHeadID = costHead.CostHeadID;//结算ID
            chargeInfo.FeeItemHeadIDs = feeHeadIDs.ToArray();//一次结算对应的费用头表ID
            listChargeInfos.Add(chargeInfo);
            return listChargeInfos;
        }

        /// <summary>
        /// 处方正式结算
        /// </summary>
        /// <param name="curPatlist">当前病人对象</param>
        /// <param name="operatoreid">操作员ID</param>
        /// <param name="budgeInfo">预算对象</param>
        /// <param name="prescriptions">处方对象</param>
        public override void Balance(OP_PatList curPatlist, int operatoreid, List<ChargeInfo> budgeInfo, List<Prescription> prescriptions)
        {
            #region 医生站处方判断和状态修改，医技确费状态修改
            CheckDocPrsc(prescriptions);
            #endregion

            DateTime chargedate = DateTime.Now;
            int iAccountType = 0;

            //得到当前结账ID
            int curAccountId = NewObject<CommonMethod>().GetAccountId(operatoreid, iAccountType);

            //实际上多张处方一次结算只有一条记录
            foreach (ChargeInfo chargeInfo in budgeInfo) 
            {
                string invoiceNo = string.Empty;
                Basic_Invoice basicInvoice = NewObject<CommonMethod>().GetCurInvoice(InvoiceType.门诊收费, operatoreid);
                string perfChar = string.Empty;
                invoiceNo = NewObject<InvoiceManagement>().GetInvoiceCurNOAndUse(InvoiceType.门诊收费, operatoreid, out perfChar);
                invoiceNo = perfChar + invoiceNo; //多张处方一张票号
                int costheadid = chargeInfo.CostHeadID;
                int[] feeHeadids = chargeInfo.FeeItemHeadIDs;
                chargeInfo.InvoiceCount = 1;
                chargeInfo.ChargeDate = chargedate;
                OP_CostHead costHead = NewObject<OP_CostHead>().getmodel(costheadid) as OP_CostHead;
                if (costHead != null && costHead.CostHeadID > 0)
                {
                    //结算主表状态和金额写入
                    costHead.ChargeEmpID = operatoreid;
                    costHead.CostStatus = 0;//正常收费标志改为0
                    costHead.CostDate = chargedate;
                    costHead.CashFee = chargeInfo.CashFee;
                    costHead.PosFee = chargeInfo.PosFee;
                    costHead.PromFee = chargeInfo.FavorableTotalFee;
                    costHead.RoundingFee = chargeInfo.RoundFee;                    
                    costHead.EndInvoiceNO = invoiceNo;
                    costHead.BeInvoiceNO = invoiceNo;
                    costHead.AccountID = curAccountId;
                    costHead.InvoiceID = basicInvoice.ID;//保存使用票据的发票卷序号
                    this.BindDb(costHead);
                    costHead.save();

                    //费用主表状态修改
                    NewDao<IOPManageDao>().UpdateFeeItemHeadStatus(costHead.CostHeadID, invoiceNo, chargedate,operatoreid);
                    
                    //插入结算支付方式表
                    foreach (OP_CostPayMentInfo payment in chargeInfo.PayInfoList)
                    {
                        payment.PatListID = curPatlist.PatListID;
                        payment.PatName = curPatlist.PatName;
                        payment.PatType = costHead.PatTypeID.ToString();
                        payment.AccountID = curAccountId;
                        payment.CostHeadID = costHead.CostHeadID;
                        Basic_Payment basePayment = NewObject<Basic_Payment>().getmodel(payment.PayMentID) as Basic_Payment;
                        payment.PayMentCode = basePayment.PayCode;
                        payment.PayMentName = basePayment.PayName;
                        this.BindDb(payment);
                        payment.save();
                    }

                    //减虚拟库存
                    MinisStorage(prescriptions,false);

                    //会员积分
                    AddScore(curPatlist.MemberAccountID, costHead.TotalFee, costHead.CostHeadID.ToString(),operatoreid);
                    if (chargeInfo.FavorableTotalFee > 0)
                    {
                        SavePromData(costHead.PatTypeID, costHead.MemberAccountID, costHead.TotalFee, prescriptions, operatoreid, costHead.CostHeadID);
                    }

                    AddAccoutFee(costHead, curAccountId, 1, 0);
                }
                else
                {
                    throw new Exception("没有找到结算号的记录！");
                }
            }         
        }

        /// <summary>
        /// 全退
        /// </summary>
        /// <param name="costHeadid">结算ID</param>
        /// <param name="operatoreid">操作员ID</param>
        /// <param name="refundPrescriptions">退费处方</param>
        /// <param name="refundInvoiceNO">退费票据号</param>
        /// <returns>全退后未收的处方对象</returns>
        public override List<Prescription> RefundFee(int costHeadid, int operatoreid, List<Prescription> refundPrescriptions,string refundInvoiceNO)
        {
            AllRefund(costHeadid, operatoreid, refundPrescriptions, refundInvoiceNO);           
            List<Prescription> balancePresc = new List<Prescription>();

            // 医生站的处方退后重新生成的处方
            List<int> updateDocPres = new List<int>();

            //返回需要补收的处方记录
            foreach (Prescription refundPresc in refundPrescriptions)
            {
                if (refundPresc.PresAmount == 0)
                {
                    refundPresc.PresAmount = 1;
                }
                if (refundPresc.Amount*refundPresc.PresAmount != refundPresc.Refundamount)
                {
                    refundPresc.Amount = refundPresc.Amount* refundPresc.PresAmount - refundPresc.Refundamount;
                    refundPresc.TotalFee = refundPresc.TotalFee - refundPresc.Refundfee;

                    //部分退费重新生成的新处方
                    if (refundPresc.Refundamount > 0 && refundPresc.DocPresHeadID>0)
                    {
                        updateDocPres.Add(refundPresc.FeeItemHeadID);
                    }  
                                   
                    balancePresc.Add(refundPresc);
                }
            }  
                    
            if (balancePresc.Count > 0)
            {
                //补收的处方数据保存到数据库
                List<int> presNum = GetPrescNum(balancePresc);
                for (int i = 0; i < presNum.Count; i++)
                {
                    int groupid = presNum[i];
                    List<Prescription> presDetails = balancePresc.Where(p => p.PrescGroupID == groupid && p.SubTotalFlag == 0).ToList();
                    if (presDetails.Count > 0)
                    {
                        OP_FeeItemHead oldFeeitemHead = NewObject<OP_FeeItemHead>().getmodel(presDetails[0].FeeItemHeadID) as OP_FeeItemHead;
                        OP_FeeItemHead feeitemHead = oldFeeitemHead.Clone() as OP_FeeItemHead;
                        feeitemHead.FeeItemHeadID = 0;
                        feeitemHead.ChargeEmpID = operatoreid;
                        feeitemHead.ChargeDate = DateTime.Now;
                        feeitemHead.ChargeFlag = 0;
                        feeitemHead.ChargeStatus = 0;
                        if (updateDocPres.Contains(presDetails[0].FeeItemHeadID))
                        {
                            feeitemHead.DocPresHeadID = 0;                         
                            feeitemHead.DocPresNO = 0;
                        }

                        decimal roundingMoney = 0;
                        feeitemHead.TotalFee = NewObject<PrescMoneyCalculate>().GetPrescriptionTotalMoney(presDetails, out roundingMoney);
                        this.BindDb(feeitemHead);
                        feeitemHead.save();

                        for (int j = 0; j < balancePresc.Count; j++)
                        {
                            if (balancePresc[j].PrescGroupID == groupid && balancePresc[j].SubTotalFlag == 0)
                            {
                                OP_FeeItemDetail oldfeeDetail = NewObject<OP_FeeItemDetail>().getmodel(balancePresc[j].PresDetailID) as OP_FeeItemDetail;
                                OP_FeeItemDetail feeDetial = oldfeeDetail.Clone() as OP_FeeItemDetail;
                                feeDetial.PresDetailID = 0;
                                feeDetial.Amount = balancePresc[j].Amount;
                                feeDetial.TotalFee = balancePresc[j].TotalFee;
                                feeDetial.FeeItemHeadID = feeitemHead.FeeItemHeadID;
                                if (updateDocPres.Contains(presDetails[0].FeeItemHeadID))
                                {
                                    feeDetial.DocPresDetailID = 0;
                                    balancePresc[j].DocPresDetailID = 0;
                                    balancePresc[j].DocPresHeadID = 0;
                                    balancePresc[j].DocPresNO = 0;
                                }

                                feeDetial.save();
                                balancePresc[j].FeeItemHeadID = feeitemHead.FeeItemHeadID;
                                balancePresc[j].PresDetailID = feeDetial.PresDetailID;
                                balancePresc[j].FeeNo = feeitemHead.FeeNo;
                                balancePresc[j].DocPresDate = feeDetial.DocPresDate;
                                balancePresc[j].ModifyFlag = 0;
                            }
                        }
                    }
                }
            }

            return balancePresc;
        }

        /// <summary>
        /// 获取处方序号列表
        /// </summary>
        /// <param name="prescriptions">处方对象</param>
        /// <returns>处方序号列表</returns>
        private List<int> GetPrescNum(List<Prescription> prescriptions)
        {
            List<int> prescNums = new List<int>();          
            for (int i = 0; i < prescriptions.Count; i++)
            {
                if (Convert.IsDBNull(prescriptions[i].PrescGroupID))
                {
                    continue;
                }

                int ambit = Convert.ToInt32(prescriptions[i].PrescGroupID);
                if (prescNums.Where(p => p == ambit).ToList().Count == 0)
                {
                    prescNums.Add(ambit);
                }
            }

            return prescNums;
        }
    }
}
