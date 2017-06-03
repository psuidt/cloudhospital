using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.ClinicManage;
using HIS_Entity.OPManage;
using HIS_OPManage.Dao;

namespace HIS_OPManage.WcfController
{
    /// <summary>
    /// 退费处理控制器类
    /// </summary>
    [WCFController]
    public  class RefundController: WcfServerController
    {
        #region 通过票据号查询门诊收费处方
        /// <summary>
        /// 通过票据号查询门诊收费处方
        /// </summary>
        /// <returns>返回门诊收费处方</returns>
        [WCFMethod]
        public ServiceResponseData QueryPresByInvoiceNO()
        {
            string  invoiceNO = requestData.GetData<string>(0);
            int operatoreid= requestData.GetData<int>(1);
            List<OP_FeeRefundHead> refunds = NewObject<OP_FeeRefundHead>().getlist<OP_FeeRefundHead>(" InvoiceNum='"+invoiceNO+ "' and RefundPayFlag=0 and flag=0");
            if (refunds.Count > 0)
            {
                throw new Exception("该发票号存在未退费的退药消息,不能再次产生退药消息");
            }

            DataTable dtPresc = NewDao<IOPManageDao>().GetInvoicePresc(invoiceNO,operatoreid);
            if (dtPresc.Rows.Count > 0)
            {
                //if (Convert.ToInt32(dtPresc.Rows[0]["PresEmpID"]) != operatoreid)
                //{
                //    throw new Exception("只能查看自己开的处方");
                //}
                //if (Convert.ToDateTime(dtPresc.Rows[0]["ChargeDate"]).ToString("yyyy-MM-dd") != DateTime.Now.ToString("yyyy-MM-dd"))
                //{
                //    throw new Exception("该发票号收费日期不是当天，不能退费");
                //}
            }

            responseData.AddData(dtPresc);
            return responseData;
        }
        #endregion

