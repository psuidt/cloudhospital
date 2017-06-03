using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS_Entity.MIManage;

namespace HIS_MIInterface.Interface.BaseClass
{
    public  class AbstractMIAction<T,W> : AbstractAction, MIMzInterface, MIZyInterface, MIMatchInterface
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
                    _hisDao = (T)Activator.CreateInstance(typeof(T), null);
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


        #region 门诊
        public virtual ResultClass MZ_CancelCharge(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_CancelRegister(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_CancelRegisterCommit(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_CancelChargeCommit(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_Charge(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_UpdateRegister(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_DownloadzyPatFee(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_GetCardInfo(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_GetPersonInfo(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_LoadFeeDetailByTicketNum(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_PreviewCharge(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_PreviewRegister(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_Register(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_CommitTradeState(InputClass inputClass)
        {
            throw new NotImplementedException();
        }
        public virtual ResultClass MZ_RePrintInvoice(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass MZ_PrintInvoice(InputClass inputClass)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// HIS自行打印发票
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public virtual ResultClass MZ_HISPrintInvoice(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        ///// <summary>
        ///// 回滚门诊医保登记
        ///// </summary>
        ///// <returns></returns>
        //public virtual ResultClass MZ_RegisterRollBack(InputClass inputClass)//int registerId, string serialNO);
        //{
        //    throw new NotImplementedException();
        //}

        public virtual ResultClass MZ_UploadzyPatFee(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据医保交易状态更新交易记录
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public virtual ResultClass Mz_UpdateTradeRecord(InputClass inputClass)
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
        #region 匹配
        public virtual ResultClass M_DeleteMatchContent(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass M_DownLoadDrugContent(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass M_DownLoadHospContent(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass M_DownLoadItemContent(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass M_DownLoadMaterialsContent(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass M_JudgeSingleContentMatch(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass M_ResetMatchContent(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass M_UpLoadDept(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass M_UploadMatchContent(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass M_UpLoadStaff(InputClass inputClass)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 住院
        public virtual ResultClass ZY_AlterRegister(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass ZY_CancelCharge(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass ZY_CancelRegister(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass ZY_Charge(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass ZY_DeleteFeeDetail(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass ZY_DownloadzyPatFee(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass ZY_GetHospitalInfo(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass ZY_GetPatientInfo(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass ZY_LevHosCancel(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass ZY_LevHosRegister(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass ZY_PreviewCharge(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass ZY_Register(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass ZY_UpLoadDiagnosis(InputClass inputClass)
        {
            throw new NotImplementedException();
        }

        public virtual ResultClass ZY_UploadzyPatFee(InputClass inputClass)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
