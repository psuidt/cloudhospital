using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_Entity.OPManage;
using HIS_OPManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_OPManage.ObjectModel
{
    /// <summary>
    /// 收费基类
    /// </summary>
    public abstract class BaseBalaceProcess : AbstractObjectModel
    {
        #region 参数
        /// <summary>
        /// 病人对象
        /// </summary>
        private OP_PatList curPatient;

        /// <summary>
        /// 操作员ID
        /// </summary>
        private int operatorId;

        /// <summary>
        /// 预算处方ID        
        /// </summary>
        private List<int> budgeGroupID;

        /// <summary>
        /// 结算病人类型
        /// </summary>
        private int costtypeid;

       /// <summary>
       /// 收费基类
       /// </summary>
       /// <param name="patient">病人对象</param>
       /// <param name="operatorId">操作员ID</param>
       /// <param name="chargeType">结算类型</param>
       /// <param name="budgeGroupID">预算处方的groupid</param>
       /// <param name="costTypeid">结算病人类型</param>
        public void BaseBalaceProcessInit(OP_PatList patient, int operatorId, OP_Enum.BalanceType chargeType, List<int> budgeGroupID, int costTypeid)
        {
            curPatient = patient;
            this.operatorId = operatorId;
            this.chargeType = chargeType;
            this.budgeGroupID = budgeGroupID;
            costtypeid = costTypeid;
        }

        /// <summary>
        /// 结算病人类型
        /// </summary>
        public int CostTypeid
        {
            get { return costtypeid; }
        }

        /// <summary>
        /// 预算ID
        /// </summary>
        public List<int> BudgeGroupID
        {
            get
            {
                return budgeGroupID;
            }
        }

        /// <summary>
        /// 操作的病人对象
        /// </summary>
        public OP_PatList GetPatient
        {
            get
            {
                return curPatient;
            }
        }

        /// <summary>
        /// 当前操作员
        /// </summary>
        public int GetOperatorId
        {
            get
            {
                return operatorId;
            }
        }

        /// <summary>
        /// 结算方式
        /// </summary>
        protected OP_Enum.BalanceType chargeType;

        /// <summary>
        /// 系统结算方式
        /// </summary>
        public OP_Enum.BalanceType GetChargeType
        {
            get
            {
                return chargeType;
            }
        }
        #endregion

        /// <summary>
        /// 收费预算
        /// </summary>
        /// <param name="prescriptions">处方对象</param>
        /// <returns>返回预算对象</returns>
        public abstract List<ChargeInfo> Budget(List<Prescription> prescriptions);

        /// <summary>
        /// 收费结算
        /// </summary>
        /// <param name="patlist">当前病人对象</param>
        /// <param name="operatoreid">操作员ID</param>
        /// <param name="budgeInfo">预算对象</param>
        /// <param name="prescriptions">收费处方</param>
        public abstract void Balance(OP_PatList patlist, int operatoreid, List<ChargeInfo> budgeInfo, List<Prescription> prescriptions);

        /// <summary>
        /// 处方退费
        /// </summary>
        /// <param name="costHeadid">结算ID</param>
        /// <param name="operatoreid">操作员ID</param>
        /// <param name="refundPrescriptions">退费处方</param>
        /// <param name="refundInvoiceNO">退费票据号</param>
        /// <returns>返回处方对象</returns>
        public abstract List<Prescription> RefundFee(int costHeadid,int operatoreid, List<Prescription> refundPrescriptions, string refundInvoiceNO);

        /// <summary>
        /// 取消预算
        /// </summary>
        /// <param name="budgeInfo">预算对象</param>
        public  void AbortBudget(List<ChargeInfo> budgeInfo)
        {
            foreach (ChargeInfo chargeInfo in budgeInfo)
            {
                List<OP_FeeItemHead> listFeeHeads = NewObject<OP_FeeItemHead>().getlist<OP_FeeItemHead>(" costheadid=" + chargeInfo.CostHeadID);
                foreach (OP_FeeItemHead feehead in listFeeHeads)
                {
                    feehead.ChargeStatus = 0;
                    feehead.save();
                }
            }
        }

        /// <summary>
        /// 药品收费后减虚拟库库存
        /// </summary>
        /// <param name="prescriptions">处方信息</param>
        /// <param name="isAdd">true加库存false减库存</param>
        public void MinisStorage(List<Prescription> prescriptions,bool isAdd)
        {
            foreach (Prescription presc in prescriptions)
            {
                if (Convert.ToInt32(presc.ItemType) ==(int) OP_Enum.ItemType.药品)
                {
                    DrugStoreManagement drugManager = NewObject<DrugStoreManagement>();

                    //退费减虚拟库存
                    if (isAdd)
                    {
                        drugManager.UpdateStorage(presc.ItemID, presc.ExecDeptID, presc.Amount);
                    }
                    else
                    {
                        drugManager.UpdateStorage(presc.ItemID, presc.ExecDeptID, presc.Amount * (-1));
                    }                    
                }
            }
        }

        /// <summary>
        /// 会员积分修改
        /// </summary>
        /// <param name="memberAccountID">会员账户ID</param>
        /// <param name="amount">金额</param>
        /// <param name="costHeadID">结算ID</param>
        /// <param name="operatorid">操作员ID</param>
        public void AddScore(int memberAccountID, decimal amount,string costHeadID,int operatorid)
        {
            MemberManagement membermanager = NewObject<MemberManagement>();
            membermanager.SaveAddScoreList(memberAccountID, amount, 2, costHeadID,operatorid);
        }

        /// <summary>
        /// 保存优惠明细
        /// </summary>
        /// <param name="costPatTypeid">病人类型ID</param>
        /// <param name="memberAccountID">会员账户ID</param>
        /// <param name="totalFee">总金额</param>
        /// <param name="prescriptions">处方对象</param>
        /// <param name="operatoreID">操作员ID</param>
        /// <param name="costHeadid">结算ID</param>
        public void SavePromData(int costPatTypeid, int memberAccountID, decimal totalFee, List<Prescription> prescriptions, int operatoreID, int costHeadid)
        {
            //#region  构造明细表
            //DataTable dtDetail = new DataTable();
            //DataColumn col = new DataColumn();
            //col.ColumnName = "ItemTypeID";
            //col.DataType = typeof(System.Decimal);
            //dtDetail.Columns.Add(col);
            //col = new DataColumn();
            //col.ColumnName = "PresDetailId";
            //col.DataType = typeof(System.Int32);
            //dtDetail.Columns.Add(col);
            //col = new DataColumn();
            //col.ColumnName = "ItemAmount";
            //col.DataType = typeof(System.Decimal);
            //dtDetail.Columns.Add(col);
            //col = new DataColumn();
            //col.ColumnName = "PromAmount";
            //col.DataType = typeof(System.Decimal);
            //dtDetail.Columns.Add(col);
            //foreach (Prescription presc in prescriptions)
            //{
            //    DataRow dr = dtDetail.NewRow();
            //    dr["ItemTypeID"] = presc.ItemID;
            //    dr["PresDetailId"] = presc.PresDetailID;
            //    dr["ItemAmount"] = presc.TotalFee;
            //    dr["PromAmount"] = 0;
            //    dtDetail.Rows.Add(dr);
            //}
            //#endregion

            //#region 构造分类表
            //var result = from p in prescriptions.AsEnumerable()
            //             group p by p.StatID into g
            //             select new
            //             {
            //                 g.Key,
            //                 SumValue = g.Sum(p => p.TotalFee)
            //             };
            //DataTable dtPromClass = new DataTable();
            //col = new DataColumn();
            //col.ColumnName = "ClassTypeID";
            //col.DataType = typeof(System.String);
            //dtPromClass.Columns.Add(col);
            //col = new DataColumn();
            //col.ColumnName = "ClassAmount";
            //col.DataType = typeof(System.Decimal);
            //dtPromClass.Columns.Add(col);
            //col = new DataColumn();
            //col.ColumnName = "PromAmount";
            //col.DataType = typeof(System.Decimal);
            //dtPromClass.Columns.Add(col);
            //foreach (var stat in result)
            //{
            //    DataRow dr = dtPromClass.NewRow();
            //    dr["ClassTypeID"] = stat.Key;
            //    dr["ClassAmount"] = stat.SumValue;
            //    dr["PromAmount"] = 0;
            //    dtPromClass.Rows.Add(dr);
            //}
            //#endregion
            //DataTable outdtPromClass = new DataTable();
            //DataTable outDetail = new DataTable();
            //PromotionManagement promManager = NewObject<PromotionManagement>();
            //DiscountInfo discountinfo = new DiscountInfo();
            //discountinfo.AccountID = memberAccountID;
            //discountinfo.CostType = costPatTypeid;
            //discountinfo.PatientType = 1;
            //discountinfo.Amount = totalFee;
            //discountinfo.OperateID = operatoreID;
            //discountinfo.SettlementNO = costHeadid.ToString();
            //discountinfo.DtDetail = dtDetail;
            //discountinfo.DtClass = dtPromClass;
            //discountinfo.IsSave = true;
            //promManager.CalculationPromotion(discountinfo);
            PromotionManagement promManager = NewObject<PromotionManagement>();
            promManager.UpdateDiscountInfo(costHeadid,costHeadid);
        }
       
        /// <summary>
        /// 每笔记录汇总插入结账表
        /// </summary>
        /// <param name="costHead">结算对象</param>
        /// <param name="curAccountId">缴款ID</param>
        /// <param name="invoiceCount">收费票据张数</param>
        /// <param name="refundInvoiceCount">退票张数</param>
        public void AddAccoutFee(OP_CostHead costHead,int curAccountId, int invoiceCount,int refundInvoiceCount)
        {
            NewObject<CommonMethod>().AddAccoutFee(costHead, curAccountId, invoiceCount, refundInvoiceCount);
        }

        /// <summary>
        /// 全退处方
        /// </summary>
        /// <param name="costHeadid">结算ID</param>
        /// <param name="operatoreid">操作员ID</param>
        /// <param name="refundPrescriptions">退费处方</param>
        /// <param name="refundInvoiceNO">退费票据号</param>
        public void AllRefund(int costHeadid, int operatoreid, List<Prescription> refundPrescriptions, string refundInvoiceNO)
        {
            /*
            1:获取当前退费操作员结账ID
            2：修改原结算记录OP_CostHead CostStatus=1
            3：新增红冲结算记录 CostStatus = 2，写入对冲OldID，金额为负
            4:新增红冲结算明细记录 OP_CostDetail
            5：新增红冲结算支付方式记录 OP_CostPayMentInfo
            6：修改原费用主表 OP_FeeItemHead ChargeStatus=1
            7：新增红冲费用主表记录ChargeStatus=2，写入对冲OldID，金额为负
            8：新增红冲费用明细表记录 OP_FeeItemDetail
            9：修改退费消息表OP_FeeRefundHead RefundPayFlag = 1 
            10：减会员积分
            11：加虚拟库存
            12:修改op_account汇总金额
           */
            int iAccountType = 0;
            DateTime chargedate = DateTime.Now;

            //获取当前操作员得到当前结账ID
            int curAccountId = NewObject<CommonMethod>().GetAccountId(operatoreid, iAccountType);
            OP_CostHead oldCostHead = NewObject<OP_CostHead>().getmodel(costHeadid) as OP_CostHead;
            if (oldCostHead.CostStatus != 0)
            {
                throw new Exception("该处方已经被退费");
            }

            List< OP_FeeRefundHead> feerefundheadlist = NewObject<OP_FeeRefundHead>().getlist<OP_FeeRefundHead>("invoicenum='"+ refundInvoiceNO+"' and flag=0");
            if (feerefundheadlist == null || feerefundheadlist.Count == 0)
            {
                throw new Exception("退费消息已经删除");
            }

            //再次判断退费消息，是不是存在已经删除又修改状态
            List<OP_FeeRefundDetail> feerefundDetailList = NewObject<OP_FeeRefundDetail>().getlist<OP_FeeRefundDetail>(" reheadid=" + feerefundheadlist[0].ReHeadID);

            //返回需要补收的处方记录
            foreach (Prescription refundPresc in refundPrescriptions)
            {
                foreach (OP_FeeRefundDetail refundDetail in feerefundDetailList)
                {
                    if (refundPresc.FeeItemHeadID == refundDetail.FeeItemHeadID && refundPresc.PresDetailID == refundDetail.FeeItemDetailID)
                    {
                        if (refundPresc.Refundamount != refundDetail.RefundAmount)
                        {
                            throw new Exception("退费消息已经修改，请重新获取退费消息");
                        }
                    }
                }
            }

            int refundPosType = Convert.ToInt32(NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.RefundPosType));
            //原结算记录状态修改
            oldCostHead.CostStatus = 1; //状态变为退费状态
            this.BindDb(oldCostHead);
            oldCostHead.save();

            //新结算主表
            OP_CostHead newCostHead = oldCostHead.Clone() as OP_CostHead;
            newCostHead.CostHeadID = 0;
            newCostHead.CostStatus = 2;//状态为空冲
            newCostHead.AccountID = curAccountId;//定入新的结算ID
            if (refundPosType == 0)//pos退现金
            {
                newCostHead.CashFee = (oldCostHead.CashFee + oldCostHead.PosFee) * -1;
                newCostHead.PosFee = 0;
            }
            else
            {
                newCostHead.CashFee = oldCostHead.CashFee * -1;
                newCostHead.PosFee = oldCostHead.PosFee * -1;
            }

            newCostHead.TotalFee = newCostHead.TotalFee * -1;           
            newCostHead.PromFee = newCostHead.PromFee * -1;
            newCostHead.OldID = oldCostHead.CostHeadID;//写入原costHeadID
            newCostHead.ChargeEmpID = operatoreid;
            newCostHead.CostDate = chargedate;
            newCostHead.RoundingFee = oldCostHead.RoundingFee * -1;
            this.BindDb(newCostHead);
            newCostHead.save();
            
            //新结算支付方式表
            List<OP_CostPayMentInfo> oldCostPayList = NewObject<OP_CostPayMentInfo>().getlist<OP_CostPayMentInfo>(" costheadid=" + oldCostHead.CostHeadID);
            if (refundPosType == 0)
            {
                oldCostPayList = oldCostPayList.Where(p => p.PayMentCode != "02").ToList();
                foreach (OP_CostPayMentInfo oldCostPay in oldCostPayList)
                {
                    OP_CostPayMentInfo newCostPay = oldCostPay.Clone() as OP_CostPayMentInfo;
                    newCostPay.ID = 0;
                    newCostPay.CostHeadID = newCostHead.CostHeadID;
                    newCostPay.AccountID = curAccountId;

                    //现金          
                    if (oldCostPay.PayMentCode != "01")
                    {
                        //退现金另外处理
                        newCostPay.PayMentMoney = oldCostPay.PayMentMoney * -1;
                        this.BindDb(newCostPay);
                        newCostPay.save();
                    }
                }

                if (newCostHead.CashFee != 0)
                {
                    OP_CostPayMentInfo cashCostPay = new OP_CostPayMentInfo();
                    cashCostPay.CostHeadID = newCostHead.CostHeadID;
                    cashCostPay.AccountID = curAccountId;
                    cashCostPay.PatListID = newCostHead.PatListID;
                    cashCostPay.PatName = newCostHead.PatName;
                    cashCostPay.PatType = newCostHead.PatTypeID.ToString();
                    cashCostPay.PayMentCode = "01";
                    cashCostPay.PayMentID = 1002;
                    cashCostPay.PayMentMoney = newCostHead.CashFee;
                    cashCostPay.PayMentName = "现金支付";
                    this.BindDb(cashCostPay);
                    cashCostPay.save();
                }
            }
            else if (refundPosType == 1)
            {
                foreach (OP_CostPayMentInfo oldCostPay in oldCostPayList)
                {
                    OP_CostPayMentInfo newCostPay = oldCostPay.Clone() as OP_CostPayMentInfo;
                    newCostPay.ID = 0;
                    newCostPay.CostHeadID = newCostHead.CostHeadID;
                    newCostPay.AccountID = curAccountId;                 
                    newCostPay.PayMentMoney = oldCostPay.PayMentMoney * -1;
                    this.BindDb(newCostPay);
                    newCostPay.save();
                }
            }

            //费用表插入红冲记录
            List<OP_FeeItemHead> oldFeeItemHeadList = NewObject<OP_FeeItemHead>().getlist<OP_FeeItemHead>(" costheadid=" + oldCostHead.CostHeadID);
            foreach (OP_FeeItemHead oldFeeHead in oldFeeItemHeadList)
            {
                oldFeeHead.ChargeStatus = 1;//退费状态
                this.BindDb(oldFeeHead);
                oldFeeHead.save();

                OP_FeeItemHead newFeeHead = oldFeeHead.Clone() as OP_FeeItemHead;
                newFeeHead.FeeItemHeadID = 0;
                newFeeHead.CostHeadID = newCostHead.CostHeadID;
                newFeeHead.OldID = oldFeeHead.FeeItemHeadID;//红冲退记录ID
                newFeeHead.ChargeStatus = 2;//新插入记录为红冲记录
                newFeeHead.ChargeEmpID = operatoreid;
                newFeeHead.ChargeDate = chargedate;
                newFeeHead.TotalFee = oldFeeHead.TotalFee * -1;
                this.BindDb(newFeeHead);
                newFeeHead.save();

                List<OP_FeeItemDetail> oldFeeItemDetailList = NewObject<OP_FeeItemDetail>().getlist<OP_FeeItemDetail>(" feeitemheadid=" + oldFeeHead.FeeItemHeadID);
                foreach (OP_FeeItemDetail oldFeeItemDetail in oldFeeItemDetailList)
                {
                    OP_FeeItemDetail newFeeItemDetail = oldFeeItemDetail.Clone() as OP_FeeItemDetail;
                    newFeeItemDetail.PresDetailID = 0;
                    newFeeItemDetail.TotalFee = oldFeeItemDetail.TotalFee * -1;
                    newFeeItemDetail.Amount = oldFeeItemDetail.Amount * -1;
                    newFeeItemDetail.FeeItemHeadID = newFeeHead.FeeItemHeadID;
                    this.BindDb(newFeeItemDetail);
                    newFeeItemDetail.save();
                }
            }

            //新结算明细表
            List<OP_CostDetail> oldCostDetailList = NewObject<OP_CostDetail>().getlist<OP_CostDetail>("costheadid=" + oldCostHead.CostHeadID);
            foreach (OP_CostDetail oldCostDetail in oldCostDetailList)
            {
                OP_CostDetail newCostDetail = oldCostDetail.Clone() as OP_CostDetail;
                newCostDetail.CostDetailID = 0;
                newCostDetail.CostHeadID = newCostHead.CostHeadID;//写入新CostHeadID
                newCostDetail.TotalFee = oldCostDetail.TotalFee * -1;
                newCostDetail.FeeItemHeadID = NewDao<IOPManageDao>().GetNewFeeItemHeadId(oldCostDetail.FeeItemHeadID);
                this.BindDb(newCostDetail);
                newCostDetail.save();
            }

            //退费消息表置退费状态
            List<OP_FeeRefundHead> refundHeadList = NewObject<OP_FeeRefundHead>().getlist<OP_FeeRefundHead>(" invoiceNum='" + refundInvoiceNO + "' and flag=0");
            if (refundHeadList.Count == 0)
            {
                throw new Exception("找不到退费消息");
            }

            OP_FeeRefundHead refundHead = refundHeadList[0] as OP_FeeRefundHead;
            refundHead.RefundPayFlag = 1;//退费完成状态
            this.BindDb(refundHead);
            refundHead.save();

            //减会员积分
            AddScore(newCostHead.MemberAccountID, newCostHead.TotalFee, newCostHead.CostHeadID.ToString(), operatoreid);
           
            //药品加虚拟库存
            MinisStorage(refundPrescriptions, true);
            AddAccoutFee(newCostHead, curAccountId, 0, 1);
           
            //修改医生站处方状态
            RefundDocPrsc(refundPrescriptions);
        }

        /// <summary>
        /// 收费时判断医生站处状态
        /// </summary>
        /// <param name="prescriptions">处方对象</param>
        public void CheckDocPrsc(List<Prescription> prescriptions)
        {
            string obj = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.HasOpDSystem);
            if (Convert.ToInt32(obj) == 1)
            {
                List<Prescription> newpres = prescriptions.Where((x, i) => prescriptions.FindIndex(z => z.DocPresHeadID == x.DocPresHeadID && z.DocPresHeadID > 0) == i).ToList();
                List<int> presDocnos = new List<int>();
                for (int i = 0; i < newpres.Count; i++)
                {
                    //判断医生处方有没有修改，如果有修改，则抛出异常  
                    OPD_PresDetail presDetail = NewObject<OPD_PresDetail>().getmodel(newpres[i].DocPresDetailID) as OPD_PresDetail;
                    if (presDetail == null)
                    {
                        throw new Exception("医生站处方有修改，请重新获取医生处方再收费");
                    }

                    if (newpres[i].DocPresDate != presDetail.PresDate)
                    {
                        throw new Exception("医生站处方有修改，请重新获取医生处方再收费");
                    }

                    presDocnos = prescriptions.Where(p => p.DocPresHeadID == newpres[i].DocPresHeadID).Select(p => p.DocPresNO).ToList();
                    NewObject<IOPManageDao>().UpdateDocPresStatus(newpres[i].DocPresHeadID, presDocnos,1);
                }
            }
        }

        /// <summary>
        /// 处方退费时候改医生站处方状态
        /// </summary>
        /// <param name="prescriptions">处方对象</param>
        private void RefundDocPrsc(List<Prescription> prescriptions)
        {
            List<Prescription> newpres = prescriptions.Where((x, i) => prescriptions.FindIndex(z => z.DocPresHeadID == x.DocPresHeadID && z.DocPresHeadID > 0) == i).ToList();
            List<int> presDocnos = new List<int>();
            for (int i = 0; i < newpres.Count; i++)
            {
                presDocnos = prescriptions.Where(p => p.DocPresHeadID == newpres[i].DocPresHeadID).Select(p => p.DocPresNO).ToList();
                NewObject<IOPManageDao>().UpdateDocPresStatus(newpres[i].DocPresHeadID, presDocnos,2);
            }
        }
    }
}
