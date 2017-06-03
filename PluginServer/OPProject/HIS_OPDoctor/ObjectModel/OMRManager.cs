using System.Collections.Generic;
using System.Linq;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_Entity.OPManage;

namespace HIS_OPDoctor.ObjectModel
{
    /// <summary>
    /// 门诊病历业务处理
    /// </summary>
    public class OMRManager : AbstractObjectModel
    {
        /// <summary>
        /// 保存病历
        /// </summary>
        /// <param name="omrModel">病历实体</param>
        /// <returns>true成功</returns>
        public bool SaveOMRData(OPD_MedicalRecord omrModel)
        {
            //查询病历表
            List<OPD_MedicalRecord> ormList = NewObject<OPD_MedicalRecord>().getlist<OPD_MedicalRecord>("PatListID="+ omrModel.PatListID);
            //查询挂号表
            OP_PatList op = (OP_PatList)NewObject<OP_PatList>().getmodel(omrModel.PatListID);
            if (ormList.Count > 0)
            {
                //主键赋值
                omrModel.MedicalRecordID = ormList[0].MedicalRecordID;
            }
            //会员Id
            omrModel.MemberID = op.MemberID;
            this.BindDb(omrModel);
            int iRtn = omrModel.save();
            if (iRtn > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 查询病历信息
        /// </summary>
        /// <param name="patListId">病人Id</param>
        /// <returns>病历信息实体</returns>
        public OPD_MedicalRecord GetPatientOMRData(int patListId)
        {
            OPD_MedicalRecord model = NewObject<OPD_MedicalRecord>().getlist<OPD_MedicalRecord>("PatListID = " + patListId).FirstOrDefault();
            return model;
        }
    }
}
