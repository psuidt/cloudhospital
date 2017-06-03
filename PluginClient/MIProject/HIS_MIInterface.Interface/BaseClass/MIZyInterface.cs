using HIS_Entity.MIManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_MIInterface.Interface.BaseClass
{
    /// <summary>
    /// 医保报销住院接口处理
    /// </summary>
    public interface MIZyInterface
    {
        /// <summary>
        /// 医疗机构信息
        /// </summary>
        ResultClass ZY_GetHospitalInfo(InputClass inputClass); //);
        /// <summary>
        /// 得到病人信息数据
        /// </summary>
        /// <returns></returns>
        ResultClass ZY_GetPatientInfo(InputClass inputClass); //);
        /// <summary>
        /// 入院
        /// </summary>
        /// <returns></returns>
        ResultClass ZY_Register(InputClass inputClass); //);
        /// <summary>
        /// 修改入院信息
        /// </summary>
        /// <returns></returns>
        ResultClass ZY_AlterRegister(InputClass inputClass); //);
        /// <summary>
        /// 取消入院
        /// </summary>
        /// <returns></returns>
        ResultClass ZY_CancelRegister(InputClass inputClass); //);
        /// <summary>
        /// 上传病人费用
        /// </summary>
        /// <returns></returns>
        ResultClass ZY_UploadzyPatFee(InputClass inputClass); //int patientId);
        /// <summary>
        /// 下载病人费用数据
        /// </summary>
        /// <returns></returns>
        ResultClass ZY_DownloadzyPatFee(InputClass inputClass); //);
        /// <summary>
        /// 删除社保处方
        /// </summary>
        ResultClass ZY_DeleteFeeDetail(InputClass inputClass); //);
        /// <summary>
        /// 预算
        /// </summary>
        /// <returns></returns>
        ResultClass ZY_PreviewCharge(InputClass inputClass); //);
        /// <summary>
        /// 结算
        /// </summary>
        /// <returns></returns>
        ResultClass ZY_Charge(InputClass inputClass); //);
        /// <summary>
        /// 取消出院结算
        /// </summary>
        /// <returns></returns>
        ResultClass ZY_CancelCharge(InputClass inputClass); //);

        //上传多诊断
        ResultClass ZY_UpLoadDiagnosis(InputClass inputClass); //);

        //出院登记
        ResultClass ZY_LevHosRegister(InputClass inputClass); //);

        //出院登记撤销
        ResultClass ZY_LevHosCancel(InputClass inputClass); //);
    }
}
