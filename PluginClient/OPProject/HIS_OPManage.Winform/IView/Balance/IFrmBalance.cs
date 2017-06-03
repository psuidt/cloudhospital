using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.ClinicManage;
using HIS_Entity.OPManage;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    /// 门诊收费界面接口类
    /// </summary>
    public interface IFrmBalance : IBaseView
    {
        /// <summary>
        /// 当前操作病人对象
        /// </summary>
        OP_PatList CurPatList { get; set; }

        /// <summary>
        /// 0收费 1退费
        /// </summary>
        int BalanceMode { get; set; }

        /// <summary>
        /// 获取卡类型
        /// </summary>
        /// <param name="dtCardType">卡类型</param>
        void LoadCardType(DataTable dtCardType);

        /// <summary>
        /// 获取病人类型
        /// </summary>
        /// <param name="dtPatType">病人类型</param>
        void LoadPatType(DataTable dtPatType);

        /// <summary>
        /// 绑定开方科室
        /// </summary>
        /// <param name="dtPrescDepts">科室数据</param>
        void LoadPrescDepts(DataTable dtPrescDepts);

        /// <summary>
        /// 绑定开方医生
        /// </summary>
        /// <param name="dtPrescDoctors">开方医生数据</param>
        void LoadPrescDoctors(DataTable dtPrescDoctors);

        /// <summary>
        /// 年龄
        /// </summary>
        int Age { get; set; }

        /// <summary>
        /// 年龄单位
        /// </summary>
        string AgeUnit { get; set; }

        /// <summary>
        /// 当前票据号
        /// </summary>
        string CurInvoiceNO { get; set; }

        /// <summary>
        /// 设置处方费用录入选项卡数据源
        /// </summary>
        /// <param name="dtFeeSource">费用录入数据源</param>
        void BindDrugItemShowCard(DataTable dtFeeSource);

        /// <summary>
        /// 绑定疾病诊断
        /// </summary>
        /// <param name="dtDisease">疾病诊断数据</param>
        void LoadDiagnose(DataTable dtDisease);

        /// <summary>
        /// 获取界面处方数据
        /// </summary>
       DataTable  Prescriptions { get; }

        /// <summary>
        /// 绑定处方数据
        /// </summary>
        /// <param name="dt">处方数据</param>
        void SetPresDataSource(DataTable dt);

        /// <summary>
        /// 获取选择的结算的病人类型Id
        /// </summary>
        int CostPatTypeid { get; set; }

        /// <summary>
        /// 获取开方科室ID
        /// </summary>
        int GetPresDeptid { get; set; }

        /// <summary>
        /// 获取开方医生ID
        /// </summary>
        int GetPresDocid { get; set; }

        /// <summary>
        /// 所有处方总金额
        /// </summary>
       decimal   AllPrescriptionTotalFee{get;set;}
      
        /// <summary>
        /// 开方医生姓名
        /// </summary>
        string GetPresDocName { get; }

        /// <summary>
        /// 开方科室名称
        /// </summary>
        string GetPresDeptName { get; }

        /// <summary>
        /// 诊疗卡号
        /// </summary>
        string CardNo { get; set; }

        /// <summary>
        /// 初始化卡类型下拉项
        /// </summary>
        void SetCardTypeSelIndex();

        /// <summary>
        /// 设置只读
        /// </summary>
        /// <param name="readonlyType">只读类型</param>
        void SetReadOnly(ReadOnlyType readonlyType);

        /// <summary>
        /// 设置卡号焦点
        /// </summary>
        void CardNoFocus();

        /// <summary>
        /// 分方后设置当前行
        /// </summary>
        /// <param name="rowIndex">当前行索引</param>
        void SetDgPresfocus(int rowIndex);

        /// <summary>
        /// 设置网格颜色
        /// </summary>
        void SetGridColor();

        /// <summary>
        /// 上次收费信息
        /// </summary>
        string StrPayInfo { get; set; }

        /// <summary>
        /// 设置上次收费信息面板是否可见
        /// </summary>
        bool SetexPlLastPayExpanded {set; }

        /// <summary>
        /// 是否是医生处方
        /// </summary>
        int IsAddOpdoctor { get; set; }

        /// <summary>
        /// 设置按钮是否可用
        /// </summary>
        /// <param name="enabled">true可用false不可用</param>
        void SetBarEnabled(bool enabled);

        /// <summary>
        /// 半闭窗体
        /// </summary>
        void CloseBalanceForm();

        /// <summary>
        /// 医生诊断对象
        /// </summary>
        /// <typeparam name="OPD_DiagnosisRecord"></typeparam>
        /// <param name=""></param>
         List<OPD_DiagnosisRecord> DiagnosisList{ get; set; }

        /// <summary>
        /// 医保刷卡显示信息
        /// </summary>
         string MedicardReadInfo { get; set; }

        /// <summary>
        /// 显示医保刷卡显示信息
        /// </summary>
         string SetMedicardReadInfo { set; }
    }

    /// <summary>
    /// 只读枚举
    /// </summary>
    public enum ReadOnlyType
    {
        药品可拆零 = 1,
        药品不可拆零 = 2,
        项目 = 3,
        中草药 = 4,
        新开=5,
        不能修改 = 6,
        全部只读=7       
    }
}
