using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManger.Dao;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManger.ObjectModel.BaseData
{
    /// <summary>
    /// 药品类型维护类
    /// </summary>
    public class DrugTypeMgr : AbstractObjectModel
    {
        #region 药品类型
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="typeDic">实体对象</param>
        /// <returns>是否成功</returns>
        public bool SaveDrugType(DG_TypeDic typeDic)
        {
            List<Tuple<string, string, SqlOperator>> lst = new List<Tuple<string, string, SqlOperator>>();
            lst.Add(Tuple.Create("TypeName", typeDic.TypeName, SqlOperator.Equal));
            IEnumerable<DG_TypeDic> objs = NewObject<IDGDao>().GetEntityType<DG_TypeDic>(lst, null);

            if (objs != null && objs.Any())
            {
                //同名记录存在
                if (objs.Count() > 1)
                {
                    return false;
                }

                var t = objs.FirstOrDefault(i => i.TypeID == typeDic.TypeID);
                if (t != null)
                {
                    //完全没修过
                    typeDic.save();
                    return true;
                }
                else
                {
                    return false;
                }
            }

            typeDic.save();
            return true;
        }

        /// <summary>
        /// 获取药品类型
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>药品类型</returns>
        public DataTable GetDrugType(Dictionary<string, string> query = null)
        {
            StringBuilder stb = new StringBuilder();
            if (query != null && query.Count > 0)
            {
                stb.AppendFormat(" 1=1 ");
                stb.Append(" and ( ");
                var i = 0;
                foreach (var pair in query)
                {
                    if (i == 0)
                    {
                        stb.AppendFormat(" {0} like'{1}%' ", pair.Key, pair.Value);
                    }
                    else
                    {
                        stb.AppendFormat(" OR {0} like '{1}%' ", pair.Key, pair.Value);
                    }

                    ++i;
                }

                stb.Append(" ) ");
            }

            return NewObject<DG_TypeDic>().gettable(stb.ToString());
        }
        #endregion

        #region 药品子类型
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="childType">药品值类型对象</param>
        /// <returns>是否成功</returns>
        public bool SaveChildDrugType(DG_ChildTypeDic childType)
        {
            List<Tuple<string, string, SqlOperator>> lst = new List<Tuple<string, string, SqlOperator>>();
            lst.Add(Tuple.Create("CTypeName", childType.CTypeName, SqlOperator.Equal));
            lst.Add(Tuple.Create("TypeID", childType.TypeID.ToString(), SqlOperator.Equal));
            IEnumerable<DG_ChildTypeDic> objs = NewObject<IDGDao>().GetEntityType<DG_ChildTypeDic>(lst, null);
            if (objs != null && objs.Any())
            {
                //同名记录存在
                if (objs.Count() > 1)
                {
                    return false;
                }

                var t = objs.FirstOrDefault(i => i.TypeID == childType.CTypeID);
                if (t != null)
                {
                    //完全没修改过不操作数据库
                    return true;
                }
                else
                {
                    return false;
                }
            }

            childType.save();
            return true;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>药品子类型</returns>
        public DataTable GetChildDrugType(Dictionary<string, string> query = null)
        {
            return NewObject<IDGDao>().GetChildDrugType(query);
        }

        /// <summary>
        /// 药品类型和子类型
        /// </summary>
        /// <returns>返回药品类型和子类型</returns>
        public DataTable GetDrugTypeAndChild()
        {
            return NewObject<IDGDao>().GetDrugTypeAndChild();
        }
        #endregion
    }
}
