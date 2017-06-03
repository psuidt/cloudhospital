using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.ClinicManage;
using HIS_Entity.MemberManage;
using HIS_Entity.OPManage;
using HIS_OPManage.Dao;
using HIS_OPManage.ObjectModel;
using HIS_PublicManage.ObjectModel;

namespace HIS_OPManage.WcfController
{
    /// <summary>
    /// 门诊收费控制器类
    /// </summary>
    [WCFController]
    public class BalanceController : WcfServerController
    {
        /// <summary>
        /// 收费界面初始化
        /// </summary>
        /// <returns>返回收费界面基础数据</returns>
        [WCFMethod]
        public ServiceResponseData BalanceDataInit()
        {
            int operatorID = requestData.GetData<int>(0);

            BasicDataManagement basicmanagement = NewObject<BasicDataManagement>();
            DataTable dtCardType = NewObject<ME_CardTypeList>().gettable(" UseFlag=1");
            responseData.AddData(dtCardType);//卡类型

            DataTable dtPattype = basicmanagement.GetPatType(false);
            responseData.AddData(dtPattype);//病人类型

            DataTable dtDept = basicmanagement.GetBasicData(DeptDataSourceType.全部科室);
            responseData.AddData(dtDept);//门诊科室

            DataTable dtDoctor = basicmanagement.GetBasicData(EmpDataSourceType.医生);
            responseData.AddData(dtDoctor);//医生

            string obj = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.HasOpDSystem); //判断是否上医生站，上了医生站不能录入药品和组合项目

            string curInvoiceNO = string.Empty;
            int invoiceCount = NewObject<CommonMethod>().GetInvoiceInfo(InvoiceType.门诊收费, operatorID, out curInvoiceNO);
            responseData.AddData(invoiceCount);//可用票据张数
            responseData.AddData(curInvoiceNO);//当前可用票据号   

            responseData.AddData(Convert.ToInt32(obj));
            return responseData;
        }

        /// <summary>
        /// 药品项目数据源
        /// </summary>
        /// <returns>返回药品项目信息</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugItemShowCard()
        {
            FeeItemDataSource feeitem = NewObject<FeeItemDataSource>();
            string obj = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.HasOpDSystem); //判断是否上医生站，上了医生站不能录入药品和组合项目
            if (Convert.ToInt32(obj) == 0)
            {
                DataTable dtDrugItem = feeitem.GetSimpleFeeItemDataDt(FeeBusinessType.处方业务);//项目和药品选项卡来源数据
                responseData.AddData(dtDrugItem);//药品项目选项卡数据
            }
            else
            {
                DataTable dtDrugItem = feeitem.GetSimpleFeeItemDataDt(FeeBusinessType.基础业务);//项目和药品选项卡来源数据
                responseData.AddData(dtDrugItem);//药品项目选项卡数据
            }          
              
