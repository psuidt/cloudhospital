using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace HIS_MIInterface.Dao
{
    public interface IZYInterface
    {
        DataTable Zy_GetMIPatient(int iMiType, int iDeptCode);

        DataTable Zy_GetPatientInfo(int PatientId, int iFeeType);

        bool Zy_UploadzyPatFee(int PatientId, int iFlag);

        bool Zy_ReSetzyPatFee(int feeRecordID);
        /// <summary>
        /// 获取医保类型
        /// </summary>
        /// <returns></returns>
        DataTable M_GetMIType(string sRoute);
        /// <summary>
        /// 获取门诊病人费用
        /// </summary>
        /// <param name="iPatType"></param>
        /// <param name="dDate"></param>
        /// <returns></returns>
        DataTable Mz_GetOutPatientFee(int iPatType, DateTime dDate);
    }
}
