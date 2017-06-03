using HIS_Entity.DrugManage;

namespace HIS_DrugManger.ObjectModel.Store
{
    /// <summary>
    /// 库存处理器构建工厂
    /// </summary>
    public class StoreFactory : EFWCoreLib.CoreFrame.Business.AbstractObjectModel
    {
        /// <summary>
        /// 获取库存处理器
        /// </summary>
        /// <param name="systemType">业务类型</param>
        /// <returns>库存处理对象</returns>
        internal IStore GetStoreProcess(string systemType)
        {
            switch (systemType)
            {
                case DGConstant.OP_DS_SYSTEM:
                    return NewObject<DSStore>();
                case DGConstant.OP_DW_SYSTEM:
                    return NewObject<DWStore>();
                default:
                    return null;
            }
        }
    }
}
