using System;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 入院登记接口
    /// </summary>
    public interface IAdmissionRegistration : IBaseView
    {
        /// <summary>
        /// 绑定住院病人列表
        /// </summary>
        /// <param name="patListDt">住院病人列表</param>
        /// <param name="isAdd">是否为新登记病人后查询病人列表</param>
        void Bind_grdPatList(DataTable patListDt, bool isAdd);

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="deptListDt">科室列表</param>
        void Bind_cboDeptList(DataTable deptListDt);

        /// <summary>
        /// 绑定病人类型列表
        /// </summary>
        /// <param name="patTypeListDt">病人列表</param>
        void Bind_cboPatType(DataTable patTypeListDt);

        /// <summary>
        /// 新入院病人ID
        /// </summary>
        int PatientID { get; }

        /// <summary>
        /// 新入院病人登记信息ID
        /// </summary>
        int PatListID { get; }

        /// <summary>
        /// 开始时间
        /// </summary>
        DateTime StartTime { get; }

        /// <summary>
        /// 结束时间
        /// </summary>
        DateTime EndTime { get; }

        /// <summary>
        /// 科室
        /// </summary>
        int Dept { get; }

        /// <summary>
        /// 病人类型
        /// </summary>
        int PatType { get; }

        /// <summary>
        /// 检索条件
        /// </summary>
        string SelectParm { get; }

        /// <summary>
        /// 病人状态集合
        /// </summary>
        string PatStatus { get; }

        /// <summary>
        /// 设置数据没加载完时不能进行病人登记，取消登记，修改病人信息操作
        /// </summary>
        void SetControlEnabled();
    }
}
