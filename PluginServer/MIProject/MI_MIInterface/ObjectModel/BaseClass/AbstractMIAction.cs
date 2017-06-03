using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HIS_Entity.MIManage;

namespace MI_MIInterface.ObjectModel.BaseClass
{
    public class AbstractMIAction<T, W> : AbstractAction,MIMzInterface, MIZyInterface, MIMatchInterface
    {
        private T _hisDao = default(T);
        /// <summary>
        /// 操作HIS数据库的对象
        /// </summary>
        protected T hisDao
        {
            get
            {
                if (_hisDao == null)
                {
                    _hisDao = NewDao<T>();
                        //(T)Activator.CreateInstance(typeof(T), null);
                }
                return _hisDao;
            }
        }

        private W _ybInterfaceDao = default(W);
        /// <summary>
        /// 操作医保接口的对象
        /// </summary>
        protected W ybInterfaceDao
        {
            get
            {
                if (_ybInterfaceDao == null)
                {
                    _ybInterfaceDao = (W)Activator.CreateInstance(typeof(W), null);
                }
                return _ybInterfaceDao;
            }
        }


        //protected override void BeginInitData()
        //{
        //    //(hisDao as AbstractHISDao).BeginInitData(ReadParam());
        //    //(ybInterfaceDao as AbstractMIInterfaceDao).BeginInitData(ReadParam());
        //}

        //protected override void EndInitData()
        //{
        //    //(hisDao as AbstractHISDao).EndInitData();
        //    //(ybInterfaceDao as AbstractMIInterfaceDao).EndInitData();
        //}

        public override string GetRemoteCommParaDescript()
        {
            throw new NotImplementedException();
        }

        #region 门诊接口
        public virtual ResultClass MZ_GetCardInfo(InputClass inputClass)//string sCardNo)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_GetPersonInfo(InputClass inputClass)//string sCardNo)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_PreviewRegister(InputClass inputClass)//MI_Register register)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_Register(InputClass inputClass)//int registerId, string serialNO)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_UpdateRegister(InputClass inputClass)
        { throw new NotImplementedException(); }
            
        public virtual ResultClass Mz_CancelRegister(InputClass inputClass)//string serialNO)
        {
            throw new NotImplementedException();
        }
        public virtual ResultClass MZ_CancelRegisterCommit(InputClass inputClass)//string serialNO)
        {
            throw new NotImplementedException();
        }
        public virtual ResultClass MZ_PreviewCharge(InputClass inputClass)//TradeData tradedata)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_Charge(InputClass inputClass)//int tradeRecordId, string fph)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_CancelCharge(InputClass inputClass)//string fph)
        {
            throw new NotImplementedException();
        }
        public virtual ResultClass MZ_CancelChargeCommit(InputClass inputClass)//string serialNO)
        {
            throw new NotImplementedException();
        }
        public virtual ResultClass MZ_UploadzyPatFee(InputClass inputClass)//)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_DownloadzyPatFee(InputClass inputClass)//)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_LoadFeeDetailByTicketNum(InputClass inputClass)//)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass Mz_UpdateTradeRecord(InputClass inputClass)//)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据就诊号 获取挂号交易流水号
        /// </summary>
        /// <param name="sSerialNO">挂号就诊号</param>
        /// <returns></returns>
        public virtual ResultClass Mz_GetRegisterTradeNo(string sSerialNO)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 住院接口
        public virtual void Zy_GetHospitalInfo()
        {
            throw new NotImplementedException();
        }

        public virtual void Zy_GetPatientInfo()
        {
            throw new NotImplementedException();
        }

        public virtual void Zy_Register()
        {
            throw new NotImplementedException();
        }

        public virtual void Zy_AlterRegister()
        {
            throw new NotImplementedException();
        }

        public virtual void Zy_CancelRegister()
        {
            throw new NotImplementedException();
        }

        public virtual void Zy_PreviewCharge()
        {
            throw new NotImplementedException();
        }

        public virtual void Zy_Charge()
        {
            throw new NotImplementedException();
        }

        public virtual void Zy_CancelCharge()
        {
            throw new NotImplementedException();
        }

        public virtual bool Zy_UploadzyPatFee(int patientId,int iFlag)
        {
            throw new NotImplementedException();
        }

        public virtual void Zy_DownloadzyPatFee()
        {
            throw new NotImplementedException();
        }

        public virtual void Zy_DeleteFeeDetail()
        {
            throw new NotImplementedException();
        }

        public virtual void Zy_UpLoadDiagnosis()
        {
            throw new NotImplementedException();
        }

        public virtual void Zy_LevHosRegister()
        {
            throw new NotImplementedException();
        }

        public virtual void Zy_LevHosCancel()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 匹配接口
        public virtual bool M_DownLoadDrugContent(int ybId, int workId)
        {
            throw new NotImplementedException();
        }

        public virtual bool M_DownLoadItemContent(int ybId, int workId)
        {
            throw new NotImplementedException();
        }

        public virtual bool M_DownLoadMaterialsContent(int ybId, int workId)
        {
            throw new NotImplementedException();
        }

        public virtual bool M_JudgeSingleContentMatch()
        {
            throw new NotImplementedException();
        }

        public virtual bool M_UploadMatchContent()
        {
            throw new NotImplementedException();
        }

        public virtual bool M_ResetMatchContent()
        {
            throw new NotImplementedException();
        }

        public virtual bool M_DeleteMatchContent()
        {
            throw new NotImplementedException();
        }

        public virtual bool M_DownLoadHospContent()
        {
            throw new NotImplementedException();
        }

        public virtual bool M_UpLoadDept()
        {
            throw new NotImplementedException();
        }

        public virtual bool M_UpLoadStaff()
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
