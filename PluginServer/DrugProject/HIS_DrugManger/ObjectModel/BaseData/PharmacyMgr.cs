using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManger.Dao;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManger.ObjectModel.BaseData
{
    /// <summary>
    /// 药理分类维护类
    /// </summary>
    public class PharmacyMgr : AbstractObjectModel
    {
        /// <summary>
        /// 获取所有的药理分类
        /// </summary>
        /// <returns>所有的药理分类</returns>
        public DataTable GetAllPharmacy()
        {
            return NewObject<DG_Pharmacology>().gettable(" DelFlag=0 ");
        }

        /// <summary>
        /// 获取父级ID
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>获取父级ID数据集</returns>
        public DataTable GetGridByParentId(Dictionary<string, string> query = null)
        {
            return NewDao<IDGDao>().GetPharmByParentId(query);
        }

        /// <summary>
        /// 更新为删除状态
        /// </summary>
        /// <param name="p">药理分类对象</param>
        public void UpdatePharmacy(DG_Pharmacology p)
        {
            p.Delflag = 1;
            p.save();
        }

        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="p">药理分类对象</param>
        /// <returns>返回结果</returns>
        public bool SavePharmacy(DG_Pharmacology p)
        {
            List<Tuple<string, string, SqlOperator>> lst = new List<Tuple<string, string, SqlOperator>>();
            lst.Add(Tuple.Create("PharmName", p.PharmName, SqlOperator.Equal));
            lst.Add(Tuple.Create("delflag", "0", SqlOperator.Equal));
            lst.Add(Tuple.Create("parentid", p.ParentID.ToString(), SqlOperator.Equal));
            IEnumerable<DG_Pharmacology> objs = NewObject<IDGDao>().GetEntityType<DG_Pharmacology>(lst, null);
            var vp = objs.FirstOrDefault(i => i.PharmID == p.PharmID);
            if (vp != null)
            {
                p.save();
                return true;
            }

            if (objs != null && objs.Any())
            {
                //同名记录存在
                return false;
            }

            p.save();
            return true;
        }
    }
}
