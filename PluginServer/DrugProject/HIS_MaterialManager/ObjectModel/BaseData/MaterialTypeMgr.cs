using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MaterialManage;
using HIS_Entity.SqlAly;
using HIS_MaterialManager.Dao;

namespace HIS_MaterialManager.ObjectModel.BaseData
{
    /// <summary>
    /// 物资类型管理类
    /// </summary>
    public class MaterialTypeMgr : AbstractObjectModel
    {
        #region 物资类型

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="typeDic">类型字典实体</param>
        /// <returns>true成功false存在相同</returns>
        public bool SaveMaterialType(MW_TypeDic typeDic)
        {
            List<Tuple<string, string, SqlOperator>> lst = new List<Tuple<string, string, SqlOperator>>();
            lst.Add(Tuple.Create("TypeName", typeDic.TypeName, SqlOperator.Equal));
            lst.Add(Tuple.Create("ParentID", typeDic.ParentID.ToString(), SqlOperator.Equal));
            IEnumerable<MW_TypeDic> objs = NewObject<IMWDao>().GetEntityType<MW_TypeDic>(lst, null).Where(item => item.ParentID == 0);

            //同名记录存在
            if (objs != null && objs.Any())
            {
                if (objs.Count() > 1)
                {
                    return false;
                }

                var t = objs.FirstOrDefault(i => i.TypeID == typeDic.TypeID);

                //完全没修过
                if (t != null)
                {
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
        /// 获取物资类型
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>物资类型数据</returns>
        public DataTable GetMaterialType(Dictionary<string, string> query = null)
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
                        stb.AppendFormat(" {0} like '%{1}%' ", pair.Key, pair.Value);
                    }
                    else
                    {
                        stb.AppendFormat(" OR {0} like '%{1}%' ", pair.Key, pair.Value);
                    }

                    ++i;
                }

                stb.Append(" ) ");
            }

            //return NewObject<MW_TypeDic>().getlist<MW_TypeDic>(stb.ToString());
            return NewObject<MW_TypeDic>().gettable(stb.ToString());
        }

        /// <summary>
        /// 获取物资类型
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>物资类型</returns>
        public DataTable GetMaterialListType(Dictionary<string, string> query = null)
        {
            StringBuilder stb = new StringBuilder();
            if (query != null && query.Count > 0)
            {
                stb.AppendFormat(" 1=1 ");
                stb.Append(" and ( ");
                foreach (var pair in query)
                {
                    if (pair.Key != string.Empty)
                    {
                        stb.AppendFormat(" {0}={1} ", pair.Key, pair.Value);
                    }
                }

                stb.Append(" ) ");
            }

            return NewObject<MW_TypeDic>().gettable(stb.ToString());
        }
        #endregion

        #region 物资子类型
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="childType">物资子类型实体</param>
        /// <returns>true成功false存在同名</returns>
        public bool SaveChildMaterialType(MW_TypeDic childType)
        {
            List<Tuple<string, string, SqlOperator>> lst = new List<Tuple<string, string, SqlOperator>>();
            lst.Add(Tuple.Create("TypeName", childType.TypeName, SqlOperator.Equal));
            lst.Add(Tuple.Create("ParentID", childType.ParentID.ToString(), SqlOperator.Equal));
            IEnumerable<MW_TypeDic> objs = NewObject<IMWDao>().GetEntityType<MW_TypeDic>(lst, null).Where(item => item.ParentID > 0);

            //同名记录存在
            if (objs != null && objs.Any())
            {
                if (objs.Count() > 1)
                {
                    return false;
                }

                var t = objs.FirstOrDefault(i => i.TypeID == childType.TypeID);

                //完全没修改过不操作数据库
                if (t != null)
                {
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
        /// 获取物资子类型
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>物资子类型数据</returns>
        public DataTable GetChildMaterialType(Dictionary<string, string> query = null)
        {
            StringBuilder stb = new StringBuilder();
            if (query != null && query.Count > 0)
            {
                stb.AppendFormat(" ParentID>0 ");
                stb.Append(" and ( ");
                var i = 0;
                foreach (var pair in query)
                {
                    if (i == 0)
                    {
                        stb.AppendFormat(" {0} like '%{1}%' ", pair.Key, pair.Value);
                    }
                    else
                    {
                        stb.AppendFormat(" OR {0} like '%{1}%' ", pair.Key, pair.Value);
                    }

                    ++i;
                }

                stb.Append(" ) ");
            }

            return NewObject<MW_TypeDic>().gettable(stb.ToString());
        }
        #endregion
    }
}