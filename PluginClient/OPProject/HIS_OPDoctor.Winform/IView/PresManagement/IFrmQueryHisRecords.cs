using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_OPDoctor.Winform.IView
{
    /// <summary>
    /// 查看历史诊疗记录界面接口
    /// </summary>
    public interface IFrmQueryHisRecords : IBaseView
    {
        /// <summary>
        /// 病人id
        /// </summary>
        int PatListId { get; set; }

        /// <summary>
        /// 科室id
        /// </summary>
        int DeptId { get; set; }

        /// <summary>
        /// 病人信息
        /// </summary>
        DataTable DtPatient { get; set; }

        /// <summary>
        /// 获取历史就诊记录
        /// </summary>
        /// <param name="dtRecord">就诊记录数据集</param>
        void BindHisRecord(DataTable dtRecord);

        /// <summary>
        /// 绑定申请头表信息
        /// </summary>
        /// <param name="dt">申请头表数据集</param>
        void BindApplyHead(DataTable dt);

        /// <summary>
        /// 绑定医生所在的挂号科室
        /// </summary>
        /// <param name="dt">科室信息</param>
        /// <param name="deptId">当前登陆科室</param>
        void BindDocInDept(DataTable dt, int deptId);

        /// <summary>
        /// 绑定科室下的所有医生
        /// </summary>
        /// <param name="dt">医生数据</param>
        /// <param name="doctorId">医生id</param>
        void BindDoctor(DataTable dt, int doctorId);

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>查询条件字典集合</returns>
        Dictionary<string, string> GetQueryWhere();
    }
}
