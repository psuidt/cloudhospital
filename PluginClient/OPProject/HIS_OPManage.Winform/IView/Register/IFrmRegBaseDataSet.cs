using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.OPManage;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    /// 挂号基础数据设置界面接口类
    /// </summary>
    public interface IFrmRegBaseDataSet : IBaseView
    {
        /// <summary>
        /// 当前挂号类型对象
        /// </summary>
        OP_RegType CurRegtype { get; set; }

        /// <summary>
        /// 挂号类型对应的收费项目
        /// </summary>
        DataTable DtRegTypeFees { get; }

        /// <summary>
        /// 获取所有挂号类别
        /// </summary>
        /// <param name="regTypeList">挂号类别</param>
        void loadRegTypes(DataTable  regTypeList);

        /// <summary>
        /// 收费项目选项卡数据源
        /// </summary>
        /// <param name="dtFeeSource">收费项目选项卡数据</param>
        void SetRegItemShowCard(DataTable dtFeeSource);

        /// <summary>
        /// 获取收费项目数据源
        /// </summary>
        /// <param name="dt">DataTable</param>
        void GetDgRegItemFees(DataTable dt);

        /// <summary>
        /// 设置按钮是否可用
        /// </summary>
        /// <param name="enabled">true可用false不可用</param>
        void setBarEnabled(bool enabled);
    }
}
