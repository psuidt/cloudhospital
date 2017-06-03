using HIS_Entity.DrugManage;

namespace HIS_DrugManger.ObjectModel.Account
{
    /// <summary>
    /// 财务处理器构建工厂
    /// </summary>
    internal class AccountFactory
    {
        /// <summary>
        /// 获取报表查询器
        /// </summary>
        /// <param name="opType">类型</param>
        /// <returns>查询对象</returns>
        public AccountQuery GetAccountQuery(string opType)
        {
            switch (opType)
            {
                case DGConstant.OP_DW_SYSTEM:
                    return new DWAccountQuery();
                case DGConstant.OP_DS_SYSTEM:
                    return new DSAccountQuery();
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取月结处理器
        /// </summary>
        /// <param name="opType">类型</param>
        /// <returns>查询对象</returns>
        public DrugBalance GetBalance(string opType)
        {
            switch (opType)
            {
                case DGConstant.OP_DS_SYSTEM:
                    return new DSBalance();
                case DGConstant.OP_DW_SYSTEM:
                    return new DWBalance();
                default:
                    return null;
            }
        }
    }
}
