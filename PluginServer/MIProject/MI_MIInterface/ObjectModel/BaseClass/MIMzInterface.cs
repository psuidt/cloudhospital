using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS_Entity.MIManage;

namespace MI_MIInterface.ObjectModel.BaseClass
{
    /// <summary>
    /// 医保报销门诊接口处理
    /// </summary>
    public interface MIMzInterface
    {
        /// <summary>
        /// 获取卡片信息病人列表
        /// </summary>
        /// <returns></returns>
        ResultClass MZ_GetCardInfo(InputClass inputClass);//string sCardNo);

        /// <summary>
        /// 根据身份证号查找农合病人
        /// </summary>
        ResultClass MZ_GetPersonInfo(InputClass inputClass);//string sCardNo);

        /// <summary>
        /// 门诊预登记
        /// </summary>
        /// <returns></returns>
        ResultClass MZ_PreviewRegister(InputClass inputClass);//MI_Register register);

        /// <summary>
        /// 门诊登记
        /// </summary>
        /// <returns></returns>
        ResultClass MZ_Register(InputClass inputClass);//int registerId, string serialNO);
        /// <summary>
        /// 挂号成功更新登记表
        /// </summary>
        /// <param name="inputClass">RegID，SerNO</param>
        /// <returns></returns>
        ResultClass MZ_UpdateRegister(InputClass inputClass);
        /// <summary>
        /// 取消门诊登记
        /// </summary>
        /// <returns></returns>
        ResultClass Mz_CancelRegister(InputClass inputClass);//string serialNO);
        /// <summary>
        /// 确认取消门诊登记
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        ResultClass MZ_CancelRegisterCommit(InputClass inputClass);//string serialNO);
        /// <summary>
        /// 预算
        /// </summary>
        /// <returns></returns>
        ResultClass MZ_PreviewCharge(InputClass inputClass);//TradeData tradedata);
        /// <summary>
        /// 结算
        /// </summary>
        /// <returns></returns>
        ResultClass MZ_Charge(InputClass inputClass);//int tradeRecordId, string fph);
        /// <summary>
        /// 取消门诊结算
        /// </summary>
        /// <returns></returns>
        ResultClass MZ_CancelCharge(InputClass inputClass);//string fph);

        /// <summary>
        /// 确认取消门诊结算
        /// </summary>
        /// <returns></returns>
        ResultClass MZ_CancelChargeCommit(InputClass inputClass);//string fph);
        /// <summary>
        /// 上传门诊病人费用
        /// </summary>
        /// <returns></returns>
        ResultClass MZ_UploadzyPatFee(InputClass inputClass);// );
        /// <summary>
        /// 下载门诊病人费用数据
        /// </summary>
        /// <returns></returns>
        ResultClass MZ_DownloadzyPatFee(InputClass inputClass);// );

        //获取已结算费用
        ResultClass MZ_LoadFeeDetailByTicketNum(InputClass inputClass);// );


        /// <summary>
        /// 根据医保交易状态更新交易记录
        /// </summary>
        /// <param name="inputClass"></param>
        /// <returns></returns>
        ResultClass Mz_UpdateTradeRecord(InputClass inputClass);// );


        /// <summary>
        /// 根据就诊号 获取挂号交易流水号
        /// </summary>
        /// <param name="sSerialNO">挂号就诊号</param>
        /// <returns></returns>
        ResultClass Mz_GetRegisterTradeNo(string sSerialNO);
    }
}
