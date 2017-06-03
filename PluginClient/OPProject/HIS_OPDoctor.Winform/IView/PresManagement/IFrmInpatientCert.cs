using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.ClinicManage;

namespace HIS_OPDoctor.Winform.IView
{
    /// <summary>
    /// 住院证界面接口
    /// </summary>
    public interface IFrmInpatientCert : IBaseView
    {
        /// <summary>
        /// 病人id
        /// </summary>
        int PatId { get; set; }

        /// <summary>
        /// 会员id
        /// </summary>
        int MeId { get; set; }
        
        /// <summary>
        /// 病人信息数据
        /// </summary>
        DataTable DtPatient { get; set; }

        /// <summary>
        /// 绑定病人信息
        /// </summary>
        /// <param name="dtPatient">病人数据</param>
        /// <param name="strDisease">诊断字符串</param>
        void BindPatientInfo(DataTable dtPatient,string strDisease);

        /// <summary>
        /// 获取年龄
        /// </summary>
        /// <param name="age">年龄字符串</param>
        /// <returns>年龄</returns>
        string GetAge(string age);

        /// <summary>
        /// 绑定诊断Showcard
        /// </summary>
        /// <param name="dt">诊断数据</param>
        void BindDiseaseShowCard(DataTable dt);

        /// <summary>
        /// 绑定科室数据
        /// </summary>
        /// <param name="deptDt">科室数据</param>
        void BindDept(DataTable deptDt);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="result">结果</param>
        void SaveComplete(int result);

        /// <summary>
        /// 获取住院整信息
        /// </summary>
        /// <param name="inpatientReg">住院证数据</param>
        void GetInpatientReg(OPD_InpatientReg inpatientReg);
    }
}
