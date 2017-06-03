using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPNurse.Winform.IView
{
    /// <summary>
    /// 医嘱管理接口
    /// </summary>
    public interface IDoctorManagement: IBaseView
    {
        /// <summary>
        /// 科室ID
        /// </summary>
        int DeptID { get; }

        /// <summary>
        /// 医嘱类别(长期/临时)
        /// </summary>
        string OrderCategory { get; }

        /// <summary>
        /// 医嘱类型(新开/新停)
        /// </summary>
        string OrderStatus { get; }

        /// <summary>
        /// 皮试医嘱
        /// </summary>
        string AstFlag { get; }

        /// <summary>
        /// 病人登记ID
        /// </summary>
        int PatListID { get; }

        /// <summary>
        /// 分组ID
        /// </summary>
        int GroupID { get; }

        /// <summary>
        /// 转抄标志
        /// </summary>
        bool IsTrans { get; }

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="deptDt">科室列表</param>
        /// <param name="deptID">默认科室ID</param>
        void bind_DeptList(DataTable deptDt, int deptID);

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="patListDt">病人列表</param>
        void bind_PatList(DataTable patListDt);

        /// <summary>
        /// 绑定未转抄医嘱列表
        /// </summary>
        /// <param name="docListDt">未转抄医嘱列表</param>
        void bind_DocList(DataTable docListDt);

        #region 皮试界面
        /// <summary>
        /// 加载皮试医嘱数据
        /// </summary>
        /// <param name="dtSkinTest">皮试医嘱数据</param>
        void LoadSkinTestInfo(DataTable dtSkinTest);

        /// <summary>
        /// 查询皮试数据
        /// </summary>
        void QuerySkinTestInfo();
        #endregion
    }
}
