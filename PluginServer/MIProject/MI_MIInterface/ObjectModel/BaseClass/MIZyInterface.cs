using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_MIInterface.ObjectModel.BaseClass
{
    /// <summary>
    /// 医保报销住院接口处理
    /// </summary>
    public interface MIZyInterface
    {
        /// <summary>
        /// 医疗机构信息
        /// </summary>
        void Zy_GetHospitalInfo();
        /// <summary>
        /// 得到病人信息数据
        /// </summary>
        /// <returns></returns>
        void Zy_GetPatientInfo();
        /// <summary>
        /// 入院
        /// </summary>
        /// <returns></returns>
        void Zy_Register();
        /// <summary>
        /// 修改入院信息
        /// </summary>
        /// <returns></returns>
        void Zy_AlterRegister();
        /// <summary>
        /// 取消入院
        /// </summary>
        /// <returns></returns>
        void Zy_CancelRegister();
        /// <summary>
        /// 上传病人费用
        /// </summary>
        /// <returns></returns>
        bool Zy_UploadzyPatFee(int patientId,int iFlag);
        /// <summary>
        /// 下载病人费用数据
        /// </summary>
        /// <returns></returns>
        void Zy_DownloadzyPatFee();
        /// <summary>
        /// 删除社保处方
        /// </summary>
        void Zy_DeleteFeeDetail();       
        /// <summary>
        /// 预算
        /// </summary>
        /// <returns></returns>
        void Zy_PreviewCharge();
        /// <summary>
        /// 结算
        /// </summary>
        /// <returns></returns>
        void Zy_Charge();
        /// <summary>
        /// 取消出院结算
        /// </summary>
        /// <returns></returns>
        void Zy_CancelCharge();
        

        ////上传门诊处方
        //void UpLoadMzFeeDetail();

        //上传多诊断
        void Zy_UpLoadDiagnosis();

        //出院登记
        void Zy_LevHosRegister();

        //出院登记撤销
        void Zy_LevHosCancel();
    }
}
