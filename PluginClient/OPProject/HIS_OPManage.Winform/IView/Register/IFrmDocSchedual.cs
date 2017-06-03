using System;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.OPManage;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    /// 医生排班界面接口类
    /// </summary>
    public  interface IFrmDocSchedual:IBaseView
    {
        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <param name="dtDept">科室</param>
        void loadDept(DataTable dtDept);

        /// <summary>
        /// 获取医生列表
        /// </summary>
        /// <param name="dtDoc">医生</param>
        void loadDoctor(DataTable dtDoc);

        /// <summary>
        /// 获取排班日期列表
        /// </summary>
        /// <param name="dtDate">排班日期列表</param>
        void loadSchedualDate(DataTable dtDate);
       
        /// <summary>
        /// 获取查询的开始日期
        /// </summary>
        DateTime GetStatDate { get; }

        /// <summary>
        /// 获取查询的结束日期
        /// </summary>
        DateTime GetEndDate { get; }

        /// <summary>
        /// 获取查询科室Id
        /// </summary>
        int QueryDeptid { get; }

        /// <summary>
        /// 获取查询医生ID
        /// </summary>
        int QueryDocid { get; }

        /// <summary>
        /// 当前当个排班对象
        /// </summary>
        OP_DocSchedual CurDocSchedual { get; set; }

        /// <summary>
        /// 获取排班信息
        /// </summary>
        /// <param name="dtSchedual">排班信息</param>
        void LoadSchedual(DataTable dtSchedual);

        /// <summary>
        /// 复制排班原最大排班日期
        /// </summary>
        DateTime CopyMaxDate { get; set; }

        /// <summary>
        /// 获取和设置新排班开始日期
        /// </summary>
        DateTime CopyNewBDate { get; set; }

        /// <summary>
        /// 所有收费员数据
        /// </summary>
        DataTable DtAlldoctor { get; set; }
    }
}