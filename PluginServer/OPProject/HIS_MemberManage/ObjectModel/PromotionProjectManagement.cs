using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.Common;
using HIS_Entity.MemberManage;
using HIS_MemberManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_MemberManage.ObjectModel
{
    /// <summary>
    /// 优惠方案管理
    /// </summary>
    public class PromotionProjectManagement : AbstractObjectModel
    {
        /// <summary>
        /// 获取优惠方案头表信息
        /// </summary>
        /// <param name="workID">机构ID</param>
        /// <param name="stDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>优惠方案头表信息</returns>
        public DataTable GetPromotionProjectHeadInfo(int workID, string stDate, string endDate)
        {
            return NewDao<IOPPromotionProject>().GetPromotionProjectHeadInfo(workID, stDate, endDate);
        }

        /// <summary>
        /// 新增方案头表时名称校验是否唯一
        /// </summary>
        /// <param name="name">方案名称</param>
        /// <returns>true有效</returns>
        public bool CheckPromNameForADD(string name)
        {
            DataTable dt = NewDao<IOPPromotionProject>().ChecPromName(name);

            return (dt.Rows.Count > 0) ? true : false;
        }

        /// <summary>
        /// 修改方案头表时名称校验是否唯一
        /// </summary>
        /// <param name="headID">头id</param>
        /// <param name="name">方案名称</param>
        /// <returns>true成功</returns>
        public bool CheckPromNameForEdit(int headID, string name)
        {
            DataTable dt = NewDao<IOPPromotionProject>().ChecPromName(name);
            if (dt.Rows.Count > 0)
            {
                return (headID == Convert.ToInt16(dt.Rows[0]["PromID"])) ? true : false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 保存头表信息
        /// </summary>
        /// <param name="headEntity">头信息</param>
        /// <returns>1成功</returns>
        public int SaveHeadInfo(ME_PromotionProjectHead headEntity)
        {
            this.BindDb(headEntity);
            return headEntity.save();
        }

        /// <summary>
        /// 删除方案信息
        /// </summary>
        /// <param name="promID">方案id</param>
        /// <returns>1成功</returns>
        public int DeletePromPro(int promID)
        {
            NewDao<IOPPromotionProject>().DeleteDetail(promID);
            return NewDao<IOPPromotionProject>().DeleteHead(promID);
        }

        /// <summary>
        /// 更新方案头表使用状态
        /// </summary>
        /// <param name="promID">方案id</param>
        /// <param name="flag">使用标识</param>
        /// <param name="operateID">操作员id</param>
        /// <returns>1成功</returns>
        public int UpdateHeadUseFlag(int promID, int flag, int operateID)
        {
            return NewDao<IOPPromotionProject>().UpdateHeadUseFlag(promID, flag, operateID);
        }

        /// <summary>
        /// 检查方案日期
        /// </summary>
        /// <param name="stDate">日期</param>
        /// <param name="workID">机构编码</param>
        /// <returns>true有效</returns>
        public bool CheckPromDate(string stDate, int workID)
        {
            DataTable dt = NewDao<IOPPromotionProject>().CheckPromStartDate(stDate, workID);
            return (dt.Rows.Count > 0) ? false : true;
        }

        /// <summary>
        /// 检查总额优惠方案
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="patTypeID">病人类型id</param>
        /// <param name="costTypeID">费用类型id</param>
        /// <param name="promTypeID">优惠类型id</param>
        /// <param name="promID">方案id</param>
        /// <param name="promSunID">方案明细id</param>
        /// <returns>true成功</returns>
        public bool CheckDetailForAmount(int cardTypeID, int patTypeID, int costTypeID, int promTypeID, int promID, int promSunID)
        {
            DataTable dt = NewDao<IOPPromotionProject>().CheckDetailForAmount(cardTypeID, patTypeID, costTypeID, promTypeID, promID, promSunID);
            if (promSunID == 0)
            {//新增
                return (dt.Rows.Count > 0) ? false : true;
            }
            else
            {//修改
                if (dt.Rows.Count > 0)
                {
                    int detailID = Convert.ToInt16(dt.Rows[0]["PromSunID"]);
                    return (detailID == promSunID) ? true : false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 检查类型优惠方案明细
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="patTypeID">病人类型id</param>
        /// <param name="costTypeID">费用类型id</param>
        /// <param name="promTypeID">优惠类型id</param>
        /// <param name="classID">类型id</param>
        /// <param name="promID">方案id</param>
        /// <param name="promSunID">方案明细id</param>
        /// <returns>true成功</returns>
        public bool CheckDetailForClass(int cardTypeID, int patTypeID, int costTypeID, int promTypeID, int classID, int promID, int promSunID)
        {
            DataTable dt = NewDao<IOPPromotionProject>().CheckDetailForClass(cardTypeID, patTypeID, costTypeID, promTypeID, classID, promID, promSunID);
            if (promSunID == 0)
            {//新增
                return (dt.Rows.Count > 0) ? false : true;
            }
            else
            {//修改
                if (dt.Rows.Count > 0)
                {
                    int detailID = Convert.ToInt16(dt.Rows[0]["PromSunID"]);
                    return (detailID == promSunID) ? true : false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 检查明细项目优惠
        /// </summary>
        /// <param name="cardTypeID">卡类型id</param>
        /// <param name="patTypeID">病人类型id</param>
        /// <param name="costTypeID">费用类型id</param>
        /// <param name="promTypeID">优惠类型id</param>
        /// <param name="itemID">项目id</param>
        /// <param name="promID">方案id</param>
        /// <param name="promSunID">方案明细id</param>
        /// <returns>true成功</returns>
        public bool CheckDetailForItem(int cardTypeID, int patTypeID, int costTypeID, int promTypeID, int itemID, int promID, int promSunID)
        {
            DataTable dt = NewDao<IOPPromotionProject>().CheckDetailForItem(cardTypeID, patTypeID, costTypeID, promTypeID, itemID, promID, promSunID);
            if (promSunID == 0)
            {//新增
                return (dt.Rows.Count > 0) ? false : true;
            }
            else
            {//修改
                if (dt.Rows.Count > 0)
                {
                    int detailID = Convert.ToInt16(dt.Rows[0]["PromSunID"]);
                    return (detailID == promSunID) ? true : false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 获取方案明细数据
        /// </summary>
        /// <param name="promID">方案id</param>
        /// <returns>方案明细数据</returns>
        public DataTable GetPromotionProjectDetail(int promID)
        {
            return NewObject<IOPPromotionProject>().GetPromotionProjectDetail(promID);
        }

        /// <summary>
        /// 保存方案明细数据
        /// </summary>
        /// <param name="detailEntity">明细信息</param>
        /// <returns>1成功</returns>
        public int SavePromotionProjectDetail(ME_PromotionProjectDetail detailEntity)
        {
            this.BindDb(detailEntity);
            return detailEntity.save();
        }

        /// <summary>
        /// 获取大项目列表
        /// </summary>
        /// <returns>大项目列表</returns>
        public DataTable GetStatItem()
        {
            return NewObject<BasicDataManagement>().GetStatItem(false);
        }

        /// <summary>
        /// 获取收费项目
        /// </summary>
        /// <returns>收费项目</returns>
        public DataTable GetSimpleFeeItemDataDt()
        {
            return NewObject<FeeItemDataSource>().GetSimpleFeeItemDataDt(FeeClass.收费项目);
        }

        /// <summary>
        /// 更新明细表使用状态
        /// </summary>
        /// <param name="promSunID">优惠明细id</param>
        /// <param name="flag">启用标识</param>
        /// <returns>1成功</returns>
        public int UpdateDetailFlag(int promSunID, int flag)
        {
            return NewObject<IOPPromotionProject>().UpdateDetailFlag(promSunID, flag);
        }

        /// <summary>
        /// 复制优惠方案
        /// </summary>
        /// <param name="promID">方案id</param>
        /// <param name="operateID">操作人id</param>
        /// <returns>1成功</returns>
        public int CopypPromotionProject(int promID,int operateID)
        {
            int resFlag = 0;

            //获取方案主表信息
            DataTable dtHead= NewObject<IOPPromotionProject>().GetPromotionProject(promID);
            ME_PromotionProjectHead headEntity = new ME_PromotionProjectHead();
            headEntity = ConvertExtend.ToObject<ME_PromotionProjectHead>(dtHead, 0);
            Random rd = new Random();
            headEntity.PromName = headEntity.PromName + "-复制"+rd.Next(1, 999);
            headEntity.UseFlag = 0;
            headEntity.PromID = 0;
            headEntity.OperateDate = DateTime.Now;
            headEntity.OperateID = operateID;
            this.BindDb(headEntity);
            resFlag = headEntity.save();     //保存主表，获取ID
            //获取明细表信息
            DataTable dtDetail= NewObject<IOPPromotionProject>().GetPromotionProjectDetail(promID);
            for (int i = 0; i < dtDetail.Rows.Count; i++)
            {
                ME_PromotionProjectDetail detailEntity = new ME_PromotionProjectDetail();
                detailEntity = ConvertExtend.ToObject<ME_PromotionProjectDetail>(dtDetail, i);
                detailEntity.PromSunID = 0;
                detailEntity.PromID = headEntity.PromID;
                detailEntity.OperateDate= DateTime.Now;
                detailEntity.OperateID = operateID;
                this.BindDb(detailEntity);
                resFlag=detailEntity.save();
            } 

            return resFlag;
        }
    }
}
