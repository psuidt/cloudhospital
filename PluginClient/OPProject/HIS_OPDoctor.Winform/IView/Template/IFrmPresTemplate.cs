using System.Collections.Generic;
using DevComponents.AdvTree;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.ClinicManage;

namespace HIS_OPDoctor.Winform.IView
{
    /// <summary>
    /// 处方模板界面接口
    /// </summary>
    public interface IFrmPresTemplate : IBaseView
    {
        /// <summary>
        /// 模板级别
        /// </summary>
        int IntModilLevel { get; set; }

        /// <summary>
        /// 使用的树控件
        /// </summary>
        AdvTree UseTree { get; set; }

        /// <summary>
        /// 模板头类
        /// </summary>
        List<OPD_PresMouldHead> ListHead { get; set; }

        /// <summary>
        /// 树节点集合
        /// </summary>
        NodeCollection WestDrugNode { get; set; }

        /// <summary>
        /// 选中的树节点
        /// </summary>
        Node SelectWestDrugNode { get; set; }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="node">树节点</param>
        /// <param name="tree">树控件</param>
        void AddNode(Node node,AdvTree tree);

        /// <summary>
        /// 编辑树节点
        /// </summary>
        /// <param name="nodeName">节点名称</param>
        /// <param name="tree">树控件</param>
        void EditNode(string nodeName, AdvTree tree);

        /// <summary>
        /// 树节点级别
        /// </summary>
        int TreeLevel { get; set; }
    }    
}
