using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MemberManage;
using HIS_MemberManage.Dao;

namespace HIS_MemberManage.ObjectModel
{
    /// <summary>
    /// 积分转换规则管理
    /// </summary>
    public class ConvertPointsManagement: AbstractObjectModel
    {
        /// <summary>
        /// 获取帐户类型信息
        /// </summary>
        /// <returns>帐户类型信息</returns>
        public DataTable GetAccountTypeInfo()
        {
            return NewDao<IOPConvertPoints>().GetAccountTypeInfo();
        }

        /// <summary>
        /// 保存卡片帐户类型信息
        /// </summary>
        /// <param name="cardTypeList">卡类型信息</param>
        /// <returns>帐户类型信息</returns>
        public int SaveCardTypeInfo(ME_CardTypeList cardTypeList)
        {
            this.BindDb(cardTypeList);
            return cardTypeList.save();
        }

        /// <summary>
        /// 检验新增帐户类型名称是否重名
        /// </summary>
        /// <param name="name">帐户类型名称</param>
        /// <returns>false存在重复</returns>
        public bool CheckCardTypeNameForADD(string name)
        {
            DataTable dt = NewDao<IOPConvertPoints>().CheckCardType(name);
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
        /// 检验修改帐户类型名称是否重名
        /// </summary>
        /// <param name="name">帐户类型名称</param>
        /// <param name="id">帐户类型ID</param>
        /// <returns>true重复</returns>
        public bool CheckCardTypeNameForEdit(string name,int id)
        {
            DataTable dt = NewDao<IOPConvertPoints>().CheckCardType(name);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt16(dt.Rows[0]["CardTypeID"])==id)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 更新帐户类型的状态
        /// </summary>
        /// <param name="id">帐户类型ID</param>
        /// <param name="flag">状态标志</param>
        /// <param name="operateID">操作员ID</param>
        /// <returns>1成功</returns>
        public int UpdateUseFlag(int id, int flag, int operateID)
        {
            return NewDao<IOPConvertPoints>().UpdateUseFlag(id, flag, operateID);
        }

        /// <summary>
        /// 获取积分规则信息
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <returns>积分规则信息</returns>
        public DataTable GetConvertPointsInfo(int cardTypeID)
        {
            return NewDao<IOPConvertPoints>().GetConvertPointsInfo(cardTypeID);
        }

        /// <summary>
        /// 积分设置兑换保存
        /// </summary>
        /// <param name="convertPoints">积分兑换信息</param>
        /// <returns>1成功</returns>
        public int SavePoints(ME_ConvertPoints convertPoints)
        {
            this.BindDb(convertPoints);
            return convertPoints.save();
        }

        /// <summary>
        /// 检验新增积分兑换设置是否存在
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="workID">组织机构id</param>
        /// <returns>false存在</returns>
        public bool CheckPointsForADD(int cardTypeID,int workID)
        {
            DataTable dt = NewDao<IOPConvertPoints>().CheckConvertPoints(cardTypeID, workID);
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
        /// 检查卡前缀
        /// </summary>
        /// <param name="cardPrefix">卡前缀</param>
        /// <returns>卡信息</returns>
        public DataTable CheckCardPrefix(string cardPrefix)
        {
            return NewDao<IOPConvertPoints>().CheckCardPrefix(cardPrefix);
        }

        /// <summary>
        /// 更新积分使用标识
        /// </summary>
        /// <param name="pointsID">积分id</param>
        /// <param name="useFlag">启用id</param>
        /// <param name="operateID">操作人id</param>
        /// <returns>1成功</returns>
        public int UpdatePointsUseFlag(int pointsID,int useFlag,int operateID)
        {
            return NewDao<IOPConvertPoints>().UpdatePointsUseFlag(pointsID, useFlag, operateID);
        }
    }
}
