using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MI_MIInterface.ObjectModel.BaseClass;
using MI_MIInterface.ObjectModel.CustomAction;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MIManage;

namespace MI_MIInterface.ObjectModel
{
    public class ActionObjectFactory : AbstractObjectModel
    {
        AbstractAction abstractAction
        {
            get
            {
                string hospitalName= ObjectModel.Common.ActionMappingConfig.GetMedicalInsuranceData(1, "GetHospitalName", "HospitalName");
                switch (hospitalName)
                {
                    case "例子":
                        return NewObject<CustomAction.例子.CustomAction>();
                    case "北京":
                        return NewObject<CustomAction.beijing.CustomAction>();
                    default:
                        return null;
                }
            }
        }

        //public ActionObjectFactory()
        //{
            
        //}

        public bool M_UpdateMIlog(int ybId, int workId, int logType)
        {
            bool bResult = false;
            switch (logType)
            {
                case 1:
                    bResult=((MIMatchInterface)abstractAction).M_DownLoadDrugContent(ybId, workId);
                    break;
                case 2:
                    bResult = ((MIMatchInterface)abstractAction).M_DownLoadMaterialsContent(ybId, workId);
                    break;
                case 3:
                    bResult = ((MIMatchInterface)abstractAction).M_DownLoadItemContent(ybId, workId);
                    break;
                default:
                    bResult = false;
                    break;
            }
            return bResult;
        }
        public bool M_DownLoadHospContent()
        {
            return ((MIMatchInterface)abstractAction).M_DownLoadHospContent();
        }

        public bool Zy_UploadzyPatFee(int iPatientId,int iFlag)
        {
            return ((MIZyInterface)abstractAction).Zy_UploadzyPatFee(iPatientId, iFlag);
        }


        #region 门诊
        /// <summary>
        /// 调用读取卡片信息接口
        /// </summary>
        /// <param name="b"></param>
        /// <param name="s"></param>
        /// <param name="dt"></param>
        public ResultClass Mz_GetCardInfo(InputClass inputClass)
        {
            return ((MIMzInterface)abstractAction).MZ_GetCardInfo(inputClass);
        }

        /// <summary>
        /// 获取病人信息
        /// </summary>
        public ResultClass MZ_GetPersonInfo(InputClass inputClass)
        {
            return ((MIMzInterface)abstractAction).MZ_GetPersonInfo(inputClass);
        }

        /// <summary>
        /// 门诊预登记
        /// </summary>
        /// <returns></returns>
        public ResultClass MZ_PreviewRegister(InputClass inputClass)
        {
            return ((MIMzInterface)abstractAction).MZ_PreviewRegister(inputClass);
        }

        /// <summary>
        /// 门诊登记
        /// </summary>
        /// <returns></returns>
        public ResultClass MZ_Register(InputClass inputClass)
        {
            return ((MIMzInterface)abstractAction).MZ_Register(inputClass);
        }
        /// <summary>
        ///  挂号成功更新登记表
        /// </summary>
        /// <param name="inputClass">RegID，SerNO</param>
        /// <returns></returns>
        public ResultClass MZ_UpdateRegister(InputClass inputClass)
        {
            return ((MIMzInterface)abstractAction).MZ_UpdateRegister(inputClass);
        }
        /// <summary>
        /// 取消门诊登记
        /// </summary>
        /// <returns></returns>
        public ResultClass Mz_CancelRegister(InputClass inputClass)
        {
            return ((MIMzInterface)abstractAction).Mz_CancelRegister(inputClass);
        }

        /// <summary>
        /// 确认取消门诊登记
        /// </summary>
        /// <returns></returns>
        public ResultClass MZ_CancelRegisterCommit(InputClass inputClass)
        {
            return ((MIMzInterface)abstractAction).MZ_CancelRegisterCommit(inputClass);
        }

        /// <summary>
        /// 预算
        /// </summary>
        /// <param name="b"></param>
        /// <param name="s"></param>
        /// <param name="dt"></param>
        public ResultClass MZ_PreviewCharge(InputClass inputClass)
        {
            return ((MIMzInterface)abstractAction).MZ_PreviewCharge(inputClass);
        } 
          
        /// <summary>
        /// 结算
        /// </summary>
        /// <returns></returns>
        public ResultClass MZ_Charge(InputClass inputClass)
        {
            return ((MIMzInterface)abstractAction).MZ_Charge(inputClass);
        }
        /// <summary>
        /// 取消门诊结算
        /// </summary>
        /// <returns></returns>
        public ResultClass MZ_CancelCharge(InputClass inputClass)
        {
            return ((MIMzInterface)abstractAction).MZ_CancelCharge(inputClass);
        }
        /// <summary>
        /// 确认取消门诊收费
        /// </summary>
        /// <returns></returns>
        public ResultClass MZ_CancelChargeCommit(InputClass inputClass)
        {
            return ((MIMzInterface)abstractAction).MZ_CancelChargeCommit(inputClass);
        }
        /// <summary>
        /// 上传门诊病人费用
        /// </summary>
        /// <returns></returns>
        public ResultClass MZ_UploadzyPatFee( ) { throw new NotImplementedException(); }
        /// <summary>
        /// 下载门诊病人费用数据
        /// </summary>
        /// <returns></returns>
        public ResultClass MZ_DownloadzyPatFee( ) { throw new NotImplementedException(); }

        //获取已结算费用
        public ResultClass MZ_LoadFeeDetailByTicketNum( ) { throw new NotImplementedException(); }

        /// <summary>
        /// 根据医保交易状态更新交易记录
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        public ResultClass Mz_UpdateTradeRecord(InputClass inputClass)
        {
            return ((MIMzInterface)abstractAction).Mz_UpdateTradeRecord(inputClass);
        }


        /// <summary>
        /// 根据就诊号 获取挂号交易流水号
        /// </summary>
        /// <param name="sSerialNO">挂号就诊号</param>SocialCreateNum
        /// <returns></returns>
        public ResultClass Mz_GetRegisterTradeNo(string sSerialNO)
        {
            ResultClass resultClass = ((MIMzInterface)abstractAction).Mz_GetRegisterTradeNo(sSerialNO);

            return resultClass;
        }
        #endregion
    }
}
