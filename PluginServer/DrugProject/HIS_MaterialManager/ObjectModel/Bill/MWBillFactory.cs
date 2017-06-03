using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS_Entity.MaterialManage;

namespace HIS_MaterialManager.ObjectModel.Bill
{
    /// <summary>
    /// 物资单据处理器构建工厂
    /// </summary>
    public class MwBillFactory : EFWCoreLib.CoreFrame.Business.AbstractObjectModel
    {
        /// <summary>
        /// 获取单据处理器
        /// </summary>
        /// <param name="opType">业务类型</param>
        /// <returns>单据接口</returns>
        public IMwBill GetBillProcess(string opType)
        {
            switch (opType)
            {
                //药库入、退库
                case MWConstant.OP_MW_INSTORE:
                case MWConstant.OP_MW_BUYINSTORE:
                case MWConstant.OP_MW_FIRSTIN:
                case MWConstant.OP_MW_BACKSTORE:
                    return NewObject<MwInstoreBill>();
                case MWConstant.OP_MW_STOCKPLAN:
                    return NewObject<MwPurchaseBill>();
                case MWConstant.OP_MW_CHECK:
                case MWConstant.OP_MW_AUDITCHECK:
                     return NewObject<MwCheckBill>(); 
                case MWConstant.OP_MW_DEPTDRAW:
                case MWConstant.OP_MW_CIRCULATEOUT:
                case MWConstant.OP_MW_REPORTLOSS:
                case MWConstant.OP_MW_RETURNSTORE:
                    return NewObject<MwOutStoreBill>();
                default:
                    return null;
            }
        }
    }
}
