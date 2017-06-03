using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using static HIS_OPDoctor.Winform.ViewForm.MedicalApply.FrmMedicalApply;

namespace HIS_OPDoctor.Winform.ViewForm.MedicalApply
{
    /// <summary>
    /// 医技申请界面接口
    /// </summary>
    public interface IFrmMedicalApply : IBaseView
    {
        /// <summary>
        /// 0门珍 1住院
        /// </summary>
        int SystemType { get; set; }

        /// <summary>
        /// 化验属性Json对象
        /// </summary>
        TestJson Test { get; set; }

        /// <summary>
        /// 检查属性Json对象
        /// </summary>
        CheckJson Check { get; set; }

        /// <summary>
        /// 当前项目大类值
        /// </summary>
        int ExamClass { get; set; }

        /// <summary>
        /// 保存项目数据
        /// </summary>
        DataTable SaveItemData { get; set; }

        /// <summary>
        /// 申请头id
        /// </summary>
        string ApplyHeadID { get; set; }

        /// <summary>
        /// 病人ID
        /// </summary>
       int PatListID { get; set; }

        /// <summary>
        /// 申请单状态
        /// </summary>
        string ApplyStatu { get; set; }

        /// <summary>
        /// 申请类型
        /// </summary>
        string ApplyType { get; set; }

        /// <summary>
        /// 是否退费
        /// </summary>
        string IsReturns { get; set; }

        /// <summary>
        /// 绑定医技申请科室
        /// </summary>
        /// <param name="dtDept">申请科室数据</param>
        void BindExecDept(DataTable dtDept);

        /// <summary>
        /// 绑定项目分类
        /// </summary>
        /// <param name="dtType">项目分类数据</param>
        void BindExecType(DataTable dtType);

        /// <summary>
        /// 绑定项目信息
        /// </summary>
        /// <param name="dtItem">项目数据</param>
        void BindExecItem(DataTable dtItem);

        /// <summary>
        /// 设置网格数据
        /// </summary>
        /// <param name="dtItem">项目数据</param>
        void SetExecItem(DataTable dtItem);

        /// <summary>
        /// 绑定检验标本
        /// </summary>
        /// <param name="dtSample">标本数据</param>
        void BindSample(DataTable dtSample);

        /// <summary>
        /// 获取表头和明细
        /// </summary>
        /// <param name="dtHeadDetail">表头和明细数据</param>
        void BindHeadDetail(DataTable dtHeadDetail);
    }
}
