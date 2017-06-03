using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MemberManage;
using HIS_MemberManage.Dao;

namespace HIS_MemberManage.ObjectModel
{
    /// <summary>
    /// 礼品管理
    /// </summary>
    public class GiftManagement : AbstractObjectModel
    {
        /// <summary>
        /// 获取帐户类型信息
        /// </summary>
        /// <param name="workID">机构id</param>
        /// <returns>帐户类型信息</returns>
        public DataTable GetCardTypeForWork(int workID)
        {
            return NewDao<IOPGiftDao>().GetCardTypeForWork(workID);
        }

        /// <summary>
        /// 获取礼品信息
        /// </summary>
        /// <param name="workID">机构id</param>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="useFlag">使用标识</param>
        /// <returns>礼品信息</returns>
        public DataTable GetGiftInfo(int workID, int cardTypeID,int useFlag)
        {
            return NewDao<IOPGiftDao>().GetGiftInfo(workID, cardTypeID, useFlag);
        }

        /// <summary>
        /// 保存礼品信息
        /// </summary>
        /// <param name="giftEntity">礼品信息</param>
        /// <returns>1成功</returns>
        public int SaveGiftInfo(ME_Gift giftEntity)
        {
            return NewDao<IOPGiftDao>().SaveGiftInfo(giftEntity);
        }

        /// <summary>
        /// 新增礼品时校验礼品名称是否唯一
        /// </summary>
        /// <param name="workID">机构id</param>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="giftName">礼品名称</param>
        /// <returns>false存在</returns>
        public bool CheckGiftNameForADD(int workID,int cardTypeID,string giftName)
        {
            DataTable dt = NewDao<IOPGiftDao>().CheckGiftName(workID, cardTypeID, giftName);
            if (dt.Rows.Count>0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 编辑检查礼品名称重复性
        /// </summary>
        /// <param name="workID">机构id</param>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="giftName">礼品名称</param>
        /// <param name="giftID">礼品id</param>
        /// <returns>false存在</returns>
        public bool CheckGiftNameForEdit(int workID, int cardTypeID, string giftName,int giftID)
        {
            DataTable dt = NewDao<IOPGiftDao>().CheckGiftName(workID, cardTypeID, giftName);
            if (dt.Rows.Count > 0)
            {
                return (giftID == Convert.ToInt16(dt.Rows[0]["GiftID"])) ? true : false;          
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 更新礼品管理状态标志
        /// </summary>
        /// <param name="giftID">礼品id</param>
        /// <param name="useFlag">使用标识</param>
        /// <param name="operateID">操作人id</param>
        /// <returns>1成功</returns>
        public int UpdateGiftFlag(int giftID, int useFlag, int operateID)
        {
            return NewDao<IOPGiftDao>().UpdateGiftFlag(giftID, useFlag, operateID);
        }
    }
}
