using System;
using System.Data;
using System.Text;
using EFWCoreLib.CoreFrame.Business;

namespace HIS_IPManage.Dao
{
    /// <summary>
    /// 住院诊断DAO
    /// </summary>
    public class SqlDiagnosisDao: AbstractDao, IDiagnosisDao
    {
        /// <summary>
        /// 加载病人诊断信息
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <returns>病人诊断信息</returns>
        public DataTable LoadDiagnosisInfo(int patListID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT a.ID, PatListID, dbo.fnGetDeptName(DeptID) as deptname, dbo.fnGetEmpName(DgsDocID) as empname, DiagnosisTime, DiagnosisClass, Main, ");
            sql.Append(" DiagnosisName, DiagnosisID, ICDCode, Effect, OrderID, a.WorkID, b.name,");
            sql.Append(" (CASE Main WHEN 1 THEN '主诊断'  ELSE ' ' END) AS mainDESC");
            sql.Append(" from IPD_Diagnosis a join  BaseDictContent b on a.DiagnosisClass=b.Id where PatListID=" + patListID);

            return oleDb.GetDataTable(sql.ToString());
        }

        /// <summary>
        /// 校验诊断名称是否重复
        /// </summary>
        /// <param name="patListID">病人入院登记ID</param>
        /// <param name="flag">是否为新增主诊断</param>
        /// <param name="diagID">诊断ID</param>
        /// <param name="main">是否为主诊断</param>
        /// <param name="diagName">诊断名</param>
        /// <param name="id">id</param>
        /// <returns>false：重复</returns>
        public bool CheckDiagnosisInfo(int patListID, int flag,int diagID,int main,string diagName,int id)
        {
            bool resFlag = false;
            StringBuilder sql = new StringBuilder();
            sql.Append(" select * from IPD_Diagnosis where PatListID=" + patListID);
            sql.Append(" and main="+ main);
            DataTable dt= oleDb.GetDataTable(sql.ToString());

            if (dt.Rows.Count>0)
            {
                //新增主诊断状态
                if ((flag==0))
                {
                    if (main==1)
                    {
                        resFlag = false;
                    }
                    else
                    {
                        DataRow[] dr = dt.Select("DiagnosisName='"+ diagName+"'");
                        resFlag = (dr.Length > 0) ? false : true;                       
                    }
                }
                else
                {
                    if (main == 1)
                    {
                        if (id==Convert.ToInt32(dt.Rows[0]["ID"]))
                        {
                            resFlag = true;
                        }
                        else
                        {
                            resFlag = false;
                        }
                    }
                    else
                    {
                        DataRow[] dr = dt.Select("DiagnosisName='" + diagName + "'");
                        if (dr.Length > 0)
                        {
                            if (Convert.ToInt32(dr[0]["ID"]) == id)
                            {
                                resFlag = true;
                            }
                            else
                            {
                                resFlag = false;
                            }
                        }
                        else
                        {
                            resFlag = true;
                        }
                    }
                }
            }
            else
            {
                resFlag = true;
            }

            return resFlag;
        }

        /// <summary>
        /// 获取诊断类型信息
        /// </summary>
        /// <returns>诊断类型信息</returns>
        public DataTable GetDiagnosisClass()
        {
            string sql = @" select Id,NAME from BaseDictContent where ClassId=1031";
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 获取诊断信息
        /// </summary>
        /// <returns>诊断信息</returns>
        public DataTable GetBasicDiagnosis()
        {
            string sql = @" select * from Basic_Disease ";
            return oleDb.GetDataTable(sql);
        }

        /// <summary>
        /// 删除诊断
        /// </summary>
        /// <param name="id">诊断id</param>
        /// <returns>删除行数</returns>
        public int DeleteDiag(int id)
        {
            string sql = @" DELETE from IPD_Diagnosis where ID="+ id;
            return oleDb.DoCommand(sql);
        }
    }
}
