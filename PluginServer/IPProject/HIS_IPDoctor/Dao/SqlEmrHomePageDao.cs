using System;
using System.Data;
using System.Text;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_IPDoctor.Dao
{
    /// <summary>
    /// SqlEmrHomePageDao数据库实现
    /// </summary>
    public class SqlEmrHomePageDao : AbstractDao, IEmrHomePageDao
    {
        /// <summary>
        /// 获取病案首页病人基本信息
        /// </summary>
        /// <param name="patlistID">病人ID</param>
        /// <returns>病人基本信息</returns>
        public DataTable GetCasePatientInfo(int patlistID)
        {
            StringBuilder selectSql = new StringBuilder();
            selectSql.Append(" SELECT  ");
            selectSql.Append(" a.PatListID ");
            selectSql.Append(" ,a.IdentityNum ");//身份证号
            selectSql.Append(" ,dbo.fnGetDictName(1002,a.Nationality) as  Nationality");//国籍
            selectSql.Append(" ,dbo.fnGetDictName(1003,a.Nation) as Nation");//民族
            selectSql.Append(" ,dbo.fnGetDictName(1004,a.Native) as  Native");//籍贯
            //selectSql.Append(" ,a.Native as  Native");//籍贯
            selectSql.Append(" ,a.Matrimony");//婚姻状况
            selectSql.Append(" ,dbo.fnGetDictName(1005,a.Occupation) as Occupation");//职业
            //selectSql.Append(" ,a.Occupation");//职业
            selectSql.Append(" ,dbo.fnGetDictName(1004,a.Birthplace) +a.BirthplaceDetail as Birthplace ");  //出生地址
            selectSql.Append(" ,dbo.fnGetDictName(1004,a.DRegisterAddr)+a.DRegisterAddrDetail as DRegisterAddr ");//户口地址                 
            selectSql.Append(" ,DZipCode ");//户口邮编     
            selectSql.Append(" ,dbo.fnGetDictName(1004,a.NAddress) +a.NAddressDetail as NAddress ");//现住址
            selectSql.Append(" ,Phone ");//电话
            selectSql.Append(" ,NZipCode ");     //现住编码      
            selectSql.Append(" ,UnitName ");//单位
            selectSql.Append(" ,UnitPhone ");//单位电话
            selectSql.Append(" ,UZipCode ");//单位邮编
            selectSql.Append(" ,RelationName ");//联系人
            selectSql.Append(" ,dbo.fnGetDictName(1007,Relation) as Relation  ");//联系人关系
            //selectSql.Append(" ,Relation  ");//联系人关系
            selectSql.Append(" ,RPhone ");//联系人电话
            selectSql.Append(" ,dbo.fnGetDictName(1004,a.RAddress)+a.RAddressDetail as  RAddress");//联系人地址
            selectSql.Append(" ,b.PatName ");//姓名
            selectSql.Append(" ,b.Sex ");//性别
            selectSql.Append(" ,b.Birthday ");//出生日期
            selectSql.Append(" ,b.Age ");//年龄
            selectSql.Append(" ,b.PatDatCardNo ");//健康卡号
            selectSql.Append(" ,b.Times ");//入院次数
            selectSql.Append(" ,b.CaseNumber ");//病案号
            selectSql.Append(" ,d.PatTypeCode as PatTypeID ");//医疗付费方式
            selectSql.Append(" ,c.WorkNo as HospCode ");//医疗机构编码
            selectSql.Append(" ,c.WorkName as HospName");//医疗机构名称
            //selectSql.Append(" ,9 as SourceWay");//病人来源,入院途径
            selectSql.Append(" ,b.EnterHDate");//入院时间
            selectSql.Append(" ,dbo.fnGetDeptName(b.EnterDeptID) as EnterDeptName");//入院科室
            selectSql.Append(" ,dbo.fnGetWardName(b.EnterWardID) as EnterWardName");//入院病房
            selectSql.Append(" ,b.LeaveHDate as LeaveDate");//出院时间
            selectSql.Append(" ,dbo.fnGetDeptName(b.CurrDeptID) as CurrDeptName");//出院科室
            selectSql.Append(" ,dbo.fnGetWardName(b.CurrWardID) as CurrwardName");//出院病房
            selectSql.Append(" ,b.Status");//病人状态
            //selectSql.Append(" ,0 as Weight");//体重
            //selectSql.Append(" ,0 as BabyInhosWeight");//体重
            selectSql.Append(" FROM IP_PatientInfo a");
            selectSql.Append(" Inner Join IP_PatList b");
            selectSql.Append(" on a.PatlistID=b.PatlistID");
            selectSql.Append(" Left Join BaseWorkers c");
            selectSql.Append(" on a.WorkID=c.WorkId");
            selectSql.Append(" Left Join Basic_PatType d");
            selectSql.Append(" on b.PatTypeID=d.PatTypeID");
            selectSql.Append(" WHERE ");
            selectSql.AppendFormat(" a.patlistID = {0} AND a.WorkID = {1} ", patlistID, oleDb.WorkId);
            return oleDb.GetDataTable(selectSql.ToString());
        }

        /// <summary>
        /// 获取病人转科信息
        /// </summary>
        /// <param name="patlistID">病人ID</param>
        /// <returns>转科信息</returns>
        public DataTable GetCasePatTransDeptInfo(int patlistID)
        {
            string strsql =
                        @"select dbo.fnGetDeptName(OldDeptID) as oldDeptName,
                           dbo.fnGetDeptName(NewDeptID) as newDeptName
                            from IPD_TransDept
                      where patlistID=" + patlistID + @"
                        and CancelFlag=0
                     and workid="
                    + oleDb.WorkId
                    + " order by TransDate ";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取病人诊断信息
        /// </summary>
        /// <param name="patlistID">病人ID</param>
        /// <returns>诊断信息</returns>
        public DataTable GetCasePatDiagInfo(int patlistID)
        {
            string strsql =
                            @"select a.ID,a.DiagnosisName,
                                  a.ICDCode,
                                   a.Main,
                                   b.Code,
                                   b.Name,
                                   a.Effect
                             from IPD_Diagnosis a left join (select * from BaseDictContent where classid=1031) b 
                             on a.DiagnosisClass=b.Id where a.patlistid="
                             + patlistID
                             + " and a.WorkId="
                             + oleDb.WorkId
                             + " order by DiagnosisTime";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取病案首页住院病人费用信息
        /// </summary>
        /// <param name="patlistID">病人ID</param>
        /// <returns>返回病案首页住院病人费用信息</returns>
        public DataTable GetCasePatFee(int patlistID)
        {
            string strsql =
                            @"select d.subName,sum(b.TotalFee) as totalFee from IP_CostHead
                            a left join IP_CostDetail b on a.CostHeadID=b.CostHeadID 
                            left join Basic_StatItem c on b.StatID=c.StatID
                            left join Basic_StatItemSubclass d on c.BaItemID=d.SubID                           
                            where a.PatlistId="
                            + patlistID
                            + " and a.WorkID="
                            + oleDb.WorkId
                            + " and a.Status=0  group by d.subName";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取在院病人费用信息
        /// </summary>
        /// <param name="patlistID">病人ID</param>
        /// <returns>在院病人费用信息</returns>
        public DataTable GetCasePatFeeInHospital(int patlistID)
        {
            string strsql = @"select d.subName,sum(a.TotalFee) as totalFee from IP_FeeItemRecord a                           
                            left join Basic_StatItem c on a.StatID=c.StatID
                            left join Basic_StatItemSubclass d on c.BaItemID=d.SubID                           
                            where a.PatlistId=" + patlistID + " and a.WorkID=" + oleDb.WorkId + " and  a.workid=c.workid and c.workid=d.workid group by d.subName";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取在院病人总费用信息
        /// </summary>
        /// <param name="patlistID">病人ID</param>
        /// <returns>总费用信息</returns>
        public DataTable GetCasePatTotalFeeInHospital(int patlistID)
        {
            string strsql = @"select sum(a.TotalFee) as totalFee,0 as SelfFee from IP_FeeItemRecord a 
                            where a.PatlistId=" + patlistID + "  AND a.CostHeadID = 0 and a.WorkID=" + oleDb.WorkId;
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取病人总费用信息
        /// </summary>
        /// <param name="patlistID">病人ID</param>
        /// <returns>返回获取病人总费用信息</returns>
        public DataTable GetCasePatTotalFee(int patlistID)
        {
            string strsql = @"select  totalFee,(DeptositFee-BalanceFee) as selfFee from IP_CostHead                                                    
                            where PatlistId=" + patlistID + " and WorkID=" + oleDb.WorkId + " and Status=0";
            return oleDb.GetDataTable(strsql);
        }

        /// <summary>
        /// 获取抗菌药物费用
        /// </summary>
        /// <param name="patlistID">病人ID</param>
        /// <returns>抗菌药物费用</returns>
        public decimal GetAntFee(int patlistID)
        {
            string strsql = @"select sum(a.TotalFee) from IP_FeeItemRecord a
                               left join DG_HospMakerDic b on a.ItemID=b.DrugID AND b.WorkID = a.WorkID
                              left join DG_CenterSpecDic c on  b.CenteDrugID = c.CenteDrugID
                        where a.PatlistID=" + patlistID + " and a.FeeClass=1  and c.AntID>0 and a.WorkID=" + oleDb.WorkId;
            object obj = oleDb.GetDataResult(strsql);
            return obj == DBNull.Value ? 0 : Convert.ToDecimal(obj);
        }
    }
}
