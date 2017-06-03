using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;

namespace HIS_IPDoctor.Dao
{
    /// <summary>
    /// 医嘱模板数据库访问接口实现类
    /// </summary>
    public class SqlOrderTempManageDao : AbstractDao, IOrderTempManageDao
    {
        /// <summary>
        /// 获取医嘱模板列表
        /// </summary>
        /// <param name="tempLevel">模板级别</param>
        /// <param name="deptId">所属科室</param>
        /// <param name="empId">所属用户</param>
        /// <returns>模板列表</returns>
        public List<IPD_OrderModelHead> GetOrderTempList(int tempLevel, int deptId, int empId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT  * ");
            strSql.Append(" FROM    IPD_OrderModelHead ");
            strSql.AppendFormat(" WHERE   ModelLevel = {0} ", tempLevel);
            // 科室模板
            if (tempLevel == 1)
            {
                strSql.AppendFormat(" AND CreatDeptID = {0} ", deptId);
            }
            else if (tempLevel == 2)
            {
                // 个人模板
                strSql.AppendFormat(" AND CreateDocID = {0} ", empId);
            }

            strSql.AppendFormat(" AND WorkID = {0} ", oleDb.WorkId);
            strSql.Append(" AND DeleteFlag = 0 ORDER BY ModelHeadID ");
            return oleDb.Query<IPD_OrderModelHead>(strSql.ToString(), string.Empty).ToList();
        }

        /// <summary>
        /// 根据模板头ID删除模板明细数据
        /// </summary>
        /// <param name="modelHeadID">模板头ID</param>
        /// <param name="isModelType">是否为模板分类</param>
        public void DelOrderTempDetails(int modelHeadID, bool isModelType)
        {
            string strSql = string.Empty;
            if (isModelType)
            {
                // 删除模板分类
                strSql = string.Format(@" DELETE FROM IPD_OrderModelDetail WHERE ModelHeadID IN 
                (SELECT ModelHeadID FROM IPD_OrderModelHead WHERE PID = {0} AND WorkID = {1}) AND WorkID={1} ", modelHeadID, oleDb.WorkId);
            }
            else
            {
                // 删除模板
                strSql = string.Format(" DELETE FROM IPD_OrderModelDetail WHERE ModelHeadID = {0} AND WorkID={1} ", modelHeadID, oleDb.WorkId);
            }

            oleDb.DoCommand(strSql);
        }

        /// <summary>
        /// 删除模板分类关联的模板
        /// </summary>
        /// <param name="modelHeadID">模板分类ID</param>
        public void DelOrderTempByModelType(int modelHeadID)
        {
            string strSql = string.Format(@" DELETE FROM IPD_OrderModelHead 
                                WHERE PID = {0} AND  ModelType = 1 AND WorkID={1} ", modelHeadID, oleDb.WorkId);
            oleDb.DoCommand(strSql);
        }

        /// <summary>
        /// 根据模板头ID获取模板明细
        /// </summary>
        /// <param name="modelHeadID">模板头ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetOrderTempDetail(int modelHeadID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT A.ModelDetailID ");
            strSql.Append(" ,A.ModelHeadID ");
            strSql.Append(" ,A.OrderCategory ");
            strSql.Append(" ,A.GroupID ");
            strSql.Append(" ,A.ItemType ");
            strSql.Append(" ,A.ItemID ");
            strSql.Append(" ,A.ItemName ");
            strSql.Append(" ,A.Dosage ");
            strSql.Append(" ,A.DosageUnit ");
            strSql.Append(" ,A.Factor ");
            strSql.Append(" ,A.ChannelName ");
            strSql.Append(" ,A.ChannelID ");
            strSql.Append(" ,A.FrenquencyID ");
            strSql.Append(" ,A.Frenquency ");
            strSql.Append(" ,A.DoseNum ");
            strSql.Append(" ,A.Amount ");
            strSql.Append(" ,A.Unit ");
            strSql.Append(" ,A.UnitNO ");
            strSql.Append(" ,A.Entrust ");
            strSql.Append(" ,A.FirstNum ");
            strSql.Append(" ,A.ExecDeptId ");
            strSql.Append(" ,B.Name ExecDeptName ");
            strSql.Append(" ,A.ExamItemID ");
            strSql.Append(" ,A.Spec ");
            strSql.Append(" ,A.Flag ");
            strSql.Append(" ,A.WorkID ");
            strSql.Append(" ,A.DropSpec ");
            strSql.Append(" ,A.StatID ");
            strSql.Append(" ,0 AS IsLast ");
            strSql.Append(" ,1 AS SaveFlag ");
            strSql.Append(" ,A.OrderType ");
            strSql.Append(" FROM IPD_OrderModelDetail A ");
            strSql.Append(" LEFT JOIN BaseDept B ON A.ExecDeptId = B.DeptId ");
            strSql.AppendFormat(" WHERE A.ModelHeadID = {0} ", modelHeadID);
            return oleDb.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 根据模板明细ID删除模板明细数据
        /// </summary>
        /// <param name="modelDetailID">模板明细ID</param>
        /// <returns>是否删除成功</returns>
        public bool DelOrderDetailsData(string modelDetailID)
        {
            string strSql = string.Format(" DELETE FROM IPD_OrderModelDetail WHERE ModelDetailID IN ({0}) ", modelDetailID);
            return oleDb.DoCommand(strSql) > 0;
        }
    }
}
