using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.BasicData;
using HIS_Entity.MemberManage;
using HIS_Entity.OPManage;
using HIS_OPManage.Dao;
using HIS_OPManage.ObjectModel;
using HIS_PublicManage.ObjectModel;

namespace HIS_OPManage.WcfController
{
    /// <summary>
    /// 挂号控制器类
    /// </summary>
    [WCFController]
    public class RegisterController : WcfServerController
    {
        /// <summary>
        /// 挂号界面数据初始化
        /// </summary>
        /// <returns>挂号类别 卡类型 病人类型</returns>
        [WCFMethod]
        public ServiceResponseData RegDataInit()
        {
            int operatorID = requestData.GetData<int>(0);
            BasicDataManagement basicmanagement = NewObject<BasicDataManagement>();
            DataTable dtRegType = NewObject<CommonMethod>().GetRegType();
            responseData.AddData(dtRegType);//挂号类别

            DataTable dtCardType = NewObject<ME_CardTypeList>().gettable(" UseFlag=1");
            responseData.AddData(dtCardType);//卡类型

            DataTable dtPattype = basicmanagement.GetPatType(false);
            responseData.AddData(dtPattype);//病人类型

            string regMoningBTime = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.RegMoningBTime);
            string regAfternoonBtime = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.RegAfternoonBtime);
            string regNightBtime = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.RegNightBtime);

            responseData.AddData(regMoningBTime);
            responseData.AddData(regAfternoonBtime);
            responseData.AddData(regNightBtime);

            string curInvoiceNO = string.Empty;
            int invoiceCount = NewObject<CommonMethod>().GetInvoiceInfo(InvoiceType.门诊挂号, operatorID, out curInvoiceNO);
            responseData.AddData(invoiceCount);//可用票据张数
            responseData.AddData(curInvoiceNO);//当前可用票据号
            return responseData;
        }

        /// <summary>
        /// 获取选择时间段的排班数据
        /// </summary>
        /// <returns>排班数据</returns>
        [WCFMethod]
        public ServiceResponseData SelectSchedual()
        {
            int timerange = requestData.GetData<int>(0);
            DataTable dtDept = NewObject<IOPManageDao>().GetScheualDept(DateTime.Now, timerange);
            DataTable dtDoctor = NewObject<IOPManageDao>().GetSchedualDoctor(DateTime.Now, timerange);

            responseData.AddData(dtDept);
            responseData.AddData(dtDoctor);
            return responseData;
        }

        /// <summary>
        /// 通过卡号获取会员基本信息
        /// </summary>
        /// <returns>会员基本信息</returns>
        [WCFMethod]
        public ServiceResponseData QueryMemberInfo()
        {
            int queryCardtype = (int)requestData.GetData<OP_Enum.MemberQueryType>(0);
            string cardno = requestData.GetData<string>(1);
            if (queryCardtype == (int)OP_Enum.MemberQueryType.会员ID)
            {
                DataTable dtPatinfo = NewObject<IOPManageDao>().GetMemberInfoByOther(string.Empty, string.Empty, string.Empty, Convert.ToInt32(cardno));
                responseData.AddData(dtPatinfo);
                return responseData;
            }
            else
            {
                MemberManagement membermanager = NewObject<MemberManagement>();
                DataTable dtPatInfo = membermanager.QueryMemberInfo(queryCardtype, cardno);//卡号
                responseData.AddData(dtPatInfo);
                return responseData;
            }
        }

        /// <summary>
        /// 通过姓名，电话号码，身份证号组合条件或取会员信息
        /// </summary>
        /// <returns>获取会员信息</returns>
        [WCFMethod]
        public ServiceResponseData GetMemberInfoByOther()
        {    
            string queryContent= requestData.GetData<string>(0);   
            DataTable dtPatinfo = NewObject<IOPManageDao>().GetMemberInfoByQueryConte(queryContent);
            responseData.AddData(dtPatinfo);
            return responseData;
        }

        /// <summary>
        /// 获取操作员当天的挂号记录
        /// </summary>
        /// <returns>操作员当天的挂号记录</returns>
        [WCFMethod]
        public ServiceResponseData GetRegInfoByOperator()
        {
            int operatorID = requestData.GetData<int>(0);
            DataTable dtRegInfo = NewObject<IOPManageDao>().GetRegInfoByOperator(operatorID, DateTime.Now);
            responseData.AddData(dtRegInfo);
            return responseData;
        }

        /// <summary>
        /// 获取挂号支付方式
        /// </summary>
        /// <returns>ResponseData </returns>
        [WCFMethod]
        public ServiceResponseData GetRegPayMentType()
        {
            DataTable dtPayMentInfo = NewObject<RegisterProcess>().GetRegPayMentType();
            responseData.AddData(dtPayMentInfo);//挂号支付方式
            return responseData;
        }

        /// <summary>
        /// 挂号支付界面初始化
        /// </summary>
        /// <returns>ResponseData</returns>
        [WCFMethod]
        public ServiceResponseData RegPayMentInit()
        {
            int operatorID = requestData.GetData<int>(0);
            int regTypeid = requestData.GetData<int>(1);
            int pattypeid = requestData.GetData<int>(2);
            string curInvoiceNO = NewObject<CommonMethod>().GetCurInvoiceNO(InvoiceType.门诊挂号,operatorID);
            responseData.AddData(curInvoiceNO);//当前可用票据号
            decimal totalRegFee = NewObject<RegisterProcess>().GetRegTotalFee(regTypeid);//挂号总金额
            responseData.AddData(totalRegFee);
            DataTable dtPayMentInfo = NewObject<RegisterProcess>().GetRegPayMentType();//挂号支付方式
            responseData.AddData(dtPayMentInfo);

            //判断所选的挂号病人类型是否是医保病人
            bool isMedicarePat = NewObject<CommonMethod>().IsMedicarePat(pattypeid);
            responseData.AddData(isMedicarePat);
            return responseData;
        }

        /// <summary>
        /// 挂号结算
        /// </summary>
        /// <returns>ServiceResponse</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveRegister()
        {
            try
            {
                OP_PatList curPatlist = requestData.GetData<OP_PatList>(0);
                decimal totalFee = requestData.GetData<decimal>(1);//挂号总金额
                string paymentCode = requestData.GetData<string>(2);//支付方式Code
                decimal medicareFee = requestData.GetData<decimal>(3);//医保统筹支付金额
                decimal payFee = requestData.GetData<decimal>(4);//应付金额
                decimal promFee = requestData.GetData<decimal>(5);//优惠金额
                decimal medicarePersFee = requestData.GetData<decimal>(6);//医保个账支付金额
                if (string.IsNullOrEmpty( curPatlist.PatName))
                {
                    throw new Exception("请先输入病人信息");
                }

                PayMentInfoList payinfoList = new PayMentInfoList();
                List<OP_CostPayMentInfo> paymentInfoList = new List<OP_CostPayMentInfo>();
                if (medicareFee > 0)
                {
                    string medicarePayMentCode = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.RegMedicareCode);//获取医保对应的支付方式Code                  
                    OP_CostPayMentInfo paymentinfo = new OP_CostPayMentInfo();
                    paymentinfo.PayMentCode = medicarePayMentCode;
                    paymentinfo.PayMentMoney = medicareFee;
                    Basic_Payment basicPayment = NewObject<CommonMethod>().GetPayMentByCode(paymentinfo.PayMentCode);
                    paymentinfo.PayMentID = basicPayment.PaymentID;
                    paymentinfo.PayMentName = basicPayment.PayName;
                    paymentInfoList.Add(paymentinfo);
                }

                if (medicarePersFee > 0)
                {
                    OP_CostPayMentInfo paymentinfo = new OP_CostPayMentInfo();
                    paymentinfo.PayMentCode = "04";
                    paymentinfo.PayMentMoney = medicarePersFee;
                    Basic_Payment basicPayment = NewObject<CommonMethod>().GetPayMentByCode(paymentinfo.PayMentCode);
                    paymentinfo.PayMentID = basicPayment.PaymentID;
                    paymentinfo.PayMentName = basicPayment.PayName;
                    paymentInfoList.Add(paymentinfo);
                }

                if (promFee > 0)
                {
                    OP_CostPayMentInfo paymentinfo = new OP_CostPayMentInfo();
                    paymentinfo.PayMentCode = "03";
                    paymentinfo.PayMentMoney = promFee;
                    Basic_Payment basicPayment = NewObject<CommonMethod>().GetPayMentByCode(paymentinfo.PayMentCode);
                    paymentinfo.PayMentID = basicPayment.PaymentID;
                    paymentinfo.PayMentName = basicPayment.PayName;
                    paymentInfoList.Add(paymentinfo);
                }

                decimal cashFee = 0;
                decimal posFee = 0;
                if (payFee > 0)
                {
                    OP_CostPayMentInfo paymentinfo = new OP_CostPayMentInfo();
                    paymentinfo.PayMentCode = paymentCode;
                    paymentinfo.PayMentMoney = payFee;
                    Basic_Payment basicPayment = NewObject<CommonMethod>().GetPayMentByCode(paymentinfo.PayMentCode);
                    paymentinfo.PayMentID = basicPayment.PaymentID;
                    paymentinfo.PayMentName = basicPayment.PayName;
                    paymentInfoList.Add(paymentinfo);

                    //现金
                    if (paymentCode == "01")
                    {
                        cashFee += payFee;
                    }

                    //POS
                    if (paymentCode == "02")
                    {
                        posFee += posFee;
                    }
                }

                payinfoList.paymentInfolist = paymentInfoList;
                DataTable dtPrint = new DataTable();
                NewObject<RegisterProcess>().SaveRegInfo(curPatlist, payinfoList, totalFee, cashFee, posFee, out dtPrint,promFee);
                responseData.AddData(dtPrint);//打印信息
                responseData.AddData(curPatlist);//病人对象
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 通过输入票据号判断是否可以退号
        /// </summary>
        /// <returns>是否可退号，原支付信息，是否有医保支付，原病人对象</returns>
        [WCFMethod]
        public ServiceResponseData GetRegInfoByInvoiceNO()
        {
            try
            {
                string invoiceNO = requestData.GetData<string>(0);//票据号
                PayMentInfoList payInfolist = new PayMentInfoList();
                OP_PatList regPatList = new OP_PatList();
                bool result = NewObject<RegisterProcess>().GetRegInfoByInvoiceNO(invoiceNO, out payInfolist,out regPatList);
                bool isMedicarePay = false;               
                string medicarePayMentCode = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.RegMedicareCode);//获取医保对应的支付方式Code          
                for (int index = 0; index < payInfolist.paymentInfolist.Count; index++)
                {
                    //有医保支付的
                    if (payInfolist.paymentInfolist[index].PayMentCode == medicarePayMentCode)
                    {
                        isMedicarePay = true; 
                    }
                }

                responseData.AddData(result);//是否可退号
                responseData.AddData(payInfolist);//原支付信息
                responseData.AddData(isMedicarePay);//是否有医保支付
                responseData.AddData(regPatList);//原病人对象
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 退号提交
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData BackReg()
        {
            try
            {
                string invoiceNO = requestData.GetData<string>(0);//票据号         
                int operatorID = requestData.GetData<int>(1);//操作员ID
                NewObject<RegisterProcess>().BackReg(invoiceNO, operatorID);
                responseData.AddData(true);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 通过挂号ID获取挂号费用信息
        /// </summary>
        /// <returns>挂号费用信息</returns>
        [WCFMethod]       
        public ServiceResponseData GetRegInfoByRegId()
        {
            try
            {
                int retTypeID = requestData.GetData<int>(0);//挂号类别   
                DataTable dt= NewDao<IOPManageDao>().GetRegItemFees(retTypeID);
                responseData.AddData(dt);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
} 