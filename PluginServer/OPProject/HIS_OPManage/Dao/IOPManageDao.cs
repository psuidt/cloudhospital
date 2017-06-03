using System;
using System.Collections.Generic;
using System.Data;
using HIS_Entity.OPManage;

namespace HIS_OPManage.Dao
{
    /// <summary>
    /// 门诊数据库Dao接口
    /// </summary>
    public interface IOPManageDao
    {
        #region 票据管理

        /// <summary>
        /// 获取所有票据信息
        /// </summary>
        /// <returns>DataTable</returns>
        DataTable GetInvoices();

        /// <summary>
        /// 获取门诊票据
        /// </summary>
        /// <param name="perfChar">票据前缀</param>
        /// <param name="startNo">开始号码</param>
        /// <param name="endNo">结束号码</param>
        /// <param name="totalMoney">票据总金额</param>
        /// <param name="count">票据使用张数</param>
        /// <param name="refundMoney">退费金额</param>
        /// <param name="refundCount">退费张数</param>
        void GetInvoiceListInfo(string perfChar, string startNo, string endNo, out decimal totalMoney, out int count, out decimal refundMoney, out int refundCount);
        #endregion

        #region 挂号基础数据维护

        /// <summary>
        /// 获取挂号类型对应的收费项目明细
        /// </summary>
        /// <param name="regtypeid">挂号类别ID</param>
        /// <returns>DataTable</returns>
        DataTable GetRegItemFees(int regtypeid);
        #endregion

        #region 医生排班

        /// <summary>
        /// 获取指定日期的排班信息
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="deptid">科室ID</param>
        /// <param name="docid">医生ID</param>
        /// <returns>DataTable指定日期的排班信息</returns>
        DataTable GetSchedualOneDate(DateTime bdate,DateTime edate, int deptid,int docid);

        /// <summary>
        /// 获取之前最近一次排班日期
        /// </summary>
        /// <param name="deptid">科室ID</param>
        /// <param name="docid">医生ID</param>
        /// <returns>object</returns>
        object GetMaxSchedualDate(int deptid, int docid);

        /// <summary>
        /// 复制排班时，删除原有排班信息
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="deptid">科室ID</param>
        /// <param name="docid">医生ID</param>
        void DeleteOldCopySchedual(DateTime bdate,DateTime edate,int deptid,int docid);
        #endregion
        #region 挂号
        /// <summary>
        /// 根据日期和时间段获取排班科室
        /// </summary>
        /// <param name="date">排班日期</param>    
        ///  <param name="timerange">时间段 1上午 2下午 3晚上</param>
        /// <returns>DataTable</returns>
        DataTable GetScheualDept(DateTime date, int timerange );

        /// <summary>
        /// 根据日期和时间段获取排班医生
        /// </summary>
        /// <param name="date">排班日期</param>
        /// <param name="timerange">时间段 1上午 2下午 3晚上</param>
        /// <returns>DataTable</returns>
        DataTable GetSchedualDoctor(DateTime date, int timerange);

        /// <summary>
        /// 查询会员信息
        /// </summary>
        /// <param name="nameContent">姓名</param>
        /// <param name="telContent">电话号码</param>
        /// <param name="idContent">身份证号</param>
        /// <param name="memberid">会员ID</param>
        /// <returns>DataTable</returns>
        DataTable GetMemberInfoByOther(string nameContent, string telContent, string idContent, int memberid);

        /// <summary>
        /// 获取操作员当天的挂号数据
        /// </summary>
        /// <param name="operatorID">操作员ID</param>
        /// <param name="date">日期</param>
        /// <returns>DataTable</returns>
        DataTable GetRegInfoByOperator(int operatorID, DateTime date);

        /// <summary>
        /// 获取挂号总金额
        /// </summary>
        /// <param name="regTypeid">挂号类别ID</param>
        /// <returns>decimal总金额</returns>
        decimal GetRegtotalFee(int regTypeid);

        /// <summary>
        /// 通过挂号类别获取按大项目分类的金额
        /// </summary>
        /// <param name="regTypeid">挂号类别ID</param>
        /// <returns>DataTable</returns>    
        DataTable GetRegItemFeeByStat(int regTypeid);

        /// <summary>
        /// 获取挂号打印数据
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <returns>DataTable 挂号打印数据</returns>
        DataTable GetRegPrint(int patlistid);

