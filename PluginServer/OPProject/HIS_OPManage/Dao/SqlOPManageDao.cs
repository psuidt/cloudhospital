using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.OPManage;

namespace HIS_OPManage.Dao
{
    /// <summary>
    /// 数据库接口实现类
    /// </summary>
    public class SqlOPManageDao : AbstractDao, IOPManageDao
    {
        #region 票据管理
        /// <summary>
        /// 获取票据使用信息
        /// </summary>
        /// <param name="perfChar">前缀</param>
        /// <param name="startNo">开始号码</param>
        /// <param name="endNo">结束号码</param>
        /// <param name="totalMoney">返回总金额</param>
        /// <param name="count">返回票据张数</param>
        /// <param name="refundMoney">返回退费金额</param>
        /// <param name="refundCount">返回退费张数</param>
        public void GetInvoiceListInfo(string perfChar, string startNo, string endNo, out decimal totalMoney, out int count, out decimal refundMoney, out int refundCount)
        {
            totalMoney = 0;
            count = 0;
            refundMoney = 0;
            refundCount = 0;
            try
            {
                int blength = perfChar.Length + 1;

                string sql = @"select EndInvoiceNO,CostStatus,TotalFee
                            from
                            (
                                select EndInvoiceNO,CostStatus,TotalFee from OP_CostHead where CostStatus in (0,1,2) and workid=" + oleDb.WorkId + @"
                            ) a
                            where  CAST(substring(EndInvoiceNO,patindex( '%[0-9]%',EndInvoiceNO),LEN(EndInvoiceNO)) AS BIGINT) between " + startNo + " and " + endNo;
                DataTable tb = oleDb.GetDataTable(sql);
                string fileter = "CostStatus in (0,1)";
                object objSum = tb.Compute("Sum(TotalFee)", fileter);
                totalMoney = 0;
                if (!Convert.IsDBNull(objSum))
                {
                    totalMoney = Convert.ToDecimal(objSum);
                }

                count = tb.Select(fileter).Length;

                fileter = "CostStatus in (1)";
                objSum = tb.Compute("Sum(TotalFee)", fileter);
                refundMoney = 0;
                if (!Convert.IsDBNull(objSum))
                {
                    refundMoney = Convert.ToDecimal(objSum);
                }

                refundCount = tb.Select(fileter).Length;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 获取票据列表
        /// </summary>
        /// <returns>DataTable票据列表</returns>
        public DataTable GetInvoices()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.*,b.name as empname,c.name as allotempname,case when status=0 then '在用' when status=1 then '用完' when status=2 then '备用'  when status=3 then '停用'  end as statusname");
            strSql.Append(" ,case when invoicetype=0 then '门诊收费票' when invoicetype=1 then '门诊挂号票' when invoicetype=2 then '住院预交金票'  when invoicetype=3 then '住院结算票'  when invoicetype=4 then '账户充值票' end as invoicetypename");
            strSql.Append("  from Basic_Invoice a left join BaseEmployee b on a.empid=b.empid left join BaseEmployee c on a.AllotEmpID=c.empid where a.workid=" + oleDb.WorkId);
            return oleDb.GetDataTable(strSql.ToString());
        }
        #endregion

        #region 挂号类型维护
        /// <summary>
        /// 获取挂号类型对应的收费项目明细
        /// </summary>
        /// <param name="regtypeid">挂号类别ID</param>
        /// <returns>DataTable收费项目明细</returns>
        public DataTable GetRegItemFees(int regtypeid)
        {
            string strsql = @"
                       select * from
                         (
                       select a.id,a.itemid,a.regtypeid,ISNULL(b.itemname,'') itemname,b.sellprice as itemprice,b.statId,b.ItemClass,b.Standard,b.UnPickUnit,b.MiniUnitName,b.MiniConvertNum,b.InPrice,b.SellPrice from op_regitemfee a left join ViewFeeItem_SimpleList b
                 on a.itemid=b.itemid where a.regtypeid=" + regtypeid+" and a.workid="+oleDb.WorkId+" )T where T.itemname<>''";
            return oleDb.GetDataTable(strsql);
        }
        #endregion

        #region 门诊排班维护

        /// <summary>
        /// 获取指定日期的排班信息
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="deptid">科室ID</param>
        /// <param name="docid">医生ID</param>
        /// <returns>DataTable排班信息</returns>
        public DataTable GetSchedualOneDate(DateTime bdate,DateTime edate, int deptid, int docid)
        {
            string strWhere = " where workid="+oleDb.WorkId;
            strWhere += " and SchedualDate>='" + bdate.ToString("yyyy-MM-dd") + "' and SchedualDate<='"+edate.ToString("yyyy-MM-dd")+"'";
            if (deptid > 0)
            {
                strWhere += " and deptid=" + deptid ;
            }

            if (docid > 0)
            {
                strWhere += "  and docempid=" + docid ;
            }

            string strsql = @"select a.SchedualID,a.deptid,a.docempid,a.DocProfessionName,a.SchedualDate,a.SchedualTimeRange,a.WorkID,a.flag,
                      case when SchedualTimeRange=1 then '上午' when SchedualTimeRange =2 then '下午'  when SchedualTimeRange=3 then '晚上' end as TimeRangeName,
                      b.name as deptname,c.name as docname,case when a.flag=1 then '出诊' else '停诊' end as schedualFlag
                 from (select * from OP_DocSchedual   " + strWhere+ " and workid="+oleDb.WorkId+")a left join  BaseDept b on a.deptid=b.deptid left join BaseEmployee c on a.docempid=c.empid ";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取之前最近一次排班日期
        /// </summary>
        /// <param name="deptid">科室ID</param>
        /// <param name="docid">医生ID</param>
        /// <returns>object最近一次排班日期</returns>
        public object GetMaxSchedualDate(int deptid, int docid)
        {           
            string strWhere = " where workid="+oleDb.WorkId;
            if (deptid > 0)
            {
                strWhere += " and deptid=" + deptid;
            }

            if (docid > 0)
            {
                strWhere += " and docempid=" + docid;
            }

            string strsql = @"select max(SchedualDate) as maxdate from OP_DocSchedual "+strWhere;
            return oleDb.GetDataResult(strsql);
        }

        /// <summary>
        /// 复制排班时，删除原有排班信息
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="deptid">科室ID</param>
        /// <param name="docid">医生ID</param>        
        public void DeleteOldCopySchedual(DateTime bdate, DateTime edate, int deptid, int docid)
        {
            string strWhere = " where workid=" + oleDb.WorkId;
            strWhere += " and SchedualDate>='" + bdate.ToString("yyyy-MM-dd") + "' and SchedualDate<='" + edate.ToString("yyyy-MM-dd") + "'";
            if (deptid > 0)
            {
                strWhere += " and deptid=" + deptid;
            }

            if (docid > 0)
            {
                strWhere += "  and docempid=" + docid ;
            }

            string strsql = "delete from OP_DocSchedual " + strWhere;
            oleDb.DoCommand(strsql);
        }
        #endregion

        #region 挂号
        /// <summary>
        /// 根据日期和时间段获取排班科室
        /// </summary>
        /// <param name="date">排班日期</param>  
        /// <param name="timerange">timerange1上午2下午3晚上</param>   
        /// <returns>DataTable排班科室</returns>
        public DataTable GetScheualDept(DateTime date, int timerange)
        {
            string strsql = @"select distinct a.deptid,b.name,b.pym,b.wbm from
         (select  distinct deptid,docempid from OP_DocSchedual
          where SchedualDate='" + date.ToString("yyyy-MM-dd") + @"' and SchedualTimeRange="+timerange+"  and flag=1 and workid=" + oleDb.WorkId + @")a
           left join BaseDept b on a.deptid=b.deptid 
          left join BaseEmployee c on a.docempid=c.empid left join BaseUser d on c.empid=d.empid
               where b.delflag=0 and d.usertype=2";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 根据日期和时间段获取排班医生
        /// </summary>
        /// <param name="date">排班日期</param>   
        /// <param name="timerange">timerange1上午2下午3晚上</param>   
        /// <returns>DataTable</returns>
        public DataTable GetSchedualDoctor(DateTime date,int timerange)
        {
            string strsql = @"select a.DeptID,a.docempid as empid,b.name,b.pym,b.wbm,a.DocProfessionName from
         (select  DeptID,docempid,DocProfessionName from OP_DocSchedual
          where SchedualDate='" + date.ToString("yyyy-MM-dd") + @"' and SchedualTimeRange=" + timerange + "  and flag=1 and workid=" + oleDb.WorkId + @")a
           left join BaseEmployee b on a.docempid=b.empid left join BaseUser c on b.empid=c.empid  where c.usertype=2 and b.DelFlag=0 and c.Lock=0";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 查询会员信息
        /// </summary>
        /// <param name="nameContent">姓名</param>
        /// <param name="telContent">电话号码</param>
        /// <param name="idContent">身份证号</param>
        /// <param name="memberid">会员ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetMemberInfoByOther(string nameContent, string telContent, string idContent,int memberid)
        {
            string strWhere = string.Empty;            
            if (nameContent != string.Empty)
            {
                strWhere += " and membername='" + nameContent + "'";
            }

            if (telContent != string.Empty)
            {
                strWhere += " and mobile='" + telContent + "'";
            }

            if (idContent != string.Empty)
            {
                strWhere += " and idnumber='" + idContent + "'";
            }

            if (memberid > 0)
            {
                strWhere += " and MemberID=" + memberid;
            }

            string strSql = @" select * from V_ME_AccountInfo where MemberUseFlag=1 
                                and AccountUseFlag=1 "+strWhere+" ";
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 查找会员信息
        /// </summary>
        /// <param name="queryContent">查找条件</param>
        /// <returns>DataTable会员信息</returns>
        public DataTable GetMemberInfoByQueryConte(string queryContent)
        {
            string strSql = @" select * from V_ME_AccountInfo where MemberUseFlag=1 
                                and AccountUseFlag=1  and (membername like '" + queryContent + "%' or mobile like '" + queryContent + "%'  or idnumber like '" + queryContent + "%'  or MedicareCard like '" + queryContent + "%') ";
            return oleDb.GetDataTable(strSql);
        }

        /// <summary>
        /// 获取操作员当天的挂号数据
        /// </summary>
        /// <param name="operatorID">操作员ID</param>
        /// <param name="date">日期</param>
        /// <returns>DataTable</returns>
        public DataTable GetRegInfoByOperator(int operatorID, DateTime date)
        {
            DataTable dtReginfo = new DataTable();

            string strsql = @"select a.patname,a.VisitNO,a.RegDeptName,a.RegDocName,a.cardno,a.RegTypeName,b.totalFee,b.EndInvoiceNO,a.regdate,case when a.regstatus=0 then '正常' else '退号' end as statusname,a.regstatus 
                from (select * from op_patlist where CONVERT(varchar(100),regdate, 23)='" + date.ToString("yyyy-MM-dd")+ "' and OperatorID="+operatorID+" and workid="+oleDb.WorkId+") a left join op_costhead b on a.costheadid=b.costheadid order by a.regdate desc";
            dtReginfo = oleDb.GetDataTable(strsql);
            return dtReginfo;
        }

        /// <summary>
        /// 由挂号类别ID计算挂号总金额
        /// </summary>
        /// <param name="regTypeid">挂号类别ID</param>
        /// <returns>decimal</returns>
        public decimal GetRegtotalFee(int regTypeid)
        {            
            string strsql = " SELECT SUM(b.SellPrice) FROM dbo.OP_RegItemFee a LEFT JOIN dbo.ViewFeeItem_SimpleList b ON a.itemid=b.ItemID WHERE a.RegTypeID=" + regTypeid+" and a.workid="+oleDb.WorkId;
            object obj = oleDb.GetDataResult(strsql);
            return obj==DBNull.Value?0:Convert.ToDecimal(obj);
        }

        /// <summary>
        /// 通过挂号类别获取按大项目分类的金额
        /// </summary>
        /// <param name="regTypeid">挂号类别ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetRegItemFeeByStat(int regTypeid)
        {
            string strsql = "select sum(b.SellPrice) as statFee,b.statId from op_regitemfee a left join ViewFeeItem_SimpleList b on a.itemid=b.itemid where a.regtypeid=" + regTypeid + " and a.workid=" + oleDb.WorkId + "  group by b.statid";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取挂号打印数据
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetRegPrint(int patlistid)
        {
            string strsql = "select a.*,b.totalfee,b.cashfee,b.endInvoiceNO as invoiceNO from  op_patlist a left join op_costhead b on a.costheadid=b.costheadid where a.patlistid=" + patlistid + " and a.regstatus=0 and a.RegStatus=0 and a.ChargeFlag=1";
            return oleDb.GetDataTable(strsql);
        }
        #endregion

        #region 收费

        /// <summary>
        /// 通过指定查询类别和内容获取挂号病人信息
        /// </summary>
        /// <param name="queryType">查询类别</param>
        /// <param name="content">查询内容</param>
        /// <returns>DataTable</returns>
        public DataTable GetRegPatList(OP_Enum.MemberQueryType queryType, string content)
        {
            string strWhere = string.Empty;
            if (queryType == OP_Enum.MemberQueryType.账户号码)
            {
                strWhere = " and cardno='"+content+"'";
            }

            if (queryType == OP_Enum.MemberQueryType.医保卡号)
            {
                strWhere = " and MedicareCard='" + content + "'";
            }

            string strsql = @"select *,dbo.fnGetDeptName(curedeptid) cureDeptName,dbo.fnGetEmpName(cureempid) cureEmpName  from op_patlist where regstatus=0 
              and regdate >='" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")+@"'
             and MemberID in(select MemberID from V_ME_AccountInfo where MemberUseFlag=1 
                                and AccountUseFlag = 1 "+strWhere+")";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 通过组合内容获取挂号病人信息
        /// </summary>
        /// <param name="nameContent">姓名</param>
        /// <param name="telContent">电话号码</param>
        /// <param name="idContent">身份证号</param>
        /// <param name="mediCard">医保卡号</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <returns>DataTable挂号病人信息</returns>
        public DataTable GetRegPatListByOther(string nameContent, string telContent, string idContent,string mediCard, DateTime bdate,DateTime edate)
        {
            string strWhere = string.Empty;
            if (nameContent != string.Empty)
            {
                strWhere += " and MemberName='" + nameContent + "'";
            }

            if (telContent != string.Empty)
            {
                strWhere += " and Mobile='"+telContent+"'";
            }

            if (idContent != string.Empty)
            {
                strWhere += " and IDNumber='" + idContent + "'";
            }

            if (mediCard != string.Empty)
            {
                strWhere += " and MedicareCard='" + mediCard + "'";
            }

            string strsql = @"select *,dbo.fnGetDeptName(curedeptid) cureDeptName,dbo.fnGetEmpName(cureempid) cureEmpName from op_patlist where regstatus=0 and regdate >='"+bdate.ToString("yyyy-MM-dd 00:00:00")+"' and regdate<='"+edate.ToString("yyyy-MM-dd 23:59:59")+@"' and  MemberID in(select MemberID from V_ME_AccountInfo where MemberUseFlag=1 
                                and AccountUseFlag = 1 " + strWhere + ")";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 收费修改OP_FeeItemHead状态
        /// </summary>
        /// <param name="costheadid">结算ID</param>
        /// <param name="invoiceNO">票据号</param>
        /// <param name="chareDate">收费日期</param>
        /// <param name="chargeEmpid">收费员ID</param>
        public void UpdateFeeItemHeadStatus(int costheadid,string invoiceNO,DateTime chareDate,int chargeEmpid)
        {
            string strsql = @"update OP_FeeItemHead set ChargeStatus=0,ChargeFlag=1,
                            InvoiceNO='" + invoiceNO + "',ChargeDate='" + chareDate + "' ,ChargeEmpID="+chargeEmpid+" where costheadid=" + costheadid + " and workid="+oleDb.WorkId;
            oleDb.DoCommand(strsql);
        }

        /// <summary>
        /// 收费完成后获取发票信息
        /// </summary>
        /// <param name="costHeadId">结算ID</param>
        ///  <returns>DataTable</returns>
        public DataTable  GetBalancePrintInvoiceDt(int costHeadId)
        { 
            string strsql = @"select invoiceNO,sum(TotalFee) as TotalFee 
                                    from OP_FeeItemHead 
                                   where ChargeStatus=0 and ChargeFlag=1 and RegFlag=0
                                    and costHeadId=" + costHeadId + " and workid=" + oleDb.WorkId + " group by invoiceNO";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 根据结算ID获取病人信息和收费明细
        /// </summary>
        /// <param name="costHeadId">结算ID</param>
        /// <returns>DataTable</returns>
        public DataTable  GetBalancePrintDetailDt(int costHeadId)
        {
            string strsql = @"select a.invoiceNO,c.patlistid,a.ChargeDate,  
                                    c.patname,c.visitno, c.IDNumber ,c.MedicareCard,
                                    dbo.fnGetEmpName(a.ChargeEmpID) ChargeName,
                                b.ItemID,b.ItemName,b.ItemType,b.StatID,
                                   b.RetailPrice,b.Amount,b.PresAmount,b.TotalFee,
                                    b.MiniUnit,
                                    b.Spec,(CASE d.YBItemLevel WHEN 1 THEN '无自付' WHEN  2 THEN '有自付' ELSE '全自付 ' END ) YBItemLevel
                                    from OP_FeeItemHead a 
                                     left outer join OP_FeeItemDetail b 
                                     on a.FeeItemHeadID=b.FeeItemHeadID
                                     LEFT OUTER JOIN mi_Match_his d ON b.ItemID=d.ItemCode
                                      left outer JOIN (SELECT cc.patlistid,cc.patname,cc.visitno,dd.IDNumber,dd.MedicareCard FROM   op_patlist cc  INNER JOIN ME_MemberInfo dd ON cc.MemberID=dd.MemberID AND cc.workid=dd.workid) c 
                                      on a.patlistid=c.patlistid
                                   where a.ChargeStatus=0 and a.ChargeFlag=1 and a.RegFlag=0
                                    and a.costHeadId=" + costHeadId + " and a.workid=" + oleDb.WorkId + " ";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 根据发票号获取病人信息和收费明细
        /// </summary>
        /// <param name="invoiceNO">发票号</param>
        /// <returns>DataTable病人信息和收费明细</returns>
        public DataTable GetBalancePrintDetailDtByInvoiceNo(string invoiceNO)
        {
            string strsql = @"select a.invoiceNO,
                                     c.patlistid  
                                     c.patname,
                                     c.visitno,                   
                                     dbo.fnGetEmpName(a.ChargeEmpID) ChargeName,
                                     b.ItemID,b.ItemName,b.ItemType,b.StatID,
                                     b.RetailPrice,b.Amount,b.PresAmount,b.TotalFee
                                     from OP_FeeItemHead a 
                                     left outer join OP_FeeItemDetail b 
                                     on a.FeeItemHeadID=b.FeeItemHeadID
                                     left outer join op_patlist c
                                      on a.patlistid=c.patlistid
                                     where a.ChargeStatus=0 and a.ChargeFlag=1 and a.RegFlag=0
                                    and a.invoiceNO=" + invoiceNO + " and a.workid=" + oleDb.WorkId ;
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 根据结算ID获取病人收费明细按大项目分类
        /// </summary>
        /// <param name="costHeadId">结算ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetBalancePrintStatDt(int costHeadId)
        {
            string strsql = @"select a.invoiceno,c.InFpItemID as statid,d.SubName as statname,sum(b.totalfee) totalfee from  op_feeitemhead a,
                      op_feeitemDetail b,
                      Basic_StatItem  c,
                       Basic_StatItemSubclass d
                     where a.costheadid = " + costHeadId+ @"
                     and a.feeitemheadid = b.feeitemheadid
                    and b.statid = c.statid
                    and c.OutFpItemID=d.SubID
                  group by a.invoiceno,c.InFpItemID,d.SubName";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取医生站处方
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns>DataTable</returns>
        public DataTable GetPrescription(string strWhere)
        {
            string strsql = @"select
                                dbo.fnGetEmpName(a.PresEmpID) PresDocName,
                                   dbo.fnGetDeptName(a.ExecDeptID) ExecDetpName,    
                               b.*
                               from 
                             (select FeeItemHeadID,PresEmpID,ExecDeptID from OP_FeeItemHead where workid=" + oleDb.WorkId + @" and " + strWhere + @")a 
                             left join OP_FeeItemDetail b
                              on a.FeeItemHeadID=b.FeeItemHeadID  order by PresDetailID";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取医生站处方明细
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetDocPrescription(int patlistid)
        {
            string strsql = @"select   
                               a.patlistid, 
                               a.PresType,
                               b.PresDetailID,
                               b.PresHeadID,
                               b.PresNO ,                             
                               b.ItemID,
                               b.ItemName,
                               b.StatID,
                               c.Standard ,
                               c.UnPickUnit,
                               c.InPrice,
                               c.MiniConvertNum ,
                               c.SellPrice,
                               c.ItemClass ,
                               c.NationalCode ,  
                               c.MiniUnitName,
                               b.DoseNum ,
                               b.ChargeAmount,   
                               b.PresAmount,   
                               b.PresDoctorID,
                               b.PresDeptID,
                               b.ExecDeptID,
                               b.PresDate ,
                               dbo.fnGetEmpName(b.PresDoctorID) PresDocName,
                               dbo.fnGetDeptName(b.ExecDeptID) ExecDetpName,
                               b.IsReimbursement,
                               b.Dosage,
                               b.Days,
                               d.FrequencyName,
                               c.DosageID,
                               b.FrequencyID,
                               c.DosageName,
                               b.DosageUnit,
                               b.Factor,
                           CASE WHEN (ISNULL(c.MedicareID,0)=3) THEN '□' 
                                             WHEN (ISNULL(c.MedicareID,0)=1) THEN '△'
                                              WHEN( ISNULL(c.MedicareID,0)=2) THEN '◇'
                                              ELSE '□'  END   AS MedicareItemName
                               from 
                             (select patlistid,PresHeadID,PresType from OPD_PresHead where workid=" + oleDb.WorkId + @" and patlistid=" + patlistid + @")a 
                             left join OPD_PresDetail b
                              on a.PresHeadID=b.PresHeadID 
                             left join Basic_Frequency d 
                             on b.FrequencyID=d.FrequencyID
                             left join ViewFeeItem_List c 
                              on b.ItemID=c.ItemID 
                              AND((a.PresType!=3 AND b.ExecDeptID=c.ExecDeptID) OR(a.PresType=3 AND 1=1) )
                             where  b.itemid IS NOT null and b.IsCharged=0 and b.IsTake=0 and b.IsCancel=0 order by PresHeadID,PresNO,GroupID,GroupSortNO ";
            return oleDb.GetDataTable(strsql);
        }
     
        /// <summary>
        /// 获取组合项目明细
        /// </summary>
        /// <param name="examItemID">组合项目ID</param>
        /// <returns>返回组合项目明细</returns>
        public DataTable GetExamItemDetailDt(int examItemID)
        {
            string strsql = @"select a.ItemAmount,
                               b.ItemID,
                               b.ItemName,
                               b.StatID,
                               b.Standard ,
                               b.UnPickUnit,
                               b.InPrice,
                               b.MiniConvertNum ,
                               b.SellPrice,
                               b.ItemClass ,
                               b.NationalCode ,  
                               b.MiniUnitName,
                       CASE WHEN (ISNULL(b.MedicareID,0)=3) THEN '□' 
                                             WHEN (ISNULL(b.MedicareID,0)=1) THEN '△'
                                              WHEN( ISNULL(b.MedicareID,0)=2) THEN '◇'
                                              ELSE '□' End  AS MedicareItemName
                          from Basic_ExamItemFee a  left join ViewFeeItem_List b on a.ItemID=b.ItemID where a.ExamItemID={0} AND a.WorkId={1}";
            strsql = string.Format(strsql, examItemID, oleDb.WorkId);
            return oleDb.GetDataTable(strsql);
        }
        #endregion

        #region 退费
        /// <summary>
        /// 通过票据号获取票号对应处方
        /// </summary>
        /// <param name="invoiceNO">票据号</param>
        /// <param name="operatoreid">操作员ID</param>
        /// <returns>DataTable票号对应处方</returns>
        public DataTable GetInvoicePresc(string invoiceNO,int operatoreid)
        {
            DataTable dtPresc = new DataTable();
            string strsql = @"select 0 as presNO,b.patlistid, b.invoiceNO,b.patname,dbo.fnGetPatTypeName(a.pattypeid) PatTypeName,
                                 b.feeno,b.FeeItemHeadID,b.PresEmpID,
                             dbo.fnGetDeptName(b.presdeptid) presDeptName,
                             dbo.fnGetEmpName(b.PresEmpID) presdocName,
                             b.totalFee,b.DistributeFlag,
                             b.PresDate,b.ChargeDate,
                            case when b.DistributeFlag=0 and c.ExamItemID=0 then '未发药' when b.DistributeFlag=1 and c.ExamItemID=0 then '已发药' 
                          when b.DistributeFlag=0 and c.ExamItemID>0 then '未确费' when b.DistributeFlag=1 and c.ExamItemID>0 then '已确费' 
                             end as statusName,
                             c.PresDetailID,
                              c.ItemID,c.presamount,c.presamount as refundpresamount,
                                c.ItemType,c.StatID,c.ItemName,c.spec,c.PackUnit,
                               c.UnitNO,c.RetailPrice,c.TotalFee as oldItemFee,
                                c.MiniUnit,c.Amount,
                                0 as oldPackNum,0 as oldMiniNum,0 as RefundPackNum,0 as RefundMiniNum,
                                c.TotalFee as RefundFee,
                                c.Memo,
                                c.ExamItemID,
                                 d.ResolveFlag                                
                              from 
                            OP_CostHead a
                             left join OP_FeeItemHead b
                              on a.costheadid=b.costheadid                            
                            left join OP_FeeItemDetail c
                            on b.FeeItemHeadID=c.FeeItemHeadID  
                            left join ViewFeeItem_List d 
                             on c.ItemID=d.ItemID  AND b.ExecDeptID=d.ExecDeptId                           
                              where a.CostStatus=0 and b.invoiceNO='" + invoiceNO + "' and b.ChargeFlag=1 and b.regflag=0  and b.ChargeStatus=0 order by b.FeeItemHeadID";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 查询退费消息
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="querycontion">查询条件</param>
        /// <param name="operatorid">操作员ID</param>
        /// <returns>DataTable</returns>
        public DataTable QueryRefundMessage(DateTime bdate, DateTime edate, string querycontion,int operatorid)
        {
            DataTable dtPresc = new DataTable();
            string strWhere = string.Empty;
            if (querycontion != string.Empty)
            {
                strWhere += " and (a.patname='"+querycontion+"' or a.invoiceNum='"+querycontion+"')";
            }

            string strsql = @"select  a.invoiceNum as DisPlayinvoiceNO, '' as oldpackAmount,'' as refundpackAmount,
                                       dbo.fnGetEmpName(a.RefunddocID) RefundDoc,a.RefundDate,
                                    a.RefundDocID,a.RefundDate,a.patName,a.invoiceNum,
		                             b.itemid,b.itemname,c.feeno,d.amount,b.refundAmount,d.statid,d.spec,
		                             d.retailPrice,b.refundFee,b.feeitemheadid,
                                     d.presamount,b.refundpresamount,
	                             	d.packunit,d.miniunit,d.unitNO,d.itemtype,
		                             b.DistributeFlag,case when b.DistributeFlag = 0 then '未发药' else '已发药' end as DistributeStatus,
		                            b.RefundFlag,case when b.RefundFlag = 0 then'未退药' else '已退药' end as RefundStatus,
		                           a.RefundPayFlag,case when a.RefundPayFlag = 0 then '未退费' else '已退费' end as RefundPayStatus,
                                  d.Memo,
                                  d.ExamItemID
                                   from OP_FeeRefundHead a
                                   left join OP_FeeRefundDetail b
                                    on a.ReHeadID = b.ReHeadID
                                  left join OP_FeeItemHead c
                                  on b.FeeItemHeadID = c.FeeItemHeadID
                                   left join OP_FeeItemDetail d
                                     on b.FeeItemDetailID = d.presDetailID
                                 where a.RefundDate >= '" + bdate+@"' and a.RefundDate <= '"+edate+@"'
                                  and a.Flag = 0  "+strWhere+" order by b.feeitemheadid";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 通过红冲ID获取新的FeeitemHeadId
        /// </summary>
        /// <param name="oldFeeitemHeadId">原对应的费用头ID</param>
        /// <returns>int</returns>
        public int GetNewFeeItemHeadId(int oldFeeitemHeadId)
        {
            string strsql = "select FeeItemHeadID from OP_FeeItemHead where oldid=" + oldFeeitemHeadId;
            return Convert.ToInt32( oleDb.GetDataResult(strsql));
        }
        #endregion

        #region 个人缴款

        /// <summary>
        /// 获取交款表信息ByEmpId
        /// </summary>
        /// <param name="iEmpId">用户ID</param>
        /// <param name="iState">交款状态 0 未交 1已交</param>
        /// <param name="bdate">已交的开始时间</param>
        /// <param name="edate">已交的结束时间</param>
        /// <returns>交款表信息</returns>
        public List<OP_Account> GetAccountByEmp(int iEmpId,int iState, DateTime bdate, DateTime edate)
        {
            string strSql = string.Empty;
            if (iState > 0)
            {
                strSql = @" SELECT a.AccountID
                                ,a.LastDate
                                ,a.TotalFee
                                ,a.CashFee
                                ,a.PromFee
                                ,a.PosFee
                                ,a.AccountEmpID
                                ,a.AccountDate
                                ,a.AccountFlag
                                ,a.ReceivFlag
                                ,a.ReceivEmpID
                                ,a.ReceivDate
                                ,a.ReceivBillNO
                                ,a.WorkID
                                ,a.InvoiceCount
                                ,a.RefundInvoiceCount
                                ,a.RoundingFee
                                ,a.AccountType
                                ,dbo.fnGetEmpName(a.AccountEmpID) AccountEmpName
                                FROM dbo.OP_Account a 
                                WHERE a.AccountEmpID = {0} and a.WorkID={1} and a.AccountFlag={2} 
                                      and AccountDate between '" + bdate.ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + edate.ToString("yyyy-MM-dd HH:mm:ss") + "' order by a.AccountDate desc";
            }
            else
            {
                strSql = @" SELECT a.AccountID
                                ,a.LastDate
                                ,a.TotalFee
                                ,a.CashFee
                                ,a.PromFee
                                ,a.PosFee
                                ,a.AccountEmpID
                                ,a.AccountDate
                                ,a.AccountFlag
                                ,a.ReceivFlag
                                ,a.ReceivEmpID
                                ,a.ReceivDate
                                ,a.ReceivBillNO
                                ,a.WorkID
                                ,a.InvoiceCount
                                ,a.RefundInvoiceCount
                                ,a.RoundingFee
                                ,a.AccountType
                                ,dbo.fnGetEmpName(a.AccountEmpID) AccountEmpName
                                FROM dbo.OP_Account a 
                                WHERE a.AccountEmpID = {0} and a.WorkID={1} and a.AccountFlag={2}  ";
            }

            return oleDb.Query<OP_Account>(string.Format(strSql, iEmpId,WorkId, iState), string.Empty).ToList();              
        }

        /// <summary>
        /// 根据accountid获取票据信息
        /// </summary>
        /// <param name="accountid">结账ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetAccountInvoiceData(int accountid)
        {
            string strsql = @" select invoicetype, InvoiceID,                              
                             max(statno)+' - '+ max(endno) as invoiceNOs,
                             sum(invoiceAllcount) as invoiceAllcount,
                             sum(refundinvoicecount) as refundinvoicecount,
                             CAST(substring(MAX(endno),patindex( '%[0-9]%',MAX(endno)),LEN(MAX(endno))) AS BIGINT)
							 -						  
							CAST(substring(max(statno),patindex( '%[0-9]%',max(statno)),LEN(max(statno))) AS BIGINT)
                           +1- sum(invoiceAllcount) as BadInvoiceCount,                                                    
                             sum(refundFee) as refundFee
                             from
                             (
                             select 
                             case when c.invoicetype=0 then '门诊收费票' when c.invoicetype=1 then '门诊挂号票' 
                            when c.invoicetype=2 then '住院预交金票'  when c.invoicetype=3 then '住院结算票'  
                              when c.invoicetype=4 then '账户充值票' end as invoicetype,
                               a.InvoiceID,                            
                             '' as statno,
                             '' as endno,
                              0 as invoiceAllcount,
                              count(distinct b.invoiceno) as refundinvoicecount,
                              sum(b.totalfee)*-1 as refundFee
                             from(select * from op_costhead  where accountid = "+accountid+ @" and coststatus = 2)a
                              left join op_feeitemhead b on a.costheadid = b.costheadid 
                              left join  Basic_Invoice c on a.InvoiceID=c.ID
                              group by a.InvoiceID,c.invoicetype

                             union all

                             select 
                             case when c.invoicetype=0 then '门诊收费票' when c.invoicetype=1 then '门诊挂号票' 
                             when c.invoicetype=2 then '住院预交金票'  when c.invoicetype=3 then '住院结算票'  
                              when c.invoicetype=4 then '账户充值票' end as invoicetype,
                               a.InvoiceID,                             
                             min(b.invoiceNO) as statno,
                             max(b.invoiceNO) as endno,
                             count(distinct b.invoiceno) as invoiceAllcount,
                             0 as refundinvoicecount,
                             0 as refundfee
                             from(select * from op_costhead where accountid = " + accountid + @" and coststatus in(0, 1)) a
                             left join op_feeitemhead b on a.costheadid = b.costheadid
                             left join  Basic_Invoice c on a.InvoiceID=c.ID
                             group by  a.InvoiceID,c.invoicetype
                             ) AA
                            group by InvoiceID,invoicetype
                          ";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取支付方式数据
        /// </summary>
        /// <param name="accountid">交款ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetAccountPayMent(int accountid)
        {
            string strsql = @"select b.paymentcode,
                                     b.paymentid,
                                     b.paymentname,
                                     sum(b.paymentmoney) as paymentmoney,
                                     count(b.paymentcode) as paymentcount
                                     from
                                     (select * from op_costhead  where accountid = "+accountid+@" and coststatus in(0, 1, 2))a
                                     left join op_costpaymentinfo b on a.costheadid = b.costheadid
                                    and a.accountid = b.accountid
                                    group by b.paymentcode,b.paymentid,b.paymentname";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取项目分类数据
        /// </summary>
        /// <param name="accountid">交款ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetAccountItemData(int accountid)
        {
            string strsql = @"select * from ( 
                                    select f.subname as fpItemName,sum(d.totalFee) as ItemFee from
                               (select * from op_costhead where accountid = "+accountid+@" and coststatus in(0, 1, 2)) c
                               left join op_costDetail d
                               on c.costheadid = d.costheadid
                               left join (select * from Basic_StatItem where workid="+oleDb.WorkId+@") e
                               on d.statid = e.statid
                               left join Basic_StatItemSubclass f
                               on e.OutFpItemID = f.subid
                               group by f.subname)AA
                                where fpItemName is not null and ItemFee is not null";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取挂号费及收费金额
        /// </summary>
        /// <param name="accountid">交款ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetRegBalanceFee(int accountid)
        {
            string strsql = @"select regFlag,sum(totalFee) as totalFee from
                               op_costhead where accountid = "+accountid+@" and coststatus in(0,1,2)
                                 group by regFlag";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取票据明细
        /// </summary>
        /// <param name="invoiceID">发票卷ID</param>
        /// <param name="invoiceType">票据类型 0正常 1 退费</param>
        /// <param name="accountid">缴款ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetInvoiceDetail(int invoiceID, int invoiceType, int accountid)
        {
            string strsql = string.Empty;
            if (invoiceType == 0)
            {
                strsql = @"select a.costHeadid,a.PatName , a.BeInvoiceNO , a.EndInvoiceNO , 
                                a.CostDate , a.RoundingFee ,a.TotalFee                               
                             from op_costHead a where a.accountid=" + accountid + @" and a.invoiceID=" + invoiceID + @" and a.costStatus in(0,1) order by costDate desc";
            }
            else
            {
                strsql = @"select a.costHeadid,a.PatName , a.BeInvoiceNO , a.EndInvoiceNO , 
                                a.CostDate , a.RoundingFee ,a.TotalFee 
                           from op_costHead a where a.accountid=" + accountid + @" and a.invoiceID=" + invoiceID + @" and a.costStatus =2 order by costDate desc";
            }

            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取最大缴款单号
        /// </summary>
        /// <returns>int</returns>
        public int GetMaxJkdh()
        {
            string strsql = "select max(ReceivBillNO) from op_account where workid=" + oleDb.WorkId;
            object obj = oleDb.GetDataResult(strsql);
            return obj == null ? 0 : Convert.ToInt32(obj);            
        }

        /// <summary>
        /// 获取会员充值明细数据
        /// </summary>
        /// <param name="iAcountId">缴款ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetAccountDataME(int iAcountId)
        {
            string strsql = @"select memi.Name PatName
                                    ,mema.CardNO
                                    ,mer.RechargeID
                                    ,mer.RechargeCode
                                    ,mer.AccountID
                                    ,(case mer.TypeID when 1 then '换卡' when 2 then '充值' end) ChangeType
                                    ,(case mer.PayType when 1 then '现金' when 2 then 'POS' end ) PayType
                                    ,mer.Money
                                    ,mer.Account
                                    ,( case mer.OperateFlag when 0 then '未交款' else '已交款' end) OperateFlag
                                    ,dbo.fnGetEmpName(mer.OperateID) EmpName
                                    ,mer.OperateTime
                            from dbo.ME_Recharge mer    
                           INNER JOIN dbo.ME_MemberAccount mema ON mema.AccountID=mer.AccountID  and mema.WorkID=mer.WorkID
                           INNER JOIN dbo.ME_MemberInfo memi ON memi.MemberID = mema.MemberID AND memi.WorkID = mema.WorkID
                            where mer.Account={0} and mer.workID={1}
                            order by  mer.OperateTime desc";
            strsql = string.Format(strsql, iAcountId, WorkId);
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取会员未交款金额汇总
        /// </summary>
        /// <param name="empid">人员ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetAccountDataMETotal(int empid)
        {
            string strsql = @"select mer.Account
                                     ,SUM(case mer.PayType when 1 then mer.Money when 2 then 0 end) '现金'
				                     ,SUM(case mer.PayType when 1 then 0 when 2 then mer.Money end) 'POS'
                            from dbo.ME_Recharge mer 
                            where (mer.OperateID={0} or {0}=0) and mer.workID={1} AND mer.OperateFlag=0
                            GROUP BY mer.Account";
            strsql = string.Format(strsql, empid, WorkId);
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 更新会员交易记录表
        /// </summary>
        /// <param name="iAccountID">缴款ID</param>
        /// <returns>bool</returns>
        public bool UpdateAccountME(int iAccountID)
        {
            string sUpSql = "update dbo.ME_Recharge set OperateFlag=1 where Account=" + iAccountID;
            return oleDb.DoCommand(sUpSql)>0;
        }
        #endregion

        #region 门诊缴款查询

        /// <summary>
        /// 查询所有已经缴款记录
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="empid">收费员ID</param>
        /// <param name="status">0未收款1已收款2全部</param>
        /// <returns>DataTable</returns>
        public DataTable GetAllAccountData(DateTime bdate,DateTime edate,int empid,int status)
        {
            DataTable dt = new DataTable();
            string strsql = string.Empty;
            strsql = @"select case when ReceivFlag=0 then 1 else 0 end as Selected,
                            ReceivBillNO,(case AccountType when 0 then '门诊收费' else '会员充值' end)  as AccountType,
                           dbo.fnGetEmpName(AccountEmpID) as empName,
                           ReceivFlag,
                           case when ReceivFlag = 0 then '未收款' else '已收款' end as ReceivFlagName,
                           AccountID,
                           LastDate,
                           AccountDate,
                           AccountFlag,
                           ReceivDate,
                           dbo.fnGetEmpName(ReceivEmpID) as ReceivEmpName,
                           TotalFee,
                           RoundingFee from
                           op_account where accountflag = 1 and
                           accountdate>='" + bdate + @"' and accountdate<='" + edate + @"'
                           and (ReceivFlag=" + status + @"  or " + status + @"=2)
                           and (AccountEmpID=" + empid + @" or " + empid + @"=0) and workid=" + oleDb.WorkId + " order by ReceivFlag,ReceivBillNO,AccountEmpID";           

            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 查询已缴款支付信息
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="empid">人员ID</param>
        /// <returns>DataTable支付信息</returns>
        public DataTable GetAllAccountPayment(DateTime bdate, DateTime edate, int empid)
        {
            string strsql = string.Empty;
            strsql = @"select * from OP_AccountPatMentInfo 
                              where accountid in
                             (select accountid from op_account
                             where accountflag = 1 and
                             accountdate>='" + bdate + @"' and accountdate<='" + edate + @"'
                             and (AccountEmpID=" + empid + @" or " + empid + @"=0) and workid=" + oleDb.WorkId + ")";

            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 查询未缴款支付信息
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="empid">人员ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetAllNotAccountPayment(DateTime bdate, DateTime edate, int empid)
        {
            string  strsql = @"select b.paymentcode,
                                     b.paymentid,
                                     b.paymentname,
                                     b.accountid,
                                     sum(b.paymentmoney) as paymentmoney,
                                     count(b.paymentcode) as paymentcount
                                     from
                                     (select * from op_costhead  where accountid in
                                     (select accountid from op_account
                                      where accountflag = 0                                          
                                     and (AccountEmpID=" + empid + @" or " + empid + @"=0) and workid=" + oleDb.WorkId + @") and coststatus in(0, 1, 2))a
                                     left join op_costpaymentinfo b on a.costheadid = b.costheadid
                                     and a.accountid = b.accountid
                                     group by b.accountid,b.paymentcode,b.paymentid,b.paymentname order by b.paymentcode";
            
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 查询所有未缴款记录
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="empid">收费员ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetAllNotAccountData(DateTime bdate, DateTime edate, int empid)
        {
            DataTable dt = new DataTable();
            string strsql = string.Empty;
            strsql = @"select ReceivBillNO,(case AccountType when 0 then '门诊收费' else '会员充值' end) FeeType,
                           dbo.fnGetEmpName(AccountEmpID) as empName,
                           ReceivFlag,  
                           case when ReceivFlag = 0 then '未收款' else '已收款' end as ReceivFlagName,                        
                           AccountID,
                           LastDate,
                           AccountDate,
                           AccountFlag,                          
                           TotalFee,
                           RoundingFee from
                           op_account where accountflag = 0                          
                           and (AccountEmpID=" + empid + @" or " + empid + @"=0) and workid=" + oleDb.WorkId + " order by ReceivFlag,AccountEmpID,ReceivBillNO";
            
            return oleDb.GetDataTable(strsql);
        }
        #endregion

        #region 处方查询
        /// <summary>
        /// 处方查询
        /// </summary>
        /// <param name="queryDictionary">查询条件</param>
        /// <returns>DataTable</returns>
        public DataTable ReciptQuery(Dictionary<string, object> queryDictionary)
        {
            DataTable dt = new DataTable();
            string strWhere = "  b.workid="+oleDb.WorkId+ " and b.regflag=0  and b.chargeFlag=1";
            if (queryDictionary.ContainsKey("CostBdate"))
            {
                strWhere += " and b.ChargeDate>='" + queryDictionary["CostBdate"] + "' and b.ChargeDate<='" + queryDictionary["CostEdate"] + "'";
            }

            if (queryDictionary.ContainsKey("InvoiceNO"))
            {
                strWhere += " and b.InvoiceNO='" + queryDictionary["InvoiceNO"] + "'";
            }

            if (queryDictionary.ContainsKey("ChargeEmp"))
            {
                strWhere += " and b.ChargeEmpID=" + queryDictionary["ChargeEmp"];
            }
          
            if (queryDictionary.ContainsKey("PayType"))
            {
                strWhere += " and a.PatTypeID=" + queryDictionary["PayType"];
            }

            if (queryDictionary.ContainsKey("PatName"))
            {
                strWhere += " and b.PatName like '" + queryDictionary["PatName"] + "%'";
            }

            if (queryDictionary.ContainsKey("CardNO"))
            {
                strWhere += " and a.CardNO=" + queryDictionary["CardNO"];
            }

            if (queryDictionary.ContainsKey("RefundFalg"))
            {
                strWhere += " and b.ChargeStatus=1";
            }
            else
            {
                strWhere += " and b.ChargeStatus in(0,1)";
            }

            string strsql = @"select 0 as selected,                              
                              b.costHeadid,
                              b.FeeItemHeadID,
                              b.InvoiceNO,                              
                              case when b.ChargeStatus=0 then '正常' when b.ChargeStatus=1 then '退费' when b.ChargeStatus=2 then '红冲' end as status,                              
                              b.patname,
                              dbo.fnGetDeptName(b.PresDeptID) as presDeptName,
                              dbo.fnGetEmpName(b.PresEmpID) as presEmpName,
                              b.ChargeDate,
                              dbo.fnGetEmpName(b.ChargeEmpID) as ChargeEmpName,
                              b.totalFee,
                              dbo.fnGetDeptName(b.ExecDeptID) as ExecDeptName                           
                              from  op_feeItemHead b 
                              left join op_costHead a
                              on b.costHeadid=a.costHeadid and b.chargeFlag=1                                               
                              where " + strWhere + " order by b.ChargeDate desc";
            return oleDb.GetDataTable(strsql);
        }
        #endregion

        #region 门诊病人费用查询
        /// <summary>
        ///  查询门诊病人按支付方式查询
        /// </summary>
        /// <param name="queryDictionary">查询条件</param>
        /// <returns>DataTable</returns>
        public DataTable GetOPCostPayMentQuery(Dictionary<string, object> queryDictionary)
        {
            DataTable dt = new DataTable();
            string strCostWhere = GetStrWhereBydictionary(queryDictionary);
            string strAllWhere = " 1=1";
            if (Convert.ToInt32(queryDictionary["PresDeptID"]) > 0)
            {
                strAllWhere += " and c.CureDeptID=" + queryDictionary["PresDeptID"];
            }

            if (Convert.ToInt32(queryDictionary["PrsEmpID"]) > 0)
            {
                strAllWhere += " and c.CureEmpID=" + queryDictionary["PrsEmpID"];
            }  
                    
            if (queryDictionary["QueryCondition"].ToString() != string.Empty)
            {
                string content = queryDictionary["QueryCondition"].ToString();
                strAllWhere += " and (c.patname='" + content + "' or c.visitNO='" + content + "' or a.cardno='" + content + "' or a.EndInvoiceNO='" + content + "')";
            }

            string strsql = @"select a.costHeadID,
                              case when a.regflag = 0 then '收费' else '挂号' end as regflag,
	                          a.costDate,
	                          a.BeInvoiceNO,
	                          a.EndInvoiceNO,
	                          dbo.fnGetEmpName(a.ChargeEmpID) as ChargeEmpName,
	                          case when a.costStatus = 0 then '正常' when a.costStatus = 1 then'被退费' when a.costStatus = 2 then '退费' end as costStatus,
	                          a.cardNO,
	                          c.visitNO,
	                          c.PatName,
	                          c.PatSex,
	                          c.BirthDay,
	                          c.Age,
	                          dbo.fnGetPatTypeName(a.PatTypeID) as patTypeName,
	                          dbo.fnGetEmpName(c.CureEmpID) as DoctName,
	                          dbo.fnGetDeptName(c.CureDeptID) as DeptName,
	                          a.totalFee,
	                          a.RoundingFee,	                        
	                          b.payMentName,
	                          b.PayMentMoney
                              from
                              (select * from  OP_CostHead where " + strCostWhere + @")a
                              left join OP_CostPayMentInfo b
                              on a.CostHeadID = b.CostHeadId
                              left join OP_Patlist c
                              on a.PatListID = c.PatListID where " + strAllWhere + @" order by a.CostHeadID,a.InvoiceID,a.EndInvoiceNO desc";
            dt = oleDb.GetDataTable(strsql);
            return dt;
        }

        /// <summary>
        /// 查询门诊病人费用按项目分类
        /// </summary>
        /// <param name="queryDictionary">查询条件</param>
        /// <returns>DataTable</returns>
        public DataTable GetOPCostFpItemQuery(Dictionary<string, object> queryDictionary)
        {
            DataTable dt = new DataTable();
            string strCostWhere = GetStrWhereBydictionary(queryDictionary);
            string strAllWhere = " 1=1";
            if (Convert.ToInt32(queryDictionary["PresDeptID"]) > 0)
            {
                strAllWhere += " and c.CureDeptID=" + queryDictionary["PresDeptID"];
            }

            if (Convert.ToInt32(queryDictionary["PrsEmpID"]) > 0)
            {
                strAllWhere += " and c.CureEmpID=" + queryDictionary["PrsEmpID"];
            }

            if (queryDictionary["QueryCondition"].ToString() != string.Empty)
            {
                string content = queryDictionary["QueryCondition"].ToString();
                strAllWhere += " and (c.patname='" + content + "' or c.visitNO='" + content + "' or a.cardno='" + content + "' or a.EndInvoiceNO='" + content + "')";
            }

            string strsql = @"select a.costHeadID,
                              case when a.regflag = 0 then '收费' else '挂号' end as regflag,
	                          a.costDate,
	                          a.BeInvoiceNO,
	                          a.EndInvoiceNO,
	                          dbo.fnGetEmpName(a.ChargeEmpID) as ChargeEmpName,
	                          case when a.costStatus = 0 then '正常' when a.costStatus = 1 then'被退费' when a.costStatus = 2 then '退费' end as costStatus,
	                          a.cardNO,
	                          c.visitNO,
	                          c.PatName,
	                          c.PatSex,
	                          c.BirthDay,
	                          c.Age,
	                          dbo.fnGetPatTypeName(a.PatTypeID) as patTypeName,
	                          dbo.fnGetEmpName(c.CureEmpID) as DoctName,
	                          dbo.fnGetDeptName(c.CureDeptID) as DeptName,
	                          a.totalFee,
	                          a.RoundingFee,	                        
	                          f.subname as FpItemName,
	                          b.totalFee as ItemFee
                              from
                             (select * from  OP_CostHead where " + strCostWhere + @")a
                              left join OP_CostDetail b
                              on a.CostHeadID = b.CostHeadId
                              left join (select * from Basic_StatItem where workid=" + oleDb.WorkId + @") e
                              on b.statid = e.statid
                              left join Basic_StatItemSubclass f
                              on e.OutFpItemID = f.subid
                              left join OP_Patlist c
                              on a.PatListID = c.PatListID where " + strAllWhere + @" order by a.CostHeadID,a.InvoiceID,a.EndInvoiceNO desc";
            dt = oleDb.GetDataTable(strsql);
            return dt;
        }

        /// <summary>
        /// 处方查询构造查询条件
        /// </summary>
        /// <param name="queryDictionary">查询条件</param>
        /// <returns>string</returns>
        private string GetStrWhereBydictionary(Dictionary<string, object> queryDictionary)
        {
            string strCostWhere = " workid=" + oleDb.WorkId;
            if ((bool)queryDictionary["ChargeData"] == true)
            {
                strCostWhere += " and costDate>='" + queryDictionary["Bdate"] + "' and costDate<='" + queryDictionary["Edate"] + "'";
            }

            if ((bool)queryDictionary["AccountData"] == true)
            {
                strCostWhere += " and accountId in(select AccountId from OP_Account where AccountDate >='" + queryDictionary["Bdate"] + "' and AccountDate<='" + queryDictionary["Edate"] + "')";
            }

            if (Convert.ToInt32(queryDictionary["ChargerEmpID"]) > 0)
            {
                strCostWhere += " and ChargeEmpID=" + queryDictionary["ChargerEmpID"];
            }

            if (Convert.ToInt32(queryDictionary["PayTypeID"]) > 0)
            {
                strCostWhere += " and PatTypeID=" + queryDictionary["PayTypeID"];
            }

            if (queryDictionary["BeInvoiceNO"].ToString() != string.Empty)
            {
                strCostWhere += " and EndInvoiceNO>='" + queryDictionary["BeInvoiceNO"] + "'";
            }

            if (!string.IsNullOrEmpty( queryDictionary["EndInvoiceNO"].ToString() ))
            {
                strCostWhere += " and EndInvoiceNO<='" + queryDictionary["EndInvoiceNO"] + "'";
            }

            if ((bool)queryDictionary["AllSatus"] == true)
            {
                strCostWhere += " and costStatus in(0,1,2)";
            }

            if ((bool)queryDictionary["NormalStatus"] == true)
            {
                strCostWhere += " and costStatus =0";
            }

            if ((bool)queryDictionary["RefundStatus"] == true)
            {
                strCostWhere += " and costStatus =2";
            }

            if ((bool)queryDictionary["BalanceRecord"] == true)
            {
                strCostWhere += " and regFlag =0";
            }

            if ((bool)queryDictionary["RegRecord"] == true)
            {
                strCostWhere += " and regFlag =1";
            }

            return strCostWhere;
        }

        /// <summary>
        /// 获取病人费用明细
        /// </summary>
        /// <param name="costHeadid">病人结算ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetCostDetail(int costHeadid)
        {
            string strsql= @"select a.patName,a.feeNO,
                                    a.invoiceNO,
                                    dbo.fnGetDeptName(a.ExecDeptID) as execDeptName,
                                    a.ChargeDate,
                                    b.*
                                    from (select * from OP_FeeItemHead where costHeadid=" + costHeadid+ @") a 
                                   left join OP_FeeItemDetail b
                              on a.FeeItemHeadID=b.FeeItemHeadID order by a.FeeItemHeadID desc
                ";
            return oleDb.GetDataTable(strsql);
        }
        #endregion

        #region 修改医生站处方状态
        /// <summary>
        /// 医生站处方状态修改
        /// </summary>
        /// <param name="docPresHeadID">医生站处方头表ID</param>
        /// <param name="docPresNO">处方号</param>
        /// <param name="status">状态</param>
        public void UpdateDocPresStatus(int docPresHeadID, List<int> docPresNO,int status)
        {
            int[] ids = docPresNO.ToArray();
            string str = string.Join(",", ids);
            if (status == 1)
            {
                //收费
                string strsql = " update OPD_PresDetail set IsCharged=1 where PresHeadID=" + docPresHeadID + " and PresNO in(" + str + ")";
                oleDb.DoCommand(strsql);
                strsql = " update EXA_MedicalApplyDetail set ApplyStatus=1 where PresDetailID in(select PresDetailID from  OPD_PresDetail where PresHeadID=" + docPresHeadID + " and PresNO in(" + str + "))";
                oleDb.DoCommand(strsql);
            }
            else if(status==2)
            {
                //退费
                //注意：Modify by yjb退费是IsCancel=1 不是IsCharged=2
                string strsql = " update OPD_PresDetail set IsCancel=1 where PresHeadID=" + docPresHeadID + " and PresNO in(" + str + ")";
                oleDb.DoCommand(strsql);
                strsql = " update EXA_MedicalApplyDetail set IsReturns=1 where PresDetailID in(select PresDetailID from  OPD_PresDetail where PresHeadID=" + docPresHeadID + " and PresNO in(" + str + "))";
                oleDb.DoCommand(strsql);
            }
        }
        #endregion
    }
}
