using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.OPManage;

namespace HIS_OPManage.ObjectModel
{
    /// <summary>
    /// 收费工厂类
    /// </summary>
    public  class BalanceFactory: AbstractObjectModel
    {
        /// <summary>
        /// 根据结算类型实例化类
        /// </summary>
        /// <param name="chargeType">结算类型</param>
        /// <returns>结算处理对象</returns>
        public  BaseBalaceProcess CreateChargeObject(OP_Enum.BalanceType chargeType)
        {
            if (chargeType == OP_Enum.BalanceType.一次结算一张票号)
            {
                return NewObject<BalanceProcess>();
            }
            else
            {
                return NewObject<BalanceProcessOne>();
            }
        }
    }
}
