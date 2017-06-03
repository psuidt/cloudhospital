using System.Collections.Generic;
using System.Data;
using System.Linq;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;

namespace HIS_OPDoctor.Dao
{
    /// <summary>
    /// 处方模板Dao
    /// </summary>
    public class SqlOPPresTemplate: AbstractDao, IOPPresTemplate
    {
        /// <summary>
        /// 获取模板树信息
        /// </summary>
        /// <param name="intLevel">模板级别</param>
        /// <param name="workID">机构id</param>
        /// <param name="presType">处方类型</param>
        /// <param name="deptID">科室id</param>
        /// <param name="empID">人员id</param>
        /// <returns>处方头信息</returns>
        public List<OPD_PresMouldHead> GetPresTemplate(int intLevel, int workID,int presType,int deptID,int empID)
        {
            string sql = @" select * from OPD_PresMouldHead where delflag=0 and ModulLevel=" + intLevel+ " and PresType="+ presType;
            sql = sql + " and workID=" + workID;

             switch (intLevel)
             {
                case 0:
                     break;
                case 1:
                    sql = sql + " and CreateDeptID=" + deptID;
                    break;
                case 2:
                    sql = sql + " and CreateEmpID=" + empID;
                    break;
             }
               
            return oleDb.Query<OPD_PresMouldHead>(sql, intLevel).ToList();
        }

        /// <summary>
        /// 检验模板名称是否重复
        /// </summary>
        /// <param name="name">模板名称</param>
        /// <param name="presType">处方类型</param>
        /// <param name="level">模板级别</param>
        /// <param name="pid">模板父ID</param>
        /// <returns>模板名称数据</returns>
        public DataTable CheckMoudelName(string name, int presType, int level, int pid)
        {
            string sql = @" select * from OPD_PresMouldHead where delflag=0 and ModulLevel=" + level + " and PresType=" + presType;
            sql = sql + " and PID="+pid+ " and ModuldName='"+name+"'";

            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 获取处方模板内容
        /// </summary>
        /// <param name="presHeadID">模板头ID</param>
        /// <returns>处方模板数据</returns>
        public DataTable GetPresTemplateData(int presHeadID)
        {
            string strsql = @"SELECT  b.PresMouldDetailID  ,
                                        b.PresMouldHeadID ,
                                        b.ItemID ,
                                        b.ItemName ,
                                       b.StatID,
                                        b.Price ,
                                        b.Entrust,
                                        b.ExecDeptID,
		                                dbo.fnGetDeptName(b.ExecDeptID) ExecDeptName,
                                        b.Spec ,
                                        b.Dosage ,
                                        b.DosageUnit ,
                                        b.Factor ,
                                        b.DoseNum ,
                                        b.ChannelID ,
                                        b.FrequencyID ,
                                        b.Days ,
                                        b.ChargeAmount ,
                                        b.ChargeUnit ,
                                        b.PresAmount ,
                                        b.PresAmountUnit ,
                                        b.PresFactor ,
                                        b.GroupID ,
                                        b.PresNO ,
                                        0 OrderNO ,
		                                b.GroupSortNO,
                                        c.ChannelName ,
                                        d.FrequencyName ,
                                        d.ExecuteCode ,
                                        1 RoundingMode,
                                        a.CreateDeptID as  PresDeptID,
										dbo.fnGetDeptName(a.CreateDeptID) PresDeptName,
										a.CreateEmpID as PresDoctorID,
										dbo.fnGetEmpName(a.CreateEmpID) PresDoctorName 
                                FROM    OPD_PresMouldHead a
                                        JOIN OPD_PresMouldDetail b ON a.PresMouldHeadID = b.PresMouldHeadID
                                        LEFT JOIN Basic_Channel AS c ON b.ChannelID = c.ID
                                        LEFT JOIN Basic_Frequency AS d ON b.FrequencyID = d.FrequencyID
                                WHERE   a.PresMouldHeadID = {0}
                                ORDER BY b.PresNO ,
                                        b.GroupID,b.GroupSortNO";

            strsql = string.Format(strsql, presHeadID);
            DataTable dt = oleDb.GetDataTable(strsql);
            return dt;
        }

        /// <summary>
        /// 删除模板明细
        /// </summary>
        /// <param name="presMouldDetailID">模板明细ID</param>
        /// <returns>true成功</returns>
        public bool DeletePrescriptionData(int presMouldDetailID)
        {
            string strsql = @"DELETE FROM OPD_PresMouldDetail WHERE PresMouldDetailID ={0}";
            strsql = string.Format(strsql, presMouldDetailID);
            int iRtn = oleDb.DoCommand(strsql);
            if (iRtn > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除处方
        /// </summary>
        /// <param name="presMouldHeadID">处方模板头id</param>
        /// <param name="presNo">处方号</param>
        /// <returns>true成功</returns>
        public bool DeletePrescriptionData(int presMouldHeadID, int presNo)
        {
            string strsql = @"DELETE FROM OPD_PresMouldDetail WHERE PresMouldHeadID={0} 
                                AND PresNO={1}";
            strsql = string.Format(strsql, presMouldHeadID, presNo);
            int iRtn = oleDb.DoCommand(strsql);
            if (iRtn > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新处方号和组号
        /// </summary>
        /// <param name="list">处方列表</param>
        /// <returns>true成功</returns>
        public bool UpdatePresNoAndGroupId(List<OPD_PresMouldDetail> list)
        {
            List<string> sqllist = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                string strsql = @"UPDATE OPD_PresMouldDetail SET PresNO={1},GroupID={2} WHERE PresMouldDetailID={0}";
                strsql = string.Format(strsql, list[i].PresMouldDetailID, list[i].PresNO, list[i].GroupID);
                sqllist.Add(strsql);
            }

            for (int i = 0; i < sqllist.Count; i++)
            {
                oleDb.DoCommand(sqllist[i]);
            }

            return true;
        }
    }
}
