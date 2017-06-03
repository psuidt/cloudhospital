using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.IPManage;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 病人基本信息
    /// </summary>
    public interface IPatientInfo : IBaseView
    {
        /// <summary>
        /// 获取病人国籍列表
        /// </summary>
        /// <param name="nationalityDt">国籍列表</param>
        void Bind_txtPatNationality(DataTable nationalityDt);

        /// <summary>
        /// 获取病人民族列表
        /// </summary>
        /// <param name="nationDt">民族列表</param>
        void Bind_txtNation(DataTable nationDt);

        /// <summary>
        /// 获取病人职业列表
        /// </summary>
        /// <param name="jodListDt">职业列表</param>
        void Bind_txtPatJob(DataTable jodListDt);

        /// <summary>
        /// 获取病人教育程度列表
        /// </summary>
        /// <param name="culturalLevelDt">教育程度列表</param>
        void Bind_txtCulturalLevel(DataTable culturalLevelDt);

        /// <summary>
        /// 获取病人婚姻状况列表
        /// </summary>
        /// <param name="matrimonyDt">婚姻状况列表</param>
        void Bind_Matrimony(DataTable matrimonyDt);

        /// <summary>
        /// 获取病人与联系人关系列表
        /// </summary>
        /// <param name="relationDt">病人与联系人关系列表</param>
        void Bind_txtRelation(DataTable relationDt);

        /// <summary>
        /// 获取病人性别列表
        /// </summary>
        /// <param name="sexDt">性别列表</param>
        void Bind_PatSex(DataTable sexDt);

        /// <summary>
        /// 获取病人入院情况列表
        /// </summary>
        /// <param name="enterSituationDt">入院情况列表</param>
        void Bind_EnterSituation(DataTable enterSituationDt);

        /// <summary>
        /// 获取病人地区编码列表
        /// </summary>
        /// <param name="regionCodeDt">地区编码列表</param>
        void Bind_RegionCode(DataTable regionCodeDt);

        /// <summary>
        /// 获取病人来源列表
        /// </summary>
        /// <param name="sourceWayDt">病人来源列表</param>
        void Bind_txtSourceWay(DataTable sourceWayDt);

        /// <summary>
        /// 获取病区列表
        /// </summary>
        /// <param name="enterWardIDDt">病区列表</param>
        void Bind_txtEnterWardID(DataTable enterWardIDDt);

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="deptListDt">科室列表</param>
        void Bind_cboDeptList(DataTable deptListDt);

        /// <summary>
        /// 绑定病人类型列表
        /// </summary>
        /// <param name="patTypeListDt">病人列表</param>
        void Bind_cboPatType(DataTable patTypeListDt);

        /// <summary>
        /// 绑定诊断列表
        /// </summary>
        /// <param name="diseaseDt">诊断列表</param>
        void Bind_txtPatDisease(DataTable diseaseDt);

        /// <summary>
        /// 绑定医生列表
        /// </summary>
        /// <param name="currDoctorDt">医生列表</param>
        void Bind_txtCurrDoctor(DataTable currDoctorDt);

        /// <summary>
        /// 绑定护士列表
        /// </summary>
        /// <param name="currNurseDt">护士列表</param>
        void Bind_txtCurrNurse(DataTable currNurseDt);

        /// <summary>
        /// 住院病人信息
        /// </summary>
        IP_PatientInfo PatientInfo { get; set; }

        /// <summary>
        /// 住院登记记录信息
        /// </summary>
        IP_PatList PatList { get; set; }

        /// <summary>
        /// 关闭新病人入院信息录入界面
        /// </summary>
        void FormClose();

        /// <summary>
        /// 是否为新入院病人
        /// </summary>
        bool IsNewPatient { get; set; }

        /// <summary>
        /// 获取选择的卡类型
        /// </summary>
        int GetCardTypeID { get; }

        /// <summary>
        /// 获取卡号码
        /// </summary>
        string CardNo { get; }

        /// <summary>
        /// 是否为住院证登记
        /// </summary>
        bool InpatientReg { get; set; }

        /// <summary>
        /// 住院证预交金预交金金额
        /// </summary>
        decimal Deposit { get; set; }

        /// <summary>
        /// 预交金票据号
        /// </summary>
        string InvoiceNO { get; }

        /// <summary>
        /// 预交金支付方式
        /// </summary>
        string PayType { get; }

        /// <summary>
        /// 预交金缴纳金额
        /// </summary>
        decimal TotalFee { get; }

        /// <summary>
        /// 绑定卡类型列表
        /// </summary>
        /// <param name="dtCardType">住院登记卡类型列表</param>
        void LoadCardType(DataTable dtCardType);

        /// <summary>
        /// 设置界面控件是否可用
        /// </summary>
        void SetPatFrmControlEnabled();

        /// <summary>
        /// 读卡、办卡后设置部分控件是否可用
        /// </summary>
        void SetCardControlEnabled();

        /// <summary>
        /// 绑定预交金金额
        /// </summary>
        void Bind_Deposit();

        /// <summary>
        /// 设置预交金票据号
        /// </summary>
        /// <param name="billNumber">预交金票据号</param>
        void SetBillNumber(string billNumber);

        /// <summary>
        /// 绑定预交金支付方式
        /// </summary>
        /// <param name="patMethodDt">支付方式列表</param>
        void Binding_PaymentMethod(DataTable patMethodDt);

        /// <summary>
        /// 设置是否可收取预交金
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="isLast">首次加载</param>
        void SetInvoiceControlEnabled(bool status, bool isLast);
    }
}