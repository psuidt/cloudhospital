using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevComponents.AdvTree;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.ClinicManage;

namespace HIS_OPDoctor.Winform.IView.Template
{
    /// <summary>
    /// 处方模板
    /// </summary>
    public interface IFrmORMTemplateInfo : IBaseView
    {
        /// <summary>
        /// 模板头类
        /// </summary>
        OPD_OMRTmpHead Head { get; set; }

        /// <summary>
        /// 是否模板分类
        /// </summary>
        bool ResNode { get; set; }

        /// <summary>
        /// 模板是否选中
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
