using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS_Entity.IPManage;

namespace HIS_BGInterface.Dao
{
    public interface IHISBloodGlucoseDao
    {
        /// <summary>
        /// 1.获取在院患者主索引基本信息
        /// </summary>
        /// <returns></returns>
        DataTable GetInPatientBaseInfo(int iWorkId);

        /// <summary>
        /// 2.获取在院患者住院信息
        /// </summary>
        /// <returns></returns>
        DataTable GetInPatientHospitalizationInfo(int iWorkId);


        /// <summary>
        /// 3.获取在用科室信息
        /// </summary>
        /// <returns></returns>
        DataTable GetWorkDeptInfo(int iWorkId);


        /// <summary>
        /// 4.获取在用病区信息
        /// </summary>
        /// <returns></returns>
        DataTable GetWorkAreaInfo(int iWorkId);


        /// <summary>
        /// 5.获取在用床位信息
        /// </summary>
        /// <returns></returns>
        DataTable GetWorkBedInfo(int iWorkId);


        /// <summary>
        /// 6.获取在用用户信息
        /// </summary>
        /// <returns></returns>
        DataTable GetWorkUserInfo(int iWorkId);

        /// <summary>
        /// 获取血糖记录ID
        /// </summary>
        /// <param name="record">血糖记录</param>
        /// <returns></returns>
        int GetPluRecordID(IP_PluRecord record);

    }
}
