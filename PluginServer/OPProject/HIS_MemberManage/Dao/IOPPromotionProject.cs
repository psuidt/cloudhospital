using System.Data;

namespace HIS_MemberManage.Dao
{
    /// <summary>
    /// 优惠方案接口
    /// </summary>
    public interface IOPPromotionProject
    {
        /// <summary>
        /// 取得优惠方案头信息
        /// </summary>
        /// <param name="workID">组织机构id</param>
        /// <param name="stDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>优惠方案头信息</returns>
        DataTable GetPromotionProjectHeadInfo(int workID, string stDate, string endDate);

        /// <summary>
        /// 检查方案名称唯一性
        /// </summary>
        /// <param name="name">方案名称</param>
        /// <returns>方案信息</returns>
        DataTable ChecPromName(string name);

        /// <summary>
        /// 删除方案
        /// </summary>
        /// <param name="promID">方案id</param>
        /// <returns>1成功</returns>
        int DeleteHead(int promID);

        /// <summary>
        /// 删除方案明细
        /// </summary>
        /// <param name="promID">方案id</param>
        /// <returns>1成功</returns>
        int DeleteDetail(int promID);

        /// <summary>
        /// 更新方案头使用标识
        /// </summary>
        /// <param name="promID">方案id</param>
        /// <param name="flag">使用标识</param>
        /// <param name="operateID">操作人id</param>
        /// <returns>1成功</returns>
        int UpdateHeadUseFlag(int promID, int flag, int operateID);

        /// <summary>
        /// 检查方案开始日期
        /// </summary>
        /// <param name="stDate">开始日期</param>
        /// <param name="workID">组织机构id</param>
        /// <returns>方案数据</returns>
        DataTable CheckPromStartDate(string stDate, int workID);

        /// <summary>
        /// 检查方案明细总额优惠
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="patTypeID">病人类型id</param>
        /// <param name="costTypeID">费用类型id</param>
        /// <param name="promTypeID">优惠类型id</param>
        /// <param name="promID">优惠方案id</param>
        /// <param name="promSunID">优惠明细id</param>
        /// <returns>明细数据</returns>
        DataTable CheckDetailForAmount(int cardTypeID, int patTypeID, int costTypeID, int promTypeID, int promID, int promSunID);

        /// <summary>
        /// 检查方案明细类型优惠
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="patTypeID">病人类型id</param>
        /// <param name="costTypeID">费用类型id</param>
        /// <param name="promTypeID">优惠类型id</param>
        /// <param name="classID">分类id</param>
        /// <param name="promID">方案id</param>
        /// <param name="promSunID">优惠明细id</param>
        /// <returns>明细数据</returns>
        DataTable CheckDetailForClass(int cardTypeID, int patTypeID, int costTypeID, int promTypeID, int classID, int promID, int promSunID);

        /// <summary>
        /// 检查方案明细项目优惠
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="patTypeID">病人类型id</param>
        /// <param name="costTypeID">费用类型id</param>
        /// <param name="promTypeID">优惠类型id</param>
        /// <param name="itemID">项目id</param>
        /// <param name="promID">方案id</param>
        /// <param name="promSunID">优惠明细id</param>
        /// <returns>明细数据</returns>
        DataTable CheckDetailForItem(int cardTypeID, int patTypeID, int costTypeID, int promTypeID, int itemID, int promID, int promSunID);

        /// <summary>
        /// 取得优惠方案明细
        /// </summary>
        /// <param name="promID">方案id</param>
        /// <returns>优惠方案明细</returns>
        DataTable GetPromotionProjectDetail(int promID);

        /// <summary>
        /// 更新明细标识
        /// </summary>
        /// <param name="promSunID">明细Id</param>
        /// <param name="flag">启用标识</param>
        /// <returns>1成功</returns>
        int UpdateDetailFlag(int promSunID, int flag);

        /// <summary>
        /// 取得优惠方案
        /// </summary>
        /// <param name="promID">方案id</param>
        /// <returns>优惠方案数据</returns>
        DataTable GetPromotionProject(int promID);
    }
}
