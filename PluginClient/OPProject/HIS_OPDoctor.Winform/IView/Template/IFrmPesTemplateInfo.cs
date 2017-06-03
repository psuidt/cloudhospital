using DevComponents.AdvTree;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.ClinicManage;

namespace HIS_OPDoctor.Winform.IView.Template
{
    /// <summary>
    /// 处方模板界面接口
    /// </summary>
    public interface IFrmPesTemplateInfo : IBaseView
    {
        /// <summary>
        /// 模板头类
        /// </summary>
        OPD_PresMouldHead Head { get; set; }

        /// <summary>
        /// 是否模板分类
        /// </summary>
        bool ResNode { get; set; }

        /// <summary>
        /// 是否模板
        /// </summary>
        bool ResTemp { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        string TempName { get; set; }

        /// <summary>
        /// 是否是新增数据
        /// </summary>
        bool IsNewFlag { get; set; }

        /// <summary>
        /// 选中的节点
        /// </summary>
        Node SelectNode { get; set; }
    }
}
