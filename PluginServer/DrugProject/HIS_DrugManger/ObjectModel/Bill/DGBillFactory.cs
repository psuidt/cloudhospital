using HIS_Entity.DrugManage;

namespace HIS_DrugManger.ObjectModel.Bill
{
    /// <summary>
    /// 药品单据处理器构建工厂
    /// </summary>
    public class DGBillFactory : EFWCoreLib.CoreFrame.Business.AbstractObjectModel
    {
        /// <summary>
        /// 获取单据处理器
        /// </summary>
        /// <param name="opType">类型</param>
        /// <returns>处理对象</returns>
        public IDGBill GetBillProcess(string opType)
        {
            switch (opType)
            {
                //药房入、退库
                case DGConstant.OP_DS_INSTORE:
                case DGConstant.OP_DS_BUYINSTORE:
                case DGConstant.OP_DS_FIRSTIN:
                case DGConstant.OP_DS_CIRCULATEIN:
                case DGConstant.OP_DS_RETURNSOTRE:
                    return NewObject<DSInStoreBill>();

                //药房申请入库
                case DGConstant.OP_DS_APPLYPLAN:
                    return NewObject<DSApplyStoreBill>();

                //药房出库
                case DGConstant.OP_DS_DEPTDRAW:
                case DGConstant.OP_DS_REPORTLOSS:
                    return NewObject<DSOutStoreBill>();
                //药房盘点
                case DGConstant.OP_DS_CHECK:
                case DGConstant.OP_DS_AUDITCHECK:
                    return NewObject<DSCheckBill>();

                //药品调价
                case DGConstant.OP_DS_ADJPRICE:
                case DGConstant.OP_DW_ADJPRICE:
                    return NewObject<DGAdjPriceBill>();

                //药库入、退库
                case DGConstant.OP_DW_INSTORE:
                case DGConstant.OP_DW_BUYINSTORE:
                case DGConstant.OP_DW_FIRSTIN:
                case DGConstant.OP_DW_BACKSTORE:
                    return NewObject<DWInStoreBill>();

                //药库采购计划
                case DGConstant.OP_DW_STOCKPLAN:
                    return NewObject<DGPurchaseBill>();

                //药库出、返库
                case DGConstant.OP_DW_DEPTDRAW:
                case DGConstant.OP_DW_REPORTLOSS:
                case DGConstant.OP_DW_CIRCULATEOUT:
                case DGConstant.OP_DW_RETURNSTORE:
                    return NewObject<DWOutStoreBill>();

                //药库盘点
                case DGConstant.OP_DW_CHECK:
                case DGConstant.OP_DW_AUDITCHECK:
                    return NewObject<DWCheckBill>();
                default:
                    return null;
            }
        }
    }
}
