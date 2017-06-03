using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MIManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_MIInterface.ObjectModel
{
    [Serializable]
    public class CommonMethod : AbstractObjectModel
    {
        /// <summary>
        /// 获取当前交易记录ID
        /// </summary>
        /// <param name="empid"></param>
        /// <returns></returns>
        public int GetPayRecordId(int miid,string tradno )
        {
            int payRecordid = 0;
            List<MI_MedicalInsurancePayRecord> medicalInsurancePayRecords= NewObject<MI_MedicalInsurancePayRecord>().getlist<MI_MedicalInsurancePayRecord>(" MIID=" + miid + " and TradeNO='"+ tradno + "'");
            if (medicalInsurancePayRecords != null && medicalInsurancePayRecords.Count > 0)
            {
                payRecordid = medicalInsurancePayRecords[0].ID;
            }
            return payRecordid;
        }
    }
}
