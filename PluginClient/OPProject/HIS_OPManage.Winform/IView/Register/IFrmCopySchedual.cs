using System;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    /// 排班复制界面接口类
    /// </summary>
    public interface IFrmCopySchedual : IBaseView
    {
        /// <summary>
        /// 加载科室
        /// </summary>
        /// <param name="dtDept">科室数据</param>
        void LoadDept(DataTable dtDept);

        /// <summary>
        /// 加载医生
        /// </summary>
        /// <param name="dtDoctor">医生数据</param>
        void LoadDoctor(DataTable dtDoctor);

        /// <summary>
        /// 复制排班的科室ID
        /// </summary>
        int CopyDeptid { get; set; }

        /// <summary>
        /// 复制排班的医生ID
        /// </summary>
        int CopyDocid { get; set; }

        /// <summary>
        /// 原开始日期
        /// </summary>
        DateTime OldBdate { get; }

        /// <summary>
        ///  //原结束日期
        /// </summary>
        DateTime OldEdate { get; }

        /// <summary>
        /// 新开始日期
        /// </summary>
        DateTime NewBdate { get; }

        /// <summary>
        /// 新结束日期
        /// </summary>
        DateTime NewEdate { get; }

        /// <summary>
        /// 历史排班最大日期
        /// </summary>
        DateTime MaxCopyDate { get; set; }

        /// <summary>
        /// 新排班开始最小日期
        /// </summary>
        DateTime MinNewCopyDate { get; set; }
    }
}
