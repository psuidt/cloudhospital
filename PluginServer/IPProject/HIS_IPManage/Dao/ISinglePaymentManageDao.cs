using System.Data;
using HIS_Entity.IPManage;

namespace HIS_IPManage.Dao
{
    /// <summary>
    /// 住院缴款Dao
    /// </summary>
    public interface ISinglePaymentManageDao
    {
        /// <summary>
        /// 获取时间段内的交款记录
        /// </summary>
        /// <param name="iWorkId">机构ID</param>
        /// <param name="sBDate">交款开始时间</param>
        /// <param name="sEDate">交款结束时间</param>
        /// <param name="iEmpId">操作员ID -1未所有</param>
        /// <returns>交款记录</returns>
        DataTable GetAccountListUploaded(int iWorkId, string sBDate, string sEDate, int iEmpId);

        /// <summary>
        /// 获取所有未缴款的结算操作员
        /// </summary>
        /// <param name="iWorkId">机构ID</param>
        /// <param name="iEmpId">操作员ID -1未所有</param>
        /// <returns>操作员信息</returns>
        DataTable GetAccountListUnUpload(int iWorkId, int iEmpId);

        /// <summary>
        /// 查询预交金信息
        /// </summary>
        /// <param name="iWorkId">机构ID</param>
        /// <param name="iStaffId">操作员ID</param>
        /// <param name="iAccountId">缴款ID</param>
        /// <returns>预交金信息</returns>
        DataTable GetDepositList(int iWorkId, int iStaffId, int iAccountId);

        /// <summary>
        /// 根据accountid获取票据信息
        /// </summary>
        /// <param name="iWorkId">机构ID</param>
        /// <param name="iStaffId">操作员ID</param>
        /// <param name="iAccountId">缴款ID</param>
        /// <returns>票据信息</returns>
        DataTable GetPayMentItemFPSum(int iWorkId, int iStaffId, int iAccountId);

        /// <summary>
        /// 获取项目分类数据
        /// </summary>
        /// <param name="iWorkId">机构ID</param>
        /// <param name="iStaffId">操作员ID</param>
        /// <param name="iAccountId">缴款ID</param>
        /// <returns>项目分类数据</returns>
        DataTable GetPayMentItemFPClass(int iWorkId, int iStaffId, int iAccountId);

        /// <summary>
        /// 根据accountid获取支付方式信息
        /// </summary>
        /// <param name="iWorkId">机构ID</param>
        /// <param name="iStaffId">操作员ID</param>
        /// <param name="iAccountId">缴款ID</param>
        /// <returns>支付方式信息</returns>
        DataTable GetPayMentItemAccountClass(int iWorkId, int iStaffId, int iAccountId);

        /// <summary>
        /// 执行缴款
        /// </summary>
        /// <param name="iWorkId">机构Id</param>
        /// <param name="iStaffId">操作员ID</param>
        /// <param name="iAccountType">结算类型0预交金1结算</param>
        /// <param name="dTotalFee">此次缴款总额</param>
        /// <param name="dTotalPaymentFee">实际缴款现金总额</param>
        /// <param name="sErrcode">错误代码</param>
        /// <param name="sErrmsg">错误消息</param>
        /// <returns>缴款信息</returns>
        IP_Account AccountPayment(int iWorkId, int iStaffId, int iAccountType, decimal dTotalFee, decimal dTotalPaymentFee, out int sErrcode, out string sErrmsg);

        /// <summary>
        /// 获取票据明细
        /// </summary>
        /// <param name="iWorkId">机构Id</param>
        /// <param name="invoiceID">发票卷ID</param>
        /// <param name="invoiceType">类型0-收费 1-退费</param>
        /// <param name="accountid">缴款id</param>
        /// <returns>票据明细</returns>
        DataTable GetInvoiceDetail(int iWorkId,int invoiceID, int invoiceType, int accountid);

        /// <summary>
        /// 获取缴款实体
        /// </summary>
        /// <param name="iAccountID">缴款ID</param>
        /// <returns>缴款实体</returns>
        IP_Account GetAccount(int iAccountID);
    }
}