        #region 保存退费消息
        /// <summary>
        /// 保存退费消息
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveRefundMessage()
        {
            try
            {
                DataTable dtPresc = requestData.GetData<DataTable>(0);
                DataTable dtMedical = requestData.GetData<DataTable>(1);
                int operatoreid = requestData.GetData<int>(2);
                OP_FeeRefundHead refundHead = new OP_FeeRefundHead();
                refundHead.RefundDocID = operatoreid;
                refundHead.RefundDate = DateTime.Now;                
                refundHead.RefundPayFlag = 0;
                refundHead.Flag = 0;
                refundHead.PatListID = dtPresc==null || dtPresc.Rows.Count==0 ? Convert.ToInt32(dtMedical.Rows[0]["patlistid"]):  Convert.ToInt32(dtPresc.Rows[0]["patlistid"]);
                refundHead.PatName = dtPresc == null || dtPresc.Rows.Count == 0 ? dtMedical.Rows[0]["patname"].ToString(): dtPresc.Rows[0]["patname"].ToString();
                refundHead.InvoiceNum = dtPresc == null || dtPresc.Rows.Count == 0 ? dtMedical.Rows[0]["invoiceNO"].ToString():dtPresc.Rows[0]["invoiceNO"].ToString();
                this.BindDb(refundHead);
                refundHead.save();

                if (dtPresc != null)
                {
                    for (int i = 0; i < dtPresc.Rows.Count; i++)
                    {
                        if (dtPresc.Rows[i]["ItemName"].ToString().Trim() == "小   计")
                        {
                            continue;
                        }

                        OP_FeeRefundDetail refundDetail = new OP_FeeRefundDetail();
                        refundDetail.ReHeadID = refundHead.ReHeadID;
                        refundDetail.FeeItemHeadID = Convert.ToInt32(dtPresc.Rows[i]["FeeItemHeadID"]);
                        OP_FeeItemHead opfeeitemhead = NewObject<OP_FeeItemHead>().getmodel(refundDetail.FeeItemHeadID) as OP_FeeItemHead;
                        if (opfeeitemhead.ChargeStatus != 0 && opfeeitemhead.ChargeFlag != 1 && opfeeitemhead.DistributeFlag != Convert.ToInt32(dtPresc.Rows[i]["DistributeFlag"]))
                        {
                            throw new Exception("该处方没有正常的收费记录");
                        }

                        refundDetail.DistributeFlag = opfeeitemhead.DistributeFlag;//取数据库最新记录状态，防止并发状态
                        refundDetail.RefundFlag = 0;
                        refundDetail.FeeItemDetailID = Convert.ToInt32(dtPresc.Rows[i]["PresDetailID"]);
                        refundDetail.ItemID = Convert.ToInt32(dtPresc.Rows[i]["ItemID"]);
                        refundDetail.ItemName = dtPresc.Rows[i]["ItemName"].ToString();
                        refundDetail.OldAmount = Convert.ToDecimal(dtPresc.Rows[i]["Amount"]);

                        decimal refundminimun = Convert.ToDecimal(dtPresc.Rows[i]["RefundMiniNum"]);
                        decimal refundpacknum = Convert.ToDecimal(dtPresc.Rows[i]["RefundPackNum"]);
                        decimal refundpresamount = Convert.ToDecimal(dtPresc.Rows[i]["refundpresamount"]);
                        decimal unitNO = Convert.ToDecimal(dtPresc.Rows[i]["UnitNO"]);
                        decimal refundamount = ((refundpacknum * unitNO) + refundminimun) * refundpresamount;
                        refundDetail.RefundAmount = refundamount;
                        refundDetail.NewAmount = refundDetail.OldAmount - refundDetail.RefundAmount;
                        refundDetail.RefundFee = Convert.ToDecimal(dtPresc.Rows[i]["RefundFee"]);
                        refundDetail.RefundPresAmount = refundpresamount;
                        this.BindDb(refundDetail);
                        refundDetail.save();
                    }
                }

                if (dtMedical != null)
                {
                    //根据组合项目生成明细
                    for (int i = 0; i < dtMedical.Rows.Count; i++)
                    {
                        int feeItemHeadID = Convert.ToInt32(dtMedical.Rows[i]["FeeItemHeadID"]);
                        OP_FeeItemHead opfeeitemhead = NewObject<OP_FeeItemHead>().getmodel(feeItemHeadID) as OP_FeeItemHead;
                        if (opfeeitemhead.ChargeStatus != 0 && opfeeitemhead.ChargeFlag != 1 && opfeeitemhead.DistributeFlag != Convert.ToInt32(dtMedical.Rows[i]["DistributeFlag"]))
                        {
                            throw new Exception("该处方没有正常的收费记录");
                        }

                        List<OP_FeeItemDetail> list = NewObject<OP_FeeItemDetail>().getlist<OP_FeeItemDetail>(" FeeItemHeadID=" + feeItemHeadID + " and ExamItemID=" + dtMedical.Rows[i]["ExamItemID"] + " ");
                        foreach (OP_FeeItemDetail detail in list)
                        {
                            OP_FeeRefundDetail refundDetail = new OP_FeeRefundDetail();
                            refundDetail.ReHeadID = refundHead.ReHeadID;
                            refundDetail.FeeItemHeadID = feeItemHeadID;
                            refundDetail.DistributeFlag = opfeeitemhead.DistributeFlag;//取数据库最新记录状态，防止并发状态
                            refundDetail.RefundFlag = 0;
                            refundDetail.FeeItemDetailID = detail.PresDetailID;
                            refundDetail.ItemID = detail.ItemID;
                            refundDetail.ItemName = detail.ItemName;
                            refundDetail.OldAmount = detail.Amount;
                            if (dtMedical.Rows[i]["Sel"] != DBNull.Value
                                && Convert.ToInt32(dtMedical.Rows[i]["Sel"]) == 1)
                            {
                                refundDetail.RefundAmount = detail.Amount;
                            }
                            else
                            {
                                refundDetail.RefundAmount = 0;
                            }

                            refundDetail.NewAmount = refundDetail.OldAmount - refundDetail.RefundAmount;
                            refundDetail.RefundFee = detail.RetailPrice * refundDetail.RefundAmount;
                            refundDetail.RefundPresAmount = detail.PresAmount;
                            this.BindDb(refundDetail);
                            refundDetail.save();
                        }
                    }
                }

                responseData.AddData(true);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
        #endregion

        /// <summary>
        /// 查询退费消息
        /// </summary>
        /// <returns>返回退费消息</returns>
        [WCFMethod]
        public ServiceResponseData QueryRefundMessage()
        {
            try
            {
                DateTime bdate = requestData.GetData<DateTime>(0);
                DateTime edate = requestData.GetData<DateTime>(1);
                string queryCondition = requestData.GetData<string>(2);
                int operatorid = requestData.GetData<int>(3);
                DataTable dtPresc = NewDao<IOPManageDao>().QueryRefundMessage(bdate, edate, queryCondition, operatorid);
                responseData.AddData(dtPresc);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 删除退费消息
        /// </summary>
        /// <returns>true</returns>        
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData DeleteRefundMessage()
        {
            try
            {
                string  invoiceNum = requestData.GetData<string>(0);                
                int operatorid = requestData.GetData<int>(1);
                List<OP_FeeRefundHead> refundheadList = NewObject<OP_FeeRefundHead>().getlist<OP_FeeRefundHead>(" invoicenum='"+invoiceNum+"' and flag=0");
                if (refundheadList == null || refundheadList.Count == 0)
                {
                    throw new Exception("没有找到该票据号对应退费消息");
                }

                OP_FeeRefundHead refundHead = refundheadList[0] as OP_FeeRefundHead;
                if (refundHead.RefundPayFlag == 1)
                {
                    throw new Exception("该票号已经退费完成，不能删除退费消息");
                }

                List<OP_FeeRefundDetail> refundDetailList = NewObject<OP_FeeRefundDetail>().getlist<OP_FeeRefundDetail>(" ReHeadID="+ refundHead.ReHeadID+string.Empty);
                foreach (OP_FeeRefundDetail feerefundDetail in refundDetailList)
                {
                    if (feerefundDetail.RefundFlag == 1)
                    {
                        throw new Exception("该票号有药品已经退药，不能删除退费消息");                      
                    }

                    feerefundDetail.delete();
                } 
                 
                this.BindDb(refundHead);
                refundHead.delete();
                responseData.AddData(true);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 通过门诊医生处方头和处方号获取该处方票据号
        /// </summary>
        /// <returns>返回票据号</returns>
        [WCFMethod]
        public ServiceResponseData GetInvoiceNOByPres()
        {
            try
            {
                int presHeadID = requestData.GetData<int>(0);//门诊医生处方ID
                int presNO = requestData.GetData<int>(1);//门诊医生处方号
                List<OP_FeeItemHead> list = NewObject<OP_FeeItemHead>().getlist<OP_FeeItemHead>(" DocPresHeadID="+presHeadID+" and DocPresNO="+presNO+" and ChargeFlag=1 and ChargeStatus=0 and RegFlag=0");
                if (list.Count == 0)
                {
                    throw new Exception("该处方没有正常的收费记录，不能退费");
                }

                responseData.AddData(list[0].InvoiceNO);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 获取医技项目可以退费的票据号
        /// </summary>
        /// <returns>票据号</returns>
        [WCFMethod]
        public ServiceResponseData GetInvoiceNOExaApplyHeadID()
        {
            try
            {
                int applyHeadID = requestData.GetData<int>(0);//医技申请头ID
                List<EXA_MedicalApplyDetail> applyDetails = NewObject<EXA_MedicalApplyDetail>().getlist<EXA_MedicalApplyDetail>(" ApplyHeadID="+applyHeadID+ " and  (ApplyStatus=1 or ApplyStatus=2) and IsReturns=0");
                if (applyDetails.Count == 0)
                {
                    throw new Exception("该处方没有正常的收费记录，不能退费");
                }

                int presDetailId =Convert.ToInt32( applyDetails[0].PresDetailID);
                OPD_PresDetail presDetail = NewObject<OPD_PresDetail>().getmodel(presDetailId) as OPD_PresDetail;
                if (presDetail == null)
                {
                    throw new Exception("该处方没有正常的收费记录，不能退费");
                }

                int presHeadID = presDetail.PresHeadID;//门诊医生处方ID
                int presNO = presDetail.PresNO;//门诊医生处方号
                List<OP_FeeItemHead> list = NewObject<OP_FeeItemHead>().getlist<OP_FeeItemHead>(" DocPresHeadID=" + presHeadID + " and DocPresNO=" + presNO + " and ChargeFlag=1 and ChargeStatus=0 and RegFlag=0");
                if (list.Count == 0)
                {
                    throw new Exception("该处方没有正常的收费记录，不能退费");
                }

                responseData.AddData(list[0].InvoiceNO);
                return responseData;               
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