        /// <summary>
        /// 查找会员信息
        /// </summary>
        /// <param name="queryContent">查找条件</param>
        /// <returns>DataTable会员信息</returns>
        DataTable GetMemberInfoByQueryConte(string queryContent);
        #endregion

        #region 收费

        /// <summary>
        /// 通过指定查询类别和内容获取挂号病人信息
        /// </summary>
        /// <param name="queryType">查询类别</param>
        /// <param name="content">查询内容</param>
        /// <returns>DataTable挂号病人信息</returns>
        DataTable GetRegPatList(HIS_Entity.OPManage.OP_Enum.MemberQueryType queryType, string content);

        /// <summary>
        /// 通过组合内容获取挂号病人信息
        /// </summary>
        /// <param name="nameContent">姓名</param>
        /// <param name="telContent">电话号码</param>
        /// <param name="idContent">身份证号</param>
        /// <param name="idContent">医保卡号</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <returns>DataTable挂号病人信息</returns>
        DataTable GetRegPatListByOther(string nameContent, string telContent, string idContent,string mediCard,DateTime bdate,DateTime edate);

        /// <summary>
        /// 正式结算时通过预结算ID修改费用主表状态,发票号，收费时间
        /// </summary>
        /// <param name="costheadid">结算ID</param>
        /// <param name="invoiceNO">票据号</param>
        /// <param name="chareDate">收费日期</param>
        /// <param name="chargeEmpid">收费人员ID</param>
        void UpdateFeeItemHeadStatus(int costheadid,string invoiceNO, DateTime chareDate, int chargeEmpid);

        /// <summary>
        /// 收费完成后获取发票信息
        /// </summary>
        /// <param name="costHeadId">结算ID</param>
        /// <returns>DataTable发票信息</returns>
        DataTable GetBalancePrintInvoiceDt(int costHeadId);

        /// <summary>
        /// 获取一次收费的收费明细
        /// </summary>
        /// <param name="costHeadId">结算ID</param>
        /// <returns>DataTable</returns>    
        DataTable GetBalancePrintDetailDt(int costHeadId);

        /// <summary>
        /// 根据结算ID获取病人收费明细按大项目分类
        /// </summary>
        /// <param name="costHeadId">结算ID</param>
        /// <returns>DataTable</returns>
        DataTable GetBalancePrintStatDt(int costHeadId);

        /// <summary>
        /// 获取处方信息
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>DataTable</returns>
        DataTable GetPrescription(string strWhere);
        #endregion
        #region  退费     

        /// <summary>
        /// 通过票据号获取票号对应处方
        /// </summary>
        /// <param name="invoiceNO">发票号</param>
        /// <param name="operatorid">操作员ID</param>
        /// <returns>DataTable票号对应处方</returns>
        DataTable GetInvoicePresc(string invoiceNO,int operatorid);

        /// <summary>
        /// 查询退费消息
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="querycontion">查询条件</param>
        /// <param name="operatorid">操作人员ID</param>
        /// <returns>DataTable退费消息</returns>
        DataTable QueryRefundMessage(DateTime bdate, DateTime edate, string querycontion,int operatorid);

        /// <summary>
        /// 退费时获取红冲的FeeItemHeadId
        /// </summary>
        /// <param name="oldFeeitemHeadId">原对应的红冲费用ID</param>
        /// <returns>获取红冲的FeeItemHeadId</returns>
        int GetNewFeeItemHeadId(int oldFeeitemHeadId);

        /// <summary>
        /// 获取医生站处方
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <returns>DataTable医生站处方</returns>
        DataTable GetDocPrescription(int patlistid);

        /// <summary>
        /// 获取组合项目明细信息
        /// </summary>
        /// <param name="examItemID">组合项目ID</param>
        /// <returns>DataTable</returns>
        DataTable GetExamItemDetailDt(int examItemID);
        #endregion

        #region 门诊缴款

        /// <summary>
        /// 获取交款表信息ByEmpId
        /// </summary>
        /// <param name="iEmpId">用户ID</param>
        /// <param name="iState">交款状态 0 未交 1已交</param>
        /// <param name="bdate">已交的开始时间</param>
        /// <param name="edate">已交的结束时间</param>
        /// <returns>交款表信息</returns>
        List<OP_Account> GetAccountByEmp(int iEmpId, int iState, DateTime bdate, DateTime edate);

        /// <summary>
        /// 获取票据数据
        /// </summary>
        /// <param name="accountid">交款ID</param>
        /// <returns>DataTable</returns>
        DataTable GetAccountInvoiceData(int accountid);

