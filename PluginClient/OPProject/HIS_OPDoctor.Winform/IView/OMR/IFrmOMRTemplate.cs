using System.Collections.Generic;
using DevComponents.AdvTree;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.ClinicManage;

namespace HIS_OPDoctor.Winform.IView
{
    /// <summary>
    /// 病历模板界面接口
    /// </summary>
    public interface IFrmOMRTemplate : IBaseView
    {
        /// <summary>
        /// 模板级别
        /// </summary>
        int ModilLevel { get; set; }

        /// <summary>
        /// 使用的树
        /// </summary>
        AdvTree UseTree { get; set; }

        /// <summary>
        /// 模板头类
        /// </summary>
        List<OPD_OMRTmpHead> ListHead { get; set; }

        /// <summary>
        /// 树节点集合
        /// </summary>
        NodeCollection SelectedNode { get; set; }

        /// <summary>
        /// 模板类型
        /// </summary>
        string mouldType { get; set; }     
           
        /// <summary>
        /// 选中的药品节点
        /// </summary>
        Node SelectOMRNode { get; set; }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="tree">树控件</param>
        void AddNode(Node node, AdvTree tree);

        /// <summary>
        /// 编辑节点
        /// </summary>
        /// <param name="nodeName">节点名称</param>
        /// <param name="tree">树控件</param>
        void EditNode(string nodeName, AdvTree tree);

        /// <summary>
        /// 树节点级别
        /// </summary>
        int TreeLevel { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        string StrDocName { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        string StrDeptName { get; set; }
    }    
}
