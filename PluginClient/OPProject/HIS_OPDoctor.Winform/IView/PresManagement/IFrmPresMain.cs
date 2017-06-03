using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.ClinicManage;

namespace HIS_OPDoctor.Winform.IView
{
    /// <summary>
    /// 诊疗管理主界面接口
    /// </summary>
    public interface IFrmPresMain : IBaseView
    {
        #region 病人信息
        /// <summary>
        /// 绑定医生所在的挂号科室
        /// </summary>
        /// <param name="dt">科室信息</param>
        /// <param name="deptId">当前登陆科室</param>
        void BindDocInDept(DataTable dt, int deptId);

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="dtPatientList">病人列表</param>
        void BindPatientList(DataTable dtPatientList);

        /// <summary>
        /// 绑定病人信息面板
        /// </summary>
        /// <param name="dtPatient">病人信息</param>
        /// <param name="dtDiseases">病人诊断信息</param>
        void BindPatientInfo(DataTable dtPatient,DataTable dtDiseases);

        /// <summary>
        /// 绑定诊断信息
        /// </summary>
        /// <param name="diseaseNames">诊断名称以、号隔开</param>
        void ShowDiseaseInfo(string diseaseNames);

        /// <summary>
        /// 绑定药房科室
        /// </summary>
        /// <param name="dt">药房数据集</param>
        void BindDrugStoreRoom(DataTable dt);

        /// <summary>
        /// 绑定申请头表信息
        /// </summary>
        /// <param name="dt">申请头表数据集</param>
        void BindApplyHead(DataTable dt);

        /// <summary>
        /// 设置处方按钮可用状态
        /// </summary>
        /// <param name="isvalid">true可用false不可用</param>
        void EnablePresButton(bool isvalid);

        /// <summary>
        /// 科室id
        /// </summary>
        int DeptId { get; set; }
        #endregion

        #region 常用诊断
        /// <summary>
        /// 绑定常用诊断表格
        /// </summary>
        /// <param name="dtCommonDiagnosis">常用诊断信息</param>
        void BindCommonDiagnosisGrid(DataTable dtCommonDiagnosis);
        #endregion

        #region 模板
        /// <summary>
        /// 模板级别
        /// </summary>
        int TemplateLevel { get; set; }

        /// <summary>
        /// 模板头类
        /// </summary>
        List<OPD_PresMouldHead> TemplateListHead { get; set; }

        /// <summary>
        /// 绑定模板明细
        /// </summary>
        /// <param name="dt">模板明细数据</param>
        void BindTemplateDetailGrid(DataTable dt);

        /// <summary>
        /// 一键复制后刷新处方控件
        /// </summary>
        /// <param name="flag">true一键复制成功</param>
        void RefreshOneCopyControl(bool flag);

        /// <summary>
        /// 绑定病历信息
        /// </summary>
        /// <param name="modelOMR">病历实体</param>
        void BindOMRInfo(OPD_MedicalRecord modelOMR);

        /// <summary>
        /// 绑定符号类型下拉框
        /// </summary>
        /// <param name="dtSymbolType">符号类型</param>
        /// <param name="dtSymbolContent">符号内容</param>
        void BindOMRSymbolComboBox(DataTable dtSymbolType, DataTable dtSymbolContent);

        /// <summary>
        /// 刷新模板树
        /// </summary>
        void FreshOMRTplTree();
        
        /// <summary>
        /// 模板级别
        /// </summary>
        int OMRTemplateLevel
        {
            get;
            set;
        }

        /// <summary>
        /// 模板头列表类
        /// </summary>
        List<OPD_OMRTmpHead> OMRTemplateListHead
        {
            get;
            set;
        }

        /// <summary>
        /// 绑定模板内容
        /// </summary>
        /// <param name="tmpDetail">模板模型</param>
        void BindTplContentControl(OPD_OMRTmpDetail tmpDetail);
        #endregion

        #region 异步加载
        /// <summary>
        /// 绑定处方控件数据
        /// </summary>
        void BindControlData();

        /// <summary>
        /// 完成异步绑定处方数据源
        /// </summary>
        void BindControlDataComplete();
        #endregion

        int CanPrintChargedPres { get; set; }
    }
}
