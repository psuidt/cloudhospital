using System;
using System.Collections.Generic;
using System.Linq;
using HIS_Entity.BasicData;
using HIS_Entity.OPManage;
using HIS_PublicManage.ObjectModel;

namespace HIS_OPManage.ObjectModel
{
    /// <summary>
    /// 多张处方一次结算多张发票
    /// </summary>
    public class BalanceProcessOne:BaseBalaceProcess
    {
        /// <summary>
        /// 收费正式结算
        /// </summary>
        /// <param name="curPatlist">病人对象</param>
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

            //实际上一次结算只有一条记录结算表只一条记录
            foreach (ChargeInfo chargeInfo in budgeInfo) 
            {
                int costheadid = chargeInfo.CostHeadID;
                int[] feeHeadids = chargeInfo.FeeItemHeadIDs;
              
                //int invoiceNum= NewObject<CommonMethod>().GetInvoiceInfo(InvoiceType.门诊收费, operatoreid, out curInvoiceNO);
                //if (invoiceNum < feeHeadids.Length)
                //{
                //    throw new Exception("本次收费需要"+feeHeadids.Length+"张票，您的可用票据张数不足");
                //}
                chargeInfo.InvoiceCount = feeHeadids.Length;
                chargeInfo.ChargeDate = chargedate;
                OP_CostHead costHead = NewObject<OP_CostHead>().getmodel(costheadid) as OP_CostHead;
                if (costHead != null && costHead.CostHeadID > 0)
                {
                    string bInvoiceNo = string.Empty;
                    string eInvoiceNo = string.Empty;
                    Basic_Invoice basicInvoice = NewObject<CommonMethod>().GetCurInvoice(InvoiceType.门诊收费, operatoreid);
                    if (basicInvoice.EndNO - basicInvoice.CurrentNO +1< feeHeadids.Length)
                    {
                        throw new Exception("本次收费需要" + feeHeadids.Length + "张票，您当前使用发票卷的可用票据张数不足");
                    }

                    for (int feeIndex = 0; feeIndex < feeHeadids.Length; feeIndex++)
                    {                        
                        //费用主表状态修改
                        OP_FeeItemHead feeItemHead = NewObject<OP_FeeItemHead>().getmodel(feeHeadids[feeIndex]) as OP_FeeItemHead;
                        string invoiceNo = string.Empty;
                        string perfChar = string.Empty;
                        invoiceNo = NewObject<InvoiceManagement>().GetInvoiceCurNOAndUse(InvoiceType.门诊收费, operatoreid, out perfChar);
                        invoiceNo = perfChar + invoiceNo;

                        //一张处方一张票号
                        feeItemHead.InvoiceNO = invoiceNo;
                        feeItemHead.ChargeDate = chargedate;
                        feeItemHead.ChargeFlag = 1;
                        feeItemHead.ChargeStatus = 0;
                        feeItemHead.ChargeEmpID = operatoreid;
                        feeItemHead.save();
                        if (feeIndex == 0)
                        {
                            bInvoiceNo = invoiceNo;
                        }
                        else if (feeIndex == feeHeadids.Length - 1)
                        {
                            eInvoiceNo = invoiceNo;
                        }
                    }

                    //结算主表状态和金额写入
                    costHead.ChargeEmpID = operatoreid;
                    costHead.CostStatus = 0;//正常收费标志改为0
                    costHead.CostDate = chargedate;
                    costHead.CashFee = chargeInfo.CashFee;
                    costHead.PosFee = chargeInfo.PosFee;
                    costHead.PromFee = chargeInfo.FavorableTotalFee;
                    costHead.RoundingFee = chargeInfo.RoundFee;
                    costHead.EndInvoiceNO = bInvoiceNo;
                    costHead.BeInvoiceNO = eInvoiceNo;
                    costHead.AccountID = curAccountId;
                    costHead.InvoiceID = basicInvoice.ID;//写入发票卷序号
                    this.BindDb(costHead);
                    costHead.save();

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
                    AddScore(curPatlist.MemberAccountID, costHead.TotalFee, costHead.CostHeadID.ToString(),operatoreid);
                    AddAccoutFee(costHead, curAccountId, feeHeadids.Length, 0);
                }
                else
                {
                    throw new Exception("没有找到结算号的记录！");
                }
            }         
        }

