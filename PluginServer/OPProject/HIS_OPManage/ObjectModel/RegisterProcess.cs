using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.BasicData;
using HIS_Entity.OPManage;
using HIS_OPManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_OPManage.ObjectModel
{
    /// <summary>
    /// 挂号处理对象
    /// </summary>
    public class RegisterProcess : AbstractObjectModel
    {
        /// <summary>
        /// 获取挂号支付方式
        /// </summary>
        /// <returns>DataTable挂号支付方式</returns>
        public DataTable GetRegPayMentType()
        {
            DataTable dtPayment = new DataTable();
            BasicDataManagement basicmanagement = NewObject<BasicDataManagement>();
            dtPayment = basicmanagement.GetBasicData(HIS_PublicManage.ObjectModel.PatientInfoDataSourceType.挂号支付方式, false);
            return dtPayment;
        }

        /// <summary>
        /// 由挂号类别得到挂号总金额
        /// </summary>
        /// <param name="regtypeid">挂号类别ID</param>
        /// <returns>decimal返回总金额</returns>
        public decimal GetRegTotalFee(int regtypeid)
        {
            decimal regFee = 0;
            regFee = NewObject<IOPManageDao>().GetRegtotalFee(regtypeid);
            return regFee;
        }

        #region 挂号提交
        /// <summary>
        /// 挂号提交
        /// </summary>
        /// <param name="curPatlist">病人对象</param>
        /// <param name="paymentInfoList">支付对象</param>
        /// <param name="totalFee">总金额</param>
        /// <param name="cashFee">现金金额</param>
        /// <param name="posFee">POS金额</param>
        /// <param name="dtPrint">返回的打印数据</param>
        /// <param name="promFee">优惠金额</param>
        /// <returns>true成功false失败</returns>
        public bool SaveRegInfo(OP_PatList curPatlist, PayMentInfoList paymentInfoList, decimal totalFee, decimal cashFee, decimal posFee,out DataTable dtPrint,decimal promFee)
        {
            try
            {
                dtPrint = new DataTable();
                bool result = false;

                //插入挂号就诊表记录
                #region 插入挂号就诊表记录
                SerialNumberSource serialNumberSource = NewObject<SerialNumberSource>();
                curPatlist.VisitNO = serialNumberSource.GetSerialNumber(SnType.门诊流水号);
                curPatlist.CureDeptID = curPatlist.RegDeptID;
                curPatlist.CureEmpID = curPatlist.RegEmpID;
                curPatlist.RegDate = DateTime.Now;
                curPatlist.VisitStatus = 0;
                curPatlist.RegStatus = 0;
                curPatlist.RegCategory = 0;
                DataTable dtOld = NewObject<OP_PatList>().gettable(" MemberID=" + curPatlist.MemberID);
                if (dtOld != null && dtOld.Rows.Count > 0)
                {
                    curPatlist.IsNew = 0;
                }
                else
                {
                    curPatlist.IsNew = 1;
                }

                this.BindDb(curPatlist);
                curPatlist.save();
                #endregion
                int iAccountType = 0;

                //得到当前结账ID
                int curAccountId = NewObject<CommonMethod>().GetAccountId(curPatlist.OperatorID, iAccountType);

                //插入结算主表对象
                #region 插入结算主表对象
                OP_CostHead costHead = new OP_CostHead();
                SetRegCostHead(costHead, curPatlist);
                costHead.CashFee = cashFee;
                costHead.PosFee = posFee;              
                costHead.TotalFee = totalFee;
                costHead.PromFee = promFee;          
                costHead.AccountID = curAccountId;
                this.BindDb(costHead);
                costHead.save();
                #endregion

                #region 插入费用表

                //插入费用主表 OP_FeeItemHead
                OP_FeeItemHead feeItemHead = new OP_FeeItemHead();
                SetRegFeeHeadValue(feeItemHead,  costHead, curPatlist);
                this.BindDb(feeItemHead);
                feeItemHead.save();

                //插入费用明细表 OP_FeeItemDetail               
                List<OP_FeeItemDetail> feeDetials = SetRegFeeDetailValue(feeItemHead.FeeItemHeadID, curPatlist);
                foreach (OP_FeeItemDetail feeDetial in feeDetials)
                {
                    this.BindDb(feeDetial);
                    feeDetial.save();
                }
                #endregion

                //插入结算明细表
                #region 插入结算明细表
                DataTable dtRegFeeDetail = NewDao<IOPManageDao>().GetRegItemFeeByStat(curPatlist.RegTypeID);
                for (int rowindex = 0; rowindex < dtRegFeeDetail.Rows.Count; rowindex++)
                {
                    OP_CostDetail costDetail = new OP_CostDetail();
                    costDetail.CostHeadID = costHead.CostHeadID;
                    costDetail.FeeItemHeadID = feeItemHead.FeeItemHeadID;
                    costDetail.ExeDeptID = curPatlist.RegDeptID;
                    costDetail.PresEmpID = curPatlist.RegEmpID;
                    costDetail.PresDeptID = curPatlist.RegDeptID;
                    costDetail.TotalFee = Convert.ToDecimal(dtRegFeeDetail.Rows[rowindex]["statFee"]);
                    costDetail.StatID =Convert.ToInt32( dtRegFeeDetail.Rows[rowindex]["statId"]);
                    this.BindDb(costDetail);
                    costDetail.save();
                }
                #endregion

                //插入结算支付方式明细表
                #region 插入结算支付方式明细表
                foreach (OP_CostPayMentInfo costpaymentinfo in paymentInfoList.paymentInfolist)
                {
                    if (costpaymentinfo.PayMentMoney > 0)
                    {                       
                        costpaymentinfo.AccountID = curAccountId;
                        costpaymentinfo.CostHeadID = costHead.CostHeadID;
                        costpaymentinfo.PatListID = curPatlist.PatListID;
                        costpaymentinfo.PatName = curPatlist.PatName;
                        costpaymentinfo.PatType = costHead.PatTypeID.ToString();
                        this.BindDb(costpaymentinfo);
                        costpaymentinfo.save();
                    }
                }
                #endregion

                //挂号记录表回写costheadid
                curPatlist.CostHeadID = costHead.CostHeadID;
                curPatlist.ChargeFlag = 1;                
                this.BindDb(curPatlist);
                curPatlist.save();

                //结账表插入汇总金额
                NewObject<CommonMethod>().AddAccoutFee(costHead,curAccountId,1,0);

                //查数据库得到打印dtPrint
                dtPrint = NewObject<IOPManageDao>().GetRegPrint(curPatlist.PatListID);
                result = true;
                return result;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 结算主表赋值
        /// </summary>
        /// <param name="costHead">结算对象</param>
        /// <param name="curPatlist">病人对象</param>
        private void SetRegCostHead(OP_CostHead costHead, OP_PatList curPatlist)
        {
            costHead.MemberID = curPatlist.MemberID;
            costHead.MemberAccountID = curPatlist.MemberAccountID;
            costHead.CardNO = curPatlist.CardNO;
            costHead.PatListID = curPatlist.PatListID;
            costHead.PatName = curPatlist.PatName;
            costHead.PatTypeID = curPatlist.PatTypeID;
            costHead.CostDate = curPatlist.RegDate;
            Basic_Invoice basicInvoice = NewObject<CommonMethod>().GetCurInvoice(InvoiceType.门诊挂号, curPatlist.OperatorID);
            string invoiceNo = string.Empty;
            string perfChar = string.Empty;
            invoiceNo = NewObject<InvoiceManagement>().GetInvoiceCurNOAndUse(InvoiceType.门诊挂号, curPatlist.OperatorID, out perfChar);
            invoiceNo = perfChar + invoiceNo;
            costHead.BeInvoiceNO = invoiceNo;
            costHead.EndInvoiceNO = invoiceNo;
            costHead.RecipeFlag = 0;
            costHead.RegCategory = curPatlist.RegCategory;
            costHead.RegFlag = 1;           
            costHead.ChargeEmpID = curPatlist.OperatorID;
            costHead.InvoiceID = basicInvoice.ID;         
        }

        /// <summary>
        /// 费用主表赋值
        /// </summary>
        /// <param name="feeItemHead">费用主表对象</param>
        /// <param name="costHead">结算对象</param>
        /// <param name="curPatlist">病人对象</param>
        private void SetRegFeeHeadValue(OP_FeeItemHead feeItemHead,OP_CostHead costHead,OP_PatList curPatlist)
        {
            feeItemHead.CostHeadID = costHead.CostHeadID;
            feeItemHead.MemberID = curPatlist.MemberID;
            feeItemHead.PatListID = curPatlist.PatListID;
            feeItemHead.PatName = curPatlist.PatName;
            feeItemHead.PresType = "0";
            feeItemHead.PresEmpID = curPatlist.RegEmpID;
            feeItemHead.PresDeptID = curPatlist.RegDeptID;
            feeItemHead.ExecDeptID = curPatlist.RegDeptID;
            feeItemHead.ExecEmpID = curPatlist.RegEmpID;
            feeItemHead.ChargeEmpID = curPatlist.OperatorID;
            feeItemHead.PresAmount = 1;
            feeItemHead.TotalFee = costHead.TotalFee;
            feeItemHead.InvoiceNO = costHead.EndInvoiceNO;
            feeItemHead.ChargeStatus = 0;
            feeItemHead.ChargeFlag = 1;
            feeItemHead.PresDate = curPatlist.RegDate;
            feeItemHead.DocPresHeadID = 0;
            feeItemHead.ChargeDate = curPatlist.RegDate;
            feeItemHead.FeeNo = 0;
            feeItemHead.RegFlag = 1;
        }

        /// <summary>
        /// 费用明细表赋值
        /// </summary>
        /// <param name="feeItemheadid">费用头表ID</param>
        /// <param name="curPatlist">病人对象</param>
        /// <returns>返回费用明细</returns>
        private List<OP_FeeItemDetail> SetRegFeeDetailValue(int feeItemheadid, OP_PatList curPatlist)
        {
            DataTable dtRegFeeDetails = NewDao<IOPManageDao>().GetRegItemFees(curPatlist.RegTypeID);
            List<OP_FeeItemDetail> listFeeItemDetail = new List<OP_FeeItemDetail>();    
            for (int i = 0; i < dtRegFeeDetails.Rows.Count; i++)
            {
                OP_FeeItemDetail feeItemDetail = new OP_FeeItemDetail();
                feeItemDetail.FeeItemHeadID = feeItemheadid;
                feeItemDetail.MemberID = curPatlist.MemberID;
                feeItemDetail.PatListID = curPatlist.PatListID;
                feeItemDetail.ItemID =Convert.ToInt32( dtRegFeeDetails.Rows[i]["itemid"]);
                feeItemDetail.ItemName =dtRegFeeDetails.Rows[i]["itemname"].ToString();
                feeItemDetail.StatID=Convert.ToInt32( dtRegFeeDetails.Rows[i]["StatID"]);
                feeItemDetail.ItemType = dtRegFeeDetails.Rows[i]["ItemClass"].ToString();
                feeItemDetail.Spec= dtRegFeeDetails.Rows[i]["Standard"].ToString();
                feeItemDetail.PackUnit = dtRegFeeDetails.Rows[i]["UnPickUnit"].ToString();
                feeItemDetail.UnitNO =Convert.ToDecimal( dtRegFeeDetails.Rows[i]["MiniConvertNum"]);
                feeItemDetail.StockPrice = Convert.ToDecimal(dtRegFeeDetails.Rows[i]["InPrice"]);
                feeItemDetail.RetailPrice = Convert.ToDecimal(dtRegFeeDetails.Rows[i]["SellPrice"]);
                feeItemDetail.Amount =1;
                feeItemDetail.PresAmount = 1;
                feeItemDetail.TotalFee = feeItemDetail.RetailPrice;
                feeItemDetail.ExamItemID = 0;
                feeItemDetail.DocPresDetailID = 0;
                feeItemDetail.MiniUnit= dtRegFeeDetails.Rows[i]["MiniUnitName"].ToString();
                listFeeItemDetail.Add(feeItemDetail);
            }

            return listFeeItemDetail;
        }
        #endregion

        /// <summary>
        /// 通过票据号获取票据号对应的挂号支付信息
        /// </summary>
        /// <param name="invoiceNO">票据号</param>
        /// <param name="payMentInfoList">支付信息</param>
        /// <param name="opPatlist">病人对象</param>
        /// <returns>挂号支付信息</returns>
        public bool GetRegInfoByInvoiceNO(string invoiceNO,out PayMentInfoList payMentInfoList,out OP_PatList opPatlist)
        {
            bool result = false;
            List<OP_CostHead> costList = NewObject<OP_CostHead>().getlist<OP_CostHead>(" EndInvoiceNO='" + invoiceNO + "' and regflag=1");
            if (costList == null || costList.Count == 0)
            {
                throw new Exception("找不到该票据号对应的挂号信息");
            }
            else if (costList[0].CostStatus != 0)
            {
                throw new Exception("该票据号已经退费，不能再退");
            }
            else if (costList[0].CostStatus == 0 && costList[0].CostDate.ToString("yyyy-MM-dd") != DateTime.Now.ToString("yyyy-MM-dd"))
            {
                throw new Exception("已超过退号日期，只能退当天挂的号");
            }
            else
            {
                 opPatlist = NewObject<OP_PatList>().getmodel( costList[0].PatListID) as OP_PatList;
                if (opPatlist.VisitStatus != 0)
                {
                    throw new Exception("您已就诊，不能再退号");
                }

                payMentInfoList = new PayMentInfoList(); 
                List<OP_CostPayMentInfo> payInfos = NewObject<OP_CostPayMentInfo>().getlist<OP_CostPayMentInfo>(" costheadid="+costList[0].CostHeadID);
                payMentInfoList.paymentInfolist = payInfos;               
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 退号提交
        /// </summary>
        /// <param name="invoiceNO">票据号</param>
        /// <param name="operatorID">操作员ID</param>
        public void BackReg(string invoiceNO, int operatorID)
        {
            int iAccountType = 0;
            List<OP_CostHead> costList = NewObject<OP_CostHead>().getlist<OP_CostHead>(" endInvoiceNO='" + invoiceNO + "' and regFlag=1");
            if (costList == null || costList.Count == 0)
            {
                throw new Exception("找不到该票据号信息");
            }
            else if (costList[0].CostStatus != 0)
            {
                throw new Exception("该票据号已经退费，不能再退");
            }
            else if (costList[0].CostStatus == 0 && costList[0].CostDate.ToString("yyyy-MM-dd") != DateTime.Now.ToString("yyyy-MM-dd"))
            {
                throw new Exception("已超过退号日期，只能退当天挂的号");
            }

            //原op_patlist变化
            OP_PatList patlist = NewObject<OP_PatList>().getmodel(costList[0].PatListID) as OP_PatList;
            if (patlist.VisitStatus != 0)
            {
                throw new Exception("您已就诊，不能再退号");
            }

            patlist.RegStatus = 1;//原挂号记录状态置1
            this.BindDb(patlist);
            patlist.save();

            //得到当前结账ID
            int curAccountId = NewObject<CommonMethod>().GetAccountId(operatorID, iAccountType);

            //原op_costHead变化
            OP_CostHead oldCostHead = costList[0].Clone() as OP_CostHead;
            oldCostHead.CostStatus = 1;//原有记录状态改为1
            this.BindDb(oldCostHead);
            oldCostHead.save();

            //生成新op_costHead变化
            OP_CostHead newCostHead = costList[0].Clone() as OP_CostHead;
            newCostHead.CostHeadID = 0;
            newCostHead.CostDate = DateTime.Now;
            newCostHead.CostStatus = 2;//新增记录状态为2
            newCostHead.ChargeEmpID = operatorID;
            newCostHead.AccountID = curAccountId;
            newCostHead.CashFee = (oldCostHead.CashFee + oldCostHead.PosFee) * (-1);//退金额，POS金额退现金
            newCostHead.PosFee = 0;
            newCostHead.PromFee = oldCostHead.PromFee * (-1);
            newCostHead.OldID = oldCostHead.CostHeadID;//写入原退记录ID
            newCostHead.TotalFee = newCostHead.TotalFee * (-1);
            this.BindDb(newCostHead);
            newCostHead.save();

            //得到原来OP_FeeItemHead
            List<OP_FeeItemHead> listFeeitemHead = NewObject<OP_FeeItemHead>().getlist<OP_FeeItemHead>(" costheadid=" + oldCostHead.CostHeadID);
            OP_FeeItemHead newFeeItemHead = new OP_FeeItemHead();
            if (listFeeitemHead != null && listFeeitemHead.Count > 0)
            {
                OP_FeeItemHead oldFeeItemHead = listFeeitemHead[0].Clone() as OP_FeeItemHead;
                oldFeeItemHead.ChargeStatus = 1;//原有记录状态改为1
                this.BindDb(oldFeeItemHead);
                oldFeeItemHead.save();

                //生成新OP_FeeItemHead变化
                 newFeeItemHead = listFeeitemHead[0].Clone() as OP_FeeItemHead;
                newFeeItemHead.FeeItemHeadID = 0;
                newFeeItemHead.ChargeStatus = 2;
                newFeeItemHead.CostHeadID = newCostHead.CostHeadID;
                newFeeItemHead.TotalFee = newFeeItemHead.TotalFee * (-1);
                newFeeItemHead.OldID = oldFeeItemHead.FeeItemHeadID;//写入原退记录ID
                this.BindDb(newFeeItemHead);
                newFeeItemHead.save();

                List<OP_FeeItemDetail> listFeeItemDetail = NewObject<OP_FeeItemDetail>().getlist<OP_FeeItemDetail>(" FeeItemHeadID="+oldFeeItemHead.FeeItemHeadID);
                foreach (OP_FeeItemDetail oldfeeitemDetail in listFeeItemDetail)
                {
                    OP_FeeItemDetail newfeeitemDetail = oldfeeitemDetail.Clone() as OP_FeeItemDetail;
                    newfeeitemDetail.PresDetailID = 0;
                    newfeeitemDetail.FeeItemHeadID = newFeeItemHead.FeeItemHeadID;
                    newfeeitemDetail.TotalFee = newfeeitemDetail.TotalFee * (-1);
                    newfeeitemDetail.Amount = oldfeeitemDetail.Amount * (-1);
                    this.BindDb(newfeeitemDetail);
                    newfeeitemDetail.save();
                }
            }

            //生成新op_costDetail
            List<OP_CostDetail> costDetailList = NewObject<OP_CostDetail>().getlist<OP_CostDetail>(" costheadid=" + oldCostHead.CostHeadID);
            foreach (OP_CostDetail oldcostDetail in costDetailList)
            {
                OP_CostDetail newCostDetail = oldcostDetail.Clone() as OP_CostDetail;
                newCostDetail.CostHeadID = newCostHead.CostHeadID;
                newCostDetail.FeeItemHeadID = newFeeItemHead.FeeItemHeadID;
                newCostDetail.TotalFee = oldcostDetail.TotalFee * (-1);                
                newCostDetail.CostDetailID = 0;
                this.BindDb(newCostDetail);
                newCostDetail.save();
            }

            //生成新OP_CostPayMentInfo
            List<OP_CostPayMentInfo> costPayMentInfoList = NewObject<OP_CostPayMentInfo>().getlist<OP_CostPayMentInfo>(" costheadid=" + oldCostHead.CostHeadID);
            decimal oldposfee = 0;
            foreach (OP_CostPayMentInfo oldCostpayInfo in costPayMentInfoList)
            {
                if (oldCostpayInfo.PayMentCode == "02")
                {
                    oldposfee = oldCostpayInfo.PayMentMoney;
                }
            }

            foreach (OP_CostPayMentInfo oldCostpayInfo in costPayMentInfoList)
            {
                OP_CostPayMentInfo newCostPayInfo = oldCostpayInfo.Clone() as OP_CostPayMentInfo;
                newCostPayInfo.AccountID = curAccountId;
                newCostPayInfo.ID = 0;
                newCostPayInfo.CostHeadID = newCostHead.CostHeadID;

                //POS
                if (oldCostpayInfo.PayMentCode == "02")
                {
                    newCostPayInfo.PayMentMoney = 0;
                }

                //现金
                if (oldCostpayInfo.PayMentCode == "01")
                {
                    newCostPayInfo.PayMentMoney = (oldCostpayInfo.PayMentMoney + oldposfee) * (-1);
                }
                else
                {
                    newCostPayInfo.PayMentMoney = oldCostpayInfo.PayMentMoney * (-1);
                }

                this.BindDb(newCostPayInfo);
                newCostPayInfo.save();
            }

            //结账表插入汇总金额
            NewObject<CommonMethod>().AddAccoutFee(newCostHead, curAccountId, 0, 1);
        }
    }
}
