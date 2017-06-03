using System;
using System.Data;

namespace HIS_IPDoctor.Dao
{
    /// <summary>
    /// 住院医生站数据库访问接口类
    /// </summary>
    public interface IIPDOrderDao
    {
        /// <summary>
        /// 住院医生站主界面获取床位信息
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <param name="doctorID">医生ID</param>
        /// <param name="isMy">true我的病人false科室病人</param>
        /// <returns>病人数据</returns>
        DataTable GetInBedPatient(int deptID, int doctorID, bool isMy);

        /// <summary>
        /// 住院医生站查询病人信息
        /// </summary>
        /// <param name="doctorID">医生ID</param>
        /// <param name="deptID">科室ID</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="isOut">true出院false在院</param>
        /// <param name="isMy">true我的病人false科室病人</param>
        /// <param name="queryContent">查询内容</param>
        /// <param name="patlistid">病人ID</param>
        /// <returns>病人信息</returns>
        DataTable GetPatientInfo(int doctorID, int deptID, DateTime bdate, DateTime edate, bool isOut, bool isMy, string queryContent,int patlistid);

        /// <summary>
        /// 查询病人住院费用信息
        /// </summary>
        /// <param name="patListID">病人Id</param>
        /// <returns>住院费用信息数据</returns>
        DataTable GetPatDepositFee(int patListID);

        /// <summary>
        /// 获取病人医嘱数据
        /// </summary>
        /// <param name="orderCategory">医嘱类型 0长嘱1临嘱</param>
        /// <param name="patlistid">病人Id</param>
        /// <param name="orderStatus">医嘱状态</param>
        /// <param name="deptID">科室ID</param>
        /// <returns>医嘱数据</returns>
        DataTable GetOrders(int orderCategory, int patlistid, int orderStatus, int deptID);

        /// <summary>
        /// 获取血糖测量数据
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <returns>返回血糖数据</returns>
        DataTable GetBloodGluRecord(int patlistid);
    }
}