        /// <summary>
        /// 获取支付方式数据
        /// </summary>
        /// <param name="accountid">交款ID</param>
        /// <returns>DataTable</returns>
        DataTable GetAccountPayMent(int accountid);

        /// <summary>
        /// 获取项目分类数据
        /// </summary>
        /// <param name="accountid">交款ID</param>
        /// <returns>DataTable</returns>
        DataTable GetAccountItemData(int accountid);

        /// <summary>
        /// 获取挂号费及收费金额
        /// </summary>
        /// <param name="accountid">交款ID</param>
        /// <returns>DataTable</returns>
        DataTable GetRegBalanceFee(int accountid);

        /// <summary>
        /// 获取票据明细信息
        /// </summary>
        /// <param name="regFlag">0收费1挂号</param>
        /// <param name="invoiceType">0正常1退票</param>
        /// <param name="accountid">缴款ID</param>
        /// <returns>DataTable</returns>
        DataTable GetInvoiceDetail(int regFlag, int invoiceType, int accountid);

        /// <summary>
        /// 获取最大缴款单号
        /// </summary>
        /// <returns>缴款单号</returns>
        int GetMaxJkdh();

        /// <summary>
        /// 查询所有已经缴款记录
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="empid">收费员ID</param>
        /// <param name="status">状态0未收款1已经收款</param>
        /// <returns>DataTable</returns>
        DataTable GetAllAccountData(DateTime bdate, DateTime edate, int empid,int status);

        /// <summary>
        /// 查询所有未缴款记录
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="empid">收费员ID</param>
        /// <returns>DataTable</returns>
        DataTable GetAllNotAccountData(DateTime bdate, DateTime edate, int empid);

        /// <summary>
        /// 查询已缴款支付信息
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="empid">人员ID</param>
        /// <returns>DataTable支付信息</returns>
        DataTable GetAllAccountPayment(DateTime bdate, DateTime edate, int empid);

        /// <summary>
        /// 查询未缴款支付信息
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="empid">人员ID</param>
        /// <returns>DataTable</returns>
        DataTable GetAllNotAccountPayment(DateTime bdate, DateTime edate, int empid);

        /// <summary>
        /// 获取会员充值明细数据
        /// </summary>
        /// <param name="iAcountId">结账ID</param>
        /// <returns>DataTable会员充值明细</returns>
        DataTable GetAccountDataME(int iAcountId);

        /// <summary>
        /// 获取会员未交款金额汇总
        /// </summary>
        /// <param name="empid">人员ID</param>
        /// <returns>DataTable</returns>
        DataTable GetAccountDataMETotal(int empid);

        /// <summary>
        /// 更新会员交易记录表
        /// </summary>
        /// <param name="iAccountID">缴款ID</param>
        /// <returns>bool true更新成功 false 失败</returns>
        bool UpdateAccountME(int iAccountID);

        #endregion

        #region 处方查询

        /// <summary>
        /// 处方查询
        /// </summary>
        /// <param name="queryDictionary">查询条件</param>
        /// <returns>DataTable</returns>
        DataTable ReciptQuery(Dictionary<string, object> queryDictionary);
        #endregion
        #region 门诊病人费用查询

        /// <summary>
        ///  查询门诊病人按支付方式查询
        /// </summary>
        /// <param name="queryDictionary">查询条件</param>
        /// <returns>DataTable</returns>
        DataTable GetOPCostPayMentQuery(Dictionary<string, object> queryDictionary);

        /// <summary>
        /// 查询门诊病人费用按项目分类
        /// </summary>
        /// <param name="queryDictionary">查询条件</param>
        /// <returns>DataTable</returns>
        DataTable GetOPCostFpItemQuery(Dictionary<string, object> queryDictionary);

        /// <summary>
        /// 获取病人费用明细
        /// </summary>
        /// <param name="costHeadid">病人结算ID</param>
        /// <returns>DataTable</returns>
        DataTable GetCostDetail(int costHeadid);
        #endregion
        #region 医生站处方状态修改

        /// <summary>
        /// 医生站处方状态修改
        /// </summary>
        /// <param name="docPresHeadID">医生站处方头表ID</param>
        /// <param name="docPresNO">处方号</param>
        /// <param name="status">状态</param>
        void UpdateDocPresStatus(int docPresHeadID,List<int> docPresNO,int status);        
        #endregion
    }
}
