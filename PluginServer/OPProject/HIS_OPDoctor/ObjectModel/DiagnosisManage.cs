using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_OPDoctor.Dao;

namespace HIS_OPDoctor.ObjectModel
{
    /// <summary>
    /// 诊断管理对象
    /// </summary>
    [Serializable]
    public class DiagnosisManage : AbstractObjectModel
    {
        /// <summary>
        /// 加载诊断记录
        /// </summary>
        /// <param name="patListID">病人Id</param>
        /// <returns>诊断记录</returns>
        public DataTable LoadDiagnosisList(int patListID)
        {
            return NewDao<IOPDDao>().LoadDiagnosisList(patListID);
        }

        /// <summary>
        /// 添加诊断记录
        /// </summary>
        /// <param name="model">诊断记录实体</param>
        /// <returns>true成功</returns>
        public bool AddDiagnosis(OPD_DiagnosisRecord model)
        {
            return NewDao<IOPDDao>().AddDiagnosis(model);
        }

        /// <summary>
        /// 删除诊断记录
        /// </summary>
        /// <param name="diagnosisId">诊断记录Id</param>
        /// <param name="patListID">病人Id</param>
        /// <returns>true成功</returns>
        public bool DeleteDiagnosis(int diagnosisId, int patListID)
        {
            return NewDao<IOPDDao>().DeleteDiagnosis(diagnosisId, patListID);
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="dtDiagnosis">诊断记录表</param>
        public void SortDiagnosis(DataTable dtDiagnosis)
        {
            NewDao<IOPDDao>().SortDiagnosis(dtDiagnosis);
        }

        /// <summary>
        /// 添加常用诊断
        /// </summary>
        /// <param name="model">诊断记录模型</param>
        /// <returns>true成功</returns>
        public bool AddCommonDiagnosis(OPD_DiagnosisRecord model)
        {
            bool bRtn = false;
            bool bExist = NewDao<IOPDDao>().ExistCommonDianosis(model.PresDoctorID, model.DiagnosisName);

            //修改使用频次
            if (bExist)
            {
                bRtn = NewDao<IOPDDao>().UpdateCommonDiagnosis(model.PresDoctorID, model.DiagnosisName);
            }            
            else
            {
                //添加
                OPD_CommonDiagnosis commonModel = new OPD_CommonDiagnosis();
                commonModel.DiagnosisCode = model.DiagnosisCode;
                commonModel.DiagnosisName = model.DiagnosisName;
                commonModel.PYCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetSpellCode(model.DiagnosisName);
                commonModel.WBCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetWBCode(model.DiagnosisName);
                commonModel.RecordDoctorID = model.PresDoctorID;
                commonModel.Frequency = 1;
                commonModel.DelFlag = 0;
                this.BindDb(commonModel);
               int iRtn = commonModel.save();
                if (iRtn > 0)
                {
                    bRtn = true;
                }
                else
                {
                    bRtn = false;
                }
            }

            return bRtn;
        }

        /// <summary>
        /// 删除常用诊断
        /// </summary>
        /// <param name="commonDiagnosisID">常用诊断Id</param>
        /// <returns>true成功</returns>
        public bool DeleteCommonDianosis(int commonDiagnosisID)
        {
            OPD_CommonDiagnosis commonModel = (OPD_CommonDiagnosis)NewObject<OPD_CommonDiagnosis>().getmodel(commonDiagnosisID);
            commonModel.DelFlag = 1;
            this.BindDb(commonModel);
            int iRtn = commonModel.save();
            if (iRtn > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
