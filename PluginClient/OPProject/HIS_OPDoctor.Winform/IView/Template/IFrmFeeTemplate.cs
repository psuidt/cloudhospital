using System.Collections.Generic;
using System.Data;
using DevComponents.AdvTree;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.ClinicManage;

namespace HIS_OPDoctor.Winform.IView
{
    /// <summary>
    /// 费用模板界面接口
    /// </summary>
    public interface IFrmFeeTemplate : IBaseView
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
        /// 西药处方树节点集合
        /// </summary>
        NodeCollection WestDrugNode { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        string StrDocName { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        string StrDeptName { get; set; }

        /// <summary>
        /// 模板类型
        /// </summary>
        string MouldType { get; set; }

        /// <summary>
        /// 绑定录入ShowCard
        /// </summary>
        /// <param name="dtFeeInfo">费用联动信息</param>
        void BindFeeInfoCard(DataTable dtFeeInfo);

        /// <summary>
        /// 绑定费用模板网格
        /// </summary>
        /// <param name="dtFee">收费项目数据</param>
        void BindDgFee(DataTable dtFee);

        /// <summary>
        /// 选中西药处方树节点
        /// </summary>
        Node SelectWestDrugNode { get; set; }

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
    }    
}
