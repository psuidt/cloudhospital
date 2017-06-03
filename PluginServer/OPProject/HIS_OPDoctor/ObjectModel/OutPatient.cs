using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_OPDoctor.Dao;

namespace HIS_OPDoctor.ObjectModel
{
    /// <summary>
    /// 门诊病人对象
    /// </summary>
    [Serializable]
    public class OutPatient : AbstractObjectModel
    {
        /// <summary>
        /// 取得医生所在科室信息
        /// </summary>
        /// <param name="empId">医生Id</param>
        /// <returns>医生所在科室数据</returns>
        public DataTable GetDocRelateDeptInfo(int empId)
        {
            DataTable dt = NewDao<IOPDDao>().GetDocRelateDeptInfo(empId);
            return dt;
        }

        /// <summary>
        /// 加载病人列表
        /// </summary>
        /// <param name="docId">医生Id</param>
        /// <param name="deptId">科室Id</param>
        /// <param name="regBeginDate">挂号开始日期</param>
        /// <param name="regEndDate">挂号结束日期</param>
        /// <param name="visitStatus">就诊状态</param>
        /// <param name="belong">病人所属</param>
        /// <returns>病人列表</returns>
        public DataTable LoadPatientList(int docId, int deptId, string regBeginDate, string regEndDate, int visitStatus, int belong)
        {
            DataTable dt = NewDao<IOPDDao>().LoadPatientList(docId, deptId, regBeginDate, regEndDate, visitStatus, belong);
            return dt;
        }

        /// <summary>
        /// 通过卡号就诊号查询病人信息
        /// </summary>
        /// <param name="id">查询信息</param>
        /// <param name="type">0卡号1就诊号2病人Id</param>
        /// <returns>病人信息</returns>
        public DataTable GetPatientInfo(string id, int type)
        {
            DataTable dt = NewDao<IOPDDao>().GetPatientInfo(id, type);
            return dt;
        }

        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="memberID">会员Id</param>
        /// <returns>会员信息</returns>
        public DataTable GetMemberInfo(int memberID)
        {
            DataTable dt = NewDao<IOPDDao>().GetMemberInfo(memberID);
            return dt;
        }
    }
}