            return responseData;
        }

        /// <summary>
        /// 获取诊断
        /// </summary>
        /// <returns>返回诊断信息</returns>
        [WCFMethod]
        public ServiceResponseData GetDiseaseShowCard()
        {
            DataTable dtDisease = NewObject<BasicDataManagement>().GetDisease();
            responseData.AddData(dtDisease);//诊断
            return responseData;
        }

        /// <summary>
        /// 通过指定查询类别和内容获取挂号病人信息
        /// </summary>   
        /// <returns>返回挂号病人信息</returns>
        [WCFMethod]
        public ServiceResponseData GetRegPatListByCardNo()
        {
            OP_Enum.MemberQueryType queryType = requestData.GetData<OP_Enum.MemberQueryType>(0);//查找类型
            string content = requestData.GetData<string>(1);//卡号
            List<OP_PatList> patlist = NewObject<OutPatient>().GetPatlist(queryType, content);
            responseData.AddData(patlist);
            return responseData;
        }

        /// <summary>
        /// 通过发票号获取病人信息和退费处方信息
        /// </summary>
        /// <returns>返回病人信息和退费处方信息</returns>
        [WCFMethod]
        public ServiceResponseData GetBackFeeByInvoiceNO()
        {
            try
            {
                OP_Enum.MemberQueryType queryType = requestData.GetData<OP_Enum.MemberQueryType>(0);//查找类型
                string content = requestData.GetData<string>(1);//退费发票号 
                List<OP_FeeRefundHead> listRefundHead = NewObject<OP_FeeRefundHead>().getlist<OP_FeeRefundHead>(" invoicenum='" + content + "' and flag=0 and RefundPayFlag=0");
                if (listRefundHead == null || listRefundHead.Count == 0)
                {
                    throw new Exception("查不到该票号对应的退费消息");
                }

                List<OP_FeeRefundDetail> refundDetailList = NewObject<OP_FeeRefundDetail>().getlist<OP_FeeRefundDetail>(" ReHeadID=" + listRefundHead[0].ReHeadID);
                foreach (OP_FeeRefundDetail feerefundDetail in refundDetailList)
                {
                    if (feerefundDetail.RefundAmount > 0 && feerefundDetail.DistributeFlag == 1 && feerefundDetail.RefundFlag == 0)
                    {
                        throw new Exception("已经发药处方要先退药才能退费");
                    }
                }

                OP_PatList patlist = NewObject<OP_PatList>().getmodel(listRefundHead[0].PatListID) as OP_PatList;
                responseData.AddData(patlist);
                List<Prescription> preslist = NewObject<PrescriptionProcess>().GetPrescriptionByInvoiceNo(content, patlist.PatListID);
                DataTable dtRefund = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable<Prescription>(preslist);// EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable(preslist);
                responseData.AddData(dtRefund);

                int costheadid = preslist[0].CostHeadID;
                OP_CostHead costHead = NewObject<OP_CostHead>().getmodel(costheadid) as OP_CostHead;
                responseData.AddData(costHead.PatTypeID);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 通过组合内容获取挂号病人信息
        /// </summary>
        /// <returns>返回挂号病人信息</returns>
        [WCFMethod]
        public ServiceResponseData GetRegPatListByOther()
        {
            string nameContent = requestData.GetData<string>(0);//姓名
            string telContent = requestData.GetData<string>(1);//电话号码
            string idContent = requestData.GetData<string>(2);//身份证号
            string mediCard = requestData.GetData<string>(3);//医保卡号
            DateTime bdate = requestData.GetData<DateTime>(4);//开始日期
            DateTime edate = requestData.GetData<DateTime>(5);//结束日期
            List<OP_PatList> patlist = NewObject<OutPatient>().GetPatlist(nameContent, telContent, idContent,mediCard, bdate, edate);
            responseData.AddData(patlist);
            return responseData;
        }

        #region 处方操作
        /// <summary>
        /// 获取处方总金额
        /// </summary>
        /// <returns>返回处方总金额</returns>
        [WCFMethod]
        public ServiceResponseData GetPrescriptionTotalMoney()
        {
            List<Prescription> prescDetails = requestData.GetData<List<Prescription>>(0);
            decimal totalfee = NewObject<PrescMoneyCalculate>().GetPrescriptionTotalMoney(prescDetails);
            responseData.AddData(totalfee);
            return responseData;
        }

        /// <summary>
        /// 收费处获取病人处方信息
        /// </summary>
        /// <returns>返回处方信息</returns>
        [WCFMethod]
        public ServiceResponseData GetPatPrescription()
        {
            int patlistid = requestData.GetData<int>(0);//病人ID
            OP_Enum.PresStatus presStatus = requestData.GetData<OP_Enum.PresStatus>(1); //处方状态
            int costPatTypeID = requestData.GetData<int>(2);//病人结算类型
            bool isMedicarePat = NewObject<CommonMethod>().IsMedicarePat(costPatTypeID);//根据病人类型ID判断是否是医保病人
            List<Prescription> preslist = NewObject<PrescriptionProcess>().GetPrescriptions(patlistid, presStatus, true);
            
            #region 应北京医保要求，相关大项目ID相同的只取第一条
            //应北京医保要求，相关大项目ID相同的只取第一条
            if (isMedicarePat)
            {
                string statString = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.MedicalNotBalanceStatID);
                if (!string.IsNullOrEmpty( statString ))
                {
                    string[] statIDs = statString.Split(',');
                    for (int i = 0; i < statIDs.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(statIDs[i]))
                        {
                            List<Prescription> presSameList = preslist.Where(p => p.StatID == Convert.ToInt32(statIDs[i])).OrderBy(p => p.ItemID).ThenBy(p=> p.PrescGroupID).ToList();                           
                            if (presSameList.Count > 1)
                            {                               
                                int itemid = presSameList[0].ItemID;
                                for (int j = 1; j < presSameList.Count; j++)
                                {
                                    if (presSameList[j].ItemID == itemid)
                                    {
                                        int groupid = presSameList[j].PrescGroupID;
                                        preslist.Remove(presSameList[j]);
                                        List<Prescription> presGroupList = preslist.FindAll(p => p.PrescGroupID == groupid);
                                        if (presGroupList.Count > 0 && presGroupList.Count == 1)
                                        {
                                            preslist.Remove(presGroupList[0]);
                                        }
                                        else
                                        {
                                            List<Prescription> totalPres = presGroupList.FindAll(p => p.SubTotalFlag == 1);
                                            if (totalPres.Count > 0)
                                            {
                                                totalPres[0].TotalFee = totalPres[0].TotalFee - presSameList[j].TotalFee;
                                            }
                                        }
                                    }

                                    itemid = presSameList[j].ItemID;
                                }
                            }
                        }
                    }                   
                }
            }
            #endregion
            //返回处方信息
            DataTable dtPres = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable<Prescription>(preslist);// EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable(preslist);
            responseData.AddData(dtPres);

            //返回医生诊断信息
            List<OPD_DiagnosisRecord> diagnoseList = NewObject<OPD_DiagnosisRecord>().getlist<OPD_DiagnosisRecord>(" PatlistID=" + patlistid).OrderBy(p => p.SortNo).ToList();
            responseData.AddData(diagnoseList);
            return responseData;
        }

        /// <summary>
        /// 删除整张处方
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData DeletePrescription()
        {
            int feeItemHeadID = requestData.GetData<int>(0);//费用头ID
            NewObject<PrescriptionProcess>().DeletePrescription(feeItemHeadID);
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 删除处方明细
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData DeletePrescriptionDetail()
        {
            int feeItemDetailID = requestData.GetData<int>(0);//费用明细ID
            NewObject<PrescriptionProcess>().DeletePrescriptionDetail(feeItemDetailID);
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 保存处方明细
        /// </summary>
        /// <returns>返回保存后的处方</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SavePrescription()
        {
            OP_PatList curPatlist = requestData.GetData<OP_PatList>(0);
            List<Prescription> prescriptions = requestData.GetData<List<Prescription>>(1);
            List<int> presNum = requestData.GetData<List<int>>(2);
            int opratorid = requestData.GetData<int>(3);
            NewObject<PrescriptionProcess>().SavePrescription(curPatlist, prescriptions, presNum, opratorid);
            responseData.AddData(prescriptions);
            return responseData;
        }

        /// <summary>
        /// 获取组合项目明细
        /// </summary>
        /// <returns>返回组合项目明细</returns>
        [WCFMethod]
        public ServiceResponseData GetExamItemDetailDt()
        {
            int examItemID = requestData.GetData<int>(0);
            FeeItemDataSource feeitem = NewObject<FeeItemDataSource>();
            DataTable dtDetailItem = feeitem.GetExamItemDetailDt(examItemID);
            responseData.AddData(dtDetailItem);//药品项目选项卡数据          
            return responseData;
        }
        #endregion

        /// <summary>
        /// 调整发票号
        /// </summary>
        /// <returns>返回当前票据号</returns>
       [WCFMethod]
        public ServiceResponseData AdjustInvoice()
        {
            try
            {
                string perfChar = requestData.GetData<string>(0);
                string invoiceNO = requestData.GetData<string>(1);
                int operatorid = requestData.GetData<int>(2);
                InvoiceManagement invoiceManager = NewObject<InvoiceManagement>();
                invoiceManager.AdjustInvoiceNo(InvoiceType.门诊收费, operatorid, perfChar, invoiceNO);
                string curInvoiceNo = NewObject<CommonMethod>().GetCurInvoiceNO(InvoiceType.门诊收费, operatorid);
                responseData.AddData(curInvoiceNo);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        #region 处方收银
        /// <summary>
        /// 判断收费所选的病人类型是否是医保病人
        /// </summary>
        /// <returns>true 医保病人 false非医保病人</returns>
        [WCFMethod]       
        public ServiceResponseData IsMediaPat()
        {
            try
            {
                int patTypeid = requestData.GetData<int>(0);
                bool isMedicarePat = NewObject<CommonMethod>().IsMedicarePat(patTypeid);//根据病人类型ID判断是否是医保病人
                responseData.AddData(isMedicarePat);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 优惠金额计算
        /// </summary>
        /// <returns>返回优惠金额</returns>
        [WCFMethod]       
        public ServiceResponseData PromFeeCaculate()
        {
            try
            {
                int costPatTypeid = requestData.GetData<int>(0);
                int memberAccountID = requestData.GetData<int>(1);
                decimal totalFee = requestData.GetData<decimal>(2);
                List<Prescription> prescriptions = requestData.GetData<List<Prescription>>(3);
                int operatoreID = requestData.GetData<int>(4);
                int costHeadid = requestData.GetData<int>(5);
                #region  构造明细表
                DataTable dtDetail = new DataTable();
                DataColumn col = new DataColumn();
                col.ColumnName = "ItemTypeID";
                col.DataType = typeof(decimal);
                dtDetail.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "PresDetailId";
                col.DataType = typeof(int);
                dtDetail.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "ItemAmount";
                col.DataType = typeof(decimal);
                dtDetail.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "PromAmount";
                col.DataType = typeof(decimal);
                dtDetail.Columns.Add(col);
                foreach (Prescription presc in prescriptions)
                {
                    DataRow dr = dtDetail.NewRow();
                    dr["ItemTypeID"] = presc.ItemID;
                    dr["PresDetailId"] = presc.PresDetailID;
                    dr["ItemAmount"] = presc.TotalFee;
                    dr["PromAmount"] = 0;
                    dtDetail.Rows.Add(dr);
                }
                #endregion

                #region 构造分类表
                var result = from p in prescriptions.AsEnumerable()
                             group p by p.StatID into g
                             select new
                             {
                                 g.Key,
                                 SumValue = g.Sum(p => p.TotalFee)
                             };
                DataTable dtPromClass = new DataTable();
                col = new DataColumn();
                col.ColumnName = "ClassTypeID";
                col.DataType = typeof(string);
                dtPromClass.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "ClassAmount";
                col.DataType = typeof(decimal);
                dtPromClass.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "PromAmount";
                col.DataType = typeof(decimal);
                dtPromClass.Columns.Add(col);
                foreach (var stat in result)
                {
                    DataRow dr = dtPromClass.NewRow();
                    dr["ClassTypeID"] = stat.Key;
                    dr["ClassAmount"] = stat.SumValue;
                    dr["PromAmount"] = 0;
                    dtPromClass.Rows.Add(dr);
                }
                #endregion

                DataTable outdtPromClass = new DataTable();
                DataTable outDetail = new DataTable();
                PromotionManagement promManager = NewObject<PromotionManagement>();
                DiscountInfo discountinfo = new DiscountInfo();
                discountinfo.AccountID = memberAccountID;
                discountinfo.CostType = costPatTypeid;
                discountinfo.PatientType = 1;
                discountinfo.Amount = totalFee;
                discountinfo.OperateID = operatoreID;
                discountinfo.SettlementNO = costHeadid.ToString();
                discountinfo.DtDetail = dtDetail;
                discountinfo.DtClass = dtPromClass;
                discountinfo.IsSave = true;
                discountinfo.AccID = costHeadid;//结算ID
                DiscountInfo resDiscountInfo = promManager.CalculationPromotion(discountinfo);
                responseData.AddData(resDiscountInfo.DisAmount);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 收银预算
        /// </summary>
        /// <returns>处方预算信息</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData BudgeBalance()
        {
            try
            {
                List<Prescription> prescriptions = requestData.GetData<List<Prescription>>(0);//预算处方
                int operatorid = requestData.GetData<int>(1);//操作员ID
                OP_PatList curPatlist = requestData.GetData<OP_PatList>(2);//当前病人对象
                int costPatTypeid = requestData.GetData<int>(3);      //结算病人类型 
                List<int> budgePresNum = requestData.GetData<List<int>>(4);//选中的处方张数

                string balanceType = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.BalanceType);//结算方式
                OP_Enum.BalanceType chargeType = (OP_Enum.BalanceType)Convert.ToInt32(balanceType);
                BaseBalaceProcess balanceProcess = NewObject<BalanceFactory>().CreateChargeObject(chargeType);
                balanceProcess.BaseBalaceProcessInit(curPatlist, operatorid, chargeType, budgePresNum, costPatTypeid);//初始化
                List<ChargeInfo> listChargeInfo = balanceProcess.Budget(prescriptions);//处方预算
                responseData.AddData(listChargeInfo);

                bool isMedicarePat = NewObject<CommonMethod>().IsMedicarePat(costPatTypeid);//判断是否是医保病人 
                responseData.AddData(isMedicarePat);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 收银正式结算
        /// </summary>
        /// <returns>成功返回票据打印信息</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData Balance()
        {
            try
            {
                List<Prescription> prescriptions = requestData.GetData<List<Prescription>>(0);//预算处方
                int operatoreid = requestData.GetData<int>(1);//操作员ID
                OP_PatList curPatlist = requestData.GetData<OP_PatList>(2);//当前病人对象         
                List<ChargeInfo> budgeInfo = requestData.GetData<List<ChargeInfo>>(3);//金额预算信息
                string balanceType = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.BalanceType);
                OP_Enum.BalanceType chargeType = (OP_Enum.BalanceType)Convert.ToInt32(balanceType);
                BaseBalaceProcess balanceProcess = NewObject<BalanceFactory>().CreateChargeObject(chargeType);
                balanceProcess.Balance(curPatlist, operatoreid, budgeInfo, prescriptions);//处方正式结算
                responseData.AddData(budgeInfo);
                string curInvoiceNO = NewObject<CommonMethod>().GetCurInvoiceNO(InvoiceType.门诊收费, operatoreid);
                responseData.AddData(curInvoiceNO);//当前可用票据号

                //过滤药品数据
                List<Prescription> resultUser = prescriptions.FindAll(
                    delegate (Prescription user)
                    {
                        return user.StatID == 100 || user.StatID == 101 || user.StatID == 102;
                    });

                // 根据过滤出的药品数据 过滤出不同的药房
                if (resultUser.Count > 0)
                {
                    // 根据药房生成消息
                    IEnumerable<IGrouping<string, Prescription>> query = resultUser.GroupBy(x => x.ExecDetpName);
                    foreach (IGrouping<string, Prescription> info in query)
                    {
                        List<Prescription> sl = info.ToList<Prescription>();

                        // 生成消息
                        #region "保存业务消息数据 --Add By ZhangZhong"

                        // 保存业务消息数据
                        Dictionary<string, string> msgDic = new Dictionary<string, string>();
                        int workId = requestData.GetData<int>(4);
                        int userId = requestData.GetData<int>(5);
                        int deptId = requestData.GetData<int>(6);
                        msgDic.Add("WorkID", workId.ToString()); // 消息机构ID
                        msgDic.Add("SendUserId", userId.ToString()); // 消息生成人ID
                        msgDic.Add("SendDeptId", deptId.ToString()); // 消息生成科室ID
                        msgDic.Add("PatListID", curPatlist.PatListID.ToString()); // 病人ID
                        msgDic.Add("ExecDeptName", sl[0].ExecDetpName); // 病人ID
                        NewObject<BusinessMessage>().GenerateBizMessage(MessageType.门诊新处方, msgDic);
                        #endregion
                    }
                }

                //获取发票打印信息
                DataTable dtInvoice = NewDao<IOPManageDao>().GetBalancePrintInvoiceDt(budgeInfo[0].CostHeadID);//票据信息
                DataTable dtInvoiceDetail = NewDao<IOPManageDao>().GetBalancePrintDetailDt(budgeInfo[0].CostHeadID);//票据详细信息
                DataTable dtInvoiceStatDetail = NewDao<IOPManageDao>().GetBalancePrintStatDt(budgeInfo[0].CostHeadID);//项目信息
                responseData.AddData(dtInvoice);
                responseData.AddData(dtInvoiceDetail);
                responseData.AddData(dtInvoiceStatDetail);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 获取小票打印方式
        /// </summary>
        /// <returns>返回小票打印方式</returns>
        [WCFMethod]
        public ServiceResponseData GetBillPrintType()
        {
            try
            {
                string value = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.BillPrintType);
                responseData.AddData(Convert.ToInt32(value));
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 取消收银预算
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]       
        public ServiceResponseData AbortBudget()
        {
            try
            {
                List<ChargeInfo> budgeInfo = requestData.GetData<List<ChargeInfo>>(0);//金额预算信息
                string balanceType = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.BalanceType);
                OP_Enum.BalanceType chargeType = (OP_Enum.BalanceType)Convert.ToInt32(balanceType);
                BaseBalaceProcess balanceProcess = NewObject<BalanceFactory>().CreateChargeObject(chargeType);
                balanceProcess.AbortBudget(budgeInfo);
                responseData.AddData(true);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
        #endregion

        #region 处方退费
        /// <summary>
        /// 处方退费界面初始数据
        /// </summary>
        /// <returns>原收费结算信息</returns>
        [WCFMethod]
        public ServiceResponseData RefundInit()
        {
            try
            {
                int costHeadid = requestData.GetData<int>(0);
                OP_CostHead costHead = NewObject<OP_CostHead>().getmodel(costHeadid) as OP_CostHead;
                bool isMedicarePat = NewObject<CommonMethod>().IsMedicarePat(costHead.PatTypeID);
                responseData.AddData(isMedicarePat);
                List<OP_CostPayMentInfo> listPayInfo = NewObject<OP_CostPayMentInfo>().getlist<OP_CostPayMentInfo>(" costheadid=" + costHeadid);
                responseData.AddData(listPayInfo);
                responseData.AddData(costHead);
                int refundPosType = Convert.ToInt32(NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.RefundPosType));
                responseData.AddData(refundPosType);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 门诊退费
        /// </summary>
        /// <returns>是否全退，需补收的处方</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData RefundFee()
        {
            try
            {
                List<Prescription> prescriptions = requestData.GetData<List<Prescription>>(0);//退费处方
                int operatoreid = requestData.GetData<int>(1);//操作员ID
                int costheadId = requestData.GetData<int>(2);//结算ID
                string refundInvoiceNO = requestData.GetData<string>(3);//退费票据号
                string balanceType = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.BalanceType);
                OP_Enum.BalanceType chargeType = (OP_Enum.BalanceType)Convert.ToInt32(balanceType);
                BaseBalaceProcess balanceProcess = NewObject<BalanceFactory>().CreateChargeObject(chargeType);
                List<Prescription> balancePresc = balanceProcess.RefundFee(costheadId, operatoreid, prescriptions, refundInvoiceNO);
                bool isAllRefund = true;
                if (balancePresc.Count > 0)
                {
                    isAllRefund = false;
                }

                responseData.AddData(isAllRefund);//是否全退
                responseData.AddData(balancePresc);//需要补收的处方
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
        #endregion
    }
}