        /// <summary>
        /// 结算预算
        /// </summary>
        /// <param name="prescriptions">处方对象</param>
        /// <returns>预算对象</returns>
        public override List<ChargeInfo> Budget(List<Prescription> prescriptions)
        {
            List<ChargeInfo> listChargeInfos = new List<ChargeInfo>();
            decimal toTalFee = prescriptions.Sum(p => p.TotalFee);

            //插入结算头表
            OP_CostHead costHead = new OP_CostHead();
            costHead.MemberID = GetPatient.MemberID;
            costHead.CardNO = GetPatient.CardNO;
            costHead.PatListID = GetPatient.PatListID;
            costHead.MemberAccountID = GetPatient.MemberAccountID;
            costHead.PatName = GetPatient.PatName;
            costHead.PatTypeID = CostTypeid;//结算时选择的病人类型ID
            costHead.BeInvoiceNO =string.Empty;
            costHead.EndInvoiceNO =string.Empty;
            costHead.ChargeEmpID = GetOperatorId;
            costHead.TotalFee = toTalFee;
            costHead.CashFee = 0;
            costHead.PosFee = 0;
            costHead.PromFee = 0;
            costHead.RecipeFlag = 0;
            costHead.CostStatus = 9;//预算状态为9
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
                feeItemHead.ChargeStatus = 0;//费用表不修改状态
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
        /// 处方退费
        /// </summary>
        /// <param name="costHeadid">结算ID</param>
        /// <param name="operatoreid">操作员ID</param>
        /// <param name="refundPrescriptions">退处方对象</param>
        /// <param name="refundInvoiceNO">退费票据号</param>
        /// <returns> 被退后的处方对象</returns>
        public override List<Prescription> RefundFee(int costHeadid, int operatoreid, List<Prescription> refundPrescriptions, string refundInvoiceNO)
        {
            //先全退，再算出需退处方再退费
            AllRefund(costHeadid, operatoreid, refundPrescriptions, refundInvoiceNO);
            List<Prescription> balancePresc = new List<Prescription>();

            //返回需要补收的处方记录
            foreach (Prescription refundPresc in refundPrescriptions)
            {
                if (refundPresc.Amount != refundPresc.Refundamount)
                {
                    refundPresc.Amount = refundPresc.Amount - refundPresc.Refundamount;
                    refundPresc.TotalFee = refundPresc.TotalFee - refundPresc.Refundfee;
                    balancePresc.Add(refundPresc);
                }
            }

            #region 暂不用
            //OP_CostHead oldCostHead = NewObject<OP_CostHead>().getmodel(costHeadid) as OP_CostHead;
            //bool autoProcess = false;
            //if (oldCostHead.TotalFee == oldCostHead.CashFee + oldCostHead.PosFee + oldCostHead.RoundingFee)
            //{
            //    autoProcess = true;
            //}
            //if (autoProcess)//全现金处理
            //{
            //    if (_isallRefund)
            //    {
            //        List<OP_FeeItemHead> feeItemHeadList = NewObject<OP_FeeItemHead>().getlist<OP_FeeItemHead>(" costHeadId=" + costHeadid + " and invoiceNO!='" + RefundInvoiceNO + " and ChargeStatus=1");
            //        if (feeItemHeadList.Count == 0)//表示一次结算只有一张票据
            //        {
            //            return balancePresc;
            //        }
            //        decimal allTotalFee = feeItemHeadList.Sum(p => p.TotalFee);
            //        OP_CostHead costHead = new OP_CostHead();
            //        costHead.CostHeadID = 0;
            //        costHead.TotalFee = allTotalFee;
            //        costHead.CashFee = allTotalFee;
            //        costHead.PosFee = 0;
            //        costHead.PromFee = 0;
            //        costHead.ChargeEmpID = operatoreid;
            //        costHead.CostDate = DateTime.Now;
            //        foreach (OP_FeeItemHead feeitemHead in feeItemHeadList)
            //        {

            //        }
            //    }
            //}
            #endregion
            BasicDataManagement basicdata = NewObject<BasicDataManagement>();

            //补收的处方数据保存到数据库
            if (balancePresc.Count > 0)
            {
                OP_FeeItemHead oldFeeitemHead = NewObject<OP_FeeItemHead>().getmodel(balancePresc[0].FeeItemHeadID) as OP_FeeItemHead;
                OP_FeeItemHead feeitemHead = oldFeeitemHead.Clone() as OP_FeeItemHead;
                feeitemHead.FeeItemHeadID = 0;
                feeitemHead.ChargeEmpID = operatoreid;
                feeitemHead.ChargeDate = DateTime.Now;
                feeitemHead.ChargeFlag = 0;
                feeitemHead.ChargeStatus = 0;
                decimal roundingMoney = 0;
                feeitemHead.TotalFee = NewObject<PrescMoneyCalculate>().GetPrescriptionTotalMoney(balancePresc, out roundingMoney);
                this.BindDb(feeitemHead);
                feeitemHead.save();
                for (int j = 0; j < balancePresc.Count; j++)
                {
                    OP_FeeItemDetail oldfeeDetail = NewObject<OP_FeeItemDetail>().getmodel(balancePresc[j].PresDetailID) as OP_FeeItemDetail;
                    OP_FeeItemDetail feeDetial = oldfeeDetail.Clone() as OP_FeeItemDetail;
                    feeDetial.PresDetailID = 0;
                    feeDetial.Amount = balancePresc[j].Amount;
                    feeDetial.TotalFee = balancePresc[j].TotalFee;
                    feeDetial.FeeItemHeadID = feeitemHead.FeeItemHeadID;
                    feeDetial.save();
                    balancePresc[j].FeeItemHeadID = feeitemHead.FeeItemHeadID;
                    balancePresc[j].PresDetailID = feeDetial.PresDetailID;
                    balancePresc[j].FeeNo = feeitemHead.FeeNo;
                    balancePresc[j].ModifyFlag = 0;
                }
            }

            OP_CostHead oldCostHead = NewObject<OP_CostHead>().getmodel(costHeadid) as OP_CostHead;
            List<OP_FeeItemHead> feeItemHeadList = NewObject<OP_FeeItemHead>().getlist<OP_FeeItemHead>(" costHeadId=" + costHeadid + " and invoiceNO!='" + refundInvoiceNO + "' and ChargeStatus=1");
            int i = 1;
            foreach (OP_FeeItemHead oldfeeitemhead in feeItemHeadList)
            {
                OP_FeeItemHead newfeeitemhead = oldfeeitemhead.Clone() as OP_FeeItemHead;
                newfeeitemhead.ChargeFlag = 0;
                newfeeitemhead.FeeItemHeadID = 0;
                newfeeitemhead.ChargeStatus = 0;
                newfeeitemhead.ChargeEmpID = operatoreid;
                newfeeitemhead.CostHeadID = 0;
                newfeeitemhead.save();
                List<OP_FeeItemDetail> feeItemDetaliList = NewObject<OP_FeeItemDetail>().getlist<OP_FeeItemDetail>(" feeitemheadid="+oldfeeitemhead.FeeItemHeadID);
                foreach (OP_FeeItemDetail oldfeeItemDetail in feeItemDetaliList)
                {
                    int j = 0;
                    OP_FeeItemDetail newFeeitemDetail = oldfeeItemDetail.Clone() as OP_FeeItemDetail;
                    newFeeitemDetail.PresDetailID = 0;
                    newFeeitemDetail.FeeItemHeadID = newfeeitemhead.FeeItemHeadID;
                    newfeeitemhead.save();
                    #region 明细
                    Prescription pres = new Prescription();
                    pres.PresDetailID = newFeeitemDetail.PresDetailID;
                    pres.FeeItemHeadID = newFeeitemDetail.FeeItemHeadID;
                    pres.PatListID = newFeeitemDetail.PatListID;
                    pres.ItemID = newFeeitemDetail.ItemID;
                    pres.ItemName = newFeeitemDetail.ItemName;
                    pres.Spec = newFeeitemDetail.Spec;
                    pres.PackUnit = newFeeitemDetail.PackUnit;
                    pres.UnitNO = newFeeitemDetail.UnitNO;
                    pres.StockPrice = newFeeitemDetail.StockPrice;
                    pres.Amount = newFeeitemDetail.Amount;
                    pres.PresAmount = newFeeitemDetail.PresAmount;
                    pres.TotalFee = newFeeitemDetail.TotalFee;
                    pres.ExamItemID = newFeeitemDetail.ExamItemID;
                    pres.DocPresDetailID = newFeeitemDetail.DocPresDetailID;
                    pres.MiniUnit = newFeeitemDetail.MiniUnit;
                    pres.RetailPrice = newFeeitemDetail.RetailPrice;
                    pres.StatID = newFeeitemDetail.StatID;
                    pres.ItemType = newFeeitemDetail.ItemType;

                    pres.PrescGroupID = i + 1;
                    pres.presNO = j == 0 ? i + 1 : 0;
                    pres.PresDeptID = newfeeitemhead.PresDeptID;
                    pres.PresEmpID = newfeeitemhead.PresEmpID;
                    pres.ExecDeptID = newfeeitemhead.ExecDeptID;
                    pres.PresDocName = basicdata.GetEmpName(newfeeitemhead.PresEmpID);
                    pres.ExecDetpName = basicdata.GetDeptName(newfeeitemhead.ExecDeptID);
                    pres.PresType = newfeeitemhead.PresType;
                    pres.ModifyFlag = 0;
                    pres.Selected = 1;
                    if (Convert.ToInt32(pres.ItemType) == (int)OP_Enum.ItemType.收费项目 || Convert.ToInt32(pres.ItemType) == (int)OP_Enum.ItemType.组合项目)
                    {
                        pres.MiniAmount = newFeeitemDetail.Amount;
                        pres.PackAmount = (newFeeitemDetail.Amount - pres.MiniAmount) / pres.UnitNO;
                    }
                    else
                    {
                        pres.MiniAmount = newFeeitemDetail.Amount % newFeeitemDetail.UnitNO;
                        pres.PackAmount = (newFeeitemDetail.Amount - pres.MiniAmount) / pres.UnitNO;
                    }

                    pres.DocPresHeadID = newfeeitemhead.DocPresHeadID;
                    balancePresc.Add(pres);
                    j += 1;
                    #endregion
                }

                i += 1;
            }

            return balancePresc;
        }
    }
}
