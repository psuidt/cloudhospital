using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManger.Dao;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManger.ObjectModel.BaseData
{
    /// <summary>
    /// 药品规格维护类
    /// </summary>
    public class DrugSpecDicMgr : AbstractObjectModel
    {
        /// <summary>
        /// 获取药品字典数据
        /// </summary>
        /// <param name="andWhere">and查询条件</param>
        /// <param name="orWhere">or查询条件</param>
        /// <returns>药品字典数据</returns>
        public DataTable GetDurgDic(List<Tuple<string, string, SqlOperator>> andWhere, List<Tuple<string, string, SqlOperator>> orWhere)
        {
            return NewDao<IDGDao>().GetDrugDic(andWhere, orWhere);
        }

        /// <summary>
        /// 获取本院查中心典数据
        /// </summary>
        /// <param name="andWhere">and查询条件</param>
        /// <param name="orWhere">or查询条件</param>
        /// <param name="creatWorkId">创建机构ID</param>
        /// <returns>本院查中心典数据</returns>
        public DataTable GetHospDrugDic(List<Tuple<string, string, SqlOperator>> andWhere, List<Tuple<string, string, SqlOperator>> orWhere, int creatWorkId)
        {
            return NewDao<IDGDao>().GetDrugDic(andWhere, orWhere, creatWorkId);
        }

        /// <summary>
        /// 查询本院典数据
        /// </summary>
        /// <param name="andWhere">and查询条件</param>
        /// <param name="orWhere">or查询条件</param>
        /// <returns>本院典数据</returns>
        public DataTable GetDrugDicHisDataTable(List<Tuple<string, string, SqlOperator>> andWhere = null, List<Tuple<string, string, SqlOperator>> orWhere = null)
        {
            return NewDao<IDGDao>().GetDrugDicHisDataTable(andWhere, orWhere);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="p">中心典对象</param>
        public void SaveDrugDic(DG_CenterSpecDic p)
        {
            p.save();
        }

        /// <summary>
        /// 保存本院典
        /// </summary>
        /// <param name="p">本院典对象</param>
        public void SaveHospDic(DG_HospMakerDic p)
        {
            p.save();
        }
    }
}
