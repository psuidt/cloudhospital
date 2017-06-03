using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business;
using System.Data;
using HIS_Entity.MIManage;
using EFWCoreLib.CoreFrame.Common;
using System.Drawing;

namespace HIS_MIInterface.ObjectModel
{
    public class TradeProcess: AbstractObjectModel
    {
        #region 交易信息提交
        /// <summary>
        /// 交易信息提交
        /// </summary>
        /// <param name="_curPatlist"></param>
        /// <param name="paymentInfoList"></param>
        /// <param name="totalFee"></param>
        /// <returns></returns>
        public MI_MedicalInsurancePayRecord SaveTradeInfo(MI_MedicalInsurancePayRecord medicalInsurancePayRecord, MI_MIPayRecordHead mIPayRecordHead, List<MI_MIPayRecordDetail> mIPayRecordDetailList)
        {
            try
            {
                //插入挂号就诊表记录    
                AddLog("HIS插入记录表记录开始！");
                this.BindDb(medicalInsurancePayRecord);
                medicalInsurancePayRecord.save();
                AddLog("HIS插入记录表记录结束！");

                //得到当前结账ID
                int payRecordId = medicalInsurancePayRecord.ID;//NewObject<CommonMethod>().GetPayRecordId(medicalInsurancePayRecord.MIID, medicalInsurancePayRecord.TradeNO);
                if (payRecordId == 0)
                    return null;

                AddLog("HIS插入打印数据表记录开始！");
                if (mIPayRecordHead != null)
                {
                    //插入交易头表
                    mIPayRecordHead.PayRecordId = payRecordId;
                    mIPayRecordHead.tradeno = medicalInsurancePayRecord.TradeNO;
                    this.BindDb(mIPayRecordHead);
                    mIPayRecordHead.save();
                }
                AddLog("HIS插入明细表记录结束！");
                #region 插入交易明细表
                foreach (MI_MIPayRecordDetail mIPayRecordDetail in mIPayRecordDetailList)
                {
                    if (mIPayRecordDetail.itemno == null || mIPayRecordDetail.itemno.Equals(String.Empty))
                        continue;
                    mIPayRecordDetail.PayRecordId = payRecordId;
                    mIPayRecordDetail.tradeno= medicalInsurancePayRecord.TradeNO;
                    this.BindDb(mIPayRecordDetail);
                    mIPayRecordDetail.save();
                }
                AddLog("HIS插入明细表记录结束！");
                #endregion
                return medicalInsurancePayRecord;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

        }

        #endregion


        private void AddLog(string s)
        {
            MiddlewareLogHelper.WriterLog(LogType.MILog, true, Color.Blue, s);
        }
    }
}
