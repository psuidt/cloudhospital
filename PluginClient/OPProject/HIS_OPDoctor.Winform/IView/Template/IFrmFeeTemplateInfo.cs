using DevComponents.AdvTree;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.ClinicManage;

namespace HIS_OPDoctor.Winform.IView.Template
{
    /// <summary>
    /// 费用模板信息窗体
    /// </summary>
    public interface IFrmFeeTemplateInfo : IBaseView
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
        /// 是否模板节点
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
        /// 选中节点
        /// </summary>
        Node SelectNode { get; set; }
    }
}
